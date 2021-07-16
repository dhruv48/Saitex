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

public partial class Module_Mail_Controls_ReadMail : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static Hashtable htReadMail;
    public static Hashtable htSendMail;
    public static bool bReceive;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                trDetail.Visible = false;
                trReplyForward.Visible = false;

                bReceive = false;

                if (Session["htReadMail"] != null)
                {
                    htReadMail = Session["htReadMail"] as Hashtable;
                    bReceive = true;
                    Session["htReadMail"] = null;
                }

                if (Session["dtSendMail"] != null)
                {
                    htSendMail = Session["dtSendMail"] as Hashtable;
                    Session["dtSendMail"] = null;
                }

                if (bReceive)
                {
                    BindReceivedMail();
                    MarkAsRead();
                    lbtnInbox.Text = "Back To Inbox";
                }
                else
                {
                    BindSentMail();
                    lbtnInbox.Text = "Back To Mails";
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail"));
        }
    }

    private void BindReceivedMail()
    {
        try
        {
            if (htReadMail.Count > 0)
            {
                lblSubject.Text = htReadMail["SUBJECT"].ToString();
                lblFrom.Text = htReadMail["FROM_EMAIL_ADD"].ToString();
                lblEmail_Add.Text = htReadMail["EMAIL_ADD"].ToString();
                lblDateTime.Text = htReadMail["TDATE"].ToString();
                lblTo.Text = htReadMail["TO_EMAIL_ADD"].ToString();
                lblCc.Text = htReadMail["CC_EMAIL_ADD"].ToString();
                lblBcc.Text = htReadMail["BCC_EMAIL_ADD"].ToString();
                dvBody.InnerHtml = htReadMail["BODY"].ToString();


            }
        }
        catch
        {
            throw;
        }
    }

    private void BindSentMail()
    {
        try
        {
            if (htSendMail.Count > 0)
            {
                lblSubject.Text = htSendMail["SUBJECT"].ToString();
                lblFrom.Text = htSendMail["EMAIL_ADD"].ToString();
                lblEmail_Add.Text = htSendMail["EMAIL_ADD"].ToString();
                lblDateTime.Text = htSendMail["TDATE"].ToString();
                lblTo.Text = htSendMail["TO_EMAIL_ADD"].ToString();
                lblCc.Text = htSendMail["CC_EMAIL_ADD"].ToString();
                lblBcc.Text = htSendMail["BCC_EMAIL_ADD"].ToString();
                dvBody.InnerHtml = htSendMail["BODY"].ToString();

            }
        }
        catch
        {
            throw;
        }
    }

    protected void lbtnShowDetail_Click(object sender, EventArgs e)
    {
        if (lbtnShowDetail.Text == "Show Details")
        {
            trDetail.Visible = true;
            lbtnShowDetail.Text = "Hide Details";
        }
        else if (lbtnShowDetail.Text == "Hide Details")
        {
            trDetail.Visible = false;
            lbtnShowDetail.Text = "Show Details";
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (bReceive)
                DeleteMsgReceive();
            else
                DeleteMsgSend();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting mail.\r\nSee error log for detail"));
        }
    }

    protected void ddlOtherOpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOtherOpt.SelectedItem.Text.Equals("Reply"))
            {
                Compose1._Body = dvBody.InnerHtml.ToString();
                Compose1._From = lblEmail_Add.Text;
                Compose1._Subject = "Rpl: " + lblSubject.Text;
                Compose1._To = lblTo.Text;
                Compose1._Heading = "Reply";
                Compose1.FillReplyVal();
                trReplyForward.Visible = true;
            }
            else if (ddlOtherOpt.SelectedItem.Text.Equals("Forward"))
            {
                Compose1._Body = dvBody.InnerHtml.ToString();
                Compose1._From = lblEmail_Add.Text;
                Compose1._Subject = "Fwd: " + lblSubject.Text;
                Compose1._Heading = "Forward";
                Compose1.FillReplyVal();
                trReplyForward.Visible = true;
            }
            else if (ddlOtherOpt.SelectedItem.Text.Equals("Delete"))
            {
                if (bReceive)
                    DeleteMsgReceive();
                else
                    DeleteMsgSend();
            }

        }
        catch
        {
            throw;
        }
    }

    private void DeleteMsgReceive()
    {
        try
        {
            SaitexDM.Common.DataModel.Mail.ReceiveMail oReceiveMail = new SaitexDM.Common.DataModel.Mail.ReceiveMail();
            oReceiveMail.USER_CODE = oUserLoginDetail.UserCode;
            oReceiveMail.EMAIL_ADD = htReadMail["EMAIL_ADD"].ToString();
            oReceiveMail.RECEIVE_MAIL_ID = int.Parse(htReadMail["RECEIVE_MAIL_ID"].ToString());

            bool bDeleted = SaitexBL.Interface.Method.Mail.SendReceiveMail.DeleteReceiveMail(oReceiveMail);

            if (bDeleted)
            {
                Common.CommonFuction.ShowMessage("Mail Deleted Successfully.");
                Server.Transfer("~/Module/Mail/Pages/Inbox.aspx");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Mail Deleted Failed.");
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteMsgSend()
    {
        try
        {
            SaitexDM.Common.DataModel.Mail.SendMail oSendMail = new SaitexDM.Common.DataModel.Mail.SendMail();
            oSendMail.USER_CODE = oUserLoginDetail.UserCode;
            oSendMail.EMAIL_ADD = htReadMail["EMAIL_ADD"].ToString();
            oSendMail.SEND_MAIL_ID = int.Parse(htReadMail["SEND_MAIL_ID"].ToString());

            bool bDeleted = SaitexBL.Interface.Method.Mail.SendReceiveMail.DeleteSendMail(oSendMail);

            if (bDeleted)
            {
                Common.CommonFuction.ShowMessage("Mail Deleted Successfully.");
                Server.Transfer("~/Module/Mail/Pages/SentMail.aspx");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Mail Deleted Failed.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void lbtnDiscardReply_Click(object sender, EventArgs e)
    {
        try
        {
            Compose1._Body = string.Empty;
            Compose1._From = string.Empty;
            Compose1._Subject = string.Empty;
            Compose1._To = string.Empty;
            Compose1._Heading = "Compose";
            ddlOtherOpt.SelectedIndex = -1;
            trReplyForward.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void MarkAsRead()
    {
        try
        {
            if (htReadMail["READ_STATUS"].ToString().Equals("Un-Read"))
            {
                SaitexDM.Common.DataModel.Mail.ReceiveMail oReceiveMail = new SaitexDM.Common.DataModel.Mail.ReceiveMail();
                oReceiveMail.USER_CODE = oUserLoginDetail.UserCode;
                oReceiveMail.EMAIL_ADD = htReadMail["EMAIL_ADD"].ToString();
                oReceiveMail.RECEIVE_MAIL_ID = int.Parse(htReadMail["RECEIVE_MAIL_ID"].ToString());
                oReceiveMail.TUSER = oUserLoginDetail.UserCode;
                oReceiveMail.READ_STATUS = true;

                bool bDeleted = SaitexBL.Interface.Method.Mail.SendReceiveMail.MarkAsReadReceivedMail(oReceiveMail);
            }
        }
        catch
        {
            throw;
        }
    }
    protected void lbtnInbox_Click(object sender, EventArgs e)
    {
        string URL = string.Empty;
        if (bReceive)
        {
            URL = "~/Module/Mail/Pages/Inbox.aspx";
        }
        else
        {
            URL = "~/Module/Mail/Pages/SentMail.aspx";
        }
        Server.Transfer(URL, true);
    }
}
