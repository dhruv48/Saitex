using System;
using System.Data;
using System.Web.UI;

using Common;

public partial class Module_Production_Controls_OrderPacking : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.YRN_PROD_PACKING oYRN_PROD_PACKING;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (oUserLoginDetail.VC_DEPARTMENTCODE == "D00006")
            {
                tblMain.Visible = true;
                if (!IsPostBack)
                {
                    InitialisePage();
                }

                if (Convert.ToInt16(Session["saveStatus"]) == 1)
                {
                    if (Request.QueryString["cId"].ToString().Trim() == "S")
                    {
                        string strPID = Request.QueryString["PID"].ToString().Trim();
                        Common.CommonFuction.ShowMessage("Order Packing Saved Successfully ! And Your Packing ID is : " + strPID);
                    }
                    if (Request.QueryString["cId"].ToString().Trim() == "U")
                    {
                        Common.CommonFuction.ShowMessage("Order Packing Updated Successfully !");
                    }
                    if (Request.QueryString["cId"].ToString().Trim() == "D")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", false);
                    }
                    Session["saveStatus"] = 0;
                }
            }
            else
            {
                string url = string.Empty;
                if (Session["RedirectURL"] != null)
                {
                    url = Session["RedirectURL"].ToString();
                }
                else
                {
                    url = "~/Admin/Pages/welcome.aspx";
                }

                Common.CommonFuction.ShowMessage(@"Packing can be done by only Packaging Department.\r\nYou will be redirected to Welcome page automaticallly within few seconds");

                Response.AddHeader("REFRESH", ".02;URL='" + ResolveUrl(url) + "'");

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Save Mode";
            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;

            txtPackingID.Visible = true;
            cmbPackingID.Visible = false;
            ddlPackingCategory.Enabled = true;

            ddlUOMOfUnit.Enabled = true;
            txtPackingDate.Text = DateTime.Now.Date.ToShortDateString();
            txtPackingID.Text = BindMaxPackingID();

            BindShiftMst();
        }
        catch
        {
            throw;
        }
    }

    private void ClearData()
    {
        try
        {
            ddlPackingCategory.SelectedIndex = 0;
            txtPackingDate.Text = string.Empty;
            txtOrderNumber.Text = string.Empty;
            txtLOTIDNumber.Text = string.Empty;
            txtArticleCode.Text = string.Empty;
            txtNoOfCone.Text = string.Empty;
            txtWtPerCone.Text = string.Empty;

            ddlUOMOfUnit.SelectedIndex = 0;
            txtProduction.Text = string.Empty;
            txtShift.Text = string.Empty;
            txtEfficiency.Text = string.Empty;
            txtRemarks.Text = string.Empty;

        }
        catch
        {
            throw;
        }
    }

    private string BindMaxPackingID()
    {
        try
        {
            string strID = string.Empty;
            int y = SaitexBL.Interface.Method.YRN_PROD_PACKING.GetMaxPackingID();
            y = y + 1;
            strID = y.ToString();

            return strID;
        }
        catch
        {
            throw;
        }
    }

    private void BindShiftMst()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.Get_ShiftMst();
            if (dt != null && dt.Rows.Count > 0)
            {
                txtShift.Items.Clear();
                txtShift.DataSource = dt;
                txtShift.DataTextField = "SFT_NAME";
                txtShift.DataValueField = "SFT_ID";
                txtShift.DataBind();
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
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InsertData()
    {
        try
        {
            string strID = string.Empty;
            int dblPckID = 0;
            if (CheckValidation())
            {
                GetProductionInKG();

                oYRN_PROD_PACKING = new SaitexDM.Common.DataModel.YRN_PROD_PACKING();

                oYRN_PROD_PACKING.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRN_PROD_PACKING.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oYRN_PROD_PACKING.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oYRN_PROD_PACKING.PACKING_DATE = DateTime.Parse(txtPackingDate.Text.Trim());
                oYRN_PROD_PACKING.PCK_CODE = string.Empty;
                oYRN_PROD_PACKING.PACKING_CAT = ddlPackingCategory.SelectedValue.ToString().Trim();
                oYRN_PROD_PACKING.ORDER_NO = txtOrderNumber.Text.Trim();
                oYRN_PROD_PACKING.PI_NO = txtPANo.Text.ToString().Trim();
                oYRN_PROD_PACKING.LOT_ID_NO = txtLOTIDNumber.Text.Trim();
                oYRN_PROD_PACKING.ARTICAL_CODE = txtArticleCode.Text.Trim();
                oYRN_PROD_PACKING.NO_OF_UNIT = double.Parse(txtNoOfCone.Text.Trim());
                oYRN_PROD_PACKING.UOM_OF_UNIT = ddlUOMOfUnit.SelectedValue.ToString().Trim();
                oYRN_PROD_PACKING.WEIGHT_OF_UNIT = double.Parse(txtWtPerCone.Text.Trim());
                oYRN_PROD_PACKING.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                oYRN_PROD_PACKING.TUSER = oUserLoginDetail.UserCode;
                strID = BindMaxPackingID();
                int.TryParse(strID, out dblPckID);
                oYRN_PROD_PACKING.PACKING_ID = dblPckID;
                oYRN_PROD_PACKING.PACK_LOT_NO = txtPackLotNo.Text.Trim();
                oYRN_PROD_PACKING.PRODUCTION = double.Parse(txtProduction.Text.Trim());
                oYRN_PROD_PACKING.SHIFT = txtShift.Text.ToUpper().Trim();
                oYRN_PROD_PACKING.EFFICIENCY = txtEfficiency.Text.Trim();
                oYRN_PROD_PACKING.REMARKS = txtRemarks.Text.Trim();
                oYRN_PROD_PACKING.CHECKED_BY = txtCheckedBy.Text;
                oYRN_PROD_PACKING.SHADE_CODE = txtShadeCode.Text;
                oYRN_PROD_PACKING.PACK_GRADE = txtPackGrade.Text;
                oYRN_PROD_PACKING.FR_PROS_CODE = txtFrProcessCode.Text;

                int iRecordFound = 0;
                bool bResult = false;// SaitexBL.Interface.Method.YRN_PROD_PACKING.Insert(oYRN_PROD_PACKING, out iRecordFound);
                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("./OrderPacking.aspx?cId=S&PID=" + dblPckID, false);
                }
                else if (iRecordFound > 0)
                {
                    Common.CommonFuction.ShowMessage("This Record is already saved.. Please enter another.");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Details Saving failed..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please provide some required field denoted with(*) mark..");
            }
        }
        catch
        {
            throw;
        }
    }

    private bool CheckValidation()
    {
        try
        {
            bool IsValidation = false;

            if (txtPANo.Text != string.Empty)
            {
                string Pack_Code = string.Empty;

                if (txtPackingDate.Text != "")
                {
                    if (txtOrderNumber.Text != "")
                    {
                        double noofunit = 0;
                        double.TryParse(txtNoOfCone.Text, out noofunit);
                        if (noofunit > 0)
                        {
                            double unitWeight = 0;
                            double.TryParse(txtWtPerCone.Text, out unitWeight);
                            if (unitWeight > 0)
                            {
                                double packqty = 0;
                                double.TryParse(txtProduction.Text, out packqty);
                                if (packqty > 0)
                                {
                                    if (txtShift.SelectedValue != "select")
                                    {
                                        IsValidation = true;
                                    }
                                    else
                                    {
                                        IsValidation = false;
                                        Common.CommonFuction.ShowMessage("Dear! Please select Shift..");
                                    }
                                }
                                else
                                {
                                    IsValidation = false;
                                    Common.CommonFuction.ShowMessage("Dear! Please enter Production..");
                                }
                            }
                            else
                            {
                                IsValidation = false;
                                Common.CommonFuction.ShowMessage("Dear! Please enter Weight Per Cone..");
                            }
                        }
                        else
                        {
                            IsValidation = false;
                            Common.CommonFuction.ShowMessage("Dear! Please enter Number Of Cone..");
                        }
                    }
                    else
                    {
                        IsValidation = false;
                        Common.CommonFuction.ShowMessage("Dear! Please provide Order Number..");
                    }
                }
                else
                {
                    IsValidation = false;
                    Common.CommonFuction.ShowMessage("Dear! Please enter Packing Date..");
                }
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please provide PI Number..");
            }
            return IsValidation;
        }
        catch
        {
            return false;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
            txtPackingID.Visible = false;
            cmbPackingID.Visible = true;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void UpdateData()
    {
        try
        {
            if (CheckValidation())
            {
                oYRN_PROD_PACKING = new SaitexDM.Common.DataModel.YRN_PROD_PACKING();

                oYRN_PROD_PACKING.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRN_PROD_PACKING.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oYRN_PROD_PACKING.PACKING_DATE = DateTime.Parse(txtPackingDate.Text.Trim());
                oYRN_PROD_PACKING.NO_OF_UNIT = double.Parse(txtNoOfCone.Text.Trim());
                oYRN_PROD_PACKING.WEIGHT_OF_UNIT = double.Parse(txtWtPerCone.Text.Trim());
                oYRN_PROD_PACKING.PACKING_ID = int.Parse(cmbPackingID.SelectedValue.ToString().Trim());
                oYRN_PROD_PACKING.PRODUCTION = double.Parse(txtProduction.Text.Trim());
                oYRN_PROD_PACKING.SHIFT = txtShift.Text.Trim();
                oYRN_PROD_PACKING.EFFICIENCY = txtEfficiency.Text.Trim();
                oYRN_PROD_PACKING.REMARKS = txtRemarks.Text.Trim();
                oYRN_PROD_PACKING.EFFICIENCY = txtEfficiency.Text.Trim();
                oYRN_PROD_PACKING.REMARKS = txtRemarks.Text.Trim();
                oYRN_PROD_PACKING.CHECKED_BY = txtCheckedBy.Text;
                oYRN_PROD_PACKING.UOM_OF_UNIT = ddlUOMOfUnit.SelectedValue.Trim();
                oYRN_PROD_PACKING.PACK_GRADE = txtPackGrade.Text;
                oYRN_PROD_PACKING.PACKING_CAT = ddlPackingCategory.SelectedValue.ToString().Trim();
                oYRN_PROD_PACKING.PACK_LOT_NO = txtPackLotNo.Text;

                int iRecordFound = 0;
                bool bResult = false;// SaitexBL.Interface.Method.YRN_PROD_PACKING.Update(oYRN_PROD_PACKING, out iRecordFound);
                if (bResult)
                {
                    txtPackingDate.Enabled = true;
                    Session["saveStatus"] = 1;
                    Response.Redirect("./OrderPacking.aspx?cId=U", false);
                }
                else if (iRecordFound > 0)
                {
                    Common.CommonFuction.ShowMessage("This Record is already saved.. Please enter another.");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Details Saving failed..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please provide some required field denoted with(*) mark..");
            }
        }
        catch
        {
            throw;
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
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./OrderPacking.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void ddlUOMOfUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetWeightOfUnit();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void GetWeightOfUnit()
    {
        try
        {
            if (txtArticleCode.Text != "")
            {
                string ArticleCode = txtArticleCode.Text;
                string BaseUOM = ddlUOMOfUnit.SelectedItem.Text;

                double WeightOfUnit = SaitexBL.Interface.Method.YRN_MST.GetWeightOfUnitByArticle(oUserLoginDetail.COMP_CODE, ArticleCode, BaseUOM);
                txtWtPerCone.Text = WeightOfUnit.ToString();

                GetProductionInKG();
            }
            else
            {
                CommonFuction.ShowMessage("Please select Article First.");
            }
        }
        catch
        {
            throw;
        }
    }

    private void GetProductionInKG()
    {
        try
        {
            double Qty = 0;
            double NoUnit = 0;
            double WeightOfUnit = 0;
            double maxqtytopack = 0;
            double.TryParse(txtNoOfCone.Text, out NoUnit);
            double.TryParse(txtWtPerCone.Text, out WeightOfUnit);
            double.TryParse(txtLotRemQty.Text, out maxqtytopack);
            Qty = WeightOfUnit * NoUnit;
            if (Qty > maxqtytopack)
            {
                txtNoOfCone.Text = "0";
                txtProduction.Text = "0";
                CommonFuction.ShowMessage("Pack qty can not be more than bal to pack qty.");
            }
            else
            {
                txtProduction.Text = Qty.ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void cmbPackingID_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPackDataByID(e.Text.ToUpper().Trim(), e.ItemsOffset);
            cmbPackingID.DataTextField = "PACKING_ID";
            cmbPackingID.DataValueField = "PACKING_ID";
            cmbPackingID.DataSource = data;
            cmbPackingID.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPackCountByID(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Packing Details.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPackDataByID(string Text, int StartOffSet)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT PACKING_ID, PACKING_DATE, PI_NO, LOT_ID_NO, ARTICAL_CODE, SHADE_CODE, FR_PROS_CODE FROM YRN_PROD_PACKING WHERE BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ORDER BY PACKING_ID) WHERE PI_NO LIKE :SearchQuery OR LOT_ID_NO LIKE :SearchQuery OR PACKING_ID LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (StartOffSet != 0)
            {
                whereClause += " AND PACKING_ID NOT IN(SELECT PACKING_ID FROM (SELECT * FROM (SELECT PACKING_ID,PACKING_DATE,PI_NO,LOT_ID_NO,ARTICAL_CODE,SHADE_CODE,FR_PROS_CODE FROM YRN_PROD_PACKING WHERE BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND DEPT_CODE ='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' ORDER BY PACKING_ID) WHERE PI_NO LIKE :SearchQuery OR LOT_ID_NO LIKE :SearchQuery OR PACKING_ID LIKE :SearchQuery) WHERE ROWNUM <= '" + StartOffSet + "')";
            }

            string SortExpression = " order by PACKING_ID";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int GetPackCountByID(string Text)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT PACKING_ID, PACKING_DATE, PI_NO, LOT_ID_NO, ARTICAL_CODE, SHADE_CODE, FR_PROS_CODE FROM YRN_PROD_PACKING WHERE BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ORDER BY PACKING_ID) WHERE PI_NO LIKE :SearchQuery OR LOT_ID_NO LIKE :SearchQuery OR PACKING_ID LIKE :SearchQuery)";
            string whereClause = string.Empty;

            string SortExpression = " ";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void cmbPackingID_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtPackingID.Text = cmbPackingID.SelectedText.Trim();
            FillDataForEdit(cmbPackingID.SelectedValue.ToString().Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Packing Details.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void FillDataForEdit(string PCK_ID)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_PACKING.GetPackingDTLByPackingID(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PCK_ID);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlLotIdNo.Enabled = false;
                txtOrderNumber.Text = dt.Rows[0]["ORDER_NO"].ToString().Trim();
                txtArticleCode.Text = dt.Rows[0]["ARTICAL_CODE"].ToString().Trim();
                txtArticleDesc.Text = dt.Rows[0]["ORD_ARTICAL_DESC"].ToString().Trim();
                txtShadeCode.Text = dt.Rows[0]["SHADE_CODE"].ToString().Trim();
                txtOrderQty.Text = dt.Rows[0]["ORD_QTY"].ToString().Trim();
                txtPackingQty.Text = dt.Rows[0]["ORD_PACKING_QTY"].ToString().Trim();
                txtRemainingQty.Text = dt.Rows[0]["ORD_REM_QTY"].ToString().Trim();

                lblPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                lblPartyName.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                lblBusinessType.Text = dt.Rows[0]["BUSINESS_TYPE"].ToString().Trim();
                lblProductType.Text = dt.Rows[0]["PRODUCT_TYPE"].ToString().Trim();
                lblOrderCategory.Text = dt.Rows[0]["ORDER_CAT"].ToString().Trim();
                lblOrderTypeS.Text = dt.Rows[0]["ORDER_TYPE"].ToString().Trim();
                txtPANo.Text = dt.Rows[0]["PI_NO"].ToString().Trim();

                txtLOTIDNumber.Text = dt.Rows[0]["LOT_ID_NO"].ToString().Trim();
                txtLotQty.Text = dt.Rows[0]["STOCK_QTY"].ToString().Trim();
                txtLotPackQty.Text = dt.Rows[0]["LOT_QTY_PACK"].ToString().Trim();
                txtLotRemQty.Text = dt.Rows[0]["LOT_STOCK_BAL_QTY"].ToString().Trim();
                txtWtPerCone.Text = dt.Rows[0]["WEIGHT_OF_UNIT"].ToString().Trim();
                txtNoOfCone.Text = dt.Rows[0]["NO_OF_UNIT"].ToString().Trim();
                ddlUOMOfUnit.SelectedIndex = ddlUOMOfUnit.Items.IndexOf(ddlUOMOfUnit.Items.FindByValue("UOM_OF_UNIT"));
                txtFrProcessCode.Text = dt.Rows[0]["FR_PROS_CODE"].ToString().Trim();
                txtPackingDate.Text = DateTime.Parse(dt.Rows[0]["PACKING_DATE"].ToString().Trim()).ToShortDateString();

                txtShift.SelectedIndex = txtShift.Items.IndexOf(txtShift.Items.FindByValue("SHIFT"));
                txtEfficiency.Text = dt.Rows[0]["EFFICIENCY"].ToString().Trim();
                ddlPackingCategory.SelectedIndex = ddlPackingCategory.Items.IndexOf(ddlPackingCategory.Items.FindByValue("PACKING_CAT"));
                txtPackGrade.Text = dt.Rows[0]["PACK_GRADE"].ToString().Trim();
                txtPackLotNo.Text = dt.Rows[0]["PACK_LOT_NO"].ToString().Trim();
                txtCheckedBy.Text = dt.Rows[0]["CHECKED_BY"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString().Trim();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void txtNoOfCone_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUOMOfUnit.SelectedIndex != -1)
            {
                GetProductionInKG();
            }
            else
            {
                CommonFuction.ShowMessage("Please select UOM of UNIT.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Text Changed Event (No Of Cone).\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtWtPerCone_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GetProductionInKG();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Text Changed Event (Weight Per Cone).\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlLotIdNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            DataTable data = GetCompoDataForLOV(e.Text.ToUpper().Trim(), e.ItemsOffset);
            ddlLotIdNo.DataTextField = "LOT_NUMBER";
            ddlLotIdNo.DataValueField = "LOT_DATA";
            ddlLotIdNo.DataSource = data;
            ddlLotIdNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetCompoDataCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetCompoDataForLOV(string Text, int StartOffSet)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( ORDER_NO|| '@'|| LOT_NUMBER|| '@'|| DEPT_CODE|| '@'|| PROS_CODE) LOT_DATA, COMP_CODE, BRANCH_CODE, DEPT_CODE, ORDER_NO, LOT_NUMBER, PRTY_CODE, PROS_CODE, PRTY_NAME AS PARTY FROM V_YRN_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (StartOffSet != 0)
            {
                whereClause += " AND LOT_DATA NOT IN(SELECT LOT_DATA FROM (SELECT * FROM (SELECT ( ORDER_NO || '@' || LOT_NUMBER || '@' || DEPT_CODE || '@' || PROS_CODE) LOT_DATA,COMP_CODE,BRANCH_CODE,DEPT_CODE,ORDER_NO,LOT_NUMBER,PRTY_CODE,PROS_CODE,PRTY_NAME AS PARTY FROM V_YRN_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE ='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery) WHERE ROWNUM <= '" + StartOffSet + "')";
            }

            string SortExpression = " order by LOT_NUMBER";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int GetCompoDataCount(string Text)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( ORDER_NO|| '@'|| LOT_NUMBER|| '@'|| DEPT_CODE|| '@'|| PROS_CODE) LOT_DATA, COMP_CODE, BRANCH_CODE, DEPT_CODE, ORDER_NO, LOT_NUMBER, PRTY_CODE, PROS_CODE, PRTY_NAME AS PARTY FROM V_YRN_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery)  ";
            string whereClause = string.Empty;

            string SortExpression = " ";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlLotIdNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ddlLotIdNo.SelectedValue.Trim().ToString() != "0")
            {
                string LOT_DATA = ddlLotIdNo.SelectedValue.Trim();


                Load_Party_Detail_By_LotID(LOT_DATA);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Selecting Record"));
        }
    }

    private void Load_Party_Detail_By_LotID(string LOT_DATA)
    {
        try
        {

            char[] splitter = { '@' };
            string[] arrString = LOT_DATA.Split(splitter);
            string sORDER_NO = arrString[0].ToString();
            string sLOT_NUMBER = arrString[1].ToString();
            string sDEPT_CODE = arrString[2].ToString();
            string sPROS_CODE = arrString[3].ToString();

            DataTable dtProc = SaitexBL.Interface.Method.YRN_PROD_PACKING.GetLotToPackByPA(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, sORDER_NO.Trim(), oUserLoginDetail.VC_DEPARTMENTCODE);
            if (dtProc != null && dtProc.Rows.Count > 0)
            {

                txtOrderNumber.Text = dtProc.Rows[0]["MAIN_ORDER_NO"].ToString().Trim();
                txtArticleCode.Text = dtProc.Rows[0]["ORD_ARTICAL_CODE"].ToString().Trim();
                txtArticleDesc.Text = dtProc.Rows[0]["ORD_ARTICAL_DESC"].ToString().Trim();
                txtShadeCode.Text = dtProc.Rows[0]["ORD_SHADE_CODE"].ToString().Trim();
                txtOrderQty.Text = dtProc.Rows[0]["ORD_QTY"].ToString().Trim();
                txtPackingQty.Text = dtProc.Rows[0]["PACKING_QTY"].ToString().Trim();
                txtRemainingQty.Text = dtProc.Rows[0]["REM_QTY"].ToString().Trim();

                lblPartyCode.Text = dtProc.Rows[0]["PRTY_CODE"].ToString().Trim();
                lblPartyName.Text = dtProc.Rows[0]["PRTY_NAME"].ToString().Trim();
                lblBusinessType.Text = dtProc.Rows[0]["BUSINESS_TYPE"].ToString().Trim();
                lblProductType.Text = dtProc.Rows[0]["PRODUCT_TYPE"].ToString().Trim();
                lblOrderCategory.Text = dtProc.Rows[0]["ORDER_CAT"].ToString().Trim();
                lblOrderTypeS.Text = dtProc.Rows[0]["ORDER_TYPE"].ToString().Trim();
                txtPANo.Text = dtProc.Rows[0]["ORDER_NO"].ToString().Trim();

                txtLOTIDNumber.Text = dtProc.Rows[0]["LOT_NUMBER"].ToString().Trim();
                txtLotQty.Text = dtProc.Rows[0]["STOCK_QTY"].ToString().Trim();
                txtLotPackQty.Text = dtProc.Rows[0]["QTY_PACK"].ToString().Trim();
                txtLotRemQty.Text = dtProc.Rows[0]["STOCK_BAL_QTY"].ToString().Trim();
                txtWtPerCone.ReadOnly = true;
                txtWtPerCone.Text = dtProc.Rows[0]["WEIGHT_OF_UNIT"].ToString().Trim();
                txtFrProcessCode.Text = dtProc.Rows[0]["PROS_CODE"].ToString().Trim();

                GetWeightOfUnit();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Sorry Dear! First you have to clear previous cycle then you can make Packing.");
            }

        }
        catch
        {
            throw;
        }
    }


}