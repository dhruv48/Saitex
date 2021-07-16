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

public partial class Module_FA_Controls_CashPayment : System.Web.UI.UserControl
{
    public string JournalId = string.Empty;
    public string VoucherCode = "8";     // For Payment Voucher Fixed
    private DataTable dtJournalDetail;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST, oFA_Journal_MST1;

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
            if (txtJournalDate.Text == "")
                txtJournalDate.Text = System.DateTime.Now.Date.ToShortDateString();

            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            ddlVoucherNo.Visible = false;

            txtVoucherNo.AutoPostBack = false;

            txtPaymentVoucher.ReadOnly = true;
            ddlLedgerCode.Enabled = true;
            ddlEntry_Type.Enabled = false;
            txtVoucherNo.ReadOnly = true;
            txtVoucherNo.Text = string.Empty;

            bindPaymentVoucher();
            bindCashLedgerOnly();
            bindLedgers();
        }
        catch
        {
            throw;
        }
    }

    private void bindPaymentVoucher()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "VCHR_NAME='" + "CASH PAYMENT" + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtPaymentVoucher.Text = dv[iLoop]["VCHR_NAME"].ToString();
                    }
                }
            }

            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
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

    private void bindCashLedgerOnly()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetItems("", 0, 10, 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlPaymentLedger.Items.Clear();
                ddlPaymentLedger.DataSource = dt;
                ddlPaymentLedger.DataValueField = "LDGR_CODE";
                ddlPaymentLedger.DataTextField = "LDGR_NAME";
                ddlPaymentLedger.DataBind();
                ddlPaymentLedger.Items.Insert(0, new ListItem("------- Select Payment Ledger ------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindLedgers()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetItems("", 0, 10, 2);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlLedgerCode.Items.Clear();
                ddlLedgerCode.DataSource = dt;
                ddlLedgerCode.DataValueField = "LDGR_CODE";
                ddlLedgerCode.DataTextField = "LDGR_NAME";
                ddlLedgerCode.DataBind();
                ddlLedgerCode.Items.Insert(0, new ListItem("-------- Select Ledger Name -------", "0"));
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
            if (icond == 1)        // For Payment Ledger Code Group Code 17 Cash Only..
            {
                string whereClause = " WHERE LDGR_CODE like :SearchQuery or LDGR_NAME like :SearchQuery";
                string sortExpression = " ORDER BY LDGR_NAME";
                string commandText = "select * from (select * from (SELECT * FROM FA_LGR_MST WHERE DEL_STATUS = '0') WHERE GRP_CODE = '17' ) ";
                string sPO = "";
                DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
                return dt;
            }
            else if (icond == 2)             // For All Ledger Code except BANK OR CASH Ledgers
            {
                string whereClause = " WHERE LDGR_CODE like :SearchQuery or LDGR_NAME like :SearchQuery";
                string sortExpression = " ORDER BY LDGR_NAME";
                string commandText = "select * from (select * from (SELECT * FROM FA_LGR_MST WHERE DEL_STATUS = '0') WHERE GRP_CODE <> '16' And GRP_CODE <> '17' ) ";
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
            if (icond == 1)       // For Payment Ledger Code Group Code 17 Cash Only..
            {
                string CommandText = "SELECT COUNT(*) FROM FA_LGR_MST WHERE GRP_CODE = '17' and LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_CODE = '17' and LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
                return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
            }
            else if (icond == 2)             // For Ledger Code except select ledger code such as BANK OR CASH
            {
                string CommandText = "SELECT COUNT(*) FROM FA_LGR_MST WHERE GRP_CODE <> '16' or GRP_CODE <> '17' and LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_CODE <> '16' or GRP_CODE <> '17' and LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
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

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDocNo.Text != "" && txtDocDT.Text == "")
            {
                Common.CommonFuction.ShowMessage("Please enter doc date.");
            }
            else
            {
                if (ddlLedgerCode.SelectedIndex > 0)
                {
                    if (ViewState["dtJournalDetail"] != null)
                        dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

                    if (dtJournalDetail == null)
                        CreateDataTable();

                    string Entry_type = ddlEntry_Type.SelectedItem.Text.Trim();
                    string Ledger_Code = ddlLedgerCode.SelectedValue.ToString().Trim();
                    string Ledger_Name = ddlLedgerCode.SelectedItem.ToString().Trim();
                    string Doc_No = txtDocNo.Text.ToUpper().Trim();
                    string Doc_Dt = txtDocDT.Text.Trim();

                    double Debit_Amount = 0;
                    double.TryParse(txtAmount.Text.Trim(), out Debit_Amount);

                    double Amount = 0;
                    if (Debit_Amount > 0)
                        Amount = Debit_Amount;
                    else
                        Amount = 0;

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

                            if (Entry_type == "Cr")
                                spaces = "&nbsp;&nbsp;&nbsp;";
                            if (UNIQUE_ID > 0)
                            {
                                DataView dvEdit = new DataView(dtJournalDetail);
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
                                dr["ENTRY_TYPE"] = spaces + Entry_type;
                                dr["LEDGER_CODE"] = Ledger_Code;
                                dr["LEDGER_NAME"] = Ledger_Name;
                                dr["IS_DEBIT"] = IsDebit;
                                dr["AMOUNT"] = Amount;
                                dr["DOC_NO"] = Doc_No;
                                dr["DOC_DT"] = Doc_Dt;

                                if (Debit_Amount > 0)
                                    dr["DR_AMOUNT"] = Debit_Amount;

                                dr["DESC"] = Desc;
                                dtJournalDetail.Rows.Add(dr);
                            }
                            RefreshDetailRow();
                            ddlLedgerCode.SelectedIndex = 0;
                        }
                        ViewState["dtJournalDetail"] = dtJournalDetail;
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please enter amount first.");
                        if (txtAmount.ReadOnly == false)
                            txtAmount.Focus();
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please select Ledger first.");
                }
            }
            BindGridFromTable();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Transaction ..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancellation..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the Data..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding..\r\nSee error log for detail."));
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
            Response.Redirect("./CashPaymentEntry.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the Data..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
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

    protected void txtVoucherNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //FillEditDataByVoucherNo(txtVoucherNo.Text.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Voucher number TextChanged Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            BindGridFromTable();
            lblMessage.Text = "";
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
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            txtTranDescription.Text = "";
            txtDocNo.Text = "";
            txtDocDT.Text = "";
            txtAmount.Text = "0";
            ViewState["UNIQUE_ID"] = null;
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
            {
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
            }

            if (dtJournalDetail != null)
            {
                DataView dv = new DataView(dtJournalDetail);
                dtJournalDetail.AcceptChanges();
            }
            grdPaymentVoucher.DataSource = dtJournalDetail;
            grdPaymentVoucher.DataBind();

            if (dtJournalDetail != null)
            {
                double Debit_Total = 0;
                CalculateDebitCreditTotal(out Debit_Total);

                Label lblDr_Amount_ftr = (Label)grdPaymentVoucher.FooterRow.FindControl("lblDr_Amount_ftr");
                lblDr_Amount_ftr.Text = Debit_Total.ToString();
                if (Debit_Total > 0)
                {
                    imgbtnNew.Enabled = true;
                    imgbtnUpdate.Enabled = true;
                }
                else
                {
                    imgbtnNew.Enabled = false;
                    imgbtnUpdate.Enabled = false;
                }
            }
        }
        catch
        {
            throw;
        }
    }

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

    protected void grdPaymentVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid RowCommand..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                ddlLedgerCode.SelectedIndex = ddlLedgerCode.Items.IndexOf(ddlLedgerCode.Items.FindByValue(dv[0]["LEDGER_CODE"].ToString()));
                txtAmount.Text = dv[0]["DR_AMOUNT"].ToString();
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

    private void SaveJournalEntry()
    {
        try
        {
            double Debit_Total = 0;
            string vou_no = string.Empty;

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString();

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

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
                oFA_Journal_MST.VOUCHER_NO = GenerateVoucherNumber();
                oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                oFA_Journal_MST.STATUS = true;
                vou_no = GenerateVoucherNumber();

                addCreditJournalDetail();

                bool bResult = SaitexBL.Interface.Method.FA_Journal_DTL.Insert(oFA_Journal_MST, dtJournalDetail);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage(@"Cash Payment Entry Saved Successfully! \n And Your Cash Payment Voucher Number is: " + vou_no);
                    if (ViewState["dtJournalDetail"] != null)
                    {
                        dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
                        dtJournalDetail = null;
                        grdPaymentVoucher.DataSource = dtJournalDetail;
                        grdPaymentVoucher.DataBind();
                    }
                    ViewState["dtJournalDetail"] = dtJournalDetail;
                    if (ViewState["JournalId"] != null)
                    {
                        JournalId = null;
                    }
                    ViewState["JournalId"] = JournalId;
                    InitialisePage();
                    ddlLedgerCode.SelectedIndex = 0;
                    ddlPaymentLedger.SelectedIndex = 0;
                    txtDescription.Text = string.Empty;
                    txtTranDescription.Text = string.Empty;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Cash Payment Entry Saving failed.");
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
            oFA_Journal_MST1 = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST1.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST1.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST1.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST1.VOUCHER_CODE = VoucherCode;
            string Voucher_No = SaitexBL.Interface.Method.FA_Journal_DTL.GetVoucherNo(oFA_Journal_MST1, out JournalId);
            ViewState["JournalId"] = JournalId;
            return Voucher_No;
        }
        catch
        {
            throw;
        }
    }

    private void addCreditJournalDetail()
    {
        try
        {
            double Amt = 0;
            string Desc = "";
            string spaces;
            string Entry_type = "Cr";
            string Doc_No = txtDocNo.Text.ToUpper().Trim();
            string Doc_Dt = txtDocDT.Text.Trim();

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in dtJournalDetail.Rows)
                {
                    Amt = Amt + double.Parse(dr["AMOUNT"].ToString());
                    Desc = dr["DESC"].ToString();
                }
            }

            spaces = "&nbsp;&nbsp;&nbsp;";
            DataRow dr1 = dtJournalDetail.NewRow();
            dr1["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
            dr1["ENTRY_TYPE"] = spaces + Entry_type;
            dr1["LEDGER_CODE"] = ddlPaymentLedger.SelectedValue.ToString().Trim();
            dr1["LEDGER_NAME"] = ddlPaymentLedger.SelectedItem.ToString().Trim();
            dr1["IS_DEBIT"] = false;
            dr1["AMOUNT"] = Amt;
            dr1["CR_AMOUNT"] = Amt;
            dr1["DESC"] = Desc;
            dr1["DOC_NO"] = Doc_No;
            dr1["DOC_DT"] = Doc_Dt;
            dtJournalDetail.Rows.Add(dr1);

            ViewState["dtJournalDetail"] = dtJournalDetail;
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
            string whereClause = " WHERE VCHR_CODE = '8' And VCHR_NO like :SearchQuery OR VCHR_CODE = '8' AND VCHR_NAME LIKE :SearchQuery";
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
                bindPaymentVoucher();
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
                if (ViewState["dtJournalDetail"] != null)
                {
                    dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
                    dtJournalDetail = null;
                }

                grdPaymentVoucher.DataSource = dtJournalDetail;
                grdPaymentVoucher.DataBind();

                RefreshDetailRow();
                ddlLedgerCode.SelectedIndex = 0;
                txtVoucherNo.Text = string.Empty;
                txtDescription.Text = string.Empty;
                Common.CommonFuction.ShowMessage("Sorry Dear! This voucher has been comfirmed, you cannot make any change..");
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

                    drNew["LEDGER_CODE"] = dr["LEDGER_CODE"];
                    drNew["LEDGER_NAME"] = dr["LDGR_NAME"];
                    drNew["IS_DEBIT"] = IsDebit;

                    drNew["AMOUNT"] = dr["AMOUNT"];
                    drNew["DESC"] = dr["DESCRIPTION"];
                    drNew["DOC_NO"] = dr["DOC_NO"];
                    drNew["DOC_DT"] = dr["DOC_DT"];
                    dtJournalDetail.Rows.Add(drNew);
                }
                else
                {
                    ddlPaymentLedger.SelectedIndex = ddlPaymentLedger.Items.IndexOf(ddlPaymentLedger.Items.FindByValue(dr["LEDGER_CODE"].ToString()));
                }
            }
            ViewState["dtJournalDetail"] = dtJournalDetail;
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

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString();

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

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
                        dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];
                        dtJournalDetail = null;
                        grdPaymentVoucher.DataSource = dtJournalDetail;
                        grdPaymentVoucher.DataBind();
                    }
                    ViewState["dtJournalDetail"] = dtJournalDetail;

                    if (ViewState["JournalId"] != null)
                    {
                        JournalId = null;
                    }
                    ViewState["JournalId"] = JournalId;

                    Common.CommonFuction.ShowMessage("Cash Payment Entry Updated Successfully!");
                    InitialisePage();
                    ddlLedgerCode.SelectedIndex = 0;
                    ddlPaymentLedger.SelectedIndex = 0;
                    txtDescription.Text = string.Empty;
                    txtTranDescription.Text = string.Empty;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Cash Payment Entry updation failed.");
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

    protected void ddlLedgerCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int unique_id = 0;
            if (ViewState["UNIQUE_ID"] != null)
                unique_id = int.Parse(ViewState["UNIQUE_ID"].ToString());

            if (!CheckDuplicateRow(ddlLedgerCode.SelectedValue.Trim(), unique_id))
            {
                txtAmount.Text = "";
                txtAmount.Focus();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting LedgerCode..\r\nSee error log for detail."));
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
            bindLedgers();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger Popup Text Changed Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}