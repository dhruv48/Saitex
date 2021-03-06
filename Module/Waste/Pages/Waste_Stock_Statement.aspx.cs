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
using System.Text.RegularExpressions;
using System.Xml.Linq;

using Common;

public partial class Module_Waste_Pages_Waste_Stock_Statement : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;

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

    private void Initial_Control()
    {
        try
        {
            Bind_Financial_Year();
            Bind_Financial_Year_Date(oUserLoginDetail.OPEN_YEAR);
            DDLFinancialYear.SelectedIndex = 1;

            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = Sdate.ToShortDateString();
            TxtToDate.Text = Edate.ToShortDateString();
          
            Bind_Item_Make(DDLAssociate, "ASOC_ITEM_CODE", "TX_WASTE_MST");
            Bind_Item_Make(DDLItemMake, "ITEM_MAKE", "TX_WASTE_MST");
            Bind_Item_Make(DDLItemRac, "RACK_CODE", "TX_WASTE_MST");
           

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
            Response.Redirect("../Report/Waste_StockStatement.aspx?FromDate=" + StDate + "&ToDate=" + EnDate + "&ITEM_CODE=" + Item_Code + "&CAT_CODE=" + Cat_Code + "&RAC_CODE=" + Cat_Code + "&ITEM_MAKE=" + Cat_Code + "&ASSOC=" + Cat_Code, false);

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
            //DataTable dt = GET_MOM_DATA(SearchQuery, "ITEM_CATG");
            //ddlItemCategory.Items.Clear();
            //ddlItemCategory.DataSource = dt;
            //ddlItemCategory.DataTextField = "MST_CODE";
            //ddlItemCategory.DataValueField = "MST_CODE";
            //ddlItemCategory.DataBind();


            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_WASTE_MST.GetItemCate();
            ddlItemCategory.DataSource = dt;
            ddlItemCategory.DataValueField = "CAT_CODE";
            ddlItemCategory.DataTextField = "CAT_CODE";
            ddlItemCategory.DataBind();          
            dt.Dispose();
            dt = null;
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
            TxtFromDate.Text = Sdate.ToShortDateString();
            TxtToDate.Text = Edate.ToShortDateString();
          
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

    protected void txtICODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtICODE.Items.Clear();
                txtICODE.DataSource = data;
                txtICODE.DataBind();
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

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT ITEM_CODE, ITEM_DESC, CAT_CODE, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM  as Combined FROM TX_WASTE_MST WHERE ITEM_CODE LIKE :SearchQuery OR CAT_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM as Combined   FROM TX_WASTE_MST WHERE ITEM_CODE LIKE :SearchQuery OR CAT_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY ITEM_CODE";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE, ITEM_DESC, CAT_CODE FROM   TX_WASTE_MST WHERE      ITEM_CODE LIKE :SearchQuery OR CAT_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY ITEM_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./Waste_Stock_Statement.aspx", false);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
}


