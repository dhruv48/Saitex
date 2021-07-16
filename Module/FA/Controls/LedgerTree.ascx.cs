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

public partial class Module_FA_Controls_LedgerTree : System.Web.UI.UserControl
{
    private static string TextBoxId;
    private static string LedgerId = string.Empty;
    private static DateTime StartDate;
    private static DateTime EndDate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();
                LedgerId = string.Empty;
                trLedger.Visible = false;
                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                }

                if (Request.QueryString["LEDGER_CODE"] != null && Request.QueryString["LEDGER_CODE"].ToString() != "")
                {
                    LedgerId = Request.QueryString["LEDGER_CODE"].ToString();
                    trLedger.Visible = true;
                }
                CreateDatatableForViewState();
                BindTreeView(null, LedgerId);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page loading..\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }
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
            string Account_Id = TreeView1.SelectedNode.Value.Trim();
            string AccountType = Account_Id.Remove(1);
            Account_Id = Account_Id.Remove(0, 1);
            string URL = string.Empty;
            if (Account_Id != "999999" && Account_Id != "999998")
            {
                if (AccountType == "L")
                {
                    string sSelectedNode = Account_Id;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + sSelectedNode + "','" + TextBoxId + "')", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Tree Selection..\r\nSee error log for detail."));
        }
    }

    private void CreateDatatableForViewState()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt = SaitexBL.Interface.Method.FA_Statement_Of_Account.get_Statement_Of_Account(oUserLoginDetail, StartDate, EndDate);
            ViewState["dtTree"] = dt;
            TreeView1.Nodes.Clear();
            BindTreeView(null, "G");
            if (TreeView1.Nodes.Count > 0)
            {
                TreeView1.CollapseAll();
            }
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
                    string Account_Name = dvTree[iLoop]["ACCOUNT_NAME"].ToString();
                    //string Amount = dvTree[iLoop]["AMOUNT"].ToString();
                    //bool IS_DEBIT = bool.Parse(dvTree[iLoop]["IS_DEBIT"].ToString());
                    string TreeValue = Account_Name; // +"( " + Amount + " , " + !IS_DEBIT + " )";

                    TreeNode tnn = new TreeNode(TreeValue, dvTree[iLoop]["ACCOUNT_ID"].ToString());
                    BindTreeView(tnn, dvTree[iLoop]["ACCOUNT_ID"].ToString());
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
}