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
public partial class Module_Inventory_Controls_FabricReceiptAgainstRetGatePass : System.Web.UI.UserControl
{
    private DataTable dtTRN_SUB = null;
    private DataTable dtInvoiceDicRateMST = null;
    private DataTable dtDicRate = null;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    private void InitialisePage()
    {
        try
        {
            TRN_TYPE = "RFS04";

            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();
            BindCostCode();
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }
           
            Session["dtInvoiceDicRateMST"] = null;
            Session["dtDicRate"] = null;
            Session["dtTRN_SUB"] = null;
            Session["dtFabricReceipt"] = null;
            ViewState["dtDetailTBL"] = null;
            rvbill.MinimumValue = StartDate.ToShortDateString();
            rvbill.MaximumValue = EndDate.ToShortDateString();

            rvlr.MinimumValue = StartDate.ToShortDateString();
            rvlr.MaximumValue = EndDate.ToShortDateString();

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
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
            dtDetailTBL.Columns.Add("TRN_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("TRN_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("FABR_CODE", typeof(string));
            dtDetailTBL.Columns.Add("FABR_DESC", typeof(string));
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
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            //ViewState["dtDetailTBL"] = dtDetailTBL;
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            if (dtDetailTBL == null)
                InsertBlankRowInTable();

            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();
            CalculateTotalAmount();
        }
        catch
        {
            throw;
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
            txtPartyBillAmount.Text = TotalAmount.ToString();
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
                //InsertBlankRowInTable();
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
                        string sFABR_CODE = dr["FABR_CODE"].ToString();
                        deleteItemReceiptSUBTRNRow(sPO_COMP_CODE, sPO_BRANCH, sPO_TYPE, iPO_NUMB, sFABR_CODE);
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
                ViewState["dtDetailTBL"] = dtDetailTBL;
            }
        }
        catch
        {
            throw;
        }
    }
    private void deleteItemReceiptSUBTRNRow(string PO_COMP_CODE, string PO_BRANCH, string PO_TYPE, int PO_NUMB, string FABR_CODE)
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
                    string sFABR_CODE = row["FABR_CODE"].ToString();

                    if (sPO_COMP_CODE == PO_COMP_CODE && sPO_BRANCH == PO_BRANCH && sPO_TYPE == PO_TYPE && iPO_NUMB == PO_NUMB && sFABR_CODE == FABR_CODE)
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for detail."));
        }
    }
    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            bool ReturnResult = false;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
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
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading update mode.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in cleae page data.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/Fabric/FabricSaleWork/Reports/FabricReciptPremRpt.aspx?TRN_TYPE=" + TRN_TYPE, false);
            //Response.Redirect("~/Module/Inventory/Reports/ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting print.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing.\r\nSee error log for detail."));
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
            if (ViewState["TRN_TYPE"] != null)
               TRN_TYPE = (string)ViewState["TRN_TYPE"];

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
            oTX_FABRIC_IR_MST.LOT_ID_NO = "NA";
            oTX_FABRIC_IR_MST.ST_COMP_CODE = "NA";
            oTX_FABRIC_IR_MST.ST_BRANCH_CODE = "NA";
            oTX_FABRIC_IR_MST.ST_TRN_TYPE = "NA";
            oTX_FABRIC_IR_MST.ST_TRN_NUMB = 0;
            oTX_FABRIC_IR_MST.CONSIGNEE_CODE = "NA";
            oTX_FABRIC_IR_MST.REC_BRANCH_CODE = "NA";
            oTX_FABRIC_IR_MST.TO_DEPT_CODE = "NA";
            int TRN_NUMB = 0;
           // DataTable dtFabricReceipt = GetReceiptIssueAdjDataTable(int.Parse(txtTrnNumb.Text.Trim()));
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.InsertFabricReceive(oTX_FABRIC_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, dtDicRate, dtInvoiceDicRateMST);
                //Insert(oTX_FABRIC_IR_MST, out TRN_NUMB, dtDetailTBL, dtFabricReceipt, htReceive);
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
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

          
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
           // DataTable dtFabricReceipt = GetReceiptIssueAdjDataTable(int.Parse(ddlTRNNumber.SelectedValue.ToString()));
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.UpdateFabricReceive(oTX_FABRIC_IR_MST,dtDetailTBL,htReceive,dtTRN_SUB,dtDicRate,dtInvoiceDicRateMST);
               // Update(oTX_FABRIC_IR_MST, dtDetailTBL, dtFabricReceipt, htReceive);
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
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

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
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                dtTRN_SUB = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    //TX_ITEM_IR_MST.GetSubTrnDetailByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtTRN_SUB.Rows.Count > 0)
                {
                    MapDataTableSubTRNDetail(dtTRN_SUB);
                }
                dtDetailTBL.Rows.Clear();
               
                dtDetailTBL = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetTRN_DataByTRN_NUMBAgnstGatePass(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                if (dtDetailTBL.Rows.Count > 0)
                {
                    ViewState["dtDetailTBL"] = dtDetailTBL;
                    MapDataTable();
                    BindGridFromDataTable();
                    DataTable dtDisTaxMstTemp = SaitexDL.Interface.Method.TX_FABRIC_IR_MST.GetDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                        
                       // .TX_ITEM_IR_MST.GetDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    if (dtDisTaxMstTemp.Rows.Count > 0)
                    {
                        MapDataTableDisTaxMST(dtDisTaxMstTemp);
                    }

                    // code to get dis taxes for transaction entry 
                    DataTable dtDisTaxTrnTemp = SaitexDL.Interface.Method.TX_FABRIC_IR_MST.GetTrnDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                       // TX_ITEM_IR_MST.GetTrnDisTaxesDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                    if (dtDisTaxTrnTemp.Rows.Count > 0)
                    {
                        MapDataTableDisTaxTrn(dtDisTaxTrnTemp);
                    }
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Fabric Detail.";
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
        catch
        {
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
            else
            {
 
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

           // if (!dtDetailTBL.Columns.Contains("TRN_TYPE"))
               // dtDetailTBL.Columns.Add("TRN_TYPE", typeof(string));
          //  if (!dtDetailTBL.Columns.Contains("NO_OF_UNIT"))
           // {
              //  dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
           // }
          //  if(!dtDetailTBL.Columns.Contains("UOM_OF_UNIT"))
             //   dtDetailTBL.Columns.Add("UOM_OF_UNIT",typeof(string));
          //  if (!dtDetailTBL.Columns.Contains("WEIGHT_OF_UNIT"))
            //    dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
           // if (!dtDetailTBL.Columns.Contains("PI_NO"))
             //   dtDetailTBL.Columns.Add("PI_NO", typeof(string));
           // if (!dtDetailTBL.Columns.Contains("SHADE_CODE"))
              //  dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            if (!dtDetailTBL.Columns.Contains("MAC_CODE"))
               dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                //dtDetailTBL.Rows[iLoop]["TRN_TYPE"] = string.Empty;
                //dtDetailTBL.Rows[iLoop]["NO_OF_UNIT"] = 0f;
               // dtDetailTBL.Rows[iLoop]["UOM_OF_UNIT"] = string.Empty;
                dtDetailTBL.Rows[iLoop]["MAC_CODE"] = string.Empty;
               // dtDetailTBL.Rows[iLoop]["WEIGHT_OF_UNIT"] = 0f;
               // dtDetailTBL.Rows[iLoop]["PI_NO"] = string.Empty;
               // dtDetailTBL.Rows[iLoop]["SHADE_CODE"]=string.Empty;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
        }
    }
    private DataTable GetLOVForParty(string Text)
    {
        try
        {
            string CommandText = "SELECT DISTINCT * FROM (SELECT * FROM (SELECT DISTINCT v.PRTY_CODE, TRN_TYPE, PRTY_STATE, PRTY_ADD1, PRTY_ADD2, v.PRTY_NAME,PRTY_ADD1 || ',' || NVL (PRTY_ADD2, ' ') || ',' || NVL (PRTY_STATE, ' ')address FROM TX_VENDOR_MST v RIGHT OUTER JOIN tx_fabric_ir_mst p ON V.PRTY_CODE = P.PRTY_CODE) a WHERE TRN_TYPE = 'IFS05') b ";
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
        }
    }
    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (dtDetailTBL == null)
            {
                CreateDataTable();
            }
            if (dtDetailTBL.Rows.Count < 15)
            {
                if (cmbPOITEM.SelectedText != "" && txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "")
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

                            double subQty = 0;
                            if (Session["dtTRN_SUB"] != null)
                            {
                                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                                //subQty = (from DataRow data in dtTRN_SUB.Rows
                                //          where data.Field<string>("FABR_CODE") == txtICODE.Text.Trim() && data.Field<int>("PO_NUMB") == int.Parse(txtPONumb.Text.Trim())
                                //          select data).Sum(p => p.Field<double>("TRN_QTY"));

                            }
                         

                                double MaxQty = 0;
                                double.TryParse(lblMaxQTY.Text.Trim(), out MaxQty);
                                //double MaxQty = 0;
                                //double.TryParse(lblMaxQTY.Text.Trim(), out MaxQty);
                                if (Qty > MaxQty)
                                {
                                    txtQTY.Text = MaxQty.ToString();
                                    CommonFuction.ShowMessage(@"Entered Quantity is larger than po Quantity.\r\nYou can receive maximum " + MaxQty + " quantity for this item of selected po.");
                                    txtQTY.Focus();
                                }
                               // else
                               // {
                                    if (UNIQUEID > 0)
                                    {
                                        DataView dv = new DataView(dtDetailTBL);
                                        dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                                        if (dv.Count > 0)
                                        {
                                            dv[0]["TRN_NUMB"] = int.Parse(txtPONumb.Text);

                                            dv[0]["TRN_TYPE"] = TRN_TYPE;
                                            dv[0]["PO_NUMB"] = int.Parse(txtPONumb.Text);
                                            dv[0]["PO_TYPE"] = lblPO_TYPE.Text;
                                            dv[0]["PO_COMP_CODE"] = lblPO_COMP.Text ;
                                            dv[0]["PO_BRANCH"] = lblPO_BRANCH.Text;
                                            dv[0]["FABR_CODE"] = txtICODE.Text.Trim();
                                            dv[0]["FABR_DESC"] = txtDESC.Text.Trim();
                                            dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                            dv[0]["UOM"] = txtUNIT.Text.Trim();
                                            dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                            dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                            dv[0]["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                            dv[0]["COST_CODE"] = ddlCostCode.SelectedValue.Trim();
                                            dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();

                                            dv[0]["NO_OF_UNIT"] = 0f;

                                            dv[0]["UOM_OF_UNIT"] = txtUNIT.Text;
                                            //dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;

                                            dv[0]["WEIGHT_OF_UNIT"] = 0f;

                                            dv[0]["PI_NO"] = "NA";
                                            if (cmbShade.SelectedValue.ToString() == null || cmbShade.SelectedValue.ToString() == string.Empty || cmbShade.SelectedValue.ToString() == "")
                                            {
                                                dv[0]["SHADE_CODE"] = "NA";
                                            }
                                            else
                                            {
                                                dv[0]["SHADE_CODE"] = cmbShade.SelectedItem.ToString();
                                            }
                                           dv[0]["QCFLAG"] = "Yes";
                                            //if (chk_QCFlag.Checked)
                                            //    dv[0]["QCFLAG"] = "Yes";
                                            //else
                                            //    dv[0]["QCFLAG"] = "No";

                                            DateTime dd = System.DateTime.Now;
                                            DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                            dv[0]["DATE_OF_MFG"] = dd;

                                            dtDetailTBL.AcceptChanges();

                                            createdatatableforadjustment(cmbPOITEM.SelectedText.Trim(), lblPO_TYPE.Text, double.Parse(txtQTY.Text.Trim()), txtICODE.Text.Trim(), double.Parse(txtFinalRate.Text.Trim()), UNIQUEID);
                                        }
                                    }
                                    else
                                    {
                                        DataRow dr = dtDetailTBL.NewRow();
                                        dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                        dr["TRN_NUMB"] = int.Parse(txtPONumb.Text.Trim());
                                        dr["TRN_TYPE"] = TRN_TYPE;
                                        dr["PO_NUMB"] = int.Parse(txtPONumb.Text.Trim()); 
                                        dr["PO_TYPE"] = lblPO_TYPE.Text;
                                        dr["PO_COMP_CODE"] = lblPO_COMP.Text;
                                        dr["PO_BRANCH"] = lblPO_BRANCH.Text;
                                        dr["FABR_CODE"] = txtICODE.Text.Trim();
                                        dr["FABR_DESC"] = txtDESC.Text.Trim();
                                        dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                        dr["UOM"] = txtUNIT.Text.Trim();
                                        dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                        dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                        dr["AMOUNT"] = double.Parse(txtAmount.Text.Trim());
                                        dr["COST_CODE"] = ddlCostCode.SelectedValue.Trim();
                                        dr["MAC_CODE"] = string.Empty;
                                        dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                        //if (chk_QCFlag.Checked)
                                        //    dr["QCFLAG"] = "Yes";
                                        //else
                                        //    dr["QCFLAG"] = "No";
                                        dr["NO_OF_UNIT"] = 0f;

                                        dr["UOM_OF_UNIT"] = txtUNIT.Text;
                                        //dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                        dr["QCFLAG"] = "Yes";
                                        dr["WEIGHT_OF_UNIT"] = 0f;

                                        dr["PI_NO"] = "NA";
                                        if (cmbShade.SelectedValue.ToString() == null || cmbShade.SelectedValue.ToString() == string.Empty || cmbShade.SelectedValue.ToString() == "")
                                        {
                                            dr["SHADE_CODE"] = "NA";
                                        }
                                        else
                                        {
                                            dr["SHADE_CODE"] = cmbShade.SelectedItem.ToString();
                                        }
                                        DateTime dd = System.DateTime.Now;
                                        DateTime.TryParse(txtDOM.Text.Trim(), out dd);
                                        dr["DATE_OF_MFG"] = dd;

                                        dtDetailTBL.Rows.Add(dr);

                                        createdatatableforadjustment(cmbPOITEM.SelectedText.Trim(), lblPO_TYPE.Text, double.Parse(txtQTY.Text.Trim()), txtICODE.Text.Trim(), double.Parse(txtFinalRate.Text.Trim()), UNIQUEID);
                                    }
                                    RefreshDetailRow();

                                
                          
                        
                        } else
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding transaction data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in canceling transaction.\r\nSee error log for detail."));
        }
    }
    protected void cmbPOITEM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPOData(e.Text);
            cmbPOITEM.Items.Clear();
            cmbPOITEM.DataSource = data;
            cmbPOITEM.DataTextField = "TRN_NUMB";
            cmbPOITEM.DataValueField = "po_Fabric_trn";
            cmbPOITEM.DataBind();
            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading transaction data.\r\nSee error log for detail."));
        }
    }
    protected DataTable GetPOData(string text)
    {
        try
        {
            DataTable dt = new DataTable();
            if (txtPartyCode.SelectedText != "")
            {
                string po_type = "IFS05";
                string whereClause = " WHERE pm.conf_flag='1' and PM.GATE_PASS_TYPE = '1' AND PM.TRN_TYPE = '" + po_type + "' AND (NVL (PT.TRN_QTY, 0) - NVL (PT.ISS_QTY, 0)) > 0 AND pt.FABR_CODE = i.FABR_CODE AND PT.TRN_TYPE = PM.TRN_TYPE AND pm.TRN_NUMB = PT.TRN_NUMB AND PM.PRTY_CODE = '" + txtPartyCode.SelectedText.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.FABR_CODE LIKE :SearchQuery";
                string sortExpression = " ORDER BY PT.TRN_NUMB ASC, PT.FABR_CODE ASC ";
                string commandText = "SELECT DISTINCT (PT.TRN_TYPE || '@' || PT.TRN_NUMB || '@' || PT.FABR_CODE) po_fabric_trn, pt.TRN_NUMB, pt.FABR_CODE, pt.TRN_QTY, pt.UOM, PM.PRTY_CODE, pt.FINAL_RATE BASIC_RATE, pt.FINAL_RATE, i.FABR_DESC, NVL (PT.ISS_QTY, 0) QTY_RCPT, NVL (PT.TRN_QTY, 0) - NVL (PT.ISS_QTY, 0) AS QTY_REM FROM TX_FABRIC_IR_TRN pt, TX_FABRIC_MST i, TX_FABRIC_IR_MST PM ";

                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
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
    protected DataTable GetPOData(string text, int PO_Numb, string Fabric_Code)
    {
        try
        {
            DataTable dt = new DataTable();
            if (txtPartyCode.SelectedText != "")
            {
                string po_type = "IFS05";
                string whereClause = " WHERE (pm.conf_flag='1' and PM.GATE_PASS_TYPE = '1' AND PM.TRN_TYPE = '" + po_type + "' AND (NVL (PT.TRN_QTY, 0) - NVL (PT.ISS_QTY, 0)) > 0 AND pt.FABR_CODE = i.FABR_CODE AND PT.TRN_TYPE = PM.TRN_TYPE AND pm.TRN_NUMB = PT.TRN_NUMB AND PM.PRTY_CODE = '" + txtPartyCode.SelectedText.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.FABR_CODE LIKE :SearchQuery) OR (pm.conf_flag='1' and PM.GATE_PASS_TYPE = '1' AND pt.FABR_CODE = i.FABR_CODE AND pm.TRN_NUMB = PT.TRN_NUMB AND PT.TRN_NUMB = '" + PO_Numb + "' AND PT.TRN_TYPE = '" + po_type + "' AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PT.FABR_CODE = '" + Fabric_Code + "' )";
                string sortExpression = " ORDER BY TRN_NUMB";
                string commandText = " SELECT DISTINCT (PT.TRN_TYPE || '@' || PT.TRN_NUMB || '@' || PT.FABR_CODE) po_Fabric_trn, pt.COMP_CODE, pt.BRANCH_CODE, pt.TRN_NUMB, pt.FABR_CODE,pt.TRN_QTY,pt.PO_NUMB, i.FABR_DESC, NVL (PT.ISS_QTY, 0) QTY_RCPT, NVL (PT.ISS_QTY, 0) QTY_RTN, NVL (PT.TRN_QTY, 0) - NVL (PT.ISS_QTY, 0) AS QTY_REM FROM TX_FABRIC_IR_TRN pt, TX_FABRIC_MST i, TX_FABRIC_IR_MST pm ";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
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
                    if (dr1["FABR_CODE"].ToString() == dr["FABR_CODE"].ToString())
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
             txtPONumb.Text = PONumb.ToString();
            string Fabr_Code = arrString[2].ToString();
            GetDataForDetail(PO_Type, PONumb, Fabr_Code);
            BindShadeByFabrCode(Fabr_Code.Trim());  
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading transaction data.\r\nSee error log for detail."));
        }
    }
    private void BindShadeByFabrCode(string FABR_CODE)
    {
        try
        {
            cmbShade.Items.Clear();
            List<string> dtShade = SaitexDL.Interface.Method.TX_FABRIC_MST.GetShadeDataByFabrCode(FABR_CODE);
            if (dtShade != null && dtShade.Count > 0)
            {
                cmbShade.DataSource = dtShade;

                cmbShade.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    private void BindCostCode()
    {
        try
        {
            ddlCostCode.Items.Clear();
            ddlCostCode.Items.Add(new ListItem("Select", "NA"));

            DataTable dtCostCode = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("COST_CODE", oUserLoginDetail.COMP_CODE);
            if (dtCostCode != null && dtCostCode.Rows.Count > 0)
            {
                ddlCostCode.DataSource = dtCostCode;
                ddlCostCode.DataTextField = "MST_CODE";
                ddlCostCode.DataValueField = "MST_CODE";
                ddlCostCode.DataBind();
            }
            else
            {
                ListItem Li = new ListItem();
                Li.Value = "NA";
                Li.Text = "NA";
                cmbShade.Items.Add(Li);
            }
        }
        catch
        {
            throw;
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
                DataTable dt =  SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetTRNData_ForReceivingAgainstReturn( PO_Type, PONumb, Fabric_Code);

                if (dt != null && dt.Rows.Count > 0)
                {
                   
                    txtICODE.Text = dt.Rows[0]["FABR_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["FABR_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    lblMaxQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    txtBasicRate.Text = float.Parse(dt.Rows[0]["BASIC_RATE"].ToString().Trim()).ToString();
                    txtFinalRate.Text = float.Parse(dt.Rows[0]["FINAL_RATE"].ToString().Trim()).ToString();
                    txtAmount.Text = (float.Parse(dt.Rows[0]["QTY_REM"].ToString()) * float.Parse(dt.Rows[0]["FINAL_RATE"].ToString())).ToString();
                    txtDOM.Text = DateTime.Now.Date.ToShortDateString();
                    lblPO_BRANCH.Text = dt.Rows[0]["BRANCH_CODE"].ToString().Trim();
                    lblPO_COMP.Text = dt.Rows[0]["COMP_CODE"].ToString().Trim();
                    //lblPO_YEAR.Text = dt.Rows[0]["YEAR"].ToString().Trim();
                    lblPO_TYPE.Text = dt.Rows[0]["PO_TYPE"].ToString().Trim();

                    //DataTable dtdistax=SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetTRNData_ForReceivingDisTaxes(
                    DataTable dtdistax = SaitexDL.Interface.Method.TX_FABRIC_IR_MST.GetTRNData_ForReceivingDisTaxes1( lblPO_COMP.Text.Trim(), lblPO_BRANCH.Text.Trim(), lblPO_TYPE.Text, int.Parse(txtPONumb.Text), txtICODE.Text);
                   //SaitexBL.Interface.Method.Material_Purchase_Order.GetTRNData_ForReceivingDisTaxes(int.Parse(lblPO_YEAR.Text), lblPO_COMP.Text.Trim(), lblPO_BRANCH.Text.Trim(), lblPO_TYPE.Text, int.Parse(txtPONumb.Text), Item_Code);
                    mapDataTableForDisTaxes(dtdistax);
                    //txtCostCode.Focus();

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
           // txtpoRate.Text = dFinalRate.ToString();
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
            txtICODE.Text = "";
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtFinalRate.Text = "";
            txtAmount.Text = "";
            ddlCostCode.SelectedIndex = -1;
            txtDOM.Text = "";
            txtDetRemarks.Text = "";
            txtNoOfUnit.Text = "";
            txtWeightofUnit.Text = "";
            cmbShade.Items.Clear();  
            //chk_QCFlag.Checked = false;
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                string cString = dv[0]["PO_TYPE"].ToString() + "@" + dv[0]["PO_NUMB"].ToString() + "@" + dv[0]["FABR_CODE"].ToString();
                BindCMBPOITEM(cString, double.Parse(dv[0]["TRN_QTY"].ToString()));

                cmbPOITEM.SelectedText = dv[0]["TRN_NUMB"].ToString();
                cmbPOITEM.SelectedValue = cString;

                foreach (ComboBoxItem item in cmbPOITEM.Items)
                {
                    if (item.Text == dv[0]["TRN_NUMB"].ToString().Trim())
                    {
                        cmbPOITEM.SelectedIndex = cmbPOITEM.Items.IndexOf(item);
                        break;
                    }
                }
                txtPONumb.Text = dv[0]["TRN_NUMB"].ToString();
                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                //lblPO_YEAR.Text = dv[0]["PO_YEAR"].ToString();
                txtICODE.Text = dv[0]["FABR_CODE"].ToString();
                txtDESC.Text = dv[0]["FABR_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                //lblMaxQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["AMOUNT"].ToString();

                ddlCostCode.SelectedIndex = ddlCostCode.Items.IndexOf(ddlCostCode.Items.FindByValue(dv[0]["COST_CODE"].ToString()));
                BindShadeByFabrCode(dv[0]["FABR_CODE"].ToString());
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                //dv[0]["QCFLAG"].ToString() == "NO";
                //    chk_QCFlag.Checked = true;

                //else
                //    chk_QCFlag.Checked = false;

                txtDOM.Text = dv[0]["DATE_OF_MFG"].ToString();
                ViewState["UNIQUEID"] = UNIQUEID;
                DataTable dt = SaitexDL.Interface.Method.TX_FABRIC_IR_MST.GetTRNData_ForReceiving(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, dv[0]["PO_TYPE"].ToString(), int.Parse(dv[0]["PO_NUMB"].ToString()), dv[0]["FABR_CODE"].ToString());
                    //SaitexBL.Interface.Method.Material_Purchase_Order.GetTRNData_ForReceiving(int.Parse(lblPO_YEAR.Text), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, dv[0]["PO_TYPE"].ToString(), int.Parse(dv[0]["PO_NUMB"].ToString()), dv[0]["ITEM_CODE"].ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblMaxQTY.Text = (double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()) + double.Parse(dv[0]["TRN_QTY"].ToString())).ToString();
                }
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
            cmbPOITEM.DataTextField = "TRN_NUMB";
            cmbPOITEM.DataValueField = "po_Fabric_trn";
            cmbPOITEM.DataBind();
        }
        catch
        {
            throw;
        }
    }
    //private void EditItemReceiptRow(int UNIQUEID)
    //{
    //    try
    //    {
    //        DataView dv = new DataView(dtDetailTBL);
    //        dv.RowFilter = "UNIQUEID=" + UNIQUEID;
    //        if (dv.Count > 0)
    //        {
    //            string cString = dv[0]["PO_TYPE"].ToString() + "@" + dv[0]["PO_NUMB"].ToString() + "@" + dv[0]["FABR_CODE"].ToString();
    //            BindCMBPOITEM(cString, double.Parse(dv[0]["TRN_QTY"].ToString()));

    //            cmbPOITEM.SelectedText = dv[0]["PO_NUMB"].ToString();
    //            cmbPOITEM.SelectedValue = cString;

    //            foreach (ComboBoxItem item in cmbPOITEM.Items)
    //            {
    //                if (item.Text == dv[0]["PO_NUMB"].ToString().Trim())
    //                {
    //                    cmbPOITEM.SelectedIndex = cmbPOITEM.Items.IndexOf(item);
    //                    break;
    //                }
    //            }

    //            lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
    //            lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
    //            lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
    //            txtICODE.Text = dv[0]["FABR_CODE"].ToString();
    //            txtDESC.Text = dv[0]["FDESC"].ToString();
    //            txtQTY.Text = dv[0]["TRN_QTY"].ToString();
    //            txtUNIT.Text = dv[0]["UOM"].ToString();
    //            txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
    //            txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
    //            txtAmount.Text = dv[0]["AMOUNT"].ToString();
    //            txtCostCode.Text = dv[0]["COST_CODE"].ToString();
    //            txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
    //            //if (dv[0]["QCFLAG"].ToString() == "Yes")
    //               // chk_QCFlag.Checked = true;

    //            //else
    //            //    chk_QCFlag.Checked = false;

    //            txtDOM.Text = dv[0]["DATE_OF_MFG"].ToString();
    //            ViewState["UNIQUEID"] = UNIQUEID;
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //private void BindCMBPOITEM(string cString, double TRN_QTY)
    //{
    //    try
    //    {
    //        DataTable data = new DataTable();
    //        if (cString.Equals(""))
    //            data = GetPOData("");
    //        else
    //        {
    //            char[] splitter = { '@' };
    //            string[] arrString = cString.Split(splitter);
    //            string PO_Type = arrString[0].ToString();
    //            int PONumb = int.Parse(arrString[1].ToString());
    //            string Fabr_Code = arrString[2].ToString();
    //            data = GetPOData("", PONumb, Fabr_Code);

    //            if (data != null && data.Rows.Count > 0)
    //            {
    //                DataView dv = new DataView(data);
    //                dv.RowFilter = "COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND TRN_NUMB='" + PONumb + "' AND FABR_CODE='" + Fabr_Code + "' ";
    //                if (dv.Count > 0)
    //                {
    //                    lblMaxQTY.Text = (double.Parse(dv[0]["QTY_REM"].ToString()) + TRN_QTY).ToString();
    //                }
    //            }

    //        }
    //        cmbPOITEM.Items.Clear();
    //        cmbPOITEM.DataSource = data;
    //        cmbPOITEM.DataTextField = "TRN_NUMB";
    //        cmbPOITEM.DataValueField = "po_Fabric_trn";
    //        cmbPOITEM.DataBind();
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in gate entry detail selection.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in gate entry detail selection.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading data for updation.\r\nSee error log for detail."));
        }
    }
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
    protected void ddlTRNNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            int TRN_NUMBER = int.Parse(ddlTRNNumber.SelectedValue.Trim());
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
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
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading data for updation.\r\nSee error log for detail."));
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
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting bill amount.\r\nSee error log for detail."));
        }
    }
    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("YEAR", typeof(int));
        dt.Columns.Add("COMP_CODE", typeof(string));
        dt.Columns.Add("BRANCH_CODE", typeof(string));
        dt.Columns.Add("TRN_TYPE", typeof(string));
        dt.Columns.Add("TRN_NUMB", typeof(int));
        dt.Columns.Add("PO_COMP", typeof(string));
        dt.Columns.Add("PO_BRANCH", typeof(string));
        dt.Columns.Add("PO_TYPE", typeof(string));
        dt.Columns.Add("PO_NUMB", typeof(int));
        dt.Columns.Add("FABR_CODE", typeof(string));
        dt.Columns.Add("ISSUE_QTY", typeof(double));
        dt.Columns.Add("FINAL_RATE", typeof(double));
        dt.Columns.Add("ISS_YEAR", typeof(int));
        dt.Columns.Add("ISS_COMP", typeof(string));
        dt.Columns.Add("ISS_BRANCH", typeof(string));
        dt.Columns.Add("ISS_TRN_TYPE", typeof(string));
        dt.Columns.Add("ISS_TRN_NUMB", typeof(int));
        dt.Columns.Add("ISS_PO_COMP", typeof(string));
        dt.Columns.Add("ISS_PO_BRNCH", typeof(string));
        dt.Columns.Add("ISS_PO_TYPE", typeof(string));
        dt.Columns.Add("ISS_PO_NUMB", typeof(int));
        dt.Columns.Add("ISS_PI_NO", typeof(string));
        return dt;
    }
    private DataTable GetReceiptIssueAdjDataTable(int ISSUE_NUMB)
    {
        try
        {
            DataTable dtAdj = createAdjTable();
            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {

                foreach (DataRow drTRN in dtDetailTBL.Rows)
                {
                    DataRow drAdj = dtAdj.NewRow();
                    drAdj["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                    drAdj["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    drAdj["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    //drAdj["TRN_TYPE"] = RECEIPT_TYPE;
                    //drAdj["TRN_NUMB"] = RECEIPT_NUMB;
                    drAdj["TRN_TYPE"] = TRN_TYPE;
                    drAdj["TRN_NUMB"] = ISSUE_NUMB;
                    drAdj["PO_COMP"] = drTRN["PO_COMP_CODE"];
                    drAdj["PO_BRANCH"] = drTRN["PO_BRANCH"];
                    drAdj["PO_TYPE"] = drTRN["PO_TYPE"];
                    drAdj["PO_NUMB"] = drTRN["PO_NUMB"];
                    drAdj["FABR_CODE"] = drTRN["FABR_CODE"];
                    drAdj["ISSUE_QTY"] = drTRN["TRN_QTY"];
                    drAdj["FINAL_RATE"] = drTRN["FINAL_RATE"];
                    drAdj["LOT_NO"] = "NA";
                    drAdj["NO_OF_UNIT"] = 0;
                    drAdj["UOM_OF_UNIT"] = "NA";
                    drAdj["WEIGHT_OF_UNIT"] = 0;
                    drAdj["PI_NO"] = "NA";
                    drAdj["SHADE_CODE"] = "NA";
                    drAdj["PO_RATE"] = 0;
                    drAdj["ISS_PI_NO"] = "NA";
                    dtAdj.Rows.Add(drAdj);
                }
            }
            return dtAdj;
        }
        catch
        {
            throw;
        }
    }
    private void createdatatableforadjustment(string RET_NUMB, string RET_TYPE, double Adjustqty, string FABR_CODE, double FINAL_RATE, int UNIQUEID)
    {
        try
        {
            DataTable dtFabricReceipt = new DataTable();
            if (Session["dtFabricReceipt"] == null)
                dtFabricReceipt = createAdjTable();
            else
            {
                dtFabricReceipt = (DataTable)Session["dtFabricReceipt"];
            }

            if (Adjustqty > 0)
            {
                if (UNIQUEID > 0)
                {
                    DataView dvFabricRec = new DataView(dtFabricReceipt);
                    dvFabricRec.RowFilter = "YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'  AND COMP_CODE='" + oUserLoginDetail.COMP_CODE + "'  AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "'  AND TRN_TYPE='" + RET_TYPE + "'  AND TRN_NUMB='" + RET_NUMB + "'  AND FABR_CODE='" + FABR_CODE + "'";
                    if (dvFabricRec.Count > 0)
                    {
                        dvFabricRec[0]["ISSUE_QTY"] = Adjustqty;
                        dvFabricRec[0]["FINAL_RATE"] = FINAL_RATE;
                        dtFabricReceipt.AcceptChanges();
                    }
                }
                else
                {
                    DataRow dr = dtFabricReceipt.NewRow();
                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["TRN_TYPE"] = RET_TYPE;
                    dr["TRN_NUMB"] = int.Parse(RET_NUMB);
                    dr["PO_COMP"] = "C99999";
                    dr["PO_BRANCH"] = "B99999";
                    dr["PO_TYPE"] = "MII";
                    dr["PO_NUMB"] = 999998;
                    dr["FABR_CODE"] = FABR_CODE;
                    dr["ISSUE_QTY"] = Adjustqty;
                    dr["FINAL_RATE"] = FINAL_RATE;
                    dtFabricReceipt.Rows.Add(dr);
                }
            }
            Session["dtFabricReceipt"] = dtFabricReceipt;
        }
        catch
        {
            throw;
        }
    }
    protected void btnSubDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPONumb.Text != string.Empty)
            {
                txtNoOfUnit.ReadOnly = false;
                 txtWeightofUnit.ReadOnly = false;
                txtQTY.ReadOnly = false;

                string URL = "TX_FABRIC_TRN_SUB.aspx";
                URL = URL + "?FABR_CODE=" + txtICODE.Text;
                URL = URL + "&PO_NUMB=" + txtPONumb.Text;
                URL = URL + "&PO_TYPE=" + lblPO_TYPE.Text;
                URL = URL + "&txtShadeCode=" + cmbShade.SelectedItem.ToString();
                URL = URL + "&PO_COMP_CODE=" + lblPO_COMP.Text;
                URL = URL + "&PO_BRANCH=" + lblPO_BRANCH.Text;
                URL = URL + "&lblMaxQTY=" + txtQTY.Text;
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
              txtWeightofUnit.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Weight Of Unit.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }
}
