using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;

public partial class Module_Production_Pages_PackingSummayDetail_Date : System.Web.UI.Page
{
    string Packing_Date_From;
    string Packing_Date_To;
    string url = "";
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialControl();  
        }
    }

    protected void InitialControl()
    {
        txtPackingDateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtPackingDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
    
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        if (txtPackingDateFrom.Text != null && txtPackingDateTo.Text != string.Empty)
        {
            Packing_Date_From = txtPackingDateFrom.Text;
        }
        else
        {
            Packing_Date_From = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        if (txtPackingDateTo.Text != null && txtPackingDateTo.Text != string.Empty)
        {
            Packing_Date_To = txtPackingDateTo.Text;
        }
        else
        {
            Packing_Date_To = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        string chk = string.Empty;
        if (redForQuery.SelectedValue == "red")
        {
            chk = "0";
        }
        else if (redForQuery.SelectedValue == "green")
        {
            chk = "1";
        }
        else if (redForQuery.SelectedValue == "blue")
        {
            chk = "2";
        }
        url = "../Report/PackingSummarydetail_Report.aspx?Packing_Date_From=" + Packing_Date_From + "&Packing_Date_To=" + Packing_Date_To+"&chk="+ chk;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialControl();
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
            throw ex;
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    { 
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        string strFilename = string.Empty;
        string strTitle = string.Empty;
        DataTable dt = null;
        DateTime DT_FROM = DateTime.Now.Date;
        DateTime DT_TO = DateTime.Now.Date;
        DateTime.TryParse(txtPackingDateFrom.Text , out DT_FROM);
        DateTime.TryParse(txtPackingDateTo.Text, out DT_TO);
        if (redForQuery.SelectedValue == "red")
        {
            strFilename = "PACKING_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "PACKING DETAILS";

            dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPackingSummaryForm(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.VC_DEPARTMENTCODE, DT_FROM, DT_TO);
        }
        else if (redForQuery.SelectedValue == "green")
        {
            strFilename = "LOT_WISE_PACKIING_" + DateTime.Now.ToString() + ".xls";
            strTitle = "LOT WISE PACKING";
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPackingSummaryFormLot_Wise(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.VC_DEPARTMENTCODE, DT_FROM, DT_TO);
        }
        else if (redForQuery.SelectedValue == "blue")
        {
            strFilename = "LOT_WISE_PACKIING_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "LOT WISE PACKIING DETAILS";

            dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPackingSummaryFormLot_Wise_DETAILS(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.VC_DEPARTMENTCODE, DT_FROM, DT_TO);
        }
        
        ExporttoExcel(dt, strFilename, strTitle);

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

}
