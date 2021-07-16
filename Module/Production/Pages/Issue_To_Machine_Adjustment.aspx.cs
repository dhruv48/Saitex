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

public partial class Module_Production_Pages_Issue_To_Machine_Adjustment : System.Web.UI.Page
{
    private int TRN_NUMB;
    public string TRN_TYPE;
    string PA_NO = string.Empty;
    string LOT_NUMBER = string.Empty;
    DateTime TRN_DATE = DateTime.Now;
    string DOFF_LOT_NUMBER = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail ;
   
    string IS_GAIN = string.Empty;
    string GAIN_QTY = string.Empty;
    string IS_GAIN_ID = string.Empty;
    string GAIN_QTY_ID = string.Empty;   
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Request.QueryString["TRN_TYPE"] != null)
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
            }

            if (Request.QueryString["PI_NO"] != null)
            {
                lblPaHeding.Text = "For Pa No: ";
                lblPANO.Text = PA_NO = Request.QueryString["PI_NO"].ToString();
            }

            if (Request.QueryString["ChallanNo"] != null)
            {

                TRN_NUMB = int.Parse(Request.QueryString["ChallanNo"].ToString());
            }
            if (Request.QueryString["LOT_NUMBER"] != null)
            {

                LOT_NUMBER = Request.QueryString["LOT_NUMBER"].ToString();
            }
            if (Request.QueryString["DOFF_LOT_NUMBER"] != null)
            {

                DOFF_LOT_NUMBER = Request.QueryString["DOFF_LOT_NUMBER"].ToString();
            }
            if (Request.QueryString["TRN_DATE"] != null)
            {
                TRN_DATE = DateTime.Parse(Request.QueryString["TRN_DATE"].ToString());
            }

           
            if (Request.QueryString["IS_GAIN_ID"] != null)
            {
                IS_GAIN_ID = Request.QueryString["IS_GAIN_ID"].ToString();
            }
            if (Request.QueryString["GAIN_QTY_ID"] != null)
            {
                GAIN_QTY_ID = Request.QueryString["GAIN_QTY_ID"].ToString();
            }
            if (Request.QueryString["IS_GAIN"] != null)
            {
                IS_GAIN = Request.QueryString["IS_GAIN"].ToString();

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
               
            if (!IsPostBack)
            {
              
                if (Request.QueryString["GAIN_QTY"] != null)
                {
                    txtGainQty.Text = Request.QueryString["GAIN_QTY"].ToString();
                }
                if (IS_GAIN.Equals("1"))
                {
                    txtGainQty.Visible = true;
                    lblProcessGain.Visible = true;
                    
                }
                else
                {
                    IS_GAIN = "0";
                    txtGainQty.Visible = false;
                    lblProcessGain.Visible = false;
                }
                
                if (Request.QueryString["ItemCodeId"] != null)
                {
                    string ItemCode = lblAdjustItemReceiptCode.Text = Request.QueryString["ItemCodeId"].ToString();
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
            DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForProductionIssueAdjustment(TRN_NUMB, TRN_TYPE, LOT_NUMBER, ItemCode, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, TRN_DATE);
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
                CalculateAllData();
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


                Label weighofUnit = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYWeight");
              
            


                double Total = 0;
                double copsweight = 0;
                double TotalBale = 0;
                double.TryParse(weighofUnit.Text, out copsweight);

                 double.TryParse(txtAdjustedReceiptQTY.Text, out Total);
                 txtAdjustedReceiptQTY.Text = Total.ToString();
                  txtAdjustedReceiptQTYUnit.Text = Math.Round(Total / copsweight, 3).ToString();
                  TotalBale = Math.Round(Total / copsweight, 3);
                //double.TryParse(txtAdjustedReceiptQTYUnit.Text, out TotalBale);

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
            
            DataTable dtItemReceipt = createdatatableforadjustment();
            Session["dtItemReceipt"] = dtItemReceipt;

            if (IS_GAIN.Equals("1"))
            {
                if (string.IsNullOrEmpty(txtGainQty.Text))
                {
                    Common.CommonFuction.ShowMessage(@"Please enter process gain qty.");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue1('" + GAIN_QTY_ID + "','" + txtGainQty.Text + "')", true);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue()", true);
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
           
            DataTable dtReceiptAdjustment = (DataTable)ViewState["dtReceiptAdjustment"];
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
                Label lblMachineIssueNumber = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblMachineIssueNumber");
                string ReceiptNumber = lblMachineIssueNumber.Text.Trim();

                TextBox txtAdjustedReceiptQTY = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTY");
                double Adjustqty = double.Parse(txtAdjustedReceiptQTY.Text.Trim());

                string ItemCode = lblAdjustItemReceiptCode.Text.Trim();
                
                //Label lblAPPR_QTY = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
                //double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());

                string Receipt_Type = lblMachineIssueNumber.ToolTip.Trim();

                Label lblLotNo = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("lblLotNo");
                 string lotno = lblLotNo.Text.Trim();

                TextBox txtAdjustedReceiptQTYUnit = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYUnit");
                TextBox txtAdjustedReceiptQTYUOM = (TextBox)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYUOM");
                Label txtAdjustedReceiptQTYWeight = (Label)grdReceiptAdjustment.Rows[iLoop].FindControl("txtAdjustedReceiptQTYWeight");
                double UNIT_NO = 0;
                double UnitWeight = 0;
                string UNITUOM = string.Empty;
                double.TryParse(txtAdjustedReceiptQTYUnit.Text, out UNIT_NO);
               
                double.TryParse(txtAdjustedReceiptQTYWeight.Text, out UnitWeight);
                string ISS_PI_NO = lblPANO.Text.Trim() != string.Empty ? lblPANO.Text.Trim() : "NA";

                DataView dv = new DataView(dtReceiptAdjustment);
                dv.RowFilter = "  PROS_ID_NO=" + ReceiptNumber + " and LOT_NUMBER='" + lotno + "' ";

                DataView dvDup = new DataView(dtItemReceipt);
                if (dtItemReceipt.Rows.Count > 0)
                {
                    dvDup.RowFilter = "COMP_CODE='" + dv[0]["COMP_CODE"] + "' and BRANCH_CODE='" + dv[0]["BRANCH_CODE"] + "' and TRN_TYPE='" + Receipt_Type + "' and PROD_PROS_ID_NO='" + ReceiptNumber + "'  and YEAR='" + dv[0]["YEAR"].ToString() + "' and LOT_NUMBER='" + lotno + "' ";

                }
                if (dvDup.Count > 0)
                {
                    dvDup[0]["NO_OF_UNIT"] = UNIT_NO;                  
                    dvDup[0]["WEIGHT_OF_UNIT"] = UnitWeight;
                    dvDup[0]["ISSUE_QTY"] = Adjustqty;
                    dvDup[0]["PI_NO"] = PA_NO;
                    dtItemReceipt.AcceptChanges();
                }
                else if (Adjustqty > 0)
                {
                    int Year = int.Parse(dv[0]["YEAR"].ToString());
                    string Comp_code = dv[0]["COMP_CODE"].ToString();
                    string Branch_code = dv[0]["BRANCH_CODE"].ToString();
                    
                    DataRow dr = dtItemReceipt.NewRow();
                    dr["YEAR"] = Year;
                    dr["COMP_CODE"] = Comp_code;
                    dr["BRANCH_CODE"] = Branch_code;
                    dr["TRN_TYPE"] = Receipt_Type;
                    dr["PROD_PROS_ID_NO"] = int.Parse(ReceiptNumber);

                    dr["PROS_CODE"] = dv[0]["PROS_CODE"].ToString();
                    dr["DOFF_LOT_NUMBER"] = DOFF_LOT_NUMBER;
                    dr["ARTICLE_CODE"] = ItemCode;
                    
                    dr["ISSUE_QTY"] = Adjustqty;
                    dr["LOT_NUMBER"] = dv[0]["LOT_NUMBER"].ToString();
                    dr["NO_OF_UNIT"] = UNIT_NO;                  
                    dr["WEIGHT_OF_UNIT"] = UnitWeight;
                    dr["PI_NO"] = PA_NO ;

                    //dr["DOFF_YEAR"] = Year;
                    //dr["DOFF_COMP_CODE"] = Comp_code;
                    //dr["DOFF_BRANCH_CODE"] = Branch_code;
                    //dr["DOFF_TRN_TYPE"] = Receipt_Type;
                    //dr["DOFF_PROS_ID_NO"] = int.Parse(ReceiptNumber);
                    //dr["DOFF_PROS_CODE"] = Receipt_Type;
                   

                   
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
        dt.Columns.Add("DOFF_YEAR", typeof(int));
        dt.Columns.Add("DOFF_COMP_CODE", typeof(string));
        dt.Columns.Add("DOFF_BRANCH_CODE", typeof(string));
        dt.Columns.Add("DOFF_TRN_TYPE", typeof(string));
        dt.Columns.Add("DOFF_PROS_ID_NO", typeof(int));
        dt.Columns.Add("DOFF_PROS_CODE", typeof(string));
        dt.Columns.Add("DOFF_LOT_NUMBER", typeof(string));
        dt.Columns.Add("YEAR", typeof(int));
        dt.Columns.Add("COMP_CODE", typeof(string));
        dt.Columns.Add("BRANCH_CODE", typeof(string));
        dt.Columns.Add("TRN_TYPE", typeof(string));
        dt.Columns.Add("PROD_PROS_ID_NO", typeof(int));
        dt.Columns.Add("PROS_CODE", typeof(string));
        dt.Columns.Add("LOT_NUMBER", typeof(string));
        dt.Columns.Add("ARTICLE_CODE", typeof(string));
        dt.Columns.Add("ISSUE_QTY", typeof(double));  
        dt.Columns.Add("NO_OF_UNIT", typeof(double));   
        dt.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
        dt.Columns.Add("PI_NO", typeof(string));
      
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
             
                //string pi_no = dr["LOT_NUMBER"].ToString();
                //string labelPI = lblPANO.Text.Trim() != string.Empty ? lblPANO.Text.Trim() : "NA";
                //if (pi_no == labelPI && FIBER_CODE == lblAdjustItemReceiptCode.Text.Trim())
                //{
                    TotalAverageWeight = QtyWeight;
                    TotalQTY = TotalQTY + Qty;
                    TotalUnit = TotalUnit + QtyUnit;
               // }
            }
            double FinalWeightedRate = TotalWeightedRate / TotalQTY;
            TotalWeight = TotalAverageWeight;
            
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
                if (Math.Round(MaxQty,3) < Math.Round(FinalQtya,3))
                {

                    txtFinalQTY.Text = "0";
                    txtTotalQTYUnit.Text = "0";
                    txtAdjustedReceiptQTYUnit.Text = "0";
                    txtAdjustedReceiptQTY1.Text = "0";
                    Common.CommonFuction.ShowMessage("total qty is being more than production qty.");
                }
                else
                {

                    txtFinalQTY.Text = FinalQtya.ToString();
                    txtTotalQTYUnit.Text = total_no_unit.ToString();
                }

               
            }
            else
            {

                //txtFinalQTY.Text = FinalQtya.ToString();
                //txtTotalQTYUnit.Text = total_no_unit.ToString();
            }
            if (IS_GAIN.Equals("1"))
            {

                txtGainQty.Text = (MaxQty - FinalQtya).ToString();
            }

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
                if (Math.Round(MaxQty, 3) < Math.Round(FinalQtya, 3))
                {
                    txtFinalQTY.Text = "0";
                    thisTextBox.Text = "0";
                    Common.CommonFuction.ShowMessage("total qty can not be more than production qty.");
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
            if (IS_GAIN.Equals("1"))
            {

                txtGainQty.Text = (MaxQty - FinalQtya).ToString();
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
            Label weighofUnit = (Label)row.FindControl("txtAdjustedReceiptQTYWeight");
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
                if (Math.Round(MaxQty, 3) < Math.Round(FinalQtya, 3))
                {
                    txtFinalQTY.Text = "0";
                    thisTextBox.Text = "0";
                    Common.CommonFuction.ShowMessage("total qty can not be more than production qty.");
                }
                else
                {
                    txtFinalWeight.Text = FinalQtya.ToString();
                    txtFinalQTY.Text = total_no_unit.ToString();
                }
                
            }
            else
            {
                txtFinalWeight.Text = FinalQtya.ToString();
                txtFinalQTY.Text = total_no_unit.ToString();
            }

            if (IS_GAIN.Equals("1"))
            {

                txtGainQty.Text = (MaxQty - FinalQtya).ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            lblReceiptAdjustmentError.Text = ex.Message;
            Common.CommonFuction.ShowMessage(@"Problem in Quantity adjustment.\r\nsee error log for detail.");
        }
    }


    protected void CalculateAllData()
    {
        if (grdReceiptAdjustment.Rows.Count > 0)
        {
          
            double TotalRemQty = 0;

            for (int i = 0; i < grdReceiptAdjustment.Rows.Count; i++)
            {
                double _RemQty = 0;
                Label lblAdjustRemQty = grdReceiptAdjustment.Rows[i].FindControl("lblAdjustRemQty") as Label;
                double.TryParse(lblAdjustRemQty.Text, out _RemQty);
                TotalRemQty = TotalRemQty + _RemQty;       
            }
            ((Label)grdReceiptAdjustment.FooterRow.FindControl("lblTotalAdjustRemQty")).Text = Math.Round(TotalRemQty, 3).ToString();
           
        }
    }


    
}
