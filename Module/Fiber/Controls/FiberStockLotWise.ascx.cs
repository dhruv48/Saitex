using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class Module_Fiber_Controls_FiberStockLotWise : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            printGrid.Attributes.Add("onclick", "javascript:CallPrint('divPrint');");
           
            if (!IsPostBack)
            {
                //ViewState["Filter"] = "ALL";
                //DropDownList ddlbranch = (DropDownList)gvStock.HeaderRow.FindControl("ddlBranchName");
             
                bindCustomerRequestApproval();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }





   



    public DataTable bindCustomerRequestApproval()
    {
        try
        {

           
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetFiberStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","","","");
            if (dt != null && dt.Rows.Count > 0)
            {                
                gvStock.DataSource = dt;
                gvStock.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {               
                gvStock.DataSource = null;
                gvStock.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No record found.");
            }
            CalculateAllData();
            return dt;
        }
        catch
        {
            throw;
        }
    }


    protected void row_boundgrd(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //DropDownList ddlbranch = (DropDownList)e.Row.FindControl("ddlBranchName");
            DropDownList ddlTRANTYPE = (DropDownList)e.Row.FindControl("ddlTRANTYPE");
            try
            {
            //    DataTable dt = null;
            //    dt = new DataTable();
            //    dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectbranchMst();
            //    ddlbranch.Items.Clear();
            //    ddlbranch.DataSource = dt;
            //    ddlbranch.DataValueField = "BRANCH_NAME";
            //    ddlbranch.DataTextField = "BRANCH_NAME";
            //    ddlbranch.DataBind();
            //    ddlbranch.Items.Insert(0, new ListItem("--SELECT--", string.Empty));
            //    dt.Dispose();

                DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.getTransType();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlTRANTYPE.DataTextField = "TRN_DESC";
                    ddlTRANTYPE.DataValueField = "TRN_TYPE";
                    ddlTRANTYPE.DataSource = dt;
                    ddlTRANTYPE.DataBind();

                }
                ddlTRANTYPE.Items.Insert(0, new ListItem("--SELECT--", string.Empty));
                dt = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }


   



    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            string BranchName = string.Empty;
            string tdate = string.Empty;
            string trndesc = string.Empty;           
            string trnno = string.Empty;
            //string BranchName = ((DropDownList)gvStock.HeaderRow.FindControl("ddlBranchName")).SelectedValue;
            //string tdate = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDate")).Text;
            //string trndesc = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).Text;
            //string trntype = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).ToolTip;
            //string trnno = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnNo")).Text; 
            string trntype = ((DropDownList)gvStock.HeaderRow.FindControl("ddlTRANTYPE")).SelectedValue;
            string fibercode = string.Empty;// ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCode")).Text;
            string fiberdesc = ((TextBox)gvStock.HeaderRow.FindControl("txtFiberDesc")).Text;
            string fibercat = string.Empty;// ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCat")).Text;
            string finalrate = ((TextBox)gvStock.HeaderRow.FindControl("txtFinalRate")).Text;
            string lotno = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text;
            string totalbale = ((TextBox)gvStock.HeaderRow.FindControl("txtTotalBale")).Text;
            string issubale = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueBale")).Text;
            string balbale = ((TextBox)gvStock.HeaderRow.FindControl("txtBalBale")).Text;
            string weightofunit = ((TextBox)gvStock.HeaderRow.FindControl("txtWeightofUnit")).Text;
            string trnqty = ((TextBox)gvStock.HeaderRow.FindControl("txtQuantity")).Text;
            string totalvalue = ((TextBox)gvStock.HeaderRow.FindControl("txtTotalValue")).Text;
            string issueqty = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueQty")).Text;
            string issuevalue = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueValue")).Text;
            string balqty = ((TextBox)gvStock.HeaderRow.FindControl("txtBalQty")).Text;
            string balvalue = ((TextBox)gvStock.HeaderRow.FindControl("txtBalValue")).Text;
            string palletcode = ((TextBox)gvStock.HeaderRow.FindControl("txtpalletcode")).Text;
            string grade = ((TextBox)gvStock.HeaderRow.FindControl("txtgrade")).Text;
            string palletno = ((TextBox)gvStock.HeaderRow.FindControl("txtpalletno")).Text;







            string URL = "../Reports/FIBER_LOTWISE_STOCK_REPORT.aspx?BRANCHNAME=" + BranchName + "&TDATE=" + tdate + "&TRNDESC=" + trndesc + "&TRNTYPE=" + trntype + "&TRNNO=" + trnno + "&FIBERCODE=" + fibercode + "&FIBERDESC=" + fiberdesc + "&FIBERCAT=" + fibercat + "&FINALRATE=" + finalrate + "&LOTNO=" + lotno + "&TOTALBALE=" + totalbale + "&ISSUBALE=" + issubale + "&BALBALE=" + balbale + "&WEIGHTOFUNIT=" + weightofunit + "&TRNQTY=" + trnqty + "&TOATALVALUE=" + totalvalue + "&ISSUEQTY=" + issueqty + "&ISSUEVALUE=" + issuevalue + "&BALQTY=" + balqty + "&BALVALUE=" + balvalue + "&PALLETCODE=" + palletcode + "&GRADE=" + grade + "&PALLETNO=" + palletno;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        
        }
        catch
        {
            throw;
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void FilterGrid_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }



    public DataTable SearchbyKeywords()
    {

        try
        {
            string BranchName = string.Empty ;
            string tdate = string.Empty;
            string trndesc = string.Empty;           
            string trnno = string.Empty;

            //string BranchName = ((DropDownList)gvStock.HeaderRow.FindControl("ddlBranchName")).SelectedValue;
            //string tdate = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDate")).Text;
            //string trndesc = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).Text;
            //string trntype = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).ToolTip;
            //string trnno = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnNo")).Text;
            string trntype = ((DropDownList)gvStock.HeaderRow.FindControl("ddlTRANTYPE")).SelectedValue;
            string fibercode = string.Empty;// ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCode")).Text;
            string fiberdesc = string.Empty; //((TextBox)gvStock.HeaderRow.FindControl("txtFiberDesc")).Text;
            string fibercat = string.Empty; //((TextBox)gvStock.HeaderRow.FindControl("txtFiberCat")).Text;
            string finalrate = ((TextBox)gvStock.HeaderRow.FindControl("txtFinalRate")).Text;
            string lotno = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text;
            string totalbale = ((TextBox)gvStock.HeaderRow.FindControl("txtTotalBale")).Text;
            string issubale = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueBale")).Text;
            string balbale = ((TextBox)gvStock.HeaderRow.FindControl("txtBalBale")).Text;
            string weightofunit = ((TextBox)gvStock.HeaderRow.FindControl("txtWeightofUnit")).Text;
            string trnqty = ((TextBox)gvStock.HeaderRow.FindControl("txtQuantity")).Text;
            string totalvalue = ((TextBox)gvStock.HeaderRow.FindControl("txtTotalValue")).Text;
            string issueqty = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueQty")).Text;
            string issuevalue = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueValue")).Text;
            string balqty = ((TextBox)gvStock.HeaderRow.FindControl("txtBalQty")).Text;
            string balvalue = ((TextBox)gvStock.HeaderRow.FindControl("txtBalValue")).Text;
            string palletcode = ((TextBox)gvStock.HeaderRow.FindControl("txtpalletcode")).Text;
            string grade = ((TextBox)gvStock.HeaderRow.FindControl("txtgrade")).Text;
            string palletno = ((TextBox)gvStock.HeaderRow.FindControl("txtpalletno")).Text;
            string PartyName = ((TextBox)gvStock.HeaderRow.FindControl("txtPartyName")).Text;






            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetFiberStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "", BranchName, tdate, trndesc, trntype, trnno, fibercode, fiberdesc, fibercat, finalrate, lotno, totalbale, issubale, balbale, weightofunit, trnqty, totalvalue, issueqty, issuevalue, balqty, balvalue, palletcode, grade, palletno, PartyName);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvStock.DataSource = dt;
                gvStock.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                gvStock.DataSource = null;
                gvStock.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
              
            }
            CalculateAllData();
            AutofillSearchContent(BranchName, tdate, trndesc, trntype, trnno, fibercode, fiberdesc, fibercat, finalrate, lotno, totalbale, issubale, balbale, weightofunit, trnqty, totalvalue, issueqty, issuevalue, balqty, balvalue, palletcode, grade, palletno, PartyName);
            return dt;

        }
        catch
        {
            throw;
        }
    }

    private void AutofillSearchContent(string BranchName, string tdate, string trndesc, string trntype, string trnno, string fibercode, string fiberdesc, string fibercat, string finalrate, string lotno, string totalbale, string issubale, string balbale, string weightofunit, string trnqty, string totalvalue, string issueqty, string issuevalue, string balqty, string balvalue, string palletcode, string grade, string palletno, string PartyName)
    {
        try
        {
           //((DropDownList)gvStock.HeaderRow.FindControl("ddlBranchName")).SelectedValue = BranchName;
           //  ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDate")).Text=tdate ;
           //  ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).Text=trndesc;
           //  //((TextBox)gvStock.HeaderRow.FindControl("txtTrnType")).ToolTip=trntype;
            ((DropDownList)gvStock.HeaderRow.FindControl("ddlTRANTYPE")).SelectedValue = trntype;
             //((TextBox)gvStock.HeaderRow.FindControl("txtTrnNo")).Text=trnno;
             //((TextBox)gvStock.HeaderRow.FindControl("txtFiberCode")).Text=fibercode;
             ((TextBox)gvStock.HeaderRow.FindControl("txtFiberDesc")).Text=fiberdesc;
             //((TextBox)gvStock.HeaderRow.FindControl("txtFiberCat")).Text=fibercat;
             ((TextBox)gvStock.HeaderRow.FindControl("txtFinalRate")).Text=finalrate;
             ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text=lotno;
             ((TextBox)gvStock.HeaderRow.FindControl("txtTotalBale")).Text=totalbale;
             ((TextBox)gvStock.HeaderRow.FindControl("txtIssueBale")).Text=issubale;
             ((TextBox)gvStock.HeaderRow.FindControl("txtBalBale")).Text=balbale;
             ((TextBox)gvStock.HeaderRow.FindControl("txtWeightofUnit")).Text=weightofunit;
             ((TextBox)gvStock.HeaderRow.FindControl("txtQuantity")).Text=trnqty;
             ((TextBox)gvStock.HeaderRow.FindControl("txtTotalValue")).Text=totalvalue;
             ((TextBox)gvStock.HeaderRow.FindControl("txtIssueQty")).Text=issueqty;
             ((TextBox)gvStock.HeaderRow.FindControl("txtIssueValue")).Text=issuevalue;
             ((TextBox)gvStock.HeaderRow.FindControl("txtBalQty")).Text=balqty;
            ((TextBox)gvStock.HeaderRow.FindControl("txtBalValue")).Text=balvalue;
            ((TextBox)gvStock.HeaderRow.FindControl("txtpalletcode")).Text = palletcode;
            ((TextBox)gvStock.HeaderRow.FindControl("txtgrade")).Text = grade;
            ((TextBox)gvStock.HeaderRow.FindControl("txtpalletno")).Text = palletno;
            ((TextBox)gvStock.HeaderRow.FindControl("txtPartyName")).Text = PartyName; 

        }
        catch
        {
            throw;
        }

    }

    protected void gvStock_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvStock.PageIndex = e.NewPageIndex;
        bindCustomerRequestApproval();
    }

    protected void gvStock_PreRender1(object sender, EventArgs e)
    {
        gvStock.UseAccessibleHeader = true;
        //gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Lot_Wise_POY_Stock_List_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(SearchbyKeywords(), strFilename, "P.O.Y. Stock (Lot Wise)");
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

    protected void CalculateAllData()
    {
        if (gvStock.Rows.Count > 0)
        {
            double TotalBale = 0;
            double TotalQuantity=0;
            double TotalValue = 0;
            double TotalIssueBale = 0;
            double TotalIssueQty = 0;
            double TotalIssueValue = 0;
           double TotalBalBale = 0;
            double TotalBalQty = 0;
            double TotalBalValue = 0;

            double total = 0;




            for (int i = 0; i < gvStock.Rows.Count; i++)
            {
                double Bale = 0;
                double Quantity = 0;
                double Value = 0;
                double IssueBale = 0;
                double IssueQty = 0;
                double IssueValue = 0;
                double BalBale = 0;             
                double BalQty = 0;
                double BalValue = 0;
                
                Label lblBale = gvStock.Rows[i].FindControl("lblBale") as Label;
                Label lblQuantity = gvStock.Rows[i].FindControl("lblQuantity") as Label;
                Label lblValue = gvStock.Rows[i].FindControl("lblValue") as Label;
                Label lblIssueBale = gvStock.Rows[i].FindControl("lblIssueBale") as Label;
                Label lblIssueQty = gvStock.Rows[i].FindControl("lblIssueQty") as Label;
                Label lblIssueValue = gvStock.Rows[i].FindControl("lblIssueValue") as Label;
                Label lblBalBale = gvStock.Rows[i].FindControl("lblBalBale") as Label;
                Label lblBalQty = gvStock.Rows[i].FindControl("lblBalQty") as Label;
                Label lblBalValue = gvStock.Rows[i].FindControl("lblBalValue") as Label;

                double.TryParse(lblBale.Text, out Bale);
                double.TryParse(lblQuantity.Text, out Quantity);
                double.TryParse(lblValue.Text, out Value);
                double.TryParse(lblIssueBale.Text, out IssueBale);
                double.TryParse(lblIssueQty.Text, out IssueQty);
                double.TryParse(lblIssueValue.Text, out IssueValue);
                double.TryParse(lblBalBale.Text, out BalBale);
                double.TryParse(lblBalQty.Text, out BalQty);
                double.TryParse(lblBalValue.Text, out BalValue);


                TotalBale = TotalBale + Bale;
                TotalQuantity = TotalQuantity + Quantity;
                TotalValue = TotalValue + Value;
                TotalIssueBale = TotalIssueBale + IssueBale;
                TotalIssueQty = TotalIssueQty + IssueQty;
                TotalIssueValue = TotalIssueValue + IssueValue;
                TotalBalBale= TotalBalBale + BalBale;
                TotalBalQty = TotalBalQty + BalQty;
                TotalBalValue = TotalBalValue + BalValue;
                total = total + 1;
              


            }

            ((Label)gvStock.FooterRow.FindControl("lblTotalBale")).Text = TotalBale.ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalQuantity")).Text = Math.Round(TotalQuantity, 3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalValue")).Text = Math.Round(TotalValue, 3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalIssueBale")).Text = TotalIssueBale.ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalIssueQty")).Text = Math.Round(TotalIssueQty, 3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalIssueValue")).Text = Math.Round(TotalIssueValue, 3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalBalBale")).Text = TotalBalBale.ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalBalQty")).Text = Math.Round(TotalBalQty, 3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalBalValue")).Text = Math.Round(TotalBalValue, 3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalPallet")).Text = total.ToString();


        }
    }



}
