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
public partial class Module_Production_Controls_YarnIssueToMachineTwisting : System.Web.UI.UserControl
{
    private  DataTable dtProdTRN;
    //private  DataTable dtProdColorChem;
    SaitexDM.Common.DataModel.YRN_PROD_MST oYRN_PROD_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public   string TRN_TYPE{get;set;} 
    public   string PRODUCT_TYPE{get;set;} 
    public   string MAIN_PROCESS{get;set;}
    public   string PROS_CODE{get;set;}
  
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
            Session["dtMachineStopDataTable"] = null;
            Session["dtProdTRN"] = null;
            Session["dtProdColorChem"] = null;
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
            cmbMachineGroup.SelectedIndex = -1;
            txtMachineDesc.Text = string.Empty;
            txtProsIdNo.Text = string.Empty;
            txtEntryDate.Text = System.DateTime.Now.Date.ToShortDateString();
            ddlShift.SelectedIndex = -1;
            var todayDate = DateTime.Now;
            txtLoadingDate.Text = todayDate.ToString("dd/MM/yyyy");
            txtUnLoadingDate.Text = string.Empty;
            txtMachineStopage.Text ="0";
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
            txtOrderNo.Text = string.Empty;
            txtOrderDescription.Text = string.Empty;
            txtLoadQty.Text = string.Empty;
            txtUnloadQty.Text = string.Empty;
            txtToLocation.Text = string.Empty;
            txtToBatchNo.Text = string.Empty;
            ddlPackaging.SelectedIndex = -1;
            txtLoadNoOfUnit.Text = "0";
            ddlLoadUOM.SelectedIndex = -1;
            txtLoadWeightOfUnit.Text = "0";
            txtUnloadNoOfUnit.Text = "0";
            ddlUnloadUOM.SelectedIndex = -1;
            txtUnloadWeightOfUnit.Text = "0";
            txtPattern.Text = string.Empty;
            txtGreyArticleCode.Text = string.Empty;
            txtDyedLot.Text = string.Empty;
            txtGreyArticleDesc.Text = string.Empty;
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

                txtMachineDesc.Text = arrString[0].ToString();
                if (!arrString[1].ToString().Equals(string.Empty))
                {
                    txtLoadingDate.Text = DateTime.Parse(arrString[1].ToString()).ToString();
                    txtUnLoadingDate.Text = DateTime.Parse(arrString[1].ToString()).ToString();
                }
            }
            ddlLotNo.SelectedIndex = -1;
            getMachineStartDetailsDetails();
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
            //string MACHINE_DATA = ddlMachineCode.SelectedValue.ToString();
            //char[] splitter = { ',' };
            //string[] arrString = MACHINE_DATA.Split(splitter);
            //string MACHINE_GROUP = arrString[1].ToString();
            string CommandText = "select * from ( select * from TX_MAC_PROC_MST WHERE PROS_DESC='TWISTING' ) ASD ";
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
                txtProsDesc.Text = ddlProsCode.SelectedText.Trim();
                lblProsCode.Text = ddlProsCode.SelectedValue.Trim();
                BindProsIdNo();
                ddlLotNo.SelectedIndex = -1;
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
                txtProsIdNo.Text = (int.Parse(SaitexBL.Interface.Method.YRN_PROD_MST.GetMaxProsIdNo(oYRN_PROD_MST)) + 1).ToString();
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

            DateTime dtLoad = System.DateTime.Now.Date;
            if (!DateTime.TryParse(txtLoadingDate.Text.Trim(), out dtLoad))
            {
                msg += "Please select Load Date.";
            }

            DateTime dtunLoad = System.DateTime.Now.Date;
            if (!DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out dtunLoad))
            {
               // msg += "Please select Un-Load Date.";
                
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
                double totalProcessTimeInHours = 0;
                double.TryParse(TotalProcessTime.ToString(), out totalProcessTimeInHours);

                if (TotalProcessTime < 0)
                {
                    //CommonFuction.ShowMessage("Please select load date and time less than unload date and time");
                    txtUnLoadingDate.Text = string.Empty;
                }
                txtProcessTime.Text = Math.Round((totalProcessTimeInHours /60), 2).ToString();//TotalProcessTime.ToString();
            
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
            string PROS_CODE_NAME=string.Empty;
           
            if (MAIN_PROCESS.Equals("TWISTING"))
            {
                PROS_CODE_NAME = "'GMI01'";
            }
            string CommandText = "SELECT   *  FROM   (SELECT  distinct LOT_NUMBER, ORDER_NO, DEPT_CODE, PROS_CODE, ORD_ARTICAL_CODE, ORD_ARTICAL_DESC,ORD_SHADE_CODE,(   ORDER_NO  || '@'  || STOCK_QTY  || '@'  || ARTICLE_CODE   || '@'|| PROS_CODE|| '@'|| BIN_LOCT|| '@'|| LOT_NUMBER   || '@'|| NO_OF_UNIT|| '@'|| UOM_OF_UNIT|| '@'|| WEIGHT_OF_UNIT|| '@'|| ORD_ARTICAL_CODE|| '@'|| ORD_ARTICAL_DESC|| '@'||ORD_SHADE_CODE|| '@'   || ARTICLE_DESC|| '@'   || ORD_ASS_ARTICAL_CODE)    LOT_DATA FROM   V_YRN_WIP_STOCK WHERE   DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' and PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND PROS_CODE NOT IN ('" + ddlProsCode.SelectedValue + "')  AND PROS_CODE  IN (" + PROS_CODE_NAME + ") and nvl(stock_qty,0)>0 ) ASD";
            string WhereClause = "  where LOT_NUMBER like :SearchQuery or ORDER_NO like :SearchQuery ";
            string SortExpression = " order by LOT_NUMBER asc";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
          
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetLotDataForEdit(string Text)
    {
        try
        {
            string PROS_CODE_NAME = string.Empty;

            if (MAIN_PROCESS.Equals("TWISTING"))
            {
                PROS_CODE_NAME = "'GMI01'";
            }
            string CommandText = "SELECT   *  FROM   (SELECT  distinct LOT_NUMBER, ORDER_NO, DEPT_CODE, PROS_CODE, ORD_ARTICAL_CODE, ORD_ARTICAL_DESC,ORD_SHADE_CODE,(   ORDER_NO  || '@'  || STOCK_QTY  || '@'  || ARTICLE_CODE   || '@'|| PROS_CODE|| '@'|| BIN_LOCT|| '@'|| LOT_NUMBER   || '@'|| NO_OF_UNIT|| '@'|| UOM_OF_UNIT|| '@'|| WEIGHT_OF_UNIT|| '@'|| ORD_ARTICAL_CODE|| '@'|| ORD_ARTICAL_DESC|| '@'||ORD_SHADE_CODE|| '@'   || ARTICLE_DESC|| '@'   || ORD_ASS_ARTICAL_CODE)    LOT_DATA FROM   V_YRN_WIP_STOCK WHERE   DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' and PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND PROS_CODE NOT IN ('" + ddlProsCode.SelectedValue + "')  AND PROS_CODE  IN (" + PROS_CODE_NAME + ")  ) ASD";//and nvl(stock_qty,0)>0
            string WhereClause = "  where LOT_NUMBER like :SearchQuery or ORDER_NO like :SearchQuery ";
            string SortExpression = " order by LOT_NUMBER asc";
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

                            txtOrderNo.Text = arrString[0].ToString();
                            // txtOrderDescription.Text = arrString[1].ToString();
                            txtLotQty.Text = arrString[1].ToString();
                            txtMaxLoadQty.Text = arrString[1].ToString();
                            txtLoadQty.Text = arrString[1].ToString();
                            txtUnloadQty.Text = arrString[1].ToString();
                            txtGreyArticleCode.Text = arrString[2].ToString();

                            lblPROS_CODE.Text = arrString[3].ToString();
                            lblBIN_LOCT.Text = arrString[4].ToString();
                            lblBATCH_NO.Text = arrString[5].ToString();

                            txtLoadNoOfUnit.Text = arrString[6].ToString();
                            ddlLoadUOM.SelectedIndex = ddlLoadUOM.Items.IndexOf(ddlLoadUOM.Items.FindByValue(arrString[7].ToString()));
                            txtLoadWeightOfUnit.Text = arrString[8].ToString();
                            txtPattern.Text = arrString[9].ToString();
                            txtOrderDescription.Text = arrString[10].ToString();                           
                            //txtOrderDescription.Text = arrString[9].ToString() + "," + arrString[10].ToString() + "," + arrString[11].ToString();
                            ddlUnloadUOM.SelectedIndex = ddlUnloadUOM.Items.IndexOf(ddlUnloadUOM.Items.FindByValue(arrString[7].ToString()));
                            //txtQuality.Text = arrString[11].ToString();
                            txtGreyArticleDesc.Text = arrString[12].ToString();  
                           
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
    protected void txtLoadNoOfUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!GetLoadQty())
            {
                txtLoadNoOfUnit.Text = "0";
            }
            btnsaveLotDetail.Focus();

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
           
            if (Session["dtProdTRN"] == null)
                dtProdTRN=CreateLotDetailTable();
            else 
               dtProdTRN= (DataTable)Session["dtProdTRN"];

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

                    if (UNIQUE_TRN > 0)
                    {
                        DataView dv = new DataView(dtProdTRN);
                        dv.RowFilter = "UNIQUE_TRN=" + UNIQUE_TRN;
                        if (dv.Count > 0)
                        {
                            dv[0]["ORDER_NO"] = txtOrderNo.Text.Trim();
                            dv[0]["ORDER_DESC"] = txtOrderDescription.Text.Trim();
                            dv[0]["LOT_NUMBER"] = cmbLotNo.SelectedText;
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
                            dv[0]["UNLOAD_QTY"] = 0;// UNLOAD_QTY;
                            dv[0]["DEPT_CODE"] = oUserLoginDetail.VC_DEPARTMENTCODE;
                            dv[0]["FR_LOCATION"] = lblBIN_LOCT.Text;
                            dv[0]["TO_LOCATION"] = ddlPackaging.SelectedValue;//txtToLocation.Text.Trim();
                            dv[0]["FR_BATCH_NO"] = lblBATCH_NO.Text;
                            dv[0]["TO_BATCH_NO"] =  txtToBatchNo.Text.Trim();
                            dv[0]["FR_PROS_CODE"] = lblPROS_CODE.Text;

                            DateTime loadingDate = DateTime.Now;
                            DateTime unloadingDate = DateTime.Now;
                            DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
                            DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
                            dv[0]["LOAD_DATE"] = loadingDate;
                            dv[0]["UNLOAD_DATE"] = unloadingDate;
                            dv[0]["NO_OF_DRUM"] = 0;
                            dv[0]["SPEED_OF_DRUM"] = 0;
                            //int PATTERN_NO = 0;
                            //int.TryParse(txtPattern.Text.Trim(), out PATTERN_NO);
                            //dv[0]["PATTERN_NO"] = PATTERN_NO;
                            dv[0]["PATTERN_NO"]=txtPattern.Text.Trim();
                            dv[0]["EFFICIENCY"] = 0f;
                            dv[0]["WET_PACKAGES_REDRYING"] = 0;
                            dv[0]["ARTICLE_CODE"] = txtGreyArticleCode.Text.Trim();
                            dv[0]["ARTICLE_DESC"] = txtGreyArticleDesc.Text.Trim();
                            dv[0]["DYED_LOT_NO"] = txtDyedLot.Text.Trim();
                            dv[0]["LENGTH_IN_METERS"] = 0f;
                            dv[0]["REJECTIONS_OF_UNIT"] = 0f;
                            dv[0]["WEIGHT_OF_REJECTION"] = 0f;
                            dv[0]["PEEL_OF"] = 0f;
                            dtProdTRN.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtProdTRN.NewRow();
                        dr["UNIQUE_TRN"] = dtProdTRN.Rows.Count + 1;
                        dr["ORDER_NO"] = txtOrderNo.Text.Trim();
                        dr["ORDER_DESC"] = txtOrderDescription.Text.Trim();
                        dr["LOT_NUMBER"] = cmbLotNo.SelectedText;
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
                        dr["UNLOAD_NO_OF_UNIT"] = 0;// UNLOAD_NO_OF_UNIT;
                        dr["UNLOAD_UOM_OF_UNIT"] = ddlUnloadUOM.SelectedItem.Text.ToString();
                        double UNLOAD_WEIGHT_OF_UNIT = 0;
                        double.TryParse(txtUnloadWeightOfUnit.Text.Trim(), out UNLOAD_WEIGHT_OF_UNIT);
                        dr["UNLOAD_WEIGHT_OF_UNIT"] = 0;// UNLOAD_WEIGHT_OF_UNIT;
                        dr["UNLOAD_QTY"] = 0;// UNLOAD_QTY;
                        dr["DEPT_CODE"] = oUserLoginDetail.VC_DEPARTMENTCODE;
                        dr["FR_LOCATION"] = lblBIN_LOCT.Text;
                        dr["TO_LOCATION"] = ddlPackaging.SelectedValue;// txtToLocation.Text.Trim();
                        dr["FR_BATCH_NO"] = lblBATCH_NO.Text;
                        dr["TO_BATCH_NO"] = txtToBatchNo.Text.Trim();
                        dr["FR_PROS_CODE"] = lblPROS_CODE.Text;
                        dr["LOAD_DATE"] = string.Empty; // DateTime.Parse(txtLoadingDateTime.Text.Trim());
                        dr["UNLOAD_DATE"] = string.Empty; //DateTime.Parse(txtUnLoadingDateTime.Text.Trim());
                        dr["NO_OF_DRUM"] = 0;
                        dr["SPEED_OF_DRUM"] = 0;
                        //int PATTERN_NO = 0;
                        //int.TryParse(txtPattern.Text.Trim(), out PATTERN_NO);
                        //dr["PATTERN_NO"] = PATTERN_NO;
                        dr["PATTERN_NO"] = txtPattern.Text.Trim();
                        dr["EFFICIENCY"] = 0f;
                        dr["WET_PACKAGES_REDRYING"] = 0;
                        dr["ARTICLE_CODE"] = txtGreyArticleCode.Text.Trim();
                        dr["ARTICLE_DESC"] = txtGreyArticleDesc.Text.Trim();
                        dr["DYED_LOT_NO"] = txtDyedLot.Text.Trim();
                        dr["LENGTH_IN_METERS"] = 0f;
                        dr["REJECTIONS_OF_UNIT"] = 0f;
                        dr["WEIGHT_OF_REJECTION"] = 0f;
                        dr["PEEL_OF"] = 0f;
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
            CalculateUnloadQty();
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
            dtProdTRN.Columns.Add("NO_OF_DRUM", typeof(int));
            dtProdTRN.Columns.Add("SPEED_OF_DRUM", typeof(int));
            dtProdTRN.Columns.Add("PATTERN_NO", typeof(string));
            dtProdTRN.Columns.Add("EFFICIENCY", typeof(double));
            dtProdTRN.Columns.Add("WET_PACKAGES_REDRYING", typeof(int));
            dtProdTRN.Columns.Add("ARTICLE_CODE", typeof(string));
            dtProdTRN.Columns.Add("ARTICLE_DESC", typeof(string));
            dtProdTRN.Columns.Add("DYED_LOT_NO", typeof(string));
            dtProdTRN.Columns.Add("LENGTH_IN_METERS", typeof(double));
            dtProdTRN.Columns.Add("REJECTIONS_OF_UNIT", typeof(double));
            dtProdTRN.Columns.Add("WEIGHT_OF_REJECTION", typeof(double));
            dtProdTRN.Columns.Add("PEEL_OF", typeof(double));

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
                ViewState["UNIQUE_TRN"] = UNIQUE_TRN;     
                lblBATCH_NO.Text = dv[0]["LOT_NUMBER"].ToString();
                txtLoadQty.Text = dv[0]["LOAD_QTY"].ToString();
                txtUnloadQty.Text = dv[0]["UNLOAD_QTY"].ToString();
                txtToBatchNo.Text = dv[0]["TO_BATCH_NO"].ToString();
                ddlPackaging.SelectedIndex = ddlPackaging.Items.IndexOf(ddlPackaging.Items.FindByValue(dv[0]["TO_LOCATION"].ToString()));
                txtToLocation.Text = dv[0]["TO_LOCATION"].ToString(); 
                txtGreyArticleCode.Text = dv[0]["ARTICLE_CODE"].ToString();
                txtGreyArticleDesc.Text = dv[0]["ARTICLE_DESC"].ToString();
                txtPattern.Text = dv[0]["PATTERN_NO"].ToString();
                txtLoadNoOfUnit.Text = dv[0]["LOAD_NO_OF_UNIT"].ToString();
                ddlLoadUOM.SelectedIndex=ddlLoadUOM.Items.IndexOf(ddlLoadUOM.Items.FindByValue( dv[0]["LOAD_UOM_OF_UNIT"].ToString()));
                ddlUnloadUOM.SelectedIndex = ddlUnloadUOM.Items.IndexOf(ddlUnloadUOM.Items.FindByValue(dv[0]["UNLOAD_UOM_OF_UNIT"].ToString()));
                txtLoadWeightOfUnit.Text = dv[0]["LOAD_WEIGHT_OF_UNIT"].ToString();
                txtUnloadNoOfUnit.Text = dv[0]["UNLOAD_NO_OF_UNIT"].ToString();
                txtUnloadWeightOfUnit.Text = dv[0]["UNLOAD_WEIGHT_OF_UNIT"].ToString();
                txtLotQty.Text = dv[0]["LOT_QTY"].ToString();
                lblPROS_CODE.Text=dv[0]["FR_PROS_CODE"].ToString();
                txtMaxLoadQty.Text = dv[0]["LOT_QTY"].ToString();
                DataTable data = GetLotDataForEdit("");
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
                        txtOrderNo.Text = arrString[0].ToString();
                        txtOrderDescription.Text = arrString[10].ToString();
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

    private void SaveProductionEntry( )
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
            oYRN_PROD_MST.MACHINE_CODE = cmbMachineGroup.SelectedValue;// ddlMachineCode.SelectedText.Trim();            
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
            DateTime loadingDate = DateTime.Now;
            DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
            DateTime times = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", startTime.Hour, startTime.Minute, startTime.Second, startTime.AmPm));
            var stime = TimeSpan.Parse(times.ToString("HH:mm:ss"));
            oYRN_PROD_MST.LOAD_DATE = loadingDate.Add(stime);  


            DateTime unloadingDate = DateTime.Now;
            DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
            oYRN_PROD_MST.UNLOAD_DATE = unloadingDate;            
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
            oYRN_PROD_MST.MACHINE_SIDE = ddlSide.SelectedValue ;

            oYRN_PROD_MST.LOT_NO = cmbLotNo.SelectedText;
            oYRN_PROD_MST.MERGE_NO = txtMergeNo.Text;
            oYRN_PROD_MST.LOT_TYPE = txtLotType.Text;
            oYRN_PROD_MST.FINISH_TYPE = txtFinishType.Text;
            oYRN_PROD_MST.TYPE = MAIN_PROCESS;
            string msg = string.Empty;
            int PROS_ID_NO = 0;
            DataTable dtMachineStopDataTable = new DataTable();
            if (Session["dtMachineStopDataTable"] != null)
            {
                dtMachineStopDataTable = (DataTable)Session["dtMachineStopDataTable"];
            }
            bool result = SaitexBL.Interface.Method.YRN_PROD_MST.InsertYS(oYRN_PROD_MST, out PROS_ID_NO, dtProdTRN, out msg, dtMachineStopDataTable,new DataTable());
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
            string commandText = "SELECT * FROM (select PROS_ID_NO,PROS_CODE,TRN_TYPE,MACHINE_CODE,DEPT_CODE,SFT_ID,(YEAR ||'@'|| COMP_CODE ||'@'|| BRANCH_CODE ||'@'||  TRN_TYPE ||'@'|| PROS_CODE ||'@'|| PROS_ID_NO ||'@'|| MACHINE_CODE )PROS_DATA from YRN_PROD_MST WHERE DEPT_CODE='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'   AND BRANCH_CODE =       '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND TRN_TYPE='" + TRN_TYPE + "' ) ASD ";
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
            oYRN_PROD_MST.PROS_ID_NO = int.Parse(sPROS_ID_NO);
            oYRN_PROD_MST.TYPE = MAIN_PROCESS;
            lblProsCode.Text = sPROS_CODE;
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetProdDataByPROS_ID_NO(oYRN_PROD_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtProsIdNo.Text = dt.Rows[0]["PROS_ID_NO"].ToString().Trim();
                txtEntryDate.Text = DateTime.Parse(dt.Rows[0]["TRN_DATE"].ToString().Trim()).ToShortDateString();
                ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByValue(dt.Rows[0]["SFT_ID"].ToString().Trim()));
                //txtLoadingDate.Text = dt.Rows[0]["LOAD_DATE"].ToString().Trim();
                DateTime dtloaddate = DateTime.Now;
                DateTime.TryParse(dt.Rows[0]["LOAD_DATE"].ToString().Trim(), out dtloaddate);
                txtLoadingDate.Text = dtloaddate.ToString("dd/MM/yyyy");
                //startTime.Hour = dtloaddate.Hour;
                //startTime.Minute = dtloaddate.Minute;
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

                txtUnLoadingDate.Text = dt.Rows[0]["UNLOAD_DATE"].ToString().Trim();
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
               txtFinishType.Text=dt.Rows[0]["FINISH_TYPE"].ToString();

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
                       string[] arrString = cmbLotNo.SelectedValue.Split('@');
                       if (arrString.Length > 1)
                       {
                           lblFinishDenier.Text = arrString[7].ToString();
                       }

                       break;
                   }
               }

                ddlSide.SelectedIndex  = ddlSide.Items.IndexOf(ddlSide.Items.FindByValue(dt.Rows[0]["MACHINE_SIDE"].ToString()));
                DataTable dataMac = GetMachineData("");
                ddlMachineCode.DataSource = dataMac;
                ddlMachineCode.DataTextField = "MACHINE_CODE";
                ddlMachineCode.DataValueField = "MACHINE_DATA";
                ddlMachineCode.DataBind();
                foreach (Obout.ComboBox.ComboBoxItem item in ddlMachineCode.Items)
                {
                    if (item.Text == dt.Rows[0]["MACHINE_CODE"].ToString().Trim())
                    {
                        //ddlMachineCode.SelectedIndex = ddlMachineCode.Items.IndexOf(item);
                        txtMachineDesc.Text = item.Value.Trim();
                        ddlMachineCode.SelectedText = item.Text.ToString();
                        ddlMachineCode.SelectedValue = item.Value.ToString();
                        break;
                    }
                }


                DataTable dataMacGroup = GetItems("%", 0, 25);
                cmbMachineGroup.DataSource = dataMacGroup;
                cmbMachineGroup.DataTextField = "MACHINE_GROUP";
                cmbMachineGroup.DataValueField = "MACHINE_GROUP";
                cmbMachineGroup.DataBind();
                foreach (Obout.ComboBox.ComboBoxItem item in cmbMachineGroup.Items)
                {
                    if (item.Text == dt.Rows[0]["MACHINE_CODE"].ToString().Trim())
                    {
                        //ddlMachineCode.SelectedIndex = ddlMachineCode.Items.IndexOf(item);
                        txtMachineDesc.Text = item.Value.Trim();
                        cmbMachineGroup.SelectedText = item.Text.ToString();
                        cmbMachineGroup.SelectedValue = item.Value.ToString();
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

                        break;
                    }
                }
            }


            if (iRecordFound == 1)
            {
                DataTable dtTemp_ProdTRN = SaitexBL.Interface.Method.YRN_PROD_MST.GetTRN_ByProsIdNo(oYRN_PROD_MST);
                DataTable dtMachineStopDataTable = SaitexBL.Interface.Method.YRN_PROD_MST.GetMachineStopData(oYRN_PROD_MST);

                Session["dtMachineStopDataTable"] = dtMachineStopDataTable;

                if (dtTemp_ProdTRN != null && dtTemp_ProdTRN.Rows.Count > 0)
                {
                    MapDataTable_ProdTRN(dtTemp_ProdTRN);

                    BindLotDetailGrid();
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

                    dr["NO_OF_DRUM"] = drTemp["NO_OF_DRUM"];
                    dr["SPEED_OF_DRUM"] = drTemp["SPEED_OF_DRUM"];
                    dr["PATTERN_NO"] = drTemp["PATTERN_NO"];
                    dr["EFFICIENCY"] = drTemp["EFFICIENCY"];
                    dr["WET_PACKAGES_REDRYING"] = drTemp["WET_PACKAGES_REDRYING"];

                    dr["ARTICLE_CODE"] = drTemp["ARTICLE_CODE"];
                    dr["ARTICLE_DESC"] = drTemp["ARTICLE_DESC"];
                    dr["DYED_LOT_NO"] = drTemp["DYED_LOT_NO"];
                    dr["LENGTH_IN_METERS"] = drTemp["LENGTH_IN_METERS"];
                    dr["REJECTIONS_OF_UNIT"] = drTemp["REJECTIONS_OF_UNIT"];
                    dr["WEIGHT_OF_REJECTION"] = drTemp["WEIGHT_OF_REJECTION"];
                    dr["PEEL_OF"] = drTemp["PEEL_OF"];
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
            oYRN_PROD_MST.MACHINE_CODE = cmbMachineGroup.SelectedValue;//ddlMachineCode.SelectedText.Trim();

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
            DateTime loadingDate = DateTime.Now;
            DateTime.TryParse(txtLoadingDate.Text.Trim(), out loadingDate);
            DateTime times = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", startTime.Hour, startTime.Minute, startTime.Second, startTime.AmPm));
            var stime = TimeSpan.Parse(times.ToString("HH:mm:ss"));
            oYRN_PROD_MST.LOAD_DATE = loadingDate.Add(stime);  




            DateTime unloadingDate = DateTime.Now;
            DateTime.TryParse(txtUnLoadingDate.Text.Trim(), out unloadingDate);
            oYRN_PROD_MST.UNLOAD_DATE = unloadingDate;

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
            oYRN_PROD_MST.MACHINE_SIDE = ddlSide.SelectedValue ;
            oYRN_PROD_MST.LOT_NO = cmbLotNo.SelectedText;
            oYRN_PROD_MST.MERGE_NO = txtMergeNo.Text;
            oYRN_PROD_MST.LOT_TYPE = txtLotType.Text;
            oYRN_PROD_MST.FINISH_TYPE = txtFinishType.Text;
            oYRN_PROD_MST.TYPE = MAIN_PROCESS;
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

            bool result = SaitexBL.Interface.Method.YRN_PROD_MST.UpdateYS(oYRN_PROD_MST, dtProdTRN, out msg, dtMachineStopDataTable,new DataTable());
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

           
            string URL = "../../Production/Report/ProductionFormReport.aspx?TRN_TYPE=" + TRN_TYPE;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

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
            //if (ddlMachineCode.SelectedIndex != -1)
            if (cmbMachineGroup.SelectedIndex != -1)
            {
                string URL = "MachineStop.aspx";
                CalcProcessTime();
                URL = URL + "?TextBoxId=" + txtMachineStopage.ClientID;
                URL = URL + "&MAC_CODE=" + ddlMachineCode.SelectedText.Trim();
                URL = URL + "&MAC_LOAD_TIME=" + txtLoadingDate.Text;
                URL = URL + "&MAC_UNLOAD_TIME=" + txtUnLoadingDate.Text;
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
            txtDoffingNetWt.Text  = Math.Round(TotalUnloadQty, 3).ToString();
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

            string CommandText = "SELECT * FROM (SELECT DISTINCT  LOT_NO,LOT_TYPE,PURPOSE,POY,POY_DESC,MERGE_NO,FINISHED_DENIER,FINISHED_DENIER_DESC,FINISHED_TYPE,JET_NO,MACHINE_NAME,(   LOT_NO|| '@'|| LOT_TYPE|| '@'||      PURPOSE|| '@'|| POY|| '@'|| POY_DESC|| '@'||  MERGE_NO|| '@'||      FINISHED_DENIER|| '@' || FINISHED_DENIER_DESC|| '@'|| FINISHED_TYPE||      '@'|| JET_NO|| '@'|| MACHINE_NAME )         LOT_DATA FROM   V_YRN_LOT_MAKING WHERE  NVL(CONF_FLAG,0)=1 AND     PRODUCT_CATEGORY='TEXTURISING'  AND  ( UPPER(LOT_NO) LIKE :SearchQuery OR  UPPER(POY) LIKE :SearchQuery OR  UPPER(   POY_DESC) LIKE :SearchQuery OR  UPPER(MERGE_NO) LIKE :SearchQuery OR      UPPER(FINISHED_DENIER) LIKE :SearchQuery OR  UPPER(FINISHED_DENIER_DESC)      LIKE :SearchQuery OR  UPPER(MACHINE_NAME) LIKE :SearchQuery ))    WHERE  ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " WHERE  LOT_NO NOT IN  ( SELECT LOT_NO FROM (SELECT DISTINCT  LOT_NO FROM   V_YRN_LOT_MAKING WHERE NVL(CONF_FLAG,0)=1 AND    PRODUCT_CATEGORY='TEXTURISING'  AND  ( UPPER(LOT_NO) LIKE :SearchQuery OR  UPPER(POY) LIKE :SearchQuery OR  UPPER(   POY_DESC) LIKE :SearchQuery OR  UPPER(MERGE_NO) LIKE :SearchQuery OR      UPPER(FINISHED_DENIER) LIKE :SearchQuery OR  UPPER(FINISHED_DENIER_DESC)      LIKE :SearchQuery OR  UPPER(MACHINE_NAME) LIKE :SearchQuery )) WHERE  ROWNUM <= " + startOffset + ")";
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

            string CommandText = " SELECT DISTINCT  LOT_NO  FROM   V_YRN_LOT_MAKING WHERE   NVL(CONF_FLAG,0)=1 AND    PRODUCT_CATEGORY='TEXTURISING' AND  ( UPPER(LOT_NO) LIKE :SearchQuery OR  UPPER(POY) LIKE :SearchQuery OR  UPPER(   POY_DESC) LIKE :SearchQuery OR  UPPER(MERGE_NO) LIKE :SearchQuery OR      UPPER(FINISHED_DENIER) LIKE :SearchQuery OR  UPPER(FINISHED_DENIER_DESC)      LIKE :SearchQuery OR  UPPER(MACHINE_NAME) LIKE :SearchQuery ) ";
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
            string CommandText = "SELECT DISTINCT  LOT_NO,LOT_TYPE,PURPOSE,POY,POY_DESC,MERGE_NO,FINISHED_DENIER,FINISHED_DENIER_DESC,FINISHED_TYPE,JET_NO,MACHINE_NAME,(   LOT_NO|| '@'|| LOT_TYPE|| '@'||      PURPOSE|| '@'|| POY|| '@'|| POY_DESC|| '@'||  MERGE_NO|| '@'||      FINISHED_DENIER|| '@' || FINISHED_DENIER_DESC|| '@'|| FINISHED_TYPE||      '@'|| JET_NO|| '@'|| MACHINE_NAME )         LOT_DATA FROM   V_YRN_LOT_MAKING WHERE   NVL(CONF_FLAG,0)=1 AND    PRODUCT_CATEGORY='TEXTURISING'  AND  ( UPPER(LOT_NO) LIKE :SearchQuery OR  UPPER(POY) LIKE :SearchQuery OR  UPPER(   POY_DESC) LIKE :SearchQuery OR  UPPER(MERGE_NO) LIKE :SearchQuery OR      UPPER(FINISHED_DENIER) LIKE :SearchQuery OR  UPPER(FINISHED_DENIER_DESC)      LIKE :SearchQuery OR  UPPER(MACHINE_NAME) LIKE :SearchQuery )    ";
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
            //txtMachineDesc.Text = arrString[3].ToString();//POY
            //txtMachineDesc.Text = arrString[4].ToString();//POY_DESC
            txtMergeNo.Text = arrString[5].ToString();//MERGE_NO
            //txtMachineDesc.Text = arrString[6].ToString();//FINISHED_DENIER
            lblFinishDenier.Text = arrString[7].ToString();//FINISHED_DENIER_DESC
            txtFinishType.Text = arrString[8].ToString();//FINISHED_TYPE
            txtRotoJetNo.Text = arrString[9].ToString();//JET_NO
            if (txtRotoJetNo.Text.ToUpper().Equals("NA"))
            {
                txtAirPressure.Text = "NA";
            }

            //txtMachineDesc.Text = arrString[10].ToString();//MACHINE_NAME
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
                    ddlMachineCode.SelectedText = item.Text.ToString();
                    ddlMachineCode.SelectedValue = item.Value.ToString();
                    string[] arrString1 = ddlMachineCode.SelectedValue.Split('@');
                    txtMachineDesc.Text = arrString1[0].ToString();                  
                    break;
                }
            }

            getMachineStartDetailsDetails();
                      
            
        }
    }

    protected void getMachineStartDetailsDetails()
    {
        DataTable machineStartDetails = SaitexBL.Interface.Method.YRN_PROD_MST.Get_Machine_Start_Time(ddlMachineCode.SelectedText, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
        if(machineStartDetails.Rows.Count > 0)
        {
        txtLoadingDate.Text = machineStartDetails.Rows[0]["MACHINE_START_DATE"].ToString();//DOFF_START_TIME  
        }  
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE MACHINE_TYPE='TWISTING' AND  MACHINE_GROUP like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select distinct * from MC_MACHINE_GRP";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected void cmbMachineGroup_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);
        cmbMachineGroup.Items.Clear();
        cmbMachineGroup.DataSource = data;
        cmbMachineGroup.DataBind();

    }
    protected void cmbMachineGroup_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbMachineGroup.SelectedIndex != -1)
            {
                //string[] arrString = cmbMachineGroup.SelectedValue.Split('@');

                txtMachineDesc.Text = cmbMachineGroup.SelectedValue;
                //if (!arrString[1].ToString().Equals(string.Empty))
                //{
                //    txtLoadingDate.Text = DateTime.Parse(arrString[1].ToString()).ToString();
                //    txtUnLoadingDate.Text = DateTime.Parse(arrString[1].ToString()).ToString();
                //}
            }
           // ddlLotNo.SelectedIndex = -1;
            //getMachineStartDetailsDetails();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in selecting machine.\r\nSee error log for detail."));
        }
    }
}
