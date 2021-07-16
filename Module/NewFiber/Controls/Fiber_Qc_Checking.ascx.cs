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
using Obout.ComboBox;
using System.Collections.Generic;

public partial class Module_NewFiber_Controls_Fiber_Qc_Checking : System.Web.UI.UserControl
{  
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBLQC = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                bool NYQC = false;
                if (Request.QueryString["NYQC"] != null)
                {
                    bool.TryParse(Request.QueryString["NYQC"].ToString(), out NYQC);
                }
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                if (!IsPostBack)
                {
                    if (!NYQC)
                    {
                        InitialisePage();
                    }
                    else
                    {
                        tdSave.Visible = true;
                        tdUpdate.Visible = false;
                        tdFind.Visible = true;
                        lblMode.Text = "Save";
                        Already_QC_Checked();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_QC_Checking.aspx?NYQC=true");
    }

    private void Already_QC_Checked()
    {
        int YEAR = 0, TRN_NUMB = 0;

        string Fiber_Code = string.Empty;
        if (ddlyarncode.SelectedIndex > -1)
        {
            Fiber_Code = ddlyarncode.SelectedText.Trim();
        }
        if (ddlTRNNumber.SelectedIndex > -1)
        {
            int.TryParse(hdYEAR.Value, out YEAR);
            int.TryParse(hdTRN_NUMB.Value, out TRN_NUMB);
        }
        dtDetailTBLQC = SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_NUMB, YEAR, Fiber_Code);
        ViewState["dtDetailTBLQC"] = dtDetailTBLQC;
        BindGridFromDataTable();
        gvMaterialReceiptApproval.Columns[14].Visible = true;

        gvMaterialReceiptApproval.Columns[15].Visible = false;
    }

    private void InitialisePage()
    {
        try
        {
            ddlyarncode.SelectedIndex = -1;
            hdTRN_NUMB.Value = string.Empty;
            hdTRN_TYPE.Value = string.Empty;
            hdYEAR.Value = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            ViewState["dtDetailTBLQC"] = null;

            gvMaterialReceiptApproval.DataSource = null;
            gvMaterialReceiptApproval.DataBind();
            Getdata();
            gvMaterialReceiptApproval.Columns[14].Visible = false;
            gvMaterialReceiptApproval.Columns[15].Visible = false;
        }
        catch
        {
            throw;
        }
    }
    private DataTable CreateDataTable()
    {
        DataTable dtReceiptDetail = new DataTable();
        dtReceiptDetail.Columns.Add("COMP_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_NUMB", typeof(double));
        dtReceiptDetail.Columns.Add("QC_NUMB", typeof(double));
        dtReceiptDetail.Columns.Add("SR_NUMB", typeof(int));
        dtReceiptDetail.Columns.Add("YEAR", typeof(int));
        dtReceiptDetail.Columns.Add("TRN_YEAR", typeof(int));
        dtReceiptDetail.Columns.Add("FIBER_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("STD_TYPE", typeof(string));
        dtReceiptDetail.Columns.Add("QC_VALUE", typeof(double));
        dtReceiptDetail.Columns.Add("QC_RESULT", typeof(string));
        dtReceiptDetail.Columns.Add("QC_DONE_BY", typeof(string));
        dtReceiptDetail.Columns.Add("QC_REMARKS", typeof(string));
        dtReceiptDetail.Columns.Add("IsAdd", typeof(bool));


        return dtReceiptDetail;
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        lblMode.Text = "Update";
        ddlTRNNumber.SelectedIndex = -1;
        Getdata();
        gvMaterialReceiptApproval.Columns[14].Visible = false;

        gvMaterialReceiptApproval.Columns[15].Visible = true;
    }

    private void Save()
    {
        try
        {
            List<DataRow> rowdelete = new List<DataRow>();
            DataTable dt = CreateDataTable();
            int UniqueId = 0;
            int PREVIOUS_NUMB = 0, PREVIOUSTRN_YEAR = 0;
            string FIBER_CODE = "";
            foreach (GridViewRow row in gvMaterialReceiptApproval.Rows)
            {
                bool savedata = false;
                UniqueId++;
                string QC_RESULT = "";
                int QC_NUMB = 0, QC_Year = 0, TRN_YEAR = 0, TRN_NUMB = 0;
                double QC_Value = 0, Max_Value = 0, Min_Value = 0;
                TextBox lblQC_VALUE = (TextBox)row.FindControl("lblQC_VALUE");
                Label lblMaxValue = (Label)row.FindControl("lblMaxValue");
                Label lblMinValue = (Label)row.FindControl("lblMinValue");
                Label lblTRN_Year = (Label)row.FindControl("lblTRN_Year");
                Label lblTRN_NUMB = (Label)row.FindControl("lblTRN_NUMB");
                Label lblTRN_TYPE = (Label)row.FindControl("lblTRN_TYPE");
                TextBox lblREMARKS = (TextBox)row.FindControl("lblREMARKS");
                Label lblSTD_TYPE = (Label)row.FindControl("lblSTD_TYPE");
                Label lblYarn_Code = (Label)row.FindControl("lblYarn_Code");
                Label lblQC_NUMB = (Label)row.FindControl("lblQC_NUMB");
                Label lblTRN_YEAR = (Label)row.FindControl("lblTRN_YEAR");
                Label lblQC_Year = (Label)row.FindControl("lblQC_Year");
                CheckBox chkApproved = (CheckBox)row.FindControl("chkApproved");
                CheckBox chkEdit = (CheckBox)row.FindControl("chkEdit");
                double.TryParse(lblMaxValue.Text, out Max_Value);
                double.TryParse(lblMinValue.Text, out Min_Value);

                int.TryParse(lblTRN_NUMB.Text, out TRN_NUMB);
                int.TryParse(lblQC_NUMB.Text, out QC_NUMB);
                int.TryParse(lblTRN_YEAR.Text, out TRN_YEAR);
                int.TryParse(lblQC_Year.Text, out QC_Year);

                if (!string.IsNullOrEmpty(lblQC_VALUE.Text))
                {
                    if (double.TryParse(lblQC_VALUE.Text, out QC_Value))
                    {

                        if (QC_Value <= Max_Value && QC_Value >= Min_Value)
                        {
                            QC_RESULT = "1";
                        }
                        else
                        {
                            QC_RESULT = "0";
                        }
                        if (gvMaterialReceiptApproval.Columns[14].Visible == true && chkApproved.Checked == true)
                        {
                            savedata = true;
                        }
                        else if (QC_NUMB == 0 && gvMaterialReceiptApproval.Columns[14].Visible == false)
                        {
                            savedata = true;
                        }
                        else if (QC_NUMB != 0 && gvMaterialReceiptApproval.Columns[14].Visible == false && tdUpdate.Visible == true && chkEdit.Checked == true)
                        {
                            savedata = true;
                        }
                        if (savedata)
                        {
                            DataRow dr = dt.NewRow();
                            dr["QC_NUMB"] = QC_NUMB;
                            dr["SR_NUMB"] = UniqueId;
                            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                            dr["TRN_NUMB"] = TRN_NUMB;
                            dr["TRN_YEAR"] = TRN_YEAR;
                            dr["TRN_TYPE"] = lblTRN_TYPE.Text.Trim();
                            dr["YEAR"] = QC_Year == 0 ? oUserLoginDetail.DT_STARTDATE.Year : QC_Year;
                            dr["FIBER_CODE"] = lblYarn_Code.Text.Trim();
                            dr["STD_TYPE"] = lblSTD_TYPE.Text.Trim();
                            dr["QC_VALUE"] = QC_Value;
                            dr["QC_RESULT"] = QC_RESULT;
                            dr["QC_DONE_BY"] = oUserLoginDetail.UserCode;
                            dr["QC_REMARKS"] = lblREMARKS.Text;
                            dr["IsAdd"] = chkApproved != null ? chkApproved.Checked : false;
                            dt.Rows.Add(dr);

                        }

                    }
                }

            }
            bool Is_Save = false;
            if (tdSave.Visible)
            {
                Is_Save = true;
            }
            if (ViewState["dtDetailTBLQC"] != null && dt != null && dt.Rows.Count > 0)
            {
                dtDetailTBLQC = (DataTable)ViewState["dtDetailTBLQC"];
                foreach (DataRow dr in dtDetailTBLQC.Rows)
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "TRN_TYPE='" + dr["TRN_TYPE"].ToString() + "' and TRN_YEAR='" + dr["TRN_YEAR"].ToString() + "' and TRN_NUMB='" + dr["TRN_NUMB"].ToString() + "' and FIBER_CODE='" + dr["FIBER_CODE"].ToString() + "' and STD_TYPE='" + dr["STD_TYPE"].ToString() + "'";
                    if (dv != null && dv.Count > 0)
                    {
                        int.TryParse(dv[0]["TRN_NUMB"].ToString(), out PREVIOUS_NUMB);
                        int.TryParse(dv[0]["TRN_YEAR"].ToString(), out PREVIOUSTRN_YEAR);
                        FIBER_CODE = dv[0]["FIBER_CODE"].ToString();

                    }
                    else if (int.Parse(dr["TRN_YEAR"].ToString()) == PREVIOUSTRN_YEAR && int.Parse(dr["TRN_NUMB"].ToString()) == PREVIOUS_NUMB && dr["FIBER_CODE"].ToString() == FIBER_CODE)
                    {
                        rowdelete.Add(dr);
                    }
                }
                if (rowdelete != null && rowdelete.Count > 0)
                {
                    foreach (DataRow dr in rowdelete)
                    {
                        DataRow[] drrows = dt.Select("TRN_TYPE='" + dr["TRN_TYPE"].ToString() + "' and TRN_YEAR='" + dr["TRN_YEAR"].ToString() + "' and TRN_NUMB='" + dr["TRN_NUMB"].ToString() + "' and FIBER_CODE='" + dr["FIBER_CODE"].ToString() + "'");
                        foreach (DataRow drr in drrows)
                        {
                            dt.Rows.Remove(drr);
                        }

                    }
                }
                dt.AcceptChanges();
                if (dt != null && dt.Rows.Count > 0)
                {
                    bool iResult = SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.Insert_Yarn_QC_Checking(dt, oUserLoginDetail.UserCode, oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.UserCode, Is_Save);
                    if (iResult)
                    {
                        InitialisePage();
                        CommonFuction.ShowMessage("QC Checking saved Successfully.");

                    }
                }
                else
                {
                    CommonFuction.ShowMessage("Please Fill All Standard type details!!!");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please Fill All Standard type details!!!");
            }


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in QC Checking .\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Yarn_QC_CheckingReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=0,toolbar=0,menubar=0,location=100,scrollbars=1,resizable=1,width=800,height=500');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
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



    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }




    protected void gvMaterialReceiptApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string stdvalue = DataBinder.Eval(e.Row.DataItem, "QC_Value").ToString();

            double QC_Value = 0, Max_Value = 0, Min_Value = 0;
            TextBox lblQC_VALUE = (TextBox)e.Row.FindControl("lblQC_VALUE");
            TextBox lblREMARKS = (TextBox)e.Row.FindControl("lblREMARKS");
            Label lblMaxValue = (Label)e.Row.FindControl("lblMaxValue");
            Label lblMinValue = (Label)e.Row.FindControl("lblMinValue");
            Image imgSuccess = (Image)e.Row.FindControl("imgSuccess");
            Image imgFail = (Image)e.Row.FindControl("imgFail");
            if (stdvalue == "0")
            {
                lblQC_VALUE.Text = string.Empty;
            }
            double.TryParse(lblMaxValue.Text, out Max_Value);
            double.TryParse(lblMinValue.Text, out Min_Value);

            if (!string.IsNullOrEmpty(lblQC_VALUE.Text))
            {
                if (double.TryParse(lblQC_VALUE.Text, out QC_Value))
                {
                    if (QC_Value <= Max_Value && QC_Value >= Min_Value)
                    {
                        imgSuccess.Visible = true;
                        imgFail.Visible = false;

                    }
                    else
                    {
                        imgSuccess.Visible = false;
                        imgFail.Visible = true;

                    }
                }
                else
                {
                    imgSuccess.Visible = false;
                    imgFail.Visible = false;
                }
            }
            else
            {
                imgSuccess.Visible = false;
                imgFail.Visible = false;
            }

            if (tdUpdate.Visible)
            {
                lblQC_VALUE.ReadOnly = true;
                lblREMARKS.ReadOnly = true;
            }
            else
            {
                lblQC_VALUE.ReadOnly = false;
                lblREMARKS.ReadOnly = false;

            }


        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        Save();

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        Save();

    }
    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/NewFiber/Pages/Fiber_QC_CheckingList.aspx");
        //Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_QC_CheckingList.aspx");
    }


    private void BindGridFromDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBLQC"] != null)
                dtDetailTBLQC = (DataTable)ViewState["dtDetailTBLQC"];
            gvMaterialReceiptApproval.DataSource = dtDetailTBLQC;
            gvMaterialReceiptApproval.DataBind();

        }
        catch
        {
            throw;
        }
    }





    protected void ddlTRNNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetReceiving(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetReceivingCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Standard No selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {
            DataTable dt = null;
            if (tdSave.Visible)
            {
                string CommandText = "SELECT   * FROM   (SELECT   * FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR,c.TRN_TYPE,AB.PRTY_NAME,AB.PRTY_CODE, (c.TRN_TYPE||'@'||c.YEAR)as combined  FROM   TX_FIBER_NEW_IR_MST a, TX_FIBER_NEW_IR_TRN c, TX_FIBER_NEW_MASTER d,TX_VENDOR_MST AB  WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE AND a.YEAR = c.YEAR AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND A.PRTY_CODE = AB.PRTY_CODE AND c.COMP_CODE = d.COMP_CODE  AND c.FIBER_CODE = d.FIBER_CODE  AND NVL (d.QC_REQUIRED, 0) = '1' AND NVL (a.CONF_FLAG, 0) = '1' AND SUBSTR(a.TRN_TYPE,0,1)='R' AND c.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) WHERE   ROWNUM <= 15";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " and TRN_NUMB not in (SELECT TRN_NUMB from (SELECT  TRN_NUMB FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR,c.TRN_TYPE,AB.PRTY_NAME,AB.PRTY_CODE, (c.TRN_TYPE||'@'||c.YEAR)as combined FROM   TX_FIBER_NEW_IR_MST a, TX_FIBER_NEW_IR_TRN c, TX_FIBER_NEW_MASTER d,TX_VENDOR_MST AB WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE AND a.YEAR = c.YEAR AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND A.PRTY_CODE = AB.PRTY_CODE AND c.COMP_CODE = d.COMP_CODE AND c.FIBER_CODE = d.FIBER_CODE AND NVL (d.QC_REQUIRED, 0) = '1' AND NVL (a.CONF_FLAG, 0) = '1' AND SUBSTR(a.TRN_TYPE,0,1)='R' AND c.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) where rownum<='" + startOffset + "')";
                }

                string SortExpression = "  ORDER BY TRN_NUMB DESC ";
                string SearchQuery = "%" + text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
                string CommandText1 = " SELECT   DISTINCT d.TRN_NUMB,d.TRN_TYPE,d.TRN_YEAR  FROM   FIBER_QC d WHERE  d.YEAR = " + oUserLoginDetail.DT_STARTDATE.Year + "  AND d.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ";
                DataTable dt1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, "", "", "", "", "");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        DataView dv = new DataView(dt);
                        dv.RowFilter = "TRN_NUMB=" + dr["TRN_NUMB"].ToString() + " and TRN_TYPE='" + dr["TRN_TYPE"].ToString() + "' and YEAR=" + dr["TRN_YEAR"].ToString() + " ";
                        if (dv.Count > 0)
                        {
                            dt.Rows.Remove(dv[0].Row);
                            dt.AcceptChanges();
                        }
                    }
                }
            }
            else if (tdUpdate.Visible)
            {
                string CommandText = "SELECT   * FROM   (SELECT   * FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR,c.TRN_TYPE, (c.TRN_TYPE||'@'||c.YEAR)as combined  FROM   FIBER_QC a, TX_FIBER_NEW_IR_TRN c, TX_FIBER_NEW_MASTER d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE  AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE  AND c.FIBER_CODE = d.FIBER_CODE  AND NVL (d.QC_REQUIRED, 0) = '1' AND NVL (a.QC_CONF_FLAG, 0) = '0' AND SUBSTR(c.TRN_TYPE,0,1)='R' AND c.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) WHERE   ROWNUM <= 15";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " and TRN_NUMB not in (SELECT TRN_NUMB from (SELECT  TRN_NUMB FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR,c.TRN_TYPE, (c.TRN_TYPE||'@'||c.YEAR)as combined FROM   FIBER_QC a, TX_FIBER_NEW_IR_TRN c, TX_FIBER_NEW_MSTER d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE  AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE AND c.FIBER_CODE = d.FIBER_CODE AND NVL (d.QC_REQUIRED, 0) = '1' AND NVL (a.QC_CONF_FLAG, 0) = '0' AND SUBSTR(c.TRN_TYPE,0,1)='R' AND c.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) where rownum<='" + startOffset + "')";
                }

                string SortExpression = "  ORDER BY TRN_NUMB DESC ";
                string SearchQuery = "%" + text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            }
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetReceivingCount(string text)
    {
        try
        {
            DataTable dt = null;
            if (tdSave.Visible)
            {
                string CommandText = "SELECT   * FROM   (SELECT   * FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR,c.TRN_TYPE, (c.TRN_TYPE||'@'||c.YEAR)as combined FROM   TX_FIBER_NEW_IR_MST a, TX_FIBER_NEW_IR_TRN c, TX_FIBER_NEW_MASTER d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE AND a.YEAR = c.YEAR AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE  AND c.FIBER_CODE = d.FIBER_CODE  AND NVL (d.QC_REQUIRED, 0) = '1' AND NVL (a.CONF_FLAG, 0) = '1' AND SUBSTR(a.TRN_TYPE,0,1)='R' AND c.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery))";
                string SearchQuery = "%" + text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", SearchQuery, "");
                string CommandText1 = " SELECT   DISTINCT d.TRN_NUMB,d.TRN_TYPE,d.TRN_YEAR  FROM   FIBER_QC d WHERE  d.YEAR = " + oUserLoginDetail.DT_STARTDATE.Year + "  AND d.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ";
                DataTable dt1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, "", "", "", "", "");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        DataView dv = new DataView(dt);
                        dv.RowFilter = "TRN_NUMB=" + dr["TRN_NUMB"].ToString() + " and TRN_TYPE='" + dr["TRN_TYPE"].ToString() + "' and YEAR=" + dr["TRN_YEAR"].ToString() + " ";
                        if (dv.Count > 0)
                        {
                            dt.Rows.Remove(dv[0].Row);
                            dt.AcceptChanges();
                        }
                    }
                }
            }
            else if (tdUpdate.Visible)
            {
                string CommandText = "SELECT   * FROM   (SELECT   * FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR,c.TRN_TYPE, (c.TRN_TYPE||'@'||c.YEAR)as combined  FROM   YRN_QC a, YRN_IR_TRN c, YRN_MST d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE  AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE  AND c.FIBER_CODE = d.FIBER_CODE  AND NVL (d.QC_REQUIRED, 0) = '1' AND NVL (a.QC_CONF_FLAG, 0) = '0' AND SUBSTR(c.TRN_TYPE,0,1)='R' AND c.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) ";
                string SearchQuery = "%" + text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", SearchQuery, "");

            }
            return dt.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int TRN_NUMB = 0;
            int.TryParse(ddlTRNNumber.SelectedText.ToString().Trim(), out TRN_NUMB);
            string data = ddlTRNNumber.SelectedValue.Trim();
            string[] arr = data.Split('@');
            if (arr.Length > 0)
            {
                int YEAR = 0;
                string TRN_TYPE = arr[0].ToString();
                int.TryParse(arr[1].ToString(), out YEAR);
                hdTRN_NUMB.Value = TRN_NUMB.ToString();
                hdTRN_TYPE.Value = TRN_TYPE;
                hdYEAR.Value = YEAR.ToString();
                Getdata();

            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn QC Standard  selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private void Getdata()
    {
        int YEAR = 0, TRN_NUMB = 0;
        string TRN_TYPE = hdTRN_TYPE.Value;
        int.TryParse(hdYEAR.Value, out YEAR);
        int.TryParse(hdTRN_NUMB.Value, out TRN_NUMB);
        string FIBER_CODE = string.Empty;
        if (ddlyarncode.SelectedIndex > -1)
        {
            FIBER_CODE = ddlyarncode.SelectedText.Trim();
        }

        if (tdSave.Visible)
        {
            dtDetailTBLQC = SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.GetDataByTRN_NUMBfor_QC(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_NUMB, YEAR, oUserLoginDetail.DT_STARTDATE.Year, FIBER_CODE);
            ViewState["dtDetailTBLQC"] = dtDetailTBLQC;
            BindGridFromDataTable();
        }
        else if (tdUpdate.Visible)
        {
            dtDetailTBLQC = SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.GetQCDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_NUMB, oUserLoginDetail.DT_STARTDATE.Year, YEAR, FIBER_CODE);
            ViewState["dtDetailTBLQC"] = dtDetailTBLQC;
            BindGridFromDataTable();
        }
    }

    protected void lblQC_VALUE_TextChanged(object sender, EventArgs e)
    {

        double QC_Value = 0, Max_Value = 0, Min_Value = 0;
        GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox lblQC_VALUE = (TextBox)currentRow.FindControl("lblQC_VALUE");
        Label lblMaxValue = (Label)currentRow.FindControl("lblMaxValue");
        Label lblMinValue = (Label)currentRow.FindControl("lblMinValue");
        Image imgSuccess = (Image)currentRow.FindControl("imgSuccess");
        Image imgFail = (Image)currentRow.FindControl("imgFail");



        double.TryParse(lblMaxValue.Text, out Max_Value);
        double.TryParse(lblMinValue.Text, out Min_Value);
        double.TryParse(lblQC_VALUE.Text, out QC_Value);
        if (!string.IsNullOrEmpty(lblQC_VALUE.Text))
        {
            if (QC_Value <= Max_Value && QC_Value >= Min_Value)
            {
                imgSuccess.Visible = true;
                imgFail.Visible = false;
            }
            else
            {
                imgSuccess.Visible = false;
                imgFail.Visible = true;
            }

        }
        else
        {
            imgSuccess.Visible = false;
            imgFail.Visible = false;
        }

    }

    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset);
            ddlyarncode.Items.Clear();
            ddlyarncode.DataSource = data;
            ddlyarncode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn Code selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }


    private DataTable GetYarnData(string Text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   d.FIBER_CODE, d.FIBER_DESC  FROM   TX_FABRIC_NEW_MASTER d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE) WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND FIBER_CODE NOT IN ( select FIBER_CODE from ( SELECT   FIBER_CODE FROM   (SELECT   d.FIBER_CODE, d.FIBER_DESC  FROM   TX_FABRIC_NEW_MASTER d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE)    WHERE  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by FIBER_CODE";
            string SearchQuery = "%" + Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");


        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text)
    {

        string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   d.FIBER_CODE, d.FIBER_DESC  FROM  TX_FIBER_NEW_MASTER d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE)";
        string SearchQuery = "%" + text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", SearchQuery, "").Rows.Count;

    }


    protected void ddlyarncode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Getdata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

        }

    }

    protected void chkEdit_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = ((GridViewRow)((CheckBox)sender).NamingContainer);
        TextBox txtQCValue = (TextBox)currentRow.FindControl("lblQC_VALUE");
        TextBox txtRemarks = (TextBox)currentRow.FindControl("lblREMARKS");
        CheckBox chkEdit = (CheckBox)currentRow.FindControl("chkEdit");
        if (chkEdit.Checked)
        {
            txtQCValue.ReadOnly = false;
            txtRemarks.ReadOnly = false;
        }
        else
        {
            txtQCValue.ReadOnly = true;
            txtRemarks.ReadOnly = true;
        }

    }

}

