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
using Common;
using errorLog;

using Obout.Interface;

public partial class Module_Production_Controls_LotCLosing4YS : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialControls();
                GetLotClosingDataInGrid();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;

            int TotalRows = Grd_Lot_Closing.Rows.Count;

            for (int r = 0; r < TotalRows; r++)
            {
                GridViewRow ThisGridViewRow = Grd_Lot_Closing.Rows[r];
                if (ThisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblOrderNo = (Label)ThisGridViewRow.FindControl("lblOrderNo");
                    Label lblLotNo = (Label)ThisGridViewRow.FindControl("lblLotNo");
                    Label lblStockQty = (Label)ThisGridViewRow.FindControl("lblStockQty");
                    Label lblDeptCode = (Label)ThisGridViewRow.FindControl("lblDeptCode");
                    Label lblproductType = (Label)ThisGridViewRow.FindControl("lblproductType");
                    CheckBox closing = (CheckBox)ThisGridViewRow.FindControl("chklotclosing");

                    closing.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetLotClosingDataInGrid()
    {
        try
        {
            //string CLOSE_DATE = System.DateTime.Now.ToShortDateString();
            //string CLOSE_BY = oUserLoginDetail.UserCode;

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_WIP_LOT_MOVE_LOG.GetLotDataInGrid4YS();
            if (dt != null && dt.Rows.Count > 0)
            {
                Grd_Lot_Closing.DataSource = dt;
                Grd_Lot_Closing.DataBind();
                Grd_Lot_Closing.Visible = true;

            }
            else
            {
                Grd_Lot_Closing.DataSource = null;
                Grd_Lot_Closing.DataBind();
                Grd_Lot_Closing.Visible = true;
                Common.CommonFuction.ShowMessage("No Record Exists for Updation");

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable CreateDataTable()
    {
        DataTable dtLotClosing = new DataTable();


        dtLotClosing.Columns.Add("COMP_CODE", typeof(string));
        dtLotClosing.Columns.Add("BRANCH_CODE", typeof(string));
        dtLotClosing.Columns.Add("CLOSE_DATE", typeof(string));
        dtLotClosing.Columns.Add("CLOSE_BY", typeof(string));

        //dtLotClosing.Columns.Add("ORDER_NO", typeof(string));
        dtLotClosing.Columns.Add("LOT_NUMBER", typeof(string));
        dtLotClosing.Columns.Add("STOCK_QTY", typeof(string));
        dtLotClosing.Columns.Add("DEPT_CODE", typeof(string));
        //dtLotClosing.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtLotClosing.Columns.Add("STATUS", typeof(string));

        return dtLotClosing;
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpdateData4Closing();
    }

    /// <summary>
    /// Code to close the selected lot
    /// </summary>
    private void UpdateData4Closing()
    {
        try
        {

            DataTable dtLotClosing = CreateDataTable();
            int TotalRows = Grd_Lot_Closing.Rows.Count;
            int YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            for (int r = 0; r < TotalRows; r++)
            {
                GridViewRow ThisGridViewRow = Grd_Lot_Closing.Rows[r];

                if (ThisGridViewRow.RowType == DataControlRowType.DataRow)
                {

                    //Label lblOrderNo = (Label)ThisGridViewRow.FindControl("lblOrderNo");
                    Label lblLotNo = (Label)ThisGridViewRow.FindControl("lblLotNo");
                    // Label lbllotqty = (Label)ThisGridViewRow.FindControl("lbllotqty");
                    Label lblDeptCode = (Label)ThisGridViewRow.FindControl("lblDeptCode");
                    // Label lblproductType = (Label)ThisGridViewRow.FindControl("lblproductType");
                    CheckBox closing = (CheckBox)ThisGridViewRow.FindControl("chklotclosing");


                    if (closing.Checked == true)
                    {
                        DataRow dr = dtLotClosing.NewRow();


                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["DEPT_CODE"] = lblDeptCode.Text.Trim();
                        dr["CLOSE_DATE"] = System.DateTime.Now;
                        dr["CLOSE_BY"] = oUserLoginDetail.UserCode.Trim();
                        //dr["ORDER_NO"] = lblOrderNo.Text.Trim();
                        dr["LOT_NUMBER"] = lblLotNo.Text.Trim();
                        // dr["STOCK_QTY"] = lbllotqty.Text.Trim();

                        // dr["PRODUCT_TYPE"] = lblproductType.Text.Trim();
                        dr["STATUS"] = "3";

                        dtLotClosing.Rows.Add(dr);

                        closing.Checked = false;

                    }

                }

            }
            int iResult = SaitexBL.Interface.Method.YRN_WIP_LOT_MOVE_LOG.UpdateLotRecord_Closing(YEAR, oUserLoginDetail.UserCode, dtLotClosing);
            if (iResult > 0)
            {
                Common.CommonFuction.ShowMessage("Lot Close Successfully...");
                GetLotClosingDataInGrid();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Code To check that selected lot's qty is zero or more then zero
    /// In all department apart from Packing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chklotclosing_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = ((CheckBox)(sender));
            GridViewRow gv1 = ((GridViewRow)(chk.NamingContainer));

            Label lblOrderNo = (Label)gv1.FindControl("lblOrderNo");
            Label lblLotNo = (Label)gv1.FindControl("lblLotNo");
            //Label lbllotqty = (Label)gv1.FindControl("lbllotqty");
            Label lblDeptCode = (Label)gv1.FindControl("lblDeptCode");
            CheckBox chklotclosing = (CheckBox)gv1.FindControl("chklotclosing");

            string COMP_CODE = oUserLoginDetail.COMP_CODE;
            string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            string ORDER_NO = lblOrderNo.Text.Trim();
            string LOT_NUMBER = lblLotNo.Text.Trim();
            //string STOCK_QTY = lbllotqty.Text.Trim();
            string DEPT_CODE = lblDeptCode.Text.Trim();

            if (chklotclosing.Checked == true)
            {

                DataTable dt = SaitexBL.Interface.Method.YRN_WIP_LOT_MOVE_LOG.validateRowForUpdation(COMP_CODE, BRANCH_CODE, ORDER_NO, LOT_NUMBER, DEPT_CODE);
                if (dt == null)
                {
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Adjust Pending Qty of lot In All Branches First Before Close The Lot...");
                    chklotclosing.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Grd_Lot_Closing_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grd_Lot_Closing.PageIndex = e.NewPageIndex;
            GetLotClosingDataInGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
