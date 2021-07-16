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

public partial class Module_Sewing_Thread_Controls_StockTransferIssueSW : System.Web.UI.UserControl
{

    private static DateTime StartDate;
    private static DateTime EndDate;
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in loading page.\r\nSee error log for detail.");
            lblMode.Text = ex.Message;

        }
    }

    private void InitialisePage()
    {
        try
        {
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdSave.Visible = true;

            TRN_TYPE = "IYS06";

            Session["dtItemReceipt"] = null;

            ActivateSaveMode();
            BindShift();
            BindDelBranch();
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
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
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
                    ViewState["dtDetailTBL"] = dtDetailTBL;
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUEID"] = iCount;
                }
                ViewState["dtDetailTBL"] = dtDetailTBL;

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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();
            // FinalTotal = 0;
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
            lblMode.Text = Ex.Message;

        }
    }

    private int GetdataByChallaNumber(int ChallanNumber)
    {
        int iRecordFound = 0;
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

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
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                MapDataTable();
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in saving data.\r\nSee error log for detail.");
            lblMode.Text = ex.Message;

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
            lblMode.Text = ex.Message;

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
            lblMode.Text = ex.Message;

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
            lblMode.Text = ex.Message;

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
            lblMode.Text = ex.Message;

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
            CommonFuction.ShowMessage(@"Problem in generating report data.\r\nSee error log for detail.");
            lblMode.Text = ex.Message;

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
            lblMode.Text = ex.Message;

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
            lblMode.Text = ex.Message;

        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

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

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            string SHADE_CODE = txtShade.Text;
            foreach (GridViewRow grdRow in grdMaterialItemIssue.Rows)
            {
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                Label txtSHADECODE = (Label)grdRow.FindControl("txtSHADECODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (txtICODE.Text.Trim() == ItemCode && txtSHADECODE.Text.Trim() == SHADE_CODE && UniqueId != iUniqueId)
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
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetItemDetailByItemCode(ItemCode, oUserLoginDetail.DT_STARTDATE.Year,"","","","","");

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
            int Qty = 0;
            double Rate = 0;
            double Amount = 0;
            int.TryParse(CommonFuction.funFixQuotes(txtQTY.Text.Trim()), out Qty);
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
            oYRN_IR_MST.PRTY_CODE = "NA";
            oYRN_IR_MST.PRTY_NAME = "";
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
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
            oYRN_IR_MST.LOT_ID_NO = "NA";
            oYRN_IR_MST.REC_BRANCH_CODE = ddlDelAdd.SelectedValue.ToString();
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            int TRN_NUMB = 0;
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.InsertStockTransferSW(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue);
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
            oYRN_IR_MST.PRTY_CODE = "NA";
            oYRN_IR_MST.PRTY_NAME = "";
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes(ddlReprocess.SelectedValue.Trim());
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
            oYRN_IR_MST.LOT_ID_NO = "NA";
            oYRN_IR_MST.REC_BRANCH_CODE = ddlDelAdd.SelectedValue.ToString();
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Update(oYRN_IR_MST, dtDetailTBL, dtItemReceipt, htIssue);
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            if (dtDetailTBL.Rows.Count < 15)
            {
                if (txtSWCode.Text != "" && txtShade.Text != "" && txtQTY.Text != "" && txtBasicRate.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                    bool bb = SearchItemCodeInGrid(txtSWCode.Text.Trim(), UNIQUEID);
                    if (!bb)
                    {
                        int Qty = 0;
                        int.TryParse(txtQTY.Text.Trim(), out Qty);
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
                                    dv[0]["YARN_CODE"] = txtSWCode.Text.Trim();
                                    dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                    dv[0]["SHADE_CODE"] = txtShade.Text.Trim();
                                    dv[0]["TRN_QTY"] = int.Parse(txtQTY.Text.Trim());
                                    dv[0]["UOM"] = txtUNIT.Text.Trim();
                                    dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                    dv[0]["COST_CODE"] = txtCostCode.Text.Trim();
                                    dv[0]["MAC_CODE"] = string.Empty;
                                    dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                    dv[0]["QCFLAG"] = "No";
                                    DateTime dd = System.DateTime.Now;
                                    dv[0]["DATE_OF_MFG"] = dd;
                                    dv[0]["UOM_OF_UNIT"] = "UOM";
                                    dv[0]["WEIGHT_OF_UNIT"] = 111;
                                    dv[0]["NO_OF_UNIT"] = 111;
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
                                dr["YARN_CODE"] = txtSWCode.Text.Trim();
                                dr["YARN_DESC"] = txtDESC.Text.Trim();
                                dr["SHADE_CODE"] = txtShade.Text.Trim();
                                dr["TRN_QTY"] = int.Parse(txtQTY.Text.Trim());
                                dr["UOM"] = txtUNIT.Text.Trim();
                                dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                dr["COST_CODE"] = txtCostCode.Text.Trim();
                                dr["MAC_CODE"] = string.Empty;
                                dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                dr["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dr["UOM_OF_UNIT"] = "UOM";
                                dr["WEIGHT_OF_UNIT"] = 111;
                                dr["NO_OF_UNIT"] = 111;
                                dr["PI_NO"] = "NA";


                                dtDetailTBL.Rows.Add(dr);
                            }
                            RefreshDetailRow();
                            ViewState["dtDetailTBL"] = dtDetailTBL;

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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in adding Yarn detail data.\r\nSee error log for detail.");
            lblMode.Text = ex.Message;

        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            txtICODE.SelectedIndex = -1;
            txtICODE.Enabled = true;
            txtSWCode.Text = string.Empty;
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtAmount.Text = "";
            txtCostCode.Text = "";
            txtDetRemarks.Text = "";
            txtQtyUnit.Text = string.Empty;
            txtQtyWeight.Text = string.Empty;
            txtShade.Text = string.Empty;
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
            lblMode.Text = ex.Message;

        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text.ToUpper(), e.ItemsOffset);
            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "YARN_CODE";
            txtICODE.DataValueField = "TRN_DATA";
            txtICODE.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;

            e.ItemsCount = GetPOCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in getting data for material detail.\r\nSee error log for detail.");
            lblMode.Text = ex.Message;

        }
    }

    protected DataTable GetPOData(string text, int Startoffset)
    {
        try
        {
            DataTable dt = new DataTable();

            string commandText = "SELECT * FROM (SELECT * FROM (SELECT DISTINCT TRN_DATA,YARN_CODE,YARN_DESC,SHADE_CODE,SUM (TRN_QTY) TRN_QTY,SUM (TRN_QTY_ADJ) TRN_QTY_ADJ,SUM (REMQTY) REMQTY FROM (SELECT ( a.YEAR|| '@'|| a.COMP_CODE|| '@'|| a.BRANCH_CODE|| '@'|| a.YARN_CODE|| '@'|| b.YARN_DESC|| '@'|| a.SHADE_CODE|| '@'|| NVL (a.UOM, 'KG.')) TRN_DATA, a.YEAR, B.YARN_DESC, a.TRN_NUMB, a.TRN_TYPE, a.YARN_CODE, A.SHADE_CODE, a.FINAL_RATE, NVL (a.TRN_QTY, 0) AS TRN_QTY, NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ, NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0) AS REMQTY FROM YRN_IR_TRN a, YRN_IR_MST c, YRN_MST B WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND A.YARN_CODE = B.YARN_CODE AND A.TRN_NUMB = c.TRN_NUMB AND A.TRN_TYPE = c.TRN_TYPE AND A.COMP_CODE = c.COMP_CODE AND A.BRANCH_CODE = c.BRANCH_CODE AND A.YEAR = c.YEAR AND A.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND B.YARN_TYPE = 'SEWING THREAD' ORDER BY YARN_CODE ASC) GROUP BY TRN_DATA, YARN_CODE, YARN_DESC, SHADE_CODE) WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";

            string whereClause = string.Empty;
            if (Startoffset != 0)
            {
                whereClause += " AND TRN_DATA NOT IN(SELECT TRN_DATA FROM (SELECT * FROM (SELECT DISTINCT TRN_DATA,YARN_CODE,YARN_DESC,SHADE_CODE,SUM (TRN_QTY) TRN_QTY,SUM (TRN_QTY_ADJ) TRN_QTY_ADJ,SUM (REMQTY) REMQTY FROM (SELECT ( a.YEAR || '@' || a.COMP_CODE || '@' || a.BRANCH_CODE || '@' || a.YARN_CODE|| '@'|| b.YARN_DESC || '@' || a.SHADE_CODE || '@' || NVL (a.UOM, 'KG.')) TRN_DATA,a.YEAR,B.YARN_DESC,a.TRN_NUMB,a.TRN_TYPE,a.YARN_CODE,A.SHADE_CODE,a.FINAL_RATE,NVL (a.TRN_QTY, 0) AS TRN_QTY,NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ,NVL (a.TRN_QTY, 0)- NVL (a.ISS_QTY, 0) AS REMQTY FROM YRN_IR_TRN a,YRN_IR_MST c,YRN_MST B WHERE SUBSTR (a.trn_type,1,1) = 'R' AND A.YARN_CODE =B.YARN_CODE AND A.TRN_NUMB =c.TRN_NUMB AND A.TRN_TYPE =c.TRN_TYPE AND A.COMP_CODE =c.COMP_CODE AND A.BRANCH_CODE =c.BRANCH_CODE AND A.YEAR = c.YEAR AND A.Comp_Code ='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND B.YARN_TYPE ='SEWING THREAD' ORDER BY YARN_CODE ASC) GROUP BY TRN_DATA,YARN_CODE,YARN_DESC,SHADE_CODE) WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery) WHERE ROWNUM <= '" + Startoffset + "')";
            }

            string SortExpression = " ORDER BY YARN_CODE";

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

        string CommandText = "SELECT * FROM (SELECT * FROM (SELECT DISTINCT TRN_DATA,YARN_CODE,YARN_DESC,SHADE_CODE,SUM (TRN_QTY) TRN_QTY,SUM (TRN_QTY_ADJ) TRN_QTY_ADJ,SUM (REMQTY) REMQTY FROM (SELECT ( a.YEAR|| '@'|| a.COMP_CODE|| '@'|| a.BRANCH_CODE|| '@'|| a.YARN_CODE|| '@'|| b.YARN_DESC|| '@'|| a.SHADE_CODE|| '@'|| NVL (a.UOM, 'KG.')) TRN_DATA, a.YEAR, B.YARN_DESC, a.TRN_NUMB, a.TRN_TYPE, a.YARN_CODE, A.SHADE_CODE, a.FINAL_RATE, NVL (a.TRN_QTY, 0) AS TRN_QTY, NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ, NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0) AS REMQTY FROM YRN_IR_TRN a, YRN_IR_MST c, YRN_MST B WHERE SUBSTR (a.trn_type, 1, 1) = 'R' AND A.YARN_CODE = B.YARN_CODE AND A.TRN_NUMB = c.TRN_NUMB AND A.TRN_TYPE = c.TRN_TYPE AND A.COMP_CODE = c.COMP_CODE AND A.BRANCH_CODE = c.BRANCH_CODE AND A.YEAR = c.YEAR AND A.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND B.YARN_TYPE = 'SEWING THREAD' ORDER BY YARN_CODE ASC) GROUP BY TRN_DATA, YARN_CODE, YARN_DESC, SHADE_CODE) WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery) ";
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
            string cString = txtICODE.SelectedValue.Trim();
            string[] stringYarn = cString.Split('@');
            int iYEAR = int.Parse(stringYarn[0].ToString());
            string comp_code = stringYarn[1].ToString();
            string branch_code = stringYarn[2].ToString();
            string yarn_code = stringYarn[3].ToString();
            string YARN_DESC = stringYarn[4].ToString();
            string SHADE_CODE = stringYarn[5].ToString();
            string UOM = stringYarn[6].ToString();

            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(yarn_code, UNIQUEID))
            {
                txtSWCode.Text = yarn_code;
                txtDESC.Text = YARN_DESC;
                txtShade.Text = SHADE_CODE;
                txtUNIT.Text = UOM;
                txtQTY.Text = "0";
                txtBasicRate.Text = "0";
                txtAmount.Text = "0";
                btnAdjRec.Focus();
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("SW Already Included");
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in selecting data.\r\nSee error log for detail.");
            lblMode.Text = ex.Message;

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

                txtSWCode.Text = dv[0]["YARN_CODE"].ToString();
                txtICODE.Enabled = false;
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                txtCostCode.Text = dv[0]["COST_CODE"].ToString();

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
            if (txtSWCode.Text != "" || txtShade.Text != "")
            {
                string URL = "Sw_Recipet_Adjustment.aspx";
                URL = URL + "?ItemCodeId=" + txtSWCode.Text.Trim();
                URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
                URL = URL + "&AmountId=" + txtAmount.ClientID;
                URL = URL + "&TRN_TYPE=" + TRN_TYPE;
                URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
                URL = URL + "&SHADE_CODE=" + txtShade.Text;
                URL = URL + "&txtQtyUnit=" + txtQtyUnit.ClientID;
                URL = URL + "&txtQtyUom=" + txtUNIT.ClientID;
                URL = URL + "&txtQtyWeight=" + txtQtyWeight.ClientID;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                txtQTY.ReadOnly = false;
                txtAmount.ReadOnly = false;
                txtBasicRate.ReadOnly = false;
                txtQtyUnit.ReadOnly = false;
                txtUNIT.ReadOnly = false;
                txtQtyWeight.ReadOnly = false;

            }
            else
            {

                CommonFuction.ShowMessage("Please select SW Code to adjust Receipt.");
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in adjusting Receiving.\r\nSee error log for detail.");
            lblMode.Text = ex.Message;

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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Getting Final Amount.\r\nSee error log for detail.");
            lblMode.Text = ex.Message;

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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in loading Indent for updation. see error log for detail");
            lblMode.Text = ex.Message;

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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];


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

                ActivateUpdateMode();
                string msg = "Dear " + oUserLoginDetail.Username + "!! Select Issue No already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception Ex)
        {
            errorLog.ErrHandler.WriteError(Ex.Message);
            CommonFuction.ShowMessage(@"Problem in selection of Challan Number.\r\nSee error log for detail.");
            lblMode.Text = Ex.Message;

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

    protected void ddlDelAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindDeliveryAddByCode(ddlDelAdd.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Delivery branch selection.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindDeliveryAddByCode(string Del_BranchCode)
    {
        try
        {
            if (ddlDelAdd.SelectedValue != "")
            {
                DataTable dtDelBranch = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMasterByBranchCode(oUserLoginDetail.COMP_CODE, Del_BranchCode);
                if (dtDelBranch != null && dtDelBranch.Rows.Count > 0)
                {
                    txtDelAddress.Text = dtDelBranch.Rows[0]["BRANCH_ADD"].ToString();
                }
            }
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

            ddlDelAdd.Items.Remove(ddlDelAdd.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));


            //ddlDelAdd.SelectedIndex = ddlDelAdd.Items.IndexOf(ddlDelAdd.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));
            BindDeliveryAddByCode(ddlDelAdd.SelectedValue.Trim());
        }
        catch
        {
            throw;
        }
    }
}

