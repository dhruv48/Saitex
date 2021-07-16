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

using Common;
using errorLog;

public partial class Module_FA_Controls_OnLinePVGenerate : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_BILL_MST oTX_BILL_MST;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Bill Received Successfully Done!');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }
                Session["saveStatus"] = 0;
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
            lblMode.Text = "Update";
            BindBillDTLGrid();
        }
        catch
        {
            throw;
        }
    }

    private void BindBillDTLGrid()
    {
        try
        {
            oTX_BILL_MST = new SaitexDM.Common.DataModel.TX_BILL_MST();

            oTX_BILL_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetBillForPVGenerate(oTX_BILL_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdBillGenerate.DataSource = dt;
                grdBillGenerate.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "Sorry Dear ! There is no Bill Received for Vouching..";
                grdBillGenerate.DataSource = null;
                grdBillGenerate.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("Sorry Dear ! There is no Bill Received for Vouching..");
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
            Response.Redirect("/Saitex/Module/FA/Pages/OnLinePVGeneration.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Bill Receiving..\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void grdBillGenerate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdBillGenerate.PageIndex = e.NewPageIndex;
            BindBillDTLGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
        }
    }

    protected void grdBillGenerate_RowCommand(object sender, GridViewCommandEventArgs e)
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

                string URL = "PurchaseVoucher.aspx";
                URL = URL + "?LedgerCode=" + strLedgerCode;
                URL = URL + "&BillAmt=" + strBillAmt;
                URL = URL + "&Branch=" + strBranch;
                URL = URL + "&BillYear=" + strBillYear;
                URL = URL + "&BillType=" + strBillType;
                URL = URL + "&BillNumb=" + strBillNumb;
                URL = URL + "&BillDate=" + strBillDate;

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=795,height=555');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Row Command Event..\r\nSee error log for detail."));
        }
    }
}