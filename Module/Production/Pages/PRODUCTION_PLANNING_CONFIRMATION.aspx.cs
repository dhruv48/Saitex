 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Production_Pages_PRODUCTION_PLANNING_CONFIRMATION : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PRODUCT_TYPE"] != null)
        {
            ProductionIssueConfirmation.PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"];
        }

    }
}
