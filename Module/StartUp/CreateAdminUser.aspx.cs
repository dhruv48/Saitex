using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SaitexBL.Interface.Method;
using System.Data.OracleClient;
using errorLog;
using Common;
using DBLibrary;

public partial class Module_StartUp_CreateAdminUser : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMode.Text = "Save";       
        if (!IsPostBack)
        {
            Grid1.AutoPostBackOnSelect = false;
            radUserType.SelectedIndex = -1;
            chk_Status.Checked = true;
            bindGvUser();
            tdUpdate.Visible = false;
            tdComboBoxID.Visible = false;
            ValidationSummary2.Visible = true;  
        }
    }
    protected void SaveUserMaster()
    {

        try
        {            
           
            var oCM_USER_MST = new SaitexDM.Common.DataModel.CM_USER_MST();
            oCM_USER_MST.USER_CODE = CommonFuction.funFixQuotes(txtUserCode.Text.Trim());
            oCM_USER_MST.USER_LOG_ID = CommonFuction.funFixQuotes(txtLoginId.Text.Trim());
            oCM_USER_MST.USER_NAME = CommonFuction.funFixQuotes(txtUserName.Text.Trim());            
            oCM_USER_MST.USER_PASS = CommonFuction.funFixQuotes(txtPassword.Text.ToString().Trim());
            oCM_USER_MST.USER_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.ToString().Trim());
            oCM_USER_MST.USER_TYPE = CommonFuction.funFixQuotes(radUserType.SelectedValue.Trim());
            oCM_USER_MST.TUSER = "SUPER ADMIN";
            oCM_USER_MST.STATUS = true;
            oCM_USER_MST.DEL_STATUS = false;
            int iRecordFound = 0;
            bool Result = SaitexBL.Interface.Method.CM_USER_MST.Insert(oCM_USER_MST, out iRecordFound);
            if (Result)
            {
                BlanksControls();              
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('User Master saved successfully');", true);
                Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                Response.Redirect("CreateUserAuth.aspx", false);
            

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('User Master Duplicate Entry');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + ex.Message + "');", true);
        }

    }      
    private void bindGvUser()
    {       
        try
        {
            var dt = SaitexBL.Interface.Method.CM_USER_MST.PrintReport();         
            Grid1.DataSource = dt;
            Grid1.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void getGvUserMaster(int User_Id)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.CM_USER_MST.GetUserMasterByUserId(User_Id);

            if (dt != null && dt.Rows.Count > 0)
            {
                radUserType.SelectedValue = dt.Rows[0]["USER_TYPE"].ToString();
                txtUserCode.Text = dt.Rows[0]["USER_CODE"].ToString();              
                txtUserName.Text = dt.Rows[0]["USER_NAME"].ToString();               
                txtPassword.Text = dt.Rows[0]["USER_PASS"].ToString();              
                txtRemarks.Text = dt.Rows[0]["USER_REMARKS"].ToString();
            }
        }

        catch (OracleException ex)
        {
            throw ex;

        }

        catch (Exception ex)
        {
            throw ex;

        }



        finally
        {
            //if (obj != null)
            //{
            //    obj = null;
            //}

        }
    }   
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {

            SaveUserMaster();
            bindGvUser();

        }
    }
    private void BlanksControls()
    {

        try
        {
          
            txtUserCode.Text = "";
            txtUserName.Text = "";
            txtLoginId.Text = "";
            txtPassword.Text = "";
            txtRemarks.Text = "";
            chk_Status.Text = "";
            txtPassword.Attributes.Clear();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }
    }    
 }
