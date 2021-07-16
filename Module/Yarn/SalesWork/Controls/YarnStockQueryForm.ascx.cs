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
public partial class Module_Yarn_SalesWork_Controls_YarnStockQueryForm : System.Web.UI.UserControl
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
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));

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

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void Initial_Control()
    {
        BindDepartment(ddlStore);
        BindDropDown(ddlLocation);           
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        getBrachName();
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        bindyear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
        bindFromToDate();
        //ddlYarnTypes();
        //ddlYarnCates();
        //GridYarnLedger();
        bindCategory("YARN_CAT");
        bindYarnType("YARN_TYPE");
        ddlYarn.SelectedIndex = -1;
        GridYarnLedger();
        cmbShade.SelectedIndex = -1;
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarn();
            DataView Dv = new DataView(dt);
            ddlYarn.DataSource = Dv;
            ddlYarn.DataValueField = "YARN_CODE";
            ddlYarn.DataTextField = "YARN_CODE";
            ddlYarn.DataBind();
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

    private void ddlYarnTypes()
    {
        try
        {

            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnType();
            DataView Dv = new DataView(dt);
            ddlYarnType.DataSource = Dv;
            ddlYarnType.DataValueField = "YARN_TYPE";
            ddlYarnType.DataTextField = "YARN_TYPE";
            ddlYarnType.DataBind();
            ddlYarnType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void ddlYarnCates()
    {
        try
        {


            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnCate();
            ddlYarnCate.DataSource = dt;
            ddlYarnCate.DataValueField = "YARN_CAT";
            ddlYarnCate.DataTextField = "YARN_CAT";
            ddlYarnCate.DataBind();
            ddlYarnCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }

    public void bindCategory(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlYarnCate.Items.Clear();
                ddlYarnCate.DataSource = dt;
                ddlYarnCate.DataTextField = "MST_DESC";
                ddlYarnCate.DataValueField = "MST_DESC";
                ddlYarnCate.DataBind();
                ddlYarnCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bindYarnType(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAMEForYarnType(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                // dv.RowFilter = "MST_DESC='" + TYPE + "'";
                if (dv != null && dv.Count > 0)
                {
                    ddlYarnType.Items.Clear();
                    ddlYarnType.DataSource = dv;
                    ddlYarnType.DataTextField = "MST_CODE";
                    ddlYarnType.DataValueField = "MST_CODE";
                    ddlYarnType.DataBind();
                    ddlYarnType.Items.Insert(0, new ListItem("---------------All---------------", ""));
                }
                else
                {
                    ddlYarnType.Items.Clear();
                    ddlYarnType.Items.Insert(0, new ListItem("------NoItems------", "0"));

                }
            }
        }
        catch
        {
            throw;
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
                    Common.CommonFuction.ShowMessage("Date is fine .");
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

    private void GridYarnLedger()
    {

        DateTime StDate;
        DateTime EnDate;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string YARN_TYPE = string.Empty;
        string YARN_CAT = string.Empty;
        string YARN_CODE = string.Empty;

        string YARN_SHADE_FAMILY = string.Empty;
        string YARN_SHADE = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
        string PARTY = string.Empty;
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


            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }
            if (ddlYarnCate.SelectedValue.ToString() != null && ddlYarnCate.SelectedValue.ToString() != string.Empty)
            {
                YARN_CAT = ddlYarnCate.SelectedValue.ToString();
            }
            else
            {
                YARN_CAT = string.Empty;
            }

            if (ddlYarnType.SelectedValue.ToString() != null && ddlYarnType.SelectedValue.ToString() != string.Empty)
            {
                YARN_TYPE = ddlYarnType.SelectedValue.ToString();
            }
            else
            {
                YARN_TYPE = string.Empty;
            }

            if (ddlYarn.SelectedValue.ToString() != null && ddlYarn.SelectedValue.ToString() != string.Empty)
            {
                YARN_CODE = ddlYarn.SelectedValue.Trim().ToString();
            }
            else
            {
                YARN_CODE = string.Empty;
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

            if (!string.IsNullOrEmpty(ddlLocation.SelectedValue))
            {
                LOCATION = ddlLocation.SelectedValue;
            }
            else
            {
                LOCATION = string.Empty;
            }

            if (!string.IsNullOrEmpty(ddlStore.SelectedValue))
            {
                STORE = ddlStore.SelectedValue;
            }
            else
            {
                STORE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
            {
                YARN_SHADE = cmbShade.SelectedValue.Trim().ToString();
            }
            else
            {
                YARN_SHADE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
            {
                YARN_SHADE_FAMILY = cmbShade.SelectedValue;
            }
            else
            {
                YARN_SHADE_FAMILY = string.Empty;
            }
            if (txtPartyCode.SelectedValue != string.Empty && txtPartyCode.SelectedText.Trim().ToString() != string.Empty)
            {
                PARTY = txtPartyCode.SelectedValue;
            }
            else
            {
                PARTY = string.Empty;
            }
            DataTable dt = new DataTable();


            dt = SaitexBL.Interface.Method.YRN_MST.Load_Stock_Data(BRANCH_CODE, DEPT_CODE, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate, oUserLoginDetail.COMP_CODE, int.Parse(ddlYear.SelectedItem.Text.ToString()), YARN_SHADE_FAMILY, YARN_SHADE, LOCATION, STORE, PARTY);
            // dt = SaitexBL.Interface.Method.YRN_MST.GetDataForLEDGER_Report1(BRANCH_CODE, DEPT_CODE, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate);
            GridLedger.DataSource = dt;
            GridLedger.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
        }

        catch (Exception ex)
        {
            throw ex;
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

    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        try
        {
            GridYarnLedger();
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

    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {


        DateTime StDate;
        DateTime EnDate;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string YARN_TYPE = string.Empty;
        string YARN_CAT = string.Empty;
        string YARN_CODE = string.Empty;
        string SHADE_FAMILY = string.Empty;
        string SHADE = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
        string PARTY = string.Empty;


        try
        {

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

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
            myDataColumn.ColumnName = "YARN_TYPE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "YARN_CAT";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "YARN_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.DateTime");
            myDataColumn.ColumnName = "StDate";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.DateTime");
            myDataColumn.ColumnName = "EnDate";
            myDataTable.Columns.Add(myDataColumn);

            myDataTable.Columns.Add(new DataColumn("YEAR", typeof(int)));

            myDataTable.Columns.Add(new DataColumn("SHADE_FAMILY", typeof(string)));
            myDataTable.Columns.Add(new DataColumn("SHADE", typeof(string)));
            myDataTable.Columns.Add(new DataColumn("LOCATION", typeof(string)));
            myDataTable.Columns.Add(new DataColumn("STORE", typeof(string)));
            myDataTable.Columns.Add(new DataColumn("PARTY", typeof(string)));
          
            DataRow row;

            row = myDataTable.NewRow();

            if (ddlYear.SelectedValue.ToString() != null && (int.Parse(ddlYear.SelectedItem.Text.ToString()) > 0))
            {
                row["YEAR"] = int.Parse(ddlYear.SelectedItem.Text.Trim());
            }
            else
            {
                row["YEAR"] = 0;
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

            if (ddlYarnCate.SelectedValue.ToString() != null && ddlYarnCate.SelectedValue.ToString() != string.Empty)
            {
                row["YARN_CAT"] = ddlYarnCate.SelectedValue.ToString();
            }
            else
            {
                row["YARN_CAT"] = string.Empty;
            }

            if (ddlYarnType.SelectedValue.ToString() != null && ddlYarnType.SelectedValue.ToString() != string.Empty)
            {
                row["YARN_TYPE"] = ddlYarnType.SelectedValue.ToString();
            }
            else
            {
                row["YARN_TYPE"] = string.Empty;

            }
            if (ddlYarn.SelectedValue.ToString() != null && ddlYarn.SelectedValue.ToString() != string.Empty)
            {
                row["YARN_CODE"] = ddlYarn.SelectedValue.Trim().ToString();
            }
            else
            {
                row["YARN_CODE"] = string.Empty;
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
            if (!string.IsNullOrEmpty(ddlLocation.SelectedValue))
            {
                row["LOCATION"] = ddlLocation.SelectedValue;
            }
           

            if (!string.IsNullOrEmpty(ddlStore.SelectedValue))
            {
                row["STORE"] = ddlStore.SelectedValue;
            }
            
            if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
            {
                row["SHADE"] = cmbShade.SelectedValue.Trim().ToString();
            }
           
            if (!string.IsNullOrEmpty(cmbShade.SelectedText))
            {
                row["SHADE_FAMILY"] = cmbShade.SelectedText;
            }

            if (txtPartyCode.SelectedValue != string.Empty && txtPartyCode.SelectedText.Trim().ToString() != string.Empty)
            {
                row["PARTY"] = txtPartyCode.SelectedValue;

            }
            else
            {
                row["PARTY"] = string.Empty;

            }
            myDataTable.Rows.Add(row);

            Session["MaterialLedger"] = myDataTable;
            string URL = "../Reports/YarnStockCrystlReport.aspx";
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

    protected void ddlYarn_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();

            dt = SaitexBL.Interface.Method.YRN_MST.GetYarn();
            DataView Dv = new DataView(dt);
            ddlYarn.DataSource = Dv;
            ddlYarn.DataValueField = "YARN_CODE";
            ddlYarn.DataTextField = "YARN_CODE";
            ddlYarn.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }

    protected void GridLedger_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridYarnLedger();
            GridLedger.PageIndex = e.NewPageIndex;
            GridLedger.DataBind();
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindyear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString())); ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            bindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void ddlYarn_LoadingItems1(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlYarn.Items.Clear();

                ddlYarn.DataSource = data;
                ddlYarn.DataBind();
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
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;       
    }

    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetShadeItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
            
        }
    }
    
    protected DataTable GetShadeItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_NAME ||'@' ||T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            
        }
        catch
        {
            throw;
        }
    }

    protected int GetShadeItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindDropDown(DropDownList ddl)
    {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.Items.Clear();
            ddl.DataSource = dt;
            ddl.DataTextField = "MST_DESC";
            ddl.DataValueField = "MST_DESC";
            ddl.DataBind();
        }
        else
        {
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

        }
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

    }

    private void BindDepartment(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode.Items.Clear();
            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            //CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE  UPPER (VENDOR_CAT_CODE) NOT IN ('TRANSPORTER','SPINNER','VENDOR')   and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE  UPPER (VENDOR_CAT_CODE) IN ('TRANSPORTER','SPINNER','VENDOR')  and ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {
        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE  UPPER (VENDOR_CAT_CODE) IN ('PARTY', 'KNITTER', 'SUPPLIER','SELF')  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

}
