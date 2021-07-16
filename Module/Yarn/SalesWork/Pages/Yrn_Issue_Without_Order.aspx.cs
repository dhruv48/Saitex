using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Yarn_SalesWork_Pages_Yrn_Issue_Without_Order : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {  
        if (Request.QueryString["PI_NO"] != null)
        {
            Issue_agnst_PA1.PI_NO = Request.QueryString["PI_NO"].ToString();
          
        }

        
    }
}
