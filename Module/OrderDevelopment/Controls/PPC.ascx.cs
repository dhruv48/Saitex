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

public partial class Module_OrderDevelopment_Controls_PPC : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST;
    public static string PI_TYPE;
    private static string PRODUCT_TYPE;
    private static string sOrderNo = string.Empty;
    public string PRODUCTTYPE
    {
        get
        {
            return PRODUCT_TYPE;
        }
        set
        {
            PRODUCT_TYPE = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ddlOrderNo.PRODUCT_TYPE = PRODUCT_TYPE;
            txtTRNYarnSpiningArticalCode.PI_TYPE = PI_TYPE;
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"].ToString().Equals(string.Empty) == false)
                {
                    PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
                }
                DisableFieldsforPPC();
                ActivateUpdateMode();

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    protected override void OnInit(EventArgs e)
    {
        ddlParty.AutoPostBack = true;
        ddlParty.OnTextChanged += new CommonControls_PartyCodeLOV.RefreshDataGridView(ddlParty_OnTextChanged);
        base.OnInit(e);

        txtTRNYarnSpiningArticalCode.AutoPostBack = true;
        txtTRNYarnSpiningArticalCode.OnTextChanged += new CommonControls_LOV_CustReqArticleLOV.RefreshDataGridView(txtTRNYarnSpiningArticalCode_OnTextChanged);
        txtTRNYarnSpiningArticalCode.PI_TYPE = PI_TYPE;

        ddlOrderNo.AutoPostBack = true;
        ddlOrderNo.OnTextChanged += new CommonControls_LOV_OrderNoLOV.RefreshDataGridView(ddlOrderNo_OnTextChanged);
        ddlOrderNo.PRODUCT_TYPE = PRODUCT_TYPE;
    }

    void ddlOrderNo_OnTextChanged(string Value, string Text)
    {
        try
        {
            string ORDER_STRING = ddlOrderNo.SelectedValue.Trim();

            if (dtTRNYarnSpining == null || dtTRNYarnSpining.Rows.Count == 0)
                CreateTRNYarnSpiningTable();

            dtTRNYarnSpining.Rows.Clear();

            int iRecordFound = GetdataByOrderNumber(Common.CommonFuction.funFixQuotes(ORDER_STRING));

            if (iRecordFound > 0)
            {
                ActivateUpdateMode();
                txtOrderNo.Visible = true;
                ddlOrderNo.Visible = false;
            }
            else
            {
                ClearPage();
                lblMode.Text = "Update";
                txtOrderNo.Text = "";

                ActivateUpdateMode();

                string msg = "Dear " + oUserLoginDetail.Username + " !! Order already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }

    void txtTRNYarnSpiningArticalCode_OnTextChanged(string Value, string Text)
    {
        try
        {
            string sYarnData = txtTRNYarnSpiningArticalCode.SelectedValue.ToString();

            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

            string ARTICLE_CODE = string.Empty;
            string UOM = string.Empty;
            string Description = string.Empty;
            string Cust_Req_no = string.Empty;
            string TKT_NO = string.Empty;
            string SHADE = string.Empty;

            if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out Cust_Req_no, out TKT_NO, out SHADE);
            else
                ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

            bool bb = SearchTRNYarnSpiningArticalCodeInGrid(ARTICLE_CODE, UNIQUE_ID);
            if (!bb)
            {
                txtTRNYarnSpiningUOM.Text = UOM;
                lblTRNYSpinDesc.Text = Description;
                lblTRNYarnSpiningArticalCode.Text = ARTICLE_CODE;
                txtTRNYarnSpiningCReqNo.Text = Cust_Req_no;
                //For TKT NO 
                txtTRNYarnSpiningUOM.Text = UOM;
                //  txtTRNYarnSpiningOrderQty.Text = crQty.ToString();
                txtTRNYarnSpiningShade.SelectedIndex = txtTRNYarnSpiningShade.Items.IndexOf(txtTRNYarnSpiningShade.Items.FindByValue(SHADE));
                txtTRNYarnSpiningShade.Enabled = false;
            }
            else
            {
                lblTRNYarnSpiningArticalCode.Text = string.Empty;
                txtTRNYarnSpiningArticalCode.SelectedIndex = -1;
                txtTRNYarnSpiningShade.SelectedIndex = -1;
                lblTRNYSpinDesc.Text = string.Empty;
                txtTRNYarnSpiningCReqNo.Text = string.Empty;
                Common.CommonFuction.ShowMessage("This Yarn artical code already exists.");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Yarn artical code"));
        }
    }

    void ddlParty_OnTextChanged(string Value, string Text)
    {
        try
        {
            txtPartyDetail.Text = ddlParty.SelectedItem.Trim();

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party selection"));
        }
    }

    private void InitialisePage()
    {
        try
        {
            SetPageAccordingProductType();
            BindBusinessType();
            BindOrderProcess();
            BindOrderType();
            BindProductType();
            BindShadeType();
            BindProcessRoot();
            BindCurrency();
            BindFromBranch();
            ClearPage();

        }
        catch
        {
            throw;
        }
    }

    protected void SetPageAccordingProductType()
    {
        try
        {
            #region code to set pi type
            if (PRODUCT_TYPE == "YARN SPINING")
            {
                PI_TYPE = "YARN_SPINING";
            }
            else if (PRODUCT_TYPE == "YARN DYEING")
            {
                PI_TYPE = "YARN_DYEING";
            }
            else if (PRODUCT_TYPE == "SEWING THREAD")
            {
                PI_TYPE = "SEWING_THREAD";
            }
            else if (PRODUCT_TYPE == "GRAY WEAVING")
            {
                PI_TYPE = "GRAY_WEAV";
            }
            else if (PRODUCT_TYPE == "FABRIC PROCESSING")
            {
                PI_TYPE = "FABR_PROC";
            }

            txtTRNYarnSpiningArticalCode.PI_TYPE = PI_TYPE;
            ddlOrderNo.PRODUCT_TYPE = PRODUCT_TYPE;
            lblFormHeading.Text = PRODUCT_TYPE;
            #endregion
        }
        catch
        {
            throw;
        }
    }

    protected void ClearPage()
    {
        try
        {
            ClearMainData();
            ActivateSaveMode();
            BindOrderNo();

            RefreshTRNYarnSpiningRow();

            dtTRNYarnSpining = null;

            BindTRNYarnSpiningGrid();

        }
        catch
        {
            throw;
        }
    }

    protected void ClearMainData()
    {
        try
        {
            ddlBusinessType.SelectedIndex = -1;
            ddlCurrencyCode.SelectedIndex = -1;
            ddlOrderProcess.SelectedIndex = -1;
            ddlOrderType.SelectedIndex = -1;
            ddlParty.SelectedIndex = -1;
            ddlOrderCategory.SelectedIndex = -1;
            ddlPaymentMode.SelectedIndex = -1;
            ddlDeliveryMode.SelectedIndex = -1;
            //ddlProductType.SelectedIndex = -1;

            txtConversionRate.Text = "1";
            txtOrderDate.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtPartyDetail.Text = string.Empty;
            txtPartyRefDate.Text = string.Empty;
            txtPartyRefNumber.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtShipment.Text = string.Empty;
            txtconsigneeName.Text = string.Empty;
            txtConsigneeAdd.Text = string.Empty;
            txtBillTo.Text = string.Empty;
            txtPaymentTerm.Text = string.Empty;
            txtGenInstruction.Text = string.Empty;
            txtSplInstruction.Text = string.Empty;

            btnTRN_ADJ_BOM.Enabled = false;

            txtOrderDate.Text = System.DateTime.Now.Date.ToShortDateString();

            Session["dtTRN_DEL_SCHEDULE"] = null;

            Session["dtTRN_COST"] = null;

            Session["dtTRN_BOM"] = null;

            Session["dtBOMAdj"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void ActivateSaveMode()
    {
        try
        {
            lblMode.Text = "Save";
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;

            ddlOrderCategory.Enabled = true;
            ddlBusinessType.Enabled = true;
            ddlProductType.Enabled = false;
            ddlOrderType.Enabled = true;

            txtOrderNo.Text = string.Empty;
            txtOrderNo.Visible = true;

            ddlOrderNo.Visible = false;
            ddlOrderNo.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    private void BindBusinessType()
    {
        try
        {
            ddlBusinessType.Items.Clear();
            DataTable dtBusinessType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);

            ddlBusinessType.DataSource = dtBusinessType;
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void BindProductType()
    {
        try
        {
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);

            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();

            ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(PRODUCT_TYPE));
            ddlProductType.Text = PRODUCT_TYPE;
            ddlProductType.Enabled = false;
        }
        catch
        {
            throw;
        }
    }

    private void BindOrderType()
    {
        try
        {
            ddlOrderType.Items.Clear();
            DataTable dtOrderType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);

            ddlOrderType.DataSource = dtOrderType;
            ddlOrderType.DataTextField = "MST_DESC";
            ddlOrderType.DataValueField = "MST_CODE";
            ddlOrderType.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void BindOrderProcess()
    {
        try
        {
            ddlOrderProcess.Items.Clear();
            DataTable dtOrderProcess = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("OC_ORDER_PROCESS", oUserLoginDetail.COMP_CODE);

            ddlOrderProcess.DataSource = dtOrderProcess;
            ddlOrderProcess.DataTextField = "MST_DESC";
            ddlOrderProcess.DataValueField = "MST_CODE";
            ddlOrderProcess.DataBind();
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

    private void BindOrderNo()
    {
        try
        {

            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();

            string sOrderNo = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetMaxOrderNo(oOD_CAPTURE_MST);

            txtOrderNo.Text = sOrderNo;
        }
        catch
        {
            throw;
        }
    }

    private void BindShadeType()
    {
        try
        {
            txtTRNYarnSpiningShade.Items.Clear();

            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("YARN_SHADETYPE", oUserLoginDetail.COMP_CODE);

            txtTRNYarnSpiningShade.DataSource = dtProductionType;
            txtTRNYarnSpiningShade.DataTextField = "MST_DESC";
            txtTRNYarnSpiningShade.DataValueField = "MST_CODE";
            txtTRNYarnSpiningShade.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void BindProcessRoot()
    {
        try
        {
            ddlProcessRoot.Items.Clear();
            SaitexDM.Common.DataModel.TX_PRO_STN_MST oTX_PRO_STN_MST = new SaitexDM.Common.DataModel.TX_PRO_STN_MST();
            oTX_PRO_STN_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_PRO_STN_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_PRO_STN_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dtProcessRoot = SaitexBL.Interface.Method.TX_PRO_STN_MST.GetProcessingMaster(oTX_PRO_STN_MST);

            ddlProcessRoot.DataSource = dtProcessRoot;
            ddlProcessRoot.DataTextField = "PROS_ROUTE_CODE";
            ddlProcessRoot.DataValueField = "PROS_ROUTE_CODE";
            ddlProcessRoot.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void BindFromBranch()
    {
        try
        {
            ddlFromBranch.Items.Clear();

            DataTable dtFromBranch = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMasterByCompCode(oUserLoginDetail.COMP_CODE);

            ddlFromBranch.DataSource = dtFromBranch;
            ddlFromBranch.DataTextField = "BRANCH_NAME";
            ddlFromBranch.DataValueField = "BRANCH_CODE";
            ddlFromBranch.DataBind();

            ddlFromBranch.SelectedIndex = ddlFromBranch.Items.IndexOf(ddlFromBranch.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Business Type Selection"));
        }
    }

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Production Type Selection"));
        }
    }

    protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            OrdercategorySelection();
            BindOrderNo();

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order category Selection"));
        }
    }

    private void OrdercategorySelection()
    {
        try
        {
            txtPartyDetail.Text = string.Empty;

            if (ddlOrderCategory.SelectedValue.Trim().Equals("INHOUSE"))
            {
                txtPartyDetail.Text = "SELF";
                btnTRN_ADJ_BOM.Enabled = true;
                btnTRN_YRNSPIN_CostPrice.Enabled = false;
                ddlParty.Enabled = false;
            }
            else
            {
                ddlParty.Enabled = true;
                btnTRN_ADJ_BOM.Enabled = false;
                btnTRN_YRNSPIN_CostPrice.Enabled = true;
            }
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
            BindOrderNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Type Selection"));
        }
    }

    #region Code to Manage Yarn Spining Data

    private static DataTable dtTRNYarnSpining;

    protected void btnsaveTRNYarnSpiningDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateYarnSpiningTRNRow(out msg))
            {
                SaveTRNYarnSpiningRow();
                BindTRNYarnSpiningGrid();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving row for Yarn Spining PI Indent"));
        }
    }

    private bool ValidateYarnSpiningTRNRow(out string msg)
    {
        try
        {
            string ARTICLE_CODE = string.Empty;
            string UOM = string.Empty;
            string Description = string.Empty;
            string Cust_Req_no = string.Empty;
            string TKT_NO = string.Empty;
            string SHADE = string.Empty;

            if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out Cust_Req_no, out TKT_NO, out SHADE);
            else
                ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

            int iCount = 0;
            int TotalCount = 0;
            msg = string.Empty;

            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

            if (ddlOrderCategory.SelectedItem.Text != "INHOUSE")
            {
                TotalCount++;
                if (txtTRNYarnSpiningCReqNo.Text != string.Empty)
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Customer Request selected";
                }
            }

            TotalCount++;
            if (UNIQUE_ID == 0)
            {
                if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Yarn Spining Artical Code";
                }
            }
            else
            {
                if (txtTRNYarnSpiningArticalCode.SelectedValue.Trim() != string.Empty)
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Yarn Spining Artical Code";
                }
            }

            double dTemp = 0;
            TotalCount++;
            if (txtTRNYarnSpiningOrderQty.Text != string.Empty && double.TryParse(txtTRNYarnSpiningOrderQty.Text, out dTemp))
            {
                iCount++;
            }
            else
            {
                msg += @"\r\nInvalid Yarn Spining Ordered Quantity";
            }

            if (ddlOrderCategory.SelectedItem.Text != "INHOUSE")
            {
                dTemp = 0;
                TotalCount++;
                if (txtTRNYarnSpiningCost.Text != string.Empty && double.TryParse(txtTRNYarnSpiningCost.Text, out dTemp))
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Yarn Spining Cost Price";
                }
            }

            TotalCount++;
            if (Session["dtTRN_BOM"] != null)
            {
                DataTable dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
                DataView dv = new DataView(dtTRN_BOM);
                dv.RowFilter = "ARTICAL_CODE='" + ARTICLE_CODE + "' and PI_TYPE='" + PI_TYPE + "'";
                if (dv.Count > 0)
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Yarn Spining BOM";
                }
            }
            else
            {
                msg += @"\r\nInvalid Yarn Spining BOM";
            }

            dTemp = 0;
            if (txtTRNYarnSpiningSrinkage.Text != string.Empty)
            {
                TotalCount++;
                if (double.TryParse(txtTRNYarnSpiningSrinkage.Text, out dTemp))
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Yarn Spining Shrinkage";
                }
            }

            TotalCount++;
            if (Session["dtTRN_PACK"] != null)
            {
                DataTable dtTRN_PACK = (DataTable)Session["dtTRN_PACK"];
                DataView dv = new DataView(dtTRN_PACK);
                dv.RowFilter = "ARTICAL_CODE='" + ARTICLE_CODE + "' and PI_TYPE='" + PI_TYPE + "'";
                if (dv.Count > 0)
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Yarn Spining Packing Detail";
                }
            }
            else
            {
                msg += @"\r\nInvalid Yarn Spining Packing Detail";
            }


            if (iCount == TotalCount)
                return true;
            else
                return false;
        }
        catch
        {
            throw;
        }
    }

    private void SaveTRNYarnSpiningRow()
    {
        try
        {
            if (dtTRNYarnSpining == null)
                CreateTRNYarnSpiningTable();

            if (dtTRNYarnSpining.Rows.Count < 15)
            {
                int UNIQUE_ID = 0;
                if (ViewState["UNIQUE_ID"] != null)
                    UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

                string ARTICLE_CODE = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;

                if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                    ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out CUST_REQ_NO, out TKT_NO, out SHADE);
                else
                    ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

                bool bb = SearchTRNYarnSpiningArticalCodeInGrid(ARTICLE_CODE, UNIQUE_ID);
                if (!bb)
                {
                    double ORD_Qty = 0;
                    double.TryParse(txtTRNYarnSpiningOrderQty.Text.Trim(), out ORD_Qty);
                    if (ORD_Qty > 0)
                    {
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtTRNYarnSpining);
                            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                            if (dv.Count > 0)
                            {

                                dv[0]["PI_TYPE"] = PI_TYPE;
                                dv[0]["PI_NO"] = UNIQUE_ID;

                                dv[0]["CUST_REQ_NO"] = txtTRNYarnSpiningCReqNo.Text;
                                dv[0]["ARTICAL_CODE"] = ARTICLE_CODE;
                                dv[0]["UOM"] = UOM;
                                dv[0]["ORD_QTY"] = double.Parse(txtTRNYarnSpiningOrderQty.Text.Trim());
                                dv[0]["DEL_DATE"] = DateTime.Parse(txtTRNYarnSpiningDelDate.Text.Trim());
                                dv[0]["DEL_ADDRESS"] = string.Empty;
                                dv[0]["TOTAL_COST"] = double.Parse(txtTRNYarnSpiningCost.Text.Trim());
                                dv[0]["SHADE"] = txtTRNYarnSpiningShade.SelectedValue.Trim();
                                dv[0]["DESIGN"] = string.Empty;
                                dv[0]["LOT_NO"] = string.Empty;
                                dv[0]["PROS_ROUTE_CODE"] = ddlProcessRoot.SelectedValue.Trim();

                                double srinkage = 0;
                                double.TryParse(txtTRNYarnSpiningSrinkage.Text.Trim(), out srinkage);
                                dv[0]["SRINKAGE"] = srinkage;

                                dtTRNYarnSpining.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtTRNYarnSpining.NewRow();
                            dr["UNIQUE_ID"] = dtTRNYarnSpining.Rows.Count + 1;

                            dr["PI_TYPE"] = PI_TYPE;
                            dr["PI_NO"] = dtTRNYarnSpining.Rows.Count + 1;

                            dr["CUST_REQ_NO"] = txtTRNYarnSpiningCReqNo.Text;
                            dr["ARTICAL_CODE"] = ARTICLE_CODE;
                            dr["UOM"] = UOM;
                            dr["ORD_QTY"] = double.Parse(txtTRNYarnSpiningOrderQty.Text.Trim());
                            dr["DEL_DATE"] = DateTime.Parse(txtTRNYarnSpiningDelDate.Text.Trim());
                            dr["DEL_ADDRESS"] = string.Empty;
                            dr["TOTAL_COST"] = double.Parse(txtTRNYarnSpiningCost.Text.Trim());
                            dr["SHADE"] = txtTRNYarnSpiningShade.SelectedValue.Trim();
                            dr["DESIGN"] = string.Empty;
                            dr["LOT_NO"] = string.Empty;
                            dr["PROS_ROUTE_CODE"] = ddlProcessRoot.SelectedValue.Trim();

                            double srinkage = 0;
                            double.TryParse(txtTRNYarnSpiningSrinkage.Text.Trim(), out srinkage);
                            dr["SRINKAGE"] = srinkage;

                            dtTRNYarnSpining.Rows.Add(dr);
                        }
                        RefreshTRNYarnSpiningRow();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(@"Quantity can not be zero");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage(@"enter valid Artical Code");
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchTRNYarnSpiningArticalCodeInGrid(string ArticalCode, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            if (grdTRNYarnSpiningDetail.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdTRNYarnSpiningDetail.Rows)
                {
                    Label txtTRNYarnSpiningArticalCodes = (Label)grdRow.FindControl("txtTRNYarnSpiningArticalCode");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUE_ID = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                    if (txtTRNYarnSpiningArticalCodes.Text.Trim() == ArticalCode && UNIQUE_ID != iUNIQUE_ID)
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

    private void BindTRNYarnSpiningGrid()
    {
        try
        {
            grdTRNYarnSpiningDetail.DataSource = dtTRNYarnSpining;
            grdTRNYarnSpiningDetail.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void CreateTRNYarnSpiningTable()
    {
        try
        {
            dtTRNYarnSpining = new DataTable();
            dtTRNYarnSpining.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRNYarnSpining.Columns.Add("PI_TYPE", typeof(string));
            dtTRNYarnSpining.Columns.Add("PI_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRNYarnSpining.Columns.Add("CUST_REQ_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("UOM", typeof(string));
            dtTRNYarnSpining.Columns.Add("ORD_QTY", typeof(double));
            dtTRNYarnSpining.Columns.Add("DEL_DATE", typeof(DateTime));
            dtTRNYarnSpining.Columns.Add("DEL_ADDRESS", typeof(string));
            dtTRNYarnSpining.Columns.Add("TOTAL_COST", typeof(double));
            dtTRNYarnSpining.Columns.Add("SHADE", typeof(string));
            dtTRNYarnSpining.Columns.Add("DESIGN", typeof(string));
            dtTRNYarnSpining.Columns.Add("LOT_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("SRINKAGE", typeof(double));
            dtTRNYarnSpining.Columns.Add("PROS_ROUTE_CODE", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    private void RefreshTRNYarnSpiningRow()
    {
        try
        {
            txtTRNYarnSpiningCReqNo.Text = string.Empty;
            txtTRNYarnSpiningUOM.Text = string.Empty;
            lblTRNYSpinDesc.Text = string.Empty;
            txtTRNYarnSpiningSrinkage.Text = string.Empty;
            txtTRNYarnSpiningShade.SelectedIndex = -1;
            txtTRNYarnSpiningOrderQty.Text = string.Empty;

            txtTRNYarnSpiningArticalCode.SelectedIndex = -1;
            txtTRNYarnSpiningCost.Text = string.Empty;
            txtTRNYarnSpiningDelDate.Text = string.Empty;
            txtTRNYarnSpiningArticalCode.Enabled = true;
            ddlProcessRoot.SelectedIndex = -1;

            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void btnTRNYarnSpiningCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshTRNYarnSpiningRow();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearing Yarn Spining Detail Row"));
        }
    }

    protected void grdTRNYarnSpiningDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DelTRNYarnSpiningDetail")
            {
                DeleteTRNYarnSpiningRow(UNIQUE_ID);
                BindTRNYarnSpiningGrid();
            }
            if (e.CommandName == "EditTRNYarnSpiningDetail")
            {
                FillTRNYarnSpiningRowByGrid(UNIQUE_ID);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting data for updation/ deletion of Detail Row"));
        }
    }

    private void DeleteTRNYarnSpiningRow(int UNIQUE_ID)
    {
        try
        {
            if (grdTRNYarnSpiningDetail.Rows.Count == 1)
            {
                dtTRNYarnSpining.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRNYarnSpining.Rows)
                {
                    int IUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (IUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRNYarnSpining.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRNYarnSpining.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillTRNYarnSpiningRowByGrid(int UNIQUE_ID)
    {
        try
        {
            DataView dv = new DataView(dtTRNYarnSpining);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {

                ViewState["UNIQUE_ID"] = UNIQUE_ID;
                txtTRNYarnSpiningUOM.Text = dv[0]["UOM"].ToString();
                txtTRNYarnSpiningCReqNo.Text = dv[0]["CUST_REQ_NO"].ToString();
                txtTRNYarnSpiningSrinkage.Text = dv[0]["SRINKAGE"].ToString();
                txtTRNYarnSpiningShade.SelectedIndex = txtTRNYarnSpiningShade.Items.IndexOf(txtTRNYarnSpiningShade.Items.FindByValue(dv[0]["SHADE"].ToString()));
                ddlProcessRoot.SelectedIndex = ddlProcessRoot.Items.IndexOf(ddlProcessRoot.Items.FindByValue(dv[0]["PROS_ROUTE_CODE"].ToString()));
                txtTRNYarnSpiningOrderQty.Text = dv[0]["ORD_QTY"].ToString();
                txtTRNYarnSpiningDelDate.Text = dv[0]["DEL_DATE"].ToString();
                txtTRNYarnSpiningCost.Text = dv[0]["TOTAL_COST"].ToString();

                txtTRNYarnSpiningArticalCode.Enabled = false;

                lblTRNYarnSpiningArticalCode.Text = dv[0]["ARTICAL_CODE"].ToString();
                lblTRNYSpinDesc.Text = dv[0]["ARTICAL_CODE"].ToString();

            }
        }
        catch
        {
            throw;
        }
    }

    private string GetArticalCodeFromString(string sString, out string UOM, out string Description, out string Cust_Req_no, out string TKT_NO, out string SHADE)
    {
        try
        {
            UOM = string.Empty;
            Description = string.Empty;

            char[] splitter = { '@' };
            string[] arrString = sString.Split(splitter);
            string ARTICLE_CODE = arrString[0].ToString();
            UOM = arrString[1].ToString();
            Description = arrString[2].ToString();
            Cust_Req_no = arrString[3].ToString();
            TKT_NO = arrString[4].ToString();
            SHADE = arrString[5].ToString();
            //  double.TryParse(arrString[6].ToString(), out  crQty);

            return ARTICLE_CODE;
        }
        catch
        {
            throw;
        }
    }

    protected void btnTRN_ADJ_BOM_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
            {
                string URL = "bom_adj.aspx";

                string ARTICLE_CODE = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;

                if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                    ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out CUST_REQ_NO, out TKT_NO, out SHADE);
                else
                    ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

                URL = URL + "?TextBoxId=" + txtTRNYarnSpiningOrderQty.ClientID;

                URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.Trim();
                URL = URL + "&PRODUCT_TYPE=" + ddlProductType.SelectedValue.Trim();
                URL = URL + "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.Trim();
                URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.Trim();
                URL = URL + "&ORDER_NO=" + txtOrderNo.Text;
                URL = URL + "&PI_TYPE=" + PI_TYPE;
                URL = URL + "&PI_NO=" + "0";
                URL = URL + "&ARTICAL_CODE=" + ARTICLE_CODE;

                txtTRNYarnSpiningOrderQty.ReadOnly = false;
                txtTRNYarnSpiningDelDate.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=900,height=320,left=200,top=300');", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Item to adjust Indent");
            }
        }
        catch
        {
        }
    }

    protected void btnTRN_YRNSPIN_DelSchedule_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
            {
                string URL = "Del_Schedule.aspx";

                URL = URL + "?TextBoxOrderQty=" + txtTRNYarnSpiningOrderQty.ClientID;
                URL = URL + "&PI_TYPE=" + PI_TYPE;

                string ARTICLE_CODE = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;

                if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                    ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out CUST_REQ_NO, out TKT_NO, out SHADE);
                else
                    ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

                URL = URL + "&ARTICAL_CODE=" + ARTICLE_CODE;
                URL = URL + "&TextBoxDelDate=" + txtTRNYarnSpiningDelDate.ClientID;

                double Qty = 0;
                double.TryParse(txtTRNYarnSpiningOrderQty.Text, out Qty);
                if (Qty > 0)
                {
                    URL = URL + "&QTY=" + Qty;
                }

                txtTRNYarnSpiningOrderQty.ReadOnly = false;
                txtTRNYarnSpiningDelDate.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=700,height=320,left=200,top=300');", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Item to adjust Indent");
            }
        }
        catch
        {
        }
    }

    protected void txtTRNYarnSpiningOrderQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTRNYarnSpiningOrderQty.ReadOnly = true;
            txtTRNYarnSpiningDelDate.ReadOnly = true;
        }
        catch
        {

        }
    }

    protected void btnTRN_YRNSPIN_CostPrice_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
            {
                string URL = "COST.aspx";

                URL = URL + "?TextBoxCost=" + txtTRNYarnSpiningCost.ClientID;
                URL = URL + "&PI_TYPE=" + PI_TYPE;

                string ARTICLE_CODE = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;

                if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                    ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out CUST_REQ_NO, out TKT_NO, out SHADE);
                else
                    ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

                URL = URL + "&ARTICAL_CODE=" + ARTICLE_CODE;

                txtTRNYarnSpiningCost.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=700,height=320,left=200,top=300');", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Item to adjust Indent");
            }
        }
        catch
        {
        }
    }

    protected void txtTRNYarnSpiningCost_TextChanged(object sender, EventArgs e)
    {
        txtTRNYarnSpiningCost.ReadOnly = true;
    }

    protected void btnTRN_YRNSPIN_BOM_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
            {
                string URL = "BOM.aspx";

                // URL = URL + "?TextBoxCost=" + txtTRNYarnSpiningCost.ClientID;
                URL = URL + "?PI_TYPE=" + PI_TYPE;

                string ARTICLE_CODE = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;

                if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                    ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out CUST_REQ_NO, out TKT_NO, out SHADE);
                else
                    ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

                URL = URL + "&ARTICAL_CODE=" + ARTICLE_CODE;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=700,height=320,left=200,top=300');", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select asrtical code");
            }
        }
        catch
        {
        }

    }

    protected void btnTRN_YRNSPIN_Pack_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
            {
                string URL = "Packing.aspx";

                // URL = URL + "?TextBoxCost=" + txtTRNYarnSpiningCost.ClientID;
                URL = URL + "?PI_TYPE=" + PI_TYPE;

                string ARTICLE_CODE = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;

                if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                    ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out CUST_REQ_NO, out TKT_NO, out SHADE);
                else
                    ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

                URL = URL + "&ARTICAL_CODE=" + ARTICLE_CODE;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=700,height=320,left=200,top=300');", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select asrtical code");
            }
        }
        catch
        {
        }

    }

    protected void grdTRNYarnSpiningDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridViewRow grdRow = e.Row;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton txtTRNYarnSpiningCost = (LinkButton)grdRow.FindControl("txtTRNYarnSpiningCost");
                int UNIQUE_ID = int.Parse(txtTRNYarnSpiningCost.CommandArgument);

                if (dtTRNYarnSpining != null && dtTRNYarnSpining.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtTRNYarnSpining);

                    dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                    if (dv.Count > 0)
                    {
                        string ARTICAL_CODE = dv[0]["ARTICAL_CODE"].ToString();

                        // code to add Cost Price data
                        if (Session["dtTRN_COST"] != null)
                        {
                            DataTable dtTRN_COST = (DataTable)Session["dtTRN_COST"];

                            DataView dv_Cost = new DataView(dtTRN_COST);
                            dv_Cost.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE + "' and PI_TYPE='" + PI_TYPE + "'";
                            if (dv_Cost.Count > 0)
                            {
                                DataList dlTRNYRNSPIN_Cost = grdRow.FindControl("dlTRNYRNSPIN_Cost") as DataList;
                                dlTRNYRNSPIN_Cost.DataSource = dv_Cost;
                                dlTRNYRNSPIN_Cost.DataBind();
                            }
                        }

                        // code to add Delivery Schedule data
                        if (Session["dtTRN_DEL_SCHEDULE"] != null)
                        {
                            DataTable dtTRN_DEL_SCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];

                            DataView dv_YRNSPIN_Del_Schedule = new DataView(dtTRN_DEL_SCHEDULE);
                            dv_YRNSPIN_Del_Schedule.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE + "' and PI_TYPE='" + PI_TYPE + "' ";
                            if (dv_YRNSPIN_Del_Schedule.Count > 0)
                            {
                                GridView grdDelSchedule = grdRow.FindControl("grdDelSchedule") as GridView;
                                grdDelSchedule.DataSource = dv_YRNSPIN_Del_Schedule;
                                grdDelSchedule.DataBind();
                            }
                        }

                        // code to add BOM data
                        if (Session["dtTRN_BOM"] != null)
                        {
                            DataTable dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];

                            DataView dvYRNSPIN_BOM = new DataView(dtTRN_BOM);
                            dvYRNSPIN_BOM.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE + "' and PI_TYPE='" + PI_TYPE + "' ";
                            if (dvYRNSPIN_BOM.Count > 0)
                            {
                                GridView grdBOM = grdRow.FindControl("grdBOM") as GridView;
                                grdBOM.DataSource = dvYRNSPIN_BOM;
                                grdBOM.DataBind();
                            }
                        }

                        // code to add PACKING data
                        if (Session["dtTRN_PACK"] != null)
                        {
                            DataTable dtTRN_PACK = (DataTable)Session["dtTRN_PACK"];

                            DataView dvYRNSPIN_PACK = new DataView(dtTRN_PACK);
                            dvYRNSPIN_PACK.RowFilter = "ARTICAL_CODE='" + ARTICAL_CODE + "' and PI_TYPE='" + PI_TYPE + "' ";
                            if (dvYRNSPIN_PACK.Count > 0)
                            {
                                GridView grdPACK = grdRow.FindControl("grdPACK") as GridView;
                                grdPACK.DataSource = dvYRNSPIN_PACK;
                                grdPACK.DataBind();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem on row data bound.\r\nSee error log for detail."));
        }
    }

    #endregion

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                saveOrder();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving Data.\r\nSee error log for detail."));
        }
    }

    private void saveOrder()
    {
        try
        {
            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.CONSIGNEE_ADD = txtConsigneeAdd.Text;
            oOD_CAPTURE_MST.CONSIGNEE_NAME = txtconsigneeName.Text;
            oOD_CAPTURE_MST.CONV_RATE = double.Parse(txtConversionRate.Text);
            oOD_CAPTURE_MST.CURRENCY_CODE = ddlCurrencyCode.SelectedValue.Trim();
            oOD_CAPTURE_MST.DEL_STATUS = false;
            oOD_CAPTURE_MST.ORDER_DATE = DateTime.Parse(txtOrderDate.Text);
            oOD_CAPTURE_MST.ORDER_NO = txtOrderNo.Text;
            oOD_CAPTURE_MST.ORDER_PROCESS = ddlOrderProcess.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();

            DateTime date = System.DateTime.Now.Date;
            bool IsPartyPODate = DateTime.TryParse(txtPartyRefDate.Text, out date);
            oOD_CAPTURE_MST.PARTY_REF_DATE = date;
            oOD_CAPTURE_MST.PARTY_REF_NO = txtPartyRefNumber.Text;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;

            if (ddlOrderCategory.SelectedValue.Trim().Equals("INHOUSE"))
            {
                oOD_CAPTURE_MST.PRTY_CODE = "SELF";
            }
            else
            {
                oOD_CAPTURE_MST.PRTY_CODE = ddlParty.SelectedValue.Trim();
            }

            oOD_CAPTURE_MST.SHIPMENT = txtShipment.Text;
            oOD_CAPTURE_MST.REMARKS = txtRemarks.Text.Trim();
            oOD_CAPTURE_MST.STATUS = true;
            oOD_CAPTURE_MST.TDATE = DateTime.Now.Date;
            oOD_CAPTURE_MST.TUSER = oUserLoginDetail.UserCode;

            oOD_CAPTURE_MST.PAYMENT_MODE = ddlPaymentMode.SelectedValue.Trim();
            oOD_CAPTURE_MST.PAYMENT_TERMS = txtPaymentTerm.Text;
            oOD_CAPTURE_MST.BILL_TO = txtBillTo.Text;
            oOD_CAPTURE_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
            oOD_CAPTURE_MST.GENERAL_INSTRUCTION = txtGenInstruction.Text;
            oOD_CAPTURE_MST.SPECIAL_INSTRUCTION = txtSplInstruction.Text;
            oOD_CAPTURE_MST.FROM_BRANCH = ddlFromBranch.SelectedValue.Trim();

            string msg_YRNSPIN = string.Empty;

            DataTable dtYRNSPIN_DELSCHEDULE = null;
            DataTable dtYRNSPIN_COST = null;
            DataTable dtTRN_BOM = null;

            if (Session["dtTRN_DEL_SCHEDULE"] != null)
                dtYRNSPIN_DELSCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];

            if (Session["dtTRN_COST"] != null)
                dtYRNSPIN_COST = (DataTable)Session["dtTRN_COST"];

            if (Session["dtTRN_BOM"] != null)
                dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];

            DataTable dtBOMAdj = null;
            if (Session["dtBOMAdj"] != null)
                dtBOMAdj = (DataTable)Session["dtBOMAdj"];

            DataTable dtTRN_PACK = null;
            if (Session["dtTRN_PACK"] != null)
                dtTRN_PACK = (DataTable)Session["dtTRN_PACK"];

            bool result = SaitexBL.Interface.Method.OD_CAPTURE_MST.Insert_Order(oOD_CAPTURE_MST, out sOrderNo, dtTRNYarnSpining, dtYRNSPIN_DELSCHEDULE, dtYRNSPIN_COST, out msg_YRNSPIN, IsPartyPODate, dtTRN_BOM, dtBOMAdj, dtTRN_PACK);
            if (result)
            {
                ClearPage();
                string msg = string.Empty;
                msg += "Order Number : " + sOrderNo + " saved successfully.";

                //if (msg_YRNSPIN != string.Empty)
                //    msg += msg_YRNSPIN;

                Common.CommonFuction.ShowMessage(msg);
            }
            else
            {
                Common.CommonFuction.ShowMessage("data Saving Failed");
            }
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

            if (txtOrderNo.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Order Number required.\r\n";
            }

            if (ddlParty.SelectedIndex != -1 || ddlParty.SelectedValue.Trim() != "SELECT" || ddlParty.SelectedValue.Trim() != string.Empty || txtPartyDetail.Text.Equals("SELF"))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select party first.\r\n";
            }

            if (ValidateTRNDataForFormSaving(ref msg))
            {
                count += 1;
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

    private bool ValidateTRNDataForFormSaving(ref string msg)
    {
        try
        {
            bool bResult = false;
            int iCount = 0;
            int iCountAll = 0;

            if (iCount == iCountAll)
            {
                bResult = true;
            }
            else
            {
                bResult = false;
            }
            return bResult;
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
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in update mode.\r\nSee error log for detail."));
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {

            SetPageAccordingProductType();
            BindBusinessType();
            BindOrderProcess();
            BindOrderType();
            BindProductType();
            BindShadeType();
            BindProcessRoot();
            BindCurrency();
            BindFromBranch();
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            lblMode.Text = "Update";

            ddlOrderCategory.Enabled = false;
            ddlBusinessType.Enabled = false;
            ddlProductType.Enabled = false;
            ddlOrderType.Enabled = false;
            txtOrderNo.Visible = false;

            ddlOrderNo.Visible = true;
            txtTRNYarnSpiningArticalCode.Enabled = false;
            ddlOrderNo.LoadData(PRODUCT_TYPE);
        }
        catch
        {
            throw;
        }
    }

    private int GetdataByOrderNumber(string ORDER_STRING)
    {
        int iRecordFound = 0;
        try
        {

            string[] Order_strings = ORDER_STRING.Split('@');
            string sComp_code = Order_strings[0].ToString();
            string sBRANCH_CODE = Order_strings[1].ToString();
            string sBUSINESS_TYPE = Order_strings[2].ToString();
            string sPRODUCT_TYPE = Order_strings[3].ToString();
            string sORDER_CAT = Order_strings[4].ToString();
            string sORDER_TYPE = Order_strings[5].ToString();
            string sORDER_NO = Order_strings[6].ToString();

            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
            oOD_CAPTURE_MST.COMP_CODE = sComp_code;
            oOD_CAPTURE_MST.BRANCH_CODE = sBRANCH_CODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = sBUSINESS_TYPE;
            oOD_CAPTURE_MST.PRODUCT_TYPE = sPRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = sORDER_CAT;
            oOD_CAPTURE_MST.ORDER_TYPE = sORDER_TYPE;
            oOD_CAPTURE_MST.ORDER_NO = sORDER_NO;

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetORDERDATAByORDER_NO(oOD_CAPTURE_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;

                ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue(dt.Rows[0]["BUSINESS_TYPE"].ToString().Trim()));
                ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(dt.Rows[0]["PRODUCT_TYPE"].ToString().Trim()));
                ddlOrderCategory.SelectedIndex = ddlOrderCategory.Items.IndexOf(ddlOrderCategory.Items.FindByValue(dt.Rows[0]["ORDER_CAT"].ToString().Trim()));
                ddlOrderProcess.SelectedIndex = ddlOrderProcess.Items.IndexOf(ddlOrderProcess.Items.FindByValue(dt.Rows[0]["ORDER_PROCESS"].ToString().Trim()));
                ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue(dt.Rows[0]["ORDER_TYPE"].ToString().Trim()));

                txtOrderNo.Text = dt.Rows[0]["ORDER_NO"].ToString().Trim();
                txtOrderDate.Text = DateTime.Parse(dt.Rows[0]["ORDER_DATE"].ToString().Trim()).ToShortDateString();
                ddlCurrencyCode.SelectedIndex = ddlCurrencyCode.Items.IndexOf(ddlCurrencyCode.Items.FindByValue(dt.Rows[0]["CURRENCY_CODE"].ToString().Trim()));
                txtConversionRate.Text = dt.Rows[0]["CONV_RATE"].ToString().Trim();

                txtPartyRefNumber.Text = dt.Rows[0]["PARTY_REF_NO"].ToString().Trim();

                if (dt.Rows[0]["PARTY_REF_DATE"].ToString().Trim() != string.Empty)
                    txtPartyRefDate.Text = DateTime.Parse(dt.Rows[0]["PARTY_REF_DATE"].ToString().Trim()).ToShortDateString();

                txtShipment.Text = dt.Rows[0]["SHIPMENT"].ToString().Trim();

                txtconsigneeName.Text = dt.Rows[0]["CONSIGNEE_NAME"].ToString().Trim();
                txtConsigneeAdd.Text = dt.Rows[0]["CONSIGNEE_ADD"].ToString().Trim();

                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString().Trim();

                ddlPaymentMode.SelectedIndex = ddlPaymentMode.Items.IndexOf(ddlPaymentMode.Items.FindByValue(dt.Rows[0]["PAYMENT_MODE"].ToString().Trim()));
                txtPaymentTerm.Text = dt.Rows[0]["PAYMENT_TERMS"].ToString().Trim();
                txtBillTo.Text = dt.Rows[0]["BILL_TO"].ToString().Trim();
                ddlDeliveryMode.SelectedIndex = ddlDeliveryMode.Items.IndexOf(ddlDeliveryMode.Items.FindByValue(dt.Rows[0]["DELIVERY_MODE"].ToString().Trim()));
                txtGenInstruction.Text = dt.Rows[0]["GENERAL_INSTRUCTION"].ToString().Trim();
                txtSplInstruction.Text = dt.Rows[0]["SPECIAL_INSTRUCTION"].ToString().Trim();

                ddlFromBranch.SelectedIndex = ddlFromBranch.Items.IndexOf(ddlFromBranch.Items.FindByValue(dt.Rows[0]["FROM_BRANCH"].ToString().Trim()));

                if (dt.Rows[0]["ORDER_CAT"].ToString().Trim().Equals("INHOUSE"))
                {
                    OrdercategorySelection();
                }
                else
                {
                    txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                    txtPartyDetail.Text = ddlParty.SelectedItem.Trim();
                }
            }

            if (iRecordFound == 1)
            {
                // Code For Yarn Spining 
                DataTable dtTemp_YRNSPIN = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetTRN_ByORDER_NO(oOD_CAPTURE_MST);

                if (dtTemp_YRNSPIN != null && dtTemp_YRNSPIN.Rows.Count > 0)
                {

                    DataTable dtTRN_COST = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetCOST_ByORDER_NO(oOD_CAPTURE_MST);
                    DataTable dtTRN_DEL_SCHEDULE = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDELSCHEDULE_ByORDER_NO(oOD_CAPTURE_MST);
                    DataTable dtTRN_BOM = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetBOM_ByORDER_NO(oOD_CAPTURE_MST);
                    DataTable dtTRN_PACK = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetPACK_TRN_ByORDER_NO(oOD_CAPTURE_MST);

                    MapDataTable_YRNSPIN(dtTemp_YRNSPIN);
                    MapDataTable_YRNSPIN_COST(dtTRN_COST);
                    MapDataTable_YRNSPIN_DEL(dtTRN_DEL_SCHEDULE);
                    MapDataTable_YRNSPIN_BOM(dtTRN_BOM);
                    MapDataTable_YRNSPIN_PACK(dtTRN_PACK);

                    BindTRNYarnSpiningGrid();
                }
            }

            return iRecordFound;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MapDataTable_YRNSPIN(DataTable dtTemp)
    {
        try
        {
            if (dtTRNYarnSpining == null || dtTRNYarnSpining.Rows.Count == 0)
                CreateTRNYarnSpiningTable();
            dtTRNYarnSpining.Rows.Clear();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtTRNYarnSpining.NewRow();

                    dr["UNIQUE_ID"] = dtTRNYarnSpining.Rows.Count + 1;
                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["PI_NO"] = drTemp["PI_NO"];
                    dr["CUST_REQ_NO"] = drTemp["CUST_REQ_NO"];
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["UOM"] = drTemp["UOM"];
                    dr["ORD_QTY"] = drTemp["ORD_QTY"];
                    dr["SHADE"] = drTemp["SHADE"];
                    dr["DESIGN"] = drTemp["DESIGN"];
                    dr["LOT_NO"] = drTemp["LOT_NO"];
                    dr["PROS_ROUTE_CODE"] = drTemp["PROS_ROUTE_CODE"];
                    dr["SRINKAGE"] = drTemp["SRINKAGE"];
                    dr["DEL_DATE"] = drTemp["DEL_DATE"];
                    dr["DEL_ADDRESS"] = drTemp["DEL_ADDRESS"];
                    dr["TOTAL_COST"] = drTemp["TOTAL_COST"];

                    dtTRNYarnSpining.Rows.Add(dr);

                }
                dtTemp = null;
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable_YRNSPIN_COST(DataTable dtTemp)
    {
        try
        {

            if (Session["dtTRN_COST"] != null)
                Session["dtTRN_COST"] = null;

            DataTable dtTRN_COST = CreateDataTable_Cost();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtTRN_COST.NewRow();

                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["SALE"] = drTemp["SALE"];
                    dr["FREIGHT"] = drTemp["FREIGHT"];
                    dr["COMMISSION"] = drTemp["COMMISSION"];
                    dr["BROKERAGE"] = drTemp["BROKERAGE"];
                    dr["INCENTIVES"] = drTemp["INCENTIVES"];
                    dr["EX_MILL_RATE"] = drTemp["EX_MILL_RATE"];
                    dr["TOTAL"] = drTemp["TOTAL"];
                    dr["COST_REMARKS"] = drTemp["COST_REMARKS"];

                    // ADDED ON 11 MAY 2011
                    dr["FOB"] = drTemp["FOB"];
                    dr["HANDLING_CHARGES"] = drTemp["HANDLING_CHARGES"];
                    dr["BILL_D_CHARGES"] = drTemp["BILL_D_CHARGES"];
                    dr["EXPORT_INCENTIVES"] = drTemp["EXPORT_INCENTIVES"];
                    dr["EXPORT_INCENTIVES_AMT"] = drTemp["EXPORT_INCENTIVES_AMT"];
                    dr["OTHER_COST"] = drTemp["OTHER_COST"];

                    dtTRN_COST.Rows.Add(dr);
                }
                dtTemp = null;
                Session["dtTRN_COST"] = dtTRN_COST;
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable_Cost()
    {
        try
        {
            DataTable dtTRN_COST = new DataTable();
            dtTRN_COST.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRN_COST.Columns.Add("PI_TYPE", typeof(string));
            dtTRN_COST.Columns.Add("SALE", typeof(double));
            dtTRN_COST.Columns.Add("FREIGHT", typeof(double));
            dtTRN_COST.Columns.Add("COMMISSION", typeof(double));
            dtTRN_COST.Columns.Add("BROKERAGE", typeof(double));
            dtTRN_COST.Columns.Add("INCENTIVES", typeof(double));
            dtTRN_COST.Columns.Add("EX_MILL_RATE", typeof(double));
            dtTRN_COST.Columns.Add("TOTAL", typeof(double));
            dtTRN_COST.Columns.Add("COST_REMARKS", typeof(string));

            //ADDED ON 11 MAY 2011
            dtTRN_COST.Columns.Add("FOB", typeof(double));
            dtTRN_COST.Columns.Add("HANDLING_CHARGES", typeof(double));
            dtTRN_COST.Columns.Add("BILL_D_CHARGES", typeof(double));
            dtTRN_COST.Columns.Add("EXPORT_INCENTIVES", typeof(double));
            dtTRN_COST.Columns.Add("EXPORT_INCENTIVES_AMT", typeof(double));
            dtTRN_COST.Columns.Add("OTHER_COST", typeof(double));
            return dtTRN_COST;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable_YRNSPIN_DEL(DataTable dtTemp)
    {
        try
        {

            if (Session["dtTRN_DEL_SCHEDULE"] != null)
                Session["dtTRN_DEL_SCHEDULE"] = null;

            DataTable dtYRNSPIN_Del = CreateDataTable_Del();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtYRNSPIN_Del.NewRow();

                    dr["UNIQUE_ID"] = dtYRNSPIN_Del.Rows.Count + 1;
                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["DEL_ADDRESS"] = drTemp["DEL_ADDRESS"];
                    dr["DEL_DATE"] = drTemp["DEL_DATE"];
                    dr["DEL_QTY"] = drTemp["DEL_QTY"];
                    dr["DEL_REMARKS"] = drTemp["DEL_REMARKS"];

                    dtYRNSPIN_Del.Rows.Add(dr);
                }
                dtTemp = null;
                Session["dtTRN_DEL_SCHEDULE"] = dtYRNSPIN_Del;
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable_Del()
    {
        try
        {
            DataTable dtTRN_DEL_SCHEDULE = new DataTable();
            dtTRN_DEL_SCHEDULE.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_DEL_SCHEDULE.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRN_DEL_SCHEDULE.Columns.Add("PI_TYPE", typeof(string));
            dtTRN_DEL_SCHEDULE.Columns.Add("DEL_ADDRESS", typeof(string));
            dtTRN_DEL_SCHEDULE.Columns.Add("DEL_DATE", typeof(DateTime));
            dtTRN_DEL_SCHEDULE.Columns.Add("DEL_QTY", typeof(double));
            dtTRN_DEL_SCHEDULE.Columns.Add("DEL_REMARKS", typeof(string));
            return dtTRN_DEL_SCHEDULE;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable_YRNSPIN_BOM(DataTable dtTemp)
    {
        try
        {
            if (Session["dtTRN_BOM"] != null)
                Session["dtTRN_BOM"] = null;

            DataTable dtTRN_BOM = CreateDataTable_BOM();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtTRN_BOM.NewRow();

                    dr["UNIQUE_ID"] = dtTRN_BOM.Rows.Count + 1;
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["W_SIDE"] = drTemp["W_SIDE"];
                    dr["BASE_ARTICAL_TYPE"] = drTemp["BASE_ARTICAL_TYPE"];
                    dr["BASE_ARTICAL_CODE"] = drTemp["BASE_ARTICAL_CODE"];
                    dr["UOM"] = drTemp["UOM"];
                    dr["BASIS"] = drTemp["BASIS"];
                    dr["VALUE_QTY"] = drTemp["VALUE_QTY"];
                    dr["REQ_QTY"] = drTemp["REQ_QTY"];
                    dr["BOM_REMARKS"] = drTemp["BOM_REMARKS"];

                    dtTRN_BOM.Rows.Add(dr);
                }
                dtTemp = null;
                Session["dtTRN_BOM"] = dtTRN_BOM;
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable_BOM()
    {
        try
        {
            DataTable dtTRN_BOM = new DataTable();
            dtTRN_BOM.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_BOM.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("PI_TYPE", typeof(string));

            dtTRN_BOM.Columns.Add("W_SIDE", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_ARTICAL_TYPE", typeof(string));
            dtTRN_BOM.Columns.Add("BASE_ARTICAL_CODE", typeof(string));
            dtTRN_BOM.Columns.Add("UOM", typeof(string));
            dtTRN_BOM.Columns.Add("BASIS", typeof(string));
            dtTRN_BOM.Columns.Add("VALUE_QTY", typeof(double));
            dtTRN_BOM.Columns.Add("REQ_QTY", typeof(double));
            dtTRN_BOM.Columns.Add("BOM_REMARKS", typeof(double));
            return dtTRN_BOM;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable_YRNSPIN_PACK(DataTable dtTemp)
    {
        try
        {
            if (Session["dtTRN_PACK"] != null)
                Session["dtTRN_PACK"] = null;

            DataTable dtTRN_PACK = CreateDataTable_PACK();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtTRN_PACK.NewRow();

                    dr["UNIQUE_ID"] = dtTRN_PACK.Rows.Count + 1;
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["PCK_CODE"] = drTemp["PCK_CODE"];
                    dr["PCK_DESC"] = drTemp["PCK_DESC"];
                    dr["PCK_QTY"] = drTemp["PCK_QTY"];

                    dtTRN_PACK.Rows.Add(dr);
                }
                dtTemp = null;
                Session["dtTRN_PACK"] = dtTRN_PACK;
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable_PACK()
    {
        try
        {
            DataTable dtTRN_PACK = new DataTable();
            dtTRN_PACK.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_PACK.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRN_PACK.Columns.Add("PI_TYPE", typeof(string));

            dtTRN_PACK.Columns.Add("PCK_CODE", typeof(string));
            dtTRN_PACK.Columns.Add("PCK_DESC", typeof(string));
            dtTRN_PACK.Columns.Add("PCK_QTY", typeof(string));

            return dtTRN_PACK;
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
                UpdateOrder();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating Data.\r\nSee error log for detail."));
        }

    }

    private void UpdateOrder()
    {
        try
        {
            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.CONSIGNEE_ADD = txtConsigneeAdd.Text;
            oOD_CAPTURE_MST.CONSIGNEE_NAME = txtconsigneeName.Text;
            oOD_CAPTURE_MST.CONV_RATE = double.Parse(txtConversionRate.Text);
            oOD_CAPTURE_MST.CURRENCY_CODE = ddlCurrencyCode.SelectedValue.Trim();
            oOD_CAPTURE_MST.DEL_STATUS = false;
            oOD_CAPTURE_MST.ORDER_DATE = DateTime.Parse(txtOrderDate.Text);
            oOD_CAPTURE_MST.ORDER_NO = txtOrderNo.Text;
            oOD_CAPTURE_MST.ORDER_PROCESS = ddlOrderProcess.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();

            DateTime date = System.DateTime.Now.Date;
            bool IsPartyPODate = DateTime.TryParse(txtPartyRefDate.Text, out date);
            oOD_CAPTURE_MST.PARTY_REF_DATE = date;
            oOD_CAPTURE_MST.PARTY_REF_NO = txtPartyRefNumber.Text;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;

            if (ddlOrderCategory.SelectedValue.Trim().Equals("INHOUSE"))
            {
                oOD_CAPTURE_MST.PRTY_CODE = "SELF";
            }
            else
            {
                oOD_CAPTURE_MST.PRTY_CODE = ddlParty.SelectedValue.Trim();
            }

            oOD_CAPTURE_MST.SHIPMENT = txtShipment.Text;
            oOD_CAPTURE_MST.REMARKS = txtRemarks.Text.Trim();
            oOD_CAPTURE_MST.STATUS = true;
            oOD_CAPTURE_MST.TDATE = DateTime.Now.Date;
            oOD_CAPTURE_MST.TUSER = oUserLoginDetail.UserCode;

            oOD_CAPTURE_MST.PAYMENT_MODE = ddlPaymentMode.SelectedValue.Trim();
            oOD_CAPTURE_MST.PAYMENT_TERMS = txtPaymentTerm.Text;
            oOD_CAPTURE_MST.BILL_TO = txtBillTo.Text;
            oOD_CAPTURE_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
            oOD_CAPTURE_MST.GENERAL_INSTRUCTION = txtGenInstruction.Text;
            oOD_CAPTURE_MST.SPECIAL_INSTRUCTION = txtSplInstruction.Text;
            oOD_CAPTURE_MST.FROM_BRANCH = ddlFromBranch.SelectedValue.Trim();

            string ORDER_NO = txtOrderNo.Text;
            string msg_YRNSPIN = string.Empty;

            DataTable dtYRNSPIN_DELSCHEDULE = null;
            DataTable dtYRNSPIN_COST = null;
            DataTable dtTRN_BOM = null;

            if (Session["dtTRN_DEL_SCHEDULE"] != null)
                dtYRNSPIN_DELSCHEDULE = (DataTable)Session["dtTRN_DEL_SCHEDULE"];

            if (Session["dtTRN_COST"] != null)
                dtYRNSPIN_COST = (DataTable)Session["dtTRN_COST"];

            if (Session["dtTRN_BOM"] != null)
                dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];

            DataTable dtBOMAdj = null;
            if (Session["dtBOMAdj"] != null)
                dtBOMAdj = (DataTable)Session["dtBOMAdj"];

            DataTable dtTRN_PACK = null;
            if (Session["dtTRN_PACK"] != null)
                dtTRN_PACK = (DataTable)Session["dtTRN_PACK"];

            bool result = SaitexBL.Interface.Method.OD_CAPTURE_MST.Update_Order(oOD_CAPTURE_MST, dtTRNYarnSpining, dtYRNSPIN_DELSCHEDULE, dtYRNSPIN_COST, out msg_YRNSPIN, IsPartyPODate, dtTRN_BOM, dtBOMAdj, dtTRN_PACK);
            if (result)
            {
                ClearPage();
                string msg = string.Empty;
                msg += "Order Number : " + ORDER_NO + " Updated successfully.";

                //if (msg_YRNSPIN != string.Empty)
                //    msg += "\\r\\nPI Indent # for Yarn Spining are " + msg_YRNSPIN + ".";

                Common.CommonFuction.ShowMessage(msg);

            }
            else
            {
                Common.CommonFuction.ShowMessage("data Updation Failed");
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

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearPage();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the page"));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/OrderDevelopment/Reports/OC_Parameter.aspx?PRODUCT_TYPE=" + PRODUCT_TYPE, false);
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leaving the page"));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void DisableFieldsforPPC()
    {

        try
        {

            ddlBusinessType.Enabled = false;
            ddlProductType.Enabled = false;
            ddlOrderCategory.Enabled = false;
            txtOrderDate.Enabled = false;
            ddlCurrencyCode.Enabled = false;
            ddlParty.Enabled = false;

            txtPartyDetail.Enabled = false;
            txtPartyRefDate.Enabled = false;
            ddlOrderProcess.Enabled = false;
            txtconsigneeName.Enabled = false;
            txtConsigneeAdd.Enabled = false;
            ddlPaymentMode.Enabled = false;

            txtPaymentTerm.Enabled = false;
            ddlDeliveryMode.Enabled = false;
            ddlFromBranch.Enabled = false;
            txtBillTo.Enabled = false;
            txtRemarks.Enabled = false;
            txtGenInstruction.Enabled = false;
            txtSplInstruction.Enabled = false;
            btnTRN_YRNSPIN_DelSchedule.Enabled = false;
            btnTRN_ADJ_BOM.Enabled = false;
            txtConversionRate.Enabled = false;
            txtShipment.Enabled = false;
            txtPartyRefNumber.Enabled = false;
        }
        catch
        {


        }

    }
}
