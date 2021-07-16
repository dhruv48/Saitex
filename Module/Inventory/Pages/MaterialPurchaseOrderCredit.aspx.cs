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
//using DBLibrary;
//using Common;
//using errorLog;
//using Obout.ComboBox;

public partial class Inventory_MaterialPurchaseOrderCredit : System.Web.UI.Page
{

    //private static DataTable dtMaterialPOCredit = null;
    //private static bool UpdateMode = false;
    //private string UserCode;
    //private static double FinalTotal;

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        //    DateTime startdate = oUserLoginDetail.DT_STARTDATE;
        //    DateTime Enddate = CommonFuction.GetYearEndDate(startdate);

        //    if (!IsPostBack)
        //    {
        //        UserCode = Session["urLoginId"].ToString();
        //        InitialisePage();
        //    }
        //    txtFinalTotal.Text = FinalTotal.ToString();
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
        //}
    }
    //private void InitialisePage()
    //{
    //    try
    //    {
    //        txtOrderNumber.AutoPostBack = false;
    //        txtOrderNumber.ReadOnly = true;
    //        lblMode.Text = "Save";

    //        ddlOrderType.Enabled = true;
    //        txtOrderNumber.Enabled = true;
    //        txtFinalTotal.Text = "0";
    //        txtPartyAddress.Text = "";
    //        txtPartyCode.SelectedIndex = -1;
    //        txtRemarks.Text = "";
    //        txtTransporterCode.SelectedIndex = -1;
    //        txtTransporterName.Text = "";
    //        txtDelAddress.Text = "";
    //        txtDespatchMode.Text = "";
    //        txtAdvance.Text = "0";
    //        txtAdvanceAmount.Text = "0";
    //        txtPayTerm.Text = "";

    //        txtDeliveryDate.Text = System.DateTime.Now.Date.ToShortDateString();
    //        txtOrderDate.Text = System.DateTime.Now.Date.ToShortDateString();
    //        UpdateMode = false;
    //        getPOMaxId();
    //        if (dtMaterialPOCredit == null)
    //            CreateMaterialPODetailTable();
    //        dtMaterialPOCredit.Rows.Clear();

    //        BindMaterialPOCredittoGrid();

    //        BindItemCodeCombo("");

    //        RefreshDetailRow();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //private void getPOMaxId()
    //{
    //    try
    //    {
    //        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
    //        txtOrderNumber.Text = (int.Parse(SaitexBL.Interface.Method.Material_Purchase_Order.GetNewPONo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "PUC")) + 1).ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //private void CreateMaterialPODetailTable()
    //{
    //    try
    //    {
    //        dtMaterialPOCredit = new DataTable();
    //        dtMaterialPOCredit.Columns.Add("UniqueId", typeof(int));
    //        dtMaterialPOCredit.Columns.Add("PO_NUMB", typeof(string));
    //        dtMaterialPOCredit.Columns.Add("ITEM_CODE", typeof(string));
    //        dtMaterialPOCredit.Columns.Add("ITEM_DESC", typeof(string));
    //        dtMaterialPOCredit.Columns.Add("ORD_QTY", typeof(int));
    //        dtMaterialPOCredit.Columns.Add("UOM", typeof(string));
    //        dtMaterialPOCredit.Columns.Add("BASIC_RATE", typeof(double));
    //        dtMaterialPOCredit.Columns.Add("FINAL_RATE", typeof(double));
    //        dtMaterialPOCredit.Columns.Add("Amount", typeof(double));
    //        dtMaterialPOCredit.Columns.Add("CURRENCY", typeof(string));
    //        dtMaterialPOCredit.Columns.Add("DEL_DATE", typeof(DateTime));
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
    //    }
    //}
    //private void BindMaterialPOCredittoGrid()
    //{
    //    try
    //    {
    //        gvMaterialPOTRN.DataSource = dtMaterialPOCredit;
    //        gvMaterialPOTRN.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //        throw ex;
    //    }
    //}

    //protected void gvMaterialPOTRN_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        int UniqueId = int.Parse(e.CommandArgument.ToString());

    //        if (e.CommandName == "POMateialCreditDelete")
    //        {
    //            deletePOMaterialCreditRow(UniqueId);
    //            BindMaterialPOCredittoGrid();
    //        }
    //        if (e.CommandName == "POMateialCreditEdit")
    //        {
    //            FillDetailByGrid(UniqueId);
    //        }
    //        CalculateFinalTotal();
    //    }
    //    catch (Exception ex)
    //    {

    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}
    //private void deletePOMaterialCreditRow(int UniqueId)
    //{
    //    try
    //    {
    //        if (gvMaterialPOTRN.Rows.Count == 1)
    //        {
    //            dtMaterialPOCredit.Rows.Clear();
    //        }
    //        else
    //        {
    //            foreach (DataRow dr in dtMaterialPOCredit.Rows)
    //            {
    //                int iUniqueId = int.Parse(dr["UniqueId"].ToString());
    //                if (iUniqueId == UniqueId)
    //                {
    //                    dtMaterialPOCredit.Rows.Remove(dr);
    //                    break;
    //                }
    //            }
    //            int iCount = 0;
    //            foreach (DataRow dr in dtMaterialPOCredit.Rows)
    //            {
    //                iCount = iCount + 1;
    //                dr["UniqueId"] = iCount;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}

    //private int GetItemDetailByItemCode(string ItemCode, out string Description, out string UOM, out double OpeningRate)
    //{
    //    int iRecordFound = 0;
    //    Description = "";
    //    UOM = "";
    //    OpeningRate = 0;

    //    try
    //    {
    //        DataTable dts = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemDetailByItemCode(ItemCode);
    //        if (dts != null && dts.Rows.Count > 0)
    //        {
    //            Description = dts.Rows[0]["ITEM_DESC"].ToString().Trim();
    //            UOM = dts.Rows[0]["UNIT_NAME"].ToString().Trim();
    //            OpeningRate = double.Parse(dts.Rows[0]["OP_RATE"].ToString().Trim());
    //            iRecordFound = dts.Rows.Count;
    //        }
    //        return iRecordFound;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void txtOrderQty_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TextBox thisTextBox = (TextBox)txtOrderQty;
    //        if (thisTextBox.Text != "")
    //        {
    //            int RequestQTY = 0;
    //            if (int.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
    //            {
    //                txtAmount.Text = CalculateAmount().ToString();
    //            }
    //            else
    //            {
    //                thisTextBox.Text = "0";
    //            }
    //        }
    //        thisTextBox.ReadOnly = true;
    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}
    //protected void txtFinalRate_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TextBox thisTextBox = (TextBox)txtFinalRate;
    //        if (thisTextBox.Text != "")
    //        {
    //            double RequestQTY = 0;
    //            if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
    //            {

    //                txtAmount.Text = CalculateAmount().ToString();
    //            }
    //            else
    //            {
    //                thisTextBox.Text = "0";
    //            }
    //        }
    //        thisTextBox.ReadOnly = true;
    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}
    //private double CalculateAmount()
    //{
    //    try
    //    {
    //        int OrderQty = 0;
    //        double BaseRate = 0f;
    //        double FinalRate = 0f;
    //        double Amount = 0f;
    //        int.TryParse(CommonFuction.funFixQuotes(txtOrderQty.Text.Trim()), out OrderQty);
    //        double.TryParse(CommonFuction.funFixQuotes(txtBaseRate.Text.Trim()), out BaseRate);
    //        double.TryParse(CommonFuction.funFixQuotes(txtFinalRate.Text.Trim()), out FinalRate);

    //        Amount = OrderQty * FinalRate;
    //        return Amount;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        if (dtMaterialPOCredit != null && dtMaterialPOCredit.Rows.Count > 0 && txtPartyCode.SelectedIndex >= 0)
    //        {
    //            if (UpdateMode)
    //            {
    //                UpdateMaterialPOCreditMST();
    //            }
    //            else
    //            {
    //                saveMaterialPOOrderCreditMST();
    //            }
    //        }
    //        else if (dtMaterialPOCredit == null || dtMaterialPOCredit.Rows.Count == 0)
    //        {
    //            CommonFuction.ShowMessage("Please select atleast one item to generate purchase order");
    //        }
    //        else if (txtPartyCode.SelectedIndex < 0)
    //        {
    //            CommonFuction.ShowMessage("Please select party to generate purchase order");
    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}

    //private void saveMaterialPOOrderCreditMST()
    //{
    //    try
    //    {
    //        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
    //        SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();

    //        oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //        oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
    //        oTX_ITEM_PU_MST.PO_TYPE = "PUM";
    //        oTX_ITEM_PU_MST.PO_NUMB = int.Parse(txtOrderNumber.Text.Trim());
    //        oTX_ITEM_PU_MST.PO_DATE = DateTime.Parse(txtOrderDate.Text.Trim());
    //        oTX_ITEM_PU_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
    //        oTX_ITEM_PU_MST.DEL_DATE = DateTime.Parse(txtDeliveryDate.Text.Trim());
    //        oTX_ITEM_PU_MST.PAY_TERM = CommonFuction.funFixQuotes(txtPayTerm.Text.Trim());
    //        if (radConfirm.SelectedItem.Value == "Yes")
    //            oTX_ITEM_PU_MST.CONF_FLAG = true;
    //        else
    //            oTX_ITEM_PU_MST.CONF_FLAG = false;
    //        oTX_ITEM_PU_MST.COMMENTS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
    //        oTX_ITEM_PU_MST.DELV_BRANCH = CommonFuction.funFixQuotes(txtDelAddress.Text.Trim());

    //        if (txtTransporterCode.SelectedIndex < 0)
    //            oTX_ITEM_PU_MST.TRSP_CODE = "NA";
    //        else
    //            oTX_ITEM_PU_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

    //        oTX_ITEM_PU_MST.DESP_MODE = CommonFuction.funFixQuotes(txtDespatchMode.Text.Trim());
    //        if (radInsurance.SelectedItem.Value == "Yes")
    //            oTX_ITEM_PU_MST.INSU_FLAG = true;
    //        else
    //            oTX_ITEM_PU_MST.INSU_FLAG = false;
    //        oTX_ITEM_PU_MST.ADV_PRCNT = double.Parse(txtAdvance.Text.Trim());
    //        oTX_ITEM_PU_MST.FINAL_AMT = double.Parse(txtFinalTotal.Text.Trim());
    //        oTX_ITEM_PU_MST.TUSER = oUserLoginDetail.UserCode;
    //        oTX_ITEM_PU_MST.CH_CASH = false;
    //        DataTable dtItemIndent = (DataTable)Session["dtItemIndent"];
    //        DataTable dtRateCompo = (DataTable)Session["dtDicRate"];
    //        bool bResult = SaitexBL.Interface.Method.Material_Purchase_Order.Insert(oTX_ITEM_PU_MST, dtMaterialPOCredit, dtItemIndent, dtRateCompo);

    //        if (bResult)
    //        {
    //            InitialisePage();
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Purchase Order saved successfully');", true);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //private void UpdateMaterialPOCreditMST()
    //{
    //    try
    //    {
    //        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
    //        SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();

    //        oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //        oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
    //        oTX_ITEM_PU_MST.PO_TYPE = "PUM";
    //        oTX_ITEM_PU_MST.PO_NUMB = int.Parse(txtOrderNumber.Text.Trim());
    //        oTX_ITEM_PU_MST.PO_DATE = DateTime.Parse(txtOrderDate.Text.Trim());
    //        oTX_ITEM_PU_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
    //        oTX_ITEM_PU_MST.DEL_DATE = DateTime.Parse(txtDeliveryDate.Text.Trim());
    //        oTX_ITEM_PU_MST.PAY_TERM = CommonFuction.funFixQuotes(txtPayTerm.Text.Trim());
    //        if (radConfirm.SelectedItem.Value == "Yes")
    //            oTX_ITEM_PU_MST.CONF_FLAG = true;
    //        else
    //            oTX_ITEM_PU_MST.CONF_FLAG = false;
    //        oTX_ITEM_PU_MST.COMMENTS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
    //        oTX_ITEM_PU_MST.DELV_BRANCH = CommonFuction.funFixQuotes(txtDelAddress.Text.Trim());

    //        if (txtTransporterCode.SelectedIndex < 0)
    //            oTX_ITEM_PU_MST.TRSP_CODE = "NA";
    //        else
    //            oTX_ITEM_PU_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

    //        oTX_ITEM_PU_MST.DESP_MODE = CommonFuction.funFixQuotes(txtDespatchMode.Text.Trim());
    //        if (radInsurance.SelectedItem.Value == "Yes")
    //            oTX_ITEM_PU_MST.INSU_FLAG = true;
    //        else
    //            oTX_ITEM_PU_MST.INSU_FLAG = false;
    //        oTX_ITEM_PU_MST.ADV_PRCNT = double.Parse(txtAdvance.Text.Trim());
    //        oTX_ITEM_PU_MST.FINAL_AMT = double.Parse(txtFinalTotal.Text.Trim());
    //        oTX_ITEM_PU_MST.TUSER = oUserLoginDetail.UserCode;
    //        oTX_ITEM_PU_MST.CH_CASH = false;
    //        DataTable dtItemIndent = (DataTable)Session["dtItemIndent"];
    //        DataTable dtRateCompo = (DataTable)Session["dtDicRate"];
    //        bool bResult = SaitexBL.Interface.Method.Material_Purchase_Order.Update(oTX_ITEM_PU_MST, dtMaterialPOCredit, dtItemIndent, dtRateCompo);

    //        if (bResult)
    //        {
    //            InitialisePage();
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Purchase Order updated successfully');", true);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        if (UpdateMode)
    //        {
    //            if (txtOrderNumber.Text != null)
    //            {
    //                //    DeletePOMasterData();
    //            }

    //        }
    //        else
    //        {
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}
    //protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("~/Module/Inventory/Reports/POPermRPT.aspx?PO_TYPE=" + ddlOrderType.SelectedValue.Trim(), false);
    //}
    //protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    //{
    //    InitialisePage();
    //}
    //protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        if (Session["RedirectURL"] != null)
    //        {
    //            Response.Redirect(Session["RedirectURL"].ToString(), false);
    //            Session["RedirectURL"] = null;
    //        }
    //        else
    //        {
    //            Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}
    //protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    //{

    //}

    //protected void txtOrderNumber_TextChanged1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (dtMaterialPOCredit == null || dtMaterialPOCredit.Rows.Count == 0)
    //            CreateMaterialPODetailTable();
    //        dtMaterialPOCredit.Rows.Clear();
    //        FinalTotal = 0;

    //        int iRecordFound = GetdataByOrderNumber(CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim()));
    //        BindMaterialPOCredittoGrid();
    //        if (iRecordFound > 0)
    //        {
    //            ddlOrderType.Enabled = false;
    //            UpdateMode = true;
    //        }
    //        else
    //        {
    //            InitialisePage();
    //            txtOrderNumber.AutoPostBack = true;
    //            txtOrderNumber.ReadOnly = false;
    //            lblMode.Text = "Update";
    //            txtOrderNumber.Text = "";

    //            ddlOrderType.Enabled = true;
    //            UpdateMode = false;

    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Ivalid PO Number');", true);
    //        }
    //    }
    //    catch (Exception Ex)
    //    {

    //        errorLog.ErrHandler.WriteError(Ex.Message);
    //    }
    //}
    //private int GetdataByOrderNumber(string poNumber)
    //{
    //    int iRecordFound = 0;
    //    try
    //    {
    //        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

    //        SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();
    //        oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //        oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
    //        oTX_ITEM_PU_MST.PO_TYPE = "PUM";
    //        oTX_ITEM_PU_MST.PO_NUMB = int.Parse(poNumber);
    //        DataTable dt = SaitexBL.Interface.Method.Material_Purchase_Order.SelectByPONumber(oTX_ITEM_PU_MST);

    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            iRecordFound = 1;
    //            txtOrderDate.Text = dt.Rows[0]["PO_DATE"].ToString().Trim();
    //            txtDelAddress.Text = dt.Rows[0]["DELV_BRANCH"].ToString().Trim();
    //            txtDeliveryDate.Text = dt.Rows[0]["DEL_DATE"].ToString().Trim();
    //            txtAdvance.Text = dt.Rows[0]["ADV_PRCNT"].ToString().Trim();
    //            txtDespatchMode.Text = dt.Rows[0]["DESP_MODE"].ToString().Trim();
    //            txtPayTerm.Text = dt.Rows[0]["PAY_TERM"].ToString().Trim();
    //            txtRemarks.Text = dt.Rows[0]["COMMENTS"].ToString().Trim();
    //            ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue(dt.Rows[0]["PO_TYPE"].ToString().Trim()));
    //            if (dt.Rows[0]["CONF_FLAG"].ToString().Trim() == "1")
    //                radConfirm.SelectedValue = "Yes";
    //            else
    //                radConfirm.SelectedValue = "No";
    //            if (dt.Rows[0]["INSU_FLAG"].ToString().Trim() == "1")
    //                radInsurance.SelectedValue = "Yes";
    //            else
    //                radInsurance.SelectedValue = "No";


    //            string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0) asd";
    //            string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
    //            string SortExpression = " order by PRTY_CODE asc";
    //            string SearchQuery = "%";
    //            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //            txtTransporterCode.DataSource = data;
    //            txtTransporterCode.DataTextField = "PRTY_CODE";
    //            txtTransporterCode.DataValueField = "Address";
    //            txtTransporterCode.DataBind();
    //            txtPartyCode.DataSource = data;
    //            txtPartyCode.DataTextField = "PRTY_CODE";
    //            txtPartyCode.DataValueField = "Address";
    //            txtPartyCode.DataBind();
    //            foreach (ComboBoxItem item in txtPartyCode.Items)
    //            {
    //                if (item.Text == dt.Rows[0]["PRTY_CODE"].ToString().Trim())
    //                {
    //                    txtPartyCode.SelectedIndex = txtPartyCode.Items.IndexOf(item);
    //                    break;
    //                }
    //            }
    //            foreach (ComboBoxItem item in txtTransporterCode.Items)
    //            {
    //                if (item.Text == dt.Rows[0]["TRSP_CODE"].ToString().Trim())
    //                {
    //                    txtTransporterCode.SelectedIndex = txtTransporterCode.Items.IndexOf(item);
    //                    break;
    //                }
    //            }


    //            txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
    //            txtTransporterName.Text = dt.Rows[0]["TAddress"].ToString().Trim();
    //        }

    //        if (iRecordFound == 1)
    //        {
    //            DataTable dtTemp = GetTRNdataByOrderNumber(poNumber);
    //            MapDataTable(dtTemp);
    //            DataTable dtIndentAdjustment = SaitexBL.Interface.Method.Material_Purchase_Order.GetAdjustIndentByPO("PUM", int.Parse(poNumber), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);

    //            Session["dtItemIndent"] = dtIndentAdjustment;
    //        }
    //        return iRecordFound;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //private DataTable GetTRNdataByOrderNumber(string PONum)
    //{
    //    try
    //    {
    //        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

    //        SaitexDM.Common.DataModel.TX_ITEM_PU_MST oTX_ITEM_PU_MST = new SaitexDM.Common.DataModel.TX_ITEM_PU_MST();
    //        oTX_ITEM_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //        oTX_ITEM_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
    //        oTX_ITEM_PU_MST.PO_TYPE = "PUM";
    //        oTX_ITEM_PU_MST.PO_NUMB = int.Parse(PONum);
    //        DataTable dtTemp = SaitexBL.Interface.Method.Material_Purchase_Order.Select_TransactionByPONumber(oTX_ITEM_PU_MST);
    //        return dtTemp;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //private void MapDataTable(DataTable dtTemp)
    //{
    //    try
    //    {
    //        if (dtMaterialPOCredit == null || dtMaterialPOCredit.Rows.Count == 0)
    //            CreateMaterialPODetailTable();
    //        dtMaterialPOCredit.Rows.Clear();
    //        FinalTotal = 0;

    //        if (dtTemp != null && dtTemp.Rows.Count > 0)
    //        {
    //            foreach (DataRow drTemp in dtTemp.Rows)
    //            {
    //                DataRow dr = dtMaterialPOCredit.NewRow();
    //                double Amount = 0f;
    //                Amount = (double.Parse(drTemp["FINAL_RATE"].ToString().Trim())) * (int.Parse(drTemp["ORD_QTY"].ToString().Trim()));
    //                FinalTotal = FinalTotal + Amount;
    //                dr["UniqueId"] = dtMaterialPOCredit.Rows.Count + 1;
    //                dr["PO_NUMB"] = drTemp["PO_NUMB"].ToString().Trim();
    //                dr["ITEM_CODE"] = drTemp["ITEM_CODE"].ToString().Trim();
    //                dr["ITEM_DESC"] = drTemp["ITEM_DESC"].ToString().Trim();
    //                dr["ORD_QTY"] = drTemp["ORD_QTY"].ToString().Trim();
    //                dr["UOM"] = drTemp["UOM"].ToString().Trim();
    //                dr["BASIC_RATE"] = drTemp["BASIC_RATE"].ToString().Trim();
    //                dr["FINAL_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
    //                dr["Amount"] = Amount;
    //                dr["CURRENCY"] = drTemp["Curr_CODE"].ToString().Trim();
    //                dr["DEL_DATE"] = drTemp["DEL_DATE"].ToString().Trim();
    //                dtMaterialPOCredit.Rows.Add(dr);
    //            }
    //            dtTemp = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}

    //private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    //{
    //    bool Result = false;
    //    try
    //    {
    //        foreach (GridViewRow grdRow in gvMaterialPOTRN.Rows)
    //        {
    //            TextBox txtItemCode1 = (TextBox)grdRow.FindControl("txtItemCode");
    //            LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
    //            int iUniqueId = int.Parse(lnkEdit.CommandArgument.Trim());
    //            if (txtItemCode1.Text.Trim() == ItemCode && UniqueId != iUniqueId)
    //                Result = true;
    //        }
    //        return Result;
    //    }
    //    catch (Exception ex)
    //    {

    //        errorLog.ErrHandler.WriteError(ex.Message);
    //        return Result;
    //    }
    //}

    //private double GetAdvanceAmount(double AdvancePercent)
    //{
    //    try
    //    {
    //        double totaladvance = (FinalTotal * AdvancePercent) / 100;
    //        return totaladvance;
    //    }
    //    catch (Exception ex)
    //    {

    //        errorLog.ErrHandler.WriteError(ex.Message);
    //        throw ex;
    //    }
    //}
    //private void SetFinalTotal()
    //{
    //    if (txtAdvance.Text != "")
    //    {
    //        double advance = 0f;
    //        double.TryParse(txtAdvance.Text.Trim(), out advance);
    //        txtAdvanceAmount.Text = GetAdvanceAmount(advance).ToString();
    //    }
    //}
    //protected void txtAdvance_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        CalculateFinalTotal();
    //        if (txtAdvance.Text != "")
    //        {
    //            double advance = 0f;
    //            double.TryParse(txtAdvance.Text.Trim(), out advance);
    //            txtAdvanceAmount.Text = GetAdvanceAmount(advance).ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}

    //protected void CalculateFinalTotal()
    //{
    //    try
    //    {
    //        FinalTotal = 0;
    //        if (gvMaterialPOTRN.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow grdRow in gvMaterialPOTRN.Rows)
    //            {
    //                TextBox txtItemCode = (TextBox)grdRow.FindControl("txtItemCode");
    //                TextBox txtAmount = (TextBox)grdRow.FindControl("txtAmount");

    //                if (txtItemCode.Text.Trim() != "")
    //                {
    //                    FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
    //                }
    //            }
    //        }
    //        txtFinalTotal.Text = FinalTotal.ToString();
    //    }
    //    catch (Exception ex)
    //    {

    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}

    //protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);
    //    imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
    //    imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
    //    imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
    //    imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    //}

    //protected void txtCurrency_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    DataTable data = GetCurrencyData(e.Text.Trim());

    //    for (int i = 0; i < data.Rows.Count; i++)
    //    {
    //        (sender as ComboBox).Items.Add(new ComboBoxItem(data.Rows[i]["CURRENCY"].ToString(), data.Rows[i]["CURRENCY"].ToString()));
    //    }

    //    e.ItemsLoadedCount = data.Rows.Count;
    //    e.ItemsCount = data.Rows.Count;
    //}

    //private DataTable GetCurrencyData(string Text)
    //{
    //    try
    //    {
    //        string CommandText = "select MST_CODE CURRENCY from (select MST_CODE,MST_NAME from tx_Master_TRN where Del_Status=0 and MST_NAME='CURRENCY') asd ";
    //        string WhereClause = " where MST_CODE like :SearchQuery";
    //        string SortExpression = " order by MST_CODE asc";
    //        string SearchQuery = Text + "%";
    //        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    //{
    //    txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
    //}
    //protected void txtPartyCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    //{
    //    string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0) asd";
    //    string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
    //    string SortExpression = " order by PRTY_CODE asc";
    //    string SearchQuery = e.Text + "%";
    //    DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //    txtPartyCode.Items.Clear();

    //    txtPartyCode.DataSource = data;
    //    txtPartyCode.DataBind();

    //    e.ItemsLoadedCount = data.Rows.Count;
    //    e.ItemsCount = data.Rows.Count;
    //}
    //protected void txtTransporterCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    //{
    //    string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0) asd";
    //    string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
    //    string SortExpression = " order by PRTY_CODE asc";
    //    string SearchQuery = e.Text + "%";
    //    DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //    txtTransporterCode.Items.Clear();

    //    txtTransporterCode.DataSource = data;
    //    txtTransporterCode.DataBind();

    //    e.ItemsLoadedCount = data.Rows.Count;
    //    e.ItemsCount = data.Rows.Count;
    //}
    //protected void txtTransporterCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    //{
    //    txtTransporterName.Text = txtTransporterCode.SelectedValue;
    //}

    //protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    //{
    //    txtOrderNumber.AutoPostBack = true;
    //    txtOrderNumber.ReadOnly = false;
    //    lblMode.Text = "Update";
    //    txtOrderNumber.Text = "";
    //}
    //protected void txtItemCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        BindItemCodeCombo(e.Text);
    //        e.ItemsLoadedCount = txtItemCode.Items.Count;
    //        e.ItemsCount = txtItemCode.Items.Count;
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
    //    }
    //}
    //private void BindItemCodeCombo(string text)
    //{
    //    try
    //    {
    //        string CommandText = "select distinct * from (SELECT i.*,pt.IND_TYPE FROM TX_ITEM_IND_TRN pt,TX_ITEM_MST i WHERE i.ITEM_CODE=pt.ITEM_CODE and nvl(pt.APPR_QTY,0)>0 and nvl(pt.APPR_QTY,0)-nvl(PT.PUR_ADJ_QTY,0)<>0 ) asd";
    //        string WhereClause = "  where ITEM_CODE like :SearchQuery or ITEM_DESC like :SearchQuery or ITEM_TYPE like :SearchQuery";
    //        string SortExpression = " order by ITEM_CODE asc";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //        txtItemCode.Items.Clear();

    //        txtItemCode.DataSource = data;
    //        txtItemCode.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
    //    }
    //}
    //protected void txtItemCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    //{
    //    try
    //    {
    //        string Description = "";
    //        string UOM = "";
    //        double OpeningRate = 0;
    //        ComboBox thisTextBox = (ComboBox)txtItemCode;

    //        int UniqueId = 0;
    //        if (ViewState["UniqueId"] != null)
    //            UniqueId = int.Parse(ViewState["UniqueId"].ToString());
    //        if (!SearchItemCodeInGrid(thisTextBox.SelectedValue.Trim(), UniqueId))
    //        {
    //            int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(thisTextBox.SelectedValue.Trim()), out Description, out UOM, out OpeningRate);

    //            if (iRecordFound > 0)
    //            {
    //                txtBaseRate.Text = OpeningRate.ToString();
    //                txtItemDescription.Text = Description;
    //                txtFinalRate.Text = OpeningRate.ToString();
    //                txtUnit.Text = UOM;
    //                btnAdjustIndent.Focus();
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jsfun", "SetFocus('" + btnAdjustIndent.ClientID + "');", true);
    //            }
    //            else
    //            {
    //                txtBaseRate.Text = "0";
    //                txtItemDescription.Text = "";
    //                txtUnit.Text = "";
    //                thisTextBox.SelectedIndex = -1;
    //                thisTextBox.Focus();
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jsfun", "SetFocus('" + thisTextBox.ClientID + "');", true);
    //            }
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('This Item already included');", true);
    //            thisTextBox.SelectedIndex = -1;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}
    //protected void btnAdjustIndent_Click1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string URL = "POIndentAdjustment.aspx";
    //        URL = URL + "?ItemCodeId=" + txtItemCode.SelectedValue.Trim();
    //        URL = URL + "&TextBoxId=" + txtOrderQty.ClientID;
    //        if (UpdateMode)
    //        {
    //            URL = URL + "&PONum=" + txtOrderNumber.Text.Trim();
    //            string PO_TYPE = "PUM";
    //            URL = URL + "&PO_TYPE=" + PO_TYPE;
    //        }
    //        txtOrderQty.ReadOnly = false;
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=600');", true);
    //    }
    //    catch
    //    {
    //    }
    //}
    //protected void btnDiscountTaxes_Click1(object sender, EventArgs e)
    //{
    //    string URL = "GetPODisTex.aspx";
    //    URL = URL + "?FinalAmount=" + txtBaseRate.Text.Trim();
    //    URL = URL + "&TextBoxId=" + txtFinalRate.ClientID;
    //    URL = URL + "&ITEM_CODE=" + txtItemCode.SelectedValue.Trim();
    //    if (UpdateMode)
    //    {
    //        URL = URL + "&PONum=" + txtOrderNumber.Text.Trim();
    //        string PO_TYPE = "PUM";
    //        URL = URL + "&PO_TYPE=" + PO_TYPE;
    //    }
    //    txtFinalRate.ReadOnly = false;
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=600');", true);

    //}
    //protected void btnSaveDetail_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (dtMaterialPOCredit.Rows.Count < 15)
    //        {
    //            txtAmount.Text = CalculateAmount().ToString();

    //            if (txtItemCode.SelectedText != "" && txtOrderQty.Text != "" && txtFinalRate.Text != "" && txtAmount.Text != "" && txtCurrency.SelectedIndex >= 0)
    //            {
    //                int UniqueId = 0;
    //                if (ViewState["UniqueId"] != null)
    //                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
    //                bool bb = SearchItemCodeInGrid(txtItemCode.SelectedValue.Trim(), UniqueId);
    //                if (!bb)
    //                {
    //                    int Qty = 0;
    //                    int.TryParse(txtOrderQty.Text.Trim(), out Qty);
    //                    if (Qty > 0)
    //                    {
    //                        if (UniqueId > 0)
    //                        {
    //                            DataView dv = new DataView(dtMaterialPOCredit);
    //                            dv.RowFilter = "UniqueId=" + UniqueId;
    //                            if (dv.Count > 0)
    //                            {
    //                                dv[0]["PO_NUMB"] = txtOrderNumber.Text;
    //                                dv[0]["ITEM_CODE"] = txtItemCode.SelectedValue.Trim();
    //                                dv[0]["ITEM_DESC"] = txtItemDescription.Text.Trim();
    //                                dv[0]["ORD_QTY"] = int.Parse(txtOrderQty.Text.Trim());
    //                                dv[0]["UOM"] = txtUnit.Text.Trim();
    //                                dv[0]["BASIC_RATE"] = double.Parse(txtBaseRate.Text.Trim());
    //                                dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
    //                                dv[0]["Amount"] = double.Parse(txtAmount.Text.Trim());
    //                                dv[0]["CURRENCY"] = txtCurrency.SelectedText.Trim();
    //                                DateTime dd = System.DateTime.Now;
    //                                DateTime.TryParse(txtTrnDeliveryDate.Text.Trim(), out dd);
    //                                dv[0]["DEL_DATE"] = dd;
    //                                FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
    //                                dtMaterialPOCredit.AcceptChanges();
    //                            }
    //                        }
    //                        else
    //                        {
    //                            DataRow dr = dtMaterialPOCredit.NewRow();
    //                            dr["UniqueId"] = dtMaterialPOCredit.Rows.Count + 1;
    //                            dr["PO_NUMB"] = txtOrderNumber.Text;
    //                            dr["ITEM_CODE"] = txtItemCode.SelectedValue.Trim();
    //                            dr["ITEM_DESC"] = txtItemDescription.Text.Trim();
    //                            dr["ORD_QTY"] = int.Parse(txtOrderQty.Text.Trim());
    //                            dr["UOM"] = txtUnit.Text.Trim();
    //                            dr["BASIC_RATE"] = double.Parse(txtBaseRate.Text.Trim());
    //                            dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
    //                            dr["Amount"] = double.Parse(txtAmount.Text.Trim());
    //                            dr["CURRENCY"] = txtCurrency.SelectedText.Trim();
    //                            DateTime dd = System.DateTime.Now;
    //                            DateTime.TryParse(txtTrnDeliveryDate.Text.Trim(), out dd);
    //                            dr["DEL_DATE"] = dd;
    //                            FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
    //                            dtMaterialPOCredit.Rows.Add(dr);
    //                        }
    //                        RefreshDetailRow();
    //                    }
    //                    else
    //                    {
    //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
    //                    }
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
    //                }
    //            }
    //            else if (txtCurrency.SelectedIndex < 0)
    //            {
    //                CommonFuction.ShowMessage("Please select Currency");
    //            }

    //            gvMaterialPOTRN.DataSource = dtMaterialPOCredit;
    //            gvMaterialPOTRN.DataBind();
    //        }
    //        else
    //        {
    //            CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void btnCancelDetail_Click(object sender, EventArgs e)
    //{
    //    RefreshDetailRow();
    //}
    //private void RefreshDetailRow()
    //{
    //    txtItemCode.SelectedIndex = -1;
    //    txtItemDescription.Text = "";
    //    txtOrderQty.Text = "";
    //    txtUnit.Text = "";
    //    txtBaseRate.Text = "";
    //    txtFinalRate.Text = "";
    //    txtAmount.Text = "";
    //    txtCurrency.SelectedIndex = -1;
    //    txtTrnDeliveryDate.Text = System.DateTime.Now.Date.ToShortDateString();

    //    ViewState["UniqueId"] = null;
    //}
    //private void FillDetailByGrid(int UniqueId)
    //{
    //    try
    //    {
    //        DataView dv = new DataView(dtMaterialPOCredit);
    //        dv.RowFilter = "UniqueId=" + UniqueId;
    //        if (dv.Count > 0)
    //        {
    //            txtCurrency.SelectedIndex = -1;
    //            txtItemCode.SelectedValue = dv[0]["ITEM_CODE"].ToString();
    //            txtItemDescription.Text = dv[0]["ITEM_DESC"].ToString();
    //            txtOrderQty.Text = dv[0]["ORD_QTY"].ToString();
    //            txtUnit.Text = dv[0]["UOM"].ToString();
    //            txtBaseRate.Text = dv[0]["BASIC_RATE"].ToString();
    //            txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
    //            txtAmount.Text = dv[0]["Amount"].ToString();
    //            txtCurrency.SelectedValue = dv[0]["CURRENCY"].ToString();
    //            txtTrnDeliveryDate.Text = dv[0]["DEL_DATE"].ToString();
    //            ViewState["UniqueId"] = UniqueId;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void txtBaseRate_TextChanged1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TextBox thisTextBox = (TextBox)txtBaseRate;
    //        if (thisTextBox.Text != "")
    //        {
    //            double RequestQTY = 0;
    //            if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
    //            {

    //                txtAmount.Text = CalculateAmount().ToString();
    //            }
    //            else
    //            {
    //                thisTextBox.Text = "0";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}

    //protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getPOMaxId();
    //}
}
