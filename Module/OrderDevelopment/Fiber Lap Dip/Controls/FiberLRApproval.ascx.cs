using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common;
using errorLog;
using System.IO;
using DBLibrary;
using Obout.Grid;

public partial class Module_OrderDevelopment_LabDip_Controls_FiberLRApproval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FIBER_OD_SHADE_FAMILY oOD_SHADE_FAMILY;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }

            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('LR Approved Successfully!');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }
                Session["saveStatus"] = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
        }
    }
    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdFind.Visible = true;
            txtApprovalDate.Text = System.DateTime.Now.ToShortDateString();
            txtApprovedBy.Text = oUserLoginDetail.Username;
            Session["saveStatus"] = 0;
            //BindOrderNo();
            BindShade();

        }
        catch
        {
            throw;
        }
    }

    //private void BindOrderNo()
    //{
    //    try
    //    {
    //        ddlOrderNo.Items.Clear();
    //        DataTable dtOrder = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetLabDipOrderForSubmission(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
    //        if (dtOrder != null && dtOrder.Rows.Count > 0)
    //        {
    //            ddlOrderNo.DataSource = dtOrder;
    //            ddlOrderNo.DataBind();
    //            ddlOrderNo.Items.Insert(0, new ListItem("---- Select Order No ----", "0"));
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}  

    private void ClearControls()
    {
        try
        {
            txtOrderDate.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtBranchCode.Text = string.Empty;

            if (ddlLRNo.Items.Count > 0)
                ddlLRNo.Items.Clear();

            if (rdoLstOption.Items.Count > 0)
            {
                rdoLstOption.Items.Clear();
                grdDyeName.DataSource = null;
                grdDyeName.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlLRNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOrderNo.SelectedIndex != -1)
            {
                if (rdoLstOption.Items.Count > 0)
                {
                    rdoLstOption.Items.Clear();
                    grdDyeName.DataSource = null;
                    grdDyeName.DataBind();
                }

                DataTable dtMst = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetST_LabDip_MstByOrderAndLRNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlOrderNo.SelectedValue.Trim().ToString(), ddlLRNo.SelectedItem.Text.Trim());
                if (dtMst != null && dtMst.Rows.Count > 0)
                {
                    for (int iloop = 0; iloop < dtMst.Rows.Count; iloop++)
                    {
                        rdoLstOption.DataSource = dtMst;
                        txtGreyLotNo.Text = dtMst.Rows[iloop]["GREY_LOT_NO"].ToString();
                        txtArticalCode.Text = dtMst.Rows[iloop]["ARTICAL_NO"].ToString();
                        txtArticalDesc.Text = dtMst.Rows[iloop]["ARTICAL_DESC"].ToString();
                        txtShadeCode.Text = dtMst.Rows[iloop]["SHADE_GROUP"].ToString();
                        rdoLstOption.DataValueField = "LR_OPTION";
                        rdoLstOption.DataTextField = "LR_OPTION_DTL";
                        rdoLstOption.DataBind();
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("There is no Group Code or Options are available for Approval, Please select different LR Number for Approval..");
                    ddlLRNo.SelectedIndex = 0;
                }

                // BindShadeCodeAuto();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please select Customer Request Number first..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of LR Number..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
        }
    }

    private void InsertData()
    {
        try
        {
            bool bChecked = false;
            string LR_OPTION = string.Empty;

            if (txtApprovalDate.Text != "")
            {
                if (ddlOrderNo.SelectedIndex != -1)
                {
                    if (ddlLRNo.SelectedIndex != 0)
                    {
                        if (rdoLstOption.Items.Count > 0)
                        {
                            foreach (ListItem item in rdoLstOption.Items)
                            {
                                if (item.Selected == true)
                                {
                                    bChecked = true;
                                    LR_OPTION = rdoLstOption.SelectedValue.ToString().Trim();
                                }
                            }

                            if (bChecked)
                            {
                                int iFound = 0;

                                if (iFound > 0)
                                {
                                    CommonFuction.ShowMessage("Only one option can be approved.. Option already exists..");
                                }
                                else
                                {
                                    bool bResult = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.Insert_LRApproval(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlOrderNo.SelectedValue.Trim().ToString(), ddlLRNo.SelectedItem.Text.Trim(), LR_OPTION, oUserLoginDetail.UserCode, txtApprovalDate.Text.Trim(), txtRemarks.Text.Trim(), ddlShadeCat.SelectedValue.Trim(), ddlNatureShade.SelectedValue.Trim(), ddlShadeFamily.SelectedValue.Trim(), txtShadeCodeAuto.Text.Trim(), txtShadeCode.Text.Trim(), txtOrderRefNo.Text);
                                    if (bResult)
                                    {
                                        Session["saveStatus"] = 1;
                                        Common.CommonFuction.ShowMessage("Approved succesfully.");
                                        Response.Redirect("./FiberLRApproval.aspx?cId=S", false);
                                    }
                                    else
                                    {
                                        Common.CommonFuction.ShowMessage("Error.. in Saving LR Approval.");
                                    }
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Dear! Please select Option for Approval..");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("There is no Option remaining for Approval, Please select different LR Number..");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please select LR Number..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please select Order Number first..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please provide Approval Date..");
            }
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
            // UpdateData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the Data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Common.CommonFuction.ShowMessage("Sorry Dear ! No Deletion allowed..");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deletion.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./LRApproval.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/LR_APPROVAL_REPORT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
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

    protected void rdoLstOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOrderNo.SelectedIndex != -1)
            {
                if (ddlLRNo.SelectedIndex != 0)
                {
                    grdDyeName.DataSource = null;
                    grdDyeName.DataBind();

                    DataTable dt = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetST_LabDip_TrnByOrderAndLRNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlOrderNo.SelectedValue.Trim().ToString(), ddlLRNo.SelectedItem.Text.Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataView dv = new DataView(dt);
                        dv.RowFilter = "LR_OPTION='" + rdoLstOption.SelectedValue.ToString().Trim() + "'";
                        if (dv.Count > 0)
                        {
                            grdDyeName.DataSource = dv;
                            grdDyeName.DataBind();
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Sorry Dear! There is no Dye Detail found in the DataBase..");
                        }
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please select LR Number..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please select Customer Request Number first..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in RadioButtonList Selecting Index Event..\r\nSee error log for detail."));
        }
    }

    protected void ddlOrderNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            if (data != null && data.Rows.Count > 0)
            {
                ddlOrderNo.Items.Clear();
                ddlOrderNo.DataSource = data;
                ddlOrderNo.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
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
            CommandText = " SELECT   *  FROM   (SELECT   *  FROM (SELECT  DISTINCT (ORDER_NO) AS ORDER_NO,BRANCH_CODE,BRANCH_NAME,CR_BUSINESS_TYPE,CR_PRODUCT_TYPE,order_ref_no FROM   V_TX_FIBER_OD_LAB_DIP_ENTRY V   WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE AND TUSER='" + oUserLoginDetail.UserCode + "'  AND v.LAB_DIP_NO NOT IN (SELECT LAB_DIP_ENTRY FROM V_TX_ST_LABDIP_SUB_MST B WHERE NVL (IS_APPROVED, 0) = '1' AND COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE AND b.ORDER_NO = v.ORDER_NO AND V.YEAR = B.YEAR) ORDER BY   ORDER_NO ) asd WHERE    ORDER_NO LIKE :SearchQuery OR CR_PRODUCT_TYPE LIKE :SearchQuery) bd where ROWNUM <= 200 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND ORDER_NO NOT IN (SELECT   *  FROM   (SELECT   *  FROM (SELECT  DISTINCT (ORDER_NO) AS ORDER_NO,BRANCH_CODE,BRANCH_NAME,CR_BUSINESS_TYPE,CR_PRODUCT_TYPE FROM   V_TX_FIBER_OD_LAB_DIP_ENTRY V   WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE and TUSER='" + oUserLoginDetail.UserCode + "' AND v.LAB_DIP_NO NOT IN (SELECT LAB_DIP_ENTRY FROM V_TX_ST_LABDIP_SUB_MST B WHERE NVL (IS_APPROVED, 0) = '1' AND COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE AND b.ORDER_NO = v.ORDER_NO AND V.YEAR = B.YEAR)  ORDER BY   ORDER_NO ) asd  WHERE ORDER_NO LIKE :SearchQuery OR CR_PRODUCT_TYPE LIKE :SearchQuery) bd WHERE  ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " ORDER BY ORDER_NO ASC";
            string SearchQuery = "%" + text + "%";
            DataTable data = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);

            return data;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of Order Number..\r\nSee error log for detail."));
            return null;
        }
    }

    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = "  SELECT   *  FROM   (SELECT   *  FROM (SELECT  DISTINCT (ORDER_NO) AS ORDER_NO,BRANCH_CODE,BRANCH_NAME,CR_BUSINESS_TYPE,CR_PRODUCT_TYPE,order_ref_no FROM   V_TX_FIBER_OD_LAB_DIP_ENTRY V   WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE AND v.LAB_DIP_NO NOT IN (SELECT LAB_DIP_ENTRY FROM V_TX_ST_LABDIP_SUB_MST B WHERE NVL (IS_APPROVED, 0) = '1' AND COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE AND b.ORDER_NO = v.ORDER_NO) ORDER BY   ORDER_NO ) asd WHERE    ORDER_NO LIKE :SearchQuery OR CR_PRODUCT_TYPE LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ORDER_NO ASC";
            string SearchQuery = "%" + text + "%";
            DataTable data = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlOrderNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            ddlLRNo.Items.Clear();
            if (ddlOrderNo.SelectedIndex != -1)
            {
                ClearControls();
                DataTable dtOrder = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetLabDipDTLByORDERForSubmission(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlOrderNo.SelectedValue.Trim());
                if (dtOrder != null && dtOrder.Rows.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dtOrder.Rows.Count; iLoop++)
                    {
                        txtOrderDate.Text = dtOrder.Rows[iLoop]["ORDER_DATE"].ToString();
                        txtBranchCode.Text = dtOrder.Rows[iLoop]["BRANCH_CODE"].ToString();
                        txtBranch.Text = dtOrder.Rows[iLoop]["BRANCH_NAME"].ToString();
                        txtOrderRefNo.Text = dtOrder.Rows[iLoop]["ORDER_REF_NO"].ToString();
                    }
                }

                DataTable dtMst = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetST_LabDip_MstUnApprovedLRNew(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlOrderNo.SelectedValue.Trim().ToString());
                if (dtMst != null && dtMst.Rows.Count > 0)
                {
                    ddlLRNo.DataSource = dtMst;
                    ddlLRNo.DataBind();
                }
                ddlLRNo.Items.Insert(0, new ListItem("------ Selct LR No ------", "0"));
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Customer Request Number..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of Order Number..\r\nSee error log for detail."));
        }
    }

    protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlShadeFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlShadeFamily.SelectedValue != "select")
            {
                //ddlShadeFamily.Text = string.Empty;
                //BindShadeName(ddlShadeFamily.SelectedValue.ToString());
                //BindShadeCodeAuto();
                txtShadeCodeAuto.Text = BindShadeCodeAuto();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Shade..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of Shade DropDown..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindShade()
    {
        try
        {
            oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.FIBER_OD_SHADE_FAMILY();
            oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            DataTable dt = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GetShadeFamilyCodeALL(oOD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeFamily.Items.Clear();
                ddlShadeFamily.DataSource = dt;
                ddlShadeFamily.DataBind();
                ddlShadeFamily.Items.Insert(0, new ListItem("--Select Shade--", "select"));
                // ddlShadeFamily.Items.Insert(0, new ListItem("NONE", "NONE"));
            }
        }
        catch
        {
            throw;
        }
    }


    private string BindShadeCodeAuto()
    {
        string sNewItemCode = string.Empty;

        try
        {
            string sItemCategory = txtShadeCode.Text.Trim();
            string PrefixString = string.Empty;
            if (sItemCategory.Length > 2)
            {
                PrefixString = sItemCategory.Substring(0, 3);

                DataTable dt = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GetMaxShadeCodeNo(PrefixString.ToUpper(), oUserLoginDetail.COMP_CODE);

                double NewItmCode = 0;

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
                    {
                        double sTempCode = Convert.ToDouble(dt.Rows[iLoop]["TItem_Code"].ToString());
                        if (sTempCode > NewItmCode)
                            NewItmCode = sTempCode;
                    }
                }

                double iNewItmCode = NewItmCode + 1;
                if (Convert.ToInt64(iNewItmCode) < 10)
                {
                    sNewItemCode = PrefixString + "0000" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 100)
                {
                    sNewItemCode = PrefixString + "000" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 1000)
                {
                    sNewItemCode = PrefixString + "00" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 10000)
                {
                    sNewItemCode = PrefixString + "0" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 100000)
                {
                    sNewItemCode = PrefixString + "" + iNewItmCode.ToString();
                }
            }

            return sNewItemCode;
        }
        catch
        {
            return sNewItemCode;
        }
    }
   
}