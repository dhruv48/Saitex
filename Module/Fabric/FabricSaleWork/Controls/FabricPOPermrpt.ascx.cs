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

public partial class Module_Inventory_Controls_FabricPOPermrpt : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.TX_FABRIC_PU_MST oTX_FABRIC_PU_MST = new SaitexDM.Common.DataModel.TX_FABRIC_PU_MST();
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
                    //else
                    //{
                    //    PO_TYPE = "PUM";
                    //}
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
            oTX_FABRIC_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_PU_MST.PO_TYPE = PO_TYPE;
            string PONUMBER = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetNewPONo(oTX_FABRIC_PU_MST.PO_TYPE, oTX_FABRIC_PU_MST.COMP_CODE, oTX_FABRIC_PU_MST.BRANCH_CODE);
            txtFrom.Text = PONUMBER.Trim();
            txtTo.Text = PONUMBER.Trim();
            if (PO_TYPE == "PUM")
                lblPOTYPE.Text = "Credit";
            else if (PO_TYPE == "PUC")
                lblPOTYPE.Text = "Cash";
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
                    QueryString += "&PO_TYPE=" + PO_TYPE;
                   // ~/Module/Fabric/FabricSaleWork/Reports/FabricPOPermtRPT.aspx
                    string URL = "FabricPORPT.aspx" + QueryString;
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
