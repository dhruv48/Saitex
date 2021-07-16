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

public partial class Module_SewingThread_Queries_SewingThreadTransactionQuery : System.Web.UI.Page
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
                InitialControls();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }

    }

    private void InitialControls()
    {
        try
        {
            //grid_trn_Query.Visible = true;
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            GetBranchName();
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            BindYarnType();
            BindYarnCat();
            BindPartyCode();
            BindTrnType();
            BindDepartment();
            ddlYarn.SelectedIndex = -1;
            //GridYarnTrn();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetBranchName()
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
            ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", string.Empty));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            throw ex;
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
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataBind();
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));

                dt.Dispose();
                dt = null;
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindYarnType()
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
            throw ex;
        }
    }

    private void BindYarnCat()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnCate();
            DataView Dv = new DataView(dt);
            Dv.RowFilter = "YARN_CAT = 'SEWING THREAD'";
            ddlYarnCate.DataSource =Dv;
            ddlYarnCate.DataValueField = "YARN_CAT";
            ddlYarnCate.DataTextField = "YARN_CAT";
            ddlYarnCate.DataBind();
           // ddlYarnCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindPartyCode()
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
        catch (Exception ex)
        {
        }
    }

    private void BindTrnType()
    {
        try
        {
            ddlTrnType.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.YRN_MST.getTransType();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTrnType.DataTextField = "TRN_TDESC";
                ddlTrnType.DataValueField = "TRN_TYPE";
                ddlTrnType.DataSource = dt;
                ddlTrnType.DataBind();
                ddlTrnType.Items.Insert(0, new ListItem("-----------All---------------", ""));
                dt.Dispose();
                dt = null;
            
            }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindDepartment()
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

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControls();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }

    private void GridYarnTrn()
    {
        DateTime StDate;
        DateTime EnDate;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string YARN_TYPE = string.Empty;
        string YARN_CAT = string.Empty;
        string YARN_CODE = string.Empty;
        string TRAN_TYPE = string.Empty;
        string PARTY_CODE = string.Empty;
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


            if (ddlTrnType.SelectedValue.ToString() != null && ddlTrnType.SelectedValue.ToString() != string.Empty)
            {
                TRAN_TYPE = ddlTrnType.SelectedValue.ToString();
            }
            else
            {
                TRAN_TYPE = string.Empty;

            }

            if (ddlPartycode.SelectedValue.ToString() != null && ddlPartycode.SelectedValue.ToString() != string.Empty)
            {
                PARTY_CODE = ddlPartycode.SelectedValue.ToString();
            }
            else
            {
                PARTY_CODE = string.Empty;

            }
            DataTable dt = new DataTable();

            dt = SaitexBL.Interface.Method.YRN_MST.YARNTRANSACTION(BRANCH_CODE, DEPT_CODE, "", YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE, "", "", "", "", "");
            if (dt.Rows.Count > 0)
            {
                grid_trn_Query.DataSource = dt;
                grid_trn_Query.DataBind();
                grid_trn_Query.Visible = true;
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

            }
            else
            {
                grid_trn_Query.DataSource = null;
                grid_trn_Query.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                grid_trn_Query.Visible = true;
                lblTotalRecord.Text = "0";
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        try
        {
            GridYarnTrn();
            grid_trn_Query.Visible = true;
        }
        catch (Exception ex)
        {
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
protected void  ddlYear_SelectedIndexChanged(object sender, EventArgs e)
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
protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
{
    try
    {

        string URL = "../Reports/SwTransactionCrstReport.aspx?BRANCH_CODE=" + ddlBranch.SelectedValue.ToString() + "&YEAR=" + ddlYear.SelectedItem.Text.ToString() + "&DEPT_CODE=" + ddlDepartment.SelectedValue.ToString() + "&YARN_TYPE=" + ddlYarnType.SelectedValue.ToString() + "&YARN_CAT=" + ddlYarnCate.SelectedValue.ToString() + "&YARN_CODE=" + ddlYarn.SelectedValue.ToString() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString() + "&TRAN_TYPE=" + ddlTrnType.SelectedValue.ToString() + "&PARTY_CODE=" + ddlPartycode.SelectedValue.ToString() + "&BRANCH=" + ddlBranch.SelectedItem.ToString() + "&TRNTYPE=" + ddlTrnType.SelectedItem.ToString() + "&YRNTYPE=" + ddlYarnType.SelectedItem.ToString() + "&YARNCAT=" + ddlYarnType.SelectedItem.ToString() + "&PARTY=" + ddlPartycode.SelectedItem.ToString() + "&DEPARTMENT1=" + ddlDepartment.SelectedItem.ToString() + "&YARN1=" + ddlYarn.SelectedValue.ToString();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        // Response.Redirect("../Reports/YarnTransactionCrstReport.aspx?BRANCH_CODE=" + ddlBranch.SelectedValue.ToString() + "&YEAR=" + ddlYear.SelectedItem.Text.ToString() + "&DEPT_CODE=" + ddlDepartment.SelectedValue.ToString() + "&YARN_TYPE=" + ddlYarnType.SelectedValue.ToString() + "&YARN_CAT=" + ddlYarnCate.SelectedValue.ToString() + "&YARN_CODE=" + ddlYarn.SelectedValue.ToString() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString() + "&TRAN_TYPE=" + ddlTrnType.SelectedValue.ToString() + "&PARTY_CODE=" + ddlPartycode.SelectedValue.ToString() + "&BRANCH=" + ddlBranch.SelectedItem.ToString() + "&TRNTYPE=" + ddlTrnType.SelectedItem.ToString() + "&YRNTYPE=" + ddlYarnType.SelectedItem.ToString() + "&YARNCAT=" + ddlYarnType.SelectedItem.ToString() + "&PARTY=" + ddlPartycode.SelectedItem.ToString() + "&DEPARTMENT1=" + ddlDepartment.SelectedItem.ToString() + "&YARN1=" + ddlYarn.SelectedValue.ToString());
    }
    catch (Exception ex)
    {
        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Open Report.\r\nSee error log for detail."));
    }
}
}
