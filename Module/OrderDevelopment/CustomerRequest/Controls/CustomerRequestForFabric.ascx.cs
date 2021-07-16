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
public partial class Module_OrderDevelopment_CustomerRequest_Controls_CustomerRequestForFabric : System.Web.UI.UserControl
{

    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtOrderFabric;
    private static string BussinessType;
    private static string ProductType = "FABRIC";
    private static string OrderType;
    private static string OrderCat;
    private static string CompanyCode = string.Empty;
    private static string BranchCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        CompanyCode = oUserLoginDetail.COMP_CODE;
        BranchCode = oUserLoginDetail.CH_BRANCHCODE;
        if (!IsPostBack)
        {
            InitialiseData();
        }
    }

    private void InitialiseData()
    {
        lblMode.Text = "Save";
        tdSave.Visible = true;
        tdUpdate.Visible = false;
        tdDelete.Visible = false;

        txtDate.Text = System.DateTime.Now.ToShortDateString();
        DDLBusinessType.SelectedIndex = -1;
        DDLOrderCat.SelectedIndex = -1;
        DDLOrderType.SelectedIndex = -1;
        cmbOrderNo.Visible = false;
        txtOrderNo.Visible = true;

        Bind_Control(DDLBusinessType, "BUSINESS_TYPE");
        Bind_Control(DDLOrderType, "ORDER_TYPE");
        Bind_Control(cmbDeliveryMode, "DELIVERY_MODE");
        Bind_Control(DDLOrderCat, "ORDER_CAT");

        bindDesignNo();



        txtOrderNo.Text = string.Empty;
        bindcmbPartyCode();
        txtAddress.Text = string.Empty;
        cmbPartyCode.SelectedIndex = -1;
        txtCustomerReffNo.Text = string.Empty;
        txtAgent.Text = string.Empty;
        txtDirectBilling.Text = string.Empty;
        cmbDeliveryMode.SelectedIndex = -1;
        BlankTrnControls();
    }

    private void bindcmbPartyCode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectParty();
            cmbPartyCode.DataSource = dt;
            cmbPartyCode.DataValueField = "PRTY_CODE";
            cmbPartyCode.DataTextField = "PRTY_CODE";
            cmbPartyCode.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BlankTrnControls()
    {
        try
        {
            cmbDesignNo.SelectedIndex = -1;
            txtCollection.Text = string.Empty;
            ddlColour.SelectedIndex = -1;
            txtMatchingReff.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtRollSize.Text = string.Empty;
            txtEndUse.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void Bind_Control(DropDownList DDL, string MST_NAME)
    {
        try
        {
            DDL.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, CompanyCode);
            DDL.DataSource = dt;
            DDL.DataTextField = "MST_DESC";
            DDL.DataValueField = "MST_CODE";
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("--------Select-------"));
        }
        catch
        {
            throw;
        }
    }

    private void bindColor(int DESIGN_NO)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectColor(DESIGN_NO);
            ddlColour.DataSource = dt;
            ddlColour.DataValueField = "COLOR_NAME";
            ddlColour.DataTextField = "COLOR_NAME";
            ddlColour.DataBind();
            ddlColour.Items.Insert(0, "Select");
        }

        catch (Exception ex)
        {
            throw ex;

        }

    }

    private void bindDesignNo()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectDesignNo();
            cmbDesignNo.DataSource = dt;
            cmbDesignNo.DataValueField = "DESIGN_NO";
            cmbDesignNo.DataTextField = "DESIGN_NO";
            cmbDesignNo.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void bindOrderNo()
    {
        try
        {

            DataTable dt = new DataTable();
            string OrderNo = txtOrderNo.Text.Trim();
            dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectFabricCustomerRqstMst(OrderNo);
            cmbOrderNo.DataSource = dt;
            cmbOrderNo.DataValueField = "ORDER_NO";
            cmbOrderNo.DataTextField = "ORDER_NO";
            cmbOrderNo.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }

    private bool ValidateRecipeMasterRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;

            int count = 0;
            int msgCount = 1;
            if (DDLBusinessType.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select cmbBusiness Type. ";
                msgCount += 1;
            }

            if (DDLOrderType.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Order Type. ";
                msgCount += 1;
            }
            if (DDLOrderCat.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Product Type. ";
                msgCount += 1;
            }
            if (cmbPartyCode.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Customer Name. ";
                msgCount += 1;
            }
            if (txtAddress.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Address. ";
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

    private bool ValidateFabricRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;
            int count = 0;
            int msgCount = 1;
            if (cmbDesignNo.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ":Please select Design No.";
                msgCount += 1;
            }
            if (txtMatchingReff.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ":Please select Matching Reff.";
                msgCount += 1;
                txtMatchingReff.Text = string.Empty;
            }
            double dd = 0;
            double.TryParse(txtQuantity.Text.Trim(), out dd);
            if (dd > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ":Please Enter Quantity/Valid Quantity.";
                msgCount += 1;
                txtQuantity.Text = string.Empty;
            }
            if (txtRollSize.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ":Please select Roll Size.";
                msgCount += 1;
                txtRollSize.Text = string.Empty;
            }
            if (txtEndUse.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ":Please select End Use.";
                msgCount += 1;
                txtEndUse.Text = string.Empty;
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

    private void Insertdata()
    {
        try
        {
            bool bResult = false;
            string msg = string.Empty;
            if (ValidateRecipeMasterRow(out msg))
            {

                if ((dtOrderFabric != null && dtOrderFabric.Rows.Count > 0))
                {
                    SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();
                    oOD_CUSTOMER_RQST_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oOD_CUSTOMER_RQST_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oOD_CUSTOMER_RQST_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oOD_CUSTOMER_RQST_MST.ORDER_NO = txtOrderNo.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.ORDER_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                    oOD_CUSTOMER_RQST_MST.ORDER_REFF_NO = txtCustomerReffNo.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.ORDER_CAT = DDLOrderCat.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.ORDER_TYPE = DDLOrderType.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = DDLBusinessType.SelectedValue.Trim();

                    oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = ProductType.Trim().ToString();
                    oOD_CUSTOMER_RQST_MST.ADDRESS = txtAddress.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = cmbPartyCode.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = cmbDeliveryMode.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.TUSER = Session["urLoginId"].ToString().Trim();
                    bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.InsertCustomerRequestForFabric(oOD_CUSTOMER_RQST_MST, dtOrderFabric);

                    if (bResult == true)
                    {
                        InitialiseData();
                        BlankTrnControls();
                        dtOrderFabric.Rows.Clear();
                        bindFabricGrid();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CreateFabricDataTable()
    {
        try
        {
            dtOrderFabric = new DataTable();
            dtOrderFabric.Columns.Add("UNIQUE_ID", typeof(int));
            dtOrderFabric.Columns.Add("ORDER_NO", typeof(string));
            dtOrderFabric.Columns.Add("DESIGN_NO", typeof(string));
            dtOrderFabric.Columns.Add("COLLECTION_NAME", typeof(string));
            dtOrderFabric.Columns.Add("COLOR", typeof(string));
            dtOrderFabric.Columns.Add("MATCHING_REFF", typeof(string));
            dtOrderFabric.Columns.Add("QUANTITY", typeof(int));
            dtOrderFabric.Columns.Add("ROLL_SIZE", typeof(string));
            dtOrderFabric.Columns.Add("END_USE", typeof(string));
            dtOrderFabric.Columns.Add("REMARKS", typeof(string));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InsertFabricRow()
    {
        try
        {

            if (dtOrderFabric == null)
            {
                CreateFabricDataTable();
            }

            DataRow dr = dtOrderFabric.NewRow();
            if (dtOrderFabric.Rows.Count > 0)
            {
                dr["UNIQUE_ID"] = dtOrderFabric.Rows.Count + 1;
                ViewState["UNIQUE_ID"] = dr["UNIQUE_ID"].ToString();
                dtOrderFabric.Rows.Add(dr);
            }

            bindFabricGrid();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void bindFabricGrid()
    {
        try
        {
            if (dtOrderFabric == null)
                InsertFabricRow();
            GridFabric.DataSource = dtOrderFabric;
            GridFabric.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Record\n\r Please Check error log"));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Record\n\r Please Check error log"));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Record\n\r Please Check error log"));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit Page\n\r Please Check error log"));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cmbOrderNo.SelectedIndex = -1;
            cmbOrderNo.Visible = true;
            txtOrderNo.Visible = false;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnFabricSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFabricRow(out msg))
            {
                if (dtOrderFabric == null)
                {
                    CreateFabricDataTable();
                }
                int UniqueId = 0;
                if (ViewState["UNIQUE_ID"] != null)

                    UniqueId = int.Parse(ViewState["UNIQUE_ID"].ToString());

                if (UniqueId > 0 && dtOrderFabric.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtOrderFabric);
                    dv.RowFilter = "UNIQUE_ID=" + UniqueId;
                    if (dv.Count > 0)
                    {
                        dv[0]["ORDER_NO"] = txtOrderNo.Text.Trim();
                        dv[0]["DESIGN_NO"] = Convert.ToInt32(cmbDesignNo.SelectedText.Trim());
                        dv[0]["COLLECTION_NAME"] = txtCollection.Text.Trim();
                        dv[0]["COLOR"] = ddlColour.SelectedValue.Trim();
                        dv[0]["MATCHING_REFF"] = txtMatchingReff.Text.Trim();
                        dv[0]["QUANTITY"] = txtQuantity.Text.Trim();
                        dv[0]["ROLL_SIZE"] = txtRollSize.Text.Trim();
                        dv[0]["END_USE"] = txtEndUse.Text.Trim();
                        dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                    }

                    dtOrderFabric.AcceptChanges();
                }
                else
                {
                    dtOrderFabric.Rows.Clear();
                    DataRow dr = dtOrderFabric.NewRow();
                    dr["UNIQUE_ID"] = dtOrderFabric.Rows.Count + 1;
                    dr["ORDER_NO"] = txtOrderNo.Text.Trim();
                    dr["DESIGN_NO"] = Convert.ToInt32(cmbDesignNo.SelectedText.Trim());
                    dr["COLLECTION_NAME"] = txtCollection.Text.Trim();
                    dr["COLOR"] = ddlColour.SelectedValue.Trim();
                    dr["MATCHING_REFF"] = txtMatchingReff.Text.Trim();
                    dr["QUANTITY"] = txtQuantity.Text.Trim();
                    dr["ROLL_SIZE"] = txtRollSize.Text.Trim();
                    dr["END_USE"] = txtEndUse.Text.Trim();
                    dr["REMARKS"] = txtRemarks.Text.Trim();
                    dtOrderFabric.Rows.Add(dr);
                }

                GridFabric.DataSource = dtOrderFabric;
                GridFabric.DataBind();
                BlankTrnControls();


            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void DDLOrderCat_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            BussinessType = DDLBusinessType.SelectedValue.Trim();
            OrderType = DDLOrderType.SelectedValue.Trim();
            OrderCat = DDLOrderCat.SelectedValue.Trim();
            if (DDLBusinessType.SelectedIndex >= 0 && DDLOrderCat.SelectedIndex >= 0)
            {
                string strNewOrderNo = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetNewFabricOrderNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BussinessType, OrderCat);
                txtOrderNo.Text = strNewOrderNo.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void cmbOrderNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dtMst = new DataTable();
            DataTable dtFabric = new DataTable();
            string OrderNumber = cmbOrderNo.SelectedValue.Trim();
            dtMst = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectFabricCustomerRqstMst(OrderNumber);
            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                BindControls(dtMst);
                dtFabric = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstFabric(OrderNumber);
                if (dtFabric != null && dtFabric.Rows.Count > 0)
                {
                    MapTableFabric(dtFabric);
                    BindFabricTranasaction();
                }

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }

    }

    private void MapTableFabric(DataTable dtFabric)
    {
        try
        {
            if (dtOrderFabric == null || dtOrderFabric.Rows.Count == 0)
            {
                CreateFabricDataTable();
            }
            dtOrderFabric.Rows.Clear();
            if (dtFabric != null && dtFabric.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFabric.Rows)
                {
                    DataRow drFabric = dtOrderFabric.NewRow();
                    drFabric["UNIQUE_ID"] = dtOrderFabric.Rows.Count + 1;
                    drFabric["ORDER_NO"] = dr["ORDER_NO"];
                    drFabric["DESIGN_NO"] = dr["DESIGN_NO"];
                    drFabric["COLLECTION_NAME"] = dr["COLLECTION_NAME"];
                    drFabric["COLOR"] = dr["COLOR"];
                    drFabric["MATCHING_REFF"] = dr["MATCHING_REFF"];
                    drFabric["DESIGN_NO"] = dr["DESIGN_NO"];
                    drFabric["QUANTITY"] = dr["QUANTITY"];
                    drFabric["ROLL_SIZE"] = dr["ROLL_SIZE"];
                    drFabric["END_USE"] = dr["END_USE"];
                    drFabric["REMARKS"] = dr["REMARKS"];
                    dtOrderFabric.Rows.Add(drFabric);

                }
                dtFabric = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFabricControls(DataTable dt)
    {
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbDesignNo.SelectedText = dt.Rows[0]["DESIGN_NO"].ToString();
                txtCollection.Text = dt.Rows[0]["COLLECTION_NAME"].ToString();
                ddlColour.SelectedValue = dt.Rows[0]["COLOR"].ToString();
                txtMatchingReff.Text = dt.Rows[0]["MATCHING_REFF"].ToString();
                txtQuantity.Text = dt.Rows[0]["QUANTITY"].ToString();
                txtRollSize.Text = dt.Rows[0]["ROLL_SIZE"].ToString();
                txtEndUse.Text = dt.Rows[0]["END_USE"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFabricTranasaction()
    {
        try
        {
            GridFabric.DataSource = dtOrderFabric;
            GridFabric.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
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
                cmbPartyCode.SelectedValue = dt.Rows[0]["CUSTOMER_NAME"].ToString();
                txtCustomerReffNo.Text = dt.Rows[0]["ORDER_REFF_NO"].ToString();
                txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                DDLOrderType.SelectedValue = dt.Rows[0]["ORDER_TYPE"].ToString();
                DDLOrderCat.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();
                DDLBusinessType.SelectedValue = dt.Rows[0]["BUSINESS_TYPE"].ToString();
                cmbDeliveryMode.SelectedValue = dt.Rows[0]["DELIVERY_MODE"].ToString();
                txtAgent.Text = dt.Rows[0]["AGENT"].ToString();
                txtDirectBilling.Text = dt.Rows[0]["DIRECT_BILLING"].ToString();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void cmbOrderNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbOrderNo.Items.Clear();
            cmbOrderNo.DataSource = data;
            cmbOrderNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " where ORDER_NO like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "order by ORDER_NO asc";
        string commandText = "select Distinct ORDER_NO,BUSINESS_TYPE,ORDER_CAT,ORDER_TYPE from OD_CUSTOMER_REQUEST_MST";
        string sPO = string.Empty;
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        Updatedata();
    }

    private void Updatedata()
    {
        bool bResult = false;
        try
        {
            if ((dtOrderFabric != null && dtOrderFabric.Rows.Count > 0))
            {
                SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();
                oOD_CUSTOMER_RQST_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_CUSTOMER_RQST_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oOD_CUSTOMER_RQST_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oOD_CUSTOMER_RQST_MST.ORDER_NO = cmbOrderNo.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.ORDER_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                oOD_CUSTOMER_RQST_MST.ORDER_REFF_NO = txtCustomerReffNo.Text.Trim();
                oOD_CUSTOMER_RQST_MST.ORDER_CAT = DDLOrderCat.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.ORDER_TYPE = DDLOrderType.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = DDLBusinessType.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = ProductType.Trim();
                oOD_CUSTOMER_RQST_MST.ADDRESS = txtAddress.Text.Trim();
                oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = cmbPartyCode.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = cmbDeliveryMode.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();
                oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                // oOD_RECIPE_ENTRY_MST.STATUS = chkActive.Checked;
                oOD_CUSTOMER_RQST_MST.TUSER = Session["urLoginId"].ToString().Trim();


                bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.UpdateCustomerRequestFabric(oOD_CUSTOMER_RQST_MST, dtOrderFabric);


                if (bResult)
                {
                    InitialiseData();
                    BlankTrnControls();
                    dtOrderFabric.Rows.Clear();
                    bindFabricGrid();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);

                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnExit_Click1(object sender, ImageClickEventArgs e)
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

    protected void GridFabric_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditFabricItem(UniqueId);
                string OrderNO = txtOrderNo.Text.Trim();

                DescFabric(OrderNO);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteFabricItem(UniqueId);
                bindFabricGrid();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    private void DescFabric(string OrderNO)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstFabric(OrderNO);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbDesignNo.SelectedValue = dt.Rows[0]["DESIGN_NO"].ToString();
                txtCollection.Text = dt.Rows[0]["COLLECTION_NAME"].ToString();
                ddlColour.SelectedValue = dt.Rows[0]["COLOR"].ToString();
                txtMatchingReff.Text = dt.Rows[0]["MATCHING_REFF"].ToString();
                txtQuantity.Text = dt.Rows[0]["QUANTITY"].ToString();
                txtRollSize.Text = dt.Rows[0]["ROLL_SIZE"].ToString();
                txtEndUse.Text = dt.Rows[0]["END_USE"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void EditFabricItem(int UniqueId)
    {
        try
        {
            DataView dv = new DataView(dtOrderFabric);
            dv.RowFilter = "UNIQUE_ID=" + UniqueId;
            if (dv.Count > 0)
            {
                cmbDesignNo.SelectedText = dv[0]["DESIGN_NO"].ToString();

                int DesignNo = Convert.ToInt32(cmbDesignNo.SelectedText.Trim());
                bindColor(DesignNo);
                txtMatchingReff.Text = dv[0]["MATCHING_REFF"].ToString();
                txtQuantity.Text = dv[0]["QUANTITY"].ToString();
                txtRollSize.Text = dv[0]["ROLL_SIZE"].ToString();
                txtEndUse.Text = dv[0]["END_USE"].ToString();
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void deleteFabricItem(int UniqueId)
    {
        try
        {
            if (dtOrderFabric.Rows.Count == 1)
            {
                dtOrderFabric.Rows.Clear();
                InsertFabricRow();
            }
            else
            {
                foreach (DataRow dr in dtOrderFabric.Rows)
                {
                    int iUniqueId = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtOrderFabric.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtOrderFabric.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    protected void cmbPartyCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string pc = cmbPartyCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectPartyCode(pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtAddress.Text = dt.Rows[0]["PRTY_ADD1"].ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void cmbPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string pc = cmbPartyCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectPartyCode(pc);
            if (dt != null && dt.Rows.Count > 0)
            {

                txtAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString() + ", " + dt.Rows[0]["PRTY_ADD1"].ToString();

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected DataTable GetItemsPC(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE PRTY_CODE like :SearchQuery and PRTY_NAME like :SearchQuery and PRTY_ADD1 like :SearchQuery and DEL_STATUS='0'";
            string sortExpression = string.Empty;
            string commandText = "select distinct PRTY_CODE,PRTY_NAME,PRTY_ADD1 from TX_VENDOR_MST ";
            string sPO = string.Empty;
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void cmbPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItemsPC(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbPartyCode.Items.Clear();
            cmbPartyCode.DataSource = data;
            cmbPartyCode.DataBind();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    private void BlankFabricControls()
    {
        try
        {
            txtCollection.Text = string.Empty;
            txtMatchingReff.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtRollSize.Text = string.Empty;
            txtEndUse.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            cmbDesignNo.SelectedIndex = -1;
            ddlColour.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    protected void btnFabricCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BlankFabricControls();
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {


    }

    protected DataTable GetItemsforDN(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE t.DESIGN_CODE like :SearchQuery  and t.DEL_STATUS='0'";
            string sortExpression = string.Empty;
            string commandText = "select distinct t.* from TX_FABRIC_DESIGN_MST t ";
            string sPO = string.Empty;
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbDesignNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItemsforDN(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbDesignNo.Items.Clear();
            cmbDesignNo.DataSource = data;
            cmbDesignNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void cmbDesignNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            int DesignNo = Convert.ToInt32(cmbDesignNo.SelectedValue.Trim());
            DataTable dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectDesignDetails(DesignNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtCollection.Text = dt.Rows[0]["COLLECTION_NAME"].ToString();
                bindColor(DesignNo);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
