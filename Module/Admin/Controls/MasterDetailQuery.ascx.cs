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

public partial class Module_Admin_Controls_MasterDetailQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string MST_NAME = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
                BindControls();
                //BindMST_CODE();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
            DDLMasterName.SelectedIndex = -1;
            lblTotalRecord.Text = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void BindControls()
    {
        try
        {
            DDLMasterName.Items.Clear();
            DataTable dtMasterName = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMAST_NAME();
            if (dtMasterName != null && dtMasterName.Rows.Count > 0)
            {
                DDLMasterName.DataTextField = "MST_NAME";
                DDLMasterName.DataValueField = "MST_NAME";
                DDLMasterName.DataSource = dtMasterName;
                DDLMasterName.DataBind();
            }
            DDLMasterName.Items.Insert(0, new ListItem("SELECT", ""));
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetMasterData()
    {

        try
        {
            if (DDLMasterName.SelectedValue.ToString() != null && DDLMasterName.SelectedValue.ToString() != string.Empty)
            {
                MST_NAME = DDLMasterName.SelectedValue.ToString();
            }
            else
            {
                MST_NAME = string.Empty;
            }


            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetMasterData(MST_NAME);

            if (dt.Rows.Count > 0)
            {
                MasterDetailQuery.DataSource = dt;
                // MasterDetailQuery.DataSource = dt;
                MasterDetailQuery.DataBind();

                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                MasterDetailQuery.Visible = true;
            }
            else
            {
                 MasterDetailQuery.DataSource = null;
                 MasterDetailQuery.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void MasterDetailQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetMasterData();

            MasterDetailQuery.PageIndex = e.NewPageIndex;
            MasterDetailQuery.DataBind();
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


    protected void btngetrecord_Click1(object sender, EventArgs e)
    {
        try
        {
            GetMasterData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}



