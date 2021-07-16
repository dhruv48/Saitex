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

public partial class Module_OrderDevelopment_CustomerRequest_Controls_CustReqForLabdipYarnDeying : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private DataTable dtOrderST;

    private static string PRODUCT_TYPE = "YARN DYEING";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            cmbPartyCode.OnTextChanged += new CommonControls_PartyCodeLOV.RefreshDataGridView(cmbPartyCode_OnTextChanged);
            //   txtMatchingReff.AutoPostBack = false;

            if (!IsPostBack)
            {
                InitialiseData();
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nsee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        cmbPartyCode.AutoPostBack = true;
        cmbPartyCode.OnTextChanged += new CommonControls_PartyCodeLOV.RefreshDataGridView(cmbPartyCode_OnTextChanged);
        base.OnInit(e);
    }

    void cmbPartyCode_OnTextChanged(string Value, string Text)
    {
        try
        {
            txtPartyCode.Text = cmbPartyCode.SelectedItem.Trim();
            txtAddress.Text = cmbPartyCode.SelectedValue.ToString();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Changed.\r\nsee error log for detail."));
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
            // BindArticleNo();
            bindUOM();
            bindBusinessType();
            //ddlShadeCode.Items.Insert(0, "Select");
            cmbShade.SelectedIndex = -1;
            //BindShadeFamilyCode();
            bindEndUse();
            bindOrderType();
            bindOrderCat();
            bindDeliveryMode();
            BindLightSource();
            //BindOrderNo();
            ClearMainData();
            EnablePrimaryFields();
            BlankTrnControls();
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
            ddlBusinessType.Items.Insert(0, "Select");
            ddlBusinessType.SelectedIndex = 2;
        }
        catch
        {
            throw;

        }
    }

    private void bindUOM()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("UOM", oUserLoginDetail.COMP_CODE);
            ddlUom.DataSource = dt;
            ddlUom.DataValueField = "MST_CODE";
            ddlUom.DataTextField = "MST_DESC";
            ddlUom.DataBind();
            ddlUom.Items.Insert(0, "Select");
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
            txtLightSource.Items.Insert(0, "Select");
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
            ddlDeliveryMode.SelectedIndex = 3;
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
            //ddlBusinessType.SelectedIndex = -1;
            ddlOrderCategory.SelectedIndex = -1;
            ddlOrderType.SelectedIndex = -1;
            txtDirectBilling.Text = "";
            txtAgent.Text = "";
            txtOrderNo.Text = "";
            txtAddress.Text = "";
            cmbPartyCode.SelectedIndex = -1;
            txtCustomerReffNo.Text = "";
            txtPartyCode.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtMstRemarks.Text = string.Empty;
            ddlDeliveryMode.SelectedIndex = -1;
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
            ddlUom.SelectedIndex = -1;
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
            lblMode.Text = ex.Message;
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

    //private void BindArticleNo()
    //{

    //    try
    //    {
    //        DataTable data = new DataTable();
    //        data = GetItemsAN(string.Empty);
    //        cmbArticleNo.Items.Clear();
    //        cmbArticleNo.Items.Add(new ListItem("----Select-----", "0"));
    //        cmbArticleNo.DataSource = data;
    //        cmbArticleNo.DataTextField = "YARN_CODE";
    //        cmbArticleNo.DataValueField = "Combined";
    //        cmbArticleNo.DataBind();
    //    }
    //    catch
    //    {
    //        throw;

    //    }


    //}

    protected DataTable GetItemsAN(string text)
    {
        try
        {
            string whereClause = " where y.YARN_CODE like :SearchQuery and y.DEL_STATUS='0' and YARN_CAT='SEWING THREAD' ";
            string sortExpression = " order by y.YARN_CODE asc";
            string commandText = " SELECT   DISTINCT y.*, y.TKTNO  || '@'  || Y.MAKE   AS Combined FROM   YRN_MST y ";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbArticleNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {

    }

    private bool SearchArticalCodeInGrid(string ArticalCode, string TKTNo, string ShadeFamily, string Shade, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            if (GridSpinningThread.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in GridSpinningThread.Rows)
                {
                    Label txtArticleNo = (Label)grdRow.FindControl("txtSubtrate");
                    Label txtTktNo = (Label)grdRow.FindControl("txtCount");
                    Label txtShade = (Label)grdRow.FindControl("txtShade");
                    Label txtShadeName = (Label)grdRow.FindControl("txtShadeName");

                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUNIQUE_ID = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (txtArticleNo.Text.Trim() == ArticalCode && txtCount.Text.Trim() == TKTNo && txtShade.Text.Trim() == ShadeFamily && txtShadeName.Text == Shade && UNIQUE_ID != iUNIQUE_ID)
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

                    bool bb = SearchArticalCodeInGrid(txtSubrate.Text.Trim(), txtCount.Text, ddlShadeFamilyCode.Text.Trim(), ddlShadeCode.Text.Trim(), UniqueId);
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
                                    dv[0]["SUBTRATE"] = txtSubrate.Text.Trim();
                                    dv[0]["COUNT"] = txtCount.Text.Trim();
                                    dv[0]["ARTICLE"] = "NA";
                                    dv[0]["SHADE_FAMILY_CODE"] = ddlShadeFamilyCode.Text.Trim();
                                    dv[0]["SHADE_FAMILY_NAME"] = txtShadeFamilyName.Text.Trim();

                                    dv[0]["SHADE_CODE"] = ddlShadeCode.Text.Trim();
                                    dv[0]["SHADE_NAME"] = txtShadeName.Text.Trim();

                                    dv[0]["MATCHING_REFF"] = string.Empty;// txtMatchingReff.SelectedItem.Trim();
                                    dv[0]["MATCHING_REFF_NAME"] = string.Empty;// txtMatchingReff.SelectedItem.Trim();
                                    dv[0]["QUANTITY"] = qty;
                                    dv[0]["UOM"] = ddlUom.SelectedItem.ToString();
                                    dv[0]["END_USE"] = txtEndUse.SelectedValue.Trim();
                                    dv[0]["END_USE_NAME"] = txtEndUse.SelectedItem.Text.Trim();
                                    dv[0]["LIGHT_SOURCE"] = txtLightSource.SelectedItem.Text.Trim();

                                    dv[0]["REMARKS"] = txtRemarks.Text.Trim();

                                    dtOrderST.AcceptChanges();
                                }
                            }
                            else
                            {
                                DataRow dr = dtOrderST.NewRow();
                                dr["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                                dr["ORDER_NO"] = txtOrderNo.Text.Trim();
                                dr["SUBTRATE"] = txtSubrate.Text.Trim();
                                dr["COUNT"] = txtCount.Text.Trim();
                                dr["ARTICLE"] = "NA";
                                dr["SHADE_FAMILY_CODE"] = ddlShadeFamilyCode.Text.Trim();
                                dr["SHADE_FAMILY_NAME"] = txtShadeFamilyName.Text.Trim();
                                dr["SHADE_CODE"] = ddlShadeCode.Text.Trim();
                                dr["SHADE_NAME"] = txtShadeName.Text.Trim();
                                dr["MATCHING_REFF"] = string.Empty;// txtMatchingReff.SelectedItem.Trim();
                                dr["MATCHING_REFF_NAME"] = string.Empty;//txtMatchingReff.SelectedItem.Trim();
                                dr["QUANTITY"] = qty;
                                dr["UOM"] = ddlUom.SelectedItem.ToString();

                                dr["END_USE"] = txtEndUse.SelectedValue.Trim();
                                dr["END_USE_NAME"] = txtEndUse.SelectedItem.Text.Trim();
                                dr["LIGHT_SOURCE"] = txtLightSource.SelectedItem.Text.Trim();
                                dr["REMARKS"] = txtRemarks.Text.Trim();
                                // dr["MAKE"] = txtMake.Text.Trim();
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
            if (txtSubrate.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Substrate No. ";
                msgCount += 1;
            }


            if (txtCount.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please Enter Count. ";
                msgCount += 1;
            }

            if (ddlShadeFamilyCode.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Shade. ";
                msgCount += 1;
            }
            //if (txtMatchingReff.SelectedValue != "SELECT")
            //    count = count + 1;
            //else
            //{
            //    msg = msg + msgCount.ToString() + ": Please select Matching Reff. ";
            //    msgCount += 1;
            //}

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

            if (txtLightSource.SelectedIndex > 0)
            {
                count = count + 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Light Source ";
                msgCount += 1;
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
            dtOrderST.Columns.Add("SUBTRATE", typeof(string));
            dtOrderST.Columns.Add("ARTICLE", typeof(string));

            dtOrderST.Columns.Add("COUNT", typeof(string));
            dtOrderST.Columns.Add("TKT_NO", typeof(string));
            dtOrderST.Columns.Add("SHADE_FAMILY_CODE", typeof(string));
            dtOrderST.Columns.Add("SHADE_FAMILY_NAME", typeof(string));

            dtOrderST.Columns.Add("SHADE_CODE", typeof(string));
            dtOrderST.Columns.Add("SHADE_NAME", typeof(string));
            dtOrderST.Columns.Add("MATCHING_REFF", typeof(string));
            dtOrderST.Columns.Add("MATCHING_REFF_NAME", typeof(string));
            dtOrderST.Columns.Add("QUANTITY", typeof(double));
            dtOrderST.Columns.Add("UOM", typeof(string));
            //dtOrderST.Columns.Add("NO_OF_CASE_BOX", typeof(string));
            dtOrderST.Columns.Add("LIGHT_SOURCE", typeof(string));
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
            // cmbArticleNo.Items.Clear();
            // cmbArticleNo.DataSource = data;
            // cmbArticleNo.DataBind();

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
            // cmbArticleNo.SelectedIndex = -1;
            // txtTktNo.Text = "";
            txtSubrate.Text = string.Empty;
            txtCount.Text = string.Empty;
            ddlShadeCode.Text = string.Empty;
            txtLightSource.SelectedIndex = -1;
            cmbShade.SelectedIndex = -1;
            ddlShadeFamilyCode.Text.Trim();
            txtShadeFamilyName.Text = string.Empty;
            txtShadeName.Text = string.Empty;
            // txtMatchingReff.SelectedIndex = -1;
            // txtNoOfCaseBox.Text = "";
            txtEndUse.SelectedIndex = -1;
            txtRemarks.Text = "";
            //txtMake.Text = string.Empty;
            txtQuantity.Text = "";
            ddlUom.SelectedIndex = 0;
            ViewState["UNIQUE_ID"] = null;
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
                //BindShadeFamilyCode();
                //ddlShadeFamilyCode.SelectedIndex = ddlShadeFamilyCode.Items.IndexOf(ddlShadeFamilyCode.Items.FindByValue(dv[0]["SHADE_FAMILY_CODE"].ToString()));
                ddlShadeFamilyCode.Text = dv[0]["SHADE_FAMILY_CODE"].ToString();
                //BindShadeCode(ddlShadeFamilyCode.SelectedValue.ToString());
                //ddlShadeCode.SelectedIndex = ddlShadeCode.Items.IndexOf(ddlShadeCode.Items.FindByValue(dv[0]["SHADE_CODE"].ToString()));
                ddlShadeCode.Text = dv[0]["SHADE_CODE"].ToString();
                txtShadeFamilyName.Text = dv[0]["SHADE_FAMILY_NAME"].ToString();
                txtShadeName.Text = dv[0]["SHADE_NAME"].ToString();
                txtQuantity.Text = dv[0]["QUANTITY"].ToString();
                bindEndUse();
                txtEndUse.SelectedIndex = txtEndUse.Items.IndexOf(txtEndUse.Items.FindByText(dv[0]["END_USE_NAME"].ToString()));
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                txtSubrate.Text = dv[0]["SUBTRATE"].ToString();
                txtCount.Text = dv[0]["COUNT"].ToString();


                ddlUom.SelectedIndex = ddlUom.Items.IndexOf(ddlUom.Items.FindByText(dv[0]["UOM"].ToString()));
                txtLightSource.SelectedIndex = txtLightSource.Items.IndexOf(txtLightSource.Items.FindByText(dv[0]["LIGHT_SOURCE"].ToString()));
                //txtLightSource.Text = dv[0]["LIGHT_SOURCE"].ToString();
                ViewState["UNIQUE_ID"] = UniqueId;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
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
                    oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = cmbPartyCode.SelectedItem.ToString();
                    oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
                    oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                    oOD_CUSTOMER_RQST_MST.TUSER = oUserLoginDetail.UserCode;
                    oOD_CUSTOMER_RQST_MST.REMARKS = txtMstRemarks.Text.Trim();
                    bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.InsertCustomerRequestForSTLapdip(oOD_CUSTOMER_RQST_MST, dtOrderST);

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

            if (txtOrderNo.Text != "")
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
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbOrderNo.Items.Clear();
            cmbOrderNo.DataSource = data;
            cmbOrderNo.DataBind();


            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in OrderNo Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string commandText = "SELECT * FROM (SELECT DISTINCT ORDER_NO, BUSINESS_TYPE, ORDER_CAT, ORDER_TYPE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE)AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE = 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY ORDER_NO ASC) asd WHERE ORDER_NO LIKE :SearchQuery and rownum<=15 ";

            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and order_no not in ( SELECT ORDER_NO FROM (SELECT DISTINCT ORDER_NO, BUSINESS_TYPE, ORDER_CAT, ORDER_TYPE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE) AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE = 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY ORDER_NO ASC) asd WHERE ORDER_NO LIKE :SearchQuery and rownum<='" + startOffset + "' ) ";
            }

            string sortExpression = " order by ORDER_NO asc ";
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

    protected int GetItemsCount(string text)
    {

        string CommandText = " SELECT * FROM (SELECT DISTINCT ORDER_NO, BUSINESS_TYPE, ORDER_CAT, ORDER_TYPE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE)AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE = 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "' ORDER BY ORDER_NO ASC) asd WHERE ORDER_NO LIKE :SearchQuery  ";
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
            SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST oOD_CUSTOMER_RQST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();

            DataTable dtMst = new DataTable();
            DataTable dtST = new DataTable();

            string cString = cmbOrderNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);

            oOD_CUSTOMER_RQST_MST.COMP_CODE = arrString[0].ToString();
            oOD_CUSTOMER_RQST_MST.BRANCH_CODE = arrString[1].ToString();
            oOD_CUSTOMER_RQST_MST.YEAR = int.Parse(arrString[2].ToString());
            oOD_CUSTOMER_RQST_MST.ORDER_TYPE = arrString[3].ToString();
            oOD_CUSTOMER_RQST_MST.ORDER_CAT = arrString[4].ToString();
            oOD_CUSTOMER_RQST_MST.PRODUCT_TYPE = arrString[5].ToString();
            oOD_CUSTOMER_RQST_MST.ORDER_NO = arrString[6].ToString();
            oOD_CUSTOMER_RQST_MST.BUSINESS_TYPE = arrString[7].ToString();


            dtMst = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectSTCustomerRqstMst(oOD_CUSTOMER_RQST_MST);

            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                BindControls(dtMst);
                dtST = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.SelectCustomerRqstSTLapdip(oOD_CUSTOMER_RQST_MST);
                if (dtST != null && dtST.Rows.Count > 0)
                {
                    MapTableST(dtST);
                    BindSTTranasaction();
                }
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
                    drST["SHADE_FAMILY_NAME"] = dr["SHADE_FAMILY_NAME"];
                    drST["MATCHING_REFF"] = dr["MATCHING_REFF"];
                    drST["QUANTITY"] = dr["QUANTITY"];
                    drST["END_USE_NAME"] = dr["END_USE_NAME"];
                    drST["END_USE"] = dr["END_USE"];
                    drST["SUBTRATE"] = dr["SUBSTRATE"];
                    drST["COUNT"] = dr["COUNT"];
                    drST["SHADE_CODE"] = dr["SHADE_CODE"];
                    drST["SHADE_NAME"] = dr["SHADE_NAME"];
                    drST["LIGHT_SOURCE"] = dr["LIGHT_SOURCE"];
                    drST["UOM"] = dr["UOM"];
                    drST["REMARKS"] = dr["REMARKS"];
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
                oOD_CUSTOMER_RQST_MST.CUSTOMER_NAME = txtPartyCode.Text;
                oOD_CUSTOMER_RQST_MST.DELIVERY_MODE = ddlDeliveryMode.SelectedValue.Trim();
                oOD_CUSTOMER_RQST_MST.AGENT = txtAgent.Text.Trim();
                oOD_CUSTOMER_RQST_MST.DIRECT_BILLING = txtDirectBilling.Text.Trim();
                oOD_CUSTOMER_RQST_MST.TUSER = oUserLoginDetail.UserCode;
                oOD_CUSTOMER_RQST_MST.REMARKS = txtMstRemarks.Text.Trim();
                bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.UpdateCustomerRequestSTLapdip(oOD_CUSTOMER_RQST_MST, dtOrderST);

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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leaving Page.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;

        }
    }

    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
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

    }

    private void EnablePrimaryFields()
    {

        try
        {
            ddlOrderCategory.Enabled = true;
            //ddlBusinessType.Enabled = true;
            ddlOrderType.Enabled = true;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.Message;
        }
    }

    protected void cmbArticleNo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            //int UNIQUE_ID = 0;
            //if (ViewState["UNIQUE_ID"] != null)
            //{
            //    UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
            //}
            //string ArticleNo = cmbArticleNo.SelectedItem.ToString();
            //string[] arrString = cmbArticleNo.SelectedValue.Split('@');
            // txtTktNo.Text = arrString[0].ToString();
            // txtMake.Text = arrString[1].ToString();
            //if (SearchArticalCodeInGrid(ArticleNo,txtTktNo.Text , ddlShade.SelectedItem.ToString(),    UNIQUE_ID))
            //{
            //    cmbArticleNo.SelectedIndex = -1;
            //    Common.CommonFuction.ShowMessage("This Yarn artical code already exists.");
            //}
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    //private void BindShadeFamilyCode()
    //{
    //    try
    //    {
    //        DataTable data = new DataTable();
    //        oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //        data = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeFamilyCodeALL(oOD_SHADE_FAMILY);
    //        if (data != null && data.Rows.Count > 0)
    //        {
    //            ddlShadeFamilyCode.Items.Clear();
    //            ddlShadeFamilyCode.DataSource = data;
    //            ddlShadeFamilyCode.DataValueField = "SHADE_FAMILY_CODE";
    //            ddlShadeFamilyCode.DataTextField = "SHADE_FAMILY_NAME";
    //            ddlShadeFamilyCode.DataBind();
    //            ddlShadeFamilyCode.Items.Insert(0, "Select");
    //        }
    //        else
    //        {
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //protected void ddlShadeFamilyCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindShadeCode(ddlShadeFamilyCode.SelectedValue.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Shade Family Selection .\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }

    //}

    //private void BindShadeCode(string ShadeFamilyCode)
    //{
    //    try
    //    {
    //        DataTable data = new DataTable();


    //        data = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeTrnByComAndMstCode(oUserLoginDetail.COMP_CODE, ddlShadeFamilyCode.SelectedValue.ToString());
    //        if (data != null && data.Rows.Count > 0)
    //        {
    //            ddlShadeCode.Items.Clear();
    //            ddlShadeCode.DataSource = data;
    //            ddlShadeCode.DataValueField = "SHADE_CODE";
    //            ddlShadeCode.DataTextField = "SHADE_NAME";
    //            ddlShadeCode.DataBind();
    //            ddlShadeCode.Items.Insert(0, "Select");
    //        }
    //        else
    //        {
    //            ddlShadeCode.Items.Clear();
    //            ddlShadeCode.Items.Insert(0, "NOShadeCodeExists");
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
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

    protected void cmbShade_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbShade.SelectedIndex > -1)
            {
                string[] arrString = cmbShade.SelectedValue.Split('@');
                ddlShadeFamilyCode.Text = arrString[0].ToString();
                txtShadeFamilyName.Text = arrString[1].ToString();
                ddlShadeCode.Text = arrString[2].ToString();
                txtShadeName.Text = arrString[3].ToString();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Shade");
            }
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
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
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

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}
