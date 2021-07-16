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


public partial class Module_OrderDevelopment_Fiber_Lap_Dip_Controls_Fib_Quality_Shade_Group_Master : System.Web.UI.UserControl
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
                oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFIBER_OD_SHADE_FAMILY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                oFIBER_OD_SHADE_FAMILY.FIBER_CODE = txtYarnCode.Text.Trim();
                oFIBER_OD_SHADE_FAMILY.FIBER_DESC = txtYarnDescription.Text.Trim();
                oFIBER_OD_SHADE_FAMILY.SHADE_GROUP = txtShadeFamilycode.Text.Trim();
                oFIBER_OD_SHADE_FAMILY.REMARKS = txtRemarks.Text.Trim();
                oFIBER_OD_SHADE_FAMILY.STATUS = "1";
                oFIBER_OD_SHADE_FAMILY.TUSER = oUserLoginDetail.UserCode;
                oFIBER_OD_SHADE_FAMILY.DEL_STATUS = "0";
                oFIBER_OD_SHADE_FAMILY.TDATE = System.DateTime.Now.Date;

                bool Result = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.InsertQualityShadeCode(oFIBER_OD_SHADE_FAMILY, out iRecordFound);
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
            oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
         
            oFIBER_OD_SHADE_FAMILY.SHADE_FAMILY_CODE = txtShadeFamilycode.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.SHADE_FAMILY_NAME = txtShadeFamilycode.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.REMARKS = txtRemarks.Text.Trim();
            oFIBER_OD_SHADE_FAMILY.STATUS = "1";
            oFIBER_OD_SHADE_FAMILY.TUSER = oUserLoginDetail.UserCode;
            oFIBER_OD_SHADE_FAMILY.DEL_STATUS = "0";
            oFIBER_OD_SHADE_FAMILY.TDATE = System.DateTime.Now.Date;
          

            bool Result = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.UpdateShadeFamilyCode(oFIBER_OD_SHADE_FAMILY);
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
            oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            DataTable dt = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GetQualityShadeCodeALL(oFIBER_OD_SHADE_FAMILY);
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
            oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            DataTable dt = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GetQualityShadeBindCodeALL(oFIBER_OD_SHADE_FAMILY);
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
            string CommandText = " SELECT * FROM (SELECT FIBER_CODE, FIBER_DESC, FIBER_CAT FROM TX_FIBER_NEW_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) WHERE   NOT (   FIBER_CAT = 'NON DYED' OR FIBER_CAT = 'DOP DYED' OR FIBER_CAT = 'NA') AND ROWNUM <= 15 ";

            //string CommandText = "SELECT   A.ASS_FIBER_CODE FIBER_CODE, M.YARN_CAT, A.ASS_FIBER_DESC FIBER_DESC  FROM   TX_FIBER_NEW_MASTER M,YRN_ASSOCATED_MST A WHERE    M.FIBER_CODE=A.FIBER_CODE  AND  (UPPER (M.FIBER_CODE) LIKE :SearchQuery          OR UPPER (A.ASS_FIBER_DESC) LIKE :SearchQuery)         AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND FIBER_CODE NOT IN (SELECT FIBER_CODE FROM (SELECT FIBER_CODE, FIBER_DESC FROM TX_FIBER_NEW_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) WHERE   NOT (   FIBER_CAT = 'NON DYED' OR FIBER_CAT = 'DOP DYED' OR FIBER_CAT = 'NA') AND ROWNUM <= " + startOffset + ") ";
                
             // whereClause += " AND (A.ASS_FIBER_CODE,A.ASS_FIBER_DESC) NOT IN ( SELECT   A.ASS_FIBER_CODE, A.ASS_FIBER_DESC   FROM   TX_FIBER_NEW_MASTER M,YRN_ASSOCATED_MST A WHERE    M.FIBER_CODE=A.FIBER_CODE  AND  (UPPER (M.FIBER_CODE) LIKE :SearchQuery          OR UPPER (A.ASS_FIBER_DESC) LIKE :SearchQuery)     AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " ORDER BY FIBER_CODE";
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

        string CommandText = "SELECT * FROM (SELECT FIBER_CODE, FIBER_DESC, FIBER_CAT FROM TX_FIBER_NEW_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) WHERE   NOT (   FIBER_CAT = 'NON DYED' OR FIBER_CAT = 'DOP DYED' OR FIBER_CAT = 'NA') ";
        
//        string CommandText = " SELECT   A.ASS_FIBER_CODE  FROM   TX_FIBER_NEW_MASTER M,YRN_ASSOCATED_MST A WHERE   M.FIBER_CODE=A.FIBER_CODE  AND   (UPPER (M.FIBER_CODE) LIKE :SearchQuery          OR UPPER (A.ASS_FIBER_DESC) LIKE :SearchQuery) ";
        string WhereClause = " ";
        //string SortExpression = " ";
        string SortExpression = " ORDER BY FIBER_CODE ";
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Fiber Code Selection.\r\nSee error log for detail."));

        }

    }
    protected void txtShadeFamilycode_TextChanged(object sender, EventArgs e)
    {
       
        oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oFIBER_OD_SHADE_FAMILY.SHADE_FAMILY_CODE = txtShadeFamilycode.Text.Trim();
        bool Result = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GenrateShadeFamilyCode(oFIBER_OD_SHADE_FAMILY);
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
