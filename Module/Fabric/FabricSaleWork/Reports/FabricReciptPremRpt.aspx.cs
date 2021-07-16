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

public partial class Module_Inventory_Reports_FabricREciptPremRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
            {
                
                FabricReciptPremRpt1.TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();            
            
            }
            else
            {
                FabricReciptPremRpt1.TRN_TYPE = "RCR";
              
            }
        }
    }
}
