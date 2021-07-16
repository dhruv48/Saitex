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


public partial class Module_Fiber_Pages_Fiber_adjustment_pallet_return : System.Web.UI.Page
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail; 
    string TRN_TYPE = string.Empty;
    string TRN_NUMB = string.Empty;
    string PALLET_CODE=string.Empty;    
    string MERGE_NO=string.Empty;    
    private string TextBoxId = ""; 
    string RETURN_TRN_NUMB=string.Empty;    
    string RETURN_TRN_TYPE=string.Empty;   

    

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];


        if (Request.QueryString["TRN_NUMB"] != null)
        {
            TRN_NUMB = Request.QueryString["TRN_NUMB"].ToString();
        }

        if (Request.QueryString["TRN_TYPE"] != null)
        {
            TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
        }
        if (Request.QueryString["PALLET_CODE"] != null)
        {
            PALLET_CODE = Request.QueryString["PALLET_CODE"].ToString();
        }
        if (Request.QueryString["MERGE_NO"] != null)
        {
            MERGE_NO = Request.QueryString["MERGE_NO"].ToString();
        }       
        if (Request.QueryString["txtid"] != null)
        {
            TextBoxId = Request.QueryString["txtid"].ToString();
        }
        if (Request.QueryString["RETURN_TRN_NUMB"] != null)
        {
            RETURN_TRN_NUMB = Request.QueryString["RETURN_TRN_NUMB"].ToString();
        }
        if (Request.QueryString["RETURN_TRN_TYPE"] != null)
        {
            RETURN_TRN_TYPE = Request.QueryString["RETURN_TRN_TYPE"].ToString();
        }   
        if (!IsPostBack)
        {
            GetDataForItemAdjustment(PALLET_CODE);
        }
    }

    public void GetDataForItemAdjustment(string PALLET_CODE)
    {
        try
        {
            
            SaitexDM.Common.DataModel.UserLoginDetail oUserLogindetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtPalletReturn = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetAdjpalletReturn(oUserLogindetail.COMP_CODE, oUserLogindetail.CH_BRANCHCODE, TRN_TYPE, TRN_NUMB, PALLET_CODE, MERGE_NO,RETURN_TRN_NUMB,RETURN_TRN_TYPE,oUserLoginDetail.DT_STARTDATE.Year );
            if (dtPalletReturn != null && dtPalletReturn.Rows.Count > 0)
            {
                grdAdjpalletreturn.DataSource = dtPalletReturn;
                grdAdjpalletreturn.DataBind();        
            }
            else
            {
                lblAdjBomError.Text = "No Record exists for adjustment for provided Pallet";
            }
        }
        catch (Exception ex)
        {
            lblAdjBomError.Text = ex.Message;
        }
    }
    protected void btnAdjpalletreturnItem_Click(object sender, EventArgs e)
    {     
        double TotalQTY = 0;
        DataTable dtPalletReturn = createdatatableforadjustment(out TotalQTY);        
        if (dtPalletReturn != null && dtPalletReturn.Rows.Count > 0)
        {
            Session["dtPalletReturn"] = dtPalletReturn;
            grdAdjpalletreturn.DataSource = dtPalletReturn;
            grdAdjpalletreturn.DataBind();    
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "')", true);

        }
        else
        {
            lblAdjBomError.Text = "No Record exists for adjustment for provided Pallet";
        } 
    }

    public DataTable createdatatableforadjustment(out double TOTALQTY)
    {
        TOTALQTY = 0.0;
        try
        {
            DataTable dtPalletReturn = new DataTable();
            if (Session["dtPalletReturn"] == null)
            {
                dtPalletReturn = createAdjTable();
            }
            else
            {
                dtPalletReturn.Rows.Clear();
                dtPalletReturn = (DataTable)Session["dtPalletReturn"];
            }

            Label total = (Label)grdAdjpalletreturn.FooterRow.FindControl("txtTotalAdjQTY");          
            double.TryParse(total.Text, out TOTALQTY);

            for (int iLoop = 0; iLoop < grdAdjpalletreturn.Rows.Count; iLoop++)
            {
                Label lblMRN_NUMBER = (Label)grdAdjpalletreturn.Rows[iLoop].FindControl("lblMRN_NUMBER");
                Label lblMRN_CODE = (Label)grdAdjpalletreturn.Rows[iLoop].FindControl("lblMRN_CODE");
                Label lblMERGE_NO = (Label)grdAdjpalletreturn.Rows[iLoop].FindControl("lblMERGE_NO");
                Label lblPALLET_CODE = (Label)grdAdjpalletreturn.Rows[iLoop].FindControl("lblPALLET_CODE");
                Label lblPALLET_NUMBER = (Label)grdAdjpalletreturn.Rows[iLoop].FindControl("lblPALLET_NUMBER");
                CheckBox chkpalletnoApproved = (CheckBox)grdAdjpalletreturn.Rows[iLoop].FindControl("chkpalletnoApproved");     
                DataView dv = new DataView(dtPalletReturn);
                dv.RowFilter = "COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and TRN_NUMB ='" + lblMRN_NUMBER.Text + "' and TRN_TYPE ='" + lblMRN_CODE.Text + "' and PALLET_CODE ='" + lblPALLET_NUMBER.Text + "' and PALLET_NO ='" + lblPALLET_NUMBER.Text + "' and MERGE_NO ='" + lblMERGE_NO.Text + "'";

                if (chkpalletnoApproved.Checked)
                {
                    if (dv.Count == 0)
                    {

                        DataRow dr = dtPalletReturn.NewRow();
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] =oUserLoginDetail.DT_STARTDATE.Year;
                        dr["TRN_NUMB"] = lblMRN_NUMBER.Text;
                        dr["TRN_TYPE"] = lblMRN_CODE.Text;
                        dr["MERGE_NO"] = lblMERGE_NO.Text;
                        dr["PALLET_CODE"] = lblPALLET_CODE.Text;
                        dr["PALLET_NO"] = lblPALLET_NUMBER.Text;
                        dr["IS_RETURNED"] = "1";
                        dtPalletReturn.Rows.Add(dr);
                    }
                    else
                    {
                        dv[0]["IS_RETURNED"] = "1";
                    }
                }
                dtPalletReturn.AcceptChanges();
            }



            return dtPalletReturn;
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

   
  
    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("COMP_CODE", typeof(string));
        dt.Columns.Add("BRANCH_CODE", typeof(string));
        dt.Columns.Add("TRN_NUMB", typeof(string));
        dt.Columns.Add("TRN_TYPE", typeof(string));
        dt.Columns.Add("MERGE_NO", typeof(string));
        dt.Columns.Add("PALLET_CODE", typeof(string));
        dt.Columns.Add("PALLET_NO", typeof(string));        
        dt.Columns.Add("IS_RETURNED", typeof(string));
        dt.Columns.Add("YEAR", typeof(int));          
        return dt;
    }



    protected void chkpalletnoApproved_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox thisTextBox = (CheckBox)sender;
            Label txtTotalAdjQTY = (Label)grdAdjpalletreturn.FooterRow.FindControl("txtTotalAdjQTY");
            int total = 0;
            var FinalQty = GetFinalTotalOfItemReceiptAdjustment(out total);
            if (FinalQty != 0)
            {
                txtTotalAdjQTY.Text = FinalQty.ToString();
            }
            else
            {
                txtTotalAdjQTY.Text = "0";            
            }            
        }
        catch (Exception ex)
        {
            
        }
    }



    private double GetFinalTotalOfItemReceiptAdjustment(out int checkedCheckBox)
    {
        try
        {
            checkedCheckBox = 0;   
            for (int iLoop = 0; iLoop < grdAdjpalletreturn.Rows.Count; iLoop++)
            {
                CheckBox chkApproved = (CheckBox)grdAdjpalletreturn.Rows[iLoop].FindControl("chkpalletnoApproved");
                if (chkApproved.Checked)
                {
                    checkedCheckBox = checkedCheckBox + 1;                
                }               
            }
            return checkedCheckBox;
        }
        catch
        {
            throw;
        }
    }

 

    protected void grdAdjpalletreturn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView drv = e.Row.DataItem as DataRowView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkpalletnoApproved = (CheckBox)e.Row.FindControl("chkpalletnoApproved");
            Label lblPalletNoApproved = (Label)e.Row.FindControl("lblPalletNoApproved");           
            chkpalletnoApproved.Checked = (lblPalletNoApproved.Text == "1") ? true : false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label txtTotalAdjQTY = (Label)e.Row.FindControl("txtTotalAdjQTY");
            int total = 0;
            var FinalQty = GetFinalTotalOfItemReceiptAdjustment(out total);
            if (FinalQty != 0)
            {
                txtTotalAdjQTY.Text = FinalQty.ToString();
            }
            else
            {
                txtTotalAdjQTY.Text = "0";
            } 
        
        }

    }
}
