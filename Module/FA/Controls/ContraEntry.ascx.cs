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

public partial class Module_FA_Controls_ContraEntry : System.Web.UI.UserControl
{
    public string JournalId = string.Empty;
    private DataTable dtJournalDetail = null;
    private DataTable dtChequeNo = null;
    public string VoucherCode = "1";     // For Contra Voucher Fixed
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST;
    SaitexDM.Common.DataModel.FA_CHEQUE_DTL oFA_CHEQUE_DTL;

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

    private void InitialisePage()
    {
        try
        {
            BlankControls();
            txtVoucherNo.Text = GetVoucherNo();
            if (txtJournalDate.Text == "")
                txtJournalDate.Text = System.DateTime.Now.Date.ToShortDateString();

            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;

            txtVoucherNo.AutoPostBack = false;

            trCheque.Visible = false;
            ddlVoucherType.ReadOnly = false;
            txtVoucherNo.ReadOnly = true;
            txtVoucherNo.Text = string.Empty;

            RefreshDetailRow();
            bindContraVoucher();
            ddlVoucherType.ReadOnly = true;
            BindLedgers();
        }
        catch
        {
            throw;
        }
    }

    private string GetVoucherNo()
    {
        try
        {
            return string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void BlankControls()
    {
        try
        {
            txtVoucherNo.Text = string.Empty;
            lblMode.Text = "You are in Save Mode";
            RefreshDetailRow();

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                dtJournalDetail.Rows.Clear();
                grdJourenaldetails.DataSource = dtJournalDetail;
                grdJourenaldetails.DataBind();
            }

            ddlVoucherNo.Visible = false;
            txtVoucherNo.Visible = true;
            BindGridFromTable();
        }
        catch
        {
            throw;
        }
    }

    private void BindLedgers()
    {
        try
        {
            ddlLedgerCode.Items.Clear();
            DataTable data = new DataTable();
            data = GetItems("", 0, 10, 2);
            if (data != null && data.Rows.Count > 0)
            {
                ddlLedgerCode.DataSource = data;
                ddlLedgerCode.DataTextField = "LDGR_NAME";
                ddlLedgerCode.DataValueField = "LDGR_CODE";
                ddlLedgerCode.DataBind();
                ddlLedgerCode.Items.Insert(0, new ListItem("-------- Select Ledger Name -------", "0"));
            }
            else
            {
                Common.CommonFuction.ShowMessage("There is no Ledger Code found in the DataBase..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems, int icond)
    {
        try
        {
            if (icond == 1)        // For Group Code
            {
                string whereClause = " WHERE GRP_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_NAME like :SearchQuery And DEL_STATUS = '0'";
                string sortExpression = " ORDER BY GRP_CODE";
                string commandText = "SELECT * FROM FA_Grp_Mst";
                string sPO = "";
                DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
                return dt;
            }
            else if (icond == 2)             // For Ledger Code
            {
                string whereClause = " WHERE LDGR_CODE like :SearchQuery or LDGR_NAME like :SearchQuery";
                string sortExpression = " ORDER BY LDGR_CODE";
                string commandText = "select * from (select * from (SELECT * FROM FA_LGR_MST WHERE DEL_STATUS = '0') WHERE GRP_CODE = '16' or GRP_CODE = '17' ) ";
                string sPO = "";
                DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
                return dt;
            }
            else if (icond == 3)          // For Ledger  Type
            {
                string whereClause = " WHERE MST_NAME = 'LDGR_TYPE' And DEL_STATUS = '0' And MST_CODE like :SearchQuery And DEL_STATUS = '0'";
                string sortExpression = " ORDER BY MST_CODE";
                string commandText = "SELECT * FROM TX_MASTER_TRN";
                string sPO = "";
                DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
                return dt;
            }
            else                       // For Ledger Group
            {
                string whereClause = " WHERE MST_NAME = 'LDGR_Group' And DEL_STATUS = '0' And MST_CODE like :SearchQuery And DEL_STATUS = '0'";
                string sortExpression = " ORDER BY MST_CODE";
                string commandText = "SELECT * FROM TX_MASTER_TRN";
                string sPO = "";
                DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
                return dt;
            }
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text, int icond)
    {
        try
        {
            if (icond == 1)        // For Group Code
            {
                string CommandText = "SELECT COUNT(*) FROM FA_Grp_Mst WHERE GRP_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_NAME like :SearchQuery And DEL_STATUS = '0'";
                return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
            }
            else if (icond == 2)             // For Ledger Code
            {
                string CommandText = "SELECT COUNT(*) FROM FA_LGR_MST WHERE GRP_CODE like '16' or GRP_CODE like '17' and LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_CODE like '16' or GRP_CODE like '17' and LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
                return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
            }
            else if (icond == 3)          // For Ledger  Type
            {
                string CommandText = "SELECT COUNT(*) FROM TX_MASTER_TRN WHERE MST_NAME = 'LDGR_TYPE' And DEL_STATUS = '0' And MST_NAME like :SearchQuery And DEL_STATUS = '0'";
                return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
            }
            else                       // For Ledger Group
            {
                string CommandText = "SELECT COUNT(*) FROM TX_MASTER_TRN WHERE MST_NAME = 'LDGR_Group' And DEL_STATUS = '0' And MST_NAME like :SearchQuery And DEL_STATUS = '0'";
                return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
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
            int unique_id = 0;
            if (ViewState["UNIQUE_ID"] != null)
                unique_id = int.Parse(ViewState["UNIQUE_ID"].ToString());

            if (!CheckDuplicateRow(ddlLedgerCode.SelectedValue.Trim(), unique_id))
            {
                if (ddlEntry_Type.SelectedValue.Trim() == "Dr")
                {
                    trCheque.Visible = false;
                    txtDebitAmount.ReadOnly = false;
                    txtDebitAmount.Text = string.Empty;
                    txtCreditAmount.ReadOnly = true;
                    txtDebitAmount.Focus();
                }
                else
                {
                    if (ddlLedgerCode.SelectedValue != "1")
                    {
                        trCheque.Visible = true;
                        BindChequeBooks();
                    }

                    txtDebitAmount.ReadOnly = true;
                    txtCreditAmount.ReadOnly = false;
                    txtCreditAmount.Text = string.Empty;
                    txtCreditAmount.Focus();
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Ledger already included");
                RefreshDetailRow();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Ledger Code..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindChequeBooks()       // Fill Cheque Book According to Bank Ledger
    {
        try
        {
            ddlChequeBookNo.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeBookMasterByBankLedgerCode(ddlLedgerCode.SelectedValue.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                trCheque.Visible = true;
                ddlChequeBookNo.DataSource = dt;
                ddlChequeBookNo.DataBind();
                ddlChequeBookNo.Items.Insert(0, new ListItem("---Cheque Book---", "0"));
            }
            else
            {
                Common.CommonFuction.ShowMessage("There is no Cheque Book found, related to this Bank..");
                trCheque.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }

    private bool CheckDuplicateRow(string Ledger_Code, int UniqueId)
    {
        try
        {
            bool IsDuplicate = false;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                DataView dv = new DataView(dtJournalDetail);
                dv.RowFilter = "LEDGER_CODE='" + Ledger_Code + "' and UNIQUE_ID<>" + UniqueId;
                if (dv.Count > 0)
                {
                    IsDuplicate = true;
                }
            }
            return IsDuplicate;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlLedgerCode.SelectedIndex > 0)
            {
                if (ViewState["dtJournalDetail"] != null)
                    dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

                if (dtJournalDetail == null)
                    CreateDataTable();

                string Entry_type = ddlEntry_Type.SelectedItem.Text.Trim();
                string Ledger_Code = ddlLedgerCode.SelectedValue.Trim();
                string Ledger_Name = ddlLedgerCode.SelectedItem.ToString().Trim();
                string Doc_No = txtDocNo.Text.ToUpper().Trim();
                string Doc_Dt = txtDocDT.Text.Trim();

                double Debit_Amount = 0;
                double.TryParse(txtDebitAmount.Text.Trim(), out Debit_Amount);

                double Credit_amount = 0;
                double.TryParse(txtCreditAmount.Text.Trim(), out Credit_amount);

                double Amount = 0;

                if (txtDocNo.Text != "" && txtDocDT.Text == "")
                {
                    Common.CommonFuction.ShowMessage("Please enter doc date.");
                }
                else
                {
                    if (Debit_Amount > 0 || Credit_amount > 0)
                    {
                        if (Debit_Amount > 0)
                            Amount = Debit_Amount;
                        else if (Credit_amount > 0)
                            Amount = Credit_amount;

                        if (Amount > 0)
                        {
                            bool IsDebit = false;
                            if (Entry_type == "Dr")
                                IsDebit = true;
                            else
                                IsDebit = false;

                            string Desc = txtTranDescription.Text.Trim();

                            int UNIQUE_ID = 0;
                            if (ViewState["UNIQUE_ID"] != null)
                                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

                            if (CheckDuplicateRow(Ledger_Code, UNIQUE_ID))
                            {
                                Common.CommonFuction.ShowMessage("Ledger already included");
                            }
                            else
                            {
                                if (UNIQUE_ID > 0)
                                {
                                    DataView dvEdit = new DataView(dtJournalDetail);
                                    dvEdit.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                                    if (dvEdit.Count > 0)
                                    {
                                        dvEdit[0]["ENTRY_TYPE"] = Entry_type;
                                        dvEdit[0]["LEDGER_CODE"] = Ledger_Code;
                                        dvEdit[0]["LEDGER_NAME"] = Ledger_Name;
                                        dvEdit[0]["IS_DEBIT"] = IsDebit;
                                        dvEdit[0]["AMOUNT"] = Amount;

                                        if (Debit_Amount > 0)
                                        {
                                            dvEdit[0]["DR_AMOUNT"] = Debit_Amount;
                                        }
                                        else
                                        {
                                            dvEdit[0]["CR_AMOUNT"] = Credit_amount;
                                            if (Ledger_Code != "1")
                                            {
                                                dvEdit[0]["CHEQUEBOOK_NO"] = ddlChequeBookNo.SelectedItem.ToString().Trim();
                                                dvEdit[0]["CHEQUEBOOK_CODE"] = ddlChequeBookNo.SelectedValue.ToString().Trim();
                                                dvEdit[0]["CHEQUE_NO"] = ddlChequeNo.SelectedItem.Text.Trim();
                                            }
                                        }
                                        dvEdit[0]["DESC"] = Desc;
                                        dvEdit[0]["DOC_NO"] = Doc_No;
                                        dvEdit[0]["DOC_DT"] = Doc_Dt;
                                        dtJournalDetail.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtJournalDetail.NewRow();
                                    dr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
                                    dr["ENTRY_TYPE"] = Entry_type;
                                    dr["LEDGER_CODE"] = Ledger_Code;
                                    dr["LEDGER_NAME"] = Ledger_Name;
                                    dr["IS_DEBIT"] = IsDebit;
                                    dr["AMOUNT"] = Amount;
                                    dr["DOC_NO"] = Doc_No;
                                    dr["DOC_DT"] = Doc_Dt;

                                    if (Debit_Amount > 0)
                                    {
                                        dr["DR_AMOUNT"] = Debit_Amount;
                                    }
                                    else
                                    {
                                        dr["CR_AMOUNT"] = Credit_amount;
                                        if (Ledger_Code != "1")
                                        {
                                            dr["CHEQUEBOOK_NO"] = ddlChequeBookNo.SelectedItem.ToString().Trim();
                                            dr["CHEQUEBOOK_CODE"] = ddlChequeBookNo.SelectedValue.ToString().Trim();
                                            dr["CHEQUE_NO"] = ddlChequeNo.SelectedItem.Text.Trim();
                                        }
                                    }
                                    dr["DESC"] = Desc;
                                    dtJournalDetail.Rows.Add(dr);
                                }
                                RefreshDetailRow();
                                ddlLedgerCode.SelectedIndex = 0;
                                ViewState["dtJournalDetail"] = dtJournalDetail;
                            }
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please enter amount first.");
                        if (txtCreditAmount.ReadOnly)
                        { txtDebitAmount.Focus(); txtDebitAmount.Text = ""; }
                        else
                        { txtCreditAmount.Focus(); txtCreditAmount.Text = ""; }
                    }
                }
                trCheque.Visible = false;
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Ledger first.");
            }
            BindGridFromTable();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Transaction..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

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
            dtJournalDetail.Columns.Add("DR_AMOUNT", typeof(double));
            dtJournalDetail.Columns.Add("CR_AMOUNT", typeof(double));
            dtJournalDetail.Columns.Add("DESC", typeof(string));
            dtJournalDetail.Columns.Add("DOC_NO", typeof(string));
            dtJournalDetail.Columns.Add("DOC_DT", typeof(string));
            dtJournalDetail.Columns.Add("CHEQUEBOOK_NO", typeof(string));
            dtJournalDetail.Columns.Add("CHEQUEBOOK_CODE", typeof(string));
            dtJournalDetail.Columns.Add("CHEQUE_NO", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            ddlEntry_Type.SelectedIndex = 0;
            txtTranDescription.Text = "";
            txtDocNo.Text = "";
            txtDocDT.Text = "";
            txtCreditAmount.Text = "0";
            txtDebitAmount.Text = "0";
            ViewState["UNIQUE_ID"] = null;
            ddlEntry_Type.Focus();
            txtDebitAmount.ReadOnly = (ddlEntry_Type.SelectedValue.Trim() == "Dr");
            txtCreditAmount.ReadOnly = (ddlEntry_Type.SelectedValue.Trim() == "Cr");
            SetEntryType();
        }
        catch
        {
            throw;
        }
    }

    private void BindGridFromTable()
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                DataView dv = new DataView(dtJournalDetail);
                dv.Sort = "ENTRY_TYPE ASC";
                dtJournalDetail.AcceptChanges();
            }

            grdJourenaldetails.DataSource = dtJournalDetail;
            grdJourenaldetails.DataBind();
            if (dtJournalDetail != null)
            {

                double Debit_Total = 0;
                double Credit_Total = 0;
                CalculateDebitCreditTotal(out Debit_Total, out Credit_Total);

                Label lblDr_Amount_ftr = (Label)grdJourenaldetails.FooterRow.FindControl("lblDr_Amount_ftr");
                Label lblCr_Amount_ftr = (Label)grdJourenaldetails.FooterRow.FindControl("lblCr_Amount_ftr");
                lblCr_Amount_ftr.Text = Credit_Total.ToString();
                lblDr_Amount_ftr.Text = Debit_Total.ToString();
            }
        }
        catch { throw; }
    }

    private void CalculateDebitCreditTotal(out double Debit_Total, out double Credit_Total)
    {
        try
        {
            Debit_Total = 0;
            Credit_Total = 0;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            foreach (DataRow dr in dtJournalDetail.Rows)
            {
                double amt = 0;
                double.TryParse(dr["DR_AMOUNT"].ToString(), out amt);
                Debit_Total = Debit_Total + amt;
                amt = 0;
                double.TryParse(dr["CR_AMOUNT"].ToString(), out amt);
                Credit_Total = Credit_Total + amt;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
            ddlLedgerCode.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel Button..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdJourenaldetails_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select..\r\nSee error log for detail."));
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

    private void SaveJournalEntry()
    {
        try
        {
            double Debit_Total = 0;
            double Credit_Total = 0;
            string vou_no = string.Empty;
            bool bCheque = false;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString().Trim();

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                CalculateDebitCreditTotal(out Debit_Total, out Credit_Total);
                if (Debit_Total == Credit_Total)
                {
                    if (txtJournalDate.Text != "")
                    {
                        oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
                        oFA_CHEQUE_DTL = new SaitexDM.Common.DataModel.FA_CHEQUE_DTL();

                        oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                        oFA_Journal_MST.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                        oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
                        oFA_Journal_MST.VOUCHER_NO = GenerateVoucherNumber();
                        oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                        oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                        oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                        oFA_Journal_MST.STATUS = true;
                        vou_no = GenerateVoucherNumber();

                        // For Cheque Detail
                        DataView dv = new DataView(dtJournalDetail);
                        dv.RowFilter = "CHEQUEBOOK_NO IS NOT NULL AND CHEQUE_NO IS NOT NULL";
                        if (dv.Count > 0)
                        {
                            bCheque = true;
                            oFA_CHEQUE_DTL.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_CHEQUE_DTL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oFA_CHEQUE_DTL.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            oFA_CHEQUE_DTL.JOURNAL_ID = int.Parse(JournalId);
                            oFA_CHEQUE_DTL.VCHR_NO = vou_no;
                            oFA_CHEQUE_DTL.BANK_NAME = dv[0]["LEDGER_CODE"].ToString().Trim();
                            oFA_CHEQUE_DTL.CHEQUEBOOK_CODE = dv[0]["CHEQUEBOOK_CODE"].ToString().Trim();
                            oFA_CHEQUE_DTL.CHEQUE_NO = int.Parse(dv[0]["CHEQUE_NO"].ToString().Trim());
                            oFA_CHEQUE_DTL.CHEQUE_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                            oFA_CHEQUE_DTL.PARTY_LGR_CODE = dv[0]["LEDGER_CODE"].ToString().Trim();
                            oFA_CHEQUE_DTL.AMOUNT = double.Parse(dv[0]["AMOUNT"].ToString().Trim());
                            oFA_CHEQUE_DTL.IS_ISSUED = true;
                            oFA_CHEQUE_DTL.TUSER = oUserLoginDetail.UserCode;
                            oFA_CHEQUE_DTL.ISSUED_BY = oUserLoginDetail.UserCode;
                        }
                        int iRecordFound = 0;
                        bool bResult = SaitexBL.Interface.Method.FA_Journal_DTL.InsertContraEntry(oFA_Journal_MST, dtJournalDetail, oFA_CHEQUE_DTL, bCheque, out iRecordFound);
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

                            Common.CommonFuction.ShowMessage(@"Contra Entry Saved Successfully! \n And Your Contra Voucher Number is: " + vou_no);
                            InitialisePage();
                            txtDescription.Text = string.Empty;
                            txtTranDescription.Text = string.Empty;
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Contra Entry Saving failed.");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please enter the Contra Date.");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Debit and Credit total should be same.");
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

    private string GenerateVoucherNumber()
    {
        try
        {
            SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
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

    private void UpdateJournalEntry()
    {
        try
        {
            double Debit_Total = 0;
            double Credit_Total = 0;

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString().Trim();

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                CalculateDebitCreditTotal(out Debit_Total, out Credit_Total);
                if (Debit_Total == Credit_Total)
                {
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

                        Common.CommonFuction.ShowMessage("Contra Entry Updated Successfully");
                        InitialisePage();
                        txtDescription.Text = string.Empty;
                        txtTranDescription.Text = string.Empty;
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Contra Entry updation failed.");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Debit and Credit total should be same.");
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
            ddlVoucherType.ReadOnly = true;
            txtVoucherNo.ReadOnly = false;
            txtVoucherNo.Text = string.Empty;
            ddlVoucherNo.Visible = true;
            txtVoucherNo.Visible = false;
            txtVoucherNo.AutoPostBack = true;
            RefreshDetailRow();
            ddlLedgerCode.SelectedIndex = 0;
            BindVoucherCodeDDL();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in find..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindVoucherCodeDDL()
    {
        try
        {
            ddlVoucherNo.Items.Clear();
            DataTable dt = GetDataForVoucherno(string.Empty);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlVoucherNo.DataSource = dt;
                ddlVoucherNo.DataTextField = "VCHR_NO";
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
            //InitialisePage();
            Response.Redirect("./ContraEntryForm.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
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
        }
        catch
        {
            throw;
        }
    }

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
                ddlLedgerCode.SelectedIndex = 0;

                if (dv[0]["ENTRY_TYPE"].ToString() == "Dr")
                {
                    txtDebitAmount.ReadOnly = false;
                    txtCreditAmount.ReadOnly = true;
                }
                else
                {
                    txtDebitAmount.ReadOnly = true;
                    txtCreditAmount.ReadOnly = false;
                }

                ddlEntry_Type.SelectedIndex = ddlEntry_Type.Items.IndexOf(ddlEntry_Type.Items.FindByValue(dv[0]["ENTRY_TYPE"].ToString()));
                ddlLedgerCode.SelectedValue = dv[0]["LEDGER_CODE"].ToString();
                txtDebitAmount.Text = dv[0]["DR_AMOUNT"].ToString();
                txtCreditAmount.Text = dv[0]["CR_AMOUNT"].ToString();
                txtDocNo.Text = dv[0]["DOC_NO"].ToString();
                txtDocDT.Text = dv[0]["DOC_DT"].ToString();
                txtTranDescription.Text = dv[0]["DESC"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void txtVoucherNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FillEditDataByVoucherNo(txtVoucherNo.Text.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Voucher Number TextChanged Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void FillEditDataByVoucherNo(string Voucher_No)
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = Voucher_No;
            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectByVoucherNo(oFA_Journal_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlVoucherType.Text = dt.Rows[0]["VCHR_NAME"].ToString();
                txtJournalDate.Text = dt.Rows[0]["JOURNAL_DATE"].ToString();
                txtDescription.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                txtVoucherNo.Text = Voucher_No;
                JournalId = dt.Rows[0]["JOURNAL_ID"].ToString();
                ViewState["JournalId"] = JournalId;

                DataTable dt1 = FillEditTRNDataByVoucherNo(Voucher_No, oFA_Journal_MST);
                MapDataTable(dt1);
                BindGridFromTable();
            }
            else
            {
                dtJournalDetail = null;
                grdJourenaldetails.DataSource = dtJournalDetail;
                grdJourenaldetails.DataBind();

                RefreshDetailRow();
                ddlLedgerCode.SelectedIndex = 0;
                txtVoucherNo.Text = string.Empty;
                txtDescription.Text = string.Empty;
                Common.CommonFuction.ShowMessage("Sorry Dear! this voucher has been comfirmed, you can not make any change.");
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable(DataTable dt)
    {
        try
        {
            string CHEQUEBOOK_NO = string.Empty;
            string CHEQUEBOOK_CODE = string.Empty;
            string CHEQUE_NO = string.Empty;

            DataTable dtChq = SaitexBL.Interface.Method.FA_CHEQUE_DTL.GetChequeDTLWithVoucher(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlVoucherNo.SelectedItem.Text.Trim());
            if (dtChq != null && dtChq.Rows.Count > 0)
            {
                CHEQUEBOOK_NO = dtChq.Rows[0]["CHEQUEBOOK_NO"].ToString();
                CHEQUEBOOK_CODE = dtChq.Rows[0]["CHEQUEBOOK_CODE"].ToString();
                CHEQUE_NO = dtChq.Rows[0]["CHEQUE_NO"].ToString();
            }

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail == null)
                CreateDataTable();

            dtJournalDetail.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drNew = dtJournalDetail.NewRow();
                drNew["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
                bool IsDebit = false;
                if (dr["IS_DEBIT"].ToString() == "1")
                {
                    IsDebit = true;
                }
                else
                {
                    drNew["CHEQUEBOOK_NO"] = CHEQUEBOOK_NO;
                    drNew["CHEQUEBOOK_CODE"] = CHEQUEBOOK_CODE;
                    drNew["CHEQUE_NO"] = CHEQUE_NO;
                }

                if (IsDebit)
                {
                    drNew["ENTRY_TYPE"] = "Dr";
                    drNew["DR_AMOUNT"] = dr["AMOUNT"];
                }
                else
                {
                    drNew["ENTRY_TYPE"] = "Cr";
                    drNew["CR_AMOUNT"] = dr["AMOUNT"];
                }
                drNew["LEDGER_CODE"] = dr["LEDGER_CODE"];
                drNew["LEDGER_NAME"] = dr["LDGR_NAME"];
                drNew["IS_DEBIT"] = IsDebit;
                drNew["AMOUNT"] = dr["AMOUNT"];
                drNew["DESC"] = dr["DESCRIPTION"];
                drNew["DOC_NO"] = dr["DOC_NO"];
                drNew["DOC_DT"] = dr["DOC_DT"];
                dtJournalDetail.Rows.Add(drNew);
            }
            ViewState["dtJournalDetail"] = dtJournalDetail;
        }
        catch
        {
            throw;
        }
    }

    private DataTable FillEditTRNDataByVoucherNo(string Voucher_No, SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST)
    {
        try
        {
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = Voucher_No;
            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectTRNByVoucherNo(oFA_Journal_MST);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetDataForVouchernO(string Text)
    {
        try
        {
            string whereClause = " WHERE VCHR_NO like :SearchQuery OR VCHR_CODE LIKE :SearchQuery OR VCHR_NAME LIKE :SearchQuery ";
            string sortExpression = " ORDER BY VCHR_NO";
            string commandText = "SELECT * FROM V_FA_JOURNAL_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", Text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private void bindContraVoucher()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "VCHR_NAME='" + "CONTRA" + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        ddlVoucherType.Text = dv[iLoop]["VCHR_NAME"].ToString();
                    }
                }
            }

            SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
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

    private DataTable GetDataForVoucherno(string Text)
    {
        try
        {
            string whereClause = " WHERE VCHR_CODE LIKE '1' And VCHR_NO like :SearchQuery OR VCHR_CODE LIKE '1' AND VCHR_NAME LIKE :SearchQuery";
            string sortExpression = " ORDER BY VCHR_NO";
            string commandText = "SELECT * FROM (SELECT * FROM V_FA_JOURNAL_MST WHERE COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') ASD";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", Text + '%', sPO);
            return dt;
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

    protected void ddlEntry_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetEntryType();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Entry Type..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void SetEntryType()
    {
        try
        {
            if (ddlEntry_Type.SelectedValue.Trim() == "Dr")
            {
                txtDebitAmount.ReadOnly = false;
                //txtDebitAmount.Text = string.Empty;
                txtCreditAmount.ReadOnly = true;
                txtCreditAmount.Text = string.Empty;
            }
            else
            {
                txtCreditAmount.ReadOnly = false;
                //txtCreditAmount.Text = string.Empty;
                txtDebitAmount.ReadOnly = true;
                txtDebitAmount.Text = string.Empty;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdJourenaldetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Unique_id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditTRN")
            {
                EditJournalTRNRow(Unique_id);
            }
            else if (e.CommandName == "DeleteTRN")
            {
                DeleteJournalTRNRow(Unique_id);
                BindGridFromTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid RowCommand Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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

    protected void ddlVoucherNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillEditDataByVoucherNo(ddlVoucherNo.SelectedItem.Text.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Voucher Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlChequeBookNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindChequeNumbers();
            ddlChequeNo.Focus();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting ChequeBook Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindChequeNumbers()    // Fill Cheque Numbers according to Cheque Book and Leafs
    {
        try
        {
            ddlChequeNo.Items.Clear();
            dtChequeNo = null;
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
        }
        catch
        {
            throw;
        }
    }

    protected void ddlChequeNo_SelectedIndexChanged(object sender, EventArgs e)
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

    //protected void btnLedger_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtLedgerPopUp.Visible = true;
    //        string URL = "LedgerMstPopUp.aspx";
    //        URL = URL + "?TextBoxId=" + txtLedgerPopUp.ClientID;
    //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=600');", true);
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclickLedger('" + URL + "')", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Open Ledger Window..\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //protected void txtLedgerPopUp_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtLedgerPopUp.Visible = false;
    //        BindLedgers();
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger Popup Text Changed Event..\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}
}