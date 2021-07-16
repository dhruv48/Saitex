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

public partial class Module_FA_Controls_VoucherApproved : System.Web.UI.UserControl
{
    private DataTable dtJournalTrn;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST;

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
            trVoucher.Visible = false;
            trDate.Visible = true;
            txtstartdate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txtenddate.Text = Common.CommonFuction.GetYearEndDate(DateTime.Parse(txtstartdate.Text.Trim())).ToString();
            bindVoucherApprovedGrid();
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

            DataTable dtJournal = CreateDataTable();
            int totalRows = grdVoucherApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdVoucherApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblVoucherCode = (Label)thisGridViewRow.FindControl("lblVoucherCode");
                    Label lblVoucherNo = (Label)thisGridViewRow.FindControl("lblVoucherNo");
                    Label lblVoucherDate = (Label)thisGridViewRow.FindControl("lblVoucherDate");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");

                    if (Approved.Checked == false)
                    {
                        DataRow dr = dtJournal.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["VCHR_CODE"] = lblVoucherCode.Text.Trim();
                        dr["VCHR_NO"] = lblVoucherNo.Text.Trim();
                        dr["JOURNAL_DATE"] = DateTime.Parse(lblVoucherDate.Text.Trim());
                        dr["APPR_DATE"] = DateTime.Now.Date.ToString();
                        dr["APPR_BY"] = oUserLoginDetail.UserCode;
                        dr["APPR_FLAG"] = "0";
                        dtJournal.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_Journal_DTL.Update_VoucherUnApproval(oUserLoginDetail.UserCode, dtJournal);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Voucher UnApproved Successfully.");
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            lblMode.Text = "Update";
            trVoucher.Visible = true;
            trTotalRecord.Visible = false;
            trgrid.Visible = false;
            trDate.Visible = false;
            bindVoucherUnApproved();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/VoucherApproval.aspx", false);
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help..\r\nSee error log for detail."));
        }
    }

    private void bindVoucherApprovedGrid()
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstVoucherApproval(oFA_Journal_MST);
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

                grdVoucherApproval.DataSource = dt;
                grdVoucherApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Journal Voucher for Approval..";
                grdVoucherApproval.DataSource = null;
                grdVoucherApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Journal Voucher for Approval..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdVoucherApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string strVou_No = string.Empty;

            if (ViewState["dtJournalTrn"] != null)
                dtJournalTrn = (DataTable)ViewState["dtJournalTrn"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblVoucherNo = (Label)e.Row.FindControl("lblVoucherNo");
                strVou_No = lblVoucherNo.Text.Trim();

                bindJournalTrn(strVou_No);
                GridView grdJourenaldetails = (GridView)e.Row.FindControl("grdJourenaldetails");

                if (dtJournalTrn != null)
                {
                    grdJourenaldetails.DataSource = dtJournalTrn;
                    grdJourenaldetails.DataBind();
                }
                if (ViewState["dtJournalTrn"] != null)
                {
                    dtJournalTrn = (DataTable)ViewState["dtJournalTrn"];
                    dtJournalTrn = null;
                }
            }
            ViewState["dtJournalTrn"] = dtJournalTrn;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row DataBound Event..\r\nSee error log for detail."));
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtJournal = new DataTable();
            dtJournal.Columns.Add("COMP_CODE", typeof(string));
            dtJournal.Columns.Add("BRANCH_CODE", typeof(string));
            dtJournal.Columns.Add("YEAR", typeof(int));
            dtJournal.Columns.Add("VCHR_CODE", typeof(string));
            dtJournal.Columns.Add("VCHR_NAME", typeof(string));
            dtJournal.Columns.Add("VCHR_NO", typeof(string));
            dtJournal.Columns.Add("JOURNAL_DATE", typeof(DateTime));
            dtJournal.Columns.Add("DESCRIPTION", typeof(string));
            dtJournal.Columns.Add("APPR_FLAG", typeof(string));
            dtJournal.Columns.Add("APPR_DATE", typeof(DateTime));
            dtJournal.Columns.Add("APPR_BY", typeof(string));
            return dtJournal;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTableTrn()
    {
        try
        {
            dtJournalTrn = new DataTable();
            dtJournalTrn.Columns.Add("ENTRY_TYPE", typeof(string));
            dtJournalTrn.Columns.Add("LEDGER_CODE", typeof(string));
            dtJournalTrn.Columns.Add("LDGR_NAME", typeof(string));
            dtJournalTrn.Columns.Add("IS_DEBIT", typeof(string));
            dtJournalTrn.Columns.Add("DR_AMOUNT", typeof(double));
            dtJournalTrn.Columns.Add("CR_AMOUNT", typeof(double));
            dtJournalTrn.Columns.Add("DOC_NO", typeof(string));
            dtJournalTrn.Columns.Add("DOC_DT", typeof(string));
            dtJournalTrn.Columns.Add("DESCRIPTION", typeof(string));
            dtJournalTrn.Columns.Add("VCHR_NO", typeof(string));
            dtJournalTrn.Columns.Add("CONF_BY", typeof(string));
            dtJournalTrn.Columns.Add("CONF_DATE", typeof(DateTime));
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

    private void bindJournalTrn(string strVou_No)
    {
        try
        {
            string strLedger_Code = string.Empty;
            string strLedger_Name = string.Empty;
            string strDebit = string.Empty;
            double dblAmount = 0;
            string strDoc_No = string.Empty;
            string strDoc_DT;
            string strDesc = string.Empty;
            string strConf_DT;
            string strConf_By = string.Empty;

            if (ViewState["dtJournalTrn"] != null)
            {
                dtJournalTrn = (DataTable)ViewState["dtJournalTrn"];
                dtJournalTrn = null;
            }

            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.VOUCHER_NO = strVou_No;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectTRNByVoucherNo(oFA_Journal_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        strLedger_Code = dv[iLoop]["LEDGER_CODE"].ToString();
                        strLedger_Name = dv[iLoop]["LDGR_NAME"].ToString();
                        strDebit = dv[iLoop]["IS_DEBIT"].ToString();
                        dblAmount = double.Parse(dv[iLoop]["AMOUNT"].ToString());
                        strDoc_No = dv[iLoop]["DOC_NO"].ToString();
                        strDoc_DT = dv[iLoop]["DOC_DT"].ToString();
                        strDesc = dv[iLoop]["DESCRIPTION"].ToString();
                        strConf_By = dv[iLoop]["CONF_BY"].ToString();
                        strConf_DT = dv[iLoop]["CONF_DATE"].ToString();

                        insertJournalTrn(strLedger_Code, strLedger_Name, strDebit, dblAmount, strDoc_No, strDoc_DT, strDesc, strVou_No, strConf_By, strConf_DT);
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void insertJournalTrn(string strLedger_Code, string strLedger_Name, string strDebit, double dblAmount, string strDoc_No, string strDoc_DT, string strDesc, string strVou_No, string strConf_By, string strConf_DT)
    {
        try
        {
            if (ViewState["dtJournalTrn"] != null)
                dtJournalTrn = (DataTable)ViewState["dtJournalTrn"];

            if (dtJournalTrn == null)
                CreateDataTableTrn();

            DataRow dr = dtJournalTrn.NewRow();

            if (strDebit == "1")
            {
                dr["ENTRY_TYPE"] = "Debit";
                dr["DR_AMOUNT"] = dblAmount;
                dr["CR_AMOUNT"] = 0;
            }
            else
            {
                dr["ENTRY_TYPE"] = "Credit";
                dr["CR_AMOUNT"] = dblAmount;
                dr["DR_AMOUNT"] = 0;
            }

            dr["LEDGER_CODE"] = strLedger_Code;
            dr["LDGR_NAME"] = strLedger_Name;
            dr["IS_DEBIT"] = strDebit;
            dr["DOC_NO"] = strDoc_No;
            dr["DOC_DT"] = strDoc_DT;
            dr["DESCRIPTION"] = strDesc;
            dr["VCHR_NO"] = strVou_No;
            dr["CONF_BY"] = strConf_By;
            dr["CONF_DATE"] = strConf_DT;
            dtJournalTrn.Rows.Add(dr);

            ViewState["dtJournalTrn"] = dtJournalTrn;
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

            DataTable dtJournal = CreateDataTable();
            int totalRows = grdVoucherApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdVoucherApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblVoucherCode = (Label)thisGridViewRow.FindControl("lblVoucherCode");
                    Label lblVoucherNo = (Label)thisGridViewRow.FindControl("lblVoucherNo");
                    Label lblVoucherDate = (Label)thisGridViewRow.FindControl("lblVoucherDate");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");

                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtJournal.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["VCHR_CODE"] = lblVoucherCode.Text.Trim();
                        dr["VCHR_NO"] = lblVoucherNo.Text.Trim();
                        dr["JOURNAL_DATE"] = DateTime.Parse(lblVoucherDate.Text.Trim());
                        dr["APPR_DATE"] = DateTime.Now.Date.ToString();
                        dr["APPR_BY"] = oUserLoginDetail.UserCode;
                        dr["APPR_FLAG"] = "1";
                        dtJournal.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_Journal_DTL.Update_VoucherApproval(oUserLoginDetail.UserCode, dtJournal);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Voucher Approved Successfully.");
                bindVoucherApprovedGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving..\r\nSee error log for detail."));
        }
    }

    private void bindVoucherUnApproved()
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstUnApproval(oFA_Journal_MST);
            cmbVoucher.Items.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbVoucher.DataSource = dt;
                cmbVoucher.DataBind();
                cmbVoucher.Items.Insert(0, new ListItem("-------- Select Voucher -------", "0"));
            }
            else
            {
                grdVoucherApproval.DataSource = null;
                grdVoucherApproval.DataBind();
                CommonFuction.ShowMessage("No Journal Voucher for UnApproval.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdVoucherApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdVoucherApproval.PageIndex = e.NewPageIndex;
            bindVoucherApprovedGrid();
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
                    bindVoucherApprovedGridWithDate();
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

    private void bindVoucherApprovedGridWithDate()
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstVoucherApprovalWithDate(oFA_Journal_MST, txtstartdate.Text.Trim(), txtenddate.Text.Trim());

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

                grdVoucherApproval.DataSource = dt;
                grdVoucherApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Journal Voucher for Approval..";
                grdVoucherApproval.DataSource = null;
                grdVoucherApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Journal Voucher for Approval..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void cmbVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstUnApproval(oFA_Journal_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdVoucherApproval.DataSource = null;
                grdVoucherApproval.DataBind();

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
                dv.RowFilter = "VCHR_NO='" + cmbVoucher.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        grdVoucherApproval.DataSource = dv;
                        grdVoucherApproval.DataBind();
                    }
                }

                int totalRows = grdVoucherApproval.Rows.Count;
                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = grdVoucherApproval.Rows[r];
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
                lblTotalRecord.Text = "No Journal Voucher for UnApproval.";
                grdVoucherApproval.DataSource = null;
                grdVoucherApproval.DataBind();
                CommonFuction.ShowMessage("No Journal Voucher for UnApproval.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Vouchers..\r\nSee error log for detail."));
        }
    }

    protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}