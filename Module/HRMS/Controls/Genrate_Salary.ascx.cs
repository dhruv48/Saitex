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
using System.Data.OracleClient;
using errorLog;
using Common;
using DBLibrary;

public partial class Module_HRMS_Controls_Genrate_Salary : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                btnSalaryGenerate.Attributes.Add("onClick", "ShowModalPopup('pload');");
                if (Convert.ToString(oUserLoginDetail.OPEN_MONTH) != null && Convert.ToString(oUserLoginDetail.OPEN_YEAR) != null)
                {
                    TxtSalMonth.Text = oUserLoginDetail.OPEN_MONTH.ToString();
                    TxtSalYear.Text = oUserLoginDetail.OPEN_YEAR.ToString();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("No Open Year Or open Month,Please Open Year or Month");
                }
                bindddlBrachName();
                getEmployeeDepartment();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
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
    private void getEmployeeDepartment()
    {
        try
        {
            ////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            //DataView DV = new DataView(dt);
            //if (oUserLoginDetail.VC_DEPARTMENTNAME.Trim().ToString().ToUpper() != "ADMIN")
            //{
            //    DV.RowFilter = "DEPT_CODE='" + oUserLoginDetail.VC_DEPARTMENTCODE.Trim().ToString() + "'";
            //    ddlDepartment.DataSource = DV;
            //}
            //else
            //{
            ddlDepartment.DataSource = dt;
            //}
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            //if (oUserLoginDetail.VC_DEPARTMENTNAME.Trim().ToString().ToUpper() != "ADMIN")
            //{
            //    ddlDepartment.Items.Insert(0, new ListItem("-------------Select------------", ""));
            //}
            //else
            //{
            ddlDepartment.Items.Insert(0, new ListItem("-----------All-----------", ""));
            //}
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
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
            ddlBranch.Items.Insert(0, new ListItem("-------------ALL------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch
        {
            throw;
        }
    }
    protected void btnSalaryGenerate_Click(object sender, EventArgs e)
    {
        try
        {
           
            bool Result = false;
            string strCompanyCode = oUserLoginDetail.COMP_CODE;
            string StrQuery = "";
            StrQuery = " ltrim(rtrim(E.COMP_CODE))='" + strCompanyCode + "' AND ltrim(rtrim(E.DEL_STATUS))='0'";
            if (ddlBranch.SelectedValue.Trim() != "" && ddlBranch.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + "AND ltrim(rtrim(E.branch_code))='" + ddlBranch.SelectedValue.Trim() + "'";
            }
            if (ddlEmployee.SelectedValue.Trim() != "" && ddlEmployee.SelectedValue.Trim() != null)
            {
                StrQuery = StrQuery + "AND ltrim(rtrim(E.EMP_CODE))='" + ddlEmployee.SelectedValue.Trim() + "'";
            }
            if (DDLCader.SelectedValue.Trim() != "" && DDLCader.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + "AND ltrim(rtrim(E.CADDER_CODE))='" + DDLCader.SelectedValue.Trim() + "'";
            }
            if (ddlDepartment.SelectedValue.Trim() != "" && ddlDepartment.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + "AND ltrim(rtrim(E.DEPT_CODE))='" + ddlDepartment.SelectedValue.Trim() + "'";
            }
            System.Threading.Thread.Sleep(2000);
            Result = SaitexBL.Interface.Method.HR_SAL_SLIP_MST.Employee_Salary(StrQuery, DateTime.Parse(oUserLoginDetail.SALARY_FROMDATE), DateTime.Parse(oUserLoginDetail.SALARY_TODATE), oUserLoginDetail.OPEN_MONTH_NO.ToString(), oUserLoginDetail.UserCode.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            modalPopup.Hide();
            if (Result)
            {
                Response.Redirect("/Saitex/Module/HRMS/Pages/GridDispayofSalary.aspx?cId=S", false);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Salary Genrate for the Month " + TxtSalMonth.Text.Trim().ToUpper().ToString() + " Faild!\r\nPlz Check Approve Attendance Record");
            }
        }
        catch (Exception ex)
        {
            modalPopup.Hide();
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
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
            ddlBranch.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
}
