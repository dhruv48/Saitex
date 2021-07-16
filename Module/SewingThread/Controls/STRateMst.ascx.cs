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

public partial class Module_SewingThread_Controls_STRateMst : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.YRN_RATE_INT oYRN_RATE_INT;

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
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Save Mode";
            cmbArticleCode.Visible = false;
            ddlArticleCode.Enabled = true;
            ddlShadeGroup.Enabled = true;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            ClearControls();
            BindArticleCode();
            BindShadeGroup();
        }
        catch
        {
            throw;
        }
    }

    private void ClearControls()
    {
        try
        {
            ddlArticleCode.SelectedIndex = -1;
            ddlShadeGroup.SelectedIndex = -1;
            txtOpeningRate.Text = string.Empty;
            txtTransferPrice.Text = string.Empty;
            txtSalePrice.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void BindArticleCode()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetYarnMasterWithYarnCategory();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlArticleCode.Items.Clear();
                ddlArticleCode.DataSource = dt;
                ddlArticleCode.DataTextField = "YARN_CODE";
                ddlArticleCode.DataValueField = "YARN_CODE";
                ddlArticleCode.DataBind();
                ddlArticleCode.Items.Insert(0, new ListItem("-------------Select------------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindShadeGroup()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("SHADE_GROUP", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeGroup.Items.Clear();
                ddlShadeGroup.DataSource = dt;
                ddlShadeGroup.DataTextField = "MST_DESC";
                ddlShadeGroup.DataValueField = "MST_CODE";
                ddlShadeGroup.DataBind();
                ddlShadeGroup.Items.Insert(0, new ListItem("-------------Select------------", "0"));
            }
        }
        catch
        {

        }
    }

    protected void cmbArticleCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable dtRate = SaitexBL.Interface.Method.YRN_RATE_INT.GetAllArticleRateIntegration(oUserLoginDetail.COMP_CODE);
            if (dtRate != null && dtRate.Rows.Count > 0)
            {
                cmbArticleCode.Items.Clear();
                cmbArticleCode.DataSource = dtRate;
                cmbArticleCode.DataTextField = "ARTICLE_CODE";
                cmbArticleCode.DataValueField = "SHADE_GROUP";
                cmbArticleCode.DataBind();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Loading Article Code.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbArticleCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dtRate = SaitexBL.Interface.Method.YRN_RATE_INT.GetDetailsWithArticleShadeGroup(oUserLoginDetail.COMP_CODE, cmbArticleCode.SelectedText.ToString(), cmbArticleCode.SelectedValue.ToString().Trim());
            if (dtRate != null && dtRate.Rows.Count > 0)
            {
                ddlShadeGroup.SelectedIndex = ddlShadeGroup.Items.IndexOf(ddlShadeGroup.Items.FindByValue(dtRate.Rows[0]["SHADE_GROUP"].ToString()));
                ddlArticleCode.SelectedIndex = ddlArticleCode.Items.IndexOf(ddlArticleCode.Items.FindByValue(dtRate.Rows[0]["ARTICLE_CODE"].ToString()));
                txtOpeningRate.Text = dtRate.Rows[0]["OP_RATE"].ToString();
                txtTransferPrice.Text = dtRate.Rows[0]["TRANS_PRICE"].ToString();
                txtSalePrice.Text = dtRate.Rows[0]["SALE_PRICE"].ToString();
                cmbArticleCode.Visible = false;
                ddlArticleCode.Visible = true;
                ddlArticleCode.Enabled = false;
                ddlShadeGroup.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Article Code.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlArticleCode.Visible = false;
            cmbArticleCode.Visible = true;
            cmbArticleCode.SelectedIndex = -1;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
            lblMode.Text = "You are in Update Mode";
            ddlShadeGroup.Enabled = false;
            txtOpeningRate.Text = string.Empty;
            txtTransferPrice.Text = string.Empty;
            txtSalePrice.Text = string.Empty;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Deleting.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./STRateMaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InsertData()
    {
        try
        {
            if (CheckValidation())
            {
                double dblOpeningRate = 0;
                double dblTransferPrice = 0;
                double dblSalePrice = 0;
                oYRN_RATE_INT = new SaitexDM.Common.DataModel.YRN_RATE_INT();

                oYRN_RATE_INT.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRN_RATE_INT.ARTICLE_CODE = ddlArticleCode.SelectedValue.ToString().Trim();
                oYRN_RATE_INT.SHADE_GROUP = ddlShadeGroup.SelectedValue.ToString().Trim();
                double.TryParse(txtOpeningRate.Text.Trim(), out dblOpeningRate);
                oYRN_RATE_INT.OP_RATE = dblOpeningRate;
                double.TryParse(txtTransferPrice.Text.Trim(), out dblTransferPrice);
                oYRN_RATE_INT.TRANS_PRICE = dblTransferPrice;
                double.TryParse(txtSalePrice.Text.Trim(), out dblSalePrice);
                oYRN_RATE_INT.SALE_PRICE = dblSalePrice;
                oYRN_RATE_INT.TUSER = oUserLoginDetail.UserCode;

                int iRecordFound = 0;
                bool bResult = SaitexBL.Interface.Method.YRN_RATE_INT.Insert(oYRN_RATE_INT, out iRecordFound);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage("Record Saved Successfully !!");
                    InitialisePage();
                }
                else if (iRecordFound > 0)
                {
                    Common.CommonFuction.ShowMessage("This Article Code and Shade Group is already in use.. Please enter another.");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Error.. in saving..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please fill mendatory(*) fields..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void UpdateData()
    {
        try
        {
            double dblOpeningRate = 0;
            double dblTransferPrice = 0;
            double dblSalePrice = 0;
            oYRN_RATE_INT = new SaitexDM.Common.DataModel.YRN_RATE_INT();

            oYRN_RATE_INT.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_RATE_INT.ARTICLE_CODE = ddlArticleCode.SelectedValue.ToString().Trim();
            oYRN_RATE_INT.SHADE_GROUP = ddlShadeGroup.SelectedValue.ToString().Trim();
            double.TryParse(txtOpeningRate.Text.Trim(), out dblOpeningRate);
            oYRN_RATE_INT.OP_RATE = dblOpeningRate;
            double.TryParse(txtTransferPrice.Text.Trim(), out dblTransferPrice);
            oYRN_RATE_INT.TRANS_PRICE = dblTransferPrice;
            double.TryParse(txtSalePrice.Text.Trim(), out dblSalePrice);
            oYRN_RATE_INT.SALE_PRICE = dblSalePrice;
            oYRN_RATE_INT.TUSER = oUserLoginDetail.UserCode;

            bool bResult = SaitexBL.Interface.Method.YRN_RATE_INT.Update(oYRN_RATE_INT);
            if (bResult)
            {
                Common.CommonFuction.ShowMessage("Record Updated Successfully !!");
                InitialisePage();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Error.. in updation..");
            }
        }
        catch
        {
            throw;
        }
    }

    private bool CheckValidation()
    {
        try
        {
            bool IsValidation = false;
            if (ddlArticleCode.SelectedIndex != 0)
            {
                if (ddlShadeGroup.SelectedIndex != 0)
                {
                    if (txtOpeningRate.Text != "")
                    {
                        if (txtTransferPrice.Text != "")
                        {
                            if (txtSalePrice.Text != "")
                            {
                                IsValidation = true;
                            }
                            else
                            {
                                IsValidation = false;
                                Common.CommonFuction.ShowMessage("Dear! Please enter Sale Price..");
                            }
                        }
                        else
                        {
                            IsValidation = false;
                            Common.CommonFuction.ShowMessage("Dear! Please enter Transfer Rate..");
                        }
                    }
                    else
                    {
                        IsValidation = false;
                        Common.CommonFuction.ShowMessage("Dear! Please enter Opening Rate..");
                    }
                }
                else
                {
                    IsValidation = false;
                    Common.CommonFuction.ShowMessage("Dear! Please select Shade Group..");
                }
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please select Article Code..");
            }
            return IsValidation;
        }
        catch
        {
            return false;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}