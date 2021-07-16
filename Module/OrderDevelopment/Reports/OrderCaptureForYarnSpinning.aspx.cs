using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_OrderDevelopment_Reports_OrderCaptureForYarnSpinning : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public  string PI_TYPE;
    public  string OrderPrefix = string.Empty;
    private string PRODUCT_TYPE {get;set;}
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"] != "")
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }

            if (Request.QueryString["INVOICE"] != null && Request.QueryString["INVOICE"] != "")
            {
                ViewState["IsInvoice"] = Request.QueryString["INVOICE"].ToString();
            }

            #region code to set pi type
            if (PRODUCT_TYPE == "TEXTURISED YARN")
            {
                PI_TYPE = "YARN TEXTURISING";
            }
            else if (PRODUCT_TYPE == "YARN DYEING")
            {
                PI_TYPE = "YARN_DYEING";
            }
            else if (PRODUCT_TYPE == "SEWING THREAD")
            {
                PI_TYPE = "SEWING_THREAD";
            }
            else if (PRODUCT_TYPE == "GRAY WEAVING")
            {
                PI_TYPE = "GRAY_WEAV";
            }
            else if (PRODUCT_TYPE == "TEXTURISED YARN")
            {
                PI_TYPE = "YARN TWISTING";
            }

            lblFormHeading.Text = " Order Capture for " + PRODUCT_TYPE;
            #endregion

            if (!IsPostBack)
            {


                BindType(ddlBusinessType, "BUSINESS_TYPE");
                BindType(ddlProductType, "PRODUCT_TYPE");
                BindType(ddlOrderType, "ORDER_TYPE");
                
            }
            SetFromAndTo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading."));
        }
    }
    private void BindType(Obout.Interface.OboutDropDownList DDL, string TYPE)
    {
        try
        {
            DDL.Items.Clear();
            DataTable dtType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(TYPE, oUserLoginDetail.COMP_CODE);

            DDL.DataSource = dtType;
            DDL.DataTextField = "MST_DESC";
            DDL.DataValueField = "MST_CODE";
            DDL.DataBind();
            if (DDL.ID == "ddlProductType")
            {
                DDL.SelectedIndex = DDL.Items.IndexOf(DDL.Items.FindByValue(PRODUCT_TYPE));
                DDL.Text = PRODUCT_TYPE;
                DDL.Enabled = false;
            }
        }
        catch
        {
            throw;
        }

    }
    private void SetFromAndTo()
    {
        try
        {

            SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
            oOD_CAPTURE_MST.PRODUCT_TYPE = ddlProductType.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();

            string OrderNo = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetMaxOrderNo(oOD_CAPTURE_MST);
            OrderPrefix = OrderNo.Substring(0, 6);
            int ii = int.Parse(OrderNo.Substring(6)) - 1;



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

                int From = 0;
                int To = 0;



                string QueryString = "";
                QueryString += "?FromNo=" + From;
                QueryString += "&ToNo=" + To;
                QueryString += "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.Trim();
                QueryString += "&PRODUCT_TYPE=" + ddlProductType.SelectedValue.Trim();
                QueryString += "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.Trim();
                QueryString += "&ORDER_TYPE=" + ddlOrderType.SelectedValue.Trim();
                QueryString += "&ORDER_PREFIX=" + OrderPrefix;

                string URL = "OC_PrintReport.aspx" + QueryString;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

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
                SetFromAndTo();
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
    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetFromAndTo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Business type selection."));
        }

    }
  
}