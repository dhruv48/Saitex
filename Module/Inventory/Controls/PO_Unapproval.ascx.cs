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

public partial class Module_Inventory_Controls_PO_Unapproval : System.Web.UI.UserControl
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
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }
    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtPODetail = new DataTable();
            dtPODetail.Columns.Add("YEAR", typeof(int));
            dtPODetail.Columns.Add("COMP_CODE", typeof(string));
            dtPODetail.Columns.Add("BRANCH_CODE", typeof(string));
            dtPODetail.Columns.Add("PO_TYPE", typeof(string));
            dtPODetail.Columns.Add("PO_NUMB", typeof(int));
            dtPODetail.Columns.Add("PO_NATURE", typeof(string));
            dtPODetail.Columns.Add("PARTY_DATA", typeof(string));
            dtPODetail.Columns.Add("DEL_DATE", typeof(DateTime));
            dtPODetail.Columns.Add("CONF_FLAG", typeof(string));
            dtPODetail.Columns.Add("CONF_DATE", typeof(DateTime));
            dtPODetail.Columns.Add("CONF_BY", typeof(string));
            dtPODetail.Columns.Add("AUTH", typeof(string));
            dtPODetail.Columns.Add("AUTH_DATE", typeof(DateTime));
            dtPODetail.Columns.Add("AUTH_BY", typeof(string));
            dtPODetail.Columns.Add("REMARKS", typeof(string));
            return dtPODetail;
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            DataTable dtPODetail = CreateDataTable();
            int totalRows = gvMaterialpoApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialpoApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblPO_NUMB = (Label)thisGridViewRow.FindControl("lblPO_NUMB");
                    Label lblPO_type = (Label)thisGridViewRow.FindControl("lblPO_type");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                  

                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtPODetail.NewRow();

                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["PO_TYPE"] = lblPO_type.Text.Trim();
                        dr["PO_NUMB"] = int.Parse(lblPO_NUMB.Text.Trim());
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "0";
                        dr["AUTH_DATE"] = DateTime.Now.Date.ToString();
                        dr["AUTH_BY"] = oUserLoginDetail.UserCode;
                        dr["AUTH"] = "0";
                        dr["REMARKS"] = string.Empty;
                        dtPODetail.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.Material_Purchase_Order.Update_POForApproval(oUserLoginDetail.UserCode, dtPODetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                CommonFuction.ShowMessage("PO Unapproved Successfully.");
                bindMaterialPOApproval();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Unapproval.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "PrintItemPO.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page Printing.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Inventory/Pages/PO_Unapproval.aspx", false);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page Re-loading.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page Leaving.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void bindMaterialPOApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();
            oTX_ITEM_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            DataTable dt = SaitexBL.Interface.Method.Material_Purchase_Order.GetPODataForUnApproval(oTX_ITEM_PU_MST);
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
                gvMaterialpoApproval.DataSource = dt;
                gvMaterialpoApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No PO for approval";
                gvMaterialpoApproval.DataSource = null;
                gvMaterialpoApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No PO for approval");
            }
        }
        catch
        {
            throw;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void gvMaterialpoApproval_RowDataBound(object sender, GridViewRowEventArgs e)
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

                DataTable dtPOTRN = SaitexBL.Interface.Method.Material_Purchase_Order.GetPOTRNDataForApproval(oTX_ITEM_PU_MST);

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

}
