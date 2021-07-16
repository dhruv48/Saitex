using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;


public partial class Module_Production_Controls_Production_Issue_Confirmation : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string PRODUCT_TYPE { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            Session["checkStock"] = null;
            bindDataGrid();
        }

    }

    private void bindDataGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetAllProdcutionIssueByDepartement1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), PRODUCT_TYPE, oUserLoginDetail.VC_BRANCHNAME.ToString(), "", "", "", "", "", "", "","");
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
        catch(Exception ex)
        {
            throw ex ;
        }
    }

    protected void grdProductionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["checkStock"] = null;
            }

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
                var btnIndent = ((Button)e.Row.FindControl("btnIndent"));
                var lblParty = ((Label)e.Row.FindControl("lblPartyName"));
                DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetBaseArticleDetailsForProdcutionOrderByDepartement(lblCompCode.Text, lblBranchCode.Text, lblYear.Text, lblOrderNo.ToolTip, lblOrderNo.Text, lblBusinessType.Text, lblProductionType.ToolTip, lblProductionType.Text, "PRODUCT_SHADE", lblArticalDesc.ToolTip, lblShade.Text, "", oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), lblParty.ToolTip );
                
             
                if (!dt.Columns.Contains("BAL_QTY"))
                {
                    dt.Columns.Add("BAL_QTY");
                }
                
                if (Session["checkStock"] != null)
                {
                    DataTable dtStock = (DataTable)Session["checkStock"];
                    DataView dvStock = dtStock.DefaultView;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    { 
                        dvStock.RowFilter = "BASE_ARTICAL_CODE='" + dt.Rows[i]["BASE_ARTICAL_CODE"].ToString() + "' AND BASE_SHADE_CODE='" + dt.Rows[i]["BASE_SHADE_CODE"].ToString() + "'";
                        if (dvStock.Count < 1)
                        {
                            DataRow dr = dtStock.NewRow();
                            dr["BASE_ARTICAL_CODE"] = dt.Rows[i]["BASE_ARTICAL_CODE"].ToString();
                            dr["BASE_SHADE_CODE"] = dt.Rows[i]["BASE_SHADE_CODE"].ToString();
                            dr["REQ_QTY"] = dt.Rows[i]["REQ_QTY"].ToString();
                            dr["STOCK_QTY"] = dt.Rows[i]["STOCK_QTY"].ToString();
                            dr["BAL_QTY"] = dt.Rows[i]["STOCK_QTY"].ToString();
                            dt.Rows[i]["BAL_QTY"] = dt.Rows[i]["STOCK_QTY"].ToString();
                            dtStock.Rows.Add(dr);

                        }
                        else 
                        {
                           
                            double reqQty = 0;
                            double balQty = 0;
                            double.TryParse(dt.Rows[i]["REQ_QTY"].ToString(), out reqQty);
                            double.TryParse( dvStock[0]["BAL_QTY"].ToString(), out balQty);
                            dt.Rows[i]["BAL_QTY"] = balQty - reqQty;
                            dvStock[0]["BAL_QTY"] = balQty - reqQty;
                        }
                        dtStock.AcceptChanges();
                    }
                    Session["checkStock"] = dtStock;

                }
                else
                {
                                       
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double reqQty = 0;
                        double balQty = 0;
                        double.TryParse(dt.Rows[i]["REQ_QTY"].ToString(), out reqQty);
                        double.TryParse(dt.Rows[i]["STOCK_QTY"].ToString(), out balQty);
                        dt.Rows[i]["BAL_QTY"] = balQty - reqQty;
                       
                       
                    }
                    dt.AcceptChanges();
                    Session["checkStock"] = dt;
                }
                
                //********************************//
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView grdTRN = (GridView)grdRow.FindControl("grdTRN");
                    grdTRN.DataSource = dt;
                    grdTRN.DataBind();
                }
                bool result = false;
                bool result1 = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    double balQty = 0;
                    double issQty = 0;
                    double adjQty = 0;
                    double.TryParse(dt.Rows[i]["BAL_QTY"].ToString(), out balQty);
                    double.TryParse(dt.Rows[i]["ISS_QTY"].ToString(), out issQty);
                    double.TryParse(dt.Rows[i]["ADJ_QTY"].ToString(), out adjQty);


                    if ((balQty + issQty) > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        if (adjQty > 0)
                        {
                            result1 = true;
                        }
                        else 
                        {
                            result1 = false;
                        }
                        break;
                    }
                    

                }
                if (result)
                {
                    btnStatus.ForeColor = System.Drawing.Color.DarkGreen;
                    btnStatus.Text = "Ready To Issue";

                    btnIndent.ForeColor = System.Drawing.Color.Green;
                    btnIndent.Text = "In Stock";
                }
                else
                {
                    btnStatus.ForeColor = System.Drawing.Color.DarkRed;
                    btnStatus.Text = "Not In Stock";

                    if (result1)
                    {
                        btnIndent.ForeColor = System.Drawing.Color.DarkViolet;
                        btnIndent.Text = "Indented";
                    }
                    else
                    {
                        btnIndent.ForeColor = System.Drawing.Color.Red;
                        btnIndent.Text = "Indent";
                    }

                    
                }
                btnStatus.Enabled = result;
                btnIndent.Enabled = !result;
                
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting load of Approved Production Data.\r\nSee error log for detail."));
        }

    }

    protected void btnStatus_Click(object sender, EventArgs e)
    {
        Session["checkStock"] = null;
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        //var lblCompCode = ((Label)gvr.FindControl("lblCompCode"));
        //var lblBranchCode = ((Label)gvr.FindControl("lblBranchCode"));
        //var lblYear = ((Label)gvr.FindControl("lblYear"));
        //var lblBusinessType = ((Label)gvr.FindControl("lblBusinessType"));
        //var lblOrderType = ((Label)gvr.FindControl("lblOrderType"));       
        //var lblProductionType = ((Label)gvr.FindControl("lblProductionType"));
        //var lblShade = (Label)gvr.FindControl("lblShade");        
        var lblArticalQty = ((Label)gvr.FindControl("lblArticalQty"));
        var lblArticalDesc = ((Label)gvr.FindControl("lblArticalDesc"));
        var lblOrderNo = ((Label)gvr.FindControl("lblOrderNo"));
        Common.CommonFuction.ShowMessage("It will go to Production planning For Order No: " + lblOrderNo.ToolTip + " PI NO: " + lblOrderNo .Text+ " and Article Details :" +lblArticalDesc.Text + " Qty:" + lblArticalQty.Text);



        if (Request.QueryString["TYPE"] != null)
        {
            if (Request.QueryString["TYPE"].ToString().Equals("POY"))
            {
                //Response.Redirect("~/Module/Fiber/Pages/FiberIssueAgnstPA.aspx?PI_NO=" + lblOrderNo.Text.Trim() + "&ORDER_CODE=" + lblOrderNo.ToolTip.Trim());

                Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_Issue_Agnst_PA1.aspx?PI_NO=" + lblOrderNo.Text.Trim() + "&ORDER_CODE=" + lblOrderNo.ToolTip.Trim());
            }
            else if (Request.QueryString["TYPE"].ToString().Equals("TEXTURISED"))
            {
                Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_Issue_Agnst_PA1.aspx?PI_NO=" + lblOrderNo.Text.Trim() + "&ORDER_CODE=" + lblOrderNo.ToolTip.Trim());

            }
            else if (Request.QueryString["TYPE"].ToString().Equals("DYEING"))
            {
                //Response.Redirect("~/Module/Fiber/Pages/FiberIssueAgnstPA.aspx?PI_NO=" + lblOrderNo.Text.Trim() + "&ORDER_CODE=" + lblOrderNo.ToolTip.Trim());

            }
        }
        
        

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
            var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalDesc")).Text;
            var txtArticalShade = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalShade")).Text;
            var txtArticalQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalQty")).Text;
           
            var txtPlanning = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtPlanning")).Text;

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetAllProdcutionIssueByDepartement(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtYear, PRODUCT_TYPE, oUserLoginDetail.VC_BRANCHNAME.ToString(), txtOrderNo, txtBtype, txtProductDesc, txtSOQty, txtArticalDesc, txtArticalShade, txtArticalQty, txtPlanning.Trim());
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
            AutofillSearchContent(txtYear, txtOrderNo, txtBtype, txtProductDesc, txtSOQty, txtArticalDesc, txtArticalShade, txtArticalQty,txtPlanning);
        }


        catch
        {
            throw;
        }
    }

    private void AutofillSearchContent(string YEAR, string ORDER_NO, string BTYPE, string PRODUCTION_DESC, string SQ_QTY, string ARTICLE_DESC, string SHADE, string QTY,string PLANNING_DATE)
    {

        try
        {
            var txtYear = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtYear"));
            var txtOrderNo = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtOrderNo"));
            var txtBtype = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtBtype"));
            var txtProductDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtProductDesc"));
            var txtSOQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtSOQty"));
            var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalDesc"));
            var txtArticalShade = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalShade"));
            var txtArticalQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalQty"));
            var txtPlanning = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtPlanning"));
            txtYear.Text = YEAR;
            txtOrderNo.Text = ORDER_NO;
            txtBtype.Text = BTYPE;
            txtProductDesc.Text = PRODUCTION_DESC;
            txtSOQty.Text = SQ_QTY;
            txtArticalDesc.Text = ARTICLE_DESC;
            txtArticalShade.Text = SHADE;
            txtArticalQty.Text = QTY;
            txtPlanning.Text = PLANNING_DATE;
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
        var txtPlanning = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtPlanning")).Text;

        Response.Redirect("~/Module/Yarn/SalesWork/Reports/Production_Issue_Confirmation.aspx?COMP_CODE=" + oUserLoginDetail.COMP_CODE + "&BRANCH_CODE=" + oUserLoginDetail.CH_BRANCHCODE + "&YEAR=" + oUserLoginDetail.DT_STARTDATE.Year.ToString() + "&PRODUCT_TYPE=" + PRODUCT_TYPE + "&BRANCH_NAME=" + oUserLoginDetail.VC_BRANCHNAME.ToString() + "&PLANNING_DATE=" + txtPlanning);

    }
    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        var name = string.Empty;
        string strFilename = name + "POY_Allocation_For_Production_" + DateTime.Now.ToString() + ".xls";
        var txtYear = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtYear")).Text;
        var txtOrderNo = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtOrderNo")).Text;
        var txtBtype = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtBtype")).Text;
        var txtProductDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtProductDesc")).Text;
        var txtSOQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtSOQty")).Text;
        var txtArticalDesc = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalDesc")).Text;
        var txtArticalShade = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalShade")).Text;
        var txtArticalQty = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtArticalQty")).Text;
        var txtPlanning = ((TextBox)grdProductionDetails.HeaderRow.FindControl("txtPlanning")).Text;
        //DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetAllProdcutionIssueByDepartement(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtYear, PRODUCT_TYPE, oUserLoginDetail.VC_BRANCHNAME.ToString(), txtOrderNo, txtBtype, txtProductDesc, txtSOQty, txtArticalDesc, txtArticalShade, txtArticalQty);

        DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetAllProdcutionIssueByDepartement2(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), PRODUCT_TYPE, oUserLoginDetail.VC_BRANCHNAME.ToString(), "", "", "", "", "", "", "", txtPlanning);
    
      ExporttoExcel(dt, strFilename, "Yarn Allocation For Production");
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
        Session["checkStock"] = null;
    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnIndent_Click(object sender, EventArgs e)
    {
        Session["checkStock"] = null;
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        //var lblCompCode = ((Label)gvr.FindControl("lblCompCode"));
        //var lblBranchCode = ((Label)gvr.FindControl("lblBranchCode"));
        //var lblYear = ((Label)gvr.FindControl("lblYear"));
        //var lblBusinessType = ((Label)gvr.FindControl("lblBusinessType"));
        //var lblOrderType = ((Label)gvr.FindControl("lblOrderType"));       
        //var lblProductionType = ((Label)gvr.FindControl("lblProductionType"));
        //var lblShade = (Label)gvr.FindControl("lblShade");        
        var lblArticalQty = ((Label)gvr.FindControl("lblArticalQty"));
        var lblArticalDesc = ((Label)gvr.FindControl("lblArticalDesc"));
        var lblOrderNo = ((Label)gvr.FindControl("lblOrderNo"));
        Common.CommonFuction.ShowMessage("It will go to Indent For Order No: " + lblOrderNo.ToolTip + " PI NO: " + lblOrderNo .Text+ " and Article Details :" +lblArticalDesc.Text + " Qty:" + lblArticalQty.Text);
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_Indent.aspx?PI_NO=" + lblOrderNo.Text.Trim() + "&ORDER_NO=" + lblOrderNo.ToolTip.Trim(), false);


    }
}
