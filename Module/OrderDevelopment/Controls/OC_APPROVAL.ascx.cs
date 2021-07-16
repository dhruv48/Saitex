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

public partial class Module_OrderDevelopment_Controls_OC_APPROVAL : System.Web.UI.UserControl
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
                BindOrderApprovalData();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }

    private void BindOrderApprovalData()
    {
        try
        {
            SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetOCDataForApproval(oOD_CAPTURE_MST);

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
                grdORDER_APPROVAL.DataSource = dt;
                grdORDER_APPROVAL.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Order for approval";
                grdORDER_APPROVAL.DataSource = null;
                grdORDER_APPROVAL.DataBind();
                lblTotalRecord.Text = "0";
                Common.CommonFuction.ShowMessage("No Order for approval");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdORDER_APPROVAL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;

                Label lblBUSINESS_TYPE = (Label)grdRow.FindControl("lblBUSINESS_TYPE");
                Label lblPRODUCT_TYPE = (Label)grdRow.FindControl("lblPRODUCT_TYPE");
                Label lblORDER_CAT = (Label)grdRow.FindControl("lblORDER_CAT");
                Label lblORDER_TYPE = (Label)grdRow.FindControl("lblORDER_TYPE");
                Label lblORDER_NO = (Label)grdRow.FindControl("lblORDER_NO");

                SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
                oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oOD_CAPTURE_MST.BUSINESS_TYPE = lblBUSINESS_TYPE.Text;
                oOD_CAPTURE_MST.PRODUCT_TYPE = lblPRODUCT_TYPE.Text;
                oOD_CAPTURE_MST.ORDER_CAT = lblORDER_CAT.Text;
                oOD_CAPTURE_MST.ORDER_TYPE = lblORDER_TYPE.Text;
                oOD_CAPTURE_MST.ORDER_NO = lblORDER_NO.Text;

                DataTable dtPOTRN = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetTRN_ByORDER_NO(oOD_CAPTURE_MST);

                if (dtPOTRN != null && dtPOTRN.Rows.Count > 0)
                {
                    GridView grdTRNDetail = (GridView)grdRow.FindControl("grdTRNDetail");
                    grdTRNDetail.DataSource = dtPOTRN;
                    grdTRNDetail.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
        }
    }

    protected void grdTRNDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grdRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTRNBT = (Label)grdRow.FindControl("lblTRNBT");
                Label lblTRNPT = (Label)grdRow.FindControl("lblTRNPT");
                Label lblTRNOC = (Label)grdRow.FindControl("lblTRNOC");
                Label lblTRNOT = (Label)grdRow.FindControl("lblTRNOT");
                Label lblTRNON = (Label)grdRow.FindControl("lblTRNON");
                Label lblTRNPI_Type = (Label)grdRow.FindControl("lblTRNPI_Type");
                Label lblTRNArticalCode = (Label)grdRow.FindControl("lblTRNArticalCode");

                SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
                oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oOD_CAPTURE_MST.BUSINESS_TYPE = lblTRNBT.Text;
                oOD_CAPTURE_MST.PRODUCT_TYPE = lblTRNPT.Text;
                oOD_CAPTURE_MST.ORDER_CAT = lblTRNOC.Text;
                oOD_CAPTURE_MST.ORDER_TYPE = lblTRNOT.Text;
                oOD_CAPTURE_MST.ORDER_NO = lblTRNON.Text;

                DataTable dtTRN_COST = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetCOST_By_ARTICAL_CODE(oOD_CAPTURE_MST, lblTRNPI_Type.Text, string.Empty, lblTRNArticalCode.Text);
                DataTable dtTRN_DEL_SCHEDULE = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDELSCHEDULE_By_ARTICAL_CODE(oOD_CAPTURE_MST, lblTRNPI_Type.Text, string.Empty, lblTRNArticalCode.Text);
                DataTable dtTRN_BOM = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetBOM_By_ARTICAL_CODE(oOD_CAPTURE_MST, lblTRNPI_Type.Text, string.Empty, lblTRNArticalCode.Text);

                if (dtTRN_COST != null && dtTRN_COST.Rows.Count > 0)
                {
                    DataList dlTRNYRNSPIN_Cost = grdRow.FindControl("dlTRNYRNSPIN_Cost") as DataList;
                    dlTRNYRNSPIN_Cost.DataSource = dtTRN_COST;
                    dlTRNYRNSPIN_Cost.DataBind();
                }

                if (dtTRN_DEL_SCHEDULE != null && dtTRN_DEL_SCHEDULE.Rows.Count > 0)
                {
                    GridView grdDelSchedule = grdRow.FindControl("grdDelSchedule") as GridView;
                    grdDelSchedule.DataSource = dtTRN_DEL_SCHEDULE;
                    grdDelSchedule.DataBind();
                }

                if (dtTRN_BOM != null && dtTRN_BOM.Rows.Count > 0)
                {
                    GridView grdBOM = grdRow.FindControl("grdBOM") as GridView;
                    grdBOM.DataSource = dtTRN_BOM;
                    grdBOM.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem on row data bound.\r\nSee error log for detail."));
        }
    }

    private DataTable CreateDataTable()
    {
        DataTable dtOC = new DataTable();
        dtOC.Columns.Add("COMP_CODE", typeof(string));
        dtOC.Columns.Add("BRANCH_CODE", typeof(string));
        dtOC.Columns.Add("BUSINESS_TYPE", typeof(string));
        dtOC.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtOC.Columns.Add("ORDER_CAT", typeof(string));
        dtOC.Columns.Add("ORDER_TYPE", typeof(string));
        dtOC.Columns.Add("ORDER_NO", typeof(string));
        dtOC.Columns.Add("CONF_FLAG", typeof(string));
        dtOC.Columns.Add("CONF_DATE", typeof(DateTime));
        dtOC.Columns.Add("CONF_BY", typeof(string));
        dtOC.Columns.Add("REMARKS", typeof(string));
        return dtOC;
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            DataTable dtOCDetail = CreateDataTable();
            int totalRows = grdORDER_APPROVAL.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdORDER_APPROVAL.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblBUSINESS_TYPE = (Label)thisGridViewRow.FindControl("lblBUSINESS_TYPE");
                    Label lblPRODUCT_TYPE = (Label)thisGridViewRow.FindControl("lblPRODUCT_TYPE");
                    Label lblORDER_CAT = (Label)thisGridViewRow.FindControl("lblORDER_CAT");
                    Label lblORDER_TYPE = (Label)thisGridViewRow.FindControl("lblORDER_TYPE");
                    Label lblORDER_NO = (Label)thisGridViewRow.FindControl("lblORDER_NO");

                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");

                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtOCDetail.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["BUSINESS_TYPE"] = lblBUSINESS_TYPE.Text;
                        dr["PRODUCT_TYPE"] = lblPRODUCT_TYPE.Text;
                        dr["ORDER_CAT"] = lblORDER_CAT.Text;
                        dr["ORDER_TYPE"] = lblORDER_TYPE.Text;
                        dr["ORDER_NO"] = lblORDER_NO.Text;
                        dr["CONF_FLAG"] = "1";
                        dr["CONF_DATE"] = System.DateTime.Now.Date;
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["REMARKS"] = string.Empty;
                        dtOCDetail.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                Common.CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.OD_CAPTURE_MST.Update_OCForApproval(oUserLoginDetail.UserCode, dtOCDetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                Common.CommonFuction.ShowMessage("Order approved Successfully.");
                BindOrderApprovalData();
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Confirm.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/OrderDevelopment/Pages/OC_Approval.aspx", false);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

}
