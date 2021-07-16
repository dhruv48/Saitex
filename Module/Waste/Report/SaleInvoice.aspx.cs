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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetData();
            if (dt != null && dt.Rows.Count > 0)
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

    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"InvoiceAgainstCR.rpt"));
            
          
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
            
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetData()
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

            SaitexDM.Common.DataModel.TX_WASTE_IR_MST oTX_WASTE_IR_MST = new SaitexDM.Common.DataModel.TX_WASTE_IR_MST();

            oTX_WASTE_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_WASTE_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_WASTE_IR_MST.BUSINESS_TYPE = BUSINESS_TYPE;
            oTX_WASTE_IR_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oTX_WASTE_IR_MST.ORDER_CAT = ORDER_CAT;
            oTX_WASTE_IR_MST.ORDER_TYPE = ORDER_TYPE;

            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_IR_MST.GetDataForReport_SaleInvoice(oTX_WASTE_IR_MST, From, To, InvoiceFrom, InvoiceTo, PRINT_INVOICE);


            return dt;
        }
        catch
        {
            throw;
        }
    }

    private void UpdateSectionHeight(ReportDocument reportDocument, String sectionName, int height)
    {

        Section section = reportDocument.ReportDefinition.Sections[sectionName];

        section.Height = height;

    }
}
