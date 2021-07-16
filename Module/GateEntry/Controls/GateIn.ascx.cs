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

public partial class Module_GateEntry_Controls_GateIn : System.Web.UI.UserControl
{
    private static string ITEM_TYPE = "";
    string GATE_TYPE = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["ITEM_TYPE"] != null && Request.QueryString["ITEM_TYPE"] != "")
                    {
                        ITEM_TYPE = Request.QueryString["ITEM_TYPE"].ToString();
                    }

                    if (Request.QueryString["GATE_TYPE"] != null && Request.QueryString["GATE_TYPE"] != "")
                    {
                        GATE_TYPE = Request.QueryString["GATE_TYPE"].ToString();
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
            string YEAR = oUserLoginDetail.DT_STARTDATE.Year.ToString();
            string COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
            string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
           
           
           string GATE_NO = SaitexBL.Interface.Method.TX_Gate_MST.GetNewGateNo(YEAR, ITEM_TYPE, COMP_CODE, BRANCH_CODE, GATE_TYPE);

           txtFrom.Text = GATE_NO.ToString();
           txtTo.Text = GATE_NO.ToString();

            lblPOTYPE.Text = ITEM_TYPE.ToString();
            
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
                //string msg = "";
                int From = 0;
                int To = 0;
                //if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtFrom.Text.Trim()), out From))
                //    msg += "Invalid Starting Number.</ br>";
                //if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtTo.Text.Trim()), out To))
                //    msg += "Invalid Ending Number.</ br>";
                //if (msg == "")
                //{
                    string QueryString = "";
                    QueryString += "?FromNo=" + txtFrom.Text.ToString();
                    QueryString += "&ToNo=" + txtTo.Text.ToString();
                    QueryString += "&GATE_TYPE=" + GATE_TYPE;
                    QueryString += "&ITEM_TYPE=" + ITEM_TYPE;
                    string URL = "YarnGateInReport.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=40000,height=600');", true);
                //}
                //else
                //    Common.CommonFuction.ShowMessage(msg);
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
                //PO_TYPE = "PUM";
                //if (Request.QueryString["PO_TYPE"] != null && Request.QueryString["PO_TYPE"] != "")
                //{
                //    PO_TYPE = Request.QueryString["PO_TYPE"].ToString();
                //}
                //SetFromAndTo();
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
