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

public partial class Module_Appaerals_Pages_CR_Receipt_Adjustment : System.Web.UI.Page
{
    private string TextBoxId;
    private int TRN_NUMB;
    private double FinalRate;
    private string txtBasicRate;
    private string AmountId;
    public string CombineId;
    public string TRN_TPYE;
    private string txtQtyUnit;
    private string txtQtyUom;
    private string txtQtyWeight;
    private string LOCATION;
    private string STORE;
    private string PARTY_CODE;
    private string GROSS_WT_clint_id;
    private string TARE_WT_clint_id;
    private string CARTONS;
    private string hdnLOT_NO;
    private string hdnGRADE;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["LOCATION"] != null)
            {
                LOCATION = Request.QueryString["LOCATION"].ToString();
            }
            if (Request.QueryString["STORE"] != null)
            {
                STORE = Request.QueryString["STORE"].ToString();
            }
            if (Request.QueryString["STORE"] != null)
            {
                GROSS_WT_clint_id = Request.QueryString["GROSS_WT"].ToString();
            }
            if (Request.QueryString["STORE"] != null)
            {
                TARE_WT_clint_id = Request.QueryString["TARE_WT"].ToString();
            }
            if (Request.QueryString["STORE"] != null)
            {
                CARTONS = Request.QueryString["CARTONS"].ToString();
            }
            if (Request.QueryString["hdnLOT_NO"] != null)
            {
                hdnLOT_NO = Request.QueryString["hdnLOT_NO"].ToString();
            }
            if (Request.QueryString["hdnGRADE"] != null)
            {
                hdnGRADE = Request.QueryString["hdnGRADE"].ToString();
            }
            if (!IsPostBack)
            {
                TextBoxId = string.Empty;
                ViewState["TextBoxId"] = TextBoxId;
                TRN_NUMB = 0;
                FinalRate = 0;
                ViewState["FinalRate"] = FinalRate;
                ViewState["TRN_NUMB"] = TRN_NUMB;

                if (Request.QueryString["TRN_TYPE"] != null)
                {
                    TRN_TPYE = Request.QueryString["TRN_TYPE"].ToString();
                    ViewState["TRN_TPYE"] = TRN_TPYE;
                }
                string PA_NO = string.Empty;
                if (Request.QueryString["PI_NO"] != null)
                {
                    PA_NO = Request.QueryString["PI_NO"].ToString();
                }
                lblPaHeding.Text = "For Pa No: ";
                lblPANO.Text = PA_NO;
                if (Request.QueryString["ChallanNo"] != null)
                {

                    TRN_NUMB = int.Parse(Request.QueryString["ChallanNo"].ToString());
                    ViewState["TRN_NUMB"] = TRN_NUMB;
                }
                if (Request.QueryString["AmountId"] != null)
                {
                    AmountId = Request.QueryString["AmountId"].ToString();
                    ViewState["AmountId"] = AmountId;
                }
                if (Request.QueryString["txtBasicRate"] != null)
                {

                    txtBasicRate = Request.QueryString["txtBasicRate"].ToString();
                    ViewState["txtBasicRate"] = txtBasicRate;
                }

                if (Request.QueryString["txtQtyUnit"] != null)
                {
                    txtQtyUnit = Request.QueryString["txtQtyUnit"].ToString();
                    ViewState["txtQtyUnit"] = txtQtyUnit;
                }
                if (Request.QueryString["txtQtyUom"] != null)
                {
                    txtQtyUom = Request.QueryString["txtQtyUom"].ToString();
                    ViewState["txtQtyUom"] = txtQtyUom;

                }
                if (Request.QueryString["txtQtyWeight"] != null)
                {
                    txtQtyWeight = Request.QueryString["txtQtyWeight"].ToString();
                    ViewState["txtQtyWeight"] = txtQtyWeight;

                }

                if (Request.QueryString["UOM_OF_UNIT"] != null)
                {
                    string UOM_OF_UNIT = Request.QueryString["UOM_OF_UNIT"].ToString();
                    ViewState["UOM_OF_UNIT"] = UOM_OF_UNIT;
                }
                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    string SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
                    lblAdjustItemReceiptShade.Text = SHADE_CODE;
                }

                if (Request.QueryString["SHADE_FAMILY"] != null)
                {
                    string SHADE_FAMILY = Request.QueryString["SHADE_FAMILY"].ToString();
                    lblShadeFamily.Text = SHADE_FAMILY;
                }

                if (Request.QueryString["PARTY_CODE"] != null)
                {
                    PARTY_CODE = Request.QueryString["PARTY_CODE"].ToString();
                }
                if (Request.QueryString["ItemCodeId"] != null)
                {
                    string ItemCode = Request.QueryString["ItemCodeId"].ToString();
                    GetDataForItemAdjustment(ItemCode);
                }
                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                    ViewState["TextBoxId"] = TextBoxId;
                }
                if (Request.QueryString["CombineId"] != null)
                {
                    CombineId = Request.QueryString["CombineId"].ToString();
                    ViewState["CombineId"] = CombineId;
                }
                if (Request.QueryString["MAX_QTY"] != null)
                {
                    lblMaxQty.Text = (string)Request.QueryString["MAX_QTY"].ToString();
                    lblMaxQty.Visible = true;
                    lblMaxQtyDisp.Visible = true;
                }
                else
                {
                    lblMaxQty.Text = "0";
                    lblMaxQty.Visible = false;
                    lblMaxQtyDisp.Visible = false;
                }



            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in Page Loading.\r\nsee error log for detail.");
        }
    }

    private void GetDataForItemAdjustment(string ItemCode)
    {
        try
        {
            if (ViewState["TRN_TPYE"] != null)
            {
                TRN_TPYE = (string)ViewState["TRN_TPYE"];
            }
            if (ViewState["TRN_NUMB"] != null)
            {
                TRN_NUMB = (int)ViewState["TRN_NUMB"];
            }
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.YRN_IR_ISS_ADJ oYRN_IR_ISS_ADJ = new SaitexDM.Common.DataModel.YRN_IR_ISS_ADJ();

            if (TRN_NUMB > 0)
            {
                oYRN_IR_ISS_ADJ.TRN_NUMB = TRN_NUMB;
            }
            oYRN_IR_ISS_ADJ.ITEM_CODE = ItemCode;
            oYRN_IR_ISS_ADJ.SHADE_CODE = lblAdjustItemReceiptShade.Text;
            oYRN_IR_ISS_ADJ.SHADE_FAMILY = lblShadeFamily.Text.Trim();
            oYRN_IR_ISS_ADJ.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_ISS_ADJ.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_ISS_ADJ.TRN_TYPE = TRN_TPYE;
            oYRN_IR_ISS_ADJ.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_ISS_ADJ.ISS_PI_NO = lblPANO.Text;
            oYRN_IR_ISS_ADJ.STORE = STORE;
            oYRN_IR_ISS_ADJ.LOCATION = LOCATION;
            oYRN_IR_ISS_ADJ.PARTY_CODE = PARTY_CODE;


            DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.GetDataForInvoiceIssueAdjustment(oYRN_IR_ISS_ADJ);

            grdReceiptAdjustment.DataSource = null;
            grdReceiptAdjustment.DataBind();
            lblAdjustItemReceiptCode.Text = ItemCode;
            //dtReceiptAdjustment.Columns.Add("IRADJUST_QTY", typeof(double));
            if (dtReceiptAdjustment != null && dtReceiptAdjustment.Rows.Count > 0)
            {
                if (TRN_NUMB <= 0)
                {
                    foreach (DataRow dr in dtReceiptAdjustment.Rows)
                    {
                        dr["IRADJUST_QTY"] = 0;
                    }
                }
                grdReceiptAdjustment.DataSource = dtReceiptAdjustment;
                grdReceiptAdjustment.DataBind();
                ViewState["dtReceiptAdjustment"] = dtReceiptAdjustment;
            }
            else
            {
                lblReceiptAdjustmentError.Text = "No Record exists for adjustment for provided Item";
            }
        }
        catch
        {
            throw;
        }
    }

    private double GetFinalTotalOfItemReceiptAdjustment(out double FinalTotalUnit)
    {
        try
        {
            double FinalTotal = 0;
            FinalTotalUnit = 0;
            for (int iLoop = 0; iLoop < grdReceiptAdjustment.Rows.Count; iLoop++)
            {
                TextBox txtAdjustedReceiptQTY = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTY");
                Label lblAdjustRemQty = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAdjustRemQty");
                double Total = 0;
                double.TryParse(txtAdjustedReceiptQTY.Text, out Total);
                txtAdjustedReceiptQTY.Text = Total.ToString();

                if (double.Parse(txtAdjustedReceiptQTY.Text) <= double.Parse(lblAdjustRemQty.Text))
                {
                    lblReceiptAdjustmentError.Text = string.Empty;
                    FinalTotal = FinalTotal + Total;
                }
                else
                {
                    lblReceiptAdjustmentError.Text = "Entered quantity is larger then Receipt quantity";
                    txtAdjustedReceiptQTY.Text = 0.ToString();

                    break;
                }
            }
            return FinalTotal;
        }
        catch
        {
            throw;
        }
    }

    protected void btnAdjustReceiptItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["txtBasicRate"] != null)
            {
                txtBasicRate = (string)ViewState["txtBasicRate"];
            }
            if (ViewState["FinalRate"] != null)
            {
                FinalRate = (double)ViewState["FinalRate"];
            }
            if (ViewState["TextBoxId"] != null)
            {
                TextBoxId = (string)ViewState["TextBoxId"];
            }

            if (ViewState["AmountId"] != null)
            {
                AmountId = (string)ViewState["AmountId"];
            }
            if (ViewState["txtQtyUnit"] != null)
            {
                txtQtyUnit = (string)ViewState["txtQtyUnit"];
            }
            if (ViewState["txtQtyUom"] != null)
            {
                txtQtyUom = (string)ViewState["txtQtyUom"];
            }
            if (ViewState["txtQtyWeight"] != null)
            {
                txtQtyWeight = (string)ViewState["txtQtyWeight"];
            }

            double TotalQTY = 0;
            double TotalUnit = 0;
            double TotalWeight = 0;
            string UnitUOM = string.Empty;
            double TotalGrossWt=0;
            double TotalTareWT=0;
            DataTable dtItemReceipt = createdatatableforadjustment();
            Session["dtItemReceipt"] = dtItemReceipt;
            FinalRate = GetWaightedAvarageRate(out TotalQTY, out TotalUnit, out UnitUOM, out TotalWeight, out  TotalGrossWt,out  TotalTareWT);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "','" + txtBasicRate + "','" + FinalRate + "','" + AmountId + "','" + TotalQTY * FinalRate + "','" + txtQtyUnit + "','" + TotalUnit + "','" + txtQtyUom + "','" + UnitUOM + "','" + txtQtyWeight + "','" + TotalWeight + "','" + CARTONS + "','" + grdReceiptAdjustment.Rows.Count + "','" + GROSS_WT_clint_id + "','" + TotalGrossWt + "','" + TARE_WT_clint_id + "','" + TotalTareWT + "')", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValueNew('" + TotalQTY + "','" + TextBoxId + "','" + txtQtyUnit + "','" + TotalUnit + "','" + txtQtyUom + "','" + UnitUOM + "','" + txtQtyWeight + "','" + TotalWeight + "','" + CARTONS + "','" + grdReceiptAdjustment.Rows.Count + "','" + GROSS_WT_clint_id + "','" + TotalGrossWt + "','" + TARE_WT_clint_id + "','" + TotalTareWT + "','" + hdnLOT_NO + "','" + dtItemReceipt.Rows[0]["LOT_NO"].ToString() + "','" + hdnGRADE + "','" + dtItemReceipt.Rows[0]["GRADE"].ToString() + "')", true);
       
        
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in receipt adjustment.\r\nsee error log for detail.");
        }
    }

    private DataTable createdatatableforadjustment()
    {
        try
        {
            if (ViewState["TRN_NUMB"] != null)
            {
                TRN_NUMB = (int)ViewState["TRN_NUMB"];
            }
            DataTable dtReceiptAdjustment = (DataTable)ViewState["dtReceiptAdjustment"];

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];


            DataTable dtItemReceipt = new DataTable();
            if (Session["dtItemReceipt"] == null)
                dtItemReceipt = createAdjTable();
            else
            {
                dtItemReceipt.Rows.Clear();
                dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            }
            for (int iLoop = 0; iLoop < grdReceiptAdjustment.Rows.Count; iLoop++)
            {
                Label lblAdjustReceiptNuber = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAdjustReceiptNuber");
                string ReceiptNumber = lblAdjustReceiptNuber.Text.Trim();

                TextBox txtAdjustedReceiptQTY = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTY");
                double Adjustqty = double.Parse(txtAdjustedReceiptQTY.Text.Trim());
                TextBox txtAdjustedReceiptCheese = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptCheese");
                double AdjustCheese = double.Parse(txtAdjustedReceiptCheese.Text.Trim());


                string ItemCode = lblAdjustItemReceiptCode.Text.Trim();
                string ShadeCode = lblAdjustItemReceiptShade.Text;
                string ShadeFAMILY = lblShadeFamily.Text;

                Label lblAPPR_QTY = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
                double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());

                string Receipt_Type = lblAPPR_QTY.ToolTip.Trim();

                Label lblLotNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblLotNo");
                string lotno = lblLotNo.Text.Trim();

                Label txtAdjustedReceiptQTYUnit = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblNoOfUnit");
                TextBox txtAdjustedReceiptQTYUOM = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYUOM");
                TextBox txtAdjustedReceiptQTYWeight = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYWeight");
                double UNIT_NO = 0;
                double UnitWeight = 0;
                string UNITUOM = string.Empty;
                double.TryParse(txtAdjustedReceiptQTYUnit.Text, out UNIT_NO);
                UNITUOM = txtAdjustedReceiptQTYUOM.Text;
               // double.TryParse(txtAdjustedReceiptQTYWeight.Text, out UnitWeight);

                UnitWeight = Adjustqty / AdjustCheese;
                UnitWeight = Math.Round(UnitWeight, 4);

                Label grade = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblGrade");
                Label CartonNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblCartonNo");
                Label grossWt = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblGrossWt");
                Label piNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblPINo");

                string ISS_PI_NO = lblPANO.Text.Trim() != string.Empty ? lblPANO.Text.Trim() : "NA";

                DataView dv = new DataView(dtReceiptAdjustment);
                dv.RowFilter = " YARN_CODE='" + ItemCode + "' and TRN_NUMB=" + ReceiptNumber + " and LOT_NO='" + lotno + "'  and GRADE='" + grade.Text + "' and CARTON_NO='" + CartonNo.Text + "' and SHADE_CODE='" + ShadeCode + "' and SHADE_FAMILY='" + ShadeFAMILY + "'";

                DataView dvDup = new DataView(dtItemReceipt);
                dvDup.RowFilter = "COMP_CODE='" + dv[0]["COMP_CODE"] + "' and BRANCH_CODE='" + dv[0]["BRANCH_CODE"] + "' and TRN_TYPE='" + Receipt_Type + "' and TRN_NUMB='" + ReceiptNumber + "' and ISS_PI_NO='" + ISS_PI_NO + "' AND YARN_CODE='" + ItemCode + "' and YEAR='" + dv[0]["YEAR"].ToString() + "' and LOT_NO='" + lotno + "' and SHADE_CODE='" + ShadeCode + "' and SHADE_FAMILY='" + ShadeFAMILY + "'   and GRADE='" + grade.Text + "' and CARTON_NO='" + CartonNo.Text + "'  ";
                if (dvDup.Count > 0)
                {
                   // dvDup[0]["NO_OF_UNIT"] = UNIT_NO;
                    dvDup[0]["NO_OF_UNIT"] = AdjustCheese;

                    dvDup[0]["UOM_OF_UNIT"] = UNITUOM;

                    dvDup[0]["WEIGHT_OF_UNIT"] = UnitWeight;
                    dvDup[0]["ISSUE_QTY"] = Adjustqty;
                    dvDup[0]["FINAL_RATE"] = double.Parse(dv[0]["FINAL_RATE"].ToString());
                    dvDup[0]["PI_NO"] = piNo.Text;// "NA";
                    dvDup[0]["ISS_PI_NO"] = ISS_PI_NO;
                    dtItemReceipt.AcceptChanges();
                }
                else if (Adjustqty > 0)
                {
                    int Year = int.Parse(dv[0]["YEAR"].ToString());
                    string Comp_code = dv[0]["COMP_CODE"].ToString();
                    string Branch_code = dv[0]["BRANCH_CODE"].ToString();
                    string PO_Comp = dv[0]["PO_COMP_CODE"].ToString();
                    string PO_Branch = dv[0]["PO_BRANCH"].ToString();
                    string PO_Type = dv[0]["PO_TYPE"].ToString();
                    int PO_numb = 0;
                    if (dv[0]["PO_NUMB"].ToString() != string.Empty)
                    {
                        PO_numb = Convert.ToInt32(dv[0]["PO_NUMB"].ToString());
                    }
                    double Final_rate = 0f;
                    if (dv[0]["FINAL_RATE"].ToString() != string.Empty)
                    {
                        Final_rate = double.Parse(dv[0]["FINAL_RATE"].ToString());
                    }
                    DataRow dr = dtItemReceipt.NewRow();
                    dr["YEAR"] = Year;
                    dr["COMP_CODE"] = Comp_code;
                    dr["BRANCH_CODE"] = Branch_code;
                    dr["TRN_TYPE"] = Receipt_Type;
                    dr["TRN_NUMB"] = int.Parse(ReceiptNumber);
                    dr["PO_COMP"] = PO_Comp;
                    dr["PO_BRANCH"] = PO_Branch;
                    dr["PO_TYPE"] = PO_Type;
                    dr["PO_NUMB"] = PO_numb;
                    dr["PO_YEAR"] = dv[0]["PO_YEAR"];
                    dr["YARN_CODE"] = ItemCode;
                    dr["SHADE_CODE"] = ShadeCode;
                    dr["SHADE_FAMILY"] = ShadeFAMILY;
                    dr["ISSUE_QTY"] = Adjustqty;
                    dr["FINAL_RATE"] = Final_rate;
                    dr["LOT_NO"] = dv[0]["LOT_NO"].ToString();
                    dr["DYED_BATCH"] = dv[0]["DYED_BATCH"].ToString();
                   // dr["NO_OF_UNIT"] = UNIT_NO;
                    dr["NO_OF_UNIT"] = AdjustCheese;
                    dr["UOM_OF_UNIT"] = UNITUOM;
                    dr["WEIGHT_OF_UNIT"] = UnitWeight;
                   
                    dr["ISS_PI_NO"] = ISS_PI_NO;
                    dr["LOCATION"] = LOCATION;
                    dr["STORE"] = STORE;

                    dr["GRADE"] = grade.Text;
                    dr["CARTON_NO"] = CartonNo.Text;
                    dr["PI_NO"] = piNo.Text;//"NA";
                    double GROSS_WT=0;
                    double.TryParse(grossWt.Text, out GROSS_WT);
                    dr["GROSS_WT"] = GROSS_WT;
                    dtItemReceipt.Rows.Add(dr);
                }

            }
            return dtItemReceipt;
        }
        catch
        {
            throw;
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
        dt.Columns.Add("PO_YEAR", typeof(int));
        dt.Columns.Add("YARN_CODE", typeof(string));
        dt.Columns.Add("SHADE_CODE", typeof(string));
        dt.Columns.Add("SHADE_FAMILY", typeof(string));
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
        dt.Columns.Add("LOT_NO", typeof(string));
        dt.Columns.Add("NO_OF_UNIT", typeof(double));
        dt.Columns.Add("UOM_OF_UNIT", typeof(string));
        dt.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
        dt.Columns.Add("PI_NO", typeof(string));
        dt.Columns.Add("ISS_PI_NO", typeof(string));
        dt.Columns.Add("LOCATION", typeof(string));
        dt.Columns.Add("STORE", typeof(string));
        dt.Columns.Add("GROSS_WT", typeof(double ));
        dt.Columns.Add("GRADE", typeof(string));
        dt.Columns.Add("CARTON_NO", typeof(string));
        dt.Columns.Add("DYED_BATCH", typeof(string));

        return dt;
    }

    private double GetWaightedAvarageRate(out double TotalQTY, out double TotalUnit, out string UnitUOM, out double TotalWeight, out double TotalGrossWt,out double TotalTareWT)
    {
        try
        {
            TotalQTY = 0;
            TotalUnit = 0;
            TotalWeight = 0;
            TotalGrossWt = 0;
            TotalTareWT = 0;
            UnitUOM = string.Empty;
            double TotalWeightedRate = 0;
            double TotalAverageWeight = 0;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            foreach (DataRow dr in dtItemReceipt.Rows)
            {

                double Qty = double.Parse(dr["ISSUE_QTY"].ToString());
                double QtyUnit = double.Parse(dr["NO_OF_UNIT"].ToString());
                UnitUOM = dr["UOM_OF_UNIT"].ToString();
                double QtyWeight = double.Parse(dr["WEIGHT_OF_UNIT"].ToString());
                double Rate = double.Parse(dr["FINAL_RATE"].ToString());
                string YARN_CODE = dr["YARN_CODE"].ToString();
                string ShadeCode = dr["SHADE_CODE"].ToString();
                string ShadeFAMILY = dr["SHADE_FAMILY"].ToString();
                string pi_no = dr["ISS_PI_NO"].ToString();
                double  gross_wt = double.Parse(dr["GROSS_WT"].ToString());
                string labelPI = lblPANO.Text.Trim() != string.Empty ? lblPANO.Text.Trim() : "NA";
                if (pi_no == labelPI && YARN_CODE == lblAdjustItemReceiptCode.Text.Trim() && ShadeCode == lblAdjustItemReceiptShade.Text.Trim() && ShadeFAMILY == lblShadeFamily.Text.Trim())
                {
                    double WeightedRate = Qty * Rate;
                    TotalWeightedRate = TotalWeightedRate + WeightedRate;

                    TotalAverageWeight = QtyWeight;
                    TotalQTY = TotalQTY + Qty;
                    TotalUnit = TotalUnit + QtyUnit;
                    TotalGrossWt = TotalGrossWt + gross_wt;
                    TotalTareWT = TotalTareWT + (gross_wt - Qty);
                }
            }
            double FinalWeightedRate = TotalWeightedRate / TotalQTY;
            TotalWeight = TotalAverageWeight;

            foreach (DataRow dr in dtItemReceipt.Rows)
            {
                dr["FINAL_RATE"] = FinalWeightedRate;
            }
            Session["dtItemReceipt"] = dtItemReceipt;
            return FinalWeightedRate;
        }
        catch
        {
            throw;
        }
    }

    protected void grdReceiptAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblNoOfUnit = (Label)row.FindControl("lblNoOfUnit");
            Label lblAdjustRemQty = (Label)row.FindControl("lblAdjustRemQty");
            TextBox txtAdjustedReceiptQTY = (TextBox)row.FindControl("txtAdjustedReceiptQTY");

            txtAdjustedReceiptQTY.Text = lblAdjustRemQty.Text;
            Label txtFinalQTY = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTY");
            Label txtTotalQTYUnit = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTYUnit");
            Label txtFinalCheese = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptCheese");


            TextBox txtAdjustedReceiptQTYUnit = (TextBox)row.FindControl("txtAdjustedReceiptQTYUnit");
            TextBox txtAdjustedReceiptQTY1 = (TextBox)row.FindControl("txtAdjustedReceiptQTY");
            TextBox txtAdjustedReceiptCheese = (TextBox)row.FindControl("txtAdjustedReceiptCheese");


            txtAdjustedReceiptQTYUnit.Text = lblNoOfUnit.Text;
            txtAdjustedReceiptQTY1.Text = lblAdjustRemQty.Text;
            txtAdjustedReceiptCheese.Text = lblNoOfUnit.Text;


            double TotalUnit = 0;
            double FinalQtya = 0;
            double MaxQty = 0;

            double TotalCheese = 0;
            double FinalCheese = 0;
            double NoOfCheese = 0;
            FinalQtya = GetFinalTotalOfItemReceiptAdjustment(out TotalUnit);
            FinalCheese = GetFinalTotalOfItemReceiptAdjustmentCheese(out TotalCheese);

            
                 double.TryParse(lblNoOfUnit.Text, out NoOfCheese);
            double.TryParse(lblMaxQty.Text, out MaxQty);
            if (MaxQty != 0 && NoOfCheese != 0)
            {
                if (MaxQty  < FinalQtya && NoOfCheese < FinalCheese)
                {
                    //txtFinalQTY.Text = "0";
                    //txtAdjustedReceiptQTY.Text = "0";
                    //Common.CommonFuction.ShowMessage("total qty can not be more than remaining invoiced qty.");
                    txtFinalCheese.Text = FinalCheese.ToString();
                    txtFinalQTY.Text = FinalQtya.ToString();
                   
                    Common.CommonFuction.ShowMessage("total qty is not being more than remaining invoiced qty.");


                }
                else
                {
                    txtFinalCheese.Text = FinalCheese.ToString();
                    txtFinalQTY.Text = FinalQtya.ToString();
                }
            }
            else
            {
                txtFinalCheese.Text = FinalCheese.ToString();
                txtFinalQTY.Text = FinalQtya.ToString();
            }

          //  txtFinalCheese.Text = TotalCheese.ToString();
            txtTotalQTYUnit.Text = TotalUnit.ToString();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in adjusting all receipt quantity.\r\nsee error log for detail.");
        }
    }

    private double GetFinalTotalOfItemReceiptAdjustmentCheese(out double FinalTotalCheese)
    {
        try
        {
            double FinalTotal = 0;
            FinalTotalCheese = 0;
            for (int iLoop = 0; iLoop < grdReceiptAdjustment.Rows.Count; iLoop++)
            {
                TextBox txtAdjustedReceiptCheese = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptCheese");
                Label lblNoOfUnit = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblNoOfUnit");
                double Total = 0;
                double.TryParse(txtAdjustedReceiptCheese.Text, out Total);
                txtAdjustedReceiptCheese.Text = Total.ToString();

                if (double.Parse(txtAdjustedReceiptCheese.Text) <= double.Parse(lblNoOfUnit.Text))
                {
                    lblReceiptAdjustmentError.Text = string.Empty;
                    FinalTotal = FinalTotal + Total;
                }
                else
                {
                    lblReceiptAdjustmentError.Text = "Entered Cheese is larger then Receipt quantity";
                    txtAdjustedReceiptCheese.Text = 0.ToString();

                    break;
                }
            }
            return FinalTotal;
        }
        catch
        {
            throw;
        }
    }









    protected void txtAdjustedReceiptQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)sender;
            Label txtFinalQTY = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTY");
            double TotalUnit = 0;

            double FinalQtya = 0;
            double MaxQty = 0;
            FinalQtya = GetFinalTotalOfItemReceiptAdjustment(out TotalUnit);
            double.TryParse(lblMaxQty.Text, out MaxQty);
            MaxQty = MaxQty + 10;
            if (MaxQty != 0)
            {
                if (MaxQty < FinalQtya)
                {
                    txtFinalQTY.Text = "0";
                    thisTextBox.Text = "0";
                    Common.CommonFuction.ShowMessage("total qty can not be more than remaining invoiced qty.");
                }
                else
                {
                    txtFinalQTY.Text = FinalQtya.ToString();
                }
            }
            else
            {
                txtFinalQTY.Text = FinalQtya.ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in Quantity adjustment.\r\nsee error log for detail.");
        }
    }


    protected void txtAdjustedReceiptQTY_TextChanged2(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)sender;
            Label txtFinalQTY = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptCheese");
            Label lblCheeseUnit = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTYUnit");

            
            double TotalUnit = 0;

            double FinalQtya = 0;
            double MaxQty = 0;
            FinalQtya = GetFinalTotalOfItemReceiptAdjustmentCheese(out TotalUnit);
            double.TryParse(lblCheeseUnit.Text, out MaxQty);
           // MaxQty = MaxQty + 10;
            if (MaxQty != 0)
            {
                if (MaxQty < FinalQtya)
                {
                    txtFinalQTY.Text = "0";
                    thisTextBox.Text = "0";
                    Common.CommonFuction.ShowMessage("total cheese can not be more than remaining Cheese.");
                }
                else
                {
                    txtFinalQTY.Text = FinalQtya.ToString();
                }
            }
            else
            {
                txtFinalQTY.Text = FinalQtya.ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in Quantity adjustment.\r\nsee error log for detail.");
        }
    }








}

