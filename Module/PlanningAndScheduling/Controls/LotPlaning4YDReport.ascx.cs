using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_PlanningAndScheduling_Controls_LotPlaning4YDReport : System.Web.UI.UserControl
{
    string PRODUCT_TYPE = "YARN DYEING";
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindProductType();
                BindOrderType();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void BindProductType()
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

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
    protected void BindOrderType()
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        try
        {
            ddlordertype.Items.Clear();
            DataTable dtOrderType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);

            ddlordertype.DataSource = dtOrderType;
            ddlordertype.DataTextField = "MST_DESC";
            ddlordertype.DataValueField = "MST_CODE";
            ddlordertype.DataBind();
        }
        catch
        {
            throw;
        }
    }
    protected void imgPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                //int From = 0;
                //int To = 0;



                string QueryString = "";
                //QueryString += "?FromNo=" + From;
                //QueryString += "&ToNo=" + To;
                QueryString += "?BUSINESS_TYPE=" + ddlBusiness.SelectedValue.Trim();
                QueryString += "&PRODUCT_TYPE=" + ddlProductType.SelectedValue.Trim();
                QueryString += "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.Trim();
                QueryString += "&ORDER_TYPE=" + ddlordertype.SelectedValue.Trim();
                // QueryString += "&ORDER_PREFIX=" + OrderPrefix;
                string URL = "/Saitex/Module/PlanningAndScheduling/Reports/LotPlanReport.aspx" + QueryString;
                //string URL = "LotPlanning.aspx" + QueryString;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page print."));
        }
    }
    protected void imgClear_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void imgExit_Click(object sender, ImageClickEventArgs e)
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
        catch (Exception exe)
        {
            throw exe;
        }
    }
}
