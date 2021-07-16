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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;

public partial class Module_Production_Controls_LOT_QTY_TRANSFER : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string TRN_TYPE="PRD01";
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            InitialisePage();
        }
    }

    private void InitialisePage()
    {
        try
        {
         
            Blankrecords();
            BindNewTRNNum();
            BindShift();
            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();       
        }
        catch
        {
            throw;
        }
    }
    private void BindShift()
    {
        try
        {
            DataTable dtShift = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftMaster();
            if (dtShift != null && dtShift.Rows.Count > 0)
            {
                ddlReceiptShift.DataSource = dtShift;
                ddlReceiptShift.DataTextField = "SFT_NAME";
                ddlReceiptShift.DataValueField = "SFT_NAME";
                ddlReceiptShift.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindNewTRNNum()
    {
        try
        {          
          
            txtTRNNUMBer.Text = SaitexBL.Interface.Method.YRN_PROD_MST.GetMaxProdTransfer(TRN_TYPE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
        }
        catch
        {
            throw;
        }
    }

    private void Blankrecords()
    {
        try
        {
            lblMode.Text = "Save";
            txtMRNDate.Text = "";
            txtTRNNUMBer.Text = "";
            cmbPOITEM.SelectedIndex = -1;
            cmbTOPOITEM.SelectedIndex = -1;
            txtMergeNO.Text = string.Empty;
            txtProdQty.Text = string.Empty;
            txtTrnQty.Text = string.Empty;
            
            txtTRNNUMBer.ReadOnly = true;
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
               
            }           
            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            ActivateSaveMode();
        }
        catch
        {
            throw;
        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text, e.ItemsOffset);
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "LOT_NUMBER";
            cmbPOITEM.DataValueField = "po_Item_trn";
            cmbPOITEM.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetPODataCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Po Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetPOData(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (     SELECT   DISTINCT  (   PT.LOT_NUMBER || '@'  || PT.MERGE_NO      || '@' || YA.YARN_CODE|| '@' || YA.ASS_YARN_DESC  || '@' || YM.YARN_SHADE  || '@' || (NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0))     || '@'    || LM.MACHINE_NAME)                        po_Item_trn,                     PT.MERGE_NO,                     PT.LOT_NUMBER,                     YA.YARN_CODE ARTICLE_CODE,                     YA.ASS_YARN_DESC ARTICLE_DESC,                     NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0) AS QTY_REM              FROM   YRN_PROD_DOFF_OP_BAL pt,                     YRN_LOT_MAKING LM,                     YRN_ASSOCATED_MST YA    ,YRN_MST YM         WHERE       NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0) > 0                     AND PT.TRN_TYPE = 'PRD01'                    AND LM.LOT_NO = PT.LOT_NUMBER                     AND LM.MERGE_NO = PT.MERGE_NO      AND YA.ASS_YARN_CODE = LM.FINISHED_DENIER         AND YA.YARN_CODE=YM.YARN_CODE            AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'      AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'                     AND (   UPPER (YA.YARN_CODE) LIKE :SearchQuery                   OR UPPER (YA.ASS_YARN_DESC) LIKE :SearchQuery                OR UPPER (PT.LOT_NUMBER) LIKE :SearchQuery               OR UPPER (PT.MERGE_NO) LIKE :SearchQuery)          ORDER BY   PT.LOT_NUMBER) WHERE   ROWNUM <= 15";

            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND LOT_NUMBER  NOT IN  ( SELECT LOT_NUMBER FROM ( SELECT   DISTINCT  (   PT.LOT_NUMBER || '@'  || PT.MERGE_NO      || '@' || YA.YARN_CODE|| '@' || YA.ASS_YARN_DESC || '@' || YM.YARN_SHADE   || '@' || (NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0))    || '@'    || LM.MACHINE_NAME )                        po_Item_trn,                     PT.MERGE_NO,                     PT.LOT_NUMBER,                     YA.YARN_CODE ARTICLE_CODE,                     YA.ASS_YARN_DESC ARTICLE_DESC,                     NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0) AS QTY_REM              FROM   YRN_PROD_DOFF_OP_BAL pt,                     YRN_LOT_MAKING LM,                     YRN_ASSOCATED_MST YA   , YRN_MST YM          WHERE       NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0) > 0                     AND PT.TRN_TYPE = 'PRD01'                    AND LM.LOT_NO = PT.LOT_NUMBER                     AND LM.MERGE_NO = PT.MERGE_NO      AND YA.ASS_YARN_CODE = LM.FINISHED_DENIER         AND YA.YARN_CODE=YM.YARN_CODE            AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'      AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'                     AND (   UPPER (YA.YARN_CODE) LIKE :SearchQuery                   OR UPPER (YA.ASS_YARN_DESC) LIKE :SearchQuery                OR UPPER (PT.LOT_NUMBER) LIKE :SearchQuery               OR UPPER (PT.MERGE_NO) LIKE :SearchQuery)          ORDER BY   PT.LOT_NUMBER ) WHERE ROWNUM <='" + startOffset + "' )";

            }
            string SortExpression = " ";
            string SearchQuery = text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetPODataCount(string text)
    {


        string CommandText = "SELECT   DISTINCT  (   PT.LOT_NUMBER || '@'  || PT.MERGE_NO      || '@' || YA.YARN_CODE|| '@' || YA.ASS_YARN_DESC    || '@' || (NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0))     )                        po_Item_trn,                     PT.MERGE_NO,                     PT.LOT_NUMBER,                     YA.YARN_CODE ARTICLE_CODE,                     YA.ASS_YARN_DESC ARTICLE_DESC,                     NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0) AS QTY_REM              FROM   YRN_PROD_DOFF_OP_BAL pt,                     YRN_LOT_MAKING LM,                     YRN_ASSOCATED_MST YA             WHERE       NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0) > 0                     AND PT.TRN_TYPE = 'PRD01'                    AND LM.LOT_NO = PT.LOT_NUMBER                     AND LM.MERGE_NO = PT.MERGE_NO      AND YA.ASS_YARN_CODE = LM.FINISHED_DENIER                     AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'      AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'                     AND (   UPPER (YA.YARN_CODE) LIKE :SearchQuery                   OR UPPER (YA.ASS_YARN_DESC) LIKE :SearchQuery                OR UPPER (PT.LOT_NUMBER) LIKE :SearchQuery               OR UPPER (PT.MERGE_NO) LIKE :SearchQuery) ";
        string whereClause = string.Empty;
        string SortExpression = "";
        string SearchQuery = text + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = cmbPOITEM.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            txtMergeNO.Text  = arrString[1].ToString();
            txtProdQty.Text = arrString[5].ToString();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Po Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbTOPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetToPOData(e.Text, e.ItemsOffset);
            cmbTOPOITEM.Items.Clear();
            cmbTOPOITEM.DataSource = data;
            cmbTOPOITEM.DataTextField = "LOT_NUMBER";
            cmbTOPOITEM.DataValueField = "LOT_NUMBER";
            cmbTOPOITEM.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetToPODataCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Po Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetToPOData(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT LM.MERGE_NO,LM.LOT_NO LOT_NUMBER,  YA.YARN_CODE ARTICLE_CODE,      YA.ASS_YARN_DESC ARTICLE_DESC FROM  YRN_LOT_MAKING LM,  YRN_ASSOCATED_MST YA,    YRN_MST YM       WHERE  YA.ASS_YARN_CODE = LM.FINISHED_DENIER                     AND YA.YARN_CODE = YM.YARN_CODE        AND LM.CONF_FLAG=1              AND LM.MERGE_NO='" + txtMergeNO.Text + "'        AND LM.LOT_NO NOT IN ('" + cmbPOITEM.SelectedText + "')                   AND (   UPPER (YA.YARN_CODE) LIKE :SearchQuery                          OR UPPER (YA.ASS_YARN_DESC) LIKE :SearchQuery                    OR UPPER (LM.LOT_NO) LIKE :SearchQuery                          OR UPPER (LM.MERGE_NO) LIKE :SearchQuery)          ORDER BY   LM.LOT_NO) WHERE   ROWNUM <= 15";

            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND LOT_NUMBER  NOT IN  ( SELECT LOT_NUMBER FROM ( SELECT   DISTINCT LM.MERGE_NO,LM.LOT_NO LOT_NUMBER,  YA.YARN_CODE ARTICLE_CODE,      YA.ASS_YARN_DESC ARTICLE_DESC FROM  YRN_LOT_MAKING LM,  YRN_ASSOCATED_MST YA,    YRN_MST YM       WHERE  YA.ASS_YARN_CODE = LM.FINISHED_DENIER                     AND YA.YARN_CODE = YM.YARN_CODE           AND LM.CONF_FLAG=1           AND LM.MERGE_NO='" + txtMergeNO.Text + "'         AND LM.LOT_NO NOT IN ('" + cmbPOITEM.SelectedText + "')                      AND (   UPPER (YA.YARN_CODE) LIKE :SearchQuery                          OR UPPER (YA.ASS_YARN_DESC) LIKE :SearchQuery                    OR UPPER (LM.LOT_NO) LIKE :SearchQuery                          OR UPPER (LM.MERGE_NO) LIKE :SearchQuery)          ORDER BY   LM.LOT_NO) WHERE ROWNUM <='" + startOffset + "' )";

            }
            string SortExpression = " ";
            string SearchQuery = text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetToPODataCount(string text)
    {


        string CommandText = "SELECT   DISTINCT LM.MERGE_NO,LM.LOT_NO LOT_NUMBER,  YA.YARN_CODE ARTICLE_CODE,      YA.ASS_YARN_DESC ARTICLE_DESC FROM  YRN_LOT_MAKING LM,  YRN_ASSOCATED_MST YA,    YRN_MST YM       WHERE  YA.ASS_YARN_CODE = LM.FINISHED_DENIER                     AND YA.YARN_CODE = YM.YARN_CODE               AND LM.CONF_FLAG=1       AND LM.MERGE_NO='" + txtMergeNO.Text + "'                  AND (   UPPER (YA.YARN_CODE) LIKE :SearchQuery                          OR UPPER (YA.ASS_YARN_DESC) LIKE :SearchQuery                    OR UPPER (LM.LOT_NO) LIKE :SearchQuery                          OR UPPER (LM.MERGE_NO) LIKE :SearchQuery)";
        string whereClause = string.Empty;
        string SortExpression = "";
        string SearchQuery = text + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }




    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                SaveUpdate("INSERT");
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
           

            bool ReturnResult = false;

            int count = 0;
            msg = string.Empty;
            double prod_qty = 0;
            double trn_qty = 0;

            double.TryParse(txtProdQty.Text, out prod_qty);
            double.TryParse(txtTrnQty.Text, out trn_qty);
            if (txtTRNNUMBer.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Trn Number required.\r\n";
            }

            if (!string.IsNullOrEmpty(cmbPOITEM.SelectedValue ))
            {
                count += 1;
            }
            else
            {
                msg += @"#. From Lot Number required.\r\n";
            }
            if (!string.IsNullOrEmpty(txtMergeNO.Text ))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Merger No required.\r\n";
            }
            if (prod_qty > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Prod Qty Cannot be zero.\r\n";
            }
            if (!string.IsNullOrEmpty(cmbTOPOITEM.SelectedValue))
            {
                count += 1;
            }
            else
            {
                msg += @"#. To Lot No required.\r\n";
            }

            if (trn_qty>0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Trnsfer Qty Cannot be zero.\r\n";
            }
            if (prod_qty >= trn_qty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Trnsfer Qty Cannot be greater then prod qty.\r\n";
            }

            if (count == 7)
                ReturnResult = true;

            return ReturnResult;
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

            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                SaveUpdate("UPDATE");
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ActivateUpdateMode();

            lblMode.Text = "Update";
            txtTRNNUMBer.Text = "";
            tdUpdate.Visible = true;
            tdDelete.Visible = false ;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Update Mode.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Page Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ddlTRNNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetReceiving(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "TRN_NUMB";
            thisTextBox.DataValueField = "TRN_NUMB";
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Indent for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {


            string CommandText = "SELECT   *  FROM   (  SELECT   A.TRN_NUMB,   A.TRN_DATE    FROM   YRN_PROD_LOT_TRANSFER a             WHERE  A.TRN_TYPE='PRD01'                      AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'                     AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'          ORDER BY   TRN_NUMB DESC, TRN_DATE DESC) asd WHERE   (   TRN_NUMB LIKE :searchQuery          OR TRN_DATE LIKE :searchQuery       )         AND ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND TRN_NUMB NOT IN (SELECT   *  FROM   (  SELECT   A.TRN_NUMB,   A.TRN_DATE    FROM   YRN_PROD_LOT_TRANSFER a             WHERE  A.TRN_TYPE='PRD01'                      AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'                     AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'          ORDER BY   TRN_NUMB DESC, TRN_DATE DESC) asd WHERE   (   TRN_NUMB LIKE :searchQuery          OR TRN_DATE LIKE :searchQuery       )    AND ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = "    ORDER BY TRN_NUMB DESC, TRN_DATE DESC";
            string SearchQuery = text + "%";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", text + "%", "");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetReceivingCount(string text, int startOffset, int numberOfItems)
    {
        try
        {
         
            string CommandText = "SELECT   *  FROM   (  SELECT   A.TRN_NUMB,   A.TRN_DATE    FROM   YRN_PROD_LOT_TRANSFER a             WHERE  A.TRN_TYPE='PRD01'                      AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'                     AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'          ORDER BY   TRN_NUMB DESC, TRN_DATE DESC) asd WHERE   (   TRN_NUMB LIKE :searchQuery          OR TRN_DATE LIKE :searchQuery       )   ";
            string whereClause = string.Empty;
            string SortExpression = " ";
            string SearchQuery = text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", text + "%", "");
            return dt.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());          
            int iRecordFound = GetdataByTRNNUMBer(TRN_NUMBER);           
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            txtTRNNUMBer.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();

            txtTRNNUMBer.Visible = false;
            ddlTRNNumber.Visible = true;

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
            txtTRNNUMBer.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();

            txtTRNNUMBer.Visible = true;
            ddlTRNNumber.Visible = false;

        }
        catch
        {
            throw;
        }
    }

    private void SaveUpdate(string STATE)
    {
        try
        { 
          
         
            int TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            DateTime TRN_DATE=DateTime.Now;
            double TRN_QTY = 0;
            double PROD_QTY = 0;

            DateTime.TryParse(txtMRNDate.Text,out TRN_DATE);
            double.TryParse(txtProdQty.Text, out PROD_QTY);
            double.TryParse(txtTrnQty.Text, out TRN_QTY);
         
            bool result = SaitexBL.Interface.Method.YRN_PROD_MST.InsertUpdateProdTransfer( ref TRN_NUMB,  oUserLoginDetail.COMP_CODE,   oUserLoginDetail.CH_BRANCHCODE,  TRN_TYPE,   oUserLoginDetail.DT_STARTDATE.Year,  TRN_DATE, ddlReceiptShift.SelectedValue.Trim(), cmbPOITEM.SelectedText , cmbTOPOITEM.SelectedValue , txtMergeNO.Text ,  PROD_QTY, 0,  TRN_QTY, 0,  oUserLoginDetail.UserCode, STATE);
            if (result)
            {
                InitialisePage();
                string Msg = string.Empty;
                Msg += @"\r\n Trn Number : " + TRN_NUMB + " saved successfully.";
                CommonFuction.ShowMessage(Msg);

               
            }
            else
            {
                CommonFuction.ShowMessage("Data  Saving Failed");
            }

        }
        catch
        {
            throw;
        }
    }

    private int GetdataByTRNNUMBer(int TRNNUMBer)
    {
        int iRecordFound = 0;
        try
        {

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataByTRN_NUMBForProdTransfer(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                    iRecordFound = 1;   
                    txtMRNDate.Text = dt.Rows[0]["TRN_DATE"].ToString();
                    txtTRNNUMBer.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();
                    txtProdQty.Text=dt.Rows[0]["PROD_QTY"].ToString().Trim();
                    txtTrnQty.Text = dt.Rows[0]["TRN_QTY"].ToString().Trim();
                    txtMergeNO.Text = dt.Rows[0]["MERGE_NO"].ToString().Trim();

                    string CommandText = "SELECT   *  FROM   (     SELECT   DISTINCT  (   PT.LOT_NUMBER || '@'  || PT.MERGE_NO      || '@' || YA.YARN_CODE|| '@' || YA.ASS_YARN_DESC  || '@' || YM.YARN_SHADE  || '@' || (NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0))     || '@'    || LM.MACHINE_NAME)                        po_Item_trn,                     PT.MERGE_NO,                     PT.LOT_NUMBER,                     YA.YARN_CODE ARTICLE_CODE,                     YA.ASS_YARN_DESC ARTICLE_DESC,                     NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0) AS QTY_REM              FROM   YRN_PROD_DOFF_OP_BAL pt,                     YRN_LOT_MAKING LM,                     YRN_ASSOCATED_MST YA    ,YRN_MST YM         WHERE       NVL (PT.PROD_QTY_PACKING, 0) - NVL (PT.PACKING_QTY, 0) > 0                     AND PT.TRN_TYPE = 'PRD01'                    AND LM.LOT_NO = PT.LOT_NUMBER                     AND LM.MERGE_NO = PT.MERGE_NO      AND YA.ASS_YARN_CODE = LM.FINISHED_DENIER         AND YA.YARN_CODE=YM.YARN_CODE            AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'      AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'                     AND (   UPPER (YA.YARN_CODE) LIKE :SearchQuery                   OR UPPER (YA.ASS_YARN_DESC) LIKE :SearchQuery                OR UPPER (PT.LOT_NUMBER) LIKE :SearchQuery               OR UPPER (PT.MERGE_NO) LIKE :SearchQuery)          ORDER BY   PT.LOT_NUMBER)  ";
                    DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", "%", "");
                    cmbPOITEM.DataSource = data;
                    cmbPOITEM.DataTextField = "LOT_NUMBER";
                    cmbPOITEM.DataValueField = "po_Item_trn";
                    cmbPOITEM.DataBind();
                    foreach (ComboBoxItem item in cmbPOITEM.Items)
                    {
                        if (item.Text.Trim() == dt.Rows[0]["FROM_LOT_NUMBER"].ToString().Trim())
                        {
                            cmbPOITEM.SelectedIndex = cmbPOITEM.Items.IndexOf(item);
                            cmbPOITEM.SelectedText = item.Text.ToString();
                            cmbPOITEM.SelectedValue = item.Value.ToString();
                            break;
                        }
                    }

                    string CommandText1 = " SELECT   DISTINCT LM.MERGE_NO,LM.LOT_NO LOT_NUMBER,  YA.YARN_CODE ARTICLE_CODE,      YA.ASS_YARN_DESC ARTICLE_DESC FROM  YRN_LOT_MAKING LM,  YRN_ASSOCATED_MST YA,    YRN_MST YM       WHERE  YA.ASS_YARN_CODE = LM.FINISHED_DENIER                     AND YA.YARN_CODE = YM.YARN_CODE        AND LM.CONF_FLAG=1              AND LM.MERGE_NO='" + txtMergeNO.Text + "'                           AND (   UPPER (YA.YARN_CODE) LIKE :SearchQuery                          OR UPPER (YA.ASS_YARN_DESC) LIKE :SearchQuery                    OR UPPER (LM.LOT_NO) LIKE :SearchQuery                          OR UPPER (LM.MERGE_NO) LIKE :SearchQuery) ";
                    DataTable data1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, "", "", "", "%", "");
                    cmbTOPOITEM.DataSource = data1;
                    cmbTOPOITEM.DataTextField = "LOT_NUMBER";
                    cmbTOPOITEM.DataValueField = "LOT_NUMBER";
                    cmbTOPOITEM.DataBind();
                    foreach (ComboBoxItem item in cmbTOPOITEM.Items)
                    {
                        if (item.Value == dt.Rows[0]["TO_LOT_NUMBER"].ToString().Trim())
                        {
                            cmbTOPOITEM.SelectedIndex = cmbTOPOITEM.Items.IndexOf(item);
                            cmbTOPOITEM.SelectedText = item.Text.ToString();
                            cmbTOPOITEM.SelectedValue = item.Value.ToString();
                            break;
                        }
                    }
             

                
            }
           
            return iRecordFound;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            return iRecordFound;
        }
    }




    protected void txtTrnQty_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string msg = string.Empty;
            if (!ValidateFormForSavingOrUpdating(out msg))
            {
                CommonFuction.ShowMessage(msg);
            }
           
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}
