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

public partial class Module_Production_Pages_Production_Fiber_Recipt_Adjustment : System.Web.UI.Page
{

    private string TextBoxId;
    private int PROS_ID_NO;    
    public string CombineId;
    public string TRN_TYPE;
    private string txtQtyUnit;
    private string txtQtyUom;
    private string txtQtyWeight;
    private string LOT_NUMBER;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["TextBoxId"] != null)
            {
                TextBoxId = Request.QueryString["TextBoxId"].ToString();

            }
            if (Request.QueryString["TRN_TYPE"] != null)
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();

            }
            if (Request.QueryString["LOT_NUMBER"] != null)
            {
                LOT_NUMBER = Request.QueryString["LOT_NUMBER"].ToString();

            }

            if (Request.QueryString["PROS_ID_NO"] != null)
            {

                PROS_ID_NO = int.Parse(Request.QueryString["PROS_ID_NO"].ToString());

            }


            if (Request.QueryString["txtQtyUnit"] != null)
            {
                txtQtyUnit = Request.QueryString["txtQtyUnit"].ToString();

            }
            if (Request.QueryString["txtQtyUom"] != null)
            {
                txtQtyUom = Request.QueryString["txtQtyUom"].ToString();

            }
            if (Request.QueryString["txtQtyWeight"] != null)
            {
                txtQtyWeight = Request.QueryString["txtQtyWeight"].ToString();

            }

            if (Request.QueryString["UOM_OF_UNIT"] != null)
            {
                string UOM_OF_UNIT = Request.QueryString["UOM_OF_UNIT"].ToString();

            }

            if (!IsPostBack)
            {

                
              
                if (Request.QueryString["ItemCodeId"] != null)
                {
                    string ItemCode = Request.QueryString["ItemCodeId"].ToString();
                    GetDataForItemAdjustment(ItemCode);
                }
                
                lblPaHeding.Text = "For Merge No: ";
                lblPANO.Text = LOT_NUMBER;

                if (Request.QueryString["MAX_QTY"] != null)
                {
                    lblMaxQty.Text = (string)Request.QueryString["MAX_QTY"].ToString();
                    lblMaxQty.Visible = true;
                    lblMaxQtyDisp.Visible = true;
                }
                else
                {
                    lblMaxQty.Text = "999999";
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
           

            DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForIssueAdjustmentForMearge(PROS_ID_NO, TRN_TYPE, LOT_NUMBER, ItemCode, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);

            lblAdjustItemReceiptCode.Text = ItemCode;            
            if (dtReceiptAdjustment != null && dtReceiptAdjustment.Rows.Count > 0)
            {

                if (PROS_ID_NO <= 0)
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
                grdReceiptAdjustment.DataSource = null;
                grdReceiptAdjustment.DataBind();
                lblReceiptAdjustmentError.Text = "No Record exists for adjustment for provided Item";
            }
        }
        catch
        {
            throw;
        }
    }

    private double GetFinalTotalOfItemReceiptAdjustment(out double FinalTotalUnit, out double TOTAL_NO_OF_UNIT)
    {
        try
        {
            double FinalTotal = 0;
            FinalTotalUnit = 0;
            TOTAL_NO_OF_UNIT = 0;

            for (int iLoop = 0; iLoop < grdReceiptAdjustment.Rows.Count; iLoop++)
            {
                TextBox txtAdjustedReceiptQTY = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTY");
                Label lblAdjustRemQty = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAdjustRemQty");
                TextBox txtAdjustedReceiptQTYUnit = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYUnit");

                double Total = 0;
                double.TryParse(txtAdjustedReceiptQTY.Text, out Total);
                txtAdjustedReceiptQTY.Text = Total.ToString();

                double TotalBale = 0;
                double.TryParse(txtAdjustedReceiptQTYUnit.Text, out TotalBale);

                if (double.Parse(txtAdjustedReceiptQTY.Text) <= double.Parse(lblAdjustRemQty.Text))
                {
                    lblReceiptAdjustmentError.Text = string.Empty;
                    FinalTotal = FinalTotal + Total;
                    TOTAL_NO_OF_UNIT = TOTAL_NO_OF_UNIT + TotalBale;

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
            GetWaightedAvarageRate(out TotalQTY, out TotalUnit, out UnitUOM, out TotalWeight);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" +Math.Round(TotalQTY,3) + "','" + TextBoxId + "','" + txtQtyUnit + "','" + TotalUnit + "','" + txtQtyWeight + "','" + Math.Round(TotalWeight,3)+ "')", true);
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
                Label lblIssueNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblIssueNo");
               
                Label lblAdjustReceiptNumber = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAdjustReceiptNumber");
                Label lblTRN_TYPE = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblTRN_TYPE");  
                TextBox txtAdjustedReceiptQTY = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTY");
                double Adjustqty = double.Parse(txtAdjustedReceiptQTY.Text.Trim());
                string ItemCode = lblAdjustItemReceiptCode.Text.Trim();
                Label lblLotNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblLotNo");
                Label lblPalletNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblPalletNo");
                Label lblPalletCode = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblPalletCode");            

                TextBox txtAdjustedReceiptQTYUnit = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYUnit");
                TextBox txtAdjustedReceiptQTYUOM = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYUOM");
                TextBox txtAdjustedReceiptQTYWeight = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYWeight");
                double UNIT_NO = 0;
                double UnitWeight = 0;
                string UNITUOM = string.Empty;
                double.TryParse(txtAdjustedReceiptQTYUnit.Text, out UNIT_NO);
                UNITUOM = txtAdjustedReceiptQTYUOM.Text;
                double.TryParse(txtAdjustedReceiptQTYWeight.Text, out UnitWeight);
            

                DataView dv = new DataView(dtReceiptAdjustment);
                dv.RowFilter = " FIBER_CODE='" + ItemCode + "' and ISS_TRN_NUMB=" + lblIssueNo.Text + " and TRN_NUMB=" + lblAdjustReceiptNumber.Text + " and LOT_NO='" + lblLotNo.Text  + "' and PALLET_CODE='" + lblPalletCode.Text + "' and PALLET_NO='" + lblPalletNo.Text + "'";

                DataView dvDup = new DataView(dtItemReceipt);
                if (dtItemReceipt.Rows.Count > 0)
                {
                    dvDup.RowFilter = "COMP_CODE='" + dv[0]["COMP_CODE"] + "' and BRANCH_CODE='" + dv[0]["BRANCH_CODE"] + "' and TRN_TYPE='" + lblTRN_TYPE.Text + "' and TRN_NUMB='" + lblAdjustReceiptNumber.Text + "' and ISS_TRN_NUMB=" + lblIssueNo.Text + "  AND FIBER_CODE='" + ItemCode + "' and YEAR='" + dv[0]["YEAR"].ToString() + "' and LOT_NO='" + lblLotNo.Text + "' and PALLET_CODE='" + lblPalletCode.Text + "' and PALLET_NO='" + lblPalletNo.Text + "'";

                }
                if (dvDup.Count > 0)
                {
                    dvDup[0]["NO_OF_UNIT"] = UNIT_NO;
                    dvDup[0]["UOM_OF_UNIT"] = UNITUOM;
                    dvDup[0]["WEIGHT_OF_UNIT"] = UnitWeight;
                    dvDup[0]["ISSUE_QTY"] = Adjustqty;
                    dvDup[0]["PI_NO"] = lblLotNo.Text ;
                    dvDup[0]["ISS_PI_NO"] = lblLotNo.Text ;
                    dtItemReceipt.AcceptChanges();
                }
                else if (Adjustqty > 0)
                {
                    int Year = int.Parse(dv[0]["YEAR"].ToString());
                    string Comp_code = dv[0]["COMP_CODE"].ToString();
                    string Branch_code = dv[0]["BRANCH_CODE"].ToString();
                    string PO_Comp = dv[0]["PO_COMP"].ToString();
                    string PO_Branch = dv[0]["PO_BRANCH"].ToString();
                    string PO_Type = dv[0]["PO_TYPE"].ToString();
                    int PO_numb = 0;
                    if (dv[0]["PO_NUMB"].ToString() != string.Empty)
                    {
                        PO_numb = Convert.ToInt32(dv[0]["PO_NUMB"].ToString());
                    }                
                     
                    DataRow dr = dtItemReceipt.NewRow();
                    dr["ISS_YEAR"] = int.Parse(dv[0]["ISS_YEAR"].ToString());
                    dr["ISS_COMP"] = dv[0]["ISS_COMP"];
                    dr["ISS_BRANCH"] = dv[0]["ISS_BRANCH"];
                    dr["ISS_TRN_TYPE"] = lblIssueNo.ToolTip  ;
                    dr["ISS_TRN_NUMB"] = lblIssueNo.Text;


                    dr["YEAR"] = Year;
                    dr["COMP_CODE"] = Comp_code;
                    dr["BRANCH_CODE"] = Branch_code;
                    dr["TRN_TYPE"] = lblTRN_TYPE.Text ;
                    dr["TRN_NUMB"] = int.Parse(lblAdjustReceiptNumber.Text );
                    dr["PO_COMP"] = PO_Comp;
                    dr["PO_BRANCH"] = PO_Branch;
                    dr["PO_TYPE"] = PO_Type;
                    dr["PO_NUMB"] = PO_numb;
                    dr["PO_YEAR"] = dv[0]["PO_YEAR"];


                    dr["FIBER_CODE"] = ItemCode;                  
                    dr["ISSUE_QTY"] = Adjustqty;              
                    dr["LOT_NO"] = dv[0]["LOT_NO"].ToString();
                    dr["PALLET_CODE"] = dv[0]["PALLET_CODE"].ToString();
                    dr["PALLET_NO"] = dv[0]["PALLET_NO"].ToString();
                    dr["NO_OF_UNIT"] = UNIT_NO;
                    dr["UOM_OF_UNIT"] = UNITUOM;
                    dr["WEIGHT_OF_UNIT"] = UnitWeight;
                    dr["PI_NO"] = LOT_NUMBER ;
                    dr["ISS_PI_NO"] = LOT_NUMBER;

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
        dt.Columns.Add("ISS_YEAR", typeof(int));
        dt.Columns.Add("ISS_COMP", typeof(string));
        dt.Columns.Add("ISS_BRANCH", typeof(string));
        dt.Columns.Add("ISS_TRN_TYPE", typeof(string));
        dt.Columns.Add("ISS_TRN_NUMB", typeof(int));
        dt.Columns.Add("ISS_PO_COMP", typeof(string));
        dt.Columns.Add("ISS_PO_BRNCH", typeof(string));
        dt.Columns.Add("ISS_PO_YEAR", typeof(int));
        dt.Columns.Add("ISS_PO_TYPE", typeof(string));
        dt.Columns.Add("ISS_PO_NUMB", typeof(int));
        dt.Columns.Add("LOT_NO", typeof(string));
        dt.Columns.Add("FIBER_CODE", typeof(string));      
        dt.Columns.Add("PALLET_CODE", typeof(string));
        dt.Columns.Add("PALLET_NO", typeof(string));
        dt.Columns.Add("ISSUE_QTY", typeof(double));       
        dt.Columns.Add("NO_OF_UNIT", typeof(double));
        dt.Columns.Add("UOM_OF_UNIT", typeof(string));
        dt.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
        dt.Columns.Add("PI_NO", typeof(string));
        dt.Columns.Add("ISS_PI_NO", typeof(string));
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
            double TotalAverageWeight = 0;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            foreach (DataRow dr in dtItemReceipt.Rows)
            {
                double Qty = double.Parse(dr["ISSUE_QTY"].ToString());
                double QtyUnit = double.Parse(dr["NO_OF_UNIT"].ToString());
                UnitUOM = dr["UOM_OF_UNIT"].ToString();
                double QtyWeight = double.Parse(dr["WEIGHT_OF_UNIT"].ToString());                      
                string FIBER_CODE = dr["FIBER_CODE"].ToString();
                string LOT_NO = dr["LOT_NO"].ToString();             
                if (LOT_NO == lblPANO.Text && FIBER_CODE == lblAdjustItemReceiptCode.Text.Trim())
                {                 
                   
                    TotalAverageWeight = QtyWeight;
                    TotalQTY = TotalQTY + Qty;
                    TotalUnit = TotalUnit + QtyUnit;
                }
            }           
            TotalWeight = TotalAverageWeight;

           
            Session["dtItemReceipt"] = dtItemReceipt;
            return TotalWeight;
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
            TextBox txtAdjustedReceiptQTYUnit = (TextBox)row.FindControl("txtAdjustedReceiptQTYUnit");
            TextBox txtAdjustedReceiptQTY1 = (TextBox)row.FindControl("txtAdjustedReceiptQTY");
            txtAdjustedReceiptQTYUnit.Text = lblNoOfUnit.Text;
            txtAdjustedReceiptQTY1.Text = lblAdjustRemQty.Text;
            double TotalUnit = 0;
            double FinalQtya = 0;
            double MaxQty = 0;
            double total_no_unit = 0;
            FinalQtya = GetFinalTotalOfItemReceiptAdjustment(out TotalUnit, out total_no_unit);
            double.TryParse(lblMaxQty.Text, out MaxQty);
            if (MaxQty != 0)
            {
                //if (MaxQty < FinalQtya)
                //{

                //    txtFinalQTY.Text = FinalQtya.ToString();
                //    txtTotalQTYUnit.Text = total_no_unit.ToString();
                //    Common.CommonFuction.ShowMessage("total qty is being more than remaining required qty.");
                //}
                //else
                //{

                //    txtFinalQTY.Text = FinalQtya.ToString();
                //    txtTotalQTYUnit.Text = total_no_unit.ToString();
                //}

                //********************* THIS ALLWED FOR ACCEPT ALL QTY SENT FOR PRODUCTION***************//
                txtFinalQTY.Text = FinalQtya.ToString();
                txtTotalQTYUnit.Text = total_no_unit.ToString();
                //********************* THIS ALLWED FOR ACCEPT ALL QTY SENT FOR PRODUCTION***************//

            }
            else
            {

                txtFinalQTY.Text = FinalQtya.ToString();
            }
            txtTotalQTYUnit.Text = total_no_unit.ToString();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in adjusting all receipt quantity.\r\nsee error log for detail.");
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
            double total_no_unit = 0;
            FinalQtya = GetFinalTotalOfItemReceiptAdjustment(out TotalUnit, out total_no_unit);
            double.TryParse(lblMaxQty.Text, out MaxQty);
            if (MaxQty != 0)
            {
                //if (MaxQty < FinalQtya)
                //{
                //    txtFinalQTY.Text = "0";
                //    thisTextBox.Text = "0";
                //    Common.CommonFuction.ShowMessage("total qty can not be more than remaining invoiced qty.");
                //}
                //else
                //{
                //    txtFinalQTY.Text = FinalQtya.ToString();
                //}
                txtFinalQTY.Text = FinalQtya.ToString();
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


    protected void txtAdjustedReceiptQTYUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((TextBox)sender).NamingContainer);
            TextBox weighofUnit = (TextBox)row.FindControl("txtAdjustedReceiptQTYWeight");
            TextBox NoOfBale = (TextBox)row.FindControl("txtAdjustedReceiptQTYUnit");
            TextBox totalbaleWeight = (TextBox)row.FindControl("txtAdjustedReceiptQTY");
            Label txtFinalWeight = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTY");
            double _weighofUnit = 0;
            double _NoOfBale = 0;
            double _totalbaleWeight = 0;
            double.TryParse(weighofUnit.Text, out _weighofUnit);
            double.TryParse(NoOfBale.Text, out _NoOfBale);
            _totalbaleWeight = (_NoOfBale * _weighofUnit);
            totalbaleWeight.Text = _totalbaleWeight.ToString();




            TextBox thisTextBox = (TextBox)sender;
            Label txtFinalQTY = (Label)grdReceiptAdjustment.FooterRow.FindControl("txtTotalAdjustedReceiptQTYUnit");
            double TotalUnit = 0;

            double FinalQtya = 0;
            double MaxQty = 0;
            double total_no_unit = 0;
            FinalQtya = GetFinalTotalOfItemReceiptAdjustment(out TotalUnit, out total_no_unit);
            double.TryParse(lblMaxQty.Text, out MaxQty);
            if (MaxQty != 0)
            {
                //if (MaxQty < FinalQtya)
                //{
                //    txtFinalQTY.Text = "0";
                //    thisTextBox.Text = "0";
                //    Common.CommonFuction.ShowMessage("total qty can not be more than remaining invoiced qty.");
                //}
                //else
                //{
                //    txtFinalWeight.Text = FinalQtya.ToString();
                //    txtFinalQTY.Text = total_no_unit.ToString();
                //}
                txtFinalWeight.Text = FinalQtya.ToString();
                txtFinalQTY.Text = total_no_unit.ToString();
            }
            else
            {
                txtFinalWeight.Text = FinalQtya.ToString();
                txtFinalQTY.Text = total_no_unit.ToString();
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
