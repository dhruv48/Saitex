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

public partial class Inventory_ReceiptAdjustment : System.Web.UI.Page
{
    private static string TextBoxId;
    //private static string btnGetGridValue;
    private static int TRN_NUMB;
    private static string TRN_TYPE;
    private static double FinalRate = 0;
    private static string txtBasicRate;
    private static string AmountId;
    public static string CombineId;
    private string txtQtyUnit;
    private string txtQtyUom;
    private string txtQtyWeight;
    private string LOCATION;
    private string STORE;

    protected void Page_Load(object sender, EventArgs e)
     {
        try
        {
            if (!IsPostBack)
            {
                TRN_NUMB = 0;
                TRN_TYPE = "";
                FinalRate = 0;
                txtBasicRate = "";
                AmountId = "";
                CombineId = "";
                TextBoxId = "";
                TRN_NUMB = 0;
                if (Request.QueryString["ChallanNo"] != null)
                {
                    TRN_NUMB = int.Parse(Request.QueryString["ChallanNo"].ToString());
                }
                if (Request.QueryString["TRN_TYPE"] != null)
                {
                    TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
                }


                string PA_NO = string.Empty;
                if (Request.QueryString["PI_NO"] != null)
                {
                    PA_NO = Request.QueryString["PI_NO"].ToString();
                }
                lblPaHeding.Text = "For Pa No: ";
                lblPANO.Text = PA_NO;


                if (Request.QueryString["AmountId"] != null)
                {
                    AmountId = Request.QueryString["AmountId"].ToString();
                }
                if (Request.QueryString["txtBasicRate"] != null)
                {
                    txtBasicRate = Request.QueryString["txtBasicRate"].ToString();
                }

                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                }
                if (Request.QueryString["CombineId"] != null)
                {
                    CombineId = Request.QueryString["CombineId"].ToString();
                }
                if (Request.QueryString["txtQtyUnit"] != null)
                {

                    txtQtyUnit = Request.QueryString["txtQtyUnit"].ToString();
                    //ViewState["txtQtyUnit"] = txtQtyUnit;

                }
                if (Request.QueryString["txtQtyUom"] != null)
                {

                    txtQtyUom = Request.QueryString["txtQtyUom"].ToString();
                    //ViewState["txtQtyUom"] = txtQtyUom;

                }
                if (Request.QueryString["txtQtyWeight"] != null)
                {
                    txtQtyWeight = Request.QueryString["txtQtyWeight"].ToString();
                    //ViewState["txtQtyWeight"] = txtQtyWeight;
                }
                if (Request.QueryString["LOCATION"] != null)
                {
                    LOCATION = Request.QueryString["LOCATION"].ToString();
                }
                if (Request.QueryString["STORE"] != null)
                {
                    STORE = Request.QueryString["STORE"].ToString();
                }
                if (Request.QueryString["ItemCodeId"] != null)
                {
                    string ItemCode = Request.QueryString["ItemCodeId"].ToString();
                    GetDataForItemAdjustment(ItemCode);
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
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_ITEM_IR_ISS_ADJ oTX_ITEM_IR_ISS_ADJ = new SaitexDM.Common.DataModel.TX_ITEM_IR_ISS_ADJ();

            if (TRN_NUMB > 0)
            {
                oTX_ITEM_IR_ISS_ADJ.TRN_NUMB = TRN_NUMB;
                oTX_ITEM_IR_ISS_ADJ.TRN_TYPE = TRN_TYPE;
            }
            oTX_ITEM_IR_ISS_ADJ.ITEM_CODE = ItemCode;
            oTX_ITEM_IR_ISS_ADJ.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_ISS_ADJ.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_ISS_ADJ.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IR_ISS_ADJ.ISS_PI_NO = lblPANO.Text;
            oTX_ITEM_IR_ISS_ADJ.LOCATION=LOCATION;
            oTX_ITEM_IR_ISS_ADJ.STORE = STORE;
            DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataForIssueAdjustment(oTX_ITEM_IR_ISS_ADJ);

            grdReceiptAdjustment.DataSource = null;
            grdReceiptAdjustment.DataBind();

            lblAdjustItemReceiptCode.Text = ItemCode;
            if (dtReceiptAdjustment != null && dtReceiptAdjustment.Rows.Count > 0)
            {
                if (TRN_NUMB <= 0)
                {
                    dtReceiptAdjustment.Columns.Add("IRADJUST_QTY", typeof(double));
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

    private double GetFinalTotalOfItemReceiptAdjustment()
    {
        try
        {
            double FinalTotal = 0;

            for (int iLoop = 0; iLoop < grdReceiptAdjustment.Rows.Count; iLoop++)
            {
                TextBox txtAdjustedReceiptQTY = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTY");

                Label lblAdjustRemQty = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAdjustRemQty");

                double Total = 0;
                double.TryParse(txtAdjustedReceiptQTY.Text, out Total);

                if (Total <= double.Parse(lblAdjustRemQty.Text))
                {
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
            double TotalQTY = 0;
            double TotalUnit = 0;
            double TotalWeight = 0;
            string UnitUOM = string.Empty;

            DataTable dtItemReceipt = createdatatableforadjustment();
            Session["dtItemReceipt"] = dtItemReceipt;
            FinalRate = GetWaightedAvarageRate(out TotalQTY, out TotalUnit, out UnitUOM, out TotalWeight);
            if (TotalQTY > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "','" + txtBasicRate + "','" + FinalRate + "','" + AmountId + "','" + TotalQTY * FinalRate + "','" + txtQtyUnit + "','" + TotalUnit + "','" + txtQtyUom + "','" + UnitUOM + "','" + txtQtyWeight + "','" + TotalWeight + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:PopUpClose()", true);
            }
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
            DataTable dtReceiptAdjustment = (DataTable)ViewState["dtReceiptAdjustment"]; //dtReceiptAdjustment

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

                string ItemCode = lblAdjustItemReceiptCode.Text.Trim();

                Label lblpocomp = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblpocomp");
                Label lblpoBranch = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblpoBranch");
                Label lblpotype = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblpotype");
                Label lblponumb = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblponumb");
                //Label lblPiNO = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblPiNO");

                Label lblTRN_TYPE = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblTRN_TYPE");
                Label lblAPPR_QTY = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");

                Label lblLocation = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblLocation");
                Label lblStore = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblStore");
                double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());

                string Receipt_Type = lblTRN_TYPE.Text.Trim();

                Label lblLotNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblLotNo");
                string lotno = lblLotNo.Text.Trim();

                double UNIT_NO = 0;
                double UnitWeight = 0;
                string UNITUOM = string.Empty;
                string ISS_PI_NO = lblPANO.Text.Trim() != string.Empty ? lblPANO.Text.Trim() : "NA";

                DataView dv = new DataView(dtReceiptAdjustment);
                dv.RowFilter = "ITEM_CODE='" + ItemCode + "' and TRN_NUMB=" + ReceiptNumber + " and LOT_NO='" + lotno + "' and PO_COMP_CODE='" + lblpocomp.Text.ToString() + "' and PO_BRANCH='" + lblpoBranch.Text.ToString() + "'  and PO_TYPE='" + lblpotype.Text.ToString() + "'  and PO_NUMB='" + int.Parse(lblponumb.Text.ToString()) + "'";

                DataView dvDup = new DataView(dtItemReceipt);
                dvDup.RowFilter = "COMP_CODE='" + dv[0]["COMP_CODE"] + "' and BRANCH_CODE='" + dv[0]["BRANCH_CODE"] + "' and PO_COMP='" + dv[0]["PO_COMP_CODE"].ToString() + "' and PO_BRANCH='" + dv[0]["PO_BRANCH"].ToString() + "'  and PO_TYPE='" + dv[0]["PO_TYPE"].ToString() + "'  and PO_NUMB='" + int.Parse(dv[0]["PO_NUMB"].ToString()) + "'  and TRN_TYPE='" + Receipt_Type + "' and TRN_NUMB='" + ReceiptNumber + "' and ITEM_CODE='" + ItemCode + "' and YEAR='" + dv[0]["YEAR"].ToString() + "' and LOT_NO='" + lotno + "' and  ISS_PI_NO='" + ISS_PI_NO + "'";
                if (dvDup.Count > 0)
                {
                    dvDup[0]["ISSUE_QTY"] = Adjustqty;
                    dvDup[0]["FINAL_RATE"] = double.Parse(dv[0]["FINAL_RATE"].ToString());
                    dvDup[0]["PI_NO"] = "NA";
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
                    int PO_numb = int.Parse(dv[0]["PO_NUMB"].ToString());
                    double Final_rate = double.Parse(dv[0]["FINAL_RATE"].ToString());

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
                    dr["ITEM_CODE"] = ItemCode;
                    dr["ISSUE_QTY"] = Adjustqty;
                    dr["FINAL_RATE"] = Final_rate;
                    dr["LOT_NO"] = dv[0]["LOT_NO"].ToString();
                    dr["NO_OF_UNIT"] = UNIT_NO;
                    dr["UOM_OF_UNIT"] = UNITUOM;
                    dr["WEIGHT_OF_UNIT"] = UnitWeight;
                    dr["PI_NO"] = "NA";
                    dr["ISS_PI_NO"] = ISS_PI_NO;
                    dr["LOCATION"] = lblLocation.Text;
                    dr["STORE"] = lblStore.Text;

                    dtItemReceipt.Rows.Add(dr);
                }

            }
            ViewState["dtItemReceipt"] = dtItemReceipt;
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
        dt.Columns.Add("LOT_NO", typeof(string));
        dt.Columns.Add("NO_OF_UNIT", typeof(double));
        dt.Columns.Add("UOM_OF_UNIT", typeof(string));
        dt.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
        dt.Columns.Add("PI_NO", typeof(string));
        dt.Columns.Add("ISS_PI_NO", typeof(string));
        dt.Columns.Add("LOCATION", typeof(string));
        dt.Columns.Add("STORE", typeof(string));
        return dt;
    }

    private double GetWaightedAvarageRate(out double TotalQTY, out double TotalUnit, out string UnitUOM, out double TotalWeight)
    {
        try
        {
            TotalQTY = 0;
            TotalUnit = 0;
            TotalWeight = 0;
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

                string ITEM_CODE = dr["ITEM_CODE"].ToString();
                string pi_no = dr["ISS_PI_NO"].ToString();
                string labelPI = lblPANO.Text.Trim() != string.Empty ? lblPANO.Text.Trim() : "NA";
                if (pi_no == labelPI && ITEM_CODE == lblAdjustItemReceiptCode.Text.Trim())
                {
                    double WeightedRate = Qty * Rate;
                    TotalWeightedRate = TotalWeightedRate + WeightedRate;

                    TotalAverageWeight = TotalAverageWeight + (QtyWeight * QtyUnit);
                    TotalQTY = TotalQTY + Qty;
                    TotalUnit = TotalUnit + QtyUnit;
                }
            }
            double FinalWeightedRate = 0;
            if (TotalWeightedRate != 0 && TotalQTY != 0)
            {
                FinalWeightedRate = TotalWeightedRate / TotalQTY;
            }

            if (TotalAverageWeight != 0 && TotalUnit != 0)
            {
                TotalWeight = TotalAverageWeight / TotalUnit;
            }

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

    protected void txtAdjustedReceiptQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            Label txtFinalQTY = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTY");

            txtFinalQTY.Text = GetFinalTotalOfItemReceiptAdjustment().ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in Quantity adjustment.\r\nsee error log for detail.");
        }
    }

    protected void grdReceiptAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblAdjustRemQty = (Label)row.FindControl("lblAdjustRemQty");
            Label lblAdjustedQty = (Label)row.FindControl("lblAdjustedQty");
            TextBox txtAdjustedReceiptQTY = (TextBox)row.FindControl("txtAdjustedReceiptQTY");
            double adjremqty = 0;
            double adjstedqty = 0;
            double.TryParse(lblAdjustRemQty.Text, out adjremqty);
            double.TryParse(lblAdjustedQty.Text, out adjstedqty);

           // txtAdjustedReceiptQTY.Text = (adjremqty - adjstedqty).ToString();//lblAdjustRemQty.Text;
            txtAdjustedReceiptQTY.Text = (adjremqty).ToString();
            Label txtFinalQTY = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTY");

            txtFinalQTY.Text = GetFinalTotalOfItemReceiptAdjustment().ToString();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in adjusting all receipt quantity.\r\nsee error log for detail.");
        }
    }

}
