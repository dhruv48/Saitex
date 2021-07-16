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

public partial class Module_OrderDevelopment_Reports_OC_Parameter : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    public static string PI_TYPE;
    public static string OrderPrefix = string.Empty;
    private static string PRODUCT_TYPE = "";

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

                if (Request.QueryString["INVOICE"] != null && Request.QueryString["INVOICE"] != "")
                {
                    ViewState["IsInvoice"] = Request.QueryString["INVOICE"].ToString();
                }

                #region code to set pi type
                if (PRODUCT_TYPE == "YARN SPINING")
                {
                    PI_TYPE = "YARN_SPINING";
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
                else if (PRODUCT_TYPE == "FABRIC PROCESSING")
                {
                    PI_TYPE = "FABR_PROC";
                }

                lblFormHeading.Text = " Order Capture for " + PRODUCT_TYPE;
                #endregion

                BindBusinessType();
                BindOrderType();
                BindProductType();

                SetFromAndTo();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading."));
        }
    }

    private void BindBusinessType()
    {
        try
        {
            ddlBusinessType.Items.Clear();
            DataTable dtBusinessType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);

            ddlBusinessType.DataSource = dtBusinessType;
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataBind();
        }
        catch
        {
            throw;
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

    private void BindOrderType()
    {
        try
        {
            ddlOrderType.Items.Clear();
            DataTable dtOrderType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);

            ddlOrderType.DataSource = dtOrderType;
            ddlOrderType.DataTextField = "MST_DESC";
            ddlOrderType.DataValueField = "MST_CODE";
            ddlOrderType.DataBind();
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
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            string OrderNo = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetMaxOrderNo1(oOD_CAPTURE_MST);
            //OrderPrefix = OrderNo.Substring(0, 6);
            //int ii = int.Parse(OrderNo.Substring(6)) - 1;

            //txtFrom.Text = ii.ToString();
            //txtTo.Text = ii.ToString();
            if (OrderNo=="Null")
            {
                txtFrom.Text = "No Records";
                txtTo.Text = "No Records";
            }
            else
            {
                txtFrom.Text = OrderNo;
                txtTo.Text = OrderNo;
            }
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
                string From = "";
                string To = "";
                //if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtFrom.Text.Trim()), out From))
                //    msg += "Invalid Starting Number.</ br>";
                //if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtTo.Text.Trim()), out To))
                //    msg += "Invalid Ending Number.</ br>";

                if (msg == "")
                {
                    string QueryString = "";
                    QueryString += "?FromNo=" + txtFrom.Text.Trim();
                    QueryString += "&ToNo=" + txtTo.Text.Trim();
                    QueryString += "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.Trim();
                    QueryString += "&PRODUCT_TYPE=" + ddlProductType.SelectedValue.Trim();
                    QueryString += "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.Trim();
                    QueryString += "&ORDER_TYPE=" + ddlOrderType.SelectedValue.Trim();
                   // QueryString += "&ORDER_PREFIX=" + OrderPrefix;
                    string URL = "OC_Print.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                    Common.CommonFuction.ShowMessage(msg);
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

    protected void ddlBusinessType_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetFromAndTo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in order category selection."));
        }


    }

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetFromAndTo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Product type selection."));
        }

    }

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetFromAndTo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order type selection."));
        }
    }

}
