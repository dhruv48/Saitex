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

public partial class Module_Inventory_Controls_Debit_Note_Parameter : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.TX_DEBIT_MST oTX_DEBIT_MST = new SaitexDM.Common.DataModel.TX_DEBIT_MST();
    private static string NOTE_TYPE = "";
    public string POTYPE
    {
        get { return NOTE_TYPE; }
        set { NOTE_TYPE = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["NOTE_TYPE"] != null && Request.QueryString["NOTE_TYPE"] != "")
                    {
                        NOTE_TYPE = Request.QueryString["NOTE_TYPE"].ToString();
                    }                  
                    SetFromAndTo();
                }
            }
        }
        catch
        {

        }
    }
    private void SetFromAndTo()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            oTX_DEBIT_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_DEBIT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_DEBIT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_DEBIT_MST.NOTE_TYPE = NOTE_TYPE;
            string _NUMBER =  SaitexBL.Interface.Method.TX_DEBIT_MST.GetNewDANo(oTX_DEBIT_MST);
            txtFrom.Text = _NUMBER.Trim();
            txtTo.Text = _NUMBER.Trim();
            if (NOTE_TYPE == "DEBIT NOTE")
                lblPOTYPE.Text = "Debit Note Report";
            else if (NOTE_TYPE == "CREDIT NOTE")
                lblPOTYPE.Text = "Credit Note Report";
            else
                lblPOTYPE.Text = "Mis.";
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
                int From = 0;
                int To = 0;
                if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtFrom.Text.Trim()), out From))
                    msg += "Invalid Starting Number.</ br>";
                if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtTo.Text.Trim()), out To))
                    msg += "Invalid Ending Number.</ br>";
                if (msg == "")
                {
                    string QueryString = "";
                    QueryString += "?FromNo=" + From;
                    QueryString += "&ToNo=" + To;
                    QueryString += "&NOTE_TYPE=" + NOTE_TYPE;
                    string URL = "DEBIT_NOTE_REPORT.aspx" + QueryString;
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
        try
        {
            if (Session["LoginDetail"] != null)
            {
                NOTE_TYPE = "DEBIT NOTE";
                if (Request.QueryString["NOTE_TYPE"] != null && Request.QueryString["NOTE_TYPE"] != "")
                {
                    NOTE_TYPE = Request.QueryString["NOTE_TYPE"].ToString();
                }
                SetFromAndTo();
            }
        }
        catch
        {

        }
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
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
