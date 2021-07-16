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

public partial class Module_Yarn_SalesWork_Controls_YarnRecepitParty : System.Web.UI.UserControl
{
    private DataTable dtTRN_SUB = null;
    private static DateTime StartDate;
    private static DateTime EndDate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtDetailTBL = null;
    private string TRN_TYPE = "RYJ01";
    private static int IsUpdateCall = 2;
    private  string PRODUCT_TYPE = "YARN";
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
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);  

            
            
            lblPO_TYPE.Text = "MII";
            lblPO_BRANCH.Text = "B99996";
            lblPO_COMP.Text = "C99996";
            lblPO_NUMB.Value = "999996";
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
            txtSpinnerCode.SelectedIndex = -1;
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
            dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_FAMILY", typeof(string));
            
            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("GROSS_WT", typeof(double));
            dtDetailTBL.Columns.Add("TARE_WT", typeof(double));
            dtDetailTBL.Columns.Add("CARTONS", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_PALLET", typeof(string));
            dtDetailTBL.Columns.Add("JOBER", typeof(string));

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
        try
        {
            if (ViewState["TRN_TYPE"] != null)
                TRN_TYPE = (string)ViewState["TRN_TYPE"];

            Response.Redirect("~/Module/Yarn/SalesWork/Reports/YRN_ReceiptPermRPT.aspx?TRN_TYPE=" + TRN_TYPE, false);
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

            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    LinkButton txtICODE = (LinkButton)grdRow.FindControl("txtICODE");
                    Label txtSHADE = (Label)grdRow.FindControl("txtshadeCode");
                    Label txtSHADEFAMILY = (Label)grdRow.FindControl("txtshadeFamily");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (txtICODE.Text.Trim() == ItemCode && UNIQUEID != iUNIQUEID && txtShadeName.Text.Trim() == txtSHADE.Text.Trim() && txtShadeFamilyName.Text.Trim() == txtSHADEFAMILY.Text)
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
            oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue ;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
            oYRN_IR_MST.CONSIGNEE_CODE = string.Empty;
           
            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.VC_BRANCHNAME;
            oYRN_IR_MST.BILL_DATE = DateTime.Now;
            int BILLNO = 0;
            int.TryParse(txtPartyBillNo.Text,out BILLNO);
            oYRN_IR_MST.BILL_NUMBER = BILLNO;
            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            double BILLAMOUNT = 0;
            double.TryParse(txtPartyBillAmount.Text, out BILLAMOUNT);
            oYRN_IR_MST.PARTY_BILL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.TOTAL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.SPINNER_CODE = txtSpinnerCode.SelectedValue;


            int TRN_NUMB = 0;
            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Insert(oYRN_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtTRN_SUB, null, null);
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
            oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(lblTransporterCode.Text.Trim());
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
            oYRN_IR_MST.CONSIGNEE_CODE = string.Empty;

            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.VC_BRANCHNAME;
            oYRN_IR_MST.BILL_DATE = DateTime.Now;
            int BILLNO = 0;
            int.TryParse(txtPartyBillNo.Text, out BILLNO);
            oYRN_IR_MST.BILL_NUMBER = BILLNO;
            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            double BILLAMOUNT = 0;
            double.TryParse(txtPartyBillAmount.Text, out BILLAMOUNT);
            oYRN_IR_MST.PARTY_BILL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.TOTAL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.SPINNER_CODE = txtSpinnerCode.SelectedValue;

            if (Session["dtTRN_SUB"] != null)
            {
                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
            }
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.Update(oYRN_IR_MST, dtDetailTBL, htReceive, dtTRN_SUB, null, null);
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

            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);

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
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["STORE"].ToString()));
                
                DataTable dataMac = GetSpinnerData("", 1000);
                txtSpinnerCode.DataSource = dataMac;
                txtSpinnerCode.DataTextField = "PRTY_NAME";
                txtSpinnerCode.DataValueField = "PRTY_CODE";
                txtSpinnerCode.DataBind();
                foreach (Obout.ComboBox.ComboBoxItem item in txtSpinnerCode.Items)
                {
                    if (item.Text == dt.Rows[0]["SPINNER_CODE"].ToString().Trim())
                    {
                        txtSpinnerCode.SelectedIndex = txtSpinnerCode.Items.IndexOf(item);

                        txtSpinnerCode.SelectedText = item.Text.ToString();
                        txtSpinnerCode.SelectedValue = item.Value.ToString();

                        break;
                    }
                }
            }
            if (iRecordFound == 1)
            {
                if (ViewState["dtDetailTBL"] != null)
                    dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                if (ViewState["dtTRN_Sub"] != null)
                    dtTRN_SUB = (DataTable)ViewState["dtTRN_Sub"];

                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.YRN_IR_MST.GetTRN_DataByTRN_NUMBst(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                if (dtDetailTBL.Rows.Count > 0)
                {
                    MapDataTable();
                    if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                    {
                        //DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];
                        dtTRN_SUB = SaitexBL.Interface.Method.YRN_IR_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                        ViewState["dtTRN_Sub"] = dtTRN_SUB;
                        MapTrnDataTable();

                    }

                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! MRN not contains Yarn Detail";
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

            if (!dtDetailTBL.Columns.Contains("COST_CENTER_CODE"))
                dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["MAC_CODE"] = string.Empty;
                dtDetailTBL.Rows[iLoop]["COST_CENTER_CODE"] = string.Empty;
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

            if (dtDetailTBL == null)
                CreateDataTable();

            if (txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "" && txtShadeName.Text != "")
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
                                dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dv[0]["YARN_CODE"] = txtICODE.Text.Trim();
                                dv[0]["SHADE_CODE"] = txtShadeName.Text.Trim();
                                dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dv[0]["UOM"] = txtUNIT.Text.Trim();
                                dv[0]["BASIC_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dv[0]["AMOUNT"] = double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim());
                                dv[0]["COST_CENTER_CODE"] = string.Empty;// txtCostCode.Text.Trim();
                                dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                dv[0]["QCFLAG"] = "No";


                                dv[0]["NO_OF_UNIT"] = txtNoOfUnit.Text;
                                dv[0]["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                                dv[0]["UOM_OF_UNIT"] = txtUOm.Text;
                                dv[0]["PI_NO"] = "NA";
                                dv[0]["SHADE_FAMILY"] = txtShadeFamilyName.Text.Trim();

                                dv[0]["LOT_NO"] = txtLotNo.SelectedValue;                               
                                double grossWt = 0;
                                double tareWt = 0;
                                double cartons = 0;
                                double.TryParse(txtGrossWt.Value, out grossWt);
                                double.TryParse(txtTareWt.Value, out tareWt);
                                double.TryParse(txtCartons.Value, out cartons);
                                dv[0]["GRADE"] = txtGrade.SelectedValue;
                                dv[0]["GROSS_WT"] = grossWt;
                                dv[0]["TARE_WT"] = tareWt;
                                dv[0]["CARTONS"] = cartons;
                                dv[0]["NO_OF_PALLET"] = txtNoOfPallet.Value;
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
                            dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["YARN_CODE"] = txtICODE.Text.Trim();
                            dr["SHADE_CODE"] = txtShadeName.Text.Trim();
                            dr["YARN_DESC"] = txtDESC.Text.Trim();
                            dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                            dr["UOM"] = txtUNIT.Text.Trim();
                            dr["BASIC_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                            dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                            dr["AMOUNT"] = double.Parse(txtFinalRate.Text.Trim()) * double.Parse(txtQTY.Text.Trim());
                            dr["COST_CENTER_CODE"] = string.Empty;// txtCostCode.Text.Trim();
                            dr["MAC_CODE"] = string.Empty;
                            dr["REMARKS"] = txtDetRemarks.Text.Trim();
                            dr["QCFLAG"] = "No";

                            DateTime dd = System.DateTime.Now;
                            dr["DATE_OF_MFG"] = dd;
                            dr["NO_OF_UNIT"] = txtNoOfUnit.Text;
                            dr["WEIGHT_OF_UNIT"] = txtWeightOfUnit.Text;
                            dr["UOM_OF_UNIT"] = txtUOm.Text;
                            dr["PI_NO"] = "NA";
                            dr["SHADE_FAMILY"] = txtShadeFamilyName.Text.Trim();

                                dr["LOT_NO"] = txtLotNo.SelectedValue;                               
                                double grossWt = 0;
                                double tareWt = 0;
                                double cartons = 0;
                                double.TryParse(txtGrossWt.Value, out grossWt);
                                double.TryParse(txtTareWt.Value, out tareWt);
                                double.TryParse(txtCartons.Value, out cartons);
                                dr["GRADE"] = txtGrade.SelectedValue;
                                dr["GROSS_WT"] = grossWt;
                                dr["TARE_WT"] = tareWt;
                                dr["CARTONS"] = cartons;
                                dr["NO_OF_PALLET"] = txtNoOfPallet.Value;
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

                string CommandText = "SELECT * FROM (SELECT *FROM (SELECT DISTINCT ( PT.PO_TYPE|| '@'|| PT.PO_NUMB|| '@'|| PT.YARN_CODE)po_Item_trn, pt.PO_NUMB, pt.YARN_CODE, pt.ORD_QTY, PM.PRTY_CODE, pt.BASIC_RATE, pt.FINAL_RATE, i.YARN_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)AS QTY_REM FROM YRN_PU_TRN pt, YRN_MST i, YRN_PU_MST pm WHERE pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.YARN_CODE = i.YARN_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB AND pm.PRTY_CODE = '" + lblPartyCode.Text + "') WHERE PO_NUMB LIKE :SearchQuery OR YARN_CODE LIKE :SearchQuery ORDER BY PO_NUMB) WHERE ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += "   AND PO_NUMB NOT IN(SELECT PO_NUMB FROM (SELECT * FROM (SELECT DISTINCT( PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.YARN_CODE) po_Item_trn,pt.PO_NUMB,pt.YARN_CODE,pt.ORD_QTY,PM.PRTY_CODE,pt.BASIC_RATE,pt.FINAL_RATE,i.YARN_DESC,NVL (PT.QTY_RCPT, 0) QTY_RCPT,NVL (PT.ORD_QTY, 0)- NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM YRN_PU_TRN pt,YRN_MST i,YRN_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb  AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.YARN_CODE = i.YARN_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or YARN_CODE LIKE :SearchQuery ORDER BY PO_NUMB) WHERE ROWNUM <= " + startOffset + ")";
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

    protected DataTable GetPOData(string text, int PO_Numb, string Yarn_Code)
    {
        try
        {
            DataTable dt = new DataTable();
            if (lblPartyCode.Text != "")
            {
                string po_type = "PUM";
                string whereClause = " WHERE ( PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) >0 AND pt.YARN_CODE = i.YARN_CODE AND PT.PO_TYPE = '" + po_type + "' AND pm.PO_NUMB = PT.PO_NUMB AND PM.PRTY_CODE = '" + lblPartyCode.Text.Trim() + "' AND pt.PO_NUMB LIKE :SearchQuery AND pt.YARN_CODE LIKE :SearchQuery) OR ( PM.CONF_FLAG = '1' AND pt.YARN_CODE = i.YARN_CODE AND pm.PO_NUMB = PT.PO_NUMB AND PT.PO_NUMB = '" + PO_Numb + "' AND PT.PO_TYPE = '" + po_type + "' AND PT.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PT.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PT.YARN_CODE = '" + Yarn_Code + "')";
                string sortExpression = " ORDER BY PO_NUMB";
                string commandText = " SELECT   DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.YARN_CODE) po_Item_trn,pt.COMP_CODE,pt.BRANCH_CODE,pt.PO_NUMB,pt.YARN_CODE, pt.ORD_QTY,i.YARN_DESC,i.YARN_TYPE,i.COLOUR, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.QTY_RTN, 0) QTY_RTN, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM  FROM   YRN_PU_TRN pt, YRN_MST i, YRN_PU_MST pm";

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
                    if (dr1["YARN_CODE"].ToString() == dr["YARN_CODE"].ToString())
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

    private void GetDataForDetail(string PO_Type, int PONumb, string YARN_CODE)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(YARN_CODE, UNIQUEID))
            {
                string SHADE_CODE = string.Empty;
                DataTable dt = SaitexBL.Interface.Method.YRN_PU_MST.GetTRNData_ForReceiving(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PO_Type, PONumb, YARN_CODE, SHADE_CODE, "", oUserLoginDetail.DT_STARTDATE.Year);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtICODE.Text = dt.Rows[0]["YARN_CODE"].ToString().Trim();
                    txtDESC.Text = dt.Rows[0]["YARN_DESC"].ToString().Trim();
                    txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                    txtUOm.Text = dt.Rows[0]["UOM"].ToString().Trim();
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
                CommonFuction.ShowMessage("Yarn Already Included");
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
            txtShadeName.Text = string.Empty;
            cmbShade.SelectedIndex = -1;
            txtDESC.Text = string.Empty;
            txtQTY.Text = string.Empty;
            txtUNIT.Text = string.Empty;
            txtFinalRate.Text = string.Empty;
            txtDetRemarks.Text = string.Empty;
            lblPO_TYPE.Text = "MII";
            lblPO_BRANCH.Text = "B99996";
            lblPO_COMP.Text = "C99996";
            lblPO_NUMB.Value = "999996";
            txtNoOfUnit.Text = string.Empty;
            txtWeightOfUnit.Text = string.Empty;
            txtUOm.Text = string.Empty;
            ViewState["UNIQUEID"] = null;
            txtShadeFamilyName.Text = string.Empty;
            txtShadeName.Text = string.Empty; 
            txtLotNo.SelectedIndex=-1;
            txtGrade.SelectedIndex=-1;
            txtNoOfPallet.Value = string.Empty;
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
                txtICODE.Text = dv[0]["YARN_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();

                txtUOm.Text = dv[0]["UOM_OF_UNIT"].ToString();
                txtWeightOfUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtNoOfUnit.Text = dv[0]["NO_OF_UNIT"].ToString();                
                txtShadeName.Text = dv[0]["SHADE_CODE"].ToString().Trim();
                txtShadeFamilyName.Text = dv[0]["SHADE_FAMILY"].ToString().Trim();
                setShadeFamilyCombo(dv[0]["SHADE_FAMILY"].ToString().Trim(), dv[0]["SHADE_CODE"].ToString());
                txtItemCode.Enabled = false;

                txtNoOfPallet.Value = dv[0]["NO_OF_PALLET"].ToString();
                string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GREY_LOT_NO'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression = " order by MST_CODE asc";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", SortExpression, "", "%", "");
                txtLotNo.DataSource = data;
                txtLotNo.DataTextField = "MST_CODE";
                txtLotNo.DataValueField = "MST_CODE";
                txtLotNo.DataBind();
                foreach (ComboBoxItem item in txtLotNo.Items)
                {
                    if (item.Text == dv[0]["LOT_NO"].ToString())
                    {
                        txtLotNo.SelectedIndex = txtLotNo.Items.IndexOf(item);
                        break;
                    }
                }





                string CommandText2 = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GRADE'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression2 = " order by MST_CODE asc";
                DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, "", SortExpression2, "", "%", "");
                txtGrade.DataSource = data2;
                txtGrade.DataTextField = "MST_CODE";
                txtGrade.DataValueField = "MST_CODE";
                txtGrade.DataBind();
                foreach (ComboBoxItem item in txtGrade.Items)
                {
                    if (item.Text == dv[0]["GRADE"].ToString())
                    {
                        txtGrade.SelectedIndex = txtGrade.Items.IndexOf(item);
                        break;
                    }
                }
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
                string Yarn_Code = arrString[2].ToString();
                data = GetPOData("", PONumb, Yarn_Code);

                if (data != null && data.Rows.Count > 0)
                {
                    DataView dv = new DataView(data);
                    dv.RowFilter = "COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND PO_NUMB='" + PONumb + "' AND YARN_CODE='" + Yarn_Code + "' ";
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
                CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name,A.ITEM_TYPE FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE IN  ( 'YARN JOBWORK IN') AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15";
            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {
                CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB,A.ITEM_TYPE FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE IN  ( 'YARN JOBWORK IN')) asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= 15 ";
            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "Save", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE,a.prty_code,a.prty_name,a.trsp_code,a.trsp_name,A.ITEM_TYPE FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE IN  ( 'YARN JOBWORK IN') AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) WHERE ROWNUM <= " + startOffset + ")";
                }
                else if (string.Compare(lblMode.Text, "Update", true) != 1)
                {
                    whereClause = " AND GATE_NUMB NOT IN(SELECT GATE_NUMB FROM (SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE || '@' || a.LORRY_NO || '@' || a.DOC_NO || '@' || a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name) GATE_DATA,A.GATE_DATE,A.GATE_NUMB,A.GATE_TYPE, a.trsp_code, a.trsp_name,A.ISSUE_NUMB ,A.ITEM_TYPE FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE IN  ( 'YARN JOBWORK IN'))asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0)asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB)WHERE ROWNUM <= " + startOffset + ")";
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
            CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.prty_code, a.prty_name, a.trsp_code, a.trsp_name FROM v_tx_gate_mst a WHERE a.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'YARN JOBWORK IN' AND NVL (a.ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
        }
        else if (string.Compare(lblMode.Text, "Update", true) != 1)
        {
            CommandText = "SELECT *FROM (SELECT *FROM (SELECT *FROM (SELECT ( a.GATE_DATE|| '@'|| a.LORRY_NO|| '@'|| a.DOC_NO|| '@'|| a.DOC_DATE|| '@'||a.prty_code|| '@'|| a.prty_name|| '@'|| a.trsp_code|| '@'|| a.trsp_name)GATE_DATA, A.GATE_DATE, A.GATE_NUMB, A.GATE_TYPE, a.trsp_code, a.trsp_name, ISSUE_NUMB FROM v_tx_gate_mst a WHERE a.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND a.YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' AND a.ITEM_TYPE = 'YARN JOBWORK IN') asd WHERE NVL (ISSUE_NUMB, 0) = '" + txtTRNNUMBer.Text.Trim() + "' OR NVL (ISSUE_NUMB, 0) = 0) asd WHERE GATE_NUMB LIKE :SearchQuery OR GATE_DATE LIKE :SearchQuery ORDER BY GATE_NUMB) ";
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
            string commandText = "select * from (Select a.TRN_NUMB, a.TRN_DATE,a.PRTY_CODE,b.PRTY_NAME from YRN_IR_MST a, tx_vendor_mst b Where  NVL(a.CONF_FLAG,0)=0 AND a.prty_code=b.prty_code (+) and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + TRN_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

           
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

            if (txtICODE.Text != string.Empty && txtLotNo.SelectedValue.Trim() != string.Empty && txtGrade.SelectedValue != string.Empty)
            {
                txtQTY.ReadOnly = false;
                txtNoOfUnit.ReadOnly = false;
                txtUOm.ReadOnly = false;
                txtWeightOfUnit.ReadOnly = false;
                string URL = "YRN_SUB_TRN_Misc.aspx";
                URL = URL + "?YARN_CODE=" + txtICODE.Text;
                URL = URL + "&LOT_NO=" + txtLotNo.SelectedValue.TrimEnd().TrimStart();
                URL = URL + "&PI_NO=NA";
                URL = URL + "&GRADE=" + txtGrade.SelectedValue;
                URL = URL + "&PO_NUMB=" + "999996";
                URL = URL + "&PO_TYPE=" + "MII";
                URL = URL + "&PO_COMP_CODE=" + "C99996";
                URL = URL + "&PO_BRANCH=" + "B99996";
                URL = URL + "&PO_YEAR=" + oUserLoginDetail.DT_STARTDATE.Year ;
                URL = URL + "&txtShadeCode=" + txtShadeName.Text.Trim();
                URL = URL + "&txtShadeFamily=" + txtShadeFamilyName.Text.Trim();   
                URL = URL + "&txtQTY=" + txtQTY.ClientID;
                URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
                URL = URL + "&txtWeightOfUnit=" + txtWeightOfUnit.ClientID;
               //URL = URL + "&txtUOm=" + txtUOm.ClientID;
                URL = URL + "&GROSS_WT=" + txtGrossWt.ClientID;
                URL = URL + "&TARE_WT=" + txtTareWt.ClientID;
                URL = URL + "&CARTONS=" + txtCartons.ClientID;
                URL = URL + "&txtNoOfPallet=" + txtNoOfPallet.ClientID;
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=850,height=320,left=200,top=300');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);


            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Lot Number and Grade");
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
                    if (dv.Count > 0)                    {
                       
                        if (Session["dtTRN_SUB"] != null)
                        {
                            DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];

                            DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
                            dvYRNDRecieve_trn.RowFilter = "YARN_CODE='" + dv[0]["YARN_CODE"].ToString() + "' AND SHADE_CODE='" + dv[0]["SHADE_CODE"].ToString() + "' AND SHADE_FAMILY='" + dv[0]["SHADE_FAMILY"].ToString() + "' and LOT_NO='" + (dv[0]["LOT_NO"].ToString()).TrimEnd().TrimStart() + "'  and GRADE='" + dv[0]["GRADE"].ToString() + "' ";
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
            if (data != null && data.Rows.Count > 0)
            {
                txtItemCode.Items.Clear();

                txtItemCode.DataSource = data;
                txtItemCode.DataBind();
            }           
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;            
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   * FROM   (SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE,M.COLOUR, A.ASS_YARN_CODE, A.ASS_YARN_DESC, (   M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE) AS Combined FROM   YRN_MST M, YRN_ASSOCATED_MST A WHERE   M.YARN_CODE = A.YARN_CODE AND M.YARN_CODE LIKE :SearchQuery OR M.YARN_TYPE LIKE :SearchQuery OR A.ASS_YARN_DESC LIKE :SearchQuery) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "AND ASS_YARN_CODE NOT IN (SELECT   ASS_YARN_CODE FROM  (SELECT   * FROM   (SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR, A.ASS_YARN_CODE, A.ASS_YARN_DESC, (   M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE) AS Combined FROM   YRN_MST M, YRN_ASSOCATED_MST A WHERE   M.YARN_CODE = A.YARN_CODE AND M.YARN_CODE LIKE :SearchQuery OR M.YARN_TYPE LIKE :SearchQuery OR A.ASS_YARN_DESC LIKE :SearchQuery) asd) WHERE   ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY ASS_YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
           return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

           
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   * FROM   (SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE,M.COLOUR, A.ASS_YARN_CODE, A.ASS_YARN_DESC, (   M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE) AS Combined FROM   YRN_MST M, YRN_ASSOCATED_MST A WHERE   M.YARN_CODE = A.YARN_CODE AND M.YARN_CODE LIKE :SearchQuery OR M.YARN_TYPE LIKE :SearchQuery OR M.YARN_DESC LIKE :SearchQuery) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY ASS_YARN_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count; ;
        
    }

    protected void Item_LOV_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = txtItemCode.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            txtDESC.Text = arrString[1].ToString();
            txtICODE.Text = arrString[0].ToString();
            txtUNIT.Text = arrString[2].ToString();
            txtUOm.Text = arrString[2].ToString();
            txtFinalRate.Text = arrString[3].ToString();

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
            DataTable dts = SaitexBL.Interface.Method.YRN_INT_MST.GetItemDetailByItemCode(oUserLoginDetail.DT_STARTDATE.Year, ItemCode, oUserLoginDetail.CH_BRANCHCODE, "", "", "", "");
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["YARN_DESC"].ToString().Trim();
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
  
    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetShadeItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbShade_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbShade.SelectedIndex > -1)
            {
                //string[] arrString = cmbShade.SelectedValue.Split('@');
                //txtShadeFamilyName.Text = arrString[0].ToString();
                //txtShadeFamilyName.Text = arrString[1].ToString();
                //txtShadeName.Text = arrString[2].ToString();
                //txtShadeName.Text = arrString[3].ToString();
                txtShadeFamilyName.Text = cmbShade.SelectedText ;
                txtShadeName.Text = cmbShade.SelectedValue ;

            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Shade");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetShadeItems(string text, int startOffset)
    {
        try
        {
           // string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE LIKE  '%" + PRODUCT_TYPE + "%' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_NAME ||'@' ||T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND T.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
               // whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '%" + PRODUCT_TYPE + "%' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND T.PRODUCT_TYPE = 'YARN'  AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            
        }
        catch
        {
            throw;
        }
    }

    protected int GetShadeItemsCount(string text)
    {
        try
        {
            //string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '%" + PRODUCT_TYPE + "%' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE LIKE '%YARN%' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
            
        }
        catch
        {
            throw;
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
   
    public void setShadeFamilyCombo(string shade_family, string shade)
    {

        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery)", "", "", "", "%", "");
        cmbShade.DataSource = data;
        cmbShade.DataTextField = "SHADE_FAMILY_NAME";
        cmbShade.DataValueField = "SHADE_NAME";
        cmbShade.DataBind();
        foreach (ComboBoxItem dl in cmbShade.Items)
        {
            if (dl.Text == shade_family && dl.Value == shade)
            {
                cmbShade.SelectedIndex = cmbShade.Items.IndexOf(dl);
                break;
            }
        }
    }    


    protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "GREY_LOT_NO");
            txtLotNo.Items.Clear();
            txtLotNo.DataSource = data;
            txtLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetMOMCount(e.Text, "GREY_LOT_NO");
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
                whereClause += " AND MST_CODE NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";
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
    protected void txtSpinner_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetSpinnerData(e.Text.ToUpper(), e.ItemsOffset);

            txtSpinnerCode.Items.Clear();

            txtSpinnerCode.DataSource = data;
            txtSpinnerCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetSpinnerCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetSpinnerData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST WHERE NVL(CONF_FLAG,0)=1 AND (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery) ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)=upper('Spinner') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST  WHERE NVL(CONF_FLAG,0)=1 AND (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery) ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)<> upper('Spinner') and ROWNUM <= " + startOffset + ")";
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

    protected int GetSpinnerCount(string text)
    {
        try
        {

            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1,VENDOR_CAT_CODE, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE  NVL(CONF_FLAG,0)=1 AND (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery) ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)=upper('Spinner') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch
        {
            throw;
        }
    }
}
