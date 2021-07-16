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

public partial class Module_WorkOrder_Controls_IssueAgainstWorkOrder : System.Web.UI.UserControl
{
    private DataTable dtDepotTRN_SUB = null;
    private static DateTime StartDate;
    private static DateTime EndDate;
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    private static string TRN_TYPE = "IYS11";
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            TRN_TYPE = "IYS11";
            ActivateSaveMode();
            Blankrecords();
            BindNewMRNNum();
            BindShift();
            BindDropDown(ddlLocation);
            BindDepartment(ddlStore);
            StartDate = oUserLoginDetail.DT_STARTDATE;
            EndDate = Common.CommonFuction.GetYearEndDate(StartDate);

            txtMRNDate.Text = System.DateTime.Now.ToShortDateString();
            Session["dtTRN_SUB"] = null;

            ViewState["dtDepotTRN_SUB"] = null;
            Session["dtItemReceipt"] = null;
            Session["dtDepotRate1"] = null;
            BindGridFromDataTable();
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

    private void Blankrecords()
    {
        try
        {
            ddlReceiptShift.SelectedIndex = 0;
            ddlJoberParty.SelectedIndex = -1;
            txtJoberPartyCode.Text = string.Empty;
            txtJoberPartyAddress.Text = string.Empty;
            txtProductType.Text = string.Empty;
            txtcategoryType.Text = string.Empty;
            lblPartyCode.Text = string.Empty;
            txtPartyAddress.Text = string.Empty;
            ddlTransporterCode.SelectedIndex = -1;
            txtTransporterCode.Text = string.Empty;
            txtTransporterAdd.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtLRDate.Text = string.Empty;
            txtLRNo.Text = string.Empty;

            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB != null)
                dtDepotTRN_SUB.Rows.Clear();

            lblMode.Text = "You are in Save Mode";

            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            }
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

    private void CreateDataTable()
    {
        try
        {
            dtDepotTRN_SUB = new DataTable();
            dtDepotTRN_SUB.Columns.Add("UNIQUEID", typeof(int));
            dtDepotTRN_SUB.Columns.Add("TRNNUMB", typeof(int));
            dtDepotTRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtDepotTRN_SUB.Columns.Add("YARN_CODE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("SHADE_CODE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("SHADE_FAMILY", typeof(string));
            dtDepotTRN_SUB.Columns.Add("YARN_DESC", typeof(string));
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
            dtDepotTRN_SUB.Columns.Add("PO_YEAR", typeof(string));
            dtDepotTRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtDepotTRN_SUB.Columns.Add("PI_NO", typeof(string));
            dtDepotTRN_SUB.Columns.Add("REM_QTY", typeof(double));

            dtDepotTRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtDepotTRN_SUB.Columns.Add("GRADE", typeof(string));
            dtDepotTRN_SUB.Columns.Add("GROSS_WT", typeof(double));
            dtDepotTRN_SUB.Columns.Add("TARE_WT", typeof(double));
            dtDepotTRN_SUB.Columns.Add("CARTONS", typeof(double));
            dtDepotTRN_SUB.Columns.Add("NO_OF_PALLET", typeof(string));
            dtDepotTRN_SUB.Columns.Add("JOBER", typeof(string));

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

            grdMaterialItemReceipt.DataSource = dtDepotTRN_SUB;
            grdMaterialItemReceipt.DataBind();
        }
        catch
        {
            throw;
        }
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

    protected void ddlTransporterCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetTransporterData(e.Text.ToUpper(), e.ItemsOffset);

            ddlTransporterCode.Items.Clear();

            ddlTransporterCode.DataSource = data;
            ddlTransporterCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetTransporterCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetTransporterData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('NA') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('NA') and ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = "%" + Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetTransporterCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('NA') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtTransporterAdd.Text = ddlTransporterCode.SelectedValue;
            txtTransporterCode.Text = ddlTransporterCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            tdDelete.Visible = false;
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
            InitialisePage();
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

    private bool SearchItemCodeInGrid(string ItemCode, int PONumb, int UNIQUEID, string ShadeCode)
    {
        bool Result = false;
        try
        {
            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                    Label txtSHADE_CODE = (Label)grdRow.FindControl("txtSHADE_CODE");
                    Label txtPONum = (Label)grdRow.FindControl("txtPONum");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (int.Parse(txtPONum.Text.Trim()) == PONumb && txtICODE.Text.Trim() == ItemCode && UNIQUEID != iUNIQUEID && ShadeCode == txtSHADE_CODE.Text)
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
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            DataTable dtDepotRate1 = (DataTable)Session["dtDepotRate1"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = txtProductType.Text.Trim();
            oYRN_IR_MST.FORM_TYPE = txtcategoryType.Text.Trim();

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
            //oYRN_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtPartyAddress.Text.Trim());
            oYRN_IR_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtJoberPartyAddress.Text.Trim());
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


            if (txtTransporterCode.Text != "")
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.Text.Trim());
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
            oYRN_IR_MST.CONSIGNEE_CODE = CommonFuction.funFixQuotes(txtJoberPartyCode.Text.Trim());


            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.VC_BRANCHNAME;

            int BILLNO = 0;
            int.TryParse("", out BILLNO);
            oYRN_IR_MST.BILL_NUMBER = BILLNO;
            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            double BILLAMOUNT = 0;
            double.TryParse("", out BILLAMOUNT);
            oYRN_IR_MST.PARTY_BILL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.TOTAL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;

            int TRN_NUMB = 0;
            oYRN_IR_MST.LOT_ID_NO = string.Empty;

          //  dtDepotTRN_SUB.Columns.Add("JOBER", typeof(string));
            for (int i = 0; i < dtDepotTRN_SUB.Rows.Count; i++) 
            {

                dtDepotTRN_SUB.Rows[i]["JOBER"] = "NA";
            
            }
           // dtItemReceipt.Columns.Add("DYED_BATCH", typeof(string));
            for (int i = 0; i < dtItemReceipt.Rows.Count; i++)
            {

                dtItemReceipt.Rows[i]["DYED_BATCH"] = "NA";

            }




            bool result = SaitexBL.Interface.Method.YRN_IR_MST.IssueAgainstWorkOrderInsertMst(oYRN_IR_MST, out TRN_NUMB, dtDepotTRN_SUB, dtItemReceipt, htReceive, dtDepotRate1);

            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"Issue against Work Order No : " + TRN_NUMB + " saved successfully.");

            }
            else
            {
                CommonFuction.ShowMessage("Issue against Work Order Saving Failed");
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
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];
            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            DataTable dtDepotRate1 = (DataTable)Session["dtDepotRate1"];

            Hashtable htReceive = new Hashtable();

            SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IR_MST.FORM_NUMB = txtProductType.Text.Trim();
            oYRN_IR_MST.FORM_TYPE = txtcategoryType.Text.Trim();

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

            oYRN_IR_MST.TRN_NUMB = int.Parse(CommonFuction.funFixQuotes(txtTRNNUMBer.Text.Trim()));
            oYRN_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);


            if (txtTransporterCode.Text != "")
                oYRN_IR_MST.TRSP_CODE = "NA";
            else
                oYRN_IR_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.Text.Trim());
            oYRN_IR_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_IR_MST.LOCATION = ddlLocation.SelectedValue;
            oYRN_IR_MST.STORE = ddlStore.SelectedValue;
            oYRN_IR_MST.TO_LOCATION = string.Empty;
            oYRN_IR_MST.TO_STORE = string.Empty;
            oYRN_IR_MST.TO_DEPT_CODE = string.Empty;
            oYRN_IR_MST.CONSIGNEE_CODE = txtJoberPartyCode.Text.Trim();
            

            oYRN_IR_MST.REC_BRANCH_CODE = oUserLoginDetail.VC_BRANCHNAME;

            int BILLNO = 0;
            int.TryParse("", out BILLNO);
            oYRN_IR_MST.BILL_NUMBER = BILLNO;
            oYRN_IR_MST.BILL_TYPE = "YSP";
            oYRN_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            double BILLAMOUNT = 0;
            double.TryParse("", out BILLAMOUNT);
            oYRN_IR_MST.PARTY_BILL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.TOTAL_AMOUNT = BILLAMOUNT;
            oYRN_IR_MST.SPINNER_CODE = string.Empty;

            int TRN_NUMB = 0;
            oYRN_IR_MST.LOT_ID_NO = string.Empty;
            
                for (int i = 0; i < dtDepotTRN_SUB.Rows.Count; i++)
                {
                    if (dtDepotTRN_SUB.Rows[i]["JOBER"].ToString() != string.Empty)
                    {
                        dtDepotTRN_SUB.Rows[i]["JOBER"] = "NA";
                    }
                }
            
           
            for (int i = 0; i < dtItemReceipt.Rows.Count; i++)
            {
                if (dtItemReceipt.Rows[i]["DYED_BATCH"].ToString() != string.Empty)
                {
                    dtItemReceipt.Rows[i]["DYED_BATCH"] = "NA";
                }

            }
            bool result = SaitexBL.Interface.Method.YRN_IR_MST.DepotSaleInvoiceUpdate(oYRN_IR_MST, dtDepotTRN_SUB, dtItemReceipt, htReceive, dtDepotRate1);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Issue against Work Order Updated SuccessFully");

            }
            else
            {
                CommonFuction.ShowMessage("Issue against Work Order updation Failed");
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

        if (ViewState["dtDepotTRN_SUB"] != null)
            dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.JobWorkDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
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
               
                txtRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString().Trim();
                ddlReceiptShift.Text = dt.Rows[0]["SHIFT"].ToString().Trim();


                txtJoberPartyCode.Text = dt.Rows[0]["CONSIGNEE_CODE"].ToString().Trim();
                txtJoberPartyAddress.Text = dt.Rows[0]["PRTY_NAME1"].ToString().Trim();
                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                txtProductType.Text = dt.Rows[0]["FORM_NUMB"].ToString().Trim();
                txtcategoryType.Text = dt.Rows[0]["FORM_TYPE"].ToString().Trim();
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt.Rows[0]["STORE"].ToString().Trim()));
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dt.Rows[0]["LOCATION"].ToString().Trim()));
               
                ddlTransporterCode.SelectedValue = dt.Rows[0]["TAddress"].ToString().Trim();
                txtTransporterCode.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtTransporterAdd.Text = dt.Rows[0]["TAddress"].ToString().Trim();
                //string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)      ";
                //string WhereClause = "  and  (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                //string SortExpression = " order by PRTY_CODE asc";
                //string SearchQuery = "%";
                //DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
                //ddlJoberParty.DataSource = data;
                //ddlJoberParty.DataTextField = "PRTY_CODE";
                //ddlJoberParty.DataValueField = "PRTY_NAME";
                //ddlJoberParty.DataBind();
                //foreach (ComboBoxItem item in ddlJoberParty.Items)
                //{
                //    if (item.Text == dt.Rows[0]["CONSIGNEE_CODE"].ToString().Trim())
                //    {
                //        ddlJoberParty.SelectedIndex = ddlJoberParty.Items.IndexOf(item);

                       
                //        break;
                //    }
                //}

            }
            if (iRecordFound == 1)
            {
                dtDepotTRN_SUB.Rows.Clear();
                dtDepotTRN_SUB = SaitexBL.Interface.Method.YRN_IR_MST.GetWorkOrderTrnDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, TRN_TYPE);
                if (dtDepotTRN_SUB.Rows.Count > 0)
                {
                    MapDataTable();
                    if (dtDepotTRN_SUB != null && dtDepotTRN_SUB.Rows.Count > 0)
                    {
                        DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_IR_MST.GetWorkOrderAdjustReceiving(dtDepotTRN_SUB.Rows[0]["PO_TYPE"].ToString(), Convert.ToInt32(dtDepotTRN_SUB.Rows[0]["PO_NUMB"].ToString()), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
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

            if (!dtDepotTRN_SUB.Columns.Contains("COST_CODE"))
                dtDepotTRN_SUB.Columns.Add("COST_CODE", typeof(string));

            for (int iLoop = 0; iLoop < dtDepotTRN_SUB.Rows.Count; iLoop++)
            {
                dtDepotTRN_SUB.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtDepotTRN_SUB.Rows[iLoop]["MAC_CODE"] = string.Empty;
                dtDepotTRN_SUB.Rows[iLoop]["COST_CODE"] = string.Empty;
            }
            ViewState["dtDepotTRN_SUB"] = dtDepotTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlJoberParty_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetJoberPartyData(e.Text.ToUpper(), e.ItemsOffset);
            ddlJoberParty.Items.Clear();
            ddlJoberParty.DataSource = data;
            ddlJoberParty.DataTextField = "JOBER_PARTY";
            ddlJoberParty.DataValueField = "WORK_DATA";
            ddlJoberParty.DataBind();
            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
            e.ItemsCount = GetJoberPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetJoberPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   * FROM   (  SELECT   * FROM   (SELECT   ( A.JOBER_PARTY || '@' || A.JOBER_NAME || '@' || A.PRODUCT_TYPE || '@' || A.JOB_WORK_CAT|| '@' || A.PRTY_CODE || '@' || A.PRTY_NAME || '@' || A.TRSP_CODE  || '@' || A.TRSP_NAME)  WORK_DATA, (SUM (NVL (B.QTY, 0)) - SUM (NVL (B.QTY_ISS, 0))) AS REM_QTY,  A.JOBER_PARTY,A.WO_NUMB,  A.JOBER_NAME, A.PRODUCT_TYPE, A.JOB_WORK_CAT, A.PRTY_CODE,  A.PRTY_NAME, A.TRSP_CODE, A.TRSP_NAME FROM   V_OD_WO_MST A, OD_WO_TRN_SUB B WHERE   A.WO_TYPE IN ('WUM') AND A.WO_NUMB = B.WO_NUMB   AND A.WO_TYPE = B.WO_TYPE AND A.PRODUCT_TYPE = B.PRODUCT_TYPE  GROUP BY   A.JOBER_PARTY, A.WO_NUMB,  A.JOBER_NAME, A.PRODUCT_TYPE,  A.JOB_WORK_CAT, A.PRTY_CODE, A.PRTY_NAME, A.TRSP_CODE,  A.TRSP_NAME) WHERE   JOBER_PARTY LIKE :SearchQuery OR JOBER_NAME LIKE :SearchQuery  ORDER BY   JOBER_PARTY) WHERE   REM_QTY > 0 AND ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause = " AND JOBER_PARTY NOT IN(SELECT JOBER_PARTY FROM (SELECT   * FROM   (  SELECT   * FROM   (SELECT   ( A.JOBER_PARTY || '@' || A.JOBER_NAME || '@' || A.PRODUCT_TYPE || '@' || A.JOB_WORK_CAT|| '@' || A.PRTY_CODE || '@' || A.PRTY_NAME || '@' || A.TRSP_CODE  || '@' || A.TRSP_NAME)  WORK_DATA,(SUM (NVL (B.QTY, 0)) - SUM (NVL (B.QTY_ISS, 0))) AS REM_QTY,  A.JOBER_PARTY,A.WO_NUMB,  A.JOBER_NAME, A.PRODUCT_TYPE, A.JOB_WORK_CAT, A.PRTY_CODE,  A.PRTY_NAME, A.TRSP_CODE, A.TRSP_NAME FROM   V_OD_WO_MST A, OD_WO_TRN_SUB B WHERE   A.WO_TYPE IN ('WUM') AND A.WO_NUMB = B.WO_NUMB   AND A.WO_TYPE = B.WO_TYPE AND A.PRODUCT_TYPE = B.PRODUCT_TYPE  GROUP BY   A.JOBER_PARTY, A.WO_NUMB,  A.JOBER_NAME, A.PRODUCT_TYPE,  A.JOB_WORK_CAT, A.PRTY_CODE, A.PRTY_NAME, A.TRSP_CODE,  A.TRSP_NAME) WHERE   JOBER_PARTY LIKE :SearchQuery OR JOBER_NAME LIKE :SearchQuery  ORDER BY   JOBER_PARTY) WHERE   REM_QTY > 0 AND ROWNUM <= '" + startOffset + "') ";
            }

            string SortExpression = " order by JOBER_PARTY";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetJoberPartyCount(string text)
    {

        string CommandText = " SELECT * FROM (SELECT   * FROM   (  SELECT   * FROM   (SELECT   ( A.JOBER_PARTY || '@' || A.JOBER_NAME || '@' || A.PRODUCT_TYPE || '@' || A.JOB_WORK_CAT|| '@' || A.PRTY_CODE || '@' || A.PRTY_NAME || '@' || A.TRSP_CODE  || '@' || A.TRSP_NAME)  WORK_DATA, (SUM (NVL (B.QTY, 0)) - SUM (NVL (B.QTY_ISS, 0))) AS REM_QTY,  A.JOBER_PARTY,A.WO_NUMB,  A.JOBER_NAME, A.PRODUCT_TYPE, A.JOB_WORK_CAT, A.PRTY_CODE,  A.PRTY_NAME, A.TRSP_CODE, A.TRSP_NAME FROM   V_OD_WO_MST A, OD_WO_TRN_SUB B WHERE   A.WO_TYPE IN ('WUM') AND A.WO_NUMB = B.WO_NUMB   AND A.WO_TYPE = B.WO_TYPE AND A.PRODUCT_TYPE = B.PRODUCT_TYPE  GROUP BY   A.JOBER_PARTY, A.WO_NUMB,  A.JOBER_NAME, A.PRODUCT_TYPE,  A.JOB_WORK_CAT, A.PRTY_CODE, A.PRTY_NAME, A.TRSP_CODE,  A.TRSP_NAME) WHERE   JOBER_PARTY LIKE :SearchQuery OR JOBER_NAME LIKE :SearchQuery  ORDER BY   JOBER_PARTY) WHERE   REM_QTY > 0 AND ROWNUM <= 15";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void ddlJoberParty_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtJoberPartyCode.Text = ddlJoberParty.SelectedText.ToString();
            string cString = ddlJoberParty.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            txtJoberPartyCode.Text = arrString[0].ToString();
            txtJoberPartyAddress.Text = arrString[1].ToString();
            txtProductType.Text = arrString[2].ToString();
            txtcategoryType.Text = arrString[3].ToString();
            lblPartyCode.Text = arrString[4].ToString();
            txtPartyAddress.Text = arrString[5].ToString();
            //lblTransporterCode.Text = arrString[6].ToString();
            //txtTransporterAddress.Text = arrString[7].ToString();


            //txtJoberPartyAddress.Text = ddlJoberParty.SelectedValue.Trim();
            //txtJoberPartyCode.Text = ddlJoberParty.SelectedText.Trim();
            if (IsUpdateCall != 1)
                ResetDetailOnPartySelection();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
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

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDepotTRN_SUB"] != null)
                dtDepotTRN_SUB = (DataTable)ViewState["dtDepotTRN_SUB"];

            if (dtDepotTRN_SUB == null)
                CreateDataTable();

            if (cmbPOITEM.Text != "" && txtICODE.Text != "" && txtQTY.Text != "" && txtFinalRate.Text != "" && txtShade.Text != "")
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
                                dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dv[0]["YARN_CODE"] = txtICODE.Text.Trim();
                                dv[0]["SHADE_CODE"] = txtShade.Text.Trim();
                                dv[0]["SHADE_FAMILY"] = txtShade.Text.Trim();
                                dv[0]["YARN_DESC"] = txtDESC.Text.Trim();
                                dv[0]["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                                dv[0]["UOM"] = txtUNIT.Text.Trim();
                                dv[0]["BASIC_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dv[0]["NO_OF_UNIT"] = double.Parse(txtnoofunit.Text.Trim());
                                dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                dv[0]["AMOUNT"] = double.Parse(txtQTY.Text.Trim()) * double.Parse(txtFinalRate.Text.Trim());
                                dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                dv[0]["QCFLAG"] = "No";
                                double MAXQTY = 0;
                                double.TryParse(lblMaxQTY.Text, out MAXQTY);
                                dv[0]["REM_QTY"] = MAXQTY;
                                dv[0]["PI_NO"] = "NA";
                                DataTable dtItemReceipt=(DataTable)Session["dtItemReceipt"];
                                dv[0]["LOT_NO"] = dtItemReceipt.Rows[0]["LOT_NO"] ;
                                dv[0]["GRADE"] = "NA";
                                dv[0]["GROSS_WT"] = 0;
                                dv[0]["TARE_WT"] = 0;
                                dv[0]["CARTONS"] = double.Parse(txtCarton.Text.Trim());

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
                            dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["YARN_CODE"] = txtICODE.Text.Trim();
                            dr["SHADE_CODE"] = txtShade.Text.Trim();
                            dr["SHADE_FAMILY"] = txtShade.Text.Trim();
                            dr["YARN_DESC"] = txtDESC.Text.Trim();
                            dr["TRN_QTY"] = double.Parse(txtQTY.Text.Trim());
                            dr["UOM"] = txtUNIT.Text.Trim();
                            dr["NO_OF_UNIT"] = double.Parse(txtnoofunit.Text.Trim());
                            dr["BASIC_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                            dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                            dr["AMOUNT"] = double.Parse(txtQTY.Text.Trim()) * double.Parse(txtFinalRate.Text.Trim());

                            dr["MAC_CODE"] = string.Empty;
                            dr["REMARKS"] = txtDetRemarks.Text.Trim();
                            dr["QCFLAG"] = "No";

                            double MAXQTY = 0;
                            double.TryParse(lblMaxQTY.Text, out MAXQTY);
                            dr["REM_QTY"] = MAXQTY;

                            dr["PI_NO"] = "NA";
                            DataTable dtItemReceipt = (DataTable)Session["dtItemReceipt"];
                            dr["LOT_NO"] = dtItemReceipt.Rows[0]["LOT_NO"];
                            dr["GRADE"] = "NA";
                            dr["GROSS_WT"] = 0;
                            dr["TARE_WT"] = 0;
                            dr["CARTONS"] = double.Parse(txtCarton.Text.Trim());

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
            DataTable data = GetWOData(e.Text.ToUpper(), e.ItemsOffset);
            lblPOITEM.Items.Clear();
            lblPOITEM.DataSource = data;
            lblPOITEM.DataTextField = "WO_NUMB";
            lblPOITEM.DataValueField = "WO_TRN_DATA";
            lblPOITEM.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;

            e.ItemsCount = GetWOCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Item Loading..\r\nSee error log for detail."));
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
                string commandText = "SELECT * FROM (SELECT * FROM (SELECT DISTINCT ( PT.WO_TYPE|| '@'|| PT.WO_NUMB|| '@'|| PT.BASE_ARTICLE_CODE|| '@'||BASE_ARTICLE_DESC|| '@'|| pt.UOM|| '@'|| QTY_REM|| '@'|| pt.BASIC_RATE|| '@'|| pt.FINAL_RATE|| '@'|| pt.comp_code|| '@'|| pt.branch_code ||'@'|| pt.BASE_SHADE_CODE) WO_TRN_DATA, pt.WO_NUMB, PT.BASE_ARTICLE_CODE,pt.BASE_SHADE_CODE, pt.QTY, pt.UOM, PT.BASE_ARTICLE_DESC, NVL (PT.QTY_ISS, 0) QTY_ISS, QTY_REM FROM V_OD_WO_TRN_SUB pt WHERE QTY_REM > 0 AND PT.WO_TYPE IN ('WUM') AND branch_code ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND PRTY_CODE = '" + lblPartyCode.Text + "' ORDER BY WO_NUMB) WHERE WO_NUMB LIKE :SearchQuery OR BASE_ARTICLE_CODE LIKE :SearchQuery  OR BASE_ARTICLE_DESC LIKE :SearchQuery   OR QTY  LIKE :SearchQuery or BASE_SHADE_CODE like :SearchQuery) WHERE ROWNUM <= 15 ";

                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND WO_TRN_DATA NOT IN ( SELECT WO_TRN_DATA FROM (SELECT * FROM (SELECT DISTINCT ( PT.WO_TYPE|| '@'|| PT.WO_NUMB|| '@'|| PT.BASE_ARTICLE_CODE|| '@'||BASE_ARTICLE_DESC|| '@'|| pt.UOM|| '@'|| QTY_REM|| '@'|| pt.BASIC_RATE|| '@'|| pt.FINAL_RATE|| '@'|| pt.comp_code|| '@'|| pt.branch_code ||'@'||pt.BASE_SHADE_CODE) WO_TRN_DATA, pt.WO_NUMB, PT.BASE_ARTICLE_CODE,pt.BASE_SHADE_CODE, pt.QTY, pt.UOM, PT.BASE_ARTICLE_DESC, NVL (PT.QTY_ISS, 0) QTY_ISS, QTY_REM FROM V_OD_WO_TRN_SUB pt WHERE QTY_REM > 0 AND PT.WO_TYPE IN ('WUM') AND branch_code ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND PRTY_CODE = '" + lblPartyCode.Text + "' ORDER BY WO_NUMB) WHERE WO_NUMB LIKE :SearchQuery OR BASE_ARTICLE_CODE LIKE :SearchQuery  OR BASE_ARTICLE_DESC LIKE :SearchQuery   OR QTY  LIKE :SearchQuery or BASE_SHADE_CODE like :SearchQuery) WHERE ROWNUM<='" + startOffset + "')";
                }

                string SortExpression = " ORDER BY WO_NUMB desc";
                string SearchQuery = "%" + text.ToUpper() + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
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

        string CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT ( PT.WO_TYPE|| '@'|| PT.WO_NUMB|| '@'|| PT.BASE_ARTICLE_CODE|| '@'||BASE_ARTICLE_DESC|| '@'|| pt.UOM|| '@'|| QTY_REM|| '@'|| pt.BASIC_RATE|| '@'|| pt.FINAL_RATE|| '@'|| pt.comp_code|| '@'|| pt.branch_code|| '@' ||pt.BASE_SHADE_CODE) WO_TRN_DATA, pt.WO_NUMB, PT.BASE_ARTICLE_CODE,pt.BASE_SHADE_CODE, pt.QTY, pt.UOM, PT.BASE_ARTICLE_DESC, NVL (PT.QTY_ISS, 0) QTY_ISS, QTY_REM FROM V_OD_WO_TRN_SUB pt WHERE QTY_REM > 0 AND PT.WO_TYPE IN ('WUM') AND branch_code ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND PRTY_CODE = '" + lblPartyCode.Text + "' ORDER BY WO_NUMB) WHERE WO_NUMB LIKE :SearchQuery OR BASE_ARTICLE_CODE LIKE :SearchQuery  OR BASE_ARTICLE_DESC LIKE :SearchQuery   OR QTY  LIKE :SearchQuery OR BASE_SHADE_CODE like :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery.ToUpper(), "");

        return data.Rows.Count;
    }

    protected void lblPOITEM_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = lblPOITEM.SelectedValue.ToString().Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string WO_Type = arrString[0].ToString();
            int WONumb = int.Parse(arrString[1].ToString());
            string ArticleCode = arrString[2].ToString();
            string Art_desc = arrString[3].ToString();
            string UOM = arrString[4].ToString();
            double qty_rem = double.Parse(arrString[5].ToString());
            double basic_rate = double.Parse(arrString[6].ToString());
            double final_rate = double.Parse(arrString[7].ToString());
            string wo_comp = arrString[8].ToString();
            string wo_branch = arrString[9].ToString();
            string ShadeCode = arrString[10].ToString();

            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());

            if (!SearchItemCodeInGrid(ArticleCode, WONumb, UNIQUEID, ShadeCode))
            {
                cmbPOITEM.Text = lblPOITEM.SelectedText.ToString().Trim();
                txtICODE.Text = ArticleCode;
                txtDESC.Text = Art_desc;
                txtUNIT.Text = UOM;
                lblMaxQTY.Text = qty_rem.ToString();
                txtFinalRate.Text = final_rate.ToString();
                lblSO_BRANCH.Text = wo_branch;
                lblSO_COMP.Text = wo_comp;
                lblSO_TYPE.Text = WO_Type;
                txtShade.Text = ShadeCode;
                // change code by Arun Sharma 
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Article Already Included");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Item Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            txtQTY.Text = string.Empty;
            txtnoofunit.Text = string.Empty;
            txtUNIT.Text = string.Empty;
            txtFinalRate.Text = string.Empty;
            txtDetRemarks.Text = string.Empty;
            lblSO_BRANCH.Text = string.Empty;
            lblSO_COMP.Text = string.Empty;
            lblSO_TYPE.Text = string.Empty;
            lblMaxQTY.Text = string.Empty;
            txtShade.Text = string.Empty;
            txtCarton.Text = string.Empty;
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
                txtShade.Text = dv[0]["SHADE_CODE"].ToString();
                txtDESC.Text = dv[0]["YARN_DESC"].ToString();
                txtQTY.Text = dv[0]["TRN_QTY"].ToString();
                txtnoofunit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtUNIT.Text = dv[0]["UOM"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                lblMaxQTY.Text = dv[0]["REM_QTY"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
                txtCarton.Text = dv[0]["CARTONS"].ToString();

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
            string commandText = "select * from (Select a.TRN_NUMB, TO_CHAR (a.TRN_DATE, 'DD/MM/YYYY') AS TRN_DATE ,a.PRTY_CODE,b.PRTY_NAME from YRN_IR_MST a, tx_vendor_mst b Where a.prty_code=b.prty_code (+) AND A.CONF_FLAG = 0 and A.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.TRN_TYPE='" + TRN_TYPE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";

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

    protected void txtPartyBillAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dPartyBillAmount = 0;
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
                string URL = "Wo_ReceiptAdjustment.aspx";
                URL = URL + "?ItemCodeId=" + txtICODE.Text;
                URL = URL + "&SHADE_CODE=" + txtShade.Text;
                URL = URL + "&SHADE_FAMILY=" + txtShade.Text;
                URL = URL + "&TextBoxId=" + txtQTY.ClientID;
                URL = URL + "&txtBasicRate=" + txtFinalRate.ClientID;
                URL = URL + "&TRN_TYPE=" + TRN_TYPE;
                URL = URL + "&ChallanNo=" + txtTRNNUMBer.Text;
                URL = URL + "&REMQTY=" + lblMaxQTY.Text;
               
                URL = URL + "&txtQtyUnit=" + txtnoofunit.ClientID;
                URL = URL + "&txtQtyUom=" + txtUNIT.ClientID;
                URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
                URL = URL + "&STORE=" + ddlStore.SelectedValue;
                URL = URL + "&PI_NO=NA";
                URL = URL + "&PARTY_CODE=" + lblPartyCode.Text;
                URL = URL + "&CARTONS=" + txtCarton.ClientID;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                txtnoofunit.ReadOnly = false;

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

}