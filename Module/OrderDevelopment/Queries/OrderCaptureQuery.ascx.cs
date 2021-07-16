using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using errorLog;
using System.IO;
using System.Data;
using System.Globalization;
public partial class Module_OrderDevelopment_Queries_OrderCaptureQuery : System.Web.UI.UserControl
{ 
 
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
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
    
    private void Intiallize()
    {
        try
        {
            BindBranch();
            BindBussinessType();
            BindPRODUCT_TYPE();
            BindORDER_CAT();
            BindPI_TYPE();
            BindORDER_TYPE();
        }
        catch
        {
            throw;
        }
    
    }
    private void BindBranch()
    {
        try
        {
            DataSet  ds = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllFilterforQuery(oUserLoginDetail.COMP_CODE);
            ddlBranch.Items.Clear();

            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataValueField = "BRANCH_NAME";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("------Select------", ""));
            ds.Dispose();
            ds = null;
        }
        catch
        {
            
            throw;
        }
    }
    private void BindBussinessType()
    {
        try
        {
            DataSet ds = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllFilterforQuery(oUserLoginDetail.COMP_CODE);
            ddlBussinesstype .Items.Clear();

            ddlBussinesstype.DataSource = ds.Tables[1];
            ddlBussinesstype.DataValueField = "BUSINESS_TYPE";
            ddlBussinesstype.DataTextField = "BUSINESS_TYPE";
            ddlBussinesstype.DataBind();
            ddlBussinesstype.Items.Insert(0, new ListItem("------Select------", ""));
            ds.Dispose();
            ds = null;
        }
        catch
        {

            throw;
        }
    }
    private void BindPRODUCT_TYPE()
    {
        try
        {
            DataSet ds = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllFilterforQuery(oUserLoginDetail.COMP_CODE);
            ddlProductType.Items.Clear();

            ddlProductType.DataSource = ds.Tables[2];
            ddlProductType.DataValueField = "PRODUCT_TYPE";
            ddlProductType.DataTextField = "PRODUCT_TYPE";
            ddlProductType.DataBind();
            ddlProductType.Items.Insert(0, new ListItem("------Select------", ""));
            ds.Dispose();
            ds = null;
        }
        catch
        {

            throw;
        }
    }
    private void BindORDER_CAT()
    {
        try
        {
            DataSet ds = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllFilterforQuery(oUserLoginDetail.COMP_CODE);
            ddlOrderCat .Items.Clear();

            ddlOrderCat.DataSource = ds.Tables[3];
            ddlOrderCat.DataValueField = "ORDER_CAT";
            ddlOrderCat.DataTextField = "ORDER_CAT";
            ddlOrderCat.DataBind();
            ddlOrderCat.Items.Insert(0, new ListItem("------Select------", ""));
            ds.Dispose();
            ds = null;
        }
        catch
        {

            throw;
        }
    }
    private void BindORDER_TYPE()
    {
        try
        {
            DataSet ds = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllFilterforQuery(oUserLoginDetail.COMP_CODE);
            ddlOrderType.Items.Clear();

            ddlOrderType .DataSource = ds.Tables[4];
            ddlOrderType.DataValueField = "ORDER_TYPE";
            ddlOrderType.DataTextField = "ORDER_TYPE";
            ddlOrderType.DataBind();
            ddlOrderType.Items.Insert(0, new ListItem("------Select------", ""));
            ds.Dispose();
            ds = null;
        }
        catch
        {

            throw;
        }
    }
    private void BindPI_TYPE()
    {
        try
        {
            DataSet ds = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllFilterforQuery(oUserLoginDetail.COMP_CODE);
            ddlPiType.Items.Clear();

            ddlPiType.DataSource = ds.Tables[5];
            ddlPiType.DataValueField = "PI_TYPE";
            ddlPiType.DataTextField = "PI_TYPE";
            ddlPiType.DataBind();
            ddlPiType.Items.Insert(0, new ListItem("------Select------", ""));
            ds.Dispose();
            ds = null;
        }
        catch
        {

            throw;
        }
    }
           
    private DataTable CreateDataTable()
    {
       DataTable dtMain = new DataTable();
       dtMain.Columns.Add("COMP_CODE", typeof(string));
       dtMain.Columns.Add("BRANCH_CODE", typeof(string));
       dtMain.Columns.Add("BRANCH_NAME", typeof(string));
       dtMain.Columns.Add("BUSINESS_TYPE", typeof(string));
       dtMain.Columns.Add("PRODUCT_TYPE", typeof(string));
       dtMain.Columns.Add("ORDER_CAT", typeof(string));
       dtMain.Columns.Add("ORDER_TYPE", typeof(string));
       dtMain.Columns.Add("ORDER_NO", typeof(string));
       dtMain.Columns.Add("PI_TYPE", typeof(string));
       dtMain.Columns.Add("PI_NO", typeof(string));
       dtMain.Columns.Add("ARTICAL_CODE", typeof(string)); dtMain.Columns.Add("ARTICAL_DESC", typeof(string));
       dtMain.Columns.Add("SHADE_CODE", typeof(string));
       dtMain.Columns.Add("UOM", typeof(string));
       dtMain.Columns.Add("PRTY_CODE", typeof(string));
       dtMain.Columns.Add("PRTY_NAME", typeof(string));

       dtMain.Columns.Add("ORDER_DATE", typeof(DateTime));
       dtMain.Columns.Add("QTY", typeof(double));
       dtMain.Columns.Add("DEL_DATE", typeof(DateTime));
       dtMain.Columns.Add("PACKING_QTY", typeof(double));
       dtMain.Columns.Add("REQ_QTY", typeof(double));
       dtMain.Columns.Add("ISS_QTY", typeof(double));
       dtMain.Columns.Add("ORD_QTY", typeof(double));
        
       return dtMain;
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
            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllOrderCaptureApproval(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            gvOrder.DataSource = dt;
            gvOrder.DataBind();
           
         
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

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
   
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.background='#eeff00';";
           // e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.background='#ffffff';";
            
            e.Row.Attributes["onclick"] = "__doPostBack('" + gvOrder.UniqueID + "','Select$" + e.Row.RowIndex + "');";

        }
    }

    protected void NestedDl_ItemCommand(object source, DataListCommandEventArgs e)
    {
        //GridViewRow item =(GridViewRow)((Control)e.CommandSource).Parent.Parent.Parent.Parent.NamingContainer ;
        //string name = ((Label)(item.Controls[1].FindControl("lblCOMP_CODE"))).Text; 
         
        //    Label lblCOMP_CODE = e.Item.FindControl("lblCOMP_CODE") as Label;
        //    Label lblBRANCH_CODE = e.Item.FindControl("lblBRANCH_CODE") as Label;
        //    Label lblBUSINESS_TYPE = e.Item.FindControl("lblBUSINESS_TYPE") as Label;
        //    Label lblPRODUCT_TYPE = e.Item.FindControl("lblPRODUCT_TYPE") as Label;
        //    Label lblORDER_CAT = e.Item.FindControl("lblORDER_CAT") as Label;
        //    Label lblORDER_TYPE = e.Item.FindControl("lblORDER_TYPE") as Label;
        //    Label lblORDER_NO = e.Item.FindControl("lblORDER_NO") as Label;
        //    Label lblPI_TYPE = e.Item.FindControl("lblPI_TYPE") as Label;
        //    Label lblPI_NO = e.Item.FindControl("lblPI_NO") as Label;
        //    Label lblARTICAL_CODE = e.Item.FindControl("lblARTICAL_CODE") as Label;
        //    Label lblSHADE_CODE = e.Item.FindControl("lblSHADE_CODE") as Label;

        //    if (e.CommandName == "ViewCR")
        //    {

        //        try
        //        {
        //            string URL = "ViewCROrderAdj.aspx";
        //            URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
        //            URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
        //            URL = URL + "&BUSINESS_TYPE=" + lblBUSINESS_TYPE.Text;
        //            URL = URL + "&PRODUCT_TYPE=" + lblPRODUCT_TYPE.Text;
        //            URL = URL + "&ORDER_CAT=" + lblORDER_CAT.Text;
        //            URL = URL + "&ORDER_TYPE=" + lblORDER_TYPE.Text;
        //            URL = URL + "&ORDER_NO=" + lblORDER_NO.Text;
        //            URL = URL + "&PI_TYPE=" + lblPI_TYPE.Text;
        //            URL = URL + "&PI_NO=" + lblPI_NO.Text;
        //            URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
        //            URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

        //        }
        //        catch
        //        {

        //        }

        //    }
        //    if (e.CommandName == "Minus")
        //    {



        //    }
       

    }

    private void SearchbyKeywords()
    {
        try
        {
            string BranchName = string.Empty;
            string BusinessType = string.Empty;
            string ProductType = string.Empty;
            string OrderCat = string.Empty;
            string OrderType = string.Empty;
            string PiType = string.Empty;
            

            if (ddlBranch.SelectedIndex > 0)
            {
                 BranchName = ddlBranch.SelectedItem.ToString();
            }
            else
            {
                 BranchName = string.Empty;
            }
            if (ddlBussinesstype.SelectedIndex > 0)
            {
                 BusinessType = ddlBussinesstype.SelectedItem.ToString();
            }
            else
            {
                 BusinessType = string.Empty;
            }
            if (ddlProductType.SelectedIndex > 0)
            {
                 ProductType = ddlProductType.SelectedItem.ToString();
            }
            else
            {
                 ProductType = string.Empty;
            }
            if (ddlOrderCat.SelectedIndex > 0)
            {
                 OrderCat = ddlOrderCat.SelectedItem.ToString();
            }
            else
            {
                 OrderCat = string.Empty;
            }
            if (ddlOrderType.SelectedIndex > 0)
            {
                 OrderType = ddlOrderType.SelectedItem.ToString();  
            }
            else
            {
                 OrderType = string.Empty;
            }
            if (ddlPiType.SelectedIndex > 0)
            {
                PiType = ddlPiType.SelectedItem.ToString();  
            }
            else
            {
                PiType = string.Empty;
            }

          
          

            
            string OrderNo = ((TextBox)gvOrder.HeaderRow.FindControl("txtOrderNo")).Text;
            string PrtyCode = ((TextBox)gvOrder.HeaderRow.FindControl("txtPrtyCode")).Text;

            string PrtyName = ((TextBox)gvOrder.HeaderRow.FindControl("txtPrtyName")).Text;
          
            string PiNo = ((TextBox)gvOrder.HeaderRow.FindControl("txtPiNo")).Text;
            string ArticleCode = ((TextBox)gvOrder.HeaderRow.FindControl("txtArticleCode")).Text;
            string Articaldesc = ((TextBox)gvOrder.HeaderRow.FindControl("txtArticaldesc")).Text;
            string Uom = ((TextBox)gvOrder.HeaderRow.FindControl("txtUom")).Text;

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.SearchDataByfilterOC(BranchName, BusinessType, ProductType, OrderCat, OrderType, OrderNo, PrtyCode, PrtyName, PiType, PiNo, ArticleCode, Articaldesc, Uom);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvOrder.DataSource = dt;
                gvOrder.DataBind();
                AutofillSearchContent(BranchName, BusinessType, ProductType, OrderCat, OrderType, OrderNo, PrtyCode, PrtyName, PiType, PiNo, ArticleCode, Articaldesc, Uom);
               
            }
            else
            {
                DataTable dtblank = CreateDataTable();
                DataRow dr = dtblank.NewRow();
                dr["COMP_CODE"] = string.Empty;
                dtblank.Rows.Add(dr);  
                gvOrder.DataSource = dtblank;
                gvOrder.DataBind();
                AutofillSearchContent(BranchName, BusinessType, ProductType, OrderCat, OrderType, OrderNo, PrtyCode, PrtyName, PiType, PiNo, ArticleCode, Articaldesc, Uom);
              
            }
         
        }
        catch
        {
            throw;
        }


    }
    private void AutofillSearchContent(string BranchName, string BusinessType, string ProductType, string OrderCat, string OrderType, string OrderNo, string PrtyCode, string PrtyName, string PiType, string PiNo, string ArticleCode, string Articaldesc, string Uom)
       
    {
        try
        {
            //TextBox txtBranchName = (TextBox)gvOrder.HeaderRow.FindControl("txtBranchName");
            //TextBox txtBusinessType = (TextBox)gvOrder.HeaderRow.FindControl("txtBusinessType");
            //TextBox txtProductType = (TextBox)gvOrder.HeaderRow.FindControl("txtProductType");
            //TextBox txtOrderCat = (TextBox)gvOrder.HeaderRow.FindControl("txtOrderCat");
            //TextBox txtOrderType = (TextBox)gvOrder.HeaderRow.FindControl("txtOrderType");
            //TextBox txtOrderNo = (TextBox)gvOrder.HeaderRow.FindControl("txtOrderNo");
            //TextBox txtPrtyCode = (TextBox)gvOrder.HeaderRow.FindControl("txtPrtyCode");

            //TextBox txtPrtyName = (TextBox)gvOrder.HeaderRow.FindControl("txtPrtyName");
            //TextBox txtPiType = (TextBox)gvOrder.HeaderRow.FindControl("txtPiType");
            //TextBox txtPiNo = (TextBox)gvOrder.HeaderRow.FindControl("txtPiNo");
            //TextBox txtArticleCode = (TextBox)gvOrder.HeaderRow.FindControl("txtArticleCode");
            //TextBox txtArticaldesc = (TextBox)gvOrder.HeaderRow.FindControl("txtArticaldesc");
            //TextBox txtUom = (TextBox)gvOrder.HeaderRow.FindControl("txtUom");



            //txtBranchName.Text = BranchName;
            //txtBusinessType.Text = BusinessType;
            //txtProductType.Text = ProductType;
            //txtOrderCat.Text = OrderCat;
            //txtOrderType.Text = OrderType;
            //txtOrderNo.Text = OrderNo;
            //txtPrtyCode.Text = PrtyCode;
            //txtPrtyCode.Text = PrtyCode;

            //txtPrtyName.Text = PrtyName;
            //txtPiType.Text = PiType;
            //txtPiNo.Text = PiNo;
            //txtArticleCode.Text = ArticleCode;
            //txtArticaldesc.Text = Articaldesc;
            //txtUom.Text = Uom;

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
    protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            //int index = int.Parse(e.CommandArgument.ToString());
            //GridViewRow row = gvOrder.Rows[index];
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
            else if (e.CommandName == "ViewPacking")
            {
                string URL = "ViewPacking.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;

                URL = URL + "&ORDER_NO=" + lblORDER_NO.Text;
                URL = URL + "&PI_NO=" + lblPI_NO.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

            }
            else if (e.CommandName == "ViewMachineEntries")
            {
                string URL = "MachineEntries.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                URL = URL + "&PI_NO=" + lblPI_NO.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
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

            else if (e.CommandName == "ViewWIPStock")
            {

                string URL = "ViewWipStock.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                URL = URL + "&PI_NO=" + lblPI_NO.Text;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

            }
            else if (e.CommandName == "ViewIssueAgainstPA")
            {
                string URL = "ViewIssueAgainstPA.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                URL = URL + "&PI_NO=" + lblPI_NO.Text;
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
            else
            {

                // Do Nothing
            }
        }
        else if (e.CommandName == "Select")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = gvOrder.Rows[index];
            
            Label lblPRTY_NAME = (Label)row.FindControl("lblPRTY_NAME");

            Label lblARTICAL_DESC = (Label)row.FindControl("lblARTICAL_DESC");
            
            Label lblCOMP_CODE = (Label)row.FindControl("lblCOMP_CODE");
            Label lblBRANCH_CODE = (Label)row.FindControl("lblBRANCH_CODE");
            Label lblBranchName = (Label)row.FindControl("lblBranchName");

            Label lblBUSINESS_TYPE = (Label)row.FindControl("lblBUSINESS_TYPE");
            Label lblPRODUCT_TYPE = (Label)row.FindControl("lblPRODUCT_TYPE");
            Label lblORDER_CAT = (Label)row.FindControl("lblORDER_CAT");
            Label lblORDER_TYPE = (Label)row.FindControl("lblORDER_TYPE");
            Label lblORDER_NO = (Label)row.FindControl("lblORDER_NO");
            Label lblPI_TYPE = (Label)row.FindControl("lblPI_TYPE");
            Label lblPI_NO = (Label)row.FindControl("lblPI_NO");
            Label lblARTICAL_CODE = (Label)row.FindControl("lblARTICAL_CODE");
            Label lblSHADE_CODE = (Label)row.FindControl("lblSHADE_CODE");
            txtPartyName.Text = lblPRTY_NAME.Text;
            txtArticleCode.Text = lblARTICAL_DESC.Text;
            BindBranch();
            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByText(lblBranchName.Text));
            BindBussinessType();
 
            ddlBussinesstype.SelectedIndex = ddlBussinesstype.Items.IndexOf(ddlBussinesstype.Items.FindByText(lblBUSINESS_TYPE.Text));
            BindPRODUCT_TYPE();
 
            ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByText(lblPRODUCT_TYPE.Text));
            BindORDER_CAT(); 
            ddlOrderCat.SelectedIndex = ddlOrderCat.Items.IndexOf(ddlOrderCat.Items.FindByText(lblORDER_CAT.Text));
            BindORDER_TYPE(); 
            ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByText(lblORDER_TYPE.Text));
            BindPI_TYPE(); 
            ddlPiType.SelectedIndex = ddlPiType.Items.IndexOf(ddlPiType.Items.FindByText(lblPI_TYPE.Text));
            txtShadeCode.Text = lblSHADE_CODE.Text;  
       

        
        }
        
            
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    protected void gvOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvOrder.Rows[gvOrder.SelectedIndex].BackColor = System.Drawing.Color.Red ;
    }
    protected void btnOrderNo_Click(object sender, ImageClickEventArgs e)
    {
        SearchbyKeywords(); 
    }
    protected void gvOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetAllOrderCaptureApproval();

            gvOrder.PageIndex = e.NewPageIndex;
            gvOrder.DataBind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Grid View Paging.\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }

    }
}
