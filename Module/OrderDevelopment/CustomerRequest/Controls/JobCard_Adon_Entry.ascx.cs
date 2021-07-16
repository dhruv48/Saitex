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
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_OrderDevelopment_CustomerRequest_Controls_JobCard_Adon_Entry : System.Web.UI.UserControl
{
    List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN> dtBatchTrn;
    SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string PRODUCT_TYPE = string.Empty;
    private DataTable dtDetailTBL = null;
    private DataTable dtLotDetail = null;
    #region code to determine Refresh state of Page ie. button click or F5 hit
    private bool _refreshState; private bool _isRefresh;
    List<string> Rack_List = new List<string>();

    protected override void LoadViewState(object savedState)
    {
        object[] AllStates = (object[])savedState;
        base.LoadViewState(AllStates[0]);
        _refreshState = bool.Parse(AllStates[1].ToString());
        _isRefresh = _refreshState == bool.Parse(Session["__ISREFRESH"].ToString());
    }

    protected override object SaveViewState()
    {
        Session["__ISREFRESH"] = _refreshState;
        object[] AllStates = new object[2];
        AllStates[0] = base.SaveViewState();
        AllStates[1] = !(_refreshState);
        return AllStates;
    }
    #endregion

    public string PRODUCTTYPE
    {
        get
        {
            return PRODUCT_TYPE;
        }
        set
        {
            PRODUCT_TYPE = value;
        }
    }

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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void InitialisePage()
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            lblMode.Text = "Update";
            ddlBatchCode.Visible = true;
            txtBatchCode.Visible = false;
            GREYLOT.Visible = false;
            PA_NO.Visible = false;
            LABDIP.Visible = false;
            Process.Visible = false;
            ddlProcessCode.Enabled = false;
            ddlPaNo.Enabled = false;
            ddlGreyLot.Enabled = false;
            txtProcess.Visible = true;
            ddlBatchCode.SelectedIndex = -1;
            txtArticle.Text = string.Empty;
            txtArticleDesc.Text = string.Empty;
            txtBatchCode.Text = string.Empty;
            txtBatchDate.Text = string.Empty;
            txtCustReqNo.Text = string.Empty;
            txtLotSize.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtPaNo.Text = string.Empty;
            txtPartDtl.Text = string.Empty;
            txtParty.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtTRNno.Text = string.Empty;
            txtShade.Text = string.Empty;
            txtMachineCode.Text = string.Empty;
            txtMachineName.Text = string.Empty;
            txtMachineCap.Text = string.Empty;
            txtMachineSpindle.Text = string.Empty;
            txtGreyLotNo.Text = string.Empty;
            txtLabDipNo.Text = string.Empty;
            txtOption.Text = string.Empty;
            txtSpring.Text = string.Empty;
            txtProcess.Text = string.Empty;
            ddlGreyLot.SelectedIndex = -1;
            ddlPaNo.SelectedIndex = -1;
            ddlTint.SelectedIndex = -1;
            ddlTrolyNo.Text = string.Empty;
            txtIssueDate.Text = string.Empty;
            txtIssueNo.Text = string.Empty;
            grdProcessTrn.DataSource = null;
            grdProcessTrn.DataBind();
            dtBatchTrn = null;
            RefreshDetailRow();
            RefreshLotDetailRow();
            BindGridFromDataTable();
            BindGridFromLotDataTable();
            BindBatchCode();
            Bind_ChemicalUOM();
            GetDyeProcess();
            Bind_CHEMICAL_BASIS();
            txtBatchDate.Text = DateTime.Now.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }
    private void RefreshLotDetailRow()
    {
        if (ViewState["dtLotDetail"] != null)
            dtLotDetail = (DataTable)ViewState["dtLotDetail"];
        if (dtLotDetail == null)
            CreateLotDataTable();
        if (dtLotDetail != null)
            dtLotDetail.Rows.Clear();
        grdDyesTrn.DataSource = null;
        grdDyesTrn.DataBind();
        ddlProcessCode.SelectedIndex = -1;
    }
    private void RefreshDetailRow()
    {
        if (ViewState["dtDetailTBL"] != null)
            dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
        if (dtDetailTBL == null)
            CreateDataTable();
        if (dtDetailTBL != null)
            dtDetailTBL.Rows.Clear();
        grdProcessTrn.DataSource = null;
        grdProcessTrn.DataBind();
        ddlProcessCode.SelectedIndex = -1;
    }
    private void RefreshRow()
    {
        try
        {
            ddlChemicalBasis.SelectedIndex = -1;
            ddlDyeProcessCode.SelectedIndex = -1;
            txtItemCode.SelectedIndex = -1;
            txtICode.Text = string.Empty;
            txtAditionQty.Text = string.Empty;
            txtAdition2Qty.Text = string.Empty;
            txtProcessDesc.Text = "";
            txtChemicalQuantity.Text = "";
            ddlChemicalunit.SelectedIndex = -1;
            txtItemQtykg.Text = "";
            txtiMachineVolum.Text = "";
            txtiItemQty.Text = "";
            ViewState["UniqueId"] = null;
            ddlChemicalBasis.SelectedIndex = -1;
            txtDyeRemarks.Text = "";
            txtTemp.Text = "";
            txtHoldTemp.Text = "";
        }
        catch
        {
            throw;
        }
    }

    private void BindBatchCode()
    {
        try
        {
            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            string BatchCode = SaitexBL.Interface.Method.BATCH_CARD_MST.GetNewBatchCode(oBATCH_CARD_MST);
            txtBatchCode.Text = BatchCode;
        }
        catch
        {
            throw;
        }
    }
    private void Bind_CHEMICAL_BASIS()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("DYE_BASIS", oUserLoginDetail.COMP_CODE);
            ddlChemicalBasis.DataSource = dt;
            ddlChemicalBasis.DataValueField = "MST_CODE";
            ddlChemicalBasis.DataTextField = "MST_DESC";
            ddlChemicalBasis.DataBind();
            //ddlFabType.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UniqueId", typeof(int));
            dtDetailTBL.Columns.Add("PARA_BASIS", typeof(string));
            dtDetailTBL.Columns.Add("DYE_PROCESS", typeof(string));
            dtDetailTBL.Columns.Add("ITEM_CODE", typeof(string));
            dtDetailTBL.Columns.Add("ITEM_DESC", typeof(string));
            dtDetailTBL.Columns.Add("QTY", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("LOT_SIZE", typeof(double));
            dtDetailTBL.Columns.Add("MACHINE_CAPACITY", typeof(double));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("ADD_TRN_QTY", typeof(string));
            dtDetailTBL.Columns.Add("ADD2_TRN_QTY", typeof(string));
            dtDetailTBL.Columns.Add("TEMP", typeof(string));
            dtDetailTBL.Columns.Add("HOLD_TIME", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("SR_NO", typeof(int));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindGridFromDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            if (dtDetailTBL == null)
                CreateDataTable();
            double qty = 0, LotSize = 0, Machine_Volumn = 0, Item_Qty = 0, dblTemp = 0;
            if (grdProcessTrn.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in grdProcessTrn.Rows)
                {
                    Label lblSr_No = (Label)gvrow.FindControl("lblSr");
                    Label txtPARA_BASIS = (Label)gvrow.FindControl("lblBasic");
                    Label txtDYE_PROCESS = (Label)gvrow.FindControl("txtDYE_PROCESS");
                    TextBox txtLotQty = (TextBox)gvrow.FindControl("txtLotQty");
                    TextBox txtMachineVolumn = (TextBox)gvrow.FindControl("txtMachineVolumn");
                    TextBox txtItemQty = (TextBox)gvrow.FindControl("txtItemQty");
                    //TextBox txtAditionQty = (TextBox)gvrow.FindControl("txtAditionQty");
                    //TextBox txtAdition2Qty = (TextBox)gvrow.FindControl("txtAdition2Qty");
                    Label txtICODE = (Label)gvrow.FindControl("txtICODE");
                    Label txtUNIT = (Label)gvrow.FindControl("txtUNIT");
                    TextBox txtQTY = (TextBox)gvrow.FindControl("txtQTY");
                    Label txtTemp = (Label)gvrow.FindControl("txtTemp");
                    Label txtHoldTemp = (Label)gvrow.FindControl("txtHoldTemp");
                    Label txtDRemarks = (Label)gvrow.FindControl("txtDyeRemarks");
                    double.TryParse(txtQTY.Text.Trim(), out qty);
                    double.TryParse(txtLotQty.Text.Trim(), out LotSize);
                    double.TryParse(txtMachineVolumn.Text.Trim(), out Machine_Volumn);
                    double.TryParse(txtItemQty.Text.Trim(), out Item_Qty);
                    if (txtUNIT.Text == "%")
                    {
                        dblTemp = (qty * LotSize) / 100;
                        txtItemQty.Text = dblTemp.ToString();
                    }
                    else
                    {
                        dblTemp = (qty * Machine_Volumn) / 1000;
                        txtItemQty.Text = dblTemp.ToString();
                    }
                    if (dtDetailTBL.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtDetailTBL.Rows)
                        {
                            if (dr["DYE_PROCESS"].ToString() == txtDYE_PROCESS.Text && dr["ITEM_CODE"].ToString() == txtICODE.Text && dr["UOM_OF_UNIT"].ToString() == txtUNIT.Text && dr["PARA_BASIS"].ToString() == txtPARA_BASIS.Text)
                            {
                                dr["QTY"] = qty;
                                dr["UOM_OF_UNIT"] = txtUNIT.Text;
                                dr["LOT_SIZE"] = LotSize;
                                dr["MACHINE_CAPACITY"] = Machine_Volumn;
                                dr["TRN_QTY"] = txtItemQty.Text;
                                dtDetailTBL.AcceptChanges();
                                break;
                            }

                        }
                    }
                }
            }
            grdProcessTrn.DataSource = dtDetailTBL;
            grdProcessTrn.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlPaNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPaNoData(e.Text.ToUpper(), e.ItemsOffset);

            ddlPaNo.Items.Clear();

            ddlPaNo.DataSource = data;
            ddlPaNo.DataTextField = "PI_NO";
            ddlPaNo.DataValueField = "TRN_DATA";
            ddlPaNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPaNoCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPaNoData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   *FROM   (  SELECT   O.CUST_REQ_NO, O.MACHINE_CODE, R.PI_NO, O.PLAN_QTY  AS TRN_QTY, O.ORDER_NO, O.ARTICAL_CODE, O.ARTICAL_DESC, O.SHADE_CODE, O.PRTY_CODE, O.PRTY_NAME,  O.TRN_NUMB,r.TRN_NUMB AS ISSUE_NO, r.TRN_DATE AS ISSUE_DATE, MAC.MACHINE_MAKE, MAC.SOFTWATER AS MACHINE_CAPACITY,MAC.NO_OF_SPINDLES, ( O.CUST_REQ_NO  || '@'|| R.PI_NO || '@' || O.PLAN_QTY || '@'|| O.ORDER_NO || '@' || O.ARTICAL_CODE || '@'|| O.ARTICAL_DESC || '@'|| O.SHADE_CODE || '@'|| O.MACHINE_CODE|| '@'|| MACHINE_MAKE  || '@'|| SOFTWATER|| '@'|| NO_OF_SPINDLES || '@'|| O.PRTY_CODE || '@'|| O.PRTY_NAME ||'@'||O.TRN_NUMB  || '@'|| r.TRN_NUMB || '@'|| r.TRN_DATE) TRN_DATA  FROM   V_YRN_IR_TRN r,  V_OD_CAPT_TRN_MAIN o,  MC_MACHINE_MASTER MAC WHERE   R.COMP_CODE = O.COMP_CODE  AND R.BRANCH_CODE = O.BRANCH_CODE AND MAC.MACHINE_CODE = O.MACHINE_CODE AND R.PI_NO = O.PI_NO AND R.MAC_CODE = O.MACHINE_CODE AND R.YEAR = O.YEAR and r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and r.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and r.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'  and r.trn_type='IYS01' and o.PRODUCT_TYPE= '" + PRODUCT_TYPE + "' AND (O.PI_NO, o.PLAN_QTY,  O.TRN_NUMB, O.COMP_CODE,  O.BRANCH_CODE, O.YEAR) NOT IN  (SELECT   PA_NO AS PI_NO, LOT_SIZE AS PLAN_QTY,  TRN_NUMB, COMP_CODE,    BRANCH_CODE,   YEAR FROM   BATCH_CARD_MST WHERE COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "') GROUP BY   O.CUST_REQ_NO, O.MACHINE_CODE,  R.PI_NO, O.PLAN_QTY,  O.ORDER_NO, O.ARTICAL_CODE,  O.ARTICAL_DESC,  O.SHADE_CODE,   O.PRTY_CODE, O.PRTY_NAME,  O.TRN_NUMB, R.TRN_NUMB, R.TRN_DATE, MAC.MACHINE_MAKE,  MAC.SOFTWATER,  MAC.NO_OF_SPINDLES ORDER BY   CUST_REQ_NO)  WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery   OR CUST_REQ_NO LIKE :SearchQuery  OR MACHINE_CODE  LIKE :SearchQuery   OR ARTICAL_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND trn_data NOT IN(SELECT trn_data FROM (SELECT * FROM (SELECT   O.CUST_REQ_NO, O.MACHINE_CODE, R.PI_NO, O.PLAN_QTY  AS TRN_QTY, O.ORDER_NO, O.ARTICAL_CODE, O.ARTICAL_DESC, O.SHADE_CODE, O.PRTY_CODE, O.PRTY_NAME,  O.TRN_NUMB,r.TRN_NUMB AS ISSUE_NO, r.TRN_DATE AS ISSUE_DATE, MAC.MACHINE_MAKE, MAC.SOFTWATER AS MACHINE_CAPACITY,MAC.NO_OF_SPINDLES, ( O.CUST_REQ_NO  || '@'|| R.PI_NO || '@' || O.PLAN_QTY || '@'|| O.ORDER_NO || '@' || O.ARTICAL_CODE || '@'|| O.ARTICAL_DESC || '@'|| O.SHADE_CODE || '@'|| O.MACHINE_CODE|| '@'|| MACHINE_MAKE  || '@'|| SOFTWATER|| '@'|| NO_OF_SPINDLES || '@'|| O.PRTY_CODE || '@'|| O.PRTY_NAME ||'@'||O.TRN_NUMB  || '@'|| r.TRN_NUMB || '@'|| r.TRN_DATE) TRN_DATA  FROM   V_YRN_IR_TRN r,  V_OD_CAPT_TRN_MAIN o,  MC_MACHINE_MASTER MAC WHERE   R.COMP_CODE = O.COMP_CODE  AND R.BRANCH_CODE = O.BRANCH_CODE AND MAC.MACHINE_CODE = O.MACHINE_CODE AND R.PI_NO = O.PI_NO and r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and r.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and r.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'  and r.trn_type='IYS01' and o.PRODUCT_TYPE= '" + PRODUCT_TYPE + "' AND AND (O.PI_NO, o.PLAN_QTY,  O.TRN_NUMB, O.COMP_CODE,  O.BRANCH_CODE, O.YEAR) NOT IN  (SELECT   PA_NO AS PI_NO, LOT_SIZE AS PLAN_QTY,  TRN_NUMB, COMP_CODE,    BRANCH_CODE,   YEAR FROM   BATCH_CARD_MST WHERE COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "') GROUP BY   O.CUST_REQ_NO, O.MACHINE_CODE,  R.PI_NO, O.PLAN_QTY,  O.ORDER_NO, O.ARTICAL_CODE,  O.ARTICAL_DESC,  O.SHADE_CODE,   O.PRTY_CODE, O.PRTY_NAME,  O.TRN_NUMB,  R.TRN_NUMB, R.TRN_DATE, MAC.MACHINE_MAKE,  MAC.SOFTWATER,  MAC.NO_OF_SPINDLES ORDER BY   CUST_REQ_NO)  WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery   OR CUST_REQ_NO LIKE :SearchQuery  OR MACHINE_CODE  LIKE :SearchQuery   OR ARTICAL_CODE LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by CUST_REQ_NO";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetPaNoCount(string text)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   *FROM   (  SELECT   O.CUST_REQ_NO, O.MACHINE_CODE, R.PI_NO, O.PLAN_QTY  AS TRN_QTY, O.ORDER_NO, O.ARTICAL_CODE, O.ARTICAL_DESC, O.SHADE_CODE, O.PRTY_CODE, O.PRTY_NAME,  O.TRN_NUMB, r.TRN_NUMB AS ISSUE_NO, r.TRN_DATE AS ISSUE_DATE, MAC.MACHINE_MAKE, MAC.SOFTWATER AS MACHINE_CAPACITY,MAC.NO_OF_SPINDLES, ( O.CUST_REQ_NO  || '@'|| R.PI_NO|| '@' || O.PLAN_QTY || '@'|| O.ORDER_NO || '@' || O.ARTICAL_CODE || '@'|| O.ARTICAL_DESC || '@'|| O.SHADE_CODE || '@'|| O.MACHINE_CODE|| '@'|| MACHINE_MAKE  || '@'|| SOFTWATER|| '@'|| NO_OF_SPINDLES || '@'|| O.PRTY_CODE || '@'|| O.PRTY_NAME ||'@'||O.TRN_NUMB  || '@'|| r.TRN_NUMB || '@'|| r.TRN_DATE) TRN_DATA  FROM   V_YRN_IR_TRN r,  V_OD_CAPT_TRN_MAIN o,  MC_MACHINE_MASTER MAC WHERE   R.COMP_CODE = O.COMP_CODE  AND R.BRANCH_CODE = O.BRANCH_CODE AND MAC.MACHINE_CODE = O.MACHINE_CODE AND R.PI_NO = O.PI_NO and r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and r.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and r.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' and r.trn_type='IYS01' and o.PRODUCT_TYPE= '" + PRODUCT_TYPE + "' AND AND (O.PI_NO, o.PLAN_QTY,  O.TRN_NUMB, O.COMP_CODE,  O.BRANCH_CODE, O.YEAR) NOT IN  (SELECT   PA_NO AS PI_NO, LOT_SIZE AS PLAN_QTY,  TRN_NUMB, COMP_CODE,    BRANCH_CODE,   YEAR FROM   BATCH_CARD_MST WHERE COMP_CODE='C00001' AND BRANCH_CODE='SL0001') GROUP BY   O.CUST_REQ_NO, O.MACHINE_CODE,  R.PI_NO, O.PLAN_QTY,  O.ORDER_NO, O.ARTICAL_CODE,  O.ARTICAL_DESC,  O.SHADE_CODE,   O.PRTY_CODE, O.PRTY_NAME, O.TRN_NUMB, R.TRN_NUMB, R.TRN_DATE, MAC.MACHINE_MAKE,  MAC.SOFTWATER,  MAC.NO_OF_SPINDLES ORDER BY   CUST_REQ_NO)  WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery   OR CUST_REQ_NO LIKE :SearchQuery  OR MACHINE_CODE  LIKE :SearchQuery   OR ARTICAL_CODE LIKE :SearchQuery)";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlPaNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string TRN_DATA = ddlPaNo.SelectedValue.Trim();
            string[] CombineData = TRN_DATA.Split('@');

            txtCustReqNo.Text = CombineData[0].ToString();
            txtPaNo.Text = CombineData[1].ToString();

            txtLotSize.Text = CombineData[2].ToString();
            txtOrderNo.Text = CombineData[3].ToString();
            txtArticle.Text = CombineData[4].ToString();
            txtArticleDesc.Text = CombineData[5].ToString();
            txtShade.Text = CombineData[6].ToString();

            txtMachineCode.Text = CombineData[7].ToString();
            txtMachineName.Text = CombineData[8].ToString();
            txtMachineCap.Text = CombineData[9].ToString();
            if (CombineData[10].ToString() != "")
                txtMachineSpindle.Text = CombineData[10].ToString();
            else
                txtMachineSpindle.Text = "0";
            txtParty.Text = CombineData[11].ToString();
            txtPartDtl.Text = CombineData[12].ToString();
            txtTRNno.Text = CombineData[13].ToString();
            txtIssueNo.Text = CombineData[14].ToString();
            txtIssueDate.Text = CombineData[15].ToString();
            GetProcessRouteByPaNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting PA No.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddlPaNo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DropDownList ddlTrolyNo = (e.Row.FindControl("ddlTrolyNo") as DropDownList);

        //    for (int i = 1; i <= 100; i++)
        //    {
        //        string Trolly_No = string.Format("Trolly-{0}", i);
        //        Rack_List.Add(Trolly_No);
        //    }
        //    ddlTrolyNo.DataSource = Rack_List;
        //    ddlTrolyNo.DataBind();
        //    ddlTrolyNo.Items.Insert(0, new ListItem("Select", string.Empty));
        //}
    }

    private void GetProcessRouteByPaNo()
    {
        try
        {
            dtBatchTrn = new List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>();
            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();

            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.PA_NO = txtPaNo.Text.Trim();

            dtBatchTrn = SaitexBL.Interface.Method.BATCH_CARD_MST.GetProcessRouteByPaNo(oBATCH_CARD_MST);

            ViewState["dtBatchTrn"] = dtBatchTrn;
        }
        catch
        {
            throw;
        }
    }
    protected void ddlProcessCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetProsCodeData(e.Text.ToUpper(), e.ItemsOffset);

            ddlProcessCode.Items.Clear();

            ddlProcessCode.DataSource = data;
            ddlProcessCode.DataTextField = "PROS_CODE";
            ddlProcessCode.DataValueField = "PROS_DATA";
            ddlProcessCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetProsCodeCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading process.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetProsCodeData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT *FROM (SELECT *FROM (SELECT DEPARTMENT as DEPT_CODE, DEPT_NAME, MAIN_PROCESS, PROS_CODE, PROS_DESC,MAC_GRUP_CODE as MAC_GROUP_CODE,MAC_CODE as MACHINE_CODE, ( DEPARTMENT|| '@'|| DEPT_NAME|| '@'|| MAIN_PROCESS|| '@'|| PROS_CODE|| '@'|| PROS_DESC|| '@'|| MAC_GRUP_CODE|| '@'|| MAC_CODE)PROS_DATA FROM V_TX_MAC_PROC_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PROS_CODE) WHERE PROS_CODE LIKE :SearchQuery OR DEPT_NAME LIKE :SearchQuery OR MAIN_PROCESS LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and PROS_DATA not in  ( SELECT PROS_DATA FROM (SELECT *FROM (SELECT DEPARTMENT as DEPT_CODE, DEPT_NAME, MAIN_PROCESS, PROS_CODE, PROS_DESC,MAC_GRUP_CODE as MAC_GROUP_CODE,MAC_CODE as MACHINE_CODE, ( DEPARTMENT|| '@'|| DEPT_NAME|| '@'|| MAIN_PROCESS|| '@'|| PROS_CODE|| '@'|| PROS_DESC|| '@'|| MAC_GRUP_CODE|| '@'|| MAC_CODE)PROS_DATA FROM V_TX_MAC_PROC_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PROS_CODE) WHERE PROS_CODE LIKE :SearchQuery OR DEPT_NAME LIKE :SearchQuery OR MAIN_PROCESS LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by PROS_CODE";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetProsCodeCount(string text)
    {
        try
        {
            string CommandText = " SELECT *FROM (SELECT *FROM (SELECT DEPARTMENT as DEPT_CODE, DEPT_NAME, MAIN_PROCESS, PROS_CODE, PROS_DESC,MAC_GRUP_CODE as MAC_GROUP_CODE,MAC_CODE as MACHINE_CODE, ( DEPARTMENT|| '@'|| DEPT_NAME|| '@'|| MAIN_PROCESS|| '@'|| PROS_CODE|| '@'|| PROS_DESC|| '@'|| MAC_GRUP_CODE|| '@'|| MAC_CODE)PROS_DATA FROM V_TX_MAC_PROC_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY PROS_CODE) WHERE PROS_CODE LIKE :SearchQuery OR DEPT_NAME LIKE :SearchQuery OR MAIN_PROCESS LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlProcessCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ddlPaNo.SelectedIndex >= 0 && ddlGreyLot.SelectedIndex >= 0)
            {
                string cString = ddlProcessCode.SelectedValue.ToString();
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
                string PROS_CODE = arrString[3].ToString();
                txtProcess.Text = PROS_CODE;

                DataTable dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetProcessData(PROS_CODE);
                if (dt.Rows.Count > 0)
                {
                    MapDataTable(dt);
                    BindGridFromDataTable();
                }
                //RefreshDetailRow();
            }
            else
            {
                grdProcessTrn.DataSource = null;
                grdProcessTrn.DataBind();
                CommonFuction.ShowMessage("Please select PA No And Machine");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Process.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void deleteItemReceiptRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UniqueId"].ToString());
                    if (iUNIQUEID == UniqueId)
                    {
                        dtDetailTBL.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }
    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
            {
                CreateDataTable(); ;
            }
            else
            {
                dtDetailTBL.Rows.Clear();
            }
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtDetailTBL.NewRow();
                    dr["UniqueId"] = dtDetailTBL.Rows.Count + 1;
                    dr["PARA_BASIS"] = drTemp["PARA_BASIS"].ToString().Trim();
                    dr["DYE_PROCESS"] = drTemp["DYE_PROCESS"].ToString().Trim();
                    dr["ITEM_CODE"] = drTemp["ITEM_CODE"].ToString().Trim();
                    dr["ITEM_DESC"] = drTemp["ITEM_DESC"].ToString().Trim();
                    dr["QTY"] = drTemp["QTY"].ToString().Trim();
                    dr["UOM_OF_UNIT"] = drTemp["UOM"].ToString().Trim();
                    dr["LOT_SIZE"] = txtLotSize.Text.Trim();
                    dr["MACHINE_CAPACITY"] = txtMachineCap.Text.Trim();
                    dr["TRN_QTY"] = 0;
                    dr["REMARKS"] = drTemp["REMARKS"].ToString().Trim();
                    dr["TEMP"] = drTemp["TEMP"].ToString().Trim();
                    dr["HOLD_TIME"] = drTemp["HOLD_TIME"].ToString().Trim();
                    dr["SR_NO"] = drTemp["SR_NO"].ToString().Trim();
                    dtDetailTBL.Rows.Add(dr);
                }
                dtTemp = null;
                ViewState["dtDetailTBL"] = dtDetailTBL;
                grdProcessTrn.DataSource = dtDetailTBL;
                grdProcessTrn.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void grdProcessTrn_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "JOBCreditDelete")
            {
                deleteItemReceiptRow(UniqueId);
                BindGridFromDataTable();
            }
            if (e.CommandName == "JOBCreditEdit")
            {
                FillDetailByGrid(UniqueId);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row updfation / deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                ddlChemicalBasis.SelectedValue = dv[0]["PARA_BASIS"].ToString();
                ddlDyeProcessCode.SelectedValue = dv[0]["DYE_PROCESS"].ToString();
                txtItemCode.SelectedValue = dv[0]["ITEM_CODE"].ToString();
                txtICode.Text = dv[0]["ITEM_CODE"].ToString();
                txtProcessDesc.Text = dv[0]["ITEM_DESC"].ToString();
                txtChemicalQuantity.Text = dv[0]["QTY"].ToString();
                ddlChemicalunit.SelectedValue = dv[0]["UOM_OF_UNIT"].ToString();
                txtItemQtykg.Text = dv[0]["LOT_SIZE"].ToString();
                txtiMachineVolum.Text = dv[0]["MACHINE_CAPACITY"].ToString();
                txtiItemQty.Text = dv[0]["TRN_QTY"].ToString();
                txtAditionQty.Text = dv[0]["ADD_TRN_QTY"].ToString();
                txtAdition2Qty.Text = dv[0]["ADD2_TRN_QTY"].ToString();
                txtTemp.Text = dv[0]["TEMP"].ToString();
                txtHoldTemp.Text = dv[0]["HOLD_TIME"].ToString();
                txtDyeRemarks.Text = dv[0]["REMARKS"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;

        }
        catch
        {
            throw;
        }
    }

    private bool SearchValidateProsindtTrn()
    {
        try
        {
            bool bResult = false;
            dtBatchTrn = (List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>)ViewState["dtBatchTrn"];

            var oVar = (from data in dtBatchTrn
                        where data.PROS_CODE == ddlProcessCode.SelectedText //txtProsCode.Text
                        orderby data.SR_NO
                        select data).ToList();

            if (oVar.Count > 0)
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

    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();
            if (txtICode.Text != "" && txtChemicalQuantity.Text != " ")
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                bool bb = SearchItemCodeInGrid(txtICode.Text, UniqueId, ddlDyeProcessCode.SelectedValue, ddlChemicalBasis.SelectedValue, ddlChemicalunit.SelectedValue);
                if (!bb)
                {
                    double Qty = 0;
                    double.TryParse(txtChemicalQuantity.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {
                        if (UniqueId > 0)
                        {
                            DataView dv = new DataView(dtDetailTBL);
                            dv.RowFilter = "UniqueId=" + UniqueId;
                            if (dv.Count > 0)
                            {
                                dv[0]["PARA_BASIS"] = ddlChemicalBasis.SelectedItem.ToString();
                                dv[0]["DYE_PROCESS"] = ddlDyeProcessCode.SelectedItem.ToString();
                                dv[0]["ITEM_CODE"] = txtICode.Text;
                                dv[0]["ITEM_DESC"] = txtProcessDesc.Text.Trim();
                                dv[0]["QTY"] = double.Parse(txtChemicalQuantity.Text.Trim());
                                dv[0]["UOM_OF_UNIT"] = ddlChemicalunit.SelectedItem.ToString();
                                dv[0]["LOT_SIZE"] = txtItemQtykg.Text.Trim();
                                dv[0]["MACHINE_CAPACITY"] = txtiMachineVolum.Text.Trim();
                                dv[0]["TRN_QTY"] = txtiItemQty.Text.Trim();
                                dv[0]["ADD_TRN_QTY"] = txtAditionQty.Text.Trim();
                                dv[0]["ADD2_TRN_QTY"] = txtAdition2Qty.Text.Trim();
                                dv[0]["TEMP"] = txtTemp.Text.Trim();
                                dv[0]["HOLD_TIME"] = txtHoldTemp.Text.Trim();
                                dv[0]["REMARKS"] = txtDyeRemarks.Text.ToString();
                                dv[0]["SR_NO"] = UniqueId;
                                dtDetailTBL.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtDetailTBL.NewRow();
                            dr["UniqueId"] = dtDetailTBL.Rows.Count + 1;
                            dr["PARA_BASIS"] = ddlChemicalBasis.SelectedItem.ToString();
                            dr["DYE_PROCESS"] = ddlDyeProcessCode.SelectedItem.ToString();
                            dr["ITEM_CODE"] = txtICode.Text;
                            dr["ITEM_DESC"] = txtProcessDesc.Text.Trim();
                            dr["QTY"] = double.Parse(txtChemicalQuantity.Text.Trim());
                            dr["UOM_OF_UNIT"] = ddlChemicalunit.SelectedItem.ToString();
                            dr["LOT_SIZE"] = txtItemQtykg.Text.Trim();
                            dr["MACHINE_CAPACITY"] = txtiMachineVolum.Text.Trim();
                            dr["TRN_QTY"] = txtiItemQty.Text.Trim();
                            dr["ADD_TRN_QTY"] = txtAditionQty.Text.Trim();
                            dr["ADD2_TRN_QTY"] = txtAdition2Qty.Text.Trim();
                            dr["TEMP"] = txtTemp.Text.Trim();
                            dr["HOLD_TIME"] = txtHoldTemp.Text.Trim();
                            dr["REMARKS"] = txtDyeRemarks.Text.ToString();
                            dr["SR_NO"] = dtDetailTBL.Rows.Count + 1;
                            dtDetailTBL.Rows.Add(dr);
                        }
                        ViewState["dtDetailTBL"] = dtDetailTBL;
                        RefreshRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
                }
            }

            grdProcessTrn.DataSource = dtDetailTBL;
            grdProcessTrn.DataBind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item Detail Row.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId, string DYE_PROCESS, string BASIS, string UOM)
    {
        bool Result = false;
        try
        {
            if (grdProcessTrn.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdProcessTrn.Rows)
                {
                    Label txtItemCode1 = (Label)grdRow.FindControl("txtICODE");
                    Label TxtDyeProcess = (Label)grdRow.FindControl("txtDYE_PROCESS");
                    Label lblBasic = (Label)grdRow.FindControl("lblBasic");
                    Label txtUNIT = (Label)grdRow.FindControl("txtUNIT");
                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUniqueId = int.Parse(lnkEdit.CommandArgument.Trim());

                    if (txtItemCode1.Text.Trim() == ItemCode && TxtDyeProcess.Text.Trim() == DYE_PROCESS && lblBasic.Text.Trim() == BASIS && UniqueId != iUniqueId && txtUNIT.Text.Trim() == UOM)
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
    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        try
        {
            RefreshRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refreshing Item Detail ROw.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (!_isRefresh)
            {
                string msg = "";
                if (ValidateData(out msg))
                {
                    SaitexBL.Common.CommonFuction.SaveState oSaveState = SaitexBL.Common.CommonFuction.SaveState.Save;
                    SaveData(oSaveState);
                }
                else
                {
                    Common.CommonFuction.ShowMessage(msg);
                }
            }
            else
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Batch Entry.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool ValidateData(out string msg)
    {
        try
        {
            msg = "";
            int iCount = 0;
            int iCountAll = 0;
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (ViewState["dtLotDetail"] != null)
                dtLotDetail = (DataTable)ViewState["dtLotDetail"];
            iCountAll += 1;
            if (txtPaNo.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += @"Invalid PA No.\r\n";
            }
            iCountAll += 1;
            if (txtSpring.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += @"No Of Cheese Required \r\n";
            }
            iCountAll += 1;
            if (txtCustReqNo.Text != string.Empty)
            {
                iCount += 1;
            }
            else
            {
                msg += @"Invalid Lot No.\r\n";
            }
            iCountAll += 1;
            if (dtDetailTBL.Rows.Count > 0)
            {
                iCount += 1;
            }
            else
            {
                msg += @"Invalid Process Detail No.\r\n";
            }



            if (iCount == iCountAll)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            throw;
        }
    }

    private void SaveData(SaitexBL.Common.CommonFuction.SaveState savestate)
    {
        try
        {
            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.BATCH_CODE = Convert.ToInt32(txtBatchCode.Text);
            oBATCH_CARD_MST.BATCH_DATE = DateTime.Parse(txtBatchDate.Text);
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oBATCH_CARD_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oBATCH_CARD_MST.CUST_REQ_NO = txtCustReqNo.Text;

            oBATCH_CARD_MST.GREY_LOT_NO = txtGreyLotNo.Text;
            oBATCH_CARD_MST.LAB_DIP_NO = txtLabDipNo.Text;
            oBATCH_CARD_MST.LR_OPTION = txtOption.Text;
            oBATCH_CARD_MST.TRN_NUMB = Convert.ToInt32(txtTRNno.Text);


            oBATCH_CARD_MST.ORDER_NO = txtOrderNo.Text;
            oBATCH_CARD_MST.PA_NO = txtPaNo.Text;

            oBATCH_CARD_MST.CONF_FLAG = true;
            oBATCH_CARD_MST.COMP_FLAG = false;

            oBATCH_CARD_MST.MACHINE_CODE = txtMachineCode.Text.Trim();
            oBATCH_CARD_MST.MACHINE_MAKE = txtMachineName.Text.Trim();
            oBATCH_CARD_MST.MACHINE_VOLUMN = Convert.ToInt32(txtMachineCap.Text.Trim());
            oBATCH_CARD_MST.NO_OF_SPINDLES = Convert.ToInt32(txtMachineSpindle.Text.Trim().ToString());
            oBATCH_CARD_MST.PROS_CODE = txtProcess.Text.Trim();
            oBATCH_CARD_MST.SPRINGS = Convert.ToInt32(txtSpring.Text.Trim());
            oBATCH_CARD_MST.REMARKS = txtRemarks.Text;
            oBATCH_CARD_MST.TDATE = DateTime.Now;
            oBATCH_CARD_MST.TUSER = oUserLoginDetail.UserCode;
            oBATCH_CARD_MST.LOT_SIZE = Convert.ToDouble(txtLotSize.Text.Trim());
            oBATCH_CARD_MST.PRTY_CODE = txtParty.Text.ToString();
            oBATCH_CARD_MST.ARTICLE_CODE = txtArticle.Text.ToString();
            oBATCH_CARD_MST.SHADE_CODE = txtShade.Text.ToString();

            oBATCH_CARD_MST.ISSUE_NO = txtIssueNo.Text.ToString();
            oBATCH_CARD_MST.IUUSE_DATE = DateTime.Parse(txtIssueDate.Text);
            oBATCH_CARD_MST.TROLLY_NO = ddlTrolyNo.Text.Trim().ToString();
            oBATCH_CARD_MST.TINT = ddlTint.SelectedValue;

            // Code For Chemical ......................
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            // Code For Dyes .................
            if (ViewState["dtLotDetail"] != null)
                dtLotDetail = (DataTable)ViewState["dtLotDetail"];


            int BATCH_CODE = 0;
            bool bResult = SaitexBL.Interface.Method.BATCH_CARD_MST.InsertBatchCardEntry(oBATCH_CARD_MST, dtDetailTBL, dtLotDetail, out BATCH_CODE, savestate);
            if (bResult)
            {
                Common.CommonFuction.ShowMessage(@"Batch Card Entry Saved Successfully.\r\nYour Batch Card No is " + BATCH_CODE);
                InitialisePage();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Batch Card Entry saving failed.");
            }
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
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            lblMode.Text = "Update";
            ddlBatchCode.Visible = true;
            txtBatchCode.Visible = false;

            ddlProcessCode.Enabled = false;
            ddlPaNo.Enabled = false;
            ddlGreyLot.Enabled = false;
            txtProcess.Visible = true;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding Batch Entry.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlBatchCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetBatchCodeData(e.Text.ToUpper(), e.ItemsOffset);

            ddlBatchCode.Items.Clear();

            ddlBatchCode.DataSource = data;
            ddlBatchCode.DataTextField = "BATCH_CODE";
            ddlBatchCode.DataValueField = "BATCH_CODE";
            ddlBatchCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetBatchCodeCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading process.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetBatchCodeData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, GREY_LOT_NO FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND NVL(R.CONF_FLAG,0)=1 AND NVL(R.COMP_FLAG,0)=0  AND R.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + " ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR GREY_LOT_NO LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND BATCH_CODE NOT IN (SELECT BATCH_CODE FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, GREY_LOT_NO FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND NVL(A.CONF_FLAG,0)=1 AND NVL(R.COMP_FLAG,0)=0  AND a.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + " ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR GREY_LOT_NO LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by BATCH_CODE";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetBatchCodeCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, GREY_LOT_NO FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND NVL(R.CONF_FLAG,0)=1 AND AND NVL(R.COMP_FLAG,0)=0  R.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + " ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR GREY_LOT_NO LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBatchCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {

            int BATCH_NUMBER = int.Parse(ddlBatchCode.SelectedValue.Trim());
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            else
            {
                CreateDataTable();
            }

            int iRecordFound = GetdataByBatchNumber(BATCH_NUMBER);
            BindGridFromDataTable();
            BindGridFromLotDataTable();
            if (iRecordFound > 0)
            {

            }
            else
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void MapDataTable()
    {
        try
        {

            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            else
            {
                CreateDataTable();
            }
            if (!dtDetailTBL.Columns.Contains("UniqueId"))
                dtDetailTBL.Columns.Add("UniqueId", typeof(int));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UniqueId"] = iLoop + 1;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MapDataTableLot()
    {
        try
        {
            if (ViewState["dtLotDetail"] != null)
            {
                dtLotDetail = (DataTable)ViewState["dtLotDetail"];
            }
            else
            {
                CreateLotDataTable();
            }
            if (!dtLotDetail.Columns.Contains("UniqueId"))
                dtLotDetail.Columns.Add("UniqueId", typeof(int));

            for (int iLoop = 0; iLoop < dtLotDetail.Rows.Count; iLoop++)
            {
                dtLotDetail.Rows[iLoop]["UniqueId"] = iLoop + 1;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private int GetdataByBatchNumber(int BatchNumber)
    {
        int iRecordFound = 0;
        try
        {

            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            int BatchCode = Convert.ToInt32(ddlBatchCode.SelectedText.Trim());
            oBATCH_CARD_MST.BATCH_CODE = BatchCode;
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetBatchEntryDataByBatchCodeDye(oBATCH_CARD_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                ddlBatchCode.Visible = false;
                txtBatchCode.Visible = true;

                txtArticle.Text = dt.Rows[0]["ARTICLE_CODE"].ToString();
                txtArticleDesc.Text = dt.Rows[0]["ARTICLE_DESC"].ToString();
                txtBatchCode.Text = dt.Rows[0]["BATCH_CODE"].ToString();
                txtBatchDate.Text = DateTime.Parse(dt.Rows[0]["BATCH_DATE"].ToString().Trim()).ToShortDateString();
                txtCustReqNo.Text = dt.Rows[0]["CUST_REQ_NO"].ToString();
                txtLotSize.Text = dt.Rows[0]["LOT_SIZE"].ToString().Trim();
                txtOrderNo.Text = dt.Rows[0]["ORDER_NO"].ToString();
                txtIssueNo.Text = dt.Rows[0]["ISSUE_NO"].ToString();
                txtIssueDate.Text = DateTime.Parse(dt.Rows[0]["ISSUE_DATE"].ToString().Trim()).ToShortDateString();
                ddlTrolyNo.Text = dt.Rows[0]["TROLLY_NO"].ToString();
                ddlTint.SelectedIndex = ddlTint.Items.IndexOf(ddlTint.Items.FindByValue(dt.Rows[0]["TINT"].ToString().Trim()));
                ddlProcessCode.SelectedValue = dt.Rows[0]["PROS_CODE"].ToString().Trim();
                txtGreyLotNo.Text = dt.Rows[0]["GREY_LOT_NO"].ToString();
                txtTRNno.Text = dt.Rows[0]["TRN_NUMB"].ToString();
                txtLabDipNo.Text = dt.Rows[0]["LAB_DIP_NO"].ToString();
                txtOption.Text = dt.Rows[0]["LR_OPTION"].ToString();
                txtPaNo.Text = dt.Rows[0]["PA_NO"].ToString();
                txtParty.Text = dt.Rows[0]["PRTY_CODE"].ToString();
                txtPartDtl.Text = dt.Rows[0]["PRTY_NAME"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                txtShade.Text = dt.Rows[0]["SHADE_CODE"].ToString();
                txtMachineCode.Text = dt.Rows[0]["MACHINE_CODE"].ToString();
                txtMachineName.Text = dt.Rows[0]["MACHINE_MAKE"].ToString();
                txtMachineCap.Text = dt.Rows[0]["MACHINE_CAPACITY"].ToString().Trim();
                txtSpring.Text = dt.Rows[0]["SPRINGS"].ToString().Trim();
                txtMachineSpindle.Text = dt.Rows[0]["NO_OF_SPINDLES"].ToString().Trim();
                txtProcess.Text = dt.Rows[0]["PROS_CODE"].ToString();
            }
            if (iRecordFound == 1)
            {
                dtDetailTBL = SaitexBL.Interface.Method.BATCH_CARD_MST.GetBatchEntryTRNByBatchCodeDye(oBATCH_CARD_MST);
                if (dtDetailTBL.Rows.Count > 0)
                {
                    ViewState["dtDetailTBL"] = dtDetailTBL;
                    MapDataTable();

                    if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
                    {
                        dtLotDetail = SaitexBL.Interface.Method.BATCH_CARD_MST.GetLotDetailsDyes(oBATCH_CARD_MST);
                        ViewState["dtLotDetail"] = dtLotDetail;
                        MapDataTableLot();
                    }
                }

            }
            else
            {
                InitialisePage();

                txtBatchCode.Text = "";
                ddlBatchCode.Focus();
                lblMode.Text = "Update";
                RefreshDetailRow();
            }
            return iRecordFound;
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
            if (!_isRefresh)
            {
                string msg = "";
                if (ValidateData(out msg))
                {
                    SaitexBL.Common.CommonFuction.SaveState oSaveState = SaitexBL.Common.CommonFuction.SaveState.Update;
                    SaveData(oSaveState);
                }
                else
                {
                    Common.CommonFuction.ShowMessage(msg);
                }
            }
            else
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Batch Entry.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            Response.Redirect("~/Module/OrderDevelopment/Reports/PrintJobSheet.aspx");
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing Batch Card.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refreshing Page.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void ddlGreyLot_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetGreyLotData(e.Text.ToUpper(), e.ItemsOffset);
            ddlGreyLot.Items.Clear();
            ddlGreyLot.DataSource = data;
            ddlGreyLot.DataTextField = "LAB_DIP_NO";
            ddlGreyLot.DataValueField = "LOT_DATA";
            ddlGreyLot.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetGreyLotCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetGreyLotData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (SELECT   *   FROM   (  SELECT   LAB_DIP_NO,  LR_OPTION, SHADE_CODE,  SHADE_GROUP, GREY_LOT_NO, ARTICAL_NO,  ( SHADE_CODE || '@'||  LAB_DIP_NO     || '@'  || LR_OPTION || '@' || GREY_LOT_NO)  LOT_DATA  FROM   V_ST_LABDIP_SUB_MST A  WHERE A.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND A.SHADE_CODE = '" + txtShade.Text + "'   ORDER BY LAB_DIP_NO)WHERE      LAB_DIP_NO LIKE :SearchQuery  OR LR_OPTION LIKE :SearchQuery OR GREY_LOT_NO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and LOT_DATA not in(SELECT   *  FROM   (SELECT   *   FROM   (  SELECT  LAB_DIP_NO,  LR_OPTION, SHADE_CODE,  SHADE_GROUP, GREY_LOT_NO, ARTICAL_NO,  ( SHADE_CODE || '@'||  LAB_DIP_NO     || '@'  || LR_OPTION || '@' || GREY_LOT_NO)  LOT_DATA  FROM   V_ST_LABDIP_SUB_MST A  WHERE A.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND A.SHADE_CODE = '" + txtShade.Text + "' ORDER BY LAB_DIP_NO) WHERE      LAB_DIP_NO LIKE :SearchQuery  OR LR_OPTION LIKE :SearchQuery OR GREY_LOT_NO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " order by LAB_DIP_NO";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected int GetGreyLotCount(string text)
    {
        try
        {
            string CommandText = " SELECT   *  FROM   (SELECT   *   FROM   (  SELECT   LAB_DIP_NO,  LR_OPTION, SHADE_CODE,  SHADE_GROUP, GREY_LOT_NO, ARTICAL_NO,  (SHADE_CODE || '@'||   LAB_DIP_NO     || '@'  || LR_OPTION || '@' || GREY_LOT_NO)  LOT_DATA  FROM   V_ST_LABDIP_SUB_MST A  WHERE A.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND A.SHADE_CODE = '" + txtShade.Text + "'   ORDER BY LAB_DIP_NO)WHERE      LAB_DIP_NO LIKE :SearchQuery  OR LR_OPTION LIKE :SearchQuery OR GREY_LOT_NO LIKE :SearchQuery   OR SHADE_CODE LIKE :SearchQuery)";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlGreyLot_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ddlPaNo.SelectedIndex >= 0)
            {
                string cString = ddlGreyLot.SelectedValue.ToString();
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
                string SHADE_CODE = arrString[0].ToString();
                string LAB_DIP_NO = arrString[1].ToString();
                string LR_OPTION = arrString[2].ToString();
                string GREY_LOT_NO = arrString[3].ToString();
                txtShade.Text = SHADE_CODE;
                txtGreyLotNo.Text = GREY_LOT_NO;
                txtLabDipNo.Text = LAB_DIP_NO;
                txtOption.Text = LR_OPTION;

                DataTable dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetLoadLotData(SHADE_CODE, LAB_DIP_NO, GREY_LOT_NO);
                if (dt.Rows.Count > 0)
                {
                    MapDataTableGreyLot(dt);
                    BindGridFromLotDataTable();
                }

            }
            else
            {
                grdProcessTrn.DataSource = null;
                grdProcessTrn.DataBind();
                CommonFuction.ShowMessage("Please select PA No And Machine");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Process.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void MapDataTableGreyLot(DataTable dtTemp)
    {
        try
        {
            if (dtLotDetail == null || dtLotDetail.Rows.Count == 0)
            {
                CreateLotDataTable(); ;
            }
            else
            {
                dtLotDetail.Rows.Clear();
            }
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtLotDetail.NewRow();
                    dr["SR_NO"] = dtLotDetail.Rows.Count + 1;
                    dr["LAB_DIP_NO"] = drTemp["LAB_DIP_NO"].ToString().Trim();
                    dr["LR_OPTION"] = drTemp["LR_OPTION"].ToString().Trim();
                    dr["SHADE_GROUP"] = drTemp["SHADE_GROUP"].ToString().Trim();
                    dr["GREY_LOT_NO"] = drTemp["GREY_LOT_NO"].ToString().Trim();
                    dr["ARTICAL_NO"] = drTemp["ARTICAL_NO"].ToString().Trim();
                    dr["SHADE_FAMILY_NAME"] = drTemp["SHADE_FAMILY_NAME"].ToString().Trim();
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"].ToString().Trim();
                    dr["LOT_SIZE"] = txtLotSize.Text.Trim();
                    dr["DYE_NAME"] = drTemp["DYE_NAME"].ToString().Trim();
                    dr["DYE_DTL"] = drTemp["DYE_DTL"].ToString().Trim();
                    dr["RECIPE_COST"] = drTemp["RECIPE_COST"].ToString().Trim();
                    dr["DOSE"] = drTemp["DOSE"].ToString().Trim();
                    dr["RATE"] = drTemp["RATE"].ToString().Trim();
                    dr["TRN_QTY"] = 0;
                    dr["TRN_QTY_KG"] = 0;
                    dr["TRN_QTY_GM"] = 0;
                    dr["TRN_QTY_MGM"] = 0;
                    dtLotDetail.Rows.Add(dr);
                }
                dtTemp = null;
                ViewState["dtLotDetail"] = dtLotDetail;
                grdDyesTrn.DataSource = dtLotDetail;
                grdDyesTrn.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    private void CreateLotDataTable()
    {
        try
        {
            dtLotDetail = new DataTable();
            dtLotDetail.Columns.Add("UniqueId", typeof(int));
            dtLotDetail.Columns.Add("LAB_DIP_NO", typeof(string));
            dtLotDetail.Columns.Add("LR_OPTION", typeof(string));
            dtLotDetail.Columns.Add("SHADE_GROUP", typeof(string));
            dtLotDetail.Columns.Add("GREY_LOT_NO", typeof(string));
            dtLotDetail.Columns.Add("ARTICAL_NO", typeof(string));
            dtLotDetail.Columns.Add("SHADE_FAMILY_NAME", typeof(string));
            dtLotDetail.Columns.Add("SHADE_CODE", typeof(string));
            dtLotDetail.Columns.Add("LOT_SIZE", typeof(double));
            dtLotDetail.Columns.Add("DYE_NAME", typeof(string));
            dtLotDetail.Columns.Add("DYE_DTL", typeof(string));
            dtLotDetail.Columns.Add("RECIPE_COST", typeof(double));
            dtLotDetail.Columns.Add("DOSE", typeof(double));
            dtLotDetail.Columns.Add("RATE", typeof(double));
            dtLotDetail.Columns.Add("TRN_QTY", typeof(double));
            dtLotDetail.Columns.Add("TRN_QTY_KG", typeof(string));
            dtLotDetail.Columns.Add("TRN_QTY_GM", typeof(string));
            dtLotDetail.Columns.Add("TRN_QTY_MGM", typeof(string));
            dtLotDetail.Columns.Add("ADD_QTY_KG", typeof(string));
            dtLotDetail.Columns.Add("ADD_QTY_GM", typeof(string));
            dtLotDetail.Columns.Add("ADD_QTY_MGM", typeof(string));
            dtLotDetail.Columns.Add("ADD2_QTY_KG", typeof(string));
            dtLotDetail.Columns.Add("ADD2_QTY_GM", typeof(string));
            dtLotDetail.Columns.Add("ADD2_QTY_MGM", typeof(string));
            dtLotDetail.Columns.Add("ISS_QTY", typeof(double));
            dtLotDetail.Columns.Add("SR_NO", typeof(int));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindGridFromLotDataTable()
    {
        try
        {
            if (ViewState["dtLotDetail"] != null)
                dtLotDetail = (DataTable)ViewState["dtLotDetail"];
            if (dtLotDetail == null)
                CreateLotDataTable();
            double Does = 0, LotQty = 0, DyesWT = 0;
            string ABC, DyesKG, DyesGM, DyesMGM;
            if (grdDyesTrn.Rows.Count > 0)
            {
                foreach (GridViewRow gvLotrow in grdDyesTrn.Rows)
                {
                    Label lblSr_No = (Label)gvLotrow.FindControl("lblSr1");
                    Label lblLabDipNo = (Label)gvLotrow.FindControl("lblLabDipNo");
                    Label lblLrOption = (Label)gvLotrow.FindControl("lblLrOption");
                    Label lblGreyLotNo = (Label)gvLotrow.FindControl("lblGreyLotNo");
                    Label lblShadeCode = (Label)gvLotrow.FindControl("lblShadeCode");
                    TextBox txtLotQty = (TextBox)gvLotrow.FindControl("txtLotQty");
                    TextBox txtDyesQTY = (TextBox)gvLotrow.FindControl("txtDyesQTY");
                    TextBox txtDyesWT = (TextBox)gvLotrow.FindControl("txtDyesWT");
                    TextBox txtDyesKG = (TextBox)gvLotrow.FindControl("txtDyesKG");
                    TextBox txtDyesGM = (TextBox)gvLotrow.FindControl("txtDyesGM");
                    TextBox txtDyesMGM = (TextBox)gvLotrow.FindControl("txtDyesMGM");
                    //Add Addon open  Process Dyes entry By Arun  Sharma 
                    TextBox txtAdition1DyesKG = (TextBox)gvLotrow.FindControl("txtAdition1DyesKG");
                    TextBox txtAdition1DyesGM = (TextBox)gvLotrow.FindControl("txtAdition1DyesGM");
                    TextBox txtAdition1DyesMGM = (TextBox)gvLotrow.FindControl("txtAdition1DyesMGM");
                    TextBox txtAdition2DyesKG = (TextBox)gvLotrow.FindControl("txtAdition2DyesKG");
                    TextBox txtAdition2DyesGM = (TextBox)gvLotrow.FindControl("txtAdition2DyesGM");
                    TextBox txtAdition2DyesMGM = (TextBox)gvLotrow.FindControl("txtAdition2DyesMGM");
                    //Add Addon close  Process Dyes entry By Arun  Sharma 
                    Label txtDYENAME = (Label)gvLotrow.FindControl("txtDYENAME");

                    double.TryParse(txtLotQty.Text.Trim(), out LotQty);
                    double.TryParse(txtDyesQTY.Text.Trim(), out Does);

                    string Add1DyesKG = txtAdition1DyesKG.Text.Trim();
                    string Add1DyesGM = txtAdition1DyesGM.Text.Trim();
                    string Add1DyesMGM = txtAdition1DyesMGM.Text.Trim();
                    string Add2DyesKG = txtAdition2DyesKG.Text.Trim();
                    string Add2DyesGM = txtAdition2DyesGM.Text.Trim();
                    string Add2DyesMGM = txtAdition2DyesMGM.Text.Trim();


                    DyesWT = Does * LotQty / 100;
                    txtDyesQTY.Text = DyesWT.ToString();
                    {
                        // spit value by Arun Sharma
                        string value = txtDyesQTY.Text;
                        var values = value.ToString().Split('.');
                        string firstValue = "";
                        string secondValue = "";
                        if (values.Length == 2)
                        {
                            firstValue = (values[0]);
                            secondValue = (values[1]);
                        }
                        else
                        {
                            firstValue = (values[0]);
                        }
                        DyesKG = firstValue;
                        ABC = secondValue;
                        {
                            DyesGM = ABC.ToString().Substring(0, 3);
                            DyesMGM = ABC.ToString().Substring(3);
                        }
                        txtDyesKG.Text = DyesKG.ToString();
                        txtDyesGM.Text = DyesGM.ToString();
                        txtDyesMGM.Text = DyesMGM.ToString();
                    }
                    if (dtLotDetail.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtLotDetail.Rows)
                        {
                            if (dr["GREY_LOT_NO"].ToString() == lblGreyLotNo.Text && dr["DYE_NAME"].ToString() == txtDYENAME.Text && dr["SHADE_CODE"].ToString() == lblShadeCode.Text)
                            {
                                dr["DOSE"] = Does;
                                dr["LOT_SIZE"] = LotQty;
                                dr["TRN_QTY"] = txtDyesQTY.Text;
                                dr["TRN_QTY_KG"] = txtDyesKG.Text;
                                dr["TRN_QTY_GM"] = txtDyesGM.Text;
                                dr["TRN_QTY_MGM"] = txtDyesMGM.Text;
                                dr["ADD_QTY_KG"] = Add1DyesKG;
                                dr["ADD_QTY_GM"] = Add1DyesGM;
                                dr["ADD_QTY_MGM"] = Add1DyesMGM;
                                dr["ADD2_QTY_KG"] = Add2DyesKG;
                                dr["ADD2_QTY_GM"] = Add2DyesGM;
                                dr["ADD2_QTY_MGM"] = Add2DyesMGM;

                                txtAdition1DyesKG.Text = "";
                                txtAdition1DyesGM.Text = "";
                                txtAdition1DyesMGM.Text = "";
                                txtAdition2DyesKG.Text = "";
                                txtAdition2DyesGM.Text = "";
                                txtAdition2DyesMGM.Text = "";

                                dtLotDetail.AcceptChanges();
                                break;
                            }
                        }
                    }
                }
            }
            grdDyesTrn.DataSource = dtLotDetail;
            grdDyesTrn.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Process.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    // for addon proces code 
    protected void txtAdition1DyesKG_TextChanged(object sender, EventArgs e)
    {
        BindGridFromLotDataTable();
    }
    protected void txtAdition1DyesGM_TextChanged(object sender, EventArgs e)
    {
        BindGridFromLotDataTable();
    }
    protected void txtAdition1DyesMGM_TextChanged(object sender, EventArgs e)
    {
        BindGridFromLotDataTable();
    }
    protected void txtAdition2DyesKG_TextChanged(object sender, EventArgs e)
    {
        BindGridFromLotDataTable();
    }
    protected void txtAdition2DyesGM_TextChanged(object sender, EventArgs e)
    {
        BindGridFromLotDataTable();
    }
    protected void txtAdition2DyesMGM_TextChanged(object sender, EventArgs e)
    {
        BindGridFromLotDataTable();
    }
    
    protected void grdDyesTrn_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "JOBCreditDelete")
            {
                deleteItemReceiptRow(UniqueId);
                BindGridFromLotDataTable();
            }
            if (e.CommandName == "JOBCreditEdit")
            {
                FillDetailByGrid(UniqueId);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row updfation / deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdProcesTrn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void txtItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLItemCode(e.Text.ToUpper(), e.ItemsOffset);
            txtItemCode.Items.Clear();
            txtItemCode.DataSource = data;
            txtItemCode.DataTextField = "ITEM_CODE";
            txtItemCode.DataValueField = "ITEM_DESC";
            txtItemCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetLItemCode(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT i.ITEM_CODE,i.ITEM_TYPE, i.ITEM_DESC, i.ITEM_MAKE,i.OP_BAL_STOCK, i.OP_RATE, i.MIN_STOCK_LVL, i.REODR_QTY,i.REODR_LVL, i.MIN_PROCURE_DAYS, i.EXPIRY_DAYS, i.QC_REQUIRED, i.ITEM_STATUS, i.ITEM_REMARKS, i.UOM,i.ASOC_ITEM_CODE, i.DEPT_CODE, i.BRANCH_CODE, i.RACK_CODE, i.CAT_CODE,d.DEPT_NAME, b.BRANCH_NAME FROM TX_ITEM_MST i, CM_DEPT_MST d, CM_BRANCH_MST b WHERE i.BRANCH_CODE = b.BRANCH_CODE AND i.DEPT_CODE = d.DEPT_CODE AND I.CAT_CODE IN ('DYE', 'CHEMICALS', 'CHEMICAL') ORDER BY ITEM_CODE ) WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery)";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_CODE not in(SELECT * FROM   (SELECT *  FROM (SELECT   i.ITEM_CODE,i.ITEM_TYPE, i.ITEM_DESC, i.ITEM_MAKE,i.OP_BAL_STOCK, i.OP_RATE, i.MIN_STOCK_LVL, i.REODR_QTY,i.REODR_LVL, i.MIN_PROCURE_DAYS, i.EXPIRY_DAYS, i.QC_REQUIRED, i.ITEM_STATUS, i.ITEM_REMARKS, i.UOM,i.ASOC_ITEM_CODE, i.DEPT_CODE, i.BRANCH_CODE, i.RACK_CODE, i.CAT_CODE,d.DEPT_NAME, b.BRANCH_NAME FROM TX_ITEM_MST i, CM_DEPT_MST d, CM_BRANCH_MST b WHERE i.BRANCH_CODE = b.BRANCH_CODE AND i.DEPT_CODE = d.DEPT_CODE AND I.CAT_CODE IN ('DYE', 'CHEMICALS', 'CHEMICAL') ORDER BY ITEM_CODE ) WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOVMaterial(CommandText, whereClause, SortExpression, "", SearchQuery, "", "");
            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT *  FROM (SELECT   i.ITEM_CODE,i.ITEM_TYPE, i.ITEM_DESC, i.ITEM_MAKE,i.OP_BAL_STOCK, i.OP_RATE, i.MIN_STOCK_LVL, i.REODR_QTY,i.REODR_LVL, i.MIN_PROCURE_DAYS, i.EXPIRY_DAYS, i.QC_REQUIRED, i.ITEM_STATUS, i.ITEM_REMARKS, i.UOM,i.ASOC_ITEM_CODE, i.DEPT_CODE, i.BRANCH_CODE, i.RACK_CODE, i.CAT_CODE,d.DEPT_NAME, b.BRANCH_NAME FROM TX_ITEM_MST i, CM_DEPT_MST d, CM_BRANCH_MST b WHERE i.BRANCH_CODE = b.BRANCH_CODE AND i.DEPT_CODE = d.DEPT_CODE AND I.CAT_CODE IN ('DYE', 'CHEMICALS', 'CHEMICAL') ORDER BY ITEM_CODE ) WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE)";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ITEM_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void txtItemCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtICode.Text = txtItemCode.SelectedText.ToString();
            txtProcessDesc.Text = txtItemCode.SelectedValue.ToString();
            txtItemQtykg.Text = txtLotSize.Text.Trim();
            txtiMachineVolum.Text = txtMachineCap.Text.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in chemical selection.\r\nSee error log for detail."));
        }
    }
    private void Bind_ChemicalUOM()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("UOM", oUserLoginDetail.COMP_CODE);
            ddlChemicalunit.DataSource = dt;
            ddlChemicalunit.DataValueField = "MST_CODE";
            ddlChemicalunit.DataTextField = "MST_DESC";
            ddlChemicalunit.DataBind();
            //ddlFabType.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void GetDyeProcess()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME_DYE("DYE_PROCESS", oUserLoginDetail.COMP_CODE);
            ddlDyeProcessCode.DataSource = dt;
            ddlDyeProcessCode.DataValueField = "MST_CODE";
            ddlDyeProcessCode.DataTextField = "MST_DESC";
            ddlDyeProcessCode.DataBind();
            //ddlDyeProcessCode.Items.Insert(0, new ListItem("---Select---", "0"));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void txtChemicalQuantity_TextChanged(object sender, EventArgs e)
    {
        double qty = 0, LotSize = 0, Machine_Volumn = 0, dblTemp = 0;
        string uom = string.Empty;
        double.TryParse(txtChemicalQuantity.Text.Trim(), out qty);
        double.TryParse(txtItemQtykg.Text.Trim(), out LotSize);
        // Change by Arun Sharma Valume to capacity
        double.TryParse(txtMachineCap.Text.Trim(), out Machine_Volumn);
        uom = ddlChemicalunit.SelectedValue.ToString();
        if (uom == "%")
        {
            dblTemp = (qty * LotSize) / 100;
            txtiItemQty.Text = dblTemp.ToString();
        }
        else
        {
            dblTemp = (qty * Machine_Volumn) / 1000;
            txtiItemQty.Text = dblTemp.ToString();
        }
    }
}


