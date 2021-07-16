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

public partial class CommonControls_DashBoard : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblWelcomeMsg.Text = "Welcome Dear '" + oUserLoginDetail.Username + "'";
                ShowMails();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    private void ShowMails()
    {
        try
        {
            SaitexDM.Common.DataModel.Mail.ReceiveMail oReceiveMail = new SaitexDM.Common.DataModel.Mail.ReceiveMail();
            oReceiveMail.USER_CODE = oUserLoginDetail.UserCode;
            DataTable dtDashBoardMail = SaitexBL.Interface.Method.Mail.SendReceiveMail.GetReceivedMailForDashBoard(oReceiveMail);

            if (dtDashBoardMail != null && dtDashBoardMail.Rows.Count > 0)
            {
                dlmailDetail.DataSource = dtDashBoardMail;
                dlmailDetail.DataBind();
                lblNoMail.Text = string.Empty;
            }
            else
            {
                lblNoMail.Text = "No Mails in your Inbox";
            }
        }
        catch
        {
            throw;
        }
    }
}
