using System;
using System.Data;
using System.Linq;
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

public partial class Module_Inventory_Controls_ReceiptAgainstRetGatePass : System.Web.UI.UserControl
{
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private static string TRN_TYPE = "";
    private static int IsUpdateCall = 2;

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
            TRN_TYPE = "RMS04";

            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();

            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }

            Session["dtItemReceipt"] = null;

            rvbill.MinimumValue = StartDate.ToShortDateString();
            rvbill.MaximumValue = EndDate.ToShortDateString();

            rvlr.MinimumValue = StartDate.ToShortDateString();
            rvlr.MaximumValue = EndDate.ToShortDateString();

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    private void BindNewMRNNum()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.TRN_TYPE = TRN_TYPE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            txtTRNNUMBer.Text = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetNewTRNNumber(oTX_ITEM_IR_MST).ToString();
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
            spnAInW.InnerText = "Amount In Words... ";
            txtDepartment.Text = "";
            txtFormRefNo.Text = "";
            txtFormType.Text = "";
            txtGateEntryDate.Text = "";
            //  txtGateEntryNo.Text = "";
            txtLRDate.Text = "";
            txtLRNo.Text = "";
            txtMRNDate.Text = "";
            txtTRNNUMBer.Text = "";
            txtPartyAddress.Text = "";
            txtPartyBillAmount.Text = "";
            txtPartyBillDate.Text = "";
            txtPartyBillNo.Text = "";
            txtPartyChallanDate.Text = "";
            txtPartyChallanNo.Text = "";
            txtPartyCode.SelectedIndex = -1;
            ddlGateEntryNo.SelectedIndex = -1;
            txtRemarks.Text = "";
            txtTransporterAddress.Text = "";
            txtTransporterCode.SelectedIndex = -1;
            txtVehicleNo.Text = "";

            ddlReceiptShift.SelectedIndex = 0;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();

            grdMaterialItemReceipt.DataSource = null;
            grdMaterialItemReceipt.DataBind();

            lblMode.Text = "Save";

            txtTRNNUMBer.ReadOnly = true;

            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                txtDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;
            }
            RefreshDetailRow();

            tdDelete.Visible = false;
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
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    private void InsertBlankRowInTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            DataRow dr = dtDetailTBL.NewRow();
            dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
            dr["DATE_OF_MFG"] = DateTime.Now.Date.ToShortDateString();
            dtDetailTBL.Rows.Add(dr);
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

            if (dtDetailTBL == null)
                InsertBlankRowInTable();

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
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
                InsertBlankRowInTable();
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
            } ViewState["dtDetailTBL"] = dtDetailTBL;
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
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
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

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            bool ReturnResult = false;

            int count = 0;
            msg = string.Empty;

            if (txtTRNNUMBer.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. MRN Number required.\r\n";
            }

            if (txtPartyCode.SelectedIndex != -1 || txtPartyCode.SelectedText.Trim() != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select party first.\r\n";
            }

            if (ddlGateEntryNo.SelectedIndex != -1 || ddlGateEntryNo.SelectedText.Trim() != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Gate Entry Details first.\r\n";
            }

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
            }

            if (count == 4)
                ReturnResult = true;

            return ReturnResult;
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
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading update mode.\r\nSee error log for detail."));
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

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in cleae page data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting print.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
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

    protected void grdMaterialItemReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUEID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItemReceiptRow(UNIQUEID);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItemReceiptRow(UNIQUEID);
                BindGridFromDataTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing.\r\nSee error log for detail."));
        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int PONumb, int UNIQUEID)
    {
        bool Result = false;
        try
        {
            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                    Label txtPONum = (Label)grdRow.FindControl("txtPONum");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (int.Parse(txtPONum.Text.Trim()) == PONumb && txtICODE.Text.Trim() == ItemCode && UNIQUEID != iUNIQUEID)
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

    private bool ValidatePONumber(string sPONumb)
    {
        try
        {
            bool Result = false;
            int poNumb = 0;

            if (int.TryParse(sPONumb, out poNumb))
                Result = true;

            return Result;
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

            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_ITEM_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oTX_ITEM_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oTX_ITEM_IR_MST.GATE_DATE = dt;

            oTX_ITEM_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(ddlGateEntryNo.SelectedText.Trim());
            oTX_ITEM_IR_MST.GATE_OUT_NUMB = "";
            oTX_ITEM_IR_MST.GATE_PASS_TYPE = "";
            oTX_ITEM_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oTX_ITEM_IR_MST.LR_DATE = dt;

            oTX_ITEM_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_ITEM_IR_MST.PRTY_CH_DATE = dt;

            oTX_ITEM_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oTX_ITEM_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_ITEM_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_ITEM_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_ITEM_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oTX_ITEM_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oTX_ITEM_IR_MST.TRN_DATE = dt;

            oTX_ITEM_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oTX_ITEM_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            if (txtTransporterCode.SelectedIndex < 0)
                oTX_ITEM_IR_MST.TRSP_CODE = "NA";
            else
                oTX_ITEM_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

            oTX_ITEM_IR_MST.TUSER = oUserLoginDetail.UserCode;
            int TRN_NUMB = 0;

            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            bool result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.Insert(oTX_ITEM_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htReceive);
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
    private void UpdateMaterialReceipt()
    {
        try
        {
            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_ITEM_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oTX_ITEM_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oTX_ITEM_IR_MST.GATE_DATE = dt;

            oTX_ITEM_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(ddlGateEntryNo.SelectedText.Trim());
            oTX_ITEM_IR_MST.GATE_OUT_NUMB = "";
            oTX_ITEM_IR_MST.GATE_PASS_TYPE = "";
            oTX_ITEM_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oTX_ITEM_IR_MST.LR_DATE = dt;

            oTX_ITEM_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_ITEM_IR_MST.PRTY_CH_DATE = dt;

            oTX_ITEM_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oTX_ITEM_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_ITEM_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_ITEM_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_ITEM_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oTX_ITEM_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oTX_ITEM_IR_MST.TRN_DATE = dt;

            oTX_ITEM_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oTX_ITEM_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            if (txtTransporterCode.SelectedIndex < 0)
                oTX_ITEM_IR_MST.TRSP_CODE = "NA";
            else
                oTX_ITEM_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

            oTX_ITEM_IR_MST.TUSER = oUserLoginDetail.UserCode;

            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            bool result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.Update(oTX_ITEM_IR_MST, dtDetailTBL, dtItemReceipt, htReceive);
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

    private int GetdataByTRNNUMBer(int TRNNUMBer)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();
                txtFormRefNo.Text = dt.Rows[0]["FORM_NUMB"].ToString().Trim();
                txtFormType.Text = dt.Rows[0]["FORM_TYPE"].ToString().Trim();

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
                //txtPartyBillAmount.Text = dt.Rows[0][""].ToString().Trim();
                //txtPartyBillDate.Text = dt.Rows[0][""].ToString().Trim();
                //txtPartyBillNo.Text = dt.Rows[0][""].ToString().Trim();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim(), out dd))
                    txtPartyChallanDate.Text = dd.ToShortDateString();

                txtPartyChallanNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                txtPartyCode.SelectedText = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                txtTransporterCode.SelectedText = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();

                ddlGateEntryNo.SelectedText = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

                DataTable partyData = GetLOVForParty("");

                txtPartyCode.DataSource = partyData;
                txtPartyCode.DataTextField = "PRTY_CODE";
                txtPartyCode.DataValueField = "Address";
                txtPartyCode.DataBind();

                foreach (ComboBoxItem item in txtPartyCode.Items)
                {
                    if (item.Text == dt.Rows[0]["PRTY_CODE"].ToString().Trim())
                    {
                        txtPartyCode.SelectedIndex = txtPartyCode.Items.IndexOf(item);
                        break;
                    }
                }

                DataTable transporterData = GetLOVForTransporter("");

                txtTransporterCode.DataSource = transporterData;
                txtTransporterCode.DataTextField = "PRTY_CODE";
                txtTransporterCode.DataValueField = "Address";
                txtTransporterCode.DataBind();

                foreach (ComboBoxItem item in txtTransporterCode.Items)
                {
                    if (item.Text == dt.Rows[0]["TRSP_CODE"].ToString().Trim())
                    {
                        txtTransporterCode.SelectedIndex = txtTransporterCode.Items.IndexOf(item);
                        break;
                    }
                }

                txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TAddress"].ToString().Trim();

                DataTable GateEntryData = GetLOVForGate("");

                ddlGateEntryNo.DataSource = GateEntryData;
                ddlGateEntryNo.DataTextField = "GATE_NUMB";
                ddlGateEntryNo.DataValueField = "GATE_DATA";
                ddlGateEntryNo.DataBind();

                foreach (ComboBoxItem item in ddlGateEntryNo.Items)
                {
                    if (item.Text == dt.Rows[0]["GATE_NUMB"].ToString().Trim())
                    {
                        ddlGateEntryNo.SelectedIndex = ddlGateEntryNo.Items.IndexOf(item);
                        break;
                    }
                }
                ddlGateEntryNo.SelectedText = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

            }
            if (iRecordFound == 1)
            {
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetTRN_DataByTRN_NUMBAgnstGatePass(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtDetailTBL.Rows.Count > 0)
                {
                    MapDataTable();

                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Material Detail.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();

                    txtTRNNUMBer.Text = "";
                    ddlTRNNumber.Focus();

                    lblMode.Text = "Update";

                    ActivateUpdateMode();
                    RefreshDetailRow();
                }
            }
            else
            {
                string msg = "Dear " + oUserLoginDetail.Username + " !! MRN already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);

            }
            return iRecordFound;
        }
        catch
        {
            return iRecordFound;
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

            if (!dtDetailTBL.Columns.Contains("MAC_CODE"))
                dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["MAC_CODE"] = string.Empty;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOVForParty(e.Text.ToUpper());
            txtPartyCode.Items.Clear();

            txtPartyCode.DataSource = data;
            txtPartyCode.DataTextField = "PRTY_CODE";
            txtPartyCode.DataValueField = "address";
            txtPartyCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
        }
    }

    private DataTable GetLOVForParty(string Text)
    {
        try
        {
            string CommandText = "SELECT DISTINCT * FROM (SELECT * FROM (SELECT DISTINCT v.PRTY_CODE, TRN_TYPE, PRTY_STATE, PRTY_ADD1, PRTY_ADD2, v.PRTY_NAME,PRTY_ADD1 || ',' || NVL (PRTY_ADD2, ' ') || ',' || NVL (PRTY_STATE, ' ')address FROM TX_VENDOR_MST v RIGHT OUTER JOIN tx_item_ir_mst p ON V.PRTY_CODE = P.PRTY_CODE) a WHERE TRN_TYPE = 'IMS05') b ";
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

    protected void txtPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (IsUpdateCall != 1)
                ResetDetailOnPartySelection();

            txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
            BindCMBPOITEM("", 0);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
        }

    }

    private void ResetDetailOnPartySelection()
    {
        try
        {
            ddlGateEntryNo.SelectedIndex = -1;
            ddlGateEntryNo.SelectedText = string.Empty;
            ddlGateEntryNo.SelectedValue = string.Empty;
            ddlGateEntryNo.Items.Clear();

            txtGateEntryDate.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtPartyChallanDate.Text = string.Empty;
            txtPartyChallanNo.Text = string.Empty;

            RefreshDetailRow();

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            dtDetailTBL.Rows.Clear();

            IsUpdateCall = IsUpdateCall + 1;
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
            DataTable data = GetLOVForTransporter(e.Text);

            txtTransporterCode.Items.Clear();

            txtTransporterCode.DataSource = data;
            txtTransporterCode.DataTextField = "PRTY_CODE";
            txtTransporterCode.DataValueField = "Address";
            txtTransporterCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
        }
    }

    private DataTable GetLOVForTransporter(string Text)
    {
        try
        {
            string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0) asd";
            string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
            string SortExpression = " order by PRTY_CODE asc";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void txtTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtTransporterAddress.Text = txtTransporterCode.SelectedValue;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
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

            if (cmbPOITEM.SelectedText != "" && txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "")
            {
                int UNIQUEID = 0;
                if (ViewState["UNIQUEID"] != null)
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                bool bb = SearchItemCodeInGrid(txtICODE.Text.Trim(), int.Parse(cmbPOITEM.SelectedText.Trim()), UNIQUEID);
                if (!bb)
                {
                    double Qty = 0;
                    double.TryParse(txtQTY.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {

                        double MaxQty = 0;
                        double.TryParse(lblMaxQTY.Text.Trim(), out MaxQty);
                        if (Qty > MaxQty)
                        {
                            txtQTY.Text = MaxQty.ToString();
                            CommonFuction.ShowMessage(@"Entered Quantity is larger than po Quantity.\r\nYou can receive maximum " + MaxQty + " quantity for this item of selected po.");
                            txtQTY.Focus();
                        }
                        else
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
                                    dv[0]["ITEM_CODE"] = txtICODE.Text.Trim();
                                    dv[0]["ITEM_DESC"] = txtDESC.Text.Trim();
                                    dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                    dv[0]["UOM"] = txtUNIT.Text.Trim();
                                    dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                    dv[0]["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                    dv[0]["COST_CODE"] = txtCostCode.Text.Trim();
                                    dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                    if (chk_QCFlag.Checked)
                                        dv[0]["QCFLAG"] = "Yes";
                                    else
                                        dv[0]["QCFLAG"] = "No";

                                    DateTime dd = System.DateTime.Now;
                                    DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                    dv[0]["DATE_OF_MFG"] = dd;

                                    dtDetailTBL.AcceptChanges();

                                    createdatatableforadjustment(cmbPOITEM.SelectedText.Trim(), lblPO_TYPE.Text, double.Parse(txtQTY.Text.Trim()), txtICODE.Text.Trim(), double.Parse(txtFinalRate.Text.Trim()), UNIQUEID);
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
                                dr["ITEM_CODE"] = txtICODE.Text.Trim();
                                dr["ITEM_DESC"] = txtDESC.Text.Trim();
                                dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dr["UOM"] = txtUNIT.Text.Trim();
                                dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dr["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                dr["COST_CODE"] = txtCostCode.Text.Trim();
                                dr["MAC_CODE"] = string.Empty;
                                dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                if (chk_QCFlag.Checked)
                                    dr["QCFLAG"] = "Yes";
                                else
                                    dr["QCFLAG"] = "No";

                                DateTime dd = System.DateTime.Now;
                                DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                dr["DATE_OF_MFG"] = dd;

                                dtDetailTBL.Rows.Add(dr);

                                createdatatableforadjustment(cmbPOITEM.SelectedText.Trim(), lblPO_TYPE.Text, double.Parse(txtQTY.Text.Trim()), txtICODE.Text.Trim(), double.Parse(txtFinalRate.Text.Trim()), UNIQUEID);

                            }
                            RefreshDetailRow();
                        }
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
            } ViewState["dtDetailTBL"] = dtDetailTBL;
            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding transaction data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in canceling transaction.\r\nSee error log for detail."));
        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text);
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "TRN_NUMB";
            cmbPOITEM.DataValueField = "po_Item_trn";
            cmbPOITEM.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;

            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading transaction data.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetPOData(string text)
    {
        try
        {
            DataTable dt = new DataTable();
            if (txtPartyCode.SelectedText != "")
            {
                string po_type = "IMS05";
                string whereClause = " WHERE pm.conf_flag='1' and PM.GATE_PASS_TYPE = '1' AND PM.TRN_TYPE = '" + po_type + "' AND (NVL (PT.TRN_QTY, 0) - NVL (PT.ISS_QTY, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND PT.TRN_TYPE = PM.TRN_TYPE AND pm.TRN_NUMB = PT.TRN_NUMB AND PM.PRTY_CODE = '" + txtPartyCode.SelectedText.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.ITEM_CODE LIKE :SearchQuery";
                string sortExpression = " ORDER BY PT.TRN_NUMB ASC, PT.ITEM_CODE ASC ";
                string commandText = "SELECT DISTINCT (PT.TRN_TYPE || '@' || PT.TRN_NUMB || '@' || PT.ITEM_CODE) po_Item_trn, pt.TRN_NUMB, pt.ITEM_CODE, pt.TRN_QTY, pt.UOM, PM.PRTY_CODE, pt.FINAL_RATE BASIC_RATE, pt.FINAL_RATE, i.ITEM_DESC, NVL (PT.ISS_QTY, 0) QTY_RCPT, NVL (PT.TRN_QTY, 0) - NVL (PT.ISS_QTY, 0) AS QTY_REM FROM TX_ITEM_IR_TRN pt, TX_ITEM_MST i, TX_ITEM_IR_MST PM ";

                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
            }
            else
            {
                CommonFuction.ShowMessage("Please select Party First");
            }
            //if (dt != null && dt.Rows.Count > 0)
            //    dt = RemoveAlreadyAddedRow(dt);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetPOData(string text, int PO_Numb, string Item_Code)
    {
        try
        {
            DataTable dt = new DataTable();
            if (txtPartyCode.SelectedText != "")
            {
                string po_type = "IMS05";
                string whereClause = " WHERE (pm.conf_flag='1' and PM.GATE_PASS_TYPE = '1' AND PM.TRN_TYPE = '" + po_type + "' AND (NVL (PT.TRN_QTY, 0) - NVL (PT.ISS_QTY, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND PT.TRN_TYPE = PM.TRN_TYPE AND pm.TRN_NUMB = PT.TRN_NUMB AND PM.PRTY_CODE = '" + txtPartyCode.SelectedText.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.ITEM_CODE LIKE :SearchQuery) OR (pm.conf_flag='1' and PM.GATE_PASS_TYPE = '1' AND pt.ITEM_CODE = i.ITEM_CODE AND pm.TRN_NUMB = PT.TRN_NUMB AND PT.TRN_NUMB = '" + PO_Numb + "' AND PT.TRN_TYPE = '" + po_type + "' AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PT.ITEM_CODE = '" + Item_Code + "' )";
                string sortExpression = " ORDER BY TRN_NUMB";
                string commandText = " SELECT DISTINCT (PT.TRN_TYPE || '@' || PT.TRN_NUMB || '@' || PT.ITEM_CODE) po_Item_trn, pt.COMP_CODE, pt.BRANCH_CODE, pt.TRN_NUMB, pt.ITEM_CODE, pt.TRN_QTY, i.ITEM_DESC, NVL (PT.ISS_QTY, 0) QTY_RCPT, NVL (PT.ISS_QTY, 0) QTY_RTN, NVL (PT.TRN_QTY, 0) - NVL (PT.ISS_QTY, 0) AS QTY_REM FROM TX_ITEM_IR_TRN pt, TX_ITEM_MST i, TX_ITEM_IR_MST pm ";

                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
            }
            else
            {
                CommonFuction.ShowMessage("Please select Party First");
            }
            //if (dt != null && dt.Rows.Count > 0)
            //    dt = RemoveAlreadyAddedRow(dt);

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
            string cString = cmbPOITEM.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string PO_Type = arrString[0].ToString();
            int PONumb = int.Parse(arrString[1].ToString());
            string Item_Code = arrString[2].ToString();
            GetDataForDetail(PO_Type, PONumb, Item_Code);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading transaction data.\r\nSee error log for detail."));
        }
    }

    private void GetDataForDetail(string PO_Type, int PONumb, string Item_Code)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(Item_Code, PONumb, UNIQUEID))
            {
                DataTable dt = SaitexBL.Interface.Method.Material_Purchase_Order.GetTRNData_ForReceivingAgainstReturn(oUserLoginDetail.DT_STARTDATE.Year, PO_Type, PONumb, Item_Code);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtICODE.Text = dt.Rows[0]["ITEM_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["ITEM_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    lblMaxQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    txtBasicRate.Text = float.Parse(dt.Rows[0]["BASIC_RATE"].ToString().Trim()).ToString();
                    txtFinalRate.Text = float.Parse(dt.Rows[0]["FINAL_RATE"].ToString().Trim()).ToString();
                    txtAmount.Text = (float.Parse(dt.Rows[0]["QTY_REM"].ToString()) * float.Parse(dt.Rows[0]["FINAL_RATE"].ToString())).ToString();
                    txtDOM.Text = DateTime.Now.Date.ToShortDateString();
                    lblPO_BRANCH.Text = dt.Rows[0]["BRANCH_CODE"].ToString().Trim();
                    lblPO_COMP.Text = dt.Rows[0]["COMP_CODE"].ToString().Trim();
                    lblPO_TYPE.Text = dt.Rows[0]["PO_TYPE"].ToString().Trim();
                    txtCostCode.Focus();

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

    private void RefreshDetailRow()
    {
        try
        {
            cmbPOITEM.SelectedIndex = -1;
            txtICODE.Text = "";
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtFinalRate.Text = "";
            txtAmount.Text = "";
            txtCostCode.Text = "";
            txtDOM.Text = "";
            txtDetRemarks.Text = "";
            chk_QCFlag.Checked = false;
            lblPO_BRANCH.Text = "";
            lblPO_COMP.Text = "";
            lblPO_TYPE.Text = "";
            ViewState["UNIQUEID"] = null;
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
                string cString = dv[0]["PO_TYPE"].ToString() + "@" + dv[0]["PO_NUMB"].ToString() + "@" + dv[0]["ITEM_CODE"].ToString();
                BindCMBPOITEM(cString, double.Parse(dv[0]["TRN_QTY"].ToString()));

                cmbPOITEM.SelectedText = dv[0]["PO_NUMB"].ToString();
                cmbPOITEM.SelectedValue = cString;

                foreach (ComboBoxItem item in cmbPOITEM.Items)
                {
                    if (item.Text == dv[0]["PO_NUMB"].ToString().Trim())
                    {
                        cmbPOITEM.SelectedIndex = cmbPOITEM.Items.IndexOf(item);
                        break;
                    }
                }

                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                txtICODE.Text = dv[0]["ITEM_CODE"].ToString();
                txtDESC.Text = dv[0]["ITEM_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                txtCostCode.Text = dv[0]["COST_CODE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                if (dv[0]["QCFLAG"].ToString() == "Yes")
                    chk_QCFlag.Checked = true;

                else
                    chk_QCFlag.Checked = false;

                txtDOM.Text = dv[0]["DATE_OF_MFG"].ToString();
                ViewState["UNIQUEID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindCMBPOITEM(string cString, double TRN_QTY)
    {
        try
        {
            DataTable data = new DataTable();
            if (cString.Equals(""))
                data = GetPOData("");
            else
            {
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
                string PO_Type = arrString[0].ToString();
                int PONumb = int.Parse(arrString[1].ToString());
                string Item_Code = arrString[2].ToString();
                data = GetPOData("", PONumb, Item_Code);

                if (data != null && data.Rows.Count > 0)
                {
                    DataView dv = new DataView(data);
                    dv.RowFilter = "COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND TRN_NUMB='" + PONumb + "' AND ITEM_CODE='" + Item_Code + "' ";
                    if (dv.Count > 0)
                    {
                        lblMaxQTY.Text = (double.Parse(dv[0]["QTY_REM"].ToString()) + TRN_QTY).ToString();
                    }
                }

            }
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "TRN_NUMB";
            cmbPOITEM.DataValueField = "po_Item_trn";
            cmbPOITEM.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlGateEntryNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOVForGate(e.Text.ToUpper());
            ddlGateEntryNo.Items.Clear();

            ddlGateEntryNo.DataSource = data;
            ddlGateEntryNo.DataTextField = "GATE_NUMB";
            ddlGateEntryNo.DataValueField = "GATE_DATA";
            ddlGateEntryNo.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in gate entry detail selection.\r\nSee error log for detail."));
        }
    }

    private DataTable GetLOVForGate(string text)
    {
        try
        {
            DataTable data = null;
            if (txtPartyCode.SelectedText != "")
            {
                string CommandText = string.Empty;
                if (lblMode.Text == "Save")
                {
                    CommandText = "SELECT * FROM ( select (a.GATE_DATE||'@'||a.LORRY_NO||'@'||a.DOC_NO||'@'||a.DOC_DATE) GATE_DATA ,a.* from v_tx_gate_mst a where a.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE='" + txtPartyCode.SelectedText.Trim() + "' AND a.ITEM_TYPE='MATERIAL IN' and nvl(a.ISSUE_NUMB,0)=0) asd ";
                }
                else if (lblMode.Text == "Update")
                {
                    CommandText = "select * from ( SELECT * FROM (select (a.GATE_DATE||'@'||a.LORRY_NO||'@'||a.DOC_NO||'@'||a.DOC_DATE) GATE_DATA ,a.* from v_tx_gate_mst a where a.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE='" + txtPartyCode.SelectedText.Trim() + "' AND a.ITEM_TYPE='MATERIAL IN' ) asd where nvl(ISSUE_NUMB,0)='" + txtTRNNUMBer.Text.Trim() + "' or nvl(ISSUE_NUMB,0)=0 ) asd ";
                }
                string WhereClause = " WHERE GATE_NUMB LIKE :SearchQuery or GATE_DATE LIKE :SearchQuery";
                string SortExpression = " ORDER BY GATE_NUMB ";
                string SearchQuery = text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
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

            GetDataForGateDetail(GateDate, VahicleNo, PartyChallanNo, partyChallanDate);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in gate entry detail selection.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading data for updation.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " where TRN_NUMB like :searchQuery or TRN_DATE like :searchQuery or prty_code like :searchQuery or prty_name like :searchQuery";
            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";
            string commandText = "select * from (Select a.TRN_NUMB, a.TRN_DATE,a.PRTY_CODE,b.PRTY_NAME from TX_ITEM_IR_MST a, tx_vendor_mst b Where a.prty_code=b.prty_code (+) and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + TRN_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt;
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();
            int iRecordFound = GetdataByTRNNUMBer(TRN_NUMBER);
            BindGridFromDataTable();
            if (iRecordFound > 0)
            {
                IsUpdateCall = 1;
            }
            else
            {
                InitialisePage();
                ActivateUpdateMode();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading data for updation.\r\nSee error log for detail."));
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

        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyBillAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {

            spnAInW.InnerText = "Amount In Words... ";
            double dPartyBillAmount = 0;
            if (double.TryParse(txtPartyBillAmount.Text.Trim(), out dPartyBillAmount))
            {
                AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
                // lblPartyBillAmountInWords.Text = oRupeesToWord.changeNumericToWords(dPartyBillAmount);
                spnAInW.InnerText = "Amount In Words... " + oRupeesToWord.changeNumericToWords(dPartyBillAmount);
            }
            else
            {
                txtPartyBillAmount.Text = string.Empty;
                txtPartyBillAmount.Focus();
                CommonFuction.ShowMessage("Invalid Party Bill Amount Entered.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting bill amount.\r\nSee error log for detail."));
        }
    }

    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("YEAR", typeof(int));
        dt.Columns.Add("COMP_CODE", typeof(string));
        dt.Columns.Add("BRANCH_CODE", typeof(string));
        dt.Columns.Add("TRN_TYPE", typeof(string));
        dt.Columns.Add("TRN_NUMB", typeof(int));
        dt.Columns.Add("PO_COMP", typeof(string));
        dt.Columns.Add("PO_BRANCH", typeof(string));
        dt.Columns.Add("PO_TYPE", typeof(string));
        dt.Columns.Add("PO_NUMB", typeof(int));
        dt.Columns.Add("ITEM_CODE", typeof(string));
        dt.Columns.Add("ISSUE_QTY", typeof(double));
        dt.Columns.Add("FINAL_RATE", typeof(double));
        dt.Columns.Add("ISS_YEAR", typeof(int));
        dt.Columns.Add("ISS_COMP", typeof(string));
        dt.Columns.Add("ISS_BRANCH", typeof(string));
        dt.Columns.Add("ISS_TRN_TYPE", typeof(string));
        dt.Columns.Add("ISS_TRN_NUMB", typeof(int));
        dt.Columns.Add("ISS_PO_COMP", typeof(string));
        dt.Columns.Add("ISS_PO_BRNCH", typeof(string));
        dt.Columns.Add("ISS_PO_TYPE", typeof(string));
        dt.Columns.Add("ISS_PO_NUMB", typeof(int));

        return dt;
    }

    private void createdatatableforadjustment(string RET_NUMB, string RET_TYPE, double Adjustqty, string ITEM_CODE, double FINAL_RATE, int UNIQUEID)
    {
        try
        {
            DataTable dtItemReceipt = new DataTable();
            if (Session["dtItemReceipt"] == null)
                dtItemReceipt = createAdjTable();
            else
            {
                dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            }

            if (Adjustqty > 0)
            {
                if (UNIQUEID > 0)
                {
                    DataView dvItemRec = new DataView(dtItemReceipt);
                    dvItemRec.RowFilter = "YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'  AND COMP_CODE='" + oUserLoginDetail.COMP_CODE + "'  AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "'  AND TRN_TYPE='" + RET_TYPE + "'  AND TRN_NUMB='" + RET_NUMB + "'  AND ITEM_CODE='" + ITEM_CODE + "'";
                    if (dvItemRec.Count > 0)
                    {
                        dvItemRec[0]["ISSUE_QTY"] = Adjustqty;
                        dvItemRec[0]["FINAL_RATE"] = FINAL_RATE;
                        dtItemReceipt.AcceptChanges();
                    }
                }
                else
                {
                    DataRow dr = dtItemReceipt.NewRow();
                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["TRN_TYPE"] = RET_TYPE;
                    dr["TRN_NUMB"] = int.Parse(RET_NUMB);
                    dr["PO_COMP"] = "C99999";
                    dr["PO_BRANCH"] = "B99999";
                    dr["PO_TYPE"] = "MII";
                    dr["PO_NUMB"] = 999998;
                    dr["ITEM_CODE"] = ITEM_CODE;
                    dr["ISSUE_QTY"] = Adjustqty;
                    dr["FINAL_RATE"] = FINAL_RATE;
                    dtItemReceipt.Rows.Add(dr);
                }
            }
            Session["dtItemReceipt"] = dtItemReceipt;
        }
        catch
        {
            throw;
        }
    }

}
