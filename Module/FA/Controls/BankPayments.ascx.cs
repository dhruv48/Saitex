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
using Obout.ComboBox;
using Obout.Interface;

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_BankPayments : System.Web.UI.UserControl
{
    public string JournalId = string.Empty;
    public string TDSJournalId = string.Empty;
    public string VoucherCode = "23";         // For Payment Voucher Fixed
    private DataTable dtJournalDetail, dtChequeNo, dtBankChargesJournal, dtTDSDeduction, dtAdvancedAdvice, dtPartyPayment;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_TDS_DEDUCT oFA_TDS_DEDUCT;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST, oBankChargesJournal_Mst, oTDSJournal_Mst;
    SaitexDM.Common.DataModel.FA_PAYMENT_MODE oFA_PAYMENT_MODE;
    SaitexDM.Common.DataModel.FA_CHEQUE_DTL oFA_CHEQUE_DTL;
    SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE oFA_ADVANCED_ADVICE;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    /// <summary>
    /// Use of Intialization. Means blank controls.
    /// </summary>
    private void InitialisePage()
    {
        try
        {
            ddlLedgerCode.Items.Insert(0, new ListItem("-------- Select Ledger ----------", "0"));
            ddlTypeOfPayment.Items.Insert(0, new ListItem("----- Type Of Payment ----", "0"));
            ddlModeOfPayment.Items.Insert(0, new ListItem("---- Mode Of Payment ----", "0"));
            ddlChequeBookNo.Items.Insert(0, new ListItem("-------- Select Cheque Book No --------", "0"));
            ddlChequeNo.Items.Insert(0, new ListItem("---- Select Cheque No ----", "0"));

            BlankControls();
            if (txtJournalDate.Text == "")
                txtJournalDate.Text = System.DateTime.Now.Date.ToShortDateString();

            if (txtChequeDate.Text == "")
                txtChequeDate.Text = System.DateTime.Now.Date.ToShortDateString();

            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            ddlVoucherNo.Visible = false;

            txtVoucherNo.AutoPostBack = false;

            ddlTypeOfPayment.Visible = true;
            ddlModeOfPayment.Visible = true;
            txtTypeOfPayment.Visible = false;
            txtModeOfPayment.Visible = false;
            txtPaymentVoucher.ReadOnly = true;
            ddlTypeOfPayment.SelectedIndex = 0;
            ddlModeOfPayment.SelectedIndex = 0;
            ddlLedgerCode.Enabled = true;
            ddlEntry_Type.Enabled = false;
            txtVoucherNo.ReadOnly = true;
            txtJournalDate.ReadOnly = true;
            txtAmount.ReadOnly = true;
            txtVoucherNo.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtTranDescription.Text = string.Empty;
            txtPaymentNo.Text = string.Empty;
            txtAmountPayable.Text = string.Empty;

            bindPaymentVoucher();
            bindPaymentBankAndCash();
            bindBankChargesCode();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// To find the Voucher Name And According to voucher it also return voucher number.
    /// </summary>
    private void bindPaymentVoucher()
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "VCHR_NAME='" + "BANK PAYMENT" + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtPaymentVoucher.Text = dv[iLoop]["VCHR_NAME"].ToString();
                    }
                }
            }
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
            string Voucher_No = SaitexBL.Interface.Method.FA_Journal_DTL.GetVoucherNo(oFA_Journal_MST, out JournalId);
            ViewState["JournalId"] = JournalId;
            txtVoucherNo.Text = Voucher_No;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Fill Payment Ledgers Bank And Cash Only.
    /// </summary>
    private void bindPaymentBankAndCash()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetItems("", 0, 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlPaymentLedger.DataSource = dt;
                ddlPaymentLedger.DataValueField = "LDGR_CODE";
                ddlPaymentLedger.DataTextField = "LDGR_NAME";
                ddlPaymentLedger.DataBind();
                ddlPaymentLedger.Items.Insert(0, new ListItem("-------- Select Payment Ledger --------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindBankChargesCode()   // For Bank Charges Ledger Code
    {
        try
        {
            txtChargesCode.Text = "56";
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTypeOfPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlLedgerCode.Items.Clear();
            if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")
            {
                bindOnAccountLedgerCode();
                blnAdjustment.Enabled = true;
                ddlModeOfPayment.Focus();
            }
            else
            {
                bindLedgerCode();
                blnAdjustment.Enabled = true;
                ddlModeOfPayment.Focus();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Type Of Payment..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlModeOfPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            if (ddlModeOfPayment.SelectedItem.Text == "CHEQUE")
            {
                if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")
                {
                    blnAdjustment.Enabled = true;
                }
                else
                {
                    blnAdjustment.Enabled = true;
                }

                trChequeRow.Visible = true;
                ddlChequeBookNo.Enabled = true;
                ddlChequeNo.Enabled = true;
                blankBankCharges();
                lblPaymentNo.Visible = false;
                txtPaymentNo.Visible = false;
                txtChequeDate.Focus();
            }
            else if (ddlModeOfPayment.SelectedItem.Text == "DEMAND DRAFT")
            {
                if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")
                {
                    blnAdjustment.Enabled = true;
                }
                else
                {
                    blnAdjustment.Enabled = true;
                }

                trChequeRow.Visible = false;
                ddlChequeBookNo.Enabled = false;
                ddlChequeNo.Enabled = false;
                lblPaymentNo.Text = "Demand Draft Number :";
                lblPaymentNo.Visible = true;
                txtPaymentNo.Visible = true;
                txtChequeDate.Focus();
            }
            else if (ddlModeOfPayment.SelectedItem.Text == "PAY ORDER")
            {
                if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")
                {
                    blnAdjustment.Enabled = true;
                }
                else
                {
                    blnAdjustment.Enabled = true;
                }

                trChequeRow.Visible = false;
                ddlChequeBookNo.Enabled = false;
                ddlChequeNo.Enabled = false;
                lblPaymentNo.Text = "Pay Order Number :";
                lblPaymentNo.Visible = true;
                txtPaymentNo.Visible = true;
                txtChequeDate.Focus();
            }
            else
            {
                if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")
                {
                    blnAdjustment.Enabled = true;
                }
                else
                {
                    blnAdjustment.Enabled = true;
                }

                trChequeRow.Visible = false;
                ddlChequeBookNo.Enabled = false;
                ddlChequeNo.Enabled = false;
                lblPaymentNo.Text = "ECS Number :";
                lblPaymentNo.Visible = true;
                txtPaymentNo.Visible = true;
                txtChequeDate.Focus();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting ModeOfPayment Dropdown..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void blankBankCharges()     // Blank Bank Charges Controls
    {
        try
        {
            txtBankCharges.Text = string.Empty;
            txtBillNo.Text = string.Empty;
            txtAmtJV.Text = string.Empty;
            txtPayableTo.Text = string.Empty;
            txtImportNo.Text = string.Empty;
            txtChequeRemarks.Text = string.Empty;
            txtPayableAt.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void bindOnAccountLedgerCode()  // All Ledgers instead of select payment Ledger..
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                
                ddlLedgerCode.DataSource = dv;
                ddlLedgerCode.DataBind();
                ddlLedgerCode.Items.Insert(0, new ListItem("-------- Select Ledger ----------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindLedgerCode()       // Fill Ledgers for purchase approved vouchers
    {
        try
        {
            ddlLedgerCode.Items.Clear();

            oFA_CHEQUE_DTL = new SaitexDM.Common.DataModel.FA_CHEQUE_DTL();
            oFA_CHEQUE_DTL.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_CHEQUE_DTL.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_CHEQUE_DTL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUE_DTL.GetPurchaseApprovedJournalForBankPayment(oFA_CHEQUE_DTL);

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlLedgerCode.DataSource = dt;
                ddlLedgerCode.DataBind();
                ddlLedgerCode.Items.Insert(0, new ListItem("-------- Select Ledger ----------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlLedgerCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            blnAdjustment.Enabled = true;
            blnAdjustment.Focus();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Ledger Code..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    /// <summary>
    /// Open a pop-up window for Adjustment the bills, and send Ledger Code and TextboxID(For return amount).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void blnAdjustment_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlLedgerCode.SelectedIndex != 0)
            {
                if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")
                {
                    txtAmount.ReadOnly = false;
                    string URL = "AdjustAdvanceAdvice.aspx";
                    URL = URL + "?LedgerCode=" + ddlLedgerCode.SelectedValue.ToString().Trim();
                    URL = URL + "&TextBoxId=" + txtAmount.ClientID;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=600');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }
                else
                {
                    txtAmount.ReadOnly = false;
                    string URL = "AdjustPurchaseVoucher.aspx";
                    URL = URL + "?LedgerCode=" + ddlLedgerCode.SelectedValue.ToString().Trim();
                    URL = URL + "&TextBoxId=" + txtAmount.ClientID;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Ledger first.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ajust Button Click Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    /// <summary>
    /// Find Cash Ledgers only -- Group 16
    /// For Drop-Down filled.
    /// </summary>
    /// <param name="text">Entered Text</param>
    /// <param name="startOffset">Start Charactor</param>
    /// <param name="numberOfItems">Count No Of Items</param>
    /// <returns></returns>
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            // For Payment Group 16 Bank Only..
            string whereClause = " WHERE LDGR_CODE like :SearchQuery or LDGR_NAME like :SearchQuery";
            string sortExpression = " ORDER BY LDGR_CODE";
            string commandText = "select * from (select * from (SELECT * FROM FA_LGR_MST WHERE DEL_STATUS = '0') WHERE GRP_CODE = '16' ) ";
            string sPO = string.Empty;
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlPaymentLedger_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPaymentLedger.SelectedIndex != 0)
            {
                bindChequeBooks();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Payment Ledger Code..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindChequeBooks()       // Fill Cheque Book According to Bank Ledger
    {
        try
        {
            ddlChequeBookNo.Items.Clear();
            DataTable dt1 = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeBookMasterByBankLedgerCode(ddlPaymentLedger.SelectedValue.ToString());
            if (dt1 != null && dt1.Rows.Count > 0)
            { 

                ddlChequeBookNo.DataSource = dt1;
                ddlChequeBookNo.DataBind();
                ddlChequeBookNo.Items.Insert(0, new ListItem("-------- Select Cheque Book No --------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlChequeBookNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindChequeNumbers();
            ddlChequeNo.Focus();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting ChequeBook Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindChequeNumbers()    // Fill Cheque Numbers according to Cheque Book and Leafs
    {
        try
        {
            ddlChequeNo.Items.Clear();
            if (ViewState["dtChequeNo"] != null)
            {
                dtChequeNo = (DataTable)ViewState["dtChequeNo"];
                dtChequeNo = null;
            }
            dtChequeNo = new DataTable();
            dtChequeNo.Columns.Add("CHEQUE_NO", typeof(int));
            dtChequeNo.Rows.Clear();
            int iStartLeaf = 0;
            int iEndLeaf = 0;

            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeNoByChequeBookCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlChequeBookNo.SelectedValue.ToString().Trim());
            if (dt != null && dt.Rows.Count > 0)
            {
                dtChequeNo.Rows.Clear();
                iStartLeaf = int.Parse(dt.Rows[0]["START_LEAF"].ToString());
                iEndLeaf = int.Parse(dt.Rows[0]["END_LEAF"].ToString());
                for (int i = iStartLeaf; i <= iEndLeaf; i++)
                {
                    FillChequeNo(i);
                }

                ddlChequeNo.DataSource = dtChequeNo;
                ddlChequeNo.DataBind();
                dtChequeNo.Rows.Clear();
            }
            ViewState["dtChequeNo"] = dtChequeNo;
        }
        catch
        {
            throw;
        }
    }

    private void FillChequeNo(int iChequeNo)  // Fill Sub-Function Cheque Numbers according to Cheque Book and Leafs
    {
        try
        {
            if (ViewState["dtChequeNo"] != null)
                dtChequeNo = (DataTable)ViewState["dtChequeNo"];

            oFA_CHEQUE_DTL = new SaitexDM.Common.DataModel.FA_CHEQUE_DTL();
            oFA_CHEQUE_DTL.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_CHEQUE_DTL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_CHEQUE_DTL.CHEQUEBOOK_CODE = ddlChequeBookNo.SelectedValue.ToString();
            oFA_CHEQUE_DTL.CHEQUE_NO = iChequeNo;

            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetDuplicateChequeNo(oFA_CHEQUE_DTL);
            if (dt != null && dt.Rows.Count > 0)
            {
            }
            else
            {
                dtChequeNo.Rows.Add(iChequeNo);
            }
            ViewState["dtChequeNo"] = dtChequeNo;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlChequeNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            if (ddlChequeNo.Items.Count < 0)
            {
                Common.CommonFuction.ShowMessage("All Leafs are issued for this Cheque Book.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Cheque Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SaveJournalEntry();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    /// <summary>
    /// Saving Journal Entry Details through this..According to condition (Conditions are given below)
    /// </summary>
    private void SaveJournalEntry()
    {
        try
        {
            bool bOnAccount = false;
            bool bCheque = false;
            bool bBankCharges = false;
            bool bTDS = false;
            string strContractCode = string.Empty;
            string strVou_No = string.Empty;
            string strBankVou_No = string.Empty;
            string strTDSVou_No = string.Empty;
            double dblPaidAmt = 0;
            double dBankCharges = 0;

            if (ViewState["dtJournalDetail"] != null)
            {
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
                dtJournalDetail = null;
            }

            if (ViewState["dtBankChargesJournal"] != null)
            {
                dtBankChargesJournal = (DataTable)ViewState["dtBankChargesJournal"];
                dtBankChargesJournal = null;
            }

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString();

            if (ViewState["TDSJournalId"] != null)
                TDSJournalId = ViewState["TDSJournalId"].ToString();

            if (ViewState["dtAdvancedAdvice"] != null)
                dtAdvancedAdvice = (DataTable)ViewState["dtAdvancedAdvice"];

            if (ViewState["dtPartyPayment"] != null)
                dtPartyPayment = (DataTable)ViewState["dtPartyPayment"];

            if (CheckValidation())
            {
                ///////// All Class Object Initialisation Here.....
                oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
                oFA_PAYMENT_MODE = new SaitexDM.Common.DataModel.FA_PAYMENT_MODE();
                oFA_CHEQUE_DTL = new SaitexDM.Common.DataModel.FA_CHEQUE_DTL();
                oBankChargesJournal_Mst = new SaitexDM.Common.DataModel.FA_Journal_MST();
                oFA_TDS_DEDUCT = new SaitexDM.Common.DataModel.FA_TDS_DEDUCT();
                oTDSJournal_Mst = new SaitexDM.Common.DataModel.FA_Journal_MST();

                // Execute TDSJournalID..
                string strJournalVoucher = GenerateSecondVoucherNumber();
                // Boolean variable value assignment Here...
                if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")
                {
                    bOnAccount = true;
                }
                else
                {
                    bOnAccount = false;
                }

                if (ddlModeOfPayment.SelectedItem.Text == "CHEQUE")
                {
                    bCheque = true;
                }
                else
                {
                    bCheque = false;
                }

                if (txtBankCharges.Text != "")
                {
                    dBankCharges = double.Parse(txtBankCharges.Text);
                    if (dBankCharges > 0)
                    {
                        bBankCharges = true;
                    }
                    else
                    {
                        bBankCharges = false;
                    }
                }

                if (Session["dtTDS"] != null)
                {
                    dtTDSDeduction = (DataTable)Session["dtTDS"];
                    if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
                    {
                        bTDS = true;
                    }
                    else
                    {
                        bTDS = false;
                    }
                }

                // Create and Insert DataTable for Bank Charges
                if (txtBankCharges.Text != "")
                {
                    dBankCharges = double.Parse(txtBankCharges.Text);
                    if (dBankCharges > 0)
                    {
                        bBankCharges = true;
                        if (dtBankChargesJournal == null)
                            CreateBankChargesDataTable();

                        // For Debit Entry
                        DataRow dr = dtBankChargesJournal.NewRow();
                        dr["UNIQUE_ID"] = dtBankChargesJournal.Rows.Count + 1;
                        dr["ENTRY_TYPE"] = "Dr";
                        dr["LEDGER_CODE"] = "56";
                        dr["LEDGER_NAME"] = "BANK CHARGES";
                        dr["IS_DEBIT"] = true;
                        dr["AMOUNT"] = double.Parse(txtBankCharges.Text);
                        dr["DR_AMOUNT"] = double.Parse(txtBankCharges.Text);
                        dr["DESC"] = "BANK CHARGES TRANSACTION VOUCHER ENTRY " + System.DateTime.Now.ToShortDateString();
                        dtBankChargesJournal.Rows.Add(dr);

                        // For Credit Entry
                        DataRow dr1 = dtBankChargesJournal.NewRow();
                        dr1["UNIQUE_ID"] = dtBankChargesJournal.Rows.Count + 1;
                        dr1["ENTRY_TYPE"] = "Cr";
                        dr1["LEDGER_CODE"] = ddlPaymentLedger.SelectedValue.ToString().Trim();
                        dr1["LEDGER_NAME"] = ddlPaymentLedger.SelectedItem.Text.Trim();
                        dr1["IS_DEBIT"] = false;
                        dr1["AMOUNT"] = double.Parse(txtBankCharges.Text);
                        dr1["CR_AMOUNT"] = double.Parse(txtBankCharges.Text);
                        dr1["DESC"] = "BANK CHARGES TRANSACTION VOUCHER ENTRY " + System.DateTime.Now.ToShortDateString();
                        dtBankChargesJournal.Rows.Add(dr1);
                        ViewState["dtBankChargesJournal"] = dtBankChargesJournal;
                    }
                }
                // For Common Insertion Object Creation *******************
                if (dtJournalDetail == null)
                    CreateDataTable();

                // For Journal Master Entry
                oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                oFA_Journal_MST.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
                strVou_No = GenerateFirstVoucherNumber();
                oFA_Journal_MST.VOUCHER_NO = strVou_No;
                oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                oFA_Journal_MST.STATUS = true;

                saveDataTable();    // For Journal Transaction entry

                // For Payment Mode entry
                oFA_PAYMENT_MODE.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFA_PAYMENT_MODE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFA_PAYMENT_MODE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFA_PAYMENT_MODE.VCHR_NO = GenerateFirstVoucherNumber();
                oFA_PAYMENT_MODE.BANK_NAME = ddlPaymentLedger.SelectedItem.ToString().Trim().ToUpper();
                oFA_PAYMENT_MODE.BANK_LGR_CODE = ddlPaymentLedger.SelectedValue.ToString().Trim().ToUpper();
                oFA_PAYMENT_MODE.PARTY_LGR_CODE = ddlLedgerCode.SelectedValue.ToString().Trim().ToUpper();

                // Prevent for null value error..
                if (txtBankCharges.Text == "")
                {
                    oFA_PAYMENT_MODE.BANK_CHARGES = 0;
                    oFA_PAYMENT_MODE.AMOUNT_JV = double.Parse(txtAmountPayable.Text);
                }
                else
                {
                    oFA_PAYMENT_MODE.BANK_CHARGES = double.Parse(txtBankCharges.Text);
                    oFA_PAYMENT_MODE.AMOUNT_JV = double.Parse(txtAmtJV.Text);
                }

                oFA_PAYMENT_MODE.BANK_CHARGES_CODE = txtChargesCode.Text.Trim();
                oFA_PAYMENT_MODE.DOC_NO = txtVoucherNo.Text.Trim().ToUpper();
                oFA_PAYMENT_MODE.DOC_DT = DateTime.Parse(txtChequeDate.Text.Trim());
                oFA_PAYMENT_MODE.PAYABLE_TO = txtPayableTo.Text.Trim().ToUpper();
                oFA_PAYMENT_MODE.IMPORT_NO = txtImportNo.Text.Trim();
                oFA_PAYMENT_MODE.REMARKS = txtChequeRemarks.Text.Trim();
                oFA_PAYMENT_MODE.PAYABLE_AT = txtPayableAt.Text.Trim().ToUpper();
                oFA_PAYMENT_MODE.TUSER = oUserLoginDetail.UserCode;
                oFA_PAYMENT_MODE.STATUS = true;
                oFA_PAYMENT_MODE.MODE_OF_PAYMENT = ddlModeOfPayment.SelectedItem.Text.ToUpper().Trim();
                oFA_PAYMENT_MODE.PAYMENT_NO = txtPaymentNo.Text.ToUpper().Trim();
                oFA_PAYMENT_MODE.TYPE_OF_PAYMENT = ddlTypeOfPayment.SelectedItem.Text.ToUpper().Trim();
                
                if (ddlModeOfPayment.SelectedItem.Text == "CHEQUE")   // Condition for checking, Mode Of Payment -- Cheque
                {
                    bCheque = true;
                    // For Cheque Detail Entry
                    oFA_CHEQUE_DTL.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oFA_CHEQUE_DTL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oFA_CHEQUE_DTL.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oFA_CHEQUE_DTL.JOURNAL_ID = int.Parse(JournalId);
                    oFA_CHEQUE_DTL.VCHR_NO = GenerateFirstVoucherNumber();
                    oFA_CHEQUE_DTL.BANK_NAME = ddlPaymentLedger.SelectedValue.ToString().Trim();
                    oFA_CHEQUE_DTL.CHEQUEBOOK_CODE = ddlChequeBookNo.SelectedValue.ToString().Trim();
                    oFA_CHEQUE_DTL.CHEQUE_NO = int.Parse(ddlChequeNo.SelectedItem.Text.Trim());
                    oFA_CHEQUE_DTL.CHEQUE_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                    oFA_CHEQUE_DTL.PARTY_LGR_CODE = ddlLedgerCode.SelectedValue.ToString().Trim();
                    oFA_CHEQUE_DTL.AMOUNT = double.Parse(txtAmount.Text);
                    oFA_CHEQUE_DTL.IS_ISSUED = true;
                    oFA_CHEQUE_DTL.TUSER = oUserLoginDetail.UserCode;
                    oFA_CHEQUE_DTL.STATUS = true;
                    oFA_CHEQUE_DTL.ISSUED_BY = oUserLoginDetail.UserCode;

                    if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")   // Condition for checking, Type Of Payment -- On Account
                    {
                        bOnAccount = true;

                        //// Insertion For 1st Condition Simple Adjustment, without Bank Charges..
                        if (dtAdvancedAdvice == null)
                            CreateAdviceDT();

                        if (dtAdvancedAdvice != null)
                        {
                            dtAdvancedAdvice.Rows.Clear();
                        }

                        if (Session["dtAdjAdvice"] != null)
                        {
                            DataTable dtAdjAdvice = (DataTable)Session["dtAdjAdvice"];
                            if (dtAdjAdvice != null && dtAdjAdvice.Rows.Count > 0)
                            {
                                DataView dv1 = new DataView(dtAdjAdvice);
                                if (dv1.Count > 0)
                                {
                                    for (int iLoop = 0; iLoop < dv1.Count; iLoop++)
                                    {
                                        string Adv_No = dv1[iLoop]["ADV_NO"].ToString();
                                        DateTime Adv_DT = Convert.ToDateTime(dv1[iLoop]["ADV_DATE"].ToString());
                                        dblPaidAmt = double.Parse(dv1[iLoop]["ADV_AMT"].ToString());
                                        double dblTDS = 0;
                                        double dblTDS1 = 0;

                                        if (dv1[iLoop]["TDS_DEDUCT_AMT"].ToString() == null)
                                        {
                                            dblTDS = dblPaidAmt;
                                        }
                                        else
                                        {
                                            dblTDS1 = double.Parse(dv1[iLoop]["TDS_DEDUCT_AMT"].ToString());
                                            dblTDS = (dblPaidAmt - dblTDS1);
                                        }

                                        UpdateAdvice(Adv_No, Adv_DT, dblPaidAmt, dblTDS, dblTDS1);
                                    }
                                }
                            }
                        }
                        // For Bank Charges Voucher Entry.. 17 March 2011
                        // For Journal Master Entry -- Bank Charges Voucher Entry..
                        if (txtBankCharges.Text != "")
                        {
                            dBankCharges = double.Parse(txtBankCharges.Text);
                            if (dBankCharges > 0)
                            {
                                bBankCharges = true;
                                oBankChargesJournal_Mst.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                oBankChargesJournal_Mst.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                oBankChargesJournal_Mst.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                                oBankChargesJournal_Mst.JOURNAL_ID = int.Parse(TDSJournalId);
                                oBankChargesJournal_Mst.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oBankChargesJournal_Mst.VOUCHER_CODE = "6";
                                oBankChargesJournal_Mst.VOUCHER_NO = GenerateSecondVoucherNumber();
                                oBankChargesJournal_Mst.DESCRIPTION = "BANK CHARGES VOUCHER ENTRY FOR MASTER." + System.DateTime.Now.ToShortDateString();
                                oBankChargesJournal_Mst.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oBankChargesJournal_Mst.TUSER = oUserLoginDetail.UserCode;
                                oBankChargesJournal_Mst.STATUS = true;
                                strBankVou_No = GenerateSecondVoucherNumber();
                            }
                        }

                        if (Session["dtTDS"] != null)
                        {
                            dtTDSDeduction = (DataTable)Session["dtTDS"];
                        }

                        if (Session["ContractCode"] != null)
                        {
                            strContractCode = Session["ContractCode"].ToString();
                        }
                        if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
                        {
                            bTDS = true;
                            // For TDS Journal Entry..
                            oTDSJournal_Mst.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oTDSJournal_Mst.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oTDSJournal_Mst.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            strTDSVou_No = string.Empty;
                            if (bBankCharges)
                            {
                                int Bnk_Journal = int.Parse(TDSJournalId) + 1;
                                oTDSJournal_Mst.JOURNAL_ID = Bnk_Journal;
                                oTDSJournal_Mst.VOUCHER_NO = GenerateThirdVoucherNumber();
                                strTDSVou_No = GenerateThirdVoucherNumber();
                            }
                            else
                            {
                                int Bnk_JournalId = int.Parse(TDSJournalId);
                                oTDSJournal_Mst.JOURNAL_ID = Bnk_JournalId;
                                oTDSJournal_Mst.VOUCHER_NO = GenerateSecondVoucherNumber();
                                strTDSVou_No = GenerateSecondVoucherNumber();
                            }
                            oTDSJournal_Mst.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                            oTDSJournal_Mst.VOUCHER_CODE = "6";
                            oTDSJournal_Mst.DESCRIPTION = "TDS Deduction for " + ddlLedgerCode.SelectedItem.Text.Trim() + " Account";
                            oTDSJournal_Mst.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                            oTDSJournal_Mst.TUSER = oUserLoginDetail.UserCode;
                            oTDSJournal_Mst.STATUS = true;

                            // For TDS Deduction
                            oFA_TDS_DEDUCT.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_TDS_DEDUCT.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oFA_TDS_DEDUCT.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            oFA_TDS_DEDUCT.VCHR_CODE = "6";
                            oFA_TDS_DEDUCT.VCHR_NO = strTDSVou_No;
                            oFA_TDS_DEDUCT.TDS_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                            oFA_TDS_DEDUCT.REF_VCHR_CODE = VoucherCode;
                            oFA_TDS_DEDUCT.REF_VCHR_NO = GenerateFirstVoucherNumber();
                            oFA_TDS_DEDUCT.CONTRACT_CODE = strContractCode;
                            oFA_TDS_DEDUCT.PARTY_LDGR_CODE = ddlLedgerCode.SelectedValue.ToString().Trim();
                            oFA_TDS_DEDUCT.TUSER = oUserLoginDetail.UserCode;
                        }
                        else
                        {
                            bTDS = false;
                        }
                    }
                    else     // Condition for checking, Type Of Payment -- Bill wise
                    {
                        bOnAccount = false;
                        if (dtPartyPayment == null)
                            CreatePartyPaymentDT();

                        if (dtPartyPayment != null)
                        {
                            dtPartyPayment.Rows.Clear();
                        }

                        // For Bank Charges Voucher Entry.. 17 March 2011
                        // For Journal Master Entry -- Bank Charges Voucher Entry..
                        if (txtBankCharges.Text != "")
                        {
                            dBankCharges = double.Parse(txtBankCharges.Text);
                            if (dBankCharges > 0)
                            {
                                oBankChargesJournal_Mst.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                oBankChargesJournal_Mst.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                oBankChargesJournal_Mst.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                                oBankChargesJournal_Mst.JOURNAL_ID = int.Parse(TDSJournalId);
                                oBankChargesJournal_Mst.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oBankChargesJournal_Mst.VOUCHER_CODE = "6";
                                oBankChargesJournal_Mst.VOUCHER_NO = GenerateSecondVoucherNumber();
                                oBankChargesJournal_Mst.DESCRIPTION = "BANK CHARGES VOUCHER ENTRY FOR MASTER." + System.DateTime.Now.ToShortDateString();
                                oBankChargesJournal_Mst.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oBankChargesJournal_Mst.TUSER = oUserLoginDetail.UserCode;
                                oBankChargesJournal_Mst.STATUS = true;
                                strBankVou_No = GenerateSecondVoucherNumber();
                            }
                        }

                        if (Session["dtAdj"] != null)
                        {
                            DataTable dtAdj = (DataTable)Session["dtAdj"];
                            if (dtAdj != null && dtAdj.Rows.Count > 0)
                            {
                                DataView dv = new DataView(dtAdj);
                                if (dv.Count > 0)
                                {
                                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                                    {
                                        string Vou_No = dv[iLoop]["VCHR_NO"].ToString();
                                        dblPaidAmt = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                                        UpdatePaymentParty(Vou_No, dblPaidAmt);
                                    }
                                }
                            }
                        }
                    }
                }
                else // Condition for checking, Mode Of Payment -- Demand Draft OR Pay Order OR ECS
                {
                    bCheque = false;
                    if (ddlTypeOfPayment.SelectedItem.Text == "ON ACCOUNT")   // Condition for checking, Type Of Payment -- On Account
                    {
                        bOnAccount = true;

                        if (dtAdvancedAdvice == null)
                            CreateAdviceDT();

                        if (dtAdvancedAdvice != null)
                        {
                            dtAdvancedAdvice.Rows.Clear();
                        }

                        // For Bank Charges Voucher Entry..
                        // For Journal Master Entry -- Bank Charges Voucher Entry..
                        if (txtBankCharges.Text != "")
                        {
                            dBankCharges = double.Parse(txtBankCharges.Text);
                            if (dBankCharges > 0)
                            {
                                oBankChargesJournal_Mst.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                oBankChargesJournal_Mst.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                oBankChargesJournal_Mst.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                                oBankChargesJournal_Mst.JOURNAL_ID = int.Parse(TDSJournalId);
                                oBankChargesJournal_Mst.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oBankChargesJournal_Mst.VOUCHER_CODE = "6";
                                oBankChargesJournal_Mst.VOUCHER_NO = GenerateSecondVoucherNumber();
                                oBankChargesJournal_Mst.DESCRIPTION = "BANK CHARGES VOUCHER ENTRY FOR MASTER." + System.DateTime.Now.ToShortDateString();
                                oBankChargesJournal_Mst.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oBankChargesJournal_Mst.TUSER = oUserLoginDetail.UserCode;
                                oBankChargesJournal_Mst.STATUS = true;
                                strBankVou_No = GenerateSecondVoucherNumber();
                            }
                        }

                        DataTable dtAdjAdvice = (DataTable)Session["dtAdjAdvice"];
                        if (dtAdjAdvice != null && dtAdjAdvice.Rows.Count > 0)
                        {
                            DataView dv1 = new DataView(dtAdjAdvice);
                            if (dv1.Count > 0)
                            {
                                for (int iLoop = 0; iLoop < dv1.Count; iLoop++)
                                {
                                    string Adv_No = dv1[iLoop]["ADV_NO"].ToString();
                                    DateTime Adv_DT = Convert.ToDateTime(dv1[iLoop]["ADV_DATE"].ToString());
                                    dblPaidAmt = double.Parse(dv1[iLoop]["ADV_AMT"].ToString());
                                    double dblTDS = 0;
                                    double dblTDS1 = 0;
                                    if (dv1[iLoop]["TDS_DEDUCT_AMT"].ToString() == null)
                                    {
                                        dblTDS = dblPaidAmt;
                                    }
                                    else
                                    {
                                        dblTDS1 = double.Parse(dv1[iLoop]["TDS_DEDUCT_AMT"].ToString());
                                        dblTDS = (dblPaidAmt - dblTDS1);
                                    }

                                    UpdateAdvice(Adv_No, Adv_DT, dblPaidAmt, dblTDS, dblTDS1);
                                }
                            }
                        }

                        if (Session["dtTDS"] != null)
                        {
                            dtTDSDeduction = (DataTable)Session["dtTDS"];
                        }

                        if (Session["ContractCode"] != null)
                        {
                            strContractCode = Session["ContractCode"].ToString();
                        }

                        if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
                        {
                            // For TDS Journal Entry
                            oTDSJournal_Mst.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oTDSJournal_Mst.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oTDSJournal_Mst.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            strTDSVou_No = string.Empty;
                            if (bBankCharges)
                            {
                                int Bnk_Journal = int.Parse(TDSJournalId) + 1;
                                oTDSJournal_Mst.JOURNAL_ID = Bnk_Journal;
                                oTDSJournal_Mst.VOUCHER_NO = GenerateThirdVoucherNumber();
                                strTDSVou_No = GenerateThirdVoucherNumber();
                            }
                            else
                            {
                                oTDSJournal_Mst.JOURNAL_ID = int.Parse(TDSJournalId);
                                oTDSJournal_Mst.VOUCHER_NO = GenerateSecondVoucherNumber();
                                strTDSVou_No = GenerateSecondVoucherNumber();
                            }
                            oTDSJournal_Mst.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                            oTDSJournal_Mst.VOUCHER_CODE = "6";
                            oTDSJournal_Mst.DESCRIPTION = "TDS Deduction for " + ddlLedgerCode.SelectedItem.Text.Trim() + " Account";
                            oTDSJournal_Mst.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                            oTDSJournal_Mst.TUSER = oUserLoginDetail.UserCode;
                            oTDSJournal_Mst.STATUS = true;

                            // For TDS Deduction
                            oFA_TDS_DEDUCT.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_TDS_DEDUCT.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oFA_TDS_DEDUCT.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            oFA_TDS_DEDUCT.VCHR_CODE = "6";
                            oFA_TDS_DEDUCT.VCHR_NO = strTDSVou_No;
                            oFA_TDS_DEDUCT.TDS_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                            oFA_TDS_DEDUCT.REF_VCHR_CODE = VoucherCode;
                            oFA_TDS_DEDUCT.REF_VCHR_NO = txtVoucherNo.Text.Trim();
                            oFA_TDS_DEDUCT.CONTRACT_CODE = strContractCode;
                            oFA_TDS_DEDUCT.PARTY_LDGR_CODE = ddlLedgerCode.SelectedValue.ToString().Trim();
                            oFA_TDS_DEDUCT.TUSER = oUserLoginDetail.UserCode;
                        }
                    }
                    else     // Condition for checking, Type Of Payment -- Bill wise
                    {
                        bOnAccount = false;

                        if (dtPartyPayment == null)
                            CreatePartyPaymentDT();

                        if (dtPartyPayment != null)
                        {
                            dtPartyPayment.Rows.Clear();
                        }

                        // For Bank Charges Voucher Entry..
                        // For Journal Master Entry -- Bank Charges Voucher Entry..
                        if (txtBankCharges.Text != "")
                        {
                            dBankCharges = double.Parse(txtBankCharges.Text);
                            if (dBankCharges > 0)
                            {
                                oBankChargesJournal_Mst.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                oBankChargesJournal_Mst.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                oBankChargesJournal_Mst.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                                oBankChargesJournal_Mst.JOURNAL_ID = int.Parse(TDSJournalId);
                                oBankChargesJournal_Mst.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oBankChargesJournal_Mst.VOUCHER_CODE = "6";
                                oBankChargesJournal_Mst.VOUCHER_NO = GenerateSecondVoucherNumber();
                                oBankChargesJournal_Mst.DESCRIPTION = "BANK CHARGES VOUCHER ENTRY FOR MASTER." + System.DateTime.Now.ToShortDateString();
                                oBankChargesJournal_Mst.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oBankChargesJournal_Mst.TUSER = oUserLoginDetail.UserCode;
                                oBankChargesJournal_Mst.STATUS = true;
                                strBankVou_No = GenerateSecondVoucherNumber();
                            }
                        }

                        DataTable dtAdj = (DataTable)Session["dtAdj"];
                        if (dtAdj != null && dtAdj.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dtAdj);
                            if (dv.Count > 0)
                            {
                                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                                {
                                    string Vou_No = dv[iLoop]["VCHR_NO"].ToString();
                                    dblPaidAmt = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                                    UpdatePaymentParty(Vou_No, dblPaidAmt);
                                }
                            }
                        }
                    }
                }
                // For Insertion Conditions......
                int iRecordFound = 0;

                bool bResult = SaitexBL.Interface.Method.FA_Journal_DTL.InsertBankPayment(oFA_Journal_MST, dtJournalDetail, oFA_PAYMENT_MODE, oFA_CHEQUE_DTL, dtAdvancedAdvice, oBankChargesJournal_Mst, dtBankChargesJournal, oFA_TDS_DEDUCT, oTDSJournal_Mst, dtTDSDeduction, dtPartyPayment, bOnAccount, bCheque, bBankCharges, bTDS, out iRecordFound);
                if (bResult)
                {
                    string strMsg = string.Empty;
                    if (bOnAccount)  // On Account Message
                    {
                        if (bCheque)
                        {
                            if (bBankCharges == true && bTDS == false)
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment, Cheque Detail And Bank Charges Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No + "\\r\\n And Bank Charges Voucher Number is :" + strBankVou_No;
                            }
                            else if (bTDS == true && bBankCharges == false)
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment, Cheque Detail And TDS Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No + "\\r\\n And TDS Voucher Number is : " + strTDSVou_No;
                            }
                            else if (bBankCharges == true && bTDS == true)
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment, Cheque Detail, Bank Charges And TDS Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No + "\\r\\nAnd Bank Charges Voucher Number is : " + strBankVou_No + "\\r\\n And TDS Voucher Number is : " + strTDSVou_No;
                            }
                            else
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment And Cheque Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No;
                            }
                        }
                        else
                        {
                            if (bBankCharges == true && bTDS == false)
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment And Bank Charges Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No + "\\r\\n And Bank Charges Voucher Number is :" + strBankVou_No;
                            }
                            else if (bTDS == true && bBankCharges == false)
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment And TDS Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No + "\\r\\n And TDS Voucher Number is : " + strTDSVou_No;
                            }
                            else if (bBankCharges == true && bTDS == true)
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment, Bank Charges And TDS Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No + "\\r\\n Bank Charges Voucher Number is : " + strBankVou_No + "\\r\\n TDS Voucher Number is : " + strTDSVou_No;
                            }
                            else
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No;
                            }
                        }
                    }
                    else        // Bill Wise Message
                    {
                        if (bCheque)
                        {
                            if (bBankCharges)
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment, Cheque Detail And Bank Charges Details Saved Successfully!\\r\\nAnd Your Payment Voucher Number is : " + strVou_No + "\\r\\n And Bank Charges Voucher Number is :" + strBankVou_No;
                            }
                            else
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment And Cheque Details Saved Successfully!\\r\\n And Your Payment Voucher Number is : " + strVou_No;
                            }
                        }
                        else
                        {
                            if (bBankCharges)
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment And Bank Charges Details Saved Successfully!\\r\\n And Your Payment Voucher Number is : " + strVou_No + "\\r\\n And Bank Charges Voucher Number is :" + strBankVou_No;
                            }
                            else
                            {
                                strMsg = string.Empty;
                                strMsg = "Bank Payment Details Saved Successfully!\\r\\n And Your Payment Voucher Number is : " + strVou_No;
                            }
                        }
                    }

                    if (strMsg == "")
                    {

                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(strMsg);
                    }

                    if (ViewState["dtJournalDetail"] != null)
                    {
                        dtJournalDetail = null;
                    }
                    ViewState["dtJournalDetail"] = dtJournalDetail;

                    if (ViewState["JournalId"] != null)
                    {
                        JournalId = null;
                    }
                    ViewState["JournalId"] = JournalId;

                    if (Session["dtAdj"] != null)
                        Session["dtAdj"] = null;

                    if (Session["dtAdjAdvice"] != null)
                        Session["dtAdjAdvice"] = null;

                    if (Session["ContractCode"] != null)
                        Session["ContractCode"] = null;

                    InitialisePage();
                    blankBankCharges();
                    ddlLedgerCode.SelectedIndex = 0;
                    ddlPaymentLedger.SelectedIndex = 0;
                    ddlChequeBookNo.SelectedIndex = 0;
                    ddlChequeNo.SelectedIndex = 0;
                }
                else if (iRecordFound == 1)
                {
                    Common.CommonFuction.ShowMessage("This Payment Voucher is already saved.. Please enter another.");
                }
                else if (iRecordFound == 2)
                {
                    Common.CommonFuction.ShowMessage("This Payment Mode Of this Voucher is already saved.. Please enter another.");
                }
                else if (iRecordFound == 3)
                {
                    Common.CommonFuction.ShowMessage("This Cheque Detail is already saved.. Please enter another.");
                }
                else if (iRecordFound == 4)
                {
                    Common.CommonFuction.ShowMessage("The Bank Charges Voucher already exists.. Please enter another.");
                }
                else if (iRecordFound == 5)
                {
                    Common.CommonFuction.ShowMessage("The TDS Voucher already exists.. Please enter another.");
                }
                else if (iRecordFound == 6)
                {
                    Common.CommonFuction.ShowMessage("This TDS Deduction already exists.. Please enter another.");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Problem in Saving this voucher...");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please enter some required field denoted with(*) mark..");
            }
        }
        catch
        {
            throw;
        }
    }

    private string GenerateFirstVoucherNumber()
    {
        try
        {
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
            string Voucher_No = SaitexBL.Interface.Method.FA_Journal_DTL.GetVoucherNo(oFA_Journal_MST, out JournalId);
            ViewState["JournalId"] = JournalId;
            return Voucher_No;
        }
        catch
        {
            throw;
        }
    }

    private string GenerateSecondVoucherNumber()
    {
        try
        {
            string Voucher_No = string.Empty;

            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.VOUCHER_CODE = "6";  // For Journal Voucher

            DataTable dtVoucher = SaitexDL.Interface.Method.FA_VCHR_MST.GetVoucherMaster();
            DataView dv = new DataView(dtVoucher);
            dv.RowFilter = "VCHR_CODE='" + oFA_Journal_MST.VOUCHER_CODE + "'";
            if (dv.Count > 0)
            {
                TDSJournalId = SaitexBL.Interface.Method.FA_Journal_DTL.GetNewJournalId(oFA_Journal_MST);
                TDSJournalId = (int.Parse(TDSJournalId) + 1).ToString();
                ViewState["TDSJournalId"] = TDSJournalId;
                Voucher_No = oFA_Journal_MST.YEAR.ToString() + DateTime.Now.Date.Month.ToString() + dv[0]["VCHR_PREFIX"].ToString() + TDSJournalId + dv[0]["VCHR_SUFFIX"].ToString();
            }
            return Voucher_No;
        }
        catch
        {
            throw;
        }
    }

    private string GenerateThirdVoucherNumber()
    {
        try
        {
            string Voucher_No = string.Empty;

            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.VOUCHER_CODE = "6";  // For Journal Voucher

            DataTable dtVoucher = SaitexDL.Interface.Method.FA_VCHR_MST.GetVoucherMaster();
            DataView dv = new DataView(dtVoucher);
            dv.RowFilter = "VCHR_CODE='" + oFA_Journal_MST.VOUCHER_CODE + "'";
            if (dv.Count > 0)
            {
                TDSJournalId = SaitexBL.Interface.Method.FA_Journal_DTL.GetNewJournalId(oFA_Journal_MST);
                TDSJournalId = (int.Parse(TDSJournalId) + 2).ToString();
                ViewState["TDSJournalId"] = TDSJournalId;
                Voucher_No = oFA_Journal_MST.YEAR.ToString() + DateTime.Now.Date.Month.ToString() + dv[0]["VCHR_PREFIX"].ToString() + TDSJournalId + dv[0]["VCHR_SUFFIX"].ToString();
            }
            return Voucher_No;
        }
        catch
        {
            throw;
        }
    }

    private bool CheckValidation()
    {
        try
        {
            bool IsValidation = false;
            if (ddlTypeOfPayment.SelectedIndex != 0)
            {
                if (ddlModeOfPayment.SelectedIndex != 0)
                {
                    if (txtChequeDate.Text != "")
                    {
                        if (ddlLedgerCode.SelectedIndex != 0)
                        {
                            if (ddlPaymentLedger.SelectedIndex != 0)
                            {
                                IsValidation = true;
                            }
                            else
                            {
                                IsValidation = false;
                                Common.CommonFuction.ShowMessage("Dear! Please select Bank Ledger..");
                            }
                        }
                        else
                        {
                            IsValidation = false;
                            Common.CommonFuction.ShowMessage("Dear! Please select Debit Ledger Account..");
                        }
                    }
                    else
                    {
                        IsValidation = false;
                        Common.CommonFuction.ShowMessage("Dear! Please enter date of payment..");
                    }
                }
                else
                {
                    IsValidation = false;
                    Common.CommonFuction.ShowMessage("Dear! Please select Mode Of Payment..");
                }
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please select Type Of Payment..");
            }
            return IsValidation;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Find Cash Ledgers Counting -- Group 16
    /// </summary>
    /// <param name="text">Select charactor</param>
    /// <returns></returns>
    protected int GetItemsCount(string text)
    {
        try
        {
            // For Payment Group 16 Bank Only..
            string CommandText = "SELECT COUNT(*) FROM FA_LGR_MST WHERE GRP_CODE = '16' and LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_CODE = '16' and LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateJournalEntry();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
            txtVoucherNo.ReadOnly = true;
            txtVoucherNo.Text = string.Empty;
            ddlVoucherNo.Visible = true;
            txtVoucherNo.Visible = false;
            txtVoucherNo.AutoPostBack = true;
            RefreshDetailRow();
            BindVoucherCodeDDL();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Find..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindVoucherCodeDDL()
    {
        try
        {
            ddlVoucherNo.Items.Clear();
            DataTable dt = GetDataForVoucherno("","23");
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlVoucherNo.DataSource = dt;
                ddlVoucherNo.DataTextField = "VCHR_DTL";
                ddlVoucherNo.DataValueField = "VCHR_NO";
                ddlVoucherNo.DataBind();
                ddlVoucherNo.Items.Insert(0, new ListItem("---Voucher No---", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["dtTDS"] != null)
                Session["dtTDS"] = null;

            if (Session["dtAdj"] != null)
                Session["dtAdj"] = null;

            if (Session["dtAdjAdvice"] != null)
                Session["dtAdjAdvice"] = null;

            if (Session["ContractCode"] != null)
                Session["ContractCode"] = null;

            Response.Redirect("./PaymentThroughBank.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./BankPaymentOPT.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["dtTDS"] != null)
                Session["dtTDS"] = null;

            if (Session["dtAdj"] != null)
                Session["dtAdj"] = null;

            if (Session["dtAdjAdvice"] != null)
                Session["dtAdjAdvice"] = null;

            if (Session["ContractCode"] != null)
                Session["ContractCode"] = null;

            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtVoucherNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //FillEditDataByVoucherNo(txtVoucherNo.Text.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Voucher Number TextChanged Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    /// <summary>
    /// It clears rhe controls.
    /// </summary>
    private void BlankControls()
    {
        try
        {
            txtVoucherNo.Text = string.Empty;
            lblMode.Text = "You are in Save Mode";
            RefreshDetailRow();

            if (ViewState["dtJournalDetail"] != null)
            {
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
                dtJournalDetail = null;
            }
            ddlVoucherNo.Visible = false;
            txtVoucherNo.Visible = true;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// It refreshes the datatable rows.
    /// </summary>
    private void RefreshDetailRow()
    {
        try
        {
            ddlLedgerCode.SelectedIndex = 0;
            txtAmount.Text = "0";
            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// It calculates the Credit and Debit sites of Datatable.
    /// </summary>
    /// <param name="Debit_Total">Pass debit total</param>
    private void CalculateDebitCreditTotal(out double Debit_Total)
    {
        try
        {
            Debit_Total = 0;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            foreach (DataRow dr in dtJournalDetail.Rows)
            {
                double amt = 0;
                double.TryParse(dr["DR_AMOUNT"].ToString(), out amt);
                Debit_Total = Debit_Total + amt;
                amt = 0;
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// For editing Journal Transaction Rows..
    /// </summary>
    /// <param name="UNIQUEID">According to Unique ID, edit the datatable</param>
    private void EditJournalTRNRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            DataView dv = new DataView(dtJournalDetail);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                RefreshDetailRow();
                ddlLedgerCode.SelectedValue = dv[0]["LEDGER_CODE"].ToString();
                txtAmount.Text = dv[0]["DR_AMOUNT"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// For deleting the Journal transaction row from data table..
    /// </summary>
    /// <param name="UNIQUEID">By passing Unique ID</param>
    private void DeleteJournalTRNRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail.Rows.Count == 1)
            {
                dtJournalDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtJournalDetail.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
                        dtJournalDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtJournalDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            ViewState["dtJournalDetail"] = dtJournalDetail;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// It adds Credit Journal Entry for saving the Credit Bank Ledger
    /// </summary>
    private void addCreditJournalDetail()
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            double Amt = 0;
            string Desc = string.Empty;
            string spaces;
            string Entry_type = "By";
            if (dtJournalDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtJournalDetail.Rows)
                {
                    Amt = Amt + double.Parse(dr["AMOUNT"].ToString());
                    if (dtJournalDetail.Columns.Contains("DESCRIPTION"))
                    {
                        //Desc = dr["DESCRIPTION"].ToString();
                        dr["DESCRIPTION"] = txtTranDescription.Text;
                    }
                    else
                    {
                        dr["DESC"] = txtTranDescription.Text;
                        //Desc = dr["DESC"].ToString();
                    }
                }
            }
            //spaces = "&nbsp;&nbsp;&nbsp;";
            //DataRow dr1 = dtJournalDetail.NewRow();
            //dr1["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
            //dr1["ENTRY_TYPE"] = spaces + Entry_type;
            //dr1["LEDGER_CODE"] = ddlPaymentLedger.SelectedValue.ToString().Trim();
            //dr1["LEDGER_NAME"] = ddlPaymentLedger.SelectedItem.Text.Trim();
            //dr1["IS_DEBIT"] = false;
            //dr1["AMOUNT"] = Amt;
            //dr1["CR_AMOUNT"] = Amt;
            //dr1["DESC"] = Desc;
            //dtJournalDetail.Rows.Add(dr1);
            ViewState["dtJournalDetail"] = dtJournalDetail;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Fetch payment type data return from Journal transaction, Voucher No = 8 = Payment
    /// </summary>
    /// <param name="Text">Text entered by user</param>
    /// <returns>Only Payment type data</returns>
    private DataTable GetDataForVoucherno(string Text,string vourcherCode)
    {
        try
        {
            string whereClause = " WHERE VCHR_CODE = '" + vourcherCode + "'  And VCHR_NO like :SearchQuery OR VCHR_CODE = '" + vourcherCode + "' AND VCHR_NAME LIKE :SearchQuery";
            string sortExpression = " ORDER BY VCHR_NO";
            string commandText = "SELECT * FROM (SELECT * FROM V_FA_JOURNAL_MST WHERE COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') ASD";
            string sPO = string.Empty;
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", Text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private void UpdateJournalEntry()
    {
        try
        {
            double Debit_Total = 0;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString();

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                CalculateDebitCreditTotal(out Debit_Total);

                oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
                oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                oFA_Journal_MST.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
                oFA_Journal_MST.VOUCHER_NO = txtVoucherNo.Text.Trim();
                oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                oFA_Journal_MST.STATUS = true;
                addCreditJournalDetail();

                bool bResult = SaitexBL.Interface.Method.FA_Journal_DTL.Update(oFA_Journal_MST, dtJournalDetail);
                if (bResult)
                {
                    if (ViewState["dtJournalDetail"] != null)
                    {
                        dtJournalDetail = null;
                    }
                    ViewState["dtJournalDetail"] = dtJournalDetail;

                    if (ViewState["JournalId"] != null)
                    {
                        JournalId = null;
                    }
                    ViewState["JournalId"] = JournalId;

                    Common.CommonFuction.ShowMessage("Journal Entry Updated Successfully");
                    InitialisePage();
                    ddlLedgerCode.SelectedIndex = 0;
                    ddlPaymentLedger.SelectedIndex = 0;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Journal Entry updation failed.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please provide debit and credit entries.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlChequeNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Cheque Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void FillLedgers(string strLedgerCode)   // Fill Sub-Function Ledgers for purchase approved vouchers
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LDGR_CODE=" + strLedgerCode;
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        ddlLedgerCode.DataSource = dv;
                        ddlLedgerCode.DataBind();
                    }

                }
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Here, we create Datatable for Journal Transaction..
    /// </summary>
    private void CreateDataTable()
    {
        try
        {
            dtJournalDetail = new DataTable();
            dtJournalDetail.Columns.Add("UNIQUE_ID", typeof(int));
            dtJournalDetail.Columns.Add("ENTRY_TYPE", typeof(string));
            dtJournalDetail.Columns.Add("LEDGER_CODE", typeof(string));
            dtJournalDetail.Columns.Add("LEDGER_NAME", typeof(string));
            dtJournalDetail.Columns.Add("IS_DEBIT", typeof(bool));
            dtJournalDetail.Columns.Add("AMOUNT", typeof(double));
            dtJournalDetail.Columns.Add("DESC", typeof(string));
            dtJournalDetail.Columns.Add("DOC_NO", typeof(string));
            dtJournalDetail.Columns.Add("DOC_DT", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Here, we save the data for the journal transaction in Datatable..
    /// </summary>
    private void saveDataTable()
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            // Credit Entry
            bool IsDebit = false;
            string Entry_type = ddlCredit.SelectedItem.Text.Trim();
            string Doc_No = txtBillNo.Text.ToUpper().Trim();
            string Doc_Dt = txtChequeDate.Text.Trim();
            string Desc = txtTranDescription.Text.Trim();

            if (Entry_type == "Cr")    // For Bank Entry
                IsDebit = false;
            else
                IsDebit = true;

            if (dtJournalDetail == null)
                CreateDataTable();

            DataRow dr = dtJournalDetail.NewRow();
            dr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
            dr["ENTRY_TYPE"] = Entry_type;
            dr["LEDGER_CODE"] = ddlPaymentLedger.SelectedValue.ToString().Trim();
            dr["LEDGER_NAME"] = ddlPaymentLedger.SelectedItem.Text.Trim();
            dr["IS_DEBIT"] = IsDebit;
            dr["AMOUNT"] = double.Parse(txtAmount.Text);
            dr["DESC"] = Desc;
            dr["DOC_NO"] = Doc_No;
            dr["DOC_DT"] = Doc_Dt;
            dtJournalDetail.Rows.Add(dr);

            // Debit Entry      ------- For Purchase Party Amount

            IsDebit = false;
            Entry_type = ddlEntry_Type.SelectedItem.Text.Trim();

            if (Entry_type == "Dr")
                IsDebit = true;
            else
                IsDebit = false;

            dr = dtJournalDetail.NewRow();
            dr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
            dr["ENTRY_TYPE"] = Entry_type;
            dr["LEDGER_CODE"] = ddlLedgerCode.SelectedValue.ToString().Trim();
            dr["LEDGER_NAME"] = ddlLedgerCode.SelectedItem.Text.Trim();
            dr["IS_DEBIT"] = IsDebit;
            dr["AMOUNT"] = double.Parse(txtAmount.Text);
            dr["DESC"] = Desc;
            dr["DOC_NO"] = Doc_No;
            dr["DOC_DT"] = Doc_Dt;
            dtJournalDetail.Rows.Add(dr);

            ViewState["dtJournalDetail"] = dtJournalDetail;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// This function updates the journal transaction entry, according to paid amount..
    /// </summary>
    /// <param name="Vou_No">According to this Voucher Number</param>
    /// <param name="dblPaidAmt">Bill wise paid amount through that Adjustment Pop-up</param>
    private void UpdatePaymentParty(string Vou_No, double dblPaidAmt)
    {
        try
        {
            double dblAdjustAmt;
            dblAdjustAmt = 0;

            if (ViewState["dtPartyPayment"] != null)
                dtPartyPayment = (DataTable)ViewState["dtPartyPayment"];

            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUE_DTL.GetPurchaseAdjustJournal();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LEDGER_CODE='" + ddlLedgerCode.SelectedValue.ToString().Trim() + "' And VCHR_NO='" + Vou_No + "'";

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        DataRow dr = dtPartyPayment.NewRow();
                        dr["UNIQUE_ID"] = dtPartyPayment.Rows.Count + 1;
                        dr["COMP_CODE"] = dv[iLoop]["COMP_CODE"].ToString();
                        dr["BRANCH_CODE"] = dv[iLoop]["BRANCH_CODE"].ToString();
                        dr["YEAR"] = int.Parse(dv[iLoop]["YEAR"].ToString());
                        dr["VCHR_NO"] = GenerateFirstVoucherNumber();
                        dr["VCHR_DT"] = txtJournalDate.Text.Trim();
                        dr["PUR_VCHR_NO"] = dv[iLoop]["VCHR_NO"].ToString();
                        dr["PUR_VCHR_DT"] = dv[iLoop]["JOURNAL_DATE"].ToString().Trim();
                        dr["LEDGER_CODE"] = dv[iLoop]["LEDGER_CODE"].ToString();
                        dr["REF_DOC_NO"] = txtBillNo.Text.Trim();
                        dr["REF_DOC_DT"] = txtChequeDate.Text.Trim();
                        dblAdjustAmt = double.Parse(dv[iLoop]["ADJ_AMT"].ToString());
                        dr["ADJ_AMT"] = dblPaidAmt;
                        dr["ADJ_PREV_AMT"] = (dblAdjustAmt + dblPaidAmt);
                        dr["DESCRIPTION"] = txtTranDescription.Text;
                        dr["DOC_NO"] = txtBillNo.Text;
                        dr["DOC_DT"] = txtChequeDate.Text;
                        dr["TUSER"] = oUserLoginDetail.UserCode;
                        dr["DEL_STATUS"] = false;

                        dtPartyPayment.Rows.Add(dr);
                    }
                }
            }
            ViewState["dtPartyPayment"] = dtPartyPayment;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Here, we create Datatable for Party Updation..
    /// </summary>
    private void CreatePartyPaymentDT()
    {
        try
        {
            dtPartyPayment = new DataTable();
            dtPartyPayment.Columns.Add("UNIQUE_ID", typeof(int));
            dtPartyPayment.Columns.Add("COMP_CODE", typeof(string));
            dtPartyPayment.Columns.Add("BRANCH_CODE", typeof(string));
            dtPartyPayment.Columns.Add("YEAR", typeof(int));
            dtPartyPayment.Columns.Add("VCHR_NO", typeof(string));
            dtPartyPayment.Columns.Add("VCHR_DT", typeof(string));
            dtPartyPayment.Columns.Add("PUR_VCHR_NO", typeof(string));
            dtPartyPayment.Columns.Add("PUR_VCHR_DT", typeof(string));
            dtPartyPayment.Columns.Add("LEDGER_CODE", typeof(string));
            dtPartyPayment.Columns.Add("REF_DOC_NO", typeof(string));
            dtPartyPayment.Columns.Add("REF_DOC_DT", typeof(string));
            dtPartyPayment.Columns.Add("ADJ_AMT", typeof(double));
            dtPartyPayment.Columns.Add("ADJ_PREV_AMT", typeof(double));
            dtPartyPayment.Columns.Add("DESCRIPTION", typeof(string));
            dtPartyPayment.Columns.Add("DOC_NO", typeof(string));
            dtPartyPayment.Columns.Add("DOC_DT", typeof(string));
            dtPartyPayment.Columns.Add("TUSER", typeof(string));
            dtPartyPayment.Columns.Add("DEL_STATUS", typeof(bool));
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// It fills the Data, according to Voucher number selection
    /// </summary>
    /// <param name="Voucher_No">Selected Voucher Number</param>
    private void FillEditDataByVoucherNo(string Voucher_No)
    {
        string strMODE_OF_PAYMENT = string.Empty;
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = Voucher_No;

            // To fetch the Payment Mode Details, according to Voucher number..
            DataTable dtPaymentMode = SaitexBL.Interface.Method.FA_PAYMENT_MODE.SelectPaymentModeByVoucherNo(oFA_Journal_MST);

            if (dtPaymentMode != null && dtPaymentMode.Rows.Count > 0)
            {
                ddlTypeOfPayment.Visible = false;
                ddlModeOfPayment.Visible = false;
                txtTypeOfPayment.Visible = true;
                txtModeOfPayment.Visible = true;

                txtTypeOfPayment.Text = dtPaymentMode.Rows[0]["TYPE_OF_PAYMENT"].ToString();
                txtModeOfPayment.Text = dtPaymentMode.Rows[0]["MODE_OF_PAYMENT"].ToString();
                strMODE_OF_PAYMENT = dtPaymentMode.Rows[0]["MODE_OF_PAYMENT"].ToString();
                txtChequeDate.Text = dtPaymentMode.Rows[0]["DOC_DT"].ToString();

                if (strMODE_OF_PAYMENT == "CHEQUE")
                {
                    ddlChequeBookNo.Visible = true;
                    lblChequeBookNo.Visible = true;
                    ddlChequeNo.Visible = true;
                    lblChequeNo.Visible = true;
                    lblPaymentNo.Visible = false;
                    txtPaymentNo.Visible = false;
                    txtPaymentNo.Text = string.Empty;
                }
                else
                {
                    txtPaymentNo.Text = dtPaymentMode.Rows[0]["PAYMENT_NO"].ToString();
                    ddlChequeBookNo.Visible = false;
                    lblChequeBookNo.Visible = false;
                    ddlChequeNo.Visible = false;
                    lblChequeNo.Visible = false;
                    lblPaymentNo.Visible = true;
                    txtPaymentNo.Visible = true;
                }

                txtBankCharges.Text = dtPaymentMode.Rows[0]["BANK_CHARGES"].ToString();
                txtPayableTo.Text = dtPaymentMode.Rows[0]["PAYABLE_TO"].ToString();
                txtImportNo.Text = dtPaymentMode.Rows[0]["IMPORT_NO"].ToString();
                txtBillNo.Text = dtPaymentMode.Rows[0]["DOC_NO"].ToString();
                txtAmtJV.Text = dtPaymentMode.Rows[0]["AMOUNT_JV"].ToString();
                txtPayableAt.Text = dtPaymentMode.Rows[0]["PAYABLE_AT"].ToString();
                txtChequeRemarks.Text = dtPaymentMode.Rows[0]["REMARKS"].ToString();
            }

            // Fill controls, according to voucher number.
            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectByVoucherNo(oFA_Journal_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
              
                txtPaymentVoucher.Text = dt.Rows[0]["VCHR_NAME"].ToString();
                txtJournalDate.Text = dt.Rows[0]["JOURNAL_DATE"].ToString();
                txtDescription.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                txtVoucherNo.Text = Voucher_No;
                JournalId = dt.Rows[0]["JOURNAL_ID"].ToString();
                ViewState["JournalId"] = JournalId;
                FillEditTRNDataByVoucherNo(Voucher_No, oFA_Journal_MST);
            }
            else
            {
                if (ViewState["dtJournalDetail"] != null)
                {
                    dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
                    dtJournalDetail = null;
                }
                RefreshDetailRow();
                txtVoucherNo.Text = string.Empty;
                txtDescription.Text = string.Empty;
                Common.CommonFuction.ShowMessage("Invalid Voucher No");
            }
            txtAmountPayable.Text = txtAmount.Text;
        }
        catch
        {
            throw;
        }
    }
   

    /// <summary>
    /// It fills the journal transaction data, according to Voucher number selection
    /// </summary>
    /// <param name="Voucher_No">Selected Voucher Number</param>
    /// <param name="oFA_Journal_MST">Journal master object for parameter values to fetch the journal transaction.</param>
    private void FillEditTRNDataByVoucherNo(string Voucher_No, SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST)
    {
        try
        {
            string strBankLedger = string.Empty;
            BindLedgersForEdit();  // For Search and Update fill the Ledger, according to Textboxes because Dropdown replace with textboxes.
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = Voucher_No;
            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectTRNByVoucherNo(oFA_Journal_MST);
            ViewState["dtJournalDetail"] = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "IS_DEBIT=1";

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        //ddlLedgerCode.SelectedValue = dv[iLoop]["LEDGER_CODE"].ToString();
                        ddlLedgerCode.SelectedIndex = ddlLedgerCode.Items.IndexOf(ddlLedgerCode.Items.FindByValue(dv[iLoop]["LEDGER_CODE"].ToString()));
                        txtAmount.Text = dv[iLoop]["AMOUNT"].ToString();
                    }
                }
            }

            DataTable dt1 = SaitexBL.Interface.Method.FA_Journal_DTL.SelectTRNByVoucherNo(oFA_Journal_MST);

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                DataView dv1 = new DataView(dt1);
                dv1.RowFilter = "IS_DEBIT=0";

                if (dv1.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv1.Count; iLoop++)
                    {
                        strBankLedger = dv1[iLoop]["LEDGER_CODE"].ToString();
                        bindCreditForUpdate(Voucher_No, strBankLedger);
                        txtTranDescription.Text = dv1[iLoop]["DESCRIPTION"].ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Fill the Credit details for Update
    /// </summary>
    /// <param name="Voucher_No">Voucher Number</param>
    /// <param name="strBankLedger">Bank Ledger</param>
    private void bindCreditForUpdate(string Voucher_No, string strBankLedger)
    {
        try
        {
            ddlPaymentLedger.SelectedValue = strBankLedger;

            bindChequeBooks();
           

            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUE_DTL.SelectCheques();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "VCHR_NO='" + Voucher_No + "'";

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        ddlChequeBookNo.SelectedValue = dv[iLoop]["CHEQUEBOOK_CODE"].ToString();
                        bindChequeNumbersForUpdate();
                        ddlChequeNo.SelectedValue = dv[iLoop]["CHEQUE_NO"].ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindChequeNumbersForUpdate()      // Fill Cheque No for update and find..
    {
        try
        {
            ddlChequeNo.Items.Clear();
            if (ViewState["dtChequeNo"] != null)
            {
                dtChequeNo = (DataTable)ViewState["dtChequeNo"];
                dtChequeNo = null;
            }
            dtChequeNo = new DataTable();
            dtChequeNo.Columns.Add("CHEQUE_NO", typeof(int));
            dtChequeNo.Rows.Clear();
            int iStartLeaf = 0;
            int iEndLeaf = 0;

            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeBookMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "CHEQUEBOOK_CODE='" + ddlChequeBookNo.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        iStartLeaf = int.Parse(dv[iLoop]["START_LEAF"].ToString());
                        iEndLeaf = int.Parse(dv[iLoop]["END_LEAF"].ToString());
                    }
                }
            }

            for (int i = iStartLeaf; i <= iEndLeaf; i++)
            {
                FillChequeNoForUpdate(i);
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillChequeNoForUpdate(int iChequeNo)    // Fill Sub-Function Cheque No for update and find..
    {
        try
        {
            if (ViewState["dtChequeNo"] != null)
                dtChequeNo = (DataTable)ViewState["dtChequeNo"];

            oFA_CHEQUE_DTL = new SaitexDM.Common.DataModel.FA_CHEQUE_DTL();
            oFA_CHEQUE_DTL.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_CHEQUE_DTL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_CHEQUE_DTL.CHEQUEBOOK_CODE = ddlChequeBookNo.SelectedValue.ToString();
            oFA_CHEQUE_DTL.CHEQUE_NO = iChequeNo;

            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetDuplicateChequeNo(oFA_CHEQUE_DTL);
            if (dt != null && dt.Rows.Count > 0)
            {
                dtChequeNo.Rows.Add(iChequeNo);
                ddlChequeNo.DataSource = dtChequeNo;
                ddlChequeNo.DataBind();
            }
            else
            {

            }
            dtChequeNo.Rows.Clear();
            ViewState["dtChequeNo"] = dtChequeNo;
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void txtBankCharges_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtAmount.Text == "" || txtBankCharges.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Please enter Amount and Bank Charges.');", true);
            }
            else
            {
                txtAmtJV.Text = (double.Parse(txtAmount.Text) + (double.Parse(txtBankCharges.Text))).ToString();
                txtAmtJV.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in BankChanges TextChanged Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    /// <summary>
    /// For Search and Update fill the Ledger, according to Textboxes because Dropdown replace with textboxes.
    /// </summary>
    private void BindLedgersForEdit()
    {
        try
        {
            if (txtTypeOfPayment.Text == "ON ACCOUNT")
            {
                blnAdjustment.Enabled = true;
                txtAmtJV.ReadOnly = true;
                bindOnAccountLedgerCode();
            }
            else
            {
                bindLedgerCode();
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// This function updates the Advanced Advice entry, according to paid amount..
    /// </summary>
    /// <param name="Vou_No">According to this Advice Number</param>
    /// <param name="dblPaidAmt">On Account paid amount through that Adjustment Pop-up</param>
    private void UpdateAdvice(string Adv_No, DateTime Adv_DT, double dblPaidAmt, double dblTDS, double dblTDS1)
    {
        try
        {
            if (ViewState["dtAdvancedAdvice"] != null)
                dtAdvancedAdvice = (DataTable)ViewState["dtAdvancedAdvice"];

            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();
            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.GetPendingAdvice(oFA_ADVANCED_ADVICE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "ADV_NO='" + Adv_No + "'";

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        DataRow dr = dtAdvancedAdvice.NewRow();
                        dr["UNIQUE_ID"] = dtAdvancedAdvice.Rows.Count + 1;
                        dr["COMP_CODE"] = dv[iLoop]["COMP_CODE"].ToString();
                        dr["BRANCH_CODE"] = dv[iLoop]["BRANCH_CODE"].ToString();
                        dr["YEAR"] = int.Parse(dv[iLoop]["YEAR"].ToString());
                        dr["ADV_NO"] = dv[iLoop]["ADV_NO"].ToString();
                        dr["ADV_DATE"] = dv[iLoop]["ADV_DATE"].ToString();
                        dr["ADV_FLAG"] = true;
                        dr["ADV_CLEARED_BY"] = oUserLoginDetail.Username;
                        dr["ADV_CLEARED_DT"] = System.DateTime.Now.Date;
                        dr["VCHR_NO"] = GenerateFirstVoucherNumber();
                        dr["TRN_DATE"] = txtJournalDate.Text.Trim();
                        dr["REF_DOC_NO"] = txtBillNo.Text.ToUpper().Trim();
                        dr["REF_DOC_DT"] = txtChequeDate.Text.Trim();
                        dr["ADJ_AMT"] = (dblTDS + dblTDS1);

                        dtAdvancedAdvice.Rows.Add(dr);
                    }
                }
            }
            ViewState["dtAdvancedAdvice"] = dtAdvancedAdvice;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Here, we create Datatable for Advice Updation..
    /// </summary>
    private void CreateAdviceDT()
    {
        try
        {
            dtAdvancedAdvice = new DataTable();
            dtAdvancedAdvice.Columns.Add("UNIQUE_ID", typeof(int));
            dtAdvancedAdvice.Columns.Add("COMP_CODE", typeof(string));
            dtAdvancedAdvice.Columns.Add("BRANCH_CODE", typeof(string));
            dtAdvancedAdvice.Columns.Add("YEAR", typeof(int));
            dtAdvancedAdvice.Columns.Add("ADV_NO", typeof(string));
            dtAdvancedAdvice.Columns.Add("ADV_DATE", typeof(string));
            dtAdvancedAdvice.Columns.Add("ADV_FLAG", typeof(bool));
            dtAdvancedAdvice.Columns.Add("ADV_CLEARED_BY", typeof(string));
            dtAdvancedAdvice.Columns.Add("ADV_CLEARED_DT", typeof(DateTime));
            dtAdvancedAdvice.Columns.Add("VCHR_NO", typeof(string));
            dtAdvancedAdvice.Columns.Add("TRN_DATE", typeof(string));
            dtAdvancedAdvice.Columns.Add("REF_DOC_NO", typeof(string));
            dtAdvancedAdvice.Columns.Add("REF_DOC_DT", typeof(string));
            dtAdvancedAdvice.Columns.Add("ADJ_AMT", typeof(double));
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
            txtAmount.ReadOnly = true;
            txtAmountPayable.Text = txtAmount.Text;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Amount TextChanged Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    /// <summary>
    /// Create Datatable for Bank Charges Voucher Entry..
    /// </summary>
    private void CreateBankChargesDataTable()
    {
        try
        {
            dtBankChargesJournal = new DataTable();
            dtBankChargesJournal.Columns.Add("UNIQUE_ID", typeof(int));
            dtBankChargesJournal.Columns.Add("ENTRY_TYPE", typeof(string));
            dtBankChargesJournal.Columns.Add("LEDGER_CODE", typeof(string));
            dtBankChargesJournal.Columns.Add("LEDGER_NAME", typeof(string));
            dtBankChargesJournal.Columns.Add("IS_DEBIT", typeof(bool));
            dtBankChargesJournal.Columns.Add("AMOUNT", typeof(double));
            dtBankChargesJournal.Columns.Add("DR_AMOUNT", typeof(double));
            dtBankChargesJournal.Columns.Add("CR_AMOUNT", typeof(double));
            dtBankChargesJournal.Columns.Add("DESC", typeof(string));
            dtBankChargesJournal.Columns.Add("DOC_NO", typeof(string));
            dtBankChargesJournal.Columns.Add("DOC_DT", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    protected void ddlVoucherNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillEditDataByVoucherNo(ddlVoucherNo.SelectedValue.ToString().Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Voucher Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}