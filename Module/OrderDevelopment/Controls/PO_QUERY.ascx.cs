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


public partial class Module_OrderDevelopment_Controls_PO_QUERY : System.Web.UI.UserControl
{
    public static string PRTY_CODE = string.Empty;
    public static string Fdate;
    public static string Tdate;
    public static string PoFrom;
    public static string PoTo;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Getpodata_Grid();
                Initial_Controls();
            }


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page loading.\r\nplease see error log"));
        }

    }

    protected override void OnInit(EventArgs e)
    {
        ddlprtycode.AutoPostBack = false;
        base.OnInit(e);
    }
    private void Initial_Controls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
            imgbtnPrint.Visible = true;
            ddlprtycode.SelectedIndex = -1;
            TxtFdate.Text = "";
            TxtTdate.Text = "";
            TxtpoFrom.Text = "";
            TxtpoTo.Text = "";
        }
        catch (Exception ex)
        {
        }
    }

    private void Getpodata_Grid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetPodata_Grid();
            Podata_Grid.DataSource = dt;
            Podata_Grid.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void ViewInGrid()
    {
        try
        {
            if (ddlprtycode.SelectedValue.ToString() != null && ddlprtycode.SelectedValue.ToString() != "SELECT")
            {
                PRTY_CODE = ddlprtycode.SelectedValue.ToUpper().ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }

            if (TxtFdate.Text.ToString() != null && TxtFdate.Text.ToString() != string.Empty)
            {
                Fdate = TxtFdate.Text.Trim().ToString();
            }
            else
            {
                Fdate = string.Empty;
            }

            if (TxtTdate.Text.ToString() != null && TxtTdate.Text.ToString() != string.Empty)
            {
                Tdate = TxtTdate.Text.Trim().ToString();
            }
            else
            {
                Tdate = string.Empty;
            }
            if (TxtpoFrom.Text.ToString() != null && TxtpoFrom.Text.ToString() != string.Empty)
            {
                PoFrom = TxtpoFrom.Text.Trim().ToString();
            }
            else
            {
                PoFrom = string.Empty;
            }
            if (TxtpoTo.Text.ToString() != null && TxtpoTo.Text.ToString() != string.Empty)
            {
                PoTo = TxtpoTo.Text.Trim().ToString();
            }

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.ViewInGrid(PRTY_CODE, Fdate, Tdate, PoFrom, PoTo);
            if (dt != null && dt.Rows.Count > 0)
            {
                Podata_Grid.DataSource = dt;
                Podata_Grid.DataBind();
            }
            else
            {
                Podata_Grid.DataSource = null;
                Podata_Grid.DataBind();
                Common.CommonFuction.ShowMessage("Data Not By Selected Item.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            ViewInGrid();
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
            Initial_Controls();
            ViewInGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
}
