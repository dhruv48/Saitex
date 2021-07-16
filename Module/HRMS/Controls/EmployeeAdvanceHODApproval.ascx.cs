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
using errorLog;
using Common;
public partial class Module_HRMS_Controls_EmployeeAdvanceRequestScreen_HOD_Approval_ : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void Clear()
    {
        try { 
        
        txtDesig.Text = string.Empty;
        txtDept.Text = string.Empty;
        txtposition.Text = string.Empty;
        txtlavel.Text = string.Empty;
        txtGrade.Text = string.Empty;
        txtAmtAply.Text = string.Empty;
        txtpurpose.Text = string.Empty;
        txtApproveAmtbyHOD.Text = string.Empty;
        txtHODApprovDate.Text = string.Empty;
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
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
            Clear();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
}
