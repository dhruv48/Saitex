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

public partial class Module_Yarn_SalesWork_Pages_YarnType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //YarnCategoryTransaction1. FormHeading = "Yarn Type";
        //YarnCategoryTransaction1.MasterName = "YARN_TYPE";
        //var txt=YarnCategoryTransaction1.FindControl("txtprefixCode");
        //var lbl=YarnCategoryTransaction1.FindControl("lblprefixcode");
        //txt.Visible = false;
        //lbl.Visible = false;
        Transaction_Of_Master1.FormHeading = "Dyed/Non Dyed";
        Transaction_Of_Master1.MasterName = "YARN_TYPE";
   
    }
}
