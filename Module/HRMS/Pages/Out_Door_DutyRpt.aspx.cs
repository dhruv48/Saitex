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

public partial class Module_HRMS_Reports_Out_Door_DutyRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            DataTable dt = GetData();
            GetReport(dt);           
        }
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\Rpt_Out_Door_Duty.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData()
    {
        try
        {
            string OD_ID = string.Empty; string EmpCode = string.Empty;
            if (Request.QueryString["OD_ID"] != "" && Request.QueryString["OD_ID"] != null)
            {
                OD_ID = Request.QueryString["OD_ID"].Trim().ToString();
            }
            if (Request.QueryString["EmpCode"] != "" && Request.QueryString["EmpCode"] != null)
            {
                EmpCode = Request.QueryString["EmpCode"].Trim().ToString();
            }
            else
            {
                EmpCode = Session["EmpCode"].ToString();
            }           
            DataTable DTable = SaitexBL.Interface.Method.EMPOUTDOORDUTY.Load_ODD_Record(EmpCode, OD_ID);
            return DTable;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
