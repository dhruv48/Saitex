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

public partial class Module_HRMS_Queries_EmpSlryDtlFilter : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!Page.IsPostBack)
            {
                bindddlBrachName();
                getEmployeeDepartment();
                BindDesignation();
                BindCmbEmp();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    
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
            DDLBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
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
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            DDLDept.DataSource = dt;
            DDLDept.DataValueField = "DEPT_CODE";
            DDLDept.DataTextField = "DEPT_NAME";
            DDLDept.DataBind();
            DDLDept.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
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
            DDLDesign.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void BindCmbEmp()
    {
        try
        {
            ddlemp.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlemp.DataValueField = "EMP_CODE";
                ddlemp.DataTextField = "EMPLOYEENAME";
                ddlemp.DataSource = dt;
                ddlemp.DataBind();
                ddlemp.Items.Insert(0, new ListItem("---------------All---------------", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void CMDPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "EMP_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DEPT_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DESIG_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BRANCH_CODE";
            myDataTable.Columns.Add(myDataColumn);

            DataRow row;

            row = myDataTable.NewRow();
            row["EMP_CODE"] = ddlemp.SelectedValue.ToString(); ;
            row["DEPT_CODE"] = DDLDept.SelectedValue.ToString();
            row["DESIG_CODE"] = DDLDesign.SelectedValue.ToString();
            row["BRANCH_CODE"] = DDLBranch.SelectedValue.ToString();
            myDataTable.Rows.Add(row);
            Session["promotiondt"] = myDataTable;

            string URL = "../Reports/EmployeeSalaryDetailReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlemp.SelectedIndex = 0;
            DDLBranch.SelectedIndex = 0;
            DDLDept.SelectedIndex = 0;
            DDLDesign.SelectedIndex = 0;
        }
        catch
        {
            throw;
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
                Response.Redirect("~/Module/HRMS/Pages/PromotionIncrement.aspx", false);

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }      
    }
}
