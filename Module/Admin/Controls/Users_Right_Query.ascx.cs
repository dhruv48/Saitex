using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using errorLog;

public partial class Module_Admin_Controls_Users_Right_Query : System.Web.UI.UserControl
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
                BindControls();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindControls()
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

    protected void BindUserName()
    {
        try
        {
            ddlUserName.Items.Clear();
            DataTable dtUserName = SaitexBL.Interface.Method.CM_USER_MST.GetUserData();
            ddlUserName.Items.Clear();
            ddlUserName.DataSource = dtUserName;
            ddlUserName.DataTextField = "USER_NAME";
            ddlUserName.DataValueField = "USER_CODE";
          
            ddlUserName.DataBind();
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GetsUserData()
    {
      try
        {
            
          string str = ddlUserName.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_USER_MST.getdata();
            DataView dv = new DataView(dt);
            dv.RowFilter = "USER_CODE='" + str + "'";
            grUser_Right_Query.DataSource = dv;
            grUser_Right_Query.DataBind();
            grUser_Right_Query.Visible = true;
      }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GetsUserData1()
    {
        try
        {

            string str = ddlUserName.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_USER_MST.getdata1();
            DataView dv = new DataView(dt);
            dv.RowFilter = "USER_CODE='" + str + "'";
            GridView1.DataSource = dv;
            GridView1.DataBind();
          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

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
    protected void InitialControls()
    {
        try
        {
            BindUserName();
            grUser_Right_Query.Visible = false;
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


    protected void btngetrecord_Click1(object sender, EventArgs e)
    {
        try
        {
            GetsUserData();
            GetsUserData1();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grUser_Right_Query_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

