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

public partial class Module_OrderDevelopment_Controls_Quality_Shade_Group_Master : System.Web.UI.UserControl
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
            txtYarnCode.Text = "";
            txtYarnCode.Visible = false;
            ddlyarncode.Visible = true;
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
            ddlyarncode.SelectedIndex = -1;
            txtShadeFamilycode.Visible = true;
            txtYarnDescription.Text = string.Empty;
            txtShadeFamilycode.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            lblmsg.Text = string.Empty;
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
            //if (lblmsg.Text == "Yarn Code Avaliable")
            //{
                int iRecordFound = 0;
                oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_SHADE_FAMILY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                oOD_SHADE_FAMILY.YARN_CODE = txtYarnCode.Text.Trim();
                oOD_SHADE_FAMILY.YARN_DESC = txtYarnDescription.Text.Trim();
                oOD_SHADE_FAMILY.SHADE_GROUP = txtShadeFamilycode.Text.Trim();
                oOD_SHADE_FAMILY.REMARKS = txtRemarks.Text.Trim();
                oOD_SHADE_FAMILY.STATUS = "1";
                oOD_SHADE_FAMILY.TUSER = oUserLoginDetail.UserCode;
                oOD_SHADE_FAMILY.DEL_STATUS = "0";
                oOD_SHADE_FAMILY.TDATE = System.DateTime.Now.Date;

                bool Result = SaitexBL.Interface.Method.OD_SHADE_FAMILY.InsertQualityShadeCode(oOD_SHADE_FAMILY, out iRecordFound);
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
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Shade Family Code Already Exisits');", true);
            //}
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
         
            oOD_SHADE_FAMILY.SHADE_FAMILY_CODE = txtShadeFamilycode.Text.Trim();
            oOD_SHADE_FAMILY.SHADE_FAMILY_NAME = txtShadeFamilycode.Text.Trim();
            oOD_SHADE_FAMILY.REMARKS = txtRemarks.Text.Trim();
            oOD_SHADE_FAMILY.STATUS = "1";
            oOD_SHADE_FAMILY.TUSER = oUserLoginDetail.UserCode;
            oOD_SHADE_FAMILY.DEL_STATUS = "0";
            oOD_SHADE_FAMILY.TDATE = System.DateTime.Now.Date;
          

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
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetQualityShadeCodeALL(oOD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                //dv.RowFilter = "PRODUCT_TYPE='" + ddlProductType.SelectedItem.ToString() + "'";
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
          
            txtShadeFamilycode.Visible = false;
           
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
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetQualityShadeBindCodeALL(oOD_SHADE_FAMILY);
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

    protected void gvShadefamily_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset);
        ddlyarncode.Items.Clear();
        ddlyarncode.DataSource = data;
        ddlyarncode.DataBind();
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        e.ItemsCount = GetYarnCount(e.Text);
    }

    private DataTable GetYarnData(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) WHERE   NOT (   YARN_TYPE = 'NON DYED' OR YARN_TYPE = 'DOP DYED' OR YARN_TYPE = 'NA') AND ROWNUM <= 15 ";

            //string CommandText = "SELECT   A.ASS_YARN_CODE YARN_CODE, M.YARN_CAT, A.ASS_YARN_DESC YARN_DESC  FROM   YRN_MST M,YRN_ASSOCATED_MST A WHERE    M.YARN_CODE=A.YARN_CODE  AND  (UPPER (M.YARN_CODE) LIKE :SearchQuery          OR UPPER (A.ASS_YARN_DESC) LIKE :SearchQuery)         AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_code NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) WHERE   NOT (   YARN_TYPE = 'NON DYED' OR YARN_TYPE = 'DOP DYED' OR YARN_TYPE = 'NA') AND ROWNUM <= " + startOffset + ") ";
                
             // whereClause += " AND (A.ASS_YARN_CODE,A.ASS_YARN_DESC) NOT IN ( SELECT   A.ASS_YARN_CODE, A.ASS_YARN_DESC   FROM   YRN_MST M,YRN_ASSOCATED_MST A WHERE    M.YARN_CODE=A.YARN_CODE  AND  (UPPER (M.YARN_CODE) LIKE :SearchQuery          OR UPPER (A.ASS_YARN_DESC) LIKE :SearchQuery)     AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " ORDER BY YARN_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text)
    {

        string CommandText = "SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) WHERE   NOT (   YARN_TYPE = 'NON DYED' OR YARN_TYPE = 'DOP DYED' OR YARN_TYPE = 'NA') ";
        
//        string CommandText = " SELECT   A.ASS_YARN_CODE  FROM   YRN_MST M,YRN_ASSOCATED_MST A WHERE   M.YARN_CODE=A.YARN_CODE  AND   (UPPER (M.YARN_CODE) LIKE :SearchQuery          OR UPPER (A.ASS_YARN_DESC) LIKE :SearchQuery) ";
        string WhereClause = " ";
        //string SortExpression = " ";
        string SortExpression = " ORDER BY YARN_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }
    protected void ddlyarncode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            txtYarnDescription.Text = ddlyarncode.SelectedValue;
            txtYarnCode.Text = ddlyarncode.SelectedText; 
            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

        }

    }
    protected void txtShadeFamilycode_TextChanged(object sender, EventArgs e)
    {
       
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