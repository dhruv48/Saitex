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

public partial class Module_Yarn_SalesWork_Controls_BillEntryYarn : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    TX_BILL_MST oTX_BILL_MST;
    private static DataTable dtTRNDetail;
    private static string BILL_TYPE;
    private static double FinalTotal;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = Session["LoginDetail"] as SaitexDM.Common.DataModel.UserLoginDetail;

            if (!IsPostBack)
            {

                InitiallisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
        }
    }

    private void InitiallisePage()
    {
        try
        {
            BILL_TYPE = "YSP";
            BindBillType();
            BindSuppType();
            BindVatCategory();
            ClearForm();
            //GetNewBillNo();

            if (dtTRNDetail != null)
                dtTRNDetail.Rows.Clear();

            BindTRNDetailToGrid();

            ActivateSaveMode();
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
            txtBillNo.Text = string.Empty;
            txtForwardedBy.Text = string.Empty;
            txtForwardedDate.Text = string.Empty;
            txtPartyBillAmount.Text = string.Empty;
            txtPartyBillAmtInWords.Text = string.Empty;
            txtPartyBillDate.Text = string.Empty;
            txtPartyDetail.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtVatAmount.Text = string.Empty;
            txtVatTRNAmount.Text = string.Empty;

            chkFwdToPurDept.Checked = false;

            ddlBillType.SelectedIndex = -1;
            ddlParty.SelectedIndex = -1;
            ddlSuppType.SelectedIndex = -1;
            ddlVatCategory.SelectedIndex = -1;

            ClearTRNDetails();

        }
        catch
        {
            throw;
        }
    }

    private void ClearTRNDetails()
    {
        try
        {
            ddlTRNDetail.SelectedIndex = -1;
            ddlTRNDetail.Items.Clear();

            txtTRNAmount.Text = string.Empty;
            txtTRNDel.Text = string.Empty;
            txtTRNNo.Text = string.Empty;
            txtTRNPFlag.Text = string.Empty;
            txtTRNPrice.Text = string.Empty;
            txtTRNQuality.Text = string.Empty;
            txtTRNSupport.Text = string.Empty;
            txtTRNType.Text = string.Empty;
            txtTRNYear.Text = string.Empty;

            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void ActivateSaveMode()
    {
        try
        {
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            txtBillNo.Visible = true;

            ddlFindBill.Visible = false;
            ddlFindBill.SelectedIndex = -1;

            txtPartyBillDate.Text = System.DateTime.Now.Date.ToShortDateString();

            ddlParty.Enabled = true;
            ddlBillType.Enabled = true;

            lblMode.Text = "Save";
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
            dtTRNDetail.Columns.Add("PRN_FLAG", typeof(string));
            dtTRNDetail.Columns.Add("QUALITY_POINT", typeof(int));
            dtTRNDetail.Columns.Add("DEL_POINT", typeof(int));
            dtTRNDetail.Columns.Add("PRICE_POINT", typeof(int));
            dtTRNDetail.Columns.Add("SUPPORT_POINT", typeof(int));
        }
        catch
        {
            throw;
        }
    }

    private void BindBillType()
    {
        try
        {
            //ddlBillType.Items.Add(new ListItem("SUPPLIER-FABRIC-SALE", "FSP"));
             ddlBillType.Items.Add(new ListItem("SUPPLIER-YARN", "YSP"));
            //ddlBillType.Items.Add(new ListItem("SUPPLIER-INVENTORY", "MSP"));
            //ddlBillType.Items.Add(new ListItem("SUPPLIER-FABRIC-JOB", "FJP"));
            //ddlBillType.Items.Add(new ListItem("SUPPLIER-YARN-JOB", "YJP"));
        }
        catch
        {
            throw;
        }
    }

    private void BindSuppType()
    {
        try
        {
            ddlSuppType.Items.Clear();
            ddlSuppType.Items.Add(new ListItem("Select", "N/A"));

            DataTable dtSupp = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BILL_SUPP_TYPE", oUserLoginDetail.COMP_CODE);
            if (dtSupp != null && dtSupp.Rows.Count > 0)
            {
                ddlSuppType.DataSource = dtSupp;
                ddlSuppType.DataTextField = "MST_DESC";
                ddlSuppType.DataValueField = "MST_CODE";
                ddlSuppType.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindVatCategory()
    {
        try
        {
            ddlVatCategory.Items.Clear();
            ddlVatCategory.Items.Add(new ListItem("Select", "N/A"));

            //DataTable dtSupp = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BILL_SUPP_TYPE", oUserLoginDetail.COMP_CODE);
            //if (dtSupp != null && dtSupp.Rows.Count > 0)
            //{
            //    ddlVatCategory.DataSource = dtSupp;
            //    ddlSuppType.DataTextField = "MST_DESC";
            //    ddlSuppType.DataValueField = "MST_CODE";
            //    ddlVatCategory.DataBind();
            //}
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // GetNewBillNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Bill Type.\r\nSee error log for detail."));
        }
    }

    private void GetNewBillNo()
    {
        try
        {
            oTX_BILL_MST = new TX_BILL_MST();
            oTX_BILL_MST.BILL_TYPE = BILL_TYPE;
            oTX_BILL_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_BILL_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            string MaxBillNo = SaitexBL.Interface.Method.TX_BILL_MST.GetMaxBillNo(oTX_BILL_MST);
            txtBillNo.Text = (int.Parse(MaxBillNo) + 1).ToString();

        }
        catch
        {
            throw;
        }
    }

    protected void ddlParty_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper());

            ddlParty.Items.Clear();

            ddlParty.DataSource = data;
            ddlParty.DataTextField = "PRTY_CODE";
            ddlParty.DataValueField = "Address";
            ddlParty.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Party / Vendor.\r\nSee error log for detail."));
        }
    }

    private DataTable GetPartyData(string Text)
    {
        try
        {
            string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0 AND PRTY_CODE  IN (SELECT PRTY_CODE FROM YRN_IR_MST WHERE CONF_FLAG=1 AND NVL(BILL_NUMB,0)=0)) asd";
            string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
            string SortExpression = " order by PRTY_CODE asc";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlParty_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyDetail.Text = ddlParty.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Party / Vendor Details.\r\nSee error log for detail."));
        }
    }

    protected void txtPartyBillAmount_TextChanged(object sender, EventArgs e)
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
            bool bBill = double.TryParse(txtPartyBillAmount.Text.ToString(), out Bill_Amount);

            if (bBill)
                txtPartyBillAmtInWords.Text = oRupeesToWord.changeCurrencyToWords(Bill_Amount);
            else
            {
                Common.CommonFuction.ShowMessage("Please enter correct amount");
                txtPartyBillAmount.Text = string.Empty;
                txtPartyBillAmount.Focus();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNDetail_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
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
            string CommandText = "SELECT YEAR, TRN_TYPE, TRN_NUMB, PRTY_CODE,(YEAR|| '@'|| TRN_TYPE|| '@'|| TRN_NUMB|| '@'|| SUM (NVL (AMOUNT, 0))) TRNDATA, SUM(AMOUNT) AMOUNT FROM (SELECT DISTINCT A.YEAR, A.TRN_TYPE, A.TRN_NUMB, A.PRTY_CODE, NVL (B.TRN_QTY, 0) TRN_QTY, NVL (B.FINAL_RATE, 0) FINAL_RATE, NVL (B.TRN_QTY, 0) * NVL (B.FINAL_RATE, 0) AMOUNT, ( B.YEAR|| '@'|| B.TRN_TYPE|| '@'|| B.TRN_NUMB|| '@'|| NVL (B.TRN_QTY, 0) * NVL (B.FINAL_RATE, 0))trndata FROM YRN_IR_MST A, YRN_IR_TRN B WHERE a.year='" + oUserLoginDetail.DT_STARTDATE.Year + "' and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "'  AND NVL(A.BILL_NUMB,0)=0  AND A.PRTY_CODE = '" + ddlParty.SelectedText.Trim() + "' AND A.YEAR = B.YEAR AND A.TRN_TYPE = B.TRN_TYPE AND A.TRN_NUMB = B.TRN_NUMB AND B.TRN_TYPE IN('RYS01', 'RYS02', 'RYS03', 'RYS04', 'RYS05', 'RYS11')) ASD";
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

    protected void ddlTRNDetail_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
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

                txtTRNPFlag.Focus();

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
        catch (Exception ex)
        {

            errorLog.ErrHandler.WriteError(ex.Message);
            return Result;
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
            if (dtTRNDetail == null)
                CreateTRNTable();

            if (dtTRNDetail.Rows.Count < 15)
            {
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
                                dv[0]["PRN_FLAG"] = txtTRNPFlag.Text.Trim();
                                dv[0]["QUALITY_POINT"] = txtTRNQuality.Text.Trim();
                                dv[0]["DEL_POINT"] = txtTRNDel.Text.Trim();
                                dv[0]["PRICE_POINT"] = txtTRNPrice.Text.Trim();
                                dv[0]["SUPPORT_POINT"] = txtTRNSupport.Text.Trim();

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
                            dr["PRN_FLAG"] = txtTRNPFlag.Text.Trim();
                            dr["QUALITY_POINT"] = txtTRNQuality.Text.Trim();
                            dr["DEL_POINT"] = txtTRNDel.Text.Trim();
                            dr["PRICE_POINT"] = txtTRNPrice.Text.Trim();
                            dr["SUPPORT_POINT"] = txtTRNSupport.Text.Trim();

                            dtTRNDetail.Rows.Add(dr);
                        }
                        ClearTRNDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid TRN Details');", true);
                    }
                }

                BindTRNDetailToGrid();
            }
            else
            {
                Common.CommonFuction.ShowMessage("You have reached the limit of TRN Details. Only 15 TRN Details allowed in one Bill Entry.");
            }
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
            DataView dv = new DataView(dtTRNDetail);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                txtTRNYear.Text = dv[0]["YEAR"].ToString();
                txtTRNType.Text = dv[0]["TRN_TYPE"].ToString();
                txtTRNNo.Text = dv[0]["TRN_NUMB"].ToString();
                txtTRNAmount.Text = dv[0]["TRN_AMT"].ToString();
                txtTRNPFlag.Text = dv[0]["PRN_FLAG"].ToString();
                txtTRNQuality.Text = dv[0]["QUALITY_POINT"].ToString();
                txtTRNDel.Text = dv[0]["DEL_POINT"].ToString();
                txtTRNPrice.Text = dv[0]["PRICE_POINT"].ToString();
                txtTRNSupport.Text = dv[0]["SUPPORT_POINT"].ToString();

                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void deleteTRNDetailRow(int UNIQUE_ID)
    {
        try
        {
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

                txtPartyBillAmount.Text = FinalTotal.ToString();
            }

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
        }
    }

    private bool ValidateForm(out string msg)
    {
        try
        {
            msg = string.Empty;
            int iCount = 0;

            if (dtTRNDetail != null && dtTRNDetail.Rows.Count > 0)
            {
                iCount++;
            }
            else
            {
                msg += @"\r\nPlease select atleast one M.R.N./T.R.N.";
            }

            //if (ddlParty.SelectedText.Trim() != "select...")
            //{
            //    iCount++;
            //}
            //else
            //{
            //    msg += @"\r\nPlease select party";
            //}

            if (iCount == 1)
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
            oTX_BILL_MST = new TX_BILL_MST();

            double Bill_Amount = 0;
            double.TryParse(txtPartyBillAmount.Text, out Bill_Amount);
            oTX_BILL_MST.BILL_AMNT = Bill_Amount;

            oTX_BILL_MST.BILL_DATE = DateTime.Parse(txtPartyBillDate.Text);
            oTX_BILL_MST.BILL_ENTR_BY = oUserLoginDetail.UserCode;
            oTX_BILL_MST.BILL_ENTRY_DATE = System.DateTime.Now.Date;
            oTX_BILL_MST.BILL_NUMB = txtBillNo.Text;
            oTX_BILL_MST.BILL_REMARK = txtRemarks.Text;
            oTX_BILL_MST.BILL_TYPE = BILL_TYPE;
            oTX_BILL_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_BILL_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_BILL_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST.CONV_RATE = double.Parse("0");
            oTX_BILL_MST.CURR_CODE = "Rs.";
            oTX_BILL_MST.PRTY_CODE = ddlParty.SelectedText.Trim();
            oTX_BILL_MST.PRTY_NAME = ddlParty.SelectedText.Trim();
            oTX_BILL_MST.TDATE = System.DateTime.Now.Date;
            oTX_BILL_MST.TUSER = oUserLoginDetail.UserCode;

            double Vat_amount = 0;
            double.TryParse(txtVatAmount.Text, out Vat_amount);
            oTX_BILL_MST.VAT_AMT = Vat_amount;

            oTX_BILL_MST.VAT_SUPP_TYPE = ddlSuppType.SelectedValue.Trim();

            double Vat_Trn_amount = 0;
            double.TryParse(txtVatTRNAmount.Text, out Vat_Trn_amount);
            oTX_BILL_MST.VAT_TRN_AMT = Vat_Trn_amount;

            oTX_BILL_MST.VAT_TYPE = ddlVatCategory.SelectedValue.Trim();

            bool bForwarded = false;
            if (chkFwdToPurDept.Checked)
            {

                bForwarded = true;
                oTX_BILL_MST.BILL_CLR_DATE = System.DateTime.Now;
                oTX_BILL_MST.BILL_CLR_USER = oUserLoginDetail.UserCode;
            }

            string Bill_Numb = string.Empty;
            bool bResult = SaitexBL.Interface.Method.TX_BILL_MST.Insert(oTX_BILL_MST, dtTRNDetail, out Bill_Numb, bForwarded);

            if (bResult)
            {
                Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + ".. your Bill no " + oTX_BILL_MST.BILL_NUMB + " saved successfully.");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + ".. sorry bill saving failed. check your data.");
            }
            InitiallisePage();
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
            lblMode.Text = "Update";

            ddlFindBill.Visible = true;
            txtBillNo.Visible = false;

            ddlParty.Enabled = false;
            ddlBillType.Enabled = false;

            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Bill Information for update.\r\nSee error log for detail."));
        }
    }

    protected void ddlFindBill_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ComboBox thisTextBox = (ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetBill(e.Text.ToUpper(), e.ItemsOffset, 10);

            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "BILL_NUMB";
            thisTextBox.DataValueField = "BILL_NUMB";
            thisTextBox.DataBind();

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
            string commandText = "SELECT * FROM (SELECT a.bill_year, a.bill_type, a.bill_numb, a.prty_code, a.BILL_AMNT, b.PRTY_NAME, ( B.PRTY_NAME|| '( '|| A.PRTY_CODE|| ' ), '|| B.PRTY_ADD1|| ' '|| B.PRTY_CITY) PARTY FROM TX_BILL_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.BILL_YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND A.BILL_TYPE = '" + BILL_TYPE + "') asd";
            string whereClause = " where bill_numb like :searchQuery or PARTY like :searchQuery OR PRTY_NAME like :searchQuery  OR prty_code like :searchQuery OR BILL_AMNT LIKE :searchQuery";
            string sortExpression = " order by bill_numb desc, prty_code desc";

            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlFindBill_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int Bill_Numb = int.Parse(ddlFindBill.SelectedValue.Trim());
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
                lblMode.Text = "Update";
                txtBillNo.Text = "";

                ActivateUpdateMode();

                string msg = "Dear " + oUserLoginDetail.Username + " !! Bill already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Bill Information for update.\r\nSee error log for detail."));
        }
    }

    private int GetdataByBillNumber(int Bill_Numb)
    {
        int iRecordFound = 0;
        try
        {
            string strBill_Numb = Bill_Numb.ToString();
            oTX_BILL_MST = new TX_BILL_MST();

            oTX_BILL_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_BILL_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_BILL_MST.BILL_TYPE = BILL_TYPE;
            oTX_BILL_MST.BILL_NUMB = strBill_Numb;

            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetBillDetailByNumber(oTX_BILL_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BILL_CLR_PUR_USER"].ToString() == string.Empty)
                {
                 iRecordFound = 1;
                txtBillNo.Text = dt.Rows[0]["BILL_NUMB"].ToString().Trim();
                txtPartyBillDate.Text = DateTime.Parse(dt.Rows[0]["BILL_DATE"].ToString().Trim()).ToShortDateString();
                txtPartyBillAmount.Text = dt.Rows[0]["BILL_AMNT"].ToString().Trim();
                txtVatAmount.Text = dt.Rows[0]["VAT_AMT"].ToString().Trim();
                txtVatTRNAmount.Text = dt.Rows[0]["VAT_TRN_AMT"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["BILL_REMARK"].ToString().Trim();

                ddlVatCategory.SelectedIndex = ddlVatCategory.Items.IndexOf(ddlVatCategory.Items.FindByValue(dt.Rows[0]["VAT_TYPE"].ToString().Trim()));
                ddlSuppType.SelectedIndex = ddlSuppType.Items.IndexOf(ddlSuppType.Items.FindByValue(dt.Rows[0]["VAT_SUPP_TYPE"].ToString().Trim()));
                //ddlVatCategory.SelectedValue = dt.Rows[0]["VAT_TYPE"].ToString().Trim();
                //ddlSuppType.SelectedValue = dt.Rows[0]["VAT_SUPP_TYPE"].ToString().Trim();

                DataTable data = GetPartyData("");

                ddlParty.DataSource = data;
                ddlParty.DataTextField = "PRTY_CODE";
                ddlParty.DataValueField = "ADDRESS";
                ddlParty.DataBind();

                foreach (ComboBoxItem item in ddlParty.Items)
                {
                    if (item.Text == dt.Rows[0]["PRTY_CODE"].ToString().Trim())
                    {
                        ddlParty.SelectedIndex = ddlParty.Items.IndexOf(item);
                        break;
                    }
                }

                txtPartyDetail.Text = dt.Rows[0]["ADDRESS"].ToString().Trim();
                ViewState["PARTY_CODE"] = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
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
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! Bill Number already approved. Modification not allowed.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitiallisePage();

                    txtBillNo.Text = "";
                    ddlFindBill.Focus();

                    lblMode.Text = "Update";

                    ActivateUpdateMode();

                    ClearTRNDetails();
                }
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetTRNdataByBillNumber(int Bill_Numb)
    {
        try
        {
            string strBill_Numb = Bill_Numb.ToString();
            oTX_BILL_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_BILL_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_BILL_MST.BILL_TYPE = BILL_TYPE;
            oTX_BILL_MST.BILL_NUMB = strBill_Numb;

            DataTable dtTemp = SaitexBL.Interface.Method.TX_BILL_MST.GetBillTRNDetailByNumber(oTX_BILL_MST);
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
        }
        catch
        {
            throw;
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            ddlFindBill.Visible = true;
            ddlFindBill.SelectedValue = string.Empty;
            ddlFindBill.SelectedText = string.Empty;
            ddlFindBill.SelectedIndex = -1;
            ddlFindBill.Items.Clear();
            txtBillNo.Visible = false;

            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
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
            string msg = string.Empty;
            if (ValidateForm(out msg))
            {
                UpdateBillData();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Bill Information.\r\nSee error log for detail."));
        }
    }

    private void UpdateBillData()
    {
        try
        {
            oTX_BILL_MST = new TX_BILL_MST();

            double Bill_Amount = 0;
            double.TryParse(txtPartyBillAmount.Text, out Bill_Amount);
            oTX_BILL_MST.BILL_AMNT = Bill_Amount;

            oTX_BILL_MST.BILL_DATE = DateTime.Parse(txtPartyBillDate.Text);
            oTX_BILL_MST.BILL_ENTR_BY = oUserLoginDetail.UserCode;
            oTX_BILL_MST.BILL_ENTRY_DATE = System.DateTime.Now.Date;
            oTX_BILL_MST.BILL_NUMB = txtBillNo.Text;
            oTX_BILL_MST.BILL_REMARK = txtRemarks.Text;
            oTX_BILL_MST.BILL_TYPE = BILL_TYPE;
            oTX_BILL_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_BILL_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_BILL_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST.CONV_RATE = double.Parse("0");
            oTX_BILL_MST.CURR_CODE = "Rs.";
            oTX_BILL_MST.PRTY_CODE = ViewState["PARTY_CODE"].ToString();// ddlParty.SelectedText.Trim();
            oTX_BILL_MST.PRTY_NAME = ViewState["PARTY_CODE"].ToString();// ddlParty.SelectedText.Trim();
            oTX_BILL_MST.TDATE = System.DateTime.Now.Date;
            oTX_BILL_MST.TUSER = oUserLoginDetail.UserCode;

            double Vat_amount = 0;
            double.TryParse(txtVatAmount.Text, out Vat_amount);
            oTX_BILL_MST.VAT_AMT = Vat_amount;

            oTX_BILL_MST.VAT_SUPP_TYPE = ddlSuppType.SelectedValue.Trim();

            double Vat_Trn_amount = 0;
            double.TryParse(txtVatTRNAmount.Text, out Vat_Trn_amount);
            oTX_BILL_MST.VAT_TRN_AMT = Vat_Trn_amount;

            oTX_BILL_MST.VAT_TYPE = ddlVatCategory.SelectedValue.Trim();

            bool bForwarded = false;
            if (chkFwdToPurDept.Checked)
            {

                bForwarded = true;
                oTX_BILL_MST.BILL_CLR_DATE = System.DateTime.Now;
                oTX_BILL_MST.BILL_CLR_USER = oUserLoginDetail.UserCode;
            }

            bool bResult = SaitexBL.Interface.Method.TX_BILL_MST.Update(oTX_BILL_MST, dtTRNDetail, bForwarded);

            if (bResult)
            {
                Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + ".. your Bill No " + oTX_BILL_MST.BILL_NUMB + " updated successfully.");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + ".. sorry bill no " + oTX_BILL_MST.BILL_NUMB + " updation failed. check your data.");
            }
            InitiallisePage();
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
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitiallisePage();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Refreshing Page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Print.\r\nSee error log for detail."));
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
        }
    }

    protected void chkFwdToPurDept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkFwdToPurDept.Checked)
            {
                txtForwardedBy.Text = oUserLoginDetail.Username;
                txtForwardedDate.Text = DateTime.Now.ToString();
            }
            else
            {
                txtForwardedDate.Text = string.Empty;
                txtForwardedBy.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection.\r\n See error log for detail."));
        }
    }

}
