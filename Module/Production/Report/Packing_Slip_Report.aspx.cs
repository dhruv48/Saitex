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
public partial class Module_Production_Report_Packing_Slip_Report : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        string Carton_No = "";
        int YEAR = 0;
        YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        string BRANCHCODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
        string COMPCODE = oUserLoginDetail.COMP_CODE.ToString();
        try
        {
            DataTable dtReportdata = GetData(YEAR, COMPCODE, BRANCHCODE, Carton_No);
            GetReport(dtReportdata);
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
        rDoc.Load(Server.MapPath(@"SLIP_REPORT.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
        }
        private DataTable GetData(int YEAR, string COMPCODE, string BRANCHCODE, string Carton_No)
      {
        try
         {
             if (Request.QueryString["Carton_No"] != null && Request.QueryString["Carton_No"] != "")
                 Carton_No = Request.QueryString["Carton_No"].ToString().Trim();
             DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForPackingSlip(YEAR, COMPCODE, BRANCHCODE, Carton_No);
          if (dt.Rows.Count > 0)
            {
                if (dt.Columns["COMP_NAME"] == null)
                    dt.Columns.Add("COMP_NAME", typeof(string));

                if (dt.Columns["BRANCH_NAME"] == null)
                    dt.Columns.Add("BRANCH_NAME", typeof(string));

                if (dt.Columns["USER_NAME"] == null)
                    dt.Columns.Add("USER_NAME", typeof(string));
                //if (dt.Columns["BRANCH_ADD"] == null)
                //    dt.Columns.Add("BRANCH_ADD", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                    dr["USER_NAME"] = oUserLoginDetail.Username;
                    //dr["BRANCH_ADD"] = oUserLoginDetail.BRANCH_ADD;
                    dt.AcceptChanges();
                }
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
