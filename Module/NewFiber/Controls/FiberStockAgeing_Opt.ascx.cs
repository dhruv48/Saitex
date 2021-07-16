using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Common;
using System.Data;

public partial class Module_Fiber_Controls_FiberStockAgeing_Opt : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }
    public void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Print Mode";
            GetBranch();
            GetYarnType();
            GetYarnCategory();
        }
        catch
        {
            throw;
        }
    }
    private void GetBranch()
    {
        try
        {
            DataTable dt = new DataTable();
            string strCompany = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompany);
            DataView dv = new DataView(dt);
            ddlBranch.DataSource = dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("---------------Select-------------------", ""));
            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }

    }
    private void GetYarnType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiber();
            DataView dv = new DataView(dt);
            ddlItemType.DataSource = dv;
            ddlItemType.DataValueField = "FIBER_CODE";
            ddlItemType.DataTextField = "FIBER_CODE";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("---------------ALL-------------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch { throw; }
    }
    private void GetYarnCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiberCat();
            //dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemCate();
            DataView dv = new DataView(dt);
            ddlCatCode.DataSource = dv;
            ddlCatCode.DataValueField = "FIBER_CAT";
            ddlCatCode.DataTextField = "FIBER_CAT";
            ddlCatCode.DataBind();
            ddlCatCode.Items.Insert(0, new ListItem("-----------------ALL------------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./FiberItemStockAgeing.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string strBranch = string.Empty;
        string strItemType = string.Empty;
        string strCatCode = string.Empty;
        string strDay1 = string.Empty;
        string strDay2 = string.Empty;
        string strDay3 = string.Empty;
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                strBranch = ddlBranch.SelectedValue.ToString().Trim();

                if (ddlItemType.SelectedIndex > 0)
                {
                    strItemType = ddlItemType.SelectedValue.ToString().Trim();
                }


                if (ddlCatCode.SelectedIndex > 0)
                {
                    strCatCode = ddlCatCode.SelectedValue.ToString().Trim();
                }

                if (txtDay1.Text != string.Empty && txtDay2.Text != string.Empty && txtDay3.Text != string.Empty)
                {
                    strDay1 = txtDay1.Text.Trim();
                    strDay2 = txtDay2.Text.Trim();
                    strDay3 = txtDay3.Text.Trim();

                    string URL = "./FiberItemStockAge.aspx?strBranch=" + strBranch + "&strItemType=" + strItemType + "&strCatCode=" + strCatCode + "&strDay1=" + strDay1 + "&strDay2=" + strDay2 + "&strDay3=" + strDay3 + "";
                    //URL = URL.Replace("'", "$");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                {
                    CommonFuction.ShowMessage("Please enter Day1, Day2, and Day3 first..");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please Select Branch first..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

}
