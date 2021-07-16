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
    SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST;
    private string MaxTrnNum = string.Empty;
    public  string PI_TYPE;
    public  string OrderPrefix = string.Empty;
    private string PRODUCT_TYPE = string.Empty;
    private string TRN_TYPE = string.Empty;
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
            if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
            }
            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"] != "")
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }
            if (!IsPostBack)
            {
              

                #region code to set pi type
                if (PRODUCT_TYPE == "TEXTURISED YARN")
                {
                    PI_TYPE = "TEXTURISED YARN";
                }
                if (PRODUCT_TYPE == "YARN SPINING")
                {
                    PI_TYPE = "YARN_SPINING";
                }
                else if (PRODUCT_TYPE == "YARN DYEING")
                {
                    PI_TYPE = "YARN_DYEING";
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
                if(TRN_TYPE=="IYS26")
                {
                    lblFormHeading.Text = " Delevery Order for Sale Work (" + PRODUCT_TYPE + ")"; 
                }
                if (TRN_TYPE == "IYS27")
                {
                    lblFormHeading.Text = " Delevery Order for Job Work (" + PRODUCT_TYPE + ")"; 
                }
                else
                {
                    lblFormHeading.Text = " Sale Invoice for " + PRODUCT_TYPE;
                }
                #endregion

                //BindBusinessType();
                //BindOrderType();
              //  BindProductType();
                BindInvoiceType();
                BindLastInvoiceNo();
                //SetFromAndTo();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading."));
        }
    }

    //private void BindBusinessType()
    //{
    //    try
    //    {
    //        ddlBusinessType.Items.Clear();
    //        DataTable dtBusinessType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);

    //        ddlBusinessType.DataSource = dtBusinessType;
    //        ddlBusinessType.DataTextField = "MST_DESC";
    //        ddlBusinessType.DataValueField = "MST_CODE";
    //        ddlBusinessType.DataBind();
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //private void BindProductType()
    //{
    //    try
    //    {
    //        ddlProductType.Items.Clear();
    //        DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);

    //        ddlProductType.DataSource = dtProductionType;
    //        ddlProductType.DataTextField = "MST_DESC";
    //        ddlProductType.DataValueField = "MST_CODE";
    //        ddlProductType.DataBind();

    //      //  ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue("SALE WORK"));
    //        ddlProductType.Items.Add(0, "FROM STOCK");      
    //        //ddlProductType.Text = BUSINESS_TYPE;
    //        ddlProductType.Enabled = true;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    private void BindLastInvoiceNo()
    {
        try
        {
            oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.TRN_TYPE = ddlProductType.SelectedValue.ToString();
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            MaxTrnNum = SaitexBL.Interface.Method.YRN_IR_MST.GetMaxTRNNumber(oYRN_IR_MST);
            txtInvoiceFrom.Text = MaxTrnNum;
            txtInvoiceTo.Text = MaxTrnNum;
        }
        catch
        {
            throw;
        }
    }

    //private void BindOrderType()
    //{
    //    try
    //    {
    //        ddlOrderType.Items.Clear();
    //        DataTable dtOrderType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);

    //        ddlOrderType.DataSource = dtOrderType;
    //        ddlOrderType.DataTextField = "MST_DESC";
    //        ddlOrderType.DataValueField = "MST_CODE";
    //        ddlOrderType.DataBind();
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //private void SetFromAndTo()
    //{
    //    try
    //    {

    //        SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

    //        oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //        oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
    //        oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
    //        oOD_CAPTURE_MST.PRODUCT_TYPE = ddlProductType.SelectedValue.Trim();
    //        oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();
    //        oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();

    //        string OrderNo = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetMaxOrderNo(oOD_CAPTURE_MST);
    //        OrderPrefix = OrderNo.Substring(0, 6);
    //        int ii = int.Parse(OrderNo.Substring(6)) - 1;

    //        if (ii < 10)
    //            OrderPrefix += "000";
    //        else if (ii > 9 && ii < 100)
    //            OrderPrefix += "00";
    //        else if (ii > 99 && ii < 1000)
    //            OrderPrefix += "0";

    //        txtFrom.Text = OrderPrefix + ii.ToString();
    //        txtTo.Text = OrderPrefix + ii.ToString();

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

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
                    QueryString += "?PRODUCT_TYPE=" + "TEXTURISED YARN";  // ddlProductType.SelectedValue.Trim()
                    //QueryString += "?FromNo=" + txtFrom.Text.Trim();
                    //QueryString += "&ToNo=" + txtTo.Text.Trim();
                    //QueryString += "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.Trim();
                    //QueryString += "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.Trim();
                    //QueryString += "&ORDER_TYPE=" + ddlOrderType.SelectedValue.Trim();
                    QueryString += "&INVOICE_FROM=" + txtInvoiceFrom.Text.Trim();
                    QueryString += "&INVOICE_TO=" + txtInvoiceTo.Text.Trim();
                    QueryString += "&PRINT_INVOICE=" + Invoice_copy;
                    QueryString += "&TRN_TYPE=" + ddlProductType.SelectedValue.ToString() ;
                    //QueryString += "&ORDER_PREFIX=" + OrderPrefix;

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
                //// SetFromAndTo();
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

    //protected void ddlBusinessType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SetFromAndTo();
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Business type selection."));
    //    }

    //}

    //protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SetFromAndTo();
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in order category selection."));
    //    }


    //}

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  SetFromAndTo();
            BindLastInvoiceNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Product type selection."));
        }

    }

    //protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SetFromAndTo();
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order type selection."));
    //    }
    //}

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
