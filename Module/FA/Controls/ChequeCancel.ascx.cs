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

public partial class Module_FA_Controls_ChequeCancel : System.Web.UI.UserControl
{
    private static DataTable dtChequeCancel;
    SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_CHEQUE_DTL oFA_CHEQUE_DTL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Cheque Cancellation Successfully Done!');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }
                Session["saveStatus"] = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page loading..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            dtChequeCancel = CreateDataTable();
            int totalRows = grdChequeCancellation.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdChequeCancellation.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblVoucherNo = (Label)thisGridViewRow.FindControl("lblVoucherNo");
                    Label lblAmount = (Label)thisGridViewRow.FindControl("lblAmount");
                    CheckBox chkCancel = (CheckBox)thisGridViewRow.FindControl("chkCancel");

                    if (chkCancel.Checked == true)
                    {
                        oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        oFA_Journal_MST.VOUCHER_NO = lblVoucherNo.Text.Trim();

                        DataTable dt = SaitexBL.Interface.Method.FA_PAYMENT_MODE.SelectPaymentModeByVoucherNo(oFA_Journal_MST);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataView dv = new DataView(dt);

                            if (dv.Count > 0)
                            {
                                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                                {
                                    DataRow dr = dtChequeCancel.NewRow();

                                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                    dr["JOURNAL_MST_VCHR_NO"] = lblVoucherNo.Text.Trim();
                                    dr["JOURNAL_MST_DESC"] = "THIS CHEQUE HAS BEEN CANCELLED BY - " + oUserLoginDetail.Username + " ON " + DateTime.Now.ToShortDateString();
                                    dr["JOURNAL_TRN_AMT"] = 0;
                                    dr["JOURNAL_TRN_DESC"] = "THIS CHEQUE HAS BEEN CANCELLED BY - " + oUserLoginDetail.Username + " ON " + DateTime.Now.ToShortDateString();
                                    dr["JOURNAL_VCHR_NO"] = lblVoucherNo.Text.Trim();
                                    dr["PAYMENT_VCHR_NO"] = lblVoucherNo.Text.Trim();
                                    dr["PAYMENT_BANK_LGR_CODE"] = dv[iLoop]["BANK_LGR_CODE"].ToString();
                                    dr["PAYMENT_PARTY_LGR_CODE"] = dv[iLoop]["PARTY_LGR_CODE"].ToString();
                                    dr["PAYMENT_AMT_JV"] = 0;
                                    dr["CHEQUE_CANCELLED_BY"] = oUserLoginDetail.Username;
                                    dr["CHEQUE_CANCELLED_AMT"] = lblAmount.Text.Trim();
                                    dtChequeCancel.Rows.Add(dr);
                                    chkCancel.Checked = false;
                                }
                            }
                        }
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.FA_CHEQUE_DTL.Update_ChequeCancellation(dtChequeCancel);
            if (iResult > 0)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./ChequeCancellation.aspx?cId=U", false);
            }
            else
            {
                CommonFuction.ShowMessage("Problem in Cheque Cancellation..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/ChequeCancellation.aspx", false);
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

    /// <summary>
    /// Use of Intialization. Means blank controls.
    /// </summary>
    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            BindChequeCancellation();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Here, we are binding the Gridview for Cheque Cancellation..
    /// </summary>
    private void BindChequeCancellation()
    {
        try
        {
            oFA_CHEQUE_DTL = new SaitexDM.Common.DataModel.FA_CHEQUE_DTL();

            oFA_CHEQUE_DTL.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_CHEQUE_DTL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_CHEQUE_DTL.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUE_DTL.SelectChequeDetailsWithCompBranch(oFA_CHEQUE_DTL);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CANCELLED_DATE"))
                    dt.Columns.Add("CANCELLED_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CANCELLED_BY"))
                    dt.Columns.Add("CANCELLED_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConcelBy = dr["CANCELLED_BY"].ToString();
                    if (ConcelBy == "")
                        dr["CANCELLED_BY"] = oUserLoginDetail.Username;

                    dr["CANCELLED_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }

                grdChequeCancellation.DataSource = dt;
                grdChequeCancellation.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Cheque for Cancellation..";
                grdChequeCancellation.DataSource = null;
                grdChequeCancellation.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Cheque for Cancellation..");
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
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private DataTable CreateDataTable()
    {
        try
        {
            dtChequeCancel = new DataTable();
            dtChequeCancel.Columns.Add("COMP_CODE", typeof(string));
            dtChequeCancel.Columns.Add("BRANCH_CODE", typeof(string));
            dtChequeCancel.Columns.Add("YEAR", typeof(int));
            dtChequeCancel.Columns.Add("JOURNAL_MST_VCHR_NO", typeof(string));
            dtChequeCancel.Columns.Add("JOURNAL_MST_DESC", typeof(string));
            dtChequeCancel.Columns.Add("JOURNAL_TRN_AMT", typeof(double));
            dtChequeCancel.Columns.Add("JOURNAL_TRN_DESC", typeof(string));
            dtChequeCancel.Columns.Add("JOURNAL_VCHR_NO", typeof(string));
            dtChequeCancel.Columns.Add("PAYMENT_VCHR_NO", typeof(string));
            dtChequeCancel.Columns.Add("PAYMENT_BANK_LGR_CODE", typeof(string));
            dtChequeCancel.Columns.Add("PAYMENT_PARTY_LGR_CODE", typeof(string));
            dtChequeCancel.Columns.Add("PAYMENT_AMT_JV", typeof(double));
            dtChequeCancel.Columns.Add("CHEQUE_CANCELLED_BY", typeof(string));
            dtChequeCancel.Columns.Add("CHEQUE_CANCELLED_AMT", typeof(double));
            return dtChequeCancel;
        }
        catch
        {
            throw;
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStart.Text != "")
            {
                if (txtEnd.Text != "")
                {
                    int iStart = int.Parse(txtStart.Text.Trim());
                    int iEnd = int.Parse(txtEnd.Text.Trim());
                    if (iStart <= iEnd)
                    {
                        oFA_CHEQUE_DTL = new SaitexDM.Common.DataModel.FA_CHEQUE_DTL();

                        oFA_CHEQUE_DTL.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFA_CHEQUE_DTL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oFA_CHEQUE_DTL.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        oFA_CHEQUE_DTL.START_CHEQUE_NO = int.Parse(txtStart.Text.Trim());
                        oFA_CHEQUE_DTL.END_CHEQUE_NO = int.Parse(txtEnd.Text.Trim());

                        DataTable dt = SaitexBL.Interface.Method.FA_CHEQUE_DTL.SelectChequeDetailsWithCompBranchBetweenCheques(oFA_CHEQUE_DTL);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (!dt.Columns.Contains("CANCELLED_DATE"))
                                dt.Columns.Add("CANCELLED_DATE", typeof(DateTime));
                            if (!dt.Columns.Contains("CANCELLED_BY"))
                                dt.Columns.Add("CANCELLED_BY", typeof(string));

                            foreach (DataRow dr in dt.Rows)
                            {
                                string ConcelBy = dr["CANCELLED_BY"].ToString();
                                if (ConcelBy == "")
                                    dr["CANCELLED_BY"] = oUserLoginDetail.Username;

                                dr["CANCELLED_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                            }

                            grdChequeCancellation.DataSource = dt;
                            grdChequeCancellation.DataBind();
                            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                        }
                        else
                        {
                            lblTotalRecord.Text = "No Cheque found for Cancellation..";
                            grdChequeCancellation.DataSource = null;
                            grdChequeCancellation.DataBind();
                            lblTotalRecord.Text = "0";
                            CommonFuction.ShowMessage("No Cheque found for Cancellation..");
                        }
                    }
                    else
                    {
                        CommonFuction.ShowMessage("Dear! Starting Cheque Number should be less than or equal to the Ending Cheque Number..");
                    }
                }
                else
                {
                    CommonFuction.ShowMessage("Dear, Please enter Ending Cheque Number..");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Dear, Please enter Starting Cheque Number..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Searching Cheque..\r\nSee error log for detail."));
        }
    }

    protected void grdChequeCancellation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdChequeCancellation.PageIndex = e.NewPageIndex;
            BindChequeCancellation();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
        }
    }
}