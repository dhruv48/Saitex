using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_OrderDevelopment_Reports_OrderTrackingReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string CATEGORY = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        DataTable dt = new DataTable();
        dt = getData();
        getReport(dt);

    }

  public DataTable getData()
    {
        string PARTY_CODE = "";
        string QUALITY="";
        string SHADE_CODE = "";
        string ORDER_NO = "";
        string BATCH_CODE = "";
      
        DataTable dt = new DataTable();

        if (Request.QueryString["PRTY_CODE"] != null)
        {
            PARTY_CODE = Request.QueryString["PRTY_CODE"].ToString().Trim();
        }
        if (Request.QueryString["Quality"] != null)
        {
            QUALITY = Request.QueryString["Quality"].ToString().Trim();
        }
        if (Request.QueryString["Shade_Code"] != null)
        {
            SHADE_CODE = Request.QueryString["Shade_Code"].ToString().Trim();
        }
        if (Request.QueryString["ORDER_NO"] != null)
        {
            ORDER_NO = Request.QueryString["ORDER_NO"].ToString().Trim();
        }
        if (Request.QueryString["BATCH_CODE"] != null)
        {
            BATCH_CODE = Request.QueryString["BATCH_CODE"].ToString().Trim();
        }
        if (Request.QueryString["CATEGORY"] != null)
        {
            CATEGORY = Request.QueryString["CATEGORY"].ToString().Trim();
        }


        if (CATEGORY == "Order Tracking")
        {
            dt = SaitexBL.Interface.Method.OD_CAPT_MST.OrderTrackingReport(PARTY_CODE, QUALITY, SHADE_CODE, ORDER_NO, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BATCH_CODE, CATEGORY);
         
        }
        if (CATEGORY == "JobCard Analysis")
        {
            dt = SaitexBL.Interface.Method.OD_CAPT_MST.OrderTrackingReport(PARTY_CODE, QUALITY, SHADE_CODE, ORDER_NO, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BATCH_CODE, CATEGORY);
         
        }
      return dt;
    }



    public void getReport(DataTable dt) 
    {
        ReportDocument doc = new ReportDocument();
        string ReportPath = "";

        if (CATEGORY == "Order Tracking")
        {
            ReportPath = Server.MapPath(@"OrderTrackingReport.rpt");
        }
        if (CATEGORY == "JobCard Analysis")
        {
            ReportPath = Server.MapPath(@"JobCardAnalysis.rpt");
        }


        doc.Load(ReportPath);
        doc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = doc;
    }
}