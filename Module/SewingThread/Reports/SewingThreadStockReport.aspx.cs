﻿using System;
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
public partial class Module_Yarn_SalesWork_Reports_YarnStockReport : System.Web.UI.Page
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
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        getBrachName();
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        bindyear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
        bindFromToDate();
        ddlYarnTypes();
        ddlYarnCates();
        //GridYarnLedger();
        ddlYarn.SelectedIndex = -1;
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
            ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
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
            Dv.RowFilter = "YARN_TYPE = 'SEWING THREAD'";
            ddlYarnType.DataSource = Dv;
            ddlYarnType.DataValueField = "YARN_TYPE";
            ddlYarnType.DataTextField = "YARN_TYPE";
            ddlYarnType.DataBind();
            //ddlYarnType.Items.Insert(0, new ListItem("---------------All---------------", ""));
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
            DataView Dv = new DataView(dt);
            Dv.RowFilter = "YARN_CAT = 'SEWING THREAD'";
            ddlYarnCate.DataSource = Dv;
            ddlYarnCate.DataValueField = "YARN_CAT";
            ddlYarnCate.DataTextField = "YARN_CAT";
            ddlYarnCate.DataBind();
            //ddlYarnCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
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
            DataTable dt = new DataTable();


            dt = SaitexBL.Interface.Method.YRN_MST.Load_Stock_Data(BRANCH_CODE, DEPT_CODE, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate, oUserLoginDetail.COMP_CODE, int.Parse(ddlYear.SelectedItem.Text.ToString()),"","","","","");
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
    //protected void GridLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridLedger.PageIndex = e.NewPageIndex;
    //    GridYarnLedger();
    //}

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


            myDataTable.Rows.Add(row);

            Session["MaterialLedger"] = myDataTable;
            string URL = "../Reports/SwStockCrystalReport.aspx";
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
}