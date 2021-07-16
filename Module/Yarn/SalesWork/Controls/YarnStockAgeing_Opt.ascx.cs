using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Common;
using System.Data;

public partial class Module_Yarn_SalesWork_Controls_YarnStockAgeing_Opt : System.Web.UI.UserControl
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
           // GetYarnType();
           // GetYarnCategory();
            bindCategory("YARN_CAT");
            bindYarnType("YARN_TYPE");
            BindDepartment(ddlstore);
            BindDropDown(ddllocation);

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
            dt = SaitexBL.Interface.Method.Yarn_Stock_Data.GetYarnType();
            DataView dv = new DataView(dt);
            ddlItemType.DataSource = dv;
            ddlItemType.DataValueField = "YARN_TYPE";
            ddlItemType.DataTextField = "YARN_TYPE";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("---------------ALL-------------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch { throw; }
    }

    public void bindYarnType(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAMEForYarnType(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
               // dv.RowFilter = "MST_DESC='" + TYPE + "'";
                if (dv != null && dv.Count > 0)
                {
                    ddlItemType.Items.Clear();
                    ddlItemType.DataSource = dv;
                    ddlItemType.DataTextField = "MST_CODE";
                    ddlItemType.DataValueField = "MST_CODE";
                    ddlItemType.DataBind();
                    ddlItemType.Items.Insert(0, new ListItem("---------------ALL-------------------", ""));
                    dt.Dispose();
                    dt = null;
                }
                else
                {
                    ddlItemType.Items.Clear();
                    ddlItemType.Items.Insert(0, new ListItem("------NoItems------", "0"));

                }
            }
        }
        catch
        {
            throw;
        }


    }


    private void GetYarnCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt =  SaitexBL.Interface.Method.Yarn_Stock_Data.GetYarnCate();
            //dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemCate();
            DataView dv = new DataView(dt);
            ddlCatCode.DataSource = dv;
            ddlCatCode.DataValueField = "YARN_CAT";
            ddlCatCode.DataTextField = "YARN_CAT";
            ddlCatCode.DataBind();
            ddlCatCode.Items.Insert(0, new ListItem("-----------------ALL------------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch {
            throw;
        }
    }


    public void bindCategory(string MST_NAME)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlCatCode.Items.Clear();
                ddlCatCode.DataSource = dt;
                ddlCatCode.DataTextField = "MST_DESC";
                ddlCatCode.DataValueField = "MST_DESC";
                ddlCatCode.DataBind();
                ddlCatCode.Items.Insert(0, new ListItem("------Select------", "0"));

            }
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
            Response.Redirect("./YarnItemStockAgeing.aspx", false);
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
        string LOCATION = string.Empty;
        string STORE = string.Empty;
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
                if (ddllocation.SelectedIndex > 0)
                {
                    LOCATION = ddllocation.SelectedValue.ToString().Trim();
                }
                if (ddlstore.SelectedIndex > 0)
                {
                    STORE = ddlstore.SelectedValue.ToString().Trim();
                }

                if (txtDay1.Text != string.Empty && txtDay2.Text != string.Empty && txtDay3.Text != string.Empty)
                {
                    strDay1 = txtDay1.Text.Trim();
                    strDay2 = txtDay2.Text.Trim();
                    strDay3 = txtDay3.Text.Trim();

                    string URL = "./YarnItemStockAge.aspx?strBranch=" + strBranch + "&strItemType=" + strItemType + "&strCatCode=" + strCatCode + "&LOCATION=" + LOCATION + "&STORE=" + STORE + "&strDay1=" + strDay1 + "&strDay2=" + strDay2 + "&strDay3=" + strDay3 + "";
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
    private void BindDropDown(DropDownList ddllocation)
    {
        try
        {
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {


                ddllocation.DataSource = dt;
                ddllocation.DataValueField = "MST_DESC";
                ddllocation.DataTextField = "MST_DESC";
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }

            //else
            //{
            //    ddllocation.DataSource = null;
            //    ddllocation.DataBind();
            //    ddllocation.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

            //}
            //ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
        }
        catch
        {
            throw;
        }
    }
    private void BindDepartment(DropDownList ddlstore)
    {
        try
        {
            ddlstore.Items.Clear();
            DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            {

                ddlstore.DataSource = dtDepartment;
                ddlstore.DataValueField = "DEPT_NAME";
                ddlstore.DataTextField = "DEPT_NAME";
                ddlstore.DataBind();
                ddlstore.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dtDepartment.Dispose();
                dtDepartment = null;
            }
            //ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }
    
}
