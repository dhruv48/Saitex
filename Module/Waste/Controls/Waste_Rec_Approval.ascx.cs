using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DBLibrary;
using Common;
using errorLog;
public partial class Module_Waste_Controls_Waste_Rec_Approval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bindMaterialReceiptApproval();
                //if (Request.QueryString["TRN_TYPE"] != null)
                //{
                //    if (Request.QueryString["TRN_TYPE1"] != null)
                //    {
                //        bindMaterialReceiptApprovalForReceving(Request.QueryString["TRN_TYPE"].ToString());
                //    }
                //    else
                //    {
                //        bindMaterialReceiptApprovalForType(Request.QueryString["TRN_TYPE"].ToString());
                //    }
                //}
                //else
                //{
                //    bindMaterialReceiptApproval();
                //}
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }
    private DataTable CreateDataTable()
    {
        DataTable dtReceiptDetail = new DataTable();
        dtReceiptDetail.Columns.Add("YEAR", typeof(int));
        dtReceiptDetail.Columns.Add("COMP_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_NUMB", typeof(int));
        dtReceiptDetail.Columns.Add("WR_NO", typeof(string));
        dtReceiptDetail.Columns.Add("ITEM_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("ITEM_DESC", typeof(string));
        dtReceiptDetail.Columns.Add("UOM", typeof(string));
        dtReceiptDetail.Columns.Add("CAT_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_QTY", typeof(string));
        dtReceiptDetail.Columns.Add("CONF_FLAG", typeof(string));
        dtReceiptDetail.Columns.Add("CONF_DATE", typeof(DateTime));
        dtReceiptDetail.Columns.Add("CONF_BY", typeof(string));
        return dtReceiptDetail;
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            DataTable dtReceiptDetail = CreateDataTable();
            int totalRows = gvMaterialReceiptApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialReceiptApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblTRN_NUMB = (Label)thisGridViewRow.FindControl("lblWR_NUMB");
                    Label lblTRN_type = (Label)thisGridViewRow.FindControl("lblTRN_type");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    //TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    //TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");

                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtReceiptDetail.NewRow();

                        dr["YEAR"] = int.Parse(lblTRN_type.ToolTip.Trim());
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["TRN_TYPE"] = lblTRN_type.Text.Trim();
                        dr["WR_NO"] = lblTRN_NUMB.Text.Trim();
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "1";
                        dtReceiptDetail.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.TX_WASTE_IR_MST.Update_ReceiptForApproval(oUserLoginDetail.UserCode, dtReceiptDetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                CommonFuction.ShowMessage("Waste Received approved Successfully.");
                bindMaterialReceiptApproval();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in MRN Confirm.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "PrintItemPO.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/Saitex/Module/Inventory/Pages/ReceiptApproval.aspx", false);
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void bindMaterialReceiptApproval()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_IR_MST.GetDataForApprovalMain(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Waste Received for approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Waste Received for approval");
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindMaterialReceiptApprovalForType(string TRN_TYPE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_IR_MST.GetReceiptDataForApprovalForType(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Waste Received for approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Waste Received for approval");
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindMaterialReceiptApprovalForReceving(string TRN_TYPE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_IR_MST.GetReceiptDataForApprovalForReceving(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Waste Received for approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Waste Received for approval");
            }
        }
        catch
        {
            throw;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void gvMaterialReceiptApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;

                Label lblTRN_NUMB = (Label)grdRow.FindControl("lblTRN_NUMB");
                Label lblTRN_TYPE = (Label)grdRow.FindControl("lblTRN_TYPE");

                SaitexDM.Common.DataModel.TX_WASTE_IR_MST oTX_WASTE_IR_MST = new SaitexDM.Common.DataModel.TX_WASTE_IR_MST();
                oTX_WASTE_IR_MST.YEAR = int.Parse(lblTRN_TYPE.ToolTip.Trim());
                oTX_WASTE_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_WASTE_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_WASTE_IR_MST.WR_NO = lblTRN_NUMB.Text.Trim();
                oTX_WASTE_IR_MST.TRN_TYPE = lblTRN_TYPE.Text.Trim();

                DataTable dtTRN = SaitexBL.Interface.Method.TX_WASTE_IR_MST.GetTRN_DataByTRN_NUMB(int.Parse(lblTRN_TYPE.ToolTip.Trim()), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, lblTRN_NUMB.Text.Trim(), lblTRN_TYPE.Text.Trim());

                if (dtTRN != null && dtTRN.Rows.Count > 0)
                {
                    GridView grdTRN = (GridView)grdRow.FindControl("grdTRN");
                    grdTRN.DataSource = dtTRN;
                    grdTRN.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
        }
    }
}
