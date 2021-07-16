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
using System.Data.OracleClient;
using errorLog;
using Common;

public partial class Module_HRMS_Pages_DailyAttendance : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    String url = string.Empty;
    string branch_code = string.Empty;
    string dept_code = string.Empty;
    string cadre_code = string.Empty;
    string adate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!Page.IsPostBack)
            {
                txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                getBrachName();
                getDepartment();
                Bind_DropDown("EMP_CADDER", ddlCadre);
                Load_Attendance();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Page Loading"));
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
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }

    private void getDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
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
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }

    public void Bind_DropDown(string MST_NAME, DropDownList DDL)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                DDL.Items.Clear();
                DDL.DataSource = dt;
                DDL.DataTextField = "MST_DESC";
                DDL.DataValueField = "MST_CODE";
                DDL.DataBind();
                DDL.Items.Insert(0, new ListItem("--------------Select---------------", "0"));

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        Load_Attendance();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Attendance();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Attendance();
    }

    protected void ddlCadre_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Attendance();
    }
    private void Load_Attendance()
    {
        if (ddlBranch.SelectedValue.Trim() == "")
        {
            branch_code = "";
        }
        else
        {
            branch_code = " AND B.BRANCH_CODE='" + ddlBranch.SelectedValue.Trim() + "'";
        }
        if (ddlDepartment.SelectedValue.Trim() == "")
        {
            dept_code = "";
        }
        else
        {
            dept_code = " AND B.DEPT_CODE='" + ddlDepartment.SelectedValue.Trim() + "'";
        }
        if (ddlCadre.SelectedValue.Trim() != "" && ddlCadre.SelectedValue.Trim() != "0")
        {
            cadre_code = " AND B.CADDER_CODE='" + ddlCadre.SelectedValue.Trim() + "'";
        }
        adate = txtDate.Text.ToString();
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_ATTN_TRN.Get_Daily_Attendance(branch_code, dept_code, cadre_code, adate);
            gvReportDisplayGrid.DataSource = dt;
            gvReportDisplayGrid.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            //            lblLCount.Text = k.ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }
    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReportDisplayGrid.PageIndex = e.NewPageIndex;
            Load_Attendance();
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
                Response.Redirect("~/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Load_Attendance();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
}
