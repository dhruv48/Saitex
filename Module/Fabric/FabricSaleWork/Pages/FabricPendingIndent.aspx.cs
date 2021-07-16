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
using errorLog;

public partial class Module_Fabric_FabricSaleWork_Pages_FabricPendingIndent : System.Web.UI.Page
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
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            getBranchName();
            getDepartment();
            fillYear();
            Load_Pend_Indents();
        }

    }
    protected void gvDeptPendIndents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDeptPendIndents.PageIndex = e.NewPageIndex;
        Load_Pend_Indents();
    }
    public void getBranchName()
    {
        try
        {
            DataTable dv = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dv = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            ddlBranch.DataSource = dv;
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }

    }
    public void getDepartment()
    {
        try
        {
            DataTable dv = new DataTable();
            dv = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            ddlDepartment.DataSource = dv;
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("--------------ALL-------------",""));
            dv.Dispose();
            dv = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    public void fillYear()
    {

        for (int i = -10; i < 10; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
    }
    public void Load_Pend_Indents()
    {
        if (ddlDepartment.SelectedValue.Trim()!= "")
        {
            department = " and c.dept_code='" + ddlDepartment.SelectedValue.Trim() + "'";
        }
        if (ddlBranch.SelectedValue.Trim() != "")
        {
            branch = " and c.branch_code='" + ddlBranch.SelectedValue.Trim() + "'";
        }
        try
        {
            DataTable Dtable = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.Load_Pend_Indents(branch, department);
            gvDeptPendIndents.DataSource = Dtable;
            gvDeptPendIndents.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
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
        
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlBranch.SelectedValue=oUserLoginDetail.CH_BRANCHCODE;
            ddlDepartment.SelectedIndex =0;
            ddlYear.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
