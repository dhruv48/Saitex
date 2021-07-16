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
using Obout.Interface;
using System.Collections.Generic;

public partial class Module_Production_Controls_Dyeing : System.Web.UI.UserControl
{
    private static DataTable dtProdTRN;
    public static string PRODUCT_TYPE = string.Empty;
    SaitexDM.Common.DataModel.YRN_PROD_MST oYRN_PROD_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string TRN_TYPE = "RYS21";
    List<string> Rack_List = new List<string>();

    public string PRODUCTTYPE
    {
        get
        {
            return PRODUCT_TYPE;
        }
        set
        {
            PRODUCT_TYPE = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in page loading.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            TRN_TYPE = "RYS21";
            BlanksControls();
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            lblMode.Text = "Save";
            ddlProductionNo.Visible = false;
            txtProductionNo.Visible = true;
            ddlProductionNo.SelectedIndex = -1;
            ddlPaNo.SelectedIndex = -1;
            ddlToLocation.SelectedIndex = -1;
            ddlFromLocation.SelectedIndex = -1;
            txtArticle.Text = string.Empty;
            txtArticleDesc.Text = string.Empty;
            txtProductionNo.Text = string.Empty;
            txtBatchDate.Text = string.Empty;
            txtCustReqNo.Text = string.Empty;
            txtOrderQty.Text = string.Empty;
            txtJobCardNo.Text = string.Empty;
            txtPaNo.Text = string.Empty;
            txtPartDtl.Text = string.Empty;
            txtParty.Text = string.Empty;
            txtShade.Text = string.Empty;
            BindProductionNo();
            dtProdTRN = null;
            RefreshDetailRow();
            bindToProcess();
            bindFromProcess();
            txtBatchDate.Text = DateTime.Now.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        if (ViewState["dtProdTRN"] != null)
            dtProdTRN = (DataTable)ViewState["dtProdTRN"];
        if (dtProdTRN == null)
            CreateDataTable();
        if (dtProdTRN != null)
            dtProdTRN.Rows.Clear();
        gvProductionEntry.DataSource = null;
        gvProductionEntry.DataBind();
        gvProductionEntry.SelectedIndex = -1;
    }
    private void BindProductionNo()
    {
        try
        {
            string PREFIX = string.Empty;
            oYRN_PROD_MST = new SaitexDM.Common.DataModel.YRN_PROD_MST();
            oYRN_PROD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_PROD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_PROD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_PROD_MST.TRN_TYPE = TRN_TYPE;
            string ProductionNo = SaitexBL.Interface.Method.YRN_PROD_MST.GetMaxProdutionDyeingNO(oYRN_PROD_MST);
            txtProductionNo.Text = ProductionNo;

        }
        catch
        {
            throw;
        }
    }
    private void bindToProcess()
    {
        try
        {
            ddlToProcess.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PACK_PROCESS", oUserLoginDetail.COMP_CODE);
            ddlToProcess.DataSource = dt;
            ddlToProcess.DataValueField = "MST_CODE";
            ddlToProcess.DataTextField = "MST_DESC";
            ddlToProcess.DataBind();
            ddlToProcess.Items.Insert(0, new ListItem("--Select--", ""));
            ddlToProcess.SelectedIndex = ddlToProcess.Items.IndexOf(ddlToProcess.Items.FindByValue("DYEING"));
        }
        catch
        {
            throw;
        }
    }
    private void bindFromProcess()
    {
        try
        {
            ddlFromProcess.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PACK_PROCESS", oUserLoginDetail.COMP_CODE);
            ddlFromProcess.DataSource = dt;
            ddlFromProcess.DataValueField = "MST_CODE";
            ddlFromProcess.DataTextField = "MST_DESC";
            ddlFromProcess.DataBind();
            ddlFromProcess.Items.Insert(0, new ListItem("--Select--", ""));
            ddlFromProcess.SelectedIndex = ddlFromProcess.Items.IndexOf(ddlFromProcess.Items.FindByValue("DYEING"));
        }
        catch
        {
            throw;
        }
    }
    protected void ddlProductionNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetBatchCodeData(e.Text.ToUpper(), e.ItemsOffset);

            ddlProductionNo.Items.Clear();

            ddlProductionNo.DataSource = data;
            ddlProductionNo.DataTextField = "BATCH_CODE";
            ddlProductionNo.DataValueField = "BATCH_CODE";
            ddlProductionNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetBatchCodeCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading process.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetBatchCodeData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, LOT_NUMBER FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND NVL(R.CONF_FLAG,0)=0 AND R.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + " ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND BATCH_CODE NOT IN (SELECT BATCH_CODE FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, LOT_NUMBER FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND NVL(A.CONF_FLAG,0)=0 AND a.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + " ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by BATCH_CODE";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetBatchCodeCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, LOT_NUMBER FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND NVL(R.CONF_FLAG,0)=0 AND R.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + " ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlProductionNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        //try
        //{
        //    int BATCH_NUMBER = int.Parse(ddlBatchCode.SelectedValue.Trim());
        //    if (ViewState["dtDetailTBL"] != null)
        //    {
        //        dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
        //    }
        //    else
        //    {
        //        CreateDataTable();
        //    }

        //    int iRecordFound = GetdataByBatchNumber(BATCH_NUMBER);
        //    BindGridFromDataTable();
        //    if (iRecordFound > 0)
        //    {

        //    }
        //    else
        //    {
        //        InitialisePage();
        //        //ActivateUpdateMode();
        //        //  string msg = "Dear " + oUserLoginDetail.Username + "!! Select Issue No already approved. Modification not allowed.";
        //        //Common.CommonFuction.ShowMessage(msg);
        //        // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Challan No');", true);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        //    lblMode.Text = ex.ToString();
        //}
    }

    protected void ddlPaNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPaNoData(e.Text.ToUpper(), e.ItemsOffset);

            ddlPaNo.Items.Clear();

            ddlPaNo.DataSource = data;
            ddlPaNo.DataTextField = "PA_NO";
            ddlPaNo.DataValueField = "TRN_DATA";
            ddlPaNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPaNoCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetPaNoData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   *  FROM   (  SELECT   CUST_REQ_NO, PA_NO, LOT_SIZE AS TRN_QTY,  ORDER_NO, CAST (BATCH_CODE AS int) BATCH_CODE, ARTICLE_CODE, ARTICLE_DESC, SHADE_CODE, PRTY_CODE, PRTY_NAME,  TRN_NUMB, (   CUST_REQ_NO || '@'|| PA_NO  || '@'  || LOT_SIZE  || '@'  || BATCH_CODE   || '@'  || ARTICLE_CODE   || '@'  || ARTICLE_DESC || '@'   || SHADE_CODE || '@' || PRTY_CODE  || '@' || PRTY_NAME   || '@' || TRN_NUMB|| '@'|| LOT_SIZE || '@'|| SPRINGS) TRN_DATA     FROM   V_BATCH_CARD_MST WHERE  COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' and  CONF_FLAG='1'   and PRODUCT_TYPE= '" + PRODUCT_TYPE + "'AND (PA_NO,  LOT_SIZE,  BATCH_CODE, COMP_CODE, BRANCH_CODE,  YEAR) NOT IN   (SELECT   PA_NO, TRN_QTY AS LOT_SIZE,   BATCH_CODE,  COMP_CODE,  BRANCH_CODE,   YEAR   FROM   YARN_DYEING_PROD_MST WHERE COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "') GROUP BY   CUST_REQ_NO, PA_NO,  LOT_SIZE,  ORDER_NO, BATCH_CODE,   ARTICLE_CODE,   ARTICLE_DESC, SHADE_CODE,  PRTY_CODE,  PRTY_NAME,   TRN_NUMB , LOT_SIZE, SPRINGS ORDER BY   CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery) WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND trn_data NOT IN(SELECT trn_data FROM (SELECT   *  FROM   (  SELECT   CUST_REQ_NO, PA_NO, LOT_SIZE AS TRN_QTY,  ORDER_NO, CAST (BATCH_CODE AS int) BATCH_CODE, ARTICLE_CODE, ARTICLE_DESC, SHADE_CODE, PRTY_CODE, PRTY_NAME,  TRN_NUMB, (   CUST_REQ_NO || '@'|| PA_NO  || '@'  || LOT_SIZE  || '@'  || BATCH_CODE   || '@'  || ARTICLE_CODE   || '@'  || ARTICLE_DESC || '@'   || SHADE_CODE || '@' || PRTY_CODE  || '@' || PRTY_NAME   || '@' || TRN_NUMB || '@'|| LOT_SIZE || '@'|| SPRINGS) TRN_DATA  FROM   V_BATCH_CARD_MST WHERE  COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'  and  CONF_FLAG='1'  and PRODUCT_TYPE= '" + PRODUCT_TYPE + "'AND (PA_NO,  LOT_SIZE,  BATCH_CODE, COMP_CODE, BRANCH_CODE,  YEAR) NOT IN   (SELECT   PA_NO, TRN_QTY AS LOT_SIZE,   BATCH_CODE,  COMP_CODE,  BRANCH_CODE,   YEAR   FROM   YARN_DYEING_PROD_MST WHERE COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "')  GROUP BY   CUST_REQ_NO, PA_NO,  LOT_SIZE,  ORDER_NO, BATCH_CODE,   ARTICLE_CODE,   ARTICLE_DESC, SHADE_CODE,  PRTY_CODE,  PRTY_NAME,   TRN_NUMB, LOT_SIZE, SPRINGS ORDER BY   CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by BATCH_CODE";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetPaNoCount(string text)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   *  FROM   (  SELECT   CUST_REQ_NO, PA_NO, LOT_SIZE AS TRN_QTY,  ORDER_NO, BATCH_CODE, ARTICLE_CODE, ARTICLE_DESC, SHADE_CODE, PRTY_CODE, PRTY_NAME,  TRN_NUMB, (   CUST_REQ_NO || '@'|| PA_NO  || '@'  || LOT_SIZE  || '@'  || BATCH_CODE   || '@'  || ARTICLE_CODE   || '@'  || ARTICLE_DESC || '@'   || SHADE_CODE || '@' || PRTY_CODE  || '@' || PRTY_NAME   || '@' || TRN_NUMB|| '@'|| LOT_SIZE || '@'|| SPRINGS) TRN_DATA     FROM   V_BATCH_CARD_MST WHERE  COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND CONF_FLAG = '1'   and PRODUCT_TYPE= '" + PRODUCT_TYPE + "'AND (PA_NO,  LOT_SIZE,  BATCH_CODE, COMP_CODE, BRANCH_CODE,  YEAR) NOT IN   (SELECT   PA_NO, TRN_QTY AS LOT_SIZE,   BATCH_CODE,  COMP_CODE,  BRANCH_CODE,   YEAR   FROM   YARN_DYEING_PROD_MST WHERE COMP_CODE='C00001' AND BRANCH_CODE='SL0001') GROUP BY   CUST_REQ_NO, PA_NO,  LOT_SIZE,  ORDER_NO, BATCH_CODE,   ARTICLE_CODE,   ARTICLE_DESC, SHADE_CODE,  PRTY_CODE,  PRTY_NAME,   TRN_NUMB , LOT_SIZE, SPRINGS ORDER BY   CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }
    protected void ddlPaNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string TRN_DATA = ddlPaNo.SelectedValue.Trim();
            string[] CombineData = TRN_DATA.Split('@');

            txtCustReqNo.Text = CombineData[0].ToString();
            txtPaNo.Text = CombineData[1].ToString();

            txtOrderQty.Text = CombineData[2].ToString();
            txtJobCardNo.Text = CombineData[3].ToString();
            txtArticle.Text = CombineData[4].ToString();
            txtArticleDesc.Text = CombineData[5].ToString();
            txtShade.Text = CombineData[6].ToString();

            txtParty.Text = CombineData[7].ToString();
            txtPartDtl.Text = CombineData[8].ToString();

            bindProductionData();
            T1.Visible = false;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting PA No.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }


    private void bindProductionData()
    {
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string BATCH_CODE = string.Empty;
        string SHADE_CODE = string.Empty;
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            SaitexDM.Common.DataModel.YRN_PROD_MST oYRN_PROD_MST = new SaitexDM.Common.DataModel.YRN_PROD_MST();
            oYRN_PROD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_PROD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_PROD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            if (txtJobCardNo.Text.Trim() != null && txtJobCardNo.Text.Trim() != string.Empty)
            {
                BATCH_CODE = txtJobCardNo.Text.Trim();
            }
            else
            {
                BATCH_CODE = string.Empty;
            }
            if (txtShade.Text.Trim() != string.Empty)
            {

                SHADE_CODE = txtShade.Text.ToString();
            }

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetProductionData(BRANCH_CODE, BATCH_CODE, YEAR, SHADE_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }

                int x;
                if (dt.Columns.Contains("CONF_DATE"))
                {
                    x = 1;
                }
                else
                {
                    x = 2;
                }
                int z = x;

                DataView dvProduction = new DataView(dt);
                dvProduction.RowFilter = "YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'";
                if (dvProduction.Count > 0)
                {
                    gvProductionEntry.DataSource = dvProduction;
                    gvProductionEntry.DataBind();
                    //lblTotalRecord.Text = dvShadeGrouping.Count.ToString().Trim();
                }
                else
                {
                    // lblTotalRecord.Text = "No ShadeGroup for approval";
                    gvProductionEntry.DataSource = null;
                    gvProductionEntry.DataBind();
                    //lblTotalRecord.Text = "0";
                }
            }
            else
            {
                //lblTotalRecord.Text = "No ShadeGroup for approval";
                gvProductionEntry.DataSource = null;
                gvProductionEntry.DataBind();
                //lblTotalRecord.Text = "0";
            }
        }
        catch
        {
            throw;
        }
    }
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;
        TextBox txtTrnQty = (TextBox)gvRow.FindControl("txtTrnQty");
        TextBox txtRejNetWt = (TextBox)gvRow.FindControl("txtRejNetWt");
        TextBox txtQty = (TextBox)gvRow.FindControl("txtQty");

        try
        {
            txtRejNetWt.Text = ((Convert.ToInt64(txtTrnQty.Text)) - (Convert.ToInt64(txtQty.Text))).ToString();

        }
        catch { txtRejNetWt.Text = "0"; txtQty.Text = "0"; }
    }



    protected void gvProductionEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlRack_No = (e.Row.FindControl("ddlRack_No") as DropDownList);

            for (int i = 1; i <= 100; i++)
            {
                string Rack_No = string.Format("Trolly-{0}", i);
                Rack_List.Add(Rack_No);
            }
            ddlRack_No.DataSource = Rack_List;
            ddlRack_No.DataBind();
            ddlRack_No.Items.Insert(0, new ListItem("Select", string.Empty));
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            dtProdTRN = new DataTable();

            dtProdTRN.Columns.Add("YEAR", typeof(int));
            dtProdTRN.Columns.Add("COMP_CODE", typeof(string));
            dtProdTRN.Columns.Add("BRANCH_CODE", typeof(string));
            dtProdTRN.Columns.Add("TRN_TYPE", typeof(string));
            dtProdTRN.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtProdTRN.Columns.Add("ARTICLE_CODE", typeof(string));
            dtProdTRN.Columns.Add("BATCH_CODE", typeof(string));
            dtProdTRN.Columns.Add("POST_DATE", typeof(DateTime));
            dtProdTRN.Columns.Add("GREY_LOT_NO", typeof(string));
            dtProdTRN.Columns.Add("SHADE_CODE", typeof(string));
            dtProdTRN.Columns.Add("MACHINE_CODE", typeof(string));
            dtProdTRN.Columns.Add("PROCESS", typeof(string));
            dtProdTRN.Columns.Add("GRADE", typeof(string));
            dtProdTRN.Columns.Add("SHADE_REFF_NO", typeof(string));
            dtProdTRN.Columns.Add("TRN_QTY", typeof(double));
            dtProdTRN.Columns.Add("CORTOON_NO", typeof(double));
            dtProdTRN.Columns.Add("COPS", typeof(double));
            dtProdTRN.Columns.Add("REJ_COPS", typeof(double));
            dtProdTRN.Columns.Add("REJ_TRN_QTY", typeof(double));
            dtProdTRN.Columns.Add("LOT_SIZE", typeof(double));
            dtProdTRN.Columns.Add("TROLLY_NO", typeof(string));
            dtProdTRN.Columns.Add("CONF_FLAG", typeof(string));
            dtProdTRN.Columns.Add("CONF_BY", typeof(string));
            dtProdTRN.Columns.Add("CONF_DATE", typeof(string));
            dtProdTRN.Columns.Add("DATE_OF_MFG", typeof(string));
            dtProdTRN.Columns.Add("BATCH_ISSUE_NO", typeof(string));
            dtProdTRN.Columns.Add("CONF_REMARKS", typeof(string));
            return dtProdTRN;

        }
        catch
        {
            throw;
        }
    }


    private DataTable CreateDataTableDyeingProduction()
    {

        try
        {
            string msg = string.Empty;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtProdTRN = new DataTable();
            if (ViewState["dtProdTRN"] == null)
                dtProdTRN = CreateDataTable();
            else
            {
                dtProdTRN.Rows.Clear();
                dtProdTRN = (DataTable)ViewState["dtProdTRN"];
            }
            int totalRows = gvProductionEntry.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvProductionEntry.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblPostDate = (Label)thisGridViewRow.FindControl("lblPostDate");
                    Label lblBatchNo = (Label)thisGridViewRow.FindControl("lblBatchNo");
                    Label lblItemCode = (Label)thisGridViewRow.FindControl("lblItemCode");
                    Label lblGreyLotNo = (Label)thisGridViewRow.FindControl("lblGreyLotNo");
                    Label lblShade = (Label)thisGridViewRow.FindControl("lblShade");
                    Label lblTrnQty = (Label)thisGridViewRow.FindControl("lblTrnQty");

                    Label lblUOM = (Label)thisGridViewRow.FindControl("lblUOM");
                    Label lblMAchineCode = (Label)thisGridViewRow.FindControl("lblMAchineCode");
                    Label lblProcess = (Label)thisGridViewRow.FindControl("lblProcess");

                    TextBox BatchIssueNo = (TextBox)thisGridViewRow.FindControl("txtBatchIssueNo");
                    TextBox CortoonNo = (TextBox)thisGridViewRow.FindControl("txtCortoonNo");
                    TextBox Cops = (TextBox)thisGridViewRow.FindControl("txtCops");
                    TextBox TrnQty = (TextBox)thisGridViewRow.FindControl("txtQty");
                    TextBox RejCops = (TextBox)thisGridViewRow.FindControl("txtRejCops");
                    TextBox RejNetWt = (TextBox)thisGridViewRow.FindControl("txtRejNetWt");
                    DropDownList ddlRack_No = (DropDownList)thisGridViewRow.FindControl("ddlRack_No");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox DyeingRemark = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    if (Approved.Checked == true)
                    {
                        string iTrnQty = TrnQty.Text.Trim();
                        string iRqTrnQty = lblTrnQty.Text.Trim();

                        DateTime dConfirmDate = Convert.ToDateTime(ConfirmDate.Text.Trim());
                        string strConfirmBy = ConfirmBy.Text.Trim();
                        string strRemarks = DyeingRemark.Text.Trim();

                        DataRow dr = dtProdTRN.NewRow();

                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["TRN_TYPE"] = "RYS21";
                        dr["BATCH_CODE"] = lblBatchNo.Text.Trim();
                        dr["UOM_OF_UNIT"] = lblUOM.Text.Trim();
                        DateTime dd = System.DateTime.Now;
                        DateTime.TryParse(lblPostDate.Text.Trim(), out dd);
                        dr["POST_DATE"] = dd;
                        dr["SHADE_CODE"] = lblShade.Text.Trim();
                        dr["GREY_LOT_NO"] = lblGreyLotNo.Text.Trim();

                        dr["LOT_SIZE"] = lblTrnQty.Text.Trim();
                        dr["MACHINE_CODE"] = lblMAchineCode.Text.Trim();
                        dr["PROCESS"] = lblProcess.Text.Trim();

                        string[] arrstr = lblItemCode.Text.Split('-');
                        if (arrstr.Length > 0)
                        {
                            dr["ARTICLE_CODE"] = arrstr[0].ToString();
                        }
                        else
                        {
                            dr["ARTICLE_CODE"] = lblItemCode.Text.Trim();
                        }

                        //dr["ARTICLE_CODE"] = lblItemCode.Text.Trim();

                        dr["BATCH_ISSUE_NO"] = BatchIssueNo.Text.ToString().Trim();
                        dr["CORTOON_NO"] = CortoonNo.Text.ToString().Trim();
                        dr["COPS"] = Cops.Text.ToString().Trim();
                        dr["REJ_COPS"] = RejCops.Text.ToString().Trim();
                        dr["REJ_TRN_QTY"] = RejNetWt.Text.ToString().Trim();
                        dr["TROLLY_NO"] = ddlRack_No.SelectedValue;

                        dr["CONF_FLAG"] = "1";
                        dr["TRN_QTY"] = iTrnQty;
                        dr["CONF_DATE"] = dConfirmDate;
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_REMARKS"] = strRemarks;
                        dtProdTRN.Rows.Add(dr);
                        TrnQty.Text = "";
                        Approved.Checked = false;
                        ConfirmDate.Text = "";
                        ConfirmBy.Text = "";
                        DyeingRemark.Text = "";
                    }
                }
            }
            return dtProdTRN;
        }

        catch
        {
            throw;
        }
    }
    private void BlanksControls()
    {
        try
        {
            {
                int totalRows = gvProductionEntry.Rows.Count;
                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = gvProductionEntry.Rows[r];
                    if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                    {
                        Label lblBatchNo = (Label)thisGridViewRow.FindControl("lblBatchNo");
                        TextBox TrnQty = (TextBox)thisGridViewRow.FindControl("txtTrnQty");
                        TextBox BatchIssueNo = (TextBox)thisGridViewRow.FindControl("txtBatchIssueNo");
                        TextBox CortoonNo = (TextBox)thisGridViewRow.FindControl("txtCortoonNo");
                        TextBox Cops = (TextBox)thisGridViewRow.FindControl("txtCops");
                        TextBox RejCops = (TextBox)thisGridViewRow.FindControl("txtRejCops");
                        TextBox RejNetWt = (TextBox)thisGridViewRow.FindControl("txtRejNetWt");
                        DropDownList ddlRack_No = (DropDownList)thisGridViewRow.FindControl("ddlRack_No");
                        CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                        TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                        TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                        TextBox DyeingRemark = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                        TrnQty.Text = "";
                        Approved.Checked = false;
                        ConfirmDate.Text = "";
                        ConfirmBy.Text = "";
                        DyeingRemark.Text = "";
                    }
                }
            }
        }
        catch
        {
            throw;
        }

    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {


    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string iRecordFound = "";
            string TRN_NUMB = string.Empty;

            SaitexDM.Common.DataModel.YRN_PROD_MST oYRN_PROD_MST = new SaitexDM.Common.DataModel.YRN_PROD_MST();

            oYRN_PROD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_PROD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_PROD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_PROD_MST.TRN_NUMB = txtProductionNo.Text.Trim();
            oYRN_PROD_MST.TRN_TYPE = "RYS21";
            oYRN_PROD_MST.BATCH_CODE = txtJobCardNo.Text.Trim();
            oYRN_PROD_MST.CUST_REQ_NO = txtCustReqNo.Text.Trim();
            oYRN_PROD_MST.PA_NO = txtPaNo.Text.Trim();
            oYRN_PROD_MST.SHADE_CODE = txtShade.Text.Trim();
            oYRN_PROD_MST.TO_LOCATION = ddlToLocation.SelectedValue;
            oYRN_PROD_MST.FROM_LOCATION = ddlFromLocation.SelectedValue;
            oYRN_PROD_MST.TO_PROCESS = ddlToProcess.SelectedValue;
            oYRN_PROD_MST.FROM_PROCESS = ddlFromProcess.SelectedValue;
            oYRN_PROD_MST.PARTY_CODE = txtParty.Text.Trim();
            oYRN_PROD_MST.PARTY_NAME = txtPartDtl.Text.Trim();

            string[] arrstr = txtArticle.Text.Split('-');

            if (arrstr.Length > 0)
            {
                oYRN_PROD_MST.ARTICAL_CODE = arrstr[0].ToString();
            }
            else
            {
                oYRN_PROD_MST.ARTICAL_CODE = txtArticle.Text.Trim();
            }

            oYRN_PROD_MST.ARTICAL_DESC = txtArticleDesc.Text.Trim();
            double TRN_QTY = 0;
            double.TryParse(txtOrderQty.Text, out TRN_QTY);
            oYRN_PROD_MST.TRN_QTY = TRN_QTY;
            oYRN_PROD_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_PROD_MST.TDATE = DateTime.Now;
            if (ViewState["TRN_NUMB"] != null)
            {
                TRN_NUMB = (string)ViewState["TRN_NUMB"];
            }
            if (ViewState["dtProdTRN"] == null)
            {
                dtProdTRN = CreateDataTableDyeingProduction();

            }
            bool bResult = SaitexBL.Interface.Method.YRN_PROD_MST.InsertDyeingProductionEntry(oYRN_PROD_MST, dtProdTRN, out  iRecordFound);
            if (bResult)
            {
                InitialisePage();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Dyeing Production Saved Successfully');", true);

            }
            else if (!string.IsNullOrEmpty(iRecordFound))
            {
                iRecordFound += "Production Entry  List of this Fabric " + iRecordFound + " Already exist";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + iRecordFound + "');", true);
            }
            
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fill Production Entry Qty');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in saving.\r\nSee error log for detail."));
        }

    }


    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Production/Pages/Dyeing_Production.aspx");
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        //imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {

    }
}


