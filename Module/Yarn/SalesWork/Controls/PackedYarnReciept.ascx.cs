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

public partial class Module_Yarn_SalesWork_Controls_PackedYarnReciept : System.Web.UI.UserControl
{
    private DataTable dtTRN_SUB = null;    
    private static DateTime StartDate;
    private static DateTime EndDate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    public  string TRN_TYPE {get;set;}
    private static int IsUpdateCall_PACKING = 2;
    private static string BUSINESS_TYPE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
                bindProcess();
            }
            //if (TRN_TYPE.Equals("RYP01"))
            //{
            //    lblHeading.Text = " Carrate ";
            //}
            //else 
            //{
            //    lblHeading.Text = " Carton ";
            //}
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
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);             
           //// ViewState["TRN_TYPE"] = TRN_TYPE;
           // ViewState["TRN_TYPE"] = ddlPackingType.SelectedValue; 
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

            rvbill.MinimumValue = StartDate.ToShortDateString();
            rvbill.MaximumValue = EndDate.ToShortDateString();

            rvlr.MinimumValue = StartDate.ToShortDateString();
            rvlr.MaximumValue = EndDate.ToShortDateString();

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();


            Session["dtTRN_SUB_PACKING"] = null;            

            if (ViewState["dtDetailTBL"] != null)
                ViewState["dtDetailTBL"] = null;
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
            //if (ViewState["TRN_TYPE"] != null)
            //    TRN_TYPE = (string)ViewState["TRN_TYPE"];
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            //oYRN_IR_MST.TRN_TYPE = TRN_TYPE;
            oYRN_IR_MST.TRN_TYPE = ddlPackingType.SelectedValue;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtTRNNUMBer.Text = SaitexBL.Interface.Method.YRN_IR_MST.GetNewTRNNumberForPacking(oYRN_IR_MST).ToString();
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
            //spnAInW.InnerText = "Amount In Words... ";
            txtDepartment.Text = "";
            txtFormRefNo.SelectedIndex = -1;
            txtFormType.SelectedIndex = -1;
            CmBParty.SelectedIndex = -1;
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
            txtRemarks.SelectedIndex = -1;
            txtTransporterAddress.Text = "";
            txtTotalAmount.Text = string.Empty;
            txtVehicleNo.Text = "";
            txtPartyAddress1.Text = string.Empty;
            lblPartyCode1.Text = string.Empty;

            txtTotalPartyAmt.Text = "";
            txtPartyBillDate.Text = "";
            txtPartyBillNo.Text = "";

            ddlReceiptShift.SelectedIndex = 0;
            hdnCartonWt.Value = string.Empty;
            hdnPaperTubeWt.Value = string.Empty;

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
            txtBatchNo.Text = string.Empty;
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
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_FAMILY", typeof(string));
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
            dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("GROSS_WT", typeof(double));
            dtDetailTBL.Columns.Add("TARE_WT", typeof(double));           
            dtDetailTBL.Columns.Add("CARTONS", typeof(double));
            dtDetailTBL.Columns.Add("FINISH_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("DYED_BATCH", typeof(string));
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
            CalculateTotalAmount();

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
                msg += @"#. Packing Number required.\r\n";
            }
            //if (CmBParty.SelectedValue != string.Empty)
            //{
            //    count += 1;
            //}
            //else
            //{
            //    msg += @"#.Please Select Party Details!.\r\n";
            //}

            if (ddlStore.SelectedValue != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#.Select Finish Good Store!.\r\n";
            }

            //if (txtRemarks.SelectedValue != string.Empty)
            //{
            //    count += 1;
            //}
            //else
            //{
            //    msg += @"#.Select Process!.\r\n";
            //}

            //if (txtGateEntryNo.Text != "")
            //{
            //    count += 1;
            //}
            //else
            //{
            //    msg += @"#. Please select Gate Entry Details first.\r\n";
            //}

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for packing.\r\n";
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

            Response.Redirect("./PrintPackingSlip.aspx?TRN_TYPE=" + ddlPackingType.SelectedValue, false);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
        //    if (ViewState["TRN_TYPE"] != null)
        //        TRN_TYPE = (string)ViewState["TRN_TYPE"];

        //    Response.Redirect("~/Module/Yarn/SalesWork/Reports/YRN_ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
        //}
        //catch (Exception ex)
        //{
        //    CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
        //    lblMode.Text = ex.ToString();
        //}
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
        string shade_Code = txtShadeCode.Text;
        try
        {
            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                    Label txtshadeCode = (Label)grdRow.FindControl("txtshadeCode");
                    Label txtPONum = (Label)grdRow.FindControl("txtPoNo");
                    Label txtGrade1 = (Label)grdRow.FindControl("txtGrade1");
                    Label txtLotNo1 = (Label)grdRow.FindControl("txtLotNo1");
                    Label txtPI_NO1 = (Label)grdRow.FindControl("txtPI_NO1");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (int.Parse(txtPONum.Text.Trim()) == PONumb && txtICODE.ToolTip.Trim() == ItemCode && txtshadeCode.Text.Trim() == shade_Code && txtGrade.SelectedValue==txtGrade1.Text && txtPINO.Text==txtPI_NO1.Text && txtLotNo.Text==txtLotNo1.Text  && UNIQUEID != iUNIQUEID)
                    {
                        Result = true;
                    }
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
            //if (ViewState["TRN_TYPE"] != null)
            //    TRN_TYPE = (string)ViewState["TRN_TYPE"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.SelectedValue.Trim());
            oYRN_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.SelectedValue.Trim());

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
            oYRN_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode1.Text.Trim());
            oYRN_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress1.Text.Trim());
            oYRN_IR_MST.RCOMMENT = txtRemarks.SelectedValue.Trim();
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oYRN_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(ddlPackingType.SelectedValue);

            oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            int TRN_NUMB = 0;
            //:ToDo
          

           

            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;


            double totalAmt = 0;
            double.TryParse(txtTotalAmount.Text, out totalAmt);
            oYRN_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(txtPartyBillAmount.Text, out finalAmt);
            oYRN_IR_MST.FINAL_AMOUNT = finalAmt;

            oYRN_IR_MST.BILL_NUMB = txtPartyBillNo.Text;

            int BILLNUMBER=0;
            int.TryParse(txtPartyBillNo.Text,out BILLNUMBER);
            oYRN_IR_MST.BILL_NUMBER = BILLNUMBER;



            dt = System.DateTime.Now.Date;
            bool Is_Bill_Date = false;
            Is_Bill_Date = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("BILL_DATE", Is_Bill_Date);
            oYRN_IR_MST.BILL_DATE = dt;

            oYRN_IR_MST.LOCATION  = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            

            if (Session["dtTRN_SUB_PACKING"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB_PACKING"];
            }

            

            double dblPartyBillAmt = 0;     // For Party Bill Amount
            double dblTotalAmount = 0;       // Total Amount
            double dblFinalAmount = 0;  // For Final Amount
         
            double.TryParse(txtTotalPartyAmt.Text.Trim(), out dblPartyBillAmt);
            double.TryParse(txtTotalAmount.Text.Trim(), out dblTotalAmount);
            double.TryParse(txtPartyBillAmount.Text.Trim(), out dblFinalAmount);


            oYRN_IR_MST.PARTY_BILL_AMOUNT = dblPartyBillAmt;           

                bool result = SaitexBL.Interface.Method.YRN_IR_MST.InsertForPacking(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB);
                if (result)
                {
                    InitialisePage();
                    string Msg = string.Empty;                
                    Msg += @"\r\n Packing Number : " + TRN_NUMB + " saved successfully.";
                    CommonFuction.ShowMessage(Msg);                   

                    if (Session["dtTRN_SUB_PACKING"] != null)
                    {
                        Session["dtTRN_SUB_PACKING"] = null;
                    }

                    if (ViewState["dtDetailTBL"] != null)
                        ViewState["dtDetailTBL"] = null;
                }
                else
                {
                    CommonFuction.ShowMessage("Data  Saving Failed");
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
            //if (ViewState["TRN_TYPE"] != null)
            //    TRN_TYPE = (string)ViewState["TRN_TYPE"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.SelectedValue.Trim());
            oYRN_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.SelectedValue.Trim());

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
            oYRN_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
            oYRN_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
            oYRN_IR_MST.RCOMMENT = txtRemarks.SelectedValue.Trim();
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oYRN_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(ddlPackingType.SelectedValue);

            oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;


            oYRN_IR_MST.BILL_NUMB = txtPartyBillNo.Text;


            int BILLNUMBER = 0;
            int.TryParse(txtPartyBillNo.Text, out BILLNUMBER);
            oYRN_IR_MST.BILL_NUMBER = BILLNUMBER;


            dt = System.DateTime.Now.Date;
            bool Is_Bill_Date = false;
            Is_Bill_Date = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("BILL_DATE", Is_Bill_Date);
            oYRN_IR_MST.BILL_DATE = dt;

            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            double totalAmt = 0;
            double.TryParse(txtTotalAmount.Text, out totalAmt);
            oYRN_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(txtPartyBillAmount.Text, out finalAmt);
            oYRN_IR_MST.FINAL_AMOUNT = finalAmt;
            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;

            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];          

            if (Session["dtTRN_SUB_PACKING"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB_PACKING"];
            }

            // Show message to the user that Total Amount OR Final Amount is not same as party Bill Amount.
            // Only message show then saved, .....  Guided By Akhikesh Sir 18 Oct 2011 (Added By Rajesh as per Bharat discussion.)

            double dblPartyBillAmt = 0;     // For Party Bill Amount
            double dblTotalAmount = 0;       // Total Amount
            double dblFinalAmount = 0;  // For Final Amount

            double.TryParse(txtTotalPartyAmt.Text.Trim(), out dblPartyBillAmt);
            double.TryParse(txtTotalAmount.Text.Trim(), out dblTotalAmount);
            double.TryParse(txtPartyBillAmount.Text.Trim(), out dblFinalAmount);
            oYRN_IR_MST.PARTY_BILL_AMOUNT = dblPartyBillAmt;            
                bool result = SaitexBL.Interface.Method.YRN_IR_MST.UpdateForPacking(oYRN_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB);
                if (result)
                {
                    InitialisePage();
                    string Msg = string.Empty;                    
                    Msg += @"\r\n Packing Number : " + oYRN_IR_MST.TRN_NUMB + " Updated successfully.";
                    CommonFuction.ShowMessage(Msg);                   
                   
                    if (Session["dtTRN_SUB_PACKING"] != null)
                    {
                        Session["dtTRN_SUB_PACKING"] = null;
                    }

                    if (ViewState["dtDetailTBL"] != null)
                        ViewState["dtDetailTBL"] = null;

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

    private int GetdataByTRNNUMBer(int TRNNUMBer)
    {
        int iRecordFound = 0;
        try
        {
            //if (ViewState["TRN_TYPE"] != null)
            //    TRN_TYPE = (string)ViewState["TRN_TYPE"];

            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMBForPacking(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, ddlPackingType.SelectedValue, oUserLoginDetail.DT_STARTDATE.Year);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();

                ddlPackingType.SelectedIndex = ddlPackingType.Items.IndexOf(ddlPackingType.Items.FindByValue(dt.Rows[0]["TRN_TYPE"].ToString().Trim()));
                
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

                txtTotalPartyAmt.Text = dt.Rows[0]["PARTY_BILL_AMOUNT"].ToString().Trim();
                txtPartyBillDate.Text = dt.Rows[0]["BILL_DATE"].ToString().Trim();
                txtPartyBillNo.Text = dt.Rows[0]["BILL_NUMB"].ToString().Trim();
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["STORE"].ToString().Trim()));
                //txtFormRefNo.Text = dt.Rows[0]["FORM_NUMB"].ToString().Trim();
                //txtFormType.Text = dt.Rows[0]["FORM_TYPE"].ToString().Trim();



                string CommandText1 = "SELECT   ITEM_CODE,           ITEM_DESC      FROM   TX_ITEM_MST      WHERE        ITEM_TYPE='CARTON'      AND (ITEM_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery)               AND      ROWNUM <= 15          ORDER BY   ITEM_CODE           ";
                DataTable data1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, "", "", "", "%", "");
                txtFormType.DataSource = data1;
                //txtFormType.DataTextField = "ITEM_DESC";
                //txtFormType.DataTextField = "ITEM_DESC";
                txtFormType.DataValueField = "ITEM_CODE";
                txtFormType.DataBind();
                foreach (ComboBoxItem item in txtFormType.Items)
                {
                    if (item.Value == dt.Rows[0]["FORM_TYPE"].ToString().Trim())
                    {
                       // txtFormType.SelectedIndex = txtFormType.Items.IndexOf(item);
                        txtFormType.SelectedText = item.Text.ToString();
                        txtFormType.SelectedValue = item.Value.ToString();
                        break;
                    }
                }

                string CommandText2 = "SELECT   ITEM_CODE,           ITEM_DESC          FROM   TX_ITEM_MST      WHERE        ITEM_TYPE='PAPERTUBE-PCW/PLY/PINEAPPLE'      AND (ITEM_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery)               AND      ROWNUM <= 15          ORDER BY   ITEM_CODE          ";
                DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, "", "", "", "%", "");
                txtFormRefNo.DataSource = data2;
                txtFormRefNo.DataTextField = "ITEM_DESC";
                txtFormRefNo.DataTextField = "ITEM_DESC";
                txtFormRefNo.DataValueField = "ITEM_CODE";
                txtFormRefNo.DataBind();
                foreach (ComboBoxItem item in txtFormRefNo.Items)
                {
                    if (item.Value == dt.Rows[0]["FORM_NUMB"].ToString().Trim())
                    {
                        //txtFormRefNo.SelectedIndex = txtFormRefNo.Items.IndexOf(item);
                        txtFormRefNo.SelectedText = item.Text.ToString();
                        txtFormRefNo.SelectedValue = item.Value.ToString();
                        break;
                    }
                }

                  hdnCartonWt.Value = SaitexBL.Interface.Method.TX_ITEM_MST.GetWeightForItem(dt.Rows[0]["FORM_TYPE"].ToString()).ToString();
                  hdnPaperTubeWt.Value = SaitexBL.Interface.Method.TX_ITEM_MST.GetWeightForItem(dt.Rows[0]["FORM_NUMB"].ToString()).ToString();
            }
            if (iRecordFound == 1)
            {
                if (ViewState["dtDetailTBL"] != null)
                {
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                }
                if (ViewState["dtTRN_SUB_PACKING"] != null)
                {
                    dtTRN_SUB = (DataTable)ViewState["dtTRN_SUB_PACKING"];
                }               

                if (Session["dtTRN_SUB_PACKING"] != null)
                {
                    dtTRN_SUB = (DataTable)Session["dtTRN_SUB_PACKING"];
                }


                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetTRN_DataByTRN_NUMBstForPacking(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, ddlPackingType.SelectedValue);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                if (dtDetailTBL.Rows.Count > 0)
                {
                    MapDataTable();
                    if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                    {
                        dtTRN_SUB = SaitexBL.Interface.Method.YRN_IR_MST.GetSUBTRN_DataByTRN_NUMBForPacking(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, ddlPackingType.SelectedValue);
                        ViewState["dtTRN_SUB_PACKING"] = dtTRN_SUB;
                        MapTrnDataTable();

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

            if (!dtDetailTBL.Columns.Contains("COST_CENTER_CODE"))
                dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;              
               // dtDetailTBL.Rows[iLoop]["COST_CENTER_CODE"] = string.Empty;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
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

            if (ViewState["dtTRN_SUB_PACKING"] != null)
                dtTRN_SUB = (DataTable)ViewState["dtTRN_SUB_PACKING"];
            if (!dtTRN_SUB.Columns.Contains("UNIQUE_ID"))
                dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            if (!dtTRN_SUB.Columns.Contains("PI_NO"))
                dtTRN_SUB.Columns.Add("PI_NO", typeof(string));

            for (int iLoop = 0; iLoop < dtTRN_SUB.Rows.Count; iLoop++)
            {
                dtTRN_SUB.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;               
            }
            dtTRN_SUB.AcceptChanges();
            Session["dtTRN_SUB_PACKING"] = dtTRN_SUB;
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
            IsUpdateCall_PACKING = IsUpdateCall_PACKING + 1;
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

            //if (dtDetailTBL.Rows.Count < 15)
            //{
            if (txtPONumb.Text != "" && txtICODE.Text != "" && txtQTY.Text != "" )
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
                                string[] arrstr = txtICODE.Text.Split('-');
                                if (arrstr.Length > 0)
                                {
                                    dv[0]["YARN_CODE"] = arrstr[0].ToString();
                                }
                                else {
                                    dv[0]["YARN_CODE"] = txtICODE.Text.Trim();
                                }
                               
                                dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dv[0]["UOM"] = txtUNIT.Text.Trim();
                                dv[0]["BASIC_RATE"] = 0;//Math.Round(double.Parse(txtBasicRate.Text.Trim()),3);
                                dv[0]["FINAL_RATE"] = 0;// Math.Round(double.Parse(txtFinalRate.Text.Trim()), 3);
                                dv[0]["AMOUNT"] = 0;// Math.Round(double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim()), 3);
                                dv[0]["COST_CENTER_CODE"] = txtGrade.SelectedValue;// txtCostCode.Text.Trim();
                                dv[0]["MAC_CODE"] = ddlMachineCode.SelectedValue;
                                dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                if (chk_QCFlag.Checked)
                                    dv[0]["QCFLAG"] = "Yes";
                                else
                                    dv[0]["QCFLAG"] = "No";
                               
                                dv[0]["DATE_OF_MFG"] = DateTime.Now ;
                                dv[0]["NO_OF_UNIT"] = txtNoOfUnit.Text;
                                dv[0]["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                                dv[0]["UOM_OF_UNIT"] = txtUOm.Text;
                                dv[0]["PI_NO"] = txtPINO.Text;
                                dv[0]["LOT_NO"] = txtLotNo.Text;
                                dv[0]["SHADE_CODE"] = txtShadeCode.Text.Trim();
                                dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                                double grossWt = 0;
                                double tareWt = 0;
                                double cartons = 0;
                                double.TryParse(txtGrossWt.Text,out grossWt );
                                double.TryParse(txtTareWt.Text ,out tareWt );
                                double.TryParse(txtCartons.Text,out cartons );
                                dv[0]["GRADE"] = txtGrade.SelectedValue;
                                dv[0]["GROSS_WT"] = grossWt;
                                dv[0]["TARE_WT"] = tareWt;                               
                                dv[0]["CARTONS"] = cartons ;
                                dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year ;
                                dv[0]["FINISH_TYPE"] = ddlFinishedType.SelectedValue;
                                dv[0]["DYED_BATCH"] = txtBatchNo.Text.Trim();
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
                            string[] arrstr = txtICODE.Text.Split('-');
                            if (arrstr.Length > 0)
                            {
                                dr["YARN_CODE"] = arrstr[0].ToString();
                            }
                            else
                            {
                               dr["YARN_CODE"] = txtICODE.Text.Trim();
                            }
                            dr["YARN_DESC"] = txtDESC.Text.Trim();
                            dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                            dr["UOM"] = txtUNIT.Text.Trim();
                            dr["BASIC_RATE"] = 0;// Math.Round(double.Parse(txtBasicRate.Text.Trim()), 3);
                            dr["FINAL_RATE"] = 0;// Math.Round(double.Parse(txtFinalRate.Text.Trim()), 3);
                            dr["AMOUNT"] = 0;// Math.Round(double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim()), 3);
                            dr["COST_CENTER_CODE"] = txtGrade.SelectedValue; //txtCostCode.Text.Trim();
                            dr["MAC_CODE"] = ddlMachineCode.SelectedValue;
                            dr["REMARKS"] = txtDetRemarks.Text.Trim();
                            if (chk_QCFlag.Checked)
                                dr["QCFLAG"] = "Yes";
                            else
                                dr["QCFLAG"] = "No";
                       
                            dr["DATE_OF_MFG"] = DateTime.Now;
                            dr["NO_OF_UNIT"] = txtNoOfUnit.Text;
                            dr["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                            dr["UOM_OF_UNIT"] = txtUOm.Text;
                            dr["PI_NO"] = txtPINO.Text;
                            dr["LOT_NO"] = txtLotNo.Text;
                            dr["SHADE_CODE"] = txtShadeCode.Text.Trim();
                            dr["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                            double grossWt = 0;
                            double tareWt = 0;
                            double cartons = 0;
                            double.TryParse(txtGrossWt.Text, out grossWt);
                            double.TryParse(txtTareWt.Text, out tareWt);
                            double.TryParse(txtCartons.Text, out cartons);
                            dr["GRADE"] = txtGrade.SelectedValue;
                            dr["GROSS_WT"] = grossWt;
                            dr["TARE_WT"] = tareWt;
                            dr["CARTONS"] = cartons;
                            dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["FINISH_TYPE"] = ddlFinishedType.SelectedValue;
                            dr["DYED_BATCH"] = txtBatchNo.Text.Trim();
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
            //}
            //else
            //{
            //    CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
            //}
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


    protected void CmBParty_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyDatacmb(e.Text.ToUpper(), e.ItemsOffset);

            CmBParty.Items.Clear();

            CmBParty.DataSource = data;
            CmBParty.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCountcmb(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindProcess()
    {
        try
        {
            txtRemarks.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PACK_PROCESS", oUserLoginDetail.COMP_CODE);
            txtRemarks.DataSource = dt;
            txtRemarks.DataValueField = "MST_CODE";
            txtRemarks.DataTextField = "MST_DESC";
            txtRemarks.DataBind();
            txtRemarks.Items.Insert(0, new ListItem("--Select--", ""));
        }
        catch
        {
            throw;
        }
    }
    protected void CmBParty_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyAddress1.Text = CmBParty.SelectedValue.Trim();
            lblPartyCode1.Text = CmBParty.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyDatacmb(string Text, int startOffset)
    {
        try
        {
            DataTable dt = null;
            if (ddlPackingType.SelectedValue == "RYJ32")
            {
                string CommandText = "SELECT   DISTINCT  PRTY_CODE,PRTY_NAME, (PRTY_NAME) Address, PRTY_GRP_CODE     FROM   (  SELECT   *   FROM   (SELECT   TX_VENDOR_MST.PRTY_CODE,    TX_VENDOR_MST.PRTY_NAME,    TX_VENDOR_MST.PRTY_ADD1,  TX_VENDOR_MST.PRTY_GRP_CODE  FROM   TX_VENDOR_MST, V_YRN_PROD_DYEING_TRN vw  WHERE  vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE  AND vw.CONF_FLAG = 1     AND (NVL (vw.TRN_QTY, 0)   - NVL (vw.PACKED_QTY, 0) > 0)) msd     WHERE   msd.PRTY_CODE LIKE :SearchQuery                     OR msd.PRTY_NAME LIKE :SearchQuery          ORDER BY   msd.PRTY_CODE ASC) WHERE   UPPER (PRTY_GRP_CODE) <> UPPER ('Transporter') AND ROWNUM <= 15";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT  distinct  PRTY_CODE    FROM  (SELECT   TX_VENDOR_MST.PRTY_CODE,    TX_VENDOR_MST.PRTY_NAME,    TX_VENDOR_MST.PRTY_ADD1,  TX_VENDOR_MST.PRTY_GRP_CODE  FROM   TX_VENDOR_MST, V_YRN_PROD_DYEING_TRN vw  WHERE  vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE  AND vw.CONF_FLAG = 1     AND (NVL (vw.TRN_QTY, 0)   - NVL (vw.PACKED_QTY, 0) > 0)) msd   WHERE   UPPER (PRTY_GRP_CODE) <> UPPER ('Transporter')         AND ROWNUM <= " + startOffset + ")";
                }

                string SortExpression = " order by PRTY_CODE";
                string SearchQuery = "%" + Text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            }
            else
            {
                string CommandText = "SELECT  distinct  PRTY_CODE, PRTY_NAME, (PRTY_NAME) Address, PRTY_GRP_CODE  FROM   ( SELECT   PRTY_CODE, PRTY_NAME, PRTY_ADD1, PRTY_GRP_CODE  FROM   TX_VENDOR_MST WHERE UPPER (PRTY_CODE) = 'SELF' AND  UPPER (PRTY_GRP_CODE) <> UPPER ('Transporter') AND (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery))  where ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (  SELECT DISTINCT  PRTY_CODE  FROM   TX_VENDOR_MST WHERE UPPER (PRTY_CODE) = 'SELF' AND  UPPER (PRTY_GRP_CODE) <> UPPER ('Transporter') AND (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery)   AND    ROWNUM <= " + startOffset + ")";
                }

                string SortExpression = " order by PRTY_CODE";
                string SearchQuery = "%" + Text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            }
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCountcmb(string text)
    {
        DataTable data = null;
        if (ddlPackingType.SelectedValue == "RYJ32")
        {
            string CommandText = " SELECT   DISTINCT PRTY_CODE,PRTY_NAME,  (PRTY_NAME) Address, PRTY_GRP_CODE     FROM   (  SELECT   *   FROM   (SELECT   TX_VENDOR_MST.PRTY_CODE,    TX_VENDOR_MST.PRTY_NAME,    TX_VENDOR_MST.PRTY_ADD1,  TX_VENDOR_MST.PRTY_GRP_CODE   FROM   TX_VENDOR_MST, V_YRN_PROD_DYEING_TRN vw  WHERE  vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE  AND vw.CONF_FLAG = 1     AND (NVL (vw.TRN_QTY, 0)   - NVL (vw.PACKED_QTY, 0) > 0)) msd     WHERE   msd.PRTY_CODE LIKE :SearchQuery                     OR msd.PRTY_NAME LIKE :SearchQuery          ORDER BY   msd.PRTY_CODE ASC) WHERE   UPPER (PRTY_GRP_CODE) <> UPPER ('Transporter')";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        else
        {

            string CommandText = " SELECT   PRTY_CODE, PRTY_NAME, PRTY_ADD1, PRTY_GRP_CODE  FROM   TX_VENDOR_MST WHERE  UPPER (PRTY_CODE) = 'SELF' AND UPPER (PRTY_GRP_CODE) <> UPPER ('Transporter') AND (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery)";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        }
        return data.Rows.Count;
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

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetPODataCount(e.Text);
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
            DataTable dt = null;
            if (ddlPackingType.SelectedValue == "RYJ32")
            {
                string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT  ( BATCH_CODE  || '@'|| PT.PA_NO  || '@'  || GREY_LOT_NO   || '@'  || pt.ARTICAL_CODE  || '@'|| pt.ARTICAL_DESC || '@' || B.SHADE_CODE   || '@'  || B.SHADE_CODE  || '@' || UOM_OF_UNIT  || '@'  || (NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0))  || '@'  || PROCESS   || '@' || MACHINE_CODE || '@'|| pt.PRTY_CODE || '@'|| pt.PARTY_NAME || '@'|| pt.BUSINESS_TYPE)   po_Item_trn,    BATCH_CODE PO_NUMB,  PT.PA_NO ,  GREY_LOT_NO,    pt.ARTICAL_CODE, TRN_QTY PRD_QTY,pt.ARTICAL_DESC,   NVL (PACKED_QTY, 0) QTY_PACKED,    NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) AS QTY_REM,  B.SHADE_CODE,   B.SHADE_CODE SHADE_FAMILY,  MACHINE_CODE FROM   V_YRN_PROD_DYEING_TRN pt, OD_CAPT_TRN_BOM B  WHERE       NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) > 0  AND NVL (COPS, 0) - NVL (PACKED_NO_OF_UNIT, 0) > 0  AND B.PI_NO = PT.PA_NO  AND B.ASS_ARTICAL_CODE LIKE '%' || PT.ARTICAL_CODE || '%'   AND B.PRODUCT_TYPE = 'YARN DYEING'  AND B.COMP_CODE = PT.COMP_CODE   AND B.BRANCH_CODE = PT.BRANCH_CODE   AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'         AND PT.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "'   AND pt.BUSINESS_TYPE ='JW' AND TRN_TYPE = 'RYS21'  AND (   UPPER (BATCH_CODE) LIKE :SearchQuery OR UPPER (pt.ARTICAL_CODE) LIKE :SearchQuery   OR UPPER (pt.ARTICAL_DESC) LIKE :SearchQuery  OR UPPER (GREY_LOT_NO) LIKE :SearchQuery   OR UPPER (PT.PA_NO) LIKE :SearchQuery OR UPPER (PT.BATCH_ISSUE_NO) LIKE :SearchQuery   OR UPPER (PT.SHADE_CODE) LIKE :SearchQuery  ) ORDER BY   PO_NUMB) WHERE   ROWNUM <= 15";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PO_NUMB  NOT IN  ( SELECT PO_NUMB FROM (  SELECT   DISTINCT  ( BATCH_CODE  || '@'|| PT.PA_NO  || '@'  || GREY_LOT_NO   || '@'  || pt.ARTICAL_CODE  || '@'|| pt.ARTICAL_DESC || '@' || B.SHADE_CODE   || '@'  || B.SHADE_CODE  || '@' || UOM_OF_UNIT  || '@'  || (NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0))  || '@'  || PROCESS   || '@' || MACHINE_CODE || '@'|| pt.PRTY_CODE || '@'|| pt.PARTY_NAME || '@'|| pt.BUSINESS_TYPE)   po_Item_trn,    BATCH_CODE PO_NUMB,  PT.PA_NO ,  GREY_LOT_NO,    pt.ARTICAL_CODE, TRN_QTY PRD_QTY,pt.ARTICAL_DESC,   NVL (PACKED_QTY, 0) QTY_PACKED,    NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) AS QTY_REM,  B.SHADE_CODE,   B.SHADE_CODE SHADE_FAMILY,  MACHINE_CODE FROM   V_YRN_PROD_DYEING_TRN pt, OD_CAPT_TRN_BOM B  WHERE       NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) > 0  AND NVL (COPS, 0) - NVL (PACKED_NO_OF_UNIT, 0) > 0  AND B.PI_NO = PT.PA_NO  AND B.ASS_ARTICAL_CODE LIKE '%' || PT.ARTICAL_CODE || '%'   AND B.PRODUCT_TYPE = 'YARN DYEING'  AND B.COMP_CODE = PT.COMP_CODE   AND B.BRANCH_CODE = PT.BRANCH_CODE     AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'         AND PT.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "'  AND pt.BUSINESS_TYPE ='JW'  AND TRN_TYPE = 'RYS21'  AND (   UPPER (BATCH_CODE) LIKE :SearchQuery OR UPPER (pt.ARTICAL_CODE) LIKE :SearchQuery   OR UPPER (pt.ARTICAL_DESC) LIKE :SearchQuery  OR UPPER (GREY_LOT_NO) LIKE :SearchQuery   OR UPPER (PT.PA_NO) LIKE :SearchQuery OR UPPER (PT.BATCH_ISSUE_NO) LIKE :SearchQuery   OR UPPER (PT.SHADE_CODE) LIKE :SearchQuery ) ORDER BY   PO_NUMB) WHERE   ROWNUM <='" + startOffset + "' )";
                }
                string SortExpression = "";
                string SearchQuery = text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
            }

            else
            {
                string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT  ( BATCH_CODE  || '@'|| PT.PA_NO  || '@'  || GREY_LOT_NO   || '@'  || pt.ARTICAL_CODE  || '@'|| pt.ARTICAL_DESC || '@' || B.SHADE_CODE   || '@'  || B.SHADE_CODE  || '@' || UOM_OF_UNIT  || '@'  || (NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0))  || '@'  || PROCESS   || '@' || MACHINE_CODE || '@'|| pt.PRTY_CODE || '@'|| pt.PARTY_NAME || '@'|| pt.BUSINESS_TYPE||'@'||pt.BATCH_ISSUE_NO)   po_Item_trn,    BATCH_CODE PO_NUMB,  PT.PA_NO ,  GREY_LOT_NO,    pt.ARTICAL_CODE, TRN_QTY PRD_QTY,pt.ARTICAL_DESC,   NVL (PACKED_QTY, 0) QTY_PACKED,    NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) AS QTY_REM,  B.SHADE_CODE,   B.SHADE_CODE SHADE_FAMILY,  MACHINE_CODE,PT.ISS_TRN_QTY, PT.ISS_COPS,pt.BATCH_ISSUE_NO FROM   V_YRN_PROD_DYEING_TRN pt, OD_CAPT_TRN_BOM B  WHERE       NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) > 0  AND NVL (COPS, 0) - NVL (PACKED_NO_OF_UNIT, 0) > 0  AND B.PI_NO = PT.PA_NO  AND B.ASS_ARTICAL_CODE LIKE '%' || PT.ARTICAL_CODE || '%'   AND B.PRODUCT_TYPE = 'YARN DYEING'  AND B.COMP_CODE = PT.COMP_CODE   AND B.BRANCH_CODE = PT.BRANCH_CODE   AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'         AND PT.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "'  AND pt.BUSINESS_TYPE ='SW' AND TRN_TYPE = 'RYS21' AND NVL (pt.ISS_TRN_QTY, 0)>0  AND (   UPPER (BATCH_CODE) LIKE :SearchQuery OR UPPER (pt.ARTICAL_CODE) LIKE :SearchQuery   OR UPPER (pt.ARTICAL_DESC) LIKE :SearchQuery  OR UPPER (GREY_LOT_NO) LIKE :SearchQuery   OR UPPER (PT.PA_NO) LIKE :SearchQuery OR UPPER (PT.BATCH_ISSUE_NO) LIKE :SearchQuery   OR UPPER (PT.SHADE_CODE) LIKE :SearchQuery ) ORDER BY   PT.BATCH_CODE DESC) WHERE   ROWNUM <= 15";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PO_NUMB  NOT IN  ( SELECT PO_NUMB FROM (  SELECT   DISTINCT  ( BATCH_CODE  || '@'|| PT.PA_NO  || '@'  || GREY_LOT_NO   || '@'  || pt.ARTICAL_CODE  || '@'|| pt.ARTICAL_DESC || '@' || B.SHADE_CODE   || '@'  || B.SHADE_CODE  || '@' || UOM_OF_UNIT  || '@'  || (NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0))  || '@'  || PROCESS   || '@' || MACHINE_CODE || '@'|| pt.PRTY_CODE || '@'|| pt.PARTY_NAME || '@'|| pt.BUSINESS_TYPE||'@'||pt.BATCH_ISSUE_NO)   po_Item_trn,    BATCH_CODE PO_NUMB,  PT.PA_NO ,  GREY_LOT_NO,    pt.ARTICAL_CODE, TRN_QTY PRD_QTY,pt.ARTICAL_DESC,   NVL (PACKED_QTY, 0) QTY_PACKED,    NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) AS QTY_REM,  B.SHADE_CODE,   B.SHADE_CODE SHADE_FAMILY,  MACHINE_CODE,PT.ISS_TRN_QTY, PT.ISS_COPS,pt.BATCH_ISSUE_NO FROM   V_YRN_PROD_DYEING_TRN pt, OD_CAPT_TRN_BOM B  WHERE       NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) > 0  AND NVL (COPS, 0) - NVL (PACKED_NO_OF_UNIT, 0) > 0  AND B.PI_NO = PT.PA_NO  AND B.ASS_ARTICAL_CODE LIKE '%' || PT.ARTICAL_CODE || '%'   AND B.PRODUCT_TYPE = 'YARN DYEING'  AND B.COMP_CODE = PT.COMP_CODE   AND B.BRANCH_CODE = PT.BRANCH_CODE     AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'         AND PT.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "'   AND pt.BUSINESS_TYPE ='SW' AND TRN_TYPE = 'RYS21' AND NVL (pt.ISS_TRN_QTY, 0)>0  AND (   UPPER (BATCH_CODE) LIKE :SearchQuery OR UPPER (pt.ARTICAL_CODE) LIKE :SearchQuery   OR UPPER (pt.ARTICAL_DESC) LIKE :SearchQuery  OR UPPER (GREY_LOT_NO) LIKE :SearchQuery   OR UPPER (PT.PA_NO) LIKE :SearchQuery OR UPPER (PT.BATCH_ISSUE_NO) LIKE :SearchQuery   OR UPPER (PT.SHADE_CODE) LIKE :SearchQuery ) ORDER BY   PT.BATCH_CODE DESC) WHERE   ROWNUM <='" + startOffset + "' )";
                }
                string SortExpression = "";
                string SearchQuery = text + "%";
                dt =  SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
            }
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPODataCount(string text)
    {
        DataTable data = null;
        if (ddlPackingType.SelectedValue == "RYJ32")
        {
            string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT  ( BATCH_CODE  || '@'|| PT.PA_NO  || '@'  || GREY_LOT_NO   || '@'  || pt.ARTICAL_CODE  || '@'|| pt.ARTICAL_DESC || '@' || B.SHADE_CODE   || '@'  || B.SHADE_CODE  || '@' || UOM_OF_UNIT  || '@'  || (NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0))  || '@'  || PROCESS   || '@' || MACHINE_CODE || '@'|| pt.PRTY_CODE || '@'|| pt.PARTY_NAME || '@'|| pt.BUSINESS_TYPE)   po_Item_trn,    BATCH_CODE PO_NUMB,  PT.PA_NO ,  GREY_LOT_NO,    pt.ARTICAL_CODE, TRN_QTY PRD_QTY,pt.ARTICAL_DESC,   NVL (PACKED_QTY, 0) QTY_PACKED,    NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) AS QTY_REM,  B.SHADE_CODE,   B.SHADE_CODE SHADE_FAMILY,  MACHINE_CODE FROM   V_YRN_PROD_DYEING_TRN pt, OD_CAPT_TRN_BOM B  WHERE       NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) > 0  AND NVL (COPS, 0) - NVL (PACKED_NO_OF_UNIT, 0) > 0  AND B.PI_NO = PT.PA_NO  AND B.ASS_ARTICAL_CODE LIKE '%' || PT.ARTICAL_CODE || '%'   AND B.PRODUCT_TYPE = 'YARN DYEING'  AND B.COMP_CODE = PT.COMP_CODE   AND B.BRANCH_CODE = PT.BRANCH_CODE   AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'         AND PT.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "'  AND pt.BUSINESS_TYPE ='JW'  AND TRN_TYPE = 'RYS21'  AND (   UPPER (BATCH_CODE) LIKE :SearchQuery OR UPPER (pt.ARTICAL_CODE) LIKE :SearchQuery   OR UPPER (pt.ARTICAL_DESC) LIKE :SearchQuery  OR UPPER (GREY_LOT_NO) LIKE :SearchQuery   OR UPPER (PT.PA_NO) LIKE :SearchQuery) ORDER BY   PO_NUMB) ";
            string whereClause = string.Empty;
            string SortExpression = " order by PO_NUMB";
            string SearchQuery = text + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        else
        {
            string CommandText = "SELECT   *  FROM   (  SELECT   DISTINCT  ( BATCH_CODE  || '@'|| PT.PA_NO  || '@'  || GREY_LOT_NO   || '@'  || pt.ARTICAL_CODE  || '@'|| pt.ARTICAL_DESC || '@' || B.SHADE_CODE   || '@'  || B.SHADE_CODE  || '@' || UOM_OF_UNIT  || '@'  || (NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0))  || '@'  || PROCESS   || '@' || MACHINE_CODE || '@'|| pt.PRTY_CODE || '@'|| pt.PARTY_NAME || '@'|| pt.BUSINESS_TYPE)   po_Item_trn,    BATCH_CODE PO_NUMB,  PT.PA_NO ,  GREY_LOT_NO,    pt.ARTICAL_CODE, TRN_QTY PRD_QTY,pt.ARTICAL_DESC,   NVL (PACKED_QTY, 0) QTY_PACKED,    NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) AS QTY_REM,  B.SHADE_CODE,   B.SHADE_CODE SHADE_FAMILY,  MACHINE_CODE FROM   V_YRN_PROD_DYEING_TRN pt, OD_CAPT_TRN_BOM B  WHERE       NVL (TRN_QTY, 0) - NVL (PACKED_QTY, 0) > 0  AND NVL (COPS, 0) - NVL (PACKED_NO_OF_UNIT, 0) > 0  AND B.PI_NO = PT.PA_NO  AND B.ASS_ARTICAL_CODE LIKE '%' || PT.ARTICAL_CODE || '%'   AND B.PRODUCT_TYPE = 'YARN DYEING'  AND B.COMP_CODE = PT.COMP_CODE   AND B.BRANCH_CODE = PT.BRANCH_CODE   AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'         AND PT.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "'  AND pt.BUSINESS_TYPE ='SW' AND TRN_TYPE = 'RYS21'  AND (   UPPER (BATCH_CODE) LIKE :SearchQuery OR UPPER (pt.ARTICAL_CODE) LIKE :SearchQuery   OR UPPER (pt.ARTICAL_DESC) LIKE :SearchQuery  OR UPPER (GREY_LOT_NO) LIKE :SearchQuery   OR UPPER (PT.PA_NO) LIKE :SearchQuery) ORDER BY   PO_NUMB) ";
            string whereClause = string.Empty;
            string SortExpression = " order by PO_NUMB";
            string SearchQuery = text + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        return data.Rows.Count;
    }
    protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = cmbPOITEM.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter); 
            txtPONumb.Text = arrString[0].ToString();
            txtPINO.Text = arrString[1].ToString();
            txtLotNo.Text=arrString[2].ToString();
            txtICODE.Text = arrString[3].ToString();
            txtDESC.Text = arrString[4].ToString();
            txtShadeCode.Text = arrString[5].ToString();
            txtShadeFamily.Text = arrString[6].ToString();
            txtUNIT.Text = arrString[7].ToString();
            lblMaxQTY.Text = arrString[8].ToString();
            txtBatchNo.Text = arrString[14].ToString();
            ddlFinishedType.SelectedIndex=ddlFinishedType.Items.IndexOf(ddlFinishedType.Items.FindByValue(arrString[9].ToString()));    
            lblPO_BRANCH.Text = oUserLoginDetail.CH_BRANCHCODE;
            lblPO_COMP.Text = oUserLoginDetail.COMP_CODE ;
            lblPO_TYPE.Text = "PRD01";
            
            // New Code For Party Auto Bind the Job Work And Sale Work

            BUSINESS_TYPE = arrString[13];
            if (BUSINESS_TYPE == "JW")
            {
                CmBParty.SelectedText = (arrString[11]);
                CmBParty.SelectedValue = (arrString[11]);
                ComboBoxItem item1 = new ComboBoxItem(arrString[11]);
                string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                string whereClause = string.Empty;

                string SortExpression = " order by PRTY_CODE";
                string SearchQuery = "%";
                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
                CmBParty.DataSource = dt;
                CmBParty.DataTextField = "PRTY_CODE";
                CmBParty.DataValueField = "PRTY_NAME";
                CmBParty.DataBind();


                int Party = -1;
                foreach (ComboBoxItem JW in CmBParty.Items)
                {
                    if (JW.Text == item1.Text)
                    {
                        CmBParty.SelectedIndex = Party;
                        CmBParty.SelectedText = JW.Text;
                        CmBParty.SelectedValue = JW.Value;
                        lblPartyCode1.Text = JW.Text;
                        txtPartyAddress1.Text = JW.Value;
                        break;
                    }
                    Party++;
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
                CmBParty.DataSource = dt;
                CmBParty.DataTextField = "PRTY_CODE";
                CmBParty.DataValueField = "PRTY_NAME";
                CmBParty.DataBind();


                int SELF = -1;
                foreach (ComboBoxItem SW in CmBParty.Items)
                {
                    if (SW.Text == item1.Text)
                    {
                        CmBParty.SelectedIndex = SELF;
                        CmBParty.SelectedText = SW.Text;
                        CmBParty.SelectedValue = SW.Value;
                        lblPartyCode1.Text = SW.Text;
                        txtPartyAddress1.Text = SW.Value;
                        break;
                    }
                    SELF++;
                }


            }
            DataTable dataMac = GetMachineData("");
            ddlMachineCode.DataSource = dataMac;
            ddlMachineCode.DataTextField = "MACHINE_CODE";
            ddlMachineCode.DataValueField = "MACHINE_CODE";
            ddlMachineCode.DataBind();

            foreach (Obout.ComboBox.ComboBoxItem item in ddlMachineCode.Items)
            {
                if (item.Text == arrString[10].ToString().Trim())
                {
                    //ddlMachineCode.SelectedIndex = ddlMachineCode.Items.IndexOf(item);                   
                    ddlMachineCode.SelectedText = item.Text.ToString();
                    ddlMachineCode.SelectedValue = item.Value.ToString();
                    break;
                }
            }

            DataTable cartonDt = GetCartonDetailsFromBOM(txtPINO.Text, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.DT_STARTDATE.Year );
            if (cartonDt.Rows.Count > 0)
            {
                DataView cartonDv = new DataView(cartonDt);
                cartonDv.RowFilter = "ITEM_TYPE='CARTON'";
                if (cartonDv.Count > 0)
                {
                    txtFormType.DataSource = cartonDv;                    
                    txtFormType.DataTextField = "ITEM_DESC";
                    txtFormType.DataValueField = "ITEM_CODE";
                    txtFormType.DataBind();
                    foreach (ComboBoxItem item in txtFormType.Items)
                    {
                        if (item.Value == cartonDv[0]["ITEM_CODE"].ToString().Trim())
                        {
                            txtFormType.SelectedIndex = txtFormType.Items.IndexOf(item);
                            hdnCartonWt.Value = cartonDv[0]["WEIGHT"].ToString();
                            break;
                        }
                    }

                   
                }


                DataView copsDv = new DataView(cartonDt);
                copsDv.RowFilter = "ITEM_TYPE='PAPER TUBE'";
                if (copsDv.Count > 0)
                {

                    txtFormRefNo.DataSource = copsDv;
                    txtFormRefNo.DataTextField = "ITEM_DESC";
                    txtFormRefNo.DataValueField = "ITEM_CODE";
                    txtFormRefNo.DataBind();
                    foreach (ComboBoxItem item in txtGrade.Items)
                    {
                        if (item.Value == copsDv[0]["ITEM_CODE"].ToString().Trim())
                        {
                            txtFormRefNo.SelectedIndex = txtFormRefNo.Items.IndexOf(item);
                            hdnPaperTubeWt.Value = copsDv[0]["WEIGHT"].ToString();
                            break;
                        }
                    }
                }
            }



            grdSale.Columns.Clear();
            DataTable dt3 = new DataTable();
            dt3 = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectBySaledetails(cmbPOITEM.SelectedText.ToString(), oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.DT_STARTDATE.Year.ToString());
            grdSale.DataSource = dt3;
            grdSale.DataBind();


            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Po Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            //txtBasicRate.Text = "";
            //txtFinalRate.Text = "";
            //txtAmount.Text = "";
            //txtCostCode.Text = "";
            //txtDOM.Text = "";
             //txtPoRate.Text = string.Empty;
            txtDetRemarks.Text = "";
            chk_QCFlag.Checked = false;
            lblPO_BRANCH.Text = "";
            lblPO_COMP.Text = "";
            lblPO_TYPE.Text = "";
            txtPONumb.Text = string.Empty;
            txtNoOfUnit.Text = "";
            txtWeightOfUnit.Text = "";
            txtUOm.Text = "";
            lblMaxQTY.Text = "";
            ViewState["UNIQUEID"] = null;
            txtNoOfUnit.Text = string.Empty;
            txtWeightOfUnit.Text = string.Empty;
            txtUOm.Text = string.Empty;
            txtShadeCode.Text = string.Empty;
            txtShadeFamily.Text = string.Empty;
            txtGrade.SelectedIndex = -1;
            txtTareWt.Text = string.Empty;
            txtGrossWt.Text=string.Empty;
            txtCartons.Text = string.Empty;
            txtPINO.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            ddlMachineCode.SelectedIndex = -1;
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
                txtPONumb.Text = dv[0]["PO_NUMB"].ToString();
                cmbPOITEM.Enabled = false;
                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                txtICODE.Text = dv[0]["YARN_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtShadeCode.Text = dv[0]["SHADE_CODE"].ToString();
                txtShadeFamily.Text = dv[0]["SHADE_FAMILY"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                lblMaxQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                //txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                //txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                //txtAmount.Text = dv[0]["AMOUNT"].ToString();
                //txtCostCode.Text = dv[0]["COST_CENTER_CODE"].ToString();
                //txtPoRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                txtNoOfUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightOfUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtUOm.Text = dv[0]["UOM_OF_UNIT"].ToString();    
                if (dv[0]["QCFLAG"].ToString() == "Yes" || dv[0]["QCFLAG"].ToString() == "1")
                    chk_QCFlag.Checked = true;

                else
                    chk_QCFlag.Checked = false;                
                
                
                txtTareWt.Text = dv[0]["TARE_WT"].ToString();
                txtGrossWt.Text = dv[0]["GROSS_WT"].ToString();
                txtCartons.Text = dv[0]["CARTONS"].ToString();
                txtPINO.Text = dv[0]["PI_NO"].ToString();
                txtLotNo.Text = dv[0]["LOT_NO"].ToString();
                txtBatchNo.Text = dv[0]["DYED_BATCH"].ToString();
                ddlFinishedType.SelectedIndex = ddlFinishedType.Items.IndexOf(ddlFinishedType.Items.FindByValue(dv[0]["FINISH_TYPE"].ToString()));    
           
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
                        //txtGrade.SelectedIndex = txtGrade.Items.IndexOf(item);
                        txtGrade.SelectedText = item.Text.ToString();
                        txtGrade.SelectedValue = item.Value.ToString();
                        break;
                    }
                }

                DataTable dataMac = GetMachineData("");
                ddlMachineCode.DataSource = dataMac;
                ddlMachineCode.DataTextField = "MACHINE_CODE";
                ddlMachineCode.DataValueField = "MACHINE_CODE";
                ddlMachineCode.DataBind();

                foreach (Obout.ComboBox.ComboBoxItem item in ddlMachineCode.Items)
                {
                    if (item.Text == dv[0]["MAC_CODE"].ToString())
                    {
                        //ddlMachineCode.SelectedIndex = ddlMachineCode.Items.IndexOf(item);                   
                        ddlMachineCode.SelectedText = item.Text.ToString();
                        ddlMachineCode.SelectedValue = item.Value.ToString();
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
                string PO_Type = arrString[0].ToString();
                int PONumb = int.Parse(arrString[1].ToString());
                string Yarn_Code = arrString[2].ToString();
              

                if (data != null && data.Rows.Count > 0)
                {
                    DataView dv = new DataView(data);
                    dv.RowFilter = "COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND PO_NUMB='" + PONumb + "' AND YARN_CODE='" + Yarn_Code + "' ";
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
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA,  trunc(A.GATE_DATE) as GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'YARN PURCHASE IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15";
            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {
                CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA,  trunc(A.GATE_DATE) as GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'YARN PURCHASE IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15 ";
            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "Save", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT) GATE_DATA, trunc(A.GATE_DATE) as GATE_DATE,A.GATE_NUMB,A.GATE_TYPE,a.prty_code,a.prty_name,a.trsp_code,a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='YARN PURCHASE IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }
                else if (string.Compare(lblMode.Text, "Update", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT) GATE_DATA, trunc(A.GATE_DATE) as GATE_DATE,A.GATE_NUMB,A.GATE_TYPE, a.trsp_code, a.trsp_name,A.ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='YARN PURCHASE IN')asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0)asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB)WHERE ROWNUM <= " + startOffset + ")";
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
            CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'YARN PURCHASE IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
        }
        else if (string.Compare(lblMode.Text, "Update", true) != 1)
        {
            CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'YARN PURCHASE IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
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
            //if (ViewState["TRN_TYPE"] != null)
            //    TRN_TYPE = (string)ViewState["TRN_TYPE"];

            string CommandText = "SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b  WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND  DECODE(A.TRN_TYPE,'RYP01','NA','RYP02','NA',A.TRN_TYPE) = DECODE('" + ddlPackingType.SelectedValue + "','RYP01','NA','RYP02','NA','" + ddlPackingType.SelectedValue + "') AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE ( TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery)  AND ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND TRN_NUMB NOT IN (SELECT TRN_NUMB FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME  FROM YRN_IR_MST a, tx_vendor_mst b WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND DECODE(A.TRN_TYPE,'RYP01','NA','RYP02','NA',A.TRN_TYPE) = DECODE('" + ddlPackingType.SelectedValue + "','RYP01','NA','RYP02','NA','" + ddlPackingType.SelectedValue + "') AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE ( TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) AND ROWNUM <= '" + startOffset + "')";
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
            //if (ViewState["TRN_TYPE"] != null)
            //    TRN_TYPE = (string)ViewState["TRN_TYPE"];

            string CommandText = "SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b  WHERE NVL (A.CONF_FLAG, 0) = 0 AND a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + ddlPackingType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY TRN_NUMB DESC, TRN_DATE DESC) asd WHERE ( TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery)  AND ROWNUM <= 15";
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
                IsUpdateCall_PACKING = 1;
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

            //spnAInW.InnerText = "Amount In Words... ";
            double dPartyBillAmount = 0;
            if (double.TryParse(txtPartyBillAmount.Text.Trim(), out dPartyBillAmount))
            {
                AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
                // lblPartyBillAmountInWords.Text = oRupeesToWord.changeNumericToWords(dPartyBillAmount);
                //spnAInW.InnerText = "Amount In Words... " + oRupeesToWord.changeNumericToWords(dPartyBillAmount);
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

            if (txtPONumb.Text != string.Empty && txtGrade.SelectedValue!=string.Empty )
            {
                txtQTY.ReadOnly = false;
                txtNoOfUnit.ReadOnly = false;
                txtUOm.ReadOnly = false;
                txtWeightOfUnit.ReadOnly = false;
                txtGrossWt.ReadOnly = false;
                txtTareWt.ReadOnly = false;
                txtCartons.ReadOnly = false;
                string URL = "YRN_TRN_SUB_PACKING.aspx";
                string[] arrstr = txtICODE.Text.Split('-');
                if (arrstr.Length > 0)
                {
                   
                    URL = URL + "?YARN_CODE=" + arrstr[0].ToString();
                }
                else
                {
                    URL = URL + "?YARN_CODE=" + txtICODE.Text;
                }
                              
                URL = URL + "&LOT_NO=" + txtLotNo.Text.Trim();
                URL = URL + "&PI_NO=" + txtPINO.Text;
                URL = URL + "&GRADE=" + txtGrade.SelectedValue; 
                URL = URL + "&lblMaxQTY=" + lblMaxQTY.Text;
                URL = URL + "&txtQTY=" + txtQTY.ClientID;
                URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
                URL = URL + "&txtWeightOfUnit=" + txtWeightOfUnit.ClientID;
                URL = URL + "&GROSS_WT=" + txtGrossWt.ClientID;
                URL = URL + "&TARE_WT=" + txtTareWt.ClientID;
                URL = URL + "&CARTONS=" + txtCartons.ClientID;                
                URL = URL + "&txtShadeCode=" + txtShadeCode.Text.Trim();
                URL = URL + "&txtShadeFamily=" + txtShadeFamily.Text.Trim();
                URL = URL + "&PO_COMP_CODE=" + lblPO_COMP.Text.Trim();
                URL = URL + "&PO_BRANCH=" + lblPO_BRANCH.Text.Trim();
                URL = URL + "&PO_TYPE=" + lblPO_TYPE.Text.Trim();
                URL = URL + "&PO_NUMB=" + txtPONumb.Text.Trim();
                URL = URL + "&PO_YEAR=" + oUserLoginDetail.DT_STARTDATE.Year ;
                URL = URL + "&TRN_TYPE=" + ddlPackingType.SelectedValue;
                URL = URL + "&CWT=" + hdnCartonWt.Value;
                URL = URL + "&PWT=" + hdnPaperTubeWt.Value;
                URL = URL + "&FINISH=" + ddlFinishedType.SelectedValue ;
                URL = URL + "&BATCH_NO=" + txtBatchNo.Text;
                //URL = URL + "&txtUOm=" + txtUOm.ClientID;
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=900,height=500,left=50,top=50');", true);
                
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Lot Number and Grade.");
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
                        string SHADE_CODE = dv[0]["SHADE_CODE"].ToString();
                        string SHADE_FAMILY = dv[0]["SHADE_FAMILY"].ToString();
                        string PI_NO = dv[0]["PI_NO"].ToString();
                        string LOT_NO = dv[0]["LOT_NO"].ToString();
                        string GRADE = dv[0]["GRADE"].ToString();
                        string PO_NUMB = dv[0]["PO_NUMB"].ToString();
                        string PO_YEAR = dv[0]["PO_YEAR"].ToString();
                        string FINISH_TYPE = dv[0]["FINISH_TYPE"].ToString();
                        if (Session["dtTRN_SUB_PACKING"] != null)
                        {
                            DataTable dtTRN_SUB_PACKING = (DataTable)Session["dtTRN_SUB_PACKING"];

                            DataView dvYRNDRecieve_trn = new DataView(dtTRN_SUB_PACKING);
                            dvYRNDRecieve_trn.RowFilter = "YARN_CODE='" + YARN_CODE + "' AND SHADE_CODE='" + SHADE_CODE + "' AND SHADE_FAMILY='" + SHADE_FAMILY + "'  AND PO_NUMB='" + PO_NUMB + "'  AND PI_NO='" + PI_NO + "'  AND LOT_NO='" + LOT_NO + "'  AND GRADE='" + GRADE + "' AND PO_YEAR='" + PO_YEAR + "'  AND FINISH_TYPE='" + FINISH_TYPE + "'";
                            if (dvYRNDRecieve_trn.Count > 0)
                            {
                                GridView grdBOM = e.Row.FindControl("grdBOM") as GridView;
                                grdBOM.DataSource = dvYRNDRecieve_trn;
                                grdBOM.DataBind();
                                CalculateAllData(grdBOM);
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
    protected void CalculateAllData(GridView grdsub_trn)
    {
        if (grdsub_trn.Rows.Count > 0)
        {
            double totalcartonno = 0;
            double totalNoUnit = 0;
            double totalGrossWt = 0;
            double totalTareWt = 0;
            double totalQTY = 0;


            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                double cartonno = 0;
                double NoUnit = 0;
                double GrossWt = 0;
                double TareWt = 0;
                double QTY = 0;

                //Label lbtcartonno = grdsub_trn.Rows[i].FindControl("lbtcartonno") as Label;
                Label lblNoUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                Label lblGrossWt = grdsub_trn.Rows[i].FindControl("lblGrossWt") as Label;
                Label lblTareWt = grdsub_trn.Rows[i].FindControl("lblTareWt") as Label;
                Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;

                //double.TryParse(lbtcartonno.Text, out cartonno);
                double.TryParse(lblNoUnit.Text, out NoUnit);
                double.TryParse(lblGrossWt.Text, out GrossWt);
                double.TryParse(lblTareWt.Text, out TareWt);
                double.TryParse(lblQTY.Text, out QTY);
                totalcartonno = totalcartonno + 1;
                totalNoUnit = totalNoUnit + NoUnit;
                totalGrossWt = totalGrossWt + GrossWt;
                totalTareWt = totalTareWt + TareWt;
                totalQTY = totalQTY + QTY;



            }

            ((Label)grdsub_trn.FooterRow.FindControl("lblFBarcodeNo")).Text = Math.Round(totalcartonno, 3).ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("lblFNoUnit")).Text = Math.Round(totalNoUnit, 3).ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("lblFGrossWt")).Text = Math.Round(totalGrossWt, 3).ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("lblFTareWt")).Text = Math.Round(totalTareWt, 3).ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("lblFQTY")).Text = Math.Round(totalQTY, 3).ToString();

        }
    }

    protected void txtQTY_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
        txtQTY.ReadOnly = true;
        txtNoOfUnit.ReadOnly = true;
        txtUOm.ReadOnly = true;
        txtWeightOfUnit.ReadOnly = true;
    }

    protected void CalculateAmount()
    {
        //double Amount = 0;
        //double fRate = 0;
        //double Qty = 0;
        //double.TryParse(txtFinalRate.Text.Trim(), out fRate);
        //double.TryParse(txtQTY.Text.Trim(), out Qty);
        //Amount = Qty * fRate;
        //txtAmount.Text = Amount.ToString();
    }

    protected void btnOtherCharges_Click(object sender, EventArgs e)
    {
        try
        {
            txtPartyBillAmount.ReadOnly = false;
            string URL = "MRNDisTaxAdjMST.aspx";
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
            //string URL = "MRNDisTaxAdj.aspx";
            //URL = URL + "?FinalAmount=" + txtBasicRate.Text.Trim();
            //URL = URL + "&TextBoxId=" + txtFinalRate.ClientID;
            //URL = URL + "&PO_COMP_CODE=" + lblPO_COMP.Text.Trim();
            //URL = URL + "&PO_BRANCH=" + lblPO_BRANCH.Text.Trim();
            //URL = URL + "&PO_TYPE=" + lblPO_TYPE.Text.Trim();
            //URL = URL + "&PO_NUMB=" + txtPONumb.Text.Trim();
            //URL = URL + "&YARN_CODE=" + txtICODE.Text.Trim();
            //URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
            //URL = URL + "&SHADE_FAMILY=" + txtShadeFamily.Text.Trim();
            //txtFinalRate.ReadOnly = false;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting discount/ taxes adjustment for transaction."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbPOITEM_PreRender(object sender, EventArgs e)
    {

    }

    protected void CalculateTotalAmount()
    {
        try
        {
            double TotalAmount = 0;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    double Amount = 0;

                    double.TryParse(dr["AMOUNT"].ToString(), out Amount);
                    TotalAmount += Amount;
                }
            }
            txtTotalAmount.Text = Math.Round(TotalAmount, 3).ToString();
        }
        catch
        {
            throw;
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

    private bool CheckBillEntryExistence(string BillNo, string PartyName, string PartyCode, int Year)
    {
        try
        {
            return  SaitexBL.Interface.Method.YRN_IR_MST.CheckBillEntryExistence(BillNo, PartyName, PartyCode, Year);
            
        }
        catch
        {
            throw;

        }
    }

    

    protected void lblMaxQTY_TextChanged(object sender, EventArgs e)
    {

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




    protected void txtFormType_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset, "CARTON");
           
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtFormType.Items.Clear();

                txtFormType.DataSource = data;
                txtFormType.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text, "CARTON");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article  loading.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();

        }
    }
    protected void txtFormRefNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset, "PAPERTUBE-PCW/PLY/PINEAPPLE");

            // Looping through the items and adding them to the "Items" collection of the ComboBox/ Packing Cortoon details
            if (data != null && data.Rows.Count > 0)
            {
                txtFormRefNo.Items.Clear();

                txtFormRefNo.DataSource = data;
                txtFormRefNo.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text, "PAPERTUBE-PCW/PLY/PINEAPPLE");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article  loading.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();

        }
    }


    protected DataTable GetItems(string text, int startOffset, string TYPE)
    {
        try
        {
            //string CommandText = "  SELECT   ITEM_CODE,           ITEM_DESC,   ITEM_TYPE,   UOM,       ITEM_DESC          || '@' ||ITEM_CODE   || '@'       || UOM    || '@' || NVL(WEIGHT,0)        AS Combined              FROM   TX_ITEM_MST      WHERE        ITEM_TYPE='" + TYPE + "'      AND (ITEM_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery)               AND      ROWNUM <= 15          ORDER BY   ITEM_CODE            ";
            string CommandText = "  SELECT   ITEM_CODE,           ITEM_DESC           FROM   TX_ITEM_MST      WHERE        ITEM_TYPE='" + TYPE + "'      AND (ITEM_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery)               AND      ROWNUM <= 15                 ";
            
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_CODE NOT IN   ( SELECT   ITEM_CODE           FROM   TX_ITEM_MST      WHERE         ITEM_TYPE='" + TYPE + "'      AND (ITEM_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery)   AND  ROWNUM <= " + startOffset + "                      )";
            
            }

            string SortExpression = "";
            string SearchQuery = text.ToUpper() + "%";
           return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            
        }
        catch
        {
            throw;
        }
    }



    protected int GetItemsCount(string text,string TYPE)
    {
        string CommandText = " SELECT   ITEM_CODE           FROM   TX_ITEM_MST      WHERE         ITEM_TYPE='" + TYPE + "'      AND (ITEM_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery)                       ORDER BY   ITEM_CODE  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        
    }





    protected DataTable GetCartonDetailsFromBOM(string PI_NO, string  COMP_CODE,string BRANCH_CODE,int  Year )
    {
        try
        {
            string CommandText = "  SELECT VB.BASE_ARTICAL_CODE as ITEM_CODE,           IM.ITEM_DESC,   IM.ITEM_TYPE,   IM.UOM,       IM.ITEM_DESC ,NVL(IM.WEIGHT,0) WEIGHT , IM.ITEM_DESC || '@' ||IM.ITEM_CODE   || '@'       || IM.UOM    || '@' || NVL(IM.WEIGHT,0)        AS Combined      FROM V_OD_CAPT_TRN_BOM VB, TX_ITEM_MST IM WHERE VB.MASTER_DEPT='ITEM' AND IM.ITEM_CODE=VB.BASE_ARTICAL_CODE AND VB.PI_NO='" + PI_NO + "'  AND VB.COMP_CODE='" + COMP_CODE + "' AND VB.BRANCH_CODE='" + BRANCH_CODE + "'  AND VB.YEAR=" + Year + " AND  ITEM_TYPE LIKE '%'      AND (IM.ITEM_CODE LIKE :SearchQuery         OR IM.ITEM_DESC LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery)";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", "%", "");


        }
        catch
        {
            throw;
        }
    }






    protected void txtFormType_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        //string[] strArr = txtFormType.SelectedText.Split('@');
        //if (strArr.Length > 0)
        //{
        //    hdnCartonWt.Value = strArr[3].ToString();
        //}
        hdnCartonWt.Value = SaitexBL.Interface.Method.TX_ITEM_MST.GetWeightForItem(txtFormType.SelectedValue).ToString();
    }
    protected void txtFormRefNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        //string[] strArr = txtFormRefNo.SelectedText.Split('@');
        //if (strArr.Length > 0)
        //{
        //    hdnPaperTubeWt.Value = strArr[3].ToString();
        //}
        hdnPaperTubeWt.Value = SaitexBL.Interface.Method.TX_ITEM_MST.GetWeightForItem(txtFormRefNo.SelectedValue).ToString();
    }

    protected void ddlMachineCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMachineData(e.Text.ToUpper());
            ddlMachineCode.Items.Clear();
            ddlMachineCode.DataSource = data;
            ddlMachineCode.DataBind();
            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in loading Machine data.\r\nSee error log for detail."));
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
        catch
        {
            throw;
        }
    }

    
}
