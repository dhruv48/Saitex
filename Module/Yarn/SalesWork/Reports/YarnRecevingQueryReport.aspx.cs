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

public partial class Module_Yarn_SalesWork_Reports_YarnRecevingQueryReport : System.Web.UI.Page
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

            rdoc.Load(Server.MapPath(@"YarnRecevingReport.rpt"));

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

            string Party_Code = Request.QueryString["Party_Code"].ToString();
            string YARN_CODE = Request.QueryString["YARN_CODE"].ToString();
            string STORE = Request.QueryString["STORE"].ToString();
            string Comp_Code = Request.QueryString["Comp_Code"].ToString();
            string Branch_Code = Request.QueryString["Branch_Code"].ToString();
            string Sdate = Request.QueryString["Sdate"].ToString();
            string Edate = Request.QueryString["Edate"].ToString();
            int FromTrnNumb = Convert.ToInt32(Request.QueryString["FromTrnNumb"].ToString());
            int ToTrnNumb = Convert.ToInt32(Request.QueryString["ToTrnNumb"].ToString());
            string YARN_SHADE_FAMILY = Request.QueryString["YARN_SHADE_FAMILY"].ToString();
            string YARN_SHADE = Request.QueryString["YARN_SHADE"].ToString();
            string TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
            string LOCATION = Request.QueryString["LOCATION"].ToString();
            string TRN_DESC = Request.QueryString["TRN_DESC"].ToString();
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.YrnRecievingForQueryForm(Party_Code, YARN_CODE, Sdate, Edate, FromTrnNumb, ToTrnNumb, LOCATION, STORE, YARN_SHADE_FAMILY, YARN_SHADE, TRN_TYPE);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("FromDate", typeof(DateTime));
                dt.Columns.Add("ToDate", typeof(DateTime));
                dt.Columns.Add("TRN_DESC", typeof(String));
                foreach (DataRow row in dt.Rows)
                {
                    row["FromDate"] = Sdate;
                    row["ToDate"] = Edate;
                    row["TRN_DESC"] = TRN_DESC;
                }
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