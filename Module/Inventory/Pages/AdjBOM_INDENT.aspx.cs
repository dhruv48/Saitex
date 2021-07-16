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

public partial class Module_Inventory_Pages_AdjBOM_INDENT : System.Web.UI.Page
{
    private static string TextBoxId;
    private static int IND_NUMB;
    private static string IND_TYPE;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            TextBoxId = "";
            IND_NUMB = 0;
            if (Request.QueryString["IND_NUMB"] != null)
            {
                IND_NUMB = int.Parse(Request.QueryString["IND_NUMB"].ToString());
            }
            if (Request.QueryString["IND_TYPE"] != null)
            {
                IND_TYPE = Request.QueryString["IND_TYPE"].ToString();
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
            DataTable dtAdjBOM = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetAdjBOMByItemCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, IND_NUMB, ItemCode);
            if (dtAdjBOM != null && dtAdjBOM.Rows.Count > 0)
            {
                grdAdjBOM.DataSource = dtAdjBOM;
                grdAdjBOM.DataBind();
                lblAdjBOMItemCode.Text = ItemCode;
            }
            else
            {
                lblAdjBOMError.Text = "No Record exists for adjustment for provided Item";
            }
        }
        catch (Exception ex)
        {
            lblAdjBOMError.Text = ex.Message;
        }
    }

    protected void txtAdjustedBOMQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            Label txtFinalQTY = (Label)grdAdjBOM.FooterRow.FindControl("txtTotalAdjBOMQTY");
            txtFinalQTY.Text = GetFinalTotalOfItemAdjBOM().ToString();
        }
        catch (Exception ex)
        {
            lblAdjBOMError.Text = ex.Message;
        }
    }

    private double GetFinalTotalOfItemAdjBOM()
    {
        try
        {
            double FinalTotal = 0;
            for (int iLoop = 0; iLoop < grdAdjBOM.Rows.Count; iLoop++)
            {
                TextBox txtAdjBOMQTY = (TextBox)grdAdjBOM.Rows[iLoop].FindControl("txtAdjBOMQTY");
                Label lblAdjRemQty = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjRemQty");
                double Total = double.Parse(txtAdjBOMQTY.Text);
                if (Total <= double.Parse(lblAdjRemQty.Text))
                {
                    FinalTotal = FinalTotal + Total;
                }
                else
                {
                    lblAdjBOMError.Text = "Entered quantity is larger then indent quantity";
                    txtAdjBOMQTY.Text = 0.ToString();
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

    protected void grdAdjBOM_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblAdjRemQty = (Label)row.FindControl("lblAdjRemQty");
            TextBox txtAdjBOMQTY = (TextBox)row.FindControl("txtAdjBOMQTY");
            txtAdjBOMQTY.Text = lblAdjRemQty.Text;
            Label txtFinalQTY = (Label)grdAdjBOM.FooterRow.FindControl("txtTotalAdjBOMQTY");
            txtFinalQTY.Text = GetFinalTotalOfItemAdjBOM().ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }
    }

    protected void btnAdjBOMItem_Click(object sender, EventArgs e)
    {
        try
        {
            double TotalQTY = 0;
            DataTable dtBOMIndent = createdatatableforadjustment(out TotalQTY);
            Session["dtBOMIndent"] = dtBOMIndent;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + TextBoxId + "')", true);
        }
        catch (Exception ex)
        {
            lblAdjBOMError.Text = ex.Message;
        }
    }

    private DataTable createdatatableforadjustment(out double TotalQTY)
    {
        try
        {
            TotalQTY = 0;
            DataTable dtBOMIndent = new DataTable();
            if (Session["dtBOMIndent"] == null)
                dtBOMIndent = createAdjTable();
            else
            {
                dtBOMIndent.Rows.Clear();
                dtBOMIndent = (DataTable)Session["dtItemIndent"];
                if (!dtBOMIndent.Columns.Contains("ADJ_QTY"))
                {
                    dtBOMIndent.Columns.Add("ADJ_QTY", typeof(int));
                }
            }
            for (int iLoop = 0; iLoop < grdAdjBOM.Rows.Count; iLoop++)
            {
                Label lblAdjBOM_BUSINESS_TYPE = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_BUSINESS_TYPE");
                Label lblAdjBOM_PRODUCT_TYPE = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_PRODUCT_TYPE");
                Label lblAdjBOM_ORDER_CAT = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_ORDER_CAT");
                Label lblAdjBOM_ORDER_TYPE = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_ORDER_TYPE");
                Label lblAdjBOM_ORDER_NO = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_ORDER_NO");
                Label lblAdjBOM_PI_TYPE = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_PI_TYPE");
                Label lblAdjBOM_PI_NO = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_PI_NO");
                Label lblAdjBOM_ARTICAL_CODE = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_ARTICAL_CODE");
                Label lblAdjBOM_W_SIDE = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_W_SIDE");
                Label lblAdjBOM_BASE_ARTICAL_TYPE = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_BASE_ARTICAL_TYPE");
                Label lblAdjBOM_BASE_ARTICAL_CODE = (Label)grdAdjBOM.Rows[iLoop].FindControl("lblAdjBOM_BASE_ARTICAL_CODE");
                TextBox txtAdjBOMQTY = (TextBox)grdAdjBOM.Rows[iLoop].FindControl("txtAdjBOMQTY");

                double ADJ_QTY = double.Parse(txtAdjBOMQTY.Text.Trim());
                string ItemCode = lblAdjBOMItemCode.Text.Trim();

                DataView dv = new DataView(dtBOMIndent);
                dv.RowFilter = "BOM_COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and BOM_BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and BOM_BUSINESS_TYPE ='" + lblAdjBOM_BUSINESS_TYPE.Text + "' and BOM_PRODUCT_TYPE ='" + lblAdjBOM_PRODUCT_TYPE.Text + "' and BOM_ORDER_CAT ='" + lblAdjBOM_ORDER_CAT.Text + "' and BOM_ORDER_TYPE ='" + lblAdjBOM_ORDER_TYPE.Text + "' and BOM_ORDER_NO ='" + lblAdjBOM_ORDER_NO.Text + "' and BOM_PI_TYPE ='" + lblAdjBOM_PI_TYPE.Text + "' and BOM_PI_NO ='" + lblAdjBOM_PI_NO.Text + "' and BOM_ARTICAL_CODE ='" + lblAdjBOM_ARTICAL_CODE.Text + "' and BOM_W_SIDE ='" + lblAdjBOM_W_SIDE.Text + "' and BOM_BASE_ARTICAL_TYPE ='" + lblAdjBOM_BASE_ARTICAL_TYPE.Text + "' and BOM_BASE_ARTICAL_CODE ='" + lblAdjBOM_BASE_ARTICAL_CODE.Text + "'";
                if (dv.Count == 0)
                {
                    DataRow dr = dtBOMIndent.NewRow();

                    dr["BOM_COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BOM_BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["BOM_BUSINESS_TYPE"] = lblAdjBOM_BUSINESS_TYPE.Text;
                    dr["BOM_PRODUCT_TYPE"] = lblAdjBOM_PRODUCT_TYPE.Text;
                    dr["BOM_ORDER_CAT"] = lblAdjBOM_ORDER_CAT.Text;
                    dr["BOM_ORDER_TYPE"] = lblAdjBOM_ORDER_TYPE.Text;
                    dr["BOM_ORDER_NO"] = int.Parse(lblAdjBOM_ORDER_NO.Text);
                    dr["BOM_PI_TYPE"] = lblAdjBOM_PI_TYPE.Text;
                    dr["BOM_PI_NO"] = int.Parse(lblAdjBOM_PI_NO.Text);
                    dr["BOM_ARTICAL_CODE"] = lblAdjBOM_ARTICAL_CODE.Text;
                    dr["BOM_W_SIDE"] = lblAdjBOM_W_SIDE.Text;
                    dr["BOM_BASE_ARTICAL_TYPE"] = lblAdjBOM_BASE_ARTICAL_TYPE.Text;
                    dr["BOM_BASE_ARTICAL_CODE"] = lblAdjBOM_BASE_ARTICAL_CODE.Text;
                    dr["ADJ_TYPE"] = "INDENT";
                    dr["ADJ_QTY"] = ADJ_QTY;

                    dtBOMIndent.Rows.Add(dr);
                }
                else
                {
                    dv[0]["ADJ_QTY"] = ADJ_QTY;
                    dv[0]["ADJ_TYPE"] = "INDENT";
                }
                dtBOMIndent.AcceptChanges();
                TotalQTY = TotalQTY + ADJ_QTY;
            }
            return dtBOMIndent;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("BOM_COMP_CODE", typeof(string));
        dt.Columns.Add("BOM_BRANCH_CODE", typeof(string));
        dt.Columns.Add("BOM_BUSINESS_TYPE", typeof(string));
        dt.Columns.Add("BOM_PRODUCT_TYPE", typeof(string));
        dt.Columns.Add("BOM_ORDER_CAT", typeof(string));
        dt.Columns.Add("BOM_ORDER_TYPE", typeof(string));
        dt.Columns.Add("BOM_ORDER_NO", typeof(int));
        dt.Columns.Add("BOM_PI_TYPE", typeof(string));
        dt.Columns.Add("BOM_PI_NO", typeof(int));
        dt.Columns.Add("BOM_ARTICAL_CODE", typeof(string));
        dt.Columns.Add("BOM_W_SIDE", typeof(string));
        dt.Columns.Add("BOM_BASE_ARTICAL_TYPE", typeof(string));
        dt.Columns.Add("BOM_BASE_ARTICAL_CODE", typeof(string));
        dt.Columns.Add("REQ_QTY", typeof(double));
        dt.Columns.Add("ADJ_TYPE", typeof(string));
        dt.Columns.Add("ADJ_QTY", typeof(double));

        return dt;
    }

}
