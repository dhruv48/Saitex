using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security; 
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.Common;
using System.Drawing;
using DBLibrary;
using errorLog;
using System.Windows;
using System.Net.NetworkInformation;
using System.Xml;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

           if (!IsPostBack)
            {
              
                /*************** CREATED BY NISHANT RAI FOR STARTUP ********************************/
                var USER_COUNT = SaitexBL.Interface.Method.CM_USER_MST.GetUserCount();
                if (USER_COUNT.Rows.Count > 0 && USER_COUNT != null)
                {
                    Int64 userCount = 0;
                    Int64.TryParse(USER_COUNT.Rows[0]["USERS"].ToString(), out userCount);                 
                    if (userCount < 1)
                    {
                        Response.Redirect("~/Module/StartUp/CreateGroup.aspx", false);
                        return;
                    }
                }
                /*************** CREATED BY NISHANT RAI FOR STARTUP ********************************/



                if (Cache["dtUserMenu"] != null)
                    Cache.Remove("dtUserMenu");

                txtLoginName.Focus();
                Session.Abandon();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Login page.\r\n See error log for detail."));
        }
    }

    protected void imgButtonLogin_Click(object sender, ImageClickEventArgs e)
    {
        if (checkMACCode())
        {
            if (Page.IsValid)
            {
                try
                {
                    if (txtLoginName.Text.Trim() == "sai" && txtPassword.Text.Trim() == "sai")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('Attendance/index.aspx','','fullscreen=yes,status=yes,location=no,scrollbars=no,titlebar=no,addressbar=no');", true);
                        //Response.Redirect("~/Attendance/index.aspx", false);
                    }
                    else
                    {
                        #region Session for employees
                        HR_EMP_MST.USER_NAME = txtLoginName.Text.Trim().ToString();
                        HR_EMP_MST.PWD = txtPassword.Text.Trim().ToString();
                        DataTable DTable = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Login_Info(HR_EMP_MST);
                        if (DTable.Rows.Count > 0)
                        {

                            Session["EmpCode"] = DTable.Rows[0]["EMP_CODE"].ToString().Trim();
                            Session["empType"] = DTable.Rows[0]["EMP_TYPE"].ToString().Trim();
                            Session["usrName"] = DTable.Rows[0]["USER_NAME"].ToString().Trim();
                            Session["DESIG_CODE"] = DTable.Rows[0]["DESIG_CODE"].ToString().Trim();
                            if (DTable.Rows[0]["POSITION"].ToString() != "" && DTable.Rows[0]["POSITION"].ToString() != null)
                            {
                                Session["POSITION"] = DTable.Rows[0]["POSITION"].ToString().Trim();
                            }
                            else
                            {
                                Session["POSITION"] = "0";
                            }

                            if (DTable.Rows[0]["ReportTo"].ToString() != "" && DTable.Rows[0]["ReportTo"].ToString() != null)
                            {
                                Session["ReportTo"] = DTable.Rows[0]["ReportTo"].ToString().Trim();
                            }
                            else
                            {
                                Session["ReportTo"] = "1";
                            }
                        }
                        #endregion

                        DataTable dtLogin = SaitexBL.Interface.Method.CM_USER_MST.ValidateUser(txtLoginName.Text.Trim(), txtPassword.Text.Trim());

                        if (dtLogin != null && dtLogin.Rows.Count > 0)
                        {
                            Session["urLoginId"] = dtLogin.Rows[0]["USER_CODE"].ToString().Trim();
                            Session["urType"] = dtLogin.Rows[0]["USER_TYPE"].ToString().Trim();
                            Session["usrNames"] = dtLogin.Rows[0]["USER_NAME"].ToString().Trim();
                            Response.Redirect("~/GetUserAuthorisation.aspx", false);
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Invalid Login!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in validating Login Id/ Password.\r\n See error log for detail."));
                }
            }
        }
        else 
        {
            Common.CommonFuction.ShowMessage("Not Defined System!");
            Response.Redirect("~/Default.aspx", false);
        }
    }

    public bool checkMACCode()
    {
        /*************** CREATED BY NISHANT RAI FOR MAC Code ********************************/

        bool result = true;
        var newTitle = GetMACAddress();
        var oldTitle = readXML();
        if (!string.IsNullOrEmpty(oldTitle))
        {
            if (!oldTitle.Equals(newTitle))
            {           
                result = false;
            }            
        }
        else
        {

            createXML(newTitle);
        }
        return result;

        /*************** CREATED BY NISHANT RAI FOR MAC Code ********************************/
    }

    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        } return sMacAddress;
    }

    public void createXML(string m)
    {
        var dir = Server.MapPath("~/Module/Admin/Help1/books.xml");
        using (XmlWriter writer = XmlWriter.Create(dir))
        {
            writer.WriteStartElement("book");
            writer.WriteElementString("title", m);
            writer.WriteEndElement();
            writer.Flush();
        }
    }

    public string readXML()
    {
        string title = string.Empty;
        var dt = new DataSet();
        //dt.ReadXml(MapPath("Module/Admin/Help1/books.xml"));
         var dir = Server.MapPath("~/Module/Admin/Help1/books.xml");
        //var dir = @"Module/Admin/Help1/books.xml";
        if (File.Exists(dir))
        {
            dt.ReadXml(dir);
            if (dt.Tables[0].Rows.Count > 0 && dt != null)
            {
                title = dt.Tables[0].Rows[0]["title"].ToString();
            }
        }
        else
        {
            var newTitle = GetMACAddress();
            createXML(newTitle);
        }
        return title;
    }

}

