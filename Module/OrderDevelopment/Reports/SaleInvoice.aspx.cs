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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_OrderDevelopment_Reports_SaleInvoice : System.Web.UI.Page
{
    private string TRN_TYPE = string.Empty;
    protected void Page_InIt(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
            }
            DataSet dt = GetData();
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                GetReport(dt);
            }
            else
            {
                Common.CommonFuction.ShowMessage("No data available for invoice.");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }

    private void GetReport(DataSet dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            if (TRN_TYPE == "IYS26" || TRN_TYPE == "IYS27" || TRN_TYPE == "IYS29")
            {
                rDoc.Load(Server.MapPath(@"SaleInvoice1.rpt"));
                // rDoc.Load(Server.MapPath(@"DeleveryOrderSaleWork.rpt"));
            }
            else if (TRN_TYPE == "IYS27")
            {
                rDoc.Load(Server.MapPath(@"DeleveryOrderJobWork.rpt"));
            }
            else
            {
                rDoc.Load(Server.MapPath(@"InvoiceAgainstCR.rpt"));
            }

            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataSet GetData()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            string From = string.Empty;
            string To = string.Empty;
            string InvoiceFrom = string.Empty;
            string InvoiceTo = string.Empty;
            string BUSINESS_TYPE = string.Empty;
            string PRODUCT_TYPE = string.Empty;
            string ORDER_CAT = string.Empty;
            string ORDER_TYPE = string.Empty;
            string ORDER_PREFIX = string.Empty;
            string PRINT_INVOICE = string.Empty;

            if (Request.QueryString["FromNo"] != null)
            {
                From = Request.QueryString["FromNo"].ToString().Trim();
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = Request.QueryString["ToNo"].ToString().Trim();
            }

            if (Request.QueryString["BUSINESS_TYPE"] != null && Request.QueryString["BUSINESS_TYPE"] != "")
            {
                BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].ToString();
            }

            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"] != "")
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }

            if (Request.QueryString["ORDER_CAT"] != null && Request.QueryString["ORDER_CAT"] != "")
            {
                ORDER_CAT = Request.QueryString["ORDER_CAT"].ToString();
            }

            if (Request.QueryString["ORDER_TYPE"] != null && Request.QueryString["ORDER_TYPE"] != "")
            {
                ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
            }

            if (Request.QueryString["INVOICE_FROM"] != null && Request.QueryString["INVOICE_FROM"] != "")
            {
                InvoiceFrom = Request.QueryString["INVOICE_FROM"].ToString();
            }

            if (Request.QueryString["INVOICE_TO"] != null && Request.QueryString["INVOICE_TO"] != "")
            {
                InvoiceTo = Request.QueryString["INVOICE_TO"].ToString();
            }

            if (Request.QueryString["PRINT_INVOICE"] != null && Request.QueryString["PRINT_INVOICE"] != "")
            {
                PRINT_INVOICE = Request.QueryString["PRINT_INVOICE"].ToString();
                PRINT_INVOICE = PRINT_INVOICE.Replace("$", "'");
            }

            SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
            oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            if (TRN_TYPE == "IYS26" || TRN_TYPE == "IYS27")
            {
                dt = SaitexBL.Interface.Method.YRN_IR_MST.Yrn_Delevery_Order_Details(TRN_TYPE, oOD_CAPTURE_MST.COMP_CODE, oOD_CAPTURE_MST.BRANCH_CODE, oOD_CAPTURE_MST.YEAR.ToString(), int.Parse(InvoiceFrom), int.Parse(InvoiceTo), PRINT_INVOICE);
            }
            else if (TRN_TYPE == "IYS29")
            {
                dt = SaitexBL.Interface.Method.YRN_IR_MST.Yrn_Delevery_Order_Details1(TRN_TYPE, oOD_CAPTURE_MST.COMP_CODE, oOD_CAPTURE_MST.BRANCH_CODE, oOD_CAPTURE_MST.YEAR.ToString(), int.Parse(InvoiceFrom), int.Parse(InvoiceTo), PRINT_INVOICE);
            }
            else
            {
                dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForReport_SaleInvoice(oOD_CAPTURE_MST, From, To, InvoiceFrom, InvoiceTo, PRINT_INVOICE);
                dt1 = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForReport_AdjPacking_SaleInvoice(oOD_CAPTURE_MST, From, To, InvoiceFrom, InvoiceTo, PRINT_INVOICE);

            }
            DataSet DS = new DataSet();
            if (TRN_TYPE == "IYS26" || TRN_TYPE == "IYS27" || TRN_TYPE == "IYS29")
            {
                DS.Tables.Add(dt);
                DS.Tables[0].TableName = "DELEVERY_ORDER";
            }
            else
            {
                DS.Tables.Add(dt);
                DS.Tables.Add(dt1);
                DS.Tables[0].TableName = "V_SALE_INVOICE";
                DS.Tables[1].TableName = "V_YRN_IR_ISS_ADJ";
            }
            //if (!dt.Columns.Contains("PRINTED_BY"))
            //{
            //    dt.Columns.Add("PRINTED_BY", typeof(string));
            //}

            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["PRINTED_BY"] = oUserLoginDetail.Username;
            //}
            return DS;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateSectionHeight(ReportDocument reportDocument, String sectionName, int height)
    {
        Section section = reportDocument.ReportDefinition.Sections[sectionName];
        section.Height = height;
    }
}
