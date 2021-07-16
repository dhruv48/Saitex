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

public partial class Module_Yarn_SalesWork_Reports_YarnPackingSlipReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string TRN_TYPE = string.Empty;
    string REPORT_TYPE = string.Empty;
         
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        int From = 0;
        int To = 0;
       int YEAR = 0;
       YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        string BRANCHCODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
       string COMPCODE = oUserLoginDetail.COMP_CODE.ToString();
       if (Request.QueryString["TRN_TYPE"] != null)
       {
           TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
       }
       if (Request.QueryString["REPORT_TYPE"] != null)
       {
           REPORT_TYPE = Request.QueryString["REPORT_TYPE"].ToString();
       }


       //if (Request.QueryString["From"] != null)
       // {
       //     From = int.Parse(Request.QueryString["From"].ToString());
       // }
       //if (Request.QueryString["To"] != null)
       //{
       //    To = int.Parse(Request.QueryString["To"].ToString());
       //}
        try
        {
            DataTable dtReportdata = GetData(YEAR, COMPCODE, BRANCHCODE, From, To);
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

        if (REPORT_TYPE == "Main")
        {
            rDoc.Load(Server.MapPath(@"YarnPackingSlipReport.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
            rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
            rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);
            rDoc.SetParameterValue("BRANCH_NAME", oUserLoginDetail.VC_BRANCHNAME);
        }
        else if (REPORT_TYPE == "Cartons")
        {
            rDoc.Load(Server.MapPath(@"YarnPackingCartonsSlipReport.rpt"));
            rDoc.SetDataSource(dt);
           
        } 
       
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData(int YEAR, string COMPCODE, string BRANCHCODE, int From, int To)
      {
        try
         {
             //int From = 0;
             //int To = 0;

             if (Request.QueryString["From"] != null && Request.QueryString["From"] != "")
                 From = int.Parse(Request.QueryString["From"].ToString().Trim());

             if (Request.QueryString["To"] != null && Request.QueryString["To"] != "")
                 To = int.Parse(Request.QueryString["To"].ToString().Trim());
             DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataYarnPackingNo(YEAR, COMPCODE, BRANCHCODE, From, To,TRN_TYPE );
            if (dt.Rows.Count > 0)
             {
                //if (dt.Columns["COMP_NAME"] == null)
                //    dt.Columns.Add("COMP_NAME", typeof(string));

                //if (dt.Columns["BRANCH_NAME"] == null)
                //    dt.Columns.Add("BRANCH_NAME", typeof(string));

                //if (dt.Columns["USER_NAME"] == null)
                //    dt.Columns.Add("USER_NAME", typeof(string));
                ////if (dt.Columns["BRANCH_ADD"] == null)
                ////    dt.Columns.Add("BRANCH_ADD", typeof(string));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                //    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                //    dr["USER_NAME"] = oUserLoginDetail.Username;
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
        catch(Exception ex)
        {
            throw ex;
        }
    } 
}
