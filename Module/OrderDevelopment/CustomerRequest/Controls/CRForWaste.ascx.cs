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

public partial class Module_OrderDevelopment_CustomerRequest_Controls_CRForWaste : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string PRODUCT_TYPE = "WASTE";
    private DataTable dtOrderST;
    private static double FinalTotal;
    private string strContext = string.Empty;
    private DataTable dtDicRate = null;
    private static int PO_NUMB = 999998;
    private static string PO_TYPE = "MII";
    private static string PO_COMP_CODE = "999998";
    private static string PO_BRANCH = "999998";
    private static string ORDER_NO = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            txtMatchingReff.AutoPostBack = false;
            txtMatchingReff.Width = Unit.Pixel(100);

            if (!IsPostBack)
            {
                strContext = oUserLoginDetail.COMP_CODE + "@" + oUserLoginDetail.CH_BRANCHCODE;
                aceAgent.ContextKey = strContext;
                InitialiseData();
                if (Request.QueryString["ISREVISED"] != null && Request.QueryString["ORDER_NO"] != null)
                {
                    cmbOrderNo.SelectedIndex = -1;
                    cmbOrderNo.Visible = true;
                    txtOrderNo.Visible = false;
                    lblMode.Text = "You are in Update Mode";
                    tdUpdate.Visible = true;
                    tdSave.Visible = false;
                    DiablePrimaryFields();
                    GETDATA();
                }
            }

            

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialiseData()
    {
        try
        {
            txtCrLocation.Text = oUserLoginDetail.VC_BRANCHNAME;
            Bind_BillingMode();
            bindBusinessType();
            bindEndUse();
            bindOrderType();
            bindDeliveryMode();
            Bind_CaseBox();
            ActivateSaveMode();
            cmbOrderNo.Visible = false;
            txtOrderNo.Visible = true;
            ClearMainData();
            FinalTotal = 0;

            BindCurrency();
            //bindYarnSHADE("YARN_SHADE");

            EnablePrimaryFields();
            BlankSTControls();

            if (ViewState["dtOrderST"] != null)
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            dtOrderST = null;
            ViewState["dtOrderST"] = dtOrderST;
            bindSTGrid();
            ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue("SW"));
            ddlDeliveryMode.SelectedIndex = ddlDeliveryMode.Items.IndexOf(ddlDeliveryMode.Items.FindByValue("BY ROAD"));
            ddlCurrencyCode.SelectedIndex = ddlCurrencyCode.Items.IndexOf(ddlCurrencyCode.Items.FindByValue("Rs."));
            txtShadeName.Text = ddlYarnShade.SelectedValue;
            txtShadeFamilyName.Text = ddlYarnShade.SelectedValue;
            txtConversionRate.Text = "1";
            txtShipment.Text = string.Empty;
            txtBillTo.Text = string.Empty;
            txtPaymentTerm.Text = string.Empty;

            //txtDelevery.Text = string.Empty ;
            //txtTolerance.Text = string.Empty ;
            //txtTaxAgainst.Text =string.Empty;
            txtBillTo.Text = string.Empty;
            txtDeleveredTo.Text = string.Empty;

            BindOrderNo();
            Session["dtDicRate"] = null;
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void bindSTGrid()
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }
            GridSpinningThread.DataSource = dtOrderST;
            GridSpinningThread.DataBind();
            FinalTotal = 0;
            foreach (GridViewRow row in GridSpinningThread.Rows)
            {
                Label txtTotalCost = (Label)row.FindControl("txtTotalCost");
                FinalTotal = FinalTotal + double.Parse(txtTotalCost.Text.Trim());
            }
        }
        catch
        {
            throw;
        }
    }

    private void BlankSTControls()
    {
        try
        {
            cmbArticleNo.SelectedIndex = -1;
            cmbArticleNo.Enabled = true;
            txtItemCodeLabel.Text = string.Empty;
            // cmbShade.SelectedIndex = -1;
            ddlYarnShade.SelectedValue = "GREIGE";
            txtShadeFamilyName.Text = ddlYarnShade.SelectedValue;
            txtShadeName.Text = ddlYarnShade.SelectedValue;
            txtMatchingReff.SelectedIndex = -1;
            txtEndUse.SelectedIndex = -1;
            txtRemarks.Text = string.Empty;
            txtNoofUnit.Text = string.Empty;
            txtReqDate.Text = string.Empty;
            txtSaleRate.Text = string.Empty;
            txtTotalCost.Text = string.Empty;
            txtShadeName.Text = string.Empty;
            ddlShadeCode.Text = string.Empty;
            txtfinal.Text = string.Empty;
            txtCarryQty.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;


        }
        catch
        {

            throw;
        }
    }

    private void EnablePrimaryFields()
    {
        try
        {
            ddlOrderType.Enabled = true;
            ddlBusinessType.Enabled = true;

        }
        catch
        {

            throw;
        }
    }

    private void BindOrderNo()
    {
        try
        {
            string CRLocationPrefix = string.Empty;
            string CRType = string.Empty;
            string ORDER_CAT = string.Empty;
            string msg = string.Empty;
            FinalTotal = 0;
            CRLocationPrefix = oUserLoginDetail.SEQ_PREFIX.ToString();
            CRType = ddlOrderType.SelectedItem.ToString();
            ORDER_CAT = ddlBusinessType.SelectedItem.ToString();
            string OrderNo = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetNewSTOrderNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, CRLocationPrefix, CRType, ORDER_CAT);
            txtOrderNo.Text = OrderNo;
        }
        catch
        {
            throw;
        }
    }

    private void ClearMainData()
    {
        try
        {
            txtDate.Text = System.DateTime.Now.ToShortDateString();
            ddlBusinessType.SelectedIndex = -1;
            ddlOrderType.SelectedIndex = -1;
            txtDirectBilling.Text = string.Empty;
            TxtDocumentDate.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            cmbPartyCode.SelectedIndex = -1;
            txtPartyCode.Text = "";
            txtCustomerReffNo.Text = string.Empty;
            txtMstRemarks.Text = string.Empty;
            txtAgent.Text = string.Empty;
            txtPreCarriage.Text = string.Empty;
            txtPlaceToReceipt.Text = string.Empty;
            txtPortofLoading.Text = string.Empty;
            txtPortOfDischarge.Text = string.Empty;
            txtNoOfContainer.Text = string.Empty;
            txtnoofpackages.Text = string.Empty;
            txtLCNo.Text = string.Empty;
            txtLCDate.Text = string.Empty;
            txtvesselNo.Text = string.Empty;
            TextBox1.Text = string.Empty;
            txtpackages.Text = string.Empty;
            txtDestination.Text = string.Empty;
            TxtofOG.Text = string.Empty;
            TxtofFD.Text = string.Empty;

            //ddlBillingMode.SelectedIndex = -1;
            //ddlBillingMode.SelectedIndex = 0;



            //txtConversionRate.Text = "1";
            //ddlCurrencyCode.SelectedIndex = ddlCurrencyCode.Items.IndexOf(ddlCurrencyCode.Items.FindByValue("Rs."));
            //txtShipment.Text = "";
            //txtPaymentTerm.Text = "";
            //txtBillTo.Text = "";
            //ddlPaymentMode.SelectedIndex = -1;

        }
        catch (Exception)
        {

            throw;
        }
    }

    private void ActivateSaveMode()
    {
        try
        {
            lblMode.Text = "You are in Save Mode";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void Bind_CaseBox()
    {
        //throw new NotImplementedException();
    }

    private void bindDeliveryMode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("DELIVERY_MODE", oUserLoginDetail.COMP_CODE);
            ddlDeliveryMode.DataSource = dt;
            ddlDeliveryMode.DataValueField = "MST_CODE";
            ddlDeliveryMode.DataTextField = "MST_DESC";
            ddlDeliveryMode.DataBind();
            ddlDeliveryMode.Items.Insert(0, "Select");
        }
        catch
        {
            throw;
        }
    }

    private void bindOrderType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string sType = dr["MST_CODE"].ToString();
                    if (string.Compare(sType, "Development", true) == 0)
                    {
                        dt.Rows.Remove(dr);
                        dt.AcceptChanges();
                        break;
                    }
                }
            }
            ddlOrderType.DataSource = dt;
            ddlOrderType.DataValueField = "MST_CODE";
            ddlOrderType.DataTextField = "MST_DESC";
            ddlOrderType.DataBind();

        }
        catch
        {
            throw;
        }
    }

    private void bindEndUse()
    {
        try
        {
            txtEndUse.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("END_USE", oUserLoginDetail.COMP_CODE);
            txtEndUse.DataSource = dt;
            txtEndUse.DataValueField = "MST_CODE";
            txtEndUse.DataTextField = "MST_DESC";
            txtEndUse.DataBind();
            txtEndUse.Items.Insert(0, "Select");
        }
        catch
        {
            throw;
        }
    }

    private void bindBusinessType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);
            ddlBusinessType.Items.Clear();
            ddlBusinessType.DataSource = dt;
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void Bind_BillingMode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BILLING_MODE", oUserLoginDetail.COMP_CODE);
            ddlBillingMode.Items.Clear();
            ddlBillingMode.DataSource = dt;
            ddlBillingMode.DataValueField = "MST_CODE";
            ddlBillingMode.DataTextField = "MST_DESC";
            ddlBillingMode.DataBind();
            //ddlBillingMode.Items.Insert(0, "SELECT");
            ddlBillingMode.SelectedIndex = 1;

        }
        catch
        {

            throw;
        }
    }
    protected void ddlBusinessType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Bisiness Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    void PartyCodeLOV1_OnTextChanged(string Val, string Text)
    {
        try
        {
            txtAddress.Text = Val;
            txtPartyCode.Text = Text;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Code Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchArticalCodeInGrid(string ArticalCode, string TKTNo, string Shade, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            if (GridSpinningThread.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in GridSpinningThread.Rows)
                {
                    Label txtArticleNo = (Label)grdRow.FindControl("txtArticleNo");
                    //Label txtTktNo = (Label)grdRow.FindControl("txtTktNo");
                    Label txtShade = (Label)grdRow.FindControl("txtShade");
                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUNIQUE_ID = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (txtArticleNo.Text.Trim() == ArticalCode && txtShade.Text.Trim() == Shade && UNIQUE_ID != iUNIQUE_ID)
                    {
                        Result = true;
                    }
                }
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void btnSTSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }
            if (dtOrderST == null)
            {
                CreateSTDataTable();
            }
            var dtRateCompo = (DataTable)Session["dtDicRate"];
            string msg = string.Empty;
            if (ValidateSTRow(out msg))
            {
                if (dtOrderST.Rows.Count < 15)
                {
                    int UniqueId = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                        UniqueId = int.Parse(ViewState["UNIQUE_ID"].ToString());
                    string ArticalCode = txtItemCodeLabel.Text.ToString();
                    bool bb = SearchArticalCodeInGrid(ArticalCode, "", txtShadeName.Text, UniqueId);
                    if (!bb)
                    {
                        double qty = 0;
                        double.TryParse(txtNoofUnit.Text.Trim(), out qty);
                        double salerate = 0;
                        double.TryParse(txtSaleRate.Text, out salerate);
                        double TotalCost = 0;
                        double.TryParse(txtTotalCost.Text.Trim(), out TotalCost);
                        if (qty < 1)
                        {
                            Common.CommonFuction.ShowMessage(@"Quantity can not be zero or empty.");
                            return;
                        }
                        if (salerate < 1)
                        {
                            Common.CommonFuction.ShowMessage(@"Sale Rate can not be zero or empty.");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtReqDate.Text))
                        {
                            Common.CommonFuction.ShowMessage(@"Please select Required Date.");
                            return;
                        }

                        if (qty > 0)
                        {

                            if (UniqueId > 0)
                            {
                                DataView dv = new DataView(dtOrderST);
                                dv.RowFilter = "UNIQUE_ID =" + UniqueId;
                                if (dv.Count > 0)
                                {
                                    dv[0]["ORDER_NO"] = txtOrderNo.Text.Trim();
                                    dv[0]["ARTICLE_NO"] = txtItemCodeLabel.Text.ToString();
                                    dv[0]["TKT_NO"] = string.Empty;
                                    dv[0]["SHADE_FAMILY_CODE"] = txtShadeFamilyName.Text.Trim();
                                    dv[0]["SHADE_FAMILY_NAME"] = txtShadeFamilyName.Text.Trim();
                                    dv[0]["SHADE_CODE"] = txtShadeName.Text.Trim();
                                    dv[0]["SHADE_NAME"] = txtShadeName.Text.Trim();
                                    dv[0]["MATCHING_REFF"] = txtMatchingReff.SelectedItem.Trim();
                                    dv[0]["MATCHING_REFF_NAME"] = txtMatchingReff.SelectedItem.Trim();
                                    dv[0]["QUANTITY"] = qty;
                                    dv[0]["CR_UNIT"] = "KG.";
                                    dv[0]["END_USE"] = txtEndUse.SelectedValue.Trim();
                                    dv[0]["FINAL_RATE"] = double.Parse(txtfinal.Text.Trim());
                                    dv[0]["TOTAL_COST"] = TotalCost;
                                    dv[0]["END_USE_NAME"] = txtEndUse.SelectedItem.Text.Trim();
                                    dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                                    dv[0]["MAKE"] = string.Empty;
                                    dv[0]["NO_OF_UNIT"] = qty.ToString();
                                    dv[0]["WEIGHT_OF_UNIT"] = 1;
                                    dv[0]["REQ_DATE"] = DateTime.Parse(txtReqDate.Text);
                                    dv[0]["SALE_RATE"] = salerate;
                                    dv[0]["CARRY_QTY"] = txtCarryQty.Text;
                                    FinalTotal = FinalTotal + salerate * double.Parse(txtNoofUnit.Text.Trim());
                                }
                                dtOrderST.AcceptChanges();
                            }
                            else
                            {
                                DataRow dr = dtOrderST.NewRow();
                                dr["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                                dr["ORDER_NO"] = txtOrderNo.Text.Trim();
                                dr["ARTICLE_NO"] = txtItemCodeLabel.Text.ToString();
                                dr["TKT_NO"] = string.Empty;
                                dr["SHADE_FAMILY_CODE"] = txtShadeFamilyName.Text.Trim();
                                dr["SHADE_FAMILY_NAME"] = txtShadeFamilyName.Text.Trim();
                                dr["SHADE_CODE"] = txtShadeName.Text.Trim();
                                dr["SHADE_NAME"] = txtShadeName.Text.Trim();
                                dr["MATCHING_REFF"] = txtMatchingReff.SelectedItem.Trim();
                                dr["MATCHING_REFF_NAME"] = txtMatchingReff.SelectedItem.Trim();
                                dr["QUANTITY"] = qty;
                                dr["CR_UNIT"] = "KG.";
                                dr["END_USE"] = txtEndUse.SelectedValue.Trim();
                                dr["END_USE_NAME"] = txtEndUse.SelectedItem.Text.Trim();
                                dr["FINAL_RATE"] = double.Parse(txtfinal.Text.Trim());
                                dr["TOTAL_COST"] = TotalCost;
                                dr["REMARKS"] = txtRemarks.Text.Trim();
                                dr["MAKE"] = string.Empty;
                                dr["NO_OF_UNIT"] = qty.ToString();
                                dr["WEIGHT_OF_UNIT"] = 1;
                                dr["REQ_DATE"] = DateTime.Parse(txtReqDate.Text);
                                dr["SALE_RATE"] = salerate;
                                dr["CARRY_QTY"] = txtCarryQty.Text;
                                FinalTotal = FinalTotal + salerate * double.Parse(txtNoofUnit.Text.Trim());
                                dtOrderST.Rows.Add(dr);
                            }
                            BlankSTControls();
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage(@"Quantity can not be zero");
                            return;
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(@"Please Select Diffrent Artical Code !! Article Code|TktNo | Shade Should be Diffrent ");
                        return;
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage(@"Maximum Limit Reached");
                    return;
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
            ViewState["dtOrderST"] = dtOrderST;
            bindSTGrid();


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Save Button.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    private bool ValidateSTRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;
            int count = 0;
            int countAll = 0;
            int msgCount = 1;

            countAll += 1;
            if (txtItemCodeLabel.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Article No. ";

            }

           
            //if (txtShadeName.Text != string.Empty)
            //{
            //    count += 1;
            //}
            //else
            //{
            //    msg = msg + msgCount.ToString() + ": Please select Shade Code. ";

            //}

            countAll += 1;
            if (txtMatchingReff.SelectedValue != "SELECT")
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Matching Reff. ";

            }

            countAll += 1;
            double dd = 0;
            double.TryParse(txtNoofUnit.Text.Trim(), out dd);
            if (dd > 0)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Quantity/Validy Quantity. ";
                txtNoofUnit.Text = string.Empty;
            }
           
            //if (txtEndUse.SelectedIndex > 0)
            //{
            //    count += 1;
            //}
            //else
            //{
            //    msg = msg + msgCount.ToString() + ": Please select End Use. ";
            //    msgCount += 1;
            //}
            if (countAll == count)
            {
                result = true;
            }
            return result;
        }
        catch
        {

            throw;
        }
    }

    private void CreateSTDataTable()
    {
        try
        {
            dtOrderST = new DataTable();
            dtOrderST.Columns.Add("UNIQUE_ID", typeof(int));
            dtOrderST.Columns.Add("ORDER_NO", typeof(string));
            dtOrderST.Columns.Add("ARTICLE_NO", typeof(string));
            dtOrderST.Columns.Add("TKT_NO", typeof(string));
            dtOrderST.Columns.Add("SHADE_FAMILY_CODE", typeof(string));
            dtOrderST.Columns.Add("SHADE_FAMILY_NAME", typeof(string));
            dtOrderST.Columns.Add("SHADE_CODE", typeof(string));
            dtOrderST.Columns.Add("SHADE_NAME", typeof(string));
            dtOrderST.Columns.Add("MATCHING_REFF", typeof(string));
            dtOrderST.Columns.Add("MATCHING_REFF_NAME", typeof(string));
            dtOrderST.Columns.Add("QUANTITY", typeof(double));
            dtOrderST.Columns.Add("CR_UNIT", typeof(string));
            dtOrderST.Columns.Add("END_USE", typeof(string));
            dtOrderST.Columns.Add("END_USE_NAME", typeof(string));
            dtOrderST.Columns.Add("REMARKS", typeof(string));
            dtOrderST.Columns.Add("MAKE", typeof(string));
            dtOrderST.Columns.Add("NO_OF_UNIT", typeof(double));
            dtOrderST.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtOrderST.Columns.Add("REQ_DATE", typeof(DateTime));
            dtOrderST.Columns.Add("SALE_RATE", typeof(double));
            dtOrderST.Columns.Add("TOTAL_COST", typeof(double));
            dtOrderST.Columns.Add("FINAL_RATE", typeof(double));
            dtOrderST.Columns.Add("CARRY_QTY", typeof(string));

        }
        catch
        {

            throw;
        }
    }
    protected void btnSTCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BlankSTControls();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }
    protected void GridSpinningThread_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditSTItem(UniqueId);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteSTItem(UniqueId);
                bindSTGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row Command.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected void txtfinal_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateAmount();
            txtNoofUnit.ReadOnly = false;
            // txtBasicRate.ReadOnly = false;
            txtSaleRate.ReadOnly = false;
            txtTotalCost.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Final Rate Text Changed event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void CalculateAmount()
    {
        try
        {
            double Unit = 0;
            //  double Rate = 0;
            double fRate = 0;
            double Amount = 0;
            double.TryParse(CommonFuction.funFixQuotes(txtNoofUnit.Text.Trim()), out Unit);
            // double.TryParse(CommonFuction.funFixQuotes(txtBasicRate.Text.Trim()), out Rate);
            double.TryParse(CommonFuction.funFixQuotes(txtfinal.Text.Trim()), out fRate);
            double.TryParse(CommonFuction.funFixQuotes(txtTotalCost.Text.Trim()), out Amount);

            Amount = fRate * Unit;

            txtTotalCost.Text = Amount.ToString();
            // txtBasicRate.Text = Rate.ToString();
            txtfinal.Text = fRate.ToString();
            //txtQTY.Text = Qty.ToString();
        }
        catch
        {
            throw;
        }
    }
    private void deleteSTItem(int UniqueId)
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }
            if (dtOrderST.Rows.Count == 1)
                dtOrderST.Rows.Clear();
            else
            {
                foreach (DataRow dr in dtOrderST.Rows)
                {
                    int iUniqueId = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtOrderST.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtOrderST.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
                ViewState["dtOrderST"] = dtOrderST;

            }
            BlankSTControls();
        }
        catch
        {

            throw;
        }
    }

    private void EditSTItem(int UniqueId)
    {
        try
        {

            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }
            DataView dv = new DataView(dtOrderST);
            dv.RowFilter = "UNIQUE_ID = " + UniqueId;
            if (dv.Count > 0)
            {
                cmbArticleNo.Enabled = false;
                txtItemCodeLabel.Text = dv[0]["ARTICLE_NO"].ToString();
                txtShadeFamilyName.Text = dv[0]["SHADE_FAMILY_CODE"].ToString();
                txtShadeName.Text = dv[0]["SHADE_CODE"].ToString();
                txtMatchingReff.SetIndexByText(dv[0]["MATCHING_REFF"].ToString());
                txtNoofUnit.Text = dv[0]["QUANTITY"].ToString();
                txtEndUse.SelectedIndex = txtEndUse.Items.IndexOf(txtEndUse.Items.FindByValue(dv[0]["END_USE"].ToString()));
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtReqDate.Text = dv[0]["REQ_DATE"].ToString();
                txtSaleRate.Text = dv[0]["SALE_RATE"].ToString();
                txtfinal.Text = dv[0]["FINAL_RATE"].ToString();
                txtTotalCost.Text = dv[0]["TOTAL_COST"].ToString();
                txtCarryQty.Text = dv[0]["CARRY_QTY"].ToString();
                ViewState["UNIQUE_ID"] = UniqueId;
                GetTaxAdjByPO(ORDER_NO);
            }
        }
        catch
        {

            throw;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }
    protected void btnOther_Click(object sender, EventArgs e)
    {
        try
        {
            trOther.Visible = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Opening Other Detail POPUp..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void Insertdata()
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }
            bool bResult = false;
            string msg = string.Empty;
            if (ValidateSTMasterRow(out msg))
            {
                if (dtOrderST != null && dtOrderST.Rows.Count > 0)
                {
                    SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();
                    oOD_CUSTOMER_RQST_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oOD_CUSTOMER_RQST_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oOD_CUSTOMER_RQST_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oOD_CUSTOMER_RQST_MST.ORDER_NO = txtOrderNo.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.ORDER_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                    oOD_CUSTOMER_RQST_MST.ORDER_REFF_NO = txtCustomerReffNo.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.ORDER_CAT = txtCrLocation.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
                    oOD_CUSTOMER_RQST_MST.ADDRESS = txtAddress.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = txtPartyCode.Text.ToString();
                    oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.DOCUMENT_DATE = DateTime.Parse(TxtDocumentDate.Text.Trim().ToString());
                    oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.TUSER = oUserLoginDetail.UserCode;
                    oOD_CUSTOMER_RQST_MST.REMARKS = txtMstRemarks.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.BILLING_MODE = ddlBillingMode.SelectedItem.ToString();
                    oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();



                    oOD_CUSTOMER_RQST_MST.CURRENCY_CODE = ddlCurrencyCode.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.CONV_RATE = double.Parse(txtConversionRate.Text);
                    oOD_CUSTOMER_RQST_MST.SHIPMENT = txtShipment.Text;
                    oOD_CUSTOMER_RQST_MST.PAYMENT_MODE = ddlPaymentMode.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.PAYMENT_TERMS = txtPaymentTerm.Text;
                    oOD_CUSTOMER_RQST_MST.BILL_TO = txtBillTo.Text;
                    if (txtPreCarriage.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.PRE_CARRIAGE_BY = txtPreCarriage.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.PRE_CARRIAGE_BY = "NA";

                    if (txtPlaceToReceipt.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.PLC_TO_RECEIPT = txtPlaceToReceipt.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.PLC_TO_RECEIPT = "NA";

                    if (txtPortofLoading.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.PORT_OF_LOADING = txtPortofLoading.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.PORT_OF_LOADING = "NA";


                    if (txtPortOfDischarge.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.PORT_OF_DISCHARGE = txtPortOfDischarge.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.PORT_OF_DISCHARGE = "NA";

                    if (txtNoOfContainer.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = txtNoOfContainer.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = "NA";

                    if (txtnoofpackages.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = txtnoofpackages.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = "NA";
                    if (txtLCNo.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.SC_NO = txtLCNo.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.SC_NO = "NA";

                    if (txtLCDate.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.SC_DATE = DateTime.Parse(txtLCDate.Text.Trim());
                    else
                        oOD_CUSTOMER_RQST_MST.SC_DATE = System.DateTime.Now;
                    oOD_CUSTOMER_RQST_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtvesselNo.Text.Trim());

                    if (txtDestination.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.DESTINATION = txtDestination.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.DESTINATION = "NA";

                    if (TextBox1.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.BUY_DATE = DateTime.Parse(TextBox1.Text.Trim());
                    else
                        oOD_CUSTOMER_RQST_MST.BUY_DATE = System.DateTime.Now;

                    if (txtnoofpackages.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.EXP_REF = txtnoofpackages.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.EXP_REF = "NA";

                    if (TxtofOG.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.COUNTRY_ORIGIN = TxtofOG.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.COUNTRY_ORIGIN = "NA";
                    if (TxtofFD.Text != string.Empty)
                        oOD_CUSTOMER_RQST_MST.COUNTRY_FINAL = TxtofFD.Text.Trim();
                    else
                        oOD_CUSTOMER_RQST_MST.COUNTRY_FINAL = "NA";
                    var dtRateCompo = (DataTable)Session["dtDicRate"];

                    oOD_CUSTOMER_RQST_MST.DELEVERY = txtDelevery.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.TOLERANCE = txtTolerance.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.AGAINST_FORM = txtTaxAgainst.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.BILL_TO = txtBillTo.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.DELEVERED_TO = txtDeleveredTo.Text.Trim();
                    int pack = 0;
                    int.TryParse(txtpackages.Text, out pack);
                    oOD_CUSTOMER_RQST_MST.NO_OF_PACKAGE = pack;
                    DataTable DTA= new DataTable ();
                    bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.InsertCustomerRequestForYarn(oOD_CUSTOMER_RQST_MST, dtOrderST, dtRateCompo, PO_NUMB, PO_TYPE,DTA);

                    if (bResult == true)
                    {
                        string Resultmsg = "Customer Request Saved Successfully" + "\\r\\n";
                        Resultmsg += "Customer Request No is:" + oOD_CUSTOMER_RQST_MST.ORDER_NO;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                        InitialiseData();
                        if (dtOrderST != null)
                        {
                            dtOrderST.Rows.Clear();
                        }
                        bindSTGrid();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved :Enter atleast 1 Recipe Colour Details');", true);
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }


        }



        catch (Exception)
        {

            throw;
        }
    }
    protected void btncncelpack_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                trOther.Visible = false;
                // mpepacking.Show();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel Button..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private bool ValidateSTMasterRow(out string msg)
    {

        try
        {
            msg = string.Empty;
            bool result = false;

            int count = 0;
            int msgCount = 1;
            if (ddlBusinessType.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select cmbBusiness Type. ";
                msgCount += 1;
            }

            if (ddlOrderType.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Order Type. ";
                msgCount += 1;
            }
            if (txtPartyCode.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Customer Name. ";
                msgCount += 1;
            }
            if (txtAddress.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Address. ";
                msgCount += 1;
            }
            if (txtOrderNo.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please Create Order No. ";
                msgCount += 1;
            }
            if (count == 5)
                result = true;

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
            cmbOrderNo.SelectedIndex = -1;
            cmbOrderNo.Visible = true;
            txtOrderNo.Visible = false;
            lblMode.Text = "You are in Update Mode";
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            DiablePrimaryFields();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void DiablePrimaryFields()
    {
        try
        {
            ddlOrderType.Enabled = false;
            ddlBusinessType.Enabled = false;
        }
        catch
        {

            throw;
        }
    }
    protected void cmbOrderNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetCRItems(e.Text.ToUpper(), e.ItemsOffset);

            if (data != null && data.Rows.Count > 0)
            {
                cmbOrderNo.Items.Clear();
                cmbOrderNo.DataSource = data;
                cmbOrderNo.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);

        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetCRItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT ORDER_NO, ORDER_TYPE, ORDER_CAT,ORDER_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE|| '@'|| CURRENCY_CODE|| '@' || CONV_RATE|| '@'|| BILL_TO || '@'|| SHIPMENT || '@'|| PAYMENT_MODE || '@' || PAYMENT_TERMS || '@' || DOCUMENT_DATE || '@'|| AGENT || '@'|| PRTY_CODE || '@'|| PRTY_NAME || '@'|| LORY_NUMB  ||'@'|| PRE_CARRIAGE_BY ||'@'|| PLC_TO_RECEIPT ||'@'|| PORT_OF_LOADING || '@' || PORT_OF_DISCHARGE || '@' || NO_OF_CONTAINER|| '@' || SC_NO || '@' ||SC_DATE || '@' || NO_OF_PACKAGE || '@' || DESTINATION || '@' || COUNTRY_ORIGIN || '@'|| COUNTRY_FINAL || '@'|| BUY_DATE|| '@'|| EXP_REF) AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE DEL_STATUS = '0' AND CONF_FLAG = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE <> 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE ORDER_NO LIKE :SearchQuery OR ORDER_CAT LIKE :SearchQuery OR ORDER_TYPE LIKE :SearchQuery ORDER BY ORDER_NO) www WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND Combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT ORDER_NO, ORDER_TYPE, ORDER_CAT,ORDER_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE|| '@' || CURRENCY_CODE|| '@' || CONV_RATE|| '@'|| BILL_TO || '@' || SHIPMENT || '@' || PAYMENT_MODE || '@' || PAYMENT_TERMS || '@' || DOCUMENT_DATE || '@' || AGENT || '@' || PRTY_CODE || '@' || PRTY_NAME || '@' || LORY_NUMB  || PRE_CARRIAGE_BY || '@' || PLC_TO_RECEIPT || '@' || PORT_OF_LOADING || '@ || PORT_OF_DISCHARGE || '@' || NO_OF_CONTAINER|| '@' || SC_NO || '@' || ||SC_DATE || '@' ||NO_OF_PACKAGE || '@' || DESTINATION || '@' || COUNTRY_ORIGIN || '@'|| COUNTRY_FINAL || '@'|| BUY_DATE|| '@'|| EXP_REF) AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE ='" + PRODUCT_TYPE + "' AND ORDER_TYPE <>'DEVELOPMENT' AND comp_code ='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE ORDER_NO LIKE :SearchQuery OR ORDER_CAT LIKE :SearchQuery OR ORDER_TYPE LIKE :SearchQuery ORDER BY ORDER_NO) www WHERE ROWNUM <= '" + startOffset + "') ";
            }

            string SortExpression = " ORDER BY ORDER_NO";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {

            throw;
        }
    }

    private int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE, YARN_CAT,(TKTNO || '@' || MAKE || '@' || ENDUSE || '@' || YARN_CODE || '@' || UNITWT || '@' || NET_BOX_WT || '@' || NET_CART_WT || '@'  || UOM )  AS Combined   FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE  YARN_CAT = 'YARN' ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY YARN_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    private void BindControls(DataTable dt)
    {
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                txtOrderNo.Text = dt.Rows[0]["ORDER_NO"].ToString();
                txtDate.Text = dt.Rows[0]["ORDER_DATE"].ToString();
                //   ddlOrderCategory.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();
                txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString();
                txtCustomerReffNo.Text = dt.Rows[0]["ORDER_REFF_NO"].ToString();
                txtAddress.Text = cmbPartyCode.SelectedText.Trim();
                ddlOrderType.SelectedValue = dt.Rows[0]["ORDER_TYPE"].ToString();
                //   ddlOrderCategory.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();
                ddlBusinessType.SelectedValue = dt.Rows[0]["BUSINESS_TYPE"].ToString();
                ddlDeliveryMode.SelectedValue = dt.Rows[0]["DELIVERY_MODE"].ToString();
                TxtDocumentDate.Text = dt.Rows[0]["DOCUMENT_DATE"].ToString();
                txtDirectBilling.Text = dt.Rows[0]["DIRECT_BILLING"].ToString();
                txtMstRemarks.Text = dt.Rows[0]["REMARKS"].ToString();

                //*********** Added by nishant rai at 05-08-2013******************/

                ddlCurrencyCode.SelectedIndex = ddlCurrencyCode.Items.IndexOf(ddlCurrencyCode.Items.FindByValue(dt.Rows[0]["CURRENCY_CODE"].ToString().Trim()));
                txtConversionRate.Text = dt.Rows[0]["CONV_RATE"].ToString().Trim();
                txtShipment.Text = dt.Rows[0]["SHIPMENT"].ToString().Trim();
                ddlPaymentMode.SelectedIndex = ddlPaymentMode.Items.IndexOf(ddlPaymentMode.Items.FindByValue(dt.Rows[0]["PAYMENT_MODE"].ToString().Trim()));
                txtPaymentTerm.Text = dt.Rows[0]["PAYMENT_TERMS"].ToString().Trim();
                txtBillTo.Text = dt.Rows[0]["BILL_TO"].ToString().Trim();
                txtAgent.Text = dt.Rows[0]["AGENT"].ToString().Trim();
                txtAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString();
                txtPreCarriage.Text = dt.Rows[0]["PRE_CARRIAGE_BY"].ToString();
                txtPlaceToReceipt.Text = dt.Rows[0]["PLC_TO_RECEIPT"].ToString();
                txtPortofLoading.Text = dt.Rows[0]["PORT_OF_LOADING"].ToString();
                txtPortOfDischarge.Text = dt.Rows[0]["PORT_OF_DISCHARGE"].ToString();
                txtNoOfContainer.Text = dt.Rows[0]["NO_OF_CONTAINER"].ToString();
                txtnoofpackages.Text = dt.Rows[0]["EXP_REF"].ToString();
                txtLCNo.Text = dt.Rows[0]["SC_NO"].ToString();
                txtLCDate.Text = dt.Rows[0]["SC_DATE"].ToString();
                txtDestination.Text = dt.Rows[0]["DESTINATION"].ToString();
                TxtofOG.Text = dt.Rows[0]["COUNTRY_ORIGIN"].ToString();
                TxtofFD.Text = dt.Rows[0]["COUNTRY_FINAL"].ToString();
                TextBox1.Text = dt.Rows[0]["BUY_DATE"].ToString();
                txtvesselNo.Text = dt.Rows[0]["LORY_NUMB"].ToString();
                txtpackages.Text = dt.Rows[0]["NO_OF_PACKAGE"].ToString();


                txtDelevery.Text = dt.Rows[0]["DELEVERY"].ToString();
                txtTolerance.Text = dt.Rows[0]["TOLERANCE"].ToString();
                txtTaxAgainst.Text = dt.Rows[0]["AGAINST_FORM"].ToString();
                txtBillTo.Text = dt.Rows[0]["BILL_TO"].ToString();
                txtDeleveredTo.Text = dt.Rows[0]["DELEVERED_TO"].ToString();


                //*********** Added by nishant rai at 05-08-2013******************/
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapTableST(DataTable dtSpinningThread)
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            if (dtOrderST == null || dtOrderST.Rows.Count == 0)
            {
                CreateSTDataTable();
            }
            dtOrderST.Rows.Clear();
            if (dtSpinningThread != null && dtSpinningThread.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSpinningThread.Rows)
                {
                    DataRow drST = dtOrderST.NewRow();
                    drST["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                    drST["ORDER_NO"] = dr["ORDER_NO"];
                    drST["ARTICLE_NO"] = dr["ARTICLE_NO"];
                    drST["TKT_NO"] = dr["TKT_NO"];
                    //drST["SHADE_FAMILY_CODE"] = dr["SHADE_FAMILY_CODE"];
                    //drST["SHADE_CODE"] = dr["SHADE_CODE"];
                    //drST["SHADE_FAMILY_NAME"] = dr["SHADE_FAMILY_CODE"];
                    //drST["SHADE_NAME"] = dr["SHADE_CODE"];
                    drST["MATCHING_REFF"] = dr["MATCHING_REFF"];
                    drST["QUANTITY"] = dr["QUANTITY"];
                    drST["CR_UNIT"] = dr["CR_UNIT"];
                    //drST["END_USE"] = dr["END_USE"];
                    drST["REMARKS"] = dr["REMARKS"];
                    drST["MATCHING_REFF_NAME"] = dr["MATCHING_REFF"];
                    drST["END_USE_NAME"] = dr["END_USE_NAME"];
                    drST["TOTAL_COST"] = dr["TOTAL_COST"];
                    drST["MAKE"] = dr["MAKE"];
                    drST["NO_OF_UNIT"] = dr["NO_OF_UNIT"];
                    drST["WEIGHT_OF_UNIT"] = dr["WEIGHT_OF_UNIT"];
                    drST["REQ_DATE"] = dr["REQ_DATE"];
                    drST["SALE_RATE"] = dr["SALE_RATE"];
                    drST["FINAL_RATE"] = dr["FINAL_RATE"];
                    drST["CARRY_QTY"] = dr["CARRY_QTY"];
                  
                    FinalTotal = FinalTotal + double.Parse(dr["TOTAL_COST"].ToString());
                    dtOrderST.Rows.Add(drST);
                }
                dtSpinningThread = null;
                ViewState["dtOrderST"] = dtOrderST;
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindSTTranasaction()
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            GridSpinningThread.DataSource = dtOrderST;
            GridSpinningThread.DataBind();
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
            Updatedata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void Updatedata()
    {
        bool bResult = false;
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            if (dtOrderST != null && dtOrderST.Rows.Count > 0)
            {
                SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();
                oOD_CUSTOMER_RQST_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_CUSTOMER_RQST_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oOD_CUSTOMER_RQST_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oOD_CUSTOMER_RQST_MST.ORDER_NO = txtOrderNo.Text.Trim();
                oOD_CUSTOMER_RQST_MST.ORDER_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                oOD_CUSTOMER_RQST_MST.ORDER_REFF_NO = txtCustomerReffNo.Text.Trim();
                oOD_CUSTOMER_RQST_MST.ORDER_CAT = txtCrLocation.Text.Trim();
                oOD_CUSTOMER_RQST_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
                oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.ADDRESS = txtAddress.Text.Trim();
                oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = txtPartyCode.Text.ToString();
                oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.DOCUMENT_DATE = DateTime.Parse(TxtDocumentDate.Text.Trim().ToString());
                oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                oOD_CUSTOMER_RQST_MST.TUSER = oUserLoginDetail.UserCode;
                oOD_CUSTOMER_RQST_MST.REMARKS = txtMstRemarks.Text.Trim();
                oOD_CUSTOMER_RQST_MST.BILLING_MODE = ddlBillingMode.SelectedItem.ToString();
                oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();

                oOD_CUSTOMER_RQST_MST.CURRENCY_CODE = ddlCurrencyCode.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.CONV_RATE = double.Parse(txtConversionRate.Text);
                oOD_CUSTOMER_RQST_MST.SHIPMENT = txtShipment.Text;
                oOD_CUSTOMER_RQST_MST.PAYMENT_MODE = ddlPaymentMode.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.PAYMENT_TERMS = txtPaymentTerm.Text;
                oOD_CUSTOMER_RQST_MST.BILL_TO = txtBillTo.Text;
                if (txtPreCarriage.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.PRE_CARRIAGE_BY = txtPreCarriage.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.PRE_CARRIAGE_BY = "NA";

                if (txtPlaceToReceipt.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.PLC_TO_RECEIPT = txtPlaceToReceipt.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.PLC_TO_RECEIPT = "NA";

                if (txtPortofLoading.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.PORT_OF_LOADING = txtPortofLoading.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.PORT_OF_LOADING = "NA";


                if (txtPortOfDischarge.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.PORT_OF_DISCHARGE = txtPortOfDischarge.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.PORT_OF_DISCHARGE = "NA";

                if (txtNoOfContainer.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = txtNoOfContainer.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = "NA";

                if (txtnoofpackages.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = txtnoofpackages.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = "NA";
                if (txtLCNo.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.SC_NO = txtLCNo.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.SC_NO = "NA";

                if (txtLCDate.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.SC_DATE = DateTime.Parse(txtLCDate.Text.Trim());
                else
                    oOD_CUSTOMER_RQST_MST.SC_DATE = System.DateTime.Now;
                oOD_CUSTOMER_RQST_MST.LORY_NUMB = CommonFuction.funFixQuotes(txtvesselNo.Text.Trim());

                if (txtDestination.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.DESTINATION = txtDestination.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.DESTINATION = "NA";

                if (TextBox1.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.BUY_DATE = DateTime.Parse(TextBox1.Text.Trim());
                else
                    oOD_CUSTOMER_RQST_MST.BUY_DATE = System.DateTime.Now;

                if (txtnoofpackages.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.EXP_REF = txtnoofpackages.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.EXP_REF = "NA";

                if (TxtofOG.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.COUNTRY_ORIGIN = TxtofOG.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.COUNTRY_ORIGIN = "NA";
                if (TxtofFD.Text != string.Empty)
                    oOD_CUSTOMER_RQST_MST.COUNTRY_FINAL = TxtofFD.Text.Trim();
                else
                    oOD_CUSTOMER_RQST_MST.COUNTRY_FINAL = "NA";

                if (Request.QueryString["ISREVISED"] != null)
                {
                    oOD_CUSTOMER_RQST_MST.ISREVISED = "1";
                }
                else 
                {
                    oOD_CUSTOMER_RQST_MST.ISREVISED = "";
                }


                oOD_CUSTOMER_RQST_MST.DELEVERY = txtDelevery.Text.Trim();
                oOD_CUSTOMER_RQST_MST.TOLERANCE = txtTolerance.Text.Trim();
                oOD_CUSTOMER_RQST_MST.AGAINST_FORM = txtTaxAgainst.Text.Trim();
                oOD_CUSTOMER_RQST_MST.BILL_TO = txtBillTo.Text.Trim();
                oOD_CUSTOMER_RQST_MST.DELEVERED_TO = txtDeleveredTo.Text.Trim();

                int pack = 0;
                int.TryParse(txtpackages.Text, out pack);
                oOD_CUSTOMER_RQST_MST.NO_OF_PACKAGE = pack;

                var dtRateCompo = (DataTable)Session["dtDicRate"];
                DataTable DTA = new DataTable();
                bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.UpdateCustomerRequestYARN(oOD_CUSTOMER_RQST_MST, dtOrderST, dtRateCompo, PO_NUMB, PO_TYPE,DTA);

                if (bResult)
                {
                    string Resultmsg = "Customer Request Updated Successfully" + "\\r\\n";
                    Resultmsg += "Customer Request No is:" + oOD_CUSTOMER_RQST_MST.ORDER_NO;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                    InitialiseData();
                    // dtOrderST.Rows.Clear();
                    bindSTGrid();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialiseData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            Response.Redirect("~/Module/OrderDevelopment/CustomerRequest/Reports/SalesRequestForWaste.aspx?CR_NO=" + txtOrderNo.Text.Trim()+"&PRODUCT_TYPE="+PRODUCT_TYPE, true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Bisiness Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
        //try
        //{
        //    if (ddlOrderCategory.SelectedIndex != -1)
        //    {
        //        BindOrderNo();
        //    }
        //    else
        //    {
        //        Common.CommonFuction.ShowMessage("Please Select Request Order");
        //        txtOrderNo.Text = string.Empty;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Type Selection.\r\nSee error log for detail."));
        //    lblMode.Text = ex.ToString();
        //}
    }

    public void GETDATA()
    {

        try
        {
            DataTable dtMst = new DataTable();
            DataTable dtST = new DataTable();
            string COMP_CODE = Request.QueryString["COMP_CODE"].ToString();
            string BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
            string YEAR = Request.QueryString["YEAR"].ToString();
            string ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
            string ORDER_CAT = Request.QueryString["ORDER_CAT"].ToString();
            string PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            string ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
            string BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].ToString();      


            //*********** Added by nishant rai at 05-08-2013******************/

            SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();
            oOD_CUSTOMER_RQST_MST.COMP_CODE = COMP_CODE;
            oOD_CUSTOMER_RQST_MST.BRANCH_CODE = BRANCH_CODE;
            oOD_CUSTOMER_RQST_MST.YEAR = int.Parse(YEAR);
            oOD_CUSTOMER_RQST_MST.ORDER_TYPE = ORDER_TYPE;
            oOD_CUSTOMER_RQST_MST.ORDER_CAT = ORDER_CAT;
            oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CUSTOMER_RQST_MST.ORDER_NO = ORDER_NO;
            oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = BUSINESS_TYPE;
           
            dtMst = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectWasteCustomerRqstMst(oOD_CUSTOMER_RQST_MST);
            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                BindControls(dtMst);
                // dtFabric = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstFabric(OrderNumber);
                dtST = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstSTWaste(oOD_CUSTOMER_RQST_MST);
                if (dtST != null && dtST.Rows.Count > 0)
                {
                    MapTableST(dtST);
                    GetTaxAdjByPO(ORDER_NO);
                    BindSTTranasaction();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    
    }


    protected void cmbOrderNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dtMst = new DataTable();
            DataTable dtST = new DataTable();
            string cString = cmbOrderNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string COMP_CODE = arrString[0].ToString();
            string BRANCH_CODE = arrString[1].ToString();
            string YEAR = arrString[2].ToString();
            string ORDER_TYPE = arrString[3].ToString();
            string ORDER_CAT = arrString[4].ToString();
            string PRODUCT_TYPE = arrString[5].ToString();
            string ORDER_NO = arrString[6].ToString();
            string BUSINESS_TYPE = arrString[7].ToString();


            //*********** Added by nishant rai at 05-08-2013******************/
            string CURRENCY_CODE = arrString[8].ToString();
            string CONV_RATE = arrString[9].ToString();
            string BILL_TO = arrString[10].ToString();
            string SHIPMENT = arrString[11].ToString();
            string PAYMENT_MODE = arrString[12].ToString();
            string PAYMENT_TERMS = arrString[13].ToString();
            string DOCUMENT_DATE = arrString[14].ToString();
            string AGENT = arrString[15].ToString();
            string PRTY_CODE = arrString[16].ToString();
            string PRTY_NAME = arrString[17].ToString();
            string LORY_NUMB = arrString[18].ToString();
            string PRE_CARRIAGE_BY = arrString[19].ToString();
            string PLC_TO_RECEIPT = arrString[20].ToString();
            string PORT_OF_LOADING = arrString[21].ToString();
            string PORT_OF_DISCHARGE = arrString[22].ToString();
            string NO_OF_CONTAINER = arrString[23].ToString();
            string SC_NO = arrString[24].ToString();
            string SC_DATE = arrString[25].ToString();
            string NO_OF_PACKAGE = arrString[26].ToString();
            string DESTINATION = arrString[27].ToString();
            string COUNTRY_ORIGIN = arrString[28].ToString();
            string COUNTRY_FINAL = arrString[29].ToString();
            string BUY_DATE = arrString[30].ToString();
            string EXP_REF = arrString[31].ToString();


            //*********** Added by nishant rai at 05-08-2013******************/

            SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();
            oOD_CUSTOMER_RQST_MST.COMP_CODE = COMP_CODE;
            oOD_CUSTOMER_RQST_MST.BRANCH_CODE = BRANCH_CODE;
            oOD_CUSTOMER_RQST_MST.YEAR = int.Parse(YEAR);
            oOD_CUSTOMER_RQST_MST.ORDER_TYPE = ORDER_TYPE;
            oOD_CUSTOMER_RQST_MST.ORDER_CAT = ORDER_CAT;
            oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CUSTOMER_RQST_MST.ORDER_NO = ORDER_NO;
            oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = BUSINESS_TYPE;

            //*********** Added by nishant rai at 05-08-2013******************/
            oOD_CUSTOMER_RQST_MST.CURRENCY_CODE = CURRENCY_CODE;
            double convRate = 0;
            double.TryParse(CONV_RATE, out convRate);
            oOD_CUSTOMER_RQST_MST.CONV_RATE = convRate;
            oOD_CUSTOMER_RQST_MST.BILL_TO = BILL_TO;
            oOD_CUSTOMER_RQST_MST.SHIPMENT = SHIPMENT;
            oOD_CUSTOMER_RQST_MST.PAYMENT_MODE = PAYMENT_MODE;
            oOD_CUSTOMER_RQST_MST.PAYMENT_TERMS = PAYMENT_TERMS;
            DateTime DocumntDate = DateTime.Now;
            DateTime.TryParse(DOCUMENT_DATE, out DocumntDate);
            oOD_CUSTOMER_RQST_MST.DOCUMENT_DATE = DocumntDate;
            oOD_CUSTOMER_RQST_MST.AGENT = AGENT;
            oOD_CUSTOMER_RQST_MST.PRTY_CODE = PRTY_CODE;
            oOD_CUSTOMER_RQST_MST.PRTY_NAME = PRTY_NAME;
            oOD_CUSTOMER_RQST_MST.LORY_NUMB = LORY_NUMB;
            oOD_CUSTOMER_RQST_MST.PRE_CARRIAGE_BY = PRE_CARRIAGE_BY;
            oOD_CUSTOMER_RQST_MST.PLC_TO_RECEIPT = PLC_TO_RECEIPT;
            oOD_CUSTOMER_RQST_MST.PORT_OF_LOADING = PORT_OF_LOADING;
            oOD_CUSTOMER_RQST_MST.PORT_OF_DISCHARGE = PORT_OF_DISCHARGE;
            oOD_CUSTOMER_RQST_MST.NO_OF_CONTAINER = NO_OF_CONTAINER;
            oOD_CUSTOMER_RQST_MST.SC_NO = SC_NO;
            DateTime SCDate = DateTime.Now;
            DateTime.TryParse(SC_DATE, out SCDate);
            oOD_CUSTOMER_RQST_MST.SC_DATE = SCDate;
            int noofpckg = 0;
            int.TryParse(NO_OF_PACKAGE, out noofpckg);
            oOD_CUSTOMER_RQST_MST.NO_OF_PACKAGE = noofpckg;
            oOD_CUSTOMER_RQST_MST.DESTINATION = DESTINATION;
            oOD_CUSTOMER_RQST_MST.COUNTRY_ORIGIN = COUNTRY_ORIGIN;
            oOD_CUSTOMER_RQST_MST.COUNTRY_FINAL = COUNTRY_FINAL;
            DateTime Buydate = DateTime.Now;
            DateTime.TryParse(BUY_DATE, out Buydate);
            oOD_CUSTOMER_RQST_MST.BUY_DATE = Buydate;
            oOD_CUSTOMER_RQST_MST.EXP_REF = EXP_REF;

            //*********** Added by nishant rai at 05-08-2013******************/

            dtMst = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectWasteCustomerRqstMst(oOD_CUSTOMER_RQST_MST);
            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                BindControls(dtMst);
                // dtFabric = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstFabric(OrderNumber);
                dtST = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstSTWaste(oOD_CUSTOMER_RQST_MST);
                if (dtST != null && dtST.Rows.Count > 0)
                {
                    MapTableST(dtST);
                    GetTaxAdjByPO(ORDER_NO);
                    BindSTTranasaction();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }





    private void GetTaxAdjByPO(string ORDER_NO)
    {
        try
        {
            var oOD_CUSTOMER_REQUEST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();
            oOD_CUSTOMER_REQUEST_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CUSTOMER_REQUEST_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CUSTOMER_REQUEST_MST.PO_TYPE = PO_TYPE;
            oOD_CUSTOMER_REQUEST_MST.PO_NUMB = txtOrderNo.Text.Trim();
            var dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetTaxByPOOnly(oOD_CUSTOMER_REQUEST_MST);
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
    private DataTable CreateTaxDataTable()
    {
        try
        {
            var dtRate = new DataTable();
            dtRate.Columns.Add("UniqueId", typeof(int));
            dtRate.Columns.Add("YEAR", typeof(int));
            dtRate.Columns.Add("ITEM_CODE", typeof(string));
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
    private void MapTaxTable(DataTable temp, DataTable dtRate)
    {
        try
        {
            foreach (DataRow dr in temp.Rows)
            {
                var drNew = dtRate.NewRow();
                drNew["UniqueId"] = dtRate.Rows.Count + 1;
                drNew["YEAR"] = dr["YEAR"];
                drNew["ITEM_CODE"] = dr["ITEM_CODE"];
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
    protected void cmbArticleNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbArticleNo.Items.Clear();
                cmbArticleNo.DataSource = data;
                cmbArticleNo.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE,   ITEM_DESC,ITEM_TYPE,CAT_CODE,(   '' || '@'|| ITEM_MAKE|| '@'|| ''|| '@'|| ITEM_CODE|| '@'|| UOM || '@' || ''|| '@' || ''|| '@'|| UOM)  AS Combined FROM   TX_WASTE_MST      WHERE      ITEM_CODE LIKE :SearchQuery          OR CAT_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery    ORDER BY   ITEM_CODE) asd ";
            //AND ROWNUM <= 15 

            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND ITEM_CODE NOT IN (SELECT ITEM_CODE FROM (   SELECT   ITEM_CODE,   ITEM_DESC,ITEM_TYPE,CAT_CODE,(   '' || '@'|| ITEM_MAKE|| '@'|| ''|| '@'|| ITEM_CODE|| '@'|| UOM || '@' || ''|| '@' || ''|| '@'|| UOM)  AS Combined FROM   TX_WASTE_MST      WHERE      ITEM_CODE LIKE :SearchQuery          OR CAT_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery    ORDER BY   ITEM_CODE  ) asd   AND ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }
    protected void cmbArticleNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {

        try
        {
            string ArticleNo = cmbArticleNo.SelectedText.ToString();
            string[] arrString = cmbArticleNo.SelectedValue.Split('@');
            //txtTktNo.Text = arrString[0].ToString();
            //txtMake.Text = arrString[1].ToString();
            txtItemCodeLabel.Text = ArticleNo;
            txtEndUse.SelectedIndex = txtEndUse.Items.IndexOf(txtEndUse.Items.FindByValue(arrString[2].ToString()));
            //lblUNIT_WT.Text = arrString[4].ToString();
            //lblNETBOX_WT.Text = arrString[5].ToString();
            //lblNETCART_WT.Text = arrString[6].ToString();
            //DDLCaseBox.SelectedIndex = DDLCaseBox.Items.IndexOf(DDLCaseBox.Items.FindByValue(arrString[7].ToString()));

            BindDetailByCaseSelection();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article No Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindDetailByCaseSelection()
    {
        try
        {
            string ArticleCode = txtItemCodeLabel.Text.Trim();
            SaitexDM.Common.DataModel.TX_WASTE_MASTER oTX_WASTE_MASTER = new SaitexDM.Common.DataModel.TX_WASTE_MASTER();
            oTX_WASTE_MASTER.ITEM_CODE = CommonFuction.funFixQuotes(ArticleCode);
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetTX_WASTE_MASTERByItemCode(oTX_WASTE_MASTER);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                lblUNIT_WT.Text = dt.Rows[0]["UOM"].ToString();
                lblNETBOX_WT.Text = dt.Rows[0]["UOM"].ToString();
                lblNETCART_WT.Text = dt.Rows[0]["UOM"].ToString();
            }
        }
        catch
        {

            throw;
        }
    }
    protected void DDLCaseBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindDetailByCaseSelection();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Case Box Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtNoofUnit;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {

                    double salerate = 0f;
                    if (double.TryParse(CommonFuction.funFixQuotes(txtSaleRate.Text.Trim()), out salerate))
                    {
                        double Total = (salerate) * (double.Parse(RequestQTY.ToString()));
                        txtTotalCost.Text = Total.ToString();
                        FinalTotal = FinalTotal + Total;

                    }
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void txtWeightofUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //double WeightofUnit = 0;
            //double.TryParse(txtWeightofUnit.Text, out WeightofUnit);
            //double NoofUnit = 0;
            //double.TryParse(txtNoofUnit.Text, out NoofUnit);
            //txtQuantity.Text = (WeightofUnit * NoofUnit).ToString();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Weight Of Unit TextChanged.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                //cmbShade.Items.Clear();
                //cmbShade.DataSource = data;
                //cmbShade.DataBind();
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
    protected void btnDisc_Click(object sender, EventArgs e)
    {

        try
        {

            string URL = "~/../../../CustomerRequest/Pages/Cust_Adj_Tax_For_Waste.aspx";
            URL = URL + "?FinalAmount=" + txtSaleRate.Text.Trim();
            URL = URL + "&TextBoxId=" + txtfinal.ClientID;
            URL = URL + "&PO_COMP_CODE=" + PO_COMP_CODE;
            URL = URL + "&PO_BRANCH=" + PO_BRANCH;
            URL = URL + "&PO_TYPE=" + PO_TYPE;
            URL = URL + "&PO_NUMB=" + PO_NUMB;
            URL = URL + "&ITEM_CODE=" + txtItemCodeLabel.Text.Trim();
            URL = URL + "&SHADE_CODE=" + txtShadeName.Text.Trim();
            txtSaleRate.ReadOnly = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting discount/ taxes adjustment for transaction."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void cmbShade_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            //if (cmbShade.SelectedIndex > -1)
            //{
            //    string[] arrString = cmbShade.SelectedValue.Split('@');
            //    txtShadeFamilyName.Text = arrString[0].ToString();
            //    txtShadeFamilyName.Text = arrString[1].ToString();
            //    txtShadeName.Text = arrString[2].ToString();
            //    txtShadeName.Text = arrString[3].ToString();

            //    txtMatchingReff.LoadData(txtShadeFamilyName.Text.Trim(), txtShadeName.Text.Trim());
            //}
            //else
            //{
            //    Common.CommonFuction.ShowMessage("Please select Shade");
            //}
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
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
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
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }
    //protected int GetShadeItemsCount(string text)
    //{
    //    try
    //    {
    //        string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
    //        string WhereClause = " ";
    //        string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
    //        return data.Rows.Count;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void ddlBillingMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillingMode.SelectedItem.ToString() == "Branch Transfer")
        {
            txtPartyCode.Text = oUserLoginDetail.BRANCH_PRTYCODE;
            txtAddress.Text = oUserLoginDetail.BRANCH_PRTYNAMEADDRES;

        }
        else
        {
            txtPartyCode.Text = string.Empty;
            txtAddress.Text = string.Empty;
            //do nothing;
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;
            string whereClause = string.Empty;
            if (ddlBillingMode.SelectedItem.ToString() == "Branch Transfer")
            {
                CommandText = " SELECT   PRTY_CODE,PRTY_NAME, (PRTY_NAME || PRTY_ADD1|| ' ' ||PRTY_ADD2|| DECODE(PRTY_CITY,'','',', CITY- '||PRTY_CITY)||DECODE(PRTY_STATE,'','',' ,STATE- '||PRTY_STATE)|| DECODE(PIN_CODE,'','', ' ,PINCODE- ' ||PIN_CODE)||DECODE(COUNTRY,'','', ' ,COUNTY- '||COUNTRY)||DECODE(PRTY_TINNO,'','', ' ,TIN NO-'||PRTY_TINNO)) Address, PRTY_GRP_CODE,PARTY_CODE  FROM   (  SELECT   TV.PRTY_CODE, TV.PRTY_NAME,  TV.PRTY_ADD1,TV.PRTY_ADD2,TV.PRTY_CITY,  TV.PRTY_STATE,TV.PIN_CODE, TV.COUNTRY,TV.PRTY_TINNO, TV.PRTY_GRP_CODE, BR.PARTY_CODE  FROM   TX_VENDOR_MST TV, CM_BRANCH_MST BR       WHERE   TV.PRTY_CODE LIKE :SearchQuery       OR TV.PRTY_NAME LIKE :SearchQuery          ORDER BY   TV.PRTY_CODE ASC) asd WHERE   PRTY_CODE = PARTY_CODE AND ROWNUM <= 15";
                whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM  ( SELECT TV.PRTY_CODE, TV.PRTY_NAME, TV.PRTY_ADD1, TV.PRTY_GRP_CODE, BR.PARTY_CODE  FROM TX_VENDOR_MST TV, CM_BRANCH_MST BR  WHERE   TV.PRTY_CODE LIKE :SearchQuery OR TV.PRTY_NAME LIKE :SearchQuery  ORDER BY   TV.PRTY_CODE ASC) asd WHERE PRTY_CODE = PARTY_CODE AND ROWNUM <= " + startOffset + ")";
                }
            }
            else if (ddlBillingMode.SelectedItem.ToString() == "Direct Billing")
            {

                CommandText = "SELECT PRTY_CODE, PRTY_NAME, (   PRTY_NAME|| PRTY_ADD1|| ' ' || PRTY_ADD2 || DECODE (PRTY_CITY, '', '', ' ,CITY- ' || PRTY_CITY)     || DECODE (PRTY_STATE, '', '', ' ,STATE- ' || PRTY_STATE)  || DECODE (PIN_CODE, '', '', ' ,PINCODE- ' || PIN_CODE)   || DECODE (COUNTRY, '', '', ' ,COUNTY- ' || COUNTRY)  || DECODE (PRTY_TINNO, '', '', ' ,TIN NO-' || PRTY_TINNO))       Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_ADD2,  PRTY_CITY,    PRTY_STATE,     PIN_CODE,     COUNTRY, PRTY_TINNO,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE) IN (upper('WASTE'),upper('INNOVATIVE')) and upper(VENDOR_CAT_CODE) IN (upper('Party'),upper('DEPOT')) and ROWNUM <= 15 ";
                whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE) IN (upper('WASTE'),upper('INNOVATIVE'))  and upper(VENDOR_CAT_CODE) IN (upper('Party'),upper('DEPOT'))  and ROWNUM <= " + startOffset + ")";
                }
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

    protected int GetPartyCount(string text)
    {

        string CommandText = string.Empty;
        string WhereClause = string.Empty;
        string SortExpression = string.Empty;
        string SearchQuery = string.Empty;
        DataTable data = null;
        if (ddlBillingMode.SelectedItem.ToString() == "Branch Transfer")
        {

            CommandText = " SELECT PRTY_CODE, PRTY_NAME,(PRTY_NAME || PRTY_ADD1) Address, PRTY_GRP_CODE, PARTY_CODE  FROM  ( SELECT TV.PRTY_CODE, TV.PRTY_NAME, TV.PRTY_ADD1, TV.PRTY_GRP_CODE, BR.PARTY_CODE  FROM TX_VENDOR_MST TV, CM_BRANCH_MST BR  WHERE   TV.PRTY_CODE LIKE :SearchQuery OR TV.PRTY_NAME LIKE :SearchQuery  ORDER BY   TV.PRTY_CODE ASC) asd WHERE   PRTY_CODE = PARTY_CODE ";
            WhereClause = " ";
            SortExpression = " ";
            SearchQuery = "%" + text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        else if (ddlBillingMode.SelectedItem.ToString() == "Direct Billing")
        {
            CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
            WhereClause = " ";
            SortExpression = " ";
            SearchQuery = text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        return data.Rows.Count;
    }

    protected void cmbPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);

            cmbPartyCode.Items.Clear();

            cmbPartyCode.DataSource = data;
            cmbPartyCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void cmbPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtAddress.Text = cmbPartyCode.SelectedValue.Trim();
            txtPartyCode.Text = cmbPartyCode.SelectedText.Trim();
            txtBillTo.Text = cmbPartyCode.SelectedValue.Trim();
            txtDeleveredTo.Text = cmbPartyCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    //protected int GetShadeItemsCount(string text)
    //{
    //    try
    //    {
    //        string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
    //        string WhereClause = " ";
    //        string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
    //        return data.Rows.Count;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}




    private void BindCurrency()
    {
        try
        {
            ddlCurrencyCode.Items.Clear();
            DataTable dtCurrencyCode = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("CURRENCY", oUserLoginDetail.COMP_CODE);

            ddlCurrencyCode.DataSource = dtCurrencyCode;
            ddlCurrencyCode.DataTextField = "MST_DESC";
            ddlCurrencyCode.DataValueField = "MST_CODE";
            ddlCurrencyCode.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtsalerate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtNoofUnit;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {

                    double salerate = 0f;
                    if (double.TryParse(CommonFuction.funFixQuotes(txtSaleRate.Text.Trim()), out salerate))
                    {
                        double Total = (salerate) * (double.Parse(RequestQTY.ToString()));
                        txtTotalCost.Text = Total.ToString();
                        FinalTotal = FinalTotal + Total;

                    }
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in No Of Unit TextChanged.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindYarnSHADE(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlYarnShade.Items.Clear();
                ddlYarnShade.DataSource = dt;
                ddlYarnShade.DataTextField = "MST_CODE";
                ddlYarnShade.DataValueField = "MST_CODE";
                ddlYarnShade.DataBind();
                //ddlYarnShade.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlYarnShade.SelectedValue = "GREIGE";
                ddlYarnShade.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlCurrencyCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Bisiness Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddlYarnShade_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtShadeFamilyName.Text = ddlYarnShade.SelectedValue;
        txtShadeName.Text = ddlYarnShade.SelectedValue;
    }
    protected void txtSaleRate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtTotalCost_TextChanged(object sender, EventArgs e)
    {

    }
}
