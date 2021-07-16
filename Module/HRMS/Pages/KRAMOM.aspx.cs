using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_HRMS_Pages_KRAMOM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.FormHeading = "Key Result Areas (KRAs)";
        Transaction_Of_Master1.MasterName = "KRA_NO";
    }
}
