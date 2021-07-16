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
using System.IO;

public partial class Module_FileManagement_Controls_FileUpload : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
                
                bindFileType();
                bindFileGroup();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbFileCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FM_FILE_UPLOAD.GetFileDetails();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);

                dv.RowFilter = "FILE_CODE=" + cmbFileCode.SelectedValue.ToString().Trim();

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtFileCode.Text = dv[iLoop]["FILE_CODE"].ToString();
                        txtFileName.Text = dv[iLoop]["FILE_NAME"].ToString();
                        txtFileReference.Text = dv[iLoop]["FILE_REF"].ToString();
                        cmbFileGroup.SelectedValue = dv[iLoop]["FILE_GROUP"].ToString();
                        cmbFileType.SelectedValue = dv[iLoop]["FILE_TYPE"].ToString();
                        txtDescription.Text = dv[iLoop]["DESCRIPTION"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbFileCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FM_FILE_UPLOAD.GetFileDetails();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbFileCode.DataValueField = "FILE_CODE";
                cmbFileCode.DataTextField = "FILE_CODE";
                cmbFileCode.DataSource = dt;
                cmbFileCode.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbFileType_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindFileType();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtFileCode.Visible = false;
            cmbFileCode.Visible = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
            trView.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/FileManagement/Pages/FileUploading.aspx", false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// To find the Maximum count from table..
    /// </summary>
    private void MaxFileCode()
    {
        try
        {
            string x = "";
            int y = 0;

            DataTable dt = SaitexBL.Interface.Method.FM_FILE_UPLOAD.GetMaxFileCode();

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
                        txtFileCode.Text = y.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Use of Intialization. Means blank controls.
    /// </summary>
    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Save";
            tdSave.Visible = true;
            txtFileCode.Visible = true;
            BlankControls();
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            lblErrorMessage.Text = "";
            lblMessage.Text = "";
            cmbFileCode.Visible = false;
            trView.Visible = false;
            MaxFileCode();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Insertion the data.
    /// </summary>
    private void InsertData()
    {
        try
        {
            int iRecordFound = 0;

            SaitexDM.Common.DataModel.FM_FILE_UPLOAD oFM_FILE_UPLOAD = new SaitexDM.Common.DataModel.FM_FILE_UPLOAD();

            oFM_FILE_UPLOAD.FILE_CODE = txtFileCode.Text.ToUpper().Trim();
            oFM_FILE_UPLOAD.FILE_NAME = txtFileName.Text.ToUpper().Trim();
            oFM_FILE_UPLOAD.FILE_GROUP = cmbFileGroup.SelectedText.ToUpper().Trim();
            oFM_FILE_UPLOAD.FILE_REF = txtFileReference.Text.ToUpper().Trim();
            oFM_FILE_UPLOAD.FILE_TYPE = cmbFileType.SelectedText.ToUpper().Trim();
            oFM_FILE_UPLOAD.DESCRIPTION = txtDescription.Text.Trim();
            oFM_FILE_UPLOAD.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFM_FILE_UPLOAD.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFM_FILE_UPLOAD.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFM_FILE_UPLOAD.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oFM_FILE_UPLOAD.TUSER = oUserLoginDetail.UserCode;
            oFM_FILE_UPLOAD.STATUS = true;

            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];

            Stream fs = tPhoto.PostedFile.InputStream;

            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }

            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                oFM_FILE_UPLOAD.SUB_CAT_IMG = bytearr;
                oFM_FILE_UPLOAD.FILE_EXTENSION = tPhoto.PostedFile.ContentType;
                oFM_FILE_UPLOAD.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
            }
            else
            {
                oFM_FILE_UPLOAD.FILE_EXTENSION = "";
                oFM_FILE_UPLOAD.POSTED_LENGTH = 0;
            }

            bool bResult = SaitexBL.Interface.Method.FM_FILE_UPLOAD.Insert(oFM_FILE_UPLOAD, out iRecordFound);

            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                InitialisePage();
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record already exists :Pls Enter Another Record');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
    /// <summary>
    /// Fill File Type from Master Of Transaction table..   "FM_TYPE" 
    /// </summary>
    private void bindFileType()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FM_FILE_UPLOAD.GetFileTypeMOM();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbFileType.DataTextField = "MST_CODE";
                cmbFileType.DataValueField = "MST_CODE";
                cmbFileType.DataSource = dt;
                cmbFileType.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Blank Controls
    /// </summary>
    private void BlankControls()
    {
        try
        {
            txtFileName.Text = "";
            cmbFileType.SelectedText = "";
            cmbFileGroup.SelectedText = "";
            cmbFileGroup.SelectedIndex = -1;
            cmbFileType.SelectedIndex = -1;
            txtFileReference.Text = "";
            txtDescription.Text = "";
            lblMode.Text = "Save";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Link Button for Open a Pop-Up with sending File Code, according to File Code
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnView_Click(object sender, EventArgs e)
    {
        try
        {
            if (cmbFileCode.SelectedIndex != -1)
            {
                string FILE_CODE = string.Empty;
                FILE_CODE = cmbFileCode.SelectedValue.ToString().Trim();
                string URL = "ViewFile.aspx?FILE_CODE=" + FILE_CODE;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=1,menubar=1,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Dear ! Please select the File..');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// Update Record
    /// </summary>
    private void UpdateData()
    {
        try
        {
            int iRecordFound = 0;

            SaitexDM.Common.DataModel.FM_FILE_UPLOAD oFM_FILE_UPLOAD = new SaitexDM.Common.DataModel.FM_FILE_UPLOAD();

            oFM_FILE_UPLOAD.FILE_CODE = cmbFileCode.SelectedValue.ToString().Trim();
            oFM_FILE_UPLOAD.FILE_NAME = txtFileName.Text.ToUpper().Trim();
            oFM_FILE_UPLOAD.FILE_GROUP = cmbFileGroup.SelectedText.ToUpper().Trim();
            oFM_FILE_UPLOAD.FILE_REF = txtFileReference.Text.ToUpper().Trim();
            oFM_FILE_UPLOAD.FILE_TYPE = cmbFileType.SelectedText.ToUpper().Trim();
            oFM_FILE_UPLOAD.DESCRIPTION = txtDescription.Text.Trim();
            oFM_FILE_UPLOAD.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFM_FILE_UPLOAD.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFM_FILE_UPLOAD.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFM_FILE_UPLOAD.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oFM_FILE_UPLOAD.TUSER = oUserLoginDetail.UserCode;
            oFM_FILE_UPLOAD.STATUS = true;

            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];

            Stream fs = tPhoto.PostedFile.InputStream;

            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }

            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                oFM_FILE_UPLOAD.SUB_CAT_IMG = bytearr;
                oFM_FILE_UPLOAD.FILE_EXTENSION = tPhoto.PostedFile.ContentType;
                oFM_FILE_UPLOAD.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
            }
            else
            {
                oFM_FILE_UPLOAD.FILE_EXTENSION = "";
                oFM_FILE_UPLOAD.POSTED_LENGTH = 0;
            }

            bool bResult = SaitexBL.Interface.Method.FM_FILE_UPLOAD.Update(oFM_FILE_UPLOAD, out iRecordFound);

            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully !');", true);
                InitialisePage();
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record already exists :Pls Enter Another Record');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated !');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// To delete the data, (Update DEL_STATUS = 1)
    /// </summary>
    private void DeleteData()
    {
        try
        {
            SaitexDM.Common.DataModel.FM_FILE_UPLOAD oFM_FILE_UPLOAD = new SaitexDM.Common.DataModel.FM_FILE_UPLOAD();

            oFM_FILE_UPLOAD.FILE_CODE = txtFileCode.Text.ToUpper().Trim();
            oFM_FILE_UPLOAD.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFM_FILE_UPLOAD.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFM_FILE_UPLOAD.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFM_FILE_UPLOAD.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;

            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];

            Stream fs = tPhoto.PostedFile.InputStream;

            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }

            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                oFM_FILE_UPLOAD.SUB_CAT_IMG = bytearr;
                oFM_FILE_UPLOAD.FILE_EXTENSION = tPhoto.PostedFile.ContentType;
                oFM_FILE_UPLOAD.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
            }
            else
            {
                oFM_FILE_UPLOAD.FILE_EXTENSION = "";
                oFM_FILE_UPLOAD.POSTED_LENGTH = 0;
            }

            bool bResult = SaitexBL.Interface.Method.FM_FILE_UPLOAD.Delete(oFM_FILE_UPLOAD);

            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
                InitialisePage();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Deleted !');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Fill File Group from Master Of Transaction table..    "FM_GROUP" 
    /// </summary>
    private void bindFileGroup()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FM_FILE_UPLOAD.GetFileGroupMOM();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbFileGroup.DataTextField = "MST_CODE";
                cmbFileGroup.DataValueField = "MST_CODE";
                cmbFileGroup.DataSource = dt;
                cmbFileGroup.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void cmbFileGroup_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindFileGroup();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
}
