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

public partial class Module_OrderDevelopment_CustomerRequest_Controls_CustForLabDipNew : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtOrderST;

    private static string PRODUCT_TYPE = "SEWING THREAD";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            cmbPartyCode.OnTextChanged += new CommonControls_PartyCodeLOV.RefreshDataGridView(PartyCodeLOV1_OnTextChanged);

            if (!IsPostBack)
            {
                InitialiseData();
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nsee error log for detail."));
        }
    }

    private void InitialiseData()
    {
        try
        {
            ActivateSaveMode();

            cmbOrderNo.Visible = false;
            txtOrderNo.Visible = true;
            BindArticleNo();
            bindBusinessType();
            bindShade();
            bindEndUse();
            bindOrderType();
            bindOrderCat();
            bindDeliveryMode();
            //BindOrderNo();
            ClearMainData();
            EnablePrimaryFields();
            BlankTrnControls();
            BlankSTControls();

            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }
            dtOrderST = null;
            ViewState["dtOrderST"] = dtOrderST;

            bindSTGrid();
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

            lblMode.Text = "Save";
            tdSave.Visible = true;
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
        }
        catch
        {
            throw;

        }
    }

    private void bindShade()
    {
        try
        {
            ddlShade.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("YARN_SHADETYPE", oUserLoginDetail.COMP_CODE);
            ddlShade.DataSource = dt;
            ddlShade.DataValueField = "MST_CODE";
            ddlShade.DataTextField = "MST_DESC";
            ddlShade.DataBind();
            ddlShade.Items.Insert(0, "Select");
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

    private void bindOrderCat()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_CAT", oUserLoginDetail.COMP_CODE);
            ddlOrderCategory.DataSource = dt;
            ddlOrderCategory.DataValueField = "MST_CODE";
            ddlOrderCategory.DataTextField = "MST_DESC";
            ddlOrderCategory.DataBind();
            ddlOrderCategory.Items.Insert(0, "---Select---");
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

            ddlOrderType.Items.Clear();
            ddlOrderType.Items.Insert(0, "---Select---");
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

    private void ClearMainData()
    {
        try
        {
            txtDate.Text = System.DateTime.Now.ToShortDateString();
            ddlBusinessType.SelectedIndex = -1;
            ddlOrderCategory.SelectedIndex = -1;
            ddlOrderType.SelectedIndex = -1;
            txtDirectBilling.Text = "";
            txtAgent.Text = "";
            txtOrderNo.Text = "";
            txtAddress.Text = "";
            cmbPartyCode.SelectedIndex = -1;
            txtCustomerReffNo.Text = "";
            txtMstRemarks.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void BlankTrnControls()
    {

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
        }
    }

    protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOrderCategory.SelectedIndex > 0 && ddlOrderType.SelectedIndex > 0)
            {
                BindOrderNo();
            }
            else
            {
                //Common.CommonFuction.ShowMessage("Please Select Request Type");
                txtOrderNo.Text = "";

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in order Category selection.\r\nSee error log for detail."));
        }
    }

    private void BindOrderNo()
    {
        try
        {
            string BusinessType = string.Empty;
            string OrderCategory = string.Empty;
            string RequestType = string.Empty;
            string msg = string.Empty;

            int count = 0;
            if (ddlBusinessType.SelectedIndex != -1)
            {
                BusinessType = ddlBusinessType.SelectedValue.Trim();
                count += 1;
            }

            if (ddlOrderCategory.SelectedIndex != -1)
            {
                OrderCategory = ddlOrderCategory.SelectedValue.Trim();
                count += 1;
            }
            if (ddlOrderType.SelectedIndex != -1)
            {
                RequestType = ddlOrderType.SelectedValue.Trim();
                count += 1;
            }

            if (count == 3)
            {
                string OrderNo = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetNewSTOrderNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BusinessType, OrderCategory, PRODUCT_TYPE, RequestType);
                txtOrderNo.Text = OrderNo;
            }
        }
        catch
        {
            throw;
        }
    }

    void PartyCodeLOV1_OnTextChanged(string Val, string Text)
    {
        try
        {
            txtAddress.Text = Text;

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
            txtAddress.Text = cmbPartyCode.SelectedValue.Trim();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    private void BindArticleNo()
    {

        try
        {
            DataTable data = new DataTable();
            data = GetItemsAN(string.Empty);
            cmbArticleNo.Items.Clear();
            cmbArticleNo.Items.Add(new ListItem("----Select-----", "0"));
            cmbArticleNo.DataSource = data;
            cmbArticleNo.DataTextField = "YARN_CODE";
            cmbArticleNo.DataValueField = "Combined";
            cmbArticleNo.DataBind();
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
            string whereClause = " where y.YARN_CODE like :SearchQuery and y.DEL_STATUS='0' and YARN_CAT='SEWING THREAD' ";
            string sortExpression = " order by y.YARN_CODE asc";
            string commandText = " SELECT   DISTINCT y.*,( y.TKTNO  || '@'  || Y.MAKE || '@' || Y.ENDUSE || '@'  || Y.YARN_CODE)  AS Combined FROM   YRN_MST y ";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
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
                    Label txtTktNo = (Label)grdRow.FindControl("txtTktNo");
                    Label txtShade = (Label)grdRow.FindControl("txtShade");

                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUNIQUE_ID = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (txtArticleNo.Text.Trim() == ArticalCode && txtTktNo.Text.Trim() == TKTNo && txtShade.Text.Trim() == Shade && UNIQUE_ID != iUNIQUE_ID)
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

                if (dtOrderST.Rows.Count < 15)
                {
                    int UniqueId = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                        UniqueId = int.Parse(ViewState["UNIQUE_ID"].ToString());

                    string ArticalCode = cmbArticleNo.SelectedItem.ToString();


                    bool bb = SearchArticalCodeInGrid(ArticalCode, txtTktNo.Text, ddlShade.SelectedItem.ToString(), UniqueId);
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
                                    dv[0]["ARTICLE_NO"] = cmbArticleNo.SelectedItem.ToString();
                                    dv[0]["TKT_NO"] = txtTktNo.Text.Trim();
                                    dv[0]["SHADE"] = ddlShade.SelectedValue.Trim();
                                    dv[0]["SHADE_NAME"] = ddlShade.SelectedItem.Text.Trim();
                                    dv[0]["MATCHING_REFF"] = string.Empty;
                                    dv[0]["MATCHING_REFF_NAME"] = string.Empty;
                                    dv[0]["QUANTITY"] = qty;
                                    dv[0]["NO_OF_CASE_BOX"] = txtNoOfCaseBox.Text.Trim();
                                    dv[0]["END_USE"] = txtEndUse.SelectedValue.Trim();
                                    dv[0]["END_USE_NAME"] = txtEndUse.SelectedItem.Text.Trim();
                                    dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                                    dv[0]["MAKE"] = txtMake.Text.Trim();
                                }

                                dtOrderST.AcceptChanges();
                            }
                            else
                            {

                                DataRow dr = dtOrderST.NewRow();
                                dr["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                                dr["ORDER_NO"] = txtOrderNo.Text.Trim();
                                dr["ARTICLE_NO"] = cmbArticleNo.SelectedItem.ToString();
                                dr["TKT_NO"] = txtTktNo.Text.Trim();
                                dr["SHADE"] = ddlShade.SelectedValue.Trim();
                                dr["SHADE_NAME"] = ddlShade.SelectedItem.Text.Trim();
                                dr["MATCHING_REFF"] = string.Empty;
                                dr["MATCHING_REFF_NAME"] = string.Empty;

                                dr["QUANTITY"] = qty;
                                dr["NO_OF_CASE_BOX"] = txtNoOfCaseBox.Text.Trim();
                                dr["END_USE"] = txtEndUse.SelectedValue.Trim();
                                dr["END_USE_NAME"] = txtEndUse.SelectedItem.Text.Trim();
                                dr["REMARKS"] = txtRemarks.Text.Trim();
                                dr["MAKE"] = txtMake.Text.Trim();
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
                        Common.CommonFuction.ShowMessage(@"Please Select Diffrent Artical Code !! Article Code|TktNo | Shade Should be Diffrent ");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage(@"Maximum Limit Reached");
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
            errorLog.ErrHandler.WriteError(ex.Message);
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
            if (cmbArticleNo.SelectedIndex >= -1)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Article No. ";
                msgCount += 1;
            }


            if (txtTktNo.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please Enter Tkt No. ";
                msgCount += 1;
            }

            if (ddlShade.SelectedIndex > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Shade. ";
                msgCount += 1;
            }

            double dd = 0;
            double.TryParse(txtQuantity.Text.Trim(), out dd);
            if (dd > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Quantity/Validy Quantity. ";
                msgCount += 1;
                txtQuantity.Text = string.Empty;
            }

            if (txtNoOfCaseBox.Text != "")
            {
                double NoOfCase = 0;
                double.TryParse(txtNoOfCaseBox.Text, out NoOfCase);
                if (NoOfCase > 0)
                {
                    count = count + 1;
                }
                else
                {
                    msg = msg + msgCount.ToString() + ": Please enter valid No Of Case/Box. ";
                    msgCount += 1;
                    txtNoOfCaseBox.Text = string.Empty;
                }
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please enter valid No Of Case/Box. ";
                msgCount += 1;
                txtNoOfCaseBox.Text = string.Empty;
            }

            if (txtEndUse.SelectedIndex > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select End Use. ";
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

    private void CreateSTDataTable()
    {
        try
        {
            dtOrderST = new DataTable();
            dtOrderST.Columns.Add("UNIQUE_ID", typeof(int));
            dtOrderST.Columns.Add("ORDER_NO", typeof(string));
            dtOrderST.Columns.Add("ARTICLE_NO", typeof(string));
            dtOrderST.Columns.Add("TKT_NO", typeof(string));
            dtOrderST.Columns.Add("SHADE", typeof(string));
            dtOrderST.Columns.Add("SHADE_NAME", typeof(string));
            dtOrderST.Columns.Add("MATCHING_REFF", typeof(string));
            dtOrderST.Columns.Add("MATCHING_REFF_NAME", typeof(string));
            dtOrderST.Columns.Add("QUANTITY", typeof(double));
            dtOrderST.Columns.Add("NO_OF_CASE_BOX", typeof(string));
            dtOrderST.Columns.Add("END_USE", typeof(string));
            dtOrderST.Columns.Add("END_USE_NAME", typeof(string));
            dtOrderST.Columns.Add("REMARKS", typeof(string));
            dtOrderST.Columns.Add("MAKE", typeof(string));


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
            cmbArticleNo.Items.Clear();
            cmbArticleNo.DataSource = data;
            cmbArticleNo.DataBind();

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
            txtTktNo.Text = "";
            ddlShade.SelectedIndex = -1;

            txtNoOfCaseBox.Text = "";
            txtEndUse.SelectedIndex = -1;
            txtRemarks.Text = "";
            txtMake.Text = string.Empty;
            txtQuantity.Text = "";
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
            errorLog.ErrHandler.WriteError(ex.Message);
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
            errorLog.ErrHandler.WriteError(ex.Message);
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

                cmbArticleNo.SelectedIndex = cmbArticleNo.Items.IndexOf(cmbArticleNo.Items.FindByText(dv[0]["ARTICLE_NO"].ToString()));
                txtTktNo.Text = dv[0]["TKT_NO"].ToString();
                ddlShade.SelectedIndex = ddlShade.Items.IndexOf(ddlShade.Items.FindByValue(dv[0]["SHADE"].ToString()));

                txtQuantity.Text = dv[0]["QUANTITY"].ToString();
                txtNoOfCaseBox.Text = dv[0]["NO_OF_CASE_BOX"].ToString();
                txtEndUse.SelectedIndex = txtEndUse.Items.IndexOf(txtEndUse.Items.FindByValue(dv[0]["END_USE"].ToString()));
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                txtMake.Text = dv[0]["MAKE"].ToString();
                ViewState["UNIQUE_ID"] = UniqueId;
                // ViewState["GREY_YARN_CODE"] = GreyYarnCode;

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

    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
                    oOD_CUSTOMER_RQST_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
                    oOD_CUSTOMER_RQST_MST.ADDRESS = txtAddress.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = cmbPartyCode.SelectedValue.ToString();
                    oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.TUSER = oUserLoginDetail.UserCode;
                    oOD_CUSTOMER_RQST_MST.REMARKS = txtMstRemarks.Text.Trim();
                  //  bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.InsertCustomerRequestForST(oOD_CUSTOMER_RQST_MST, dtOrderST);

                    if (bResult == true)
                    {
                        string Resultmsg = "Customer Request Saved Successfully" + "\\r\\n";
                        Resultmsg += "Customer Request No is:" + oOD_CUSTOMER_RQST_MST.ORDER_NO;
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
            if (ddlOrderCategory.SelectedIndex >= 0)
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
            //if (txtBranch.Text != "")
            //    count = count + 1;
            //else
            //{
            //    msg = msg + msgCount.ToString() + ": Please select Branch. ";
            //    msgCount += 1;
            //}
            if (txtAddress.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Address. ";
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

    protected void imgbtnFind_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            cmbOrderNo.SelectedIndex = -1;
            cmbOrderNo.Visible = true;
            txtOrderNo.Visible = false;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            GetCustReq();
            DiablePrimaryFields();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void GetCustReq()
    {

        try
        {
            string whereClause = " where ORDER_NO like :SearchQuery ";
            string sortExpression = " order by ORDER_NO DESC ";
            string commandText = "SELECT * FROM   (SELECT   DISTINCT ORDER_NO, BUSINESS_TYPE, ORDER_CAT, ORDER_TYPE, (COMP_CODE ||'@'|| BRANCH_CODE||'@'||YEAR||'@'||ORDER_TYPE||'@'||ORDER_CAT||'@'||PRODUCT_TYPE||'@'||ORDER_NO||'@'||BUSINESS_TYPE) AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE   DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' and ORDER_TYPE='DEVELOPMENT' and comp_code='" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' ) asd";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", "%", sPO);

            cmbOrderNo.Items.Clear();
            cmbOrderNo.Items.Add(new ListItem("Select", "SELECT"));

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbOrderNo.DataSource = dt;
                cmbOrderNo.DataValueField = "Combined";
                cmbOrderNo.DataTextField = "ORDER_NO";
                cmbOrderNo.DataBind();
            }

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
                ddlOrderCategory.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();

                txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString();

                txtCustomerReffNo.Text = dt.Rows[0]["ORDER_REFF_NO"].ToString();
                txtAddress.Text = cmbPartyCode.SelectedItem.Trim();
                ddlOrderType.SelectedValue = dt.Rows[0]["ORDER_TYPE"].ToString();
                ddlOrderCategory.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();
                ddlBusinessType.SelectedValue = dt.Rows[0]["BUSINESS_TYPE"].ToString();
                ddlDeliveryMode.SelectedValue = dt.Rows[0]["DELIVERY_MODE"].ToString();
                txtAgent.Text = dt.Rows[0]["AGENT"].ToString();
                txtDirectBilling.Text = dt.Rows[0]["DIRECT_BILLING"].ToString();
                txtMstRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
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
                    drST["SHADE"] = dr["SHADE"];
                    drST["MATCHING_REFF"] = dr["MATCHING_REFF"];
                    drST["QUANTITY"] = dr["QUANTITY"];
                    drST["NO_OF_CASE_BOX"] = dr["NO_OF_CASE_BOX"];
                    drST["END_USE"] = dr["END_USE"];
                    drST["REMARKS"] = dr["REMARKS"];
                    drST["SHADE_NAME"] = dr["SHADE_NAME"];
                    drST["MATCHING_REFF_NAME"] = dr["MATCHING_REFF"];
                    drST["END_USE_NAME"] = dr["END_USE_NAME"];
                    drST["MAKE"] = dr["MAKE"];
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

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Updatedata();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
                oOD_CUSTOMER_RQST_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
                oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.ADDRESS = txtAddress.Text.Trim();
                oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = cmbPartyCode.SelectedValue.ToString();
                oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();
                oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                oOD_CUSTOMER_RQST_MST.TUSER = oUserLoginDetail.UserCode;
                oOD_CUSTOMER_RQST_MST.REMARKS = txtMstRemarks.Text.Trim();
               // bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.UpdateCustomerRequestST(oOD_CUSTOMER_RQST_MST, dtOrderST);

                if (bResult)
                {
                    string Resultmsg = "Customer Request Updated Successfully" + "\\r\\n";
                    Resultmsg += "Customer Request No is:" + oOD_CUSTOMER_RQST_MST.ORDER_NO;
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

    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {

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

    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialiseData();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void EnablePrimaryFields()
    {

        try
        {
            ddlOrderCategory.Enabled = true;
            //ddlBusinessType.Enabled = true;
            ddlOrderType.Enabled = true;
            ddlBusinessType.Enabled = true;
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
            ddlBusinessType.Enabled = false;
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
            if (ddlOrderCategory.SelectedIndex > 0)
            {
                BindOrderNo();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Request Order");
                txtOrderNo.Text = "";

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in order Category selection.\r\nSee error log for detail."));
        }
    }

    protected void cmbArticleNo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            string ArticleNo = cmbArticleNo.SelectedItem.ToString();
            string[] arrString = cmbArticleNo.SelectedValue.Split('@');
            txtTktNo.Text = arrString[0].ToString();
            txtMake.Text = arrString[1].ToString();

            txtEndUse.SelectedIndex = txtEndUse.Items.IndexOf(txtEndUse.Items.FindByValue(arrString[2].ToString()));

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void cmbOrderNo_SelectedIndexChanged1(object sender, EventArgs e)
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

            SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();

            oOD_CUSTOMER_RQST_MST.COMP_CODE = COMP_CODE;
            oOD_CUSTOMER_RQST_MST.BRANCH_CODE = BRANCH_CODE;
            oOD_CUSTOMER_RQST_MST.YEAR = int.Parse(YEAR);
            oOD_CUSTOMER_RQST_MST.ORDER_TYPE = ORDER_TYPE;
            oOD_CUSTOMER_RQST_MST.ORDER_CAT = ORDER_CAT;
            oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CUSTOMER_RQST_MST.ORDER_NO = ORDER_NO;
            oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = BUSINESS_TYPE;

            dtMst = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectYarnCustomerRqstMst(oOD_CUSTOMER_RQST_MST);
            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                BindControls(dtMst);
                // dtFabric = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstFabric(OrderNumber);
                dtST = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstST(oOD_CUSTOMER_RQST_MST);
                if (dtST != null && dtST.Rows.Count > 0)
                {
                    MapTableST(dtST);
                    BindSTTranasaction();
                }
            }


        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }
    }

}