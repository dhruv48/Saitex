using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Yarn_SalesWork_Pages_Yarn_Process : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.FormHeading = "Yarn Process";
        Transaction_Of_Master1.MasterName = "YARN_PROCESS";
    }
}
