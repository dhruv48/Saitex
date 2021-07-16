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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;

public partial class Module_Sewing_Thread_Controls_SWReceivingCredit : System.Web.UI.UserControl
{
    private DataTable dtTRN_SUB = null;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private string TRN_TYPE = "";

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            TRN_TYPE = "RYS01";
            ViewState["TRN_TYPE"] = TRN_TYPE;
            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();

            DateTime StartDate;
            DateTime EndDate;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            StartDate = oUserLoginDetail.DT_STARTDATE;
            EndDate = Common.CommonFuction.GetYearEndDate(StartDate);

            rvbill.MinimumValue = StartDate.ToShortDateString();
            rvbill.MaximumValue = EndDate.ToShortDateString();

            rvlr.MinimumValue = StartDate.ToShortDateString();
            rvlr.MaximumValue = EndDate.ToShortDateString();

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();

            Session["dtTRN_SUB"] = null;
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
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

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
            spnAInW.InnerText = "Amount In Words... ";
            txtDepartment.Text = "";
            txtFormRefNo.Text = "";
            txtFormType.Text = "";
            txtGateEntryDate.Text = "";
            txtLRDate.Text = "";
            txtLRNo.Text = "";
            txtMRNDate.Text = "";
            txtTRNNUMBer.Text = "";
            txtPartyAddress.Text = "";
            lblPartyCode.Text = string.Empty;
            txtPartyBillAmount.Text = "";
            txtPartyBillDate.Text = "";
            txtPartyBillNo.Text = "";
            txtPartyChallanDate.Text = "";
            txtPartyChallanNo.Text = "";
            ddlGateEntryNo.SelectedIndex = -1;
            txtGateEntryNo.Text = "";
            txtRemarks.Text = "";
            txtTransporterAddress.Text = "";
            lblTransporterCode.Text = string.Empty;
            txtVehicleNo.Text = "";

            ddlReceiptShift.SelectedIndex = 0;

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
            txtGateEntryNo.Text = "";
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
            {
                // InsertBlankRowInTable();
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
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
                //InsertBlankRowInTable();
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
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];


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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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

            if (lblPartyCode.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select party first.\r\n";
            }

            if (txtGateEntryNo.Text != "")
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Update Mode.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Data Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Page Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            Response.Redirect("~/Module/Yarn/SalesWork/Reports/YRN_ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Editing/ Deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

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
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oYRN_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

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
            oYRN_IR_MST.PRTY_CODE = lblPartyCode.Text;
            oYRN_IR_MST.PRTY_NAME = txtPartyAddress.Text;
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
            if (lblTransporterCode.Text == string.Empty)
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            int TRN_NUMB = 0;
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
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

    private void UpdateMaterialReceipt()
    {
        try
        {
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oYRN_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

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
            oYRN_IR_MST.PRTY_CODE = lblPartyCode.Text;
            oYRN_IR_MST.PRTY_NAME = txtPartyAddress.Text;
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
            if (lblTransporterCode.Text == string.Empty)
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
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

    private int GetdataByTRNNUMBer(int TRNNUMBer)
    {
        int iRecordFound = 0;
        try
        {
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

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

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim(), out dd))
                    txtPartyChallanDate.Text = dd.ToShortDateString();

                txtPartyChallanNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();

                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();

                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TAddress"].ToString().Trim();

                ddlGateEntryNo.SelectedText = dt.Rows[0]["GATE_NUMB"].ToString().Trim();
                txtGateEntryNo.Text = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

            }
            if (iRecordFound == 1)
            {
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetTRN_DataByTRN_NUMBst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                if (dtDetailTBL.Rows.Count > 0)
                {
                    MapDataTable();
                    if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                    {
                        //DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];
                        DataTable dtTRN_Sub = SaitexBL.Interface.Method.YRN_IR_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                        Session["dtTRN_SUB"] = dtTRN_Sub;


                    }

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

    private void ResetDetailOnPartySelection()
    {
        try
        {
            RefreshDetailRow();

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

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
                if (txtPONumb.Text != "" && txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                    bool bb = SearchItemCodeInGrid(txtICODE.Text.Trim(), int.Parse(txtPONumb.Text.Trim()), UNIQUEID);
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
                                    dv[0]["PO_TYPE"] = lblPO_TYPE.Text;
                                    dv[0]["PO_COMP_CODE"] = lblPO_COMP.Text;
                                    dv[0]["PO_BRANCH"] = lblPO_BRANCH.Text;
                                    dv[0]["YARN_CODE"] = txtICODE.Text.Trim();
                                    dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
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
                                    dv[0]["NO_OF_UNIT"] = txtNoOfUnit.Text;
                                    dv[0]["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                                    dv[0]["UOM_OF_UNIT"] = txtUOm.Text;
                                    dv[0]["PI_NO"] = "NA";

                                    dtDetailTBL.AcceptChanges();
                                }
                            }
                            else
                            {
                                DataRow dr = dtDetailTBL.NewRow();
                                dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                dr["PO_NUMB"] = txtPONumb.Text.Trim();
                                dr["PO_TYPE"] = lblPO_TYPE.Text;
                                dr["PO_COMP_CODE"] = lblPO_COMP.Text;
                                dr["PO_BRANCH"] = lblPO_BRANCH.Text;
                                dr["YARN_CODE"] = txtICODE.Text.Trim();
                                dr["YARN_DESC"] = txtDESC.Text.Trim();
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
                                dr["NO_OF_UNIT"] = txtNoOfUnit.Text;
                                dr["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                                dr["UOM_OF_UNIT"] = txtUOm.Text;
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
                grdMaterialItemReceipt.DataSource = dtDetailTBL;
                grdMaterialItemReceipt.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding transaction data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in cancelling transaction.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text, e.ItemsOffset);
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "PO_NUMB";
            cmbPOITEM.DataValueField = "po_Item_trn";
            cmbPOITEM.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;

            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Po Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetPOData(string text, int startOffset)
    {
        try
        {
            DataTable dt = new DataTable();
            if (lblPartyCode.Text != "")
            {
                string po_type = "PSM";

                string CommandText = "SELECT *FROM (SELECT *FROM (SELECT DISTINCT ( PT.PO_TYPE|| '@'|| PT.PO_NUMB|| '@'|| PT.YARN_CODE)po_Item_trn, pt.PO_NUMB, pt.YARN_CODE, pt.ORD_QTY, PM.PRTY_CODE, pt.BASIC_RATE, pt.FINAL_RATE, i.YARN_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)AS QTY_REM FROM YRN_PU_TRN pt, YRN_MST i, YRN_PU_MST pm WHERE pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.YARN_CODE = i.YARN_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB AND pm.PRTY_CODE = '" + lblPartyCode.Text + "') WHERE PO_NUMB LIKE :SearchQuery OR YARN_CODE LIKE :SearchQuery ORDER BY PO_NUMB) WHERE ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += "   AND PO_NUMB NOT IN(SELECT PO_NUMB FROM (SELECT * FROM (SELECT DISTINCT( PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.YARN_CODE) po_Item_trn,pt.PO_NUMB,pt.YARN_CODE,pt.ORD_QTY,PM.PRTY_CODE,pt.BASIC_RATE,pt.FINAL_RATE,i.YARN_DESC,NVL (PT.QTY_RCPT, 0) QTY_RCPT,NVL (PT.ORD_QTY, 0)- NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM YRN_PU_TRN pt,YRN_MST i,YRN_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb  AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.YARN_CODE = i.YARN_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or YARN_CODE LIKE :SearchQuery ORDER BY PO_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }

                string SortExpression = " order by PO_NUMB";
                string SearchQuery = text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            }
            else
            {
                CommonFuction.ShowMessage("Please select Party First");
            }


            return dt;
        }
        catch
        {
            throw;
        }
    }

    //protected DataTable GetPOData(string text, int PO_Numb, string Yarn_Code)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        if (lblPartyCode.Text != "")
    //        {
    //            string po_type = "PSM";
    //            string whereClause = " WHERE ( PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) >0 AND pt.YARN_CODE = i.YARN_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB AND PM.PRTY_CODE = '" + lblPartyCode.Text.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.YARN_CODE LIKE :SearchQuery) OR ( PM.CONF_FLAG = '1' AND pt.YARN_CODE = i.YARN_CODE AND pm.PO_NUMB = PT.PO_NUMB AND PT.PO_NUMB = '" + PO_Numb + "' AND PT.PO_TYPE = '" + po_type + "' AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PT.YARN_CODE = '" + Yarn_Code + "')";
    //            string sortExpression = " ORDER BY PO_NUMB";
    //            string commandText = " SELECT   DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.YARN_CODE) po_Item_trn,pt.COMP_CODE,pt.BRANCH_CODE,pt.PO_NUMB,pt.YARN_CODE, pt.ORD_QTY,i.YARN_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.QTY_RTN, 0) QTY_RTN, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM  FROM   YRN_PU_TRN pt, YRN_MST i, YRN_PU_MST pm";

    //            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
    //        }
    //        else
    //        {
    //            CommonFuction.ShowMessage("Please select Party First");
    //        }


    //        return dt;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = cmbPOITEM.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string PO_Type = arrString[0].ToString();
            int PONumb = int.Parse(arrString[1].ToString());
            txtPONumb.Text = PONumb.ToString();
            string YARN_CODE = arrString[2].ToString();
            GetDataForDetail(PO_Type, PONumb, YARN_CODE);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Po Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void GetDataForDetail(string PO_Type, int PONumb, string YARN_CODE)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(YARN_CODE, PONumb, UNIQUEID))
            {
                DataTable dt = SaitexBL.Interface.Method.YRN_PU_MST.GetTRNData_ForReceiving(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PO_Type, PONumb, YARN_CODE, string.Empty,"",oUserLoginDetail.DT_STARTDATE.Year);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtICODE.Text = dt.Rows[0]["YARN_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["YARN_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
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
            txtPONumb.Text = string.Empty;
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
            txtNoOfUnit.Text = "";
            txtWeightOfUnit.Text = "";
            txtUOm.Text = "";
            lblMaxQTY.Text = "";
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
            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                string cString = dv[0]["PO_TYPE"].ToString() + "@" + dv[0]["PO_NUMB"].ToString() + "@" + dv[0]["YARN_CODE"].ToString();
                // BindCMBPOITEM(cString, double.Parse(dv[0]["TRN_QTY"].ToString()));

                txtPONumb.Text = dv[0]["PO_NUMB"].ToString();

                cmbPOITEM.SelectedValue = cString;

                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                txtICODE.Text = dv[0]["YARN_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
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
                cmbPOITEM.Enabled = false;

                DataTable dt = SaitexBL.Interface.Method.YRN_PU_MST.GetTRNData_ForReceiving(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, dv[0]["PO_TYPE"].ToString(), int.Parse(dv[0]["PO_NUMB"].ToString()), dv[0]["ITEM_CODE"].ToString(), string.Empty, "", oUserLoginDetail.DT_STARTDATE.Year);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblMaxQTY.Text = (double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()) + double.Parse(dv[0]["TRN_QTY"].ToString())).ToString();
                }
            }
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Entry Detail selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    private DataTable GetLOVForGate(string text, int startOffset)
    {
        try
        {
            DataTable data = null;
            string CommandText = string.Empty;
            string whereClause = string.Empty;

            if (string.Compare(lblMode.Text, "Save", true) != 1)
            {
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'SEWING THREAD IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15";
            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {
                CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'SEWING THREAD IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15 ";
            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "Save", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE,a.prty_code,a.prty_name,a.trsp_code,a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='SEWING THREAD IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }
                else if (string.Compare(lblMode.Text, "Update", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE, a.trsp_code, a.trsp_name,A.ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='SEWING THREAD IN')asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0)asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB)WHERE ROWNUM <= " + startOffset + ")";
                }
            }

            string SortExpression = " ORDER BY GATE_NUMB ";
            string SearchQuery = text + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

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
        string CommandText = "";
        if (string.Compare(lblMode.Text, "Save", true) != 1)
        {
            CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'SEWING THREAD IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
        }
        else if (string.Compare(lblMode.Text, "Update", true) != 1)
        {
            CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'SEWING THREAD IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
        }

        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlGateEntryNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            ResetDetailOnPartySelection();

            string cString = ddlGateEntryNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            DateTime GateDate = DateTime.Parse(arrString[0].ToString());
            string VahicleNo = arrString[1].ToString();
            string PartyChallanNo = arrString[2].ToString();
            DateTime partyChallanDate = DateTime.Parse(arrString[3].ToString());

            txtGateEntryNo.Text = ddlGateEntryNo.SelectedText.Trim();
            txtGateEntryDate.Text = GateDate.ToShortDateString();
            txtVehicleNo.Text = VahicleNo;
            txtPartyChallanNo.Text = PartyChallanNo;
            txtPartyChallanDate.Text = partyChallanDate.ToShortDateString();
            lblPartyCode.Text = arrString[4].ToString();
            txtPartyAddress.Text = arrString[5].ToString();
            lblTransporterCode.Text = arrString[6].ToString();
            txtTransporterAddress.Text = arrString[7].ToString();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Entry Detail selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected void ddlTRNNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            //thisTextBox.SelectedIndex = 0;
            //thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetReceiving(e.Text.ToUpper(), e.ItemsOffset);
            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "TRN_NUMB";
            thisTextBox.DataValueField = "TRN_NUMB";
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetReceivingCount(e.Text);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Indent for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetReceiving(string text, int startOffset)
    {
        try
        {
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND trn_numb NOT IN (SELECT trn_numb FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";

            string SearchQuery = text + "%";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, sortExpression, "", SearchQuery, "");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetReceivingCount(string text)
    {
        try
        {
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery)";
            string whereClause = string.Empty;

            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";
            string SearchQuery = text + "%";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, sortExpression, "", SearchQuery, "");

            return dt.Rows.Count;
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

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();
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
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party BillAmount Text Changed.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnSubDetail_Click1(object sender, EventArgs e)
    {
        try
        {

            if (txtPONumb.Text != "")
            {
                string URL = "SW_TRN_SUB.aspx";
                URL = URL + "?YARN_CODE=" + txtICODE.Text;
                URL = URL + "&PO_NUMB=" + txtPONumb.Text.Trim();
                URL = URL + "&PO_TYPE=" + lblPO_TYPE.Text;
                URL = URL + "&PO_COMP_CODE=" + lblPO_COMP.Text;
                URL = URL + "&PO_BRANCH=" + lblPO_BRANCH.Text;
                URL = URL + "&lblMaxQTY=" + lblMaxQTY.Text;
                URL = URL + "&txtQTY=" + txtQTY.ClientID;
                URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
                URL = URL + "&txtWeightOfUnit=" + txtWeightOfUnit.ClientID;
                URL = URL + "&txtUOm=" + txtUOm.ClientID;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=850,height=320,left=200,top=300');", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select PO Number");
            }
        }
        catch
        {
        }
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

                        if (Session["dtTRN_SUB"] != null)
                        {
                            DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];

                            DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
                            dvYRNDRecieve_trn.RowFilter = "YARN_CODE='" + YARN_CODE + "'";
                            if (dvYRNDRecieve_trn.Count > 0)
                            {
                                GridView grdBOM = e.Row.FindControl("grdBOM") as GridView;
                                grdBOM.DataSource = dvYRNDRecieve_trn;
                                grdBOM.DataBind();
                            }

                        }

                    }

                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Material GridRow DataBound.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

    //protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = GetLOVForParty(e.Text.ToUpper(), e.ItemsOffset);
    //        txtPartyCode.Items.Clear();

    //        txtPartyCode.DataSource = data;
    //        txtPartyCode.DataTextField = "PRTY_CODE";
    //        txtPartyCode.DataValueField = "address";
    //        txtPartyCode.DataBind();

    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    //        e.ItemsCount = GetPartyCount(e.Text);
    //    }

    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nsee error log for detail"));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //private DataTable GetLOVForParty(string Text, int startOffset)
    //{
    //    try
    //    {
    //        string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1 FROM (SELECT *FROM (SELECT DISTINCT v.PRTY_CODE, PO_TYPE, PRTY_STATE, PRTY_ADD1, PRTY_ADD2, PRTY_NAME,PRTY_ADD1 || ',' || NVL (PRTY_ADD2, ' ') || ',' || NVL (PRTY_STATE, ' ')address FROM TX_VENDOR_MST v RIGHT OUTER JOIN YRN_PU_MST p ON V.PRTY_CODE = P.PRTY_CODE) a WHERE PO_TYPE = 'PSM') b WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= 15 ";
    //        string whereClause = string.Empty;
    //        if (startOffset != 0)
    //        {
    //            whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE FROM (SELECT * FROM (SELECT DISTINCT v.PRTY_CODE, PO_TYPE, PRTY_STATE, PRTY_ADD1, PRTY_ADD2, PRTY_NAME,PRTY_ADD1 || ',' || NVL (PRTY_ADD2, ' ') || ',' || NVL (PRTY_STATE, ' ')address FROM TX_VENDOR_MST v RIGHT OUTER JOIN YRN_PU_MST p ON V.PRTY_CODE = P.PRTY_CODE) a WHERE PO_TYPE = 'PSM') b  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= " + startOffset + ")";
    //        }

    //        string SortExpression = " order by PRTY_CODE";
    //        string SearchQuery = Text + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //protected int GetPartyCount(string text)
    //{

    //    string CommandText = " SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1 FROM (SELECT *FROM (SELECT DISTINCT v.PRTY_CODE, PO_TYPE, PRTY_STATE, PRTY_ADD1, PRTY_ADD2, PRTY_NAME,PRTY_ADD1 || ',' || NVL (PRTY_ADD2, ' ') || ',' || NVL (PRTY_STATE, ' ')address FROM TX_VENDOR_MST v RIGHT OUTER JOIN YRN_PU_MST p ON V.PRTY_CODE = P.PRTY_CODE) a WHERE PO_TYPE = 'PSM') b WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd ";
    //    string WhereClause = " ";
    //    string SortExpression = " ";
    //    string SearchQuery = text.ToUpper() + "%";
    //    DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //    return data.Rows.Count;
    //}

    //protected void txtPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    //{
    //    try
    //    {
    //        ResetDetailOnPartySelection();

    //        txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
    //        lblPartyCode.Text = txtPartyCode.SelectedText.Trim();

    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nsee error log for detail"));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //protected void txtTransporterCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = GetLOVForTransporter(e.Text, e.ItemsOffset);

    //        txtTransporterCode.Items.Clear();

    //        txtTransporterCode.DataSource = data;
    //        txtTransporterCode.DataTextField = "PRTY_CODE";
    //        txtTransporterCode.DataValueField = "Address";
    //        txtTransporterCode.DataBind();

    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    //        e.ItemsCount = GetPartyCount(e.Text);
    //    }

    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nsee error log for detail"));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //private DataTable GetLOVForTransporter(string Text, int startOffset)
    //{
    //    try
    //    {
    //        string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= 15 ";
    //        string whereClause = string.Empty;
    //        if (startOffset != 0)
    //        {
    //            whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= " + startOffset + ")";
    //        }

    //        string SortExpression = " order by PRTY_CODE";
    //        string SearchQuery = Text + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //protected int GetTransporterCount(string text)
    //{

    //    string CommandText = "SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') ";
    //    string WhereClause = " ";
    //    string SortExpression = " ";
    //    string SearchQuery = text.ToUpper() + "%";
    //    DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //    return data.Rows.Count;
    //}

    //protected void txtTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    //{
    //    try
    //    {
    //        txtTransporterAddress.Text = txtTransporterCode.SelectedValue;
    //        lblTransporterCode.Text = txtTransporterCode.SelectedText;
    //    }

    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nsee error log for detail"));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

}
