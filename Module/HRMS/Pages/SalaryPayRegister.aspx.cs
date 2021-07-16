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
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Pages_SalaryPayRegister : System.Web.UI.Page
{
    string strCompanyCode = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
   protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string strCompanyCode = oUserLoginDetail.COMP_CODE;
            if (!Page.IsPostBack)
            {
                fillYear();
                bindddlBrachName();
                getEmployeeDepartment();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page loading"));
        }
    }
    private void bindddlBrachName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE );
            ddlBranch.DataSource = dt;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("------------Select-----------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getEmployeeDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-------------Select-------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void fillYear()
    {
        for (int i = -2; i < 2; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        ddlYear.Items.Insert(0, new ListItem("----------Select----------", ""));
        ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string Month = "", Year = "",str="";
        if (ddlMonth.SelectedValue.Trim() != "" && ddlMonth.SelectedValue.Trim() != "0")
        {
            Month = ddlMonth.SelectedValue.Trim().ToString();
        }
        else if (Convert.ToString(oUserLoginDetail.OPEN_MONTH_NO) != null )
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
        else if (Convert.ToString(oUserLoginDetail.OPEN_YEAR) != null)
        {
            Year = oUserLoginDetail.OPEN_YEAR.ToString();
        }
        else
        {
            Year = System.DateTime.Now.Year.ToString();
        }
        if (ddlDepartment.SelectedValue.Trim() != "" && ddlDepartment.SelectedValue.Trim() != "0")
        {
            str = str + "&DEPT_CODE='" + ddlDepartment.SelectedValue.Trim().ToString() + "'";
        }
        if (DDLCader.SelectedValue.Trim() != "" && DDLCader.SelectedValue.Trim() != "0")
        {
            str = str + "&CADDER_CODE='" + DDLCader.SelectedValue.Trim().ToString() + "'";
        }
        if (ddlYear.SelectedValue != "" && ddlYear.SelectedValue != null)
        {
            Response.Redirect("../../HRMS/Reports/PrintPayRegister.aspx?Year=" + Year  + "&MONTH='" + Month + "'"+ str );
        }
        else
        {
            Common.CommonFuction.ShowMessage("Please Select Year");
        }
    }
}
