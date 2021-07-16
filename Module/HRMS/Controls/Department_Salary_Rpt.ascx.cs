using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_Department_Salary_Rpt : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                fillYear();
                bindddlBrachName();
                getEmployeeDepartment();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    
    private void bindddlBrachName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
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
        catch
        {
            throw;
        }
    }
    private void getEmployeeDepartment()
    {
        try
        {
            ////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---------------Select---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }

    }
    private void fillYear()
    {
        try
        {
            for (int i = -1; i < 1; i++)
            {
                ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
            }
            ddlYear.Items.Insert(0, new ListItem("---Select---", ""));
            ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
        }
        catch
        {
            throw;
        }
    } 
    
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string SearchQuery = string.Empty;
        string Search = string.Empty;
        try
        {
            if (ddlYear.SelectedValue.Trim().ToString() != "0" && ddlYear.SelectedValue.Trim().ToString() != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.SAL_YEAR='" + ddlYear.SelectedValue.Trim().ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.SAL_YEAR='" + ddlYear.SelectedValue.Trim().ToString() + "'";
                }
            }
            else if (oUserLoginDetail.OPEN_YEAR != null && oUserLoginDetail.OPEN_YEAR != "")
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.SAL_YEAR='" + oUserLoginDetail.OPEN_YEAR.ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.SAL_YEAR='" + oUserLoginDetail.OPEN_YEAR.ToString() + "'";
                }
            }
            else
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.SAL_YEAR='" + System.DateTime.Now.Year + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.SAL_YEAR='" + System.DateTime.Now.Year + "'";
                }
            }
            if (ddlMonth.SelectedValue.Trim().ToString() != "" && ddlMonth.SelectedValue.Trim().ToString() != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.MONTHNO=" + ddlMonth.SelectedValue.Trim().ToString().ToString();
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.MONTHNO=" + ddlMonth.SelectedValue.Trim().ToString().ToString();
                }
            }
            else if (oUserLoginDetail.OPEN_YEAR != null && oUserLoginDetail.OPEN_YEAR != "")
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.MONTHNO='" + oUserLoginDetail.OPEN_MONTH_NO.ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.MONTHNO='" + oUserLoginDetail.OPEN_MONTH_NO.ToString() + "'";
                }
            }
            else
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.MONTHNO='" + System.DateTime.Now.Month + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.MONTHNO='" + System.DateTime.Now.Month + "'";
                }
            }
            if (ddlBranch.SelectedValue.Trim().ToString() != "" && ddlBranch.SelectedValue.Trim().ToString() != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.BRANCH_CODE='" + ddlBranch.SelectedValue.Trim().ToString().ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.BRANCH_CODE='" + ddlBranch.SelectedValue.Trim().ToString().ToString() + "'";
                }
            }
            Search = SearchQuery;
            if (ddlDepartment.SelectedValue.Trim().ToString() != "" && ddlDepartment.SelectedValue.Trim().ToString() != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.DEPT_CODE='" + ddlDepartment.SelectedValue.Trim().ToString().ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.DEPT_CODE='" + ddlDepartment.SelectedValue.Trim().ToString().ToString() + "'";
                }
            }
            if (DDLCader.SelectedValue.Trim().ToString() != "" && DDLCader.SelectedValue.Trim().ToString() != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " S.CADDER_CODE='" + DDLCader.SelectedValue.Trim().ToString().ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND S.CADDER_CODE='" + DDLCader.SelectedValue.Trim().ToString().ToString() + "'";
                }
            }
            Response.Redirect("../../HRMS/Reports/Department_Wise_Month_Salary.aspx?Search=" + SearchQuery.ToString()+"&SearchNew="+ Search );

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlMonth.SelectedIndex = -1;
            ddlYear.SelectedIndex = -1;
            ddlBranch.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = -1;
           
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
}