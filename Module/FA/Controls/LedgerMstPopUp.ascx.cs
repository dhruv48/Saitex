using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class FA_Controls_LedgerMstPopUp : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_LGR_MST oFA_LGR_MST;
    private static string TextBoxId;
    bool chStatus, chIsDebit;

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

                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                }
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
            lblMode.Text = "Save";
            lblErrorMessage.Text = "";
            lblMessage.Text = "";
            txtLedgerCode.ReadOnly = true;
            cmbLedgerCode.Visible = false;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            chk_Status.Checked = true;
            chk_Debit.Checked = true;
            txtLedgerCode.Text = "";
            txtGroupCode.Text = "";
            txtGroupName.Text = "";
            chk_Debit.Enabled = true;
            txtOpeningBalance.Enabled = true;

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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtLedgerCode.Visible = false;
            cmbLedgerCode.Visible = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
            txtLedgerCode.Text = "";
            txtGroupCode.Text = "";
            txtGroupName.Text = "";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //DeleteData();  // Commented By Rajesh 02 Feb 2011
            Common.CommonFuction.ShowMessage("Sorry! dear you can't delete any Ledger");
            lblMessage.Text = "Sorry! dear you can't delete any Ledger";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in deleting data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./LedgerMstPopUp.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Ledger_Mst_Rpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
        }
    }

    //protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        if (Session["RedirectURL"] != null)
    //        {
    //            Response.Redirect(Session["RedirectURL"].ToString(), false);
    //            Session["RedirectURL"] = null;
    //        }
    //        else
    //        {
    //            Response.Redirect("~/Admin/Pages/welcome.aspx", false);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
    //    }
    //}

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Help/LedgerMasterHelp.htm";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Help.\r\nSee error log for detail."));
        }
    }

    private void MaxLedgerCode()
    {
        string x = "";
        int y = 0;
        try
        {
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

    private void InsertData()
    {
        try
        {
            if (txtLedgerCode.Text != "")
            {
                if (txtLedgerName.Text != "")
                {
                    if (cmbLedgerType.SelectedIndex != 0)
                    {
                        if (cmbLedgerGroup.SelectedIndex != 0)
                        {
                            if (txtGroupCode.Text != "")
                            {
                                if (txtLedgerPrint.Text != "")
                                {
                                    if (ValidateGroupCode())
                                    {
                                        oFA_LGR_MST = new SaitexDM.Common.DataModel.FA_LGR_MST();

                                        if (chk_Status.Checked == true)
                                        {
                                            chStatus = true;
                                        }
                                        else
                                        {
                                            chStatus = false;
                                        }

                                        if (chk_Debit.Checked == true)
                                        {
                                            chIsDebit = true;
                                        }
                                        else
                                        {
                                            chIsDebit = false;
                                        }

                                        oFA_LGR_MST.STATUS = chStatus;
                                        oFA_LGR_MST.IS_DR_OP = chIsDebit;
                                        oFA_LGR_MST.LDGR_CODE = txtLedgerCode.Text.ToUpper();
                                        oFA_LGR_MST.LDGR_NAME = txtLedgerName.Text.Trim().ToUpper();
                                        oFA_LGR_MST.GRP_CODE = txtGroupCode.Text.ToUpper().Trim();
                                        oFA_LGR_MST.PRINT_NAME = txtLedgerPrint.Text.ToUpper().Trim();
                                        oFA_LGR_MST.LDGR_DESC = txtDescription.Text.Trim();
                                        oFA_LGR_MST.DEL_STATUS = false;
                                        oFA_LGR_MST.TUSER = oUserLoginDetail.UserCode;
                                        oFA_LGR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                        oFA_LGR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

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

                                        int iRecordFound = 0;

                                        bool bResult = SaitexBL.Interface.Method.FA_LGR_MST.InsertLedgerMaster(oFA_LGR_MST, out iRecordFound);

                                        if (bResult)
                                        {
                                            Session["saveStatus"] = 1;
                                            Response.Redirect("./LedgerMstPopUp.aspx?cId=S", false);
                                        }
                                        else if (iRecordFound > 0)
                                        {
                                            Common.CommonFuction.ShowMessage("This Record is already saved.. Please enter another.");
                                        }
                                        else
                                        {
                                            Common.CommonFuction.ShowMessage("Error.. in saving Ledger.");
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please! enter a valid group code...');", true);
                                    }
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Dear! Please enter Print Ledger..");
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Dear! Please provide Group Code..");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please select Ledger group..");
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
        catch
        {
            throw;
        }
    }

    private void UpdateData()
    {
        try
        {
            if (cmbLedgerCode.SelectedIndex != -1 || cmbLedgerCode.SelectedText != "")
            {
                if (txtLedgerName.Text != "")
                {
                    if (cmbLedgerType.SelectedIndex != 0)
                    {
                        if (cmbLedgerGroup.SelectedIndex != 0)
                        {
                            if (txtGroupCode.Text != "")
                            {
                                if (txtLedgerPrint.Text != "")
                                {
                                    if (ValidateGroupCode())
                                    {
                                        oFA_LGR_MST = new SaitexDM.Common.DataModel.FA_LGR_MST();

                                        if (chk_Status.Checked == true)
                                        {
                                            chStatus = true;
                                        }
                                        else
                                        {
                                            chStatus = false;
                                        }
                                        if (chk_Debit.Checked == true)
                                        {
                                            chIsDebit = true;
                                        }
                                        else
                                        {
                                            chIsDebit = false;
                                        }

                                        oFA_LGR_MST.STATUS = chStatus;
                                        oFA_LGR_MST.IS_DR_OP = chIsDebit;
                                        oFA_LGR_MST.LDGR_CODE = cmbLedgerCode.SelectedValue.ToString().ToUpper().Trim();
                                        oFA_LGR_MST.LDGR_NAME = txtLedgerName.Text.ToUpper().Trim();
                                        oFA_LGR_MST.PRINT_NAME = txtLedgerPrint.Text.ToUpper().Trim();
                                        oFA_LGR_MST.GRP_CODE = txtGroupCode.Text.ToUpper().Trim();
                                        oFA_LGR_MST.LDGR_DESC = txtDescription.Text.Trim();
                                        oFA_LGR_MST.TUSER = oUserLoginDetail.UserCode;
                                        oFA_LGR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                        oFA_LGR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

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

                                        int iRecordFound = 0;
                                        bool bResult = SaitexBL.Interface.Method.FA_LGR_MST.UpdateLedgerMaster(oFA_LGR_MST, out iRecordFound);

                                        if (bResult)
                                        {
                                            Session["saveStatus"] = 1;
                                            Response.Redirect("./LedgerMstPopUp.aspx?cId=U", false);
                                        }
                                        else if (iRecordFound > 0)
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please! enter a valid group code...');", true);
                                    }
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Dear! Please enter Print Ledger..");
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Dear! Please provide Group Code..");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please select Ledger group..");
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
        catch
        {
            throw;
        }
    }

    private void DeleteData()
    {
        try
        {
            SaitexDM.Common.DataModel.FA_LGR_MST oFA_LGR_MST = new SaitexDM.Common.DataModel.FA_LGR_MST();
            oFA_LGR_MST.COMP_CODE = CommonFuction.funFixQuotes(oUserLoginDetail.COMP_CODE);
            oFA_LGR_MST.BRANCH_CODE = CommonFuction.funFixQuotes(oUserLoginDetail.CH_BRANCHCODE);
            oFA_LGR_MST.GRP_CODE = CommonFuction.funFixQuotes(txtGroupCode.Text.Trim());
            oFA_LGR_MST.LDGR_CODE = CommonFuction.funFixQuotes(cmbLedgerCode.SelectedValue.ToString().Trim());
            oFA_LGR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            bool bResult = SaitexBL.Interface.Method.FA_LGR_MST.DeleteLedgerMaster(oFA_LGR_MST);

            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./LedgerMstPopUp.aspx?cId=D", false);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.! Pls enter valid Category Code.');", true);
        }
        catch
        {
            throw;
        }
    }

    private void HelpData()
    {
        try
        {

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
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        else if (icond == 2)             // For Ledger Code
        {
            string whereClause = " WHERE LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY LDGR_CODE";
            string commandText = "SELECT * FROM FA_LGR_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        else if (icond == 3)          // For Ledger  Type
        {
            string whereClause = " WHERE MST_NAME LIKE 'LDGR_TYPE' And DEL_STATUS = '0' And MST_CODE like :SearchQuery";
            string sortExpression = " ORDER BY MST_CODE";
            string commandText = "SELECT * FROM TX_MASTER_TRN";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        else                       // For Ledger Group
        {
            string whereClause = " WHERE MST_NAME LIKE 'LDGR_GROUP' And DEL_STATUS = '0' And MST_CODE like :SearchQuery";
            string sortExpression = " ORDER BY MST_CODE";
            string commandText = "SELECT * FROM TX_MASTER_TRN";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
    }

    protected int GetItemsCount(string text, int icond)
    {
        if (icond == 1)        // For Group Code
        {
            string CommandText = "SELECT COUNT(*) FROM FA_Grp_Mst WHERE GRP_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        else if (icond == 2)             // For Ledger Code
        {
            string CommandText = "SELECT COUNT(*) FROM FA_LGR_MST WHERE LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        else if (icond == 3)          // For Ledger  Type
        {
            string CommandText = "SELECT COUNT(*) FROM TX_MASTER_TRN WHERE MST_NAME = 'LDGR_TYPE' And DEL_STATUS = '0' And MST_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        else                       // For Ledger Group
        {
            string CommandText = "SELECT COUNT(*) FROM TX_MASTER_TRN WHERE MST_NAME = 'LDGR_GROUP' And DEL_STATUS = '0' And MST_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
        }
    }

    protected void cmbLedgerCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10, 2);

            cmbLedgerCode.DataSource = data;
            cmbLedgerCode.DataBind();

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text.ToUpper(), 2);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Loading Ledger.\r\nSee error log for detail."));
        }
    }

    protected void cmbLedgerCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMasterWithOutStatus();
            char chCheck, chDebit;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LDGR_CODE='" + cmbLedgerCode.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtLedgerCode.Text = dv[iLoop]["LDGR_CODE"].ToString();
                        txtLedgerName.Text = dv[iLoop]["LDGR_NAME"].ToString();
                        txtGroupCode.Text = dv[iLoop]["GRP_CODE"].ToString();
                        txtGroupName.Text = dv[iLoop]["GRP_NAME"].ToString();
                        txtLedgerPrint.Text = dv[iLoop]["PRINT_NAME"].ToString();
                        txtDescription.Text = dv[iLoop]["LDGR_DESC"].ToString();
                        txtOpeningBalance.Text = dv[iLoop]["OP_BAL"].ToString();
                        chCheck = char.Parse(dv[iLoop]["STATUS"].ToString());
                        chDebit = char.Parse(dv[iLoop]["IS_DR_OP"].ToString());
                        cmbLedgerType.SelectedValue = dv[iLoop]["LDGR_TYPE"].ToString();
                        cmbLedgerGroup.SelectedValue = dv[iLoop]["LDGR_Group"].ToString();

                        if (chCheck == '1')
                        {
                            chk_Status.Checked = true;
                        }
                        else if (chCheck == '0')
                        {
                            chk_Status.Checked = false;
                        }
                        else
                        {
                            chk_Status.Checked = false;
                        }

                        if (chDebit == '1')
                        {
                            chk_Debit.Checked = true;
                        }
                        else if (chDebit == '0')
                        {
                            chk_Debit.Checked = false;
                        }
                        else
                        {
                            chk_Debit.Checked = false;
                        }
                    }
                    chk_Debit.Enabled = false;
                    txtOpeningBalance.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Ledger.\r\nSee error log for detail."));
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

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        //imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private bool ValidateGroupCode()
    {
        try
        {
            bool IsValidGroup = false;
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);

                dv.RowFilter = "GRP_CODE='" + txtGroupCode.Text.Trim() + "'";
                if (dv.Count > 0)
                {
                    IsValidGroup = true;
                }
            }
            return IsValidGroup;
        }
        catch
        {
            return false;
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
        catch
        {
            throw;
        }
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Loading Group Tree PopUp..\r\nSee error log for detail."));
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            double Total = 0;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + Total + "','" + TextBoxId + "')", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Close..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}