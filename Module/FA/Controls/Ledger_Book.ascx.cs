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

public partial class Module_FA_Controls_Ledger_Book : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
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
            txtStartingDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txtEndingDate.Text = System.DateTime.Now.ToShortDateString();
            ShowLedger.Visible = false;
            ddlLedgerCode.SelectedIndex = -1;
            ddlLedgerCode.SelectedText = "";
            ddlLedgerCode.SelectedValue = "";
            trComboLedger.Visible = true;
            trTextLedger.Visible = false;
            chkShowHint.Visible = false;  // Ledger Tree Prob
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
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Dear ! Please select Ledger first..');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GetLedgerCode Button..\r\nSee error log for detail."));
        }
    }

    private void GetLedger_Book_By_Ledger(string Ledger_Code, string Vou_No, int IsDoc)
    {
        try
        {
            DataTable dtLedger_Book = SaitexBL.Interface.Method.FA_LEDGER_BOOK.Get_LEDGER_BOOK(oUserLoginDetail, DateTime.Parse(txtStartingDate.Text.Trim()), DateTime.Parse(txtEndingDate.Text.Trim()), Ledger_Code, Vou_No, IsDoc);
            if (dtLedger_Book.Rows.Count > 0)
            {
                grdLgr_Book.DataSource = dtLedger_Book;
                grdLgr_Book.DataBind();
                ViewState["dtLedger_Book"] = dtLedger_Book;
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

                lblAccountName.Text = "Details Of " + ddlLedgerCode.SelectedText.Trim() + " Ledger for the Period Of " + txtStartingDate.Text.Trim() + " To " + txtEndingDate.Text.Trim();
                ShowLedger.Visible = true;
            }
            else
            {
                ShowLedger.Visible = false;
                Common.CommonFuction.ShowMessage("Sorry ! No records found for the selected Ledger..");
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
            if (VOUCHER_NO != "999999" && VOUCHER_NO != "999998")
            {
                string URL = "JournalEntryForm.aspx?VOUCHER_NO=" + VOUCHER_NO;
                Response.Redirect(URL);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid RowCommand Event..\r\nSee error log for detail."));
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

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        
        {
          string LedgerCode=string.Empty;
            string StartDate=string.Empty;
            string EndDate=string.Empty;
            if(ddlLedgerCode.SelectedIndex>-1 && ddlLedgerCode.SelectedValue!=string.Empty)
            {
                LedgerCode=ddlLedgerCode.SelectedValue.Trim();
            }
            if(txtStartingDate.Text!=string.Empty && txtEndingDate.Text!=string.Empty)
            {
                StartDate=txtStartingDate.Text.Trim();
                EndDate=txtEndingDate.Text.Trim();

            }
            string URL = "../Reports/LedgerBookReport.aspx?LedgerCode=" + LedgerCode + "&StartDate="+StartDate+"&EndDate="+EndDate+"";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
        }
    }

    protected void grdLgr_Book_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList arr = e.RecordsCollection;
            Hashtable ht = arr[0] as Hashtable;
            string VOUCHER_NO = ht["VOUCHER_NO"].ToString();
            string VOUCHER_DT = DateTime.Parse(ht["JOURNAL_DATE"].ToString()).ToShortDateString();

            if (VOUCHER_NO != "999999" && VOUCHER_NO != "999998")
            {
                string URL = "~/Module/FA/Queries/JournalQueryForm.aspx?VOUCHER_NO=" + VOUCHER_NO + "&VOUCHER_DT=" + VOUCHER_DT;
                Response.Redirect(URL);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select Event..\r\nSee error log for detail."));
        }
    }

    protected void grdLgr_Book_Rebind(object sender, EventArgs e)
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Rebind Event..\r\nSee error log for detail."));
        }
    }

    protected void grdLgr_Book_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;

            if (e.Row.RowType == GridRowType.ColumnFooter)
            {
                double Dr_Total = 0;
                double Cr_Total = 0;
                DataTable dtLedger_Book = (DataTable)ViewState["dtLedger_Book"];
                for (int iLoop = 0; iLoop < dtLedger_Book.Rows.Count; iLoop++)
                {
                    double Amt = 0;
                    double.TryParse(dtLedger_Book.Rows[iLoop]["DR_AMOUNT"].ToString(), out Amt);
                    Dr_Total += Amt;

                    Amt = 0;
                    double.TryParse(dtLedger_Book.Rows[iLoop]["CR_AMOUNT"].ToString(), out Amt);
                    Cr_Total += Amt;
                }
                e.Row.Cells[7].Text = Dr_Total.ToString();
                e.Row.Cells[8].Text = Cr_Total.ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row DataBound..\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private void GetLedger_Book_By_LedgerAndDate(string Ledger_Code, string LGR_S_DT, string LGR_E_DT, string Vou_No, int IsDoc)
    {
        try
        {
            DataTable dtLedger_Book = SaitexBL.Interface.Method.FA_LEDGER_BOOK.Get_LEDGER_BOOK(oUserLoginDetail, DateTime.Parse(LGR_S_DT), DateTime.Parse(LGR_E_DT), Ledger_Code, Vou_No, IsDoc);
            if (dtLedger_Book.Rows.Count > 0)
            {
                grdLgr_Book.DataSource = dtLedger_Book;
                grdLgr_Book.DataBind();
                ViewState["dtLedger_Book"] = dtLedger_Book;
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

                lblAccountName.Text = "Ledger detail for the Period Of " + txtStartingDate.Text.Trim() + " to " + txtEndingDate.Text.Trim();
                ShowLedger.Visible = true;
            }
            else
            {
                ShowLedger.Visible = false;
                Common.CommonFuction.ShowMessage("Sorry ! No records found for the selected Ledger..");
            }

        }
        catch
        {
            throw;
        }
    }

    protected void chkHint_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkHint.Checked == true)
            {
                chkShowHint.Checked = false;
                trTextLedger.Visible = false;
                trComboLedger.Visible = true;
                ddlLedgerCode.SelectedIndex = -1;
                ddlLedgerCode.SelectedText = "";
            }
            else
            {
                chkShowHint.Checked = false;
                trComboLedger.Visible = false;
                trTextLedger.Visible = true;
                txtLedgerCode.Text = "";
                txtLedgerName.Text = "";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Hint CheckBox..\r\nSee error log for detail."));
        }
    }

    protected void chkShowHint_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkShowHint.Checked == true)
            {
                chkHint.Checked = false;
                trComboLedger.Visible = false;
                trTextLedger.Visible = true;
                txtLedgerCode.Text = "";
                txtLedgerName.Text = "";
            }
            else
            {
                chkHint.Checked = false;
                trComboLedger.Visible = true;
                trTextLedger.Visible = false;
                ddlLedgerCode.SelectedIndex = -1;
                ddlLedgerCode.SelectedText = "";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in ShowHint CheckBox..\r\nSee error log for detail."));
        }
    }

    protected void btnGetLedgerHint_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtLedgerCode.Text != "")
            {
                string Vou_No = string.Empty;
                int IsDoc = 0;
                GetLedger_Book_By_LedgerTextBox(txtLedgerCode.Text.Trim(), Vou_No, IsDoc);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Dear ! Please select Ledger from PopUp..');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GetLedgerHint Button..\r\nSee error log for detail."));
        }
    }

    protected void lnkbtnLedgerCode_Click(object sender, EventArgs e)
    {
        try
        {
            txtLedgerCode.ReadOnly = false;
            string URL = "FALedgerTree.aspx";
            URL = URL + "?TextBoxId=" + txtLedgerCode.ClientID;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=400,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in LedgerCode Button..\r\nSee error log for detail."));
        }
    }

    protected void txtLedgerCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetLedgerMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);

                dv.RowFilter = "LDGR_CODE= '" + txtLedgerCode.Text.Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtLedgerName.Text = dv[iLoop]["LDGR_NAME"].ToString();
                    }
                }
            }
            txtLedgerCode.ReadOnly = true;
            txtLedgerName.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in LedgerCode TextBox TextChanged Event..\r\nSee error log for detail."));
        }
    }

    private void GetLedger_Book_By_LedgerTextBox(string Ledger_Code, string Vou_No, int IsDoc)
    {
        try
        {
            DataTable dtLedger_Book = SaitexBL.Interface.Method.FA_LEDGER_BOOK.Get_LEDGER_BOOK(oUserLoginDetail, DateTime.Parse(txtStartingDate.Text.Trim()), DateTime.Parse(txtEndingDate.Text.Trim()), Ledger_Code, Vou_No, IsDoc);
            if (dtLedger_Book.Rows.Count > 0)
            {
                grdLgr_Book.DataSource = dtLedger_Book;
                grdLgr_Book.DataBind();
                ViewState["dtLedger_Book"] = dtLedger_Book;
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

                lblAccountName.Text = "Details of " + txtLedgerName.Text.Trim() + " ledger for the Period Of " + txtStartingDate.Text.Trim() + " to " + txtEndingDate.Text.Trim();
                ShowLedger.Visible = true;
            }
            else
            {
                ShowLedger.Visible = false;
                Common.CommonFuction.ShowMessage("Sorry ! No records found for the selected Ledger..");
            }

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
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox

            if (data != null && data.Rows.Count > 0)
            {
                ddlLedgerCode.Items.Clear();
                ddlLedgerCode.DataSource = data;
                ddlLedgerCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Ledger loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT LDGR_CODE, LDGR_NAME, GRP_NAME FROM V_FA_LGR_MST WHERE LDGR_CODE LIKE :SearchQuery OR LDGR_NAME LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery ORDER BY CAST(LDGR_CODE AS INT)) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND LDGR_CODE NOT IN (SELECT LDGR_CODE FROM (SELECT LDGR_CODE, LDGR_NAME, GRP_NAME FROM V_FA_LGR_MST WHERE LDGR_CODE LIKE :SearchQuery OR LDGR_NAME LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery ORDER BY CAST(LDGR_CODE AS INT)) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY CAST(LDGR_CODE AS INT)";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
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
            string CommandText = " SELECT * FROM (SELECT LDGR_CODE, LDGR_NAME, GRP_NAME FROM V_FA_LGR_MST WHERE LDGR_CODE LIKE :SearchQuery OR GRP_NAME LIKE :SearchQuery OR LDGR_NAME LIKE :SearchQuery ORDER BY CAST(LDGR_CODE AS INT)) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY CAST(LDGR_CODE AS INT) ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./Get_Ledger_Book.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
        }
    }
}