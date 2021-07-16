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

public partial class FreeUserPage_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();

        string url = "Default.aspx";
        Response.AddHeader("REFRESH", "5;URL='" + url + "'");
        //  Common.CommonFuction.ShowMessage(@"Your Session Expired.\r\nYou will be redirected to Login page automaticallly within few seconds");

    }
}
