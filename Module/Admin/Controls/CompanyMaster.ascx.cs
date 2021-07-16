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
using System.Data.OracleClient;
using System.Text.RegularExpressions;
public partial class Module_Admin_Controls_CompanyMaster : System.Web.UI.UserControl
{
    string FilePath = string.Empty;
    string file = string.Empty;
    string strTUser = "";
    string logoimagepath = string.Empty;
    SaitexDM.Common.DataModel.CM_COMPANY_MST oCMCompanyDetail = new SaitexDM.Common.DataModel.CM_COMPANY_MST();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gvCompMaster.AutoPostBackOnSelect = false;

            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                strTUser = oUserLoginDetail.UserCode;
                if (!IsPostBack)
                {
                    bindGrid();
                    gvCompMaster.AutoPostBackOnSelect = false;
                    bindGroupMaster();

                    tdSave.Visible = true;
                    tdUpdate.Visible = false;

                    lblMode.Text = "Save";

                }
            }
            else
            {
                Response.Redirect("/Saitex/Default.aspx", false);

            }
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading.\\r\\nSee error log for detail."));
            lblErrorMessage.Text = ex.Message;
  
        }
    }
    
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Insert Record.\\r\\nSee error log for detail."));
            lblErrorMessage.Text = ex.Message;
  
        }

    }
    
    private void Insertdata()
    {
        try
        {
            SaitexDM.Common.DataModel.CM_COMPANY_MST oCMCompanyDetail = new SaitexDM.Common.DataModel.CM_COMPANY_MST();
            int iRecordFound = 0;
            bool bCSTDATE;
            bool bTINDATE;
            bool bESIDATE;
            bool bTAXDATE;
            oCMCompanyDetail.COMP_CODE = CommonFuction.funFixQuotes(txtCompanyCode.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_NAME = CommonFuction.funFixQuotes(txtCompanyName.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_ADD = CommonFuction.funFixQuotes(txtCompAddress.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_STATE = CommonFuction.funFixQuotes(txtCompState.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_COUNTRY = CommonFuction.funFixQuotes(txtCompCountry.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_PHONE_NO = CommonFuction.funFixQuotes(txtComPhoneNo.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_FAX_NO = CommonFuction.funFixQuotes(txtCompFaxNo.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_EMAIL_ID = CommonFuction.funFixQuotes(txtCompEmailId.Text.Trim());
            oCMCompanyDetail.COMP_PAN_NO = CommonFuction.funFixQuotes(txtCompPANNumber.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_CST_NO = CommonFuction.funFixQuotes(txtCompCSTNumber.Text.ToUpper().Trim());

            oCMCompanyDetail.GRP_Code = ddlGroupName.SelectedValue.ToString();
            DateTime dtCompCSTDate = System.DateTime.Now;
            bCSTDATE = DateTime.TryParse(txtCompCSTDate.Text, out dtCompCSTDate);
            oCMCompanyDetail.COMP_CST_DATE = dtCompCSTDate;
            oCMCompanyDetail.COMP_TIN_NO = CommonFuction.funFixQuotes(txtCompanyTINNumber.Text.ToUpper().Trim());
            DateTime dtCompanyTINDate = System.DateTime.Now;
            bTINDATE = DateTime.TryParse(txtCompanyTINDate.Text, out dtCompanyTINDate);
            oCMCompanyDetail.COMP_TIN_DATE = dtCompanyTINDate;
            oCMCompanyDetail.COMP_ESI_NO = CommonFuction.funFixQuotes(txtCompESINumber.Text.ToUpper().Trim());
            DateTime dtCompESIDate = System.DateTime.Now;
            bESIDATE = DateTime.TryParse(txtCompESIDate.Text, out dtCompESIDate);
            oCMCompanyDetail.COMP_ESI_DATE = dtCompESIDate;
            oCMCompanyDetail.COMP_SERVICE_TAX_NO = CommonFuction.funFixQuotes(txtCompServiceTaxNo.Text.ToUpper().Trim());
            DateTime dtCompServiceTaxDate = System.DateTime.Now;
            bTAXDATE = DateTime.TryParse(txtCompServiceTaxDate.Text, out dtCompServiceTaxDate);
            oCMCompanyDetail.COMP_SERVICE_TAX_DATE = dtCompServiceTaxDate;
            oCMCompanyDetail.COMP_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.ToUpper().Trim());
            oCMCompanyDetail.COMP_HISTROY = CommonFuction.funFixQuotes(txtCompanyHistory.Text.ToUpper().Trim());
            if (chk_Status.Checked)
            {

                oCMCompanyDetail.STATUS = true;
            }
            else
            {
                oCMCompanyDetail.STATUS = false;


            }
            oCMCompanyDetail.TUSER = strTUser;

            if (FileUpload1.HasFile)
            {
                string extension = System.IO.Path.GetExtension(FileUpload1.FileName).ToUpper();
                string filepath = FileUpload1.PostedFile.FileName;


                string fileNAme = string.Empty;
                fileNAme = DateTime.Now.ToString();
                fileNAme = fileNAme.Replace(":", "");
                fileNAme = fileNAme.Replace("/", "");
                fileNAme = fileNAme.Replace(" ", "");
                fileNAme = fileNAme.Replace("AM", "01");
                fileNAme = fileNAme.Replace("PM", "02");
                //save the file to the server
                FilePath = "~/CommonImages/logo/" + fileNAme + extension;
                FileUpload1.SaveAs(Server.MapPath(FilePath));

            }

            oCMCompanyDetail.UploadImagePath = FilePath;
            oCMCompanyDetail.ECC_NO = txtEccNo.Text.Trim();

            oCMCompanyDetail.TAN_NO = txtTanNo.Text.Trim();
            oCMCompanyDetail.CIN_NO = txtCIN.Text.Trim();

            bool bResult = SaitexBL.Interface.Method.CM_COMP_MST.InsertCompanyMaster(oCMCompanyDetail, out iRecordFound, bCSTDATE, bTINDATE, bESIDATE, bTAXDATE);
            if (bResult)
            {
                BlanksControls();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Company  Saved Successfully');", true);
                bindGrid();
                gvCompMaster.Enabled = false;

            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('This Company already Exists');", true);
            }

        }
        catch
        {
            throw;
        }          

    }

    
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        {
            try
            {
                if (txtCompanyCode.Text.Trim() != "" && txtCompanyName.Text.Trim() != "")
                {
                    UpdateData();
                    bindGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('First Enter CompanyCode and Company Name to update');", true);
                }
            }
            catch (Exception ex)
            {
                Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Updating Record.\\r\\nSee error log for detail."));

                lblErrorMessage.Text = ex.Message;
  
            
            }
        }
    }  
    
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvCompMaster.AutoPostBackOnSelect = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            lblMode.Text = "Update";
            ddlCompanyCode.Visible = true;
            txtCompanyCode.ReadOnly = true;
            txtCompanyCode.Visible = false;
            gvCompMaster.DataSource = null;
            gvCompMaster.DataBind();
            bindGrid();
            ddlGroupName.Items.Clear();
            txtCompanyCode.Text = "";
            txtCompanyName.Text = "";
            txtCompanyHistory.Text = "";
            txtCompAddress.Text = "";
            txtCompState.Text = "";
            txtCompCountry.Text = "";
            txtComPhoneNo.Text = "";
            txtCompFaxNo.Text = "";
            txtCompEmailId.Text = "";
            txtCompCSTNumber.Text = "";
            txtCompanyTINNumber.Text = "";
            txtCompESINumber.Text = "";
            txtCompPANNumber.Text = "";
            txtCompServiceTaxNo.Text = "";
            txtRemarks.Text = "";
            txtCompanyTINDate.Text = "";
            txtCompCSTDate.Text = "";
            txtCompESIDate.Text = "";
            txtCompServiceTaxDate.Text = "";
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Find The Records.\\r\\nSee error log for detail."));
            lblErrorMessage.Text = ex.Message;
  
        }
    }
    
    protected void imgbtnClear_Click(object sender,ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Admin/Pages/CompanyMaster.aspx", false);
        BlanksControls();
    }
    
    protected void imgbtnPrint_Click(object sender,ImageClickEventArgs e)
    {
        string URL = "../Reports/CompanyMaster_Rpt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
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
            errorLog.ErrHandler.WriteError(ex.Message);
            lblErrorMessage.Text = ex.Message;
  
        }
    }
    
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void gvCompMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    private void BlanksControls()
    {
        try
        {

            ddlGroupName.Items.Clear();
            txtCompanyCode.Text = "";
            txtCompanyName.Text = "";
            txtCompanyHistory.Text = "";
            txtCompAddress.Text = "";
            txtCompState.Text = "";
            txtCompCountry.Text = "";
            txtComPhoneNo.Text = "";
            txtCompFaxNo.Text = "";
            txtCompEmailId.Text = "";
            txtCompCSTNumber.Text = "";
            txtCompanyTINNumber.Text = "";
            txtCompESINumber.Text = "";
            txtCompPANNumber.Text = "";
            txtCompServiceTaxNo.Text = "";
            txtRemarks.Text = "";
            txtCompanyTINDate.Text = "";
            txtCompCSTDate.Text = "";
            txtCompESIDate.Text = "";
            txtCompServiceTaxDate.Text = "";
            txtEccNo.Text = string.Empty;

            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "Save";
            ddlCompanyCode.Visible = false;
            txtCompanyCode.ReadOnly = false;
            txtCompanyCode.Visible = true;
            txtCIN.Text = string.Empty;
            txtTanNo.Text = string.Empty;
 
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            
  
        }
    }
    
    private void bindGroupMaster()
    {
        try
        {
            DataTable dtgroup = SaitexBL.Interface.Method.CM_GroupMaster.Select();


            ddlGroupName.DataValueField = "grp_code";
            ddlGroupName.DataTextField = "Grp_Name";
            ddlGroupName.DataSource = dtgroup;
            ddlGroupName.DataBind();
            ddlGroupName.Items.Insert(0, new ListItem("------Select------", ""));
        }
        catch
        {
            throw;
        }       
    }
    
    private void bindGrid()
    {
        try
        {
            gvCompMaster.AutoPostBackOnSelect = true;
            DataTable dtcompanymaster = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();
            if (dtcompanymaster != null)
            {
                if (dtcompanymaster.Rows.Count > 0)
                {
                    lblTotalRecord.Text = dtcompanymaster.Rows.Count.ToString();
                    gvCompMaster.DataSource = dtcompanymaster;
                    gvCompMaster.DataBind();

                }

            }
        }
        catch
        {
            throw;
        }

    }
    
    protected void ddlCompanyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            string CommandText = "select COMP_CODE,COMP_NAME from CM_COMPANY_MST";
            string WhereClause = "  where COMP_CODE like :SearchQuery or COMP_NAME like :SearchQuery ";
            string SortExpression = " order by COMP_CODE asc";
            string SearchQuery = e.Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            ddlCompanyCode.Items.Clear();
            ddlCompanyCode.DataSource = data;
            ddlCompanyCode.DataBind();
            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Grid Loading.\\r\\nSee error log for detail."));

            lblErrorMessage.Text = ex.Message;
  
        }
    }
    
    protected void ddlCompanyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtCompanyCode.ReadOnly = true;

            FillDataForedit(CommonFuction.funFixQuotes(ddlCompanyCode.SelectedValue.Trim()));

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Filling data for updation.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.Message;
  
        }
    }
    
    private void FillDataForedit(string Comp_code)
    {
        try
        {

            SaitexDM.Common.DataModel.CM_COMPANY_MST oCM_COMPANY_MST = new SaitexDM.Common.DataModel.CM_COMPANY_MST();
            oCM_COMPANY_MST.COMP_CODE = Comp_code;
            DataTable dt = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMasterByCompCode(oCM_COMPANY_MST);
            if (dt != null && dt.Rows.Count > 0)
            {

                txtCompanyCode.Text = dt.Rows[0]["COMP_CODE"].ToString();
                txtCompanyName.Text = dt.Rows[0]["COMP_NAME"].ToString();
                txtCompanyHistory.Text = dt.Rows[0]["COMP_HISTROY"].ToString();
                txtCompAddress.Text = dt.Rows[0]["COMP_ADD"].ToString();
                txtCompState.Text = dt.Rows[0]["COMP_STATE"].ToString();
                txtCompCountry.Text = dt.Rows[0]["COMP_COUNTRY"].ToString();
                txtComPhoneNo.Text = dt.Rows[0]["COMP_PHONE_NO"].ToString();
                txtCompFaxNo.Text = dt.Rows[0]["COMP_FAX_NO"].ToString();
                txtCompEmailId.Text = dt.Rows[0]["COMP_EMAIL_ID"].ToString();
                txtCompCSTNumber.Text = dt.Rows[0]["COMP_CST_NO"].ToString();
                txtCompanyTINNumber.Text = dt.Rows[0]["COMP_TIN_NO"].ToString();
                txtCompESINumber.Text = dt.Rows[0]["COMP_ESI_NO"].ToString();
                txtCompPANNumber.Text = dt.Rows[0]["COMP_PAN_NO"].ToString();
                txtCompServiceTaxNo.Text = dt.Rows[0]["COMP_SERVICE_TAX_NO"].ToString();
                txtRemarks.Text = dt.Rows[0]["COMP_REMARKS"].ToString();
                txtCompServiceTaxDate.Text = dt.Rows[0]["COMP_SERVICE_TAX_DATE"].ToString();
                txtCompESIDate.Text = dt.Rows[0]["COMP_ESI_DATE"].ToString();
                txtCompCSTDate.Text = dt.Rows[0]["COMP_CST_DATE"].ToString();
                txtCompanyTINDate.Text = dt.Rows[0]["COMP_TIN_DATE"].ToString();
                bindGroupMaster();
                ddlGroupName.SelectedIndex = ddlGroupName.Items.IndexOf(ddlGroupName.Items.FindByValue(dt.Rows[0]["GRP_CODE"].ToString()));
                previewField.ImageUrl = dt.Rows[0]["LOGO_PATH"].ToString();
                TxtImagepath.Text = dt.Rows[0]["LOGO_PATH"].ToString().Trim();
                ViewState["COMP_CODE"] = dt.Rows[0]["COMP_CODE"].ToString();

                if (dt.Rows[0]["STATUS"].ToString() == "1")
                    chk_Status.Checked = true;
                else
                    chk_Status.Checked = false;

                ddlCompanyCode.SelectedIndex = -1;
                txtEccNo.Text = dt.Rows[0]["ECC_NO"].ToString();
                txtTanNo.Text = dt.Rows[0]["TAN_NO"].ToString();
                txtCIN.Text = dt.Rows[0]["CIN_NO"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Invalid Item Selection');", true);
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
            if (CheckForDuplicacy(txtCompanyCode.Text.Trim()))
            {    int iRecordFound = 0;
                oCMCompanyDetail.COMP_CODE = CommonFuction.funFixQuotes(txtCompanyCode.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_NAME = CommonFuction.funFixQuotes(txtCompanyName.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_ADD = CommonFuction.funFixQuotes(txtCompAddress.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_STATE = CommonFuction.funFixQuotes(txtCompState.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_COUNTRY = CommonFuction.funFixQuotes(txtCompCountry.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_PHONE_NO = CommonFuction.funFixQuotes(txtComPhoneNo.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_FAX_NO = CommonFuction.funFixQuotes(txtCompFaxNo.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_EMAIL_ID = CommonFuction.funFixQuotes(txtCompEmailId.Text);
                oCMCompanyDetail.COMP_PAN_NO = CommonFuction.funFixQuotes(txtCompPANNumber.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_CST_NO = CommonFuction.funFixQuotes(txtCompCSTNumber.Text.ToUpper().Trim());
                oCMCompanyDetail.GRP_Code = ddlGroupName.SelectedValue.ToString();
                DateTime dtCompCSTDate = System.DateTime.Now;
                DateTime.TryParse(txtCompCSTDate.Text, out dtCompCSTDate);
                oCMCompanyDetail.COMP_CST_DATE = dtCompCSTDate;
                 oCMCompanyDetail.COMP_TIN_NO = CommonFuction.funFixQuotes(txtCompanyTINNumber.Text.ToUpper().Trim());
                DateTime dtCompanyTINDate = System.DateTime.Now;
                DateTime.TryParse(txtCompanyTINDate.Text, out dtCompanyTINDate);
                oCMCompanyDetail.COMP_TIN_DATE = dtCompanyTINDate;
                oCMCompanyDetail.COMP_ESI_NO = CommonFuction.funFixQuotes(txtCompESINumber.Text.ToUpper().Trim());
                DateTime dtCompESIDate = System.DateTime.Now;
                DateTime.TryParse(txtCompESIDate.Text, out dtCompESIDate);
                oCMCompanyDetail.COMP_ESI_DATE = dtCompESIDate;
                oCMCompanyDetail.COMP_SERVICE_TAX_NO = CommonFuction.funFixQuotes(txtCompServiceTaxNo.Text.ToUpper().Trim());
                DateTime dtCompServiceTaxDate = System.DateTime.Now;
                DateTime.TryParse(txtCompServiceTaxDate.Text, out dtCompServiceTaxDate);
                oCMCompanyDetail.COMP_SERVICE_TAX_DATE = dtCompServiceTaxDate;
                oCMCompanyDetail.COMP_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.ToUpper().Trim());
                oCMCompanyDetail.COMP_HISTROY = CommonFuction.funFixQuotes(txtCompanyHistory.Text.ToUpper().Trim());
                if (chk_Status.Checked)
                {
                    oCMCompanyDetail.STATUS = true;
                }
                else
                {
                    oCMCompanyDetail.STATUS = false;
                }
                oCMCompanyDetail.TUSER = strTUser;
                if (FileUpload1.HasFile)
                {
                    // previewField.ImageUrl   
                    string filepath = previewField.ImageUrl;
                    string cString = filepath;
                    char[] splitter = { '/' };
                    string[] arrString = cString.Split(splitter);
                    string file = arrString[3].ToString();
                    //save the file to the server
                    string FilePath = "~/CommonImages/logo/" + file;
                    FileUpload1.SaveAs(Server.MapPath(FilePath));
                    //lblStatus.Text = "File Saved to: " + Server.MapPath(".\\") + file;
                }             
                if (previewField.ImageUrl == "")
                {
                    if (FileUpload1.HasFile)
                    {
                        string extension = System.IO.Path.GetExtension(FileUpload1.FileName).ToUpper();
                        string filepath = FileUpload1.PostedFile.FileName;
                        string fileNAme = string.Empty;
                        fileNAme = DateTime.Now.ToString();
                        fileNAme = fileNAme.Replace(":", "");
                        fileNAme = fileNAme.Replace("/", "");
                        fileNAme = fileNAme.Replace(" ", "");
                        fileNAme = fileNAme.Replace("AM", "01");
                        fileNAme = fileNAme.Replace("PM", "02");
                        //save the file to the server
                        FilePath = "~/CommonImages/logo/" + fileNAme + extension;
                        FileUpload1.SaveAs(Server.MapPath(FilePath));
                    }
                }
                if (FileUpload1.PostedFile.FileName != "" && FileUpload1.PostedFile.FileName != null)
                {
                    oCMCompanyDetail.UploadImagePath = FileUpload1.PostedFile.FileName;
                }
                else if (TxtImagepath.Text.Trim() != "" && TxtImagepath.Text != null)
                {
                    oCMCompanyDetail.UploadImagePath = TxtImagepath.Text.Trim();
                }
                else
                {
                    oCMCompanyDetail.UploadImagePath = string.Empty;
                }
                oCMCompanyDetail.ECC_NO = txtEccNo.Text.Trim();


                oCMCompanyDetail.TAN_NO = txtTanNo.Text.Trim();
                oCMCompanyDetail.CIN_NO = txtCIN.Text.Trim();

                bool bResult = SaitexBL.Interface.Method.CM_COMP_MST.UpdateCompanyMaster(oCMCompanyDetail, out iRecordFound);
                if (bResult)
                {
                    BlanksControls();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert(' Updated successfully');", true);
                    bindGrid();
                  
                }
                else if (iRecordFound > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('This record is already saved Please save Another Record');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('No such record exits.! Pls enter valid CompanyCode Code');", true);
            }
        }
        catch 
        { throw ; }

    }
    
    private bool CheckForDuplicacy(string CompCode)
    {
        bool Flag = false;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "COMP_CODE='" + CompCode + "'";
                if (dv.Count > 0)
                {
                    Flag = true;
                }
            }
            return Flag;
        }
        catch
        { return Flag; }
    }
    
    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            string subPath = "~/CommonImages/logo/";
            bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!IsExists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in On Prerender.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.Message;

        }
    }
    
    protected void gvCompMaster_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList ar = gvCompMaster.SelectedRecords;
            gvCompMaster.AutoPostBackOnSelect = true;
            txtCompanyCode.ReadOnly = true;
            Hashtable ht = (Hashtable)ar[0];
            FillDataForedit(ht["COMP_CODE"].ToString());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Filling data for updation.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.Message;
  
        }
    }
 }


