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
using System.Data.OracleClient;

public partial class Module_Production_Report_ProductionDoffFormReport : System.Web.UI.Page
{   
    string Query_String=string.Empty;
    string BRANCH_CODE = string.Empty;
    string DEPT_CODE = string.Empty;
    int PROS_ID_NO ;
    string LOT_NUMBER = string.Empty;
    string ORDER_NO = string.Empty;
    string FromDate = string.Empty;
    string ToDate = string.Empty;
    string LOT_TYPE = string.Empty;
    string FINISH_TYPE = string.Empty;
    string MERGE_NO = string.Empty;
    string PROD_PROS_ID_NO = string.Empty;
    string TRN_TYPE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
       
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Request.QueryString["TRN_TYPE"] != null)
        {
            TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
        }
        DataTable dt = GetData();
        GetReport(dt);
        dt.Dispose();
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"ProductionDoffFormReport11.rpt"));
        rDoc.SetDataSource(dt);
        rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
        rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData()
    {
        try
        {
            string where_Query = string.Empty; 
            int From = 0;
            int To = 0;

            if (Request.QueryString["From"] != null && Request.QueryString["From"] != "")
                From = int.Parse(Request.QueryString["From"].ToString().Trim());

            if (Request.QueryString["To"] != null && Request.QueryString["To"] != "")
                To = int.Parse(Request.QueryString["To"].ToString().Trim());


            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.PrintProductionDoffQuery(oUserLoginDetail.CH_BRANCHCODE, DEPT_CODE, From, To,TRN_TYPE );

            dt.TableName = "ProductionDoffForm1";
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
