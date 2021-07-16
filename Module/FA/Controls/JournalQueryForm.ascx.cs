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

public partial class Module_FA_Controls_JournalQueryForm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtJournalMst;
    private static DataTable dtJournalTrn;
    private static DataTable dtVoucherType;
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

                if (Request.QueryString["VOUCHER_NO"] != null && Request.QueryString["VOUCHER_NO"].ToString() != "" && Request.QueryString["VOUCHER_DT"] != null && Request.QueryString["VOUCHER_DT"].ToString() != "")
                {
                    string Voucher_No = Request.QueryString["VOUCHER_NO"].ToString();
                    string Voucher_DT = Request.QueryString["VOUCHER_DT"].ToString();
                    SetDetailByVoucherNo(Voucher_No, Voucher_DT);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string URL = "../Reports/FA_Bank_Mst_Rpt.aspx";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
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
            //string URL = "../Help/BankMasterHelp.htm";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help..\r\nSee error log for detail."));
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
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    /// <summary>
    /// On Click any record of Grid, we can fetch the Journal transaction details in Transaction Grid..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdJournalMst_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            string strVou_No = "";
            
            dtJournalTrn = null;

            ArrayList arr = grdJournalMst.SelectedRecords;

            Hashtable ht = (Hashtable)arr[0];

            strVou_No = ht["VCHR_NO"].ToString();
            bindJournalTrn(strVou_No);

            dtJournalMst = null;
            bindJournalMstGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select..\r\nSee error log for detail."));
        }
    }
    /// <summary>
    /// Here, the Range validator works for Financial Year Date Checking, and bind Voucher Type also.
    /// </summary>
    private void InitialisePage()
    {
        try
        {
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
            grdJournalTrn.AutoPostBackOnSelect = false;

            bindVoucherTypes();
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// Bind CheckBox List, with Vouchers from Voucher Master.
    /// </summary>
    private void bindVoucherTypes()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetVoucherMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        chkVoucherType.Items.Add(new ListItem(dv[iLoop]["VCHR_NAME"].ToString().Trim(), dv[iLoop]["VCHR_CODE"].ToString().Trim()));
                    }
                }
                selectCheckboxes();
            }
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// Here, we checked all kind of validation related to fetch the data, and then fill the Journal Master according to Dates between and Voucher Type selection..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnVoucherType_Click(object sender, EventArgs e)
    {
        try
        {
            string strVou_Code = string.Empty;
            string strVou_Name = string.Empty;
            bool flagCheck = false;   // Flag to check weather any voucher type select or not.

            dtJournalMst = null;
            dtVoucherType = null;
            dtJournalTrn = null;

            if ((Convert.ToDateTime(txtStartingDate.Text)) > (Convert.ToDateTime(txtEndingDate.Text)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Starting date should be less than Ending date.');", true);
            }
            else
            {
                if (txtStartingDate.Text != "" || txtEndingDate.Text != "")
                {
                    if (chkVoucherType.Items.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < chkVoucherType.Items.Count; iLoop++)
                        {
                            if (chkVoucherType.Items[iLoop].Selected == true)
                            {
                                flagCheck = true;
                                break;
                            }
                        }
                        if (flagCheck)
                        {
                            if (dtVoucherType == null)
                                CreateVoucherTypeDT();

                            if (chkVoucherType.Items.Count > 0)
                            {
                                for (int iLoop = 0; iLoop < chkVoucherType.Items.Count; iLoop++)
                                {
                                    if (chkVoucherType.Items[iLoop].Selected == true)
                                    {
                                        strVou_Code = chkVoucherType.Items[iLoop].Value.ToUpper().Trim();
                                        strVou_Name = chkVoucherType.Items[iLoop].Text.ToUpper().Trim();
                                        insertVoucherTypeDT(strVou_Code, strVou_Name);
                                    }
                                }
                            }
                            bindJournalMstGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Please.. select voucher type.');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Please.. enter the starting and ending date.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Voucher Type Button..\r\nSee error log for detail."));
        }
    }
    /// <summary>
    /// Here, we fetch the data from Journal Master and Insert into a temp table according to selection..
    /// </summary>
    private void bindJournalMstGrid()
    {
        try
        {
            string strVou_Code = string.Empty;
            string strVou_Name = string.Empty;
            string strVou_No = string.Empty;
            DateTime dateJounalDate;
            string strDesc = string.Empty;

            SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

            oFA_Journal_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_Journal_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFA_Journal_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFA_Journal_MST.FinStartDate = Convert.ToDateTime(txtStartingDate.Text.Trim());
            oFA_Journal_MST.FinEndDate = Convert.ToDateTime(txtEndingDate.Text.Trim());

            if (dtJournalMst == null)
                CreateDataTableMst();

            DataTable dt = SaitexBL.Interface.Method.FA_Journal_DTL.SelectJournalMstFin(oFA_Journal_MST);

            if (dtVoucherType != null)
            {
                DataView dv = new DataView(dtVoucherType);
                
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        strVou_Code = dv[iLoop]["VCHR_CODE"].ToString();

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataView dv1 = new DataView(dt);

                            dv1.RowFilter = "VCHR_CODE='" + strVou_Code + "'";

                            if (dv1.Count > 0)
                            {
                                for (int jLoop = 0; jLoop < dv1.Count; jLoop++)
                                {
                                    strVou_Code = dv1[jLoop]["VCHR_CODE"].ToString();
                                    strVou_Name = dv1[jLoop]["VCHR_NAME"].ToString();
                                    strVou_No = dv1[jLoop]["VCHR_NO"].ToString();
                                    dateJounalDate = Convert.ToDateTime(dv1[jLoop]["JOURNAL_DATE"].ToString());
                                    strDesc = dv1[jLoop]["DESCRIPTION"].ToString();
                                    insertJournalMst(strVou_Code, strVou_Name, strVou_No, dateJounalDate, strDesc);
                                }
                            }
                        }
                    }
                }
            }
         
            if (dtJournalMst != null)
            {
                grdJournalMst.DataSource = dtJournalMst;
                grdJournalMst.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// Here, we fetch the data from Journal Transaction and Insert into a temp table according to selection throught Jornal Master Grid..
    /// </summary>
    /// <param name="strVou_No">Voucher Number</param>
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

            dtJournalTrn = null;

            SaitexDM.Common.DataModel.FA_Journal_MST oFA_Journal_MST = new SaitexDM.Common.DataModel.FA_Journal_MST();

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

            if (dtJournalTrn != null)
            {
                grdJournalTrn.DataSource = dtJournalTrn;
                grdJournalTrn.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void chkAllVouchers_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            selectCheckboxes();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Check All Vouchers Checkbox..\r\nSee error log for detail."));
        }
    }
    /// <summary>
    /// Here, according to Checkbox named "chkAllVouchers", we set the checkboxes checked OR Unchecked..
    /// </summary>
    private void selectCheckboxes()
    {
        try
        {
            if (chkAllVouchers.Checked == true)
            {
                if (chkVoucherType.Items.Count > 0)
                {
                    for (int iLoop = 0; iLoop < chkVoucherType.Items.Count; iLoop++)
                    {
                        chkVoucherType.Items[iLoop].Selected = true;
                    }
                }
            }
            else
            {
                if (chkVoucherType.Items.Count > 0)
                {
                    for (int iLoop = 0; iLoop < chkVoucherType.Items.Count; iLoop++)
                    {
                        chkVoucherType.Items[iLoop].Selected = false;
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// To Create a temporary table for Journal Master..
    /// </summary>
    private void CreateDataTableMst()
    {
        try
        {
            dtJournalMst = new DataTable();
            dtJournalMst.Columns.Add("VCHR_CODE", typeof(string));
            dtJournalMst.Columns.Add("VCHR_NAME", typeof(string));
            dtJournalMst.Columns.Add("VCHR_NO", typeof(string));
            dtJournalMst.Columns.Add("JOURNAL_DATE", typeof(DateTime));
            dtJournalMst.Columns.Add("DESCRIPTION", typeof(string));
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// To Create a temporary table for Journal Transaction..
    /// </summary>
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
    /// <summary>
    /// To Create a temporary table for Selected Voucher type from Checkbox list..
    /// </summary>
    private void CreateVoucherTypeDT()
    {
        try
        {
            dtVoucherType = new DataTable();
            dtVoucherType.Columns.Add("VCHR_CODE", typeof(string));
            dtVoucherType.Columns.Add("VCHR_NAME", typeof(string));
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// Insert data in temporary table, for Voucher Type..
    /// </summary>
    /// <param name="strVou_Code">Voucher Code</param>
    /// <param name="strVou_Name">Voucher Name</param>
    private void insertVoucherTypeDT(string strVou_Code, string strVou_Name)
    {
        try
        {
            DataRow dr = dtVoucherType.NewRow();
            dr["VCHR_CODE"] = strVou_Code;
            dr["VCHR_NAME"] = strVou_Name;

            dtVoucherType.Rows.Add(dr);
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// Insert data in temporary table, for Journal Master..
    /// </summary>
    /// <param name="strVou_Code">Voucher Code</param>
    /// <param name="strVou_Name">Voucher Name</param>
    /// <param name="strVou_No">Voucher Number</param>
    /// <param name="dateJounalDate">Journal Date</param>
    /// <param name="strDesc">Journal Master Description</param>
    private void insertJournalMst(string strVou_Code, string strVou_Name, string strVou_No, DateTime dateJounalDate, string strDesc)
    {
        try
        {
            DataRow dr = dtJournalMst.NewRow();
            dr["VCHR_CODE"] = strVou_Code;
            dr["VCHR_NAME"] = strVou_Name;
            dr["VCHR_NO"] = strVou_No;
            dr["JOURNAL_DATE"] = dateJounalDate;
            dr["DESCRIPTION"] = strDesc;

            dtJournalMst.Rows.Add(dr);
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// Insert data in temporary table, for Journal Transaction..
    /// </summary>
    /// <param name="strLedger_Code">Ledger Code</param>
    /// <param name="strLedger_Name">Ledger Name</param>
    /// <param name="strDebit">Is_Debit</param>
    /// <param name="dblAmount">Debit Amount</param>
    /// <param name="strDoc_No">Document Number</param>
    /// <param name="strDoc_DT">Document Date</param>
    /// <param name="strDesc">Transaction Narration</param>
    /// <param name="strVou_No">Voucher Number</param>
    private void insertJournalTrn(string strLedger_Code, string strLedger_Name, string strDebit, double dblAmount, string strDoc_No, string strDoc_DT, string strDesc, string strVou_No)
    {
        try
        {
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
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// This function calls, when this page calls through another reporting page with Query String (Voucher Number).
    /// </summary>
    /// <param name="Voucher_No">Pass voucher number through Query String..</param>
    private void SetDetailByVoucherNo(string Voucher_No, string Voucher_DT)
    {
        try
        {
            trSelectVoucherType.Visible = false;
            trCheckBoxList.Visible = false;
            trClickVoucher.Visible = false;
            trJournalMst.Visible = false;
            trJournalTrn.Visible = true;
            txtStartingDate.Text = Voucher_DT;
            txtEndingDate.Text = Voucher_DT;
            txtStartingDate.Enabled = false;
            txtEndingDate.Enabled = false;
            
            bindJournalTrn(Voucher_No);
        }
        catch
        {
            throw;
        }
    }
}
