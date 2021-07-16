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
using Common;
using errorLog;

public partial class Module_Yarn_SalesWork_Reports_Yarn_Mst_Opt : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!Page.IsPostBack)
        {
            bindCategory("YARN_CAT");
            BindBranch();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            BindDepartment();
            BindRGB();
            BindYARNSHADE();
            BindCOATING();
            BindENDUSE();
            BindLOCATION();
            BindSTORE();
            BindISEXCISABLE();
        }
    }

    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        DataTable myDataTable = new DataTable();
        DataColumn myDataColumn;

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "YARN_CODE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "DEPT_CODE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "YARN_CAT";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "BRANCH_CODE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "RGB";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "SHADE";
        myDataTable.Columns.Add(myDataColumn);
        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "COATING";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "ENDUSE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "LOCATION";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "STORE";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "IS_EXCISABLE";
        myDataTable.Columns.Add(myDataColumn);




        DataRow row;

        row = myDataTable.NewRow();
        row["YARN_CODE"] = ddlyarncode.SelectedValue;//txtYarnCodeRpt.Text;
        
        //row["DEPT_CODE"] = txtDeptCodeRpt.Text;
        if (ddlDepartment.SelectedValue != "")
        {
            row["DEPT_CODE"] = ddlDepartment.SelectedValue;
        }
        else
        {
            row["DEPT_CODE"] = string.Empty; ;
        }
        //row["YARN_CAT"] = ddlYarnCat.SelectedValue;//txtCatCodeRpt.Text;
        if (ddlYarnCat.SelectedValue != "0")
        {
            row["YARN_CAT"] = ddlYarnCat.SelectedValue;
        }
        else
        {
            row["YARN_CAT"] = string.Empty; ;
        } 


        if (ddlBranch.SelectedValue != "0")
        {
            row["BRANCH_CODE"] = string.Empty;// ddlBranch.SelectedValue;
        }
        else
        {
            row["BRANCH_CODE"] = string.Empty; ; 
        }



        if (ddlRGB.SelectedValue != "0")
        {
            row["RGB"] = ddlRGB.SelectedValue;
        }
        else
        {
            row["RGB"] = string.Empty; ;
        }

        if (ddlYARNSHADE.SelectedValue != "0")
        {
            row["SHADE"] = ddlYARNSHADE.SelectedValue;
        }
        else
        {
            row["SHADE"] = string.Empty; ;
        }
        if (ddlCOATING.SelectedValue != "0")
        {
            row["COATING"] = ddlCOATING.SelectedValue;
        }
        else
        {
            row["COATING"] = string.Empty; ;
        }
        if (ddlENDUSE.SelectedValue != "0")
        {
            row["ENDUSE"] = ddlENDUSE.SelectedValue;
        }
        else
        {
            row["ENDUSE"] = string.Empty; ;
        }
        if (ddlLOCATION.SelectedValue != "0")
        {
            row["LOCATION"] = ddlLOCATION.SelectedValue;
        }
        else
        {
            row["LOCATION"] = string.Empty; ;
        }
        if (ddlSTORE.SelectedValue != "0")
        {
            row["STORE"] = ddlSTORE.SelectedValue;
        }
        else
        {
            row["STORE"] = string.Empty; ;
        }
        if (ddlISEXCISABLE.SelectedValue != "0")
        {
            row["IS_EXCISABLE"] = ddlISEXCISABLE.SelectedValue;
        }
        else
        {
            row["IS_EXCISABLE"] = string.Empty; ;
        }
       

        myDataTable.Rows.Add(row);
        Session["yarnreportdt"] = myDataTable;

        string URL = "../Reports/YarnMasterReport.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);


    }

    public void bindCategory(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlYarnCat.Items.Clear();
                ddlYarnCat.DataSource = dt;
                ddlYarnCat.DataTextField = "MST_DESC";
                ddlYarnCat.DataValueField = "MST_DESC";
                ddlYarnCat.DataBind();
                ddlYarnCat.Items.Insert(0, new ListItem("------Select------", "0"));

            }
        }
        catch
        {
            throw;
        }


    }

    private void BindYarnCodeinFindMode()
    {

        try
        {

            string CommandText = "select * from (select YARN_CODE,YARN_CAT,YARN_DESC, YARN_CAT         ||'---'|| YARN_DESC as Combined   from YRN_MST Where YARN_CAT <> 'SEWING THREAD')a";
            string WhereClause = "  where a.YARN_CODE like :SearchQuery  or a.YARN_DESC like :SearchQuery";
            string SortExpression = "  order by a.YARN_CAT  asc";
            string SearchQuery = "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    ddlyarncode.Items.Clear();
                    ddlyarncode.DataTextField = "Combined";
                    ddlyarncode.DataValueField = "YARN_CODE";
                    ddlyarncode.DataSource = data;
                    ddlyarncode.DataBind();
                    // ddlyarncode.Items.Insert(0, new ListItem("------Select------", "0"));
                }
            }


        }
        catch
        {

        }
    }

    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        BindYarnCodeinFindMode();
    }

    private void BindBranch()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            ddlBranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlBranch.DataSource = dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--------ALL---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {

            throw;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlyarncode.SelectedIndex = -1;
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        ddlYarnCat.SelectedValue="0";
        ddlDepartment.SelectedValue = "";

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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void BindDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindRGB()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.SelectRGB();
            ddlRGB.DataSource = dt;
            ddlRGB.DataValueField = "RGB";
            ddlRGB.DataTextField = "RGB";
            ddlRGB.DataBind();
            ddlRGB.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindYARNSHADE()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.SelectYarnShade();
            ddlYARNSHADE.DataSource = dt;
            ddlYARNSHADE.DataValueField = "SHADE";
            ddlYARNSHADE.DataTextField = "SHADE";
            ddlYARNSHADE.DataBind();
            ddlYARNSHADE.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindCOATING()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.SelectCOATING();
            ddlCOATING.DataSource = dt;
            ddlCOATING.DataValueField = "COATING";
            ddlCOATING.DataTextField = "COATING";
            ddlCOATING.DataBind();
            ddlCOATING.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindENDUSE()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.SelectENDUSE();
            ddlENDUSE.DataSource = dt;
            ddlENDUSE.DataValueField = "ENDUSE";
            ddlENDUSE.DataTextField = "ENDUSE";
            ddlENDUSE.DataBind();
            ddlENDUSE.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindLOCATION()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.SelectLOCATION();
            ddlLOCATION.DataSource = dt;
            ddlLOCATION.DataValueField = "LOCATION";
            ddlLOCATION.DataTextField = "LOCATION";
            ddlLOCATION.DataBind();
            ddlLOCATION.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindSTORE()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.SelectSTORE();
            ddlSTORE.DataSource = dt;
            ddlSTORE.DataValueField = "STORE";
            ddlSTORE.DataTextField = "STORE";
            ddlSTORE.DataBind();
            ddlSTORE.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindISEXCISABLE()
    {
        try
        {
            DataTable dt = new DataTable();
        dt = SaitexBL.Interface.Method.CM_DEPT_MST.SelectISEXCISABLE();
            ddlISEXCISABLE.DataSource = dt;
            ddlISEXCISABLE.DataValueField = "IS_EXCISABLE";
            ddlISEXCISABLE.DataTextField = "IS_EXCISABLE";
            ddlISEXCISABLE.DataBind();
            ddlISEXCISABLE.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
