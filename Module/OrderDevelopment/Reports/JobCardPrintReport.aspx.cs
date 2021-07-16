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
public partial class Module_OrderDevelopment_Reports_JobCardPrintReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        GetReport(dt);
    }

    private void GetReport(DataSet dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            string ReportPath = string.Empty;
            ReportPath = Server.MapPath(@"JobCardPrintReport.rpt");
            rDoc.Load(ReportPath);
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataSet GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            int From = 0;
            int To = 0;
            string TRN_TYPE = "";

            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }

            DataSet dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetDataForReport(oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), From, To);
            if (dt.Tables[0].Rows.Count == 0)
            {
                Common.CommonFuction.ShowMessage(" Please Approve the Job Card ! ");
            }

            dt.Tables[0].TableName = "BATCH_CARD_MST";
            dt.Tables[1].TableName = "BATCH_CARD_TRN_SUB";
            dt.Tables[2].TableName = "BATCH_CARD_TRN";

            return dt;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
