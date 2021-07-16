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

public partial class Module_HRMS_Controls_Monthly_Absent_Report : System.Web.UI.UserControl
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
                BindDesignation();
                bindddlBrachName();
                getEmployeeDepartment();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    private void BindDesignation()
    {
        try
        {
            //////////////////////////// Bind Degination Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            DDLDesigination.DataSource = dt;
            DDLDesigination.DataValueField = "desig_Code";
            DDLDesigination.DataTextField = "desig_Name";
            DDLDesigination.DataBind();
            DDLDesigination.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
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
            ddlBranch.Items.Insert(0, new ListItem("-------------Select------------", ""));
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
    protected void ddlEmployee_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            ddlEmployee.Items.Clear();
            ddlEmployee.DataSource = data;
            ddlEmployee.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string sPO = "";

            string commandText = "SELECT * FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause = " and emp_code not in(SELECT emp_code FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' AND ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " ORDER BY EMP_CODE";
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, SortExpression, "", text.ToUpper() + '%', oUserLoginDetail.COMP_CODE.ToString(), string.Empty, sPO);
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT count(*) FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE EMP_CODE LIKE :SearchQuery OR F_NAME LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' ";
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text.ToUpper() + '%', oUserLoginDetail.COMP_CODE.ToString(), string.Empty, "");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string Month = "", Year = "", SEARCH = "";
            if (ddlMonth.SelectedValue.Trim() != "" && ddlMonth.SelectedValue.Trim() != "0")
            {
                Month = ddlMonth.SelectedValue.Trim().ToString();
            }
            else if (oUserLoginDetail.OPEN_MONTH_NO.ToString() != "")
            {
                Month = oUserLoginDetail.OPEN_MONTH_NO.ToString();
            }
            else
            {
                Month = System.DateTime.Now.Month.ToString();
            }
            if (ddlYear.SelectedValue.Trim() != "" && ddlYear.SelectedValue.Trim() != "0")
            {
                Year = ddlYear.SelectedValue.Trim().ToString();
            }
            else if (oUserLoginDetail.OPEN_MONTH_NO.ToString() != "")
            {
                Year = oUserLoginDetail.OPEN_YEAR.ToString();
            }
            else
            {
                Year = System.DateTime.Now.Year.ToString();
            }
            SEARCH = SEARCH + " WHERE MON='" + Month + "' AND AYEAR='" + Year + "'";
            if (ddlBranch.SelectedValue.Trim().ToString() != "" && ddlBranch.SelectedValue.Trim().ToString() != "0")
            {
                SEARCH = SEARCH + " AND BRANCH_CODE='" + ddlBranch.SelectedValue.Trim().ToString() + "'";
            }
            if (ddlDepartment.SelectedValue.Trim().ToString() != "" && ddlDepartment.SelectedValue.Trim().ToString() != "0")
            {
                SEARCH = SEARCH + " AND DEPT_CODE='" + ddlDepartment.SelectedValue.Trim().ToString() + "'";
            }
            if (ddlEmployee.SelectedValue.Trim().ToString() != "" && ddlEmployee.SelectedValue.Trim().ToString() != "0")
            {
                SEARCH = SEARCH + " AND EMP_CODE='" + ddlEmployee.SelectedValue.Trim().ToString() + "'";
            }
            if (DDLCader.SelectedValue.Trim().ToString() != "" && DDLCader.SelectedValue.Trim().ToString() != "0")
            {
                SEARCH = SEARCH + " AND CADDER_CODE='" + DDLCader.SelectedValue.Trim().ToString() + "'";
            }
            Response.Redirect("../../HRMS/Reports/AttendanceReport.aspx?Search=" + SEARCH);

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
            ddlEmployee.SelectedIndex = -1;
            DDLDesigination.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
}
