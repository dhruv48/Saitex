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

public partial class Module_Yarn_SalesWork_Reports_Yarn_PO_Report : System.Web.UI.Page
{
    
    SaitexDM.Common.DataModel.YRN_PU_MST oYRNPUMST = new SaitexDM.Common.DataModel.YRN_PU_MST();    
    private static string PO_TYPE = "";
    public string POTYPE
    {
        get { return PO_TYPE; }
        set { PO_TYPE = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["PO_TYPE"] != null && Request.QueryString["PO_TYPE"] != "")
                    {
                        PO_TYPE = Request.QueryString["PO_TYPE"].ToString();
                    }
                    else
                    {
                        PO_TYPE = "PUM";
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
            oYRNPUMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRNPUMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRNPUMST.PO_TYPE = PO_TYPE;
            string PONUMBER = SaitexBL.Interface.Method.YRN_PU_MST.GetNewPONo(PO_TYPE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);   
            txtFrom.Text = PONUMBER.Trim();
            txtTo.Text = PONUMBER.Trim();
            if (PO_TYPE == "PUM")
            {
                lblPOTYPE.Text = "Credit";
            }
            else if (PO_TYPE == "PUC")
            {
                lblPOTYPE.Text = "Cash";
            }
            else
            {
                lblPOTYPE.Text = "Mis.";
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
                    QueryString += "&PO_TYPE=" + PO_TYPE;
                    string URL = "Yarn_Po.aspx" + QueryString;
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
                PO_TYPE = "PUM";
                if (Request.QueryString["PO_TYPE"] != null && Request.QueryString["PO_TYPE"] != "")
                {
                    PO_TYPE = Request.QueryString["PO_TYPE"].ToString();
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
