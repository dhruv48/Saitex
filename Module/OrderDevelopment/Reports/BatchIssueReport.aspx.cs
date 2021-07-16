using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;

using CrystalDecisions.ReportSource;

public partial class Module_OrderDevelopment_Reports_BatchIssueReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt); 
    }

    private void GetReport(DataTable dt)
    {

        ReportDocument rDoc = new ReportDocument();
        string ReportPath = string.Empty;


        ReportPath = Server.MapPath(@"BatchIssueReport.rpt");


        rDoc.Load(ReportPath);
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;

    }


    private DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {

            string FROM_DATE = "";
            string TO_DATE = "";
            string Machine = "";
            //string Report_Type = string.Empty;

            if (Request.QueryString["FDate"] != null)
            {
                FROM_DATE = Request.QueryString["FDate"].ToString().Trim();
            }

            if (Request.QueryString["TDate"] != null)
            {
                TO_DATE = Request.QueryString["TDate"].ToString().Trim();
            }

            if (Request.QueryString["Machine"] != null)
            {
                Machine = Request.QueryString["Machine"].ToString().Trim();
            }

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForBatchIssueReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, FROM_DATE, TO_DATE, Machine);
            return dt;
        }

        catch
        {
            throw;
        }

    }
}