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

public partial class Module_Production_Report_FiberWIPStockReport : System.Web.UI.Page
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
    DateTime StDate;
    DateTime EnDate;
    DateTime Sdate = System.DateTime.Now;
    DateTime Edate = System.DateTime.Now;
    

    protected void Page_Load(object sender, EventArgs e)
    {    
        //if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
        //{
        //    StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
        //    EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
        //}
        //else
        //{
        //    StDate = Sdate;
        //    EnDate = Edate;
        //}
        DataTable dt = GetData( BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE);
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"WIPDetailsSummaryReport1.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData(string BRANCH_CODE, string DEPT_CODE, string PROS_ID_NO, string FromDate, string ToDate, string LOT_NUMBER, string ORDER_NO, string PRTY_NAME, string ARTICLE_DESC, DateTime StDate, DateTime EnDate, string PRODUCT_TYPE)
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.WIPStockQuery(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE,true );
            dt.TableName = "FiberWIPStockQuery1";

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
