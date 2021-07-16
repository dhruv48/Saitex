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
using DBLibrary;
using Common;
using System.Data.OracleClient;
public partial class Module_Inventory_Pages_MaterialInTransitv : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
               // lblMode.Text = "Find";
                BindMaterialPOTransit();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
           // lblErrorMessage.Text = ex.ToString();
        }


    }
    private void BindMaterialPOTransit()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();
            oTX_ITEM_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            DataTable dt = GetPODataForApproval_Transit(oTX_ITEM_PU_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvmaterialintransitv.DataSource = dt;
                gvmaterialintransitv.DataBind();
                //lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
               // lblTotalRecord.Text = "No PO for approval";
                gvmaterialintransitv.DataSource = null;
                gvmaterialintransitv.DataBind();
                //lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No PO for approval");
            }
        }
        catch
        {
            throw;
        }
    }

    public static DataTable GetPODataForApproval_Transit(SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST)
    {
        OracleConnection con = SaitexDL.Common.Data.GetConnection();

        try
        {
            string TransitStatus = "Transit";            
            OracleCommand cmd = SaitexDL.Common.Data.CreateCommand(con);            
            cmd = new OracleCommand();
            cmd.Connection = con;
           // cmd.CommandText = "SELECT a.COMP_CODE, a.BRANCH_CODE, a.PO_TYPE,  a.PO_NUMB, a.PO_DATE, a.PRTY_CODE,  to_char(a.DEL_DATE,'dd/MM/yyyy') DEL_DATE, a.PAY_TERM, a.CONF_FLAG,    a.COMMENTS, a.DELV_BRANCH, a.TRSP_CODE,  a.DESP_MODE, a.INSU_FLAG, a.AUTH,    a.AUTH_BY, to_char(a.AUTH_DATE,'dd/MM/yyyy') AUTH_DATE, a.ADV_PRCNT,    a.FINAL_AMT, a.STATUS, a.DEL_STATUS,   a.TUSER,  to_char(a.TDATE,'dd/MM/yyyy') TDATE , a.CURRENCY_CODE,    a.CONVERSION_RATE, a.INSTRUCTIONS, a.PO_NATURE,    a.CONF_BY, to_char(a.CONF_DATE,'dd/MM/yyyy') CONF_DATE , a.YEAR,    a.CLOSE_REMARK, a.GATE_NUMB, a.GATE_DATE,    a.DELV_BRANCH_CODE, (B.PRTY_CODE || ', ' || B.PRTY_NAME ) PARTY_DATA,a.SHIPMENT_DATE,a.TRANSIT_REMARK FROM tx_item_pu_mst a INNER JOIN  tx_vendor_mst b ON A.PRTY_CODE (+) = B.PRTY_CODE RIGHT JOIN TX_GATE_MST c ON A.PRTY_CODE = C.PRTY_CODE WHERE  NVL(a.Conf_flag,1) = 1 AND a.COMP_CODE = :Comp_Code AND a.BRANCH_CODE = :Branch_Code and a.YEAR=:YEAR and a.TRANSIT_STATUS= :TransitStatus  ORDER BY a.PO_NUMB";

            cmd.CommandText = "SELECT a.COMP_CODE, a.BRANCH_CODE, a.PO_TYPE,  a.PO_NUMB, a.PO_DATE, a.PRTY_CODE,  to_char(a.DEL_DATE,'dd/MM/yyyy') DEL_DATE, a.PAY_TERM, a.CONF_FLAG,    a.COMMENTS, a.DELV_BRANCH, a.TRSP_CODE,  a.DESP_MODE, a.INSU_FLAG, a.AUTH,    a.AUTH_BY, to_char(a.AUTH_DATE,'dd/MM/yyyy') AUTH_DATE, a.ADV_PRCNT,    a.FINAL_AMT, a.STATUS, a.DEL_STATUS,   a.TUSER,  to_char(a.TDATE,'dd/MM/yyyy') TDATE , a.CURRENCY_CODE,    a.CONVERSION_RATE, a.INSTRUCTIONS, a.PO_NATURE,    a.CONF_BY, to_char(a.CONF_DATE,'dd/MM/yyyy') CONF_DATE , a.YEAR,    a.CLOSE_REMARK, a.GATE_NUMB, a.GATE_DATE,    a.DELV_BRANCH_CODE, (B.PRTY_CODE || ', ' || B.PRTY_NAME ) PARTY_DATA,a.SHIPMENT_DATE,a.TRANSIT_REMARK,c.GATE_DATE FROM tx_item_pu_mst a INNER JOIN  tx_vendor_mst b ON A.PRTY_CODE (+) = B.PRTY_CODE RIGHT JOIN TX_GATE_MST c ON A.PRTY_CODE = C.PRTY_CODE WHERE  NVL(a.Conf_flag,1) = 1 AND a.COMP_CODE = :Comp_Code AND a.BRANCH_CODE = :Branch_Code and a.YEAR=:YEAR   ORDER BY a.PO_NUMB";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue(":YEAR", oTX_ITEM_PU_MST.YEAR);
            cmd.Parameters.AddWithValue(":Comp_Code", oTX_ITEM_PU_MST.COMP_CODE);
            cmd.Parameters.AddWithValue(":Branch_Code", oTX_ITEM_PU_MST.BRANCH_CODE);
            //cmd.Parameters.AddWithValue(":TransitStatus", TransitStatus);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }
        }
    }

    protected void lbtnViewPOTRN_Click(object sender, EventArgs e)
    {
        try
        {
            var chk = ((LinkButton)(sender));
            var gv1 = ((GridViewRow)(chk.NamingContainer));
            var lblPO_NUMB = (Label)gv1.FindControl("lblPO_NUMB");
            var lblPO_type = (Label)gv1.FindControl("lblPO_type");


            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow","window.open('~/Module/Inventory/Pages/MaterialPurchaseOrderCredit.aspx?PO_NUMB=" + lblPO_NUMB.Text + " ','_newtab');", true);
            if (lblPO_type.Text.Contains("PUM"))
            {
                Response.Redirect("~/Module/Inventory/Pages/MaterialPurchaseOrderCredit.aspx?PO_NUMB=" + lblPO_NUMB.Text);
            }
            else if (lblPO_type.Text.Contains("PUC"))
            {
                Response.Redirect("~/Module/Inventory/Pages/MaterialPurchaseOrderCash.aspx?PO_NUMB=" + lblPO_NUMB.Text);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Closing.\r\nSee error log for detail."));
        }

    }
    protected void gvmaterialintransit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;

                Label lblPO_NUMB = (Label)grdRow.FindControl("lblPO_NUMB");
                Label lblPO_type = (Label)grdRow.FindControl("lblPO_type");

                SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();
                oTX_ITEM_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_ITEM_PU_MST.PO_NUMB = int.Parse(lblPO_NUMB.Text.Trim());
                oTX_ITEM_PU_MST.PO_TYPE = lblPO_type.Text.Trim();

                DataTable dtPOTRN = SaitexBL.Interface.Method.Material_Purchase_Order.GetPOTRNDataForApproval_Transit(oTX_ITEM_PU_MST);

                if (dtPOTRN != null && dtPOTRN.Rows.Count > 0)
                {
                    GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
                    grdPOTRN.DataSource = dtPOTRN;
                    grdPOTRN.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
            //lblErrorMessage.Text = ex.ToString();
        }

    }
    protected void txtsubmit_Click(object sender, EventArgs e)
    {
        BindMaterialPOTransit();
    }
}