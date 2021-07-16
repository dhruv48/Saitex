using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using errorLog;
using Common;
using Obout.ComboBox;

public partial class Module_Inventory_Controls_ReturnAgstPO : System.Web.UI.UserControl
{
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    private DataTable dtDetailTBL = null;
    private string TRN_TYPE = "IMS03";

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
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdSave.Visible = true;
            TRN_TYPE = "IMS03";
            Session["dtItemReceipt"] = null;
            ActivateSaveMode();
            BindShift();
            BindCostCode();
            Blankrecords();
            BindNewChallanNum();
            CreateDataTable();
            txtIssueDate.Text = System.DateTime.Now.ToShortDateString();
            RefreshDetailRow();
        }
        catch ( Exception ex)
        {
            throw ex; 
        }
    }

    private void BindNewChallanNum()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.TRN_TYPE = TRN_TYPE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            txtChallanNumber.Text = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetNewTRNNumber(oTX_ITEM_IR_MST).ToString();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void Blankrecords()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            txtChallanNumber.Text = string.Empty;
            txtDocDate.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            txtIssueDate.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtPartyCode1.Text = string.Empty;
            txtPartyAddress.Text = string.Empty;
            ddlIssueShift.SelectedIndex = 0;
            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();
            grdMaterialItemIssue.DataSource = null;
            grdMaterialItemIssue.DataBind();
            lblMode.Text = "You are in Save Mode";
            txtChallanNumber.ReadOnly = true;
            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
            ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
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
            dtDetailTBL.Columns.Add("ITEM_CODE", typeof(string));
            dtDetailTBL.Columns.Add("ITEM_DESC", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
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
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---Select---", ""));
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if( dtDepartment !=null && dtDepartment.Rows.Count > 0 )
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch(Exception ex)
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

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtChallanNumber.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();
                txtIssueDate.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                txtDocDate.Text = dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim();
                txtDocNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                ddlIssueShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(dt.Rows[0]["LOCATION"].ToString()));
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByText(dt.Rows[0]["STORE"].ToString()));
                txtPartyCode.SelectedValue = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyCode1.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
            }
            if (iRecordFound == 1)
            {
                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE);
                MapDataTable(dtDetailTBL);


                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetIssueAdjustFrmMST(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE, ChallanNumber);
                    MapAdjustDataTable(dtReceiptAdjustment);
                    Session["dtItemReceipt"] = dtReceiptAdjustment;
                }
            }
            else
            {
                string msg = "Dear " + oUserLoginDetail.Username + " !! MRN already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
                txtChallanNumber.Text = string.Empty;
                ddlTRNNumber.Focus();
                lblMode.Text = "You are in Update Mode";
                ActivateUpdateMode();
                RefreshDetailRow();
                InitialisePage();
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
            dtReceiptAdjustment.Columns.Add("NO_OF_UNIT", typeof(double));
            dtReceiptAdjustment.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtReceiptAdjustment.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            if (!dtDetailTBL.Columns.Contains("PI_NO"))
                 dtReceiptAdjustment.Columns.Add("PI_NO", typeof(string));
            for (int iLoop = 0; iLoop < dtReceiptAdjustment.Rows.Count; iLoop++)
            {
                dtReceiptAdjustment.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtReceiptAdjustment.Rows[iLoop]["NO_OF_UNIT"] = 0f;
                dtReceiptAdjustment.Rows[iLoop]["UOM_OF_UNIT"] = string.Empty;
                dtReceiptAdjustment.Rows[iLoop]["WEIGHT_OF_UNIT"] = 0f;
                dtReceiptAdjustment.Rows[iLoop]["PI_NO"] = string.Empty;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MapDataTable(DataTable dtDetailTBL)
    {
        try
        {
            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));

            if (!dtDetailTBL.Columns.Contains("PI_NO"))
                dtDetailTBL.Columns.Add("PI_NO", typeof(string));

            if (!dtDetailTBL.Columns.Contains("PO_YEAR"))
                dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["PI_NO"] = "NA";
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

            int count = 0;
            msg = string.Empty;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (txtChallanNumber.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Issue Number required.\r\n";
            }

            if (txtPartyCode1.Text != "")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Party first.\r\n";
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

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdMaterialItemIssue.Rows)
            {
                Label txtPONUMB = (Label)grdRow.FindControl("txtPONUMB");
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (txtPO_NUMB.Text == txtPONUMB.Text && txtICODE.Text.Trim() == ItemCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetItemDetailByItemCode(string ItemCode, out string Description, out string UOM)
    {
        Description = "";
        UOM = "";
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemDetailByItemCode(ItemCode, oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.CH_BRANCHCODE,"","");

            if (dt != null && dt.Rows.Count > 0)
            {
                Description = dt.Rows[0]["ITEM_DESC"].ToString().Trim();
                UOM = dt.Rows[0]["UOM"].ToString().Trim();
            }
        }
        catch(Exception ex)
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
            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_ITEM_IR_MST.FORM_NUMB = string.Empty;
            oTX_ITEM_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;
            oTX_ITEM_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oTX_ITEM_IR_MST.GATE_NUMB = string.Empty;
            oTX_ITEM_IR_MST.GATE_OUT_NUMB = string.Empty;
            oTX_ITEM_IR_MST.GATE_PASS_TYPE = string.Empty;
            oTX_ITEM_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_ITEM_IR_MST.LR_DATE = dt;

            oTX_ITEM_IR_MST.LR_NUMB = string.Empty;

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_ITEM_IR_MST.PRTY_CH_DATE = dt;

            oTX_ITEM_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_ITEM_IR_MST.PRTY_CODE = txtPartyCode1.Text;
            oTX_ITEM_IR_MST.PRTY_NAME = txtPartyAddress.Text;
            oTX_ITEM_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_ITEM_IR_MST.REPROCESS = "NA";// CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
            oTX_ITEM_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oTX_ITEM_IR_MST.TRN_DATE = dt;

            oTX_ITEM_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oTX_ITEM_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            oTX_ITEM_IR_MST.TRSP_CODE = "NA";
            oTX_ITEM_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_ITEM_IR_MST.LOT_ID_NO = "NA";

            oTX_ITEM_IR_MST.BILL_NUMB = "0";
            oTX_ITEM_IR_MST.BILL_TYPE = "NA";
            oTX_ITEM_IR_MST.BILL_YEAR = 0;

            dt = System.DateTime.Now.Date;
            bool BILL_DATE = false;
            htIssue.Add("BILL_DATE", BILL_DATE);
            oTX_ITEM_IR_MST.BILL_DATE = dt;
            oTX_ITEM_IR_MST.TOTAL_AMOUNT = 0;
            oTX_ITEM_IR_MST.FINAL_AMOUNT = 0;
            oTX_ITEM_IR_MST.LOCATION = ddlLocation.SelectedItem.ToString();
            oTX_ITEM_IR_MST.STORE = ddlStore.SelectedItem.ToString();
            oTX_ITEM_IR_MST.TO_LOCATION = "";
            oTX_ITEM_IR_MST.TO_STORE = "";

            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            int TRN_NUMB = 0;
            bool result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.Insert(oTX_ITEM_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue);
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
        catch(Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateMaterialReceipt()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_ITEM_IR_MST.FORM_NUMB = string.Empty;
            oTX_ITEM_IR_MST.FORM_TYPE = string.Empty;
            DateTime dt = System.DateTime.Now.Date;
            oTX_ITEM_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);
            oTX_ITEM_IR_MST.GATE_NUMB = string.Empty;
            oTX_ITEM_IR_MST.GATE_OUT_NUMB = string.Empty;
            oTX_ITEM_IR_MST.GATE_PASS_TYPE = string.Empty;
            oTX_ITEM_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_ITEM_IR_MST.LR_DATE = dt;

            oTX_ITEM_IR_MST.LR_NUMB = string.Empty;

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_ITEM_IR_MST.PRTY_CH_DATE = dt;

            oTX_ITEM_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_ITEM_IR_MST.PRTY_CODE = txtPartyCode1.Text;
            oTX_ITEM_IR_MST.PRTY_NAME = txtPartyAddress.Text;
            oTX_ITEM_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_ITEM_IR_MST.REPROCESS = "NA";// CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
            oTX_ITEM_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oTX_ITEM_IR_MST.TRN_DATE = dt;

            oTX_ITEM_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oTX_ITEM_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            oTX_ITEM_IR_MST.TRSP_CODE = "NA";
            oTX_ITEM_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_ITEM_IR_MST.LOT_ID_NO = "NA";

            oTX_ITEM_IR_MST.BILL_NUMB = "0";
            oTX_ITEM_IR_MST.BILL_TYPE = "NA";
            oTX_ITEM_IR_MST.BILL_YEAR = 0;

            dt = System.DateTime.Now.Date;
            bool BILL_DATE = false;
            htIssue.Add("BILL_DATE", BILL_DATE);
            oTX_ITEM_IR_MST.BILL_DATE = dt;
            oTX_ITEM_IR_MST.TOTAL_AMOUNT = 0;
            oTX_ITEM_IR_MST.FINAL_AMOUNT = 0;
            oTX_ITEM_IR_MST.LOCATION = ddlLocation.SelectedItem.ToString();
            oTX_ITEM_IR_MST.STORE = ddlStore.SelectedItem.ToString();
            oTX_ITEM_IR_MST.TO_LOCATION = "";
            oTX_ITEM_IR_MST.TO_STORE = "";
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            bool result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.Update(oTX_ITEM_IR_MST, dtDetailTBL, dtItemReceipt, htIssue);
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

            if (txtItemCode.Text != "" && txtQTY.Text != "" && txtBasicRate.Text != "")
            {
                int UNIQUEID = 0;
                if (ViewState["UNIQUEID"] != null)
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                bool bb = SearchItemCodeInGrid(txtItemCode.Text.Trim(), UNIQUEID);
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
                                dv[0]["PO_NUMB"] = txtPO_NUMB.Text;
                                dv[0]["PO_TYPE"] = lblPO_TYPE.Text;
                                dv[0]["PO_COMP_CODE"] = lblPO_COMP.Text;
                                dv[0]["PO_BRANCH"] = lblPO_BRANCH.Text;
                                dv[0]["ITEM_CODE"] = txtItemCode.Text.Trim();
                                dv[0]["ITEM_DESC"] = txtDESC.Text.Trim();
                                dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dv[0]["UOM"] = txtUNIT.Text.Trim();
                                dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dv[0]["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dv[0]["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                dv[0]["COST_CODE"] = ddlCostCode.SelectedValue.Trim();
                                dv[0]["MAC_CODE"] = txtMacCode.Text.Trim();
                                dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                dv[0]["PI_NO"] = "NA";
                                dv[0]["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dv[0]["DATE_OF_MFG"] = dd;
                                dv[0]["PO_YEAR"] = lblPO_Year.Text;

                                double QtyUnit = 0;
                                double.TryParse(txtQtyUnit.Text, out QtyUnit);
                                dv[0]["NO_OF_UNIT"] = 1;

                                dv[0]["UOM_OF_UNIT"] = txtUNIT.Text;

                                double Unitweight = 0;
                                double.TryParse(txtQtyWeight.Text, out Unitweight);
                                dv[0]["WEIGHT_OF_UNIT"] = 1;

                                dtDetailTBL.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtDetailTBL.NewRow();
                            dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                            dr["PO_NUMB"] = txtPO_NUMB.Text;
                            dr["PO_TYPE"] = lblPO_TYPE.Text;
                            dr["PO_COMP_CODE"] = lblPO_COMP.Text;
                            dr["PO_BRANCH"] = lblPO_BRANCH.Text;
                            dr["ITEM_CODE"] = txtItemCode.Text.Trim();
                            dr["ITEM_DESC"] = txtDESC.Text.Trim();
                            dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                            dr["UOM"] = txtUNIT.Text.Trim();
                            dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                            dr["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                            dr["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                            dr["COST_CODE"] = ddlCostCode.SelectedValue.Trim();
                            dr["MAC_CODE"] = txtMacCode.Text.Trim();
                            dr["REMARKS"] = txtDetRemarks.Text.Trim();
                            dr["PI_NO"] = "NA";
                            dr["QCFLAG"] = "No";
                            DateTime dd = System.DateTime.Now;
                            dr["DATE_OF_MFG"] = dd;
                            dr["PO_YEAR"] = lblPO_Year.Text;

                            double QtyUnit = 0;
                            double.TryParse(txtQtyUnit.Text, out QtyUnit);
                            dr["NO_OF_UNIT"] = 1;

                            dr["UOM_OF_UNIT"] = txtUNIT.Text;

                            double Unitweight = 0;
                            double.TryParse(txtQtyWeight.Text, out Unitweight);
                            dr["WEIGHT_OF_UNIT"] = 1;
                            dtDetailTBL.Rows.Add(dr);
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
                }
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
            grdMaterialItemIssue.DataSource = dtDetailTBL;
            grdMaterialItemIssue.DataBind();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding Material detail data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            txtICODE.SelectedIndex = -1;
            ddlMacCode.SelectedIndex = -1;
            txtItemCode.Text = "";
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtAmount.Text = "";
            ddlCostCode.SelectedIndex = -1;
            txtMacCode.Text = "";
            txtDetRemarks.Text = "";
            txtICODE.Enabled = true;
            txtQtyUnit.Text = "";
            txtQtyWeight.Text = "";

            txtPO_NUMB.Text = string.Empty;
            lblPO_COMP.Text = string.Empty;
            lblPO_BRANCH.Text = string.Empty;
            lblPO_TYPE.Text = string.Empty;
            lblPO_Year.Text = string.Empty;

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

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text.ToUpper(), e.ItemsOffset);
            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "ITEM_CODE";
            txtICODE.DataValueField = "ITEM_DATA";
            txtICODE.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetPODataCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetPOData(string text, int startoffset)
    {
        try
        {
            DataTable dt = new DataTable();
            string commandText = "SELECT * FROM (SELECT asd.po_numb, asd.ITEM_CODE, asd.item_desc, ( asd.PO_YEAR|| '@'|| asd.PO_COMP_CODE|| '@'|| asd.PO_BRANCH|| '@'|| asd.PO_TYPE|| '@'|| asd.po_numb|| '@'|| asd.ITEM_CODE|| '@'|| asd.item_desc|| '@'|| asd.UOM) ITEM_DATA,TRN_QTY,TRN_QTY_ADJ,REMQTY FROM (SELECT A.PO_YEAR, A.PO_COMP_CODE, A.PO_BRANCH, A.PO_TYPE, a.po_numb, a.ITEM_CODE, a.item_desc, A.UOM, SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY, SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ, SUM (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_ITEM_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.conf_flag = '1' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.PRTY_CODE = '" + txtPartyCode1.Text + "' GROUP BY A.PO_YEAR, A.PO_COMP_CODE, A.PO_BRANCH, A.PO_TYPE, a.po_numb, a.ITEM_CODE, a.item_desc, A.UOM) asd WHERE LTRIM (RTRIM (ITEM_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (ITEM_DESC)) LIKE :SearchQuery ORDER BY po_numb ASC, ITEM_CODE ASC) WHERE REMQTY > 0 AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += "  AND ITEM_DATA NOT IN (SELECT ITEM_DATA FROM (SELECT asd.po_numb, asd.ITEM_CODE, asd.item_desc,( asd.PO_YEAR ||'@' || asd.PO_COMP_CODE || '@' || asd.PO_BRANCH || '@' || asd.PO_TYPE || '@' || asd.po_numb || '@' || asd.ITEM_CODE || '@' || asd.item_desc ||'@' || asd.UOM) ITEM_DATA,TRN_QTY,TRN_QTY_ADJ,REMQTY FROM (SELECT A.PO_YEAR,A.PO_COMP_CODE,A.PO_BRANCH,A.PO_TYPE,a.po_numb,a.ITEM_CODE,a.item_desc,A.UOM,SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY,SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ,SUM(NVL (a.TRN_QTY, 0)- NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_ITEM_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.conf_flag = '1' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.PRTY_CODE = '" + txtPartyCode1.Text + "' GROUP BY A.PO_YEAR, A.PO_COMP_CODE, A.PO_BRANCH, A.PO_TYPE, a.po_numb, a.ITEM_CODE, a.item_desc, A.UOM)asd WHERE LTRIM (RTRIM (ITEM_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (ITEM_DESC)) LIKE :SearchQuery ORDER BY po_numb ASC, ITEM_CODE ASC) WHERE REMQTY > 0 AND ROWNUM <= '" + startoffset + "')";
            }
            string SortExpression = " order by po_numb asc, ITEM_CODE asc";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int GetPODataCount(string text)
    {
        try
        {
            DataTable dt = new DataTable();

            string whereClause = " ";
            string sortExpression = " ";
            string commandText = "SELECT * FROM (SELECT asd.po_numb, asd.ITEM_CODE, asd.item_desc, ( asd.PO_YEAR|| '@'|| asd.PO_COMP_CODE|| '@'|| asd.PO_BRANCH|| '@'|| asd.PO_TYPE|| '@'|| asd.po_numb|| '@'|| asd.ITEM_CODE|| '@'|| asd.item_desc|| '@'|| asd.UOM) ITEM_DATA,TRN_QTY,TRN_QTY_ADJ,REMQTY FROM (SELECT A.PO_YEAR, A.PO_COMP_CODE, A.PO_BRANCH, A.PO_TYPE, a.po_numb, a.ITEM_CODE, a.item_desc, A.UOM, SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY, SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ, SUM (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_ITEM_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.conf_flag = '1' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.PRTY_CODE = '" + txtPartyCode1.Text + "' GROUP BY A.PO_YEAR, A.PO_COMP_CODE, A.PO_BRANCH, A.PO_TYPE, a.po_numb, a.ITEM_CODE, a.item_desc, A.UOM) asd WHERE LTRIM (RTRIM (ITEM_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (ITEM_DESC)) LIKE :SearchQuery ORDER BY po_numb ASC, ITEM_CODE ASC) WHERE REMQTY > 0 ";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
            return dt.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = txtICODE.SelectedValue.Trim();
            txtItemCode.Text = cString;
            GetDataForDetail(cString);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void GetDataForDetail(string sItemData)
    {
        try
        {
            string[] ItemData = sItemData.Split('@');

            int po_year = int.Parse(ItemData[0].ToString());
            string PO_CompCode = ItemData[1].ToString();
            string PO_BranchCode = ItemData[2].ToString();
            string PO_Type = ItemData[3].ToString();
            int po_Numb = int.Parse(ItemData[4].ToString());
            string ItemCode = ItemData[5].ToString();
            string ItemDesc = ItemData[6].ToString();
            string UOM = ItemData[7].ToString();

            txtPO_NUMB.Text = po_Numb.ToString();

            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(ItemCode, UNIQUEID))
            {
                txtItemCode.Text = ItemCode;
                txtDESC.Text = ItemDesc;
                txtUNIT.Text = UOM;
                lblPO_Year.Text = po_year.ToString();
                lblPO_COMP.Text = PO_CompCode;
                lblPO_BRANCH.Text = PO_BranchCode;
                lblPO_TYPE.Text = PO_Type;
                txtQTY.Text = "0";
                txtBasicRate.Text = "0";
                txtAmount.Text = "0";
                btnAdjRec.Focus();
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Item Already Included");
            }
        }
        catch (Exception ex)
        {
            throw ex;
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

                txtItemCode.Text = dv[0]["ITEM_CODE"].ToString();
                txtDESC.Text = dv[0]["ITEM_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                ddlCostCode.SelectedIndex = ddlCostCode.Items.IndexOf(ddlCostCode.Items.FindByValue(dv[0]["COST_CODE"].ToString()));
                txtMacCode.Text = dv[0]["MAC_CODE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                txtQtyUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtQtyWeight.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                lblPO_Year.Text = dv[0]["PO_YEAR"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                txtPO_NUMB.Text = dv[0]["PO_NUMB"].ToString();
                txtICODE.Enabled = false;
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
            string URL = "ReceiptAdjustmentForStockTransfer.aspx";
            URL = URL + "?ItemCodeId=" + txtItemCode.Text.Trim();
            URL = URL + "&TextBoxId=" + txtQTY.ClientID;
            URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
            URL = URL + "&AmountId=" + txtAmount.ClientID;
            URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
            URL = URL + "&TRN_TYPE=" + TRN_TYPE;
            URL = URL + "&txtQtyUnit=" + txtQtyUnit.ClientID;
            URL = URL + "&txtQtyUom=" + txtUNIT.ClientID;
            URL = URL + "&txtQtyWeight=" + txtQtyWeight.ClientID;
            URL = URL + "&LOCATION=" + ddlLocation.SelectedItem.ToString();
            URL = URL + "&STORE=" + ddlStore.SelectedItem.ToString();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);


            txtQTY.ReadOnly = false;
            txtAmount.ReadOnly = false;
            txtBasicRate.ReadOnly = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adjusting Receiving.\r\nSee error log for detail."));
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
        }
    }

    protected DataTable GetIssueData(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " where TRN_NUMB like :searchQuery or TRN_DATE like :searchQuery or prty_code like :searchQuery or prty_name like :searchQuery";
            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";
            string commandText = "select * from (Select a.TRN_NUMB, TO_CHAR (a.TRN_DATE, 'DD/MM/YYYY') AS TRN_DATE,a.PRTY_CODE,b.PRTY_NAME,c.DEPT_NAME from TX_ITEM_IR_MST a, tx_vendor_mst b, cm_dept_mst c Where a.prty_code=b.prty_code (+) and A.DEPT_CODE=C.DEPT_CODE and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + TRN_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";

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
            ddlTRNNumber.Visible = false;
            txtChallanNumber.Visible = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Challan Number.\r\nSee error log for detail."));

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

    protected void ddlMacCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMacData(e.Text.ToUpper(), e.ItemsOffset);
            ddlMacCode.Items.Clear();
            ddlMacCode.Items.Add(new ComboBoxItem("MISC", "MISC"));
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
            string commandText = "SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC) WHERE ROWNUM <= 15 ";
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

    protected void txtPartyCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);

            txtPartyCode.Items.Clear();

            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();

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
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
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

        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
            txtPartyCode1.Text = txtPartyCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

}