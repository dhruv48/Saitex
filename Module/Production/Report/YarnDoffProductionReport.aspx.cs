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
using Common;
using errorLog;
public partial class Module_Production_Report_YarnDoffProductionReport : System.Web.UI.Page
{
    string Query_String = string.Empty;
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
    string TRN_TYPE = string.Empty;
    int chksumry = 0;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    ReportDocument rDoc = new ReportDocument();
    protected void Page_unLoad(object sender, EventArgs e)
    {
        rDoc.Close();
        rDoc.Dispose();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
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
                ToDate = Request.QueryString["TO_DATE"].ToString();

            } 
            if (Request.QueryString["chk"] != null && Request.QueryString["chk"] != "")
            {
                chksumry = int.Parse(Request.QueryString["chk"].ToString());
            }
            if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
            }    
             DataTable dtrportdat = GetData();
             GetReport(dtrportdat);          
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataTable dt)
    {
        try
        {           
            if (chksumry == 0)
            {
                rDoc.Load(Server.MapPath(@"ProductinSummaryReport.rpt"));
            }
            else if (chksumry == 1)
            {
                rDoc.Load(Server.MapPath(@"ProductinSummaryReport_LotWise.rpt"));
            }
            else if (chksumry == 2)
            {
                rDoc.Load(Server.MapPath(@"ProductinDetailsReport_LotWise.rpt"));
            }
            else if (chksumry == 3)
            {
                rDoc.Load(Server.MapPath(@"ProductinSummaryReport_MachineWise.rpt"));
            }
            else if (chksumry == 4)
            {
                rDoc.Load(Server.MapPath(@"ProductinDetailsReport_MachineWise.rpt"));
            }
            else if (chksumry == 5)
            {
                rDoc.Load(Server.MapPath(@"ProductionDoffFormReport1.rpt"));
            } 
                rDoc.SetDataSource(dt);

              if (chksumry != 5)
              {
                rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
                rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.BRANCH_PRTYNAMEADDRES);
                rDoc.SetParameterValue("BRANCH_NAME", oUserLoginDetail.VC_BRANCHNAME);
                rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
                rDoc.SetParameterValue("Start", FromDate);
                rDoc.SetParameterValue("End", ToDate);
              }             
            CrystalReportViewer1.ReportSource = rDoc;     
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData()
    {
        try
        {
            DataTable dt = null;       

            if (chksumry == 0)
            {
                dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionSummary(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, PROD_PROS_ID_NO, ORDER_NO, MACHINE, TRN_TYPE);
          
            }
            else if (chksumry == 1 )
            {
                dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionLotWiseSummary(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, PROD_PROS_ID_NO, ORDER_NO, MACHINE, TRN_TYPE);

            } 
            else if (chksumry == 2)
            {
                dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionLotMachineWiseDetails(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, PROD_PROS_ID_NO, ORDER_NO, MACHINE, TRN_TYPE);
          
            }            
            else if (chksumry == 3)
            {
                dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionMachineWiseSummary(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, PROD_PROS_ID_NO, ORDER_NO, MACHINE, TRN_TYPE);
          
            }
            else if (chksumry == 4)
            {
                dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionLotMachineWiseDetails(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, PROD_PROS_ID_NO, ORDER_NO, MACHINE, TRN_TYPE);
          
            }
            else if (chksumry == 5)
            {
                dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionDoffQuery(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, PROD_PROS_ID_NO, ORDER_NO, MACHINE, TRN_TYPE);

            }

            if (dt.Rows.Count > 0)
            {
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
                //    dr["COMP_ADD"] = oUserLoginDetail.BRANCH_PRTYNAMEADDRES;
                //    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                //    dr["USER_NAME"] = oUserLoginDetail.Username;
                //}
             
            }
            else 
            {
                CommonFuction.ShowMessage("Data Not Found .");
            
            }

       
            return dt;
        }
        catch
        {
            throw;
        }
    }
}
