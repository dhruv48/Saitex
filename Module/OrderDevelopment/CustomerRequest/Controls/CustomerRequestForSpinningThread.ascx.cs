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

public partial class Module_OrderDevelopment_CustomerRequest_Controls_CustomerRequestForSpinningThread : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string PRODUCT_TYPE = "SEWING THREAD";
    private DataTable dtOrderST;
    private string strContext = string.Empty;

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
            ClearMainData();
            txtCrLocation.Text = oUserLoginDetail.VC_BRANCHNAME;
            bindBusinessType();
            //bindShadefamily();
            bindEndUse();
            bindOrderType();
            // bindOrderCat();
            bindDeliveryMode();
            Bind_BillingMode();
            Bind_CaseBox();
            ActivateSaveMode();
            cmbOrderNo.Visible = false;
            txtOrderNo.Visible = true;

            BindOrderNo();
            EnablePrimaryFields();
            BlankSTControls();

            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            dtOrderST = null;
            ViewState["dtOrderST"] = dtOrderST;

            bindSTGrid();
            ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue("SW"));
            ddlDeliveryMode.SelectedIndex = ddlDeliveryMode.Items.IndexOf(ddlDeliveryMode.Items.FindByValue("BY ROAD"));
            DDLCaseBox.SelectedIndex = DDLCaseBox.Items.IndexOf(DDLCaseBox.Items.FindByText("BOX"));
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

    //private void bindShadefamily()
    //{
    //    try
    //    {
    //        DataTable data = new DataTable();
    //        SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    //        oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
    //        data = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeFamilyCodeALL(oOD_SHADE_FAMILY);
    //        if (data != null && data.Rows.Count > 0)
    //        {
    //            ddlShadeFamily.Items.Clear();
    //            ddlShadeFamily.DataSource = data;
    //            ddlShadeFamily.DataValueField = "SHADE_FAMILY_CODE";
    //            ddlShadeFamily.DataTextField = "SHADE_FAMILY_NAME";
    //            ddlShadeFamily.DataBind();
    //            ddlShadeFamily.Items.Insert(0, "Select");
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

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

    //private void bindOrderCat()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_CAT", oUserLoginDetail.COMP_CODE);
    //        ddlOrderCategory.DataSource = dt;
    //        ddlOrderCategory.DataValueField = "MST_CODE";
    //        ddlOrderCategory.DataTextField = "MST_DESC";
    //        ddlOrderCategory.DataBind();
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

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

    private void Bind_CaseBox()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("CR_UNIT", oUserLoginDetail.COMP_CODE);
            DDLCaseBox.Items.Clear();
            DDLCaseBox.DataSource = dt;
            DDLCaseBox.DataValueField = "MST_CODE";
            DDLCaseBox.DataTextField = "MST_DESC";
            DDLCaseBox.DataBind();
            DDLCaseBox.Items.Insert(0, "Select");
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
            ddlBillingMode.Items.Insert(0, "Select");
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
            //ddlOrderType.Items.Insert(0, "Select");
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
            txtCrLocation.Text = string.Empty;
            //ddlOrderCategory.SelectedIndex = 1;
            ddlOrderType.SelectedIndex = -1;
            txtDirectBilling.Text = string.Empty;
            TxtDocumentDate.Text = string.Empty;
            //TxtDocumentDate.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            cmbPartyCode.SelectedIndex = -1;
            txtPartyCode.Text = "";
            txtCustomerReffNo.Text = string.Empty;
            txtMstRemarks.Text = string.Empty;
            txtAgent.Text = string.Empty;
            ddlBillingMode.SelectedIndex = -1;
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

    protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlOrderCategory.SelectedIndex != -1 && ddlOrderType.SelectedIndex != -1)
            //{
            BindOrderNo();
            //}
            //else
            //{
            //    txtOrderNo.Text = string.Empty;
            //}
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Category Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            ORDER_CAT = ddlBusinessType.SelectedItem.ToString();
            string OrderNo = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetNewSTOrderNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, CRLocationPrefix, CRType, ORDER_CAT);
            txtOrderNo.Text = OrderNo;
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
            txtAddress.Text = Val;
            txtPartyCode.Text = Text;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Code Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchArticalCodeInGrid(string ArticalCode, string TKTNo, string ShadeFamily, int UNIQUE_ID)
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
                    Label txtShadeFamily = (Label)grdRow.FindControl("txtShadeFamily");
                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUNIQUE_ID = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (txtArticleNo.Text.Trim() == ArticalCode && txtTktNo.Text.Trim() == TKTNo && txtShadeFamily.Text.Trim() == ShadeFamily && txtShadeName.Text == txtShade.Text && UNIQUE_ID != iUNIQUE_ID)
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
                //if (dtOrderST.Rows.Count < 15)
                //{
                int UniqueId = 0;
                if (ViewState["UNIQUE_ID"] != null)
                    UniqueId = int.Parse(ViewState["UNIQUE_ID"].ToString());

                string ArticalCode = txtItemCodeLabel.Text.ToString();
                bool bb = SearchArticalCodeInGrid(ArticalCode, txtTktNo.Text, ddlShadeFamily.Text, UniqueId);
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
                                dv[0]["ARTICLE_NO"] = txtItemCodeLabel.Text.ToString();
                                dv[0]["TKT_NO"] = txtTktNo.Text.Trim();
                                dv[0]["SHADE_FAMILY_CODE"] = ddlShadeFamily.Text.Trim();
                                dv[0]["SHADE_FAMILY_NAME"] = txtShadeFamilyName.Text.Trim();
                                dv[0]["SHADE_CODE"] = ddlShadeCode.Text.Trim();
                                dv[0]["SHADE_NAME"] = txtShadeName.Text.Trim();
                                dv[0]["MATCHING_REFF"] = txtMatchingReff.SelectedItem.Trim();
                                dv[0]["MATCHING_REFF_NAME"] = txtMatchingReff.SelectedItem.Trim();
                                dv[0]["QUANTITY"] = qty;
                                dv[0]["CR_UNIT"] = DDLCaseBox.SelectedValue.Trim().ToString();
                                //dv[0]["CR_UNIT"] = txtNoOfCaseBox.Text.Trim();
                                dv[0]["END_USE"] = txtEndUse.SelectedValue.Trim();
                                dv[0]["END_USE_NAME"] = txtEndUse.SelectedItem.Text.Trim();
                                dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                                dv[0]["MAKE"] = txtMake.Text.Trim();
                                dv[0]["NO_OF_UNIT"] = txtNoofUnit.Text.Trim();
                                dv[0]["WEIGHT_OF_UNIT"] = txtWeightofUnit.Text.Trim();
                                dv[0]["REQ_DATE"] = DateTime.Parse(txtReqDate.Text);


                            }
                            dtOrderST.AcceptChanges();
                        }
                        else
                        {
                            DataRow dr = dtOrderST.NewRow();
                            dr["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                            dr["ORDER_NO"] = txtOrderNo.Text.Trim();
                            dr["ARTICLE_NO"] = txtItemCodeLabel.Text.ToString();
                            dr["TKT_NO"] = txtTktNo.Text.Trim();
                            dr["SHADE_FAMILY_CODE"] = ddlShadeFamily.Text.Trim();
                            dr["SHADE_FAMILY_NAME"] = txtShadeFamilyName.Text.Trim();
                            dr["SHADE_CODE"] = ddlShadeCode.Text.Trim();
                            dr["SHADE_NAME"] = txtShadeName.Text.Trim();
                            dr["MATCHING_REFF"] = txtMatchingReff.SelectedItem.Trim();
                            dr["MATCHING_REFF_NAME"] = txtMatchingReff.SelectedItem.Trim();
                            dr["QUANTITY"] = qty;
                            dr["CR_UNIT"] = DDLCaseBox.SelectedValue.Trim().ToString();
                            dr["END_USE"] = txtEndUse.SelectedValue.Trim();
                            dr["END_USE_NAME"] = txtEndUse.SelectedItem.Text.Trim();
                            dr["REMARKS"] = txtRemarks.Text.Trim();
                            dr["MAKE"] = txtMake.Text.Trim();
                            dr["NO_OF_UNIT"] = txtNoofUnit.Text.Trim();
                            dr["WEIGHT_OF_UNIT"] = txtWeightofUnit.Text.Trim();
                            dr["REQ_DATE"] = DateTime.Parse(txtReqDate.Text);
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
                //}
                //else
                //{
                //    Common.CommonFuction.ShowMessage(@"Maximum Limit Reached");
                //}
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
                count += 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Article No. ";

            }

            countAll += 1;
            if (txtTktNo.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please Enter Tkt No. ";
            }

            countAll += 1;
            if (ddlShadeFamily.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Shade Family. ";
            }

            countAll += 1;
            if (ddlShadeCode.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Shade Code. ";

            }

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
            double.TryParse(txtQuantity.Text.Trim(), out dd);
            if (dd > 0)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Quantity/Validy Quantity. ";
                txtQuantity.Text = string.Empty;
            }

            countAll += 1;
            if (DDLCaseBox.SelectedIndex > 0)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please enter valid Case/Box. ";

                DDLCaseBox.SelectedIndex = -1;
            }

            countAll += 1;
            if (txtEndUse.SelectedIndex > 0)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select End Use. ";
                msgCount += 1;
            }
            countAll += 1;
            if (txtReqDate.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please Enter Req Date. ";
                msgCount += 1;
            }


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
            txtTktNo.Text = string.Empty;
            ddlShadeFamily.Text = string.Empty;
            ddlShadeCode.Text = string.Empty;
            cmbShade.SelectedIndex = -1;
            txtShadeFamilyName.Text = string.Empty;
            txtShadeName.Text = string.Empty;
            txtMatchingReff.SelectedIndex = -1;
            DDLCaseBox.SelectedIndex = -1;
            txtEndUse.SelectedIndex = -1;
            txtRemarks.Text = string.Empty;
            txtMake.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtWeightofUnit.Text = string.Empty;
            txtNoofUnit.Text = string.Empty;
            txtReqDate.Text = string.Empty;

            ViewState["UNIQUE_ID"] = null;
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

                cmbArticleNo.Enabled = false;
                txtItemCodeLabel.Text = dv[0]["ARTICLE_NO"].ToString();

                txtTktNo.Text = dv[0]["TKT_NO"].ToString();
                //bindShadefamily();
                //ddlShadeFamily.SelectedIndex = ddlShadeFamily.Items.IndexOf(ddlShadeFamily.Items.FindByValue(dv[0]["SHADE_FAMILY_CODE"].ToString()));
                ddlShadeFamily.Text = dv[0]["SHADE_FAMILY_CODE"].ToString();
                txtShadeFamilyName.Text = dv[0]["SHADE_FAMILY_NAME"].ToString();
                //BindShadeCode(ddlShadeFamily.SelectedValue.ToString());
                //ddlShadeCode.SelectedIndex = ddlShadeCode.Items.IndexOf(ddlShadeCode.Items.FindByValue(dv[0]["SHADE_CODE"].ToString()));
                ddlShadeCode.Text = dv[0]["SHADE_CODE"].ToString();
                txtShadeName.Text = dv[0]["SHADE_NAME"].ToString();
                txtMatchingReff.SetIndexByText(dv[0]["MATCHING_REFF"].ToString());

                txtQuantity.Text = dv[0]["QUANTITY"].ToString();
                DDLCaseBox.SelectedValue = dv[0]["CR_UNIT"].ToString();
                //txtNoOfCaseBox.Text = dv[0]["CR_UNIT"].ToString();
                txtEndUse.SelectedIndex = txtEndUse.Items.IndexOf(txtEndUse.Items.FindByValue(dv[0]["END_USE"].ToString()));
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                txtMake.Text = dv[0]["MAKE"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtReqDate.Text = dv[0]["REQ_DATE"].ToString();
                ViewState["UNIQUE_ID"] = UniqueId;
                // ViewState["GREY_YARN_CODE"] = GreyYarnCode;
            }
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
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
                   // bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.InsertCustomerRequestForST(oOD_CUSTOMER_RQST_MST, dtOrderST);

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

            if (txtPartyCode.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Customer Name. ";
                msgCount += 1;
            }
            //if (txtBranch.Text != string.Empty)
            //    count = count + 1;
            //else
            //{
            //    msg = msg + msgCount.ToString() + ": Please select Branch. ";
            //    msgCount += 1;
            //}
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

    protected void imgbtnFind_Click1(object sender, ImageClickEventArgs e)
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

    protected void cmbOrderNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetCRItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbOrderNo.Items.Clear();
                cmbOrderNo.DataSource = data;
                cmbOrderNo.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Number Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetCRItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT ORDER_NO, ORDER_TYPE, ORDER_CAT,ORDER_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE) AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE <> 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE ORDER_NO LIKE :SearchQuery OR ORDER_CAT LIKE :SearchQuery OR ORDER_TYPE LIKE :SearchQuery ORDER BY ORDER_NO) www WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND Combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT ORDER_NO, ORDER_TYPE, ORDER_CAT,ORDER_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE) AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE ='" + PRODUCT_TYPE + "' AND ORDER_TYPE <>'DEVELOPMENT' AND comp_code ='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR ='" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE ORDER_NO LIKE :SearchQuery OR ORDER_CAT LIKE :SearchQuery OR ORDER_TYPE LIKE :SearchQuery ORDER BY ORDER_NO) www WHERE ROWNUM <= '" + startOffset + "') ";
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

    protected int GetCRItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT ORDER_NO, ORDER_TYPE, ORDER_CAT,ORDER_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| ORDER_TYPE|| '@'|| ORDER_CAT|| '@'|| PRODUCT_TYPE|| '@'|| ORDER_NO|| '@'|| BUSINESS_TYPE) AS Combined FROM v_OD_CUSTOMER_REQUEST_MST WHERE DEL_STATUS = '0' AND PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND ORDER_TYPE <> 'DEVELOPMENT' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE ORDER_NO LIKE :SearchQuery OR ORDER_CAT LIKE :SearchQuery OR ORDER_TYPE LIKE :SearchQuery ORDER BY ORDER_NO) www ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ORDER_NO ";
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
                txtCrLocation.Text = dt.Rows[0]["ORDER_CAT"].ToString();
                txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString();
                txtCustomerReffNo.Text = dt.Rows[0]["ORDER_REFF_NO"].ToString();
                txtAddress.Text = cmbPartyCode.SelectedText.Trim();
                ddlOrderType.SelectedValue = dt.Rows[0]["ORDER_TYPE"].ToString();

                ddlBusinessType.SelectedValue = dt.Rows[0]["BUSINESS_TYPE"].ToString();
                ddlDeliveryMode.SelectedValue = dt.Rows[0]["DELIVERY_MODE"].ToString();
                TxtDocumentDate.Text = dt.Rows[0]["DOCUMENT_DATE"].ToString();
                txtDirectBilling.Text = dt.Rows[0]["DIRECT_BILLING"].ToString();
                txtMstRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                txtAgent.Text = dt.Rows[0]["AGENT"].ToString();
                ddlBillingMode.SelectedIndex = ddlBillingMode.Items.IndexOf(ddlBillingMode.Items.FindByText(dt.Rows[0]["BILLING_MODE"].ToString()));
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
                    drST["SHADE_FAMILY_CODE"] = dr["SHADE_FAMILY_CODE"];
                    drST["SHADE_FAMILY_NAME"] = dr["SHADE_FAMILY_NAME"];
                    drST["SHADE_CODE"] = dr["SHADE_CODE"];
                    drST["SHADE_NAME"] = dr["SHADE_NAME"];
                    drST["MATCHING_REFF"] = dr["MATCHING_REFF"];
                    drST["QUANTITY"] = dr["QUANTITY"];
                    drST["CR_UNIT"] = dr["CR_UNIT"];
                    drST["END_USE"] = dr["END_USE"];
                    drST["REMARKS"] = dr["REMARKS"];
                    drST["MATCHING_REFF_NAME"] = dr["MATCHING_REFF"];
                    drST["END_USE_NAME"] = dr["END_USE_NAME"];
                    drST["MAKE"] = dr["MAKE"];
                    drST["NO_OF_UNIT"] = dr["NO_OF_UNIT"];
                    drST["WEIGHT_OF_UNIT"] = dr["WEIGHT_OF_UNIT"];
                    drST["REQ_DATE"] = dr["REQ_DATE"];
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
               // bResult = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.UpdateCustomerRequestST(oOD_CUSTOMER_RQST_MST, dtOrderST);

                if (bResult)
                {
                    string Resultmsg = "Customer Request Updated Successfully" + "\\r\\n";
                    Resultmsg += "Customer Request No is:" + oOD_CUSTOMER_RQST_MST.ORDER_NO;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                    InitialiseData();
                    //dtOrderST.Rows.Clear();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void EnablePrimaryFields()
    {
        try
        {
            //ddlOrderCategory.Enabled = true;
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
            //ddlOrderCategory.Enabled = false;
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
            //if (ddlOrderCategory.SelectedIndex != -1)
            //{
            BindOrderNo();
            //}
            //else
            //{
            //    Common.CommonFuction.ShowMessage("Please Select Request Order");
            //    txtOrderNo.Text = string.Empty;
            //}
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    //protected void ddlShade_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindShadeCode(ddlShadeFamily.SelectedValue.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Shade Family Selection .\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    //private void BindShadeCode(string ShadeFamilyCode)
    //{
    //    try
    //    {
    //        DataTable data = new DataTable();
    //        data = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeTrnByComAndMstCode(oUserLoginDetail.COMP_CODE, ddlShadeFamily.SelectedValue.ToString());
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

    //protected void ddlShadeCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlShadeFamily.SelectedValue != "SELECT" && ddlShadeCode.SelectedValue != "SELECT")
    //        {
    //            txtMatchingReff.LoadData(ddlShadeFamily.SelectedValue.Trim(), ddlShadeCode.SelectedValue.Trim());
    //        }
    //        else
    //        {
    //            Common.CommonFuction.ShowMessage("Please select Shade");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Code Selection.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

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
            string CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE,YARN_CAT , (   TKTNO  || '@'  || MAKE  || '@'  || ENDUSE  || '@'  || YARN_CODE  || '@'  || UNITWT  || '@'  || NET_BOX_WT  || '@'  || NET_CART_WT  || '@'  || UOM  )    AS Combined  FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery          ORDER BY   YARN_CODE) asd WHERE   YARN_CAT  = 'SEWING THREAD' AND ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE,YARN_CAT , (TKTNO || '@' || MAKE || '@' || ENDUSE || '@' || YARN_CODE || '@' || UNITWT || '@' || NET_BOX_WT || '@' || NET_CART_WT || '@'  || UOM )  AS Combined  FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE  YARN_CAT = 'SEWING THREAD' AND ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
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
        try
        {
            string CommandText = "SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE,YARN_CAT, (TKTNO || '@' || MAKE || '@' || ENDUSE || '@' || YARN_CODE || '@' || UNITWT || '@' || NET_BOX_WT || '@' || NET_CART_WT || '@'  || UOM )  AS Combined   FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE  YARN_CAT  = 'SEWING THREAD' ";
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

    protected void cmbArticleNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {

        try
        {
            string ArticleNo = cmbArticleNo.SelectedText.ToString();
            string[] arrString = cmbArticleNo.SelectedValue.Split('@');
            txtTktNo.Text = arrString[0].ToString();
            txtMake.Text = arrString[1].ToString();
            txtItemCodeLabel.Text = ArticleNo;
            txtEndUse.SelectedIndex = txtEndUse.Items.IndexOf(txtEndUse.Items.FindByValue(arrString[2].ToString()));
            //lblUNIT_WT.Text = arrString[4].ToString();
            //lblNETBOX_WT.Text = arrString[5].ToString();
            //lblNETCART_WT.Text = arrString[6].ToString();
            DDLCaseBox.SelectedIndex = DDLCaseBox.Items.IndexOf(DDLCaseBox.Items.FindByValue(arrString[7].ToString()));

            BindDetailByCaseSelection();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article No Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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

    private void BindDetailByCaseSelection()
    {

        try
        {
            string ArticleCode = txtItemCodeLabel.Text.Trim();
            DataTable dt = SaitexDL.Interface.Method.YRN_MST.GetArticleDetailByYarnCode(ArticleCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblUNIT_WT.Text = dt.Rows[0]["UNITWT"].ToString();
                lblNETBOX_WT.Text = dt.Rows[0]["NET_BOX_WT"].ToString();
                lblNETCART_WT.Text = dt.Rows[0]["NET_CART_WT"].ToString();
            }

            if ((DDLCaseBox.SelectedItem.ToString().Equals("BASE UNIT", StringComparison.OrdinalIgnoreCase)) || (DDLCaseBox.SelectedItem.ToString().Equals("BASE", StringComparison.OrdinalIgnoreCase)))
            {
                txtWeightofUnit.Text = lblUNIT_WT.Text;
            }
            else if (DDLCaseBox.SelectedItem.ToString().Equals("BOX", StringComparison.OrdinalIgnoreCase))
            {
                txtWeightofUnit.Text = lblNETBOX_WT.Text;

            }
            else if (DDLCaseBox.SelectedItem.ToString().Equals("CARTON", StringComparison.OrdinalIgnoreCase))
            {
                txtWeightofUnit.Text = lblNETCART_WT.Text;

            }
            else if (DDLCaseBox.SelectedItem.ToString().Equals("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                txtWeightofUnit.Text = string.Empty;
            }
            else
            {
                txtWeightofUnit.Text = string.Empty;
            }

            double WeightofUnit = 0;
            double.TryParse(txtWeightofUnit.Text, out WeightofUnit);

            double NoofUnit = 0;
            double.TryParse(txtNoofUnit.Text, out NoofUnit);

            txtQuantity.Text = (WeightofUnit * NoofUnit).ToString();
        }
        catch
        {
            throw;
        }
    }

    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double WeightofUnit = 0;
            double.TryParse(txtWeightofUnit.Text, out WeightofUnit);
            double NoofUnit = 0;
            double.TryParse(txtNoofUnit.Text, out NoofUnit);
            txtQuantity.Text = (WeightofUnit * NoofUnit).ToString();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in No Of Unit TextChanged.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtWeightofUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double WeightofUnit = 0;
            double.TryParse(txtWeightofUnit.Text, out WeightofUnit);
            double NoofUnit = 0;
            double.TryParse(txtNoofUnit.Text, out NoofUnit);
            txtQuantity.Text = (WeightofUnit * NoofUnit).ToString();
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
                ddlShadeFamily.Text = arrString[0].ToString();
                txtShadeFamilyName.Text = arrString[1].ToString();
                ddlShadeCode.Text = arrString[2].ToString();
                txtShadeName.Text = arrString[3].ToString();

                txtMatchingReff.LoadData(ddlShadeFamily.Text.Trim(), ddlShadeCode.Text.Trim());
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
                CommandText = " SELECT PRTY_CODE, PRTY_NAME,(PRTY_NAME || PRTY_ADD1) Address, PRTY_GRP_CODE, PARTY_CODE  FROM  ( SELECT TV.PRTY_CODE, TV.PRTY_NAME, TV.PRTY_ADD1, TV.PRTY_GRP_CODE, BR.PARTY_CODE  FROM TX_VENDOR_MST TV, CM_BRANCH_MST BR  WHERE   TV.PRTY_CODE LIKE :SearchQuery OR TV.PRTY_NAME LIKE :SearchQuery  ORDER BY   TV.PRTY_CODE ASC) asd WHERE   PRTY_CODE = PARTY_CODE AND ROWNUM <= 15 ";
                whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM  ( SELECT TV.PRTY_CODE, TV.PRTY_NAME, TV.PRTY_ADD1, TV.PRTY_GRP_CODE, BR.PARTY_CODE  FROM TX_VENDOR_MST TV, CM_BRANCH_MST BR  WHERE   TV.PRTY_CODE LIKE :SearchQuery OR TV.PRTY_NAME LIKE :SearchQuery  ORDER BY   TV.PRTY_CODE ASC) asd WHERE PRTY_CODE = PARTY_CODE AND ROWNUM <= " + startOffset + ")";
                }
            }
            else if (ddlBillingMode.SelectedItem.ToString() == "Direct Billing")
            {

                CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
                whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
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
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }



}