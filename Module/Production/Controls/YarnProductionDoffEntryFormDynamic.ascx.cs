using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Common;
using errorLog;
using Obout.Interface;
using System.Data;
using Obout.ComboBox;
public partial class Module_Production_Controls_YarnProductionDoffEntryFormDynamic : System.Web.UI.UserControl
{
    private DataTable dtProdTRN;
    //private  DataTable dtProdColorChem;
    SaitexDM.Common.DataModel.YRN_PROD_MST oYRN_PROD_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string TRN_TYPE { get; set; }
    public string PRODUCT_TYPE { get; set; }
    public string MAIN_PROCESS { get; set; }
    public string PROS_CODE { get; set; }

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
            txtMachineStopage.ReadOnly = true;
            BindShift();
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

    private void BindUOM(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dtUOM = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("UOM", oUserLoginDetail.COMP_CODE);
            ddl.DataSource = dtUOM;
            ddl.DataTextField = "MST_CODE";
            ddl.DataValueField = "MST_CODE";
            ddl.DataBind();
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue("KG"));
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
            Session["dtMachineStopDataTable"] = null;
            Session["dtProdTRN"] = null;
            Session["dtProdColorChem"] = null;
            Session["dtItemReceipt"] = null;
            RefreshProdTRN();
            BindLotDetailGrid();
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
            ddlProsCode.Enabled = true;
            ddlMachineCode.SelectedIndex = -1;
            txtMachineDesc.Text = string.Empty;
            txtProsIdNo.Text = string.Empty;
            txtEntryDate.Text = System.DateTime.Now.Date.ToShortDateString();
            ddlShift.SelectedIndex = -1;
            txtMachineStopage.Text = "0";
            txtProcessTime.Text = string.Empty;
            txtOperator.Text = string.Empty;
            txtSupervisor.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtPaperTubeColor.Text = string.Empty;
            txtDoffingNetWt.Text = string.Empty;
            txtAirPressure.Text = string.Empty;
            txtRotoJetNo.Text = string.Empty;
            txtQuality.Text = string.Empty;
            ddlSide.SelectedIndex = -1;
            cmbLotNo.SelectedIndex = -1;
            txtFinishType.Text = string.Empty;
            txtMergeNo.Text = string.Empty;
            txtLotType.Text = string.Empty;
            lblTotalDoff.Text = string.Empty;
            lblTotalDoffWt.Text = string.Empty;
            ddlMachineCode.Enabled = true;
            ddlProsCode.Enabled = true;
            txtMachineCode.Text = string.Empty;
            var todayDate = DateTime.Now;
            txtLoadingDate.Text = todayDate.ToString("dd/MM/yyyy");
            txtUnLoadingDate.Text = todayDate.AddDays(1).ToString("dd/MM/yyyy");
            lblTotalCops.Text = string.Empty;
            txtToBatchNo.Text = string.Empty;
            startTime.SetTime(08, 00, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
            endTime.SetTime(07, 59, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
            txtMachineSpeed.Text = string.Empty;
            txtTotalDoffNo.Text = string.Empty;

            txtOrderDescription.Text = string.Empty;
            txtPattern.Text = string.Empty;
            txtGreyArticleCode.Text = string.Empty;
            txtGreyArticleDesc.Text = string.Empty;
            txtProdProcessID.Text = string.Empty;
            txtGainQty.Text = string.Empty;
           
            chkIsGain.Checked = false;
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
            ddlLotNo.Enabled = true;
            //txtOrderNo.Text = string.Empty;
            cmbOrderNo.SelectedIndex = -1;

            txtUnloadQty.Text = string.Empty;
            txtToLocation.Text = string.Empty;
            //txtToBatchNo.Text = string.Empty;
            ddlPackaging.SelectedIndex = -1;
            txtUnloadNoOfUnit.Text = "0";
            ddlUnloadUOM.SelectedIndex = -1;
            txtUnloadWeightOfUnit.Text = "0";
            //txtOrderDescription.Text = string.Empty;        
            //txtPattern.Text = string.Empty;
            // txtGreyArticleCode.Text = string.Empty;
            //txtGreyArticleDesc.Text = string.Empty;
            txtDyedLot.Text = string.Empty;

            ViewState["UNIQUE_TRN"] = null;
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
            string CommandText = " SELECT distinct * FROM (SELECT MC.MACHINE_CODE, MC.MACHINE_GROUP, MC.MACHINE_MAKE,MC.OLD_MACHINE_NAME , ( '('|| MC.MACHINE_CODE|| '),'|| MC.MACHINE_GROUP|| ', '|| MC.MACHINE_MAKE|| '@'|| TO_CHAR (MC.LAST_UNLOAD, 'YYYY/MM/DD HH:MM:SS AM'))MACHINE_DATA FROM MC_MACHINE_MASTER MC, TX_MAC_PROC_MST PC WHERE MC.MACHINE_GROUP= PC.MAC_CODE AND PC.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PC.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PC.MAIN_PROCESS = '" + MAIN_PROCESS + "') ASD ";
            string WhereClause = "  where MACHINE_CODE like :SearchQuery or MACHINE_GROUP like :SearchQuery or MACHINE_MAKE like :SearchQuery";
            string SortExpression = " order by MACHINE_CODE asc";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
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
                txtMachineCode.Text = ddlMachineCode.SelectedText;
                txtMachineDesc.Text = arrString[0].ToString();

                //if (!arrString[1].ToString().Equals(string.Empty))
                //{
                //    txtLoadingDate.Text = DateTime.Parse(arrString[1].ToString()).ToString();
                //    txtUnLoadingDate.Text = DateTime.Parse(arrString[1].ToString()).ToString();
                //}
            }
            //ddlLotNo.SelectedIndex = -1;
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
            string MACHINE_DATA = ddlMachineCode.SelectedValue.ToString();
            char[] splitter = { ',' };
            string[] arrString = MACHINE_DATA.Split(splitter);
            string MACHINE_GROUP = arrString[1].ToString();
            string CommandText = "select * from ( select * from TX_MAC_PROC_MST WHERE MAC_CODE='" + MACHINE_GROUP.Trim() + "' ) ASD ";
            string WhereClause = "  where PROS_CODE like :SearchQuery or PROS_DESC like :SearchQuery ";
            string SortExpression = " order by PROS_CODE asc";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

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
                ddlLotNo.SelectedIndex = -1;
                txtProsDesc.Text = ddlProsCode.SelectedText.Trim();
                lblProsCode.Text = ddlProsCode.SelectedValue.Trim();
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
                oYRN_PROD_MST.PROS_CODE = ddlProsCode.SelectedValue.Trim();
                txtProsIdNo.Text = (int.Parse(SaitexBL.Interface.Method.YRN_PROD_MST.GetMaxDoffProsIdNo(oYRN_PROD_MST)) + 1).ToString();
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
            txtMachineStopage.ReadOnly = true;
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


            DateTime loadingDate = DateTime.Now;
            DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
            DateTime times = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", startTime.Hour, startTime.Minute, startTime.Second, startTime.AmPm));
            var stime = TimeSpan.Parse(times.ToString("HH:mm:ss"));
            var LDATE = loadingDate.Add(stime);



            DateTime unloadingDate = DateTime.Now;
            DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
            DateTime timee = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", endTime.Hour, endTime.Minute, endTime.Second, endTime.AmPm));
            var etime = TimeSpan.Parse(timee.ToString("HH:mm:ss"));
            var UDATE = unloadingDate.Add(etime);

            if (msg.Equals(string.Empty))
            {
                int StopageTime = 0;
                int.TryParse(txtMachineStopage.Text, out StopageTime);

                TimeSpan tm = UDATE.Subtract(LDATE);
                int diffday = tm.Days;
                int diffhour = tm.Hours;
                int diffminute = tm.Minutes;
                int Totaldiffminute = diffday * 24 * 60 + diffhour * 60 + diffminute;
                int TotalProcessTime = Totaldiffminute - StopageTime;
                double totalProcessTimeInHours = 0;
                double.TryParse(TotalProcessTime.ToString(), out totalProcessTimeInHours);

                if (TotalProcessTime < 0)
                {
                    //CommonFuction.ShowMessage("Please select load date and time less than unload date and time");
                    txtUnLoadingDate.Text = string.Empty;
                }
                txtProcessTime.Text = Math.Round((totalProcessTimeInHours / 60), 2).ToString();//TotalProcessTime.ToString();

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
            string CommandText = string.Empty;
            string WhereClause = string.Empty;
            //CommandText = " SELECT   *  FROM   (  SELECT   DISTINCT       TO_CHAR(TRN_DATE,'dd/MM/yyyy') LOAD_DATE, TRN_DATE, PROS_ID_NO,MACHINE_CODE,LOT_NUMBER, ORDER_NO,DEPT_CODE, PROS_CODE, ARTICLE_CODE, ARTICLE_DESC,   NVL (LOAD_QTY, 0) - NVL (UNLOAD_QTY, 0) LOAD_QTY,NVL (LOAD_NO_OF_UNIT, 0) - NVL (UNLOAD_NO_OF_UNIT, 0)     LOAD_NO_OF_UNIT, (   ORDER_NO    || '@' || (NVL (LOAD_QTY, 0) - NVL (UNLOAD_QTY, 0))     || '@' || ARTICLE_CODE|| '@'|| ARTICLE_DESC || '@'|| PROS_CODE || '@'|| LOT_NUMBER || '@' || FR_BATCH_NO|| '@'|| TO_BATCH_NO      || '@'    ||  LOAD_UOM_OF_UNIT || '@'|| PATTERN_NO || '@'|| PROS_ID_NO)     LOT_DATA     FROM   V_YRN_PROD_TRN       WHERE   TRN_TYPE = 'PRD01'         AND NVL (UNLOAD_QTY, 0) = 0 AND LOT_NUMBER='" + txtMergeNo.Text + "' AND ARTICLE_CODE='" + txtGreyArticleCode.Text + "'    ) ASD";// AND NVL (LOAD_QTY, 0) - NVL (UNLOAD_QTY, 0) > 0 AND TO_CHAR(TRN_DATE,'dd/MM/yyyy')='" + txtEntryDate.Text + "'
            if (chkIsGain.Checked)
            {
                CommandText = " SELECT   *  FROM   (SELECT   DISTINCT   LOT_NUMBER,   ORDER_NO,           PROS_CODE,          ARTICLE_CODE,               ARTICLE_DESC,                   SUM(NVL (LOAD_QTY, 0) - NVL (UNLOAD_QTY, 0)) LOAD_QTY,SUM(NVL (LOAD_NO_OF_UNIT, 0) - NVL (UNLOAD_NO_OF_UNIT, 0))         LOAD_NO_OF_UNIT,      (         SUM((NVL (LOAD_QTY, 0) - NVL (UNLOAD_QTY, 0)))|| '@'|| ARTICLE_CODE  || '@' || ARTICLE_DESC || '@'  || PROS_CODE  || '@'   || LOT_NUMBER|| '@'  || FR_BATCH_NO  || '@' || TO_BATCH_NO|| '@'|| LOAD_UOM_OF_UNIT      || '@'     || PATTERN_NO   )      LOT_DATA  FROM   V_YRN_PROD_TRN          WHERE       TRN_TYPE = 'PRD01'                             AND LOT_NUMBER = '" + txtMergeNo.Text + "'                   AND ARTICLE_CODE = '" + txtGreyArticleCode.Text + "'                   GROUP BY  LOT_NUMBER,                   ORDER_NO,                     PROS_CODE,                   ARTICLE_CODE,                   ARTICLE_DESC,FR_BATCH_NO,TO_BATCH_NO,LOAD_UOM_OF_UNIT,PATTERN_NO   ) ASD";
         
                 }
            else
            {
                CommandText = " SELECT   *  FROM   (SELECT   DISTINCT   LOT_NUMBER,   ORDER_NO,           PROS_CODE,          ARTICLE_CODE,               ARTICLE_DESC,                   SUM(NVL (LOAD_QTY, 0) - NVL (UNLOAD_QTY, 0)) LOAD_QTY,SUM(NVL (LOAD_NO_OF_UNIT, 0) - NVL (UNLOAD_NO_OF_UNIT, 0))         LOAD_NO_OF_UNIT,      (         SUM((NVL (LOAD_QTY, 0) - NVL (UNLOAD_QTY, 0)))|| '@'|| ARTICLE_CODE  || '@' || ARTICLE_DESC || '@'  || PROS_CODE  || '@'   || LOT_NUMBER|| '@'  || FR_BATCH_NO  || '@' || TO_BATCH_NO|| '@'|| LOAD_UOM_OF_UNIT      || '@'     || PATTERN_NO   )      LOT_DATA  FROM   V_YRN_PROD_TRN          WHERE       TRN_TYPE = 'PRD01'          AND NVL (LOAD_QTY, 0) - NVL (UNLOAD_QTY, 0) > 0                   AND LOT_NUMBER = '" + txtMergeNo.Text + "'                   AND ARTICLE_CODE = '" + txtGreyArticleCode.Text + "'                   GROUP BY  LOT_NUMBER,                   ORDER_NO,                     PROS_CODE,                   ARTICLE_CODE,                   ARTICLE_DESC,FR_BATCH_NO,TO_BATCH_NO,LOAD_UOM_OF_UNIT,PATTERN_NO   ) ASD";
           
                   }
            WhereClause = "  where LOT_NUMBER like :SearchQuery or ORDER_NO like :SearchQuery ";
            string SortExpression = "";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

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
            if (imgbtnSave.Visible)
            {
                if (ddlLotNo.SelectedIndex != -1)
                {
                    if (ddlProsCode.SelectedIndex != -1)
                    {
                        string[] arrString = ddlLotNo.SelectedValue.Split('@');
                        string msg = string.Empty;
                        //if (ValidateMachineProcess(out msg, ddlProsCode.SelectedValue.ToString(), arrString[0].ToString()))
                        //{
                        if (msg == string.Empty)
                        {
                         
                            txtLotQty.Text = arrString[0].ToString();
                            txtMaxLoadQty.Text = arrString[0].ToString();
                            txtUnloadQty.Text = arrString[0].ToString();                          
                            lblPROS_CODE.Text = arrString[3].ToString();
                            lblBIN_LOCT.Text = arrString[4].ToString();
                            lblBATCH_NO.Text = arrString[5].ToString();
                            ddlUnloadUOM.SelectedIndex = ddlUnloadUOM.Items.IndexOf(ddlUnloadUOM.Items.FindByValue(arrString[7].ToString()));
                           // txtProdProcessID.Text = arrString[10].ToString();



                            //txtPattern.Text = arrString[9].ToString();
                            //txtOrderDescription.Text = arrString[10].ToString();
                            // txtGreyArticleDesc.Text = arrString[12].ToString();

                        }
                        else
                        {

                            Common.CommonFuction.ShowMessage(msg);
                        }

                        //}
                        //else
                        //{

                        //    Common.CommonFuction.ShowMessage(msg);
                        //}
                    }
                    else
                    {

                        Common.CommonFuction.ShowMessage("Please Select Process Code!!");
                    }

                }

                else
                {

                    Common.CommonFuction.ShowMessage("Please Select Lot No!!");
                }
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting lot detail.\r\nSee error log for detail."));
        }
    }
    private bool ValidateMachineProcess(out string msg, string ProcessCode, string Orderno)
    {
        try
        {
            msg = string.Empty;
            string Text = string.Empty;
            bool result = false;
            string CommandText = " select * from (select NVL(S_NO,0)S_NO from OD_CAPT_TRN_PROCESS_ROUTE Where PROS_CODE= '" + ProcessCode + "' and PI_NO= '" + Orderno + "' And COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' And BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ) ma ";//And BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'
            string WhereClause = "  where S_NO like :SearchQuery ";
            string SortExpression = " order by S_NO asc";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            if (data != null)
            {

                if (data.Rows.Count > 0)
                {

                    if (int.Parse(data.Rows[0]["S_NO"].ToString()) == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        int S_NO = int.Parse(data.Rows[0]["S_NO"].ToString());
                        S_NO = S_NO - 1;
                        string CommandText1 = " SELECT   *  FROM   (SELECT   S_NO , PROS_CODE  FROM OD_CAPT_TRN_PROCESS_ROUTE WHERE   S_NO = '" + S_NO + "' AND PI_NO = '" + Orderno + "' AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'  And BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ) ma ";//And BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'
                        string WhereClause1 = "  where S_NO like :SearchQuery ";
                        string SortExpression1 = " order by S_NO asc";
                        string SearchQuery1 = Text + "%";
                        DataTable data1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, WhereClause1, SortExpression1, "", SearchQuery1, "");

                        if (data1 != null)
                        {
                            if (data1.Rows.Count > 0)
                            {
                                string PROS_CODE = data1.Rows[0]["PROS_CODE"].ToString();
                                string CommandText2 = "  SELECT   BIN_LOCT,  PROS_CODE,  BATCH_NO,  DYED_LOT_NO,  PROS_CODE,  ORDER_NO,  COMP_CODE,  BRANCH_CODE FROM   YRN_WIP_STOCK";
                                string WhereClause2 = "   WHERE  PROS_CODE like :searchquery and PROS_CODE = '" + PROS_CODE + "' AND ORDER_NO = '" + Orderno + "' AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' And BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ";
                                string SortExpression2 = " order by BIN_LOCT asc ";
                                string SearchQuery2 = "%";
                                DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, WhereClause2, SortExpression2, "", SearchQuery2, "");

                                if (data2 != null)
                                {
                                    if (data2.Rows.Count > 0)
                                    {
                                        lblPROS_CODE.Text = data2.Rows[0]["PROS_CODE"].ToString();
                                        lblBIN_LOCT.Text = data2.Rows[0]["BIN_LOCT"].ToString();
                                        lblBATCH_NO.Text = data2.Rows[0]["BATCH_NO"].ToString();
                                        txtDyedLot.Text = data2.Rows[0]["DYED_LOT_NO"].ToString();
                                        result = true;
                                    }
                                    else
                                    {
                                        msg = "Please Run Preceding Process for the Order First!! Preceding Process Code is =" + PROS_CODE;
                                        result = false;
                                    }

                                }

                            }

                        }


                    }

                }
                else
                {
                    msg = " No Process For This Order Please Create Process Route";

                    result = false;
                }
            }
            else
            {
                msg = " No Process For This Order Please Create Process Route";

                result = false;

            }
            return result;

        }
        catch
        {
            throw;
        }
    }











    private void GetUnLoadQty()
    {
        try
        {

            //double UnLoadNoOfUnit = 0;
            //double UnLoadWeightOfUnit = 0;
            //double.TryParse(txtUnloadNoOfUnit.Text, out UnLoadNoOfUnit);
            //txtUnloadNoOfUnit.Text = UnLoadNoOfUnit.ToString();
            //double.TryParse(txtUnloadWeightOfUnit.Text, out UnLoadWeightOfUnit);
            //txtUnloadWeightOfUnit.Text = UnLoadWeightOfUnit.ToString();
            //double UnLoadQty = UnLoadNoOfUnit * UnLoadWeightOfUnit;
            //txtUnloadQty.Text = UnLoadQty.ToString();

            double UnLoadNoOfUnit = 0;
            double UnLoadWeight = 0;
            double.TryParse(txtUnloadQty.Text, out UnLoadWeight);
            double.TryParse(txtUnloadNoOfUnit.Text, out UnLoadNoOfUnit);
            if (UnLoadNoOfUnit > 0 && UnLoadWeight > 0)
                txtUnloadWeightOfUnit.Text = (UnLoadWeight / UnLoadNoOfUnit).ToString();


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

            double UNLOAD_QTY = 0;
            double.TryParse(txtUnloadQty.Text.Trim(), out UNLOAD_QTY);
            double UnloadNoOfUnit = 0;
            double.TryParse(txtUnloadNoOfUnit.Text, out UnloadNoOfUnit);
            TotalCount += 1;
            if (UNLOAD_QTY > 0)
            {
                iCount += 1;
            }
            else
            {
                msg += "@Please Enter Quantity!";
            }
            TotalCount += 1;
            if (UnloadNoOfUnit > 0)
            {
                iCount += 1;
            }
            else
            {
                msg += "@Please Enter No of Cheeses!";

            }
            TotalCount += 1;
            if (!string.IsNullOrEmpty(cmbOrderNo.SelectedValue))
            {
                iCount += 1;
            }
            else
            {
                msg += "@Please Select Order No!";

            }

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

            if (Session["dtProdTRN"] == null)
                dtProdTRN = CreateLotDetailTable();
            else
                dtProdTRN = (DataTable)Session["dtProdTRN"];

            if (dtProdTRN.Rows.Count < 15)
            {
                int UNIQUE_TRN = 0;
                if (ViewState["UNIQUE_TRN"] != null)
                    UNIQUE_TRN = int.Parse(ViewState["UNIQUE_TRN"].ToString());

                string LotNo = ddlLotNo.SelectedText.Trim();
                bool bb = SearchTRNLotInGrid(LotNo, UNIQUE_TRN);
                if (!bb)
                {
                    
                    double LOT_QTY = 0;
                    double.TryParse(txtLotQty.Text, out LOT_QTY);

                    double UNLOAD_QTY = 0;
                    double.TryParse(txtUnloadQty.Text.Trim(), out UNLOAD_QTY);


                    GetUnLoadQty();
                    if (UNIQUE_TRN > 0)
                    {
                        DataView dv = new DataView(dtProdTRN);
                        dv.RowFilter = "UNIQUE_TRN=" + UNIQUE_TRN;
                        if (dv.Count > 0)
                        {
                            dv[0]["ORDER_NO"] = cmbOrderNo.SelectedValue;//txtOrderNo.Text.Trim();
                            dv[0]["ORDER_DESC"] = txtOrderDescription.Text.Trim();
                            dv[0]["LOT_NUMBER"] = cmbLotNo.SelectedText;
                            dv[0]["LOT_QTY"] = LOT_QTY;
                            int UNLOAD_NO_OF_UNIT = 0;
                            int.TryParse(txtUnloadNoOfUnit.Text.Trim(), out UNLOAD_NO_OF_UNIT);
                            dv[0]["UNLOAD_NO_OF_UNIT"] = UNLOAD_NO_OF_UNIT;
                            dv[0]["UNLOAD_UOM_OF_UNIT"] = ddlUnloadUOM.SelectedItem.Text.ToString();
                            double UNLOAD_WEIGHT_OF_UNIT = 0;
                            double.TryParse(txtUnloadWeightOfUnit.Text.Trim(), out UNLOAD_WEIGHT_OF_UNIT);
                            dv[0]["UNLOAD_WEIGHT_OF_UNIT"] = Math.Round(UNLOAD_WEIGHT_OF_UNIT, 3);
                            dv[0]["UNLOAD_QTY"] = Math.Round(UNLOAD_QTY, 3);
                            dv[0]["DEPT_CODE"] = oUserLoginDetail.VC_DEPARTMENTCODE;
                            dv[0]["FR_LOCATION"] = lblBIN_LOCT.Text;
                            dv[0]["TO_LOCATION"] = ddlPackaging.SelectedValue;//txtToLocation.Text.Trim();
                            dv[0]["FR_BATCH_NO"] = lblBATCH_NO.Text;
                            dv[0]["TO_BATCH_NO"] = txtToBatchNo.Text.Trim();
                            dv[0]["FR_PROS_CODE"] = lblPROS_CODE.Text;
                            DateTime loadingDate = DateTime.Now;
                            DateTime unloadingDate = DateTime.Now;
                            DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
                            DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
                            dv[0]["LOAD_DATE"] = loadingDate;
                            dv[0]["UNLOAD_DATE"] = unloadingDate;
                            dv[0]["PATTERN_NO"] = txtPattern.Text.Trim();
                            dv[0]["EFFICIENCY"] = 0f;
                            dv[0]["ARTICLE_CODE"] = txtGreyArticleCode.Text.Trim();
                            dv[0]["ARTICLE_DESC"] = txtGreyArticleDesc.Text.Trim();
                            dv[0]["REJECTIONS_OF_UNIT"] = 0f;
                            dv[0]["WEIGHT_OF_REJECTION"] = 0f;
                            int PROD_PROS_ID_NO = 0;
                            int.TryParse(txtProdProcessID.Text.Trim(), out PROD_PROS_ID_NO);
                            dv[0]["PROD_PROS_ID_NO"] = PROD_PROS_ID_NO;

                            dtProdTRN.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtProdTRN.NewRow();
                        dr["UNIQUE_TRN"] = dtProdTRN.Rows.Count + 1;
                        dr["ORDER_NO"] = cmbOrderNo.SelectedValue;//txtOrderNo.Text.Trim();
                        dr["ORDER_DESC"] = txtOrderDescription.Text.Trim();
                        dr["LOT_NUMBER"] = cmbLotNo.SelectedText;
                        dr["LOT_QTY"] = LOT_QTY;

                        int UNLOAD_NO_OF_UNIT = 0;
                        int.TryParse(txtUnloadNoOfUnit.Text.Trim(), out UNLOAD_NO_OF_UNIT);
                        dr["UNLOAD_NO_OF_UNIT"] = UNLOAD_NO_OF_UNIT;
                        dr["UNLOAD_UOM_OF_UNIT"] = ddlUnloadUOM.SelectedItem.Text.ToString();
                        double UNLOAD_WEIGHT_OF_UNIT = 0;
                        double.TryParse(txtUnloadWeightOfUnit.Text.Trim(), out UNLOAD_WEIGHT_OF_UNIT);
                    
                        dr["UNLOAD_WEIGHT_OF_UNIT"] = Math.Round(UNLOAD_WEIGHT_OF_UNIT, 3);
                        dr["UNLOAD_QTY"] = Math.Round(UNLOAD_QTY, 3);

                        dr["DEPT_CODE"] = oUserLoginDetail.VC_DEPARTMENTCODE;
                        dr["FR_LOCATION"] = lblBIN_LOCT.Text;
                        dr["TO_LOCATION"] = ddlPackaging.SelectedValue;// txtToLocation.Text.Trim();
                        dr["FR_BATCH_NO"] = lblBATCH_NO.Text;
                        dr["TO_BATCH_NO"] = txtToBatchNo.Text.Trim();
                        dr["FR_PROS_CODE"] = lblPROS_CODE.Text;
                        dr["LOAD_DATE"] = string.Empty; // DateTime.Parse(txtLoadingDateTime.Text.Trim());
                        dr["UNLOAD_DATE"] = string.Empty; //DateTime.Parse(txtUnLoadingDateTime.Text.Trim());                       
                        dr["PATTERN_NO"] = txtPattern.Text.Trim();
                        dr["EFFICIENCY"] = 0f;
                        dr["ARTICLE_CODE"] = txtGreyArticleCode.Text.Trim();
                        dr["ARTICLE_DESC"] = txtGreyArticleDesc.Text.Trim();
                        dr["REJECTIONS_OF_UNIT"] = 0f;
                        dr["WEIGHT_OF_REJECTION"] = 0f;
                        int PROD_PROS_ID_NO = 0;
                        int.TryParse(txtProdProcessID.Text.Trim(), out PROD_PROS_ID_NO);
                        dr["PROD_PROS_ID_NO"] = PROD_PROS_ID_NO;

                        dtProdTRN.Rows.Add(dr);
                    }
                    RefreshProdTRN();
                    Session["dtProdTRN"] = dtProdTRN;
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

            if (Session["dtProdTRN"] == null)
                dtProdTRN = CreateLotDetailTable();
            else
                dtProdTRN = (DataTable)Session["dtProdTRN"];
            grdLotDetail.DataSource = dtProdTRN;
            grdLotDetail.DataBind();
            //CalculateUnloadQty();
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateLotDetailTable()
    {
        try
        {
            dtProdTRN = new DataTable();
            dtProdTRN.Columns.Add("UNIQUE_TRN", typeof(int));
            dtProdTRN.Columns.Add("ORDER_NO", typeof(string));
            dtProdTRN.Columns.Add("ORDER_DESC", typeof(string));
            dtProdTRN.Columns.Add("LOT_NUMBER", typeof(string));
            dtProdTRN.Columns.Add("LOT_QTY", typeof(double));

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

            dtProdTRN.Columns.Add("PATTERN_NO", typeof(string));
            dtProdTRN.Columns.Add("EFFICIENCY", typeof(double));
            dtProdTRN.Columns.Add("ARTICLE_CODE", typeof(string));
            dtProdTRN.Columns.Add("ARTICLE_DESC", typeof(string));
            dtProdTRN.Columns.Add("REJECTIONS_OF_UNIT", typeof(double));
            dtProdTRN.Columns.Add("WEIGHT_OF_REJECTION", typeof(double));
            dtProdTRN.Columns.Add("PROD_PROS_ID_NO", typeof(int));

            return dtProdTRN;
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

            if (Session["dtProdTRN"] == null)
                dtProdTRN = CreateLotDetailTable();
            else
                dtProdTRN = (DataTable)Session["dtProdTRN"];
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
            Session["dtProdTRN"] = dtProdTRN;
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

            if (Session["dtProdTRN"] == null)
                dtProdTRN = CreateLotDetailTable();
            else
                dtProdTRN = (DataTable)Session["dtProdTRN"];
            DataView dv = new DataView(dtProdTRN);
            dv.RowFilter = "UNIQUE_TRN=" + UNIQUE_TRN;
            if (dv.Count > 0)
            {

                DataTable data = GetLotData("");
                ddlLotNo.Items.Clear();
                ddlLotNo.DataSource = data;
                ddlLotNo.DataTextField = "LOT_NUMBER";
                ddlLotNo.DataValueField = "LOT_DATA";
                ddlLotNo.DataBind();
                ddlLotNo.Enabled = false;
                foreach (Obout.ComboBox.ComboBoxItem item in ddlLotNo.Items)
                {
                    if (item.Text == dv[0]["FR_BATCH_NO"].ToString())
                    {
                        //ddlLotNo.SelectedIndex = ddlLotNo.Items.IndexOf(item);
                        ddlLotNo.SelectedText = item.Text.ToString();
                        ddlLotNo.SelectedValue = item.Value.ToString();
                        string[] arrString = item.Value.Split('@');
                        // txtOrderNo.Text = arrString[0].ToString();

                        break;
                    }
                }

                ViewState["UNIQUE_TRN"] = UNIQUE_TRN;
                lblBATCH_NO.Text = dv[0]["FR_BATCH_NO"].ToString();
                txtLotQty.Text = dv[0]["LOT_QTY"].ToString();
                txtMaxLoadQty.Text = dv[0]["LOT_QTY"].ToString();
                txtUnloadQty.Text = dv[0]["UNLOAD_QTY"].ToString();
                txtUnloadNoOfUnit.Text = dv[0]["UNLOAD_NO_OF_UNIT"].ToString();
                txtUnloadWeightOfUnit.Text = dv[0]["UNLOAD_WEIGHT_OF_UNIT"].ToString();
                txtToBatchNo.Text = dv[0]["TO_BATCH_NO"].ToString();
                ddlPackaging.SelectedIndex = ddlPackaging.Items.IndexOf(ddlPackaging.Items.FindByValue(dv[0]["TO_LOCATION"].ToString()));
                txtToLocation.Text = dv[0]["TO_LOCATION"].ToString();
                txtGreyArticleCode.Text = dv[0]["ARTICLE_CODE"].ToString();
                txtGreyArticleDesc.Text = dv[0]["ARTICLE_DESC"].ToString();
                txtPattern.Text = dv[0]["PATTERN_NO"].ToString();
                txtOrderDescription.Text = dv[0]["ORDER_DESC"].ToString();
                ddlUnloadUOM.SelectedIndex = ddlUnloadUOM.Items.IndexOf(ddlUnloadUOM.Items.FindByValue(dv[0]["UNLOAD_UOM_OF_UNIT"].ToString()));


                lblPROS_CODE.Text = dv[0]["FR_PROS_CODE"].ToString();

                DataTable orderData = GetOrderItems();
                cmbOrderNo.Items.Clear();
                cmbOrderNo.DataSource = orderData;
                cmbOrderNo.DataTextField = "ORDER_NO";
                cmbOrderNo.DataValueField = "ORDER_NO";
                cmbOrderNo.DataBind();
                foreach (Obout.ComboBox.ComboBoxItem item in cmbOrderNo.Items)
                {
                    if (item.Text == dv[0]["ORDER_NO"].ToString())
                    {
                        cmbOrderNo.SelectedText = item.Text.ToString();
                        cmbOrderNo.SelectedValue = item.Value.ToString();

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
                
                if (Session["dtProdTRN"] == null)
                    dtProdTRN = CreateLotDetailTable();
                else
                    dtProdTRN = (DataTable)Session["dtProdTRN"];
                if (dtProdTRN != null && dtProdTRN.Rows.Count > 0)
                {
                    SaveProductionEntry();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear !!Please Select Atleast One Lot Detail Must !!");
                }
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
            oYRN_PROD_MST.PROS_CODE = ddlProsCode.SelectedValue.Trim();
            int processID = 0;
            int.TryParse(txtProsIdNo.Text, out processID);
            oYRN_PROD_MST.PROS_ID_NO = processID;
            oYRN_PROD_MST.MACHINE_CODE = txtMachineCode.Text;
            DateTime trnDate = DateTime.Now;
            DateTime.TryParse(txtEntryDate.Text.Trim(), out trnDate);
            oYRN_PROD_MST.TRN_DATE = trnDate;
            oYRN_PROD_MST.TRN_TYPE = TRN_TYPE;
            int shiftID = 0;
            int.TryParse(ddlShift.SelectedValue.Trim(), out shiftID);
            oYRN_PROD_MST.SFT_ID = shiftID;


            //DateTime loadingDate = DateTime.Now;
            //DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
            //var stime = TimeSpan.Parse(string.Format("{0}:{1}", startTime.Hour, startTime.Minute));
            //oYRN_PROD_MST.LOAD_DATE = loadingDate.Add(stime);

            //DateTime unloadingDate = DateTime.Now;
            //DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
            //var etime = TimeSpan.Parse(string.Format("{0}:{1}", endTime.Hour, endTime.Minute));
            //oYRN_PROD_MST.UNLOAD_DATE = unloadingDate.Add(etime);    

            DateTime loadingDate = DateTime.Now;
            DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
            DateTime times = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", startTime.Hour, startTime.Minute, startTime.Second, startTime.AmPm));
            var stime = TimeSpan.Parse(times.ToString("HH:mm:ss"));
            oYRN_PROD_MST.LOAD_DATE = loadingDate.Add(stime);

            DateTime unloadingDate = DateTime.Now;
            DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
            DateTime timee = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", endTime.Hour, endTime.Minute, endTime.Second, endTime.AmPm));
            var etime = TimeSpan.Parse(timee.ToString("HH:mm:ss"));
            oYRN_PROD_MST.UNLOAD_DATE = unloadingDate.Add(etime);

            CalcProcessTime();
            double machineStoppage = 0;
            double.TryParse(txtMachineStopage.Text.Trim(), out machineStoppage);
            oYRN_PROD_MST.STOP_TIME = machineStoppage;
            oYRN_PROD_MST.OPERATOR = txtOperator.Text.Trim();
            oYRN_PROD_MST.SUPERVISOR = txtSupervisor.Text.Trim(); ;
            oYRN_PROD_MST.REMARKS = txtRemarks.Text;
            oYRN_PROD_MST.DEL_STATUS = false;
            oYRN_PROD_MST.STATUS = true;
            oYRN_PROD_MST.TDATE = DateTime.Now.Date;
            oYRN_PROD_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_PROD_MST.AIR_PRESSURE = txtAirPressure.Text.Trim();
            oYRN_PROD_MST.PAPER_TUBE_COLOR = txtPaperTubeColor.Text.Trim();
            oYRN_PROD_MST.ROTO_JET_NO = txtRotoJetNo.Text.Trim();
            oYRN_PROD_MST.QUALITY = txtQuality.Text.Trim();
            double doffingnetWt = 0;
            double.TryParse(txtDoffingNetWt.Text, out doffingnetWt);
            oYRN_PROD_MST.DOFFING_NET_WT = doffingnetWt;
            oYRN_PROD_MST.MACHINE_SIDE = ddlSide.SelectedValue;

            oYRN_PROD_MST.LOT_NO = cmbLotNo.SelectedText;
            oYRN_PROD_MST.MERGE_NO = txtMergeNo.Text;
            oYRN_PROD_MST.LOT_TYPE = txtLotType.Text;
            oYRN_PROD_MST.FINISH_TYPE = txtFinishType.Text;

            int PROD_PROS_ID = 0;
            int.TryParse(txtProdProcessID.Text, out PROD_PROS_ID);
            oYRN_PROD_MST.PROD_PROS_ID_NO = PROD_PROS_ID;
            oYRN_PROD_MST.TYPE = MAIN_PROCESS;
            oYRN_PROD_MST.CARTON_CODE = "NA";
            oYRN_PROD_MST.PAPER_TUBE_CODE = "NA";
            double machineSpeed = 0;
            int totalDoffNo = 0;
            double.TryParse(txtMachineSpeed.Text, out machineSpeed);
            int.TryParse(txtTotalDoffNo.Text, out totalDoffNo);
            oYRN_PROD_MST.MACHINE_SPEED = machineSpeed;
            oYRN_PROD_MST.TOTAL_DOFF_NO = totalDoffNo;
            oYRN_PROD_MST.IS_GAIN = chkIsGain.Checked?"1":"0";
            oYRN_PROD_MST.GAIN_QTY = txtGainQty.Text;
            string msg = string.Empty;
            int PROS_ID_NO = 0;
            DataTable dtMachineStopDataTable = new DataTable();
            if (Session["dtMachineStopDataTable"] != null)
            {
                dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];
            }
            DataTable dtItemReceipt = new DataTable();
            if (Session["dtItemReceipt"] != null)
            {
                dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            }
            else
            {
                if (!chkIsGain.Checked)
                {
                    Common.CommonFuction.ShowMessage("please adjust poy machine to issue.");
                    return;
                }

            }


            bool result = SaitexBL.Interface.Method.YRN_PROD_MST.Insert_DOFF_YS(oYRN_PROD_MST, out PROS_ID_NO, dtProdTRN, out msg, dtMachineStopDataTable, dtItemReceipt);
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

           

            int doffno = 0;
            int.TryParse(txtTotalDoffNo.Text, out doffno);
            
            iCountAll = iCountAll + 1;
            if (doffno > 0)
            {
                iCount = iCount + 1;
            }
            else 
            {
                msg = "Please enter doff no!"; 
            }

            if (chkIsGain.Checked )
            {
                iCountAll = iCountAll + 1;

                double gainqty = 0;
                double.TryParse(txtGainQty.Text, out gainqty);
                if (gainqty > 0)
                {
                    iCount = iCount + 1;
                }
                else
                {
                    msg = "Please enter process gain qty in poy adjustment!";
                }   
            }

            

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
            string commandText = "SELECT * FROM (select PROS_ID_NO,PROS_CODE,TRN_TYPE,MACHINE_CODE,DEPT_CODE,SFT_ID,(YEAR ||'@'|| COMP_CODE ||'@'|| BRANCH_CODE ||'@'||  TRN_TYPE ||'@'|| PROS_CODE ||'@'|| PROS_ID_NO ||'@'|| MACHINE_CODE )PROS_DATA from YRN_PROD_DOFF_MST WHERE DEPT_CODE='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'   AND BRANCH_CODE =       '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND TRN_TYPE='" + TRN_TYPE + "') ASD ";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

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

            if (Session["dtProdTRN"] == null)
                dtProdTRN = CreateLotDetailTable();
            else
                dtProdTRN = (DataTable)Session["dtProdTRN"];
            //dtProdColorChem.Rows.Clear();
            int iRecordFound = GetdataByProsIdNo(Common.CommonFuction.funFixQuotes(ddlProsIdNo.SelectedValue.Trim()));
            if (iRecordFound > 0)
            {
                txtProsIdNo.Text = ddlProsIdNo.SelectedText;
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
            oYRN_PROD_MST.TYPE = MAIN_PROCESS;
            oYRN_PROD_MST.PROS_ID_NO = int.Parse(sPROS_ID_NO);
            lblProsCode.Text = sPROS_CODE;
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetProdDoffDataByPROS_ID_NO(oYRN_PROD_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtProsIdNo.Text = dt.Rows[0]["PROS_ID_NO"].ToString().Trim();
                txtEntryDate.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByValue(dt.Rows[0]["SFT_ID"].ToString().Trim()));


                DateTime dtloaddate = DateTime.Now;
                DateTime.TryParse(dt.Rows[0]["LOAD_DATE"].ToString(), out dtloaddate);
                txtLoadingDate.Text = dtloaddate.ToString("dd/MM/yyyy");

                DateTime dtunloaddate = DateTime.Now;
                DateTime.TryParse(dt.Rows[0]["UNLOAD_DATE"].ToString(), out dtunloaddate);
                txtUnLoadingDate.Text = dtunloaddate.ToString("dd/MM/yyyy");
                //startTime.Hour = dtloaddate.Hour;
                //startTime.Minute = dtloaddate.Minute;
                //endTime.Hour = dtunloaddate.Hour;
                //endTime.Minute = dtunloaddate.Minute;

                MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                if (dtloaddate.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                startTime.SetTime(dtloaddate.Hour, dtloaddate.Minute, am_pm);

                if (dtunloaddate.ToString("tt") == "AM")
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                }
                else
                {
                    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                }
                endTime.SetTime(dtunloaddate.Hour, dtunloaddate.Minute, am_pm);


                txtMachineStopage.Text = dt.Rows[0]["STOP_TIME"].ToString().Trim();
                CalcProcessTime();

                txtOperator.Text = dt.Rows[0]["OPERATOR"].ToString().Trim();
                txtSupervisor.Text = dt.Rows[0]["SUPERVISOR"].ToString().Trim();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString().Trim();
                lblProsCode.Text = dt.Rows[0]["PROS_CODE"].ToString().Trim();
                txtPaperTubeColor.Text = dt.Rows[0]["PAPER_TUBE_COLOR"].ToString();
                txtDoffingNetWt.Text = dt.Rows[0]["DOFFING_NET_WT"].ToString();
                txtAirPressure.Text = dt.Rows[0]["AIR_PRESSURE"].ToString();
                txtRotoJetNo.Text = dt.Rows[0]["ROTO_JET_NO"].ToString();
                txtQuality.Text = dt.Rows[0]["QUALITY"].ToString();


                txtMergeNo.Text = dt.Rows[0]["MERGE_NO"].ToString();
                txtLotType.Text = dt.Rows[0]["LOT_TYPE"].ToString();
                txtFinishType.Text = dt.Rows[0]["FINISH_TYPE"].ToString();
                txtProdProcessID.Text = dt.Rows[0]["PROD_PROS_ID_NO"].ToString();

                lblTotalDoff.Text = dt.Rows[0]["TOTAL_NO_OF_DOFF"].ToString();
                lblTotalDoffWt.Text = dt.Rows[0]["TOTAL_DOFF_WEIGHT"].ToString();
                lblTotalCops.Text = dt.Rows[0]["TOTAL_COPS"].ToString();

                txtMachineSpeed.Text = dt.Rows[0]["MACHINE_SPEED"].ToString();
                txtTotalDoffNo.Text = dt.Rows[0]["TOTAL_DOFF_NO"].ToString();

                
               
                txtGainQty.Text  = dt.Rows[0]["GAIN_QTY"].ToString();
                if ( dt.Rows[0]["IS_GAIN"].ToString().Equals("1"))
                {
                    chkIsGain.Checked = true;
                }
                else
                {
                    chkIsGain.Checked = false;
                }
                DataTable dataLot = GetLotNoItems();
                cmbLotNo.DataSource = dataLot;
                cmbLotNo.DataTextField = "LOT_NO";
                cmbLotNo.DataValueField = "LOT_DATA";
                cmbLotNo.DataBind();
                foreach (Obout.ComboBox.ComboBoxItem item in cmbLotNo.Items)
                {
                    if (item.Text == dt.Rows[0]["LOT_NO"].ToString().Trim())
                    {
                        //cmbLotNo.SelectedIndex = ddlMachineCode.Items.IndexOf(item);
                        cmbLotNo.SelectedText = item.Text.ToString();
                        cmbLotNo.SelectedValue = item.Value.ToString();
                        txtToBatchNo.Text = item.Text.ToString();
                        string[] arrString = cmbLotNo.SelectedValue.Split('@');
                        txtGreyArticleCode.Text = arrString[3].ToString();
                        txtGreyArticleDesc.Text = arrString[4].ToString();
                        txtPattern.Text = arrString[6].ToString();
                        txtOrderDescription.Text = arrString[7].ToString();
                        break;
                    }
                }

                ddlSide.SelectedIndex = ddlSide.Items.IndexOf(ddlSide.Items.FindByValue(dt.Rows[0]["MACHINE_SIDE"].ToString()));
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
                        ddlMachineCode.SelectedText = txtMachineCode.Text = item.Text.ToString();
                        ddlMachineCode.SelectedValue = item.Value.ToString();
                        ddlMachineCode.Enabled = true;
                        break;
                    }
                }

                DataTable data = GetProsData("");
                ddlProsCode.DataSource = data;
                ddlProsCode.DataTextField = "PROS_DESC";
                ddlProsCode.DataValueField = "PROS_CODE";
                ddlProsCode.DataBind();
                foreach (Obout.ComboBox.ComboBoxItem item in ddlProsCode.Items)
                {
                    if (item.Value == dt.Rows[0]["PROS_CODE"].ToString().Trim())
                    {
                        //ddlProsCode.SelectedIndex = ddlProsCode.Items.IndexOf(item);
                        ddlProsCode.SelectedText = item.Text.ToString();
                        ddlProsCode.SelectedValue = item.Value.ToString();
                        txtProsDesc.Text = item.Text.Trim();
                        ddlProsCode.Enabled = true;
                        break;
                    }
                }
            }


            if (iRecordFound == 1)
            {
                DataTable dtTemp_ProdTRN = SaitexBL.Interface.Method.YRN_PROD_MST.Get_DOFF_TRN_ByProsIdNo(oYRN_PROD_MST);
                DataTable dtMachineStopDataTable = SaitexBL.Interface.Method.YRN_PROD_MST.GetMachineStopData(oYRN_PROD_MST);

                Session["dtMachineStopDataTable"] = dtMachineStopDataTable;

                if (dtTemp_ProdTRN != null && dtTemp_ProdTRN.Rows.Count > 0)
                {
                    MapDataTable_ProdTRN(dtTemp_ProdTRN);
                    BindLotDetailGrid();

                    if (dtTemp_ProdTRN != null && dtTemp_ProdTRN.Rows.Count > 0)
                    {
                        DataTable dtReceiptAdjustment = SaitexBL.Interface.Method.YRN_PROD_MST.GetProductionIssueAdjustment(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE, int.Parse(txtProsIdNo.Text), ddlProsCode.SelectedValue);
                        MapAdjustDataTable(dtReceiptAdjustment);
                        Session["dtItemReceipt"] = dtReceiptAdjustment;

                    }
                }
            }

            return iRecordFound;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MapAdjustDataTable(DataTable dtReceiptAdjustment)
    {
        try
        {

            if (!dtReceiptAdjustment.Columns.Contains("UNIQUEID"))
                dtReceiptAdjustment.Columns.Add("UNIQUEID", typeof(double));



            for (int iLoop = 0; iLoop < dtReceiptAdjustment.Rows.Count; iLoop++)
            {
                dtReceiptAdjustment.Rows[iLoop]["UNIQUEID"] = iLoop + 1;

            }
        }
        catch
        {
            throw;
        }
    }
    private void MapDataTable_ProdTRN(DataTable dtTemp)
    {
        try
        {

            if (Session["dtProdTRN"] == null)
                dtProdTRN = CreateLotDetailTable();
            else
                dtProdTRN = (DataTable)Session["dtProdTRN"];

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtProdTRN.NewRow();
                    dr["UNIQUE_TRN"] = dtProdTRN.Rows.Count + 1;
                    dr["ORDER_NO"] = drTemp["ORDER_NO"];
                    dr["LOT_NUMBER"] = drTemp["LOT_NUMBER"];
                    dr["LOT_QTY"] = drTemp["LOT_QTY"];
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
                    dr["PATTERN_NO"] = drTemp["PATTERN_NO"];
                    dr["EFFICIENCY"] = drTemp["EFFICIENCY"];
                    dr["ARTICLE_CODE"] = drTemp["ARTICLE_CODE"];
                    dr["ARTICLE_DESC"] = drTemp["ARTICLE_DESC"];
                    dr["REJECTIONS_OF_UNIT"] = drTemp["REJECTIONS_OF_UNIT"];
                    dr["WEIGHT_OF_REJECTION"] = drTemp["WEIGHT_OF_REJECTION"];
                    dr["ORDER_DESC"] = drTemp["ORDER_DESC"];
                    dtProdTRN.Rows.Add(dr);
                }
                Session["dtProdTRN"] = dtProdTRN;
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
            int processID = 0;
            int.TryParse(ddlProsIdNo.SelectedText, out processID);
            //int.TryParse(txtProsIdNo.Text, out processID);
            oYRN_PROD_MST.PROS_ID_NO = processID;
            oYRN_PROD_MST.MACHINE_CODE = txtMachineCode.Text; ;

            DateTime trnDate = DateTime.Now;
            DateTime.TryParse(txtEntryDate.Text.Trim(), out trnDate);
            oYRN_PROD_MST.TRN_DATE = trnDate;

            oYRN_PROD_MST.TRN_TYPE = TRN_TYPE;

            int shiftID = 0;
            int.TryParse(ddlShift.SelectedValue.Trim(), out shiftID);
            oYRN_PROD_MST.SFT_ID = shiftID;

            //DateTime loadingDate = DateTime.Now;
            //DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
            //var stime = TimeSpan.Parse(string.Format("{0}:{1}", startTime.Hour, startTime.Minute));
            //oYRN_PROD_MST.LOAD_DATE = loadingDate.Add(stime);

            //DateTime unloadingDate = DateTime.Now;
            //DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
            //var etime = TimeSpan.Parse(string.Format("{0}:{1}", endTime.Hour, endTime.Minute));
            //oYRN_PROD_MST.UNLOAD_DATE = unloadingDate.Add(etime); 

            DateTime loadingDate = DateTime.Now;
            DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
            DateTime times = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", startTime.Hour, startTime.Minute, startTime.Second, startTime.AmPm));
            var stime = TimeSpan.Parse(times.ToString("HH:mm:ss"));
            oYRN_PROD_MST.LOAD_DATE = loadingDate.Add(stime);



            DateTime unloadingDate = DateTime.Now;
            DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
            DateTime timee = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", endTime.Hour, endTime.Minute, endTime.Second, endTime.AmPm));
            var etime = TimeSpan.Parse(timee.ToString("HH:mm:ss"));
            oYRN_PROD_MST.UNLOAD_DATE = unloadingDate.Add(etime);

            CalcProcessTime();
            double MachineStopage = 0;
            double.TryParse(txtMachineStopage.Text.Trim(), out MachineStopage);
            oYRN_PROD_MST.STOP_TIME = MachineStopage;
            oYRN_PROD_MST.OPERATOR = txtOperator.Text.Trim();
            oYRN_PROD_MST.SUPERVISOR = txtSupervisor.Text.Trim();
            oYRN_PROD_MST.REMARKS = txtRemarks.Text;
            oYRN_PROD_MST.DEL_STATUS = false;
            oYRN_PROD_MST.STATUS = true;
            oYRN_PROD_MST.TDATE = DateTime.Now.Date;
            oYRN_PROD_MST.TUSER = oUserLoginDetail.UserCode;
            oYRN_PROD_MST.AIR_PRESSURE = txtAirPressure.Text.Trim();
            oYRN_PROD_MST.PAPER_TUBE_COLOR = txtPaperTubeColor.Text.Trim();
            oYRN_PROD_MST.ROTO_JET_NO = txtRotoJetNo.Text.Trim();
            oYRN_PROD_MST.QUALITY = txtQuality.Text.Trim();
            double doffingnetWt = 0;
            double.TryParse(txtDoffingNetWt.Text, out doffingnetWt);
            oYRN_PROD_MST.DOFFING_NET_WT = doffingnetWt;
            oYRN_PROD_MST.MACHINE_SIDE = ddlSide.SelectedValue;
            oYRN_PROD_MST.LOT_NO = cmbLotNo.SelectedText;
            oYRN_PROD_MST.MERGE_NO = txtMergeNo.Text;
            oYRN_PROD_MST.LOT_TYPE = txtLotType.Text;
            oYRN_PROD_MST.FINISH_TYPE = txtFinishType.Text;
            int PROD_PROS_ID = 0;
            int.TryParse(txtProdProcessID.Text, out PROD_PROS_ID);
            oYRN_PROD_MST.PROD_PROS_ID_NO = PROD_PROS_ID;
            oYRN_PROD_MST.TYPE = MAIN_PROCESS;
            oYRN_PROD_MST.CARTON_CODE = "NA";
            oYRN_PROD_MST.PAPER_TUBE_CODE = "NA";
            double machineSpeed = 0;
            int totalDoffNo = 0;
            double.TryParse(txtMachineSpeed.Text, out machineSpeed);
            int.TryParse(txtTotalDoffNo.Text, out totalDoffNo);
            oYRN_PROD_MST.MACHINE_SPEED = machineSpeed;
            oYRN_PROD_MST.TOTAL_DOFF_NO = totalDoffNo;
            oYRN_PROD_MST.IS_GAIN = chkIsGain.Checked ? "1" : "0";
            oYRN_PROD_MST.GAIN_QTY = txtGainQty.Text;
            string msg = string.Empty;
            DataTable dtMachineStopDataTable = new DataTable();
            if (Session["dtMachineStopDataTable"] != null)
            {
                dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];
            }
            if (Session["dtProdTRN"] == null)
                dtProdTRN = CreateLotDetailTable();
            else
                dtProdTRN = (DataTable)Session["dtProdTRN"];

            DataTable dtItemReceipt = new DataTable();
            if (Session["dtItemReceipt"] != null)
            {
                dtItemReceipt = (DataTable)Session["dtItemReceipt"];
            }
            else
            {
                if (!chkIsGain.Checked )
                {
                    Common.CommonFuction.ShowMessage("please adjust poy machine to issue.");
                    return;
                }

            }
            bool result = SaitexBL.Interface.Method.YRN_PROD_MST.Update_DOFF_YS(oYRN_PROD_MST, dtProdTRN, out msg, dtMachineStopDataTable, dtItemReceipt);
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

            //string URL = "../../Production/Queries/PrintProductionDoff.aspx?TRN_TYPE=" + TRN_TYPE;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
            Response.Redirect("../../Production/Queries/PrintProductionDoff.aspx?TRN_TYPE=" + TRN_TYPE, false);

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
    protected void lbtnmacStopDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlMachineCode.SelectedIndex != -1)
            {
                string URL = "MachineStop.aspx";
                DateTime loadingDate = DateTime.Now;
                DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
                DateTime times = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", startTime.Hour, startTime.Minute, startTime.Second, startTime.AmPm));
                var stime = TimeSpan.Parse(times.ToString("HH:mm:ss"));
                var loaddetails = loadingDate.Add(stime);

                DateTime unloadingDate = DateTime.Now;
                DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
                DateTime timee = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", endTime.Hour, endTime.Minute, endTime.Second, endTime.AmPm));
                var etime = TimeSpan.Parse(timee.ToString("HH:mm:ss"));
                var unloaddetails = unloadingDate.Add(etime);
                CalcProcessTime();
                URL = URL + "?TextBoxId=" + txtMachineStopage.ClientID;
                URL = URL + "&MAC_CODE=" + txtMachineCode.Text; ;
                URL = URL + "&MAC_LOAD_TIME=" + loaddetails.ToString();
                URL = URL + "&MAC_UNLOAD_TIME=" + unloaddetails.ToString();
                URL = URL + "&PROD_PROS_ID_NO=" + txtProdProcessID.Text;
                URL = URL + "&LOT_NO=" + cmbLotNo.SelectedText;
                if (ddlProsCode.SelectedValue.Trim() == string.Empty)
                {
                    URL = URL + "&PROS_CODE=" + lblProsCode.Text.Trim();
                }
                else
                {
                    URL = URL + "&PROS_CODE=" + ddlProsCode.SelectedValue.Trim();
                }

                txtMachineStopage.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=700,height=320,left=200,top=300');", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Machine");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void CalculateUnloadQty()
    {
        try
        {
            double TotalUnloadQty = 0;
            if (Session["dtProdTRN"] != null)
            {
                dtProdTRN = (DataTable)Session["dtProdTRN"];
            }
            if (dtProdTRN != null && dtProdTRN.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProdTRN.Rows)
                {
                    double unloadQty = 0;
                    double.TryParse(dr["UNLOAD_QTY"].ToString(), out unloadQty);
                    TotalUnloadQty += unloadQty;
                }
            }
            txtDoffingNetWt.Text = Math.Round(TotalUnloadQty, 3).ToString();
        }
        catch
        {

            throw;
        }
    }

    protected void cmbLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLotNoItems(e.Text.ToUpper(), e.ItemsOffset);
            if (data != null && data.Rows.Count > 0)
            {
                cmbLotNo.Items.Clear();
                cmbLotNo.DataSource = data;
                cmbLotNo.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetLotCounts(e.Text);

        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }
    private DataTable GetLotNoItems(string text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT DISTINCT * FROM (SELECT DISTINCT  LOT_NO,LOT_TYPE,PURPOSE,POY,POY_DESC,MERGE_NO,FINISHED_DENIER,FINISHED_DENIER_DESC,FINISHED_TYPE,JET_NO,MACHINE_NAME,(   LOT_NO|| '@'|| LOT_TYPE|| '@'||      PURPOSE|| '@'|| POY|| '@'|| POY_DESC|| '@'||  MERGE_NO|| '@'||      FINISHED_DENIER|| '@' || FINISHED_DENIER_DESC|| '@'|| FINISHED_TYPE||      '@'|| JET_NO|| '@'|| MACHINE_NAME|| '@'|| MACHINE_SPEED       || '@'  || PLT     || '@' || ROTO_PRESSURE    || '@'     || DOFF_TIME      || '@'   || DOFF_WEIGHT )         LOT_DATA FROM   V_YRN_LOT_MAKING WHERE    PRODUCT_CATEGORY='" + MAIN_PROCESS + "'  AND  ( UPPER(LOT_NO) LIKE :SearchQuery OR  UPPER(POY) LIKE :SearchQuery OR  UPPER(   POY_DESC) LIKE :SearchQuery OR  UPPER(MERGE_NO) LIKE :SearchQuery OR      UPPER(FINISHED_DENIER) LIKE :SearchQuery OR  UPPER(FINISHED_DENIER_DESC)      LIKE :SearchQuery OR  UPPER(MACHINE_NAME) LIKE :SearchQuery ))    WHERE  ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND  LOT_NO NOT IN  ( SELECT LOT_NO FROM (SELECT DISTINCT  LOT_NO FROM   V_YRN_LOT_MAKING WHERE    PRODUCT_CATEGORY='" + MAIN_PROCESS + "'  AND  ( UPPER(LOT_NO) LIKE :SearchQuery OR  UPPER(POY) LIKE :SearchQuery OR  UPPER(   POY_DESC) LIKE :SearchQuery OR  UPPER(MERGE_NO) LIKE :SearchQuery OR      UPPER(FINISHED_DENIER) LIKE :SearchQuery OR  UPPER(FINISHED_DENIER_DESC)      LIKE :SearchQuery OR  UPPER(MACHINE_NAME) LIKE :SearchQuery )) WHERE  ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by LOT_NO";
            string SearchQuery = "%" + text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }

        catch
        {

            throw;
        }
    }


    private int GetLotCounts(string text)
    {
        try
        {

            string CommandText = " SELECT DISTINCT  LOT_NO  FROM   V_YRN_LOT_MAKING WHERE    PRODUCT_CATEGORY='" + MAIN_PROCESS + "' AND  ( UPPER(LOT_NO) LIKE :SearchQuery OR  UPPER(POY) LIKE :SearchQuery OR  UPPER(   POY_DESC) LIKE :SearchQuery OR  UPPER(MERGE_NO) LIKE :SearchQuery OR      UPPER(FINISHED_DENIER) LIKE :SearchQuery OR  UPPER(FINISHED_DENIER_DESC)      LIKE :SearchQuery OR  UPPER(MACHINE_NAME) LIKE :SearchQuery ) ";
            string whereClause = string.Empty;
            string SortExpression = " order by LOT_NO";
            string SearchQuery = "%" + text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch
        {

            throw;
        }
    }
    private DataTable GetLotNoItems()
    {
        try
        {
            string CommandText = "SELECT DISTINCT  LOT_NO,LOT_TYPE,PURPOSE,POY,POY_DESC,MERGE_NO,FINISHED_DENIER,FINISHED_DENIER_DESC,FINISHED_TYPE,JET_NO,MACHINE_NAME,(   LOT_NO|| '@'|| LOT_TYPE|| '@'||      PURPOSE|| '@'|| POY|| '@'|| POY_DESC|| '@'||  MERGE_NO|| '@'||      FINISHED_DENIER|| '@' || FINISHED_DENIER_DESC|| '@'|| FINISHED_TYPE||      '@'|| JET_NO|| '@'|| MACHINE_NAME || '@'|| MACHINE_SPEED       || '@'  || PLT     || '@' || ROTO_PRESSURE    || '@'     || DOFF_TIME      || '@'   || DOFF_WEIGHT)         LOT_DATA FROM   V_YRN_LOT_MAKING WHERE    PRODUCT_CATEGORY='" + MAIN_PROCESS + "'  AND  ( UPPER(LOT_NO) LIKE :SearchQuery OR  UPPER(POY) LIKE :SearchQuery OR  UPPER(   POY_DESC) LIKE :SearchQuery OR  UPPER(MERGE_NO) LIKE :SearchQuery OR      UPPER(FINISHED_DENIER) LIKE :SearchQuery OR  UPPER(FINISHED_DENIER_DESC)      LIKE :SearchQuery OR  UPPER(MACHINE_NAME) LIKE :SearchQuery )    ";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", " order by LOT_NO", "", "%", "");
        }

        catch
        {

            throw;
        }
    }
    protected void cmbLotNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        if (cmbLotNo.SelectedIndex != -1)
        {




            string[] arrString = cmbLotNo.SelectedValue.Split('@');
            txtToBatchNo.Text = arrString[0].ToString();//LOT_NO  
            txtLotType.Text = arrString[1].ToString();//LOT_TYPE
            txtQuality.Text = arrString[2].ToString();//PURPOSE       
            txtGreyArticleCode.Text = arrString[3].ToString();
            txtGreyArticleDesc.Text = arrString[4].ToString();

            txtMergeNo.Text = arrString[5].ToString();//MERGE_NO     
            txtPattern.Text = arrString[6].ToString();
            txtOrderDescription.Text = arrString[7].ToString();
            txtFinishType.Text = arrString[8].ToString();//FINISHED_TYPE    
            txtRotoJetNo.Text = arrString[9].ToString();//JET_NO
            txtMachineSpeed.Text = arrString[11].ToString();
            txtPaperTubeColor.Text = arrString[12].ToString();
            txtAirPressure.Text = arrString[13].ToString();
            txtProcessTime.Text = arrString[14].ToString();
            txtDoffingNetWt.Text = arrString[15].ToString();






            //lblTotalDoff.Text = ;
            //lblTotalDoffWt.Text = ;

            //lblTotalCops.Text = ;
            // txtProdProcessID.Text = arrString[1].ToString();//Production Process ID  
            //txtLoadingDate.Text = arrString[4].ToString();
            //txtOperator.Text = arrString[6].ToString();
            //txtSupervisor.Text = arrString[7].ToString();
            //txtRemarks.Text = arrString[8].ToString();

            DataTable dataMac = GetMachineData("");
            ddlMachineCode.DataSource = dataMac;
            ddlMachineCode.DataTextField = "MACHINE_CODE";
            ddlMachineCode.DataValueField = "MACHINE_DATA";
            ddlMachineCode.DataBind();
            foreach (Obout.ComboBox.ComboBoxItem item in ddlMachineCode.Items)
            {
                if (item.Text == arrString[10].ToString())
                {
                    ddlMachineCode.SelectedIndex = ddlMachineCode.Items.IndexOf(item);
                    txtMachineDesc.Text = item.Value.Trim();
                    ddlMachineCode.SelectedText = txtMachineCode.Text = item.Text.ToString();
                    ddlMachineCode.SelectedValue = item.Value.ToString();
                    string[] arrString1 = ddlMachineCode.SelectedValue.Split('@');
                    txtMachineDesc.Text = arrString1[0].ToString();
                    // ddlMachineCode.Enabled = false;
                    break;
                }
            }

            ddlProsCode.DataSource = GetProsData("");
            ddlProsCode.DataTextField = "PROS_DESC";
            ddlProsCode.DataValueField = "PROS_CODE";
            ddlProsCode.DataBind();
            foreach (Obout.ComboBox.ComboBoxItem item in ddlProsCode.Items)
            {
                if (item.Value == "TEXALI001")
                {
                    ddlProsCode.SelectedIndex = ddlProsCode.Items.IndexOf(item);
                    ddlProsCode.SelectedText = item.Text.ToString();
                    ddlProsCode.SelectedValue = item.Value.ToString();
                    ddlProsCode.Enabled = false;
                    break;
                }
            }
            txtProsDesc.Text = ddlProsCode.SelectedText.Trim();
            lblProsCode.Text = ddlProsCode.SelectedValue.Trim();
            BindProsIdNo();
            //* BELOW SECTION IS COMMENTED DUE TO CHANGE DOFF ENTRY TO WHOLE DAY DOFF DETAILS ENTRY AT SINGLE TIME**//
            //getDoffDetails();
            //* Above SECTION IS COMMENTED DUE TO CHANGE DOFF ENTRY TO WHOLE DAY DOFF DETAILS ENTRY AT SINGLE TIME**//

            cmbOrderNo.SelectedIndex = -1;


        }
    }



    protected void getDoffDetails()
    {


        DataTable DoffTimeDetails = SaitexBL.Interface.Method.YRN_PROD_MST.Get_Doff_Start_End_Time(ddlMachineCode.SelectedText, cmbLotNo.SelectedText, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, txtProdProcessID.Text);
        if (DoffTimeDetails.Rows.Count > 0)
        {

            DateTime dtloaddate = DateTime.Now;
            DateTime.TryParse(DoffTimeDetails.Rows[0]["NEXT_LOT_DOFF_START_TIME"].ToString(), out dtloaddate);
            txtLoadingDate.Text = dtloaddate.ToString("dd/MM/yyyy");


            DateTime dtunloaddate = DateTime.Now;
            DateTime.TryParse(DoffTimeDetails.Rows[0]["NEXT_LOT_DOFF_END_TIME"].ToString(), out dtunloaddate);
            txtUnLoadingDate.Text = dtunloaddate.ToString("dd/MM/yyyy");

            MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
            if (dtloaddate.ToString("tt") == "AM")
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
            }
            else
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
            }
            startTime.SetTime(dtloaddate.Hour, dtloaddate.Minute, am_pm);

            if (dtunloaddate.ToString("tt") == "AM")
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
            }
            else
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
            }
            endTime.SetTime(dtunloaddate.Hour, dtunloaddate.Minute, am_pm);

            lblTotalCops.Text = DoffTimeDetails.Rows[0]["TOTAL_COPS"].ToString();

        }
    }

    protected void chkProduction_CheckedChanged(object sender, EventArgs e)
    {
        ClearPage();
    }
    protected void cmbOrderNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetOrderItems(e.Text.ToUpper(), e.ItemsOffset);
            if (data != null && data.Rows.Count > 0)
            {
                cmbOrderNo.Items.Clear();
                cmbOrderNo.DataSource = data;
                cmbOrderNo.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetOrderCounts(e.Text);

        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Details loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetOrderItems(string text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT DISTINCT * FROM (select  PI_NO ORDER_NO,ASS_ARTICAL_CODE ,ASS_ARTICAL_DESC,SHADE_CODE,NVL(REQ_QTY,0) REQ_QTY,NVL(PRODUCTION_QTY,0) PRODUCTION_QTY FROM OD_CAPT_TRN_BOM WHERE ARTICAL_TYPE='TEXTURISED' AND (NVL(REQ_QTY,0)-NVL(PRODUCTION_QTY,0))>0 AND COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND ASS_ARTICAL_CODE='" + txtPattern.Text + "' AND UPPER(ASS_ARTICAL_DESC)      LIKE  :SearchQuery )    WHERE  ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND  PI_NO NOT IN  ( select DISTINCT PI_NO  FROM OD_CAPT_TRN_BOM WHERE ARTICAL_TYPE='TEXTURISED' AND (NVL(REQ_QTY,0)-NVL(PRODUCTION_QTY,0))>0 AND COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND ASS_ARTICAL_CODE='" + txtPattern.Text + "' AND UPPER(ASS_ARTICAL_DESC)      LIKE  :SearchQuery) WHERE  ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = "";
            string SearchQuery = "%" + text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }

        catch
        {

            throw;
        }
    }

    private int GetOrderCounts(string text)
    {
        try
        {

            string CommandText = " select DISTINCT PI_NO  FROM OD_CAPT_TRN_BOM WHERE ARTICAL_TYPE='TEXTURISED' AND (NVL(REQ_QTY,0)-NVL(PRODUCTION_QTY,0))>0 AND COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND ASS_ARTICAL_CODE='" + txtPattern.Text + "' AND UPPER(ASS_ARTICAL_DESC)      LIKE  :SearchQuery ";
            string whereClause = string.Empty;
            string SortExpression = "";
            string SearchQuery = "%" + text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch
        {

            throw;
        }
    }

    private DataTable GetOrderItems()
    {
        try
        {
            string CommandText = "select  PI_NO ORDER_NO,ASS_ARTICAL_CODE ,ASS_ARTICAL_DESC,SHADE_CODE,NVL(REQ_QTY,0) REQ_QTY,NVL(PRODUCTION_QTY,0) PRODUCTION_QTY FROM OD_CAPT_TRN_BOM WHERE ARTICAL_TYPE='TEXTURISED' AND (NVL(REQ_QTY,0)-NVL(PRODUCTION_QTY,0))>0 AND COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND ASS_ARTICAL_CODE='" + txtPattern.Text + "' AND UPPER(ASS_ARTICAL_DESC)      LIKE  :SearchQuery   ";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", " ", "", "%", "");
        }

        catch
        {

            throw;
        }
    }


    protected void btnAdjRec_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtGreyArticleCode.Text != "" && txtUnloadQty.Text != "")
            {
                string URL = "Issue_To_Machine_Adjustment.aspx";
                URL = URL + "?ItemCodeId=" + txtGreyArticleCode.Text.Trim();
                URL = URL + "&TRN_TYPE=" + TRN_TYPE;
                if (imgbtnSave.Visible == true)
                {
                    BindProsIdNo();
                }
                URL = URL + "&ChallanNo=" + txtProsIdNo.Text.Trim();
                URL = URL + "&MAX_QTY=" + txtUnloadQty.Text;
                URL = URL + "&PI_NO=" + cmbOrderNo.SelectedValue;
                URL = URL + "&TRN_DATE=" + txtEntryDate.Text;
                URL = URL + "&LOT_NUMBER=" + lblBATCH_NO.Text;
                URL = URL + "&DOFF_LOT_NUMBER=" + cmbLotNo.SelectedText;               
                URL = URL + "&GAIN_QTY_ID=" + txtGainQty.ClientID;
                if (chkIsGain.Checked)
                {
                    URL = URL + "&IS_GAIN=1";
                    URL = URL + "&GAIN_QTY=" + txtGainQty.Text;
                }
                else
                {
                    URL = URL + "&IS_GAIN=0";
                    URL = URL + "&GAIN_QTY=0";
                }
                //URL = URL + "&IS_GAIN=" + hdnIsProcessGain.Value ;
                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=1000,height=500,left=50,top=50');", true);


            }
            else
            {
                CommonFuction.ShowMessage("Please enter prod qty  to adjust machine issue.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Adjustment Window Opening.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void chkIsGain_CheckedChanged(object sender, EventArgs e)
    {
        ddlLotNo.SelectedIndex = -1;
        RefreshProdTRN();

        if (chkIsGain.Checked)
        {
            txtGainQty.Visible = true;
            lblProcessGain.Visible = true;

        }
        else
        {            
            txtGainQty.Visible = false;
            lblProcessGain.Visible = false;
        }
    }
    
}
