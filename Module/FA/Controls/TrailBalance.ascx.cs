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

public partial class Module_FA_Controls_TrailBalance : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DateTime StartDate;
    private static DateTime EndDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
                Get_StatementOfAccount(StartDate, EndDate);
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
            StartDate = oUserLoginDetail.DT_STARTDATE;
            EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            //rvStartDate.MinimumValue = StartDate.ToShortDateString();
            //rvStartDate.MaximumValue = EndDate.ToShortDateString();
            //rvEndDate.MinimumValue = StartDate.ToShortDateString();
            //rvEndDate.MaximumValue = EndDate.ToShortDateString();
            txtStartingDate.Text = StartDate.ToShortDateString();
            txtEndingDate.Text = EndDate.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    private void Get_StatementOfAccount(DateTime StartDate, DateTime EndDate)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_Statement_Of_Account.get_Statement_Of_Account(oUserLoginDetail, StartDate, EndDate);
            if (dt.Rows.Count > 0)
            {
                DataView dvDebit = new DataView(dt);
                DataView dvCredit = new DataView(dt);
                string FilterString = "G";
                // string FilterString = "G%";

                dvDebit.RowFilter = "PARENT_ID = '" + FilterString + "'";
                //   dvDebit.RowFilter = "ACCOUNT_ID like '" + FilterString + "'";
                if (dvDebit.Count > 0)
                {
                    grdTrial.DataSource = dvDebit;
                    grdTrial.DataBind();

                    double Dr_Total = 0;
                    double Cr_Total = 0;
                    for (int iLoop = 0; iLoop < dvDebit.Count; iLoop++)
                    {
                        double Amt = 0;
                        double.TryParse(dvDebit[iLoop]["CR_AMOUNT"].ToString(), out Amt);
                        Dr_Total += Amt;

                        Amt = 0;
                        double.TryParse(dvDebit[iLoop]["DR_AMOUNT"].ToString(), out Amt);
                        Cr_Total += Amt;
                    }
                    ViewState["dt"] = dt;
                }
                lblAccountName.Text = "Trial Balance as on " + txtEndingDate.Text.Trim();
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Records found for the selected Ledger");
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
            string Account_Id = e.CommandArgument.ToString();
            string AccountType = Account_Id.Remove(1);
            Account_Id = Account_Id.Remove(0, 1);
            string URL = string.Empty;
            if (Account_Id != "999999" && Account_Id != "999998")
            {
                if (AccountType == "L")
                    URL = "Get_Ledger_Book.aspx?LEDGER_CODE=" + Account_Id;
                else if (AccountType == "G")
                    URL = "SOA.aspx?GROUP_CODE=" + Account_Id;

                Response.Redirect(URL);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row Command event.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./TrialBalanceOPT.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
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

    protected void grdTrial_Rebind(object sender, EventArgs e)
    {
        try
        {
            Get_StatementOfAccount(StartDate, EndDate);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Rebind Event.\r\nSee error log for detail."));
        }
    }

    protected void grdTrial_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

            if (e.Row.RowType == GridRowType.ColumnFooter)
            {
                double Dr_Total = 0;
                double Cr_Total = 0;
                DataTable dtLedger_Book = (DataTable)ViewState["dt"];
                string FilterString = "G";
                DataView dvDebit = new DataView(dtLedger_Book);
                dvDebit.RowFilter = "PARENT_ID = '" + FilterString + "'";
                // dvDebit.RowFilter = "ACCOUNT_ID like '" + FilterString + "'";
                if (dvDebit.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dvDebit.Count; iLoop++)
                    {
                        double Amt = 0;
                        double.TryParse(dvDebit[iLoop]["DR_AMOUNT"].ToString(), out Amt);
                        Dr_Total += Amt;

                        Amt = 0;
                        double.TryParse(dvDebit[iLoop]["CR_AMOUNT"].ToString(), out Amt);
                        Cr_Total += Amt;
                    }
                }
                e.Row.Cells[6].Text = Cr_Total.ToString();
                e.Row.Cells[7].Text = Dr_Total.ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Data Row Bound Event.\r\nSee error log for detail."));
        }
    }

    protected void grdTrial_Select(object sender, Obout.Grid.GridRecordEventArgs e)
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

            if (Account_Id != "999999" && Account_Id != "999998")
            {
                if (AccountType == "L")
                    URL = "Get_Ledger_Book.aspx?LEDGER_CODE=" + Account_Id + "&LGR_S_DT=" + LGR_S_DT + "&LGR_E_DT=" + LGR_E_DT;
                else if (AccountType == "G")
                    URL = "SOA.aspx?GROUP_CODE=" + Account_Id;

                Response.Redirect(URL);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select Event.\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStartingDate.Text != "")
            {
                if (txtEndingDate.Text != "")
                {
                    Get_StatementOfAccount(DateTime.Parse(txtStartingDate.Text.Trim()), DateTime.Parse(txtEndingDate.Text.Trim()));
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Enter Ending Date..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Enter Starting Date..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in OK Button Event..\r\nSee error log for detail."));
        }
    }
}