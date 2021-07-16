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

public partial class Module_Inventory_Queries_ReceiptQ : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    String yr = string.Empty;
    String branch = string.Empty;
    String itemcode = string.Empty;
    String Transtype = string.Empty;
    string sdate = string.Empty;
    string edate = string.Empty;
    string store = string.Empty;
    string location = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!Page.IsPostBack)
            {
                Load_Item_Receipt_Data();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
        }
    }

    private void Load_Item_Receipt_Data()
    {
        sdate = Request.QueryString["SDATE"];
        edate = Request.QueryString["EDATE"];
        if (Request.QueryString["BCODE"] != "")
        {
            branch = " and m.branch_code='" + Request.QueryString["BCODE"] + "'";
        }
        else
        {
            branch = "";
        }
        if (Request.QueryString["ICODE"] != "")
        {
            itemcode = " and mt.item_code='" + Request.QueryString["ICODE"] + "'";
        }
        else
        {
            itemcode = "";
        }

        if (Request.QueryString["YEAR"] != "")
        {
            yr = "AND M.YEAR='" + Request.QueryString["YEAR"] + "'";
        }
        else
        {
            yr = "";
        }
        if (Request.QueryString["LOCATION"] != "")
        {
            location = "AND M.LOCATION='" + Request.QueryString["LOCATION"].ToString() + "'";
        }
        else
        {
            location = "";
        }
        if (Request.QueryString["STORE"] != "")
        {
            store = "AND M.STORE='" + Request.QueryString["STORE"].ToString() + "'";
        }
        else
        {
            store = "";
        }
        Transtype = "  AND substr(M.TRN_TYPE,1,1) ='" + Request.QueryString["TRANS_TYPE"] + "'";

        try
        {
            DataTable Dtable = SaitexBL.Interface.Method.TX_ITEM_STOCK_DATA.Load_Item_Receipt_Data(branch, itemcode, yr, Transtype, sdate, edate, location, store);
            gvReceiptDetails.DataSource = Dtable;
            gvReceiptDetails.DataBind();
            lblItemDesc.Text = Dtable.Rows[0]["ITEM_DESC"].ToString();
            lblBranch.Text = Dtable.Rows[0]["BRANCH_NAME"].ToString();
            lblYear.Text = Request.QueryString["YEAR"].ToString();
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }

    protected void gvReceiptDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReceiptDetails.PageIndex = e.NewPageIndex;
        Load_Item_Receipt_Data();
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
}
