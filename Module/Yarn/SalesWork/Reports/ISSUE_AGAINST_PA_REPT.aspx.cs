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

public partial class Module_Yarn_SalesWork_Reports_ISSUE_AGAINST_PA_REPT : System.Web.UI.Page
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
            
            rdoc.Load(Server.MapPath(@"ISSU_AGAINST_PA_REPORT.rpt"));
           
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
            string ORDER_NO = Request.QueryString["PI_NO"].ToString();
            string YARN_CODE = Request.QueryString["YARN_CODE"].ToString();
            string LOT_NO = Request.QueryString["LOT_NO"].ToString();
            string Comp_Code = Request.QueryString["Comp_Code"].ToString();
            string Branch_Code = Request.QueryString["Branch_Code"].ToString();
            DateTime Sdate = DateTime.Parse(Request.QueryString["Sdate"].ToString());
            DateTime Edate = DateTime.Parse(Request.QueryString["Edate"].ToString());
            string MACHINE_CODE = Request.QueryString["MACHINE_CODE"].ToString();
            DataTable dt = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetDataForRecipeGried(YEAR, ORDER_NO, YARN_CODE, LOT_NO, Comp_Code, Branch_Code, Sdate, Edate, MACHINE_CODE);
            dt.Columns.Add("FromDate", typeof(DateTime));
            dt.Columns.Add("ToDate", typeof(DateTime));
            foreach (DataRow row in dt.Rows)
            {
                row["FromDate"] = Sdate;
                row["ToDate"] = Edate;
            }
               
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