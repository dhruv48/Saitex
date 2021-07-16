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
using SaitexDM.Common.DataModel.Mail;
using SaitexDM.Common.DataModel;
using System.Net.Mail;
using System.Text;
//using System.Web.Mail;
using Common;
using System.IO;

public partial class Module_Mail_Controls_Compose : System.Web.UI.UserControl
{
    UserLoginDetail oUserLoginDetail;
    public static DataTable dtSavedIDs;

    public string _From
    {
        get;
        set;
    }

    public string _To
    {
        get;
        set;
    }

    public string _Body
    {
        get;
        set;
    }

    public string _Subject
    {
        get;
        set;
    }

    public bool _HasAttach
    {
        get;
        set;
    }
    public string _Heading
    {
        get;
        set;
    }

    public void FillReplyVal()
    {
        txtSubject.Text = _Subject;
        txtBody.Content = _Body;
        txtTo.Text = _To;
        lblComposeHeading.Text = _Heading;
        ddlFrom.SelectedItem.Text = _From;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            GetDataForFromAddress();
            InitializePage();
        }
    }

    private void InitializePage()
    {
        try
        {
            txtBCC.Text = string.Empty;
            txtBody.Content = string.Empty;
            txtCC.Text = string.Empty;
            txtSubject.Text = string.Empty;
            txtTo.Text = string.Empty;

            tblAddCC.Visible = false;
            tblAddBCC.Visible = false;

            BindFromDropDown();
        }
        catch
        {
            throw;
        }
    }

    private void BindFromDropDown()
    {
        try
        {
            DataTable dtFromAddress = (DataTable)ViewState["dtFromAddress"];

            ddlFrom.Items.Clear();

            if (dtFromAddress != null && dtFromAddress.Rows.Count > 0)
            {
                ddlFrom.DataSource = dtFromAddress;
                ddlFrom.DataTextField = "EMAIL_ADD";
                ddlFrom.DataValueField = "EMAIL_ADD";
                ddlFrom.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("Sorry '" + oUserLoginDetail.Username + "'!!! You have not configured any Email Account.");
            }
        }
        catch
        {
            throw;
        }
    }

    private void GetDataForFromAddress()
    {
        try
        {
            MailSetting oMailSetting = new MailSetting();
            oMailSetting.USER_CODE = oUserLoginDetail.UserCode;

            DataTable dtFromAddress = SaitexBL.Interface.Method.Mail.MailSetting.GetSettingByUser(oMailSetting);
            ViewState["dtFromAddress"] = dtFromAddress;
        }
        catch
        {
            throw;
        }
    }

    protected void lbtnAddCC_Click(object sender, EventArgs e)
    {
        tblAddCC.Visible = true;
    }

    protected void lbtnAddBCC_Click(object sender, EventArgs e)
    {
        tblAddBCC.Visible = true;
    }

    protected void lbtnAddAttachment_Click(object sender, EventArgs e)
    {

    }

    protected void imgbtnSend_Click(object sender, ImageClickEventArgs e)
    {
        try
        {   
            MailMessage mm = CreateMailDetail();
            DataTable dtFromAddress = (DataTable)ViewState["dtFromAddress"];
            int SEND_MAIL_ID = 0;
            SaveMail(mm, dtFromAddress, ddlFrom.SelectedValue.Trim(), out SEND_MAIL_ID);
            SendMail(mm, dtFromAddress, ddlFrom.SelectedValue.Trim(), SEND_MAIL_ID);
            CommonFuction.ShowMessage(@"Mail sent successfully.");
            InitializePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Mail not send."));
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            MailMessage mm = CreateMailDetail();
            DataTable dtFromAddress = (DataTable)ViewState["dtFromAddress"];
            int SEND_MAIL_ID = 0;
            SaveMail(mm, dtFromAddress, ddlFrom.SelectedValue.Trim(), out SEND_MAIL_ID);
            CommonFuction.ShowMessage(@"Mail saved successfully.");
            InitializePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Mail not send."));
        }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        InitializePage();
    }

    private bool SaveMail(MailMessage oMailMessage, DataTable dtFromAddress, string SelectedFrom, out int SendMailId)
    {
        SendMailId = 0;
        try
        {
            DataView dvFromDetail = new DataView(dtFromAddress);
            dvFromDetail.RowFilter = "EMAIL_ADD='" + SelectedFrom + "'";
            if (dvFromDetail.Count > 0)
            {
                SendMail oSendMail = new SendMail();
                ReceiveMail oReceiveMail = new ReceiveMail();

                string Attach_Ids = string.Empty;

                #region code to save attachment
                HttpFileCollection files = Page.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    oSendMail.HAS_ATTACH = false;
                    HttpPostedFile file = files[i];

                    int iCount = 0;

                    if (file.FileName.Length > 0)
                    {
                        iCount += 1;

                        AttachmentFile oAttachmentFile = new AttachmentFile();

                        oAttachmentFile.ATTACH_EXTENSION = file.ContentType;
                        oAttachmentFile.ATTACH_ID = 0;
                        oAttachmentFile.ATTACH_NAME = file.FileName;
                        oAttachmentFile.ATTACH_TYPE = file.ContentType;
                        oAttachmentFile.DESCRIPTION = file.FileName;
                        oAttachmentFile.POSTED_LENGTH = file.ContentLength;

                        byte[] bytearr = new byte[file.ContentLength];
                        Stream fs = file.InputStream;
                        fs.Read(bytearr, 0, file.ContentLength);

                        oAttachmentFile.SUB_CAT_IMG = bytearr;

                        oAttachmentFile.TUSER = oUserLoginDetail.UserCode;

                        int Attach_id = SaitexBL.Interface.Method.Mail.SendReceiveMail.SaveAttachment(oAttachmentFile);
                        if (Attach_id > 0)
                        {
                            if (!Attach_Ids.Equals(string.Empty))
                                Attach_Ids += ",";
                            Attach_Ids += Attach_id.ToString();
                        }
                    }
                    if (iCount > 0)
                        oReceiveMail.HAS_ATTACH = true;
                }
                #endregion

                #region code to save sendmail details
                oSendMail.ATTACH_IDS = Attach_Ids;
                oSendMail.BCC_EMAIL_ID = txtBCC.Text.Trim();
                oSendMail.BODY = oMailMessage.Body;
                oSendMail.CC_EMAIL_ID = txtCC.Text.Trim();
                oSendMail.EMAIL_ADD = SelectedFrom;
                oSendMail.SEND_MAIL_ID = 0;
                oSendMail.SEND_STATUS = false;
                oSendMail.SUBJECT = oMailMessage.Subject;
                oSendMail.TO_EMAIL_ADD = txtTo.Text.Trim();
                oSendMail.TUSER = oUserLoginDetail.UserCode;
                oSendMail.USER_CODE = oUserLoginDetail.UserCode;

                SendMailId = SaitexBL.Interface.Method.Mail.SendReceiveMail.SaveSendMail(oSendMail);

                #endregion

                #region code to save receivemail details

                ArrayList receiverList = GetReceiverList();
                for (int iLoop = 0; iLoop < receiverList.Count; iLoop++)
                {
                    string Receiver = receiverList[iLoop].ToString();
                    MailSetting oMailSetting = new MailSetting();
                    oMailSetting.EMAIL_ADD = Receiver;
                    string User_Code = SaitexBL.Interface.Method.Mail.MailSetting.GetUserByEmail(oMailSetting);
                    if (User_Code != string.Empty)
                    {

                        oReceiveMail.USER_CODE = User_Code;
                        oReceiveMail.EMAIL_ADD = Receiver;
                        oReceiveMail.RECEIVE_MAIL_ID = 0;
                        oReceiveMail.FROM_EMAIL_ADD = SelectedFrom;
                        oReceiveMail.TO_EMAIL_ADD = txtTo.Text.Trim();
                        oReceiveMail.CC_EMAIL_ID = txtCC.Text.Trim();
                        oReceiveMail.BCC_EMAIL_ID = txtBCC.Text.Trim();
                        oReceiveMail.ATTACH_IDS = Attach_Ids;
                        oReceiveMail.SUBJECT = oMailMessage.Subject;
                        oReceiveMail.BODY = oMailMessage.Body;
                        oReceiveMail.READ_STATUS = false;
                        oReceiveMail.TUSER = oUserLoginDetail.UserCode;

                        SaitexBL.Interface.Method.Mail.SendReceiveMail.SaveReceiveMail(oReceiveMail);
                    }
                }


                #endregion
            }
            return true;
        }
        catch
        {
            throw;
        }
    }

    private bool SendMail(MailMessage message, DataTable dtFromAddress, string SelectedFrom, int SEND_MAIL_ID)
    {
        try
        {
            DataView dvFromDetail = new DataView(dtFromAddress);
            dvFromDetail.RowFilter = "EMAIL_ADD='" + SelectedFrom + "'";
            if (dvFromDetail.Count > 0)
            {
                SmtpClient oSmtpClient = new SmtpClient();

                MailAddress fromAddress = new MailAddress(SelectedFrom, dvFromDetail[0]["DISP_NAME"].ToString());

                message.From = fromAddress;

                oSmtpClient.Host = dvFromDetail[0]["OUTGOING_SERVER"].ToString();

                //Default port will be 25
                oSmtpClient.Port = int.Parse(dvFromDetail[0]["OUTGOING_PORT"].ToString());

                if (dvFromDetail[0]["OUTGOING_SSL"].ToString() == "1")
                    oSmtpClient.EnableSsl = true;
                else
                    oSmtpClient.EnableSsl = false;

                oSmtpClient.Credentials = new System.Net.NetworkCredential(dvFromDetail[0]["USER_NAME"].ToString(), CommonFuction.base64Decode(dvFromDetail[0]["PASSWORD"].ToString()));

                // Send SMTP mail
                oSmtpClient.Send(message);

                SendMail oSendMail = new SendMail();
                oSendMail.SEND_STATUS = true;
                oSendMail.SEND_MAIL_ID = SEND_MAIL_ID;
                oSendMail.USER_CODE = oUserLoginDetail.UserCode;
                oSendMail.EMAIL_ADD = SelectedFrom;
                oSendMail.TUSER = oUserLoginDetail.UserCode;

                SaitexBL.Interface.Method.Mail.SendReceiveMail.MarkAsSentSendMail(oSendMail);
            }
            return true;
        }
        catch
        {
            throw;
        }
    }

    private ArrayList GetReceiverList()
    {
        try
        {
            ArrayList Receiver = new ArrayList();

            string sTo = txtTo.Text.Trim();

            if (sTo.Contains(","))
            {
                string[] To = sTo.Split(',');
                if (To.Length > 1)
                {
                    foreach (string Tos in To)
                    {
                        Receiver.Add(Tos);
                    }
                }
            }
            else
                Receiver.Add(sTo);

            string sCC = txtCC.Text.Trim();

            if (!sCC.Equals(string.Empty))
            {
                if (sCC.Contains(","))
                {
                    string[] Cc = sCC.Split(',');
                    if (Cc.Length > 1)
                    {
                        foreach (string CC in Cc)
                        {
                            Receiver.Add(CC);
                        }
                    }
                }
                else
                    Receiver.Add(sCC);
            }

            string sBCC = txtBCC.Text.Trim();

            if (!sBCC.Equals(string.Empty))
            {
                if (sBCC.Contains(","))
                {
                    string[] BCc = sBCC.Split(',');
                    if (BCc.Length > 1)
                    {
                        foreach (string BCC in BCc)
                        {
                            Receiver.Add(BCC);
                        }
                    }
                }
                else
                    Receiver.Add(sBCC);
            }

            return Receiver;
        }
        catch
        {
            throw;
        }
    }

    private MailMessage CreateMailDetail()
    {
        try
        {
            MailMessage message = new MailMessage();

            #region Add To
            string sTo = txtTo.Text.Trim();

            if (sTo.Contains(","))
            {
                string[] To = sTo.Split(',');
                if (To.Length > 1)
                {
                    foreach (string Tos in To)
                    {
                        message.To.Add(Tos);
                    }
                }
            }
            else
                message.To.Add(sTo);
            #endregion
            
            #region Add CC
            string sCC = txtCC.Text.Trim();

            if (!sCC.Equals(string.Empty))
            {
                if (sCC.Contains(","))
                {
                    string[] Cc = sCC.Split(',');
                    if (Cc.Length > 1)
                    {
                        foreach (string CC in Cc)
                        {
                            message.CC.Add(CC);
                        }
                    }
                }
                else
                    message.CC.Add(sCC);
            }
            #endregion
            
            #region Add BCC
            string sBCC = txtBCC.Text.Trim();

            if (!sBCC.Equals(string.Empty))
            {
                if (sBCC.Contains(","))
                {
                    string[] BCc = sBCC.Split(',');
                    if (BCc.Length > 1)
                    {
                        foreach (string BCC in BCc)
                        {
                            message.Bcc.Add(BCC);
                        }
                    }
                }
                else
                    message.Bcc.Add(sBCC);
            }
            #endregion

            message.Body = txtBody.Content.Trim();

            // message.IsBodyHtml = true;

            //  message.Priority = MailPriority.High;

            message.Subject = txtSubject.Text.Trim();

            #region Code for attachment

            HttpFileCollection files = Page.Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                if (file.FileName.Length > 0)
                {
                    Attachment oAttachment = new Attachment(file.FileName);

                    message.Attachments.Add(oAttachment);
                }
            }

            #endregion


            return message;
        }
        catch
        {
            throw;
        }
    }

}
