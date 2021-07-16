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

public partial class Module_Inventory_Controls_MaterialClosing : System.Web.UI.UserControl
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
                // bindYarnRestApprovedIndent();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    protected override void OnInit(EventArgs e)
    {
        ddlDepartment.OnTextChanged += new CommonControls_LOV_DepartmentLOV.RefreshDataGridView(ddlDepartment_OnTextChanged);
        base.OnInit(e);
    }

    void ddlDepartment_OnTextChanged(string Value, string Text)
    {
        try
        {
            bindYarnRestApprovedIndent();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department selection"));
        }
    }

    private void BlanksConrols()
    {
        try
        {
            int totalRows = gvYarnIndentClosing.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvYarnIndentClosing.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label IndentId = (Label)thisGridViewRow.FindControl("lblIndentTrnId");
                    TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    ApprovedQty.Text = "";
                    Approved.Checked = false;
                    ConfirmDate.Text = "";
                    ConfirmBy.Text = "";
                    Remarks.Text = "";
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding the data..\r\nSee error log for detail."));
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtIndentDetail = new DataTable();
            dtIndentDetail.Columns.Add("COMP_CODE", typeof(string));
            dtIndentDetail.Columns.Add("BRANCH_CODE", typeof(string));
            dtIndentDetail.Columns.Add("INDENT_TYPE", typeof(string));
            dtIndentDetail.Columns.Add("IND_NUMB", typeof(int));
            dtIndentDetail.Columns.Add("ITEM_CODE", typeof(string));
            dtIndentDetail.Columns.Add("IND_CLOSE_FLAG", typeof(string));
            dtIndentDetail.Columns.Add("IND_CLOSE_BY", typeof(string));
            dtIndentDetail.Columns.Add("IND_CLOSE_DATE", typeof(string));
            dtIndentDetail.Columns.Add("IND_CLOSE_REMARKS", typeof(string));
            return dtIndentDetail;
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
            DataTable dtIndentDetail = CreateDataTable();
            int totalRows = gvYarnIndentClosing.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvYarnIndentClosing.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblInd_NUMB = (Label)thisGridViewRow.FindControl("lblInd_NUMB");
                    Label lblItemCode = (Label)thisGridViewRow.FindControl("lblItemCode");
                    //TextBox lApprovedQty = (TextBox)thisGridViewRow.FindControl("TextBox1");

                    // TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    //TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    //TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    if (Approved.Checked == true)
                    {

                        //int iApprovedQty = Convert.ToInt32(ApprovedQty.Text.Trim());
                        //DateTime dConfirmDate = Convert.ToDateTime(ConfirmDate.Text.Trim());
                        //string strConfirmBy = ConfirmBy.Text.Trim();
                        // string strRemarks = Remarks.Text.Trim();

                        DataRow dr = dtIndentDetail.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["INDENT_TYPE"] = lblItemCode.ToolTip.Trim();
                        dr["IND_NUMB"] = int.Parse(lblInd_NUMB.Text.Trim());
                        dr["ITEM_CODE"] = lblItemCode.Text.Trim();
                        dr["IND_CLOSE_FLAG"] = "3";
                        dr["IND_CLOSE_BY"] = oUserLoginDetail.UserCode;
                        dr["IND_CLOSE_REMARKS"] = Remarks.Text.Trim();



                        dtIndentDetail.Rows.Add(dr);

                        Approved.Checked = false;

                        Remarks.Text = "";

                    }
                }
            }
            if (msg != string.Empty)
            {
                CommonFuction.ShowMessage(msg);
            }
            int iResult = SaitexBL.Interface.Method.TX_ITEM_IND_MST.Update_TransactionForClosingMaterial(oUserLoginDetail.UserCode, dtIndentDetail);
            if (iResult > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('Indent Closed Successfully');", true);

                lblMode.Text = "Find";
                bindYarnRestApprovedIndent();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/YarnPrintIndent1.aspx");
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./MaterialIndentClosing.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the page.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected DataTable GetItems(string text)
    {
        try
        {
            string whereClause = " WHERE c.comp_code = d.comp_code AND c.branch_code = d.branch_code AND LTRIM (RTRIM (d.IND_TYPE)) = LTRIM (RTRIM (c.IND_TYPE)) AND LTRIM (RTRIM (d.IND_NUMB)) = LTRIM (RTRIM (c.IND_NUMB)) AND LTRIM (RTRIM (d.ITEM_CODE)) = LTRIM (RTRIM (a.ITEM_CODE)) AND LTRIM (RTRIM (a.ITEM_CODE)) = LTRIM (RTRIM (b.ITEM_CODE)) AND c.DEPT_CODE = dp.DEPT_CODE AND (NVL (d.APPR_QTY, 0) > 0) AND (NVL (d.APPR_QTY, 0) <> NVL (d.PUR_ADJ_QTY, 0)) AND (d.COMP_CODE = :Comp_Code) AND (D.BRANCH_CODE = :Branch_Code) AND (b.YEAR = :YEAR) AND (NVL (d.IND_CLOSE_FLAG, 0) <> 3) ";
            string sortExpression = " ORDER BY c.IND_NUMB";

            string commandText = "SELECT a.DEPT_CODE, a.ITEM_CODE, NVL (b.OP_BAL_STOCK, 0) + NVL (b.YTD_RCPT, 0) - NVL (b.YTD_ISS, 0) AS currentStock, DP.DEPT_NAME, NVL (d.APPR_QTY, 0) AS APPR_QTY, NVL b.OP_RATE, 0) * NVL (d.RQST_QTY, 0) AS iValue, a.MIN_STOCK_LVL, NVL (b.OP_RATE, 0) OP_RATE, a.ITEM_DESC, c.IND_NUMB, d.IND_TYPE, c.IND_DATE, c.PREP_BY, c.REQD_DATE, c.CONF_BY, c.CONF_DATE, c.CONF_COMMENT, d.DPT_CONF_DATE, NVL (d.RQST_QTY, 0) RQST_QTY, d.DPT_REMARK, d.COMP_CODE, d.BRANCH_CODE, NVL (d.PUR_ADJ_QTY, 0) PUR_ADJ_QTY, NVL (d.APPR_QTY, 0) APPR_QTY, IND_CLOSE_FLAG FROM TX_ITEM_IND_TRN d, TX_ITEM_IND_MST c, TX_ITEM_MST a, TX_ITEM_OP_BAL b, CM_DEPT_MST dp ";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }
    private void gvYarnIndentClosing_paging(object sender,GridViewPageEventArgs e)
    {
        gvYarnIndentClosing.PageIndex = e.NewPageIndex;
        bindYarnRestApprovedIndent();
 
    }
    private void bindYarnRestApprovedIndent()
    {
        try
        {
            string whereClause = " WHERE c.comp_code = d.comp_code AND c.branch_code = d.branch_code AND LTRIM (RTRIM (d.IND_TYPE)) = LTRIM (RTRIM (c.IND_TYPE)) AND LTRIM (RTRIM (d.IND_NUMB)) = LTRIM (RTRIM (c.IND_NUMB)) AND LTRIM (RTRIM (d.ITEM_CODE)) = LTRIM (RTRIM (a.ITEM_CODE)) AND LTRIM (RTRIM (a.ITEM_CODE)) = LTRIM (RTRIM (b.ITEM_CODE)) AND c.DEPT_CODE = dp.DEPT_CODE AND (NVL (d.APPR_QTY, 0) > 0) AND (NVL (d.APPR_QTY, 0) <> NVL (d.PUR_ADJ_QTY, 0)) AND (d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') AND (D.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "') AND (NVL (d.IND_CLOSE_FLAG, 0) <> 3) and d.IND_NUMB like :SearchQuery ";

            if (ddlDepartment.SelectedValue.ToString() != "SELECT")
            {
                whereClause += " and c.DEPT_CODE='" + ddlDepartment.SelectedValue.ToString() + "' ";
            }

            int IndNo = 0;
            int.TryParse(txtIndNo.Text, out IndNo);
            if (IndNo > 0)
            {
                whereClause += " and c.IND_NUMB=" + IndNo;
            }

            string sortExpression = " ORDER BY c.IND_NUMB";

            string commandText = "SELECT a.DEPT_CODE, a.ITEM_CODE, NVL (b.OP_BAL_STOCK, 0) + NVL (b.YTD_RCPT, 0) - NVL (b.YTD_ISS, 0) AS currentStock, DP.DEPT_NAME, NVL (d.APPR_QTY, 0) AS APPR_QTY, NVL( b.OP_RATE, 0) * NVL (d.RQST_QTY, 0) AS iValue, a.MIN_STOCK_LVL, NVL (b.OP_RATE, 0) OP_RATE, a.ITEM_DESC, c.IND_NUMB, d.IND_TYPE, c.IND_DATE, c.PREP_BY, c.REQD_DATE, c.CONF_BY, c.CONF_DATE, c.CONF_COMMENT, d.DPT_CONF_DATE, NVL (d.RQST_QTY, 0) RQST_QTY, d.DPT_REMARK, d.COMP_CODE, d.BRANCH_CODE, NVL (d.PUR_ADJ_QTY, 0) PUR_ADJ_QTY, NVL (d.APPR_QTY, 0) APPR_QTY, IND_CLOSE_FLAG FROM TX_ITEM_IND_TRN d, TX_ITEM_IND_MST c, TX_ITEM_MST a, TX_ITEM_OP_BAL b, CM_DEPT_MST dp ";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", "%", sPO);

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

                gvYarnIndentClosing.DataSource = dt;
                gvYarnIndentClosing.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                //lblTotalRecord.Text = "No Indent for Closing";
                Common.CommonFuction.ShowMessage("There is No Material Indent for Closing..");
                gvYarnIndentClosing.DataSource = null;
                gvYarnIndentClosing.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        //imgbtnUpdate.Attributes.Add("Onclick", "return confirm('Are you sure to update the record?');");
        //imgbtnDelete.Attributes.Add("Onclick", "return confirm('Are you sure to delete the record ?');");
        //imgbtnPrint.Attributes.Add("Onclick", "return confirm('Are you sure to print the record?');");
        //imgbtnClear.Attributes.Add("Onclick", "return confirm('Are you sure to clear the record ?');");
        //imgbtnExit.Attributes.Add("Onclick", "return confirm('Are you sure to exit the record?');");
        //imgbtnHelp.Attributes.Add("Onclick", "return confirm('Are you sure to see help?');");      

    }
    protected void imgbtnFindTop_Click1(object sender, ImageClickEventArgs e)
    {

    }

    protected void txtIndNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bindYarnRestApprovedIndent();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department selection"));
        }
    }

    protected void gvYarnIndentClosing_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvYarnIndentClosing.PageIndex = e.NewPageIndex;
        bindYarnRestApprovedIndent();
       
    }
}
