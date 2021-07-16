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
using errorLog;

public partial class Module_Inventory_Controls_MaterialInTransit : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bindMaterialPOApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }


    }

    private void bindMaterialPOApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();
            oTX_ITEM_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            DataTable dt = SaitexBL.Interface.Method.Material_Purchase_Order.GetPODataForApproval_Transit(oTX_ITEM_PU_MST);
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
                gvmaterialintransit.DataSource = dt;
                gvmaterialintransit.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No PO for approval";
                gvmaterialintransit.DataSource = null;
                gvmaterialintransit.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No PO for approval");
            }
        }
        catch
        {
            throw;
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
            

            //Page.ClientScript.RegisterStartupScript(      this.GetType(), "OpenWindow","window.open('~/Module/Inventory/Pages/MaterialPurchaseOrderCredit.aspx?PO_NUMB=" + lblPO_NUMB.Text + " ','_newtab');", true);
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
            lblErrorMessage.Text = ex.ToString();
        }

    }
    protected void gvmaterialintransit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();

       
        if (e.CommandName == "lnkInsert")
        {

            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            oTX_ITEM_PU_MST.PO_NUMB = Convert.ToInt16(((Label)gvmaterialintransit.Rows[index].FindControl("lblPO_NUMB")).Text);
            oTX_ITEM_PU_MST.ShipmentDate = ((TextBox)gvmaterialintransit.Rows[index].FindControl("txtshipmentdate")).Text;
            oTX_ITEM_PU_MST.Remark = ((TextBox)gvmaterialintransit.Rows[index].FindControl("txtremark")).Text;
            oTX_ITEM_PU_MST.PAY_TERM = ((Label)gvmaterialintransit.Rows[index].FindControl("lblparycode")).Text;

          

            try
            {

               
                bool Res = SaitexBL.Interface.Method.Material_Purchase_Order.Insert_Transit(oTX_ITEM_PU_MST);
                if (Res == true)
                {
                    //Clear_Control();
                    Common.CommonFuction.ShowMessage("Record save sucessfully");
                    bindMaterialPOApproval();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("unable to save!please try again");
                }
            }
            catch (Exception ex)
            {
                Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in saving the record"));
            }

        
        }
    }
}