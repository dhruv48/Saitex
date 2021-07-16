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


public partial class Module_Admin_Controls_Designationlevel : System.Web.UI.UserControl
{
    string parendtid = "parent";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTreeView(null, parendtid);
           // bindGvGroupMaster();
        }
    }

    protected void BindTreeView(TreeNode tn, string PaerntId)
    {
        DataTable dtTree = SaitexBL.Interface.Method.CM_DESIGNATIONHIERARCHY_MST.GetDesigNationHierachy();

        DataView dvTree = new DataView(dtTree);
        dvTree.RowFilter = "SR_DESIG_CODE='" + PaerntId + "'";
        if (dvTree.Count > 0)
        {
            for (int iLoop = 0; iLoop < dvTree.Count; iLoop++)
            {
                TreeNode tnn = new TreeNode(dvTree[iLoop]["DESIG_NAME"].ToString(), dvTree[iLoop]["DESIG_CODE"].ToString());
                BindTreeView(tnn, dvTree[iLoop]["DESIG_CODE"].ToString());
                if (tn == null)
                    TreeView1.Nodes.Add(tnn);
                else
                    tn.ChildNodes.Add(tnn);
            }
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Help1/HTMLPage.htm";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);
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
                Response.Redirect("~/Admin/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
}
