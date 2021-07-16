using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using errorLog;

public partial class Module_Admin_Controls_Rate_Component : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
        if(!IsPostBack)
        {
        InitialControls();
        ddlCompoType.Items.Insert(0, new ListItem("-----------ALL---------", string.Empty));
        }
        }
            catch(Exception ex)
        {
           throw ex; 
            }

        }
    

    protected void GetRateComponent()
    {
        string COMPO_TYPE = string.Empty;
        
        
        try
        {
            if (ddlCompoType.SelectedValue.ToString() != null && ddlCompoType.SelectedValue.ToString() != string.Empty)
            {
                COMPO_TYPE = ddlCompoType.SelectedValue.ToString();
            }
            else
            {
                COMPO_TYPE = string.Empty;
            }
            
            
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_RATE_COMPONENT.GetRateComponent(COMPO_TYPE);
            if(dt.Rows.Count>0)
            {
                grRateComponentQuery.DataSource = dt;
                grRateComponentQuery.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grRateComponentQuery.Visible = true;
            }
            else
            {
                grRateComponentQuery.DataSource = null;
                grRateComponentQuery.DataBind();
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
    protected void  InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
            
            ddlCompoType.SelectedIndex = -1;
           // ddlUserType.SelectedIndex = -1;
           

            grRateComponentQuery.Visible = false;
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
            GetRateComponent();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void grRateComponentQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetRateComponent();

            grRateComponentQuery.PageIndex = e.NewPageIndex;
            grRateComponentQuery.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
}
