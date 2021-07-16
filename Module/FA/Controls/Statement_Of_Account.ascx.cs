using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Obout.Grid;
using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_Statement_Of_Account : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string ForeignKey;
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static double DebitTotal;
    private static double CreditTotal;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();
                if (Request.QueryString["START_DATE"] != null && Request.QueryString["START_DATE"].ToString() != "")
                {
                    DateTime.TryParse(Request.QueryString["GROUP_CODE"].ToString(), out StartDate);
                }
                if (Request.QueryString["END_DATE"] != null && Request.QueryString["END_DATE"].ToString() != "")
                {
                    DateTime.TryParse(Request.QueryString["GROUP_CODE"].ToString(), out EndDate);
                }
                if (Request.QueryString["GROUP_CODE"] != null && Request.QueryString["GROUP_CODE"].ToString() != "")
                {
                    string GROUP_CODE = Request.QueryString["GROUP_CODE"].ToString();
                    bindGroup();
                    ddlGroupCode.SelectedValue = GROUP_CODE;
                    GetSOAByGroup(GROUP_CODE);
                    ForeignKey = "G" + GROUP_CODE;
                }
                else
                {
                    Get_StatementOfAccount();  // For Grid
                }
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
            ForeignKey = "G";
            // To Check validation for Financial Year.
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }
            rvStartDate.MinimumValue = StartDate.ToShortDateString();
            rvStartDate.MaximumValue = EndDate.ToShortDateString();
            rvEndDate.MinimumValue = StartDate.ToShortDateString();
            rvEndDate.MaximumValue = EndDate.ToShortDateString();
            txtStartingDate.Text = StartDate.ToShortDateString();
            txtEndingDate.Text = EndDate.ToShortDateString();
            ShowLedger.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void Get_StatementOfAccount()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt = SaitexBL.Interface.Method.FA_Statement_Of_Account.get_Statement_Of_Account(oUserLoginDetail, StartDate, EndDate);
            ViewState["dtTree"] = dt;
            ShowLedger.Visible = true;
            lblGroupName.Text = "Statement of Account for All Groups";
        }
        catch
        {
            throw;
        }
    }

    protected void ddlGroupCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlGroupCode.Items.Clear();
            ddlGroupCode.DataSource = null;
            ddlGroupCode.DataBind();

            DataTable data = new DataTable();
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);

            ddlGroupCode.DataSource = data;
            ddlGroupCode.DataTextField = "GRP_NAME";
            ddlGroupCode.DataValueField = "GRP_CODE";
            ddlGroupCode.DataBind();

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Group Code.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE GRP_CODE like :SearchQuery or GRP_NAME like :SearchQuery or PARENT_NAME like :SearchQuery";
            string sortExpression = " ORDER BY GRP_CODE";
            string commandText = "SELECT * FROM V_FA_GRP_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
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
            string CommandText = "SELECT COUNT(*) FROM V_FA_GRP_MST WHERE GRP_CODE like :SearchQuery or GRP_NAME like :SearchQuery or PARENT_NAME like :SearchQuery";
            return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        catch
        {
            throw;
        }
    }

    protected void btnGetLedger_Click(object sender, EventArgs e)
    {
        try
        {
            GetSOAByGroup(ddlGroupCode.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GetLedger Button..\r\nSee error log for detail."));
        }
    }

    private void GetSOAByGroup(string Group_COde)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt_Group_MST = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();
            DataTable dt_LGR_MST = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMaster();

            DataTable dtSOA = SaitexBL.Interface.Method.FA_Statement_Of_Account.Get_Table_For_Statement_Of_Account();
            bool Is_Debit = false;

            double GRP_DR_OP_AMOUNT = 0;
            double GRP_CR_OP_AMOUNT = 0;
            double GRP_DR_TOTAL = 0;
            double GRP_CR_TOTAL = 0;

            SaitexBL.Interface.Method.FA_Statement_Of_Account.Get_SOA(oUserLoginDetail, StartDate, EndDate, ref dtSOA, dt_Group_MST, dt_LGR_MST, Group_COde, ref Is_Debit, ref GRP_DR_OP_AMOUNT, ref GRP_CR_OP_AMOUNT, ref GRP_DR_TOTAL, ref GRP_CR_TOTAL);
            ViewState["dtTree"] = dtSOA;

            ShowLedger.Visible = true;
            lblGroupName.Text = "Statement of Account for " + ddlGroupCode.SelectedText.Trim();

            //BindGrid();

            //lblGroupName.Text = "Statement of Account for " + ddlGroupCode.SelectedText.Trim();
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
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

    protected int level = 0;

    protected void grdSOA_DataSourceNeeded(object sender, GridDataSourceNeededEventArgs e)
    {
        try
        {
            e.HandledFiltering = false;
            e.HandledPaging = false;
            e.HandledSorting = false;

            Grid grid = (Grid)sender;

            if (!(grid is DetailGrid))
                FillGrid(grid, ForeignKey);
            else
                FillGrid(grid, e.ForeignKeysValues["ACCOUNT_ID"]);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid DataSourceNeeded Event..\r\nSee error log for detail."));
        }
    }

    private void FillGrid(Grid grid, string ForeignKeyValue)
    {
        try
        {
            level++;
            AddDetailGrid(grid);
            PopulateGrid(grid, ForeignKeyValue);
        }
        catch
        {
            throw;
        }
    }

    protected void AddDetailGrid(Grid grid)
    {
        try
        {
            DetailGrid detail = new DetailGrid();
            detail.ID = "grid" + level.ToString();
            detail.AutoGenerateColumns = false;
            detail.AllowAddingRecords = false;
            detail.Serialize = false;
            detail.AllowPageSizeSelection = false;
            detail.AllowPaging = false;
            detail.PageSize = -1;
            detail.Width = Unit.Percentage(97);
            detail.AutoPostBackOnSelect = true;
            detail.ForeignKeys = "ACCOUNT_ID";

            detail.ClientSideEvents.ExposeSender = true;
            detail.ClientSideEvents.OnClientPopulateControls = "onPopulateControls";

            foreach (Column column in grid.Columns)
            {
                Column newColumn = column.Clone() as Column;
                newColumn.SortOrder = SortOrderType.None;
                newColumn.ShowHeader = false;
                newColumn.Wrap = true;
                detail.Columns.Add(newColumn);
            }

            detail.MasterDetailSettings = grid.MasterDetailSettings;

            detail.Select += grdSOA_Select;

            detail.Rebind += grdSOA_Rebind;
            //detail.DataSourceNeeded += grdSOA_DataSourceNeeded;

            grid.DetailGrids.Add(detail);
        }
        catch
        {
            throw;
        }
    }

    protected void PopulateGrid(Grid grid, string parentId)
    {
        try
        {
            DataTable dtTree = (DataTable)ViewState["dtTree"];
            DataView dvTree = new DataView(dtTree);
            dvTree.RowFilter = "PARENT_ID='" + parentId + "'";
            if (dvTree.Count > 0)
            {
                grid.DataSource = dvTree;
                grid.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindGroup()
    {
        try
        {
            ddlGroupCode.Items.Clear();
            ddlGroupCode.DataSource = null;
            ddlGroupCode.DataBind();

            DataTable data = new DataTable();
            data = GetItems("", 0, 10);

            ddlGroupCode.DataSource = data;
            ddlGroupCode.DataTextField = "GRP_NAME";
            ddlGroupCode.DataValueField = "GRP_CODE";
            ddlGroupCode.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void grdSOA_Select(object sender, GridRecordEventArgs e)
    {
        try
        {
            ArrayList arr = e.RecordsCollection;
            Hashtable ht = arr[0] as Hashtable;
            string Account_Id = ht["ACCOUNT_ID"].ToString();
            string AccountType = Account_Id.Remove(1);
            Account_Id = Account_Id.Remove(0, 1);
            string URL = string.Empty;
            string LGR_S_DT = DateTime.Parse(txtStartingDate.Text).ToShortDateString();
            string LGR_E_DT = DateTime.Parse(txtEndingDate.Text).ToShortDateString();

            if (Account_Id != "999999" && Account_Id != "999998")
            {
                if (AccountType == "L")
                    URL = "Get_Ledger_Book.aspx?LEDGER_CODE=" + Account_Id + "&LGR_S_DT=" + LGR_S_DT + "&LGR_E_DT=" + LGR_E_DT;
                else if (AccountType == "G")
                    URL = "SOA.aspx?GROUP_CODE=" + Account_Id;
                Response.Redirect(URL);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select Event..\r\nSee error log for detail."));
        }
    }

    protected void grdSOA_Rebind(object sender, EventArgs e)
    {
        try
        {
            Grid grid = (Grid)sender;

            FillGrid(grid, "G");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Rebind Event..\r\nSee error log for detail."));
        }
    }

    private void BindGrid()
    {
        try
        {
            DataTable dtSOA = null;

            DebitTotal = 0;
            CreditTotal = 0;

            if (ViewState["dtSOA"] != null)
                dtSOA = (DataTable)ViewState["dtSOA"];

            if (dtSOA != null && dtSOA.Rows.Count > 0)
            {
                grdSOA.DataSource = dtSOA;
                grdSOA.DataBind();

                string FilterString = "L%";
                DataView dvDebit = new DataView(dtSOA);
                dvDebit.RowFilter = "ACCOUNT_ID like '" + FilterString + "'";
                if (dvDebit.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dvDebit.Count; iLoop++)
                    {
                        double Amt = 0;
                        double.TryParse(dvDebit[iLoop]["DR_AMOUNT"].ToString(), out Amt);
                        DebitTotal += Amt;

                        Amt = 0;
                        double.TryParse(dvDebit[iLoop]["CR_AMOUNT"].ToString(), out Amt);
                        CreditTotal += Amt;
                    }
                }
                ShowLedger.Visible = true;
            }
            else
            {
                ShowLedger.Visible = false;
                Common.CommonFuction.ShowMessage("No entries found for the selected group");
            }
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    protected void grdSOA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //BindData();
            grdSOA.PageIndex = e.NewPageIndex;
            grdSOA.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}