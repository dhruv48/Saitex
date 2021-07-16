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


public partial class Module_Fabric_FabricSaleWork_Pages_FabricPendingIndentsDetail : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string yr = string.Empty;
    string branch = string.Empty;
    string dept = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["BCODE"] != "")
            {
                branch = " and c.branch_code='" + Request.QueryString["BCODE"] + "'";
            }
            else
            {
                branch = "";
            }
            if (Request.QueryString["DCODE"] != "")
            {
                dept = "and c.dept_code='" + Request.QueryString["DCODE"] + "'";
            }
            else
            {
                dept = "";
            }

            if (Request.QueryString["YEAR"] != "")
            {
                yr = "and c.year='" + Request.QueryString["YEAR"] + "'";
            }
            else
            {
                yr = "";
            }

            Load_Pend_Indent_Details();
        }
    }
    private void Load_Pend_Indent_Details()
    {
        try
        {
            DataTable Dtable = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.Load_Pend_Indent_Details(branch, dept);
            gvPendIndDetails.DataSource = Dtable;
            gvPendIndDetails.DataBind();
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
         protected void gvPendIndDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPendIndDetails.PageIndex = e.NewPageIndex;
        Load_Pend_Indent_Details();
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
