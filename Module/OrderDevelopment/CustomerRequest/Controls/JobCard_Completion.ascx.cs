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

public partial class Module_OrderDevelopment_CustomerRequest_Controls_JobCard_Completion : System.Web.UI.UserControl
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
                BindJobSheetCompletion();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }
    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtjobSheetApproval = new DataTable();
            dtjobSheetApproval.Columns.Add("BATCH_CODE", typeof(int));
            dtjobSheetApproval.Columns.Add("YEAR", typeof(int));
            dtjobSheetApproval.Columns.Add("COMP_CODE", typeof(string));
            dtjobSheetApproval.Columns.Add("BRANCH_CODE", typeof(string));
            dtjobSheetApproval.Columns.Add("BATCH_DATE", typeof(DateTime));
            dtjobSheetApproval.Columns.Add("LOT_NUMBER", typeof(string));
            dtjobSheetApproval.Columns.Add("PA_NO", typeof(string));
            dtjobSheetApproval.Columns.Add("MACHINE_CODE", typeof(string));
            dtjobSheetApproval.Columns.Add("MACHINE_MAKE", typeof(string));
            dtjobSheetApproval.Columns.Add("SPRINGS", typeof(int));
            dtjobSheetApproval.Columns.Add("LOT_SIZE", typeof(int));
            dtjobSheetApproval.Columns.Add("MACHINE_CAPACITY", typeof(int));
            dtjobSheetApproval.Columns.Add("COMP_FLAG", typeof(string));
            dtjobSheetApproval.Columns.Add("COMP_DATE", typeof(DateTime));
            dtjobSheetApproval.Columns.Add("COMP_BY", typeof(string));
            dtjobSheetApproval.Columns.Add("COMP_REM", typeof(string));
            return dtjobSheetApproval;
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            DataTable dtJobSheet = CreateDataTable();
            int totalRows = gvJobSheetApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                int Year = 0;
                GridViewRow thisGridViewRow = gvJobSheetApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblBATCH_CODE = (Label)thisGridViewRow.FindControl("lblBATCH_CODE");
                    Label lblCOMP_CODE = (Label)thisGridViewRow.FindControl("lblCOMP_CODE");
                    Label lblBRANCH_CODE = (Label)thisGridViewRow.FindControl("lblBRANCH_CODE");
                    Label lblYEAR = (Label)thisGridViewRow.FindControl("lblYEAR");
                    TextBox txtComptRem = (TextBox)thisGridViewRow.FindControl("txtComptRem");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    int.TryParse(lblYEAR.Text, out Year);
                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtJobSheet.NewRow();
                        dr["YEAR"] = Year;
                        dr["BATCH_CODE"] = lblBATCH_CODE.Text.Trim();
                        dr["COMP_CODE"] = lblCOMP_CODE.Text;
                        dr["BRANCH_CODE"] = lblBRANCH_CODE.Text;
                        dr["COMP_FLAG"] = 1;
                        dr["COMP_DATE"] = DateTime.Now.Date.ToShortDateString();
                        dr["COMP_BY"] = oUserLoginDetail.UserCode;
                        dr["COMP_REM"] = txtComptRem.Text;
                        dtJobSheet.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.BATCH_CARD_MST.update_Jobcompletion(oUserLoginDetail.UserCode, dtJobSheet);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                CommonFuction.ShowMessage(" Job Card Completion Approved Successfully.");
                BindJobSheetCompletion();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Confirm.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }
    private void BindJobSheetCompletion()
    {
        try
        {
            SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobSheetCompletion(oBATCH_CARD_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("COMP_DATE"))
                    dt.Columns.Add("COMP_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("COMP_BY"))
                    dt.Columns.Add("COMP_BY", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["COMP_BY"].ToString();
                    if (ConfBy == "")
                        dr["COMP_BY"] = oUserLoginDetail.Username;
                    dr["COMP_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvJobSheetApproval.DataSource = dt;
                gvJobSheetApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Job Card Completion For Approval";
                gvJobSheetApproval.DataSource = null;
                gvJobSheetApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Job Card Completion For Approval");

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "PrintJobSheetCompletion.aspx";
            Response.Redirect(URL);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing Batch Card.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page Leaving.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

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
    protected void gvJobSheetApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;
                Label lblBATCH_CODE = (Label)grdRow.FindControl("lblBATCH_CODE");

                SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
                oBATCH_CARD_MST.BATCH_CODE = int.Parse(lblBATCH_CODE.Text.Trim());
                oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                DataTable dtJobTRN = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardApprovalTRN(oBATCH_CARD_MST);
                if (dtJobTRN != null && dtJobTRN.Rows.Count > 0)
                {
                    GridView gvJobSheetTrn = (GridView)grdRow.FindControl("grdJobSheetTRN");
                    gvJobSheetTrn.DataSource = dtJobTRN;
                    gvJobSheetTrn.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }
    protected void chkApprovedheader_CheckedChanged(Object sender, EventArgs args)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvJobSheetApproval.HeaderRow.FindControl("chkApprovedheader");
        foreach (GridViewRow row in gvJobSheetApproval.Rows)
        {
            CheckBox chkApproved = (CheckBox)row.FindControl("chkApproved");
            if (ChkBoxHeader.Checked == true)
            {
                chkApproved.Checked = true;
            }
            else
            {
                chkApproved.Checked = false;
            }
        }
    }
    protected void grdJobSheetTRN_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
