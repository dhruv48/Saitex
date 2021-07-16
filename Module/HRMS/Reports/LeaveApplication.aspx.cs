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

public partial class Module_HRMS_Reports_LeaveApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
          DataSet   DS = GetData();
            GetReport(DS);
        
    }
    private void GetReport(DataSet   DS)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\CRLeaveApplication.rpt"));
            rDoc.SetDataSource(DS);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message.ToString());
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    private DataSet   GetData()
    {       
        try
        {
            string SearchBy = string.Empty; string EmpCode = string.Empty; string LVMonth = string.Empty;
            if (Request.QueryString["Lv_App_Id"] != "" && Request.QueryString["Lv_App_Id"] != null)
            {
                if (SearchBy != string.Empty)
                {
                    SearchBy = SearchBy + " AND LV.Lv_App_Id=" + Request.QueryString["Lv_App_Id"].Trim().ToString();
                }
                else
                {
                    SearchBy = " LV.Lv_App_Id=" + Request.QueryString["Lv_App_Id"].Trim().ToString();
                }
            }
            if (Request.QueryString["LVMonth"] != "" && Request.QueryString["LVMonth"] != null)
            {
                if (SearchBy != string.Empty)
                {
                    SearchBy = SearchBy + " AND TO_CHAR(TRUNC(LV.LV_TO_DATE),'MM')='" + Request.QueryString["LVMonth"].Trim().ToString()+"'";
                }
                else
                {
                    SearchBy = " TO_CHAR(TRUNC(LV.LV_TO_DATE),'MM')='" + Request.QueryString["LVMonth"].Trim().ToString() + "'";
                }
                
            }
            if (Request.QueryString["EmpCode"] != "" && Request.QueryString["EmpCode"] != null)
            {
                if (SearchBy != string.Empty)
                {
                    SearchBy = SearchBy + " AND LV.EMP_CODE='" + Request.QueryString["EmpCode"].Trim().ToString() + "'";
                }
                else
                {
                    SearchBy = " LV.EMP_CODE='" + Request.QueryString["EmpCode"].Trim().ToString() + "'";
                }                 
            }
            DataSet  ds = SaitexBL.Interface.Method.HR_LV_APP.Leave_Application_Record (SearchBy);
            return ds;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}



