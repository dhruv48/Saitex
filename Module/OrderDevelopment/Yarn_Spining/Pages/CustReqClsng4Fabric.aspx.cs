using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_OrderDevelopment_CustomerRequest_Pages_CustReqClsng4Fabric : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        CustReqClsng4Fabric1.PRODUCT_TYPE1 = Request.QueryString["PRODUCT_TYPE"].ToString();
    }
}
