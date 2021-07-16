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
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_TaxInt : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SaveData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DeleteData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            trTax.Visible = true;
            tdSave.Visible = false;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
            ddlCompany.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlTax.SelectedIndex = -1;
            ddlTaxIntegration.Visible = true;
            bindTaxIntegrationDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Find..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./TaxIntegration.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
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

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help..\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Save";
            txtDate.Text = "";
            txtDate.Text = System.DateTime.Now.Date.ToShortDateString();

            BlankControls();

            ddlTaxIntegration.Visible = false;
            trTax.Visible = false;
            tdDelete.Visible = false;
            tdSave.Visible = true;
            bindCompanyDropdown();
            bindTaxDropdown();
            bindTaxIntegrationDropdown();
        }
        catch
        {
            throw;
        }
    }

    private void BlankControls()
    {
        try
        {
            ddlCompany.SelectedIndex = -1;
            ddlBranch.SelectedIndex = -1;
            ddlTax.SelectedIndex = -1;
            lblMode.Text = "Save";
            lblMessage.Text = "";
        }
        catch
        {
            throw;
        }
    }

    private void bindTaxIntegrationDropdown()
    {
        try
        {
            ddlTaxIntegration.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.FA_TAX_INTEGRATION.GetTaxIntegration();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTaxIntegration.DataValueField = "BRANCH_CODE";
                ddlTaxIntegration.DataTextField = "TAX_CODE";
                ddlTaxIntegration.DataSource = dt;
                ddlTaxIntegration.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTaxIntegration_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindTaxIntegrationDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Tax Integration..\r\nSee error log for detail."));
        }
    }

    protected void ddlTaxIntegration_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            FillTaxIntegrationData();
            trTax.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Tax Integration..\r\nSee error log for detail."));
        }
    }

    private void bindCompanyDropdown()
    {
        try
        {
            ddlCompany.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCompany.DataSource = dt;
                ddlCompany.DataValueField = "COMP_CODE";
                ddlCompany.DataTextField = "COMP_NAME";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("----------- Select Company -----------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strCompCode = string.Empty;
            if (ddlCompany.SelectedIndex != 0)
            {
                strCompCode = ddlCompany.SelectedValue.ToString().Trim();
                bindBranchDropdownByComp(strCompCode);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select company name..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Branch..\r\nSee error log for detail."));
        }
    }

    private void bindBranchDropdownByComp(string strCompCode)
    {
        try
        {
            ddlBranch.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMasterByCompCode(strCompCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlBranch.DataSource = dt;
                ddlBranch.DataValueField = "BRANCH_CODE";
                ddlBranch.DataTextField = "BRANCH_NAME";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("------------ Select Branch ------------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindTaxDropdown()
    {
        try
        {
            ddlTax.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.FA_TAX_MST.SelectAll();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTax.DataValueField = "TAX_CODE";
                ddlTax.DataTextField = "TAX_CODE";
                ddlTax.DataSource = dt;
                ddlTax.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTax_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindTaxDropdown();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading TAX.\r\nSee error log for detail."));
        }
    }

    private void SaveData()
    {
        try
        {
            if (ddlCompany.SelectedIndex != -1)
            {
                if (ddlBranch.SelectedIndex != -1)
                {
                    if (ddlTax.SelectedIndex != -1)
                    {
                        SaitexDM.Common.DataModel.FA_TAX_INTEGRATION oFA_TAX_INTEGRATION = new SaitexDM.Common.DataModel.FA_TAX_INTEGRATION();

                        oFA_TAX_INTEGRATION.COMP_CODE = ddlCompany.SelectedValue.ToString().Trim();
                        oFA_TAX_INTEGRATION.BRANCH_CODE = ddlBranch.SelectedValue.ToString().Trim();
                        oFA_TAX_INTEGRATION.TAX_CODE = ddlTax.SelectedValue.ToString().Trim();
                        oFA_TAX_INTEGRATION.STATUS = true;
                        oFA_TAX_INTEGRATION.TDATE = DateTime.Parse(txtDate.Text.Trim());
                        oFA_TAX_INTEGRATION.TUSER = oUserLoginDetail.UserCode;

                        int iRecordFound = 0;

                        bool bResult = SaitexBL.Interface.Method.FA_TAX_INTEGRATION.Insert(oFA_TAX_INTEGRATION, out iRecordFound);

                        if (bResult)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Data Saved Successfully!');", true);
                            InitialisePage();
                        }
                        else if (iRecordFound > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Error in Saving the Data..');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Please select Tax..');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Please select Branch..');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Please select Company..');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteData()
    {
        try
        {
            SaitexDM.Common.DataModel.FA_TAX_INTEGRATION oFA_TAX_INTEGRATION = new SaitexDM.Common.DataModel.FA_TAX_INTEGRATION();

            oFA_TAX_INTEGRATION.COMP_CODE = ddlCompany.SelectedValue.ToString().Trim();
            oFA_TAX_INTEGRATION.BRANCH_CODE = ddlBranch.SelectedValue.ToString().Trim();
            oFA_TAX_INTEGRATION.TAX_CODE = ddlTax.SelectedValue.ToString().Trim();
            oFA_TAX_INTEGRATION.DEL_STATUS = true;

            bool bResult = SaitexBL.Interface.Method.FA_TAX_INTEGRATION.Delete(oFA_TAX_INTEGRATION);

            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Data Deleted Successfully!');", true);
                InitialisePage();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Error in Deletion.');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillTaxIntegrationData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_TAX_INTEGRATION.GetTaxIntegration();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "TAX_CODE='" + ddlTaxIntegration.SelectedText.ToString().Trim() + "' And BRANCH_CODE='" + ddlTaxIntegration.SelectedValue.ToString().Trim() + "'";

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        ddlCompany.SelectedValue = dv[iLoop]["COMP_CODE"].ToString();
                        string strCompCode = string.Empty;
                        if (ddlCompany.SelectedIndex != 0)
                        {
                            strCompCode = ddlCompany.SelectedValue.ToString().Trim();
                            bindBranchDropdownByComp(strCompCode);
                        }
                        txtDate.Text = dv[iLoop]["TDATE"].ToString();
                        ddlBranch.SelectedValue = dv[iLoop]["BRANCH_CODE"].ToString();
                        ddlTax.SelectedValue = dv[iLoop]["TAX_CODE"].ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}