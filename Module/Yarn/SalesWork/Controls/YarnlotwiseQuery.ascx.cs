using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using errorLog;


public partial class Module_Yarn_SalesWork_Controls_YarnlotwiseQuery : System.Web.UI.UserControl
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
                row_boundgrd();
                bindCustomerRequestApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }
    public void bindCustomerRequestApproval()
    {
        try
        {
           
            DataTable dt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetYarnStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "", "", "", "", "", "","", "", "", "","","", ddlTRANTYPE.SelectedValue,"");
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
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string BranchName=oUserLoginDetail.CH_BRANCHCODE ;
        int  tdate= oUserLoginDetail.DT_STARTDATE.Year;
        string YarnCode = ((TextBox)gvStock.HeaderRow.FindControl("txtYarnCode")).Text;
        string YarnDesc = ((TextBox)gvStock.HeaderRow.FindControl("txtYarnDesc")).Text;
        string PoNumb = ((TextBox)gvStock.HeaderRow.FindControl("txtPoNumb")).Text;
        string LotNo = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).ToolTip;
        string grade = ((TextBox)gvStock.HeaderRow.FindControl("txtgrade")).Text;
        string NOOFUNIT = ((TextBox)gvStock.HeaderRow.FindControl("txtNOOFUNIT")).Text;
        string WEIGHTOFUNIT = ((TextBox)gvStock.HeaderRow.FindControl("txtWEIGHTOFUNIT")).Text;
        string SHADECODE = ((TextBox)gvStock.HeaderRow.FindControl("txtSHADECODE")).Text;
        string SHADEFAMILY = ((TextBox)gvStock.HeaderRow.FindControl("txtSHADEFAMILY")).Text;
        string RGB = ((TextBox)gvStock.HeaderRow.FindControl("txtRGB")).Text;
        string LOCATION = ((TextBox)gvStock.HeaderRow.FindControl("txtLOCATION")).Text;
        string STORE = ((TextBox)gvStock.HeaderRow.FindControl("txtStore")).Text;

        string URL = "../Reports/YARN_LOTWISE_STOCK_REPORT.aspx?BRANCHNAME=" + BranchName + "&TDATE=" + tdate + "&YarnCode=" + YarnCode + "&YarnDesc=" + YarnDesc + "&PoNumb=" + PoNumb + "&LotNo=" + LotNo + "&grade=" + grade + "&NOOFUNIT=" + NOOFUNIT + "&WEIGHTOFUNIT=" + WEIGHTOFUNIT + "&SHADECODE=" + SHADECODE + "&SHADEFAMILY=" + SHADEFAMILY + "&RGB=" + RGB + "&LOCATION=" + LOCATION + "&TRNTYPE=" + ddlTRANTYPE.SelectedValue +"&STORE=" + STORE;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }
    protected void printGrid_Click(object sender, EventArgs e)
    {

    }
    protected void FilterGrid_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
    public void SearchbyKeywords()
    {

        try
        {
            string YarnCode = ((TextBox)gvStock.HeaderRow.FindControl("txtYarnCode")).Text;
            string YarnDesc = ((TextBox)gvStock.HeaderRow.FindControl("txtYarnDesc")).Text;
            string PoNumb = ((TextBox)gvStock.HeaderRow.FindControl("txtPoNumb")).Text;
           // string LotNo = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).ToolTip;
            string LotNo = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text;
            string DYED_BATCH = ((TextBox)gvStock.HeaderRow.FindControl("txtBatchNo")).Text;
            string grade = ((TextBox)gvStock.HeaderRow.FindControl("txtgrade")).Text;
            string NOOFUNIT = ((TextBox)gvStock.HeaderRow.FindControl("txtNOOFUNIT")).Text;
            string WEIGHTOFUNIT = ((TextBox)gvStock.HeaderRow.FindControl("txtWEIGHTOFUNIT")).Text;
            string SHADECODE = ((TextBox)gvStock.HeaderRow.FindControl("txtSHADECODE")).Text;
            string SHADEFAMILY = ((TextBox)gvStock.HeaderRow.FindControl("txtSHADEFAMILY")).Text;
            string RGB = ((TextBox)gvStock.HeaderRow.FindControl("txtRGB")).Text;
            string LOCATION = ((TextBox)gvStock.HeaderRow.FindControl("txtLOCATION")).Text;
            string STORE = ((TextBox)gvStock.HeaderRow.FindControl("txtStore")).Text;

            DataTable dt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetYarnStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, YarnCode, YarnDesc, PoNumb, LotNo, DYED_BATCH, grade, NOOFUNIT, WEIGHTOFUNIT, SHADECODE, SHADEFAMILY, RGB, LOCATION, ddlTRANTYPE.SelectedValue, STORE);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvStock.DataSource = dt;
                gvStock.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            CalculateAllData();
            AutofillSearchContent(YarnCode, YarnDesc, PoNumb, LotNo, DYED_BATCH, grade, NOOFUNIT, WEIGHTOFUNIT, SHADECODE, SHADEFAMILY, RGB, LOCATION, STORE);
        }
        catch
        {
            throw;
        }
    }
    private void AutofillSearchContent(string YarnCode, string YarnDesc, string PoNumb, string LotNo, string DYED_BATCH, string grade, string NOOFUNIT, string WEIGHTOFUNIT, string SHADECODE, string SHADEFAMILY, string RGB, string LOCATION,string STORE)
    {
        try
        {
              ((TextBox)gvStock.HeaderRow.FindControl("txtYarnCode")).Text=YarnCode ;
              ((TextBox)gvStock.HeaderRow.FindControl("txtYarnDesc")).Text=YarnDesc;
             ((TextBox)gvStock.HeaderRow.FindControl("txtPoNumb")).Text=PoNumb;
             ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).ToolTip=LotNo;
             ((TextBox)gvStock.HeaderRow.FindControl("txtBatchNo")).ToolTip = DYED_BATCH;
             ((TextBox)gvStock.HeaderRow.FindControl("txtgrade")).Text=grade;
             ((TextBox)gvStock.HeaderRow.FindControl("txtNOOFUNIT")).Text=NOOFUNIT;
             ((TextBox)gvStock.HeaderRow.FindControl("txtWEIGHTOFUNIT")).Text=WEIGHTOFUNIT;
             ((TextBox)gvStock.HeaderRow.FindControl("txtSHADECODE")).Text=SHADECODE;
            ((TextBox)gvStock.HeaderRow.FindControl("txtSHADEFAMILY")).Text=SHADEFAMILY;
            ((TextBox)gvStock.HeaderRow.FindControl("txtRGB")).Text=RGB;
             ((TextBox)gvStock.HeaderRow.FindControl("txtLOCATION")).Text=LOCATION;
             ((TextBox)gvStock.HeaderRow.FindControl("txtStore")).Text = STORE;

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
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        string strFilename = "LotWise_Yarn_Stock_List_" + DateTime.Now.ToString() + ".xls";
        string YarnCode = ((TextBox)gvStock.HeaderRow.FindControl("txtYarnCode")).Text;
        string YarnDesc = ((TextBox)gvStock.HeaderRow.FindControl("txtYarnDesc")).Text;
        string PoNumb = ((TextBox)gvStock.HeaderRow.FindControl("txtPoNumb")).Text;
        string LotNo = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).ToolTip;
        string DYED_BATCH = ((TextBox)gvStock.HeaderRow.FindControl("txtBatchNo")).ToolTip;
        string grade = ((TextBox)gvStock.HeaderRow.FindControl("txtgrade")).Text;
        string NOOFUNIT = ((TextBox)gvStock.HeaderRow.FindControl("txtNOOFUNIT")).Text;
        string WEIGHTOFUNIT = ((TextBox)gvStock.HeaderRow.FindControl("txtWEIGHTOFUNIT")).Text;
        string SHADECODE = ((TextBox)gvStock.HeaderRow.FindControl("txtSHADECODE")).Text;
        string SHADEFAMILY = ((TextBox)gvStock.HeaderRow.FindControl("txtSHADEFAMILY")).Text;
        string RGB = ((TextBox)gvStock.HeaderRow.FindControl("txtRGB")).Text;
        string LOCATION = ((TextBox)gvStock.HeaderRow.FindControl("txtLOCATION")).Text;
        string STORE = ((TextBox)gvStock.HeaderRow.FindControl("txtStore")).Text;





        var data = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetYarnStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, YarnCode, YarnDesc, PoNumb, LotNo, DYED_BATCH, grade, NOOFUNIT, WEIGHTOFUNIT, SHADECODE, SHADEFAMILY, RGB, LOCATION, ddlTRANTYPE.SelectedValue, STORE);
        
        ExporttoExcel(data, strFilename, "Yarn Stock Details(Lot Wise)");
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
            double TotalGrossWt = 0;
            double TotalTareWt = 0;
            double TotalNetWt = 0;
            double TotalBale = 0;
            double Total = 0;
           
            for (int i = 0; i < gvStock.Rows.Count; i++)
            {
                double GrossWt = 0;
                double TareWt = 0;
                double NetWt = 0;
                double Bale = 0;  

                Label lblGrossWt = gvStock.Rows[i].FindControl("lblGrossWt") as Label;
                Label lblTareWt = gvStock.Rows[i].FindControl("lblTareWt") as Label;
                Label lblNetWt = gvStock.Rows[i].FindControl("lblNetWt") as Label;
                Label lblTotalBale = gvStock.Rows[i].FindControl("lblBale") as Label;

                double.TryParse(lblGrossWt.Text, out GrossWt);
                double.TryParse(lblTareWt.Text, out TareWt);
                double.TryParse(lblNetWt.Text, out NetWt);
                double.TryParse(lblTotalBale.Text, out Bale);


                TotalGrossWt = TotalGrossWt + GrossWt;
                TotalTareWt = TotalTareWt + TareWt;
                TotalNetWt = TotalNetWt + NetWt;
                TotalBale = TotalBale + Bale;
                Total = Total + 1;
               

            }

            ((Label)gvStock.FooterRow.FindControl("lblTotalGrossWt")).Text =  Math.Round(TotalGrossWt,3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalTareWt")).Text = Math.Round(TotalTareWt,3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalNetWt")).Text = Math.Round(TotalNetWt, 3).ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalBale")).Text = TotalBale.ToString();
            ((Label)gvStock.FooterRow.FindControl("lblTotalCartonNo")).Text = Total.ToString();
           

        }
    }

    protected void row_boundgrd()
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
            //DropDownList ddlbranch = (DropDownList)e.Row.FindControl("ddlBranchName");
            //DropDownList ddlTRANTYPE = (DropDownList)e.Row.FindControl("ddlTRANTYPE");
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

                DataTable dt = SaitexBL.Interface.Method.YRN_MST.getTransType();
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
        //}

    }


    protected void ddlTRANTYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }
}
