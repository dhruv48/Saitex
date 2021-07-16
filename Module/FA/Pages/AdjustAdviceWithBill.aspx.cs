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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Pages_AdjustAdviceWithBill : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE oFA_ADVANCED_ADVICE;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtAdviceBill;
    private static string LedgerCode;
    private static double dblJVAmount;
    private static string TextBoxId = string.Empty;
    double Amt;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                if (Request.QueryString["LedgerCode"] != null && Request.QueryString["dblJVAmount"] != null && Request.QueryString["TextBoxId"] != null)
                {
                    LedgerCode = Request.QueryString["LedgerCode"].ToString();
                    dblJVAmount = double.Parse(Request.QueryString["dblJVAmount"].ToString());
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                    FillLedgerParty(LedgerCode);
                    bindPendingPayment(LedgerCode);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Query String returns no value... Check your query string first ..... ");
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    /// <summary>
    /// Fill the Ledger name, according to Ledger Code.
    /// </summary>
    /// <param name="LedgerCode">Ledger Code coming through Query String</param>
    private void FillLedgerParty(string LedgerCode)
    {
        try
        {
            lblLedgerPartyCode.Text = LedgerCode;
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LDGR_CODE='" + LedgerCode + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        lblLedgerPartyName.Text = dv[iLoop]["LDGR_NAME"].ToString();
                    }
                }
            }
            else
            {
                lblLedgerPartyCodeError.Text = "No Record exists for adjustment for provided ledger..";
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Bind the Grid, pending Advanced Advice, according to selected ledger.
    /// </summary>
    /// <param name="LedgerCode">Ledger Code coming through Query String</param>
    private void bindPendingPayment(string LedgerCode)
    {
        try
        {
            if (dtAdviceBill == null)
            {
                CreateDT();
            }
            else
            {
                dtAdviceBill.Rows.Clear();
            }

            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();
            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_ADVANCED_ADVICE.LEDGER_CODE = LedgerCode;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.GetPendingAdviceForBillAdjustment(oFA_ADVANCED_ADVICE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        string strAdviceNo = dv[iLoop]["ADV_NO"].ToString();
                        string strAdviceDT = dv[iLoop]["ADV_DATE"].ToString();
                        string strLedgerCode = lblLedgerPartyCode.Text.Trim();
                        string strVouNo = dv[iLoop]["VCHR_NO"].ToString();
                        string strVouDT = dv[iLoop]["TRN_DATE"].ToString();
                        double dblAdvAmt = double.Parse(dv[iLoop]["ADV_AMT"].ToString());

                        DataTable dt1 = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.GetAdviceBillAdjustment(oFA_ADVANCED_ADVICE);
                        DataView dv1 = new DataView(dt1);
                        dv1.RowFilter = "ADV_NO='" + strAdviceNo + "'";
                        if (dv1.Count > 0)
                        {
                            double dblTmp = 0;
                            for (int jLoop = 0; jLoop < dv1.Count; jLoop++)
                            {
                                dblTmp = double.Parse(dv1[jLoop]["ADJ_AMT"].ToString());
                            }

                            if (dblAdvAmt != dblTmp)
                            {
                                dblAdvAmt = dblAdvAmt - dblTmp;
                                InsertAdviceBillDT(strAdviceNo, strAdviceDT, strLedgerCode, strVouNo, strVouDT, dblAdvAmt);
                            }
                        }
                        else
                        {
                            InsertAdviceBillDT(strAdviceNo, strAdviceDT, strLedgerCode, strVouNo, strVouDT, dblAdvAmt);
                        }
                        grdPaymentAdjustment.DataSource = dtAdviceBill;
                        grdPaymentAdjustment.DataBind();
                    }
                }
                else
                {
                    lblLedgerPartyCodeError.Text = "No pending payment found against this ledger..";
                }
            }
            else
            {
                lblLedgerPartyCodeError.Text = "No such record found against this ledger..";
            }
        }
        catch
        {
            throw;
        }
    }

    protected void chkPurchaseVouchers_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            double dblTot;
            dblTot = 0;

            Label txtTotalAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("txtTotalAmt");
            txtTotalAmt.Text = GetFinalTotalAdjustmentPayable().ToString();
            dblTot = double.Parse(txtTotalAmt.Text.Trim());
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// It returns Final total adjust amount Payable.
    /// </summary>
    /// <returns>Payable Amount</returns>
    private double GetFinalTotalAdjustmentPayable()
    {
        try
        {
            int iChecking = 0;
            double dblAmount = 0;
            string strAdviceNo = string.Empty;
            int totalRows = grdPaymentAdjustment.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblAmount = (Label)thisGridViewRow.FindControl("lblAmount");
                    strAdviceNo = chkPurchaseVouchers.Text;

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        dblAmount = dblAmount + (double.Parse(lblAmount.Text));
                        DataView dvEdit = new DataView(dtAdviceBill);
                        dvEdit.RowFilter = "ADV_NO='" + strAdviceNo + "'";
                        if (dvEdit.Count > 0)
                        {
                            dvEdit[0]["IS_ADJUST"] = true;
                            dtAdviceBill.AcceptChanges();
                        }
                        if (dblAmount > dblJVAmount)
                        {
                            Common.CommonFuction.ShowMessage("Advice Amount should not be greater than Bill Amount but this Advice amount you can use as partial advice..");
                            iChecking = iChecking + 1;
                            UpdateDTForPartialAmount(strAdviceNo, dblAmount, double.Parse(lblAmount.Text));

                            if (iChecking > 1)
                            {
                                Common.CommonFuction.ShowMessage("Advice Amount should not be greater than Bill Amount..");

                                if (strAdviceNo == chkPurchaseVouchers.Text)
                                {
                                    chkPurchaseVouchers.Checked = false;
                                }
                                else
                                {
                                    chkPurchaseVouchers.Checked = true;
                                }
                                dblAmount = dblAmount - (double.Parse(lblAmount.Text));
                            }
                        }
                    }
                }
            }
            Amt = dblAmount;
            return Amt;
        }
        catch
        {
            throw;
        }
    }

    private void UpdateDTForPartialAmount(string strAdviceNo, double dblAmount, double dblAdvAmt)
    {
        try
        {
            double dblPartAmt = 0;
            double dblPartialAmt = 0;
            dblPartAmt = dblAmount - dblJVAmount;
            dblPartialAmt = dblAdvAmt - dblPartAmt;

            DataView dvEdit = new DataView(dtAdviceBill);
            dvEdit.RowFilter = "ADV_NO='" + strAdviceNo + "'";
            if (dvEdit.Count > 0)
            {
                dvEdit[0]["IS_PARTIAL"] = true;
                dvEdit[0]["PARTIAL_AMT"] = dblPartialAmt;
                dtAdviceBill.AcceptChanges();
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Create Datatable for Advice Bill...
    /// </summary>
    private void CreateDT()
    {
        try
        {
            dtAdviceBill = new DataTable();
            dtAdviceBill.Columns.Add("UNIQUE_ID", typeof(int));
            dtAdviceBill.Columns.Add("ADV_NO", typeof(string));
            dtAdviceBill.Columns.Add("ADV_DATE", typeof(string));
            dtAdviceBill.Columns.Add("LEDGER_CODE", typeof(string));
            dtAdviceBill.Columns.Add("VCHR_NO", typeof(string));
            dtAdviceBill.Columns.Add("TRN_DATE", typeof(string));
            dtAdviceBill.Columns.Add("ADV_AMT", typeof(double));
            dtAdviceBill.Columns.Add("IS_ADJUST", typeof(bool));
            dtAdviceBill.Columns.Add("IS_PARTIAL", typeof(bool));
            dtAdviceBill.Columns.Add("PARTIAL_AMT", typeof(double));
        }
        catch
        {
            throw;
        }
    }

    protected void btnAdjustAmount_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtAdjBill"] != null)
                Session["dtAdjBill"] = null;

            if (grdPaymentAdjustment.Rows.Count > 0)
            {
                double Total = 0;
                double dblTemp = 0;
                double dblTemp1 = 0;
                Label txtTotalAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("txtTotalAmt");
                double.TryParse(txtTotalAmt.Text.Trim(), out dblTemp);

                if (dblJVAmount > dblTemp)
                {
                    dblTemp1 = dblJVAmount - dblTemp;
                    Total = dblTemp1;
                }
                else
                {
                    Total = 0;
                }

                Session["dtAdjBill"] = dtAdviceBill;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + Total + "','" + TextBoxId + "')", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Adjust Button..\r\nSee error log for detail."));
        }
    }

    /// <summary>
    /// Create Datatable for Advanced Advice With Bill...
    /// </summary>
    /// <returns></returns>
    private DataTable CreateAdviceBillDT()
    {
        try
        {
            if (dtAdviceBill == null)
            {
                CreateDT();
            }
            else
            {
                dtAdviceBill.Rows.Clear();
            }

            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblAdviceDate = (Label)thisGridViewRow.FindControl("lblAdviceDate");
                    Label lblVchrNo = (Label)thisGridViewRow.FindControl("lblVchrNo");
                    Label lblVchrDT = (Label)thisGridViewRow.FindControl("lblVchrDT");
                    Label lblAmount = (Label)thisGridViewRow.FindControl("lblAmount");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        DataRow dr = dtAdviceBill.NewRow();
                        dr["UNIQUE_ID"] = dtAdviceBill.Rows.Count + 1;
                        dr["ADV_NO"] = chkPurchaseVouchers.Text;
                        dr["ADV_DATE"] = lblAdviceDate.Text;
                        dr["LEDGER_CODE"] = lblLedgerPartyCode.Text.Trim();
                        dr["VCHR_NO"] = lblVchrNo.Text;
                        dr["TRN_DATE"] = lblVchrDT.Text;
                        dr["ADV_AMT"] = double.Parse(lblAmount.Text);
                        dr["IS_ADJUST"] = true;
                        dr["IS_PARTIAL"] = false;
                        dr["PARTIAL_AMT"] = 0;

                        dtAdviceBill.Rows.Add(dr);
                    }
                }
            }
            return dtAdviceBill;
        }
        catch
        {
            throw;
        }
    }

    private void InsertAdviceBillDT(string strAdviceNo, string strAdviceDT, string strLedgerCode, string strVouNo, string strVouDT, double dblAdvAmt)
    {
        try
        {
            DataRow dr = dtAdviceBill.NewRow();
            dr["UNIQUE_ID"] = dtAdviceBill.Rows.Count + 1;
            dr["ADV_NO"] = strAdviceNo;
            dr["ADV_DATE"] = strAdviceDT;
            dr["LEDGER_CODE"] = strLedgerCode;
            dr["VCHR_NO"] = strVouNo;
            dr["TRN_DATE"] = strVouDT;
            dr["ADV_AMT"] = dblAdvAmt;
            dr["IS_ADJUST"] = false;
            dr["IS_PARTIAL"] = false;
            dr["PARTIAL_AMT"] = 0;
            dtAdviceBill.Rows.Add(dr);
        }
        catch
        {
            throw;
        }
    }
}