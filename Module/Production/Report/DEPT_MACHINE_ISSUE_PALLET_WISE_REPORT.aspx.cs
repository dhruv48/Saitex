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

public partial class Module_Production_Report_DEPT_MACHINE_ISSUE_PALLET_WISE_REPORT : System.Web.UI.Page
{
    

    string BRANCH_CODE=string.Empty;
    string DEPT_CODE=string.Empty;
    string PALLET_NO=string.Empty;
    string DEPT_ISS_NO=string.Empty;
    string MACHINE_ISS_NO=string.Empty;
    DateTime FromDate;
    DateTime ToDate;
    //string StDate = string.Empty;
    //string EnDate = string.Empty;
    DateTime FDate = System.DateTime.Now;
    DateTime TDate = System.DateTime.Now;
    DateTime StDate;
    DateTime EnDate;
    //string StDate = string.Empty;
    //string EnDate = string.Empty;
    DateTime Sdate = System.DateTime.Now;
    DateTime Edate = System.DateTime.Now;
    string LOT_NO=string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail ;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        
        if (Request.QueryString["PALLET_NO"] != null && Request.QueryString["PALLET_NO"] != "")
        {
            PALLET_NO = Request.QueryString["PALLET_NO"].ToString().Trim();
        }
        else
        {
            PALLET_NO = "";
        }
        //if (Request.QueryString["PRTY_NAME"] != null && Request.QueryString["PRTY_NAME"] != "")
        //{
        //    PRTY_NAME = Request.QueryString["PRTY_NAME"].ToString().Trim();
        //}
        //else
        //{
        //    PRTY_NAME = "";
        //}
        if (Request.QueryString["DEPT_ISS_NO"] != null && Request.QueryString["DEPT_ISS_NO"] != "")
        {
            DEPT_ISS_NO = Request.QueryString["DEPT_ISS_NO"].ToString().Trim();
        }
        else
        {
            DEPT_ISS_NO = "";
        }
        if (Request.QueryString["MACHINE_NO"] != null && Request.QueryString["MACHINE_NO"] != "")
        {
            MACHINE_ISS_NO = Request.QueryString["MACHINE_NO"].ToString().Trim();
        }
        else
        {
            MACHINE_ISS_NO = "";
        }
        if (Request.QueryString["YARN_CODE"] != null && Request.QueryString["YARN_CODE"] != "")
        {
            LOT_NO = Request.QueryString["YARN_CODE"].ToString().Trim();
        }
        else
        {
            LOT_NO = "";
        }
        if (Request.QueryString["MACHINE_ISS_FROM"] != null && Request.QueryString["MACHINE_ISS_FROM"] != "")
        {
            StDate = DateTime.Parse(Request.QueryString["MACHINE_ISS_FROM"].ToString().Trim());
        }
        if (Request.QueryString["MACHINE_ISS_TO"] != null && Request.QueryString["MACHINE_ISS_TO"] != "")
        {
            EnDate =DateTime.Parse(Request.QueryString["MACHINE_ISS_TO"].ToString().Trim());
        }

        if (Request.QueryString["FROM_DATE"] != null && Request.QueryString["FROM_DATE"] != "")
        {
            //FromDate = DRequest.QueryString["FROM_DATE"].ToString().Trim();
            FromDate=DateTime.Parse(Request.QueryString["FROM_DATE"].ToString());
        }

        if (Request.QueryString["TO_DATE"] != null && Request.QueryString["TO_DATE"] != "")
        {
            ToDate = DateTime.Parse(Request.QueryString["TO_DATE"].ToString());
        }
        
        DataTable dt = GetData();
        GetReport(dt);
        dt.Dispose();
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"DEPT_MACHINE_ISSUE_PALLET_WISE_REPORT.rpt"));
        rDoc.SetDataSource(dt);
        rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
        rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
        rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);
        rDoc.SetParameterValue("BRANCH_NAME", oUserLoginDetail.VC_BRANCHNAME);
        ProductionIssQtyFormReport.ReportSource = rDoc;
    }

    
    private DataTable GetData()
    {
       
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY(PALLET_NO, DEPT_ISS_NO, MACHINE_ISS_NO, FromDate, ToDate, LOT_NO, StDate, EnDate);
            dt.TableName = "Production_Iss_Qty_Detail";

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
