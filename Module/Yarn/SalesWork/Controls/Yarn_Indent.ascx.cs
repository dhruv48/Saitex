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
using System.Data.OracleClient;
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Yarn_SalesWork_Controls_Yarn_Indent : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtIndentDetail = null;
    private  double FinalTotal;
    
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindInitialData()
    {
        try
        {
            txtIndentDate.Text = DateTime.Now.Date.ToShortDateString();
            txtRequiredBefore.Text = DateTime.Now.Date.ToShortDateString();

            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                txtDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;
                txtPreparedBy.Text = oUserLoginDetail.Username;
                txtIndentDate.Text = DateTime.Now.ToShortDateString();
                txtRequiredBefore.Text = DateTime.Now.ToShortDateString();

            }
        }
        catch
        {
            throw;
        }
    }

    private void InitialisePage()
    {
        try
        {
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);       
         
            ActivateSaveMode();
            lblMode.Text = "Save";
            txtIndentBranch.Text = oUserLoginDetail.VC_BRANCHNAME;
            FinalTotal = 0;
            txtCommentComment.Text = "";
            txtDepartment.Text = "";
            txtIndentDate.Text = "";
            txtIndentNumber.Text = "";
            txtPreparedBy.Text = "";
            txtRequiredBefore.Text = "";
            ddlIndentNumber.SelectedIndex = -1;

            txtIndentNumber.Text = SaitexBL.Interface.Method.YRN_INT_MST.GetNewIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "GEN",oUserLoginDetail.DT_STARTDATE.Year );
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            BindInitialData();
            BindIndentDetailGrid();
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            txtIndentNumber.ReadOnly = true;
            txtIndentNumber.AutoPostBack = false;
            ddlIndentNumber.Visible = false;
            txtIndentNumber.Visible = true;
            
        }
        catch
        {
            throw;
        }
    }


    protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOTData(e.Text.ToUpper(), e.ItemsOffset, "GREY_LOT_NO");
            txtLotNo.Items.Clear();
            txtLotNo.DataSource = data;
            txtLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetLOTCount(e.Text, "GREY_LOT_NO");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Merge No in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtGrade_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "GRADE");
            txtGrade.Items.Clear();
            txtGrade.DataSource = data;
            txtGrade.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetMOMCount(e.Text, "GRADE");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grade selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetMOMData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND (MST_CODE,MST_DESC) NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";
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

    protected int GetMOMCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    private DataTable GetLOTData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))    AND OTHER_INFO LIKE '" + txtItemCodeLabel.Text + "'     AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND MST_CODE NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))  AND OTHER_INFO LIKE '" + txtItemCodeLabel.Text + "'       AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";
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

    protected int GetLOTCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))   AND OTHER_INFO LIKE '" + txtItemCodeLabel.Text + "'      AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    private void BindIndentDetailGrid()
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            grdIndentDetail.DataSource = dtIndentDetail;
            grdIndentDetail.DataBind();
            FinalTotal = 0;
            foreach (GridViewRow row in grdIndentDetail.Rows)
            {
                Label txtAmount = (Label)row.FindControl("txtAmount");
                FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
            }
            if (grdIndentDetail.Rows.Count > 0)
            {
                Label txtFooterAmount = (Label)grdIndentDetail.FooterRow.FindControl("txtFooterAmount");
                txtFooterAmount.Text = FinalTotal.ToString();
            }
            getReqDate();
            AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
            lblAmountInWords.Text = oRupeesToWord.changeNumericToWords(FinalTotal);
        }
        catch
        {
            throw;
        }
    }

    private void CreateIndentDetailTable()
    {
        try
        {
            dtIndentDetail = new DataTable();
            dtIndentDetail.Columns.Add("UniqueId", typeof(int));
            dtIndentDetail.Columns.Add("IndentDetailNumber", typeof(int));
            dtIndentDetail.Columns.Add("IndentNumber", typeof(string));
            dtIndentDetail.Columns.Add("YARN_CODE", typeof(string));
            dtIndentDetail.Columns.Add("YARN_DESC", typeof(string));
            dtIndentDetail.Columns.Add("HSN_CODE", typeof(string));
            dtIndentDetail.Columns.Add("currentStock", typeof(double));
            dtIndentDetail.Columns.Add("MIN_STOCK_LVL", typeof(double));
            dtIndentDetail.Columns.Add("Min_Procure_days", typeof(double));
            dtIndentDetail.Columns.Add("OP_RATE", typeof(double));
            dtIndentDetail.Columns.Add("RQST_QTY", typeof(double));
            dtIndentDetail.Columns.Add("VC_UNITNAME", typeof(string));
            dtIndentDetail.Columns.Add("Amount", typeof(double));
            dtIndentDetail.Columns.Add("DPT_REMARK", typeof(string));
            dtIndentDetail.Columns.Add("SHADE_CODE", typeof(string));
            dtIndentDetail.Columns.Add("SHADE_FAMILY", typeof(string));
            dtIndentDetail.Columns.Add("LOT_NO", typeof(string));
            dtIndentDetail.Columns.Add("GRADE", typeof(string));


            ViewState["dtIndentDetail"] = dtIndentDetail;
        }
        catch
        {
            throw;
        }
    }

    protected void grdIndentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {
                FillDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "indentDelete")
            {

                DeleteIndentDetailRow(UniqueId);
                BindIndentDetailGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing/ deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            DataView dv = new DataView(dtIndentDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                txtItemCode.Enabled = false;
                txtItemCode.SelectedText = (dv[0]["YARN_CODE"].ToString());
                txtItemCodeLabel.Text = dv[0]["YARN_CODE"].ToString();
                txtItemDesc.Text = dv[0]["YARN_DESC"].ToString();
                txtHSNCODE.Text = dv[0]["HSN_CODE"].ToString();
                txtCurrentStock.Text = dv[0]["currentStock"].ToString();
                //txtMinStockLevel.Text = dv[0]["MIN_STOCK_LVL"].ToString();
                txtOpeningRate.Text = dv[0]["OP_RATE"].ToString();
                txtRequestQty.Text = dv[0]["RQST_QTY"].ToString();
                txtUnitName.Text = dv[0]["VC_UNITNAME"].ToString();
                txtAmount.Text = dv[0]["Amount"].ToString();
                txtRemark.Text = dv[0]["DPT_REMARK"].ToString();                
                txtShadeName.Text = dv[0]["SHADE_CODE"].ToString();
                txtShadeFamilyName.Text = dv[0]["SHADE_FAMILY"].ToString();               
                setShadeFamilyCombo(dv[0]["SHADE_FAMILY"].ToString().Trim(), dv[0]["SHADE_CODE"].ToString());

                string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GREY_LOT_NO'))         AND OTHER_INFO LIKE '" + txtItemCodeLabel.Text + "'     AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression = " order by MST_CODE asc";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", SortExpression, "", "%", "");
                txtLotNo.DataSource = data;
                txtLotNo.DataTextField = "MST_CODE";
                txtLotNo.DataValueField = "MST_CODE";
                txtLotNo.DataBind();
                foreach (ComboBoxItem item in txtLotNo.Items)
                {
                    if (item.Text == dv[0]["LOT_NO"].ToString())
                    {
                        txtLotNo.SelectedIndex = txtLotNo.Items.IndexOf(item);
                        break;
                    }
                }
                string CommandText2 = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GRADE'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression2 = " order by MST_CODE asc";
                DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, "", SortExpression2, "", "%", "");
                txtGrade.DataSource = data2;
                txtGrade.DataTextField = "MST_CODE";
                txtGrade.DataValueField = "MST_CODE";
                txtGrade.DataBind();
                foreach (ComboBoxItem item in txtGrade.Items)
                {
                    if (item.Text == dv[0]["GRADE"].ToString())
                    {
                        txtGrade.SelectedIndex = txtGrade.Items.IndexOf(item);
                        break;
                    }
                }

                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteIndentDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (grdIndentDetail.Rows.Count == 1)
            {
                dtIndentDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtIndentDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
                ViewState["dtIndentDetail"] = dtIndentDetail;
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId,string SHADE,string SHADE_FAMILY)
    {
        bool Result = false;
        try
        {         
            foreach (GridViewRow grdRow in grdIndentDetail.Rows)
            {
                Label txtItemCode1 = (Label)grdRow.FindControl("txtItemCode");
                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                Label txtSHADE = (Label)grdRow.FindControl("txtShadeCode");
                Label txtSHADE_FAMILY = (Label)grdRow.FindControl("txtShadeFamily");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtItemCode1.Text.Trim() == ItemCode && UniqueId != iUniqueId && SHADE == txtSHADE.Text.Trim() && SHADE_FAMILY == txtSHADE_FAMILY.Text.Trim())
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

    protected void txtRequestQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtRequestQty;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {

                    double OpeningRate = 0f;
                    if (double.TryParse(CommonFuction.funFixQuotes(txtOpeningRate.Text.Trim()), out OpeningRate))
                    {
                        double Total = (OpeningRate) * (double.Parse(RequestQTY.ToString()));
                        txtAmount.Text = Total.ToString();
                        FinalTotal = FinalTotal + Total;
                        txtRemark.Focus();
                        AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
                        lblAmountInWords.Text = oRupeesToWord.changeNumericToWords(FinalTotal.ToString());

                    }
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in entering request quantity.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private int GetItemDetailByItemCode(string ItemCode, out string Description, out string UOM, out double CurrentStock, out double MinStockLevel, out double OpeningRate, out double Min_Procure_days, out string YARN_SHADE)
    {
        int iRecordFound = 0;
        Description = "";
        UOM = "KGS";
        CurrentStock = 0;
        MinStockLevel = 0;
        OpeningRate = 0;
        Min_Procure_days = 0;
        YARN_SHADE = "";
        try
        {
            DataTable dts = SaitexBL.Interface.Method.YRN_INT_MST.GetItemDetailByItemCode(oUserLoginDetail.DT_STARTDATE.Year, ItemCode, oUserLoginDetail.CH_BRANCHCODE,ddlLocation.SelectedValue,ddlStore.SelectedValue,cmbShade.SelectedText,cmbShade.SelectedValue);
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["YARN_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UNIT_NAME"].ToString().Trim();               
                double.TryParse(dts.Rows[0]["currentStock"].ToString().Trim(), out CurrentStock);
                double.TryParse(dts.Rows[0]["MIN_STOCK"].ToString().Trim(), out MinStockLevel);
                double.TryParse(dts.Rows[0]["OP_RATE"].ToString().Trim(), out OpeningRate);
                double.TryParse(dts.Rows[0]["MIN_PROCURE_DAYS"].ToString().Trim(), out Min_Procure_days);
                iRecordFound = dts.Rows.Count;
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    protected void txtIndentNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;
            int iRecordFound = GetdataByIndentNumber(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            BindIndentDetailGrid();
            if (iRecordFound == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Indent Number');", true);

                txtIndentNumber.Focus();
                txtIndentNumber.Text = "";

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data indent updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private int GetdataByIndentNumber(string IndentNumber)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dts = SaitexBL.Interface.Method.YRN_INT_MST.SelectByIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "GEN", int.Parse(IndentNumber), oUserLoginDetail.VC_DEPARTMENTCODE,oUserLoginDetail.DT_STARTDATE.Year);

            if (dts != null && dts.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtCommentComment.Text = dts.Rows[0]["CONF_COMMENT"].ToString().Trim();
                txtIndentDate.Text = DateTime.Parse(dts.Rows[0]["IND_DATE"].ToString().Trim()).ToShortDateString();
                txtPreparedBy.Text = dts.Rows[0]["PREP_BY"].ToString().Trim();
                txtRequiredBefore.Text = DateTime.Parse(dts.Rows[0]["REQD_DATE"].ToString().Trim()).ToShortDateString();
                txtDepartment.Text = dts.Rows[0]["DEPT_NAME"].ToString().Trim();
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(dts.Rows[0]["LOCATION"].ToString()));
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByText(dts.Rows[0]["STORE"].ToString()));
            }

            if (iRecordFound == 1)
            {
                DataTable dtTemp = GetItemIndentTrasaction(IndentNumber.Trim());
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! Indent already approved. Modification not allowed.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();

                    txtIndentNumber.ReadOnly = false;
                    txtIndentNumber.AutoPostBack = true;
                    txtIndentNumber.Text = "";
                    txtIndentNumber.Focus();

                    lblMode.Text = "Update";
                    tdSave.Visible = false;
                    tdUpdate.Visible = true;
                    tdDelete.Visible = false;
                    ActivateUpdateMode();
                    RefreshDetailRow();
                }
            }
            return iRecordFound;
        }
        catch (OracleException ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            return iRecordFound;
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            return iRecordFound;
        }
    }

    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {

                    DataRow dr = dtIndentDetail.NewRow();
                    dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                    dr["IndentDetailNumber"] = 0;
                    dr["IndentNumber"] = drTemp["IND_NUMB"];
                    dr["YARN_CODE"] = drTemp["YARN_CODE"];
                    dr["YARN_DESC"] = drTemp["YARN_DESC"];
                    dr["HSN_CODE"] = drTemp["HSN_CODE"];
                    dr["currentStock"] = drTemp["currentStock"];
                    dr["MIN_STOCK_LVL"] = drTemp["MIN_STOCK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];
                    dr["RQST_QTY"] = drTemp["RQST_QTY"];
                    dr["VC_UNITNAME"] = drTemp["UNIT_NAME"];
                    dr["Amount"] = drTemp["iValue"];
                    dr["DPT_REMARK"] = drTemp["DPT_REMARK"];
                    dr["Min_Procure_days"] = drTemp["MIN_PROCURE_DAYS"];
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"];
                    dr["SHADE_FAMILY"] = drTemp["SHADE_FAMILY"];
                    dr["LOT_NO"] = drTemp["LOT_NO"];
                    dr["GRADE"] = drTemp["GRADE"];
                    FinalTotal = FinalTotal + double.Parse(drTemp["iValue"].ToString());
                    dtIndentDetail.Rows.Add(dr);

                }
                dtTemp = null;
                ViewState["dtIndentDetail"] = dtIndentDetail;
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetItemIndentTrasaction(string strIndentNum)
    {
        try
        {
            DataTable dtTemp = SaitexBL.Interface.Method.YRN_INT_MST.Select_TransactionByIndentNumber(oUserLoginDetail.DT_STARTDATE.Year, int.Parse(strIndentNum), oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, "GEN");

            return dtTemp;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            string URL = "~/Module/Yarn/SalesWork/Reports/YarnPrintIndent1.aspx";
            Response.Redirect(URL);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page refresh.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
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
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["dtIndentDetail"] != null)
                    dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
                if (dtIndentDetail.Rows.Count > 0)
                {
                    SaveIndentData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Items selected. Please enter item detail');", true);
                }

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (dtIndentDetail.Rows.Count > 0)
            {
                UpdateIndentData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Items selected. Please enter item detail');", true);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtIndentNumber.Text != null)
            {

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting indent.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            txtIndentNumber.Text = "";
            lblMode.Text = "Update";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = false;
            RefreshDetailRow();
            ddlIndentNumber.Visible = true;
            txtIndentNumber.Visible = false;
            ActivateUpdateMode();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in activating update mode.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

     protected void SaveIndentData()
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IND_MST oYRN_IND_MST = new SaitexDM.Common.DataModel.YRN_IND_MST();
            oYRN_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IND_MST.IND_TYPE = "GEN";
            oYRN_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oYRN_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oYRN_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oYRN_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtCommentComment.Text.Trim());
            oYRN_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oYRN_IND_MST.STATUS = true;
            oYRN_IND_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IND_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IND_MST.STORE = ddlStore.SelectedValue;
            int Ind_numb = 0;

            bool Result = SaitexBL.Interface.Method.YRN_INT_MST.Insert(oYRN_IND_MST, dtIndentDetail, out Ind_numb);
            if (Result)
            {
                InitialisePage();
                string msg = "Indent Number " + Ind_numb + " Successfully Saved.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent saving Failed');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void UpdateIndentData()
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IND_MST oYRN_IND_MST = new SaitexDM.Common.DataModel.YRN_IND_MST();
            oYRN_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IND_MST.IND_TYPE = "GEN";
            oYRN_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oYRN_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oYRN_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oYRN_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtCommentComment.Text.Trim());
            oYRN_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oYRN_IND_MST.STATUS = true;
            oYRN_IND_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IND_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IND_MST.STORE = ddlStore.SelectedValue;
            bool Result = SaitexBL.Interface.Method.YRN_INT_MST.Update(oYRN_IND_MST, dtIndentDetail);
            if (Result)
            {
                InitialisePage();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent updated successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent updation Failed');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    private void RefreshDetailRow()
    {
        try
        {
            txtItemCode.SelectedIndex = -1;           
            cmbShade.SelectedIndex = -1;
            txtItemCode.Enabled = true;
            txtItemCodeLabel.Text = string.Empty;
            txtItemDesc.Text = "";
            txtOpeningRate.Text = "";
            txtRemark.Text = "";
            txtRequestQty.Text = "";
            txtAmount.Text = "";
            txtCurrentStock.Text = "";
            txtUnitName.Text = "KGS";
            txtLotNo.SelectedIndex = -1;
            txtGrade.SelectedIndex = -1;
            txtShadeFamilyName.Text = string.Empty;
            txtShadeName.Text = string.Empty;
            ViewState["UniqueId"] = null;
            txtHSNCODE.Text = "";

        }
        catch
        {
            throw;
        }
    }

    private void getReqDate()
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (dtIndentDetail != null && dtIndentDetail.Rows.Count > 0)
            {
                DateTime Temp = System.DateTime.Now.Date;
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    Int64 Min_Procure_Days = 0;
                    Int64.TryParse(dr["Min_Procure_days"].ToString(), out Min_Procure_Days);



                    DateTime IndentDate = DateTime.Parse(txtIndentDate.Text.Trim());
                    DateTime NewDate = IndentDate.AddDays(Min_Procure_Days);
                    int Val = Temp.CompareTo(NewDate);
                    if (Val == -1)
                    {
                        Temp = NewDate;
                    }
                }
                txtRequiredBefore.Text = Temp.ToShortDateString();
            }
            else
            {
            }
        }
        catch
        {
            throw;
        }
    }

    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (dtIndentDetail.Rows.Count < 55)
            {
                if (txtItemCodeLabel.Text != "" && txtRequestQty.Text != "" && ddlStore.SelectedValue != "")
                {
                    if (txtShadeName.Text != string.Empty)
                    {
                        int UniqueId = 0;
                        if (ViewState["UniqueId"] != null)
                            UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                        bool bb = SearchItemCodeInGrid(txtItemCode.SelectedValue.Trim(), UniqueId,cmbShade.SelectedValue,cmbShade.SelectedText );
                        if (!bb)
                        {
                            double currentStock = 0;
                            double openingRate = 0;
                            double requestQty = 0;
                            double minprocuredays = 0;

                            double.TryParse(txtCurrentStock.Text, out currentStock);
                            double.TryParse(txtOpeningRate.Text, out openingRate);
                            double.TryParse(txtRequestQty.Text, out requestQty);
                            double.TryParse(lblMin_Procure_days.Text, out minprocuredays);
                            if (requestQty > 0)
                            {
                                

                                if (UniqueId > 0)
                                {
                                    DataView dv = new DataView(dtIndentDetail);
                                    dv.RowFilter = "UniqueId=" + UniqueId;
                                    if (dv.Count > 0)
                                    {
                                       
                                        dv[0]["IndentNumber"] = txtIndentNumber.Text;
                                        dv[0]["YARN_CODE"] = txtItemCodeLabel.Text.Trim();
                                        dv[0]["YARN_DESC"] = txtItemDesc.Text.Trim();
                                        dv[0]["HSN_CODE"] = txtHSNCODE.Text.Trim();
                                        dv[0]["currentStock"] = currentStock;
                                        dv[0]["OP_RATE"] = openingRate;
                                        dv[0]["RQST_QTY"] = requestQty;
                                        dv[0]["VC_UNITNAME"] = txtUnitName.Text.Trim();
                                        dv[0]["Amount"] = openingRate * requestQty;
                                        dv[0]["DPT_REMARK"] = txtRemark.Text.Trim();
                                        dv[0]["SHADE_FAMILY"] = txtShadeFamilyName.Text.Trim();
                                        dv[0]["SHADE_CODE"] = txtShadeName.Text.Trim();
                                        dv[0]["LOT_NO"] = txtLotNo.SelectedValue;
                                        dv[0]["GRADE"] = txtGrade.SelectedValue;
                                        FinalTotal = FinalTotal + openingRate * requestQty;
                                        dtIndentDetail.AcceptChanges();
                                    }
                                }
                                else
                                {

                                    DataRow dr = dtIndentDetail.NewRow();
                                    dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                                    dr["IndentDetailNumber"] = 0;
                                    dr["IndentNumber"] = txtIndentNumber.Text;
                                    dr["YARN_CODE"] = txtItemCodeLabel.Text.Trim();
                                    dr["YARN_DESC"] = txtItemDesc.Text.Trim();
                                    dr["HSN_CODE"] = txtHSNCODE.Text.Trim();
                                    dr["currentStock"] = currentStock;
                                    dr["OP_RATE"] = openingRate;
                                    dr["RQST_QTY"] = requestQty;
                                    dr["Min_Procure_days"] = minprocuredays;
                                    dr["VC_UNITNAME"] = txtUnitName.Text.Trim();
                                    dr["Amount"] = openingRate * requestQty; ;
                                    dr["DPT_REMARK"] = txtRemark.Text.Trim();
                                    dr["SHADE_FAMILY"] = txtShadeFamilyName.Text.Trim();
                                    dr["SHADE_CODE"] = txtShadeName.Text.Trim();
                                    dr["LOT_NO"] = txtLotNo.SelectedValue;
                                    dr["GRADE"] = txtGrade.SelectedValue;
                                    FinalTotal = FinalTotal + openingRate * requestQty; 
                                    dtIndentDetail.Rows.Add(dr);
                                    lblMin_Procure_days.Text = "";
                                }
                                RefreshDetailRow();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Yarn Code and Shade Code Should Be Diffrent');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Select Shade');", true);



                    }

                }
                else if (txtItemCode.SelectedValue == "SELECT")
                {
                    CommonFuction.ShowMessage("Item Code Required");
                }
                else if (txtRequestQty.Text == "")
                {
                    CommonFuction.ShowMessage("Quantity can not be zero");
                }
                else if (ddlStore.SelectedValue == "")
                {
                    CommonFuction.ShowMessage("Please Select Store");
                }

                ViewState["dtIndentDetail"] = dtIndentDetail;
                BindIndentDetailGrid();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Indent.");
            }
            getReqDate();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving item detail row.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearing item detail row.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlIndentNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetIndents(e.Text.ToUpper(), e.ItemsOffset, 10);

            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "IND_NUMB";
            thisTextBox.DataValueField = "IND_NUMB";
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading indent for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlIndentNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string IndentNo = ddlIndentNumber.SelectedValue.Trim();
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;
            int iRecordFound = GetdataByIndentNumber(CommonFuction.funFixQuotes(IndentNo));
            BindIndentDetailGrid();
            if (iRecordFound == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Indent Number');", true);
                txtIndentNumber.Focus();
                txtIndentNumber.Text = "";
            }
            else
            {
                txtIndentNumber.Text = IndentNo;

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading indent data for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetIndents(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string CommandText = " SELECT *  FROM (SELECT a.IND_NUMB,   TO_CHAR (a.IND_DATE, 'DD/MM/YYYY') AS IND_DATE,   DM.DEPT_NAME,   A.DEPT_CODE,   UM.USER_NAME,   A.PREP_BY          FROM YRN_IND_MST a, CM_DEPT_MST dm, cm_USER_MST UM         WHERE     A.COMP_CODE = :COMP_CODE   AND A.BRANCH_CODE = :BRANCH_CODE   AND A.IND_TYPE = :IND_TYPE   AND A.DEPT_CODE = DM.DEPT_CODE   AND A.PREP_BY = UM.USER_CODE   AND a.dept_code = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "'   AND a.PREP_BY = '" + oUserLoginDetail.UserCode + "'  AND A.YEAR='"+oUserLoginDetail.DT_STARTDATE.Year +"'  ) asd";
            string WhereClause = "  where Ind_numb like:searchQuery";
            string SortExpression = " order by ind_numb desc, ind_date desc";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "GEN", CommandText, WhereClause, SortExpression, "", "%");
            return dt;




        }
        catch
        {
            throw;
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            ddlIndentNumber.Visible = true;

            txtIndentNumber.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void ActivateSaveMode()
    {
        try
        {
            ddlIndentNumber.Visible = false;


            txtIndentNumber.Visible = true;
        }
        catch
        {
            throw;
        }
    }

    protected void Item_LOV_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtItemCode.Items.Clear();
                txtItemCode.SelectedText = "YARN_CODE";
                txtItemCode.SelectedValue = "trn";
                txtItemCode.DataSource = data;
                txtItemCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            //string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE,HSN_CODE, (YARN_DESC||'@'||HSN_CODE) As trn FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";

            string CommandText = "   SELECT   *    FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC,M.YARN_TYPE, M.HSN_CODE,(M.YARN_DESC || '@' || M.HSN_CODE) AS trn, A.ASS_YARN_CODE, A.ASS_YARN_DESC FROM   YRN_MST M, YRN_ASSOCATED_MST A  WHERE   M.YARN_CODE = A.YARN_CODE AND (   M.YARN_CODE LIKE :SearchQuery OR M.YARN_TYPE LIKE :SearchQuery  OR M.YARN_DESC LIKE :SearchQuery OR A.ASS_YARN_CODE LIKE :SearchQuery OR A.ASS_YARN_DESC LIKE :SearchQuery) ORDER BY   A.ASS_YARN_CODE) asd   WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                //whereClause += " AND YARN_code NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC ,HSN_CODE(YARN_DESC||'@'||HSN_CODE) As trn FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";


                whereClause += " AND ASS_YARN_CODE NOT IN (SELECT   ASS_YARN_CODE FROM   (  SELECT   M.YARN_CODE,     M.YARN_DESC,    M.YARN_TYPE,   M.HSN_CODE, (M.YARN_DESC || '@' || M.HSN_CODE)  AS trn,    A.ASS_YARN_CODE,   A.ASS_YARN_DESC  FROM   YRN_MST M, YRN_ASSOCATED_MST A   WHERE   M.YARN_CODE = A.YARN_CODE   AND (   M.YARN_CODE LIKE :SearchQuery   OR M.YARN_TYPE LIKE :SearchQuery  OR M.YARN_DESC LIKE :SearchQuery    OR A.ASS_YARN_CODE LIKE   :SearchQuery      OR A.ASS_YARN_DESC LIKE     :SearchQuery)  ORDER BY   M.YARN_CODE) asd   WHERE   ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY ASS_YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd WHERE YARN_TYPE <> 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY YARN_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void Item_LOV_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string[] trn1 = txtItemCode.SelectedValue.Split('@');
            txtItemDesc.Text = trn1[0];
            txtItemCodeLabel.Text = txtItemCode.SelectedText;
            txtHSNCODE.Text = trn1[1];
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }


    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetShadeItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));

        }
    }

    protected DataTable GetShadeItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_NAME ||'@' ||T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND T.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND T.PRODUCT_TYPE = 'YARN'  AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetShadeItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE LIKE '%YARN%' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    private void BindDropDown(DropDownList ddl)
    {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.Items.Clear();
            ddl.DataSource = dt;
            ddl.DataTextField = "MST_DESC";
            ddl.DataValueField = "MST_DESC";
            ddl.DataBind();
        }
        else
        {
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

        }
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

    }

    private void BindDepartment(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    protected void cmbShade_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        txtShadeFamilyName.Text = cmbShade.SelectedText;
        txtShadeName.Text = cmbShade.SelectedValue;

        string Description = "";
        string UOM = "";
        double CurrentStock = 0;
        double MinStockLevel = 0;
        double OpeningRate = 0;
        double Min_Procure_days = 0;
        int UniqueId = 0;
        string YARN_SHADE = "";

        if (ViewState["UniqueId"] != null)
            UniqueId = int.Parse(ViewState["UniqueId"].ToString());     
      
        int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(txtItemCodeLabel.Text.Trim()), out Description, out UOM, out CurrentStock, out MinStockLevel, out OpeningRate, out Min_Procure_days, out YARN_SHADE);
        if (iRecordFound > 0)
        {
            txtOpeningRate.Text = OpeningRate.ToString();           
            txtCurrentStock.Text = CurrentStock.ToString();           
            txtUnitName.Text = UOM;
            txtRequestQty.Focus();
            lblMin_Procure_days.Text = Min_Procure_days.ToString();
           
        }
        else
        {
            txtOpeningRate.Text = "0";            
            txtRequestQty.Text = "0";
            txtAmount.Text = "0";
            txtCurrentStock.Text = "0";
           
           
        }
            


    }


    public void setShadeFamilyCombo(string shade_family, string shade)
    {

        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery)", "", "", "", "%", "");
        cmbShade.DataSource = data;
        cmbShade.DataTextField = "SHADE_FAMILY_NAME";
        cmbShade.DataValueField = "SHADE_NAME";
        cmbShade.DataBind();
        foreach (ComboBoxItem dl in cmbShade.Items)
        {
            if (dl.Text == shade_family && dl.Value == shade)
            {
                cmbShade.SelectedIndex = cmbShade.Items.IndexOf(dl);
                break;
            }
        }
    }
}
