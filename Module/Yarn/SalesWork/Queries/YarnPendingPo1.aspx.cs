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
using Common;
public partial class Module_Yarn_SalesWork_Queries_YarnPendingPo : System.Web.UI.Page
{



    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string User_Code = string.Empty;
    string mth = string.Empty;
    string yr = string.Empty;
    string branch = string.Empty;
    string Status = string.Empty;
    string dept = string.Empty;
    string vendor = string.Empty;
    string url = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (!Page.IsPostBack)
        {
            fillYear();
            getBrachName();
            getVendorName();
            Load_PO_Detail();
        }
    }
    private void Load_PO_Detail()
    {

        yr = ddlYear.SelectedValue.Trim();
        if (ddlBranch.SelectedValue.Trim() != "")
        {
            branch = ddlBranch.SelectedValue.Trim();
        }
        else
        {
            branch = "";
        }
        if (ddlVendor.SelectedValue.Trim() != "")
        {
            vendor = ddlVendor.SelectedValue.Trim();
        }
        else
        {
            vendor = "";
        }
        if (ddlPoStatus.SelectedValue.Trim() != "")
        {
            Status = ddlPoStatus.SelectedValue.Trim();
        }
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_INT_MST.Load_Pend_PO_Details(branch, yr, vendor, Status);
            gvReportDisplayGrid.DataSource = dt;
            gvReportDisplayGrid.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReportDisplayGrid.PageIndex = e.NewPageIndex;
        Load_PO_Detail();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Load_PO_Detail();
    }

    private void fillYear()
    {
        for (int i = -15; i < 15; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        // ddlYear.Items.Insert(0, new ListItem("---Select---", ""));
        ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();

    }

    private void getBrachName()
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
            ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getVendorName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
         
            ddlVendor.DataSource = dt;
            ddlVendor.DataValueField = "PRTY_CODE";
            ddlVendor.DataTextField = "PRTY_NAME";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("-------------------------------------All-----------------------------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, ""));
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PO_Detail();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PO_Detail();
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //DataTable dt = SaitexBL.Interface.Method.YRN_INT_MST.Load_Pend_PO_Details(ddlBranch.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(), ddlVendor.SelectedValue.Trim());
            //if (dt.Rows.Count > 0)
            //{
            //   //  url = "../Reports/Pending_PO.aspx?BRANCH_CODE=" + ddlBranch.SelectedValue.Trim() + "&&YEAR=" + ddlYear.SelectedValue.Trim() + "&&VENDOR_CODE=" + ddlVendor.SelectedValue.Trim();
            //   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            //}
            //else
            //{
            //    CommonFuction.ShowMessage("No Data Found. Report cannot be printed");
            //}
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, " Error"));
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
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PO_Detail();
    }
    protected void ddlPoStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PO_Detail();
    }
}


