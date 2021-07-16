using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Production_Pages_YarnIssueToMachineTwisting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        YarnProductionEntryForm1.TRN_TYPE = "PRD02";
        YarnProductionEntryForm1.PRODUCT_TYPE = "YARN TWISTING";
        YarnProductionEntryForm1.MAIN_PROCESS = "TWISTING";
        YarnProductionEntryForm1.PROS_CODE = "PC-3"; 
    }
}
