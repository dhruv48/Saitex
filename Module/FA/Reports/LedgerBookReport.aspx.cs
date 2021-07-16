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

public partial class Module_FA_Reports_LedgerBookReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtLedgerBook;
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static string LedgerCode = string.Empty;

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
            if (Request.QueryString["LedgerCode"] != null && Request.QueryString["LedgerCode"] != "")
            {
                LedgerCode = Request.QueryString["LedgerCode"].ToString().Trim();
            }

            dtLedgerBook = null;
            DataTable dt = GetData();
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
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"LedgerBookReport.rpt"));
            rDoc.SetDataSource(dt);
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
            if (dtLedgerBook == null)
                CreateDataTable();

            if (LedgerCode == "" || LedgerCode == string.Empty || LedgerCode == null)
            {
                GetLedgerBook();
            }
            else
            {
                GetLedgerBookWithLedger(LedgerCode);
            }

            dtLedgerBook.Columns.Add("COMP_NAME", typeof(string));
            dtLedgerBook.Columns.Add("BRANCH_NAME", typeof(string));
            dtLedgerBook.Columns.Add("TITLE", typeof(string));
            dtLedgerBook.Columns.Add("DATE_RANGE", typeof(string));
            dtLedgerBook.Columns.Add("USER_NAME", typeof(string));
            dtLedgerBook.Columns.Add("COMP_ADD", typeof(string));

            foreach (DataRow dr in dtLedgerBook.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["TITLE"] = "Ledger Book Report";
                dr["Date_Range"] = "From Date : " + StartDate.ToShortDateString() + " To : " + EndDate.ToShortDateString();
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
            }
            return dtLedgerBook;
        }
        catch
        {
            throw;
        }
    }

    private void GetLedgerBook()
    {
        try
        {
            string LedgerCode = string.Empty;
            string LedgerName = string.Empty;
            string GroupCode = string.Empty;
            string GroupName = string.Empty;
            string LedgerGroup = string.Empty;
            string Journal_DT = string.Empty;
            string Voucher_No = string.Empty;
            string Particulars = string.Empty;
            string VoucherCode = string.Empty;
            string Description = string.Empty;
            string DocNo = string.Empty;
            string DocDT = string.Empty;
            string strOPCLBal = string.Empty;

            double DRAmt = 0;
            double CRAmt = 0;
            double OPBal = 0;
            double CLBal = 0;
            string Vou_No = string.Empty;
            int IsDoc = 0;

            DataTable dtLedger = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMaster();
            if (dtLedger.Rows.Count > 0)
            {
                DataView dv = new DataView(dtLedger);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        LedgerCode = dv[iLoop]["LDGR_CODE"].ToString();
                        LedgerName = dv[iLoop]["LDGR_NAME"].ToString();
                        GroupCode = dv[iLoop]["GRP_CODE"].ToString();

                        DataTable dtLedger_Book = SaitexBL.Interface.Method.FA_LEDGER_BOOK.Get_LEDGER_BOOK(oUserLoginDetail, StartDate, EndDate, LedgerCode, Vou_No, IsDoc);
                        DataTable dtGroup = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();
                        if (dtGroup.Rows.Count > 0)
                        {
                            DataView dvGroup = new DataView(dtGroup);

                            dvGroup.RowFilter = "GRP_CODE='" + GroupCode + "'";

                            if (dvGroup.Count > 0)
                            {
                                for (int i = 0; i < dvGroup.Count; i++)
                                {
                                    GroupName = dvGroup[i]["GRP_NAME"].ToString();
                                }
                            }
                        }

                        if (dtLedger_Book.Rows.Count > 0)
                        {
                            DataView dv1 = new DataView(dtLedger_Book);
                            if (dv1.Count > 0)
                            {
                                for (int jLoop = 0; jLoop < dv1.Count; jLoop++)
                                {
                                    LedgerCode = dv1[jLoop]["LEDGER_CODE"].ToString();
                                    LedgerGroup = GroupName;
                                    Journal_DT = dv1[jLoop]["JOURNAL_DATE"].ToString();
                                    Voucher_No = dv1[jLoop]["VOUCHER_NO"].ToString();
                                    Particulars = dv1[jLoop]["R_LEDGER_NAME"].ToString();
                                    strOPCLBal = dv1[jLoop]["R_LEDGER_CODE"].ToString();
                                    VoucherCode = dv1[jLoop]["VOUCHER_CODE"].ToString();

                                    if (Voucher_No != "")
                                    {
                                        DataTable dtJournalTrn = SaitexDL.Interface.Method.FA_Journal_DTL.SelectTRNByYear(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);

                                        if (dtJournalTrn.Rows.Count > 0)
                                        {
                                            DataView dvTrn = new DataView(dtJournalTrn);
                                            dvTrn.RowFilter = "VCHR_NO='" + Voucher_No + "'";
                                            if (dvTrn.Count > 0)
                                            {
                                                for (int j = 0; j < dvTrn.Count; j++)
                                                {
                                                    DocNo = dvTrn[j]["DOC_NO"].ToString();
                                                    DocDT = dvTrn[j]["DOC_DT"].ToString();
                                                }
                                            }
                                        }
                                    }
                                    double.TryParse(dv1[jLoop]["DR_AMOUNT"].ToString(), out DRAmt);
                                    double.TryParse(dv1[jLoop]["CR_AMOUNT"].ToString(), out CRAmt);
                                    Description = dv1[jLoop]["DESCRIPTION"].ToString();

                                    if (strOPCLBal == "999998")
                                    {
                                        CLBal = 0;
                                        double.TryParse(dv1[jLoop]["AMOUNT"].ToString(), out OPBal);
                                    }
                                    if (strOPCLBal == "999999")
                                    {
                                        OPBal = 0;
                                        double.TryParse(dv1[jLoop]["AMOUNT"].ToString(), out CLBal);
                                    }

                                    insertDataTable(LedgerCode, LedgerName, LedgerGroup, Journal_DT, Voucher_No, Particulars, VoucherCode, DocNo, DocDT, DRAmt, CRAmt, Description, OPBal, CLBal);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void GetLedgerBookWithLedger(string strLedgerCode)
    {
        try
        {
            string LedgerCode = string.Empty;
            string LedgerName = string.Empty;
            string GroupCode = string.Empty;
            string GroupName = string.Empty;
            string LedgerGroup = string.Empty;
            string Journal_DT = string.Empty;
            string Voucher_No = string.Empty;
            string Particulars = string.Empty;
            string VoucherCode = string.Empty;
            string Description = string.Empty;
            string DocNo = string.Empty;
            string DocDT = string.Empty;
            string strOPCLBal = string.Empty;
            string Vou_No = string.Empty;
            int IsDoc = 0;
            double DRAmt = 0;
            double CRAmt = 0;
            double OPBal = 0;
            double CLBal = 0;

            DataTable dtLedger = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMasterWithCode(strLedgerCode);
            if (dtLedger.Rows.Count > 0)
            {
                DataView dv = new DataView(dtLedger);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        LedgerCode = dv[iLoop]["LDGR_CODE"].ToString();
                        LedgerName = dv[iLoop]["LDGR_NAME"].ToString();
                        GroupCode = dv[iLoop]["GRP_CODE"].ToString();

                        DataTable dtLedger_Book = SaitexBL.Interface.Method.FA_LEDGER_BOOK.Get_LEDGER_BOOK(oUserLoginDetail, StartDate, EndDate, LedgerCode, Vou_No, IsDoc);
                        DataTable dtGroup = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();
                        if (dtGroup.Rows.Count > 0)
                        {
                            DataView dvGroup = new DataView(dtGroup);

                            dvGroup.RowFilter = "GRP_CODE='" + GroupCode + "'";

                            if (dvGroup.Count > 0)
                            {
                                for (int i = 0; i < dvGroup.Count; i++)
                                {
                                    GroupName = dvGroup[i]["GRP_NAME"].ToString();
                                }
                            }
                        }

                        if (dtLedger_Book.Rows.Count > 0)
                        {
                            DataView dv1 = new DataView(dtLedger_Book);
                            if (dv1.Count > 0)
                            {
                                for (int jLoop = 0; jLoop < dv1.Count; jLoop++)
                                {
                                    LedgerCode = dv1[jLoop]["LEDGER_CODE"].ToString();
                                    LedgerGroup = GroupName;
                                    Journal_DT = dv1[jLoop]["JOURNAL_DATE"].ToString();
                                    Voucher_No = dv1[jLoop]["VOUCHER_NO"].ToString();
                                    Particulars = dv1[jLoop]["R_LEDGER_NAME"].ToString();
                                    strOPCLBal = dv1[jLoop]["R_LEDGER_CODE"].ToString();
                                    VoucherCode = dv1[jLoop]["VOUCHER_CODE"].ToString();

                                    if (Voucher_No != "")
                                    {
                                        DataTable dtJournalTrn = SaitexDL.Interface.Method.FA_Journal_DTL.SelectTRNByYear(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);

                                        if (dtJournalTrn.Rows.Count > 0)
                                        {
                                            DataView dvTrn = new DataView(dtJournalTrn);
                                            dvTrn.RowFilter = "VCHR_NO='" + Voucher_No + "'";
                                            if (dvTrn.Count > 0)
                                            {
                                                for (int j = 0; j < dvTrn.Count; j++)
                                                {
                                                    DocNo = dvTrn[j]["DOC_NO"].ToString();
                                                    DocDT = dvTrn[j]["DOC_DT"].ToString();
                                                }
                                            }
                                        }
                                    }
                                    double.TryParse(dv1[jLoop]["DR_AMOUNT"].ToString(), out DRAmt);
                                    double.TryParse(dv1[jLoop]["CR_AMOUNT"].ToString(), out CRAmt);
                                    Description = dv1[jLoop]["DESCRIPTION"].ToString();

                                    if (strOPCLBal == "999998")
                                    {
                                        CLBal = 0;
                                        double.TryParse(dv1[jLoop]["AMOUNT"].ToString(), out OPBal);
                                    }
                                    if (strOPCLBal == "999999")
                                    {
                                        OPBal = 0;
                                        double.TryParse(dv1[jLoop]["AMOUNT"].ToString(), out CLBal);
                                    }

                                    insertDataTable(LedgerCode, LedgerName, LedgerGroup, Journal_DT, Voucher_No, Particulars, VoucherCode, DocNo, DocDT, DRAmt, CRAmt, Description, OPBal, CLBal);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtLedgerBook = new DataTable();
            dtLedgerBook.Columns.Add("UNIQUE_ID", typeof(int));
            dtLedgerBook.Columns.Add("LEDGER_CODE", typeof(string));
            dtLedgerBook.Columns.Add("LEDGER_NAME", typeof(string));
            dtLedgerBook.Columns.Add("LEDGER_GROUP", typeof(string));
            dtLedgerBook.Columns.Add("JOURNAL_DT", typeof(DateTime));
            dtLedgerBook.Columns.Add("VOUCHER_NO", typeof(string));
            dtLedgerBook.Columns.Add("PARTICULARS", typeof(string));
            dtLedgerBook.Columns.Add("VOUCHER_CODE", typeof(string));
            dtLedgerBook.Columns.Add("DOC_NO", typeof(string));
            dtLedgerBook.Columns.Add("DOC_DT", typeof(DateTime));
            dtLedgerBook.Columns.Add("DR_AMOUNT", typeof(double));
            dtLedgerBook.Columns.Add("CR_AMOUNT", typeof(double));
            dtLedgerBook.Columns.Add("DESC", typeof(string));
            dtLedgerBook.Columns.Add("OP_BAL", typeof(double));
            dtLedgerBook.Columns.Add("CL_BAL", typeof(double));
        }
        catch
        {
            throw;
        }
    }

    private void insertDataTable(string LedgerCode, string LedgerName, string LedgerGroup, string Journal_DT, string Voucher_No, string Particulars, string VoucherCode, string DocNo, string DocDT, double DRAmt, double CRAmt, string Description, double OPBal, double CLBal)
    {
        try
        {
            DataRow dr = dtLedgerBook.NewRow();

            dr["UNIQUE_ID"] = dtLedgerBook.Rows.Count + 1;
            dr["LEDGER_CODE"] = LedgerCode;
            dr["LEDGER_NAME"] = LedgerName;
            dr["LEDGER_GROUP"] = LedgerGroup;

            if (Journal_DT == "" || Journal_DT == null)
            {

            }
            else
            {
                dr["JOURNAL_DT"] = DateTime.Parse(Journal_DT);
            }

            dr["PARTICULARS"] = Particulars;
            dr["VOUCHER_CODE"] = VoucherCode;

            if (Voucher_No == "999998" || Voucher_No == "999999")
            {

            }
            else
            {
                dr["VOUCHER_NO"] = Voucher_No;
                dr["DOC_NO"] = DocNo;

                if (DocDT == "" || DocDT == null)
                {

                }
                else
                {
                    dr["DOC_DT"] = DateTime.Parse(DocDT);
                }
            }

            if (DRAmt == 0)
            {

            }
            else
            {
                dr["DR_AMOUNT"] = DRAmt;
            }

            if (CRAmt == 0)
            {

            }
            else
            {
                dr["CR_AMOUNT"] = CRAmt;
            }

            dr["DESC"] = Description;
            dr["OP_BAL"] = OPBal;
            dr["CL_BAL"] = CLBal;

            dtLedgerBook.Rows.Add(dr);
        }
        catch
        {
            throw;
        }
    }
}
