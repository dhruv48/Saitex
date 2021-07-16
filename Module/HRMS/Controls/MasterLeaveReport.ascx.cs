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
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_MasterLeaveReport : System.Web.UI.UserControl
{
    private static string strCompanyCode = string.Empty;
    private static string strBranchCode = string.Empty;
    private static string SearchTable = string.Empty;
    private static string SearchDate = string.Empty;
    private static string LV_ID = string.Empty;
    private static string ReportName = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        strCompanyCode = oUserLoginDetail.COMP_CODE;
        strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
        if (!Page.IsPostBack)
        {
            Initial_Control();
        }
    }
    private void Initial_Control()
    {
        fillYear();
        bindddlBrachName();
        getEmployeeDepartment();
        bindEmpCode();
        BindDesignation();
        BindShift();
    }
    private void bindddlBrachName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            DDLBranch.DataSource = dt;
            DDLBranch.DataValueField = "BRANCH_CODE";
            DDLBranch.DataTextField = "BRANCH_NAME";
            DDLBranch.DataBind();
            DDLBranch.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbEmpCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10);
        cmbEmpCode.Items.Clear();
        cmbEmpCode.DataSource = data;
        cmbEmpCode.DataBind();
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        e.ItemsCount = GetItemsCount(e.Text);
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE EMP_CODE like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0' or F_NAME like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY EMP_CODE";
        string commandText = "SELECT EMP_CODE,F_NAME, F_NAME ||' '||M_NAME||' '||L_NAME AS EMPLOYEENAME FROM HR_EMP_MST ";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', strCompanyCode, strBranchCode, sPO);
        return dt;

    }
    protected int GetItemsCount(string text)
    {

        string CommandText = "SELECT COUNT(*) FROM hr_emp_mst WHERE EMP_CODE like :SearchQuery And DEL_STATUS = '0' or F_NAME like :SearchQuery And DEL_STATUS = '0'";
        return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text + '%', strCompanyCode, strBranchCode, "");

    }
    private void bindEmpCode()
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems("", 0, 10);
            cmbEmpCode.DataSource = data;
            cmbEmpCode.DataBind();
        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    private void getEmployeeDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            DDLDept.DataSource = dt;
            DDLDept.DataValueField = "DEPT_CODE";
            DDLDept.DataTextField = "DEPT_NAME";
            DDLDept.DataBind();
            DDLDept.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void fillYear()
    {
        for (int i = -2; i < 15; i++)
        {
            DDLYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        DDLYear.Items.Insert(0, new ListItem("------------SELECT------------", ""));
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
    protected void CMDPrint_Click(object sender, EventArgs e)
    {
        string SQuery = string.Empty;
        SQuery = "TA.DEL_STATUS='0'";
        if(LV_ID!="" && LV_ID!=string.Empty)
        {
            SQuery=SQuery+ "AND LV_ID='"+ LV_ID +"'";
        }
        if (cmbEmpCode.SelectedIndex != 0 && cmbEmpCode.SelectedValue != "" && cmbEmpCode.SelectedValue != null)
        {
            SQuery = SQuery + " AND E.EMP_CODE='" + cmbEmpCode.SelectedValue.Trim().ToString() + "'";
        }
        if (DDLDept.SelectedIndex != 0 && DDLDept.SelectedValue != "" && DDLDept.SelectedValue != null)
        {
            SQuery = SQuery + " AND E.DEPT_CODE='" + DDLDept.SelectedValue.Trim().ToString() + "'";
        }
        if (DDLBranch.SelectedIndex != 0 && DDLBranch.SelectedValue != "" && DDLBranch.SelectedValue != null)
        {
            SQuery = SQuery + " AND E.BRANCH_CODE='" + DDLBranch.SelectedValue.Trim().ToString() + "'";
        }
        if (DDLTable.SelectedValue != "DW")
        {
            if (DDLMonth.SelectedIndex != 0 && DDLMonth.SelectedValue != "" && DDLMonth.SelectedValue != null)
            {
                SQuery = SQuery + " AND TO_CHAR(TA." + SearchDate + ",'MM')='" + DDLMonth.SelectedValue.Trim().ToString() + "'";
            }
            if (DDLYear.SelectedIndex != 0 && DDLYear.SelectedValue != "" && DDLYear.SelectedValue != null)
            {
                SQuery = SQuery + " AND TO_CHAR(TA." + SearchDate + ",'YYYY')='" + DDLYear.SelectedValue.Trim().ToString() + "'";
            }
            if (TxtFromDate.Text != "" && TxtToDate.Text != "")
            {
                SQuery = SQuery + " AND TA." + SearchDate + " BETWEEN To_Date ('" + TxtFromDate.Text.Trim() + "', 'dd/MM/yyyy') AND To_Date ('" + TxtToDate.Text.Trim() + "','dd/MM/yyyy')";
            }
            else if (TxtFromDate.Text != "" && TxtToDate.Text == "")
            {
                SQuery = SQuery + " AND TA." + SearchDate + " > To_Date ('" + TxtFromDate.Text.Trim() + "', 'dd/MM/yyyy')";
            }
            else if (TxtFromDate.Text == "" && TxtToDate.Text != "")
            {
                SQuery = SQuery + " AND TA." + SearchDate + " < To_Date ('" + TxtFromDate.Text.Trim() + "', 'dd/MM/yyyy')";
            }
        }        
        Assign_Reports(SQuery);

    }
    protected void DDLTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLTable.SelectedValue != "" && DDLTable.SelectedValue != null)
            {
                SearchTable = "";
                if (DDLTable.SelectedValue == "CL")
                {
                    SearchTable = "HR_LV_APP_FORM_DTL";
                    SearchDate = "LV_FROM_DATE";
                    ReportName = "DailyCasualLiveReport";
                    LV_ID = "1";
                }
                else if (DDLTable.SelectedValue == "EL")
                {
                    SearchTable = "HR_LV_APP_FORM_DTL";
                    SearchDate = "LV_FROM_DATE";
                    ReportName = "DailyCasualLiveReport";
                    LV_ID = "2";
                }
                else if (DDLTable.SelectedValue == "SL")
                {
                    SearchTable = "HR_LV_APP_FORM_DTL";
                    SearchDate = "LV_FROM_DATE";
                    ReportName = "DailyCasualLiveReport";
                    LV_ID = "3";
                }
                else if (DDLTable.SelectedValue == "ML")
                {
                    SearchTable = "HR_LV_APP_FORM_DTL";
                    SearchDate = "LV_FROM_DATE";
                    ReportName = "DailyCasualLiveReport";
                    LV_ID = "4";
                }    
                else if (DDLTable.SelectedValue == "LW")
                {
                    SearchTable = "HR_LV_APP_FORM_DTL";
                    SearchDate = "LV_FROM_DATE";
                    ReportName = "DailyCasualLiveReport";
                    LV_ID = "7";
                }
                else if (DDLTable.SelectedValue == "CO")
                {
                    SearchTable = "HR_LV_APP_FORM_DTL";
                    SearchDate = "LV_FROM_DATE";
                    ReportName = "DailyCasualLiveReport";
                    LV_ID = "9";
                }
                else if (DDLTable.SelectedValue == "DW")
                {
                    SearchTable = "v_leave_detail";
                    ReportName = "Leavereportdepttwish";
                }
                else if (DDLTable.SelectedValue == "O")
                {
                    SearchTable = "EMPOUTDOORDUTY";
                    SearchDate = "FROM_DATE";
                    ReportName = "OutDoorDutyReport";
                }

            }
            else
            {
                CommonFuction.ShowMessage("Please Select Reports");
                DDLTable.Focus();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem In Selecting.\r\nSee error log for detail."));
        }
    }
    protected void Assign_Reports(string SQuery)
    {
        try
        {
            Response.Redirect("~/Module/HRMS/Reports/MasterLeave.aspx?TName=" + SearchTable + "&SQuery=" + SQuery + "&RName=" + ReportName + "&LV_ID=" + LV_ID + "", false);
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    
    private void BindDesignation()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            DDLDesign.DataSource = dt;
            DDLDesign.DataValueField = "desig_Code";
            DDLDesign.DataTextField = "desig_Name";
            DDLDesign.DataBind();
            DDLDesign.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }

    }
    private void BindShift()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getgetEmployeeShift();
            DDLShift.DataSource = dt;
            DDLShift.DataValueField = "sft_Id";
            DDLShift.DataTextField = "sft_Name";
            DDLShift.DataBind();
            DDLShift.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }    
}
