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
using Common;
using SaitexDM.Common.DataModel;
using SaitexDM.Common.DataModel.Mail;
using errorLog;

public partial class Module_Mail_Controls_Setting : System.Web.UI.UserControl
{
    UserLoginDetail oUserLoginDetail;
    private static MailSetting oMailSetting;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                oMailSetting = null;
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in loading page.\r\nSerr error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            txtDispName.Text = string.Empty;
            txtEmailAdd.Text = string.Empty;
            txtIncomingPort.Text = string.Empty;
            txtIncomingServer.Text = string.Empty;
            txtOutgoingPort.Text = string.Empty;
            txtOutgoingServer.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUserName.Text = string.Empty;

            chkDeleteFromServer.Checked = false;
            chkIncomingSsl.Checked = false;
            chkOutgoingSsl.Checked = false;
            chkRememberPwd.Checked = false;
        }
        catch { throw; }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InsertMailSetting();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in saving setting.\r\nSerr error log for detail."));
        }
    }

    private bool ValidateForm(out string msg)
    {
        msg = string.Empty;
        try
        {
            CreateDataModelProperties();
            bool IsExistingMailId = false;
            IsExistingMailId = SaitexBL.Interface.Method.Mail.MailSetting.ExistingMailId(oMailSetting);
            return IsExistingMailId;
        }
        catch { throw; }
    }

    private void CreateDataModelProperties()
    {
        try
        {
            oMailSetting = new MailSetting();
            oMailSetting.DEL_FROM_SERVER = chkDeleteFromServer.Checked;
            oMailSetting.DEL_STATUS = false;
            oMailSetting.DISP_NAME = txtDispName.Text;
            oMailSetting.EMAIL_ADD = txtEmailAdd.Text;
            oMailSetting.INCOMING_PORT = int.Parse(txtIncomingPort.Text);
            oMailSetting.INCOMING_SERVER = txtIncomingServer.Text;
            oMailSetting.INCOMING_SSL = chkIncomingSsl.Checked;
            oMailSetting.OUTGOING_PORT = int.Parse(txtOutgoingPort.Text);
            oMailSetting.OUTGOING_SERVER = txtOutgoingServer.Text;
            oMailSetting.OUTGOING_SSL = chkOutgoingSsl.Checked;
            oMailSetting.PASSWORD = CommonFuction.base64Encode(txtPassword.Text);
            oMailSetting.STATUS = true;
            oMailSetting.TUSER = oUserLoginDetail.UserCode;
            oMailSetting.USER_CODE = oUserLoginDetail.UserCode;
            oMailSetting.USER_NAME = txtUserName.Text;

        }
        catch
        {
            throw;
        }
    }

    private void InsertMailSetting()
    {
        try
        {
            string msg = string.Empty;
            if (!ValidateForm(out msg))
            {
                bool IsSaved = SaitexBL.Interface.Method.Mail.MailSetting.Insert(oMailSetting);
                if (IsSaved)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage("Setting Saved Successfully.");
                }
                else
                {
                    CommonFuction.ShowMessage("Setting Not Saved.");
                }
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
}
