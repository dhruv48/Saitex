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

public partial class Module_FA_Reports_TrialBalanceReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static DataTable dtTrial;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (Request.QueryString["StartDate"] != null && Request.QueryString["StartDate"] != "" && Request.QueryString["EndDate"] != null && Request.QueryString["EndDate"] != "")
            {
                StartDate = DateTime.Parse(Request.QueryString["StartDate"].ToString().Trim());
                EndDate = DateTime.Parse(Request.QueryString["EndDate"].ToString().Trim());
            }

            DataTable dt = GetData(StartDate, EndDate);
            GetReport(dt);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private DataTable GetData(DateTime StartDate, DateTime EndDate)
    {
        try
        {
            if (dtTrial == null)
            {
                CreateTrialDT();
            }
            else
            {
                dtTrial.Clear();
            }

            DataTable dtAll = SaitexBL.Interface.Method.FA_Statement_Of_Account.get_Statement_Of_Account(oUserLoginDetail, StartDate, EndDate);
            if (dtAll.Rows.Count > 0)
            {
                DataView dvDebit = new DataView(dtAll);
                //DataView dvCredit = new DataView(dtAll);
                string FilterString = "G";
                // string FilterString = "G%";

                dvDebit.RowFilter = "PARENT_ID = '" + FilterString + "'";
                //   dvDebit.RowFilter = "ACCOUNT_ID like '" + FilterString + "'";
                if (dvDebit.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dvDebit.Count; iLoop++)
                    {
                        DataRow dr = dtTrial.NewRow();
                        dr["UNIQUE_ID"] = dtTrial.Rows.Count + 1;
                        dr["ACCOUNT_ID"] = dvDebit[iLoop]["ACCOUNT_ID"].ToString();
                        dr["ACCOUNT_NAME"] = dvDebit[iLoop]["ACCOUNT_NAME"].ToString();
                        dr["PARENT_ID"] = dvDebit[iLoop]["PARENT_ID"].ToString();

                        double dblAmount = 0;
                        double.TryParse(dvDebit[iLoop]["AMOUNT"].ToString(), out dblAmount);
                        dr["AMOUNT"] = dblAmount;

                        dr["IS_DEBIT"] = bool.Parse(dvDebit[iLoop]["IS_DEBIT"].ToString());

                        double dblDrAmt = 0;
                        double.TryParse(dvDebit[iLoop]["DR_TOTAL"].ToString(), out dblDrAmt);
                        dr["DR_TOTAL"] = dblDrAmt;

                        double dblCrAmt = 0;
                        double.TryParse(dvDebit[iLoop]["CR_TOTAL"].ToString(), out dblCrAmt);
                        dr["CR_TOTAL"] = dblCrAmt;

                        double dblDrTot = 0;
                        double.TryParse(dvDebit[iLoop]["DR_OP_AMOUNT"].ToString(), out dblDrTot);
                        dr["DR_OP_AMOUNT"] = dblDrTot;

                        double dblCrTot = 0;
                        double.TryParse(dvDebit[iLoop]["CR_OP_AMOUNT"].ToString(), out dblCrTot);
                        dr["CR_OP_AMOUNT"] = dblCrTot;

                        double dblDr = 0;
                        double.TryParse(dvDebit[iLoop]["DR_AMOUNT"].ToString(), out dblDr);
                        dr["DR_AMOUNT"] = dblDr;

                        double dblCr = 0;
                        double.TryParse(dvDebit[iLoop]["CR_AMOUNT"].ToString(), out dblCr);
                        dr["CR_AMOUNT"] = dblCr;

                        dr["DATE_RANGE"] = "From Date : " + StartDate.ToShortDateString() + " To : " + EndDate.ToShortDateString();
                        dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                        dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                        dr["USER_NAME"] = oUserLoginDetail.Username;
                        dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                        dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                        dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;

                        dtTrial.Rows.Add(dr);
                    }
                }
            }

            return dtTrial;
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
            ReportDocument rdoc = new ReportDocument();
            rdoc.Load(Server.MapPath(@"TrialBalanceReport.rpt"));
            rdoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rdoc;
            CrystalReportViewer1.DisplayGroupTree = false;
            CrystalReportViewer1.HasPrintButton = true;
        }
        catch
        {
            throw;
        }
    }

    private void CreateTrialDT()
    {
        try
        {
            dtTrial = new DataTable();
            dtTrial.Columns.Add("UNIQUE_ID", typeof(int));
            dtTrial.Columns.Add("ACCOUNT_ID", typeof(string));
            dtTrial.Columns.Add("ACCOUNT_NAME", typeof(string));
            dtTrial.Columns.Add("PARENT_ID", typeof(string));
            dtTrial.Columns.Add("AMOUNT", typeof(double));
            dtTrial.Columns.Add("IS_DEBIT", typeof(bool));
            dtTrial.Columns.Add("DR_AMOUNT", typeof(double));
            dtTrial.Columns.Add("CR_AMOUNT", typeof(double));
            dtTrial.Columns.Add("DR_OP_AMOUNT", typeof(double));
            dtTrial.Columns.Add("CR_OP_AMOUNT", typeof(double));
            dtTrial.Columns.Add("DR_TOTAL", typeof(double));
            dtTrial.Columns.Add("CR_TOTAL", typeof(double));
            dtTrial.Columns.Add("DATE_RANGE", typeof(string));
            dtTrial.Columns.Add("COMP_NAME", typeof(string));
            dtTrial.Columns.Add("BRANCH_NAME", typeof(string));
            dtTrial.Columns.Add("USER_NAME", typeof(string));
            dtTrial.Columns.Add("COMP_ADD", typeof(string));
            dtTrial.Columns.Add("DEVELOPER_COMP", typeof(string));
            dtTrial.Columns.Add("DEVELOPER_WEB", typeof(string));
        }
        catch
        {
            throw;
        }
    }
}