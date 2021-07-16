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

public partial class Module_Sewing_Thread_Controls_StockTransferReceive : System.Web.UI.UserControl
{
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private DataTable dtTRN_SUB = null;
    private static string TRN_TYPE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            TRN_TYPE = "RYS26";

            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();

            DateTime StartDate = System.DateTime.Now;
            DateTime EndDate = System.DateTime.Now;
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }

            rvlr.MinimumValue = StartDate.ToShortDateString();
            rvlr.MaximumValue = EndDate.ToShortDateString();

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();

            ViewState["dtTRN_SUB"] = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindNewMRNNum()
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.TRN_TYPE = TRN_TYPE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            txtTRNNUMBer.Text = SaitexBL.Interface.Method.YRN_IR_MST.GetNewTRNNumber(oYRN_IR_MST).ToString();
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

            txtGateEntryDate.Text = "";
            txtLRDate.Text = "";
            txtLRNo.Text = "";
            txtMRNDate.Text = "";
            txtTRNNUMBer.Text = "";
            txtPartyChallanDate.Text = "";
            txtPartyChallanNo.Text = "";
            ddlGateEntryNo.SelectedIndex = -1;
            txtGateEntryNo.Text = "";
            txtRemarks.Text = "";
            txtTransporterAddress.Text = "";
            txtTransporterCode.SelectedIndex = -1;
            txtTransporter.Text = "";
            txtVehicleNo.Text = "";

            txtIssueBranch.Text = string.Empty;
            txtIssueCompany.Text = string.Empty;
            txtIssueTRNNumb.Text = string.Empty;
            txtIssueTRNType.Text = string.Empty;
            txtIssueYear.Text = string.Empty;
            ddlIssueDetail.SelectedIndex = -1;

            ddlReceiptShift.SelectedIndex = 0;

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();

            grdMaterialItemReceipt.DataSource = null;
            grdMaterialItemReceipt.DataBind();

            lblMode.Text = "Save";

            txtTRNNUMBer.ReadOnly = true;

            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            }

            tdSave.Visible = true;
            tdUpdate.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("TRNNUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            
            ViewState["dtDetailTBL"] = dtDetailTBL;

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
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
            {
                CreateDataTable();
            }

            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();

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

    private void deleteItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUEID"].ToString());
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
                    dr["UNIQUEID"] = iCount;
                }
            }

            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void ddlIssueDetail_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOVForISSUE(e.Text.ToUpper(), e.ItemsOffset);
            ddlIssueDetail.Items.Clear();

            ddlIssueDetail.DataSource = data;
            ddlIssueDetail.DataTextField = "TRN_NUMB";
            ddlIssueDetail.DataValueField = "ISS_DATA";
            ddlIssueDetail.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in party selection. see error log for detail.");
        }
    }

    private DataTable GetLOVForISSUE(string Text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( YEAR|| '@'|| COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| TRN_TYPE|| '@'|| TRN_NUMB)ISS_DATA, YEAR, COMP_CODE, BRANCH_CODE, TRN_TYPE, TRN_NUMB, REC_BRANCH_CODE FROM YRN_IR_MST Y WHERE REC_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND NVL (CONF_FLAG, 0) <> 0 AND NVL (ST_TRN_NUMB, 0) = 0 ORDER BY year DESC, comp_code ASC, branch_code ASC, trn_type ASC, trn_numb ASC) WHERE BRANCH_CODE LIKE :SearchQuery OR TRN_NUMB LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "   AND ISS_DATA NOT IN (SELECT ISS_DATA FROM (SELECT * FROM (SELECT ( YEAR || '@' || COMP_CODE || '@' || BRANCH_CODE || '@' || TRN_TYPE || '@' || TRN_NUMB) ISS_DATA,YEAR,COMP_CODE,BRANCH_CODE,TRN_TYPE,TRN_NUMB,REC_BRANCH_CODE FROM YRN_IR_MST Y WHERE REC_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND NVL (CONF_FLAG, 0) <> 0 AND NVL (ST_TRN_NUMB, 0) = 0 ORDER BY year DESC,comp_code ASC,branch_code ASC,trn_type ASC,trn_numb ASC) WHERE BRANCH_CODE LIKE :SearchQuery OR TRN_NUMB LIKE :SearchQuery)WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY year DESC,comp_code ASC,branch_code ASC,trn_type ASC,trn_numb ASC";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetIssueCount(string text)
    {


        string CommandText = " SELECT * FROM (SELECT * FROM (SELECT ( YEAR|| '@'|| COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| TRN_TYPE|| '@'|| TRN_NUMB)ISS_DATA, YEAR, COMP_CODE, BRANCH_CODE, TRN_TYPE, TRN_NUMB, REC_BRANCH_CODE FROM YRN_IR_MST Y WHERE REC_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY year DESC, comp_code ASC, branch_code ASC, trn_type ASC, trn_numb ASC) WHERE BRANCH_CODE LIKE :SearchQuery OR TRN_NUMB LIKE :SearchQuery) ";
        string WhereClause = "  ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlIssueDetail_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            ResetDetailOnIssueSelection();

            string Iss_Data = ddlIssueDetail.SelectedValue.Trim();
            string[] Iss_DataAll = Iss_Data.Split('@');

            txtIssueYear.Text = Iss_DataAll[0].ToString();
            txtIssueCompany.Text = Iss_DataAll[1].ToString();
            txtIssueBranch.Text = Iss_DataAll[2].ToString();
            txtIssueTRNType.Text = Iss_DataAll[3].ToString();
            txtIssueTRNNumb.Text = Iss_DataAll[4].ToString();


            dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetTRN_DataByTRN_NUMBst(int.Parse(txtIssueYear.Text), txtIssueCompany.Text, txtIssueBranch.Text, int.Parse(txtIssueTRNNumb.Text), txtIssueTRNType.Text);
            if (dtDetailTBL.Rows.Count > 0)
            {
                ViewState["dtDetailTBL"] = dtDetailTBL;
                MapDataTable();
                MapTRNSUBDataTable();
                BindGridFromDataTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(@"Problem in Stock transfer Issue.\r\nsee error log for detail.");
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable CreateSUBTRNTable()
    {
        try
        {
            DataTable dtTRN_SUB = new DataTable();
            dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_SUB.Columns.Add("YARN_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtTRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtTRN_SUB.Columns.Add("GRADE", typeof(string));
            dtTRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtTRN_SUB.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtTRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtTRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtTRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtTRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("PI_NO", typeof(string));
            dtTRN_SUB.Columns.Add("SHADE_CODE", typeof(string)); // Added By Rajesh 20 Nov
            return dtTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void MapTRNSUBDataTable()
    {
        try
        {
            if (ViewState["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)ViewState["dtTRN_SUB"];
            }

            if (dtTRN_SUB == null)
            {
                dtTRN_SUB = CreateSUBTRNTable();
            }

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {

            }
            foreach (DataRow dr in dtDetailTBL.Rows)
            {
                DataRow drSub = dtTRN_SUB.NewRow();
                drSub["UNIQUE_ID"] = dtTRN_SUB.Rows.Count + 1;
                drSub["YARN_CODE"] = dr["YARN_CODE"];
                drSub["TRN_QTY"] = dr["TRN_QTY"];
                drSub["MATERIAL_STATUS"] = "UnCheck";
                drSub["PO_COMP_CODE"] = dr["PO_COMP_CODE"];
                drSub["PO_BRANCH"] = dr["PO_BRANCH"];
                drSub["PO_TYPE"] = dr["PO_TYPE"];
                drSub["PO_NUMB"] = dr["PO_NUMB"];
                drSub["GRADE"] = "NA";
                drSub["LOT_NO"] = DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString() + TRN_TYPE;
                drSub["DATE_OF_MFG"] = DateTime.Now.Date;
                drSub["NO_OF_UNIT"] = dr["NO_OF_UNIT"];
                drSub["UOM_OF_UNIT"] = dr["UOM"];
                drSub["WEIGHT_OF_UNIT"] = dr["WEIGHT_OF_UNIT"];
                drSub["SHADE_CODE"] = dr["SHADE_CODE"];

                dtTRN_SUB.Rows.Add(drSub);
            }

            ViewState["dtTRN_SUB"] = dtTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void ResetDetailOnIssueSelection()
    {
        try
        {
            ddlGateEntryNo.SelectedIndex = -1;
            ddlGateEntryNo.Items.Clear();
            txtGateEntryNo.Text = "";
            txtGateEntryDate.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtPartyChallanDate.Text = string.Empty;
            txtPartyChallanNo.Text = string.Empty;

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            dtDetailTBL.Rows.Clear();

            ViewState["dtDetailTBL"] = dtDetailTBL;
            BindGridFromDataTable();
        }
        catch
        {
            throw;
        }
    }

    protected void txtTransporterCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetTransporterData(e.Text.ToUpper(), e.ItemsOffset);

            txtTransporterCode.Items.Clear();

            txtTransporterCode.DataSource = data;
            txtTransporterCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetTransporterCount(e.Text);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Transporter selection.\r\nsee error log for detail.");
        }
    }

    private DataTable GetTransporterData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= " + startOffset + ")";
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

    protected int GetTransporterCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void txtTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtTransporter.Text = txtTransporterCode.SelectedText.ToString().Trim();
            txtTransporterAddress.Text = txtTransporterCode.SelectedValue;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Transporter selection.\r\nsee error log for detail.");
        }
    }

    protected void ddlGateEntryNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOVForGate(e.Text.ToUpper(), e.ItemsOffset);
            ddlGateEntryNo.Items.Clear();

            ddlGateEntryNo.DataSource = data;
            ddlGateEntryNo.DataTextField = "GATE_NUMB";
            ddlGateEntryNo.DataValueField = "GATE_DATA";
            ddlGateEntryNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetGateCount(e.Text);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in Gate Entry Detail selection. see error log for detail.");
        }
    }

    private DataTable GetLOVForGate(string text, int startOffset)
    {
        try
        {
            DataTable data = null;
            if (txtIssueTRNNumb.Text != "")
            {
                string CommandText = string.Empty;
                string whereClause = string.Empty;

                if (string.Compare(lblMode.Text, "You are in Save Mode", true) != 1)
                {
                    CommandText = "select * from ( SELECT * FROM ( select (a.GATE_DATE||'@'||a.LORRY_NO||'@'||a.DOC_NO||'@'||a.DOC_DATE) GATE_DATA ,A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE from v_tx_gate_mst a where a.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE IN ('SELF', 'NA') AND a.ITEM_TYPE='SEWING THREAD IN' and nvl(a.ISSUE_NUMB,0)=0) asd  WHERE GATE_NUMB LIKE :SearchQuery or GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB ) where rownum<=15";
                }
                else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
                {
                    CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE IN ( 'SELF','NA') AND a.ITEM_TYPE = 'SEWING THREAD IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15 ";
                }

                if (startOffset != 0)
                {
                    if (string.Compare(lblMode.Text, "You are in Save Mode", true) != 1)
                    {
                        whereClause = " and GATE_NUMB NOT IN( select GATE_NUMB from ( SELECT * FROM ( select (a.GATE_DATE||'@'||a.LORRY_NO||'@'||a.DOC_NO||'@'||a.DOC_DATE) GATE_DATA ,A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE from v_tx_gate_mst a where a.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE IN ('SELF','NA') AND a.ITEM_TYPE='SEWING THREAD IN' and nvl(a.ISSUE_NUMB,0)=0) asd  WHERE GATE_NUMB LIKE :SearchQuery or GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB ) where rownum<=" + startOffset + " )";
                    }
                    else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
                    {
                        whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE,A.ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE IN ('SELF','NA') AND a.ITEM_TYPE ='SEWING THREAD IN')asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0)asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB)WHERE ROWNUM <= " + startOffset + ")";
                    }
                }

                string SortExpression = " ORDER BY GATE_NUMB ";
                string SearchQuery = text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            }
            else
            {
                CommonFuction.ShowMessage("Please select Party first.");
            }
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetGateCount(string text)
    {

        DataTable data = new DataTable();
        if (txtIssueTRNNumb.Text != "")
        {
            string CommandText = "";
            if (string.Compare(lblMode.Text, "You are in Save Mode", true) != 1)
            {
                CommandText = "select * from ( SELECT * FROM ( select (a.GATE_DATE||'@'||a.LORRY_NO||'@'||a.DOC_NO||'@'||a.DOC_DATE) GATE_DATA ,A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE from v_tx_gate_mst a where a.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE='SELF' AND a.ITEM_TYPE='SEWING THREAD IN' and nvl(a.ISSUE_NUMB,0)=0) asd  WHERE GATE_NUMB LIKE :SearchQuery or GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB ) ";
            }
            else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
            {
                CommandText = "select * from ( SELECT * FROM (select (a.GATE_DATE||'@'||a.LORRY_NO||'@'||a.DOC_NO||'@'||a.DOC_DATE) GATE_DATA ,A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE from v_tx_gate_mst a where a.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE='SELF' AND a.ITEM_TYPE='SEWING THREAD IN' ) asd where nvl(ISSUE_NUMB,0)='" + txtTRNNUMBer.Text.Trim() + "' or nvl(ISSUE_NUMB,0)=0 ) asd  WHERE GATE_NUMB LIKE :SearchQuery or GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB ) ";
            }

            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        else
        {
            CommonFuction.ShowMessage("Please select Iss Numb First");
        }

        return data.Rows.Count;
    }

    protected void ddlGateEntryNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = ddlGateEntryNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            DateTime GateDate = DateTime.Parse(arrString[0].ToString());
            string VahicleNo = arrString[1].ToString();
            string PartyChallanNo = arrString[2].ToString();
            DateTime partyChallanDate = DateTime.Parse(arrString[3].ToString());

            txtGateEntryNo.Text = ddlGateEntryNo.SelectedText.Trim();
            GetDataForGateDetail(GateDate, VahicleNo, PartyChallanNo, partyChallanDate);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in gate entry selection. see error log for detail.");
        }
    }

    private void GetDataForGateDetail(DateTime GateDate, string VahicleNo, string PartyChallanNo, DateTime partyChallanDate)
    {
        try
        {
            txtGateEntryDate.Text = GateDate.ToShortDateString();
            txtVehicleNo.Text = VahicleNo;
            txtPartyChallanNo.Text = PartyChallanNo;
            txtPartyChallanDate.Text = partyChallanDate.ToShortDateString();
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
            txtTRNNUMBer.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();

            txtTRNNUMBer.Visible = false;
            ddlTRNNumber.Visible = true;

            tdSave.Visible = false;
            tdUpdate.Visible = true;
            lblMode.Text = "Update";
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

            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "Save";
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
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                saveMaterialReceipt();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

            CommonFuction.ShowMessage(@"Problem in saving Data.\r\nSee error log for detail.");
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            bool ReturnResult = false;

            int count = 0;
            int iCountAll = 0;
            msg = string.Empty;

            iCountAll += 1;
            if (txtTRNNUMBer.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. MRN Number required.\r\n";
            }

            iCountAll += 1;
            if (txtGateEntryNo.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Gate Entry Details first.\r\n";
            }

            iCountAll += 1;
            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
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

    private void saveMaterialReceipt()
    {
        try
        {
            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = string.Empty;
            oYRN_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oYRN_IR_MST.GATE_DATE = dt;

            oYRN_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = "NA";
            oYRN_IR_MST.PRTY_NAME = "NA";
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oYRN_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);

            if (txtTransporter.Text == "")
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporter.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;

            oYRN_IR_MST.ST_YEAR = int.Parse(txtIssueYear.Text);
            oYRN_IR_MST.ST_COMP_CODE = txtIssueCompany.Text;
            oYRN_IR_MST.ST_BRANCH_CODE = txtIssueBranch.Text;
            oYRN_IR_MST.ST_TRN_TYPE = txtIssueTRNType.Text;
            oYRN_IR_MST.ST_TRN_NUMB = int.Parse(txtIssueTRNNumb.Text);

            int TRN_NUMB = 0;

            //if (ViewState["dtDetailTBL"] != null)
            //{
            //    double dblQtyActual = 0;
            //    double dblQtyNew = 0;

            //    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            //    int totalRows = grdMaterialItemReceipt.Rows.Count;
            //    for (int r = 0; r < totalRows; r++)
            //    {
            //        if (grdMaterialItemReceipt.RowType == DataControlRowType.DataRow)
            //        {
            //            Label txtUNIQUEID = (Label)grdMaterialItemReceipt.FindControl("txtUNIQUEID");
            //            Label txtQTY = (Label)grdMaterialItemReceipt.FindControl("txtQTY");
            //            TextBox txtQTY1 = (TextBox)grdMaterialItemReceipt.FindControl("txtQTY1");

            //            double.TryParse(txtQTY.Text.Trim(), out dblQtyActual);
            //            double.TryParse(txtQTY1.Text.Trim(), out dblQtyNew);
            //            if (dblQtyNew < dblQtyActual)
            //            {
            //                DataView dvEdit = new DataView(dtDetailTBL);
            //                dvEdit.RowFilter = "UNIQUEID='" + txtUNIQUEID.Text + "'";
            //                if (dvEdit.Count > 0)
            //                {
            //                    dvEdit[0]["TRN_QTY"] = txtQTY1.Text.Trim();
            //                    dtDetailTBL.AcceptChanges();
            //                }
            //            }
            //        }
            //    }
            //}

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            DataTable dtTRN_SUB = new DataTable();
            if (ViewState["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)ViewState["dtTRN_SUB"];
            }

            if (dtTRN_SUB == null)
                dtTRN_SUB = CreateSUBTRNTable();

            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, null, null);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"MRN Number : " + TRN_NUMB + " saved successfully.");
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

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ActivateUpdateMode();
            lblMode.Text = "Update";
            txtTRNNUMBer.Text = "";
            tdUpdate.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in loading update mode.\r\nsee error log for detail.");
        }
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in loading Indent for updation. see error log for detail");
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd WHERE TRN_NUMB LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC) WHERE ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND trn_numb NOT IN(SELECT TRN_NUMB FROM (SELECT * FROM (SELECT a.TRN_NUMB,a.TRN_DATE,a.PRTY_CODE,b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+)AND A.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'AND A.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "'AND A.TRN_TYPE ='" + TRN_TYPE + "'AND YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "') asd WHERE TRN_NUMB LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC)WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY   TRN_NUMB DESC";
            string SearchQuery = text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
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

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            dtDetailTBL.Rows.Clear();
            ViewState["dtDetailTBL"] = dtDetailTBL;
            int iRecordFound = GetdataByTRNNUMBer(TRN_NUMBER);

            BindGridFromDataTable();
            if (iRecordFound > 0)
            {

            }
            else
            {
                InitialisePage();
                ActivateUpdateMode();
            }
        }
        catch (Exception Ex)
        {
            errorLog.ErrHandler.WriteError(Ex.Message);
        }
    }

    private int GetdataByTRNNUMBer(int TRNNUMBer)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;

                DateTime dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["GATE_DATE"].ToString().Trim(), out dd))
                    txtGateEntryDate.Text = dd.ToShortDateString();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["LR_DATE"].ToString().Trim(), out dd))
                    txtLRDate.Text = dd.ToShortDateString();

                txtLRNo.Text = dt.Rows[0]["LR_NUMB"].ToString().Trim();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["TRN_DATE"].ToString().Trim(), out dd))
                    txtMRNDate.Text = dd.ToShortDateString();

                txtTRNNUMBer.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim(), out dd))
                    txtPartyChallanDate.Text = dd.ToShortDateString();

                txtPartyChallanNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                txtTransporter.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();

                txtGateEntryNo.Text = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

                txtTransporter.Text = dt.Rows[0]["TRSP_CODE"].ToString();
                txtTransporterAddress.Text = dt.Rows[0]["TAddress"].ToString().Trim();
                txtGateEntryNo.Text = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

                txtIssueYear.Text = dt.Rows[0]["ST_YEAR"].ToString();
                txtIssueCompany.Text = dt.Rows[0]["ST_COMP_CODE"].ToString().Trim();
                txtIssueBranch.Text = dt.Rows[0]["ST_BRANCH_CODE"].ToString().Trim();
                txtIssueTRNType.Text = dt.Rows[0]["ST_TRN_TYPE"].ToString();
                txtIssueTRNNumb.Text = dt.Rows[0]["ST_TRN_NUMB"].ToString().Trim();

            }
            if (iRecordFound == 1)
            {
                if (ViewState["dtDetailTBL"] != null)
                {
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                }

                if (dtDetailTBL == null)
                    CreateDataTable();

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetTRN_DataByTRN_NUMBst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtDetailTBL.Rows.Count > 0)
                {
                    ViewState["dtDetailTBL"] = dtDetailTBL;
                    MapDataTable();
                    MapTRNSUBDataTable();
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Yarn Detail";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();

                    txtTRNNUMBer.Text = "";
                    ddlTRNNumber.Focus();

                    lblMode.Text = "Update";

                    ActivateUpdateMode();
                }
            }
            else
            {

                string msg = "Dear " + oUserLoginDetail.Username + " !! MRN already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
            return iRecordFound;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            return iRecordFound;
        }
    }

    private void MapDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));

            if (!dtDetailTBL.Columns.Contains("MAC_CODE"))
                dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));

            if (!dtDetailTBL.Columns.Contains("PI_NO"))
                dtDetailTBL.Columns.Add("PI_NO", typeof(string));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["MAC_CODE"] = string.Empty;
                dtDetailTBL.Rows[iLoop]["PI_NO"] = string.Empty;
            }

            ViewState["dtDetailTBL"] = dtDetailTBL;
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
                UpdateMaterialReceipt();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

            CommonFuction.ShowMessage(@"Problem in updating Data.\r\nSee error log for detail.");
        }
    }

    private void UpdateMaterialReceipt()
    {
        try
        {
            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = string.Empty;
            oYRN_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oYRN_IR_MST.GATE_DATE = dt;

            oYRN_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = "NA";
            oYRN_IR_MST.PRTY_NAME = "NA";
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oYRN_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);

            if (txtTransporter.Text == "")
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporter.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;

            oYRN_IR_MST.ST_YEAR = int.Parse(txtIssueYear.Text);
            oYRN_IR_MST.ST_COMP_CODE = txtIssueCompany.Text;
            oYRN_IR_MST.ST_BRANCH_CODE = txtIssueBranch.Text;
            oYRN_IR_MST.ST_TRN_TYPE = txtIssueTRNType.Text;
            oYRN_IR_MST.ST_TRN_NUMB = int.Parse(txtIssueTRNNumb.Text);

            //if (ViewState["dtDetailTBL"] != null)
            //{
            //    double dblQtyActual = 0;
            //    double dblQtyNew = 0;

            //    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            //    int totalRows = grdMaterialItemReceipt.Rows.Count;
            //    for (int r = 0; r < totalRows; r++)
            //    {
            //        if (grdMaterialItemReceipt.RowType == DataControlRowType.DataRow)
            //        {
            //            Label txtUNIQUEID = (Label)grdMaterialItemReceipt.FindControl("txtUNIQUEID");
            //            Label txtQTY = (Label)grdMaterialItemReceipt.FindControl("txtQTY");
            //            TextBox txtQTY1 = (TextBox)grdMaterialItemReceipt.FindControl("txtQTY1");

            //            double.TryParse(txtQTY.Text.Trim(), out dblQtyActual);
            //            double.TryParse(txtQTY1.Text.Trim(), out dblQtyNew);
            //            if (dblQtyNew < dblQtyActual)
            //            {
            //                DataView dvEdit = new DataView(dtDetailTBL);
            //                dvEdit.RowFilter = "UNIQUEID='" + txtUNIQUEID.Text + "'";
            //                if (dvEdit.Count > 0)
            //                {
            //                    dvEdit[0]["TRN_QTY"] = txtQTY1.Text.Trim();
            //                    dtDetailTBL.AcceptChanges();
            //                }
            //            }
            //        }
            //    }
            //}

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            DataTable dtTRN_SUB = new DataTable();
            if (ViewState["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)ViewState["dtTRN_SUB"];
            }

            if (dtTRN_SUB == null)
                dtTRN_SUB = CreateSUBTRNTable();

            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Update(oYRN_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB, null, null);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Data updated Successfully");
            }
            else
            {
                CommonFuction.ShowMessage("data updation Failed");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

            CommonFuction.ShowMessage(@"Problem in clear page data.\r\nSee error log for detail.");
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Yarn/SalesWork/Reports/YRN_ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

            CommonFuction.ShowMessage(@"Problem in get print.\r\nSee error log for detail.");
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void grdMaterialItemReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkunige = (LinkButton)e.Row.FindControl("lnkunige");
                int UNIQUE_ID = int.Parse(lnkunige.CommandArgument);
                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtDetailTBL);
                    dv.RowFilter = "UNIQUEID=" + UNIQUE_ID;
                    if (dv.Count > 0)
                    {
                        string YARN_CODE = dv[0]["YARN_CODE"].ToString();
                        if (ViewState["dtTRN_SUB"] != null)
                        {
                            DataTable dtTRN_Sub = (DataTable)ViewState["dtTRN_SUB"];
                            DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
                            dvYRNDRecieve_trn.RowFilter = "YARN_CODE='" + YARN_CODE + "'";
                            if (dvYRNDRecieve_trn.Count > 0)
                            {
                                GridView grdBOM = e.Row.FindControl("grdBOM") as GridView;
                                grdBOM.DataSource = dvYRNDRecieve_trn;
                                grdBOM.DataBind();

                                double dblTotalQty = 0;
                                double dblTotalQty1 = 0;
                                foreach (GridViewRow subgrd in grdBOM.Rows)
                                {
                                    TextBox txtNoOfUnit1 = (TextBox)subgrd.FindControl("txtNoOfUnit1");
                                    Label lblgrdW_SIDE = (Label)subgrd.FindControl("lblgrdW_SIDE");

                                    dblTotalQty += Convert.ToDouble(txtNoOfUnit1.Text);
                                    dblTotalQty1 += Convert.ToDouble(lblgrdW_SIDE.Text);
                                }
                                Label txtTotalNoOfUnits = (Label)grdBOM.FooterRow.FindControl("txtTotalNoOfUnits");
                                txtTotalNoOfUnits.Text = dblTotalQty.ToString();
                                Label txtTotalQty = (Label)grdBOM.FooterRow.FindControl("txtTotalQty");
                                txtTotalQty.Text = dblTotalQty1.ToString();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Data Bound..\r\nSee error log for detail."));
        }
    }

    protected void txtNoOfUnit1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)ViewState["dtTRN_SUB"];
            }

            foreach (GridViewRow thisGridViewRow in grdMaterialItemReceipt.Rows)
            {
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    double dblTotalQty = 0;
                    double dblTotalQty1 = 0;
                    double dblAct = 0;
                    GridView grdBOM = (GridView)thisGridViewRow.FindControl("grdBOM");
                    foreach (GridViewRow subgrd in grdBOM.Rows)
                    {
                        if (subgrd.RowType == DataControlRowType.DataRow)
                        {
                            TextBox txtNoOfUnit1 = (TextBox)subgrd.FindControl("txtNoOfUnit1");
                            Label lblgrdW_SIDE = (Label)subgrd.FindControl("lblgrdW_SIDE");
                            Label txtWtOfUnit = (Label)subgrd.FindControl("txtWtOfUnit");
                            Label txtSrNo = (Label)subgrd.FindControl("txtSrNo");


                            if (dtTRN_SUB.Rows.Count > 0)
                            {
                                DataView dvSubTrnEdit = new DataView(dtTRN_SUB);
                                dvSubTrnEdit.RowFilter = "UNIQUE_ID = '" + txtSrNo.Text + "'";
                                if (dvSubTrnEdit.Count > 0)
                                {
                                    dblAct = Convert.ToDouble(dvSubTrnEdit[0]["TRN_QTY"].ToString());
                                }
                            }
                            if (Convert.ToDouble(txtNoOfUnit1.Text) > dblAct)
                            {
                                CommonFuction.ShowMessage("Multiplication Of (No Of unit) and (Wt Of Unit), should not be greater than Actual Quantity.");
                                txtNoOfUnit1.Text = "0";
                                lblgrdW_SIDE.Text = "0";
                            }
                            else
                            {
                                lblgrdW_SIDE.Text = (Convert.ToDouble(txtNoOfUnit1.Text) * Convert.ToDouble(txtWtOfUnit.Text)).ToString();
                            }
                            dblTotalQty += Convert.ToDouble(txtNoOfUnit1.Text);
                            dblTotalQty1 += Convert.ToDouble(lblgrdW_SIDE.Text);
                        }
                    }
                    Label txtTotalNoOfUnits = (Label)grdBOM.FooterRow.FindControl("txtTotalNoOfUnits");
                    txtTotalNoOfUnits.Text = dblTotalQty.ToString();
                    Label txtTotalQty = (Label)grdBOM.FooterRow.FindControl("txtTotalQty");
                    txtTotalQty.Text = dblTotalQty1.ToString();
                    AjaxControlToolkit.ModalPopupExtender mpeRed = (AjaxControlToolkit.ModalPopupExtender)thisGridViewRow.FindControl("mpeRed");
                    mpeRed.Show();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in No Of Unit TextChanged..\r\nSee error log for detail."));
        }
    }

    protected void btnCloseSUB_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (ViewState["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)ViewState["dtTRN_SUB"];
            }

            DataView dvTrnEdit = new DataView(dtDetailTBL);
            DataView dvSubTrnEdit = new DataView(dtTRN_SUB);
            foreach (GridViewRow thisGridViewRow in grdMaterialItemReceipt.Rows)
            {
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    GridView grdBOM = (GridView)thisGridViewRow.FindControl("grdBOM");
                    foreach (GridViewRow subgrd in grdBOM.Rows)
                    {
                        if (subgrd.RowType == DataControlRowType.DataRow)
                        {
                            TextBox txtNoOfUnit1 = (TextBox)subgrd.FindControl("txtNoOfUnit1");
                            Label lblgrdW_SIDE = (Label)subgrd.FindControl("lblgrdW_SIDE");
                            Label txtSrNo = (Label)subgrd.FindControl("txtSrNo");

                            dvSubTrnEdit.RowFilter = "UNIQUE_ID = '" + txtSrNo.Text + "'";

                            if (dvSubTrnEdit.Count > 0)
                            {
                                dvSubTrnEdit[0]["NO_OF_UNIT"] = Convert.ToDouble(txtNoOfUnit1.Text);
                                dvSubTrnEdit[0]["TRN_QTY"] = Convert.ToDouble(lblgrdW_SIDE.Text);
                                dtTRN_SUB.AcceptChanges();
                            }
                        }
                    }
                    Label txtTotalNoOfUnits = (Label)grdBOM.FooterRow.FindControl("txtTotalNoOfUnits");
                    Label txtTotalQty = (Label)grdBOM.FooterRow.FindControl("txtTotalQty");
                    Label txtUNIQUEID = (Label)thisGridViewRow.FindControl("txtUNIQUEID");

                    dvTrnEdit.RowFilter = "UNIQUEID = '" + txtUNIQUEID.Text + "'";

                    if (dvTrnEdit.Count > 0)
                    {
                        dvTrnEdit[0]["NO_OF_UNIT"] = Convert.ToDouble(txtTotalNoOfUnits.Text);
                        dvTrnEdit[0]["TRN_QTY"] = Convert.ToDouble(txtTotalQty.Text);
                        dtDetailTBL.AcceptChanges();
                    }
                }
            }
            ViewState["dtTRN_SUB"] = dtTRN_SUB;
            ViewState["dtDetailTBL"] = dtDetailTBL;

            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Sub Tran Button..\r\nSee error log for detail."));
        }
    }
}