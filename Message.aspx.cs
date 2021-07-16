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

public partial class Module_FA_Pages_Message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Message_Text"] != null && Request.QueryString["Message_Text"] != "")
            {
                string msg = Request.QueryString["Message_Text"].ToString();
                //  Label1.Text = msg;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
            }
        }
    }
}
