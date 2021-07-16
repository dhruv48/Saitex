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

public partial class Module_Fabric_FabricSaleWork_Controls_Fabric_Issue_Against_PI : System.Web.UI.UserControl
{
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private static string TRN_TYPE = string.Empty;

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
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdSave.Visible = true;
            TRN_TYPE = "IFS01";
            Session["dtItemReceipt"] = null;
            ActivateSaveMode();
            BindShift();
            BindDepartment();
            Blankrecords();
            BindCostCode();
            BindNewChallanNum();
            CreateDataTable();
            txtIssueDate.Text = System.DateTime.Now.ToShortDateString();
            RefreshDetailRow();
        }
        catch
        {
            throw;
        }
    }

    private void BindNewChallanNum()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.TRN_TYPE = TRN_TYPE;
            oTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtChallanNumber.Text = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetNewTRNNumber(oTX_FABRIC_IR_MST).ToString();
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
            txtChallanNumber.Text = string.Empty;
            txtDocDate.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            TxtLotIdNo.Text = string.Empty;
            txtIssueDate.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            ddlReprocess.SelectedIndex = 0;
            ddlIssueShift.SelectedIndex = 0;
            txtDepartment.SelectedIndex = txtDepartment.Items.IndexOf(txtDepartment.Items.FindByValue("PPC"));
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
        }
        catch
        {
            throw;
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
        catch
        {
            throw;
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
            dtDetailTBL.Columns.Add("FABR_CODE", typeof(string));
            dtDetailTBL.Columns.Add("FABR_DESC", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
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

            grdMaterialItemIssue.DataSource = dtDetailTBL;
            grdMaterialItemIssue.DataBind();
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
                ddlIssueShift.DataSource = dtShift;
                ddlIssueShift.DataTextField = "SFT_NAME";
                ddlIssueShift.DataValueField = "SFT_NAME";
                ddlIssueShift.DataBind();
            }
        }
        catch
        {
            throw;
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
                txtDepartment.SelectedIndex = txtDepartment.Items.IndexOf(txtDepartment.Items.FindByValue("PPC"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindDyedLotNo()
    {
        try
        {
            TxtLotIdNo.Text = string.Empty;
        }
        catch
        {
            throw;
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
            imgbtnDelete.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to delete this record')");
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
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
            //DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
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
                TxtLotIdNo.Text = dt.Rows[0]["LOT_ID_NO"].ToString().Trim();
            }
            if (iRecordFound == 1)
            {
                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE);
                //dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                MapDataTable();
                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetIssueAdjByMst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE, ChallanNumber);
                    //DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueAdjByMst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE, ChallanNumber);
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

                //dtReceiptAdjustment.Rows[iLoop]["PI_NO"] = "NA";
                //dtReceiptAdjustment.Rows[iLoop]["ISS_PI_NO"] = "NA";
            }
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ActivateUpdateMode();
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
            Response.Redirect("~/Module/Inventory/Reports/ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
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
            //if (TxtLotIdNo.Text != string.Empty)
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
        catch
        {
            throw;
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

                if (lblPICode.Text.Trim() == txtPANo.Text.Trim() && txtICODE.Text.Trim() == yarnCode && txtShadeCode.Text == SHADE_CODE && UniqueId != iUniqueId)
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
        catch
        {
            throw;
        }
    }

    private void saveMaterialReceipt()
    {
        try
        {

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htIssue = new Hashtable();
            //SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;  // txtDepartment.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.FORM_NUMB = string.Empty;
            oTX_FABRIC_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;
            oTX_FABRIC_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oTX_FABRIC_IR_MST.GATE_NUMB = string.Empty;
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = string.Empty;
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = string.Empty;
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;

            oTX_FABRIC_IR_MST.LR_NUMB = string.Empty;

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = "NA";
            oTX_FABRIC_IR_MST.PRTY_NAME = string.Empty;
            oTX_FABRIC_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
            oTX_FABRIC_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oTX_FABRIC_IR_MST.TRN_DATE = dt;

            oTX_FABRIC_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oTX_FABRIC_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            oTX_FABRIC_IR_MST.TRSP_CODE = "NA";
            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_FABRIC_IR_MST.LOT_ID_NO = "NA"; //TxtLotIdNo.Text.Trim().ToUpper().ToString();
            oTX_FABRIC_IR_MST.FR_DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_FABRIC_IR_MST.TO_DEPT_CODE = txtDepartment.SelectedValue.Trim();
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            int TRN_NUMB = 0;
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.Insert(oTX_FABRIC_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue);
            //bool result = SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"Issue Number : " + TRN_NUMB + " saved successfully.");

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

    private void UpdateMaterialReceipt()
    {
        try
        {
            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            //SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();

            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_FABRIC_IR_MST.FORM_NUMB = string.Empty;
            oTX_FABRIC_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;
            oTX_FABRIC_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oTX_FABRIC_IR_MST.GATE_NUMB = string.Empty;
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = string.Empty;
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = string.Empty;
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;

            oTX_FABRIC_IR_MST.LR_NUMB = string.Empty;

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = "NA";
            oTX_FABRIC_IR_MST.PRTY_NAME = string.Empty;
            oTX_FABRIC_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
            oTX_FABRIC_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oTX_FABRIC_IR_MST.TRN_DATE = dt;

            oTX_FABRIC_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oTX_FABRIC_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            oTX_FABRIC_IR_MST.TRSP_CODE = "NA";
            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_FABRIC_IR_MST.LOT_ID_NO = "NA"; //TxtLotIdNo.Text.Trim().ToString();

            oTX_FABRIC_IR_MST.FR_DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_FABRIC_IR_MST.TO_DEPT_CODE = txtDepartment.SelectedValue.Trim();

            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.Update(oTX_FABRIC_IR_MST, dtDetailTBL, dtItemReceipt, htIssue);
           // bool result = SaitexBL.Interface.Method.YRN_IR_MST.Update(oYRN_IR_MST, dtDetailTBL, dtItemReceipt, htIssue);
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
                if (txtPANo.Text != "")
                {
                    if (txtQTY.Text != "" && txtBasicRate.Text != "")
                    {
                        int UNIQUEID = 0;
                        if (ViewState["UNIQUEID"] != null)
                            UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                        bool bb = SearchItemCodeInGrid(txtFabricCode.Text.Trim(), UNIQUEID);
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
                                        dv[0]["PO_NUMB"] = 999998;
                                        dv[0]["PO_TYPE"] = "MII";
                                        dv[0]["PO_COMP_CODE"] = "C99999";
                                        dv[0]["PO_BRANCH"] = "B99999";
                                        dv[0]["FABR_CODE"] = txtFabricCode.Text.Trim();
                                        dv[0]["FABR_DESC"] = txtDESC.Text.Trim();
                                        dv[0]["SHADE_CODE"] = txtShade.Text.Trim();
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
                                        dv[0]["UOM_OF_UNIT"] = "KG";
                                        dv[0]["WEIGHT_OF_UNIT"] = 1;
                                        dv[0]["NO_OF_UNIT"] = double.Parse(txtQTY.Text.Trim());

                                        dv[0]["PI_NO"] = txtPANo.Text.ToString();
                                        dtDetailTBL.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtDetailTBL.NewRow();
                                    dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                    dr["PO_NUMB"] = 999998;
                                    dr["PO_TYPE"] = "MII";
                                    dr["PO_COMP_CODE"] = "C99999";
                                    dr["PO_BRANCH"] = "B99999";
                                    dr["FABR_CODE"] = txtFabricCode.Text.Trim();
                                    dr["FABR_DESC"] = txtDESC.Text.Trim();
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
                                    dr["WEIGHT_OF_UNIT"] = 1;
                                    dr["NO_OF_UNIT"] = double.Parse(txtQTY.Text.Trim());

                                    dr["PI_NO"] = txtPANo.Text.ToString();
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
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please selct Qty by clicking on Adjustment button');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please select PO Number from Drop down First');", true);
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding fabric detail data.\r\nSee error log for detail."));
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
            txtMacCode.Text = "NA";
            txtDetRemarks.Text = string.Empty;
            txtPANo.Text = string.Empty;
            txtFabricCode.Text = string.Empty;
            txtShade.Text = string.Empty;
            txtBalQty.Text = string.Empty;
            txtunitweight.Text = string.Empty;
            txtnoofunit.Text = string.Empty;
            ViewState["UNIQUEID"] = null;
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
                txtFabricCode.Text = dv[0]["FABR_CODE"].ToString();
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();
                txtDESC.Text = dv[0]["FABR_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                ddlCostCode.SelectedIndex = ddlCostCode.Items.IndexOf(ddlCostCode.Items.FindByValue(dv[0]["COST_CODE"].ToString()));
                txtMacCode.Text = dv[0]["MAC_CODE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnAdjRec_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFabricCode.Text != "" || txtShade.Text != "")
            {
         
                string URL = "Tx_Farbric_Recipt_Adjustment.aspx";
                URL = URL + "?ItemCodeId=" + txtFabricCode.Text.Trim();
                URL = URL + "&SHADE_CODE=" + txtShade.Text.Trim();
                URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
                URL = URL + "&AmountId=" + txtAmount.ClientID;
                URL = URL + "&TRN_TYPE=" + TRN_TYPE;
                URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
                URL = URL + "&txtQtyUnit=" + txtnoofunit.ClientID;
                URL = URL + "&txtQtyUom=" + txtUNIT.ClientID;
                URL = URL + "&txtQtyWeight=" + txtunitweight.ClientID;
                URL = URL + "&UOM_OF_UNIT=" + txtUNIT.Text;
                URL = URL + "&MAX_QTY=" + txtBalQty.Text;
                URL = URL + "&PI_NO=" + txtPANo.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                txtQTY.ReadOnly = false;
                txtAmount.ReadOnly = false;
                txtBasicRate.ReadOnly = false;
                txtunitweight.ReadOnly = false;
                txtnoofunit.ReadOnly = false;

            }
            else
            {
                CommonFuction.ShowMessage("Please select Fabric Code to adjust Receipt.");
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
            txtnoofunit.ReadOnly = true;
            txtunitweight.ReadOnly = true;
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
            string whereClause = " where TRN_NUMB like :searchQuery or TRN_DATE like :searchQuery or prty_code like :searchQuery or prty_name like :searchQuery ) where rownum<=10 ";
            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";
            string commandText = "select * from (select * from (Select a.TRN_NUMB, a.TRN_DATE,a.PRTY_CODE,b.PRTY_NAME,c.DEPT_NAME from TX_FABRIC_IR_MST a, tx_vendor_mst b, cm_dept_mst c Where a.prty_code=b.prty_code (+) and A.DEPT_CODE=C.DEPT_CODE and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + TRN_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt;
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
        catch
        {
            throw;
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
        catch
        {
            throw;
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
            if (txtDepartment.SelectedItem.ToString().ToUpper().Equals("Warp", StringComparison.OrdinalIgnoreCase))
            {
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT PI_NO, BASE_ARTICAL_CODE, ARTICAL_DESC, base_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) QTY_REM, ( PI_NO|| '@'|| BASE_ARTICAL_CODE|| '@'|| ARTICAL_DESC|| '@'|| base_SHADE_CODE|| '@'|| UOM|| '@'|| UOM|| '@'|| 1|| '@'|| (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)))TRN_DATA FROM v_OD_CAPT_TRN_BOM WHERE UPPER (BASE_ARTICAL_TYPE) = 'FABRIC' AND UPPER (PRODUCT_TYPE) = 'FABRIC' AND UPPER (W_SIDE) = '" + txtDepartment.SelectedItem.ToString().ToUpper() + "' AND (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) > 0 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";

            }
            else
            {
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT PI_NO, BASE_ARTICAL_CODE, ARTICAL_DESC, base_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) QTY_REM, ( PI_NO|| '@'|| BASE_ARTICAL_CODE|| '@'|| ARTICAL_DESC|| '@'|| base_SHADE_CODE|| '@'|| UOM|| '@'|| UOM|| '@'|| 1|| '@'|| (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)))TRN_DATA FROM v_OD_CAPT_TRN_BOM WHERE UPPER (BASE_ARTICAL_TYPE) = 'FABRIC' AND UPPER (PRODUCT_TYPE) = 'FABRIC' AND (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) > 0 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";


            }
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                if (txtDepartment.SelectedItem.ToString().ToUpper().Equals("Warp", StringComparison.OrdinalIgnoreCase))
                {
                    //whereClause += " AND TRN_DATA NOT IN(SELECT TRN_DATA FROM (SELECT * FROM (SELECT PI_NO,BASE_ARTICAL_CODE,base_artical_desc,base_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) QTY_REM,( PI_NO || '@' || BASE_ARTICAL_CODE || '@' || base_artical_desc || '@' || base_SHADE_CODE || '@' || UOM || '@' || UOM || '@' || 1 || '@' || (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0))) TRN_DATA FROM v_OD_CAPT_TRN_BOM WHERE UPPER (BASE_ARTICAL_TYPE) = 'FABRIC' AND (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) > 0 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
                    whereClause += " AND TRN_DATA NOT IN(SELECT TRN_DATA FROM (SELECT * FROM (SELECT PI_NO,BASE_ARTICAL_CODE,ARTICAL_DESC,base_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) QTY_REM,( PI_NO || '@' || BASE_ARTICAL_CODE || '@' || ARTICAL_DESC || '@' || base_SHADE_CODE || '@' || UOM || '@' || UOM || '@' || 1 || '@' || (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0))) TRN_DATA FROM v_OD_CAPT_TRN_BOM WHERE UPPER (BASE_ARTICAL_TYPE) = 'FABRIC' AND UPPER (PRODUCT_TYPE) = 'FABRIC' AND (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) > 0 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
                }
                else
                {
                    //whereClause += " AND TRN_DATA NOT IN(SELECT TRN_DATA FROM (SELECT * FROM (SELECT PI_NO,BASE_ARTICAL_CODE,base_artical_desc,base_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) QTY_REM,( PI_NO || '@' || BASE_ARTICAL_CODE || '@' || base_artical_desc || '@' || base_SHADE_CODE || '@' || UOM || '@' || UOM || '@' || 1 || '@' || (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0))) TRN_DATA FROM v_OD_CAPT_TRN_BOM WHERE UPPER (BASE_ARTICAL_TYPE) = 'FABRIC' AND W_SIDE ='" + txtDepartment.SelectedItem.ToString() + "' AND (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) > 0 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR base_artical_desc LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
                    whereClause += " AND TRN_DATA NOT IN(SELECT TRN_DATA FROM (SELECT * FROM (SELECT PI_NO,BASE_ARTICAL_CODE,ARTICAL_DESC,base_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) QTY_REM,( PI_NO || '@' || BASE_ARTICAL_CODE || '@' || ARTICAL_DESC || '@' || base_SHADE_CODE || '@' || UOM || '@' || UOM || '@' || 1 || '@' || (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0))) TRN_DATA FROM v_OD_CAPT_TRN_BOM WHERE UPPER (BASE_ARTICAL_TYPE) = 'FABRIC' AND UPPER (PRODUCT_TYPE) = 'FABRIC' AND UPPER (W_SIDE) = '" + txtDepartment.SelectedItem.ToString().ToUpper() + "' AND (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) > 0 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
                }
            }

            string SortExpression = " order by PI_NO";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPADATACount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            if (txtDepartment.SelectedItem.ToString().ToUpper().Equals("Warp", StringComparison.OrdinalIgnoreCase))
            {
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT PI_NO, BASE_ARTICAL_CODE, ARTICLE_DESC, base_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) QTY_REM, ( PI_NO|| '@'|| BASE_ARTICAL_CODE|| '@'|| ARTICLE_DESC|| '@'|| base_SHADE_CODE|| '@'|| UOM|| '@'|| UOM|| '@'|| 1|| '@'|| (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)))TRN_DATA FROM v_OD_CAPT_TRN_BOM WHERE UPPER (BASE_ARTICAL_TYPE) = 'fabric' AND UPPER (PRODUCT_TYPE) = 'FABRIC' AND UPPER (W_SIDE) = '" + txtDepartment.SelectedItem.ToString().ToUpper() + "' AND (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) > 0 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery)";
            }
            else
            {
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT PI_NO, BASE_ARTICAL_CODE, ARTICLE_DESC, base_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) QTY_REM, ( PI_NO|| '@'|| BASE_ARTICAL_CODE|| '@'|| ARTICLE_DESC|| '@'|| base_SHADE_CODE|| '@'|| UOM|| '@'|| UOM|| '@'|| 1|| '@'|| (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)))TRN_DATA FROM v_OD_CAPT_TRN_BOM WHERE UPPER (BASE_ARTICAL_TYPE) = 'fabric' AND UPPER (PRODUCT_TYPE) = 'FABRIC' AND (NVL (REQ_QTY, 0) - NVL (ADJ_QTY, 0)) > 0 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PI_NO) WHERE pi_no LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR BASE_SHADE_CODE  LIKE :SearchQuery)";

            }
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void DDLPiNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string sCombine = DDLPiNo.SelectedValue.Trim();
            string[] string_array = sCombine.Split('@');
            txtPANo.Text = string_array[0];
            txtFabricCode.Text = string_array[1];
            txtDESC.Text = string_array[2];
            txtShade.Text = string_array[3];
            txtUNIT.Text = string_array[4];
            string BaseUOM = string_array[5];
            txtunitweight.Text = string_array[6];
            double MaxQty = double.Parse(string_array[7]);
            txtBalQty.Text = MaxQty.ToString();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PI Number selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                commandText += "SELECT 'MISC' AS MACHINE_CODE, 'MISC' AS MACHINE_GROUP, 0 AS MACHINE_CAPACITY, 'MISC' AS MACHINE_SEGMENT, 'MISC' AS MACHINE_TYPE, 'MISC' AS MACHINE_SEC FROM DUAL UNION ALL ";
            }
            commandText += " SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += " AND MACHINE_CODE NOT IN(SELECT MACHINE_CODE FROM (SELECT MACHINE_CODE,MACHINE_GROUP,MACHINE_CAPACITY,MACHINE_SEGMENT,MACHINE_TYPE,MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC)WHERE ROWNUM <= '" + startoffset + "')";
            }

            string SortExpression = " order by MACHINE_CODE asc";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch
        {
            throw;
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
        catch
        {
            throw;
        }
    }

    protected void ddlMacCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        txtMacCode.Text = ddlMacCode.SelectedText.Trim();
    }

}

