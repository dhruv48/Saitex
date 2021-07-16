using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using errorLog;
using Common;
using DBLibrary;

public partial class Module_Admin_Pages_SalarySlip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_Employee();
        }
        
    }
    private void Load_Employee()
    {
        DataTable Dt = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info();
        DDLEmployee.DataSource = Dt;
        DDLEmployee.DataValueField = "EMP_CODE";
        DDLEmployee.DataTextField = "EMPLOYEENAME";       
        DDLEmployee.DataBind();
        DDLEmployee.Items.Insert(0, "------Select-------");

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string Filter_Condition = string.Empty ;
        try
        {
            if (DDLEmployee.SelectedIndex  != 0 )
            {
                Filter_Condition ="EmpCode="+ DDLEmployee.SelectedValue.ToString();
            }
            if(ddlBranch.SelectedValue != "" && ddlBranch.SelectedValue != null)
            {
                if (Filter_Condition.Trim().ToString() != "")
                {
                    Filter_Condition = Filter_Condition + "&BranchID=" + ddlBranch.SelectedValue.ToString();
                }
                else
                {
                    Filter_Condition = "BranchID=" + ddlBranch.SelectedValue.ToString();
                }
            }
            if (ddlDepartment.SelectedValue != "" && ddlDepartment.SelectedValue != null)
            {
                if (Filter_Condition.Trim().ToString() != "")
                {
                    Filter_Condition = Filter_Condition + "&DeptId=" + ddlDepartment.SelectedValue.ToString();
                }
                else
                {
                    Filter_Condition = "DeptId=" + ddlBranch.SelectedValue.ToString();
                }
                
            }
            if (ddlMonth.SelectedValue != "" && ddlMonth.SelectedValue != null)
            {
                if (Filter_Condition.Trim().ToString() != "")
                {
                    Filter_Condition = Filter_Condition + "&Month=" + ddlMonth.SelectedValue.ToString();
                }
                else
                {
                    Filter_Condition = "Month=" + ddlBranch.SelectedValue.ToString();
                }
               
            }
            if (ddlYear.SelectedValue != "" && ddlYear.SelectedValue != null)
            {
                if (Filter_Condition.Trim().ToString() != "")
                {
                    Filter_Condition = Filter_Condition + "&Year=" + ddlYear.SelectedValue.ToString();
                }
                else
                {
                    Filter_Condition = "Year=" + ddlBranch.SelectedValue.ToString();
                }              
               
            }
            else
            {
                if (Filter_Condition.Trim().ToString() != "")
                {
                    Filter_Condition = Filter_Condition + "&Year=" + System.DateTime.Now.Year;
                }
                else
                {
                    Filter_Condition = "Year=" + System.DateTime.Now.Year;
                }  
                
            }
            Response.Redirect("PrintSSlip.aspx?" + Filter_Condition.ToString());
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }        
    }
}
