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
using Obout.ComboBox;
using System.Collections.Generic;

public partial class Module_NewFiber_Controls_fiber_QC_Already_Checked : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (!IsPostBack)
                {
                    InitialisePage();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }
    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Save";
            bindMaterialReceiptApproval();
        }
        catch
        {
            throw;
        }
    }
    private void bindMaterialReceiptApproval()
    {
        try
        {
            int TRN_YEAR = 0, TRN_NUMB = 0;
            string FIBER_CODE = string.Empty;
            if (ddlTRNNumber.SelectedIndex > -1)
            {

                int.TryParse(hdYEAR.Value, out TRN_YEAR);
                int.TryParse(hdTRN_NUMB.Value, out TRN_NUMB);
            }

            if (ddlyarncode.SelectedIndex > -1)
            {
                FIBER_CODE = ddlyarncode.SelectedText.Trim();
            }
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.GetQC_CheckedDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_NUMB, TRN_YEAR, FIBER_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No QC already Checked";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No QC already Checked");
            }
        }
        catch
        {
            throw;
        }
    }



    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Yarn_QC_CheckingReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=0,toolbar=0,menubar=0,location=100,scrollbars=1,resizable=1,width=800,height=500');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
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



    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }




    protected void gvMaterialReceiptApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblQC_Result = (Label)e.Row.FindControl("lblQC_Result");
            Label lblQC_Approved_Result = (Label)e.Row.FindControl("lblQC_Approved_Result");
            if (lblQC_Result.Text.ToLower() == "pass")
            {
                e.Row.Cells[15].BackColor = System.Drawing.Color.Green;
            }
            else
            {
                e.Row.Cells[15].BackColor = System.Drawing.Color.Red;
            }

            if (lblQC_Approved_Result.Text.ToLower() == "pass")
            {
                e.Row.Cells[18].BackColor = System.Drawing.Color.Green;
            }
            else if (lblQC_Approved_Result.Text.ToLower() == "fail")
            {
                e.Row.Cells[18].BackColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_QC_CheckingList.aspx");
    }


    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_QC_Checking.aspx?NYQC=true");
    }

    protected void ddlTRNNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetReceiving(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetReceivingCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Standard No selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {

            string CommandText = "SELECT   * FROM   (SELECT   * FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR, (c.TRN_TYPE||'@'||c.YEAR)as combined  FROM   FIBER_QC a, TX_FIBER_NEW_IR_MST c, TX_FIBER_NEW_MASTER d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE  AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE  AND c.FIBER_CODE = d.FIBER_CODE  AND NVL (d.QC_REQUIRED, 0) = '1'  AND c.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and TRN_NUMB not in (SELECT TRN_NUMB from (SELECT  TRN_NUMB FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR, (c.TRN_TYPE||'@'||c.YEAR)as combined FROM   FIBER_QC a, TX_FIBER_NEW_IR_MST c, TX_FIBER_NEW_MASTER d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE  AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE AND c.FIBER_CODE = d.FIBER_CODE AND NVL (d.QC_REQUIRED, 0) = '1'  AND c.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) where rownum<='" + startOffset + "')";
            }

            string SortExpression = "  ORDER BY TRN_NUMB DESC ";
            string SearchQuery = "%" + text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetReceivingCount(string text)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   * FROM   (SELECT   DISTINCT c.TRN_NUMB,c.YEAR, (c.TRN_TYPE||'@'||c.YEAR)as combined  FROM   FIBER_QC a, TX_FIBER_NEW_IR_MST c, TX_FIBER_NEW_MASTER d WHERE a.BRANCH_CODE = c.BRANCH_CODE AND a.COMP_CODE = c.COMP_CODE  AND a.TRN_TYPE = c.TRN_TYPE AND a.TRN_NUMB = c.TRN_NUMB AND c.BRANCH_CODE = d.BRANCH_CODE AND c.COMP_CODE = d.COMP_CODE  AND c.FIBER_CODE = d.FIBER_CODE  AND NVL (d.QC_REQUIRED, 0) = '1'  AND c.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND c.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (TRN_NUMB LIKE :SearchQuery)) ";
            string SearchQuery = "%" + text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", SearchQuery, "").Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int TRN_NUMB = 0;
            int.TryParse(ddlTRNNumber.SelectedText.ToString().Trim(), out TRN_NUMB);
            string data = ddlTRNNumber.SelectedValue.Trim();
            string[] arr = data.Split('@');
            if (arr.Length > 0)
            {
                int YEAR = 0;
                string TRN_TYPE = arr[0].ToString();
                int.TryParse(arr[1].ToString(), out YEAR);
                hdTRN_NUMB.Value = TRN_NUMB.ToString();
                hdTRN_TYPE.Value = TRN_TYPE;
                hdYEAR.Value = YEAR.ToString();
                bindMaterialReceiptApproval();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn QC Standard  selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset);
            ddlyarncode.Items.Clear();
            ddlyarncode.DataSource = data;
            ddlyarncode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn Code selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }


    private DataTable GetYarnData(string Text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   d.FIBER_CODE, d.FIBER_DESC  FROM   TX_FIBER_NEW_MASTER d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE) WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND FIBER_CODE NOT IN ( select FIBER_CODE from ( SELECT   FIBER_CODE FROM   (SELECT   d.FIBER_CODE, d.FIBER_DESC  FROM   TX_FIBER_NEW_MASTER d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE)    WHERE  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by FIBER_CODE";
            string SearchQuery = "%" + Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");


        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text)
    {

        string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   d.FIBER_CODE, d.FIBER_DESC  FROM   TX_FIBER_NEW_MASTER d WHERE   NVL (d.QC_REQUIRED, 0) = '1' AND d.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND d.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE)";
        string SearchQuery = "%" + text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", SearchQuery, "").Rows.Count;

    }


    protected void ddlyarncode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindMaterialReceiptApproval();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

        }

    }

}
