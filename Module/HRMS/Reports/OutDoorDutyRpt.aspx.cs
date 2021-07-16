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
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_HRMS_Reports_OutDoorDutyRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {
            DataTable  DS = GetData();
            GetReport(DS);
        }
    }
    private void GetReport(DataTable  DS)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\OutDoorDutyReport.rpt"));
            rDoc.SetDataSource(DS);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message.ToString());
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    private DataTable  GetData()
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
