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

public partial class Module_FA_Reports_GroupMstOPT : System.Web.UI.Page
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

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Print Mode";
        }
        catch
        {
            throw;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string URL = "../Reports/GroupMstReport.aspx";

            if (cmbGroupCodeStart.SelectedIndex != -1 && cmbGroupCodeEnd.SelectedIndex != -1)
            {
                URL = URL + "?IsGroup=0";
                URL = URL + "&GroupCodeStart=" + cmbGroupCodeStart.SelectedValue.ToString().Trim();
                URL = URL + "&GroupCodeEnd=" + cmbGroupCodeEnd.SelectedValue.ToString().Trim();
            }
            else
            {
                URL = URL + "?IsGroup=1";
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbGroupCodeStart_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox

            if (data != null && data.Rows.Count > 0)
            {
                cmbGroupCodeStart.Items.Clear();
                cmbGroupCodeStart.DataSource = data;
                cmbGroupCodeStart.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Group loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbGroupCodeStart_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbGroupCodeStart.SelectedIndex != -1 && cmbGroupCodeEnd.SelectedIndex != -1)
            {
                int iGroupStart = Convert.ToInt32(cmbGroupCodeStart.SelectedValue.ToString());
                int iGroupEnd = Convert.ToInt32(cmbGroupCodeEnd.SelectedValue.ToString());
                if (iGroupStart < iGroupEnd)
                {
                    Common.CommonFuction.ShowMessage("Start Ledger Code should not be greater than End Ledger Code..");
                    cmbGroupCodeEnd.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Group Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbGroupCodeEnd_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox

            if (data != null && data.Rows.Count > 0)
            {
                cmbGroupCodeEnd.Items.Clear();
                cmbGroupCodeEnd.DataSource = data;
                cmbGroupCodeEnd.DataBind();
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

    protected void cmbGroupCodeEnd_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbGroupCodeStart.SelectedIndex != -1 && cmbGroupCodeEnd.SelectedIndex != -1)
            {
                int iGroupStart = Convert.ToInt32(cmbGroupCodeEnd.SelectedValue.ToString());
                int iGroupEnd = Convert.ToInt32(cmbGroupCodeStart.SelectedValue.ToString());
                if (iGroupStart < iGroupEnd)
                {
                    Common.CommonFuction.ShowMessage("Start Ledger Code should not be greater than End Ledger Code..");
                    cmbGroupCodeEnd.SelectedIndex = -1;
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
            string CommandText = " SELECT * FROM (SELECT GRP_CODE, GRP_NAME FROM FA_GRP_MST WHERE GRP_CODE LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery ORDER BY CAST(GRP_CODE AS INT)) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND GRP_CODE NOT IN (SELECT GRP_CODE FROM (SELECT GRP_CODE, GRP_NAME FROM FA_GRP_MST WHERE GRP_CODE LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery ORDER BY CAST(GRP_CODE AS INT)) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY CAST(GRP_CODE AS INT)";
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
            string CommandText = " SELECT * FROM (SELECT GRP_CODE, GRP_NAME FROM FA_GRP_MST WHERE GRP_CODE LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery ORDER BY CAST(GRP_CODE AS INT)) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY CAST(GRP_CODE AS INT) ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Reports/GroupMstOPT.aspx", false);
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }
}