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
using System.IO;

using errorLog;
using Common;

public partial class Module_Admin_Controls_AddChildModule : System.Web.UI.UserControl
{
    string strTUser = string.Empty;
    SaitexDM.Common.DataModel.CM_CHILD_MDL_MST oCMCHILDMDLMST = new SaitexDM.Common.DataModel.CM_CHILD_MDL_MST();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                strTUser = oUserLoginDetail.Username;
                if (!IsPostBack)
                {
                    tdUpdate.Visible = false;
                    bindChildModuleGrid();
                    bindMoulde();
                    chk_Status.Checked = true;
                    lblMode.Text = "You are in Save Mode";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindMoulde()
    {
        try
        {
            DataTable dtmodule = SaitexBL.Interface.Method.CM_MODULE_MST.GetModuleMaster();
            if (dtmodule != null && dtmodule.Rows.Count > 0)
            {
                ddlParenModuleName.DataValueField = "MDL_ID";
                ddlParenModuleName.DataTextField = "MDL_NAME";
                ddlParenModuleName.DataSource = dtmodule;
                ddlParenModuleName.DataBind();
                ddlParenModuleName.Items.Insert(0, new ListItem("---------Select----------", ""));
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindChildModuleGrid()
    {
        try
        {
            DataTable dtChildmodule = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.GetChildModuleMaster();
            if (dtChildmodule != null && dtChildmodule.Rows.Count > 0)
            {
                lblTotalRecord.Text = dtChildmodule.Rows.Count.ToString();
                gvChildAddModule.DataSource = dtChildmodule;
                gvChildAddModule.DataBind();
                ViewState["dtChildmodule"] = dtChildmodule;
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
            int irecordFound = 0;
            oCMCHILDMDLMST.CHILD_MDL_ID = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.GetNewChildModuleNumber(); 
            oCMCHILDMDLMST.MDL_ID = Convert.ToInt32(ddlParenModuleName.SelectedValue.ToString());
            oCMCHILDMDLMST.CHILD_MDL_NAME = Common.CommonFuction.funFixQuotes(txtChildModuleName.Text.Trim());

            ////////////////////////////////ImageSave///////////////////////////////////////////////////////////////////
            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
            Stream fs = tPhoto.PostedFile.InputStream;
            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }
            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                if (tPhoto.PostedFile.ContentType == "image/jpeg" || tPhoto.PostedFile.ContentType == "image/pjpeg" || tPhoto.PostedFile.ContentType == "image/gif" || tPhoto.PostedFile.ContentType == "image/bmp" || tPhoto.PostedFile.ContentType == "image/x-png" || tPhoto.PostedFile.ContentType == "image/png")
                {
                    oCMCHILDMDLMST.SUB_CAT_IMG = bytearr;
                    oCMCHILDMDLMST.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
                    oCMCHILDMDLMST.SUB_CAT_CONTENT_TYPE = tPhoto.PostedFile.ContentType;
                }

            }
            ///////////////////////////////////////////////////////End Image Save Code//////////////////////////////////////////
            oCMCHILDMDLMST.DISP_ODR = Convert.ToSingle(Common.CommonFuction.funFixQuotes(txtDisplayOrder.Text.Trim()));
            oCMCHILDMDLMST.REMARKS = Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            if (chk_Status.Checked)
            {
                oCMCHILDMDLMST.STATUS = true;

            }
            else
            {
                oCMCHILDMDLMST.STATUS = false;
            }
            oCMCHILDMDLMST.TUSER = strTUser;
            bool Result = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.InsertChildModuleMaster(oCMCHILDMDLMST, out irecordFound);
            if (Result)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Child Module Saved Successfully');", true);
                bindChildModuleGrid();
                //mpeRed.Show();
                ddlParenModuleName.SelectedIndex = 0;
                txtChildModuleName.Text = string.Empty;
                txtDisplayOrder.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                chk_Status.Checked = false;
            }
            else if (irecordFound > 0)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Child Module Already Exist');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Child Module Saving Failed');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            update();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BlanksControls();
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
            string URL = "../Reports/rptChildModule.aspx";
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void gvChildAddModule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                getChildModuleData(Convert.ToInt32(e.CommandArgument));
                ViewState["RecordEdit"] = e.CommandArgument.ToString().Trim();
            }
            if (e.CommandName == "RecordDelete")
            {
                int ChildModlueId = Convert.ToInt32(e.CommandArgument);
                Delete(ChildModlueId);

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void gvChildAddModule_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvChildAddModule.PageIndex = e.NewPageIndex;
            bindChildModuleGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gridview Paging.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Admin/AddNavigation.aspx");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Redirecting.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnCancelRed_Click(object sender, EventArgs e)
    {

    }

    private void getChildModuleData(int iChildModuleDataId)
    {
        try
        {
            if (ViewState["dtChildmodule"] != null)
            {
                DataTable dt = ViewState["dtChildmodule"] as DataTable;
                DataView dvChildmodule = new DataView(dt);
                dvChildmodule.RowFilter = "CHILD_MDL_ID=" + iChildModuleDataId;
                if (dvChildmodule != null && dvChildmodule.Count > 0)
                {
                    bindMoulde();
                    string dfd = dvChildmodule[0]["MDL_ID"].ToString();
                    ddlParenModuleName.SelectedValue = dvChildmodule[0]["MDL_ID"].ToString();
                    txtChildModuleName.Text = dvChildmodule[0]["CHILD_MDL_NAME"].ToString().Trim();
                    txtDisplayOrder.Text = dvChildmodule[0]["DISP_ODR"].ToString().Trim();
                    txtRemarks.Text = dvChildmodule[0]["REMARKS"].ToString().Trim();
                    if (dvChildmodule[0]["STATUS"].ToString() == "0")
                        chk_Status.Checked = false;
                    else
                        chk_Status.Checked = true;
                    lblMode.Text = "You are in Update Mode";
                    tdSave.Visible = false;
                    tdUpdate.Visible = true;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void update()
    {
        try
        {
            int irecordFound = 0;
            oCMCHILDMDLMST.CHILD_MDL_ID = Convert.ToInt32(ViewState["RecordEdit"].ToString());
            oCMCHILDMDLMST.MDL_ID = Convert.ToInt32(ddlParenModuleName.SelectedValue.ToString());
            oCMCHILDMDLMST.CHILD_MDL_NAME = Common.CommonFuction.funFixQuotes(txtChildModuleName.Text.Trim());
            ////////////////////////////////ImageSave///////////////////////////////////////////////////////////////////
            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
            Stream fs = tPhoto.PostedFile.InputStream;
            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }
            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                if (tPhoto.PostedFile.ContentType == "image/jpeg" || tPhoto.PostedFile.ContentType == "image/pjpeg" || tPhoto.PostedFile.ContentType == "image/gif" || tPhoto.PostedFile.ContentType == "image/bmp" || tPhoto.PostedFile.ContentType == "image/x-png" || tPhoto.PostedFile.ContentType == "image/png")
                {
                    oCMCHILDMDLMST.SUB_CAT_IMG = bytearr;
                    oCMCHILDMDLMST.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
                    oCMCHILDMDLMST.SUB_CAT_CONTENT_TYPE = tPhoto.PostedFile.ContentType;
                }

            }
            ///////////////////////////////////////////////////////End Image Save Code//////////////////////////////////////////
            oCMCHILDMDLMST.DISP_ODR = Convert.ToSingle(Common.CommonFuction.funFixQuotes(txtDisplayOrder.Text.Trim()));
            oCMCHILDMDLMST.REMARKS = Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            if (chk_Status.Checked)
            {
                oCMCHILDMDLMST.STATUS = true;

            }
            else
            {
                oCMCHILDMDLMST.STATUS = false;
            }
            oCMCHILDMDLMST.TUSER = strTUser;

            bool Result = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.UpdateChildModuleMaster(oCMCHILDMDLMST, out irecordFound);
            if (Result)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Child Module Updated Successfully');", true);
                bindChildModuleGrid();
                ddlParenModuleName.SelectedIndex = 0;
                txtChildModuleName.Text = string.Empty;
                txtDisplayOrder.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                chk_Status.Checked = false;
            }
            else if (irecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Child Module Already Exists');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Updatation Fail');", true);

            }
        }
        catch
        {
            throw;
        }
    }

    private void Delete(int ChildModlueId)
    {
        try
        {
            oCMCHILDMDLMST.CHILD_MDL_ID = ChildModlueId;
            oCMCHILDMDLMST.TUSER = strTUser;
            bool result = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.DeleteChildModuleMaster(oCMCHILDMDLMST);
            if (result)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Child Module Deleted Successfully');", true);
                bindChildModuleGrid();
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
            ddlParenModuleName.SelectedValue = "";
            txtChildModuleName.Text = "";
            txtDisplayOrder.Text = "";
            txtRemarks.Text = "";
            lblMode.Text = "You are in Save Mode";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
        }
        catch
        {
            throw;
        }
    }
}