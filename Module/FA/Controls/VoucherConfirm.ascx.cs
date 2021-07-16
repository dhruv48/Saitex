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

public partial class Module_FA_Controls_VoucherConfirm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST;
    private DataTable dtJournalTrn;
    public string VoucherType = string.Empty;
    public string StartDate = string.Empty;
    public string EndDate = string.Empty;

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
            txtenddate.Text = System.DateTime.Now.ToString();

            BindVoucherType();
            bindVoucherConfirm();
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
            int totalRows = grdVoucherConfirmation.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdVoucherConfirmation.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblVoucherCode = (Label)thisGridViewRow.FindControl("lblVoucherCode");
                    Label lblVoucherNo = (Label)thisGridViewRow.FindControl("lblVoucherNo");
                    Label lblVoucherDate = (Label)thisGridViewRow.FindControl("lblVoucherDate");
                    CheckBox Confirmed = (CheckBox)thisGridViewRow.FindControl("chkConfirmed");

                    if (Confirmed.Checked == false)
                    {
                        DataRow dr = dtJournal.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["VCHR_CODE"] = lblVoucherCode.Text.Trim();
                        dr["VCHR_NO"] = lblVoucherNo.Text.Trim();
                        dr["JOURNAL_DATE"] = DateTime.Parse(lblVoucherDate.Text.Trim());
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "0";
                        dtJournal.Rows.Add(dr);
                        Confirmed.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_Journal_DTL.Update_VoucherUnConfirm(oUserLoginDetail.UserCode, dtJournal);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Voucher UnConfirmed Successfully.");
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in update.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion.\r\nSee error log for detail."));
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
            bindVoucherUnConfirm();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/VoucherConfirmation.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help.\r\nSee error log for detail."));
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

    private void bindVoucherConfirm()
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMst(oFA_Journal_MST);
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

                grdVoucherConfirmation.DataSource = dt;
                grdVoucherConfirmation.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                grdVoucherConfirmation.DataSource = null;
                grdVoucherConfirmation.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("There is no voucher for confirmation..");
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindVoucherType()
    {
        try
        {
            ddlVoucherType.Items.Clear();
            DataTable data = new DataTable();
            data = GetDataForVoucherType("");
            if (data != null && data.Rows.Count > 0)
            {
                ddlVoucherType.DataSource = data;
                ddlVoucherType.DataTextField = "VCHR_NAME";
                ddlVoucherType.DataValueField = "VCHR_CODE";
                ddlVoucherType.DataBind();
                ddlVoucherType.Items.Insert(0, new ListItem("--- Select Voucher Type ---", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetDataForVoucherType(string Text)
    {
        try
        {
            string whereClause = " WHERE VCHR_CODE like :SearchQuery OR VCHR_NAME LIKE :SearchQuery ";
            string sortExpression = " ORDER BY VCHR_CODE";
            string commandText = "SELECT * FROM V_FA_VCHR_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", Text + '%', sPO);
            return dt;
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
            DataTable dtJournal = new DataTable();
            dtJournal.Columns.Add("COMP_CODE", typeof(string));
            dtJournal.Columns.Add("BRANCH_CODE", typeof(string));
            dtJournal.Columns.Add("YEAR", typeof(int));
            dtJournal.Columns.Add("VCHR_CODE", typeof(string));
            dtJournal.Columns.Add("VCHR_NAME", typeof(string));
            dtJournal.Columns.Add("VCHR_NO", typeof(string));
            dtJournal.Columns.Add("JOURNAL_DATE", typeof(DateTime));
            dtJournal.Columns.Add("DESCRIPTION", typeof(string));
            dtJournal.Columns.Add("CONF_FLAG", typeof(string));
            dtJournal.Columns.Add("CONF_DATE", typeof(DateTime));
            dtJournal.Columns.Add("CONF_BY", typeof(string));
            return dtJournal;
        }
        catch
        {
            throw;
        }
    }

    protected void grdVoucherConfirmation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string strVou_No = string.Empty;

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Bound Event.\r\nSee error log for detail."));
        }
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

                        insertJournalTrn(strLedger_Code, strLedger_Name, strDebit, dblAmount, strDoc_No, strDoc_DT, strDesc, strVou_No);
                    }
                }
            }
            ViewState["dtJournalTrn"] = dtJournalTrn;
        }
        catch
        {
            throw;
        }
    }

    private void insertJournalTrn(string strLedger_Code, string strLedger_Name, string strDebit, double dblAmount, string strDoc_No, string strDoc_DT, string strDesc, string strVou_No)
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
            dtJournalTrn.Rows.Add(dr);

            ViewState["dtJournalTrn"] = dtJournalTrn;
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
            int totalRows = grdVoucherConfirmation.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdVoucherConfirmation.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblVoucherCode = (Label)thisGridViewRow.FindControl("lblVoucherCode");
                    Label lblVoucherNo = (Label)thisGridViewRow.FindControl("lblVoucherNo");
                    Label lblVoucherDate = (Label)thisGridViewRow.FindControl("lblVoucherDate");
                    CheckBox Confirmed = (CheckBox)thisGridViewRow.FindControl("chkConfirmed");

                    if (Confirmed.Checked == true)
                    {
                        DataRow dr = dtJournal.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["VCHR_CODE"] = lblVoucherCode.Text.Trim();
                        dr["VCHR_NO"] = lblVoucherNo.Text.Trim();
                        dr["JOURNAL_DATE"] = DateTime.Parse(lblVoucherDate.Text.Trim());
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "1";
                        dtJournal.Rows.Add(dr);
                        Confirmed.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_Journal_DTL.Update_VoucherConfirm(oUserLoginDetail.UserCode, dtJournal);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Voucher Confirmed Successfully.");
                bindVoucherConfirm();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
        }
    }

    private void bindVoucherUnConfirm()
    {
        try
        {
            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();
            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstUnConfirm(oFA_Journal_MST);
            cmbVoucher.Items.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbVoucher.DataSource = dt;
                cmbVoucher.DataBind();
                cmbVoucher.Items.Insert(0, new ListItem("-------- Select Voucher -------", "0"));
            }
            else
            {
                grdVoucherConfirmation.DataSource = null;
                grdVoucherConfirmation.DataBind();
                CommonFuction.ShowMessage("No Journal Voucher for Unconfirmation..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdVoucherConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdVoucherConfirmation.PageIndex = e.NewPageIndex;
            bindVoucherConfirmWithDate();
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
            bindVoucherConfirmWithDate();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Searching Records..\r\nSee error log for detail."));
        }
    }

    private void bindVoucherConfirmWithDate()
    {
        try
        {
            grdVoucherConfirmation.DataSource = null;
            grdVoucherConfirmation.DataBind();

            if (ddlVoucherType.SelectedIndex > 0)
            {
                VoucherType = ddlVoucherType.SelectedValue.ToString().Trim();
            }
            else
            {
                VoucherType = string.Empty;
            }

            if (txtstartdate.Text != null && txtstartdate.Text != "")
            {
                StartDate = txtstartdate.Text.Trim();
            }
            else
            {
                StartDate = string.Empty;
            }

            if (txtenddate.Text != null && txtenddate.Text != "")
            {
                EndDate = txtenddate.Text.Trim();
            }
            else
            {
                EndDate = string.Empty;
            }

            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstWithDate(oFA_Journal_MST, StartDate, EndDate, VoucherType);
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

                grdVoucherConfirmation.DataSource = dt;
                grdVoucherConfirmation.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                grdVoucherConfirmation.DataSource = null;
                grdVoucherConfirmation.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No record found..");
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

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstUnConfirm(oFA_Journal_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdVoucherConfirmation.DataSource = null;
                grdVoucherConfirmation.DataBind();

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
                dv.RowFilter = "VCHR_NO='" + cmbVoucher.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        grdVoucherConfirmation.DataSource = dv;
                        grdVoucherConfirmation.DataBind();
                    }
                }

                int totalRows = grdVoucherConfirmation.Rows.Count;
                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = grdVoucherConfirmation.Rows[r];
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
                lblTotalRecord.Text = "No Journal Voucher for Unconfirmation..";
                grdVoucherConfirmation.DataSource = null;
                grdVoucherConfirmation.DataBind();
                CommonFuction.ShowMessage("No Journal Voucher for Unconfirmation..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Voucher Selection.\r\nSee error log for detail."));
        }
    }

    protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindVoucherConfirmWithDate();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Voucher Type Selection.\r\nSee error log for detail."));
        }
    }
}