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

public partial class Module_HRMS_Controls_EMP_MST_QUERY : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable DTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];          
            if (!IsPostBack)
            {
                Load_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Page Loading"));
        }
    }
    private void Load_Control()
    {
        try
        {
            tdClear.Visible = true;
            Bind_Shift();
            Bind_BrachName();
            Load_Department();
            BindDesignation();
            Bind_Employee();
            Bind_Cadder();
            Create_Data_Table();
            DDLShift.SelectedIndex = 0;            
            Load_Employee_record();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
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
            DDLDesigination.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_Cadder()
    {
        try
        {
            //////////////////////////// Bind Degination Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.Cadder_Code();
            DDLCader.DataSource = dt;
            DDLCader.DataValueField = "CADDER_CODE";
            DDLCader.DataTextField = "CADDER_CODE";
            DDLCader.DataBind();
            DDLCader.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_Shift()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftName();
            DDLShift.DataSource = dt;
            DDLShift.DataValueField = "SFT_ID";
            DDLShift.DataTextField = "SFT_NAME";
            DDLShift.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            DDLShift.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_BrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            DDLBranch.DataSource = dt;
            DDLBranch.DataValueField = "BRANCH_CODE";
            DDLBranch.DataTextField = "BRANCH_NAME";
            DDLBranch.DataBind();
            DDLBranch.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            dt.Dispose();            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void Load_Department()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            DDLDepartment.DataSource = dt;
            DDLDepartment.DataValueField = "DEPT_CODE";
            DDLDepartment.DataTextField = "DEPT_NAME";
            DDLDepartment.DataBind();
            DDLDepartment.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }   
    private void Bind_Employee()
    {
        try
        {

            DDLEmployee.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getempcode();

            if (dt != null && dt.Rows.Count > 0)
            {
                DDLEmployee.DataValueField = "EMP_CODE";
                DDLEmployee.DataTextField = "EMPLOYEENAME";
                DDLEmployee.DataSource = dt;
                DDLEmployee.DataBind();
            }
            DDLEmployee.Items.Insert(0, new ListItem("---------Select--------", string.Empty));
        }
        catch
        {
            throw;
        }
    }
    private void Create_Data_Table()
    {
        try
        {
            DTable = new DataTable();
            DTable.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));
            DTable.Columns.Add(new DataColumn("ATTN_ID", typeof(string)));
            DTable.Columns.Add(new DataColumn("ATTN_DATE", typeof(string)));
            DTable.Columns.Add(new DataColumn("SFT_ID", typeof(int)));
            DTable.Columns.Add(new DataColumn("ENTRY_TYPE", typeof(string)));
            DTable.Columns.Add(new DataColumn("IN_TIME", typeof(string)));
            DTable.Columns.Add(new DataColumn("OUT_TIME", typeof(string)));
            DTable.Columns.Add(new DataColumn("OT", typeof(string)));
            DTable.Columns.Add(new DataColumn("EARLYT", typeof(string)));
            DTable.Columns.Add(new DataColumn("TUSER", typeof(string)));

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Load_Employee_record()
    {
        string SearchQuery = string.Empty;
        try
        {
            if (DDLShift.SelectedIndex != -1)
            {
                SearchQuery = "WHERE LTRIM(RTRIM(E.DEL_STATUS))='0' ";
                if (DDLDepartment.SelectedValue.ToString() != null && DDLDepartment.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(DE.DEPT_CODE))='" + DDLDepartment.SelectedValue.ToString() + "'";
                }
                if (DDLDesigination.SelectedValue.ToString() != null && DDLDesigination.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(D.DESIG_CODE))='" + DDLDesigination.SelectedValue.Trim().ToString() + "'";
                }
                if (DDLBranch.SelectedValue.ToString() != null && DDLBranch.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(E.BRANCH_CODE))='" + DDLBranch.SelectedValue.Trim().ToString() + "'";
                }
                if (DDLEmployee.SelectedValue.ToString() != null && DDLEmployee.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(E.EMP_CODE))='" + DDLEmployee.SelectedValue.Trim().ToString() + "'";
                }
                if (DDLCader.SelectedValue.ToString() != null && DDLCader.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(E.CADDER_CODE))='" + DDLCader.SelectedValue.Trim().ToString() + "'";
                }
                SaitexDM.Common.DataModel.HR_ATTN_TRN oHR_ATTN_TRN = new SaitexDM.Common.DataModel.HR_ATTN_TRN();
                DataTable DT = new DataTable();
                DT = SaitexBL.Interface.Method.HR_ATTN_TRN.Get_Employee_Record(SearchQuery, int.Parse(DDLShift.SelectedValue.Trim().ToString()));
                              
                GvEmployee.DataSource = DT;
                GvEmployee.DataBind();
                tdClear.Visible = true;
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }     
    private void BlankControls()
    {
        try
        {
            GvEmployee.DataSource = null;
            GvEmployee.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void bindGvEmpQual(string EmpCode)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpQual(EmpCode);
            GVQualification.DataSource = dt;
            GVQualification.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvEmpLang(string EmpCode)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpLang(EmpCode);
            GVLanguage.DataSource = dt;
            GVLanguage.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void bindGvEmpFamInd(string EmpCode)
    {

        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpFamilyInd(EmpCode);
            GVFamily.DataSource = dt;
            GVFamily.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvEmpMedDTL(string EmpCode)
    {

        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpMedDTL(EmpCode);
            GVMedical.DataSource = dt;
            GVMedical.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvEmpCompInfo(string EmpCode)
    {

        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpCompInfo(EmpCode);
            GVCompInfo.DataSource = dt;
            GVCompInfo.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BlankControls();            
            tdClear.Visible = false;            
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string URL = "../pages/EmpMaster_OPT.aspx";
            string URL = "../reports/HR_EMP_MST_REPORT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void GvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GvEmployee.PageIndex = e.NewPageIndex;
            Load_Employee_record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Gridview Page Index Changes"));
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
    
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Load_Employee_record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Date Changing"));
        }
    }
    protected void GvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblEmp_Code = (Label)e.Row.FindControl("lblEmployeeCode");
                GridView gvAttendance = (GridView)e.Row.FindControl("gvATTN");
                if (e.Row.RowIndex == 0)   e.Row.Style.Add("height", "40px");
                bindGvEmpCompInfo(LblEmp_Code.Text);
                bindGvEmpFamInd(LblEmp_Code.Text);
                bindGvEmpLang(LblEmp_Code.Text);
                bindGvEmpMedDTL(LblEmp_Code.Text);
                bindGvEmpQual(LblEmp_Code.Text);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In GridView Row Data Bound"));
        }
    }


    protected void CmdViewRecord_Click(object sender, EventArgs e)
    {
        try
        {
            Load_Employee_record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Shift Selecting Index"));
        }
    }
}
