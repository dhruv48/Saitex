using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class RND_UserAccessRight : System.Web.UI.UserControl
{
    private static DataSet dsDatabase;
    private static DataTable dtTempUserRightMaster;


    private string UserCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "You are in Save Mode";
            lblErrorMessage.Text = "";

            if (!IsPostBack)
            {
                getUserMenuData();
                SetTempDataTable();
                // IsInsert = true;
                GetData();
                //  BindUserGrid();
                BindModule(null);
            }
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["sSuccess"].ToString().Trim() != "")
                {
                    string smsg = Request.QueryString["sSuccess"].ToString();
                    smsg = smsg.Replace("</ br>", "\\r\\n");

                    // lblMessage.Text = Request.QueryString["sSuccess"].ToString();                   
                    Common.CommonFuction.ShowMessage(smsg);

                }
                Session["saveStatus"] = 0;
            }

        }
        catch (Exception oex)
        {
            lblErrorMessage.Text = oex.Message;
        }
    }
    /// <summary>
    /// Method to get data for creating menu link provided to user
    /// </summary>
    private void getUserMenuData()
    {
        try
        {
            SaitexDM.Common.DataModel.UserAccessRight oUserAccessRight = new SaitexDM.Common.DataModel.UserAccessRight();
            oUserAccessRight.UserCode = Session["urLoginId"].ToString().Trim();
            DataTable dtUserMenu = SaitexBL.Interface.Method.UserNavigationRight.GetUserNavigationRightByUserCode(oUserAccessRight);
            if (dtUserMenu != null && dtUserMenu.Rows.Count > 0)
                Session["UserMenu"] = dtUserMenu;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            //            ErrHandler.WriteError(ex.Message);
        }
    }
    private void GetData()
    {
        try
        {
            dsDatabase = SaitexBL.Interface.Method.UserNavigationRight.GetUserAccessRight();
        }
        catch (Exception oex)
        {
            lblErrorMessage.Text = oex.Message;
        }
    }

    private void BindModule(TreeNode tn)
    {
        DataView dvModule = new DataView(dsDatabase.Tables["ModuleMaster"]);
        // dvModule.RowFilter = "parentid=" + PaerntId;
        if (dvModule.Count > 0)
        {
            for (int iLoop = 0; iLoop < dvModule.Count; iLoop++)
            {
                TreeNode tnn = new TreeNode(dvModule[iLoop]["MDL_NAME"].ToString(), dvModule[iLoop]["MDL_ID"].ToString());
                BindChildModule(tnn, Convert.ToInt32(dvModule[iLoop]["MDL_ID"].ToString()));
                if (tn == null)
                    trvModule.Nodes.Add(tnn);
                else
                    tn.ChildNodes.Add(tnn);
            }
        }
    }

    private void BindChildModule(TreeNode tn, int ModuleId)
    {
        DataView dvChildModule = new DataView(dsDatabase.Tables["ChildModuleMaster"]);
        dvChildModule.RowFilter = "MDL_ID=" + ModuleId;
        if (dvChildModule.Count > 0)
        {
            for (int iLoop = 0; iLoop < dvChildModule.Count; iLoop++)
            {
                TreeNode tnn = new TreeNode(dvChildModule[iLoop]["CHILD_MDL_NAME"].ToString(), dvChildModule[iLoop]["CHILD_MDL_ID"].ToString());
                if (tn == null)
                    trvModule.Nodes.Add(tnn);
                else
                    tn.ChildNodes.Add(tnn);
            }
        }
    }

    protected void trvModule_SelectedNodeChanged(object sender, EventArgs e)
    {
        // SetChildNodeOfSelectedNode();
        GetNavigationDataTable();
        BindNavigation();
    }
    private void SetChildNodeOfSelectedNode()
    {
        try
        {
            if (trvModule.SelectedNode.Checked == true)
            {
                if (trvModule.SelectedNode.Parent != null)
                {
                    if (trvModule.SelectedNode.Parent.Checked != true)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.alert('Select Parent Module First');", true);

                        trvModule.SelectedNode.Checked = false;
                    }
                }
                foreach (TreeNode tn in trvModule.SelectedNode.ChildNodes)
                {
                    tn.Checked = true;
                }
            }
            else
            {
                foreach (TreeNode tn in trvModule.SelectedNode.ChildNodes)
                {
                    tn.Checked = false;
                }
            }
        }
        catch (Exception oex)
        {
            lblErrorMessage.Text = oex.Message;
        }
    }
    private void GetNavigationDataTable()
    {
        if (dtTempUserRightMaster == null)
        {
            SetTempDataTable();
        }
        dtTempUserRightMaster.Rows.Clear();
        FillFinalNavigationDataTable();

        if (trvModule.Nodes.Count > 0)
        {
            for (int iLoop = 0; iLoop < trvModule.Nodes.Count; iLoop++)
            {
                if (trvModule.Nodes[iLoop].Checked == true)
                {
                    int ModuleId = Convert.ToInt32(trvModule.Nodes[iLoop].Value.Trim());
                    if (trvModule.Nodes[iLoop].ChildNodes.Count > 0)
                    {
                        for (int jLoop = 0; jLoop < trvModule.Nodes[iLoop].ChildNodes.Count; jLoop++)
                        {
                            if (trvModule.Nodes[iLoop].ChildNodes[jLoop].Checked == true)
                            {
                                int ChildModuleId = Convert.ToInt32(trvModule.Nodes[iLoop].ChildNodes[jLoop].Value.Trim());
                                if (dsDatabase != null && dsDatabase.Tables["NavigationMaster"] != null && dsDatabase.Tables["NavigationMaster"].Rows.Count > 0)
                                {
                                    DataView dvNavigation = new DataView(dsDatabase.Tables["NavigationMaster"]);
                                    dvNavigation.RowFilter = "MDL_ID=" + ModuleId + " and CHILD_MDL_ID=" + ChildModuleId;
                                    if (dvNavigation.Count > 0)
                                    {
                                        for (int kLoop = 0; kLoop < dvNavigation.Count; kLoop++)
                                        {
                                            int NavigationId = Convert.ToInt32(dvNavigation[kLoop]["NAV_ID"].ToString());
                                            string NavigationName = dvNavigation[kLoop]["NAV_NAME"].ToString();
                                            string ModuleName = dvNavigation[kLoop]["MDL_NAME"].ToString();
                                            string ChildModuleName = dvNavigation[kLoop]["CHILD_MDL_NAME"].ToString();

                                            DataView dvDup = new DataView(dtTempUserRightMaster);
                                            dvDup.RowFilter = "MDL_ID=" + ModuleId + " and CHILD_MDL_ID=" + ChildModuleId + " and NAV_ID=" + NavigationId;
                                            if (dvDup.Count == 0)
                                            {
                                                DataRow dr = dtTempUserRightMaster.NewRow();

                                                dr["User_Code"] = txtUserName.Text.Trim();
                                                dr["MDL_ID"] = ModuleId;
                                                dr["CHILD_MDL_ID"] = ChildModuleId;
                                                dr["NAV_ID"] = NavigationId;
                                                dr["MDL_NAME"] = ModuleName;
                                                dr["CHILD_MDL_NAME"] = ChildModuleName;
                                                dr["NAV_NAME"] = NavigationName;
                                                dr["CREATE_RHT"] = true;
                                                dr["MDFY_RHT"] = true;
                                                dr["DEL_RHT"] = true;
                                                dr["VIEW_RHT"] = true;

                                                dtTempUserRightMaster.Rows.Add(dr);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void BindNavigation()
    {
        grdNavigation.DataSource = null;
        grdNavigation.DataBind();
        if (dtTempUserRightMaster != null && dtTempUserRightMaster.Rows.Count > 0)
        {
            grdNavigation.DataSource = dtTempUserRightMaster;
            grdNavigation.DataBind();
        }
    }

    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        if (txtUserName.Text != null && txtUserName.Text != "")
        {
            FillDataByUserCode(Common.CommonFuction.funFixQuotes(txtUserName.Text.Trim()));
            BindNavigation();
        }
    }

    private void FillDataByUserCode(string UserCode)
    {
        if (dtTempUserRightMaster == null)
        {
            SetTempDataTable();
        }
        dtTempUserRightMaster.Rows.Clear();
        UncheckAll();
        if (dsDatabase != null && dsDatabase.Tables["UserNavigationRight"] != null && dsDatabase.Tables["UserNavigationRight"].Rows.Count > 0)
        {
            DataView dv = new DataView(dsDatabase.Tables["UserNavigationRight"]);
            dv.RowFilter = "User_Code='" + UserCode + "'";
            if (dv.Count > 0)
            {
                foreach (DataRow dr in dv.ToTable().Rows)
                {
                    int iModuleId = Convert.ToInt32(dr["MDL_ID"].ToString());
                    int iChildModuleId = Convert.ToInt32(dr["CHILD_MDL_ID"].ToString());
                    CheckTreeByUser(iModuleId, iChildModuleId);

                    DataRow dr1 = dtTempUserRightMaster.NewRow();
                    dr1["User_Code"] = dr["User_Code"];
                    dr1["MDL_ID"] = dr["MDL_ID"];
                    dr1["CHILD_MDL_ID"] = dr["CHILD_MDL_ID"];
                    dr1["NAV_ID"] = dr["NAV_ID"];
                    if (dr["CREATE_RHT"].ToString() == "1")
                        dr1["CREATE_RHT"] = true;
                    else
                        dr1["CREATE_RHT"] = false;

                    if (dr["MDFY_RHT"].ToString() == "1")
                        dr1["MDFY_RHT"] = true;
                    else
                        dr1["MDFY_RHT"] = false;

                    if (dr["DEL_RHT"].ToString() == "1")
                        dr1["DEL_RHT"] = true;
                    else
                        dr1["DEL_RHT"] = false;

                    if (dr["VIEW_RHT"].ToString() == "1")
                        dr1["VIEW_RHT"] = true;
                    else
                        dr1["VIEW_RHT"] = false;

                    dr1["MDL_NAME"] = dr["MDL_NAME"];
                    dr1["CHILD_MDL_NAME"] = dr["CHILD_MDL_NAME"];
                    dr1["NAV_NAME"] = dr["NAV_NAME"];
                    dtTempUserRightMaster.Rows.Add(dr1);
                }
            }
        }
    }
    private void CheckTreeByUser(int ModuleId, int ChildModuleId)
    {
        if (trvModule.Nodes.Count > 0)
        {
            for (int iLoop = 0; iLoop < trvModule.Nodes.Count; iLoop++)
            {
                int iModuleId = Convert.ToInt32(trvModule.Nodes[iLoop].Value.Trim());
                if (iModuleId == ModuleId)
                {
                    trvModule.Nodes[iLoop].Checked = true;

                    if (trvModule.Nodes[iLoop].ChildNodes.Count > 0)
                    {
                        for (int jLoop = 0; jLoop < trvModule.Nodes[iLoop].ChildNodes.Count; jLoop++)
                        {
                            int iChildModuleId = Convert.ToInt32(trvModule.Nodes[iLoop].ChildNodes[jLoop].Value.Trim());
                            if (iChildModuleId == ChildModuleId)
                            {
                                trvModule.Nodes[iLoop].ChildNodes[jLoop].Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }
    private void UncheckAll()
    {
        if (trvModule.Nodes.Count > 0)
        {
            for (int iLoop = 0; iLoop < trvModule.Nodes.Count; iLoop++)
            {
                trvModule.Nodes[iLoop].Checked = false;
                if (trvModule.Nodes[iLoop].ChildNodes.Count > 0)
                {
                    for (int jLoop = 0; jLoop < trvModule.Nodes[iLoop].ChildNodes.Count; jLoop++)
                    {

                        trvModule.Nodes[iLoop].ChildNodes[jLoop].Checked = false;
                    }
                }
            }
        }
    }

    private void SetTempDataTable()
    {
        #region UserRightMaster Data Table
        dtTempUserRightMaster = new DataTable();
        dtTempUserRightMaster.Columns.Add("User_Code", typeof(string));
        dtTempUserRightMaster.Columns.Add("MDL_ID", typeof(int));
        dtTempUserRightMaster.Columns.Add("CHILD_MDL_ID", typeof(int));
        dtTempUserRightMaster.Columns.Add("NAV_ID", typeof(int));
        dtTempUserRightMaster.Columns.Add("MDL_NAME", typeof(string));
        dtTempUserRightMaster.Columns.Add("CHILD_MDL_NAME", typeof(string));
        dtTempUserRightMaster.Columns.Add("NAV_NAME", typeof(string));
        dtTempUserRightMaster.Columns.Add("CREATE_RHT", typeof(bool));
        dtTempUserRightMaster.Columns.Add("MDFY_RHT", typeof(bool));
        dtTempUserRightMaster.Columns.Add("DEL_RHT", typeof(bool));
        dtTempUserRightMaster.Columns.Add("VIEW_RHT", typeof(bool));

        #endregion
    }

    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        string sSuccess = "";
        string sFailure = "";
        try
        {
            if (Validated())
            {
                InsertData(out sSuccess, out sFailure);
                lblMessage.Text = sSuccess;
                lblErrorMessage.Text = sFailure;
                ResetMenu();
                Session["saveStatus"] = 1;
                //BlanksControls();
                Response.Redirect("~/Module/Admin/Pages/UserNavigationRight.aspx?sSuccess=" + sSuccess + sFailure, false);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = sSuccess;
            lblErrorMessage.Text = sFailure + ex.Message;
        }
    }

    private bool Validated()
    {
        return true;
    }

    private string InsertData(out string sSuccess, out string sFailure)
    {
        sSuccess = string.Empty;
        sFailure = string.Empty;
        try
        {
            bool bSuccess = false;

            string UserCode = txtUserName.Text.Trim();
            SaitexDM.Common.DataModel.UserAccessRight oUserAccessRight = new SaitexDM.Common.DataModel.UserAccessRight();
            oUserAccessRight.UserCode = UserCode;
            SaitexBL.Interface.Method.UserNavigationRight.DeleteUserAccessRight(oUserAccessRight, "");
            FillFinalNavigationDataTable();
            if (dtTempUserRightMaster.Rows.Count > 0 && dtTempUserRightMaster != null)
            {
                for (int iloop = 0; iloop < dtTempUserRightMaster.Rows.Count; iloop++)
                {
                    oUserAccessRight.ModuleId = Convert.ToInt32(dtTempUserRightMaster.Rows[iloop]["MDL_ID"].ToString());
                    oUserAccessRight.ChildModuleId = Convert.ToInt32(dtTempUserRightMaster.Rows[iloop]["CHILD_MDL_ID"].ToString());
                    oUserAccessRight.NavigationId = Convert.ToInt32(dtTempUserRightMaster.Rows[iloop]["NAV_ID"].ToString());
                    oUserAccessRight.CreateRight = Convert.ToBoolean(dtTempUserRightMaster.Rows[iloop]["CREATE_RHT"].ToString());
                    oUserAccessRight.ModifyRight = Convert.ToBoolean(dtTempUserRightMaster.Rows[iloop]["MDFY_RHT"].ToString());
                    oUserAccessRight.DeleteRight = Convert.ToBoolean(dtTempUserRightMaster.Rows[iloop]["DEL_RHT"].ToString());
                    oUserAccessRight.ViewRight = Convert.ToBoolean(dtTempUserRightMaster.Rows[iloop]["VIEW_RHT"].ToString());
                    string ModuleName = dtTempUserRightMaster.Rows[iloop]["MDL_NAME"].ToString();
                    string ChildModuleName = dtTempUserRightMaster.Rows[iloop]["CHILD_MDL_NAME"].ToString();
                    string NavigationName = dtTempUserRightMaster.Rows[iloop]["NAV_NAME"].ToString();
                    int iRecordFound = 0;
                    string InsertBy = "";
                    bSuccess = SaitexBL.Interface.Method.UserNavigationRight.InsertUserAccessRight(oUserAccessRight, out iRecordFound, InsertBy);
                    if (bSuccess)
                        sSuccess = sSuccess + "Record Saved for " + UserCode + " for " + ModuleName + "/" + ChildModuleName + "/" + NavigationName + " </ br>";

                    else
                        sFailure = sFailure + "Record Saving failed for " + UserCode + " for " + ModuleName + "/" + ChildModuleName + "/" + NavigationName + " </ br>";
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            return null;
            // throw ex;
        }
    }
    private void FillFinalNavigationDataTable()
    {
        if (dtTempUserRightMaster == null)
        {
            SetTempDataTable();
        }
        dtTempUserRightMaster.Rows.Clear();
        if (grdNavigation.Rows.Count > 0)
        {
            //if (grdNavigation.PageCount > 0)
            //{
            //for (int jLoop = 0; jLoop < grdNavigation.PageCount; jLoop++)
            //{
            //    grdNavigation.PageIndex = jLoop;
            for (int iLoop = 0; iLoop < grdNavigation.Rows.Count; iLoop++)
            {
                CheckBox chkRight = (CheckBox)grdNavigation.Rows[iLoop].FindControl("chkRight");
                if (chkRight.Checked)
                {
                    Label lblModuleId = (Label)grdNavigation.Rows[iLoop].FindControl("lblModuleId");
                    Label lblChildModuleId = (Label)grdNavigation.Rows[iLoop].FindControl("lblChildModuleId");
                    Label lblNavigationId = (Label)grdNavigation.Rows[iLoop].FindControl("lblNavigationId");
                    Label lblModuleName = (Label)grdNavigation.Rows[iLoop].FindControl("lblModuleName");
                    Label lblChildModuleName = (Label)grdNavigation.Rows[iLoop].FindControl("lblChildModuleName");
                    Label lblNavigationName = (Label)grdNavigation.Rows[iLoop].FindControl("lblNavigationName");
                    CheckBox chkCreateRight = (CheckBox)grdNavigation.Rows[iLoop].FindControl("chkCreateRight");
                    CheckBox chkModifyRight = (CheckBox)grdNavigation.Rows[iLoop].FindControl("chkModifyRight");
                    CheckBox chkDeleteRight = (CheckBox)grdNavigation.Rows[iLoop].FindControl("chkDeleteRight");
                    CheckBox chkViewRight = (CheckBox)grdNavigation.Rows[iLoop].FindControl("chkViewRight");
                    DataRow dr = dtTempUserRightMaster.NewRow();

                    dr["User_Code"] = txtUserName.Text.Trim();
                    dr["MDL_ID"] = Convert.ToInt32(lblModuleId.Text.Trim());
                    dr["CHILD_MDL_ID"] = Convert.ToInt32(lblChildModuleId.Text.Trim());
                    dr["NAV_ID"] = Convert.ToInt32(lblNavigationId.Text.Trim());
                    dr["MDL_NAME"] = lblModuleName.Text.Trim();
                    dr["CHILD_MDL_NAME"] = lblChildModuleName.Text.Trim();
                    dr["NAV_NAME"] = lblNavigationName.Text.Trim();
                    dr["CREATE_RHT"] = chkCreateRight.Checked;
                    dr["MDFY_RHT"] = chkModifyRight.Checked;
                    dr["DEL_RHT"] = chkDeleteRight.Checked;
                    dr["VIEW_RHT"] = chkViewRight.Checked;

                    dtTempUserRightMaster.Rows.Add(dr);
                }

            }
            //}
            //}
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void trvModule_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        SetChildNodeOfSelectedNode();
        GetNavigationDataTable();
        BindNavigation();
    }
    protected void btnCalcelCat_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sSuccess = "";
        string sFailure = "";
        try
        {
            if (Validated())
            {
                InsertData(out sSuccess, out sFailure);
                lblMessage.Text = sSuccess;
                lblErrorMessage.Text = sFailure;
                ResetMenu();

                Session["saveStatus"] = 1;

                Response.Redirect("~/Module/Admin/Pages/UserNavigationRight.aspx?sSuccess=" + sSuccess + sFailure, false);


            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = sSuccess;
            lblErrorMessage.Text = sFailure + ex.Message;
        }
    }

    private void ResetMenu()
    {
        try
        {
            MasterPage adminMaster = (MasterPage)this.Page.Master;

            UserControl GetUserMenu1 = (UserControl)adminMaster.FindControl("GetUserMenu1");
            HiddenField HiddenField1 = (HiddenField)GetUserMenu1.FindControl("HiddenField1");
            HiddenField1.Value = "1";
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdNavigation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //FillFinalNavigationDataTable();
        //lblMessage.Text = "";
        //lblErrorMessage.Text = "";
        //grdNavigation.PageIndex = e.NewPageIndex;
        //BindNavigation();
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {

        string sSuccess = "";
        string sFailure = "";
        try
        {
            if (Validated())
            {
                InsertData(out sSuccess, out sFailure);
                ResetMenu();

                //Session["saveStatus"] = 1;

                //Response.Redirect("~/Module/Admin/Pages/UserNavigationRight.aspx?sSuccess=" + sSuccess + sFailure, false);
                string smsg = sSuccess + sFailure;
                smsg = smsg.Replace("</ br>", "\\r\\n");

                // lblMessage.Text = Request.QueryString["sSuccess"].ToString();                   
                Common.CommonFuction.ShowMessage(smsg);

                txtUserName.Text = string.Empty;
                UncheckAll();

                dtTempUserRightMaster = null;
                grdNavigation.DataSource = null;
                grdNavigation.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = sSuccess;
            lblErrorMessage.Text = sFailure + ex.Message;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Session["saveStatus"] = 0;

        Response.Redirect("~/Module/Admin/Pages/UserNavigationRight.aspx", false);


    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void ddlUserMaster_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetUserData(e.Text.ToUpper());

            ddlUserMaster.Items.Clear();

            ddlUserMaster.DataSource = data;
            ddlUserMaster.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {

        }

    }

    private DataTable GetUserData(string Text)
    {
        try
        {
            string CommandText = "select * from v_cm_user_mst ";
            string WhereClause = "  where USER_CODE like :SearchQuery or USER_NAME like :SearchQuery or USER_LOG_ID like :SearchQuery or USER_TYPE like :SearchQuery";
            string SortExpression = " order by USER_CODE asc";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlUserMaster_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtUserName.Text = ddlUserMaster.SelectedValue.ToString();
            FillDataByUserCode(Common.CommonFuction.funFixQuotes(txtUserName.Text.Trim()));
            BindNavigation();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, "");
        }
    }
}
