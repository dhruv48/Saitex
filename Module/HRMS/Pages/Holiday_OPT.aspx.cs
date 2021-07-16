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


public partial class Module_HRMS_Pages_Holiday_OPT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        string QueryString = "";
        bool flag = false;
        if (txtYear.Text.Trim() != "")
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";
            QueryString = QueryString + "year=" + txtYear.Text.Trim();
            flag = true;
        }

        if (txtDate.Text.Trim() != "")
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";
            QueryString = QueryString + "date=" + txtDate.Text.Trim();
            flag = true;
        }

        string URL = "../Reports/HolidayMasterReport.aspx" + QueryString;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);

    }
}
