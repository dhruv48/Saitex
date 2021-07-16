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

public partial class Module_Inventory_Pages_FabricReceiptAdjustment : System.Web.UI.Page
{
   private static string TextBoxId;
    //private static string btnGetGridValue;
    private static int TRN_NUMB;
    private static string TRN_TYPE;
    private static double FinalRate = 0;
    private static string txtBasicRate;
    private static string AmountId;
    public static string CombineId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
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
                if (Request.QueryString["AmountId"] != null)
                {
                    AmountId = Request.QueryString["AmountId"].ToString();
                }
                if (Request.QueryString["txtBasicRate"] != null)
                {
                    txtBasicRate = Request.QueryString["txtBasicRate"].ToString();
                }
                if (Request.QueryString["FabricCodeId"] != null)
                {
                    string FabricCode = Request.QueryString["FabricCodeId"].ToString();
                    GetDataForItemAdjustment(FabricCode);
                }
                if (Request.QueryString["TextBoxId"] != null)
                {
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                }
                if (Request.QueryString["CombineId"] != null)
                {
                    CombineId = Request.QueryString["CombineId"].ToString();
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
    private void GetDataForItemAdjustment(string FabricCode)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FABRIC_IR_ISS_ADJ oTX_FABRIC_IR_ISS_ADJ = new SaitexDM.Common.DataModel.TX_FABRIC_IR_ISS_ADJ();

            if (TRN_NUMB > 0)
            {
                oTX_FABRIC_IR_ISS_ADJ.TRN_NUMB = TRN_NUMB;
                oTX_FABRIC_IR_ISS_ADJ.TRN_TYPE = TRN_TYPE;
            }
            oTX_FABRIC_IR_ISS_ADJ.FABR_CODE = FabricCode;
            oTX_FABRIC_IR_ISS_ADJ.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_ISS_ADJ.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_ISS_ADJ.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_ISS_ADJ.SHADE_CODE = "NA";
            oTX_FABRIC_IR_ISS_ADJ.ISS_PI_NO = "NA";
            DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetDataForIssueAdjustment(oTX_FABRIC_IR_ISS_ADJ);

            grdReceiptAdjustment.DataSource = null;
            grdReceiptAdjustment.DataBind();

            lblAdjustItemReceiptCode.Text = FabricCode;
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
                lblReceiptAdjustmentError.Text = "No Record exists for adjustment for provided Fabric";
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
                double Total = double.Parse(txtAdjustedReceiptQTY.Text);
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
            DataTable dtFabricReceipt = createdatatableforadjustment();
            Session["dtFabricReceipt"] = dtFabricReceipt;
            double FinalRate = GetWaightedAvarageRate(out TotalQTY);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "','" + txtBasicRate + "','" + FinalRate + "','" + AmountId + "','" + TotalQTY * FinalRate + "')", true);
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
            DataTable dtReceiptAdjustment = (DataTable)ViewState["dtReceiptAdjustment"];

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];


            DataTable dtFabricReceipt = new DataTable();
            if (Session["dtFabricReceipt"] == null)
                dtFabricReceipt = createAdjTable();
            else
            {
                dtFabricReceipt.Rows.Clear();
                dtFabricReceipt = (DataTable)Session["dtFabricReceipt"];
            }
            for (int iLoop = 0; iLoop < grdReceiptAdjustment.Rows.Count; iLoop++)
            {
                Label lblAdjustReceiptNuber = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAdjustReceiptNuber");
                string ReceiptNumber = lblAdjustReceiptNuber.Text.Trim();

                TextBox txtAdjustedReceiptQTY = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTY");
                double Adjustqty = double.Parse(txtAdjustedReceiptQTY.Text.Trim());
                if (Adjustqty > 0)
                {
                    string FabricCode = lblAdjustItemReceiptCode.Text.Trim();

                    Label lblTRN_TYPE = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblTRN_TYPE");
                    Label lblAPPR_QTY = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
                    double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());

                    string Receipt_Type = lblTRN_TYPE.Text.Trim();

                    DataView dv = new DataView(dtReceiptAdjustment);
                    dv.RowFilter = "FABR_CODE='" + FabricCode + "' and TRN_NUMB=" + ReceiptNumber;

                    DataView dvDup = new DataView(dtFabricReceipt);
                    dvDup.RowFilter = "COMP_CODE='" + dv[0]["COMP_CODE"] + "' and BRANCH_CODE='" + dv[0]["BRANCH_CODE"] + "' and TRN_TYPE='" + Receipt_Type + "' and TRN_NUMB='" + ReceiptNumber + "' and FABR_CODE='" + FabricCode + "' and YEAR='" + dv[0]["YEAR"].ToString() + "'";
                    if (dvDup.Count > 0)
                    {
                        dvDup[0]["ISSUE_QTY"] = Adjustqty;
                        dvDup[0]["FINAL_RATE"] = double.Parse(dv[0]["FINAL_RATE"].ToString());
                        dtFabricReceipt.AcceptChanges();
                    }
                    else
                    {
                        int Year = int.Parse(dv[0]["YEAR"].ToString());
                        string Comp_code = dv[0]["COMP_CODE"].ToString();
                        string Branch_code = dv[0]["BRANCH_CODE"].ToString();
                        string PO_Comp = dv[0]["PO_COMP_CODE"].ToString();
                        string PO_Branch = dv[0]["PO_BRANCH"].ToString();
                        string PO_Type = dv[0]["PO_TYPE"].ToString();
                        int PO_numb = int.Parse(dv[0]["PO_NUMB"].ToString());
                        double Final_rate = double.Parse(dv[0]["FINAL_RATE"].ToString());

                        DataRow dr = dtFabricReceipt.NewRow();
                        dr["YEAR"] = Year;
                        dr["COMP_CODE"] = Comp_code;
                        dr["BRANCH_CODE"] = Branch_code;
                        dr["TRN_TYPE"] = Receipt_Type;
                        dr["TRN_NUMB"] = int.Parse(ReceiptNumber);
                        dr["PO_COMP"] = PO_Comp;
                        dr["PO_BRANCH"] = PO_Branch;
                        dr["PO_TYPE"] = PO_Type;
                        dr["PO_NUMB"] = PO_numb;
                        dr["FABR_CODE"] = FabricCode;
                        dr["ISSUE_QTY"] = Adjustqty;
                        dr["FINAL_RATE"] = Final_rate;

                        dtFabricReceipt.Rows.Add(dr);
                    }
                }
            }
            return dtFabricReceipt;
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
        dt.Columns.Add("FABR_CODE", typeof(string));
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
    private double GetWaightedAvarageRate(out double TotalQTY)
    {
        try
        {
            TotalQTY = 0;
            double TotalWeightedRate = 0;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtFabricReceipt = (DataTable)Session["dtFabricReceipt"];

            foreach (DataRow dr in dtFabricReceipt.Rows)
            {

                double Qty = double.Parse(dr["ISSUE_QTY"].ToString());
                double Rate = double.Parse(dr["FINAL_RATE"].ToString());
                //string COMP_CODE = dr["COMP_CODE"].ToString();
                //string BRANCH_CODE = dr["BRANCH_CODE"].ToString();
                //int YEAR = int.Parse(dr["YEAR"].ToString());
                string FABR_CODE = dr["FABR_CODE"].ToString();

                if (FABR_CODE == lblAdjustItemReceiptCode.Text.Trim())
                {
                    double WeightedRate = Qty * Rate;
                    TotalWeightedRate = TotalWeightedRate + WeightedRate;
                    TotalQTY = TotalQTY + Qty;
                }
            }
            double FinalWeightedRate = TotalWeightedRate / TotalQTY;
            foreach (DataRow dr in dtFabricReceipt.Rows)
            {
                dr["FINAL_RATE"] = FinalWeightedRate;
            }
            Session["dtFabricReceipt"] = dtFabricReceipt;
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
            Label lblAdjustRemQty = (Label)row.FindControl("lblAdjustRemQty");
            TextBox txtAdjustedReceiptQTY = (TextBox)row.FindControl("txtAdjustedReceiptQTY");
            txtAdjustedReceiptQTY.Text = lblAdjustRemQty.Text;
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

