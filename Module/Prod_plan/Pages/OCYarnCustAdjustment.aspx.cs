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
using Obout.ComboBox;
using errorLog;
using DBLibrary;
using Common;

public partial class Module_Prod_plan_Pages_OCYarnCustAdjustment : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

    private DataTable dtTRN_SUB = null;

    private static string COMP_CODE;
    private static string BRANCH_CODE;
    private static string txtQTY;
    private double lblMaxQTY = 0;
    private static string PRODUCT_TYPE;
    private static string SHADE_FAMILY;
    private static string SHADE_CODE;
    private static string YARN_CODE;
    private static string PI_TYPE;
    private static string BUSINESS_TYPE;
    private static string CR_ORDER_CAT;
    private static string ORDER_TYPE;
    private static string MAC_TRN_NUMB;
    private static string ORDER_NO;
    private static string CR_ST_ORDER_NO;
    private static int YEAR;
    private static string ARTICAL_CODE;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {

                if (Request.QueryString["COMP_CODE"] != null)
                {
                    COMP_CODE = Request.QueryString["COMP_CODE"].Trim();
                }
                if (Request.QueryString["BRANCH_CODE"] != null)
                {
                    BRANCH_CODE = Request.QueryString["BRANCH_CODE"].Trim();
                }
                if (Request.QueryString["TRN_NUMB"] != null)
                {
                    MAC_TRN_NUMB = Request.QueryString["TRN_NUMB"].Trim();
                }
                if (Request.QueryString["PI_TYPE"] != null)
                {
                    PI_TYPE = Request.QueryString["PI_TYPE"].ToString();
                }
                if (Request.QueryString["PRODUCT_TYPE"] != null)
                {
                    PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
                }
                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString().Trim();
                    txtShadeCode.Text = SHADE_CODE;
                }
                if (Request.QueryString["SHADE_FAMILY"] != null)
                {
                    SHADE_FAMILY = Request.QueryString["SHADE_FAMILY"].ToString().Trim();
                    txtShadeFamily.Text = SHADE_FAMILY;
                }
                if (Request.QueryString["txtQTY"] != null)
                {
                    txtQTY = Request.QueryString["txtQTY"].ToString();
                }
                if (Request.QueryString["lblMaxQTY"] != null)
                {
                    lblMaxQTY = double.Parse(Request.QueryString["lblMaxQTY"].Trim());
                }
                ViewState["lblMaxQTY"] = lblMaxQTY;

                lblAssQty.Text = lblMaxQTY.ToString();

                if (Request.QueryString["BUSINESS_TYPE"] != null)
                {
                    BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].ToString();
                }
                if (Request.QueryString["ORDER_CAT"] != null)
                {
                    CR_ORDER_CAT = Request.QueryString["ORDER_CAT"].ToString();
                }
                if (Request.QueryString["ORDER_TYPE"] != null)
                {
                    ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
                }
                if (Request.QueryString["ARTICAL_CODE"] != null)
                {
                    ARTICAL_CODE = Request.QueryString["ARTICAL_CODE"].ToString();
                }

                if (Request.QueryString["ORDER_NO"] != null)
                {
                    ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
                    CR_ST_ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
                }
                if (Request.QueryString["YEAR"] != null)
                {
                    int year = 0;
                    int.TryParse(Request.QueryString["YEAR"].ToString(), out year);
                    //YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    YEAR = year;
                }
                if (Request.QueryString["YARN_CODE"] != null)
                {
                    YARN_CODE = Request.QueryString["YARN_CODE"].Trim();
                    lblQualityCode.Text = YARN_CODE;
                }
                if (!IsPostBack)
                {
                    if (Session["dtTRN_SUB"] != null)
                    {
                        dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                    }
                    else
                    {
                        grdsub_trn.DataSource = null;
                        grdsub_trn.DataBind();
                        if (dtTRN_SUB != null && dtTRN_SUB.Rows.Count > 0)
                        {
                            dtTRN_SUB.Clear();
                        }
                    }
                }
                BindMachineCode();
                BindMacCodeTRN();
                BindUOM();
                BindBOMGrid();
                //txtPlanningDate.Text = System.DateTime.Today.AddDays(1).ToShortDateString();
                txtPlanningDate.Text = System.DateTime.Today.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }
    private void BindMacCodeTRN()
    {
        try
        {
            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            string MAC_TRNNO = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetNewMacCodeTRN(oOD_CAPTURE_MST);
            txtTRNNo.Text = MAC_TRNNO;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlMacCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        BindMachineCode();
    }
    private void BindMachineCode()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetMachineCode(YARN_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlMachine.Items.Clear();
                ddlMachine.DataSource = dt;
                ddlMachine.DataTextField = "MACHINE_CAPACITY";
                ddlMachine.DataValueField = "MACHINE_CODE";
                ddlMachine.DataBind();
                //ddlMachine.Items.Insert(0, new ListItem("------SELECT----", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLotData(e.Text.ToUpper(), e.ItemsOffset, "GREY_LOT_NO");
            txtLotNo.Items.Clear();
            txtLotNo.DataSource = data;
            txtLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetLotCount(e.Text, "GREY_LOT_NO");
        }
        catch (Exception ex)
        {
            //CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Merge No in party selection.\r\nSee error log for detail."));
            // lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetLotData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))    AND OTHER_INFO = '" + ARTICAL_CODE + "'     AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery)      )   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " WHERE MST_CODE NOT IN ( SELECT * FROM (SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))      AND OTHER_INFO = '" + ARTICAL_CODE + "'     AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery)      ) WHERE  ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by MST_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
        }
        catch
        {
            throw;
        }
    }
    protected int GetLotCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'       AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))       AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery)    UNION   SELECT   DISTINCT   LOT_NO MST_CODE,  MERGE_NO MST_DESC   FROM   V_YRN_LOT_MAKING      WHERE  CONF_FLAG=1   AND OTHER_INFO = '"+ARTICAL_CODE+"'   AND (   UPPER (LOT_NO) LIKE :SearchQuery OR UPPER (MERGE_NO) LIKE :SearchQuery)    ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    private void BindUOM()
    {
        try
        {
            ddlUOM.Items.Insert(0, new ListItem("KGS", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnAdjustIndentItem_Click(object sender, EventArgs e)
    {
        try
        {
            double TotalQTY = 0;
            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                TotalQTY += double.Parse(lblQTY.Text);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + txtQTY + "')", true);
        }
        catch (Exception ex)
        {
            lblIndentAdjustmentError.Text = ex.Message;
        }
    }
    private void BindBOMGrid()
    {
        try
        {
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            else
            {
                dtTRN_SUB = createAdjTable();
            }
            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "ARTICAL_CODE='" + YARN_CODE + "' and SHADE_CODE='" + txtShadeCode.Text.Trim() + "'";
            if (dv.Count > 0)
            {
                // clearInitial();
                CalculateAllData();
                txtAdjustedIndentQTY.Text = string.Empty;
                grdsub_trn.DataSource = dv;
                grdsub_trn.DataBind();
                Label lblTRNNo = grdsub_trn.Rows[grdsub_trn.Rows.Count - 1].FindControl("lblTRNNo") as Label;
                Int64 TRN_NUMB = 0;
                Int64.TryParse(lblTRNNo.Text, out TRN_NUMB);
                txtTRNNo.Text = (TRN_NUMB + 1).ToString();
            }
            else
            {
                grdsub_trn.DataSource = null;
                grdsub_trn.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BtnBOMSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            else
            {
                dtTRN_SUB = createAdjTable();
            }
            if (CheckQTYtotal())
            {
                //!(double.Parse(ddlMachine.SelectedValue) < double.Parse(txtAdjustedIndentQTY.Text)) &&


                if ( ddlMachine.SelectedText != "" && txtAdjustedIndentQTY.Text != "" && txtPlanningDate.Text != "" && txtPlanningDate.Text == System.DateTime.Today.ToShortDateString())
                {
                    int UNIQUE_ID = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                    {
                        UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                    }
                    bool bb = SearchInBOMgrid(txtTRNNo.Text, UNIQUE_ID);
                    if (!bb)
                    {
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtTRN_SUB);
                            dv.RowFilter = "ARTICAL_CODE='" + YARN_CODE + "' and SHADE_CODE='" + txtShadeCode.Text.Trim() + "' and UNIQUE_ID=" + UNIQUE_ID;
                            if (dv.Count > 0)
                            {
                                double QTY = 0f;
                                double.TryParse(txtAdjustedIndentQTY.Text.Trim(), out QTY);

                                dv[0]["ADJ_QTY"] = QTY;
                                dv[0]["ARTICAL_CODE"] = YARN_CODE;
                                dv[0]["MAC_TRN_NUMB"] = MAC_TRN_NUMB;
                                dv[0]["SHADE_CODE"] = SHADE_CODE;
                                dv[0]["PI_TYPE"] = PI_TYPE;
                                dv[0]["PRODUCT_TYPE"] = PRODUCT_TYPE;
                                dv[0]["CR_ST_SUBSTRATE"] = "NA";
                                dv[0]["CR_COMP_CODE"] = COMP_CODE;
                                dv[0]["CR_BRANCH_CODE"] = BRANCH_CODE;
                                dv[0]["CR_YEAR"] = YEAR;
                                dv[0]["CR_ORDER_TYPE"] = ORDER_TYPE;
                                dv[0]["CR_ORDER_CAT"] = CR_ORDER_CAT;
                                dv[0]["CR_PRODUCT_TYPE"] = PRODUCT_TYPE;
                                dv[0]["CR_BUSINESS_TYPE"] = BUSINESS_TYPE;
                                dv[0]["CR_ST_ORDER_NO"] = CR_ST_ORDER_NO;
                                dv[0]["CR_ST_ARTICLE_NO"] = YARN_CODE;
                                dv[0]["CR_ST_COUNT"] = "NA";
                                dv[0]["CR_ST_SHADE_CODE"] = SHADE_CODE;
                                dv[0]["ADJ_QTY"] = QTY;
                                dv[0]["CR_YRN_COUNT"] = 0;
                                dv[0]["CR_YRN_PLY"] = "NA";
                                dv[0]["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                                dv[0]["CR_ST_SHADE_CODE"] = txtShadeCode.Text.Trim();
                                dv[0]["CR_ST_SHADE_FAMILY_CODE"] = txtShadeFamily.Text.Trim();
                                dv[0]["TRN_NUMB"] = txtTRNNo.Text.Trim();
                                dv[0]["MACHINE_CODE"] = ddlMachine.SelectedValue.Trim();
                                double CONS = 0;
                                double.TryParse(txtCons.Text.Trim(), out CONS);
                                dv[0]["CONS"] = CONS;
                                dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                                dv[0]["GREY_LOT_NO"] = txtLotNo.SelectedValue.ToString().Trim();
                                dv[0]["PLANNING_DATE"] = txtPlanningDate.Text.Trim();
                                dv[0]["JOBER"] = txtPartyCode1.SelectedText.Trim();
                                dtTRN_SUB.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtTRN_SUB.NewRow();
                            dr["UNIQUE_ID"] = dtTRN_SUB.Rows.Count + 1;
                            double QTY = 0f;
                            double.TryParse(txtAdjustedIndentQTY.Text.Trim(), out QTY);
                            dr["ADJ_QTY"] = QTY;
                            dr["ARTICAL_CODE"] = YARN_CODE;
                            dr["SHADE_CODE"] = SHADE_CODE;
                            dr["MAC_TRN_NUMB"] = MAC_TRN_NUMB;
                            dr["PI_TYPE"] = PI_TYPE;
                            dr["PRODUCT_TYPE"] = PRODUCT_TYPE;
                            dr["CR_ST_SUBSTRATE"] = "NA";
                            dr["CR_COMP_CODE"] = COMP_CODE;
                            dr["CR_BRANCH_CODE"] = BRANCH_CODE;
                            dr["CR_YEAR"] = YEAR;
                            dr["CR_ORDER_TYPE"] = ORDER_TYPE;
                            dr["CR_ORDER_CAT"] = CR_ORDER_CAT;
                            dr["CR_PRODUCT_TYPE"] = PRODUCT_TYPE;
                            dr["CR_BUSINESS_TYPE"] = BUSINESS_TYPE;
                            dr["CR_ST_ORDER_NO"] = CR_ST_ORDER_NO;
                            dr["CR_ST_ARTICLE_NO"] = YARN_CODE;
                            dr["CR_ST_COUNT"] = "NA";
                            dr["CR_ST_SHADE_CODE"] = SHADE_CODE;
                            dr["ADJ_QTY"] = QTY;
                            dr["CR_YRN_COUNT"] = 0;
                            dr["CR_YRN_PLY"] = "NA";
                            dr["UOM"] = ddlUOM.SelectedItem.ToString();
                            dr["CR_ST_SHADE_CODE"] = txtShadeCode.Text.Trim();
                            dr["CR_ST_SHADE_FAMILY_CODE"] = txtShadeFamily.Text.Trim();
                            dr["TRN_NUMB"] = txtTRNNo.Text.Trim();
                            dr["MACHINE_CODE"] = ddlMachine.SelectedValue.Trim();
                            double CONS = 0;
                            double.TryParse(txtCons.Text.Trim(), out CONS);
                            dr["CONS"] = CONS;
                            dr["REMARKS"] = txtRemarks.Text.Trim();
                            dr["GREY_LOT_NO"] = txtLotNo.SelectedValue.ToString().Trim();

                            dr["PLANNING_DATE"] = txtPlanningDate.Text.Trim();
                            dr["JOBER"] = txtPartyCode1.SelectedText.Trim();
                            dtTRN_SUB.Rows.Add(dr);
                        }
                        Session["dtTRN_SUB"] = dtTRN_SUB;
                        RefresBOMRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Enter Another Lot No.This Already Added ');", true);
                    }
                    BindBOMGrid();
                }
                else if ((double.Parse(ddlMachine.SelectedText) < double.Parse(txtAdjustedIndentQTY.Text)))
                {

                    Common.CommonFuction.ShowMessage("Qty Should not be more than Machine Capacity");
                }
                else if (txtPlanningDate.Text == System.DateTime.Today.ToShortDateString())
                {
                    Common.CommonFuction.ShowMessage("Select Another Date");
                }
                else if (ddlMachine.SelectedIndex == -1)
                {
                    Common.CommonFuction.ShowMessage("Machine Code Required");
                }
                else if (txtAdjustedIndentQTY.Text == "")
                {
                    Common.CommonFuction.ShowMessage("Quantity can not be zero");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("You Cannot Recieve Exceeds Quantity !! Limit is =: " + lblMaxQTY);

            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Sub Transaction Detail Row.\r\nSee error log for detail."));

        }
    }
    private void RefresBOMRow()
    {
        try
        {
            ddlMachine.SelectedIndex = -1;
            txtAdjustedIndentQTY.Text = string.Empty;
            txtCons.Text = string.Empty;
            txtLotNo.SelectedIndex = -1;
            txtRemarks.Text = string.Empty;
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdSub_trnArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sub Tran Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }
    private void FillBOMByGrid(int UNIQUE_ID)
    {
        try
        {

            if (Session["dtTRN_SUB"] != null)
            {

                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];

            }
            else
            {
                dtTRN_SUB = createAdjTable();
            }
            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                ddlMachine.SelectedValue = dv[0]["MACHINE_CODE"].ToString();
                txtCons.Text = dv[0]["ADJ_QTY"].ToString();
                txtLotNo.SelectedValue = dv[0]["GREY_LOT_NO"].ToString();
                txtCons.Text = dv[0]["CONS"].ToString();
                txtAdjustedIndentQTY.Text = dv[0]["ADJ_QTY"].ToString();
                txtPlanningDate.Text = dv[0]["PLANNING_DATE"].ToString();
                txtPartyCode1.SelectedText = dv[0]["JOBER"].ToString();
                txtRemarks.Text = dv[0]["REMARKS"].ToString();

                string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GREY_LOT_NO'))              AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression = " order by MST_CODE asc";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", SortExpression, "", "%", "");
                txtLotNo.DataSource = data;
                txtLotNo.DataTextField = "MST_CODE";
                txtLotNo.DataValueField = "MST_CODE";
                txtLotNo.DataBind();
                foreach (ComboBoxItem item in txtLotNo.Items)
                {
                    if (item.Text == dv[0]["GREY_LOT_NO"].ToString())
                    {
                        txtLotNo.SelectedIndex = txtLotNo.Items.IndexOf(item);
                        break;
                    }
                }
                string CommandText1 = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE ,VENDOR_CAT_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)=upper('JOBER') and ROWNUM <= 15 ";
                string SortExpression1 = " order by PRTY_CODE asc";
                DataTable data1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, "", SortExpression1, "", "%", "");
                txtPartyCode1.DataSource = data1;
                txtPartyCode1.DataTextField = "PRTY_CODE";
                txtPartyCode1.DataValueField = "PRTY_NAME";
                txtPartyCode1.DataBind();
                foreach (ComboBoxItem item in txtPartyCode1.Items)
                {
                    if (item.Text == dv[0]["JOBER"].ToString())
                    {
                        txtPartyCode1.SelectedIndex = txtPartyCode1.Items.IndexOf(item);
                        break;
                    }
                }
             
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void DeleteBOMRow(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            if (grdsub_trn.Rows.Count == 1)
            {
                dtTRN_SUB.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRN_SUB.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            Session["dtTRN_SUB"] = dtTRN_SUB;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        RefresBOMRow();
    }
    private DataTable createAdjTable()
    {
        dtTRN_SUB = new DataTable();
        dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
        dtTRN_SUB.Columns.Add("ORDER_NO", typeof(string));
        dtTRN_SUB.Columns.Add("ARTICAL_CODE", typeof(string));
        dtTRN_SUB.Columns.Add("SHADE_CODE", typeof(string));
        dtTRN_SUB.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtTRN_SUB.Columns.Add("PI_TYPE", typeof(string));
        dtTRN_SUB.Columns.Add("PI_NO", typeof(string));
        dtTRN_SUB.Columns.Add("CR_COMP_CODE", typeof(string));
        dtTRN_SUB.Columns.Add("CR_BRANCH_CODE", typeof(string));
        dtTRN_SUB.Columns.Add("CR_YEAR", typeof(int));
        dtTRN_SUB.Columns.Add("CR_ORDER_TYPE", typeof(string));
        dtTRN_SUB.Columns.Add("CR_ORDER_CAT", typeof(string));
        dtTRN_SUB.Columns.Add("CR_PRODUCT_TYPE", typeof(string));
        dtTRN_SUB.Columns.Add("CR_BUSINESS_TYPE", typeof(string));
        dtTRN_SUB.Columns.Add("CR_ST_ORDER_NO", typeof(string));
        dtTRN_SUB.Columns.Add("CR_ST_ARTICLE_NO", typeof(string));
        dtTRN_SUB.Columns.Add("CR_ST_SUBSTRATE", typeof(string));
        dtTRN_SUB.Columns.Add("CR_ST_COUNT", typeof(string));
        dtTRN_SUB.Columns.Add("CR_ST_SHADE_FAMILY_CODE", typeof(string));
        dtTRN_SUB.Columns.Add("CR_ST_SHADE_CODE", typeof(string));
        dtTRN_SUB.Columns.Add("CR_YRN_COUNT", typeof(int));
        dtTRN_SUB.Columns.Add("CR_YRN_PLY", typeof(string));
        dtTRN_SUB.Columns.Add("ADJ_QTY", typeof(double));
        dtTRN_SUB.Columns.Add("TRN_NUMB", typeof(int));
        dtTRN_SUB.Columns.Add("UOM", typeof(string));
        dtTRN_SUB.Columns.Add("MACHINE_CODE", typeof(string));
        dtTRN_SUB.Columns.Add("REMARKS", typeof(string));
        dtTRN_SUB.Columns.Add("CONS", typeof(double));
        dtTRN_SUB.Columns.Add("GREY_LOT_NO", typeof(string));
        dtTRN_SUB.Columns.Add("MAC_TRN_NUMB", typeof(int));
        dtTRN_SUB.Columns.Add("PLANNING_DATE", typeof(string));
        dtTRN_SUB.Columns.Add("TUSER", typeof(string));
        dtTRN_SUB.Columns.Add("JOBER", typeof(string));
        return dtTRN_SUB;
    }
    private bool SearchInBOMgrid(string TRN_NUMB, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdsub_trn.Rows)
            {
                Label lblTrnNo = (Label)grdRow.FindControl("lblTrnNo");
                Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());
                if (lblTrnNo.Text == TRN_NUMB && UNIQUE_ID != iUNIQUE_ID)
                {
                    Result = true;
                }
            }
            return Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void CalculateAllData()
    {
        if (grdsub_trn.Rows.Count > 0)
        {
            double totalNetWt = 0;
            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                double NetWt = 0;
                Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                double.TryParse(lblQTY.Text, out NetWt);
                totalNetWt = totalNetWt + NetWt;
            }
            ((Label)grdsub_trn.FooterRow.FindControl("flblQTY")).Text = totalNetWt.ToString();
        }
    }
    protected bool CheckQTYtotal()
    {
        try
        {
            double currentpage = 0;
            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
            {
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
            }
            lblMaxQTY = double.Parse(ViewState["lblMaxQTY"].ToString());
            lblMaxQTY = lblMaxQTY + (lblMaxQTY * 10 / 100);
            if (grdsub_trn.Rows.Count > 0)
            {
                for (int i = 0; i < grdsub_trn.Rows.Count; i++)
                {

                    Label txtSubTrnUNIQUE_ID = (Label)grdsub_trn.Rows[i].FindControl("txtSubTrnUNIQUE_ID");
                    int iUNIQUEID = int.Parse(txtSubTrnUNIQUE_ID.Text);
                    if (UNIQUE_ID != iUNIQUEID)
                    {
                        Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                        currentpage += double.Parse(lblQTY.Text);
                    }
                }
                
                double alltotal = currentpage + double.Parse(txtAdjustedIndentQTY.Text);
                if (alltotal > lblMaxQTY)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else if (double.Parse(txtAdjustedIndentQTY.Text) < lblMaxQTY)
            {
                return true;
            }
            else
            {
                return false;
                Common.CommonFuction.ShowMessage("You Cannot Recieve Exceeds Quantity !! Limit is =: " + lblMaxQTY);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyDataSearch(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode1.Items.Clear();
            txtPartyCode1.DataSource = data;
            txtPartyCode1.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCountSearch(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));

        }
    }

    private DataTable GetPartyDataSearch(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE ,VENDOR_CAT_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)in ('JOBER','YARN SUPPLIER') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE ,VENDOR_CAT_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)in('JOBER','YARN SUPPLIER') and ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int GetPartyCountSearch(string text)
    {
        try
        {
            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address ,VENDOR_CAT_CODE FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)=upper('JOBER') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



}

