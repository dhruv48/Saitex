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

public partial class Module_OrderDevelopment_CustomerRequest_Reports_From_Stock_Sales : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string Report_Type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        DataSet dt = GetData();
        GetReport(dt);
    }


    private void GetReport(DataSet dt)
    {

        ReportDocument rDoc = new ReportDocument();
        string ReportPath = string.Empty;

        if (Report_Type == "From_Stock")
            ReportPath = Server.MapPath(@"From_Stock_Sales.rpt");
        else 
            ReportPath = Server.MapPath(@"InvoiceAgainstCRLIST1.rpt");
        rDoc.Load(ReportPath);
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    private DataSet GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            string FROM_DATE = "";
            string TO_DATE = "";
            //string Report_Type = string.Empty;

            if (Request.QueryString["FromNo"] != null)
            {
                FROM_DATE = Request.QueryString["FromNo"].ToString().Trim();
            }
            if (Request.QueryString["ToNo"] != null)
            {
                TO_DATE = Request.QueryString["ToNo"].ToString().Trim();
            }

            if (Request.QueryString["Report_Type"] != null)
            {
                Report_Type = Request.QueryString["Report_Type"].ToString().Trim();
            }

            var dt = GetDataFromStock(oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), FROM_DATE, TO_DATE, "DUPLICATE FOR TRANPORTOR", Report_Type);
            dt.Tables[0].TableName = "FROM_STOCK_SALE_CONTRACT";
            dt.Tables[0].Columns.Add("FROMDATE", typeof(DateTime));
            dt.Tables[0].Columns.Add("TODATE", typeof(DateTime));
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                row["FROMDATE"] = (DateTime.Parse(FROM_DATE)).ToString("dd/MM/yyyy");
                row["TODATE"] = (DateTime.Parse(TO_DATE)).ToString("dd/MM/yyyy"); ;
            }



            return dt;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }


    private DataSet GetDataFromStock(string BRANCH_CODE, string YEAR, string StDate, string EnDate, string PRINT_INVOICE, string Report_Type)
    {
        try
        {
            return SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GET_INVOICE_FROM_STOCK(BRANCH_CODE, YEAR, StDate, EnDate, PRINT_INVOICE, Report_Type);

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

    }
}