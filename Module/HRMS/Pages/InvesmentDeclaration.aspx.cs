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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;

public partial class Module_HRMS_Pages_InvesmentDeclaration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitialisePage();
        }
    }
    private void InitialisePage()
    {
        try
        {           
            //RefreshDetailRow();
            //if (DTEmpTable == null || DTEmpTable.Rows.Count == 0)
            //    CreateEmpFamilyDetailTable();
            //DTEmpTable.Rows.Clear();
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
        }
    }
    protected DataTable GetItems(string strEMPName, int startOffset, int numberOfItems)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_EMP_MST.GetItems(strEMPName);
            return dt;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    protected void DDLEmployee_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        // Getting the items
        DataTable data = GetItems(e.Text.ToString().Trim(), Convert.ToInt32(e.ItemsOffset), 10);
        DDLEmployee.DataSource = data;
        DDLEmployee.DataBind();
        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    }
    protected int GetItemsCount(string text)
    {
        int Res = 0;
        Res = SaitexBL.Interface.Method.HR_EMP_MST.TotalCount(text);
        return Res;
    }
    protected void DDLEmployee_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        //InitialisePage();
        //getEmployeeFamilyData(Convert.ToString(DDLEmployee.SelectedValue.Trim()));
        //ViewState["iEMP_CODE"] = DDLEmployee.SelectedValue.Trim();
    }
}
