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
using Common;
using errorLog;

public partial class Module_Inventory_Controls_Item_QC_Master_Query : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (!IsPostBack)
                {

                    Initialisepage();

                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page Loading.\r\nSee error log for detail."));
        }

    }

    private void Initialisepage()
    {
        bindItemCategory("");
        bindSTD_Type("");
        bindTolerance_Range("");
        bindTolerance_type("");
        BlanksControls();
        bindQcGrid();
    }
    private void BlanksControls()
    {
        try
        {
            ddlItemCode.SelectedIndex = -1;
            ddlStdType.SelectedIndex = -1;
            ddltolerancerange.SelectedIndex = -1;
            ddltoleranceType.SelectedIndex = -1;
            ddlItemCategory.SelectedIndex = -1;
            ddlstatus.SelectedIndex = 0;
        }
        catch
        {
            throw;
        }

    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/ITEM_QC_Master.aspx");
    }


    private DataTable bindQcGrid()
    {
        string Item_Category = "", Item_Code = "", Std_Type = "", Tolerance_Type = "", Tolerance_Range = "", Status = "";
        if (ddlItemCategory.SelectedIndex > -1)
        {
            Item_Category = ddlItemCategory.SelectedText.Trim();
        }
        if (ddlItemCode.SelectedIndex > -1)
        {
            Item_Code = ddlItemCode.SelectedValue.Trim();
        }
        if (ddlStdType.SelectedIndex > 0)
        {
            Std_Type = ddlStdType.SelectedValue.Trim();
        }
        if (ddltoleranceType.SelectedIndex > 0)
        {
            Tolerance_Type = ddltoleranceType.SelectedValue.Trim();
        }
        if (ddltolerancerange.SelectedIndex > 0)
        {
            Tolerance_Range = ddltolerancerange.SelectedValue.Trim();
        }
        if (ddlstatus.SelectedItem.Text != "All")
        {
            Status = ddlstatus.SelectedItem.Text;
        }
        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemQCStandard(Item_Category, Item_Code, Std_Type, Tolerance_Type, Tolerance_Range, Status);
        if (dt != null && dt.Rows.Count > 0)
        {
            Grid12.DataSource = dt;
            Grid12.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString();
        }
        else
        {
            Grid12.DataSource = dt;
            Grid12.DataBind();
            lblTotalRecord.Text = "0";
        }
        return dt;
    }



    private void bindSTD_Type(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "STD_TYPE");
            ddlStdType.Items.Clear();
            ddlStdType.DataSource = dt;
            ddlStdType.DataTextField = "MST_CODE";
            ddlStdType.DataValueField = "MST_CODE";
            ddlStdType.DataBind();
            ddlStdType.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        catch
        {
            throw;
        }
    }
    private void bindTolerance_type(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "TOLR_TYPE");
            ddltoleranceType.Items.Clear();
            ddltoleranceType.DataSource = dt;
            ddltoleranceType.DataTextField = "MST_CODE";
            ddltoleranceType.DataValueField = "MST_CODE";
            ddltoleranceType.DataBind();
            ddltoleranceType.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        catch
        {
            throw;
        }
    }

    private void bindTolerance_Range(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "TOLR_RANGE");
            ddltolerancerange.Items.Clear();
            ddltolerancerange.DataSource = dt;
            ddltolerancerange.DataTextField = "MST_CODE";
            ddltolerancerange.DataValueField = "MST_CODE";
            ddltolerancerange.DataBind();
            ddltolerancerange.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        catch
        {
            throw;
        }
    }


    private DataTable GET_MOM_DATA(string Text, string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select MST_CODE,MST_DESC from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and (UPPER(MST_CODE) like :SearchQuery OR UPPER(MST_DESC) like :SearchQuery)";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = "%" + Text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, MST_NAME, oUserLoginDetail.COMP_CODE);

        }
        catch
        {
            throw;
        }
    }



    protected void ddlItemCategory_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindItemCategory(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item Category.\r\nSee error log for detail."));
        }
    }



    protected void ddlItemCategory_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            bindQcGrid();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in item category selection.\r\nSee error log for detail."));
        }
    }


    protected void ddlItemCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            bindQcGrid();


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }

    protected void ddlItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset, ddlItemCategory.SelectedText.Trim());

            if (data != null && data.Rows.Count > 0)
            {
                ddlItemCode.Items.Clear();
                ddlItemCode.DataSource = data;
                ddlItemCode.DataBind();
            }

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetItemsCount(e.Text, "");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItems(string text, int startOffset, string CAT_CODE)
    {
        try
        {

            string CommandText = " SELECT   * FROM   (SELECT   * FROM  (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_TYPE, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM  as Combined FROM TX_ITEM_MST a WHERE NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd ) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery AND ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT   * FROM   (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM as Combined   FROM TX_ITEM_MST a WHERE  NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd ) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery AND ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = "%" + text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text, string CAT_CODE)
    {

        string CommandText = " SELECT   * FROM   (SELECT   * FROM  (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_TYPE, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM  as Combined FROM TX_ITEM_MST a WHERE NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd ) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY ITEM_CODE ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "window.confirm('Are you sure to print the record ?')");
        imgbtnExit.Attributes.Add("OnClick", "window.confirm('Are you sure to exit the record ?')");
    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in item category selection.\r\nSee error log for detail."));
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
            ddlItemCategory.DataValueField = "MST_DESC";
            ddlItemCategory.DataBind();

        }
        catch
        {
            throw;
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            string Item_Category = "", Item_Code = "", Std_Type = "", Tolerance_Type = "", Tolerance_Range = "", Status = "";
            if (ddlItemCategory.SelectedIndex > -1)
            {
                Item_Category = ddlItemCategory.SelectedText.Trim();
            }
            if (ddlItemCode.SelectedIndex > -1)
            {
                Item_Code = ddlItemCode.SelectedValue.Trim();
            }
            if (ddlStdType.SelectedIndex > 0)
            {
                Std_Type = ddlStdType.SelectedValue.Trim();
            }
            if (ddltoleranceType.SelectedIndex > 0)
            {
                Tolerance_Type = ddltoleranceType.SelectedValue.Trim();
            }
            if (ddltolerancerange.SelectedIndex > 0)
            {
                Tolerance_Range = ddltolerancerange.SelectedValue.Trim();
            }
            if (ddlstatus.SelectedItem.Text != "All")
            {
                Status = ddlstatus.SelectedItem.Text;
            }
            string URL = "../Reports/Item_QC_MasterReport.aspx?Cat=" + Item_Category + "&Item_Code=" + Item_Code + "&STD_TYPE=" + Std_Type + "&TOLERANCE_TYPE=" + Tolerance_Type + "&TOLERANCE_RANGE=" + Tolerance_Range + "&STATUS=" + Status;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void Grid12_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grid12.PageIndex = e.NewPageIndex;
            Grid12.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Item_Master_List_" + DateTime.Now.ToString() + ".xls";
        Common.CommonFuction.ExporttoExcel(bindQcGrid(), strFilename, "Item QC Standard Master List", oUserLoginDetail.VC_COMPANYNAME);

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initialisepage();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void ddlStdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
    protected void ddltoleranceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
    protected void ddltolerancerange_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
}
