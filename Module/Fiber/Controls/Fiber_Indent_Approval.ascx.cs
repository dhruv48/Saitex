using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using errorLog;

public partial class Module_Fiber_Controls_Fiber_Indent_Approval : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                if (!IsPostBack)
                {
                    lblMode.Text = "Find";
                    bindFabricIndentApproval();
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page loading"));
        }
    }
    private void BlanksConrols()
    {
        try
        {
            int totalRows = gvMaterialIndentApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialIndentApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label IndentId = (Label)thisGridViewRow.FindControl("lblIndentTrnId");
                    TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");
                    ApprovedQty.Text = string.Empty;
                    Approved.Checked = false;
                    ConfirmDate.Text = string.Empty;
                    ConfirmBy.Text = string.Empty;
                    Remarks.Text = string.Empty;
                }
            }
        }
        catch 
        {
            throw;
        }
    }    
    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtIndentDetail = new DataTable();
            dtIndentDetail.Columns.Add("COMP_CODE", typeof(string));
            dtIndentDetail.Columns.Add("BRANCH_CODE", typeof(string));
            dtIndentDetail.Columns.Add("YEAR", typeof(string));
            dtIndentDetail.Columns.Add("IND_TYPE", typeof(string));
            dtIndentDetail.Columns.Add("IND_NUMB", typeof(int));
            dtIndentDetail.Columns.Add("FABR_CODE", typeof(string));
            dtIndentDetail.Columns.Add("APPR_QTY", typeof(int));
            dtIndentDetail.Columns.Add("PUR_CONF_DATE", typeof(DateTime));
            dtIndentDetail.Columns.Add("PUR_CONF_BY", typeof(string));
            dtIndentDetail.Columns.Add("PUR_REMARK", typeof(string));
            return dtIndentDetail;
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtIndentDetail = CreateDataTable();
            int totalRows = gvMaterialIndentApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialIndentApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblInd_NUMB = (Label)thisGridViewRow.FindControl("lblInd_NUMB");
                    Label lblFabricCode = (Label)thisGridViewRow.FindControl("lblFabricCode");
                    Label lApprovedQty = (Label)thisGridViewRow.FindControl("lblApprovedQty");
                    TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");
                    if (Approved.Checked == true)
                    {
                        int iApprovedQty = Convert.ToInt32(ApprovedQty.Text.Trim());
                        DateTime dConfirmDate = Convert.ToDateTime(ConfirmDate.Text.Trim());
                        string strConfirmBy = ConfirmBy.Text.Trim();
                        string strRemarks = Remarks.Text.Trim();
                        DataRow dr = dtIndentDetail.NewRow();
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["IND_TYPE"] = lblFabricCode.ToolTip.Trim();
                        dr["IND_NUMB"] = int.Parse(lblInd_NUMB.Text.Trim());
                        dr["FABR_CODE"] = lblFabricCode.Text.Trim();
                        dr["APPR_QTY"] = iApprovedQty;
                        dr["PUR_CONF_DATE"] = dConfirmDate;
                        dr["PUR_CONF_BY"] = strConfirmBy;
                        dr["PUR_REMARK"] = strRemarks;
                        dtIndentDetail.Rows.Add(dr);
                        ApprovedQty.Text = string.Empty;
                        Approved.Checked = false;
                        ConfirmDate.Text = string.Empty;
                        ConfirmBy.Text = string.Empty;
                        Remarks.Text = string.Empty;
                    }
                }
            }
            int iResult = SaitexBL.Interface.Method.FIBER_IND_MST.Update_TransactionForApproval(oUserLoginDetail.UserCode, dtIndentDetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Indent Approved Successfully');", true);
                bindFabricIndentApproval();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in updating record"));
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
        Response.Redirect("~/Module/Inventory/yarn/Pages/Yarn_Indent_Approval.aspx", false);
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page exit"));
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void bindFabricIndentApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.FIBER_IND_MST.GetIndentDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
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
                    double APPR_QTY = double.Parse(dr["APPR_QTY"].ToString());
                    if (APPR_QTY <= 0)
                        dr["APPR_QTY"] = double.Parse(dr["RQST_QTY"].ToString());
                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvMaterialIndentApproval.DataSource = dt;
                gvMaterialIndentApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Indent for approval";
                gvMaterialIndentApproval.DataSource = null;
                gvMaterialIndentApproval.DataBind();
            }
        }
        catch 
        {
            throw;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
            imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
            imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
            imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
            imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
        }
        catch
        {
            throw;
        }
    }   
}
