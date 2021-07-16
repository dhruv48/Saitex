using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using errorLog;

public partial class Module_Admin_Controls_UserMasterQuery1 : System.Web.UI.UserControl
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
            BindUserType();
            //BindUserName();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindUserType()
    {
        try
        {
            ddlUserType.Items.Clear();
            DataTable dtUserType = SaitexBL.Interface.Method.CM_USER_MST.SelectByUSR_TYPE();
            ddlUserType.Items.Clear();
            ddlUserType.DataSource = dtUserType;
            ddlUserType.DataTextField = "USER_TYPE";
            ddlUserType.DataValueField = "USER_TYPE";
            ddlUserType.DataBind();
            ddlUserType.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));

            dtUserType.Dispose();
            dtUserType = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void BindUserName()
    //{
    //    try 
    //    {
    //        string usertype = ddlUserType.SelectedValue.ToString();
    //        //ddlUserName.Items.Clear();
    //        DataTable dtUserName = SaitexBL.Interface.Method.CM_USER_MST.SelectByUSR_NAME(usertype);
    //        //if (dtUserName.Rows.Count > 0)
    //        //{
    //        //DataView Dv = new DataView(dtUserName);
    //        //Dv.RowFilter = usertype;
    //        //ddlUserName.DataSource = Dv;

    //            //ddlUserName.Items.Clear();
    //            ddlUserName.DataSource = dtUserName;
    //            ddlUserName.DataTextField = "USER_NAME";
    //            ddlUserName.DataValueField = "USER_NAME";
    //            ddlUserName.DataBind();

    //            ddlUserName.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));
    //      //  }
    //       // ddlUserName.SelectedIndex = ddlUserName.Items.IndexOf(ddlUserType.Items.FindByValue("USER_TYPE"));
    //    }
    //    catch (Exception ex)
    //    {
    //    throw ex;
    //    }
    //}
    protected void GetUserData()
    {
        string USER_TYPE = string.Empty;
        string USER_NAME = string.Empty;
        
        try
        {
            if (ddlUserType.SelectedValue.ToString() != null && ddlUserType.SelectedValue.ToString() != string.Empty)
            {
                USER_TYPE = ddlUserType.SelectedValue.ToString();
            }
            else
            {
                USER_TYPE = string.Empty;
            }
            //if (ddlUserName.SelectedValue.ToString() != null && ddlUserName.SelectedValue.ToString() != string.Empty)
            //{
            //    USER_NAME = ddlUserName.SelectedValue.ToString();
            //}
            //else
            //{
            //    USER_NAME = string.Empty;
            //}
            if (TxtUserName.Text.ToString() != null && TxtUserName.Text.ToString() != string.Empty)
            {
                USER_NAME = "%"+TxtUserName.Text.ToString()+"%" ;
            }
            else
            {
                USER_NAME = string.Empty;
            }
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_USER_MST.GetUserData(USER_TYPE, USER_NAME);
            if(dt.Rows.Count>0)
            {
                grUserMasterQuery.DataSource = dt;
                grUserMasterQuery.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grUserMasterQuery.Visible = true;
            }
            else
            {
                //grUserMasterQuery.DataSource = null;
                //grUserMasterQuery.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }
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
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
            
            ddlUserType.SelectedIndex = -1;
           // ddlUserType.SelectedIndex = -1;
            grUserMasterQuery.Visible = false;
            lblTotalRecord.Text = "0";

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
            GetUserData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void grUserMasterQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetUserData();

            grUserMasterQuery.PageIndex = e.NewPageIndex;
            grUserMasterQuery.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
}
