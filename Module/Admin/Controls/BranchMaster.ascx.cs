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

using Common;
using errorLog;
using DBLibrary;

public partial class Module_Admin_Controls_BranchMaster : System.Web.UI.UserControl
{
    string strTUser = string.Empty;
    bool chStatus;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.CM_BRANCH_MST oCM_BRANCH_MST;
    SaitexDM.Common.DataModel.TX_VENDOR_MST oTX_VENDOR_MST;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strTUser = oUserLoginDetail.UserCode;

            if (!IsPostBack)
            {
                bindCompanyMaster();
                Control_Initial();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void Control_Initial()
    {
        try
        {
            tdSave.Visible = true;
            tdFind.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            txtPrefix.Enabled = true;
            lblMode.Text = "You are in Save Mode";
            txtBranchCode.Visible = true;
            chk_Status.Checked = true;
            lblMessage.Text = string.Empty;
            grdShowBranch.AutoPostBackOnSelect = false;
            cmbCompanyName.Enabled = true;
            txtBranchCode.Enabled = true;
            cmbCompanyName.SelectedIndex = -1;
            txtBranchAddress.Text = string.Empty;
            txtBranchCode.Text = string.Empty;
            txtBranchContactNumber.Text = string.Empty;
            txtbranchcstno.Text = string.Empty;
            txtBranchEmailId.Text = string.Empty;
            txtBranchESINumber.Text = string.Empty;
            txtBranchFaxNumber.Text = string.Empty;
            txtbranchid.Text = string.Empty;
            txtbranchlstdate.Text = string.Empty;
            txtbranchlstno.Text = string.Empty;
            txtBranchName.Text = string.Empty;
            txtBranchPFNumber.Text = string.Empty;
            txtBranchServiceTaxNo.Text = string.Empty;
            txtbranchtinno.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtPrefix.Text = string.Empty;
            branchcstdate.Text = string.Empty;
            BranchTinDate.Text = string.Empty;
            txtPartyCode.Text = string.Empty;
            bindBranchGrid();
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
            if (cmbCompanyName.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Select Company Name.');", true);
            }
            else
            {
                if (InsertData("I"))
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("./BranchMaster.aspx?cId=S", false);
                }
                else

                {
                    Common.CommonFuction.ShowMessage("Problem In Inserting Data");
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        
        try
        {

            if (InsertData("U"))
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./BranchMaster.aspx?cId=U", false);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem In updating Data");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Update.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DeleteData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./BranchMaster.aspx", false);
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
            string URL = "../Reports/Branch_Mst_Rpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            grdShowBranch.AutoPostBackOnSelect = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
            cmbBranchCode.Enabled = false;
            cmbCompanyName.Enabled = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Find.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindCompanyMaster()
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems("", 0, 10, true);
            cmbCompanyName.DataSource = data;
            cmbCompanyName.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems, bool Flag)
    {
        try
        {
            if (Flag)
            {
                string whereClause = " WHERE COMP_CODE like :SearchQuery And DEL_STATUS = '0' or COMP_NAME like :SearchQuery And DEL_STATUS = '0'";
                string sortExpression = " ORDER BY COMP_CODE";
                string commandText = "SELECT * FROM  CM_COMPANY_MST";
                string sPO = "";
                DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
                return dt;
            }
            else
            {
                string whereClause = " WHERE BRANCH_CODE like :SearchQuery And DEL_STATUS = '0' And COMP_CODE = '" + cmbCompanyName.SelectedValue.Trim() + "' or BRANCH_NAME like :SearchQuery And DEL_STATUS = '0' And COMP_CODE = '" + cmbCompanyName.SelectedValue.Trim() + "'";
                string sortExpression = " ORDER BY BRANCH_CODE";
                string commandText = "SELECT * FROM CM_BRANCH_MST";
                string sPO = "";
                DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
                return dt;
            }
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text, bool Flag)
    {
        try
        {
            if (Flag)
            {
                string CommandText = "SELECT COUNT(*) FROM CM_COMPANY_MST WHERE COMP_CODE like :SearchQuery And DEL_STATUS = '0' or COMP_NAME like :SearchQuery And DEL_STATUS = '0'";
                return SaitexBL.Interface.Method.CM_BRANCH_MST.GetCountForLOV(CommandText, text + '%', "");
            }
            else
            {
                string CommandText = "SELECT COUNT(*) FROM CM_BRANCH_MST WHERE BRANCH_CODE like :SearchQuery And DEL_STATUS = '0' or BRANCH_NAME like :SearchQuery And DEL_STATUS = '0'";
                return SaitexBL.Interface.Method.CM_BRANCH_MST.GetCountForLOV(CommandText, text + '%', "");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void cmbCompanyName_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10, true);

            cmbCompanyName.Items.Clear();
            cmbCompanyName.DataSource = data;
            cmbCompanyName.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text, true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Company.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindBranchGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                grdShowBranch.DataSource = dt;
                grdShowBranch.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdShowBranch_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            char chCheck;
            cmbBranchCode.Visible = false;
            txtBranchCode.Visible = true;
            txtBranchCode.Enabled = false;
            txtPrefix.Enabled = false;

            ArrayList ar = grdShowBranch.SelectedRecords;

            lblMessage.Text = "";
            tdClear.Visible = true;
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;

            Hashtable ht = (Hashtable)ar[0];

            cmbCompanyName.SelectedValue = ht["COMP_CODE"].ToString().Trim();
            txtBranchCode.Text = ht["BRANCH_CODE"].ToString().Trim();
            txtBranchName.Text = ht["BRANCH_NAME"].ToString().Trim();
            txtbranchid.Text = ht["BRANCH_ID"].ToString().ToUpper().Trim();
            txtBranchAddress.Text = ht["BRANCH_ADD"].ToString().Trim();
            txtBranchContactNumber.Text = ht["BRANCH_CONT_NO"].ToString().Trim();
            txtBranchEmailId.Text = ht["BRANCH_MAIL_ID"].ToString().Trim();
            txtBranchFaxNumber.Text = ht["BRANCH_FAX_NO"].ToString().Trim();
            txtBranchESINumber.Text = ht["BRANCH_ESI_NO"].ToString().Trim();
            txtBranchPFNumber.Text = ht["BRANCH_PF_NO"].ToString().Trim();
            txtBranchServiceTaxNo.Text = ht["BRANCH_SERVICE_TAX_NO"].ToString().Trim();
            txtRemarks.Text = ht["BRANCH_REMARKS"].ToString().Trim();
            chCheck = char.Parse(ht["STATUS"].ToString());
            if (chCheck == '1')
            {
                chk_Status.Checked = true;
            }
            else
            {
                chk_Status.Checked = false;
            }
            cmbCompanyName.Enabled = false;

            txtbranchcstno.Text = ht["BRANCH_CST_NO"].ToString().Trim();
            txtbranchlstno.Text = ht["BRANCH_LST_NO"].ToString().Trim();
            txtbranchtinno.Text = ht["BRANCH_TIN_NO"].ToString().Trim();
            branchcstdate.Text = ht["BRANCH_CST_DATE"].ToString().Trim();
            txtbranchlstdate.Text = ht["BRANCH_LST_DATE"].ToString().Trim();
            BranchTinDate.Text = ht["BRANCH_TIN_DATE"].ToString().Trim();
            txtPartyCode.Text = ht["PARTY_CODE"].ToString().Trim();
            txtPrefix.Text = ht["SEQ_PREFIX"].ToString().Trim();

            txtState.Text = ht["STATE"].ToString();
            txtEccNo.Text = ht["ECC_NO"].ToString();
            txtFactoryLcNo.Text = ht["F_LC_NO"].ToString();
            txtAirPolution.Text = ht["AIR_POLUTION_NO"].ToString(); 
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool InsertData(string IsInsert)
    {
        bool bcstdate;
        bool blstdate;
        bool btindate;
        bool res = false;
        try
        {
            oCM_BRANCH_MST = new SaitexDM.Common.DataModel.CM_BRANCH_MST();
            oTX_VENDOR_MST = new SaitexDM.Common.DataModel.TX_VENDOR_MST();

            oCM_BRANCH_MST.COMP_CODE = cmbCompanyName.SelectedValue.ToString().Trim();
            oCM_BRANCH_MST.BRANCH_CODE = txtBranchCode.Text.ToUpper().Trim();
            oCM_BRANCH_MST.BRANCH_NAME = txtBranchName.Text.ToUpper().Trim();
            oCM_BRANCH_MST.BRANCH_ID = txtbranchid.Text.ToUpper().Trim();
            oTX_VENDOR_MST.PRTY_ADD1 = oTX_VENDOR_MST.PRTY_ADD2 = oCM_BRANCH_MST.BRANCH_ADD = txtBranchAddress.Text.Trim();
            oCM_BRANCH_MST.BRANCH_CONT_NO = txtBranchContactNumber.Text.Trim();
            oCM_BRANCH_MST.BRANCH_MAIL_ID = txtBranchEmailId.Text.Trim();
            oCM_BRANCH_MST.BRANCH_FAX_NO = txtBranchFaxNumber.Text.Trim();
            oCM_BRANCH_MST.BRANCH_ESI_NO = txtBranchESINumber.Text.Trim();
            oCM_BRANCH_MST.BRANCH_PF_NO = txtBranchPFNumber.Text.Trim();
            oCM_BRANCH_MST.BRANCH_SERVICE_TAX_NO = txtBranchServiceTaxNo.Text.Trim();
            oCM_BRANCH_MST.BRANCH_REMARKS = txtRemarks.Text.Trim();
            oCM_BRANCH_MST.TUSER = strTUser;
            oTX_VENDOR_MST.PRTY_CSTNO=oCM_BRANCH_MST.BRANCH_CST_NO = txtbranchcstno.Text.Trim();
            oTX_VENDOR_MST.PRTY_LSTNO=oCM_BRANCH_MST.BRANCH_LST_NO = txtbranchlstno.Text.Trim();
            oTX_VENDOR_MST.PRTY_TINNO=oCM_BRANCH_MST.BRANCH_TIN_NO = txtbranchtinno.Text.Trim();
            oCM_BRANCH_MST.PARTY_CODE = txtPartyCode.Text.ToUpper().Trim();
            oCM_BRANCH_MST.SEQ_PREFIX = txtPrefix.Text.ToUpper().Trim();

            DateTime cstdates = System.DateTime.Now;
            bcstdate = DateTime.TryParse(txtbranchlstdate.Text, out cstdates);
            oTX_VENDOR_MST.PRTY_CSTDT=oCM_BRANCH_MST.BRANCH_CST_DATE = cstdates;

            DateTime lstdates = System.DateTime.Now;
            blstdate = DateTime.TryParse(txtbranchlstdate.Text, out lstdates);
            oTX_VENDOR_MST.PRTY_LSTDT= oCM_BRANCH_MST.BRANCH_LST_DATE = lstdates;

            DateTime tindates = System.DateTime.Now;
            btindate = DateTime.TryParse(BranchTinDate.Text, out tindates);
            oTX_VENDOR_MST.PRTY_TINDT=oCM_BRANCH_MST.BRANCH_TIN_DATE = tindates;

            oTX_VENDOR_MST.PRTY_STATE = oTX_VENDOR_MST.PRTY_CITY = oCM_BRANCH_MST.STATE = txtState.Text.Trim();
            oTX_VENDOR_MST.ECC_NO= oCM_BRANCH_MST.ECC_NO = txtEccNo.Text.Trim();
            oCM_BRANCH_MST.F_LC_NO = txtFactoryLcNo.Text.Trim();
            oCM_BRANCH_MST.AIR_POLUTION_NO = txtAirPolution.Text.Trim();

            // For Vendor Master Entry
            oTX_VENDOR_MST.PRTY_CODE = txtPartyCode.Text.ToUpper().Trim();
            oTX_VENDOR_MST.PRTY_NAME = txtPartyName.Text.ToUpper().Trim();
            oTX_VENDOR_MST.VENDOR_CAT_CODE = "Vendor";
            oTX_VENDOR_MST.PIN_CODE = 123456;
            oTX_VENDOR_MST.EMAIL = txtBranchEmailId.Text.Trim();
            oTX_VENDOR_MST.SERVICE_TAX_NO = "NA";
            oTX_VENDOR_MST.BANK_NAME = "NA";
            oTX_VENDOR_MST.BRANCH = "NA";
            oTX_VENDOR_MST.ACCOUNT_NO = "NA";
            oTX_VENDOR_MST.NEFT_RTGS_CODE = "NA";
            oTX_VENDOR_MST.PERSON1_EMAIL = txtBranchEmailId.Text.Trim();
            oTX_VENDOR_MST.PERSON2_EMAIL = txtBranchEmailId.Text.Trim();
            oTX_VENDOR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_VENDOR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_VENDOR_MST.COUNTRY = "INDIA";
            oTX_VENDOR_MST.PRTY_PANNO = string.Empty;
            oTX_VENDOR_MST.PRTY_ADD3 = string.Empty ;
            
                  
                   
                    
                   


            //

            int iRecordFound = 0;

            bool bResult = SaitexBL.Interface.Method.CM_BRANCH_MST.InsertBranchMaster(oCM_BRANCH_MST, out iRecordFound, bcstdate, blstdate, btindate, IsInsert, oTX_VENDOR_MST);
            if (bResult)
            {
                Control_Initial();
                res = true;
            }
            else if (iRecordFound == 1)
            {
                Common.CommonFuction.ShowMessage("This Prefix is already taken.. Please enter another prefix.");
            }
            else if (iRecordFound == 2)
            {
                Common.CommonFuction.ShowMessage("This Branch Code And Name are already used in Vendor Master.. Please enter another prefix.");
            }
            else if (iRecordFound == 3)
            {
                Common.CommonFuction.ShowMessage("This Branch Code And Name are already used in Ledger Master.. Please enter another prefix.");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Error.. in saving.");
                res = false;
            }
            return res;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbBranchCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10, false);

            cmbBranchCode.DataSource = data;
            cmbBranchCode.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text, false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Branch.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbBranchCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            char chCheck;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "BRANCH_CODE='" + cmbBranchCode.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    { 
                        cmbCompanyName.SelectedValue = dv[iLoop]["COMP_CODE"].ToString();
                        txtBranchCode.Text = dv[iLoop]["BRANCH_CODE"].ToString();
                        txtBranchName.Text = dv[iLoop]["BRANCH_NAME"].ToString();
                        txtBranchAddress.Text = dv[iLoop]["BRANCH_ADD"].ToString();
                        txtBranchContactNumber.Text = dv[iLoop]["BRANCH_CONT_NO"].ToString();
                        txtBranchEmailId.Text = dv[iLoop]["BRANCH_MAIL_ID"].ToString();
                        txtBranchFaxNumber.Text = dv[iLoop]["BRANCH_FAX_NO"].ToString();
                        txtBranchESINumber.Text = dv[iLoop]["BRANCH_ESI_NO"].ToString();
                        txtBranchPFNumber.Text = dv[iLoop]["BRANCH_PF_NO"].ToString();
                        txtBranchServiceTaxNo.Text = dv[iLoop]["BRANCH_SERVICE_TAX_NO"].ToString();
                        txtRemarks.Text = dv[iLoop]["BRANCH_REMARKS"].ToString();
                        txtState.Text=dv[iLoop]["STATE"].ToString();
                        txtEccNo.Text=dv[iLoop]["ECC_NO"].ToString();
                        txtFactoryLcNo.Text=dv[iLoop]["F_LC_NO"].ToString();
                        txtAirPolution.Text=dv[iLoop]["AIR_POLUTION_NO"].ToString();                       
                        chCheck = char.Parse(dv[iLoop]["STATUS"].ToString());
                        if (chCheck == '1')
                        {
                            chk_Status.Checked = true;
                        }
                        else
                        {
                            chk_Status.Checked = false;
                        }
                    }
                    cmbCompanyName.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Branch.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void DeleteData()
    {
        try
        {
            SaitexDM.Common.DataModel.CM_BRANCH_MST oCM_BRANCH_MST = new SaitexDM.Common.DataModel.CM_BRANCH_MST();

            oCM_BRANCH_MST.COMP_CODE = cmbCompanyName.SelectedValue.ToString().Trim();
            oCM_BRANCH_MST.TUSER = strTUser;

            if (txtBranchCode.Visible == true)
            {
                oCM_BRANCH_MST.BRANCH_CODE = txtBranchCode.Text.Trim();
            }
            else
            {
                oCM_BRANCH_MST.BRANCH_CODE = cmbBranchCode.SelectedValue.ToString().Trim();
            }

            bool bResult = SaitexBL.Interface.Method.CM_BRANCH_MST.DeleteBranchMaster(oCM_BRANCH_MST);

            if (bResult)
            {
                Control_Initial();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.!');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void cmbCompanyName_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (tdUpdate.Visible == true)
            {
                txtBranchCode.Visible = false;
                cmbBranchCode.Visible = true;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Company.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtBranchName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string strBranch = string.Empty;

            if (txtBranchCode.Text != string.Empty)
            {
                strBranch = txtBranchCode.Text.ToUpper().Trim();
                txtPrefix.Text = strBranch.Remove(2);

                if (txtBranchCode.Text != string.Empty)
                {
                    txtPartyCode.Text = strBranch;
                    txtPartyName.Text = txtBranchName.Text.ToUpper().Trim();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please enter Branch Name..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please enter Branch Code..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Branch Code TextChanged Event.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}
