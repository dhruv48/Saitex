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

public partial class Module_Fiber_Controls_Fiber_Receiving_Miss : System.Web.UI.UserControl
{
    private DataTable dtTRN_SUB = null;
    private DataTable dtInvoiceDicRateMST = null;
    private DataTable dtDicRate = null;
    private DateTime StartDate;
    private DateTime EndDate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private string  TRN_TYPE = "RBJ01";
    private int IsUpdateCall = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            } 
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }

            rvbill.MinimumValue = StartDate.ToShortDateString();
            rvbill.MaximumValue = EndDate.ToShortDateString();

            rvlr.MinimumValue = StartDate.ToShortDateString();
            rvlr.MaximumValue = EndDate.ToShortDateString();
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Page.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
           //RBS01 -> TRN_TYPE in FIBER for Receiving against PO......
            ViewState["TRN_TYPE"] = TRN_TYPE;
            ActivateSaveMode();
            BlankRecords();
            BindNewMRNNum();
            BindShift();
            

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();


            Session["dtTRN_SUB"] = null;
            Session["dtInvoiceDicRateMST"] = null;
            Session["dtDicRate"] = null;
            if(Session["dtDetailTBL"] != null)
                Session["dtDetailTBL"] = null;

        }
        catch 
        {
            
            throw;
        }
    }

    protected void BindShift()
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

    protected void BindNewMRNNum()
    {
        try
        {
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_TR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
            oTX_FIBER_TR_MST.TRN_TYPE = TRN_TYPE;
            oTX_FIBER_TR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FIBER_TR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FIBER_TR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            txtTRNNUMBer.Text = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetNewMRNNumber(oTX_FIBER_TR_MST).ToString();
         
        }
        catch (Exception)
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

    private void BlankRecords()
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
            txtPartyBillAmount.Text = "";
            txtPartyBillDate.Text = "";
            txtPartyBillNo.Text = "";
            txtPartyChallanDate.Text = "";
            txtPartyChallanNo.Text = "";

            ddlGateEntryNo.SelectedIndex = -1;
            txtGateEntryNo.Text = "";
            txtRemarks.Text = "";
            txtTransporterAddress.Text = "";
            txtTotalAmount.Text = string.Empty;
            txtVehicleNo.Text = "";

            lblPartyCode.Text = string.Empty;
            lblTransporterCode.Text = string.Empty;
            txtTotalPartyAmt.Text = string.Empty;
            txtTotalAmount.Enabled = true;
            txtTotalAmount.Text = string.Empty;
            txtTotalAmount.Enabled = false ;
            

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
            CalculateTotalAmount();
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
            cmbPOITEM.Enabled = true;
            cmbPOITEM.SelectedIndex = -1;
            txtICODE.Text = "";
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtFinalRate.Text = "";
            txtAmount.Text = "";
           
            txtDOM.Text = "";
            txtDetRemarks.Text = "";
           

           
            txtNoOfUnit.Text = "";
            txtWeightOfUnit.Text = "";
            txtUOm.Text = "";
            lblMaxQTY.Text = "";
            ViewState["UNIQUEID"] = null;
            txtNoOfUnit.Text = string.Empty;
            txtWeightOfUnit.Text = string.Empty;
            txtUOm.Text = string.Empty;
            txtkg_bail.Text = string.Empty;          
            
            //txtMergeNo.Text = string.Empty;
            //txtGrade.Text = string.Empty;
            //txtPalletCode.Text = string.Empty;
            txtMergeNo.SelectedIndex = -1;
            txtGrade.SelectedIndex = -1;
            txtPalletCode.SelectedIndex = -1;

        }
        catch 
        {
            
            throw;
        }
    }

    protected void CalculateTotalAmount()
    {
        try
        {
            double TotalAmount = 0;
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    double Amount = 0;
                    double.TryParse(dr["Amount"].ToString(), out Amount);
                    TotalAmount += Amount;

                }
            }
            txtTotalAmount.Text =  Math.Round(TotalAmount,3).ToString();

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
            dtDetailTBL.Columns.Add("FIBER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("FIBER_DESC", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("UOM1", typeof(string));
            dtDetailTBL.Columns.Add("UOM_BAIL", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));

            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("PALLET_CODE", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
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
            dr["DATE_OF_MFG"] = System.DateTime.Now.ToShortDateString();
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
            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();
            CalculateTotalAmount();
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
                dtDetailTBL.Rows.Clear();
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

            if (count == 3)
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
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htReceive = new Hashtable();
            SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
            oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FIBER_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_FIBER_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oTX_FIBER_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oTX_FIBER_IR_MST.GATE_DATE = dt;
            oTX_FIBER_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
            oTX_FIBER_IR_MST.GATE_OUT_NUMB = "";
            oTX_FIBER_IR_MST.GATE_PASS_TYPE = "";
            oTX_FIBER_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());


            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oTX_FIBER_IR_MST.LR_DATE = dt;

            oTX_FIBER_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());


            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FIBER_IR_MST.PRTY_CH_DATE = dt;


            oTX_FIBER_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oTX_FIBER_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
            oTX_FIBER_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
            oTX_FIBER_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FIBER_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oTX_FIBER_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();


            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oTX_FIBER_IR_MST.TRN_DATE = dt;

            oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oTX_FIBER_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            oTX_FIBER_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oTX_FIBER_IR_MST.TUSER = oUserLoginDetail.UserCode;
            int TRN_NUMB = 0;

            oTX_FIBER_IR_MST.BILL_NUMB = txtPartyBillNo.Text;


            dt = System.DateTime.Now.Date;
            bool Is_Bill_Date = false;
            Is_Bill_Date = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("BILL_DATE", Is_Bill_Date);
            oTX_FIBER_IR_MST.BILL_DATE = dt;

          

            oTX_FIBER_IR_MST.BILL_TYPE = "FSP";
            oTX_FIBER_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;


            double totalAmt = 0;
            double.TryParse(txtTotalAmount.Text , out totalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(txtPartyBillAmount.Text, out finalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = finalAmt;
            oTX_FIBER_IR_MST.EXCISE_TYPE = string.Empty;
            oTX_FIBER_IR_MST.SPINNERS = string.Empty;
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (Session["dtInvoiceDicRateMST"] != null)
                dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];

            if (Session["dtDicRate"] != null)
                dtDicRate = (DataTable)Session["dtDicRate"];

            if (Session["dtTRN_SUB"] != null)
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];


            double dblPartyBillAmt = 0;
            double dblTotalAmount = 0;
            double dblFinalAmount = 0;


            double.TryParse(txtTotalPartyAmt.Text.Trim(), out dblPartyBillAmt);
            double.TryParse(txtTotalAmount.Text.Trim(), out dblTotalAmount);
            double.TryParse(txtPartyBillAmount.Text.Trim(), out dblFinalAmount);


            if (dblPartyBillAmt != dblTotalAmount && dblPartyBillAmt != dblFinalAmount)
            {
                bool Result = SaitexBL.Interface.Method.TX_FIBER_IR_MST.Insert(oTX_FIBER_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
                if (Result)
                {
                    InitialisePage();
                    string msg = string.Empty;
                    msg += " Either Total Amount or Final Amount is not matched with Party Bill Amount.";
                    msg += @"\r\n MRN Number : " + TRN_NUMB + " saved successfully.";
                    CommonFuction.ShowMessage(msg);

                    if (Session["dtInvoiceDicRateMST"] != null)
                        Session["dtInvoiceDicRateMST"] = null;

                    if (Session["dtDicRate"] != null)
                        Session["dtDicRate"] = null;

                    if (Session["dtTRN_SUB"] != null)
                        Session["dtTRN_SUB"] = null;

                    if (ViewState["dtDetailTBL"] != null)
                        ViewState["dtDetailTBL"] = null;
                }
                else
                {
                    CommonFuction.ShowMessage("Data  Saving Failed");
                }
            }

            else
            { 
            bool Result = SaitexBL.Interface.Method.TX_FIBER_IR_MST.Insert(oTX_FIBER_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
            if (Result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"MRN Number : " + TRN_NUMB + " Saved successfully!!");


                if (Session["dtInvoiceDicRateMST"] != null)
                    Session["dtInvoiceDicRateMST"] = null;

                if (Session["dtDicRate"] != null)
                    Session["dtDicRate"] = null;

                if (Session["dtTRN_SUB"] != null)
                    Session["dtTRN_SUB"] = null;

                if (ViewState["dtDetailTBL"] != null)
                    ViewState["dtDetailTBL"] = null;

            }
            else
            {
                CommonFuction.ShowMessage("Data  Saving Failed");
            }
            }
        }
        catch (Exception)
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
            txtTRNNUMBer.Text= "";
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception)
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
                
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Data Page.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
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

                SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
                oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_FIBER_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                oTX_FIBER_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
                oTX_FIBER_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

                DateTime dt = System.DateTime.Now.Date;
                bool Is_Gate_Entry = false;
                Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
                htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
                oTX_FIBER_IR_MST.GATE_DATE = dt;

                oTX_FIBER_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
                oTX_FIBER_IR_MST.GATE_OUT_NUMB = "";
                oTX_FIBER_IR_MST.GATE_PASS_TYPE = "";
                oTX_FIBER_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

                dt = System.DateTime.Now.Date;
                bool Is_LR = false;
                Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
                htReceive.Add("LR", Is_LR);
                oTX_FIBER_IR_MST.LR_DATE = dt;

                oTX_FIBER_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

                dt = System.DateTime.Now.Date;
                bool Is_Party_challan = false;
                Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
                htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
                oTX_FIBER_IR_MST.PRTY_CH_DATE = dt;

                oTX_FIBER_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
                oTX_FIBER_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
                oTX_FIBER_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
                oTX_FIBER_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                oTX_FIBER_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
                oTX_FIBER_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

                dt = System.DateTime.Now.Date;
                bool Is_MRN = false;
                Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
                htReceive.Add("MRN", Is_MRN);
                oTX_FIBER_IR_MST.TRN_DATE = dt;

                oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
                oTX_FIBER_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);

                oTX_FIBER_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

                oTX_FIBER_IR_MST.TUSER = oUserLoginDetail.UserCode;


                oTX_FIBER_IR_MST.BILL_NUMB = txtPartyBillNo.Text;

                dt = System.DateTime.Now.Date;
                bool Is_Bill_Date = false;
                Is_Bill_Date = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
                htReceive.Add("BILL_DATE", Is_Bill_Date);
                oTX_FIBER_IR_MST.BILL_DATE = dt;

                oTX_FIBER_IR_MST.BILL_TYPE = "FSP";
                oTX_FIBER_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                double totalAmt = 0;
                double.TryParse(txtTotalAmount.Text, out totalAmt);
                oTX_FIBER_IR_MST.TOTAL_AMOUNT = totalAmt;

                double finalAmt = 0;
                double.TryParse(txtPartyBillAmount.Text, out finalAmt);
                oTX_FIBER_IR_MST.FINAL_AMOUNT = finalAmt;
                oTX_FIBER_IR_MST.EXCISE_TYPE = string.Empty;
                oTX_FIBER_IR_MST.SPINNERS = string.Empty;
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

                if (Session["dtInvoiceDicRateMST"] != null)
                    dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];

                if (Session["dtDicRate"] != null)
                    dtDicRate = (DataTable)Session["dtDicRate"];

                if (Session["dtTRN_SUB"] != null)
                {
                    dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                }

                // Show message to the user that Total Amount OR Final Amount is not same as party Bill Amount.
                // Only message show then saved, .....  

                double dblPartyBillAmt = 0;     // For Party Bill Amount
                double dblTotalAmount = 0;       // Total Amount
                double dblFinalAmount = 0;  // For Final Amount

                double.TryParse(txtTotalPartyAmt.Text.Trim(), out dblPartyBillAmt);
                double.TryParse(txtTotalAmount.Text.Trim(), out dblTotalAmount);
                double.TryParse(txtPartyBillAmount.Text.Trim(), out dblFinalAmount);

                if (dblPartyBillAmt != dblTotalAmount && dblPartyBillAmt != dblFinalAmount)
                {
                    
                    bool result = SaitexBL.Interface.Method.TX_FIBER_IR_MST.Update(oTX_FIBER_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
                    if (result)
                    {
                        InitialisePage();
                        string Msg = string.Empty;
                        Msg += " Either Total Amount or Final Amount is not matched with Party Bill Amount.";
                        Msg += @"\r\n MRN Number : " + oTX_FIBER_IR_MST.TRN_NUMB + " Updated successfully.";
                        CommonFuction.ShowMessage(Msg);
                        

                        if (Session["dtInvoiceDicRateMST"] != null)
                            Session["dtInvoiceDicRateMST"] = null;

                        if (Session["dtDicRate"] != null)
                            Session["dtDicRate"] = null;

                        if (Session["dtTRN_SUB"] != null)
                        {
                            Session["dtTRN_SUB"] = null;
                        }

                        if (ViewState["dtDetailTBL"] != null)
                            ViewState["dtDetailTBL"] = null;

                    }
                    else
                    {
                        CommonFuction.ShowMessage("Data updation Failed");
                    }

                }
                else
                {
                    bool result = SaitexBL.Interface.Method.TX_FIBER_IR_MST.Update(oTX_FIBER_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
                    if (result)
                    {
                        InitialisePage();
                        string Msg = string.Empty;
                        Msg += " MRN Number : " + oTX_FIBER_IR_MST.TRN_NUMB + " Updated successfully.";
                        CommonFuction.ShowMessage(Msg);

                        if (Session["dtInvoiceDicRateMST"] != null)
                            Session["dtInvoiceDicRateMST"] = null;

                        if (Session["dtDicRate"] != null)
                            Session["dtDicRate"] = null;

                        if (Session["dtTRN_SUB"] != null)
                        {
                            Session["dtTRN_SUB"] = null;
                        }

                        if (ViewState["dtDetailTBL"] != null)
                            ViewState["dtDetailTBL"] = null;
                    }
                    else
                    {
                        CommonFuction.ShowMessage("data updation Failed");
                    }
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
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Page Data.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }
        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Module/Fiber/Reports/Fiber_ReceiptPremRTP.aspx?TRN_TYPE="+TRN_TYPE);
        }
        protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Session["RedirectURL"] != null)
                {
                    Response.Redirect(Session["RedirectURL"].ToString());
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
        protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

                if (dtDetailTBL == null)
                    CreateDataTable();
                if (txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                    
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                        bool bb = SearchItemCodeInGrid(txtICODE.Text.Trim(), UNIQUEID);
                        if (!bb)
                        {
                            double Qty = 0;
                            double.TryParse(txtQTY.Text.Trim(), out Qty);
                            if (Qty > 0)
                            {
                                if (UNIQUEID > 0)
                                {
                                    DataView dv = new DataView(dtDetailTBL);
                                    dv.RowFilter = " UNIQUEID = " + UNIQUEID;
                                    if (dv.Count > 0)
                                    {
                                        dv[0]["PO_NUMB"] = 999999;
                                        dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                        dv[0]["PO_TYPE"] = "PMR";
                                        dv[0]["PO_COMP_CODE"] = "C99999";
                                        dv[0]["PO_BRANCH"] = "B99999";
                                        dv[0]["FIBER_CODE"] = txtICODE.Text.Trim();
                                        dv[0]["FIBER_DESC"] = txtDESC.Text.Trim();
                                        dv[0]["TRN_QTY"] =  Math.Round((double.Parse(txtQTY.Text.Trim())),3);
                                        dv[0]["UOM"] = txtUNIT.Text.Trim();
                                        dv[0]["UOM1"] = txtUOm.Text.Trim();
                                        dv[0]["UOM_BAIL"] = txtkg_bail.Text.Trim();
                                        dv[0]["BASIC_RATE"] = Math.Round( double.Parse(txtBasicRate.Text.Trim()),3);
                                        dv[0]["FINAL_RATE"] =  Math.Round(double.Parse(txtFinalRate.Text.Trim()),3);
                                        dv[0]["AMOUNT"] =  Math.Round(double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim()),3);
                                        dv[0]["COST_CENTER_CODE"] = "";
                                        dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                        dv[0]["QCFLAG"] = "No";
                                        DateTime dd = System.DateTime.Now;
                                        DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                        dv[0]["DATE_OF_MFG"] = dd;
                                        dv[0]["NO_OF_UNIT"] = txtNoOfUnit.Text;
                                        dv[0]["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                                        dv[0]["UOM_OF_UNIT"] = txtUOm.Text;
                                        dv[0]["PI_NO"] = "NA";
                                        //dv[0]["LOT_NO"] = txtMergeNo.Text.ToUpper().Trim();
                                        //dv[0]["PALLET_CODE"] = txtPalletCode.Text.ToUpper().Trim();
                                        //dv[0]["GRADE"] = txtGrade.Text.ToUpper().Trim();
                                        dv[0]["LOT_NO"] = txtMergeNo.SelectedValue.ToUpper().Trim();
                                        dv[0]["PALLET_CODE"] = txtPalletCode.SelectedValue.ToUpper().Trim();
                                        dv[0]["GRADE"] = txtGrade.SelectedValue.ToUpper().Trim();
                                        dtDetailTBL.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtDetailTBL.NewRow();
                                    dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                    dr["PO_NUMB"] = 999999;
                                    dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                    dr["PO_TYPE"] = "PMR";
                                    dr["PO_COMP_CODE"] = "C99999";
                                    dr["PO_BRANCH"] = "B99999";
                                    dr["FIBER_CODE"] = txtICODE.Text.Trim();
                                    dr["FIBER_DESC"] = txtDESC.Text.Trim();
                                    dr["TRN_QTY"] =  Math.Round(double.Parse(txtQTY.Text.Trim()),3);
                                    dr["UOM"] = txtUNIT.Text.Trim();
                                    dr["UOM1"] = txtUOm.Text.Trim();
                                    dr["UOM_BAIL"] = txtkg_bail.Text.Trim();
                                    dr["BASIC_RATE"] =  Math.Round(double.Parse(txtBasicRate.Text.Trim()),3);
                                    dr["FINAL_RATE"] =  Math.Round(double.Parse(txtFinalRate.Text.Trim()),3);
                                    dr["AMOUNT"] =  Math.Round(double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim()),3);
                                    dr["COST_CENTER_CODE"] = "";
                                    dr["MAC_CODE"] = string.Empty;
                                    dr["REMARKS"] = txtDetRemarks.Text.Trim();                                   
                                    dr["QCFLAG"] = "No";
                                    DateTime dd = System.DateTime.Now;
                                    DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                    dr["DATE_OF_MFG"] = dd;
                                    dr["NO_OF_UNIT"] = txtNoOfUnit.Text;
                                    dr["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                                    dr["UOM_OF_UNIT"] = txtUOm.Text;
                                    dr["PI_NO"] = "NA";
                                    dr["LOT_NO"] = txtMergeNo.SelectedValue.ToUpper().Trim();
                                    dr["PALLET_CODE"] = txtPalletCode.SelectedValue.ToUpper().Trim();
                                    dr["GRADE"] = txtGrade.SelectedValue.ToUpper().Trim();
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
                    BindGridFromDataTable();
                 
                }
            

            catch (Exception)
            {

                throw;
            }
        }

        private bool SearchItemCodeInGrid(string ItemCode,  int UNIQUEID)
        {
            bool Result = false;
            try
            {
               
                if (grdMaterialItemReceipt.Rows.Count > 0)
                {
                    foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                    {
                        LinkButton txtICODE = (LinkButton)grdRow.FindControl("txtICODE");
                        //Label txtPONum = (Label)grdRow.FindControl("txtPONum");
                        LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                        Label lblPalletCode = (Label)grdRow.FindControl("txtPalletCode");
                        Label lblMergeNo = (Label)grdRow.FindControl("txtMergeNo");
                        int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                        if (txtICODE.Text.Trim() == ItemCode && UNIQUEID != iUNIQUEID && txtMergeNo.SelectedValue.Trim().ToUpper() == lblMergeNo.Text.Trim().ToUpper() && txtPalletCode.SelectedValue.Trim().ToUpper() == lblPalletCode.Text.Trim().ToUpper())
                        {
                            Result = true;
                        }
                    }
                }
                return Result;
            }
            catch (Exception)
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
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in cancelling transaction.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }

        

        private DataTable GetPOData(string text, int startOffset)
        {
            try
            {
                DataTable dt = new DataTable();
                if (lblPartyCode.Text  != "")
                {
                    string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT        (FIBER_CAT || '@' || FIBER_DESC || '@' || FIBER_CODE|| '@' ||UOM|| '@' ||UOM1|| '@' ||UOM_BAIL)                        Item_trn,      FIBER_CAT,      FIBER_CODE,   FIBER_DESC   FROM   TX_FIBER_MASTER  pt             WHERE   COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'                     AND (FIBER_DESC LIKE :SearchQuery      OR FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery)    ORDER BY   FIBER_DESC) WHERE   ROWNUM <= 15";
                    string whereClause = string.Empty;
                    if (startOffset != 0)
                    {
                        whereClause += " AND Item_trn NOT IN  ( SELECT Item_trn FROM (SELECT   DISTINCT        (FIBER_CAT || '@' || FIBER_DESC || '@' || FIBER_CODE|| '@' || UOM|| '@' ||UOM1|| '@' ||UOM_BAIL)                        Item_trn,      FIBER_CAT,      FIBER_CODE,   FIBER_DESC   FROM   TX_FIBER_MASTER  pt             WHERE   COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'                     AND (FIBER_DESC LIKE :SearchQuery      OR FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery)    ORDER BY   FIBER_DESC) WHERE ROWNUM <='" + startOffset + "')";
                    }

                    string SortExpression = " order by FIBER_DESC";
                    string SearchQuery = text + "%";
                    dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

                }
                else 
                {
                    CommonFuction.ShowMessage("Please select Party First");
                }
                return dt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
        {
            try
            {
                DataTable data = GetPOData(e.Text, e.ItemsOffset);
                cmbPOITEM.Items.Clear();
                cmbPOITEM.DataSource = data;
                cmbPOITEM.DataTextField = "FIBER_CODE";
                cmbPOITEM.DataValueField = "Item_trn";
                cmbPOITEM.DataBind();


                e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

                e.ItemsCount = GetPODataCount(e.Text);


            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Po Loading.\r\nSee error log for detail."));

                lblMode.Text = ex.ToString();
            }
        }

        protected int GetPODataCount(string text)
        {
            try
            {
                DataTable dt = new DataTable();
                if (lblPartyCode.Text != "")
                {

                    string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT        (FIBER_CAT || '@' || FIBER_DESC || '@' || FIBER_CODE)                        Item_trn,      FIBER_CAT,      FIBER_CODE,   FIBER_DESC   FROM   TX_FIBER_MASTER  pt             WHERE   COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'                     AND (FIBER_DESC LIKE :SearchQuery      OR FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery)    ORDER BY   FIBER_DESC) ";
                    string whereClause = string.Empty;

                    string SortExpression = " order by FIBER_DESC";
                    string SearchQuery = text + "%";
                    dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

                }
                else
                {
                    CommonFuction.ShowMessage("Please select Party First");
                }

                return dt.Rows.Count;

            }
            catch (Exception)
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
                    data = GetPOData("", 0);
                else
                {
                    char[] splitter = { '@' };
                    string[] arrString = cString.Split(splitter);
                    string PO_TYPE = arrString[0].ToString();
                    int PONumb = int.Parse(arrString[1].ToString());
                    string FIBER_CODE = arrString[2].ToString();
                    data = GetPOData("", PONumb, FIBER_CODE);


                    if (data != null && data.Rows.Count > 0)
                    {
                        DataView dv = new DataView(data);
                        dv.RowFilter = "COMP_CODE = ' " + oUserLoginDetail.COMP_CODE + " '  and BRANCH_CODE = ' " + oUserLoginDetail.CH_BRANCHCODE + " ' and PO_NUMB = ' " + PONumb + " ' and FIBER_CODE = '" + FIBER_CODE + " ' ";
                        if (dv.Count > 0)
                        {
                            lblMaxQTY.Text = (double.Parse(dv[0]["QTY_REM"].ToString()) + TRN_QTY).ToString();
                        }
                    }

                }

                cmbPOITEM.Items.Clear();
                cmbPOITEM.DataSource = data;
                cmbPOITEM.DataTextField = "PO_NUMB";
                cmbPOITEM.DataValueField = "po_Item_trn";
                cmbPOITEM.DataBind();
            }
            catch
            {

                throw;
            }
        }

        protected DataTable GetPOData(string text, int PO_Numb, string FIBER_CODE)
        {
            try
            {
                 DataTable dt = new DataTable();
            if (lblPartyCode.Text != "")
            {
                string po_type = "PUM";
                string whereClause = " WHERE ( PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) >0 AND pt.FIBER_CODE = i.FIBER_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB AND PM.PRTY_CODE = '" + lblPartyCode.Text.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.FIBER_CODE LIKE :SearchQuery) OR ( PM.CONF_FLAG = '1' AND pt.FIBER_CODE = i.FIBER_CODE AND pm.PO_NUMB = PT.PO_NUMB AND PT.PO_NUMB = '" + PO_Numb + "' AND PT.PO_TYPE = '" + po_type + "' AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PT.FIBER_CODE = '" + FIBER_CODE + "')";
                string sortExpression = " ORDER BY PO_NUMB";
                string commandText = " SELECT   DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.FIBER_CODE) po_Item_trn, pt.COMP_CODE,pt.BRANCH_CODE,pt.PO_NUMB,pt.FIBER_CODE, pt.ORD_QTY,i.FIBER_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.QTY_RTN, 0) QTY_RTN, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM  FROM   TX_FIBER_PU_TRN pt, TX_FIBER_MASTER i, TX_FIBER_PU_MST pm";

                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
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

        protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMergeNo.SelectedValue) & !string.IsNullOrEmpty(txtPalletCode.SelectedValue))
                {
                    string cString = cmbPOITEM.SelectedValue.Trim();
                    char[] splitter = { '@' };
                    string[] arrString = cString.Split(splitter);
                  
                    txtDESC.Text= arrString[1].ToString();
                    txtICODE.Text = arrString[2].ToString();
                    txtUNIT.Text = arrString[3].ToString();
                    txtUOm.Text = arrString[4].ToString();
                    txtkg_bail.Text = arrString[5].ToString();
                    lblMaxQTY.Text = "999999";
                    txtBasicRate.Text = "0";
                    txtFinalRate.Text = "0";
                    txtAmount.Text = "0";
                    txtDOM.Text = DateTime.Now.Date.ToShortDateString();
                    //GetDataForDetail(PO_TYPE, PONumb, FIBER_CODE);
                }
                else 
                {
                    cmbPOITEM.SelectedIndex = -1;
                    CommonFuction.ShowMessage("Please enter merge no and pallet code first.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetDataForDetail(string PO_TYPE, int PONumb, string FIBER_CODE)
        {
            try
            {
                int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            //if (!SearchItemCodeInGrid(FIBER_CODE, PONumb, UNIQUEID))
            //{
                DataTable dt = SaitexBL.Interface.Method.TX_FIBER_PU_MST.GetTRNData_ForReceiving(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PO_TYPE, PONumb, FIBER_CODE,oUserLoginDetail.DT_STARTDATE.Year.ToString() );

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtICODE.Text = dt.Rows[0]["FIBER_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["FIBER_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtUOm.Text = dt.Rows[0]["UOM1"].ToString().Trim();
                    txtkg_bail.Text = dt.Rows[0]["UOM_BAIL"].ToString().Trim();
                    lblMaxQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    txtBasicRate.Text = double.Parse(dt.Rows[0]["BASIC_RATE"].ToString().Trim()).ToString();
                    txtFinalRate.Text = double.Parse(dt.Rows[0]["FINAL_RATE"].ToString().Trim()).ToString();
                    txtAmount.Text = "0";
                    txtDOM.Text = DateTime.Now.Date.ToShortDateString();             
                    DataTable dtdistax = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetTRNData_ForReceivingDisTaxes("C99999", "B99999", "PMR", 999999, txtICODE.Text,oUserLoginDetail.DT_STARTDATE.Year.ToString());
                    mapDataTableForDisTaxes(dtdistax);
                }
            //}
            //else
            //{
                
            //    RefreshDetailRow();
            //    CommonFuction.ShowMessage("Item Already Included");
            //}
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void mapDataTableForDisTaxes(DataTable dt)
        {
            try
            {
                if (Session["dtDicRate"] == null)
                {
                    CreateDataTableForDisTax();
                }
                DataTable dtRate1 = (DataTable)Session["dtDicRate"];
                double StartFinalAmount = 0;
                double.TryParse(txtBasicRate.Text, out StartFinalAmount);
                double dFinalRate = StartFinalAmount;
                if (dt != null && dt.Rows.Count > 0)
                {
                    int iloop = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        iloop += 1;
                        DataRow drNew = dtRate1.NewRow();
                        drNew["Uniqueid"] = iloop;
                        drNew["PO_COMP_CODE"] = "C99999";
                        drNew["PO_BRANCH"] = "B99999";
                        drNew["PO_TYPE"] = "PMR";
                        drNew["PO_NUMB"] = 999999;
                        drNew["FIBER_CODE"] = dr["FIBER_CODE"];
                        drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                        drNew["Rate"] = dr["RATE"];
                        drNew["COMPO_SL"] = dr["COMPO_SL"];
                        drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                        drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                        drNew["IS_PO"] = "1";
                        drNew["LOT_NO"] = txtMergeNo.SelectedValue.Trim().ToUpper();
                        drNew["PALLET_CODE"] = txtPalletCode.SelectedValue.Trim().ToUpper();
                        double dAmount = 0;

                        double cAmount = 0;
                        double rate = double.Parse(dr["Rate"].ToString());
                        if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                        {
                            cAmount = (StartFinalAmount * rate) / 100;
                        }
                        else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                        {
                            cAmount = (dFinalRate * rate) / 100;
                        }
                        else if (dr["BASE_COMPO_CODE"].ToString().Equals("Flat Amount"))
                        {
                            cAmount = rate;
                        }
                        else
                        {
                            DataView dvv = new DataView(dtRate1);
                            dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                            if (dvv.Count > 0)
                            {
                                double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                            }
                            cAmount = (dAmount * rate) / 100;
                        }

                        if (dr["COMPO_TYPE"].ToString().Equals("D"))
                        {
                            dFinalRate = dFinalRate - cAmount;
                        }
                        else
                        {
                            dFinalRate = dFinalRate + cAmount;
                        }
                        drNew["Amount"] = cAmount;
                        dtRate1.Rows.Add(drNew);
                    }
                }
                
                txtFinalRate.Text = dFinalRate.ToString();
                Session["dtDicRate"] = dtRate1;
            }
            catch
            {
                throw;
            }
        }

        private void CreateDataTableForDisTax()
        {
            try
            {
                DataTable dtRate1 = new DataTable();
                dtRate1.Columns.Add("Uniqueid", typeof(int));
                dtRate1.Columns.Add("PO_COMP_CODE", typeof(string));
                dtRate1.Columns.Add("PO_BRANCH", typeof(string));
                dtRate1.Columns.Add("PO_TYPE", typeof(string));
                dtRate1.Columns.Add("PO_NUMB", typeof(int));
                dtRate1.Columns.Add("FIBER_CODE", typeof(string));
                dtRate1.Columns.Add("COMPO_CODE", typeof(string));
                dtRate1.Columns.Add("Rate", typeof(double));
                dtRate1.Columns.Add("COMPO_SL", typeof(int));
                dtRate1.Columns.Add("COMPO_TYPE", typeof(string));
                dtRate1.Columns.Add("Amount", typeof(double));
                dtRate1.Columns.Add("BASE_COMPO_CODE", typeof(string));
                dtRate1.Columns.Add("IS_PO", typeof(string));
                dtRate1.Columns.Add("LOT_NO", typeof(string));
                dtRate1.Columns.Add("PALLET_CODE", typeof(string));
                Session["dtDicRate"] = dtRate1;
            }
            catch (Exception ex)
            {

                throw ex;
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

                e.ItemsLoadedCount = data.Rows.Count;
                e.ItemsCount = data.Rows.Count;
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
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT || '@'|| a.LR_NO || '@'|| a.LR_DATE)GATE_DATA,  trunc(A.GATE_DATE) as GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'FIBER IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15";
            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {
                CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT|| '@'|| a.LR_NO || '@'|| a.LR_DATE)GATE_DATA,  trunc(A.GATE_DATE) as GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'FIBER IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15 ";
            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "Save", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT|| '@'|| a.LR_NO || '@'|| a.LR_DATE) GATE_DATA, trunc(A.GATE_DATE) as GATE_DATE,A.GATE_NUMB,A.GATE_TYPE,a.prty_code,a.prty_name,a.trsp_code,a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='FIBER IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }
                else if (string.Compare(lblMode.Text, "Update", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT|| '@'|| a.LR_NO || '@'|| a.LR_DATE) GATE_DATA, trunc(A.GATE_DATE) as GATE_DATE,A.GATE_NUMB,A.GATE_TYPE, a.trsp_code, a.trsp_name,A.ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='FIBER IN')asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0)asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB)WHERE ROWNUM <= " + startOffset + ")";
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
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'FIBER IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {
                CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'FIBER IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
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
                DateTime GateDate = System.DateTime.Now;
                string VahicleNo = string.Empty;
                string PartyChallanNo = string.Empty;
                DateTime partyChallanDate = System.DateTime.Now;


                txtGateEntryNo.Text = ddlGateEntryNo.SelectedText.ToString();

                string cString = ddlGateEntryNo.SelectedValue.Trim();
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
                GateDate = DateTime.Parse(arrString[0].ToString());
                VahicleNo = arrString[1].ToString();
                PartyChallanNo = arrString[2].ToString();
                partyChallanDate = DateTime.Parse(arrString[3].ToString());
                lblPartyCode.Text = arrString[4].ToString();
                txtPartyAddress.Text = arrString[5].ToString();
                lblTransporterCode.Text = arrString[6].ToString();
                txtTransporterAddress.Text = arrString[7].ToString();

                txtTotalPartyAmt.Text = arrString[8].ToString();
                txtLRNo.Text = arrString[9].ToString();
                txtLRDate.Text = arrString[10].ToString();

                txtPartyBillNo.Text = PartyChallanNo;
                txtPartyBillDate.Text = partyChallanDate.ToShortDateString();
                GetDataForGateDetail(GateDate, VahicleNo, PartyChallanNo, partyChallanDate);

            bool result = CheckBillEntryExistence(txtPartyChallanNo.Text, txtPartyAddress.Text, lblPartyCode.Text, oUserLoginDetail.DT_STARTDATE.Year);
            if (!result)
            {
                CommonFuction.ShowMessage("BillEntry No Already Exists Please Enter Another!!");
                txtPartyBillNo.Text = string.Empty;
            }
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Entry Detail selection.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }

        private bool CheckBillEntryExistence(string BillNo, string PartyName, string PartyCode, int Year)
        {
            try
            {
                bool Result = SaitexBL.Interface.Method.TX_FIBER_IR_MST.CheckBillEntryExistence(BillNo, PartyName, PartyCode, Year);
                return Result;
            }
            catch (Exception)
            {
                
                throw;
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
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Indent for updation.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }

        protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
        {
            try
            {
                if (ViewState["TRN_TYPE"] != null)
                    TRN_TYPE = (string)ViewState["TRN_TYPE"];

                string CommandText = "SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM TX_FIBER_IR_MST a, tx_vendor_mst b  WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE ( TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery)  AND ROWNUM <= 15";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += "  AND TRN_NUMB NOT IN (SELECT TRN_NUMB FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME  FROM TX_FIBER_IR_MST a, tx_vendor_mst b WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE ( TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) AND ROWNUM <= '" + startOffset + "')";
                }

                string SortExpression = "    ORDER BY TRN_NUMB DESC, TRN_DATE DESC";
                string SearchQuery = text + "%";

                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", text + "%", "");

                return dt;
            }
            catch
            {
                throw;
            }
        }

        protected int GetReceivingCount(string text, int startOffset, int numberOfItems)
        {
            try
            {
                if (ViewState["TRN_TYPE"] != null)
                    TRN_TYPE = (string)ViewState["TRN_TYPE"];

                string CommandText = "SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM TX_FIBER_IR_MST a, tx_vendor_mst b  WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE ( TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery)  AND ROWNUM <= 15";
                string whereClause = string.Empty;

                string SortExpression = "    ORDER BY TRN_NUMB DESC, TRN_DATE DESC";
                string SearchQuery = text + "%";

                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", text + "%", "");

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
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selection.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }

        private int GetdataByTRNNUMBer(int TRNNUMBer)
        {
            int iRecordFound = 0;
            try
            {
                if (ViewState["TRN_TYPE"] != null)
                    TRN_TYPE = (string)ViewState["TRN_TYPE"];
                DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

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
                    lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                    txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                    lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                    txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                    ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();

                    lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();


                    lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();

                    txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
                    txtTransporterAddress.Text = dt.Rows[0]["TAddress"].ToString().Trim();

                    txtGateEntryNo.Text = dt.Rows[0]["GATE_NUMB"].ToString().Trim();
                    txtTotalAmount.Text = dt.Rows[0]["TOTAL_AMOUNT"].ToString().Trim();
                    txtPartyBillAmount.Text = dt.Rows[0]["FINAL_AMOUNT"].ToString().Trim();

                    DataTable dtgate = GetGateEntryData(txtGateEntryNo.Text);
                    if (dtgate != null && dtgate.Rows.Count > 0)
                    {
                        string gateDetails = dtgate.Rows[0]["GATE_DATA"].ToString().Trim();
                        char[] splitter = { '@' };
                        string[] arrString = gateDetails.Split(splitter);
                        if (!string.IsNullOrEmpty(arrString[2].ToString()))
                        {
                            string PartyChallanNo = arrString[2].ToString();
                            txtPartyBillNo.Text = PartyChallanNo;
                        }
                        if (!string.IsNullOrEmpty(arrString[3].ToString()))
                        {
                            DateTime partyChallanDate = DateTime.Parse(arrString[3].ToString());
                            txtPartyBillDate.Text = partyChallanDate.ToShortDateString();
                        }
                        if (!string.IsNullOrEmpty(arrString[8].ToString()))
                        {
                            txtTotalPartyAmt.Text = arrString[8].ToString();
                        }
                       
                       
                    }
                }
                if (iRecordFound == 1)
                {
                    if (ViewState["dtDetailTBL"] != null)
                    {
                        dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                    }
                    if (ViewState["dtTRN_Sub"] != null)
                    {
                        dtTRN_SUB = (DataTable)ViewState["dtTRN_Sub"];
                    }
                    if (Session["dtDicRate"] == null)
                    {
                        CreateDataTableDisTaxTrn();
                    }
                    if (Session["dtInvoiceDicRateMST"] == null)
                    {
                        CreateDataTableDisTaxMST();
                    }
                    if (Session["dtInvoiceDicRateMST"] != null)
                        dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];

                    if (Session["dtDicRate"] != null)
                        dtDicRate = (DataTable)Session["dtDicRate"];

                    if (Session["dtTRN_SUB"] != null)
                    {
                        dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                    }

                    dtDetailTBL.Rows.Clear();
                    dtDetailTBL = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetTRN_DataByTRN_NUMBst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    ViewState["dtDetailTBL"] = dtDetailTBL;
                    if (dtDetailTBL.Rows.Count > 0)
                    {
                        MapDataTable();

                        if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                        {
                            dtInvoiceDicRateMST.Rows.Clear();
                            DataTable dtDisTaxMstTemp = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                            if (dtDisTaxMstTemp.Rows.Count > 0)
                            {
                                MapDataTableDisTaxMST(dtDisTaxMstTemp);
                            }

                            dtDicRate.Rows.Clear();

                            DataTable dtDisTaxTrnTemp = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetTrnDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, TRNNUMBer, TRN_TYPE);
                            if (dtDisTaxTrnTemp.Rows.Count > 0)
                            {
                                MapDataTableDisTaxTrn(dtDisTaxTrnTemp);
                            }

                            dtTRN_SUB = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                            ViewState["dtTRN_SUB"] = dtTRN_SUB;
                            MapTrnDataTable();

                        }
                    }
                     else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Fiber Detail";
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

            if (!dtDetailTBL.Columns.Contains("COST_CENTER_CODE"))
                dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["MAC_CODE"] = string.Empty;
               // dtDetailTBL.Rows[iLoop]["COST_CENTER_CODE"] = string.Empty;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }
        

        private void CreateDataTableDisTaxMST()
        {

            try
            {
                DataTable dtInvoiceDicRateMST = new DataTable();
                dtInvoiceDicRateMST.Columns.Add("Uniqueid", typeof(int));
                dtInvoiceDicRateMST.Columns.Add("COMPO_CODE", typeof(string));
                dtInvoiceDicRateMST.Columns.Add("Rate", typeof(double));
                dtInvoiceDicRateMST.Columns.Add("COMPO_SL", typeof(int));
                dtInvoiceDicRateMST.Columns.Add("COMPO_TYPE", typeof(string));
                dtInvoiceDicRateMST.Columns.Add("Amount", typeof(double));
                dtInvoiceDicRateMST.Columns.Add("BASE_COMPO_CODE", typeof(string));
                Session["dtInvoiceDicRateMST"] = dtInvoiceDicRateMST;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateDataTableDisTaxTrn()
        {
            try
            {
                dtDicRate = new DataTable();
                dtDicRate.Columns.Add("Uniqueid", typeof(int));
                dtDicRate.Columns.Add("PO_COMP_CODE", typeof(string));
                dtDicRate.Columns.Add("PO_BRANCH", typeof(string));
                dtDicRate.Columns.Add("PO_TYPE", typeof(string));
                dtDicRate.Columns.Add("PO_NUMB", typeof(int));
                dtDicRate.Columns.Add("FIBER_CODE", typeof(string));
                dtDicRate.Columns.Add("COMPO_CODE", typeof(string));
                dtDicRate.Columns.Add("Rate", typeof(double));
                dtDicRate.Columns.Add("COMPO_SL", typeof(int));
                dtDicRate.Columns.Add("COMPO_TYPE", typeof(string));
                dtDicRate.Columns.Add("Amount", typeof(double));
                dtDicRate.Columns.Add("BASE_COMPO_CODE", typeof(string));
                dtDicRate.Columns.Add("IS_PO", typeof(string));

                dtDicRate.Columns.Add("LOT_NO", typeof(string));
                dtDicRate.Columns.Add("PALLET_CODE", typeof(string));
                Session["dtDicRate"] = dtDicRate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MapDataTableDisTaxMST(DataTable dt)
        {
            try
            {
                if (Session["dtInvoiceDicRateMST"] == null)
                {
                    CreateDataTableDisTaxMST();
                }
                dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];
                double StartFinalAmount = 0;
                double.TryParse(txtTotalAmount.Text, out StartFinalAmount);
                double dFinalRate = StartFinalAmount;
                if (dt != null && dt.Rows.Count > 0)
                {
                    int iloop = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        iloop += 1;
                        DataRow drNew = dtInvoiceDicRateMST.NewRow();

                        drNew["Uniqueid"] = iloop;
                        drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                        drNew["Rate"] = dr["RATE"];
                        drNew["COMPO_SL"] = dr["COMPO_SL"];
                        drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                        drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                        double dAmount = 0;

                        double cAmount = 0;
                        double rate = double.Parse(dr["Rate"].ToString());
                        if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                        {
                            cAmount = (StartFinalAmount * rate) / 100;
                        }
                        else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                        {
                            cAmount = (dFinalRate * rate) / 100;
                        }
                        else if (dr["BASE_COMPO_CODE"].ToString().Equals("Flat Amount"))
                        {
                            cAmount = rate;
                        }

                        else
                        {
                            DataView dvv = new DataView(dtInvoiceDicRateMST);
                            dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                            if (dvv.Count > 0)
                            {
                                double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                            }
                            cAmount = (dAmount * rate) / 100;
                        }

                        if (dr["COMPO_TYPE"].ToString().Equals("D"))
                        {
                            dFinalRate = dFinalRate - cAmount;
                        }
                        else
                        {
                            dFinalRate = dFinalRate + cAmount;
                        }
                        drNew["Amount"] = cAmount;
                        dtInvoiceDicRateMST.Rows.Add(drNew);
                    }
                }
                txtPartyBillAmount.Text = dFinalRate.ToString();
                Session["dtInvoiceDicRateMST"] = dtInvoiceDicRateMST;
            }
            catch
            {
                throw;
            }
        }

        private void MapDataTableDisTaxTrn(DataTable dt)
        {
            try
            {
                if (Session["dtDicRate"] == null)
                {
                    CreateDataTableDisTaxTrn();
                }
                dtDicRate = (DataTable)Session["dtDicRate"];
                double StartFinalAmount = 0;
                double.TryParse(txtBasicRate.Text, out StartFinalAmount);
                double dFinalRate = StartFinalAmount;
                if (dt != null && dt.Rows.Count > 0)
                {
                    int iloop = 0;
                    foreach (DataRow dr in dt.Rows)
                    {

                        iloop += 1;
                        DataRow drNew = dtDicRate.NewRow();

                        drNew["Uniqueid"] = iloop;
                        drNew["PO_COMP_CODE"] = dr["PO_COMP_CODE"];
                        drNew["PO_BRANCH"] = dr["PO_BRANCH"];
                        drNew["PO_TYPE"] = dr["PO_TYPE"];
                        drNew["PO_NUMB"] = dr["PO_NUMB"];
                        drNew["FIBER_CODE"] = dr["FIBER_CODE"];
                        drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                        drNew["Rate"] = dr["RATE"];
                        drNew["COMPO_SL"] = dr["COMPO_SL"];
                        drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                        drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                        drNew["IS_PO"] = dr["IS_PO"];

                        drNew["LOT_NO"] = dr["LOT_NO"];
                        drNew["PALLET_CODE"] = dr["PALLET_CODE"];
                        
                        double dAmount = 0;

                        double cAmount = 0;
                        double rate = double.Parse(dr["Rate"].ToString());
                        if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                        {
                            cAmount = (StartFinalAmount * rate) / 100;
                        }
                        else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                        {
                            cAmount = (dFinalRate * rate) / 100;
                        }
                        else
                        {
                            DataView dvv = new DataView(dtDicRate);
                            dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                            if (dvv.Count > 0)
                            {
                                double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                            }
                            cAmount = (dAmount * rate) / 100;
                        }

                        if (dr["COMPO_TYPE"].ToString().Equals("D"))
                        {
                            dFinalRate = dFinalRate - cAmount;
                        }
                        else
                        {
                            dFinalRate = dFinalRate + cAmount;
                        }
                        drNew["Amount"] = cAmount;
                        dtDicRate.Rows.Add(drNew);
                    }
                }

                Session["dtDicRate"] = dtDicRate;
            }
            catch
            {
                throw;
            }
        }

        private void MapTrnDataTable()
        {
            try
            {

                if (ViewState["dtTRN_Sub"] != null)
                    dtTRN_SUB = (DataTable)ViewState["dtTRN_Sub"];
                if (!dtTRN_SUB.Columns.Contains("UNIQUE_ID"))
                    dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
                if (!dtTRN_SUB.Columns.Contains("PI_NO"))
                    dtTRN_SUB.Columns.Add("PI_NO", typeof(string));

                for (int iLoop = 0; iLoop < dtTRN_SUB.Rows.Count; iLoop++)
                {
                    dtTRN_SUB.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
                    dtTRN_SUB.Rows[iLoop]["PI_NO"] = "NA";
                }
                dtTRN_SUB.AcceptChanges();
                Session["dtTRN_SUB"] = dtTRN_SUB;
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

                if (txtMergeNo.SelectedValue != string.Empty && txtPalletCode.SelectedValue != string.Empty && txtGrade.SelectedValue != string.Empty)
                {
                    txtQTY.ReadOnly = false;
                    txtNoOfUnit.ReadOnly = false;
                    txtUOm.ReadOnly = false;
                    txtWeightOfUnit.ReadOnly = false;
                    string URL = "FIBER_TRN_SUB.aspx";
                    URL = URL + "?FIBER_CODE=" + txtICODE.Text;
                    URL = URL + "&PO_NUMB=999999";
                    URL = URL + "&PO_TYPE=PMR";
                    URL = URL + "&PO_COMP_CODE=C99999" ;
                    URL = URL + "&PO_BRANCH=B99999" ;
                    URL = URL + "&PO_YEAR="+oUserLoginDetail.DT_STARTDATE.Year;
                    URL = URL + "&lblMaxQTY=" + lblMaxQTY.Text;
                    URL = URL + "&txtQTY=" + txtQTY.ClientID;
                    URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
                    URL = URL + "&txtWeightOfUnit=" + txtWeightOfUnit.ClientID;
                    URL = URL + "&txtUOm=" + txtUOm.ClientID;
                    URL = URL + "&MERGE_NO=" + txtMergeNo.SelectedValue;
                    URL = URL + "&PALLET_CODE=" + txtPalletCode.SelectedValue;
                    URL = URL + "&GRADE=" + txtGrade.SelectedValue;                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please select PO Number, enter merge,pallet code and grade details.");
                }
            }
            catch
            {
            }
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

        private void EditItemReceiptRow(int UNIQUEID)
        {
            try
            {
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                DataView dv = new DataView(dtDetailTBL);
                dv.RowFilter = " UNIQUEID = " + UNIQUEID;
                if (dv.Count > 0)
                {
                   
                    cmbPOITEM.Enabled = false;                    
                    txtICODE.Text = dv[0]["FIBER_CODE"].ToString();
                    txtDESC.Text = dv[0]["FIBER_DESC"].ToString();
                    txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                    lblMaxQTY.Text = dv[0]["TRN_QTY"].ToString();
                    txtUNIT.Text = dv[0]["UOM"].ToString();
                    txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                    txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                    txtAmount.Text = dv[0]["AMOUNT"].ToString();                   
                    txtDetRemarks.Text = dv[0]["REMARKS"].ToString();                    
                    txtDOM.Text = dv[0]["DATE_OF_MFG"].ToString();
                    txtUOm.Text = dv[0]["UOM_OF_UNIT"].ToString();
                    txtNoOfUnit.Text = dv[0]["NO_OF_UNIT"].ToString();                
                    txtWeightOfUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                    txtkg_bail.Text = dv[0]["UOM_BAIL"].ToString();
                    //txtMergeNo.Text = dv[0]["LOT_NO"].ToString();
                    //txtPalletCode.Text = dv[0]["PALLET_CODE"].ToString();
                    //txtGrade.Text = dv[0]["GRADE"].ToString(); 
                    string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('MERGE_NO'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                    string SortExpression = " order by MST_CODE asc";
                    DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", SortExpression, "", "%", "");
                    txtMergeNo.DataSource = data;
                    txtMergeNo.DataTextField = "MST_CODE";
                    txtMergeNo.DataValueField = "MST_CODE";
                    txtMergeNo.DataBind();
                    foreach (ComboBoxItem item in txtMergeNo.Items)
                    {
                        if (item.Text == dv[0]["LOT_NO"].ToString())
                        {
                            //txtMergeNo.SelectedIndex = txtMergeNo.Items.IndexOf(item);
                            txtMergeNo.SelectedText = item.Text;
                            txtMergeNo.SelectedValue = item.Value;
                            break;
                        }
                    }


                    string CommandText1 = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('PALLET_MASTER'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                    string SortExpression1 = " order by MST_CODE asc";
                    DataTable data1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, "", SortExpression1, "", "%", "");
                    txtPalletCode.DataSource = data1;
                    txtPalletCode.DataTextField = "MST_CODE";
                    txtPalletCode.DataValueField = "MST_CODE";
                    txtPalletCode.DataBind();
                    foreach (ComboBoxItem item in txtPalletCode.Items)
                    {
                        if (item.Text == dv[0]["PALLET_CODE"].ToString())
                        {
                            //txtPalletCode.SelectedIndex = txtPalletCode.Items.IndexOf(item);
                            txtPalletCode.SelectedText = item.Text;
                            txtPalletCode.SelectedValue = item.Value;
                            break;
                        }
                    }



                    string CommandText2 = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GRADE'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                    string SortExpression2 = " order by MST_CODE asc";
                    DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, "", SortExpression2, "", "%", "");
                    txtGrade.DataSource = data2;
                    txtGrade.DataTextField = "MST_CODE";
                    txtGrade.DataValueField = "MST_CODE";
                    txtGrade.DataBind();
                    foreach (ComboBoxItem item in txtGrade.Items)
                    {
                        if (item.Text == dv[0]["GRADE"].ToString())
                        {
                            // txtGrade.SelectedIndex = txtGrade.Items.IndexOf(item);
                            txtGrade.SelectedText = item.Text;
                            txtGrade.SelectedValue = item.Value;
                            break;
                        }
                    }

                    ViewState["UNIQUEID"] = UNIQUEID;

                }
            }
            catch 
            {
                
                throw;
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
                            if (Session["dtTRN_SUB"] != null)
                            {
                                DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];

                                DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
                                dvYRNDRecieve_trn.RowFilter = "FIBER_CODE='" + dv[0]["FIBER_CODE"].ToString() + "'  AND PALLET_CODE='" + dv[0]["PALLET_CODE"].ToString() + "' AND LOT_NO='" + dv[0]["LOT_NO"].ToString() + "' AND ROW_STATE <> 'DELETE'";
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


        protected void CalculateAmount()
        {
            double Amount = 0;
            double fRate = 0;
            double Qty = 0;
            double.TryParse(txtFinalRate.Text.Trim(), out fRate);
            double.TryParse(txtQTY.Text.Trim(), out Qty);
            Amount = Qty * fRate;
            txtAmount.Text = Amount.ToString();
        }


        protected void btnOtherCharges_Click(object sender, EventArgs e)
        {
            try
            {
                txtPartyBillAmount.ReadOnly = false;
                string URL = "MRNDisTaxAdjMST_FIBER.aspx";
                double TotalAmount = 0;
                double.TryParse(txtTotalAmount.Text, out TotalAmount);
                URL = URL + "?FinalAmount=" + TotalAmount.ToString();
                URL = URL + "&TextBoxId=" + txtPartyBillAmount.ClientID;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting other charges for transaction."));
                lblMode.Text = ex.ToString();
            }
        }
        protected void btnDis_Click(object sender, EventArgs e)
        {
            try
            {
                string URL = "MRNDisTaxAdj_FIBER.aspx";
                URL = URL + "?FinalAmount=" + txtBasicRate.Text.Trim();
                URL = URL + "&TextBoxId=" + txtFinalRate.ClientID;
                URL = URL + "&PO_COMP_CODE=C99999";
                URL = URL + "&PO_BRANCH=B99999" ;
                URL = URL + "&PO_TYPE=PMR" ;
                URL = URL + "&PO_NUMB=" + 999999;
                URL = URL + "&FIBER_CODE=" + txtICODE.Text.Trim();
                URL = URL + "&LOT_NO=" + txtMergeNo.SelectedValue.Trim().ToUpper();
                URL = URL + "&PALLET_CODE=" + txtPalletCode.SelectedValue.Trim().ToUpper();
                txtFinalRate.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting discount/ taxes adjustment for transaction."));
                lblMode.Text = ex.ToString();
            }
        }
        protected void txtPartyBillNo_TextChanged(object sender, EventArgs e)
        {
            bool result = CheckBillEntryExistence(txtPartyChallanNo.Text, txtPartyAddress.Text, lblPartyCode.Text, oUserLoginDetail.DT_STARTDATE.Year);
            if (!result)
            {
                CommonFuction.ShowMessage("BillEntry No Already Exists Please Enter Another!!");
                txtPartyBillNo.Text = string.Empty;
            }
        }
        protected void lblMaxQTY_TextChanged(object sender, EventArgs e)
        {

        }
        protected void cmbPOITEM_PreRender(object sender, EventArgs e)
        {

        }

        protected void txtQTY_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
            txtQTY.ReadOnly = true;
            txtNoOfUnit.ReadOnly = true;
            txtUOm.ReadOnly = true;
            txtWeightOfUnit.ReadOnly = true;
        }


        protected void grdMaterialItemReceipt_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                txtGateEntryNo.Text = "";
                RefreshDetailRow();
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

                if (dtDetailTBL == null)
                    CreateDataTable();

                dtDetailTBL.Rows.Clear();
                ViewState["dtDetailTBL"] = dtDetailTBL;
                IsUpdateCall = IsUpdateCall + 1;
            }
            catch
            {
                throw;
            }
        }

        protected void txtFinalRate_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
            txtQTY.ReadOnly = true;
            txtNoOfUnit.ReadOnly = true;
            txtUOm.ReadOnly = true;
            txtWeightOfUnit.ReadOnly = true;
        }


        private DataTable GetGateEntryData(string GateN)
        {
            try
            {

                DataTable data = null;
                string CommandText = string.Empty;
                string whereClause = string.Empty;               
                string SearchQuery=string.Empty;
                CommandText = "SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@' || a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA,trunc(A.GATE_DATE) as GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name  FROM v_tx_gate_mst a  WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'  AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'   AND a.ITEM_TYPE = 'FIBER IN'   and  a.GATE_NUMB='" + GateN + "'";
                             

                string SortExpression = " ORDER BY GATE_NUMB ";
             
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

                return data;


            }

            catch
            {

                throw;
            }
        }
        protected void txtBasicRate_TextChanged(object sender, EventArgs e)
        {
            txtFinalRate.Text = txtBasicRate.Text;
            CalculateAmount();
            txtQTY.ReadOnly = true;
            txtNoOfUnit.ReadOnly = true;
            txtUOm.ReadOnly = true;
            txtWeightOfUnit.ReadOnly = true;
        }


        protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            try
            {
                DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "MERGE_NO");
                txtMergeNo.Items.Clear();
                txtMergeNo.DataSource = data;
                txtMergeNo.DataBind();
                e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
                e.ItemsCount = GetMOMCount(e.Text, "MERGE_NO");
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Merge No in party selection.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }
        protected void txtGrade_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            try
            {
                DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "GRADE");
                txtGrade.Items.Clear();
                txtGrade.DataSource = data;
                txtGrade.DataBind();
                e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
                e.ItemsCount = GetMOMCount(e.Text, "GRADE");
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grade selection.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }
        protected void txtPalletCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
        {
            try
            {
                DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "PALLET_MASTER");
                txtPalletCode.Items.Clear();
                txtPalletCode.DataSource = data;
                txtPalletCode.DataBind();
                e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
                e.ItemsCount = GetMOMCount(e.Text, "PALLET_MASTER");
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Pallet Master selection.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }
        private DataTable GetMOMData(string Text, int startOffset, string MST_NAME)
        {
            try
            {
                string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND MST_CODE NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";
                }
                string SortExpression = " order by MST_CODE";
                string SearchQuery = Text + "%";
                return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
            }
            catch
            {
                throw;
            }
        }
        protected int GetMOMCount(string text, string MST_NAME)
        {

            string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

        }

}

