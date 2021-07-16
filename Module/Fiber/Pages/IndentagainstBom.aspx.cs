using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
public partial class Module_Fiber_Pages_IndentagainstBom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["ORDER_NO"]!=null)
        {
           FiberIndentBom1.ORDER_NO =  Request.QueryString["ORDER_NO"].ToString();
        }
        if (Request.QueryString["PI_NO"] != null)
        {
           FiberIndentBom1.PI_NO = Request.QueryString["PI_NO"].ToString();
        }

    }
}
