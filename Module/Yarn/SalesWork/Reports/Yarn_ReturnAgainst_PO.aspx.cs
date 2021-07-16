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
public partial class Module_Yarn_SalesWork_Reports_Yarn_ReturnAgainst_PO : System.Web.UI.Page
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

            rdoc.Load(Server.MapPath(@"Yarn_ReturnAgainst_PO.rpt"));

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
            string BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
            string COMP_CODE = Request.QueryString["COMP_CODE"].ToString();
            int PO_NUM = 0;
            int.TryParse(Request.QueryString["PO_NUM"].ToString(), out PO_NUM);
            int MRN_NUM = 0;
            int.TryParse(Request.QueryString["MRN_NUM"].ToString(), out MRN_NUM);
            string PRTY_CODE = Request.QueryString["PRTY_CODE"].ToString();

            string FROM_DATE = Request.QueryString["FROM_DATE"].ToString();
            string TO_DATE = Request.QueryString["TO_DATE"].ToString();
            string YARN_CODE = Request.QueryString["YARN_CODE"].ToString();

            int YEAR = int.Parse(Request.QueryString["YEAR"].ToString());
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.RECORDRETURNAGAINSTPO(BRANCH_CODE, COMP_CODE, PO_NUM, MRN_NUM, PRTY_CODE, FROM_DATE, TO_DATE, YARN_CODE, YEAR);
            dt.Columns.Add("FROMDATE", typeof(DateTime));
            dt.Columns.Add("TODATE", typeof(DateTime));
            foreach (DataRow row in dt.Rows)
            {
                row["FROMDATE"] = FROM_DATE;
                row["TODATE"] = TO_DATE;
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