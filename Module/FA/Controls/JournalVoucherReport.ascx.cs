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

public partial class Module_FA_Controls_JournalVoucherReport : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static string SearchTable = string.Empty;
    private static string SearchDate = string.Empty;

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

    private void InitialisePage()
    {
        try
        {
            StartDate = oUserLoginDetail.DT_STARTDATE;
            EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            txtstartdate.Text = StartDate.ToShortDateString();
            txtenddate.Text = EndDate.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlVoucherNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlVoucherNo.Items.Clear();
            DataTable dt = GetDataForVouchernO(e.Text.ToUpper());
            ddlVoucherNo.DataSource = dt;
            ddlVoucherNo.DataTextField = "VCHR_NO";
            ddlVoucherNo.DataValueField = "VCHR_NO";
            ddlVoucherNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + dt.Rows.Count;
            e.ItemsCount = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Voucher Number.\r\nSee error log for detail."));
        }
    }

    private DataTable GetDataForVouchernO(string Text)
    {
        try
        {
            string whereClause = " WHERE VCHR_NO like :SearchQuery OR VCHR_CODE LIKE :SearchQuery OR VCHR_NAME LIKE :SearchQuery ";
            string sortExpression = " ORDER BY VCHR_NO";
            string commandText = "SELECT * FROM V_FA_JOURNAL_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", Text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtstartdate.Text != "")
            {
                if (txtenddate.Text != "")
                {
                    string URL = "../Reports/JournalVoucherReportCry.aspx";
                    URL = URL + "?StartDate=" + this.txtstartdate.Text.Trim();
                    URL = URL + "&EndDate=" + this.txtenddate.Text.Trim();
                    URL = URL + "&VoucherNo=" + this.ddlVoucherNo.SelectedValue.Trim();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./JournalVoucherReport.aspx", false);
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