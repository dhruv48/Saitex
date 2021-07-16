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
using Common;
using errorLog;
using Obout.ComboBox;
using System.Collections.Generic;

public partial class Module_Inventory_Controls_ITEM_QC_Master : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
    private DataTable dtDetailTBL = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (Session["urLoginId"] != null)
                {
                    if (!IsPostBack)
                    {
                        Initialisepage();
                    }
                }
                else
                {
                    Response.Redirect("~/default.aspx", false);
                }
            }
            else
            {
                Response.Redirect("~/default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page Loading.\r\nSee error log for detail."));
        }
    }

    private void Initialisepage()
    {
        BlanksControls();
        tdSave.Visible = true;
        tdUpdate.Visible = false;
        tdFind.Visible = true;
        lblMode.Text = "Save";
        ViewState["dtDetailTBL"] = null;
        grdMaterialItemReceipt.DataSource = null;
        grdMaterialItemReceipt.DataBind();
    }
    private void BlanksControls()
    {
        try
        {
            ddlItemCode.SelectedIndex = -1;
            txtRemarks.Text = string.Empty;
            txtSTD.Text = string.Empty;
            txtMaxValue.Text = string.Empty;
            txtMinValue.Text = string.Empty;
            txtTolerance.Text = string.Empty;
            bindItemCategory("");
            bindUOM("");
            bindSTD_Type("");
            bindTolerance_Range("");
            bindTolerance_type("");
            ddlStdType.SelectedIndex = -1;
            ddlUOM.SelectedIndex = -1;
            ddltolerancerange.SelectedIndex = -1;
            ddltoleranceType.SelectedIndex = -1;
            ddlItemCategory.SelectedIndex = -1;
            ViewState["UNIQUEID"] = null;
        }
        catch
        {
            throw;
        }

    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                bool bResult = SaitexBL.Interface.Method.ItemMaster.UpdateITEM_STANDARD_PARAMETER(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.UserCode, dtDetailTBL);
                if (bResult)
                {
                    Initialisepage();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Item QC Standard updated Successfully');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Data updation failed');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fill Atleast one Item QC');", true);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in saving.\r\nSee error log for detail."));
        }
    }


    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Item_QC_MasterReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=0,toolbar=0,menubar=0,location=100,scrollbars=1,resizable=1,width=800,height=500');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Item_QC_MasterList.aspx");
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Initialisepage();

    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        lblMode.Text = "Update";

        DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetITEM_STANDARD_PARAMETER(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["dtDetailTBL"] = dt;
            grdMaterialItemReceipt.DataSource = dt;
            grdMaterialItemReceipt.DataBind();
        }
    }



    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string iRecordFound = "";
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                bool bResult = SaitexBL.Interface.Method.ItemMaster.InsertITEM_STANDARD_PARAMETER(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.UserCode, dtDetailTBL, out iRecordFound);
                if (bResult)
                {
                    Initialisepage();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Item QC Standard Saved Successfully');", true);

                }
                else if (!string.IsNullOrEmpty(iRecordFound))
                {
                    iRecordFound += "Qc Standard of these items " + iRecordFound + " Already exist";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + iRecordFound + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fill Atleast one Item QC');", true);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in saving.\r\nSee error log for detail."));
        }

    }

    protected void ddlUOM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindUOM(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting UOM.\r\nSee error log for detail."));
        }
    }

    private void bindUOM(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "QC_UOM");
            ddlUOM.Items.Clear();
            ddlUOM.DataSource = dt;
            ddlUOM.DataTextField = "MST_CODE";
            ddlUOM.DataValueField = "MST_CODE";
            ddlUOM.DataBind();

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
            DataTable data = GetItems("", 0, ddlItemCategory.SelectedText.Trim());

            if (data != null && data.Rows.Count > 0)
            {
                ddlItemCode.Items.Clear();
                ddlItemCode.DataSource = data;
                ddlItemCode.DataBind();
            }


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
            DataTable dt = null;
            if (tdSave.Visible)
            {
                string CommandText = " SELECT   * FROM   (SELECT   * FROM  (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_TYPE, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM  as Combined FROM TX_ITEM_MST a WHERE   a.CAT_CODE='" + CAT_CODE + "' AND NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd WHERE   item_code NOT IN (SELECT   ITEM_CODE  FROM   TX_ITEM_STANDARD_PARAMETER m)) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery AND ROWNUM <= 15 ";
                string whereClause = string.Empty;

                if (startOffset != 0)
                {
                    whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT   * FROM   (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM as Combined   FROM TX_ITEM_MST a WHERE   a.CAT_CODE='" + CAT_CODE + "' AND NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd WHERE   item_code NOT IN (SELECT   ITEM_CODE  FROM   TX_ITEM_STANDARD_PARAMETER m)) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery AND ROWNUM <= " + startOffset + ")  ";
                }

                string SortExpression = " ORDER BY ITEM_CODE";
                string SearchQuery = "%" + text.ToUpper() + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            }
            else if (tdUpdate.Visible)
            {
                string CommandText = " SELECT   * FROM   (SELECT   * FROM  (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_TYPE, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM  as Combined FROM TX_ITEM_MST a WHERE   a.CAT_CODE='" + CAT_CODE + "' AND NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd WHERE   item_code  IN (SELECT   ITEM_CODE  FROM   TX_ITEM_STANDARD_PARAMETER m)) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery AND ROWNUM <= 15 ";
                string whereClause = string.Empty;

                if (startOffset != 0)
                {
                    whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT   * FROM   (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM as Combined   FROM TX_ITEM_MST a WHERE   a.CAT_CODE='" + CAT_CODE + "' AND NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd WHERE   item_code  IN (SELECT   ITEM_CODE  FROM   TX_ITEM_STANDARD_PARAMETER m)) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery AND ROWNUM <= " + startOffset + ")  ";
                }

                string SortExpression = " ORDER BY ITEM_CODE";
                string SearchQuery = "%" + text.ToUpper() + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            }
            return dt;

        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text, string CAT_CODE)
    {
        DataTable dt = null;
        if (tdSave.Visible)
        {
            string CommandText = " SELECT   * FROM   (SELECT   * FROM  (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_TYPE, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM  as Combined FROM TX_ITEM_MST a WHERE   a.CAT_CODE='" + CAT_CODE + "' AND NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd WHERE   item_code NOT IN (SELECT   ITEM_CODE  FROM   TX_ITEM_STANDARD_PARAMETER m)) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ITEM_CODE ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        }
        else if (tdUpdate.Visible)
        {
            string CommandText = " SELECT   * FROM   (SELECT   * FROM  (SELECT  a.ITEM_CODE, a.ITEM_DESC, a.ITEM_TYPE, a.ITEM_CODE||'@'|| a.ITEM_DESC||'@'||a.UOM  as Combined FROM TX_ITEM_MST a WHERE   a.CAT_CODE='" + CAT_CODE + "' AND NVL(a.QC_REQUIRED,0)='1'  ORDER BY a.ITEM_CODE) asd WHERE   item_code  IN (SELECT   ITEM_CODE  FROM   TX_ITEM_STANDARD_PARAMETER m)) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ITEM_CODE ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        }
        return dt.Rows.Count;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "window.confirm('Are you sure to save the record ?')");
        imgbtnUpdate.Attributes.Add("OnClick", "window.confirm('Are you sure to update the record ?')");
        imgbtnPrint.Attributes.Add("OnClick", "window.confirm('Are you sure to print the record ?')");
        imgbtnClear.Attributes.Add("OnClick", "window.confirm('Are you sure to clear the record ?')");
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

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {

            bool ReturnResult = false;

            int count = 0;
            int iCountAll = 0;
            msg = string.Empty;
            double Std_Value = 0, Tolerance = 0,Max_Value=0,Min_Value=0;
            iCountAll += 1;
            if (ddlItemCategory.SelectedIndex > -1)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Select Item Category.\r\n";
            }

            iCountAll += 1;
            if (ddlItemCode.SelectedIndex > -1)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Select Item.\r\n";
            }

            iCountAll += 1;
            if (txtSTD.Text != "" && double.TryParse(txtSTD.Text, out Std_Value))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Invalid Std Value.\r\n";
            }

            iCountAll += 1;
            if (ddlStdType.SelectedIndex > -1)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Select Std Type.\r\n";
            }
            iCountAll += 1;
            if (txtTolerance.Text != "" && double.TryParse(txtTolerance.Text, out Tolerance))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Invalid Tolerance.\r\n";
            }
            iCountAll += 1;
            if (ddltoleranceType.SelectedIndex > -1)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Select Tolerance type.\r\n";
            }
            iCountAll += 1;
            if (ddltolerancerange.SelectedIndex > -1)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Select Tolerance Range.\r\n";
            }

            iCountAll += 1;
            if (txtMaxValue.Text != "" && double.TryParse(txtMaxValue.Text, out Max_Value))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Enter Valid Maximum Value .\r\n";
            }

            iCountAll += 1;
            if (txtMinValue.Text != "" && double.TryParse(txtMinValue.Text, out Min_Value))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Enter Valid Minimum Value .\r\n";
            }



            if (count == iCountAll)
                ReturnResult = true;

            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            bool Istrue = false;
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                Istrue = true;
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();


            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                int.TryParse(ViewState["UNIQUEID"].ToString(), out UNIQUEID);

            double Std_Value = 0, Tolerance = 0, Max_Value = 0, Min_Value = 0;
            double.TryParse(txtSTD.Text.Trim(), out Std_Value);
            double.TryParse(txtMaxValue.Text.Trim(), out Max_Value);
            double.TryParse(txtMinValue.Text.Trim(), out Min_Value);
            double.TryParse(txtTolerance.Text.Trim(), out Tolerance);

            if (ddltolerancerange.SelectedItem.Text.ToUpper() == "MAXIMUM")
            {
                if (Min_Value > Max_Value)
                {
                    CommonFuction.ShowMessage("Minimum Value can not be greater than Maximum Value!!!");
                    return;
                }
            }
            else if (ddltolerancerange.SelectedItem.Text.ToUpper() == "MINIMUM")
            {
                if (Max_Value < Min_Value)
                {
                    CommonFuction.ShowMessage("Maximum Value can not be less than Minimum Value!!!");
                    return;
                }
            }


            if (Istrue)
            {
                bool bb = SearchItemCodeInGrid(ddlItemCategory.SelectedText.Trim(), ddlItemCode.SelectedValue, UNIQUEID);
                if (!bb)
                {

                    if (UNIQUEID > 0)
                    {
                        DataView dv = new DataView(dtDetailTBL);
                        dv.RowFilter = "TRN_NUMB=" + UNIQUEID;
                        if (dv.Count > 0)
                        {
                            dv[0]["ITEM_CATEGORY"] = ddlItemCategory.SelectedText;
                            dv[0]["ITEM_CODE"] = ddlItemCode.SelectedValue.Trim();
                            dv[0]["ITEM_DESC"] = ddlItemCode.SelectedText.Trim();
                            dv[0]["STD_VALUE"] = Std_Value;
                            dv[0]["UOM"] = ddlUOM.SelectedIndex > -1 ? ddlUOM.SelectedText.Trim() : "NA";
                            dv[0]["STD_TYPE"] = ddlStdType.SelectedItem.Text.Trim();
                            dv[0]["TOLERANCE"] = Tolerance;
                            dv[0]["TOLERANCE_TYPE"] = ddltoleranceType.SelectedItem.Text.Trim();
                            dv[0]["TOLERANCE_RANGE"] = ddltolerancerange.SelectedItem.Text.Trim();
                            dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                            dv[0]["MAX_VALUE"] = Max_Value;
                            dv[0]["MIN_VALUE"] = Min_Value;
                            dtDetailTBL.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtDetailTBL.NewRow();
                        dr["TRN_NUMB"] = dtDetailTBL.Rows.Count + 1;
                        dr["ITEM_CATEGORY"] = ddlItemCategory.SelectedText;
                        dr["ITEM_CODE"] = ddlItemCode.SelectedValue.Trim();
                        dr["ITEM_DESC"] = ddlItemCode.SelectedText.Trim();
                        dr["STD_VALUE"] = Std_Value;
                        dr["UOM"] = ddlUOM.SelectedIndex > -1 ? ddlUOM.SelectedText.Trim() : "NA";
                        dr["STD_TYPE"] = ddlStdType.SelectedItem.Text.Trim();
                        dr["TOLERANCE"] = Tolerance;
                        dr["TOLERANCE_TYPE"] = ddltoleranceType.SelectedItem.Text.Trim();
                        dr["TOLERANCE_RANGE"] = ddltolerancerange.SelectedItem.Text.Trim();
                        dr["REMARKS"] = txtRemarks.Text.Trim();
                        dr["MAX_VALUE"] = Max_Value;
                        dr["MIN_VALUE"] = Min_Value;

                        dtDetailTBL.Rows.Add(dr);
                    }
                    BlanksControls();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('This Item Code Already Added!!!');", true);
                }
            }

            ViewState["dtDetailTBL"] = dtDetailTBL;
            BindGridFromDataTable();

        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding detail.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BlanksControls();
    }

    private void CreateDataTable()
    {
        try
        {
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("TRN_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("ITEM_CATEGORY", typeof(string));
            dtDetailTBL.Columns.Add("ITEM_CODE", typeof(string));
            dtDetailTBL.Columns.Add("ITEM_DESC", typeof(string));
            dtDetailTBL.Columns.Add("STD_VALUE", typeof(double));
            dtDetailTBL.Columns.Add("STD_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("TOLERANCE", typeof(double));
            dtDetailTBL.Columns.Add("TOLERANCE_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("TOLERANCE_RANGE", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("MAX_VALUE", typeof(double));
            dtDetailTBL.Columns.Add("MIN_VALUE", typeof(double));

        }
        catch
        {
            throw;
        }
    }

    private void BindGridFromDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void grdMaterialItemReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUEID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItemReceiptRow(UNIQUEID);
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                row.BackColor = System.Drawing.Color.Green;
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItemReceiptRow(UNIQUEID);
                BindGridFromDataTable();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchItemCodeInGrid(string ItemCat, string ItemCode, int UNIQUEID)
    {
        bool Result = false;
        try
        {
            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    Label lblItemCategory = (Label)grdRow.FindControl("lblItemCategory");
                    Label lblItemCode = (Label)grdRow.FindControl("lblItemCode");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (lblItemCategory.Text.Trim() == ItemCat && lblItemCode.Text.Trim() == ItemCode && UNIQUEID != iUNIQUEID)
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

    private void EditItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "TRN_NUMB=" + UNIQUEID;
            if (dv.Count > 0)
            {
                bindItemCategory("");

                foreach (ComboBoxItem item in ddlItemCategory.Items)
                {
                    if (item.Text == dv[0]["ITEM_CATEGORY"].ToString())
                    {
                        ddlItemCategory.SelectedIndex = ddlItemCategory.Items.IndexOf(item);
                        break;
                    }
                }
                DataTable data = GetItems("", 0, dv[0]["ITEM_CATEGORY"].ToString());

                if (data != null && data.Rows.Count > 0)
                {
                    ddlItemCode.Items.Clear();
                    ddlItemCode.DataSource = data;
                    ddlItemCode.DataBind();
                }

                foreach (ComboBoxItem item in ddlItemCode.Items)
                {
                    if (item.Value == dv[0]["ITEM_CODE"].ToString())
                    {
                        ddlItemCode.SelectedIndex = ddlItemCode.Items.IndexOf(item);
                        break;
                    }
                }
                bindUOM("");

                foreach (ComboBoxItem item in ddlUOM.Items)
                {
                    if (item.Text == dv[0]["UOM"].ToString())
                    {
                        ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(item);
                        break;
                    }
                }


                bindSTD_Type("");
                ddlStdType.SelectedIndex = ddlStdType.Items.IndexOf(ddlStdType.Items.FindByText(dv[0]["STD_TYPE"].ToString()));

                bindTolerance_Range("");
                ddltolerancerange.SelectedIndex = ddltolerancerange.Items.IndexOf(ddltolerancerange.Items.FindByText(dv[0]["TOLERANCE_RANGE"].ToString()));

                bindTolerance_type("");
                ddltoleranceType.SelectedIndex = ddltoleranceType.Items.IndexOf(ddltoleranceType.Items.FindByText(dv[0]["TOLERANCE_TYPE"].ToString()));

                txtSTD.Text = dv[0]["STD_VALUE"].ToString();
                txtTolerance.Text = dv[0]["TOLERANCE"].ToString();
                txtRemarks.Text = dv[0]["REMARKS"].ToString();

                txtMaxValue.Text = dv[0]["MAX_VALUE"].ToString();
                txtMinValue.Text = dv[0]["MIN_VALUE"].ToString();


                ViewState["UNIQUEID"] = UNIQUEID;

            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["TRN_NUMB"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
                        dtDetailTBL.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["TRN_NUMB"] = iCount;
                }
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }


}
