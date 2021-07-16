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
public partial class Module_Production_Report_PackingSummarydetail_Report : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string Packing_Date_From = string.Empty;
    string Packing_Date_To= string.Empty;
    string chk = string.Empty;
    DataTable dtReportData = null;
    ReportDocument rDoc = new ReportDocument();
    protected void Page_unLoad(object sender, EventArgs e)
    {
        rDoc.Close();
        rDoc.Dispose();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
      
        int YEAR = 0;
        YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
        string COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
        string DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE.ToString();
        if (Request.QueryString["Packing_Date_From"] != null && Request.QueryString["Packing_Date_From"] != "")
        {
            Packing_Date_From = Request.QueryString["Packing_Date_From"].ToString();
        }
        if (Request.QueryString["chk"] != null && Request.QueryString["chk"] != "")
        {
            chk = Request.QueryString["chk"].ToString();
        }
        if (Request.QueryString["Packing_Date_To"] != null && Request.QueryString["Packing_Date_To"] != "")
        {
            Packing_Date_To = Request.QueryString["Packing_Date_To"].ToString();
        }
        try
        {
            dtReportData = GetData(YEAR, COMP_CODE, BRANCH_CODE, DEPT_CODE, Packing_Date_From, Packing_Date_To);
            GetReport(dtReportData);
          

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
        finally 
        {
            dtReportData.Dispose();
        }
    }
      private void GetReport(DataTable dt)
    {
        
            if (chk == "0")
            {
                rDoc.Load(Server.MapPath(@"PackingSummaryDetail_Report.rpt"));
            }
            else if (chk == "1")
            {
                rDoc.Load(Server.MapPath(@"PackingSummary_LOT_Report.rpt"));
            }
            else if (chk == "2")
            {
                rDoc.Load(Server.MapPath(@"PackingSummary_LOT_DETAILS_Report.rpt"));
            }
     
        rDoc.SetDataSource(dt);
        rDoc.SetParameterValue("FROMDATE", Packing_Date_From);
        rDoc.SetParameterValue("TODATE", Packing_Date_To);
        rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
        rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
      
        
        CrystalReportViewer1.ReportSource = rDoc;
        }
      private DataTable GetData(int YEAR, string COMP_CODE, string BRANCH_CODE, string DEPT_CODE,  string Packing_Date_From,  string Packing_Date_To)
      {   DataTable dt = null;
          try
          {
              DateTime DT_FROM = DateTime.Now.Date;
              DateTime DT_TO = DateTime.Now.Date;
              DateTime.TryParse(Packing_Date_From, out DT_FROM);
              DateTime.TryParse(Packing_Date_To, out DT_TO);
             
              if (chk == "0")
              {
                  dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPackingSummaryForm(YEAR, COMP_CODE, BRANCH_CODE,DEPT_CODE, DT_FROM, DT_TO);
              }
              else if (chk == "1")
              {
                  dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPackingSummaryFormLot_Wise(YEAR, COMP_CODE, BRANCH_CODE,DEPT_CODE, DT_FROM, DT_TO);
              }
              else if (chk == "2")
              {
                  dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetPackingSummaryFormLot_Wise_DETAILS(YEAR, COMP_CODE, BRANCH_CODE,DEPT_CODE, DT_FROM, DT_TO);
              }
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
          finally 
          
          { 
            dt.Dispose();
          }
      }
}
