using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Module_Waste_Reports_Waste_Reciving : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
            {
                ReceiptPermRPT1.TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
                
                if(Request.QueryString["TRN_TYPE1"]!=null)
                {
                 ReceiptPermRPT1.TRN_TYPE1 = Request.QueryString["TRN_TYPE1"].ToString();
                }
            }
            else
            {
                ReceiptPermRPT1.TRN_TYPE = "RCR";
            }
        }
    }
}
