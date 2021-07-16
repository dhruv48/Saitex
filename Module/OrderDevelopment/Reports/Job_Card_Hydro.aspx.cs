using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_OrderDevelopment_Reports_Job_Card_Hydro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        GetReport(dt);
    }

    private void GetReport(DataSet dt)
    {

        ReportDocument rDoc = new ReportDocument();
        string ReportPath = string.Empty;

        ReportPath = Server.MapPath(@"JOB_CARD_Hydro.rpt");

        rDoc.Load(ReportPath);
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    private DataSet GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            string FDATE = string.Empty;
            string TDATE = string.Empty;
            string TRN_TYPE = "";

            if (Request.QueryString["FromDate"] != null)
            {
                FDATE = Request.QueryString["FromDate"].ToString();
                // From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToDate"] != null)
            {
                TDATE = Request.QueryString["ToDate"].ToString();
                // To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }

            DataSet dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetDataForReportQC(oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), FDATE, TDATE);
            dt.Tables[1].TableName = "BATCH_CARD_MST_QC";
            // dt.Tables[1].TableName = "BATCH_CARD_TRN_SUB";
            // dt.Tables[2].TableName = "BATCH_CARD_TRN";

            return dt;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }

}