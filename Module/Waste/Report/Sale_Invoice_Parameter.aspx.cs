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

using Common;

public partial class Module_OrderDevelopment_Reports_Sale_Invoice_Parameter : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_WASTE_IR_MST oTX_WASTE_IR_MST;
    private static string MaxTrnNum = string.Empty;
    public static string PI_TYPE;
    public static string OrderPrefix = string.Empty;
    private static string PRODUCT_TYPE = string.Empty;

    public string PRODUCTTYPE
    {
        get { return PRODUCT_TYPE; }
        set { PRODUCT_TYPE = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"] != "")
                {
                    PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
                }

                #region code to set pi type
                if (PRODUCT_TYPE == "YARN SPINING")
                {
                    PI_TYPE = "YARN_SPINING";
                }
                else if (PRODUCT_TYPE == "WASTE")
                {
                    PI_TYPE = "WASTE";
                }
                else if (PRODUCT_TYPE.Equals("SEWING THREAD", StringComparison.OrdinalIgnoreCase))
                {
                    PI_TYPE = "SEWING_THREAD";
                }
                else if (PRODUCT_TYPE == "GRAY WEAVING")
                {
                    PI_TYPE = "GRAY_WEAV";
                }
                else if (PRODUCT_TYPE == "FABRIC PROCESSING")
                {
                    PI_TYPE = "FABR_PROC";
                }

                lblFormHeading.Text = " Sale Invoice for " + PRODUCT_TYPE;
                #endregion

               
                BindProductType();
                BindInvoiceType();
                BindLastInvoiceNo();
              
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading."));
        }
    }

  
    private void BindProductType()
    {
        try
        {
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);

            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();

            ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(PRODUCT_TYPE));
            ddlProductType.Text = PRODUCT_TYPE;
            ddlProductType.Enabled = false;
        }
        catch
        {
            throw;
        }
    }

    private void BindLastInvoiceNo()
    {
        try
        {
            oTX_WASTE_IR_MST = new SaitexDM.Common.DataModel.TX_WASTE_IR_MST();
            oTX_WASTE_IR_MST.TRN_TYPE = "IWS26";
            oTX_WASTE_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_WASTE_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_WASTE_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            MaxTrnNum = SaitexBL.Interface.Method.TX_WASTE_IR_MST.GetMaxTRNNumber(oTX_WASTE_IR_MST);
            txtInvoiceFrom.Text = MaxTrnNum;
            txtInvoiceTo.Text = MaxTrnNum;
        }
        catch
        {
            throw;
        }
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

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        int count = 0;
        try
        {
            if (Page.IsValid)
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

                    string QueryString = "";
                    QueryString += "?PRODUCT_TYPE=" + "WASTE";  
                    
                    QueryString += "&INVOICE_FROM=" + txtInvoiceFrom.Text.Trim();
                    QueryString += "&INVOICE_TO=" + txtInvoiceTo.Text.Trim();
                    QueryString += "&PRINT_INVOICE=" + Invoice_copy;
                    
                    string URL = "SaleInvoice.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                {
                    CommonFuction.ShowMessage("Please Select Atleast One Invoice Copy Type..");
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page print."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
           
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page reset."));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page."));
        }

    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page help."));
        }

    }

  

    

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
       
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Product type selection."));
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
