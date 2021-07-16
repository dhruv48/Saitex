using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_HRMS_Reports_EmployeeMasterReport : System.Web.UI.Page
{
    private static string emp = "";
    private static string refno = "";
    private static string gradename = "";
    private static DateTime dt1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["empcode"] != null && Request.QueryString["ref1"] != null && Request.QueryString["gradename"] != null && Request.QueryString["dt1"] != null)
            {

                emp = Request.QueryString["empcode"].ToString().ToUpper().Trim();
                refno = Request.QueryString["ref1"].ToString().Trim();
                gradename = Request.QueryString["gradename"].ToString().Trim();
                dt1 = Convert.ToDateTime(Request.QueryString["dt1"].ToString().Trim());
            }
            DataSet dt = GetData();
            if (dt.Tables[0].Rows.Count > 0)
            {
                GetReport(dt);
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Record Found");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in report uploading"));
        }
    
    }
    private void GetReport(DataSet dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"AppointLetter.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in report uploading"));
        }
    
    }
    private DataSet GetData()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_Appoint_Let oHR_Appoint_Let = new SaitexDM.Common.DataModel.HR_Appoint_Let();

            oHR_Appoint_Let.EMP_CODE = emp;

            DataSet dt = SaitexBL.Interface.Method.HR_Appoint_Let.GetDataForReport(oHR_Appoint_Let);

            dt.Tables[0].TableName = "HR_APPOINT";
            dt.Tables[1].TableName = "HR_APPOINT_SUB";

            dt.Tables[0].Columns.Add("Appoint_Date", typeof(DateTime));
            dt.Tables[0].Columns.Add("gradename", typeof(string));
            dt.Tables[0].Columns.Add("refno", typeof(string));

            foreach (DataRow dr in dt.Tables[0].Rows)
            {
               dr["Appoint_Date"] = dt1;
               dr["gradename"] = gradename.ToUpper();
               dr["refno"] = refno.ToUpper();
            }

            return dt;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in report uploading"));
            return null;
        }
    
    }
}
