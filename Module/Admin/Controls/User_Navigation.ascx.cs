using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using errorLog;

public partial class Module_Admin_Controls_User_Navigation : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
               // BindControls();
                LoadData();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    
    }

    protected void InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;

            ddlUser.SelectedIndex = -1;
            // ddlUserType.SelectedIndex = -1;
            grd_UserNavigation.Visible = false;
            lblTotalRecord.Text = "0";
            ddlUser.Items.Insert(0, new ListItem("SELECT", ""));

        }
        catch (Exception ex)
        {
            throw ex;
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
            throw ex;
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    { }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            InitialControls();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        try
        {
            GetUserNavigation();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

        private void LoadData()
    {
        try
        {
            DataTable dtItem = GetUser("%");

            ddlUser.Items.Clear();
            ddlUser.Items.Add(new ListItem("Select", "SELECT"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlUser.DataSource = dtItem;
                ddlUser.DataTextField = "DISP_DATA";
                ddlUser.DataValueField = "USER_CODE";
                ddlUser.DataBind();
            }
        }
        catch
        {
            throw;
        }

    }

    protected DataTable GetUser(string text)
    {
        try
        {
            string whereClause = " WHERE USER_CODE like :SearchQuery or USER_NAME like :SearchQuery ";
            string sortExpression = " ORDER BY USER_CODE asc,USER_NAME asc  ";

            string commandText = "SELECT USER_CODE,USER_NAME,(USER_CODE||'-----'||USER_NAME) DISP_DATA FROM CM_USER_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void GetUserNavigation()
    {
        string USER_CODE = string.Empty;
        try
        {
            if (ddlUser.SelectedValue.ToString() != null && ddlUser.SelectedValue.ToString() != "")
            {
                USER_CODE = ddlUser.SelectedValue.ToString();
            }
           
            else
            {
                USER_CODE = string.Empty;
            }
             if (ddlUser.SelectedValue.ToString() == "SELECT")
            {
                USER_CODE = string.Empty;
            }
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.UserNavigationRight.GetUserNavigation(USER_CODE);
            if (dt.Rows.Count > 0)
            {
                grd_UserNavigation.DataSource = dt;
                grd_UserNavigation.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grd_UserNavigation.Visible = true;
            }
            else
            {
                grd_UserNavigation.DataSource = null;
                grd_UserNavigation.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   


    protected void grd_UserNavigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetUserNavigation();

            grd_UserNavigation.PageIndex = e.NewPageIndex;
            grd_UserNavigation.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grd_UserNavigation.Visible = false;
            //BindYear();
           // InitialControls();
            //TxtFromDate.Text = string.Empty;
           // TxtToDate.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    
}
   


