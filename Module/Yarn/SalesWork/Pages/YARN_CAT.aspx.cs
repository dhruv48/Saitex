using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Yarn_SalesWork_Pages_YARN_CAT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        yarn_cat1.FormHeading = "Yarn cat";
        yarn_cat1.MasterName = "YARN_CAT";
        var txt = yarn_cat1.FindControl("txtprefixCode");
        var lbl = yarn_cat1.FindControl("lblprefixcode");
        txt.Visible = true;
        lbl.Visible = true ;
    }
}
