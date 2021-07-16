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

public partial class Module_Yarn_SalesWork_Reports_Dispatch_Print_Report : System.Web.UI.Page
{
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

            //rdoc.Load(Server.MapPath(@"ISSU_AGAINST_PA_REPORT.rpt"));
            rdoc.Load(Server.MapPath(@"Dispatch_Print_Report.rpt"));
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
            string YEAR = Request.QueryString["Year"].ToString();
            string YARN_CODE = Request.QueryString["YARN_CODE"].ToString();
             string Comp_Code = Request.QueryString["Comp_Code"].ToString();
            string Branch_Code = Request.QueryString["Branch_Code"].ToString();
            DateTime Sdate = DateTime.Parse(Request.QueryString["Sdate"].ToString());
            DateTime Edate = DateTime.Parse(Request.QueryString["Edate"].ToString());
            string TRN_TYPE="IYS26";
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.Yrn_Delevery_Order_Query_Form(TRN_TYPE, Comp_Code, Branch_Code, YEAR, Sdate, Edate, YARN_CODE);
            dt.Columns.Add("FROMDATE", typeof(DateTime));
            dt.Columns.Add("TODATE", typeof(DateTime));
            foreach (DataRow row in dt.Rows)
            {
                row["FROMDATE"] = Sdate;
                row["TODATE"] = Edate;
            }
            dt.TableName = "DISPATCH_PRINT_REPORT";

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