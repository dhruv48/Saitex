using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
public partial class Module_PlanningAndScheduling_Controls_ListOfOrderPlanning : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string ORDER_TYPE { get; set; }
    public string PRODUCT_TYPE { get; set; }
    public string Header_Name { get; set; }
    public string TYPE { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (Request.QueryString["ORDER_TYPE"] != null)
        {
            ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
        }
        if (Request.QueryString["PRODUCT_TYPE"] != null)
        {
            PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
        }
        if (Request.QueryString["TYPE"] != null)
        {
            TYPE = Request.QueryString["TYPE"].ToString();
        }
         if (!IsPostBack)
        {
            lblType.Text = TYPE;
            var dt = GetListOfOrderNumbers(TYPE, "", ORDER_TYPE, PRODUCT_TYPE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            ENABLE_DISABLE_GRID(dt);

        }

    }

    protected void ENABLE_DISABLE_GRID(DataTable dt)
    {
        if (TYPE == "PLANNED")
        {
            pnlPlanned.Visible = true;
            pnlRemaining.Visible = false;
            pnlUnplanned.Visible = false;
            BindGrid(dt, grdOrderDetails);
        }
        if (TYPE == "REMAINING")
        {
            pnlPlanned.Visible = false;
            pnlRemaining.Visible = true;
            pnlUnplanned.Visible = false;
            BindGrid(dt, grdOrderDetails1);
        }
        if (TYPE == "UNPLANNED")
        {
            pnlPlanned.Visible = false;
            pnlRemaining.Visible = false;
            pnlUnplanned.Visible = true;
            BindGrid(dt, grdOrderDetails2);
        }
    }

    protected DataTable GetListOfOrderNumbers(string TYPE, string ORDER_NO, string  ORDER_TYPE, string PRODUCT_TYPE, string COMP_CODE, string BRANCH_CODE)
    {
        return SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetListOfOrderNumbers(TYPE, ORDER_NO, ORDER_TYPE, PRODUCT_TYPE, COMP_CODE, BRANCH_CODE);

    }

    protected void BindGrid(DataTable dt, GridView grd)
    {
        if (dt.Rows.Count > 0 && dt != null)
        {
            grd.DataSource=dt;
            grd.DataBind();

        }
        else 
        {
            grd.DataSource = null;
            grd.DataBind();
            grd.EmptyDataText = "Data is not available.";

        }
    }

    protected void grdOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var gv = (GridView)e.Row.FindControl("gvChildGrid");
            var ORDERNO =(Label) e.Row.FindControl("lblorderno");
            var DTDetails = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetPlannedMachineDetailsByOrderNo(ORDERNO.Text, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PRODUCT_TYPE, ORDER_TYPE);

            BindGrid(DTDetails, gv);

        }
    }

    protected void btnUnplanned_Click(object sender, EventArgs e)
    {
      var dt= getDataInDataTableFromGridForOrderDetails(grdOrderDetails);
    }
    
    private DataTable CreateDataTableForOrderMachineDetails()
    {
        var dtDetail = new DataTable();
        dtDetail.Columns.Add("ORDER_NO", typeof(string));       
        dtDetail.Columns.Add("PARTY_CODE", typeof(string));
        dtDetail.Columns.Add("ARTICLE_CODE", typeof(string));
        dtDetail.Columns.Add("SHADE_CODE", typeof(string));
        dtDetail.Columns.Add("PROS_ROUTE_CODE", typeof(string));
        dtDetail.Columns.Add("MACHINE_GROUP", typeof(string));
        dtDetail.Columns.Add("MACHINE_CODE", typeof(string));

        dtDetail.Columns.Add("SCHEDULED_DATE_FROM", typeof(DateTime));
        dtDetail.Columns.Add("SCHEDULED_DATE_TO", typeof(DateTime));
        dtDetail.Columns.Add("DIFF_TIME", typeof(string));

        dtDetail.Columns.Add("PRODUCT_TYPE", typeof(string));       
        dtDetail.Columns.Add("ORDER_TYPE", typeof(string));
        dtDetail.Columns.Add("COMP_CODE", typeof(string));
        dtDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtDetail.Columns.Add("FLAG", typeof(string));
        dtDetail.Columns.Add("TDATE", typeof(string));
        dtDetail.Columns.Add("TUSER", typeof(string));
        return dtDetail;
    }

    public DataTable getDataInDataTableFromGridForOrderDetails(GridView gridView)
    {
        var orderDetailsDataTable = CreateDataTableForOrderMachineDetails();
        try
        {
            string msg = string.Empty;
            var totalRows = gridView.Rows.Count;

            for (int r = 0; r < totalRows; r++)
            {
                var thisMainGridViewRow = gridView.Rows[r];
              if (thisMainGridViewRow.RowType == DataControlRowType.DataRow)
               {
                  var chk=(CheckBox)gridView.Rows[r].FindControl("chkOrder");
               if(chk.Checked)
                {
                var chGridView = (GridView)gridView.Rows[r].FindControl("gvChildGrid");
                var totalChildRow = chGridView.Rows.Count;
                for (int j = 0; j < totalChildRow; j++)
                {
                    var thisGridViewRow = chGridView.Rows[j];
                    if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                    {

                        var lblORDER_NO = (Label)thisMainGridViewRow.FindControl("lblorderno");
                        var lblARTICLE_CODE = (Label)thisMainGridViewRow.FindControl("lblarticleDesc");
                        var lblSHADE_CODE = (Label)thisMainGridViewRow.FindControl("lblSHADE_NAME");
                        var lblPROCESS_ROOT = (Label)thisMainGridViewRow.FindControl("lblprderpro");
                        var lblPartyName = (Label)thisMainGridViewRow.FindControl("lblprtyname");

                        var txtFrom = (Label)thisGridViewRow.FindControl("txtFrom");
                        var txtTo = (Label)thisGridViewRow.FindControl("txtTo");

                        var dr = orderDetailsDataTable.NewRow();
                        dr["ORDER_NO"] = lblORDER_NO.Text;                        
                        dr["PARTY_CODE"] = lblPartyName.ToolTip;
                        dr["ARTICLE_CODE"] = lblARTICLE_CODE.ToolTip;
                        dr["SHADE_CODE"] = lblSHADE_CODE.ToolTip;
                        dr["PROS_ROUTE_CODE"] = lblPROCESS_ROOT.Text;

                        dr["MACHINE_GROUP"] = thisGridViewRow.Cells[1].Text;
                        dr["MACHINE_CODE"] = thisGridViewRow.Cells[2].Text;
                        dr["SCHEDULED_DATE_FROM"] = txtFrom.Text;
                        dr["SCHEDULED_DATE_TO"] = txtTo.Text;
                        dr["DIFF_TIME"] = string.Empty; 

                        dr["PRODUCT_TYPE"] = PRODUCT_TYPE;
                        dr["ORDER_TYPE"] = ORDER_TYPE;
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["FLAG"] = "1";
                        dr["TDATE"] = DateTime.Now.Date.ToString();
                        dr["TUSER"] = oUserLoginDetail.UserCode;
                        orderDetailsDataTable.Rows.Add(dr);

                    }

                }
              }
             }
            }
            orderDetailsDataTable.AcceptChanges();
            return orderDetailsDataTable;
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in unplanning machine details.\r\nSee error log for detail."));
            return orderDetailsDataTable = null;
        }


    }

}



//if (!CheckPlannedQty(remqty, plqty))
//{
//    CommonFuction.ShowMessage("Planned Qty is greater then Balance Qty. Enter relavent Planned Qty");
//}