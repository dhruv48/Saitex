using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DBLibrary;
using Common;
using errorLog;


public partial class Module_Inventory_Pages_IndentPendingForPO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bindMaterialIndentApproval();
               // imgbtnPrint.Attributes.Add("onclick", "javascript:CallPrint('divPrint');");
                btnPrint.Attributes.Add("onclick", "javascript:CallPrint('divPrint');");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

 

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }
  
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    string msg = string.Empty;

        //    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        //    DataTable dtIndentDetail = CreateDataTable();
        //    int totalRows = gvMaterialIndentApproval.Rows.Count;
        //    for (int r = 0; r < totalRows; r++)
        //    {
        //        GridViewRow thisGridViewRow = gvMaterialIndentApproval.Rows[r];
        //        if (thisGridViewRow.RowType == DataControlRowType.DataRow)
        //        {
        //            Label lblInd_NUMB = (Label)thisGridViewRow.FindControl("lblInd_NUMB");
        //            Label lblItemCode = (Label)thisGridViewRow.FindControl("lblItemCode");
        //            Label lApprovedQty = (Label)thisGridViewRow.FindControl("lblApprovedQty");
        //            TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
        //            CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
        //            TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
        //            TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
        //            TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

        //            if (Approved.Checked == true)
        //            {
        //                double iApprovedQty = Convert.ToDouble(ApprovedQty.Text.Trim());
        //                double ireqQty = Convert.ToDouble(lApprovedQty.Text.Trim());
        //                if (iApprovedQty <= ireqQty)
        //                {
        //                    DateTime dConfirmDate = Convert.ToDateTime(ConfirmDate.Text.Trim());
        //                    string strConfirmBy = ConfirmBy.Text.Trim();
        //                    string strRemarks = Remarks.Text.Trim();

        //                    DataRow dr = dtIndentDetail.NewRow();

        //                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
        //                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
        //                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
        //                    dr["INDENT_TYPE"] = lblItemCode.ToolTip.Trim();
        //                    dr["IND_NUMB"] = int.Parse(lblInd_NUMB.Text.Trim());
        //                    dr["ITEM_CODE"] = lblItemCode.Text.Trim();
        //                    dr["APPR_QTY"] = iApprovedQty;
        //                    dr["PUR_CONF_DATE"] = dConfirmDate;
        //                    dr["PUR_CONF_BY"] = oUserLoginDetail.UserCode;
        //                    dr["PUR_REMARK"] = strRemarks;
        //                    dtIndentDetail.Rows.Add(dr);
        //                    ApprovedQty.Text = "";
        //                    Approved.Checked = false;
        //                    ConfirmDate.Text = "";
        //                    ConfirmBy.Text = "";
        //                    Remarks.Text = "";
        //                }
        //                else
        //                {
        //                    msg += "Approved Quantity can not be more than requested Quantity for Item Code : " + lblItemCode.Text + " of Indent Number : " + lblInd_NUMB.Text;
        //                }
        //            }
        //        }
        //    }

        //    if (msg != string.Empty)
        //        CommonFuction.ShowMessage(msg);

        //    int iResult = SaitexBL.Interface.Method.TX_ITEM_IND_MST.Update_TransactionForApproval(oUserLoginDetail.UserCode, dtIndentDetail);
        //    if (iResult > 0)
        //    {
        //        lblMode.Text = "Find";
        //        CommonFuction.ShowMessage("Indent approved Successfully.");
        //        bindMaterialIndentApproval();
        //    }

        //}
        //catch
        //{
        //    throw;
        //}
    }

    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/Module/Inventory/Pages/MaterialIndentApproval.aspx", false);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void bindMaterialIndentApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            SaitexDM.Common.DataModel.TX_ITEM_IND_MST oTX_ITEM_IND_MST = new SaitexDM.Common.DataModel.TX_ITEM_IND_MST();
            oTX_ITEM_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetPendingIndentForPO(oTX_ITEM_IND_MST);
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

                int x;
                if (dt.Columns.Contains("REQD_DATE"))
                {
                    x = 1;
                }
                else
                {
                    x = 2;
                }
                int z = x;

                DataView dvIndent = new DataView(dt);
                dvIndent.RowFilter = "YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'";
                if (dvIndent.Count > 0)
                {
                    gvMaterialIndentApproval.DataSource = dvIndent;
                    gvMaterialIndentApproval.DataBind();
                    lblTotalRecord.Text = dvIndent.Count.ToString().Trim();
                }
                else
                {
                    lblTotalRecord.Text = "No Indent for approval";
                    gvMaterialIndentApproval.DataSource = null;
                    gvMaterialIndentApproval.DataBind();
                    lblTotalRecord.Text = "0";
                }
            }
            else
            {
                lblTotalRecord.Text = "No Indent for approval";
                gvMaterialIndentApproval.DataSource = null;
                gvMaterialIndentApproval.DataBind();
                lblTotalRecord.Text = "0";
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
        //imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        //imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }
}
