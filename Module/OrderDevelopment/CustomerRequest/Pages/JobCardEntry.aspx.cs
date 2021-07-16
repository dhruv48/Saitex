using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_OrderDevelopment_CustomerRequest_Pages_JobCardEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"] != string.Empty)
        {
            JobCardEntry_Dyeing.PRODUCTTYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
        }
        else
        {
            JobCardEntry_Dyeing.PRODUCTTYPE = "YARN DYEING";
        }

    }
}
