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

public partial class Module_GateEntry_Reports_GatePassReport : System.Web.UI.Page
{
    
     public static string TYPE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        GetReport(dt);
    }




    private void GetReport(DataSet dt)
    {

        ReportDocument rDoc = new ReportDocument();
        string ReportPath = string.Empty;
        if (TYPE == string.Empty)
            ReportPath = Server.MapPath(@"GatePassReport.rpt");
        else
            ReportPath = Server.MapPath(@"GatePassListReport.rpt");
        rDoc.Load(ReportPath);
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    private DataSet GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            int From = 0;
            int To = 0;
            string GATE_TYPE = "";
            DateTime FromDate = System.DateTime.Now;
            DateTime ToDate = System.DateTime.Now;
            DataSet dt;
            

            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }

            if (Request.QueryString["GATE_TYPE"] != null)
            {
                GATE_TYPE = Request.QueryString["GATE_TYPE"].ToString().Trim();
            }


            if (Request.QueryString["FromDate"] != null)
            {
                FromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString().Trim());
            }
            if (Request.QueryString["ToDate"] != null)
            {
                ToDate = DateTime.Parse(Request.QueryString["ToDate"].ToString().Trim());
            }
            if (Request.QueryString["TYPE"] != null)
            {
                TYPE = Request.QueryString["TYPE"].ToString().Trim();
            }
            if (TYPE == string.Empty)
            {
                dt = SaitexBL.Interface.Method.TX_GATE_PASS_MST.GetDataForReport(oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), From, To, GATE_TYPE);
                dt.Tables[0].TableName = "GATE_PASS_MST";

            }
            else
            {
                dt = SaitexBL.Interface.Method.TX_GATE_PASS_MST.GetDataForReportDate(oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), FromDate, ToDate, GATE_TYPE);
                dt.Tables[0].TableName = "GATE_PASS_MST";

                dt.Tables[0].Columns.Add("FROM_DATE", typeof(System.DateTime));
                dt.Tables[0].Columns.Add("To_DATE", typeof(System.DateTime));
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    row["FROM_DATE"] = (FromDate).ToString("dd/MM/yyyy");
                    row["To_DATE"] = (ToDate).ToString("dd/MM/yyyy"); ;
                }

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
