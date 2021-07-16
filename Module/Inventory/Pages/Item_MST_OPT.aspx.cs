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

public partial class Inventory_Item_MST_OPT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGetReport_Click(object sender, EventArgs e)
    {

        string QueryString = "";
        bool flag = false;

        if (ddlDepartment.SelectedValue.Trim() != "")
        {
            if (!flag)
                QueryString = QueryString + "?";
            else
                QueryString = QueryString + "&";
            QueryString = QueryString + "DEPARTMENT=" + ddlDepartment.SelectedValue.Trim();
            flag = true;
        }
        if (ddlItemCategory.SelectedValue.Trim() != "")
        {
            if (!flag)
                QueryString = QueryString + "?";
            else
                QueryString = QueryString + "&";
            QueryString = QueryString + "VC_CATEGORYCODE=" + ddlItemCategory.SelectedValue.Trim();
            flag = true;
        }

        string URL = "../Reports/ItemMasterReport.aspx" + QueryString;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

    }

    protected void ddlItemCategory_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindItemCategory(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
        }
    }
    private void bindItemCategory(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "ITEM_CATG");
            ddlItemCategory.Items.Clear();
            ddlItemCategory.DataSource = dt;
            ddlItemCategory.DataTextField = "MST_CODE";
            ddlItemCategory.DataValueField = "MST_CODE";
            ddlItemCategory.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable GET_MOM_DATA(string Text, string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            string CommandText = "select MST_CODE from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and MST_CODE like :SearchQuery";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, MST_NAME,oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlDepartment_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindDepartment(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
        }
    }
    private void bindDepartment(string SearchQuery)
    {
        try
        {
            string CommandText = "select * from ( select * from cm_dept_mst where Del_Status=0 ) asd ";
            string WhereClause = " where dept_name like :SearchQuery or dept_code like :SearchQuery";
            string SortExpression = " order by dept_code asc";
            SearchQuery = SearchQuery + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            ddlDepartment.Items.Clear();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "dept_name";
            ddlDepartment.DataValueField = "dept_code";
            ddlDepartment.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
