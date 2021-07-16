using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class Module_Production_Controls_OrderPackingSWNew : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    List<SaitexDM.Common.DataModel.YRN_PROD_PACKING> oList;
    SaitexDM.Common.DataModel.YRN_PROD_PACKING_MST oYRN_PROD_PACKING_MST;

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
                    ddlArticle.Enabled = true;
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
            int y = 0;
            int x = SaitexBL.Interface.Method.YRN_PROD_PACKING.GetMaxPackingMstID();

            y = x + 1;
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

                oYRN_PROD_PACKING_MST = new SaitexDM.Common.DataModel.YRN_PROD_PACKING_MST();

                oYRN_PROD_PACKING_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRN_PROD_PACKING_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oYRN_PROD_PACKING_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oYRN_PROD_PACKING_MST.PACKING_DATE = DateTime.Parse(txtPackingDate.Text.Trim());
                oYRN_PROD_PACKING_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                strID = BindMaxPackingID();
                int.TryParse(strID, out dblPckID);
                oYRN_PROD_PACKING_MST.PACKING_MST_ID = dblPckID;
                oYRN_PROD_PACKING_MST.EXCISE_CARTON_NO = int.Parse(txtExciseCartonNo.Text.Trim());
                oYRN_PROD_PACKING_MST.TARE_WEIGHT = 0;
                oYRN_PROD_PACKING_MST.GROSS_WEIGHT = double.Parse(txtProduction.Text.Trim());
                oYRN_PROD_PACKING_MST.SHIFT = txtShift.Text.ToUpper().Trim();
                oYRN_PROD_PACKING_MST.REMARKS = txtRemarks.Text.Trim();
                oYRN_PROD_PACKING_MST.TUSER = oUserLoginDetail.UserCode;

                if (ViewState["YRN_PROD_PACKING_LIST"] == null)
                    oList = new List<SaitexDM.Common.DataModel.YRN_PROD_PACKING>();
                else
                    oList = (List<SaitexDM.Common.DataModel.YRN_PROD_PACKING>)ViewState["YRN_PROD_PACKING_LIST"];

                int iRecordFound = 0;
                bool bResult = SaitexBL.Interface.Method.YRN_PROD_PACKING.InsertMst(oYRN_PROD_PACKING_MST, oList, out iRecordFound);
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

            string Pack_Code = string.Empty;

            if (txtPackingDate.Text != "")
            {
                IsValidation = true;
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please enter Packing Date..");
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
                oYRN_PROD_PACKING_MST = new SaitexDM.Common.DataModel.YRN_PROD_PACKING_MST();

                oYRN_PROD_PACKING_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRN_PROD_PACKING_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oYRN_PROD_PACKING_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oYRN_PROD_PACKING_MST.PACKING_DATE = DateTime.Parse(txtPackingDate.Text.Trim());
                oYRN_PROD_PACKING_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                oYRN_PROD_PACKING_MST.PACKING_MST_ID = int.Parse(txtPackingID.Text);
                oYRN_PROD_PACKING_MST.EXCISE_CARTON_NO = 0;// txtPackLotNo.Text.Trim();
                oYRN_PROD_PACKING_MST.TARE_WEIGHT = 0;
                oYRN_PROD_PACKING_MST.GROSS_WEIGHT = double.Parse(txtProduction.Text.Trim());
                oYRN_PROD_PACKING_MST.SHIFT = txtShift.Text.ToUpper().Trim();
                oYRN_PROD_PACKING_MST.REMARKS = txtRemarks.Text.Trim();
                oYRN_PROD_PACKING_MST.TUSER = oUserLoginDetail.UserCode;

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
            cmbPackingID.DataTextField = "PACKING_MST_ID";
            cmbPackingID.DataValueField = "PACKING_MST_ID";
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
            string CommandText = "SELECT * FROM (SELECT PACKING_MST_ID, PACKING_DATE, EXCISE_CARTON_NO  FROM YRN_PROD_PACKING_MST WHERE (BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND DEPT_CODE =    '" + oUserLoginDetail.VC_DEPARTMENTCODE + "'  AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "')   AND (EXCISE_CARTON_NO LIKE :SearchQuery  OR PACKING_MST_ID LIKE :SearchQuery)  ORDER BY PACKING_MST_ID DESC) WHERE ROWNUM <= 15";
            string whereClause = string.Empty;
            if (StartOffSet != 0)
            {
                whereClause += " and PACKING_MST_ID not in (SELECT PACKING_MST_ID FROM (SELECT PACKING_MST_ID, PACKING_DATE, EXCISE_CARTON_NO  FROM YRN_PROD_PACKING_MST WHERE (BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND DEPT_CODE =    '" + oUserLoginDetail.VC_DEPARTMENTCODE + "'  AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "')   AND (EXCISE_CARTON_NO LIKE :SearchQuery  OR PACKING_MST_ID LIKE :SearchQuery)  ORDER BY PACKING_MST_ID DESC) WHERE ROWNUM <= '" + StartOffSet + "')";
            }

            string SortExpression = " order by PACKING_MST_ID desc";
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
            string CommandText = "SELECT * FROM (SELECT PACKING_MST_ID, PACKING_DATE, EXCISE_CARTON_NO  FROM YRN_PROD_PACKING_MST WHERE (BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND DEPT_CODE =    '" + oUserLoginDetail.VC_DEPARTMENTCODE + "'  AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "')   AND (EXCISE_CARTON_NO LIKE :SearchQuery  OR PACKING_MST_ID LIKE :SearchQuery)  ORDER BY PACKING_MST_ID DESC) ";
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

    private void FillDataForEdit(string PCKMst_ID)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_PACKING.GetPackingDTLByPackingID(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PCKMst_ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlLotIdNo.Enabled = false;
                txtArticleCode.Text = dt.Rows[0]["ARTICAL_CODE"].ToString().Trim();
                txtExciseCartonNo.Text = dt.Rows[0]["EXCISE_CARTON_NO"].ToString().Trim();
                txtArticleCartonNo.Text = dt.Rows[0]["ARTICLE_CARTON_NO"].ToString().Trim();
                txtArticleDesc.Text = dt.Rows[0]["ORD_ARTICAL_DESC"].ToString().Trim();
                txtPackingDate.Text = DateTime.Parse(dt.Rows[0]["PACKING_DATE"].ToString().Trim()).ToShortDateString();
                txtShift.SelectedIndex = txtShift.Items.IndexOf(txtShift.Items.FindByValue("SHIFT"));
                txtNetWeight.Text = dt.Rows[0]["NET_WEIGHT"].ToString().Trim();
                txtGrossWeight.Text = dt.Rows[0]["GROSS_WEIGHT"].ToString().Trim();
                txtTareWeight.Text = dt.Rows[0]["TARE_WEIGHT"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString().Trim();
                ddlArticle.Enabled = false;

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
            if (txtArticleCode.Text != string.Empty)
            {
                string CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( ORDER_NO|| '@'|| LOT_NUMBER|| '@'|| DEPT_CODE|| '@'|| PROS_CODE) LOT_DATA, COMP_CODE, BRANCH_CODE, DEPT_CODE, ORDER_NO, LOT_NUMBER, PRTY_CODE, PROS_CODE, PRTY_NAME AS PARTY FROM V_YRN_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'  and WS.ORD_ARTICAL_CODE='" + txtArticleCode.Text + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
                string whereClause = string.Empty;
                if (StartOffSet != 0)
                {
                    whereClause += " AND LOT_DATA NOT IN(SELECT LOT_DATA FROM (SELECT * FROM (SELECT ( ORDER_NO || '@' || LOT_NUMBER || '@' || DEPT_CODE || '@' || PROS_CODE) LOT_DATA,COMP_CODE,BRANCH_CODE,DEPT_CODE,ORDER_NO,LOT_NUMBER,PRTY_CODE,PROS_CODE,PRTY_NAME AS PARTY FROM V_YRN_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE ='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'  and WS.ORD_ARTICAL_CODE='" + txtArticleCode.Text + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery) WHERE ROWNUM <= '" + StartOffSet + "')";
                }

                string SortExpression = " order by LOT_NUMBER";
                string SearchQuery = Text + "%";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

                return data;
            }
            else return null;
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
            if (txtArticleCode.Text != string.Empty)
            {
                string CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( ORDER_NO|| '@'|| LOT_NUMBER|| '@'|| DEPT_CODE|| '@'|| PROS_CODE) LOT_DATA, COMP_CODE, BRANCH_CODE, DEPT_CODE, ORDER_NO, LOT_NUMBER, PRTY_CODE, PROS_CODE, PRTY_NAME AS PARTY FROM V_YRN_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'  and WS.ORD_ARTICAL_CODE='" + txtArticleCode.Text + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery)  ";
                string whereClause = string.Empty;

                string SortExpression = " ";
                string SearchQuery = Text + "%";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

                return data.Rows.Count;
            }
            else return 0;
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

    protected void ddlArticle_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetArticleCode(e.Text.ToUpper().Trim(), e.ItemsOffset);
            ddlArticle.DataTextField = "ARTICLE_CODE";
            ddlArticle.DataValueField = "ARTICLE_DESC";
            ddlArticle.DataSource = data;
            ddlArticle.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetArticleCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetArticleCode(string Text, int StartOffSet)
    {
        try
        {
            string CommandText = "SELECT article_code,article_desc FROM (SELECT DISTINCT WS.ORD_ARTICAL_CODE AS article_code, y.yarn_desc AS article_desc FROM V_YRN_WIP_STOCK WS, yrn_mst y WHERE (WS.ORD_ARTICAL_CODE = Y.YARN_CODE AND WS.PRODUCT_TYPE = 'SEWING THREAD' AND WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') AND (WS.ORD_ARTICAL_CODE LIKE :SearchQuery OR Y.YARN_DESC LIKE :SearchQuery)) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (StartOffSet != 0)
            {
                whereClause += " and article_code not in ( SELECT article_code FROM (SELECT DISTINCT WS.ORD_ARTICAL_CODE AS article_code, y.yarn_desc AS article_desc FROM V_YRN_WIP_STOCK WS, yrn_mst y WHERE (WS.ORD_ARTICAL_CODE = Y.YARN_CODE AND WS.PRODUCT_TYPE = 'SEWING THREAD' AND WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') AND (WS.ORD_ARTICAL_CODE LIKE :SearchQuery OR Y.YARN_DESC LIKE :SearchQuery)) WHERE ROWNUM <= '" + StartOffSet + "' )";
            }

            string SortExpression = " order by article_code";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int GetArticleCount(string Text)
    {
        try
        {
            string CommandText = "SELECT article_code,article_desc FROM (SELECT DISTINCT WS.ORD_ARTICAL_CODE AS article_code, y.yarn_desc AS article_desc FROM V_YRN_WIP_STOCK WS, yrn_mst y WHERE (WS.ORD_ARTICAL_CODE = Y.YARN_CODE AND WS.PRODUCT_TYPE = 'SEWING THREAD' AND WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') AND (WS.ORD_ARTICAL_CODE LIKE :SearchQuery OR Y.YARN_DESC LIKE :SearchQuery)) ";
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

    protected void ddlArticle_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ViewState["YRN_PROD_PACKING_LIST"] != null)
            {
                oList = (List<SaitexDM.Common.DataModel.YRN_PROD_PACKING>)ViewState["YRN_PROD_PACKING_LIST"];
                oList.Clear();
                ViewState["YRN_PROD_PACKING_LIST"] = oList;
            }

            if (ddlArticle.SelectedValue.Trim().ToString() != "0")
            {
                ddlArticle.Enabled = false;
                string ARTICLE_CODE = ddlArticle.SelectedText.Trim();
                string ARTICLE_DESC = ddlArticle.SelectedValue.Trim();
                txtArticleCode.Text = ARTICLE_CODE;
                txtArticleDesc.Text = ARTICLE_DESC;
                SetArticleCartonNo(ARTICLE_CODE);
                SetExciseCartonNo(ARTICLE_CODE);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Selecting Record"));
        }
    }

    private void SetExciseCartonNo(string ARTICLE_CODE)
    {
        int iEXCISE_CARTON_NO = SaitexDL.Interface.Method.YRN_PROD_PACKING.GetNewExciseCartonNo(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
        txtExciseCartonNo.Text = iEXCISE_CARTON_NO.ToString();

    }

    public void SetArticleCartonNo(string ARTICLE_CODE)
    {
        int iARTICLE_CARTON_NO = SaitexDL.Interface.Method.YRN_PROD_PACKING.GetNewArtCartonNo(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ARTICLE_CODE);
        txtArticleCartonNo.Text = iARTICLE_CARTON_NO.ToString();

    }

    protected void grdPacking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteRow")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                //string val1 = grdPacking.SelectedDataKey["LOT_ID_NO"].ToString();
                //string EXCISE_CARTON_NO = grdPacking.DataKeys[gvr.RowIndex]["EXCISE_CARTON_NO"].ToString();
                //string ARTICLE_CARTON_NO = grdPacking.DataKeys[gvr.RowIndex]["ARTICLE_CARTON_NO"].ToString();
                //string ARTICAL_CODE = grdPacking.DataKeys[gvr.RowIndex]["ARTICAL_CODE"].ToString();
                string LOT_ID_NO = grdPacking.DataKeys[gvr.RowIndex]["LOT_ID_NO"].ToString();
                string SHADE_CODE = grdPacking.DataKeys[gvr.RowIndex]["SHADE_CODE"].ToString();

                oList = (List<SaitexDM.Common.DataModel.YRN_PROD_PACKING>)ViewState["YRN_PROD_PACKING_LIST"];
                oList.RemoveAll(x => x.LOT_ID_NO == LOT_ID_NO && x.SHADE_CODE == SHADE_CODE);
                ViewState["YRN_PROD_PACKING_LIST"] = oList;
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string strID = string.Empty;
            int dblPckID = 0;
            string msg = string.Empty;
            if (!ValidateTRNRow(out msg))
            {
                if (CheckValidation())
                {
                    GetProductionInKG();

                    SaitexDM.Common.DataModel.YRN_PROD_PACKING oYRN_PROD_PACKING = new SaitexDM.Common.DataModel.YRN_PROD_PACKING();

                    oYRN_PROD_PACKING.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oYRN_PROD_PACKING.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oYRN_PROD_PACKING.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oYRN_PROD_PACKING.EXCISE_CARTON_NO = int.Parse(txtExciseCartonNo.Text);
                    oYRN_PROD_PACKING.ARTICLE_CARTON_NO = int.Parse(txtArticleCartonNo.Text);
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
                    oYRN_PROD_PACKING.PACK_LOT_NO = string.Empty;// txtPackLotNo.Text.Trim();
                    oYRN_PROD_PACKING.PRODUCTION = double.Parse(txtProduction.Text.Trim());
                    oYRN_PROD_PACKING.SHIFT = txtShift.Text.ToUpper().Trim();
                    oYRN_PROD_PACKING.EFFICIENCY = txtEfficiency.Text.Trim();
                    oYRN_PROD_PACKING.REMARKS = txtRemarks.Text.Trim();
                    oYRN_PROD_PACKING.CHECKED_BY = txtCheckedBy.Text;
                    oYRN_PROD_PACKING.SHADE_CODE = txtShadeCode.Text;
                    oYRN_PROD_PACKING.PACK_GRADE = string.Empty;//txtPackGrade.Text;
                    oYRN_PROD_PACKING.FR_PROS_CODE = txtFrProcessCode.Text;

                    if (ViewState["YRN_PROD_PACKING_LIST"] == null)
                        oList = new List<SaitexDM.Common.DataModel.YRN_PROD_PACKING>();
                    else
                        oList = (List<SaitexDM.Common.DataModel.YRN_PROD_PACKING>)ViewState["YRN_PROD_PACKING_LIST"];

                    oList.Add(oYRN_PROD_PACKING);
                    ViewState["YRN_PROD_PACKING_LIST"] = oList;
                    clearTrnRow();
                    BindGrid();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please provide some required field denoted with(*) mark..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);

                clearTrnRow();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void clearTrnRow()
    {
        txtOrderNumber.Text = string.Empty;
        txtLOTIDNumber.Text = string.Empty;
        txtNoOfCone.Text = string.Empty;
        txtWtPerCone.Text = string.Empty;
        ddlUOMOfUnit.SelectedIndex = 0;
        txtProduction.Text = string.Empty;
        txtEfficiency.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtShadeCode.Text = string.Empty;
        txtPANo.Text = string.Empty;
        txtCheckedBy.Text = string.Empty;
        txtFrProcessCode.Text = string.Empty;
        txtLotPackQty.Text = string.Empty;
        txtLotQty.Text = string.Empty;
        txtLotRemQty.Text = string.Empty;
        txtOrderQty.Text = string.Empty;

        txtPackingQty.Text = string.Empty;
        txtRemainingQty.Text = string.Empty;
        txtWtPerCone.Text = string.Empty;
        lblBusinessType.Text = string.Empty;
        lblOrderCategory.Text = string.Empty;
        lblOrderTypeS.Text = string.Empty;
        lblPartyCode.Text = string.Empty;
        lblPartyName.Text = string.Empty;
        lblProductType.Text = string.Empty;
        ddlLotIdNo.SelectedIndex = -1;
    }

    protected bool ValidateTRNRow(out string msg)
    {
        try
        {
            bool bResult = false;
            msg = string.Empty;
            if (ViewState["YRN_PROD_PACKING_LIST"] != null)
            {
                oList = (List<SaitexDM.Common.DataModel.YRN_PROD_PACKING>)ViewState["YRN_PROD_PACKING_LIST"];


                var isExist = (from data in oList
                               where data.ARTICAL_CODE == txtArticleCode.Text && data.SHADE_CODE == txtShadeCode.Text && data.LOT_ID_NO == txtLOTIDNumber.Text
                               select data).ToList();
                if (isExist.Count > 0)
                {
                    bResult = true;
                    msg = "This lot no and Shade already added. Please chhose another one.";
                }
            }
            return bResult;
        }
        catch
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearTrnRow();
    }

    protected void BindGrid()
    {
        if (ViewState["YRN_PROD_PACKING_LIST"] != null)
        {
            oList = (List<SaitexDM.Common.DataModel.YRN_PROD_PACKING>)ViewState["YRN_PROD_PACKING_LIST"];

            grdPacking.DataSource = oList;
            grdPacking.DataBind();

            double Netweight = oList.Sum(x => x.PRODUCTION);
            txtNetWeight.Text = Netweight.ToString();
            SetGrossWeight();
        }
    }
    protected void txtTareWeight_TextChanged(object sender, EventArgs e)
    {
        SetGrossWeight();
    }
    protected void SetGrossWeight()
    {
        double netweight = 0;
        double tareweight = 0;
        double grossweight = 0;
        double.TryParse(txtTareWeight.Text, out tareweight);
        double.TryParse(txtNetWeight.Text, out netweight);
        grossweight = netweight + tareweight;

        txtGrossWeight.Text = grossweight.ToString();
        txtTareWeight.Text = tareweight.ToString();
        txtNetWeight.Text = netweight.ToString();
    }
}
