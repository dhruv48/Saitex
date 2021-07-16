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
using System.Data.OracleClient;
using errorLog;
using Common;

public partial class Module_Inventory_Pages_Pending_Indents : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string yr = string.Empty;
    string branch = string.Empty;
    string department = string.Empty;
    string url = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!Page.IsPostBack)
        {
            getBranchName();
            getDepartment();
            fillYear();
            Load_Pend_Indents();
        }

    }
    private void Load_Pend_Indents()
    {
        if (ddlYear.SelectedValue.Trim() != "")
        {
            yr = " and a.year='" + ddlYear.SelectedValue.Trim() + "'";
        }

        if (ddlBranch.SelectedValue.Trim() != "")
        {
            branch = " and c.branch_code='" + ddlBranch.SelectedValue.Trim() + "'";
        }
        if (ddlDepartment.SelectedValue.Trim() != "")
        {
            department = " and c.dept_code='" + ddlDepartment.SelectedValue.Trim() + "'";
        }
        try
        {
            DataTable Dtable = SaitexBL.Interface.Method.TX_ITEM_IND_MST.Load_Pend_Indents(yr, branch, department);
            gvDeptPendIndents.DataSource = Dtable;
            gvDeptPendIndents.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    protected void gvDeptPendIndents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDeptPendIndents.PageIndex = e.NewPageIndex;
        Load_Pend_Indents();
    }
    private void getBranchName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            //ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getDepartment()
    {
        try
        {
            ////////////////////////// Bind Department Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fill Department.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void fillYear()
    {
        for (int i = -15; i < 15; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
        //ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        url = "../Reports/Pending_Indents.aspx?TRANS_YEAR=" + ddlYear.SelectedValue.Trim() + "&&BRANCH_CODE=" + ddlBranch.SelectedValue.Trim() + "&&DEPTT_CODE=" + ddlDepartment.SelectedValue.Trim(); ;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
}
