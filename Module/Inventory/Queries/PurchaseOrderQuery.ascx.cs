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
using Common;
using errorLog;
using System.IO;

public partial class Module_Inventory_Queries_WebUserControl : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            purchaseOrderGrid.Visible = true;
            PoDataBind();
            InitialControls();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        try
        {
            ddlprtycode.AutoPostBack = false;
            base.OnInit(e);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void PoDataBind()
    {

        string COMP_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string PRTY_CODE = string.Empty;
        string Fdate = string.Empty;
        string Tdate = string.Empty;
        try
        {
            
            if (ddlprtycode.SelectedItem.ToString() != null && ddlprtycode.SelectedItem.ToString() != "SELECT")
            {
                PRTY_CODE = ddlprtycode.SelectedItem.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }

            if (TxtTdate.Text.ToString() != null && TxtTdate.Text.ToString() != string.Empty)
            {
                Tdate = TxtTdate.Text.Trim().ToString();
            }
            else
            {
                Tdate = string.Empty;
            }

            if (TxtFdate.Text.ToString() != null && TxtFdate.Text.ToString() != string.Empty)
            {
                Fdate = TxtFdate.Text.Trim().ToString();
            }
            else
            {
                Fdate = string.Empty;
            }



            DataTable dt = SaitexBL.Interface.Method.Material_Purchase_Order.PoDataBind(COMP_CODE, BRANCH_CODE, PRTY_CODE, Tdate, Fdate);
            if (dt != null && dt.Rows.Count > 0)
            {
                purchaseOrderGrid.DataSource = dt;
                purchaseOrderGrid.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString();
            }
            else
            {
                purchaseOrderGrid.DataSource = null;
                purchaseOrderGrid.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                purchaseOrderGrid.Visible = true;
                lblTotalRecord.Text = "0";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void InitialControls()
    {
        try
        {
           
            purchaseOrderGrid.Visible = true;
            imgbtnClear.Visible = true;
            imgbtnPrint.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
            
            TxtFdate.Text = string.Empty;
            TxtTdate.Text = string.Empty;
            ddlprtycode.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void btnview_Click(object sender, EventArgs e)
    //{

    //}

    protected void btnview_Click1(object sender, EventArgs e)
    {
        try
        {
            PoDataBind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControls();
            PoDataBind();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));

        }
    }
}
