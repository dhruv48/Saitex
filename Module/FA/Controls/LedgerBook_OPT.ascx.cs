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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_LedgerBook_OPT : System.Web.UI.UserControl
{
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static string SearchTable = string.Empty;
    private static string SearchDate = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    /// <summary>
    /// Use of Intialization.
    /// </summary>
    private void InitialisePage()
    {
        try
        {
            StartDate = oUserLoginDetail.DT_STARTDATE;
            EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            txtStartDT.Text = StartDate.ToShortDateString();
            txtEndDT.Text = EndDate.ToShortDateString();

            bindLedgerCombo();
        }
        catch
        {
            throw;
        }
    }

    private void bindLedgerCombo()
    {
        try
        {
            cmbLedger.Items.Clear();

            DataTable dtLedger = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMaster();
            if (dtLedger != null && dtLedger.Rows.Count > 0)
            {
                cmbLedger.DataSource = dtLedger;
                cmbLedger.DataBind();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Sorry ! No Ledger found..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void cmbLedger_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindLedgerCombo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Ledgers.\r\nSee error log for detail."));
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStartDT.Text != "")
            {
                if (txtEndDT.Text != "")
                {
                    string URL = "../Reports/LedgerBookReport.aspx";
                    URL = URL + "?StartDate=" + this.txtStartDT.Text.Trim();
                    URL = URL + "&EndDate=" + this.txtEndDT.Text.Trim();
                    URL = URL + "&LedgerCode=" + this.cmbLedger.SelectedValue.Trim();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please enter Ending Date..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please enter Starting Date..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Print Button.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./LedgerBook_OPT.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help..\r\nSee error log for detail."));
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}