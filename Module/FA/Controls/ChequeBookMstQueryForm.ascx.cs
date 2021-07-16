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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_ChequeBookMstQueryForm : System.Web.UI.UserControl
{
    private static string code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lbPage1.Visible = false;
            if (Request.QueryString["id"] != null)
            {
                if (!IsPostBack)
                {
                    code = Request.QueryString["id"].ToString().ToUpper().Trim();
                    lbPage1.Visible = true;
                    BindGrid();
                }
            }
            else
            {
                BindGrid1();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Help/ChequeMasterQueryHelp.htm";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/FA/Reports/ChequeBookMst_Rpt.aspx", false);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
        }
    }

    private void BindGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeBookMst();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "LGR_BANK_CODE='" + code + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        grdChequeBook.DataSource = dv;
                        grdChequeBook.DataBind();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindGrid1()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeBookMst();
            grdChequeBook.DataSource = dt;
            grdChequeBook.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}