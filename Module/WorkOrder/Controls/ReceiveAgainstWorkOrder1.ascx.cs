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
using Obout.ComboBox;
using System.Collections.Generic;

public partial class Module_WorkOrder_Controls_ReceiveAgainstWorkOrder1 : System.Web.UI.UserControl
{

    private DataTable dtTRN_SUB = null;
    private DataTable dtItemReceipt = null;
    private DataTable dtInvoiceDicRateMST = null;
    private DataTable dtDicRate = null;

    private static DateTime StartDate;
    private static DateTime EndDate;
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private string TRN_TYPE = "RYS12";

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);
            ViewState["dtDetailTBL"] = null;
            Session["dtInvoiceDicRateMST"] = null;
            Session["dtDicRate"] = null;
            Session["dtTRN_SUB"] = null;
            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();

            StartDate = oUserLoginDetail.DT_STARTDATE;
            EndDate = Common.CommonFuction.GetYearEndDate(StartDate);

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindDropDown(DropDownList ddl)
    {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.Items.Clear();
            ddl.DataSource = dt;
            ddl.DataTextField = "MST_DESC";
            ddl.DataValueField = "MST_DESC";
            ddl.DataBind();
        }
        else
        {
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

        }
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

    }

    private void BindDepartment(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
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
            txtAmountInWords.Text = "Amount In Words... ";
            txtDepartment.Text = string.Empty;
            txtFormRefNo.Text = string.Empty;
            txtFormType.Text = string.Empty;
            txtGateEntryDate.Text = string.Empty;

            txtLRDate.Text = string.Empty;
            txtLRNo.Text = string.Empty;
            txtMRNDate.Text = string.Empty;
            txtTRNNUMBer.Text = string.Empty;

            lblPartyCode.Text = string.Empty;
            ddlPartyCode.SelectedIndex = -1;
            txtPartyCode.Text = string.Empty;
            txtPartyAddress1.Text = string.Empty;
            txtPartyAddress.Text = string.Empty;
            txtPartyBillAmount.Text = string.Empty;
            txtPartyBillDate.Text = string.Empty;
            txtPartyBillNo.Text = string.Empty;
            txtPartyChallanDate.Text = string.Empty;
            txtPartyChallanNo.Text = string.Empty;
            ddlGateEntryNo.SelectedIndex = -1;
            txtGateEntryNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtTransporterAddress.Text = string.Empty;
            lblTransporterCode.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtPartyBillAmount.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;
            txtNoOfPallet.Value = "0";
            ddlReceiptShift.SelectedIndex = 0;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();

            grdMaterialItemReceipt.DataSource = null;
            grdMaterialItemReceipt.DataBind();
            CalculateTotalAmount();

            lblMode.Text = "You are in Save Mode";

            txtTRNNUMBer.ReadOnly = true;

            txtDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;

            RefreshDetailRow();

            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;

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
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("TRNNUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_FAMILY", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("TRN_QTY_1", typeof(double));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("PO_RATE", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CODE", typeof(string));
            dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("REM_QTY", typeof(double));

            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("GROSS_WT", typeof(double));
            dtDetailTBL.Columns.Add("TARE_WT", typeof(double));
            dtDetailTBL.Columns.Add("CARTONS", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_PALLET", typeof(string));

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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();
            CalculateTotalAmount();
        }
        catch
        {
            throw;
        }
    }

    //protected void grdMaterialItemReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {

    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {

    //            LinkButton lnkunige = (LinkButton)e.Row.FindControl("lnkunige");
    //            int UNIQUE_ID = int.Parse(lnkunige.CommandArgument);

    //            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
    //            {
    //                DataView dv = new DataView(dtDetailTBL);

    //                dv.RowFilter = "UNIQUEID=" + UNIQUE_ID;
    //                if (dv.Count > 0)
    //                {

    //                    if (Session["dtTRN_SUB"] != null)
    //                    {
    //                        DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];

    //                        DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
    //                        dvYRNDRecieve_trn.RowFilter = "YARN_CODE='" + dv[0]["YARN_CODE"].ToString() + "' AND SHADE_CODE='" + dv[0]["SHADE_CODE"].ToString() + "' AND SHADE_CODE='" + dv[0]["SHADE_CODE"].ToString() + "' and LOT_NO='" + dv[0]["LOT_NO"].ToString() + "'  and GRADE='" + dv[0]["GRADE"].ToString() + "' ";
    //                        if (dvYRNDRecieve_trn.Count > 0)
    //                        {
    //                            GridView grdBOM = e.Row.FindControl("grdBOM") as GridView;
    //                            grdBOM.DataSource = dvYRNDRecieve_trn;
    //                            grdBOM.DataBind();
    //                        }

    //                    }

    //                }

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Material GridRow DataBound.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }

    //}
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

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);

            rvbill.MinimumValue = StartDate.ToShortDateString();
            rvbill.MaximumValue = EndDate.ToShortDateString();
            //rvDate.MinimumValue = StartDate.ToShortDateString();
            // rvDate.MaximumValue = EndDate.ToShortDateString();
            //  rvchalan.MinimumValue = StartDate.ToShortDateString();
            // rvchalan.MaximumValue = EndDate.ToShortDateString();
            // rvgate.MinimumValue = StartDate.ToShortDateString();
            // rvgate.MaximumValue = EndDate.ToShortDateString();
            rvlr.MinimumValue = StartDate.ToShortDateString();
            rvlr.MaximumValue = EndDate.ToShortDateString();

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
                bool result = CheckBillEntryExistence(txtPartyChallanNo.Text, txtPartyAddress.Text, lblPartyCode.Text, oUserLoginDetail.DT_STARTDATE.Year);
                if (!result)
                {
                    CommonFuction.ShowMessage("BillEntry No Already Exists Please Enter Another!!");
                    txtPartyBillNo.Text = string.Empty;
                }
                else
                {
                    saveMaterialReceipt();
                }
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page saving.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private bool CheckBillEntryExistence(string BillNo, string PartyName, string PartyCode, int Year)
    {
        try
        {
            bool Result = SaitexBL.Interface.Method.YRN_IR_MST.CheckBillEntryExistence(BillNo, PartyName, PartyCode, Year);
            return Result;
        }
        catch
        {
            throw;

        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            bool ReturnResult = false;

            int count = 0;
            msg = string.Empty;

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



            if (txtGateEntryNo.Text != "")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Gate Entry Details first.\r\n";
            }
            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
            }

            if (count == 4)
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
            txtTRNNUMBer.Text = "";
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding data.\r\nsee error log for detail"));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updation.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page refresh.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Yarn/SalesWork/Reports/YRN_ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing.\r\nsee error log for detail"));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page leaving.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private void deleteItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUEID"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {

                        string sPO_COMP_CODE = dr["PO_COMP_CODE"].ToString();
                        string sPO_BRANCH = dr["PO_BRANCH"].ToString();
                        string sPO_TYPE = dr["PO_TYPE"].ToString();
                        int iPO_NUMB = int.Parse(dr["PO_NUMB"].ToString());
                        string sYARN_CODE = dr["YARN_CODE"].ToString();
                        string SSHADE_CODE = dr["SHADE_CODE"].ToString();
                        deleteItemReceiptSUBTRNRow(sPO_COMP_CODE, sPO_BRANCH, sPO_TYPE, iPO_NUMB, sYARN_CODE, SSHADE_CODE);
                        dtDetailTBL.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUEID"] = iCount;
                }
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }

    private void deleteItemReceiptSUBTRNRow(string PO_COMP_CODE, string PO_BRANCH, string PO_TYPE, int PO_NUMB, string YARN_CODE, string SHADE_CODE)
    {
        try
        {
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            if (dtTRN_SUB.Rows.Count == 1)
            {
                dtTRN_SUB.Rows.Clear();
            }
            else
            {
                List<DataRow> rowsToDelete = new List<DataRow>();

                foreach (DataRow row in dtTRN_SUB.Rows)
                {

                    string sPO_COMP_CODE = row["PO_COMP_CODE"].ToString();
                    string sPO_BRANCH = row["PO_BRANCH"].ToString();
                    string sPO_TYPE = row["PO_TYPE"].ToString();
                    int iPO_NUMB = int.Parse(row["PO_NUMB"].ToString());
                    string sYARN_CODE = row["YARN_CODE"].ToString();
                    string sSHADE_CODE = row["SHADE_CODE"].ToString();

                    if (sPO_COMP_CODE == PO_COMP_CODE && sPO_BRANCH == PO_BRANCH && sPO_TYPE == PO_TYPE && iPO_NUMB == PO_NUMB && sYARN_CODE == YARN_CODE && SHADE_CODE == sSHADE_CODE)
                    {
                        rowsToDelete.Add(row);
                    }
                }
                foreach (DataRow row in rowsToDelete)
                {
                    dtTRN_SUB.Rows.Remove(row);
                }
                dtTRN_SUB.AcceptChanges();
                int iCount = 0;
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
                dtTRN_SUB.AcceptChanges();
            }
            Session["dtTRN_SUB"] = dtTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int PONumb, int UNIQUEID)
    {
        bool Result = false;
        try
        {
            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                    Label txtPONum = (Label)grdRow.FindControl("txtPONum");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (int.Parse(txtPONum.Text.Trim()) == PONumb && txtICODE.Text.Trim() == ItemCode && UNIQUEID != iUNIQUEID)
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
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oYRN_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oYRN_IR_MST.GATE_DATE = dt;

            oYRN_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());

            //  ------------- Stock Party code-------------//
            oYRN_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.Text.Trim());
            //  ------------- Jober = CONSIGNEE-------------//
            oYRN_IR_MST.CONSIGNEE_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());

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

            oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            int TRN_NUMB = 0;
            oYRN_IR_MST.BILL_TYPE = "JWP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;


            double totalAmt = 0;
            double.TryParse(txtTotalAmount.Text, out totalAmt);
            oYRN_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(txtPartyBillAmount.Text, out finalAmt);
            oYRN_IR_MST.FINAL_AMOUNT = finalAmt;

            oYRN_IR_MST.BILL_NUMB = txtPartyBillNo.Text;

            int BILLNUMBER = 0;
            int.TryParse(txtPartyBillNo.Text, out BILLNUMBER);
            oYRN_IR_MST.BILL_NUMBER = BILLNUMBER;

            dt = System.DateTime.Now.Date;
            bool Is_Bill_Date = false;
            Is_Bill_Date = DateTime.TryParse(txtPartyBillDate.Text.Trim(), out dt);
            htReceive.Add("BILL_DATE", Is_Bill_Date);
            oYRN_IR_MST.BILL_DATE = dt;

            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (Session["dtInvoiceDicRateMST"] != null)
                dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];

            if (Session["dtDicRate"] != null)
                dtDicRate = (DataTable)Session["dtDicRate"];

            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }

            if (Session["dtItemReceipt"] != null)
            {
                dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            }

            // Show message to the user that Total Amount OR Final Amount is not same as party Bill Amount.by Arun Sharma

            double dblPartyBillAmt = 0;     // For Party Bill Amount
            double dblTotalAmount = 0;       // Total Amount
            double dblFinalAmount = 0;  // For Final Amount

            double.TryParse(txtTotalPartyAmt.Text.Trim(), out dblPartyBillAmt);
            double.TryParse(txtTotalAmount.Text.Trim(), out dblTotalAmount);
            double.TryParse(txtPartyBillAmount.Text.Trim(), out dblFinalAmount);

            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            oYRN_IR_MST.PARTY_BILL_AMOUNT = dblPartyBillAmt;

            if (dblPartyBillAmt != dblTotalAmount && dblPartyBillAmt != dblFinalAmount)
            {
                string Msg = string.Empty;
                Msg += "Either Total Amount or Final Amount is not matched with Party Bill Amount.";

            }

            dtDetailTBL.Columns.Add("JOBER", typeof(string));
            for (int i = 0; i < dtDetailTBL.Rows.Count; i++)
            {
                dtDetailTBL.Rows[i]["JOBER"] = "NA";


            }

            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Insert1(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST, dtItemReceipt);
            if (result)
            {
                InitialisePage();
                string Msg = string.Empty;
                CommonFuction.ShowMessage(Msg + "MRN Number : " + TRN_NUMB + " Saved successfully!!");
            }
            else
            {
                CommonFuction.ShowMessage("Data Saving Failed..");
            }

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
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oYRN_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oYRN_IR_MST.GATE_DATE = dt;

            oYRN_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
            oYRN_IR_MST.GATE_OUT_NUMB = "";
            oYRN_IR_MST.GATE_PASS_TYPE = "";
            oYRN_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oYRN_IR_MST.LR_DATE = dt;

            oYRN_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oYRN_IR_MST.PRTY_CH_DATE = dt;

            oYRN_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());

            //  ------------- Stock Party code-------------//
            oYRN_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.Text.Trim());
            //  ------------- Jober = CONSIGNEE-------------//
            oYRN_IR_MST.CONSIGNEE_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());

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

            oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            int TRN_NUMB = 0;
            oYRN_IR_MST.BILL_TYPE = "JWP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;


            double totalAmt = 0;
            double.TryParse(txtTotalAmount.Text, out totalAmt);
            oYRN_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(txtPartyBillAmount.Text, out finalAmt);
            oYRN_IR_MST.FINAL_AMOUNT = finalAmt;

            oYRN_IR_MST.BILL_NUMB = txtPartyBillNo.Text;

            int BILLNUMBER = 0;
            int.TryParse(txtPartyBillNo.Text, out BILLNUMBER);
            oYRN_IR_MST.BILL_NUMBER = BILLNUMBER;

            dt = System.DateTime.Now.Date;
            bool Is_Bill_Date = false;
            Is_Bill_Date = DateTime.TryParse(txtPartyBillDate.Text.Trim(), out dt);
            htReceive.Add("BILL_DATE", Is_Bill_Date);
            oYRN_IR_MST.BILL_DATE = dt;

            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (Session["dtInvoiceDicRateMST"] != null)
                dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];

            if (Session["dtDicRate"] != null)
                dtDicRate = (DataTable)Session["dtDicRate"];

            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }


            if (Session["dtItemReceipt"] != null)
            {
                dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            }

            // Show message to the user that Total Amount OR Final Amount is not same as party Bill Amount.by Arun Sharma

            double dblPartyBillAmt = 0;     // For Party Bill Amount
            double dblTotalAmount = 0;       // Total Amount
            double dblFinalAmount = 0;  // For Final Amount

            double.TryParse(txtTotalPartyAmt.Text.Trim(), out dblPartyBillAmt);
            double.TryParse(txtTotalAmount.Text.Trim(), out dblTotalAmount);
            double.TryParse(txtPartyBillAmount.Text.Trim(), out dblFinalAmount);

            oYRN_IR_MST.SPINNER_CODE = string.Empty;
            oYRN_IR_MST.PARTY_BILL_AMOUNT = dblPartyBillAmt;

            if (dblPartyBillAmt != dblTotalAmount && dblPartyBillAmt != dblFinalAmount)
            {
                string msg = string.Empty;
                msg += "Either Total Amount or Final Amount is not matched with Party Bill Amount.";

            }


            for (int i = 0; i < dtDetailTBL.Rows.Count; i++)
            {
                if (dtDetailTBL.Rows[i]["JOBER"].ToString() == string.Empty)
                { dtDetailTBL.Rows[i]["JOBER"] = "NA"; }


            }
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Update1(oYRN_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST, dtItemReceipt);
            if (result)
            {
                InitialisePage();
                string msg = string.Empty;
                CommonFuction.ShowMessage(msg + "MRN Number : " + oYRN_IR_MST.TRN_NUMB + " Updated successfully!!");
            }
            else
            {
                CommonFuction.ShowMessage("Data Updateion Failed..");
            }
        }
        catch
        {
            throw;
        }
    }

    private int GetdataByTRNNUMBer(int TRNNUMBer)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();
                txtFormRefNo.Text = dt.Rows[0]["FORM_NUMB"].ToString().Trim();
                txtFormType.Text = dt.Rows[0]["FORM_TYPE"].ToString().Trim();
                lblPO_YEAR.Text = dt.Rows[0]["YEAR"].ToString().Trim();
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["STORE"].ToString().Trim()));
                DateTime dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["GATE_DATE"].ToString().Trim(), out dd))
                    txtGateEntryDate.Text = dd.ToShortDateString();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["LR_DATE"].ToString().Trim(), out dd))
                    txtLRDate.Text = dd.ToShortDateString();

                txtLRNo.Text = dt.Rows[0]["LR_NUMB"].ToString().Trim();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["TRN_DATE"].ToString().Trim(), out dd))
                    txtMRNDate.Text = dd.ToShortDateString();

                txtTRNNUMBer.Text = dt.Rows[0]["TRN_NUMB"].ToString().Trim();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim(), out dd))
                    txtPartyChallanDate.Text = dd.ToShortDateString();

                txtPartyChallanNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                //-----------------Party_stock-----------//
                txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress1.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();

                //-----------------Jober---------------//
                lblPartyCode.Text = dt.Rows[0]["CONSIGNEE_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["C_PRTY_NAME"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TAddress"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();

                ddlGateEntryNo.SelectedText = dt.Rows[0]["GATE_NUMB"].ToString().Trim();
                txtGateEntryNo.Text = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["BILL_DATE"].ToString().Trim(), out dd))
                    txtPartyBillDate.Text = dd.ToShortDateString();

                txtPartyBillNo.Text = dt.Rows[0]["BILL_NUMB"].ToString().Trim();

                txtTotalAmount.Text = dt.Rows[0]["TOTAL_AMOUNT"].ToString().Trim();
                txtPartyBillAmount.Text = dt.Rows[0]["FINAL_AMOUNT"].ToString().Trim();
            }
            if (iRecordFound == 1)
            {

                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetTRN_DataByTRN_NUMB1(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtDetailTBL.Rows.Count > 0)
                {
                    ViewState["dtDetailTBL"] = dtDetailTBL;
                    MapDataTable();
                    BindGridFromDataTable();
                    // code to get dis taxes for master entry ( Other charges)
                    //DataTable dtDisTaxMstTemp = SaitexBL.Interface.Method.YRN_IR_MST.GetDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    //if (dtDisTaxMstTemp.Rows.Count > 0)
                    //{
                    //    MapDataTableDisTaxMST(dtDisTaxMstTemp);
                    //}

                    //// code to get dis taxes for transaction entry 
                    //DataTable dtDisTaxTrnTemp = SaitexBL.Interface.Method.YRN_IR_MST.GetTrnDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    //if (dtDisTaxTrnTemp.Rows.Count > 0)
                    //{
                    //    MapDataTableDisTaxTrn(dtDisTaxTrnTemp);
                    //}

                    //// code to get sub tran detail for transaction entry 
                    //dtTRN_SUB = SaitexBL.Interface.Method.YRN_IR_MST.GetSubTrnDetailByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    //if (dtTRN_SUB.Rows.Count > 0)
                    //{
                    //    MapDataTableSubTRNDetail(dtTRN_SUB);
                    //}
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Material Detail.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();
                    BindGridFromDataTable();
                    txtTRNNUMBer.Text = "";
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
            return iRecordFound;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            return iRecordFound;
        }
    }

    #region code to map dis taxes datatable for master data

    private void MapDataTableDisTaxMST(DataTable dt)
    {
        try
        {
            if (Session["dtInvoiceDicRateMST"] == null)
            {
                CreateDataTableDisTaxMST();
            }
            dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];
            double StartFinalAmount = 0;
            double.TryParse(txtTotalAmount.Text, out StartFinalAmount);
            double dFinalRate = StartFinalAmount;
            if (dt != null && dt.Rows.Count > 0)
            {
                int iloop = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    iloop += 1;
                    DataRow drNew = dtInvoiceDicRateMST.NewRow();

                    drNew["Uniqueid"] = iloop;
                    drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                    drNew["Rate"] = dr["RATE"];
                    drNew["COMPO_SL"] = dr["COMPO_SL"];
                    drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                    drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                    double dAmount = 0;

                    double cAmount = 0;
                    double rate = double.Parse(dr["Rate"].ToString());
                    if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                    {
                        cAmount = (StartFinalAmount * rate) / 100;
                    }
                    else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                    {
                        cAmount = (dFinalRate * rate) / 100;
                    }
                    else
                    {
                        DataView dvv = new DataView(dtInvoiceDicRateMST);
                        dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                        if (dvv.Count > 0)
                        {
                            double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                        }
                        cAmount = (dAmount * rate) / 100;
                    }

                    if (dr["COMPO_TYPE"].ToString().Equals("D"))
                    {
                        dFinalRate = dFinalRate - cAmount;
                    }
                    else
                    {
                        dFinalRate = dFinalRate + cAmount;
                    }
                    drNew["Amount"] = cAmount;
                    dtInvoiceDicRateMST.Rows.Add(drNew);
                }
            }
            txtPartyBillAmount.Text = dFinalRate.ToString();
            Session["dtInvoiceDicRateMST"] = dtInvoiceDicRateMST;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTableDisTaxMST()
    {
        try
        {
            DataTable dtInvoiceDicRateMST = new DataTable();
            dtInvoiceDicRateMST.Columns.Add("Uniqueid", typeof(int));
            dtInvoiceDicRateMST.Columns.Add("COMPO_CODE", typeof(string));
            dtInvoiceDicRateMST.Columns.Add("Rate", typeof(double));
            dtInvoiceDicRateMST.Columns.Add("COMPO_SL", typeof(int));
            dtInvoiceDicRateMST.Columns.Add("COMPO_TYPE", typeof(string));
            dtInvoiceDicRateMST.Columns.Add("Amount", typeof(double));
            dtInvoiceDicRateMST.Columns.Add("BASE_COMPO_CODE", typeof(string));
            Session["dtInvoiceDicRateMST"] = dtInvoiceDicRateMST;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region code to map dis taxes datatable for transaction data

    private void MapDataTableDisTaxTrn(DataTable dt)
    {
        try
        {
            if (Session["dtDicRate"] == null)
            {
                CreateDataTableDisTaxTrn();
            }
            dtDicRate = (DataTable)Session["dtDicRate"];
            double StartFinalAmount = 0;
            double.TryParse(txtBasicRate.Text, out StartFinalAmount);
            double dFinalRate = StartFinalAmount;
            if (dt != null && dt.Rows.Count > 0)
            {
                int iloop = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    iloop += 1;
                    DataRow drNew = dtDicRate.NewRow();

                    drNew["Uniqueid"] = iloop;
                    drNew["PO_COMP_CODE"] = dr["PO_COMP_CODE"];
                    drNew["PO_BRANCH"] = dr["PO_BRANCH"];
                    drNew["PO_TYPE"] = dr["PO_TYPE"];
                    drNew["PO_NUMB"] = dr["PO_NUMB"];
                    drNew["PO_YEAR"] = dr["PO_YEAR"];
                    drNew["YARN_CODE"] = dr["YARN_CODE"];
                    drNew["SHADE_CODE"] = dr["SHADE_CODE"];
                    drNew["SHADE_FAMILY"] = dr["SHADE_FAMILY"];
                    drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                    drNew["Rate"] = dr["RATE"];
                    drNew["COMPO_SL"] = dr["COMPO_SL"];
                    drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                    drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                    drNew["IS_PO"] = dr["IS_PO"];
                    double dAmount = 0;

                    double cAmount = 0;
                    double rate = double.Parse(dr["Rate"].ToString());
                    if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                    {
                        cAmount = (StartFinalAmount * rate) / 100;
                    }
                    else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                    {
                        cAmount = (dFinalRate * rate) / 100;
                    }
                    else
                    {
                        DataView dvv = new DataView(dtDicRate);
                        dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                        if (dvv.Count > 0)
                        {
                            double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                        }
                        cAmount = (dAmount * rate) / 100;
                    }

                    if (dr["COMPO_TYPE"].ToString().Equals("D"))
                    {
                        dFinalRate = dFinalRate - cAmount;
                    }
                    else
                    {
                        dFinalRate = dFinalRate + cAmount;
                    }
                    drNew["Amount"] = cAmount;
                    dtDicRate.Rows.Add(drNew);
                }
            }

            Session["dtDicRate"] = dtDicRate;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTableDisTaxTrn()
    {
        try
        {
            dtDicRate = new DataTable();
            dtDicRate.Columns.Add("Uniqueid", typeof(int));
            dtDicRate.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDicRate.Columns.Add("PO_BRANCH", typeof(string));
            dtDicRate.Columns.Add("PO_TYPE", typeof(string));
            dtDicRate.Columns.Add("PO_NUMB", typeof(int));
            dtDicRate.Columns.Add("PO_YEAR", typeof(int));
            dtDicRate.Columns.Add("YARN_CODE", typeof(string));
            dtDicRate.Columns.Add("SHADE_CODE", typeof(string));
            dtDicRate.Columns.Add("SHADE_FAMILY", typeof(string));
            dtDicRate.Columns.Add("COMPO_CODE", typeof(string));
            dtDicRate.Columns.Add("Rate", typeof(double));
            dtDicRate.Columns.Add("COMPO_SL", typeof(int));
            dtDicRate.Columns.Add("COMPO_TYPE", typeof(string));
            dtDicRate.Columns.Add("Amount", typeof(double));
            dtDicRate.Columns.Add("BASE_COMPO_CODE", typeof(string));
            dtDicRate.Columns.Add("IS_PO", typeof(string));
            Session["dtDicRate"] = dtDicRate;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region code to map Sub Tran datatable for transaction data

    private void MapDataTableSubTRNDetail(DataTable dtTRN_SUB)
    {
        try
        {

            if (dtTRN_SUB != null && dtTRN_SUB.Rows.Count > 0)
            {
                if (!dtTRN_SUB.Columns.Contains("UNIQUE_ID"))
                {
                    dtTRN_SUB.Columns.Add("UNIQUE_ID");
                }
                if (!dtTRN_SUB.Columns.Contains("PI_NO"))
                {
                    dtTRN_SUB.Columns.Add("PI_NO");
                }

                int iloop = 0;
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    iloop += 1;

                    dr["UNIQUE_ID"] = iloop;
                    dr["PI_NO"] = "NA";
                }
            }
            dtTRN_SUB.AcceptChanges();

            Session["dtTRN_SUB"] = dtTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTableSubTRNDetail()
    {
        try
        {
            dtTRN_SUB = new DataTable();
            dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_SUB.Columns.Add("YARN_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("SHADE_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtTRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtTRN_SUB.Columns.Add("GRADE", typeof(string));
            dtTRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtTRN_SUB.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtTRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtTRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtTRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtTRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("PI_NO", typeof(string));

            Session["dtTRN_SUB"] = dtTRN_SUB;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    private void MapDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));

            if (!dtDetailTBL.Columns.Contains("MAC_CODE"))
                dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));

            if (!dtDetailTBL.Columns.Contains("COST_CODE"))
                dtDetailTBL.Columns.Add("COST_CODE", typeof(string));

            if (!dtDetailTBL.Columns.Contains("TRN_QTY_1"))
                dtDetailTBL.Columns.Add("TRN_QTY_1", typeof(double));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["MAC_CODE"] = string.Empty;
                dtDetailTBL.Rows[iLoop]["COST_CODE"] = string.Empty;
                dtDetailTBL.Rows[iLoop]["TRN_QTY_1"] = dtDetailTBL.Rows[iLoop]["TRN_QTY"];
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }

    private void ResetDetailOnPartySelection()
    {
        try
        {
            RefreshDetailRow();

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            dtDetailTBL.Rows.Clear();
            ViewState["dtDetailTBL"] = dtDetailTBL;

            BindGridFromDataTable();

        }
        catch
        {
            throw;
        }
    }

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();
            //if (Session["dtTRN_SUB"] != null)


            if (txtPONumb.Text != "" && txtICODE.Text != "" && txtShadeCode.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "")
            //&& Session["dtTRN_SUB"] != null
            {
                int UNIQUEID = 0;
                if (ViewState["UNIQUEID"] != null)
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                bool bb = SearchItemCodeInGrid(txtICODE.Text.Trim(), int.Parse(txtPONumb.Text.Trim()), UNIQUEID);
                if (!bb)
                {
                    double Qty = 0;
                    double.TryParse(txtQTY.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {

                        double MaxQty = 0;
                        double.TryParse(lblMaxQTY.Text.Trim(), out MaxQty);

                        // CODE MODIFIED FOR A SHORT SPAN OF TIME..
                        // UNCOMMENT THE ELSE CONDITION FOR BLOCKING THE EXCEED QTY.
                        if (Qty > MaxQty)
                        {
                            //  txtQTY.Text = MaxQty.ToString();
                            CommonFuction.ShowMessage(@"Entered Quantity is larger than po Quantity.\r\nYou can receive maximum " + MaxQty + " quantity for this item of selected po.");
                            txtQTY.Focus();
                        }
                        if (UNIQUEID > 0)
                        {
                            DataView dv = new DataView(dtDetailTBL);
                            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                            if (dv.Count > 0)
                            {
                                dv[0]["PO_NUMB"] =txtPONumb.Text.ToString();
                                dv[0]["PO_TYPE"] = lblPO_TYPE.Text;
                                dv[0]["PO_COMP_CODE"] = lblPO_COMP.Text;
                                dv[0]["PO_BRANCH"] = lblPO_BRANCH.Text;
                                dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dv[0]["YARN_CODE"] = txtICODE.Text.Trim();
                                dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                dv[0]["SHADE_CODE"] = txtShadeCode.Text.Trim();
                                dv[0]["SHADE_FAMILY"] = txtShadeCode.Text.Trim();
                                dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dv[0]["UOM"] = txtUNIT.Text.Trim();
                                dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                //dv[0]["PO_RATE"] = double.Parse(txtpoRate.Text.Trim());
                                dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dv[0]["AMOUNT"] = double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim());
                                dv[0]["COST_CENTER_CODE"] = string.Empty;
                                dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                dv[0]["QCFLAG"] = "Yes";

                                DateTime dd = System.DateTime.Now;
                                dv[0]["DATE_OF_MFG"] = dd;
                                
                                double NoOfUnit = 0;
                                //double.TryParse(txtnoofunit.Text, out NoOfUnit);
                                dv[0]["NO_OF_UNIT"] = txtnoofunit.Text;
                                dv[0]["UOM_OF_UNIT"] = txtUNIT.Text.Trim();
                                double weightofunit = 0;
                                //double.TryParse(txtWeightofUnit.Text, out weightofunit);
                                dv[0]["WEIGHT_OF_UNIT"] = weightofunit;
                                double REM_QTY = 0;
                                double.TryParse(lblMaxQTY.Text, out REM_QTY);
                                dv[0]["REM_QTY"] = REM_QTY;
                                dv[0]["PI_NO"] = txtChallanNo.Text.ToString() ; //---- as Issue Challan No ----//
                                dv[0]["LOT_NO"] = txtLotNo.SelectedValue;
                                double grossWt = 0;
                                double tareWt = 0;
                                double cartons = 0;
                                double.TryParse(txtGrossWt.Value, out grossWt);
                                double.TryParse(txtTareWt.Value, out tareWt);
                                double.TryParse(txtCartons.Value, out cartons);
                                dv[0]["GRADE"] = txtGrade.SelectedValue;
                                //dv[0]["GROSS_WT"] = grossWt;
                                dv[0]["GROSS_WT"] = lblMaxQTY.Text.ToString();
                                dv[0]["TARE_WT"] = tareWt;
                                dv[0]["CARTONS"] = double.Parse(txtCarton.Text.ToString());
                                dv[0]["NO_OF_PALLET"] = txtNoOfPallet.Value;
                                int poyear = 0;
                                int.TryParse(lblPO_YEAR.Text, out poyear);
                                dv[0]["PO_YEAR"] = poyear;
                                dtDetailTBL.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtDetailTBL.NewRow();
                            dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                            dr["PO_NUMB"] = txtPONumb.Text.ToString();
                            dr["PO_TYPE"] = lblPO_TYPE.Text;
                            dr["PO_COMP_CODE"] = lblPO_COMP.Text;
                            dr["PO_BRANCH"] = lblPO_BRANCH.Text;
                            dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["YARN_CODE"] = txtICODE.Text.Trim();
                            dr["YARN_DESC"] = txtDESC.Text.Trim();
                            dr["SHADE_CODE"] = txtShadeCode.Text.Trim();
                            dr["SHADE_FAMILY"] = txtShadeCode.Text.Trim();
                            dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                            dr["TRN_QTY_1"] = 0;
                            dr["UOM"] = txtUNIT.Text.Trim();
                            dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                            //dr["PO_RATE"] = double.Parse(txtpoRate.Text.Trim());
                            dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                            dr["AMOUNT"] = double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim());
                            dr["COST_CENTER_CODE"] = string.Empty; //txtCostCode.Text.Trim();
                            dr["MAC_CODE"] = string.Empty;
                            dr["REMARKS"] = txtDetRemarks.Text.Trim();
                            dr["QCFLAG"] = "Yes";

                            DateTime dd = System.DateTime.Now;
                            dr["DATE_OF_MFG"] = dd;
                            dr["PI_NO"] = txtChallanNo.Text.ToString();  //---- as Issue Challan No ----//
                            double NoOfUnit = 0;
                            //double.TryParse(txtnoofunit.Text, out NoOfUnit);
                            dr["NO_OF_UNIT"] = txtnoofunit.Text;
                            dr["UOM_OF_UNIT"] = txtUNIT.Text.Trim();
                            double weightofunit = 0;
                            //double.TryParse(txtWeightofUnit.Text, out weightofunit);
                            dr["WEIGHT_OF_UNIT"] = weightofunit;
                            double REM_QTY = 0;
                            double.TryParse(lblMaxQTY.Text, out REM_QTY);
                            dr["REM_QTY"] = REM_QTY;
                            dr["LOT_NO"] = txtLotNo.SelectedValue;
                            double grossWt = 0;
                            double tareWt = 0;
                            double cartons = 0;
                            double.TryParse(txtGrossWt.Value, out grossWt);
                            double.TryParse(txtTareWt.Value, out tareWt);
                            double.TryParse(txtCartons.Value, out cartons);
                            dr["GRADE"] = txtGrade.SelectedValue;
                            //dr["GROSS_WT"] = grossWt;
                            dr["GROSS_WT"] = lblMaxQTY.Text.ToString() ;

                            dr["TARE_WT"] = tareWt;
                            dr["CARTONS"] = double.Parse(txtCarton.Text.ToString());
                            dr["NO_OF_PALLET"] = txtNoOfPallet.Value;
                            int poyear = 0;
                            int.TryParse(lblPO_YEAR.Text, out poyear);
                            dr["PO_YEAR"] = poyear;
                            dtDetailTBL.Rows.Add(dr);
                        }
                        RefreshDetailRow();
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
            else
            {

                Common.CommonFuction.ShowMessage("Please fill the Cortoons and Other details");
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
            BindGridFromDataTable();

        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding detail.\r\nsee error log for detail"));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in cancel row editing.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOTData(e.Text.ToUpper(), e.ItemsOffset, "GREY_LOT_NO");
            txtLotNo.Items.Clear();
            txtLotNo.DataSource = data;
            txtLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetLOTCount(e.Text, "GREY_LOT_NO");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Merge No in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtGrade_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "GRADE");
            txtGrade.Items.Clear();
            txtGrade.DataSource = data;
            txtGrade.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetMOMCount(e.Text, "GRADE");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grade selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetMOMData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND (MST_CODE,MST_DESC) NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by MST_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetMOMCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    private DataTable GetLOTData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND MST_CODE NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))        AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by MST_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetLOTCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))        AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetWOData(e.Text.ToUpper(), e.ItemsOffset);
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "WO_NUMB";
            cmbPOITEM.DataValueField = "WO_TRN_DATA";
            cmbPOITEM.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetWOCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po item selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetWOData(string text, int startOffset)
    {
        try
        {
            DataTable dt = new DataTable();
            if (lblPartyCode.Text != "")
            {
                string commandText = "SELECT * FROM (SELECT * FROM (SELECT DISTINCT ( PT.WO_TYPE|| '@'|| PT.WO_NUMB|| '@'|| PT.ARTICLE_CODE|| '@'|| ARTICLE_DESC|| '@'|| pt.UOM|| '@'|| pt.BASIC_RATE|| '@'|| pt.FINAL_RATE|| '@'|| pt.comp_code|| '@'|| pt.branch_code || '@'|| pt.SHADE_CODE ||'@'||pt.YEAR|| '@'|| PT.LOT_NO|| '@'|| PT.GRADE|| '@'|| PT.PRTY_CODE|| '@'  || T.TRN_NUMB  || '@' || T.TRN_QTY) WO_TRN_DATA, pt.WO_NUMB, PT.ARTICLE_CODE,pt.SHADE_CODE, pt.UOM, PT.ARTICLE_DESC,  pt.QTY, NVL (QTY_REC, 0) QTY_REC, NVL (S.QTY_ISS, 0) - NVL (S.QTY_RTN, 0) AS QTY_REM ,T.TRN_NUMB,T.TRN_QTY,PT.LOT_NO FROM V_OD_WO_TRN pt, YRN_IR_TRN T, OD_WO_TRN_SUB S WHERE (NVL (PT.QTY, 0) - NVL (PT.QTY_REC, 0)) > 0 AND PT.WO_TYPE IN ('WUM') AND DELIVERY_LOCATION ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND JOBER_PARTY = '" + lblPartyCode.Text + "' AND T.PO_NUMB = PT.WO_NUMB AND T.TRN_TYPE = 'IYS11'  AND PT.COMP_CODE = S.COMP_CODE  AND PT.BRANCH_CODE = S.BRANCH_CODE  AND PT.YEAR = S.YEAR  AND PT.WO_NUMB = S.WO_NUMB  AND PT.WO_TYPE = S.WO_TYPE  AND NVL (S.QTY_ISS, 0) - NVL (S.QTY_RTN, 0) > 0 ORDER BY WO_NUMB) WHERE WO_NUMB LIKE :SearchQuery OR ARTICLE_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";

                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND WO_TRN_DATA NOT IN ( SELECT * FROM (SELECT * FROM (SELECT DISTINCT ( PT.WO_TYPE|| '@'|| PT.WO_NUMB|| '@'|| PT.ARTICLE_CODE|| '@'|| ARTICLE_DESC|| '@'|| pt.UOM|| '@'|| pt.BASIC_RATE|| '@'|| pt.FINAL_RATE|| '@'|| pt.comp_code|| '@'|| pt.branch_code || '@'|| pt.SHADE_CODE ||'@'||pt.YEAR|| '@'|| PT.LOT_NO|| '@'|| PT.GRADE|| '@'|| PT.PRTY_CODE|| '@'  || T.TRN_NUMB  || '@' || T.TRN_QTY ) WO_TRN_DATA, pt.WO_NUMB, PT.ARTICLE_CODE,pt.SHADE_CODE, pt.UOM, PT.ARTICLE_DESC,  pt.QTY, NVL (QTY_REC, 0) QTY_REC,NVL (S.QTY_ISS, 0) - NVL (S.QTY_RTN, 0) AS QTY_REM ,T.TRN_NUMB,T.TRN_QTY,PT.LOT_NO FROM V_OD_WO_TRN pt, YRN_IR_TRN T, OD_WO_TRN_SUB S WHERE (NVL (PT.QTY, 0) - NVL (PT.QTY_REC, 0)) > 0 AND PT.WO_TYPE IN ('WUM') AND DELIVERY_LOCATION ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND JOBER_PARTY = '" + lblPartyCode.Text + "' AND T.PO_NUMB = PT.WO_NUMB AND T.TRN_TYPE = 'IYS11' AND PT.COMP_CODE = S.COMP_CODE  AND PT.BRANCH_CODE = S.BRANCH_CODE    AND PT.YEAR = S.YEAR  AND PT.WO_NUMB = S.WO_NUMB  AND PT.WO_TYPE = S.WO_TYPE  AND NVL (S.QTY_ISS, 0) - NVL (S.QTY_RTN, 0) > 0 ORDER BY WO_NUMB) WHERE WO_NUMB LIKE :SearchQuery OR ARTICLE_CODE LIKE :SearchQuery) WHERE ROWNUM<='" + startOffset + "')";
                }

                string SortExpression = " ORDER BY WO_NUMB";

                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", text + "%", "");
            }
            else
            {
                CommonFuction.ShowMessage("Please select Party First");
            }

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetWOCount(string text)
    {

        string CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT ( PT.WO_TYPE|| '@'|| PT.WO_NUMB|| '@'|| PT.ARTICLE_CODE|| '@'|| ARTICLE_DESC|| '@'|| pt.UOM|| '@'|| pt.BASIC_RATE|| '@'|| pt.FINAL_RATE|| '@'|| pt.comp_code|| '@'|| pt.branch_code||'@'||pt.SHADE_CODE ||'@'||pt.YEAR) WO_TRN_DATA, pt.WO_NUMB, PT.ARTICLE_CODE,pt.SHADE_CODE, pt.UOM, PT.ARTICLE_DESC,  pt.QTY, NVL (QTY_REC, 0) QTY_REC, NVL (S.QTY_ISS, 0) - NVL (S.QTY_RTN, 0) AS QTY_REM,PT.LOT_NO FROM V_OD_WO_TRN pt, OD_WO_TRN_SUB S WHERE (NVL (QTY, 0) - NVL (QTY_REC, 0)) > 0 AND PT.WO_TYPE IN ('WUM') AND DELIVERY_LOCATION ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND JOBER_PARTY = '" + lblPartyCode.Text + "'  AND PT.COMP_CODE = S.COMP_CODE  AND PT.BRANCH_CODE = S.BRANCH_CODE  AND PT.YEAR = S.YEAR  AND PT.WO_NUMB = S.WO_NUMB  AND PT.WO_TYPE = S.WO_TYPE   AND NVL (S.QTY_ISS, 0) - NVL (S.QTY_RTN, 0) > 0 ORDER BY WO_NUMB) WHERE WO_NUMB LIKE :SearchQuery OR ARTICLE_CODE LIKE :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {

            string cString = cmbPOITEM.SelectedValue.ToString().Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string WO_Type = arrString[0].ToString();
            int WONumb = int.Parse(arrString[1].ToString());
            string ArticleCode = arrString[2].ToString();
            string Art_desc = arrString[3].ToString();
            string UOM = arrString[4].ToString();
            double basic_rate = double.Parse(arrString[5].ToString());
            double final_rate = double.Parse(arrString[6].ToString());
            string wo_comp = arrString[7].ToString();
            string wo_branch = arrString[8].ToString();
            string ShadeCode = arrString[9].ToString();
            lblPO_YEAR.Text = arrString[10].ToString();
            txtChallanNo.Text = arrString[14].ToString();
            lblMaxQTY.Text = arrString[15].ToString();
            txtShadeFamily.Text = "GREY";
            string SHADE_FAMILY = txtShadeFamily.Text;
            GetDataForDetailWorkOrder(WO_Type, WONumb, ArticleCode, ShadeCode, SHADE_FAMILY);

            ///--------------Auto Bind Lot No------------///

            //txtLotNo.SelectedText = arrString[11].ToString();
            //txtLotNo.SelectedValue = arrString[11].ToString();
            //ComboBoxItem item1 = new ComboBoxItem(arrString[11].ToString());
            //string CommandText = "SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GREY_LOT_NO'))         AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery) ";
            //string whereClause = string.Empty;
            //string SortExpression = " order by MST_CODE";
            //string SearchQuery = "%";
            //DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            //txtLotNo.DataSource = dt;
            //txtLotNo.DataTextField = "MST_CODE";
            //txtLotNo.DataValueField = "MST_CODE";
            //txtLotNo.DataBind();
            //int x = -1;
            //foreach (ComboBoxItem it in txtLotNo.Items)
            //{
            //    if (it.Text == item1.Text)
            //    {
            //        txtLotNo.SelectedIndex = x;
            //        txtLotNo.SelectedText = it.Text;
            //        txtLotNo.SelectedValue = it.Value;
            //        break;
            //    }
            //    x++;
            //}
            ///---------------Auto Grade Bind--------------/// 
            txtGrade.SelectedText = arrString[12].ToString();
            txtGrade.SelectedValue = arrString[12].ToString();
            ComboBoxItem item2 = new ComboBoxItem(arrString[12].ToString());

            string CommandText1 = "SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GRADE'))         AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery) ";
            string whereClause1 = string.Empty;
            string SortExpression1 = " order by MST_CODE";
            string SearchQuery1 = "%";
            DataTable dt1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, whereClause1, SortExpression1, "", SearchQuery1, "");
            txtGrade.DataSource = dt1;
            txtGrade.DataTextField = "MST_CODE";
            txtGrade.DataValueField = "MST_CODE";
            txtGrade.DataBind();
            int z = -1;
            foreach (ComboBoxItem it in txtGrade.Items)
            {
                if (it.Text == item2.Text)
                {
                    txtGrade.SelectedIndex = z;
                    txtGrade.SelectedText = it.Text;
                    txtGrade.SelectedValue = it.Value;
                    break;
                }
                z++;
            }
            ///--------------Auto Party Bind-------------//
            ddlPartyCode.SelectedText = arrString[13].ToString();
            ddlPartyCode.SelectedValue = arrString[13].ToString();

            ComboBoxItem item3 = new ComboBoxItem(arrString[13].ToString());

            string CommandText2 = "SELECT   M.PRTY_CODE, M.PRTY_NAME  FROM   tx_VENDOR_MSt M WHERE   M.VENDOR_CAT_CODE NOT IN  ('PACKING MATERIAL SUPPLIER',  'TRANSPORTER & LOGISTICS',  'GENERAL ITEMS SUPPLIER', 'DYES & CHEMICAL SUPPLIERS')  AND (UPPER (M.PRTY_CODE) LIKE :SearchQuery    OR UPPER (M.PRTY_NAME) LIKE :SearchQuery)";
            string whereClause2 = string.Empty;
            string SortExpression2 = " order by PRTY_CODE";
            string SearchQuery2 = "%";
            DataTable dt2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, whereClause2, SortExpression2, "", SearchQuery2, "");
            ddlPartyCode.DataSource = dt2;
            ddlPartyCode.DataTextField = "PRTY_CODE";
            ddlPartyCode.DataValueField = "PRTY_NAME";
            ddlPartyCode.DataBind();
            int m = -1;
            foreach (ComboBoxItem it in ddlPartyCode.Items)
            {
                if (it.Text == item3.Text)
                {
                    ddlPartyCode.SelectedIndex = m;
                    ddlPartyCode.SelectedText = it.Text;
                    ddlPartyCode.SelectedValue = it.Value;
                    txtPartyCode.Text = it.Text;
                    txtPartyAddress1.Text = it.Value;
                    break;
                }
                m++;
            }







        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po item selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private void GetDataForDetailWorkOrder(string WO_Type, int WONumb, string ArticleCode, string ShadeCode, string SHADE_FAMILY)
    {
        try
        {
            txtShadeCode.Text = ShadeCode;
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(ArticleCode, WONumb, UNIQUEID))
            {
                //DataTable dt = SaitexBL.Interface.Method.OD_WO_MST.GetTRNData_ForReceiving(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, WO_Type, WONumb, ArticleCode, ShadeCode, oUserLoginDetail.DT_STARTDATE.Year);

                DataTable dt = SaitexBL.Interface.Method.OD_WO_MST.GetTRNData_ForWorkReceiving(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, WO_Type, WONumb, ArticleCode, ShadeCode, oUserLoginDetail.DT_STARTDATE.Year);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtICODE.Text = dt.Rows[0]["ARTICLE_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["YARN_DESC"].ToString().Trim();
                    txtPONumb.Text = dt.Rows[0]["WO_NUMB"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    //lblMaxQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    txtBasicRate.Text = double.Parse(dt.Rows[0]["BASIC_RATE"].ToString().Trim()).ToString();
                    txtFinalRate.Text = double.Parse(dt.Rows[0]["FINAL_RATE"].ToString().Trim()).ToString();
                    txtAmount.Text = "0";
                    lblPO_BRANCH.Text = dt.Rows[0]["BRANCH_CODE"].ToString().Trim();
                    lblPO_COMP.Text = dt.Rows[0]["COMP_CODE"].ToString().Trim();
                    lblPO_TYPE.Text = dt.Rows[0]["WO_TYPE"].ToString().Trim();
                    txtShadeCode.Text = dt.Rows[0]["SHADE_CODE"].ToString().Trim();
                    txtShadeFamily.Text = dt.Rows[0]["SHADE_FAMILY"].ToString().Trim();
                    // ddlCostCode.Focus();
                    DataTable dtdistax = SaitexBL.Interface.Method.YRN_IR_MST.GetTRNData_ForWork_OrderReceivingDisTaxes(lblPO_COMP.Text.Trim(), lblPO_BRANCH.Text.Trim(), lblPO_TYPE.Text, int.Parse(txtPONumb.Text), txtICODE.Text, dt.Rows[0]["SHADE_CODE"].ToString().Trim(), dt.Rows[0]["SHADE_FAMILY"].ToString());
                    mapDataTableForDisTaxes(dtdistax);
                }
            }
            else
            {
                txtShadeCode.Text = string.Empty;
                RefreshDetailRow();
                CommonFuction.ShowMessage("Item Already Included");
            }
        }
        catch
        {
            throw;
        }
    }

    private void mapDataTableForDisTaxes(DataTable dt)
    {
        try
        {
            if (Session["dtDicRate"] == null)
            {
                CreateDataTableForDisTax();
            }
            DataTable dtRate1 = (DataTable)Session["dtDicRate"];
            double StartFinalAmount = 0;
            double.TryParse(txtBasicRate.Text, out StartFinalAmount);
            double dFinalRate = StartFinalAmount;
            if (dt != null && dt.Rows.Count > 0)
            {
                int iloop = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    iloop += 1;
                    DataRow drNew = dtRate1.NewRow();
                    drNew["Uniqueid"] = iloop;
                    drNew["PO_COMP_CODE"] = lblPO_COMP.Text;
                    drNew["PO_BRANCH"] = lblPO_BRANCH.Text;
                    drNew["PO_TYPE"] = lblPO_TYPE.Text;
                    drNew["PO_YEAR"] = int.Parse(dr["YEAR"].ToString());
                    drNew["PO_NUMB"] = int.Parse(txtPONumb.Text);
                    drNew["YARN_CODE"] = dr["YARN_CODE"];
                    drNew["SHADE_CODE"] = dr["SHADE_CODE"];
                    drNew["SHADE_FAMILY"] = dr["SHADE_FAMILY"];
                    drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                    drNew["Rate"] = dr["RATE"];
                    drNew["COMPO_SL"] = dr["COMPO_SL"];
                    drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                    drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                    drNew["IS_PO"] = "1";
                    double dAmount = 0;

                    double cAmount = 0;
                    double rate = double.Parse(dr["Rate"].ToString());
                    if (dr["BASE_COMPO_CODE"].ToString().Equals("Basic Rate"))
                    {
                        cAmount = (StartFinalAmount * rate) / 100;
                    }
                    else if (dr["BASE_COMPO_CODE"].ToString().Equals("Final Rate"))
                    {
                        cAmount = (dFinalRate * rate) / 100;
                    }
                    else
                    {
                        DataView dvv = new DataView(dtRate1);
                        dvv.RowFilter = "COMPO_CODE='" + dr["BASE_COMPO_CODE"].ToString() + "'";

                        if (dvv.Count > 0)
                        {
                            double.TryParse(dvv[0]["Amount"].ToString(), out dAmount);
                        }
                        cAmount = (dAmount * rate) / 100;
                    }

                    if (dr["COMPO_TYPE"].ToString().Equals("D"))
                    {
                        dFinalRate = dFinalRate - cAmount;
                    }
                    else
                    {
                        dFinalRate = dFinalRate + cAmount;
                    }
                    drNew["Amount"] = cAmount;
                    dtRate1.Rows.Add(drNew);
                }
            }
            //txtpoRate.Text = dFinalRate.ToString();
            txtFinalRate.Text = dFinalRate.ToString();
            Session["dtDicRate"] = dtRate1;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTableForDisTax()
    {
        try
        {
            DataTable dtRate1 = new DataTable();
            dtRate1.Columns.Add("Uniqueid", typeof(int));

            dtRate1.Columns.Add("PO_COMP_CODE", typeof(string));
            dtRate1.Columns.Add("PO_BRANCH", typeof(string));
            dtRate1.Columns.Add("PO_TYPE", typeof(string));
            dtRate1.Columns.Add("PO_NUMB", typeof(int));
            dtRate1.Columns.Add("PO_YEAR", typeof(int));
            dtRate1.Columns.Add("YARN_CODE", typeof(string));
            dtRate1.Columns.Add("SHADE_CODE", typeof(string));
            dtRate1.Columns.Add("SHADE_FAMILY", typeof(string));
            dtRate1.Columns.Add("COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("Rate", typeof(double));
            dtRate1.Columns.Add("COMPO_SL", typeof(int));
            dtRate1.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate1.Columns.Add("Amount", typeof(double));
            dtRate1.Columns.Add("BASE_COMPO_CODE", typeof(string));
            dtRate1.Columns.Add("IS_PO", typeof(string));
            Session["dtDicRate"] = dtRate1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            cmbPOITEM.SelectedIndex = -1;
            txtPONumb.Text = "";
            txtICODE.Text = "";
            txtDESC.Text = "";
            txtDESC.ToolTip = "";
            txtShadeCode.Text = string.Empty;
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            //txtpoRate.Text = "";
            txtFinalRate.Text = "";
            txtAmount.Text = "";
            txtLotNo.SelectedIndex = -1;
            txtGrade.SelectedIndex = -1;
            lblMaxQTY.Text = "";
            txtDetRemarks.Text = "";
            // chk_QCFlag.Checked = false;
            lblPO_BRANCH.Text = "";
            lblPO_COMP.Text = "";
            lblPO_TYPE.Text = "";
            txtChallanNo.Text = "";
            txtnoofunit.Text = "0";
            //txtWeightofUnit.Text = "";
            txtCarton.Text = string.Empty;
            ViewState["UNIQUEID"] = null;
            cmbPOITEM.Enabled = true;
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                txtPONumb.Text = dv[0]["PO_NUMB"].ToString();

                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                txtICODE.Text = dv[0]["YARN_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtShadeCode.Text = dv[0]["SHADE_CODE"].ToString();
                txtShadeFamily.Text = dv[0]["SHADE_FAMILY"].ToString();
                txtDESC.ToolTip = dv[0]["YARN_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                //txtpoRate.Text = dv[0]["PO_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                txtLotNo.SelectedValue = dv[0]["LOT_NO"].ToString();
                txtnoofunit.Text = dv[0]["NO_OF_UNIT"].ToString();
                //txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                //lblMaxQTY.Text = dv[0]["REM_QTY"].ToString();
                txtNoOfPallet.Value = dv[0]["NO_OF_PALLET"].ToString();
                //txtGrossWt.Value = dv[0]["GROSS_WT"].ToString();
                lblMaxQTY.Text = txtGrossWt.Value = dv[0]["GROSS_WT"].ToString();
                txtTareWt.Value = dv[0]["TARE_WT"].ToString();
                txtCarton.Text = dv[0]["CARTONS"].ToString();
                txtChallanNo.Text = dv[0]["PI_NO"].ToString();
                ViewState["UNIQUEID"] = UNIQUEID;
                cmbPOITEM.Enabled = false;



                ComboBoxItem item1 = new ComboBoxItem(dv[0]["LOT_NO"].ToString());
                string CommandText = "SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GREY_LOT_NO'))         AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery) ";
                string whereClause = string.Empty;
                string SortExpression = " order by MST_CODE";
                string SearchQuery = "%";
                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
                txtLotNo.DataSource = dt;
                txtLotNo.DataTextField = "MST_CODE";
                txtLotNo.DataValueField = "MST_CODE";
                txtLotNo.DataBind();
                int x = -1;
                foreach (ComboBoxItem it in txtLotNo.Items)
                {
                    if (it.Text == item1.Text)
                    {
                        txtLotNo.SelectedIndex = x;
                        txtLotNo.SelectedText = it.Text;
                        txtLotNo.SelectedValue = it.Value;
                        break;
                    }
                    x++;
                }







                ComboBoxItem item2 = new ComboBoxItem(dv[0]["GRADE"].ToString());

                string CommandText1 = "SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GRADE'))         AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery) ";
                string whereClause1 = string.Empty;
                string SortExpression1 = " order by MST_CODE";
                string SearchQuery1 = "%";
                DataTable dt1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, whereClause1, SortExpression1, "", SearchQuery1, "");
                txtGrade.DataSource = dt1;
                txtGrade.DataTextField = "MST_CODE";
                txtGrade.DataValueField = "MST_CODE";
                txtGrade.DataBind();
                int z = -1;
                foreach (ComboBoxItem it in txtGrade.Items)
                {
                    if (it.Text == item2.Text)
                    {
                        txtGrade.SelectedIndex = z;
                        txtGrade.SelectedText = it.Text;
                        txtGrade.SelectedValue = it.Value;
                        break;
                    }
                    z++;
                }

            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlGateEntryNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOVForGate(e.Text.ToUpper(), e.ItemsOffset);
            ddlGateEntryNo.Items.Clear();

            ddlGateEntryNo.DataSource = data;
            ddlGateEntryNo.DataTextField = "GATE_NUMB";
            ddlGateEntryNo.DataValueField = "GATE_DATA";
            ddlGateEntryNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetGateCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in gate entry selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetLOVForGate(string text, int startOffset)
    {
        try
        {
            DataTable data = null;
            string CommandText = string.Empty;
            string whereClause = string.Empty;

            if (string.Compare(lblMode.Text, "You are in Save Mode", true) != 1)
            {
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'JOBWORK ORDER IN' AND NVL (a.ISSUE_NUMB, 0) = 0    AND A.GATE_NUMB NOT IN(SELECT   M.GATE_NUMB  FROM   YRN_IR_MST M   WHERE       M.TRN_TYPE = 'RYS11'  AND M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'   AND M.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND M.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') ) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15";
            }
            else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
            {
                CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE,  a.prty_code, a.prty_name,a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'JOBWORK ORDER IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0  AND A.GATE_NUMB  IN(SELECT   M.GATE_NUMB  FROM   YRN_IR_MST M   WHERE       M.TRN_TYPE = 'RYS11'  AND M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'   AND M.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND M.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15 ";
            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "You are in Save Mode", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE,a.prty_code,a.prty_name,a.trsp_code,a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='JOBWORK ORDER IN' AND NVL (a.ISSUE_NUMB, 0) = 0  AND A.GATE_NUMB NOT IN(SELECT   M.GATE_NUMB  FROM   YRN_IR_MST M   WHERE       M.TRN_TYPE = 'RYS11'  AND M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'   AND M.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND M.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }
                else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name,A.ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='JOBWORK ORDER IN')asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0  AND A.GATE_NUMB  IN(SELECT   M.GATE_NUMB  FROM   YRN_IR_MST M   WHERE       M.TRN_TYPE = 'RYS11'  AND M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'   AND M.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND M.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "')asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB)WHERE ROWNUM <= " + startOffset + ")";
                }
            }

            string SortExpression = " ORDER BY GATE_NUMB ";
            string SearchQuery = text + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetGateCount(string text)
    {

        DataTable data = new DataTable();
        string CommandText = "";
        if (string.Compare(lblMode.Text, "You are in Save Mode", true) != 1)
        {
            CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'JOBWORK ORDER IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
        }
        else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
        {
            CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'JOBWORK ORDER IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
        }

        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlGateEntryNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            //ResetDetailOnPartySelection();

            string cString = ddlGateEntryNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            DateTime GateDate = DateTime.Parse(arrString[0].ToString());
            string VahicleNo = arrString[1].ToString();
            string PartyChallanNo = arrString[2].ToString();
            DateTime partyChallanDate = DateTime.Parse(arrString[3].ToString());

            txtGateEntryNo.Text = ddlGateEntryNo.SelectedText.Trim();
            txtGateEntryDate.Text = GateDate.ToShortDateString();
            txtVehicleNo.Text = VahicleNo;
            txtPartyChallanNo.Text = PartyChallanNo;
            txtPartyChallanDate.Text = partyChallanDate.ToShortDateString();
            lblPartyCode.Text = arrString[4].ToString();
            txtPartyAddress.Text = arrString[5].ToString();
            lblTransporterCode.Text = arrString[6].ToString();
            txtTransporterAddress.Text = arrString[7].ToString();
            txtTotalPartyAmt.Text = arrString[8].ToString();

            txtPartyBillNo.Text = PartyChallanNo;
            txtPartyBillDate.Text = partyChallanDate.ToShortDateString();
            bool result = CheckBillEntryExistence(txtPartyChallanNo.Text, txtPartyAddress.Text, lblPartyCode.Text, oUserLoginDetail.DT_STARTDATE.Year);
            if (!result)
            {
                CommonFuction.ShowMessage("BillEntry No Already Exists Please Enter Another!!");
                txtPartyBillNo.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in gate entry selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
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
            e.ItemsCount = GetReceivingCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in mrn selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string CommandText = "SELECT * from (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'  and nvl(a.conf_flag,0)=0 ) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) where rownum<=15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and TRN_NUMB not in (SELECT TRN_NUMB from (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'  and nvl(a.conf_flag,0)=0) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) where rownum<='" + startOffset + "')";
            }

            string SortExpression = "  ORDER BY TRN_NUMB DESC, TRN_DATE DESC";
            string SearchQuery = text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetReceivingCount(string text)
    {
        try
        {
            string CommandText = "SELECT * from (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM YRN_IR_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) ";
            string whereClause = string.Empty;

            string SortExpression = " ";
            string SearchQuery = text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return dt.Rows.Count;
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();
            int iRecordFound = GetdataByTRNNUMBer(TRN_NUMBER);

            if (iRecordFound > 0)
            {
                //
            }
            else
            {
                InitialisePage();
                ActivateUpdateMode();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in mrn selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddlPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);

            ddlPartyCode.Items.Clear();

            ddlPartyCode.DataSource = data;
            ddlPartyCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('18','47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','18','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
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

        string CommandText = " SELECT   PRTY_CODE   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','18,'48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyAddress1.Text = ddlPartyCode.SelectedValue.Trim();
            txtPartyCode.Text = ddlPartyCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
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
            cmbPOITEM.Enabled = true;
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
            cmbPOITEM.Enabled = true;
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

            txtAmountInWords.Text = "Amount In Words... ";
            double dPartyBillAmount = 0;
            if (double.TryParse(txtPartyBillAmount.Text.Trim(), out dPartyBillAmount))
            {
                AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
                // lblPartyBillAmountInWords.Text = oRupeesToWord.changeNumericToWords(dPartyBillAmount);
                txtAmountInWords.Text = "Amount In Words... " + oRupeesToWord.changeNumericToWords(dPartyBillAmount);
            }
            else
            {
                txtPartyBillAmount.Text = string.Empty;
                txtPartyBillAmount.Focus();
                CommonFuction.ShowMessage("Invalid Party Bill Amount Entered.");
            }
            txtPartyBillAmount.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in bill amount.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    // Dis/ tax Adjust button for transaction table


    // Dis/ tax Adjust button for master table
    protected void btnDisTaxAdjMST_Click(object sender, EventArgs e)
    {
        try
        {
            txtPartyBillAmount.ReadOnly = false;
            string URL = "../../Yarn/SalesWork/Pages/MRNDisTaxAdjMST.aspx";
            double TotalAmount = 0;
            double.TryParse(txtTotalAmount.Text, out TotalAmount);
            URL = URL + "?FinalAmount=" + TotalAmount.ToString();
            URL = URL + "&TextBoxId=" + txtPartyBillAmount.ClientID;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting other charges for transaction."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtFinalRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dFinalRate = 0;
            double.TryParse(txtFinalRate.Text.Trim(), out dFinalRate);
            double dQty = 0;
            double.TryParse(txtQTY.Text.Trim(), out dQty);
            double dAmount = dQty * dFinalRate;
            txtAmount.Text = dAmount.ToString();

            txtFinalRate.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Final Rate.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnSubDetail_Click1(object sender, EventArgs e)
    {
        try
        {
            if (txtPONumb.Text != "")
            {
                string URL = "WorkOrderAgainstReturnAdj.aspx";
                URL = URL + "?ItemCodeId=" + txtICODE.Text;
                URL = URL + "&SHADE_CODE=" + "GREY";
                URL = URL + "&SHADE_FAMILY=" + "GREY";
                URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                URL = URL + "&txtBasicRate=" + txtFinalRate.ClientID;
                URL = URL + "&TRN_TYPE=" + "IYS11";
                //URL = URL + "&ChallanNo=" + txtTRNNUMBer.Text;
                URL = URL + "&PO_NUMB=" + txtPONumb.Text;
                URL = URL + "&PO_TYPE=" + "WUM";
                URL = URL + "&ChallanNo=" + txtChallanNo.Text;
                URL = URL + "&REMQTY=" + lblMaxQTY.Text;
                URL = URL + "&txtQtyUnit=" + lblMaxQTY.ClientID;
                URL = URL + "&txtQtyUom=" + txtUNIT.ClientID;
                URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
                URL = URL + "&STORE=" + ddlStore.SelectedValue;
                URL = URL + "&PI_NO=NA";
                //URL = URL + "&PARTY_CODE=" + lblPartyCode.Text;
                URL = URL + "&PARTY_CODE=" + txtPartyCode.Text;
                URL = URL + "&CARTONS=" + txtCarton.ClientID;
                URL = URL + "&AmountId=" + txtAmount.ClientID;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                //txtnoofunit.ReadOnly = false;

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

    protected void CalculateTotalAmount()
    {
        try
        {
            double TotalAmount = 0;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    double Amount = 0;

                    double.TryParse(dr["AMOUNT"].ToString(), out Amount);
                    TotalAmount += Amount;
                }
            }
            txtTotalAmount.Text = TotalAmount.ToString();
        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyBillNo_TextChanged(object sender, EventArgs e)
    {
        bool result = CheckBillEntryExistence(txtPartyChallanNo.Text, txtPartyAddress.Text, lblPartyCode.Text, oUserLoginDetail.DT_STARTDATE.Year);
        if (!result)
        {
            CommonFuction.ShowMessage("BillEntry No Already Exists Please Enter Another!!");
            txtPartyBillNo.Text = string.Empty;
        }
    }

    protected void txtNoOfUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //txtnoofunit.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Number Of Unit.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtWeightofUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //txtWeightofUnit.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Weight Of Unit.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtQTY_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtQTY.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Quantity.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }




}