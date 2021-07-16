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

public partial class Module_FA_Pages_AdjustAdvanceAdvice : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE oFA_ADVANCED_ADVICE;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_CONTRACT_MST oFA_CONTRACT_MST;
    SaitexDM.Common.DataModel.FA_TAX_MST oFA_TAX_MST;
    private static DataTable dtTDSDeduction;
    private static DataTable dtFinalAmt;
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
                trTDS.Visible = false;
                trTDSGrid.Visible = false;
                trFinalAmt.Visible = false;
                ddlContractCode.Visible = false;
                btnTDS.Visible = false;

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
    
    protected void btnAdjustAmount_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtAdjAdvice"] != null)
                Session["dtAdjAdvice"] = null;
            if (Session["dtTDS"] != null)
                Session["dtTDS"] = null;
            if (Session["ContractCode"] != null)
                Session["ContractCode"] = null;

            if (grdPaymentAdjustment.Rows.Count > 0)
            {
                if (chkTDS.Visible == true && chkTDS.Checked == true)
                {
                    if (grdTDS.Rows.Count > 0)
                    {
                        double Total = 0;
                        Label lblTotalPayAmt = (Label)grdFinalAmt.FooterRow.FindControl("lblTotalPayAmt");
                        Total = double.Parse(lblTotalPayAmt.Text);

                        DataTable dtAdj = CreateTDSAdjustDT();
                        Session["dtAdjAdvice"] = dtAdj;
                        Session["dtTDS"] = dtTDSDeduction;
                        Session["ContractCode"] = ddlContractCode.SelectedValue.ToString().Trim();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + Total + "','" + TextBoxId + "')", true);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please select Contract Code and Click on Save TDS Button..");
                    }
                }
                else
                {
                    double Total = 0;
                    Label txtTotalAmt = (Label)grdPaymentAdjustment.FooterRow.FindControl("txtTotalAmt");
                    Total = double.Parse(txtTotalAmt.Text);

                    DataTable dtAdj = createdatatableforadjustment();
                    Session["dtAdjAdvice"] = dtAdj;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + Total + "','" + TextBoxId + "')", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Adjust Button..\r\nSee error log for detail."));
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
            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();
            // To Check validation for Financial Year.
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            }

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.GetPendingAdvice(oFA_ADVANCED_ADVICE);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LEDGER_CODE='" + LedgerCode + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        grdPaymentAdjustment.DataSource = dv;
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
            
            if (dblTot > 0)
            {
                DataTable dt = SaitexBL.Interface.Method.FA_TDS_MST.SelectAll();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "LDGR_CODE='" + LedgerCode + "'";
                    if (dv.Count > 0)
                    {
                        trTDS.Visible = true;
                        trTDSGrid.Visible = true;
                        chkTDS.Checked = false;
                        ddlContractCode.SelectedIndex = -1;
                        if (dtTDSDeduction != null)
                        {
                            dtTDSDeduction.Rows.Clear();
                            grdTDS.DataBind();
                        }
                    }
                }
            }
            else
            {
                trTDS.Visible = false;
                trTDSGrid.Visible = false;
                trFinalAmt.Visible = false;
                chkTDS.Checked = false;
                ddlContractCode.Visible = false;
                btnTDS.Visible = false;
                ddlContractCode.SelectedIndex = -1;
                if (dtTDSDeduction != null)
                {
                    dtTDSDeduction.Rows.Clear();
                    grdTDS.DataBind();
                }
                if (dtFinalAmt != null)
                {
                    dtFinalAmt.Rows.Clear();
                    grdFinalAmt.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Purchase Voucher CheckBox..\r\nSee error log for detail."));
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
            Amt = fAmount;
            return Amt;
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
            dtAdj.Columns.Add("ADV_NO", typeof(string));
            dtAdj.Columns.Add("ADV_DATE", typeof(DateTime));
            dtAdj.Columns.Add("ADV_AMT", typeof(double));
            dtAdj.Columns.Add("TDS_DEDUCT_AMT", typeof(double));
            return dtAdj;
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
            if (Session["dtAdjAdvice"] == null)
            {
                dtAdj = createAdjTable();
            }
            else
            {
                dtAdj.Rows.Clear();
                dtAdj = (DataTable)Session["dtAdjAdvice"];
            }

            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblAmount = (Label)thisGridViewRow.FindControl("lblAmount");
                    Label lblAdviceDate = (Label)thisGridViewRow.FindControl("lblAdviceDate");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        DataRow dr = dtAdj.NewRow();
                        dr["ADV_NO"] = chkPurchaseVouchers.Text;
                        dr["ADV_DATE"] = Convert.ToDateTime(lblAdviceDate.Text);
                        dr["ADV_AMT"] = double.Parse(lblAmount.Text);
                        dr["TDS_DEDUCT_AMT"] = 0;
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
    
    protected void chkTDS_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkTDS.Checked == true)
            {
                ddlContractCode.Visible = true;
                btnTDS.Visible = true;
                trFinalAmt.Visible = true;
                bindContractDropdown();
            }
            else
            {
                ddlContractCode.Visible = false;
                btnTDS.Visible = false;
                trFinalAmt.Visible = false;
                if (dtTDSDeduction != null)
                {
                    dtTDSDeduction.Rows.Clear();
                    grdTDS.DataBind();
                }
                if (dtFinalAmt != null)
                {
                    dtFinalAmt.Rows.Clear();
                    grdFinalAmt.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in TDS CheckBox..\r\nSee error log for detail."));
        }
    }
    
    protected void ddlContractCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindContractDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Contract Master Loading..\r\nSee error log for detail."));
        }
    }
    
    protected void btnTDS_Click(object sender, EventArgs e)
    {
        try
        {
            dtTDSDeduction = null;
            grdTDS.DataSource = dtTDSDeduction;
            grdTDS.DataBind();

            oFA_CONTRACT_MST = new SaitexDM.Common.DataModel.FA_CONTRACT_MST();
            oFA_TAX_MST = new SaitexDM.Common.DataModel.FA_TAX_MST();

            string Entry_type = string.Empty;
            string Ledger_Code = string.Empty;
            string Ledger_Name = string.Empty;
            bool IsDebit = false;
            double Amount = 0;
            double Debit_Amount = 0;
            double Credit_amount = 0;
            string Doc_No = string.Empty;
            string Doc_Dt = string.Empty;

            if (trTDS.Visible == true)
            {
                if (ddlContractCode.SelectedIndex != -1)
                {
                    if (chkTDS.Checked == true)
                    {
                        if (dtTDSDeduction == null)
                            CreateTDSDataTable();

                        Amount = GetFinalTotalAdjustmentPayable();

                        oFA_CONTRACT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFA_CONTRACT_MST.CONTRACT_CODE = ddlContractCode.SelectedValue.ToString().Trim();

                        DataTable dt = SaitexBL.Interface.Method.FA_CONTRACT_MST.SelectTRNByCONTRACT_CODE(oFA_CONTRACT_MST);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dt);

                            if (dv.Count > 0)
                            {
                                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                                {
                                    Entry_type = "Cr";
                                    IsDebit = false;
                                    double fTax = 0;
                                    double fTaxCalc = 0;
                                    fTax = double.Parse(dv[iLoop]["TAX_RATE"].ToString());
                                    fTaxCalc = (Amount * (fTax / 100));
                                    Credit_amount = fTaxCalc;

                                    oFA_TAX_MST.TAX_CODE = dv[iLoop]["TAX_CODE"].ToString();

                                    DataTable dtTax = SaitexBL.Interface.Method.FA_TAX_MST.SelectByTAX_CODE(oFA_TAX_MST);

                                    if (dtTax != null && dtTax.Rows.Count > 0)
                                    {
                                        DataView dvTax = new DataView(dtTax);
                                        if (dvTax.Count > 0)
                                        {
                                            for (int jLoop = 0; jLoop < dvTax.Count; jLoop++)
                                            {
                                                Ledger_Code = dvTax[jLoop]["LDGR_CODE"].ToString();
                                                Ledger_Name = dvTax[jLoop]["LDGR_NAME"].ToString();

                                                DataRow drCredit = dtTDSDeduction.NewRow();
                                                drCredit["UNIQUE_ID"] = dtTDSDeduction.Rows.Count + 1;
                                                drCredit["ENTRY_TYPE"] = Entry_type;
                                                drCredit["LEDGER_CODE"] = Ledger_Code;
                                                drCredit["LEDGER_NAME"] = Ledger_Name;
                                                drCredit["IS_DEBIT"] = IsDebit;
                                                drCredit["AMOUNT"] = fTaxCalc;

                                                Debit_Amount = 0;
                                                if (Debit_Amount > 0)
                                                    drCredit["DR_AMOUNT"] = Debit_Amount;
                                                if (Credit_amount > 0)
                                                    drCredit["CR_AMOUNT"] = Credit_amount;
                                                drCredit["TAX_PERCENT"] = fTax;
                                                drCredit["DOC_NO"] = Doc_No;
                                                drCredit["DOC_DT"] = Doc_Dt;
                                                drCredit["DESC"] = "TDS Tax Credited Account.";

                                                dtTDSDeduction.Rows.Add(drCredit);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        Entry_type = "Dr";
                        Ledger_Code = lblLedgerPartyCode.Text.Trim();
                        Ledger_Name = lblLedgerPartyName.Text.Trim();
                        IsDebit = true;

                        double fTDS = 0;
                        double fTDSCalc = 0;
                        double fTDSPercentage = 0;
                        double fTDSPercentageCalc = 0;

                        if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
                        {
                            DataView dvTDS = new DataView(dtTDSDeduction);
                            if (dvTDS.Count > 0)
                            {
                                for (int iLoop = 0; iLoop < dvTDS.Count; iLoop++)
                                {
                                    fTDS = 0;
                                    double.TryParse(dvTDS[iLoop]["AMOUNT"].ToString(), out fTDS);
                                    fTDSCalc = fTDSCalc + fTDS;

                                    fTDSPercentage = 0;
                                    double.TryParse(dvTDS[iLoop]["TAX_PERCENT"].ToString(), out fTDSPercentage);
                                    fTDSPercentageCalc = fTDSPercentageCalc + fTDSPercentage;
                                }
                            }
                        }

                        DataRow drDebit = dtTDSDeduction.NewRow();
                        drDebit["UNIQUE_ID"] = dtTDSDeduction.Rows.Count + 1;
                        drDebit["ENTRY_TYPE"] = Entry_type;
                        drDebit["LEDGER_CODE"] = Ledger_Code;
                        drDebit["LEDGER_NAME"] = Ledger_Name;
                        drDebit["IS_DEBIT"] = IsDebit;
                        drDebit["AMOUNT"] = fTDSCalc;

                        Debit_Amount = fTDSCalc;
                        Credit_amount = 0;

                        if (Debit_Amount > 0)
                            drDebit["DR_AMOUNT"] = Debit_Amount;
                        if (Credit_amount > 0)
                            drDebit["CR_AMOUNT"] = Credit_amount;
                        drDebit["DOC_NO"] = Doc_No;
                        drDebit["DOC_DT"] = Doc_Dt;
                        drDebit["DESC"] = "TDS Tax Debited Party Account.";

                        dtTDSDeduction.Rows.Add(drDebit);

                        BindTDSGridFromTable();
                        BindFinalAmtGrid();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please Check the TDS Deduction CheckBox first..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please select Party Contract Code first..");
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in TDS Deduct Button..\r\nSee error log for detail."));
        }
    }
    
    /// <summary>
    /// Create Datatable for TDS GridView..
    /// </summary>
    private void CreateTDSDataTable()
    {
        try
        {
            dtTDSDeduction = new DataTable();
            dtTDSDeduction.Columns.Add("UNIQUE_ID", typeof(int));
            dtTDSDeduction.Columns.Add("ENTRY_TYPE", typeof(string));
            dtTDSDeduction.Columns.Add("LEDGER_CODE", typeof(string));
            dtTDSDeduction.Columns.Add("LEDGER_NAME", typeof(string));
            dtTDSDeduction.Columns.Add("IS_DEBIT", typeof(bool));
            dtTDSDeduction.Columns.Add("AMOUNT", typeof(double));
            dtTDSDeduction.Columns.Add("DR_AMOUNT", typeof(double));
            dtTDSDeduction.Columns.Add("CR_AMOUNT", typeof(double));
            dtTDSDeduction.Columns.Add("TAX_PERCENT", typeof(double));
            dtTDSDeduction.Columns.Add("DESC", typeof(string));
            dtTDSDeduction.Columns.Add("DOC_NO", typeof(string));
            dtTDSDeduction.Columns.Add("DOC_DT", typeof(string));
        }
        catch
        {
            throw;
        }
    }
    
    private void bindContractDropdown()
    {
        try
        {
            ddlContractCode.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.FA_TDS_MST.SelectAll();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);

                dv.RowFilter = "LDGR_CODE='" + lblLedgerPartyCode.Text.Trim() + "'";
                if (dv.Count > 0)
                {
                    ddlContractCode.DataSource = dv;
                    ddlContractCode.DataBind();
                }
            }
        }
        catch
        {
            throw;
        }
    }
    
    /// <summary>
    /// Bind TDS GridView And Bind Footer..
    /// </summary>
    private void BindTDSGridFromTable()
    {
        try
        {
            if (dtTDSDeduction != null)
            {
                DataView dv = new DataView(dtTDSDeduction);
                dv.Sort = "ENTRY_TYPE ASC";
                dtTDSDeduction.AcceptChanges();
            }

            grdTDS.DataSource = dtTDSDeduction;
            grdTDS.DataBind();

            if (dtTDSDeduction != null)
            {
                double Debit_Total = 0;
                double Credit_Total = 0;
                double Tax_Total = 0;
                CalculateDebitCreditTotalForTDS(out Debit_Total, out Credit_Total, out Tax_Total);

                Label lblDr_Amount_ftr = (Label)grdTDS.FooterRow.FindControl("lblDr_Amount_ftr");
                Label lblCr_Amount_ftr = (Label)grdTDS.FooterRow.FindControl("lblCr_Amount_ftr");
                Label lblTax_ftr = (Label)grdTDS.FooterRow.FindControl("lblTax_ftr");
                lblCr_Amount_ftr.Text = Credit_Total.ToString();
                lblDr_Amount_ftr.Text = Debit_Total.ToString();
                lblTax_ftr.Text = Tax_Total.ToString();
            }
        }
        catch
        {
            throw;
        }
    }
    
    /// <summary>
    /// To calculate Dr Cr and Total tax for Grid Footer
    /// </summary>
    /// <param name="Debit_Total"></param>
    /// <param name="Credit_Total"></param>
    /// <param name="Tax_Total"></param>
    private void CalculateDebitCreditTotalForTDS(out double Debit_Total, out double Credit_Total, out double Tax_Total)
    {
        try
        {
            Debit_Total = 0;
            Credit_Total = 0;
            Tax_Total = 0;

            foreach (DataRow dr in dtTDSDeduction.Rows)
            {
                double amt = 0;
                double.TryParse(dr["DR_AMOUNT"].ToString(), out amt);
                Debit_Total = Debit_Total + amt;
                amt = 0;
                double.TryParse(dr["CR_AMOUNT"].ToString(), out amt);
                Credit_Total = Credit_Total + amt;
                amt = 0;
                double.TryParse(dr["TAX_PERCENT"].ToString(), out amt);
                Tax_Total = Tax_Total + amt;
            }
        }
        catch
        {
            throw;
        }
    }
    
    /// <summary>
    /// Create Datatable for Final Amount GridView..
    /// </summary>
    private void CreateFinalDataTable()
    {
        try
        {
            dtFinalAmt = new DataTable();
            dtFinalAmt.Columns.Add("UNIQUE_ID", typeof(int));
            dtFinalAmt.Columns.Add("ADV_AMOUNT", typeof(double));
            dtFinalAmt.Columns.Add("TDS_AMOUNT", typeof(double));
            dtFinalAmt.Columns.Add("PAY_AMOUNT", typeof(double));
        }
        catch
        {
            throw;
        }
    }
    
    private void BindFinalAmtGrid()
    {
        try
        {
            trFinalAmt.Visible = true;
            double dblAdvAmt;
            double dblTaxAmt;
            double dblPayAmt;
            dblAdvAmt = 0;
            dblTaxAmt = 0;
            dblPayAmt = 0;
            dblAdvAmt = GetFinalTotalAdjustmentPayable();
            dblTaxAmt = GetFinalTaxAmount();
            dblPayAmt = (dblAdvAmt - dblTaxAmt);

            if (dtFinalAmt == null)
            {
                CreateFinalDataTable();
            }
            else
            {
                if (dtFinalAmt.Rows.Count > 0)
                {
                    dtFinalAmt.Rows.Clear();
                }
            }
            if (dtFinalAmt.Rows.Count == 0)
            {
                DataRow dr = dtFinalAmt.NewRow();
                dr["UNIQUE_ID"] = dtFinalAmt.Rows.Count + 1;
                dr["ADV_AMOUNT"] = dblAdvAmt;
                dr["TDS_AMOUNT"] = dblTaxAmt;
                dr["PAY_AMOUNT"] = dblPayAmt;
                dtFinalAmt.Rows.Add(dr);
            }
            if (dtFinalAmt.Rows.Count > 0)
            {
                trFinalAmt.Visible = true;
                grdFinalAmt.DataSource = dtFinalAmt;
                grdFinalAmt.DataBind();

                Label lblTotalPayAmt = (Label)grdFinalAmt.FooterRow.FindControl("lblTotalPayAmt");
                lblTotalPayAmt.Text = dblPayAmt.ToString();
            }
            else
            {
                trFinalAmt.Visible = false;
                dtFinalAmt.Rows.Clear();
                grdFinalAmt.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    
    /// <summary>
    /// It returns Final Total Tax Deduct Amount.
    /// </summary>
    /// <returns>TAX Deducted Amount</returns>
    private double GetFinalTaxAmount()
    {
        try
        {
            double Total = 0;
            Label lblCr_Amount_ftr = (Label)grdTDS.FooterRow.FindControl("lblCr_Amount_ftr");
            Total = double.Parse(lblCr_Amount_ftr.Text.Trim());
            return Total;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Create Datatable for TDS Adjustment Amount.
    /// </summary>
    /// <returns></returns>
    private DataTable CreateTDSAdjustDT()
    {
        try
        {
            DataTable dtAdj = new DataTable();
            if (Session["dtAdjAdvice"] == null)
            {
                dtAdj = createAdjTable();
            }
            else
            {
                dtAdj.Rows.Clear();
                dtAdj = (DataTable)Session["dtAdjAdvice"];
            }

            int totalRows = grdPaymentAdjustment.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdPaymentAdjustment.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkPurchaseVouchers = (CheckBox)thisGridViewRow.FindControl("chkPurchaseVouchers");
                    Label lblAmount = (Label)thisGridViewRow.FindControl("lblAmount");
                    Label lblAdviceDate = (Label)thisGridViewRow.FindControl("lblAdviceDate");
                    Label lblTax_ftr = (Label)grdTDS.FooterRow.FindControl("lblTax_ftr");

                    if (chkPurchaseVouchers.Checked == true)
                    {
                        DataRow dr = dtAdj.NewRow();
                        dr["ADV_NO"] = chkPurchaseVouchers.Text;
                        dr["ADV_DATE"] = Convert.ToDateTime(lblAdviceDate.Text);
                        dr["ADV_AMT"] = double.Parse(lblAmount.Text);
                        double dblAdvAmt = double.Parse(lblAmount.Text);
                        double dblTax = double.Parse(lblTax_ftr.Text);
                        double dblTDSAmt = ((dblAdvAmt * dblTax) / 100);
                        dr["TDS_DEDUCT_AMT"] = dblTDSAmt;
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
}
