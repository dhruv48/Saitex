using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SaitexDM.Common.DataModel;

using errorLog;
using Obout.ComboBox;
public partial class Module_Fiber_Controls_FIBER_OPENING_BAL_PARTY : System.Web.UI.UserControl
{    DataTable dtOP_TRN_SUB = null;
DataTable dtOP_TRN = null;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
       
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialControls();             

                if (Session["dtOP_TRN_SUB"] != null)
                {
                    DataTable dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
                    if (dtOP_TRN_SUB.Rows.Count > 0)
                    {
                        grdsub_trn.DataSource = dtOP_TRN_SUB;
                        grdsub_trn.DataBind();
                    }

                }
                else
                {
                    grdsub_trn.DataSource = null;
                    grdsub_trn.DataBind();
                }
            }


           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControls()
    {
       
        DDLFiberCode.SelectedIndex = -1;
        DDLFiberCode.Visible = false;        
        txtdescription.Text = string.Empty;    
        Session["dtOP_TRN_SUB"] = null;
        ViewState["dtDetailTBL"] = null;
        grdsub_trn.DataSource = null;
        grdsub_trn.DataBind();
        txtPartyCode.SelectedIndex = -1;
        txtPartyName.Text = string.Empty;
       
        imgbtnUpdate.Visible = true;
        txtFiberCode.Visible = false;
        DDLFiberCode.Visible = true;       
        BindIntial();
    }

   
    private void UpdateData()
    {
        int iRecordFound = 0;
        try
        {
            if (Page.IsValid)
            {
                SaitexDM.Common.DataModel.Fiber_Master_new oFiber_Master_new = new SaitexDM.Common.DataModel.Fiber_Master_new();

                oFiber_Master_new.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFiber_Master_new.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFiber_Master_new.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFiber_Master_new.FIBER_CODE = CommonFuction.funFixQuotes(txtFiberCode.Text.Trim().ToUpper());
                oFiber_Master_new.UOM = "KG";
                oFiber_Master_new.UOM1 = "KG";
                oFiber_Master_new.UOM_BAIL = "KG";
                oFiber_Master_new.TUSER = oUserLoginDetail.UserCode;
                oFiber_Master_new.TDATE = System.DateTime.Now;
                oFiber_Master_new.STATUS = "1";
                oFiber_Master_new.PRTY_CODE = txtPartyCode.SelectedText;
                oFiber_Master_new.OP_BAL_STOCK = 0;
                oFiber_Master_new.OP_RATE = 0;
                oFiber_Master_new.OP_BAL_PRTY_STOK = 0;
                oFiber_Master_new.OP_QTY_ADJ = 0;
                oFiber_Master_new.FIB_ISS = "0";
                oFiber_Master_new.FIB_RCPT = "0";
                oFiber_Master_new.CUR_RATE = 0;
                oFiber_Master_new.WT_AVRG_RATE = 0;
                oFiber_Master_new.LAST_PO_RATE = 0;

                //bool bResult = SaitexBL.Interface.Method.Fiber_Master_new.UpdateFiberMstData(oFiber_Master_new, out iRecordFound);
                bool trnResult = updateTrnData();
                if (trnResult)
                {

                    Common.CommonFuction.ShowMessage("Record Update Successfully...");
                    InitialControls();
                  
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Error While Update Record...");
                    InitialControls();
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    
  
  
    protected void DDLFiberCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        
        DataTable data = GetFiberData(e.Text.ToUpper(), e.ItemsOffset);
            DDLFiberCode.Items.Clear();
            DDLFiberCode.DataSource = data;
            DDLFiberCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetFiberCount(e.Text);
        }
    private DataTable GetFiberData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT DISTINCT LTRIM (RTRIM (FIBER_CODE)) AS FIBER_CODE , FIBER_DESC FROM TX_FIBER_MASTER    where (upper(FIBER_CODE) like :SearchQuery  or upper(FIBER_DESC)  like :SearchQuery OR upper(FIBER_CAT)  like  :SearchQuery OR  upper(SUB_FIBER_CAT)  LIKE  :SearchQuery) AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND FIBER_CODE NOT IN ( SELECT DISTINCT LTRIM (RTRIM (FIBER_CODE)) AS FIBER_CODE  FROM TX_FIBER_MASTER    where (upper(FIBER_CODE) like :SearchQuery  or upper(FIBER_DESC)  like :SearchQuery OR upper(FIBER_CAT)  like  :SearchQuery OR  upper(SUB_FIBER_CAT)  LIKE  :SearchQuery)   AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by FIBER_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetFiberCount(string text)
    {

        string CommandText = " SELECT DISTINCT LTRIM (RTRIM (FIBER_CODE)) AS FIBER_CODE  FROM TX_FIBER_MASTER    where (upper(FIBER_CODE) like :SearchQuery  or upper(FIBER_DESC)  like :SearchQuery OR upper(FIBER_CAT)  like  :SearchQuery OR  upper(SUB_FIBER_CAT)  LIKE  :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }




   

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpdateData();
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
       
        imgbtnUpdate.Visible = true;
        txtFiberCode.Visible = false;
        DDLFiberCode.Visible = true;

       
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialControls();
       
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
        }
    }

    protected void DDLFiberCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDataByFiberCode(DDLFiberCode.SelectedText);
        ViewState["iFIBER_CODE"] = DDLFiberCode.SelectedValue.ToString();
    }

    private void GetDataByFiberCode(string iFIBER_CODE)
    {
        try
        {
           
            
            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetDataByFiberCode(iFIBER_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtFiberCode.Text = dt.Rows[0]["FIBER_CODE"].ToString().Trim();               
                txtdescription.Text = dt.Rows[0]["FIBER_DESC"].ToString().Trim();
                //************************* COMMENTED By Nishant Rai at 26-07-2013 ****************************//             


                SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
                oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_FIBER_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                oTX_FIBER_IR_MST.TRN_TYPE = "OJB01";
                //int TRNNUMBer = 0;
                //int.TryParse(SaitexDL.Interface.Method.TX_FIBER_IR_MST.GetMRNNumber(oTX_FIBER_IR_MST, txtFiberCode.Text).ToString(),out TRNNUMBer);
                Session["dtOP_TRN_SUB"] = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetSUBTRN_DataByFiberCode(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtFiberCode.Text, "OJB01");
                ViewState["dtDetailTBL"] = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetTRN_DataByFiberCode(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtFiberCode.Text, "OJB01");
               
                  MapTrnDataTable();
                  MapDataTable();
                  BindBOMGrid();





               
                //************************* Added By Nishant Rai at 27-07-2013 ****************************//
            

            }
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
  private void MapTrnDataTable()
        {
            try
            {
                DataTable dtOP_TRN_SUB = null;
                if (Session["dtOP_TRN_SUB"] != null)
                {
                    dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
                }
                if (!dtOP_TRN_SUB.Columns.Contains("UNIQUE_ID"))
                    dtOP_TRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
                if (!dtOP_TRN_SUB.Columns.Contains("PI_NO"))
                    dtOP_TRN_SUB.Columns.Add("PI_NO", typeof(string));

                for (int iLoop = 0; iLoop < dtOP_TRN_SUB.Rows.Count; iLoop++)
                {
                    dtOP_TRN_SUB.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
                    dtOP_TRN_SUB.Rows[iLoop]["PI_NO"] = "NA";
                }
                dtOP_TRN_SUB.AcceptChanges();
                Session["dtOP_TRN_SUB"] = dtOP_TRN_SUB;
            }
            catch
            {
                throw;
            }
        }

  private void MapDataTable()
  {
      try
      {
          DataTable dtDetailTBL = null;
          if (ViewState["dtDetailTBL"] != null)
          {
              dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
          }
          if (!dtDetailTBL.Columns.Contains("UNIQUE_ID"))
              dtDetailTBL.Columns.Add("UNIQUE_ID", typeof(int));
          if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
              dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
          if (!dtDetailTBL.Columns.Contains("PI_NO"))
              dtDetailTBL.Columns.Add("PI_NO", typeof(string));

          for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
          {
              dtDetailTBL.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
              dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;     
          }
          dtDetailTBL.AcceptChanges();
          ViewState["dtDetailTBL"] = dtDetailTBL;
      }
      catch
      {
          throw;
      }
  }
   
    

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
           string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME ) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT   PRTY_CODE   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }


    //************************* Added By Nishant Rai at 26-07-2013 ****************************//
    protected void txtPartyCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode.Items.Clear();
            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyName.Text = txtPartyCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
       string Query_String = string.Empty;
        try
        {
            string URL = "../../Fiber/Reports/FiberOpeingBalanceReport.aspx?Query_String =" + Query_String;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);


        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }
     
    }
    


    public bool insertTrnData()
    {

        Hashtable htReceive = new Hashtable();
        SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
        oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oTX_FIBER_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
        oTX_FIBER_IR_MST.FORM_NUMB = "";
        oTX_FIBER_IR_MST.FORM_TYPE = "";

        DateTime dt = System.DateTime.Now.Date;
        bool Is_Gate_Entry = false;
        Is_Gate_Entry = DateTime.TryParse(DateTime.MinValue.ToString().Trim(), out dt);
        htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
        oTX_FIBER_IR_MST.GATE_DATE = dt;
        oTX_FIBER_IR_MST.GATE_NUMB = "";
        oTX_FIBER_IR_MST.GATE_OUT_NUMB = "";
        oTX_FIBER_IR_MST.GATE_PASS_TYPE = "";
        oTX_FIBER_IR_MST.LORY_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_LR = false;
        Is_LR = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("LR", Is_LR);
        oTX_FIBER_IR_MST.LR_DATE = dt;

        oTX_FIBER_IR_MST.LR_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Party_challan = false;
        Is_Party_challan = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
        oTX_FIBER_IR_MST.PRTY_CH_DATE = dt;


        oTX_FIBER_IR_MST.PRTY_CH_NUMB ="";
        oTX_FIBER_IR_MST.PRTY_CODE = "";
        oTX_FIBER_IR_MST.PRTY_NAME = "";
        oTX_FIBER_IR_MST.RCOMMENT ="";
        oTX_FIBER_IR_MST.REPROCESS ="";
        oTX_FIBER_IR_MST.SHIFT = "";


        dt = System.DateTime.Now.Date;
        bool Is_MRN = false;
        Is_MRN = DateTime.TryParse(DateTime.Now.ToString() , out dt);
        htReceive.Add("MRN", Is_MRN);
        
        
        oTX_FIBER_IR_MST.TRSP_CODE = "";

        oTX_FIBER_IR_MST.TUSER = oUserLoginDetail.UserCode;
        int TRN_NUMB = 0;

        oTX_FIBER_IR_MST.BILL_NUMB ="";


        dt = System.DateTime.Now.Date;
        bool Is_Bill_Date = false;
        Is_Bill_Date = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("BILL_DATE", Is_Bill_Date);
        oTX_FIBER_IR_MST.BILL_DATE = dt;


        oTX_FIBER_IR_MST.BILL_TYPE = "FSP";
        oTX_FIBER_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;


        
        oTX_FIBER_IR_MST.EXCISE_TYPE = string.Empty ;
        oTX_FIBER_IR_MST.SPINNERS = string.Empty;
        oTX_FIBER_IR_MST.TRN_DATE = dt;
        oTX_FIBER_IR_MST.TRN_TYPE = "OJB01";
        DataTable dtOP_TRN_SUB = null;
        if (Session["dtOP_TRN_SUB"] != null)
        {
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
        }
        //creteTrnData();
        DataTable dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
        bool _result = false;
        for (int i = 0; i< dtDetailTBL.Rows.Count; i++)
        {
            oTX_FIBER_IR_MST.PRTY_CODE = dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() ;
            oTX_FIBER_IR_MST.PRTY_NAME = dtDetailTBL.Rows[i]["PRTY_NAME"].ToString();
            double totalAmt = 0;
            double.TryParse(dtDetailTBL.Rows[i]["AMOUNT"].ToString(), out totalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(dtDetailTBL.Rows[i]["AMOUNT"].ToString(), out finalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = finalAmt;
            oTX_FIBER_IR_MST.FINAL_AMOUNT = finalAmt;
            oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetNewMRNNumber(oTX_FIBER_IR_MST).ToString());
            DataView dvDetailTBL = new DataView(dtDetailTBL);
            dvDetailTBL.RowFilter = " FIBER_CODE='" + dtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'  and LOT_NO='" + dtDetailTBL.Rows[i]["LOT_NO"].ToString().ToUpper() + "' AND  PALLET_CODE='" + dtDetailTBL.Rows[i]["PALLET_CODE"].ToString().ToUpper() + "' AND PRTY_CODE='" + dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'";
            DataView dvOP_TRN_SUB = new DataView(dtOP_TRN_SUB);
            dvOP_TRN_SUB.RowFilter = " FIBER_CODE='" + dtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'  and LOT_NO='" + dtDetailTBL.Rows[i]["LOT_NO"].ToString().ToUpper() + "' AND  PALLET_CODE='" + dtDetailTBL.Rows[i]["PALLET_CODE"].ToString().ToUpper() + "' AND PRTY_CODE='" + dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'";

            _result = SaitexBL.Interface.Method.TX_FIBER_IR_MST.Insert(oTX_FIBER_IR_MST, out TRN_NUMB, dvDetailTBL.ToTable(), htReceive, dvOP_TRN_SUB.ToTable(), new DataTable(), new DataTable());
        }
        return _result;
    }

    public bool updateTrnData()
    {

        Hashtable htReceive = new Hashtable();
        SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
        oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oTX_FIBER_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
        oTX_FIBER_IR_MST.FORM_NUMB = "";
        oTX_FIBER_IR_MST.FORM_TYPE = "";

        DateTime dt = System.DateTime.Now.Date;
        bool Is_Gate_Entry = false;
        Is_Gate_Entry = DateTime.TryParse(DateTime.MinValue.ToString().Trim(), out dt);
        htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
        oTX_FIBER_IR_MST.GATE_DATE = dt;
        oTX_FIBER_IR_MST.GATE_NUMB = "";
        oTX_FIBER_IR_MST.GATE_OUT_NUMB = "";
        oTX_FIBER_IR_MST.GATE_PASS_TYPE = "";
        oTX_FIBER_IR_MST.LORY_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_LR = false;
        Is_LR = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("LR", Is_LR);
        oTX_FIBER_IR_MST.LR_DATE = dt;

        oTX_FIBER_IR_MST.LR_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Party_challan = false;
        Is_Party_challan = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
        oTX_FIBER_IR_MST.PRTY_CH_DATE = dt;


        oTX_FIBER_IR_MST.PRTY_CH_NUMB = "";
        oTX_FIBER_IR_MST.PRTY_CODE = txtPartyCode.SelectedText;
        oTX_FIBER_IR_MST.PRTY_NAME = txtPartyName.Text;
        oTX_FIBER_IR_MST.RCOMMENT = "";
        oTX_FIBER_IR_MST.REPROCESS = "";
        oTX_FIBER_IR_MST.SHIFT = "";


        dt = System.DateTime.Now.Date;
        bool Is_MRN = false;
        Is_MRN = DateTime.TryParse(DateTime.Now.ToString(), out dt);
        htReceive.Add("MRN", Is_MRN);
        oTX_FIBER_IR_MST.TRN_DATE = dt;
        oTX_FIBER_IR_MST.TRN_TYPE = "OJB01";          
        oTX_FIBER_IR_MST.TRSP_CODE = "";

        oTX_FIBER_IR_MST.TUSER = oUserLoginDetail.UserCode;
        int TRN_NUMB = 0;

        oTX_FIBER_IR_MST.BILL_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Bill_Date = false;
        Is_Bill_Date = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("BILL_DATE", Is_Bill_Date);
        oTX_FIBER_IR_MST.BILL_DATE = dt;


        oTX_FIBER_IR_MST.BILL_TYPE = "FSP";
        oTX_FIBER_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;

        oTX_FIBER_IR_MST.EXCISE_TYPE = string.Empty;
        oTX_FIBER_IR_MST.SPINNERS = string.Empty;
       

        DataTable dtOP_TRN_SUB = null;
        if (Session["dtOP_TRN_SUB"] != null)
        {
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
        }
        //creteTrnData();
        DataTable dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
        //DataTable mdtDetailTBL = dtDetailTBL;
        string[] columnArr = new String[] { "PRTY_CODE", "FIBER_CODE","PRTY_NAME","BILL_NUMB","BILL_DATE" };
        //mdtDetailTBL.DefaultView.ToTable(true, columnArr);
        DataView mdvDetailTBL = new DataView(dtDetailTBL);
        DataTable mdtDetailTBL = mdvDetailTBL.ToTable(true, columnArr);
       
        
        
        
        bool _result = false;
        for (int i = 0; i < mdtDetailTBL.Rows.Count; i++)
        {
            oTX_FIBER_IR_MST.PRTY_CODE = mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString();
            oTX_FIBER_IR_MST.PRTY_NAME = mdtDetailTBL.Rows[i]["PRTY_NAME"].ToString();
            oTX_FIBER_IR_MST.BILL_NUMB = mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString();
            oTX_FIBER_IR_MST.BILL_DATE =DateTime.Parse(mdtDetailTBL.Rows[i]["BILL_DATE"].ToString());
            oTX_FIBER_IR_MST.BILL_TYPE = "FOP";
            oTX_FIBER_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FIBER_IR_MST.PRTY_CH_NUMB = mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString();
            oTX_FIBER_IR_MST.PRTY_CH_DATE = DateTime.Parse(mdtDetailTBL.Rows[i]["BILL_DATE"].ToString());
            //double totalAmt = 0;
            //double.TryParse(mdtDetailTBL.Rows[i]["AMOUNT"].ToString(), out totalAmt);
            //oTX_FIBER_IR_MST.TOTAL_AMOUNT = totalAmt;

            //double finalAmt = 0;
            //double.TryParse(dtDetailTBL.Rows[i]["AMOUNT"].ToString(), out finalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = getFinalAmount(dtDetailTBL, mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString(), mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString(), mdtDetailTBL.Rows[i]["BILL_DATE"].ToString());
            oTX_FIBER_IR_MST.FINAL_AMOUNT = oTX_FIBER_IR_MST.TOTAL_AMOUNT;

            int TRN =0;
           int.TryParse(SaitexDL.Interface.Method.TX_FIBER_IR_MST.GetMRNNumber(oTX_FIBER_IR_MST, mdtDetailTBL.Rows[i]["FIBER_CODE"].ToString()),out TRN);

                if (TRN > 0)
                {
                    oTX_FIBER_IR_MST.TRN_NUMB = TRN;
                }
                else
                {
                    oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetNewMRNNumber(oTX_FIBER_IR_MST).ToString());
                }

            //if (!string.IsNullOrEmpty(dtDetailTBL.Rows[i]["TRN_NUMB"].ToString()))
            //{
            //    oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(dtDetailTBL.Rows[i]["TRN_NUMB"].ToString());
            //}
            //else
            //{
            //    oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetNewMRNNumber(oTX_FIBER_IR_MST).ToString());
            //}
            //DataView dvDetailTBL = new DataView(dtDetailTBL);
            //dvDetailTBL.RowFilter = " FIBER_CODE='" + dtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'  and LOT_NO='" + dtDetailTBL.Rows[i]["LOT_NO"].ToString().ToUpper() + "' AND  PALLET_CODE='" + dtDetailTBL.Rows[i]["PALLET_CODE"].ToString().ToUpper() + "' AND PRTY_CODE='" + dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'";
            //DataView dvOP_TRN_SUB = new DataView(dtOP_TRN_SUB);
            //dvOP_TRN_SUB.RowFilter = " FIBER_CODE='" + dtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'  and LOT_NO='" + dtDetailTBL.Rows[i]["LOT_NO"].ToString().ToUpper() + "' AND  PALLET_CODE='" + dtDetailTBL.Rows[i]["PALLET_CODE"].ToString().ToUpper() + "' AND PRTY_CODE='" + dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'";
            DataView dvDetailTBL = new DataView(dtDetailTBL);
            dvDetailTBL.RowFilter = " FIBER_CODE='" + mdtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'   AND PRTY_CODE='" + mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'  AND BILL_NUMB='" + mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString() + "' AND BILL_DATE='" + mdtDetailTBL.Rows[i]["BILL_DATE"].ToString() + "'";
            DataView dvOP_TRN_SUB = new DataView(dtOP_TRN_SUB);
            dvOP_TRN_SUB.RowFilter = " FIBER_CODE='" + mdtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'   AND PRTY_CODE='" + mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'  AND BILL_NUMB='" + mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString() + "' AND BILL_DATE='" + mdtDetailTBL.Rows[i]["BILL_DATE"].ToString() + "'";
            
            _result= SaitexBL.Interface.Method.TX_FIBER_IR_MST.Update(oTX_FIBER_IR_MST, dvDetailTBL.ToTable(), htReceive, dvOP_TRN_SUB.ToTable(), new DataTable(), new DataTable());

        
        
        
        }
        return _result;
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("TRNNUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("FIBER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("FIBER_DESC", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("UOM1", typeof(string));
            dtDetailTBL.Columns.Add("UOM_BAIL", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("PALLET_CODE", typeof(string));
            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("TRN_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("TRN_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PRTY_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PRTY_NAME", typeof(string));
            dtDetailTBL.Columns.Add("BILL_NUMB", typeof(string));
            dtDetailTBL.Columns.Add("BILL_DATE", typeof(string)); 
             dtDetailTBL.Columns.Add("PO_YEAR", typeof(int));
           
            return dtDetailTBL;
        }
        catch
        {

            throw;
        }
    }

    public void creteTrnData()
    {
        try
        {
            double RATE=0;
            double QTY=0;
            int bill_no = 0;
            DateTime bill_date = DateTime.MinValue;
            int.TryParse(txtBillNo.Text, out bill_no);
            DateTime.TryParse(txtBillDate.Text, out bill_date);
            DataTable dtDetailTBL=null;
            if(ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable(); 
            }
            if (Session["dtOP_TRN_SUB"] != null)
            {
                dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
                QTY = calculatiallTrnData(dtOP_TRN_SUB, out   RATE);
            }
            
            if (txtFiberCode.Text != "" && txtRate.Text != "" && !string.IsNullOrEmpty(txtPalletCode.SelectedValue))
            {
              DataView dv = new DataView(dtDetailTBL);
              dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim().ToUpper() + "'  and LOT_NO='" + txtLotNo.SelectedValue.Trim().ToUpper() + "' AND  PALLET_CODE='" + txtPalletCode.SelectedValue.ToUpper() + "' AND PRTY_CODE='" + txtPartyCode.SelectedText + "' AND   BILL_NUMB='" + bill_no + "' ";
              
              if (dv.Count > 0)
              {                  
                  dv[0]["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                  dv[0]["PO_NUMB"] = 99999;
                  dv[0]["PO_TYPE"] = "OPB";
                  dv[0]["PO_COMP_CODE"] = "C99999";
                  dv[0]["PO_BRANCH"] = "B99999";
                  dv[0]["FIBER_CODE"] = txtFiberCode.Text.Trim().ToUpper();
                  dv[0]["FIBER_DESC"] = txtdescription.Text.Trim();
                  dv[0]["TRN_QTY"] = 0;//Math.Round(double.Parse(txtOpeningBalanceStock.Text.Trim()), 3);
                  dv[0]["UOM"] = "KG";
                  dv[0]["UOM1"] = "KG";
                  dv[0]["UOM_BAIL"] = "KG";
                  dv[0]["BASIC_RATE"] =Math.Round(RATE, 3);
                  dv[0]["FINAL_RATE"] =  Math.Round(RATE, 3);
                  dv[0]["AMOUNT"] = Math.Round(RATE*QTY, 3);
                  dv[0]["COST_CENTER_CODE"] = "";
                  dv[0]["MAC_CODE"] = string.Empty;
                  dv[0]["REMARKS"] = "";
                  dv[0]["QCFLAG"] = "No";
                  DateTime dd = System.DateTime.Now;
                  DateTime.TryParse(DateTime.MinValue.ToString(), out dd);
                  dv[0]["DATE_OF_MFG"] = dd;
                  dv[0]["NO_OF_UNIT"] = 0;// calculateNoOfUnit();
                  dv[0]["WEIGHT_OF_UNIT"] = 0;// calculateWeightofunit();
                  dv[0]["UOM_OF_UNIT"] = "KG";
                  dv[0]["PI_NO"] = "NA";
                  dv[0]["PALLET_CODE"] = txtPalletCode.SelectedValue;
                  dv[0]["LOT_NO"] = txtLotNo.SelectedValue;
                  dv[0]["GRADE"] = txtGrade.SelectedValue;
                  dv[0]["PRTY_CODE"] = txtPartyCode.SelectedText;
                  dv[0]["PRTY_NAME"] = txtPartyName.Text;
                  dv[0]["BILL_NUMB"] = bill_no.ToString();
                  dv[0]["BILL_DATE"] = bill_date.ToString("dd/MM/yyyy");
                  dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                  dtDetailTBL.AcceptChanges();
              }
              else
              {
                  DataRow dr = dtDetailTBL.NewRow();
                  dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                  dr["PO_NUMB"] = 99999;
                  dr["PO_TYPE"] = "OPB";
                  dr["PO_COMP_CODE"] = "C99999";
                  dr["PO_BRANCH"] = "B99999";
                  dr["FIBER_CODE"] = txtFiberCode.Text.Trim().ToUpper();
                  dr["FIBER_DESC"] = txtdescription.Text.Trim();
                  dr["TRN_QTY"] = 0;
                  dr["UOM"] = "KG";
                  dr["UOM1"] = "KG";
                  dr["UOM_BAIL"] = "KG";
                  dr["BASIC_RATE"] = Math.Round(RATE, 3);
                  dr["FINAL_RATE"] = Math.Round(RATE, 3);
                  dr["AMOUNT"] = Math.Round(RATE * QTY, 3);
                  dr["COST_CENTER_CODE"] = "";
                  dr["MAC_CODE"] = string.Empty;
                  dr["REMARKS"] = "";
                  dr["QCFLAG"] = "No";
                  DateTime dd = System.DateTime.Now;
                  DateTime.TryParse(DateTime.MinValue.ToString(), out dd);
                  dr["DATE_OF_MFG"] = dd;
                  dr["NO_OF_UNIT"] = 0;// calculateNoOfUnit();
                  dr["WEIGHT_OF_UNIT"] = 0;// calculateWeightofunit();
                  dr["UOM_OF_UNIT"] = "KG";
                  dr["PI_NO"] = "NA";
                  dr["PALLET_CODE"] = txtPalletCode.SelectedValue;
                  dr["LOT_NO"] = txtLotNo.SelectedValue;
                  dr["GRADE"] = txtGrade.SelectedValue;
                  dr["PRTY_CODE"] = txtPartyCode.SelectedText;
                  dr["PRTY_NAME"] = txtPartyName.Text;
                  dr["BILL_NUMB"] = bill_no.ToString();
                  dr["BILL_DATE"] = bill_date.ToString("dd/MM/yyyy");
                  dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                  dtDetailTBL.Rows.Add(dr);
              
              }
                       

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
                }

            
            ViewState["dtDetailTBL"] = dtDetailTBL;
            

        }


        catch (Exception)
        {

            throw;
        }
    }



    //**********************OPENING BAL TRN DATA BY NISHANT RAI*************************//

   public void BindIntial()
    {

        try
        {

            txtGrade.SelectedIndex = -1;
            txtLotNo.SelectedIndex = -1;
            txtNoofUnit.Text = "1";
            txtWeightofUnit.Text = "1";
            txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
            bindUOM("UOM");
        }
        catch
        {

        }
    }


   public double  calculatiallTrnData()
   {

       double totalQty = 0;       
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
           totalQty += double.Parse(txtQTY.Text);  

       }
       txtOpeningBalanceStock.Value = totalQty.ToString();
       return totalQty;
   }
   public double calculatiallTrnData(DataTable dtDetailTBL,out double   RATE)
   {     
       RATE = 0;
       DataView dv = new DataView(dtDetailTBL);
       dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim().ToUpper() + "'  and LOT_NO='" + txtLotNo.SelectedValue + "' AND  PALLET_CODE='" + txtPalletCode.SelectedValue + "' AND PRTY_CODE='" + txtPartyCode.SelectedText + "'  AND ROW_STATE <> 'DELETE'";
             
       double totalQty = 0;
       double totalRate = 0;
       for (int i = 0; i < dv.Count; i++)
       {
         
           totalQty += double.Parse(dv[i]["TRN_QTY"].ToString());  
           totalRate += double.Parse(dv[i]["FINAL_RATE"].ToString())/dv.Count; 

       }
       RATE = totalRate;
      
       return totalQty;
   }
            

   public double calculateWeightofunit()
   {

       double totalQty = 0;
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("lblWeightofUnit") as Label;
           totalQty += double.Parse(txtQTY.Text);

       }
       return totalQty / grdsub_trn.Rows.Count;
   }

   public double calculateNoOfUnit()
   {

       double totalQty = 0;
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
           totalQty += double.Parse(txtQTY.Text);

       }
       return totalQty;
   }

   public double calculateTotalPallet()
   {

       double totalQty = 0;
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("lblCPNo") as Label;
           totalQty += 1;

       }
       return totalQty;
   }

    public void bindUOM(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlUOM.Items.Clear();
                ddlUOM.DataSource = dt;
                ddlUOM.DataTextField = "MST_CODE";
                ddlUOM.DataValueField = "MST_CODE";
                ddlUOM.DataBind();
                ddlUOM.SelectedValue = "KG";
                //ddlUOM.Items.Insert(0, new ListItem("------Select------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    private DataTable  CreateSUBTRNTable()
    {
        try
        {
            dtOP_TRN_SUB = new DataTable();
            dtOP_TRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtOP_TRN_SUB.Columns.Add("TRN_NUMB", typeof(int));
            dtOP_TRN_SUB.Columns.Add("TRN_TYPE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("FIBER_CODE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtOP_TRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtOP_TRN_SUB.Columns.Add("GRADE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtOP_TRN_SUB.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtOP_TRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtOP_TRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtOP_TRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtOP_TRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtOP_TRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtOP_TRN_SUB.Columns.Add("PI_NO", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PALLET_CODE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PALLET_NO", typeof(string));
            dtOP_TRN_SUB.Columns.Add("ROW_STATE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("ISS_QTY", typeof(double));
            dtOP_TRN_SUB.Columns.Add("PRTY_CODE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PRTY_NAME", typeof(string));
            dtOP_TRN_SUB.Columns.Add("FINAL_RATE", typeof(double));
            dtOP_TRN_SUB.Columns.Add("BILL_NUMB", typeof(string));
            dtOP_TRN_SUB.Columns.Add("BILL_DATE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PO_YEAR", typeof(int));
            
            return dtOP_TRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void BindBOMGrid()
    {
        try
        {
            
            if (Session["dtOP_TRN_SUB"] == null)
                dtOP_TRN_SUB = CreateSUBTRNTable();
            else
                dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
            

            DataView dv = new DataView(dtOP_TRN_SUB);
            dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim() + "' AND ROW_STATE <> 'DELETE'";
            if (dv.Count > 0)
            {
                //BindIntial();
                txtQty.Text = string.Empty;
                grdsub_trn.DataSource = dv;
                grdsub_trn.DataBind();
            }
            else
            {
                grdsub_trn.DataSource = null;
                grdsub_trn.DataBind();
            
            }
            calculatiallTrnData();
        }
        catch
        {
            throw;
        }
    }

    protected void BtnBOMSave_Click(object sender, EventArgs e)
    
    
    {
        try
        {
            double QTY = 0;
            double ISS_QTY = 0;
            double.TryParse(txtQty.Text.Trim(), out QTY);         
            double.TryParse(lblIssueQty.Text.Trim(), out ISS_QTY);
            double finalRate = 0;
            double.TryParse(txtRate.Text, out finalRate);
            int bill_no = 0;
            DateTime bill_date = DateTime.MinValue;
            int.TryParse(txtBillNo.Text, out bill_no);
            DateTime.TryParse(txtBillDate.Text, out bill_date);
            if (QTY > 0 && QTY > ISS_QTY && !string.IsNullOrEmpty(txtLotNo.SelectedValue) && !string.IsNullOrEmpty(txtPalletCode.SelectedValue) && !string.IsNullOrEmpty(txtGrade.SelectedValue) && !string.IsNullOrEmpty(txtPalletNo.Text) )
            {

                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
                if (Session["dtOP_TRN_SUB"] == null)
                    dtOP_TRN_SUB = CreateSUBTRNTable();
                else
                    dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];

               
                    int UNIQUE_ID = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                    {
                        UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                    }
                    bool bb = SearchInBOMgrid(txtLotNo.SelectedValue,txtPalletNo.Text,txtPartyCode.SelectedText,txtPalletNo.Text , UNIQUE_ID);
                    if (!bb)
                    {
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtOP_TRN_SUB);
                            dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim().ToUpper() + "'  and UNIQUE_ID=" + UNIQUE_ID;
                            if (dv.Count > 0)
                            {

                                dv[0]["TRN_QTY"] = Math.Round(QTY,3);
                                dv[0]["PO_NUMB"] = 99999;
                                dv[0]["PO_TYPE"] = "OPB";
                                dv[0]["PO_COMP_CODE"] = "C99999";
                                dv[0]["PO_BRANCH"] = "B99999";
                                dv[0]["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                                dv[0]["GRADE"] = txtGrade.SelectedValue;
                                dv[0]["LOT_NO"] = txtLotNo.SelectedValue;
                                dv[0]["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                                dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                                dv[0]["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                                dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                                dv[0]["PI_NO"] = "NA";
                                dv[0]["PALLET_CODE"] = txtPalletCode.SelectedValue;
                                dv[0]["PALLET_NO"] = txtPalletNo.Text.ToUpper().Trim();
                                dv[0]["PRTY_CODE"] = txtPartyCode.SelectedText;
                                dv[0]["PRTY_NAME"] = txtPartyName.Text;
                                
                                dv[0]["FINAL_RATE"] = finalRate;
                                dv[0]["BILL_NUMB"] = bill_no.ToString();
                                dv[0]["BILL_DATE"] = bill_date.ToString("dd/MM/yyyy");
                                dv[0]["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dtOP_TRN_SUB.AcceptChanges();
                            }
                        }
                        else
                        {


                            DataRow dr = dtOP_TRN_SUB.NewRow();
                            dr["UNIQUE_ID"] = dtOP_TRN_SUB.Rows.Count + 1;
                            dr["FIBER_CODE"] = txtFiberCode.Text.Trim().ToUpper();    
                            dr["TRN_QTY"] = Math.Round(QTY,3);
                            dr["PO_NUMB"] = 99999;
                            dr["PO_TYPE"] = "OPB";
                            dr["PO_COMP_CODE"] = "C99999";
                            dr["PO_BRANCH"] = "B99999";
                            dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                            dr["GRADE"] = txtGrade.SelectedValue;
                            dr["LOT_NO"] = txtLotNo.SelectedValue;
                            dr["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                            dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                            dr["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                            dr["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                            dr["PI_NO"] = "NA";
                            dr["PALLET_CODE"] = txtPalletCode.SelectedValue;
                            dr["PALLET_NO"] = txtPalletNo.Text.ToUpper().Trim();
                            dr["ROW_STATE"] = "INSERT";
                            dr["ISS_QTY"] = 0;
                            dr["PRTY_CODE"] = txtPartyCode.SelectedText;
                            dr["PRTY_NAME"] = txtPartyName.Text;
                            dr["FINAL_RATE"] = finalRate;
                            dr["BILL_NUMB"] = bill_no.ToString();
                            dr["BILL_DATE"] = bill_date.ToString("dd/MM/yyyy");
                            dr["PO_YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dtOP_TRN_SUB.Rows.Add(dr);
                            Session["dtOP_TRN_SUB"] = dtOP_TRN_SUB;
                            creteTrnData();
                           
                        }
                        RefresBOMRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Enter Another Lot No.This Already Added ');", true);
                    }
                    BindBOMGrid();


               
            }
            else 
            {
                Common.CommonFuction.ShowMessage("Fields are missing to select or enter!");//Lot qty must be greater then 0 OR qty could not be less then already issue qty.
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Sub Transaction Detail Row.\r\nSee error log for detail."));

        }
    }

    private void RefresBOMRow()
    {
        try
        {

            txtQty.Text = string.Empty;            
            ddlMaterialStatus.SelectedIndex = -1;
            txtGrade.SelectedIndex = -1;
            txtLotNo.SelectedIndex = -1;
            txtDofMfd.Text = DateTime.Now.Date.ToString();
            ViewState["UNIQUE_ID"] = null;
            txtPalletCode.SelectedIndex= -1;
            txtPalletNo.Text = string.Empty;

        }
        catch
        {
            throw;
        }
    }

    private void ClearAllBOMRow()
    {
        try
        {

            txtQty.Text = string.Empty;
            ddlMaterialStatus.SelectedIndex = -1;
            txtGrade.SelectedIndex = -1;
            txtLotNo.SelectedIndex = -1;
            txtDofMfd.Text = DateTime.Now.Date.ToString();
            ViewState["UNIQUE_ID"] = null;
            txtPalletCode.SelectedIndex =-1;
            txtPalletNo.Text = string.Empty;

        }
        catch
        {
            throw;
        }
    }

    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        ClearAllBOMRow();
    }

    protected void grdSub_trnArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "BOMEdit")
            {
                FillBOMByGrid(UNIQUE_ID);
            }
            else if (e.CommandName == "BOMDelete")
            {
                DeleteBOMRow(UNIQUE_ID);
                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sub Tran Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }

    private void FillBOMByGrid(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtOP_TRN_SUB"] != null)
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
            DataView dv = new DataView(dtOP_TRN_SUB);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                //ddlW_Side.SelectedValue = dv[0]["W_SIDE"].ToString();
                txtQty.Text = dv[0]["TRN_QTY"].ToString();                
                ddlMaterialStatus.SelectedValue = dv[0]["MATERIAL_STATUS"].ToString();
                //txtGrade.SelectedValue = dv[0]["GRADE"].ToString();
                //txtLotNo.SelectedValue = dv[0]["LOT_NO"].ToString();
                txtDofMfd.Text = dv[0]["DATE_OF_MFG"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtPalletNo.Text=dv[0]["PALLET_NO"].ToString();
                //txtPalletCode.SelectedValue = dv[0]["PALLET_CODE"].ToString();
                lblIssueQty.Text = dv[0]["ISS_QTY"].ToString();
                txtRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtPartyName.Text = dv[0]["PRTY_NAME"].ToString();
                txtBillNo.Text = dv[0]["BILL_NUMB"].ToString();
                txtBillDate.Text = dv[0]["BILL_DATE"].ToString();

                string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('MERGE_NO'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression = " order by MST_CODE asc";            
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", SortExpression, "", "%", "");
                txtLotNo.DataSource = data;
                txtLotNo.DataTextField = "MST_CODE";
                txtLotNo.DataValueField = "MST_CODE";
                txtLotNo.DataBind();
                foreach (ComboBoxItem item in txtLotNo.Items)
                {
                    if (item.Text == dv[0]["LOT_NO"].ToString())
                    {
                        //txtLotNo.SelectedIndex = txtLotNo.Items.IndexOf(item);
                        txtLotNo.SelectedText = item.Text;
                        txtLotNo.SelectedValue = item.Value;
                        break;
                    }
                }


                string CommandText1 = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('PALLET_MASTER'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression1 = " order by MST_CODE asc";
                DataTable data1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, "", SortExpression1, "", "%", "");
                txtPalletCode.DataSource = data1;
                txtPalletCode.DataTextField = "MST_CODE";
                txtPalletCode.DataValueField = "MST_CODE";
                txtPalletCode.DataBind();
                foreach (ComboBoxItem item in txtPalletCode.Items)
                {
                    if (item.Text == dv[0]["PALLET_CODE"].ToString())
                    {
                        //txtPalletCode.SelectedIndex = txtPalletCode.Items.IndexOf(item);
                        txtPalletCode.SelectedText = item.Text;
                        txtPalletCode.SelectedValue = item.Value;
                        break;
                    }
                }



                string CommandText2 = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GRADE'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression2 = " order by MST_CODE asc";
                DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, "", SortExpression2, "", "%", "");
                txtGrade.DataSource = data2;
                txtGrade.DataTextField = "MST_CODE";
                txtGrade.DataValueField = "MST_CODE";
                txtGrade.DataBind();
                foreach (ComboBoxItem item in txtGrade.Items)
                {
                    if (item.Text == dv[0]["GRADE"].ToString())
                    {
                        //txtGrade.SelectedIndex = txtGrade.Items.IndexOf(item);
                        txtGrade.SelectedText = item.Text;
                        txtGrade.SelectedValue = item.Value;
                        break;
                    }
                }





                string CommandText3 = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker')) ";
                string WhereClause3 = "  and  (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                string SortExpression3 = " order by PRTY_CODE asc";
                string SearchQuery3 = "%";
                DataTable data3 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText3, WhereClause3, SortExpression3, "", SearchQuery3, "");
                txtPartyCode.DataSource = data3;
                txtPartyCode.DataTextField = "PRTY_CODE";
                txtPartyCode.DataValueField = "Address";
                txtPartyCode.DataBind();
                foreach (ComboBoxItem item in txtPartyCode.Items)
                {
                    if (item.Text ==dv[0]["PRTY_CODE"].ToString())
                    {
                        //txtPartyCode.SelectedIndex = txtPartyCode.Items.IndexOf(item);
                        txtPartyCode.SelectedText = item.Text;
                        txtPartyCode.SelectedValue = item.Value;
                        break;
                    }
                }


                
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteBOMRow(int UNIQUE_ID)
    {
        try
        {
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
            if (grdsub_trn.Rows.Count == 1)
            {
                //dtOP_TRN_SUB.Rows.Clear();
                dtOP_TRN_SUB.Rows[0].SetField("ROW_STATE","DELETE");
            }
            else
            {
                foreach (DataRow dr in dtOP_TRN_SUB.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        //dtOP_TRN_SUB.Rows.Remove(dr);  
                        dr.SetField("ROW_STATE", "DELETE");
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtOP_TRN_SUB.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            dtOP_TRN_SUB.AcceptChanges();
            Session["dtOP_TRN_SUB"] = dtOP_TRN_SUB;
            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    private bool SearchInBOMgrid(string LotNo, string palletno, string PRTY, string PALLET_NO, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdsub_trn.Rows)
            {
                Label txtLotNo = (Label)grdRow.FindControl("lbtlotno");
                Label txtPalletNo = (Label)grdRow.FindControl("lblCPNo");
                Label txtParty = (Label)grdRow.FindControl("lblParty");
                Label Pallet = (Label)grdRow.FindControl("lblCPNo");
                Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtLotNo.Text.ToUpper() == LotNo.ToUpper() && txtPalletNo.Text.ToUpper() == palletno.ToUpper() && txtParty.ToolTip.ToUpper() == PRTY.ToUpper() && Pallet.Text.ToUpper() == PALLET_NO.ToUpper() && UNIQUE_ID != iUNIQUE_ID)
                {
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
   
    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        txtWeightofUnit.Text = Math.Round((double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)),3).ToString();
        BtnBOMSave.Focus();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        if (txtNoofUnit.Text != "")
        {
            txtWeightofUnit.Text = Math.Round((double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)),3).ToString();
            
        }
        txtNoofUnit.Focus();

    }
  

 


   // **********************************************************//


    protected void grdsub_trn_RowDataBound(object sender, GridViewRowEventArgs e)
    {  
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label issQty = (Label)e.Row.FindControl("txtIssueQty");
            Button btnDelete = (Button)e.Row.FindControl("lnkBOMDelete");
            double issueQty = 0;
            double.TryParse(issQty.Text, out issueQty);
            if (issueQty > 0)
            {
                btnDelete.Enabled = false;
            }
            else 
            {
                btnDelete.Enabled = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Literal totalQty = (Literal)e.Row.FindControl("totalQty");
            Literal totalPalletNo = (Literal)e.Row.FindControl("totalPalletNo");
            Literal totalNoOfUnit = (Literal)e.Row.FindControl("totalNoOfUnit");
            totalQty.Text = calculatiallTrnData().ToString();
            totalNoOfUnit.Text = calculateNoOfUnit().ToString();
            totalPalletNo.Text = calculateTotalPallet().ToString();
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Fiber/Queries/FiberStockLotWise.aspx", false);
    }

    protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "MERGE_NO");
            txtLotNo.Items.Clear();
            txtLotNo.DataSource = data;
            txtLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetMOMCount(e.Text, "MERGE_NO");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Merge No in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtGrade_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
        DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "GRADE");
        txtGrade.Items.Clear();
        txtGrade.DataSource = data;
        txtGrade.DataBind();
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        e.ItemsCount = GetMOMCount(e.Text, "GRADE");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grade selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtPalletCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {      
        try
        {
            DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "PALLET_MASTER");
            txtPalletCode.Items.Clear();
            txtPalletCode.DataSource = data;
            txtPalletCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetMOMCount(e.Text, "PALLET_MASTER");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Pallet Master selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetMOMData(string Text, int startOffset,string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND MST_CODE NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";
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
    protected int GetMOMCount(string text,string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }


    protected double getFinalAmount(DataTable dt,string PRTY_CODE,string BILL_NUMB,string BILL_DATE)
    {
        double _finalamt = 0;
        DataView dv = new DataView(dt);
        dv.RowFilter = "PRTY_CODE='" + PRTY_CODE + "' AND BILL_NUMB='" + BILL_NUMB + "' AND BILL_DATE='" + BILL_DATE + "' ";

        for (int i = 0; i < dv.Count; i++)
        {
            double amt = 0;
            double.TryParse(dv[i]["AMOUNT"].ToString(), out amt);
           _finalamt = _finalamt + amt;
        
        }
        return _finalamt;
    }

}
