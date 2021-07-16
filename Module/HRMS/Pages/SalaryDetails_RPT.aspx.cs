using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.OracleClient;
public partial class HRMS_SalaryDetails_RPT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["GRADE_ID"] != null)
        {
            string iGradeID = "";
            iGradeID = Request.QueryString["GRADE_ID"];
            DataTable dt = GetData('Y', iGradeID);
            GetReport(dt);
        }
        else
        {
            Server.Transfer("./SalaryDetails.aspx");
        }
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\SalaryDetails.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData(char ch_View, string iGradeID)
    {
        SaitexDM.Common.DataModel.HR_SAL_GRD oHR_SAL_GRD = new SaitexDM.Common.DataModel.HR_SAL_GRD();

        oHR_SAL_GRD.GRADE_ID = iGradeID;

        DataTable dt = SaitexBL.Interface.Method.HR_SAL_GRD.GetReportData(ch_View, oHR_SAL_GRD);
        return dt;
    }
}