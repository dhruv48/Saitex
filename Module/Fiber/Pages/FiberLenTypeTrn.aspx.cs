using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Fiber_Pages_FiberLenTypeTrn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.FormHeading = "Denier";
        Transaction_Of_Master1.MasterName = "LENGTH_TYPE";
    }
}
