using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Fiber_Pages_Fiber_Fancy_Effect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.FormHeading = "Fiber Fancy Effect ";
        Transaction_Of_Master1.MasterName = "FIBER_FANCY_EFFECT";
        var control1 = Transaction_Of_Master1.FindControl("lblCodePrefix");
        var control2 = Transaction_Of_Master1.FindControl("txtCodePrefix");
        control1.Visible = false;
        control2.Visible = false;
        
    }
}
