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

using Obout.ComboBox;
using Obout.Interface;
using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_PurchaseVoucher : System.Web.UI.UserControl
{
    public string VoucherCode = "10";     // For Purchase Voucher Fixed
    public string JournalId = string.Empty;
    public string strTDSJournalId = string.Empty;
    public static double dblBillAmt;
    public static string LedgerCode = string.Empty;
    public static string Branch = string.Empty;
    public static string BillType = string.Empty;
    public static int BillYear;
    public static string BillNumb;
    public static string BillDate = string.Empty;

    private DataTable dtJournalDetail, dtTDSDeduction, dtAdviceBill, dtJournalDetailTax;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_CONTRACT_MST oFA_CONTRACT_MST;
    SaitexDM.Common.DataModel.FA_TAX_MST oFA_TAX_MST;
    SaitexDM.Common.DataModel.FA_TDS_DEDUCT oFA_TDS_DEDUCT;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST, oFA_Journal_MST1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();

                if (Request.QueryString["VOUCHER_NO"] != null && Request.QueryString["VOUCHER_NO"].ToString() != "")
                {
                    string Voucher_No = Request.QueryString["VOUCHER_NO"].ToString();
                    SetEditDetailByVoucherNo(Voucher_No);
                }
               
                if (Request.QueryString["LedgerCode"] != null && Request.QueryString["LedgerCode"].ToString() != "" && Request.QueryString["BillAmt"] != null && Request.QueryString["BillAmt"].ToString() != "")
                {
                    InitialiseForPV();
                    dblBillAmt = 0;
                    LedgerCode = Request.QueryString["LedgerCode"].ToString().Trim();
                    double.TryParse(Request.QueryString["BillAmt"].ToString().Trim(), out dblBillAmt);
                    Branch = Request.QueryString["Branch"].ToString().Trim();
                    BillYear = int.Parse(Request.QueryString["BillYear"].ToString().Trim());
                    BillType = Request.QueryString["BillType"].ToString().Trim();
                    BillNumb = Request.QueryString["BillNumb"].ToString().Trim();
                    BillDate = Request.QueryString["BillDate"].ToString().Trim();
                    SetDataForPV(LedgerCode, dblBillAmt);
                }
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
            grdJourenaldetails.Columns[6].Visible = true;
            ddlEntry_Type.Enabled = true;
            ddlLedgerCode.Enabled = true;
            btnSaveDetail.Enabled = true;
            btnCancel.Enabled = true;
            txtCreditAmount.Enabled = true;
            txtDebitAmount.Enabled = true;
            txtDocNo.Enabled = true;
            txtDocDT.Enabled = true;
            txtTranDescription.Enabled = true;

            BlankControls();
            //txtVoucherNo.Text = GetVoucherNo();
            if (txtJournalDate.Text == "")
                txtJournalDate.Text = System.DateTime.Now.Date.ToShortDateString();

            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            txtVoucherNo.AutoPostBack = false;
            ddlVoucherType.Text = "PURCHASE";
            ddlVoucherType.ReadOnly = true;
            txtVoucherNo.Text = GenerateVoucherNumberForTextBox();
            txtVoucherNo.ReadOnly = true;
            trTDS.Visible = false;
            trTDSGrid.Visible = false;
            trToolBar.Visible = true;
            trClose.Visible = false;
            ddlContractCode.Visible = false;
            btnTDS.Visible = false;
            lblTDSText.Visible = false;
            trOption.Visible = false;
            lblNewMsg.Visible = false;
            BindLedgerCode();
        }
        catch
        {
            throw;
        }
    }

    private void InitialiseForPV()
    {
        try
        {
            trClose.Visible = false;
            trOption.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void BindLedgerCode()
    {
        try
        {
            ddlLedgerCode.Items.Clear();
            DataTable data = new DataTable();
            data = GetItems("", 0, 10);
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
            lblMessage.Text = "";
            BindGridFromTable();
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
           return GenerateVoucherNumber();
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
            lblMessage.Text = "";
        }
        catch
        {
            throw;
        }
    }

    private void SetEntryType()
    {
        try
        {
            if (ddlEntry_Type.SelectedValue.Trim() == "Dr")
            {
                txtDebitAmount.ReadOnly = false;
                txtCreditAmount.ReadOnly = true;
                txtCreditAmount.Text = "";
            }
            else
            {
                txtCreditAmount.ReadOnly = false;
                txtDebitAmount.ReadOnly = true;
                txtDebitAmount.Text = "";
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
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
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

    private string GenerateVoucherNumberForTextBox()
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

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

    private void SetEditDetailByVoucherNo(string Voucher_No)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
            txtVoucherNo.ReadOnly = false;
            txtVoucherNo.Text = string.Empty;
            ddlVoucherNo.Visible = true;
            txtVoucherNo.Visible = false;
            txtVoucherNo.AutoPostBack = true;

            RefreshDetailRow();
            ddlEntry_Type.SelectedIndex = 0;
            ddlLedgerCode.SelectedIndex = 0;

            FillEditDataByVoucherNo(Voucher_No);

            DataTable dt = GetDataForVouchernO("");

            ddlVoucherNo.DataSource = dt;
            ddlVoucherNo.DataTextField = "VCHR_NO";
            ddlVoucherNo.DataValueField = "VCHR_NO";
            ddlVoucherNo.DataBind();

            ddlVoucherNo.SelectedText = Voucher_No;
            if (ddlVoucherNo.Items.Count > 0)
            {
                foreach (ComboBoxItem cmbItem in ddlVoucherNo.Items)
                {
                    if (cmbItem.Text == Voucher_No)
                    {
                        ddlVoucherNo.SelectedIndex = ddlVoucherNo.Items.IndexOf(cmbItem);
                        cmbItem.Selected = true;
                        break;
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillEditDataByVoucherNo(string Voucher_No)
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = Voucher_No;
            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectByVoucherNo(oFA_Journal_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                int iTDS = 0;
                string strTDSVoucher = string.Empty;
                ddlVoucherType.Text = dt.Rows[0]["VCHR_NAME"].ToString();
                txtJournalDate.Text = dt.Rows[0]["JOURNAL_DATE"].ToString();
                txtDescription.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                txtVoucherNo.Text = Voucher_No;
                JournalId = dt.Rows[0]["JOURNAL_ID"].ToString();
                iTDS = Convert.ToInt32(dt.Rows[0]["TDS_FLAG"].ToString());
                strTDSVoucher = dt.Rows[0]["TDS_VCHR_NO"].ToString();
                ViewState["JournalId"] = JournalId;
                DataTable dt1 = FillEditTRNDataByVoucherNo(Voucher_No, oFA_Journal_MST);
                MapDataTable(dt1);
                BindGridFromTable();

                if (iTDS == 1)
                {
                    trOption.Visible = false;
                    trTDS.Visible = true;
                    trTDSGrid.Visible = true;
                    chkTDS.Visible = true;
                    chkTDS.Checked = true;
                    ddlContractCode.Visible = true;
                    btnTDS.Visible = true;
                    DataTable dt2 = SaitexBL.Interface.Method.FA_TDS_DEDUCT.GetTDSByRefVoucherNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, Voucher_No);
                    string strPartyLedger = dt2.Rows[0]["PARTY_LDGR_CODE"].ToString();
                    BindContractDropdownForEdit(strPartyLedger);
                    ddlContractCode.SelectedValue = dt2.Rows[0]["CONTRACT_CODE"].ToString(); ;
                    ForTDSClickEventForEdit();
                }
                else
                {
                    trTDS.Visible = false;
                    trTDSGrid.Visible = false;

                    if (ViewState["dtTDSDeduction"] != null)
                        dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];

                    if (dtTDSDeduction != null)
                    {
                        dtTDSDeduction.Clear();
                        grdTDS.DataSource = dtTDSDeduction;
                        grdTDS.DataBind();
                    }
                }
            }
            else
            {
                if (ViewState["dtJournalDetail"] != null)
                {
                    dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
                    dtJournalDetail = null;
                }
                grdJourenaldetails.DataSource = dtJournalDetail;
                grdJourenaldetails.DataBind();
                RefreshDetailRow();
                ddlEntry_Type.SelectedIndex = 0;
                ddlLedgerCode.SelectedIndex = 0;
                txtVoucherNo.Text = string.Empty;
                txtDescription.Text = string.Empty;
                Common.CommonFuction.ShowMessage("Sorry Dear! This Purchase Voucher has been comfirmed, you cannot make any change.");
            }
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
            string whereClause = " WHERE VCHR_CODE = '10' And VCHR_NO like :SearchQuery OR VCHR_CODE = '10' AND VCHR_NAME LIKE :SearchQuery";
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

    protected void ddlEntry_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = string.Empty;
            SetEntryType();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Amount Entry Type.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY LDGR_NAME";
            string commandText = "SELECT * FROM FA_LGR_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM FA_LGR_MST WHERE LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
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
            lblMessage.Text = "";
            if (ViewState["UNIQUE_ID"] != null)
                unique_id = int.Parse(ViewState["UNIQUE_ID"].ToString());

            if (!CheckDuplicateRow(ddlLedgerCode.SelectedValue.Trim(), unique_id))
            {
                if (ddlEntry_Type.SelectedValue.Trim() == "Dr")
                {
                    txtDebitAmount.Text = string.Empty;
                    txtDebitAmount.Focus();
                }
                else
                {
                    txtCreditAmount.Text = string.Empty;
                    txtCreditAmount.Focus();
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Ledger already included");
                RefreshDetailRow();
                ddlEntry_Type.SelectedIndex = 0;
                ddlLedgerCode.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Ledger Code..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool CheckDuplicateRow(string Ledger_Code, int UniqueId)
    {
        try
        {
            bool IsDuplicate = false;
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail != null)
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
        catch
        {
            throw;
        }
    }

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (ddlVoucherType.Text != "")
            {
                if (ddlLedgerCode.SelectedIndex > 0)
                {
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
                                        dvEdit.RowFilter = "LEDGER_CODE='" + Ledger_Code + "' and UNIQUE_ID=" + UNIQUE_ID;
                                        if (dvEdit.Count > 0)
                                        {
                                            dvEdit[0]["ENTRY_TYPE"] = Entry_type;
                                            dvEdit[0]["LEDGER_CODE"] = Ledger_Code;
                                            dvEdit[0]["LEDGER_NAME"] = Ledger_Name;
                                            dvEdit[0]["IS_DEBIT"] = IsDebit;
                                            dvEdit[0]["AMOUNT"] = Amount;

                                            if (Debit_Amount > 0)
                                                dvEdit[0]["DR_AMOUNT"] = Debit_Amount;
                                            if (Credit_amount > 0)
                                                dvEdit[0]["CR_AMOUNT"] = Credit_amount;

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
                                            dr["DR_AMOUNT"] = Debit_Amount;
                                        if (Credit_amount > 0)
                                            dr["CR_AMOUNT"] = Credit_amount;

                                        dr["DESC"] = Desc;
                                        dtJournalDetail.Rows.Add(dr);
                                    }
                                    RefreshDetailRow();
                                    ddlEntry_Type.SelectedIndex = 0;
                                    ddlLedgerCode.SelectedIndex = 0;
                                }
                            }
                            ViewState["dtJournalDetail"] = dtJournalDetail;
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
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please select Ledger first.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Voucher Type first.");
            }
            BindGridFromTable();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Details.\r\nSee error log for detail."));
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
            
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTableTax()
    {
        try
        {
            dtJournalDetailTax = new DataTable();
            dtJournalDetailTax.Columns.Add("UNIQUE_ID", typeof(int));
            dtJournalDetailTax.Columns.Add("ENTRY_TYPE", typeof(string));
            dtJournalDetailTax.Columns.Add("LEDGER_CODE", typeof(string));
            dtJournalDetailTax.Columns.Add("LEDGER_NAME", typeof(string));
            dtJournalDetailTax.Columns.Add("IS_DEBIT", typeof(bool));
            dtJournalDetailTax.Columns.Add("AMOUNT", typeof(double));
            dtJournalDetailTax.Columns.Add("DR_AMOUNT", typeof(double));
            dtJournalDetailTax.Columns.Add("CR_AMOUNT", typeof(double));
            dtJournalDetailTax.Columns.Add("DESC", typeof(string));
            dtJournalDetailTax.Columns.Add("DOC_NO", typeof(string));
            dtJournalDetailTax.Columns.Add("DOC_DT", typeof(string));
            return dtJournalDetailTax;
        }
        catch
        {
            throw;
        }
    }


    private void ForTDSClickEvent()
    {
        try
        {
            if (ViewState["dtTDSDeduction"] != null)
            {
                dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];
                dtTDSDeduction = null;
            }
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
            double dblNewAmt = 0;
            string Doc_No = string.Empty;
            string Doc_Dt = string.Empty;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (trTDS.Visible == true)
            {
                if (ddlContractCode.SelectedIndex != -1)
                {
                    if (chkTDS.Checked == true)
                    {
                        // checking only 1 TDS deduct
                        if (dtTDSDeduction == null)
                            CreateTDSDataTable();

                        if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dtJournalDetail);
                            dv.RowFilter = "LEDGER_CODE= '" + lblTDSLedgerCode.Text.Trim() + "'";
                            if (dv.Count > 0)
                            {
                                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                                {
                                    Entry_type = dv[iLoop]["ENTRY_TYPE"].ToString();
                                    Ledger_Code = dv[iLoop]["LEDGER_CODE"].ToString();
                                    Ledger_Name = dv[iLoop]["LEDGER_NAME"].ToString();
                                    IsDebit = bool.Parse(dv[iLoop]["IS_DEBIT"].ToString());
                                    Debit_Amount = 0;
                                    double.TryParse(dv[iLoop]["DR_AMOUNT"].ToString().Trim(), out Debit_Amount);
                                    Credit_amount = 0;
                                    dblNewAmt = 0;
                                    double.TryParse(txtNewAmount.Text.Trim(), out dblNewAmt);

                                    if (dblNewAmt > 0)
                                    {
                                        Amount = double.Parse(txtNewAmount.Text.Trim());
                                        double.TryParse(txtNewAmount.Text.Trim(), out Credit_amount);
                                    }
                                    else
                                    {
                                        Amount = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                                        double.TryParse(dv[iLoop]["CR_AMOUNT"].ToString().Trim(), out Credit_amount);
                                    }

                                    Doc_No = dv[iLoop]["DOC_NO"].ToString();
                                    Doc_Dt = dv[iLoop]["DOC_DT"].ToString();
                                }
                            }
                        }

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
                        Ledger_Code = lblTDSLedgerCode.Text;
                        Ledger_Name = lblTDSLedgerName.Text;
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
                        ViewState["dtTDSDeduction"] = dtTDSDeduction;
                        BindTDSGridFromTable();
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
        catch
        {
            throw;
        }
    }

    private void ForTDSClickEventForEdit()
    {
        try
        {
            if (ViewState["dtTDSDeduction"] != null)
            {
                dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];
                dtTDSDeduction = null;
            }
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

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (trTDS.Visible == true)
            {
                if (chkTDS.Checked == true)
                {
                    // checking only 1 TDS deduct
                    if (dtTDSDeduction == null)
                        CreateTDSDataTable();

                    if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
                    {
                        DataView dv = new DataView(dtJournalDetail);
                        dv.RowFilter = "LEDGER_CODE= '" + lblTDSLedgerCode.Text.Trim() + "'";
                        if (dv.Count > 0)
                        {
                            for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                            {
                                Entry_type = dv[iLoop]["ENTRY_TYPE"].ToString();
                                Ledger_Code = dv[iLoop]["LEDGER_CODE"].ToString();
                                Ledger_Name = dv[iLoop]["LEDGER_NAME"].ToString();
                                IsDebit = bool.Parse(dv[iLoop]["IS_DEBIT"].ToString());
                                Amount = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                                Debit_Amount = 0;
                                double.TryParse(dv[iLoop]["DR_AMOUNT"].ToString().Trim(), out Debit_Amount);
                                Credit_amount = 0;
                                double.TryParse(dv[iLoop]["CR_AMOUNT"].ToString().Trim(), out Credit_amount);
                                Doc_No = dv[iLoop]["DOC_NO"].ToString();
                                Doc_Dt = dv[iLoop]["DOC_DT"].ToString();
                            }
                        }
                    }

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
                    Ledger_Code = lblTDSLedgerCode.Text;
                    Ledger_Name = lblTDSLedgerName.Text;
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
                    ViewState["dtTDSDeduction"] = dtTDSDeduction;
                    BindTDSGridFromTable();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please Check the TDS Deduction CheckBox first..");
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void CreateTDSDataTable()  // Create Datatable for TDS GridView..
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

    private void BindGridFromTable()
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail != null)
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

                CheckTDSDeduction();
            }
            ViewState["dtJournalDetail"] = dtJournalDetail;
        }
        catch
        {
            throw;
        }
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

    private void CheckTDSDeduction()  // Here, we are checking all condition like, voucher checking for deduction
    {
        try
        {
            string strLedgerCode = string.Empty;
            string strLedgerName = string.Empty;
            string strFirstLedgerCode = string.Empty;
            string strFirstLedgerName = string.Empty;
            int iCount = 0;
            bool bChkTDS = false;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            // Only following four vouchers TDS should be deducted.
            // Purchase = 10.
            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                DataView dv = new DataView(dtJournalDetail);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        strLedgerCode = string.Empty;
                        if (dv[iLoop]["ENTRY_TYPE"].ToString() == "Cr")
                        {
                            strLedgerCode = dv[iLoop]["LEDGER_CODE"].ToString();
                            strLedgerName = dv[iLoop]["LEDGER_NAME"].ToString();

                            DataTable dt = SaitexBL.Interface.Method.FA_TDS_MST.SelectAll();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataView dv1 = new DataView(dt);
                                dv1.RowFilter = "LDGR_CODE='" + strLedgerCode + "'";
                                if (dv1.Count > 0)
                                {
                                    trOption.Visible = true;
                                    rdoLstOption.Items[0].Selected = false;
                                    rdoLstOption.Items[1].Selected = false;
                                    rdoLstOption.Items[1].Value = strLedgerCode;
                                    bChkTDS = true;
                                    chkTDS.Checked = false;
                                    ddlContractCode.SelectedIndex = -1;

                                    if (ViewState["dtJournalDetail"] != null)
                                        dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

                                    if (dtTDSDeduction != null)
                                    {
                                        dtTDSDeduction.Rows.Clear();
                                        grdTDS.DataBind();
                                    }
                                }
                            }
                            if (bChkTDS == true)
                            {
                                iCount++;
                                if (iCount == 1)
                                {
                                    strFirstLedgerCode = strLedgerCode;
                                    strFirstLedgerName = strLedgerName;
                                }
                            }
                        }
                    }
                }
                if (iCount > 1)    // Check condition for no multiple TDS deduction allowed here for any party..
                {
                    int iRowCount = 0;
                    Common.CommonFuction.ShowMessage("Sorry! Dear, You cannot make multiple TDS deduction in this voucher..");
                    iRowCount = dtJournalDetail.Rows.Count;
                    foreach (DataRow dr in dtJournalDetail.Rows)
                    {
                        int iUNIQUEID = int.Parse(dr["UNIQUE_ID"].ToString());
                        if (iUNIQUEID == iRowCount)
                        {
                            dtJournalDetail.Rows.Remove(dr);
                            break;
                        }
                    }
                    ViewState["dtJournalDetail"] = dtJournalDetail;
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
                else if (iCount == 1)  // Fill Labels according to TDS deduction
                {
                    lblTDSLedgerCode.Text = strFirstLedgerCode;
                    lblTDSLedgerName.Text = strFirstLedgerName;
                    lblTDSText.Visible = true;
                }
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
            ddlEntry_Type.SelectedIndex = 0;
            ddlLedgerCode.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void SaveJournalEntry()
    {
        try
        {
            bool bUpdateBillMst = false;   // For Updating TX_BILL_MST (As Purchase Voucher entry)
            if (Request.QueryString["LedgerCode"] != null && Request.QueryString["LedgerCode"].ToString() != "" && Request.QueryString["BillAmt"] != null && Request.QueryString["BillAmt"].ToString() != "")
            {
                bUpdateBillMst = true;
            }

            string vou_no = string.Empty;
            string tds_vou_no = string.Empty;
            bool bTDS = false;
            bool bAdviceBill = false;
            double Debit_Total = 0;
            double Credit_Total = 0;
            int TDS_JournalId = 0;
            string TDS_VoucherNo = string.Empty;
            string TDS_DESCRIPTION = string.Empty;
            string TDS_VoucherCode = string.Empty;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (trOption.Visible == true && rdoLstOption.Items[0].Selected == false && rdoLstOption.Items[1].Selected == false)
            {
                Common.CommonFuction.ShowMessage("Please Select Purchase Voucher Category from RadioButton, Bill Wise OR Advice Wise...");
            }
            else
            {
                if (rdoLstOption.Items[1].Selected == true)
                {
                    bAdviceBill = true;
                }

                if (CheckDuplicateTDS())
                {
                    Common.CommonFuction.ShowMessage("This TDS record is already found.");
                }
                else
                {
                    if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
                    {
                        oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
                        oFA_TDS_DEDUCT = new SaitexDM.Common.DataModel.FA_TDS_DEDUCT();

                        if (ViewState["JournalId"] != null)
                            JournalId = ViewState["JournalId"].ToString();

                        if (ViewState["strTDSJournalId"] != null)
                            strTDSJournalId = ViewState["strTDSJournalId"].ToString();

                        CalculateDebitCreditTotal(out Debit_Total, out Credit_Total);
                        if (Debit_Total == Credit_Total)
                        {
                            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                            oFA_Journal_MST.JOURNAL_DATE = DateTime.Now; //DateTime.Parse(txtJournalDate.Text.Trim());
                            oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
                            oFA_Journal_MST.VOUCHER_NO = GenerateVoucherNumber();
                            oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                            oFA_Journal_MST.TRN_DATE = DateTime.Now; //DateTime.Parse(txtJournalDate.Text.Trim());
                            oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                            oFA_Journal_MST.STATUS = true;
                            vou_no = GenerateVoucherNumber();

                            //// For TDS Deduction Journal Entry insertion...
                            if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
                            {
                                // Execute TDSJournalID..
                                string strJournalVoucher = GenerateVoucherNumberForTDS();

                                bTDS = true;
                                TDS_JournalId = int.Parse(strTDSJournalId) + 1;
                                TDS_VoucherNo = GenerateVoucherNumberForTDS();
                                TDS_VoucherCode = "6";
                                TDS_DESCRIPTION = "TDS Deduction for " + lblTDSLedgerName.Text.Trim() + " Account, against Voucher Number :" + vou_no;

                                // For TDS Deduction
                                oFA_TDS_DEDUCT.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                oFA_TDS_DEDUCT.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                oFA_TDS_DEDUCT.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                                oFA_TDS_DEDUCT.VCHR_CODE = "6";
                                oFA_TDS_DEDUCT.VCHR_NO = GenerateVoucherNumberForTDS();
                                oFA_TDS_DEDUCT.TDS_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                                oFA_TDS_DEDUCT.REF_VCHR_CODE = VoucherCode;
                                oFA_TDS_DEDUCT.REF_VCHR_NO = vou_no;
                                oFA_TDS_DEDUCT.CONTRACT_CODE = ddlContractCode.SelectedValue.Trim();
                                oFA_TDS_DEDUCT.PARTY_LDGR_CODE = lblTDSLedgerCode.Text.Trim();
                                oFA_TDS_DEDUCT.TUSER = oUserLoginDetail.UserCode;
                                tds_vou_no = GenerateVoucherNumberForTDS();
                            }
                            else
                            {
                                bTDS = false;
                            }

                            if (bAdviceBill)
                            {
                                if (Session["dtAdjBill"] != null)
                                {
                                    DataTable dtAdj = new DataTable();
                                    dtAdj = (DataTable)Session["dtAdjBill"];

                                    if (dtAdviceBill == null)
                                    {
                                        CreateAdviceDT();
                                    }
                                    else
                                    {
                                        dtAdviceBill.Rows.Clear();
                                    }

                                    if (dtAdj != null && dtAdj.Rows.Count > 0)
                                    {
                                        bAdviceBill = true;

                                        DataView dvAdj = new DataView(dtAdj);
                                        dvAdj.RowFilter = "IS_ADJUST=1";
                                        if (dvAdj.Count > 0)
                                        {
                                            for (int iLoop = 0; iLoop < dvAdj.Count; iLoop++)
                                            {
                                                double dblAdjAmt = 0;

                                                DataRow dr = dtAdviceBill.NewRow();
                                                dr["UNIQUE_ID"] = dtAdviceBill.Rows.Count + 1;
                                                dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                                dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                                                dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                                dr["PUR_VCHR_NO"] = vou_no;
                                                dr["PUR_VCHR_DT"] = txtJournalDate.Text.Trim();
                                                dr["ADV_NO"] = dvAdj[iLoop]["ADV_NO"].ToString().Trim();
                                                dr["ADV_DATE"] = dvAdj[iLoop]["ADV_DATE"].ToString().Trim();
                                                dr["LEDGER_CODE"] = dvAdj[iLoop]["LEDGER_CODE"].ToString().Trim();

                                                if (bool.Parse(dvAdj[iLoop]["IS_PARTIAL"].ToString()))
                                                {
                                                    double.TryParse(dvAdj[iLoop]["PARTIAL_AMT"].ToString().Trim(), out dblAdjAmt);
                                                    dr["ADJ_AMT"] = dblAdjAmt;
                                                }
                                                else
                                                {
                                                    double.TryParse(dvAdj[iLoop]["ADV_AMT"].ToString().Trim(), out dblAdjAmt);
                                                    dr["ADJ_AMT"] = dblAdjAmt;
                                                }

                                                dr["DOC_NO"] = txtDocNo.Text.Trim();
                                                dr["DOC_DT"] = txtDocDT.Text.Trim();
                                                dr["TUSER"] = oUserLoginDetail.UserCode;
                                                dr["TDATE"] = System.DateTime.Now.ToShortDateString();
                                                dr["DEL_STATUS"] = false;
                                                dtAdviceBill.Rows.Add(dr);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        bAdviceBill = false;
                                    }
                                }
                            }

                            int iRecordFound = 0;
                            bool bResultTDSTran = SaitexBL.Interface.Method.FA_Journal_DTL.InsertPurchaseVoucher(oFA_Journal_MST, TDS_VoucherNo, TDS_JournalId, TDS_DESCRIPTION, TDS_VoucherCode, dtJournalDetail, dtTDSDeduction, oFA_TDS_DEDUCT, bTDS, bAdviceBill, dtAdviceBill, bUpdateBillMst, Branch, BillYear, BillType, BillNumb, out iRecordFound);
                            if (bResultTDSTran)
                            {
                                string strMsg = string.Empty;
                                if (bTDS == true && bAdviceBill == false)
                                {
                                    strMsg = "Purchase Voucher (Bill Wise) And TDS Entry Saved Successfully !\\r\\nYour Purchase Voucher Number is: " + vou_no + " And TDS Voucher Number is: " + tds_vou_no;
                                }
                                else if (bAdviceBill == true && bTDS == false)
                                {
                                    strMsg = "Purchase Voucher (Advice Wise) Saved Successfully! \\r\\n And Your Purchase Voucher Number is: " + vou_no;
                                }
                                else if (bAdviceBill == true && bTDS == true)
                                {
                                    strMsg = "Purchase Voucher (Advice Wise) And TDS Entry Saved Successfully !\\r\\nYour Purchase Voucher Number is: " + vou_no + " And TDS Voucher Number is: " + tds_vou_no;
                                }
                                else
                                {
                                    strMsg = "Purchase Voucher Saved Successfully! \\r\\n And Your Purchase Voucher Number is: " + vou_no;
                                }

                                if (strMsg == "")
                                {

                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage(strMsg);
                                }

                                if (dtTDSDeduction != null)
                                {
                                    dtTDSDeduction.Rows.Clear();
                                    grdTDS.DataBind();
                                }
                                if (dtJournalDetail != null)
                                {
                                    dtJournalDetail.Rows.Clear();
                                    grdJourenaldetails.DataBind();
                                }
                                if (Session["dtAdjBill"] != null)
                                    Session["dtAdjBill"] = null;

                                InitialisePage();
                                txtDescription.Text = string.Empty;
                                txtNewAmount.Text = string.Empty;
                            }
                            else if (iRecordFound == 1)
                            {
                                Common.CommonFuction.ShowMessage("This Purchase Voucher is already saved.. Please enter another..");
                            }
                            else if (iRecordFound == 2)
                            {
                                Common.CommonFuction.ShowMessage("This TDS Voucher is already saved.. Please enter another..");
                            }
                            else if (iRecordFound == 3)
                            {
                                Common.CommonFuction.ShowMessage("TDS Entry is already saved.. Please enter another..");
                            }
                            else
                            {
                                if (bTDS == true && bAdviceBill == false)
                                {
                                    Common.CommonFuction.ShowMessage("Purchase Voucher (Bill Wise) And TDS Entry Saving failed..");
                                }
                                else if (bAdviceBill == true && bTDS == true)
                                {
                                    Common.CommonFuction.ShowMessage("Purchase Voucher (Advice Wise) And TDS Entry Saving failed..");
                                }
                                else if (bAdviceBill == true && bTDS == false)
                                {
                                    Common.CommonFuction.ShowMessage("Purchase Voucher (Advice Wise) Saving failed..");
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Purchase Voucher Saving failed..");
                                }
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
            }
        }
        catch
        {
            throw;
        }
    }

    private string GenerateVoucherNumberForTDS()
    {
        try
        {
            oFA_Journal_MST1 = new SaitexDM.Common.DataModel.FA_Journal_MST();

            string Voucher_No = string.Empty;

            oFA_Journal_MST1.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST1.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST1.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST1.VOUCHER_CODE = "6";  // For Journal Voucher

            DataTable dtVoucher = SaitexDL.Interface.Method.FA_VCHR_MST.GetVoucherMaster();
            DataView dv = new DataView(dtVoucher);
            dv.RowFilter = "VCHR_CODE='" + oFA_Journal_MST1.VOUCHER_CODE + "'";
            if (dv.Count > 0)
            {
                strTDSJournalId = SaitexBL.Interface.Method.FA_Journal_DTL.GetNewJournalId(oFA_Journal_MST1);
                strTDSJournalId = (int.Parse(strTDSJournalId) + 1).ToString();
                ViewState["strTDSJournalId"] = strTDSJournalId;
                Voucher_No = oFA_Journal_MST1.YEAR.ToString() + DateTime.Now.Date.Month.ToString() + dv[0]["VCHR_PREFIX"].ToString() + strTDSJournalId + dv[0]["VCHR_SUFFIX"].ToString();
            }
            return Voucher_No;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void UpdateJournalEntry()
    {
        try
        {
            double Debit_Total = 0;
            double Credit_Total = 0;

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString();

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
                    oFA_Journal_MST.JOURNAL_DATE = DateTime.MinValue;//DateTime.Parse(txtJournalDate.Text.Trim());
                    oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
                    oFA_Journal_MST.VOUCHER_NO = txtVoucherNo.Text.Trim();
                    oFA_Journal_MST.DESCRIPTION = txtTranDescription.Text.Trim();
                    oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                    oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                    oFA_Journal_MST.STATUS = true;

                    bool bResult = SaitexBL.Interface.Method.FA_Journal_DTL.Update(oFA_Journal_MST, dtJournalDetail);
                    if (bResult)
                    {
                        if (dtTDSDeduction != null)
                        {
                            dtTDSDeduction.Rows.Clear();
                            grdTDS.DataBind();
                        }
                        if (dtJournalDetail != null)
                        {
                            dtJournalDetail.Rows.Clear();
                            grdJourenaldetails.DataBind();
                        }
                        if (Session["dtAdjBill"] != null)
                            Session["dtAdjBill"] = null;

                        InitialisePage();
                        txtDescription.Text = string.Empty;
                        txtNewAmount.Text = string.Empty;
                        Common.CommonFuction.ShowMessage("Purchase Voucher Updated Successfully");
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Purchase Voucher updation failed.");
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deletion.\r\nSee error log for detail."));
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

            if (dtTDSDeduction != null)
            {
                dtTDSDeduction.Rows.Clear();
                grdTDS.DataBind();
            }

            RefreshDetailRow();
            ddlEntry_Type.SelectedIndex = 0;
            ddlLedgerCode.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //InitialisePage();
            Response.Redirect("./PurchaseVoucher.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row Command.\r\nSee error log for detail."));
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
            // For TDS Deduction
            if (dtJournalDetail.Rows.Count == 0)
            {
                dtTDSDeduction = null;
                grdTDS.DataSource = dtTDSDeduction;
                grdTDS.DataBind();
                chkTDS.Checked = false;
                trTDS.Visible = false;
                trTDSGrid.Visible = false;
                ddlContractCode.Visible = false;
                btnTDS.Visible = false;
                ddlContractCode.SelectedIndex = -1;
            }
            ViewState["dtJournalDetail"] = dtJournalDetail;
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
                ddlEntry_Type.SelectedIndex = 0;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Voucher Number Text Changed.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void MapDataTable(DataTable dt)
    {
        try
        {
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
                    IsDebit = true;

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
            // oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
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

    protected void ddlVoucherNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlVoucherNo.Items.Clear();

            DataTable dt = GetDataForVouchernO(e.Text.ToUpper());

            ddlVoucherNo.DataSource = dt;
            ddlVoucherNo.DataTextField = "VCHR_NO";
            ddlVoucherNo.DataValueField = "VCHR_NO";
            ddlVoucherNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + dt.Rows.Count;

            e.ItemsCount = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Voucher Number.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlVoucherNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            FillEditDataByVoucherNo(ddlVoucherNo.SelectedText.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Voucher Number.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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

    protected void ddlContractCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindContractDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Contract Code..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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

                dv.RowFilter = "LDGR_CODE='" + lblTDSLedgerCode.Text + "'";
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

    private void BindContractDropdownForEdit(string VendorCode)
    {
        try
        {
            ddlContractCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_TDS_MST.SelectAll();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LDGR_CODE='" + VendorCode + "'";
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

    protected void btnTDS_Click(object sender, EventArgs e)
    {
        try
        {
            ForTDSClickEvent();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in TDS Duduction..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                bindContractDropdown();
            }
            else
            {
                ddlContractCode.Visible = false;
                btnTDS.Visible = false;
                if (dtTDSDeduction != null)
                {
                    dtTDSDeduction.Rows.Clear();
                    grdTDS.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Contract Code..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindTDSGridFromTable()  // Bind TDS GridView And Bind Footer..
    {
        try
        {
            if (ViewState["dtTDSDeduction"] != null)
                dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];

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
            ViewState["dtTDSDeduction"] = dtTDSDeduction;
        }
        catch
        {
            throw;
        }
    }

    private void CalculateDebitCreditTotalForTDS(out double Debit_Total, out double Credit_Total, out double Tax_Total) // To calculate Dr Cr and Total tax for Grid Footer
    {
        try
        {
            Debit_Total = 0;
            Credit_Total = 0;
            Tax_Total = 0;

            if (ViewState["dtTDSDeduction"] != null)
                dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];

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

    private bool CheckDuplicateTDS()
    {
        try
        {
            bool IsDuplicate = false;

            if (ViewState["dtTDSDeduction"] != null)
                dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];

            if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
            {
                oFA_TDS_DEDUCT = new SaitexDM.Common.DataModel.FA_TDS_DEDUCT();
                oFA_TDS_DEDUCT.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFA_TDS_DEDUCT.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFA_TDS_DEDUCT.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFA_TDS_DEDUCT.REF_VCHR_CODE = VoucherCode;
                oFA_TDS_DEDUCT.REF_VCHR_NO = txtVoucherNo.Text.Trim();
                oFA_TDS_DEDUCT.CONTRACT_CODE = ddlContractCode.SelectedValue.Trim();
                oFA_TDS_DEDUCT.PARTY_LDGR_CODE = lblTDSLedgerCode.Text.Trim();

                DataTable dt = SaitexBL.Interface.Method.FA_TDS_DEDUCT.GetAllTDS(oFA_TDS_DEDUCT);

                if (dt != null && dt.Rows.Count > 0)
                {
                    IsDuplicate = true;
                }
            }
            return IsDuplicate;
        }
        catch
        {
            throw;
        }
    }

    protected void rdoLstOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strLdgrCode = string.Empty;
            double dblJVAmount = 0;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (ViewState["dtTDSDeduction"] != null)
            {
                dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];
                dtTDSDeduction.Rows.Clear();
                grdTDS.DataBind();
            }

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                if (rdoLstOption.Items[0].Selected == true)
                {
                    trTDS.Visible = true;
                    trTDSGrid.Visible = true;
                    lblNewMsg.Visible = false;
                    txtNewAmount.Text = "";
                    chkTDS.Checked = false;
                    btnTDS.Visible = false;
                    ddlContractCode.Visible = false;
                    ddlContractCode.SelectedIndex = -1;
                }
                else
                {
                    chkTDS.Checked = false;
                    ddlContractCode.Visible = false;
                    ddlContractCode.SelectedIndex = -1;

                    DataView dv = new DataView(dtJournalDetail);
                    dv.RowFilter = "ENTRY_TYPE ='Cr'";
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            strLdgrCode = dv[iLoop]["LEDGER_CODE"].ToString();
                            dblJVAmount = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                        }
                    }

                    trTDS.Visible = false;
                    trTDSGrid.Visible = false;
                    txtNewAmount.ReadOnly = false;

                    string URL = "AdjustAdviceWithBill.aspx";
                    URL = URL + "?LedgerCode=" + strLdgrCode.Trim();
                    URL = URL + "&dblJVAmount=" + dblJVAmount;
                    URL = URL + "&TextBoxId=" + txtNewAmount.ClientID;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=500');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please provide debit and credit entries.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Radio Button Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtNewAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dblNewAmt = 0;
            txtNewAmount.ReadOnly = true;
            dblNewAmt = double.Parse(txtNewAmount.Text.Trim());

            if (dblNewAmt > 0)
            {
                lblNewMsg.Visible = true;
                trTDS.Visible = true;
                trTDSGrid.Visible = true;
            }
            else
            {
                txtNewAmount.Text = "";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in New Voucher Amount TextBox Changed Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void CreateAdviceDT()
    {
        try
        {
            dtAdviceBill = new DataTable();
            dtAdviceBill.Columns.Add("UNIQUE_ID", typeof(int));
            dtAdviceBill.Columns.Add("COMP_CODE", typeof(string));
            dtAdviceBill.Columns.Add("BRANCH_CODE", typeof(string));
            dtAdviceBill.Columns.Add("YEAR", typeof(int));
            dtAdviceBill.Columns.Add("PUR_VCHR_NO", typeof(string));
            dtAdviceBill.Columns.Add("PUR_VCHR_DT", typeof(string));
            dtAdviceBill.Columns.Add("ADV_NO", typeof(string));
            dtAdviceBill.Columns.Add("ADV_DATE", typeof(string));
            dtAdviceBill.Columns.Add("LEDGER_CODE", typeof(string));
            dtAdviceBill.Columns.Add("ADJ_AMT", typeof(double));
            dtAdviceBill.Columns.Add("DOC_NO", typeof(string));
            dtAdviceBill.Columns.Add("DOC_DT", typeof(string));
            dtAdviceBill.Columns.Add("TUSER", typeof(string));
            dtAdviceBill.Columns.Add("TDATE", typeof(string));
            dtAdviceBill.Columns.Add("DEL_STATUS", typeof(bool));
        }
        catch
        {
            throw;
        }
    }

    // For PV Generating Automatically..
    private void SetDataForPV(string LedgerCode, double dblBillAmt)
    {
        try
        {
            trToolBar.Visible = false;
            trClose.Visible = true;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail == null)
                CreateDataTable();

            // For Debit Entry PV
            DataRow dr = dtJournalDetail.NewRow();
            dr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
            dr["ENTRY_TYPE"] = "Dr";

            if (BillType.Equals("FSP"))
            {
                dr["LEDGER_CODE"] = "721";
                dr["LEDGER_NAME"] = "POY PURCHASE A/C";
            }
            else if (BillType.Equals("YSP"))
            {
                dr["LEDGER_CODE"] = "722";
                dr["LEDGER_NAME"] = "YARN PURCHASE A/C";
            }
            if (BillType.Equals("MSP"))
            {
                dr["LEDGER_CODE"] = "723";
                dr["LEDGER_NAME"] = "ITEM PURCHASE A/C";
            }           
            dr["IS_DEBIT"] = true;
            dr["AMOUNT"] = dblBillAmt;
            dr["DOC_NO"] = BillNumb;
            dr["DOC_DT"] = BillDate;
            dr["DR_AMOUNT"] = dblBillAmt;
            dr["DESC"] = "Debit Entry of Purchase Voucher, against Bill No: " + BillNumb + ", and Bill Date: " + BillDate;
            dtJournalDetail.Rows.Add(dr);

            // For Credit Entry PV
            DataRow cr = dtJournalDetail.NewRow();
            cr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
            cr["ENTRY_TYPE"] = "Cr";
            cr["LEDGER_CODE"] = LedgerCode;

            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMasterWithCode(LedgerCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                cr["LEDGER_NAME"] = dt.Rows[0]["LDGR_NAME"].ToString().Trim();
            }

            cr["IS_DEBIT"] = false;
            cr["AMOUNT"] = dblBillAmt;
            cr["DOC_NO"] = BillNumb;
            cr["DOC_DT"] = BillDate;
            cr["CR_AMOUNT"] = dblBillAmt;
            cr["DESC"] = "Credit Entry of Purchase Voucher, against Bill No: " + BillNumb + ", and Bill Date: " + BillDate;
            dtJournalDetail.Rows.Add(cr);


            if (ViewState["dtJournalDetailTax"] != null)
                dtJournalDetailTax = (DataTable)ViewState["dtJournalDetailTax"];

            if (dtJournalDetailTax == null)
                dtJournalDetailTax=CreateDataTableTax();
            // For Debit Entry For Tax Recievalble
            DataTable dttx = SaitexBL.Interface.Method.FA_LGR_MST.GetTaxRecievales(oUserLoginDetail.COMP_CODE , oUserLoginDetail.CH_BRANCHCODE, BillYear.ToString(), BillType, BillNumb);
            for (int i=0; i< dttx.Rows.Count;i++)
            {
                if (dttx.Rows[i]["COMPO_CODE"].ToString().Equals("GST"))
                {
                    LedgerCode = "727";
                }
                else if (dttx.Rows[i]["COMPO_CODE"].ToString().Equals("TDS"))
                {
                    LedgerCode = "728";
                }
                else  if (dttx.Rows[i]["COMPO_CODE"].ToString().Equals("EXCISE DUTY"))
                {
                    LedgerCode = "731";
                }
                else if (dttx.Rows[i]["COMPO_CODE"].ToString().Equals("CST"))
                {
                    LedgerCode = "729";
                }

                DataRow tx = dtJournalDetailTax.NewRow();
                tx["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
                tx["ENTRY_TYPE"] = "Dr";
                tx["LEDGER_CODE"] = LedgerCode;

                DataTable dtlcode = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMasterWithCode(LedgerCode);
                if (dtlcode != null && dtlcode.Rows.Count > 0)
                {
                    tx["LEDGER_NAME"] = dtlcode.Rows[0]["LDGR_NAME"].ToString().Trim();
                }
                double taxamt=0;
                double.TryParse(dttx.Rows[i]["TOTAL_TRN_AMT"].ToString(), out taxamt);
                
                tx["IS_DEBIT"] = true;
                tx["AMOUNT"] = dblBillAmt;
                tx["DOC_NO"] = BillNumb;
                tx["DOC_DT"] = BillDate;
                tx["DR_AMOUNT"] = taxamt;
                tx["AMOUNT"] = taxamt;
                tx["DESC"] = "Debit Entry of Purchase Voucher ("+dttx.Rows[i]["COMPO_CODE"].ToString()+" ), against Bill No: " + BillNumb + ", and Bill Date: " + BillDate;
                dtJournalDetailTax.Rows.Add(tx);

            }
            grdJourenaldetails.DataSource = dtJournalDetail;
            grdJourenaldetails.DataBind();
            grdJourenaldetailsTax.DataSource = dtJournalDetailTax;
            grdJourenaldetailsTax.DataBind();
            BindGridFromTableBill();
            grdJourenaldetails.Columns[6].Visible = false;
            ddlEntry_Type.Enabled = false;
            ddlLedgerCode.Enabled = false;
            btnSaveDetail.Enabled = false;
            btnCancel.Enabled = false;
            txtCreditAmount.Enabled = false;
            txtDebitAmount.Enabled = false;
            txtDocNo.Enabled = false;
            txtDocDT.Enabled = false;
            txtTranDescription.Enabled = false;

            ViewState["dtJournalDetail"] = dtJournalDetail;
            ViewState["dtJournalDetailTax"] = dtJournalDetailTax;
        }
        catch
        {
            throw;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            SavePurchaseVoucherWithBill();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue()", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Closing..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void SavePurchaseVoucherWithBill()
    {
        try
        {
            string vou_no = string.Empty;
            string tds_vou_no = string.Empty;
            bool bTDS = false;
            bool bAdviceBill = false;      // For Bill Wise PV
            bool bUpdateBillMst = true;   // For Updating TX_BILL_MST (As Purchase Voucher entry)
            double Debit_Total = 0;
            double Credit_Total = 0;
            int TDS_JournalId = 0;
            string TDS_VoucherNo = string.Empty;
            string TDS_DESCRIPTION = string.Empty;
            string TDS_VoucherCode = string.Empty;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
            if (ViewState["dtJournalDetailTax"] != null)
                dtJournalDetailTax = (DataTable)ViewState["dtJournalDetailTax"];

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString();

            if (ViewState["strTDSJournalId"] != null)
                strTDSJournalId = ViewState["strTDSJournalId"].ToString();

            if (ViewState["dtTDSDeduction"] != null)
                dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];

            if (CheckDuplicateTDS())
            {
                Common.CommonFuction.ShowMessage("This TDS record is already found.");
            }
            else
            {
                if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
                {
                    oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
                    oFA_TDS_DEDUCT = new SaitexDM.Common.DataModel.FA_TDS_DEDUCT();

                    CalculateDebitCreditTotal(out Debit_Total, out Credit_Total);
                    if (Debit_Total == Credit_Total)
                    {
                        vou_no = GenerateVoucherNumber();
                        oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                        oFA_Journal_MST.JOURNAL_DATE = DateTime.Now;//Date.Parse(txtJournalDate.Text.Trim());
                        oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
                        oFA_Journal_MST.VOUCHER_NO = GenerateVoucherNumber();
                        oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                        oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                        oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                        oFA_Journal_MST.STATUS = true;
                       

                        //// For TDS Deduction Journal Entry insertion...
                        if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
                        {
                            // Execute TDSJournalID..
                            string strJournalVoucher = GenerateVoucherNumberForTDS();

                            bTDS = true;
                            TDS_JournalId = int.Parse(strTDSJournalId) + 1;
                            TDS_VoucherNo = GenerateVoucherNumberForTDS();
                            TDS_VoucherCode = "6";
                            TDS_DESCRIPTION = "TDS Deduction for " + lblTDSLedgerName.Text.Trim() + " Account, against Voucher Number :" + vou_no;

                            // For TDS Deduction
                            oFA_TDS_DEDUCT.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_TDS_DEDUCT.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oFA_TDS_DEDUCT.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                            oFA_TDS_DEDUCT.VCHR_CODE = "6";
                            oFA_TDS_DEDUCT.VCHR_NO = GenerateVoucherNumberForTDS();
                            oFA_TDS_DEDUCT.TDS_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                            oFA_TDS_DEDUCT.REF_VCHR_CODE = VoucherCode;
                            oFA_TDS_DEDUCT.REF_VCHR_NO = vou_no;
                            oFA_TDS_DEDUCT.CONTRACT_CODE = ddlContractCode.SelectedValue.Trim();
                            oFA_TDS_DEDUCT.PARTY_LDGR_CODE = lblTDSLedgerCode.Text.Trim();
                            oFA_TDS_DEDUCT.TUSER = oUserLoginDetail.UserCode;
                            tds_vou_no = GenerateVoucherNumberForTDS();
                        }
                        else
                        {
                            bTDS = false;
                        }

                        if (bAdviceBill)
                        {
                            if (Session["dtAdjBill"] != null)
                            {
                                DataTable dtAdj = new DataTable();
                                dtAdj = (DataTable)Session["dtAdjBill"];

                                if (dtAdviceBill == null)
                                {
                                    CreateAdviceDT();
                                }
                                else
                                {
                                    dtAdviceBill.Rows.Clear();
                                }

                                if (dtAdj != null && dtAdj.Rows.Count > 0)
                                {
                                    bAdviceBill = true;

                                    DataView dvAdj = new DataView(dtAdj);
                                    dvAdj.RowFilter = "IS_ADJUST=1";
                                    if (dvAdj.Count > 0)
                                    {
                                        for (int iLoop = 0; iLoop < dvAdj.Count; iLoop++)
                                        {
                                            double dblAdjAmt = 0;

                                            DataRow dr = dtAdviceBill.NewRow();
                                            dr["UNIQUE_ID"] = dtAdviceBill.Rows.Count + 1;
                                            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                                            dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                            dr["PUR_VCHR_NO"] = vou_no;
                                            dr["PUR_VCHR_DT"] = txtJournalDate.Text.Trim();
                                            dr["ADV_NO"] = dvAdj[iLoop]["ADV_NO"].ToString().Trim();
                                            dr["ADV_DATE"] = dvAdj[iLoop]["ADV_DATE"].ToString().Trim();
                                            dr["LEDGER_CODE"] = dvAdj[iLoop]["LEDGER_CODE"].ToString().Trim();

                                            if (bool.Parse(dvAdj[iLoop]["IS_PARTIAL"].ToString()))
                                            {
                                                double.TryParse(dvAdj[iLoop]["PARTIAL_AMT"].ToString().Trim(), out dblAdjAmt);
                                                dr["ADJ_AMT"] = dblAdjAmt;
                                            }
                                            else
                                            {
                                                double.TryParse(dvAdj[iLoop]["ADV_AMT"].ToString().Trim(), out dblAdjAmt);
                                                dr["ADJ_AMT"] = dblAdjAmt;
                                            }

                                            dr["DOC_NO"] = txtDocNo.Text.Trim();
                                            dr["DOC_DT"] = txtDocDT.Text.Trim();
                                            dr["TUSER"] = oUserLoginDetail.UserCode;
                                            dr["TDATE"] = System.DateTime.Now.ToShortDateString();
                                            dr["DEL_STATUS"] = false;
                                            dtAdviceBill.Rows.Add(dr);
                                        }
                                    }
                                }
                                else
                                {
                                    bAdviceBill = false;
                                }
                            }
                        }


                        foreach (DataRow dr in dtJournalDetailTax.Rows)
                        {
                            DataRow tx = dtJournalDetail.NewRow();
                            tx["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
                            tx["ENTRY_TYPE"] = dr["ENTRY_TYPE"];
                            tx["LEDGER_CODE"] = dr["LEDGER_CODE"];
                            tx["LEDGER_NAME"] = dr["LEDGER_NAME"];
                            tx["IS_DEBIT"] = dr["IS_DEBIT"];
                            tx["AMOUNT"] = dr["AMOUNT"];
                            tx["DOC_NO"] = dr["DOC_NO"];
                            tx["DOC_DT"] = dr["DOC_DT"];
                            tx["DR_AMOUNT"] = dr["DR_AMOUNT"];
                            tx["DESC"] = dr["DESC"];     
                            dtJournalDetail.Rows.Add(tx);
                        }








                        int iRecordFound = 0;
                        bool bResultTDSTran = SaitexBL.Interface.Method.FA_Journal_DTL.InsertPurchaseVoucher(oFA_Journal_MST, TDS_VoucherNo, TDS_JournalId, TDS_DESCRIPTION, TDS_VoucherCode, dtJournalDetail, dtTDSDeduction, oFA_TDS_DEDUCT, bTDS, bAdviceBill, dtAdviceBill, bUpdateBillMst, Branch, BillYear, BillType, BillNumb, out iRecordFound);
                        if (bResultTDSTran)
                        {
                            string strMsg = string.Empty;
                            if (bTDS == true && bAdviceBill == false)
                            {
                                strMsg = "Purchase Voucher (Bill Wise) And TDS Entry Saved Successfully !\\r\\nYour Purchase Voucher Number is: " + vou_no + " And TDS Voucher Number is: " + tds_vou_no;
                            }
                            else if (bAdviceBill == true && bTDS == false)
                            {
                                strMsg = "Purchase Voucher (Advice Wise) Saved Successfully! \\r\\n And Your Purchase Voucher Number is: " + vou_no;
                            }
                            else if (bAdviceBill == true && bTDS == true)
                            {
                                strMsg = "Purchase Voucher (Advice Wise) And TDS Entry Saved Successfully !\\r\\nYour Purchase Voucher Number is: " + vou_no + " And TDS Voucher Number is: " + tds_vou_no;
                            }
                            else
                            {
                                strMsg = "Purchase Voucher Saved Successfully! \\r\\n And Your Purchase Voucher Number is: " + vou_no;
                            }

                            if (strMsg == "")
                            {

                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage(strMsg);
                            }

                            if (dtTDSDeduction != null)
                            {
                                dtTDSDeduction.Rows.Clear();
                                grdTDS.DataBind();
                            }
                            if (dtJournalDetail != null)
                            {
                                dtJournalDetail.Rows.Clear();
                                grdJourenaldetails.DataBind();
                            }
                            if (Session["dtAdjBill"] != null)
                                Session["dtAdjBill"] = null;

                            InitialisePage();
                            txtDescription.Text = string.Empty;
                            txtNewAmount.Text = string.Empty;
                        }
                        else if (iRecordFound == 1)
                        {
                            Common.CommonFuction.ShowMessage("This Purchase Voucher is already saved.. Please enter another..");
                        }
                        else if (iRecordFound == 2)
                        {
                            Common.CommonFuction.ShowMessage("This TDS Voucher is already saved.. Please enter another..");
                        }
                        else if (iRecordFound == 3)
                        {
                            Common.CommonFuction.ShowMessage("TDS Entry is already saved.. Please enter another..");
                        }
                        else
                        {
                            if (bTDS == true && bAdviceBill == false)
                            {
                                Common.CommonFuction.ShowMessage("Purchase Voucher (Bill Wise) And TDS Entry Saving failed..");
                            }
                            else if (bAdviceBill == true && bTDS == true)
                            {
                                Common.CommonFuction.ShowMessage("Purchase Voucher (Advice Wise) And TDS Entry Saving failed..");
                            }
                            else if (bAdviceBill == true && bTDS == false)
                            {
                                Common.CommonFuction.ShowMessage("Purchase Voucher (Advice Wise) Saving failed..");
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Purchase Voucher Saving failed..");
                            }
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
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancelPopUp_Click(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MyCloseScript", "window.close()", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancelling..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindGridFromTableBill()
    {
        try
        {
            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail != null)
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

                CheckTDSDeductionBill();
            }
            ViewState["dtJournalDetail"] = dtJournalDetail;
        }
        catch
        {
            throw;
        }
    }

    private void CheckTDSDeductionBill()  // Here, we are checking all condition like, voucher checking for deduction
    {
        try
        {
            string strLedgerCode = string.Empty;
            string strLedgerName = string.Empty;
            string strFirstLedgerCode = string.Empty;
            string strFirstLedgerName = string.Empty;
            int iCount = 0;
            bool bChkTDS = false;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (ViewState["dtTDSDeduction"] != null)
                dtTDSDeduction = (DataTable)ViewState["dtTDSDeduction"];

            // Only following four vouchers TDS should be deducted.
            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                DataView dv = new DataView(dtJournalDetail);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        strLedgerCode = string.Empty;
                        if (dv[iLoop]["ENTRY_TYPE"].ToString() == "Cr")
                        {
                            strLedgerCode = dv[iLoop]["LEDGER_CODE"].ToString();
                            strLedgerName = dv[iLoop]["LEDGER_NAME"].ToString();

                            DataTable dt = SaitexBL.Interface.Method.FA_TDS_MST.SelectAll();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataView dv1 = new DataView(dt);
                                dv1.RowFilter = "LDGR_CODE='" + strLedgerCode + "'";
                                if (dv1.Count > 0)
                                {
                                    trTDS.Visible = true;
                                    trTDSGrid.Visible = true;
                                    bChkTDS = true;
                                    chkTDS.Checked = false;
                                    ddlContractCode.SelectedIndex = -1;
                                    if (dtTDSDeduction != null)
                                    {
                                        dtTDSDeduction.Rows.Clear();
                                        grdTDS.DataBind();
                                    }
                                }
                            }
                            if (bChkTDS == true)
                            {
                                iCount++;
                                if (iCount == 1)
                                {
                                    strFirstLedgerCode = strLedgerCode;
                                    strFirstLedgerName = strLedgerName;
                                }
                            }
                        }
                    }
                }
                if (iCount > 1)    // Check condition for no multiple TDS deduction allowed here for any party..
                {
                    int iRowCount = 0;
                    Common.CommonFuction.ShowMessage("Sorry! Dear, You cannot make multiple TDS deduction in this voucher..");
                    iRowCount = dtJournalDetail.Rows.Count;
                    foreach (DataRow dr in dtJournalDetail.Rows)
                    {
                        int iUNIQUEID = int.Parse(dr["UNIQUE_ID"].ToString());
                        if (iUNIQUEID == iRowCount)
                        {
                            dtJournalDetail.Rows.Remove(dr);
                            break;
                        }
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
                else if (iCount == 1)  // Fill Labels according to TDS deduction
                {
                    lblTDSLedgerCode.Text = strFirstLedgerCode;
                    lblTDSLedgerName.Text = strFirstLedgerName;
                    lblTDSText.Visible = true;
                }
            }
            ViewState["dtJournalDetail"] = dtJournalDetail;
        }
        catch
        {
            throw;
        }
    }

    protected void btnLedger_Click(object sender, EventArgs e)
    {
        try
        {
            txtLedgerPopUp.Visible = true;
            string URL = "LedgerMstPopUp.aspx";
            URL = URL + "?TextBoxId=" + txtLedgerPopUp.ClientID;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=600');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclickLedger('" + URL + "')", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Open Ledger Window..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtLedgerPopUp_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtLedgerPopUp.Visible = false;
            BindLedgerCode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger Popup Text Changed Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}