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
using System.Collections.Generic; 
public partial class Module_Fabric_FabricSaleWork_Controls_Fabric_Receipt_Misc : System.Web.UI.UserControl
{
    private DataTable dtTRN_SUB = null;
    private static DateTime StartDate;
    private static DateTime EndDate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private string TRN_TYPE = "";
    private static int IsUpdateCall = 2;
   
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
           
            TRN_TYPE = "RFS03";
            lblPO_TYPE.Text = "MII";
            lblPO_BRANCH.Text = "B99996";
            lblPO_COMP.Text = "C99996";
            lblPO_NUMB.Text = "999996";
            ViewState["TRN_TYPE"] = TRN_TYPE;
            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();
            //BindShadeCode();

            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }

            rvbill.MinimumValue = StartDate.ToShortDateString();
            rvbill.MaximumValue = EndDate.ToShortDateString();

            rvlr.MinimumValue = StartDate.ToShortDateString();
            rvlr.MaximumValue = EndDate.ToShortDateString();

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();

            Session["dtTRN_SUB"] = null;
            ViewState["dtDetailTBL"] = null;
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
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

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

            ddlGateEntryNo.SelectedIndex = -1;
            txtGateEntryNo.Text = "";
            txtRemarks.Text = "";
            txtTransporterAddress.Text = "";

            txtVehicleNo.Text = "";
            lblPartyCode.Text = string.Empty;
            lblTransporterCode.Text = string.Empty;

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

            tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            txtGateEntryNo.Text = "";
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
            dtDetailTBL.Columns.Add("FABR_DESC", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
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
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));

            ViewState["dtDetailTBL"] = dtDetailTBL;
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

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

            lblMode.Text = "Update";
            txtTRNNUMBer.Text = "";
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Update Mode.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Data Page.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Page Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    if (ViewState["TRN_TYPE"] != null)
        //        TRN_TYPE = (string)ViewState["TRN_TYPE"];

        //    Response.Redirect("~/Module/Yarn/SalesWork/Reports/YRN_ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
        //}
        //catch (Exception ex)
        //{
        //    CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
        //    lblMode.Text = ex.ToString();
        //}
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Editing/ Deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int UNIQUEID)
    {
        bool Result = false;
        try
        {
            string Shade = cmbShade.SelectedItem.ToString();
            if (Shade == string.Empty || Shade == "" || Shade == null)
            {
                Shade = "NA";
            }

            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                    Label txtSHADE = (Label)grdRow.FindControl("txtSHADE");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (txtICODE.Text.Trim() == ItemCode && UNIQUEID != iUNIQUEID && Shade == txtSHADE.Text.Trim())
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

            oTX_FABRIC_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
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
            oTX_FABRIC_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
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


            oTX_FABRIC_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
            int TRN_NUMB = 0;
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.InsertFabricReceive(oTX_FABRIC_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, null, null);     
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

            oTX_FABRIC_IR_MST.GATE_NUMB = CommonFuction.funFixQuotes(txtGateEntryNo.Text.Trim());
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
            oTX_FABRIC_IR_MST.PRTY_CODE = CommonFuction.funFixQuotes(lblPartyCode.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
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

            oTX_FABRIC_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());

            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.UpdateFabricReceive(oTX_FABRIC_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB, null, null);    
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Data updated Successfully");
            }
            else
            {
                CommonFuction.ShowMessage("Data Updation Failed");
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


                dd = System.DateTime.Now.Date;
                if (DateTime.TryParse(dt.Rows[0]["PRTY_CH_DATE"].ToString().Trim(), out dd))
                    txtPartyChallanDate.Text = dd.ToShortDateString();

                txtPartyChallanNo.Text = dt.Rows[0]["PRTY_CH_NUMB"].ToString().Trim();
                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtVehicleNo.Text = dt.Rows[0]["LORY_NUMB"].ToString().Trim();
                ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();

                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();

                lblTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();

                txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
                txtTransporterAddress.Text = dt.Rows[0]["TAddress"].ToString().Trim();

                txtGateEntryNo.Text = dt.Rows[0]["GATE_NUMB"].ToString().Trim();

            }
            if (iRecordFound == 1)
            {
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                if (ViewState["dtTRN_Sub"] != null)
                    dtTRN_SUB = (DataTable)ViewState["dtTRN_Sub"];

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                if (dtDetailTBL.Rows.Count > 0)
                {
                    MapDataTable();
                    if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                    {
                        //DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];
                        dtTRN_SUB = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                        ViewState["dtTRN_Sub"] = dtTRN_SUB;
                        MapTrnDataTable();

                    }

                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Fabric Detail";
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
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));

            if (!dtDetailTBL.Columns.Contains("MAC_CODE"))
                dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));

            if (!dtDetailTBL.Columns.Contains("COST_CODE"))
                dtDetailTBL.Columns.Add("COST_CODE", typeof(string));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["MAC_CODE"] = string.Empty;
                dtDetailTBL.Rows[iLoop]["COST_CODE"] = string.Empty;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }

    private void MapTrnDataTable()
    {
        try
        {

            if (ViewState["dtTRN_Sub"] != null)
                dtTRN_SUB = (DataTable)ViewState["dtTRN_Sub"];
            if (!dtTRN_SUB.Columns.Contains("UNIQUE_ID"))
                dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            if (!dtTRN_SUB.Columns.Contains("PI_NO"))
                dtTRN_SUB.Columns.Add("PI_NO", typeof(string));

            for (int iLoop = 0; iLoop < dtTRN_SUB.Rows.Count; iLoop++)
            {
                dtTRN_SUB.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
                dtTRN_SUB.Rows[iLoop]["PI_NO"] = "NA";
            }
            Session["dtTRN_SUB"] = dtTRN_SUB;
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
            ddlGateEntryNo.SelectedIndex = -1;
            ddlGateEntryNo.SelectedText = string.Empty;
            ddlGateEntryNo.SelectedValue = string.Empty;
            ddlGateEntryNo.Items.Clear();

            txtGateEntryDate.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtPartyChallanDate.Text = string.Empty;
            txtPartyChallanNo.Text = string.Empty;
            txtGateEntryNo.Text = "";
            RefreshDetailRow();
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            dtDetailTBL.Rows.Clear();
            ViewState["dtDetailTBL"] = dtDetailTBL;
            IsUpdateCall = IsUpdateCall + 1;
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
            string SHADE_CODE;
            if (dtDetailTBL == null)
                CreateDataTable();
            if (cmbShade.SelectedValue.ToString() == null || cmbShade.SelectedValue.ToString() == string.Empty || cmbShade.SelectedValue.ToString() == string.Empty)
            {
                SHADE_CODE = "NA";
            }
            else
            {
                SHADE_CODE = cmbShade.SelectedValue.ToString();
            }

            if (txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "" && SHADE_CODE != "")
            {
                int UNIQUEID = 0;
                if (ViewState["UNIQUEID"] != null)
                {
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                }
                bool bb = SearchItemCodeInGrid(txtICODE.Text.Trim(), UNIQUEID);
                if (!bb)
                {
                    double Qty = 0;
                    double.TryParse(txtQTY.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {
                        if (UNIQUEID > 0)
                        {
                            DataView dv = new DataView(dtDetailTBL);
                            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                            if (dv.Count > 0)
                            {
                                dv[0]["PO_NUMB"] = 999996;
                                dv[0]["PO_TYPE"] = "MII";
                                dv[0]["PO_COMP_CODE"] = "C99996";
                                dv[0]["PO_BRANCH"] = "B99996";
                                dv[0]["FABR_CODE"] = txtICODE.Text.Trim();
                                if (cmbShade.SelectedValue.ToString() == string.Empty || cmbShade.SelectedValue.ToString() == "" || cmbShade.SelectedValue.ToString() == null)
                                {
                                    dv[0]["SHADE_CODE"] = "NA";
                                }
                                else
                                {
                                    dv[0]["SHADE_CODE"] = cmbShade.SelectedValue.ToString(); ;
                                }
                                dv[0]["FABR_DESC"] = txtDESC.Text.Trim();
                                dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dv[0]["UOM"] = txtUNIT.Text.Trim();
                                dv[0]["BASIC_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dv[0]["AMOUNT"] = double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim());
                                dv[0]["COST_CODE"] = string.Empty;// txtCostCode.Text.Trim();
                                dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                dv[0]["QCFLAG"] = "No";


                                dv[0]["NO_OF_UNIT"] = txtNoOfUnit.Text;
                                dv[0]["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                                dv[0]["UOM_OF_UNIT"] = txtUOm.Text;
                                dv[0]["PI_NO"] = "NA";

                                dtDetailTBL.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtDetailTBL.NewRow();
                            dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                            dr["PO_NUMB"] = 999996;
                            dr["PO_TYPE"] = "MII";
                            dr["PO_COMP_CODE"] = "C99996";
                            dr["PO_BRANCH"] = "B99996";
                            dr["FABR_CODE"] = txtICODE.Text.Trim();
                            if (cmbShade.SelectedValue.ToString() == string.Empty || cmbShade.SelectedValue.ToString() == "" || cmbShade.SelectedValue.ToString() == null)
                            {
                                dr["SHADE_CODE"] = "NA";
                            }
                            else
                            {
                                dr["SHADE_CODE"] = cmbShade.SelectedValue.ToString();
                            }
                            dr["FABR_DESC"] = txtDESC.Text.Trim();
                            dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                            dr["UOM"] = txtUNIT.Text.Trim();
                            dr["BASIC_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                            dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                            dr["AMOUNT"] = double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim());
                            dr["COST_CODE"] = string.Empty;// txtCostCode.Text.Trim();
                            dr["MAC_CODE"] = string.Empty;
                            dr["REMARKS"] = txtDetRemarks.Text.Trim();
                            dr["QCFLAG"] = "No";

                            DateTime dd = System.DateTime.Now;
                            dr["DATE_OF_MFG"] = dd;
                            dr["NO_OF_UNIT"] = txtNoOfUnit.Text;
                            dr["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                            dr["UOM_OF_UNIT"] = txtUOm.Text;
                            dr["PI_NO"] = "NA";
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

            ViewState["dtDetailTBL"] = dtDetailTBL;
            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding transaction data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in cancelling transaction.\r\nSee error log for detail."));
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
                string po_type = "PUM";

                string CommandText = "SELECT * FROM (SELECT *FROM (SELECT DISTINCT ( PT.PO_TYPE|| '@'|| PT.PO_NUMB|| '@'|| PT.FABR_CODE)po_Item_trn, pt.PO_NUMB, pt.FABR_CODE, pt.ORD_QTY, PM.PRTY_CODE, pt.BASIC_RATE, pt.FINAL_RATE, i.FABR_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)AS QTY_REM FROM YRN_PU_TRN pt, YRN_MST i, YRN_PU_MST pm WHERE pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.FABR_CODE = i.FABR_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB AND pm.PRTY_CODE = '" + lblPartyCode.Text + "') WHERE PO_NUMB LIKE :SearchQuery OR FABR_CODE LIKE :SearchQuery ORDER BY PO_NUMB) WHERE ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += "   AND PO_NUMB NOT IN(SELECT PO_NUMB FROM (SELECT * FROM (SELECT DISTINCT( PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.FABR_CODE) po_Item_trn,pt.PO_NUMB,pt.FABR_CODE,pt.ORD_QTY,PM.PRTY_CODE,pt.BASIC_RATE,pt.FINAL_RATE,i.FABR_DESC,NVL (PT.QTY_RCPT, 0) QTY_RCPT,NVL (PT.ORD_QTY, 0)- NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM YRN_PU_TRN pt,YRN_MST i,YRN_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb  AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.FABR_CODE = i.FABR_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or FABR_CODE LIKE :SearchQuery ORDER BY PO_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }

                string SortExpression = " order by PO_NUMB";
                string SearchQuery = text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

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

    protected DataTable GetPOData(string text, int PO_Numb, string FABR_CODE)
    {
        try
        {
            DataTable dt = new DataTable();
            if (lblPartyCode.Text != "")
            {
                string po_type = "PUM";
                string whereClause = " WHERE ( PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) >0 AND pt.FABR_CODE = i.FABR_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB AND PM.PRTY_CODE = '" + lblPartyCode.Text.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.FABR_CODE LIKE :SearchQuery) OR ( PM.CONF_FLAG = '1' AND pt.FABR_CODE = i.FABR_CODE AND pm.PO_NUMB = PT.PO_NUMB AND PT.PO_NUMB = '" + PO_Numb + "' AND PT.PO_TYPE = '" + po_type + "' AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PT.FABR_CODE = '" + FABR_CODE + "')";
                string sortExpression = " ORDER BY PO_NUMB";
                string commandText = " SELECT   DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.FABR_CODE) po_Item_trn,pt.COMP_CODE,pt.BRANCH_CODE,pt.PO_NUMB,pt.FABR_CODE, pt.ORD_QTY,i.FABR_DESC,i.FABR_TYPE,i.COLOUR, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.QTY_RTN, 0) QTY_RTN, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM  FROM   TX_FABRIC_PU_TRN pt, TX_FABRIC_MST i, TX_FABRIC_PU_MST pm";

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

    private void GetDataForDetail(string PO_Type, int PONumb, string FABR_CODE)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(FABR_CODE, UNIQUEID))
            {
                string SHADE_CODE = string.Empty;
                DataTable dt = SaitexBL.Interface.Method.YRN_PU_MST.GetTRNData_ForReceiving(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PO_Type, PONumb, FABR_CODE, SHADE_CODE,"",oUserLoginDetail.DT_STARTDATE.Year );

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtICODE.Text = dt.Rows[0]["FABR_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["FABR_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    //  lblMaxQTY.Text = double.Parse(dt.Rows[0]["QTY_REM"].ToString().Trim()).ToString();
                    txtFinalRate.Text = double.Parse(dt.Rows[0]["FINAL_RATE"].ToString().Trim()).ToString();
                    lblPO_BRANCH.Text = dt.Rows[0]["BRANCH_CODE"].ToString().Trim();
                    lblPO_COMP.Text = dt.Rows[0]["COMP_CODE"].ToString().Trim();
                    lblPO_TYPE.Text = dt.Rows[0]["PO_TYPE"].ToString().Trim();
                    txtRemarks.Focus();

                }
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("fabric Already Included");
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
            txtItemCode.SelectedIndex = -1;
            txtItemCode.Enabled = true;
            txtICODE.Text = string.Empty;
            cmbShade.SelectedIndex = -1;
            txtDESC.Text = string.Empty;
            txtQTY.Text = string.Empty;
            txtUNIT.Text = string.Empty;
            txtFinalRate.Text = string.Empty;
            txtDetRemarks.Text = string.Empty;
            lblPO_TYPE.Text = "MII";
            lblPO_BRANCH.Text = "B99996";
            lblPO_COMP.Text = "C99996";
            lblPO_NUMB.Text = "999996";
            txtNoOfUnit.Text = string.Empty;
            txtWeightOfUnit.Text = string.Empty;
            txtUOm.Text = string.Empty;
            ViewState["UNIQUEID"] = null;
            cmbShade.Items.Clear();   
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
                lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                txtICODE.Text = dv[0]["FABR_CODE"].ToString();
                txtDESC.Text = dv[0]["FABR_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();

                txtUOm.Text = dv[0]["UOM_OF_UNIT"].ToString();
                txtWeightOfUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtNoOfUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                //ddlshadeCode.SelectedIndex = ddlshadeCode.Items.IndexOf(ddlshadeCode.Items.FindByText(dv[0]["SHADE_CODE"].ToString()));
                BindShadeByFabrCode(dv[0]["FABR_CODE"].ToString());
                txtItemCode.Enabled = false;
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
                data = GetPOData("", 0);
            else
            {
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
                string PO_Type = arrString[0].ToString();
                int PONumb = int.Parse(arrString[1].ToString());
                string FABR_CODE = arrString[2].ToString();
                data = GetPOData("", PONumb, FABR_CODE);

                if (data != null && data.Rows.Count > 0)
                {
                    DataView dv = new DataView(data);
                    dv.RowFilter = "COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND PO_NUMB='" + PONumb + "' AND FABR_CODE='" + FABR_CODE + "' ";
                    if (dv.Count > 0)
                    {
                        // lblMaxQTY.Text = (double.Parse(dv[0]["QTY_REM"].ToString()) + TRN_QTY).ToString();
                    }
                }

            }
            txtItemCode.Items.Clear();
            txtItemCode.DataSource = data;
            txtItemCode.DataTextField = "PO_NUMB";
            txtItemCode.DataValueField = "po_Item_trn";
            txtItemCode.DataBind();
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

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Entry Detail selection.\r\nSee error log for detail."));
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

            if (string.Compare(lblMode.Text, "Save", true) != 1)
            {
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'FABRIC IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15";
            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {
                CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'FABRIC IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15 ";
            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "Save", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE,a.prty_code,a.prty_name,a.trsp_code,a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='FABRIC IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }
                else if (string.Compare(lblMode.Text, "Update", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE, a.trsp_code, a.trsp_name,A.ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE ='FABRIC IN')asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0)asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB)WHERE ROWNUM <= " + startOffset + ")";
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
        if (string.Compare(lblMode.Text, "Save", true) != 1)
        {
            CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'FABRIC IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
        }
        else if (string.Compare(lblMode.Text, "Update", true) != 1)
        {
            CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'FABRIC IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
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
            DateTime GateDate = System.DateTime.Now;
            string VahicleNo = string.Empty;
            string PartyChallanNo = string.Empty;
            DateTime partyChallanDate = System.DateTime.Now;


            txtGateEntryNo.Text = ddlGateEntryNo.SelectedText.ToString();

            string cString = ddlGateEntryNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            GateDate = DateTime.Parse(arrString[0].ToString());
            VahicleNo = arrString[1].ToString();
            PartyChallanNo = arrString[2].ToString();
            partyChallanDate = DateTime.Parse(arrString[3].ToString());
            lblPartyCode.Text = arrString[4].ToString();
            txtPartyAddress.Text = arrString[5].ToString();
            lblTransporterCode.Text = arrString[6].ToString();
            txtTransporterAddress.Text = arrString[7].ToString();


            GetDataForGateDetail(GateDate, VahicleNo, PartyChallanNo, partyChallanDate);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Entry Detail selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Indent for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selection.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party BillAmount Text Changed.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnSubDetail_Click1(object sender, EventArgs e)
    {
        try
        {

            if (txtICODE.Text != string.Empty)
            {
                txtQTY.ReadOnly = false;
                txtNoOfUnit.ReadOnly = false;
                txtUOm.ReadOnly = false;
                txtWeightOfUnit.ReadOnly = false;
                string URL = "Fabric_SUB_TRN_Misc.aspx";
                URL = URL + "?FABR_CODE=" + txtICODE.Text;
                URL = URL + "&PO_NUMB=" + "999996";
                URL = URL + "&PO_TYPE=" + "MII";
                URL = URL + "&PO_COMP_CODE=" + "C99996";
                URL = URL + "&PO_BRANCH=" + "B99996";
                if (cmbShade.SelectedValue.ToString() == string.Empty || cmbShade.SelectedValue.ToString() == "" || cmbShade.SelectedValue.ToString() == null)
                {
                    URL = URL + "&SHADE_CODE=" + "NA";
                }
                else
                {
                    URL = URL + "&SHADE_CODE=" + cmbShade.SelectedItem.ToString();
                }
                //URL = URL + "&lblMaxQTY=" + "";
                URL = URL + "&txtQTY=" + txtQTY.ClientID;
                URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
                URL = URL + "&txtWeightOfUnit=" + txtWeightOfUnit.ClientID;
                URL = URL + "&txtUOm=" + txtUOm.ClientID;
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=850,height=320,left=200,top=300');", true);
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

    protected void grdMaterialItemReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lnkunige = (LinkButton)e.Row.FindControl("lnkunige");
                int UNIQUE_ID = int.Parse(lnkunige.CommandArgument);

                if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtDetailTBL);

                    dv.RowFilter = "UNIQUEID=" + UNIQUE_ID;
                    if (dv.Count > 0)
                    {
                        string FABR_CODE = dv[0]["FABR_CODE"].ToString();
                        string SHADE_CODE = dv[0]["SHADE_CODE"].ToString();
                        if (Session["dtTRN_SUB"] != null)
                        {
                            DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];

                            DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
                            dvYRNDRecieve_trn.RowFilter = "FABR_CODE='" + FABR_CODE + "' AND SHADE_CODE='" + SHADE_CODE + "'";

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

    protected void txtQTY_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
    }

    protected void CalculateAmount()
    {
        txtQTY.ReadOnly = true;
        txtNoOfUnit.ReadOnly = true;
        txtUOm.ReadOnly = true;
        txtWeightOfUnit.ReadOnly = true;
        double Amount = 0;
        double fRate = 0;
        double Qty = 0;
        double.TryParse(txtFinalRate.Text.Trim(), out fRate);
        double.TryParse(txtQTY.Text.Trim(), out Qty);
        Amount = Qty * fRate;

    }

    protected void Item_LOV_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtItemCode.Items.Clear();

                txtItemCode.DataSource = data;
                txtItemCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in fabric loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (  SELECT   FABR_CODE, FABR_DESC, FABR_TYPE, DESIGN_NO,    FABR_CODE || '@' || FABR_DESC || '@' || UOM || '@' || TRANSFER_RATE    AS Combined FROM   TX_FABRIC_MST WHERE  FABR_CODE LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY   FABR_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  WHERE   ROWNUM <= 15 AND FABR_CODE NOT IN (SELECT   FABR_CODE FROM   (  SELECT   FABR_CODE, FABR_DESC, DESIGN_NO, FABR_CODE  || '@'  || FABR_DESC  || '@'  || UOM  || '@'  || v  AS Combined  FROM   TX_FABRIC_MST  WHERE      FABR_CODE LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery  OR FABR_DESC LIKE :SearchQuery  ORDER BY   FABR_CODE) asd  WHERE   ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY FABR_CODE";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   FABR_CODE, FABR_DESC, FABR_TYPE, DESIGN_NO,    FABR_CODE || '@' || FABR_DESC || '@' || UOM || '@' || TRANSFER_RATE    AS Combined FROM   TX_FABRIC_MST WHERE  FABR_CODE LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY   FABR_CODE) asd";
        string WhereClause = " ";
        string SortExpression = " ORDER BY FABR_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void Item_LOV_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = txtItemCode.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string FABR_CODE = arrString[0].ToString();
            string FABR_DESC = arrString[1].ToString();
            string UOM = arrString[2].ToString();
            string TRANSFER_PRICE = arrString[3].ToString();


            txtDESC.Text = FABR_DESC.Trim();
            txtICODE.Text = FABR_CODE.Trim();
            txtUNIT.Text = UOM.Trim();
            txtFinalRate.Text = TRANSFER_PRICE.Trim();
            BindShadeByFabrCode(FABR_CODE.Trim());  
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Po Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

    private int GetItemDetailByItemCode(string ItemCode, out string Description, out string UOM, out double CurrentStock, out double MinStockLevel, out double OpeningRate, out double Min_Procure_days)
    {
        int iRecordFound = 0;
        Description = "";
        UOM = "";
        CurrentStock = 0;
        MinStockLevel = 0;
        OpeningRate = 0;
        Min_Procure_days = 0;
        try
        {
            DataTable dts = null;// SaitexBL.Interface.Method.YRN_INT_MST.GetItemDetailByItemCode(oUserLoginDetail.DT_STARTDATE.Year, ItemCode);
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["FABR_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UNIT_NAME"].ToString().Trim();

                double.TryParse(dts.Rows[0]["currentStock"].ToString().Trim(), out CurrentStock);
                double.TryParse(dts.Rows[0]["MIN_STOCK"].ToString().Trim(), out MinStockLevel);
                double.TryParse(dts.Rows[0]["OP_RATE"].ToString().Trim(), out OpeningRate);
                double.TryParse(dts.Rows[0]["MIN_PROCURE_DAYS"].ToString().Trim(), out Min_Procure_days);
                iRecordFound = dts.Rows.Count;
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    //private void BindShadeCode()
    //{
    //    try
    //    {
    //        ddlshadeCode.Items.Clear();
    //        SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    //        oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;

    //        DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL(oOD_SHADE_FAMILY);
    //        ddlshadeCode.DataSource = dt;
    //        ddlshadeCode.DataTextField = "SHADE_CODE";
    //        ddlshadeCode.DataValueField = "SHADE_CODE";
    //        ddlshadeCode.DataBind();

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //protected void ddlshadeCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int UNIQUEID = 0;
    //        if (ViewState["UNIQUEID"] != null)
    //            UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());

    //        if (SearchItemCodeInGrid(txtICODE.Text, UNIQUEID))
    //        {
    //            ddlshadeCode.SelectedIndex = -1;
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('This Yarn already included with selected shade');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item loading.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

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
            else
            {
                ListItem li = new ListItem();
                li.Value="NA";
                li.Text = "NA";
                cmbShade.Items.Add(li);
            }
        }
        catch
        {
            throw;
        }
    }
}
