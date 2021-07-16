using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Admin_Pages_DeptGroupMst : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.MasterName = "DEPT_GROUP";
        Transaction_Of_Master1.FormHeading = "Department Group Master";
    }
}
