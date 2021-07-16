using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_PlanningAndScheduling_Pages_ListOfOrderPlanning : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        OMPList.ORDER_TYPE = "PRODUCTION";
        OMPList.PRODUCT_TYPE = "FABRIC";
        OMPList.Header_Name = "Dyeing";
        //OMPList.TYPE = "PLANNED";
        //OMPList.TYPE = "UNPLANNED";
        OMPList.TYPE = "REMAINING";
    }
}
