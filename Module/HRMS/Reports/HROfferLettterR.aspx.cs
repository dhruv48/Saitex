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
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_HRMS_Pages_HROfferLettterR : System.Web.UI.Page
{
    private static string RefNo = "";
    private static string Ref = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["RefNo"] != null && Request.QueryString["Ref"] != null)
        {
            RefNo = Request.QueryString["RefNo"].ToString().Trim();
            Ref = Request.QueryString["Ref"].ToString().Trim(); 
        }        
        DataTable dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"HROfferLetter.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_OFFER_LET.GetReportData(RefNo);
            dt.Columns.Add("OFF_REF_NO1", typeof(string));
            dt.Rows[0]["OFF_REF_NO1"] = Ref.ToString() ;           
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}

