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
public partial class Module_Yarn_SalesWork_Controls_Yarn_Receipt_UnApproval : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bindMaterialReceiptApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }
    private DataTable CreateDataTable()
    {
        DataTable dtReceiptDetail = new DataTable();
        dtReceiptDetail.Columns.Add("YEAR", typeof(int));
        dtReceiptDetail.Columns.Add("COMP_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_NUMB", typeof(int));
        dtReceiptDetail.Columns.Add("PARTY_DATA", typeof(string));
        dtReceiptDetail.Columns.Add("PRTY_CH_NUMB", typeof(string));
        dtReceiptDetail.Columns.Add("GATE_NUMB", typeof(string));
        dtReceiptDetail.Columns.Add("CONF_FLAG", typeof(string));
        dtReceiptDetail.Columns.Add("CONF_DATE", typeof(DateTime));
        dtReceiptDetail.Columns.Add("CONF_BY", typeof(string));
        return dtReceiptDetail;
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtReceiptDetail = CreateDataTable();
            int totalRows = gvMaterialReceiptApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialReceiptApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblTRN_NUMB = (Label)thisGridViewRow.FindControl("lblTRN_NUMB");
                    Label lblTRN_type = (Label)thisGridViewRow.FindControl("lblTRN_type");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    //TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    //TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");

                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtReceiptDetail.NewRow();

                        dr["YEAR"] = int.Parse(lblTRN_type.ToolTip.Trim());
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["TRN_TYPE"] = lblTRN_type.Text.Trim();
                        dr["TRN_NUMB"] = int.Parse(lblTRN_NUMB.Text.Trim());
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "0";
                        dtReceiptDetail.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.YRN_IR_MST.Update_ReceiptForApproval(oUserLoginDetail.UserCode, dtReceiptDetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                CommonFuction.ShowMessage("Transaction unapproved Successfully.");
                bindMaterialReceiptApproval();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in MRN Confirm.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/YRN_ReceiptPermRPT.aspx");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/Saitex/Module/Yarn/SalesWork/Pages/Receipt_Approval.aspx", false);
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
    private void bindMaterialReceiptApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetReceiptDataForUnApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "YARN");
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
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No MRN for approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No MRN for approval");
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
    protected void gvMaterialReceiptApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;
                Label lblTRN_NUMB = (Label)grdRow.FindControl("lblTRN_NUMB");
                Label lblTRN_TYPE = (Label)grdRow.FindControl("lblTRN_TYPE");
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                SaitexDM.Common.DataModel.YRN_IR_MST oTX_YRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
                oTX_YRN_IR_MST.YEAR = int.Parse(lblTRN_TYPE.ToolTip.Trim());
                oTX_YRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_YRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_YRN_IR_MST.TRN_NUMB = int.Parse(lblTRN_NUMB.Text.Trim());
                oTX_YRN_IR_MST.TRN_TYPE = lblTRN_TYPE.Text.Trim();
                DataTable dtTRN = SaitexBL.Interface.Method.YRN_IR_MST.GetTRN_DataByTRN_NUMBst(int.Parse(lblTRN_TYPE.ToolTip.Trim()), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, int.Parse(lblTRN_NUMB.Text.Trim()), lblTRN_TYPE.Text.Trim());

                if (dtTRN != null && dtTRN.Rows.Count > 0)
                {
                    GridView grdTRN = (GridView)grdRow.FindControl("grdTRN");
                    grdTRN.DataSource = dtTRN;
                    grdTRN.DataBind();
                }
            }
        }
        catch (Exception exe)
        {
            throw exe;
        }
    }
    protected void gvMaterialReceiptApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMaterialReceiptApproval.PageIndex = e.NewPageIndex;
        bindMaterialReceiptApproval();
    }
}
