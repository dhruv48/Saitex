using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using errorLog;

public partial class Module_Yarn_SalesWork_Queries_IssueY : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    String yr = string.Empty;
    String branch = string.Empty;
    String yarncode = string.Empty;
    String Transtype = string.Empty;
    string sdate = string.Empty;
    string edate = string.Empty;
    string shade = string.Empty;
    string shade_family = string.Empty;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }
    private void Load_Item_Receipt_Data()
    {
        sdate = Request.QueryString["SDATE"];
        edate = Request.QueryString["EDATE"];
        string Comp_Code = string.Empty;

        Comp_Code = " and m.Comp_Code='" + oUserLoginDetail.COMP_CODE + "'";

        if (Request.QueryString["BCODE"] != "")
        {
            branch = " and m.branch_code='" + Request.QueryString["BCODE"] + "'";
        }
        else
        {
            branch = "";
        }
        if (Request.QueryString["YCODE"] != "")
        {
            yarncode = " and mt.yarn_code='" + Request.QueryString["YCODE"] + "'";
        }
        else
        {
            yarncode = "";
        }

        if (Request.QueryString["YEAR"] != "")
        {
            yr = "AND M.YEAR ='" + Request.QueryString["YEAR"] + "'";
        }
        else
        {
            yr = "";
        }
        if (Request.QueryString["SHADE"] != "")
        {
            shade = " and MT.SHADE_CODE='" + Request.QueryString["SHADE"] + "'";
        }
        else
        {
            shade = "";
        }

        if (Request.QueryString["SHADE_FAMILY"] != "")
        {
            shade_family = "AND MT.SHADE_FAMILY='" + Request.QueryString["SHADE_FAMILY"] + "'";
        }
        else
        {
            shade_family = "";
        }
        Transtype = "  AND SUBSTR(M.TRN_TYPE,1,1) ='" + Request.QueryString["TRANS_TYPE"] + "'";

        try
        {
            DataTable Dtable = SaitexBL.Interface.Method.YRN_IR_MST.Load_Item_Receipt_Data(branch, yarncode, yr, Transtype, sdate, edate, Comp_Code,shade_family,shade,"");
            gvIssueDetails.DataSource = Dtable;
            gvIssueDetails.DataBind();
            if (Dtable.Rows.Count > 0)
            {
                lblItemDesc.Text = Dtable.Rows[0]["YARN_DESC"].ToString();
                lblBranch.Text = Dtable.Rows[0]["BRANCH_NAME"].ToString();
                lblYear.Text = Request.QueryString["YEAR"].ToString();
            }
        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }
    protected void gvIssueDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvIssueDetails.PageIndex = e.NewPageIndex;
            Load_Item_Receipt_Data();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
}

