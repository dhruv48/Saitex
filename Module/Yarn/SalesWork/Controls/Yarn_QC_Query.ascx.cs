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

public partial class Module_Yarn_SalesWork_Controls_Yarn_QC_Query : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                try
                {
                    Initialisepage();
                }
                catch (Exception ex)
                {
                    errorLog.ErrHandler.WriteError(ex.Message);
                }
            }
        }
        else
        {
            Response.Redirect("~/default.aspx", false);
        }

    }

    private void Initialisepage()
    {
        bindINWARDTYPE("");
        bindCategory("YARN_CAT");
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
            ddlyarncode.SelectedIndex = -1;
            ddlStdType.SelectedIndex = -1;
            ddltolerancerange.SelectedIndex = -1;
            ddltoleranceType.SelectedIndex = -1;
            ddlYarnCat.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            ddlInwardType.SelectedIndex = 0;
        }
        catch
        {
            throw;
        }

    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_QC_Master.aspx");
    }


    private DataTable bindQcGrid()
    {
        string Inward_Type = "", Yarn_Category = "", Yarn_Code = "", Std_Type = "", Tolerance_Type = "", Tolerance_Range = "", Status = "";
        if (ddlInwardType.SelectedIndex > 0)
        {
            Inward_Type = ddlInwardType.SelectedValue.Trim();
        }
        if (ddlYarnCat.SelectedIndex > 0)
        {
            Yarn_Category = ddlYarnCat.SelectedValue.Trim();
        }
        if (ddlyarncode.SelectedIndex > -1)
        {
            Yarn_Code = ddlyarncode.SelectedText.Trim();
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
        DataTable dt = SaitexBL.Interface.Method.YARN_STANDARD_PARAMETER_MST.GetYQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Inward_Type, Yarn_Category, Yarn_Code, Std_Type, Tolerance_Type, Tolerance_Range, Status);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvMaterialReceiptApproval.DataSource = dt;
            gvMaterialReceiptApproval.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString();
        }
        else
        {
            gvMaterialReceiptApproval.DataSource = dt;
            gvMaterialReceiptApproval.DataBind();
            lblTotalRecord.Text = "0";
        }
        return dt;
    }



    private void bindINWARDTYPE(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "INWARDTYPE");
            ddlInwardType.Items.Clear();
            ddlInwardType.DataSource = dt;
            ddlInwardType.DataTextField = "MST_CODE";
            ddlInwardType.DataValueField = "MST_CODE";
            ddlInwardType.DataBind();
            ddlInwardType.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        catch
        {
            throw;
        }
    }

    private void bindSTD_Type(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "Y_STD_TYPE");
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

    protected void ddlInwardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQcGrid();
    }

    protected void ddlYarnCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable data = GetYarnData("", 0, ddlYarnCat.SelectedValue);
            ddlyarncode.Items.Clear();
            ddlyarncode.DataSource = data;
            ddlyarncode.DataBind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn Category selection.\r\nSee error log for detail."));
        }
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

    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {

        try
        {    
            DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset, ddlYarnCat.SelectedValue);
            ddlyarncode.Items.Clear();
            ddlyarncode.DataSource = data;
            ddlyarncode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text, ddlYarnCat.SelectedValue);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn Code selection.\r\nSee error log for detail."));

        }

    }


    private DataTable GetYarnData(string Text, int startOffset, string Yarn_Category)
    {
        try
        {
            string CommandText = " SELECT   * FROM   (  SELECT   * FROM   (SELECT   * FROM   (SELECT   YARN_CODE, YARN_CAT, YARN_DESC, Y_COUNT FROM   YRN_MST WHERE   YARN_CAT = '" + Yarn_Category + "' AND NVL (QC_REQUIRED, 0) = '1') WHERE   YARN_CODE IN (SELECT   YARN_CODE FROM   YARN_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (YARN_CODE) LIKE :SearchQuery OR UPPER (YARN_DESC) LIKE :SearchQuery) ORDER BY   YARN_CODE) WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN ( select YARN_CODE from ( SELECT   YARN_CODE FROM   (SELECT   * FROM   (SELECT   YARN_CODE, YARN_CAT, YARN_DESC, Y_COUNT FROM   YRN_MST WHERE   YARN_CAT = '" + Yarn_Category + "' AND NVL (QC_REQUIRED, 0) = '1') WHERE   YARN_CODE  IN  (SELECT   YARN_CODE FROM   YARN_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (YARN_CODE) LIKE :SearchQuery OR UPPER (YARN_DESC) LIKE :SearchQuery) ORDER BY   YARN_CODE)    WHERE  ROWNUM <= " + startOffset + " ) ";
            }

            string SortExpression = " ORDER BY YARN_CODE";
            string SearchQuery = "%" + Text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text, string Yarn_Category)
    {

        string CommandText = " SELECT   * FROM   (  SELECT   * FROM   (SELECT   * FROM   (SELECT   YARN_CODE, YARN_CAT, YARN_DESC, Y_COUNT FROM   YRN_MST WHERE   YARN_CAT = '" + Yarn_Category + "' AND NVL (QC_REQUIRED, 0) = '1') WHERE   YARN_CODE IN (SELECT   YARN_CODE FROM   YARN_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (YARN_CODE) LIKE :SearchQuery OR UPPER (YARN_DESC) LIKE :SearchQuery) ORDER BY   YARN_CODE) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
    }


    protected void ddlyarncode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            bindQcGrid();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

        }

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
            string Inward_Type = "", Yarn_Category = "", Yarn_Code = "", Std_Type = "", Tolerance_Type = "", Tolerance_Range = "", Status = "";
            if (ddlInwardType.SelectedIndex > 0)
            {
                Inward_Type = ddlInwardType.SelectedValue.Trim();
            }
            if (ddlYarnCat.SelectedIndex > 0)
            {
                Yarn_Category = ddlYarnCat.SelectedValue.Trim();
            }
            if (ddlyarncode.SelectedIndex > -1)
            {
                Yarn_Code = ddlyarncode.SelectedText.Trim();
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
            string URL = "../Reports/Yarn_QC_Report.aspx?Inward_Type=" + Inward_Type + "&YCat=" + Yarn_Category + "&Y_Code=" + Yarn_Code + "&STD_TYPE=" + Std_Type + "&TOLERANCE_TYPE=" + Tolerance_Type + "&TOLERANCE_RANGE=" + Tolerance_Range + "&STATUS=" + Status;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void gvMaterialReceiptApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMaterialReceiptApproval.PageIndex = e.NewPageIndex;
            gvMaterialReceiptApproval.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Yarn_QC_Master_List_" + DateTime.Now.ToString() + ".xls";
        Common.CommonFuction.ExporttoExcel(bindQcGrid(), strFilename, "Yarn QC Standard Master List", oUserLoginDetail.VC_COMPANYNAME);

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initialisepage();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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

