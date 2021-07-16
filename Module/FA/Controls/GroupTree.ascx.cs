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

public partial class Module_FA_Controls_GroupTree : System.Web.UI.UserControl
{
    private static string TextBoxId;
    private static string GroupId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GroupId = string.Empty;
                trGroup.Visible = false;
                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                }

                if (Request.QueryString["GROUP_CODE"] != null && Request.QueryString["GROUP_CODE"].ToString() != "")
                {
                    GroupId = Request.QueryString["GROUP_CODE"].ToString();
                    trGroup.Visible = true;
                    bindGroupName(GroupId);
                }
                CreateDatatableForViewState();
                BindTreeView(null, GroupId);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            string sSelectedNode = TreeView1.SelectedNode.Value.Trim();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + sSelectedNode + "','" + TextBoxId + "')", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting TreeView..\r\nSee error log for detail."));
        }
    }

    private void CreateDatatableForViewState()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();
            ViewState["dtTree"] = dt;
        }
        catch
        {
            throw;
        }
    }

    protected void BindTreeView(TreeNode tn, string GroupId)
    {
        try
        {
            DataTable dtTree = (DataTable)ViewState["dtTree"];
            DataView dvTree = new DataView(dtTree);

            if (GroupId == "")
            {
                dvTree.RowFilter = "PARENT_CODE is null";
            }
            else
            {
                dvTree.RowFilter = "PARENT_CODE='" + GroupId + "'";
            }

            if (dvTree.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvTree.Count; iLoop++)
                {
                    string Group_Name = dvTree[iLoop]["GRP_NAME"].ToString();
                    string Group_Code = dvTree[iLoop]["GRP_CODE"].ToString();
                    string TreeValue = Group_Name + " [" + Group_Code + "]";

                    TreeNode tnn = new TreeNode(TreeValue, dvTree[iLoop]["GRP_CODE"].ToString());
                    BindTreeView(tnn, dvTree[iLoop]["GRP_CODE"].ToString());
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

    private void bindGroupName(string GroupId)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "GRP_CODE='" + GroupId + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        lblGroupCode.Text = dv[iLoop]["GRP_CODE"].ToString();
                        lblGroupName.Text = dv[iLoop]["GRP_NAME"].ToString();
                    }
                }
                else
                {
                    lblGroupCode.Text = "No Such record found..";
                    lblGroupName.Text = string.Empty;
                }
            }
        }
        catch
        {
            throw;
        }
    }
}