using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Module_Yarn_SalesWork_Controls_YarnReceivingReport : System.Web.UI.UserControl
{
    private static string TRNTYPE = "";
    public string TRN_TYPE
    {
        get { return TRNTYPE; }
        set { TRNTYPE = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
                    {
                        TRNTYPE = Request.QueryString["TRN_TYPE"].ToString();
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
            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.TRN_TYPE = TRNTYPE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            string TRN_NUMB = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetNewTRNNumber(oTX_ITEM_IR_MST);
            txtFrom.Text = (int.Parse(TRN_NUMB) - 1).ToString();
            txtTo.Text = (int.Parse(TRN_NUMB) - 1).ToString();
            if (TRNTYPE == "RMS01")
                lblTRNType.Text = "Yarn Receipt Against PO Credit";
            else if (TRNTYPE == "RMS02")
                lblTRNType.Text = "Yarn Receipt Against PO Cash";
            else if (TRNTYPE == "RMS04")
                lblTRNType.Text = "Yarn Receipt Against Gate Pass";
            else if (TRNTYPE == "RMS03")
                lblTRNType.Text = " Receipt Miss.";

            else if (TRNTYPE == "IMS04")
                lblTRNType.Text = "Issue against Gate Pass";
            else if (TRNTYPE == "IMS01")
                lblTRNType.Text = "Issue Against Production Order";
            else if (TRNTYPE == "IMS02")
                lblTRNType.Text = "Issue Misc.";
            else if (TRNTYPE == "IMS03")
                lblTRNType.Text = "Return Against PO";
            else if (TRNTYPE == "IMS05")
                lblTRNType.Text = "Return Misc.";
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
                    QueryString += "&TRN_TYPE=" + TRNTYPE;
                    string URL = "Materail_Receipt.aspx" + QueryString;
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
                TRNTYPE = "RMS01";
                if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
                {
                    TRNTYPE = Request.QueryString["TRN_TYPE"].ToString();
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

