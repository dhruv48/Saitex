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
using Obout.ComboBox;

public partial class Module_Production_Controls_DyedIssueFrom : System.Web.UI.UserControl
{
    private SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private string STISSUE_TYPE = "IYS12"; //IYS06
    private string STRECEIPT_TYPE = "RYS07"; //"RYS06";
    private string LOT_NO = string.Empty;
    private static string PRODUCT_TYPE = string.Empty;
    public string PI_NO { get; set; }
    public string ORDER_NO { get; set; }
    private static string Business_type = string.Empty;
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
            BindDepartment(ddlStore);//from store
            BindDropDown(ddlLocation);// from store
            BindDepartment(ddlToStore);
            BindDropDown(ddlToLocation);
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdSave.Visible = true;
            STISSUE_TYPE = "IYS12";
            STRECEIPT_TYPE = "RYS07";
            BATCH_NO.Visible = false;
            Session["dtItemReceipt"] = null;
            ActivateSaveMode();
            BindShift();
            BindDepartment();
            Blankrecords();
            BindCostCode();
            BindNewChallanNum();
            BindReceiveNumb();
            CreateDataTable();
            txtIssueDate.Text = System.DateTime.Now.ToShortDateString();
            RefreshDetailRow();
            //ChkList.Items.Clear();
            BindYarnLotMaster();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindNewChallanNum()
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.TRN_TYPE = STISSUE_TYPE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtChallanNumber.Text = SaitexBL.Interface.Method.YRN_IR_MST.GetNewTRNNumber(oYRN_IR_MST).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindReceiveNumb()
    {
        try
        {
            txtReceiveNumb.Text = GetNewTRNNumb(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, STRECEIPT_TYPE);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string GetNewTRNNumb(string Comp_code, string Branch_code, string ISSUE_TYPE)
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IR_MST oTX_YRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oTX_YRN_IR_MST.TRN_TYPE = ISSUE_TYPE;
            oTX_YRN_IR_MST.COMP_CODE = Comp_code;
            oTX_YRN_IR_MST.BRANCH_CODE = Branch_code;
            oTX_YRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            return SaitexBL.Interface.Method.YRN_IR_MST.GetNewTRNNumber(oTX_YRN_IR_MST);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Blankrecords()
    {
        try
        {
            txtPartyCode.SelectedIndex = -1;
            txtChallanNumber.Text = string.Empty;
            txtDocDate.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            TxtLotIdNo.Text = string.Empty;
            TxtLotIdNo.Visible = true;
            lblPartyCode.Text = string.Empty;
            txtPartyAddress.Text = string.Empty;
            txtIssueDate.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            ddlReprocess.SelectedIndex = 0;
            ddlIssueShift.SelectedIndex = 0;
            txtDepartment.SelectedIndex = txtDepartment.Items.IndexOf(txtDepartment.Items.FindByText("Dyeing"));
            txtunitweight.Text = string.Empty;
            txtnoofunit.Text = string.Empty;
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();
            grdMaterialItemIssue.DataSource = null;
            grdMaterialItemIssue.DataBind();
            lblMode.Text = "You are in Save Mode";
            txtChallanNumber.ReadOnly = true;
            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            TxtLotIdNo.Text = string.Empty;
            // ddlLotNo.SelectedIndex = -1;
            chkLot.Checked = false;
            ddlLotNo.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindCostCode()
    {
        try
        {
            ddlCostCode.Items.Clear();
            ddlCostCode.Items.Add(new ListItem("Select", "NA"));

            DataTable dtCostCode = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("COST_CODE", oUserLoginDetail.COMP_CODE);
            if (dtCostCode != null && dtCostCode.Rows.Count > 0)
            {
                ddlCostCode.DataSource = dtCostCode;
                ddlCostCode.DataTextField = "MST_CODE";
                ddlCostCode.DataValueField = "MST_CODE";
                ddlCostCode.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void CreateDataTable()
    {
        try
        {
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("TRNNUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_FAMILY", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
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
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));

            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("GROSS_WT", typeof(double));
            dtDetailTBL.Columns.Add("TARE_WT", typeof(double));
            dtDetailTBL.Columns.Add("CARTONS", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_PALLET", typeof(string));
            dtDetailTBL.Columns.Add("JOBER", typeof(string));

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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            grdMaterialItemIssue.DataSource = dtDetailTBL;
            grdMaterialItemIssue.DataBind();
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

    private void BindDepartment()
    {
        try
        {
            txtDepartment.Items.Clear();
            txtDepartment.Items.Add(new ListItem("Select", "Select"));

            DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            {
                txtDepartment.DataSource = dtDepartment;
                txtDepartment.DataTextField = "DEPT_NAME";
                txtDepartment.DataValueField = "DEPT_CODE";
                txtDepartment.DataBind();
                txtDepartment.SelectedIndex = txtDepartment.Items.IndexOf(txtDepartment.Items.FindByValue("Dyeing"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void deleteItemIssueRow(int UniqueId)
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
            ViewState["dtDetailTBL"] = dtDetailTBL;
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
            imgbtnDelete.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to delete this record')");
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
    protected void txtChallanNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();

            int iRecordFound = GetdataByChallaNumber(int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim())));
            BindGridFromDataTable();
            if (iRecordFound > 0)
            {

            }
            else
            {
                InitialisePage();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Challan No');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Challan Number.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private int GetdataByChallaNumber(int ChallanNumber)
    {
        int iRecordFound = 0;
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            BindYarnLotMaster();
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, STISSUE_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtChallanNumber.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();
                txtDepartment.SelectedIndex = txtDepartment.Items.IndexOf(txtDepartment.Items.FindByValue(dt.Rows[0]["TO_DEPT_CODE"].ToString().Trim()));
                txtIssueDate.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                txtDocDate.Text = dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim();
                txtDocNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                ddlIssueShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();
                ddlReprocess.Text = dt.Rows[0]["REPROCESS"].ToString().Trim();
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["STORE"].ToString().Trim()));
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dt.Rows[0]["LOCATION"].ToString().Trim()));
                // TxtLotIdNo.Text = dt.Rows[0]["LOT_ID_NO"].ToString().Trim();
                ddlLotNo.SelectedIndex = ddlLotNo.Items.IndexOf(ddlLotNo.Items.FindByValue(dt.Rows[0]["LOT_ID_NO"].ToString()));

                string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)      ";
                string WhereClause = "  and  (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                string SortExpression = " order by PRTY_CODE asc";
                string SearchQuery = "%";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
                txtPartyCode.DataSource = data;
                txtPartyCode.DataTextField = "PRTY_CODE";
                txtPartyCode.DataValueField = "PRTY_NAME";
                txtPartyCode.DataBind();
                foreach (ComboBoxItem item in txtPartyCode.Items)
                {
                    if (item.Text == dt.Rows[0]["PRTY_CODE"].ToString().Trim())
                    {
                        txtPartyCode.SelectedIndex = txtPartyCode.Items.IndexOf(item);
                        lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                        txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                        break;
                    }
                }
                TxtLotIdNo.Text = dt.Rows[0]["LOT_ID_NO"].ToString().Trim();
                if (ddlLotNo.SelectedIndex < 0)
                {
                    ddlLotNo.Visible = false;
                    TxtLotIdNo.Visible = true;
                }
            }
            if (iRecordFound == 1)
            {
                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, STISSUE_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                MapDataTable();
                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueAdjByMst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, STISSUE_TYPE, ChallanNumber);
                    MapAdjustDataTable(dtReceiptAdjustment);
                    Session["dtItemReceipt"] = dtReceiptAdjustment;
                }
            }
            else
            {
                string msg = "Dear " + oUserLoginDetail.Username + " !! MRN already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
                InitialisePage();
                txtChallanNumber.Text = string.Empty;
                ddlTRNNumber.Focus();
                lblMode.Text = "You are in Update Mode";
                ActivateUpdateMode();
                RefreshDetailRow();
            }
            return iRecordFound;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void MapAdjustDataTable(DataTable dtReceiptAdjustment)
    {
        try
        {

            if (!dtReceiptAdjustment.Columns.Contains("UNIQUEID"))
                dtReceiptAdjustment.Columns.Add("UNIQUEID", typeof(double));
            if (!dtReceiptAdjustment.Columns.Contains("NO_OF_UNIT"))
                dtReceiptAdjustment.Columns.Add("NO_OF_UNIT", typeof(double));
            if (!dtReceiptAdjustment.Columns.Contains("UOM_OF_UNIT"))
                dtReceiptAdjustment.Columns.Add("UOM_OF_UNIT", typeof(string));
            if (!dtReceiptAdjustment.Columns.Contains("WEIGHT_OF_UNIT"))
                dtReceiptAdjustment.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            if (!dtReceiptAdjustment.Columns.Contains("PI_NO"))
                dtReceiptAdjustment.Columns.Add("PI_NO", typeof(string));
            if (!dtReceiptAdjustment.Columns.Contains("ISS_PI_NO"))
                dtReceiptAdjustment.Columns.Add("ISS_PI_NO", typeof(string));
            for (int iLoop = 0; iLoop < dtReceiptAdjustment.Rows.Count; iLoop++)
            {
                dtReceiptAdjustment.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                if (dtReceiptAdjustment.Rows[iLoop]["NO_OF_UNIT"] == null)
                    dtReceiptAdjustment.Rows[iLoop]["NO_OF_UNIT"] = 0f;
                if (dtReceiptAdjustment.Rows[iLoop]["UOM_OF_UNIT"] == null)
                    dtReceiptAdjustment.Rows[iLoop]["UOM_OF_UNIT"] = string.Empty;
                if (dtReceiptAdjustment.Rows[iLoop]["WEIGHT_OF_UNIT"] == null)
                    dtReceiptAdjustment.Rows[iLoop]["WEIGHT_OF_UNIT"] = 0f;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void MapDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                if (txtPartyCode.SelectedIndex >= 0)
                {
                    saveMaterialReceipt();
                }
                else if (txtPartyCode.SelectedIndex < 0)
                {
                    CommonFuction.ShowMessage("Please select party....!");
                }
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

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ActivateUpdateMode();
            RefreshDetailRow();
            lblMode.Text = "You are in Update Mode";
            txtChallanNumber.Text = string.Empty;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation mode.\r\nSee error log for detail."));
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
                UpdateMaterialReceipt();
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
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting data.\r\nSee error log for detail."));
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

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Yarn/SalesWork/Reports/YRN_ReceiptPermRPT.aspx?STISSUE_TYPE=" + STISSUE_TYPE, false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in generating report data.\r\nSee error log for detail."));
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

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdMaterialItemIssue_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItemReceiptRow(UniqueId);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItemIssueRow(UniqueId);
                BindGridFromDataTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Editing/ deletion of Material detail.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            bool ReturnResult = false;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            int count = 0;
            msg = string.Empty;

            if (txtChallanNumber.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Issue Number required.\r\n";
            }
            //if (ddlLotNo.SelectedIndex > 0 || ddlLotNo.SelectedValue.ToString() != null)
            //{
            //    count += 1;
            //}
            //if (!string.IsNullOrEmpty(TxtLotIdNo.Text))
            //{
            //    count += 1;
            //}
            //else
            //{
            //    msg += @"#. Lot Id Requried.\r\n";
            //}

            if (txtDepartment.SelectedIndex > 0 || txtDepartment.SelectedValue.Trim() != "Select")
            {
                if (!oUserLoginDetail.VC_DEPARTMENTCODE.Equals(txtDepartment.SelectedValue.Trim()))
                {
                    count += 1;
                }
                else
                {
                    msg += @"#. Issue department cannot be same as logged user department.\r\n";
                }
            }
            else
            {
                msg += @"#. Please select Department first.\r\n";
            }

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
            }

            if (count == 3)
                ReturnResult = true;

            return ReturnResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SearchItemCodeInGrid(string yarnCode, int UniqueId)
    {
        bool Result = false;
        string SHADE_CODE = txtShade.Text;
        try
        {
            foreach (GridViewRow grdRow in grdMaterialItemIssue.Rows)
            {
                Label lblPICode = (Label)grdRow.FindControl("lblPICode");
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                Label txtShadeCode = (Label)grdRow.FindControl("txtShadeCode");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                Label lblMacCode = (Label)grdRow.FindControl("txtMacCode");
                if (lblPICode.Text.Trim() == txtPANo.Text.Trim() && txtICODE.Text.Trim() == yarnCode && txtShadeCode.Text == SHADE_CODE && UniqueId != iUniqueId)
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

    private void CalculateAmount()
    {
        try
        {
            double Qty = 0;
            double Rate = 0;
            double Amount = 0;
            double.TryParse(CommonFuction.funFixQuotes(txtQTY.Text.Trim()), out Qty);
            double.TryParse(CommonFuction.funFixQuotes(txtBasicRate.Text.Trim()), out Rate);
            double.TryParse(CommonFuction.funFixQuotes(txtAmount.Text.Trim()), out Amount);

            Amount = Rate * Qty;
            txtAmount.Text = Amount.ToString();
            txtBasicRate.Text = Rate.ToString();
            txtQTY.Text = Qty.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void saveMaterialReceipt()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;  // txtDepartment.SelectedValue.Trim();
            oYRN_IR_MST.FORM_NUMB = string.Empty;
            oYRN_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oYRN_IR_MST.GATE_NUMB = string.Empty;
            oYRN_IR_MST.GATE_OUT_NUMB = string.Empty;
            oYRN_IR_MST.GATE_PASS_TYPE = string.Empty;
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;
            oYRN_IR_MST.LR_NUMB = string.Empty;
            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;
            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = txtPartyCode.SelectedText.ToString();
            oYRN_IR_MST.PRTY_NAME = txtPartyCode.SelectedValue.ToString();
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
            oYRN_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();
            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;
            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(STISSUE_TYPE);
            oYRN_IR_MST.TRSP_CODE = "NA";
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            // oYRN_IR_MST.LOT_ID_NO = ddlLotNo.SelectedValue.Trim().ToString();
            oYRN_IR_MST.LOT_ID_NO = TxtLotIdNo.Text;// ChkList.SelectedValue.Trim().ToString();

            oYRN_IR_MST.FR_DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.TO_DEPT_CODE = txtDepartment.SelectedValue.Trim();

            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            //oYRN_IR_MST.TO_LOCATION = string.Empty;
            //oYRN_IR_MST.TO_STORE = string.Empty;

            oYRN_IR_MST.TO_LOCATION = ddlToLocation.SelectedValue.ToString().Trim();
            oYRN_IR_MST.TO_STORE = ddlToStore.SelectedValue.ToString().Trim();

            oYRN_IR_MST.CONSIGNEE_CODE = string.Empty;

            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.VC_BRANCHNAME;
            oYRN_IR_MST.BILL_DATE = DateTime.Now;
            int BILLNO = 0;
            int.TryParse("", out BILLNO);
            oYRN_IR_MST.BILL_NUMBER = BILLNO;
            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            double BILLAMOUNT = 0;
            double.TryParse("", out BILLAMOUNT);
            oYRN_IR_MST.PARTY_BILL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.TOTAL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            dtItemReceipt.Columns.Add("DYED_BATCH", typeof(string));
            for (int i = 0; i < dtItemReceipt.Rows.Count; i++)
            {

                dtItemReceipt.Rows[i]["DYED_BATCH"] = "NA";

            }


            int TRN_NUMB = 0;
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue);
            if (result)
            {
                int REceipt_numb = 0;
                if (saveMaterialReceipt(out REceipt_numb, dtItemReceipt))
                {
                    string Resultmsg = "Dyeing Production Transfer Save Successfully" + "\\r\\n";
                    Resultmsg += "Issue Number  is:" + oYRN_IR_MST.TRN_NUMB;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                    InitialisePage();
                }
            }
            else
            {
                CommonFuction.ShowMessage("data  Saving Failed");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool saveMaterialReceipt(out int RECEIPT_NUMB, DataTable dtItemReceipt)
    {
        RECEIPT_NUMB = 0;
        try
        {
            Hashtable htIssue = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;  // txtDepartment.SelectedValue.Trim();
            oYRN_IR_MST.FORM_NUMB = string.Empty;
            oYRN_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oYRN_IR_MST.GATE_NUMB = string.Empty;
            oYRN_IR_MST.GATE_OUT_NUMB = string.Empty;
            oYRN_IR_MST.GATE_PASS_TYPE = string.Empty;
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = string.Empty;

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = txtPartyCode.SelectedText.ToString();
            oYRN_IR_MST.PRTY_NAME = txtPartyCode.SelectedValue.ToString();
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
            oYRN_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(STRECEIPT_TYPE);
            oYRN_IR_MST.TRSP_CODE = "NA";
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            // oYRN_IR_MST.LOT_ID_NO = TxtLotIdNo.Text.Trim().ToUpper().ToString();
            // oYRN_IR_MST.LOT_ID_NO = ddlLotNo.SelectedValue.Trim().ToString();
            oYRN_IR_MST.LOT_ID_NO = TxtLotIdNo.Text;// ChkList.SelectedValue.Trim().ToString();

            oYRN_IR_MST.FR_DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.TO_DEPT_CODE = txtDepartment.SelectedValue.Trim();

            oYRN_IR_MST.LOCATION = ddlToLocation.SelectedValue.ToString().Trim();
            oYRN_IR_MST.STORE = ddlToStore.SelectedValue.ToString().Trim();
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            //oYRN_IR_MST.TO_LOCATION = ddlToLocation.SelectedValue.ToString().Trim();
            //oYRN_IR_MST.TO_STORE = ddlToStore.SelectedValue.ToString().Trim();

            oYRN_IR_MST.CONSIGNEE_CODE = string.Empty;

            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.VC_BRANCHNAME;
            oYRN_IR_MST.BILL_DATE = DateTime.Now;
            int BILLNO = 0;
            int.TryParse("", out BILLNO);
            oYRN_IR_MST.BILL_NUMBER = BILLNO;
            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            double BILLAMOUNT = 0;
            double.TryParse("", out BILLAMOUNT);
            oYRN_IR_MST.PARTY_BILL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.TOTAL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;

            return SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out RECEIPT_NUMB, dtDetailTBL, htIssue, dtItemReceipt);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void UpdateMaterialReceipt()
    {
        try
        {
            Hashtable htIssue = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();

            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = string.Empty;
            oYRN_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oYRN_IR_MST.GATE_NUMB = string.Empty;
            oYRN_IR_MST.GATE_OUT_NUMB = string.Empty;
            oYRN_IR_MST.GATE_PASS_TYPE = string.Empty;
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = string.Empty;

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = txtPartyCode.SelectedText.ToString();
            oYRN_IR_MST.PRTY_NAME = txtPartyCode.SelectedValue.ToString();
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
            oYRN_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(STISSUE_TYPE);
            oYRN_IR_MST.TRSP_CODE = "NA";
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            // oYRN_IR_MST.LOT_ID_NO = TxtLotIdNo.Text.Trim().ToString();
            // oYRN_IR_MST.LOT_ID_NO = ddlLotNo.SelectedValue.Trim().ToString();
            oYRN_IR_MST.LOT_ID_NO = TxtLotIdNo.Text;// ChkList.SelectedValue.Trim().ToString();
            oYRN_IR_MST.FR_DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.TO_DEPT_CODE = txtDepartment.SelectedValue.Trim();
            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;

            oYRN_IR_MST.CONSIGNEE_CODE = string.Empty;

            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.VC_BRANCHNAME;
            oYRN_IR_MST.BILL_DATE = DateTime.Now;
            int BILLNO = 0;
            int.TryParse("", out BILLNO);
            oYRN_IR_MST.BILL_NUMBER = BILLNO;
            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            double BILLAMOUNT = 0;
            double.TryParse("", out BILLAMOUNT);
            oYRN_IR_MST.PARTY_BILL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.TOTAL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Update(oYRN_IR_MST, dtDetailTBL, dtItemReceipt, htIssue);
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            if (dtDetailTBL.Rows.Count < 15)
            {
                if (txtPANo.Text != "" && txtQTY.Text != "" && txtBasicRate.Text != "" && txtMacCode.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                    bool bb = SearchItemCodeInGrid(txtYarnCode.Text.Trim(), UNIQUEID);
                    if (!bb)
                    {
                        double Qty = 0;
                        double.TryParse(txtQTY.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {
                            if (UNIQUEID > 0)
                            {
                                DataView dv = new DataView(dtDetailTBL);
                                dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                                if (dv.Count > 0)
                                {
                                    dv[0]["PO_NUMB"] = txtPONumb.Text.Trim();
                                    dv[0]["PO_TYPE"] = "PRD01";
                                    dv[0]["PO_COMP_CODE"] = "C00001";
                                    dv[0]["PO_BRANCH"] = "SL0001";
                                    dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                    dv[0]["YARN_CODE"] = txtYarnCode.Text.Trim();
                                    dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                    dv[0]["SHADE_CODE"] = txtShade.Text.Trim();
                                    dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                                    dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                    dv[0]["UOM"] = txtUNIT.Text.Trim();
                                    dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                    dv[0]["COST_CODE"] = ddlCostCode.SelectedValue.Trim();
                                    dv[0]["MAC_CODE"] = txtMacCode.Text.Trim();
                                    dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                    dv[0]["QCFLAG"] = "No";
                                    DateTime dd = System.DateTime.Now;
                                    dv[0]["DATE_OF_MFG"] = dd;
                                    dv[0]["UOM_OF_UNIT"] = "KGS";
                                    dv[0]["WEIGHT_OF_UNIT"] = txtunitweight.Text;
                                    dv[0]["NO_OF_UNIT"] = double.Parse(txtnoofunit.Text.Trim());

                                    dv[0]["PI_NO"] = txtPANo.Text.ToString();

                                    dv[0]["LOT_NO"] = txtLotNo.Text.Trim();
                                    dv[0]["GRADE"] = "NA";
                                    dv[0]["GROSS_WT"] = 0;
                                    dv[0]["TARE_WT"] = 0;
                                    dv[0]["CARTONS"] = 0;
                                    dtDetailTBL.AcceptChanges();
                                }
                            }
                            else
                            {
                                DataRow dr = dtDetailTBL.NewRow();
                                dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                dr["PO_NUMB"] = txtPONumb.Text.Trim();
                                dr["PO_TYPE"] = "PRD01";
                                dr["PO_COMP_CODE"] = "C00001";
                                dr["PO_BRANCH"] = "SL0001";
                                dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dr["YARN_CODE"] = txtYarnCode.Text.Trim();
                                dr["YARN_DESC"] = txtDESC.Text.Trim();
                                dr["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                                dr["SHADE_CODE"] = txtShade.Text.Trim();
                                dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dr["UOM"] = txtUNIT.Text.Trim();
                                dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                dr["COST_CODE"] = ddlCostCode.SelectedValue.Trim();
                                dr["MAC_CODE"] = txtMacCode.Text.Trim();
                                dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                dr["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dr["DATE_OF_MFG"] = dd;
                                dr["UOM_OF_UNIT"] = "UOM";
                                dr["WEIGHT_OF_UNIT"] = txtunitweight.Text;
                                dr["NO_OF_UNIT"] = double.Parse(txtnoofunit.Text.Trim());
                                dr["PI_NO"] = txtPANo.Text.ToString();

                                dr["LOT_NO"] = txtLotNo.Text.Trim();
                                dr["GRADE"] = "NA";
                                dr["GROSS_WT"] = 0;
                                dr["TARE_WT"] = 0;
                                dr["CARTONS"] = 0;
                                dtDetailTBL.Rows.Add(dr);

                            }
                            ViewState["dtDetailTBL"] = dtDetailTBL;
                            RefreshDetailRow();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code, it is already in use.');", true);
                    }
                }
                grdMaterialItemIssue.DataSource = dtDetailTBL;
                grdMaterialItemIssue.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding YARN detail data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            DDLPiNo.SelectedIndex = -1;
            ddlMacCode.SelectedIndex = -1;
            txtDESC.Text = string.Empty;
            txtQTY.Text = string.Empty;
            txtUNIT.Text = string.Empty;
            txtBasicRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            ddlCostCode.SelectedIndex = -1;
            txtMacCode.Text = string.Empty;
            txtDetRemarks.Text = string.Empty;
            txtPANo.Text = string.Empty;
            txtYarnCode.Text = string.Empty;
            txtShade.Text = string.Empty;
            txtBalQty.Text = string.Empty;
            txtunitweight.Text = string.Empty;
            txtnoofunit.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtPONumb.Text = string.Empty;

            ViewState["UNIQUEID"] = null;
        }
        catch (Exception ex)
        {
            throw ex;
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

    private void EditItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                ViewState["UNIQUEID"] = UNIQUEID;

                txtPANo.Text = dv[0]["PI_NO"].ToString();
                txtYarnCode.Text = dv[0]["YARN_CODE"].ToString();
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();
                txtShadeFamily.Text = dv[0]["SHADE_FAMILY"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                ddlCostCode.SelectedIndex = ddlCostCode.Items.IndexOf(ddlCostCode.Items.FindByValue(dv[0]["COST_CODE"].ToString()));
                txtMacCode.Text = dv[0]["MAC_CODE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAdjRec_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStore.SelectedIndex > 0)
            {
                if (txtYarnCode.Text != "" || txtShade.Text != "")
                {
                    string URL = "Yrn_Production_Adjustment.aspx";
                    URL = URL + "?ItemCodeId=" + txtYarnCode.Text.Trim();
                    URL = URL + "&SHADE_FAMILY=" + txtShadeFamily.Text;
                    URL = URL + "&SHADE_CODE=" + txtShade.Text;
                    URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                    URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
                    URL = URL + "&AmountId=" + txtAmount.ClientID;
                    URL = URL + "&TRN_TYPE=" + STISSUE_TYPE;
                    URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
                    URL = URL + "&txtQtyUnit=" + txtnoofunit.ClientID;
                    URL = URL + "&txtQtyUom=" + txtUNIT.ClientID;
                    URL = URL + "&txtQtyWeight=" + txtunitweight.ClientID;
                    URL = URL + "&UOM_OF_UNIT=" + txtUNIT.Text;
                    URL = URL + "&MAX_QTY=" + txtBalQty.Text;
                    URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
                    URL = URL + "&STORE=" + ddlStore.SelectedValue;
                    URL = URL + "&PI_NO=" + txtPANo.Text;
                    URL = URL + "&PARTY_CODE=" + lblPartyCode.Text;
                    URL = URL + "&PO_NUMB=" + txtPONumb.Text.Trim();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                    txtQTY.ReadOnly = false;
                    txtAmount.ReadOnly = false;
                    txtBasicRate.ReadOnly = false;
                    // txtunitweight.ReadOnly = false;
                    //  txtnoofunit.ReadOnly = false;

                }
                else
                {
                    CommonFuction.ShowMessage("Please select YARN Code to adjust Receipt.");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please select Store to adjust Receipt.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Adjustment Window Opening.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            CalculateAmount();
            txtQTY.ReadOnly = true;
            txtBasicRate.ReadOnly = true;
            txtAmount.ReadOnly = true;
            //  txtnoofunit.ReadOnly = true;
            //  txtunitweight.ReadOnly = true;
           // txtnoofunit.Text = "1";
            txtunitweight.Text = "1";

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Final Amount.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            e.ItemsCount = data.Rows.Count;

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
            string whereClause = " where TRN_NUMB like :searchQuery or TRN_DATE like :searchQuery or prty_code like :searchQuery or prty_name like :searchQuery )  ";
            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";
            string commandText = "select * from (select * from (Select a.TRN_NUMB, a.TRN_DATE,a.PRTY_CODE,b.PRTY_NAME,c.DEPT_NAME from YRN_IR_MST a, tx_vendor_mst b, cm_dept_mst c Where a.prty_code=b.prty_code (+) and A.DEPT_CODE=C.DEPT_CODE and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + STISSUE_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlTRNNumber_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();

            int iRecordFound = GetdataByChallaNumber(TRN_NUMBER);
            BindGridFromDataTable();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Challan Number.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void ActivateUpdateMode()
    {
        try
        {
            txtChallanNumber.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();
            txtChallanNumber.Visible = false;
            ddlTRNNumber.Visible = true;
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
            txtChallanNumber.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();
            txtChallanNumber.Visible = true;
            ddlTRNNumber.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DDLPiNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPAData(e.Text.ToUpper(), e.ItemsOffset);

            DDLPiNo.Items.Clear();

            DDLPiNo.DataSource = data;
            DDLPiNo.DataTextField = "PI_NO";
            DDLPiNo.DataValueField = "TRN_DATA";
            DDLPiNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPADATACount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPAData(string Text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;
            string whereClause = string.Empty;
            if (lblPartyCode.Text.Trim() != string.Empty && lblYarnCode.Text.Trim() == string.Empty)
            {
                CommandText = "SELECT   *  FROM   (SELECT   * FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,  t.ARTICAL_CODE BASE_ARTICAL_CODE,  t.ARTICAL_DESC base_artical_desc,  t.SHADE_CODE base_SHADE_CODE,  t.SHADE_CODE base_SHADE_FAMILY, (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))  QTY_REM, CAST (t.BATCH_CODE AS int) PO_NUMB, t.MACHINE_CODE, DECODE (NVL (B.QC_COMP_FLAG, 0), 0,'Pending', 1, 'Appoved') AS QC_COMP_FLAG, DECODE (NVL (B.H_COMP_FLAG, 0),  0, 'Pending', 1, 'Appoved') AS H_COMP_FLAG, (   t.PA_NO || '@'|| t.ARTICAL_CODE|| '@'|| t.ARTICAL_DESC|| '@'|| t.SHADE_CODE|| '@'  || t.SHADE_CODE  || '@'|| t.UOM_OF_UNIT || '@'|| t.UOM_OF_UNIT|| '@'|| 1 || '@'|| (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))|| '@'|| t.PRTY_CODE|| '@'|| t.PARTY_NAME  || '@'  || t.BUSINESS_TYPE  || '@'  || t.GREY_LOT_NO|| '@' || t.BATCH_CODE || '@'|| t.MACHINE_CODE  || '@' || NVL (B.QC_RELEASE, 0)  || '@'  || NVL (B.H_COMP_FLAG, 0)) TRN_DATA  FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B  WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1   AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) >  0     AND T.BATCH_CODE = B.BATCH_CODE    AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "'  AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND T.PRTY_CODE='" + lblPartyCode.Text.Trim() + "'  ORDER BY   PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery) WHERE 1=1  ";

                if (startOffset != 0)
                {
                    whereClause += " AND  NOT IN(PA_NO PI_NO,   GREY_LOT_NO LOT_NO,  ARTICAL_CODE BASE_ARTICAL_CODE,  ARTICAL_DESC base_artical_desc,  SHADE_CODE base_SHADE_CODE FROM (SELECT   * FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,  t.ARTICAL_CODE BASE_ARTICAL_CODE,  t.ARTICAL_DESC base_artical_desc,  t.SHADE_CODE base_SHADE_CODE,  t.SHADE_CODE base_SHADE_FAMILY, (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))  QTY_REM, CAST (t.BATCH_CODE AS int) PO_NUMB, t.MACHINE_CODE,DECODE (NVL (B.QC_COMP_FLAG, 0), 0,'Pending', 1, 'Appoved') AS QC_COMP_FLAG, DECODE (NVL (B.H_COMP_FLAG, 0),  0, 'Pending', 1, 'Appoved') AS H_COMP_FLAG, (   t.PA_NO || '@'|| t.ARTICAL_CODE|| '@'|| t.ARTICAL_DESC|| '@'|| t.SHADE_CODE|| '@'  || t.SHADE_CODE  || '@'|| t.UOM_OF_UNIT || '@'|| t.UOM_OF_UNIT|| '@'|| 1 || '@'|| (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))|| '@'|| t.PRTY_CODE|| '@'|| t.PARTY_NAME  || '@'  || t.BUSINESS_TYPE  || '@'  || t.GREY_LOT_NO|| '@' || t.BATCH_CODE || '@'|| t.MACHINE_CODE || '@' || NVL (B.QC_RELEASE, 0)  || '@'  || NVL (B.H_COMP_FLAG, 0)) TRN_DATA  FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B  WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1   AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) >  0     AND T.BATCH_CODE = B.BATCH_CODE     AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "'  AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND T.PRTY_CODE='" + lblPartyCode.Text.Trim() + "' ORDER BY   PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
                }
            }

            else if (lblYarnCode.Text.Trim() != string.Empty && lblPartyCode.Text.Trim() == string.Empty)
            {
                CommandText = "SELECT   *  FROM   (SELECT   * FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,  t.ARTICAL_CODE BASE_ARTICAL_CODE,  t.ARTICAL_DESC base_artical_desc,  t.SHADE_CODE base_SHADE_CODE,  t.SHADE_CODE base_SHADE_FAMILY, (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))  QTY_REM, CAST (t.BATCH_CODE AS int) PO_NUMB, t.MACHINE_CODE,DECODE (NVL (B.QC_COMP_FLAG, 0), 0,'Pending', 1, 'Appoved') AS QC_COMP_FLAG, DECODE (NVL (B.H_COMP_FLAG, 0),  0, 'Pending', 1, 'Appoved') AS H_COMP_FLAG, (   t.PA_NO || '@'|| t.ARTICAL_CODE|| '@'|| t.ARTICAL_DESC|| '@'|| t.SHADE_CODE|| '@'  || t.SHADE_CODE  || '@'|| t.UOM_OF_UNIT || '@'|| t.UOM_OF_UNIT|| '@'|| 1 || '@'|| (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))|| '@'|| t.PRTY_CODE|| '@'|| t.PARTY_NAME  || '@'  || t.BUSINESS_TYPE  || '@'  || t.GREY_LOT_NO|| '@' || t.BATCH_CODE || '@'|| t.MACHINE_CODE  || '@' || NVL (B.QC_RELEASE, 0)  || '@'  || NVL (B.H_COMP_FLAG, 0)) TRN_DATA  FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B  WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1   AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) >  0     AND T.BATCH_CODE = B.BATCH_CODE    AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "'  AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND T.ARTICAL_CODE='" + lblYarnCode.Text.Trim() + "'  ORDER BY   PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery) WHERE 1=1  ";

                if (startOffset != 0)
                {
                    whereClause += " AND  NOT IN(PA_NO PI_NO,   GREY_LOT_NO LOT_NO,  ARTICAL_CODE BASE_ARTICAL_CODE,  ARTICAL_DESC base_artical_desc,  SHADE_CODE base_SHADE_CODE FROM (SELECT   * FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,  t.ARTICAL_CODE BASE_ARTICAL_CODE,  t.ARTICAL_DESC base_artical_desc,  t.SHADE_CODE base_SHADE_CODE,  t.SHADE_CODE base_SHADE_FAMILY, (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))  QTY_REM, CAST (t.BATCH_CODE AS int) PO_NUMB, t.MACHINE_CODE,DECODE (NVL (B.QC_COMP_FLAG, 0), 0,'Pending', 1, 'Appoved') AS QC_COMP_FLAG, DECODE (NVL (B.H_COMP_FLAG, 0),  0, 'Pending', 1, 'Appoved') AS H_COMP_FLAG, (   t.PA_NO || '@'|| t.ARTICAL_CODE|| '@'|| t.ARTICAL_DESC|| '@'|| t.SHADE_CODE|| '@'  || t.SHADE_CODE  || '@'|| t.UOM_OF_UNIT || '@'|| t.UOM_OF_UNIT|| '@'|| 1 || '@'|| (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))|| '@'|| t.PRTY_CODE|| '@'|| t.PARTY_NAME  || '@'  || t.BUSINESS_TYPE  || '@'  || t.GREY_LOT_NO|| '@' || t.BATCH_CODE || '@'|| t.MACHINE_CODE || '@' || NVL (B.QC_RELEASE, 0)  || '@'  || NVL (B.H_COMP_FLAG, 0)) TRN_DATA  FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B  WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1  AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) >  0     AND T.BATCH_CODE = B.BATCH_CODE     AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "'  AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND T.ARTICAL_CODE='" + lblYarnCode.Text.Trim() + "' ORDER BY   PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
                }
            }

            else if (lblYarnCode.Text.Trim() != string.Empty && lblPartyCode.Text.Trim() != string.Empty)
            {
                CommandText = "SELECT   *  FROM   (SELECT   * FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,  t.ARTICAL_CODE BASE_ARTICAL_CODE,  t.ARTICAL_DESC base_artical_desc,  t.SHADE_CODE base_SHADE_CODE,  t.SHADE_CODE base_SHADE_FAMILY, (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))  QTY_REM, CAST (t.BATCH_CODE AS int) PO_NUMB, t.MACHINE_CODE, DECODE (NVL (B.QC_COMP_FLAG, 0), 0,'Pending', 1, 'Appoved') AS QC_COMP_FLAG, DECODE (NVL (B.H_COMP_FLAG, 0),  0, 'Pending', 1, 'Appoved') AS H_COMP_FLAG,(   t.PA_NO || '@'|| t.ARTICAL_CODE|| '@'|| t.ARTICAL_DESC|| '@'|| t.SHADE_CODE|| '@'  || t.SHADE_CODE  || '@'|| t.UOM_OF_UNIT || '@'|| t.UOM_OF_UNIT|| '@'|| 1 || '@'|| (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))|| '@'|| t.PRTY_CODE|| '@'|| t.PARTY_NAME  || '@'  || t.BUSINESS_TYPE  || '@'  || t.GREY_LOT_NO|| '@' || t.BATCH_CODE || '@'|| t.MACHINE_CODE  || '@' || NVL (B.QC_RELEASE, 0)  || '@'  || NVL (B.H_COMP_FLAG, 0)) TRN_DATA  FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B  WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1  AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) >  0     AND T.BATCH_CODE = B.BATCH_CODE    AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "'  AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND T.ARTICAL_CODE='" + lblYarnCode.Text.Trim() + "' AND T.PRTY_CODE='" + lblPartyCode.Text.Trim() + "'  ORDER BY   PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery) WHERE 1=1  ";

                if (startOffset != 0)
                {
                    whereClause += " AND  NOT IN(PA_NO PI_NO,   GREY_LOT_NO LOT_NO,  ARTICAL_CODE BASE_ARTICAL_CODE,  ARTICAL_DESC base_artical_desc,  SHADE_CODE base_SHADE_CODE FROM (SELECT   * FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,  t.ARTICAL_CODE BASE_ARTICAL_CODE,  t.ARTICAL_DESC base_artical_desc,  t.SHADE_CODE base_SHADE_CODE,  t.SHADE_CODE base_SHADE_FAMILY, (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))  QTY_REM, CAST (t.BATCH_CODE AS int) PO_NUMB, t.MACHINE_CODE, DECODE (NVL (B.QC_COMP_FLAG, 0), 0,'Pending', 1, 'Appoved') AS QC_COMP_FLAG, DECODE (NVL (B.H_COMP_FLAG, 0),  0, 'Pending', 1, 'Appoved') AS H_COMP_FLAG,(   t.PA_NO || '@'|| t.ARTICAL_CODE|| '@'|| t.ARTICAL_DESC|| '@'|| t.SHADE_CODE|| '@'  || t.SHADE_CODE  || '@'|| t.UOM_OF_UNIT || '@'|| t.UOM_OF_UNIT|| '@'|| 1 || '@'|| (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))|| '@'|| t.PRTY_CODE|| '@'|| t.PARTY_NAME  || '@'  || t.BUSINESS_TYPE  || '@'  || t.GREY_LOT_NO|| '@' || t.BATCH_CODE || '@'|| t.MACHINE_CODE || '@' || NVL (B.QC_RELEASE, 0)  || '@'  || NVL (B.H_COMP_FLAG, 0)) TRN_DATA  FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B  WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1   AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) >  0     AND T.BATCH_CODE = B.BATCH_CODE     AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "'  AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND T.ARTICAL_CODE='" + lblYarnCode.Text.Trim() + "' AND T.PRTY_CODE='" + lblPartyCode.Text.Trim() + "' ORDER BY   PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
                }
            }

            else
            {
                CommandText = "SELECT   * FROM   (SELECT   *FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,t.ARTICAL_CODE BASE_ARTICAL_CODE,t.ARTICAL_DESC base_artical_desc,t.SHADE_CODE base_SHADE_CODE,t.SHADE_CODE base_SHADE_FAMILY,(NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) QTY_REM,CAST (t.BATCH_CODE AS int) PO_NUMB,t.MACHINE_CODE,DECODE (NVL (B.QC_RELEASE, 0),0,'Unapproved',1,'Approved',2,'Partially Approved') AS QC_RELEASE,DECODE (NVL (B.H_COMP_FLAG, 0),0, 'Pending',1, 'Approved') AS H_COMP_FLAG, (   t.PA_NO || '@' || t.ARTICAL_CODE || '@' || t.ARTICAL_DESC || '@' || t.SHADE_CODE || '@' || t.SHADE_CODE || '@' || t.UOM_OF_UNIT || '@' || t.UOM_OF_UNIT || '@' || 1 || '@' || (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) || '@' || t.PRTY_CODE || '@' || t.PARTY_NAME || '@' || t.BUSINESS_TYPE || '@' || t.GREY_LOT_NO || '@' || t.BATCH_CODE || '@' || t.MACHINE_CODE || '@' || NVL (B.QC_RELEASE, 0) || '@' || NVL (B.H_COMP_FLAG, 0)) TRN_DATA FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1 AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) > 0 AND T.BATCH_CODE = B.BATCH_CODE AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY   PI_NO) WHERE      pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery) WHERE   1 = 1  ";

                if (startOffset != 0)
                {
                    whereClause += " AND  NOT IN(PA_NO PI_NO,   GREY_LOT_NO LOT_NO,  ARTICAL_CODE BASE_ARTICAL_CODE,  ARTICAL_DESC base_artical_desc,  SHADE_CODE base_SHADE_CODE FROM (SELECT   * FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,  t.ARTICAL_CODE BASE_ARTICAL_CODE,  t.ARTICAL_DESC base_artical_desc,  t.SHADE_CODE base_SHADE_CODE,  t.SHADE_CODE base_SHADE_FAMILY, (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))  QTY_REM,  CAST (t.BATCH_CODE AS int) PO_NUMB, t.MACHINE_CODE,DECODE (NVL (B.QC_COMP_FLAG, 0), 0,'Pending', 1, 'Appoved','2','Partially Appoved') AS QC_COMP_FLAG, DECODE (NVL (B.H_COMP_FLAG, 0),  0, 'Pending', 1, 'Appoved') AS H_COMP_FLAG, (   t.PA_NO || '@'|| t.ARTICAL_CODE|| '@'|| t.ARTICAL_DESC|| '@'|| t.SHADE_CODE|| '@'  || t.SHADE_CODE  || '@'|| t.UOM_OF_UNIT || '@'|| t.UOM_OF_UNIT|| '@'|| 1 || '@'|| (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))|| '@'|| t.PRTY_CODE|| '@'|| t.PARTY_NAME  || '@'  || t.BUSINESS_TYPE  || '@'  || t.GREY_LOT_NO|| '@' || t.BATCH_CODE || '@'|| t.MACHINE_CODE || '@' || NVL (B.QC_RELEASE, 0)  || '@'  || NVL (B.H_COMP_FLAG, 0)) TRN_DATA  FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B  WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1  AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) >  0     AND T.BATCH_CODE = B.BATCH_CODE     AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "'  AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "'  ORDER BY   PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
                }
            }

            string SortExpression = " order by PO_NUMB ASC";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected int GetPADATACount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = "SELECT   *  FROM   (SELECT   * FROM   (  SELECT   t.PA_NO PI_NO,t.GREY_LOT_NO LOT_NO,  t.ARTICAL_CODE BASE_ARTICAL_CODE,  t.ARTICAL_DESC base_artical_desc,  t.SHADE_CODE base_SHADE_CODE,  t.SHADE_CODE base_SHADE_FAMILY, (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))  QTY_REM,  t.BATCH_CODE PO_NUMB, t.MACHINE_CODE, (   t.PA_NO || '@'|| t.ARTICAL_CODE|| '@'|| t.ARTICAL_DESC|| '@'|| t.SHADE_CODE|| '@'  || t.SHADE_CODE  || '@'|| t.UOM_OF_UNIT || '@'|| t.UOM_OF_UNIT|| '@'|| 1 || '@'|| (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0))|| '@'|| t.PRTY_CODE|| '@'|| t.PARTY_NAME  || '@'  || t.BUSINESS_TYPE  || '@'  || t.GREY_LOT_NO|| '@' || t.BATCH_CODE || '@'|| t.MACHINE_CODE  || '@' || NVL (B.QC_RELEASE, 0)  || '@'  || NVL (B.H_COMP_FLAG, 0)) TRN_DATA  FROM   V_YRN_PROD_DYEING_TRN t, BATCH_CARD_MST B  WHERE   t.TRN_TYPE = 'RYS21' AND t.CONF_FLAG = 1   AND (NVL (t.TRN_QTY, 0) - NVL (t.ISS_TRN_QTY, 0)) >  0     AND T.BATCH_CODE = B.BATCH_CODE    AND t.comp_code = '" + oUserLoginDetail.COMP_CODE + "'  AND t.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "'  ORDER BY   PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery OR PO_NUMB LIKE :SearchQuery)";
            string WhereClause = " ";
            string SortExpression = " order by PI_NO";
            string SearchQuery = text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DDLPiNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string sCombine = DDLPiNo.SelectedValue.Trim();
            string[] string_array = sCombine.Split('@');
           // if (string_array[15].ToString() == "1" && string_array[16].ToString() == "1" || string_array[15].ToString() == "0" && string_array[16].ToString() == "0")
           // {
                txtPANo.Text = string_array[0];
                txtYarnCode.Text = string_array[1];
                txtDESC.Text = string_array[2];
                if (string.IsNullOrEmpty(string_array[3]))
                    txtShade.Text = "N/A";
                else
                    txtShade.Text = string_array[3];
                if (string.IsNullOrEmpty(string_array[4]))
                    txtShadeFamily.Text = "N/A";
                else
                    txtShadeFamily.Text = string_array[4];
                txtUNIT.Text = string_array[5];
                string BaseUOM = string_array[6];
                txtunitweight.Text = string_array[7];
                double MaxQty = double.Parse(string_array[8]);
                txtBalQty.Text = MaxQty.ToString();
                string PANO = txtPANo.Text;
                Business_type = string_array[11];
                txtLotNo.Text = string_array[12];
                txtPONumb.Text = string_array[13];
                txtMacCode.Text = string_array[14];

                if (Business_type == "JW")
                {
                    txtPartyCode.SelectedText = (string_array[9]);
                    txtPartyCode.SelectedValue = (string_array[9]);
                    ComboBoxItem item1 = new ComboBoxItem(string_array[9]);

                    string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                    string whereClause = string.Empty;

                    string SortExpression = " order by PRTY_CODE";
                    string SearchQuery = "%";
                    DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
                    txtPartyCode.DataSource = dt;
                    txtPartyCode.DataTextField = "PRTY_CODE";
                    txtPartyCode.DataValueField = "PRTY_NAME";
                    txtPartyCode.DataBind();

                    int x = -1;
                    foreach (ComboBoxItem it in txtPartyCode.Items)
                    {
                        if (it.Text == item1.Text)
                        {
                            txtPartyCode.SelectedIndex = x;
                            txtPartyCode.SelectedText = it.Text;
                            txtPartyCode.SelectedValue = it.Value;
                            lblPartyCode.Text = it.Text;
                            txtPartyAddress.Text = it.Value;
                            break;
                        }
                        x++;
                    }
                }
                else
                {
                    ComboBoxItem item1 = new ComboBoxItem("SELF");
                    string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                    string whereClause = string.Empty;
                    string SortExpression = " order by PRTY_CODE";
                    string SearchQuery = "%";
                    DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
                    txtPartyCode.DataSource = dt;
                    txtPartyCode.DataTextField = "PRTY_CODE";
                    txtPartyCode.DataValueField = "PRTY_NAME";
                    txtPartyCode.DataBind();
                    int x = -1;
                    foreach (ComboBoxItem it in txtPartyCode.Items)
                    {
                        if (it.Text == item1.Text)
                        {
                            txtPartyCode.SelectedIndex = x;
                            txtPartyCode.SelectedText = it.Text;
                            txtPartyCode.SelectedValue = it.Value;
                            lblPartyCode.Text = it.Text;
                            txtPartyAddress.Text = it.Value;
                            break;
                        }
                        x++;
                    }
                }

                DataTable dataMac = GetMachineData("");
                ddlMacCode.DataSource = dataMac;
                ddlMacCode.DataTextField = "MACHINE_CODE";
                ddlMacCode.DataValueField = "MACHINE_CODE";
                ddlMacCode.DataBind();

                foreach (Obout.ComboBox.ComboBoxItem item in ddlMacCode.Items)
                {
                    if (item.Text == string_array[14].ToString().Trim())
                    {
                        ddlMacCode.SelectedText = item.Text.ToString();
                        ddlMacCode.SelectedValue = item.Value.ToString();
                        break;
                    }
                }
           // }
           // else
           // {
            //    Common.CommonFuction.ShowMessage("QC is Approved  Please perform the Hydro");

           // }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PI Number selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetMachineData(string Text)
    {
        try
        {
            string CommandText = " SELECT distinct * FROM (SELECT MC.MACHINE_CODE,MC.OLD_MACHINE_NAME FROM MC_MACHINE_MASTER MC, TX_MAC_PROC_MST PC WHERE MC.MACHINE_GROUP= PC.MAC_CODE AND PC.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PC.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PC.MAIN_PROCESS = 'DYEING') ASD ";
            string WhereClause = "  where MACHINE_CODE like :SearchQuery";
            string SortExpression = " order by MACHINE_CODE asc";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlMacCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMacData(e.Text.ToUpper(), e.ItemsOffset);
            ddlMacCode.Items.Clear();
            ddlMacCode.DataSource = data;
            ddlMacCode.DataTextField = "MACHINE_CODE";
            ddlMacCode.DataValueField = "MACHINE_CODE";
            ddlMacCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetMacCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for Machine Code.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected DataTable GetMacData(string text, int startoffset)
    {
        try
        {
            DataTable dt = new DataTable();
            string commandText = string.Empty;
            if (startoffset == 0)
            {
                commandText += "SELECT 'MISC' AS MACHINE_CODE, 'MISC' AS MACHINE_GROUP, 0 AS MACHINE_CAPACITY, 'MISC' AS MACHINE_SEGMENT, 'MISC' AS MACHINE_TYPE, 'MISC' AS MACHINE_SEC,0 AS QTY,0 AS CONS,TO_DATE (NULL, 'dd / mm / yyyy') AS PLANNING_DATE,'MISC' AS PI_NO FROM DUAL UNION ALL ";
            } commandText += " SELECT distinct * FROM (  SELECT   DISTINCT V.MACHINE_CODE,V.MACHINE_GROUP, V.MACHINE_CAPACITY,  V.MACHINE_SEGMENT,  V.MACHINE_TYPE, V.MACHINE_SEC,  M.QTY,  M.CONS,TO_CHAR(M.PLANNING_DATE,'dd/Mon/yyyy')PLANNING_DATE, M.PI_NO  FROM   v_MC_MACHINE_MASTER V, OD_CAPT_TRN_MACHINE_PLAN M      WHERE  V.MACHINE_CODE = M.MACHINE_CODE  AND V.COMP_CODE = M.COMP_CODE AND V.BRANCH_CODE = M.BRANCH_CODE  AND M.PI_NO = '" + txtPANo.Text.Trim() + "'  AND M.YEAR = 2018  ORDER BY   V.MACHINE_CODE ASC) WHERE   MACHINE_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery         OR PLANNING_DATE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery  OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery AND ROWNUM <= 15 ";

            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += " AND MACHINE_CODE NOT IN(SELECT distinct MACHINE_CODE FROM (SELECT   DISTINCT V.MACHINE_CODE,V.MACHINE_GROUP, V.MACHINE_CAPACITY,  V.MACHINE_SEGMENT,  V.MACHINE_TYPE, V.MACHINE_SEC,  M.QTY,  M.CONS, TO_CHAR(M.PLANNING_DATE,'dd/Mon/yyyy')PLANNING_DATE, M.PI_NO  FROM   v_MC_MACHINE_MASTER V, OD_CAPT_TRN_MACHINE_PLAN M      WHERE  V.MACHINE_CODE = M.MACHINE_CODE  AND V.COMP_CODE = M.COMP_CODE AND V.BRANCH_CODE = M.BRANCH_CODE  AND M.PI_NO = '" + txtPANo.Text.Trim() + "'  AND M.YEAR = 2018  ORDER BY   V.MACHINE_CODE ASC)WHERE MACHINE_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR PLANNING_DATE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery  OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery AND ROWNUM <= '" + startoffset + "')";
            }
            string SortExpression = " order by MACHINE_CODE asc";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected int GetMacCount(string text)
    {
        try
        {
            DataTable dt = new DataTable();

            string whereClause = " ";
            string sortExpression = " ";
            string commandText = "SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC)  ";

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlMacCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        txtMacCode.Text = ddlMacCode.SelectedText.Trim();
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
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chkLot_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLot.Checked == true)
        {
            ddlLotNo.Visible = true;
            TxtLotIdNo.Visible = false;
        }
        else
        {
            ddlLotNo.Visible = false;
            TxtLotIdNo.Visible = true;
            TxtLotIdNo.Text = string.Empty;
            ddlLotNo.SelectedIndex = -1;
        }
    }

    protected void ddlLotNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtLotIdNo.Text = ddlLotNo.SelectedValue;
    }
    private void BindYarnLotMaster()
    {
        try
        {
            ddlLotNo.Items.Clear();
            DataTable _objdt = SaitexDL.Interface.Method.YRN_LOT_MAKING.SearchLotNoFromYarnLotMaster("LOT_NO", "", "TWISTING");
            if (_objdt != null && _objdt.Rows.Count > 0)
            {
                ddlLotNo.DataSource = _objdt;
                ddlLotNo.DataTextField = "LOT_NO";
                ddlLotNo.DataValueField = "LOT_NO";
                ddlLotNo.DataBind();
            }
            ddlLotNo.Items.Insert(0, new ListItem("--Select--", ""));
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
            txtPartyCode.Items.Clear();
            DataTable data = GetLOVForParty(e.Text.ToUpper(), e.ItemsOffset);

            txtPartyCode.DataSource = data;
            txtPartyCode.DataTextField = "PRTY_CODE";
            txtPartyCode.DataValueField = "PRTY_NAME";
            txtPartyCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Loading..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetLOVForParty(string Text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
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
    protected int GetPartyCount(string text)
    {
        try
        {
            string CommandText = " SELECT   PRTY_CODE   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
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
    protected void txtPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblPartyCode.Text = txtPartyCode.SelectedText.ToString().Trim();
            txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }


    protected void txtYarnCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            cmbYarnCode.Items.Clear();
            DataTable data = GetLOVForYarn(e.Text.ToUpper(), e.ItemsOffset);

            cmbYarnCode.DataSource = data;
            cmbYarnCode.DataTextField = "YARN_CODE";
            cmbYarnCode.DataValueField = "YARN_DESC";
            cmbYarnCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn Loading..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetLOVForYarn(string Text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT   M.YARN_CODE, M.YARN_DESC, A.ASS_YARN_DESC  FROM   YRN_MST M, YRN_ASSOCATED_MST A WHERE   NVL (M.DEL_STATUS, 0) = 0 AND M.YARN_CODE = A.YARN_CODE  AND (   UPPER (RTRIM (LTRIM (M.YARN_CODE))) LIKE :SearchQuery  OR UPPER (RTRIM (LTRIM (M.YARN_DESC))) LIKE :SearchQuery  OR UPPER (RTRIM (LTRIM (A.ASS_YARN_DESC))) LIKE :SearchQuery)  AND ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND M.YARN_CODE NOT IN (SELECT   M.YARN_CODE  FROM   YRN_MST M, YRN_ASSOCATED_MST A  WHERE   NVL (M.DEL_STATUS, 0) = 0  AND M.YARN_CODE = A.YARN_CODE    AND (UPPER (RTRIM (LTRIM (M.YARN_CODE))) LIKE   :SearchQuery   OR UPPER (RTRIM (LTRIM (M.YARN_DESC))) LIKE   :SearchQuery   OR UPPER (RTRIM (LTRIM (A.ASS_YARN_DESC))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by M.YARN_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected int GetYarnCount(string text)
    {
        try
        {
            string CommandText = " SELECT   M.YARN_CODE, M.YARN_DESC, A.ASS_YARN_DESC  FROM   YRN_MST M, YRN_ASSOCATED_MST A WHERE   NVL (M.DEL_STATUS, 0) = 0 AND M.YARN_CODE = A.YARN_CODE  AND (   UPPER (RTRIM (LTRIM (M.YARN_CODE))) LIKE :SearchQuery  OR UPPER (RTRIM (LTRIM (M.YARN_DESC))) LIKE :SearchQuery  OR UPPER (RTRIM (LTRIM (A.ASS_YARN_DESC))) LIKE :SearchQuery) ";
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


    protected void txtYarnCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblYarnCode.Text = cmbYarnCode.SelectedText.ToString().Trim();
            lblYarnDesc.Text = cmbYarnCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }


    protected void txtnoofunit_TextChanged(object sender, EventArgs e)
    {

    }
}
