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

public partial class Module_OrderDevelopment_LabDip_Pages_Depth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Transaction_Of_Master1.MasterName = "YARN_DEPTH";
        Transaction_Of_Master1.FormHeading = "DEPTH";
    }
}
