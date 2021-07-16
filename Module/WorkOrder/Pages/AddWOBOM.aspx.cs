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

public partial class Module_WorkOrder_Pages_AddWOBOM : System.Web.UI.Page
{

    private static DataTable dtTRN_BOM_WO = null;

    private static string ARTICAL_CODE = string.Empty;
    private static string SHADE_CODE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                ARTICAL_CODE = string.Empty;
                SHADE_CODE = string.Empty;
                dtTRN_BOM_WO = null;
                bindBOMBASE_ARTICAL_TYPE();
                bindBOMUOM("UOM");
                BindShadeCode();

                if (Request.QueryString["ARTICAL_CODE"] != null)
                    ARTICAL_CODE = Request.QueryString["ARTICAL_CODE"].Trim();

                if (Request.QueryString["SHADE_CODE"] != null)
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].Trim();

                if (Session["dtWOBOM"] != null)
                {
                    if (dtTRN_BOM_WO == null)
                        CreateBOMTable();
                    dtTRN_BOM_WO = (DataTable)Session["dtWOBOM"];
                }

                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    public void bindBOMBASE_ARTICAL_TYPE()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetBaseArticleType();
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlBOMProductType.Items.Clear();
                ddlBOMProductType.DataSource = dt;
                ddlBOMProductType.DataTextField = "ARTICLE_TYPE";
                ddlBOMProductType.DataValueField = "ARTICLE_TYPE";
                ddlBOMProductType.DataBind();
                ddlBOMProductType.Items.Insert(0, new ListItem("--------Select-------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    public void bindBOMUOM(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlBOMUOM.Items.Clear();
                ddlBOMUOM.DataSource = dt;
                ddlBOMUOM.DataTextField = "MST_CODE";
                ddlBOMUOM.DataValueField = "MST_CODE";
                ddlBOMUOM.DataBind();
                ddlBOMUOM.Items.Insert(0, new ListItem("------Select------", "0"));
            }
            ddlBOMUOM.SelectedValue = "KGS";
        }
        catch
        {
            throw;
        }
    }
    private void BindShadeCode()
    {
        try
        {
            ddlshadeCode.Items.Clear();
            SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
            oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL(oOD_SHADE_FAMILY);
            
            if(dt != null && dt.Rows.Count > 0)
            {
            ddlshadeCode.DataSource = dt;
            ddlshadeCode.DataTextField = "SHADE_CODE";
            ddlshadeCode.DataValueField = "SHADE_CODE";
            ddlshadeCode.DataBind();
            }
            ddlshadeCode.SelectedValue = "GREY";
        }
        catch
        {
            throw;
        }
    }

    private void CreateBOMTable()
    {
        try
        {
            dtTRN_BOM_WO = new DataTable();
            dtTRN_BOM_WO.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_BOM_WO.Columns.Add("YEAR", typeof(int));
            dtTRN_BOM_WO.Columns.Add("ARTICLE_CODE", typeof(string));
            dtTRN_BOM_WO.Columns.Add("SHADE_CODE", typeof(string));
            dtTRN_BOM_WO.Columns.Add("BASE_ARTICLE_TYPE", typeof(string));
            dtTRN_BOM_WO.Columns.Add("BASE_ARTICLE_CODE", typeof(string));
            dtTRN_BOM_WO.Columns.Add("BASE_ARTICLE_DESC", typeof(string));           
            dtTRN_BOM_WO.Columns.Add("BASE_SHADE_CODE", typeof(string));
            dtTRN_BOM_WO.Columns.Add("UOM", typeof(string));
            dtTRN_BOM_WO.Columns.Add("QTY", typeof(string));
            dtTRN_BOM_WO.Columns.Add("SHRINKAGE", typeof(string));
            dtTRN_BOM_WO.Columns.Add("SPL_INSTRUCTION", typeof(string));
            dtTRN_BOM_WO.Columns.Add("REMARKS", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    private void BindBOMGrid()
    {
        try
        {
            if (dtTRN_BOM_WO == null)
                CreateBOMTable();

            DataView dv = new DataView(dtTRN_BOM_WO);
            dv.RowFilter = "ARTICLE_CODE='" + ARTICAL_CODE + "' and SHADE_CODE='" + SHADE_CODE + "' ";
            if (dv.Count > 0)
            {
                grdBOM.DataSource = dv;
                grdBOM.DataBind();
            }
            else
            {
                grdBOM.DataSource = null;
                grdBOM.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBOMProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindARTICAL_CODE();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in product type selection.See error log for detail."));
        }
    }

    protected void BindARTICAL_CODE()
    {
        try
        {
            DataTable dtallbasedetail = SaitexBL.Interface.Method.YRN_MST.GetALLBaseArticleDetail();
            DataView dvBOMArticle = new DataView(dtallbasedetail);
            dvBOMArticle.RowFilter = "ARTICLE_TYPE='" + ddlBOMProductType.SelectedItem.ToString() + "'";
            if (dvBOMArticle != null && dvBOMArticle.Count > 0)
            {
                txtBOMArticleCode.Items.Clear();
                txtBOMArticleCode.DataSource = dvBOMArticle;
                txtBOMArticleCode.DataValueField = "CODE";
                txtBOMArticleCode.DataTextField = "YARNDESC";
                txtBOMArticleCode.DataBind();
                txtBOMArticleCode.Items.Insert(0, new ListItem("Select", "Select"));

            }
            else
            {
                txtBOMArticleCode.Items.Clear();
                txtBOMArticleCode.Items.Insert(0, new ListItem("Select", "Select"));
            }
        }
        catch
        {
            throw;
        }

    }

    protected void BtnBOMSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (dtTRN_BOM_WO == null)
            {
                CreateBOMTable();
            }

            if (txtBOMRequiredQty.Text != "" && txtBOMArticleCode.SelectedValue != "Select")
            {
                int UNIQUE_ID = 0;
                if (ViewState["UNIQUE_ID"] != null)
                {
                    UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                }
                bool bb = SearchInBOMgrid(ddlBOMProductType.SelectedItem.ToString().Trim(), txtBOMArticleCode.SelectedValue, ddlshadeCode.SelectedValue.Trim(), UNIQUE_ID);
                if (!bb)
                {
                    if (UNIQUE_ID > 0)
                    {
                        DataView dv = new DataView(dtTRN_BOM_WO);
                        dv.RowFilter = "ARTICLE_CODE='" + ARTICAL_CODE + "' and SHADE_CODE='" + SHADE_CODE + "' and UNIQUE_ID=" + UNIQUE_ID;
                        if (dv.Count > 0)
                        {
                            dv[0]["BASE_ARTICLE_TYPE"] = ddlBOMProductType.SelectedItem.ToString().Trim();
                            dv[0]["BASE_ARTICLE_CODE"] = txtBOMArticleCode.SelectedValue.Trim();
                            dv[0]["BASE_ARTICLE_DESC"] = txtBOMArticleCode.SelectedItem.ToString();
                            dv[0]["BASE_SHADE_CODE"] = ddlshadeCode.SelectedItem.ToString().Trim();
                            dv[0]["UOM"] = ddlBOMUOM.SelectedItem.ToString();
                            dv[0]["QTY"] = txtBOMRequiredQty.Text;
                            dv[0]["SPL_INSTRUCTION"] = string.Empty;
                            dv[0]["REMARKS"] = string.Empty;
                            dv[0]["SHRINKAGE"] = string.Empty;

                            dtTRN_BOM_WO.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtTRN_BOM_WO.NewRow();
                        dr["UNIQUE_ID"] = dtTRN_BOM_WO.Rows.Count + 1;
                        dr["ARTICLE_CODE"] = ARTICAL_CODE;
                        dr["SHADE_CODE"] = SHADE_CODE;

                        dr["BASE_ARTICLE_TYPE"] = ddlBOMProductType.SelectedItem.ToString().Trim();
                        dr["BASE_ARTICLE_CODE"] = txtBOMArticleCode.SelectedValue.Trim();
                        dr["BASE_ARTICLE_DESC"] = txtBOMArticleCode.SelectedItem.ToString();
                        dr["BASE_SHADE_CODE"] = ddlshadeCode.SelectedItem.ToString().Trim();
                        dr["UOM"] = ddlBOMUOM.SelectedItem.ToString();
                        dr["QTY"] = txtBOMRequiredQty.Text;
                        dr["SPL_INSTRUCTION"] = string.Empty;
                        dr["REMARKS"] = string.Empty;
                        dr["SHRINKAGE"] = string.Empty;

                        dtTRN_BOM_WO.Rows.Add(dr);

                    }
                    RefresBOMRow();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected Product Type Already Added.Please Select Another');", true);
                }
                BindBOMGrid();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Enter Value Quantity");
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving BOM Detail Row.\r\nSee error log for detail."));

        }
    }

    private void RefresBOMRow()
    {
        try
        {
            ddlshadeCode.SelectedIndex = -1;
            ddlBOMProductType.SelectedIndex = -1;
            txtBOMArticleCode.SelectedIndex = -1;
            ddlBOMUOM.SelectedIndex = -1;
            txtBOMRequiredQty.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        RefresBOMRow();
    }

    protected void grdBOMArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "BOMEdit")
            {
                FillBOMByGrid(UNIQUE_ID);
            }
            else if (e.CommandName == "BOMDelete")
            {
                DeleteBOMRow(UNIQUE_ID);
                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in BOM Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }

    private void FillBOMByGrid(int UNIQUE_ID)
    {
        try
        {
            DataView dv = new DataView(dtTRN_BOM_WO);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                ddlshadeCode.SelectedValue = dv[0]["BASE_SHADE_CODE"].ToString();
                ddlBOMProductType.SelectedValue = dv[0]["BASE_ARTICLE_TYPE"].ToString();
                BindARTICAL_CODE();
                txtBOMArticleCode.SelectedValue = dv[0]["BASE_ARTICLE_CODE"].ToString();
                ddlBOMUOM.SelectedValue = dv[0]["UOM"].ToString();
                txtBOMRequiredQty.Text = dv[0]["QTY"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteBOMRow(int UNIQUE_ID)
    {
        try
        {
            if (dtTRN_BOM_WO.Rows.Count == 1)
            {
                dtTRN_BOM_WO.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRN_BOM_WO.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRN_BOM_WO.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRN_BOM_WO.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchInBOMgrid(string BASE_ARTICAL_TYPE, string BASE_ARTICAL_CODE, string BASE_SHADE_CODE, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdBOM.Rows)
            {
                Label txtBASE_SHADE_CODE = (Label)grdRow.FindControl("txtBASE_SHADE_CODE");
                Label txtBOMProductType = (Label)grdRow.FindControl("txtBOMProductType");
                Label txtBOMArticleCodeS = (Label)grdRow.FindControl("txtBOMArticleCode");
                Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtBASE_SHADE_CODE.Text.Trim() == BASE_SHADE_CODE && txtBOMProductType.Text.Trim() == BASE_ARTICAL_TYPE && txtBOMArticleCodeS.Text.Trim() == BASE_ARTICAL_CODE && UNIQUE_ID != iUNIQUE_ID)
                {
                    Result = true;
                }
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Session["dtWOBOM"] = dtTRN_BOM_WO;

            string BOM = string.Empty;
            string TextBoxBOM = string.Empty;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNSPIN_BOM('" + BOM + "','" + TextBoxBOM + "')", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }

}
