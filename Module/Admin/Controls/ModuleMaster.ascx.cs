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

public partial class Module_Admin_Controls_ModuleMaster : System.Web.UI.UserControl
{
    string strMaxId = string.Empty;
    string strTUser = string.Empty;
    SaitexDM.Common.DataModel.CM_MODULE_MST oCMMODULEMST = new SaitexDM.Common.DataModel.CM_MODULE_MST();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {
            pnlRed.Visible = false;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strTUser = oUserLoginDetail.UserCode;
            {
                if (!IsPostBack)
                {
                    bindAddModuleMaster();
                    BlanksControls();
                    chk_Status.Checked = true;
                    ViewState["MaxModuleId"] = SaitexBL.Interface.Method.CM_MODULE_MST.GetNewModuleNumber();
                }
            }
        }
    }

    private void BlanksControls()
    {
        try
        {
            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            txtModuleName.Text = "";
            txtDisplayOrder.Text = "";
            txtRemarks.Text = "";
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }
    }

    private void bindAddModuleMaster()
    {
        try
        {
            DataTable dtmodulemaster = SaitexBL.Interface.Method.CM_MODULE_MST.GetModuleMaster();
            if (dtmodulemaster != null && dtmodulemaster.Rows.Count > 0)
            {

                lblTotalRecord.Text = dtmodulemaster.Rows.Count.ToString();
                gvAddModule.DataSource = dtmodulemaster;
                gvAddModule.DataBind();
                ViewState["dtmodulemaster"] = dtmodulemaster;
            }

        }


        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {


        }

    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iRecordFound = 0;

            if (ViewState["MaxModuleId"] != null)
            {
                strMaxId = ViewState["MaxModuleId"].ToString();
            }

            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
            Stream fs = tPhoto.PostedFile.InputStream;
            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }
            if (ViewState["MaxModuleId"] != null)
            {
                oCMMODULEMST.MDL_ID = Convert.ToInt32(ViewState["MaxModuleId"].ToString());
            }


            oCMMODULEMST.MDL_NAME = Common.CommonFuction.funFixQuotes(txtModuleName.Text.Trim());
            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                if (tPhoto.PostedFile.ContentType == "image/jpeg" || tPhoto.PostedFile.ContentType == "image/pjpeg" || tPhoto.PostedFile.ContentType == "image/gif" || tPhoto.PostedFile.ContentType == "image/bmp" || tPhoto.PostedFile.ContentType == "image/x-png" || tPhoto.PostedFile.ContentType == "image/png")
                {
                    oCMMODULEMST.SUB_CAT_IMG = bytearr;
                    oCMMODULEMST.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
                    oCMMODULEMST.SUB_CAT_CONTENT_TYPE = tPhoto.PostedFile.ContentType;
                }
            }
            if (txtDisplayOrder.Text != "")
            {
                oCMMODULEMST.DISP_ODR = Convert.ToSingle(Common.CommonFuction.funFixQuotes(txtDisplayOrder.Text.Trim()));
            }
            oCMMODULEMST.REMARKS = Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            if (chk_Status.Checked)
            {
                oCMMODULEMST.STATUS = true;
            }
            else
            {
                oCMMODULEMST.STATUS = false;

            }
            oCMMODULEMST.TUSER = strTUser;
            bool Result = SaitexBL.Interface.Method.CM_MODULE_MST.InsertModuleMaster(oCMMODULEMST, out iRecordFound);

            if (Result)
            {
                BlanksControls();
                //bindAddModuleMaster('S');
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Module Saved Successfully');", true);

                //lblRedirect.Text = "Data Saved Successfully<br />Go To AddChildMenu";
                //mpeRed0.Show();
                bindAddModuleMaster();

            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Module Already Exists');", true);


            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Module Data Not Saved');", true);

            }

        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Module Data Not Saved');", true);


        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            UpdateModuleData(Convert.ToInt32(ViewState["editId"].ToString()));
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Module Data Updated ');", true);


        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        BlanksControls();
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/rptModule.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                string test = Session["RedirectURL"].ToString();
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
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void gvAddModule_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAddModule.PageIndex = e.NewPageIndex;
        bindAddModuleMaster();
    }

    protected void gvAddModule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMessage.Text = "";
        lblErrorMessage.Text = "";
        if (e.CommandName == "ImageEdit")
        {
            //btnSave.Visible = false;
            //btnUpdate.Visible = true;
            ViewState["editId"] = e.CommandArgument.ToString().Trim();
            getAddModuleData(Convert.ToInt32(e.CommandArgument));

        }

        if (e.CommandName == "ImageDelete")
        {

            deleteAddModuleData(Convert.ToInt32(e.CommandArgument));
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {

    }

    protected void btnRedirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AddChildMenu.aspx");
    }

    protected void btnCancelRed_Click(object sender, EventArgs e)
    {

    }

    private void getAddModuleData(int iAddModuleData)
    {
        try
        {
            if (ViewState["dtmodulemaster"] != null)
            {
                DataTable dtmodulemaste = (DataTable)ViewState["dtmodulemaster"];
                DataView dv = new DataView(dtmodulemaste);
                dv.RowFilter = "MDL_ID=" + iAddModuleData;
                if (dv != null && dv.Count > 0)
                {
                    txtModuleName.Text = dv[0]["MDL_NAME"].ToString().Trim();
                    txtDisplayOrder.Text = dv[0]["DISP_ODR"].ToString().Trim();
                    txtRemarks.Text = dv[0]["REMARKS"].ToString().Trim();
                    if (dv[0]["STATUS"].ToString().Trim() == "1")
                    {
                        chk_Status.Checked = true;
                    }
                    lblMode.Text = "Update";
                    tdSave.Visible = false;
                    tdUpdate.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }


    }

    private void UpdateModuleData(int ModuleID)
    {
        try
        {
            int iRecordFound = 0;

            oCMMODULEMST.MDL_ID = ModuleID;
            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
            Stream fs = tPhoto.PostedFile.InputStream;
            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }



            oCMMODULEMST.MDL_NAME = Common.CommonFuction.funFixQuotes(txtModuleName.Text.Trim());
            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                if (tPhoto.PostedFile.ContentType == "image/jpeg" || tPhoto.PostedFile.ContentType == "image/pjpeg" || tPhoto.PostedFile.ContentType == "image/gif" || tPhoto.PostedFile.ContentType == "image/bmp" || tPhoto.PostedFile.ContentType == "image/x-png" || tPhoto.PostedFile.ContentType == "image/png")
                {
                    oCMMODULEMST.SUB_CAT_IMG = bytearr;
                    oCMMODULEMST.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
                    oCMMODULEMST.SUB_CAT_CONTENT_TYPE = tPhoto.PostedFile.ContentType;
                }
            }
            oCMMODULEMST.DISP_ODR = Convert.ToSingle(txtDisplayOrder.Text.Trim());
            oCMMODULEMST.REMARKS = Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            if (chk_Status.Checked)
            {
                oCMMODULEMST.STATUS = true;
            }
            else
            {
                oCMMODULEMST.STATUS = false;

            }
            oCMMODULEMST.TUSER = strTUser;

            bool Result = SaitexBL.Interface.Method.CM_MODULE_MST.UpdateModuleMaster(oCMMODULEMST, out iRecordFound);
            if (Result)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Module Updated Successfully');", true);
                bindAddModuleMaster();
            }
            else if (iRecordFound > 0)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Module Name Already Exists');", true);

            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;

        }

    }

    private void deleteAddModuleData(int MouduleID)
    {
        oCMMODULEMST.MDL_ID = MouduleID;
        oCMMODULEMST.TUSER = strTUser;
        bool result = SaitexBL.Interface.Method.CM_MODULE_MST.DeleteModuleMaster(oCMMODULEMST);
        if (result)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Module Deleted Successfully');", true);
            bindAddModuleMaster();
        }


    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        imgbtnSave.Attributes.Add("onClick", "javascript:return confirm('You sure you want to Save ?');");

    }
}