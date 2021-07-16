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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.IO;

public partial class Module_HRMS_Reports_AttendanceReport : System.Web.UI.Page
{
    private string SearchQuery = string.Empty;
    private string Search = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["Search"] != null && Request.QueryString["Search"] != "")
            {
                SearchQuery = Request.QueryString["Search"].ToString().Trim();
            }            
            DataTable  DT = GetData();
            GetReport(DT, "Absenteeism.rpt");
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataTable  ds, string rptname)
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
    private DataTable  GetData()
    {
        try
        {
            DataTable  Dt = new DataTable ();
            Dt = SaitexBL.Interface.Method.HR_ATTN_TRN.Load_Attendance_Record(SearchQuery);
            return Dt;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }
}
