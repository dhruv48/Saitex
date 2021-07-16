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


public partial class Module_Admin_Controls_DesigNationHierarchy : System.Web.UI.UserControl
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
    //private void bindGvGroupMaster()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.CM_DESIGNATIONHIERARCHY_MST.GetDesigNationHierachy1();
    //        Grid1.DataSource = dt;
    //        Grid1.DataBind();

    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('" + ex.Message + "');", true);

    //    }
    //}
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Help1/HTMLPage.htm";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);


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
                string desig_Code = dvTree[iLoop]["DESIG_CODE"].ToString();
                string Desig_Name = GetEmployeeByDesignation(desig_Code, dvTree[iLoop]["DESIG_NAME"].ToString());
                
                TreeNode tnn = new TreeNode(Desig_Name, desig_Code);
                BindTreeView(tnn, dvTree[iLoop]["DESIG_CODE"].ToString());
                if (tn == null)
                    TreeView1.Nodes.Add(tnn);
                else
                    tn.ChildNodes.Add(tnn);
            }
        }
    }
    private string GetEmployeeByDesignation(string DESIG_CODE, string DESIG_NAME)
    {

        string retname = "";
        string returnString = "";

        try
        {

            DataTable dt = SaitexBL.Interface.Method.CM_DESIGNATIONHIERARCHY_MST.GetEmployeeByDesignation1(DESIG_CODE, DESIG_NAME);

            DataView dv = new DataView(dt);
            returnString = DESIG_NAME + "( ";
            if (dv.Count > 0)
            {
                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                {
                    retname = dv[iLoop]["EMPLOYEENAME"].ToString();
                    returnString +=" ' "+ retname + " ' ";
                }
            }
            returnString += ")";
            return returnString;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string treeselectedid = TreeView1.SelectedNode.Value.ToString();
        DataTable dt = SaitexBL.Interface.Method.CM_DESIGNATIONHIERARCHY_MST.GetDesignationEmpName();

        DataView dv = new DataView(dt);
        dv.RowFilter = "DESIG_CODE='" + treeselectedid + "'";
        if (dv != null && dv.Count > 0)
        {
            //Grid1.DataSource = dv;
            //Grid1.DataBind();

        }
        else
        {
            //Grid1.DataSource = null;

            //Grid1.DataBind();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Not Exist');", true);

        }

    }
   
}
