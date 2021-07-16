using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Module_Inventory_Pages_ITEM_CAT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.FormHeading = "Item Category";
        Transaction_Of_Master1.MasterName = "ITEM_CATG";
    }
}
