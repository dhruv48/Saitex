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
using System.IO;

public partial class Module_FA_Reports_TrialBalanceFilter : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public DataTable dtTrial;
    public DateTime StartDate;
    public DateTime EndDate;
    public string LedgerCodeStart = string.Empty;
    public string LedgerCodeEnd = string.Empty;
    public string Vou_No = string.Empty;
    public string IsDoc = string.Empty;
    public string Order = string.Empty;
    public string ReportType = string.Empty;
    public bool IsLedger = false;

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

            if (Request.QueryString["LedgerCodeStart"] != null && Request.QueryString["LedgerCodeStart"] != "" && Request.QueryString["LedgerCodeEnd"] != null && Request.QueryString["LedgerCodeEnd"] != "")
            {
                LedgerCodeStart = Request.QueryString["LedgerCodeStart"].ToString().Trim();
                LedgerCodeEnd = Request.QueryString["LedgerCodeEnd"].ToString().Trim();
            }

            if (Request.QueryString["Vou_No"] != null && Request.QueryString["Vou_No"] != "")
            {
                Vou_No = Request.QueryString["Vou_No"].ToString().Trim();
            }

            if (Request.QueryString["IsDoc"] != null && Request.QueryString["IsDoc"] != "")
            {
                IsDoc = Request.QueryString["IsDoc"].ToString().Trim();
            }

            if (LedgerCodeStart == string.Empty || LedgerCodeEnd == string.Empty)
            {
                IsLedger = false;
                LedgerCodeStart = "0";
                LedgerCodeEnd = "0";
            }
            else
            {
                IsLedger = true;
            }

            if (Request.QueryString["Order"] != null && Request.QueryString["Order"] != "")
            {
                Order = Request.QueryString["Order"].ToString().Trim();
            }

            if (Request.QueryString["ReportType"] != null && Request.QueryString["ReportType"] != "")
            {
                ReportType = Request.QueryString["ReportType"].ToString().Trim();
            }

            DataTable dt = GetData(StartDate, EndDate, Vou_No, Convert.ToInt32(IsDoc), IsLedger, Convert.ToInt32(LedgerCodeStart), Convert.ToInt32(LedgerCodeEnd), Order);
            GetReport(dt, ReportType);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private DataTable GetData(DateTime StartDate, DateTime EndDate, string Vou_No, int IsDoc, bool IsLedger, int LedgerCodeStart, int LedgerCodeEnd, string Order)
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

            DataTable dtAll = SaitexBL.Interface.Method.FA_Statement_Of_Account.get_Statement_Of_AccountFilter(oUserLoginDetail, StartDate, EndDate, Vou_No, IsDoc, IsLedger, LedgerCodeStart, LedgerCodeEnd);
            if (dtAll.Rows.Count > 0)
            {
                DataView dvDebit = new DataView(dtAll);

                if (Order == "NAME")
                {
                    dvDebit.Sort = " ACCOUNT_NAME ASC"; // For Ledger Name Sorting
                }
                else
                {
                    dvDebit.Sort = " UNIQUE_ID ASC"; // For Ledger Code Sorting
                }
                //string FilterString = "L%";
                //dvDebit.RowFilter = "ACCOUNT_ID like '" + FilterString + "'";
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

    private void GetReport(DataTable dt, string ReportType)
    {
        try
        {
            ReportDocument rdoc = new ReportDocument();

            if (ReportType == "1") // For With Opening
            {
                rdoc.Load(Server.MapPath(@"TrialBalanceFilterReport.rpt"));
            }
            else if (ReportType == "2")    // For Without Opening..
            {
                rdoc.Load(Server.MapPath(@"TrialBalanceFilterReportWithOutOpen.rpt"));
            }
            else
            {
                // For Only Opening..
                rdoc.Load(Server.MapPath(@"TrialBalanceFilterReportOnlyOpen.rpt"));
            }

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