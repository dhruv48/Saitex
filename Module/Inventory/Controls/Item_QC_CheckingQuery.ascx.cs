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

public partial class Module_Inventory_Controls_Item_QC_CheckingQuery : System.Web.UI.UserControl
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }

    private void Initialisepage()
    {
        bindSTD_Type("");
        BlanksControls();
        bindQcGrid();
    }
    private void BlanksControls()
    {
        try
        {
            ddlItemCode.SelectedIndex = -1;
            ddlStdType.SelectedIndex = -1;
            ddlQCResult.SelectedIndex = -1;
            ddlMQCResult.SelectedIndex = -1;
            ddlstatus.SelectedIndex = 0;
        }
        catch
        {
            throw;
        }

    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Item_QC_Checking.aspx");
    }


    private DataTable bindQcGrid()
    {
        string Item_Code = "", Std_Type = "", QCResult = "", MQCResult = "", Status = "";

        if (ddlItemCode.SelectedIndex > -1)
        {
            Item_Code = ddlItemCode.SelectedValue.Trim();
        }
        if (ddlStdType.SelectedIndex > 0)
        {
            Std_Type = ddlStdType.SelectedValue.Trim();
        }
        if (ddlQCResult.SelectedIndex > 0)
        {
            QCResult = ddlQCResult.SelectedItem.Text.Trim();
        }
        if (ddlMQCResult.SelectedIndex > 0)
        {
            MQCResult = ddlMQCResult.SelectedItem.Text.Trim();
        }
        if (ddlstatus.SelectedItem.Text != "All")
        {
            Status = ddlstatus.SelectedItem.Text.Trim();
        }
        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetItemQC_Checking_Query(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Item_Code, Std_Type, QCResult, Status, MQCResult);
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
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            if (data != null && data.Rows.Count > 0)
            {
                ddlItemCode.Items.Clear();
                ddlItemCode.DataSource = data;
                ddlItemCode.DataBind();
            }

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItems(string text, int startOffset)
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

    protected int GetItemsCount(string text)
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
            string Item_Code = "", Std_Type = "", QCResult = "", MQCResult = "", Status = "";

            if (ddlItemCode.SelectedIndex > -1)
            {
                Item_Code = ddlItemCode.SelectedValue.Trim();
            }
            if (ddlStdType.SelectedIndex > 0)
            {
                Std_Type = ddlStdType.SelectedValue.Trim();
            }
            if (ddlQCResult.SelectedIndex > 0)
            {
                QCResult = ddlQCResult.SelectedItem.Text.Trim();
            }
            if (ddlMQCResult.SelectedIndex > 0)
            {
                MQCResult = ddlMQCResult.SelectedItem.Text.Trim();
            }
            if (ddlstatus.SelectedItem.Text != "All")
            {
                Status = ddlstatus.SelectedItem.Text.Trim();
            }
            string URL = "../Reports/Item_QC_CheckingReport.aspx?ITEM_CODE=" + Item_Code + "&STD_TYPE=" + Std_Type + "&QC_Rst=" + QCResult + "&QC_Apprv_Rst=" + MQCResult + "&STATUS=" + Status;
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
        string strFilename = "Item_QC_Checking_List_" + DateTime.Now.ToString() + ".xls";
        Common.CommonFuction.ExporttoExcel(bindQcGrid(), strFilename, "Item QC Checking List", oUserLoginDetail.VC_COMPANYNAME);

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
    protected void ddlQCResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
    protected void ddlMQCResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }
}