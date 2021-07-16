using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.IO;
using System.Data;
public partial class Module_HRMS_Reports_MonthlyPFESIReport : System.Web.UI.Page
{
    private string SearchQuery = string.Empty;  
    private string RName = string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string rpt="";
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["Search"] != null && Request.QueryString["Search"] != "")
            {
                SearchQuery = Request.QueryString["Search"].ToString().Trim();
            }
            if (Request.QueryString["RptType"] != null && Request.QueryString["RptType"] != "")
            {
                rpt = Request.QueryString["RptType"].ToString().Trim();
            }
            else
            {
                RName = "EMP_PF_REPORT.rpt";
            }
            if (rpt == "PF")
            {
                RName = "EMP_PF_REPORT.rpt";
            }
            else
            {
                RName = "EMP_ESI_REPORT.rpt";
            }
            DataTable DS = GetData();
            GetReport(DS, RName);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataTable ds, string rptname)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"" + rptname));
            rDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }
    private DataTable GetData()
    {


        try
        {
            DataTable DTable = new DataTable("PF_ESI_RECORD");
            DTable = SaitexBL.Interface.Method.HR_PF_ESI_MAST.Load_PF_ESI_MST_Report(SearchQuery);
            return DTable;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }
}

