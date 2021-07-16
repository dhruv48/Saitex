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
using errorLog;
using Common;
using Obout.ComboBox;

public partial class Module_Inventory_Controls_FabricIssueMiss : System.Web.UI.UserControl
{
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtDetailTBL = null;
    private static string TRN_TYPE = string.Empty;

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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in loading page.\r\nSee error log for detail.");
        }
    }

    private void InitialisePage()
    {
        try
        {
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdSave.Visible = true;

            TRN_TYPE = "IFS02";

            Session["dtItemReceipt"] = null;

            ActivateSaveMode();
            BindShift();
            BindDepartment();
            Blankrecords();
            BindNewChallanNum();
            CreateDataTable();
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }

            txtIssueDate.Text = System.DateTime.Now.ToShortDateString();

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
            txtChallanNumber.Text = "";
            txtDocDate.Text = "";
            txtDocNo.Text = "";
            txtIssueDate.Text = "";
            txtRemarks.Text = "";
            txtVehicleNo.Text = "";

            ddlReprocess.SelectedIndex = 0;

            ddlIssueShift.SelectedIndex = 0;
            txtDepartment.SelectedIndex = 0;

            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();

            grdMaterialItemIssue.DataSource = null;
            grdMaterialItemIssue.DataBind();

            lblMode.Text = "Save";

            txtChallanNumber.ReadOnly = true;

            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                txtDepartment.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;
            }

            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            RefreshDetailRow();
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
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
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

    private void deleteItemIssueRow(int UniqueId)
    {
        try
        {
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
        catch (Exception Ex)
        {
            errorLog.ErrHandler.WriteError(Ex.Message);
            CommonFuction.ShowMessage(@"Problem in selection of Challan Number.\r\nSee error log for detail.");
        }
    }

    private int GetdataByChallaNumber(int ChallanNumber)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

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
            }
            if (iRecordFound == 1)
            {
                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE);

                if (dtDetailTBL.Rows.Count > 0)
                {
                    MapDataTable();
                    if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                    {
                        DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetIssueAdjByMst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE, ChallanNumber);
                        MapAdjustDataTable(dtReceiptAdjustment);
                        Session["dtItemReceipt"] = dtReceiptAdjustment;

                    }
                }
            }
            else
            {
                //string msg = "Dear " + oUserLoginDetail.Username + "!! MRN already approved. Modification not allowed.";
                //Common.CommonFuction.ShowMessage(msg);

                InitialisePage();

                txtChallanNumber.Text = "";
                ddlTRNNumber.Focus();

                lblMode.Text = "Update";

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

    private void MapDataTable()
    {
        try
        {
            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
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
            CommonFuction.ShowMessage(@"Problem in saving data.\r\nSee error log for detail.");
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ActivateUpdateMode();

            lblMode.Text = "Update";
            txtChallanNumber.Text = "";
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Updation mode.\r\nSee error log for detail.");
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
            CommonFuction.ShowMessage(@"Problem in updating data.\r\nSee error log for detail.");
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in saving data.\r\nSee error log for detail.");
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in clearing data.\r\nSee error log for detail.");
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Fabric/Reports/TX_FABRIC_ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in generating report data.\r\nSee error log for detail.");
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
            CommonFuction.ShowMessage(@"Problem in leaving page.\r\nSee error log for detail.");
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Editing/ deletion of Material detail.\r\nSee error log for detail.");
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            bool ReturnResult = false;

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

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId, string ShadeCode)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdMaterialItemIssue.Rows)
            {
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                Label txtSHADE_CODE = (Label)grdRow.FindControl("txtSHADE_CODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (txtICODE.Text.Trim() == ItemCode && txtSHADE_CODE.Text.Trim() == ShadeCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    private void GetItemDetailByItemCode(string FabrCode, out string Description, out string UOM)
    {
        Description = "";
        UOM = "";
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricDetailByFabricCode(FabrCode);

            if (dt != null && dt.Rows.Count > 0)
            {
                Description = dt.Rows[0]["FABR_DESC"].ToString().Trim();
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

            Amount = Rate * Qty;

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
            Hashtable htIssue = new Hashtable();

            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_MST.DEPT_CODE = txtDepartment.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.FORM_NUMB = "";
            oTX_FABRIC_IR_MST.FORM_TYPE = "";

            DateTime dt = System.DateTime.Now.Date;
            oTX_FABRIC_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oTX_FABRIC_IR_MST.GATE_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = "";
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;

            oTX_FABRIC_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = "NA";
            oTX_FABRIC_IR_MST.PRTY_NAME = "";
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
            oTX_FABRIC_IR_MST.LOT_ID_NO = "NA";
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            int TRN_NUMB = 0;
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.InsertFabricIssue(oTX_FABRIC_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue);
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

            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_MST.DEPT_CODE = txtDepartment.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.FORM_NUMB = "";
            oTX_FABRIC_IR_MST.FORM_TYPE = "";

            DateTime dt = System.DateTime.Now.Date;
            oTX_FABRIC_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oTX_FABRIC_IR_MST.GATE_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = "";
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;

            oTX_FABRIC_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = "NA";
            oTX_FABRIC_IR_MST.PRTY_NAME = "";
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
            oTX_FABRIC_IR_MST.LOT_ID_NO = "NA";
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.UpdateFabricIssue(oTX_FABRIC_IR_MST, dtDetailTBL, dtItemReceipt, htIssue);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Data updated Successfully");
            }
            else
            {
                CommonFuction.ShowMessage("Data updation Failed");
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
            if (dtDetailTBL == null)
                CreateDataTable();

            //if (dtDetailTBL.Rows.Count < 15)
            //{
            if (txtFabricCode.Text != "" && txtQTY.Text != "" && txtBasicRate.Text != "")
            {
                int UNIQUEID = 0;
                if (ViewState["UNIQUEID"] != null)
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                bool bb = SearchItemCodeInGrid(txtFabricCode.Text.Trim(), UNIQUEID, txtShade.Text.Trim());
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
                                dv[0]["AMOUNT"] = double.Parse(txtQTY.Text.Trim()) * double.Parse(txtBasicRate.Text.Trim());
                                dv[0]["COST_CODE"] = txtCostCode.Text.Trim();
                                dv[0]["MAC_CODE"] = txtMacCode.Text.Trim();
                                dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                dv[0]["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dv[0]["DATE_OF_MFG"] = dd;

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
                            dr["FABR_CODE"] = txtFabricCode.Text.Trim();
                            dr["FABR_DESC"] = txtDESC.Text.Trim();
                            dr["SHADE_CODE"] = txtShade.Text.Trim();
                            dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                            dr["UOM"] = txtUNIT.Text.Trim();
                            dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                            dr["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                            dr["AMOUNT"] = double.Parse(txtQTY.Text.Trim()) * double.Parse(txtBasicRate.Text.Trim());
                            dr["COST_CODE"] = txtCostCode.Text.Trim();
                            dr["MAC_CODE"] = txtMacCode.Text.Trim();
                            dr["REMARKS"] = txtDetRemarks.Text.Trim();
                            dr["QCFLAG"] = "No";
                            DateTime dd = System.DateTime.Now;

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
            grdMaterialItemIssue.DataSource = dtDetailTBL;
            grdMaterialItemIssue.DataBind();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in adding Fabric detail data.\r\nSee error log for detail.");
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            txtICODE.SelectedIndex = -1;
            txtICODE.Enabled = true;
            txtFabricCode.Text = "";
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtShade.Text = "";
            txtCostCode.Text = "";
            txtMacCode.Text = "";
            txtDetRemarks.Text = "";
            txtAmount.Text = "0";
            //lblPO_BRANCH.Text = "";
            //lblPO_COMP.Text = "";
            //lblPO_TYPE.Text = "";

            txtQtyUnit.Text = string.Empty;
            txtUNIT.Text = string.Empty;
            txtQtyWeight.Text = string.Empty;

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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in refresh detail information.\r\nSee error log for detail.");
        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text.ToUpper(), e.ItemsOffset);
            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "FABR_CODE";
            txtICODE.DataValueField = "SHADE_CODE";
            txtICODE.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetPOCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in getting data for material detail.\r\nSee error log for detail.");
        }
    }

    protected DataTable GetPOData(string text, int startOffset)
    {
        try
        {
            DataTable dt = new DataTable();
            string commandText = "SELECT * FROM (SELECT asd.YEAR, asd.COMP_CODE, asd.BRANCH_CODE, asd.FABR_DESC, asd.FABR_CODE, asd.SHADE_CODE, ASD.PI_NO, asd.TRN_QTY, asd.TRN_QTY_ADJ, asd.REMQTY FROM (SELECT a.YEAR, a.COMP_CODE, a.BRANCH_CODE, a.FABR_DESC, a.FABR_CODE, a.SHADE_CODE, A.PI_NO, SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY, SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ, SUM (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_FABRIC_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.conf_flag = '1' AND (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0))>0 GROUP BY a.YEAR, a.COMP_CODE, a.BRANCH_CODE, a.FABR_DESC, a.FABR_CODE, a.SHADE_CODE, A.PI_NO ORDER BY a.FABR_CODE ASC, a.SHADE_CODE ASC) asd WHERE (UPPER (asd.shade_code) LIKE :SearchQuery OR UPPER (FABR_CODE) LIKE :SearchQuery) ) ss WHERE ROWNUM <= 15 ";

            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND (YEAR,COMP_CODE,BRANCH_CODE,FABR_DESC,FABR_CODE,SHADE_CODE,PI_NO) NOT IN(SELECT YEAR,COMP_CODE,BRANCH_CODE,FABR_DESC,FABR_CODE,SHADE_CODE,PI_NO FROM (SELECT asd.YEAR, asd.COMP_CODE, asd.BRANCH_CODE, asd.FABR_DESC, asd.FABR_CODE, asd.SHADE_CODE, ASD.PI_NO, asd.TRN_QTY, asd.TRN_QTY_ADJ, asd.REMQTY FROM (SELECT a.YEAR,a.COMP_CODE,a.BRANCH_CODE,a.FABR_DESC,a.FABR_CODE,a.SHADE_CODE,A.PI_NO,SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY,SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ,SUM(NVL (a.TRN_QTY, 0)- NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_FABRIC_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.conf_flag = '1' AND (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0))>0 GROUP BY a.YEAR,a.COMP_CODE,a.BRANCH_CODE,a.FABR_DESC,a.FABR_CODE,a.SHADE_CODE,A.PI_NO ORDER BY a.FABR_CODE ASC, a.SHADE_CODE ASC) asd WHERE (UPPER (asd.shade_code) LIKE:SearchQuery OR UPPER (FABR_CODE) LIKE :SearchQuery)) ss WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY FABR_CODE ASC, SHADE_CODE ASC ";

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", text + "%", "");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPOCount(string text)
    {

        string CommandText = " SELECT * FROM (SELECT asd.YEAR, asd.COMP_CODE, asd.BRANCH_CODE, asd.FABR_DESC, asd.FABR_CODE, asd.SHADE_CODE, ASD.PI_NO, asd.TRN_QTY, asd.TRN_QTY_ADJ, asd.REMQTY FROM (SELECT a.YEAR, a.COMP_CODE, a.BRANCH_CODE, a.FABR_DESC, a.FABR_CODE, a.SHADE_CODE, A.PI_NO, SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY, SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ, SUM (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)) AS REMQTY FROM V_TX_FABRIC_IR_TRN a WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.conf_flag = '1' AND (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0))>0 GROUP BY a.YEAR, a.COMP_CODE, a.BRANCH_CODE, a.FABR_DESC, a.FABR_CODE, a.SHADE_CODE, A.PI_NO ORDER BY a.FABR_CODE ASC, a.SHADE_CODE ASC) asd WHERE (UPPER (asd.shade_code) LIKE :SearchQuery OR UPPER (FABR_CODE) LIKE :SearchQuery) ) ss  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = txtICODE.SelectedText.Trim();
            txtShade.Text = txtICODE.SelectedValue.Trim();
            GetDataForDetail(cString, txtShade.Text);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in selecting data.\r\nSee error log for detail.");
        }
    }

    private void GetDataForDetail(string FABR_CODE, string SHADE_CODE)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(FABR_CODE, UNIQUEID, SHADE_CODE))
            {
                DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricDetailByFabricCode(FABR_CODE);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtFabricCode.Text = dt.Rows[0]["FABR_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["FABR_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtQTY.Text = "0";
                    txtAmount.Text = "0";
                    txtBasicRate.Text = "0";
                    btnAdjRec.Focus();
                }
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Fabric Already Included");
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
            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                ViewState["UNIQUEID"] = UNIQUEID;

                txtFabricCode.Text = dv[0]["FABR_CODE"].ToString();
                txtICODE.Enabled = false;

                txtQtyUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtQtyWeight.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                txtDESC.Text = dv[0]["FABR_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();
                txtCostCode.Text = dv[0]["COST_CODE"].ToString();
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
            if (txtFabricCode.Text != "")
            {
                string URL = "FabricReceiptAdjustment.aspx";
                URL = URL + "?FABR_CODE=" + txtFabricCode.Text.Trim();
                URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                URL = URL + "&SHADE_CODE=" + txtShade.Text;
                URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
                URL = URL + "&TRN_TYPE=" + TRN_TYPE;
                URL = URL + "&AmountId=" + txtAmount.ClientID;
                URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
                URL = URL + "&txtQtyUnit=" + txtQtyUnit.ClientID;
                URL = URL + "&txtQtyUom=" + txtUNIT.ClientID;
                URL = URL + "&txtQtyWeight=" + txtQtyWeight.ClientID;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                txtQTY.ReadOnly = false;
                txtBasicRate.ReadOnly = false;
                txtQtyUnit.ReadOnly = false;
                txtUNIT.ReadOnly = false;
                txtAmount.ReadOnly = false;
                txtQtyWeight.ReadOnly = false;
            }
            else
            {

                CommonFuction.ShowMessage("Please select Fabric Code to adjust Receipt.");
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in adjusting Receiving.\r\nSee error log for detail.");
        }
    }

    protected void txtQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            CalculateAmount();
            txtQTY.ReadOnly = true;
            txtBasicRate.ReadOnly = true;

            txtQtyUnit.ReadOnly = true;
            txtUNIT.ReadOnly = true;
            txtQtyWeight.ReadOnly = true;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Getting Final Amount.\r\nSee error log for detail.");
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in loading Indent for updation. see error log for detail");
        }
    }

    protected DataTable GetIssueData(string text, int startOffset, int numberOfItems)
    {
        try
        {
            DataTable dt = new DataTable();
            string commandText = "SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_TX_FABRIC_IR_MST a WHERE  nvl(A.CONF_FLAG,'0')='0' and YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "') asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) WHERE ROWNUM <= 15";


            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause = "  AND trn_numb NOT IN (SELECT TRN_NUMB FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_TX_FABRIC_IR_MST a WHERE nvl(A.CONF_FLAG,'0')='0' and YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND A.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE ='" + TRN_TYPE + "') asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by TRN_NUMB desc, TRN_DATE desc";

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", text + "%", "");

            return dt;
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
            DataTable dt = new DataTable();
            string commandText = "SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_TX_FABRIC_IR_MST a WHERE  nvl(A.CONF_FLAG,'0')='0' and YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "') asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) ";

            string whereClause = string.Empty;

            string SortExpression = " order by TRN_NUMB desc, TRN_DATE desc";

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", text + "%", "");

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

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();

            int iRecordFound = GetdataByChallaNumber(TRN_NUMBER);
            BindGridFromDataTable();
            if (iRecordFound > 0)
            {

            }
            else
            {
                // InitialisePage();
                ActivateUpdateMode();
                string msg = "Dear " + oUserLoginDetail.Username + "!! Select Issue No already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Challan No');", true);
            }
        }
        catch (Exception Ex)
        {
            errorLog.ErrHandler.WriteError(Ex.Message);
            CommonFuction.ShowMessage(@"Problem in selection of Challan Number.\r\nSee error log for detail.");
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

    protected void txtQtyUnit_TextChanged(object sender, EventArgs e)
    {

    }
}
