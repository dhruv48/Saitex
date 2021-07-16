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


public partial class CommonMaster_UserMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            Page.Title = "Textiles Application Management System (TexAMS)";
            SetEmpLogin();

        }
        catch (Exception ex)
        {
            // errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void SetEmpLogin()
    {
        //string LocalPath = Request.Url.LocalPath;

        //if (LocalPath.Contains("HRMS"))
        //    lnkbtnEmpLogin.Visible = false;
        //else
        //    lnkbtnEmpLogin.Visible = true;
    }
}

   

