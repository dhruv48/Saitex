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
using System.Data.OracleClient;
using Common;
using errorLog;

public partial class Module_Production_Report_MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISEReport : System.Web.UI.Page
{
    string MACHINE_ISS_NO = string.Empty;
    DateTime FDate = System.DateTime.Now;
    DateTime TDate = System.DateTime.Now;
    DateTime FmDate;
    DateTime Tdate;
    string MERGE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail ;
       
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Request.QueryString["MACH_ISS_NO"] != null && Request.QueryString["MACH_ISS_NO"] != "")
        {
            MACHINE_ISS_NO = Request.QueryString["MACH_ISS_NO"].ToString().Trim();
        }
        else
        {
            MACHINE_ISS_NO = "";
        }
        if (Request.QueryString["FROM_DATE"] != null && Request.QueryString["FROM_DATE"] != "")
        {
            FmDate = DateTime.Parse(Request.QueryString["FROM_DATE"].ToString().Trim());
        }
        if (Request.QueryString["TO_DATE"] != null && Request.QueryString["TO_DATE"] != "")
        {
            Tdate = DateTime.Parse(Request.QueryString["TO_DATE"].ToString().Trim());
        }
        if (Request.QueryString["MERGE"] != null && Request.QueryString["MERGE"] != "")
        {
            MERGE = Request.QueryString["MERGE"].ToString().Trim();
        }
        DataTable dt = GetData();
        GetReport(dt);
        dt.Dispose();
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISEReport.rpt"));
        rDoc.SetDataSource(dt);
        rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
        rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
        rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);
        rDoc.SetParameterValue("BRANCH_NAME", oUserLoginDetail.VC_BRANCHNAME);
        COPSreport.ReportSource = rDoc;
    }

    
    private DataTable GetData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISE(MACHINE_ISS_NO, FmDate, Tdate, MERGE);
            dt.TableName = "Detail_of_COPS";

            //if (!dt.Columns.Contains("COMP_NAME"))
            //    dt.Columns.Add("COMP_NAME", typeof(string));
            //if (!dt.Columns.Contains("COMP_ADD"))
            //    dt.Columns.Add("COMP_ADD", typeof(string));
            //if (!dt.Columns.Contains("BRANCH_NAME"))
            //    dt.Columns.Add("BRANCH_NAME", typeof(string));
            //if (!dt.Columns.Contains("USER_NAME"))
            //    dt.Columns.Add("USER_NAME", typeof(string));

           
            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["COMP_NAME"] = OUserLoginDetail.VC_COMPANYNAME;
            //    dr["COMP_ADD"] = OUserLoginDetail.COMP_ADD;
            //    dr["BRANCH_NAME"] = OUserLoginDetail.VC_BRANCHNAME;
            //    dr["USER_NAME"] = OUserLoginDetail.Username;

            //}
            return dt;
        }

        catch (OracleException ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
