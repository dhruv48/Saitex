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

public partial class Module_Production_Report_Dept_Issue_Reciept_Details_DateWise : System.Web.UI.Page
{
    string DEPT_ISS_SLIP_NO = string.Empty;
    string MERGE_NO = string.Empty;
    DateTime FDate = System.DateTime.Now;
    DateTime TDate = System.DateTime.Now;
    DateTime FmDate;
    DateTime Tdate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        
        if (Request.QueryString["DEPT_ISS_SLIP"] != null && Request.QueryString["DEPT_ISS_SLIP"] != "")
        {
            DEPT_ISS_SLIP_NO = Request.QueryString["DEPT_ISS_SLIP"].ToString().Trim();
        }
        else
        {
            DEPT_ISS_SLIP_NO = "";
        }
        
        if (Request.QueryString["YARN_CODE"] != null && Request.QueryString["YARN_CODE"] != "")
        {
            MERGE_NO = Request.QueryString["YARN_CODE"].ToString().Trim();
        }
        else
        {
            MERGE_NO = "";
        }
        if (Request.QueryString["FROM_DATE"] != null && Request.QueryString["FROM_DATE"] != "")
        {
            FmDate = DateTime.Parse(Request.QueryString["FROM_DATE"].ToString().Trim());
        }
        if (Request.QueryString["TO_DATE"] != null && Request.QueryString["TO_DATE"] != "")
        {
            Tdate = DateTime.Parse(Request.QueryString["TO_DATE"].ToString().Trim());
        }
        DataTable dt = GetData();
        GetReport(dt);
        dt.Dispose();
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Dept_Issue_Reciept_Details_DateWiseReport.rpt"));
        rDoc.SetDataSource(dt);
        rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
        rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);        
        rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);
        rDoc.SetParameterValue("BRANCH_NAME", oUserLoginDetail.VC_BRANCHNAME);
        DeptIssDetail.ReportSource = rDoc;
    }

    
    private DataTable GetData()
    {
       
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.Dept_Issue_Reciept_Details_DateWise(DEPT_ISS_SLIP_NO, FmDate, Tdate, MERGE_NO);
            dt.TableName = "Pro_Dept_Iss_Detail";

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
