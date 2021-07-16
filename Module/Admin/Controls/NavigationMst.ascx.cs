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
using System.IO;
using DBLibrary;
using obout_ASPTreeView_2_NET;

public partial class Module_Admin_Controls_NavigationMst : System.Web.UI.UserControl
{
       SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {

                initialbind(); 
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
        }
    }

    private void initialbind()
    {
        bindAddModuleMaster();
    }
    private void bindAddModuleMaster()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_MODULE_MST.GetModuleMaster();
            ddlParenModuleName.DataValueField = "MDL_ID";
            ddlParenModuleName.DataTextField = "MDL_NAME";
            ddlParenModuleName.DataSource = dt;
            ddlParenModuleName.DataBind();
            ddlParenModuleName.Items.Insert(0, new ListItem("---------ALL----------", ""));
             
        }
        catch
        {
            throw;
        }
    }

    private void bindChildModuleMaster(int strModuleCategoryId, string strSelect)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.GetChildModuleMasterfromModule(strModuleCategoryId, strSelect);
            ddlChildModuleName.DataValueField = "CHILD_MDL_ID";
            ddlChildModuleName.DataTextField = "CHILD_MDL_NAME";
            ddlChildModuleName.DataSource = dt;
            ddlChildModuleName.DataBind();
            ddlChildModuleName.Items.Insert(0, new ListItem("---------ALL----------", ""));
            if (strSelect != string.Empty)
            {
                ddlChildModuleName.SelectedValue = strSelect;
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindGvAddModuleNavigation()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavigationMst();
            if(dt.Rows.Count>0)
            {
            //lblTotalRecord.Text = dt.Rows.Count.ToString();
            gvNavigation.DataSource = dt;
            gvNavigation.DataBind();
            gvNavigation.Visible=true;
            }
            else{
                 gvNavigation.DataSource = null;
                gvNavigation.DataBind();
                
                Common.CommonFuction.ShowMessage("Record Not Available For Selected Parameter");

            }

        }
        catch
        {
            throw;
        }

    }

    private void bindGvAddModuleNav(int MDLID,int CHILDMDLID)
    {
       
        try
        {
           
            DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavMaster(MDLID,CHILDMDLID);
            //lblTotalRecord.Text = dt.Rows.Count.ToString();
            if (dt.Rows.Count > 0)
            {
                gvNavigation.DataSource = dt;
                gvNavigation.DataBind();
                gvNavigation.Visible = true;
            }
            else
            {
                gvNavigation.DataSource = null;
                gvNavigation.DataBind();

                Common.CommonFuction.ShowMessage("Record Not Available For Selected Parameter");

            }
        }
        catch
        {
            throw;
        }
    }

    private void bindGvAddNavigation()
    {
        try
        {
            GetData();
            DataTable dt = (DataTable)ViewState["Navigation"];
            gvNavigation.DataSource = dt;
            gvNavigation.DataBind();
            //lblTotalRecord.Text = dt.Rows.Count.ToString();
        }
        catch
        {
            throw;
        }
    }

    private void GetData()
    {
        try
        {
            DataTable dt = new DataTable();

            if ((ddlParenModuleName.SelectedIndex > 0) && (ddlChildModuleName.SelectedIndex > 0))
            {
                int mdlid = int.Parse(ddlParenModuleName.SelectedValue.Trim());
                int childmdlid = int.Parse(ddlChildModuleName.SelectedValue.Trim());
                dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavMaster(mdlid, childmdlid);

                ViewState["Navigation"] = dt;
            }
            else if (ddlParenModuleName.SelectedIndex > 0)
            {
                int mdlid = int.Parse(ddlParenModuleName.SelectedValue.Trim());
                dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavModule(mdlid);
                ViewState["Navigation"] = dt;
            }
            else
            {
                dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavigationMst();
                ViewState["Navigation"] = dt;
            }
        }
        catch
        {
            throw;
        }

    }

    private void bindGvAddModule(int mdlid)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavModule(mdlid);
            if (dt.Rows.Count > 0)
            {

                //lblTotalRecord.Text = dt.Rows.Count.ToString();
                gvNavigation.DataSource = dt;
                gvNavigation.DataBind();
                gvNavigation.Visible = true;
            }
            else
            {
                gvNavigation.DataSource = null;
                gvNavigation.DataBind();

                Common.CommonFuction.ShowMessage("Record Not Available For Selected Parameter");

            }

        }
        catch
        {
            throw;
        }
    }

    protected void gvNavigation_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvNavigation.PageIndex = e.NewPageIndex;
            GetData();
            bindGvAddNavigation();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gridview Paging.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }



   //protected void ddlChildModuleName_SelectedIndexChanged(object sender, EventArgs e)
   // {
   //     try
   //     {
   //         int mdlid = Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim());
   //         int childmdlid = Convert.ToInt32(ddlChildModuleName.SelectedValue.Trim());

   //        //utoCompleteExtender1.ContextKey = mdlid.ToString() + "@" + childmdlid.ToString();

   //         this.bindGvAddModuleNav(mdlid, childmdlid);
   //     }
   //     catch (Exception ex)
   //     {
   //         CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Module Dropdown Selection.\r\nSee error log for detail."));
   //         lblMode.Text = ex.ToString();
   //     }
   // }

   

  protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //BlanksControls();
            //tdDelete.Visible = false;
            //tdSave.Visible = true;
            //tdUpdate.Visible = false;
            //lblMode.Text = "You are in Save Mode";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }



               
   
    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        try
        {
          btnGetReported();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void btnGetReported()
    {
        if (ddlParenModuleName.SelectedValue.ToString() != null && ddlParenModuleName.SelectedValue.ToString() != string.Empty)
        {

            if (ddlChildModuleName.SelectedValue.ToString() != null && ddlChildModuleName.SelectedValue.ToString() != string.Empty)
            {

                int mdlid = Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim());
                int childmdlid = Convert.ToInt32(ddlChildModuleName.SelectedValue.Trim());
                this.bindGvAddModuleNav(mdlid, childmdlid);




            }
            else
            {
                //bindChildModuleMaster(Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim()), "");
                int mdlid = Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim());
                bindGvAddModule(mdlid);
            }
        }

        else
        {
            bindGvAddModuleNavigation();
        }
    }

    protected void ddlParenModuleName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlParenModuleName.SelectedValue.Trim() != string.Empty)
            {
                bindChildModuleMaster(Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim()), "");
                int mdlid = Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim());
                //bindGvAddModule(mdlid);
            }
            else
            {
                ddlChildModuleName.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Parent Module Dropdown.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }
}
