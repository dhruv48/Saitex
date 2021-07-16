using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using Common;
using System.Data;
using errorLog;
public partial class Module_OrderDevelopment_Reports_CryOrderBomReport : System.Web.UI.Page
{
    string PI_NO = string.Empty;
    string BASE_ARTICAL_CODE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
         oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
         try
         {
             if (Request.QueryString["PI_NO"] != null && Request.QueryString["PI_NO"] != "All")
             {
                 PI_NO = Request.QueryString["PI_NO"];

             }
             else
             {
                 PI_NO = string.Empty;

             }
             if (Request.QueryString["BASE_ARTICAL_CODE"] != null && Request.QueryString["BASE_ARTICAL_CODE"] != "All")
             {
                 BASE_ARTICAL_CODE = Request.QueryString["BASE_ARTICAL_CODE"];
             }
             else
             {
                 BASE_ARTICAL_CODE = string.Empty;

             }
             DataTable dtrportdat = GetData(PI_NO,BASE_ARTICAL_CODE);
             GetReport(dtrportdat);
         }
         catch (Exception ex)
         {
             Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
         }
    }


    private DataTable GetData(string PI_NO, string BASE_ARTICAL_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetOrderVomReport(PI_NO, BASE_ARTICAL_CODE);
            return dt;
        }
        catch
        {
            throw;
        }

    }
    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"OrderBomReport.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("Branch",oUserLoginDetail.VC_BRANCHNAME);
            rDoc.SetParameterValue("compname", oUserLoginDetail.VC_COMPANYNAME);
            rDoc.SetParameterValue("compadd", oUserLoginDetail.COMP_ADD );
            rDoc.SetParameterValue("username", oUserLoginDetail.Username);
           
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
}
