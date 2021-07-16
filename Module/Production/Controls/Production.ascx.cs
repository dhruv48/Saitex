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
using errorLog;
using Obout.Interface;

public partial class Module_Production_Controls_Production : System.Web.UI.UserControl
{
    private static DataTable dtProdTRN;
    private static DataTable dtProdColorChem;
    SaitexDM.Common.DataModel.YRN_PROD_MST oYRN_PROD_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string TRN_TYPE = "PRD01";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in page loading.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            BindShift();
            BindUOM(ddlLoadUOM);
            BindUOM(ddlUnloadUOM);
            ClearPage();
        }
        catch
        {
            throw;
        }
    }

    private void BindShift()
    {
        try
        {
            ddlShift.Items.Clear();
            DataTable dtCurrencyCode = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftName();

            ddlShift.DataSource = dtCurrencyCode;
            ddlShift.DataTextField = "SFT_NAME";
            ddlShift.DataValueField = "SFT_ID";
            ddlShift.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindUOM(OboutDropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dtUOM = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("UOM", oUserLoginDetail.COMP_CODE);

            ddl.DataSource = dtUOM;
            ddl.DataTextField = "MST_CODE";
            ddl.DataValueField = "MST_CODE";
            ddl.DataBind();
            ddl.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ClearPage()
    {
        try
        {
            ClearMainData();
            ActivateSaveMode();

            RefreshProdTRN();
            RefreshProdColorChem();

            dtProdTRN = null;
            dtProdColorChem = null;

            BindLotDetailGrid();
            BindColorChemGrid();


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
            ddlProsCode.SelectedIndex = -1;
            txtProsDesc.Text = string.Empty;
            lblProsCode.Text = string.Empty;

            ddlMachineCode.SelectedIndex = -1;
            txtMachineDesc.Text = string.Empty;

            txtProsIdNo.Text = string.Empty;
            txtEntryDate.Text = System.DateTime.Now.Date.ToShortDateString();

            ddlShift.SelectedIndex = -1;

            txtLoadingDate.Text = string.Empty;
            txtUnLoadingDate.Text = string.Empty;

            txtMachineStopage.Text = string.Empty;
            txtProcessTime.Text = string.Empty;
            txtOperator.Text = string.Empty;
            txtSupervisor.Text = string.Empty;

            txtRemarks.Text = string.Empty;

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

            txtProsIdNo.Text = string.Empty;
            txtProsIdNo.Visible = true;

            ddlProsCode.Enabled = true;

            ddlProsIdNo.Visible = false;
            ddlProsIdNo.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    private void RefreshProdTRN()
    {
        try
        {
            ddlLotNo.SelectedIndex = -1;
            txtOrderNo.Text = string.Empty;
            txtOrderDescription.Text = string.Empty;
            txtLoadQty.Text = string.Empty;
            txtUnloadQty.Text = string.Empty;
            txtToLocation.Text = string.Empty;
            txtToBatchNo.Text = string.Empty;
            txtLoadNoOfUnit.Text = "0";
            ddlLoadUOM.SelectedIndex = -1;
            txtLoadWeightOfUnit.Text = "0";
            txtUnloadNoOfUnit.Text = "0";
            ddlUnloadUOM.SelectedIndex = -1;
            txtUnloadWeightOfUnit.Text = "0";

            //txtLoadingDateTime.Text = string.Empty;
            //txtUnLoadingDateTime.Text = string.Empty;

            ViewState["UNIQUE_TRN"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void RefreshProdColorChem()
    {
        try
        {
            ddlItem.SelectedIndex = -1;
            txtItemDesc.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            txtItemRate.Text = string.Empty;
            txtBasis.Text = string.Empty;
            txtTubeQty.Text = string.Empty;
            txtExpr.Text = string.Empty;
            txtUnitQty.Text = string.Empty;
            txtDensity.Text = string.Empty;

            ViewState["UNIQUE_CHEM"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void BindColorChemGrid()
    {
        try
        {
            grdItemDetail.DataSource = dtProdColorChem;
            grdItemDetail.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlMachineCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMachineData(e.Text.ToUpper());

            ddlMachineCode.Items.Clear();

            ddlMachineCode.DataSource = data;
            ddlMachineCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in loading Machine data.\r\nSee error log for detail."));
        }
    }

    private DataTable GetMachineData(string Text)
    {
        try
        {
            string CommandText = "select MACHINE_CODE,MACHINE_GROUP,MACHINE_MAKE , ('('||MACHINE_CODE ||') '||MACHINE_GROUP ||', '||MACHINE_MAKE ||'@'||TO_CHAR(LAST_UNLOAD, 'YYYY/MM/DD HH:MM:SS AM'))MACHINE_DATA from MC_MACHINE_MASTER ";
            string WhereClause = "  where MACHINE_CODE like :SearchQuery or MACHINE_GROUP like :SearchQuery or MACHINE_MAKE like :SearchQuery";
            string SortExpression = " order by MACHINE_CODE asc";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlMachineCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ddlMachineCode.SelectedIndex != -1)
            {
                string[] arrString = ddlMachineCode.SelectedValue.Split('@');

                txtMachineDesc.Text = arrString[0].ToString();
                if (!arrString[1].ToString().Equals(string.Empty))
                {
                    txtLoadingDate.Text = DateTime.Parse(arrString[1].ToString()).ToString();
                    txtUnLoadingDate.Text = DateTime.Parse(arrString[1].ToString()).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting machine.\r\nSee error log for detail."));
        }
    }

    protected void ddlProsCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetProsData(e.Text.ToUpper());

            ddlProsCode.Items.Clear();

            ddlProsCode.DataSource = data;
            ddlProsCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in loading Process Code.\r\nSee error log for detail."));
        }
    }

    private DataTable GetProsData(string Text)
    {
        try
        {
            string CommandText = "select * from ( select * from TX_MAC_PROC_MST WHERE MAC_CODE='" + ddlMachineCode.SelectedText.Trim() + "' AND DEPARTMENT='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' ) ASD ";
            string WhereClause = "  where PROS_CODE like :SearchQuery or PROS_DESC like :SearchQuery ";
            string SortExpression = " order by PROS_CODE asc";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlProsCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ddlProsCode.SelectedIndex != -1 && ddlProsCode.SelectedText.Trim() != string.Empty)
            {
                txtProsDesc.Text = ddlProsCode.SelectedValue.Trim();
                //lblProsCode.Text = ddlItem.SelectedText.Trim();
                BindProsIdNo();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting Process code.\r\nSee error log for detail."));
        }
    }

    private void BindProsIdNo()
    {
        try
        {
            if (ddlProsCode.SelectedIndex != -1)
            {
                oYRN_PROD_MST = new SaitexDM.Common.DataModel.YRN_PROD_MST();
                oYRN_PROD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oYRN_PROD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRN_PROD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oYRN_PROD_MST.TRN_TYPE = TRN_TYPE;
                oYRN_PROD_MST.PROS_CODE = ddlProsCode.SelectedText.Trim();

                string ProsIdNo = SaitexBL.Interface.Method.YRN_PROD_MST.GetMaxProsIdNo(oYRN_PROD_MST);
                txtProsIdNo.Text = (int.Parse(ProsIdNo) + 1).ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void txtLoadingDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalcProcessTime();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting Load Date.\r\nSee error log for detail."));
        }
    }

    protected void txtUnLoadingDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalcProcessTime();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting un Load Date.\r\nSee error log for detail."));
        }
    }

    protected void txtMachineStopage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalcProcessTime();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting machine stopage time.\r\nSee error log for detail."));
        }
    }

    private void CalcProcessTime()
    {
        try
        {
            string msg = string.Empty;

            DateTime dtLoad = System.DateTime.Now.Date;
            if (!DateTime.TryParse(txtLoadingDate.Text.Trim(), out dtLoad))
            {
                msg += "Please select Load Date.";
            }

            DateTime dtunLoad = System.DateTime.Now.Date;
            if (!DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out dtunLoad))
            {
                msg += "Please select Un-Load Date.";
            }

            if (msg.Equals(string.Empty))
            {
                int StopageTime = 0;
                int.TryParse(txtMachineStopage.Text, out StopageTime);

                TimeSpan tm = dtunLoad.Subtract(dtLoad);
                int diffday = tm.Days;
                int diffhour = tm.Hours;
                int diffminute = tm.Minutes;
                int Totaldiffminute = diffday * 24 * 60 + diffhour * 60 + diffminute;

                int TotalProcessTime = Totaldiffminute - StopageTime;

                if (TotalProcessTime < 0)
                {
                    CommonFuction.ShowMessage("Please select load date and time less than unload date and time");
                    txtUnLoadingDate.Text = string.Empty;
                }
                txtProcessTime.Text = TotalProcessTime.ToString();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch
        {
            throw;
        }
    }

    #region Lot Detail

    protected void ddlLotNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLotData(e.Text.ToUpper());

            ddlLotNo.Items.Clear();

            ddlLotNo.DataSource = data;
            ddlLotNo.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in loading Lot information.\r\nSee error log for detail."));
        }

    }

    private DataTable GetLotData(string Text)
    {
        try
        {
            string CommandText = "select * FROM (select LOT_NUMBER,ORDER_NO,PROS_CODE,DEPT_CODE, (ORDER_NO||'@'||ORDER_NO||'@'||LOT_QTY||'@'||(nvl(STOCK_QTY,0)-nvl(UN_CONF_QTY,0)))LOT_DATA  from YRN_WIP_STOCK WHERE DEPT_CODE='" + oUserLoginDetail.VC_DEPARTMENTCODE + "') ASD ";
            string WhereClause = "  where LOT_NUMBER like :SearchQuery or ORDER_NO like :SearchQuery or PROS_CODE like :SearchQuery";
            string SortExpression = " order by LOT_NUMBER asc";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlLotNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ddlLotNo.SelectedIndex != -1)
            {
                string[] arrString = ddlLotNo.SelectedValue.Split('@');

                txtOrderNo.Text = arrString[0].ToString();
                txtOrderDescription.Text = arrString[1].ToString();

                txtLotQty.Text = arrString[2].ToString();
                txtMaxLoadQty.Text = arrString[3].ToString();
                txtLoadQty.Text = arrString[3].ToString();
                txtUnloadQty.Text = arrString[3].ToString();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting lot detail.\r\nSee error log for detail."));
        }
    }

    protected void txtLoadNoOfUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!GetLoadQty())
            {
                txtLoadNoOfUnit.Text = "0";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in Load Qty.\r\nSee error log for detail."));
        }
    }

    protected void txtLoadWeightOfUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!GetLoadQty())
            {
                txtLoadWeightOfUnit.Text = "0";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in Load Qty.\r\nSee error log for detail."));
        }
    }

    private bool GetLoadQty()
    {
        try
        {
            bool bVal = false;

            double LoadNoOfUnit = 0;
            double LoadWeightOfUnit = 0;
            double MaxLoadQty = 0;

            double.TryParse(txtLoadNoOfUnit.Text, out LoadNoOfUnit);
            txtLoadNoOfUnit.Text = LoadNoOfUnit.ToString();

            double.TryParse(txtLoadWeightOfUnit.Text, out LoadWeightOfUnit);
            txtLoadWeightOfUnit.Text = LoadWeightOfUnit.ToString();

            double.TryParse(txtMaxLoadQty.Text, out MaxLoadQty);
            txtMaxLoadQty.Text = MaxLoadQty.ToString();

            double LoadQty = LoadNoOfUnit * LoadWeightOfUnit;

            if (LoadQty > MaxLoadQty)
            {
                txtLoadQty.Text = "0";
                CommonFuction.ShowMessage("Load quantity can not exceed " + MaxLoadQty);
                bVal = false;
            }
            else
            {
                txtLoadQty.Text = LoadQty.ToString();
                bVal = true;
            }
            return bVal;
        }
        catch
        {
            throw;
        }
    }

    protected void txtUnloadNoOfUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GetUnLoadQty();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in UnLoad Qty.\r\nSee error log for detail."));
        }

    }

    protected void txtUnloadWeightOfUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GetUnLoadQty();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in UnLoad Qty.\r\nSee error log for detail."));
        }
    }

    private void GetUnLoadQty()
    {
        try
        {

            double UnLoadNoOfUnit = 0;
            double UnLoadWeightOfUnit = 0;

            double.TryParse(txtUnloadNoOfUnit.Text, out UnLoadNoOfUnit);
            txtUnloadNoOfUnit.Text = UnLoadNoOfUnit.ToString();

            double.TryParse(txtUnloadWeightOfUnit.Text, out UnLoadWeightOfUnit);
            txtUnloadWeightOfUnit.Text = UnLoadWeightOfUnit.ToString();

            double UnLoadQty = UnLoadNoOfUnit * UnLoadWeightOfUnit;

            txtUnloadQty.Text = UnLoadQty.ToString();

        }
        catch
        {
            throw;
        }
    }

    protected void btnsaveLotDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateLotDetailRow(out msg))
            {
                SaveLotDetailRow();
                BindLotDetailGrid();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in adding lot detail.\r\nSee error log for detail."));
        }
    }

    private bool ValidateLotDetailRow(out string msg)
    {
        try
        {

            int iCount = 0;
            int TotalCount = 0;
            msg = string.Empty;

            int UNIQUE_TRN = 0;
            if (ViewState["UNIQUE_TRN"] != null)
                UNIQUE_TRN = int.Parse(ViewState["UNIQUE_TRN"].ToString());

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

    private void SaveLotDetailRow()
    {
        try
        {
            if (dtProdTRN == null)
                CreateLotDetailTable();

            if (dtProdTRN.Rows.Count < 15)
            {
                int UNIQUE_TRN = 0;
                if (ViewState["UNIQUE_TRN"] != null)
                    UNIQUE_TRN = int.Parse(ViewState["UNIQUE_TRN"].ToString());

                string LotNo = ddlLotNo.SelectedText.Trim();
                bool bb = SearchTRNLotInGrid(LotNo, UNIQUE_TRN);
                if (!bb)
                {
                    double LOAD_QTY = 0;
                    double.TryParse(txtLoadQty.Text.Trim(), out LOAD_QTY);

                    double UNLOAD_QTY = 0;
                    double.TryParse(txtUnloadQty.Text.Trim(), out UNLOAD_QTY);

                    double LOT_QTY = 0;
                    double.TryParse(txtLotQty.Text, out LOT_QTY);

                    if (LOAD_QTY > 0 && UNLOAD_QTY > 0)
                    {
                        if (UNIQUE_TRN > 0)
                        {
                            DataView dv = new DataView(dtProdTRN);
                            dv.RowFilter = "UNIQUE_TRN=" + UNIQUE_TRN;
                            if (dv.Count > 0)
                            {
                                dv[0]["ORDER_NO"] = txtOrderNo.Text.Trim();
                                dv[0]["ORDER_DESC"] = txtOrderDescription.Text.Trim();
                                dv[0]["LOT_NUMBER"] = LotNo.Trim();
                                dv[0]["LOT_QTY"] = LOT_QTY;

                                int LOAD_NO_OF_UNIT = 0;
                                int.TryParse(txtLoadNoOfUnit.Text.Trim(), out LOAD_NO_OF_UNIT);
                                dv[0]["LOAD_NO_OF_UNIT"] = LOAD_NO_OF_UNIT;

                                dv[0]["LOAD_UOM_OF_UNIT"] = ddlLoadUOM.SelectedItem.Text.ToString();

                                double LOAD_WEIGHT_OF_UNIT = 0;
                                double.TryParse(txtLoadWeightOfUnit.Text.Trim(), out LOAD_WEIGHT_OF_UNIT);
                                dv[0]["LOAD_WEIGHT_OF_UNIT"] = LOAD_WEIGHT_OF_UNIT;
                                dv[0]["LOAD_QTY"] = LOAD_QTY;

                                int UNLOAD_NO_OF_UNIT = 0;
                                int.TryParse(txtUnloadNoOfUnit.Text.Trim(), out UNLOAD_NO_OF_UNIT);
                                dv[0]["UNLOAD_NO_OF_UNIT"] = UNLOAD_NO_OF_UNIT;

                                dv[0]["UNLOAD_UOM_OF_UNIT"] = ddlUnloadUOM.SelectedItem.Text.ToString();

                                double UNLOAD_WEIGHT_OF_UNIT = 0;
                                double.TryParse(txtUnloadWeightOfUnit.Text.Trim(), out UNLOAD_WEIGHT_OF_UNIT);
                                dv[0]["UNLOAD_WEIGHT_OF_UNIT"] = UNLOAD_WEIGHT_OF_UNIT;
                                dv[0]["UNLOAD_QTY"] = UNLOAD_QTY;

                                dv[0]["DEPT_CODE"] = oUserLoginDetail.VC_DEPARTMENTCODE;
                                dv[0]["FR_LOCATION"] = string.Empty;
                                dv[0]["TO_LOCATION"] = txtToLocation.Text.Trim();
                                dv[0]["FR_BATCH_NO"] = string.Empty;
                                dv[0]["TO_BATCH_NO"] = txtToBatchNo.Text.Trim();
                                dv[0]["FR_PROS_CODE"] = string.Empty;
                                dv[0]["LOAD_DATE"] = string.Empty; // DateTime.Parse(txtLoadingDateTime.Text.Trim());
                                dv[0]["UNLOAD_DATE"] = string.Empty; // DateTime.Parse(txtUnLoadingDateTime.Text.Trim());

                                dtProdTRN.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtProdTRN.NewRow();
                            dr["UNIQUE_TRN"] = dtProdTRN.Rows.Count + 1;

                            dr["ORDER_NO"] = txtOrderNo.Text.Trim();
                            dr["ORDER_DESC"] = txtOrderDescription.Text.Trim();
                            dr["LOT_NUMBER"] = LotNo.Trim();
                            dr["LOT_QTY"] = LOT_QTY;

                            int LOAD_NO_OF_UNIT = 0;
                            int.TryParse(txtLoadNoOfUnit.Text.Trim(), out LOAD_NO_OF_UNIT);
                            dr["LOAD_NO_OF_UNIT"] = LOAD_NO_OF_UNIT;

                            dr["LOAD_UOM_OF_UNIT"] = ddlLoadUOM.SelectedItem.Text.ToString();

                            double LOAD_WEIGHT_OF_UNIT = 0;
                            double.TryParse(txtLoadWeightOfUnit.Text.Trim(), out LOAD_WEIGHT_OF_UNIT);
                            dr["LOAD_WEIGHT_OF_UNIT"] = LOAD_WEIGHT_OF_UNIT;
                            dr["LOAD_QTY"] = LOAD_QTY;

                            int UNLOAD_NO_OF_UNIT = 0;
                            int.TryParse(txtUnloadNoOfUnit.Text.Trim(), out UNLOAD_NO_OF_UNIT);
                            dr["UNLOAD_NO_OF_UNIT"] = UNLOAD_NO_OF_UNIT;

                            dr["UNLOAD_UOM_OF_UNIT"] = ddlUnloadUOM.SelectedItem.Text.ToString();

                            double UNLOAD_WEIGHT_OF_UNIT = 0;
                            double.TryParse(txtUnloadWeightOfUnit.Text.Trim(), out UNLOAD_WEIGHT_OF_UNIT);
                            dr["UNLOAD_WEIGHT_OF_UNIT"] = UNLOAD_WEIGHT_OF_UNIT;
                            dr["UNLOAD_QTY"] = UNLOAD_QTY;

                            dr["DEPT_CODE"] = oUserLoginDetail.VC_DEPARTMENTCODE;
                            dr["FR_LOCATION"] = string.Empty;
                            dr["TO_LOCATION"] = txtToLocation.Text.Trim();
                            dr["FR_BATCH_NO"] = string.Empty;
                            dr["TO_BATCH_NO"] = txtToBatchNo.Text.Trim();
                            dr["FR_PROS_CODE"] = string.Empty;
                            dr["LOAD_DATE"] = string.Empty; // DateTime.Parse(txtLoadingDateTime.Text.Trim());
                            dr["UNLOAD_DATE"] = string.Empty; //DateTime.Parse(txtUnLoadingDateTime.Text.Trim());

                            dtProdTRN.Rows.Add(dr);
                        }
                        RefreshProdTRN();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(@"load and unload Quantity can not be zero");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage(@"enter valid lot number");
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchTRNLotInGrid(string LotNo, int UNIQUE_TRN)
    {
        bool Result = false;
        try
        {
            if (grdLotDetail.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdLotDetail.Rows)
                {
                    Label lblLotNo = (Label)grdRow.FindControl("lblLotNo");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUE_TRN = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                    if (lblLotNo.Text.Trim() == LotNo && UNIQUE_TRN != iUNIQUE_TRN)
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

    private void BindLotDetailGrid()
    {
        try
        {
            grdLotDetail.DataSource = dtProdTRN;
            grdLotDetail.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void CreateLotDetailTable()
    {
        try
        {
            dtProdTRN = new DataTable();
            dtProdTRN.Columns.Add("UNIQUE_TRN", typeof(int));
            dtProdTRN.Columns.Add("ORDER_NO", typeof(string));
            dtProdTRN.Columns.Add("ORDER_DESC", typeof(string));
            dtProdTRN.Columns.Add("LOT_NUMBER", typeof(string));
            dtProdTRN.Columns.Add("LOT_QTY", typeof(double));
            dtProdTRN.Columns.Add("LOAD_QTY", typeof(double));
            dtProdTRN.Columns.Add("LOAD_NO_OF_UNIT", typeof(int));
            dtProdTRN.Columns.Add("LOAD_UOM_OF_UNIT", typeof(string));
            dtProdTRN.Columns.Add("LOAD_WEIGHT_OF_UNIT", typeof(double));
            dtProdTRN.Columns.Add("UNLOAD_QTY", typeof(double));
            dtProdTRN.Columns.Add("UNLOAD_NO_OF_UNIT", typeof(int));
            dtProdTRN.Columns.Add("UNLOAD_UOM_OF_UNIT", typeof(string));
            dtProdTRN.Columns.Add("UNLOAD_WEIGHT_OF_UNIT", typeof(double));
            dtProdTRN.Columns.Add("DEPT_CODE", typeof(string));
            dtProdTRN.Columns.Add("FR_LOCATION", typeof(string));
            dtProdTRN.Columns.Add("TO_LOCATION", typeof(string));
            dtProdTRN.Columns.Add("FR_BATCH_NO", typeof(string));
            dtProdTRN.Columns.Add("TO_BATCH_NO", typeof(string));
            dtProdTRN.Columns.Add("FR_PROS_CODE", typeof(string));
            dtProdTRN.Columns.Add("LOAD_DATE", typeof(string));
            dtProdTRN.Columns.Add("UNLOAD_DATE", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancelLotDetail_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshProdTRN();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in cancel lot detail.\r\nSee error log for detail."));
        }
    }

    protected void grdLotDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_TRN = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DelLotDetail")
            {
                DeleteLotDetailRow(UNIQUE_TRN);
                BindLotDetailGrid();
            }
            if (e.CommandName == "EditLotDetail")
            {
                FillLotDetailRowByGrid(UNIQUE_TRN);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in lot updation/ deletion.\r\nSee error log for detail."));
        }
    }

    private void DeleteLotDetailRow(int UNIQUE_TRN)
    {
        try
        {
            if (dtProdTRN.Rows.Count == 1)
            {
                dtProdTRN.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtProdTRN.Rows)
                {
                    int iUNIQUE_TRN = int.Parse(dr["UNIQUE_TRN"].ToString());
                    if (iUNIQUE_TRN == UNIQUE_TRN)
                    {
                        dtProdTRN.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtProdTRN.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_TRN"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillLotDetailRowByGrid(int UNIQUE_TRN)
    {
        try
        {
            DataView dv = new DataView(dtProdTRN);
            dv.RowFilter = "UNIQUE_TRN=" + UNIQUE_TRN;
            if (dv.Count > 0)
            {
                ViewState["UNIQUE_TRN"] = UNIQUE_TRN;

                txtLoadQty.Text = dv[0]["LOAD_QTY"].ToString();
                txtUnloadQty.Text = dv[0]["UNLOAD_QTY"].ToString();
                txtToBatchNo.Text = dv[0]["TO_BATCH_NO"].ToString();
                txtToLocation.Text = dv[0]["TO_LOCATION"].ToString();
                //txtLoadingDateTime.Text = dv[0]["LOAD_DATE"].ToString();
                //txtUnLoadingDateTime.Text = dv[0]["UNLOAD_DATE"].ToString();

                // code to bind Lot No
                DataTable data = GetLotData("");
                ddlLotNo.Items.Clear();
                ddlLotNo.DataSource = data;
                ddlLotNo.DataTextField = "LOT_NUMBER";
                ddlLotNo.DataValueField = "SEARCH_DATA";
                ddlLotNo.DataBind();

                ddlLotNo.Enabled = false;

                foreach (Obout.ComboBox.ComboBoxItem item in ddlLotNo.Items)
                {
                    if (item.Text == dv[0]["LOT_NUMBER"].ToString())
                    {
                        ddlLotNo.SelectedIndex = ddlLotNo.Items.IndexOf(item);

                        string[] arrString = item.Value.Split('@');

                        txtOrderNo.Text = arrString[0].ToString();
                        txtOrderDescription.Text = arrString[1].ToString();
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

    #endregion

    #region Color Chemical

    protected void ddlItem_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItemData(e.Text.ToUpper());

            ddlItem.Items.Clear();

            ddlItem.DataSource = data;
            ddlItem.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in loading Item Detail.\r\nSee error log for detail."));
        }

    }

    private DataTable GetItemData(string Text)
    {
        try
        {
            string CommandText = "SELECT * FROM TX_ITEM_MST";
            string WhereClause = "  WHERE ITEM_CODE like :SearchQuery or ITEM_TYPE like :SearchQuery or ITEM_DESC like :SearchQuery";
            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ddlItem.SelectedIndex != -1)
            {
                txtItemDesc.Text = ddlItem.SelectedValue.ToString();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting item.\r\nSee error log for detail."));
        }
    }

    protected void btnSaveItemDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateColorChemRow(out msg))
            {
                SaveColorChemRow();
                BindColorChemGrid();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in adding color chemical detail.\r\nSee error log for detail."));
        }
    }

    private bool ValidateColorChemRow(out string msg)
    {
        try
        {

            int iCount = 0;
            int TotalCount = 0;
            msg = string.Empty;

            int UNIQUE_CHEM = 0;
            if (ViewState["UNIQUE_CHEM"] != null)
                UNIQUE_CHEM = int.Parse(ViewState["UNIQUE_CHEM"].ToString());

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

    private void SaveColorChemRow()
    {
        try
        {
            if (dtProdColorChem == null)
                CreateColorChemTable();

            if (dtProdColorChem.Rows.Count < 15)
            {
                int UNIQUE_CHEM = 0;
                if (ViewState["UNIQUE_CHEM"] != null)
                    UNIQUE_CHEM = int.Parse(ViewState["UNIQUE_CHEM"].ToString());

                string ItemCode = ddlItem.SelectedText.Trim();
                bool bb = SearchColorChemItemInGrid(ItemCode, UNIQUE_CHEM);
                if (!bb)
                {
                    double ITEM_QTY = 0;
                    double.TryParse(txtItemQty.Text.Trim(), out ITEM_QTY);

                    if (ITEM_QTY > 0)
                    {
                        if (UNIQUE_CHEM > 0)
                        {
                            DataView dv = new DataView(dtProdColorChem);
                            dv.RowFilter = "UNIQUE_CHEM=" + UNIQUE_CHEM;
                            if (dv.Count > 0)
                            {
                                dv[0]["ITEM_CODE"] = ItemCode;
                                dv[0]["ITEM_DESC"] = txtItemDesc.Text.Trim();
                                dv[0]["ITEM_QTY"] = ITEM_QTY;
                                dv[0]["ITEM_RATE"] = txtItemRate.Text.Trim();
                                dv[0]["BASIS"] = txtBasis.Text.Trim();

                                double Tub_Qty = 0;
                                double.TryParse(txtTubeQty.Text, out Tub_Qty);
                                dv[0]["TUB_QTY"] = Tub_Qty;

                                dv[0]["EXPR"] = txtExpr.Text.Trim();

                                double UNIT_QTY = 0;
                                double.TryParse(txtUnitQty.Text, out UNIT_QTY);
                                dv[0]["UNIT_QTY"] = UNIT_QTY;

                                dv[0]["DENSITY"] = txtDensity.Text.Trim();

                                dtProdColorChem.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtProdColorChem.NewRow();
                            dr["UNIQUE_CHEM"] = dtProdColorChem.Rows.Count + 1;

                            dr["ITEM_CODE"] = ItemCode;
                            dr["ITEM_DESC"] = txtItemDesc.Text.Trim();
                            dr["ITEM_QTY"] = ITEM_QTY;
                            dr["ITEM_RATE"] = txtItemRate.Text.Trim();
                            dr["BASIS"] = txtBasis.Text.Trim();

                            double Tub_Qty = 0;
                            double.TryParse(txtTubeQty.Text, out Tub_Qty);
                            dr["TUB_QTY"] = Tub_Qty;

                            dr["EXPR"] = txtExpr.Text.Trim();

                            double UNIT_QTY = 0;
                            double.TryParse(txtUnitQty.Text, out UNIT_QTY);
                            dr["UNIT_QTY"] = UNIT_QTY;

                            dr["DENSITY"] = txtDensity.Text.Trim();

                            dtProdColorChem.Rows.Add(dr);
                        }
                        RefreshProdColorChem();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(@"Quantity can not be zero");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage(@"enter valid item");
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchColorChemItemInGrid(string ItemCode, int UNIQUE_CHEM)
    {
        bool Result = false;
        try
        {
            if (grdItemDetail.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdItemDetail.Rows)
                {
                    Label lblItemCode = (Label)grdRow.FindControl("lblItemCode");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUE_CHEM = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                    if (lblItemCode.Text.Trim() == ItemCode && UNIQUE_CHEM != iUNIQUE_CHEM)
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

    private void CreateColorChemTable()
    {
        try
        {
            dtProdColorChem = new DataTable();
            dtProdColorChem.Columns.Add("UNIQUE_CHEM", typeof(int));
            dtProdColorChem.Columns.Add("ITEM_CODE", typeof(string));
            dtProdColorChem.Columns.Add("ITEM_DESC", typeof(string));
            dtProdColorChem.Columns.Add("ITEM_QTY", typeof(double));
            dtProdColorChem.Columns.Add("ITEM_RATE", typeof(double));
            dtProdColorChem.Columns.Add("BASIS", typeof(string));
            dtProdColorChem.Columns.Add("TUB_QTY", typeof(double));
            dtProdColorChem.Columns.Add("EXPR", typeof(string));
            dtProdColorChem.Columns.Add("UNIT_QTY", typeof(double));
            dtProdColorChem.Columns.Add("DENSITY", typeof(string));

        }
        catch
        {
            throw;
        }
    }

    protected void btnCancelItemDetail_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshProdColorChem();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in cancel color chemical detail.\r\nSee error log for detail."));
        }
    }

    protected void grdItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_CHEM = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DelItemDetail")
            {
                DeleteColorChemRow(UNIQUE_CHEM);
                BindColorChemGrid();
            }
            if (e.CommandName == "EditItemDetail")
            {
                FillColorChemRowByGrid(UNIQUE_CHEM);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in color chemical updation / deletion.\r\nSee error log for detail."));
        }
    }

    private void DeleteColorChemRow(int UNIQUE_CHEM)
    {
        try
        {
            if (dtProdColorChem.Rows.Count == 1)
            {
                dtProdColorChem.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtProdColorChem.Rows)
                {
                    int iUNIQUE_CHEM = int.Parse(dr["UNIQUE_CHEM"].ToString());
                    if (iUNIQUE_CHEM == UNIQUE_CHEM)
                    {
                        dtProdColorChem.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtProdColorChem.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_CHEM"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void FillColorChemRowByGrid(int UNIQUE_CHEM)
    {
        try
        {
            DataView dv = new DataView(dtProdColorChem);
            dv.RowFilter = "UNIQUE_CHEM=" + UNIQUE_CHEM;
            if (dv.Count > 0)
            {
                ViewState["UNIQUE_CHEM"] = UNIQUE_CHEM;

                txtBasis.Text = dv[0]["BASIS"].ToString();
                txtDensity.Text = dv[0]["DENSITY"].ToString();
                txtTubeQty.Text = dv[0]["TUB_QTY"].ToString();
                txtUnitQty.Text = dv[0]["UNIT_QTY"].ToString();
                txtExpr.Text = dv[0]["EXPR"].ToString();
                txtItemQty.Text = dv[0]["ITEM_QTY"].ToString();

                // code to bind Lot No
                DataTable data = GetItemData("");
                ddlItem.Items.Clear();
                ddlItem.DataSource = data;
                ddlItem.DataTextField = "ITEM_CODE";
                ddlItem.DataValueField = "SEARCH_DATA";
                ddlItem.DataBind();

                ddlItem.Enabled = false;

                foreach (Obout.ComboBox.ComboBoxItem item in ddlItem.Items)
                {
                    if (item.Text == dv[0]["ITEM_CODE"].ToString())
                    {
                        ddlItem.SelectedIndex = ddlItem.Items.IndexOf(item);

                        string[] arrString = item.Value.Split('@');

                        txtItemDesc.Text = arrString[0].ToString();
                        txtItemRate.Text = arrString[1].ToString();
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

    #endregion

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                SaveProductionEntry();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in saving Production entry.\r\nSee error log for detail."));
        }
    }

    private void SaveProductionEntry()
    {
        try
        {
            oYRN_PROD_MST = new SaitexDM.Common.DataModel.YRN_PROD_MST();

            oYRN_PROD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_PROD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_PROD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_PROD_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_PROD_MST.PROS_CODE = ddlProsCode.SelectedText.Trim();
            oYRN_PROD_MST.PROS_ID_NO = int.Parse(txtProsIdNo.Text);
            oYRN_PROD_MST.MACHINE_CODE = ddlMachineCode.SelectedText.Trim();
            oYRN_PROD_MST.TRN_DATE = DateTime.Parse(txtEntryDate.Text.Trim());
            oYRN_PROD_MST.TRN_TYPE = TRN_TYPE;
            oYRN_PROD_MST.SFT_ID = int.Parse(ddlShift.SelectedValue.Trim());
            oYRN_PROD_MST.LOAD_DATE = DateTime.Parse(txtLoadingDate.Text.Trim());
            oYRN_PROD_MST.UNLOAD_DATE = DateTime.Parse(txtUnLoadingDate.Text.Trim());
            CalcProcessTime();
            oYRN_PROD_MST.STOP_TIME = double.Parse(txtMachineStopage.Text.Trim());
            oYRN_PROD_MST.OPERATOR = txtOperator.Text.Trim();
            oYRN_PROD_MST.SUPERVISOR = txtSupervisor.Text.Trim(); ;
            oYRN_PROD_MST.REMARKS = txtRemarks.Text;
            oYRN_PROD_MST.DEL_STATUS = false;
            oYRN_PROD_MST.STATUS = true;
            oYRN_PROD_MST.TDATE = DateTime.Now.Date;
            oYRN_PROD_MST.TUSER = oUserLoginDetail.UserCode;

            string msg = string.Empty;
            int PROS_ID_NO = 0;

            DataTable dtMachineStopDataTable = new DataTable();
            if (Session["dtMachineStopDataTable"] != null)
            {
                dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];
            }

            bool result = SaitexBL.Interface.Method.YRN_PROD_MST.Insert(oYRN_PROD_MST, out PROS_ID_NO, dtProdTRN, dtProdColorChem, out msg,dtMachineStopDataTable);
            if (result)
            {
                ClearPage();

                msg = "Production Id Number : " + PROS_ID_NO + " saved successfully.";

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

            int iCountAll = 0;
            int count = 0;
            msg = string.Empty;

            iCountAll += 1;
            if (ValidateTRNDataForFormSaving(ref msg))
            {
                count += 1;
            }

            if (count == iCountAll)
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

            txtProsIdNo.Text = string.Empty;
            ddlProsCode.SelectedIndex = -1;
            txtProsDesc.Text = string.Empty;
            ddlMachineCode.SelectedIndex = -1;
            txtMachineDesc.Text = string.Empty;
            ddlProsIdNo.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in finding data.\r\nSee error log for detail."));
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

            ddlProsIdNo.Visible = true;
            txtProsIdNo.Visible = false;
            ddlProsCode.Enabled = false;

        }
        catch
        {
            throw;
        }
    }

    protected void ddlProsIdNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetProdData(e.Text.ToUpper());
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {

        }
    }

    protected DataTable GetProdData(string text)
    {
        try
        {
            string whereClause = " where PROS_CODE like :searchQuery or TRN_TYPE like :searchQuery or PROS_ID_NO like :searchQuery or SFT_ID like :searchQuery or MACHINE_CODE like :searchQuery or DEPT_CODE like :searchQuery";
            string sortExpression = " order by PROS_ID_NO desc";
            string commandText = "SELECT * FROM (select PROS_ID_NO,PROS_CODE,TRN_TYPE,MACHINE_CODE,DEPT_CODE,SFT_ID,(YEAR ||'@'|| COMP_CODE ||'@'|| BRANCH_CODE ||'@'||  TRN_TYPE ||'@'|| PROS_CODE ||'@'|| PROS_ID_NO ||'@'|| MACHINE_CODE )PROS_DATA from YRN_PROD_MST WHERE DEPT_CODE='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' ) ASD ";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlProsIdNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string PROD_STRING = ddlProsIdNo.SelectedValue.Trim();

            if (dtProdTRN == null || dtProdTRN.Rows.Count == 0)
                CreateLotDetailTable();

            if (dtProdColorChem == null || dtProdColorChem.Rows.Count == 0)
                CreateColorChemTable();

            dtProdTRN.Rows.Clear();
            dtProdColorChem.Rows.Clear();

            int iRecordFound = GetdataByProsIdNo(Common.CommonFuction.funFixQuotes(PROD_STRING));

            if (iRecordFound > 0)
            {
                ActivateUpdateMode();
            }
            else
            {
                ClearPage();
                lblMode.Text = "Update";
                txtProsIdNo.Text = "";

                ActivateUpdateMode();

                string msg = "Dear " + oUserLoginDetail.Username + " !! Production entry already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }

    private int GetdataByProsIdNo(string PROD_STRING)
    {
        int iRecordFound = 0;
        try
        {

            string[] Prod_strings = PROD_STRING.Split('@');
            int iYear = int.Parse(Prod_strings[0].ToString());
            string sComp_code = Prod_strings[1].ToString();
            string sBRANCH_CODE = Prod_strings[2].ToString();
            string sTRN_TYPE = Prod_strings[3].ToString();
            string sPROS_CODE = Prod_strings[4].ToString();
            string sPROS_ID_NO = Prod_strings[5].ToString();
            string sMACHINE_CODE = Prod_strings[6].ToString();

            oYRN_PROD_MST = new SaitexDM.Common.DataModel.YRN_PROD_MST();
            oYRN_PROD_MST.YEAR = iYear;
            oYRN_PROD_MST.COMP_CODE = sComp_code;
            oYRN_PROD_MST.BRANCH_CODE = sBRANCH_CODE;
            oYRN_PROD_MST.TRN_TYPE = sTRN_TYPE;
            oYRN_PROD_MST.PROS_CODE = sPROS_CODE;
            oYRN_PROD_MST.PROS_ID_NO = int.Parse(sPROS_ID_NO);

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetProdDataByPROS_ID_NO(oYRN_PROD_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;

                txtProsIdNo.Text = dt.Rows[0]["PROS_ID_NO"].ToString().Trim();

                txtEntryDate.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByValue(dt.Rows[0]["SFT_ID"].ToString().Trim()));

                txtLoadingDate.Text = dt.Rows[0]["LOAD_DATE"].ToString().Trim();
                txtUnLoadingDate.Text = dt.Rows[0]["UNLOAD_DATE"].ToString().Trim();
                txtMachineStopage.Text = dt.Rows[0]["STOP_TIME"].ToString().Trim();
                CalcProcessTime();
                txtOperator.Text = dt.Rows[0]["OPERATOR"].ToString().Trim();
                txtSupervisor.Text = dt.Rows[0]["SUPERVISOR"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString().Trim();

                lblProsCode.Text = dt.Rows[0]["PROS_CODE"].ToString().Trim();

                DataTable data = GetProsData("");
                ddlProsCode.DataSource = data;
                ddlProsCode.DataTextField = "PROS_CODE";
                ddlProsCode.DataValueField = "PROS_DESC";
                ddlProsCode.DataBind();

                foreach (Obout.ComboBox.ComboBoxItem item in ddlProsCode.Items)
                {
                    if (item.Text == dt.Rows[0]["PROS_CODE"].ToString().Trim())
                    {
                        ddlProsCode.SelectedIndex = ddlProsCode.Items.IndexOf(item);
                        txtProsDesc.Text = item.Value.Trim();

                        break;
                    }
                }

                DataTable dataMac = GetMachineData("");
                ddlMachineCode.DataSource = dataMac;
                ddlMachineCode.DataTextField = "MACHINE_CODE";
                ddlMachineCode.DataValueField = "MACHINE_DATA";
                ddlMachineCode.DataBind();

                foreach (Obout.ComboBox.ComboBoxItem item in ddlMachineCode.Items)
                {
                    if (item.Text == dt.Rows[0]["MACHINE_CODE"].ToString().Trim())
                    {
                        ddlMachineCode.SelectedIndex = ddlMachineCode.Items.IndexOf(item);
                        txtMachineDesc.Text = item.Value.Trim();
                        break;
                    }
                }
            }

            if (iRecordFound == 1)
            {
                DataTable dtTemp_ProdTRN = SaitexBL.Interface.Method.YRN_PROD_MST.GetTRN_ByProsIdNo(oYRN_PROD_MST);
                DataTable dtTemp_ColorChem = SaitexBL.Interface.Method.YRN_PROD_MST.GetCOLORCHEM_ByProsIdNo(oYRN_PROD_MST);

                if (dtTemp_ProdTRN != null && dtTemp_ProdTRN.Rows.Count > 0)
                {
                    MapDataTable_ProdTRN(dtTemp_ProdTRN);

                    BindLotDetailGrid();
                }

                if (dtTemp_ColorChem != null && dtTemp_ColorChem.Rows.Count > 0)
                {
                    MapDataTable_ColorChem(dtTemp_ColorChem);

                    BindColorChemGrid();
                }
            }

            return iRecordFound;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MapDataTable_ProdTRN(DataTable dtTemp)
    {
        try
        {
            if (dtProdTRN == null || dtProdTRN.Rows.Count == 0)
                CreateLotDetailTable();
            dtProdTRN.Rows.Clear();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtProdTRN.NewRow();

                    dr["UNIQUE_TRN"] = dtProdTRN.Rows.Count + 1;
                    dr["ORDER_NO"] = drTemp["ORDER_NO"];
                    dr["LOT_NUMBER"] = drTemp["LOT_NUMBER"];
                    dr["LOT_QTY"] = drTemp["LOT_QTY"];

                    int lOAD_NO_OF_UNIT = 1;
                    int.TryParse(drTemp["LOAD_NO_OF_UNIT"].ToString(), out lOAD_NO_OF_UNIT);
                    if (lOAD_NO_OF_UNIT > 0)
                        dr["LOAD_NO_OF_UNIT"] = lOAD_NO_OF_UNIT;
                    else
                        dr["LOAD_NO_OF_UNIT"] = 1;

                    dr["LOAD_UOM_OF_UNIT"] = drTemp["LOAD_UOM_OF_UNIT"];

                    double LOAD_WEIGHT_OF_UNIT = 0;
                    double.TryParse(drTemp["LOAD_WEIGHT_OF_UNIT"].ToString(), out LOAD_WEIGHT_OF_UNIT);
                    if (LOAD_WEIGHT_OF_UNIT > 0)
                        dr["LOAD_WEIGHT_OF_UNIT"] = LOAD_WEIGHT_OF_UNIT;
                    else
                        dr["LOAD_WEIGHT_OF_UNIT"] = drTemp["LOAD_QTY"];

                    dr["LOAD_QTY"] = drTemp["LOAD_QTY"];
                    dr["DEPT_CODE"] = drTemp["DEPT_CODE"];

                    int UNLOAD_NO_OF_UNIT = 1;
                    int.TryParse(drTemp["UNLOAD_NO_OF_UNIT"].ToString(), out UNLOAD_NO_OF_UNIT);
                    if (UNLOAD_NO_OF_UNIT > 0)
                        dr["UNLOAD_NO_OF_UNIT"] = UNLOAD_NO_OF_UNIT;
                    else
                        dr["UNLOAD_NO_OF_UNIT"] = 1;


                    dr["UNLOAD_UOM_OF_UNIT"] = drTemp["UNLOAD_UOM_OF_UNIT"];

                    double UNLOAD_WEIGHT_OF_UNIT = 0;
                    double.TryParse(drTemp["UNLOAD_WEIGHT_OF_UNIT"].ToString(), out UNLOAD_WEIGHT_OF_UNIT);
                    if (UNLOAD_WEIGHT_OF_UNIT > 0)
                        dr["UNLOAD_WEIGHT_OF_UNIT"] = UNLOAD_WEIGHT_OF_UNIT;
                    else
                        dr["UNLOAD_WEIGHT_OF_UNIT"] = drTemp["UNLOAD_QTY"];

                    dr["UNLOAD_QTY"] = drTemp["UNLOAD_QTY"];
                    dr["FR_LOCATION"] = drTemp["FR_LOCATION"];
                    dr["TO_LOCATION"] = drTemp["TO_LOCATION"];
                    dr["FR_BATCH_NO"] = drTemp["FR_BATCH_NO"];
                    dr["TO_BATCH_NO"] = drTemp["TO_BATCH_NO"];
                    dr["FR_PROS_CODE"] = drTemp["FR_PROS_CODE"];
                    dr["LOAD_DATE"] = drTemp["LOAD_DATE"];
                    dr["UNLOAD_DATE"] = drTemp["UNLOAD_DATE"];

                    dtProdTRN.Rows.Add(dr);

                }
                dtTemp = null;
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable_ColorChem(DataTable dtTemp)
    {
        try
        {
            if (dtProdColorChem == null || dtProdColorChem.Rows.Count == 0)
                CreateColorChemTable();
            dtProdColorChem.Rows.Clear();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtProdColorChem.NewRow();

                    dr["UNIQUE_CHEM"] = dtProdColorChem.Rows.Count + 1;
                    dr["ITEM_CODE"] = drTemp["ITEM_CODE"];
                    dr["ITEM_QTY"] = drTemp["ITEM_QTY"];
                    dr["ITEM_RATE"] = drTemp["ITEM_RATE"];
                    dr["BASIS"] = drTemp["BASIS"];
                    dr["TUB_QTY"] = drTemp["TUB_QTY"];
                    dr["EXPR"] = drTemp["EXPR"];
                    dr["UNIT_QTY"] = drTemp["UNIT_QTY"];
                    dr["DENSITY"] = drTemp["DENSITY"];
                    dtProdColorChem.Rows.Add(dr);

                }
                dtTemp = null;
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
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                UpdateProductionEntry();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in updation .\r\nSee error log for detail."));
        }
    }

    private void UpdateProductionEntry()
    {
        try
        {
            oYRN_PROD_MST = new SaitexDM.Common.DataModel.YRN_PROD_MST();

            oYRN_PROD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_PROD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_PROD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_PROD_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_PROD_MST.PROS_CODE = lblProsCode.Text.Trim();
            oYRN_PROD_MST.PROS_ID_NO = int.Parse(txtProsIdNo.Text);
            oYRN_PROD_MST.MACHINE_CODE = ddlMachineCode.SelectedText.Trim();
            oYRN_PROD_MST.TRN_DATE = DateTime.Parse(txtEntryDate.Text.Trim());
            oYRN_PROD_MST.TRN_TYPE = TRN_TYPE;
            oYRN_PROD_MST.SFT_ID = int.Parse(ddlShift.SelectedValue.Trim());
            oYRN_PROD_MST.LOAD_DATE = DateTime.Parse(txtLoadingDate.Text.Trim());
            oYRN_PROD_MST.UNLOAD_DATE = DateTime.Parse(txtUnLoadingDate.Text.Trim());
            CalcProcessTime();
            double MachineStopage = 0;
            double.TryParse(txtMachineStopage.Text.Trim(), out MachineStopage);
            oYRN_PROD_MST.STOP_TIME = MachineStopage;
            oYRN_PROD_MST.OPERATOR = txtOperator.Text.Trim();
            oYRN_PROD_MST.SUPERVISOR = txtSupervisor.Text.Trim(); ;
            oYRN_PROD_MST.REMARKS = txtRemarks.Text;
            oYRN_PROD_MST.DEL_STATUS = false;
            oYRN_PROD_MST.STATUS = true;
            oYRN_PROD_MST.TDATE = DateTime.Now.Date;
            oYRN_PROD_MST.TUSER = oUserLoginDetail.UserCode;

            string msg = string.Empty;

            DataTable dtMachineStopDataTable = new DataTable();
            if (Session["dtMachineStopDataTable"] != null)
            {
                dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];
            }

            bool result = SaitexBL.Interface.Method.YRN_PROD_MST.Update(oYRN_PROD_MST, dtProdTRN, dtProdColorChem, out msg,dtMachineStopDataTable);
            if (result)
            {
                ClearPage();

                msg = "Production Id Number : " + oYRN_PROD_MST.PROS_ID_NO + " updated successfully.";

                Common.CommonFuction.ShowMessage(msg);
            }
            else
            {
                Common.CommonFuction.ShowMessage("data updation Failed");
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

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in deletion.\r\nSee error log for detail."));
        }

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearPage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in reloading page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/OrderDevelopment/Reports/OC_Parameter.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting help.\r\nSee error log for detail."));
        }
    }

}
