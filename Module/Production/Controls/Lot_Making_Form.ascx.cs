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
using System.Data.OracleClient;
using DBLibrary;
using Common;

using errorLog;
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Production_Controls_LotMakingForm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.YRN_LOT_MAKING oYRN_LOT_MAKING;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string PRODUCT_CATEGORY { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {

        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        lblHeadingName.Text = PRODUCT_CATEGORY;
        if (!IsPostBack)
        {

            InitialisePage();
        }

    }



    private void InitialisePage()
    {
        try
        {
            
            txtLotNo.Text = "";           
            txtMachineSpeed.Text = "";           
            txtdr.Text = "";          
            txttkp.Text = "";
            txtdy.Text = "";          
            txtph.Text = "";
            txtsh.Text = "";
            txtsof.Text = "";
            txtRotoPressure.Text = "";
            txtJetNo.Text = "";
            txtcpm.Text = "";
            txtdryden.Text = "";
            txtelg.Text = "";
            txtOilrpm.Text = "";
            txtopu.Text = "";
            txtDoffTime.Text = "";
            txtRatio.Text = "";
            txtplt.Text = "";
            txttint.Text = "";
            txtgpd.Text = "";
            txtDoffWeight.Text = "";
            txtHeatSetting.Text = "";
            txtOilden.Text = "";           
            ddlMacCode.SelectedIndex=-1;
            cmbYarn.SelectedIndex = -1;
            txtRatio1.Text = "";
            txtt1_1.Text = "";
            txtt1_2.Text = "";
            txtt1_3.Text = "";
            txtt1_4.Text = "";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            cmbFindLotNo.SelectedIndex = -1;
            cmbFindLotNo.Visible = false;
            txtLotNo.Visible = true;
            ddlFinishedType.SelectedIndex = -1;
            txtItemCode.SelectedIndex = -1;
            cmbLotNo.SelectedIndex = -1;
            lblPartyName.Text = string.Empty;
            
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
            if (!string.IsNullOrEmpty(txtItemCode.SelectedValue) && !string.IsNullOrEmpty(cmbLotNo.SelectedText) && !string.IsNullOrEmpty(ddlMacCode.SelectedValue) && !string.IsNullOrEmpty(cmbYarn.SelectedValue) && !string.IsNullOrEmpty(txtLotNo.Text))
            {
                SaveLotMakingData();
            }
            else 
            {
                CommonFuction.ShowMessage("Please select Poy/Merge/Machine/Finish Denier/Lot No.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtItemCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = BindItemCodeCombo(e.Text.ToUpper(), e.ItemsOffset);
            if (data != null && data.Rows.Count > 0)
            {
                txtItemCode.Items.Clear();
                txtItemCode.DataSource = data;
                txtItemCode.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable BindItemCodeCombo(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   FIBER_CODE,FIBER_DESC  FROM   TX_FIBER_MASTER  WHERE   (   UPPER (FIBER_CODE) LIKE :SearchQuery          OR UPPER (FIBER_DESC) LIKE :SearchQuery     OR UPPER (FIBER_CAT) LIKE :SearchQuery          OR UPPER (SUB_FIBER_CAT) LIKE :SearchQuery)         AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  WHERE  FIBER_CODE NOT IN  ( SELECT   FIBER_CODE  FROM   TX_FIBER_MASTER  WHERE   (   UPPER (FIBER_CODE) LIKE :SearchQuery          OR UPPER (FIBER_DESC) LIKE :SearchQuery     OR UPPER (FIBER_CAT) LIKE :SearchQuery          OR UPPER (SUB_FIBER_CAT) LIKE :SearchQuery)  AND   ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by FIBER_CODE";
            string SearchQuery = "%" + text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {

        string CommandText = "SELECT  FIBER_CODE  FROM   TX_FIBER_MASTER      WHERE (   UPPER (FIBER_CODE) LIKE :SearchQuery           OR UPPER (FIBER_DESC) LIKE :SearchQuery        OR UPPER (FIBER_CAT) LIKE :SearchQuery    OR UPPER (SUB_FIBER_CAT) LIKE :SearchQuery)          ORDER BY   FIBER_CODE ASC";
        string WhereClause = "  ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
    }

    protected void SaveLotMakingData()
    {
        string LOT_NO = string.Empty;
        try
        {
            oYRN_LOT_MAKING = new SaitexDM.Common.DataModel.YRN_LOT_MAKING();
            oYRN_LOT_MAKING.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_LOT_MAKING.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_LOT_MAKING.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_LOT_MAKING.PRODUCT_CATEGORY = PRODUCT_CATEGORY;
            oYRN_LOT_MAKING.FINISHED_TYPE = ddlFinishedType.SelectedValue;
            oYRN_LOT_MAKING.TDATE = System.DateTime.Now;            
            oYRN_LOT_MAKING.POY = txtItemCode.SelectedValue;
            oYRN_LOT_MAKING.MERGE_NO = cmbLotNo.SelectedText;
            oYRN_LOT_MAKING.FINISHED_DENIER = cmbYarn.SelectedValue;    
            oYRN_LOT_MAKING.LOT_NO = txtLotNo.Text.Trim();
            double MachineSpeed = 0;
            double.TryParse(txtMachineSpeed.Text, out MachineSpeed);
            oYRN_LOT_MAKING.MACHINE_SPEED = MachineSpeed;
            double dr = 0;
            double.TryParse(txtdr.Text, out dr);
            oYRN_LOT_MAKING.DR = dr;
            double tkp = 0;
            double.TryParse(txttkp.Text, out tkp);
            oYRN_LOT_MAKING.TKP = tkp;
            double sof = 0;
            double.TryParse(txtsof.Text, out sof);
            oYRN_LOT_MAKING.SOF = sof;
            double dy = 0;
            double.TryParse(txtdy.Text, out dy);
            oYRN_LOT_MAKING.DY = dy;
            double cpm = 0;
            double.TryParse(txtcpm.Text, out cpm);
            oYRN_LOT_MAKING.CPM = cpm;
            double ph = 0;
            double.TryParse(txtph.Text, out ph);
            oYRN_LOT_MAKING.PH = ph;
            double sh = 0;
            double.TryParse(txtsh.Text, out sh);
            oYRN_LOT_MAKING.SH = sh;
            oYRN_LOT_MAKING.OIL_RPM = txtOilrpm.Text.Trim();
            double Ratio = 0;
            double.TryParse(txtRatio.Text, out Ratio);
            oYRN_LOT_MAKING.RATIO_T1_H = Ratio;
            double Ratio1 = 0;
            double.TryParse(txtRatio1.Text, out Ratio1);
            oYRN_LOT_MAKING.RATIO_T2_L = Ratio1;
            double dryden = 0;
            double.TryParse(txtdryden.Text, out dryden);
            oYRN_LOT_MAKING.DRY_DEN = dryden;
            oYRN_LOT_MAKING.ELG = txtelg.Text.Trim();
            double gpd = 0;
            double.TryParse(txtgpd.Text, out gpd);
            oYRN_LOT_MAKING.GPD = gpd;
            double opu = 0;
            double.TryParse(txtopu.Text, out opu);
            oYRN_LOT_MAKING.OPU = opu;
            oYRN_LOT_MAKING.ROTO_PRESSURE = txtRotoPressure.Text.Trim();
            oYRN_LOT_MAKING.JET_NO = txtJetNo.Text.Trim();
            double DoffTime = 0;
            double.TryParse(txtDoffTime.Text, out DoffTime);
            oYRN_LOT_MAKING.DOFF_TIME = DoffTime;
            double DoffWeight = 0;
            double.TryParse(txtDoffWeight.Text, out DoffWeight);
            oYRN_LOT_MAKING.DOFF_WEIGHT = DoffWeight;
            oYRN_LOT_MAKING.PLT = txtplt.Text.Trim();
            oYRN_LOT_MAKING.TINT = txttint.Text.Trim();
            oYRN_LOT_MAKING.PURPOSE = ddlPurpose.Text.Trim();
            oYRN_LOT_MAKING.OIL_DEN = txtOilden.Text.Trim();
            oYRN_LOT_MAKING.TUSER = oUserLoginDetail.UserCode;
            oYRN_LOT_MAKING.TDATE = DateTime.Now;      
            oYRN_LOT_MAKING.MACHINE_NAME = ddlMacCode.SelectedValue;  
            oYRN_LOT_MAKING.HEAT_SETTING = txtHeatSetting.Text.Trim();
            oYRN_LOT_MAKING.LOT_TYPE = ddlLotType.SelectedValue;
            oYRN_LOT_MAKING.CONF_FLAG = 0;
            double t1_1 = 0;
            double.TryParse(txtt1_1.Text, out t1_1);
            oYRN_LOT_MAKING.T1 = t1_1;
            double t1_3 = 0;
            double.TryParse(txtt1_3.Text, out t1_3);
            oYRN_LOT_MAKING.T1_H = t1_3;
            double t1_2 = 0;
            double.TryParse(txtt1_2.Text, out t1_2);
            oYRN_LOT_MAKING.T1_L = t1_1;
            double t1_4 = 0;
            double.TryParse(txtt1_4.Text, out t1_4);
            oYRN_LOT_MAKING.T2 = t1_4;
            bool Result = SaitexBL.Interface.Method.YRN_LOT_MAKING.InsertLotMaking(oYRN_LOT_MAKING);
            if (Result)
            {
                InitialisePage();
                string msg = "Yarn Lot Making " + LOT_NO + " Successfully Saved.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent saving Failed');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlMacCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMacData(e.Text.ToUpper(), e.ItemsOffset);
            ddlMacCode.Items.Clear();
            ddlMacCode.DataSource = data;
            ddlMacCode.DataTextField = "MACHINE_CODE";
            ddlMacCode.DataValueField = "MACHINE_CODE";
            ddlMacCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetMacCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for Machine Code.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected int GetMacCount(string text)
    {
        try
        {
            DataTable dt = new DataTable();

            string whereClause = " ";
            string sortExpression = " ";
            string commandText = "SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_TYPE='TEXTURISING' AND  (MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery) ORDER BY MACHINE_CODE ASC)  ";

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt.Rows.Count;
        }
        catch
        {
            throw;
        }
    }
    protected DataTable GetMacData(string text, int startoffset)
    {
        try
        {
            DataTable dt = new DataTable();
            string commandText = " SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC,OLD_MACHINE_NAME  FROM v_MC_MACHINE_MASTER WHERE MACHINE_TYPE='TEXTURISING' AND  (MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery) ORDER BY MACHINE_CODE ASC) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += " AND MACHINE_CODE NOT IN(SELECT MACHINE_CODE FROM (SELECT MACHINE_CODE,MACHINE_GROUP,MACHINE_CAPACITY,MACHINE_SEGMENT,MACHINE_TYPE,MACHINE_SEC,OLD_MACHINE_NAME FROM v_MC_MACHINE_MASTER WHERE MACHINE_TYPE='TEXTURISING' AND (MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery) ORDER BY MACHINE_CODE ASC)WHERE ROWNUM <= '" + startoffset + "')";
            }

            string SortExpression = " order by MACHINE_CODE asc";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cmbFindLotNo.Visible = true;
            txtLotNo.Visible = false;
            tdDelete.Visible = true;
            tdSave.Visible = false;
            tdUpdate.Visible = true;           
            lblMode.Text = "You are in Find Mode";

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding..\r\nSee error log for detail."));
        }
    }
    protected void cmbFindLotNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
                   
            
            lblMode.Text = "You are In Update";           
            GetFindDataByCode(cmbFindLotNo.SelectedValue.Trim());         


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding Vendors..\r\nSee error log for detail."));
        }
    }



    private void GetFindDataByCode(string LOT_NO)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_LOT_MAKING.SelectByCode(LOT_NO,PRODUCT_CATEGORY,oUserLoginDetail.COMP_CODE , oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
               
                txtLotNo.Text = dt.Rows[0]["LOT_NO"].ToString();
                txtMachineSpeed.Text = dt.Rows[0]["MACHINE_SPEED"].ToString();               
                txtdr.Text = dt.Rows[0]["DR"].ToString();              
                txttkp.Text = dt.Rows[0]["TKP"].ToString();
                txtsof.Text = dt.Rows[0]["SOF"].ToString();
                txtdy.Text = dt.Rows[0]["DY"].ToString();
                txtcpm.Text = dt.Rows[0]["CPM"].ToString();
                txtph.Text = dt.Rows[0]["PH"].ToString();
                txtsh.Text = dt.Rows[0]["SH"].ToString();
                txtOilrpm.Text = dt.Rows[0]["OIL_RPM"].ToString();                
                txtdryden.Text = dt.Rows[0]["DRY_DEN"].ToString();
                txtelg.Text = dt.Rows[0]["ELG"].ToString();
                txtgpd.Text = dt.Rows[0]["GPD"].ToString();
                txtopu.Text = dt.Rows[0]["OPU"].ToString();
                txtRotoPressure.Text = dt.Rows[0]["ROTO_PRESSURE"].ToString();
                txtJetNo.Text = dt.Rows[0]["JET_NO"].ToString();
                txtDoffTime.Text = dt.Rows[0]["DOFF_TIME"].ToString();
                txtDoffWeight.Text = dt.Rows[0]["DOFF_WEIGHT"].ToString();
                txtplt.Text = dt.Rows[0]["PLT"].ToString();
                txttint.Text = dt.Rows[0]["TINT"].ToString();
                ddlPurpose.Text = dt.Rows[0]["PURPOSE"].ToString();
                txtOilden.Text = dt.Rows[0]["OIL_DEN"].ToString();                   
                ddlMacCode.SelectedValue = dt.Rows[0]["MACHINE_NAME"].ToString();
                txtHeatSetting.Text = dt.Rows[0]["HEAT_SETTING"].ToString();
                ddlLotType.SelectedValue = dt.Rows[0]["LOT_TYPE"].ToString();
                txtt1_1.Text = dt.Rows[0]["T1"].ToString();
                txtt1_2.Text = dt.Rows[0]["T1_L"].ToString();
                txtt1_3.Text = dt.Rows[0]["T1_H"].ToString();
                txtt1_4.Text = dt.Rows[0]["T2"].ToString();
                txtRatio.Text = dt.Rows[0]["RATIO_T1_H"].ToString();
                txtRatio1.Text = dt.Rows[0]["RATIO_T2_L"].ToString();
                ddlFinishedType.SelectedIndex = ddlFinishedType.Items.IndexOf(ddlFinishedType.Items.FindByValue(dt.Rows[0]["FINISHED_TYPE"].ToString()));

                string CommandText1 = "SELECT   FIBER_CODE,FIBER_DESC  FROM   TX_FIBER_MASTER  WHERE   (   UPPER (FIBER_CODE) LIKE :SearchQuery          OR UPPER (FIBER_DESC) LIKE :SearchQuery     OR UPPER (FIBER_CAT) LIKE :SearchQuery          OR UPPER (SUB_FIBER_CAT) LIKE :SearchQuery)    ";
                DataTable data1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, "", "", "", "%", "");
                txtItemCode.DataSource = data1;
                txtItemCode.DataTextField = "FIBER_DESC";
                txtItemCode.DataValueField = "FIBER_CODE";
                txtItemCode.DataBind();
                foreach (ComboBoxItem item in txtItemCode.Items)
                {
                    if (item.Value == dt.Rows[0]["POY"].ToString().Trim() && item.Text == dt.Rows[0]["POY_DESC"].ToString().Trim())
                    {
                        //txtItemCode.SelectedIndex = txtItemCode.Items.IndexOf(item);
                        txtItemCode.SelectedText = item.Text;
                        txtItemCode.SelectedValue = item.Value ;

                        break;
                    }
                }





                string CommandText3 = "SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC,OLD_MACHINE_NAME FROM v_MC_MACHINE_MASTER   WHERE MACHINE_TYPE='TEXTURISING' AND (MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery) ORDER BY MACHINE_CODE ASC)";
                DataTable data3 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText3, "", "", "", "%", "");
                ddlMacCode.DataSource = data3;
                ddlMacCode.DataTextField = "MACHINE_CODE";
                ddlMacCode.DataValueField = "MACHINE_CODE";
                ddlMacCode.DataBind();
                foreach (ComboBoxItem item in ddlMacCode.Items)
                {
                    if (item.Value == dt.Rows[0]["MACHINE_NAME"].ToString() && item.Text == dt.Rows[0]["MACHINE_NAME"].ToString())
                    {
                        //ddlMacCode.SelectedIndex = ddlMacCode.Items.IndexOf(item);
                        ddlMacCode.SelectedText = item.Text;
                        ddlMacCode.SelectedValue = item.Value;
                        break;
                    }
                }               


               // string CommandText = "SELECT   'WHITE' SHADE_FAMILY,'WHITE' SHADE_CODE,      'IND_TYPE' IND_TYPE, i.YARN_CODE,  i.YARN_TYPE,    i.YARN_desc,   (   i.YARN_CODE   || '@' || i.YARN_desc    || '@'     || 'WHITE'    || '@'   || 'WHITE')         COMBINED     FROM   YRN_MST i    WHERE   UPPER (YARN_CODE) LIKE :SearchQuery          OR UPPER (YARN_desc) LIKE :SearchQuery    ";
                string CommandText = "SELECT   I.YARN_SHADE AS SHADE_FAMILY, I.YARN_SHADE AS SHADE_CODE, M.ASS_YARN_CODE YARN_CODE,   i.YARN_TYPE, M.ASS_YARN_desc YARN_desc, (   M.ASS_YARN_CODE || '@'|| M.ASS_YARN_desc || '@'|| I.YARN_SHADE  || '@'  || I.YARN_SHADE)   COMBINED FROM   YRN_MST i,YRN_ASSOCATED_MST M WHERE   I.YARN_CAT = 'TEXTURISED'    AND I.YARN_CODE=M.YARN_CODE    AND (UPPER (M.ASS_YARN_CODE) LIKE :SearchQuery      OR UPPER (M.ASS_YARN_desc) LIKE :SearchQuery)   ";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", "", "", "%", "");
                cmbYarn.DataSource = data;
                cmbYarn.DataTextField = "YARN_DESC";
                cmbYarn.DataValueField = "YARN_CODE";
                cmbYarn.DataBind();
                foreach (ComboBoxItem item in cmbYarn.Items)
                {
                    if (item.Value == dt.Rows[0]["FINISHED_DENIER"].ToString().Trim() && item.Text == dt.Rows[0]["FINISHED_DENIER_DESC"].ToString().Trim())
                    {
                        //cmbYarn.SelectedIndex = cmbYarn.Items.IndexOf(item);      
                        cmbYarn.SelectedText = item.Text;
                        cmbYarn.SelectedValue = item.Value;
                        break;
                    }
                }


                //string CommandText2 = "SELECT * FROM (SELECT DISTINCT  LOT_NO,PRTY_NAME FROM   v_TX_FIBER_IR_TRN  WHERE  FIBER_CODE LIKE '" + dt.Rows[0]["POY"].ToString().Trim() + "' AND  (UPPER(LOT_NO) LIKE :SearchQuery OR UPPER(PRTY_NAME) LIKE  :SearchQuery OR UPPER(FIBER_DESC) LIKE :SearchQuery OR UPPER(FIBER_CODE) LIKE :SearchQuery OR UPPER(PRTY_CODE) LIKE  :SearchQuery )  )   ";

                string CommandText2 = "SELECT   T.MST_CODE AS LOT_NO, T.MST_DESC,V.PRTY_NAME AS PRTY_NAME  FROM   TX_MASTER_TRN T,TX_VENDOR_MST V WHERE    T.CODE_PREFIX=V.PRTY_CODE(+) AND T.del_status = '0'         AND TRIM (RTRIM (T.MST_NAME)) = LTRIM (RTRIM ('MERGE_NO'))    AND T.OTHER_INFO='" + dt.Rows[0]["POY"].ToString().Trim() + "'      AND (UPPER (T.MST_CODE) LIKE :SearchQuery              OR UPPER (T.MST_DESC) LIKE :SearchQuery OR UPPER(V.PRTY_NAME) LIKE :SearchQuery  )      ";
                DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, "", "", "", "%", "");
                cmbLotNo.DataSource = data2;
                cmbLotNo.DataTextField = "LOT_NO";
                cmbLotNo.DataValueField = "PRTY_NAME";
                cmbLotNo.DataBind();
                foreach (ComboBoxItem item in cmbLotNo.Items)
                {
                    if (item.Text == dt.Rows[0]["MERGE_NO"].ToString().Trim() && item.Value == dt.Rows[0]["PRTY_NAME"].ToString().Trim())
                    {
                        cmbLotNo.SelectedIndex = cmbLotNo.Items.IndexOf(item);
                        //cmbLotNo.SelectedText = item.Text;
                        //cmbLotNo.SelectedValue = item.Value;
                        lblPartyName.Text = dt.Rows[0]["PRTY_NAME"].ToString();
                        break;
                    }
                }


            }
            else
            {
                CommonFuction.ShowMessage("Invalid LOT provided");
            }

        }
        catch (Exception ex)
        {
            
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading ..\r\nSee error log for detail."));
        }
    }





    //protected void cmbFindLotNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        string CommandText = "SELECT   POY,MERGE_NO, LOT_NO, MACHINE_NAME FROM   (SELECT  * FROM   YRN_LOT_MAKING WHERE   CONF_FLAG = 0) asd";
    //        string WhereClause = "  where LOT_NO like :SearchQuery or MERGE_NO like :SearchQuery ";
    //        string SortExpression = " order by POY asc";
    //        string SearchQuery = e.Text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");  
    //        cmbFindLotNo.DataSource = data;
    //        cmbFindLotNo.DataTextField = "LOT_NO";
    //        cmbFindLotNo.DataValueField = "LOT_NO";
    //        cmbFindLotNo.DataBind();

    //        e.ItemsLoadedCount = data.Rows.Count;
    //        e.ItemsCount = data.Rows.Count;
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Vendors..\r\nSee error log for detail."));
    //    }
    //}
  
    
    //private void ActivateUpdateMode()
    //{
    //    try
    //    {
    //      //  txtFindLotNo.Text = string.Empty;

    //        cmbFindLotNo.SelectedIndex = 0;
    //        cmbFindLotNo.SelectedText = string.Empty;
    //        cmbFindLotNo.SelectedValue = string.Empty;
    //        cmbFindLotNo.Items.Clear();
    //        cmbFindLotNo.Visible = true;


    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }


    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
      
        try        {

            string URL = "../../Production/Report/LotMakingReport.aspx?PRODUCT_CATEGORY=" + PRODUCT_CATEGORY;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);


        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Data.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ddlPOYDenier_SelectedIndexChanged(object sender, EventArgs e)
    {

    }




    private DataTable GetLotNoItems(string text, int startOffset)
    {
        try
        {

          string CommandText = "SELECT * FROM (SELECT DISTINCT  LOT_NO,PRTY_NAME FROM   v_TX_FIBER_IR_TRN  WHERE  FIBER_CODE LIKE '"+txtItemCode.SelectedValue+"' AND  (UPPER(LOT_NO) LIKE :SearchQuery OR UPPER(PRTY_NAME) LIKE  :SearchQuery OR UPPER(FIBER_DESC) LIKE :SearchQuery OR UPPER(FIBER_CODE) LIKE :SearchQuery OR UPPER(PRTY_CODE) LIKE  :SearchQuery )  ) WHERE  ROWNUM <= 15 ";
            
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " WHERE  LOT_NO NOT IN  ( SELECT LOT_NO FROM (SELECT DISTINCT  LOT_NO,PRTY_NAME FROM   v_TX_FIBER_IR_TRN  WHERE FIBER_CODE LIKE '" + txtItemCode.SelectedValue + "' AND   (UPPER(LOT_NO) LIKE :SearchQuery OR UPPER(PRTY_NAME) LIKE  :SearchQuery OR UPPER(FIBER_DESC) LIKE :SearchQuery OR UPPER(FIBER_CODE) LIKE :SearchQuery OR UPPER(PRTY_CODE) LIKE  :SearchQuery )   ) WHERE  ROWNUM <= " + startOffset + ")";
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

            string CommandText = " SELECT DISTINCT  LOT_NO FROM   v_TX_FIBER_IR_TRN  WHERE FIBER_CODE LIKE '" + txtItemCode.SelectedValue + "'  AND   (UPPER(LOT_NO) LIKE :SearchQuery OR UPPER(PRTY_NAME) LIKE  :SearchQuery OR UPPER(FIBER_DESC) LIKE :SearchQuery OR UPPER(FIBER_CODE) LIKE :SearchQuery OR UPPER(PRTY_CODE) LIKE  :SearchQuery )  ";
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



    protected void cmbLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            //DataTable data = GetLotNoItems(e.Text.ToUpper(), e.ItemsOffset);

            //if (data != null && data.Rows.Count > 0)
            //{
            //    cmbLotNo.Items.Clear();
            //    cmbLotNo.DataSource = data;
            //    cmbLotNo.DataBind();
            //}
            //e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            //e.ItemsCount = GetLotCounts(e.Text);
            DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "MERGE_NO");
            cmbLotNo.Items.Clear();
            cmbLotNo.DataSource = data;
            cmbLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetMOMCount(e.Text, "MERGE_NO");

        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

   protected void cmbFindLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetFindLotNoItems(e.Text.ToUpper(), e.ItemsOffset);

            if (data != null && data.Rows.Count > 0)
            {
                cmbFindLotNo.Items.Clear();
                cmbFindLotNo.DataSource = data;
                cmbFindLotNo.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetFindLotCounts(e.Text);

        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }
    private DataTable GetFindLotNoItems(string text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT * FROM (SELECT DISTINCT  LOT_NO FROM   YRN_LOT_MAKING WHERE  NVL(CONF_FLAG,0)!=3 AND  PRODUCT_CATEGORY='"+PRODUCT_CATEGORY +"'  AND   (UPPER(LOT_NO) LIKE :SearchQuery ) ) WHERE  ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND  LOT_NO NOT IN  ( SELECT LOT_NO FROM (SELECT DISTINCT  LOT_NO FROM   YRN_LOT_MAKING WHERE  NVL(CONF_FLAG,0)!=3 AND  PRODUCT_CATEGORY='" + PRODUCT_CATEGORY + "' AND    (UPPER(LOT_NO) LIKE :SearchQuery ) ) WHERE  ROWNUM <= " + startOffset + ")";
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


    private int GetFindLotCounts(string text)
    {
        try
        {

            string CommandText = " SELECT DISTINCT  LOT_NO FROM   YRN_LOT_MAKING WHERE  NVL(CONF_FLAG,0)!=3 AND  PRODUCT_CATEGORY='" + PRODUCT_CATEGORY + "'  AND  (UPPER(LOT_NO) LIKE :SearchQuery )  ";
            string whereClause = string.Empty;
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }

        catch
        {

            throw;
        }
    }


    private DataTable GetMOMData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT   T.MST_CODE AS LOT_NO, T.MST_DESC,V.PRTY_NAME AS PRTY_NAME  FROM   TX_MASTER_TRN T,TX_VENDOR_MST V WHERE    T.CODE_PREFIX=V.PRTY_CODE(+) AND T.del_status = '0'         AND TRIM (RTRIM (T.MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))   AND T.OTHER_INFO='" + txtItemCode.SelectedValue+ "'       AND (UPPER (T.MST_CODE) LIKE :SearchQuery              OR UPPER (T.MST_DESC) LIKE :SearchQuery OR UPPER(V.PRTY_NAME) LIKE :SearchQuery  )         AND ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND MST_CODE NOT IN ( SELECT   T.MST_CODE AS LOT_NO, T.MST_DESC,V.PRTY_NAME AS PRTY_NAME  FROM   TX_MASTER_TRN T,TX_VENDOR_MST V WHERE    T.CODE_PREFIX=V.PRTY_CODE(+) AND T.del_status = '0'         AND TRIM (RTRIM (T.MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))     AND T.OTHER_INFO='" + txtItemCode.SelectedValue + "'      AND (UPPER (T.MST_CODE) LIKE :SearchQuery              OR UPPER (T.MST_DESC) LIKE :SearchQuery OR UPPER(V.PRTY_NAME) LIKE :SearchQuery  )        AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by MST_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
        }
        catch
        {
            throw;
        }
    }
    protected int GetMOMCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))      AND T.OTHER_INFO='" + txtItemCode.SelectedValue + "'     AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {


        {
            try
            {                              
                if (!string.IsNullOrEmpty(txtItemCode.SelectedValue) && !string.IsNullOrEmpty(cmbLotNo.SelectedText) && !string.IsNullOrEmpty(ddlMacCode.SelectedValue) && !string.IsNullOrEmpty(cmbYarn.SelectedValue) && !string.IsNullOrEmpty(txtLotNo.Text))
                {
                    UpdateLotData();
                }
                else
                {
                    CommonFuction.ShowMessage("Please select Poy/Merge/Machine/Finish Denier/Lot No.");
                }

            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating..\r\nSee error log for detail."));
            }
        }

    }

    private void UpdateLotData()
    {
        string LOT_NO = string.Empty;
        try
        {
            oYRN_LOT_MAKING = new SaitexDM.Common.DataModel.YRN_LOT_MAKING();
            oYRN_LOT_MAKING.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_LOT_MAKING.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_LOT_MAKING.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;          
            oYRN_LOT_MAKING.POY = txtItemCode.SelectedValue;
            oYRN_LOT_MAKING.MERGE_NO = cmbLotNo.SelectedText;
            oYRN_LOT_MAKING.PRODUCT_CATEGORY = PRODUCT_CATEGORY;
            oYRN_LOT_MAKING.FINISHED_TYPE = ddlFinishedType.SelectedValue;
            oYRN_LOT_MAKING.FINISHED_DENIER = cmbYarn.SelectedValue ;           
            oYRN_LOT_MAKING.LOT_NO = txtLotNo.Text.Trim();
            double MachineSpeed = 0;
            double.TryParse(txtMachineSpeed.Text, out MachineSpeed);
            oYRN_LOT_MAKING.MACHINE_SPEED = MachineSpeed;
            double dr = 0;
            double.TryParse(txtdr.Text, out dr);
            oYRN_LOT_MAKING.DR = dr;
            double tkp = 0;
            double.TryParse(txttkp.Text, out tkp);
            oYRN_LOT_MAKING.TKP = tkp;
            double sof = 0;
            double.TryParse(txtsof.Text, out sof);
            oYRN_LOT_MAKING.SOF = sof;
            double dy = 0;
            double.TryParse(txtdy.Text, out dy);
            oYRN_LOT_MAKING.DY = dy;
            double cpm = 0;
            double.TryParse(txtcpm.Text, out cpm);
            oYRN_LOT_MAKING.CPM = cpm;
            double ph = 0;
            double.TryParse(txtph.Text, out ph);
            oYRN_LOT_MAKING.PH = ph;
            double sh = 0;
            double.TryParse(txtsh.Text, out sh);
            oYRN_LOT_MAKING.SH = sh;
            oYRN_LOT_MAKING.OIL_RPM = txtOilrpm.Text.Trim();            
            double dryden = 0;
            double.TryParse(txtdryden.Text, out dryden);
            oYRN_LOT_MAKING.DRY_DEN = dryden;

            oYRN_LOT_MAKING.ELG = txtelg.Text.Trim();
            double gpd = 0;
            double.TryParse(txtgpd.Text, out gpd);
            oYRN_LOT_MAKING.GPD = gpd;
            double opu = 0;
            double.TryParse(txtopu.Text, out opu);
            oYRN_LOT_MAKING.OPU = opu;
            oYRN_LOT_MAKING.ROTO_PRESSURE = txtRotoPressure.Text.Trim();
            oYRN_LOT_MAKING.JET_NO = txtJetNo.Text.Trim();
            double DoffTime = 0;
            double.TryParse(txtDoffTime.Text, out DoffTime);
            oYRN_LOT_MAKING.DOFF_TIME = DoffTime;
            double DoffWeight = 0;
            double.TryParse(txtDoffWeight.Text, out DoffWeight);
            oYRN_LOT_MAKING.DOFF_WEIGHT = DoffWeight;
            oYRN_LOT_MAKING.PLT = txtplt.Text.Trim();

            oYRN_LOT_MAKING.TINT = txttint.Text.Trim();

            oYRN_LOT_MAKING.PURPOSE = ddlPurpose.Text.Trim();
            oYRN_LOT_MAKING.OIL_DEN = txtOilden.Text.Trim();
            oYRN_LOT_MAKING.TUSER = oUserLoginDetail.UserCode;
            oYRN_LOT_MAKING.TDATE = DateTime.Now ;
           oYRN_LOT_MAKING.MACHINE_NAME = ddlMacCode.SelectedValue;
            oYRN_LOT_MAKING.HEAT_SETTING = txtHeatSetting.Text.Trim();
            oYRN_LOT_MAKING.LOT_TYPE = ddlLotType.SelectedValue.Trim();
            oYRN_LOT_MAKING.CONF_FLAG = 0;
            double t1_1 = 0;
            double.TryParse(txtt1_1.Text, out t1_1);
            oYRN_LOT_MAKING.T1 = t1_1;
            double t1_3 = 0;
            double.TryParse(txtt1_3.Text, out t1_3);
            oYRN_LOT_MAKING.T1_H = t1_3;
            double t1_2 = 0;
            double.TryParse(txtt1_2.Text, out t1_2);
            oYRN_LOT_MAKING.T1_L = t1_1;
            double t1_4 = 0;
            double.TryParse(txtt1_4.Text, out t1_4);
            oYRN_LOT_MAKING.T2 = t1_4;


            double Ratio = 0;
            double.TryParse(txtRatio.Text, out Ratio);
            oYRN_LOT_MAKING.RATIO_T1_H = Ratio;
            double Ratio1 = 0;
            double.TryParse(txtRatio1.Text, out Ratio1);
            oYRN_LOT_MAKING.RATIO_T2_L = Ratio1;    
            bool Result = SaitexBL.Interface.Method.YRN_LOT_MAKING.UpdateLotMaking(oYRN_LOT_MAKING);
            if (Result)
            {
                InitialisePage();
                string msg = "Yarn Lot Making " + LOT_NO + " Successfully Updated .";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent saving Failed');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void txtItemCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
      //cmbLotNo.SelectedIndex = -1;

    }

    protected void cmbYarn_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = BindYarnCodeCombo(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbYarn.Items.Clear();

                cmbYarn.DataSource = data;
                cmbYarn.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable BindYarnCodeCombo(string text, int startOffset)
    {
        try
        {


            //string CommandText = "SELECT   YARN_SHADE AS SHADE_FAMILY, YARN_SHADE AS  SHADE_CODE,  i.YARN_CODE,  i.YARN_TYPE,    i.YARN_desc,   (   i.YARN_CODE   || '@' || i.YARN_desc    || '@'     || YARN_SHADE    || '@'   || YARN_SHADE)         COMBINED     FROM   YRN_MST i    WHERE  YARN_CAT ='TEXTURISED' AND  (UPPER (YARN_CODE) LIKE :SearchQuery          OR UPPER (YARN_desc) LIKE :SearchQuery)      AND    ROWNUM <= 15";
            string CommandText = "SELECT   I.YARN_SHADE AS SHADE_FAMILY, I.YARN_SHADE AS SHADE_CODE, M.ASS_YARN_CODE YARN_CODE,   i.YARN_TYPE, M.ASS_YARN_desc YARN_desc, (   M.ASS_YARN_CODE || '@'|| M.ASS_YARN_desc || '@'|| I.YARN_SHADE  || '@'  || I.YARN_SHADE)   COMBINED FROM   YRN_MST i,YRN_ASSOCATED_MST M WHERE   I.YARN_CAT = 'TEXTURISED'    AND I.YARN_CODE=M.YARN_CODE    AND (UPPER (M.ASS_YARN_CODE) LIKE :SearchQuery      OR UPPER (M.ASS_YARN_desc) LIKE :SearchQuery)         AND ROWNUM <= 15";

            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                // whereClause += " AND YARN_CODE NOT IN    (      SELECT   i.YARN_CODE   FROM   YRN_MST i    WHERE  YARN_CAT ='TEXTURISED' AND (UPPER (YARN_CODE) LIKE :SearchQuery          OR UPPER (YARN_desc) LIKE :SearchQuery)       AND     ROWNUM <= " + startOffset + " )   ";
                   whereClause += "AND YARN_CODE NOT IN    (     SELECT    M.ASS_YARN_CODE YARN_CODE FROM   YRN_MST i,YRN_ASSOCATED_MST M WHERE   I.YARN_CAT = 'TEXTURISED'    AND I.YARN_CODE=M.YARN_CODE    AND (UPPER (M.ASS_YARN_CODE) LIKE :SearchQuery      OR UPPER (M.ASS_YARN_desc) LIKE :SearchQuery)         AND     ROWNUM <= " + startOffset + " )   ";
               
            }

            string SortExpression = "order by YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToString(), "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text)
    {


        //string CommandText = " SELECT  i.YARN_CODE     FROM   YRN_MST i    WHERE YARN_CAT ='TEXTURISED' AND  (UPPER (YARN_CODE) LIKE :SearchQuery          OR UPPER (YARN_desc) LIKE :SearchQuery )  ";
        string CommandText = "SELECT   I.YARN_SHADE AS SHADE_FAMILY, I.YARN_SHADE AS SHADE_CODE, M.ASS_YARN_CODE YARN_CODE,   i.YARN_TYPE, M.ASS_YARN_desc YARN_desc, (   M.ASS_YARN_CODE || '@'|| M.ASS_YARN_desc || '@'|| I.YARN_SHADE  || '@'  || I.YARN_SHADE)   COMBINED FROM   YRN_MST i,YRN_ASSOCATED_MST M WHERE   I.YARN_CAT = 'TEXTURISED'    AND I.YARN_CODE=M.YARN_CODE    AND (UPPER (M.ASS_YARN_CODE) LIKE :SearchQuery      OR UPPER (M.ASS_YARN_desc) LIKE :SearchQuery)   ";
        string WhereClause = "  ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
    }

    protected void txtt1_1_TextChanged(object sender, EventArgs e)
    {
        getRatio();
        txtt1_3.Focus();
    }

    protected void getRatio()
    {
        double t1_1 = 0;
        double t1_2 = 0;
        double t2_1 = 0;
        double t2_2 = 0;
        double.TryParse(txtt1_1.Text, out t1_1);
        double.TryParse(txtt1_2.Text, out t1_2);
        double.TryParse(txtt1_3.Text, out t2_1);
        double.TryParse(txtt1_4.Text, out t2_2);
        if (t1_1 > 0 && t1_2 > 0)
        {
            txtRatio.Text = Math.Round((t1_2 / t1_1), 2).ToString();
          

        }
        if (t2_1 > 0 && t2_2 > 0)
        {

            txtRatio1.Text = Math.Round((t2_2 / t2_1), 2).ToString();
            

        }
     


    }
    
    protected void txtt1_3_TextChanged(object sender, EventArgs e)
    {
        getRatio();
        txtt1_2.Focus();
    }
    protected void txtt1_2_TextChanged(object sender, EventArgs e)
    {
        getRatio();
        txtt1_4.Focus();
    }
    protected void txtt1_4_TextChanged(object sender, EventArgs e)
    {
        getRatio();
        txtLotNo.Focus();
    }
    protected void cmbLotNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        lblPartyName.Text = cmbLotNo.SelectedValue;
    }
    
}
