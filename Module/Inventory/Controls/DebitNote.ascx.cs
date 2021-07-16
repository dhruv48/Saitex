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

using SaitexDM.Common.DataModel;
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Inventory_Controls_DebitNote : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    TX_DEBIT_MST oTX_DEBIT_MST;
    private static string strType = string.Empty;
    private static string strTypeName = string.Empty;
    private DataTable dtTRNDetail;
    private static double FinalTotal;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = Session["LoginDetail"] as SaitexDM.Common.DataModel.UserLoginDetail;
            if (!IsPostBack)
            {
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"] != "")
                {
                    strType = Request.QueryString["Type"].ToString().Trim();
                }
                InitiallisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitiallisePage()
    {
        try
        {
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            BindNoteType();
            txtAdviceDate.Text = System.DateTime.Now.ToShortDateString();
            txtAdviceNo.Visible = true;
            cmbFindAdvice.Visible = false;
            cmbFindAdvice.Visible = false;
            BindNoteCategory();

            if (strType == "D")
            {
                lblHeading.Text = "Store Debit Advice";
                ddlNoteType.SelectedValue = "DEBIT";
                ddlNoteType.Enabled = false;
                strTypeName = "DEBIT NOTE";
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue("DISCOUNT"));
            }
            else
            {
                lblHeading.Text = "Store Credit Advice";
                ddlNoteType.SelectedValue = "CREDIT";
                ddlNoteType.Enabled = false;
                //ddlTRNDetail.Enabled = false;
                //btnsaveTRNDetail.Enabled = false;
                //btnTRNCancel.Enabled = false;
                strTypeName = "CREDIT NOTE";
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue("SALE RETURN"));
            }

            BindEntryBy();
            FillDrCrNumber();
            ClearForm();
        }
        catch
        {
            throw;
        }
    }

    private void BindNoteType()
    {
        try
        {
            ddlNoteType.Items.Add(new ListItem("DEBIT NOTE", "DEBIT"));
            ddlNoteType.Items.Add(new ListItem("CREDIT NOTE", "CREDIT"));
        }
        catch
        {
            throw;
        }
    }

    private void BindNoteCategory()
    {
        try
        {
            ddlCategory.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("NOTE_CATEGORY", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "MST_DESC";
                ddlCategory.DataValueField = "MST_CODE";
                ddlCategory.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillDrCrNumber()
    {
        try
        {
            txtAdviceNo.Text = SaitexBL.Interface.Method.TX_DEBIT_MST.GetMaxDrCrNote(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, strTypeName);
        }
        catch
        {
            throw;
        }
    }

    private void BindEntryBy()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_DEBIT_MST.GetUsersFromDeptForEntryBy(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.VC_DEPARTMENTCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbEntryBy.Items.Clear();
                cmbEntryBy.DataSource = dt;
                cmbEntryBy.DataTextField = "EMPLOYEENAME";
                cmbEntryBy.DataValueField = "EMP_CODE";
                cmbEntryBy.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void ClearForm()
    {
        try
        {
            //txtAdviceNo.Text = string.Empty;
            //txtAdviceDate.Text = string.Empty;
            ddlParty.SelectedIndex = -1;
            txtPartyCode.Text = string.Empty;
            txtPartyDetail.Text = string.Empty;
            txtAdviceAmount.Text = string.Empty;
            txtPartyBillAmtInWords.Text = string.Empty;
            //ddlCategory.SelectedIndex = 0;
            txtRemarks.Text = string.Empty;
            ClearTRNDetails();
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateForm(out msg))
            {
                SaveBillData();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Bill Information.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool ValidateForm(out string msg)
    {
        try
        {
            msg = string.Empty;
            int iCount = 0;

            if (txtPartyCode.Text != string.Empty)
            {
                iCount++;
            }
            else
            {
                msg += @"\r\nPlease select party";
            }

            if (txtAdviceNo.Text != string.Empty)
            {
                iCount++;
            }
            else
            {
                msg += @"\r\nPlease Advice number first..";
            }

            //if (cmbEntryBy.SelectedIndex > -1)
            //{
            //    iCount++;
            //}
            //else
            //{
            //    msg += @"\r\nPlease Select Entry By User first..";
            //}

            if (iCount == 2)
                return true;
            else
                return false;
        }
        catch
        {
            throw;
        }
    }

    private void SaveBillData()
    {
        try
        {
            if (ViewState["dtTRNDetail"] != null)
                dtTRNDetail = (DataTable)ViewState["dtTRNDetail"];

            if (dtTRNDetail != null && dtTRNDetail.Rows.Count > 0)
            {
                oTX_DEBIT_MST = new TX_DEBIT_MST();
                double Bill_Amount = 0;
                double.TryParse(txtAdviceAmount.Text, out Bill_Amount);
                oTX_DEBIT_MST.ADVICE_AMT = Bill_Amount;
                oTX_DEBIT_MST.ADVICE_DT = DateTime.Parse(txtAdviceDate.Text);
                oTX_DEBIT_MST.BILL_ENTR_BY = oUserLoginDetail.UserCode;
                oTX_DEBIT_MST.BILL_RCV_DATE = System.DateTime.Now.Date;
                oTX_DEBIT_MST.ADVICE_NO = txtAdviceNo.Text.ToUpper().Trim();
                oTX_DEBIT_MST.BILL_REMARK = txtRemarks.Text;
                oTX_DEBIT_MST.NOTE_TYPE = ddlNoteType.SelectedItem.ToString().Trim();
                oTX_DEBIT_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_DEBIT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_DEBIT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_DEBIT_MST.CONV_RATE = double.Parse("0");
                oTX_DEBIT_MST.CURR_CODE = "Rs.";
                oTX_DEBIT_MST.PRTY_CODE = txtPartyCode.Text.Trim();
                oTX_DEBIT_MST.PRTY_NAME = txtPartyCode.Text.Trim();
                oTX_DEBIT_MST.TDATE = System.DateTime.Now.Date;
                oTX_DEBIT_MST.TUSER = oUserLoginDetail.UserCode;
                oTX_DEBIT_MST.NOTE_CATEGORY = ddlCategory.SelectedValue.Trim();
                oTX_DEBIT_MST.ENTRY_BY = oUserLoginDetail.UserCode;// cmbEntryBy.SelectedValue.ToString().Trim();
                string Bill_Numb = string.Empty;

                bool IsExist = SaitexBL.Interface.Method.TX_DEBIT_MST.CheckAdviceEntryExistence(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oTX_DEBIT_MST.ADVICE_NO, oTX_DEBIT_MST.PRTY_NAME, oTX_DEBIT_MST.PRTY_CODE, oTX_DEBIT_MST.BILL_YEAR, strTypeName);
                if (IsExist)
                {
                    bool bResult = SaitexBL.Interface.Method.TX_DEBIT_MST.Insert(oTX_DEBIT_MST, dtTRNDetail, out Bill_Numb);
                    if (bResult)
                    {
                        Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + ".. your Bill no " + Bill_Numb + " Saved successfully.");
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + ".. sorry bill saving failed. check your data.");
                    }

                    InitiallisePage();
                    if (dtTRNDetail != null && dtTRNDetail.Rows.Count > 0)
                    {
                        dtTRNDetail.Clear();
                        grdBillTRNDetail.DataSource = null;
                        grdBillTRNDetail.DataBind();
                        ViewState["dtTRNDetail"] = dtTRNDetail;
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("This Advice Number is Already Exists, Please Enter Another!!");
                    txtAdviceNo.Text = string.Empty;
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("First entered Transaction Details..");
            }
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
            lblMode.Text = "You are in Update Mode";
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            txtAdviceNo.Visible = false;
            cmbFindAdvice.Visible = true;
            cmbFindAdvice.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Bill Information for update.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateBillData();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Bill Information.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void UpdateBillData()
    {
        try
        {
            if (ViewState["dtTRNDetail"] != null)
                dtTRNDetail = (DataTable)ViewState["dtTRNDetail"];

            if (dtTRNDetail != null && dtTRNDetail.Rows.Count > 0)
            {
                oTX_DEBIT_MST = new TX_DEBIT_MST();
                double Bill_Amount = 0;
                double.TryParse(txtAdviceAmount.Text, out Bill_Amount);
                oTX_DEBIT_MST.ADVICE_AMT = Bill_Amount;
                oTX_DEBIT_MST.ADVICE_DT = DateTime.Parse(txtAdviceDate.Text);
                oTX_DEBIT_MST.BILL_ENTR_BY = oUserLoginDetail.UserCode;
                oTX_DEBIT_MST.NOTE_CATEGORY = ddlCategory.SelectedItem.Text.Trim();
                oTX_DEBIT_MST.ADVICE_NO = cmbFindAdvice.SelectedValue.ToString().Trim();
                oTX_DEBIT_MST.BILL_REMARK = txtRemarks.Text;
                oTX_DEBIT_MST.NOTE_TYPE = ddlNoteType.SelectedItem.Text.Trim();
                oTX_DEBIT_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_DEBIT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_DEBIT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_DEBIT_MST.CONV_RATE = double.Parse("0");
                oTX_DEBIT_MST.CURR_CODE = "Rs.";
                oTX_DEBIT_MST.PRTY_CODE = txtPartyCode.Text.Trim();
                oTX_DEBIT_MST.PRTY_NAME = txtPartyCode.Text.Trim();
                oTX_DEBIT_MST.TDATE = System.DateTime.Now.Date;
                oTX_DEBIT_MST.TUSER = oUserLoginDetail.UserCode;
                oTX_DEBIT_MST.ENTRY_BY = oUserLoginDetail.UserCode;//cmbEntryBy.SelectedValue.ToString().Trim();

                bool IsExist = SaitexBL.Interface.Method.TX_DEBIT_MST.CheckAdviceEntryExistence(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oTX_DEBIT_MST.ADVICE_NO, oTX_DEBIT_MST.PRTY_NAME, oTX_DEBIT_MST.PRTY_CODE, oTX_DEBIT_MST.BILL_YEAR, strTypeName);
                if (!IsExist)
                {
                    bool bResult = SaitexBL.Interface.Method.TX_DEBIT_MST.Update(oTX_DEBIT_MST, dtTRNDetail);
                    if (bResult)
                    {
                        Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + ".. your Bill updated successfully.");
                        InitiallisePage();
                        if (dtTRNDetail != null && dtTRNDetail.Rows.Count > 0)
                        {
                            dtTRNDetail.Clear();
                            grdBillTRNDetail.DataSource = null;
                            grdBillTRNDetail.DataBind();
                            ViewState["dtTRNDetail"] = dtTRNDetail;
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + ".. sorry bill updation failed. check your data.");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("BillEntry No Already Exists Please Enter Another!!");
                    txtAdviceNo.Text = string.Empty;
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("First entered Transaction Details..");
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion Bill Information.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Response.Redirect("./DebitNote.aspx?Type=D", false);
            if (strType == "D")
            {
                Response.Redirect("./DebitNote.aspx?Type=D", false);
            }
            else
            {
                Response.Redirect("./DebitNote.aspx?Type=C", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Refreshing Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (strType == "C")
            {
                Response.Redirect("~/Module/Inventory/Reports/Debit_Note_Parameter.aspx?NOTE_TYPE=CREDIT NOTE", false);
            }
            else 
            {
                Response.Redirect("~/Module/Inventory/Reports/Debit_Note_Parameter.aspx?NOTE_TYPE=DEBIT NOTE", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Print.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leaving page.\r\nSee error log for detail."));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlParty_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            ddlParty.Items.Clear();
            ddlParty.DataSource = data;
            ddlParty.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Party / Vendor.\r\nSee error log for detail."));
        }
    }

    protected void ddlParty_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyCode.Text = ddlParty.SelectedText.ToString();
            txtPartyDetail.Text = ddlParty.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Party / Vendor Details.\r\nSee error log for detail."));
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void txtAdviceAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            BindAmountInWords();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Party Bill Amount.\r\nSee error log for detail."));
        }
    }

    private void BindAmountInWords()
    {
        try
        {
            AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
            double Bill_Amount = 0;
            bool bBill = double.TryParse(txtAdviceAmount.Text.ToString(), out Bill_Amount);

            if (bBill)
            {
                txtPartyBillAmtInWords.Text = oRupeesToWord.changeCurrencyToWords(Bill_Amount);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please enter correct amount");
                txtAdviceAmount.Text = string.Empty;
                txtAdviceAmount.Focus();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNDetail_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindddlTRNDetail(e.Text.ToUpper());
            e.ItemsLoadedCount = ddlTRNDetail.Items.Count;
            e.ItemsCount = ddlTRNDetail.Items.Count;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Transaction details for Bill Adjustment.\r\nSee error log for detail."));
        }
    }

    private void BindddlTRNDetail(string text)
    {
        try
        {
            string CommandText = " SELECT YEAR, TRN_TYPE, TRN_NUMB, PRTY_CODE,(YEAR|| '@'|| TRN_TYPE|| '@'|| TRN_NUMB|| '@'|| SUM (NVL (AMOUNT, 0))) TRNDATA, SUM(AMOUNT) AMOUNT FROM (SELECT DISTINCT A.YEAR, A.TRN_TYPE, A.TRN_NUMB, A.PRTY_CODE, NVL (B.TRN_QTY, 0) TRN_QTY, NVL (B.FINAL_RATE, 0) FINAL_RATE, NVL (B.TRN_QTY, 0) * NVL (B.FINAL_RATE, 0) AMOUNT, ( B.YEAR|| '@'|| B.TRN_TYPE|| '@'|| B.TRN_NUMB|| '@'|| NVL (B.TRN_QTY, 0) * NVL (B.FINAL_RATE, 0))trndata FROM TX_ITEM_IR_MST A, TX_ITEM_IR_trn B WHERE  B.TRN_TYPE IN('IMS03') AND a.year='" + oUserLoginDetail.DT_STARTDATE.Year + "' and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "'  AND A.PRTY_CODE = '" + ddlParty.SelectedText.Trim() + "' AND A.YEAR = B.YEAR AND A.TRN_TYPE = B.TRN_TYPE AND A.TRN_NUMB = B.TRN_NUMB ) ASD"; // and NVL (A.BILL_NUMB, 0) = 0 
            string WhereClause = " WHERE TRN_TYPE LIKE :SearchQuery OR TRN_NUMB LIKE :SearchQuery ";
            string SortExpression = " GROUP BY YEAR, TRN_TYPE, TRN_NUMB, PRTY_CODE ORDER BY TRN_TYPE ASC, TRN_NUMB ASC";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            ddlTRNDetail.Items.Clear();
            if (data != null && data.Rows.Count > 0)
            {
                ddlTRNDetail.DataSource = data;
                ddlTRNDetail.DataTextField = "TRN_NUMB";
                ddlTRNDetail.DataValueField = "TRNDATA";
                ddlTRNDetail.DataBind();
            }
            else
            {
                Common.CommonFuction.ShowMessage("No TRN Details Exists for Bill Entry");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNDetail_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string TRN_TYPE = string.Empty;
            int YEAR = 0;
            int TRN_NUMB = 0;
            double TRN_AMOUNT = 0;
            ComboBox thisTextBox = (ComboBox)ddlTRNDetail;

            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

            string sCombine = ddlTRNDetail.SelectedValue.Trim();

            string[] sString = sCombine.Split('@');

            YEAR = int.Parse(sString[0].ToString());
            TRN_TYPE = sString[1].ToString();
            TRN_NUMB = int.Parse(sString[2].ToString());
            TRN_AMOUNT = double.Parse(sString[3].ToString());

            if (!SearchTRNDetaileInGrid(YEAR, TRN_TYPE, TRN_NUMB, UNIQUE_ID))
            {
                txtTRNYear.Text = YEAR.ToString();
                txtTRNType.Text = TRN_TYPE;
                txtTRNNo.Text = TRN_NUMB.ToString();
                txtTRNAmount.Text = TRN_AMOUNT.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('This TRN Detail already included');", true);
                thisTextBox.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Transaction details for Bill Adjustment.\r\nSee error log for detail."));
        }
    }

    private bool SearchTRNDetaileInGrid(int YEAR, string TRN_TYPE, int TRN_NUMB, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            if (grdBillTRNDetail.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdBillTRNDetail.Rows)
                {
                    Label txtYear = (Label)grdRow.FindControl("txtYear");
                    int iYear = int.Parse(txtYear.Text);
                    Label txtTRN_Type = (Label)grdRow.FindControl("txtTRN_Type");
                    string sTRN_TYPE = txtTRN_Type.Text;
                    Label txtTRN_NUMB = (Label)grdRow.FindControl("txtTRN_NUMB");
                    int iTRN_NUMB = int.Parse(txtTRN_NUMB.Text);

                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUE_ID = int.Parse(lnkEdit.CommandArgument.Trim());

                    if (iYear == YEAR && sTRN_TYPE == TRN_TYPE && iTRN_NUMB == TRN_NUMB && UNIQUE_ID != iUNIQUE_ID)
                        Result = true;
                }
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            saveTRNDetailToDataTable();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Transaction details for Bill Adjustment.\r\nSee error log for detail."));
        }
    }

    private void saveTRNDetailToDataTable()
    {
        try
        {
            if (ViewState["dtTRNDetail"] != null)
                dtTRNDetail = (DataTable)ViewState["dtTRNDetail"];

            if (dtTRNDetail == null)
                CreateTRNTable();

            if (txtTRNYear.Text != string.Empty && txtTRNType.Text != string.Empty && txtTRNNo.Text != string.Empty && txtTRNAmount.Text != string.Empty)
            {
                int iYEAR = int.Parse(txtTRNYear.Text);
                string TRN_TYPE = txtTRNType.Text;
                int iTRN_NUMB = int.Parse(txtTRNNo.Text);
                double dTRNAmount = double.Parse(txtTRNAmount.Text);

                int UNIQUE_ID = 0;
                if (ViewState["UNIQUE_ID"] != null)
                    UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

                bool bb = SearchTRNDetaileInGrid(iYEAR, TRN_TYPE, iTRN_NUMB, UNIQUE_ID);
                if (!bb)
                {
                    if (UNIQUE_ID > 0)
                    {
                        DataView dv = new DataView(dtTRNDetail);
                        dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                        if (dv.Count > 0)
                        {
                            dv[0]["YEAR"] = iYEAR;
                            dv[0]["TRN_TYPE"] = TRN_TYPE;
                            dv[0]["TRN_NUMB"] = iTRN_NUMB;
                            dv[0]["TRN_AMT"] = dTRNAmount;
                            dtTRNDetail.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtTRNDetail.NewRow();
                        dr["UNIQUE_ID"] = dtTRNDetail.Rows.Count + 1;
                        dr["YEAR"] = iYEAR;
                        dr["TRN_TYPE"] = TRN_TYPE;
                        dr["TRN_NUMB"] = iTRN_NUMB;
                        dr["TRN_AMT"] = dTRNAmount;
                        dtTRNDetail.Rows.Add(dr);
                    }
                    ViewState["dtTRNDetail"] = dtTRNDetail;
                    ClearTRNDetails();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid TRN Details');", true);
                }
            }
            BindTRNDetailToGrid();
        }
        catch
        {
            throw;
        }
    }

    protected void btnTRNCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearTRNDetails();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Canceling Transaction details for Bill Adjustment.\r\nSee error log for detail."));
        }
    }

    private void ClearTRNDetails()
    {
        try
        {
            ddlTRNDetail.SelectedIndex = -1;
            ddlTRNDetail.Items.Clear();
            txtTRNAmount.Text = string.Empty;
            txtTRNNo.Text = string.Empty;
            txtTRNType.Text = string.Empty;
            txtTRNYear.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void CreateTRNTable()
    {
        try
        {
            dtTRNDetail = new DataTable();
            dtTRNDetail.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRNDetail.Columns.Add("YEAR", typeof(int));
            dtTRNDetail.Columns.Add("TRN_TYPE", typeof(string));
            dtTRNDetail.Columns.Add("TRN_NUMB", typeof(int));
            dtTRNDetail.Columns.Add("TRN_AMT", typeof(double));
        }
        catch
        {
            throw;
        }
    }

    private void BindTRNDetailToGrid()
    {
        try
        {
            if (ViewState["dtTRNDetail"] != null)
                dtTRNDetail = (DataTable)ViewState["dtTRNDetail"];

            grdBillTRNDetail.DataSource = dtTRNDetail;
            grdBillTRNDetail.DataBind();
            FinalTotal = 0;
            foreach (GridViewRow row in grdBillTRNDetail.Rows)
            {
                Label txtAmount = (Label)row.FindControl("txtTRN_Amount");
                FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
            }
            if (grdBillTRNDetail.Rows.Count > 0)
            {
                txtAdviceAmount.Text = FinalTotal.ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdBillTRNDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "EditTRNDEtail")
            {
                FillDetailByGrid(UNIQUE_ID);
            }
            else if (e.CommandName == "DelTRNDetail")
            {
                deleteTRNDetailRow(UNIQUE_ID);
                BindTRNDetailToGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Transaction details for Bill Adjustment.\r\nSee error log for detail."));
        }
    }

    private void FillDetailByGrid(int UNIQUE_ID)
    {
        try
        {
            if (ViewState["dtTRNDetail"] != null)
                dtTRNDetail = (DataTable)ViewState["dtTRNDetail"];

            DataView dv = new DataView(dtTRNDetail);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                txtTRNYear.Text = dv[0]["YEAR"].ToString();
                txtTRNType.Text = dv[0]["TRN_TYPE"].ToString();
                txtTRNNo.Text = dv[0]["TRN_NUMB"].ToString();
                txtTRNAmount.Text = dv[0]["TRN_AMT"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteTRNDetailRow(int UNIQUE_ID)
    {
        try
        {
            if (ViewState["dtTRNDetail"] != null)
                dtTRNDetail = (DataTable)ViewState["dtTRNDetail"];

            if (grdBillTRNDetail.Rows.Count == 1)
            {
                dtTRNDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRNDetail.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRNDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRNDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            ViewState["dtTRNDetail"] = dtTRNDetail;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbFindAdvice_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            cmbFindAdvice.Items.Clear();
            DataTable data = new DataTable();
            data = GetBill(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbFindAdvice.DataSource = data;
            cmbFindAdvice.DataTextField = "ADVICE_NO";
            cmbFindAdvice.DataValueField = "ADVICE_NO";
            cmbFindAdvice.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Bill Information for update.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetBill(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string commandText = "SELECT * FROM (SELECT a.bill_year, a.NOTE_TYPE, a.ADVICE_NO, a.prty_code, a.ADVICE_AMT, b.PRTY_NAME, ( B.PRTY_NAME|| '( '|| A.PRTY_CODE|| ' ), '|| B.PRTY_ADD1|| ' '|| B.PRTY_CITY) PARTY FROM TX_DEBIT_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.BILL_YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND A.NOTE_TYPE = '" + strTypeName + "') asd";
            string whereClause = " where ADVICE_NO like :searchQuery or PARTY like :searchQuery OR PRTY_NAME like :searchQuery  OR prty_code like :searchQuery OR ADVICE_AMT LIKE :searchQuery";
            string sortExpression = " order by ADVICE_NO desc, prty_code desc";

            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbFindAdvice_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            if (ViewState["dtTRNDetail"] != null)
                dtTRNDetail = (DataTable)ViewState["dtTRNDetail"];

            string Bill_Numb = cmbFindAdvice.SelectedValue.Trim();

            if (dtTRNDetail == null || dtTRNDetail.Rows.Count == 0)
                CreateTRNTable();

            dtTRNDetail.Rows.Clear();

            int iRecordFound = GetdataByBillNumber(Bill_Numb);
            BindTRNDetailToGrid();
            if (iRecordFound > 0)
            {

            }
            else
            {
                InitiallisePage();
                lblMode.Text = "You are in Update Mode";
                txtAdviceNo.Text = string.Empty;

                ActivateUpdateMode();

                string msg = "Dear " + oUserLoginDetail.Username + " !! Bill already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
            ViewState["dtTRNDetail"] = dtTRNDetail;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Bill Information for update.\r\nSee error log for detail."));
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            cmbFindAdvice.Visible = true;
            cmbFindAdvice.SelectedValue = string.Empty;
            cmbFindAdvice.SelectedText = string.Empty;
            cmbFindAdvice.SelectedIndex = -1;
            cmbFindAdvice.Items.Clear();
            txtAdviceNo.Visible = false;

            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private int GetdataByBillNumber(string Bill_Numb)
    {
        int iRecordFound = 0;
        try
        {
            oTX_DEBIT_MST = new TX_DEBIT_MST();
            oTX_DEBIT_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_DEBIT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_DEBIT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_DEBIT_MST.ADVICE_NO = Bill_Numb;
            DataTable dt = SaitexBL.Interface.Method.TX_DEBIT_MST.GetBillDetailByNumber(oTX_DEBIT_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BILL_CLR_PUR_USER"].ToString() == string.Empty)
                {
                    iRecordFound = 1;
                    txtAdviceNo.Text = dt.Rows[0]["ADVICE_NO"].ToString().Trim();
                    txtAdviceDate.Text = DateTime.Parse(dt.Rows[0]["ADVICE_DT"].ToString().Trim()).ToShortDateString();
                    txtAdviceAmount.Text = dt.Rows[0]["ADVICE_AMT"].ToString().Trim();
                    txtRemarks.Text = dt.Rows[0]["BILL_REMARK"].ToString().Trim();
                    txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                    txtPartyDetail.Text = dt.Rows[0]["ADDRESS"].ToString().Trim();
                    ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(dt.Rows[0]["NOTE_CATEGORY"].ToString()));
                    cmbEntryBy.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(dt.Rows[0]["ENTRY_BY"].ToString()));
                    ViewState["PARTY_CODE"] = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                    ViewState["PRTY_NAME"] = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                    BindAmountInWords();
                }
            }

            if (iRecordFound == 1)
            {
                DataTable dtTemp = GetTRNdataByBillNumber(Bill_Numb);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                }
                //else
                //{
                //    string msg = "Dear " + oUserLoginDetail.Username + " !! Bill Number already approved. Modification not allowed.";
                //    Common.CommonFuction.ShowMessage(msg);
                //    InitiallisePage();
                //    txtAdviceNo.Text = "";
                //    cmbFindAdvice.Focus();
                //    lblMode.Text = "Update";
                //    ActivateUpdateMode();
                //    ClearTRNDetails();
                //}
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetTRNdataByBillNumber(string Bill_Numb)
    {
        try
        {
            oTX_DEBIT_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_DEBIT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_DEBIT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_DEBIT_MST.ADVICE_NO = Bill_Numb;

            DataTable dtTemp = SaitexBL.Interface.Method.TX_DEBIT_MST.GetBillTRNDetailByNumber(oTX_DEBIT_MST);
            return dtTemp;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtTRNDetail"] != null)
                dtTRNDetail = (DataTable)ViewState["dtTRNDetail"];

            if (dtTRNDetail == null || dtTRNDetail.Rows.Count == 0)
                CreateTRNTable();

            dtTRNDetail.Rows.Clear();
            dtTRNDetail = dtTemp.Copy();

            if (!dtTRNDetail.Columns.Contains("UNIQUE_ID"))
                dtTRNDetail.Columns.Add("UNIQUE_ID", typeof(int));

            if (dtTRNDetail != null && dtTRNDetail.Rows.Count > 0)
            {
                for (int iLoop = 0; iLoop < dtTRNDetail.Rows.Count; iLoop++)
                {
                    dtTRNDetail.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
                }
                dtTemp = null;
            }
            ViewState["dtTRNDetail"] = dtTRNDetail;
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}