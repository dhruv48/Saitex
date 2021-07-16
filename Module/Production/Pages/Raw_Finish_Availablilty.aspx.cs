using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_Production_Pages_Raw_Finish_Availablilty : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    double total_req_qty = 0;
    double total_bal_qty = 0;
    double total_stock_qty = 0;
    double total_req_qty1 = 0;
    double total_bal_qty1 = 0;
    double total_stock_qty1 = 0;
    protected void Page_Load(object sender, EventArgs e)    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];       
        if (!IsPostBack)
        {
            bindData();
        }

    }

    private void bindData()
    {
        try
        {
            DataTable dtFinish = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForFinishMaterialRequirement(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString());
            DataTable dtRaw = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForRawMaterialRequirement(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString());
            bindGrid(grdFinishDetails, dtFinish);
            bindGrid(grdRawDetails, dtRaw);
           
        }
        catch
        {
            throw;
        }
    }

    public void bindGrid(GridView grd,DataTable dt)
    {
      
        if (dt != null && dt.Rows.Count > 0)
        {
            grd.DataSource = dt;
            grd.DataBind();
        }
        else
        {
            grd.DataSource = null ;
            grd.DataBind();
            grd.EmptyDataText = "No Records Available.";
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:gridWindowClose()", true);
    }
    protected void grdRawDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double req_qty = 0;
            double bal_qty = 0;
            double stock_qty = 0;
            var grdRow = e.Row;

            var lblREQ_QTY = ((Label)e.Row.FindControl("lblREQ_QTY"));
            var lblBAL_QTY = ((Label)e.Row.FindControl("lblBAL_QTY"));
            var lblSTOCK_QTY = ((Label)e.Row.FindControl("lblSTOCK_QTY"));

            double.TryParse(lblREQ_QTY.Text,out req_qty);
            double.TryParse(lblBAL_QTY.Text,out bal_qty);
            double.TryParse(lblSTOCK_QTY.Text,out stock_qty);
            if(stock_qty<bal_qty)
            {
             lblSTOCK_QTY.ForeColor=System.Drawing.Color.Red;
            }

         total_req_qty = total_req_qty+req_qty;
         total_bal_qty =total_bal_qty+bal_qty;
         total_stock_qty = total_stock_qty+stock_qty;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            var grdRow = e.Row;
            ((Label)e.Row.FindControl("lblTOTAL_REQ_QTY")).Text = total_req_qty.ToString();
            ((Label)e.Row.FindControl("lblTOTAL_BAL_QTY")).Text = total_bal_qty.ToString();
            ((Label)e.Row.FindControl("lblTOTAL_STOCK_QTY")).Text = total_stock_qty.ToString();

        }

    }
    protected void grdFinishDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double req_qty = 0;
            double bal_qty = 0;
            double stock_qty = 0;
            var grdRow = e.Row;

            var lblREQ_QTY = ((Label)e.Row.FindControl("lblREQ_QTY"));
            var lblBAL_QTY = ((Label)e.Row.FindControl("lblBAL_QTY"));
            var lblSTOCK_QTY = ((Label)e.Row.FindControl("lblSTOCK_QTY"));

            double.TryParse(lblREQ_QTY.Text, out req_qty);
            double.TryParse(lblBAL_QTY.Text, out bal_qty);
            double.TryParse(lblSTOCK_QTY.Text, out stock_qty);
            if (stock_qty < bal_qty)
            {
                lblSTOCK_QTY.ForeColor = System.Drawing.Color.Red;
            }

            total_req_qty1 = total_req_qty1 + req_qty;
            total_bal_qty1 = total_bal_qty1 + bal_qty;
            total_stock_qty1 = total_stock_qty1 + stock_qty;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            var grdRow = e.Row;
            ((Label)e.Row.FindControl("lblTOTAL_REQ_QTY")).Text = total_req_qty1.ToString();
            ((Label)e.Row.FindControl("lblTOTAL_BAL_QTY")).Text = total_bal_qty1.ToString();
            ((Label)e.Row.FindControl("lblTOTAL_STOCK_QTY")).Text = total_stock_qty1.ToString();

        }
    }
}
