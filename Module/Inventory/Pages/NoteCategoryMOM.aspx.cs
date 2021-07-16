using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Inventory_Pages_NoteCategoryMOM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.FormHeading = "Dr/Cr Note Category";
        Transaction_Of_Master1.MasterName = "NOTE_CATEGORY";
    }
}
