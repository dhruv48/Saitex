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
using Obout.ComboBox;
public partial class Module_OrderDevelopment_Controls_OC_YARN_SPINING : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST;
    public   string PI_TYPE; 
    private  string sOrderNo = string.Empty;
    public string PRODUCT_TYPE
    {
        get;
        set;
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ddlOrderNo.PRODUCT_TYPE = PRODUCT_TYPE;
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"].ToString().Equals(string.Empty) == false)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }
            SetPageAccordingProductType();
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    protected override void OnInit(EventArgs e)
    {
        //txtPartyCodecmb.AutoPostBack = true;
        //txtPartyCodecmb.OnTextChanged += new CommonControls_PartyCodeLOV.RefreshDataGridView(txtPartyCodecmb_OnTextChanged);

        ddlConsignee.AutoPostBack = true;
        ddlConsignee.OnTextChanged += new CommonControls_PartyCodeLOV.RefreshDataGridView(ddlConsignee_OnTextChanged);
        base.OnInit(e);

        //txtTRNYarnSpiningArticalCode.AutoPostBack = true;
        //txtTRNYarnSpiningArticalCode.OnTextChanged += new CommonControls_LOV_CustReqArticleLOV.RefreshDataGridView(txtTRNYarnSpiningArticalCode_OnTextChanged);
        //txtTRNYarnSpiningArticalCode.PI_TYPE = PI_TYPE;

        ddlOrderNo.AutoPostBack = true;
        ddlOrderNo.OnTextChanged += new CommonControls_LOV_OrderNoLOV.RefreshDataGridView(ddlOrderNo_OnTextChanged);
        ddlOrderNo.PRODUCT_TYPE = PRODUCT_TYPE;
    }

    void ddlConsignee_OnTextChanged(string Value, string Text)
    {
        try
        {
            txtconsigneeName.Text = ddlConsignee.SelectedItem.Trim();
            txtConsigneeAdd.Text = ddlConsignee.SelectedValue.Trim();

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party selection"));
        }
    }

    void ddlOrderNo_OnTextChanged(string Value, string Text)
    {
        try
        {
            string ORDER_STRING = ddlOrderNo.SelectedValue.Trim();

            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();
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
                //ClearPage();
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
            string SHADE_NAME = string.Empty;


            if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                ARTICLE_CODE = string.Empty;//GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out Cust_Req_no, out TKT_NO, out SHADE, out crQty, out SHADE_NAME);
            else
                ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

            bool bb = SearchTRNYarnSpiningArticalCodeInGrid(ARTICLE_CODE, UNIQUE_ID, txtShadeCode.Text);
            if (!bb)
            {
                txtTRNYarnSpiningUOM.Text = UOM;
                lblTRNYSpinDesc.Text = Description;
                lblTRNYarnSpiningArticalCode.Text = ARTICLE_CODE;
                txtTRNYarnSpiningUOM.Text = UOM;
               
            }
            else
            {
                lblTRNYarnSpiningArticalCode.Text = string.Empty;
                txtTRNYarnSpiningArticalCode.SelectedIndex = -1;
                lblTRNYSpinDesc.Text = string.Empty;
                Common.CommonFuction.ShowMessage("This Yarn artical code already exists.");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Yarn artical code"));
        }
    }

    private void InitialisePage()
    {
        try
        {
           
            BindBusinessType();
            BindOrderProcess();
            BindOrderType();
            BindProductType();
            BindCurrency();
            BindFromBranch();
            ClearPage();
            CONSINEE.Visible = false;

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
            
            if (PRODUCT_TYPE == "YARN DYEING")
            {
                PI_TYPE = "YARN DYEING";
            }
            else if (PRODUCT_TYPE == "TEXTURISED YARN")
            {
                PI_TYPE = "YARN TEXTURISING";
            }

            else if (PRODUCT_TYPE == "TWISTED YARN")
            {
                PI_TYPE = "YARN TWISTING";
            }
            else if (PRODUCT_TYPE == "YARN SPINING")
            {
                PI_TYPE = "YARN SPINING";
            }
            else if (PRODUCT_TYPE == "FABRIC")
            {
                PI_TYPE = "FABR_PROC";
            }

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
            //ddlOrderType.SelectedIndex = -1;
            ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue("PRODUCTION"));
            txtPartyCodecmb.SelectedIndex = -1;
            ddlOrderCategory.SelectedIndex = -1;
            ddlPaymentMode.SelectedIndex = -1;
            ddlDeliveryMode.SelectedIndex = -1;
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
            txtOrderDate.Text = System.DateTime.Now.Date.ToShortDateString();
            Session["dtTRN_DEL_SCHEDULE"] = null;
            Session["dtTRN_COST"] = null;
            Session["dtTRN_BOM"] = null;
            Session["dtBOMAdj"] = null;
            Session["dtYarnSpinningCustAdj"] = null;
            Session["dtTRNYarnSpining"] = null;
            ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue("SW"));
            ddlCurrencyCode.SelectedIndex = ddlCurrencyCode.Items.IndexOf(ddlCurrencyCode.Items.FindByValue("Rs."));
            txtPartyCode.Text = string.Empty;
            ddlConsignee.SelectedIndex = -1;
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
            ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue("PRODUCTION"));

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
            txtOrderNo.Text = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetMaxOrderNo(oOD_CAPTURE_MST);
            
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

            if (ddlOrderCategory.SelectedValue.Trim().Equals("CR_CLUB"))
            {
                txtPartyCode.Text = "SELF";
                txtPartyDetail.Text = "SANIMO POLYMERS PVT. LTD.";
                txtPartyCodecmb.Enabled = false;
            }
            else
            {
                txtPartyCodecmb.Enabled = true;
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

    private  DataTable dtTRNYarnSpining;

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
            string SHADE_NAME = string.Empty;
            double crQty = 0;

            if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                ARTICLE_CODE = string.Empty;// GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out Cust_Req_no, out TKT_NO, out SHADE, out crQty, out SHADE_NAME);
            else
                ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

            int iCount = 0;
            int TotalCount = 0;
            msg = string.Empty;

            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

            if (ddlOrderCategory.SelectedItem.Text != "CR_CLUB") // INHOUSE
            {
                TotalCount++;
                //if (txtTRNYarnSpiningCReqNo.Text != string.Empty)
                //{
                //    iCount++;
                //}
                //else
                //{
                //    msg += @"\r\nInvalid Customer Request selected";
                //}
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
                    msg += @"\r\nInvalid Artical Code";
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
                    msg += @"\r\nInvalid Artical Code";
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
                msg += @"\r\nInvalid Ordered Quantity";
            }

            if (ddlOrderCategory.SelectedItem.Text != "CR_CLUB")
            {
                //dTemp = 0;
                //TotalCount++;
                //if (txtTRNYarnSpiningCost.Text != string.Empty && double.TryParse(txtTRNYarnSpiningCost.Text, out dTemp))
                //{
                //    iCount++;
                //}
                //else
                //{
                //    msg += @"\r\nInvalid Cost Price";
                //}
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
                    msg += @"\r\nInvalid BOM";
                }
            }
            else
            {
                msg += @"\r\nInvalid BOM";
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
                    msg += @"\r\nInvalid Shrinkage";
                }
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
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();

            if (dtTRNYarnSpining.Rows.Count < 15)
            {
                int UNIQUE_ID = 0;
                if (ViewState["UNIQUE_ID"] != null)
                {
                    UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                }

                string ARTICLE_CODE = string.Empty;
                string GREY_LOT_NO = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;
                string SHADE_NAME = string.Empty;


                if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
                {
                    ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description);
                }

                else
                {
                    ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;
                }
                bool bb = SearchTRNYarnSpiningArticalCodeInGrid(ARTICLE_CODE, UNIQUE_ID, txtShadeCode.Text.Trim());
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
                                dv[0]["PI_NO"] = lblpi_no.Text;
                                dv[0]["ARTICAL_CODE"] = ARTICLE_CODE;
                                dv[0]["GREY_LOT_NO"] = txtLotNo.SelectedValue;
                                dv[0]["UOM"] = txtTRNYarnSpiningUOM.Text ;
                                dv[0]["ORD_QTY"] = double.Parse(txtTRNYarnSpiningOrderQty.Text.Trim());
                                DateTime deldate = Convert.ToDateTime(txtTRNYarnSpiningDelDate.Text.Trim());
                                dv[0]["DEL_DATE"] = deldate.ToShortDateString();
                                dv[0]["SHADE_CODE"] = txtShadeCode.Text.Trim();
                                dv[0]["LAB_DIP_NO"] = txtLabDipNo.Text.Trim();
                             // dv[0]["LOT_ID"] = txtLotID.Text.Trim();
                                double srinkage = 0;
                                double.TryParse(txtTRNYarnSpiningSrinkage.Text.Trim(), out srinkage);
                                dv[0]["SRINKAGE"] = srinkage;
                                dv[0]["LOT_ID"] = lblpi_no.Text;
                                dv[0]["CUST_REQ_NO"] = lblCRNo.Text ;
                                dv[0]["REMARKS"] = txtTRNYyarnRemarks.Text.Trim();
                                dtTRNYarnSpining.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtTRNYarnSpining.NewRow();
                            dr["UNIQUE_ID"] = dtTRNYarnSpining.Rows.Count + 1;
                            dr["PI_TYPE"] = PI_TYPE;
                            dr["PI_NO"] = dtTRNYarnSpining.Rows.Count + 1;
                            dr["ARTICAL_CODE"] = ARTICLE_CODE;
                            dr["GREY_LOT_NO"] = txtLotNo.SelectedValue;
                            dr["UOM"] = txtTRNYarnSpiningUOM.Text;
                            dr["ORD_QTY"] = double.Parse(txtTRNYarnSpiningOrderQty.Text.Trim());
                            DateTime deldate = Convert.ToDateTime(txtTRNYarnSpiningDelDate.Text.Trim());
                            dr["DEL_DATE"] = deldate.ToShortDateString();
                            dr["LOT_ID"] = lblpi_no.Text;
                            dr["SHADE_CODE"] = txtShadeCode.Text.Trim();
                            dr["LAB_DIP_NO"] = txtLabDipNo.Text.Trim();
                            double srinkage = 0;
                            double.TryParse(txtTRNYarnSpiningSrinkage.Text.Trim(), out srinkage);
                            dr["SRINKAGE"] = srinkage;
                            dr["CUST_REQ_NO"] = lblCRNo.Text;
                            dr["REMARKS"] = txtTRNYyarnRemarks.Text.Trim();
                            dtTRNYarnSpining.Rows.Add(dr);
                            
                        }
                        Session["dtTRNYarnSpining"] = dtTRNYarnSpining;                        
                        BindTRNYarnSpiningGrid();
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

    private bool SearchTRNYarnSpiningArticalCodeInGrid(string ArticalCode, int UNIQUE_ID, string Shade_code)
    {
        bool Result = false;
        try
        {
            if (grdTRNYarnSpiningDetail.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdTRNYarnSpiningDetail.Rows)
                {
                    Label txtTRNYarnSpiningArticalCodes = (Label)grdRow.FindControl("txtTRNYarnSpiningArticalCode");
                    Label txtTRNYarnSpiningShade = (Label)grdRow.FindControl("txtTRNYarnSpiningShade");

                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUE_ID = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                    if (txtTRNYarnSpiningArticalCodes.Text.Trim() == ArticalCode && UNIQUE_ID != iUNIQUE_ID && txtTRNYarnSpiningShade.Text == Shade_code)
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
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();
            grdTRNYarnSpiningDetail.DataSource = dtTRNYarnSpining;
            grdTRNYarnSpiningDetail.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private DataTable  CreateTRNYarnSpiningTable()
    {
        try
        {
            dtTRNYarnSpining = new DataTable();
            dtTRNYarnSpining.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRNYarnSpining.Columns.Add("PI_TYPE", typeof(string));
            dtTRNYarnSpining.Columns.Add("PI_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRNYarnSpining.Columns.Add("GREY_LOT_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("REMARKS", typeof(string));
            dtTRNYarnSpining.Columns.Add("UOM", typeof(string));
            dtTRNYarnSpining.Columns.Add("ORD_QTY", typeof(double));
            dtTRNYarnSpining.Columns.Add("DEL_DATE", typeof(DateTime));
            dtTRNYarnSpining.Columns.Add("LOT_ID", typeof(string));
            dtTRNYarnSpining.Columns.Add("SHADE_CODE", typeof(string));
            dtTRNYarnSpining.Columns.Add("LAB_DIP_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("SRINKAGE", typeof(double));            
            dtTRNYarnSpining.Columns.Add("FINAL_ORDER_CONF_CLAG", typeof(string));
            dtTRNYarnSpining.Columns.Add("CUST_REQ_NO", typeof(string));
            return dtTRNYarnSpining;

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

            txtTRNYarnSpiningUOM.Text = string.Empty;
            txtTRNYyarnRemarks.Text = string.Empty;
            lblTRNYSpinDesc.Text = string.Empty;
            txtTRNYarnSpiningOrderQty.Text = string.Empty;
            txtTRNYarnSpiningArticalCode.SelectedIndex = -1;
            txtTRNYarnSpiningArticalCode.Enabled = true;
            txtShadeCode.Enabled = true;
            txtLabDipNo.Enabled = true;
            txtLotNo.SelectedIndex = -1;
            lblTRNYarnSpiningArticalCode.Text = string.Empty;
            txtTRNYarnSpiningDelDate.Text = string.Empty;
            txtTRNYarnSpiningSrinkage.Text = string.Empty;
            txtShadeCode.Text = string.Empty;
            txtLabDipNo.Text = string.Empty;
            lblCRNo.Text = string.Empty;
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
                txtTRNYarnSpiningArticalCode.SelectedIndex = -1;
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
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();

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
            Session["dtTRNYarnSpining"] = dtTRNYarnSpining;
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
            if(Session["dtTRNYarnSpining"]!=null)           
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
           else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();
            DataView dv = new DataView(dtTRNYarnSpining);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {

                ViewState["UNIQUE_ID"] = UNIQUE_ID;
                txtTRNYarnSpiningUOM.Text = dv[0]["UOM"].ToString();
                txtTRNYarnSpiningSrinkage.Text = dv[0]["SRINKAGE"].ToString();
                txtTRNYarnSpiningOrderQty.Text = dv[0]["ORD_QTY"].ToString();
                txtTRNYarnSpiningDelDate.Text = dv[0]["DEL_DATE"].ToString();
                txtTRNYarnSpiningArticalCode.Enabled = false;
                txtShadeCode.Enabled = false;
                txtLabDipNo.Enabled = false;
                lblTRNYarnSpiningArticalCode.Text = dv[0]["ARTICAL_CODE"].ToString();
                lblTRNYSpinDesc.Text = dv[0]["ARTICAL_CODE"].ToString();
                lblpi_no.Text = dv[0]["PI_NO"].ToString();
                lblCRNo.Text = dv[0]["CUST_REQ_NO"].ToString();
                txtTRNYyarnRemarks.Text = dv[0]["REMARKS"].ToString();
                //txtLotID.Text = dv[0]["LOT_ID"].ToString();
                // ddlShadeCode.SelectedIndex = ddlShadeCode.Items.IndexOf(ddlShadeCode.Items.FindByText(dv[0]["SHADE_CODE"].ToString()));
                txtShadeCode.Text = dv[0]["SHADE_CODE"].ToString();
                txtLabDipNo.Text = dv[0]["LAB_DIP_NO"].ToString();
                string CommandText2 = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GREY_LOT_NO'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression2 = " order by MST_CODE asc";
                DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, "", SortExpression2, "", "%", "");
                txtLotNo.DataSource = data2;
                txtLotNo.DataTextField = "MST_CODE";
                txtLotNo.DataValueField = "MST_CODE";
                txtLotNo.DataBind();
                foreach (ComboBoxItem item in txtLotNo.Items)
                {
                    if (item.Text == dv[0]["GREY_LOT_NO"].ToString())
                    {
                        txtLotNo.SelectedIndex = txtLotNo.Items.IndexOf(item);
                        break;
                    }
                }

            }
        }
        catch
        {
            throw;
        }
    }

    private string GetArticalCodeFromString(string sString, out string UOM, out string Description)
    {
        try
        {
            UOM = string.Empty;
            Description = string.Empty;

            char[] splitter = { '@' };
            string[] arrString = sString.Split(splitter);
            string ARTICLE_CODE = arrString[0].ToString();
            Description = arrString[1].ToString();
            UOM = arrString[2].ToString();



            return ARTICLE_CODE;
        }
        catch
        {
            throw;
        }
    }

  

    protected void txtTRNYarnSpiningOrderQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTRNYarnSpiningOrderQty.ReadOnly = true;

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

               
                URL = URL + "?PI_TYPE=" + PI_TYPE;

                string ARTICLE_CODE = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;
                string SHADE_NAME = string.Empty;


                if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                    ARTICLE_CODE = string.Empty;// GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out CUST_REQ_NO, out TKT_NO, out SHADE, out crQty, out SHADE_NAME);
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFINAL_ORDER_CONF_CLAG = (Label)e.Row.FindControl("lblFINAL_ORDER_CONF_CLAG");
                if (lblFINAL_ORDER_CONF_CLAG.Text.Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton lnkbtnDel = (LinkButton)e.Row.FindControl("lnkbtnDel");

                    lnkbtnDel.Visible = false;
                    lnkbtnEdit.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is Gridview RowDataBound. See Error log for detail"));
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
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DateTime date = System.DateTime.Now.Date;
            bool IsPartyPODate = DateTime.TryParse(txtPartyRefDate.Text, out date);
            oOD_CAPTURE_MST.PARTY_REF_DATE = date;
            oOD_CAPTURE_MST.PARTY_REF_NO = txtPartyRefNumber.Text;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;

            if (ddlOrderCategory.SelectedValue.Trim().Equals("CR_CLUB"))
            {
                oOD_CAPTURE_MST.PRTY_CODE = "SELF";
            }
            else
            {
                oOD_CAPTURE_MST.PRTY_CODE = txtPartyCode.Text.Trim();
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


            DataTable dtSewingCustAdj = null;
            if (Session["dtYarnSpinningCustAdj"] != null)
                dtSewingCustAdj = (DataTable)Session["dtYarnSpinningCustAdj"];
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();

            bool result = SaitexBL.Interface.Method.OD_CAPTURE_MST.Insert_Order(oOD_CAPTURE_MST, out sOrderNo, dtTRNYarnSpining, out msg_YRNSPIN, IsPartyPODate, dtSewingCustAdj);
            if (result)
            {
                ClearPage();
                string msg = string.Empty;
                msg += "Order Number : " + sOrderNo + " saved successfully.";

               
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

            if (txtPartyCode.Text != string.Empty)
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
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

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

                if (dt.Rows[0]["ORDER_CAT"].ToString().Trim().Equals("CR_CLUB"))
                {
                    OrdercategorySelection();
                }
                else
                {
                    txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();

                    txtPartyDetail.Text = txtPartyCodecmb.SelectedText.Trim();
                }
            }

            if (iRecordFound == 1)
            {
                //Code For Yarn Spining 
                DataTable dtTemp_YRNSPIN = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetTRN_ByORDER_NO(oOD_CAPTURE_MST);

                if (dtTemp_YRNSPIN != null && dtTemp_YRNSPIN.Rows.Count > 0)
                {
                    DataTable dtADJ = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetADJ_ByORDER_NO(oOD_CAPTURE_MST);
                    if (dtADJ != null & dt.Rows.Count > 0)
                    {
                        Session["dtYarnSpinningCustAdj"] = dtADJ;
                    }


                    MapDataTable_YRNSPIN(dtTemp_YRNSPIN);


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
            if(Session["dtTRNYarnSpining"]!=null)           
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
           else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();
                dtTRNYarnSpining.Rows.Clear();
            
                     

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtTRNYarnSpining.NewRow();

                    dr["UNIQUE_ID"] = dtTRNYarnSpining.Rows.Count + 1;
                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["PI_NO"] = drTemp["PI_NO"];
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["GREY_LOT_NO"] = drTemp["GREY_LOT_NO"];
                    dr["REMARKS"] = drTemp["REMARKS"];
                    dr["UOM"] = drTemp["UOM"];
                    dr["ORD_QTY"] = drTemp["ORD_QTY"];
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"];
                    dr["LAB_DIP_NO"] = drTemp["LAB_DIP_NO"];
                    dr["SRINKAGE"] = drTemp["SRINKAGE"];
                    dr["DEL_DATE"] = drTemp["DEL_DATE"];
                    dr["FINAL_ORDER_CONF_CLAG"] = drTemp["FINAL_ORDER_CONF_CLAG"];
                    dr["CUST_REQ_NO"] = drTemp["CUST_REQ_NO"];
                    
                   
                    dtTRNYarnSpining.Rows.Add(dr);
                    Session["dtTRNYarnSpining"] = dtTRNYarnSpining;

                }
                dtTemp = null;
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable_YRNSPIN_ADJ(DataTable dtTemp)
    {
        try
        {

            if (Session["dtYarnSpinningCustAdj"] != null)
                Session["dtYarnSpinningCustAdj"] = null;

            DataTable dtSewingCustAdj = createAdjTable();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtSewingCustAdj.NewRow();

                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["GREY_LOT_NO"] = drTemp["GREY_LOT_NO"];
                    dr["REMARKS"] = drTemp["REMARKS"];
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

                    dtSewingCustAdj.Rows.Add(dr);
                }
                dtTemp = null;
                Session["dtYarnSpinningCustAdj"] = dtSewingCustAdj;
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
                    dr["GREY_LOT_NO"] = drTemp["GREY_LOT_NO"];
                    dr["REMARKS"] = drTemp["REMARKS"];
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

    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ORDER_NO", typeof(string));
        dt.Columns.Add("ARTICAL_CODE", typeof(string));
        dt.Columns.Add("GREY_LOT_NO", typeof(string));
        dt.Columns.Add("SHADE_CODE", typeof(string));
        dt.Columns.Add("PRODUCT_TYPE", typeof(string));
        dt.Columns.Add("PI_TYPE", typeof(string));
        dt.Columns.Add("PI_NO", typeof(string));
        dt.Columns.Add("CR_COMP_CODE", typeof(string));
        dt.Columns.Add("CR_BRANCH_CODE", typeof(string));
        dt.Columns.Add("CR_YEAR", typeof(int));
        dt.Columns.Add("CR_ORDER_TYPE", typeof(string));
        dt.Columns.Add("CR_ORDER_CAT", typeof(string));
        dt.Columns.Add("CR_PRODUCT_TYPE", typeof(string));
        dt.Columns.Add("CR_BUSINESS_TYPE", typeof(string));
        dt.Columns.Add("CR_ST_ORDER_NO", typeof(string));
        dt.Columns.Add("CR_ST_ARTICLE_NO", typeof(string));
        dt.Columns.Add("CR_ST_SUBSTRATE", typeof(string));
        dt.Columns.Add("CR_ST_COUNT", typeof(string));
        dt.Columns.Add("CR_ST_SHADE_FAMILY_CODE", typeof(string));
        dt.Columns.Add("CR_ST_SHADE_CODE", typeof(string));
        dt.Columns.Add("CR_YRN_COUNT", typeof(int));
        dt.Columns.Add("CR_YRN_PLY", typeof(string));
        dt.Columns.Add("ADJ_QTY", typeof(double));
        return dt;
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
            oOD_CAPTURE_MST.ORDER_NO = txtOrderNo.Text.Trim();
            oOD_CAPTURE_MST.ORDER_PROCESS = ddlOrderProcess.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();

            DateTime date = System.DateTime.Now.Date;
            bool IsPartyPODate = DateTime.TryParse(txtPartyRefDate.Text, out date);
            oOD_CAPTURE_MST.PARTY_REF_DATE = date;
            oOD_CAPTURE_MST.PARTY_REF_NO = txtPartyRefNumber.Text;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            if (ddlOrderCategory.SelectedValue.Trim().Equals("CR_CLUB"))
            {
                oOD_CAPTURE_MST.PRTY_CODE = "SELF";
            }
            else
            {
                oOD_CAPTURE_MST.PRTY_CODE = txtPartyCode.Text.Trim();
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


            DataTable dtSewingCustAdj = null;
            if (Session["dtYarnSpinningCustAdj"] != null)
            {
                dtSewingCustAdj = (DataTable)Session["dtYarnSpinningCustAdj"];
            }
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();

            bool result = SaitexBL.Interface.Method.OD_CAPTURE_MST.Update_Order(oOD_CAPTURE_MST, dtTRNYarnSpining, out msg_YRNSPIN, IsPartyPODate, dtSewingCustAdj);
            if (result)
            {
                ClearPage();
                string msg = string.Empty;
                msg += "Order Number : " + ORDER_NO + " Updated successfully.";

              

                Common.CommonFuction.ShowMessage(msg);

            }
            else
            {
                Common.CommonFuction.ShowMessage("Data Updation Failed");
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

    protected void btnAdjCustReq_Click(object sender, EventArgs e)
    {
        if (lblTRNYarnSpiningArticalCode.Text != string.Empty)
        {

            if (PRODUCT_TYPE == "SEWING THREAD")
            {

                try
                {

                    if (lblTRNYarnSpiningArticalCode.Text != string.Empty && txtShadeCode.Text != string.Empty)
                    {
                        txtTRNYarnSpiningOrderQty.ReadOnly = false;//Code to enable TxtQty for insert

                        string URL = "OCSewingCustAdjustment.aspx";
                        URL = URL + "?YARN_CODE=" + lblTRNYarnSpiningArticalCode.Text.Trim();
                        URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
                        URL = URL + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
                        URL = URL + "&PI_TYPE=" + PI_TYPE;
                        URL = URL + "&txtQTY=" + txtTRNYarnSpiningOrderQty.ClientID;
                        URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.ToString();
                        URL = URL + "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.ToString();
                        URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.ToString();
                        URL = URL + "&ORDER_NO=" + txtOrderNo.Text.Trim();
                        URL = URL + "&CR_NO=" + lblCRNo.Text.Trim();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please select Article Number and Shade Code");
                    }
                }
                catch
                {
                }
            }
            else if (PRODUCT_TYPE == "YARN DYEING")
            {
                try
                {
                    if (lblTRNYarnSpiningArticalCode.Text != string.Empty)
                    {
                        txtTRNYarnSpiningOrderQty.ReadOnly = false;//Code to enable TxtQty for insert

                        string URL = "OCYarnCustAdjustment.aspx";
                        URL = URL + "?YARN_CODE=" + lblTRNYarnSpiningArticalCode.Text.Trim();
                        URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
                        URL = URL + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
                        URL = URL + "&PI_TYPE=" + PI_TYPE;
                        URL = URL + "&txtQTY=" + txtTRNYarnSpiningOrderQty.ClientID;
                        URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.ToString();
                        URL = URL + "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.ToString();
                        URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.ToString();
                        URL = URL + "&ORDER_NO=" + txtOrderNo.Text.Trim();
                        URL = URL + "&CR_NO=" + lblCRNo.Text.Trim();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please select Article Number");
                    }
                }
                catch
                {
                }
            }
            else if (PRODUCT_TYPE == "FABRIC")
            {
                try
                {
                    if (lblTRNYarnSpiningArticalCode.Text != string.Empty)
                    {
                        txtTRNYarnSpiningOrderQty.ReadOnly = false;//Code to enable TxtQty for insert

                        string URL = "OCFabricCustAdjustment.aspx";
                        URL = URL + "?FABR_CODE=" + lblTRNYarnSpiningArticalCode.Text.Trim();
                        URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
                        URL = URL + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
                        URL = URL + "&PI_TYPE=" + PI_TYPE;
                        URL = URL + "&txtQTY=" + txtTRNYarnSpiningOrderQty.ClientID;

                        URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.ToString();
                        URL = URL + "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.ToString();
                        URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.ToString();
                        URL = URL + "&ORDER_NO=" + txtOrderNo.Text.Trim();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please select Article Number");
                    }
                }
                catch
                {
                }
            }
            else if (PRODUCT_TYPE == "TEXTURISED YARN")
            {
                try
                {
                    if (lblTRNYarnSpiningArticalCode.Text != string.Empty)
                    {
                        txtTRNYarnSpiningOrderQty.ReadOnly = false;//Code to enable TxtQty for insert

                        string URL = "OCYarnSpinningCustAdjustment.aspx";
                        URL = URL + "?YARN_CODE=" + lblTRNYarnSpiningArticalCode.Text.Trim();
                        URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
                        URL = URL + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
                        URL = URL + "&PI_TYPE=" + PI_TYPE;
                        URL = URL + "&txtQTY=" + txtTRNYarnSpiningOrderQty.ClientID;
                        URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.ToString();
                        URL = URL + "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.ToString();
                        URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.ToString();
                        URL = URL + "&ORDER_NO=" + txtOrderNo.Text.Trim();
                        URL = URL + "&CR_NO=" + lblCRNo.Text.Trim();
                        URL = URL + "&YEAR=" + oUserLoginDetail.DT_STARTDATE.Year;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please select Article Number");
                    }
                }
                catch
                {
                }
            }
            else if (PRODUCT_TYPE == "TWISTED YARN")
            {
                try
                {
                    if (lblTRNYarnSpiningArticalCode.Text != string.Empty)
                    {
                        txtTRNYarnSpiningOrderQty.ReadOnly = false;//Code to enable TxtQty for insert

                        string URL = "OCYarnSpinningCustAdjustment.aspx";
                        URL = URL + "?YARN_CODE=" + lblTRNYarnSpiningArticalCode.Text.Trim();
                        URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
                        URL = URL + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
                        URL = URL + "&PI_TYPE=" + PI_TYPE;
                        URL = URL + "&txtQTY=" + txtTRNYarnSpiningOrderQty.ClientID;
                        URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.ToString();
                        URL = URL + "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.ToString();
                        URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.ToString();
                        URL = URL + "&ORDER_NO=" + txtOrderNo.Text.Trim();
                        URL = URL + "&CR_NO=" + lblCRNo.Text.Trim();
                        URL = URL + "&YEAR=" + oUserLoginDetail.DT_STARTDATE.Year;

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please select Article Number");
                    }
                }
                catch
                {
                }
            }

            else
            { 
            //do nothing
            }


        }

    }

    protected void txtTRNYarnSpiningArticalCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)    
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtTRNYarnSpiningArticalCode.Items.Clear();
                txtTRNYarnSpiningArticalCode.DataSource = data;
                txtTRNYarnSpiningArticalCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article  loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }
   
    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;
            string whereClause = string.Empty;
            string SortExpression = string.Empty;
            
             if (ddlProductType.SelectedValue.Trim() == "TEXTURISED YARN" || ddlProductType.SelectedValue.Trim() == "TWISTED YARN" || ddlProductType.SelectedValue.Trim() == "YARN DYEING" && ddlOrderCategory.SelectedValue.Trim() =="DIRECT SALE")
            {

                CommandText = "SELECT   *  FROM   (SELECT   * FROM  (SELECT   * FROM  (  SELECT  YA.ASS_YARN_CODE AS ARTICAL_CODE,  YA.ASS_YARN_DESC AS ARTICAL_DESC,    M.YARN_TYPE,    M.COLOUR,ST.SHADE_CODE,           ST.ORDER_NO,YA.ASS_YARN_CODE   || '@'||YA.ASS_YARN_DESC|| '@'|| M.UOM || '@' || M.TRANSFER_PRICE|| '@'|| ST.SHADE_CODE|| '@' || ST.MATCHING_REFF || '@' || ST.QTY_APPROVED|| '@'|| ST.ORDER_NO ||'@'||ST.REMARKS AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0) AS QTYBAL        FROM   YRN_MST M, YRN_ASSOCATED_MST YA,  OD_CUSTOMER_REQUEST_ST st, OD_CUSTOMER_REQUEST_mst A  WHERE   M.YARN_CODE=YA.YARN_CODE AND  st.PRODUCT_TYPE = '" + ddlProductType.SelectedValue + "'  AND A.PRTY_CODE = '" + txtPartyCode.Text + "'  AND A.YEAR=ST.YEAR AND a.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + "    AND YA.ASS_YARN_CODE = ST.ARTICLE_NO AND ST.ORDER_NO = A.ORDER_NO   AND ST.PRODUCT_TYPE = A.PRODUCT_TYPE AND ST.CONF_FLAG = 1 AND ST.ORDER_TYPE NOT IN ('FROM_STOCK') and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0  AND   ( NVL (St.QTY_APPROVED, 0) - NVL (ST.INVOICE_QTY, 0))>0  ORDER BY   M.YARN_CODE)  WHERE   ORDER_NO NOT IN (SELECT   DISTINCT AFT.CR_ST_ORDER_NO  FROM   OD_CAPT_CUST_ADJ AFT ))asd  WHERE asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd ";
                if (startOffset != 0)
                {
                    whereClause += "  AND (ARTICAL_CODE,SHADE_CODE) NOT IN(SELECT   *  FROM   (SELECT   * FROM  (SELECT   * FROM  (  SELECT  YA.ASS_YARN_CODE AS ARTICAL_CODE,  YA.ASS_YARN_DESC AS ARTICAL_DESC,    M.YARN_TYPE,    M.COLOUR,ST.SHADE_CODE,           ST.ORDER_NO,YA.ASS_YARN_CODE   || '@'||YA.ASS_YARN_DESC|| '@'|| M.UOM || '@' || M.TRANSFER_PRICE|| '@'|| ST.SHADE_CODE|| '@' || ST.MATCHING_REFF ||'@'|| ST.QTY_APPROVED|| '@'|| ST.ORDER_NO ||'@'||ST.REMARKS AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0) AS QTYBAL        FROM   YRN_MST M, YRN_ASSOCATED_MST YA,  OD_CUSTOMER_REQUEST_ST st, OD_CUSTOMER_REQUEST_mst A  WHERE   M.YARN_CODE=YA.YARN_CODE AND   m.YARN_TYPE <> 'SEWING THREAD' AND st.PRODUCT_TYPE = '" + ddlProductType.SelectedValue + "'  AND A.PRTY_CODE = '" + txtPartyCode.Text + "'  AND A.YEAR=ST.YEAR AND a.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + "    AND YA.ASS_YARN_CODE = ST.ARTICLE_NO AND ST.ORDER_NO = A.ORDER_NO   AND ST.PRODUCT_TYPE = A.PRODUCT_TYPE AND ST.CONF_FLAG = 1 AND ST.ORDER_TYPE NOT IN ('FROM_STOCK') and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 AND  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.INVOICE_QTY, 0))>0  ORDER BY   M.YARN_CODE) WHERE   ORDER_NO NOT IN (SELECT   DISTINCT AFT.CR_ST_ORDER_NO  FROM   OD_CAPT_CUST_ADJ AFT ))asd  WHERE asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd  WHERE ROWNUM <= " + startOffset + ")";
                }
            }

             if (ddlProductType.SelectedValue.Trim() == "TEXTURISED YARN" || ddlProductType.SelectedValue.Trim() == "TWISTED YARN" || ddlProductType.SelectedValue.Trim() == "YARN DYEING" && ddlOrderCategory.SelectedValue.Trim() == "CR_CLUB")
             {

                 CommandText = "SELECT   *  FROM   (SELECT   * FROM  (SELECT   * FROM  (  SELECT  YA.ASS_YARN_CODE AS ARTICAL_CODE,  YA.ASS_YARN_DESC AS ARTICAL_DESC,    M.YARN_TYPE,    M.COLOUR,ST.SHADE_CODE,           ST.ORDER_NO,YA.ASS_YARN_CODE   || '@'||YA.ASS_YARN_DESC|| '@'|| M.UOM || '@' || M.TRANSFER_PRICE|| '@'|| ST.SHADE_CODE|| '@' || ST.MATCHING_REFF || '@' || ST.QTY_APPROVED|| '@'|| ST.ORDER_NO ||'@'||ST.REMARKS AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0) AS QTYBAL        FROM   YRN_MST M, YRN_ASSOCATED_MST YA,  OD_CUSTOMER_REQUEST_ST st, OD_CUSTOMER_REQUEST_mst A  WHERE   M.YARN_CODE=YA.YARN_CODE AND  st.PRODUCT_TYPE = '" + ddlProductType.SelectedValue + "'   AND A.YEAR=ST.YEAR AND a.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + "    AND YA.ASS_YARN_CODE = ST.ARTICLE_NO AND ST.ORDER_NO = A.ORDER_NO   AND ST.PRODUCT_TYPE = A.PRODUCT_TYPE AND ST.CONF_FLAG = 1 AND ST.ORDER_TYPE NOT IN ('FROM_STOCK') and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0  AND   ( NVL (St.QTY_APPROVED, 0) - NVL (ST.INVOICE_QTY, 0))>0  ORDER BY   M.YARN_CODE)  WHERE   ORDER_NO NOT IN (SELECT   DISTINCT AFT.CR_ST_ORDER_NO  FROM   OD_CAPT_CUST_ADJ AFT ))asd  WHERE asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd ";
                 if (startOffset != 0)
                 {
                     whereClause += "  AND (ARTICAL_CODE,SHADE_CODE) NOT IN(SELECT   *  FROM   (SELECT   * FROM  (SELECT   * FROM  (  SELECT  YA.ASS_YARN_CODE AS ARTICAL_CODE,  YA.ASS_YARN_DESC AS ARTICAL_DESC,    M.YARN_TYPE,    M.COLOUR,ST.SHADE_CODE,           ST.ORDER_NO,YA.ASS_YARN_CODE   || '@'||YA.ASS_YARN_DESC|| '@'|| M.UOM || '@' || M.TRANSFER_PRICE|| '@'|| ST.SHADE_CODE|| '@' || ST.MATCHING_REFF ||'@'|| ST.QTY_APPROVED|| '@'|| ST.ORDER_NO ||'@'||ST.REMARKS AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0) AS QTYBAL        FROM   YRN_MST M, YRN_ASSOCATED_MST YA,  OD_CUSTOMER_REQUEST_ST st, OD_CUSTOMER_REQUEST_mst A  WHERE   M.YARN_CODE=YA.YARN_CODE AND   m.YARN_TYPE <> 'SEWING THREAD' AND st.PRODUCT_TYPE = '" + ddlProductType.SelectedValue + "'  AND A.YEAR=ST.YEAR AND a.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + "    AND YA.ASS_YARN_CODE = ST.ARTICLE_NO AND ST.ORDER_NO = A.ORDER_NO   AND ST.PRODUCT_TYPE = A.PRODUCT_TYPE AND ST.CONF_FLAG = 1 AND ST.ORDER_TYPE NOT IN ('FROM_STOCK') and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 AND  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.INVOICE_QTY, 0))>0  ORDER BY   M.YARN_CODE) WHERE   ORDER_NO NOT IN (SELECT   DISTINCT AFT.CR_ST_ORDER_NO  FROM   OD_CAPT_CUST_ADJ AFT ))asd  WHERE asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd  WHERE ROWNUM <= " + startOffset + ")";
                 }
             }
           else if (ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
            {
                CommandText = "SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE as ARTICAL_CODE, M.YARN_DESC as ARTICAL_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE  YARN_TYPE = 'SEWING THREAD' AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE   asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd WHERE   ROWNUM <= 15  ";

                if (startOffset != 0)
                {
                    whereClause += "  AND (ARTICAL_CODE,SHADE_CODE) NOT IN(SELECT   YARN_code as ARTICAL_CODE,SHADE_CODE FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR,           ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE || '@' || ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0)    AS QUANTITY, NVL (ST.QTY_APPROVED, 0)    AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL     FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st    WHERE   m.YARN_TYPE = 'SEWING THREAD'         AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY M.YARN_CODE) asd WHERE asd.YARN_CODE LIKE :SearchQuery OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd WHERE  ROWNUM <= " + startOffset + ")";
                }
            }
            else if (ddlProductType.SelectedValue.Trim() == "FABRIC")
            {
                CommandText = "SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.FABR_CODE as ARTICAL_CODE, M.FABR_DESC as ARTICAL_DESC, M.FABR_TYPE,   ST.SHADE_CODE,ST.ORDER_NO,    M.FABR_CODE || '@' || M.FABR_DESC || '@' || M.UOM || '@' || M.TRANSFER_RATE || '@' || M.FABR_TYPE || '@' || ST.QTY_APPROVED || '@' || ST.ORDER_NO   AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   TX_FABRIC_MST M, OD_CUSTOMER_REQUEST_ST st            WHERE        M.FABR_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 AND (NVL (St.QTY_APPROVED, 0)      - NVL (ST.ADJUST_QTY, 0)) > 0         ORDER BY   M.FABR_CODE) asd           WHERE      asd.ARTICAL_CODE LIKE :SearchQuery        OR asd.FABR_TYPE LIKE :SearchQuery        OR asd.ARTICAL_DESC LIKE :SearchQuery        OR asd.ORDER_NO LIKE :SearchQuery) bd WHERE   ROWNUM <= 15 ";

                if (startOffset != 0)
                {
                    whereClause += "  AND (ARTICAL_CODE, SHADE_CODE) NOT IN (SELECT   FABR_CODE as ARTICAL_CODE, SHADE_CODE   FROM   (SELECT   * FROM   (  SELECT   M.FABR_CODE,M.FABR_DESC, M.FABR_TYPE,  ST.SHADE_CODE,   ST.ORDER_NO,    M.FABR_CODE || '@' || M.FABR_DESC || '@'|| M.UOM || '@' || M.TRANSFER_PRICE || '@' || M.FABR_TYPE || '@' || ST.QTY_APPROVED || '@' || ST.ORDER_NO     AS Combined, NVL (ST.QUANTITY, 0)   AS QUANTITY, NVL (ST.QTY_APPROVED, 0)    AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL             FROM   TX_FABRIC_MST M, OD_CUSTOMER_REQUEST_ST st            WHERE    M.FABR_CODE =       ST.ARTICLE_NO AND ST.CONF_FLAG = 1 AND (NVL (St.QTY_APPROVED, 0)      - NVL (ST.ADJUST_QTY, 0)) >       0        ORDER BY   M.FABR_CODE) asd           WHERE      asd.FABR_CODE LIKE :SearchQuery        OR asd.FABR_TYPE LIKE :SearchQuery        OR asd.FABR_DESC LIKE :SearchQuery        OR asd.ORDER_NO LIKE :SearchQuery) bd WHERE   ROWNUM <= " + startOffset + ")  ";
                }
            }
           
          SortExpression = " ORDER BY ARTICAL_CODE";

            
            string SearchQuery = "%" + text.ToUpper() + "%";
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
        string CommandText = string.Empty;
        string SortExpression = string.Empty;
        if (ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
        {
            CommandText = "  SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE  YARN_TYPE = 'SEWING THREAD' AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE   asd.YARN_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd ";
        }
        else if (ddlProductType.SelectedValue.Trim() == "YARN DYEING")
        {
            CommandText = " SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE   m.YARN_TYPE <> 'SEWING THREAD' AND   M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE      asd.YARN_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd ";
        }
        else if (ddlProductType.SelectedValue.Trim() == "YARN DYEING" && ddlOrderCategory.SelectedValue.Trim() == "CR_CLUB")
        {
            CommandText = " SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE   m.YARN_TYPE <> 'SEWING THREAD' AND   M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE      asd.YARN_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd ";
        }
        else if (ddlProductType.SelectedValue.Trim() == "FABRIC")
        {
            CommandText = " SELECT   *  FROM   (SELECT   *            FROM   (  SELECT   M.FABR_CODE, M.FABR_DESC, M.FABR_TYPE, ST.ORDER_NO,    M.FABR_CODE || '@' || M.FABR_DESC || '@' || M.UOM || '@' || M.TRANSFER_RATE || '@' || M.FABR_TYPE || '@' ||  ST.ORDER_NO   AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL FROM   TX_FABRIC_MST M, OD_CUSTOMER_REQUEST_ST st WHERE       M.FABR_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 AND (NVL (St.QTY_APPROVED, 0)      - NVL (ST.ADJUST_QTY, 0)) > 0 ORDER BY   M.FABR_CODE) asd WHERE      asd.FABR_CODE LIKE :SearchQuery OR asd.FABR_TYPE LIKE :SearchQuery OR asd.FABR_DESC LIKE :SearchQuery) bd  ";
        }
        string WhereClause = " ";
        if (ddlProductType.SelectedValue.Trim() == "FABRIC")
        {
            SortExpression = " ORDER BY FABR_CODE";
        }
        else if (ddlProductType.SelectedValue.Trim() == "YARN DYEING" || ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
        {
            SortExpression = " ORDER BY YARN_code";

        }
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    //protected DataTable GetItems(string text, int startOffset)
    //{
    //    try
    //    {
    //        string CommandText = string.Empty;
    //        string whereClause = string.Empty;
    //        string SortExpression = string.Empty;
    //        if (ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
    //        {
    //            CommandText = " SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE  YARN_TYPE = 'SEWING THREAD' AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE   asd.YARN_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd WHERE   ROWNUM <= 15  ";

    //            if (startOffset != 0)
    //            {
    //                whereClause += "  AND (YARN_code,SHADE_CODE) NOT IN(SELECT   YARN_code,SHADE_CODE FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR,           ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE || '@' || ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0)    AS QUANTITY, NVL (ST.QTY_APPROVED, 0)    AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL     FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st    WHERE   m.YARN_TYPE = 'SEWING THREAD'         AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY M.YARN_CODE) asd WHERE asd.YARN_CODE LIKE :SearchQuery OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd WHERE  ROWNUM <= " + startOffset + ")";
    //            }
    //        }
    //        else if (ddlProductType.SelectedValue.Trim() == "YARN DYEING")
    //        {
    //            CommandText = " SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE AS ARTICAL_CODE, M.YARN_DESC AS ARTICAL_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE   m.YARN_TYPE <> 'SEWING THREAD' AND   M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd WHERE ROWNUM <= 15  ";

    //            if (startOffset != 0)
    //            {
    //                whereClause += "  AND (YARN_code,SHADE_CODE) NOT IN(SELECT   YARN_code,SHADE_CODE FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR,           ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE || '@' || ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0)    AS QUANTITY, NVL (ST.QTY_APPROVED, 0)    AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL     FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st    WHERE    m.YARN_TYPE <> 'SEWING THREAD'         AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd WHERE asd.YARN_CODE LIKE :SearchQuery OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd WHERE ROWNUM <= " + startOffset + ")";
    //            }
    //        }
    //        else if (ddlProductType.SelectedValue.Trim() == "FABRIC")
    //        {
    //            CommandText = " SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.FABR_CODE, M.FABR_DESC, M.FABR_TYPE,  ST.SHADE_CODE,    M.FABR_CODE || '@' || M.FABR_DESC || '@' || M.UOM || '@' || M.TRANSFER_RATE || '@' || ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   TX_FABRIC_MST M, OD_CUSTOMER_REQUEST_ST st            WHERE        M.FABR_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 AND (NVL (St.QTY_APPROVED, 0)      - NVL (ST.ADJUST_QTY, 0)) > 0         ORDER BY   M.FABR_CODE) asd           WHERE      asd.FABR_CODE LIKE :SearchQuery        OR asd.FABR_TYPE LIKE :SearchQuery        OR asd.FABR_DESC LIKE :SearchQuery        OR asd.SHADE_CODE LIKE :SearchQuery) bd WHERE   ROWNUM <= 15  ";

    //            if (startOffset != 0)
    //            {
    //                whereClause += "  AND (FABR_CODE, SHADE_CODE) NOT IN (SELECT   FABR_CODE, SHADE_CODE   FROM   (SELECT   * FROM   (  SELECT   M.FABR_CODE,M.FABR_DESC, M.FABR_TYPE,   ST.SHADE_CODE,    M.FABR_CODE || '@' || M.FABR_DESC || '@'|| M.UOM || '@' || M.TRANSFER_PRICE || '@' || ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0)   AS QUANTITY, NVL (ST.QTY_APPROVED, 0)    AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL             FROM   TX_FABRIC_MST M, OD_CUSTOMER_REQUEST_ST st            WHERE    M.FABR_CODE =       ST.ARTICLE_NO AND ST.CONF_FLAG = 1 AND (NVL (St.QTY_APPROVED, 0)      - NVL (ST.ADJUST_QTY, 0)) >       0        ORDER BY   M.FABR_CODE) asd           WHERE      asd.FABR_CODE LIKE :SearchQuery        OR asd.FABR_TYPE LIKE :SearchQuery        OR asd.FABR_DESC LIKE :SearchQuery        OR asd.SHADE_CODE LIKE :SearchQuery) bd WHERE   ROWNUM <= " + startOffset + ")  ";
    //            }
    //        }
    //        if (ddlProductType.SelectedValue.Trim() == "FABRIC")
    //        {
    //            SortExpression = " ORDER BY FABR_CODE";
    //        }
    //        else if (ddlProductType.SelectedValue.Trim() == "YARN DYEING" || ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
    //        {
    //            SortExpression = " ORDER BY ARTICAL_code";
            
    //        }
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected void txtTRNYarnSpiningArticalCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
   {
        try
        {
            string cString = txtTRNYarnSpiningArticalCode.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string YARN_CODE = arrString[0].ToString();
            string YARN_DESC = arrString[1].ToString();
            string UOM = arrString[2].ToString();
            //string TRANSFER_PRICE = arrString[3].ToString();
            string shadeCode = arrString[4].ToString();
            string LABDIPNO = arrString[5].ToString();
            string CRNO=arrString[7].ToString();
            string REMARKS = arrString[8].ToString();
           // string approvedQty=arrString[5].ToString();
           // string LotId="";
            lblTRNYarnSpiningArticalCode.Text = YARN_CODE;
            lblTRNYSpinDesc.Text = YARN_DESC;
            txtTRNYarnSpiningUOM.Text = UOM;
            txtShadeCode.Text = shadeCode;
            txtLabDipNo.Text = LABDIPNO;
            lblCRNo.Text = CRNO;
            txtTRNYyarnRemarks.Text = REMARKS;
           // txtTRNYarnSpiningOrderQty.Text = approvedQty;
            //GenerateLotId();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Article Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

    //protected void GenerateLotId()
    //{

    //    oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

    //    oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //    oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
    //    oOD_CAPTURE_MST.ORDER_NO = txtOrderNo.Text;
    //    string PI_NO = lblpi_no.Text;
    //    //oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
    //    oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;


   //     string LotId = SaitexBL.Interface.Method.OD_CAPTURE_MST.GenerateLotId(oOD_CAPTURE_MST, PI_NO);
    //    //txtLotID.Text = LotId;
 
    //}

    protected void btnTrnSave_Click(object sender, EventArgs e)
    {
        SaveTRNYarnSpiningRow();
    }

    protected void btnTrnCancel_Click(object sender, EventArgs e)
    {
        RefreshTRNYarnSpiningRow();
    }

    protected void txtPartyCodecmb_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCodecmb.Items.Clear();
            txtPartyCodecmb.DataSource = data;
            txtPartyCodecmb.DataBind();
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
           // string CommandText = "SELECT   DISTINCT * FROM   (SELECT DISTINCT PRTY_CODE, ( PRTY_NAME||  PRTY_ADD1)ADDRESS,  PRTY_GRP_CODE,VENDOR_CAT_CODE  FROM   (  SELECT   DISTINCT B.PRTY_CODE,  A.PRTY_NAME,    A.PRTY_ADD1,    A.PRTY_GRP_CODE,A.VENDOR_CAT_CODE,      B.ORDER_NO    FROM   TX_VENDOR_MST A,     OD_CUSTOMER_REQUEST_MST B,   OD_CUSTOMER_REQUEST_ST C     WHERE   (B.PRTY_CODE LIKE :SearchQuery      OR A.PRTY_NAME LIKE :SearchQuery)   AND A.PRTY_CODE = B.PRTY_CODE   AND B.ORDER_NO = C.ORDER_NO     AND B.PRODUCT_TYPE = '"+ ddlProductType.SelectedValue +"'       AND C.CONF_FLAG = '1'    ORDER BY   PRTY_CODE ASC)   WHERE   ORDER_NO NOT IN (SELECT   DISTINCT AFT.ORDER_NO     FROM   OD_CAPT_MST AFT       WHERE   AFT.STATUS = '1')) asd WHERE   UPPER (PRTY_GRP_CODE) IN  (UPPER ('Yarn'),UPPER ('INNOVATIVE')) and UPPER(VENDOR_CAT_CODE) IN (UPPER ('PARTY'),UPPER ('DEPOT'))  AND ROWNUM <= 15";
            string CommandText = "SELECT   DISTINCT  B.PRTY_CODE, A.PRTY_NAME, A.PRTY_ADD1,  (A.PRTY_NAME || A.PRTY_ADD1) ADDRESS,A.PRTY_GRP_CODE, A.VENDOR_CAT_CODE    FROM   TX_VENDOR_MST A,   OD_CUSTOMER_REQUEST_MST B,  OD_CUSTOMER_REQUEST_ST C  WHERE   (B.PRTY_CODE LIKE :SearchQuery       OR A.PRTY_NAME LIKE :SearchQuery)  AND A.PRTY_CODE = B.PRTY_CODE       AND B.ORDER_NO = C.ORDER_NO      AND C.CONF_FLAG = '1' AND C.ORDER_TYPE NOT IN ('FROM_STOCK') AND  UPPER (PRTY_GRP_CODE) IN (UPPER ('18'))     AND UPPER (VENDOR_CAT_CODE) IN (UPPER ('DOMESTIC CUSTOMER'))    AND B.PRODUCT_TYPE =  '" + ddlProductType.SelectedValue + "'         AND  B.ORDER_NO  IN (  SELECT   DISTINCT AFT.ORDER_NO       FROM   OD_CUSTOMER_REQUEST_ST AFT         WHERE  ( NVL(AFT.QTY_APPROVED,0) - NVL(AFT.ADJUST_QTY,0))>0    )   AND ROWNUM <= 15 ";
           
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
               // whereClause += " AND PRTY_CODE NOT IN (SELECT   DISTINCT * FROM   (SELECT       DISTINCT PRTY_CODE, ( PRTY_NAME||  PRTY_ADD1)ADDRESS,  PRTY_GRP_CODE,VENDOR_CAT_CODE  FROM   (  SELECT   DISTINCT B.PRTY_CODE,  A.PRTY_NAME,    A.PRTY_ADD1,    A.PRTY_GRP_CODE,A.VENDOR_CAT_CODE,      B.ORDER_NO    FROM   TX_VENDOR_MST A,     OD_CUSTOMER_REQUEST_MST B,   OD_CUSTOMER_REQUEST_ST C     WHERE   (B.PRTY_CODE LIKE :SearchQuery      OR A.PRTY_NAME LIKE :SearchQuery)   AND A.PRTY_CODE = B.PRTY_CODE   AND B.ORDER_NO = C.ORDER_NO     AND B.PRODUCT_TYPE = '" + ddlProductType.SelectedValue + "'       AND C.CONF_FLAG = '1'    ORDER BY   PRTY_CODE ASC)   WHERE   ORDER_NO NOT IN (SELECT   DISTINCT AFT.ORDER_NO     FROM   OD_CAPT_MST AFT       WHERE   AFT.STATUS = '1')) asd WHERE   UPPER (PRTY_GRP_CODE) IN  (UPPER ('Yarn'),UPPER ('INNOVATIVE'))  and UPPER(VENDOR_CAT_CODE) IN (UPPER ('PARTY'),UPPER ('DEPOT'))  and ROWNUM <= " + startOffset + ")";
                whereClause += " AND PRTY_CODE NOT IN (SELECT   DISTINCT  B.PRTY_CODE, A.PRTY_NAME, A.PRTY_ADD1,  (A.PRTY_NAME || A.PRTY_ADD1) ADDRESS,A.PRTY_GRP_CODE, A.VENDOR_CAT_CODE    FROM   TX_VENDOR_MST A,   OD_CUSTOMER_REQUEST_MST B,  OD_CUSTOMER_REQUEST_ST C  WHERE   (B.PRTY_CODE LIKE :SearchQuery       OR A.PRTY_NAME LIKE :SearchQuery)  AND A.PRTY_CODE = B.PRTY_CODE       AND B.ORDER_NO = C.ORDER_NO      AND C.CONF_FLAG = '1' AND C.ORDER_TYPE NOT IN ('FROM_STOCK') AND  UPPER (PRTY_GRP_CODE) IN (UPPER ('18'))     AND UPPER (VENDOR_CAT_CODE) IN (UPPER ('DOMESTIC CUSTOMER'))    AND B.PRODUCT_TYPE =  '" + ddlProductType.SelectedValue + "'         AND  B.ORDER_NO  IN (  SELECT   DISTINCT AFT.ORDER_NO       FROM   OD_CUSTOMER_REQUEST_ST AFT         WHERE  ( NVL(AFT.QTY_APPROVED,0) - NVL(AFT.ADJUST_QTY,0))>0     ) and ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT   DISTINCT  B.PRTY_CODE, A.PRTY_NAME, A.PRTY_ADD1,  (A.PRTY_NAME || A.PRTY_ADD1) ADDRESS,A.PRTY_GRP_CODE, A.VENDOR_CAT_CODE     FROM   TX_VENDOR_MST A,   OD_CUSTOMER_REQUEST_MST B,  OD_CUSTOMER_REQUEST_ST C  WHERE   (B.PRTY_CODE LIKE :SearchQuery       OR A.PRTY_NAME LIKE :SearchQuery)  AND A.PRTY_CODE = B.PRTY_CODE       AND B.ORDER_NO = C.ORDER_NO AND C.CONF_FLAG = '1' AND C.ORDER_TYPE NOT IN ('FROM_STOCK') AND  UPPER (PRTY_GRP_CODE) IN (UPPER ('18'))     AND UPPER (VENDOR_CAT_CODE) IN (UPPER ('DOMESTIC CUSTOMER'))    AND B.PRODUCT_TYPE =  '" + ddlProductType.SelectedValue + "'         AND  B.ORDER_NO  IN (  SELECT   DISTINCT AFT.ORDER_NO       FROM   OD_CUSTOMER_REQUEST_ST AFT         WHERE  ( NVL(AFT.QTY_APPROVED,0) - NVL(AFT.ADJUST_QTY,0))>0 ) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count; ;

        
    }

    protected void txtPartyCodecmb_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
           
            txtPartyCode.Text = txtPartyCodecmb.SelectedText.ToString();
            txtPartyDetail.Text = txtPartyCodecmb.SelectedValue.ToString();
            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlShadeCode_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlCurrencyCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOTData(e.Text.ToUpper(), e.ItemsOffset, "GREY_LOT_NO");
            txtLotNo.Items.Clear();
            txtLotNo.DataSource = data;
            txtLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetLOTCount(e.Text, "GREY_LOT_NO");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Merge No in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetLOTData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))     AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";//  AND OTHER_INFO LIKE '" + lblTRNYarnSpiningArticalCode.Text + "'  
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND MST_CODE NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))        AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";//AND OTHER_INFO LIKE '" + lblTRNYarnSpiningArticalCode.Text + "' 
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

    protected int GetLOTCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))       AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";// AND OTHER_INFO LIKE '" + lblTRNYarnSpiningArticalCode.Text + "' 
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

}
