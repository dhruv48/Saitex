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

public partial class Module_OrderDevelopment_Pages_SewingThread_Recipt_Adjustment : System.Web.UI.Page
{
    private static string TextBoxId;
    private static string noofUnit_ID;
    private static int TRN_NUMB;

    private static string SHADE_CODE;
    private static string txtBasicRate;
    private static string AmountId;
    public static string CombineId;
    public static string TRN_TPYE;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                noofUnit_ID = string.Empty;
                SHADE_CODE = string.Empty;
                TextBoxId = "";
                TRN_NUMB = 0;

                txtBasicRate = string.Empty;
                AmountId = string.Empty;
                CombineId = string.Empty;
                TRN_TPYE = string.Empty;

                if (Request.QueryString["TRN_TYPE"] != null)
                {
                    TRN_TPYE = Request.QueryString["TRN_TYPE"].ToString();
                }
                if (Request.QueryString["noofUnit_ID"] != null)
                {
                    noofUnit_ID = Request.QueryString["noofUnit_ID"].ToString();
                }
                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
                }
                if (Request.QueryString["ChallanNo"] != null)
                {
                    TRN_NUMB = int.Parse(Request.QueryString["ChallanNo"].ToString());
                }
                if (Request.QueryString["AmountId"] != null)
                {
                    AmountId = Request.QueryString["AmountId"].ToString();
                }
                if (Request.QueryString["txtBasicRate"] != null)
                {
                    txtBasicRate = Request.QueryString["txtBasicRate"].ToString();
                }
                if (Request.QueryString["UOM_OF_UNIT"] != null)
                {
                    string UOM_OF_UNIT = Request.QueryString["UOM_OF_UNIT"].ToString();
                    ViewState["UOM_OF_UNIT"] = UOM_OF_UNIT;
                }
                if (Request.QueryString["ItemCodeId"] != null)
                {
                    string ItemCode = Request.QueryString["ItemCodeId"].ToString();
                    GetDataForItemAdjustment(ItemCode);
                }
                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                }
                if (Request.QueryString["CombineId"] != null)
                {
                    CombineId = Request.QueryString["CombineId"].ToString();
                }
                if (Request.QueryString["REMQTY"] != null)
                {
                    lblRemainingMaxQty.Text = Request.QueryString["REMQTY"].ToString();
                }

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, "");
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in Page Loading.\r\nsee error log for detail.");
        }
    }

    private void GetDataForItemAdjustment(string ItemCode)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.YRN_IR_ISS_ADJ oYRN_IR_ISS_ADJ = new SaitexDM.Common.DataModel.YRN_IR_ISS_ADJ();

            if (TRN_NUMB > 0)
            {
                oYRN_IR_ISS_ADJ.TRN_NUMB = TRN_NUMB;
            }
            oYRN_IR_ISS_ADJ.ITEM_CODE = ItemCode;
            oYRN_IR_ISS_ADJ.SHADE_CODE = SHADE_CODE;
            oYRN_IR_ISS_ADJ.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_ISS_ADJ.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_ISS_ADJ.TRN_TYPE = TRN_TPYE;
            oYRN_IR_ISS_ADJ.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            if (ViewState["UOM_OF_UNIT"] != null)
            {
                oYRN_IR_ISS_ADJ.UOM_OF_UNIT = ViewState["UOM_OF_UNIT"].ToString();
            }
            else
            {
                oYRN_IR_ISS_ADJ.UOM_OF_UNIT = "BOX";
            }

            DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.GetDataForIssueAdjustment(oYRN_IR_ISS_ADJ);

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

    private double GetFinalTotalOfItemReceiptAdjustment()
    {
        try
        {
            double FinalTotal = 0;
            for (int iLoop = 0; iLoop < grdReceiptAdjustment.Rows.Count; iLoop++)
            {
                Label lblunitweight = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblunitweight");
                Label lblnoofunit = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblnoofunit");
                Label lblAdjustRemQty = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAdjustRemQty");

                TextBox txtAdjustedReceiptNoofUnit = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptNoofUnit");
                TextBox txtAdjustedReceiptUnitWeight = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptUnitWeight");
                TextBox txtAdjustedReceiptQTY = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTY");

                double unitweight = 0;
                double noofunit = 0;
                double total = 0;

                double adjunitweight = 0;
                double adjnoofunit = 0;
                double adjtotalqty = 0;

                double.TryParse(lblunitweight.Text, out unitweight);
                double.TryParse(lblnoofunit.Text, out noofunit);
                double.TryParse(lblAdjustRemQty.Text, out total);

                double.TryParse(txtAdjustedReceiptUnitWeight.Text, out adjunitweight);
                double.TryParse(txtAdjustedReceiptNoofUnit.Text, out adjnoofunit);
                double.TryParse(txtAdjustedReceiptQTY.Text, out adjtotalqty);

                adjunitweight = unitweight;
                total = unitweight * noofunit;
                adjtotalqty = adjnoofunit * adjunitweight;
                txtAdjustedReceiptUnitWeight.Text = adjunitweight.ToString();

                if (adjtotalqty <= total)
                {
                    FinalTotal = FinalTotal + adjtotalqty;
                    txtAdjustedReceiptNoofUnit.Text = adjnoofunit.ToString();
                    txtAdjustedReceiptUnitWeight.Text = adjunitweight.ToString();
                    txtAdjustedReceiptQTY.Text = adjtotalqty.ToString();
                }
                else
                {
                    lblReceiptAdjustmentError.Text = "Entered quantity is larger then Receipt quantity";
                    txtAdjustedReceiptQTY.Text = 0.ToString();
                    txtAdjustedReceiptNoofUnit.Text = 0.ToString();
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

            Label TotalAdjustedReceiptQTY = grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTY") as Label;
            double TotalAdjustedQTY;
            double.TryParse(TotalAdjustedReceiptQTY.Text, out  TotalAdjustedQTY);
            double RemainingMaxQty;
            double.TryParse(lblRemainingMaxQty.Text, out  RemainingMaxQty);


            if (TotalAdjustedQTY > RemainingMaxQty)
            {
                Common.CommonFuction.ShowMessage("Please Enter Below Quantity Than Remaning Quantity");
            }
            else
            {
                double TotalQTY = 0;
                double NoofUnit = 0;
                DataTable dtItemReceipt = createdatatableforadjustment();
                Session["dtItemReceipt"] = dtItemReceipt;
                double FinalRate = GetWaightedAvarageRate(out TotalQTY, out NoofUnit);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "','" + txtBasicRate + "','" + FinalRate + "','" + AmountId + "','" + TotalQTY * FinalRate + "','" + NoofUnit + "','" + noofUnit_ID + "')", true);

            }

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, "");
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in receipt adjustment.\r\nsee error log for detail.");
        }
    }

    private DataTable createdatatableforadjustment()
    {
        try
        {
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
                if (Adjustqty > 0)
                {

                    string ItemCode = lblAdjustItemReceiptCode.Text.Trim();

                    Label lblAPPR_QTY = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
                    double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());

                    string Receipt_Type = lblAPPR_QTY.ToolTip.Trim();

                    Label lblLotNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblLotNo");
                    string lotno = lblLotNo.Text.Trim();

                    TextBox txtAdjustedReceiptNoofUnit = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptNoofUnit");
                    double noofunit = 0;
                    double.TryParse(txtAdjustedReceiptNoofUnit.Text, out noofunit);

                    TextBox txtAdjustedReceiptUnitWeight = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptUnitWeight");
                    double unitweight = 0;
                    double.TryParse(txtAdjustedReceiptUnitWeight.Text, out unitweight);

                    Label lblbaseuom = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblbaseuom");

                    DataView dv = new DataView(dtReceiptAdjustment);
                    dv.RowFilter = "YARN_CODE='" + ItemCode + "' and SHADE_CODE='" + SHADE_CODE + "' and TRN_NUMB=" + ReceiptNumber + " and LOT_NO='" + lotno + "'";

                    DataView dvDup = new DataView(dtItemReceipt);
                    dvDup.RowFilter = "COMP_CODE='" + dv[0]["COMP_CODE"] + "' and BRANCH_CODE='" + dv[0]["BRANCH_CODE"] + "' and TRN_TYPE='" + Receipt_Type + "' and TRN_NUMB='" + ReceiptNumber + "' and YARN_CODE='" + ItemCode + "' and SHADE_CODE='" + SHADE_CODE + "' and YEAR='" + dv[0]["YEAR"].ToString() + "'";
                    if (dvDup.Count > 0)
                    {
                        dvDup[0]["ISSUE_QTY"] = Adjustqty;
                        dvDup[0]["NO_OF_UNIT"] = noofunit;
                        dvDup[0]["UOM_OF_UNIT"] = lblbaseuom.Text;
                        dvDup[0]["WEIGHT_OF_UNIT"] = unitweight;
                        dvDup[0]["FINAL_RATE"] = double.Parse(dv[0]["FINAL_RATE"].ToString());
                        dtItemReceipt.AcceptChanges();
                    }
                    else
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
                        dr["YARN_CODE"] = ItemCode;
                        dr["SHADE_CODE"] = SHADE_CODE;
                        dr["ISSUE_QTY"] = Adjustqty;
                        dr["FINAL_RATE"] = Final_rate;
                        dr["LOT_NO"] = dv[0]["LOT_NO"].ToString();
                        dr["NO_OF_UNIT"] = noofunit;
                        dr["UOM_OF_UNIT"] = lblbaseuom.Text;
                        dr["WEIGHT_OF_UNIT"] = unitweight;
                        dr["PI_NO"] = "NA";
                        dr["ISS_PI_NO"] = "NA";

                        dtItemReceipt.Rows.Add(dr);
                    }
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
        dt.Columns.Add("YARN_CODE", typeof(string));
        dt.Columns.Add("SHADE_CODE", typeof(string));
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
        return dt;
    }

    private double GetWaightedAvarageRate(out double TotalQTY, out double iNoofUnit)
    {
        try
        {
            iNoofUnit = 0;
            TotalQTY = 0;
            double TotalWeightedRate = 0;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];

            foreach (DataRow dr in dtItemReceipt.Rows)
            {

                double Qty = double.Parse(dr["ISSUE_QTY"].ToString());
                double Noofunits = double.Parse(dr["NO_OF_UNIT"].ToString());
                double Rate = double.Parse(dr["FINAL_RATE"].ToString());

                string YARN_CODE = dr["YARN_CODE"].ToString();
                string sSHADE_CODE = dr["SHADE_CODE"].ToString();

                if (YARN_CODE == lblAdjustItemReceiptCode.Text.Trim() && SHADE_CODE == sSHADE_CODE)
                {
                    double WeightedRate = Qty * Rate;
                    TotalWeightedRate = TotalWeightedRate + WeightedRate;
                    TotalQTY = TotalQTY + Qty;
                    iNoofUnit = iNoofUnit + Noofunits;
                }
            }
            double FinalWeightedRate = TotalWeightedRate / TotalQTY;
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

            Label lblunitweight = (Label)row.FindControl("lblunitweight");
            Label lblnoofunit = (Label)row.FindControl("lblnoofunit");
            Label lblAdjustRemQty = (Label)row.FindControl("lblAdjustRemQty");

            TextBox txtAdjustedReceiptNoofUnit = (TextBox)row.FindControl("txtAdjustedReceiptNoofUnit");
            TextBox txtAdjustedReceiptUnitWeight = (TextBox)row.FindControl("txtAdjustedReceiptUnitWeight");
            TextBox txtAdjustedReceiptQTY = (TextBox)row.FindControl("txtAdjustedReceiptQTY");
            txtAdjustedReceiptQTY.Text = lblAdjustRemQty.Text;
            txtAdjustedReceiptNoofUnit.Text = lblnoofunit.Text;
            txtAdjustedReceiptUnitWeight.Text = lblunitweight.Text;

            Label txtFinalQTY = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTY");
            txtFinalQTY.Text = GetFinalTotalOfItemReceiptAdjustment().ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, "");
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in adjusting all receipt quantity.\r\nsee error log for detail.");
        }
    }

    protected void txtAdjustedReceiptNoofUnit_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            Label txtFinalQTY = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTY");
            txtFinalQTY.Text = GetFinalTotalOfItemReceiptAdjustment().ToString();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, "");
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in Quantity adjustment.\r\nsee error log for detail.");
        }
    }

}
