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
public partial class Module_HRMS_Reports_HR_LEAVE_APP_REPORT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            DataTable dt = GetData();
            GetReport(dt);
            if (!Page.IsPostBack)
            {

            }
        }
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"RPT_HR_LV_APP__.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData()
    {
        try
        {
            string Lv_App_Id = string.Empty; string EmpCode = string.Empty;
            if (Request.QueryString["Lv_App_Id"].Trim().ToString() != "" && Request.QueryString["Lv_App_Id"].Trim().ToString() != null)
            {
                Lv_App_Id = Request.QueryString["Lv_App_Id"].Trim().ToString();
            }
            if (Request.QueryString["EmpCode"].Trim().ToString() != "" && Request.QueryString["EmpCode"].Trim().ToString() != null)
            {
                EmpCode = Request.QueryString["Lv_App_Id"].Trim().ToString();
            }
            else
            {
                EmpCode = Session["EmpCode"].ToString();
            }
            DataTable DTable = SaitexBL.Interface.Method.HR_LV_APP.Load_HR_LV_APP_Record(EmpCode, Lv_App_Id);
            return DTable;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
