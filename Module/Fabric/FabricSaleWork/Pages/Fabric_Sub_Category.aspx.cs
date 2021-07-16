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

public partial class Module_Fabric_FabricSaleWork_Pages_Fabric_Sub_Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FabricSubCategory1.FormHeading = "Fabric Sub Category";
        FabricSubCategory1.MasterName = "FABRIC_SUB_CATEGORY";
        //var txt = FabricSubCategory1.FindControl("txtprefixCode");
        //var lbl = FabricSubCategory1.FindControl("lblprefixcode");
        //txt.Visible = true;
        //lbl.Visible = true;    


    }
}
