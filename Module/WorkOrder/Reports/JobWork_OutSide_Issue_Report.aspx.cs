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
using System.Data.OracleClient;
using errorLog;
using Common;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_WorkOrder_Reports_JobWork_OutSide_Issue_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable ds = getRecord();
        GetReport(ds);
    }

    public void GetReport(DataTable ds)
    {
        ReportDocument rDoc = new ReportDocument();
        //rDoc.Load(Server.MapPath(@"WorkOrderEntry.rpt"));
        rDoc.Load(Server.MapPath(@"OutSide_IssueReport.rpt"));
        rDoc.SetDataSource(ds);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable getRecord()
    {
        string BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
        string COMP_CODE = Request.QueryString["COMP_CODE"].ToString();
        int WO_NUM = int.Parse(Request.QueryString["WO_NUM"].ToString());
        string PRTY_CODE = Request.QueryString["PRTY_CODE"].ToString();
        string FROM_DATE = Request.QueryString["FROM_DATE"].ToString();
        string TO_DATE = Request.QueryString["TO_DATE"].ToString();
        string YARN_DESC = Request.QueryString["YARN_DESC"].ToString();
        int YEAR = int.Parse(Request.QueryString["Year"].ToString());
        DataTable dt = SaitexDL.Interface.Method.YRN_IR_MST.BindIssueOrder(BRANCH_CODE, COMP_CODE, WO_NUM, PRTY_CODE, FROM_DATE, TO_DATE, YARN_DESC, YEAR);
        dt.Columns.Add("FROMDATE", typeof(DateTime));
        dt.Columns.Add("TODATE", typeof(DateTime));
        foreach (DataRow row in dt.Rows)
        {
            row["FROMDATE"] = FROM_DATE;
            row["TODATE"] = TO_DATE;
        }
        return dt;
    }
}