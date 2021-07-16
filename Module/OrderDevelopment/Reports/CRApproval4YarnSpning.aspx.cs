using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_OrderDevelopment_Reports_CRApproval4YarnSpning : System.Web.UI.Page
{
    string PRODUCT_TYPE = string.Empty;
    string ArticleNo = "";
    string BusinessType = "";
    string OrderNo = "";
    string Branch = "";
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Request.QueryString["PRODUCT_TYPE"] != null)
        {
            PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
        }
        if (!IsPostBack)
        {
           
            getBranch();
            getProduct();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        }
    }
    public void getProduct()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
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
    public void getBranch()
    {
        DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
        ddlBranch.DataSource = dt;
        ddlBranch.DataValueField = "BRANCH_CODE";
        ddlBranch.DataTextField = "BRANCH_NAME";

        ddlBranch.DataBind();


    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtArticleNo.Text != string.Empty || txtArticleNo.Text != "" || txtArticleNo.Text != null)
            {
                ArticleNo = txtArticleNo.Text;
            }
            else
            {
                ArticleNo = "";
            }
            if (txtBusinessType.Text != string.Empty || txtBusinessType.Text != "" || txtBusinessType.Text != null)
            {
                BusinessType = txtBusinessType.Text;
            }
            else
            {
                BusinessType = "";
            }
            if (txtOrderno.Text != string.Empty || txtOrderno.Text != "" || txtOrderno.Text != null)
            {
                OrderNo = txtOrderno.Text;
            }
            else
            {
                OrderNo = "";
            }
            if (ddlBranch.SelectedValue != "SELECT" || ddlBranch.SelectedValue != null || ddlBranch.SelectedIndex != -1)
            {
                Branch = ddlBranch.SelectedValue.ToString().Trim();
            }
            string QueryString = "";
            QueryString += "?ArticleNo=" + ArticleNo;
            QueryString += "&BusinessType=" + BusinessType;
            QueryString += "&PRODUCT_TYPE=" + PRODUCT_TYPE;
            QueryString += "&OrderNo=" + OrderNo;
            QueryString += "&Branch=" + Branch;
            //C:\inetpub\wwwroot\Saitex\Module\Fabric\FabricSaleWork\Reports\Fabric_reciptrpt.aspx
            string URL = "../../../Module/OrderDevelopment/CustomerRequest/Reports/CRApproval4YarnDyeing.aspx" + QueryString;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtArticleNo.Text = "";
        txtBusinessType.Text = "";
        txtOrderno.Text = "";
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;

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
}