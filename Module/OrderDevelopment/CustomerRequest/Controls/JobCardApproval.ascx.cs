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


public partial class Module_OrderDevelopment_CustomerRequest_Controls_JobCardApproval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "Update";
                BindJobCardApproval();
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
            DataTable dtJobCardApproval = new DataTable();
            dtJobCardApproval.Columns.Add("BATCH_CODE", typeof(int));
            dtJobCardApproval.Columns.Add("YEAR", typeof(int));
            dtJobCardApproval.Columns.Add("COMP_CODE", typeof(string));
            dtJobCardApproval.Columns.Add("BRANCH_CODE", typeof(string));
            dtJobCardApproval.Columns.Add("BATCH_DATE", typeof(DateTime));
            dtJobCardApproval.Columns.Add("GREY_LOT_NO", typeof(string));
            dtJobCardApproval.Columns.Add("PA_NO", typeof(string));
            dtJobCardApproval.Columns.Add("MACHINE_CODE", typeof(string));
            dtJobCardApproval.Columns.Add("MACHINE_MAKE", typeof(string));
            dtJobCardApproval.Columns.Add("SPRINGS", typeof(int));
            dtJobCardApproval.Columns.Add("LOT_SIZE", typeof(int));
            dtJobCardApproval.Columns.Add("MACHINE_CAPACITY", typeof(int));
            dtJobCardApproval.Columns.Add("CONF_FLAG", typeof(string));
            dtJobCardApproval.Columns.Add("CONF_DATE", typeof(DateTime));
            dtJobCardApproval.Columns.Add("CONF_BY", typeof(string));
            return dtJobCardApproval;
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
            DataTable dtJobCard = CreateDataTable();
            int totalRows = gvJobCardApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                int Year = 0;
                GridViewRow thisGridViewRow = gvJobCardApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblBATCH_CODE = (Label)thisGridViewRow.FindControl("lblBATCH_CODE");
                    Label lblCOMP_CODE = (Label)thisGridViewRow.FindControl("lblCOMP_CODE");
                    Label lblBRANCH_CODE = (Label)thisGridViewRow.FindControl("lblBRANCH_CODE");
                    Label lblYEAR = (Label)thisGridViewRow.FindControl("lblYEAR");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    int.TryParse(lblYEAR.Text, out Year);
                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtJobCard.NewRow();
                        dr["YEAR"] = Year;
                        dr["BATCH_CODE"] = lblBATCH_CODE.Text.Trim();
                        dr["COMP_CODE"] = lblCOMP_CODE.Text;
                        dr["BRANCH_CODE"] = lblBRANCH_CODE.Text;
                        dr["CONF_FLAG"] = 1;
                        dr["CONF_DATE"] = DateTime.Now.Date.ToShortDateString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dtJobCard.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.BATCH_CARD_MST.Update_JobCardApproval(oUserLoginDetail.UserCode, dtJobCard);
            if (iResult > 0)
            {
                lblMode.Text = "Update";
                CommonFuction.ShowMessage("Job approved Successfully.");
                BindJobCardApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Confirm.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }
    private void BindJobCardApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardApproval(oBATCH_CARD_MST);
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
                gvJobCardApproval.DataSource = dt;
                gvJobCardApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No JobSheet For Approval";
                gvJobCardApproval.DataSource = null;
                gvJobCardApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No JobSheet For Approval");
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
    protected void gvJobCardApproval_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    GridView gvJobCardTrn = (GridView)grdRow.FindControl("grdJobCardTRN");
                    gvJobCardTrn.DataSource = dtJobTRN;
                    gvJobCardTrn.DataBind();
                }

                DataTable dtJobTRNDYES = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardApprovalTRNDYES(oBATCH_CARD_MST);
                if (dtJobTRNDYES != null && dtJobTRNDYES.Rows.Count > 0)
                {
                    GridView gvJobCardDYES = (GridView)grdRow.FindControl("grdJobCardDYES");
                    gvJobCardDYES.DataSource = dtJobTRNDYES;
                    gvJobCardDYES.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }


    protected void grdJobCardData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Page" && e.CommandName != "")
        {
            GridViewRow gv1 = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblBATCH_CODE = (Label)gv1.FindControl("lblBATCH_CODE");

            if (e.CommandName == "JobCardDetails")
            {
                
                    try
                    {
                        string QueryString = "";
                        QueryString += "?FromNo=" + lblBATCH_CODE.Text.Trim();
                        QueryString += "&ToNo=" + lblBATCH_CODE.Text.Trim();


                        string URL = "../../Reports/JobCardPrintReport.aspx" + QueryString;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                       

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }


            else if (e.CommandName == "JobCardEdit")
            {

                try
                {
                    string QueryString = "";
                    QueryString += "?JobCard=" + lblBATCH_CODE.Text.Trim();
                    string URL = "JobCardEntry.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }
    }


    protected void chkApprovedheader_CheckedChanged(Object sender, EventArgs args)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvJobCardApproval.HeaderRow.FindControl("chkApprovedheader");
        foreach (GridViewRow row in gvJobCardApproval.Rows)
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
    protected void grdJobCardTRN_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdJobCardDYES_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
