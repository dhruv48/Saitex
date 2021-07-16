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
public partial class Module_Inventory_Reports_PurchaseRegisterDetailReport : System.Web.UI.Page
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

    private void Initial_Control()
    {

        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
        getBrachName();
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        bindyear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
        getDepartment();
        getItemType();
        getItemCategory();
        gridmaterialledger();
        txtICODE.SelectedIndex = -1;
        bindddlprty();

    }

    private void bindyear()
    {
        try
        {

            string branch = ddlBranch.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);
            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));

                dt.Dispose();
                dt = null;

            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(brnch + " No have financial year & data .");
                getBrachName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                bindyear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                gridmaterialledger();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void bindddlprty()
    {

        try
        {

            ddlpartycode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlpartycode.DataTextField = "PRTY_NAME";
                ddlpartycode.DataValueField = "PRTY_CODE";
                ddlpartycode.DataSource = dt;
                ddlpartycode.DataBind();

            }

            ddlpartycode.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }
    }

    private void bindFromToDate()
    {
        string FIN_YEAR_CODE = ddlYear.SelectedValue.ToString();
        DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "FIN_YEAR_CODE='" + ddlYear.SelectedValue.ToString() + "'";
            if (dv.Count > 0)
            {
                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                {
                    TxtFromDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                    TxtToDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                }
            }
        }
    }

    private void getBrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();

            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }

    private void getItemType()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemType();
            DataView Dv = new DataView(dt);
            ddlItemType.DataSource = Dv;
            ddlItemType.DataValueField = "ITEM_TYPE";
            ddlItemType.DataTextField = "ITEM_TYPE";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getItemCategory()
    {
        try
        {

            DataTable dt = new DataTable();

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemCate();
            ddlItemCate.DataSource = dt;
            ddlItemCate.DataValueField = "CAT_CODE";
            ddlItemCate.DataTextField = "CAT_CODE";
            ddlItemCate.DataBind();
            ddlItemCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
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

    private void gridmaterialledger()
    {

        DateTime StDate;
        DateTime EnDate;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string ITEM_TYPE = string.Empty;
        string ITEM_CATE = string.Empty;
        string ITEM_CODE = string.Empty;
        string PRTY_CODE = string.Empty;
        string YEAR = string.Empty;
        try
        {

            if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
            {
                YEAR = ddlYear.SelectedValue.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }


            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }
            if (ddlItemCate.SelectedValue.ToString() != null && ddlItemCate.SelectedValue.ToString() != string.Empty)
            {
                ITEM_CATE = ddlItemCate.SelectedValue.ToString();
            }
            else
            {
                ITEM_CATE = string.Empty;
            }

            if (ddlItemType.SelectedValue.ToString() != null && ddlItemType.SelectedValue.ToString() != string.Empty)
            {
                ITEM_TYPE = ddlItemType.SelectedValue.ToString();
            }
            else
            {
                ITEM_TYPE = string.Empty;
            }
            if (txtICODE.SelectedValue.ToString() != null && txtICODE.SelectedValue.ToString() != string.Empty)
            {
                ITEM_CODE = txtICODE.SelectedValue.Trim().ToString();
            }
            else
            {
                ITEM_CODE = string.Empty;
            }
            if (ddlpartycode.SelectedValue.ToString() != null && ddlpartycode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddlpartycode.SelectedValue.Trim().ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }


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
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.Purchaseregisterdetail(YEAR, BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, PRTY_CODE, StDate, EnDate);
            GridLedger.DataSource = dt;
            GridLedger.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        try
        {
            gridmaterialledger();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void TxtToDate_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
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

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./PurchaseRegisterDetailReport.aspx", false);
            //Initial_Control();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string YEAR = string.Empty;
        string StDate = string.Empty;
        string EnDate = string.Empty;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string ITEM_TYPE = string.Empty;
        string ITEM_CATE = string.Empty;
        string ITEM_CODE = string.Empty;
        string PRTY_CODE = string.Empty;
        try
        {

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;


            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "YEAR";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BRANCH_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DEPT_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ITEM_TYPE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ITEM_CATE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ITEM_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "StDate";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "EnDate";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PRTY_CODE";
            myDataTable.Columns.Add(myDataColumn);

            DataRow row;

            row = myDataTable.NewRow();

            if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
            {
                row["YEAR"] = ddlYear.SelectedValue.ToString();
            }
            else
            {
                row["YEAR"] = string.Empty;
            }

            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                row["BRANCH_CODE"] = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                row["BRANCH_CODE"] = string.Empty;
            }


            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            {
                row["DEPT_CODE"] = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                row["DEPT_CODE"] = string.Empty;
            }
            if (ddlItemCate.SelectedValue.ToString() != null && ddlItemCate.SelectedValue.ToString() != string.Empty)
            {
                row["ITEM_CATE"] = ddlItemCate.SelectedValue.ToString();
            }
            else
            {
                row["ITEM_CATE"] = string.Empty;
            }

            if (ddlItemType.SelectedValue.ToString() != null && ddlItemType.SelectedValue.ToString() != string.Empty)
            {
                row["ITEM_TYPE"] = ddlItemType.SelectedValue.ToString();
            }
            else
            {
                row["ITEM_TYPE"] = string.Empty;
            }
            if (txtICODE.SelectedValue.ToString() != null && txtICODE.SelectedValue.ToString() != string.Empty)
            {
                row["ITEM_CODE"] = txtICODE.SelectedValue.Trim().ToString();
            }
            else
            {
                row["ITEM_CODE"] = string.Empty;
            }

            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                row["StDate"] = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                row["EnDate"] = DateTime.Parse(TxtToDate.Text.Trim().ToString());
            }
            else
            {
                row["StDate"] = Sdate;
                row["EnDate"] = Edate;
            }

            if (ddlpartycode.SelectedValue.ToString() != null && ddlpartycode.SelectedValue.ToString() != string.Empty)
            {
                row["PRTY_CODE"] = ddlpartycode.SelectedValue.Trim().ToString();
            }
            else
            {
                row["PRTY_CODE"] = string.Empty;
            }

            myDataTable.Rows.Add(row);

            Session["MaterialLedger"] = myDataTable;
            string URL = "../Reports/MaterialPurchaseRegisterCrReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void GridLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridLedger.PageIndex = e.NewPageIndex;
        gridmaterialledger();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            bindyear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString())); ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            //TxtFromDate.Text = string.Empty;
            //TxtToDate.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GridLedger_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            string CommandText = " SELECT * FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_TYPE, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM  as Combined FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM as Combined   FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE, ITEM_DESC, ITEM_TYPE FROM   TX_ITEM_MST WHERE      ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd ";
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
}