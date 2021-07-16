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

public partial class Module_Admin_Controls_UserAccessRightNew : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            ddlUser.OnTextChanged += new CommonControls_LOV_UserLOV.RefreshDropDownList(ddlUser_OnTextChanged);
            lblMessage.Text = "You are in Save Mode";
            lblErrorMessage.Text = "";
            ddlUser.AutoPostBack = true;
            if (!IsPostBack)
            {
                trvNav.Attributes.Add("onclick", "OnTreeClick(event)");          
                trvNav.CollapseAll();
                DataSet dsDatabase = GetData();
                BindModule(null, dsDatabase);
                trvNav.CollapseAll();
            }
        }
        catch (Exception oex)
        {
            lblErrorMessage.Text = oex.Message;
        }
    }

    private DataSet GetData()
    {
        try
        {
            return  SaitexBL.Interface.Method.UserNavigationRight.GetUserAccessRight();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindModule(TreeNode tn, DataSet dsDatabase)
    {
        try
        {
            DataView dvModule = new DataView(dsDatabase.Tables["ModuleMaster"]);
            if (dvModule.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvModule.Count; iLoop++)
                {
                    TreeNode tnn = new TreeNode(dvModule[iLoop]["MDL_NAME"].ToString(), dvModule[iLoop]["MDL_ID"].ToString());
                    BindChildModule(tnn, Convert.ToInt32(dvModule[iLoop]["MDL_ID"].ToString()), dsDatabase);

                    if (tn == null)
                        trvNav.Nodes.Add(tnn);
                    else
                        tn.ChildNodes.Add(tnn);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindChildModule(TreeNode tn, int ModuleId, DataSet dsDatabase)
    {
        try
        {
            DataView dvChildModule = new DataView(dsDatabase.Tables["ChildModuleMaster"]);
            dvChildModule.RowFilter = "MDL_ID=" + ModuleId;
            if (dvChildModule.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvChildModule.Count; iLoop++)
                {
                    TreeNode tnn = new TreeNode(dvChildModule[iLoop]["CHILD_MDL_NAME"].ToString(), dvChildModule[iLoop]["CHILD_MDL_ID"].ToString());
                    BindNavigation(tnn, ModuleId, Convert.ToInt32(dvChildModule[iLoop]["CHILD_MDL_ID"].ToString()), dsDatabase);

                    if (tn == null)
                        trvNav.Nodes.Add(tnn);
                    else
                        tn.ChildNodes.Add(tnn);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindNavigation(TreeNode tn, int ModuleId, int ChildModuleId, DataSet dsDatabase)
    {
        try
        {
            DataView dvNav = new DataView(dsDatabase.Tables["NavigationMaster"]);
            dvNav.RowFilter = "MDL_ID=" + ModuleId + " and CHILD_MDL_ID=" + ChildModuleId;
            if (dvNav.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvNav.Count; iLoop++)
                {
                    TreeNode tnn = new TreeNode(dvNav[iLoop]["NAV_NAME"].ToString(), dvNav[iLoop]["NAV_ID"].ToString());
                    if (tn == null)
                        trvNav.Nodes.Add(tnn);
                    else
                        tn.ChildNodes.Add(tnn);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void ddlUser_OnTextChanged(string Value, string Text)
    {
        try
        {
            FillDataByUserCode(Common.CommonFuction.funFixQuotes(ddlUser.SelectedValue.Trim()));
        
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in User Selection.\r\nSee error log for detail."));
        }
    }

    private void FillDataByUserCode(string UserCode)
    {
        try
        {
            UncheckAll();
            SaitexDM.Common.DataModel.UserAccessRight oUserAccessRight = new SaitexDM.Common.DataModel.UserAccessRight();
            oUserAccessRight.UserCode = ddlUser.SelectedValue.Trim();
            DataTable dtUserNav = SaitexBL.Interface.Method.UserNavigationRight.GetUserNavigationRightByUserCode(oUserAccessRight);
            if (dtUserNav != null && dtUserNav.Rows.Count > 0)
            {
                DataView dv = new DataView(dtUserNav);
                dv.RowFilter = "User_Code='" + UserCode + "'";
                if (dv.Count > 0)
                {
                    foreach (DataRow dr in dv.ToTable().Rows)
                    {
                        int iModuleId = Convert.ToInt32(dr["MDL_ID"].ToString());
                        int iChildModuleId = Convert.ToInt32(dr["CHILD_MDL_ID"].ToString());
                        int iNavId = Convert.ToInt32(dr["NAV_ID"].ToString());
                        CheckTreeByUser(iModuleId, iChildModuleId, iNavId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
  
    private void UncheckAll()
    {
        try
        {
            if (trvNav.Nodes.Count > 0)
            {
                for (int iLoop = 0; iLoop < trvNav.Nodes.Count; iLoop++)
                {
                    trvNav.Nodes[iLoop].Checked = false;
                    if (trvNav.Nodes[iLoop].ChildNodes.Count > 0)
                    {
                        for (int jLoop = 0; jLoop < trvNav.Nodes[iLoop].ChildNodes.Count; jLoop++)
                        {
                            trvNav.Nodes[iLoop].ChildNodes[jLoop].Checked = false;
                            if (trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes.Count > 0)
                            {
                                for (int kLoop = 0; kLoop < trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes.Count; kLoop++)
                                {
                                    trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes[kLoop].Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void CheckTreeByUser(int ModuleId, int ChildModuleId, int iNavId)
    {
        try
        {
            if (trvNav.Nodes.Count > 0)
            {
                for (int iLoop = 0; iLoop < trvNav.Nodes.Count; iLoop++)
                {
                    int iModuleId = Convert.ToInt32(trvNav.Nodes[iLoop].Value.Trim());
                    if (iModuleId == ModuleId)
                    {
                        trvNav.Nodes[iLoop].Checked = true;

                        if (trvNav.Nodes[iLoop].ChildNodes.Count > 0)
                        {
                            for (int jLoop = 0; jLoop < trvNav.Nodes[iLoop].ChildNodes.Count; jLoop++)
                            {
                                int iChildModuleId = Convert.ToInt32(trvNav.Nodes[iLoop].ChildNodes[jLoop].Value.Trim());
                                if (iChildModuleId == ChildModuleId)
                                {
                                    trvNav.Nodes[iLoop].ChildNodes[jLoop].Checked = true;

                                    if (trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes.Count > 0)
                                    {
                                        for (int kLoop = 0; kLoop < trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes.Count; kLoop++)
                                        {
                                            int iNavigationId = Convert.ToInt32(trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes[kLoop].Value.Trim());
                                            if (iNavigationId == iNavId)
                                            {
                                                trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes[kLoop].Checked = true;
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
        catch  (Exception ex)
        { 
            throw ex; 
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
                string smsg = sSuccess + sFailure;
                smsg = smsg.Replace("</ br>", "\\r\\n");
                Common.CommonFuction.ShowMessage(smsg);
                ddlUser.SelectedIndex = -1;
                UncheckAll();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = sSuccess;
            lblErrorMessage.Text = sFailure + ex.Message;
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for datail."));
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
            string UserCode = ddlUser.SelectedValue.Trim();
            SaitexDM.Common.DataModel.UserAccessRight oUserAccessRight = new SaitexDM.Common.DataModel.UserAccessRight();
            oUserAccessRight.UserCode = UserCode;
            SaitexBL.Interface.Method.UserNavigationRight.DeleteUserAccessRight(oUserAccessRight, "");
            DataTable DtNav = DtNav1();
            if (trvNav.Nodes.Count > 0)
            {
                for (int iLoop = 0; iLoop < trvNav.Nodes.Count; iLoop++)
                {
                    if (trvNav.Nodes[iLoop].Checked)
                    {
                        int iModuleId = Convert.ToInt32(trvNav.Nodes[iLoop].Value.Trim());
                        string ModuleName = trvNav.Nodes[iLoop].Text.Trim();
                        if (trvNav.Nodes[iLoop].ChildNodes.Count > 0)
                        {
                            for (int jLoop = 0; jLoop < trvNav.Nodes[iLoop].ChildNodes.Count; jLoop++)
                            {
                                if (trvNav.Nodes[iLoop].ChildNodes[jLoop].Checked)
                                {
                                    int iChildModuleId = Convert.ToInt32(trvNav.Nodes[iLoop].ChildNodes[jLoop].Value.Trim());
                                    string ChildModuleName = trvNav.Nodes[iLoop].ChildNodes[jLoop].Text.Trim();
                                    DtNav.Rows.Clear();
                                    if (trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes.Count > 0)
                                    {
                                        string NavigationName = string.Empty;
                                        for (int k = 0; k < trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes.Count; k++)
                                        {
                                            if (trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes[k].Checked)
                                            {
                                                int iNavId = Convert.ToInt32(trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes[k].Value.Trim());
                                                NavigationName = trvNav.Nodes[iLoop].ChildNodes[jLoop].ChildNodes[k].Text.Trim();

                                                DataRow dr = DtNav.NewRow();
                                                dr["ModuleId"] = iModuleId;
                                                dr["ChildModuleId"] = iChildModuleId;
                                                dr["NavigationId"] = iNavId;
                                                dr["CreateRight"] = true;
                                                dr["ModifyRight"] = true;
                                                dr["DeleteRight"] = true;
                                                dr["ViewRight"] = true;
                                                DtNav.Rows.Add(dr);
                                            }
                                        }
                                        int iRecordFound = 0;
                                        string InsertBy = "";
                                        bSuccess = SaitexBL.Interface.Method.UserNavigationRight.InsertUserAccessRight1(DtNav, oUserAccessRight, out iRecordFound, InsertBy);
                                        if (bSuccess)
                                            sSuccess = sSuccess + "Record Saved for " + UserCode + " for " + ModuleName + "/" + ChildModuleName + "/" + NavigationName + " </ br>";


                                        else
                                            sFailure = sFailure + "Record Saving failed for " + UserCode + " for " + ModuleName + "/" + ChildModuleName + "/" + NavigationName + " </ br>";

                                    }
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable DtNav1()
    {
        DataTable DtNav = new DataTable();
        DtNav.Columns.Add("ModuleId", typeof(int));
        DtNav.Columns.Add("ChildModuleId", typeof(int));
        DtNav.Columns.Add("NavigationId", typeof(int));
        DtNav.Columns.Add("CreateRight", typeof(bool));
        DtNav.Columns.Add("ModifyRight", typeof(bool));
        DtNav.Columns.Add("DeleteRight", typeof(bool));
        DtNav.Columns.Add("ViewRight", typeof(bool));
        return DtNav;
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
        catch  (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {

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
            errorLog.ErrHandler.LogError(ex,"");
        }
    }

    protected void imgbtnHelp_Click1(object sender, ImageClickEventArgs e)
    {

    }

    protected void trvNav_SelectedNodeChanged(object sender, EventArgs e)
    {
       int ii= trvNav.CheckedNodes.Count;
    }
    protected void trvNav_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
       string name= e.Node.Text;
    }
}
