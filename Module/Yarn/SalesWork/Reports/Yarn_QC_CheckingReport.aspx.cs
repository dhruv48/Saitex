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

public partial class Module_Yarn_SalesWork_Reports_Yarn_QC_CheckingReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                DataTable dtrportdat = GetData();
                GetReport(dtrportdat);
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }


    private void GetReport(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"Yarn_QC_CheckingReport.rpt"));
            rDoc.SetDataSource(dt);
            Yarn_QC_CheckingReport1.ReportSource = rDoc;
        }
        else
        {
            Common.CommonFuction.ShowMessage("No Data Found");
        }
    }


    private DataTable GetData()
    {
        try
        {

            int TRN_YEAR = 0;
            int TRN_NUMB = 0;
            int QC_NUMB = 0;
            double QC_VALUE = 0;
            string TRN_TYPE = string.Empty;
            string Y_COUNT = string.Empty;
            string Q_RESULT = string.Empty;
            string QC_CHANGE_RESULT = string.Empty;
            string Y_Code = string.Empty;
            string YDesc = string.Empty;
            string STD_TYPE = string.Empty;
            string STATUS = string.Empty;

            if (Request.QueryString["TRN_YEAR"] != null)
            {
                int.TryParse( Request.QueryString["TRN_YEAR"].ToString(),out TRN_YEAR);
            }
            if (Request.QueryString["TRN_NUMB"] != null)
            {
                int.TryParse(Request.QueryString["TRN_NUMB"].ToString(), out TRN_NUMB);
            }


            if (Request.QueryString["QC_VALUE"] != null)
            {
                double.TryParse(Request.QueryString["QC_VALUE"].ToString(), out QC_VALUE);
            }

            if (Request.QueryString["QC_NUMB"] != null)
            {
                int.TryParse(Request.QueryString["QC_NUMB"].ToString(), out QC_NUMB);
            }

            if (Request.QueryString["Y_Code"] != null)
            {
                Y_Code = Request.QueryString["Y_Code"].ToString();
            }
            if (Request.QueryString["YDesc"] != null)
            {
                YDesc = Request.QueryString["YDesc"].ToString();
            }
            if (Request.QueryString["Y_COUNT"] != null)
            {
                Y_COUNT = Request.QueryString["Y_COUNT"].ToString();
            }
            if (Request.QueryString["Q_RESULT"] != null)
            {
                Q_RESULT = Request.QueryString["Q_RESULT"].ToString();
            }
            if (Request.QueryString["STD_TYPE"] != null)
            {
                STD_TYPE = Request.QueryString["STD_TYPE"].ToString();
            }
            if (Request.QueryString["QC_CHANGE_RESULT"] != null)
            {
                QC_CHANGE_RESULT = Request.QueryString["QC_CHANGE_RESULT"].ToString();
            }
           
            if (Request.QueryString["STATUS"] != null)
            {
                STATUS = Request.QueryString["STATUS"].ToString();
            }


            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetQCChecking_Report(oUserLoginDetail.DT_STARTDATE.Year,oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, QC_NUMB, TRN_NUMB, Y_COUNT, Y_Code, YDesc, TRN_YEAR, STD_TYPE, QC_VALUE, Q_RESULT, QC_CHANGE_RESULT, STATUS);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Columns["COMP_NAME"] == null)
                    dt.Columns.Add("COMP_NAME", typeof(string));

                if (dt.Columns["BRANCH_NAME"] == null)
                    dt.Columns.Add("BRANCH_NAME", typeof(string));

                if (dt.Columns["USER_NAME"] == null)
                    dt.Columns.Add("USER_NAME", typeof(string));
                if (dt.Columns["COMP_ADD"] == null)
                    dt.Columns.Add("COMP_ADD", typeof(string));


                if (dt.Columns["DEVELOPER_WEB"] == null)
                    dt.Columns.Add("DEVELOPER_WEB", typeof(string));
                if (dt.Columns["DEVELOPER_COMP"] == null)
                    dt.Columns.Add("DEVELOPER_COMP", typeof(string));


                foreach (DataRow dr in dt.Rows)
                {
                    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                    dr["USER_NAME"] = oUserLoginDetail.Username;
                    dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                    dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                    dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                    dt.AcceptChanges();

                }
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
