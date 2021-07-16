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

public partial class Module_FA_Pages_GroupMasterView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CreateDatatableForViewState();
                BindTreeView(null, "");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void CreateDatatableForViewState()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.Fa_GetGroupMaster();
            ViewState["dtTree"] = dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindTreeView(TreeNode tn, string GroupId)
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
}