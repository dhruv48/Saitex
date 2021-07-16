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

public partial class Module_StartUp_CreateCompany : System.Web.UI.Page
{
    string FilePath = string.Empty;
    string file = string.Empty;   
    string logoimagepath = string.Empty;  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                bindGrid();
                gvCompMaster.AutoPostBackOnSelect = false;
                bindGroupMaster();
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                lblMode.Text = "Save";

            }
        }
        catch (Exception ex)
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
            var oCMCompanyDetail = new SaitexDM.Common.DataModel.CM_COMPANY_MST();
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
            oCMCompanyDetail.TUSER = "SUPER ADMIN";

            if (FileUpload1.HasFile)
            {
                var extension = System.IO.Path.GetExtension(FileUpload1.FileName).ToUpper();
                var filepath = FileUpload1.PostedFile.FileName;

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

            var bResult = SaitexBL.Interface.Method.CM_COMP_MST.InsertCompanyMaster(oCMCompanyDetail, out iRecordFound, bCSTDATE, bTINDATE, bESIDATE, bTAXDATE);
            if (bResult)
            {
                BlanksControls();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Company  Saved Successfully');", true);
                bindGrid();
                gvCompMaster.Enabled = false;
                Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                Response.Redirect("CreateBranch.aspx",false);

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
            var dtgroup = SaitexBL.Interface.Method.CM_GroupMaster.Select();
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
            var dtcompanymaster = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();
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

    private bool CheckForDuplicacy(string CompCode)
    {
        bool Flag = false;
        try
        {
            var dt = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                var dv = new DataView(dt);
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
   
}
