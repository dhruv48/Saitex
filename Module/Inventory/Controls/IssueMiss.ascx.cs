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

public partial class Module_Inventory_Controls_IssueMiss : System.Web.UI.UserControl
{
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    private DataTable dtDetailTBL = null;
    private  string TRN_TYPE = "IMS02";

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
            TRN_TYPE = "IMS02";
            Session["dtItemReceipt"] = null;

            BindDropDown(ddlFromLocation);
            BindDepartment(ddlFromStore);        
            BindDropDown(ddlToLocation);
            BindDepartment(ddlToStore);
               

            ActivateSaveMode();
            BindShift();
            BindDepartment();
            BindCostCode();
            Blankrecords();
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
            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.TRN_TYPE = TRN_TYPE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            txtChallanNumber.Text = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetNewTRNNumber(oTX_ITEM_IR_MST).ToString();
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
            DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            {
                ddl.DataSource = dtDepartment;
                ddl.DataTextField = "DEPT_NAME";
                ddl.DataValueField = "DEPT_NAME";
                ddl.DataBind();
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            txtChallanNumber.Text = string.Empty;
            txtDocDate.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            txtIssueDate.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;

            ddlReprocess.SelectedIndex = 0;

            ddlIssueShift.SelectedIndex = 0;
            txtDepartment.SelectedIndex = 0;

            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();

            grdMaterialItemIssue.DataSource = null;
            grdMaterialItemIssue.DataBind();

            lblMode.Text = "You are in Save Mode";

            txtChallanNumber.ReadOnly = true;
            txtDepartment.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;

            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            
            ddlToLocation.SelectedIndex = ddlToLocation.Items.IndexOf(ddlToLocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
            ddlToStore.SelectedIndex = ddlToStore.Items.IndexOf(ddlToStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
            ddlFromLocation.SelectedIndex = ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
            ddlFromStore.SelectedIndex = ddlFromStore.Items.IndexOf(ddlFromStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));


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
            }
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

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtChallanNumber.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();
                txtDepartment.SelectedValue = dt.Rows[0]["DEPT_CODE"].ToString().Trim();
                txtIssueDate.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                txtDocDate.Text = dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim();
                txtDocNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                ddlIssueShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();
                ddlReprocess.Text = dt.Rows[0]["REPROCESS"].ToString().Trim();

                ddlFromLocation.SelectedIndex = ddlFromLocation.Items.IndexOf(ddlFromLocation.Items.FindByText(dt.Rows[0]["LOCATION"].ToString()));
                ddlFromStore.SelectedIndex = ddlFromStore.Items.IndexOf(ddlFromStore.Items.FindByText(dt.Rows[0]["STORE"].ToString()));
                ddlToLocation.SelectedIndex = ddlToLocation.Items.IndexOf(ddlToLocation.Items.FindByText(dt.Rows[0]["TO_LOCATION"].ToString()));
                ddlToStore.SelectedIndex = ddlToStore.Items.IndexOf(ddlToStore.Items.FindByText(dt.Rows[0]["TO_STORE"].ToString()));
                
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

                dtReceiptAdjustment.Rows[iLoop]["PI_NO"] = "NA";
                dtReceiptAdjustment.Rows[iLoop]["ISS_PI_NO"] = "NA";

            }
        }
        catch
        {
            throw;
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
                dtDetailTBL.Rows[iLoop]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
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

            if (txtDepartment.SelectedIndex > 0 || txtDepartment.SelectedValue.Trim() != "Select")
            {
                count += 1;
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

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdMaterialItemIssue.Rows)
            {
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (txtICODE.Text.Trim() == ItemCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
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
            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.DEPT_CODE = txtDepartment.SelectedValue.Trim();
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
            oTX_ITEM_IR_MST.PRTY_CODE = "NA";
            oTX_ITEM_IR_MST.PRTY_NAME = string.Empty;
            oTX_ITEM_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_ITEM_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
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

            oTX_ITEM_IR_MST.LOCATION = ddlFromLocation.SelectedItem.ToString();
            oTX_ITEM_IR_MST.STORE = ddlFromStore.SelectedItem.ToString();
            
            oTX_ITEM_IR_MST.TO_LOCATION = ddlToLocation.SelectedItem.ToString();
            oTX_ITEM_IR_MST.TO_STORE = ddlToStore.SelectedItem.ToString();


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
        catch
        {
            throw;
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
            oTX_ITEM_IR_MST.DEPT_CODE = txtDepartment.SelectedValue.Trim();
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
            oTX_ITEM_IR_MST.PRTY_CODE = "NA";
            oTX_ITEM_IR_MST.PRTY_NAME = string.Empty;
            oTX_ITEM_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_ITEM_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
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
            oTX_ITEM_IR_MST.LOCATION = ddlFromLocation.SelectedItem.ToString();
            oTX_ITEM_IR_MST.STORE = ddlFromStore.SelectedItem.ToString();
           
            oTX_ITEM_IR_MST.TO_LOCATION = ddlToLocation.SelectedItem.ToString();
            oTX_ITEM_IR_MST.TO_STORE = ddlToStore.SelectedItem.ToString();
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
                                dv[0]["PO_NUMB"] = 999998;
                                dv[0]["PO_TYPE"] = "MII";
                                dv[0]["PO_COMP_CODE"] = "C99999";
                                dv[0]["PO_BRANCH"] = "B99999";
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
                                dv[0]["PI_NO"] = string.Empty;
                                dv[0]["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dv[0]["DATE_OF_MFG"] = dd;
                                dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;

                                double QtyUnit = 0;
                                double.TryParse(txtQtyUnit.Text, out QtyUnit);
                                dv[0]["NO_OF_UNIT"] = QtyUnit;

                                dv[0]["UOM_OF_UNIT"] = txtUNIT.Text;

                                double Unitweight = 0;
                                double.TryParse(txtQtyWeight.Text, out Unitweight);
                                dv[0]["WEIGHT_OF_UNIT"] = Unitweight;
                                dv[0]["PI_NO"] = "NA";

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
                            dr["PI_NO"] = string.Empty;
                            dr["QCFLAG"] = "No";
                            DateTime dd = System.DateTime.Now;
                            dr["DATE_OF_MFG"] = dd;
                            dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;

                            double QtyUnit = 0;
                            double.TryParse(txtQtyUnit.Text, out QtyUnit);
                            dr["NO_OF_UNIT"] = QtyUnit;

                            dr["UOM_OF_UNIT"] = txtUNIT.Text;

                            double Unitweight = 0;
                            double.TryParse(txtQtyWeight.Text, out Unitweight);
                            dr["WEIGHT_OF_UNIT"] = Unitweight;
                            dr["PI_NO"] = "NA";
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

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text.ToUpper(), e.ItemsOffset);
            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "ITEM_CODE";
            txtICODE.DataValueField = "ITEM_CODE";
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
            string commandText = "SELECT * FROM (SELECT  asd.COMP_CODE, asd.BRANCH_CODE, asd.ITEM_CODE, asd.item_desc, ASD.PI_NO, asd.TRN_QTY, asd.TRN_QTY_ADJ, asd.REMQTY FROM (SELECT a.COMP_CODE, a.BRANCH_CODE, a.ITEM_CODE, a.item_desc, A.PI_NO, SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY, SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ, SUM (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_ITEM_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.conf_flag = '1' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND  A.LOCATION='"+ddlFromLocation.SelectedItem.ToString()+"' AND  A.STORE='"+ddlFromStore.SelectedItem.ToString()+"'  AND (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) > 0 GROUP BY a.COMP_CODE, a.BRANCH_CODE, a.ITEM_CODE, a.item_desc, A.PI_NO) asd WHERE LTRIM (RTRIM (ITEM_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (ITEM_DESC)) LIKE :SearchQuery ORDER BY ITEM_CODE ASC) ss WHERE ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += "  AND ( COMP_CODE, BRANCH_CODE, ITEM_CODE, item_desc, PI_NO) NOT IN(SELECT COMP_CODE,BRANCH_CODE,ITEM_CODE,item_desc,PI_NO FROM (SELECT asd.COMP_CODE,asd.BRANCH_CODE,asd.ITEM_CODE,asd.item_desc,ASD.PI_NO,asd.TRN_QTY,asd.TRN_QTY_ADJ,asd.REMQTY FROM (SELECT a.COMP_CODE,a.BRANCH_CODE,a.ITEM_CODE,a.item_desc,A.PI_NO, SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY, SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ, SUM(NVL (a.TRN_QTY, 0)- NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_ITEM_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.conf_flag = '1' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND  A.LOCATION='" + ddlFromLocation.SelectedItem.ToString() + "' AND  A.STORE='" + ddlFromStore.SelectedItem.ToString() + "' AND (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) >0 GROUP BY a.COMP_CODE,a.BRANCH_CODE,a.ITEM_CODE,a.item_desc,A.PI_NO) asd WHERE LTRIM (RTRIM (ITEM_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (ITEM_DESC)) LIKE :SearchQuery ORDER BY ITEM_CODE ASC) ss WHERE ROWNUM <= '" + startoffset + "')";
            }

            string SortExpression = " order by ITEM_CODE asc";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPODataCount(string text)
    {
        try
        {
            DataTable dt = new DataTable();

            string whereClause = " ";
            string sortExpression = " ";
            string commandText = "SELECT * FROM (SELECT asd.COMP_CODE, asd.BRANCH_CODE, asd.ITEM_CODE, asd.item_desc, ASD.PI_NO, asd.TRN_QTY, asd.TRN_QTY_ADJ, asd.REMQTY FROM (SELECT a.COMP_CODE, a.BRANCH_CODE, a.ITEM_CODE, a.item_desc, A.PI_NO, SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY, SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ, SUM (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_ITEM_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.conf_flag = '1' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) > 0 GROUP BY a.COMP_CODE, a.BRANCH_CODE, a.ITEM_CODE, a.item_desc, A.PI_NO) asd WHERE LTRIM (RTRIM (ITEM_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (ITEM_DESC)) LIKE :SearchQuery ORDER BY ITEM_CODE ASC) ss ";

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt.Rows.Count;
        }
        catch
        {
            throw;
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

    private void GetDataForDetail(string Item_Code)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(Item_Code, UNIQUEID))
            {
                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemDetailByItemCode(Item_Code, oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.CH_BRANCHCODE,ddlFromStore.SelectedItem.ToString(),ddlFromLocation.SelectedItem.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtItemCode.Text = dt.Rows[0]["ITEM_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["ITEM_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtQTY.Text = "0";
                    txtBasicRate.Text = "0";
                    txtAmount.Text = "0";
                    btnAdjRec.Focus();
                }
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Item Already Included");
            }
        }
        catch
        {
            throw;
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
                txtICODE.Enabled = false;
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
            URL = URL + "&STORE=" + ddlFromStore.SelectedItem.ToString();
            URL = URL + "&LOCATION=" + ddlFromLocation.SelectedItem.ToString();
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
            e.ItemsCount = GetIssueCount(e.Text.ToUpper());

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
            DataTable dt = new DataTable();
            string commandText = "SELECT * FROM ( SELECT * FROM (SELECT a.TRN_NUMB, TO_CHAR (a.TRN_DATE, 'DD/MM/YYYY') AS TRN_DATE, a.PRTY_CODE, b.PRTY_NAME, c.DEPT_NAME FROM TX_ITEM_IR_MST a, tx_vendor_mst b, cm_dept_mst c WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.DEPT_CODE = C.DEPT_CODE AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) where rownum<=15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and trn_numb not in ( SELECT TRN_NUMB FROM ( SELECT * FROM (SELECT a.TRN_NUMB, TO_CHAR (a.TRN_DATE, 'DD/MM/YYYY') AS TRN_DATE, a.PRTY_CODE, b.PRTY_NAME, c.DEPT_NAME FROM TX_ITEM_IR_MST a, tx_vendor_mst b, cm_dept_mst c WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.DEPT_CODE = C.DEPT_CODE AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) where rownum<='" + startOffset + "')";
            }

            string SortExpression = " ORDER BY TRN_NUMB DESC, TRN_DATE DESC";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetIssueCount(string text)
    {
        try
        {
            string whereClause = " ";
            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";
            string commandText = "SELECT * FROM ( SELECT * FROM (SELECT a.TRN_NUMB, TO_CHAR (a.TRN_DATE, 'DD/MM/YYYY') AS TRN_DATE, a.PRTY_CODE, b.PRTY_NAME, c.DEPT_NAME FROM TX_ITEM_IR_MST a, tx_vendor_mst b, cm_dept_mst c WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.DEPT_CODE = C.DEPT_CODE AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) ";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt.Rows.Count;
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

    protected void grdMaterialItemIssue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            GridView grdRecAdj = (GridView)e.Row.FindControl("grdRecAdj");
            Label txtICODE = (Label)e.Row.FindControl("txtICODE");

            if (dtItemReceipt != null)
            {
                DataView dv = new DataView(dtItemReceipt);
                dv.RowFilter = "ITEM_CODE='" + txtICODE.Text.Trim() + "'";
                if (dv.Count > 0)
                {
                    if (grdRecAdj != null)
                    {
                        grdRecAdj.DataSource = dv;
                        grdRecAdj.DataBind();
                    }
                }
            }

        }
    }
}