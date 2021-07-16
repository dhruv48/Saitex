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

public partial class Module_Production_Controls_Dyeing_Pending : System.Web.UI.UserControl
{
    private static DateTime Sdate;
    private static DateTime Edate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Sdate = oUserLoginDetail.DT_STARTDATE;
                Edate = Common.CommonFuction.GetYearEndDate(Sdate);
                txtFROMDATE.Text = (Sdate.ToShortDateString()).ToString();
                txtTODATE.Text = (Edate.ToShortDateString()).ToString();
            }
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = "";
                //string From = "";
                //string To = "";
                if (txtFROMDATE.Text.Trim() == "")
                    msg += "Enter the From Date.</ br>";
                if (txtTODATE.Text.Trim() == "")
                    msg += "Enter the To Date</ br>";
                if (msg == "")
                {
                    string QueryString = "";
                    QueryString += "?FromNo=" + txtFROMDATE.Text.Trim();
                    QueryString += "&ToNo=" + txtTODATE.Text.Trim();
                    string URL = "../Report/Dyeing_Pending_Print_Report.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                    Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch
        {

        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
}