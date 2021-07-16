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
public partial class Module_Inventory_Controls_MaterailStockQueryForm : System.Web.UI.UserControl
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                Initial_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }
    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text);
            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "ITEM_CODE";
            txtICODE.DataValueField = "ITEM_CODE";
            txtICODE.DataBind();
            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }
    protected DataTable GetPOData(string text)
    {
        try
        {
            DataTable dt = new DataTable();

            string whereClause = " WHERE ITEM_CODE like :SearchQuery or ITEM_TYPE like :SearchQuery or ITEM_DESC like :SearchQuery";
            string sortExpression = " ORDER BY ITEM_CODE";
            string commandText = "SELECT * FROM TX_ITEM_MST ";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text.ToUpper() + "%", "");
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Initial_Control()
    {
        try
        {
            TxtFromDate.Text = System.DateTime.Now.Date.ToShortDateString();
            TxtToDate.Text = System.DateTime.Now.Date.ToShortDateString();
            Bind_Financial_Year();
            Bind_Financial_Year_Date(oUserLoginDetail.OPEN_YEAR);
            Bind_Item_Make(DDLAssociate, "ASOC_ITEM_CODE", "TX_ITEM_MST");
            Bind_Item_Make(DDLItemMake, "ITEM_MAKE", "TX_ITEM_MST");
            Bind_Item_Make(DDLItemRac, "RACK_CODE", "TX_ITEM_MST");
            DDLFinancialYear.SelectedIndex = 1;
            GridItemStock();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_Item_Make(DropDownList DDL, string Search_Keyword, string TableName)
    {
        try
        {
            DataTable DT = new DataTable();
            DT.Rows.Clear();
            DT = SaitexBL.Interface.Method.ItemMaster.Load_Drop_Down_Control(Search_Keyword.ToUpper().ToString(), TableName, oUserLoginDetail.CH_BRANCHCODE.Trim().ToString(), oUserLoginDetail.COMP_CODE.Trim().ToString());
            DDL.DataSource = DT;
            DDL.DataTextField = Search_Keyword;
            DDL.DataValueField = Search_Keyword;
            DDL.DataBind();
            DDL.Items.Insert(0, "-------Select-------");

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_Financial_Year()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = SaitexBL.Interface.Method.ItemMaster.Bind_Financial_Year(oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.Trim().ToString(), string.Empty);
            DDLFinancialYear.DataSource = DT;
            DDLFinancialYear.DataValueField = "FIN_YEAR_CODE";
            DDLFinancialYear.DataTextField = "FIN_DESC";
            DDLFinancialYear.DataBind();
            DDLFinancialYear.Items.Insert(0, new ListItem("---------Select---------", ""));
            DT.Dispose();
            DT = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string SearchQuery = string.Empty;
        DateTime StDate;
        DateTime EnDate;
        string Item_Code = string.Empty;
        string Cat_Code = string.Empty;
        string Item_Rac = string.Empty;
        string Associate = string.Empty;
        string Item_Make = string.Empty;
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
            }
            else
            {
                StDate = Sdate;
                EnDate = Edate;
            }
            if (txtICODE.SelectedIndex > 0)
            {
                Item_Code = txtICODE.SelectedValue.Trim().ToString();
            }
            if (ddlItemCategory.SelectedIndex > 0)
            {
                Cat_Code = ddlItemCategory.SelectedValue.Trim().ToString();
            }
            if (DDLItemMake.SelectedIndex != 0)
            {
                Item_Make = DDLItemMake.SelectedValue.Trim().ToString();
            }
            if (DDLItemRac.SelectedIndex != 0)
            {
                Item_Rac = DDLItemRac.SelectedValue.Trim().ToString();
            }
            if (DDLAssociate.SelectedIndex != 0)
            {
                Associate = DDLAssociate.SelectedValue.Trim().ToString();
            }
            Response.Redirect("../Reports/Material_StockStatement.aspx?FromDate=" + StDate + "&ToDate=" + EnDate + "&ITEM_CODE=" + Item_Code + "&CAT_CODE=" + Cat_Code + "&RAC_CODE=" + Cat_Code + "&ITEM_MAKE=" + Cat_Code + "&ASSOC=" + Cat_Code, false);

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
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
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void ddlItemCategory_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindItemCategory(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Item Category.\r\nSee error log for detail."));
        }
    }
    private void bindItemCategory(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "ITEM_CATG");
            ddlItemCategory.Items.Clear();
            ddlItemCategory.DataSource = dt;
            ddlItemCategory.DataTextField = "MST_CODE";
            ddlItemCategory.DataValueField = "MST_CODE";
            ddlItemCategory.DataBind();
        }
        catch
        {
            throw;
        }
    }
    private DataTable GET_MOM_DATA(string Text, string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select MST_CODE from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and MST_CODE like :SearchQuery";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, MST_NAME, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch
        {
            throw;
        }
    }
    private void Bind_Financial_Year_Date(string Year)
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.ItemMaster.Bind_Financial_Year(oUserLoginDetail.COMP_CODE.Trim().ToString(), oUserLoginDetail.CH_BRANCHCODE.Trim().ToString(), Year);
            if (DT.Rows.Count > 0)
            {
                Sdate = DateTime.Parse(DT.Rows[0]["START_DATE"].ToString());
                Edate = DateTime.Parse(DT.Rows[0]["END_DATE"].ToString());
            }
            else
            {
                Sdate = oUserLoginDetail.DT_STARTDATE;
                Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void DDLFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLFinancialYear.SelectedIndex > 0)
            {
                Bind_Financial_Year_Date(DDLFinancialYear.SelectedValue.Trim().ToString());
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                DateTime EndDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                if (StartDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = Sdate.ToShortDateString().ToString();
                }
                if (EndDate > Edate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtToDate.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                if (StartDate < Sdate || StartDate > Edate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = Sdate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void GridItemStock()
    {
        try
        {
            string SearchQuery = string.Empty;
            DateTime StDate;
            DateTime EnDate;
            string Item_Code = string.Empty;
            string Cat_Code = string.Empty;
            string Item_Rac = string.Empty;
            string Associate = string.Empty;
            string Item_Make = string.Empty;
            try
            {
                if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
                {
                    StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                    EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                }
                else
                {
                    StDate = Sdate;
                    EnDate = Edate;
                }
                if (txtICODE.SelectedIndex > 0)
                {
                    Item_Code = txtICODE.SelectedValue.Trim().ToString();
                }
                if (ddlItemCategory.SelectedIndex > 0)
                {
                    Cat_Code = ddlItemCategory.SelectedValue.Trim().ToString();
                }
                if (DDLItemMake.SelectedIndex != 0)
                {
                    Item_Make = DDLItemMake.SelectedValue.Trim().ToString();
                }
                if (DDLItemRac.SelectedIndex != 0)
                {
                    Item_Rac = DDLItemRac.SelectedValue.Trim().ToString();
                }
                if (DDLAssociate.SelectedIndex != 0)
                {
                    Associate = DDLAssociate.SelectedValue.Trim().ToString();
                }

                DataTable DT = SaitexBL.Interface.Method.ItemMaster.Load_Material_Reports(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, StDate, EnDate, Item_Code, Cat_Code, Item_Rac, Item_Make, Associate,"","");
            
            if (DT != null && DT.Rows.Count > 0)
            {
                
                GridItemStocks.DataSource = DT;
                GridItemStocks.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
            }
            else
            {
                GridItemStocks.DataSource = null;
                GridItemStocks.DataBind();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
            }
        }
        catch
        {
            throw;
        }

            }
            catch (Exception ex)
            {
                errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
                throw ex;
            }
        }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            GridItemStock();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }

    }
}



