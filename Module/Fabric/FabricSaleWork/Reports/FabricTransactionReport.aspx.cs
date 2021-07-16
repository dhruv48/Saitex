using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using errorLog;
using Obout.ComboBox;
using Obout.Interface;
using System.Data;
public partial class Module_Fabric_FabricSaleWork_Reports_FabricTransactionReport : System.Web.UI.Page
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    string branch = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    string FABR_TYPE;
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
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
        getBrachName();
        BindYear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));

        GetFabrType();
        //ddlYarnCates();
        getDepartment();
        bindddlPartycode();
        bindddltrntype();
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
            throw ex;
        }
    }
    private void GetFabrType()
    {
        try
        {

            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabrType();
          
            ddlFabrType.DataSource = dt;
            ddlFabrType.DataValueField = "FABR_TYPE";
            ddlFabrType.DataTextField = "FABR_TYPE";
            ddlFabrType.DataBind();
            ddlFabrType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    //private void ddlYarnCates()
    //{
    //    try
    //    {

            
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.YRN_MST.GetYarnCate();
    //        DataView Dv = new DataView(dt);
    //        Dv.RowFilter = "YARN_CAT = '"+ FABR_TYPE + "'";

          
            
    //        ddlYarnCate.DataSource = Dv;

    //        ddlYarnCate.DataValueField = "YARN_CAT";
    //        ddlYarnCate.DataTextField = "YARN_CAT";
    //        ddlYarnCate.DataBind();
    //        //ddlYarnCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }

    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
    //    }
    //}
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
        try
        {
            string URL = "../Reports/FabricTransactionCry.aspx?BRANCH_CODE=" + ddlBranch.SelectedValue.ToString() + "&YEAR=" + ddlYear.SelectedItem.Text.ToString() + "&DEPT_CODE=" + ddlDepartment.SelectedValue.ToString() + "&FABR_TYPE=" + ddlFabrType.SelectedValue.ToString()  + "&YARN_CODE=" + ddlYarn.SelectedValue.ToString() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString() + "&TRAN_TYPE=" + ddlTrnType.SelectedValue.ToString() + "&PARTY_CODE=" + ddlPartycode.SelectedValue.ToString() + "&BRANCH=" + ddlBranch.SelectedItem.ToString() + "&TRNTYPE=" + ddlTrnType.SelectedItem.ToString() + "&PARTY=" + ddlPartycode.SelectedItem.ToString() + "&DEPARTMENT1=" + ddlDepartment.SelectedItem.ToString() + "&YARN1=" + ddlYarn.SelectedValue.ToString();
           // string URL = "../Reports/FabricTransactionCry.aspx?BRANCH_CODE=" + ddlBranch.SelectedValue.ToString() + "&YEAR=" + ddlYear.SelectedItem.Text.ToString() + "&DEPT_CODE=" + ddlDepartment.SelectedValue.ToString() + "&YARN_TYPE=" + ddlYarnType.SelectedValue.ToString() + "&YARN_CAT=" + ddlYarnCate.SelectedValue.ToString() + "&YARN_CODE=" + ddlYarn.SelectedValue.ToString() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString() + "&TRAN_TYPE=" + ddlTrnType.SelectedValue.ToString() + "&PARTY_CODE=" + ddlPartycode.SelectedValue.ToString() + "&BRANCH=" + ddlBranch.SelectedItem.ToString() + "&TRNTYPE=" + ddlTrnType.SelectedItem.ToString() + "&YRNTYPE=" + ddlYarnType.SelectedItem.ToString() + "&YARNCAT=" + ddlYarnType.SelectedItem.ToString() + "&PARTY=" + ddlPartycode.SelectedItem.ToString() + "&DEPARTMENT1=" + ddlDepartment.SelectedItem.ToString() + "&YARN1=" + ddlYarn.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            // Response.Redirect("../Reports/YarnTransactionCrstReport.aspx?BRANCH_CODE=" + ddlBranch.SelectedValue.ToString() + "&YEAR=" + ddlYear.SelectedItem.Text.ToString() + "&DEPT_CODE=" + ddlDepartment.SelectedValue.ToString() + "&YARN_TYPE=" + ddlYarnType.SelectedValue.ToString() + "&YARN_CAT=" + ddlYarnCate.SelectedValue.ToString() + "&YARN_CODE=" + ddlYarn.SelectedValue.ToString() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString() + "&TRAN_TYPE=" + ddlTrnType.SelectedValue.ToString() + "&PARTY_CODE=" + ddlPartycode.SelectedValue.ToString() + "&BRANCH=" + ddlBranch.SelectedItem.ToString() + "&TRNTYPE=" + ddlTrnType.SelectedItem.ToString() + "&YRNTYPE=" + ddlYarnType.SelectedItem.ToString() + "&YARNCAT=" + ddlYarnType.SelectedItem.ToString() + "&PARTY=" + ddlPartycode.SelectedItem.ToString() + "&DEPARTMENT1=" + ddlDepartment.SelectedItem.ToString() + "&YARN1=" + ddlYarn.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Open Report.\r\nSee error log for detail."));
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
            ddlYarn.DataTextField = "YARN_DESC";
            ddlYarn.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }

    //protected void ddlYarn_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = null;
    //         //dt = GetYarn(e.Text.ToUpper());

           
    //        ddlYarn.Items.Clear();
    //        dt = new DataTable();
           
    //        dt = GetYarn(e.Text.ToUpper(), e.ItemsOffset);
    //        dt = SaitexBL.Interface.Method.YRN_MST.GetYarn();
    //        DataView Dv = new DataView(dt);
    //        ddlYarn.DataSource = Dv;
    //        ddlYarn.DataTextField = "YARN_CODE";
    //        ddlYarn.DataValueField = "YARN_DESC";
    //        ddlYarn.DataSource = dt;
    //        ddlYarn.DataBind();
    //        dt.Dispose();

    //        e.ItemsLoadedCount = dt.Rows.Count;
    //        e.ItemsCount = dt.Rows.Count;


    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PI Number loading.\r\nSee error log for detail."));
           
    //    }
    //}

    private DataTable GetYarn(string p, int p_2)
    {
        throw new NotImplementedException();
    }

    private int GetYarn_Count(string p)
    {
        throw new NotImplementedException();
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
    private void bindddlPartycode()
    {
        try
        {
            ddlPartycode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlPartycode.DataTextField = "PRTY_NAME";
                ddlPartycode.DataValueField = "PRTY_CODE";
                ddlPartycode.DataSource = dt;
                ddlPartycode.DataBind();
            }

            ddlPartycode.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }

    }
    private void bindddltrntype()
    {

        try
        {

            ddlTrnType.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.YRN_MST.getTransType();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTrnType.DataTextField = "TRN_DESC";
                ddlTrnType.DataValueField = "TRN_TYPE";
                ddlTrnType.DataSource = dt;
                ddlTrnType.DataBind();

            }
            ddlTrnType.Items.Insert(0, new ListItem("---------------All---------------", ""));
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

    protected void ddlYarnType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
 
}
