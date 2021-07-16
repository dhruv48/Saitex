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

using Common;

public partial class Module_FA_Controls_DrCrVoucher : System.Web.UI.UserControl
{
    public string JournalId = string.Empty;
    private DataTable dtJournalDetail;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST;
    private static string strType = string.Empty;
    private static string strTypeName = string.Empty;
    public static double dblBillAmt;
    public static string LedgerCode = string.Empty;
    public static string Branch = string.Empty;
    public static string BillType = string.Empty;
    public static int BillYear;
    public static string BillNumb;
    public static string BillDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                if (Request.QueryString["strType"] != null && Request.QueryString["strType"].ToString() != "")
                {
                    strType = Request.QueryString["strType"].ToString().Trim();
                }

                InitialisePage();

                if (Request.QueryString["LedgerCode"] != null && Request.QueryString["LedgerCode"].ToString() != "" && Request.QueryString["BillAmt"] != null && Request.QueryString["BillAmt"].ToString() != "")
                {
                    dblBillAmt = 0;
                    LedgerCode = Request.QueryString["LedgerCode"].ToString().Trim();
                    double.TryParse(Request.QueryString["BillAmt"].ToString().Trim(), out dblBillAmt);
                    Branch = Request.QueryString["Branch"].ToString().Trim();
                    BillYear = int.Parse(Request.QueryString["BillYear"].ToString().Trim());
                    BillType = Request.QueryString["BillType"].ToString().Trim();
                    BillNumb = Request.QueryString["BillNumb"].ToString().Trim();
                    BillDate = Request.QueryString["BillDate"].ToString().Trim();
                    SetDataForVouching(LedgerCode, dblBillAmt);
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
            trTools.Visible = false;

            if (strType == "D")
            {
                lblHeading.Text = "Debit Note";
                strTypeName = "DEBIT NOTE";
                ddlVoucherType.SelectedValue = "3";
            }
            else
            {
                lblHeading.Text = "Credit Note";
                strTypeName = "CREDIT NOTE";
                ddlVoucherType.SelectedValue = "2";
            }

            BlankControls();
            txtVoucherNo.Text = GetVoucherNo();
            if (txtJournalDate.Text == "")
                txtJournalDate.Text = System.DateTime.Now.Date.ToShortDateString();

            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            txtVoucherNo.AutoPostBack = false;
            ddlVoucherType.Enabled = true;
            txtVoucherNo.ReadOnly = true;
            txtVoucherNo.Text = string.Empty;
            RefreshDetailRow();
            BindVoucherType();
            BindLedgerCode();
            FillVoucherNo();
            ddlVoucherType.Enabled = false;
        }
        catch
        {
            throw;
        }
    }

    private void SetDataForVouching(string LedgerCode, double dblBillAmt)
    {
        try
        {
            trClose.Visible = true;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail == null)
                CreateDataTable();

            if (strType == "D")
            {
                DataRow dr = dtJournalDetail.NewRow();
                dr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
                dr["ENTRY_TYPE"] = "Dr";
                dr["LEDGER_CODE"] = LedgerCode;

                DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMasterWithCode(LedgerCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dr["LEDGER_NAME"] = dt.Rows[0]["LDGR_NAME"].ToString().Trim();
                }

                dr["IS_DEBIT"] = true;
                dr["AMOUNT"] = dblBillAmt;
                dr["DOC_NO"] = BillNumb;
                dr["DOC_DT"] = BillDate;
                dr["DR_AMOUNT"] = dblBillAmt;
                dr["DESC"] = "Debit Entry of Debit Note, against Bill No: " + BillNumb + ", and Bill Date: " + BillDate;
                dtJournalDetail.Rows.Add(dr);

                DataRow cr = dtJournalDetail.NewRow();
                cr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
                cr["ENTRY_TYPE"] = "Cr";
                cr["LEDGER_CODE"] = "660";
                cr["LEDGER_NAME"] = "PURCHASE RETURN A/C";
                cr["IS_DEBIT"] = false;
                cr["AMOUNT"] = dblBillAmt;
                cr["DOC_NO"] = BillNumb;
                cr["DOC_DT"] = BillDate;
                cr["CR_AMOUNT"] = dblBillAmt;
                cr["DESC"] = "Credit Entry of Debit Note, against Bill No: " + BillNumb + ", and Bill Date: " + BillDate;
                dtJournalDetail.Rows.Add(cr);

                grdJourenaldetails.DataSource = dtJournalDetail;
                grdJourenaldetails.DataBind();
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
            }
            else
            {
                DataRow dr = dtJournalDetail.NewRow();
                dr["UNIQUE_ID"] = dtJournalDetail.Rows.Count + 1;
                dr["ENTRY_TYPE"] = "Dr";
                dr["LEDGER_CODE"] = "11";
                dr["LEDGER_NAME"] = "PURCHASE A/C";
                dr["IS_DEBIT"] = true;
                dr["AMOUNT"] = dblBillAmt;
                dr["DOC_NO"] = BillNumb;
                dr["DOC_DT"] = BillDate;
                dr["DR_AMOUNT"] = dblBillAmt;
                dr["DESC"] = "Debit Entry of Credit Note, against Bill No: " + BillNumb + ", and Bill Date: " + BillDate;
                dtJournalDetail.Rows.Add(dr);

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
                cr["DESC"] = "Credit Entry of Credit Note, against Bill No: " + BillNumb + ", and Bill Date: " + BillDate;
                dtJournalDetail.Rows.Add(cr);

                grdJourenaldetails.DataSource = dtJournalDetail;
                grdJourenaldetails.DataBind();
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
            }

            ViewState["dtJournalDetail"] = dtJournalDetail;
        }
        catch
        {
            throw;
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
            }
            ViewState["dtJournalDetail"] = dtJournalDetail;
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

    private void BindVoucherType()
    {
        try
        {
            ddlVoucherType.Items.Clear();
            DataTable data = new DataTable();
            data = GetDataForVoucherType("");
            if (data != null && data.Rows.Count > 0)
            {
                ddlVoucherType.DataSource = data;
                ddlVoucherType.DataTextField = "VCHR_NAME";
                ddlVoucherType.DataValueField = "VCHR_CODE";
                ddlVoucherType.DataBind();
                ddlVoucherType.Items.Insert(0, new ListItem("--- Select Voucher Type ---", "0"));
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

    private void FillVoucherNo()
    {
        try
        {
            string voucher_code = ddlVoucherType.SelectedValue.Trim();
            //string voucher_code = "6";

            SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.VOUCHER_CODE = voucher_code;
            string Voucher_No = SaitexBL.Interface.Method.FA_Journal_DTL.GetVoucherNo(oFA_Journal_MST, out JournalId);
            ViewState["JournalId"] = JournalId;
            txtVoucherNo.Text = Voucher_No;
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
            ddlLedgerCode.SelectedIndex = -1;
            txtTranDescription.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            txtDocDT.Text = string.Empty;
            txtCreditAmount.Text = "0";
            txtDebitAmount.Text = "0";
            ViewState["UNIQUE_ID"] = null;
            ddlEntry_Type.Focus();
            txtDebitAmount.ReadOnly = (ddlEntry_Type.SelectedValue.Trim() == "Dr");
            txtCreditAmount.ReadOnly = (ddlEntry_Type.SelectedValue.Trim() == "Cr");
            SetEntryType();
            lblMessage.Text = string.Empty;
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

    private void SetEditDetailByVoucherNo(string Voucher_No)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
            ddlVoucherType.Enabled = false;
            txtVoucherNo.ReadOnly = false;
            txtVoucherNo.Text = string.Empty;
            ddlVoucherNo.Visible = true;
            txtVoucherNo.Visible = false;
            txtVoucherNo.AutoPostBack = true;
            RefreshDetailRow();
            FillEditDataByVoucherNo(Voucher_No);
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
            SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = Voucher_No;
            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectByVoucherNo(oFA_Journal_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlVoucherType.Items.Clear();
                DataTable dtVT = GetDataForVoucherType("");

                ddlVoucherType.DataSource = dtVT;
                ddlVoucherType.DataTextField = "VCHR_NAME";
                ddlVoucherType.DataValueField = "VCHR_CODE";
                ddlVoucherType.DataBind();

                ddlVoucherType.SelectedValue = dt.Rows[0]["VCHR_CODE"].ToString();
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

                dtJournalDetail = null;
                grdJourenaldetails.DataSource = dtJournalDetail;
                grdJourenaldetails.DataBind();

                RefreshDetailRow();
                txtVoucherNo.Text = "";
                txtDescription.Text = "";
                Common.CommonFuction.ShowMessage("Sorry Dear! This voucher has been comfirmed, you cannot make any change.");
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
            string whereClause = " WHERE VCHR_NO like :SearchQuery OR VCHR_NAME LIKE :SearchQuery ";
            string sortExpression = " ORDER BY VCHR_NO";
            string commandText = "SELECT * FROM (SELECT * FROM V_FA_JOURNAL_MST WHERE COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND VCHR_CODE = '6' ) ASD";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", Text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetDataForVoucherType(string Text)
    {
        try
        {
            string whereClause = " WHERE VCHR_CODE like :SearchQuery OR VCHR_NAME LIKE :SearchQuery ";
            string sortExpression = " ORDER BY VCHR_CODE";
            string commandText = "SELECT * FROM V_FA_VCHR_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", Text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string voucher_code = ddlVoucherType.SelectedValue.Trim();

            SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.VOUCHER_CODE = voucher_code;
            string Voucher_No = SaitexBL.Interface.Method.FA_Journal_DTL.GetVoucherNo(oFA_Journal_MST, out JournalId);
            ViewState["JournalId"] = JournalId;
            txtVoucherNo.Text = Voucher_No;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting voucher type.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            if (ddlVoucherType.SelectedIndex > -1)
            {
                if (ddlLedgerCode.SelectedIndex > -1)
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
                                    string spaces = string.Empty;

                                    if (UNIQUE_ID > 0)
                                    {
                                        DataView dvEdit = new DataView(dtJournalDetail);
                                        dvEdit.RowFilter = "LEDGER_CODE='" + Ledger_Code + "' and UNIQUE_ID=" + UNIQUE_ID;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();

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
            string vou_no = string.Empty;
            double Debit_Total = 0;
            double Credit_Total = 0;

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString();

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

                CalculateDebitCreditTotal(out Debit_Total, out Credit_Total);
                if (Debit_Total == Credit_Total)
                {
                    oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                    oFA_Journal_MST.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                    oFA_Journal_MST.VOUCHER_CODE = ddlVoucherType.SelectedValue.Trim();
                    oFA_Journal_MST.VOUCHER_NO = GenerateVoucherNumber();
                    oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                    oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                    oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                    oFA_Journal_MST.STATUS = true;
                    vou_no = GenerateVoucherNumber();

                    int iRecordFound = 0;
                    bool bResultTDSTran = SaitexBL.Interface.Method.FA_Journal_DTL.InsertJV(oFA_Journal_MST, dtJournalDetail, out iRecordFound);
                    if (bResultTDSTran)
                    {
                        if (dtJournalDetail != null)
                        {
                            dtJournalDetail.Rows.Clear();
                            grdJourenaldetails.DataBind();
                        }
                        ViewState["dtJournalDetail"] = dtJournalDetail;

                        if (ViewState["JournalId"] != null)
                        {
                            JournalId = null;
                        }
                        ViewState["JournalId"] = JournalId;

                        Common.CommonFuction.ShowMessage("Journal Entry Saved Successfully! \\r\\n And Your Journal Voucher Number is: " + vou_no);
                        InitialisePage();
                        txtDescription.Text = string.Empty;
                        //ddlVoucherType.SelectedIndex = 0;
                    }
                    else if (iRecordFound == 1)
                    {
                        Common.CommonFuction.ShowMessage("This Voucher is already saved.. Please enter another..");
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Journal Entry Saving failed..");
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
            string voucher_code = ddlVoucherType.SelectedValue.Trim();

            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.VOUCHER_CODE = voucher_code;
            string Voucher_No = SaitexBL.Interface.Method.FA_Journal_DTL.GetVoucherNo(oFA_Journal_MST, out JournalId);
            ViewState["JournalId"] = JournalId;
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
                    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
                    oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                    oFA_Journal_MST.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                    oFA_Journal_MST.VOUCHER_CODE = ddlVoucherType.SelectedValue.Trim();
                    oFA_Journal_MST.VOUCHER_NO = txtVoucherNo.Text.Trim();
                    oFA_Journal_MST.DESCRIPTION = txtTranDescription.Text.Trim();
                    oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                    oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                    oFA_Journal_MST.STATUS = true;

                    bool bResult = SaitexBL.Interface.Method.FA_Journal_DTL.Update(oFA_Journal_MST, dtJournalDetail);
                    if (bResult)
                    {
                        if (dtJournalDetail != null)
                        {
                            dtJournalDetail.Rows.Clear();
                            grdJourenaldetails.DataBind();
                        }
                        ViewState["dtJournalDetail"] = dtJournalDetail;

                        if (ViewState["JournalId"] != null)
                        {
                            JournalId = null;
                        }
                        ViewState["JournalId"] = JournalId;

                        Common.CommonFuction.ShowMessage("Journal Entry Updated Successfully");
                        InitialisePage();
                        txtDescription.Text = string.Empty;
                        //ddlVoucherType.SelectedIndex = -1;
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Journal Entry updation failed.");
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
            ddlVoucherType.Enabled = false;
            txtVoucherNo.ReadOnly = false;
            txtVoucherNo.Text = string.Empty;
            ddlVoucherNo.Visible = true;
            txtVoucherNo.Visible = false;
            txtVoucherNo.AutoPostBack = true;
            RefreshDetailRow();
            BindVoucherCodeDDL();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindVoucherCodeDDL()
    {
        try
        {
            ddlVoucherNo.Items.Clear();
            DataTable dt = GetDataForVouchernO(string.Empty);
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
            Response.Redirect("./JournalEntryForm.aspx", false);
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
            Response.Redirect("./JournalVoucherReport.aspx", false);
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
                ddlLedgerCode.Items.Clear();
                ddlLedgerCode.DataSource = null;
                ddlLedgerCode.DataBind();
                DataTable data = new DataTable();
                data = GetItems("", 0, 10);
                ddlLedgerCode.DataSource = data;
                ddlLedgerCode.DataTextField = "LDGR_NAME";
                ddlLedgerCode.DataValueField = "LDGR_CODE";
                ddlLedgerCode.DataBind();
                RefreshDetailRow();

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
            //  SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Voucher Number.\r\nSee error log for detail."));
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
            BindLedgerCode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger Popup Text Changed Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            SaveDrCrVoucher();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue()", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Closing..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void SaveDrCrVoucher()
    {
        try
        {
            string vou_no = string.Empty;
            double Debit_Total = 0;
            double Credit_Total = 0;

            if (ViewState["dtJournalDetail"] != null)
                dtJournalDetail = (DataTable)ViewState["dtJournalDetail"];

            if (ViewState["JournalId"] != null)
                JournalId = ViewState["JournalId"].ToString();

            if (dtJournalDetail != null && dtJournalDetail.Rows.Count > 0)
            {
                oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

                CalculateDebitCreditTotal(out Debit_Total, out Credit_Total);
                if (Debit_Total == Credit_Total)
                {
                    if (strType == "D")
                    {
                        oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                        oFA_Journal_MST.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                        oFA_Journal_MST.VOUCHER_CODE = "3";
                        oFA_Journal_MST.VOUCHER_NO = GenerateVoucherNumber();
                        oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                        oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                        oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                        oFA_Journal_MST.STATUS = true;
                        vou_no = GenerateVoucherNumber();
                    }
                    else
                    {
                        oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        oFA_Journal_MST.JOURNAL_ID = int.Parse(JournalId);
                        oFA_Journal_MST.JOURNAL_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                        oFA_Journal_MST.VOUCHER_CODE = "2";
                        oFA_Journal_MST.VOUCHER_NO = GenerateVoucherNumber();
                        oFA_Journal_MST.DESCRIPTION = txtDescription.Text.Trim();
                        oFA_Journal_MST.TRN_DATE = DateTime.Parse(txtJournalDate.Text.Trim());
                        oFA_Journal_MST.TUSER = oUserLoginDetail.UserCode;
                        oFA_Journal_MST.STATUS = true;
                        vou_no = GenerateVoucherNumber();
                    }

                    int iRecordFound = 0;
                    bool bResultTDSTran = SaitexBL.Interface.Method.FA_Journal_DTL.InsertDrCrVoucher(oFA_Journal_MST, dtJournalDetail, strType, Branch, BillYear, BillType, BillNumb, LedgerCode, out iRecordFound);
                    if (bResultTDSTran)
                    {
                        string strMsg = string.Empty;
                        if (strType == "D")
                        {
                            strMsg = "Debit Note Voucher Saved Successfully !\\r\\nYour Debit Note Voucher Number is: " + vou_no;
                        }
                        else
                        {
                            strMsg = "Credit Note Voucher Saved Successfully !\\r\\nYour Credit Note Voucher Number is: " + vou_no;
                        }

                        CommonFuction.ShowMessage(strMsg);

                        if (dtJournalDetail != null)
                        {
                            dtJournalDetail.Rows.Clear();
                            grdJourenaldetails.DataBind();
                        }

                        InitialisePage();
                        txtDescription.Text = string.Empty;
                    }
                    else
                    {
                        if (strType == "D")
                        {
                            Common.CommonFuction.ShowMessage("Error!!! in Debit Note Saving");
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Error!!! in Credit Note Saving");
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
        catch
        {
            throw;
        }
    }
}