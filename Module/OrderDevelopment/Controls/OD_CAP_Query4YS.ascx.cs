using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class Module_OrderDevelopment_Controls_OD_CAP_Query4YS : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string PRODUCT_TYPE = "YARN SPINING";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                GetAllOrderCaptureApproval();
                Intiallize();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }
    public void Intiallize()
    {
 
    }
    private void GetAllOrderCaptureApproval()
    {
        try
        {
            //DataTable dt = CreateDataTable();
            //DataRow dr = dt.NewRow();
            //dr["COMP_CODE"] = string.Empty ;
            //dt.Rows.Add(dr);  
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllOrderCaptureApproval1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,PRODUCT_TYPE);
            gvOrder.DataSource = dt;
            gvOrder.DataBind();


        }
        catch
        {
            throw;
        }
    }
    protected void BtnBranchName_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    protected void btnBusinessType_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    protected void btnProductType_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    protected void btnOrderCat_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();

    }
    protected void btnOrderType_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();

    }
    protected void btnPrtyCode_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    protected void btnPiType_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    protected void btnArticleCode_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    protected void btnUom_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    protected void btnPrtyName_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }
    }
    protected void SearchbyKeywords()
    {
        try
        {
          GridViewRow gvr= gvOrder.HeaderRow;
          string OrderNo = ((TextBox)gvr.FindControl("txtOrderNo")).Text;
          string PartyCode = ((TextBox)gvr.FindControl("txtPrtyCode")).Text;
          string PartyName = ((TextBox)gvr.FindControl("txtPrtyName")).Text;
          string PI_NO = ((TextBox)gvr.FindControl("txtPiNo")).Text;
          string Artical_Code = ((TextBox)gvr.FindControl("txtArticleCode")).Text;
          string UOM = ((TextBox)gvr.FindControl("txtUom")).Text;
          string PI_TYPE = ((TextBox)gvr.FindControl("txtPiType")).Text;
          string Branch_Code = ((TextBox)gvr.FindControl("txtBranchName")).Text;
          string Business_Type = ((TextBox)gvr.FindControl("txtBusinessType")).Text;
          string Product_Type = "YARN SPINING";
          string Order_Cat = ((TextBox)gvr.FindControl("txtOrderCat")).Text;
          string ORDER_Type = ((TextBox)gvr.FindControl("txtOrderType")).Text;
          string Artical_Desc = ((TextBox)gvr.FindControl("txtArticaldesc")).Text;
          DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.SearchDataByfilterOC(Branch_Code, Business_Type, Product_Type, Order_Cat, ORDER_Type, OrderNo, PartyCode, PartyName, PI_TYPE, PI_NO, Artical_Code, Artical_Desc, UOM);
          if (dt != null && dt.Rows.Count > 0)
          {
              gvOrder.DataSource = dt;
              gvOrder.DataBind();
              //AutofillSearchContent(BranchName, BusinessType, ProductType, OrderCat, OrderType, OrderNo, PrtyCode, PrtyName, PiType, PiNo, ArticleCode, Articaldesc, Uom);

          }

        }
        catch (Exception ex)
        {
            throw ex;

        }
 
    }
    protected void btnOrderNo_Click(object sender, ImageClickEventArgs e)
    {
        SearchbyKeywords();
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Page" && e.CommandName != "")
        {
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblCOMP_CODE = (Label)row.FindControl("lblCOMP_CODE");

            Label lblBRANCH_CODE = (Label)row.FindControl("lblBRANCH_CODE");

            Label lblBUSINESS_TYPE = (Label)row.FindControl("lblBUSINESS_TYPE");
            Label lblPRODUCT_TYPE = (Label)row.FindControl("lblPRODUCT_TYPE");
            Label lblORDER_CAT = (Label)row.FindControl("lblORDER_CAT");
            Label lblORDER_TYPE = (Label)row.FindControl("lblORDER_TYPE");
            Label lblORDER_NO = (Label)row.FindControl("lblORDER_NO");
            Label lblPI_TYPE = (Label)row.FindControl("lblPI_TYPE");
            Label lblPI_NO = (Label)row.FindControl("lblPI_NO");
            Label lblARTICAL_CODE = (Label)row.FindControl("lblARTICAL_CODE");
            Label lblSHADE_CODE = (Label)row.FindControl("lblSHADE_CODE");
            if (e.CommandName == "ViewCR")
            {
                try
                {
                    string URL = "ViewCROrderAdj.aspx";
                    URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                    URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                    URL = URL + "&BUSINESS_TYPE=" + lblBUSINESS_TYPE.Text;
                    URL = URL + "&PRODUCT_TYPE=" + lblPRODUCT_TYPE.Text;
                    URL = URL + "&ORDER_CAT=" + lblORDER_CAT.Text;
                    URL = URL + "&ORDER_TYPE=" + lblORDER_TYPE.Text;
                    URL = URL + "&ORDER_NO=" + lblORDER_NO.Text;
                    URL = URL + "&PI_TYPE=" + lblPI_TYPE.Text;
                    URL = URL + "&PI_NO=" + lblPI_NO.Text;
                    URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                    URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

                }
                catch
                {

                }
            }
            else if (e.CommandName == "ViewBom")
            {
                string URL = "ViewBom.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                URL = URL + "&BUSINESS_TYPE=" + lblBUSINESS_TYPE.Text;
                URL = URL + "&PRODUCT_TYPE=" + lblPRODUCT_TYPE.Text;
                URL = URL + "&ORDER_CAT=" + lblORDER_CAT.Text;
                URL = URL + "&ORDER_TYPE=" + lblORDER_TYPE.Text;
                URL = URL + "&ORDER_NO=" + lblORDER_NO.Text;
                URL = URL + "&PI_TYPE=" + lblPI_TYPE.Text;
                URL = URL + "&PI_NO=" + lblPI_NO.Text;
                URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

            }
            else if (e.CommandName == "ViewCost")
            {
                string URL = "ViewCost.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                URL = URL + "&BUSINESS_TYPE=" + lblBUSINESS_TYPE.Text;
                URL = URL + "&PRODUCT_TYPE=" + lblPRODUCT_TYPE.Text;
                URL = URL + "&ORDER_CAT=" + lblORDER_CAT.Text;
                URL = URL + "&ORDER_TYPE=" + lblORDER_TYPE.Text;
                URL = URL + "&ORDER_NO=" + lblORDER_NO.Text;
                URL = URL + "&PI_TYPE=" + lblPI_TYPE.Text;
                URL = URL + "&PI_NO=" + lblPI_NO.Text;
                URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
           
            }
            else if (e.CommandName == "ViewProcessRoute")
            {
                string URL = "ViewProcessRoute.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                URL = URL + "&BUSINESS_TYPE=" + lblBUSINESS_TYPE.Text;
                URL = URL + "&PRODUCT_TYPE=" + lblPRODUCT_TYPE.Text;
                URL = URL + "&ORDER_CAT=" + lblORDER_CAT.Text;
                URL = URL + "&ORDER_TYPE=" + lblORDER_TYPE.Text;
                URL = URL + "&ORDER_NO=" + lblORDER_NO.Text;
                URL = URL + "&PI_TYPE=" + lblPI_TYPE.Text;
                URL = URL + "&PI_NO=" + lblPI_NO.Text;
                URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
         
            }
 
        }

    }
    protected void gvOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOrder.PageIndex = e.NewPageIndex;
        GetAllOrderCaptureApproval();
        
    }
}
