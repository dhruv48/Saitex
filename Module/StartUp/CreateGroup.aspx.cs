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
using System.Data.OracleClient;
using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_StartUp_CreateGroup : System.Web.UI.Page
{  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 
            lblMode.Text = "Save";
            if (!IsPostBack)
            {
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                txtGroupName.Visible = true;
                BlanksControls();
                chk_Status.Checked = true;
                bindGvGroupMaster();              
            }            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }
    private void BlanksControls()
    {
        try
        { 
            txtGroupCode.Visible = true;
            txtGroupCode.Text = "";
            txtGroupName.Text = "";
            txtRemarks.Text = "";
            chk_Status.Checked = false;
        }
        catch
        {
            throw;
        }
    }  
    private void bindGvGroupMaster()
    {
        try
        {
            var dt = SaitexBL.Interface.Method.CM_GroupMaster.Select();
            ViewState["dtGroup"] = dt;
            gvGrpMaster.DataSource = dt;
            gvGrpMaster.DataBind();

        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                SaveGroup();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving page.\r\nSee error log for detail."));

        }
    }
    protected void SaveGroup()
    {
        try
        {

            int iRecordFound = 0;
            var Result = SaitexBL.Interface.Method.CM_GroupMaster.Insert(txtGroupName.Text.ToUpper().Trim(), txtGroupCode.Text.ToUpper().Trim(), txtRemarks.Text, chk_Status.Checked, "SUPER ADMIN", out iRecordFound);

            if (Result)
            {
                BlanksControls();
                bindGvGroupMaster();
              ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('Saved Successfully. ');", true);
              Response.Redirect("CreateCompany.aspx",false);    
                         
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('data already exists ');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fun", "window.confirm('Saving Failed');", true);
            }

        }
        catch
        {
            throw;
        }
    }      
  
   
}
