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

public partial class Module_OrderDevelopment_Fiber_Lap_Dip_Controls_Fiber_CustReqforLapDip : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.FIBER_OD_SHADE_FAMILY oFIBER_OD_SHADE_FAMILY = new SaitexDM.Common.DataModel.FIBER_OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtOrderST;
    private static string PRODUCT_TYPE = "FIBER_DYEING";
    private string strContext = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];            
            if (!IsPostBack)
            {
                strContext = oUserLoginDetail.COMP_CODE + "@" + oUserLoginDetail.CH_BRANCHCODE;
                aceAgent.ContextKey = strContext;
                InitialiseData();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nsee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    private void InitialiseData()
    {
        try
        {
            ActivateSaveMode();
            cmbOrderNo.Visible = false;
            txtOrderNo.Visible = true;          
            bindBusinessType();        
            Bind_BillingMode();
            bindOrderType();
            bindOrderCat();
            bindDeliveryMode();
            BindLightSource();
            BindLightSource1();
            ClearMainData();
            EnablePrimaryFields();
            BlankTrnControls();
            BindOrderNo();         
           
        }
        catch
        {
            throw;
        }

    }

    private void BindLightSource1()
    {
        try
        {
            txtLightSource1.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("LIGHT_SOURCE", oUserLoginDetail.COMP_CODE);
            txtLightSource1.DataSource = dt;
            txtLightSource1.DataValueField = "MST_CODE";
            txtLightSource1.DataTextField = "MST_DESC";
            txtLightSource1.DataBind();
            txtLightSource1.Items.Insert(0, "NA");
        }
        catch
        {
            throw;
        }
    }

    protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage("Problem in getting Item for Indent. see error log for detail.");
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected DataTable GetItems(string Text, int startOffset, int numberOfItems)
    {
        string CommandText = " SELECT * FROM (SELECT FIBER_CODE, FIBER_DESC, FIBER_CAT FROM TX_FIBER_NEW_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) WHERE    FIBER_CAT in('CATONIC' , 'SUPER BRIGHT' ,'NA') AND ROWNUM <= 15 ";
        string whereClause = string.Empty;
        if (startOffset != 0)
        {
            whereClause += " AND FIBER_CODE NOT IN (SELECT FIBER_CODE FROM (SELECT FIBER_CODE, FIBER_DESC, FIBER_CAT FROM TX_FIBER_NEW_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) WHERE    FIBER_CAT in('CATONIC' , 'SUPER BRIGHT' ,'NA') AND ROWNUM <= " + startOffset + ") ";
        }
        string SortExpression = " ORDER BY FIBER_CODE";
        string SearchQuery = Text + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");


    }

    protected int GetItemsCount(string text)
    {
        string CommandText = "SELECT * FROM (SELECT FIBER_CODE, FIBER_DESC, FIBER_CAT FROM TX_FIBER_NEW_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) WHERE   FIBER_CAT in('CATONIC' , 'SUPER BRIGHT' ,'NA') ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY FIBER_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
    }

    public void setFabricCombo(string code, string desc)
    {

        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("SELECT * FROM TX_FABRIC_NEW_MASTER", "", "", "", "", "");
        cmbFabricCode.DataSource = data;
        cmbFabricCode.DataTextField = "FIBER_CODE";
        cmbFabricCode.DataValueField = "FIBER_CODE";
        cmbFabricCode.DataBind();
        foreach (Obout.ComboBox.ComboBoxItem dl in cmbFabricCode.Items)
        {
            if (dl.Text == code)
            {
                cmbFabricCode.SelectedIndex = cmbFabricCode.Items.IndexOf(dl);
                break;
            }
        }
    }     

    private void ActivateSaveMode()
    {
        try
        {
            lblMode.Text = "You are in Save Mode";
            tdSave.Visible = true;

            cmbOrderNo.Visible = false;
            cmbOrderNo.SelectedValue = string.Empty;
            cmbOrderNo.SelectedText = string.Empty;
            cmbOrderNo.SelectedIndex = -1;
            cmbOrderNo.Items.Clear();
            txtOrderNo.Visible = true;

            tdUpdate.Visible = false;
            tdDelete.Visible = false;
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
            ddlBusinessType.DataSource = dt;
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataBind();         
            ddlBusinessType.SelectedIndex = 2;
        }
        catch
        {
            throw;

        }
    }
   
    private void BindLightSource()
    {
        try
        {
            txtLightSource.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("LIGHT_SOURCE", oUserLoginDetail.COMP_CODE);
            txtLightSource.DataSource = dt;
            txtLightSource.DataValueField = "MST_CODE";
            txtLightSource.DataTextField = "MST_DESC";
            txtLightSource.DataBind();
            //txtLightSource.Items.Insert(0, "Select");
            txtLightSource.SelectedIndex = txtLightSource.Items.IndexOf(txtLightSource.Items.FindByValue("D65"));
        }
        catch
        {
            throw;
        }
    }

    private void bindOrderCat()
    {
        try
        {

            //DataTable dt = new DataTable();
            //dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_CAT", oUserLoginDetail.COMP_CODE);
            //ddlOrderCategory.DataSource = dt;
            //ddlOrderCategory.DataValueField = "MST_CODE";
            //ddlOrderCategory.DataTextField = "MST_DESC";
            //ddlOrderCategory.DataBind();
            DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            ddlOrderCategory.DataSource = dt;
            ddlOrderCategory.DataValueField = "BRANCH_NAME";
            ddlOrderCategory.DataTextField = "BRANCH_NAME";
            ddlOrderCategory.DataBind();
            ddlOrderCategory.SelectedIndex = ddlOrderCategory.Items.IndexOf(ddlOrderCategory.Items.FindByValue(oUserLoginDetail.VC_BRANCHNAME));
        }

        catch
        {
            throw;

        }

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
            //ddlDeliveryMode.Items.Insert(0, "Select");
            ddlDeliveryMode.SelectedIndex = 2;
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
            ddlOrderType.Items.Clear();
            //ddlOrderType.Items.Insert(0, "---Select---");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string sType = dr["MST_CODE"].ToString();
                    if (string.Compare(sType, "Development", true) == 0)
                    {
                        ddlOrderType.Items.Add(new ListItem(dr["MST_DESC"].ToString(), dr["MST_CODE"].ToString()));
                    }
                }
            }
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
            ddlBillingMode.Items.Insert(0, "--------Select-------");
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
            ddlOrderType.SelectedIndex = 0;
            txtDirectBilling.Text = string.Empty;
            txtAgent.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            cmbPartyCode.SelectedIndex = -1;
            txtCustomerReffNo.Text = string.Empty;
            txtPartyCode.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtMstRemarks.Text = string.Empty;
            ddlDeliveryMode.SelectedIndex = 1;
            ddlBillingMode.SelectedIndex = 1;
            txtFabCode.Text = string.Empty;
            txtFabDesc.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void BlankTrnControls()
    {
        try
        {
            txtRemarks.Text = string.Empty;           
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Business type selection.\r\nSee error log for detail."));

            lblMode.Text = ex.Message;
        }
    }

    protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in order Category selection.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
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
            CRLocationPrefix = oUserLoginDetail.SEQ_PREFIX.ToString();
            CRType = ddlOrderType.SelectedItem.ToString();
            ORDER_CAT = ddlBusinessType.SelectedValue.ToString();
            string OrderNo = SaitexBL.Interface.Method.FIBER_OD_CUSTOMER_REQUEST_MST.GetNewSTOrderNo1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, CRLocationPrefix, CRType, ORDER_CAT);
            txtOrderNo.Text = OrderNo;
            txtFabCode.Text = string.Empty;
            txtFabDesc.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetItemsAN(string text)
    {
        try
        {
            string whereClause = " where y.FIBER_CODE like :SearchQuery and y.DEL_STATUS='0' and YARN_CAT='SEWING THREAD' ";
            string sortExpression = " order by y.FIBER_CODE asc";
            string commandText = " SELECT   DISTINCT y.*, y.TKTNO  || '@'  || Y.MAKE   AS Combined FROM   TX_FABRIC_NEW_MASTER y ";
            string sPO = "";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
           
        }
        catch
        {
            throw;
        }
    }

    protected void cmbArticleNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {

    }

    private bool SearchArticalCodeInGrid(string ArticalCode, string Shade, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            if (GridSpinningThread.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in GridSpinningThread.Rows)
                {
                    Label txtArticleNo = (Label)grdRow.FindControl("txtSubtrate");                   
                    Label txtShadeName = (Label)grdRow.FindControl("txtShadeName");
                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUNIQUE_ID = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (txtArticleNo.Text.Trim() == ArticalCode&& txtShadeName.Text == Shade && UNIQUE_ID != iUNIQUE_ID)
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

            string msg = string.Empty;
            if (ValidateSTRow(out msg))
            {
                int UniqueId = 0;
                if (ViewState["UNIQUE_ID"] != null)
                    UniqueId = int.Parse(ViewState["UNIQUE_ID"].ToString());

                bool bb = SearchArticalCodeInGrid(txtFabCode.Text.Trim(), txtShadeName.Text.Trim(), UniqueId);
                if (!bb)
                {
                    double qty = 0;
                    double.TryParse(txtQuantity.Text.Trim(), out qty);

                    if (qty > 0)
                    {
                        if (UniqueId > 0)
                        {
                            DataView dv = new DataView(dtOrderST);
                            dv.RowFilter = "UNIQUE_ID=" + UniqueId;
                            if (dv.Count > 0)
                            {
                                dv[0]["ORDER_NO"] = txtOrderNo.Text.Trim();
                                dv[0]["SUBTRATE"] = txtFabCode.Text.Trim();
                                dv[0]["DESCRIPTION"] = txtFabDesc.Text;
                                dv[0]["ARTICLE"] = txtFabCode.Text.Trim(); 
                                dv[0]["SHADE_FAMILY_CODE"] = "NA";
                                dv[0]["SHADE_FAMILY_NAME"] = "NA";
                                dv[0]["SHADE_CODE"] = txtShadeName.Text.Trim();
                                dv[0]["SHADE_NAME"] = txtShadeName.Text.Trim();
                                dv[0]["MATCHING_REFF"] = string.Empty;// txtMatchingReff.SelectedItem.Trim();
                                dv[0]["MATCHING_REFF_NAME"] = string.Empty;// txtMatchingReff.SelectedItem.Trim();
                                dv[0]["QUANTITY"] = qty;
                                dv[0]["UOM"] = string.Empty;
                                dv[0]["END_USE"] = string.Empty ;
                                dv[0]["END_USE_NAME"] = string.Empty;
                                dv[0]["LIGHT_SOURCE"] = txtLightSource.SelectedItem.Text.Trim();
                                dv[0]["LIGHT_SOURCE1"] = txtLightSource1.SelectedItem.Text.Trim();
                                dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                                dtOrderST.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtOrderST.NewRow();
                            dr["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                            dr["ORDER_NO"] = txtOrderNo.Text.Trim();
                            dr["SUBTRATE"] = txtFabCode.Text.Trim();
                            dr["DESCRIPTION"] = txtFabDesc.Text;
                            dr["ARTICLE"] = txtFabCode.Text.Trim();
                            dr["SHADE_FAMILY_CODE"] = "NA";
                            dr["SHADE_FAMILY_NAME"] = "NA";
                            dr["SHADE_CODE"] = txtShadeName.Text.Trim();
                            dr["SHADE_NAME"] = txtShadeName.Text.Trim();
                            dr["MATCHING_REFF"] = string.Empty;// txtMatchingReff.SelectedItem.Trim();
                            dr["MATCHING_REFF_NAME"] = string.Empty;//txtMatchingReff.SelectedItem.Trim();
                            dr["QUANTITY"] = qty;
                            dr["UOM"] = string.Empty;
                            dr["END_USE"] = string.Empty;
                            dr["END_USE_NAME"] = string.Empty;
                            dr["LIGHT_SOURCE"] = txtLightSource.SelectedItem.Text.Trim();
                            dr["LIGHT_SOURCE1"] = txtLightSource1.SelectedItem.Text.Trim();
                            dr["REMARKS"] = txtRemarks.Text.Trim();                           
                            dtOrderST.Rows.Add(dr);
                        }
                        BlankSTControls();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(@"Quantity can not be zero");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage(@"Please Select Diffrent Substrate !! Substrate |AtrticleCode | Shade Family Should be Diffrent ");
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Tran Detail Saving\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    private bool ValidateSTRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;


            int count = 0;
            int msgCount = 1;
            if (!string.IsNullOrEmpty(txtFabCode.Text))
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Fabric ";
                msgCount += 1;
            }

           

            if (txtShadeName.Text != string.Empty)
            {
                count = count + 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please enter Shade Description ";
                msgCount += 1;
            }
           

            double dd = 0;
            double.TryParse(txtQuantity.Text.Trim(), out dd);
            if (dd > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please enter valid No Of Option. ";
                msgCount += 1;
                txtQuantity.Text = string.Empty;
            }
            
            if (count == 3)
                result = true;

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
            dtOrderST.Columns.Add("SUBTRATE", typeof(string));
            dtOrderST.Columns.Add("ARTICLE", typeof(string));          
            dtOrderST.Columns.Add("TKT_NO", typeof(string));
            dtOrderST.Columns.Add("SHADE_FAMILY_CODE", typeof(string));
            dtOrderST.Columns.Add("SHADE_FAMILY_NAME", typeof(string));
            dtOrderST.Columns.Add("SHADE_CODE", typeof(string));
            dtOrderST.Columns.Add("SHADE_NAME", typeof(string));
            dtOrderST.Columns.Add("MATCHING_REFF", typeof(string));
            dtOrderST.Columns.Add("MATCHING_REFF_NAME", typeof(string));
            dtOrderST.Columns.Add("QUANTITY", typeof(double));
            dtOrderST.Columns.Add("UOM", typeof(string));           
            dtOrderST.Columns.Add("LIGHT_SOURCE", typeof(string));
            dtOrderST.Columns.Add("LIGHT_SOURCE1", typeof(string));
            dtOrderST.Columns.Add("END_USE", typeof(string));
            dtOrderST.Columns.Add("END_USE_NAME", typeof(string));
            dtOrderST.Columns.Add("REMARKS", typeof(string));
            dtOrderST.Columns.Add("MAKE", typeof(string));
            dtOrderST.Columns.Add("COUNT", typeof(string));
            dtOrderST.Columns.Add("DESCRIPTION", typeof(string));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindCmbArticle()
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItemsAN(string.Empty);        
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
           
            cmbFabricCode.SelectedIndex = -1;           
            txtLightSource1.SelectedIndex = -1;            
            txtLightSource.SelectedIndex = -1;          
            txtShadeName.Text = string.Empty;           
            txtRemarks.Text = "";          
            txtQuantity.Text = "";            
            ViewState["UNIQUE_ID"] = null;
            txtFabDesc.Text = string.Empty;
            txtFabCode.Text = string.Empty;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancelling Detail \r\nSee error log for detail."));
            lblMode.Text = ex.Message;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Command \r\nSee error log for detail."));
            lblMode.Text = ex.Message;
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
            dv.RowFilter = "UNIQUE_ID=" + UniqueId;
            if (dv.Count > 0)
            {                
                txtShadeName.Text = dv[0]["SHADE_CODE"].ToString();
                txtQuantity.Text = dv[0]["QUANTITY"].ToString();           
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                cmbFabricCode.SelectedValue = dv[0]["SUBTRATE"].ToString();
                txtFabDesc.Text = dv[0]["DESCRIPTION"].ToString();
                txtFabCode.Text= dv[0]["SUBTRATE"].ToString();             
                txtLightSource1.SelectedIndex = txtLightSource1.Items.IndexOf(txtLightSource1.Items.FindByText(dv[0]["LIGHT_SOURCE1"].ToString()));
                txtLightSource.SelectedIndex = txtLightSource.Items.IndexOf(txtLightSource.Items.FindByText(dv[0]["LIGHT_SOURCE"].ToString()));
                ViewState["UNIQUE_ID"] = UniqueId;
                setFabricCombo(dv[0]["SUBTRATE"].ToString(),dv[0]["SUBTRATE"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
            {
                dtOrderST.Rows.Clear();
            }
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
        }
        catch
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    private void Insertdata()
    {
        try
        {
            //if (lblmsg.Text == "Shade Family Code Avaliable")
            //{

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
                        SaitexDM.Common.DataModel.FIBER_OD_CUSTOMER_RQST_MST oFIBER_OD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.FIBER_OD_CUSTOMER_RQST_MST();
                        oFIBER_OD_CUSTOMER_RQST_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        oFIBER_OD_CUSTOMER_RQST_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        oFIBER_OD_CUSTOMER_RQST_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

                        //open the code New Function multiuser entry one time  by Arun Sharma 23/02/2018
                        try
                        {

                            string CRLocationPrefix = string.Empty;
                            string CRType = string.Empty;
                            string ORDER_CAT = string.Empty;
                            CRLocationPrefix = oUserLoginDetail.SEQ_PREFIX.ToString();
                            CRType = ddlOrderType.SelectedItem.ToString();
                            ORDER_CAT = ddlBusinessType.SelectedValue.ToString();
                            string NewOrderNo = SaitexBL.Interface.Method.FIBER_OD_CUSTOMER_REQUEST_MST.GetNewSTOrderNo1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, CRLocationPrefix, CRType, ORDER_CAT);
                            txtOrderNo.Text = NewOrderNo;
                        }
                        catch
                        {
                            throw;
                        }
                        //close code 
                        oFIBER_OD_CUSTOMER_RQST_MST.ORDER_NO = txtOrderNo.Text.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.ORDER_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                        oFIBER_OD_CUSTOMER_RQST_MST.ORDER_REFF_NO = txtCustomerReffNo.Text.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.ORDER_CAT = ddlOrderCategory.SelectedItem.ToString().Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
                        oFIBER_OD_CUSTOMER_RQST_MST.ADDRESS = txtAddress.Text.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.CUSTOMER_NAME = txtPartyCode.Text.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.TUSER = oUserLoginDetail.UserCode;
                        oFIBER_OD_CUSTOMER_RQST_MST.REMARKS = txtMstRemarks.Text.Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.BILLING_MODE = ddlBillingMode.SelectedValue.ToString().Trim();
                        oFIBER_OD_CUSTOMER_RQST_MST.BILL_TO = string.Empty;
                        oFIBER_OD_CUSTOMER_RQST_MST.CURRENCY_CODE = string.Empty;
                        oFIBER_OD_CUSTOMER_RQST_MST.SHIPMENT = string.Empty;
                        oFIBER_OD_CUSTOMER_RQST_MST.CONV_RATE = 0.0;
                        oFIBER_OD_CUSTOMER_RQST_MST.PAYMENT_MODE = string.Empty;
                        oFIBER_OD_CUSTOMER_RQST_MST.PAYMENT_TERMS = string.Empty;


                        bResult = SaitexBL.Interface.Method.FIBER_OD_CUSTOMER_REQUEST_MST.InsertCustomerRequestForSTLapdip(oFIBER_OD_CUSTOMER_RQST_MST, dtOrderST);

                        if (bResult == true)
                        {
                            string Resultmsg = "Customer Request Saved Successfully" + "\\r\\n";
                            Resultmsg += "Customer Request No is:" + oFIBER_OD_CUSTOMER_RQST_MST.ORDER_NO;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                            InitialiseData();
                            dtOrderST.Rows.Clear();
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
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Artical Code Already Exisits');", true);
            //}
        }
        catch
        {
            throw;
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
                msg = msg + msgCount.ToString() + ": Please select Business Type. ";
                msgCount += 1;
            }

            if (ddlOrderType.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Order Type. ";
                msgCount += 1;
            }

            if (ddlOrderCategory.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Product Type. ";
                msgCount += 1;
            }

            if (txtPartyCode.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Customer Name. ";
                msgCount += 1;
            }

            if (ddlBillingMode.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Billing Mode. ";
                msgCount += 1;
            }

            if (txtOrderNo.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please Create Order No. ";
                msgCount += 1;
            }

            if (count == 6)
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Update Mode Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    protected void cmbOrderNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems1(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbOrderNo.Items.Clear();
            cmbOrderNo.DataSource = data;
            cmbOrderNo.DataBind();


            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount1(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in OrderNo Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    protected DataTable GetItems1(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string commandText = "SELECT * FROM (SELECT DISTINCT ORDER_NO, BUSINESS_TYPE, ORDER_CAT, ORDER_TYPE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE)AS Combined FROM V_TX_FIB_OD_CUSTOMER_REQST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE = 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY ORDER_NO DESC) asd WHERE ORDER_NO LIKE :SearchQuery and rownum<=15 ";

            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and order_no not in ( SELECT ORDER_NO FROM (SELECT DISTINCT ORDER_NO, BUSINESS_TYPE, ORDER_CAT, ORDER_TYPE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE) AS Combined FROM V_TX_FIB_OD_CUSTOMER_REQST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE = 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY ORDER_NO DESC) asd WHERE ORDER_NO LIKE :SearchQuery and rownum<='" + startOffset + "' ) ";
            }

            string sortExpression = " order by ORDER_NO desc ";
            string SearchQuery = "%" + text + "%";

            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", SearchQuery, sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount1(string text)
    {

        string CommandText = " SELECT * FROM (SELECT DISTINCT ORDER_NO, BUSINESS_TYPE, ORDER_CAT, ORDER_TYPE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE)AS Combined FROM V_TX_FIB_OD_CUSTOMER_REQST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE = 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY ORDER_NO ASC) asd WHERE ORDER_NO LIKE :SearchQuery  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void cmbOrderNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.FIBER_OD_CUSTOMER_RQST_MST FIBER_oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.FIBER_OD_CUSTOMER_RQST_MST();

            DataTable dtMst = new DataTable();
            DataTable dtST = new DataTable();

            string cString = cmbOrderNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            FIBER_oOD_CUSTOMER_RQST_MST.COMP_CODE = arrString[0].ToString();
            FIBER_oOD_CUSTOMER_RQST_MST.BRANCH_CODE = arrString[1].ToString();
            FIBER_oOD_CUSTOMER_RQST_MST.YEAR = int.Parse(arrString[2].ToString());
            FIBER_oOD_CUSTOMER_RQST_MST.ORDER_TYPE = arrString[3].ToString();
            FIBER_oOD_CUSTOMER_RQST_MST.ORDER_CAT = arrString[4].ToString();
            FIBER_oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = arrString[5].ToString();
            FIBER_oOD_CUSTOMER_RQST_MST.ORDER_NO = arrString[6].ToString();
            FIBER_oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = arrString[7].ToString();
            dtMst = SaitexBL.Interface.Method.FIBER_OD_CUSTOMER_REQUEST_MST.SelectSTCustomerRqstMst(FIBER_oOD_CUSTOMER_RQST_MST);
            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                BindControls(dtMst);
                dtST = SaitexBL.Interface.Method.FIBER_OD_CUSTOMER_REQUEST_MST.SelectLapdipRqstST(FIBER_oOD_CUSTOMER_RQST_MST);
                if (dtST != null && dtST.Rows.Count > 0)
                {
                    MapTableST(dtST);
                   
                }
                BindSTTranasaction();
            }


        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in OrderNo Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
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
                ddlOrderCategory.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();
                txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString();
                txtCustomerReffNo.Text = dt.Rows[0]["ORDER_REFF_NO"].ToString();
                txtAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString();
                ddlOrderType.SelectedValue = dt.Rows[0]["ORDER_TYPE"].ToString();
                ddlOrderCategory.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();
                ddlBusinessType.SelectedValue = dt.Rows[0]["BUSINESS_TYPE"].ToString();
                ddlDeliveryMode.SelectedValue = dt.Rows[0]["DELIVERY_MODE"].ToString();
                txtAgent.Text = dt.Rows[0]["AGENT"].ToString();
                txtDirectBilling.Text = dt.Rows[0]["DIRECT_BILLING"].ToString();
                txtMstRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                ddlBillingMode.SelectedIndex = ddlBillingMode.Items.IndexOf(ddlBillingMode.Items.FindByValue(dt.Rows[0]["BILLING_MODE"].ToString()));
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
                    drST["ARTICLE"] = dr["ARTICLE_NO"];
                    drST["SHADE_FAMILY_CODE"] = dr["SHADE_FAMILY_CODE"];
                    drST["SHADE_FAMILY_NAME"] = dr["SHADE_FAMILY_CODE"];
                    drST["MATCHING_REFF"] = dr["MATCHING_REFF"];
                    drST["QUANTITY"] = dr["QUANTITY"];                 
                    drST["SUBTRATE"] = dr["SUBSTRATE"];
                    drST["COUNT"] = dr["COUNT"];
                    drST["LIGHT_SOURCE1"] = dr["LIGHT_SOURCE1"];
                    drST["SHADE_CODE"] = dr["SHADE_CODE"];
                    drST["SHADE_NAME"] = dr["SHADE_CODE"];
                    drST["LIGHT_SOURCE"] = dr["LIGHT_SOURCE"];
                    drST["UOM"] = dr["UOM"];
                    drST["REMARKS"] = dr["REMARKS"];
                    drST["DESCRIPTION"] = dr["DESCRIPTION"];
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Data.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;

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
                SaitexDM.Common.DataModel.FIBER_OD_CUSTOMER_RQST_MST FIBER_oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.FIBER_OD_CUSTOMER_RQST_MST();
                FIBER_oOD_CUSTOMER_RQST_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                FIBER_oOD_CUSTOMER_RQST_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                FIBER_oOD_CUSTOMER_RQST_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                FIBER_oOD_CUSTOMER_RQST_MST.ORDER_NO = txtOrderNo.Text.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.ORDER_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                FIBER_oOD_CUSTOMER_RQST_MST.ORDER_REFF_NO = txtCustomerReffNo.Text.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
                FIBER_oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.ADDRESS = txtAddress.Text.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = txtPartyCode.Text;
                FIBER_oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.TUSER = oUserLoginDetail.UserCode;
                FIBER_oOD_CUSTOMER_RQST_MST.REMARKS = txtMstRemarks.Text.Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.BILLING_MODE = ddlBillingMode.SelectedValue.ToString().Trim();
                FIBER_oOD_CUSTOMER_RQST_MST.SHIPMENT = string.Empty;
                FIBER_oOD_CUSTOMER_RQST_MST.CURRENCY_CODE = string.Empty;
                FIBER_oOD_CUSTOMER_RQST_MST.CONV_RATE = 0.0;
                FIBER_oOD_CUSTOMER_RQST_MST.PAYMENT_MODE = string.Empty;
                FIBER_oOD_CUSTOMER_RQST_MST.PAYMENT_TERMS = string.Empty;
                FIBER_oOD_CUSTOMER_RQST_MST.BILL_TO = string.Empty;

                bResult = SaitexBL.Interface.Method.FIBER_OD_CUSTOMER_REQUEST_MST.UpdateCustomerRequestSTLapdip(FIBER_oOD_CUSTOMER_RQST_MST, dtOrderST);

                if (bResult)
                {
                    string Resultmsg = "Customer Request Updated Successfully" + "\\r\\n";
                    Resultmsg += "Customer Request No is:" + FIBER_oOD_CUSTOMER_RQST_MST.ORDER_NO;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                    InitialiseData();
                    dtOrderST.Rows.Clear();
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leaving Page.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;

        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./CustreqForLabDip.aspx", false);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing Page.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;

        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../../LabDip/Reports/Customer_Request_For_Yarn.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);



    }

    private void EnablePrimaryFields()
    {

        try
        {
            ddlOrderCategory.Enabled = false;
            //ddlBusinessType.Enabled = true;
            //ddlOrderType.Enabled = true;
        }
        catch
        {
            throw;

        }


    }

    private void DiablePrimaryFields()
    {

        try
        {
            ddlOrderCategory.Enabled = false;
            //ddlBusinessType.Enabled = false;
            ddlOrderType.Enabled = false;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

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
        try
        {
            if (ddlBillingMode.SelectedIndex > 0)
            {
                if (ddlBillingMode.SelectedItem.ToString() == "Branch Transfer")
                {
                    txtPartyCode.Text = oUserLoginDetail.BRANCH_PRTYCODE;
                    txtAddress.Text = oUserLoginDetail.BRANCH_PRTYNAMEADDRES;
                }
                else if (ddlBillingMode.SelectedItem.ToString() == "Direct Billing")
                {
                    txtPartyCode.Text = string.Empty;
                    txtAddress.Text = string.Empty;
                    cmbPartyCode.SelectedIndex = -1;
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please select Billing Mode first..");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Billing Mode Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    protected void ddlCustomerRefNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlBillingMode.SelectedIndex > 0)
            //{
            //    if (ddlBillingMode.SelectedItem.ToString() == "Branch Transfer")
            //    {
            //        txtPartyCode.Text = oUserLoginDetail.BRANCH_PRTYCODE;
            //        txtAddress.Text = oUserLoginDetail.BRANCH_PRTYNAMEADDRES;
            //    }
            //    else if (ddlBillingMode.SelectedItem.ToString() == "Direct Billing")
            //    {
            //        txtPartyCode.Text = string.Empty;
            //        txtAddress.Text = string.Empty;
            //        cmbPartyCode.SelectedIndex = -1;
            //    }
            //}
            //else
            //{
            //    CommonFuction.ShowMessage("Please select Billing Mode first..");
            //}
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Billing Mode Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    private DataTable GetBranchPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT PRTY_CODE, PRTY_NAME,(PRTY_NAME || PRTY_ADD1) Address, PRTY_GRP_CODE, PARTY_CODE  FROM  ( SELECT TV.PRTY_CODE, TV.PRTY_NAME, TV.PRTY_ADD1, TV.PRTY_GRP_CODE, BR.PARTY_CODE  FROM TX_VENDOR_MST TV, CM_BRANCH_MST BR  WHERE   TV.PRTY_CODE LIKE :SearchQuery OR TV.PRTY_NAME LIKE :SearchQuery  ORDER BY   TV.PRTY_CODE ASC) asd WHERE   PRTY_CODE = PARTY_CODE AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM  ( SELECT TV.PRTY_CODE, TV.PRTY_NAME, TV.PRTY_ADD1, TV.PRTY_GRP_CODE, BR.PARTY_CODE  FROM TX_VENDOR_MST TV, CM_BRANCH_MST BR  WHERE   TV.PRTY_CODE LIKE :SearchQuery OR TV.PRTY_NAME LIKE :SearchQuery  ORDER BY   TV.PRTY_CODE ASC) asd WHERE PRTY_CODE = PARTY_CODE AND ROWNUM <= " + startOffset + ")";
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

    protected int GetBranchPartyCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE, PRTY_NAME,(PRTY_NAME || PRTY_ADD1) Address, PRTY_GRP_CODE, PARTY_CODE  FROM  ( SELECT TV.PRTY_CODE, TV.PRTY_NAME, TV.PRTY_ADD1, TV.PRTY_GRP_CODE, BR.PARTY_CODE  FROM TX_VENDOR_MST TV, CM_BRANCH_MST BR  WHERE   TV.PRTY_CODE LIKE :SearchQuery OR TV.PRTY_NAME LIKE :SearchQuery  ORDER BY   TV.PRTY_CODE ASC) asd WHERE   PRTY_CODE = PARTY_CODE ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void cmbPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            if (ddlBillingMode.SelectedIndex > 0)
            {
                if (ddlBillingMode.SelectedItem.ToString() == "Branch Transfer")
                {
                    DataTable data = GetBranchPartyData(e.Text.ToUpper(), e.ItemsOffset);

                    cmbPartyCode.Items.Clear();

                    cmbPartyCode.DataSource = data;
                    cmbPartyCode.DataBind();

                    e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
                    e.ItemsCount = GetBranchPartyCount(e.Text);
                }
                else if (ddlBillingMode.SelectedItem.ToString() == "Direct Billing")
                {
                    DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);

                    cmbPartyCode.Items.Clear();

                    cmbPartyCode.DataSource = data;
                    cmbPartyCode.DataBind();

                    e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
                    e.ItemsCount = GetPartyCount(e.Text);
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please select Billing Mode first..");
            }
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
            bindCustomerRefNo(txtPartyCode.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindCustomerRefNo(string Party)
    {
        try
        {
            DataTable dt = new DataTable();
            //dt = GetCustomerRefNo(Party);

            ddlCustomerRefNo.DataSource = dt;
            ddlCustomerRefNo.DataTextField = "";
            ddlCustomerRefNo.DataValueField = "";
            ddlCustomerRefNo.DataBind();

        }
        catch (Exception ex)
        {
            //CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex,@"Problem in Customer No Dropdown creation.");
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
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

        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

        
    }
    protected void Item_LOV_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string fc = cmbFabricCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.SelectYarnDetail(fc);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtFabCode.Text = dt.Rows[0]["FIBER_CODE"].ToString();
                txtFabDesc.Text = dt.Rows[0]["FIBER_DESC"].ToString();                       
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void txtCustomerReffNo_TextChanged(object sender, EventArgs e)
    {
        oFIBER_OD_SHADE_FAMILY.PRODUCT_TYPE = "FIBER_DYEING";
        oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oFIBER_OD_SHADE_FAMILY.ORDER_REFF_NO = txtCustomerReffNo.Text.Trim();
        bool Result = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GenrateArticalCode(oFIBER_OD_SHADE_FAMILY);
        if (Result)
        {
            lblmsg.Text = "Artical Code Avaliable";
            lblmsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblmsg.Text = "Artical Code Already Exists";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }
}

