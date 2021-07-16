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
using Obout.ComboBox;
using Obout.Interface;


public partial class Module_Yarn_SalesWork_Controls_Yarn_Stock_Statement : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
                BindControls();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControls()
    {
        try
        {
            DDLFinancialYear.SelectedIndex = -1;
            TxtStartDate.Text = "";
            TxtEndDate.Text = "";
            ddlcatgory.SelectedIndex = -1;
            txtItemCode.SelectedIndex = -1;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void BindFinancialYear()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = SaitexBL.Interface.Method.ItemMaster.Bind_Financial_Year(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE.Trim().ToString(), string.Empty);
            if (DT != null && DT.Rows.Count > 0)
            {
                DDLFinancialYear.Items.Clear();
                DDLFinancialYear.DataSource = DT;
                DDLFinancialYear.DataValueField = "FIN_YEAR_CODE";
                DDLFinancialYear.DataTextField = "FIN_DESC";
                DDLFinancialYear.DataBind();
                DDLFinancialYear.Items.Insert(0, new ListItem("---------Select---------", ""));
                DT.Dispose();
                DT = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindYarnCatagory(string MST_NAME)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlcatgory.Items.Clear();
                ddlcatgory.DataSource = dt;
                ddlcatgory.DataValueField = "MST_CODE";
                ddlcatgory.DataTextField = "MST_DESC";
                ddlcatgory.DataBind();
                ddlcatgory.Items.Insert(0, new ListItem("Select"));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void BindControls()
    {
        try
        {
            BindFinancialYear();
            BindYarnCatagory("YARN_CAT");
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItemsCount(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtItemCode.Items.Clear();

                txtItemCode.DataSource = data;
                txtItemCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));

        }
    }

    protected DataTable GetItemsCount(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND YARN_code NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY YARN_CODE";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd WHERE YARN_TYPE <> 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY YARN_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void txtItemCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {

    }

    private DataTable GetData()
    {
        DateTime StartDate;
        DateTime Enddate;
        string YARN_CODE = string.Empty;
        string YARN_CAT = string.Empty;
        try
        {
            if (txtItemCode.SelectedValue.ToString() != null && txtItemCode.SelectedValue.ToString() != string.Empty)
            {
                YARN_CODE = txtItemCode.SelectedValue.ToString();
            }
            else
            {
                YARN_CODE = string.Empty;
            }

            if (ddlcatgory.SelectedValue.ToString() != null && ddlcatgory.SelectedValue.ToString() != "Select")
            {
                YARN_CAT = ddlcatgory.SelectedValue.ToString();
            }
            else
            {
                YARN_CAT = string.Empty;
            }

            if (TxtStartDate.Text.ToString() != null && TxtStartDate.Text.ToString() != string.Empty)
            {
                StartDate = DateTime.Parse(TxtStartDate.Text);
            }
            else
            {
                StartDate = DateTime.Now.Date;
            }

            if (TxtEndDate.Text.ToString() != null && TxtEndDate.Text.ToString() != string.Empty)
            {
                Enddate = DateTime.Parse(TxtEndDate.Text);
            }
            else
            {
                Enddate = DateTime.Now.Date;
            }

            DataTable dt = SaitexBL.Interface.Method.YRN_MST.load_yarnstock_Detail(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, YARN_CODE, YARN_CAT, StartDate, Enddate);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdyrnstock.DataSource = dt;
                grdyrnstock.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                grdyrnstock.DataSource = null;
                grdyrnstock.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                grdyrnstock.Visible = true;
            }

            return dt;

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            GetData();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControls();

        }
        catch (Exception)
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
}
