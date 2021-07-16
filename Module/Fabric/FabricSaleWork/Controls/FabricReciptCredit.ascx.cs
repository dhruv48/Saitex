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
using Obout.ComboBox;
using Common;
using errorLog;
public partial class Module_Inventory_Controls_FabricReciptCredit : System.Web.UI.UserControl
{
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtDetailTBL = null;
    private static string TRN_TYPE = "";
    private static int IsUpdateCall = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void InitialisePage()
    {
        try
        {
            TRN_TYPE = "RFS01";
            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();

            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }

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
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.TRN_TYPE = TRN_TYPE;
            oTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            txtTRNNUMBer.Text = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetNewTRNNumber(oTX_FABRIC_IR_MST).ToString();
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
            spnAInW.InnerText = "Amount In Words... ";
            txtDepartment.Text = "";
            txtFormRefNo.Text = "";
            txtFormType.Text = "";
            txtGateEntryDate.Text = "";
            //  txtGateEntryNo.Text = "";
            txtLRDate.Text = "";
            txtLRNo.Text = "";
            txtMRNDate.Text = "";
            txtTRNNUMBer.Text = "";
            txtPartyAddress.Text = "";
            txtPartyBillAmount.Text = "";
            txtPartyBillDate.Text = "";
            txtPartyBillNo.Text = "";
            txtPartyChallanDate.Text = "";
            txtPartyChallanNo.Text = "";
            txtPartyCode.SelectedIndex = -1;
            ddlGateEntryNo.SelectedIndex = -1;
            txtRemarks.Text = "";
            txtTransporterAddress.Text = "";
            txtTransporterCode.SelectedIndex = -1;
            txtVehicleNo.Text = "";

            ddlReceiptShift.SelectedIndex = 0;

            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();

            grdMaterialItemReceipt.DataSource = null;
            grdMaterialItemReceipt.DataBind();

            lblMode.Text = "Save";

            txtTRNNUMBer.ReadOnly = true;

            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                txtDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;
            }
            RefreshDetailRow();

           
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
            dtDetailTBL.Columns.Add("FABR_CODE", typeof(string));
            dtDetailTBL.Columns.Add("FDESC", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
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
            if (dtDetailTBL == null)
                CreateDataTable();

            DataRow dr = dtDetailTBL.NewRow();
            dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
            dr["DATE_OF_MFG"] = DateTime.Now.Date.ToShortDateString();
            dtDetailTBL.Rows.Add(dr);
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
            if (dtDetailTBL == null)
                InsertBlankRowInTable();

            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();

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
            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
                InsertBlankRowInTable();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUEID"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
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
            //imgbtnDelete.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to delete this record')");
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
            errorLog.ErrHandler.WriteError(ex.Message);

            CommonFuction.ShowMessage(@"Problem in saving Data.\r\nSee error log for detail.");
        }
    }
    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
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

            if (txtPartyCode.SelectedIndex != -1 || txtPartyCode.SelectedText.Trim() != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select party first.\r\n";
            }

            if (ddlGateEntryNo.SelectedIndex != -1 || ddlGateEntryNo.SelectedText.Trim() != string.Empty)
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

            lblMode.Text = "Update";
            txtTRNNUMBer.Text = "";
            tdUpdate.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in loading update mode.\r\nsee error log for detail.");
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
            errorLog.ErrHandler.WriteError(ex.Message);

            CommonFuction.ShowMessage(@"Problem in updating Data.\r\nSee error log for detail.");
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
            errorLog.ErrHandler.WriteError(ex.Message);

            CommonFuction.ShowMessage(@"Problem in clear page data.\r\nSee error log for detail.");
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           // Response.Redirect("~/Module/Inventory/Reports/ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

            CommonFuction.ShowMessage(@"Problem in get print.\r\nSee error log for detail.");
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
            errorLog.ErrHandler.WriteError(ex.Message);
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Row Editing/ Deletion.\r\nsee error log for detail.");
        }
    }

    private bool SearchItemCodeInGrid(string FabricCode, int PONumb, int UNIQUEID)
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

                    if (int.Parse(txtPONum.Text.Trim()) == PONumb && txtICODE.Text.Trim() == FabricCode && UNIQUEID != iUNIQUEID)
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

            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_FABRIC_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oTX_FABRIC_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oTX_FABRIC_IR_MST.GATE_DATE = dt;

            oTX_FABRIC_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(ddlGateEntryNo.SelectedText.Trim());
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = "";
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;

            oTX_FABRIC_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_FABRIC_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_FABRIC_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oTX_FABRIC_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oTX_FABRIC_IR_MST.TRN_DATE = dt;

            oTX_FABRIC_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oTX_FABRIC_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            if (txtTransporterCode.SelectedIndex < 0)
                oTX_FABRIC_IR_MST.TRSP_CODE = "NA";
            else
                oTX_FABRIC_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
            int TRN_NUMB = 0;
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.Insert(oTX_FABRIC_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"MRN Number : " + TRN_NUMB + " saved successfully.");
            }
            else
            {
                CommonFuction.ShowMessage("data  Saving Failed");
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

            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_FABRIC_IR_MST.FORM_NUMB = CommonFuction.funFixQuotes(txtFormRefNo.Text.Trim());
            oTX_FABRIC_IR_MST.FORM_TYPE = CommonFuction.funFixQuotes(txtFormType.Text.Trim());

            DateTime dt = System.DateTime.Now.Date;
            bool Is_Gate_Entry = false;
            Is_Gate_Entry = DateTime.TryParse(txtGateEntryDate.Text.Trim(), out dt);
            htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
            oTX_FABRIC_IR_MST.GATE_DATE = dt;

            oTX_FABRIC_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(ddlGateEntryNo.SelectedText.Trim());
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = "";
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            Is_LR = DateTime.TryParse(txtLRDate.Text.Trim(), out dt);
            htReceive.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;

            oTX_FABRIC_IR_MST.LR_NUMB = CommonFuction.funFixQuotes(txtLRNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtPartyChallanDate.Text.Trim(), out dt);
            htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtPartyChallanNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_FABRIC_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_FABRIC_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_IR_MST.REPROCESS = CommonFuction.funFixQuotes("");
            oTX_FABRIC_IR_MST.SHIFT = ddlReceiptShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtMRNDate.Text.Trim(), out dt);
            htReceive.Add("MRN", Is_MRN);
            oTX_FABRIC_IR_MST.TRN_DATE = dt;

            oTX_FABRIC_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oTX_FABRIC_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
            if (txtTransporterCode.SelectedIndex < 0)
                oTX_FABRIC_IR_MST.TRSP_CODE = "NA";
            else
                oTX_FABRIC_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;

            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.Update(oTX_FABRIC_IR_MST, dtDetailTBL, htReceive);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Data updated Successfully");
            }
            else
            {
                CommonFuction.ShowMessage("data updation Failed");
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
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();
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
                txtPartyCode.SelectedText = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                txtTransporterCode.SelectedText = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();

                ddlGateEntryNo.SelectedText = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

                DataTable partyData = GetLOVForParty("");

                txtPartyCode.DataSource = partyData;
                txtPartyCode.DataTextField = "PRTY_CODE";
                txtPartyCode.DataValueField = "Address";
                txtPartyCode.DataBind();

                foreach (ComboBoxItem item in txtPartyCode.Items)
                {
                    if (item.Text == dt.Rows[0]["PRTY_CODE"].ToString().Trim())
                    {
                        txtPartyCode.SelectedIndex = txtPartyCode.Items.IndexOf(item);
                        break;
                    }
                }

                DataTable transporterData = GetLOVForTransporter("");

                txtTransporterCode.DataSource = transporterData;
                txtTransporterCode.DataTextField = "PRTY_CODE";
                txtTransporterCode.DataValueField = "Address";
                txtTransporterCode.DataBind();

                foreach (ComboBoxItem item in txtTransporterCode.Items)
                {
                    if (item.Text == dt.Rows[0]["TRSP_CODE"].ToString().Trim())
                    {
                        txtTransporterCode.SelectedIndex = txtTransporterCode.Items.IndexOf(item);
                        break;
                    }
                }

                txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TAddress"].ToString().Trim();

                DataTable GateEntryData = GetLOVForGate("");

                ddlGateEntryNo.DataSource = GateEntryData;
                ddlGateEntryNo.DataTextField = "GATE_NUMB";
                ddlGateEntryNo.DataValueField = "GATE_DATA";
                ddlGateEntryNo.DataBind();

                foreach (ComboBoxItem item in ddlGateEntryNo.Items)
                {
                    if (item.Text == dt.Rows[0]["GATE_NUMB"].ToString().Trim())
                    {
                        ddlGateEntryNo.SelectedIndex = ddlGateEntryNo.Items.IndexOf(item);
                        break;
                    }
                }
                ddlGateEntryNo.SelectedText = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

            }
            if (iRecordFound == 1)
            {
                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtDetailTBL.Rows.Count > 0)
                {
                    MapDataTable();
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Material Detail.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();

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

    private void MapDataTable()
    {
        try
        {
            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));

            if (!dtDetailTBL.Columns.Contains("MAC_CODE"))
                dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["MAC_CODE"] = string.Empty;
            }
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
            DataTable data = GetLOVForParty(e.Text.ToUpper());
            txtPartyCode.Items.Clear();

            txtPartyCode.DataSource = data;
            txtPartyCode.DataTextField = "PRTY_CODE";
            txtPartyCode.DataValueField = "address";
            txtPartyCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in party selection. see error log for detail.");
        }
    }
    private DataTable GetLOVForParty(string Text)
    {
        try
        {
            string CommandText = "select distinct * from (select * from(Select distinct v.PRTY_CODE,PO_TYPE,PRTY_STATE,PRTY_ADD1,PRTY_ADD2,PRTY_NAME,PRTY_ADD1 ||',  '||nvl( PRTY_ADD2,' ') ||',  '|| nvl(PRTY_STATE,' ') address from TX_VENDOR_MST v right outer join tx_fabric_pu_mst p on V.PRTY_CODE=P.PRTY_CODE) a where PO_TYPE='PUM') b ";
            string WhereClause = " where PRTY_CODE like :SearchQuery or  PRTY_NAME like :SearchQuery or  PRTY_ADD1 like :SearchQuery  or PRTY_ADD2 like :SearchQuery or PRTY_STATE like :SearchQuery ";
            string SortExpression = " ORDER BY PRTY_CODE ";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }
    protected void txtPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (IsUpdateCall != 1)
                ResetDetailOnPartySelection();

            txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
            BindCMBPOITEM("", 0);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in party selection.\r\nsee error log for detail.");
        }

    }
    private void ResetDetailOnPartySelection()
    {
        try
        {
            ddlGateEntryNo.SelectedIndex = -1;
            ddlGateEntryNo.SelectedText = string.Empty;
            ddlGateEntryNo.SelectedValue = string.Empty;
            ddlGateEntryNo.Items.Clear();

            txtGateEntryDate.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtPartyChallanDate.Text = string.Empty;
            txtPartyChallanNo.Text = string.Empty;

            RefreshDetailRow();
            if (dtDetailTBL == null)
                CreateDataTable();

            dtDetailTBL.Rows.Clear();

            IsUpdateCall = IsUpdateCall + 1;
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
            DataTable data = GetLOVForTransporter(e.Text);

            txtTransporterCode.Items.Clear();

            txtTransporterCode.DataSource = data;
            txtTransporterCode.DataTextField = "PRTY_CODE";
            txtTransporterCode.DataValueField = "Address";
            txtTransporterCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Transporter selection.\r\nsee error log for detail.");
        }
    }
    private DataTable GetLOVForTransporter(string Text)
    {
        try
        {
            string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0) asd";
            string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
            string SortExpression = " order by PRTY_CODE asc";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }
    protected void txtTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtTransporterAddress.Text = txtTransporterCode.SelectedValue;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in Transporter selection.\r\nsee error log for detail.");
        }
    }
    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (dtDetailTBL == null)
                CreateDataTable();

            if (dtDetailTBL.Rows.Count < 15)
            {
                if (cmbPOITEM.SelectedText != "" && txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                    bool bb = SearchItemCodeInGrid(txtICODE.Text.Trim(), int.Parse(cmbPOITEM.SelectedText.Trim()), UNIQUEID);
                    if (!bb)
                    {
                        double Qty = 0;
                        double.TryParse(txtQTY.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {

                            double MaxQty = 0;
                            double.TryParse(lblMaxQTY.Text.Trim(), out MaxQty);
                            if (Qty > MaxQty)
                            {
                                txtQTY.Text = MaxQty.ToString();
                                CommonFuction.ShowMessage(@"Entered Quantity is larger than po Quantity.\r\nYou can receive maximum " + MaxQty + " quantity for this item of selected po.");
                                txtQTY.Focus();
                            }
                            else
                            {
                                   if (UNIQUEID > 0)
                                    {
                                        DataView dv = new DataView(dtDetailTBL);
                                        dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                                        if (dv.Count > 0)
                                        {
                                            dv[0]["PO_NUMB"] = cmbPOITEM.SelectedText.Trim();
                                            dv[0]["PO_TYPE"] = lblPO_TYPE.Text;
                                            dv[0]["PO_COMP_CODE"] = lblPO_COMP.Text;
                                            dv[0]["PO_BRANCH"] = lblPO_BRANCH.Text;
                                            dv[0]["FABR_CODE"] = txtICODE.Text.Trim();
                                            dv[0]["FDESC"] = txtDESC.Text.Trim();
                                            dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                            dv[0]["UOM"] = txtUNIT.Text.Trim();
                                            dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                            dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                            dv[0]["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                            dv[0]["COST_CODE"] = txtCostCode.Text.Trim();
                                            dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                            //if (chk_QCFlag.Checked)
                                            //    dv[0]["QCFLAG"] = "Yes";
                                            //else
                                            dv[0]["QCFLAG"] = "No";

                                            DateTime dd = System.DateTime.Now;
                                            DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                            dv[0]["DATE_OF_MFG"] = dd;

                                            dtDetailTBL.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        DataRow dr = dtDetailTBL.NewRow();
                                        dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                        dr["PO_NUMB"] = cmbPOITEM.SelectedText.Trim();
                                        dr["PO_TYPE"] = lblPO_TYPE.Text;
                                        dr["PO_COMP_CODE"] = lblPO_COMP.Text;
                                        dr["PO_BRANCH"] = lblPO_BRANCH.Text;
                                        dr["FABR_CODE"] = txtICODE.Text.Trim();
                                        dr["FDESC"] = txtDESC.Text.Trim();
                                        dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                        dr["UOM"] = txtUNIT.Text.Trim();
                                        dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                        dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                        dr["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                        dr["COST_CODE"] = txtCostCode.Text.Trim();
                                        dr["MAC_CODE"] = string.Empty;
                                        dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                        //if (chk_QCFlag.Checked)
                                        //    dr["QCFLAG"] = "Yes";
                                        //else
                                            dr["QCFLAG"] = "No";

                                        DateTime dd = System.DateTime.Now;
                                        DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                        dr["DATE_OF_MFG"] = dd;

                                        dtDetailTBL.Rows.Add(dr);
                                    }
                                RefreshDetailRow();
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
                grdMaterialItemReceipt.DataSource = dtDetailTBL;
                grdMaterialItemReceipt.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in adding transaction data.\r\nsee error log for detail.");
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in cancelling transaction.\r\nsee error log for detail.");
        }
    }

    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text);
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "PO_NUMB";
            cmbPOITEM.DataValueField = "po_Fabric_trn";
            cmbPOITEM.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;

            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in party selection.\r\nsee error log for detail.");
        }
    }
    protected DataTable GetPOData(string text)
    {
        try
        {
            DataTable dt = new DataTable();
            if (txtPartyCode.SelectedText != "")
            {
                string po_type = "PUM";
                string whereClause = " where PM.CONF_FLAG='1' and (nvl(PT.ORD_QTY,0)-nvl(PT.QTY_RCPT,0)) > 0 and pt.FABR_CODE = i.FABR_CODE and PT.PO_TYPE='" + po_type + "' and pm.PO_NUMB=PT.PO_NUMB and PM.PRTY_CODE='" + txtPartyCode.SelectedText.Trim() + "' and pt.PO_NUMB like :SearchQuery and pt.FABR_CODE like :SearchQuery";
                string sortExpression = " ORDER BY PO_NUMB";
                string commandText = "SELECT distinct (PT.PO_TYPE||'@'||PT.PO_NUMB||'@'||PT.FABR_CODE)  po_Fabric_trn, pt.PO_NUMB, pt.FABR_CODE, pt.ORD_QTY, pt.UOM, pm.CURRENCY_CODE, pm.CONVERSION_RATE, pt.DEL_DATE,PM.PRTY_CODE,pt.COMMENTS, pt.BASIC_RATE, pt.FINAL_RATE, pt.PRC_TYPE, i.FDESC,nvl(PT.QTY_RCPT,0) QTY_RCPT,nvl(PT.ORD_QTY,0)-nvl(PT.QTY_RCPT,0) as QTY_REM FROM TX_FABRIC_PU_TRN pt,TX_FABRIC_MST i ,tx_fabric_pu_mst pm ";

                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
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


    protected DataTable GetPOData(string text, int PO_Numb, string Fabric_Code)
    {
        try
        {
            DataTable dt = new DataTable();
            if (txtPartyCode.SelectedText != "")
            {
                string po_type = "PUM";
                string whereClause = " WHERE ( PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 30 AND pt.FABR_CODE = i.FABR_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB AND PM.PRTY_CODE = '" + txtPartyCode.SelectedText.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.FABR_CODE LIKE :SearchQuery) OR ( PM.CONF_FLAG = '1' AND pt.FABR_CODE = i.FABR_CODE AND pm.PO_NUMB = PT.PO_NUMB AND PT.PO_NUMB = '" + PO_Numb + "' AND PT.PO_TYPE = '" + po_type + "' AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PT.FABR_CODE = '" + Fabric_Code + "' )";
                string sortExpression = " ORDER BY PO_NUMB";
                string commandText = " SELECT DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.FABR_CODE) po_Fabric_trn,pt.COMP_CODE, pt.BRANCH_CODE,  pt.PO_NUMB,  pt.FABR_CODE,  pt.ORD_QTY,  i.FDESC,  NVL (PT.QTY_RCPT, 0) QTY_RCPT,  NVL (PT.QTY_RTN, 0) QTY_RTN,  NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_FABRIC_PU_TRN pt, TX_FABRIC_MST i, tx_fabric_pu_mst pm";

                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
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


    private DataTable RemoveAlreadyAddedRow(DataTable dt)
    {
        try
        {
            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtDetailTBL.Rows)
                {
                    dt = RemoveRowFromItemSelectionList(dr1, dt);
                }

            }
            return dt;
        }
        catch
        {
            throw;
        }
    }
    private DataTable RemoveRowFromItemSelectionList(DataRow dr, DataTable dt)
    {
        try
        {
            foreach (DataRow dr1 in dt.Rows)
            {
                int PO_Numb1 = int.Parse(dr["PO_NUMB"].ToString());
                int PO_Numb2 = int.Parse(dr1["PO_NUMB"].ToString());

                if (PO_Numb1 == PO_Numb2)
                {
                    if (dr1["ITEM_CODE"].ToString() == dr["ITEM_CODE"].ToString())
                    {
                        dt.Rows.Remove(dr1);
                        break;
                    }
                }
            }
            dt.AcceptChanges();
            return dt;
        }
        catch
        {
            throw;
        }
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
            string Fabric_Code = arrString[2].ToString();
            GetDataForDetail(PO_Type, PONumb, Fabric_Code);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in party selection.\r\nsee error log for detail.");
        }
    }

    private void GetDataForDetail(string PO_Type, int PONumb, string Fabric_Code)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(Fabric_Code, PONumb, UNIQUEID))
            {
                DataTable dt = null;// SaitexBL.Interface.Method.Fabric_Purchase_Order.GetTRNData_ForReceiving(oUserLoginDetail.DT_STARTDATE.Year, PO_Type, PONumb, Fabric_Code);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtICODE.Text = dt.Rows[0]["FABR_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["FDESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    lblMaxQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    txtBasicRate.Text = float.Parse(dt.Rows[0]["BASIC_RATE"].ToString().Trim()).ToString();
                    txtFinalRate.Text = float.Parse(dt.Rows[0]["FINAL_RATE"].ToString().Trim()).ToString();
                    txtAmount.Text = (float.Parse(dt.Rows[0]["QTY_REM"].ToString()) * float.Parse(dt.Rows[0]["FINAL_RATE"].ToString())).ToString();
                    txtDOM.Text = DateTime.Now.Date.ToShortDateString();
                    lblPO_BRANCH.Text = dt.Rows[0]["BRANCH_CODE"].ToString().Trim();
                    lblPO_COMP.Text = dt.Rows[0]["COMP_CODE"].ToString().Trim();
                    lblPO_TYPE.Text = dt.Rows[0]["PO_TYPE"].ToString().Trim();
                    txtCostCode.Focus();

                }
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Fabric Already Included");
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
            cmbPOITEM.SelectedIndex = -1;
            txtICODE.Text = "";
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtFinalRate.Text = "";
            txtAmount.Text = "";
            txtCostCode.Text = "";
            txtDOM.Text = "";
            txtDetRemarks.Text = "";
            
            lblPO_BRANCH.Text = "";
            lblPO_COMP.Text = "";
            lblPO_TYPE.Text = "";
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
            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                string cString = dv[0]["PO_TYPE"].ToString() + "@" + dv[0]["PO_NUMB"].ToString() + "@" + dv[0]["FABR_CODE"].ToString();
                BindCMBPOITEM(cString, double.Parse(dv[0]["TRN_QTY"].ToString()));

                cmbPOITEM.SelectedText = dv[0]["PO_NUMB"].ToString();
                cmbPOITEM.SelectedValue = cString; 

                foreach (ComboBoxItem item in cmbPOITEM.Items)
                {
                    if (item.Text == dv[0]["PO_NUMB"].ToString().Trim())
                    {
                        cmbPOITEM.SelectedIndex = cmbPOITEM.Items.IndexOf(item);
                        break;
                    }
                }
               // cmbPOITEM.SelectedValue = dv[0]["PO_NUMB"].ToString();
                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                txtICODE.Text = dv[0]["FABR_CODE"].ToString();
                txtDESC.Text = dv[0]["FDESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();
                txtCostCode.Text = dv[0]["COST_CODE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                //dv[0]["QCFLAG"].ToString() == "NO";
                //    chk_QCFlag.Checked = true;

                //else
                //    chk_QCFlag.Checked = false;

                txtDOM.Text = dv[0]["DATE_OF_MFG"].ToString();
                ViewState["UNIQUEID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }
    private void BindCMBPOITEM(string cString, double TRN_QTY)
    {
        try
        {
            DataTable data = new DataTable();
            if (cString.Equals(""))
                data = GetPOData("");
            else
            {
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
                string PO_Type = arrString[0].ToString();
                int PONumb = int.Parse(arrString[1].ToString());
                string Fabric_Code = arrString[2].ToString();
                data = GetPOData("", PONumb, Fabric_Code);

                if (data != null && data.Rows.Count > 0)
                {
                    DataView dv = new DataView(data);
                    dv.RowFilter = "COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND PO_NUMB='" + PONumb + "' AND FABR_CODE='" + Fabric_Code + "' ";
                    if (dv.Count > 0)
                    {
                        lblMaxQTY.Text = (double.Parse(dv[0]["QTY_REM"].ToString()) + TRN_QTY).ToString();
                    }
                }

            }
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "PO_NUMB";
            cmbPOITEM.DataValueField = "po_Fabric_trn";
            cmbPOITEM.DataBind();
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
            DataTable data = GetLOVForGate(e.Text.ToUpper());
            ddlGateEntryNo.Items.Clear();

            ddlGateEntryNo.DataSource = data;
            ddlGateEntryNo.DataTextField = "GATE_NUMB";
            ddlGateEntryNo.DataValueField = "GATE_DATA";
            ddlGateEntryNo.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in Gate Entry Detail selection. see error log for detail.");
        }
    }
    private DataTable GetLOVForGate(string text)
    {
        try
        {
            DataTable data = null;
            if (txtPartyCode.SelectedText != "")
            {
                string CommandText = string.Empty;
                if (lblMode.Text == "Save")
                {
                    CommandText = "SELECT * FROM ( select (a.GATE_DATE||'@'||a.LORRY_NO||'@'||a.DOC_NO||'@'||a.DOC_DATE) GATE_DATA ,a.* from v_tx_gate_mst a where a.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE='" + txtPartyCode.SelectedText.Trim() + "' AND a.ITEM_TYPE='FABRIC IN' and nvl(a.ISSUE_NUMB,0)=0) asd ";
                }
                else if (lblMode.Text == "Update")
                {
                    CommandText = "select * from ( SELECT * FROM (select (a.GATE_DATE||'@'||a.LORRY_NO||'@'||a.DOC_NO||'@'||a.DOC_DATE) GATE_DATA ,a.* from v_tx_gate_mst a where a.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.PRTY_CODE='" + txtPartyCode.SelectedText.Trim() + "' AND a.ITEM_TYPE='FABRIC IN' ) asd where nvl(ISSUE_NUMB,0)='" + txtTRNNUMBer.Text.Trim() + "' or nvl(ISSUE_NUMB,0)=0 ) asd ";
                }
                string WhereClause = " WHERE GATE_NUMB LIKE :SearchQuery or GATE_DATE LIKE :SearchQuery";
                string SortExpression = " ORDER BY GATE_NUMB ";
                string SearchQuery = text + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            }
            else
            {
                CommonFuction.ShowMessage("Please select Party first.");
            }
            return data;
        }
        catch
        {
            throw;
        }
    }
    protected void ddlGateEntryNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = ddlGateEntryNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            DateTime GateDate = DateTime.Parse(arrString[0].ToString());
            string VahicleNo = arrString[1].ToString();
            string PartyChallanNo = arrString[2].ToString();
            DateTime partyChallanDate = DateTime.Parse(arrString[3].ToString());

            GetDataForGateDetail(GateDate, VahicleNo, PartyChallanNo, partyChallanDate);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in gate entry selection. see error log for detail.");
        }
    }
    private void GetDataForGateDetail(DateTime GateDate, string VahicleNo, string PartyChallanNo, DateTime partyChallanDate)
    {
        try
        {
            txtGateEntryDate.Text = GateDate.ToShortDateString();
            txtVehicleNo.Text = VahicleNo;
            txtPartyChallanNo.Text = PartyChallanNo;
            txtPartyChallanDate.Text = partyChallanDate.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    //protected void ddlTRNNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
    //        thisTextBox.SelectedIndex = 0;
    //        thisTextBox.Items.Clear();

    //        DataTable data = new DataTable();
    //        data = GetReceiving(e.Text.ToUpper(), e.ItemsOffset, 10);
    //        thisTextBox.DataSource = data;
    //        thisTextBox.DataTextField = "TRN_NUMB";
    //        thisTextBox.DataValueField = "TRN_NUMB";
    //        thisTextBox.DataBind();

    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    //        e.ItemsCount = data.Rows.Count;

    //    }
    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //        CommonFuction.ShowMessage("Problem in loading Indent for updation. see error log for detail");
    //    }
    //}
    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " where TRN_NUMB like :searchQuery or TRN_DATE like :searchQuery or prty_code like :searchQuery or prty_name like :searchQuery";
            string sortExpression = " order by TRN_NUMB desc, TRN_DATE desc";
            string commandText = "select * from (Select a.TRN_NUMB, a.TRN_DATE,a.PRTY_CODE,b.PRTY_NAME from TX_FABRIC_IR_MST a, tx_vendor_mst b Where a.prty_code=b.prty_code (+) and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + TRN_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    //protected void ddlTRNNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    //{
    //    try
    //    {
    //        int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());

    //        if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
    //            CreateDataTable();
    //        dtDetailTBL.Rows.Clear();
    //        int iRecordFound = GetdataByTRNNUMBer(TRN_NUMBER);
    //        BindGridFromDataTable();
    //        if (iRecordFound > 0)
    //        {
    //            IsUpdateCall = 1;
    //        }
    //        else
    //        {
    //            InitialisePage();
    //            ActivateUpdateMode();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        errorLog.ErrHandler.WriteError(Ex.Message);
    //    }
    //}
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

            spnAInW.InnerText = "Amount In Words... ";
            double dPartyBillAmount = 0;
            if (double.TryParse(txtPartyBillAmount.Text.Trim(), out dPartyBillAmount))
            {
                AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
                // lblPartyBillAmountInWords.Text = oRupeesToWord.changeNumericToWords(dPartyBillAmount);
                spnAInW.InnerText = "Amount In Words... " + oRupeesToWord.changeNumericToWords(dPartyBillAmount);
            }
            else
            {
                txtPartyBillAmount.Text = string.Empty;
                txtPartyBillAmount.Focus();
                CommonFuction.ShowMessage("Invalid Party Bill Amount Entered.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in loading Indent for updation. see error log for detail");
        }
    }
    protected void ddlTRNNumber_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();
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
        }
        catch (Exception Ex)
        {
            errorLog.ErrHandler.WriteError(Ex.Message);
        }
    }
}
