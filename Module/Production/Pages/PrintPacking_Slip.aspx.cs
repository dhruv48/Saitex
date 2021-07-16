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
using Common;

public partial class Module_Production_Pages_PrintPacking_Slip : System.Web.UI.Page
{
    string Carton_No = "";
    string url = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCartonNo_Click(object sender, EventArgs e)
    {
        if (txtCartonNo.Text.Trim() != null && txtCartonNo.Text.Trim() != string.Empty)
        {
            Carton_No = txtCartonNo.Text.Trim();
        }
        else
        {
            Carton_No = "";
        }
        try
        {
            url = "../Report/Packing_Slip_Report.aspx?Carton_No=" + Carton_No;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));

        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCartonNo.Text.Trim() != null && txtCartonNo.Text.Trim() != string.Empty)
        {
            Carton_No = txtCartonNo.Text.Trim();
        }
        else
        {
            Carton_No = "";
        }
        try
        {
         url = "../Report/Packing_Slip_Report.aspx?Carton_No=" + Carton_No;
       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
        
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
}
