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


public partial class Module_FA_Queries_ChequeBookmstQueryForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScriptManager cs = Page.ClientScript;
       
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Shortcut", "document.attachEvent ('onkeyup',ShortcutKeys);", true); 
    }
}
