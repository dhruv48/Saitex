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

public partial class Module_OrderDevelopment_Pages_bom_adj : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {

                if (Request.QueryString["BUSINESS_TYPE"] != null)
                {
                    hfBUSINESS_TYPE.Value = Request.QueryString["BUSINESS_TYPE"].ToString();
                }
                if (Request.QueryString["PRODUCT_TYPE"] != null)
                {
                    hfPRODUCT_TYPE.Value = Request.QueryString["PRODUCT_TYPE"].ToString();
                }
                if (Request.QueryString["ORDER_CAT"] != null)
                {
                    hfORDER_CAT.Value = Request.QueryString["ORDER_CAT"].ToString();
                }
                if (Request.QueryString["ORDER_TYPE"] != null)
                {
                    hfORDER_TYPE.Value = Request.QueryString["ORDER_TYPE"].ToString();
                }
                if (Request.QueryString["ORDER_NO"] != null)
                {
                    hfORDER_NO.Value = Request.QueryString["ORDER_NO"].ToString();
                }
                if (Request.QueryString["PI_TYPE"] != null)
                {
                    hfPI_TYPE.Value = Request.QueryString["PI_TYPE"].ToString();
                }
                if (Request.QueryString["PI_NO"] != null)
                {
                    hfPI_NO.Value = Request.QueryString["PI_NO"].ToString();
                }
                if (Request.QueryString["ARTICAL_CODE"] != null)
                {
                    hfARTICAL_CODE.Value = Request.QueryString["ARTICAL_CODE"].ToString();
                    lbladjBOMItemCode.Text = hfARTICAL_CODE.Value;
                }
                if (Request.QueryString["TextBoxId"] != null)
                {
                    hfTextBoxId.Value = Request.QueryString["TextBoxId"].ToString();
                }
                GetDataForAdj();

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading data for adjustment.\r\nSee error log for detail."));
        }
    }

    private void GetDataForAdj()
    {
        try
        {
            SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = hfBUSINESS_TYPE.Value;
            oOD_CAPTURE_MST.PRODUCT_TYPE = hfPRODUCT_TYPE.Value;
            oOD_CAPTURE_MST.ORDER_CAT = hfORDER_CAT.Value;
            oOD_CAPTURE_MST.ORDER_TYPE = hfORDER_TYPE.Value;
            oOD_CAPTURE_MST.ORDER_NO = hfORDER_NO.Value;

            DataTable dtBOMAdj = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetBOMAdjData(oOD_CAPTURE_MST, hfPI_TYPE.Value, hfPI_NO.Value, hfARTICAL_CODE.Value);

            if (dtBOMAdj != null && dtBOMAdj.Rows.Count > 0)
            {
                grdBOMAdjustment.DataSource = dtBOMAdj;
                grdBOMAdjustment.DataBind();
                lblAdjustItemBOMCode.Text = hfARTICAL_CODE.Value;
            }
            else
            {
                lblBOMAdjustmentError.Text = "No Record exists for adjustment for provided Artical Code";
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdBOMAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblAdjustREM_QTY = (Label)row.FindControl("lblAdjustREM_QTY");
            TextBox txtAdjBOMQTY = (TextBox)row.FindControl("txtAdjBOMQTY");
            txtAdjBOMQTY.Text = lblAdjustREM_QTY.Text;

            Label txtTotalAdjBOMQTY = (Label)grdBOMAdjustment.FooterRow.FindControl("txtTotalAdjBOMQTY");
            txtTotalAdjBOMQTY.Text = GetFinalTotalOfBOMAdj().ToString();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for adjustment.\r\nSee error log for detail."));
        }
    }

    private double GetFinalTotalOfBOMAdj()
    {
        try
        {
            double FinalTotal = 0;
            for (int iLoop = 0; iLoop < grdBOMAdjustment.Rows.Count; iLoop++)
            {
                TextBox txtAdjBOMQTY = (TextBox)grdBOMAdjustment.Rows[iLoop].FindControl("txtAdjBOMQTY");
                Label lblAdjustREM_QTY = (Label)grdBOMAdjustment.Rows[iLoop].FindControl("lblAdjustREM_QTY");
                double Total = double.Parse(txtAdjBOMQTY.Text);
                if (Total <= double.Parse(lblAdjustREM_QTY.Text))
                {
                    FinalTotal = FinalTotal + Total;
                }
                else
                {
                    lblBOMAdjustmentError.Text = "Entered quantity is larger then BOM quantity";
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

    protected void btnAdjBOMItem_Click(object sender, EventArgs e)
    {

    }

    private void CreateDataTableForAdj()
    {
        try
        {
            DataTable dtBOMAdj;
            if (Session["dtBOMAdj"] == null)
            {
                dtBOMAdj = CreateAdjTable();
            }
            else
            {
                dtBOMAdj = (DataTable)Session["dtBOMAdj"];
            }

            if (grdBOMAdjustment.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdBOMAdjustment.Rows)
                {
                    TextBox txtAdjBOMQTY = (TextBox)grdRow.FindControl("txtAdjBOMQTY");

                    double Adj_Qty = 0;
                    double.TryParse(txtAdjBOMQTY.Text, out Adj_Qty);

                    if (Adj_Qty > 0)
                    {
                        Label lbladjBOM_BUSINESS_TYPE = (Label)grdRow.FindControl("lbladjBOM_BUSINESS_TYPE");
                        Label lbladjBOM_PRODUCT_TYPE = (Label)grdRow.FindControl("lbladjBOM_PRODUCT_TYPE");
                        Label lbladjBOM_ORDER_CAT = (Label)grdRow.FindControl("lbladjBOM_ORDER_CAT");
                        Label lbladjBOM_ORDER_TYPE = (Label)grdRow.FindControl("lbladjBOM_ORDER_TYPE");
                        Label lbladjBOM_ORDER_NO = (Label)grdRow.FindControl("lbladjBOM_ORDER_NO");
                        Label lbladjBOM_PI_TYPE = (Label)grdRow.FindControl("lbladjBOM_PI_TYPE");
                        Label lbladjBOM_PI_NO = (Label)grdRow.FindControl("lbladjBOM_PI_NO");
                        Label lbladjBOM_ARTICAL_CODE = (Label)grdRow.FindControl("lbladjBOM_ARTICAL_CODE");
                        Label lbladjBOM_W_SIDE = (Label)grdRow.FindControl("lbladjBOM_W_SIDE");
                        Label lbladjBOM_BASE_ARTICAL_TYPE = (Label)grdRow.FindControl("lbladjBOM_BASE_ARTICAL_TYPE");
                        Label lbladjBOM_BASE_ARTICAL_CODE = (Label)grdRow.FindControl("lbladjBOM_BASE_ARTICAL_CODE");

                        DataRow dr = dtBOMAdj.NewRow();

                        dr["BOM_COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BOM_BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["BOM_BUSINESS_TYPE"] = lbladjBOM_BUSINESS_TYPE.Text;
                        dr["BOM_PRODUCT_TYPE"] = lbladjBOM_PRODUCT_TYPE.Text;
                        dr["BOM_ORDER_CAT"] = lbladjBOM_ORDER_CAT.Text;
                        dr["BOM_ORDER_TYPE"] = lbladjBOM_ORDER_TYPE.Text;
                        dr["BOM_ORDER_NO"] = lbladjBOM_ORDER_NO.Text;
                        dr["BOM_PI_TYPE"] = lbladjBOM_PI_TYPE.Text;
                        dr["BOM_PI_NO"] = lbladjBOM_PI_NO.Text;
                        dr["BOM_ARTICAL_CODE"] = lbladjBOM_ARTICAL_CODE.Text;
                        dr["BOM_W_SIDE"] = lbladjBOM_W_SIDE.Text;
                        dr["BOM_BASE_ARTICAL_TYPE"] = lbladjBOM_BASE_ARTICAL_TYPE.Text;
                        dr["BOM_BASE_ARTICAL_CODE"] = lbladjBOM_BASE_ARTICAL_CODE.Text;
                        dr["ADJ_TYPE"] = "ORDER";
                        dr["ADJ_QTY"] = Adj_Qty;

                        dr["ARTICAL_CODE"] = hfARTICAL_CODE.Value;
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["BUSINESS_TYPE"] = hfBUSINESS_TYPE.Value;
                        dr["PRODUCT_TYPE"] = hfPRODUCT_TYPE.Value;
                        dr["ORDER_CAT"] = hfORDER_CAT.Value;
                        dr["ORDER_TYPE"] = hfORDER_TYPE.Value;
                        dr["ORDER_NO"] = hfORDER_NO.Value;
                        dr["PI_TYPE"] = hfPI_TYPE.Value;
                        dr["PI_NO"] = hfPI_NO.Value;
                        dtBOMAdj.Rows.Add(dr);
                    }
                }
            }

            Session["dtBOMAdj"] = dtBOMAdj;
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateAdjTable()
    {
        try
        {
            DataTable dtBOMAdj = new DataTable();
            dtBOMAdj.Columns.Add("BOM_COMP_CODE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_BRANCH_CODE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_BUSINESS_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_PRODUCT_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_ORDER_CAT", typeof(string));
            dtBOMAdj.Columns.Add("BOM_ORDER_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_ORDER_NO", typeof(string));
            dtBOMAdj.Columns.Add("BOM_PI_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_PI_NO", typeof(string));
            dtBOMAdj.Columns.Add("BOM_ARTICAL_CODE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_W_SIDE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_BASE_ARTICAL_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("BOM_BASE_ARTICAL_CODE", typeof(string));
            dtBOMAdj.Columns.Add("ADJ_QTY", typeof(double));
            dtBOMAdj.Columns.Add("ADJ_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("ARTICAL_CODE", typeof(string));
            dtBOMAdj.Columns.Add("COMP_CODE", typeof(string));
            dtBOMAdj.Columns.Add("BRANCH_CODE", typeof(string));
            dtBOMAdj.Columns.Add("BUSINESS_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("PRODUCT_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("ORDER_CAT", typeof(string));
            dtBOMAdj.Columns.Add("ORDER_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("ORDER_NO", typeof(string));
            dtBOMAdj.Columns.Add("PI_TYPE", typeof(string));
            dtBOMAdj.Columns.Add("PI_NO", typeof(string));
            return dtBOMAdj;
        }
        catch
        {
            throw;
        }
    }

    protected void btnAdjBOMItem_Click1(object sender, EventArgs e)
    {
        try
        {
            CreateDataTableForAdj();
            double Total_Qty = GetFinalTotalOfBOMAdj();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + Total_Qty + "','" + hfTextBoxId.Value + "')", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for adjustment.\r\nSee error log for detail."));
        }
    }
    protected void txtAdjBOMQTY_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Label txtTotalAdjBOMQTY = (Label)grdBOMAdjustment.FooterRow.FindControl("txtTotalAdjBOMQTY");
            txtTotalAdjBOMQTY.Text = GetFinalTotalOfBOMAdj().ToString();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Qty Adj.\r\nSee error log for detail."));
        }
    }
}
