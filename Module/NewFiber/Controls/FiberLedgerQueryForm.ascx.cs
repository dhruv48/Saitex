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

public partial class Module_Fiber_Controls_FiberLedgerQueryForm : System.Web.UI.UserControl
{
    private  string Start_Year = string.Empty;
    private  string End_Year = string.Empty;
    string branch = string.Empty;
    private  DateTime Sdate;
    private  DateTime Edate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                Initial_Control();
                GridFiberLedger();

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }
    private void Initial_Control()
    {
        GridLedger.Visible = false;
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
        getBrachName();
        BindYear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
        //getDepartment();
        ddlFiberTypes();
        ddlFiberCates();
        ddlFiber.SelectedIndex = -1;
        lblTotalRecord.Text = string.Empty;
    }
    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiber();
            DataView Dv = new DataView(dt);
            ddlFiber.DataSource = Dv;
            ddlFiber.DataValueField = "FIBER_CODE";
            ddlFiber.DataTextField = "FIBER_CODE";
            ddlFiber.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
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
            //ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    //private void getDepartment()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
    //        ddlDepartment.DataSource = dt;
    //        ddlDepartment.DataValueField = "DEPT_CODE";
    //        ddlDepartment.DataTextField = "DEPT_NAME";
    //        ddlDepartment.DataBind();
    //        ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }

    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
    //    }
    //}
    private void BindYear()
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
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(" No financial Year associated with " + brnch + " branch ");
                getBrachName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                BindYear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                bindFromToDate();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ddlFiberTypes()
    {
        try
        {

            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiberCat();
            DataView Dv = new DataView(dt);
            ddlFiberType.DataSource = Dv;
            ddlFiberType.DataValueField = "FIBER_CAT";
            ddlFiberType.DataTextField = "FIBER_CAT";
            ddlFiberType.DataBind();
            ddlFiberType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void ddlFiberCates()
    {
        try
        {


            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiberSubCate();
            ddlFiberCate.DataSource = dt;
            ddlFiberCate.DataValueField = "SUB_FIBER_CAT";
            ddlFiberCate.DataTextField = "SUB_FIBER_CAT";
            ddlFiberCate.DataBind();
            ddlFiberCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
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

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void GridFiberLedger()
    {

        DateTime StDate;
        DateTime EnDate;
        //string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string SUB_FIBER_CAT = string.Empty;
        string FIBER_CAT = string.Empty;
        string FIBER_CODE = string.Empty;
        try
        {
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }

            if (ddlYear.SelectedItem.Text.ToString() != null && ddlYear.SelectedItem.Text.ToString() != string.Empty)
            {
                YEAR = ddlYear.SelectedItem.Text.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }
            //if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            //{
            //    DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            //}
            //else
            //{
            //    DEPT_CODE = string.Empty;
            //}
            if (ddlFiberCate.SelectedValue.ToString() != null && ddlFiberCate.SelectedValue.ToString() != string.Empty)
            {
                SUB_FIBER_CAT = ddlFiberCate.SelectedValue.ToString();
            }
            else
            {
                SUB_FIBER_CAT = string.Empty;
            }

            if (ddlFiberType.SelectedValue.ToString() != null && ddlFiberType.SelectedValue.ToString() != string.Empty)
            {
                FIBER_CAT = ddlFiberType.SelectedValue.ToString();
            }
            else
            {
                FIBER_CAT = string.Empty;
            }

            if (ddlFiber.SelectedValue.ToString() != null && ddlFiber.SelectedValue.ToString() != string.Empty)
            {
                FIBER_CODE = ddlFiber.SelectedValue.Trim().ToString();
            }
            else
            {
                FIBER_CODE = string.Empty;
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
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetDataForLEDGER_Report1(BRANCH_CODE, YEAR, SUB_FIBER_CAT, FIBER_CAT, FIBER_CODE, StDate, EnDate);
            if (dt.Rows.Count > 0)
            {
                GridLedger.DataSource = dt;
                GridLedger.DataBind();
                GridLedger.Visible = true;
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

            }
            else
            {
                GridLedger.DataSource = null;
                GridLedger.DataBind();
                lblTotalRecord.Text = "0";

            }
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void GridLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridFiberLedger();
        GridLedger.PageIndex = e.NewPageIndex;
        GridLedger.DataBind();
    }
    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        try
        {
            GridFiberLedger();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
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
    protected void imgbtnExit_Click1(object sender, ImageClickEventArgs e)
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
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {


        string StDate = string.Empty;
        string EnDate = string.Empty;
        //string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string FIBER_CAT = string.Empty;
        string SUB_FIBER_CAT = string.Empty;
        string FIBER_CODE = string.Empty;
        string BRANCH = string.Empty;
        string SUBFIBERCAT = string.Empty;
        string FIBERCAT = string.Empty;
        string FIBER1 = string.Empty;
        string YEAR1 = string.Empty;
        try
        {

            DataTable myDataTable = new DataTable();
            myDataTable.Columns.Add("BRANCH_CODE", typeof(string));
            myDataTable.Columns.Add("YEAR", typeof(string));
            myDataTable.Columns.Add("FIBER_CAT", typeof(string));
            myDataTable.Columns.Add("SUB_FIBER_CAT", typeof(string));
            myDataTable.Columns.Add("FIBER_CODE", typeof(string));
            myDataTable.Columns.Add("StDate", typeof(string));
            myDataTable.Columns.Add("EnDate", typeof(string));
            myDataTable.Columns.Add("BRANCH", typeof(string));
            myDataTable.Columns.Add("SUBFIBERCAT", typeof(string));
            myDataTable.Columns.Add("FIBERCAT", typeof(string));
            myDataTable.Columns.Add("FIBER1", typeof(string));
            myDataTable.Columns.Add("YEAR1", typeof(string));


            DataRow row;

            row = myDataTable.NewRow();

            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                row["BRANCH_CODE"] = ddlBranch.SelectedValue.ToString();
                row["BRANCH"] = ddlBranch.SelectedItem.ToString();
            }
            else
            {
                row["BRANCH_CODE"] = string.Empty;
                row["BRANCH"] = string.Empty;
            }

            if (ddlYear.SelectedItem.Text.ToString() != null && ddlYear.SelectedItem.Text.ToString() != string.Empty)
            {
                row["YEAR"] = ddlYear.SelectedItem.Text.ToString();
                row["YEAR1"] = ddlYear.SelectedItem.Text.ToString();
            }
            else
            {
                row["YEAR"] = string.Empty;
                row["YEAR1"] = string.Empty;
            }

            //if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            //{
            //    row["DEPT_CODE"] = ddlDepartment.SelectedValue.ToString();
            //}
            //else
            //{
            //    row["DEPT_CODE"] = string.Empty;
            //}

            if (ddlFiberCate.SelectedValue.ToString() != null && ddlFiberCate.SelectedValue.ToString() != string.Empty)
            {
                row["SUB_FIBER_CAT"] = ddlFiberCate.SelectedValue.ToString();
                row["SUBFIBERCAT"] = ddlFiberCate.SelectedItem.ToString();
            }
            else
            {
                row["SUB_FIBER_CAT"] = string.Empty;
                row["SUBFIBERCAT"] = string.Empty;
            }

            if (ddlFiberType.SelectedValue.ToString() != null && ddlFiberType.SelectedValue.ToString() != string.Empty)
            {
                row["FIBER_CAT"] = ddlFiberType.SelectedValue.ToString();
                row["FIBER_CAT"] = ddlFiberType.SelectedItem.ToString();
            }
            else
            {
                row["FIBER_CAT"] = string.Empty;
                row["FIBER_CAT"] = string.Empty;
            }
            if (ddlFiber.SelectedValue.ToString() != null && ddlFiber.SelectedValue.ToString() != string.Empty)
            {
                row["FIBER_CODE"] = ddlFiber.SelectedValue.Trim().ToString();
                row["FIBER1"] = ddlFiber.SelectedValue.ToString();
            }
            else
            {
                row["FIBER_CODE"] = string.Empty;
                row["FIBER1"] = string.Empty;
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


            myDataTable.Rows.Add(row);

            Session["MaterialLedger"] = myDataTable;
            string URL = "../Reports/Fiber_Ledger.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initial_Control();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void ddlFiber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiber();
            DataView Dv = new DataView(dt);
            ddlFiber.DataSource = Dv;
            ddlFiber.DataValueField = "FIBER_CODE";
            ddlFiber.DataTextField = "FIBER_CODE";
            ddlFiber.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            bindFromToDate();
        }
        catch (Exception ex)
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
    protected void Item_LOV_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlFiber.Items.Clear();

                ddlFiber.DataSource = data;
                ddlFiber.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();

        }
    }
    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT FIBER_CODE, FIBER_DESC, FIBER_CAT FROM TX_FIBER_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND FIBER_code NOT IN (SELECT FIBER_CODE FROM (SELECT FIBER_CODE, FIBER_DESC FROM TX_FIBER_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY FIBER_CODE";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   FIBER_CODE, FIBER_DESC, FIBER_CAT FROM   TX_FIBER_MASTER WHERE      FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY   FIBER_CODE) asd WHERE FIBER_CAT <> 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY FIBER_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
}
