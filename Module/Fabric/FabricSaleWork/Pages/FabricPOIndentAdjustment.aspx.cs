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

public partial class Module_Inventory_Pages_FabricPOIndentAdjustment : System.Web.UI.Page
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
            if (Request.QueryString["FabricCodeId"] != null)
            {
                string FabricCode = Request.QueryString["FabricCodeId"].ToString();
                GetDataForFabricAdjustment(FabricCode);
            }
            if (Request.QueryString["TextBoxId"] != null)
            {
                TextBoxId = Request.QueryString["TextBoxId"].ToString();
            }
        }
    }
    private void GetDataForFabricAdjustment(string FabricCode)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtIndentAdjustment = null;// SaitexBL.Interface.Method.Fabric_Purchase_Order.GetAdjustIndentByFabricCode(oUserLoginDetail.DT_STARTDATE.Year, FabricCode, PO_TYPE, PONum, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dtIndentAdjustment != null && dtIndentAdjustment.Rows.Count > 0)
            {
                grdIndentAdjustment.DataSource = dtIndentAdjustment;
                grdIndentAdjustment.DataBind();
                lblAdjustFabricCode.Text = FabricCode;
            }
            else
            {
                lblFabricIndentAdjustmentError.Text = "No Record exists for adjustment for provided Fabric";
            }
        }
        catch (Exception ex)
        {
            lblFabricIndentAdjustmentError.Text = ex.Message;
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
                    lblFabricIndentAdjustmentError.Text = "Entered quantity is larger then indent quantity";
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
            DataTable dtFabricIndent = createdatatableforadjustment(out TotalQTY);
            Session["dtFabricIndent"] = dtFabricIndent;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "')", true);
        }
        catch (Exception ex)
        {
            lblFabricIndentAdjustmentError.Text = ex.Message;
        }
    }
    private DataTable createdatatableforadjustment(out double TotalQTY)
    {
        try
        {
            TotalQTY = 0;
            DataTable dtFabricIndent = new DataTable();
            if (Session["dtFabricIndent"] == null)
                dtFabricIndent = createAdjTable();
            else
            {
                dtFabricIndent.Rows.Clear();
                dtFabricIndent = (DataTable)Session["dtFabricIndent"];
                if (!dtFabricIndent.Columns.Contains("ADJUST_QTY"))
                {
                    dtFabricIndent.Columns.Add("ADJUST_QTY", typeof(int));
                }
            }
            for (int iLoop = 0; iLoop < grdIndentAdjustment.Rows.Count; iLoop++)
            {
                Label lblAdjustIndentNumber = (Label)grdIndentAdjustment.Rows[iLoop].FindControl("lblAdjustIndentNumber");
                string IndentNumber = lblAdjustIndentNumber.Text.Trim();
                TextBox txtAdjustedIndentQTY = (TextBox)grdIndentAdjustment.Rows[iLoop].FindControl("txtAdjustedIndentQTY");
                double ADJUST_QTY = double.Parse(txtAdjustedIndentQTY.Text.Trim());
                string FabricCode = lblAdjustFabricCode.Text.Trim();
                Label lblAPPR_QTY = (Label)grdIndentAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
                double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());
                string Indent_Type = lblAPPR_QTY.ToolTip.Trim();
                DataView dv = new DataView(dtFabricIndent);
                dv.RowFilter = " FABR_CODE='" + FabricCode + "' and IND_NUMB=" + IndentNumber;
                if (dv.Count == 0)
                {
                    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                    DataRow dr = dtFabricIndent.NewRow();
                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                    dr["IND_NUMB"] = IndentNumber;
                    dr["FABR_CODE"] = FabricCode;
                    dr["ADJUST_QTY"] = ADJUST_QTY;
                    dr["APPR_QTY"] = APPR_QTY;
                    dr["IND_TYPE"] = Indent_Type;
                    dtFabricIndent.Rows.Add(dr);
                }
                else
                {
                    dv[0]["ADJUST_QTY"] = ADJUST_QTY;
                    dv[0]["APPR_QTY"] = APPR_QTY;
                }
                dtFabricIndent.AcceptChanges();
                TotalQTY = TotalQTY + ADJUST_QTY;
            }
            return dtFabricIndent;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("YEAR", typeof(int));
        dt.Columns.Add("IND_NUMB", typeof(string));
        dt.Columns.Add("FABR_CODE", typeof(string));
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
            lblFabricIndentAdjustmentError.Text = ex.Message;
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
