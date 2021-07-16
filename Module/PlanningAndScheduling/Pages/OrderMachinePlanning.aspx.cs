using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_PlanningAndScheduling_Pages_OrderMachinePlanning : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //OMP.ORDER_TYPE = "KNITTING";
        //OMP.PRODUCT_TYPE = "FABRIC";
        //OMP.Header_Name = "Knitting";
        if (Request.QueryString["ORDER_NO"] != null)
        {
            OMP.ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
        }
        if (Request.QueryString["PI_TYPE"] != null)
        {
            OMP.ORDER_TYPE = Request.QueryString["PI_TYPE"].ToString();
        }
        else
        {
            OMP.ORDER_TYPE = "YARN";
        }
        if (Request.QueryString["ARTICAL_TYPE"] != null)
        {

            OMP.Header_Name = OMP.PRODUCT_TYPE = Request.QueryString["ARTICAL_TYPE"].ToString();
        }
        else
        {
         
            OMP.PRODUCT_TYPE = "TEXTURISED";
            OMP.Header_Name = "Texturising";
        }
        if (Request.QueryString["PI_NO"] != null)
        {
            OMP.PI_NO = Request.QueryString["PI_NO"].ToString();            
        }
        if (Request.QueryString["ARTICAL_CODE"] != null)
        {
            OMP.ARTICAL_CODE = Request.QueryString["ARTICAL_CODE"].ToString();
        }
        if (Request.QueryString["SHADE_CODE"] != null)
        {
            OMP.SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
        }
        if (Request.QueryString["PROS_ROUTE_CODE"] != null)
        {
            OMP.PROS_ROUTE_CODE = Request.QueryString["PROS_ROUTE_CODE"].ToString();
        }
        if (Request.QueryString["YEAR"] != null)
        {
            OMP.YEAR = Request.QueryString["YEAR"].ToString();
        }
        if (Request.QueryString["BRANCH_CODE"] != null)
        {
            OMP.BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
        }
       

    }
}
