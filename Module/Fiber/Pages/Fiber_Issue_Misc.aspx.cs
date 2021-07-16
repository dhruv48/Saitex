using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Fiber_Pages_Fiber_Issue_Misc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PI_NO"] != null)
        {
            Fiber_Issue_Misc2.PI_NO = Request.QueryString["PI_NO"].ToString();
          
        }
    }
}
