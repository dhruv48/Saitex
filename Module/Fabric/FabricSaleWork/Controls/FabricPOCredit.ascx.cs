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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;

public partial class Module_Inventory_Controls_FabricPOCredit : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.TX_FABRIC_PU_MST oTX_FABRIC_PU_MST = new SaitexDM.Common.DataModel.TX_FABRIC_PU_MST();
    private DataTable dtPODetail = null;
    private static bool UpdateMode = false;
    private string UserCode;
    private static double FinalTotal;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DateTime startdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Enddate = CommonFuction.GetYearEndDate(startdate);
            UserCode = oUserLoginDetail.UserCode;
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

            btnAdjustIndent.Enabled = true;

            ActivateSaveMode();
            ddlIndent.Visible = false;
            chkFetchIndent.Checked = false;
            txtOrderNumber.AutoPostBack = false;
            txtOrderNumber.ReadOnly = true;
            lblMode.Text = "Save";
            ddlOrderType.Enabled = true;
            txtOrderNumber.Enabled = true;
            UpdateMode = false;
            BindDelBranch();
            BindCurrency();
            BindPONature();
            txtOrderNumber.Text = "";
            getPOMaxId();
            txtOrderDate.Text = System.DateTime.Now.Date.ToShortDateString();
            txtPartyAddress.Text = "";
            txtPartyCode.SelectedIndex = -1;
            txtTransporterCode.SelectedIndex = -1;
            txtTransporterName.Text = "";
            txtPayTerm.Text = "";
            txtDeliveryDate.Text = System.DateTime.Now.Date.ToShortDateString();
            txtDelAddress.Text = "";
            txtDespatchMode.Text = "";
            txtRemarks.Text = "";
            txtInstructions.Text = "";
            txtCurrencyCode.SelectedIndex = 0;
            BindDeliveryAddByCode(ddlDelAdd.SelectedValue.Trim());
            txtconversionRate.Text = "1.00";

            if (dtPODetail == null)
                CreateMaterialPODetailTable();
            dtPODetail.Rows.Clear();

            BindMaterialPOCredittoGrid();



            RefreshDetailRow();
            Session["dtItemIndent"] = null;
            Session["dtDicRate"] = null;
            chkFetchIndent.Checked = false;
            txtCurrencyCode.SelectedIndex = txtCurrencyCode.Items.IndexOf(txtCurrencyCode.Items.FindByValue("Rs."));

        }
        catch
        {
            throw;
        }
    }

    private void getPOMaxId()
    {
        try
        {
            int _orderNo = 0;
            var oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            oTX_FABRIC_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_PU_MST.PO_TYPE = ddlOrderType.SelectedValue.Trim();
            int.TryParse(SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetNewPONo(oTX_FABRIC_PU_MST.PO_TYPE, oTX_FABRIC_PU_MST.COMP_CODE, oTX_FABRIC_PU_MST.BRANCH_CODE), out _orderNo);
            txtOrderNumber.Text = (_orderNo + 1).ToString();
        }
        catch
        {
            throw;
        }
    }

    private void BindCurrency()
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("CURRENCY", oUserLoginDetail.COMP_CODE);
            txtCurrencyCode.DataSource = dt;
            txtCurrencyCode.DataTextField = "MST_CODE";
            txtCurrencyCode.DataValueField = "MST_CODE";
            txtCurrencyCode.DataBind();
            // txtCurrencyCode.SelectedIndex = txtCurrencyCode.Items.IndexOf(txtCurrencyCode.Items.FindByText("Rs."));
            //ddlColor.SelectedIndex = ddlColor.Items.IndexOf(ddlColor.Items.FindByText(dv[0]["COLOUR"].ToString()));
        }
        catch
        {
            throw;
        }
    }

    private void BindPONature()
    {
        try
        {
            ddlPONature.Items.Clear();
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PO_NATURE", oUserLoginDetail.COMP_CODE);
            ddlPONature.DataSource = dt;
            ddlPONature.DataTextField = "MST_DESC";
            ddlPONature.DataValueField = "MST_CODE";
            ddlPONature.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void CreateMaterialPODetailTable()
    {
        try
        {
            dtPODetail = new DataTable();
            dtPODetail.Columns.Add("UniqueId", typeof(int));
            dtPODetail.Columns.Add("PO_NUMB", typeof(string));
            dtPODetail.Columns.Add("FABR_CODE", typeof(string));
            dtPODetail.Columns.Add("FABR_DESC", typeof(string));
            dtPODetail.Columns.Add("ORD_QTY", typeof(double));
            dtPODetail.Columns.Add("UOM", typeof(string));
            dtPODetail.Columns.Add("BASIC_RATE", typeof(double));
            dtPODetail.Columns.Add("FINAL_RATE", typeof(double));
            dtPODetail.Columns.Add("Amount", typeof(double));
            dtPODetail.Columns.Add("QUOTATION_NO", typeof(string));
            dtPODetail.Columns.Add("SHADE_CODE", typeof(string));
            dtPODetail.Columns.Add("DEL_DATE", typeof(DateTime));
            ViewState["dtPODetail"] = dtPODetail;

        }
        catch
        {
            throw;
        }
    }

    private void BindMaterialPOCredittoGrid()
    {
        try
        {
            if (ViewState["dtPODetail"] != null)
            {
                dtPODetail = (DataTable)ViewState["dtPODetail"];
            }
            gvMaterialPOTRN.DataSource = dtPODetail;
            gvMaterialPOTRN.DataBind();

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
            lblItemCode.Text = "";
            txtItemDescription.Text = "";
            txtOrderQty.Text = "";
            txtUnit.Text = "";
            txtBaseRate.Text = "";
            txtFinalRate.Text = "";
            txtAmount.Text = "";
            txtQuotation.Text = "";
            txtTrnDeliveryDate.Text = System.DateTime.Now.Date.ToShortDateString();
            txtItemCode.Enabled = true;
            txtShadeCode.Text = string.Empty;
            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getPOMaxId();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in order type selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtPartyCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode.Items.Clear();
            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();
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
            var CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }
            var SortExpression = " order by PRTY_CODE";
            var SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
                      
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        var CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
        var WhereClause = " ";
        var SortExpression = " ";
        var SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
                
    }

    protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtTransporterCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var data = GetTransporterData(e.Text.ToUpper(), e.ItemsOffset);
            txtTransporterCode.Items.Clear();
            txtTransporterCode.DataSource = data;
            txtTransporterCode.DataBind();
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
            var CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= 15 ";
            var whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }

            var SortExpression = " order by PRTY_CODE";
            var SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
                       
        }
        catch
        {
            throw;
        }
    }

    protected int GetTransporterCount(string text)
    {
        var CommandText = " SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') ";
        var WhereClause = " ";
        var SortExpression = " ";
        var SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void txtTransporterCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtTransporterName.Text = txtTransporterCode.SelectedValue;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            var Shade = txtShadeCode.Text.Trim();
            foreach (GridViewRow grdRow in gvMaterialPOTRN.Rows)
            {
                var txtItemCode1 = (Label)grdRow.FindControl("txtItemCode");
                var lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete");
                var txtSHADE = (Label)grdRow.FindControl("txtShadeCode");
                var iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtItemCode1.Text.Trim() == ItemCode && UniqueId != iUniqueId && Shade == txtSHADE.Text.Trim())
                {
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

    private int GetItemDetailByItemCode(string ItemCode, out string Description, out string UOM, out double OpeningRate)
    {
        int iRecordFound = 0;
        Description = "";
        UOM = "";
        OpeningRate = 0;
        //  SHADE_CODE = string.Empty;
        try
        {           
            var   dts = SaitexDL.Interface.Method.TX_FABRIC_MST.GetFabricDetailByFabricCode(ItemCode);
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["FABR_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UOM"].ToString().Trim();
                OpeningRate = double.Parse(dts.Rows[0]["OP_RATE"].ToString().Trim());
                iRecordFound = dts.Rows.Count;
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    protected void btnAdjustIndent_Click1(object sender, EventArgs e)
    {
        try
        {
            if (lblItemCode.Text != "")
            {
                var URL = "POIndentAdjustment.aspx";
                URL = URL + "?ItemCodeId=" + lblItemCode.Text.Trim();
                URL = URL + "&txtShadeCode=" + txtShadeCode.Text.Trim();
                URL = URL + "&TextBoxId=" + txtOrderQty.ClientID;
                URL = URL + "&PONum=" + txtOrderNumber.Text.Trim();
                var PO_TYPE = "PUM";
                URL = URL + "&PO_TYPE=" + PO_TYPE;

                txtOrderQty.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);


            }
            else
            {
                CommonFuction.ShowMessage("Please select Yarn to adjust Indent");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting adjustment.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtOrderQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var thisTextBox = (TextBox)txtOrderQty;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {
                    txtAmount.Text = CalculateAmount().ToString();
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
            thisTextBox.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in entering quantity .\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private double CalculateAmount()
    {
        try
        {
            double OrderQty = 0;
            double BaseRate = 0f;
            double FinalRate = 0f;
            double Amount = 0f;
            double.TryParse(CommonFuction.funFixQuotes(txtOrderQty.Text.Trim()), out OrderQty);
            double.TryParse(CommonFuction.funFixQuotes(txtBaseRate.Text.Trim()), out BaseRate);
            double.TryParse(CommonFuction.funFixQuotes(txtFinalRate.Text.Trim()), out FinalRate);
            if (FinalRate == 0f)
                FinalRate = BaseRate;

            Amount = OrderQty * FinalRate;
            return Amount;
        }
        catch
        {
            throw;
        }
    }

    protected void txtBaseRate_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            var thisTextBox = (TextBox)txtBaseRate;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {

                    txtAmount.Text = CalculateAmount().ToString();
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in entering base rate.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnDiscountTaxes_Click1(object sender, EventArgs e)
    {
        try
        {
            if (lblItemCode.Text != "")
            {
                var URL = "GetPODisTex.aspx";
                URL = URL + "?FinalAmount=" + txtBaseRate.Text.Trim();
                URL = URL + "&TextBoxId=" + txtFinalRate.ClientID;
                URL = URL + "&FABR_CODE=" + lblItemCode.Text.Trim();
                URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
                if (UpdateMode)
                {
                    URL = URL + "&PONum=" + txtOrderNumber.Text.Trim();
                    var PO_TYPE = "PUM";
                    URL = URL + "&PO_TYPE=" + PO_TYPE;
                }
                txtFinalRate.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

            }
            else
            {
                CommonFuction.ShowMessage("Please select Yarn to add rate component");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Rate Component.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtFinalRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var thisTextBox = (TextBox)txtFinalRate;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {

                    txtAmount.Text = CalculateAmount().ToString();
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
            thisTextBox.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting final rate.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtPODetail"] != null)
            {
                dtPODetail = (DataTable)ViewState["dtPODetail"];
            }

            double BaseRate = 0f;
            double FinalRate = 0f;
            double.TryParse(CommonFuction.funFixQuotes(txtBaseRate.Text.Trim()), out BaseRate);
            var dtRateCompo = (DataTable)Session["dtDicRate"];
            FinalRate = CommonFuction.GetFinalRateOfDisTaxFABRIC(dtRateCompo, lblItemCode.Text.Trim(), txtShadeCode.Text.Trim(), BaseRate);

            if (FinalRate != 0f && BaseRate != 0f)
            {
                if (dtPODetail == null)
                    CreateMaterialPODetailTable();

                txtAmount.Text = CalculateAmount().ToString();

                if (lblItemCode.Text != "" && txtOrderQty.Text != "" && txtFinalRate.Text != "" && txtAmount.Text != "")
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    bool bb = SearchItemCodeInGrid(lblItemCode.Text.Trim(), UniqueId);
                    if (!bb)
                    {
                        double Qty = 0;
                        double.TryParse(txtOrderQty.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {
                            if (UniqueId > 0)
                            {
                                var dv = new DataView(dtPODetail);
                                dv.RowFilter = "UniqueId=" + UniqueId;
                                if (dv.Count > 0)
                                {
                                    dv[0]["PO_NUMB"] = txtOrderNumber.Text;
                                    dv[0]["FABR_CODE"] = lblItemCode.Text.Trim();
                                    dv[0]["FABR_DESC"] = txtItemDescription.Text.Trim();
                                    dv[0]["ORD_QTY"] = double.Parse(txtOrderQty.Text.Trim());
                                    dv[0]["UOM"] = txtUnit.Text.Trim();
                                    dv[0]["BASIC_RATE"] = Math.Round(BaseRate,3);
                                    dv[0]["FINAL_RATE"] = Math.Round(FinalRate,3);
                                    dv[0]["Amount"] = Math.Round(double.Parse(txtAmount.Text.Trim()),3);
                                    dv[0]["QUOTATION_NO"] = txtQuotation.Text.Trim();
                                    DateTime dd = System.DateTime.Now;
                                    DateTime.TryParse(txtTrnDeliveryDate.Text.Trim(), out dd);
                                    dv[0]["DEL_DATE"] = dd;
                                    dv[0]["SHADE_CODE"] = txtShadeCode.Text.Trim();

                                    FinalTotal = Math.Round(FinalTotal + double.Parse(txtAmount.Text.Trim()),3);
                                    dtPODetail.AcceptChanges();
                                }
                            }
                            else
                            {
                                var dr = dtPODetail.NewRow();
                                dr["UniqueId"] = dtPODetail.Rows.Count + 1;
                                dr["PO_NUMB"] = txtOrderNumber.Text;
                                dr["FABR_CODE"] = lblItemCode.Text.Trim();
                                dr["FABR_DESC"] = txtItemDescription.Text.Trim();
                                dr["ORD_QTY"] = double.Parse(txtOrderQty.Text.Trim());
                                dr["UOM"] = txtUnit.Text.Trim();
                                dr["BASIC_RATE"] =Math.Round( BaseRate,3);
                                dr["FINAL_RATE"] = Math.Round(FinalRate,3);
                                dr["Amount"] = Math.Round(double.Parse(txtAmount.Text.Trim()),3);
                                dr["QUOTATION_NO"] = txtQuotation.Text.Trim();
                                DateTime dd = System.DateTime.Now;
                                DateTime.TryParse(txtTrnDeliveryDate.Text.Trim(), out dd);
                                dr["DEL_DATE"] = dd;
                                dr["SHADE_CODE"] = txtShadeCode.Text;
                                FinalTotal = Math.Round(FinalTotal + double.Parse(txtAmount.Text.Trim()),3);
                                dtPODetail.Rows.Add(dr);
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid Yarn code');", true);
                    }
                }

                ViewState["dtPODetail"] = dtPODetail;
                gvMaterialPOTRN.DataSource = dtPODetail;
                gvMaterialPOTRN.DataBind();
                CalculateFinalTotal();
            }
            else
            {
                CommonFuction.ShowMessage("You have entered base rate/ final rate Zero. Please enter proper base rate or Dis/ Taxes.");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn Detail Row.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnCancelDetail_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refreshing Yarn Detail ROw.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void gvMaterialPOTRN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "POMateialCreditDelete")
            {
                deletePOMaterialCreditRow(UniqueId);
                BindMaterialPOCredittoGrid();
            }
            if (e.CommandName == "POMateialCreditEdit")
            {
                FillDetailByGrid(UniqueId);
            }
            CalculateFinalTotal();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row updation / deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void deletePOMaterialCreditRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtPODetail"] != null)
            {
                dtPODetail = (DataTable)ViewState["dtPODetail"];
            }

            if (gvMaterialPOTRN.Rows.Count == 1)
            {
                dtPODetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtPODetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtPODetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtPODetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
                ViewState["dtPODetail"] = dtPODetail;
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtPODetail"] != null)
            {
                dtPODetail = (DataTable)ViewState["dtPODetail"];
            }
            var dv = new DataView(dtPODetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                lblItemCode.Text = dv[0]["FABR_CODE"].ToString();
                txtItemDescription.Text = dv[0]["FABR_DESC"].ToString();
                txtOrderQty.Text = dv[0]["ORD_QTY"].ToString();
                txtUnit.Text = dv[0]["UOM"].ToString();
                txtBaseRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["Amount"].ToString();
                txtQuotation.Text = dv[0]["QUOTATION_NO"].ToString();
                if (dv[0]["DEL_DATE"].ToString() != null && dv[0]["DEL_DATE"].ToString() != string.Empty)
                {
                    txtTrnDeliveryDate.Text = DateTime.Parse(dv[0]["DEL_DATE"].ToString()).ToShortDateString();
                }
                txtShadeCode.Text = dv[0]["SHADE_CODE"].ToString();
                ViewState["UniqueId"] = UniqueId;

                txtItemCode.Enabled = false;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void CalculateFinalTotal()
    {
        try
        {
            FinalTotal = 0;
            if (gvMaterialPOTRN.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in gvMaterialPOTRN.Rows)
                {
                    var txtItemCode = (Label)grdRow.FindControl("txtItemCode");
                    var txtAmount = (Label)grdRow.FindControl("txtAmount");

                    if (txtItemCode.Text.Trim() != "")
                    {
                        FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
                    }
                }
            }
            //      txtFinalTotal.Text = FinalTotal.ToString();
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
            if (ViewState["dtPODetail"] != null)
            {
                dtPODetail = (DataTable)ViewState["dtPODetail"];
            }

            if (txtOrderNumber.Text != "")
            {
                if (dtPODetail != null && dtPODetail.Rows.Count > 0 && txtPartyCode.SelectedIndex >= 0)
                {
                    UpdateMaterialPOCreditMST();

                }
                else if (dtPODetail == null || dtPODetail.Rows.Count == 0)
                {
                    CommonFuction.ShowMessage("Please select atleast one Fabric to generate purchase order");
                }
                else if (txtPartyCode.SelectedIndex < 0)
                {
                    CommonFuction.ShowMessage("Please select party to generate purchase order");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please enter purchase order");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po Processing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void saveMaterialPOOrderCreditMST()
    {
        try
        {
            var oTX_FABRIC_PU_MST = new SaitexDM.Common.DataModel.TX_FABRIC_PU_MST();
            oTX_FABRIC_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_PU_MST.PO_TYPE = ddlOrderType.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.PO_NUMB = int.Parse(txtOrderNumber.Text.Trim());
            oTX_FABRIC_PU_MST.PO_DATE = DateTime.Parse(txtOrderDate.Text.Trim());
            oTX_FABRIC_PU_MST.PO_NATURE = ddlPONature.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_FABRIC_PU_MST.DEL_DATE = DateTime.Parse(txtDeliveryDate.Text.Trim());
            oTX_FABRIC_PU_MST.PAY_TERM = CommonFuction.funFixQuotes(txtPayTerm.Text.Trim());
            oTX_FABRIC_PU_MST.CONF_FLAG = false;
            oTX_FABRIC_PU_MST.COMMENTS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_PU_MST.DELV_BRANCH = CommonFuction.funFixQuotes(txtDelAddress.Text.Trim());
            oTX_FABRIC_PU_MST.DELV_BRANCH_CODE = CommonFuction.funFixQuotes(ddlDelAdd.SelectedValue.Trim());
            if (txtTransporterCode.SelectedIndex < 0)
                oTX_FABRIC_PU_MST.TRSP_CODE = "NA";
            else
                oTX_FABRIC_PU_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());
            oTX_FABRIC_PU_MST.DESP_MODE = CommonFuction.funFixQuotes(txtDespatchMode.Text.Trim());
            oTX_FABRIC_PU_MST.INSU_FLAG = false;
            oTX_FABRIC_PU_MST.ADV_PRCNT = 0;
            CalculateFinalTotal();
            oTX_FABRIC_PU_MST.FINAL_AMT = FinalTotal;
            oTX_FABRIC_PU_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_FABRIC_PU_MST.CURRENCY_CODE = txtCurrencyCode.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.CONVERSION_RATE = txtconversionRate.Text.Trim();
            oTX_FABRIC_PU_MST.INSTRUCTIONS = txtInstructions.Text.Trim();
            var dtItemIndent = (DataTable)Session["dtItemIndent"];
            var dtRateCompo = (DataTable)Session["dtDicRate"];
            int PO_NUMB = 0;
            var bResult = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.Insert(oTX_FABRIC_PU_MST, dtPODetail, dtItemIndent, dtRateCompo, out PO_NUMB);
            if (bResult)
            {
                string msg = "PO Number " + PO_NUMB + " Successfully Saved.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
                InitialisePage();
                chkFetchIndent.Checked = false;

                ddlIndent.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }

    private void UpdateMaterialPOCreditMST()
    {
        try
        {
            var oTX_FABRIC_PU_MST = new SaitexDM.Common.DataModel.TX_FABRIC_PU_MST();
            oTX_FABRIC_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_PU_MST.PO_TYPE = ddlOrderType.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.PO_NUMB = int.Parse(txtOrderNumber.Text.Trim());
            oTX_FABRIC_PU_MST.PO_DATE = DateTime.Parse(txtOrderDate.Text.Trim());
            oTX_FABRIC_PU_MST.PO_NATURE = ddlPONature.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oTX_FABRIC_PU_MST.DEL_DATE = DateTime.Parse(txtDeliveryDate.Text.Trim());
            oTX_FABRIC_PU_MST.PAY_TERM = CommonFuction.funFixQuotes(txtPayTerm.Text.Trim());
            //if (radConfirm.SelectedItem.Value == "Yes")
            //    oTX_FABRIC_PU_MST.CONF_FLAG = true;
            //else
            oTX_FABRIC_PU_MST.CONF_FLAG = false;
            oTX_FABRIC_PU_MST.COMMENTS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTX_FABRIC_PU_MST.DELV_BRANCH = CommonFuction.funFixQuotes(txtDelAddress.Text.Trim());
            oTX_FABRIC_PU_MST.DELV_BRANCH_CODE = CommonFuction.funFixQuotes(ddlDelAdd.SelectedValue.Trim());
            if (txtTransporterCode.SelectedIndex < 0)
                oTX_FABRIC_PU_MST.TRSP_CODE = "NA";
            else
                oTX_FABRIC_PU_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

            oTX_FABRIC_PU_MST.DESP_MODE = CommonFuction.funFixQuotes(txtDespatchMode.Text.Trim());
            //if (radInsurance.SelectedItem.Value == "Yes")
            //    oTX_FABRIC_PU_MST.INSU_FLAG = true;
            //else
            oTX_FABRIC_PU_MST.INSU_FLAG = false;
            oTX_FABRIC_PU_MST.ADV_PRCNT = 0;// double.Parse(txtAdvance.Text.Trim());
            CalculateFinalTotal();
            oTX_FABRIC_PU_MST.FINAL_AMT = FinalTotal;
            oTX_FABRIC_PU_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_FABRIC_PU_MST.CURRENCY_CODE = txtCurrencyCode.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.CONVERSION_RATE = txtconversionRate.Text.Trim();
            oTX_FABRIC_PU_MST.INSTRUCTIONS = txtInstructions.Text.Trim();
            var dtItemIndent = (DataTable)Session["dtItemIndent"];
            var dtRateCompo = (DataTable)Session["dtDicRate"];
            var bResult = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.Update(oTX_FABRIC_PU_MST, dtPODetail, dtItemIndent, dtRateCompo);
            if (bResult)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Purchase Order updated successfully');", true);
                InitialisePage();
            }
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
            lblMode.Text = "Update";
            txtOrderNumber.Text = "";
            ActivateUpdateMode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in update mode activation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtOrderNumber_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            if (dtPODetail == null || dtPODetail.Rows.Count == 0)
                CreateMaterialPODetailTable();
            dtPODetail.Rows.Clear();
            FinalTotal = 0;

            int iRecordFound = GetdataByOrderNumber(CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim()));
            BindMaterialPOCredittoGrid();
            if (iRecordFound > 0)
            {
                ddlOrderType.Enabled = false;
                UpdateMode = true;
            }
            else
            {
                InitialisePage();
                txtOrderNumber.AutoPostBack = true;
                txtOrderNumber.ReadOnly = false;
                lblMode.Text = "Update";
                txtOrderNumber.Text = "";

                ddlOrderType.Enabled = true;
                UpdateMode = false;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid PO Number');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po order number selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private int GetdataByOrderNumber(string poNumber)
    {
        int iRecordFound = 0;
        try
        {
            var oTX_FABRIC_PU_MST = new SaitexDM.Common.DataModel.TX_FABRIC_PU_MST();
            oTX_FABRIC_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_PU_MST.PO_TYPE = ddlOrderType.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.PO_NUMB = int.Parse(poNumber);
            var dt = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.SelectByPONumber(oTX_FABRIC_PU_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtOrderDate.Text = DateTime.Parse(dt.Rows[0]["PO_DATE"].ToString().Trim()).ToShortDateString();
                txtDelAddress.Text = dt.Rows[0]["DELV_BRANCH"].ToString().Trim();
                ddlDelAdd.SelectedIndex = ddlDelAdd.Items.IndexOf(ddlDelAdd.Items.FindByValue(dt.Rows[0]["DELV_BRANCH_CODE"].ToString().Trim()));
                txtDeliveryDate.Text = DateTime.Parse(dt.Rows[0]["DEL_DATE"].ToString().Trim()).ToShortDateString();
                txtDespatchMode.Text = dt.Rows[0]["DESP_MODE"].ToString().Trim();
                txtPayTerm.Text = dt.Rows[0]["PAY_TERM"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["COMMENTS"].ToString().Trim();
                txtCurrencyCode.Text = dt.Rows[0]["CURRENCY_CODE"].ToString().Trim();
                txtconversionRate.Text = dt.Rows[0]["CONVERSION_RATE"].ToString().Trim();
                txtInstructions.Text = dt.Rows[0]["INSTRUCTIONS"].ToString().Trim();

                var CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0) asd";
                var WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
                var SortExpression = " order by PRTY_CODE asc";
                var SearchQuery = "%";
                var data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

                txtTransporterCode.DataSource = data;
                txtTransporterCode.DataTextField = "PRTY_CODE";
                txtTransporterCode.DataValueField = "Address";
                txtTransporterCode.DataBind();
                txtPartyCode.DataSource = data;
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
                foreach (ComboBoxItem item in txtTransporterCode.Items)
                {
                    if (item.Text == dt.Rows[0]["TRSP_CODE"].ToString().Trim())
                    {
                        txtTransporterCode.SelectedIndex = txtTransporterCode.Items.IndexOf(item);
                        break;
                    }
                }
                txtPartyAddress.Text = dt.Rows[0]["Address"].ToString().Trim();
                txtTransporterName.Text = dt.Rows[0]["TAddress"].ToString().Trim();
            }

            if (iRecordFound == 1)
            {
                var dtTemp = GetTRNdataByOrderNumber(poNumber);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                    var dtIndentAdjustment = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetAdjustIndentByPO("PUM", int.Parse(poNumber), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                    GetTaxAdjByPO(poNumber);
                    Session["dtItemIndent"] = dtIndentAdjustment;
                    txtOrderNumber.Text = poNumber;
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! Purchase Order already approved. Modification not allowed.";
                    Common.CommonFuction.ShowMessage(msg);
                    InitialisePage();
                    txtOrderNumber.Text = "";
                    ddlOrderNumber.Focus();
                    lblMode.Text = "Update";
                    ActivateUpdateMode();
                    RefreshDetailRow();
                }
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateTaxDataTable()
    {
        try
        {
            var dtRate = new DataTable();
            dtRate.Columns.Add("UniqueId", typeof(int));
            dtRate.Columns.Add("YEAR", typeof(int));
            dtRate.Columns.Add("FABR_CODE", typeof(string));
            dtRate.Columns.Add("SHADE_CODE", typeof(string));
            dtRate.Columns.Add("COMPO_CODE", typeof(string));
            dtRate.Columns.Add("Rate", typeof(float));
            dtRate.Columns.Add("COMPO_SL", typeof(int));
            dtRate.Columns.Add("COMPO_TYPE", typeof(string));
            dtRate.Columns.Add("Amount", typeof(double));
            dtRate.Columns.Add("BASE_COMPO_CODE", typeof(string));
            return dtRate;
        }
        catch { throw; }
    }

    private void GetTaxAdjByPO(string poNumber)
    {
        try
        {
            var oTX_FABRIC_PU_MST = new SaitexDM.Common.DataModel.TX_FABRIC_PU_MST();
            oTX_FABRIC_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_PU_MST.PO_TYPE = ddlOrderType.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.PO_NUMB = int.Parse(poNumber);
            var dt = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetTaxByPOOnly(oTX_FABRIC_PU_MST);
            if (dt.Rows.Count > 0)
            {
                var dtRate = CreateTaxDataTable();
                MapTaxTable(dt, dtRate);
                Session["dtDicRate"] = dtRate;
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapTaxTable(DataTable temp, DataTable dtRate)
    {
        try
        {
            foreach (DataRow dr in temp.Rows)
            {
                var drNew = dtRate.NewRow();
                drNew["UniqueId"] = dtRate.Rows.Count + 1;
                drNew["YEAR"] = dr["YEAR"];
                drNew["FABR_CODE"] = dr["FABR_CODE"];
                drNew["SHADE_CODE"] = dr["SHADE_CODE"];
                drNew["COMPO_CODE"] = dr["COMPO_CODE"];
                drNew["Rate"] = dr["RATE"];
                drNew["COMPO_SL"] = dr["COMPO_SL"];
                drNew["COMPO_TYPE"] = dr["COMPO_TYPE"];
                drNew["BASE_COMPO_CODE"] = dr["BASE_COMPO_CODE"];
                dtRate.Rows.Add(drNew);
            }
            temp = null;

        }
        catch
        {
            throw;
        }
    }

    private DataTable GetTRNdataByOrderNumber(string PONum)
    {
        try
        {
            var oTX_FABRIC_PU_MST = new SaitexDM.Common.DataModel.TX_FABRIC_PU_MST();
            oTX_FABRIC_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FABRIC_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FABRIC_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FABRIC_PU_MST.PO_TYPE = ddlOrderType.SelectedValue.Trim();
            oTX_FABRIC_PU_MST.PO_NUMB = int.Parse(PONum);
            return SaitexBL.Interface.Method.TX_FABRIC_PU_MST.Select_TransactionByPONumber(oTX_FABRIC_PU_MST);
           
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (dtPODetail == null || dtPODetail.Rows.Count == 0)
                CreateMaterialPODetailTable();
            dtPODetail.Rows.Clear();
            FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    var dr = dtPODetail.NewRow();
                    double Amount = 0f;
                    double _finalRate = 0;
                    double _ordQty = 0;
                    double.TryParse(drTemp["FINAL_RATE"].ToString().Trim(),out _finalRate);
                    double.TryParse(drTemp["ORD_QTY"].ToString().Trim(), out _ordQty);
                    Amount = _finalRate * _ordQty;
                    FinalTotal = FinalTotal + Amount;
                    dr["UniqueId"] = dtPODetail.Rows.Count + 1;
                    dr["PO_NUMB"] = drTemp["PO_NUMB"].ToString().Trim();
                    dr["FABR_CODE"] = drTemp["FABR_CODE"].ToString().Trim();
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"].ToString().Trim();
                    dr["FABR_DESC"] = drTemp["FABR_DESC"].ToString().Trim();
                    dr["ORD_QTY"] = drTemp["ORD_QTY"].ToString().Trim();
                    dr["UOM"] = drTemp["UOM"].ToString().Trim();
                    dr["BASIC_RATE"] = drTemp["BASIC_RATE"].ToString().Trim();
                    dr["FINAL_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
                    dr["Amount"] = Amount;
                    dr["QUOTATION_NO"] = drTemp["QUOTATION_NO"].ToString().Trim();
                    dr["DEL_DATE"] = drTemp["DEL_DATE"].ToString().Trim();
                    dtPODetail.Rows.Add(dr);
                }
                dtTemp = null;
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapIndentDataTable(DataTable dtTemp)
    {
        try
        {
            if (dtPODetail == null || dtPODetail.Rows.Count == 0)
                CreateMaterialPODetailTable();
            dtPODetail.Rows.Clear();
            FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtPODetail.NewRow();
                    double Amount = 0f;
                    Amount = (double.Parse(drTemp["FINAL_RATE"].ToString().Trim())) * (int.Parse(drTemp["ORD_QTY"].ToString().Trim()));
                    FinalTotal = FinalTotal + Amount;
                    dr["UniqueId"] = dtPODetail.Rows.Count + 1;
                    //dr["PO_NUMB"] = drTemp["PO_NUMB"].ToString().Trim();
                    dr["FABR_CODE"] = drTemp["FABR_CODE"].ToString().Trim();
                    dr["FABR_DESC"] = drTemp["FABR_DESC"].ToString().Trim();
                    dr["ORD_QTY"] = drTemp["ORD_QTY"].ToString().Trim();
                    dr["UOM"] = drTemp["UNIT_NAME"].ToString().Trim();
                    dr["BASIC_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
                    dr["FINAL_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
                    dr["Amount"] = Amount;
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"].ToString().Trim();
                    //dr["QUOTATION_NO"] = drTemp["QUOTATION_NO"].ToString().Trim();
                    //dr["DEL_DATE"] = drTemp["DEL_DATE"].ToString().Trim();
                    dtPODetail.Rows.Add(dr);

                }
                dtTemp = null;
                btnAdjustIndent.Enabled = false;
            }
        }
        catch
        {
            throw;
        }
    }

    private double GetAdvanceAmount(double AdvancePercent)
    {
        try
        {
            return (FinalTotal * AdvancePercent) / 100;
            
        }
        catch
        {
            throw;
        }
    }

    private void SetFinalTotal()
    {
        //if (txtAdvance.Text != "")
        //{
        //    double advance = 0f;
        //    double.TryParse(txtAdvance.Text.Trim(), out advance);
        //    txtAdvanceAmount.Text = GetAdvanceAmount(advance).ToString();
        //}
    }

    protected void txtAdvance_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    CalculateFinalTotal();
        //    if (txtAdvance.Text != "")
        //    {
        //        double advance = 0f;
        //        double.TryParse(txtAdvance.Text.Trim(), out advance);
        //        txtAdvanceAmount.Text = GetAdvanceAmount(advance).ToString();
        //    }
        //}
        //catch (Exception ex)
        //{

        //    errorLog.ErrHandler.WriteError(ex.Message);
        //}
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (UpdateMode)
            {
                if (txtOrderNumber.Text != null)
                {
                    //    DeletePOMasterData();
                }

            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Fabric/FabricSaleWork/Reports/FabricPOPermtRPT.aspx?PO_TYPE=" + ddlOrderType.SelectedValue.Trim(), false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Printing.\r\nSee error log for detail."));

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Clearing.\r\nSee error log for detail."));

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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void ddlOrderNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();          
            var data = GetPOs(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading po number for updation.\r\nSee error log for detail."));

            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetPOs(string text, int startOffset, int numberOfItems)
    {
        try
        {
            var oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            var whereClause = " where PO_NUMB like :searchQuery or prty_name like :searchQuery";
            var sortExpression = " order by PO_NUMB desc, PO_DATE desc";
            var commandText = "select * from (Select a.PO_NUMB, a.PO_DATE,b.PRTY_NAME,a.PO_NATURE from TX_FABRIC_PU_MST a, tx_vendor_mst b Where a.prty_code=b.prty_code (+) and A.COMP_CODE=:COMP_CODE AND A.BRANCH_CODE=:BRANCH_CODE AND A.PO_TYPE=:IND_TYPE) asd";
            return SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlOrderType.SelectedValue.Trim(), commandText, whereClause, sortExpression, "", text + "%");
                       
        }
        catch
        {
            throw;
        }
    }

    protected void ddlOrderNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            var OrderNumber = ddlOrderNumber.SelectedValue.Trim();
            if (dtPODetail == null || dtPODetail.Rows.Count == 0)
            {
                CreateMaterialPODetailTable();
            }
            dtPODetail.Rows.Clear();
            FinalTotal = 0;
            int iRecordFound = GetdataByOrderNumber(CommonFuction.funFixQuotes(OrderNumber));
            BindMaterialPOCredittoGrid();
            if (iRecordFound > 0)
            {
                UpdateMode = true;
            }
            else
            {
                InitialisePage();
                lblMode.Text = "Update";
                txtOrderNumber.Text = "";
                UpdateMode = false;
                ActivateUpdateMode();
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                string msg = "Dear " + oUserLoginDetail.Username + "!! Purchase Order already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            ddlOrderNumber.Visible = true;
            ddlOrderNumber.SelectedValue = string.Empty;
            ddlOrderNumber.SelectedText = string.Empty;
            ddlOrderNumber.SelectedIndex = -1;
            ddlOrderNumber.Items.Clear();
            imgbtnSave.Visible = false;
            imgbtnUpdate.Visible = true;
            txtOrderNumber.Visible = false;
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
            ddlOrderNumber.Visible = false;
            ddlOrderNumber.SelectedValue = string.Empty;
            ddlOrderNumber.SelectedText = string.Empty;
            ddlOrderNumber.SelectedIndex = -1;
            ddlOrderNumber.Items.Clear();
            imgbtnUpdate.Visible = false;
            imgbtnSave.Visible = true;
            txtOrderNumber.Visible = true;
        }
        catch
        {
            throw;
        }
    }

    protected void chkFetchIndent_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckFetchIndent();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fetching Indent .\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void CheckFetchIndent()
    {
        try
        {
            if (chkFetchIndent.Checked)
            {             
               var  dt = SaitexDL.Interface.Method.TX_FABRIC_PU_MST.GetApprovedIndentDataForPO(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                if (dt.Rows.Count > 0)
                {
                    BindFetchFromIndent(dt);
                    ddlIndent.Visible = true;
                }
                else
                {
                    ddlIndent.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No Indent Exists for PO');", true);
                }
            }
            else
            {
                chkFetchIndent.Checked = false;
                ddlIndent.Visible = false;
                if (dtPODetail != null && dtPODetail.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Clear The Grid Before Unselect');", true);
                }
                else
                {
                    ddlIndent.Visible = false;
                }
            }
        }
        catch
        {
            throw;


        }

    }

    private void BindFetchFromIndent(DataTable dt)
    {
        try
        {
           
            ddlIndent.DataSource = dt;
            ddlIndent.DataValueField = "IND_VAL";
            ddlIndent.DataTextField = "IND_DISP";
            ddlIndent.DataBind();
            ddlIndent.Items.Insert(0, "Select");
        }
        catch
        {
            throw;
        }
    }

    protected void ddlIndent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var ss = ddlIndent.SelectedValue.Trim();
            var IND_VAL = ss.Split('@');
            int _indNo = 0;
            int.TryParse(IND_VAL[1].ToString(),out  _indNo);
            int IndentNumber = _indNo ;
            var IND_TYPE = IND_VAL[0].ToString();
            var dt = SaitexDL.Interface.Method.TX_FABRIC_PU_MST.Select_ApprovedTransactionByIndentNumber(oUserLoginDetail.DT_STARTDATE.Year, IndentNumber, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, IND_TYPE);
            MapIndentDataTable(dt);
            Datatableforadjustment(dt);
            BindMaterialPOCredittoGrid();
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Indent Index Changing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable createAdjTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("YEAR", typeof(int));
        dt.Columns.Add("IND_NUMB", typeof(string));
        dt.Columns.Add("FABR_CODE", typeof(string));
        dt.Columns.Add("ADJUST_QTY", typeof(double));
        dt.Columns.Add("APPR_QTY", typeof(double));
        dt.Columns.Add("IND_TYPE", typeof(string));
        dt.Columns.Add("IND_BRANCH_CODE", typeof(string));
        dt.Columns.Add("SHADE_CODE", typeof(string));
        return dt;
    }

    private DataTable Datatableforadjustment(DataTable dtAdjust)
    {
        try
        {
            DataTable dtItemIndent = new DataTable();
            if (dtItemIndent == null || dtItemIndent.Rows.Count == 0)
                dtItemIndent = createAdjTable();
            dtItemIndent.Rows.Clear();
            if (dtAdjust != null && dtAdjust.Rows.Count > 0)
            {
                foreach (DataRow drAdjust in dtAdjust.Rows)
                {
                    var dr = dtItemIndent.NewRow();
                    // dr["YEAR"] = drAdjust["YEAR"].ToString().Trim();
                    dr["IND_NUMB"] = drAdjust["IND_NUMB"].ToString().Trim();
                    dr["FABR_CODE"] = drAdjust["FABR_CODE"].ToString().Trim();
                    dr["ADJUST_QTY"] = drAdjust["ORD_QTY"].ToString().Trim();
                    dr["APPR_QTY"] = drAdjust["ORD_QTY"].ToString().Trim();
                    dr["IND_TYPE"] = drAdjust["IND_TYPE"].ToString().Trim();
                    dr["IND_BRANCH_CODE"] = drAdjust["BRANCH_CODE"].ToString().Trim();
                    // dr["IND_BRANCH_NAME"] = drAdjust["BRANCH_NAME"].ToString().Trim();
                    dr["SHADE_CODE"] = drAdjust["SHADE_CODE"].ToString().Trim();
                    dtItemIndent.Rows.Add(dr);
                }
                //dtAdjust = null;
            }
            Session["dtItemIndent"] = dtItemIndent;
            return dtItemIndent;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
  
    protected void ddlDelAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindDeliveryAddByCode(ddlDelAdd.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Delivery branch selection.\r\n See error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindDeliveryAddByCode(string Del_BranchCode)
    {
        try
        {
            if (ddlDelAdd.SelectedValue != "")
            {
                var dtDelBranch = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMasterByBranchCode(oUserLoginDetail.COMP_CODE, Del_BranchCode);
                if (dtDelBranch != null && dtDelBranch.Rows.Count > 0)
                {
                    txtDelAddress.Text = dtDelBranch.Rows[0]["BRANCH_ADD"].ToString();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindDelBranch()
    {
        try
        {
            ddlDelAdd.Items.Clear();
            var dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMasterByCompCode(oUserLoginDetail.COMP_CODE);
            ddlDelAdd.DataSource = dt;
            ddlDelAdd.DataTextField = "BRANCH_NAME";
            ddlDelAdd.DataValueField = "BRANCH_CODE";
            ddlDelAdd.DataBind();
            ddlDelAdd.SelectedIndex = ddlDelAdd.Items.IndexOf(ddlDelAdd.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));
            BindDeliveryAddByCode(ddlDelAdd.SelectedValue.Trim());
        }
        catch
        {
            throw;
        }
    }

    protected void txtItemCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var data = BindItemCodeCombo(e.Text.ToUpper(), e.ItemsOffset);          
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable BindItemCodeCombo(string text, int startOffset)
    {
        try
        {
            var CommandText = "SELECT   * FROM   (  SELECT   DISTINCT *  FROM   (  SELECT   pt.comp_code, PT.SHADE_CODE, pt.IND_TYPE, i.FABR_CODE, i.FABR_TYPE, i.FABR_DESC, SUM (NVL (pt.APPR_QTY, 0)) APPR_QTY, SUM (NVL (PT.PUR_ADJ_QTY, 0)) PUR_ADJ_QTY, SUM(NVL (pt.APPR_QTY, 0)     - NVL (PT.PUR_ADJ_QTY, 0))    AS bal_qty  FROM   TX_FABRIC_IND_TRN pt, TX_FABRIC_MST i             WHERE       i.FABR_CODE = pt.FABR_CODE AND NVL (pt.APPR_QTY, 0) > 0 AND NVL (pt.APPR_QTY, 0)    - NVL (PT.PUR_ADJ_QTY, 0) <> 0 AND NVL (pt.IND_CLOSE_FLAG, 0) <> 3          GROUP BY   pt.comp_code, pt.SHADE_CODE, pt.IND_TYPE, i.FABR_TYPE, i.FABR_CODE, i.FABR_DESC) asd WHERE   FABR_CODE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery          ORDER BY   FABR_CODE ASC) WHERE   ROWNUM <= 15 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'";
            var whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " WHERE   ROWNUM <= 15 AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND FABR_CODE NOT IN  (SELECT   FABR_CODE FROM   (  SELECT   DISTINCT * FROM   (  SELECT   pt.comp_code, PT.SHADE_CODE, pt.IND_TYPE, i.FABR_CODE, i.FABR_DESC, i.FABR_TYPE, SUM (NVL (pt.APPR_QTY, 0))    APPR_QTY, SUM (NVL (PT.PUR_ADJ_QTY, 0))    PUR_ADJ_QTY, SUM(NVL (pt.APPR_QTY, 0)     - NVL (PT.PUR_ADJ_QTY, 0))    AS bal_qty FROM   TX_FABRIC_IND_TRN pt, TX_FABRIC_MST i WHERE   i.FABR_CODE = pt.FABR_CODE AND NVL (pt.APPR_QTY, 0) > 0 AND NVL (pt.APPR_QTY, 0)    - NVL (PT.PUR_ADJ_QTY, 0) <>       0 AND NVL (pt.IND_CLOSE_FLAG, 0) <>       3 GROUP BY   pt.comp_code, pt.SHADE_CODE, pt.ind_type, i.FABR_CODE, i.FABR_DESC, i.FABR_TYPE) WHERE   FABR_CODE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY   FABR_CODE ASC) dss WHERE  ROWNUM <= " + startOffset + " AND comp_code =  '" + oUserLoginDetail.COMP_CODE + "')";
            }
            var SortExpression = " order by FABR_CODE";
            var SearchQuery = text.ToUpper() + "%";
           return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {


        var CommandText = " SELECT *FROM (SELECT DISTINCT *FROM (SELECT pt.comp_code, pt.IND_TYPE, i.FABR_CODE, i.FABR_DESC, SUM (NVL (pt.APPR_QTY, 0)) APPR_QTY, SUM (NVL (PT.PUR_ADJ_QTY, 0)) PUR_ADJ_QTY, SUM(NVL (pt.APPR_QTY, 0) - NVL (PT.PUR_ADJ_QTY, 0))AS bal_qty FROM YRN_IND_TRN pt, YRN_MST i WHERE i.FABR_CODE = pt.FABR_CODE AND NVL (pt.APPR_QTY, 0) > 0 AND NVL (pt.APPR_QTY, 0)- NVL (PT.PUR_ADJ_QTY, 0) <> 0 AND NVL (IND_CLOSE_FLAG, 0) <> 3GROUP BY pt.comp_code, pt.ind_type, i.FABR_CODE, i.FABR_DESC) asd WHERE FABR_CODE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY FABR_CODE ASC) WHERE comp_code = '" + oUserLoginDetail.COMP_CODE + "'";
        var WhereClause = "  ";
        var SortExpression = " ";
        var SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
                
    }

    protected void txtItemCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            var Description = "";
            var UOM = "";
            double OpeningRate = 0;
            var SHADE_CODE = string.Empty;
            var thisTextBox = (ComboBox)txtItemCode;

            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());
            if (!SearchItemCodeInGrid(thisTextBox.SelectedText.Trim(), UniqueId))
            {
                int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(thisTextBox.SelectedText.Trim()), out Description, out UOM, out OpeningRate);
                if (iRecordFound > 0)
                {
                    lblItemCode.Text = thisTextBox.SelectedText.Trim();
                    txtBaseRate.Text = OpeningRate.ToString();
                    txtItemDescription.Text = Description;
                    txtFinalRate.Text = OpeningRate.ToString();
                    txtUnit.Text = UOM;
                    txtShadeCode.Text = thisTextBox.SelectedValue.Trim();
                    btnAdjustIndent.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jsfun", "SetFocus('" + btnAdjustIndent.ClientID + "');", true);
                }
                else
                {
                    txtBaseRate.Text = "0";
                    txtItemDescription.Text = "";
                    txtUnit.Text = "";
                    txtShadeCode.Text = string.Empty;
                    thisTextBox.SelectedIndex = -1;
                    thisTextBox.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jsfun", "SetFocus('" + thisTextBox.ClientID + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('This Fabric already included');", true);
                thisTextBox.SelectedIndex = -1;
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fabric selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["dtPODetail"] != null)
            {
                dtPODetail = (DataTable)ViewState["dtPODetail"];
            }
            if (txtOrderNumber.Text != "")
            {
                if (dtPODetail != null && dtPODetail.Rows.Count > 0 && txtPartyCode.SelectedIndex >= 0)
                {
                    saveMaterialPOOrderCreditMST();
                }
                else if (dtPODetail == null || dtPODetail.Rows.Count == 0)
                {
                    CommonFuction.ShowMessage("Please select atleast one Yarn to generate purchase order");
                }
                else if (txtPartyCode.SelectedIndex < 0)
                {
                    CommonFuction.ShowMessage("Please select party to generate purchase order");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please enter purchase order");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}

