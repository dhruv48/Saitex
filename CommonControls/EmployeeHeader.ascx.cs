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


public partial class CommonControls_EmployeeHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetLogInDetail();

    }   
    private void GetLogInDetail()
    {
        try
        {
            DataTable dtLoginEmpDetail = SaitexDL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(Session["EmpCode"].ToString());
            if (dtLoginEmpDetail != null && dtLoginEmpDetail.Rows.Count > 0)
            {
                tdChangePwd.Visible = true;
                lblUserName.Text = dtLoginEmpDetail.Rows[0]["EMPLOYEENAME"].ToString();
                logBranch.Text = dtLoginEmpDetail.Rows[0]["BRANCH_NAME"].ToString();
                //logCompany.Text = dtLoginEmpDetail.Rows[0]["VC_COMPANYNAME"].ToString();
                logDepartment.Text = dtLoginEmpDetail.Rows[0]["DEPT_NAME"].ToString();
                logFinYear.Text = "2010-2011";
                LogInDate.Text = DateTime.Now.ToString("DD/MM/YYYY");
            }
            else
            {
                tdChangePwd.Visible = false;
            }
        }
        catch 
        {
            throw;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string LocalPath = Request.Url.LocalPath;
       
    } 
    protected void lbtnChangeCOBRANCH_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GetUserAuthorisation.aspx", false);
    }
    protected void lbtnLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Module/HRMS/Default.aspx");
    }
    protected void LbtChangePassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Module/HRMS/Pages/ChangePassword.aspx", false);
    }
}
