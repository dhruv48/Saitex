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
using System.Linq;


public partial class Module_FA_Controls_FABankMaster : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_LGR_MST oFA_LGR_MST;
    SaitexDM.Common.DataModel.FA_BANK_MST oFA_BANK_MST;
    bool chStatus;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                InitialisePage();

                BindLedgerType();
                BindLedgerGroup();
                BindGrid();
            }
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }
                Session["saveStatus"] = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblErrorMessage.Text = "";
            lblMessage.Text = "";
            lblMode.Text = "Save";
            txtBankCode.ReadOnly = true;
            txtLedgerCode.ReadOnly = true;
            txtGroupCode.ReadOnly = true;
            txtGroupName.ReadOnly = true;
            cmbBankCode.Visible = false;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            chk_Status.Checked = true;
            txtOpeningBalance.Enabled = true;
            txtOpeningDate.Enabled = true;

            MaxBankCode();
            MaxLedgerCode();
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
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving the data..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            txtBankCode.Visible = false;
            cmbBankCode.Visible = true;
           //grdBank.AutoPostBackOnSelect = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            txtGroupCode.Text = "";
            txtGroupName.Text = "";
            lblMode.Text = "Update";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating the data..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            // DeleteData();
            Common.CommonFuction.ShowMessage("Sorry! dear you can't delete any Bank detail");
            lblMessage.Text = "Sorry! Dear you can't delete any Bank detail";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting the data..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./FABankMaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    private void MaxLedgerCode()
    {
        try
        {
            string x = "";
            int y = 0;
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetMaxLedgerCode();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        x = dv[iLoop]["MAX_ID"].ToString();
                        y = int.Parse(x);
                        y = y + 1;
                        txtLedgerCode.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void MaxBankCode()
    {
        string x = "";
        int y = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetMaxBankCode();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        x = dv[iLoop]["MAX_ID"].ToString();
                        y = int.Parse(x);
                        y = y + 1;
                        txtBankCode.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/BankMstReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing the data..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Help/BankMasterHelp.htm";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in helping..\r\nSee error log for detail."));
        }
    }

  

    protected void cmbBankCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10, 2);

            cmbBankCode.DataSource = data;
            cmbBankCode.DataBind();

            // Calculating the number of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text.ToUpper(), 2);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Bank Code..\r\nSee error log for detail."));
        }
    }

    protected void cmbBankCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string strBankCode = "";
            lblMessage.Text = "";
            strBankCode = cmbBankCode.SelectedValue.ToString().Trim();
            BindData(strBankCode);
            txtBankCode.Visible = true;
            txtBankCode.ReadOnly = true;
            cmbBankCode.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting the data..\r\nSee error log for detail."));
        }
    }

    private void BindLedgerType()
    {
        try
        {
            cmbLedgerType.Items.Clear();
            DataTable data = new DataTable();
            data = GetItems("", 0, 10, 3);

            cmbLedgerType.DataSource = data;
            cmbLedgerType.DataValueField = "MST_CODE";
            cmbLedgerType.DataTextField = "MST_CODE";
            cmbLedgerType.DataBind();
            cmbLedgerType.Items.Insert(0, new ListItem("--------- Select Ledger Type --------", "0"));
        }
        catch
        {
            throw;
        }
    }

    private void BindLedgerGroup()
    {
        try
        {
            cmbLedgerGroup.Items.Clear();
            DataTable data = new DataTable();
            data = GetItems("", 0, 10, 4);

            cmbLedgerGroup.DataSource = data;
            cmbLedgerGroup.DataTextField = "MST_CODE";
            cmbLedgerGroup.DataValueField = "MST_CODE";
            cmbLedgerGroup.DataBind();
            cmbLedgerGroup.Items.Insert(0, new ListItem("-------- Select Ledger Group --------", "0"));
        }
        catch
        {
            throw;
        }
    }

    private void BindGrid()
    {


        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetBankMaster(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdBank.DataSource = dt;
                grdBank.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems, int icond)
    {
        if (icond == 1)        // For Group Code
        {
            string whereClause = " WHERE GRP_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_NAME like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY GRP_CODE";
            string commandText = "SELECT * FROM FA_Grp_Mst";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        else if (icond == 2)             // For Bank Code --- Selection
        {
            string whereClause = " WHERE BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' and COMP_CODE like :SearchQuery And DEL_STATUS = '0'";
         
         //   string whereClause = " WHERE LGR_BANK_CODE like :SearchQuery And DEL_STATUS = '0' or LGR_BANK_NAME like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY LGR_BANK_CODE";
            string commandText = "SELECT * FROM FA_BANK_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        else if (icond == 3)          // For Ledger  Type
        {
            string whereClause = " WHERE MST_NAME LIKE 'LDGR_TYPE' And DEL_STATUS = '0' And MST_CODE like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY MST_CODE";
            string commandText = "SELECT * FROM TX_MASTER_TRN";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        else                       // For Ledger Group
        {
            string whereClause = " WHERE MST_NAME LIKE 'LDGR_GROUP' And DEL_STATUS = '0' And MST_CODE like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY MST_CODE";
            string commandText = "SELECT * FROM TX_MASTER_TRN";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
    }

    protected int GetItemsCount(string text, int icond)
    {
        if (icond == 1)              // For Group Code
        {
            string CommandText = "SELECT COUNT(*) FROM FA_Grp_Mst WHERE GRP_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_BANK_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        else if (icond == 2)           // For Bank Code --- Selection
        {
            string CommandText = "SELECT COUNT(*) FROM FA_BANK_MST WHERE LGR_BANK_CODE like :SearchQuery And DEL_STATUS = '0' or LGR_BANK_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_BANK_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        else if (icond == 3)          // For Ledger  Type
        {
            string CommandText = "SELECT COUNT(*) FROM TX_MASTER_TRN WHERE MST_NAME LIKE 'LDGR_TYPE' And DEL_STATUS = '0' And MST_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_BANK_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        else                       // For Ledger Group
        {
            string CommandText = "SELECT COUNT(*) FROM TX_MASTER_TRN WHERE MST_NAME LIKE 'LDGR_GROUP' And DEL_STATUS = '0' And MST_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_BANK_MST.GetCountForLOV(CommandText, text + '%', "");
        }
    }

    private void InsertData()
    {
        try
        {
            if (txtBankCode.Text != "")
            {
                if (txtBankName.Text != "")
                {
                    if (txtBranchCode.Text != "")
                    {
                        if (txtACNo.Text != "")
                        {
                            if (txtACType.Text != "")
                            {
                                if (txtRTGSCode.Text != "")
                                {
                                    if (txtLedgerCode.Text != "")
                                    {
                                        if (txtLedgerName.Text != "")
                                        {
                                            if (cmbLedgerType.SelectedIndex != -1)
                                            {
                                                if (txtPrintName.Text != "")
                                                {
                                                    if (txtGroupCode.Text != "")
                                                    {
                                                        if (cmbLedgerGroup.SelectedIndex != -1)
                                                        {
                                                            oFA_BANK_MST = new SaitexDM.Common.DataModel.FA_BANK_MST();
                                                            oFA_LGR_MST = new SaitexDM.Common.DataModel.FA_LGR_MST();

                                                            oFA_BANK_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                                            oFA_BANK_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                                            oFA_BANK_MST.LDGR_CODE = txtLedgerCode.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.LGR_BANK_CODE = txtBankCode.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.LGR_BANK_NAME = txtBankName.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.BANK_BRANCH_CODE = txtBranchCode.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.BANK_BRANCH_ADD = txtAddress.Text.Trim();
                                                            oFA_BANK_MST.BANK_AC_NO = txtACNo.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.BANK_AC_TYPE = txtACType.Text.Trim();
                                                            if (txtOpeningDate.Text != "")
                                                            {
                                                                oFA_BANK_MST.OPENING_DATE = DateTime.Parse(txtOpeningDate.Text.Trim());
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.OPENING_DATE = DateTime.Parse(System.DateTime.Now.ToShortDateString());
                                                            }
                                                            oFA_BANK_MST.RTGS_CODE = txtRTGSCode.Text.ToUpper().Trim();

                                                            if (rdoListChequeBook.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.CHEQ_BOOK = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.CHEQ_BOOK = false;
                                                            }

                                                            if (rdoListDebitCard.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.DEBIT_CARD = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.DEBIT_CARD = false;
                                                            }

                                                            if (rdoListCreditCard.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.CREDIT_CARD = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.CREDIT_CARD = false;
                                                            }

                                                            if (rdoListInternetBanking.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.INTERNET_BANKING = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.INTERNET_BANKING = false;
                                                            }

                                                            if (rdoListPhoneBanking.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.PHONE_BANKING = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.PHONE_BANKING = false;
                                                            }

                                                            if (rdoListPassbook.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.PASSBOOK = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.PASSBOOK = false;
                                                            }

                                                            if (rdoListOnlineShopping.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.ONLINE_SHOPPING = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.ONLINE_SHOPPING = false;
                                                            }

                                                            oFA_BANK_MST.TUSER = oUserLoginDetail.UserCode;

                                                            if (chk_Status.Checked == true)
                                                            {
                                                                chStatus = true;
                                                            }
                                                            else
                                                            {
                                                                chStatus = false;
                                                            }
                                                            oFA_BANK_MST.STATUS = chStatus;
                                                            oFA_BANK_MST.DEL_STATUS = false;


                                                            oFA_LGR_MST.LDGR_NAME = txtLedgerName.Text.ToUpper().Trim();
                                                            oFA_LGR_MST.PRINT_NAME = txtPrintName.Text.ToUpper().Trim();
                                                            oFA_LGR_MST.GRP_CODE = txtGroupCode.Text.ToUpper().Trim();
                                                            oFA_LGR_MST.LDGR_DESC = txtDescription.Text.Trim();
                                                            if (txtOpeningBalance.Text == "")
                                                            {
                                                                oFA_LGR_MST.OP_BAL = 0;
                                                            }
                                                            else
                                                            {
                                                                oFA_LGR_MST.OP_BAL = float.Parse(txtOpeningBalance.Text.Trim());
                                                            }
                                                            oFA_LGR_MST.LDGR_TYPE = cmbLedgerType.SelectedValue.ToString().Trim();
                                                            oFA_LGR_MST.LDGR_Group = cmbLedgerGroup.SelectedValue.ToString().Trim();
                                                            oFA_LGR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                                                            oFA_LGR_MST.IS_DR_OP = true;        // For Banks Ledgers Always Debit

                                                            int iRecordFound = 0;

                                                            bool bResult = SaitexBL.Interface.Method.FA_BANK_MST.InsertFABankMaster(oFA_BANK_MST, oFA_LGR_MST, out iRecordFound);

                                                            if (bResult)
                                                            {
                                                                Session["saveStatus"] = 1;
                                                                Response.Redirect("./FABankMaster.aspx?cId=S", false);
                                                            }
                                                            else if (iRecordFound > 0)
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                                                            }
                                                            else
                                                            {
                                                                Common.CommonFuction.ShowMessage("Dear! DLL Error in saving data..");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Common.CommonFuction.ShowMessage("Dear! Please select Ledger Group..");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Common.CommonFuction.ShowMessage("Dear! Please enter Group Code..");
                                                    }
                                                }
                                                else
                                                {
                                                    Common.CommonFuction.ShowMessage("Dear! Please enter Print Name..");
                                                }
                                            }
                                            else
                                            {
                                                Common.CommonFuction.ShowMessage("Dear! Please select Ledger type..");
                                            }
                                        }
                                        else
                                        {
                                            Common.CommonFuction.ShowMessage("Dear! Please enter Ledger Name..");
                                        }
                                    }
                                    else
                                    {
                                        Common.CommonFuction.ShowMessage("Dear! Please enter Ledger Code..");
                                    }
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Dear! Please enter RTGS Code..");
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Dear! Please enter Account Type..");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please enter Account Number..");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please enter Branch Code..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please enter Bank Name..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please enter Bank Code..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindData(string BankCode)
    {
        try
        {
            string LedgerCode = string.Empty;

            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetBankMaster(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            char chCheck;
            char chChequeBook;
            char chDebitCard;
            char chCreditCard;
            char chInternetBanking;
            char chPhoneBanking;
            char chPassbook;
            char chOnlineShopping;

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LGR_BANK_CODE='" + BankCode + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtBankCode.Text = dv[iLoop]["LGR_BANK_CODE"].ToString();
                        txtBankName.Text = dv[iLoop]["LGR_BANK_NAME"].ToString();
                        txtBranchCode.Text = dv[iLoop]["BANK_BRANCH_CODE"].ToString();
                        txtAddress.Text = dv[iLoop]["BANK_BRANCH_ADD"].ToString();
                        txtACNo.Text = dv[iLoop]["BANK_AC_NO"].ToString();
                        txtACType.Text = dv[iLoop]["BANK_AC_TYPE"].ToString();
                        txtLedgerCode.Text = dv[iLoop]["LDGR_CODE"].ToString();
                        txtOpeningDate.Text = dv[iLoop]["OPENING_DATE"].ToString();
                        chCheck = char.Parse(dv[iLoop]["STATUS"].ToString());
                        chChequeBook = char.Parse(dv[iLoop]["CHEQ_BOOK"].ToString());
                        chDebitCard = char.Parse(dv[iLoop]["DEBIT_CARD"].ToString());
                        chCreditCard = char.Parse(dv[iLoop]["CREDIT_CARD"].ToString());
                        chInternetBanking = char.Parse(dv[iLoop]["INTERNET_BANKING"].ToString());
                        chPhoneBanking = char.Parse(dv[iLoop]["PHONE_BANKING"].ToString());
                        chPassbook = char.Parse(dv[iLoop]["PASSBOOK"].ToString());
                        chOnlineShopping = char.Parse(dv[iLoop]["ONLINE_SHOPPING"].ToString());
                        txtLedgerName.Text = dv[iLoop]["LDGR_NAME"].ToString();
                        cmbLedgerType.SelectedValue = dv[iLoop]["LDGR_TYPE"].ToString();
                        txtPrintName.Text = dv[iLoop]["PRINT_NAME"].ToString();
                        txtGroupCode.Text = dv[iLoop]["GRP_CODE"].ToString();
                        txtGroupName.Text = dv[iLoop]["GRP_NAME"].ToString();
                        cmbLedgerGroup.SelectedValue = dv[iLoop]["LDGR_Group"].ToString();
                        txtOpeningBalance.Text = dv[iLoop]["OP_BAL"].ToString();
                        txtRTGSCode.Text = dv[iLoop]["RTGS_CODE"].ToString();
                        txtDescription.Text = dv[iLoop]["LDGR_DESC"].ToString();

                        if (chCheck == '1')
                        {
                            chk_Status.Checked = true;
                        }
                        else
                        {
                            chk_Status.Checked = false;
                        }

                        if (chChequeBook == '1')
                        {
                            rdoListChequeBook.Items[0].Selected = true;
                            rdoListChequeBook.Items[1].Selected = false;
                        }
                        else
                        {
                            rdoListChequeBook.Items[0].Selected = false;
                            rdoListChequeBook.Items[1].Selected = true;
                        }

                        if (chDebitCard == '1')
                        {
                            rdoListDebitCard.Items[0].Selected = true;
                            rdoListDebitCard.Items[1].Selected = false;
                        }
                        else
                        {
                            rdoListDebitCard.Items[0].Selected = false;
                            rdoListDebitCard.Items[1].Selected = true;
                        }

                        if (chCreditCard == '1')
                        {
                            rdoListCreditCard.Items[0].Selected = true;
                            rdoListCreditCard.Items[1].Selected = false;
                        }
                        else
                        {
                            rdoListCreditCard.Items[0].Selected = false;
                            rdoListCreditCard.Items[1].Selected = true;
                        }

                        if (chInternetBanking == '1')
                        {
                            rdoListInternetBanking.Items[0].Selected = true;
                            rdoListInternetBanking.Items[1].Selected = false;
                        }
                        else
                        {
                            rdoListInternetBanking.Items[1].Selected = true;
                            rdoListInternetBanking.Items[0].Selected = false;
                        }

                        if (chPhoneBanking == '1')
                        {
                            rdoListPhoneBanking.Items[0].Selected = true;
                            rdoListPhoneBanking.Items[1].Selected = false;
                        }
                        else
                        {
                            rdoListPhoneBanking.Items[1].Selected = true;
                            rdoListPhoneBanking.Items[0].Selected = false;
                        }

                        if (chPassbook == '1')
                        {
                            rdoListPassbook.Items[0].Selected = true;
                            rdoListPassbook.Items[1].Selected = false;
                        }
                        else
                        {
                            rdoListPassbook.Items[1].Selected = true;
                            rdoListPassbook.Items[0].Selected = false;
                        }

                        if (chOnlineShopping == '1')
                        {
                            rdoListOnlineShopping.Items[0].Selected = true;
                            rdoListOnlineShopping.Items[1].Selected = false;
                        }
                        else
                        {
                            rdoListOnlineShopping.Items[1].Selected = true;
                            rdoListOnlineShopping.Items[0].Selected = false;
                        }
                    }
                    txtOpeningBalance.Enabled = false;
                    txtOpeningDate.Enabled = false;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void UpdateData()
    {
        try
        {
            if (txtBankCode.Text != "")
            {
                if (txtBankName.Text != "")
                {
                    if (txtBranchCode.Text != "")
                    {
                        if (txtACNo.Text != "")
                        {
                            if (txtACType.Text != "")
                            {
                                if (txtRTGSCode.Text != "")
                                {
                                    if (txtLedgerCode.Text != "")
                                    {
                                        if (txtLedgerName.Text != "")
                                        {
                                            if (cmbLedgerType.SelectedIndex != -1)
                                            {
                                                if (txtPrintName.Text != "")
                                                {
                                                    if (txtGroupCode.Text != "")
                                                    {
                                                        if (cmbLedgerGroup.SelectedIndex != -1)
                                                        {
                                                            oFA_BANK_MST = new SaitexDM.Common.DataModel.FA_BANK_MST();
                                                            oFA_LGR_MST = new SaitexDM.Common.DataModel.FA_LGR_MST();

                                                            oFA_BANK_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                                            oFA_BANK_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                                            oFA_BANK_MST.LDGR_CODE = txtLedgerCode.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.LGR_BANK_CODE = txtBankCode.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.LGR_BANK_NAME = txtBankName.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.BANK_BRANCH_CODE = txtBranchCode.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.BANK_BRANCH_ADD = txtAddress.Text.Trim();
                                                            oFA_BANK_MST.BANK_AC_NO = txtACNo.Text.ToUpper().Trim();
                                                            oFA_BANK_MST.BANK_AC_TYPE = txtACType.Text.Trim();
                                                            if (txtOpeningDate.Text != "")
                                                            {
                                                                oFA_BANK_MST.OPENING_DATE = DateTime.Parse(txtOpeningDate.Text.Trim());
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.OPENING_DATE = DateTime.Parse(DateTime.Now.ToShortDateString());
                                                            }

                                                            oFA_BANK_MST.RTGS_CODE = txtRTGSCode.Text.ToUpper().Trim();

                                                            if (rdoListChequeBook.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.CHEQ_BOOK = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.CHEQ_BOOK = false;
                                                            }

                                                            if (rdoListDebitCard.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.DEBIT_CARD = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.DEBIT_CARD = false;
                                                            }

                                                            if (rdoListCreditCard.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.CREDIT_CARD = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.CREDIT_CARD = false;
                                                            }

                                                            if (rdoListInternetBanking.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.INTERNET_BANKING = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.INTERNET_BANKING = false;
                                                            }

                                                            if (rdoListPhoneBanking.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.PHONE_BANKING = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.PHONE_BANKING = false;
                                                            }

                                                            if (rdoListPassbook.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.PASSBOOK = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.PASSBOOK = false;
                                                            }

                                                            if (rdoListOnlineShopping.Items[0].Selected == true)
                                                            {
                                                                oFA_BANK_MST.ONLINE_SHOPPING = true;
                                                            }
                                                            else
                                                            {
                                                                oFA_BANK_MST.ONLINE_SHOPPING = false;
                                                            }

                                                            oFA_BANK_MST.TUSER = oUserLoginDetail.UserCode;

                                                            if (chk_Status.Checked == true)
                                                            {
                                                                chStatus = true;
                                                            }
                                                            else
                                                            {
                                                                chStatus = false;
                                                            }
                                                            oFA_BANK_MST.STATUS = chStatus;
                                                            oFA_BANK_MST.DEL_STATUS = false;

                                                            oFA_LGR_MST.LDGR_NAME = txtLedgerName.Text.ToUpper().Trim();
                                                            oFA_LGR_MST.PRINT_NAME = txtPrintName.Text.ToUpper().Trim();
                                                            oFA_LGR_MST.GRP_CODE = txtGroupCode.Text;
                                                            oFA_LGR_MST.LDGR_DESC = txtDescription.Text.Trim();
                                                            if (txtOpeningBalance.Text == "")
                                                            {
                                                                oFA_LGR_MST.OP_BAL = 0;
                                                            }
                                                            else
                                                            {
                                                                oFA_LGR_MST.OP_BAL = float.Parse(txtOpeningBalance.Text.Trim());
                                                            }
                                                            oFA_LGR_MST.LDGR_TYPE = cmbLedgerType.SelectedValue.ToString().Trim();
                                                            oFA_LGR_MST.LDGR_Group = cmbLedgerGroup.SelectedValue.ToString().Trim();
                                                            oFA_LGR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                                                            oFA_LGR_MST.IS_DR_OP = true;        // For Banks Ledgers Always Debit

                                                            int iRecordFound = 0;

                                                            bool bResult = SaitexBL.Interface.Method.FA_BANK_MST.UpdateFABankMaster(oFA_BANK_MST, oFA_LGR_MST, out iRecordFound);

                                                            if (bResult)
                                                            {
                                                                Session["saveStatus"] = 1;
                                                                Response.Redirect("./FABankMaster.aspx?cId=U", false);
                                                            }
                                                            else if (iRecordFound > 0)
                                                            {
                                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                                                            }
                                                            else
                                                            {
                                                                Common.CommonFuction.ShowMessage("Dear! DLL Error in updating data..");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Common.CommonFuction.ShowMessage("Dear! Please select Ledger Group..");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Common.CommonFuction.ShowMessage("Dear! Please enter Group Code..");
                                                    }
                                                }
                                                else
                                                {
                                                    Common.CommonFuction.ShowMessage("Dear! Please enter Print Name..");
                                                }
                                            }
                                            else
                                            {
                                                Common.CommonFuction.ShowMessage("Dear! Please select Ledger type..");
                                            }
                                        }
                                        else
                                        {
                                            Common.CommonFuction.ShowMessage("Dear! Please enter Ledger Name..");
                                        }
                                    }
                                    else
                                    {
                                        Common.CommonFuction.ShowMessage("Dear! Please enter Ledger Code..");
                                    }
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Dear! Please enter RTGS Code..");
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Dear! Please enter Account Type..");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please enter Account Number..");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please enter Branch Code..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please enter Bank Name..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please enter Bank Code..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteData()
    {
        try
        {
            SaitexDM.Common.DataModel.FA_BANK_MST oFA_BANK_MST = new SaitexDM.Common.DataModel.FA_BANK_MST();
            SaitexDM.Common.DataModel.FA_LGR_MST oFA_LGR_MST = new SaitexDM.Common.DataModel.FA_LGR_MST();

            oFA_BANK_MST.COMP_CODE = CommonFuction.funFixQuotes(oUserLoginDetail.COMP_CODE);
            oFA_BANK_MST.BRANCH_CODE = CommonFuction.funFixQuotes(oUserLoginDetail.CH_BRANCHCODE);
            oFA_BANK_MST.LDGR_CODE = CommonFuction.funFixQuotes(txtLedgerCode.Text.ToString().Trim());
            oFA_LGR_MST.GRP_CODE = CommonFuction.funFixQuotes(txtGroupCode.Text);
            oFA_LGR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            if (txtBankCode.Visible == true)
            {
                oFA_BANK_MST.LGR_BANK_CODE = CommonFuction.funFixQuotes(txtBankCode.Text.ToString().Trim());
            }
            else
            {
                oFA_BANK_MST.LGR_BANK_CODE = CommonFuction.funFixQuotes(cmbBankCode.SelectedValue.ToString().Trim());
            }

            bool bResult = SaitexBL.Interface.Method.FA_BANK_MST.DeleteFABankMaster(oFA_BANK_MST, oFA_LGR_MST);

            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./FABankMaster.aspx?cId=D", false);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.! Pls enter valid Category Code.');", true);
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

    protected void lnkbtnGroupCode_Click(object sender, EventArgs e)
    {
        try
        {
            txtGroupCode.ReadOnly = false;
            string URL = "FAGroupTree.aspx";
            URL = URL + "?TextBoxId=" + txtGroupCode.ClientID;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=350,height=500');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in generating Group Master Tree PopUp..\r\nSee error log for detail."));
        }
    }

    protected void txtGroupCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);

                dv.RowFilter = "GRP_CODE= '" + txtGroupCode.Text.Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtGroupName.Text = dv[iLoop]["GRP_NAME"].ToString();
                    }
                }
            }
            txtGroupCode.ReadOnly = true;
            txtGroupName.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Group Name..\r\nSee error log for detail."));
        }
    }
    protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //BindData();
            grdBank.PageIndex = e.NewPageIndex;
            grdBank.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}