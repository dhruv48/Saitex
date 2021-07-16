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
using Common;
using errorLog;
using Obout.ComboBox;
public partial class Module_Yarn_SalesWork_Controls_YARN_SO : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.YRN_SO_MST oYRN_SO_MST = new SaitexDM.Common.DataModel.YRN_SO_MST();
    private  DataTable dtSODetail = null;
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
        }
    }

    private void InitialisePage()
    {
        try
        {
            //btnAdjustIndent.Enabled = true; 
            ActivateSaveMode();
           // ddlIndent.Visible = false;
           // chkFetchIndent.Checked = false;

            txtOrderNumber.AutoPostBack = false;
            txtOrderNumber.ReadOnly = true;
            lblMode.Text = "Save";
            ddlOrderType.Enabled = true;
            txtOrderNumber.Enabled = true;
            UpdateMode = false;
            BindCurrency();
            BindSONature();
            txtOrderNumber.Text = "";
            getSOMaxId();
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
            txtconversionRate.Text = "1.00";

            if (dtSODetail == null)
                CreateMaterialSODetailTable();
            dtSODetail.Rows.Clear();

            BindMaterialSOCredittoGrid();

            BindItemCodeCombo("");

            RefreshDetailRow();
            Session["dtItemIndent"] = null;
            Session["dtDicRate"] = null;
            //chkFetchIndent.Checked = false;
        }
        catch
        {
            throw;
        }
    }

    private void getSOMaxId()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            oYRN_SO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_SO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_SO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_SO_MST.SO_TYPE = ddlOrderType.SelectedValue.Trim();
            txtOrderNumber.Text = (int.Parse(SaitexBL.Interface.Method.YRN_SO_MST.GetNewSONo(oYRN_SO_MST.SO_TYPE, oYRN_SO_MST.COMP_CODE, oYRN_SO_MST.BRANCH_CODE)) + 1).ToString();
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
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("CURRENCY", oUserLoginDetail.COMP_CODE);
            txtCurrencyCode.DataSource = dt;
            txtCurrencyCode.DataTextField = "MST_CODE";
            txtCurrencyCode.DataValueField = "MST_CODE";
            txtCurrencyCode.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void BindSONature()
    {
        try
        {
            ddlPONature.Items.Clear();

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PO_NATURE", oUserLoginDetail.COMP_CODE);
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

    private void CreateMaterialSODetailTable()
    {
        try
        {
            dtSODetail = new DataTable();
            dtSODetail.Columns.Add("UniqueId", typeof(int));
            dtSODetail.Columns.Add("SO_NUMB", typeof(string));
            dtSODetail.Columns.Add("YARN_CODE", typeof(string));
            dtSODetail.Columns.Add("YARN_DESC", typeof(string));
            dtSODetail.Columns.Add("ORD_QTY", typeof(double));
            dtSODetail.Columns.Add("UOM", typeof(string));
            dtSODetail.Columns.Add("BASIC_RATE", typeof(double));
            dtSODetail.Columns.Add("FINAL_RATE", typeof(double));
            dtSODetail.Columns.Add("Amount", typeof(double));
            dtSODetail.Columns.Add("QUOTATION_NO", typeof(string));
            dtSODetail.Columns.Add("DEL_DATE", typeof(DateTime));

            ViewState["dtSODetail"] = dtSODetail;
        }
        catch
        {
            throw;
        }
    }

    private void BindMaterialSOCredittoGrid()
    {
        try
        {
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];
            gvMaterialSOTRN.DataSource = dtSODetail;
            gvMaterialSOTRN.DataBind();

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
            txtItemDescription.Text = "";
            txtOrderQty.Text = "";
            txtUnit.Text = "";
            txtBaseRate.Text = "";
            txtFinalRate.Text = "";
            txtAmount.Text = "";
            txtQuotation.Text = "";
            txtTrnDeliveryDate.Text = System.DateTime.Now.Date.ToShortDateString();

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
            getSOMaxId();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in order type selection.\r\nSee error log for detail."));
        }
    }

    protected void txtPartyCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper());

            txtPartyCode.Items.Clear();

            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
        }
    }

    private DataTable GetPartyData(string Text)
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

    protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
        }
    }

    protected void txtTransporterCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper());

            txtTransporterCode.Items.Clear();

            txtTransporterCode.DataSource = data;
            txtTransporterCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
        }
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
        }
    }

    protected void txtItemCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindItemCodeCombo(e.Text.ToUpper());
            e.ItemsLoadedCount = txtItemCode.Items.Count;
            e.ItemsCount = txtItemCode.Items.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
        }
    }

    private void BindItemCodeCombo(string text)
    {
        try
        {
            string CommandText = "SELECT   DISTINCT *  FROM   (SELECT   i.* FROM   YRN_MST i WHere  YARN_CAT <> 'SEWING THREAD')asd";
            string WhereClause = "  where YARN_CODE like :SearchQuery or YARN_CAT like :SearchQuery OR YARN_DESC like :SearchQuery or YARN_TYPE like :SearchQuery";
            string SortExpression = " order by YARN_CODE asc";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            txtItemCode.Items.Clear();

            if (data != null && data.Rows.Count > 0)
            {

                DataView dvItem = new DataView(data);
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                dvItem.RowFilter = "COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "'";
                if (dvItem.Count > 0)
                {
                    txtItemCode.DataSource = dvItem;
                    txtItemCode.DataBind();
                }
                else
                {
                    // CommonFuction.ShowMessage("No Yarns Exists for PO");
                }
            }
            else
            {
                //  CommonFuction.ShowMessage("No Yarns Exists for PO");
            }
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
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];
            if (dtSODetail != null && dtSODetail.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtSODetail.Rows)
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
                if (dr1["YARN_CODE"].ToString() == dr["YARN_CODE"].ToString())
                {
                    dt.Rows.Remove(dr1);
                    break;
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

    protected void txtItemCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string Description = "";
            string UOM = "";
            double OpeningRate = 0;
            ComboBox thisTextBox = (ComboBox)txtItemCode;

            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());
            if (!SearchItemCodeInGrid(thisTextBox.SelectedValue.Trim(), UniqueId))
            {
                int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(thisTextBox.SelectedValue.Trim()), out Description, out UOM, out OpeningRate);

                if (iRecordFound > 0)
                {
                    txtBaseRate.Text = OpeningRate.ToString();
                    txtItemDescription.Text = Description;
                    txtFinalRate.Text = OpeningRate.ToString();
                    txtUnit.Text = UOM;
                  //  btnAdjustIndent.Focus();

                   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jsfun", "SetFocus('" + btnAdjustIndent.ClientID + "');", true);
                }
                else
                {
                    txtBaseRate.Text = "0";
                    txtItemDescription.Text = "";
                    txtUnit.Text = "";
                    thisTextBox.SelectedIndex = -1;
                    thisTextBox.Focus();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jsfun", "SetFocus('" + thisTextBox.ClientID + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('This Yarn already included');", true);
                thisTextBox.SelectedIndex = -1;
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn selection.\r\nSee error log for detail."));
        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            if (gvMaterialSOTRN.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in gvMaterialSOTRN.Rows)
                {
                    Label txtItemCode1 = (Label)grdRow.FindControl("txtItemCode");
                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUniqueId = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (txtItemCode1.Text.Trim() == ItemCode && UniqueId != iUniqueId)
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

        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dts = SaitexBL.Interface.Method.YRN_MST.GetItemDetailByItemCode(ItemCode, oUserLoginDetail.DT_STARTDATE.Year, "", "");
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["YARN_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UNIT_NAME"].ToString().Trim();
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
            if (txtItemCode.SelectedValue != "" || txtItemCode.SelectedIndex != -1)
            {
                string URL = "POIndentAdjustment.aspx";
                URL = URL + "?ItemCodeId=" + txtItemCode.SelectedValue.Trim();
                URL = URL + "&TextBoxId=" + txtOrderQty.ClientID;

                URL = URL + "&PONum=" + txtOrderNumber.Text.Trim();
                string PO_TYPE = "PUM";
                URL = URL + "&PO_TYPE=" + PO_TYPE;

                txtOrderQty.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=400,height=320,left=200,top=300');", true);
            }
            else
            {
                CommonFuction.ShowMessage("Please select Yarn to adjust Indent");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting adjustment.\r\nSee error log for detail."));
        }
    }

    protected void txtOrderQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtOrderQty;
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
            TextBox thisTextBox = (TextBox)txtBaseRate;
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
        }
    }

    protected void btnDiscountTaxes_Click1(object sender, EventArgs e)
    {
        try
        {
            if (txtItemCode.SelectedValue != "" || txtItemCode.SelectedIndex != -1)
            {
                string URL = "GetSODisTex.aspx";
                URL = URL + "?FinalAmount=" + txtBaseRate.Text.Trim();
                URL = URL + "&TextBoxId=" + txtFinalRate.ClientID;
                URL = URL + "&YARN_CODE=" + txtItemCode.SelectedValue.Trim();

                if (UpdateMode)
                {
                    URL = URL + "&SONum=" + txtOrderNumber.Text.Trim();
                    string SO_TYPE = "PUM";
                    URL = URL + "&SO_TYPE=" + SO_TYPE;
                }
                txtFinalRate.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=400,height=360,left=200,top=300');", true);
            }
            else
            {
                CommonFuction.ShowMessage("Please select Yarn to add rate component");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Rate Component.\r\nSee error log for detail."));
        }
    }

    protected void txtFinalRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtFinalRate;
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
        }
    }

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];
            double BaseRate = 0f;
            double FinalRate = 0f;
            double.TryParse(CommonFuction.funFixQuotes(txtBaseRate.Text.Trim()), out BaseRate);
            double.TryParse(CommonFuction.funFixQuotes(txtFinalRate.Text.Trim()), out FinalRate);
            if (FinalRate != 0f && BaseRate != 0f)
            {
                if (dtSODetail == null)
                    CreateMaterialSODetailTable();

                if (dtSODetail.Rows.Count < 15)
                {
                    txtAmount.Text = CalculateAmount().ToString();

                    if (txtItemCode.SelectedText != "" && txtOrderQty.Text != "" && txtFinalRate.Text != "" && txtAmount.Text != "")
                    {
                        int UniqueId = 0;
                        if (ViewState["UniqueId"] != null)
                            UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                        bool bb = SearchItemCodeInGrid(txtItemCode.SelectedValue.Trim(), UniqueId);
                        if (!bb)
                        {
                            double Qty = 0;
                            double.TryParse(txtOrderQty.Text.Trim(), out Qty);
                            if (Qty > 0)
                            {
                                if (UniqueId > 0)
                                {
                                    DataView dv = new DataView(dtSODetail);
                                    dv.RowFilter = "UniqueId=" + UniqueId;
                                    if (dv.Count > 0)
                                    {
                                        dv[0]["SO_NUMB"] = txtOrderNumber.Text;
                                        dv[0]["YARN_CODE"] = txtItemCode.SelectedValue.Trim();
                                        dv[0]["YARN_DESC"] = txtItemDescription.Text.Trim();
                                        dv[0]["ORD_QTY"] = double.Parse(txtOrderQty.Text.Trim());
                                        dv[0]["UOM"] = txtUnit.Text.Trim();
                                        dv[0]["BASIC_RATE"] = double.Parse(txtBaseRate.Text.Trim());
                                        dv[0]["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                        dv[0]["Amount"] = double.Parse(txtAmount.Text.Trim());
                                        dv[0]["QUOTATION_NO"] = txtQuotation.Text.Trim();
                                        DateTime dd = System.DateTime.Now;
                                        DateTime.TryParse(txtTrnDeliveryDate.Text.Trim(), out dd);
                                        dv[0]["DEL_DATE"] = dd;
                                        FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
                                        dtSODetail.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtSODetail.NewRow();
                                    dr["UniqueId"] = dtSODetail.Rows.Count + 1;
                                    dr["SO_NUMB"] = txtOrderNumber.Text;
                                    dr["YARN_CODE"] = txtItemCode.SelectedValue.Trim();
                                    dr["YARN_DESC"] = txtItemDescription.Text.Trim();
                                    dr["ORD_QTY"] = double.Parse(txtOrderQty.Text.Trim());
                                    dr["UOM"] = txtUnit.Text.Trim();
                                    dr["BASIC_RATE"] = double.Parse(txtBaseRate.Text.Trim());
                                    dr["FINAL_RATE"] = double.Parse(txtFinalRate.Text.Trim());
                                    dr["Amount"] = double.Parse(txtAmount.Text.Trim());
                                    dr["QUOTATION_NO"] = txtQuotation.Text.Trim();
                                    DateTime dd = System.DateTime.Now;
                                    DateTime.TryParse(txtTrnDeliveryDate.Text.Trim(), out dd);
                                    dr["DEL_DATE"] = dd;
                                    FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
                                    dtSODetail.Rows.Add(dr);
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

                    ViewState["dtSODetail"] = dtSODetail;
                    gvMaterialSOTRN.DataSource = dtSODetail;
                    gvMaterialSOTRN.DataBind();
                    CalculateFinalTotal();
                }
                else
                {
                    CommonFuction.ShowMessage("You have reached the limit of Yarns. Only 15 Yarns allowed in one Purchase Order.");
                }
            }
            else
            {
                CommonFuction.ShowMessage("You have entered base rate/ final rate Zero. Please enter proper base rate or Dis/ Taxes.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn Detail Row.\r\nSee error log for detail."));
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
        }
    }

    protected void gvMaterialSOTRN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "SOMateialCreditDelete")
            {
                deleteSOMaterialCreditRow(UniqueId);
                BindMaterialSOCredittoGrid();
            }
            if (e.CommandName == "SOMateialCreditEdit")
            {
                FillDetailByGrid(UniqueId);
            }
            CalculateFinalTotal();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row updation / deletion.\r\nSee error log for detail."));
        }
    }

    private void deleteSOMaterialCreditRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];
            if (gvMaterialSOTRN.Rows.Count == 1)
            {
                dtSODetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtSODetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtSODetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtSODetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
                ViewState["dtSODetail"] = dtSODetail;
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
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];

            DataView dv = new DataView(dtSODetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                txtItemCode.SelectedValue = dv[0]["YARN_CODE"].ToString();
                txtItemDescription.Text = dv[0]["YARN_DESC"].ToString();
                txtOrderQty.Text = dv[0]["ORD_QTY"].ToString();
                txtUnit.Text = dv[0]["UOM"].ToString();
                txtBaseRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtAmount.Text = dv[0]["Amount"].ToString();
                txtQuotation.Text = dv[0]["QUOTATION_NO"].ToString();
                txtTrnDeliveryDate.Text = dv[0]["DEL_DATE"].ToString();
                ViewState["UniqueId"] = UniqueId;
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
            if (gvMaterialSOTRN.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in gvMaterialSOTRN.Rows)
                {
                    Label txtItemCode = (Label)grdRow.FindControl("txtItemCode");
                    Label txtAmount = (Label)grdRow.FindControl("txtAmount");

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
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];

            if (txtOrderNumber.Text != "")
            {
                if (dtSODetail != null && dtSODetail.Rows.Count > 0 && txtPartyCode.SelectedIndex >= 0)
                {
                    if (UpdateMode)
                    {
                        UpdateMaterialSOCreditMST();
                    }
                    else
                    {
                        saveMaterialSOOrderCreditMST();
                    }
                }
                else if (dtSODetail == null || dtSODetail.Rows.Count == 0)
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in SO updation.\r\nSee error log for detail."));
        }
    }

    private void saveMaterialSOOrderCreditMST()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.YRN_SO_MST oYRN_SO_MST = new SaitexDM.Common.DataModel.YRN_SO_MST();

            oYRN_SO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_SO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_SO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_SO_MST.SO_TYPE = ddlOrderType.SelectedValue.Trim();
            oYRN_SO_MST.SO_NUMB = int.Parse(txtOrderNumber.Text.Trim());
            oYRN_SO_MST.SO_DATE = DateTime.Parse(txtOrderDate.Text.Trim());
            oYRN_SO_MST.SO_NATURE = ddlPONature.SelectedValue.Trim();
            oYRN_SO_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oYRN_SO_MST.DEL_DATE = DateTime.Parse(txtDeliveryDate.Text.Trim());
            oYRN_SO_MST.PAY_TERM = CommonFuction.funFixQuotes(txtPayTerm.Text.Trim());
            //if (radConfirm.SelectedItem.Value == "Yes")
            //    oYRN_SO_MST.CONF_FLAG = true;
            //else
            oYRN_SO_MST.CONF_FLAG = false;
            oYRN_SO_MST.COMMENTS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_SO_MST.DELV_BRANCH = CommonFuction.funFixQuotes(txtDelAddress.Text.Trim());

            if (txtTransporterCode.SelectedIndex < 0)
                oYRN_SO_MST.TRSP_CODE = "NA";
            else
                oYRN_SO_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

            oYRN_SO_MST.DESP_MODE = CommonFuction.funFixQuotes(txtDespatchMode.Text.Trim());
            //if (radInsurance.SelectedItem.Value == "Yes")
            //    oYRN_SO_MST.INSU_FLAG = true;
            //else
            oYRN_SO_MST.INSU_FLAG = false;
            oYRN_SO_MST.ADV_PRCNT = 0;// double.Parse(txtAdvance.Text.Trim());
            CalculateFinalTotal();
            oYRN_SO_MST.FINAL_AMT = FinalTotal;
            oYRN_SO_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_SO_MST.CURRENCY_CODE = txtCurrencyCode.SelectedValue.Trim();
            oYRN_SO_MST.CONVERSION_RATE = txtconversionRate.Text.Trim();
            oYRN_SO_MST.INSTRUCTIONS = txtInstructions.Text.Trim();
            DataTable dtItemIndent = (DataTable)Session["dtItemIndent"];
            DataTable dtRateCompo = (DataTable)Session["dtDicRate"];
            int SO_NUMB = 0;

            bool bResult = SaitexBL.Interface.Method.YRN_SO_MST.Insert(oYRN_SO_MST, dtSODetail, dtItemIndent, dtRateCompo, out SO_NUMB);

            if (bResult)
            {

                string msg = "SO Number " + SO_NUMB + " Successfully Saved.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
                InitialisePage();
               // chkFetchIndent.Checked = false;

               // ddlIndent.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }

    private void UpdateMaterialSOCreditMST()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.YRN_SO_MST oYRN_SO_MST = new SaitexDM.Common.DataModel.YRN_SO_MST();

            oYRN_SO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_SO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_SO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_SO_MST.SO_TYPE = ddlOrderType.SelectedValue.Trim();
            oYRN_SO_MST.SO_NUMB = int.Parse(txtOrderNumber.Text.Trim());
            oYRN_SO_MST.SO_DATE = DateTime.Parse(txtOrderDate.Text.Trim());
            oYRN_SO_MST.SO_NATURE = ddlPONature.SelectedValue.Trim();
            oYRN_SO_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
            oYRN_SO_MST.DEL_DATE = DateTime.Parse(txtDeliveryDate.Text.Trim());
            oYRN_SO_MST.PAY_TERM = CommonFuction.funFixQuotes(txtPayTerm.Text.Trim());
            //if (radConfirm.SelectedItem.Value == "Yes")
            //    oYRN_SO_MST.CONF_FLAG = true;
            //else
            oYRN_SO_MST.CONF_FLAG = false;
            oYRN_SO_MST.COMMENTS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oYRN_SO_MST.DELV_BRANCH = CommonFuction.funFixQuotes(txtDelAddress.Text.Trim());

            if (txtTransporterCode.SelectedIndex < 0)
                oYRN_SO_MST.TRSP_CODE = "NA";
            else
                oYRN_SO_MST.TRSP_CODE = CommonFuction.funFixQuotes(txtTransporterCode.SelectedText.Trim());

            oYRN_SO_MST.DESP_MODE = CommonFuction.funFixQuotes(txtDespatchMode.Text.Trim());
            //if (radInsurance.SelectedItem.Value == "Yes")
            //    oYRN_SO_MST.INSU_FLAG = true;
            //else
            oYRN_SO_MST.INSU_FLAG = false;
            oYRN_SO_MST.ADV_PRCNT = 0;// double.Parse(txtAdvance.Text.Trim());
            CalculateFinalTotal();
            oYRN_SO_MST.FINAL_AMT = FinalTotal;
            oYRN_SO_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_SO_MST.CURRENCY_CODE = txtCurrencyCode.SelectedValue.Trim();
            oYRN_SO_MST.CONVERSION_RATE = txtconversionRate.Text.Trim();
            oYRN_SO_MST.INSTRUCTIONS = txtInstructions.Text.Trim();
            DataTable dtItemIndent = (DataTable)Session["dtItemIndent"];
            DataTable dtRateCompo = (DataTable)Session["dtDicRate"];
            bool bResult = SaitexBL.Interface.Method.YRN_SO_MST.Update(oYRN_SO_MST, dtSODetail, dtItemIndent, dtRateCompo);

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
        }
    }

    protected void txtOrderNumber_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];

            if (dtSODetail == null || dtSODetail.Rows.Count == 0)
                CreateMaterialSODetailTable();
            dtSODetail.Rows.Clear();
            FinalTotal = 0;

            int iRecordFound = GetdataByOrderNumber(CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim()));
            BindMaterialSOCredittoGrid();
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

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid SO Number');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in SO order number selection.\r\nSee error log for detail."));
        }
    }

    private int GetdataByOrderNumber(string SONumber)
    {
        int iRecordFound = 0;
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            SaitexDM.Common.DataModel.YRN_SO_MST oYRN_SO_MST = new SaitexDM.Common.DataModel.YRN_SO_MST();
            oYRN_SO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_SO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_SO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_SO_MST.SO_TYPE = ddlOrderType.SelectedValue.Trim();
            oYRN_SO_MST.SO_NUMB = int.Parse(SONumber);
            DataTable dt = SaitexBL.Interface.Method.YRN_SO_MST.SelectBySONumber(oYRN_SO_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtOrderDate.Text = DateTime.Parse(dt.Rows[0]["SO_DATE"].ToString().Trim()).ToShortDateString();
                txtDelAddress.Text = dt.Rows[0]["DELV_BRANCH"].ToString().Trim();
                txtDeliveryDate.Text = DateTime.Parse(dt.Rows[0]["DEL_DATE"].ToString().Trim()).ToShortDateString();
                txtDespatchMode.Text = dt.Rows[0]["DESP_MODE"].ToString().Trim();
                txtPayTerm.Text = dt.Rows[0]["PAY_TERM"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["COMMENTS"].ToString().Trim();
                txtCurrencyCode.Text = dt.Rows[0]["CURRENCY_CODE"].ToString().Trim();
                txtconversionRate.Text = dt.Rows[0]["CONVERSION_RATE"].ToString().Trim();
                txtInstructions.Text = dt.Rows[0]["INSTRUCTIONS"].ToString().Trim();

                string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0) asd";
                string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
                string SortExpression = " order by PRTY_CODE asc";
                string SearchQuery = "%";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

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
                DataTable dtTemp = GetTRNdataByOrderNumber(SONumber);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                    //DataTable dtIndentAdjustment = SaitexBL.Interface.Method.YRN_SO_MST.GetAdjustIndentByPO("PUM", int.Parse(poNumber), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);

                    GetTaxAdjBySO(SONumber);
                   // Session["dtItemIndent"] = dtIndentAdjustment;

                    txtOrderNumber.Text = SONumber;
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
            DataTable dtRate = new DataTable();
            dtRate.Columns.Add("UniqueId", typeof(int));
            dtRate.Columns.Add("YEAR", typeof(int));
            dtRate.Columns.Add("YARN_CODE", typeof(string));
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

    private void GetTaxAdjBySO(string SONumber)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.YRN_SO_MST oYRN_SO_MST = new SaitexDM.Common.DataModel.YRN_SO_MST();
            oYRN_SO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_SO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_SO_MST.SO_TYPE = ddlOrderType.SelectedValue.Trim();
            oYRN_SO_MST.SO_NUMB = int.Parse(SONumber);

            DataTable dt = SaitexBL.Interface.Method.YRN_SO_MST.GetTaxBySOOnly(oYRN_SO_MST);
            if (dt.Rows.Count > 0)
            {
                DataTable dtRate = CreateTaxDataTable();
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
                DataRow drNew = dtRate.NewRow();
                drNew["UniqueId"] = dtRate.Rows.Count + 1;
                drNew["YEAR"] = dr["YEAR"];
                drNew["YARN_CODE"] = dr["YARN_CODE"];
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

    private DataTable GetTRNdataByOrderNumber(string SONum)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            SaitexDM.Common.DataModel.YRN_SO_MST oYRN_SO_MST = new SaitexDM.Common.DataModel.YRN_SO_MST();
            oYRN_SO_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_SO_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_SO_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_SO_MST.SO_TYPE = ddlOrderType.SelectedValue.Trim();
            oYRN_SO_MST.SO_NUMB = int.Parse(SONum);
            DataTable dtTemp = SaitexBL.Interface.Method.YRN_SO_MST.Select_TransactionBySONumber(oYRN_SO_MST);
            return dtTemp;
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
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];

            if (dtSODetail == null || dtSODetail.Rows.Count == 0)
                CreateMaterialSODetailTable();
            dtSODetail.Rows.Clear();
            FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtSODetail.NewRow();
                    double Amount = 0f;

                    Amount = (double.Parse(drTemp["FINAL_RATE"].ToString().Trim())) * (int.Parse(drTemp["ORD_QTY"].ToString().Trim()));


                    FinalTotal = FinalTotal + Amount;
                    dr["UniqueId"] = dtSODetail.Rows.Count + 1;
                    dr["SO_NUMB"] = drTemp["SO_NUMB"].ToString().Trim();
                    dr["YARN_CODE"] = drTemp["YARN_CODE"].ToString().Trim();
                    dr["YARN_DESC"] = drTemp["YARN_DESC"].ToString().Trim();
                    dr["ORD_QTY"] = drTemp["ORD_QTY"].ToString().Trim();
                    dr["UOM"] = drTemp["UOM"].ToString().Trim();
                    dr["BASIC_RATE"] = drTemp["BASIC_RATE"].ToString().Trim();
                    dr["FINAL_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
                    dr["Amount"] = Amount;
                    dr["QUOTATION_NO"] = drTemp["QUOTATION_NO"].ToString().Trim();
                    dr["DEL_DATE"] = drTemp["DEL_DATE"].ToString().Trim();
                    dtSODetail.Rows.Add(dr);
                }
                dtTemp = null;
                ViewState["dtSODetail"] = dtSODetail;
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
            if (dtSODetail == null || dtSODetail.Rows.Count == 0)
                CreateMaterialSODetailTable();
            dtSODetail.Rows.Clear();
            FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtSODetail.NewRow();
                    double Amount = 0f;

                    Amount = (double.Parse(drTemp["FINAL_RATE"].ToString().Trim())) * (int.Parse(drTemp["ORD_QTY"].ToString().Trim()));


                    FinalTotal = FinalTotal + Amount;
                    dr["UniqueId"] = dtSODetail.Rows.Count + 1;
                    //dr["PO_NUMB"] = drTemp["PO_NUMB"].ToString().Trim();
                    dr["YARN_CODE"] = drTemp["YARN_CODE"].ToString().Trim();
                    dr["YARN_DESC"] = drTemp["YARN_DESC"].ToString().Trim();
                    dr["ORD_QTY"] = drTemp["ORD_QTY"].ToString().Trim();
                    dr["UOM"] = drTemp["UNIT_NAME"].ToString().Trim();
                    dr["BASIC_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
                    dr["FINAL_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
                    dr["Amount"] = Amount;
                    //dr["QUOTATION_NO"] = drTemp["QUOTATION_NO"].ToString().Trim();
                    //dr["DEL_DATE"] = drTemp["DEL_DATE"].ToString().Trim();
                    dtSODetail.Rows.Add(dr);

                }
                dtTemp = null;
               // btnAdjustIndent.Enabled = false;
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
            double totaladvance = (FinalTotal * AdvancePercent) / 100;
            return totaladvance;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in SO deletion.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {  try
        {
        
        Response.Redirect("~/Module/Yarn/SalesWork/Reports/Yarn_PO_Report.aspx?PO_TYPE=" + ddlOrderType.SelectedValue.Trim(), false);
        }
        catch (Exception ex)
        {
          CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in po deletion.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in SO deletion.\r\nSee error log for detail."));
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
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    { try
        {
        base.OnPreRender(e);
        if (lblMode.Text == "Save")
        {
            imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        }
        else
        {
            imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        
        }
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
        }
    catch (Exception ex)
    {
        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
    }
    }

    protected void ddlOrderNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetSOs(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading SO number for updation.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetSOs(string text, int startOffset, int numberOfItems)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string whereClause = " where SO_NUMB like :searchQuery or prty_name like :searchQuery";
            string sortExpression = " order by SO_NUMB desc, SO_DATE desc";
            string commandText = "select * from (Select a.SO_NUMB, a.SO_DATE,b.PRTY_NAME,a.SO_NATURE from YRN_SO_MST a, tx_vendor_mst b Where a.prty_code=b.prty_code (+) and A.COMP_CODE=:COMP_CODE AND A.BRANCH_CODE=:BRANCH_CODE AND A.SO_TYPE=:IND_TYPE) asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlOrderType.SelectedValue.Trim(), commandText, whereClause, sortExpression, "", text + "%");

            return dt;
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
            if (ViewState["dtSODetail"] != null)
                dtSODetail = (DataTable)ViewState["dtSODetail"];

            string OrderNumber = ddlOrderNumber.SelectedValue.Trim();
            if (dtSODetail == null || dtSODetail.Rows.Count == 0)
                CreateMaterialSODetailTable();
            dtSODetail.Rows.Clear();
            FinalTotal = 0;

            int iRecordFound = GetdataByOrderNumber(CommonFuction.funFixQuotes(OrderNumber));
            BindMaterialSOCredittoGrid();
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
                string msg = "Dear " + oUserLoginDetail.Username + "!! Sales Order already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
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
            imgbtnUpdate.ImageUrl = "~/CommonImages/edit1.jpg";
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
            imgbtnUpdate.ImageUrl = "~/CommonImages/save.jpg";
            txtOrderNumber.Visible = true;
        }
        catch
        {
            throw;
        }
    }

   
    private DataTable createAdjTable()
    {
        try
        {
        DataTable dt = new DataTable();
        dt.Columns.Add("YEAR", typeof(int));
        dt.Columns.Add("IND_NUMB", typeof(string));
        dt.Columns.Add("YARN_CODE", typeof(string));
        dt.Columns.Add("ADJUST_QTY", typeof(double));
        dt.Columns.Add("APPR_QTY", typeof(double));
        dt.Columns.Add("IND_TYPE", typeof(string));
        return dt;
        }
        catch {throw;}
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
                    DataRow dr = dtItemIndent.NewRow();
                    // dr["YEAR"] = drAdjust["YEAR"].ToString().Trim();
                    dr["IND_NUMB"] = drAdjust["IND_NUMB"].ToString().Trim();
                    dr["YARN_CODE"] = drAdjust["YARN_CODE"].ToString().Trim();
                    dr["ADJUST_QTY"] = drAdjust["ORD_QTY"].ToString().Trim();
                    dr["APPR_QTY"] = drAdjust["ORD_QTY"].ToString().Trim();
                    dr["IND_TYPE"] = drAdjust["IND_TYPE"].ToString().Trim();
                    dtItemIndent.Rows.Add(dr);
                }
                //dtAdjust = null;
            }
            Session["dtItemIndent"] = dtItemIndent;
            return dtItemIndent;

        }
        catch { throw; }
    }


}
