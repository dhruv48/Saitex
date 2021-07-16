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

using System.IO;
using errorLog;
using Common;

public partial class Module_OrderDevelopment_Controls_ShadeFamilyMaster : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                {
                    if (!IsPostBack)
                    {
                        Inital();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void Inital()
    {
        try
        {
            BindGrid();
            BlanksControls();
            BindProductType();
            BindShadeGroup();
        }
        catch
        {
            throw;
        }
    }

    private void BindProductType()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlProductType.Items.Clear();
                ddlProductType.DataSource = dt;
                ddlProductType.DataTextField = "MST_DESC";
                ddlProductType.DataValueField = "MST_CODE";
                ddlProductType.DataBind();
                ddlProductType.Items.Insert(0, new ListItem("------Select------", "0"));

                //Code add as per bharat requriement 12 Sept-2011
                ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByText("SEWING THREAD"));

            }
        }
        catch
        {
            throw;
        }
    }

    private void BlanksControls()
    {
        try
        {
            lblMode.Text = "You are in Save Mode";
            tdSave.Visible = true;
            tdFind.Visible = true;
            tdUpdate.Visible = false;
            ddlProductType.Enabled = true;
            txtShadeFamilycode.Visible = true;
            ddlShadeCodes.Visible = false;
            ddlProductType.SelectedIndex = -1;
            txtShadeFamilycode.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            lblmsg.Text = string.Empty;
            ddlShadeGroup.SelectedIndex = -1;
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
                ddlShadeGroup.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch
        {

        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (lblmsg.Text == "Shade Family Code Avaliable")
            {
                int iRecordFound = 0;
                oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_SHADE_FAMILY.PRODUCT_TYPE = ddlProductType.SelectedItem.ToString();
                oOD_SHADE_FAMILY.SHADE_FAMILY_CODE = txtShadeFamilycode.Text.Trim();
                oOD_SHADE_FAMILY.SHADE_FAMILY_NAME = txtShadeFamilycode.Text.Trim();
                oOD_SHADE_FAMILY.REMARKS = txtRemarks.Text.Trim();
                oOD_SHADE_FAMILY.STATUS = "1";
                oOD_SHADE_FAMILY.TUSER = oUserLoginDetail.UserCode;
                oOD_SHADE_FAMILY.DEL_STATUS = "0";
                oOD_SHADE_FAMILY.TDATE = System.DateTime.Now.Date;
                oOD_SHADE_FAMILY.SHADE_GROUP = ddlShadeGroup.SelectedValue.ToString().Trim();

                bool Result = SaitexBL.Interface.Method.OD_SHADE_FAMILY.InsertShadeFamilyCode(oOD_SHADE_FAMILY, out iRecordFound);
                if (Result)
                {
                    bindGridByProductType();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Shade Family code Saved Successfully');", true);
                    BlanksControls();
                }
                else if (iRecordFound > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Shade Family code Already Exists');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Shade Family code Not Saved');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Shade Family Code Already Exisits');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Saving.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iRecordFound = 0;
            oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_SHADE_FAMILY.PRODUCT_TYPE = ddlProductType.SelectedItem.ToString();
            oOD_SHADE_FAMILY.SHADE_FAMILY_CODE = txtShadeFamilycode.Text.Trim();
            oOD_SHADE_FAMILY.SHADE_FAMILY_NAME = txtShadeFamilycode.Text.Trim();
            oOD_SHADE_FAMILY.REMARKS = txtRemarks.Text.Trim();
            oOD_SHADE_FAMILY.STATUS = "1";
            oOD_SHADE_FAMILY.TUSER = oUserLoginDetail.UserCode;
            oOD_SHADE_FAMILY.DEL_STATUS = "0";
            oOD_SHADE_FAMILY.TDATE = System.DateTime.Now.Date;
            oOD_SHADE_FAMILY.SHADE_GROUP = ddlShadeGroup.SelectedValue.ToString().Trim();

            bool Result = SaitexBL.Interface.Method.OD_SHADE_FAMILY.UpdateShadeFamilyCode(oOD_SHADE_FAMILY);
            if (Result)
            {
                bindGridByProductType();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Shade Family code Updated Successfully');", true);
                BlanksControls();
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Shade Family code Already Exists');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Shade Family code Not Update');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Saving.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            Response.Redirect("~/Module/OrderDevelopment/Pages/ShadeFamilyMaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Clearing.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                string test = Session["RedirectURL"].ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Existing.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void gvAddModule_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gvAddModule_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }

    protected void btnView_Click(object sender, EventArgs e)
    {

    }

    protected void btnRedirect_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancelRed_Click(object sender, EventArgs e)
    {

    }

    private void UpdateModuleData(int ModuleID)
    {


    }

    private void deleteAddModuleData(int MouduleID)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);


    }

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridByProductType();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PRODUCT SELECTION.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindGridByProductType()
    {

        try
        {
            oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeFamilyCodeALL(oOD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "PRODUCT_TYPE='" + ddlProductType.SelectedItem.ToString() + "'";
                if (dv != null && dv.Count > 0)
                {
                    lblErrorMessage.Text = "";
                    gvShadefamily.DataSource = dv;
                    gvShadefamily.DataBind();
                }
                else
                {
                    lblErrorMessage.Text = "No Row Exists!!";
                    gvShadefamily.DataSource = null;
                    gvShadefamily.DataBind();
                }

            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgfind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMode.Text = "You are in Update Mode";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdFind.Visible = false;
            BindShades();
            txtShadeFamilycode.Visible = false;
            ddlShadeCodes.Visible = true;
            ddlProductType.Enabled = false;
            ddlShadeGroup.Enabled = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindGrid()
    {
        try
        {
            oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeFamilyCodeALL(oOD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvShadefamily.DataSource = dt;
                gvShadefamily.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void gvShadefamily_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShadefamily.PageIndex = e.NewPageIndex;
        BindGrid();

    }

    protected void gvShadefamily_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void ddlShadeCodes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Combined = ddlShadeCodes.SelectedItem.ToString();
            string[] Combined_strings = Combined.Split('@');
            oOD_SHADE_FAMILY.PRODUCT_TYPE = Combined_strings[0].ToString();
            oOD_SHADE_FAMILY.SHADE_GROUP = Combined_strings[1].ToString();
            oOD_SHADE_FAMILY.SHADE_FAMILY_CODE = Combined_strings[2].ToString();
            oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;

            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeFamilyCodeByKeys(oOD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByText(dt.Rows[0]["PRODUCT_TYPE"].ToString()));
                ddlShadeGroup.SelectedIndex = ddlShadeGroup.Items.IndexOf(ddlShadeGroup.Items.FindByText(dt.Rows[0]["SHADE_GROUP"].ToString()));
                txtShadeFamilycode.Text = dt.Rows[0]["SHADE_FAMILY_CODE"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in ShadeCode Changing.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

    private void BindShades()
    {

        try
        {
            string text = string.Empty;
            string CommandText = " SELECT   O.COMP_CODE, O.DEL_STATUS, O.PRODUCT_TYPE, O.REMARKS, O.SHADE_FAMILY_CODE, O.SHADE_FAMILY_NAME, O.STATUS, O.TDATE, O.TUSER, O.PRODUCT_TYPE || '@' ||  O.SHADE_GROUP || '@' || O.SHADE_FAMILY_CODE as Combined  FROM   OD_SHADE_FAMILY_MST O";
            string WhereClause = "  where O.SHADE_FAMILY_CODE like :SearchQuery or O.PRODUCT_TYPE like :SearchQuery ";
            string SortExpression = " order by O.SHADE_FAMILY_CODE asc";
            string SearchQuery = text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeCodes.Items.Clear();
                ddlShadeCodes.DataSource = dt;
                ddlShadeCodes.DataTextField = "Combined";
                ddlShadeCodes.DataValueField = "Combined";
                ddlShadeCodes.DataBind();
                ddlShadeCodes.Items.Insert(0, new ListItem("------Select------", "0"));
            }

        }
        catch
        {

            throw;
        }
    }

    protected void gvShadefamily_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtShadeFamilycode_TextChanged(object sender, EventArgs e)
    {
        oOD_SHADE_FAMILY.PRODUCT_TYPE = ddlProductType.SelectedItem.ToString();
        oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oOD_SHADE_FAMILY.SHADE_FAMILY_CODE = txtShadeFamilycode.Text.Trim();
        bool Result = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GenrateShadeFamilyCode(oOD_SHADE_FAMILY);
        if (Result)
        {
            lblmsg.Text = "Shade Family Code Avaliable";
            lblmsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblmsg.Text = "Shade Family Code Already Exists";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }
}