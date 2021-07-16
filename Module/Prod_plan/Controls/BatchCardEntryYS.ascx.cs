using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_Prod_plan_Controls_BatchCardEntryYS : System.Web.UI.UserControl
{
    List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN> dtBatchTrn;
    SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string PRODUCT_TYPE = string.Empty;
    #region code to determine Refresh state of Page ie. button click or F5 hit
    private bool _refreshState; 
    private bool _isRefresh;

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
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            lblMode.Text = "Save";
            ddlBatchCode.Visible = false;
            txtBatchCode.Visible = true;
            ddlBatchCode.SelectedIndex = -1;
            txtArticle.Text = string.Empty;
            txtArticleDesc.Text = string.Empty;
            txtBatchCode.Text = string.Empty;
            txtBatchDate.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtLotSize.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtPaNo.Text = string.Empty;
            txtPartDtl.Text = string.Empty;
            txtParty.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtShade.Text = string.Empty;
            ddlPaNo.SelectedIndex = -1;
            grdBatchTrn.DataSource = null;
            grdBatchTrn.DataBind();
            dtBatchTrn = null;
            ViewState["dtBatchTrn"] = null;

            RefreshDetailRow();

            BindBatchCode();
            txtBatchDate.Text = DateTime.Now.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            txtSrNo.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            txtDepartmentName.Text = string.Empty;
            txtMacCode.Text = string.Empty;
            txtMacGroup.Text = string.Empty;
            txtMainProcess.Text = string.Empty;
            txtProcessDesc.Text = string.Empty;
            txtProsCode.Text = string.Empty;
            ddlProcessCode.SelectedIndex = -1;
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
            txtBatchCode.Text = SaitexBL.Interface.Method.BATCH_CARD_MST.GetNewBatchCode(oBATCH_CARD_MST);
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
            ddlPaNo.Items.Clear();
            ddlPaNo.DataSource = GetPaNoData(e.Text.ToUpper(), e.ItemsOffset);
            ddlPaNo.DataTextField = "PI_NO";
            ddlPaNo.DataValueField = "TRN_DATA";
            ddlPaNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + ddlPaNo.Items.Count;
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
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT distinct R.LOT_ID_NO, R.PI_NO, R.TRN_QTY, O.ORDER_NO, O.ARTICAL_CODE, O.ARTICAL_DESC, O.SHADE_CODE, O.PRTY_CODE, O.PRTY_NAME, ( R.LOT_ID_NO|| '@'|| R.PI_NO|| '@'|| R.TRN_QTY|| '@'|| O.ORDER_NO|| '@'|| O.ARTICAL_CODE|| '@'|| O.ARTICAL_DESC|| '@'|| O.SHADE_CODE|| '@'|| O.PRTY_CODE|| '@'|| O.PRTY_NAME)TRN_DATA FROM V_TX_FIBER_IR_TRN r, V_OD_CAPT_TRN_MAIN o WHERE R.COMP_CODE = O.COMP_CODE AND R.BRANCH_CODE = O.BRANCH_CODE AND R.PI_NO = O.PI_NO and r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and r.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "'  and r.trn_type='IBS01' and o.PRODUCT_TYPE= '" + PRODUCT_TYPE + "' order by LOT_ID_NO) WHERE PRTY_NAME LIKE :SearchQuery OR PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR LOT_ID_NO LIKE :SearchQuery OR ARTICAL_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND trn_data NOT IN(SELECT trn_data FROM (SELECT * FROM (SELECT distinct R.LOT_ID_NO,R.PI_NO,R.TRN_QTY,O.ORDER_NO,O.ARTICAL_CODE,O.ARTICAL_DESC,O.SHADE_CODE,O.PRTY_CODE,O.PRTY_NAME,( R.LOT_ID_NO || '@' || R.PI_NO || '@' || R.TRN_QTY || '@' || O.ORDER_NO || '@' || O.ARTICAL_CODE || '@' || O.ARTICAL_DESC || '@' || O.SHADE_CODE || '@' || O.PRTY_CODE || '@' || O.PRTY_NAME) TRN_DATA FROM V_TX_FIBER_IR_TRN r, V_OD_CAPT_TRN_MAIN o WHERE R.COMP_CODE = O.COMP_CODE AND R.BRANCH_CODE = O.BRANCH_CODE AND R.PI_NO = O.PI_NO and r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and r.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "'  and r.trn_type='IBS01' and o.PRODUCT_TYPE= '" + PRODUCT_TYPE + "' order by LOT_ID_NO) WHERE PRTY_NAME LIKE :SearchQuery OR PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR LOT_ID_NO LIKE :SearchQuery OR ARTICAL_CODE LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by LOT_ID_NO";
            string SearchQuery = Text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            
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
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT R.LOT_ID_NO, R.PI_NO, R.TRN_QTY, O.ORDER_NO, O.ARTICAL_CODE, O.ARTICAL_DESC, O.SHADE_CODE, O.PRTY_CODE, O.PRTY_NAME, ( R.LOT_ID_NO|| '@'|| R.PI_NO|| '@'|| R.TRN_QTY|| '@'|| O.ORDER_NO|| '@'|| O.ARTICAL_CODE|| '@'|| O.ARTICAL_DESC|| '@'|| O.SHADE_CODE|| '@'|| O.PRTY_CODE|| '@'|| O.PRTY_NAME)TRN_DATA FROM V_TX_FIBER_IR_TRN r, V_OD_CAPT_TRN_MAIN o WHERE R.COMP_CODE = O.COMP_CODE AND R.BRANCH_CODE = O.BRANCH_CODE AND R.PI_NO = O.PI_NO and r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and r.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and r.trn_type='IBS01' and o.PRODUCT_TYPE= '" + PRODUCT_TYPE + "' order by LOT_ID_NO) WHERE PRTY_NAME LIKE :SearchQuery OR PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR LOT_ID_NO LIKE :SearchQuery OR ARTICAL_CODE LIKE :SearchQuery)";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
            
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
            var TRN_DATA = ddlPaNo.SelectedValue.Trim();
            string[] CombineData = TRN_DATA.Split('@');
            txtLotNo.Text = CombineData[0].ToString();
            txtPaNo.Text = CombineData[1].ToString();
            txtLotSize.Text = CombineData[2].ToString();
            txtOrderNo.Text = CombineData[3].ToString();
            txtArticle.Text = CombineData[4].ToString();
            txtArticleDesc.Text = CombineData[5].ToString();
            txtShade.Text = CombineData[6].ToString();
            txtParty.Text = CombineData[7].ToString();
            txtPartDtl.Text = CombineData[8].ToString();
            GetProcessRouteByPaNo();
            BindGridByData();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting PA No.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void GetProcessRouteByPaNo()
    {
        try
        {          
            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.PA_NO = txtPaNo.Text.Trim();
            dtBatchTrn = SaitexBL.Interface.Method.BATCH_CARD_MST.GetProcessRouteByPaNo(oBATCH_CARD_MST);
            var oVar = (from data in dtBatchTrn  orderby  data.PROS_CODE  ascending  select  data).ToList();
            int iCount = 0;
            foreach (var r in oVar)
            {
                iCount += 1;
                r.SR_NO = iCount;
            }
            ViewState["dtBatchTrn"] = oVar;    
        
        }
        catch
        {
            throw;
        }
    }

    private void BindGridByData()
    {
        try
        {
            dtBatchTrn = (List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>)ViewState["dtBatchTrn"];
            if (dtBatchTrn != null && dtBatchTrn.Count > 0)
            {
                grdBatchTrn.DataSource = dtBatchTrn;
                grdBatchTrn.DataBind();
            }
            else
            {
                grdBatchTrn.DataSource = null;
                grdBatchTrn.DataBind();
                grdBatchTrn.EmptyDataText = "No Data Available.";
                Common.CommonFuction.ShowMessage("No process route for selected Pa No.");
            }
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
            ddlProcessCode.Items.Clear();
            ddlProcessCode.DataSource = GetProsCodeData(e.Text.ToUpper(), e.ItemsOffset);
            ddlProcessCode.DataTextField = "PROS_CODE";
            ddlProcessCode.DataValueField = "PROS_DATA";
            ddlProcessCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + ddlProcessCode.Items.Count;
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
        {   //AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'
            string CommandText = "SELECT * FROM (SELECT *FROM (SELECT DEPARTMENT as DEPT_CODE, DEPT_NAME, MAIN_PROCESS, PROS_CODE, PROS_DESC,MAC_GRUP_CODE as MAC_GROUP_CODE,MAC_CODE as MACHINE_CODE, ( DEPARTMENT|| '@'|| DEPT_NAME|| '@'|| MAIN_PROCESS|| '@'|| PROS_CODE|| '@'|| PROS_DESC|| '@'|| MAC_GRUP_CODE|| '@'|| MAC_CODE)PROS_DATA FROM V_TX_MAC_PROC_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'  ORDER BY PROS_CODE) WHERE PROS_CODE LIKE :SearchQuery OR DEPT_NAME LIKE :SearchQuery OR MAIN_PROCESS LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and PROS_DATA not in  ( SELECT PROS_DATA FROM (SELECT *FROM (SELECT DEPARTMENT as DEPT_CODE, DEPT_NAME, MAIN_PROCESS, PROS_CODE, PROS_DESC,MAC_GRUP_CODE as MAC_GROUP_CODE,MAC_CODE as MACHINE_CODE, ( DEPARTMENT|| '@'|| DEPT_NAME|| '@'|| MAIN_PROCESS|| '@'|| PROS_CODE|| '@'|| PROS_DESC|| '@'|| MAC_GRUP_CODE|| '@'|| MAC_CODE)PROS_DATA FROM V_TX_MAC_PROC_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'  ORDER BY PROS_CODE) WHERE PROS_CODE LIKE :SearchQuery OR DEPT_NAME LIKE :SearchQuery OR MAIN_PROCESS LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " order by PROS_CODE";
            string SearchQuery = Text.ToUpper() + "%";
            return  SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");                       
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
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
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
            var Pros_DATA = ddlProcessCode.SelectedValue.Trim();
            string[] CombineData = Pros_DATA.Split('@');
            txtDepartment.Text = CombineData[0].ToString();
            txtDepartmentName.Text = CombineData[1].ToString();
            txtMainProcess.Text = CombineData[2].ToString();
            txtProsCode.Text = CombineData[3].ToString();
            txtProcessDesc.Text = CombineData[4].ToString();
            txtMacGroup.Text = CombineData[5].ToString();
            txtMacCode.Text = CombineData[6].ToString();
            if (SearchValidateProsindtTrn())
            {
                RefreshDetailRow();
                Common.CommonFuction.ShowMessage("Process already exists. Select another Process");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Process.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchValidateProsindtTrn()
    {
        try
        {
            var bResult = false;
            dtBatchTrn = (List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>)ViewState["dtBatchTrn"];

            var oVar = (from data in dtBatchTrn
                        where data.PROS_CODE == txtProsCode.Text
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
            if (SearchValidateProsindtTrn())
            {
                RefreshDetailRow();
                Common.CommonFuction.ShowMessage("Process already exists. Select another Process");
            }
            else
            {
                var oBATCH_CARD_TRN = new SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN();
                oBATCH_CARD_TRN.BATCH_CODE = int.Parse("");
                oBATCH_CARD_TRN.BATCH_TRN_CODE = "";
                oBATCH_CARD_TRN.DEPT_CODE = txtDepartment.Text;
                oBATCH_CARD_TRN.DEPT_NAME = txtDepartmentName.Text;
                oBATCH_CARD_TRN.MACHINE_CODE = txtMacCode.Text;
                oBATCH_CARD_TRN.MACHINE_GROUP = txtMacGroup.Text;
                oBATCH_CARD_TRN.MAIN_PROCESS = txtMainProcess.Text;
                oBATCH_CARD_TRN.NO_OF_UNIT = 0;
                oBATCH_CARD_TRN.PROS_CODE = txtProsCode.Text;
                oBATCH_CARD_TRN.PROS_DESC = txtProcessDesc.Text;
                oBATCH_CARD_TRN.QTY = 0;
                oBATCH_CARD_TRN.REMARKS = "";
                oBATCH_CARD_TRN.SR_NO = int.Parse(txtSrNo.Text.Trim());
                oBATCH_CARD_TRN.TDATE = DateTime.Now;
                oBATCH_CARD_TRN.TUSER = oUserLoginDetail.UserCode;
                oBATCH_CARD_TRN.UOM_OF_UNIT = "BOX";
                oBATCH_CARD_TRN.WEIGHT_OF_UNIT = 0;

                dtBatchTrn = (List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>)ViewState["dtBatchTrn"];
                var oSort = (from data in dtBatchTrn   orderby data.SR_NO ascending   select data).ToList();
                var Newdt = new List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>();
                int iCount = 0;
                var rRes = (from data1 in dtBatchTrn  where data1.SR_NO == oBATCH_CARD_TRN.SR_NO  orderby data1.SR_NO ascending select data1).ToList();
                foreach (var r in oSort)
                {
                    if (oBATCH_CARD_TRN.SR_NO > r.SR_NO)
                    {
                        Newdt.Add(r);
                        iCount = r.SR_NO;
                    }
                    else if (oBATCH_CARD_TRN.SR_NO == r.SR_NO)
                    {
                        iCount = oBATCH_CARD_TRN.SR_NO;
                        Newdt.Add(oBATCH_CARD_TRN);

                        foreach (var v in rRes)
                        {
                            iCount += 1;
                            r.SR_NO = iCount;
                            Newdt.Add(v);
                        }
                    }
                    else if (oBATCH_CARD_TRN.SR_NO < r.SR_NO)
                    {
                        iCount += 1;
                        r.SR_NO = iCount;
                        Newdt.Add(r);
                    }
                }
                if (iCount < oBATCH_CARD_TRN.SR_NO)
                {
                    iCount += 1;
                    oBATCH_CARD_TRN.SR_NO = iCount;
                    Newdt.Add(oBATCH_CARD_TRN);
                }

                ViewState["dtBatchTrn"] = Newdt;
                BindGridByData();

                RefreshDetailRow();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving Trn Detail.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        RefreshDetailRow();
    }

    protected void grdBatchTrn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DelBatchTrn")
            {              
                var ProsCode = e.CommandArgument.ToString();
                dtBatchTrn = (List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>)ViewState["dtBatchTrn"];
                dtBatchTrn.RemoveAll(x => x.PROS_CODE == ProsCode);
                var oVar = (from data in dtBatchTrn orderby data.PROS_CODE ascending   select data).ToList();
                int iCount = 0;
                foreach (var r in oVar)
                {
                    iCount += 1; r.SR_NO = iCount;
                }               
                ViewState["dtBatchTrn"] = oVar; 
                BindGridByData();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting Process.\r\nSee error log for detail."));
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
                    SaveData(SaitexBL.Common.CommonFuction.SaveState.Save);
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

            iCountAll += 1;
            if (!string.IsNullOrEmpty(txtPaNo.Text))
            {
                iCount += 1;
            }
            else
            {
                msg += @"Invalid PA No.\r\n";
            }

            iCountAll += 1;
            if (!string.IsNullOrEmpty(txtLotNo.Text))
            {
                iCount += 1;
            }
            else
            {
                msg += @"Invalid Lot No.\r\n";
            }

            dtBatchTrn = (List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>)ViewState["dtBatchTrn"];
            iCountAll += 1;
            if (dtBatchTrn.Count > 0)
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
            oBATCH_CARD_MST.BATCH_CODE = int.Parse(txtBatchCode.Text.ToString());
            oBATCH_CARD_MST.BATCH_DATE = DateTime.Parse(txtBatchDate.Text);
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.LOT_NUMBER = txtLotNo.Text;
            oBATCH_CARD_MST.PA_NO = txtPaNo.Text;
            oBATCH_CARD_MST.REMARKS = txtRemarks.Text;
            oBATCH_CARD_MST.TDATE = DateTime.Now;
            oBATCH_CARD_MST.TUSER = oUserLoginDetail.UserCode;
            dtBatchTrn = (List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_TRN>)ViewState["dtBatchTrn"];            string BATCH_CODE = string.Empty;
            //var bResult = SaitexBL.Interface.Method.BATCH_CARD_MST.InsertBatchCardEntry(oBATCH_CARD_MST, dtBatchTrn, out BATCH_CODE, savestate);
            //if (bResult)
            //{
            //    Common.CommonFuction.ShowMessage(@"Batch Card Entry Saved Successfully.\r\nYour Batch Card No is :-" + BATCH_CODE);
            //    InitialisePage();
            //}
            //else
            //{
            //    Common.CommonFuction.ShowMessage("Batch Card Entry saving failed.");
            //}
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
            ddlBatchCode.Items.Clear();
            ddlBatchCode.DataSource = GetBatchCodeData(e.Text.ToUpper(), e.ItemsOffset);
            ddlBatchCode.DataTextField = "BATCH_CODE";
            ddlBatchCode.DataValueField = "BATCH_CODE";
            ddlBatchCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + ddlBatchCode.Items.Count;
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
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, LOT_NUMBER FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND BATCH_CODE NOT IN (SELECT BATCH_CODE FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, LOT_NUMBER FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " order by BATCH_CODE";
            string SearchQuery = Text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
                       
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
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT BATCH_CODE, BATCH_DATE, PA_NO, LOT_NUMBER FROM V_BATCH_CARD_MST r WHERE r.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND r.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY BATCH_CODE) WHERE BATCH_CODE LIKE :SearchQuery OR PA_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
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
            oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            var BatchCode = ddlBatchCode.SelectedText.Trim();
            oBATCH_CARD_MST.BATCH_CODE =int.Parse(BatchCode);
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            int Status = 0;
            oBATCH_CARD_MST = SaitexBL.Interface.Method.BATCH_CARD_MST.GetBatchEntryDataByBatchCode(oBATCH_CARD_MST, out Status);
            if (Status > 0)
            {
                ddlBatchCode.Visible = false;
                txtBatchCode.Visible = true;
                txtArticle.Text = oBATCH_CARD_MST.ARTICLE_CODE;
                txtArticleDesc.Text = oBATCH_CARD_MST.ARTICLE_DESC;
                txtBatchCode.Text =  oBATCH_CARD_MST.BATCH_CODE.ToString();
                txtBatchDate.Text = oBATCH_CARD_MST.BATCH_DATE.ToShortDateString();
                txtLotNo.Text = oBATCH_CARD_MST.LOT_NUMBER;
                txtLotSize.Text = oBATCH_CARD_MST.LOT_SIZE.ToString();
                txtOrderNo.Text = oBATCH_CARD_MST.ORDER_NO;
                txtPaNo.Text = oBATCH_CARD_MST.PA_NO;
                txtParty.Text = oBATCH_CARD_MST.PRTY_CODE;
                txtPartDtl.Text = oBATCH_CARD_MST.PRTY_NAME;
                txtRemarks.Text = oBATCH_CARD_MST.REMARKS;
                txtShade.Text = oBATCH_CARD_MST.SHADE_CODE;
                dtBatchTrn = SaitexBL.Interface.Method.BATCH_CARD_MST.GetBatchEntryTRNByBatchCode(oBATCH_CARD_MST);
                ViewState["dtBatchTrn"] = dtBatchTrn;
                BindGridByData();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                    SaveData(SaitexBL.Common.CommonFuction.SaveState.Update);
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
            Response.Redirect("~/Module/prod_plan/Reports/BatchCardEntryReport.aspx?BATCH_CODE=" + (int.Parse(txtBatchCode.Text) - 1).ToString(), false);
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

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

}
