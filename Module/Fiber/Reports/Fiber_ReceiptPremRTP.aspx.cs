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


public partial class Module_Fiber_Reports_Fiber_ReceiptPremRTP : System.Web.UI.Page
{
    private static  string TRNTYPE = "";
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
            SaitexDM.Common.DataModel.TX_FIBER_IR_MST OTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
            OTX_FIBER_IR_MST.TRN_TYPE = TRNTYPE;
            OTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            OTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            OTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            string TRN_NUMB = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetNewTRNNumber(OTX_FIBER_IR_MST);
            int TRN_NUMBI = int.Parse(TRN_NUMB);
            TRN_NUMB = (TRN_NUMBI - 1).ToString();
            txtFrom.Text = TRN_NUMB.ToString().Trim();
            txtTo.Text = TRN_NUMB.ToString().Trim();
            if (TRNTYPE == "RBS01")
                lblTRNType.Text = "Receipt Against Credit Purchase Order";
            else if (TRNTYPE == "RBS02")
                lblTRNType.Text = "Receipt Against Cash Purchase Order";
            else if (TRNTYPE == "RBS03")
                lblTRNType.Text = " Receipt Against Misc";
            else if (TRNTYPE == "RBS26")
                lblTRNType.Text = "Stock Transfer Receipt Fiber";

            else if (TRNTYPE == "IBS28")
                lblTRNType.Text = "Deport Sales Invoice";
            else if (TRNTYPE == "IBS26")
                lblTRNType.Text = "Stock Transfer/PlantInvoice";
            //else if (TRNTYPE == "IYS02")
            //    lblTRNType.Text = "Issue Misc.";
            //else if (TRNTYPE == "IYS03")
            //    lblTRNType.Text = "Return Against PO";
            //else if (TRNTYPE == "IYS05")
            //    lblTRNType.Text = "Return Misc.";
            //else if (TRNTYPE == "IYS05")
            //    lblTRNType.Text = "Return Misc.";
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
                    QueryString += "&TRN_TYPE=" + TRN_TYPE;
                    string URL = "Fiber1_Materail_Receipt.aspx" + QueryString;
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
                TRNTYPE = "RBS01";
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