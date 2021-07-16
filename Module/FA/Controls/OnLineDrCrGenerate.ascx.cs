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

using SaitexDM.Common.DataModel;
using Common;

public partial class Module_FA_Controls_OnLineDrCrGenerate : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    TX_DEBIT_MST oTX_DEBIT_MST;
    private static string strType = string.Empty;
    private static string strTypeName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"] != "")
                {
                    strType = Request.QueryString["Type"].ToString().Trim();
                }
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page loading..\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Update Mode";

            if (strType == "D")
            {
                lblHeading.Text = "On Line Debit Note Vouching";
                strTypeName = "DEBIT NOTE";
            }
            else
            {
                lblHeading.Text = "On Line Credit Note Vouching";
                strTypeName = "CREDIT NOTE";
            }
            BindDrCrNoteGrid();
        }
        catch
        {
            throw;
        }
    }

    private void BindDrCrNoteGrid()
    {
        try
        {
            grdDrCrNoteGenerate.DataSource = null;
            grdDrCrNoteGenerate.DataBind();

            oTX_DEBIT_MST = new TX_DEBIT_MST();
            oTX_DEBIT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_DEBIT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_DEBIT_MST.NOTE_TYPE = strTypeName;

            DataTable dt = SaitexBL.Interface.Method.TX_DEBIT_MST.GetDrCrNoteGenerate(oTX_DEBIT_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdDrCrNoteGenerate.DataSource = dt;
                grdDrCrNoteGenerate.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                if (strType == "D")
                {
                    CommonFuction.ShowMessage("Sorry Dear ! There is no Debit Note found for Vouching..");
                }
                else
                {
                    CommonFuction.ShowMessage("Sorry Dear ! There is no Credit Note found for Vouching..");
                }

                grdDrCrNoteGenerate.DataSource = null;
                grdDrCrNoteGenerate.DataBind();
                lblTotalRecord.Text = "0";
            }
        }
        catch
        {
            throw;
        }
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (strType == "D")
            {
                Response.Redirect("./OnLineDrCrNoteGenerate.aspx?Type=D", false);
            }
            else
            {
                Response.Redirect("./OnLineDrCrNoteGenerate.aspx?Type=C", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Refreshing Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void grdDrCrNoteGenerate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDrCrNoteGenerate.PageIndex = e.NewPageIndex;
            BindDrCrNoteGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
        }
    }

    protected void grdDrCrNoteGenerate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "VOUCHING")
            {
                string strLedgerCode = string.Empty;
                string strBillAmt = string.Empty;
                string strBranch = string.Empty;
                string strBillType = string.Empty;
                string strBillNumb = string.Empty;
                string strBillYear = string.Empty;
                string strBillDate = string.Empty;

                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                Label lblPartyLedgerCode = (Label)row.FindControl("lblPartyLedgerCode");
                Label lblBillAmt = (Label)row.FindControl("lblBillAmt");
                Label lblBranchCode = (Label)row.FindControl("lblBranchCode");
                Label lblBillType = (Label)row.FindControl("lblBillType");
                Label lblBillNumb = (Label)row.FindControl("lblBillNumb");
                Label lblBillYear = (Label)row.FindControl("lblBillYear");
                Label lblBillDate = (Label)row.FindControl("lblBillDate");

                strLedgerCode = lblPartyLedgerCode.Text.Trim();
                strBillAmt = lblBillAmt.Text.Trim();
                strBranch = lblBranchCode.Text.Trim();
                strBillType = lblBillType.Text.Trim();
                strBillNumb = lblBillNumb.Text.Trim();
                strBillYear = lblBillYear.Text.Trim();
                strBillDate = lblBillDate.Text.Trim();

                string URL = "DrCrNoteVoucher.aspx";
                URL = URL + "?LedgerCode=" + strLedgerCode;
                URL = URL + "&BillAmt=" + strBillAmt;
                URL = URL + "&Branch=" + strBranch;
                URL = URL + "&BillYear=" + strBillYear;
                URL = URL + "&BillType=" + strBillType;
                URL = URL + "&BillNumb=" + strBillNumb;
                URL = URL + "&BillDate=" + strBillDate;
                URL = URL + "&strType=" + strType;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Row Command Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}