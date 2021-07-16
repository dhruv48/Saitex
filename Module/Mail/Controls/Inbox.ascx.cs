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

public partial class Module_Mail_Controls_Inbox : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string FOLDER_NAME = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            GetReceivedMail();

            if (!IsPostBack)
            {
                if (Request.QueryString["FOLDER_NAME"] != null && Request.QueryString["FOLDER_NAME"].ToString().Equals(string.Empty) == false)
                {
                    FOLDER_NAME = Request.QueryString["FOLDER_NAME"].ToString();
                }

                BindReceivedMailGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail"));
        }
    }

    private void GetReceivedMail()
    {
        try
        {
            SaitexDM.Common.DataModel.Mail.ReceiveMail oReceiveMail = new SaitexDM.Common.DataModel.Mail.ReceiveMail();
            oReceiveMail.USER_CODE = oUserLoginDetail.UserCode;

            DataTable dtReceivedMail = SaitexBL.Interface.Method.Mail.SendReceiveMail.GetReceivedMail(oReceiveMail);

            ViewState["dtReceivedMail"] = dtReceivedMail;
        }
        catch
        {
            throw;
        }
    }

    private void BindReceivedMailGrid()
    {
        try
        {
            DataTable dtReceivedMail = (DataTable)ViewState["dtReceivedMail"];

            DataView dvReceivedMail = new DataView(dtReceivedMail);
            if (!FOLDER_NAME.Equals(string.Empty))
            {
                dvReceivedMail.RowFilter = "FOLDER_NAME IN '" + FOLDER_NAME + "'";
            }

            grdInboxList.DataSource = dvReceivedMail;
            grdInboxList.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void grdInboxList_Select(object sender, GridRecordEventArgs e)
    {
        try
        {
            ArrayList ar = e.RecordsCollection;
            Hashtable ht = ar[0] as Hashtable;
            string URL = string.Empty;
            URL = "~/Module/Mail/Pages/ReadMail.aspx";
            Session["htReadMail"] = ht;
            Server.Transfer(URL, true);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Mail Detail.\r\nSee error log for detail"));
        }
    }
}
