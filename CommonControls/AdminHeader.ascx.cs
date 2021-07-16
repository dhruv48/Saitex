using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CommonControls_AdminHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetLogInDetail();
    }
    private void GetLogInDetail()
    {
        try
        {
            if (Session["LoginDetail"] == null)
                Response.Redirect("~/Logout.aspx");
            else
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                lblUserName.Text = oUserLoginDetail.Username;
                logBranch.Text = oUserLoginDetail.VC_BRANCHNAME;
                logCompany.Text = oUserLoginDetail.VC_COMPANYNAME;
                logDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;
                int EndYear = int.Parse(oUserLoginDetail.DT_STARTDATE.Year.ToString()) + 1;
                logFinYear.Text = oUserLoginDetail.DT_STARTDATE.Year.ToString() + "-" + EndYear.ToString();
                LogInDate.Text = oUserLoginDetail.DT_LOGINDATE.ToShortDateString();
                logInTime.Text = oUserLoginDetail.DT_LOGINTIME;
                Image1.ImageUrl = oUserLoginDetail.COMP_LOGO;
                //------------------------------------------------
                LblOpeningMonth.Text = oUserLoginDetail.OPEN_MONTH;
                LblOpeningYear.Text = oUserLoginDetail.OPEN_YEAR;
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

     }
    protected void lbtnLogOut_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Logout.aspx", false);
    }
    protected void lbtnChangeCOBRANCH_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GetUserAuthorisation.aspx", false);
    }
    protected void lbtnDashBoard_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
        }
        catch
        {

        }
    }
    protected void lbtnSearchPeople_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/Admin/Pages/SearchPeople.aspx", false);
        }
        catch
        {

        }
    }
}
