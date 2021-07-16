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
using Obout.ComboBox;
using Obout.Interface;
using System.Collections.Generic;

public partial class Module_Fabric_FabricSaleWork_Controls_FabricStockTransferNew : System.Web.UI.UserControl
{
    private SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail ;
    private DataTable dtDetailTBL = null;
    private static string ISSUE_TYPE = string.Empty;
    private static string RECEIPT_TYPE = string.Empty;
    private static double FinalTotal = 0;
    private static DateTime StartDate;
    private static DateTime EndDate;
    protected void Page_Load(object sender, EventArgs e)
    {
 try
        {
            if (!IsPostBack)
            {
                GetCompanyData();
                GetDepartmentData();
                InitialisePage();
            }
        }
 catch (Exception ex)
 {

     CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
 }
    }
    private void GetCompanyData()
    {
        try
        {
            DataTable dtCompany = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();
            ViewState["dtCompany"] = dtCompany;
        }
        catch
        {
            throw;
        }
    }
    private void GetDepartmentData()
    {
        try
        {
            DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ViewState["dtDepartment"] = dtDepartment;
        }
        catch
        {
            throw;
        }
    }
    private void InitialisePage()
    {
        try
        {
            BindCMBPOITEM();
            tdUpdate.Visible = false;
            //tdDelete.Visible = false;
            tdSave.Visible = true;

            // ISSUE_TYPE = "IRS06";
            ISSUE_TYPE = "IFS06";
            RECEIPT_TYPE = "RFS06";

            Session["dtFabricReceipt"] = null;

            ActivateSaveMode();
            BindShift();
            BindCompanyDropDown(ddlSourceCompany);
            BindCompanyDropDown(ddlDestinationCompany);
            BindDepartmentDropDown(ddlSourceDepartment);
            BindDepartmentDropDown(ddlDestinationDepartment);
            Blankrecords();
            cmbShade.Items.Clear();
            CreateDataTable();
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                StartDate = oUserLoginDetail.DT_STARTDATE;
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }

            txtIssueDate.Text = System.DateTime.Now.ToShortDateString();
            RefreshDetailRow();
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
            dtDetailTBL.Columns.Add("PO_RATE", typeof(int));
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("TRN_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
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
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(int));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(int));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("SHADE_CODE", typeof(string));


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
            txtICODE.SelectedIndex = -1;
            txtDESC.Text = "";
            txtQTY.Text = "";
            txtUNIT.Text = "";
            txtBasicRate.Text = "";
            txtAmount.Text = "";
            //txtCostCode.Text = "";
            //txtMacCode.Text = "";
            txtDetRemarks.Text = "";
            //lblPO_BRANCH.Text = "";
            //lblPO_COMP.Text = "";
            //lblPO_TYPE.Text = "";
            ViewState["UNIQUEID"] = null;
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
            txtDocDate.Text = "";
            txtDocNo.Text = "";
            txtIssueDate.Text = "";
            txtRemarks.Text = "";
            txtVehicleNo.Text = "";

            ddlIssueShift.SelectedIndex = 0;
            ddlSourceDepartment.SelectedIndex = 0;
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL != null)
                dtDetailTBL.Rows.Clear();

            grdMaterialItemIssue.DataSource = null;
            grdMaterialItemIssue.DataBind();

            lblMode.Text = "Save";

            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                ddlSourceDepartment.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;
                ddlDestinationDepartment.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;
                ddlSourceCompany.SelectedValue = oUserLoginDetail.COMP_CODE;
                ddlDestinationCompany.SelectedValue = oUserLoginDetail.COMP_CODE;
                BindBranchDropDown(ddlDestinationBranch, oUserLoginDetail.COMP_CODE);
                BindBranchDropDown(ddlSourceBranch, oUserLoginDetail.COMP_CODE);
                ddlSourceBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                ddlDestinationBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;

                BindIssueNumb();
                BindReceiptNumb();
            }

            // tdDelete.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;

        }
        catch
        {
            throw;
        }
    }
    private void BindIssueNumb()
    {
        try
        {
            string Branch_code = ddlSourceBranch.SelectedValue.Trim();
            string Comp_Code = ddlSourceCompany.SelectedValue.Trim();

            txtIssueNumb.Text = GetNewTRNNumb(Comp_Code, Branch_code, ISSUE_TYPE);
        }
        catch { throw; }
    }
    private void BindReceiptNumb()
    {
        try
        {
            string Branch_code = ddlDestinationBranch.SelectedValue.Trim();
            string Comp_Code = ddlDestinationCompany.SelectedValue.Trim();

            txtReceiptNumb.Text = GetNewTRNNumb(Comp_Code, Branch_code, RECEIPT_TYPE);
        }
        catch { throw; }
    }
    private string GetNewTRNNumb(string Comp_code, string Branch_code, string ISSUE_TYPE)
    {
        try
        {
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.TRN_TYPE = ISSUE_TYPE;
            oTX_FABRIC_IR_MST.COMP_CODE = Comp_code;
            oTX_FABRIC_IR_MST.BRANCH_CODE = Branch_code;
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            string TRN_NUMB = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetNewTRNNumber(oTX_FABRIC_IR_MST);
            return TRN_NUMB;
        }
        catch
        {
            throw;
        }
    }

    private void BindBranchDropDown(OboutDropDownList ddl, string Comp_Code)
    {
        try
        {
            DataTable dtBranch = GetBranchByCompany(Comp_Code);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Select", "Select"));

            if (dtBranch != null && dtBranch.Rows.Count > 0)
            {
                ddl.DataSource = dtBranch;
                ddl.DataTextField = "BRANCH_NAME";
                ddl.DataValueField = "BRANCH_CODE";
                ddl.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetBranchByCompany(string Comp_Code)
    {
        try
        {
            DataTable dtBranch = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMasterByCompCode(Comp_Code);
            return dtBranch;
        }
        catch
        {
            throw;
        }
    }
    private void BindDepartmentDropDown(OboutDropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Select", "Select"));

            DataTable dtDepartment = (DataTable)ViewState["dtDepartment"];
            if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            {
                ddl.DataSource = dtDepartment;
                ddl.DataTextField = "DEPT_NAME";
                ddl.DataValueField = "DEPT_CODE";
                ddl.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    private void BindCompanyDropDown(OboutDropDownList ddl)
    {
        try
        {
            DataTable dtCompany = (DataTable)ViewState["dtCompany"];
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Select", "Select"));

            if (dtCompany != null && dtCompany.Rows.Count > 0)
            {
                ddl.DataSource = dtCompany;
                ddl.DataTextField = "COMP_NAME";
                ddl.DataValueField = "COMP_CODE";
                ddl.DataBind();
            }
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
                ddlIssueShift.DataSource = dtShift;
                ddlIssueShift.DataTextField = "SFT_NAME";
                ddlIssueShift.DataValueField = "SFT_NAME";
                ddlIssueShift.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    private void ActivateSaveMode()
    {
        txtReceiptNumb.Text = string.Empty;
        txtIssueNumb.Text = string.Empty;
        ddlTRNNumber.SelectedIndex = -1;
        ddlTRNNumber.SelectedText = string.Empty;
        ddlTRNNumber.SelectedValue = string.Empty;
        ddlTRNNumber.Items.Clear();

        ddlTRNNumber.Visible = false;

        ddlSourceBranch.Enabled = true;
        ddlSourceCompany.Enabled = true;
        ddlDestinationBranch.Enabled = true;
        ddlDestinationCompany.Enabled = true;
    }
    private void BindCMBPOITEM()
    {
        try
        {

            DataTable data = GetPOData("", 0);

            txtICODE.Items.Clear();
            txtICODE.DataSource = data;
            txtICODE.DataTextField = "FABR_CODE";
            txtICODE.DataValueField = "FABR_CODE";
            txtICODE.DataBind();
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetPOData(string text, int Startoffset)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = new DataTable();

            string commandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT ASD.FABR_CODE, B.FABR_DESC FROM (SELECT a.YEAR, a.COMP_CODE, a.BRANCH_CODE, a.FABR_CODE, c.conf_flag, NVL (a.TRN_QTY, 0) AS TRN_QTY, NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ, NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)AS REMQTY FROM TX_FABRIC_IR_TRN a, TX_FABRIC_IR_MST c WHERE SUBSTR (a.trn_type, 1, 1) IN ('R') AND A.YEAR = c.YEAR AND A.COMP_CODE = c.COMP_CODE AND A.BRANCH_CODE = c.BRANCH_CODE AND A.TRN_NUMB = c.TRN_NUMB AND A.TRN_TYPE = c.TRN_TYPE AND a.YEAR=" + oUserLoginDetail.DT_STARTDATE.Year + " and a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "') asd, TX_FABRIC_MST B WHERE asd.conf_flag = '1' AND REMQTY > 0 AND ASD.FABR_CODE = B.FABR_CODE ORDER BY asd.FABR_CODE ASC) WHERE LTRIM (RTRIM (FABR_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (FABR_desc)) LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (Startoffset != 0)
            {
                whereClause += "  AND FABR_code NOT IN ( SELECT FABR_CODE FROM (SELECT FABR_CODE FROM (SELECT DISTINCT ASD.FABR_CODE, B.FABR_DESC FROM (SELECT a.YEAR, a.COMP_CODE, a.BRANCH_CODE, a.FABR_CODE, c.conf_flag, NVL (a.TRN_QTY, 0) AS TRN_QTY, NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ, NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)AS REMQTY FROM TX_FABRIC_IR_TRN a, TX_FABRIC_IR_MST c WHERE SUBSTR (a.trn_type, 1, 1) IN ('R') AND A.YEAR = c.YEAR AND A.COMP_CODE = c.COMP_CODE AND A.BRANCH_CODE = c.BRANCH_CODE AND A.TRN_NUMB = c.TRN_NUMB AND A.TRN_TYPE = c.TRN_TYPE AND a.YEAR=" + oUserLoginDetail.DT_STARTDATE.Year + " AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "') asd, TX_FABRIC_MST B WHERE asd.conf_flag = '1' AND REMQTY > 0 AND ASD.FABR_CODE = B.FABR_CODE ORDER BY asd.FABR_CODE ASC) WHERE LTRIM (RTRIM (FABR_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (FABR_desc)) LIKE :SearchQuery) WHERE ROWNUM <= " + Startoffset + ") ";
            }

            //string SortExpression = " ORDER BY FABR_CODE asc";
            string SearchQuery = text.ToUpper() + "%";
            //string whereClause = " WHERE FABR_CODE like :SearchQuery or FABR_TYPE like :SearchQuery or FABR_DESC like :SearchQuery";
            string sortExpression = " ORDER BY FABR_CODE asc";
            //string commandText = "SELECT * FROM TX_FABRIC_MST ";

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt;

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
            // imgbtnDelete.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to delete this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
            imgbtnSave.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to save this record')");
            imgbtnUpdate.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to update this record')");
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
            string str = txtAmount.Text;
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (dtDetailTBL == null)
                CreateDataTable();

            if (dtDetailTBL.Rows.Count < 15)
            {
                if (txtICODE.SelectedText != "" && txtQTY.Text != "" && txtBasicRate.Text != "")
                {
                    int UNIQUEID = 0;
                    if (ViewState["UNIQUEID"] != null)
                        UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                    bool bb = SearchItemCodeInGrid(txtICODE.SelectedText.Trim(), UNIQUEID);
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

                                    dv[0]["PO_NUMB"] = 999998;
                                    dv[0]["PO_TYPE"] = "MII";
                                    dv[0]["PO_COMP_CODE"] = "C99999";
                                    dv[0]["PO_BRANCH"] = "B99999";
                                    dv[0]["FABR_CODE"] = txtICODE.SelectedText.Trim();
                                    dv[0]["FABR_DESC"] = txtDESC.Text.Trim();
                                    dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                    dv[0]["UOM"] = txtUNIT.Text.Trim();
                                    dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["AMOUNT"] = double.Parse(txtQTY.Text.Trim()) * double.Parse(txtBasicRate.Text.Trim());
                                    dv[0]["COST_CODE"] = "NA";
                                    dv[0]["MAC_CODE"] = "NA";
                                    dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                    dv[0]["QCFLAG"] = "No";
                                    DateTime dd = System.DateTime.Now;
                                    dv[0]["DATE_OF_MFG"] = dd;

                                    dv[0]["NO_OF_UNIT"] = 0;
                                    dv[0]["UOM_OF_UNIT"] = "NA";
                                    dv[0]["WEIGHT_OF_UNIT"] = 0;
                                    dv[0]["PI_NO"] = "NA";
                                    if (cmbShade.SelectedValue.ToString() == string.Empty || cmbShade.SelectedValue.ToString() == "" || cmbShade.SelectedValue.ToString() == null)
                                    {
                                        dv[0]["SHADE_CODE"] = "NA";
                                    }
                                    else
                                    {
                                        dv[0]["SHADE_CODE"] = cmbShade.SelectedItem.ToString();
                                    }
                                    //dv[0]["PO_RATE"] = 0;

                                    dtDetailTBL.AcceptChanges();
                                }
                            }
                            else
                            {
                                DataRow dr = dtDetailTBL.NewRow();
                                dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                dr["PO_NUMB"] = 999998;
                                dr["PO_TYPE"] = "MII";
                                dr["PO_COMP_CODE"] = "C99999";
                                dr["PO_BRANCH"] = "B99999";
                                dr["FABR_CODE"] = txtICODE.SelectedText.Trim();
                                dr["FABR_DESC"] = txtDESC.Text.Trim();
                                dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dr["UOM"] = txtUNIT.Text.Trim();
                                dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                                dr["AMOUNT"] = double.Parse(txtQTY.Text.Trim()) * double.Parse(txtBasicRate.Text.Trim());
                                dr["COST_CODE"] = "NA";
                                dr["MAC_CODE"] = "NA";
                                dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                dr["QCFLAG"] = "No";
                                DateTime dd = System.DateTime.Now;
                                dr["DATE_OF_MFG"] = dd;

                                dr["NO_OF_UNIT"] = 0;
                                dr["UOM_OF_UNIT"] = "NA";
                                dr["WEIGHT_OF_UNIT"] = 0;
                                dr["PI_NO"] = "NA";
                                string SHADE_CODE = cmbShade.SelectedValue.ToString();
                                if (SHADE_CODE == string.Empty || SHADE_CODE == "" || SHADE_CODE == null)
                                {
                                    dr["SHADE_CODE"] = "NA";
                                }
                                else
                                {
                                    dr["SHADE_CODE"] = cmbShade.SelectedItem.ToString();
                                }
                                // dr["PO_RATE"] = 0;
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid Fabric code');", true);
                    }
                }
                ViewState["dtDetailTBL"] = dtDetailTBL;
                grdMaterialItemIssue.DataSource = dtDetailTBL;
                grdMaterialItemIssue.DataBind();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one  Order.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding  data.\r\nSee error log for detail."));

        }
    }
    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdMaterialItemIssue.Rows)
            {
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                if (txtICODE.Text.Trim() == ItemCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refresh detail information.\r\nSee error log for detail."));
        }
    }
    protected void btnAdjRec_Click(object sender, EventArgs e)
    {
        try
        {
            string URL = "Tx_Farbric_Recipt_Adjustment.aspx";
            URL = URL + "?ItemCodeId=" + txtICODE.SelectedValue.Trim();
            URL = URL + "&TextBoxId=" + txtQTY.ClientID;
            URL = URL + "&txtBasicRate=" + txtBasicRate.ClientID;
            URL = URL + "&AmountId=" + txtAmount.ClientID;
            //   URL = URL + "&CombineId=" + btnAdjRec.CommandArgument.ToString();
            URL = URL + "&ChallanNo=" + txtIssueNumb.Text.Trim();
            URL = URL + "&TRN_TYPE=" + RECEIPT_TYPE;
            string SHADE_CODE = cmbShade.SelectedValue.ToString();
            if ((SHADE_CODE == String.Empty) || (SHADE_CODE == "") || (SHADE_CODE == null))
            {
                URL = URL + "&SHADE_CODE=" + "NA";
            }
            else
            {
                URL = URL + "&SHADE_CODE=" + cmbShade.SelectedValue.ToString();
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=400,height=320,left=200,top=300');", true);
            txtQTY.ReadOnly = false;
            txtAmount.ReadOnly = false;
            txtBasicRate.ReadOnly = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adjusting Receiving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                saveMaterialIssue();
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
    private void saveMaterialIssue()
    {
       try{
           SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
           Hashtable htIssue = new Hashtable();
        SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
        oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_FABRIC_IR_MST.BRANCH_CODE = ddlSourceBranch.SelectedValue.Trim();
        oTX_FABRIC_IR_MST.COMP_CODE = ddlSourceCompany.SelectedValue.Trim();
        oTX_FABRIC_IR_MST.DEPT_CODE = ddlSourceDepartment.SelectedValue.Trim();
        oTX_FABRIC_IR_MST.FORM_NUMB = "";
        oTX_FABRIC_IR_MST.FORM_TYPE = "";
        DateTime dt = System.DateTime.Now.Date;
        oTX_FABRIC_IR_MST.GATE_DATE = dt;
        bool Is_Gate_Entry = false;
        htIssue.Add("GATE_ENTRY", Is_Gate_Entry);
        oTX_FABRIC_IR_MST.GATE_NUMB = "";
        oTX_FABRIC_IR_MST.GATE_OUT_NUMB = "";
        oTX_FABRIC_IR_MST.GATE_PASS_TYPE = "";
        oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());
        dt = System.DateTime.Now.Date;
        bool Is_LR = false;
        htIssue.Add("LR", Is_LR);
        oTX_FABRIC_IR_MST.LR_DATE = dt;

        oTX_FABRIC_IR_MST.LR_NUMB = "";
        dt = System.DateTime.Now.Date;
        bool Is_Party_challan = false;
        Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
        htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
        oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;
        oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
        oTX_FABRIC_IR_MST.PRTY_CODE = "NA";
        oTX_FABRIC_IR_MST.PRTY_NAME = "NA";
        oTX_FABRIC_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
        oTX_FABRIC_IR_MST.REPROCESS = "NA";
        oTX_FABRIC_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();
        dt = System.DateTime.Now.Date;
        bool Is_MRN = false;
        Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
        htIssue.Add("MRN", Is_MRN);
        oTX_FABRIC_IR_MST.TRN_DATE = dt;
        oTX_FABRIC_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIssueNumb.Text.Trim()));
        oTX_FABRIC_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(ISSUE_TYPE);
        oTX_FABRIC_IR_MST.TRSP_CODE = "NA";
        oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
        oTX_FABRIC_IR_MST.LOT_ID_NO = "NA";
        oTX_FABRIC_IR_MST.ST_YEAR = 0000;
        oTX_FABRIC_IR_MST.ST_COMP_CODE = "NA";
        oTX_FABRIC_IR_MST.ST_BRANCH_CODE = "NA";
        oTX_FABRIC_IR_MST.ST_TRN_TYPE = "NA";
        oTX_FABRIC_IR_MST.ST_TRN_NUMB = 0;
        oTX_FABRIC_IR_MST.CONSIGNEE_CODE = "NA";
        oTX_FABRIC_IR_MST.REC_BRANCH_CODE = "NA";
        oTX_FABRIC_IR_MST.TO_DEPT_CODE = "NA";
        DataTable dtFabricReceipt = (DataTable)Session["dtItemReceipt"];
        if (ViewState["dtDetailTBL"] != null)
        {
            dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
        }

        if (dtDetailTBL == null)
            CreateDataTable();
        int TRN_NUMB = 0;
         bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.Insert(oTX_FABRIC_IR_MST, out TRN_NUMB, dtDetailTBL, dtFabricReceipt, htIssue);
         if (result)
         {
             int ReceiptNumber = int.Parse(txtReceiptNumb.Text.Trim());
             if (saveMaterialReceipt(out ReceiptNumber))
             {
                
                 //CommonFuction.ShowMessage(@"Issue Number : " + TRN_NUMB + " saved successfully.\r\nReceipt Number : " + ReceiptNumber + " saved suyccessfully");
                 InitialisePage();
                 CommonFuction.ShowMessage(@"Issue Number :: " + TRN_NUMB + " saved successfully..\r\nReceipt Number : " + ReceiptNumber + " saved suyccessfully");
             }
         }
         else
         {
             CommonFuction.ShowMessage("data  Saving Failed");
         }
         }
        catch (Exception exe)
        {
            throw exe;
        }


    }
    private bool saveMaterialReceipt(out int RECEIPT_NUMB)
    {
        RECEIPT_NUMB = 0;
        try
        {
            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = ddlDestinationBranch.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.COMP_CODE = ddlDestinationCompany.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.DEPT_CODE = ddlDestinationDepartment.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.FORM_NUMB = "";
            oTX_FABRIC_IR_MST.FORM_TYPE = "";

            DateTime dt = System.DateTime.Now.Date;
            oTX_FABRIC_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oTX_FABRIC_IR_MST.GATE_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = "";
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;

            oTX_FABRIC_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = "NA";
            oTX_FABRIC_IR_MST.PRTY_NAME = "NA";
            oTX_FABRIC_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_IR_MST.REPROCESS = string.Empty;
            oTX_FABRIC_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oTX_FABRIC_IR_MST.TRN_DATE = dt;

            oTX_FABRIC_IR_MST.TRN_NUMB = RECEIPT_NUMB;
            oTX_FABRIC_IR_MST.TRN_TYPE = RECEIPT_TYPE;
            oTX_FABRIC_IR_MST.TRSP_CODE = "NA";
            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_FABRIC_IR_MST.LOT_ID_NO = "NA";


            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.Insert(oTX_FABRIC_IR_MST, out RECEIPT_NUMB, dtDetailTBL, htIssue);
            return result;
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
            bool ReturnResult = false;

            int count = 0;
            msg = string.Empty;
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (dtDetailTBL == null)
                CreateDataTable();

            //if (txtChallanNumber.Text != string.Empty)
            //{
            //    count += 1;
            //}
            //else
            //{
            //    msg += @"#. Issue Number required.\r\n";
            //}
            if (ddlDestinationCompany.SelectedIndex > 0 || ddlDestinationCompany.SelectedValue.Trim() != "Select")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Destination Company first.\r\n";
            }

            if (ddlSourceCompany.SelectedIndex > 0 || ddlSourceCompany.SelectedValue.Trim() != "Select")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Source Company first.\r\n";
            }

            if (ddlDestinationBranch.SelectedIndex > 0 || ddlDestinationBranch.SelectedValue.Trim() != "Select")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Destination Branch first.\r\n";
            }

            if (ddlSourceBranch.SelectedIndex > 0 || ddlSourceBranch.SelectedValue.Trim() != "Select")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Source Branch first.\r\n";
            }

            if (ddlSourceDepartment.SelectedIndex > 0 || ddlSourceDepartment.SelectedValue.Trim() != "Select")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Source Department first.\r\n";
            }

            if (ddlDestinationDepartment.SelectedIndex > 0 || ddlDestinationDepartment.SelectedValue.Trim() != "Select")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select Destination Department first.\r\n";
            }

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one item detail for receiving.\r\n";
            }

            if (count == 7)
                ReturnResult = true;

            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                UpdateMaterialIssue();
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
    private void UpdateMaterialIssue()
    {
        try
        {
            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = ddlSourceBranch.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.COMP_CODE = ddlSourceCompany.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.DEPT_CODE = ddlSourceDepartment.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.FORM_NUMB = "";
            oTX_FABRIC_IR_MST.FORM_TYPE = "";
            DateTime dt = System.DateTime.Now.Date;
            oTX_FABRIC_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);
             oTX_FABRIC_IR_MST.GATE_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = "";
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());
            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;
            oTX_FABRIC_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = "NA";
            oTX_FABRIC_IR_MST.PRTY_NAME = "NA";
            oTX_FABRIC_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_IR_MST.REPROCESS = string.Empty;
            oTX_FABRIC_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();
            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oTX_FABRIC_IR_MST.TRN_DATE = dt;
            oTX_FABRIC_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIssueNumb.Text.Trim()));
            oTX_FABRIC_IR_MST.TRN_TYPE = ISSUE_TYPE;
            oTX_FABRIC_IR_MST.TRSP_CODE = "NA";

            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_FABRIC_IR_MST.ST_YEAR = 0000;
            oTX_FABRIC_IR_MST.ST_COMP_CODE = "NA";
            oTX_FABRIC_IR_MST.ST_BRANCH_CODE = "NA";
            oTX_FABRIC_IR_MST.ST_TRN_TYPE = "NA";
            oTX_FABRIC_IR_MST.ST_TRN_NUMB = 0;
            oTX_FABRIC_IR_MST.CONSIGNEE_CODE = "NA";
            oTX_FABRIC_IR_MST.REC_BRANCH_CODE = "NA";
            oTX_FABRIC_IR_MST.TO_DEPT_CODE = "NA";
            DataTable dtFabricReceipt = (DataTable)Session["dtItemReceipt"];

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null)
                CreateDataTable();

            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.Update(oTX_FABRIC_IR_MST, dtDetailTBL, dtFabricReceipt, htIssue);
            if (result)
            {
                int REceipt_numb = int.Parse(txtReceiptNumb.Text.Trim()); 
                if (UpdateMaterialReceipt(REceipt_numb))
                {
                    
                    //CommonFuction.ShowMessage(@"Issue Number : " + int.Parse(CommonFuction.funFixQuotes(txtIssueNumb.Text.Trim())) + " saved successfully.\r\nReceipt Number : " + REceipt_numb + " saved suyccessfully");
                    InitialisePage();
                    CommonFuction.ShowMessage(@"Issue Number : " + int.Parse(CommonFuction.funFixQuotes(txtIssueNumb.Text.Trim())) + " saved successfully.\r\nReceipt Number : " + REceipt_numb + " saved suyccessfully");
                }
            }
            else
            {
                CommonFuction.ShowMessage("data updation Failed");
            }

        }
        catch (Exception exe)
        {
            throw exe;
        }
    }
    private bool UpdateMaterialReceipt(int RECEIPT_NUMB)
    {
        try
        {
            Hashtable htIssue = new Hashtable();
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FABRIC_IR_MST oTX_FABRIC_IR_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IR_MST();
            oTX_FABRIC_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_IR_MST.BRANCH_CODE = ddlDestinationBranch.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.COMP_CODE = ddlDestinationCompany.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.DEPT_CODE = ddlDestinationDepartment.SelectedValue.Trim();
            oTX_FABRIC_IR_MST.FORM_NUMB = "";
            oTX_FABRIC_IR_MST.FORM_TYPE = "";

            DateTime dt = System.DateTime.Now.Date;
            oTX_FABRIC_IR_MST.GATE_DATE = dt;
            bool Is_Gate_Entry = false;
            htIssue.Add("GATE_ENTRY", Is_Gate_Entry);

            oTX_FABRIC_IR_MST.GATE_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_OUT_NUMB = "";
            oTX_FABRIC_IR_MST.GATE_PASS_TYPE = "";
            oTX_FABRIC_IR_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtVehicleNo.Text.Trim());

            dt = System.DateTime.Now.Date;
            bool Is_LR = false;
            htIssue.Add("LR", Is_LR);
            oTX_FABRIC_IR_MST.LR_DATE = dt;

            oTX_FABRIC_IR_MST.LR_NUMB = "";

            dt = System.DateTime.Now.Date;
            bool Is_Party_challan = false;
            Is_Party_challan = DateTime.TryParse(txtDocDate.Text.Trim(), out dt);
            htIssue.Add("PARTY_CHALLAN", Is_Party_challan);
            oTX_FABRIC_IR_MST.PRTY_CH_DATE = dt;

            oTX_FABRIC_IR_MST.PRTY_CH_NUMB = CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTX_FABRIC_IR_MST.PRTY_CODE = "NA";
            oTX_FABRIC_IR_MST.PRTY_NAME = "NA";
            oTX_FABRIC_IR_MST.RCOMMENT = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_IR_MST.REPROCESS = string.Empty;
            oTX_FABRIC_IR_MST.SHIFT = ddlIssueShift.SelectedValue.Trim();

            dt = System.DateTime.Now.Date;
            bool Is_MRN = false;
            Is_MRN = DateTime.TryParse(txtIssueDate.Text.Trim(), out dt);
            htIssue.Add("MRN", Is_MRN);
            oTX_FABRIC_IR_MST.TRN_DATE = dt;

            oTX_FABRIC_IR_MST.TRN_NUMB = RECEIPT_NUMB;
            oTX_FABRIC_IR_MST.TRN_TYPE = RECEIPT_TYPE;
            oTX_FABRIC_IR_MST.TRSP_CODE = "NA";
            oTX_FABRIC_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_FABRIC_IR_MST.ST_YEAR = 0000;
            oTX_FABRIC_IR_MST.ST_BRANCH_CODE = "NA";
            oTX_FABRIC_IR_MST.CONSIGNEE_CODE = "NA";
            oTX_FABRIC_IR_MST.ST_COMP_CODE = "NA";
            oTX_FABRIC_IR_MST.ST_TRN_NUMB = 0;
            oTX_FABRIC_IR_MST.ST_TRN_TYPE = "NA";
            oTX_FABRIC_IR_MST.REC_BRANCH_CODE = "NA";
            oTX_FABRIC_IR_MST.TO_DEPT_CODE = "NA";
            bool result = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.Update(oTX_FABRIC_IR_MST, dtDetailTBL, htIssue);
            return result;
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

            tdUpdate.Visible = true;
            //tdDelete.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation mode.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearingdata.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
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
    protected void ddlTRNNumber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetStockData(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "TRN_NUMB";
            thisTextBox.DataValueField = "adj_data";
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Indent for updation.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetStockData(string text, int startOffset, int numberOfItems)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string whereClause = " where (TRN_NUMB like :searchQuery ) and TRN_TYPE ='IFS06' ";
            string sortExpression = " order by TRN_NUMB desc";

            string commandText = "select * from (select Distinct (TRN_TYPE|| '@'|| TRN_NUMB|| '@'|| COMP_CODE|| '@'|| BRANCH_CODE)adj_data,TRN_NUMB,TRN_TYPE,COMP_CODE,BRANCH_CODE FROM TX_FABRIC_IR_MST WHERE (YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "'))asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt;
        }
        catch
        {
            throw;
        }
    }


         protected void ddlTRNNumber_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string adj_data = ddlTRNNumber.SelectedValue.Trim();
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();
            FinalTotal = 0;
            int iRecordFound = GetdataByAdjData(adj_data);
            BindGridFromDataTable();
            if (iRecordFound > 0)
            {

            }
            else
            {
                InitialisePage();
                ActivateUpdateMode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Challan No');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Challan Number.\r\nSee error log for detail."));

        }
    }
         private void ActivateUpdateMode()
         {
             try
             {
                 txtReceiptNumb.Text = string.Empty;
                 txtIssueNumb.Text = string.Empty;
                 ddlTRNNumber.SelectedIndex = -1;
                 ddlTRNNumber.SelectedText = string.Empty;
                 ddlTRNNumber.SelectedValue = string.Empty;
                 ddlTRNNumber.Items.Clear();
                 ddlTRNNumber.Visible = true;

             }
             catch
             {
                 throw;
             }
         }
         protected void ddlSourceCompany_SelectedIndexChanged(object sender, EventArgs e)
         {
             try
             {
                 string Comp_code = ddlSourceCompany.SelectedValue.Trim();
                 BindBranchDropDown(ddlSourceBranch, Comp_code);
             }
             catch (Exception ex)
             {

             }
         }
         protected void txtICODE_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
         {
             try
             {
                 DataTable data = GetPOData(e.Text, e.ItemsOffset);
                 txtICODE.Items.Clear();
                 txtICODE.DataSource = data;
                 txtICODE.DataTextField = "FABR_CODE";
                 txtICODE.DataValueField = "FABR_CODE";
                 txtICODE.DataBind();

                 e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

                 e.ItemsCount = GetItemsCount(e.Text.ToUpper());
             }
             catch (Exception ex)
             {
                 CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for fabric detail.\r\nSee error log for detail."));
             }
         }
         protected void ddlSourceBranch_SelectedIndexChanged(object sender, EventArgs e)
         {
             try
             {
                 if (ddlSourceBranch.SelectedValue != "Select")
                     BindIssueNumb();
             }
             catch (Exception ex)
             {
                 CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem .\r\nSee error log for detail."));
             }
         }
         protected void ddlDestinationBranch_SelectedIndexChanged(object sender, EventArgs e)
         {
             try
             {
                 if (ddlDestinationBranch.SelectedValue != "Select")
                     BindReceiptNumb();
             }
             catch (Exception ex)
             {
                 CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refresh detail information.\r\nSee error log for detail."));
             }
         }

     protected int GetItemsCount(string text)
     {
         string CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT ASD.FABR_CODE, B.FABR_DESC FROM (SELECT a.YEAR, a.COMP_CODE, a.BRANCH_CODE, a.FABR_CODE, c.conf_flag, NVL (a.TRN_QTY, 0) AS TRN_QTY, NVL (a.ISS_QTY, 0) AS TRN_QTY_ADJ, NVL (a.TRN_QTY, 0) - NVL (a.ISS_QTY, 0)AS REMQTY FROM TX_FABRIC_IR_TRN a, TX_FABRIC_IR_MST c WHERE SUBSTR (a.trn_type, 1, 1) IN ('R')  AND A.COMP_CODE = c.COMP_CODE AND A.BRANCH_CODE = c.BRANCH_CODE AND A.TRN_NUMB = c.TRN_NUMB AND A.TRN_TYPE = c.TRN_TYPE  AND a.Comp_Code = '" + oUserLoginDetail.COMP_CODE + "' AND a.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "') asd, TX_FABR_MST B WHERE asd.conf_flag = '1' AND REMQTY > 0 AND ASD.FABR_CODE = B.FABR_CODE ORDER BY asd.FABR_CODE ASC) WHERE LTRIM (RTRIM (FABR_CODE)) LIKE :SearchQuery OR LTRIM (RTRIM (fabr_desc)) LIKE :SearchQuery) ";
         string WhereClause = " ";
         string SortExpression = " ORDER BY FABR_CODE asc";
         string SearchQuery = text.ToUpper() + "%";
         DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
         return data.Rows.Count;
     }
     protected void txtICODE_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
     {
         try
         {
             string cString = txtICODE.SelectedValue.Trim();
             BindShadeByFabrCode(cString.Trim());
             GetDataForDetail(cString);
             btnAdjRec.Focus();
             txtQTY.Focus();
         }
         catch (Exception ex)
         {
             CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting data.\r\nSee error log for detail."));
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
     protected void grdMaterialItemIssue_RowCommand(object sender, GridViewCommandEventArgs e)
     {
         try
         {
             int UniqueId = int.Parse(e.CommandArgument.ToString());
             if (e.CommandName == "EditItem")
             {
                 EditItemReceiptRow(UniqueId);
             }
             else if (e.CommandName == "DelItem")
             {
                 deleteItemIssueRow(UniqueId);
                 BindGridFromDataTable();
             }

         }
         catch (Exception ex)
         {
             CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Editing/ deletion of detail.\r\nSee error log for detail."));
         }
     }
     private void deleteItemIssueRow(int UniqueId)
     {
         try
         {
             if (ViewState["dtDetailTBL"] != null)
             {
                 dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
             }
             if (dtDetailTBL == null)
                 CreateDataTable();
             if (dtDetailTBL.Rows.Count == 1)
             {
                 dtDetailTBL.Rows.Clear();
             }
             else
             {
                 foreach (DataRow dr in dtDetailTBL.Rows)
                 {
                     int iUniqueId = int.Parse(dr["UNIQUEID"].ToString());
                     if (iUniqueId == UniqueId)
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
             ViewState["dtDetailTBL"] = dtDetailTBL;
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
             {
                 dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
             }
             if (dtDetailTBL == null)
                 CreateDataTable();

             DataView dv = new DataView(dtDetailTBL);
             dv.RowFilter = "UNIQUEID=" + UNIQUEID;
             if (dv.Count > 0)
             {
                 ViewState["UNIQUEID"] = UNIQUEID;

                 BindCMBPOITEM();
                 txtICODE.SelectedText = dv[0]["FABR_CODE"].ToString();
                 txtICODE.SelectedValue = dv[0]["FABR_CODE"].ToString();

                 foreach (ComboBoxItem item in txtICODE.Items)
                 {
                     if (item.Text == dv[0]["FABR_CODE"].ToString().Trim())
                     {
                         txtICODE.SelectedIndex = txtICODE.Items.IndexOf(item);
                         break;
                     }
                 }


                 //  lblPO_TYPE.Text = dv[0]["PO_TYPE"].ToString();
                 //   lblPO_COMP.Text = dv[0]["PO_COMP_CODE"].ToString();
                 // lblPO_BRANCH.Text = dv[0]["PO_BRANCH"].ToString();
                 // txtICODE.Text = dv[0]["ITEM_CODE"].ToString();
                 txtDESC.Text = dv[0]["FABR_DESC"].ToString();
                 txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                 txtUNIT.Text = dv[0]["UOM"].ToString();
                 txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                 txtAmount.Text = dv[0]["AMOUNT"].ToString();
                 //txtCostCode.Text = dv[0]["COST_CODE"].ToString();
                 //txtMacCode.Text = dv[0]["MAC_CODE"].ToString();
                 txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                 BindShadeByFabrCode(dv[0]["FABR_CODE"].ToString());


             }
         }
         catch
         {
             throw;
         }
     }

     private void GetDataForDetail(string Fabr_Code)
     {
         try
         {
             int UNIQUEID = 0;
             if (ViewState["UNIQUEID"] != null)
                 UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
             if (!SearchItemCodeInGrid(Fabr_Code, UNIQUEID))
             {
                 DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricDetailByFabricCode(Fabr_Code);

                 if (dt != null && dt.Rows.Count > 0)
                 {
                     txtICODE.SelectedText = dt.Rows[0]["FABR_CODE"].ToString().Trim();
                     txtDESC.Text = dt.Rows[0]["FABR_DESC"].ToString().Trim();
                     txtUNIT.Text = dt.Rows[0]["UOM"].ToString().Trim();
                     txtQTY.Text = "0";
                     txtBasicRate.Text = dt.Rows[0]["OP_RATE"].ToString();
                     CalculateAmount();
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
     private void CalculateAmount()
     {
         try
         {
             double Qty = 0;
             double Rate = 0;
             double Amount = 0;
             double.TryParse(CommonFuction.funFixQuotes(txtQTY.Text.Trim()), out Qty);
             double.TryParse(CommonFuction.funFixQuotes(txtBasicRate.Text.Trim()), out Rate);
             double.TryParse(CommonFuction.funFixQuotes(txtAmount.Text.Trim()), out Amount);

             Amount = Rate * Qty;

             txtAmount.Text = Amount.ToString();
             txtBasicRate.Text = Rate.ToString();
             txtQTY.Text = Qty.ToString();
         }
         catch
         {
             throw;
         }
     }

         protected void ddlDestinationCompany_SelectedIndexChanged(object sender, EventArgs e)
         {
             try
             {
                 string Comp_code = ddlDestinationCompany.SelectedValue.Trim();
                 BindBranchDropDown(ddlDestinationBranch, Comp_code);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
         private void BindGridFromDataTable()
         {
             try
             {
                 if (ViewState["dtDetailTBL"] != null)
                 {
                     dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                 }

                 if (dtDetailTBL == null)
                     CreateDataTable();
                 grdMaterialItemIssue.DataSource = dtDetailTBL;
                 grdMaterialItemIssue.DataBind();
             }
             catch
             {
                 throw;
             }
         }
         private int GetdataByAdjData(string adjData)
         {
             SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
             int iRecordFound = 0;
             if (ViewState["dtDetailTBL"] != null)
             {
                 dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
             }

             if (dtDetailTBL == null)
                 CreateDataTable();

             try
             {
                 DataSet ds = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetStockDataForUpdate(oUserLoginDetail.DT_STARTDATE.Year, adjData);

                 if (ds != null && ds.Tables.Count > 0)
                 {
                     iRecordFound = 1;

                     BindDestinationDataForUpdate(ds.Tables[0]);
                     BindSourceDataForUpdate(ds.Tables[1]);
                 }
                 if (iRecordFound == 1)
                 {
                     dtDetailTBL.Rows.Clear();

                     dtDetailTBL = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetIssueTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, ds.Tables[0].Rows[0]["COMP_CODE"].ToString(), ds.Tables[0].Rows[0]["BRANCH_CODE"].ToString(), int.Parse(ds.Tables[0].Rows[0]["TRN_NUMB"].ToString()), RECEIPT_TYPE);
                     ViewState["dtDetailTBL"] = dtDetailTBL;

                     MapDataTable();
                 }
                 else
                 {
                     //string msg = "Dear " + oUserLoginDetail.Username + " !! MRN already approved. Modification not allowed.";
                     //Common.CommonFuction.ShowMessage(msg);

                     InitialisePage();

                     //       txtChallanNumber.Text = "";
                     //    ddlTRNNumber.Focus();

                     lblMode.Text = "Update";

                     ActivateUpdateMode();
                     RefreshDetailRow();
                 }
                 return iRecordFound;
             }
             catch
             {
                 throw;
             }
         }
         private void BindDestinationDataForUpdate(DataTable dtReceive)
         {
             try
             {
                 if (dtReceive != null && dtReceive.Rows.Count > 0)
                 {
                     BindCompanyDropDown(ddlDestinationCompany);
                     ddlDestinationCompany.SelectedValue = dtReceive.Rows[0]["COMP_CODE"].ToString();
                     BindBranchDropDown(ddlDestinationBranch, ddlDestinationCompany.SelectedValue.Trim());
                     ddlDestinationBranch.SelectedValue = dtReceive.Rows[0]["BRANCH_CODE"].ToString();
                     BindDepartmentDropDown(ddlDestinationDepartment);
                     ddlDestinationDepartment.SelectedValue = dtReceive.Rows[0]["DEPT_CODE"].ToString();
                     txtReceiptNumb.Text = dtReceive.Rows[0]["TRN_NUMB"].ToString();

                     ddlDestinationCompany.Enabled = false;
                     ddlDestinationBranch.Enabled = false;
                 }
             }
             catch
             {
                 throw;
             }
         }
         private void BindSourceDataForUpdate(DataTable dtIssue)
         {
             try
             {
                 if (dtIssue != null && dtIssue.Rows.Count > 0)
                 {
                     BindCompanyDropDown(ddlSourceCompany);
                     ddlSourceCompany.SelectedValue = dtIssue.Rows[0]["COMP_CODE"].ToString();
                     BindBranchDropDown(ddlSourceBranch, ddlSourceCompany.SelectedValue.Trim());
                     ddlSourceBranch.SelectedValue = dtIssue.Rows[0]["BRANCH_CODE"].ToString();
                     BindDepartmentDropDown(ddlSourceDepartment);
                     ddlSourceDepartment.SelectedValue = dtIssue.Rows[0]["DEPT_CODE"].ToString();
                     txtIssueNumb.Text = dtIssue.Rows[0]["TRN_NUMB"].ToString();
                     txtIssueDate.Text = DateTime.Parse(dtIssue.Rows[0]["TRN_DATE"].ToString()).ToShortDateString();
                     ddlIssueShift.SelectedValue = dtIssue.Rows[0]["SHIFT"].ToString();
                     txtDocNo.Text = dtIssue.Rows[0]["PRTY_CH_NUMB"].ToString();
                     txtDocDate.Text = dtIssue.Rows[0]["PRTY_CH_DATE"].ToString();
                     txtVehicleNo.Text = dtIssue.Rows[0]["LORY_NUMB"].ToString();
                     txtRemarks.Text = dtIssue.Rows[0]["RCOMMENT"].ToString();
                     ddlSourceCompany.Enabled = false;
                     ddlSourceBranch.Enabled = false;
                 }
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
                 if (ViewState["dtDetailTBL"] != null)
                 {
                     dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
                 }
                 if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                     dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));

                 for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
                 {
                     dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                 }
                 ViewState["dtDetailTBL"] = dtDetailTBL;
             }
             catch
             {
                 throw;
             }
         }
         protected void txtQTY_TextChanged(object sender, EventArgs e)
         {
             try
             {
                 CalculateAmount();

                 txtBasicRate.ReadOnly = true;
                 txtAmount.ReadOnly = true;
             }
             catch (Exception ex)
             {
                 CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adjusting Receiving.\r\nSee error log for detail."));
                 lblMode.Text = ex.ToString();
             }
         }
}
