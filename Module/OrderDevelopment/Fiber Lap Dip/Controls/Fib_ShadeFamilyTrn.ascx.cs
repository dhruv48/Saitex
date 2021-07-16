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
using System.Drawing;


public partial class Module_OrderDevelopment_Fiber_Lap_Dip_Controls_Fib_ShadeFamilyTrn : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.FIBER_OD_SHADE_FAMILY oFIBER_OD_SHADE_FAMILY = new SaitexDM.Common.DataModel.FIBER_OD_SHADE_FAMILY();
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

            BlanksControls();
            BindProductType();


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

            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("FIBER_TYPE", oUserLoginDetail.COMP_CODE);
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

                BindShadeFamilyName();

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
            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdFind.Visible = true;
            tdUpdate.Visible = false;
            ddlProductType.Enabled = true;
            txtShadecode.Text = "";
            ddlShadeCodes.Visible = false;
            ddlProductType.SelectedIndex = -1;
            ddlShadeFamilyName.SelectedIndex = -1;
            ddlShadeFamilyName.Enabled = true;
            //txtShadeName .Text = string.Empty;
            txtRemarks.Text = string.Empty;
            lblmsg.Text = string.Empty;
            txtRGBColor.Text = string.Empty;
            txtRGB.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iRecordFound = 0;
            oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFIBER_OD_SHADE_FAMILY.PRODUCT_TYPE = ddlProductType.SelectedItem.ToString();
            oFIBER_OD_SHADE_FAMILY.SHADE_FAMILY_CODE = ddlShadeFamilyName.SelectedValue.ToString();
            oFIBER_OD_SHADE_FAMILY.SHADE_CODE = txtShadecode.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.SHADE_NAME = txtShadecode.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.REMARKS = txtRemarks.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.STATUS = "1";
            oFIBER_OD_SHADE_FAMILY.TUSER = oUserLoginDetail.UserCode;
            oFIBER_OD_SHADE_FAMILY.DEL_STATUS = "0";
            oFIBER_OD_SHADE_FAMILY.TDATE = System.DateTime.Now.Date;
            oFIBER_OD_SHADE_FAMILY.RGB = txtRGB.Text;
            bool Result = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.InsertShadeCode(oFIBER_OD_SHADE_FAMILY, out iRecordFound);
            if (Result)
            {
                BlanksControls();
                //bindGridByProductType();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Shade Code Saved Successfully');", true);

            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Shade  Code Already Exists');", true);


            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Shade  Code Not Saved');", true);

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
            oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFIBER_OD_SHADE_FAMILY.PRODUCT_TYPE = ddlProductType.SelectedItem.ToString();
            oFIBER_OD_SHADE_FAMILY.SHADE_FAMILY_CODE = ddlShadeFamilyName.SelectedItem.ToString();
            oFIBER_OD_SHADE_FAMILY.SHADE_CODE = txtShadecode.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.SHADE_NAME = txtShadecode.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.REMARKS = txtRemarks.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.STATUS = "1";
            oFIBER_OD_SHADE_FAMILY.TUSER = oUserLoginDetail.UserCode;
            oFIBER_OD_SHADE_FAMILY.DEL_STATUS = "0";
            oFIBER_OD_SHADE_FAMILY.TDATE = System.DateTime.Now.Date;
            oFIBER_OD_SHADE_FAMILY.RGB = txtRGB.Text;

            bool Result = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.UpdateShadeCode(oFIBER_OD_SHADE_FAMILY);
            if (Result)
            {
                BlanksControls();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Shade Family code Updated Successfully');", true);

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Updation.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            Response.Redirect("~/Module/OrderDevelopment/Pages/ShadeFamilyTrn.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Clearing.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        //    string URL = "../Reports/rptModule.aspx";
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
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

    protected void imgfind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMode.Text = "Update";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdFind.Visible = false;
            ddlShadeFamilyName.Enabled = false;
            ddlShadeCodes.Visible = true;
            ddlProductType.Enabled = false;

            //txtShadecode.Visible = false; 
            txtShadecode.Enabled = false;
            BindShadesCode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

    protected void gvShadefamily_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void gvShadefamily_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void ddlShadeCodes_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            string Combined = ddlShadeCodes.SelectedValue.ToString();
            string[] Combined_strings = Combined.Split('@');

            oFIBER_OD_SHADE_FAMILY.COMP_CODE = Combined_strings[0].ToString();
            oFIBER_OD_SHADE_FAMILY.PRODUCT_TYPE = Combined_strings[1].ToString();
            oFIBER_OD_SHADE_FAMILY.SHADE_FAMILY_CODE = Combined_strings[2].ToString();
            oFIBER_OD_SHADE_FAMILY.SHADE_CODE = Combined_strings[3].ToString();

            DataTable dt = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GetShadeCodeByKeys(oFIBER_OD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                BindProductType();
                ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByText(dt.Rows[0]["PRODUCT_TYPE"].ToString()));

                BindShadeFamilyName();
                ddlShadeFamilyName.SelectedIndex = ddlShadeFamilyName.Items.IndexOf(ddlShadeFamilyName.Items.FindByText(dt.Rows[0]["SHADE_FAMILY_CODE"].ToString()));
                txtShadecode.Text = dt.Rows[0]["SHADE_CODE"].ToString();
                //txtShadeName.Text = dt.Rows[0]["SHADE_NAME"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in ShadeCode Changing.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }



    }

    protected void gvShadefamily_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlProductType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            BindShadeFamilyName();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in ShadeCode Changing.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindShadeFamilyName()
    {
        try
        {
            oFIBER_OD_SHADE_FAMILY.PRODUCT_TYPE = ddlProductType.SelectedItem.ToString();
            oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;

            DataTable dt = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GetShadeFamilyCodeByProducttype(oFIBER_OD_SHADE_FAMILY);

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeFamilyName.Items.Clear();
                ddlShadeFamilyName.DataSource = dt;
                ddlShadeFamilyName.DataTextField = "SHADE_FAMILY_CODE";
                ddlShadeFamilyName.DataValueField = "SHADE_FAMILY_CODE";
                ddlShadeFamilyName.DataBind();
                ddlShadeFamilyName.Items.Insert(0, new ListItem("------Select------", "0"));

            }
            else
            {
                ddlShadeFamilyName.Items.Clear();
                ddlShadeFamilyName.Items.Insert(0, new ListItem("NoShadeExists", "0"));
            }

        }
        catch
        {
            throw;
        }

    }

    protected void ddlShadeFamilyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //oFIBER_OD_SHADE_FAMILY.PRODUCT_TYPE = ddlProductType.SelectedItem.ToString();
            //oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            //oFIBER_OD_SHADE_FAMILY.SHADE_FAMILY_CODE = ddlShadeFamilyName.SelectedValue.ToString();
            //txtShadecode.Text = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GenrateShadeCode(oFIBER_OD_SHADE_FAMILY).ToString();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Family Name.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }


    }

    private void BindShadesCode()
    {

        try
        {
            string text = string.Empty;
            string CommandText = " SELECT   O.COMP_CODE, O.PRODUCT_TYPE, O.SHADE_FAMILY_CODE, O.SHADE_CODE, O.SHADE_NAME, O.REMARKS, O.STATUS, O.DEL_STATUS, O.TUSER, O.TDATE, O.COMP_CODE || '@' ||  O.PRODUCT_TYPE || '@' || O.SHADE_FAMILY_CODE || '@' || o.SHADE_CODE as Combined  FROM   FIBER_OD_SHADE_FAMILY_TRN O  ";
            string WhereClause = "  where O.SHADE_CODE like :SearchQuery or O.PRODUCT_TYPE like :SearchQuery ";
            string SortExpression = " order by O.SHADE_CODE asc";
            string SearchQuery = text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeCodes.Items.Clear();
                ddlShadeCodes.DataSource = dt;
                ddlShadeCodes.DataTextField = "SHADE_CODE";
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
    protected void txtShadecode_TextChanged(object sender, EventArgs e)
    {
        oFIBER_OD_SHADE_FAMILY.PRODUCT_TYPE = ddlProductType.SelectedItem.ToString();
        oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oFIBER_OD_SHADE_FAMILY.SHADE_FAMILY_CODE = ddlShadeFamilyName.SelectedValue.ToString();
        oFIBER_OD_SHADE_FAMILY.SHADE_CODE = txtShadecode.Text.Trim();
        bool Result = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GenrateShadeCode(oFIBER_OD_SHADE_FAMILY);
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
    protected void txtRGB_TextChanged(object sender, EventArgs e)
    {
        txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
    }

    public Color getRGBColor(string argb)
    {
        int r = 0;
        int g = 0;
        int b = 0;
        Color RGB = Color.White;
        try
        {

            if (!string.IsNullOrEmpty(argb))
            {
                string[] argbstring = argb.Split(',');
                if (argbstring.Length > 2)
                {
                    int.TryParse(argbstring[0].ToString(), out r);
                    int.TryParse(argbstring[1].ToString(), out g);
                    int.TryParse(argbstring[2].ToString(), out b);

                    if (r > 255 || g > 255 || b > 255 || r < 0 || g < 0 || b < 0)
                    {
                        Common.CommonFuction.ShowMessage("R G B values are being less then 0 or greater then 255.");
                    }
                    else
                    {
                        RGB = Color.FromArgb(r, g, b);
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("make space between R G B values.");
                    RGB = Color.FromArgb(255, 255, 255);
                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is getting Shade color.\r\nSee error log for detail."));
        }
        return RGB;

    }
}



