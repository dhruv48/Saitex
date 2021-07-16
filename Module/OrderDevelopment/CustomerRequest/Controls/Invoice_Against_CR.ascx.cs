using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using Obout.ComboBox;

public partial class Module_OrderDevelopment_CustomerRequest_Controls_Invoice_Against_CR : System.Web.UI.UserControl
{
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private DataTable dtDicRate = null;
    string TRN_TYPE = string.Empty;
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

    private void InitialisePage()
    {
        try
        {
            BindGridFromDataTable();
            BlankRecords();
            ActivateSaveMode();
            BindNewInvoiceNumber();
            BindShift();

            CreateDataTable();
            txtInvoiceDate.Text = System.DateTime.Now.ToShortDateString();
            txtDateOfRemoval.Text = System.DateTime.Now.ToShortDateString();
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindNewInvoiceNumber()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_INVOICE_MST oTX_INVOICE_MST = new SaitexDM.Common.DataModel.TX_INVOICE_MST();
            oTX_INVOICE_MST.INVOICE_TYPE = ddlInvoiceType.SelectedValue;
            oTX_INVOICE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_INVOICE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_INVOICE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtInvoiceNumber.Text = SaitexBL.Interface.Method.TX_INVOICE_MST.GetNewInvoiceNumber(oTX_INVOICE_MST).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BlankRecords()
    {
        try
        {

            txtInvoiceNumber.Text = string.Empty;
            txtInvoiceNumber.ReadOnly = true;
            ddlInvoiceNumber.SelectedIndex = -1;
            ddlInvoiceType.SelectedIndex = -1;
            txtInvoiceDate.Text = string.Empty;
            txtDateOfRemoval.Text = string.Empty;
            txtBuyerorder.Text = string.Empty;
            //txtRemarks.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            ddlIssueShift.SelectedIndex = -1;
            txtLRNumber.Text = string.Empty;

            cmbParty.SelectedIndex = -1;
            txtConsigneeCode.SelectedIndex = -1;
            txtTransporterCode.SelectedIndex = -1;

            lblTransporterCode.Text = string.Empty;
            lblPartyCode.Text = string.Empty;
            lblConsigneeCode.Text = string.Empty;

            txtPartyAddress.Text = string.Empty;
            txtConsigneeAddress.Text = string.Empty;
            txtTransporterAddress.Text = string.Empty;

            Session["dtDetailTBL"] = null;
            Session["dtItemReceipt"] = null;
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ActivateSaveMode()
    {
        try
        {
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "Save";
            txtInvoiceNumber.Text = string.Empty;
            ddlInvoiceNumber.SelectedIndex = -1;
            ddlInvoiceNumber.Items.Clear();
            txtInvoiceNumber.Visible = true;
            ddlInvoiceNumber.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {

            tdSave.Visible = false;
            tdUpdate.Visible = true;
            lblMode.Text = "Update";
            txtInvoiceNumber.Text = string.Empty;
            ddlInvoiceNumber.Items.Clear();
            txtInvoiceNumber.Visible = false;
            ddlInvoiceNumber.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            ddlYarnDetails.SelectedIndex = -1;
            lblYarnCode.Text = string.Empty;
            txtYarnDesc.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtShade.Text = string.Empty;
            txtDCNo.Text = string.Empty;
            txtNoOfUnit.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            ViewState["UNIQUEID"] = null;
            ddlYarnDetails.Enabled = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("BRANCH_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YEAR", typeof(int));
            dtDetailTBL.Columns.Add("INVOICE_NUMB", typeof(Int64));
            dtDetailTBL.Columns.Add("INVOICE_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("DOC_NO", typeof(string));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("QUANTITY", typeof(double));
            dtDetailTBL.Columns.Add("RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("TUSER", typeof(string));
            dtDetailTBL.Columns.Add("TDATE", typeof(DateTime));
            dtDetailTBL.Columns.Add("STATUS", typeof(string));
            //dtDetailTBL.Columns.Add("CHALLAN_NO", typeof(Int64));
            //dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            return dtDetailTBL;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindGridFromDataTable()
    {
        try
        {
            if (Session["dtDetailTBL"] != null)
            {
                DataTable dtDetailTBL = (DataTable)Session["dtDetailTBL"];
                grdInvoice.DataSource = dtDetailTBL;
                grdInvoice.DataBind();

            }
            else
            {
                grdInvoice.DataSource = null;
                grdInvoice.DataBind();
            }
            //GetSubTotal();
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    private void BindShift()
    {
        try
        {
            DataTable dtShift = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftMaster();
            if (dtShift != null && dtShift.Rows.Count > 0)
            {
                ddlIssueShift.DataSource = dtShift;
                ddlIssueShift.DataTextField = "SFT_NAME";
                ddlIssueShift.DataValueField = "SFT_NAME";
                ddlIssueShift.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
            imgbtnSave.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to save this record')");
            imgbtnUpdate.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to update this record')");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNewInvoiceNumber();
    }

    protected void cmbParty_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyDatacmb(e.Text.ToUpper(), e.ItemsOffset);
            cmbParty.Items.Clear();
            cmbParty.DataSource = data;
            cmbParty.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCountcmb(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbParty_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyAddress.Text = cmbParty.SelectedValue.Trim();
            lblPartyCode.Text = cmbParty.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyDatacmb(string Text, int startOffset)
    {
        try
        {
            DataTable dt = null;
            string CommandText = "SELECT   DISTINCT PRTY_CODE,     PRTY_NAME FROM   (SELECT   M.PRTY_CODE,  M.PRTY_NAME                         FROM  YRN_IR_MST M WHERE          M.TRN_TYPE IN ('IYS26','IYS26')   AND  NVL(M.BILL_NUMB,0)=0         AND  ( M.PRTY_CODE LIKE :SearchQuery OR M.PRTY_NAME LIKE :SearchQuery)  ORDER BY  PRTY_CODE ASC  ) msd  WHERE    ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT   DISTINCT PRTY_CODE,     PRTY_NAME FROM   (SELECT   M.PRTY_CODE,  M.PRTY_NAME                         FROM  YRN_IR_MST M WHERE          M.TRN_TYPE IN ('IYS26','IYS26')   AND  NVL(M.BILL_NUMB,0)=0         AND  ( M.PRTY_CODE LIKE :SearchQuery OR M.PRTY_NAME LIKE :SearchQuery)  ORDER BY  PRTY_CODE ASC  ) msd  WHERE    ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = "%" + Text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression.ToUpper(), "", SearchQuery.ToUpper(), "");
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int GetPartyCountcmb(string text)
    {
        try
        {
            DataTable data = null;
            string CommandText = " SELECT   M.PRTY_CODE,  M.PRTY_NAME   FROM  YRN_IR_MST M WHERE          M.TRN_TYPE IN ('IYS26','IYS26')   AND  NVL(M.BILL_NUMB,0)=0         AND  ( M.PRTY_CODE LIKE :SearchQuery OR M.PRTY_NAME LIKE :SearchQuery)";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtTransporterCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyDataTran(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtTransporterCode.Items.Clear();
                txtTransporterCode.DataSource = data;
                txtTransporterCode.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCountTran(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transporters Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblTransporterCode.Text = txtTransporterCode.SelectedText.ToString().Trim();
            txtTransporterAddress.Text = txtTransporterCode.SelectedValue;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transporter Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtConsigneeCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData1(e.Text.ToUpper(), e.ItemsOffset);
            txtConsigneeCode.Items.Clear();
            txtConsigneeCode.DataSource = data;
            txtConsigneeCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtConsigneeCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            lblConsigneeCode.Text = txtConsigneeCode.SelectedText.ToString().Trim();
            txtConsigneeAddress.Text = txtConsigneeCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData1(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME ) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int GetPartyCount(string text)
    {
        try
        {
            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetLOVForParty(string Text)
    {
        try
        {
            string CommandText = "  SELECT DISTINCT *  FROM (SELECT *  FROM (SELECT DISTINCT     v.PRTY_CODE,     SO_TYPE,     PRTY_STATE,     PRTY_ADD1,     PRTY_ADD2,     PRTY_NAME,        PRTY_ADD1     || ',  '     || NVL (PRTY_ADD2, ' ')     || ',  '     || NVL (PRTY_STATE, ' ')        address                  FROM    TX_VENDOR_MST v     RIGHT OUTER JOIN        YRN_SO_MST p     ON V.PRTY_CODE = P.PRTY_CODE)      WHERE SO_TYPE  IN ('SSM','SSS') )";
            string WhereClause = " where PRTY_CODE like :SearchQuery or  PRTY_NAME like :SearchQuery or  PRTY_ADD1 like :SearchQuery  or PRTY_ADD2 like :SearchQuery or PRTY_STATE like :SearchQuery ";
            string SortExpression = " ORDER BY PRTY_CODE ";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetPartyDataTran(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME ) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE VENDOR_CAT_CODE = 'Transporter' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME ) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE VENDOR_CAT_CODE = 'Transporter' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY PRTY_CODE ASC";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int GetItemsCountTran(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_GRP_CODE = 'Transporter' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY PRTY_CODE ASC ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression.ToUpper(), "", SearchQuery.ToUpper(), "");
            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlInvoiceNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlInvoiceNumber.Items.Clear();
            DataTable data = GetInvoiceData(e.Text.ToUpper(), e.ItemsOffset, 10);
            ddlInvoiceNumber.DataSource = data;
            ddlInvoiceNumber.DataTextField = "INVOICE_NUMB";
            ddlInvoiceNumber.DataValueField = "INVOICE_NUMB";
            ddlInvoiceNumber.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetInvoiceDataCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Indent for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlInvoiceNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int invoice_NUMBER = int.Parse(ddlInvoiceNumber.SelectedValue.Trim());

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            dtDetailTBL.Rows.Clear();
            int iRecordFound = GetdataByInvoiceNumber(invoice_NUMBER);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Challan Number.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetInvoiceData(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.INVOICE_NUMB, a.INVOICE_DATE, a.PRTY_CODE, a.PRTY_NAME FROM V_TX_INVOICE_MST a WHERE NVL (A.STATUS, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.INVOICE_TYPE = '" + ddlInvoiceType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.INVOICE_NUMB DESC, a.INVOICE_DATE DESC) asd WHERE INVOICE_NUMB LIKE :searchQuery OR INVOICE_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND (INVOICE_NUMB, INVOICE_DATE, PRTY_CODE, PRTY_NAME) NOT IN(SELECT INVOICE_NUMB,INVOICE_DATE,PRTY_CODE,PRTY_NAME FROM (SELECT * FROM (SELECT a.INVOICE_NUMB,a.INVOICE_DATE,a.PRTY_CODE,a.PRTY_NAME FROM V_TX_INVOICE_MST a WHERE NVL (A.STATUS, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.INVOICE_TYPE = '" + ddlInvoiceType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.INVOICE_NUMB DESC, a.INVOICE_DATE DESC) asd WHERE INVOICE_NUMB LIKE :searchQuery OR INVOICE_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= '" + startOffset + "') ";
            }

            string SortExpression = " ORDER BY INVOICE_NUMB DESC, INVOICE_DATE DESC";
            string SearchQuery = text + "%";

            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression.ToUpper(), "", SearchQuery.ToUpper(), "");
            return data;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected int GetInvoiceDataCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.INVOICE_NUMB, a.INVOICE_DATE, a.PRTY_CODE, a.PRTY_NAME FROM V_TX_INVOICE_MST a WHERE NVL (A.STATUS, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.INVOICE_TYPE = '" + ddlInvoiceType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.INVOICE_NUMB DESC, a.INVOICE_DATE DESC) asd WHERE INVOICE_NUMB LIKE :searchQuery OR INVOICE_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) ";
            string whereClause = string.Empty;

            string SortExpression = " ";
            string SearchQuery = text + "%";

            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlYarnDetails_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlYarnDetails.Items.Clear();
            DataTable data = GetYarnDetails(e.Text.ToUpper(), e.ItemsOffset);
            ddlYarnDetails.DataTextField = "TRN_NUMB";
            ddlYarnDetails.DataValueField = "TRN_DATA";
            ddlYarnDetails.DataSource = data;
            ddlYarnDetails.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnDetails_Count(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PI Number loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetYarnDetails(string Text, int startoffset)
    {
        try
        {

            string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT *           FROM   (      SELECT   M.TRN_NUMB,   Y.YARN_CODE,         O.PARTY_ARTICLE_DESC YARN_DESC,   O.SHADE_CODE,  T.LOT_NO,   T.CARTONS NO_OF_UNIT ,  T.TRN_QTY,  O.ORDER_NO,  (   M.TRN_NUMB       || '@' || Y.YARN_CODE   || '@' || O.PARTY_ARTICLE_DESC  || '@' || O.SHADE_CODE     || '@'     || T.LOT_NO|| '@'  || T.CARTONS     || '@' || T.TRN_QTY || '@'   || O.FINAL_RATE|| '@'|| M.FORM_NUMB)   TRN_DATA FROM   YRN_MST Y,     YRN_ASSOCATED_MST YA,  YRN_IR_MST M,      YRN_IR_TRN T,         OD_CUSTOMER_REQUEST_ST O WHERE       Y.YARN_CODE = YA.YARN_CODE         AND M.COMP_CODE = T.COMP_CODE         AND M.BRANCH_CODE = T.BRANCH_CODE         AND M.YEAR = T.YEAR         AND M.TRN_NUMB = T.TRN_NUMB         AND M.TRN_TYPE = T.TRN_TYPE         AND T.YARN_CODE = Y.YARN_CODE         AND O.ARTICLE_NO = YA.ASS_YARN_CODE         AND T.SHADE_CODE = O.SHADE_CODE         AND O.ORDER_NO = T.PI_NO            AND NVL(M.BILL_NUMB,0)=0             AND M.PRTY_CODE IN ('" + lblPartyCode.Text + "')    and M.TRN_NUMB ='" + ddlTRNNumber.SelectedText.ToString() + "'      AND       (M.TRN_NUMB LIKE :SearchQuery                       OR T.YARN_CODE LIKE :SearchQuery                       OR  O.ARTICLE_NO LIKE :SearchQuery                     OR O.PARTY_ARTICLE_DESC LIKE :SearchQuery                       OR O.SHADE_CODE LIKE :SearchQuery OR O.ORDER_NO LIKE :SearchQuery)      ) asd       ORDER BY   TRN_DATA ASC) WHERE   ROWNUM < 15";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += " AND (TRN_NUMB,  YARN_CODE,SHADE_CODE,LOT_NO) NOT IN   ( SELECT TRN_NUMB,  YARN_CODE,SHADE_CODE,LOT_NO FROM ( SELECT   DISTINCT *           FROM   (      SELECT   M.TRN_NUMB,   Y.YARN_CODE,         O.PARTY_ARTICLE_DESC YARN_DESC,   O.SHADE_CODE,  T.LOT_NO,  T.CARTONS NO_OF_UNIT,  T.TRN_QTY,  O.ORDER_NO,  (   M.TRN_NUMB       || '@' || Y.YARN_CODE   || '@' || O.PARTY_ARTICLE_DESC  || '@' || O.SHADE_CODE     || '@'     || T.LOT_NO|| '@'  || T.CARTONS      || '@' || T.TRN_QTY || '@'   || O.FINAL_RATE|| '@'|| M.FORM_NUMB)   TRN_DATA FROM   YRN_MST Y,     YRN_ASSOCATED_MST YA,  YRN_IR_MST M,      YRN_IR_TRN T,         OD_CUSTOMER_REQUEST_ST O WHERE       Y.YARN_CODE = YA.YARN_CODE         AND M.COMP_CODE = T.COMP_CODE         AND M.BRANCH_CODE = T.BRANCH_CODE         AND M.YEAR = T.YEAR         AND M.TRN_NUMB = T.TRN_NUMB         AND M.TRN_TYPE = T.TRN_TYPE         AND T.YARN_CODE = Y.YARN_CODE         AND O.ARTICLE_NO = YA.ASS_YARN_CODE         AND T.SHADE_CODE = O.SHADE_CODE         AND O.ORDER_NO = T.PI_NO            AND NVL(M.BILL_NUMB,0)=0             AND M.PRTY_CODE IN ('" + lblPartyCode.Text + "')    and M.TRN_NUMB ='" + ddlTRNNumber.SelectedText.ToString() + "'      AND       (M.TRN_NUMB LIKE :SearchQuery                       OR T.YARN_CODE LIKE :SearchQuery                       OR  O.ARTICLE_NO LIKE :SearchQuery                     OR O.PARTY_ARTICLE_DESC LIKE :SearchQuery                       OR O.SHADE_CODE LIKE :SearchQuery OR O.ORDER_NO LIKE :SearchQuery)      ) asd       ORDER BY   TRN_DATA ASC )  WHERE ROWNUM < '" + startoffset + "' ) ";

            }

            string SortExpression = " ORDER BY TRN_NUMB ASC";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    private int GetYarnDetails_Count(string Text)
    {
        try
        {
            string CommandText = " SELECT   M.TRN_NUMB,   Y.YARN_CODE,         O.PARTY_ARTICLE_DESC YARN_DESC,   O.SHADE_CODE,  T.LOT_NO,   T.NO_OF_UNIT,  T.TRN_QTY,  O.ORDER_NO,  (   M.TRN_NUMB       || '@' || Y.YARN_CODE   || '@' || O.PARTY_ARTICLE_DESC  || '@' || O.SHADE_CODE     || '@'     || T.LOT_NO|| '@'  || T.CARTONS      || '@' || T.TRN_QTY || '@'   || O.FINAL_RATE)   TRN_DATA FROM   YRN_MST Y,     YRN_ASSOCATED_MST YA,  YRN_IR_MST M,      YRN_IR_TRN T,         OD_CUSTOMER_REQUEST_ST O WHERE       Y.YARN_CODE = YA.YARN_CODE         AND M.COMP_CODE = T.COMP_CODE         AND M.BRANCH_CODE = T.BRANCH_CODE         AND M.YEAR = T.YEAR         AND M.TRN_NUMB = T.TRN_NUMB         AND M.TRN_TYPE = T.TRN_TYPE         AND T.YARN_CODE = Y.YARN_CODE         AND O.ARTICLE_NO = YA.ASS_YARN_CODE         AND T.SHADE_CODE = O.SHADE_CODE         AND O.ORDER_NO = T.PI_NO            AND NVL(M.BILL_NUMB,0)=0             AND M.PRTY_CODE IN ('" + lblPartyCode.Text + "')         AND       (M.TRN_NUMB LIKE :SearchQuery                       OR T.YARN_CODE LIKE :SearchQuery                       OR  O.ARTICLE_NO LIKE :SearchQuery                     OR O.PARTY_ARTICLE_DESC LIKE :SearchQuery                       OR O.SHADE_CODE LIKE :SearchQuery OR O.ORDER_NO LIKE :SearchQuery)       ";
            string whereClause = string.Empty;
            string SortExpression = " ";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "").Rows.Count; ;


        }
        catch
        {
            throw;
        }
    }
    protected void ddlYarnDetails_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            GetDataForYarnDetail(ddlYarnDetails.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Yarn details Selection"));
            lblMode.Text = ex.ToString();
        }
    }
    private void GetDataForYarnDetail(string cString)
    {
        try
        {
            string[] ss = cString.Split('@');
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(ss[1].ToString(), UNIQUEID))
            {
                txtDCNo.Text = ss[0].ToString();
                lblYarnCode.Text = ss[1].ToString();
                txtYarnDesc.Text = ss[2].ToString();
                txtShade.Text = ss[3].ToString();
                txtLotNo.Text = ss[4].ToString();
                txtNoOfUnit.Text = ss[5].ToString();
                txtQty.Text = ss[6].ToString();
                txtRate.Text = ss[7].ToString();
                if (!string.IsNullOrEmpty(ss[8].ToString()))
                {
                    if (!string.IsNullOrEmpty(txtBuyerorder.Text))
                    {
                        txtBuyerorder.Text = txtBuyerorder.Text + "," + ss[8].ToString();
                    }
                    else
                    {
                        txtBuyerorder.Text = ss[8].ToString();
                    }
                }
                CalculateAmount();
                btnsaveTRNDetail.Focus();
                if (Session["dtDetailTBL"] != null)
                {
                    dtDetailTBL = (DataTable)Session["dtDetailTBL"];
                }
                else
                {
                    dtDetailTBL = CreateDataTable();
                }

                string TRN_TYPE1 = string.Empty;
                if (ddlInvoiceType.SelectedValue.ToString() == "SALEWORK") { TRN_TYPE1 = "IYS26"; }
                else if (ddlInvoiceType.SelectedValue.ToString() == "JOBWORK") { TRN_TYPE1 = "IYS27"; }
                else { TRN_TYPE1 = "IYS29"; }

            //    dtDetailTBL.Rows.Clear();
               // dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetDODetails(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, int.Parse(ss[0].ToString()), TRN_TYPE1);
                MapDataTable();
                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.GetDOCartonDetails(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, int.Parse(ss[0].ToString()), TRN_TYPE1);
                    MapAdjustDataTable(dtReceiptAdjustment);
                }
                //BindGridFromDataTable();


            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Article With This Shade Already Included.Select another article/ shade");
            }
        }
        catch
        {
            throw;
        }
    }


    private void MapAdjustDataTable(DataTable dtReceiptAdjustment)
    {
        try
        {

            if (!dtReceiptAdjustment.Columns.Contains("UNIQUEID"))
                dtReceiptAdjustment.Columns.Add("UNIQUEID", typeof(double));

            for (int iLoop = 0; iLoop < dtReceiptAdjustment.Rows.Count; iLoop++)
            {
                dtReceiptAdjustment.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
            }
            Session["dtItemReceipt"] = dtReceiptAdjustment;
        }
        catch
        {
            throw;
        }
    }

    public void CalculateAmount()
    {
        double _QTY = 0;
        double _RATE = 0;
        double.TryParse(txtQty.Text, out _QTY);
        double.TryParse(txtRate.Text, out _RATE);
        txtAmount.Text = (_QTY * _RATE).ToString();
    }
    protected void btnTRNCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refresh detail information.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }

            if (txtYarnDesc.Text != "" && txtQty.Text != "" && txtRate.Text != "")
            {
                int UNIQUEID = 0;
                if (ViewState["UNIQUEID"] != null)
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                bool bb = SearchItemCodeInGrid(txtYarnDesc.Text.Trim(), UNIQUEID);
                if (!bb)
                {
                    double Qty = 0;
                    double.TryParse(txtQty.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {
                        if (UNIQUEID > 0)
                        {
                            DataView dv = new DataView(dtDetailTBL);
                            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                            if (dv.Count > 0)
                            {
                                double NoOfUnit = 0;
                                double.TryParse(txtNoOfUnit.Text, out NoOfUnit);
                                double RATE = 0;
                                double.TryParse(txtRate.Text, out RATE);
                                Int64 INVOICENO = 0;
                                Int64.TryParse(txtInvoiceNumber.Text, out INVOICENO);
                                dv[0]["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                dv[0]["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                                dv[0]["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dv[0]["INVOICE_NUMB"] = INVOICENO;
                                dv[0]["INVOICE_TYPE"] = ddlInvoiceType.SelectedValue;
                                dv[0]["YARN_CODE"] = lblYarnCode.Text;
                                dv[0]["YARN_DESC"] = txtYarnDesc.Text;
                                dv[0]["SHADE_CODE"] = txtShade.Text.Trim();
                                dv[0]["LOT_NO"] = txtLotNo.Text;
                                dv[0]["DOC_NO"] = txtDCNo.Text;
                                dv[0]["NO_OF_UNIT"] = NoOfUnit;
                                dv[0]["QUANTITY"] = Qty;
                                dv[0]["RATE"] = RATE;
                                dv[0]["AMOUNT"] = Qty * RATE;
                                dv[0]["TUSER"] = oUserLoginDetail.UserCode;
                                dv[0]["TDATE"] = DateTime.Now;
                                dv[0]["STATUS"] = "0";
                                //dv[0]["CHALLAN_NO"] = ddlTRNNumber.SelectedValue.ToString().Trim();
                                //dv[0]["PI_NO"] = txtPINO.Text.ToString().Trim();

                            }
                        }
                        else
                        {
                            DataRow dr = dtDetailTBL.NewRow();
                            dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                            double NoOfUnit = 0;
                            double.TryParse(txtNoOfUnit.Text, out NoOfUnit);
                            double RATE = 0;
                            double.TryParse(txtRate.Text, out RATE);
                            Int64 INVOICENO = 0;
                            Int64.TryParse(txtInvoiceNumber.Text, out INVOICENO);
                            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                            dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["INVOICE_NUMB"] = INVOICENO;
                            dr["INVOICE_TYPE"] = ddlInvoiceType.SelectedValue;
                            dr["YARN_CODE"] = lblYarnCode.Text;
                            dr["YARN_DESC"] = txtYarnDesc.Text;
                            dr["SHADE_CODE"] = txtShade.Text.Trim();
                            dr["LOT_NO"] = txtLotNo.Text;
                            dr["DOC_NO"] = txtDCNo.Text;
                            dr["NO_OF_UNIT"] = NoOfUnit;
                            dr["QUANTITY"] = Qty;
                            dr["RATE"] = RATE;
                            dr["AMOUNT"] = Qty * RATE;
                            dr["TUSER"] = oUserLoginDetail.UserCode;
                            dr["TDATE"] = DateTime.Now;
                            dr["STATUS"] = 0;
                            //dr["CHALLAN_NO"] = ddlTRNNumber.SelectedValue.ToString().Trim();
                            //dr["PI_NO"] = txtPINO.Text.ToString().Trim();
                            dtDetailTBL.Rows.Add(dr);

                        }
                        RefreshDetailRow();
                        Session["dtDetailTBL"] = dtDetailTBL;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Yarn with this Shade already included. Please select another Yarn/ shade');", true);
                }
            }
            grdInvoice.DataSource = dtDetailTBL;
            grdInvoice.DataBind();


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding YARN detail data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchItemCodeInGrid(string yarnCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdInvoice.Rows)
            {
                Label lblYARN_CODE = (Label)grdRow.FindControl("lblYARN_CODE");
                Label lblSHADE_CODE = (Label)grdRow.FindControl("lblSHADE_CODE");
                Label lblDOC_NO = (Label)grdRow.FindControl("lblDOC_NO");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (lblYARN_CODE.ToolTip.Trim() == yarnCode && UniqueId != iUniqueId && txtShade.Text == lblSHADE_CODE.Text && txtDCNo.Text.Trim() == lblDOC_NO.Text.Trim())
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void grdInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItemRow(UniqueId);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItemRow(UniqueId);
                BindGridFromDataTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Editing/ deletion of Material detail.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

    private void EditItemRow(int UNIQUEID)
    {
        try
        {

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                ViewState["UNIQUEID"] = UNIQUEID;
                ddlYarnDetails.SelectedText = dv[0]["YARN_CODE"].ToString();
                lblYarnCode.Text = dv[0]["YARN_CODE"].ToString();
                txtYarnDesc.Text = dv[0]["YARN_DESC"].ToString();
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();
                txtLotNo.Text = dv[0]["LOT_NO"].ToString();
                txtDCNo.Text = dv[0]["DOC_NO"].ToString();
                txtNoOfUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtQty.Text = dv[0]["QUANTITY"].ToString();
                txtRate.Text = dv[0]["RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                ddlYarnDetails.Enabled = false;

            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteItemRow(int UniqueId)
    {
        try
        {

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUniqueId = int.Parse(dr["UNIQUEID"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtDetailTBL.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUEID"] = iCount;
                }
            }
            Session["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;

        }
    }

    private int GetdataByInvoiceNumber(int InvoiceNumber)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_INVOICE_MST.GetDataByINVOICE_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, InvoiceNumber, ddlInvoiceType.SelectedValue, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtInvoiceNumber.Text = InvoiceNumber.ToString();
                ddlInvoiceType.SelectedValue = dt.Rows[0]["INVOICE_TYPE"].ToString().Trim();
                txtInvoiceDate.Text = DateTime.Parse(dt.Rows[0]["INVOICE_DATE"].ToString().Trim()).ToShortDateString();
                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                lblConsigneeCode.Text = dt.Rows[0]["AGENT_CODE"].ToString().Trim();
                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TRSP_NAME"].ToString().Trim();
                txtConsigneeAddress.Text = dt.Rows[0]["AGENT_NAME"].ToString().Trim();
                txtDateOfRemoval.Text = DateTime.Parse(dt.Rows[0]["INVOICE_REM_DATE"].ToString().Trim()).ToShortDateString();
                txtBuyerorder.Text = dt.Rows[0]["PO_NUMB"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["VEHICLE_NO"].ToString().Trim();
                //ddlIssueShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();        
                txtLRNumber.Text = dt.Rows[0]["LR_NO"].ToString().Trim();
                //DateTime dateLR = DateTime.Now;
                //DateTime.TryParse(dt.Rows[0]["LR_DATE"].ToString().Trim(), out dateLR);
                // txtLRDate.Text = dateLR.ToShortDateString();
                //txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();

                //PRODUCT_TYPE,                            
                //PRODUCT_DETAILS,    
                //TOTAL_AMOUNT,
                //FINAL_AMOUNT,

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_INVOICE_MST.GetTRN_DataByINVOICE_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, InvoiceNumber, ddlInvoiceType.SelectedValue);
                MapDataTable();
                BindGridFromDataTable();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear " + oUserLoginDetail.Username + " !!  Modification not allowed.");
                //InitialisePage();  
                //ActivateUpdateMode();               
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable()
    {
        try
        {

            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
            {
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            }
            if (!dtDetailTBL.Columns.Contains("COMP_CODE"))
            {
                dtDetailTBL.Columns.Add("COMP_CODE", typeof(string));
            }
            if (!dtDetailTBL.Columns.Contains("BRANCH_CODE"))
            {
                dtDetailTBL.Columns.Add("BRANCH_CODE", typeof(string));
            }
            if (!dtDetailTBL.Columns.Contains("YEAR"))
            {
                dtDetailTBL.Columns.Add("YEAR", typeof(int));
            }
            if (!dtDetailTBL.Columns.Contains("INVOICE_NUMB"))
            {
                dtDetailTBL.Columns.Add("INVOICE_NUMB", typeof(int));
            }
            if (!dtDetailTBL.Columns.Contains("INVOICE_TYPE"))
            {
                dtDetailTBL.Columns.Add("INVOICE_TYPE", typeof(string));
            }
            if (!dtDetailTBL.Columns.Contains("TUSER"))
            {
                dtDetailTBL.Columns.Add("TUSER", typeof(string));
            }
            if (!dtDetailTBL.Columns.Contains("TDATE"))
            {
                dtDetailTBL.Columns.Add("TDATE", typeof(DateTime));
            }
            if (!dtDetailTBL.Columns.Contains("STATUS"))
            {
                dtDetailTBL.Columns.Add("STATUS", typeof(string));
            }


            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                dtDetailTBL.Rows[iLoop]["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                dtDetailTBL.Rows[iLoop]["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                dtDetailTBL.Rows[iLoop]["INVOICE_NUMB"] = int.Parse(txtInvoiceNumber.Text);
                dtDetailTBL.Rows[iLoop]["INVOICE_TYPE"] = ddlInvoiceType.SelectedValue;
                dtDetailTBL.Rows[iLoop]["TUSER"] = oUserLoginDetail.UserCode;
                dtDetailTBL.Rows[iLoop]["TDATE"] = DateTime.Now;
                dtDetailTBL.Rows[iLoop]["STATUS"] = "0";
            }

            Session["dtDetailTBL"] = dtDetailTBL;
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
            InitialisePage();
            ActivateUpdateMode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation mode.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearingdata.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Yarn/SalesWork/Pages/InvoiceReportCRPrint.aspx", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in generating report data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                saveInvoice();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                updateInvoice();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating data.\r\nSee error log for detail."));
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

            if (txtInvoiceNumber.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Invoice Number required.\r\n";
            }

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for Invoicing.\r\n";
            }

            if (count == 2)
                ReturnResult = true;

            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }

    private void saveInvoice()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_INVOICE_MST oTX_INVOICE_MST = new SaitexDM.Common.DataModel.TX_INVOICE_MST();

            oTX_INVOICE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_INVOICE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_INVOICE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_INVOICE_MST.INVOICE_NUMB = int.Parse(CommonFuction.funFixQuotes(txtInvoiceNumber.Text.Trim()));
            oTX_INVOICE_MST.INVOICE_TYPE = CommonFuction.funFixQuotes(ddlInvoiceType.SelectedValue);
            DateTime Invdt = System.DateTime.Now.Date;
            DateTime.TryParse(txtInvoiceDate.Text.Trim(), out Invdt);
            oTX_INVOICE_MST.INVOICE_DATE = Invdt;
            DateTime remdt = System.DateTime.Now.Date;
            DateTime.TryParse(txtDateOfRemoval.Text.Trim(), out remdt);
            oTX_INVOICE_MST.INVOICE_REM_DATE = remdt;
            oTX_INVOICE_MST.LR_NO = CommonFuction.funFixQuotes(txtLRNumber.Text.Trim());
            oTX_INVOICE_MST.PRTY_CODE = lblPartyCode.Text.Trim();
            oTX_INVOICE_MST.TRSP_CODE = lblTransporterCode.Text.Trim();
            oTX_INVOICE_MST.AGENT_CODE = lblConsigneeCode.Text.Trim();
            oTX_INVOICE_MST.PO_NUMB = CommonFuction.funFixQuotes(txtBuyerorder.Text);
            oTX_INVOICE_MST.VEHICLE_NO = txtVehicleNo.Text;
            oTX_INVOICE_MST.TDATE = DateTime.Now;
            oTX_INVOICE_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_INVOICE_MST.STATUS = "0";

            oTX_INVOICE_MST.PRODUCT_TYPE = string.Empty;
            oTX_INVOICE_MST.PRODUCT_DETAILS = string.Empty;
            oTX_INVOICE_MST.TOTAL_AMOUNT = 0;
            oTX_INVOICE_MST.FINAL_AMOUNT = 0;
            oTX_INVOICE_MST.CHALLAN_NO = int.Parse(CommonFuction.funFixQuotes(ddlTRNNumber.SelectedValue.ToString().Trim()));
            oTX_INVOICE_MST.PI_NO = txtPINO.Text.ToString().Trim();


            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            int INVOICE_NUMB = 0;
            bool result = SaitexBL.Interface.Method.TX_INVOICE_MST.Insert(oTX_INVOICE_MST, out INVOICE_NUMB, dtDetailTBL);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"Invoice Number : " + INVOICE_NUMB + " saved successfully.");

            }
            else
            {
                CommonFuction.ShowMessage("data  Saving Failed");
            }
        }
        catch
        {
            throw;
        }
    }

    private void updateInvoice()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_INVOICE_MST oTX_INVOICE_MST = new SaitexDM.Common.DataModel.TX_INVOICE_MST();

            oTX_INVOICE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_INVOICE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_INVOICE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_INVOICE_MST.INVOICE_NUMB = int.Parse(CommonFuction.funFixQuotes(txtInvoiceNumber.Text.Trim()));
            oTX_INVOICE_MST.INVOICE_TYPE = CommonFuction.funFixQuotes(ddlInvoiceType.SelectedValue);
            DateTime Invdt = System.DateTime.Now.Date;
            DateTime.TryParse(txtInvoiceDate.Text.Trim(), out Invdt);
            oTX_INVOICE_MST.INVOICE_DATE = Invdt;
            DateTime remdt = System.DateTime.Now.Date;
            DateTime.TryParse(txtDateOfRemoval.Text.Trim(), out remdt);
            oTX_INVOICE_MST.INVOICE_REM_DATE = remdt;
            oTX_INVOICE_MST.LR_NO = CommonFuction.funFixQuotes(txtLRNumber.Text.Trim());
            oTX_INVOICE_MST.PRTY_CODE = lblPartyCode.Text.Trim();
            oTX_INVOICE_MST.TRSP_CODE = lblTransporterCode.Text.Trim();
            oTX_INVOICE_MST.AGENT_CODE = lblConsigneeCode.Text.Trim();
            oTX_INVOICE_MST.PO_NUMB = CommonFuction.funFixQuotes(txtBuyerorder.Text);
            oTX_INVOICE_MST.VEHICLE_NO = txtVehicleNo.Text;
            oTX_INVOICE_MST.TDATE = DateTime.Now;
            oTX_INVOICE_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_INVOICE_MST.STATUS = "0";

            oTX_INVOICE_MST.PRODUCT_TYPE = string.Empty;
            oTX_INVOICE_MST.PRODUCT_DETAILS = string.Empty;
            oTX_INVOICE_MST.TOTAL_AMOUNT = 0;
            oTX_INVOICE_MST.FINAL_AMOUNT = 0;

            oTX_INVOICE_MST.CHALLAN_NO = int.Parse(CommonFuction.funFixQuotes(ddlTRNNumber.SelectedValue.ToString().Trim()));
            oTX_INVOICE_MST.PI_NO = txtPINO.Text.ToString().Trim();


            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            bool result = SaitexBL.Interface.Method.TX_INVOICE_MST.Update(oTX_INVOICE_MST, dtDetailTBL);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"Invoice Number : " + oTX_INVOICE_MST.INVOICE_NUMB + " updated successfully.");

            }
            else
            {
                CommonFuction.ShowMessage("data  Updation Failed");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                GridView grdRecAdj = (GridView)e.Row.FindControl("grdRecAdj");

                LinkButton lnkbtnTrnNumb = (LinkButton)e.Row.FindControl("lnkbtnTrnNumb");
                GridView grdTaxDetails = (GridView)e.Row.FindControl("grdTaxDetails");

                if (Session["TaxDetails"] != null)
                {
                    DataTable dtTaxDetails = (DataTable)Session["TaxDetails"];
                    grdTaxDetails.DataSource = dtTaxDetails;
                    grdTaxDetails.DataBind();
                    grdTaxDetails.Visible = true;
                }

                int UNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.ToString());

                if (Session["dtDetailTBL"] != null)
                {
                    dtDetailTBL = (DataTable)Session["dtDetailTBL"];
                }
                else
                {
                    dtDetailTBL = CreateDataTable();
                }
                DataView dv = new DataView(dtDetailTBL);
                dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                if (dv.Count > 0)
                {
                    string sYARN_CODE = dv[0]["YARN_CODE"].ToString();
                    string sSHADE_CODE = dv[0]["SHADE_CODE"].ToString();
                    string sDOC_NO = dv[0]["DOC_NO"].ToString();
                    if (Session["dtItemReceipt"] != null)
                    {
                        DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
                        DataView dvsub = new DataView(dtItemReceipt);
                        dvsub.RowFilter = " YARN_CODE='" + sYARN_CODE + "' and SHADE_CODE='" + sSHADE_CODE + "' and DOC_NO='" + sDOC_NO + "'";
                        if (dvsub.Count > 0)
                        {
                            grdRecAdj.DataSource = dvsub;
                            grdRecAdj.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void ddlTRNNumber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetIssueData(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "TRN_NUMB";
            thisTextBox.DataValueField = "TRN_NUMB";
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetIssueDataCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Indent for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetIssueData(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string CommandText = string.Empty;
            if (ddlInvoiceType.SelectedValue == "SALEWORK")
            {

                CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_YARN_IR_MST a, YRN_IR_TRN b WHERE NVL (A.CONF_FLAG, 0) = 0 AND NVL (A.BILL_NUMB, 0) = 0 AND A.COMP_CODE = B.COMP_CODE AND A.BRANCH_CODE = B.BRANCH_CODE AND A.YEAR = B.YEAR AND A.TRN_TYPE = B.TRN_TYPE AND A.TRN_NUMB = B.TRN_NUMB AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = 'IYS26' AND A.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'  ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= 15 ";
            }
            if (ddlInvoiceType.SelectedValue == "JOBWORK")
            {
                CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME, b.PI_NO FROM V_YARN_IR_MST a, YRN_IR_TRN b WHERE NVL (A.CONF_FLAG, 0) = 0 AND NVL (A.BILL_NUMB, 0) = 0 AND A.COMP_CODE = B.COMP_CODE AND A.BRANCH_CODE = B.BRANCH_CODE AND A.YEAR = B.YEAR AND A.TRN_TYPE = B.TRN_TYPE AND A.TRN_NUMB = B.TRN_NUMB AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = 'IYS27' AND A.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'  ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= 15 ";

            }

            // string CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_YARN_IR_MST a WHERE NVL (A.CONF_FLAG, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + ddlInvoiceType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= 15 ";



            if (ddlInvoiceType.SelectedValue == "FROM STOCK")
            {

                CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME, b.PI_NO FROM V_YARN_IR_MST a, YRN_IR_TRN b WHERE NVL (A.CONF_FLAG, 0) = 0 AND NVL (A.BILL_NUMB, 0) = 0 AND A.COMP_CODE = B.COMP_CODE AND A.BRANCH_CODE = B.BRANCH_CODE AND A.YEAR = B.YEAR AND A.TRN_TYPE = B.TRN_TYPE AND A.TRN_NUMB = B.TRN_NUMB AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = 'IYS29' AND A.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'  ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= 15 ";
            }





            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND (TRN_NUMB, TRN_DATE, PRTY_CODE, PRTY_NAME, DEPT_NAME) NOT IN(SELECT TRN_NUMB,TRN_DATE,PRTY_CODE,PRTY_NAME,DEPT_NAME FROM (SELECT * FROM (SELECT a.TRN_NUMB,a.TRN_DATE,a.PRTY_CODE,a.PRTY_NAME,a.DEPT_NAME FROM V_YARN_IR_MST a WHERE NVL (A.CONF_FLAG, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + ddlInvoiceType.SelectedValue + "' AND A.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR a.TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= '" + startOffset + "') ";
            }

            string SortExpression = " ORDER BY TRN_NUMB DESC, TRN_DATE DESC";
            string SearchQuery = text + "%";

            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetIssueDataCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            if (ddlInvoiceType.SelectedValue == "SALEWORK")
            {
                CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_YARN_IR_MST a WHERE NVL (A.CONF_FLAG, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = 'IYS26' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) ";
            }
            if (ddlInvoiceType.SelectedValue == "JOBWORK")
            {
                CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_YARN_IR_MST a WHERE NVL (A.CONF_FLAG, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = 'IYS27' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) ";
            }

            string whereClause = string.Empty;

            string SortExpression = " ";
            string SearchQuery = text + "%";

            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ddlTRNNumber.SelectedValue.ToString() == txtInvoiceNumber.Text.ToString())
            {
                int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());
                txtChallanNo.Text = ddlTRNNumber.SelectedValue.ToString();

                if (Session["dtDetailTBL"] != null)
                {
                    dtDetailTBL = (DataTable)Session["dtDetailTBL"];
                }
                else
                {
                    dtDetailTBL = CreateDataTable();
                }
                dtDetailTBL.Rows.Clear();
                int iRecordFound = GetdataByChallaNumber(TRN_NUMBER);
                BindGridFromDataTable();
            }
            else
            {
                Common.CommonFuction.ShowMessage(" Challan No and Invoice No should be Same");

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Challan Number.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }


    private void BindNewChallanNum()
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.TRN_TYPE = ddlInvoiceType.SelectedValue;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

        }
        catch
        {
            throw;
        }
    }

    private int GetdataByChallaNumber(int ChallanNumber)
    {
        int iRecordFound = 0;
        try
        {
            cmbParty.Enabled = false;
            txtTransporterCode.Enabled = false;
            txtConsigneeCode.Enabled = false;

            if (ddlInvoiceType.SelectedValue == "SALEWORK")
            {
                TRN_TYPE = "IYS26";
            }
            if (ddlInvoiceType.SelectedValue == "JOBWORK")
            {
                TRN_TYPE = "IYS27";
            }

            if (ddlInvoiceType.SelectedValue == "FROM STOCK")
            {
                TRN_TYPE = "IYS29";
            }
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetInvoiceDataByChallan_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;

                txtBuyerorder.Text = dt.Rows[0]["FORM_NUMB"].ToString().Trim();
                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                lblConsigneeCode.Text = dt.Rows[0]["CONSIGNEE_CODE"].ToString().Trim();
                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TRSP_NAME"].ToString().Trim();
                txtConsigneeAddress.Text = dt.Rows[0]["CONSIGNEE_NAME"].ToString().Trim();
                txtEwayBillNo.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                txtLRNumber.Text = dt.Rows[0]["LR_NUMB"].ToString().Trim();
                txtPINO.Text = dt.Rows[0]["PI_NO"].ToString().Trim();

            }
            DataTable dt2 = new DataTable();
            dt2 = SaitexBL.Interface.Method.YRN_IR_MST.GetInvoiceSaleTaxDisData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, TRN_TYPE, ChallanNumber);

            Session["TaxDetails"] = dt2;

            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }


}
