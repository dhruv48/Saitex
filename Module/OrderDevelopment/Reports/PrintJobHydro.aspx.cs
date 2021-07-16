using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_OrderDevelopment_Reports_PrintJobHydro : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST;
    protected void Page_Load(object sender, EventArgs e)
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Session["LoginDetail"] != null)
            {
                string QC = string.Empty;
                if (!IsPostBack)
                {
                   
                }
            }
        }
        catch
        {

        }
    }
   
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = "";
                string FromDate = string.Empty;
                string ToDate = string.Empty;
                if (txtFDATE.Text.ToString() != string.Empty && txtFDATE.Text.ToString() != "")
                    FromDate = txtFDATE.Text;
                else
                {
                    FromDate = string.Empty;
                    msg += "Invalid Starting Date.</ br>";
                }
                if (txtTDATE.Text.ToString() != string.Empty && txtTDATE.Text.ToString() != "")
                    ToDate = txtTDATE.Text;
                else
                {
                    ToDate = string.Empty;
                    msg += "Invalid End Number.</ br>";
                }
                //if (FromDate, out From))
                //    msg += "Invalid Starting Number.</ br>";
                //if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtTo.Text.Trim()), out To))
                //    msg += "Invalid Ending Number.</ br>";
                if (msg == "")
                {



                    string QueryString = "";
                    QueryString += "?FromDate=" + FromDate;
                    QueryString += "&ToDate=" + ToDate;

                    //if (Request.QueryString["QC"] == null || Request.QueryString["QC"] == "")
                    //{
                    //    string URL = "JobCardPrintReport.aspx" + QueryString;
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                    //}
                    //else
                    //{


                        string URL = "Job_Card_Hydro.aspx" + QueryString;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

                    //}



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
        txtFDATE.Text = "";
        txtTDATE.Text = "";
    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
}