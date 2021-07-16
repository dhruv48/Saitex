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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;

public partial class Module_WorkOrder_Controls_Work_order_entry : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_WO_MST oOD_WO_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtWODetail = null;

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            ActivateSaveMode();
            TRANSPOTER.Visible = false;
            lblMode.Text = "You are In Save Mode";

            BindDelBranch();
            BindShadeCode();

            txtWONumber.Text = "";
            getWOMaxId();
            txtWODate.Text = System.DateTime.Now.Date.ToShortDateString();

            txtPartyCode.Text = string.Empty;
            txtJoberPartyAddress.Text = string.Empty;
            txtJoberPartyCode.Text = string.Empty;
            ddlPartyCode.SelectedIndex = -1;
            ddlJoberParty.SelectedIndex = -1;
            txtPartyAddress.Text = string.Empty;

            ddlTransporterCode.SelectedIndex = -1;
            txtTransporterCode.Text = string.Empty;
            txtTransporterAdd.Text = string.Empty;

            txtPayTerm.Text = string.Empty;

            ddlDelAdd.SelectedIndex = ddlDelAdd.Items.IndexOf(ddlDelAdd.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));

            txtRemarks.Text = "";
            txtInstructions.Text = "";

            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            if (dtWODetail == null)
                CreateWODetailTable();

            dtWODetail.Rows.Clear();

            BindWOTrntoGrid();

            RefreshDetailRow();
            Session["dtWOBOM"] = null;
            Session["dtWODicRate"] = null;
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
            txtWONumber.Visible = true;
            ddlWONumber.Visible = false;
            txtWONumber.ReadOnly = true;

            ddlWONumber.Items.Clear();
            ddlWONumber.SelectedIndex = -1;

            ddlProductType.Enabled = true;
            ddlWOType.Enabled = true;
            imgbtnSave.Visible = true;
            imgbtnUpdate.Visible = false;

        }
        catch
        {
            throw;
        }
    }

    private void getWOMaxId()
    {
        try
        {
            oOD_WO_MST = new SaitexDM.Common.DataModel.OD_WO_MST();
            oOD_WO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_WO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_WO_MST.PRODUCT_TYPE = ddlProductType.SelectedValue.Trim();
            oOD_WO_MST.WO_TYPE = ddlWOType.SelectedValue.Trim();
            oOD_WO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtWONumber.Text = (int.Parse(SaitexBL.Interface.Method.OD_WO_MST.GetNewWONo(oOD_WO_MST)) + 1).ToString();
        }
        catch
        {
            throw;
        }
    }

    private void BindDelBranch()
    {
        try
        {
            ddlDelAdd.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMasterByCompCode(oUserLoginDetail.COMP_CODE);
            ddlDelAdd.DataSource = dt;
            ddlDelAdd.DataTextField = "BRANCH_NAME";
            ddlDelAdd.DataValueField = "BRANCH_CODE";
            ddlDelAdd.DataBind();

            ddlDelAdd.SelectedIndex = ddlDelAdd.Items.IndexOf(ddlDelAdd.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));

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
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlshadeCode.DataSource = dt;
                ddlshadeCode.DataTextField = "SHADE_CODE";
                ddlshadeCode.DataValueField = "SHADE_CODE";
                ddlshadeCode.DataBind();
            }
            ddlshadeCode.SelectedIndex = ddlshadeCode.Items.IndexOf(ddlshadeCode.Items.FindByText("GREY"));
            
           
        }
        catch(Exception ex)
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
            string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))       AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";// AND OTHER_INFO LIKE '" + txtArticleCode.Text + "' 
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND MST_CODE NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))      AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";//AND OTHER_INFO LIKE '" + txtArticleCode.Text + "'   
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

        string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))      AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";// AND OTHER_INFO LIKE '" + txtArticleCode.Text + "'  
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    private void CreateWODetailTable()
    {
        try
        {
            dtWODetail = new DataTable();
            dtWODetail.Columns.Add("UniqueId", typeof(int));
            dtWODetail.Columns.Add("WO_NUMB", typeof(int));
            dtWODetail.Columns.Add("YEAR", typeof(int));
            dtWODetail.Columns.Add("ARTICLE_CODE", typeof(string));
            dtWODetail.Columns.Add("ARTICLE_DESC", typeof(string));
            dtWODetail.Columns.Add("SHADE_CODE", typeof(string));
            dtWODetail.Columns.Add("LOT_NO", typeof(string));
            dtWODetail.Columns.Add("GRADE", typeof(string));
            dtWODetail.Columns.Add("QTY", typeof(double));
            dtWODetail.Columns.Add("UOM", typeof(string));
            dtWODetail.Columns.Add("BASIC_RATE", typeof(double));
            dtWODetail.Columns.Add("FINAL_RATE", typeof(double));
            dtWODetail.Columns.Add("AMOUNT", typeof(double));
            dtWODetail.Columns.Add("SHRINKAGE", typeof(double));
            dtWODetail.Columns.Add("SPL_INSTRUCTION", typeof(string));
            dtWODetail.Columns.Add("REMARKS", typeof(string));
            dtWODetail.Columns.Add("ASS_YARN_CODE", typeof(string));
            dtWODetail.Columns.Add("ASS_YARN_DESC", typeof(string));
            dtWODetail.Columns.Add("NO_OF_UNIT", typeof(double));
        }
        catch
        {
            throw;
        }
    }

    private void BindWOTrntoGrid()
    {
        try
        {
            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            grdWOTRN.DataSource = dtWODetail;
            grdWOTRN.DataBind();

        }
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            ddlArticleCode.SelectedIndex = -1;
            ddlArticleCode.Enabled = true;
           // ddlshadeCode.SelectedIndex = -1;
            txtArticleCode.Text = string.Empty;
            txtArticleDescription.Text = string.Empty;
            txtWOQty.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtBasicRate.Text = string.Empty;
            txtFinalRate.Text = string.Empty;
            txtTRNRemarks.Text = string.Empty;
            txtNoOfUnite.Text = string.Empty;
            txtYarn_CodeParty.Text = string.Empty;
            txtParyItemDesc.Text = string.Empty;
            txtShrinkage.Text = "0";
            txtLotNo.SelectedIndex = -1;
            txtGrade.SelectedIndex = -1;
            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getWOMaxId();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlJoberParty_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetJoberPartyData(e.Text.ToUpper(), e.ItemsOffset);

            ddlJoberParty.Items.Clear();

            ddlJoberParty.DataSource = data;
            ddlJoberParty.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetJoberPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetJoberPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetJoberPartyCount(string text)
    {

        string CommandText = " SELECT   PRTY_CODE   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlJoberParty_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtJoberPartyAddress.Text = ddlJoberParty.SelectedValue.Trim();
            txtJoberPartyCode.Text = ddlJoberParty.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }


    protected void ddlPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);

            ddlPartyCode.Items.Clear();

            ddlPartyCode.DataSource = data;
            ddlPartyCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('18','47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('TRANSPORTER & LOGISTICS'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','18','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('TRANSPORTER & LOGISTICS'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT   PRTY_CODE   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','18,'48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('TRANSPORTER & LOGISTICS'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyAddress.Text = ddlPartyCode.SelectedValue.Trim();
            txtPartyCode.Text = ddlPartyCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlTransporterCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetTransporterData(e.Text.ToUpper(), e.ItemsOffset);

            ddlTransporterCode.Items.Clear();

            ddlTransporterCode.DataSource = data;
            ddlTransporterCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetTransporterCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetTransporterData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('NA') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('NA') and ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = "%" + Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetTransporterCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('NA') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtTransporterAdd.Text = ddlTransporterCode.SelectedValue;
            txtTransporterCode.Text = ddlTransporterCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlArticleCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlArticleCode.Items.Clear();

                ddlArticleCode.DataSource = data;
                ddlArticleCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;
            string whereClause = string.Empty;

            if (ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
            {
                CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC,YARN_TYPE,YARN_CAT, (YARN_CODE||'@'||YARN_DESC||'@'|| UOM )ITEM_DATA  FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE  YARN_CAT='SEWING THREAD' AND ROWNUM <= 15 ";

                if (startOffset != 0)
                {
                    whereClause += " AND YARN_code NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC,YARN_TYPE,YARN_CAT,(YARN_CODE||'@'||YARN_DESC||'@'|| UOM )ITEM_DATA  FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE YARN_CAT='SEWING THREAD' AND ROWNUM <= " + startOffset + ") ";
                }
            }
            if (ddlProductType.SelectedValue.Trim() == "ITEM")
            {
                CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE , ITEM_DESC,ITEM_TYPE,  CAT_CODE ITEM_CAT, (ITEM_CODE || '@' || ITEM_DESC || '@' || UOM) ITEM_DATA              FROM   TX_ITEM_MST      WHERE      ITEM_CODE LIKE :SearchQuery          OR ITEM_TYPE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery          ORDER BY   ITEM_CODE) asd WHERE   ITEM_TYPE LIKE '" + ddlJobWorkCat.SelectedValue + "' AND ROWNUM <= 15 ";

                if (startOffset != 0)
                {
                    whereClause += " AND ITEM_CODE NOT IN (SELECT   ITEM_CODE  FROM   (  SELECT   ITEM_CODE , ITEM_DESC,ITEM_TYPE,  CAT_CODE ITEM_CAT, (ITEM_CODE || '@' || ITEM_DESC || '@' || UOM) ITEM_DATA              FROM   TX_ITEM_MST      WHERE      ITEM_CODE LIKE :SearchQuery          OR ITEM_TYPE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery          ORDER BY   ITEM_CODE) asd WHERE   ITEM_TYPE LIKE '" + ddlJobWorkCat.SelectedValue + "' AND  ROWNUM <= " + startOffset + ") ";
                }
            }
            else if (ddlProductType.SelectedValue.Trim() == "YARN")
            {
                CommandText = " SELECT   *  FROM   (  SELECT   YM.YARN_CODE AS ARTICLE_CODE,  YA.ASS_YARN_CODE YARN_CODE, YM.FILAMENT AS TWIST_TYPE, YA.ASS_YARN_DESC YARN_DESC, YM.YARN_TYPE, YM.YARN_CAT,(   YM.YARN_CODE || '@'  || YA.ASS_YARN_CODE  ||'@'|| UOM  || '@' || YA.ASS_YARN_DESC) AS Combined   FROM   YRN_MST YM, YRN_ASSOCATED_MST YA  WHERE   YM.YARN_CODE = YA.YARN_CODE    AND (   YA.ASS_YARN_CODE LIKE :SearchQuery  OR YM.YARN_TYPE LIKE :SearchQuery  OR YA.ASS_YARN_DESC LIKE :SearchQuery) ORDER BY   YA.YARN_CODE) asd WHERE   1 = 1";
                //CommandText = " SELECT * FROM (SELECT YARN_CODE ITEM_CODE, YARN_DESC ITEM_DESC,YARN_TYPE ITEM_TYPE ,YARN_CAT ITEM_CAT , (YARN_CODE||'@'||YARN_DESC||'@'|| UOM )ITEM_DATA  FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE  ITEM_CAT<>'SEWING THREAD' AND ROWNUM <= 15 ";

                if (startOffset != 0)
                {
                    whereClause += " AND YARN_CODE NOT IN (SELECT YARN_CODE FROM (SELECT YM.YARN_CODE AS ARTICLE_CODE, YA.ASS_YARN_CODE YARN_CODE, YM.FILAMENT AS TWIST_TYPE, YA.ASS_YARN_DESC      YARN_DESC,         YM.YARN_TYPE,  YM.YARN_CAT,    (   YM.YARN_CODE || '@'  || YA.ASS_YARN_CODE  ||'@'|| UOM  || '@' || YA.ASS_YARN_DESC) AS Combined      FROM   YRN_MST YM ,YRN_ASSOCATED_MST YA      WHERE   YM.YARN_CODE=YA.YARN_CODE   AND (   YA.ASS_YARN_CODE LIKE :SearchQuery   OR YM.YARN_TYPE LIKE :SearchQuery OR YA.ASS_YARN_DESC LIKE :SearchQuery)  AND ROWNUM <= " + startOffset + "      ORDER BY   YA.YARN_CODE) asd   )";
                    //whereClause += " AND ITEM_CODE NOT IN (SELECT ITEM_CODE FROM (SELECT YARN_CODE ITEM_CODE, YARN_DESC ITEM_DESC,YARN_TYPE ITEM_TYPE,YARN_CAT ITEM_CAT,(YARN_CODE||'@'||YARN_DESC||'@'|| UOM )ITEM_DATA  FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ITEM_CAT<>'SEWING THREAD' AND ROWNUM <= " + startOffset + ") ";
                }
            }

            string SortExpression = " ORDER BY YARN_CODE";
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
        string CommandText = string.Empty;
        string whereClause = string.Empty;

        if (ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
        {
            CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC,YARN_TYPE,YARN_CAT,(YARN_CODE||'@'||YARN_DESC||'@'|| UOM )ITEM_DATA   FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd WHERE YARN_CAT='SEWING THREAD'";
        }
        if (ddlProductType.SelectedValue.Trim() == "ITEM")
        {
            CommandText = "  SELECT   ITEM_CODE     FROM   TX_ITEM_MST      WHERE      (ITEM_CODE LIKE :SearchQuery          OR ITEM_TYPE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery)          AND  ITEM_TYPE LIKE '" + ddlJobWorkCat.SelectedValue + "'  ";
                       
        }
        else if (ddlProductType.SelectedValue.Trim() == "YARN")
        {
            CommandText = " SELECT   *  FROM   (  SELECT   YM.YARN_CODE AS ARTICLE_CODE,  YA.ASS_YARN_CODE YARN_CODE, YM.FILAMENT AS TWIST_TYPE, YA.ASS_YARN_DESC YARN_DESC, YM.YARN_TYPE, YM.YARN_CAT,(   YM.YARN_CODE || '@'  || YA.ASS_YARN_CODE  ||'@'|| UOM  || '@' || YA.ASS_YARN_DESC) AS Combined   FROM   YRN_MST YM, YRN_ASSOCATED_MST YA  WHERE   YM.YARN_CODE = YA.YARN_CODE  AND NOT (FILAMENT IN ('NON TWIST'))  AND (   YA.ASS_YARN_CODE LIKE :SearchQuery  OR YM.YARN_TYPE LIKE :SearchQuery  OR YA.ASS_YARN_DESC LIKE :SearchQuery) ORDER BY   YA.YARN_CODE) asd";
        }
        string SortExpression = " ORDER BY YARN_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void ddlArticleCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string ArticleNo = ddlArticleCode.SelectedText.ToString();
            string[] arrString = ddlArticleCode.SelectedValue.Split('@');
            txtArticleCode.Text = ArticleNo;
            txtYarn_CodeParty.Text = arrString[1].ToString();
            BindDetailByCaseSelection();
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article No Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindDetailByCaseSelection()
    {
        try
        {
            string ArticleCode = txtArticleCode.Text.Trim();
            DataTable dt = SaitexDL.Interface.Method.OD_WO_MST.GetArticleDetailByYarnCodeWorkOrder(ArticleCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtUnit.Text = dt.Rows[0]["UOM"].ToString();
                txtYarn_CodeParty.Text = dt.Rows[0]["ASS_YARN_CODE"].ToString();
                txtArticleDescription.Text = dt.Rows[0]["YARN_DESC"].ToString();
                txtParyItemDesc.Text = dt.Rows[0]["ASS_YARN_DESC"].ToString();

                //txtWOQty.Text = dt.Rows[0]["CURRENTSTOCK"].ToString().Trim();
                //txtWOQty.ReadOnly = false;
                //ViewState["CURRENTSTOCK"] = dt.Rows[0]["CURRENTSTOCK"].ToString().Trim();
            }
            else
            {
                txtWOQty.Text = "0";
                txtWOQty.ReadOnly = false;
                txtUnit.Text = string.Empty;
                txtBasicRate.Text = string.Empty;
            }

        }
        catch (Exception ex )
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting data.\r\nSee error log for detail."));
        }
    }
    protected void ddlshadeCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (SearchItemCodeInGrid(txtArticleCode.Text))
            {
                ddlshadeCode.SelectedIndex = -1;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('This Article already included with selected shade');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchItemCodeInGrid(string ArticleCode)
    {
        bool Result = false;
        try
        {
            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());

            string Shadecode = ddlshadeCode.SelectedValue.Trim();

            if (grdWOTRN.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdWOTRN.Rows)
                {
                    Label txtArticleCodes = (Label)grdRow.FindControl("txtArticleCode");
                    Label txtSHADE_CODE = (Label)grdRow.FindControl("txtSHADE_CODE");
                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUniqueId = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (txtArticleCodes.Text.Trim() == ArticleCode && txtSHADE_CODE.Text.Trim() == Shadecode && UniqueId != iUniqueId)
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

    protected void btnDiscountTaxes_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtArticleCode.Text != "")
            {
                string URL = "GetWODisTex.aspx";
                URL = URL + "?FinalAmount=" + txtBasicRate.Text.Trim();
                URL = URL + "&TextBoxId=" + txtFinalRate.ClientID;
                URL = URL + "&ARTICLE_CODE=" + txtArticleCode.Text;
                URL = URL + "&SHADE_CODE=" + ddlshadeCode.SelectedValue.Trim();
                URL = URL + "&WO_NUMB=" + txtWONumber.Text.Trim();
                URL = URL + "&WO_TYPE=" + ddlWOType.SelectedValue.Trim();

                txtFinalRate.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }
            else
            {
                CommonFuction.ShowMessage("Please select Item to add rate component");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Rate Component.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnBOM_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtArticleCode.Text != string.Empty && ddlshadeCode.SelectedValue.Trim() != string.Empty)
            {
                string URL = "AddWOBOM.aspx";

                // URL = URL + "?TextBoxCost=" + txtTRNYarnSpiningCost.ClientID;
                URL = URL + "?SHADE_CODE=" + ddlshadeCode.SelectedValue.Trim();

                URL = URL + "&ARTICAL_CODE=" + txtArticleCode.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select asrtical code");
            }
        }
        catch
        {
        }

    }

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dtWODicRate = (DataTable)Session["dtWODicRate"];
            DataView dvWODicRate = new DataView(dtWODicRate);
            dvWODicRate.RowFilter = "ARTICLE_CODE='" + txtArticleCode.Text + "' and SHADE_CODE='" + ddlshadeCode.SelectedValue.Trim() + "' ";


            DataTable dtWOBOM = (DataTable)Session["dtWOBOM"];

            DataView dvWOBOM = new DataView(dtWOBOM);
            dvWOBOM.RowFilter = "ARTICLE_CODE='" + txtArticleCode.Text + "' and SHADE_CODE='" + ddlshadeCode.SelectedValue.Trim() + "' ";
            if (txtNoOfUnite.Text != string.Empty)
            {
                if (dvWODicRate.Count > 0)
            {
                if (dvWOBOM.Count > 0)
                {

                    double BaseRate = 0f;
                    double FinalRate = 0f;
                    double.TryParse(CommonFuction.funFixQuotes(txtBasicRate.Text.Trim()), out BaseRate);
                    double.TryParse(CommonFuction.funFixQuotes(txtFinalRate.Text.Trim()), out FinalRate);
                    if (FinalRate != 0f && BaseRate != 0f)
                    {
                        if (ViewState["dtWODetail"] != null)
                            dtWODetail = (DataTable)ViewState["dtWODetail"];

                        if (dtWODetail == null)
                            CreateWODetailTable();

                        if (txtArticleCode.Text != "" && txtWOQty.Text != "" && txtFinalRate.Text != "")
                        {
                            bool bb = SearchItemCodeInGrid(txtArticleCode.Text);

                            if (!bb)
                            {
                                int UniqueId = 0;
                                if (ViewState["UniqueId"] != null)
                                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());

                                double Qty = 0;
                                double.TryParse(txtWOQty.Text.Trim(), out Qty);
                                if (Qty > 0)
                                {
                                    if (UniqueId > 0)
                                    {
                                        DataView dv = new DataView(dtWODetail);
                                        dv.RowFilter = "UniqueId=" + UniqueId;
                                        if (dv.Count > 0)
                                        {
                                            double dd = 0;

                                            dv[0]["WO_NUMB"] = txtWONumber.Text;
                                            dv[0]["ARTICLE_CODE"] = txtArticleCode.Text;
                                            dv[0]["ARTICLE_DESC"] = txtArticleDescription.Text.Trim();
                                            dv[0]["SHADE_CODE"] = ddlshadeCode.SelectedValue.Trim();
                                            dv[0]["LOT_NO"] = txtLotNo.SelectedValue.Trim();
                                            dv[0]["GRADE"] = txtGrade.SelectedValue.Trim();
                                            dv[0]["QTY"] = Qty;
                                            dv[0]["UOM"] = txtUnit.Text.Trim();
                                            dv[0]["NO_OF_UNIT"] = txtNoOfUnite.Text.Trim();
                                            dv[0]["ASS_YARN_CODE"] = txtYarn_CodeParty.Text.Trim();
                                            dv[0]["ASS_YARN_DESC"] = txtParyItemDesc.Text.Trim();


                                            dd = 0;
                                            double.TryParse(txtBasicRate.Text, out dd);
                                            dv[0]["BASIC_RATE"] = dd;

                                            dd = 0;
                                            double.TryParse(txtFinalRate.Text, out dd);
                                            dv[0]["FINAL_RATE"] = dd;

                                            dd = Qty * dd;
                                            dv[0]["AMOUNT"] = dd;

                                            dd = 0;
                                            double.TryParse(txtShrinkage.Text, out dd);
                                            dv[0]["SHRINKAGE"] = dd;
                                            dv[0]["SPL_INSTRUCTION"] = string.Empty;
                                            dv[0]["REMARKS"] = txtTRNRemarks.Text;
                                            dtWODetail.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        double dd = 0;

                                        DataRow dr = dtWODetail.NewRow();
                                        dr["UniqueId"] = dtWODetail.Rows.Count + 1;
                                        dr["WO_NUMB"] = txtWONumber.Text;
                                        dr["ARTICLE_CODE"] = txtArticleCode.Text;
                                        dr["ARTICLE_DESC"] = txtArticleDescription.Text.Trim();
                                        dr["SHADE_CODE"] = ddlshadeCode.SelectedValue.Trim();
                                        dr["LOT_NO"] = txtLotNo.SelectedValue.Trim();
                                        dr["GRADE"] = txtGrade.SelectedValue.Trim();
                                        dr["QTY"] = Qty;
                                        dr["UOM"] = txtUnit.Text.Trim();
                                        dr["NO_OF_UNIT"] = txtNoOfUnite.Text.Trim();
                                        dr["ASS_YARN_CODE"] = txtYarn_CodeParty.Text.Trim();
                                        dr["ASS_YARN_DESC"] = txtParyItemDesc.Text.Trim();

                                        dd = 0;
                                        double.TryParse(txtBasicRate.Text, out dd);
                                        dr["BASIC_RATE"] = dd;

                                        dd = 0;
                                        double.TryParse(txtFinalRate.Text, out dd);
                                        dr["FINAL_RATE"] = dd;

                                        dd = Qty * dd;
                                        dr["AMOUNT"] = dd;

                                        dd = 0;
                                        double.TryParse(txtShrinkage.Text, out dd);
                                        dr["SHRINKAGE"] = dd;
                                        dr["SPL_INSTRUCTION"] = string.Empty;
                                        dr["REMARKS"] = txtTRNRemarks.Text;
                                        dtWODetail.Rows.Add(dr);
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
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid Article code');", true);
                            }
                        }
                        ViewState["dtWODetail"] = dtWODetail;
                        BindWOTrntoGrid();



                    }


                    else
                    {
                        CommonFuction.ShowMessage("You have entered base rate/ final rate Zero. Please enter proper base rate or Dis/ Taxes.");
                    }
                }
                else
                {
                    CommonFuction.ShowMessage("You have not generated BOM. Please generate BOM.");
                }
            }
            else 
            {
                CommonFuction.ShowMessage("You have not fill TAX. Please fill TAX Details.");
            }
        }
        else 
            {
                CommonFuction.ShowMessage("You have not Unit TAX. Please fill Unit Details.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in WO Detail Row.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private double CalculateAmount()
    {
        try
        {
            double OrderQty = 0;
            double BaseRate = 0f;
            double FinalRate = 0f;
            double Amount = 0f;
            double.TryParse(CommonFuction.funFixQuotes(txtWOQty.Text.Trim()), out OrderQty);
            double.TryParse(CommonFuction.funFixQuotes(txtBasicRate.Text.Trim()), out BaseRate);
            double.TryParse(CommonFuction.funFixQuotes(txtFinalRate.Text.Trim()), out FinalRate);
            if (FinalRate == 0f)
                FinalRate = BaseRate;

            Amount = OrderQty * FinalRate;
            return Amount;
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancelDetail_Click(object sender, EventArgs e)
    {
        RefreshDetailRow();
    }

    protected void grdWOTRN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "WOTrnDelete")
            {
                deleteWOTrnRow(UniqueId);
                BindWOTrntoGrid();
            }
            if (e.CommandName == "WOTrnEdit")
            {
                FillDetailByGrid(UniqueId);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row updfation / deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void deleteWOTrnRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            if (dtWODetail.Rows.Count == 1)
            {
                dtWODetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtWODetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtWODetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtWODetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
            ViewState["dtWODetail"] = dtWODetail;

        }
        catch
        {
            throw;
        }
    }

    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            DataView dv = new DataView(dtWODetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                ddlArticleCode.Enabled = false;
                txtArticleCode.Text = dv[0]["ARTICLE_CODE"].ToString();
                txtArticleDescription.Text = dv[0]["ARTICLE_DESC"].ToString();
                txtYarn_CodeParty.Text = dv[0]["ASS_YARN_CODE"].ToString();
                txtParyItemDesc.Text = dv[0]["ASS_YARN_DESC"].ToString();
                ddlshadeCode.SelectedIndex = ddlshadeCode.Items.IndexOf(ddlshadeCode.Items.FindByValue(dv[0]["SHADE_CODE"].ToString()));
                txtWOQty.Text = double.Parse(dv[0]["QTY"].ToString()).ToString("N4");
                txtUnit.Text = dv[0]["UOM"].ToString();
                txtNoOfUnite.Text = double.Parse(dv[0]["NO_OF_UNIT"].ToString()).ToString("N4");
                txtBasicRate.Text = double.Parse(dv[0]["BASIC_RATE"].ToString()).ToString("N4");
                txtFinalRate.Text = double.Parse(dv[0]["FINAL_RATE"].ToString()).ToString("N4");
                txtShrinkage.Text = double.Parse(dv[0]["SHRINKAGE"].ToString()).ToString();
                txtRemarks.Text = dv[0]["REMARKS"].ToString();

                string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GREY_LOT_NO'))         AND OTHER_INFO LIKE '" + txtArticleCode.Text + "'     AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
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
            ViewState["dtWODetail"] = dtWODetail;

        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateForSaving(out msg))
            {
                SaveWOEntry();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool ValidateForSaving(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool returnVal = false;
            int iCount = 0;
            int iCountAll = 0;

            iCountAll += 1;
            if (txtPartyCode.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += "Please select Party Before saving.";
            }
            iCountAll += 1;
            if (txtJoberPartyCode.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += "Please select Jober Party Before saving.";
            }

            iCountAll += 1;
            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            if (dtWODetail != null && dtWODetail.Rows.Count > 0)
            {
                iCount += 1;
            }
            else
            {
                msg += "Please select Party Before saving.";
            }

            if (iCount == iCountAll)
            {
                returnVal = true;
            }
            else
            {
                returnVal = false;
            }

            return returnVal;
        }
        catch
        {
            throw;
        }
    }

    private void SaveWOEntry()
    {
        try
        {

            oOD_WO_MST = new SaitexDM.Common.DataModel.OD_WO_MST();

            oOD_WO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_WO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_WO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oOD_WO_MST.PRODUCT_TYPE = ddlProductType.SelectedValue.Trim();
            oOD_WO_MST.WO_TYPE = ddlWOType.SelectedValue.Trim();
            oOD_WO_MST.WO_NUMB = int.Parse(txtWONumber.Text.Trim());
            oOD_WO_MST.WO_DATE = DateTime.Parse(txtWODate.Text.Trim());
            oOD_WO_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.Text.Trim());
            oOD_WO_MST.JOB_WORK_CAT = CommonFuction.funFixQuotes(ddlJobWorkCat.SelectedValue.Trim());
            oOD_WO_MST.SPL_INSTRUCTION = CommonFuction.funFixQuotes(txtInstructions.Text.Trim());
            oOD_WO_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oOD_WO_MST.PAYMENT_TERMS = CommonFuction.funFixQuotes(txtPayTerm.Text.Trim());
            oOD_WO_MST.DELIVERY_LOCATION = CommonFuction.funFixQuotes(ddlDelAdd.SelectedValue.Trim());
            oOD_WO_MST.JOBER_PARTY = CommonFuction.funFixQuotes(txtJoberPartyCode.Text.Trim());
            if (txtTransporterCode.Text != "")
                oOD_WO_MST.TRSP_CODE = "NA";
            else
                oOD_WO_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.Text.Trim());
            //oOD_WO_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.Text.Trim());
            oOD_WO_MST.TUSER = oUserLoginDetail.UserCode;

            DataTable dtWOBOM = (DataTable)Session["dtWOBOM"];
            DataTable dtWODicRate = (DataTable)Session["dtWODicRate"];
            int WO_NUMB = 0;

            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            if (dtWODetail != null && dtWODetail.Rows.Count > 0)
            {
                bool bResult = SaitexBL.Interface.Method.OD_WO_MST.Insert(oOD_WO_MST, dtWODetail, dtWOBOM, dtWODicRate, out WO_NUMB);

                if (bResult)
                {
                    InitialisePage();
                    string msg = "WO Number " + WO_NUMB + " Successfully Saved.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
                }
                else
                {
                    CommonFuction.ShowMessage("WO saving failed.");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please enter transaction details first.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ActivateUpdateMode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in loading update mode."));
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            imgbtnSave.Visible = false;
            imgbtnUpdate.Visible = true;

            ddlWONumber.Enabled = true;
            ddlWONumber.Visible = true;
            ddlWONumber.SelectedIndex = -1;
            txtWONumber.Visible = false;
            lblMode.Text = "update";
        }
        catch
        {
            throw;
        }
    }

    protected void ddlWONumber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetWOs(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetWOCount(e.Text);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading po number for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetWOs(string text, int startOffset, int numberOfItems)
    {
        try
        {

            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT WO_NUMB, trunc(WO_DATE) as WO_DATE, PRTY_NAME FROM V_OD_WO_MST WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE and PRODUCT_TYPE='" + ddlProductType.SelectedValue.Trim() + "' AND WO_TYPE = :IND_TYPE ) asf WHERE WO_NUMB LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY WO_NUMB DESC, WO_DATE DESC) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND WO_NUMB NOT IN(SELECT WO_NUMB FROM (SELECT * FROM (SELECT WO_NUMB, trunc(WO_DATE) as WO_DATE,PRTY_NAME FROM V_OD_WO_MST WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE and PRODUCT_TYPE='" + ddlProductType.SelectedValue.Trim() + "' AND WO_TYPE = :IND_TYPE ) asf   WHERE WO_NUMB LIKE :searchQuery OR prty_name LIKE :searchQuery  ORDER BY WO_NUMB DESC, WO_DATE DESC) asd WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY WO_NUMB DESC, WO_DATE DESC";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlWOType.SelectedValue.Trim(), CommandText, whereClause, SortExpression, "", "%" + text + "%");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetWOCount(string text)
    {


        string CommandText = " SELECT * FROM (SELECT * FROM (SELECT WO_NUMB, WO_DATE, PRTY_NAME FROM V_OD_WO_MST WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE and PRODUCT_TYPE='" + ddlProductType.SelectedValue.Trim() + "' AND WO_TYPE = :IND_TYPE) asf WHERE WO_NUMB LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY WO_NUMB DESC, WO_DATE DESC) asd  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlWONumber_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            ddlProductType.Enabled = true;
            ddlWOType.Enabled = true;

            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            string WONumber = ddlWONumber.SelectedValue.Trim();
            if (dtWODetail == null || dtWODetail.Rows.Count == 0)
                CreateWODetailTable();
            dtWODetail.Rows.Clear();

            int iRecordFound = GetdataByWONumber(CommonFuction.funFixQuotes(WONumber));
            BindWOTrntoGrid();
            if (iRecordFound > 0)
            {
            }
            else
            {
                InitialisePage();
                lblMode.Text = "You are in Update Mode";
                txtWONumber.Text = "";

                ActivateUpdateMode();

                string msg = "Dear " + oUserLoginDetail.Username + " !! Work Order already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private int GetdataByWONumber(string WoNumber)
    {
        int iRecordFound = 0;
        try
        {
            SaitexDM.Common.DataModel.OD_WO_MST oOD_WO_MST = new SaitexDM.Common.DataModel.OD_WO_MST();
            oOD_WO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_WO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_WO_MST.PRODUCT_TYPE = ddlProductType.SelectedValue.Trim();
            oOD_WO_MST.WO_TYPE = ddlWOType.SelectedValue.Trim();
            oOD_WO_MST.WO_NUMB = int.Parse(WoNumber);
            oOD_WO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable dt = SaitexBL.Interface.Method.OD_WO_MST.GetPendingWorkOrder(oOD_WO_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                ddlJobWorkCat.SelectedIndex = ddlJobWorkCat.Items.IndexOf(ddlJobWorkCat.Items.FindByValue(dt.Rows[0]["JOB_WORK_CAT"].ToString()));
                txtWONumber.Text = dt.Rows[0]["WO_NUMB"].ToString();
                txtWODate.Text = DateTime.Parse(dt.Rows[0]["WO_DATE"].ToString().Trim()).ToShortDateString();
                ddlDelAdd.SelectedIndex = ddlDelAdd.Items.IndexOf(ddlDelAdd.Items.FindByValue(dt.Rows[0]["DELIVERY_LOCATION"].ToString().Trim()));

                txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtJoberPartyCode.Text = dt.Rows[0]["JOBER_PARTY"].ToString().Trim();
                txtJoberPartyAddress.Text = dt.Rows[0]["JOBER_NAME"].ToString().Trim();
                txtTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                txtTransporterAdd.Text = dt.Rows[0]["TRSP_NAME"].ToString().Trim();

                txtPayTerm.Text = dt.Rows[0]["PAYMENT_TERMS"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString().Trim();
                txtInstructions.Text = dt.Rows[0]["SPL_INSTRUCTION"].ToString().Trim();

                // CODE FOR TRANSACTION DATA
                DataTable dtTemp = SaitexBL.Interface.Method.OD_WO_MST.GetWOTrnDataByWoNumb(oOD_WO_MST);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);

                    DataTable dtBom = SaitexBL.Interface.Method.OD_WO_MST.GetWOSubTrnDataByWoNumb(oOD_WO_MST);
                    MapDataTableBOM(dtBom);
                    DataTable dtDisTax = SaitexBL.Interface.Method.OD_WO_MST.GetWODisTaxDataByWoNumb(oOD_WO_MST);
                    MapDataTableDisTax(dtDisTax);

                    ddlProductType.Enabled = false;
                    ddlWOType.Enabled = false;
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! Purchase Order already approved. Modification not allowed.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();

                    txtWONumber.Text = "";
                    ddlWONumber.Focus();

                    lblMode.Text = "You are In Update Mode";

                    ActivateUpdateMode();
                    RefreshDetailRow();
                }
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTableDisTax(DataTable dtTemp)
    {
        if (Session["dtWODicRate"] != null)
            Session["dtWODicRate"] = null;

        DataTable dtWODicRate = CreateDisTaxTable();

        dtWODicRate.Rows.Clear();

        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            foreach (DataRow drTemp in dtTemp.Rows)
            {
                DataRow dr = dtWODicRate.NewRow();
                dr["UniqueId"] = dtWODicRate.Rows.Count + 1;
                dr["ARTICLE_CODE"] = drTemp["ARTICLE_CODE"].ToString().Trim();
                dr["SHADE_CODE"] = drTemp["SHADE_CODE"].ToString().Trim();
                dr["COMPO_CODE"] = drTemp["COMPO_CODE"].ToString().Trim();
                dr["Rate"] = drTemp["RATE"].ToString().Trim();
                dr["COMPO_SL"] = drTemp["COMPO_SL"].ToString().Trim();
                dr["COMPO_TYPE"] = drTemp["COMPO_TYPE"].ToString().Trim();
                dr["Amount"] = 0;// drTemp["AMOUNT"].ToString().Trim();
                dr["BASE_COMPO_CODE"] = drTemp["BASE_COMPO_CODE"].ToString().Trim();

                dtWODicRate.Rows.Add(dr);
            }
            dtTemp = null;
        }
        Session["dtWODicRate"] = dtWODicRate;
    }

    private DataTable CreateDisTaxTable()
    {
        try
        {
            DataTable dtRate = new DataTable();
            dtRate.Columns.Add("UniqueId", typeof(int));
            dtRate.Columns.Add("ARTICLE_CODE", typeof(string));
            dtRate.Columns.Add("SHADE_CODE", typeof(string));
            dtRate.Columns.Add("COMPO_CODE", typeof(string));
            dtRate.Columns.Add("Rate", typeof(double));
            dtRate.Columns.Add("COMPO_SL", typeof(int));
            dtRate.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate.Columns.Add("Amount", typeof(double));

            dtRate.Columns.Add("BASE_COMPO_CODE", typeof(string));

            return dtRate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MapDataTableBOM(DataTable dtTemp)
    {
        if (Session["dtWOBOM"] != null)
            Session["dtWOBOM"] = null;

        DataTable dtWOBOM = CreateBOMTable();

        dtWOBOM.Rows.Clear();

        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            foreach (DataRow drTemp in dtTemp.Rows)
            {
                DataRow dr = dtWOBOM.NewRow();
                dr["UNIQUE_ID"] = dtWOBOM.Rows.Count + 1;
                dr["ARTICLE_CODE"] = drTemp["ARTICLE_CODE"].ToString().Trim();
                dr["SHADE_CODE"] = drTemp["SHADE_CODE"].ToString().Trim();
                dr["BASE_ARTICLE_TYPE"] = drTemp["BASE_ARTICLE_TYPE"].ToString().Trim(); 
                dr["BASE_ARTICLE_CODE"] = drTemp["BASE_ARTICLE_CODE"].ToString().Trim();
                dr["BASE_ARTICLE_DESC"] = drTemp["BASE_ARTICLE_DESC"].ToString().Trim();
                dr["BASE_SHADE_CODE"] = drTemp["BASE_SHADE_CODE"].ToString().Trim();
                dr["UOM"] = drTemp["UOM"].ToString().Trim();
                dr["QTY"] = drTemp["QTY"].ToString().Trim();
                dr["SHRINKAGE"] = drTemp["SHRINKAGE"].ToString().Trim();
                dr["SPL_INSTRUCTION"] = drTemp["SPL_INSTRUCTION"].ToString().Trim();
                dr["REMARKS"] = drTemp["REMARKS"].ToString().Trim();

                dtWOBOM.Rows.Add(dr);
            }
            dtTemp = null;
        }
        Session["dtWOBOM"] = dtWOBOM;

    }

    private DataTable CreateBOMTable()
    {
        try
        {
            DataTable dtTRN_BOM = new DataTable();
            dtTRN_BOM.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_BOM.Columns.Add("ARTICLE_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("SHADE_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_ARTICLE_TYPE", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_ARTICLE_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_ARTICLE_DESC", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_SHADE_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("UOM", typeof(string));
            dtTRN_BOM.Columns.Add("QTY", typeof(string));
            dtTRN_BOM.Columns.Add("SHRINKAGE", typeof(string));
            dtTRN_BOM.Columns.Add("SPL_INSTRUCTION", typeof(string));
            dtTRN_BOM.Columns.Add("REMARKS", typeof(string));
            return dtTRN_BOM;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            if (dtWODetail == null || dtWODetail.Rows.Count == 0)
                CreateWODetailTable();
            dtWODetail.Rows.Clear();
            double FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtWODetail.NewRow();
                    double Amount = 0f;
                    Amount = (double.Parse(drTemp["FINAL_RATE"].ToString().Trim())) * (double.Parse(drTemp["QTY"].ToString().Trim()));
                    FinalTotal = FinalTotal + Amount;
                    dr["UniqueId"] = dtWODetail.Rows.Count + 1;
                    dr["WO_NUMB"] = drTemp["WO_NUMB"].ToString().Trim();
                    dr["ARTICLE_CODE"] = drTemp["ARTICLE_CODE"].ToString().Trim();
                    dr["ARTICLE_DESC"] = drTemp["ARTICLE_DESC"].ToString().Trim();
                    dr["LOT_NO"] = drTemp["LOT_NO"].ToString().Trim();
                    dr["GRADE"] = drTemp["GRADE"].ToString().Trim();
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"].ToString().Trim();
                    dr["QTY"] = drTemp["QTY"].ToString().Trim();
                    dr["UOM"] = drTemp["UOM"].ToString().Trim();
                    dr["BASIC_RATE"] = drTemp["BASIC_RATE"].ToString().Trim();
                    dr["FINAL_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
                    dr["AMOUNT"] = Amount;
                    dr["SHRINKAGE"] = drTemp["SHRINKAGE"].ToString().Trim();
                    dr["SPL_INSTRUCTION"] = drTemp["SPL_INSTRUCTION"].ToString().Trim();
                    dr["NO_OF_UNIT"] = drTemp["NO_OF_UNIT"].ToString().Trim();
                    dr["ASS_YARN_CODE"] = drTemp["ASS_YARN_CODE"].ToString().Trim();
                    dr["ASS_YARN_DESC"] = drTemp["ASS_YARN_DESC"].ToString().Trim();
                    dr["REMARKS"] = drTemp["REMARKS"].ToString().Trim();

                    dtWODetail.Rows.Add(dr);
                }
                dtTemp = null;
            }
            ViewState["dtWODetail"] = dtWODetail;

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
            if (ValidateForSaving(out msg))
            {
                UpdateWOEntry();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void UpdateWOEntry()
    {
        try
        {

            oOD_WO_MST = new SaitexDM.Common.DataModel.OD_WO_MST();

            oOD_WO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_WO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_WO_MST.PRODUCT_TYPE = ddlProductType.SelectedValue.Trim();
            oOD_WO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oOD_WO_MST.WO_TYPE = ddlWOType.SelectedValue.Trim();
            oOD_WO_MST.WO_NUMB = int.Parse(txtWONumber.Text.Trim());
            oOD_WO_MST.WO_DATE = DateTime.Parse(txtWODate.Text.Trim());
            oOD_WO_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.Text.Trim());
            oOD_WO_MST.JOBER_PARTY = CommonFuction.funFixQuotes( txtJoberPartyCode.Text.Trim());
            oOD_WO_MST.JOB_WORK_CAT = CommonFuction.funFixQuotes(ddlJobWorkCat.SelectedValue.Trim());
            oOD_WO_MST.SPL_INSTRUCTION = CommonFuction.funFixQuotes(txtInstructions.Text);
            oOD_WO_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oOD_WO_MST.PAYMENT_TERMS = CommonFuction.funFixQuotes(txtPayTerm.Text.Trim());
            oOD_WO_MST.DELIVERY_LOCATION = CommonFuction.funFixQuotes(ddlDelAdd.SelectedValue.Trim());

            //if (txtTransporterCode.Text != "")
            //    oOD_WO_MST.TRSP_CODE = "NA";
            //else
            //    oOD_WO_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.Text.Trim());
            oOD_WO_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.Text.Trim());
            oOD_WO_MST.TUSER = oUserLoginDetail.UserCode;

            DataTable dtWOBOM = (DataTable)Session["dtWOBOM"];
            DataTable dtWODicRate = (DataTable)Session["dtWODicRate"];
            int WO_NUMB = int.Parse(txtWONumber.Text);

            if (ViewState["dtWODetail"] != null)
                dtWODetail = (DataTable)ViewState["dtWODetail"];

            if (dtWODetail != null && dtWODetail.Rows.Count > 0)
            {
                bool bResult = SaitexBL.Interface.Method.OD_WO_MST.Update(oOD_WO_MST, dtWODetail, dtWOBOM, dtWODicRate);

                if (bResult)
                {
                    InitialisePage();
                    string msg = "WO Number " + WO_NUMB + " Successfully Updated.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
                }
                else
                {
                    CommonFuction.ShowMessage("Wo Updation failed.");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please enter transaction details first.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string PRTYPE = "";
        PRTYPE = ddlProductType.SelectedValue;
        string URL = "Type=" + ddlProductType.SelectedValue.ToString();
        Response.Redirect("../../../Module/WorkOrder/Pages/Work_order_Entry_report.aspx?" + URL);
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
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

    protected void grdWOTRN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtArticleCodes = (Label)e.Row.FindControl("txtArticleCode");
                Label txtSHADE_CODE = (Label)e.Row.FindControl("txtSHADE_CODE");
                if (Session["dtWOBOM"] != null)
                {
                    DataTable dtWOBOM = (DataTable)Session["dtWOBOM"];
                    DataView dvWOBOM = new DataView(dtWOBOM);
                    dvWOBOM.RowFilter = "ARTICLE_CODE='" + txtArticleCodes.Text + "' and SHADE_CODE='" + txtSHADE_CODE.Text + "'";
                    if (dvWOBOM.Count > 0)
                    {
                        GridView grdBom = (GridView)e.Row.FindControl("grdIssueBOM");
                        grdBom.DataSource = dvWOBOM;
                        grdBom.DataBind();
                    }
                }
                if (Session["dtWODicRate"] != null)
                {
                    DataTable dtWODicRate = (DataTable)Session["dtWODicRate"];
                    DataView dvWODicRate = new DataView(dtWODicRate);
                    dvWODicRate.RowFilter = "ARTICLE_CODE='" + txtArticleCodes.Text + "' and SHADE_CODE='" + txtSHADE_CODE.Text + "'";
                    if (dvWODicRate.Count > 0)
                    {
                        GridView grdDisTaxes = (GridView)e.Row.FindControl("grdDisTaxes");
                        grdDisTaxes.DataSource = dvWODicRate;
                        grdDisTaxes.DataBind();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in data Binding."));

        }
    }
    protected void txtBasicRate_TextChanged(object sender, EventArgs e)
    {
        txtFinalRate.Text = txtBasicRate.Text;
    }
}
