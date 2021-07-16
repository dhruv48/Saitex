using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Inventory_VendorMaster_opt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGetReport_Click(object sender, EventArgs e)
    {

        string QueryString = "";
        bool flag = false;
        if (txtVendCodeRpt.Text != "")
        {
            if (flag)
                QueryString = QueryString + "";
            else
                QueryString = QueryString + "?";
            QueryString = QueryString + "PRTY_CODE=" + txtVendCodeRpt.Text;
            flag = true;
        }
        if (txtVendNameRpt.Text != "")
        {
            if (flag)
                QueryString = QueryString + "?";
            else
                QueryString = QueryString + "&";
            QueryString = QueryString + "PRTY_NAME=" + txtVendNameRpt.Text;
            flag = true;
        }

        string URL = "VendorMasterRpt.aspx" + QueryString;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

    }
}
