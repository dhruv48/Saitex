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

public partial class Module_Production_Report_Dyeing_Pending_Print_Report : System.Web.UI.Page
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

        //ReportPath = Server.MapPath(@"Production_Dyeing_Report.rpt");
        ReportPath = Server.MapPath(@"Dyeing_Pending_Print_Report.rpt");
        rDoc.Load(ReportPath);
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    private DataSet GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            string From = "";
            string To = "";


            if (Request.QueryString["FromNo"] != null)
            {
                From = Request.QueryString["FromNo"].ToString().Trim();
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = Request.QueryString["ToNo"].ToString().Trim();
            }

            DataSet dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForDyeingPendingReport(oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), From, To);

            dt.Tables[0].TableName = "DYEING_PENDING_PRINT_REPORT";
            dt.Tables[0].Columns.Add("FROMDATE", typeof(DateTime));
            dt.Tables[0].Columns.Add("TODATE", typeof(DateTime));
            foreach (DataRow row in dt.Tables[0].Rows)
            {
                row["FROMDATE"] = (DateTime.Parse(From)).ToString("dd/MM/yyyy");
                row["TODATE"] = (DateTime.Parse(To)).ToString("dd/MM/yyyy"); ;
            }



            return dt;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}