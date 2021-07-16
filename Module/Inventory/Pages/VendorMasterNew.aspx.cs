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
using Common;
using errorLog;

public partial class Module_Inventory_Pages_VendorMasterNew : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    DataTable dtVendorDetail = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                disableValidator();
                BlanksControls();
                bindVendorCategory();
                bindPartyGroup();
                BindState();                
                chk_Status.Checked = true;
                CST.Visible = false;
                LST_NO.Visible = false;
                TIN_NO.Visible = false;
                Service_no.Visible = false;
                INSU.Visible = false;
                ADD.Visible = false;
                ADD3.Visible = false;
                CITY.Visible = false;
                txtVendCode.Text = getVendorCode();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading..\r\nSee error log for detail."));
        }

    }

    private void SaveVendorData()
    {
        if (Page.IsValid)
        {
            if (ddlVendorCategory.SelectedIndex >= 0)
            {
                 try
                    {
                        SaitexDM.Common.DataModel.TX_VENDOR_MST oTX_VENDOR_MST = new SaitexDM.Common.DataModel.TX_VENDOR_MST();
                        bool bDup = false;
                        txtVendCode.Text = getVendorCode();
                        oTX_VENDOR_MST.PRTY_CODE = txtVendCode.Text.ToUpper().Trim();
                        oTX_VENDOR_MST.PRTY_GRP_CODE = ddlPartyGroupCode.SelectedValue.Trim();
                        oTX_VENDOR_MST.PRTY_NAME = txtVendorName.Text.ToUpper().Trim();
                        oTX_VENDOR_MST.PRTY_ADD1 = txtAddress.Text.Trim();// txtVendAdd1.Text.Trim();
                        oTX_VENDOR_MST.PRTY_ADD2 = txtAddress1.Text.Trim();// txtVendAdd2.Text.Trim();
                        oTX_VENDOR_MST.PRTY_ADD3 = txtVendAdd3.Text.Trim();
                        oTX_VENDOR_MST.PRTY_CITY = txtVendCity.Text.Trim();
                        oTX_VENDOR_MST.PRTY_STATE = txtVendState.Text.Trim();
                        int pincode = 0;
                        int.TryParse(txtVendPostalCode.Text.Trim(), out pincode);
                        oTX_VENDOR_MST.PIN_CODE = pincode;
                        oTX_VENDOR_MST.COUNTRY = txtVendCountry.Text.Trim();
                        oTX_VENDOR_MST.PHONE = txtVendPhone.Text.Trim();
                        oTX_VENDOR_MST.FAX = txtVendFax.Text.Trim();
                        oTX_VENDOR_MST.PERSON1_NAME = txtVendCP1_Name.Text.Trim();
                        oTX_VENDOR_MST.REMARKS = txtVendRemarks.Text.Trim();
                        oTX_VENDOR_MST.EMAIL = txtVendEmail.Text.Trim();
                        oTX_VENDOR_MST.PRTY_STATUS = chk_Status.Checked;
                        oTX_VENDOR_MST.PRTY_PANNO = txtVendPan.Text.ToUpper().Trim();
                        oTX_VENDOR_MST.PRTY_LSTNO = txtVendLST_No.Text.ToUpper().Trim();
                        oTX_VENDOR_MST.PRTY_CSTNO = txtVendCST_No.Text.ToUpper().Trim();
                        oTX_VENDOR_MST.PRTY_TINNO = txtVendTINNo.Text.ToUpper().Trim();

                        DateTime dd = System.DateTime.Now.Date;
                        bool Is_Tin = false;
                        if (DateTime.TryParse(txtVendTINDate.Text.Trim(), out dd))
                            Is_Tin = true;
                        oTX_VENDOR_MST.PRTY_TINDT = dd;

                        oTX_VENDOR_MST.TIN_TYPE = txtVendTinType.Text.Trim();
                        oTX_VENDOR_MST.MOB_NO = txtVendMobile.Text.Trim();
                        oTX_VENDOR_MST.WEBSITE = txtVendWebsite.Text.Trim();
                        oTX_VENDOR_MST.PRODUCT = txtVendProduct.Text.Trim();
                        oTX_VENDOR_MST.STAX_TYPE = txtVendStaxType.Text.Trim();
                        oTX_VENDOR_MST.INS_POLICY_NO = txtVendINSPolicy.Text.Trim();
                        oTX_VENDOR_MST.VENDOR_CAT_CODE = ddlVendorCategory.SelectedValue.Trim();
                        oTX_VENDOR_MST.STATUS = true;
                        oTX_VENDOR_MST.TUSER = oUserLoginDetail.UserCode;
                        dd = System.DateTime.Now.Date;
                        bool is_CST = false;
                        if (DateTime.TryParse(txtVendCST_DT.Text.Trim(), out dd))
                            is_CST = true;
                        oTX_VENDOR_MST.PRTY_CSTDT = dd;

                        dd = System.DateTime.Now.Date;
                        bool is_LST = false;
                        if (DateTime.TryParse(txtVendLST_DT.Text.Trim(), out dd))
                            is_LST = true;
                        oTX_VENDOR_MST.PRTY_LSTDT = dd;

                        oTX_VENDOR_MST.PERSON2_NAME = txtVendCP2_Name.Text.Trim();
                        oTX_VENDOR_MST.PERSON1_DESIG = txtVendCP1_Desig.Text.Trim();

                        oTX_VENDOR_MST.PERSON1_LL = txtVendCP1_LL.Text.Trim();

                        oTX_VENDOR_MST.PERSON1_MOB = txtVendCP1_Mob.Text.Trim();
                        oTX_VENDOR_MST.PERSON1_EMAIL = txtVendCP1_Mail.Text.Trim();
                        oTX_VENDOR_MST.PERSON2_DESIG = txtVendCP2_Desig.Text.Trim();

                        oTX_VENDOR_MST.PERSON2_LL = txtVendCP2_LL.Text.Trim();

                        oTX_VENDOR_MST.PERSON2_MOB = txtVendCP2_Mob.Text.Trim();
                        oTX_VENDOR_MST.PERSON2_EMAIL = txtVendCP2_Mail.Text.Trim();
                        oTX_VENDOR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oTX_VENDOR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                        //Added by sandeep on 25- Feb-2011 
                        oTX_VENDOR_MST.SERVICE_TAX_NO = txtServiceTaxNo.Text.Trim();
                        oTX_VENDOR_MST.BANK_NAME = txtBankName.Text.Trim();
                        oTX_VENDOR_MST.BRANCH = txtBranchName.Text.Trim();
                        oTX_VENDOR_MST.ACCOUNT_NO = txtAccountNo.Text.Trim();
                        oTX_VENDOR_MST.NEFT_RTGS_CODE = txtNEFTRTGCCode.Text.Trim();
                        oTX_VENDOR_MST.ECC_NO = txtEccNo.Text.Trim();
                        double crlimit = 0;
                        double.TryParse(txtCrLimit.Text, out crlimit);
                        oTX_VENDOR_MST.CR_LIMIT = crlimit;
                        oTX_VENDOR_MST.GROUP_CODE = ddlPartyGroupCode.SelectedValue;


                        if (uploadPan.HasFile)
                        {
                            uploadPan.PostedFile.SaveAs(Server.MapPath(@"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadPan.FileName.Trim()));
                            oTX_VENDOR_MST.PAN_IMG_PATH = @"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadPan.FileName.Trim();
                        }
                        else
                        {
                            oTX_VENDOR_MST.PAN_IMG_PATH = string.Empty;
                        }
                        if (uploadLST.HasFile)
                        {
                            uploadLST.PostedFile.SaveAs(Server.MapPath(@"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadLST.FileName.Trim()));
                            oTX_VENDOR_MST.LST_IMG_PATH = @"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadLST.FileName.Trim();
                        }
                        else
                        {
                            oTX_VENDOR_MST.LST_IMG_PATH = string.Empty;
                        }
                        if (uploadLST.HasFile)
                        {
                            uploadCST.PostedFile.SaveAs(Server.MapPath(@"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadCST.FileName.Trim()));
                            oTX_VENDOR_MST.CST_IMG_PATH = @"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadCST.FileName.Trim();
                        }
                        else
                        {
                            oTX_VENDOR_MST.CST_IMG_PATH = string.Empty;
                        }
                        oTX_VENDOR_MST.REGION = ddlRegion.SelectedValue;

                        if (ViewState["dtVendorDetail"] != null)
                            dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];


                        bool bResult = SaitexBL.Interface.Method.TX_VENDOR_MST.InsertGstVendor(oTX_VENDOR_MST, dtVendorDetail, out bDup, is_CST, is_LST, Is_Tin);
                        if (bResult && SaveUserMaster())
                        {
                            BlanksControls();
                            txtVendCode.Text = getVendorCode();
                            
                            CommonFuction.ShowMessage("Vendor Data saved Successfully");
                        }
                        else if (bDup)
                        {
                            CommonFuction.ShowMessage("Vendor Already Exists");
                        }
                        else
                        {
                            CommonFuction.ShowMessage("Vendor Data saving failed");
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    CommonFuction.ShowMessage("Pls select vendor category");
                }
            }
        
    }


    protected bool SaveUserMaster()
    {
        bool Result = false;

        try
        {

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.CM_USER_MST oCM_USER_MST = new SaitexDM.Common.DataModel.CM_USER_MST();
            oCM_USER_MST.USER_CODE = CommonFuction.funFixQuotes(txtVendCode.Text.Trim());
            oCM_USER_MST.USER_LOG_ID = CommonFuction.funFixQuotes(txtLoginId.Text.Trim());
            oCM_USER_MST.USER_NAME = CommonFuction.funFixQuotes(txtVendorName.Text.Trim());
            oCM_USER_MST.USER_PASS = CommonFuction.funFixQuotes(txtPassword.Text.ToString().Trim());
            oCM_USER_MST.USER_REMARKS = txtVendRemarks.Text;
            oCM_USER_MST.USER_TYPE = CommonFuction.funFixQuotes(ddlVendorCategory.SelectedValue.Trim().ToUpper());
            oCM_USER_MST.TUSER = oUserLoginDetail.UserCode;
            oCM_USER_MST.STATUS = true;
            oCM_USER_MST.DEL_STATUS = false;
            int iRecordFound = 0;
            Result = SaitexBL.Interface.Method.CM_USER_MST.Insert(oCM_USER_MST, out iRecordFound);
            if (Result)
            {

                txtLoginId.Text = string.Empty;
                txtPassword.Attributes.Add("value", string.Empty);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('User Master Duplicate Entry');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + ex.Message + "');", true);
        }

        return Result;

    }

    protected bool UpdateUserMaster()
    {
        bool Result = false;
        try
        {

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.CM_USER_MST oCM_USER_MST = new SaitexDM.Common.DataModel.CM_USER_MST();
            oCM_USER_MST.USER_CODE = CommonFuction.funFixQuotes(txtVendCode.Text.Trim());
            oCM_USER_MST.USER_LOG_ID = CommonFuction.funFixQuotes(txtLoginId.Text.Trim());
            oCM_USER_MST.USER_NAME = CommonFuction.funFixQuotes(txtVendorName.Text.Trim());
            oCM_USER_MST.USER_PASS = CommonFuction.funFixQuotes(txtPassword.Text.ToString().Trim());
            oCM_USER_MST.USER_REMARKS = txtVendRemarks.Text;
            oCM_USER_MST.USER_TYPE = CommonFuction.funFixQuotes(ddlVendorCategory.SelectedValue.Trim().ToUpper());
            oCM_USER_MST.TUSER = oUserLoginDetail.UserCode;
            oCM_USER_MST.STATUS = true;
            oCM_USER_MST.DEL_STATUS = false;
            int iRecordFound = 0;
            Result = SaitexBL.Interface.Method.CM_USER_MST.Update(oCM_USER_MST, out iRecordFound);
            if (Result)
            {
                txtLoginId.Text = string.Empty;
                txtPassword.Attributes.Add("value", string.Empty);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Login Duplicate Entry');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + ex.Message + "');", true);
        }

        return Result;

    }



    private void bindVendorCategory()
    {
        try
        {
            ddlVendorCategory.Items.Clear();
            DataTable dt = GET_MOM_DATA("", "PRTY_TYPE");
            ddlVendorCategory.DataSource = dt;
            ddlVendorCategory.DataTextField = "MST_CODE";
            ddlVendorCategory.DataValueField = "MST_CODE";
            ddlVendorCategory.DataBind();
            ddlVendorCategory.Items.Insert(0, new ListItem("---------Select---------------------------", ""));
        }
        catch
        {
            throw;
        }
    }

    private DataTable GET_MOM_DATA(string Text, string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select MST_CODE,MST_DESC from tx_Master_TRN where Del_Status='0' and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and MST_CODE like :SearchQuery";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, MST_NAME, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private void bindPartyGroup()
    {
        try
        {
            ddlPartyGroupCode.Items.Clear();
            DataTable dt = GET_MOM_DATA("", "PRTY_GRP_C");
            ddlPartyGroupCode.DataTextField = "MST_DESC";
            ddlPartyGroupCode.DataValueField = "MST_CODE";
            ddlPartyGroupCode.DataSource = dt;
            ddlPartyGroupCode.DataBind();
            ddlPartyGroupCode.Items.Insert(0, new ListItem("---------Select---------------------------", ""));
        }
        catch
        {
            throw;
        }
    }

    private void BindState()
    {
        try
        {
            DataTable dt = GET_MOM_DATA("", "STATE_CODE");
            ddlState.DataTextField = "MST_DESC";
            ddlState.DataValueField = "MST_CODE";
            ddlState.DataSource = dt;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("---------Select---------------------------", ""));
        }
        catch
        {
            throw;
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            BlanksControls();
            txtVendCode.Text = getVendorCode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear..\r\nSee error log for detail."));
        }
    }

    private void BlanksControls()
    {
        try
        {
            txtVendCode.Text = "";
            txtVendorName.Text = "";
            txtVendAdd1.Text = "";
            txtVendAdd2.Text = "";
            txtVendAdd3.Text = "";
            ddlVendorCategory.SelectedIndex = -1;
            txtVendCity.Text = "";
            txtVendCode.Text = "";
            txtVendCountry.Text = "";
            txtVendEmail.Text = "";
            txtVendFax.Text = "";
            ddlPartyGroupCode.SelectedIndex = -1;
            txtVendINSPolicy.Text = "";
            txtVendLST_No.Text = "";
            txtVendMobile.Text = "";
            txtVendPan.Text = "";
            txtVendStaxType.Text = "";
            txtVendPhone.Text = "";
            txtVendPostalCode.Text = "";
            txtVendProduct.Text = "";
            txtVendRemarks.Text = "";
            txtVendState.Text = "";
            txtVendTINDate.Text = "";
            txtVendTINNo.Text = "";
            txtVendTinType.Text = "";
            txtVendWebsite.Text = "";
            chk_Status.Checked = false;
            ddlFindVendor.SelectedIndex = -1;
            txtVendLST_DT.Text = "";
            txtVendCST_DT.Text = "";
            txtVendCP1_Name.Text = "";
            txtVendCP1_Desig.Text = "";
            txtVendCP1_LL.Text = "";
            txtVendCP1_Mob.Text = "";
            txtVendCP1_Mail.Text = "";
            txtVendCP2_Name.Text = "";
            txtVendCP2_Desig.Text = "";
            txtVendCP2_LL.Text = "";
            txtVendCP2_Mob.Text = "";
            txtVendCP2_Mail.Text = "";

            txtVendCode.ReadOnly = false;
            ddlFindVendor.Visible = false;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            lblMode.Text = "Save";

            txtVendCST_No.Text = string.Empty;
            txtAccountNo.Text = "";
            txtBankName.Text = "";
            txtBranchName.Text = "";
            txtNEFTRTGCCode.Text = "";
            txtServiceTaxNo.Text = "";
            txtEccNo.Text = string.Empty;
            txtCrLimit.Text = "";
            tdSave.Visible = true;
            txtLoginId.Text = string.Empty;
            txtPassword.Text = string.Empty;
            imgCST.ImageUrl = string.Empty;
            imgLST.ImageUrl = string.Empty;
            imgPan.ImageUrl = string.Empty;
            ddlRegion.SelectedIndex = 0;
            if (ViewState["dtVendorDetail"] != null)
                dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];

            if (dtVendorDetail == null || dtVendorDetail.Rows.Count == 0)
                CreateVendorDataTable();
            dtVendorDetail.Rows.Clear();
            BindVendorGrid();

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
            txtVendCode.ReadOnly = true;
            ddlFindVendor.Visible = true;
            txtVendCode.Visible = false;
            tdDelete.Visible = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Find";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtVendCode.Text != "")
            {
                if (ddlVendorCategory.SelectedIndex >= 0 || ddlVendorCategory.SelectedValue.ToString() != "")
                {
                    UpdateData();
                }
                else
                {
                    CommonFuction.ShowMessage("Pls select vendor category.");
                }
            }
            else
            {
                CommonFuction.ShowMessage("First search Record to update.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtVendCode.Text != "")
            {
                deleteVendor();
            }
            else
            {
                CommonFuction.ShowMessage("First search Record to delete.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/VendorMasterRpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
        }
    }

    private void UpdateData()
    {
        if (Page.IsValid)
        {
            try
            {
                SaitexDM.Common.DataModel.TX_VENDOR_MST oTX_VENDOR_MST = new SaitexDM.Common.DataModel.TX_VENDOR_MST();
                bool bDup = false;
                oTX_VENDOR_MST.PRTY_CODE = txtVendCode.Text.ToUpper().Trim();
                oTX_VENDOR_MST.PRTY_GRP_CODE = ddlPartyGroupCode.SelectedValue.Trim();
                oTX_VENDOR_MST.PRTY_NAME = txtVendorName.Text.ToUpper().Trim();
                oTX_VENDOR_MST.PRTY_ADD1 = txtVendAdd1.Text.Trim();
                oTX_VENDOR_MST.PRTY_ADD2 = txtVendAdd2.Text.Trim();
                oTX_VENDOR_MST.PRTY_ADD3 = txtVendAdd3.Text.Trim();
                oTX_VENDOR_MST.PRTY_CITY = txtVendCity.Text.Trim();
                oTX_VENDOR_MST.PRTY_STATE = txtVendState.Text.Trim();
                int pincode = 0;
                int.TryParse(txtVendPostalCode.Text.Trim(), out pincode);
                oTX_VENDOR_MST.PIN_CODE = pincode;
                oTX_VENDOR_MST.COUNTRY = txtVendCountry.Text.Trim();
                oTX_VENDOR_MST.PHONE = txtVendPhone.Text.Trim();
                oTX_VENDOR_MST.FAX = txtVendFax.Text.Trim();
                oTX_VENDOR_MST.PERSON1_NAME = txtVendCP1_Name.Text.Trim();
                oTX_VENDOR_MST.REMARKS = txtVendRemarks.Text.Trim();
                oTX_VENDOR_MST.EMAIL = txtVendEmail.Text.Trim();
                oTX_VENDOR_MST.PRTY_STATUS = chk_Status.Checked;
                oTX_VENDOR_MST.PRTY_PANNO = txtVendPan.Text.ToUpper().Trim();
                oTX_VENDOR_MST.PRTY_LSTNO = txtVendLST_No.Text.ToUpper().Trim();
                oTX_VENDOR_MST.PRTY_CSTNO = txtVendCST_No.Text.ToUpper().Trim();
                oTX_VENDOR_MST.PRTY_TINNO = txtVendTINNo.Text.ToUpper().Trim();

                DateTime dd = System.DateTime.Now.Date;
                bool Is_Tin = false;
                if (DateTime.TryParse(txtVendTINDate.Text.Trim(), out dd))
                    Is_Tin = true;
                oTX_VENDOR_MST.PRTY_TINDT = dd;

                oTX_VENDOR_MST.TIN_TYPE = txtVendTinType.Text.Trim();
                oTX_VENDOR_MST.MOB_NO = txtVendMobile.Text.Trim();
                oTX_VENDOR_MST.WEBSITE = txtVendWebsite.Text.Trim();
                oTX_VENDOR_MST.PRODUCT = txtVendProduct.Text.Trim();
                oTX_VENDOR_MST.STAX_TYPE = txtVendStaxType.Text.Trim();
                oTX_VENDOR_MST.INS_POLICY_NO = txtVendINSPolicy.Text.Trim();
                oTX_VENDOR_MST.VENDOR_CAT_CODE = ddlVendorCategory.SelectedValue.Trim();
                oTX_VENDOR_MST.STATUS = true;
                oTX_VENDOR_MST.TUSER = oUserLoginDetail.UserCode;
                dd = System.DateTime.Now.Date;
                bool is_CST = false;
                if (DateTime.TryParse(txtVendCST_DT.Text.Trim(), out dd))
                    is_CST = true;
                oTX_VENDOR_MST.PRTY_CSTDT = dd;

                dd = System.DateTime.Now.Date;
                bool is_LST = false;
                if (DateTime.TryParse(txtVendLST_DT.Text.Trim(), out dd))
                    is_LST = true;
                oTX_VENDOR_MST.PRTY_LSTDT = dd;

                oTX_VENDOR_MST.PERSON2_NAME = txtVendCP2_Name.Text.Trim();
                oTX_VENDOR_MST.PERSON1_DESIG = txtVendCP1_Desig.Text.Trim();

                oTX_VENDOR_MST.PERSON1_LL = txtVendCP1_LL.Text.Trim();

                oTX_VENDOR_MST.PERSON1_MOB = txtVendCP1_Mob.Text.Trim();
                oTX_VENDOR_MST.PERSON1_EMAIL = txtVendCP1_Mail.Text.Trim();
                oTX_VENDOR_MST.PERSON2_DESIG = txtVendCP2_Desig.Text.Trim();

                oTX_VENDOR_MST.PERSON2_LL = txtVendCP2_LL.Text.Trim();

                oTX_VENDOR_MST.PERSON2_MOB = txtVendCP2_Mob.Text.Trim();
                oTX_VENDOR_MST.PERSON2_EMAIL = txtVendCP2_Mail.Text.Trim();
                oTX_VENDOR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_VENDOR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                //Added by sandeep on 25- Feb-2011 
                oTX_VENDOR_MST.SERVICE_TAX_NO = txtServiceTaxNo.Text.Trim();
                oTX_VENDOR_MST.BANK_NAME = txtBankName.Text.Trim();
                oTX_VENDOR_MST.BRANCH = txtBranchName.Text.Trim();
                oTX_VENDOR_MST.ACCOUNT_NO = txtAccountNo.Text.Trim();
                oTX_VENDOR_MST.NEFT_RTGS_CODE = txtNEFTRTGCCode.Text.Trim();
                oTX_VENDOR_MST.ECC_NO = txtEccNo.Text.Trim();
                double crlimit = 0;
                double.TryParse(txtCrLimit.Text, out crlimit);
                oTX_VENDOR_MST.CR_LIMIT = crlimit;
                oTX_VENDOR_MST.GROUP_CODE = ddlPartyGroupCode.SelectedValue;

                if (uploadPan.HasFile)
                {
                    uploadPan.PostedFile.SaveAs(Server.MapPath(@"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadPan.FileName.Trim()));
                    oTX_VENDOR_MST.PAN_IMG_PATH = @"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadPan.FileName.Trim();
                }
                else
                {
                    oTX_VENDOR_MST.PAN_IMG_PATH = imgPan.ImageUrl;
                }
                if (uploadLST.HasFile)
                {
                    uploadLST.PostedFile.SaveAs(Server.MapPath(@"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadLST.FileName.Trim()));
                    oTX_VENDOR_MST.LST_IMG_PATH = @"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadLST.FileName.Trim();
                }
                else
                {
                    oTX_VENDOR_MST.LST_IMG_PATH = imgLST.ImageUrl;
                }
                if (uploadLST.HasFile)
                {
                    uploadCST.PostedFile.SaveAs(Server.MapPath(@"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadCST.FileName.Trim()));
                    oTX_VENDOR_MST.CST_IMG_PATH = @"~\CommonImages\VendorDetails_Images\" + oTX_VENDOR_MST.PRTY_CODE + uploadCST.FileName.Trim();
                }
                else
                {
                    oTX_VENDOR_MST.CST_IMG_PATH = imgCST.ImageUrl;
                }
                oTX_VENDOR_MST.REGION = ddlRegion.SelectedValue;
                oTX_VENDOR_MST.LOGIN_ID = txtLoginId.Text.Trim();
                oTX_VENDOR_MST.PASSWORD = txtPassword.Text.Trim();
                if (ViewState["dtVendorDetail"] != null)
                    dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];


                bool bResult = SaitexBL.Interface.Method.TX_VENDOR_MST.UpdateGstVendor(oTX_VENDOR_MST, dtVendorDetail, out bDup, is_CST, is_LST, Is_Tin);
                if (bResult && UpdateUserMaster())
                {
                    BlanksControls();
                    txtVendCode.Text = getVendorCode();
                    CommonFuction.ShowMessage("Vendor Data Updated Successfully.");
                }
                else if (bDup)
                {
                    CommonFuction.ShowMessage("Vendor already Exists.");
                }
                else
                {
                    CommonFuction.ShowMessage("Vendor Data updation failed");
                }
            }
            catch
            {
                throw;
            }
        }
    }

    private void deleteVendor()
    {
        //try
        //{
        //    con = new OracleConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        //    con.Open();
        //    string strSQL = "";
        //    strSQL = "delete from TX_VENDOR_MST where ltrim(rtrim(PRTY_CODE))=:PRTY_CODE and CH_DELETESTATUS=0";
        //    cmd = new OracleCommand(strSQL, con);
        //    cmd.CommandType = CommandType.Text;

        //    param = new OracleParameter(":PRTY_CODE", OracleType.VarChar, 10);
        //    param.Direction = ParameterDirection.Input;
        //    param.Value = CommonFuction.funFixQuotes(txtVendCode.Text.Trim());
        //    cmd.Parameters.Add(param);
        //    int iRecordEffected = cmd.ExecuteNonQuery();
        //    cmd.Dispose();
        //    UpdateMode = false;

        //}
        //catch (OracleException ex)
        //{ lblMessage.Text = ""; lblErrorMessage.Text = ex.Message; ErrHandler.WriteError(ex.Message); }
        //catch (Exception ex)
        //{ lblMessage.Text = ""; lblErrorMessage.Text = ex.Message; ErrHandler.WriteError(ex.Message); }
        //finally
        //{
        //    if (con != null) { con.Close(); con.Dispose(); con = null; }
        //    if (cmd != null) { cmd.Dispose(); cmd = null; }
        //    if (param != null) { param = null; }
        //}
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BlanksControls();
            txtVendCode.Text = getVendorCode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (txtVendCode.Text != "")
            {
                SaveVendorData();
            }
            else
            {
                CommonFuction.ShowMessage("First Insert Record to save.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving..\r\nSee error log for detail."));
        }
    }

    private void GetFindDataByCode(string Vendor_Code)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectByCode(Vendor_Code);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtVendCode.Text = dt.Rows[0]["PRTY_CODE"].ToString();
                ddlPartyGroupCode.SelectedValue = dt.Rows[0]["PRTY_GRP_CODE"].ToString();
                txtVendorName.Text = dt.Rows[0]["PRTY_NAME"].ToString();
                txtVendAdd1.Text = dt.Rows[0]["PRTY_ADD1"].ToString();
                txtVendAdd2.Text = dt.Rows[0]["PRTY_ADD2"].ToString();
                txtVendAdd3.Text = dt.Rows[0]["PRTY_ADD3"].ToString();
                txtVendCity.Text = dt.Rows[0]["PRTY_CITY"].ToString();
                txtVendState.Text = dt.Rows[0]["PRTY_STATE"].ToString();
                txtVendPostalCode.Text = dt.Rows[0]["PIN_CODE"].ToString();
                txtVendCountry.Text = dt.Rows[0]["COUNTRY"].ToString();
                txtVendPhone.Text = dt.Rows[0]["PHONE"].ToString();
                txtVendFax.Text = dt.Rows[0]["FAX"].ToString();
                txtVendRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                txtVendEmail.Text = dt.Rows[0]["EMAIL"].ToString();
                txtVendPan.Text = dt.Rows[0]["PRTY_PANNO"].ToString();
                txtVendLST_No.Text = dt.Rows[0]["PRTY_LSTNO"].ToString();
                txtVendCST_No.Text = dt.Rows[0]["PRTY_CSTNO"].ToString();
                txtVendTINNo.Text = dt.Rows[0]["PRTY_TINNO"].ToString();
                DateTime dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_TINdt"].ToString(), out dd))
                    txtVendTINDate.Text = dd.ToShortDateString();

                txtVendTinType.Text = dt.Rows[0]["TIN_TYPE"].ToString();
                txtVendMobile.Text = dt.Rows[0]["MOB_NO"].ToString();
                txtVendWebsite.Text = dt.Rows[0]["WEBSITE"].ToString();
                txtVendProduct.Text = dt.Rows[0]["PRODUCT"].ToString();
                txtVendStaxType.Text = dt.Rows[0]["STAX_TYPE"].ToString();
                txtVendINSPolicy.Text = dt.Rows[0]["INS_POLICY_NO"].ToString();
                ddlVendorCategory.SelectedValue = dt.Rows[0]["VENDOR_CAT_CODE"].ToString();
                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_CSTDT"].ToString(), out dd))
                    txtVendCST_DT.Text = dd.ToShortDateString();
                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_LSTDT"].ToString(), out dd))
                    txtVendLST_DT.Text = dd.ToShortDateString();
                txtVendCP1_Name.Text = dt.Rows[0]["PERSON1_NAME"].ToString();
                txtVendCP1_Desig.Text = dt.Rows[0]["PERSON1_DESIG"].ToString();
                txtVendCP1_LL.Text = dt.Rows[0]["PERSON1_LL"].ToString();
                txtVendCP1_Mob.Text = dt.Rows[0]["PERSON1_MOB"].ToString();
                txtVendCP1_Mail.Text = dt.Rows[0]["PERSON1_EMAIL"].ToString();
                txtVendCP2_Name.Text = dt.Rows[0]["PERSON2_NAME"].ToString();
                txtVendCP2_Desig.Text = dt.Rows[0]["PERSON2_DESIG"].ToString();
                txtVendCP2_LL.Text = dt.Rows[0]["PERSON2_LL"].ToString();
                txtVendCP2_Mob.Text = dt.Rows[0]["PERSON2_MOB"].ToString();
                txtVendCP2_Mail.Text = dt.Rows[0]["PERSON2_EMAIL"].ToString();

                txtServiceTaxNo.Text = dt.Rows[0]["SERVICE_TAX_NO"].ToString();
                txtBranchName.Text = dt.Rows[0]["BRANCH"].ToString();
                txtBankName.Text = dt.Rows[0]["BANK_NAME"].ToString();
                txtAccountNo.Text = dt.Rows[0]["ACCOUNT_NO"].ToString();
                txtNEFTRTGCCode.Text = dt.Rows[0]["NEFT_RTGS_CODE"].ToString();
                txtEccNo.Text = dt.Rows[0]["ECC_NO"].ToString();
                txtCrLimit.Text = dt.Rows[0]["CR_LIMIT"].ToString();
                if (dt.Rows[0]["PRTY_STATUS"].ToString() == "1")
                    chk_Status.Checked = true;
                else
                    chk_Status.Checked = false;


                imgPan.ImageUrl = dt.Rows[0]["PAN_IMAGE_PATH"].ToString();
                imgCST.ImageUrl = dt.Rows[0]["CST_IMAGE_PATH"].ToString();
                imgLST.ImageUrl = dt.Rows[0]["LST_IMAGE_PATH"].ToString();
                ddlRegion.SelectedIndex = ddlRegion.Items.IndexOf(ddlRegion.Items.FindByValue(dt.Rows[0]["REGION"].ToString()));

                DataTable dtLogin = SaitexBL.Interface.Method.CM_USER_MST.SelectUserByUserCode(txtVendCode.Text);

                if (dtLogin != null && dtLogin.Rows.Count > 0)
                {
                    txtLoginId.Text = dtLogin.Rows[0]["USER_LOG_ID"].ToString().Trim();
                    txtPassword.Attributes.Add("value", dtLogin.Rows[0]["USER_PASS"].ToString().Trim());

                }

                DataTable dtTrn = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorTrn(Vendor_Code);
                if (dtTrn != null && dtTrn.Rows.Count > 0)
                {
                    MapVendorData(dtTrn);

                }
                
            }
            else
            {
                CommonFuction.ShowMessage("Invalid Vendor provided");
            }

        }
        catch
        {
            throw;
        }
    }

    private void MapVendorData(DataTable dtTrn)
    {
        try
        {
            if (ViewState["dtVendorDetail"] != null)
                dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];
            if (dtVendorDetail == null || dtVendorDetail.Rows.Count == 0)
                CreateVendorDataTable();
            dtVendorDetail.Rows.Clear();
            if (dtTrn != null && dtTrn.Rows.Count > 0)
            {
                foreach (DataRow drTrn in dtTrn.Rows)
                {
                    DataRow dr = dtVendorDetail.NewRow();
                    dr["UniqueId"] = dtVendorDetail.Rows.Count + 1;
                    dr["STATE"] = drTrn["STATE"];
                    dr["STATE_CODE"] = drTrn["STATE_CODE"];
                    dr["GST_NO"] = drTrn["GST_NO"];
                    dr["ADDRESS"] = drTrn["ADDRESS"];
                    dr["ADDRESS1"] = drTrn["ADDRESS1"];
                    dr["CITY"] = drTrn["CITY"];
                    dr["PIN"] = drTrn["PIN"];
                    dr["PHONE"] = drTrn["PHONE"];
                    dr["EMAIL"] = drTrn["EMAIL"];
                    dr["PHONE"] = drTrn["PHONE"];
                    dr["NAME"] = drTrn["NAME"];
                    dr["REMARKS"] = drTrn["REMARKS"];
                    dtVendorDetail.Rows.Add(dr);
                }

            }
            dtTrn = null;
            ViewState["dtVendorDetail"] = dtVendorDetail;
        }

        catch (Exception ex)
        {
            throw ex;

        }
    }
    
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "window.confirm('Are you sure to save the record ?')");
        imgbtnUpdate.Attributes.Add("OnClick", "window.confirm('Are you sure to update the record ?')");
        imgbtnDelete.Attributes.Add("OnClick", "window.confirm('Are you sure to delete the record ?')");
        imgbtnPrint.Attributes.Add("OnClick", "window.confirm('Are you sure to print the record ?')");
        imgbtnClear.Attributes.Add("OnClick", "window.confirm('Are you sure to clear the record ?')");
        imgbtnExit.Attributes.Add("OnClick", "window.confirm('Are you sure to exit the record ?')");
    }

    protected void ddlFindVendor_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ViewState["dtVendorDetail"] != null)
                dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];
            string VendorCode = ddlFindVendor.SelectedValue.Trim();
            if (dtVendorDetail == null || dtVendorDetail.Rows.Count == 0)
                CreateVendorDataTable();
            dtVendorDetail.Rows.Clear();
            
            BlanksControls();
            txtVendCode.ReadOnly = true;

            tdUpdate.Visible = true;
            lblMode.Text = "Update";
            tdSave.Visible = false;
            bindVendorCategory();
            bindPartyGroup();
            GetFindDataByCode(VendorCode);
            BindVendorGrid();
            ddlFindVendor.Visible = false;
            txtVendCode.Visible = true;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding Vendors..\r\nSee error log for detail."));
        }
    }

    protected void ddlFindVendor_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_GRP_CODE,VENDOR_CAT_CODE from ( select * from tx_Vendor_MST where Del_Status=0) asd";
            string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery ";
            string SortExpression = " order by PRTY_CODE asc";
            string SearchQuery = e.Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            ddlFindVendor.Items.Clear();

            ddlFindVendor.DataSource = data;
            ddlFindVendor.DataTextField = "PRTY_NAME";
            ddlFindVendor.DataValueField = "PRTY_CODE";
            ddlFindVendor.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Vendors..\r\nSee error log for detail."));
        }
    }
    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Queries/VendorMasterQuery.aspx", false);
    }
    protected void ddlPartyGroupCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtVendCode.Text = getVendorCode();
    }



    protected string getVendorCode()
    {
        string PREFIX = string.Empty;
        if (ddlPartyGroupCode.SelectedItem.ToString().Equals("TRADE PAYABLE FOR GOODS") || ddlPartyGroupCode.SelectedItem.ToString().Equals("TRADE PAYABLE FOR EXP & OTHERS"))
        {
            PREFIX = "5";
        }
        else if (ddlPartyGroupCode.SelectedItem.ToString().Equals("TRADE RECEIVABLES"))
        {
            PREFIX = "7";
        }
        else
        {
            PREFIX = "0";
        }
        return SaitexBL.Interface.Method.TX_VENDOR_MST.GetVendorCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlPartyGroupCode.SelectedItem.ToString(), PREFIX.ToUpper());

    }

    protected void ddlVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVendorCategory.SelectedValue.Equals("Broker"))
        {
            rvVendorPostalCode.Enabled = false;
            rfVendorPostalCode.Enabled = false;
            revEmail2.Enabled = false;
            rfBranch.Enabled = false;
            rfRtgs.Enabled = false;
            reEmail1.Enabled = false;
            reEmail3.Enabled = false;
            rvPassword.Enabled = false;
            RVLogin.Enabled = false;
            rfAccount.Enabled = false;
            rfBank.Enabled = false;
            rfService.Enabled = false;
            RFVuploadCST.Enabled = false;
            RFVuploadLST.Enabled = false;
            rfvuploadPan.Enabled = false;
            lblCreditLimit.Text = "Commission(%)";
        }
        else if (ddlVendorCategory.SelectedValue.Equals("Spinner"))
        {
            rvVendorPostalCode.Enabled = false;
            rfVendorPostalCode.Enabled = false;
            revEmail2.Enabled = false;
            rfBranch.Enabled = false;
            rfRtgs.Enabled = false;
            reEmail1.Enabled = false;
            reEmail3.Enabled = false;
            rvPassword.Enabled = false;
            RVLogin.Enabled = false;
            rfAccount.Enabled = false;
            rfBank.Enabled = false;
            rfService.Enabled = false;
            RFVuploadCST.Enabled = false;
            RFVuploadLST.Enabled = false;
            rfvuploadPan.Enabled = false;
            lblCreditLimit.Text = "Credit Limit";
        }
        else if (ddlVendorCategory.SelectedValue.Equals("Transporter"))
        {
            rvVendorPostalCode.Enabled = false;
            rfVendorPostalCode.Enabled = false;
            revEmail2.Enabled = false;
            rfBranch.Enabled = false;
            rfRtgs.Enabled = false;
            reEmail1.Enabled = false;
            reEmail3.Enabled = false;
            rvPassword.Enabled = false;
            RVLogin.Enabled = false;
            rfAccount.Enabled = false;
            rfBank.Enabled = false;
            rfService.Enabled = false;
            RFVuploadCST.Enabled = false;
            RFVuploadLST.Enabled = false;
            rfvuploadPan.Enabled = false;
            lblCreditLimit.Text = "Credit Limit";
        }

        else if (ddlVendorCategory.SelectedValue.Equals("EXPORT CUSTOMER"))
        {
            txtVendPostalCode.Text = "999999";
            txtPinCode.Text = "999999";
            txtGstNo.Text = "XXXXXXXXXXXXXXX";

        }
        else
        {
            rvVendorPostalCode.Enabled = false;
            rfVendorPostalCode.Enabled = false;
            revEmail2.Enabled = false;
            rfBranch.Enabled = false;
            rfRtgs.Enabled = false;
            reEmail1.Enabled = false;
            reEmail3.Enabled = false;
            rvPassword.Enabled = false;
            RVLogin.Enabled = false;
            rfAccount.Enabled = false;
            rfBank.Enabled = false;
            rfService.Enabled = false;
            RFVuploadCST.Enabled = false;
            RFVuploadLST.Enabled = false;
            rfvuploadPan.Enabled = false;
            lblCreditLimit.Text = "Credit Limit";

        }




    }


    public void disableValidator()
    {
        rvVendorPostalCode.Enabled = false;
        rfVendorPostalCode.Enabled = false;
        revEmail2.Enabled = false;
        rfBranch.Enabled = false;
        rfRtgs.Enabled = false;
        reEmail1.Enabled = false;
        reEmail3.Enabled = false;
        rvPassword.Enabled = false;
        RVLogin.Enabled = false;
        rfAccount.Enabled = false;
        rfBank.Enabled = false;
        rfService.Enabled = false;
        RFVuploadCST.Enabled = false;
        RFVuploadLST.Enabled = false;
        rfvuploadPan.Enabled = false;
    }
    private void CreateVendorDataTable()
    {
        dtVendorDetail = new DataTable();
        dtVendorDetail.Columns.Add("UniqueId", typeof(int));
        dtVendorDetail.Columns.Add("GST_NO", typeof(string));
        dtVendorDetail.Columns.Add("STATE", typeof(string));
        dtVendorDetail.Columns.Add("STATE_CODE", typeof(string));
        dtVendorDetail.Columns.Add("ADDRESS", typeof(string));
        dtVendorDetail.Columns.Add("ADDRESS1", typeof(string));
        dtVendorDetail.Columns.Add("CITY", typeof(string));
        dtVendorDetail.Columns.Add("PIN", typeof(int));
        dtVendorDetail.Columns.Add("PHONE", typeof(string));
        dtVendorDetail.Columns.Add("EMAIL", typeof(string));
        dtVendorDetail.Columns.Add("NAME", typeof(string));
        dtVendorDetail.Columns.Add("REMARKS", typeof(string));

    }

    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtVendorDetail"] != null)
                dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];
            if (dtVendorDetail == null || dtVendorDetail.Rows.Count == 0)
                CreateVendorDataTable();
            if (!string.IsNullOrEmpty(txtGstNo.Text) && !string.IsNullOrEmpty(ddlState.SelectedValue))
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                bool bb = SearchItemCodeInGrid(UniqueId);
                if (!bb)
                {
                    if (UniqueId > 0)
                    {
                        DataView dv = new DataView(dtVendorDetail);
                        dv.RowFilter = "UniqueId=" + UniqueId;
                        if (dv.Count > 0)
                        {
                            dv[0]["STATE"] = ddlState.SelectedItem.ToString();
                            dv[0]["STATE_CODE"] = txtStateCode.Text.Trim();
                            dv[0]["GST_NO"] = txtGstNo.Text.Trim();
                            dv[0]["ADDRESS"] = txtAddress.Text.Trim();
                            dv[0]["ADDRESS1"] = txtAddress1.Text.Trim();
                            dv[0]["CITY"] = txtCity.Text.Trim();
                            dv[0]["PIN"] = int.Parse(txtPinCode.Text.ToString());
                            dv[0]["PHONE"] = txtPhone.Text.Trim();
                            dv[0]["EMAIL"] = TxtEmail.Text.Trim();
                            dv[0]["NAME"] = TxtName.Text.Trim();
                            dv[0]["REMARKS"] = TxtRemarks.Text.Trim();
                            dtVendorDetail.AcceptChanges();

                        }

                    }
                    else
                    {
                        DataRow dr = dtVendorDetail.NewRow();
                        dr["UniqueId"] = dtVendorDetail.Rows.Count + 1;
                        dr["STATE"] = ddlState.SelectedItem.ToString();
                        dr["STATE_CODE"] = txtStateCode.Text.Trim();
                        dr["GST_NO"] = txtGstNo.Text.Trim();
                        dr["ADDRESS"] = txtAddress.Text.Trim();
                        dr["ADDRESS1"] = txtAddress1.Text.Trim();
                        dr["CITY"] = txtCity.Text.Trim();
                        dr["PIN"] = int.Parse(txtPinCode.Text.ToString());
                        dr["PHONE"] = txtPhone.Text.Trim();
                        dr["EMAIL"] = TxtEmail.Text.Trim();
                        dr["NAME"] = TxtName.Text.Trim();
                        dr["REMARKS"] = TxtRemarks.Text.Trim();
                        dtVendorDetail.Rows.Add(dr);

                    }
                    RefreshDetailRowItem();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Party Code/Bill No Should Be Diffrent');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please select State and Enter GST no.');", true);
            }

            ViewState["dtVendorDetail"] = dtVendorDetail;
            BindVendorGrid();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindVendorGrid()
    {
        if (ViewState["dtVendorDetail"] != null)
        {
            dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];
            DataView dv = new DataView(dtVendorDetail);
            grdVendorDetail.DataSource = dv;
            grdVendorDetail.DataBind();
        }
        else
        {
            grdVendorDetail.DataSource = null;
            grdVendorDetail.DataBind();
        }
    }

    private void RefreshDetailRowItem()
    {
        ddlState.SelectedIndex = -1;
        txtGstNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtPhone.Text = string.Empty;
        TxtEmail.Text = string.Empty;
        TxtName.Text = string.Empty;
        TxtRemarks.Text = string.Empty;
        txtStateCode.Text = string.Empty;
        txtAddress1.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtPinCode.Text = string.Empty;

    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        RefreshDetailRowItem();

    }
    private bool SearchItemCodeInGrid(int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdVendorDetail.Rows)
            {
                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                Label lblGst= (Label)grdRow.FindControl("txtGstNo");
                Label lblstate = (Label)grdRow.FindControl("txtState");                
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (UniqueId != iUniqueId && txtGstNo.Text == lblGst.Text.Trim() && ddlState.SelectedValue == lblstate.Text.Trim())
                {
                    Result = true;
                }
            }



            return Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void FillDetailVendor(int UniqueId)
    {
        try
        {
            if(ViewState["dtVendorDetail"] != null)
                dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];
            DataView dv = new DataView(dtVendorDetail);
            if (dv.Count > 0)
            {
                txtGstNo.Text = dv[0]["GST_NO"].ToString();
                txtAddress.Text = dv[0]["ADDRESS"].ToString();
                txtPhone.Text =  dv[0]["PHONE"].ToString();
                TxtName.Text = dv[0]["NAME"].ToString();
                TxtEmail.Text = dv[0]["EMAIL"].ToString();
                txtStateCode.Text = dv[0]["STATE_CODE"].ToString();
                TxtRemarks.Text = dv[0]["REMARKS"].ToString();
                ViewState["UniqueId"] = UniqueId;
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dv[0]["STATE"].ToString()));


            }

        }
        catch
        {
            throw;

        }
    }

    private void DeleteVendorDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtVendorDetail"] != null)
                dtVendorDetail = (DataTable)ViewState["dtVendorDetail"];
            if (grdVendorDetail.Rows.Count == 1)
            {
                dtVendorDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtVendorDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtVendorDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtVendorDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
            ViewState["dtVendorDetail"] = dtVendorDetail;

        }

        catch(Exception ex)
        {
            throw ex;

        }

    }

    protected void grdVendorDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "ROWEdit")
            {

                FillDetailVendor(UniqueId);
            }
            else if (e.CommandName == "ROWDelete")
            {
                DeleteVendorDetailRow(UniqueId);
                BindVendorGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStateCode.Text = ddlState.SelectedValue.ToString();
        if (ddlState.SelectedValue.ToString() == "NA")
        { 
        if (txtVendFax.Text == string.Empty && txtVendPan.Text.Trim() == string.Empty && ddlVendorCategory.SelectedValue.ToString() != "EXPORT CUSTOMER") 
        {
            Common.CommonFuction.ShowMessage("Please fill the Adhar and Pan Fields because GST is NA");
            txtGstNo.Text = string.Empty;
        } 
        
        }
        else 
        {
            
            //txtGstNo.Text = "NA";
        
        }
    }
}
