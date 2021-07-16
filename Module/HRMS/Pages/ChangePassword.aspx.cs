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
using Common;
using errorLog;

public partial class Module_HRMS_Pages_ChangePassword : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HEM = new SaitexDM.Common.DataModel.HR_EMP_MST();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMode.Text = "Update";
            TxtUserName.Text = Session["usrName"].ToString().Trim();
            HEM.USER_NAME = Session["usrName"].ToString().Trim();
            HEM.EMP_CODE = Session["EmpCode"].ToString().Trim();
        }

    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            if (Can_Update())
            {
                if (Update_Password())
                {
                    Common.CommonFuction.ShowMessage("Password Change Sucessfully");
                    Session.Abandon();
                    Response.Redirect("~/Module/HRMS/Pages/Default.aspx", false);
                    
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Unable to update!please try again");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("You are not valid user!Enter correct password");
                TxtOldPassword.Focus();
            }
        }

    }
    private bool Can_Update()
    {
        bool Result = false;
        try
        {            
            HEM.USER_NAME=TxtUserName.Text.Trim();
            HEM.PWD=TxtOldPassword.Text.Trim();
            HEM.EMP_CODE = Session["EmpCode"].ToString().Trim();
            bool Res = SaitexBL.Interface.Method.HR_EMP_MST.Can_UpdatePassword(HEM);
            if (Res)
            {
                Result = true;
            }
            else
            {
                Result = false;
            }
            return Result;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
            return false;
        }
    }
    private bool Update_Password()
    {
        bool Result = false;
        try
        {
            HEM.PWD = TxtNewPassword.Text.Trim();
            HEM.USER_NAME = TxtUserName.Text.Trim().ToString();
            HEM.EMP_CODE = Session["EmpCode"].ToString().Trim();
            bool Res = SaitexBL.Interface.Method.HR_EMP_MST.UpdatePassword(HEM);
            if (Res)
            {
                Result = true;
            }
            else
            {
                Result = false;
            }
            return Result;

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
            return false;
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
                Response.Redirect("/Saitex/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
}
