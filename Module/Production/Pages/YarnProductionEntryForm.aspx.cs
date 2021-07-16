using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Production_Pages_YarnProductionEntryForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    YarnProductionEntryForm1.TRN_TYPE = "PRD01";
    YarnProductionEntryForm1.PRODUCT_TYPE = "YARN TEXTURISING";
    YarnProductionEntryForm1.MAIN_PROCESS = "TEXTURISING";
    YarnProductionEntryForm1.PROS_CODE = "PC-2"; 
    }
}
