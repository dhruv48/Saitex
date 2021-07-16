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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using Common;
using errorLog;
using System.Data.OracleClient;

public partial class Module_Production_Report_PackingDailySummary_Report : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
      oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
      DateTime Packing_Date = System.DateTime.Today;        
        int YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
        string COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
        try
        {
            DataTable dtReportdata = GetData(YEAR, COMP_CODE, BRANCH_CODE, Packing_Date);
            GetReport(dtReportdata);
            dtReportdata.Dispose();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
         }
       }
        private void GetReport(DataTable dt)
        {
        string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"PackingDailySummary_Report.rpt"));
        rDoc.SetDataSource(dt);
        rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
        rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
        CrystalReportViewer1.ReportSource = rDoc;
        }
        private DataTable GetData(int YEAR, string COMP_CODE, string BRANCH_CODE, DateTime Packing_Date)
        {
            try
            {
                if (Request.QueryString["Packing_Date"] != null && Request.QueryString["Packing_Date"] != "")
                    Packing_Date = Convert.ToDateTime(Request.QueryString["Packing_Date"].ToString());

                DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDailyPackingSummary(YEAR, COMP_CODE, BRANCH_CODE, Packing_Date);
                if (dt.Rows.Count > 0)
                {
                    //if (dt.Columns["COMP_NAME"] == null)
                    //    dt.Columns.Add("COMP_NAME", typeof(string));

                    //if (dt.Columns["BRANCH_NAME"] == null)
                    //    dt.Columns.Add("BRANCH_NAME", typeof(string));

                    //if (dt.Columns["USER_NAME"] == null)
                    //    dt.Columns.Add("USER_NAME", typeof(string));
                    //if (dt.Columns["DEPT_NAME"] == null)
                    //    dt.Columns.Add("DEPT_NAME", typeof(string));
                    ////if (dt.Columns["BRANCH_ADD"] == null)
                    ////    dt.Columns.Add("BRANCH_ADD", typeof(string));
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                    //    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                    //    dr["USER_NAME"] = oUserLoginDetail.Username;
                    //    dr["DEPT_NAME"] = oUserLoginDetail.VC_DEPARTMENTNAME;
                    //    //dr["BRANCH_ADD"] = oUserLoginDetail.BRANCH_ADD;
                    //    dt.AcceptChanges();
                    //}
                }
                else
                {
                    Common.CommonFuction.ShowMessage(" Sir Data not found accroding to this record . ");
                }

                return dt;
            }
            catch
            {
                throw;
            }
        }
}
