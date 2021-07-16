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

public partial class Module_Inventory_Controls_POList : System.Web.UI.UserControl
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
                CreateMaterialPOApproval();
                bindMaterialPOApproval("ALL");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }

    private void CreateMaterialPOApproval()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.Material_Purchase_Order.SelectPO(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);//.GetPODataForClosing(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
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
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
            }

            ViewState["dtPOMainDetail"] = dt;
        }
        catch
        {
            throw;
        }
    }

    private void bindMaterialPOApproval(string FilterOption)
    {
        try
        {
            gvMaterialpo.DataSource = null;
            gvMaterialpo.DataBind();
            lblTotalRecord.Text = "0";

            DataTable dt = (DataTable)ViewState["dtPOMainDetail"];
            if (dt != null && dt.Rows.Count > 0)
            {

                if (FilterOption.Equals("ALL"))
                {
                    gvMaterialpo.DataSource = dt;
                    gvMaterialpo.DataBind();
                    lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                }
                else
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "PO_STATUS='" + FilterOption + "'";

                    lblTotalRecord.Text = dv.Count.ToString().Trim();
                    if (dv.Count > 0)
                    {
                        gvMaterialpo.DataSource = dv;
                        gvMaterialpo.DataBind();
                    }
                }
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
        //imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        //imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        //imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void gvMaterialpo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPO_NUMB = (Label)e.Row.FindControl("lblPO_NUMB");
                int PO_NUMB = int.Parse(lblPO_NUMB.Text.Trim());

                Label lblPO_type = (Label)e.Row.FindControl("lblPO_type");
                string PO_Type = lblPO_type.Text.Trim();

                SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();

                oTX_ITEM_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_ITEM_PU_MST.PO_NUMB = PO_NUMB;
                oTX_ITEM_PU_MST.PO_TYPE = PO_Type;
                oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                DataTable dtPODetail = SaitexBL.Interface.Method.Material_Purchase_Order.Select_TransactionByPONumber(oTX_ITEM_PU_MST);

                GridView grdPODetail = (GridView)e.Row.FindControl("grdPODetail");

                if (dtPODetail != null)
                {
                    grdPODetail.DataSource = dtPODetail;
                    grdPODetail.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string FilterOption = ddlFilterOption.SelectedValue.Trim();

            bindMaterialPOApproval(FilterOption);
        }
        catch (Exception ex)
        {
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/MaterialPOList.aspx", false);
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

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
}
