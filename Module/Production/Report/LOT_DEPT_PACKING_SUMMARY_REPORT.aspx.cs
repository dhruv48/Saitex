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

public partial class Module_Production_Report_LOT_DEPT_PACKING_SUMMARY_REPORT : System.Web.UI.Page
{
    
    string Query_String = string.Empty;
    string ARTICLE_DESC = string.Empty;
    string PROS_CODE=string.Empty;
    string DEPT_CODE=string.Empty;
    string Comp_Code = string.Empty;
    string BRANCH_CODE = string.Empty;
    string Year = string.Empty;
    string PROS_ID_NO = string.Empty;
    string FromDate = string.Empty;
    string ToDate = string.Empty;
    string LOT_NUMBER = string.Empty;
    string ORDER_NO = string.Empty;
    string PRTY_NAME = string.Empty;
    string PRODUCT_TYPE = string.Empty;
    string MERGE_CODE = string.Empty;
    DateTime StDate = System.DateTime.Now;
    DateTime EnDate = System.DateTime.Now;
    //string StDate = string.Empty;
    //string EnDate = string.Empty;
    //DateTime Sdate = System.DateTime.MinValue;
    //DateTime Edate = System.DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["LOT_NUMBER"] != null && Request.QueryString["LOT_NUMBER"] != "")
        {
            LOT_NUMBER = Request.QueryString["LOT_NUMBER"].ToString().Trim();
        }
        else
        {
            LOT_NUMBER = "";
        }
        if (Request.QueryString["PRTY_NAME"] != null && Request.QueryString["PRTY_NAME"] != "")
        {
            PRTY_NAME = Request.QueryString["PRTY_NAME"].ToString().Trim();
        }
        else
        {
            PRTY_NAME = "";
        }
        if (Request.QueryString["ARTICLE_DESC"] != null && Request.QueryString["ARTICLE_DESC"] != "")
        {
            ARTICLE_DESC = Request.QueryString["ARTICLE_DESC"].ToString().Trim();
        }
        else
        {
            ARTICLE_DESC = "";
        }
        if (Request.QueryString["StDate"] != null && Request.QueryString["EnDate"] != "")
        {
            DateTime.TryParse(Request.QueryString["StDate"].ToString(), out StDate);
            DateTime.TryParse(Request.QueryString["EnDate"].ToString(), out EnDate);
        }
      


        DataTable dt = GetData(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, true, MERGE_CODE);
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"LOT_DEPT_PACKING_SUMMARY_REPORT.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    //private DataTable GetData(string BRANCH_CODE, string DEPT_CODE, string PROS_ID_NO, string FromDate, string ToDate, string LOT_NUMBER, string ORDER_NO, string PRTY_NAME, string ARTICLE_DESC, DateTime StDate, DateTime EnDate, string PRODUCT_TYPE)
    private DataTable GetData(string BRANCH_CODE, string DEPT_CODE, string PROS_ID_NO, string FromDate, string ToDate, string LOT_NUMBER, string ORDER_NO, string PRTY_NAME, string ARTICLE_DESC, DateTime StDate, DateTime EnDate, string PRODUCT_TYPE, bool ChkStock, string MERGE_CODE)
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.LOT_DEPT_PACKING_SUMMARY(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, true, MERGE_CODE);
            dt.TableName = "LOT_DEPT_PACKING_SUMMARY";

            if (!dt.Columns.Contains("COMP_NAME"))
                dt.Columns.Add("COMP_NAME", typeof(string));
            if (!dt.Columns.Contains("COMP_ADD"))
                dt.Columns.Add("COMP_ADD", typeof(string));
            if (!dt.Columns.Contains("BRANCH_NAME"))
                dt.Columns.Add("BRANCH_NAME", typeof(string));
            if (!dt.Columns.Contains("USER_NAME"))
                dt.Columns.Add("USER_NAME", typeof(string));

            SaitexDM.Common.DataModel.UserLoginDetail OUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = OUserLoginDetail.VC_COMPANYNAME;
                dr["COMP_ADD"] = OUserLoginDetail.COMP_ADD;
                dr["BRANCH_NAME"] = OUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = OUserLoginDetail.Username;

            }
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
