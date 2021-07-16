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
using Common;
using errorLog;
using Obout.ComboBox;

public partial class Module_Yarn_SalesWork_Controls_InvoiceToParty : System.Web.UI.UserControl
{
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private DataTable dtDicRate = null;
    private int PO_NUMB = 999998;
    private string PO_TYPE = "MII";
    private string PO_COMP_CODE = "999998";
    private string PO_BRANCH = "999998";

    //private string TRN_TYPE = "IYS26";

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
            //TRN_TYPE = "IYS26";
            Session["dtItemReceipt"] = null;
            Session["dtDicRate"] = null;
            ActivateSaveMode();
            BindShift();
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);
            Blankrecords();
            RDA_NO.Visible = false;
            // BindDelBranch();
            BindNewChallanNum();
            CreateDataTable();
            txtIssueDate.Text = System.DateTime.Now.ToShortDateString();
            RefreshDetailRow();
            CmBParty.SelectedIndex = -1;
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
            oYRN_IR_MST.TRN_TYPE = ddlTrnType.SelectedValue;
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
            txtChallanNumber.Text = string.Empty;
            txtDocDate.Text = string.Empty;
            // txtDocNo.Text = string.Empty;
            txtIssueDate.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            ddlIssueShift.SelectedIndex = 0;

            grdMaterialItemIssue.DataSource = null;
            grdMaterialItemIssue.DataBind();
            lblMode.Text = "You are in Save Mode";
            txtChallanNumber.ReadOnly = true;
            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblTransporterCode.Text = string.Empty;
            lblPartyCode.Text = string.Empty;
            lblConsigneeCode.Text = string.Empty;
            txtPartyAddress.Text = string.Empty;
            txtConsigneeAddress.Text = string.Empty;
            txtTransporterAddress.Text = string.Empty;
            txtConsigneeCode.SelectedIndex = -1;
            txtTransporterCode.SelectedIndex = -1;
            txtInsurancePolicyNo.Text = string.Empty;

            txtLCDate.Text = string.Empty;
            txtbuyer.Text = string.Empty;
            txtdateprp.Text = string.Empty;
            txtdateremvl.Text = string.Empty;
            txtFreight.Text = string.Empty;


            txtSaleTAXRate.Text = string.Empty;
            txtSaleTAXAmt.Text = string.Empty;
            txtSaleSGSTTAXRate.Text = string.Empty;
            txtSaleSGSTTAXAmt.Text = string.Empty;
            txtSaleIGSTTAXRate.Text = string.Empty;
            txtSaleIGSTTAXAmt.Text = string.Empty;

            txtED.Text = string.Empty;
            txtSubTotal.Text = string.Empty;
            txtFinalTotal.Text = string.Empty;

            // txtInsurance.Text = string.Empty;
            // ddlSaleTax.SelectedValue = "Select";
            // txtSaleTAXRate.Text = string.Empty;
            // txtSaleTAXAmt.Text = string.Empty;
            //txtExciseOnBaseRate.Text = string.Empty;
            // txtExciseOnCESSRate1.Text = string.Empty;
            //txtExciseOnCESSRate2.Text = string.Empty;
            // txtExciseTotalRate.Text = string.Empty;
            // txtExciseBaseAmt.Text = string.Empty;
            // txtExciseCESSAmt1.Text = string.Empty;
            //  txtExciseCESSAmt2.Text = string.Empty;
            txtgoodsdisptch.Text = string.Empty;
            txtDestination.Text = string.Empty;
            Session["dtDetailTBL"] = null;

        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("TRNNUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_FAMILY", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
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
            dtDetailTBL.Columns.Add("GROSS_WT", typeof(double));
            dtDetailTBL.Columns.Add("TARE_WT", typeof(double));
            dtDetailTBL.Columns.Add("CARTONS", typeof(double));
            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
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
                DataTable dtDetailTBL = (DataTable)Session["dtDetailTBL"];
                grdMaterialItemIssue.DataSource = dtDetailTBL;
                grdMaterialItemIssue.DataBind();
                GetSubTotal();
            }
            else
            {
                grdMaterialItemIssue.DataSource = null;
                grdMaterialItemIssue.DataBind();
            }
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
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, ddlTrnType.SelectedValue, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtChallanNumber.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();
                txtInvoiceNo.Text = dt.Rows[0]["INVOICE_NO"].ToString().Trim();
                txtBuyerorder.Text = dt.Rows[0]["FORM_NUMB"].ToString().Trim();
                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                lblConsigneeCode.Text = dt.Rows[0]["CONSIGNEE_CODE"].ToString().Trim();
                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TRSP_NAME"].ToString().Trim();
                txtConsigneeAddress.Text = dt.Rows[0]["CONSIGNEE_NAME"].ToString().Trim();
                txtIssueDate.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                txtDocDate.Text = dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                ddlIssueShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();
                txtInsurancePolicyNo.Text = dt.Rows[0]["DA_NO"].ToString().Trim();
                txtLRNumber.Text = dt.Rows[0]["LC_NO"].ToString().Trim();
                txtLCDate.Text = dt.Rows[0]["OR_DATE"].ToString().Trim();
                txtbuyer.Text = dt.Rows[0]["OTHER_BUYER"].ToString().Trim();
                txtdateprp.Text = dt.Rows[0]["DATE_TIME_PREP"].ToString().Trim();
                txtdateremvl.Text = dt.Rows[0]["DATE_TIME_REMOVAL"].ToString().Trim();
                txtFreight.Text = dt.Rows[0]["FREIGHT"].ToString().Trim();
                txtLRNumber.Text = dt.Rows[0]["LR_NUMB"].ToString().Trim();
                DateTime dateLR = DateTime.Now;
                DateTime.TryParse(dt.Rows[0]["LR_DATE"].ToString().Trim(), out dateLR);
                txtLRDate.Text = dateLR.ToShortDateString();

                txtSubTotal.Text = dt.Rows[0]["TOTAL_AMOUNT"].ToString().Trim();
                txtFinalTotal.Text = dt.Rows[0]["FINAL_AMOUNT"].ToString().Trim();

                //txtDocNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                //txtLCNo.Text = dt.Rows[0]["LC_NO"].ToString().Trim();
                //txtInsurance.Text = dt.Rows[0]["INSURANCE_AMOUNT"].ToString().Trim();
                if (dt.Rows[0]["SALE_TAX_TYPE"].ToString() != "NA")
                {
                    ddlSaleTax.SelectedIndex = ddlSaleTax.Items.IndexOf(ddlSaleTax.Items.FindByValue(dt.Rows[0]["SALE_TAX_TYPE"].ToString().Trim()));
                }
                txtSaleTAXRate.Text = dt.Rows[0]["SALE_TAX_RATE"].ToString().Trim();
                txtSaleTAXAmt.Text = dt.Rows[0]["SALE_TAX_AMOUNT"].ToString().Trim();

                if (dt.Rows[0]["SALE_SGST_TYPE"].ToString() != "NA")
                {
                    ddlSSaleTax.SelectedIndex = ddlSSaleTax.Items.IndexOf(ddlSSaleTax.Items.FindByValue(dt.Rows[0]["SALE_SGST_TYPE"].ToString().Trim()));
                }
                txtSaleSGSTTAXRate.Text = dt.Rows[0]["SALE_SGST_RATE"].ToString().Trim();
                txtSaleSGSTTAXAmt.Text = dt.Rows[0]["SALE_SGST_AMOUNT"].ToString().Trim();

                if (dt.Rows[0]["SALE_IGST_TYPE"].ToString() != "NA")
                {
                    ddlISaleTax.SelectedIndex = ddlISaleTax.Items.IndexOf(ddlISaleTax.Items.FindByValue(dt.Rows[0]["SALE_IGST_TYPE"].ToString().Trim()));
                }
                txtSaleIGSTTAXRate.Text = dt.Rows[0]["SALE_IGST_RATE"].ToString().Trim();
                txtSaleIGSTTAXAmt.Text = dt.Rows[0]["SALE_IGST_AMOUNT"].ToString().Trim();

                //txtExciseOnBaseRate.Text = dt.Rows[0]["EXCISE_BASE_RATE"].ToString().Trim();
                //txtExciseOnCESSRate1.Text = dt.Rows[0]["EXCISE_CESS_RATE_1"].ToString().Trim();
                //txtExciseOnCESSRate2.Text = dt.Rows[0]["EXCISE_CESS_RATE_2"].ToString().Trim();
                //txtExciseTotalRate.Text = dt.Rows[0]["EXCISE_TOTAL_RATE"].ToString().Trim();
                //txtExciseBaseAmt.Text = dt.Rows[0]["EXCISE_BASE_AMOUNT"].ToString().Trim();
                //txtExciseCESSAmt1.Text = dt.Rows[0]["EXCISE_CESS_AMOUNT_1"].ToString().Trim();
                //txtExciseCESSAmt2.Text = dt.Rows[0]["EXCISE_CESS_AMOUNT_2"].ToString().Trim();
                txtED.Text = dt.Rows[0]["ED"].ToString().Trim();
                txtgoodsdisptch.Text = dt.Rows[0]["GOOD_DISPATCH"].ToString().Trim();
                txtDestination.Text = dt.Rows[0]["DESTINATION"].ToString().Trim();
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dt.Rows[0]["LOCATION"].ToString()));
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["STORE"].ToString()));

                //ddlDelAdd.SelectedIndex = ddlDelAdd.Items.IndexOf(ddlDelAdd.Items.FindByValue(dt.Rows[0]["REC_BRANCH_CODE"].ToString().Trim()));
                // ddlDelAdd.Items.Remove(ddlDelAdd.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));
            }
            if (iRecordFound == 1)
            {
                if (Session["dtDicRate"] != null)
                {
                    dtDicRate = (DataTable)Session["dtDicRate"];
                }
                else
                {
                    CreateDataTableDisTaxTrn();
                }

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, ddlTrnType.SelectedValue);
                MapDataTable();
                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.GetIssueAdjByMst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlTrnType.SelectedValue, ChallanNumber);
                    MapAdjustDataTable(dtReceiptAdjustment);

                    dtDicRate.Rows.Clear();
                    // code to get dis taxes for transaction entry  Sale Order
                    DataTable dtDisTaxTrnTemp = SaitexBL.Interface.Method.YRN_IR_MST.GetSaleTrnDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, ddlTrnType.SelectedValue);
                    if (dtDisTaxTrnTemp.Rows.Count > 0)
                    {
                        MapDataTableDisTaxTrn(dtDisTaxTrnTemp);
                    }
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

                if (dtReceiptAdjustment.Rows[iLoop]["PI_NO"] == null)
                    dtReceiptAdjustment.Rows[iLoop]["PI_NO"] = "NA";

                if (dtReceiptAdjustment.Rows[iLoop]["ISS_PI_NO"] == null)
                    dtReceiptAdjustment.Rows[iLoop]["ISS_PI_NO"] = "NA";

            }
            Session["dtItemReceipt"] = dtReceiptAdjustment;
        }
        catch
        {
            throw;
        }
    }

    #region code to map dis taxes datatable for transaction data

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
                    drNew["COMP_CODE"] = dr["COMP_CODE"];
                    drNew["BRANCH_CODE"] = dr["BRANCH_CODE"];
                    drNew["PO_TYPE"] = dr["PO_TYPE"];
                    drNew["PA_NO"] = dr["PA_NO"];
                    drNew["YARN_CODE"] = dr["YARN_CODE"];
                    drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                    drNew["Rate"] = dr["RATE"];
                    drNew["COMPO_SL"] = dr["COMPO_SL"];
                    drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                    drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                    drNew["IS_PO"] = dr["IS_PO"];
                    drNew["SHADE_CODE"] = dr["SHADE_CODE"];
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

    private void CreateDataTableDisTaxTrn()
    {
        try
        {
            dtDicRate = new DataTable();
            dtDicRate.Columns.Add("Uniqueid", typeof(int));
            dtDicRate.Columns.Add("COMP_CODE", typeof(string));
            dtDicRate.Columns.Add("BRANCH_CODE", typeof(string));
            dtDicRate.Columns.Add("PO_TYPE", typeof(string));
            dtDicRate.Columns.Add("PA_NO", typeof(string));
            dtDicRate.Columns.Add("YARN_CODE", typeof(string));
            dtDicRate.Columns.Add("COMPO_CODE", typeof(string));
            dtDicRate.Columns.Add("Rate", typeof(double));
            dtDicRate.Columns.Add("COMPO_SL", typeof(int));
            dtDicRate.Columns.Add("COMPO_TYPE", typeof(string));
            dtDicRate.Columns.Add("Amount", typeof(double));
            dtDicRate.Columns.Add("BASE_COMPO_CODE", typeof(string));
            dtDicRate.Columns.Add("IS_PO", typeof(string));
            dtDicRate.Columns.Add("SHADE_CODE", typeof(string));

            Session["dtDicRate"] = dtDicRate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    private void MapDataTable()
    {
        try
        {

            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
            {
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            }

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
            }
            Session["dtDetailTBL"] = dtDetailTBL;
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
            Response.Redirect("~/Module/OrderDevelopment/Reports/Sale_Invoice_Parameter.aspx?PRODUCT_TYPE=TEXTURISED YARN&TRN_TYPE=" + ddlTrnType.SelectedValue, true);
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
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

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
            }

            if (count == 2)
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
        try
        {
            string Shade = txtShade.Text;
            foreach (GridViewRow grdRow in grdMaterialItemIssue.Rows)
            {
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                Label txtSHADE_CODE = (Label)grdRow.FindControl("txtSHADE_CODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (txtICODE.Text.Trim() == yarnCode && UniqueId != iUniqueId && Shade == txtSHADE_CODE.Text)
                    Result = true;
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
            double Unit = 0;
            //  double Rate = 0;
            double fRate = 0;
            double Amount = 0;
            double.TryParse(CommonFuction.funFixQuotes(txtQTY.Text.Trim()), out Unit);
            // double.TryParse(CommonFuction.funFixQuotes(txtBasicRate.Text.Trim()), out Rate);
            double.TryParse(CommonFuction.funFixQuotes(txtfinal.Text.Trim()), out fRate);
            double.TryParse(CommonFuction.funFixQuotes(txtAmount.Text.Trim()), out Amount);

            Amount = fRate * Unit;

            txtAmount.Text = Amount.ToString();
            // txtBasicRate.Text = Rate.ToString();
            txtfinal.Text = fRate.ToString();
            //txtQTY.Text = Qty.ToString();
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
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST1 = new SaitexDM.Common.DataModel.YRN_IR_MST(); // For Other Details
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtBuyerorder.Text.Trim());
            oYRN_IR_MST.FORM_TYPE = string.Empty;
            oYRN_IR_MST.INVOICE_NO = CommonFuction.funFixQuotes(txtInvoiceNo.Text.Trim());
            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oYRN_IR_MST.GATE_NUMB = string.Empty;
            oYRN_IR_MST.GATE_OUT_NUMB = string.Empty;
            oYRN_IR_MST.GATE_PASS_TYPE = string.Empty;
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNumber.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;
            oYRN_IR_MST.PRTY_CODE = lblPartyCode.Text.Trim();
            oYRN_IR_MST.PRTY_NAME = txtPartyAddress.Text.Trim();
            oYRN_IR_MST.TRSP_CODE = lblTransporterCode.Text.Trim();
            oYRN_IR_MST.CONSIGNEE_CODE = lblConsigneeCode.Text.Trim();
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = "NA";
            oYRN_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();
            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;
            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(ddlTrnType.SelectedValue);
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.TOTAL_AMOUNT = Double.Parse(CommonFuction.funFixQuotes(txtSubTotal.Text.Trim()));
            if (txtFinalTotal.Text != string.Empty)
            {
                oYRN_IR_MST.FINAL_AMOUNT = Double.Parse(txtFinalTotal.Text.Trim());
            }
            else
            {
                oYRN_IR_MST.FINAL_AMOUNT = 0;
            }
            oYRN_IR_MST1.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST1.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST1.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST1.TRN_TYPE = CommonFuction.funFixQuotes(ddlTrnType.SelectedValue);
            oYRN_IR_MST1.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));

            if (txtInsurancePolicyNo.Text != string.Empty)
                oYRN_IR_MST1.DA_NO = txtInsurancePolicyNo.Text.Trim();
            else
                oYRN_IR_MST1.DA_NO = "NA";

            if (txtLCDate.Text != string.Empty)
                oYRN_IR_MST1.OR_DATE = DateTime.Parse(txtLCDate.Text.Trim());
            else
                oYRN_IR_MST1.OR_DATE = System.DateTime.Now;

            if (txtbuyer.Text != string.Empty)
                oYRN_IR_MST1.OTHER_BUYER = txtbuyer.Text.Trim();
            else
                oYRN_IR_MST1.OTHER_BUYER = "NA";



            if (txtdateprp.Text != string.Empty)
                oYRN_IR_MST1.DATE_TIME_PREP = DateTime.Parse(txtdateprp.Text.Trim());
            else
                oYRN_IR_MST1.DATE_TIME_PREP = System.DateTime.Now;

            if (txtdateremvl.Text != string.Empty)
                oYRN_IR_MST1.DATE_TIME_REMOVAL = DateTime.Parse(txtdateremvl.Text.Trim());
            else
                oYRN_IR_MST1.DATE_TIME_REMOVAL = System.DateTime.Now;

            if (txtFreight.Text != string.Empty)
                oYRN_IR_MST1.FREIGHT = double.Parse(txtFreight.Text.Trim());
            else
                oYRN_IR_MST1.FREIGHT = 0;



            //******************code for according to GST *********21/05/2018  By Arun Arun Sharma***//

            if (ddlSaleTax.SelectedValue != "Select")
                oYRN_IR_MST1.SALE_TAX_TYPE = ddlSaleTax.SelectedValue.ToString().Trim();
            else
                oYRN_IR_MST1.SALE_TAX_TYPE = "NA";

            if (txtSaleTAXRate.Text != string.Empty)
                oYRN_IR_MST1.SALE_TAX_RATE = double.Parse(txtSaleTAXRate.Text.Trim());
            else
                oYRN_IR_MST1.SALE_TAX_RATE = 0;

            if (txtSaleTAXAmt.Text != string.Empty)
                oYRN_IR_MST1.SALE_TAX_AMOUNT = double.Parse(txtSaleTAXAmt.Text.Trim());
            else
                oYRN_IR_MST1.SALE_TAX_AMOUNT = 0;


            if (ddlSSaleTax.SelectedValue != "Select")
                oYRN_IR_MST1.SALE_SGST_TYPE = ddlSSaleTax.SelectedValue.ToString().Trim();
            else
                oYRN_IR_MST1.SALE_SGST_TYPE = "NA";

            if (txtSaleSGSTTAXRate.Text != string.Empty)
                oYRN_IR_MST1.SALE_SGST_RATE = double.Parse(txtSaleSGSTTAXRate.Text.Trim());
            else
                oYRN_IR_MST1.SALE_SGST_RATE = 0;

            if (txtSaleSGSTTAXAmt.Text != string.Empty)
                oYRN_IR_MST1.SALE_SGST_AMOUNT = double.Parse(txtSaleSGSTTAXAmt.Text.Trim());
            else
                oYRN_IR_MST1.SALE_SGST_AMOUNT = 0;


            if (ddlISaleTax.SelectedValue != "Select")
                oYRN_IR_MST1.SALE_IGST_TYPE = ddlISaleTax.SelectedValue.ToString().Trim();
            else
                oYRN_IR_MST1.SALE_IGST_TYPE = "NA";

            if (txtSaleIGSTTAXRate.Text != string.Empty)
                oYRN_IR_MST1.SALE_IGST_RATE = double.Parse(txtSaleIGSTTAXRate.Text.Trim());
            else
                oYRN_IR_MST1.SALE_IGST_RATE = 0;

            if (txtSaleIGSTTAXAmt.Text != string.Empty)
                oYRN_IR_MST1.SALE_IGST_AMOUNT = double.Parse(txtSaleIGSTTAXAmt.Text.Trim());
            else
                oYRN_IR_MST1.SALE_IGST_AMOUNT = 0;

            //******************code close for according to GST *********21/05/2018  By Arun Arun Sharma***//





            if (txtgoodsdisptch.Text != string.Empty)
                oYRN_IR_MST1.GOOD_DISPATCH = txtgoodsdisptch.Text.Trim();
            else
                oYRN_IR_MST1.GOOD_DISPATCH = "NA";


            if (txtSubTotal.Text != string.Empty)
                oYRN_IR_MST1.TOTAL_AMOUNT = double.Parse(txtSubTotal.Text.Trim());
            else
                oYRN_IR_MST1.TOTAL_AMOUNT = 0;

            if (txtFinalTotal.Text != string.Empty)
                oYRN_IR_MST1.FINAL_AMOUNT = double.Parse(txtFinalTotal.Text.Trim());
            else
                oYRN_IR_MST1.FINAL_AMOUNT = 0;

            if (txtED.Text != string.Empty)
                oYRN_IR_MST1.ED = double.Parse(txtED.Text.Trim());
            else
                oYRN_IR_MST1.ED = 0;
            if (txtDestination.Text != string.Empty)
                oYRN_IR_MST1.DESTINATION = txtDestination.Text.Trim();
            else
                oYRN_IR_MST1.DESTINATION = "NA";


            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            DataTable dtInvoiceDicRate = (DataTable)Session["dtDicRate"];

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }

            int TRN_NUMB = 0;

            if (txtSaleTAXRate.Text == "0" && txtSaleSGSTTAXRate.Text == "0" && txtSaleIGSTTAXRate.Text == "0")
            {
                Common.CommonFuction.ShowMessage("Please fill the tax Rate");
            }
            else
            {
                bool result = SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, dtItemReceipt, htIssue, dtInvoiceDicRate, oYRN_IR_MST1);
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
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST1 = new SaitexDM.Common.DataModel.YRN_IR_MST(); // For Other Details

            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtBuyerorder.Text.Trim());
            oYRN_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtInvoiceNo.Text.Trim());
            oYRN_IR_MST.INVOICE_NO = CommonFuction.funFixQuotes(txtInvoiceNo.Text.Trim());
            DateTime dt = System.DateTime.Now.Date;
            oYRN_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oYRN_IR_MST.GATE_NUMB = string.Empty;
            oYRN_IR_MST.GATE_OUT_NUMB = string.Empty;
            oYRN_IR_MST.GATE_PASS_TYPE = string.Empty;
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htIssue.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNumber.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            //oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oYRN_IR_MST.PRTY_CODE = lblPartyCode.Text.Trim();
            oYRN_IR_MST.PRTY_NAME = txtPartyAddress.Text.Trim();
            oYRN_IR_MST.TRSP_CODE = lblTransporterCode.Text.Trim();
            oYRN_IR_MST.CONSIGNEE_CODE = lblConsigneeCode.Text.Trim();
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = "NA";
            oYRN_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(ddlTrnType.SelectedValue);
            //oYRN_IR_MST.TRSP_CODE = "NA";
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            //oYRN_IR_MST.LOT_ID_NO = "NA";

            oYRN_IR_MST.TOTAL_AMOUNT = Double.Parse(CommonFuction.funFixQuotes(txtSubTotal.Text.Trim()));
            oYRN_IR_MST.FINAL_AMOUNT = Double.Parse(CommonFuction.funFixQuotes(txtFinalTotal.Text.Trim()));

            oYRN_IR_MST1.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST1.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST1.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST1.TRN_TYPE = CommonFuction.funFixQuotes(ddlTrnType.SelectedValue);
            oYRN_IR_MST1.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtChallanNumber.Text.Trim()));

            if (txtInsurancePolicyNo.Text != string.Empty)
                oYRN_IR_MST1.DA_NO = txtInsurancePolicyNo.Text.Trim();
            else
                oYRN_IR_MST1.DA_NO = "NA";

            if (txtLCDate.Text != string.Empty)
                oYRN_IR_MST1.OR_DATE = DateTime.Parse(txtLCDate.Text.Trim());
            else
                oYRN_IR_MST1.OR_DATE = System.DateTime.Now;

            if (txtbuyer.Text != string.Empty)
                oYRN_IR_MST1.OTHER_BUYER = txtbuyer.Text.Trim();
            else
                oYRN_IR_MST1.OTHER_BUYER = "NA";



            if (txtdateprp.Text != string.Empty)
                oYRN_IR_MST1.DATE_TIME_PREP = DateTime.Parse(txtdateprp.Text.Trim());
            else
                oYRN_IR_MST1.DATE_TIME_PREP = System.DateTime.Now;

            if (txtdateremvl.Text != string.Empty)
                oYRN_IR_MST1.DATE_TIME_REMOVAL = DateTime.Parse(txtdateremvl.Text.Trim());
            else
                oYRN_IR_MST1.DATE_TIME_REMOVAL = System.DateTime.Now;

            if (txtFreight.Text != string.Empty)
                oYRN_IR_MST1.FREIGHT = double.Parse(txtFreight.Text.Trim());
            else
                oYRN_IR_MST1.FREIGHT = 0;




            //******************code for according to GST *********21/05/2018  By Arun Arun Sharma***//

            if (ddlSaleTax.SelectedValue != "Select")
                oYRN_IR_MST1.SALE_TAX_TYPE = ddlSaleTax.SelectedValue.ToString().Trim();
            else
                oYRN_IR_MST1.SALE_TAX_TYPE = "NA";

            if (txtSaleTAXRate.Text != string.Empty)
                oYRN_IR_MST1.SALE_TAX_RATE = double.Parse(txtSaleTAXRate.Text.Trim());
            else
                oYRN_IR_MST1.SALE_TAX_RATE = 0;

            if (txtSaleTAXAmt.Text != string.Empty)
                oYRN_IR_MST1.SALE_TAX_AMOUNT = double.Parse(txtSaleTAXAmt.Text.Trim());
            else
                oYRN_IR_MST1.SALE_TAX_AMOUNT = 0;


            if (ddlSSaleTax.SelectedValue != "Select")
                oYRN_IR_MST1.SALE_SGST_TYPE = ddlSSaleTax.SelectedValue.ToString().Trim();
            else
                oYRN_IR_MST1.SALE_SGST_TYPE = "NA";

            if (txtSaleSGSTTAXRate.Text != string.Empty)
                oYRN_IR_MST1.SALE_SGST_RATE = double.Parse(txtSaleSGSTTAXRate.Text.Trim());
            else
                oYRN_IR_MST1.SALE_SGST_RATE = 0;

            if (txtSaleSGSTTAXAmt.Text != string.Empty)
                oYRN_IR_MST1.SALE_SGST_AMOUNT = double.Parse(txtSaleSGSTTAXAmt.Text.Trim());
            else
                oYRN_IR_MST1.SALE_SGST_AMOUNT = 0;


            if (ddlISaleTax.SelectedValue != "Select")
                oYRN_IR_MST1.SALE_IGST_TYPE = ddlISaleTax.SelectedValue.ToString().Trim();
            else
                oYRN_IR_MST1.SALE_IGST_TYPE = "NA";

            if (txtSaleIGSTTAXRate.Text != string.Empty)
                oYRN_IR_MST1.SALE_IGST_RATE = double.Parse(txtSaleIGSTTAXRate.Text.Trim());
            else
                oYRN_IR_MST1.SALE_IGST_RATE = 0;

            if (txtSaleIGSTTAXAmt.Text != string.Empty)
                oYRN_IR_MST1.SALE_IGST_AMOUNT = double.Parse(txtSaleIGSTTAXAmt.Text.Trim());
            else
                oYRN_IR_MST1.SALE_IGST_AMOUNT = 0;




            //******************code close for according to GST *********21/05/2018  By Arun Arun Sharma***//

            if (txtgoodsdisptch.Text != string.Empty)
                oYRN_IR_MST1.GOOD_DISPATCH = txtgoodsdisptch.Text.Trim();
            else
                oYRN_IR_MST1.GOOD_DISPATCH = "NA";


            if (txtSubTotal.Text != string.Empty)
                oYRN_IR_MST1.TOTAL_AMOUNT = double.Parse(txtSubTotal.Text.Trim());
            else
                oYRN_IR_MST1.TOTAL_AMOUNT = 0;

            if (txtFinalTotal.Text != string.Empty)
                oYRN_IR_MST1.FINAL_AMOUNT = double.Parse(txtFinalTotal.Text.Trim());
            else
                oYRN_IR_MST1.FINAL_AMOUNT = 0;

            if (txtED.Text != string.Empty)
                oYRN_IR_MST1.ED = double.Parse(txtED.Text.Trim());
            else
                oYRN_IR_MST1.ED = 0;


            if (txtDestination.Text != string.Empty)
                oYRN_IR_MST1.DESTINATION = txtDestination.Text.Trim();
            else
                oYRN_IR_MST1.DESTINATION = "NA";


            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            oYRN_IR_MST.BILL_TYPE = string.Empty;
            oYRN_IR_MST.BILL_YEAR = 0;


            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            DataTable dtInvoiceDicRate = (DataTable)Session["dtDicRate"];

            if (Session["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)Session["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable();
            }
            if (txtSaleTAXRate.Text == "0" && txtSaleSGSTTAXRate.Text == "0" && txtSaleIGSTTAXRate.Text == "0")
            {
                Common.CommonFuction.ShowMessage("Please fill the tax Rate");
            }
            else
            {
                bool result = SaitexBL.Interface.Method.YRN_IR_MST.Update(oYRN_IR_MST, dtDetailTBL, dtItemReceipt, htIssue, dtInvoiceDicRate, oYRN_IR_MST1);



                if (result)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage("Data Updated Successfully");
                }
                else
                {
                    CommonFuction.ShowMessage("Data Updation Failed");
                }
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
                if (txtArticleCode.Text != "" && txtQTY.Text != "" && txtBasicRate.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                    bool bb = SearchItemCodeInGrid(txtArticleCode.Text.Trim(), UNIQUEID);
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
                                    dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                    dv[0]["YARN_CODE"] = txtArticleCode.Text.Trim();
                                    dv[0]["SHADE_CODE"] = txtShade.Text.Trim();
                                    dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                                    dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                    dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                    dv[0]["UOM"] = txtUNIT.Text.Trim();
                                    dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["FINAL_RATE"] = double.Parse(txtfinal.Text.Trim());
                                    dv[0]["AMOUNT"] = double.Parse(txtQTY.Text.Trim()) * double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["COST_CODE"] = "";
                                    dv[0]["MAC_CODE"] = "";
                                    dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                    dv[0]["QCFLAG"] = "No";
                                    DateTime dd = System.DateTime.Now;
                                    dv[0]["DATE_OF_MFG"] = dd;

                                    double QtyUnit = 0;
                                    double.TryParse(txtQtyUnit.Text, out QtyUnit);
                                    dv[0]["NO_OF_UNIT"] = QtyUnit;

                                    dv[0]["UOM_OF_UNIT"] = txtQtyUom.Text;

                                    double Unitweight = 0;
                                    double.TryParse(txtQtyWeight.Text, out Unitweight);
                                    dv[0]["WEIGHT_OF_UNIT"] = Unitweight;

                                    dv[0]["PI_NO"] = txtPA_NO.Text.Trim().ToString();

                                    double GROSS_WT = 0;
                                    double.TryParse(txtGrossWt.Value, out GROSS_WT);
                                    dv[0]["GROSS_WT"] = GROSS_WT;

                                    double TARE_WT = 0;
                                    double.TryParse(txtTareWt.Value, out TARE_WT);
                                    dv[0]["TARE_WT"] = TARE_WT;

                                    double CARTONS = 0;
                                    double.TryParse(txtCartons.Value, out CARTONS);
                                    dv[0]["CARTONS"] = CARTONS;
                                    dv[0]["LOT_NO"] = hdnLotNo.Value;
                                    dv[0]["GRADE"] = hdnGrade.Value;
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
                                dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dr["YARN_CODE"] = txtArticleCode.Text.Trim();
                                dr["SHADE_CODE"] = txtShade.Text.Trim();
                                dr["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                                dr["YARN_DESC"] = txtDESC.Text.Trim();
                                dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dr["UOM"] = txtUNIT.Text.Trim();
                                dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["FINAL_RATE"] = double.Parse(txtfinal.Text.Trim());
                                dr["AMOUNT"] = double.Parse(txtQTY.Text.Trim()) * double.Parse(txtBasicRate.Text.Trim());
                                dr["COST_CODE"] = "";
                                dr["MAC_CODE"] = "";
                                dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                dr["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dr["DATE_OF_MFG"] = dd;

                                double QtyUnit = 0;
                                double.TryParse(txtQtyUnit.Text, out QtyUnit);
                                dr["NO_OF_UNIT"] = QtyUnit;

                                dr["UOM_OF_UNIT"] = txtQtyUom.Text;

                                double Unitweight = 0;
                                double.TryParse(txtQtyWeight.Text, out Unitweight);
                                dr["WEIGHT_OF_UNIT"] = Unitweight;

                                dr["PI_NO"] = txtPA_NO.Text.Trim().ToString();

                                double GROSS_WT = 0;
                                double.TryParse(txtGrossWt.Value, out GROSS_WT);
                                dr["GROSS_WT"] = GROSS_WT;

                                double TARE_WT = 0;
                                double.TryParse(txtTareWt.Value, out TARE_WT);
                                dr["TARE_WT"] = TARE_WT;

                                double CARTONS = 0;
                                double.TryParse(txtCartons.Value, out CARTONS);
                                dr["CARTONS"] = CARTONS;
                                dr["LOT_NO"] = hdnLotNo.Value;
                                dr["GRADE"] = hdnGrade.Value;
                                dtDetailTBL.Rows.Add(dr);

                            }
                            RefreshDetailRow();
                            Session["dtDetailTBL"] = dtDetailTBL;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Article with this Shade already included. Please select another article/ shade');", true);
                    }
                }
                grdMaterialItemIssue.DataSource = dtDetailTBL;
                grdMaterialItemIssue.DataBind();
                GetSubTotal();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding YARN detail data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void RefreshDetailRow()
    {
        try
        {
            DDLPiNo.SelectedIndex = -1;
            txtPA_NO.Text = string.Empty;
            txtArticleCode.Text = string.Empty;
            txtDESC.Text = string.Empty;
            txtUNIT.Text = string.Empty;
            txtQTY.Text = string.Empty;
            //   txtBasicRate.Text = string.Empty;
            txtBasicRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtDetRemarks.Text = string.Empty;
            txtfinal.Text = string.Empty;
            txtQtyUnit.Text = string.Empty;
            txtQtyUom.Text = string.Empty;
            txtQtyWeight.Text = string.Empty;
            txtInvoiceNo.Text = string.Empty;
            txtBuyerorder.Text = string.Empty;
            DDLPiNo.Enabled = true;
            txtLRNumber.Text = string.Empty;
            CmBParty.Enabled = true;
            txtLRDate.Text = string.Empty;
            ViewState["UNIQUEID"] = null;
            txtShade.Text = string.Empty;
            txtShadeFamily.Text = string.Empty;
            txtMaxQty.Text = string.Empty;
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
    protected void DDLPiNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DDLPiNo.Items.Clear();
            DataTable data = GetPINo(e.Text.ToUpper(), e.ItemsOffset);
            DDLPiNo.DataTextField = "CUSTNO";
            DDLPiNo.DataValueField = "TRN_DATA";
            DDLPiNo.DataSource = data;
            DDLPiNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPINo_Count(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PI Number loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetPINo(string Text, int startoffset)
    {
        try
        {
            DataTable data = null;
            if (ddlTrnType.SelectedValue == "IYS26")
            {
                string CommandText = "SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_CODE || '@'|| (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETSEWINGTHREADCUSTREQ_SALE OD,YRN_MST Y,  YRN_ASSOCATED_MST YA   WHERE   Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE AND  OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.BUSINESS_TYPE ='SW' and OD.ORDER_TYPE='PRODUCTION'   AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC)";//AND A.CR_ST_ORDER_NO = OD.CUSTNO    AND A.PI_NO = B.PI_NO from OD_CAPT_CUST_ADJ A,
                string whereClause = string.Empty;
                if (startoffset != 0)
                {
                    whereClause += " AND TRN_DATA NOT IN   ( SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_CODE || '@'|| (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETSEWINGTHREADCUSTREQ_SALE OD,YRN_MST Y,  YRN_ASSOCATED_MST YA   WHERE  Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.ARTICLE_NO = YA.ASS_YARN_CODE   AND OD.BUSINESS_TYPE ='SW' and OD.ORDER_TYPE='PRODUCTION'  AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "', 'SELF')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC)  WHERE ROWNUM < '" + startoffset + "') ";
                }
                string SortExpression = " ORDER BY TRN_DATA ASC";
                string SearchQuery = Text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
            }

            else if (ddlTrnType.SelectedValue == "IYS29")
            {
                string CommandText = "SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_FAMILY_CODE || '@'||  (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETCUSTREQ_DISPATCH_STOCK OD,YRN_MST Y,  YRN_ASSOCATED_MST YA    WHERE   Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE AND  OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.BUSINESS_TYPE ='SW' and OD.ORDER_TYPE='FROM_STOCK'    AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC) WHERE ROWNUM < 15";//AND A.CR_ST_ORDER_NO = OD.CUSTNO    AND A.PI_NO = B.PI_NO from OD_CAPT_CUST_ADJ A,
                string whereClause = string.Empty;
                if (startoffset != 0)
                {
                    whereClause += " AND TRN_DATA NOT IN   ( SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_FAMILY_CODE || '@'|| (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETCUSTREQ_DISPATCH_STOCK OD,YRN_MST Y,  YRN_ASSOCATED_MST YA,  WHERE  Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.ARTICLE_NO = YA.ASS_YARN_CODE   AND OD.BUSINESS_TYPE ='SW' and OD.ORDER_TYPE='FROM_STOCK'   AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "', 'SELF')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC)  WHERE ROWNUM < '" + startoffset + "') ";
                }
                string SortExpression = " ORDER BY TRN_DATA ASC";
                string SearchQuery = Text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
            }

            else
            {
                string CommandText = "SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_CODE || '@'|| (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETSEWINGTHREADCUSTREQ_SALE OD,YRN_MST Y,  YRN_ASSOCATED_MST YA,    WHERE   Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE AND  OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.BUSINESS_TYPE ='JW'     AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC) WHERE ROWNUM < 15";//AND A.CR_ST_ORDER_NO = OD.CUSTNO    AND A.PI_NO = B.PI_NO from OD_CAPT_CUST_ADJ A,
                string whereClause = string.Empty;
                if (startoffset != 0)
                {
                    whereClause += " AND TRN_DATA NOT IN   ( SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_CODE || '@'|| (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETSEWINGTHREADCUSTREQ_SALE OD,YRN_MST Y,  YRN_ASSOCATED_MST YA,  WHERE  Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.ARTICLE_NO = YA.ASS_YARN_CODE   AND OD.BUSINESS_TYPE ='JW'   AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC)  WHERE ROWNUM < '" + startoffset + "') ";
                }
                string SortExpression = " ORDER BY TRN_DATA ASC";
                string SearchQuery = Text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
            }
            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private int GetPINo_Count(string Text)
    {
        try
        {
            DataTable data = null;
            if (ddlTrnType.SelectedValue == "IYS26")
            {
                string CommandText = " SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_CODE || '@'|| (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETSEWINGTHREADCUSTREQ_SALE OD,YRN_MST Y,  YRN_ASSOCATED_MST YA,  WHERE   Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE AND OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.BUSINESS_TYPE ='SW'    AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC)  ";
                string whereClause = string.Empty;
                string SortExpression = " ORDER BY TRN_DATA ASC";
                string SearchQuery = Text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            }
            else if (ddlTrnType.SelectedValue == "IYS29")
            {
                string CommandText = " SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_FAMILY_CODE || '@'|| (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETCUSTREQ_DISPATCH_STOCK OD,YRN_MST Y,  YRN_ASSOCATED_MST YA,   WHERE   Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE AND OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.BUSINESS_TYPE ='SW'   AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC)  ";
                string whereClause = string.Empty;
                string SortExpression = " ORDER BY TRN_DATA ASC";
                string SearchQuery = Text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            }
            else
            {
                string CommandText = " SELECT * FROM (SELECT DISTINCT * FROM (SELECT   OD.CUSTNO,   Y.YARN_CODE ARTICLE_NO,OD.yarn_desc ARTICLE_DESC, OD.SHADE_FAMILY_CODE, OD.SHADE_CODE,   (od.prty_code || ', ' || od.prty_name) prty_data,od.APP_NO_OF_UNIT,NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0) AS REM_INVOICE_NO_OF_UNIT,(   OD.CUSTNO|| '@'|| Y.YARN_CODE  || '@'|| OD.yarn_desc || '@'|| DECODE (NVL (y.uom, ''), '', 'KGS', y.uom) || '@'  || od.TRANS_PRICE || '@'|| OD.SHADE_CODE || '@'|| OD.SHADE_CODE || '@'|| (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0))|| '@'|| OD.SALE_PRICE || '@'|| OD.FINAL_RATE) TRN_DATA      FROM   VW_GETSEWINGTHREADCUSTREQ_SALE OD,YRN_MST Y,  YRN_ASSOCATED_MST YA,  WHERE   Y.YARN_CODE=YA.YARN_CODE AND (NVL (OD.APP_NO_OF_UNIT, 0) - NVL (OD.INVOICE_NO_OF_UNIT, 0)) > 0      AND OD.ARTICLE_NO = YA.ASS_YARN_CODE AND OD.ARTICLE_NO = YA.ASS_YARN_CODE  AND OD.BUSINESS_TYPE ='JW'   AND OD.PRTY_CODE IN  ('" + lblPartyCode.Text + "')) asd WHERE ARTICLE_NO LIKE :SearchQuery OR ARTICLE_DESC LIKE :SearchQuery OR CUSTNO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery ORDER BY TRN_DATA ASC)  ";
                string whereClause = string.Empty;
                string SortExpression = " ORDER BY TRN_DATA ASC";
                string SearchQuery = Text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            }

            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DDLPiNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = DDLPiNo.SelectedValue.Trim();
            string[] ss = cString.Split('@');

            string PA_NO = ss[0].ToString();
            string YARN_CODE = ss[1].ToString();
            txtPA_NO.Text = PA_NO;
            txtArticleCode.Text = YARN_CODE;
            txtDESC.Text = ss[2].ToString();
            txtUNIT.Text = ss[3].ToString();

            txtBasicRate.Text = ss[4].ToString();
            string SHADE_CODE = ss[5].ToString();
            string SHADE_FAMILY = ss[6].ToString();
            txtShade.Text = SHADE_CODE;
            txtShadeFamily.Text = SHADE_FAMILY;
            txtMaxQty.Text = ss[6].ToString();
            txtBasicRate.Text = ss[7].ToString();
            txtfinal.Text = ss[8].ToString();
            //if (!string.IsNullOrEmpty(ss[8].ToString()))
            //{
            //    if (!string.IsNullOrEmpty(txtBuyerorder.Text))
            //    {
            //        txtBuyerorder.Text = txtBuyerorder.Text + "," + ss[8].ToString();
            //    }
            //    else
            //    {
            //        txtBuyerorder.Text = ss[8].ToString();
            //    }
            //}
            int Year = oUserLoginDetail.DT_STARTDATE.Year;

            GetDataForDetail(PA_NO, YARN_CODE, SHADE_CODE, Year);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In PINO Selecting"));
            lblMode.Text = ex.ToString();
        }
    }
    private void GetDataForDetail(string PA_NO, string YARN_CODE, string SHADE_CODE, int Year)
    {
        try
        {

            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(YARN_CODE, UNIQUEID))
            {
                DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetTRNData_ForSaleOrder(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PA_NO, YARN_CODE, SHADE_CODE, oUserLoginDetail.DT_STARTDATE.Year);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtArticleCode.Text = dt.Rows[0]["YARN_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["YARN_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtMaxQty.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    txtBasicRate.Text = double.Parse(dt.Rows[0]["BASIC_RATE"].ToString().Trim()).ToString();
                    txtfinal.Text = double.Parse(dt.Rows[0]["FINAL_RATE"].ToString().Trim()).ToString();
                    txtAmount.Text = "0";
                    lblPO_COMP.Text = dt.Rows[0]["COMP_CODE"].ToString().Trim();

                    txtShade.Text = dt.Rows[0]["SHADE_CODE"].ToString().Trim();
                    txtQTY.Text = "0";
                    txtAmount.Text = "0";
                    btnAdjRec.Focus();

                    DataTable dtdistax = SaitexBL.Interface.Method.YRN_IR_MST.GetTRNDataSaleOrderDisTaxes(Year, lblPO_COMP.Text.Trim(), "SL0001", txtPA_NO.Text.Trim(), YARN_CODE, SHADE_CODE);
                    mapDataTableForSaleOrderDisTaxes(dtdistax);
                }
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Article With This Shade Already Included.Select another article/ shade");
            }
        }
        catch
        {
            throw;
        }
    }
    private void mapDataTableForSaleOrderDisTaxes(DataTable dt)
    {
        try
        {
            if (Session["dtDicRate"] == null)
            {
                CreateDataTableForSaleOrderDisTax();
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
                    drNew["COMP_CODE"] = lblPO_COMP.Text;
                    drNew["YEAR"] = int.Parse(dr["YEAR"].ToString());
                    drNew["BRANCH_CODE"] = "SL0001";
                    drNew["PO_TYPE"] = "MII";
                    drNew["PA_NO"] = txtPA_NO.Text;
                    drNew["YARN_CODE"] = dr["YARN_CODE"];
                    drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                    drNew["Rate"] = dr["RATE"];
                    drNew["COMPO_SL"] = dr["COMPO_SL"];
                    drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                    drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                    drNew["IS_PO"] = "1";

                    drNew["SHADE_CODE"] = dr["SHADE_CODE"];
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
            txtfinal.Text = dFinalRate.ToString();
            txtfinal.Text = dFinalRate.ToString();
            Session["dtDicRate"] = dtRate1;
        }
        catch
        {
            throw;
        }
    }
    private void CreateDataTableForSaleOrderDisTax()
    {
        try
        {
            DataTable dtRate1 = new DataTable();
            dtRate1.Columns.Add("Uniqueid", typeof(int));

            dtRate1.Columns.Add("COMP_CODE", typeof(string));
            dtRate1.Columns.Add("BRANCH_CODE", typeof(string));
            dtRate1.Columns.Add("PO_TYPE", typeof(string));
            dtRate1.Columns.Add("PA_NO", typeof(string));
            dtRate1.Columns.Add("YEAR", typeof(int));
            dtRate1.Columns.Add("YARN_CODE", typeof(string));
            dtRate1.Columns.Add("COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("Rate", typeof(double));
            dtRate1.Columns.Add("COMPO_SL", typeof(int));
            dtRate1.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate1.Columns.Add("Amount", typeof(double));
            dtRate1.Columns.Add("BASE_COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("IS_PO", typeof(string));
            dtRate1.Columns.Add("SHADE_CODE", typeof(string));

            Session["dtDicRate"] = dtRate1;
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
                DDLPiNo.Enabled = false;

                txtPA_NO.Text = dv[0]["PI_NO"].ToString();
                txtArticleCode.Text = dv[0]["YARN_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();


                txtQtyUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtQtyUom.Text = dv[0]["UOM_OF_UNIT"].ToString();
                txtQtyWeight.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();

                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtfinal.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();
                txtShadeFamily.Text = dv[0]["SHADE_FAMILY"].ToString();
                txtGrossWt.Value = dv[0]["GROSS_WT"].ToString();
                //lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                //lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                //lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                hdnLotNo.Value = dv[0]["LOT_NO"].ToString();
                hdnGrade.Value = dv[0]["GRADE"].ToString();
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
            if (txtArticleCode.Text != "" && !string.IsNullOrEmpty(ddlStore.SelectedValue))
            {
                //*************** Change Code By Arun Sharma *************25_05_2018****//
                if (ddlTrnType.SelectedValue == "IYS26")
                {
                    string URL = "CR_Receipt_Adjustment.aspx";
                    URL = URL + "?ItemCodeId=" + txtArticleCode.Text.Trim();
                    URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                    URL = URL + "&txtQtyUnit=" + txtQtyUnit.ClientID;
                    URL = URL + "&txtQtyUom=" + txtQtyUom.ClientID;
                    URL = URL + "&txtQtyWeight=" + txtQtyWeight.ClientID;
                    URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
                    URL = URL + "&AmountId=" + txtAmount.ClientID;
                    URL = URL + "&MAX_QTY=" + txtMaxQty.Text;
                    URL = URL + "&SHADE_CODE=" + txtShade.Text;
                    URL = URL + "&SHADE_FAMILY=" + txtShadeFamily.Text;
                    URL = URL + "&TRN_TYPE=" + ddlTrnType.SelectedValue;
                    URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
                    URL = URL + "&UOM_OF_UNIT=" + txtUNIT.Text.Trim();
                    URL = URL + "&PI_NO=" + txtPA_NO.Text.Trim();
                    URL = URL + "&STORE=" + ddlStore.SelectedValue;
                    URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
                    URL = URL + "&GROSS_WT=" + txtGrossWt.ClientID;
                    URL = URL + "&TARE_WT=" + txtTareWt.ClientID;
                    URL = URL + "&CARTONS=" + txtCartons.ClientID;
                    URL = URL + "&hdnLOT_NO=" + hdnLotNo.ClientID;
                    URL = URL + "&hdnGRADE=" + hdnGrade.ClientID;
                    URL = URL + "&PARTY_CODE=" + "SELF";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }

                else if (ddlTrnType.SelectedValue == "IYS29")
                {
                    string URL = "CR_Receipt_Adjustment.aspx";
                    URL = URL + "?ItemCodeId=" + txtArticleCode.Text.Trim();
                    URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                    URL = URL + "&txtQtyUnit=" + txtQtyUnit.ClientID;
                    URL = URL + "&txtQtyUom=" + txtQtyUom.ClientID;
                    URL = URL + "&txtQtyWeight=" + txtQtyWeight.ClientID;
                    URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
                    URL = URL + "&AmountId=" + txtAmount.ClientID;
                    URL = URL + "&MAX_QTY=" + txtMaxQty.Text;
                    URL = URL + "&SHADE_CODE=" + txtShade.Text;
                    URL = URL + "&SHADE_FAMILY=" + txtShadeFamily.Text;
                    URL = URL + "&TRN_TYPE=" + ddlTrnType.SelectedValue;
                    URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
                    URL = URL + "&UOM_OF_UNIT=" + txtUNIT.Text.Trim();
                    URL = URL + "&PI_NO=" + txtPA_NO.Text.Trim();
                    URL = URL + "&STORE=" + ddlStore.SelectedValue;
                    URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
                    URL = URL + "&GROSS_WT=" + txtGrossWt.ClientID;
                    URL = URL + "&TARE_WT=" + txtTareWt.ClientID;
                    URL = URL + "&CARTONS=" + txtCartons.ClientID;
                    URL = URL + "&hdnLOT_NO=" + hdnLotNo.ClientID;
                    URL = URL + "&hdnGRADE=" + hdnGrade.ClientID;
                    URL = URL + "&PARTY_CODE=" + "SELF";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }

                else
                {
                    string URL = "CR_Receipt_Adjustment.aspx";
                    URL = URL + "?ItemCodeId=" + txtArticleCode.Text.Trim();
                    URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                    URL = URL + "&txtQtyUnit=" + txtQtyUnit.ClientID;
                    URL = URL + "&txtQtyUom=" + txtQtyUom.ClientID;
                    URL = URL + "&txtQtyWeight=" + txtQtyWeight.ClientID;
                    URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
                    URL = URL + "&AmountId=" + txtAmount.ClientID;
                    URL = URL + "&MAX_QTY=" + txtMaxQty.Text;
                    URL = URL + "&SHADE_CODE=" + txtShade.Text;
                    URL = URL + "&SHADE_FAMILY=" + txtShadeFamily.Text;
                    URL = URL + "&TRN_TYPE=" + ddlTrnType.SelectedValue;
                    URL = URL + "&ChallanNo=" + txtChallanNumber.Text.Trim();
                    URL = URL + "&UOM_OF_UNIT=" + txtUNIT.Text.Trim();
                    URL = URL + "&PI_NO=" + txtPA_NO.Text.Trim();
                    URL = URL + "&STORE=" + ddlStore.SelectedValue;
                    URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
                    URL = URL + "&GROSS_WT=" + txtGrossWt.ClientID;
                    URL = URL + "&TARE_WT=" + txtTareWt.ClientID;
                    URL = URL + "&CARTONS=" + txtCartons.ClientID;
                    URL = URL + "&hdnLOT_NO=" + hdnLotNo.ClientID;
                    URL = URL + "&hdnGRADE=" + hdnGrade.ClientID;
                    URL = URL + "&PARTY_CODE=" + lblPartyCode.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }
                txtQTY.ReadOnly = false;
                txtQtyUnit.ReadOnly = false;
                txtQtyUom.ReadOnly = false;
                txtQtyWeight.ReadOnly = false;
                //txtAmount.ReadOnly = false;
                //txtBasicRate.ReadOnly = false;
            }
            else
            {
                CommonFuction.ShowMessage("Please select Yarn Code/Store to adjust Receipt.");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Adjust Reveived Button.."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            CalculateAmount();
            txtQTY.ReadOnly = true;
            //  txtBasicRate.ReadOnly = false;
            txtBasicRate.ReadOnly = false;
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
            e.ItemsCount = GetIssueDataCount(e.Text.ToUpper());

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
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_YARN_IR_MST a WHERE NVL (A.CONF_FLAG, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + ddlTrnType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND (TRN_NUMB, TRN_DATE, PRTY_CODE, PRTY_NAME, DEPT_NAME) NOT IN(SELECT TRN_NUMB,TRN_DATE,PRTY_CODE,PRTY_NAME,DEPT_NAME FROM (SELECT * FROM (SELECT a.TRN_NUMB,a.TRN_DATE,a.PRTY_CODE,a.PRTY_NAME,a.DEPT_NAME FROM V_YARN_IR_MST a WHERE NVL (A.CONF_FLAG, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + ddlTrnType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) WHERE ROWNUM <= '" + startOffset + "') ";
            }

            string SortExpression = " ORDER BY TRN_NUMB DESC, TRN_DATE DESC";
            string SearchQuery = text + "%";

            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
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
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, a.PRTY_NAME, a.DEPT_NAME FROM V_YARN_IR_MST a WHERE NVL (A.CONF_FLAG, '0') = 0 AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + ddlTrnType.SelectedValue + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY a.TRN_NUMB DESC, a.TRN_DATE DESC) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery) ";
            string whereClause = string.Empty;

            string SortExpression = " ";
            string SearchQuery = text + "%";

            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
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
            ddlTRNNumber.Items.Clear();
            txtChallanNumber.Visible = true;
            ddlTRNNumber.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    protected void txtFinalRate_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btnOther_Click(object sender, EventArgs e)
    {
        try
        {
            trOther.Visible = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Opening Other Detail POPUp..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btncncelpack_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                trOther.Visible = false;
                // mpepacking.Show();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel Button..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtTransporterCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyDataTran(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtTransporterCode.Items.Clear();
                txtTransporterCode.DataSource = data;
                txtTransporterCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCountTran(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transporters Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblTransporterCode.Text = txtTransporterCode.SelectedText.ToString().Trim();
            txtTransporterAddress.Text = txtTransporterCode.SelectedValue;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transporter Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtConsigneeCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData1(e.Text.ToUpper(), e.ItemsOffset);

            txtConsigneeCode.Items.Clear();

            txtConsigneeCode.DataSource = data;
            txtConsigneeCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtConsigneeCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            lblConsigneeCode.Text = txtConsigneeCode.SelectedText.ToString().Trim();
            txtConsigneeAddress.Text = txtConsigneeCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData1(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME ) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('CONSIGNEE') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)= upper('CONSIGNEE') and ROWNUM <= " + startOffset + ")";
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

        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('CONSIGNEE') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    private DataTable GetLOVForParty(string Text)
    {
        try
        {
            string CommandText = "  SELECT DISTINCT *  FROM (SELECT *  FROM (SELECT DISTINCT     v.PRTY_CODE,     SO_TYPE,     PRTY_STATE,     PRTY_ADD1,     PRTY_ADD2,     PRTY_NAME,        PRTY_ADD1     || ',  '     || NVL (PRTY_ADD2, ' ')     || ',  '     || NVL (PRTY_STATE, ' ')        address                  FROM    TX_VENDOR_MST v     RIGHT OUTER JOIN        YRN_SO_MST p     ON V.PRTY_CODE = P.PRTY_CODE)      WHERE SO_TYPE  IN ('SSM','SSS') )";
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

    private DataTable GetPartyDataTran(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME ) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE VENDOR_CAT_CODE = 'TRANSPORTER & LOGISTICS' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME ) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE VENDOR_CAT_CODE = 'TRANSPORTER & LOGISTICS' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY PRTY_CODE ASC";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCountTran(string text)
    {
        string CommandText = " SELECT * FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_GRP_CODE = 'Transporter' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY PRTY_CODE ASC ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void GetSubTotal()
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
            double Sub_Total = 0;
            foreach (DataRow dr in dtDetailTBL.Rows)
            {
                Sub_Total += double.Parse(dr["AMOUNT"].ToString());
            }

            txtSubTotal.Text = Sub_Total.ToString();
            //txtFinalTotal.Text = Sub_Total.ToString();
        }
        catch
        {
            throw;
        }
    }

    protected void GrandTotal()
    {
        try
        {
            double BaseRate = 0;
            double freight = 0;
            double OtherCharge = 0;
            double CGSTrate = 0;
            double SGSTrate = 0;
            double IGSTrate = 0;
            double Net_Weight = 0.0;
            // double CSTrate = 0;
            //double exc_total_rate = 0;
            double exc_total = 0;
            // double Exc_Base_rate = 0;
            //  double Exc_Base_amt = 0;
            // double Exc_cess1_rate = 0;
            // double Exc_cess1_amt = 0;
            //double Exc_cess2_rate = 0;
            // double Exc_cess2_amt = 0;

            double.TryParse(txtSubTotal.Text, out BaseRate);

            double.TryParse(txtFreight.Text, out freight);

            double.TryParse(txtED.Text, out OtherCharge);
            double.TryParse(txtNetWeight.Text, out Net_Weight);

            //double.TryParse(txtExciseOnBaseRate.Text, out Exc_Base_rate);
            //Exc_Base_amt = (BaseRate * Exc_Base_rate) / 100;
            //txtExciseBaseAmt.Text = Exc_Base_amt.ToString();

            //double.TryParse(txtExciseOnCESSRate1.Text, out Exc_cess1_rate);
            //Exc_cess1_amt = (Exc_Base_amt * Exc_cess1_rate) / 100;
            //txtExciseCESSAmt1.Text = Exc_cess1_amt.ToString();

            //double.TryParse(txtExciseOnCESSRate2.Text, out Exc_cess2_rate);
            //Exc_cess2_amt = (Exc_Base_amt * Exc_cess2_rate) / 100;
            //txtExciseCESSAmt2.Text = Exc_cess2_amt.ToString();

            //exc_total = Exc_Base_amt + Exc_cess1_amt + Exc_cess2_amt;
            //txtExciseTotalAmt.Text = exc_total.ToString();

            //exc_total_rate = Exc_Base_rate + Exc_cess1_rate + Exc_cess2_rate;
            //txtExciseTotalRate.Text = exc_total_rate.ToString();

            //double AmountForCST = BaseRate + freight + ED + exc_total;
            //double.TryParse(txtSaleTAXRate.Text, out CSTrate);
            //double CSTAmount = (AmountForCST * CSTrate) / 100;
            //txtSaleTAXAmt.Text = CSTAmount.ToString();

            //double GrandTotal = BaseRate + freight + OtherCharge;

            //************Tax Code CGST , SGST and IGST open by Arun Sharma*****************************//

            double AmountForGST = BaseRate + (freight * Net_Weight) + (OtherCharge * Net_Weight) + exc_total;
            double.TryParse(txtSaleTAXRate.Text, out CGSTrate);
            double CGSTAmount = (AmountForGST * CGSTrate) / 100;
            txtSaleTAXAmt.Text = CGSTAmount.ToString();


            double.TryParse(txtSaleSGSTTAXRate.Text, out SGSTrate);
            double SGSTAmount = (AmountForGST * SGSTrate) / 100;
            txtSaleSGSTTAXAmt.Text = SGSTAmount.ToString();

            double.TryParse(txtSaleIGSTTAXRate.Text, out IGSTrate);
            double IGSTAmount = (AmountForGST * IGSTrate) / 100;
            txtSaleIGSTTAXAmt.Text = IGSTAmount.ToString();


            //************Tax Code CGST , SGST and IGST open by Arun Sharma*****************************//

            double GrandTotal = AmountForGST + CGSTAmount + SGSTAmount + IGSTAmount;


            txtFinalTotal.Text = GrandTotal.ToString();
        }
        catch
        {
            throw;
        }
    }

    protected void txtExciseOnBaseRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GrandTotal();
            // mpepacking.Show();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void txtED_TextChanged(object sender, EventArgs e)
    {
        try
        {

            GrandTotal();

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void txtExciseOnCESSRate2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GrandTotal();
            //  mpepacking.Show();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void txtSaleTAXRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GrandTotal();
            // mpepacking.Show();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void txtSaleSGSTTAXRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GrandTotal();
            // mpepacking.Show();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void txtSaleIGSTTAXRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GrandTotal();
            // mpepacking.Show();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void txtFreight_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GrandTotal();

        }
        catch (Exception ex)
        {
            throw;

        }

    }

    protected void txtInsurance_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GrandTotal();
            //    mpepacking.Show();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Delivery branch selection.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlDelAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //BindDeliveryAddByCode(ddlDelAdd.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Delivery branch selection.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnDisc_Click(object sender, EventArgs e)
    {

        try
        {

            string URL = "~/../../../SalesWork/Pages/SALEDisAdj.aspx";
            URL = URL + "?FinalAmount=" + txtBasicRate.Text.Trim();
            URL = URL + "&TextBoxId=" + txtfinal.ClientID;
            URL = URL + "&COMP_CODE=" + "C00001";
            URL = URL + "&BRANCH_CODE=" + "SL0001";
            URL = URL + "&PO_TYPE=" + "MII";
            URL = URL + "&PA_NO=" + txtPA_NO.Text;
            URL = URL + "&YARN_CODE=" + txtArticleCode.Text.Trim();
            URL = URL + "&SHADE_CODE=" + txtShade.Text.Trim();
            txtBasicRate.ReadOnly = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting discount/ taxes adjustment for transaction."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtfinal_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateAmount();
            txtQTY.ReadOnly = true;
            // txtBasicRate.ReadOnly = false;
            txtBasicRate.ReadOnly = false;
            txtAmount.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Final Rate Text Changed event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtBasicRate_TextChanged(object sender, EventArgs e)
    {

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

    protected void CmBParty_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string[] xyz = (CmBParty.SelectedValue.Split('@'));


            txtPartyAddress.Text = xyz[3].ToString();
            lblPartyCode.Text = CmBParty.SelectedText.Trim();
            if (xyz[3].ToString().ToUpper() == "GUJARAT")
            {
                txtSaleTAXRate.Text = "6";
                txtSaleSGSTTAXRate.Text = "6";

            }
            else
            {
                txtSaleIGSTTAXRate.Text = "12";
            }
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
            if (ddlTrnType.SelectedValue == "IYS26")
            {
                string CommandText = "SELECT  distinct  PRTY_CODE, PRTY_NAME, (PRTY_NAME) Address, PRTY_GRP_CODE,XYZ  FROM   (  select * from  (SELECT   TX_VENDOR_MST.PRTY_CODE,  TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_GRP_CODE,   (   TX_VENDOR_MST.PRTY_ADD1     || '@'      || TX_VENDOR_MST.PRTY_GRP_CODE  || '@'   || VW.PRTY_STATE  || '@'   || VW.PRTY_NAME)    AS xyz   FROM   TX_VENDOR_MST, V_OD_CUSTOMER_REQUEST_ST1 vw                         WHERE       vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE  AND vw.CONF_FLAG = 1  AND vw.BUSINESS_TYPE='SW' and vw.ORDER_TYPE='PRODUCTION' AND (NVL (vw.QTY_APPROVED, 0) - NVL (vw.INVOICE_QTY, 0) > 0))msd  WHERE   msd.PRTY_CODE LIKE :SearchQuery OR msd.PRTY_NAME LIKE :SearchQuery  ORDER BY   msd.PRTY_CODE ASC)WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48') AND ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT  distinct  PRTY_CODE    FROM  ( select * from  (SELECT   TX_VENDOR_MST.PRTY_CODE,  TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_GRP_CODE,   (   TX_VENDOR_MST.PRTY_ADD1     || '@'      || TX_VENDOR_MST.PRTY_GRP_CODE  || '@'   || VW.PRTY_STATE  || '@'   || VW.PRTY_NAME)    AS xyz   FROM   TX_VENDOR_MST, V_OD_CUSTOMER_REQUEST_ST1 vw                         WHERE       vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE  AND vw.CONF_FLAG = 1 AND vw.BUSINESS_TYPE='SW' and vw.ORDER_TYPE='PRODUCTION'  AND (NVL (vw.QTY_APPROVED, 0) - NVL (vw.INVOICE_QTY, 0) > 0)) msd   WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48')       AND ROWNUM <= " + startOffset + ")";
                }
                string SortExpression = " order by PRTY_CODE";
                string SearchQuery = "%" + Text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            }
            else if (ddlTrnType.SelectedValue == "IYS29")
            {
                string CommandText = "SELECT  distinct  PRTY_CODE, PRTY_NAME, (PRTY_NAME) Address, PRTY_GRP_CODE,XYZ  FROM   (  select * from  (SELECT    TX_VENDOR_MST.PRTY_CODE, TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_GRP_CODE,   (   TX_VENDOR_MST.PRTY_ADD1   || '@'  || TX_VENDOR_MST.PRTY_GRP_CODE   || '@'    || VW.PRTY_STATE    || '@' || VW.PRTY_NAME)   AS xyz  FROM   TX_VENDOR_MST,V_OD_CUSTOMER_REQUEST_ST1 vw  WHERE  vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE  AND    vw.CONF_FLAG = 1  AND VW.ORDER_TYPE='FROM_STOCK' AND    (nvl(vw.QTY_APPROVED,0)-nvl(vw.INVOICE_QTY,0) >0))msd  WHERE   msd.PRTY_CODE LIKE :SearchQuery OR msd.PRTY_NAME LIKE :SearchQuery  ORDER BY   msd.PRTY_CODE ASC)WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48') AND ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT  distinct  PRTY_CODE    FROM  (SELECT   TX_VENDOR_MST.PRTY_CODE, TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_GRP_CODE,   (   TX_VENDOR_MST.PRTY_ADD1   || '@'  || TX_VENDOR_MST.PRTY_GRP_CODE   || '@'    || VW.PRTY_STATE    || '@' || VW.PRTY_NAME)   AS xyz    FROM   TX_VENDOR_MST, V_OD_CUSTOMER_REQUEST_ST1 vw      WHERE       vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE AND vw.CONF_FLAG = 1  AND VW.ORDER_TYPE='FROM_STOCK' AND (NVL (vw.QTY_APPROVED, 0)  - NVL (vw.INVOICE_QTY, 0) > 0)) msd   WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48')       AND ROWNUM <= " + startOffset + ")";
                }
                string SortExpression = " order by PRTY_CODE";
                string SearchQuery = "%" + Text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            }
            else
            {
                string CommandText = "SELECT  distinct  PRTY_CODE, PRTY_NAME, (PRTY_NAME) Address, PRTY_GRP_CODE,XYZ  FROM   (  select * from  (SELECT   TX_VENDOR_MST.PRTY_CODE, TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_GRP_CODE,   (   TX_VENDOR_MST.PRTY_ADD1   || '@'  || TX_VENDOR_MST.PRTY_GRP_CODE   || '@'    || VW.PRTY_STATE    || '@' || VW.PRTY_NAME)   AS xyz    FROM   TX_VENDOR_MST,V_OD_CUSTOMER_REQUEST_ST1 vw  WHERE  vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE  AND    vw.CONF_FLAG = 1  AND vw.BUSINESS_TYPE='JW' and vw.ORDER_TYPE='PRODUCTION' AND    (nvl(vw.QTY_APPROVED,0)-nvl(vw.INVOICE_QTY,0) >0))msd  WHERE   msd.PRTY_CODE LIKE :SearchQuery OR msd.PRTY_NAME LIKE :SearchQuery  ORDER BY   msd.PRTY_CODE ASC)WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48') AND ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT  distinct  PRTY_CODE    FROM  (SELECT   TX_VENDOR_MST.PRTY_CODE, TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_GRP_CODE,   (   TX_VENDOR_MST.PRTY_ADD1   || '@'  || TX_VENDOR_MST.PRTY_GRP_CODE   || '@'    || VW.PRTY_STATE    || '@' || VW.PRTY_NAME)   AS xyz        FROM   TX_VENDOR_MST, V_OD_CUSTOMER_REQUEST_ST1 vw      WHERE       vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE AND vw.CONF_FLAG = 1 AND vw.BUSINESS_TYPE='JW' and vw.ORDER_TYPE='PRODUCTION' AND (NVL (vw.QTY_APPROVED, 0)  - NVL (vw.INVOICE_QTY, 0) > 0)) msd   WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48')       AND ROWNUM <= " + startOffset + ")";
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
        if (ddlTrnType.SelectedValue == "IYS26")
        {
            string CommandText = " SELECT  distinct  PRTY_CODE, PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address  FROM   (SELECT   TX_VENDOR_MST.PRTY_CODE, TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_ADD1, TX_VENDOR_MST.PRTY_GRP_CODE  FROM   TX_VENDOR_MST, V_OD_CUSTOMER_REQUEST_ST1 vw WHERE vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE AND vw.CONF_FLAG = 1 AND (NVL (vw.QTY_APPROVED, 0)    - NVL (vw.INVOICE_QTY, 0) > 0)) msd WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48')";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        else if (ddlTrnType.SelectedValue == "IYS29")
        {
            string CommandText = " SELECT  distinct  PRTY_CODE, PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address  FROM   (SELECT   TX_VENDOR_MST.PRTY_CODE, TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_ADD1, TX_VENDOR_MST.PRTY_GRP_CODE  FROM   TX_VENDOR_MST, V_OD_CUSTOMER_REQUEST_ST1 vw WHERE vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE AND vw.CONF_FLAG = 1  AND VW.ORDER_TYPE='FROM_STOCK' AND (NVL (vw.QTY_APPROVED, 0)    - NVL (vw.INVOICE_QTY, 0) > 0)) msd WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48')";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }

        else
        {
            string CommandText = " SELECT  distinct  PRTY_CODE, PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address  FROM   (SELECT   TX_VENDOR_MST.PRTY_CODE, TX_VENDOR_MST.PRTY_NAME, TX_VENDOR_MST.PRTY_ADD1, TX_VENDOR_MST.PRTY_GRP_CODE  FROM   TX_VENDOR_MST, V_OD_CUSTOMER_REQUEST_ST1 vw WHERE vw.PRTY_CODE = TX_VENDOR_MST.PRTY_CODE AND vw.CONF_FLAG = 1 AND (NVL (vw.QTY_APPROVED, 0)    - NVL (vw.INVOICE_QTY, 0) > 0)) msd WHERE      UPPER (PRTY_GRP_CODE) = UPPER ('18') OR UPPER (PRTY_GRP_CODE) = UPPER ('47') OR UPPER (PRTY_GRP_CODE) = UPPER ('48')";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        return data.Rows.Count;
    }

    protected void grdMaterialItemIssue_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        double NetWeight = 0.0;

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                GridView grdRecAdj = (GridView)e.Row.FindControl("grdRecAdj");

                int UNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.ToString());

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
                    string sYARN_CODE = dv[0]["YARN_CODE"].ToString();
                    string sSHADE_CODE = dv[0]["SHADE_CODE"].ToString();
                    string sISS_PI_NO = dv[0]["PI_NO"].ToString();
                    if (Session["dtItemReceipt"] != null)
                    {
                        DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
                        DataView dvsub = new DataView(dtItemReceipt);
                        dvsub.RowFilter = " YARN_CODE='" + sYARN_CODE + "' and SHADE_CODE='" + sSHADE_CODE + "' and ISS_PI_NO='" + sISS_PI_NO + "'";
                        if (dvsub.Count > 0)
                        {
                            grdRecAdj.DataSource = dvsub;
                            grdRecAdj.DataBind();
                        }
                    }
                }

                LinkButton txtQTY = (LinkButton)e.Row.FindControl("txtQTY");

                NetWeight = NetWeight + double.Parse(txtQTY.Text);
                txtNetWeight.Text = NetWeight.ToString();

            }
        }
        catch (Exception ex)
        {

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
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue("GODOWN"));
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

    protected void ddlTrnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNewChallanNum();
    }
}
