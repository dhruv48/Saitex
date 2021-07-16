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
    string PROS_ID_NO = string.Empty;
    string LOT_NUMBER = string.Empty;
    string ORDER_NO = string.Empty;
    string FromDate = string.Empty;
    string ToDate = string.Empty;
    string LOT_TYPE = string.Empty;
    string FINISH_TYPE = string.Empty;
    string MERGE_NO = string.Empty;
    string PROD_PROS_ID_NO = string.Empty;
    string MACHINE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
      

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        DataTable dt = GetData();
        GetReport(dt);
        dt.Dispose();
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"ProductionDoffFormReport1.rpt"));
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

            if (Request.QueryString["BRANCH_NAME"] != null && Request.QueryString["BRANCH_NAME"] != "")
            {
                BRANCH_CODE = Request.QueryString["BRANCH_NAME"].ToString();

                where_Query += " and BRANCH_NAME LIKE '" + BRANCH_CODE + "%'";
            }
            if (Request.QueryString["DEPT_CODE"] != null && Request.QueryString["DEPT_CODE"] != "")
            {
                DEPT_CODE = Request.QueryString["DEPT_CODE"].ToString();

                where_Query += " and DEPT_CODE LIKE '" + DEPT_CODE + "%'";
            }
            if (Request.QueryString["PROS_ID_NO"] != null && Request.QueryString["PROS_ID_NO"] != "")
            {
                PROS_ID_NO = Request.QueryString["PROS_ID_NO"].ToString();

                where_Query += " and PROS_ID_NO  LIKE '" + PROS_ID_NO + "%'";
            }
            if (Request.QueryString["LOT_NO"] != null && Request.QueryString["LOT_NO"] != "")
            {
                LOT_NUMBER = Request.QueryString["LOT_NO"].ToString();
                where_Query += "and LOT_NO LIKE '" + LOT_NUMBER + "%'";
            }
            if (Request.QueryString["LOT_TYPE"] != null && Request.QueryString["LOT_TYPE"] != "")
            {
                LOT_TYPE = Request.QueryString["LOT_TYPE"].ToString();
                where_Query += " AND LOT_TYPE LIKE '" + LOT_TYPE + "%'";
            }
            if (Request.QueryString["FINISH_TYPE"] != null && Request.QueryString["FINISH_TYPE"] != "")
            {
                FINISH_TYPE = Request.QueryString["FINISH_TYPE"].ToString();
                where_Query += " AND FINISH_TYPE LIKE '" + FINISH_TYPE + "%'";
            }
            if (Request.QueryString["MERGE_NO"] != null && Request.QueryString["MERGE_NO"] != "")
            {
                MERGE_NO = Request.QueryString["MERGE_NO"].ToString();
                where_Query += " AND MERGE_NO LIKE '" + MERGE_NO + "%'";
            }
            if (Request.QueryString["PROD_PROS_ID_NO"] != null && Request.QueryString["PROD_PROS_ID_NO"] != "")
            {
                PROD_PROS_ID_NO = Request.QueryString["PROD_PROS_ID_NO"].ToString();
                where_Query += " AND PROD_PROS_ID_NO LIKE '" + PROD_PROS_ID_NO + "%'";
            }
            if (Request.QueryString["ORDER_NO"] != null && Request.QueryString["ORDER_NO"] != "")
            {
                ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
                where_Query += " AND ORDER_NO LIKE '" + ORDER_NO + "%'";
            }
            if (Request.QueryString["MACHINE"] != null && Request.QueryString["MACHINE"] != "")
            {
                MACHINE = Request.QueryString["MACHINE"].ToString();
                where_Query += " AND MACHINE_CODE LIKE '" + MACHINE + "%'";
            }
            if (Request.QueryString["FROM_DATE"] != null && Request.QueryString["FROM_DATE"] != "")
            {
                FromDate = Request.QueryString["FROM_DATE"].ToString();               
            }
            if (Request.QueryString["TO_DATE"] != null && Request.QueryString["TO_DATE"] != "")
            {
                ToDate  = Request.QueryString["TO_DATE"].ToString();
                
            }

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionDoffQuery(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, PROD_PROS_ID_NO, ORDER_NO,MACHINE,"" );
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
            //    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            //    dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
            //    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            //    dr["USER_NAME"] = oUserLoginDetail.Username;
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
