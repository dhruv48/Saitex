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
using Obout.Grid;

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_BalanceSheet : System.Web.UI.UserControl
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
                Get_StatementOfAccount();
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

            txtStartingDate.Text = StartDate.ToShortDateString();
            txtEndingDate.Text = EndDate.ToShortDateString();
            txtStartingDate.Enabled = false;
            txtEndingDate.Enabled = false;
        }
        catch
        {
            throw;
        }
    }

    private void Get_StatementOfAccount()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DateTime StartDate = DateTime.Parse(txtStartingDate.Text);
            DateTime EndDate = DateTime.Parse(txtEndingDate.Text);

            DataTable dt = SaitexBL.Interface.Method.FA_Statement_Of_Account.Get_BALANCE_SHEET(oUserLoginDetail, StartDate, EndDate);

            if (dt.Rows.Count > 0)
            {
                ViewState["dt"] = dt;
                dt = FillBlankRow(dt);

                DataView dvDebit = new DataView(dt);
                DataView dvCredit = new DataView(dt);

                double Dr_Total = 0;
                double Cr_Total = 0;

                string FilterString = "L%";
                dvDebit.RowFilter = "ACCOUNT_ID like '" + FilterString + "' and IS_DEBIT='" + true + "'";

                if (dvDebit.Count > 0)
                {
                    grdLedgerBook_Dr.DataSource = dvDebit;
                    grdLedgerBook_Dr.DataBind();
                }
                dvCredit.RowFilter = "ACCOUNT_ID like '" + FilterString + "' and IS_DEBIT='" + false + "'";

                if (dvCredit.Count > 0)
                {
                    grdLedgerBook_Cr.DataSource = dvCredit;
                    grdLedgerBook_Cr.DataBind();
                }
                lblAccountName.Text = "Balance Sheet as on " + txtEndingDate.Text;
            }
            else
            {
                Common.CommonFuction.ShowMessage("No records found for the selected Ledger");
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable FillBlankRow(DataTable dtBalanceSheet)
    {
        try
        {
            if (dtBalanceSheet != null && dtBalanceSheet.Rows.Count > 0)
            {
                DataView dvDebit = new DataView(dtBalanceSheet);
                DataView dvCredit = new DataView(dtBalanceSheet);
                string FilterString = "L%";
                dvDebit.RowFilter = "ACCOUNT_ID like '" + FilterString + "' and IS_DEBIT='" + true + "'";
                dvCredit.RowFilter = "ACCOUNT_ID like '" + FilterString + "' and IS_DEBIT='" + false + "'";
                if (dvDebit.Count != dvCredit.Count)
                {
                    int iDiff = 0;
                    bool bSide = false;
                    if (dvDebit.Count > dvCredit.Count)
                    {
                        bSide = false;
                        iDiff = dvDebit.Count - dvCredit.Count;
                    }
                    else
                    {
                        bSide = true;
                        iDiff = dvDebit.Count - dvCredit.Count;
                    }
                    for (int iLoop = 0; iLoop < iDiff; iLoop++)
                    {
                        DataRow drNew = dtBalanceSheet.NewRow();
                        drNew["UNIQUE_ID"] = dtBalanceSheet.Rows.Count + 1;
                        drNew["PARENT_ID"] = "987987";
                        drNew["IS_DEBIT"] = bSide;
                        dtBalanceSheet.Rows.Add(drNew);
                    }
                }
            }
            return dtBalanceSheet;
        }
        catch
        {
            throw;
        }
    }

    protected void grdBalanceSheet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string Account_Id = e.CommandArgument.ToString();
            string AccountType = Account_Id.Remove(1);
            Account_Id = Account_Id.Remove(0, 1);
            string URL = string.Empty;
            if (Account_Id == "999999")
                URL = "ProfitLossAccount.aspx";
            else
            {
                if (AccountType == "L")
                    URL = "Get_Ledger_Book.aspx?LEDGER_CODE=" + Account_Id;
                else if (AccountType == "G")
                    URL = "SOA.aspx?GROUP_CODE=" + Account_Id;
            }
            Response.Redirect(URL);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row Command Event..\r\nSee error log for detail."));
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

    #region Code to Create Balance Sheet

    private void CreateBalanceSheet(DataTable dtBalanceSheet)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void DetailMainTable(DataView dv)
    {
        try
        {
            for (int iLoop = 0; iLoop < dv.Count; iLoop++)
            {
                string ss;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private Table Create_Table()
    {
        Table tbl = new Table();
        tbl.Visible = true;
        tbl.CellPadding = 1;
        tbl.CellSpacing = 1;
        return tbl;
    }

    private TableRow Create_Row()
    {
        TableRow tr = new TableRow();
        tr.Visible = true;
        return tr;
    }

    private TableCell Create_Cell()
    {
        TableCell tc = new TableCell();
        tc.HorizontalAlign = HorizontalAlign.Left;
        tc.VerticalAlign = VerticalAlign.Top;
        return tc;
    }

    #endregion

    protected void grdLedgerBook_Dr_Rebind(object sender, EventArgs e)
    {
        try
        {
            Get_StatementOfAccount();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Rebind Event.\r\nSee error log for detail."));
        }
    }

    protected void grdLedgerBook_Dr_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

            if (e.Row.RowType == GridRowType.ColumnFooter)
            {
                double Dr_Total = 0;
                double Cr_Total = 0;
                DataTable dtLedger_Book = (DataTable)ViewState["dt"];
                string FilterString = "L%";
                DataView dv = new DataView(dtLedger_Book);
                dv.RowFilter = "ACCOUNT_ID like '" + FilterString + "' and IS_DEBIT='" + true + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        double Amt = 0;
                        double.TryParse(dv[iLoop]["DR_AMOUNT"].ToString(), out Amt);
                        Dr_Total += Amt;
                    }
                }
                e.Row.Cells[2].Text = Dr_Total.ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row DataBount Event..\r\nSee error log for detail."));
        }
    }

    protected void grdLedgerBook_Dr_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList arr = e.RecordsCollection;
            Hashtable ht = arr[0] as Hashtable;
            string Account_Id = ht["ACCOUNT_ID"].ToString();
            string AccountType = Account_Id.Remove(1);
            Account_Id = Account_Id.Remove(0, 1);
            string URL = string.Empty;
            string LGR_S_DT = DateTime.Parse(txtStartingDate.Text).ToShortDateString();
            string LGR_E_DT = DateTime.Parse(txtEndingDate.Text).ToShortDateString();

            if (Account_Id == "999999")
                URL = "ProfitLossAccount.aspx";
            else
            {
                if (AccountType == "L")
                    URL = "Get_Ledger_Book.aspx?LEDGER_CODE=" + Account_Id + "&LGR_S_DT=" + LGR_S_DT + "&LGR_E_DT=" + LGR_E_DT;
                else if (AccountType == "G")
                    URL = "SOA.aspx?GROUP_CODE=" + Account_Id;
            }
            Response.Redirect(URL);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select Event..\r\nSee error log for detail."));
        }
    }

    protected void grdLedgerBook_Cr_Rebind(object sender, EventArgs e)
    {
        try
        {
            Get_StatementOfAccount();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Rebind Event..\r\nSee error log for detail."));
        }
    }

    protected void grdLedgerBook_Cr_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

            if (e.Row.RowType == GridRowType.ColumnFooter)
            {
                double Dr_Total = 0;
                double Cr_Total = 0;
                DataTable dtLedger_Book = (DataTable)ViewState["dt"];
                string FilterString = "L%";
                DataView dv = new DataView(dtLedger_Book);
                dv.RowFilter = "ACCOUNT_ID like '" + FilterString + "' and IS_DEBIT='" + false + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        double Amt = 0;
                        double.TryParse(dv[iLoop]["CR_AMOUNT"].ToString(), out Amt);
                        Dr_Total += Amt;
                    }
                }
                e.Row.Cells[2].Text = Dr_Total.ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row DataBound Event..\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}
