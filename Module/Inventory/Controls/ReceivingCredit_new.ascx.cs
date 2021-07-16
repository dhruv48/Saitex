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

public partial class Module_Inventory_Controls_ReceivingCredit_new : System.Web.UI.UserControl
{
    private DataTable dtTRN_SUB = null;
    private DataTable dtInvoiceDicRateMST = null;
    private DataTable dtDicRate = null;
    private  DateTime StartDate;
    private  DateTime EndDate;
    private SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private  string TRN_TYPE  = "RMS01";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                InitialisePage();

                if (Session["LoginDetail"] != null)
                {
                    oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                  //  ddlStore.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;
                }
                
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
            BindDropDown(ddlLocation);
            BindDepartment();
            ViewState["dtDetailTBL"] = null;
            Session["dtInvoiceDicRateMST"] = null;
            Session["dtDicRate"] = null;
            Session["dtItemTRN_SUB"] = null;           
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

    private void BindNewMRNNum()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.TRN_TYPE = TRN_TYPE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtTRNNUMBer.Text = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetNewTRNNumber(oTX_ITEM_IR_MST).ToString();
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
            ddlLocation.SelectedIndex = 0;
            txtFormRefNo.Text = string.Empty;
            txtFormType.Text = string.Empty;
            txtGateEntryDate.Text = string.Empty;

            txtLRDate.Text = string.Empty;
            txtLRNo.Text = string.Empty;
            txtMRNDate.Text = string.Empty;
            txtTRNNUMBer.Text = string.Empty;

            lblPartyCode.Text = string.Empty;
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
            BindDepartment();
          //  ddlStore.SelectedIndex= ddlStore.Items.IndexOf(ddlStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));           
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


    private void BindDropDown(DropDownList ddl)
    {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        if (dt!=null && dt.Rows.Count > 0)
        {
           
            ddl.DataSource = dt;
            ddl.DataTextField = "MST_DESC";
            ddl.DataValueField = "MST_CODE";
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
    private void BindDepartment()
    {
        {
            try
            {
                ddlStore.Items.Clear();
                DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAMEDyes("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlStore.DataSource = dt;
                    ddlStore.DataTextField = "MST_DESC";
                    ddlStore.DataValueField = "MST_DESC";
                    ddlStore.DataBind();
                   // ddlStore.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                    if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                    {
                        ddlStore.DataSource = dtDepartment;
                        ddlStore.DataTextField = "DEPT_NAME";
                        ddlStore.DataValueField = "DEPT_NAME";
                        ddlStore.DataBind();
                    }
                }
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
            }
            catch
            {
                throw;
            }
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
            dtDetailTBL.Columns.Add("ITEM_CODE", typeof(string));
            dtDetailTBL.Columns.Add("ITEM_DESC", typeof(string));
            dtDetailTBL.Columns.Add("HSN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("TRN_QTY_1", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("PO_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));

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
                        string sITEM_CODE = dr["ITEM_CODE"].ToString();
                        deleteItemReceiptSUBTRNRow(sPO_COMP_CODE, sPO_BRANCH, sPO_TYPE, iPO_NUMB, sITEM_CODE);
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

    private void deleteItemReceiptSUBTRNRow(string PO_COMP_CODE, string PO_BRANCH, string PO_TYPE, int PO_NUMB, string ITEM_CODE)
    {
        try
        {
            if (Session["dtItemTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
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
                    string sITEM_CODE = row["ITEM_CODE"].ToString();

                    if (sPO_COMP_CODE == PO_COMP_CODE && sPO_BRANCH == PO_BRANCH && sPO_TYPE == PO_TYPE && iPO_NUMB == PO_NUMB && sITEM_CODE == ITEM_CODE)
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
            Session["dtItemTRN_SUB"] = dtTRN_SUB;
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

            //rvbill.MinimumValue = StartDate.ToShortDateString();
            //rvbill.MaximumValue = EndDate.ToShortDateString();
            //rvDate.MinimumValue = StartDate.ToShortDateString();
            // rvDate.MaximumValue = EndDate.ToShortDateString();
            //  rvchalan.MinimumValue = StartDate.ToShortDateString();
            // rvchalan.MaximumValue = EndDate.ToShortDateString();
            // rvgate.MinimumValue = StartDate.ToShortDateString();
            // rvgate.MaximumValue = EndDate.ToShortDateString();
            //rvlr.MinimumValue = StartDate.ToShortDateString();
            //rvlr.MaximumValue = EndDate.ToShortDateString();

            imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
            imgbtnDelete.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to delete this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
            imgbtnSave.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to save this record')");
            imgbtnUpdate.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to update this record')");

            //System.Web.UI.HtmlControls.HtmlGenericControl body = (System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Mybody");
            //body.Attributes.Add("OnClick", "javascript:check()");
            //body.Attributes.Add("onFocus", "javascript:check()");

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
                   // if (ddlStore.SelectedIndex != 0)
                        if (ddlStore.SelectedValue == "GENERAL") 
                        saveMaterialReceipt();
                    else
                        Common.CommonFuction.ShowMessage("Please select Store");
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
            bool Result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.CheckBillEntryExistence(BillNo, PartyName, PartyCode, Year);
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
            int iCountAll = 0;
            msg = string.Empty;

            iCountAll += 1;
            if (txtTRNNUMBer.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. MRN Number required.\r\n";
            }

            iCountAll += 1;
            if (lblPartyCode.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select party first.\r\n";
            }

            iCountAll += 1;
            if (txtGateEntryNo.Text != "")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Gate Entry Details first.\r\n";
            }

            iCountAll += 1;
            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
            }

            if (txtPartyBillNo.Text != string.Empty)
            {
                iCountAll += 1;
                if (!SaitexBL.Interface.Method.TX_BILL_MST.ValidateBillNo(txtPartyBillNo.Text.Trim(), lblPartyCode.Text.Trim(), lblPartyCode.Text.Trim()))
                {
                    count += 1;
                }
                else
                {
                    msg += @"#. Sorry. This bill from party already entered.\r\n";
                }
            }
            if (count == iCountAll)
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
            Response.Redirect("~/Module/Inventory/Reports/ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
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
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                row.BackColor = System.Drawing.Color.Green;
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
            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_ITEM_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oTX_ITEM_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());
            oTX_ITEM_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oTX_ITEM_IR_MST.STORE = ddlStore.SelectedItem.Text;

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oTX_ITEM_IR_MST.GATE_DATE = dt;

            oTX_ITEM_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
            oTX_ITEM_IR_MST.GATE_OUT_NUMB = "";
            oTX_ITEM_IR_MST.GATE_PASS_TYPE = "";
            oTX_ITEM_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oTX_ITEM_IR_MST.LR_DATE = dt;

            oTX_ITEM_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_ITEM_IR_MST.PRTY_CH_DATE = dt;

            oTX_ITEM_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oTX_ITEM_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
            oTX_ITEM_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
            oTX_ITEM_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_ITEM_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oTX_ITEM_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            double totalAmt = 0;
            double.TryParse(txtTotalAmount.Text, out totalAmt);
            oTX_ITEM_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(txtPartyBillAmount.Text, out finalAmt);
            oTX_ITEM_IR_MST.FINAL_AMOUNT = finalAmt;

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oTX_ITEM_IR_MST.TRN_DATE = dt;

            oTX_ITEM_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oTX_ITEM_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);

            if (lblTransporterCode.Text == "")
                oTX_ITEM_IR_MST.TRSP_CODE = "NA";
            else
                oTX_ITEM_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oTX_ITEM_IR_MST.TUSER = oUserLoginDetail.UserCode;

            oTX_ITEM_IR_MST.BILL_NUMB = txtPartyBillNo.Text;

            dt = System.DateTime.Now.Date;
            bool Is_Bill_Date = false;
            Is_Bill_Date = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("BILL_DATE", Is_Bill_Date);
            oTX_ITEM_IR_MST.BILL_DATE = dt;

            oTX_ITEM_IR_MST.BILL_TYPE = "MSP";
            oTX_ITEM_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            int TRN_NUMB = 0;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (Session["dtInvoiceDicRateMST"] != null)
                dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];

            if (Session["dtDicRate"] != null)
                dtDicRate = (DataTable)Session["dtDicRate"];

            if (Session["dtItemTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
            }

            // Show message to the user that Total Amount OR Final Amount is not same as party Bill Amount.
            // Only message show then saved, .....  Guided By Akhikesh Sir 18 Oct 2011 (Added By Rajesh as per Bharat discussion.)

            double dblPartyBillAmt = 0;     // For Party Bill Amount
            double dblTotalAmount = 0;       // Total Amount
            double dblFinalAmount = 0;  // For Final Amount

            double.TryParse(txtTotalPartyAmt.Text.Trim(), out dblPartyBillAmt);
            double.TryParse(txtTotalAmount.Text.Trim(), out dblTotalAmount);
            double.TryParse(txtPartyBillAmount.Text.Trim(), out dblFinalAmount);

            if (dblPartyBillAmt != dblTotalAmount && dblPartyBillAmt != dblFinalAmount)
            {
                // CommonFuction.ShowMessage("Either Total Amount or Final Amount is not matched with Party Bill Amount.");
                //msgBox1.confirm("Either Total Amount or Final Amount is not matched with Party Bill Amount.. Still Do You want to proceed..", "hid_f2");

                //if (Request.Form["hid_f2"] == "1")      //if button2 is clicked and user confirmed
                //{
                bool result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.InsertRecevingCredit(oTX_ITEM_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
                if (result)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage(@"MRN Number : " + TRN_NUMB + " Saved successfully!!");

                    if (Session["dtInvoiceDicRateMST"] != null)
                        Session["dtInvoiceDicRateMST"] = null;

                    if (Session["dtDicRate"] != null)
                        Session["dtDicRate"] = null;

                    if (Session["dtItemTRN_SUB"] != null)
                    {
                        Session["dtItemTRN_SUB"] = null;
                    }

                }
                else
                {
                    CommonFuction.ShowMessage("Data Saving Failed..");
                }
                //}
            }
            else
            {
                bool result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.InsertRecevingCredit(oTX_ITEM_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
                if (result)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage(@"MRN Number : " + TRN_NUMB + " Saved successfully!!");
                    if (Session["dtInvoiceDicRateMST"] != null)
                        Session["dtInvoiceDicRateMST"] = null;

                    if (Session["dtDicRate"] != null)
                        Session["dtDicRate"] = null;

                    if (Session["dtItemTRN_SUB"] != null)
                    {
                        Session["dtItemTRN_SUB"] = null;
                    }

                }
                else
                {
                    CommonFuction.ShowMessage("Data Saving Failed..");
                }
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
            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
            oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_ITEM_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oTX_ITEM_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());
            oTX_ITEM_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oTX_ITEM_IR_MST.STORE = oTX_ITEM_IR_MST.STORE = ddlStore.SelectedItem.Text;

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oTX_ITEM_IR_MST.GATE_DATE = dt;

            oTX_ITEM_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
            oTX_ITEM_IR_MST.GATE_OUT_NUMB = "";
            oTX_ITEM_IR_MST.GATE_PASS_TYPE = "";
            oTX_ITEM_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oTX_ITEM_IR_MST.LR_DATE = dt;

            oTX_ITEM_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_ITEM_IR_MST.PRTY_CH_DATE = dt;

            oTX_ITEM_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oTX_ITEM_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
            oTX_ITEM_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
            oTX_ITEM_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_ITEM_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oTX_ITEM_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oTX_ITEM_IR_MST.TRN_DATE = dt;

            double totalAmt = 0;
            double.TryParse(txtTotalAmount.Text, out totalAmt);
            oTX_ITEM_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(txtPartyBillAmount.Text, out finalAmt);
            oTX_ITEM_IR_MST.FINAL_AMOUNT = finalAmt;

            oTX_ITEM_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oTX_ITEM_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            if (lblTransporterCode.Text == "")
                oTX_ITEM_IR_MST.TRSP_CODE = "NA";
            else
                oTX_ITEM_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oTX_ITEM_IR_MST.TUSER = oUserLoginDetail.UserCode;

            oTX_ITEM_IR_MST.BILL_NUMB = txtPartyBillNo.Text;

            dt = System.DateTime.Now.Date;
            bool Is_Bill_Date = false;
            Is_Bill_Date = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("BILL_DATE", Is_Bill_Date);
            oTX_ITEM_IR_MST.BILL_DATE = dt;

            oTX_ITEM_IR_MST.BILL_TYPE = "MSP";
            oTX_ITEM_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (Session["dtInvoiceDicRateMST"] != null)
                dtInvoiceDicRateMST = (DataTable)Session["dtInvoiceDicRateMST"];

            if (Session["dtDicRate"] != null)
                dtDicRate = (DataTable)Session["dtDicRate"];

            if (Session["dtItemTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
            }

            // Show message to the user that Total Amount OR Final Amount is not same as party Bill Amount.
            // Only message show then saved, .....  Guided By Akhikesh Sir 18 Oct 2011 (Added By Rajesh as per Bharat discussion.)

            double dblPartyBillAmt = 0;     // For Party Bill Amount
            double dblTotalAmount = 0;       // Total Amount
            double dblFinalAmount = 0;  // For Final Amount

            double.TryParse(txtTotalPartyAmt.Text.Trim(), out dblPartyBillAmt);
            double.TryParse(txtTotalAmount.Text.Trim(), out dblTotalAmount);
            double.TryParse(txtPartyBillAmount.Text.Trim(), out dblFinalAmount);

            if (dblPartyBillAmt != dblTotalAmount && dblPartyBillAmt != dblFinalAmount)
            {
                // CommonFuction.ShowMessage("Either Total Amount or Final Amount is not matched with Party Bill Amount.");
                //msgBox1.confirm("Either Total Amount or Final Amount is not matched with Party Bill Amount.. Still Do You want to proceed..", "hid_f2");

                //if (Request.Form["hid_f2"] == "1")      //if button2 is clicked and user confirmed
                //{
                bool result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.UpdateRecevingCredit(oTX_ITEM_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
                if (result)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage("Data updated Successfully");
                    if (Session["dtInvoiceDicRateMST"] != null)
                        Session["dtInvoiceDicRateMST"] = null;

                    if (Session["dtDicRate"] != null)
                        Session["dtDicRate"] = null;

                    if (Session["dtItemTRN_SUB"] != null)
                    {
                        Session["dtItemTRN_SUB"] = null;
                    }

                }
                else
                {
                    CommonFuction.ShowMessage("data updation Failed");
                }
                //}
            }
            else
            {
                bool result = SaitexBL.Interface.Method.TX_ITEM_IR_MST.UpdateRecevingCredit(oTX_ITEM_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
                if (result)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage("Data updated Successfully");
                    if (Session["dtInvoiceDicRateMST"] != null)
                        Session["dtInvoiceDicRateMST"] = null;

                    if (Session["dtDicRate"] != null)
                        Session["dtDicRate"] = null;

                    if (Session["dtItemTRN_SUB"] != null)
                    {
                        Session["dtItemTRN_SUB"] = null;
                    }

                }
                else
                {
                    CommonFuction.ShowMessage("data updation Failed");
                }
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
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                BindDropDown(ddlLocation);
                BindDepartment();
                txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();
             ddlStore.SelectedIndex=   ddlStore.Items.IndexOf(ddlStore.Items.FindByText(dt.Rows[0]["STORE"].ToString().Trim()));
                ddlLocation.SelectedValue = dt.Rows[0]["LOCATION"].ToString().Trim();
                txtFormRefNo.Text = dt.Rows[0]["FORM_NUMB"].ToString().Trim();
                txtFormType.Text = dt.Rows[0]["FORM_TYPE"].ToString().Trim();

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
                //txtPartyBillAmount.Text = dt.Rows[0][""].ToString().Trim();
                //txtPartyBillDate.Text = dt.Rows[0][""].ToString().Trim();
                //txtPartyBillNo.Text = dt.Rows[0][""].ToString().Trim();

                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim(), out dd))
                    txtPartyChallanDate.Text = dd.ToShortDateString();

                txtPartyChallanNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                
                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
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
                txtTotalPartyAmt.Text = dt.Rows[0]["PRTY_BILL_AMOUNT"].ToString().Trim();
            }
            if (iRecordFound == 1)
            {

                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                dtTRN_SUB = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetSubTrnDetailByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtTRN_SUB.Rows.Count > 0)
                {
                    MapDataTableSubTRNDetail(dtTRN_SUB);
                }
                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtDetailTBL.Rows.Count > 0)
                {

                    ViewState["dtDetailTBL"] = dtDetailTBL;
                    MapDataTable();
                    BindGridFromDataTable();
                    
                    // code to get dis taxes for master entry ( Other charges)                    
                    DataTable dtDisTaxMstTemp = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    if (dtDisTaxMstTemp.Rows.Count > 0)
                    {
                        MapDataTableDisTaxMST(dtDisTaxMstTemp);
                    }
                    else
                    {
                        double finalamount = 0;
                        double.TryParse(dt.Rows[0]["FINAL_AMOUNT"].ToString().Trim(), out finalamount);
                        txtPartyBillAmount.Text = finalamount.ToString();
                    }

                    // code to get dis taxes for transaction entry 
                    DataTable dtDisTaxTrnTemp = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetTrnDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    if (dtDisTaxTrnTemp.Rows.Count > 0)
                    {
                        MapDataTableDisTaxTrn(dtDisTaxTrnTemp);
                    }
                    // code to get sub tran detail for transaction entry 
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
                    else if (dr["BASE_COMPO_CODE"].ToString().Equals("Flat Amount"))
                    {
                        cAmount = rate;
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
                    drNew["ITEM_CODE"] = dr["ITEM_CODE"];
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
            dtDicRate.Columns.Add("ITEM_CODE", typeof(string));
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

            Session["dtItemTRN_SUB"] = dtTRN_SUB;
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
            dtTRN_SUB.Columns.Add("ITEM_CODE", typeof(string));
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

            Session["dtItemTRN_SUB"] = dtTRN_SUB;
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

            if (txtPONumb.Text != "" && txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "")
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

                        double subQty = 0;
                        if (Session["dtItemTRN_SUB"] != null)
                        {
                            dtTRN_SUB = (DataTable)Session["dtItemTRN_SUB"];
                            subQty = (from DataRow data in dtTRN_SUB.Rows
                                      where data.Field<string>("ITEM_CODE") == txtICODE.Text.Trim() && data.Field<int>("PO_NUMB") == int.Parse(txtPONumb.Text.Trim())
                                      select data).Sum(p => p.Field<double>("TRN_QTY"));

                        }

                        if (Qty == subQty)
                        {
                            // CODE MODIFIED FOR A SHORT SPAN OF TIME..
                            // UNCOMMENT THE ELSE CONDITION FOR BLOCKING THE EXCEED QTY.
                            // CODE CHANGED ON REQUEST FROM SHEKHAWAT FROMSAINATH AND APPROVAL FROM AKHILESH SIR
                            if (Qty > MaxQty)
                            {
                                //  txtQTY.Text = MaxQty.ToString();
                                CommonFuction.ShowMessage(@"Entered Quantity is larger than po Quantity.\r\nYou can receive maximum " + MaxQty + " quantity for this item of selected po.");
                                txtQTY.Focus();
                            }
                            //else
                            //{
                            if (UNIQUEID > 0)
                            {
                                DataView dv = new DataView(dtDetailTBL);
                                dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                                if (dv.Count > 0)
                                {
                                    dv[0]["PO_NUMB"] = txtPONumb.Text.Trim();
                                    dv[0]["PO_TYPE"] = lblPO_TYPE.Text;
                                    dv[0]["PO_COMP_CODE"] = lblPO_COMP.Text;
                                    dv[0]["PO_BRANCH"] = lblPO_BRANCH.Text;
                                    dv[0]["PO_YEAR"] = lblPO_YEAR.Text;
                                    dv[0]["ITEM_CODE"] = txtICODE.Text.Trim();
                                    dv[0]["ITEM_DESC"] = txtDESC.Text.Trim();
                                    dv[0]["HSN_CODE"] = txtHSNCODE.Text.Trim();
                                    dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                    dv[0]["UOM"] = txtUNIT.Text.Trim();
                                    dv[0]["BASIC_RATE"] = Math.Round( double.Parse(txtBasicRate.Text.Trim()),4);
                                    dv[0]["PO_RATE"] = Math.Round(double.Parse(txtpoRate.Text.Trim()),4);
                                    dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                    dv[0]["AMOUNT"] = Math.Round(double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim()),4);
                                    //dv[0]["COST_CODE"] =  txtCostCode.Text.Trim();
                                    dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                    //if (chk_QCFlag.Checked)
                                    dv[0]["QCFLAG"] = "Yes";
                                    //else
                                    //    dv[0]["QCFLAG"] = "No";

                                    DateTime dd = System.DateTime.Now;
                                    //DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                    dv[0]["DATE_OF_MFG"] = dd;

                                    double NoOfUnit = 0;
                                    double.TryParse(txtNoOfUnit.Text, out NoOfUnit);
                                    dv[0]["NO_OF_UNIT"] = NoOfUnit;
                                    dv[0]["UOM_OF_UNIT"] = txtUNIT.Text.Trim();

                                    double weightofunit = 0;
                                    double.TryParse(txtWeightofUnit.Text, out weightofunit);
                                    dv[0]["WEIGHT_OF_UNIT"] = weightofunit;

                                    dtDetailTBL.AcceptChanges();
                                }
                            }
                            else
                            {
                                DataRow dr = dtDetailTBL.NewRow();
                                dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                dr["PO_NUMB"] = txtPONumb.Text.Trim();
                                dr["PO_TYPE"] = lblPO_TYPE.Text;
                                dr["PO_COMP_CODE"] = lblPO_COMP.Text;
                                dr["PO_BRANCH"] = lblPO_BRANCH.Text;
                                dr["PO_YEAR"] = lblPO_YEAR.Text;
                                dr["ITEM_CODE"] = txtICODE.Text.Trim();
                                dr["ITEM_DESC"] = txtDESC.Text.Trim();
                                dr["HSN_CODE"] = txtHSNCODE.Text.Trim();
                                dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dr["TRN_QTY_1"] = 0;
                                dr["UOM"] = txtUNIT.Text.Trim();
                                dr["BASIC_RATE"] =Math.Round( double.Parse(txtBasicRate.Text.Trim()),4);
                                dr["PO_RATE"] =Math.Round( double.Parse(txtpoRate.Text.Trim()),4);
                                dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dr["AMOUNT"] =Math.Round( double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim()),4);
                                dr["COST_CODE"] = string.Empty; //txtCostCode.Text.Trim();
                                dr["MAC_CODE"] = string.Empty;
                                dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                //   if (chk_QCFlag.Checked)
                                dr["QCFLAG"] = "Yes";
                                //else
                                //    dr["QCFLAG"] = "No";

                                DateTime dd = System.DateTime.Now;
                                //DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                dr["DATE_OF_MFG"] = dd;

                                double NoOfUnit = 0;
                                double.TryParse(txtNoOfUnit.Text, out NoOfUnit);
                                dr["NO_OF_UNIT"] = NoOfUnit;

                                dr["UOM_OF_UNIT"] = txtUNIT.Text.Trim();

                                double weightofunit = 0;
                                double.TryParse(txtWeightofUnit.Text, out weightofunit);
                                dr["WEIGHT_OF_UNIT"] = weightofunit;

                                dtDetailTBL.Rows.Add(dr);
                            }
                            RefreshDetailRow();

                            // }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity and total sub tran qty is not equal.');", true);
                        }
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
            BindGridFromDataTable();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in cancel row editing.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text, e.ItemsOffset);
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "PO_NUMB";
            cmbPOITEM.DataValueField = "po_Item_trn";
            cmbPOITEM.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPOCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po item selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetPOData(string text, int startOffset)
    {
        try
        {
            DataTable dt = new DataTable();
            if (lblPartyCode.Text != "")
            {
                //string po_type = "PUM"; By Rajesh
                //string CommandText = "SELECT * FROM (SELECT * FROM (SELECT DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.ITEM_CODE|| '@'|| PT.YEAR)po_Item_trn, pt.PO_NUMB, pt.year, pt.ITEM_CODE, pt.ORD_QTY, PM.PRTY_CODE, pt.BASIC_RATE, pt.FINAL_RATE, i.ITEM_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_ITEM_PU_TRN pt, TX_ITEM_MST i, tx_item_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND pt.year = pm.year AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or ITEM_CODE LIKE :SearchQuery ORDER BY po_Item_trn) WHERE ROWNUM <= 15 ";

                string CommandText = "SELECT * FROM (SELECT * FROM (SELECT DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.ITEM_CODE|| '@'|| PT.YEAR)po_Item_trn, pt.PO_NUMB, pt.year, pt.ITEM_CODE, pt.ORD_QTY, PM.PRTY_CODE, pt.BASIC_RATE, pt.FINAL_RATE, i.ITEM_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_ITEM_PU_TRN pt, TX_ITEM_MST i, tx_item_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND pt.year = pm.year AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or ITEM_CODE LIKE :SearchQuery ORDER BY po_Item_trn) WHERE ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    //whereClause += "   AND po_Item_trn NOT IN(SELECT po_Item_trn FROM (SELECT * FROM (SELECT DISTINCT( PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.ITEM_CODE|| '@'|| PT.YEAR) po_Item_trn,pt.PO_NUMB, pt.year,pt.ITEM_CODE,pt.ORD_QTY,PM.PRTY_CODE,pt.BASIC_RATE,pt.FINAL_RATE,i.ITEM_DESC,NVL (PT.QTY_RCPT, 0) QTY_RCPT,NVL (PT.ORD_QTY, 0)- NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i,tx_item_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND pt.year = pm.year AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or ITEM_CODE LIKE :SearchQuery ORDER BY po_Item_trn) WHERE ROWNUM <= " + startOffset + ")";

                    whereClause += "   AND po_Item_trn NOT IN(SELECT po_Item_trn FROM (SELECT * FROM (SELECT DISTINCT( PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.ITEM_CODE|| '@'|| PT.YEAR) po_Item_trn,pt.PO_NUMB, pt.year,pt.ITEM_CODE,pt.ORD_QTY,PM.PRTY_CODE,pt.BASIC_RATE,pt.FINAL_RATE,i.ITEM_DESC,NVL (PT.QTY_RCPT, 0) QTY_RCPT,NVL (PT.ORD_QTY, 0)- NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i,tx_item_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND pt.year = pm.year AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or ITEM_CODE LIKE :SearchQuery ORDER BY po_Item_trn) WHERE ROWNUM <= " + startOffset + ")";
                }

                string SortExpression = " order by PO_NUMB";
                string SearchQuery = "%" + text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            }
            else
            {
                CommonFuction.ShowMessage("Please select Party First");
            }
            //if (dt != null && dt.Rows.Count > 0)
            //    dt = RemoveAlreadyAddedRow(dt);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPOCount(string text)
    {

        DataTable data = new DataTable();
        if (lblPartyCode.Text != "")
        {
            //string po_type = "PUM";  // By Rajesh

            //string CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.ITEM_CODE)po_Item_trn, pt.PO_NUMB, pt.ITEM_CODE, pt.ORD_QTY, PM.PRTY_CODE, pt.BASIC_RATE, pt.FINAL_RATE, i.ITEM_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_ITEM_PU_TRN pt, TX_ITEM_MST i, tx_item_pu_mst pm WHERE PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or ITEM_CODE LIKE :SearchQuery ORDER BY PO_NUMB) ";
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.ITEM_CODE)po_Item_trn, pt.PO_NUMB, pt.ITEM_CODE, pt.ORD_QTY, PM.PRTY_CODE, pt.BASIC_RATE, pt.FINAL_RATE, i.ITEM_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_ITEM_PU_TRN pt, TX_ITEM_MST i, tx_item_pu_mst pm WHERE PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or ITEM_CODE LIKE :SearchQuery ORDER BY PO_NUMB) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        else
        {
            CommonFuction.ShowMessage("Please select Party First");
        }

        return data.Rows.Count;
    }

    protected void cmbPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = cmbPOITEM.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string PO_Type = arrString[0].ToString();
            int PONumb = int.Parse(arrString[1].ToString());
            string Item_Code = arrString[2].ToString();
            int Year = int.Parse(arrString[3].ToString());
            
          
            txtPONumb.Text = arrString[1].ToString();
            GetDataForDetail(PO_Type, PONumb, Item_Code, Year);
            cmbPOITEM.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po item selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private void GetDataForDetail(string PO_Type, int PONumb, string Item_Code, int Year)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(Item_Code, PONumb, UNIQUEID))
            {
                DataTable dt = SaitexBL.Interface.Method.Material_Purchase_Order.GetTRNData_ForReceiving(Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PO_Type, PONumb, Item_Code);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtICODE.Text = dt.Rows[0]["ITEM_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["ITEM_DESC"].ToString().Trim();
                    txtHSNCODE.Text = dt.Rows[0]["HSN_CODE"].ToString().Trim();
                    txtDESC.ToolTip = dt.Rows[0]["ITEM_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    // txtQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    lblMaxQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    txtBasicRate.Text = double.Parse(dt.Rows[0]["BASIC_RATE"].ToString().Trim()).ToString();
                    txtFinalRate.Text = double.Parse(dt.Rows[0]["FINAL_RATE"].ToString().Trim()).ToString();
                    txtAmount.Text = (double.Parse(dt.Rows[0]["QTY_REM"].ToString()) * double.Parse(dt.Rows[0]["FINAL_RATE"].ToString())).ToString();
                    // txtDOM.Text = DateTime.Now.Date.ToShortDateString();
                    lblPO_BRANCH.Text = dt.Rows[0]["BRANCH_CODE"].ToString().Trim();
                    lblPO_YEAR.Text = dt.Rows[0]["YEAR"].ToString().Trim();
                    lblPO_COMP.Text = dt.Rows[0]["COMP_CODE"].ToString().Trim();
                    lblPO_TYPE.Text = dt.Rows[0]["PO_TYPE"].ToString().Trim();
                    // txtCostCode.Focus();


                    DataTable dtdistax = SaitexBL.Interface.Method.Material_Purchase_Order.GetTRNData_ForReceivingDisTaxes(int.Parse(lblPO_YEAR.Text), lblPO_COMP.Text.Trim(), lblPO_BRANCH.Text.Trim(), lblPO_TYPE.Text, int.Parse(txtPONumb.Text), Item_Code);
                    mapDataTableForDisTaxes(dtdistax);
                }


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
                    drNew["PO_NUMB"] = int.Parse(txtPONumb.Text);
                    drNew["PO_YEAR"] = dr["YEAR"];
                    drNew["ITEM_CODE"] = dr["ITEM_CODE"];
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
            txtpoRate.Text = dFinalRate.ToString();
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
            dtRate1.Columns.Add("ITEM_CODE", typeof(string));
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
            txtHSNCODE.Text = "";
            txtDESC.ToolTip = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtpoRate.Text = "";
            txtFinalRate.Text = "";
            txtAmount.Text = "";
            // txtCostCode.Text = "";
            // txtDOM.Text = "";
            lblMaxQTY.Text = "";
            txtDetRemarks.Text = "";
            // chk_QCFlag.Checked = false;
            lblPO_BRANCH.Text = "";
            lblPO_YEAR.Text = "";
            lblPO_COMP.Text = "";
            lblPO_TYPE.Text = "";

            txtNoOfUnit.Text = "";
            txtWeightofUnit.Text = "";

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
                string cString = dv[0]["PO_TYPE"].ToString() + "@" + dv[0]["PO_NUMB"].ToString() + "@" + dv[0]["ITEM_CODE"].ToString();

                cmbPOITEM.SelectedText = dv[0]["PO_NUMB"].ToString();
                cmbPOITEM.SelectedValue = cString;
                txtPONumb.Text = dv[0]["PO_NUMB"].ToString();

                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                lblPO_YEAR.Text = dv[0]["PO_YEAR"].ToString();
                txtICODE.Text = dv[0]["ITEM_CODE"].ToString();
                txtDESC.Text = dv[0]["ITEM_DESC"].ToString();
                txtHSNCODE.Text = dv[0]["HSN_CODE"].ToString();
                txtDESC.ToolTip = dv[0]["ITEM_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtpoRate.Text = dv[0]["PO_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                // txtCostCode.Text = dv[0]["COST_CODE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();

                txtNoOfUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                //if (dv[0]["QCFLAG"].ToString() == "Yes")
                //    chk_QCFlag.Checked = true;
                //else
                //    chk_QCFlag.Checked = false;

                //txtDOM.Text = DateTime.Parse(dv[0]["DATE_OF_MFG"].ToString()).ToShortDateString();
                ViewState["UNIQUEID"] = UNIQUEID;
                cmbPOITEM.Enabled = false;


                DataTable dt = SaitexBL.Interface.Method.Material_Purchase_Order.GetTRNData_ForReceiving(int.Parse(lblPO_YEAR.Text), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, dv[0]["PO_TYPE"].ToString(), int.Parse(dv[0]["PO_NUMB"].ToString()), dv[0]["ITEM_CODE"].ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (imgbtnSave.Visible)
                    {
                        lblMaxQTY.Text = dt.Rows[0]["QTY_REM"].ToString();
                    }
                    else
                    {
                        lblMaxQTY.Text = (double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()) + double.Parse(dv[0]["TRN_QTY"].ToString())).ToString();
                    }
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
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT|| '@'   || a.LR_NO|| '@'   || a.LR_DATE)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'MATERIAL IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15";
            }
            else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
            {
                CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT|| '@'   || a.LR_NO|| '@'   || a.LR_DATE)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE,  a.prty_code, a.prty_name,a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'MATERIAL IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15 ";
            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "You are in Save Mode", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT|| '@'   || a.LR_NO|| '@'   || a.LR_DATE) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE,a.prty_code,a.prty_name,a.trsp_code,a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='MATERIAL IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }
                else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT|| '@'   || a.LR_NO|| '@'   || a.LR_DATE) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name,A.ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='MATERIAL IN')asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0)asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB)WHERE ROWNUM <= " + startOffset + ")";
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
            CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'MATERIAL IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
        }
        else if (string.Compare(lblMode.Text, "You are in Update Mode", true) != 1)
        {
            CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name || '@'|| a.DOC_AMOUNT)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'MATERIAL IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
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
            ResetDetailOnPartySelection();

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
            txtLRNo.Text = arrString[9].ToString();
            txtLRDate.Text = arrString[10].ToString();
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
            string CommandText = "SELECT * from (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM TX_ITEM_IR_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'  and nvl(a.conf_flag,0)=0 ) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) where rownum<=15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and TRN_NUMB not in (SELECT TRN_NUMB from (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM TX_ITEM_IR_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "'  and nvl(a.conf_flag,0)=0) asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) where rownum<='" + startOffset + "')";
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
            string CommandText = "SELECT * from (SELECT * FROM (SELECT a.TRN_NUMB, a.TRN_DATE, a.PRTY_CODE, b.PRTY_NAME FROM TX_ITEM_IR_MST a, tx_vendor_mst b WHERE a.prty_code = b.prty_code(+) AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE = '" + TRN_TYPE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd WHERE TRN_NUMB LIKE :searchQuery OR TRN_DATE LIKE :searchQuery OR prty_code LIKE :searchQuery OR prty_name LIKE :searchQuery ORDER BY TRN_NUMB DESC, TRN_DATE DESC) ";
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
    protected void btnDisTaxAdj_Click(object sender, EventArgs e)
    {
        try
        {
            string URL = "MRNDisTaxAdj.aspx";
            URL = URL + "?FinalAmount=" + txtBasicRate.Text.Trim();
            URL = URL + "&TextBoxId=" + txtFinalRate.ClientID;
            URL = URL + "&PO_COMP_CODE=" + lblPO_COMP.Text.Trim();
            URL = URL + "&PO_BRANCH=" + lblPO_BRANCH.Text.Trim();
            URL = URL + "&PO_TYPE=" + lblPO_TYPE.Text.Trim();
            URL = URL + "&PO_NUMB=" + txtPONumb.Text.Trim();
            URL = URL + "&PO_YEAR=" + lblPO_YEAR.Text.Trim();
            URL = URL + "&ITEM_CODE=" + txtICODE.Text.Trim();
            txtFinalRate.ReadOnly = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting discount/ taxes adjustment for transaction."));
            lblMode.Text = ex.ToString();
        }
    }

    // Dis/ tax Adjust button for master table
    protected void btnDisTaxAdjMST_Click(object sender, EventArgs e)
    {
        try
        {
            txtPartyBillAmount.ReadOnly = false;
            string URL = "MRNDisTaxAdjMST.aspx";
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

    protected void btnSubDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPONumb.Text != string.Empty)
            {
                txtNoOfUnit.ReadOnly = false;
                // txtWeightofUnit.ReadOnly = false;
                txtQTY.ReadOnly = false;

                string URL = "ITEM_TRN_SUB.aspx";
                URL = URL + "?ITEM_CODE=" + txtICODE.Text;
                URL = URL + "&PO_NUMB=" + txtPONumb.Text;
                URL = URL + "&PO_TYPE=" + lblPO_TYPE.Text;
                URL = URL + "&PO_YEAR=" + lblPO_YEAR.Text;
                URL = URL + "&PO_COMP_CODE=" + lblPO_COMP.Text;
                URL = URL + "&PO_BRANCH=" + lblPO_BRANCH.Text;
                URL = URL + "&lblMaxQTY=" + lblMaxQTY.Text;
                URL = URL + "&txtQTY=" + txtQTY.ClientID;
                URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
                URL = URL + "&txtWeightOfUnit=" + txtWeightofUnit.ClientID;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select PO Number");
            }
        }
        catch
        {
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
            //if (txtPartyBillAmount.Text == "0")
            //{ txtPartyBillAmount.Text = TotalAmount.ToString(); }
            
            //if (txtPartyBillAmount.Text == "")
            //{ txtPartyBillAmount.Text = TotalAmount.ToString(); }
            txtPartyBillAmount.Text = TotalAmount.ToString();
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
            txtNoOfUnit.ReadOnly = true;
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
            //  txtWeightofUnit.ReadOnly = true;
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

    protected void grdMaterialItemReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(175, 202, 228);
                LinkButton lnkunige = (LinkButton)e.Row.FindControl("lnkunige");
                int UNIQUE_ID = int.Parse(lnkunige.CommandArgument);

                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtDetailTBL);

                    dv.RowFilter = "UNIQUEID=" + UNIQUE_ID;
                    if (dv.Count > 0)
                    {
                        string ITEM_CODE = dv[0]["ITEM_CODE"].ToString();

                        if (Session["dtItemTRN_SUB"] != null)
                        {
                            DataTable dtTRN_Sub = (DataTable)Session["dtItemTRN_SUB"];

                            DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
                            dvYRNDRecieve_trn.RowFilter = "ITEM_CODE='" + ITEM_CODE + "'";
                            if (dvYRNDRecieve_trn.Count > 0)
                            {
                                GridView grdBOM = e.Row.FindControl("grdBOM") as GridView;
                                grdBOM.DataSource = dvYRNDRecieve_trn;
                                grdBOM.DataBind();
                            }

                        }

                    }

                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Material GridRow DataBound.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

}