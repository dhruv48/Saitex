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
using Common;

public partial class Module_Inventory_Controls_ReceiptPermRPT : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string TRNTYPE = "";
   
    public string TRN_TYPE
    {
        get { return TRNTYPE; }
        set { TRNTYPE = value; }
    }

    private static string TRNTYPE1 = "";
    public string TRN_TYPE1
    {
        get { return TRNTYPE1; }
        set { TRNTYPE1 = value; }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
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
                    if (TRN_TYPE == "GIM01")
                    {
                        BindInvoiceType();
                        chkLstInvoiceType.Visible = true;
                    }
                    else
                    {
                        chkLstInvoiceType.Visible = false;
                    }
                   
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
                lblTRNType.Text = "Material Receipt Against PO Credit";
            else if (TRNTYPE == "RMS02")
                lblTRNType.Text = "Material Receipt Against PO Cash";
            else if (TRNTYPE == "RMS04")
                lblTRNType.Text = "Material Receipt Against Gate Pass";
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
        int count = 0;
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
                



                if (TRN_TYPE == "GIM01")
                {
                if (!ValidationForInvoiceType())
                {
                    bool bFirst = true;
                    string Invoice_copy = string.Empty;
                    foreach (ListItem item in chkLstInvoiceType.Items)
                    {
                        if (item.Selected)
                        {
                            count++;
                            if (bFirst)
                            {
                                Invoice_copy += "$" + item.Text.Trim() + "$";
                                bFirst = false;
                            }
                            else
                            {
                                Invoice_copy += ", $" + item.Text.Trim() + "$";
                            }
                        }
                    }

                    if (msg == "")
                    {
                        string QueryString = "";
                        QueryString += "?FromNo=" + From;
                        QueryString += "&ToNo=" + To;
                        QueryString += "&TRN_TYPE=" + TRNTYPE;
                        QueryString += "&TRN_TYPE1=" + TRNTYPE1;
                        QueryString += "&PRINT_INVOICE=" + Invoice_copy;
                        string URL = "Materail_Receipt.aspx" + QueryString;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                    }
                    else
                        Common.CommonFuction.ShowMessage(msg);
                }
                else
                {
                    CommonFuction.ShowMessage("Please Select Atleast One Invoice Copy Type..");
                }
            }

                else
                {
                 if (msg == "")
                {
                    string QueryString = "";
                    QueryString += "?FromNo=" + From;
                    QueryString += "&ToNo=" + To;
                    QueryString += "&TRN_TYPE=" + TRNTYPE;
                    QueryString += "&TRN_TYPE1=" + TRNTYPE1;
                    string URL = "Materail_Receipt.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                    Common.CommonFuction.ShowMessage(msg);

                }
            
            
            
            
            
            
            
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

    private void BindInvoiceType()
    {
        try
        {
            chkLstInvoiceType.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("SW_PLANT_INV_CP", oUserLoginDetail.COMP_CODE);
            chkLstInvoiceType.DataSource = dt;
            chkLstInvoiceType.DataTextField = "MST_CODE";
            chkLstInvoiceType.DataValueField = "MST_CODE";
            chkLstInvoiceType.DataBind();
        }
        catch
        {
            throw;
        }
    }

    public bool ValidationForInvoiceType()
    {
        try
        {
            bool result = false;
            int count = 0;
            foreach (ListItem item in chkLstInvoiceType.Items)
            {
                if (item.Selected)
                {
                    count++;
                }

            }
            if (count > 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        catch
        {
            throw;
        }
    }
}
