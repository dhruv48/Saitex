using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_PlanningAndScheduling_Query_LotPlaning4YS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["PRODUCT_TYPE"] != null || Request.QueryString["PRODUCT_TYPE"] != string.Empty)
        //{
           // LotPlaningQueryForm1.PRODUCTTYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
        //}
        //else
        //{
            LotPlaningQueryForm1.PRODUCTTYPE = "YARN SPINING";
        //}

    }
}
