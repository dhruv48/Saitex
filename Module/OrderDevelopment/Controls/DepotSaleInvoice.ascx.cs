using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;
using System.IO;

public partial class Module_OrderDevelopment_Controls_DepotSaleInvoice : System.Web.UI.UserControl
{
    private DataTable dtDepotTRN_SUB = null;
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    private static string TRN_TYPE = "IYS28";
    private static int IsUpdateCall = 2;
    SaitexDM.Common.DataModel.YRN_IR_ISS_ADJ oYRN_IR_ISS_ADJ = new SaitexDM.Common.DataModel.YRN_IR_ISS_ADJ();

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
                    string TRN_NUMB = string.Empty;
                    TRN_NUMB = Request.QueryString["TRN_NUMB"].ToString();
                    CommonFuction.ShowMessage(@"Depot Invoice : " + TRN_NUMB + " saved successfully.");
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated successfully');", true);
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            TRN_TYPE = "IYS28";
            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();

            StartDate = oUserLoginDetail.DT_STARTDATE;
            EndDate = Common.CommonFuction.GetYearEndDate(StartDate);

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();
            Session["dtTRN_SUB"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void BindNewMRNNum()
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.TRN_TYPE = TRN_TYPE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            txtTRNNUMBer.Text = SaitexBL.Interface.Method.YRN_IR_MST.GetNewTRNNumber(oYRN_IR_MST).ToString();
        }
        catch
        {
            throw;
        }
    }

    private void Blankrecords()
    {
        try
        {
            txtPartyCode.SelectedIndex = -1;
            ddlReceiptShift.SelectedIndex = 0;
            lblPartyCode.Text = string.Empty;

            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB != null)
                dtDepotTRN_SUB.Rows.Clear();

            lblMode.Text = "You are in Save Mode";

            RefreshDetailRow();
            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtDepotTRN_SUB = new DataTable();
            dtDepotTRN_SUB.Columns.Add("UNIQUEID", typeof(int));
            dtDepotTRN_SUB.Columns.Add("TRNNUMB", typeof(int));
            dtDepotTRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtDepotTRN_SUB.Columns.Add("YARN_CODE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("YARN_DESC", typeof(string));
            dtDepotTRN_SUB.Columns.Add("SHADE_CODE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("UOM", typeof(string));
            dtDepotTRN_SUB.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDepotTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtDepotTRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDepotTRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDepotTRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDepotTRN_SUB.Columns.Add("BASIC_RATE", typeof(double));
            dtDepotTRN_SUB.Columns.Add("FINAL_RATE", typeof(double));
            dtDepotTRN_SUB.Columns.Add("AMOUNT", typeof(double));
            dtDepotTRN_SUB.Columns.Add("COST_CODE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("MAC_CODE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("REMARKS", typeof(string));
            dtDepotTRN_SUB.Columns.Add("QCFLAG", typeof(string));
            dtDepotTRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtDepotTRN_SUB.Columns.Add("PI_NO", typeof(string));
            dtDepotTRN_SUB.Columns.Add("REM_QTY", typeof(double));

        }
        catch
        {
            throw;
        }
    }

    private void InsertBlankRowInTable()
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB == null)
                CreateDataTable();

            DataRow dr = dtDepotTRN_SUB.NewRow();
            dr["UNIQUEID"] = dtDepotTRN_SUB.Rows.Count + 1;
            dr["DATE_OF_MFG"] = DateTime.Now.Date.ToShortDateString();
            dtDepotTRN_SUB.Rows.Add(dr);
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void BindGridFromDataTable()
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB == null)
            {
                // InsertBlankRowInTable();
            }
            grdMaterialItemReceipt.DataSource = dtDepotTRN_SUB;
            grdMaterialItemReceipt.DataBind();
            CalculateTotalAmount();
        }
        catch
        {
            throw;
        }
    }

    private void BindShift()
    {
        try
        {
            DataTable dtShift = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftMaster();
            if (dtShift != null && dtShift.Rows.Count > 0)
            {
                ddlReceiptShift.DataSource = dtShift;
                ddlReceiptShift.DataTextField = "SFT_NAME";
                ddlReceiptShift.DataValueField = "SFT_NAME";
                ddlReceiptShift.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB.Rows.Count == 1)
            {
                dtDepotTRN_SUB.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDepotTRN_SUB.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUEID"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
                        dtDepotTRN_SUB.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDepotTRN_SUB.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUEID"] = iCount;
                }
            }
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
            imgbtnDelete.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to delete this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
            imgbtnSave.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to save this record')");
            imgbtnUpdate.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to update this record')");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                saveMaterialReceipt();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            bool ReturnResult = false;
            int count = 0;
            msg = string.Empty;

            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (txtTRNNUMBer.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. MRN Number required.\r\n";
            }

            if (lblPartyCode.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select party first.\r\n";
            }

            if (dtDepotTRN_SUB != null && dtDepotTRN_SUB.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
            }

            if (count == 3)
                ReturnResult = true;

            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ActivateUpdateMode();
            lblMode.Text = "You are in Update Mode";
            txtTRNNUMBer.Text = string.Empty;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                UpdateMaterialReceipt();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/OrderDevelopment/Pages/DepotSaleInvoice.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Response.Redirect("~/Module/Yarn/SalesWork/Reports/YRN_ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
            Response.Redirect("~/Module/Inventory/Pages/Vat_Retail_Report.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdMaterialItemReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUEID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItemReceiptRow(UNIQUEID);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItemReceiptRow(UNIQUEID);

                BindGridFromDataTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row-Command Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int PONumb, int UNIQUEID, string SHADE_CODE)
    {
        bool Result = false;
        try
        {
            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                    Label txtShadeCode = (Label)grdRow.FindControl("txtShadeCode");
                    Label txtPONum = (Label)grdRow.FindControl("txtPONum");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (int.Parse(txtPONum.Text.Trim()) == PONumb && txtICODE.Text.Trim() == ItemCode && SHADE_CODE == txtShadeCode.Text.Trim() && UNIQUEID != iUNIQUEID)
                        Result = true;
                }
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    private bool ValidatePONumber(string sPONumb)
    {
        try
        {
            bool Result = false;
            int poNumb = 0;

            if (int.TryParse(sPONumb, out poNumb))
                Result = true;

            return Result;
        }
        catch
        {
            throw;
        }
    }

    private void saveMaterialReceipt()
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);

            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;

            oYRN_IR_MST.FORM_NUMB = string.Empty;
            oYRN_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;

            htReceive.Add("GATE_ENTRY", false);
            oYRN_IR_MST.GATE_DATE = dt;

            oYRN_IR_MST.GATE_NUMB = string.Empty;
            oYRN_IR_MST.GATE_OUT_NUMB = string.Empty;
            oYRN_IR_MST.GATE_PASS_TYPE = string.Empty;
            oYRN_IR_MST.LORY_NUMB = string.Empty;

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;

            htReceive.Add("PARTY_CHALLAN", false);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = string.Empty;
            oYRN_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
            oYRN_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oYRN_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            if (txtTransporterCode.SelectedIndex < 0)
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;

            double TotalAmount = 0;
            double.TryParse(txtTotalAmount.Text, out TotalAmount);
            oYRN_IR_MST.TOTAL_AMOUNT = TotalAmount;
            oYRN_IR_MST.FINAL_AMOUNT = TotalAmount;

            int TRN_NUMB = 0;
            oYRN_IR_MST.LOT_ID_NO = string.Empty;
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            DataTable dtDepotRate1 = (DataTable)Session["dtDepotRate1"];
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.DepotSaleInvoiceInsertMst(oYRN_IR_MST, out TRN_NUMB, dtDepotTRN_SUB, dtItemReceipt, htReceive, dtDepotRate1);

            if (result)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./DepotSaleInvoice.aspx?cId=S&TRN_NUMB=" + TRN_NUMB, false);

                //InitialisePage();
                //CommonFuction.ShowMessage(@"Depot Invoice : " + TRN_NUMB + " saved successfully.");
                //if (dtDepotTRN_SUB != null)
                //{
                //    dtDepotTRN_SUB.Rows.Clear();
                //}
                //grdMaterialItemReceipt.DataSource = null;
                //grdMaterialItemReceipt.DataBind();
                //txtPartyCode.SelectedIndex = -1;
                //txtTransporterCode.SelectedIndex = -1;
                //lblPartyCode.Text = string.Empty;
                //lblTransporterCode.Text = string.Empty;
                //txtPartyAddress.Text = string.Empty;
                //txtTransporterAddress.Text = string.Empty;
            }
            else
            {
                CommonFuction.ShowMessage("Depot Invoice  Saving Failed");
            }
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void UpdateMaterialReceipt()
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = string.Empty;
            oYRN_IR_MST.FORM_TYPE = string.Empty;

            DateTime dt = System.DateTime.Now.Date;

            htReceive.Add("GATE_ENTRY", false);
            oYRN_IR_MST.GATE_DATE = dt;

            oYRN_IR_MST.GATE_NUMB = string.Empty;
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = string.Empty;

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;

            htReceive.Add("PARTY_CHALLAN", false);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = string.Empty;
            oYRN_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
            oYRN_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
            oYRN_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oYRN_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oYRN_IR_MST.TRN_DATE = dt;

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            if (txtTransporterCode.SelectedIndex < 0)
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.LOT_ID_NO = string.Empty;

            double TotalAmount = 0;
            double.TryParse(txtTotalAmount.Text, out TotalAmount);
            oYRN_IR_MST.TOTAL_AMOUNT = TotalAmount;
            oYRN_IR_MST.FINAL_AMOUNT = TotalAmount;

            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            DataTable dtDepotRate1 = (DataTable)Session["dtDepotRate1"];
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.DepotSaleInvoiceUpdate(oYRN_IR_MST, dtDepotTRN_SUB, dtItemReceipt, htReceive, dtDepotRate1);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Depot Invoice Updated SuccessFully");
                if (dtDepotTRN_SUB != null)
                {
                    dtDepotTRN_SUB.Rows.Clear();
                }
                grdMaterialItemReceipt.DataSource = null;
                grdMaterialItemReceipt.DataBind();
                CalculateTotalAmount();
                txtPartyCode.SelectedIndex = -1;
                txtTransporterCode.SelectedIndex = -1;
                lblTransporterCode.Text = string.Empty;
                lblPartyCode.Text = string.Empty;
                txtPartyAddress.Text = string.Empty;
                txtTransporterAddress.Text = string.Empty;
            }
            else
            {
                CommonFuction.ShowMessage("Depot Invoice Failed");
            }
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private int GetdataByTRNNUMBer(int TRNNUMBer)
    {
        int iRecordFound = 0;

        if (ViewState["dtDepotTRN_SUB"] != null)
            dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.DepotSaleInvoiceDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                DateTime dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["GATE_DATE"].ToString().Trim(), out dd))

                    dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["LR_DATE"].ToString().Trim(), out dd))
                    txtLRDate.Text = dd.ToShortDateString();

                txtLRNo.Text = dt.Rows[0]["LR_NUMB"].ToString().Trim();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["TRN_DATE"].ToString().Trim(), out dd))
                    txtMRNDate.Text = dd.ToShortDateString();

                txtTRNNUMBer.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();
                txtPartyCode.SelectedText = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                txtTransporterCode.SelectedText = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();

                txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TAddress"].ToString().Trim();
            }
            if (iRecordFound == 1)
            {
                dtDepotTRN_SUB.Rows.Clear();
                dtDepotTRN_SUB = SaitexBL.Interface.Method.YRN_IR_MST.DepotSaleInvoiceTrnDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtDepotTRN_SUB.Rows.Count > 0)
                {
                    MapDataTable();
                    if (dtDepotTRN_SUB != null && dtDepotTRN_SUB.Rows.Count > 0)
                    {
                        DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.DepotSaleInvoiceGetAdjustReceiving(dtDepotTRN_SUB.Rows[0]["PO_TYPE"].ToString(), Convert.ToInt32(dtDepotTRN_SUB.Rows[0]["PO_NUMB"].ToString()), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                        Session["dtItemReceipt"] = dtReceiptAdjustment;
                    }
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Yarn Detail";
                    Common.CommonFuction.ShowMessage(msg);
                    InitialisePage();
                    txtTRNNUMBer.Text = string.Empty;
                    ddlTRNNumber.Focus();
                    lblMode.Text = "Update";
                    ActivateUpdateMode();
                    RefreshDetailRow();
                }
            }
            else
            {
                string msg = "Dear " + oUserLoginDetail.Username + " !! MRN already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }

            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable()
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (!dtDepotTRN_SUB.Columns.Contains("UNIQUEID"))
                dtDepotTRN_SUB.Columns.Add("UNIQUEID", typeof(int));

            if (!dtDepotTRN_SUB.Columns.Contains("MAC_CODE"))
                dtDepotTRN_SUB.Columns.Add("MAC_CODE", typeof(string));

            for (int iLoop = 0; iLoop < dtDepotTRN_SUB.Rows.Count; iLoop++)
            {
                dtDepotTRN_SUB.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDepotTRN_SUB.Rows[iLoop]["MAC_CODE"] = string.Empty;
            }
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode.Items.Clear();

            txtPartyCode.DataSource = data;
            txtPartyCode.DataTextField = "PRTY_CODE";
            txtPartyCode.DataValueField = "address";
            txtPartyCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Loading..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT DISTINCT * FROM (SELECT * FROM (SELECT DISTINCT v.PRTY_CODE, PRTY_STATE, PRTY_ADD1, PRTY_ADD2, PRTY_NAME,PRTY_ADD1 || ',' || NVL (PRTY_ADD2, ' ') || ',' || NVL (PRTY_STATE, ' ')address FROM TX_VENDOR_MST v, YRN_SO_MST p WHERE V.PRTY_CODE = P.PRTY_CODE AND SO_TYPE IN ('SSM', 'SSS') ORDER BY PRTY_CODE) WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery OR PRTY_ADD2 LIKE :SearchQuery OR PRTY_STATE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " and PRTY_CODE not in ( SELECT DISTINCT PRTY_CODE FROM (SELECT * FROM (SELECT DISTINCT v.PRTY_CODE, PRTY_STATE, PRTY_ADD1, PRTY_ADD2, PRTY_NAME,PRTY_ADD1 || ',' || NVL (PRTY_ADD2, ' ') || ',' || NVL (PRTY_STATE, ' ') address FROM TX_VENDOR_MST v, YRN_SO_MST p WHERE V.PRTY_CODE = P.PRTY_CODE AND SO_TYPE IN ('SSM', 'SSS') ORDER BY PRTY_CODE) WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery OR PRTY_ADD2 LIKE :SearchQuery OR PRTY_STATE LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "' )";
            }

            string SortExpression = " ORDER BY PRTY_CODE ASC";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {
        string CommandText = " SELECT DISTINCT * FROM (SELECT * FROM (SELECT DISTINCT v.PRTY_CODE, PRTY_STATE, PRTY_ADD1, PRTY_ADD2, PRTY_NAME,PRTY_ADD1 || ',' || NVL (PRTY_ADD2, ' ') || ',' || NVL (PRTY_STATE, ' ')address FROM TX_VENDOR_MST v, YRN_SO_MST p WHERE V.PRTY_CODE = P.PRTY_CODE AND SO_TYPE IN ('SSM', 'SSS') ORDER BY PRTY_CODE) WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery OR PRTY_ADD2 LIKE :SearchQuery OR PRTY_STATE LIKE :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY PRTY_CODE ASC ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void txtPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (IsUpdateCall != 1)
                ResetDetailOnPartySelection();

            lblPartyCode.Text = txtPartyCode.SelectedText.ToString().Trim();
            txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void ResetDetailOnPartySelection()
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            RefreshDetailRow();

            if (dtDepotTRN_SUB == null)
            {
                CreateDataTable();
            }
            else
            {
                dtDepotTRN_SUB.Rows.Clear();
            }
            IsUpdateCall = IsUpdateCall + 1;
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    protected void txtTransporterCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetTransData(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtTransporterCode.Items.Clear();
                txtTransporterCode.DataSource = data;
                txtTransporterCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetTransCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transporters Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetTransData(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_GRP_CODE = 'Transporter' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_GRP_CODE = 'Transporter' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY PRTY_CODE ASC";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetTransCount(string text)
    {
        string CommandText = " SELECT * FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_GRP_CODE = 'Transporter' AND Del_Status = 0) asd WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY PRTY_CODE ASC ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void txtTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblTransporterCode.Text = txtTransporterCode.SelectedText.ToString().Trim();
            txtTransporterAddress.Text = txtTransporterCode.SelectedValue;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transporter Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB == null)
                CreateDataTable();

            if (dtDepotTRN_SUB.Rows.Count < 15)
            {
                if (cmbPOITEM.Text != "" && txtICODE.Text != "" && txtShade.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                    bool bb = SearchItemCodeInGrid(txtICODE.Text.Trim(), int.Parse(cmbPOITEM.Text), UNIQUEID, txtShade.Text);
                    if (!bb)
                    {
                        double Qty = 0;
                        double.TryParse(txtQTY.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {
                            if (UNIQUEID > 0)
                            {
                                DataView dv = new DataView(dtDepotTRN_SUB);
                                dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                                if (dv.Count > 0)
                                {
                                    dv[0]["PO_NUMB"] = cmbPOITEM.Text.Trim();
                                    dv[0]["PO_TYPE"] = lblSO_TYPE.Text;
                                    dv[0]["PO_COMP_CODE"] = lblSO_COMP.Text;
                                    dv[0]["pO_BRANCH"] = lblSO_BRANCH.Text;
                                    dv[0]["YARN_CODE"] = txtICODE.Text.Trim();
                                    dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                    dv[0]["SHADE_CODE"] = txtShade.Text.Trim();
                                    dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                    dv[0]["UOM"] = txtBaseUOM.Text.Trim();
                                    dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                    dv[0]["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                    dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                    dv[0]["QCFLAG"] = "No";

                                    dv[0]["PI_NO"] = "NA";

                                    dv[0]["UOM_OF_UNIT"] = txtBaseUOM.Text.Trim();
                                    dv[0]["NO_OF_UNIT"] = double.Parse(txtNoOfUnit.Text.Trim());
                                    dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtUnitWeight.Text.Trim());
                                    double rem_Qty = 0;
                                    double.TryParse(lblMaxQTY.Text, out rem_Qty);
                                    dv[0]["REM_QTY"] = rem_Qty;

                                    dtDepotTRN_SUB.AcceptChanges();
                                }
                            }
                            else
                            {
                                DataRow dr = dtDepotTRN_SUB.NewRow();
                                dr["UNIQUEID"] = dtDepotTRN_SUB.Rows.Count + 1;
                                dr["PO_NUMB"] = cmbPOITEM.Text.Trim();
                                dr["PO_TYPE"] = lblSO_TYPE.Text;
                                dr["PO_COMP_CODE"] = lblSO_COMP.Text;
                                dr["PO_BRANCH"] = lblSO_BRANCH.Text;
                                dr["YARN_CODE"] = txtICODE.Text.Trim();
                                dr["YARN_DESC"] = txtDESC.Text.Trim();
                                dr["SHADE_CODE"] = txtShade.Text.Trim();
                                dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dr["UOM"] = txtBaseUOM.Text.Trim();
                                dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dr["AMOUNT"] = double.Parse(txtAmount.Text.Trim());

                                dr["MAC_CODE"] = string.Empty;
                                dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                dr["QCFLAG"] = "No";

                                dr["PI_NO"] = "NA";

                                dr["UOM_OF_UNIT"] = txtBaseUOM.Text.Trim();
                                dr["NO_OF_UNIT"] = double.Parse(txtNoOfUnit.Text.Trim());
                                dr["WEIGHT_OF_UNIT"] = double.Parse(txtUnitWeight.Text.Trim());
                                double rem_Qty = 0;
                                double.TryParse(lblMaxQTY.Text, out rem_Qty);
                                dr["REM_QTY"] = rem_Qty;

                                dtDepotTRN_SUB.Rows.Add(dr);
                            }
                            RefreshDetailRow();
                            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
                    }
                }
                grdMaterialItemReceipt.DataSource = dtDepotTRN_SUB;
                grdMaterialItemReceipt.DataBind();
                CalculateTotalAmount();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transaction Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnTRNCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transaction Cancel Button..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void lblPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetSOData(e.Text.ToUpper(), e.ItemsOffset);
            lblPOITEM.Items.Clear();
            lblPOITEM.DataSource = data;
            lblPOITEM.DataTextField = "SO_NUMB";
            lblPOITEM.DataValueField = "So_Item_trn";
            lblPOITEM.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;

            e.ItemsCount = GetSODataCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Item Loading..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetSOData(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT ( PT.SO_TYPE|| '@'|| PT.SO_NUMB|| '@'|| PT.YARN_CODE|| '@'|| PT.SHADE_CODE|| '@'|| PT.UOM|| '@'|| PT.UOM_OF_UNIT|| '@'|| PT.WEIGHT_OF_UNIT|| '@'|| PT.NO_OF_UNIT|| '@'||(NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0))|| '@'|| PT.BASIC_RATE|| '@'|| PT.FINAL_RATE|| '@'|| PT.BRANCH_CODE|| '@'|| PT.COMP_CODE|| '@'|| PT.SO_TYPE|| '@'|| PT.YARN_DESC)So_Item_trn, pt.SO_NUMB, pt.YARN_CODE, pt.shade_code, pt.prty_code, pt.ORD_QTY, pt.YARN_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM v_YRN_SO_TRN pt WHERE (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND PT.SO_TYPE IN ('SSM', 'SSS') AND pt.PRTY_CODE = '" + lblPartyCode.Text + "' and pt.comp_code='" + oUserLoginDetail.COMP_CODE + "' AND pt.Branch_code='" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY SO_NUMB) WHERE SO_NUMB LIKE :SearchQuery OR YARN_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND So_Item_trn NOT IN(SELECT So_Item_trn FROM (SELECT * FROM (SELECT DISTINCT ( PT.SO_TYPE|| '@'|| PT.SO_NUMB|| '@'|| PT.YARN_CODE|| '@'|| PT.SHADE_CODE|| '@'|| PT.UOM|| '@'|| PT.UOM_OF_UNIT|| '@'|| PT.WEIGHT_OF_UNIT|| '@'|| PT.NO_OF_UNIT|| '@'||(NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0))|| '@'|| PT.BASIC_RATE|| '@'|| PT.FINAL_RATE|| '@'|| PT.BRANCH_CODE|| '@'|| PT.COMP_CODE|| '@'|| PT.SO_TYPE|| '@'|| PT.YARN_DESC) So_Item_trn,pt.SO_NUMB,pt.YARN_CODE,pt.shade_code,pt.prty_code,pt.ORD_QTY,pt.YARN_DESC,NVL (PT.QTY_RCPT, 0) QTY_RCPT,NVL (PT.ORD_QTY, 0)- NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM v_YRN_SO_TRN pt WHERE (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0AND PT.SO_TYPE IN ('SSM', 'SSS')AND pt.PRTY_CODE = '" + lblPartyCode.Text + "' and pt.comp_code='" + oUserLoginDetail.COMP_CODE + "' AND pt.Branch_code='" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY SO_NUMB)WHERE SO_NUMB LIKE :SearchQuery OR YARN_CODE LIKE :SearchQuery)WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " ORDER BY SO_NUMB ASC";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetSODataCount(string text)
    {
        string CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT ( PT.SO_TYPE|| '@'|| PT.SO_NUMB|| '@'|| PT.YARN_CODE|| '@'|| PT.SHADE_CODE|| '@'|| PT.UOM|| '@'|| PT.UOM_OF_UNIT|| '@'|| PT.WEIGHT_OF_UNIT|| '@'|| PT.NO_OF_UNIT|| '@'||(NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0))|| '@'|| PT.BASIC_RATE|| '@'|| PT.FINAL_RATE|| '@'|| PT.BRANCH_CODE|| '@'|| PT.COMP_CODE|| '@'|| PT.SO_TYPE|| '@'|| PT.YARN_DESC) So_Item_trn, pt.SO_NUMB, pt.YARN_CODE, pt.shade_code, pt.prty_code, pt.ORD_QTY, pt.YARN_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM v_YRN_SO_TRN pt WHERE (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND PT.SO_TYPE IN ('SSM', 'SSS') AND pt.PRTY_CODE = '" + lblPartyCode.Text + "' and pt.comp_code='" + oUserLoginDetail.COMP_CODE + "' AND pt.Branch_code='" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY SO_NUMB) WHERE SO_NUMB LIKE :SearchQuery OR YARN_CODE LIKE :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY SO_NUMB ASC ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void lblPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = lblPOITEM.SelectedValue.ToString().Trim();
            GetDataForDetail(cString);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Item Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void GetDataForDetail(string cString)
    {
        try
        {
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string SO_Type = arrString[0].ToString();
            int SONumb = int.Parse(arrString[1].ToString());
            string YARN_CODE = arrString[2].ToString();
            string ShadeCode = arrString[3].ToString();

            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());

            if (!SearchItemCodeInGrid(YARN_CODE, SONumb, UNIQUEID, ShadeCode))
            {
                string UOM = arrString[4].ToString();
                string UOM_OF_UNIT = arrString[5].ToString();
                double WEIGHT_OF_UNIT = double.Parse(arrString[6].ToString());
                double NO_OF_UNIT = double.Parse(arrString[7].ToString());

                double QTY_REM = double.Parse(arrString[8].ToString());
                double BASIC_RATE = double.Parse(arrString[9].ToString());
                double FINAL_RATE = double.Parse(arrString[10].ToString());
                string sBRANCH_CODE = arrString[11].ToString();
                string sCOMP_CODE = arrString[12].ToString();
                string sSO_TYPE = arrString[13].ToString();
                string YARN_DESC = arrString[14].ToString();

                cmbPOITEM.Text = lblPOITEM.SelectedText.ToString().Trim();
                txtICODE.Text = YARN_CODE;
                txtDESC.Text = YARN_DESC;
                txtShade.Text = ShadeCode;
                txtBaseUOM.Text = UOM;
                lblMaxQTY.Text = QTY_REM.ToString();
                txtBasicRate.Text = BASIC_RATE.ToString();
                txtFinalRate.Text = FINAL_RATE.ToString();
                txtAmount.Text = (FINAL_RATE * QTY_REM).ToString();
                txtBaseUOM.Text = UOM_OF_UNIT;
                txtNoOfUnit.Text = NO_OF_UNIT.ToString();
                txtUnitWeight.Text = WEIGHT_OF_UNIT.ToString();

                lblSO_BRANCH.Text = sBRANCH_CODE;
                lblSO_COMP.Text = sCOMP_CODE;
                lblSO_TYPE.Text = sSO_TYPE;
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Item Already Included");
            }

        }
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            cmbPOITEM.Text = string.Empty;
            lblPOITEM.SelectedIndex = -1;
            txtICODE.Text = string.Empty;
            txtDESC.Text = string.Empty;
            txtShade.Text = string.Empty;
            txtQTY.Text = string.Empty;
            txtBaseUOM.Text = string.Empty;
            txtBasicRate.Text = string.Empty;
            txtFinalRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtDetRemarks.Text = string.Empty;
            lblSO_BRANCH.Text = string.Empty;
            lblSO_COMP.Text = string.Empty;
            lblSO_TYPE.Text = string.Empty;
            lblMaxQTY.Text = string.Empty;
            lblPOITEM.Enabled = true;
            ViewState["UNIQUEID"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void EditItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            DataView dv = new DataView(dtDepotTRN_SUB);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                cmbPOITEM.Text = dv[0]["PO_NUMB"].ToString();
                lblSO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblSO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblSO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                txtICODE.Text = dv[0]["YARN_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtBaseUOM.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();

                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();

                txtBaseUOM.Text = dv[0]["UOM_OF_UNIT"].ToString();
                txtUnitWeight.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtNoOfUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                lblMaxQTY.Text = dv[0]["REM_QTY"].ToString();
                lblPOITEM.Enabled = false;
                ViewState["UNIQUEID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetReceiving(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "TRN_NUMB";
            thisTextBox.DataValueField = "TRN_NUMB";
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transaction Number Loading..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " where TRN_NUMB like :searchQuery or TRN_DATE like :searchQuery or prty_code like :searchQuery or prty_name like :searchQuery";
            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";
            string commandText = "select * from (Select a.TRN_NUMB, TO_CHAR (a.TRN_DATE, 'DD/MM/YYYY') AS TRN_DATE ,a.PRTY_CODE,b.PRTY_NAME from YRN_IR_MST a, tx_vendor_mst b Where a.prty_code=b.prty_code (+) and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + TRN_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());

            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB == null || dtDepotTRN_SUB.Rows.Count == 0)
            {
                CreateDataTable();
            }
            else
            {
                dtDepotTRN_SUB.Rows.Clear();
            }

            int iRecordFound = GetdataByTRNNUMBer(TRN_NUMBER);

            BindGridFromDataTable();
            if (iRecordFound > 0)
            {
                IsUpdateCall = 1;
            }
            else
            {
                InitialisePage();
                ActivateUpdateMode();
            }
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transaction Number Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            txtTRNNUMBer.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();

            txtTRNNUMBer.Visible = false;
            ddlTRNNumber.Visible = true;

        }
        catch
        {
            throw;
        }
    }

    private void ActivateSaveMode()
    {
        try
        {
            txtTRNNUMBer.Text = string.Empty;
            ddlTRNNumber.SelectedIndex = -1;
            ddlTRNNumber.SelectedText = string.Empty;
            ddlTRNNumber.SelectedValue = string.Empty;
            ddlTRNNumber.Items.Clear();

            txtTRNNUMBer.Visible = true;
            ddlTRNNumber.Visible = false;

        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyBillAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Bill Amount TextBox..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnSubDetail_Click1(object sender, EventArgs e)
    {
        try
        {
            if (cmbPOITEM.Text != "")
            {
                string URL = "SewingThread_Recipt_Adjustment.aspx";
                URL = URL + "?ItemCodeId=" + txtICODE.Text;
                URL = URL + "&SHADE_CODE=" + txtShade.Text;
                URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                URL = URL + "&noofUnit_ID=" + txtNoOfUnit.ClientID;
                URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
                URL = URL + "&AmountId=" + txtAmount.ClientID;
                URL = URL + "&TRN_TYPE=" + TRN_TYPE;
                URL = URL + "&ChallanNo=" + txtTRNNUMBer.Text;
                URL = URL + "&REMQTY=" + lblMaxQTY.Text;
                URL = URL + "&UOM_OF_UNIT=" + txtBaseUOM.Text;

                txtNoOfUnit.ReadOnly = false;
                txtQTY.ReadOnly = false;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select PO Number");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Sub-Detail..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdMaterialItemReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row-DataBound Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtNoOfUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            calculateAmount();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, ""));
        }
    }

    protected void txtQTY_TextChanged(object sender, EventArgs e)
    {
        try
        {
            calculateAmount();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, ""));
        }
    }

    private void calculateAmount()
    {
        try
        {
            double NoofUnit = 0;
            double UnitWeight = 0;
            double Qty = 0;
            double Finalrate = 0;
            double Amount = 0;

            double.TryParse(txtNoOfUnit.Text, out NoofUnit);
            double.TryParse(txtUnitWeight.Text, out UnitWeight);
            double.TryParse(txtFinalRate.Text, out Finalrate);
            Qty = NoofUnit * UnitWeight;
            Amount = Finalrate * Qty;

            txtAmount.Text = Amount.ToString();
            txtQTY.Text = Qty.ToString();

            txtQTY.ReadOnly = true;
            txtNoOfUnit.ReadOnly = true;
            txtUnitWeight.ReadOnly = true;
            txtFinalRate.ReadOnly = true;
            txtAmount.ReadOnly = true;
        }
        catch
        {
            throw;
        }
    }

    private void CalculateTotalAmount()
    {
        try
        {
            double TOTALAMOUNT = 0;
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB != null && dtDepotTRN_SUB.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDepotTRN_SUB.Rows)
                {
                    double AMOUNT = double.Parse(dr["AMOUNT"].ToString());
                    TOTALAMOUNT += AMOUNT;
                }
            }
            txtTotalAmount.Text = TOTALAMOUNT.ToString();
        }
        catch
        {
            throw;
        }
    }
}