using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_Production_Controls_OrderPackFabNew : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING oTX_FABRIC_PROD_PACKING;

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
            tdUpdate.Visible = false;
            txtPackingID.Visible = true;
            cmbPackingID.Visible = false;
            ddlPackingCategory.Enabled = true;
            ddlUOMOfUnit.Enabled = true;

            BindShiftMst();
            ClearData();
            txtPackingDate.Text = DateTime.Now.Date.ToShortDateString();
            txtPackingID.Text = BindMaxPackingID();
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
            txtPackingID.Text = string.Empty;
            cmbPackingID.SelectedIndex = -1;
            txtPackingDate.Text = string.Empty;
            txtShift.SelectedIndex = -1;
            txtPANo.Text = string.Empty;
            txtLOTIDNumber.Text = string.Empty;
            lblPartyCode.Text = string.Empty;
            lblPartyName.Text = string.Empty;
            txtOrderNumber.Text = string.Empty;
            lblBusinessType.Text = string.Empty;
            lblProductType.Text = string.Empty;
            txtFrProcessCode.Text = string.Empty;
            lblOrderCategory.Text = string.Empty;
            lblOrderTypeS.Text = string.Empty;
            txtArticleCode.Text = string.Empty;
            txtArticleDesc.Text = string.Empty;
            txtShadeCode.Text = string.Empty;
            txtOrderQty.Text = string.Empty;
            txtPackingQty.Text = string.Empty;
            txtLotQty.Text = string.Empty;
            txtLotPackQty.Text = string.Empty;
            txtLotRemQty.Text = string.Empty;
            txtCheckedBy.Text = string.Empty;
            ddlPackingCategory.SelectedIndex = -1;
            txtPackGrade.Text = string.Empty;
            ddlUOMOfUnit.SelectedIndex = -1;
            txtWtPerCone.Text = string.Empty;
            txtNoOfCone.Text = string.Empty;
            txtPackQty.Text = string.Empty;
            txtPackLotNo.Text = string.Empty;
            txtRunSpeed.Text = string.Empty;
            txtLoomNo.Text = string.Empty;
            txtPackWidth.Text = string.Empty;
            txtBaleQty.Text = string.Empty;
            txtLoadDate.Text = string.Empty;
            txtUnloadDate.Text = string.Empty;
            txtStopTime.Text = string.Empty;
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
            string x = string.Empty;
            string strID = string.Empty;
            int y = 0;
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_PROD_PACKING.GetMaxPackingID();
            if (dt != null && dt.Rows.Count > 0)
            {
                x = dt.Rows[0]["MAX_ID"].ToString();
                int.TryParse(x, out y);
                y = y + 1;
                strID = y.ToString();
            }
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

            DataTable dtProc = SaitexBL.Interface.Method.TX_FABRIC_PROD_PACKING.GetLotToPackByPA(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, sORDER_NO.Trim(), oUserLoginDetail.VC_DEPARTMENTCODE);
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

                GetPACK_QTYInKG();
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

    private void GetPACK_QTYInKG()
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
                txtPackQty.Text = "0";
                CommonFuction.ShowMessage("Pack qty can not be more than bal to pack qty.");
            }
            else
            {
                txtPackQty.Text = Qty.ToString();
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
                GetPACK_QTYInKG();
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
            GetPACK_QTYInKG();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Text Changed Event (Weight Per Cone).\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
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
            string msg = string.Empty;
            string strID = string.Empty;
            int dblPckID = 0;
            if (CheckValidation(out msg))
            {
                GetPACK_QTYInKG();

                oTX_FABRIC_PROD_PACKING = new SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING();

                strID = BindMaxPackingID();
                int.TryParse(strID, out dblPckID);
                oTX_FABRIC_PROD_PACKING.PACKING_ID = dblPckID;
                oTX_FABRIC_PROD_PACKING.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_FABRIC_PROD_PACKING.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_FABRIC_PROD_PACKING.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_FABRIC_PROD_PACKING.PACKING_DATE = DateTime.Parse(txtPackingDate.Text.Trim());
                oTX_FABRIC_PROD_PACKING.PACKING_CAT = ddlPackingCategory.SelectedValue.ToString().Trim();
                oTX_FABRIC_PROD_PACKING.ORDER_NO = txtOrderNumber.Text.Trim();
                oTX_FABRIC_PROD_PACKING.PI_NO = txtPANo.Text.ToString().Trim();
                oTX_FABRIC_PROD_PACKING.LOT_NO = txtLOTIDNumber.Text.Trim();
                oTX_FABRIC_PROD_PACKING.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                oTX_FABRIC_PROD_PACKING.ARTICAL_CODE = txtArticleCode.Text.Trim();
                oTX_FABRIC_PROD_PACKING.SHADE_CODE = txtShadeCode.Text;
                oTX_FABRIC_PROD_PACKING.SHIFT = txtShift.Text.ToUpper().Trim();
                oTX_FABRIC_PROD_PACKING.LOOM_NO = txtLoomNo.Text;
                oTX_FABRIC_PROD_PACKING.FR_PROS_CODE = txtFrProcessCode.Text;
                oTX_FABRIC_PROD_PACKING.PACK_LOT_NO = txtPackLotNo.Text.Trim();
                oTX_FABRIC_PROD_PACKING.PACK_GRADE = txtPackGrade.Text;
                DateTime LOAD_DATE = DateTime.Now.Date;
                DateTime.TryParse(txtLoadDate.Text, out LOAD_DATE);
                oTX_FABRIC_PROD_PACKING.LOAD_DATE = LOAD_DATE;

                DateTime UNLOAD_DATE = DateTime.Now.Date;
                DateTime.TryParse(txtUnloadDate.Text, out UNLOAD_DATE);
                oTX_FABRIC_PROD_PACKING.UNLOAD_DATE = UNLOAD_DATE;

                double STOP_TIME = 0;
                double.TryParse(txtStopTime.Text, out STOP_TIME);
                oTX_FABRIC_PROD_PACKING.STOP_TIME = STOP_TIME;

                double RUN_SPEED = 0;
                double.TryParse(txtRunSpeed.Text, out RUN_SPEED);
                oTX_FABRIC_PROD_PACKING.RUN_SPEED = RUN_SPEED;

                double NO_OF_UNIT = 0;
                double.TryParse(txtNoOfCone.Text, out NO_OF_UNIT);
                oTX_FABRIC_PROD_PACKING.NO_OF_UNIT = NO_OF_UNIT;

                oTX_FABRIC_PROD_PACKING.UOM_OF_UNIT = ddlUOMOfUnit.SelectedValue.ToString().Trim();

                double WEIGHT_OF_UNIT = 0;
                double.TryParse(txtWtPerCone.Text, out WEIGHT_OF_UNIT);
                oTX_FABRIC_PROD_PACKING.WEIGHT_OF_UNIT = WEIGHT_OF_UNIT;

                double BALE_QTY = 0;
                double.TryParse(txtBaleQty.Text, out BALE_QTY);
                oTX_FABRIC_PROD_PACKING.BALE_QTY = BALE_QTY;

                double PACK_WIDTH = 0;
                double.TryParse(txtPackWidth.Text, out PACK_WIDTH);
                oTX_FABRIC_PROD_PACKING.PACK_WIDTH = PACK_WIDTH;

                double PACK_QTY = 0;
                double.TryParse(txtPackWidth.Text, out PACK_QTY);
                oTX_FABRIC_PROD_PACKING.PACK_QTY = PACK_QTY;

                oTX_FABRIC_PROD_PACKING.CHECKED_BY = txtCheckedBy.Text;
                oTX_FABRIC_PROD_PACKING.REMARKS = txtRemarks.Text.Trim();

                oTX_FABRIC_PROD_PACKING.PACK_CODE = string.Empty;
                oTX_FABRIC_PROD_PACKING.TUSER = oUserLoginDetail.UserCode;

                int iRecordFound = 0;
                bool bResult = SaitexBL.Interface.Method.TX_FABRIC_PROD_PACKING.Insert(oTX_FABRIC_PROD_PACKING, out iRecordFound);
                if (bResult)
                {
                    ClearData();
                    CommonFuction.ShowMessage("This record is saved successfully");
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
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch
        {
            throw;
        }
    }

    private bool CheckValidation(out string msg)
    {
        msg = string.Empty;
        try
        {
            int iCount = 0;
            int iCountAll = 0;

            iCountAll += 1;
            if (txtShift.SelectedValue != "select")
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please select Shift..\r\n";
            }

            double packqty = 0;
            double.TryParse(txtPackQty.Text, out packqty);
            iCountAll += 1;
            if (packqty > 0)
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please enter PACK_QTY..\r\n";
            }

            double unitWeight = 0;
            double.TryParse(txtWtPerCone.Text, out unitWeight);
            iCountAll += 1;
            if (unitWeight > 0)
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please enter Weight Per Cone..\r\n";
            }

            double noofunit = 0;
            double.TryParse(txtNoOfCone.Text, out noofunit);
            iCountAll += 1;
            if (noofunit > 0)
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please enter Number Of Cone..\r\n";
            }

            iCountAll += 1;
            if (txtOrderNumber.Text != "")
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please provide Order Number..\r\n";
            }

            iCountAll += 1;
            if (txtPackingDate.Text != "")
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please enter Packing Date..\r\n";
            }

            iCountAll += 1;
            if (txtPANo.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please provide PI Number..\r\n";
            }

            iCountAll += 1;
            if (txtLoomNo.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please provide Loom Number..\r\n";
            }

            DateTime LOAD_DATE = DateTime.Now.Date;
            iCountAll += 1;
            if (DateTime.TryParse(txtLoadDate.Text, out LOAD_DATE))
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please provide Load date time..\r\n";
            }

            DateTime UNLOAD_DATE = DateTime.Now.Date;
            iCountAll += 1;
            if (DateTime.TryParse(txtUnloadDate.Text, out UNLOAD_DATE))
            {
                iCount += 1;
            }
            else
            {
                msg += @"Dear! Please provide Unload date time..\r\n";
            }

            if (iCount == iCountAll)
                return true;
            else
                return false;
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
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT PACKING_ID, PACKING_DATE, PI_NO, LOT_NO, ARTICAL_CODE, SHADE_CODE, FR_PROS_CODE FROM TX_FABRIC_PROD_PACKING WHERE BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ORDER BY PACKING_ID) WHERE PI_NO LIKE :SearchQuery OR LOT_NO LIKE :SearchQuery OR PACKING_ID LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (StartOffSet != 0)
            {
                whereClause += " AND PACKING_ID NOT IN(SELECT PACKING_ID FROM (SELECT * FROM (SELECT PACKING_ID,PACKING_DATE,PI_NO,LOT_NO,ARTICAL_CODE,SHADE_CODE,FR_PROS_CODE FROM TX_FABRIC_PROD_PACKING WHERE BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND DEPT_CODE ='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' ORDER BY PACKING_ID) WHERE PI_NO LIKE :SearchQuery OR LOT_NO LIKE :SearchQuery OR PACKING_ID LIKE :SearchQuery) WHERE ROWNUM <= '" + StartOffSet + "')";
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
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT PACKING_ID, PACKING_DATE, PI_NO, LOT_NO, ARTICAL_CODE, SHADE_CODE, FR_PROS_CODE FROM TX_FABRIC_PROD_PACKING WHERE BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ORDER BY PACKING_ID) WHERE PI_NO LIKE :SearchQuery OR LOT_NO LIKE :SearchQuery OR PACKING_ID LIKE :SearchQuery)";
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

            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_PROD_PACKING.GetPackingDTLByPackingID(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PCK_ID);
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

                txtLOTIDNumber.Text = dt.Rows[0]["LOT_NO"].ToString().Trim();
                txtLotQty.Text = dt.Rows[0]["STOCK_QTY"].ToString().Trim();
                txtLotPackQty.Text = dt.Rows[0]["LOT_QTY_PACK"].ToString().Trim();
                txtLotRemQty.Text = dt.Rows[0]["LOT_STOCK_BAL_QTY"].ToString().Trim();
                txtWtPerCone.Text = dt.Rows[0]["WEIGHT_OF_UNIT"].ToString().Trim();
                txtNoOfCone.Text = dt.Rows[0]["NO_OF_UNIT"].ToString().Trim();
                ddlUOMOfUnit.SelectedIndex = ddlUOMOfUnit.Items.IndexOf(ddlUOMOfUnit.Items.FindByValue("UOM_OF_UNIT"));
                txtFrProcessCode.Text = dt.Rows[0]["FR_PROS_CODE"].ToString().Trim();
                txtPackingDate.Text = DateTime.Parse(dt.Rows[0]["PACKING_DATE"].ToString().Trim()).ToShortDateString();

                txtShift.SelectedIndex = txtShift.Items.IndexOf(txtShift.Items.FindByValue("SHIFT"));
                ddlPackingCategory.SelectedIndex = ddlPackingCategory.Items.IndexOf(ddlPackingCategory.Items.FindByValue("PACKING_CAT"));
                txtPackGrade.Text = dt.Rows[0]["PACK_GRADE"].ToString().Trim();
                txtPackLotNo.Text = dt.Rows[0]["PACK_LOT_NO"].ToString().Trim();
                txtCheckedBy.Text = dt.Rows[0]["CHECKED_BY"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString().Trim();

                txtLoomNo.Text = dt.Rows[0]["LOOM_NO"].ToString().Trim();
                txtLoadDate.Text = dt.Rows[0]["LOAD_DATE"].ToString().Trim();
                txtUnloadDate.Text = dt.Rows[0]["UNLOAD_DATE"].ToString().Trim();
                txtStopTime.Text = dt.Rows[0]["STOP_TIME"].ToString().Trim();
                txtRunSpeed.Text = dt.Rows[0]["RUN_SPEED"].ToString().Trim();
                txtBaleQty.Text = dt.Rows[0]["BALE_QTY"].ToString().Trim();
                txtPackWidth.Text = dt.Rows[0]["PACK_WIDTH"].ToString().Trim();
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
            string msg = string.Empty;
            if (CheckValidation(out msg))
            {
                oTX_FABRIC_PROD_PACKING = new SaitexDM.Common.DataModel.TX_FABRIC_PROD_PACKING();

                oTX_FABRIC_PROD_PACKING.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_FABRIC_PROD_PACKING.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_FABRIC_PROD_PACKING.PACKING_DATE = DateTime.Parse(txtPackingDate.Text.Trim());
                oTX_FABRIC_PROD_PACKING.NO_OF_UNIT = double.Parse(txtNoOfCone.Text.Trim());
                oTX_FABRIC_PROD_PACKING.WEIGHT_OF_UNIT = double.Parse(txtWtPerCone.Text.Trim());
                oTX_FABRIC_PROD_PACKING.PACKING_ID = int.Parse(cmbPackingID.SelectedValue.ToString().Trim());
                oTX_FABRIC_PROD_PACKING.PACK_QTY = double.Parse(txtPackQty.Text.Trim());
                oTX_FABRIC_PROD_PACKING.SHIFT = txtShift.Text.Trim();
                oTX_FABRIC_PROD_PACKING.REMARKS = txtRemarks.Text.Trim();
                oTX_FABRIC_PROD_PACKING.REMARKS = txtRemarks.Text.Trim();
                oTX_FABRIC_PROD_PACKING.CHECKED_BY = txtCheckedBy.Text;
                oTX_FABRIC_PROD_PACKING.UOM_OF_UNIT = ddlUOMOfUnit.SelectedValue.Trim();
                oTX_FABRIC_PROD_PACKING.PACK_GRADE = txtPackGrade.Text;
                oTX_FABRIC_PROD_PACKING.PACKING_CAT = ddlPackingCategory.SelectedValue.ToString().Trim();
                oTX_FABRIC_PROD_PACKING.PACK_LOT_NO = txtPackLotNo.Text;

                oTX_FABRIC_PROD_PACKING.LOOM_NO = txtLoomNo.Text;

                DateTime LOAD_DATE = DateTime.Now.Date;
                DateTime.TryParse(txtLoadDate.Text, out LOAD_DATE);
                oTX_FABRIC_PROD_PACKING.LOAD_DATE = LOAD_DATE;

                DateTime UNLOAD_DATE = DateTime.Now.Date;
                DateTime.TryParse(txtUnloadDate.Text, out UNLOAD_DATE);
                oTX_FABRIC_PROD_PACKING.UNLOAD_DATE = UNLOAD_DATE;

                double STOP_TIME = 0;
                double.TryParse(txtStopTime.Text, out STOP_TIME);
                oTX_FABRIC_PROD_PACKING.STOP_TIME = STOP_TIME;

                double RUN_SPEED = 0;
                double.TryParse(txtRunSpeed.Text, out RUN_SPEED);
                oTX_FABRIC_PROD_PACKING.RUN_SPEED = RUN_SPEED;

                double BALE_QTY = 0;
                double.TryParse(txtBaleQty.Text, out BALE_QTY);
                oTX_FABRIC_PROD_PACKING.BALE_QTY = BALE_QTY;

                double PACK_WIDTH = 0;
                double.TryParse(txtPackWidth.Text, out PACK_WIDTH);
                oTX_FABRIC_PROD_PACKING.PACK_WIDTH = PACK_WIDTH;

                int iRecordFound = 0;
                bool bResult = SaitexBL.Interface.Method.TX_FABRIC_PROD_PACKING.Update(oTX_FABRIC_PROD_PACKING, out iRecordFound);
                if (bResult)
                {
                    txtPackingDate.Enabled = true;
                    ClearData();
                    CommonFuction.ShowMessage("This record is updated successfully");
                }
                else if (iRecordFound > 0)
                {
                    Common.CommonFuction.ShowMessage("This Record is already updated.. Please enter another.");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Details updation failed..");
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

    }

    protected void lbtnGetBale_Click(object sender, EventArgs e)
    {

    }
}
