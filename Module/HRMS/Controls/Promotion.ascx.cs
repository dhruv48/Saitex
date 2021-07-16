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
using System.IO;
using DBLibrary;


public partial class Module_HRMS_Controls_Promotion : System.Web.UI.UserControl
{

    private static string strCompanyCode = string.Empty;
    private static string strBranchCode = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        strCompanyCode = oUserLoginDetail.COMP_CODE;
        strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
        try
        {
            if (!Page.IsPostBack)
            {
                Initial_Control();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    


      
    }
    private void Initial_Control()
    {
       // fillYear();   
        try
        {
            getEmployeeDepartment();
            bindEmpCode();
            BindDesignation();
            bindddlBrachName();
        }
        catch
        {
            throw;
        }
       
    }
    private void getEmployeeDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            DDLDept.DataSource = dt;
            DDLDept.DataValueField = "DEPT_CODE";
            DDLDept.DataTextField = "DEPT_NAME";
            DDLDept.DataBind();
            DDLDept.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void BindDesignation()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            DDLDesign.DataSource = dt;
            DDLDesign.DataValueField = "desig_Code";
            DDLDesign.DataTextField = "desig_Name";
            DDLDesign.DataBind();
            DDLDesign.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
   //// private void fillYear()
   // {
   //     for (int i = -2; i < 15; i++)
   //     {
   //         DDLYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
   //     }
   //     DDLYear.Items.Insert(0, new ListItem("------------SELECT------------", ""));
   // }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE EMP_CODE like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0' or F_NAME like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY EMP_CODE";
            string commandText = "SELECT EMP_CODE,F_NAME, F_NAME ||' '||M_NAME||' '||L_NAME AS EMPLOYEENAME FROM HR_EMP_MST ";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', strCompanyCode, strBranchCode, sPO);
            return dt;

        }
        catch
        {
            throw;
        }
       
    }
    private void bindEmpCode()
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems("", 0, 10);
            cmbEmpCode.DataSource = data;
            cmbEmpCode.DataBind();
        }
        catch
        {
            throw;
        }
       
    }
    protected void cmbEmpCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            cmbEmpCode.Items.Clear();
            cmbEmpCode.DataSource = data;
            cmbEmpCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    


        
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM hr_emp_mst WHERE EMP_CODE like :SearchQuery And DEL_STATUS = '0' or F_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text + '%', strCompanyCode, strBranchCode, "");
        }
        catch
        {
            throw;
        }
       
    }
    //protected void CMDPrint_Click(object sender, EventArgs e)
    //{
    //    DataTable myDataTable = new DataTable();
    //    DataColumn myDataColumn;

    //    myDataColumn = new DataColumn();
    //    myDataColumn.DataType = Type.GetType("System.String");
    //    myDataColumn.ColumnName = "EMP_CODE";
    //    myDataTable.Columns.Add(myDataColumn);

    //    myDataColumn = new DataColumn();
    //    myDataColumn.DataType = Type.GetType("System.String");
    //    myDataColumn.ColumnName = "DEPT_CODE";
    //    myDataTable.Columns.Add(myDataColumn);

    //    myDataColumn = new DataColumn();
    //    myDataColumn.DataType = Type.GetType("System.String");
    //    myDataColumn.ColumnName = "DESIG_CODE";
    //    myDataTable.Columns.Add(myDataColumn);

    //    myDataColumn = new DataColumn();
    //    myDataColumn.DataType = Type.GetType("System.String");
    //    myDataColumn.ColumnName = "BRANCH_CODE";
    //    myDataTable.Columns.Add(myDataColumn);

    //    DataRow row;

    //    row = myDataTable.NewRow();
    //    row["EMP_CODE"] = cmbEmpCode.SelectedValue.ToString(); ;
    //    row["DEPT_CODE"] = DDLDept.SelectedValue.ToString();
    //    row["DESIG_CODE"] = DDLDesign.SelectedValue.ToString();
    //    row["BRANCH_CODE"] = DDLBranch.SelectedValue.ToString();
    //    myDataTable.Rows.Add(row);
    //    Session["promotiondt"] = myDataTable;

    //    string URL = "../Reports/Promotionreport.aspx";
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
    //}
    private void bindddlBrachName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            DDLBranch.DataSource = dt;
            DDLBranch.DataValueField = "BRANCH_CODE";
            DDLBranch.DataTextField = "BRANCH_NAME";
            DDLBranch.DataBind();
            DDLBranch.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }   
    protected void clear()
    {
        try
        {
            cmbEmpCode.SelectedIndex = -1;
            DDLBranch.SelectedIndex = -1;
            DDLDept.SelectedIndex = -1;
            DDLDesign.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
        
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    

      
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "EMP_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DEPT_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DESIG_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BRANCH_CODE";
            myDataTable.Columns.Add(myDataColumn);

            DataRow row;

            row = myDataTable.NewRow();
            row["EMP_CODE"] = cmbEmpCode.SelectedValue.ToString(); ;
            row["DEPT_CODE"] = DDLDept.SelectedValue.ToString();
            row["DESIG_CODE"] = DDLDesign.SelectedValue.ToString();
            row["BRANCH_CODE"] = DDLBranch.SelectedValue.ToString();
            myDataTable.Rows.Add(row);
            Session["promotiondt"] = myDataTable;

            string URL = "../Reports/Promotionreport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/HRMS/Pages/PromotionIncrement.aspx", false);
           
            } 
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }      
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }    
}
