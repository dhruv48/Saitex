using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Common;
using errorLog;
using System.IO;
using DBLibrary;
using obout_ASPTreeView_2_NET;

public partial class Module_Admin_Controls_NavigationMaster : System.Web.UI.UserControl
{
    obout_ASPTreeView_2_NET.Tree oTree = new obout_ASPTreeView_2_NET.Tree();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                chk_Status.Checked = true;
                this.bindAddModuleMaster();
                this.BindTabMAsterDropDown();
                this.BlanksControls();
                //txtNavigationUrl.Enabled = false;
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                tdFind.Visible = true;
                lblMode.Text = "You are in Save Mode";
                this.GetTreeData();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
        }
    }

    void CreateNodes(string relativePath, string sID)
    {
        string hddPath = MapPath(relativePath);
        string[] listFiles = Directory.GetFiles(hddPath);
        string[] listDirectories = Directory.GetDirectories(hddPath);
        string sPath;
        // Display Subfolders.
        for (int i = 0; i < listDirectories.Length; i++)
        {
            oTree.Add(sID, sID + "_" + (i + 1), Path.GetFileName(listDirectories[i]), false, null, null);
            sPath = relativePath + Path.GetFileName(listDirectories[i]) + "/";
            CreateNodes(sPath, sID + "_" + (i + 1));
        }
        // Display Files.
        for (int i = 0; i < listFiles.Length; i++)
        {
            sPath = HttpUtility.UrlPathEncode(relativePath + Path.GetFileName(listFiles[i]));
            oTree.Add(sID, sID + "_" + (i + 1), "<a href='" + sPath + "'>" + Path.GetFileName(listFiles[i]) + "</a>", null, "page.gif", null);
        }
    }

    private void BindTabMAsterDropDown()
    {
        try
        {
            DataTable dtTabMaster = SaitexBL.Interface.Method.UserNavigationRight.GetTabMaster();
            if (dtTabMaster != null && dtTabMaster.Rows.Count > 0)
            {
                ddlTabMaster.DataSource = dtTabMaster;
                ddlTabMaster.DataTextField = "TAB_NAME";
                ddlTabMaster.DataValueField = "TAB_ID";
                ddlTabMaster.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindAddModuleMaster()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_MODULE_MST.GetModuleMaster();
            ddlParenModuleName.DataValueField = "MDL_ID";
            ddlParenModuleName.DataTextField = "MDL_NAME";
            ddlParenModuleName.DataSource = dt;
            ddlParenModuleName.DataBind();
            ddlParenModuleName.Items.Insert(0, new ListItem("---------Select----------", ""));
        }
        catch
        {
            throw;
        }
    }

    private void bindChildModuleMaster(int strModuleCategoryId, string strSelect)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.GetChildModuleMasterfromModule(strModuleCategoryId, strSelect);
            ddlChildModuleName.DataValueField = "CHILD_MDL_ID";
            ddlChildModuleName.DataTextField = "CHILD_MDL_NAME";
            ddlChildModuleName.DataSource = dt;
            ddlChildModuleName.DataBind();
            ddlChildModuleName.Items.Insert(0, new ListItem("---------Select----------", ""));
            if (strSelect != string.Empty)
            {
                ddlChildModuleName.SelectedValue = strSelect;
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindGvAddModuleNavigation()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavigationMst();
            lblTotalRecord.Text = dt.Rows.Count.ToString();
            gvNavigation.DataSource = dt;
            gvNavigation.DataBind();

        }
        catch
        {
            throw;
        }

    }

    private void bindGvAddModuleNav(int mdlid, int childmdlid)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavigationMaster(mdlid, childmdlid);
            lblTotalRecord.Text = dt.Rows.Count.ToString();
            gvNavigation.DataSource = dt;
            gvNavigation.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void bindGvAddNavigation()
    {
        try
        {
            GetData();
            DataTable dt = (DataTable)ViewState["Navigation"];
            gvNavigation.DataSource = dt;
            gvNavigation.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString();
        }
        catch
        {
            throw;
        }
    }

    private void GetData()
    {
        try
        {
            DataTable dt = new DataTable();

            if ((ddlParenModuleName.SelectedIndex > 0) && (ddlChildModuleName.SelectedIndex > 0))
            {
                int mdlid = int.Parse(ddlParenModuleName.SelectedValue.Trim());
                int childmdlid = int.Parse(ddlChildModuleName.SelectedValue.Trim());
                dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavigationMaster(mdlid, childmdlid);

                ViewState["Navigation"] = dt;
            }
            else if (ddlParenModuleName.SelectedIndex > 0)
            {
                int mdlid = int.Parse(ddlParenModuleName.SelectedValue.Trim());
                dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavigationModule(mdlid);
                ViewState["Navigation"] = dt;
            }
            else
            {
                dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavigationMst();
                ViewState["Navigation"] = dt;
            }
        }
        catch
        {
            throw;
        }

    }

    private void bindGvAddModule(int mdlid)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.selectNavigationModule(mdlid);
            lblTotalRecord.Text = dt.Rows.Count.ToString();
            gvNavigation.DataSource = dt;
            gvNavigation.DataBind();

        }
        catch
        {
            throw;
        }
    }

    protected void gvNavigation_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvNavigation.PageIndex = e.NewPageIndex;
            GetData();
            bindGvAddNavigation();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gridview Paging.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void gvNavigation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "You are in Update Mode";
                ViewState["recordEdit"] = e.CommandArgument.ToString().Trim();
                getNavigationData(Convert.ToInt32(ViewState["recordEdit"]));
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Row Command.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void getNavigationData(int iModuleNavigationId)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.EditNavigationMaster(iModuleNavigationId);
            iModuleNavigationId = Convert.ToInt32(ViewState["recordEdit"].ToString());
            foreach (DataRow dr in dt.Rows)
            {
                ViewState["NAV_ID"] = Convert.ToInt32(dr["NAV_ID"].ToString());
                ddlParenModuleName.SelectedValue = dr["MDL_ID"].ToString();
                bindChildModuleMaster(Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim()), dr["CHILD_MDL_ID"].ToString());
                ddlTabMaster.SelectedValue = dr["TAB_ID"].ToString();
                txtDisplayOrder.Text = dr["DISP_ODR"].ToString();
                txtNavigationName.Text = dr["NAV_NAME"].ToString();
                imgNav.ImageUrl = "~/" + dt.Rows[0]["IMG_URL"].ToString();
                txtNavigationUrl.Text = dr["NAV_URL"].ToString();
                txtRemarks.Text = dr["REMARKS"].ToString();
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "You are in Update Mode";
            }
        }

        catch
        {
            throw;
        }

    }

    private void Insertdata()
    {
        try
        {

            int iRecordFound = 0;
            string strNewNAVId = string.Empty;
            string ImageURL = string.Empty;
            SaitexDM.Common.DataModel.CM_NAV_MST oCM_NAV_MST = new SaitexDM.Common.DataModel.CM_NAV_MST();
            strNewNAVId = SaitexBL.Interface.Method.CM_NAV_MST.getNewNavId();
            oCM_NAV_MST.NAV_ID = Convert.ToInt32(strNewNAVId);
            oCM_NAV_MST.MDL_ID = Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim());
            oCM_NAV_MST.CHILD_MDL_ID = Convert.ToInt32(ddlChildModuleName.SelectedValue.Trim());
            oCM_NAV_MST.NAV_NAME = CommonFuction.funFixQuotes(txtNavigationName.Text.Trim());
            oCM_NAV_MST.NAV_URL = CommonFuction.funFixQuotes(txtNavigationUrl.Text.Trim());
            oCM_NAV_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oCM_NAV_MST.DISP_ODR = Convert.ToInt32(txtDisplayOrder.Text.Trim());
            oCM_NAV_MST.IMG_URL = "ImageURL";
            oCM_NAV_MST.TAB_ID = Convert.ToInt32(ddlTabMaster.SelectedValue.Trim());
            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
            Stream fs = tPhoto.PostedFile.InputStream;
            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }

            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                oCM_NAV_MST.SUB_CAT_IMG = bytearr;
                oCM_NAV_MST.SUB_CAT_CONTENT_TYPE = tPhoto.PostedFile.ContentType;
                oCM_NAV_MST.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
            }
            else
            {
                oCM_NAV_MST.SUB_CAT_CONTENT_TYPE = string.Empty;
                oCM_NAV_MST.POSTED_LENGTH = 0;
            }
            oCM_NAV_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oCM_NAV_MST.TAB_ID = Convert.ToInt32(ddlTabMaster.SelectedValue.Trim());
            oCM_NAV_MST.STATUS = chk_Status.Checked;
            oCM_NAV_MST.TDATE = System.DateTime.Now;
            oCM_NAV_MST.TUSER = oUserLoginDetail.UserCode;
            bool bResult = SaitexBL.Interface.Method.CM_NAV_MST.InsertNavigationMst(oCM_NAV_MST, out iRecordFound);
            if (bResult)
            {

                BlanksControls();
                bindGvAddModuleNavigation();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Navigation Saved Successfully');", true);
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Navigation Is Already Exists Please Enter Another Record');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Navigation Not Saved');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    private void Updatedata()
    {
        try
        {
            int iRecordFound = 0;

            string ImageURL = string.Empty;
            SaitexDM.Common.DataModel.CM_NAV_MST oCM_NAV_MST = new SaitexDM.Common.DataModel.CM_NAV_MST();

            oCM_NAV_MST.NAV_ID = Convert.ToInt32(Convert.ToInt32(ViewState["NAV_ID"]));
            oCM_NAV_MST.MDL_ID = Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim());

            oCM_NAV_MST.CHILD_MDL_ID = Convert.ToInt32(ddlChildModuleName.SelectedValue.Trim());

            //string ss = ddlIndent.SelectedValue.Trim();
            //string[] IND_VAL = ss.Split('@');

            oCM_NAV_MST.NAV_NAME = CommonFuction.funFixQuotes(txtNavigationName.Text.Trim());
            oCM_NAV_MST.NAV_URL = CommonFuction.funFixQuotes(txtNavigationUrl.Text.Trim());
            oCM_NAV_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oCM_NAV_MST.DISP_ODR = Convert.ToInt32(txtDisplayOrder.Text.Trim());
            //ImageURL = CommonFuction.SaveImageFile(tPhoto, "CommonImages", "Navigation");
            //tPhoto.PostedFile.SaveAs(Server.MapPath(@"~/" + ImageURL));
            oCM_NAV_MST.IMG_URL = "ImageURL";
            oCM_NAV_MST.TAB_ID = Convert.ToInt32(ddlTabMaster.SelectedValue.Trim());
            byte[] bytearr = new byte[tPhoto.PostedFile.ContentLength];
            Stream fs = tPhoto.PostedFile.InputStream;
            if (tPhoto.PostedFile.ContentLength != 0)
            {
                fs.Read(bytearr, 0, tPhoto.PostedFile.ContentLength);
            }

            if (tPhoto.PostedFile.ContentLength > 0 && tPhoto.PostedFile.ContentLength < 8388609)
            {
                oCM_NAV_MST.SUB_CAT_IMG = bytearr;
                oCM_NAV_MST.SUB_CAT_CONTENT_TYPE = tPhoto.PostedFile.ContentType;
                oCM_NAV_MST.POSTED_LENGTH = tPhoto.PostedFile.ContentLength;
            }
            else
            {
                //oEM.SUB_IMG = bytearr;
                oCM_NAV_MST.SUB_CAT_CONTENT_TYPE = string.Empty;
                oCM_NAV_MST.POSTED_LENGTH = 0;

            }
            oCM_NAV_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oCM_NAV_MST.TAB_ID = Convert.ToInt32(ddlTabMaster.SelectedValue.Trim());
            oCM_NAV_MST.STATUS = chk_Status.Checked;
            oCM_NAV_MST.TDATE = System.DateTime.Now;
            oCM_NAV_MST.TUSER = oUserLoginDetail.UserCode;
            bool bResult = SaitexBL.Interface.Method.CM_NAV_MST.UpdateNavigationMst(oCM_NAV_MST, out iRecordFound);
            if (bResult)
            {
                BlanksControls();
                bindGvAddModuleNavigation();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Navigation Updated Successfully');", true);
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Navigation Is Already Exists Please Enter Another Record');", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Navigation Not Updated');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    private void Deletedata()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.CM_NAV_MST oCM_NAV_MST = new SaitexDM.Common.DataModel.CM_NAV_MST();
            oCM_NAV_MST.NAV_ID = Convert.ToInt32(Convert.ToInt32(ViewState["NAV_ID"]));
            oCM_NAV_MST.TDATE = System.DateTime.Now;
            oCM_NAV_MST.TUSER = oUserLoginDetail.UserCode;
            bool bResult = SaitexBL.Interface.Method.CM_NAV_MST.DeleteNavigationMst(oCM_NAV_MST, out iRecordFound);
            if (bResult)
            {
                BlanksControls();
                bindGvAddModuleNavigation();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists :Pls Enter Another Record');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Deleted');", true);
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
            ddlParenModuleName.SelectedValue = string.Empty;
            ddlChildModuleName.SelectedValue = string.Empty;
            ddlTabMaster.SelectedIndex = 0;
            txtNavigationName.Text = string.Empty;
            txtNavigationUrl.Text = string.Empty;
            txtDisplayOrder.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "You are in Save Mode";
            cmbNavigation.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlParenModuleName_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlParenModuleName.SelectedValue.Trim() != string.Empty)
            {
                bindChildModuleMaster(Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim()), "");
                int mdlid = Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim());
                bindGvAddModule(mdlid);
                
                
            }
            else
            {
                ddlChildModuleName.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Parent Module Dropdown.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void gvNavigation_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                ViewState["recordEdit"] = e.CommandArgument.ToString().Trim();
                getNavigationData(Convert.ToInt32(ViewState["recordEdit"]));

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gridview RowCommand.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Updatedata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click2(object sender, ImageClickEventArgs e)
    {
        try
        {
            Deletedata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbNavigation_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbNavigation.Items.Clear();
            cmbNavigation.DataSource = data;
            cmbNavigation.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Navigation Loading Dropdown.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbNavigation_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {

            int iModuleNavigationId = Convert.ToInt32(cmbNavigation.SelectedValue.Trim());
            DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.EditNavigationMaster(iModuleNavigationId);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["NAV_ID"] = Convert.ToInt32(dt.Rows[0]["NAV_ID"].ToString());
                ddlParenModuleName.SelectedValue = dt.Rows[0]["MDL_ID"].ToString();
                this.bindChildModuleMaster(Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim()), dt.Rows[0]["CHILD_MDL_ID"].ToString());
                ddlTabMaster.SelectedValue = dt.Rows[0]["TAB_ID"].ToString();
                txtDisplayOrder.Text = dt.Rows[0]["DISP_ODR"].ToString();
                txtNavigationName.Text = dt.Rows[0]["NAV_NAME"].ToString();
                txtNavigationUrl.Text = dt.Rows[0]["NAV_URL"].ToString();
                imgNav.ImageUrl = "~/" + dt.Rows[0]["IMG_URL"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "You are in Update Mode";

            }
            else
            {
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                lblMode.Text = "You are in Save Mode";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Navigation Dropdown Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE NAV_NAME LIKE :SearchQuery OR MDL_NAME LIKE :SearchQuery OR CHILD_MDL_NAME LIKE :SearchQuery ";
            string sortExpression = " ORDER BY NAV_ID";
            string commandText = " SELECT * FROM (SELECT M.MDL_NAME, C.CHILD_MDL_NAME, N.NAV_ID, N.MDL_ID, N.CHILD_MDL_ID, N.NAV_NAME, N.NAV_URL, N.DISP_ODR, N.POSTED_LENGTH, N.TAB_ID, N.IMG_URL, N.REMARKS, N.STATUS, N.DEL_STATUS, N.TDATE, N.TUSER FROM CM_MODULE_MST M, CM_CHILD_MDL_MST C, CM_NAV_MST N WHERE LTRIM (RTRIM (M.MDL_ID)) = LTRIM (RTRIM (N.MDL_ID)) AND LTRIM (RTRIM (C.CHILD_MDL_ID)) = LTRIM(RTRIM (N.CHILD_MDL_ID)) AND N.DEL_STATUS = '0' ORDER BY N.NAV_ID) ASD ";
            string sPO = string.Empty;
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM CM_NAV_MST WHERE  NAV_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            cmbNavigation.Visible = true;
            lblMode.Text = "You are in Update Mode";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void gvNavigation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Selected Index.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlChildModuleName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int mdlid = Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim());
            int childmdlid = Convert.ToInt32(ddlChildModuleName.SelectedValue.Trim());

            AutoCompleteExtender1.ContextKey = mdlid.ToString() + "@" + childmdlid.ToString();

            this.bindGvAddModuleNav(mdlid, childmdlid);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Module Dropdown Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void GetTreeData()
    {
        try
        {
            DataTable dtTree = GetTableForTree();
            int HasFile = 0;
            dtTree = this.GetDataTableForTree("0", dtTree, "~/", out HasFile);
            this.BindTreeView(null, "0");
            TreeView1.CollapseAll();
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetDataTableForTree(string ParentId, DataTable dtTree, string relativePath, out int HasFile)
    {
        try
        {
            HasFile = 0;
            string hddPath = Server.MapPath(relativePath);

            string[] listFiles = Directory.GetFiles(hddPath);
            string[] listDirectories = Directory.GetDirectories(hddPath);
            string sPath;
            //// Display Subfolders.
            for (int i = 0; i < listDirectories.Length; i++)
            {
                sPath = relativePath + Path.GetFileName(listDirectories[i]) + "/";
                HasFile = 0;
                dtTree = this.GetDataTableForTree(ParentId + "_" + (i + 1), dtTree, sPath, out HasFile);
                if (HasFile > 0)
                {
                    DataRow drDir = dtTree.NewRow();
                    drDir["UNIQUE_ID"] = ParentId + "_" + (i + 1);
                    drDir["NAME"] = sPath;
                    drDir["PATH"] = sPath;
                    drDir["PARENT_ID"] = ParentId;
                    dtTree.Rows.Add(drDir);
                }
            }
            //// Display Files.
            for (int i = 0; i < listFiles.Length; i++)
            {
                string fileext = Path.GetExtension(listFiles[i]);

                if (fileext.ToUpper() == ".ASPX" || fileext.ToUpper() == ".HTML" || fileext.ToUpper() == ".HTM")
                {
                    sPath = HttpUtility.UrlPathEncode(relativePath + Path.GetFileName(listFiles[i]));
                    DataRow drDir = dtTree.NewRow();
                    drDir["UNIQUE_ID"] = ParentId + "_" + (i + 1);
                    drDir["NAME"] = sPath;
                    drDir["PATH"] = sPath;
                    drDir["PARENT_ID"] = ParentId;
                    dtTree.Rows.Add(drDir);
                    HasFile += 1;
                }
            }
            ViewState["dtTree"] = dtTree;
            return dtTree;
        }
        catch
        {
            throw;
        }
    }

    private void BindTreeView(TreeNode tn, string ParentId)
    {
        try
        {
            DataTable dtTree = (DataTable)ViewState["dtTree"];
            DataView dvTree = new DataView(dtTree);
            dvTree.RowFilter = "PARENT_ID='" + ParentId + "'";
            if (dvTree.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvTree.Count; iLoop++)
                {
                    string sPath = dvTree[iLoop]["NAME"].ToString();
                    string unique_id = dvTree[iLoop]["unique_id"].ToString();
                    TreeNode tnn = new TreeNode(sPath, sPath);
                    this.BindTreeView(tnn, unique_id);
                    if (tn == null)
                        TreeView1.Nodes.Add(tnn);
                    else
                        tn.ChildNodes.Add(tnn);
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetTableForTree()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UNIQUE_ID", typeof(string));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("PATH", typeof(string));
            dt.Columns.Add("PARENT_ID", typeof(string));
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            txtNavigationUrl.Text = string.Empty;
            string sSelectedNode = TreeView1.SelectedNode.Value.Trim();
            if (sSelectedNode == "~/Default.aspx" || sSelectedNode == "~/GetUserAuthorisation.aspx" || sSelectedNode == "~/Logout.aspx")
            {
                txtNavigationUrl.Text = sSelectedNode.ToString();
            }
            else if (TreeView1.SelectedNode.Depth > 2)
            {
                txtNavigationUrl.Text = TreeView1.SelectedNode.Text;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Tree View Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void lnkTree_Click(object sender, EventArgs e)
    {
        try
        {
            TreeView1.Visible = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Tree Link.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BlanksControls();
            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "You are in Save Mode";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtNavigationName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] NAV_DATA = txtNavigationName.Text.Split('-');
            // (value.GetType().Equals(typeof(string)))
            int iModuleNavigationId = 0;
            int.TryParse(NAV_DATA[0].ToString(), out iModuleNavigationId);

            if (iModuleNavigationId > 0)
            {


                DataTable dt = SaitexBL.Interface.Method.CM_NAV_MST.EditNavigationMaster(iModuleNavigationId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["NAV_ID"] = Convert.ToInt32(dt.Rows[0]["NAV_ID"].ToString());
                    ddlParenModuleName.SelectedValue = dt.Rows[0]["MDL_ID"].ToString();
                    this.bindChildModuleMaster(Convert.ToInt32(ddlParenModuleName.SelectedValue.Trim()), dt.Rows[0]["CHILD_MDL_ID"].ToString());
                    ddlTabMaster.SelectedValue = dt.Rows[0]["TAB_ID"].ToString();
                    txtDisplayOrder.Text = dt.Rows[0]["DISP_ODR"].ToString();
                    txtNavigationName.Text = NAV_DATA[1].ToString();
                    txtNavigationUrl.Text = dt.Rows[0]["NAV_URL"].ToString();
                    imgNav.ImageUrl = "~/" + dt.Rows[0]["IMG_URL"].ToString();
                    txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                    tdSave.Visible = false;
                    tdUpdate.Visible = true;
                    tdDelete.Visible = true;
                    lblMode.Text = "You are in Update Mode";

                }
                else
                {
                    tdSave.Visible = true;
                    tdUpdate.Visible = false;
                    tdDelete.Visible = false;
                    lblMode.Text = "You are in Save Mode";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Navigation Dropdown Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        AjaxControlToolkit.ModalPopupExtender mpeNavigation = new AjaxControlToolkit.ModalPopupExtender();
        mpeNavigation.Hide();
    }
}