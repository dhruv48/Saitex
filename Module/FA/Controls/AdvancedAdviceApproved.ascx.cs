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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_AdvancedAdviceApproved : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE oFA_ADVANCED_ADVICE;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            trAdvice.Visible = false;
            trDate.Visible = true;
            txtstartdate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txtenddate.Text = Common.CommonFuction.GetYearEndDate(DateTime.Parse(txtstartdate.Text.Trim())).ToString();
            BindAdviceApprovalGrid();
        }
        catch
        {
            throw;
        }
    }
    
    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            DataTable dtAdviceApproved = CreateDataTable();
            int totalRows = grdAdvancedAdviceApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdAdvancedAdviceApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblAdviceNo = (Label)thisGridViewRow.FindControl("lblAdviceNo");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");

                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtAdviceApproved.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["ADV_NO"] = lblAdviceNo.Text.Trim();
                        dr["APPR_DATE"] = DateTime.Now.Date.ToString();
                        dr["APPR_BY"] = oUserLoginDetail.UserCode;
                        dr["APPR_FLAG"] = "1";
                        dtAdviceApproved.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.Update_AdvancedAdviceApproval(dtAdviceApproved);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Advanced Advice Approved Successfully!");
                BindAdviceApprovalGrid();
            }
            else
            {
                CommonFuction.ShowMessage("Problem in Approval..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            DataTable dtAdviceApproved = CreateDataTable();
            int totalRows = grdAdvancedAdviceApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdAdvancedAdviceApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblAdviceNo = (Label)thisGridViewRow.FindControl("lblAdviceNo");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");

                    if (Approved.Checked == false)
                    {
                        DataRow dr = dtAdviceApproved.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["ADV_NO"] = lblAdviceNo.Text.Trim();
                        dr["APPR_DATE"] = DateTime.Now.Date.ToString();
                        dr["APPR_BY"] = oUserLoginDetail.UserCode;
                        dr["APPR_FLAG"] = "0";
                        dtAdviceApproved.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.Update_AdvancedAdviceUnApproval(dtAdviceApproved);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Advanced Advices UnApproved Successfully!");
                InitialisePage();
            }
            else
            {
                CommonFuction.ShowMessage("Problem in UnApproval..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            lblMode.Text = "Update";
            trAdvice.Visible = true;
            trTotalRecord.Visible = false;
            trgrid.Visible = false;
            BindComboForUnApproved();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Find..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/AdvancedAdviceApproval.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping..\r\nSee error log for detail."));
        }
    }
    
    private void BindAdviceApprovalGrid()
    {
        try
        {
            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();

            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectAdvicesWithFYOnlyConfirm(oFA_ADVANCED_ADVICE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("APPR_DATE"))
                    dt.Columns.Add("APPR_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("APPR_BY"))
                    dt.Columns.Add("APPR_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string Appr_By = dr["APPR_BY"].ToString();
                    if (Appr_By == "")
                        dr["APPR_BY"] = oUserLoginDetail.Username;

                    dr["APPR_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }

                grdAdvancedAdviceApproval.DataSource = dt;
                grdAdvancedAdviceApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Advanced Advices for Approval..";
                grdAdvancedAdviceApproval.DataSource = null;
                grdAdvancedAdviceApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Advanced Advices for Approval..");
            }
        }
        catch
        {
            throw;
        }
    }
    
    protected void cmbAdvancedAdvice_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindComboForUnApproved();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    
    protected void cmbAdvancedAdvice_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();

            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectUnApprovedAdvancedAdvices(oFA_ADVANCED_ADVICE);

            if (dt != null && dt.Rows.Count > 0)
            {
                grdAdvancedAdviceApproval.DataSource = null;
                grdAdvancedAdviceApproval.DataBind();

                if (!dt.Columns.Contains("APPR_DATE"))
                    dt.Columns.Add("APPR_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("APPR_BY"))
                    dt.Columns.Add("APPR_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string Appr_By = dr["APPR_BY"].ToString();
                    if (Appr_By == "")
                        dr["APPR_BY"] = oUserLoginDetail.UserCode;

                    dr["APPR_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }

                DataView dv = new DataView(dt);
                dv.RowFilter = "ADV_NO='" + cmbAdvancedAdvice.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        grdAdvancedAdviceApproval.DataSource = dv;
                        grdAdvancedAdviceApproval.DataBind();
                    }
                }

                int totalRows = grdAdvancedAdviceApproval.Rows.Count;
                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = grdAdvancedAdviceApproval.Rows[r];
                    if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                        Approved.Checked = true;
                    }
                }
                trgrid.Visible = true;
            }
            else
            {
                lblTotalRecord.Text = "No Advanced Advices for UnApproval.";
                grdAdvancedAdviceApproval.DataSource = null;
                grdAdvancedAdviceApproval.DataBind();
                CommonFuction.ShowMessage("No Advanced Advices for UnApproval.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Advice selection..\r\nSee error log for detail."));
        }
    }
    
    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtAdviceApproved = new DataTable();
            dtAdviceApproved.Columns.Add("COMP_CODE", typeof(string));
            dtAdviceApproved.Columns.Add("BRANCH_CODE", typeof(string));
            dtAdviceApproved.Columns.Add("YEAR", typeof(int));
            dtAdviceApproved.Columns.Add("ADV_NO", typeof(string));
            dtAdviceApproved.Columns.Add("APPR_FLAG", typeof(string));
            dtAdviceApproved.Columns.Add("APPR_DATE", typeof(DateTime));
            dtAdviceApproved.Columns.Add("APPR_BY", typeof(string));
            return dtAdviceApproved;
        }
        catch
        {
            throw;
        }
    }
    
    /// <summary>
    /// Here, we are binding the Combo Box for UnApproved.
    /// </summary>
    private void BindComboForUnApproved()
    {
        try
        {
            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();

            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectUnApprovedAdvancedAdvices(oFA_ADVANCED_ADVICE);

            cmbAdvancedAdvice.Items.Clear();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbAdvancedAdvice.DataSource = dt;
                cmbAdvancedAdvice.DataBind();
            }
            else
            {
                lblTotalRecord.Text = "No Advanced Advices for UnApproval.";
                grdAdvancedAdviceApproval.DataSource = null;
                grdAdvancedAdviceApproval.DataBind();
                CommonFuction.ShowMessage("No Advanced Advices for UnApproval.");
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
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    
    protected void grdAdvancedAdviceApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAdvancedAdviceApproval.PageIndex = e.NewPageIndex;
            BindAdviceApprovalGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtstartdate.Text != "")
            {
                if (txtenddate.Text != "")
                {
                    BindAdviceApprovalGridWithDate();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please select Ending Date..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Starting Date..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Searching Records..\r\nSee error log for detail."));
        }
    }

    private void BindAdviceApprovalGridWithDate()
    {
        try
        {
            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();

            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectAdvicesWithFYOnlyConfirmWithDate(oFA_ADVANCED_ADVICE, txtstartdate.Text.Trim(), txtenddate.Text.Trim());

            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("APPR_DATE"))
                    dt.Columns.Add("APPR_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("APPR_BY"))
                    dt.Columns.Add("APPR_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string Appr_By = dr["APPR_BY"].ToString();
                    if (Appr_By == "")
                        dr["APPR_BY"] = oUserLoginDetail.Username;

                    dr["APPR_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }

                grdAdvancedAdviceApproval.DataSource = dt;
                grdAdvancedAdviceApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Advanced Advices for Approval..";
                grdAdvancedAdviceApproval.DataSource = null;
                grdAdvancedAdviceApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Advanced Advices for Approval..");
            }
        }
        catch
        {
            throw;
        }
    }
}
