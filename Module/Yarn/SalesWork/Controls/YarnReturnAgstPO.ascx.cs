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

public partial class Module_Yarn_SalesWork_Controls_ReturnAgstPO : System.Web.UI.UserControl
{
    private  double FinalTotal = 0;

    private static DateTime StartDate;
    private static DateTime EndDate;
    private  SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private  DataTable dtDetailTBL = null;
    private string TRN_TYPE = "IYS03";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack )
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
           
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdSave.Visible = true;

           

            Session["dtItemReceipt"] = null;

            ActivateSaveMode();
            BindShift();
            BindDepartment();
            BindCostCode();
            Blankrecords();
            BindNewChallanNum();
            CreateDataTable();
            if (Session["LoginDetail"] != null)
            {
              
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
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.TRN_TYPE = TRN_TYPE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            txtChallanNumber.Text = SaitexBL.Interface.Method.YRN_IR_MST.GetNewTRNNumber(oYRN_IR_MST).ToString();
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
            txtPartyCode.SelectedIndex = -1;
            txtPartyCode1.Text = string.Empty;
            txtPartyAddress.Text = string.Empty;
            ddlIssueShift.SelectedIndex = -1;
            txtDepartment.SelectedIndex = -1;

            if (Session["dtDetailTBL"] != null)
                Session["dtDetailTBL"] = null;

            grdMaterialItemIssue.DataSource = null;
            grdMaterialItemIssue.DataBind();

            lblMode.Text = "Save";

            txtChallanNumber.ReadOnly = true;
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);  
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                txtDepartment.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;
            }

            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
        }
        catch
        {
            throw;
        }
    }
    private DataTable  CreateDataTable()
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
            dtDetailTBL.Columns.Add("SHADE_FAMILY", typeof(string));
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

            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));

            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("GROSS_WT", typeof(double));
            dtDetailTBL.Columns.Add("TARE_WT", typeof(double));
            dtDetailTBL.Columns.Add("CARTONS", typeof(double));
            dtDetailTBL.Columns.Add("JOBER", typeof(string));
          
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
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
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
            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }           
            dtDetailTBL.Rows.Clear();
            FinalTotal = 0;
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
        }
    }
    private int GetdataByChallaNumber(int ChallanNumber)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtChallanNumber.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();

                txtDepartment.SelectedValue = dt.Rows[0]["DEPT_CODE"].ToString().Trim();
                //     txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();

                txtIssueDate.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                txtDocDate.Text = dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim();
                txtDocNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                txtPartyCode1.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                ddlIssueShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();
                ddlStore.SelectedIndex=ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["STORE"].ToString().Trim()));
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dt.Rows[0]["LOCATION"].ToString().Trim()));
                // ddlReprocess.Text =string.Empty;
            }
            if (iRecordFound == 1)
            {
                Session["dtDetailTBL"]= dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE);

                MapDataTable();
                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueAdjByMst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE, ChallanNumber);
                    MapAdjustDataTable(dtReceiptAdjustment);
                    Session["dtItemReceipt"] = dtReceiptAdjustment;

                }
            }
            else
            {
                //string msg = "Dear " + oUserLoginDetail.Username + " !! MRN already approved. Modification not allowed.";
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
            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
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

                //dtReceiptAdjustment.Rows[iLoop]["PI_NO"] = "NA";
                //dtReceiptAdjustment.Rows[iLoop]["ISS_PI_NO"] = "NA";
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for detail."));
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

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation mode.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in generating report data.\r\nSee error log for detail."));

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
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            bool ReturnResult = false;
            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
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
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetItemDetailByItemCode(ItemCode, oUserLoginDetail.DT_STARTDATE.Year, "", "");

            if (dt != null && dt.Rows.Count > 0)
            {
                Description = dt.Rows[0]["YARN_DESC"].ToString().Trim();
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
            Hashtable htIssue = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = txtDepartment.SelectedValue.Trim();
            oYRN_IR_MST.FORM_NUMB = "";
            oYRN_IR_MST.FORM_TYPE = "";

            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oYRN_IR_MST.GATE_NUMB = "";
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());
            
            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = txtPartyCode1.Text;
            oYRN_IR_MST.PRTY_NAME = txtPartyAddress.Text;
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = string.Empty;
            oYRN_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            oYRN_IR_MST.TRSP_CODE = "NA";
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;

            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
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
            oYRN_IR_MST.LOT_ID_NO = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            dtDetailTBL = (DataTable)Session["dtDetailTBL"];

           

            int TRN_NUMB = 0;
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"Return Number : " + TRN_NUMB + " saved successfully.");

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

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();

            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = txtDepartment.SelectedValue.Trim();
            oYRN_IR_MST.FORM_NUMB = "";
            oYRN_IR_MST.FORM_TYPE = "";

            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oYRN_IR_MST.GATE_NUMB = "";
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = txtPartyCode1.Text;
            oYRN_IR_MST.PRTY_NAME = txtPartyAddress.Text;
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = string.Empty;
            oYRN_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            oYRN_IR_MST.TRSP_CODE = "NA";
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
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
            oYRN_IR_MST.LOT_ID_NO = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;


            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            dtDetailTBL = (DataTable)Session["dtDetailTBL"];

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
        catch
        {
            throw;
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

            if (dtDetailTBL.Rows.Count < 15)
            {
                if (txtICODE.SelectedText != "" && txtQTY.Text != "" && txtBasicRate.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                    bool bb = SearchItemCodeInGrid(txtICODE.SelectedText.Trim(), UNIQUEID);
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
                                    int ponumb = 0;
                                    int poyear = 0;
                                    int.TryParse(txtPO_NUMB.Text, out ponumb);
                                    int.TryParse(lblPO_Year.Text, out poyear);
                                    dv[0]["PO_NUMB"] = ponumb;
                                    dv[0]["PO_YEAR"] = poyear;
                                    dv[0]["PO_TYPE"] = lblPO_TYPE.Text;
                                    dv[0]["PO_COMP_CODE"] = lblPO_COMP.Text ;
                                    dv[0]["PO_BRANCH"] = lblPO_BRANCH.Text;
                                    dv[0]["YARN_CODE"] = txtICODE.SelectedText.Trim();
                                    dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                    dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                    dv[0]["UOM"] = txtUNIT.Text.Trim();
                                    dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["AMOUNT"] = (double.Parse(txtQTY.Text.Trim()) * double.Parse(txtBasicRate.Text.Trim()));
                                    dv[0]["COST_CODE"] = ddlCostCode.Text.Trim();
                                    dv[0]["MAC_CODE"] = txtMacCode.Text.Trim();
                                    dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                    dv[0]["QCFLAG"] = "No";
                                    dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text ;
                                    dv[0]["SHADE_CODE"] = txtShade.Text ;
                                    DateTime dd = System.DateTime.Now;
                                    dv[0]["DATE_OF_MFG"] = dd;

                                    dv[0]["LOT_NO"] = "NA";
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
                                int ponumb = 0;
                                int poyear = 0;
                                int.TryParse(txtPO_NUMB.Text, out ponumb);
                                int.TryParse(lblPO_Year.Text, out poyear);
                                dr["PO_NUMB"] = ponumb;
                                dr["PO_YEAR"] = poyear;
                                dr["PO_TYPE"] = lblPO_TYPE.Text;
                                dr["PO_COMP_CODE"] = lblPO_COMP.Text;
                                dr["PO_BRANCH"] = lblPO_BRANCH.Text;
                                dr["YARN_CODE"] = txtICODE.SelectedText.Trim();
                                dr["YARN_DESC"] = txtDESC.Text.Trim();
                                dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dr["UOM"] = txtUNIT.Text.Trim();
                                dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["AMOUNT"] = (double.Parse(txtQTY.Text.Trim()) * double.Parse(txtBasicRate.Text.Trim()));
                                dr["COST_CODE"] = ddlCostCode.Text.Trim();
                                dr["MAC_CODE"] = txtMacCode.Text.Trim();
                                dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                dr["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dr["DATE_OF_MFG"] = dd;
                                dr["SHADE_FAMILY"] = txtShadeFamily.Text;
                                dr["SHADE_CODE"] = txtShade.Text;
                                dr["NO_OF_UNIT"] = "0";
                                dr["UOM_OF_UNIT"] = "0";
                                dr["WEIGHT_OF_UNIT"] = "0";
                                dr["PI_NO"] = "NA";

                                dr["LOT_NO"] = "NA";
                                dr["GRADE"] = "NA";
                                dr["GROSS_WT"] = 0;
                                dr["TARE_WT"] = 0;
                                dr["CARTONS"] = 0;
                                dtDetailTBL.Rows.Add(dr);
                            }

                            
                            Session["dtDetailTBL"] = dtDetailTBL; ;
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
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding Material detail data.\r\nSee error log for detail."));

        }
    }
    private void RefreshDetailRow()
    {
        try
        {
            txtICODE.SelectedIndex = -1;
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtAmount.Text = "";
            txtItemCode.Text = "";
            txtMacCode.Text = "";
            txtDetRemarks.Text = "";
            lblPO_BRANCH.Text = "";
            lblPO_COMP.Text = "";
            lblPO_TYPE.Text = "";
            lblPO_Year.Text = string.Empty;
            txtShadeFamily.Text = "";
            txtShade.Text = "";
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
        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text,e.ItemsOffset);
            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "YARN_CODE";
            txtICODE.DataValueField = "ITEM_DATA";
            txtICODE.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;

            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }
    protected DataTable GetPOData(string text, int startoffset)
    {
        try
        {
           DataTable dt = new DataTable();
            string commandText = "SELECT   *  FROM   (  SELECT   asd.po_numb,              asd.YARN_CODE,      asd.YARN_desc,  ASD.SHADE_FAMILY,ASD.SHADE,      (   asd.PO_YEAR|| '@'  || asd.PO_COMP_CODE  || '@'   || asd.PO_BRANCH   || '@'   || asd.PO_TYPE   || '@'   || asd.po_numb   || '@'    || asd.YARN_CODE     || '@'   || asd.YARN_desc      || '@'  || asd.UOM|| '@'  || asd.SHADE_FAMILY|| '@'  || asd.SHADE)     ITEM_DATA,    TRN_QTY,           TRN_QTY_ADJ,        REMQTY         FROM   (  SELECT   A.PO_YEAR,      A.PO_COMP_CODE,      A.PO_BRANCH,       A.PO_TYPE,                  a.po_numb,   a.YARN_CODE,a.YARN_desc,     A.UOM,      SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY,     SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ,     SUM (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0))      AS REMQTY, a.shade, a.shade_family        FROM   V_YRN_IR_TRN_RETURN a          WHERE   a.trn_type = 'RYS01'     AND a.conf_flag = '0'          AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "'    AND a.BRANCH_CODE =      '" + oUserLoginDetail.CH_BRANCHCODE + "'   AND A.PRTY_CODE = '" + txtPartyCode1.Text + "'       GROUP BY   A.PO_YEAR,A.PO_COMP_CODE, A.PO_BRANCH,A.PO_TYPE,    a.po_numb,  a.YARN_CODE,  a.YARN_desc,   A.UOM, a.shade, a.shade_family) asd WHERE   LTRIM (RTRIM (YARN_CODE)) LIKE :SearchQuery      OR LTRIM (RTRIM (YARN_DESC)) LIKE :SearchQuery        ORDER BY   po_numb ASC, YARN_CODE ASC) WHERE   REMQTY > 0 AND ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += "  AND ITEM_DATA NOT IN (SELECT ITEM_DATA FROM (SELECT   asd.po_numb,              asd.YARN_CODE,      asd.YARN_desc,   ASD.SHADE_FAMILY,ASD.SHADE,      (   asd.PO_YEAR|| '@'  || asd.PO_COMP_CODE  || '@'   || asd.PO_BRANCH   || '@'   || asd.PO_TYPE   || '@'   || asd.po_numb   || '@'    || asd.YARN_CODE     || '@'   || asd.YARN_desc      || '@'  || asd.UOM|| '@'  || asd.SHADE_FAMILY|| '@'  || asd.SHADE)     ITEM_DATA,    TRN_QTY,           TRN_QTY_ADJ,        REMQTY         FROM   (  SELECT   A.PO_YEAR,      A.PO_COMP_CODE,      A.PO_BRANCH,       A.PO_TYPE,                  a.po_numb,   a.YARN_CODE,a.YARN_desc,     A.UOM,      SUM (NVL (a.TRN_QTY, 0)) AS TRN_QTY,     SUM (NVL (a.ISS_QTY, 0)) AS TRN_QTY_ADJ,     SUM (NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0))      AS REMQTY  ,a.shade, a.shade_family      FROM   V_YRN_IR_TRN_RETURN a          WHERE   a.trn_type = 'RYS01'     AND a.conf_flag = '0'          AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "'    AND a.BRANCH_CODE =      '" + oUserLoginDetail.CH_BRANCHCODE + "'   AND A.PRTY_CODE = '" + txtPartyCode1.Text + "'       GROUP BY   A.PO_YEAR,A.PO_COMP_CODE, A.PO_BRANCH,A.PO_TYPE,    a.po_numb,  a.YARN_CODE,  a.YARN_desc,   A.UOM ,a.shade, a.shade_family) asd WHERE   LTRIM (RTRIM (YARN_CODE)) LIKE :SearchQuery      OR LTRIM (RTRIM (YARN_DESC)) LIKE :SearchQuery        ORDER BY   po_numb ASC, YARN_CODE ASC) WHERE REMQTY > 0 AND ROWNUM <= '" + startoffset + "')";
            }
            string SortExpression = " order by po_numb asc, YARN_CODE,SHADE_FAMILY,SHADE asc";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable RemoveAlreadyAddedRow(DataTable dt)
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

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtDetailTBL.Rows)
                {
                    dt = RemoveRowFromItemSelectionList(dr1, dt);
                }

            }
            return dt;
        }
        catch
        {
            throw;
        }
    }
    private DataTable RemoveRowFromItemSelectionList(DataRow dr, DataTable dt)
    {
        try
        {
            foreach (DataRow dr1 in dt.Rows)
            {
                if (dr1["YARN_CODE"].ToString() == dr["YARN_CODE"].ToString())
                {
                    dt.Rows.Remove(dr1);
                    break;
                }
            }
            dt.AcceptChanges();
            return dt;
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
        }
    }
    private void GetDataForDetail(string sItemData)
    {
        try
        {
        
        //    int UNIQUEID = 0;
        //    if (ViewState["UNIQUEID"] != null)
        //        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
        //    if (!SearchItemCodeInGrid(YARN_CODE, UNIQUEID))
        //    {
        //        DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetItemDetailByItemCode(YARN_CODE, oUserLoginDetail.DT_STARTDATE.Year, "", "");
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            txtICODE.SelectedText = dt.Rows[0]["YARN_CODE"].ToString().Trim();
        //            txtDESC.Text = dt.Rows[0]["YARN_DESC"].ToString().Trim();
        //            txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
        //            txtQTY.Text = "0";
        //            txtBasicRate.Text = "0";
        //            txtAmount.Text = "0";
        //            btnAdjRec.Focus();
        //            //lblPO_BRANCH.Text = dt.Rows[0]["BRANCH_CODE"].ToString().Trim();
        //            //lblPO_COMP.Text = dt.Rows[0]["COMP_CODE"].ToString().Trim();
        //            //lblPO_TYPE.Text = dt.Rows[0]["PO_TYPE"].ToString().Trim();
        //        }
        //    }
        //    else
        //    {
        //        RefreshDetailRow();
        //        CommonFuction.ShowMessage("Yarn Already Included");
        //    }



            string[] ItemData = sItemData.Split('@');
            int po_year = int.Parse(ItemData[0].ToString());
            string PO_CompCode = ItemData[1].ToString();
            string PO_BranchCode = ItemData[2].ToString();
            string PO_Type = ItemData[3].ToString();
            int po_Numb = int.Parse(ItemData[4].ToString());
            string ItemCode = ItemData[5].ToString();
            string ItemDesc = ItemData[6].ToString();
            string UOM = ItemData[7].ToString();
            string SHADE_FAMILY = ItemData[8].ToString();
            string SHADE = ItemData[9].ToString();
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
                txtShadeFamily.Text = SHADE_FAMILY;
                txtShade.Text = SHADE;
                txtQTY.Text = "0";
                txtBasicRate.Text = "0";
                txtAmount.Text = "0";
                btnAdjRec.Focus();
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Yarn Already Included");
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

                BindCMBPOITEM();
                txtICODE.SelectedText = dv[0]["YARN_CODE"].ToString();
                txtICODE.SelectedValue = dv[0]["YARN_CODE"].ToString();

                foreach (ComboBoxItem item in txtICODE.Items)
                {
                    if (item.Text == dv[0]["YARN_CODE"].ToString().Trim())
                    {
                        txtICODE.SelectedIndex = txtICODE.Items.IndexOf(item);
                        break;
                    }
                }

                txtPO_NUMB.Text = dv[0]["PO_NUMB"].ToString();
                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                lblPO_Year.Text = dv[0]["PO_YEAR"].ToString();
                txtItemCode.Text = dv[0]["YARN_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();                
                txtMacCode.Text = dv[0]["MAC_CODE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                txtShadeFamily.Text = dv[0]["SHADE_FAMILY"].ToString();
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();

            }
        }
        catch
        {
            throw;
        }

      
    }
    private void BindCMBPOITEM()
    {
        try
        {

            DataTable data = GetPOData("",0);

            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "YARN_CODE";
            txtICODE.DataValueField = "YARN_CODE";
            txtICODE.DataBind();
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
            string URL = "Yrn_PO_Return_Adjustment.aspx";
            URL = URL + "?ItemCodeId=" + txtICODE.SelectedText.Trim();
            URL = URL + "&TextBoxId=" + txtQTY.ClientID;
            URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
            URL = URL + "&AmountId=" + txtAmount.ClientID;
            URL = URL + "&txtQtyUnit=" + txtQtyUnit.ClientID;
            URL = URL + "&txtQtyUom=" + txtUNIT.ClientID;
            URL = URL + "&txtQtyWeight=" + txtQtyWeight.ClientID;
            URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
            URL = URL + "&TRN_TYPE=" + TRN_TYPE;
            URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
            URL = URL + "&STORE=" + ddlStore.SelectedValue ;
            URL = URL + "&SHADE_FAMILY=" + txtShadeFamily.Text;
            URL = URL + "&SHADE_CODE=" + txtShade.Text;
            URL = URL + "&PARTY_CODE=" + txtPartyCode1.Text;
            URL = URL + "&PO_NUMB=" + txtPO_NUMB.Text;
            URL = URL + "&PI_NO=NA";


          

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=950,height=320,left=200,top=300');", true);
            txtQTY.ReadOnly = false;
            txtAmount.ReadOnly = false;
            txtBasicRate.ReadOnly = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adjusting Receiving.\r\nSee error log for detail."));

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
            string commandText = "select * from (Select a.TRN_NUMB, a.TRN_DATE,a.PRTY_CODE,b.PRTY_NAME,c.DEPT_NAME from YRN_IR_MST a, tx_vendor_mst b, cm_dept_mst c Where a.prty_code=b.prty_code (+) and A.DEPT_CODE=C.DEPT_CODE and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + TRN_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";

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

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            FinalTotal = 0;
            int iRecordFound = GetdataByChallaNumber(TRN_NUMBER);
            BindGridFromDataTable();
            if (iRecordFound > 0)
            {

            }
            else
            {
                //InitialisePage();
                ActivateUpdateMode();
                string msg = "Dear " + oUserLoginDetail.Username + " !! Select Return No already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Challan No');", true);
            }
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
        catch
        {
            throw;
        }
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
        catch
        {
            throw;
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


}
