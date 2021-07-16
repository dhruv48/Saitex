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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Pages_AdjustPurchaseVoucher : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.FA_TDS_DEDUCT oFA_TDS_DEDUCT;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtGrid;
    private static string TextBoxId;
    private static string LedgerCode;
    double Amt;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                TextBoxId = "";

                if (Request.QueryString["LedgerCode"] != null)
                {
                    LedgerCode = Request.QueryString["LedgerCode"].ToString();
                    FillLedgerParty(LedgerCode);
                    bindPendingPayment(LedgerCode);
                }
                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
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
    /// Bind the Grid, pending payment according to selected ledger.
    /// </summary>
    /// <param name="LedgerCode">Ledger Code coming through Query String</param>
    private void bindPendingPayment(string LedgerCode)
    {
        try
        {
            CreateTableForGrid();      // Create Datatable for GridView Binding..
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUE_DTL.GetPurchaseAdjustJournal();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LEDGER_CODE='" + LedgerCode + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        string Vou_No = dv[iLoop]["VCHR_NO"].ToString();
                        double Act_amt = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                        double Remain_amt = double.Parse(dv[iLoop]["REM_AMT"].ToString());
                        double Adjust_amt = double.Parse(dv[iLoop]["ADJ_AMT"].ToString());
                        InsertGridDataTable(Vou_No, Act_amt, Remain_amt, Adjust_amt);
                        if (dtGrid != null)
                        {
                            grdPaymentAdjustment.DataSource = dtGrid;
                            grdPaymentAdjustment.DataBind();
                        }
                        else
                        {
                            lblLedgerPartyCodeError.Text = "No pending payment found against this ledger..";
                        }
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
    
    /// <summary>
    /// It goes back to the parent window, with amount.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdjustAmount_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtAdj"] != null)
                Session["dtAdj"] = null;

            double Total = 0;
            Label txtPayAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("txtPayAmt");
            Total = double.Parse(txtPayAmt.Text);

            DataTable dtAdj = createdatatableforadjustment();
            Session["dtAdj"] = dtAdj;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + Total + "','" + TextBoxId + "')", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Amount Adjust Button..\r\nSee error log for detail."));
        }
    }
    
    protected void chkPurchaseVouchers_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Label lblTotalActAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("lblTotalActAmt");
            lblTotalActAmt.Text = GetFinalTotalActualAmt().ToString();

            Label lblTotalTDSAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("lblTotalTDSAmt");
            lblTotalTDSAmt.Text = GetFinalTotalTDSAmt().ToString();

            Label lblTotalAdjAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("lblTotalAdjAmt");
            lblTotalAdjAmt.Text = GetFinalTotalAdjAmt().ToString();

            Label lblTotalPendingAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("lblTotalPendingAmt");
            lblTotalPendingAmt.Text = GetFinalTotalPendingAmt().ToString();

            Label txtTotalAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("txtTotalAmt");
            txtTotalAmt.Text = GetFinalTotalAdjustmentPayable().ToString();

            Label txtPayAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("txtPayAmt");
            txtPayAmt.Text = GetFinalTotalAdjustment().ToString();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Purchase Vouchers CheckBox Event..\r\nSee error log for detail."));
        }
    }
    
    /// <summary>
    /// It gives total Adjustment amount.
    /// </summary>
    /// <returns>Total Amount</returns>
    private double GetFinalTotalAdjustment()
    {
        try
        {
            double fAmount = 0;
            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    TextBox txtAmount = (TextBox)thisGridViewRow.FindControl("txtAmount");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        fAmount = fAmount + (double.Parse(txtAmount.Text));
                        txtAmount.ReadOnly = false;
                    }
                    else
                    {
                        txtAmount.Text = "0";
                        txtAmount.ReadOnly = true;
                    }
                }
            }
            Amt = fAmount;
            return Amt;
        }
        catch
        {
            throw;
        }
    }
    
    /// <summary>
    /// Create Datatable for Adjustment.
    /// </summary>
    /// <returns></returns>
    private DataTable createdatatableforadjustment()
    {
        try
        {
            DataTable dtAdj = new DataTable();
            if (Session["dtAdj"] == null)
            {
                dtAdj = createAdjTable();
            }
            else
            {
                dtAdj.Rows.Clear();
                dtAdj = (DataTable)Session["dtAdj"];
            }

            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    TextBox txtAmount = (TextBox)thisGridViewRow.FindControl("txtAmount");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        DataRow dr = dtAdj.NewRow();
                        dr["VCHR_NO"] = chkPurchaseVouchers.Text;
                        dr["AMOUNT"] = double.Parse(txtAmount.Text);
                        dtAdj.Rows.Add(dr);
                    }
                }
            }
            return dtAdj;
        }
        catch
        {
            throw;
        }
    }
    
    /// <summary>
    ///  Create Datatable for Adjustment session.
    /// </summary>
    /// <returns></returns>
    private DataTable createAdjTable()
    {
        try
        {
            DataTable dtAdj = new DataTable();
            dtAdj.Columns.Add("VCHR_NO", typeof(string));
            dtAdj.Columns.Add("AMOUNT", typeof(double));
            return dtAdj;
        }
        catch
        {
            throw;
        }
    }
    
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double fAmount = 0;
            double fEnterAmt = 0;
            int totalRows = grdPaymentAdjustment.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblPendingAmount = (Label)thisGridViewRow.FindControl("lblPendingAmount");
                    TextBox txtAmount = (TextBox)thisGridViewRow.FindControl("txtAmount");

                    fAmount = double.Parse(lblPendingAmount.Text);
                    fEnterAmt = double.Parse(txtAmount.Text);

                    if (fEnterAmt > fAmount)
                    {
                        Common.CommonFuction.ShowMessage("Entered amount should be less than payable amount.");
                        txtAmount.Text = "";
                        txtAmount.Focus();
                        txtAmount.Text = fAmount.ToString();
                    }
                }
            }

            Label txtPayAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("txtPayAmt");
            txtPayAmt.Text = GetFinalTotalAdjustment().ToString();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Amount TextBox TextChanged Event..\r\nSee error log for detail."));
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
            double fAmount = 0;
            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblAmount = (Label)thisGridViewRow.FindControl("lblAmount");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        fAmount = fAmount + (double.Parse(lblAmount.Text));
                    }
                }
            }
            return fAmount;
        }
        catch
        {
            throw;
        }
    }
    
    private void CreateTableForGrid()
    {
        try
        {
            dtGrid = new DataTable();
            dtGrid.Columns.Add("VCHR_NO", typeof(string));
            dtGrid.Columns.Add("ACT_AMT", typeof(double));
            dtGrid.Columns.Add("TDS_AMT", typeof(double));
            dtGrid.Columns.Add("REM_AMT", typeof(double));
            dtGrid.Columns.Add("ADJ_AMT", typeof(double));
            dtGrid.Columns.Add("PEND_AMT", typeof(double));
        }
        catch
        {
            throw;
        }
    }
    
    private void InsertGridDataTable(string Vou_No, double Act_amt, double Remain_amt, double Adjust_amt)
    {
        try
        {
            double rem_amt;
            oFA_TDS_DEDUCT = new SaitexDM.Common.DataModel.FA_TDS_DEDUCT();

            oFA_TDS_DEDUCT.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_TDS_DEDUCT.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_TDS_DEDUCT.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dtTDSDeduct = SaitexBL.Interface.Method.FA_TDS_DEDUCT.GetSumTDSDeduct(oFA_TDS_DEDUCT);
            if (dtTDSDeduct != null && dtTDSDeduct.Rows.Count > 0)
            {
                DataView dv1 = new DataView(dtTDSDeduct);
                dv1.RowFilter = "REF_VCHR_NO='" + Vou_No + "'";
                if (dv1.Count > 0)
                {
                    for (int jLoop = 0; jLoop < dv1.Count; jLoop++)
                    {
                        rem_amt = 0;
                        double tds_amt = double.Parse(dv1[jLoop]["TDS_AMT"].ToString());
                        rem_amt = Act_amt - tds_amt;

                        if (Remain_amt != tds_amt)
                        {
                            DataRow dr = dtGrid.NewRow();
                            dr["VCHR_NO"] = Vou_No;
                            dr["ACT_AMT"] = Act_amt;
                            dr["TDS_AMT"] = tds_amt;
                            dr["REM_AMT"] = rem_amt;
                            dr["ADJ_AMT"] = Adjust_amt;
                            dr["PEND_AMT"] = Convert.ToString(rem_amt - Adjust_amt);

                            dtGrid.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    DataRow dr = dtGrid.NewRow();
                    dr["VCHR_NO"] = Vou_No;
                    dr["ACT_AMT"] = Act_amt;
                    dr["TDS_AMT"] = 0;
                    dr["REM_AMT"] = Act_amt;
                    dr["ADJ_AMT"] = Adjust_amt;
                    dr["PEND_AMT"] = Convert.ToString(Act_amt - Adjust_amt);

                    dtGrid.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtGrid.NewRow();
                dr["VCHR_NO"] = Vou_No;
                dr["ACT_AMT"] = Act_amt;
                dr["TDS_AMT"] = 0;
                dr["REM_AMT"] = Act_amt;
                dr["ADJ_AMT"] = Adjust_amt;
                dr["PEND_AMT"] = Convert.ToString(Act_amt - Adjust_amt);

                dtGrid.Rows.Add(dr);
            }
        }
        catch
        {
            throw;
        }
    }
    
    private double GetFinalTotalActualAmt()
    {
        try
        {
            double fActAmount = 0;
            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblActualAmt = (Label)thisGridViewRow.FindControl("lblActualAmt");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        fActAmount = fActAmount + (double.Parse(lblActualAmt.Text));
                    }
                }
            }
            return fActAmount;
        }
        catch
        {
            throw;
        }
    }
    
    private double GetFinalTotalTDSAmt()
    {
        try
        {
            double fTDSAmount = 0;
            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblTDSAmt = (Label)thisGridViewRow.FindControl("lblTDSAmt");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        fTDSAmount = fTDSAmount + (double.Parse(lblTDSAmt.Text));
                    }
                }
            }
            return fTDSAmount;
        }
        catch
        {
            throw;
        }
    }
    
    private double GetFinalTotalAdjAmt()
    {
        try
        {
            double fAdjAmount = 0;
            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblAdjAmount = (Label)thisGridViewRow.FindControl("lblAdjAmount");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        fAdjAmount = fAdjAmount + (double.Parse(lblAdjAmount.Text));
                    }
                }
            }
            return fAdjAmount;
        }
        catch
        {
            throw;
        }
    }
    
    private double GetFinalTotalPendingAmt()
    {
        try
        {
            double fPendingAmount = 0;
            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblPendingAmount = (Label)thisGridViewRow.FindControl("lblPendingAmount");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        fPendingAmount = fPendingAmount + (double.Parse(lblPendingAmount.Text));
                    }
                }
            }
            return fPendingAmount;
        }
        catch
        {
            throw;
        }
    }
}
