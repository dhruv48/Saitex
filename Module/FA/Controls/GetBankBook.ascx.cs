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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_GetBankBook : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DateTime StartDate;
    private static DateTime EndDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();

                if (Request.QueryString["LEDGER_CODE"] != null && Request.QueryString["LEDGER_CODE"].ToString() != "" && Request.QueryString["LGR_S_DT"] != null && Request.QueryString["LGR_S_DT"].ToString() != "" && Request.QueryString["LGR_E_DT"] != null && Request.QueryString["LGR_E_DT"].ToString() != "")
                {
                    string LEDGER_CODE = Request.QueryString["LEDGER_CODE"].ToString();
                    string LGR_S_DT = Request.QueryString["LGR_S_DT"].ToString();
                    string LGR_E_DT = Request.QueryString["LGR_E_DT"].ToString();
                    txtStartingDate.Text = LGR_S_DT;
                    txtEndingDate.Text = LGR_E_DT;
                    ddlLedgerCode.SelectedValue = LEDGER_CODE;
                    string Vou_No = string.Empty;
                    int IsDoc = 0;
                    GetLedger_Book_By_LedgerAndDate(LEDGER_CODE, LGR_S_DT, LGR_E_DT, Vou_No, IsDoc);
                }
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
            // To Check validation for Financial Year.
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }
            rvStartDate.MinimumValue = StartDate.ToShortDateString();
            rvStartDate.MaximumValue = EndDate.ToShortDateString();
            rvEndDate.MinimumValue = StartDate.ToShortDateString();
            rvEndDate.MaximumValue = EndDate.ToShortDateString();

            ShowLedger.Visible = false;
            ddlLedgerCode.SelectedIndex = -1;
            ddlLedgerCode.SelectedText = "";
            ddlLedgerCode.SelectedValue = "";
        }
        catch
        {
            throw;
        }
    }

    protected void ddlLedgerCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ddlLedgerCode.Items.Clear();
            ddlLedgerCode.DataSource = null;
            ddlLedgerCode.DataBind();

            DataTable data = new DataTable();
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);

            ddlLedgerCode.DataSource = data;
            ddlLedgerCode.DataTextField = "LDGR_NAME";
            ddlLedgerCode.DataValueField = "LDGR_CODE";
            ddlLedgerCode.DataBind();

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Ledger Code..\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE GRP_CODE='16' and LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or GRP_CODE='16' and LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY LDGR_CODE";
            string commandText = "SELECT * FROM FA_LGR_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM FA_LGR_MST WHERE LDGR_CODE like :SearchQuery And DEL_STATUS = '0' or LDGR_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.FA_LGR_MST.GetCountForLOV(CommandText, text + '%', "");
        }
        catch
        {
            throw;
        }
    }

    protected void btnGetLedger_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlLedgerCode.SelectedIndex > -1 && ddlLedgerCode.SelectedValue != "")
            {
                string Vou_No = string.Empty;
                int IsDoc = 0;
                GetLedger_Book_By_Ledger(ddlLedgerCode.SelectedValue.Trim(), Vou_No, IsDoc);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Get Ledger Button..\r\nSee error log for detail."));
        }
    }

    private void GetLedger_Book_By_Ledger(string Ledger_Code, string Vou_No, int IsDoc)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtLedger_Book = SaitexBL.Interface.Method.FA_LEDGER_BOOK.Get_LEDGER_BOOK(oUserLoginDetail, StartDate, EndDate, Ledger_Code, Vou_No, IsDoc);
            DataTable dtCheque = SaitexBL.Interface.Method.FA_CHEQUE_DTL.SelectBankBook();

            if (dtLedger_Book.Rows.Count > 0)
            {
                if (!dtLedger_Book.Columns.Contains("CLEARED"))
                    dtLedger_Book.Columns.Add("CLEARED", typeof(string));

                for (int i = 0; i < dtLedger_Book.Rows.Count; i++)
                {
                    int uniqueId = Convert.ToInt32(dtLedger_Book.Rows[i]["UNIQUE_ID"].ToString());
                    DataView dv = new DataView(dtCheque);
                    dv.RowFilter = "JOURNAL_ID='" + dtLedger_Book.Rows[i]["JOURNAL_ID"].ToString() + "' AND VCHR_NO='" + dtLedger_Book.Rows[i]["VOUCHER_NO"].ToString() + "'";

                    if (dv.Count > 0)
                    {
                        dtLedger_Book.Rows[i]["CLEARED"] = dv[0]["CLEARED"].ToString();
                        dtLedger_Book.AcceptChanges();
                    }
                }

                grdLgr_Book.DataSource = dtLedger_Book;
                grdLgr_Book.DataBind();

                double Dr_Total = 0;
                double Cr_Total = 0;
                for (int iLoop = 0; iLoop < dtLedger_Book.Rows.Count; iLoop++)
                {
                    double Amt = 0;
                    double.TryParse(dtLedger_Book.Rows[iLoop]["DR_AMOUNT"].ToString(), out Amt);
                    Dr_Total += Amt;

                    Amt = 0;
                    double.TryParse(dtLedger_Book.Rows[iLoop]["CR_AMOUNT"].ToString(), out Amt);
                    Cr_Total += Amt;
                }

                Label lblFCR_Amount = (Label)grdLgr_Book.FooterRow.FindControl("lblFCR_Amount");
                Label lblFDR_Amount = (Label)grdLgr_Book.FooterRow.FindControl("lblFDR_Amount");
                lblFDR_Amount.Text = Dr_Total.ToString();
                lblFCR_Amount.Text = Cr_Total.ToString();

                lblAccountName.Text = "Details of " + ddlLedgerCode.SelectedText.Trim() + " ledger for the Period Of " + StartDate + " to " + EndDate;
                ShowLedger.Visible = true;
            }
            else
            {
                ShowLedger.Visible = false;
                Common.CommonFuction.ShowMessage("No records found for the selected Ledger.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdLedgerBook_Dr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string VOUCHER_NO = e.CommandArgument.ToString();
            string VOUCHER_DT = string.Empty;

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            LinkButton lbtnVoucher_Date = (LinkButton)row.FindControl("lbtnVoucher_Date");
            VOUCHER_DT = DateTime.Parse(lbtnVoucher_Date.Text).ToShortDateString();

            if (VOUCHER_NO != "999999" && VOUCHER_NO != "999998")
            {
                string URL = "~/Module/FA/Queries/JournalQueryForm.aspx?VOUCHER_NO=" + VOUCHER_NO + "&VOUCHER_DT=" + VOUCHER_DT;
                Response.Redirect(URL);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid RowCommand..\r\nSee error log for detail."));
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
        }
    }

    private void GetLedger_Book_By_LedgerAndDate(string Ledger_Code, string LGR_S_DT, string LGR_E_DT, string Vou_No, int IsDoc)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtLedger_Book = SaitexBL.Interface.Method.FA_LEDGER_BOOK.Get_LEDGER_BOOK(oUserLoginDetail, DateTime.Parse(LGR_S_DT), DateTime.Parse(LGR_E_DT), Ledger_Code, Vou_No, IsDoc);
            DataTable dtCheque = SaitexBL.Interface.Method.FA_CHEQUE_DTL.SelectBankBook();

            if (dtLedger_Book.Rows.Count > 0)
            {
                if (!dtLedger_Book.Columns.Contains("CLEARED"))
                    dtLedger_Book.Columns.Add("CLEARED", typeof(string));

                for (int i = 0; i < dtLedger_Book.Rows.Count; i++)
                {
                    int uniqueId = Convert.ToInt32(dtLedger_Book.Rows[i]["UNIQUE_ID"].ToString());
                    DataView dv = new DataView(dtCheque);
                    dv.RowFilter = "JOURNAL_ID='" + dtLedger_Book.Rows[i]["JOURNAL_ID"].ToString() + "' AND VCHR_NO='" + dtLedger_Book.Rows[i]["VOUCHER_NO"].ToString() + "'";

                    if (dv.Count > 0)
                    {
                        dtLedger_Book.Rows[i]["CLEARED"] = dv[0]["CLEARED"].ToString();
                        dtLedger_Book.AcceptChanges();
                    }
                }

                grdLgr_Book.DataSource = dtLedger_Book;
                grdLgr_Book.DataBind();

                double Dr_Total = 0;
                double Cr_Total = 0;
                for (int iLoop = 0; iLoop < dtLedger_Book.Rows.Count; iLoop++)
                {
                    double Amt = 0;
                    double.TryParse(dtLedger_Book.Rows[iLoop]["DR_AMOUNT"].ToString(), out Amt);
                    Dr_Total += Amt;

                    Amt = 0;
                    double.TryParse(dtLedger_Book.Rows[iLoop]["CR_AMOUNT"].ToString(), out Amt);
                    Cr_Total += Amt;
                }

                Label lblFCR_Amount = (Label)grdLgr_Book.FooterRow.FindControl("lblFCR_Amount");
                Label lblFDR_Amount = (Label)grdLgr_Book.FooterRow.FindControl("lblFDR_Amount");
                lblFDR_Amount.Text = Dr_Total.ToString();
                lblFCR_Amount.Text = Cr_Total.ToString();

                lblAccountName.Text = "Details of " + ddlLedgerCode.SelectedText.Trim() + " ledger for the Period Of " + txtStartingDate + " to " + txtEndingDate;
                ShowLedger.Visible = true;
            }
            else
            {
                ShowLedger.Visible = false;
                Common.CommonFuction.ShowMessage("No records found for the selected Ledger.");
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
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}