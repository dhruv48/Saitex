using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_Production_Controls_Production_Confirmation : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string PRODUCT_TYPE { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        ltrHeading.Text = PRODUCT_TYPE+ " Yarn";
        if (!IsPostBack)
        {
            bindDataGrid();
        }

    }

    private void bindDataGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetAllProdcutionOrderByDepartement(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), PRODUCT_TYPE,oUserLoginDetail.VC_BRANCHNAME.ToString(),"","","","","","","");
            if (dt != null && dt.Rows.Count > 0)
            {
                grdProductionDetails.DataSource = dt;
                grdProductionDetails.DataBind();
            }
            else
            {

                CommonFuction.ShowMessage("No Data For Production!.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdProductionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var grdRow = e.Row;
                
                var lblCompCode = ((Label)e.Row.FindControl("lblCompCode"));
                var lblBranchCode = ((Label)e.Row.FindControl("lblBranchCode"));
                var lblYear = ((Label)e.Row.FindControl("lblYear"));
                var lblBusinessType = ((Label)e.Row.FindControl("lblBusinessType"));
                var lblOrderType = ((Label)e.Row.FindControl("lblOrderType"));
                var lblOrderNo = ((Label)e.Row.FindControl("lblOrderNo"));               
                var lblProductionType = ((Label)e.Row.FindControl("lblProductionType"));
                var lblShade = (Label)e.Row.FindControl("lblShade");
                var lblArticalDesc = ((Label)e.Row.FindControl("lblArticalDesc"));
                var btnStatus = ((Button)e.Row.FindControl("btnStatus"));
                var btnProcessRoot = ((Button)e.Row.FindControl("btnProcessRoot"));
                var lblProcessRootConfig = ((Label)e.Row.FindControl("lblProcessRootConfig"));
                var lblParty = ((Label)e.Row.FindControl("lblPartyName"));

               

                DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetBaseArticleDetailsForProdcutionOrderByDepartement(lblCompCode.Text, lblBranchCode.Text, lblYear.Text, lblOrderNo.ToolTip, lblOrderNo.Text, lblBusinessType.Text, lblProductionType.ToolTip, lblProductionType.Text, "PRODUCT_SHADE", lblArticalDesc.ToolTip, lblShade.Text, "", oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), lblParty.ToolTip );
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView grdTRN = (GridView)grdRow.FindControl("grdTRN");
                    grdTRN.DataSource = dt;
                    grdTRN.DataBind();
                }
                bool result = false;
                double totalissueQty = 0;
                double totalSchQty = 0;
                double totalproductionQty = 0;
                double totalapprovedQty = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double approvedQty = 0;
                    double stockQty = 0;
                   double productionQty = 0;
                    double scheduledQty = 0;
                    double issQty = 0;
                    double.TryParse(dt.Rows[i]["REQ_QTY"].ToString(), out approvedQty);
                    double.TryParse(dt.Rows[i]["STOCK_QTY"].ToString(), out stockQty);
                    double.TryParse(dt.Rows[i]["PRODUCTION_QTY"].ToString(), out productionQty);
                    double.TryParse(dt.Rows[i]["SCHEDULED_QTY"].ToString(), out scheduledQty);
                    double.TryParse(dt.Rows[i]["ISS_QTY"].ToString(), out issQty);
                    totalissueQty = totalissueQty + issQty;
                    totalSchQty = totalSchQty + scheduledQty;
                    totalproductionQty = totalproductionQty + productionQty;
                    totalapprovedQty = totalapprovedQty + approvedQty;
                    if (approvedQty <= (stockQty+issQty))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        break;
                    }

                }
                if (result)
                {
                    btnStatus.ForeColor = System.Drawing.Color.Green;
                    if (!string.IsNullOrEmpty(btnStatus.Text))
                    {
                         if (totalapprovedQty <= totalproductionQty)
                        {
                            btnStatus.Text = "(" + totalproductionQty.ToString() + ") Done";
                            btnStatus.ForeColor = System.Drawing.Color.Black;
                            btnStatus.Font.Bold = true;

                        }
                         else if (totalissueQty > 0 && totalSchQty > 0)
                        {
                            btnStatus.Text = "(" + btnStatus.Text + ") WIP";
                        }
                        else if (totalissueQty > 0 && totalSchQty == 0)
                        {
                            btnStatus.Text =totalissueQty.ToString()+ "kg Issued";
                            
                        }
                        else if (totalissueQty== 0 && totalSchQty > 0)
                        {
                            btnStatus.Text = "(" + btnStatus.Text + ") Scheduled";
                        }
                        
                        else 
                        {
                            btnStatus.Text = "(" + btnStatus.Text + ") Ready"; 
                        }
                        
                    }
                    else 
                    { 
                        btnStatus.Text = "Ready"; 
                    }
                                      
                }
                else
                {
                    btnStatus.ForeColor = System.Drawing.Color.Red;
                    btnStatus.Text = "Waiting";                   
                }
               



                if (lblProcessRootConfig.Text.Equals("1"))
                {
                    btnProcessRoot.ForeColor = System.Drawing.Color.Green;
                    result = true;
                }
                else
                {
                    btnProcessRoot.ForeColor = System.Drawing.Color.Red;
                    btnProcessRoot.Text = "Assign";
                    result = false;                
                }
                btnStatus.Enabled = result;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting load of Approved Production Data.\r\nSee error log for detail."));
        }

    }

    protected void btnStatus_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;        
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        var lblCompCode = ((Label)gvr.FindControl("lblCompCode"));
        var lblBranchCode = ((Label)gvr.FindControl("lblBranchCode"));
        var lblYear = ((Label)gvr.FindControl("lblYear"));
        var lblBusinessType = ((Label)gvr.FindControl("lblBusinessType"));
        var lblOrderType = ((Label)gvr.FindControl("lblOrderType"));
        var lblOrderNo = ((Label)gvr.FindControl("lblOrderNo"));
        var lblProductionType = ((Label)gvr.FindControl("lblProductionType"));
        var lblShade = (Label)gvr.FindControl("lblShade");
        var lblArticalDesc = ((Label)gvr.FindControl("lblArticalDesc"));
        var lblArticalQty = ((Label)gvr.FindControl("lblArticalQty"));        
        var btnProcessRoot = ((Button)gvr.FindControl("btnProcessRoot"));
        var lblProcessRootConfig = ((Label)gvr.FindControl("lblProcessRootConfig"));
        var lblArticalType = ((Label)gvr.FindControl("lblArticalType"));
       Common.CommonFuction.ShowMessage("It will go to Production planning For: "+lblArticalDesc.Text+" Qty:"+ lblArticalQty.Text);
      
       
      
       string URL = "?ORDER_NO=" + lblOrderNo.ToolTip;
       URL = URL + "&COMP_CODE=" + lblCompCode.Text;
       URL = URL + "&BRANCH_CODE=" + lblBranchCode.Text;
        URL = URL + "&PI_TYPE=" + lblBusinessType.ToolTip;
       URL = URL + "&ARTICAL_TYPE=" + lblArticalType.Text;   
       URL = URL + "&PI_NO=" + lblOrderNo.Text;
       URL = URL + "&ARTICAL_CODE=" + lblArticalDesc.ToolTip;
       URL = URL + "&SHADE_CODE=" + lblShade.Text;       
       URL = URL + "&PROS_ROUTE_CODE=" +btnProcessRoot.Text; 
       URL = URL + "&YEAR=" + lblYear.Text;
       URL = URL + "&ORD_QTY=" + lblArticalQty.Text;
       Response.Redirect("~/Module/PlanningAndScheduling/Pages/OrderMachinePlanning.aspx" + URL, false);

       
    }
    protected void FilterGrid_Click(object sender, ImageClickEventArgs e)
    {
        SearchbyKeywords();
    }

    private void SearchbyKeywords()
    {
        try
        {
            var txtYear = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtYear")).Text;
            var txtOrderNo = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtOrderNo")).Text;
            var txtBtype = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtBtype")).Text;
            var txtProductDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtProductDesc")).Text;
            var txtSOQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtSOQty")).Text;
            //var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalDesc")).Text;
            var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtAssArticalDesc")).Text;
            var txtArticalShade = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalShade")).Text;
            var txtArticalQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalQty")).Text;



            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetAllProdcutionOrderByDepartement(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtYear, PRODUCT_TYPE, oUserLoginDetail.VC_BRANCHNAME.ToString(), txtOrderNo, txtBtype, txtProductDesc, txtSOQty, txtArticalDesc, txtArticalShade, txtArticalQty);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdProductionDetails.DataSource = dt;
                grdProductionDetails.DataBind();
               
            }
            else
            {
                grdProductionDetails.DataSource = null;
                grdProductionDetails.DataBind();
                Common.CommonFuction.ShowMessage("No approved Details Available");
               
                
            }
            AutofillSearchContent(txtYear, txtOrderNo, txtBtype, txtProductDesc, txtSOQty, txtArticalDesc, txtArticalShade,  txtArticalQty);
        }
             
    
       catch
       {
          throw;
        }
    }

    private void AutofillSearchContent(string YEAR, string ORDER_NO, string BTYPE, string PRODUCTION_DESC, string SQ_QTY, string ARTICLE_DESC, string SHADE,string QTY)
    {

        try
        {
            var txtYear = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtYear"));
            var txtOrderNo = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtOrderNo"));
            var txtBtype = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtBtype"));
            var txtProductDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtProductDesc"));
            var txtSOQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtSOQty"));
            //var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalDesc"));
            var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtAssArticalDesc"));
            var txtArticalShade = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalShade"));
            var txtArticalQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalQty"));
            txtYear.Text = YEAR ;
            txtOrderNo.Text = ORDER_NO ;
            txtBtype.Text = BTYPE ;
            txtProductDesc.Text = PRODUCTION_DESC ;
            txtSOQty.Text = SQ_QTY;
            txtArticalDesc.Text = ARTICLE_DESC;
            txtArticalShade.Text = SHADE;
            txtArticalQty.Text = QTY;
        }
        catch
        {
            throw;
        }

    }
    protected void grdProductionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProductionDetails.PageIndex = e.NewPageIndex;
        SearchbyKeywords(); 
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        var name = string.Empty;
        string strFilename = name + "ProductionDetails_" + PRODUCT_TYPE +"_Yarn_" +DateTime.Now.ToString() + ".xls";
        var txtYear = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtYear")).Text;
        var txtOrderNo = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtOrderNo")).Text;
        var txtBtype = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtBtype")).Text;
        var txtProductDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtProductDesc")).Text;
        var txtSOQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtSOQty")).Text;
        //var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalDesc")).Text;
        var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtAssArticalDesc")).Text;
        var txtArticalShade = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalShade")).Text;
        var txtArticalQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalQty")).Text;
        DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetAllProdcutionOrderByDepartement(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtYear, PRODUCT_TYPE, oUserLoginDetail.VC_BRANCHNAME.ToString(), txtOrderNo, txtBtype, txtProductDesc, txtSOQty, txtArticalDesc, txtArticalShade, txtArticalQty);
        ExporttoExcel(dt, strFilename, "Prduction Details List (" + PRODUCT_TYPE + " Yarn)");
    }
    private void ExporttoExcel(DataTable table, string name, string title)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;
        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(oUserLoginDetail.VC_COMPANYNAME);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:12.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(" " + title + " ");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD  align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("DATED:" + DateTime.Now.ToString() + "");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");


        HttpContext.Current.Response.Write("<TR>");

        foreach (DataColumn dtcol in table.Columns)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");

        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }




    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Production/Pages/PRODUCTION_PLANNING_CONFIRMATION.aspx?PRODUCT_TYPE="+PRODUCT_TYPE);
    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnProcessRoot_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        var lblCompCode = ((Label)gvr.FindControl("lblCompCode"));
        var lblBranchCode = ((Label)gvr.FindControl("lblBranchCode"));
        var lblYear = ((Label)gvr.FindControl("lblYear"));
        var lblBusinessType = ((Label)gvr.FindControl("lblBusinessType"));
        var lblOrderType = ((Label)gvr.FindControl("lblOrderType"));
        var lblOrderNo = ((Label)gvr.FindControl("lblOrderNo"));
        var lblProductionType = ((Label)gvr.FindControl("lblProductionType"));
        var lblShade = (Label)gvr.FindControl("lblShade");
        var lblArticalDesc = ((Label)gvr.FindControl("lblArticalDesc"));
        var lblArticalQty = ((Label)gvr.FindControl("lblArticalQty"));
        var btnProcessRoot = ((Button)gvr.FindControl("btnProcessRoot"));
        var lblProcessRootConfig = ((Label)gvr.FindControl("lblProcessRootConfig"));
        var lblArticalType = ((Label)gvr.FindControl("lblArticalType"));
        
        string URL = "../../../Module/OrderDevelopment/Pages/ProcessRouteBOM.aspx";
        URL = URL + "?COMP_CODE=" + lblCompCode.Text;
        URL = URL + "&BRANCH_CODE=" + lblBranchCode.Text;
        URL = URL + "&BUSINESS_TYPE=" + lblBusinessType.Text;        
        URL = URL + "&ORDER_CAT=" + lblOrderType.ToolTip;
        URL = URL + "&ORDER_TYPE=" + lblOrderType.Text;
        URL = URL + "&ORDER_NO=" + lblOrderNo.ToolTip;
        URL = URL + "&PI_TYPE=" + lblBusinessType.ToolTip;
        URL = URL + "&ARTICAL_TYPE=" + lblArticalType.Text;
        string productType = string.Empty;
        if (lblBusinessType.ToolTip.Equals("YARN TEXTURISING"))
        {
            productType = "TEXTURISED YARN";
        }
        else if(lblBusinessType.ToolTip.Equals("YARN TWISTING"))
        {
            productType = "TWISTED YARN";
        }

        else if (lblBusinessType.ToolTip.Equals("YARN DYEING")) 
        {
            productType = "DYED YARN";
        }
        URL = URL + "&PRODUCT_TYPE=" + productType;
        URL = URL + "&PI_NO=" + lblOrderNo.Text;
        URL = URL + "&ARTICAL_CODE=" + lblArticalDesc.ToolTip;
        URL = URL + "&SHADE_CODE=" + lblShade.Text;
        var root=(btnProcessRoot.Text.Equals("Assign"))?"" : btnProcessRoot.Text;
        URL = URL + "&PROS_ROUTE_CODE=" + root;
        URL = URL + "&PROCESS_ROUTE_FLAG=" + lblProcessRootConfig.Text;
        URL = URL + "&YEAR=" + lblYear.Text;
        URL = URL + "&ORD_QTY=" + lblArticalQty.Text;
       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);


        
    }
    protected void grdProductionDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
