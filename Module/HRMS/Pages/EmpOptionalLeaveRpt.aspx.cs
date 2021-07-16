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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;


public partial class Module_HRMS_Pages_EmpOptionalLeaveRpt : System.Web.UI.Page
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
        rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\CrEmpOptionalLeave.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }  
    private DataTable GetData()
    {
        try
        {
            string EmpCode = Session["EmpCode"].ToString();
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_OPT_LV_DTL.EMP_Optional_Leave_Detail(EmpCode);
            return DTable;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
