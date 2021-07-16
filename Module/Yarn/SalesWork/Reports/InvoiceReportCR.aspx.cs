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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.OracleClient;

public partial class Module_Yarn_SalesWork_Reports_InvoiceReportCR : System.Web.UI.Page
{
    string INVOICE_TYPE = string.Empty;
    string PRINT_INVOICE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rdoc = new ReportDocument();
            //if (TRN_TYPE == "IYS26")
            //{
                rdoc.Load(Server.MapPath(@"InvoiceReportCR.rpt"));
            //}
            //else if (TRN_TYPE == "IYS27")
           // {
               // rdoc.Load(Server.MapPath(@"InvoiceReportCRJobWork.rpt"));
           // }

            rdoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rdoc;
        }
        catch
        { 
        
        }

    }

    private DataTable GetData()
    {
        try
        {
            int From = 0;
            int To = 0;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["From"] != null && Request.QueryString["From"] != "")
            {
                From = int.Parse(Request.QueryString["From"].ToString().Trim());
            }
            if (Request.QueryString["To"] != null && Request.QueryString["To"] != "")
            {
                To = int.Parse(Request.QueryString["To"].ToString().Trim());
            }
            if (Request.QueryString["INVOICE_TYPE"] != null && Request.QueryString["INVOICE_TYPE"] != "")
            {
                INVOICE_TYPE = Request.QueryString["INVOICE_TYPE"].ToString().Trim();
            }
            if (Request.QueryString["PRINT_INVOICE"] != null && Request.QueryString["PRINT_INVOICE"] != "")
            {
                PRINT_INVOICE = Request.QueryString["PRINT_INVOICE"].ToString().Trim();
                PRINT_INVOICE = PRINT_INVOICE.Replace("$", "'");
                PRINT_INVOICE = PRINT_INVOICE.Replace("'", "");
            }

            DataTable dt = SaitexBL.Interface.Method.YRN_INT_MST.GetInvoiceDataForPrint(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, INVOICE_TYPE, From, To, PRINT_INVOICE);
            return dt;
          }

        catch (OracleException ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }

 


}
