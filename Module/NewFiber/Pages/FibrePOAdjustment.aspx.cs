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
public partial class Module_Fiber_Pages_FibrePOAdjustment : System.Web.UI.Page
{
    private  int PONum;
    private  string PO_TYPE;
    private  string FiberCodeId;
    private  string TextBoxId;
    private string  FiberCode ;
    private string YEAR;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PONum"] != null)
        {
            PONum = int.Parse((Request.QueryString["PONum"]).ToString());
        }
        if (Request.QueryString["PO_TYPE"] != null)
        {
            PO_TYPE = Request.QueryString["PO_TYPE"];
        }
        if (Request.QueryString["TextBoxId"] != null)
        {
            TextBoxId = Request.QueryString["TextBoxId"];
        }
        if (Request.QueryString["ItemCodeId"] != null)
        {
             FiberCode = Request.QueryString["ItemCodeId"];
          
        }
        if (!IsPostBack)
        {
            getDataForFibreAdjustment(FiberCode);
        }

    }
   private void  getDataForFibreAdjustment(string FiberCode)
   {
       try
       {
           SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
           DataTable dtFibreAdjust = new DataTable();
           dtFibreAdjust = SaitexBL.Interface.Method.TX_FIBER_NEW_PO_CREDIT.GetAdjustIndentByItemCode(FiberCode, PO_TYPE, PONum, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
           if (dtFibreAdjust != null && dtFibreAdjust.Rows.Count > 0)
           {
               grdFibreIndentAdjustment.DataSource = dtFibreAdjust;
               grdFibreIndentAdjustment.DataBind();
               lblindentadjustment.Text = FiberCode;
           }
           else
           {
               lblFibreIndentAdjustmentError.Text = "No Record Found in the Grid";
           }
       }
       catch (Exception ex)
       {
           lblFibreIndentAdjustmentError.Text = ex.Message;
       }

   }
   protected void btnAdjustIndentItem_Click(object sender, EventArgs e)
   {
       try
       {
           double TotalQTY = 0;
           DataTable dtFibreAdjust = createdataTableforadjust( out TotalQTY);
           Session["dtFibreAdjust"] = dtFibreAdjust;
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "')", true);
       }
       catch (Exception exe)
       {
           lblFibreIndentAdjustmentError.Text = exe.Message;
       }



   }
   private DataTable createdataTableforadjust(out double TotalQTY)
   {
       try
       {
           TotalQTY = 0;
           DataTable dtFibreAdjust = new DataTable();
           if (Session["dtFibreAdjust"] == null)
               dtFibreAdjust = createAdjTable();
           else
           {
               dtFibreAdjust.Rows.Clear();
               dtFibreAdjust = (DataTable)Session["dtFibreAdjust"];
               if (!dtFibreAdjust.Columns.Contains("ADJUST_QTY"))
               {
                   dtFibreAdjust.Columns.Add("ADJUST_QTY", typeof(int));
               }
           }
           for (int iLoop = 0; iLoop < grdFibreIndentAdjustment.Rows.Count; iLoop++)
           {
               Label lblAdjustIndentNumber = (Label)grdFibreIndentAdjustment.Rows[iLoop].FindControl("lblAdjustIndentNumber");
               string IndentNumber = lblAdjustIndentNumber.Text.Trim();

               TextBox txtAdjustedIndentQTY = (TextBox)grdFibreIndentAdjustment.Rows[iLoop].FindControl("txtAdjustedIndentQTY");
               double ADJUST_QTY = double.Parse(txtAdjustedIndentQTY.Text.Trim());

               string ItemCode = lblindentadjustment.Text.Trim();


               Label lblBranch = (Label)grdFibreIndentAdjustment.Rows[iLoop].FindControl("lblBranch");
               string BRANCH_NAME = lblBranch.Text.Trim();
               string BRANCH_CODE = lblBranch.ToolTip.Trim();

               Label lblAPPR_QTY = (Label)grdFibreIndentAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
               double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());

               string Indent_Type = lblAPPR_QTY.ToolTip.Trim();

               DataView dv = new DataView(dtFibreAdjust);
               dv.RowFilter = " FIBER_CODE='" + ItemCode + "' and IND_NUMB=" + IndentNumber;
               if (dv.Count == 0)
               {
                   DataRow dr = dtFibreAdjust.NewRow();
                   dr["IND_NUMB"] = IndentNumber;
                   dr["IND_BRANCH_NAME"] = BRANCH_NAME;
                   dr["IND_BRANCH_CODE"] = BRANCH_CODE;
                   dr["FIBER_CODE"] = ItemCode;
                   
                   dr["ADJUST_QTY"] = ADJUST_QTY;
                   dr["APPR_QTY"] = APPR_QTY;
                   dr["IND_TYPE"] = Indent_Type;
                   //dr["YEAR"] = YEAR;
                   dtFibreAdjust.Rows.Add(dr);
               }
               else
               {
                   dv[0]["ADJUST_QTY"] = ADJUST_QTY;
                   dv[0]["APPR_QTY"] = APPR_QTY;
               }
               dtFibreAdjust.AcceptChanges();
               TotalQTY = TotalQTY + ADJUST_QTY;
           }
           return dtFibreAdjust;
       }
       catch (Exception ex)
       {
           throw ex;
       }
 
   }
   protected void txtAdjustedIndentQTY_TextChanged1(object sender, EventArgs e)
   {
       try
       {
           Label txtFinalQTY = (Label)grdFibreIndentAdjustment.FooterRow.FindControl("txtTotalAdjustedIndentQTY");
           txtFinalQTY.Text = GetFinalTotalOfFibreIndentAdjustment().ToString();

       }
       catch (Exception ex)
       {

           lblFibreIndentAdjustmentError.Text = ex.Message;
       }
   }
   private double GetFinalTotalOfFibreIndentAdjustment()
   {
       try
       {
           double FinalTotal = 0;
           for (int iLoop = 0; iLoop <grdFibreIndentAdjustment.Rows.Count;iLoop++) 
              
           {
               TextBox txtAdjustedIndentQTY = (TextBox)grdFibreIndentAdjustment.Rows[iLoop].FindControl("txtAdjustedIndentQTY");
               Label lblAdjustRemQty = (Label)grdFibreIndentAdjustment.Rows[iLoop].FindControl("lblAdjustRemQty");
               double Total = double.Parse(txtAdjustedIndentQTY.Text);
               if (Total <= double.Parse(lblAdjustRemQty.Text))
               {
                   FinalTotal = FinalTotal + Total;
               }
               else
               {
                   lblFibreIndentAdjustmentError.Text = "Entered quantity is larger then indent quantity";
                   txtAdjustedIndentQTY.Text = 0.ToString();
                   break;
               }
           }
           return FinalTotal;
       }
       catch (Exception ex)
       {
           throw ex;
       }
   }
   
             
          
        private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("IND_NUMB", typeof(string));
        dt.Columns.Add("FIBER_CODE", typeof(string));
       
        dt.Columns.Add("ADJUST_QTY", typeof(double));
        dt.Columns.Add("APPR_QTY", typeof(double));
        dt.Columns.Add("IND_TYPE", typeof(string));
        dt.Columns.Add("IND_BRANCH_NAME", typeof(string));
        dt.Columns.Add("IND_BRANCH_CODE", typeof(string));
        dt.Columns.Add("YEAR", typeof(string));
        return dt;
    }
        protected void grdFibreIndentAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblAdjustRemQty = (Label)row.FindControl("lblAdjustRemQty");
                TextBox txtAdjustedIndentQTY = (TextBox)row.FindControl("txtAdjustedIndentQTY");
                txtAdjustedIndentQTY.Text = lblAdjustRemQty.Text;
                Label txtFinalQTY = (Label)grdFibreIndentAdjustment.FooterRow.FindControl("txtTotalAdjustedIndentQTY");
                txtFinalQTY.Text = GetFinalTotalOfFibreIndentAdjustment().ToString();
            }
            catch (Exception ex)
            {
                errorLog.ErrHandler.WriteError(ex.Message);

            }
        }
 
   }

