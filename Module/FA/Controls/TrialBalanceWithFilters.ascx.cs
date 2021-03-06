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

public partial class Module_FA_Controls_TrialBalanceWithFilters : System.Web.UI.UserControl
{
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./TrialBalanceWithFilter.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/TrialBalanceFilter.aspx";
            URL = URL + "?StartDate=" + txtFromDT.Text.Trim();
            URL = URL + "&EndDate=" + txtToDT.Text.Trim();

            if (cmbLedgerCodeStart.SelectedIndex != -1 && cmbLedgerCodeEnd.SelectedIndex != -1)
            {
                URL = URL + "&LedgerCodeStart=" + cmbLedgerCodeStart.SelectedValue.ToString().Trim();
                URL = URL + "&LedgerCodeEnd=" + cmbLedgerCodeEnd.SelectedValue.ToString().Trim();
            }

            if (rdoLstVoucherType.SelectedValue != "22")
            {
                URL = URL + "&Vou_No=" + rdoLstVoucherType.SelectedValue.ToString().Trim();
            }

            if (rdoLstDate.SelectedValue == "Bill Date Wise")
            {
                URL = URL + "&IsDoc=1";
            }
            else
            {
                URL = URL + "&IsDoc=0";
            }

            if (rdoLstOrder.SelectedValue == "Code Wise")
            {
                URL = URL + "&Order=CODE";
            }
            else
            {
                URL = URL + "&Order=NAME";
            }

            if (rdbLstOpening.SelectedValue == "With Opening")
            {
                URL = URL + "&ReportType=1";
            }
            else if (rdbLstOpening.SelectedValue == "Without Opening")
            {
                URL = URL + "&ReportType=2";
            }
            else
            {
                URL = URL + "&ReportType=3";
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Print Mode";

            txtFromDT.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txtToDT.Text = Common.CommonFuction.GetYearEndDate(Convert.ToDateTime(txtFromDT.Text)).ToString();
            trType.Visible = false;
            BindVoucherType();
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY CAST(LDGR_CODE AS INT) ASC";
            string commandText = "SELECT LDGR_CODE, LDGR_NAME, (LDGR_CODE || ' ' || LDGR_NAME) AS LGR_DATA FROM FA_LGR_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private void BindVoucherType()
    {
        try
        {
            rdoLstVoucherType.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                rdoLstVoucherType.DataSource = dt;
                rdoLstVoucherType.DataValueField = "VCHR_CODE";
                rdoLstVoucherType.DataTextField = "VCHR_NAME";
                rdoLstVoucherType.DataBind();
            }
            rdoLstVoucherType.SelectedValue = "22";
        }
        catch
        {
            throw;
        }
    }

    protected void cmbLedgerCodeStart_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox

            if (data != null && data.Rows.Count > 0)
            {
                cmbLedgerCodeStart.Items.Clear();
                cmbLedgerCodeStart.DataSource = data;
                cmbLedgerCodeStart.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbLedgerCodeStart_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbLedgerCodeStart.SelectedIndex != -1 && cmbLedgerCodeEnd.SelectedIndex != -1)
            {
                int iLedgerStart = Convert.ToInt32(cmbLedgerCodeStart.SelectedValue.ToString());
                int iLedgerEnd = Convert.ToInt32(cmbLedgerCodeEnd.SelectedValue.ToString());
                if (iLedgerStart < iLedgerEnd)
                {
                    Common.CommonFuction.ShowMessage("Start Ledger Code should not be greater than End Ledger Code..");
                    cmbLedgerCodeEnd.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT LDGR_CODE, LDGR_NAME, GRP_NAME FROM V_FA_LGR_MST WHERE LDGR_CODE LIKE :SearchQuery OR LDGR_NAME LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery ORDER BY CAST(LDGR_CODE AS INT)) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND LDGR_CODE NOT IN (SELECT LDGR_CODE FROM (SELECT LDGR_CODE, LDGR_NAME, GRP_NAME FROM V_FA_LGR_MST WHERE LDGR_CODE LIKE :SearchQuery OR LDGR_NAME LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery ORDER BY CAST(LDGR_CODE AS INT)) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY CAST(LDGR_CODE AS INT)";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT LDGR_CODE, LDGR_NAME, GRP_NAME FROM V_FA_LGR_MST WHERE LDGR_CODE LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery OR LDGR_NAME LIKE :SearchQuery ORDER BY CAST(LDGR_CODE AS INT)) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY CAST(LDGR_CODE AS INT) ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbLedgerCodeEnd_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox

            if (data != null && data.Rows.Count > 0)
            {
                cmbLedgerCodeEnd.Items.Clear();
                cmbLedgerCodeEnd.DataSource = data;
                cmbLedgerCodeEnd.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbLedgerCodeEnd_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbLedgerCodeStart.SelectedIndex != -1 && cmbLedgerCodeEnd.SelectedIndex != -1)
            {
                int iLedgerStart = Convert.ToInt32(cmbLedgerCodeEnd.SelectedValue.ToString());
                int iLedgerEnd = Convert.ToInt32(cmbLedgerCodeStart.SelectedValue.ToString());
                if (iLedgerStart < iLedgerEnd)
                {
                    Common.CommonFuction.ShowMessage("Start Ledger Code should not be greater than End Ledger Code..");
                    cmbLedgerCodeEnd.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}