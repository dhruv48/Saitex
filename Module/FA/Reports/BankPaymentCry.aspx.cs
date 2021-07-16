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
using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Reports_BankPaymentCry : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            string VoucherNo = string.Empty;

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["StartDate"] != null && Request.QueryString["StartDate"] != "" && Request.QueryString["EndDate"] != null && Request.QueryString["EndDate"] != "")
            {
                FromDate = Request.QueryString["StartDate"].ToString().Trim();
                ToDate = Request.QueryString["EndDate"].ToString().Trim();
            }

            if (Request.QueryString["VoucherNo"] != null && Request.QueryString["VoucherNo"] != "")
            {
                VoucherNo = Request.QueryString["VoucherNo"].ToString().Trim();
            }

            DataTable dt = new DataTable();
            dt = GetData(VoucherNo, FromDate, ToDate);
            GetReport(dt);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rdoc = new ReportDocument();
            rdoc.Load(Server.MapPath(@"BankPayment.rpt"));
            rdoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rdoc;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetData(string VoucherNo, string FromDate, string ToDate)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.PrintJVReport(VoucherNo, FromDate, ToDate);

            if (dt.Columns["AMT_ISSUED"] == null)
                dt.Columns.Add("AMT_ISSUED", typeof(double));

            if (dt.Columns["AMT_RECIEVED"] == null)
                dt.Columns.Add("AMT_RECIEVED", typeof(double));

            foreach (DataRow dr in dt.Rows)
            {
                int Issued = Convert.ToInt32(dr["IS_DEBIT"]);
                double AMOUNT = 0;

                AMOUNT = double.Parse(dr["AMOUNT"].ToString());

                if (Issued == 1)
                {
                    dr["AMT_ISSUED"] = AMOUNT;
                }
                else
                {
                    dr["AMT_RECIEVED"] = AMOUNT;
                }
            }

            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));
            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));

            if (dt.Columns["DEVELOPER_COMP"] == null)
                dt.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (dt.Columns["DEVELOPER_WEB"] == null)
                dt.Columns.Add("DEVELOPER_WEB", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dt.AcceptChanges();
            }
            return dt;
        }
        catch
        {
            throw;
        }
    }

}
