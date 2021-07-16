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

public partial class Module_Inventory_Controls_FabricReciptPremRpt : System.Web.UI.UserControl
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
        catch(Exception ex )
        {
            throw ex;
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
                   //C:\inetpub\wwwroot\Saitex\Module\Fabric\FabricSaleWork\Reports\Fabric_reciptrpt.aspx
                    string URL = "/Saitex/Module/Fabric/FabricSaleWork/Reports/Fabric_reciptrpt.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                    Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void SetFromAndTo()
    {
        try
        {

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST OTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            OTX_FABRIC_IR_MST.TRN_TYPE = TRNTYPE;         
            OTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            OTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            OTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;     
            string TRN_NUMB = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetNewTRNNumber(OTX_FABRIC_IR_MST);
            txtFrom.Text = (int.Parse(TRN_NUMB) - 1).ToString();
            txtTo.Text = (int.Parse(TRN_NUMB) - 1).ToString();
            if (TRNTYPE == "RFS01")
                lblTRNType.Text = "Fabric Receipt Against PO Credit";
            else if (TRNTYPE == "RFS02")
                lblTRNType.Text = "Fabric Receipt Against PO Cash";
            else if (TRNTYPE == "RFS04")
                lblTRNType.Text = "Fabric Receipt Against Gate Pass";
            else if (TRNTYPE == "RFS03")
                lblTRNType.Text = "Fabric Receipt Miss.";
                
            else if (TRNTYPE == "IFS04")
                lblTRNType.Text = "Issue against Gate Pass";
            else if (TRNTYPE == "IFS01")
                lblTRNType.Text = "Issue Against Production Order";
            else if (TRNTYPE == "IFS02")
                lblTRNType.Text = "Issue Misc.";
            else if (TRNTYPE == "IFS03")
                lblTRNType.Text = "Return Against PO";
            else if (TRNTYPE == "IFS05")
                lblTRNType.Text = "Return Misc.";
        }
        catch(Exception ex )
        {
            throw ex;
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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                TRNTYPE = "RFS01";
                if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
                {
                    TRNTYPE = Request.QueryString["TRN_TYPE"].ToString();
                }
                SetFromAndTo();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
