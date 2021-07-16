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

public partial class Module_Fabric_FabricSaleWork_Controls_FabricLedgerQuery1 : System.Web.UI.UserControl
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    string branch = string.Empty;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Page Loading"));
        }
    }
    private void Initial_Control()
    {
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
        getBrachName();
        BindYear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.ToString()));
       // ddlFabricTypes();
        ddlFabricTypes();
        ddlFabric.SelectedIndex = -1;
    }
    protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabric();
            DataView Dv = new DataView(dt);
            ddlFabric.DataSource = Dv;
            ddlFabric.DataValueField = "FABR_CODE";
            ddlFabric.DataTextField = "FABR_CODE";
            ddlFabric.DataBind();
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
                ddlYear.DataValueField = "YEAR";         //"FIN_YEAR_CODE";
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

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ddlFabricTypes()
    {
        try
        {

            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricType();

            DataView Dv = new DataView(dt);
           // Dv.RowFilter = "YARN_TYPE = 'SEWING THREAD'";
            ddlFabricType.DataSource = Dv;
            ddlFabricType.DataValueField = "FABR_TYPE";
            ddlFabricType.DataTextField = "FABR_TYPE";
            ddlFabricType.DataBind();
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
    protected void grdFabricLedger()
    {
        DateTime StDate;
        DateTime EnDate;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string FABR_TYPE = string.Empty;
        string FABR_CODE = string.Empty;
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
            if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
            {
                YEAR = ddlYear.SelectedValue.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }
            if (ddlFabricType.SelectedValue.ToString() != null && ddlFabricType.SelectedValue.ToString() != string.Empty)
            {
                FABR_TYPE = ddlFabricType.SelectedValue.ToString();
            }
            else
            {
                FABR_TYPE = string.Empty;
            }
            if (ddlFabric.SelectedValue.ToString() != null && ddlFabric.SelectedValue.ToString() != string.Empty)
            {
                FABR_CODE = ddlFabric.SelectedValue.ToString();
            }
            else
            {
                FABR_CODE = string.Empty;
            }
            if (TxtFromDate.Text.Trim() != null && TxtFromDate.Text.Trim() != string.Empty)
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
            dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetDataForFabricLedger(BRANCH_CODE, YEAR, FABR_TYPE, FABR_CODE, StDate, EnDate);
            GridLedger.DataSource = dt;
            GridLedger.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    protected void GridLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridLedger.PageIndex = e.NewPageIndex;
        grdFabricLedger();
    }

    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        try
        {
            grdFabricLedger();
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

    //protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    //{

    //    string StDate = string.Empty;
    //    string EnDate = string.Empty;
    //    //string DEPT_CODE = string.Empty;
    //    string BRANCH_CODE = string.Empty;
    //    string YEAR = string.Empty;
    //    string FABR_TYPE = string.Empty;
    //    //string YARN_CAT = string.Empty;
    //    string FABR_CODE = string.Empty;
    //    string BRANCH = string.Empty;
    //   // string YARNCAT = string.Empty;
    //    string FABRTYPE = string.Empty;
    //    string FABR1 = string.Empty;
    //    string YEAR1 = string.Empty;
    //    try
    //    {

    //        DataTable myDataTable = new DataTable();
    //        myDataTable.Columns.Add("BRANCH_CODE", typeof(string));
    //        myDataTable.Columns.Add("YEAR", typeof(string));
    //        myDataTable.Columns.Add("FABR_TYPE", typeof(string));
    //      //  myDataTable.Columns.Add("YARN_CAT", typeof(string));
    //        myDataTable.Columns.Add("FABR_CODE", typeof(string));
    //        myDataTable.Columns.Add("StDate", typeof(string));
    //        myDataTable.Columns.Add("EnDate", typeof(string));
    //        myDataTable.Columns.Add("BRANCH", typeof(string));
    //       // myDataTable.Columns.Add("YARNCAT", typeof(string));
    //        myDataTable.Columns.Add("FABRTYPE", typeof(string));
    //        myDataTable.Columns.Add("FABR1", typeof(string));
    //        myDataTable.Columns.Add("YEAR1", typeof(string));


    //        DataRow row;

    //        row = myDataTable.NewRow();

    //        if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
    //        {
    //            row["BRANCH_CODE"] = ddlBranch.SelectedValue.ToString();
    //            row["BRANCH"] = ddlBranch.SelectedItem.ToString();
    //        }
    //        else
    //        {
    //            row["BRANCH_CODE"] = string.Empty;
    //            row["BRANCH"] = string.Empty;
    //        }

    //        if (ddlYear.SelectedItem.Text.ToString() != null && ddlYear.SelectedItem.Text.ToString() != string.Empty)
    //        {
    //            row["YEAR"] = ddlYear.SelectedItem.Text.ToString();
    //            row["YEAR1"] = ddlYear.SelectedItem.Text.ToString();
    //        }
    //        else
    //        {
    //            row["YEAR"] = string.Empty;
    //            row["YEAR1"] = string.Empty;
    //        }

    //        //if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
    //        //{
    //        //    row["DEPT_CODE"] = ddlDepartment.SelectedValue.ToString();
    //        //}
    //        //else
    //        //{
    //        //    row["DEPT_CODE"] = string.Empty;
    //        //}



    //        if (ddlFabricType.SelectedValue.ToString() != null && ddlFabricType.SelectedValue.ToString() != string.Empty)
    //        {
    //            row["FABR_TYPE"] = ddlFabricType.SelectedValue.ToString();
    //            row["FABR_TYPE"] = ddlFabricType.SelectedItem.ToString();
    //        }
    //        else
    //        {
    //            row["FABR_TYPE"] = string.Empty;
    //            row["FABR_TYPE"] = string.Empty;
    //        }
    //        if (ddlFabric.SelectedValue.ToString() != null && ddlFabric.SelectedValue.ToString() != string.Empty)
    //        {
    //            row["FABR_CODE"] = ddlFabric.SelectedValue.Trim().ToString();
    //            row["FABR1"] = ddlFabric.SelectedValue.ToString();
    //        }
    //        else
    //        {
    //            row["YARN_CODE"] = string.Empty;
    //            row["FABR1"] = string.Empty;
    //        }

    //        if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
    //        {
    //            row["StDate"] = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
    //            row["EnDate"] = DateTime.Parse(TxtToDate.Text.Trim().ToString());
    //        }
    //        else
    //        {
    //            row["StDate"] = Sdate;
    //            row["EnDate"] = Edate;
    //        }


    //        myDataTable.Rows.Add(row);

    //        Session["MaterialLedger"] = myDataTable;
    //        string URL = "../Reports/Sw_Ledger.aspx";
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
    //    }
    //}

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

    protected void ddlFabric_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabric();
            DataView Dv = new DataView(dt);
            ddlFabric.DataSource = Dv;
            ddlFabric.DataValueField = "FABR_CODE";
            ddlFabric.DataTextField = "FABR_CODE";
            ddlFabric.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
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
}

   


