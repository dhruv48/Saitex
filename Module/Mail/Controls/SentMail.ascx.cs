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
using Obout.Grid;

public partial class Module_Mail_Controls_SentMail : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    public string SEND_STATUS
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            GetSentMail();

            if (!IsPostBack)
            {

                BindSentMailGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail"));
        }
    }
    private void GetSentMail()
    {
        try
        {
            SaitexDM.Common.DataModel.Mail.SendMail oSendMail = new SaitexDM.Common.DataModel.Mail.SendMail();
            oSendMail.USER_CODE = oUserLoginDetail.UserCode;

            DataTable dtSendMail = SaitexBL.Interface.Method.Mail.SendReceiveMail.GetSentMail(oSendMail);

            ViewState["dtSendMail"] = dtSendMail;
        }
        catch
        {
            throw;
        }
    }

    private void BindSentMailGrid()
    {
        try
        {
            DataTable dtSendMail = (DataTable)ViewState["dtSendMail"];

            DataView dvSendMail = new DataView(dtSendMail);
            dvSendMail.RowFilter = "SEND_STATUS='" + SEND_STATUS + "'";

            grdSendList.DataSource = dvSendMail;
            grdSendList.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void grdSendList_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList ar = e.RecordsCollection;
            Hashtable ht = ar[0] as Hashtable;
            string URL = string.Empty;
            URL = "~/Module/Mail/Pages/ReadMail.aspx";
            Session["dtSendMail"] = ht;
            Server.Transfer(URL, true);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Mail Detail.\r\nSee error log for detail"));
        }
    }
}
