using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using Obout.ComboBox;

public partial class Module_GateEntry_Controls_Gate_Pass : System.Web.UI.UserControl
{
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private DataTable dtDicRate = null;
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
                   
            BlankRecords();
            ActivateSaveMode();
            BindNewGateNumber();
            BindShift();  
            CreateDataTable();
            txtGateDate.Text = System.DateTime.Now.ToShortDateString();           
            RefreshDetailRow();
        }
        catch
        {
            throw;
        }
    }

    private void BindNewGateNumber()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_GATE_PASS_MST oTX_GATE_PASS_MST = new SaitexDM.Common.DataModel.TX_GATE_PASS_MST();
            oTX_GATE_PASS_MST.GATE_TYPE = ddlGateType.SelectedValue;
            oTX_GATE_PASS_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_GATE_PASS_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_GATE_PASS_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtGateNumber.Text = SaitexBL.Interface.Method.TX_GATE_PASS_MST.GetNewGatePassNumber(oTX_GATE_PASS_MST).ToString();
        }
        catch
        {
            throw;
        }
    }

    private void BlankRecords()
    {
        try
        {
          
            txtGateNumber.Text = string.Empty;
            txtGateNumber.ReadOnly = true;
            ddlGateNumber.SelectedIndex = -1;
            ddlGateType.SelectedIndex = -1;
            txtGateDate.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;    
            cmbParty.SelectedIndex = -1;         
            txtTransporterCode.SelectedIndex = -1;
            lblTransporterCode.Text = string.Empty;
            lblPartyCode.Text = string.Empty;         
            txtPartyAddress.Text = string.Empty;         
            txtTransporterAddress.Text = string.Empty; 
            Session["dtDetailTBL"] = null;
            grdGatePass.DataSource = null;
            grdGatePass.DataBind();           
           

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
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "Save";
            txtGateNumber.Text = string.Empty;
            ddlGateNumber.SelectedIndex = -1;
            ddlGateNumber.Items.Clear();
            txtGateNumber.Visible = true;
            ddlGateNumber.Visible = false;
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
            
            tdSave.Visible = false;
            tdUpdate.Visible = true ;
            lblMode.Text = "Update";
            txtGateNumber.Text = string.Empty;            
            ddlGateNumber.Items.Clear();
            txtGateNumber.Visible = false;
            ddlGateNumber.Visible = true;
            
        }
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        ddlYarnDetails.SelectedIndex = -1;
        lblYarnCode.Text = string.Empty;
        txtYarnDesc.Text = string.Empty;
        txtLotNo.Text = string.Empty;
        txtGrade.Text = string.Empty;
        txtShade.Text = string.Empty;
        txtDCNo.Text = string.Empty;
        txtDCDate.Text = string.Empty;
        txtNoOfUnit.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtInvoiceNumb.Text = string.Empty;
        ViewState["UNIQUEID"] = null;
        ddlYarnDetails.Enabled = true;
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
            dtDetailTBL.Columns.Add("GATE_NUMB", typeof(Int64));
            dtDetailTBL.Columns.Add("GATE_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("DOC_NO", typeof(string));
            dtDetailTBL.Columns.Add("DOC_DATE", typeof(DateTime));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("QUANTITY", typeof(double));
            dtDetailTBL.Columns.Add("INVOICE_NUMB", typeof(string));          
            dtDetailTBL.Columns.Add("TUSER", typeof(string));
            dtDetailTBL.Columns.Add("TDATE", typeof(DateTime));
            dtDetailTBL.Columns.Add("STATUS", typeof(string));                 
            return dtDetailTBL;

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
            if (Session["dtDetailTBL"] != null)
            {
                DataTable dtDetailTBL = (DataTable)Session["dtDetailTBL"];
                grdGatePass.DataSource = dtDetailTBL;
                grdGatePass.DataBind();
             
            }
            else
            {
                grdGatePass.DataSource = null;
                grdGatePass.DataBind();
            }
            //GetSubTotal();
        }
        catch
        {
            throw;
        }
    }

    private void BindShift()
    {
        //try
        //{
        //    DataTable dtShift = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftMaster();
        //    if (dtShift != null && dtShift.Rows.Count > 0)
        //    {
        //        ddlIssueShift.DataSource = dtShift;
        //        ddlIssueShift.DataTextField = "SFT_NAME";
        //        ddlIssueShift.DataValueField = "SFT_NAME";
        //        ddlIssueShift.DataBind();
        //    }
        //}
        //catch
        //{
        //    throw;
        //}
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
        catch
        {
            throw;
        }
    }

    protected void ddlGateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNewGateNumber();
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

            string CommandText = "SELECT   DISTINCT PRTY_CODE,     PRTY_NAME FROM   (SELECT   M.PRTY_CODE,  M.PRTY_NAME                         FROM  YRN_IR_MST M WHERE          M.TRN_TYPE IN ('IYS26','IYS27','IYS29')   AND  NVL(M.GATE_NUMB,0)=0         AND  ( M.PRTY_CODE LIKE :SearchQuery OR M.PRTY_NAME LIKE :SearchQuery)  ORDER BY  PRTY_CODE ASC  ) msd  WHERE    ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT   DISTINCT PRTY_CODE,     PRTY_NAME FROM   (SELECT   M.PRTY_CODE,  M.PRTY_NAME                         FROM  YRN_IR_MST M WHERE          M.TRN_TYPE IN ('IYS26','IYS27','IYS29')   AND  NVL(M.GATE_NUMB,0)=0         AND  ( M.PRTY_CODE LIKE :SearchQuery OR M.PRTY_NAME LIKE :SearchQuery)  ORDER BY  PRTY_CODE ASC  ) msd  WHERE    ROWNUM <= " + startOffset + ")";
                }

                string SortExpression = " order by PRTY_CODE";
                string SearchQuery = "%" + Text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            
           
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCountcmb(string text)
    {
        DataTable data = null;

        string CommandText = " SELECT   M.PRTY_CODE,  M.PRTY_NAME   FROM  YRN_IR_MST M WHERE          M.TRN_TYPE IN ('IYS26','IYS27','IYS29')   AND  NVL(M.GATE_NUMB,0)=0         AND  ( M.PRTY_CODE LIKE :SearchQuery OR M.PRTY_NAME LIKE :SearchQuery)";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        
        return data.Rows.Count;
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
            string CommandText = " SELECT * FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME ) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE VENDOR_CAT_CODE = 'TRANSPORTER & LOGISTICS' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME ) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE VENDOR_CAT_CODE = 'TRANSPORTER & LOGISTICS' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY PRTY_CODE ASC";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCountTran(string text)
    {
        string CommandText = " SELECT * FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_GRP_CODE = 'Transporter' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY PRTY_CODE ASC ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void ddlGateNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlGateNumber.Items.Clear();
            DataTable data = GetGatePassData(e.Text.ToUpper(), e.ItemsOffset, 10);
            ddlGateNumber.DataSource = data;
            ddlGateNumber.DataTextField = "GATE_NUMB";
            ddlGateNumber.DataValueField = "GATE_NUMB";
            ddlGateNumber.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetGatePassDataCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    
    protected void ddlGateNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int gate_NUMBER = int.Parse(ddlGateNumber.SelectedValue.Trim());


            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            dtDetailTBL.Rows.Clear();
            int iRecordFound = GetdataByGateNumber(gate_NUMBER);
          
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Gate Pass Number.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
   
    protected DataTable GetGatePassData(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.GATE_NUMB, a.GATE_DATE, a.PRTY_CODE, a.PRTY_NAME FROM V_TX_GATE_PASS_MST a WHERE NVL (A.STATUS, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.GATE_TYPE = '" + ddlGateType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.GATE_NUMB DESC, a.GATE_DATE DESC) asd WHERE GATE_NUMB LIKE :searchQuery OR GATE_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND (GATE_NUMB, GATE_DATE, PRTY_CODE, PRTY_NAME) NOT IN(SELECT GATE_NUMB,GATE_DATE,PRTY_CODE,PRTY_NAME FROM (SELECT * FROM (SELECT a.GATE_NUMB,a.GATE_DATE,a.PRTY_CODE,a.PRTY_NAME FROM V_TX_GATE_PASS_MST a WHERE NVL (A.STATUS, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.GATE_TYPE = '" + ddlGateType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.GATE_NUMB DESC, a.GATE_DATE DESC) asd WHERE GATE_NUMB LIKE :searchQuery OR GATE_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= '" + startOffset + "') ";
            }

            string SortExpression = " ORDER BY GATE_NUMB DESC, GATE_DATE DESC";
            string SearchQuery = text + "%";
           return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            
        }
        catch
        {
            throw;
        }
    }

    protected int GetGatePassDataCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.GATE_NUMB, a.GATE_DATE, a.PRTY_CODE, a.PRTY_NAME FROM V_TX_GATE_PASS_MST a WHERE NVL (A.STATUS, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.GATE_TYPE = '" + ddlGateType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.GATE_NUMB DESC, a.GATE_DATE DESC) asd WHERE GATE_NUMB LIKE :searchQuery OR GATE_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) ";
            string whereClause = string.Empty;
            string SortExpression = " ";
            string SearchQuery = text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "").Rows.Count;;
           
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

            string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT *           FROM   (      SELECT   M.TRN_NUMB,   Y.YARN_CODE,         O.PARTY_ARTICLE_DESC YARN_DESC,   O.SHADE_CODE,  T.LOT_NO,   T.NO_OF_UNIT,  T.TRN_QTY,  O.ORDER_NO,  (   M.TRN_NUMB       || '@' || Y.YARN_CODE   || '@' || O.PARTY_ARTICLE_DESC  || '@' || O.SHADE_CODE     || '@'     || T.LOT_NO|| '@'  || T.CARTONS       || '@' || T.TRN_QTY || '@' || T.GRADE    || '@'   || M.TRN_DATE       || '@'   || M.BILL_NUMB)   TRN_DATA FROM   YRN_MST Y,     YRN_ASSOCATED_MST YA,  YRN_IR_MST M,      YRN_IR_TRN T,         OD_CUSTOMER_REQUEST_ST O WHERE       Y.YARN_CODE = YA.YARN_CODE         AND M.COMP_CODE = T.COMP_CODE         AND M.BRANCH_CODE = T.BRANCH_CODE         AND M.YEAR = T.YEAR         AND M.TRN_NUMB = T.TRN_NUMB         AND M.TRN_TYPE = T.TRN_TYPE         AND T.YARN_CODE = Y.YARN_CODE         AND O.ARTICLE_NO = YA.ASS_YARN_CODE         AND T.SHADE_CODE = O.SHADE_CODE         AND O.ORDER_NO = T.PI_NO         AND NVL (M.GATE_NUMB, 0) =0               AND M.PRTY_CODE IN ('" + lblPartyCode.Text + "')         AND       (M.TRN_NUMB LIKE :SearchQuery                       OR T.YARN_CODE LIKE :SearchQuery                       OR  O.ARTICLE_NO LIKE :SearchQuery                     OR O.PARTY_ARTICLE_DESC LIKE :SearchQuery                       OR O.SHADE_CODE LIKE :SearchQuery OR O.ORDER_NO LIKE :SearchQuery)      ) asd       ORDER BY   TRN_DATA ASC) WHERE   ROWNUM < 15";

             //AND NVL(M.BILL_NUMB,0)= 0
                string whereClause = string.Empty;
                if (startoffset != 0)
                {
                    whereClause += " AND (TRN_NUMB,  YARN_CODE,SHADE_CODE,LOT_NO) NOT IN   ( SELECT TRN_NUMB,  YARN_CODE,SHADE_CODE,LOT_NO FROM ( SELECT   DISTINCT *           FROM   (      SELECT   M.TRN_NUMB,   Y.YARN_CODE,         O.PARTY_ARTICLE_DESC YARN_DESC,   O.SHADE_CODE,  T.LOT_NO,   T.NO_OF_UNIT,  T.TRN_QTY,  O.ORDER_NO,  (   M.TRN_NUMB       || '@' || Y.YARN_CODE   || '@' || O.PARTY_ARTICLE_DESC  || '@' || O.SHADE_CODE     || '@'     || T.LOT_NO|| '@'  || T.CARTONS      || '@' || T.TRN_QTY || '@' || T.GRADE    || '@'   || M.TRN_DATE       || '@'   || M.BILL_NUMB)   TRN_DATA FROM   YRN_MST Y,     YRN_ASSOCATED_MST YA,  YRN_IR_MST M,      YRN_IR_TRN T,         OD_CUSTOMER_REQUEST_ST O WHERE       Y.YARN_CODE = YA.YARN_CODE         AND M.COMP_CODE = T.COMP_CODE         AND M.BRANCH_CODE = T.BRANCH_CODE         AND M.YEAR = T.YEAR         AND M.TRN_NUMB = T.TRN_NUMB         AND M.TRN_TYPE = T.TRN_TYPE         AND T.YARN_CODE = Y.YARN_CODE         AND O.ARTICLE_NO = YA.ASS_YARN_CODE         AND T.SHADE_CODE = O.SHADE_CODE         AND O.ORDER_NO = T.PI_NO          AND NVL (M.GATE_NUMB, 0) =0                 AND M.PRTY_CODE IN ('" + lblPartyCode.Text + "')         AND       (M.TRN_NUMB LIKE :SearchQuery                       OR T.YARN_CODE LIKE :SearchQuery                       OR  O.ARTICLE_NO LIKE :SearchQuery                     OR O.PARTY_ARTICLE_DESC LIKE :SearchQuery                       OR O.SHADE_CODE LIKE :SearchQuery OR O.ORDER_NO LIKE :SearchQuery)      ) asd       ORDER BY   TRN_DATA ASC )  WHERE ROWNUM < '" + startoffset + "' ) ";
                     //AND NVL(M.BILL_NUMB,0)=0

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
            string CommandText = " SELECT   M.TRN_NUMB,   Y.YARN_CODE,         O.PARTY_ARTICLE_DESC YARN_DESC,   O.SHADE_CODE,  T.LOT_NO,   T.NO_OF_UNIT,  T.TRN_QTY,  O.ORDER_NO,  (   M.TRN_NUMB       || '@' || Y.YARN_CODE   || '@' || O.PARTY_ARTICLE_DESC  || '@' || O.SHADE_CODE     || '@'     || T.LOT_NO|| '@'  || T.NO_OF_UNIT      || '@' || T.TRN_QTY || '@' || T.GRADE    || '@'   || M.TRN_DATE       || '@'   || M.BILL_NUMB)   TRN_DATA FROM   YRN_MST Y,     YRN_ASSOCATED_MST YA,  YRN_IR_MST M,      YRN_IR_TRN T,         OD_CUSTOMER_REQUEST_ST O WHERE       Y.YARN_CODE = YA.YARN_CODE         AND M.COMP_CODE = T.COMP_CODE         AND M.BRANCH_CODE = T.BRANCH_CODE         AND M.YEAR = T.YEAR         AND M.TRN_NUMB = T.TRN_NUMB         AND M.TRN_TYPE = T.TRN_TYPE         AND T.YARN_CODE = Y.YARN_CODE         AND O.ARTICLE_NO = YA.ASS_YARN_CODE         AND T.SHADE_CODE = O.SHADE_CODE         AND O.ORDER_NO = T.PI_NO           AND NVL (M.GATE_NUMB, 0) =0     AND NVL(M.BILL_NUMB,0)<> 0             AND M.PRTY_CODE IN ('" + lblPartyCode.Text + "')         AND       (M.TRN_NUMB LIKE :SearchQuery                       OR T.YARN_CODE LIKE :SearchQuery                       OR  O.ARTICLE_NO LIKE :SearchQuery                     OR O.PARTY_ARTICLE_DESC LIKE :SearchQuery                       OR O.SHADE_CODE LIKE :SearchQuery OR O.ORDER_NO LIKE :SearchQuery)       ";
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
                txtGrade.Text = ss[7].ToString();
                txtDCDate.Text = ss[8].ToString();
                txtInvoiceNumb.Text = ss[9].ToString();              
                btnsaveTRNDetail.Focus();
                if (Session["dtDetailTBL"] != null)
                {
                    dtDetailTBL = (DataTable)Session["dtDetailTBL"];
                }
                else
                {
                    dtDetailTBL = CreateDataTable();
                }

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetDODetailsForGatePass(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, int.Parse(ss[0].ToString()));
                MapDataTable();                
                BindGridFromDataTable();
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
           
                if (txtYarnDesc.Text != "" && txtQty.Text != "")
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
                                    Int64 GATE_NUMB = 0;
                                    Int64.TryParse(txtGateNumber.Text, out GATE_NUMB);
                                    DateTime DOCDATE = DateTime.Now;
                                    DateTime.TryParse(txtDCDate.Text, out DOCDATE);
                                    dv[0]["COMP_CODE"] = oUserLoginDetail.COMP_CODE ;
                                    dv[0]["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE ;
                                    dv[0]["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year ;
                                    dv[0]["GATE_NUMB"] = GATE_NUMB;
                                    dv[0]["GATE_TYPE"] =ddlGateType.SelectedValue ;
                                    dv[0]["YARN_CODE"] = lblYarnCode.Text ;
                                    dv[0]["YARN_DESC"] = txtYarnDesc.Text ;
                                    dv[0]["SHADE_CODE"] = txtShade.Text.Trim();
                                    dv[0]["LOT_NO"] = txtLotNo.Text ;
                                    dv[0]["GRADE"] = txtGrade.Text;
                                    dv[0]["DOC_NO"] = txtDCNo.Text ;
                                    dv[0]["DOC_DATE"] = DOCDATE;
                                    dv[0]["NO_OF_UNIT"] = NoOfUnit ;
                                    dv[0]["QUANTITY"] = Qty;
                                    dv[0]["INVOICE_NUMB"] = txtInvoiceNumb.Text ;
                                    dv[0]["TUSER"] = oUserLoginDetail.UserCode;
                                    dv[0]["TDATE"] = DateTime.Now ;
                                    dv[0]["STATUS"] = "0";
                                  
                                }
                            }
                            else
                            {
                                DataRow dr = dtDetailTBL.NewRow();
                                dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                double NoOfUnit = 0;
                                double.TryParse(txtNoOfUnit.Text, out NoOfUnit);                              
                                Int64 GATE_NUMB = 0;
                                Int64.TryParse(txtGateNumber.Text, out GATE_NUMB);
                                DateTime DOCDATE = DateTime.Now;
                                DateTime.TryParse(txtDCDate.Text, out DOCDATE);
                                dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                                dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dr["GATE_NUMB"] = GATE_NUMB;
                                dr["GATE_TYPE"] = ddlGateType.SelectedValue;
                                dr["YARN_CODE"] = lblYarnCode.Text;
                                dr["YARN_DESC"] = txtYarnDesc.Text;
                                dr["SHADE_CODE"] = txtShade.Text.Trim();
                                dr["LOT_NO"] = txtLotNo.Text;
                                dr["GRADE"] = txtGrade.Text;
                                dr["DOC_NO"] = txtDCNo.Text;
                                dr["DOC_DATE"] = DOCDATE;
                                dr["NO_OF_UNIT"] = NoOfUnit;
                                dr["QUANTITY"] = Qty;
                                dr["INVOICE_NUMB"] = txtInvoiceNumb.Text;
                                dr["TUSER"] = oUserLoginDetail.UserCode;
                                dr["TDATE"] = DateTime.Now;
                                dr["STATUS"] = "0";
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
                grdGatePass.DataSource = dtDetailTBL;
                grdGatePass.DataBind();
               
           
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
            string Shade = txtShade.Text;
            foreach (GridViewRow grdRow in grdGatePass.Rows)
            {
                Label txtICODE = (Label)grdRow.FindControl("lblYARN_CODE");
                Label txtSHADE_CODE = (Label)grdRow.FindControl("txtSHADE_CODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (txtICODE.ToolTip.Trim() == yarnCode && UniqueId != iUniqueId && Shade == txtSHADE_CODE.Text)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void grdGatePass_RowCommand(object sender, GridViewCommandEventArgs e)
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
                lblYarnCode.Text= dv[0]["YARN_CODE"].ToString() ;
                txtYarnDesc.Text=dv[0]["YARN_DESC"].ToString() ;
                txtShade.Text=dv[0]["SHADE_CODE"].ToString();
                txtLotNo.Text=dv[0]["LOT_NO"].ToString();
                txtGrade.Text = dv[0]["GRADE"].ToString();
                txtDCNo.Text=dv[0]["DOC_NO"].ToString();
                txtDCDate.Text = dv[0]["DOC_DATE"].ToString();
                txtNoOfUnit.Text=dv[0]["NO_OF_UNIT"].ToString();
                txtQty.Text = dv[0]["QUANTITY"].ToString();
                txtInvoiceNumb.Text= dv[0]["INVOICE_NUMB"].ToString();               
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

    private int GetdataByGateNumber(int GateNumber)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_GATE_PASS_MST.GetDataByGATE_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, GateNumber, ddlGateType.SelectedValue, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtGateNumber.Text = GateNumber.ToString();
                ddlGateType.SelectedValue  = dt.Rows[0]["GATE_TYPE"].ToString().Trim();
                txtGateDate.Text = DateTime.Parse(dt.Rows[0]["GATE_DATE"].ToString().Trim()).ToShortDateString();                
                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();              
                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TRSP_NAME"].ToString().Trim();             
                txtVehicleNo.Text = dt.Rows[0]["VEHICLE_NO"].ToString().Trim();  
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString().Trim();
               
                 //PRODUCT_TYPE,                            
                  //PRODUCT_DETAILS,    
                  //TOTAL_AMOUNT,
                 //FINAL_AMOUNT,

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_GATE_PASS_MST.GetTRN_DataByGATE_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, GateNumber, ddlGateType.SelectedValue);
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
            if (!dtDetailTBL.Columns.Contains("GATE_NUMB"))
            {
                dtDetailTBL.Columns.Add("GATE_NUMB", typeof(int));
            }
            if (!dtDetailTBL.Columns.Contains("GATE_TYPE"))
            {
                dtDetailTBL.Columns.Add("GATE_TYPE", typeof(string));
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
                dtDetailTBL.Rows[iLoop]["GATE_NUMB"] = int.Parse(txtGateNumber.Text);
                dtDetailTBL.Rows[iLoop]["GATE_TYPE"] = ddlGateType.SelectedValue;
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
            Response.Redirect("~/Module/GateEntry/Reports/GatePassPrintReport.aspx", true);
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

            if (txtGateNumber.Text != string.Empty)
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
            SaitexDM.Common.DataModel.TX_GATE_PASS_MST oTX_GATE_PASS_MST = new SaitexDM.Common.DataModel.TX_GATE_PASS_MST();

            oTX_GATE_PASS_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_GATE_PASS_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_GATE_PASS_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_GATE_PASS_MST.GATE_NUMB = int.Parse(CommonFuction.funFixQuotes(txtGateNumber.Text.Trim()));
            oTX_GATE_PASS_MST.GATE_TYPE = CommonFuction.funFixQuotes(ddlGateType.SelectedValue);           
            DateTime  Invdt = System.DateTime.Now.Date;          
            DateTime.TryParse(txtGateDate.Text.Trim(), out Invdt);         
            oTX_GATE_PASS_MST.GATE_DATE = Invdt;            
            oTX_GATE_PASS_MST.PRTY_CODE = lblPartyCode.Text.Trim();            
            oTX_GATE_PASS_MST.TRSP_CODE = lblTransporterCode.Text.Trim();        
            oTX_GATE_PASS_MST.VEHICLE_NO = txtVehicleNo.Text ;          
            oTX_GATE_PASS_MST.TDATE = DateTime.Now ;
            oTX_GATE_PASS_MST.TUSER = oUserLoginDetail.UserCode;  
            oTX_GATE_PASS_MST.STATUS ="0";
            oTX_GATE_PASS_MST.PRODUCT_TYPE = "YARN";
            oTX_GATE_PASS_MST.PRODUCT_DETAILS = "YARN";                             
            oTX_GATE_PASS_MST.REMARKS=txtRemarks.Text ;          
                                                       
                               

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            int GATE_PASS_NUMB = 0;
            bool result = SaitexBL.Interface.Method.TX_GATE_PASS_MST.Insert(oTX_GATE_PASS_MST, out GATE_PASS_NUMB, dtDetailTBL);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"Gate Pass Number : " + GATE_PASS_NUMB + " saved successfully.");

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
            SaitexDM.Common.DataModel.TX_GATE_PASS_MST oTX_GATE_PASS_MST = new SaitexDM.Common.DataModel.TX_GATE_PASS_MST();


            oTX_GATE_PASS_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_GATE_PASS_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_GATE_PASS_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_GATE_PASS_MST.GATE_NUMB = int.Parse(CommonFuction.funFixQuotes(txtGateNumber.Text.Trim()));
            oTX_GATE_PASS_MST.GATE_TYPE = CommonFuction.funFixQuotes(ddlGateType.SelectedValue);
            DateTime Invdt = System.DateTime.Now.Date;
            DateTime.TryParse(txtGateDate.Text.Trim(), out Invdt);
            oTX_GATE_PASS_MST.GATE_DATE = Invdt;
            oTX_GATE_PASS_MST.PRTY_CODE = lblPartyCode.Text.Trim();
            oTX_GATE_PASS_MST.TRSP_CODE = lblTransporterCode.Text.Trim();
            oTX_GATE_PASS_MST.VEHICLE_NO = txtVehicleNo.Text;
            oTX_GATE_PASS_MST.TDATE = DateTime.Now;
            oTX_GATE_PASS_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_GATE_PASS_MST.STATUS = "0";
            oTX_GATE_PASS_MST.PRODUCT_TYPE = "YARN";
            oTX_GATE_PASS_MST.PRODUCT_DETAILS = "YARN";   
            oTX_GATE_PASS_MST.REMARKS = txtRemarks.Text;          
                                                       
            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }           
            bool result = SaitexBL.Interface.Method.TX_GATE_PASS_MST.Update(oTX_GATE_PASS_MST,  dtDetailTBL);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"Gate Pass Number : " + oTX_GATE_PASS_MST.GATE_NUMB + " updated successfully.");

            }
            else
            {
                CommonFuction.ShowMessage("data  Updation Failed");
            }
        }
        catch
        {
            throw;
        }
    }
 
}
