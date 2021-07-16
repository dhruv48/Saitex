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
using CrystalDecisions.Shared;
using Common;
using errorLog;
public partial class Module_OrderDevelopment_CustomerRequest_Reports_SalesReport : System.Web.UI.Page
{
    string StDate = string.Empty;
    string EnDate = string.Empty;
    string YEAR = string.Empty;
    string BRANCH_CODE = string.Empty;
    string ORDER_NO = string.Empty;
    string ORDER_FROM = string.Empty;
    string PRINT_INVOICE = string.Empty;
    string PRODUCT_TYPE = string.Empty;
    string REPORT_TYPE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Session["SALE_CONTRACT"] != null)
            {
                var dt = (DataTable)Session["SALE_CONTRACT"];
                StDate = dt.Rows[0]["StDate"].ToString();
                EnDate = dt.Rows[0]["EnDate"].ToString();
                BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
                YEAR = dt.Rows[0]["YEAR"].ToString();
                ORDER_NO = dt.Rows[0]["ORDER_NO"].ToString();
                ORDER_FROM = dt.Rows[0]["ORDER_FROM"].ToString();
                PRINT_INVOICE = dt.Rows[0]["PRINT_INVOICE"].ToString();
                PRINT_INVOICE = PRINT_INVOICE.Replace("$", "'");
                var dtrportdat = GetData(BRANCH_CODE, YEAR, StDate, EnDate, ORDER_NO, ORDER_FROM, PRINT_INVOICE);
                dtrportdat.Tables[0].TableName = "SALE_CONTRACT";
                dtrportdat.Tables[1].TableName = "SALE_CONTRACT1";

                if (Request.QueryString["REPORT_TYPE"] != null)
                {
                    REPORT_TYPE = Request.QueryString["REPORT_TYPE"].ToString();
                }
                else
                {
                    REPORT_TYPE = string.Empty;
                }
                GetReport(dtrportdat, dt.Rows[0]["PRODUCT_TYPE"].ToString(), REPORT_TYPE.ToString());
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }

    }

    private void GetReport(DataSet dt, string PRODUCT_TYPE, string REPORT_TYPE)
    {
        try
        {
            var rDoc = new ReportDocument();
            if (PRODUCT_TYPE == "WASTE")
                rDoc.Load(Server.MapPath(@"InvoiceAgainstCRForWaste.rpt"));
            else
            {

                if (REPORT_TYPE == "LIST")
                {
                    rDoc.Load(Server.MapPath(@"InvoiceAgainstCRLIST.rpt"));
                }
                else
                {
                    rDoc.Load(Server.MapPath(@"InvoiceAgainstCR.rpt"));
                }
            }
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }




    private DataSet GetData(string BRANCH_CODE, string YEAR, string StDate, string EnDate, string ORDER_NO, string ORDER_FROM, string PRINT_INVOICE)
    {
        try
        {
            return SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GET_INVOICE(BRANCH_CODE, YEAR, StDate, EnDate, ORDER_NO, ORDER_FROM, PRINT_INVOICE);

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

    }

}
