using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DBLibrary;
using Common;
using errorLog;


public partial class Module_Production_Controls_Warp_Entry : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtTrnDetail = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialControl();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControl()
    {
        imgbtnUpdate.Visible = false;
        imgbtnSave.Visible = true;
        ddltrn_no.Visible = false;
        txttrnno.Visible = true;
        GetNewTrnNo();
        txttrn_date.Text = System.DateTime.Now.ToShortDateString();
        BindShift();
        ddlshift.SelectedIndex = 0;
        BindPiNo();
        ddlpinumber.SelectedIndex = -1;
        BindTrnNumber();
        txtbusinesstype.Text = "";
        txtbusinesstype.ReadOnly = true;
        txtproduct_type.Text = "";
        txtbeamloading_time.Text = System.DateTime.Now.ToString();
        txtbeamunloading_time.Text = System.DateTime.Now.ToString();
        txtproduct_type.ReadOnly = true;
        txtordertype.Text = "";
        txtordertype.ReadOnly = true;
        txtpi_type.Text = "";
        txtpi_type.ReadOnly = true;
        txtbeamID.Text = "";
        txtlength.Text = "";
        txtbroakge.Text = "";
        txttip.Text = "";
        txtcreel.Text = "";
        txtoperator.Text = "";
        txtremark.Text = "";
        txtyarn_desc.Text = "";
        txtmechineid.Text = "Warping";

        if (ViewState["dtTrnDetail"] != null)
            dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];

        if (dtTrnDetail == null || dtTrnDetail.Rows.Count == 0)
            CreateTrnTable();
        dtTrnDetail.Rows.Clear();
        BindTrnGrid();
        BindShade();
        ddlshadecode.SelectedIndex = 0;
    }

    private void GetNewTrnNo()
    {
        string COMP_CODE = oUserLoginDetail.COMP_CODE;
        string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

        try
        {
            txttrnno.Text = SaitexBL.Interface.Method.WARP_ENTRY.GetNewTrnNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.UserCode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindShift()
    {
        DataTable dt = new DataTable();
        dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftName();
        ddlshift.DataSource = dt;
        ddlshift.DataTextField = "SFT_NAME";
        ddlshift.DataValueField = "SFT_NAME";
        ddlshift.Items.Insert(0, new ListItem("--------SELECT--------", ""));
        ddlshift.DataBind();
    }

    private void BindPiNo()
    {
        DataTable dt = SaitexBL.Interface.Method.WARP_ENTRY.BindPiNo();
        ddlpinumber.Items.Clear();
        ddlpinumber.DataSource = dt;
        ddlpinumber.DataTextField = "ORDER_NO";
        ddlpinumber.DataValueField = "CONTAIN";
        ddlpinumber.DataBind();
        ddlpinumber.Items.Insert(0, new ListItem("-----Select-----"));
        dt.Dispose();
        dt = null;

    }

    private void BindTrnNumber()
    {
        DataTable dt = SaitexBL.Interface.Method.WARP_ENTRY.BindTrnNumber();
        ddltrn_no.Items.Clear();
        ddltrn_no.DataSource = dt;
        ddltrn_no.DataTextField = "TRN_NUMB";
        ddltrn_no.DataValueField = "TRN_NUMB";
        ddltrn_no.DataBind();
        ddltrn_no.Items.Insert(0, new ListItem("-----Select-----"));
        dt.Dispose();
        dt = null;

    }

    protected void ddlpinumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindYarnCode();
        string cString = ddlpinumber.SelectedValue.ToString().Trim();
        char[] splitter = { '@' };
        string[] arrstring = cString.Split(splitter);
        txtbusinesstype.Text = arrstring[0].ToString();
        txtproduct_type.Text = arrstring[1].ToString();
        txtordertype.Text = arrstring[2].ToString();
        txtpi_type.Text = arrstring[3].ToString();
    }

    private void BindYarnCode()
    {
        string ORDER_NO = ddlpinumber.SelectedItem.ToString();

        try
        {
            DataTable dt = SaitexBL.Interface.Method.WARP_ENTRY.BindYarnCode(ORDER_NO);
            ddlyarn_code.Items.Clear();
            ddlyarn_code.DataSource = dt;
            ddlyarn_code.DataTextField = "YARN_CODE";
            ddlyarn_code.DataValueField = "YARN_DESC";
            ddlyarn_code.DataBind();
            ddlyarn_code.Items.Insert(0, new ListItem("------Select-------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindShade()
    {
        DataTable dt = SaitexBL.Interface.Method.WARP_ENTRY.BindShade();
        ddlshadecode.Items.Clear();
        ddlshadecode.DataSource = dt;
        ddlshadecode.DataTextField = "shade_code";
        ddlshadecode.DataValueField = "shade_code";
        ddlshadecode.DataBind();
        dt.Dispose();
        dt = null;
    }

    private void InsertData()
    {
        try
        {
            SaitexDM.Common.DataModel.WARP_ENTRY oWARP_ENTRY = new SaitexDM.Common.DataModel.WARP_ENTRY();
            int iRecordFound = 0;

            oWARP_ENTRY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oWARP_ENTRY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oWARP_ENTRY.ORDER_NO = ddlpinumber.SelectedItem.Text.Trim().ToString();
            oWARP_ENTRY.TRN_NUMB = int.Parse(txttrnno.Text);
            oWARP_ENTRY.MECHINE_ID = txtmechineid.Text.Trim();
            oWARP_ENTRY.TRN_DATE = DateTime.Parse(txttrn_date.Text);
            oWARP_ENTRY.SHIFT = ddlshift.SelectedValue.ToString();
            oWARP_ENTRY.BEAM_ID = txtbeamID.Text.Trim();
            oWARP_ENTRY.B_LENGTH = double.Parse(txtlength.Text.Trim());
            oWARP_ENTRY.BROAKGE = txtbroakge.Text.Trim();
            oWARP_ENTRY.LOADING_TIME = DateTime.Parse(txtbeamloading_time.Text.Trim());
            oWARP_ENTRY.UNLOADING_TIME = DateTime.Parse(txtbeamunloading_time.Text.Trim());
            oWARP_ENTRY.TIP = txttip.Text.Trim();
            oWARP_ENTRY.CREEL = txtcreel.Text.Trim();
            oWARP_ENTRY.T_OPERATOR = txtoperator.Text.Trim();
            oWARP_ENTRY.T_REMARK = txtremark.Text.Trim();
            oWARP_ENTRY.TUSER = oUserLoginDetail.UserCode;

            bool Result = SaitexBL.Interface.Method.WARP_ENTRY.InsertWarpData(oWARP_ENTRY, dtTrnDetail, out iRecordFound);
            {
                if (Result)
                {
                    string msg = "Trn Number " + iRecordFound + " Successfully Saved.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
                    InitialControl();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Trn saving Failed');", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void UpdateData()
    {
        try
        {
            SaitexDM.Common.DataModel.WARP_ENTRY oWARP_ENTRY = new SaitexDM.Common.DataModel.WARP_ENTRY();
            oWARP_ENTRY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oWARP_ENTRY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oWARP_ENTRY.ORDER_NO = ddlpinumber.SelectedItem.Text.Trim().ToString();

            oWARP_ENTRY.TRN_NUMB = int.Parse(ddltrn_no.SelectedItem.Text);
            oWARP_ENTRY.MECHINE_ID = txtmechineid.Text.Trim();
            oWARP_ENTRY.TRN_DATE = DateTime.Parse(txttrn_date.Text);
            oWARP_ENTRY.SHIFT = ddlshift.SelectedValue.ToString();
            oWARP_ENTRY.BEAM_ID = txtbeamID.Text.Trim();
            oWARP_ENTRY.B_LENGTH = double.Parse(txtlength.Text.Trim());
            oWARP_ENTRY.BROAKGE = txtbroakge.Text.Trim();
            oWARP_ENTRY.LOADING_TIME = DateTime.Parse(txtbeamloading_time.Text.Trim());
            oWARP_ENTRY.UNLOADING_TIME = DateTime.Parse(txtbeamunloading_time.Text.Trim());
            oWARP_ENTRY.TIP = txttip.Text.Trim();
            oWARP_ENTRY.CREEL = txtcreel.Text.Trim();
            oWARP_ENTRY.T_OPERATOR = txtoperator.Text.Trim();
            oWARP_ENTRY.T_REMARK = txtremark.Text.Trim();
            oWARP_ENTRY.TUSER = oUserLoginDetail.UserCode;

            bool Result = SaitexBL.Interface.Method.WARP_ENTRY.UpdateWarpData(oWARP_ENTRY, dtTrnDetail);
            {
                if (Result)
                {
                    string msg = "Trn Number Successfully UpDate.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
                    InitialControl();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["dtTrnDetail"] != null)
                    dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];
                if (dtTrnDetail.Rows.Count > 0)
                {
                    InsertData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No trn selected. Please enter Transaction detail');", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["dtTrnDetail"] != null)
                    dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];
                if (dtTrnDetail.Rows.Count > 0)
                {
                    UpdateData();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        imgbtnUpdate.Visible = true;
        imgbtnSave.Visible = false;
        ddltrn_no.Visible = true;
        txttrnno.Visible = false;

    }

    private void GetDataByTrnNo()
    {
        try
        {
            if (ViewState["dtTrnDetail"] != null)
            {
                dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];
            }
            string trn_no = ddltrn_no.SelectedValue.Trim();

            if (dtTrnDetail == null || dtTrnDetail.Rows.Count == 0)
            {
                CreateTrnTable();
                dtTrnDetail.Rows.Clear();
                int iRecordFound = GetMainDataByTrnNo(CommonFuction.funFixQuotes(trn_no));
                BindTrnGrid();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int GetMainDataByTrnNo(string trn_no)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.WARP_ENTRY.GetMainDataByTrnNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, trn_no);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtbusinesstype.Text = dt.Rows[0]["BUSINESS_TYPE"].ToString().Trim();
                txtproduct_type.Text = dt.Rows[0]["PRODUCT_TYPE"].ToString().Trim();
                txtordertype.Text = dt.Rows[0]["ORDER_TYPE"].ToString().Trim();
                txtpi_type.Text = dt.Rows[0]["PI_NO"].ToString().Trim();
                txttrnno.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();
                txtmechineid.Text = dt.Rows[0]["SHIFT"].ToString().Trim();
                txttrn_date.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                ddlshift.SelectedValue = dt.Rows[0]["SHIFT"].ToString().Trim();
                ddlpinumber.SelectedItem.Text = dt.Rows[0]["ORDER_NO"].ToString().Trim();
                txtbeamID.Text = dt.Rows[0]["BEAM_ID"].ToString().Trim();
                txtlength.Text = dt.Rows[0]["B_LENGTH"].ToString().Trim();
                txtbroakge.Text = dt.Rows[0]["BROAKGE"].ToString().Trim();
                txtbeamloading_time.Text = DateTime.Parse(dt.Rows[0]["LOADING_TIME"].ToString().Trim()).ToShortDateString();
                txtbeamunloading_time.Text = DateTime.Parse(dt.Rows[0]["UNLOADING_TIME"].ToString().Trim()).ToShortDateString();
                txttip.Text = dt.Rows[0]["TIP"].ToString().Trim();
                txtcreel.Text = dt.Rows[0]["CREEL"].ToString().Trim();
                txtoperator.Text = dt.Rows[0]["T_OPERATOR"].ToString().Trim();
                txtremark.Text = dt.Rows[0]["T_REMARK"].ToString().Trim();
            }
            if (iRecordFound == 1)
            {
                DataTable dtTemp = GetTrnDataByTrnCode(trn_no);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                }

            }
            else
            {
                //string msg = "Dear " + oUserLoginDetail.Username + " !! TR already approved. Modification not allowed.";
                //Common.CommonFuction.ShowMessage(msg);
                InitialControl();
                imgbtnSave.Visible = false;
                imgbtnUpdate.Visible = true;
                RefreshDetailRow();
            }
            return iRecordFound;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetTrnDataByTrnCode(string trn_no)
    {
        try
        {
            DataTable dtTemp = SaitexBL.Interface.Method.WARP_ENTRY.GetTrnDataBytrnNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, trn_no);
            return dtTemp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtTrnDetail"] != null)
                dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];

            if (dtTrnDetail == null || dtTrnDetail.Rows.Count == 0)
                CreateTrnTable();
            dtTrnDetail.Rows.Clear();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    //if (drTemp["YEAR"].ToString().Equals(oUserLoginDetail.DT_STARTDATE.Year.ToString()))
                    // {
                    DataRow dr = dtTrnDetail.NewRow();
                    dr["UniqueId"] = dtTrnDetail.Rows.Count + 1;
                    // dr["IndentDetailNumber"] = 0;
                    dr["TRN_NUMB"] = drTemp["TRN_NUMB"];
                    dr["COMP_CODE"] = drTemp["COMP_CODE"];
                    dr["BRANCH_CODE"] = drTemp["BRANCH_CODE"];
                    dr["YARN_CODE"] = drTemp["YARN_CODE"];
                    dr["YARN_DESC"] = drTemp["YARN_DESC"];
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"];
                    dr["NO_OF_END"] = drTemp["NO_OF_END"];
                    dr["YRN_REQ_QTY"] = drTemp["YRN_REQ_QTY"];
                    dr["YARN_STD"] = drTemp["YARN_STD"];
                    dtTrnDetail.Rows.Add(dr);
                    // }
                }
                dtTemp = null;

                ViewState["dtTrnDetail"] = dtTrnDetail;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        UpdateData();
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }

    private void CreateTrnTable()
    {
        dtTrnDetail = new DataTable();
        dtTrnDetail.Columns.Add("UniqueId", typeof(int));
        dtTrnDetail.Columns.Add("COMP_CODE", typeof(string));
        dtTrnDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtTrnDetail.Columns.Add("TRN_NUMB", typeof(int));
        dtTrnDetail.Columns.Add("YARN_CODE", typeof(string));
        dtTrnDetail.Columns.Add("YARN_DESC", typeof(string));

        dtTrnDetail.Columns.Add("SHADE_CODE", typeof(string));
        dtTrnDetail.Columns.Add("NO_OF_END", typeof(string));
        dtTrnDetail.Columns.Add("YRN_REQ_QTY", typeof(double));
        dtTrnDetail.Columns.Add("YARN_STD", typeof(string));
    }

    private bool SearchItemCodeInGrid(string yrn_code, string shade, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in WarpTrnDetail.Rows)
            {
                Label lblyrncode = (Label)grdRow.FindControl("lblyrncode");
                LinkButton lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete");
                Label lblshade_code = (Label)grdRow.FindControl("lblshade_code");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (lblyrncode.Text.Trim() == yrn_code && UniqueId != iUniqueId && lblshade_code.Text.Trim() == shade)
                    Result = true;
            }
            return Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void RefreshDetailRow()
    {
        ddlyarn_code.SelectedIndex = -1;
        txtyarn_desc.Text = "";
        // txtmechine_id.Text = "";
        ddlshadecode.SelectedIndex = 0;
        txtno_of_end.Text = "";
        txtreq_qty.Text = "";
        txtyarn_std.Text = "";
    }

    private void BindTrnGrid()
    {
        if (ViewState["dtTrnDetail"] != null)
            dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];

        WarpTrnDetail.DataSource = dtTrnDetail;
        WarpTrnDetail.DataBind();
    }

    protected void lnkbtncancel_Click(object sender, EventArgs e)
    {
        RefreshDetailRow();
    }

    protected void WarpTrnDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "lnkEdit1")
            {
                EditTrnDataInGrid(UniqueId);
            }
            else if (e.CommandName == "lnkDelete1")
            {
                DeleteTrnDataInGrid(UniqueId);
                BindTrnGrid();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveTrn()
    {
        {
            if (ViewState["dtTrnDetail"] != null)
                dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];
            if (dtTrnDetail == null)
                CreateTrnTable();

            if (txtyarn_std.Text != "" || txtreq_qty.Text != "")
            {

                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                {
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                }
                bool bb = SearchItemCodeInGrid(ddlyarn_code.SelectedItem.Text, ddlshadecode.SelectedValue.ToString().Trim(), UniqueId);
                if (!bb)
                {
                    if (UniqueId > 0)
                    {
                        DataView dv = new DataView(dtTrnDetail);
                        dv.RowFilter = "UniqueId=" + UniqueId;
                        if (dv.Count > 0)
                        {
                            dv[0]["COMP_CODE"] = oUserLoginDetail.COMP_CODE.ToString();
                            dv[0]["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE.ToString();
                            dv[0]["TRN_NUMB"] = txttrnno.Text.Trim();
                            dv[0]["YARN_CODE"] = ddlyarn_code.SelectedItem.ToString().Trim();
                            dv[0]["YARN_DESC"] = txtyarn_desc.Text.Trim();
                            //dv[0]["MECHINE_ID"] = txtmechine_id.Text.Trim();
                            dv[0]["SHADE_CODE"] = ddlshadecode.SelectedValue.ToString().Trim();
                            dv[0]["NO_OF_END"] = txtno_of_end.Text.Trim();
                            dv[0]["YRN_REQ_QTY"] = double.Parse(txtreq_qty.Text.Trim());
                            dv[0]["YARN_STD"] = txtyarn_std.Text.Trim();

                            dtTrnDetail.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtTrnDetail.NewRow();
                        dr["UniqueId"] = dtTrnDetail.Rows.Count + 1;
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE.ToString();
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE.ToString();
                        dr["TRN_NUMB"] = txttrnno.Text.Trim();
                        dr["YARN_CODE"] = ddlyarn_code.SelectedItem.ToString().Trim();
                        dr["YARN_DESC"] = txtyarn_desc.Text.Trim();
                        //dr["MECHINE_ID"] = txtmechine_id.Text.Trim();
                        dr["SHADE_CODE"] = ddlshadecode.SelectedValue.ToString().Trim();
                        dr["NO_OF_END"] = txtno_of_end.Text.Trim();
                        dr["YRN_REQ_QTY"] = double.Parse(txtreq_qty.Text.Trim());
                        dr["YARN_STD"] = txtyarn_std.Text.Trim();

                        dtTrnDetail.Rows.Add(dr);
                    }
                    RefreshDetailRow();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Select Another Yarn Code or Shade');", true);
                }
                ViewState["dtTrnDetail"] = dtTrnDetail;
                BindTrnGrid();
            }
            else
            {
                CommonFuction.ShowMessage("Please Enter Requested Qty OR yarn std");
            }

        }

    }

    private void EditTrnDataInGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtTrnDetail"] != null)
                dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];
            DataView dv = new DataView(dtTrnDetail);

            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                BindYarnCode();
                ddlyarn_code.SelectedItem.Text = dv[0]["YARN_CODE"].ToString();
                txtyarn_desc.Text = dv[0]["YARN_DESC"].ToString();

                BindShade();
                ddlshadecode.SelectedValue = dv[0]["SHADE_CODE"].ToString();
                txtno_of_end.Text = dv[0]["NO_OF_END"].ToString();
                txtreq_qty.Text = dv[0]["YRN_REQ_QTY"].ToString();
                txtyarn_std.Text = dv[0]["YARN_STD"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteTrnDataInGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtTrnDetail"] != null)
                dtTrnDetail = (DataTable)ViewState["dtTrnDetail"];
            if (WarpTrnDetail.Rows.Count == 1)
            {
                dtTrnDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTrnDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtTrnDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTrnDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
                ViewState["dtTrnDetail"] = dtTrnDetail;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlyarn_code_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtyarn_desc.Text = ddlyarn_code.SelectedValue;
    }

    protected void lnkbtnsave_Click1(object sender, EventArgs e)
    {
        SaveTrn();
    }

    protected void ddltrn_no_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDataByTrnNo();
        BindYarnCode();
    }

    protected void lnkbtnrecieve_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlyarn_code.SelectedValue != null && txtreq_qty.Text != null)
            {
                string URL = "Warping_issu_4Prod.aspx";
                URL = URL + "?yarn_code=" + ddlyarn_code.SelectedValue;
                URL = URL + "&shadecode=" + ddlyarn_code.SelectedValue;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }
            else
            {
                CommonFuction.ShowMessage("Please select Item to adjust Indent");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting adjustment.\r\nSee error log for detail."));
        }
    }
}
