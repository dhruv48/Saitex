using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Fiber_Pages_Fiber_PO_Report : System.Web.UI.Page
{
    
        SaitexDM.Common.DataModel.FIBER_PU_MST oFIBERPUMST = new SaitexDM.Common.DataModel.FIBER_PU_MST();
        private static  string PO_TYPE = "";
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
    protected void SetFromAndTo()
    {
        try{
            
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        oFIBERPUMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oFIBERPUMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oFIBERPUMST.PO_TYPE = PO_TYPE;
        string PONUMBER = SaitexBL.Interface.Method.TX_FIBER_PO_CREDIT.GetNewPONo(PO_TYPE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
            txtFrom.Text = PONUMBER.Trim();
            txtTo.Text = PONUMBER.Trim();
            if (PO_TYPE == "PUM")
            {
                lblPoType.Text = "Credit";
            }
            else if (PO_TYPE == "PUC")
            {
                lblPoType.Text = "Cash";
            }
            else
            {
                lblPoType.Text = "Mis.";
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
                    string URL = "Fiber_PU.aspx" + QueryString;
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
}

