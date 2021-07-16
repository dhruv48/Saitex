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

public partial class Module_FA_Controls_AssetDTL : System.Web.UI.UserControl
{
    public static string VoucherCode = "10";     // For Purchase Voucher Fixed
    public static string JournalId = string.Empty;
    public static string strTDSJournalId = string.Empty;
    public static string TDSVoucherNo = string.Empty;
    private static DataTable dtJournalDetail, dtTDSDeduction, dtJournalTrn, dtTDSVoucher, dtAssets;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_CONTRACT_MST oFA_CONTRACT_MST;
    SaitexDM.Common.DataModel.FA_TAX_MST oFA_TAX_MST;
    SaitexDM.Common.DataModel.FA_TDS_DEDUCT oFA_TDS_DEDUCT;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST, oFA_Journal_MST1;
    SaitexDM.Common.DataModel.FA_ASSETS_GRP_MST oFA_ASSETS_GRP_MST;
    SaitexDM.Common.DataModel.FA_ASSETS_DTL oFA_ASSETS_DTL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
            // No TDS Deduction Allowed in Assets Purchasing (Recommended By CA and Shiv Sir at 20 June 2011)
            trTDS.Visible = false;
            trTDSGrid.Visible = false;
            ddlContractCode.Visible = false;
            btnTDS.Visible = false;
            lblTDSText.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    /// <summary>
    /// Use of Intialization. Means blank controls.
    /// </summary>
    private void InitialisePage()
    {
        try
        {
            BlankControls();
            txtVoucherNo.Text = GetVoucherNo();
            if (txtJournalDate.Text == "")
                txtJournalDate.Text = System.DateTime.Now.Date.ToShortDateString();

            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            txtVoucherNo.AutoPostBack = false;
            cmbAssetCode.Visible = true;
            ddlVoucherType.Text = "PURCHASE";
            ddlVoucherType.ReadOnly = true;
            txtVoucherNo.Text = GenerateVoucherNumberForTextBox();
            trTDS.Visible = false;
            trTDSGrid.Visible = false;
            ddlContractCode.Visible = false;
            btnTDS.Visible = false;
            lblTDSText.Visible = false;
            trMaster.Visible = false;
            trSpace.Visible = false;

            RefreshDetailRow();
            BindAssetGroupCombo();
            BindBranchCombo();
            BindDeptCombo();
            BindDetailTypeCombo();
            BindLedgerCode();

            grdAssetsDTL.Columns[9].Visible = false;
            grdAssetsDTL.Columns[10].Visible = false;
            grdAssetsDTL.Columns[11].Visible = false;
            grdAssetsDTL.Columns[15].Visible = false;
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
            txtSalDeprAmt.Text = "";
            txtSalDeprITAmt.Text = "";
            txtSaleCompActAmt.Text = "";
            cmbAssetGroup.SelectedIndex = -1;
            cmbLocation.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            txtDTOfManufacturing.Text = "";
            txtDTOfPurchase.Text = "";
            txtDTOfInstall.Text = "";
            txtDTOfPutInUse.Text = "";
            txtAssetDescription.Text = "";
            txtDescription.Text = "";
            RefreshDetailRow();
            dtJournalDetail = null;
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
            return string.Empty;
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
            return Voucher_No;
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
                                    //trTDS.Visible = true;
                                    //trTDSGrid.Visible = true;
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
                    lblMessage.Text = "Sorry ! Dear you can't make multiple TDS deduction in this voucher..";

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
        }
        catch
        {
            throw;
        }
    }

    protected void cmbAssetCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindAssetCodeCombo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Assets Code..\r\nSee error log for detail."));
        }
    }

    private void BindAssetCodeCombo()
    {
        try
        {
            cmbAssetCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_DTL.GetAssetMstDetail(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbAssetCode.DataValueField = "ASSET_CODE";
                cmbAssetCode.DataTextField = "ASSET_CODE";
                cmbAssetCode.DataSource = dt;
                cmbAssetCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void cmbAssetCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            trMaster.Visible = true;
            trSpace.Visible = true;
            BindAssetsDetail();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Asset Code..\r\nSee error log for detail."));
        }
    }

    private void FillEditDataByAssetCode(string Voucher_No)
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = Voucher_No;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectByVoucherNoAll(oFA_Journal_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtJournalDate.Text = dt.Rows[0]["JOURNAL_DATE"].ToString();
                txtDescription.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                txtVoucherNo.Text = Voucher_No;
                JournalId = dt.Rows[0]["JOURNAL_ID"].ToString();

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
                InitialisePage();
                Common.CommonFuction.ShowMessage("There is no Purchase Voucher found related to this Asset Acquisition Master...");
            }
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

    private void MapDataTable(DataTable dt)
    {
        try
        {
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
        }
        catch
        {
            throw;
        }
    }

    private void BindDetailTypeCombo()
    {
        try
        {
            cmbDetailType.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("FA_ASSET_DTL_TYPE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "MST_CODE <> 'ACQUISITION'";
                if (dv.Count > 0)
                {
                    cmbDetailType.DataSource = dv;
                    cmbDetailType.DataBind();
                }
            }
            cmbDetailType.Items.Insert(0, new ListItem("-- Select Detail Type --", "0"));
        }
        catch
        {
            throw;
        }
    }

    private void BindAssetGroupCombo()
    {
        try
        {
            oFA_ASSETS_GRP_MST = new SaitexDM.Common.DataModel.FA_ASSETS_GRP_MST();

            oFA_ASSETS_GRP_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ASSETS_GRP_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            cmbAssetGroup.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_GRP_MST.GetAssetGroupDetailByComp(oFA_ASSETS_GRP_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbAssetGroup.DataValueField = "ASSET_GRP_CODE";
                cmbAssetGroup.DataTextField = "ASSET_GRP_NAME";
                cmbAssetGroup.DataSource = dt;
                cmbAssetGroup.DataBind();
                cmbAssetGroup.Items.Insert(0, new ListItem("----- Asset Group ----", "0"));
            }
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
            lblMessage.Text = "";
            SetEntryType();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Amount Entry Type.\r\nSee error log for detail."));
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
                    txtDebitAmount.Text = "";
                    txtDebitAmount.Focus();
                }
                else
                {
                    txtCreditAmount.Text = "";
                    txtCreditAmount.Focus();
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Ledger already included");
                RefreshDetailRow();
                ddlLedgerCode.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Ledger Code..\r\nSee error log for detail."));
        }
    }

    private bool CheckDuplicateRow(string Ledger_Code, int UniqueId)
    {
        try
        {
            bool IsDuplicate = false;
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
                                    string spaces = string.Empty;

                                    if (UNIQUE_ID > 0)
                                    {
                                        DataView dvEdit = new DataView(dtJournalDetail);
                                        //dvEdit.RowFilter = "LEDGER_CODE='" + Ledger_Code + "' and UNIQUE_ID=" + UNIQUE_ID;
                                        dvEdit.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                                        if (dvEdit.Count > 0)
                                        {
                                            dvEdit[0]["ENTRY_TYPE"] = spaces + Entry_type;
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
                                            ForTDSClickEvent();
                                        }
                                    }
                                    else
                                    {
                                        DataRow dr = dtJournalDetail.NewRow();
                                        dr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
                                        dr["ENTRY_TYPE"] = spaces + Entry_type;
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
                                    ddlLedgerCode.SelectedIndex = 0;
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
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please select Ledger first.");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please provide Voucher Type first.");
            }
            BindGridFromTable();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Details.\r\nSee error log for detail."));
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

    private void ForTDSClickEvent()
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
            ddlLedgerCode.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel.\r\nSee error log for detail."));
        }
    }

    protected void grdJourenaldetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void chkTDS_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkTDS.Checked == true)
            {
                ddlContractCode.Visible = true;
                btnTDS.Visible = true;
                BindContractDropdown();
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
        }
    }

    private void BindTDSGridFromTable()  // Bind TDS GridView And Bind Footer..
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

    private void CalculateDebitCreditTotalForTDS(out double Debit_Total, out double Credit_Total, out double Tax_Total) // To calculate Dr Cr and Total tax for Grid Footer
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

    protected void ddlContractCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindContractDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Contract Code..\r\nSee error log for detail."));
        }
    }

    private void BindContractDropdown()
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

    protected void btnTDS_Click(object sender, EventArgs e)
    {
        try
        {
            ForTDSClickEvent();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in TDS Duduction..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InsertData();
            BindAssetsDetail();
            trMaster.Visible = true;
            trSpace.Visible = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
        }
    }

    private void InsertData()
    {
        try
        {
            string vou_no = string.Empty;
            string AssetCode = string.Empty;
            string AssetTrnID = string.Empty;
            string tds_vou_no = string.Empty;
            bool bTDS = false;
            double Debit_Total = 0;
            double Credit_Total = 0;
            int TDS_JournalId = 0;
            string TDS_VoucherNo = string.Empty;
            string TDS_DESCRIPTION = string.Empty;
            string TDS_VoucherCode = string.Empty;
            bool bAssetTran = true;  // For Asset Acquisition Attachment.

            if (CheckValidation())
            {
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
                        oFA_ASSETS_DTL = new SaitexDM.Common.DataModel.FA_ASSETS_DTL();

                        CalculateDebitCreditTotal(out Debit_Total, out Credit_Total);
                        if (Debit_Total == Credit_Total)
                        {
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

                            //// For TDS Deduction Journal Entry insertion... 22 Feb 2011
                            if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
                            {
                                // Execute TDSJournalID..
                                string strJournalVoucher = GenerateVoucherNumberForTDS();

                                bTDS = true;
                                TDS_JournalId = int.Parse(strTDSJournalId);
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

                            oFA_ASSETS_DTL.COMP_CODE = oUserLoginDetail.COMP_CODE;
                            oFA_ASSETS_DTL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                            oFA_ASSETS_DTL.ASSET_CODE = cmbAssetCode.SelectedValue.ToString().Trim();
                            AssetCode = cmbAssetCode.SelectedValue.ToString().Trim();
                            oFA_ASSETS_DTL.ASSET_DTL = cmbAssetCode.SelectedValue.ToString().Trim();
                            oFA_ASSETS_DTL.ASSET_DEPT = cmbDepartment.SelectedValue.ToString().Trim();
                            oFA_ASSETS_DTL.ASSET_LOCATION = cmbLocation.SelectedValue.ToString().Trim();

                            double dblSalDprAmt = 0;
                            double.TryParse(txtSalDeprAmt.Text.Trim(), out dblSalDprAmt);
                            oFA_ASSETS_DTL.SAL_DEPR_AMT = dblSalDprAmt;

                            double dblSalITAmt = 0;
                            double.TryParse(txtSalDeprITAmt.Text.Trim(), out dblSalITAmt);
                            oFA_ASSETS_DTL.SAL_IT_AMT = dblSalITAmt;

                            double dblSalCompActAmt = 0;
                            double.TryParse(txtSaleCompActAmt.Text.Trim(), out dblSalCompActAmt);
                            oFA_ASSETS_DTL.SAL_COMP_ACT_AMT = dblSalCompActAmt;

                            oFA_ASSETS_DTL.DESCRIPTION = txtAssetDescription.Text.Trim();
                            oFA_ASSETS_DTL.ASSET_TRN_ID = GenerateAssetTranID();
                            AssetTrnID = GenerateAssetTranID();
                            oFA_ASSETS_DTL.DETAIL_TYPE = cmbDetailType.SelectedValue.ToString().Trim();
                            oFA_ASSETS_DTL.ASSET_GRP_CODE = cmbAssetGroup.SelectedValue.ToString().Trim();

                            if (dtJournalDetail.Rows.Count > 0)
                            {
                                DataView dv = new DataView(dtJournalDetail);
                                if (dv.Count > 0)
                                {
                                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                                    {
                                        bool bDebit;
                                        bDebit = Convert.ToBoolean(dv[iLoop]["IS_DEBIT"].ToString());
                                        if (bDebit)
                                        {
                                            oFA_ASSETS_DTL.ASSET_LDGR_CODE = dv[iLoop]["LEDGER_CODE"].ToString();
                                            oFA_ASSETS_DTL.DOC_NO = dv[iLoop]["DOC_NO"].ToString();
                                            string strDoc_No = dv[iLoop]["DOC_NO"].ToString();
                                            if (strDoc_No != "")
                                            {
                                                oFA_ASSETS_DTL.DOC_DT = Convert.ToDateTime(dv[iLoop]["DOC_DT"].ToString());
                                            }
                                            oFA_ASSETS_DTL.VOUCHER_AMT = Convert.ToDouble(dv[iLoop]["AMOUNT"].ToString());
                                        }
                                        else
                                        {
                                            oFA_ASSETS_DTL.VENDOR_LDGR_CODE = dv[iLoop]["LEDGER_CODE"].ToString();
                                            oFA_ASSETS_DTL.DOC_NO = dv[iLoop]["DOC_NO"].ToString();
                                            string strDoc_No = dv[iLoop]["DOC_NO"].ToString();
                                            if (strDoc_No != "")
                                            {
                                                oFA_ASSETS_DTL.DOC_DT = Convert.ToDateTime(dv[iLoop]["DOC_DT"].ToString());
                                            }
                                            oFA_ASSETS_DTL.VOUCHER_AMT = Convert.ToDouble(dv[iLoop]["AMOUNT"].ToString());
                                        }
                                    }
                                }
                            }

                            oFA_ASSETS_DTL.VCHR_NO = vou_no;
                            oFA_ASSETS_DTL.JOURNAL_DATE = Convert.ToDateTime(txtJournalDate.Text.Trim());
                            oFA_ASSETS_DTL.DT_OF_MANUFACT = Convert.ToDateTime(txtDTOfManufacturing.Text.Trim());
                            oFA_ASSETS_DTL.DT_OF_PUR = Convert.ToDateTime(txtDTOfPurchase.Text.Trim());
                            oFA_ASSETS_DTL.DT_OF_INSTAL = Convert.ToDateTime(txtDTOfInstall.Text.Trim());
                            oFA_ASSETS_DTL.DT_OF_PUT_IN_USE = Convert.ToDateTime(txtDTOfPutInUse.Text.Trim());
                            oFA_ASSETS_DTL.FORM_DETAIL = txtAssetDescription.Text.Trim();
                            string strContractCode = string.Empty;
                            if (ddlContractCode.Visible == true)
                            {
                                oFA_ASSETS_DTL.CONTRACT_CODE = ddlContractCode.SelectedValue.ToString().Trim();
                            }
                            else
                            {
                                oFA_ASSETS_DTL.CONTRACT_CODE = strContractCode;
                            }

                            if (dtTDSDeduction != null && dtTDSDeduction.Rows.Count > 0)
                            {
                                oFA_ASSETS_DTL.TDS_AMT = GetTDSAmount();
                            }
                            else
                            {
                                oFA_ASSETS_DTL.TDS_AMT = 0;
                            }
                            oFA_ASSETS_DTL.DR_CR_ADJ_AMT = 0;
                            oFA_ASSETS_DTL.TUSER = oUserLoginDetail.UserCode;
                            oFA_ASSETS_DTL.STATUS = true;

                            int iRecordFound = 0;
                            bool bResultTDSTran = SaitexBL.Interface.Method.FA_ASSETS_DTL.InsertAssetsAcquisition(oFA_ASSETS_DTL, oFA_Journal_MST, TDS_VoucherNo, TDS_JournalId, TDS_DESCRIPTION, TDS_VoucherCode, dtJournalDetail, dtTDSDeduction, oFA_TDS_DEDUCT, bTDS, bAssetTran, out iRecordFound);
                            if (bResultTDSTran)
                            {
                                string strMsg = string.Empty;
                                if (bTDS)
                                {
                                    strMsg = "Asset Acquisition Master, Asset Acquisition Detail, Purchase Voucher And TDS Entry Saved Successfully !\\r\\nAnd Your Asset Code is : " + AssetCode + ", Asset Transaction ID : " + AssetTrnID + ", Purchase Voucher Number is: " + vou_no + " And TDS Voucher Number is: " + tds_vou_no;

                                }
                                else
                                {
                                    strMsg = "Asset Acquisition Master, Asset Acquisition Detail, Purchase Voucher Saved Successfully ! \\r\\nAnd Your Asset Code is : " + AssetCode + ", Asset Transaction ID : " + AssetTrnID + ", Purchase Voucher Number is: " + vou_no;
                                }
                                if (strMsg == "")
                                {

                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage(strMsg);
                                }
                                InitialisePage();
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
                            }
                            else if (iRecordFound == 1)
                            {
                                Common.CommonFuction.ShowMessage("This Purchase Voucher is already saved.. Please enter another..");
                            }
                            else if (iRecordFound == 2)
                            {
                                Common.CommonFuction.ShowMessage("This Asset Code OR Asset Detail is already saved.. Please enter another..");
                            }
                            else if (iRecordFound == 3)
                            {
                                Common.CommonFuction.ShowMessage("This TDS Voucher is already saved.. Please enter another..");
                            }
                            else if (iRecordFound == 4)
                            {
                                Common.CommonFuction.ShowMessage("TDS Entry is already saved.. Please enter another..");
                            }
                            else
                            {
                                if (bTDS)
                                {
                                    Common.CommonFuction.ShowMessage("Assets Acquisition Master, Purchase Voucher and TDS Entry Saving failed..");
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Assets Acquisition Master and Purchase Voucher Saving failed..");
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

    private bool CheckValidation()
    {
        try
        {
            bool IsValidation = false;
            if (cmbDetailType.SelectedIndex != -1)
            {
                if (cmbAssetGroup.SelectedIndex != -1)
                {
                    if (ddlVoucherType.Text != "")
                    {
                        if (txtVoucherNo.Text != "")
                        {
                            if (txtJournalDate.Text != "")
                            {
                                if (txtDTOfManufacturing.Text != "")
                                {
                                    if (txtDTOfPurchase.Text != "")
                                    {
                                        if (txtDTOfInstall.Text != "")
                                        {
                                            if (txtDTOfPutInUse.Text != "")
                                            {
                                                if (txtSalDeprAmt.Text != "")
                                                {
                                                    if (txtSalDeprITAmt.Text != "")
                                                    {
                                                        if (txtSaleCompActAmt.Text != "")
                                                        {
                                                            IsValidation = true;
                                                        }
                                                        else
                                                        {
                                                            IsValidation = false;
                                                            Common.CommonFuction.ShowMessage("Dear! Please Enter Sale Company Act Amount..");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        IsValidation = false;
                                                        Common.CommonFuction.ShowMessage("Dear! Please Enter Sale IT Amount..");
                                                    }
                                                }
                                                else
                                                {
                                                    IsValidation = false;
                                                    Common.CommonFuction.ShowMessage("Dear! Please Enter Sale Depreciation Amount..");
                                                }
                                            }
                                            else
                                            {
                                                IsValidation = false;
                                                Common.CommonFuction.ShowMessage("Dear! Please enter Put In Use Date..");
                                            }
                                        }
                                        else
                                        {
                                            IsValidation = false;
                                            Common.CommonFuction.ShowMessage("Dear! Please enter Installation Date..");
                                        }
                                    }
                                    else
                                    {
                                        IsValidation = false;
                                        Common.CommonFuction.ShowMessage("Dear! Please enter Purchase Date..");
                                    }
                                }
                                else
                                {
                                    IsValidation = false;
                                    Common.CommonFuction.ShowMessage("Dear! Please enter Manufacturing Date..");
                                }
                            }
                            else
                            {
                                IsValidation = false;
                                Common.CommonFuction.ShowMessage("Dear! Please enter Purchase Voucher Date..");
                            }
                        }
                        else
                        {
                            IsValidation = false;
                            Common.CommonFuction.ShowMessage("Dear! Please enter Voucher Number..");
                        }
                    }
                    else
                    {
                        IsValidation = false;
                        Common.CommonFuction.ShowMessage("Dear! Please enter Voucher Type..");
                    }
                }
                else
                {
                    IsValidation = false;
                    Common.CommonFuction.ShowMessage("Dear! Please select Asset Group..");
                }
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please enter Detail Type..");
            }
            return IsValidation;
        }
        catch
        {
            return false;
        }
    }

    private bool CheckDuplicateTDS()
    {
        try
        {
            bool IsDuplicate = false;
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

    private string GenerateVoucherNumber()
    {
        try
        {
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.VOUCHER_CODE = VoucherCode;
            string Voucher_No = SaitexBL.Interface.Method.FA_Journal_DTL.GetVoucherNo(oFA_Journal_MST, out JournalId);
            return Voucher_No;
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
                Voucher_No = oFA_Journal_MST1.YEAR.ToString() + DateTime.Now.Date.Month.ToString() + dv[0]["VCHR_PREFIX"].ToString() + strTDSJournalId + dv[0]["VCHR_SUFFIX"].ToString();
            }
            return Voucher_No;
        }
        catch
        {
            throw;
        }
    }

    private string GenerateAssetTranID()
    {
        try
        {
            string strAssetTranID = string.Empty;
            DataTable dt = SaitexDL.Interface.Method.FA_ASSETS_DTL.GetNewAssetTranID(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, cmbAssetCode.SelectedValue.ToString().Trim());
            strAssetTranID = dt.Rows[0]["ASSET_TRN_ID"].ToString();
            return strAssetTranID;
        }
        catch
        {
            throw;
        }
    }

    private double GetTDSAmount()
    {
        try
        {
            double dblTDSAmount = 0;
            Label lblCr_Amount_ftr = (Label)grdTDS.FooterRow.FindControl("lblCr_Amount_ftr");
            dblTDSAmount = Convert.ToDouble(lblCr_Amount_ftr.Text.Trim());
            return dblTDSAmount;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
            ddlVoucherType.ReadOnly = true;
            txtVoucherNo.ReadOnly = true;
            txtVoucherNo.Text = "";
            txtVoucherNo.Visible = true;
            txtVoucherNo.AutoPostBack = true;
            cmbAssetCode.Visible = true;

            BindAssetCodeCombo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //UpdateJournalEntry();
            Common.CommonFuction.ShowMessage("Sorry Dear ! No updation allowed..");
            InitialisePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the Data.\r\nSee error log for detail."));
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
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./AssetDetailMst.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Response.Redirect("./JournalVoucherReport.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
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

    protected void grdJourenaldetails_RowCommand1(object sender, GridViewCommandEventArgs e)
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
        }
    }

    private void DeleteJournalTRNRow(int UNIQUEID)
    {
        try
        {
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
            DataView dv = new DataView(dtJournalDetail);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                RefreshDetailRow();
                ddlLedgerCode.SelectedIndex = 0;

                ListItem lst = ddlEntry_Type.Items.FindByText(dv[0]["ENTRY_TYPE"].ToString());
                lst.Selected = true;

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

    protected void grdAssetsDTL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAssetsDTL.PageIndex = e.NewPageIndex;
            BindAssetsDetail();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
        }
    }

    protected void grdAssetsDTL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string strVou_No = string.Empty;
            string strTDSVoucher = string.Empty;
            string strAssetCode = string.Empty;
            string strAssetTranID = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblVoucherNo = (Label)e.Row.FindControl("lblVoucherNo");
                Label lblTDSVoucherNo = (Label)e.Row.FindControl("lblTDSVoucherNo");
                Label lblAssetCode = (Label)e.Row.FindControl("lblAssetCode");
                Label lblAssetTranID = (Label)e.Row.FindControl("lblAssetTranID");

                strVou_No = lblVoucherNo.Text.Trim();
                strTDSVoucher = lblTDSVoucherNo.Text.Trim();
                strAssetCode = lblAssetCode.Text.Trim();
                strAssetTranID = lblAssetTranID.Text.Trim();

                // Purchase Voucher Master..
                BindJournalTrn(strVou_No);
                GridView grdJourenaldetails = (GridView)e.Row.FindControl("grdJourenaldetails");
                if (dtJournalTrn != null)
                {
                    grdJourenaldetails.DataSource = dtJournalTrn;
                    grdJourenaldetails.DataBind();
                }

                // TDS Voucher Master..
                BindTDSJournalTrn(strTDSVoucher);
                GridView grdTDSJourenaldetails = (GridView)e.Row.FindControl("grdTDSJourenaldetails");
                if (dtTDSVoucher != null)
                {
                    grdTDSJourenaldetails.DataSource = dtTDSVoucher;
                    grdTDSJourenaldetails.DataBind();
                }

                // Assets Detail Master..
                BindAssetJournalTrn(strAssetCode, strAssetTranID);
                GridView grdAssetTranDetail = (GridView)e.Row.FindControl("grdAssetTranDetail");
                if (dtAssets != null)
                {
                    grdAssetTranDetail.DataSource = dtAssets;
                    grdAssetTranDetail.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row DataBound Event.\r\nSee error log for detail."));
        }
    }

    #region For TDS Voucher Grid RowCommand Event.

    /// <summary>
    /// According to Voucher Number Pass the parameters for insertion into datatable..
    /// </summary>
    /// <param name="strVou_No">Voucher Number</param>
    private void BindJournalTrn(string strVou_No)
    {
        try
        {
            string strLedger_Code = string.Empty;
            string strLedger_Name = string.Empty;
            string strDebit = string.Empty;
            double dblAmount = 0;
            string strDoc_No = string.Empty;
            string strDoc_DT;
            string strDesc = string.Empty;

            dtJournalTrn = null;

            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = strVou_No;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectTRNByVoucherNo(oFA_Journal_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        strLedger_Code = dv[iLoop]["LEDGER_CODE"].ToString();
                        strLedger_Name = dv[iLoop]["LDGR_NAME"].ToString();
                        strDebit = dv[iLoop]["IS_DEBIT"].ToString();
                        dblAmount = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                        strDoc_No = dv[iLoop]["DOC_NO"].ToString();
                        strDoc_DT = dv[iLoop]["DOC_DT"].ToString();
                        strDesc = dv[iLoop]["DESCRIPTION"].ToString();

                        InsertJournalTrn(strLedger_Code, strLedger_Name, strDebit, dblAmount, strDoc_No, strDoc_DT, strDesc, strVou_No);
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
    /// Insert into Datatable for Journal Transaction in Hover Menu..
    /// </summary>
    /// <param name="strLedger_Code">Ledger Code</param>
    /// <param name="strLedger_Name">Ledger Name</param>
    /// <param name="strDebit">Check bool for Debit or Credit</param>
    /// <param name="dblAmount">Amount for Dr OR Cr</param>
    /// <param name="strDoc_No">Doc No</param>
    /// <param name="strDoc_DT">Doc Date</param>
    /// <param name="strDesc">Narration</param>
    /// <param name="strVou_No">Voucher Number</param>
    private void InsertJournalTrn(string strLedger_Code, string strLedger_Name, string strDebit, double dblAmount, string strDoc_No, string strDoc_DT, string strDesc, string strVou_No)
    {
        try
        {
            if (dtJournalTrn == null)
                CreateDataTableTrn();

            DataRow dr = dtJournalTrn.NewRow();

            if (strDebit == "1")
            {
                dr["ENTRY_TYPE"] = "Debit";
                dr["DR_AMOUNT"] = dblAmount;
                dr["CR_AMOUNT"] = 0;
            }
            else
            {
                dr["ENTRY_TYPE"] = "Credit";
                dr["CR_AMOUNT"] = dblAmount;
                dr["DR_AMOUNT"] = 0;
            }

            dr["LEDGER_CODE"] = strLedger_Code;
            dr["LDGR_NAME"] = strLedger_Name;
            dr["IS_DEBIT"] = strDebit;
            dr["DOC_NO"] = strDoc_No;
            dr["DOC_DT"] = strDoc_DT;
            dr["DESCRIPTION"] = strDesc;
            dr["VCHR_NO"] = strVou_No;

            dtJournalTrn.Rows.Add(dr);
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Create Datatable for Journal Transaction in Hover Menu..
    /// </summary>
    private void CreateDataTableTrn()
    {
        try
        {
            dtJournalTrn = new DataTable();
            dtJournalTrn.Columns.Add("ENTRY_TYPE", typeof(string));
            dtJournalTrn.Columns.Add("LEDGER_CODE", typeof(string));
            dtJournalTrn.Columns.Add("LDGR_NAME", typeof(string));
            dtJournalTrn.Columns.Add("IS_DEBIT", typeof(string));
            dtJournalTrn.Columns.Add("DR_AMOUNT", typeof(double));
            dtJournalTrn.Columns.Add("CR_AMOUNT", typeof(double));
            dtJournalTrn.Columns.Add("DOC_NO", typeof(string));
            dtJournalTrn.Columns.Add("DOC_DT", typeof(string));
            dtJournalTrn.Columns.Add("DESCRIPTION", typeof(string));
            dtJournalTrn.Columns.Add("VCHR_NO", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region For TDS Voucher Grid RowCommand Event.

    private void BindTDSJournalTrn(string strVou_No)
    {
        try
        {
            string strLedger_Code = string.Empty;
            string strLedger_Name = string.Empty;
            string strDebit = string.Empty;
            double dblAmount = 0;
            string strDoc_No = string.Empty;
            string strDoc_DT;
            string strDesc = string.Empty;

            dtTDSVoucher = null;

            oFA_Journal_MST1 = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST1.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST1.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST1.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST1.VOUCHER_NO = strVou_No;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectTRNByVoucherNo(oFA_Journal_MST1);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        strLedger_Code = dv[iLoop]["LEDGER_CODE"].ToString();
                        strLedger_Name = dv[iLoop]["LDGR_NAME"].ToString();
                        strDebit = dv[iLoop]["IS_DEBIT"].ToString();
                        dblAmount = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                        strDoc_No = dv[iLoop]["DOC_NO"].ToString();
                        strDoc_DT = dv[iLoop]["DOC_DT"].ToString();
                        strDesc = dv[iLoop]["DESCRIPTION"].ToString();

                        InsertTDSJournalTrn(strLedger_Code, strLedger_Name, strDebit, dblAmount, strDoc_No, strDoc_DT, strDesc, strVou_No);
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void InsertTDSJournalTrn(string strLedger_Code, string strLedger_Name, string strDebit, double dblAmount, string strDoc_No, string strDoc_DT, string strDesc, string strVou_No)
    {
        try
        {
            if (dtTDSVoucher == null)
                CreateTDSDataTableTrn();

            DataRow dr = dtTDSVoucher.NewRow();

            if (strDebit == "1")
            {
                dr["ENTRY_TYPE"] = "Debit";
                dr["DR_AMOUNT"] = dblAmount;
                dr["CR_AMOUNT"] = 0;
            }
            else
            {
                dr["ENTRY_TYPE"] = "Credit";
                dr["CR_AMOUNT"] = dblAmount;
                dr["DR_AMOUNT"] = 0;
            }

            dr["LEDGER_CODE"] = strLedger_Code;
            dr["LDGR_NAME"] = strLedger_Name;
            dr["IS_DEBIT"] = strDebit;
            dr["DOC_NO"] = strDoc_No;
            dr["DOC_DT"] = strDoc_DT;
            dr["DESCRIPTION"] = strDesc;
            dr["VCHR_NO"] = strVou_No;

            dtTDSVoucher.Rows.Add(dr);
        }
        catch
        {
            throw;
        }
    }

    private void CreateTDSDataTableTrn()
    {
        try
        {
            dtTDSVoucher = new DataTable();
            dtTDSVoucher.Columns.Add("ENTRY_TYPE", typeof(string));
            dtTDSVoucher.Columns.Add("LEDGER_CODE", typeof(string));
            dtTDSVoucher.Columns.Add("LDGR_NAME", typeof(string));
            dtTDSVoucher.Columns.Add("IS_DEBIT", typeof(string));
            dtTDSVoucher.Columns.Add("DR_AMOUNT", typeof(double));
            dtTDSVoucher.Columns.Add("CR_AMOUNT", typeof(double));
            dtTDSVoucher.Columns.Add("DOC_NO", typeof(string));
            dtTDSVoucher.Columns.Add("DOC_DT", typeof(string));
            dtTDSVoucher.Columns.Add("DESCRIPTION", typeof(string));
            dtTDSVoucher.Columns.Add("VCHR_NO", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region For Assets Details Grid RowCommand Event.

    private void BindAssetJournalTrn(string AssetCode, string AssetTranID)
    {
        try
        {
            if (dtAssets != null)
            {
                dtAssets.Clear();
            }
            DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_DTL.GetAssetDetailByAssetCodeAndTranID(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, AssetCode, AssetTranID);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dtAssets == null)
                    CreateAssetDTL();

                DataRow dr = dtAssets.NewRow();

                dr["ASSET_CODE"] = dt.Rows[0]["ASSET_CODE"].ToString();
                dr["ASSET_TRN_ID"] = dt.Rows[0]["ASSET_TRN_ID"].ToString();

                double dblSalDeprAmt = 0;
                double.TryParse(dt.Rows[0]["SAL_DEPR_AMT"].ToString(), out dblSalDeprAmt);
                dr["SAL_DEPR_AMT"] = dblSalDeprAmt;

                double dblSalITAmt = 0;
                double.TryParse(dt.Rows[0]["SAL_IT_AMT"].ToString(), out dblSalITAmt);
                dr["SAL_IT_AMT"] = dblSalITAmt;

                double dblCompActAmt = 0;
                double.TryParse(dt.Rows[0]["SAL_COMP_ACT_AMT"].ToString(), out dblCompActAmt);
                dr["SAL_COMP_ACT_AMT"] = dblCompActAmt;

                dr["DT_OF_MANUFACT"] = dt.Rows[0]["DT_OF_MANUFACT"].ToString();
                dr["DT_OF_PUR"] = dt.Rows[0]["DT_OF_PUR"].ToString();
                dr["DT_OF_INSTAL"] = dt.Rows[0]["DT_OF_INSTAL"].ToString();
                dr["DT_OF_PUT_IN_USE"] = dt.Rows[0]["DT_OF_PUT_IN_USE"].ToString();

                dtAssets.Rows.Add(dr);
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Details found against this Asset Code And Asset Tran ID..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void CreateAssetDTL()
    {
        try
        {
            dtAssets = new DataTable();
            dtAssets.Columns.Add("ASSET_CODE", typeof(string));
            dtAssets.Columns.Add("ASSET_TRN_ID", typeof(string));
            dtAssets.Columns.Add("SAL_DEPR_AMT", typeof(double));
            dtAssets.Columns.Add("SAL_IT_AMT", typeof(double));
            dtAssets.Columns.Add("SAL_COMP_ACT_AMT", typeof(double));
            dtAssets.Columns.Add("DT_OF_MANUFACT", typeof(string));
            dtAssets.Columns.Add("DT_OF_PUR", typeof(string));
            dtAssets.Columns.Add("DT_OF_INSTAL", typeof(string));
            dtAssets.Columns.Add("DT_OF_PUT_IN_USE", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    #endregion

    /// <summary>
    /// Here, We are binding the Gridview, According to Assets Code..
    /// </summary>
    private void BindAssetsDetail()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_ASSETS_DTL.GetAssetDetailByAssetCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, cmbAssetCode.SelectedValue.ToString().Trim());
            if (dt != null && dt.Rows.Count > 0)
            {
                grdAssetsDTL.DataSource = dt;
                grdAssetsDTL.DataBind();
            }
            else
            {
                grdAssetsDTL.DataSource = null;
                grdAssetsDTL.DataBind();
                CommonFuction.ShowMessage("No Record found, against this Asset Code..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindDeptCombo()
    {
        try
        {
            cmbDepartment.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbDepartment.DataSource = dt;
                cmbDepartment.DataValueField = "DEPT_CODE";
                cmbDepartment.DataTextField = "DEPT_NAME";
                cmbDepartment.DataBind();
                cmbDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
            }
            else
            {
                Common.CommonFuction.ShowMessage("Department details are not found..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindBranchCombo()
    {
        try
        {
            cmbLocation.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbLocation.DataSource = dt;
                cmbLocation.DataValueField = "BRANCH_CODE";
                cmbLocation.DataTextField = "BRANCH_NAME";
                cmbLocation.DataBind();
                cmbLocation.Items.Insert(0, new ListItem("---- Select Branch ----", "0"));
            }
            else
            {
                Common.CommonFuction.ShowMessage("Company branches are not found..");
            }
        }
        catch
        {
            throw;
        }
    }
}