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

using System.Data.OracleClient;
public partial class Inventory_POIndentAdjustment : System.Web.UI.Page
{
    private static string TextBoxId;
    private static int PONum;
    private static string PO_TYPE;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBoxId = "";
            PONum = 0;
            if (Request.QueryString["PONum"] != null)
            {
                PONum = int.Parse(Request.QueryString["PONum"].ToString());
            }
            if (Request.QueryString["PO_TYPE"] != null)
            {
                PO_TYPE = Request.QueryString["PO_TYPE"].ToString();
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
        }
    }
    private void GetDataForItemAdjustment(string ItemCode)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtIndentAdjustment = SaitexBL.Interface.Method.Material_Purchase_Order.GetAdjustIndentByItemCode(oUserLoginDetail.DT_STARTDATE.Year, ItemCode, PO_TYPE, PONum, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dtIndentAdjustment != null && dtIndentAdjustment.Rows.Count > 0)
            {

                grdIndentAdjustment.DataSource = dtIndentAdjustment;
                grdIndentAdjustment.DataBind();
                lblAdjustItemIndentCode.Text = ItemCode;
            }
            else
            {
                lblIndentAdjustmentError.Text = "No Record exists for adjustment for provided Item";
            }
        }
        catch (Exception ex)
        {
            lblIndentAdjustmentError.Text = ex.Message;
        }
    }

    private double GetFinalTotalOfItemIndentAdjustment()
    {
        try
        {
            double FinalTotal = 0;
            for (int iLoop = 0; iLoop < grdIndentAdjustment.Rows.Count; iLoop++)
            {
                TextBox txtAdjustedIndentQTY = (TextBox)grdIndentAdjustment.Rows[iLoop].FindControl("txtAdjustedIndentQTY");
                Label lblAdjustRemQty = (Label)grdIndentAdjustment.Rows[iLoop].FindControl("lblAdjustRemQty");
                double Total = double.Parse(txtAdjustedIndentQTY.Text);
                if (Total <= double.Parse(lblAdjustRemQty.Text))
                {
                    FinalTotal = FinalTotal + Total;
                }
                else
                {
                    lblIndentAdjustmentError.Text = "Entered quantity is larger then indent quantity";
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
    protected void btnAdjustIndentItem_Click(object sender, EventArgs e)
    {
        try
        {
            double TotalQTY = 0;
            DataTable dtItemIndent = createdatatableforadjustment(out TotalQTY);
            Session["dtItemIndent"] = dtItemIndent;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "')", true);
        }
        catch (Exception ex)
        {
            lblIndentAdjustmentError.Text = ex.Message;
        }
    }
    private DataTable createdatatableforadjustment(out double TotalQTY)
    {
        try
        {
            TotalQTY = 0;
            DataTable dtItemIndent = new DataTable();
            if (Session["dtItemIndent"] == null)
                dtItemIndent = createAdjTable();
            else
            {
                dtItemIndent.Rows.Clear();
                dtItemIndent = (DataTable)Session["dtItemIndent"];
                if (!dtItemIndent.Columns.Contains("ADJUST_QTY"))
                {
                    dtItemIndent.Columns.Add("ADJUST_QTY", typeof(int));
                }
                if (!dtItemIndent.Columns.Contains("IND_BRANCH_NAME"))
                {
                    dtItemIndent.Columns.Add("IND_BRANCH_NAME", typeof(string));
                }
            }
            for (int iLoop = 0; iLoop < grdIndentAdjustment.Rows.Count; iLoop++)
            {
                Label lblAdjustIndentYear = (Label)grdIndentAdjustment.Rows[iLoop].FindControl("lblAdjustIndentYear");
                int IND_YEAR = int.Parse(lblAdjustIndentYear.Text.Trim());

                Label lblAdjustIndentNuber = (Label)grdIndentAdjustment.Rows[iLoop].FindControl("lblAdjustIndentNuber");
                string IndentNumber = lblAdjustIndentNuber.Text.Trim();

                TextBox txtAdjustedIndentQTY = (TextBox)grdIndentAdjustment.Rows[iLoop].FindControl("txtAdjustedIndentQTY");
                double ADJUST_QTY = double.Parse(txtAdjustedIndentQTY.Text.Trim());

                string ItemCode = lblAdjustItemIndentCode.Text.Trim();

                Label lblBranch = (Label)grdIndentAdjustment.Rows[iLoop].FindControl("lblBranch");
                string BRANCH_NAME = lblBranch.Text.Trim();
                string BRANCH_CODE = lblBranch.ToolTip.Trim();

                Label lblAPPR_QTY = (Label)grdIndentAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
                double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());

                string Indent_Type = lblAPPR_QTY.ToolTip.Trim();

                DataView dv = new DataView(dtItemIndent);
                dv.RowFilter = "IND_YEAR=" + IND_YEAR + " and ITEM_CODE='" + ItemCode + "' and IND_NUMB=" + IndentNumber;
                if (dv.Count == 0)
                {
                    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                    DataRow dr = dtItemIndent.NewRow();
                    dr["IND_YEAR"] = IND_YEAR;
                    dr["IND_BRANCH_NAME"] = BRANCH_NAME;
                    dr["IND_BRANCH_CODE"] = BRANCH_CODE;
                    dr["IND_NUMB"] = IndentNumber;
                    dr["ITEM_CODE"] = ItemCode;
                    dr["ADJUST_QTY"] = ADJUST_QTY;
                    dr["APPR_QTY"] = APPR_QTY;
                    dr["IND_TYPE"] = Indent_Type;
                    dtItemIndent.Rows.Add(dr);
                }
                else
                {
                    dv[0]["ADJUST_QTY"] = ADJUST_QTY;
                    dv[0]["APPR_QTY"] = APPR_QTY;
                }
                dtItemIndent.AcceptChanges();
                TotalQTY = TotalQTY + ADJUST_QTY;
            }
            return dtItemIndent;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("IND_YEAR", typeof(int));
        dt.Columns.Add("IND_BRANCH_NAME", typeof(string));
        dt.Columns.Add("IND_BRANCH_CODE", typeof(string));
        dt.Columns.Add("IND_NUMB", typeof(string));
        dt.Columns.Add("ITEM_CODE", typeof(string));
        dt.Columns.Add("ADJUST_QTY", typeof(double));
        dt.Columns.Add("APPR_QTY", typeof(double));
        dt.Columns.Add("IND_TYPE", typeof(string));
        return dt;
    }

    protected void txtAdjustedIndentQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            Label txtFinalQTY = (Label)grdIndentAdjustment.FooterRow.FindControl("txtTotalAdjustedIndentQTY");
            txtFinalQTY.Text = GetFinalTotalOfItemIndentAdjustment().ToString();
        }
        catch (Exception ex)
        {
            lblIndentAdjustmentError.Text = ex.Message;
        }
    }

    protected void grdIndentAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblAdjustRemQty = (Label)row.FindControl("lblAdjustRemQty");
            TextBox txtAdjustedIndentQTY = (TextBox)row.FindControl("txtAdjustedIndentQTY");
            txtAdjustedIndentQTY.Text = lblAdjustRemQty.Text;
            Label txtFinalQTY = (Label)grdIndentAdjustment.FooterRow.FindControl("txtTotalAdjustedIndentQTY");
            txtFinalQTY.Text = GetFinalTotalOfItemIndentAdjustment().ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }
    }

}
