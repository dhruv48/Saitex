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

public partial class Module_Yarn_SalesWork_Pages_Yarn_Trn_Quality : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        YarnQualityTransaction1.FormHeading = "Yarn Quality";
        YarnQualityTransaction1.MasterName = "YARN_QUALITY";

    }
}
