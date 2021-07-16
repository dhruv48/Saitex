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

public partial class Module_FA_Controls_AdvancedAdviceConfirm : System.Web.UI.UserControl
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
            BindAdviceConfirmGrid();
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

            DataTable dtAdviceConfirm = CreateDataTable();
            int totalRows = grdAdvancedAdviceConfirmation.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdAdvancedAdviceConfirmation.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblAdviceNo = (Label)thisGridViewRow.FindControl("lblAdviceNo");
                    CheckBox Confirmed = (CheckBox)thisGridViewRow.FindControl("chkConfirmed");

                    if (Confirmed.Checked == true)
                    {
                        DataRow dr = dtAdviceConfirm.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["ADV_NO"] = lblAdviceNo.Text.Trim();
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "1";
                        dtAdviceConfirm.Rows.Add(dr);
                        Confirmed.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.Update_AdvancedAdviceConfirm(dtAdviceConfirm);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Advanced Advice Confirmed Successfully!");
                BindAdviceConfirmGrid();
            }
            else
            {
                CommonFuction.ShowMessage("Problem in Confirmed Advanced Advices!");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            DataTable dtAdviceConfirm = CreateDataTable();
            int totalRows = grdAdvancedAdviceConfirmation.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdAdvancedAdviceConfirmation.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblAdviceNo = (Label)thisGridViewRow.FindControl("lblAdviceNo");
                    CheckBox Confirmed = (CheckBox)thisGridViewRow.FindControl("chkConfirmed");

                    if (Confirmed.Checked == false)
                    {
                        DataRow dr = dtAdviceConfirm.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["ADV_NO"] = lblAdviceNo.Text.Trim();
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "0";
                        dtAdviceConfirm.Rows.Add(dr);
                        Confirmed.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.Update_AdvancedAdviceUnConfirm(dtAdviceConfirm);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Advanced Advices UnConfirmed Successfully.");
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
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
            BindComboUnConfirmedAdvices();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in find.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/AdvancedAdviceConfirmation.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
        }
    }

    private void BindAdviceConfirmGrid()
    {
        try
        {
            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();

            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectAdvicesWithFY(oFA_ADVANCED_ADVICE);

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

                grdAdvancedAdviceConfirmation.DataSource = dt;
                grdAdvancedAdviceConfirmation.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Advanced Advices for confirmation..";
                grdAdvancedAdviceConfirmation.DataSource = null;
                grdAdvancedAdviceConfirmation.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Advanced Advices for confirmation..");
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

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtAdviceConfirm = new DataTable();
            dtAdviceConfirm.Columns.Add("COMP_CODE", typeof(string));
            dtAdviceConfirm.Columns.Add("BRANCH_CODE", typeof(string));
            dtAdviceConfirm.Columns.Add("YEAR", typeof(int));
            dtAdviceConfirm.Columns.Add("ADV_NO", typeof(string));
            dtAdviceConfirm.Columns.Add("CONF_FLAG", typeof(string));
            dtAdviceConfirm.Columns.Add("CONF_DATE", typeof(DateTime));
            dtAdviceConfirm.Columns.Add("CONF_BY", typeof(string));
            return dtAdviceConfirm;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Here, we are binding in Combo Box for Unconfirmation Advices.
    /// </summary>
    private void BindComboUnConfirmedAdvices()
    {
        try
        {
            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();

            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectUnConfirmedAdvancedAdvices(oFA_ADVANCED_ADVICE);

            cmbAdvancedAdvice.Items.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbAdvancedAdvice.DataSource = dt;
                cmbAdvancedAdvice.DataBind();
            }
            else
            {
                lblTotalRecord.Text = "No Advanced Advices for Unconfirmation..";
                grdAdvancedAdviceConfirmation.DataSource = null;
                grdAdvancedAdviceConfirmation.DataBind();
                CommonFuction.ShowMessage("No Advanced Advices for Unconfirmation..");
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
            BindComboUnConfirmedAdvices();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Advanced Advices..\r\nSee error log for detail."));
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

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectUnConfirmedAdvancedAdvices(oFA_ADVANCED_ADVICE);

            if (dt != null && dt.Rows.Count > 0)
            {
                grdAdvancedAdviceConfirmation.DataSource = null;
                grdAdvancedAdviceConfirmation.DataBind();

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

                DataView dv = new DataView(dt);
                dv.RowFilter = "ADV_NO='" + cmbAdvancedAdvice.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        grdAdvancedAdviceConfirmation.DataSource = dv;
                        grdAdvancedAdviceConfirmation.DataBind();
                    }
                }

                int totalRows = grdAdvancedAdviceConfirmation.Rows.Count;
                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = grdAdvancedAdviceConfirmation.Rows[r];
                    if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox Confirmed = (CheckBox)thisGridViewRow.FindControl("chkConfirmed");
                        Confirmed.Checked = true;
                    }
                }
                trgrid.Visible = true;
            }
            else
            {
                lblTotalRecord.Text = "No Advanced Advices for Unconfirmation..";
                grdAdvancedAdviceConfirmation.DataSource = null;
                grdAdvancedAdviceConfirmation.DataBind();
                CommonFuction.ShowMessage("No Advanced Advices for Unconfirmation..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Advanced Advice Selection.\r\nSee error log for detail."));
        }
    }

    protected void grdAdvancedAdviceConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAdvancedAdviceConfirmation.PageIndex = e.NewPageIndex;
            BindAdviceConfirmGrid();
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
                    BindAdviceConfirmGridWithDate();
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

    private void BindAdviceConfirmGridWithDate()
    {
        try
        {
            oFA_ADVANCED_ADVICE = new SaitexDM.Common.DataModel.FA_ADVANCED_ADVICE();

            oFA_ADVANCED_ADVICE.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_ADVANCED_ADVICE.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_ADVANCED_ADVICE.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_ADVANCED_ADVICE.SelectAdvicesWithFYAndDate(oFA_ADVANCED_ADVICE, txtstartdate.Text.Trim(), txtenddate.Text.Trim());

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

                grdAdvancedAdviceConfirmation.DataSource = dt;
                grdAdvancedAdviceConfirmation.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Advanced Advices for confirmation..";
                grdAdvancedAdviceConfirmation.DataSource = null;
                grdAdvancedAdviceConfirmation.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Advanced Advices for confirmation..");
            }
        }
        catch
        {
            throw;
        }
    }
}
