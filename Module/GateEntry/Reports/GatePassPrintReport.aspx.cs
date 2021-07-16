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

public partial class Module_GateEntry_Reports_GatePassPrintReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST;

    protected void Page_Load(object sender, EventArgs e)
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Session["LoginDetail"] != null)
            {
                if (!IsPostBack)
                {
                    Initialise();
                    
                }
            }
        }
        catch
        {

        }
    }
    private void Initialise()
    {
        BindGateType();
        BindGatePassNo();
        ddlGateType.Focus();
    }


    private void BindGateType()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable BatchCode = SaitexBL.Interface.Method.TX_GATE_PASS_MST.GetNewGateType(oBATCH_CARD_MST);
            ddlGateType.Items.Clear();
            ddlGateType.DataSource = BatchCode;
            ddlGateType.DataTextField = "GATE_TYPE";
            ddlGateType.DataValueField = "GATE_TYPE";
            ddlGateType.DataBind();
        }
        catch
        {
            throw;
        }
    }


    private void BindGatePassNo()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            string BatchCode = SaitexBL.Interface.Method.TX_GATE_PASS_MST.GetNewGatePassNO(oBATCH_CARD_MST,ddlGateType.SelectedValue.ToString());
            txtFrom.Text = (Convert.ToInt32(BatchCode)).ToString();
            txtTo.Text = (Convert.ToInt32(BatchCode)).ToString();
        }
        catch
        {
            throw;
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
                    QueryString += "&GATE_TYPE=" + ddlGateType.SelectedValue.ToString();
                    QueryString += "&TYPE=" + string.Empty;
                    string URL = "GatePassReport.aspx" + QueryString;
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
            Initialise();
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
    protected void ddlGateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGatePassNo();
    }
}
