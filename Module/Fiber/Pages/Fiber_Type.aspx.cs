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

public partial class Module_Fiber_Pages_Fiber_Type : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.FormHeading = "Poy Category";
        Transaction_Of_Master1.MasterName = "FIBER_MASTER";
        var txt = Transaction_Of_Master1.FindControl("txtCodePrefix");
        var lbl = Transaction_Of_Master1.FindControl("lblCodePrefix");
        txt.Visible = true;
        lbl.Visible = true;
        
    }
}
