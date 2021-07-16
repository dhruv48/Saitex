using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using System.Text;
using MKB.TimePicker;

public partial class Module_PlanningAndScheduling_Controls_OrderMachinePlanning : System.Web.UI.UserControl
{
    /************************************************************************************************************************************************/
    /************************************************ Added By Nishant Rai started from  27-08-2013*************************************************************/
    /************************************************************************************************************************************************/

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string ORDER_NO{ get; set; }    
    public string PI_NO { get; set; }
    public string ARTICAL_CODE{ get; set; }
    public string SHADE_CODE { get; set; }
    public string PROS_ROUTE_CODE { get; set; }
    public string YEAR{ get; set; } 
    public string ORDER_TYPE { get; set; }    
    public string PRODUCT_TYPE { get; set; }
    public string Header_Name { get; set; }
    public double  PLANNED_QTY { get; set; }
    public string BRANCH_CODE { get; set; }








    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            ResetControls();        
            lblorderplanning.Text = Header_Name;
            hideshowRow(false);
            GET_Order_Data(ORDER_NO, PI_NO, PRODUCT_TYPE, ORDER_TYPE, oUserLoginDetail.COMP_CODE , BRANCH_CODE, YEAR);
        }
    }
   
    protected void cmbOrderNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        var mchOrderDetails = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.CheckOrderNoInMac_Scheduling_Mst(cmbOrderNo.SelectedValue,"", oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PRODUCT_TYPE, ORDER_TYPE,YEAR);
        var dtOrderDetails = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetDataForApprovedOrderDetails(cmbOrderNo.SelectedValue,"", PRODUCT_TYPE, ORDER_TYPE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.DT_STARTDATE.Year.ToString());
         FillTextDetails(dtOrderDetails, mchOrderDetails);
         hideshowRow(true); 
        var dtProcessRootDetails = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetProcessingStandarProcessRouteCode(lblprocessroot.Text, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
        BindGridDetails(dtProcessRootDetails,gridProcessRoot);
        lblorderno.Text = cmbOrderNo.SelectedValue;
       
       

    }

    protected void GET_Order_Data(string ORDER_NO,string PI_NO,string PRODUCT_TYPE,string ORDER_TYPE,string COMP_CODE,string BRANCH_CODE,string YEAR)
    {
        var mchOrderDetails = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.CheckOrderNoInMac_Scheduling_Mst(ORDER_NO, PI_NO, PRODUCT_TYPE, ORDER_TYPE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, YEAR);
        var dtOrderDetails = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetDataForApprovedOrderDetails(ORDER_NO, PI_NO, PRODUCT_TYPE, ORDER_TYPE, COMP_CODE, BRANCH_CODE, YEAR);
        FillTextDetails(dtOrderDetails, mchOrderDetails);
        hideshowRow(true);
        var dtProcessRootDetails = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetProcessingStandarProcessRouteCode(lblprocessroot.Text, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
        BindGridDetails(dtProcessRootDetails, gridProcessRoot);
        lblorderno.Text = lblorderno1.Text = ORDER_NO;
    }

    protected void cmbOrderNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var  data = GetApprovedOrderNO(e.Text.ToUpper(), e.ItemsOffset);
            cmbOrderNo.Items.Clear();
            cmbOrderNo.DataSource = data;
            cmbOrderNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetApprovedOrderNOCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order No. selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetApprovedOrderNO(string Text, int startOffset)
    {
     
        string whereClause = string.Empty;
        string SearchQuery = string.Empty;
        DataTable data = null;
        try
        {

           //var CommandText = "  SELECT   ORDER_NO, PRTY_NAME,ORDER_DATE,DEL_DATE from ( select Distinct  A.ORDER_NO,C.PRTY_NAME,A.ORDER_DATE, B.DEL_DATE    FROM OD_CAPT_MST A, OD_CAPT_TRN_MAIN B, TX_VENDOR_MST C where  B.FINAL_ORDER_CONF_CLAG=1 AND A.ORDER_NO = B.ORDER_NO  AND A.PRTY_CODE = C.PRTY_CODE  AND A.ORDER_TYPE='" + ORDER_TYPE + "'  AND A.PRODUCT_TYPE='" + PRODUCT_TYPE + "'  AND A.COMP_CODE LIKE :COMP_CODE  AND A.BRANCH_CODE LIKE :BRANCH_CODE AND A.ORDER_NO NOT IN (  SELECT ORDER_NO FROM V_MAC_SCHEDULE_TRN WHERE  COMP_CODE = :COMP_CODE  AND BRANCH_CODE = :BRANCH_CODE  AND PRODUCT_TYPE='" + PRODUCT_TYPE + "'    AND ORDER_TYPE='" + ORDER_TYPE + "' AND REMQTY=0) )  where   ORDER_NO LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery  AND  1=1 AND ROWNUM <= 15 ";
            var CommandText = "  SELECT   ORDER_NO,PRTY_NAME, ORDER_DATE,DEL_DATE,ARTICAL_DESC  FROM (SELECT   DISTINCT  A.ORDER_NO, a.PRTY_NAME,A.ORDER_DATE,a.DEL_DATE,A.ARTICAL_DESC      FROM  V_OD_CAPT_TRN_BOM A      WHERE       A.FINAL_ORDER_CONF_CLAG = 1   AND  NVL(A.ISS_QTY,0)>0   AND A.MASTER_DEPT = '" + PRODUCT_TYPE + "'  AND A.PRODUCTION_TYPE='" + ORDER_TYPE + "'   AND A.COMP_CODE LIKE :COMP_CODE           AND A.MASTER_BRANCH  LIKE  '" + oUserLoginDetail.VC_BRANCHNAME + "'     AND (A.ORDER_NO,A.COMP_CODE,A.BRANCH_CODE,A.YEAR,A.MASTER_DEPT,A.PRODUCTION_TYPE) NOT IN        (SELECT   ORDER_NO,COMP_CODE,BRANCH_CODE,YEAR,PRODUCT_TYPE AS MASTER_DEPT,  ORDER_TYPE AS PRODUCTION_TYPE    FROM   V_MAC_SCHEDULE_TRN      WHERE   COMP_CODE = :COMP_CODE     AND BRANCH_CODE = :BRANCH_CODE          AND PRODUCT_TYPE ='" + PRODUCT_TYPE + "'          AND ORDER_TYPE = '" + ORDER_TYPE + "'         AND REMQTY = 0)) WHERE   ORDER_NO LIKE :SearchQuery    OR PRTY_NAME LIKE :SearchQuery AND 1 = 1 AND ROWNUM <= 15";
              
                if (startOffset != 0)
                {
                    //whereClause += " AND ORDER_NO NOT IN ( SELECT ORDER_NO FROM  ( select  Distinct  A.ORDER_NO,C.PRTY_NAME,A.ORDER_DATE   FROM OD_CAPT_MST A, OD_CAPT_TRN_MAIN B, TX_VENDOR_MST C where  B.FINAL_ORDER_CONF_CLAG=1 AND A.ORDER_NO = B.ORDER_NO  AND A.PRTY_CODE = C.PRTY_CODE  AND A.ORDER_TYPE='" + ORDER_TYPE + "' AND A.PRODUCT_TYPE='" + PRODUCT_TYPE + "'  AND A.COMP_CODE LIKE :COMP_CODE  AND A.BRANCH_CODE LIKE :BRANCH_CODE  AND A.ORDER_NO NOT IN (  SELECT ORDER_NO FROM V_MAC_SCHEDULE_TRN WHERE  COMP_CODE = :COMP_CODE  AND BRANCH_CODE = :BRANCH_CODE  AND PRODUCT_TYPE='" + PRODUCT_TYPE + "'    AND ORDER_TYPE='" + ORDER_TYPE + "' AND REMQTY=0) ORDER BY    A.ORDER_NO  ASC; )  asd WHERE ORDER_NO LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery  AND  1 = 1  AND ROWNUM <= " + startOffset + ")";
                    whereClause += "AND ORDER_NO NOT IN ( SELECT   ORDER_NO,PRTY_NAME, ORDER_DATE,DEL_DATE,ARTICAL_DESC  FROM (SELECT   DISTINCT  A.ORDER_NO, a.PRTY_NAME,A.ORDER_DATE,a.DEL_DATE,A.ARTICAL_DESC      FROM  V_OD_CAPT_TRN_BOM A      WHERE       A.FINAL_ORDER_CONF_CLAG = 1   AND  NVL(A.ISS_QTY,0)>0   AND A.MASTER_DEPT = '" + PRODUCT_TYPE + "'  AND A.PRODUCTION_TYPE='" + ORDER_TYPE + "'   AND A.COMP_CODE LIKE :COMP_CODE           AND A.MASTER_BRANCH  LIKE  '" + oUserLoginDetail.VC_BRANCHNAME + "'     AND (A.ORDER_NO,A.COMP_CODE,A.BRANCH_CODE,A.YEAR,A.MASTER_DEPT,A.PRODUCTION_TYPE) NOT IN        (SELECT   ORDER_NO,COMP_CODE,BRANCH_CODE,YEAR,PRODUCT_TYPE AS MASTER_DEPT,  ORDER_TYPE AS PRODUCTION_TYPE    FROM   V_MAC_SCHEDULE_TRN      WHERE   COMP_CODE = :COMP_CODE     AND BRANCH_CODE = :BRANCH_CODE          AND PRODUCT_TYPE ='" + PRODUCT_TYPE + "'          AND ORDER_TYPE = '" + ORDER_TYPE + "'         AND REMQTY = 0)) WHERE   ORDER_NO LIKE :SearchQuery    OR PRTY_NAME LIKE :SearchQuery  AND  1 = 1  AND ROWNUM <= " + startOffset + ")";
                }
                SearchQuery = Text + "%";
                string SortExpression = " order by ORDER_NO";

                data = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetDataForOrderDevelopement(CommandText, whereClause, SortExpression, "", SearchQuery, "", oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PRODUCT_TYPE, ORDER_TYPE);           
        }
        catch(Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
        return data;
    }

    protected int GetApprovedOrderNOCount(string text)
    {
        DataTable data = null;    
        string WhereClause = string.Empty;
        string SortExpression = string.Empty;
        string SearchQuery = string.Empty;
        int count = 0;
        try
        {
            //var CommandText = "select  Distinct  A.ORDER_NO,C.PRTY_NAME,A.ORDER_DATE    FROM OD_CAPT_MST A, OD_CAPT_TRN_MAIN B, TX_VENDOR_MST C where  B.FINAL_ORDER_CONF_CLAG=1 AND A.ORDER_NO = B.ORDER_NO  AND A.PRTY_CODE = C.PRTY_CODE AND A.ORDER_TYPE='" + ORDER_TYPE + "' AND A.PRODUCT_TYPE='" + PRODUCT_TYPE + "'  AND A.BRANCH_CODE=:BRANCH_CODE AND A.COMP_CODE=:COMP_CODE ";
            var CommandText = "SELECT   ORDER_NO,PRTY_NAME, ORDER_DATE,DEL_DATE,ARTICAL_DESC  FROM (SELECT   DISTINCT  A.ORDER_NO, a.PRTY_NAME,A.ORDER_DATE,a.DEL_DATE,A.ARTICAL_DESC      FROM  V_OD_CAPT_TRN_BOM A      WHERE       A.FINAL_ORDER_CONF_CLAG = 1   AND  NVL(A.ISS_QTY,0)>0   AND A.MASTER_DEPT = '" + PRODUCT_TYPE + "'  AND A.PRODUCTION_TYPE='" + ORDER_TYPE + "'   AND A.COMP_CODE LIKE :COMP_CODE           AND A.MASTER_BRANCH  LIKE  '" + oUserLoginDetail.VC_BRANCHNAME + "'     AND (A.ORDER_NO,A.COMP_CODE,A.BRANCH_CODE,A.YEAR,A.MASTER_DEPT,A.PRODUCTION_TYPE) NOT IN        (SELECT   ORDER_NO,COMP_CODE,BRANCH_CODE,YEAR,PRODUCT_TYPE AS MASTER_DEPT,  ORDER_TYPE AS PRODUCTION_TYPE    FROM   V_MAC_SCHEDULE_TRN      WHERE   COMP_CODE = :COMP_CODE     AND BRANCH_CODE = :BRANCH_CODE          AND PRODUCT_TYPE ='" + PRODUCT_TYPE + "'          AND ORDER_TYPE = '" + ORDER_TYPE + "'         AND REMQTY = 0)) WHERE   ORDER_NO LIKE :SearchQuery    OR PRTY_NAME LIKE :SearchQuery ";
            data = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetDataForOrderDevelopement(CommandText, WhereClause, SortExpression, "", SearchQuery, "", oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PRODUCT_TYPE, ORDER_TYPE);
            count = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in order count.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
        
        return count;
    }

    protected void BindGridDetails(DataTable data,GridView dataGrid)
    {
        try
        {        
            if (data.Rows.Count > 0 && data != null) 
            {

                dataGrid.DataSource = data;
                dataGrid.DataBind();
            }
            else
            {
                dataGrid.DataSource = null;
                dataGrid.DataBind();
                dataGrid.EmptyDataText = "Data is Not Available for selected Order No.";
            }
        }
        
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting details.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    
    }    

    protected void hideshowRow(bool flag)
    {
        trOrderDetails.Visible = flag;
        trProcessRootDetails.Visible = flag;
    }

    private DataTable CreateDataTableForOrderDetails()
    {
        var dtPADetail = new DataTable();
        dtPADetail.Columns.Add("ORDER_NO", typeof(string));       
        dtPADetail.Columns.Add("PI_NO", typeof(string));
        dtPADetail.Columns.Add("PARTY_CODE", typeof(string));
        dtPADetail.Columns.Add("ARTICLE_CODE", typeof(string));       
        dtPADetail.Columns.Add("SHADE_CODE", typeof(string)); 
        dtPADetail.Columns.Add("ORDER_QTY", typeof(double ));
        dtPADetail.Columns.Add("PLANNED_QTY", typeof(double ));
        dtPADetail.Columns.Add("REMAINING_QTY", typeof(double)); 
        dtPADetail.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtPADetail.Columns.Add("PROS_ROUTE_CODE", typeof(string));   
        dtPADetail.Columns.Add("ORDER_TYPE", typeof(string));        
        dtPADetail.Columns.Add("COMP_CODE", typeof(string));
        dtPADetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtPADetail.Columns.Add("FLAG", typeof(string));
        dtPADetail.Columns.Add("TDATE", typeof(string));
        dtPADetail.Columns.Add("TUSER", typeof(string));
        dtPADetail.Columns.Add("YEAR", typeof(int));  
        return dtPADetail;
    }

    private DataTable CreateDataTableForMachineDetails()
    {
        var dtMachineDetail = new DataTable();
        dtMachineDetail.Columns.Add("PROCESS_ROOT_CODE", typeof(string));
        dtMachineDetail.Columns.Add("MACHINE_CODE", typeof(string));
        dtMachineDetail.Columns.Add("SERIAL_NO", typeof(string));
        dtMachineDetail.Columns.Add("PROCESS_CODE", typeof(string));
        dtMachineDetail.Columns.Add("YEAR", typeof(int));  
        return dtMachineDetail;
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
       // SetOrderDataToDataTable(grdOrderDetails);
    }

    protected void FillTextDetails(DataTable dt,DataTable dt2)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
        txtPartyName.Text = dt.Rows[0]["PRTY_NAME"].ToString();
        txtPartyName.ToolTip = dt.Rows[0]["PRTY_CODE"].ToString();// +" || " + dt.Rows[0]["PRTY_ADD1"].ToString();
        txtOrderDate.Text = dt.Rows[0]["ORDER_DATE"].ToString().Substring(0, 10);
        txtDeleveryDate.Text = dt.Rows[0]["DEL_DATE"].ToString().Substring(0, 10);
        lblprocessroot.Text = dt.Rows[0]["PROS_ROUTE_CODE"].ToString();
        string _plannedQty = string.Empty;
        string _remaningQty = string.Empty;
        double  ordQty = 0;
        double  plqty = 0;
        double  remQty = 0;

        if (!dt.Columns.Contains("PLANNED_QTY"))
        {
            dt.Columns.Add("PLANNED_QTY", typeof(double));

        }
        if (!dt.Columns.Contains("REMAINING_QTY"))
        {
            dt.Columns.Add("REMAINING_QTY", typeof(double));
        }

        if (dt2 != null && dt2.Rows.Count > 0)
        {
           
            foreach (DataRow dr2 in dt2.Rows)
            {
                       
                double.TryParse(dr2["ORDER_QTY"].ToString(), out ordQty);
                double.TryParse(dr2["TOTAL_PLANNED_QTY"].ToString(), out plqty);
                remQty = ordQty - plqty;
                dt.Rows[0]["PLANNED_QTY"] = remQty;
                dt.Rows[0]["REMAINING_QTY"] = remQty;
                

            }

        }
        else
        {
           
            foreach (DataRow dr in dt.Rows)
            {
                string PLANNED_QTY = dr["ORD_QTY"].ToString();
                string REMAINING_QTY = dr["ORD_QTY"].ToString();
                if (string.IsNullOrEmpty(PLANNED_QTY))
                    dr["PLANNED_QTY"] = plqty;
                dr["PLANNED_QTY"] = PLANNED_QTY;
                if (string.IsNullOrEmpty(REMAINING_QTY))
                    dr["REMAINING_QTY"] = remQty ;
                dr["REMAINING_QTY"] = REMAINING_QTY;
               

            }
        }
    }
        BindGridDetails(dt, grdOrderDetails);

    }

    protected void ResetControls()
    {
        cmbOrderNo.SelectedIndex = -1;
        txtPartyName.Text = string.Empty;
        txtOrderDate.Text = string.Empty;
        txtDeleveryDate.Text = string.Empty;
        lblorderno.Text = string.Empty;
        lblorderno1.Text = string.Empty;
        lblorderplanning.Text = string.Empty;
        lblprocessroot.Text = string.Empty;        
        LblError.Text = string.Empty;
        LblMessage.Text = string.Empty;
        grdOrderDetails.DataSource = null;
        grdOrderDetails.DataBind();
        gridProcessRoot.DataSource = null;
        gridProcessRoot.DataBind();
        hideshowRow(false);
        
    }

    protected void SetOrderDataToDataTable(GridView gridView)
    {  
        try
        {
            string msg = string.Empty;
            bool machinResult = true;
            int machinCount = 0;
            var orderDetailsDataTable = getDataInDataTableFromGridForOrderDetails(gridView);
            var childMachinData = getChildGridViewValue();
            if (msg != string.Empty)
            {
                CommonFuction.ShowMessage(msg);
            }
            if (orderDetailsDataTable.Rows.Count > 0 && orderDetailsDataTable != null)
            {
                if (childMachinData.Rows.Count > 0 && childMachinData != null)
                {
                    for (int i = 0; i < childMachinData.Rows.Count; i++)
                    {
                        var SDATE = DateTime.Now;
                        var EDATE = DateTime.Now;                     
                        DateTime.TryParse(childMachinData.Rows[i]["SCHEDULE_DATE_FROM"].ToString(), out SDATE);
                        DateTime.TryParse(childMachinData.Rows[i]["SCHEDULE_DATE_TO"].ToString(), out EDATE);
                        

                        var availabeDatesData = GetMachineScheduleDetailsForSelectDate(childMachinData.Rows[i]["PROS_ROUTE_CODE"].ToString(), childMachinData.Rows[i]["MACHINE_CODE"].ToString(), SDATE, EDATE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.DT_STARTDATE.Year.ToString() );
                        if (availabeDatesData.Rows.Count > 0)
                        {
                            machinResult = false;
                            machinCount = machinCount + 1;
                            
                        }
                    }

                    if (machinResult == true && machinCount <= 0)
                    {
                        int iResult = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.SaveOrderDevelopment(orderDetailsDataTable, childMachinData);
                        if (iResult > 0)
                        {
                            ResetControls();
                            CommonFuction.ShowMessage("Order Development Saved Successfully.");
                            Response.Redirect("~/Module/Production/Pages/PRODUCTION_PLANNING_CONFIRMATION.aspx?PRODUCT_TYPE=" + PRODUCT_TYPE);
                        }
                    }
                    else 
                    {
                        CommonFuction.ShowMessage("please check date time availablity for the given machin codes ,data is not saved. !.");
                        return;
                    }
                }
            }          
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Planning Saving .\r\nSee error log for detail."));
        }    
    }

    public DataTable getDataInDataTableFromGridForOrderDetails(GridView gridView)
    {
        var orderDetailsDataTable = CreateDataTableForOrderDetails();
        try
        {
            string msg = string.Empty;
            
            var totalRows = gridView.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                var thisGridViewRow = gridView.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {

                    var lblORDER_NO = (Label)thisGridViewRow.FindControl("lblorderno");
                    var lblPINO = (Label)thisGridViewRow.FindControl("lblpino");
                    var lblARTICLE_CODE = (Label)thisGridViewRow.FindControl("lblarticlecode");
                    var lblSHADE_CODE = (Label)thisGridViewRow.FindControl("lblSHADE_CODE");
                    var lblPROCESS_ROOT = (Label)thisGridViewRow.FindControl("lblprderpro");
                    var lblPartyName = (Label)thisGridViewRow.FindControl("lblprtyname");
                    var txtORDERQTY = (TextBox)thisGridViewRow.FindControl("lblordqty");
                    var txtPlannedQty = (TextBox)thisGridViewRow.FindControl("txtPlannedQty");
                    var txtBalQty = (TextBox)thisGridViewRow.FindControl("txtBalQty");
                    var lblCompCode = (Label)thisGridViewRow.FindControl("lblCompCode");
                    var lblBRANCH_CODE = (Label)thisGridViewRow.FindControl("lblBRANCH_CODE");
                    var lblORDER_TYPE = (Label)thisGridViewRow.FindControl("lblORDER_TYPE");
                    var lblPRODUCT_TYPE = (Label)thisGridViewRow.FindControl("lblPRODUCT_TYPE");
                    var lblYear = (Label)thisGridViewRow.FindControl("lblYear");
                    double  ordqty = 0;
                    double  plqty = 0;
                    double  remqty = 0;
                    int year = 0;

                    double.TryParse(txtORDERQTY.Text, out ordqty);
                    double.TryParse(txtPlannedQty.Text, out plqty);
                    double.TryParse(txtBalQty.Text, out remqty);
                    int.TryParse(lblYear.Text, out year);
                    if (!CheckPlannedQty(remqty, plqty))
                    {
                        CommonFuction.ShowMessage("Planned Qty is greater then Balance Qty. Enter relavent Planned Qty");                       
                    }

                    var dr = orderDetailsDataTable.NewRow();
                    dr["ORDER_NO"] = lblORDER_NO.Text;
                    dr["PI_NO"] = lblPINO.Text;
                    dr["PARTY_CODE"] = lblPartyName.Text;
                    dr["ARTICLE_CODE"] = lblARTICLE_CODE.Text;
                    dr["SHADE_CODE"] = lblSHADE_CODE.Text;
                    dr["ORDER_QTY"] = ordqty;
                    dr["PLANNED_QTY"] = plqty;
                    dr["REMAINING_QTY"] = (remqty - plqty);
                    dr["PROS_ROUTE_CODE"] = lblPROCESS_ROOT.Text;
                    dr["PRODUCT_TYPE"] = PRODUCT_TYPE;
                    dr["ORDER_TYPE"] = ORDER_TYPE;
                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["TDATE"] = DateTime.Now.Date.ToString();
                    dr["TUSER"] = oUserLoginDetail.UserCode;
                    dr["YEAR"] = year;
                    orderDetailsDataTable.Rows.Add(dr);

                }
            }
            orderDetailsDataTable.AcceptChanges();            
            return orderDetailsDataTable;
        }
        catch (Exception ex)
        {
        
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Planning Saving .\r\nSee error log for detail."));
            return orderDetailsDataTable = null; 
        }

       
    }

    public DataTable getDataInDataTableFromGridForMachineDetails(GridView gridView)
    {
        var machineDetailsDataTable = CreateDataTableForMachineDetails();
        try
        {
            string msg = string.Empty;

            var totalRows = gridView.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                var thisGridViewRow = gridView.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {

                    
                    var lblProcessCode = (Label)thisGridViewRow.FindControl("txcProcessCode");
                    var lblMachineCode = (Label)thisGridViewRow.FindControl("txtMachineCode");
                    var lblSerialNo = (Label)thisGridViewRow.FindControl("txtSNO");
                    var dr = machineDetailsDataTable.NewRow();
                    dr["PROCESS_ROOT_CODE"] = lblprocessroot.Text;
                    dr["MACHINE_CODE"] = lblMachineCode.Text;
                    dr["PROCESS_CODE"] = lblProcessCode.Text;
                    dr["SERIAL_NO"] = lblSerialNo.Text;                              
                    machineDetailsDataTable.Rows.Add(dr);
                }
            }
            machineDetailsDataTable.AcceptChanges();
            return machineDetailsDataTable;
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Planning Saving .\r\nSee error log for detail."));
            return machineDetailsDataTable = null;
        }


    }

    protected bool CheckPlannedQty(double  ordqty, double  plqty)
    {
        bool result = true;
        if (ordqty < plqty || plqty <= 0)
        {
            result = false;
        }
        return result;    
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {       
        SetOrderDataToDataTable(grdOrderDetails);
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ResetControls();
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

    protected void txtPlannedQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var txtPlanned = ((TextBox)(sender));
            var thisGridViewRow = ((GridViewRow)(txtPlanned.NamingContainer));
            var txtORDERQTY = (TextBox)thisGridViewRow.FindControl("lblordqty");
            var txtPlannedQty = (TextBox)thisGridViewRow.FindControl("txtPlannedQty");
            var txtBalQty = (TextBox)thisGridViewRow.FindControl("txtBalQty");
           
            double  ordQty = 0;
            double  plQty = 0;
            double  balQty = 0;
            double.TryParse(txtORDERQTY.Text,out ordQty );
            double.TryParse(txtPlanned.Text,out plQty);
            double.TryParse(txtBalQty.Text,out balQty);

            if (balQty == 0)    
            {
                
                txtPlannedQty.ReadOnly = true;
                return;
            }

            if (plQty <= 0)
            {
                CommonFuction.ShowMessage(@"Planned Qantity cannot be zero.");
                txtPlanned.Text = balQty.ToString();
                return;
            }
            else if (!CheckPlannedQty(balQty, plQty))
            {
                CommonFuction.ShowMessage("Planned Qty cannot be greater then Balance Qty. Enter relavent Planned Qty");
                txtPlanned.Text = balQty.ToString();                
                return;
            }          
            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order And Planned Qty .\r\nSee error log for detail."));
        }
    }

    protected void gridProcessRoot_RowCommand(object sender, GridViewCommandEventArgs e)
    {     
        try
        {
            var orderDetailsDataTable = getDataInDataTableFromGridForOrderDetails(grdOrderDetails);
          
            var  machine_ID = e.CommandArgument.ToString(); 
            if (e.CommandName == "MachinSchedule")
            {
                var url = MachineQueryQtringForOrderDetails("MachineScheduling.aspx", orderDetailsDataTable,machine_ID);
                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=700');", true);
                           
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            }            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Machinge Scheduling.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected string MachineQueryQtringForOrderDetails(string pageName, DataTable orderDetailsDataTable,string machineCode)
    {
        string querystring = pageName; 
        if (orderDetailsDataTable.Rows.Count > 0 && orderDetailsDataTable != null)
        {
        double  ordqty = 0;
        double plqty = 0;
        double remqty = 0;
        double.TryParse(orderDetailsDataTable.Rows[0]["ORDER_QTY"].ToString(), out ordqty);
        double.TryParse(orderDetailsDataTable.Rows[0]["PLANNED_QTY"].ToString(), out plqty);
        double.TryParse(orderDetailsDataTable.Rows[0]["REMAINING_QTY"].ToString(), out remqty);
        querystring = querystring + "?ORDER_NO=" + orderDetailsDataTable.Rows[0]["ORDER_NO"].ToString() + "&";
        querystring = querystring + "PI_NO=" + orderDetailsDataTable.Rows[0]["PI_NO"].ToString() + "&";
        querystring = querystring + "PARTY_CODE=" + orderDetailsDataTable.Rows[0]["PARTY_CODE"].ToString() + "&";
        querystring = querystring + "ARTICLE_CODE=" + orderDetailsDataTable.Rows[0]["ARTICLE_CODE"].ToString() + "&";
        querystring = querystring + "SHADE_CODE=" + orderDetailsDataTable.Rows[0]["SHADE_CODE"].ToString() + "&";
        querystring = querystring + "PROCESS_ROOT_CODE=" + orderDetailsDataTable.Rows[0]["PROS_ROUTE_CODE"].ToString() + "&";        
        querystring = querystring + "ORDER_QTY=" + ordqty + "&";
        querystring = querystring + "PLANNED_QTY=" + plqty + "&";
        querystring = querystring + "REMAINING_QTY=" + remqty + "&";
        querystring = querystring + "PRODUCT_TYPE=" + orderDetailsDataTable.Rows[0]["PRODUCT_TYPE"].ToString() + "&";
        querystring = querystring + "ORDER_TYPE=" + orderDetailsDataTable.Rows[0]["ORDER_TYPE"].ToString() + "&";
        querystring = querystring + "BRANCH_CODE=" + orderDetailsDataTable.Rows[0]["BRANCH_CODE"].ToString() + "&";
        querystring = querystring + "COMP_CODE=" + orderDetailsDataTable.Rows[0]["COMP_CODE"].ToString() + "";       
        }
        var machinDetails=getDataInDataTableFromGridForMachineDetails(gridProcessRoot);
        if (machinDetails.Rows.Count > 0 & machinDetails != null)
        {
            DataView dv = new DataView(machinDetails);
            dv.RowFilter = "MACHINE_CODE='" + machineCode.Trim()+"'";
            if(dv.Table.Rows.Count>0 && dv !=null)
            {
            querystring = querystring + "&PROCESS_CODE=" + orderDetailsDataTable.Rows[0]["ORDER_QTY"].ToString() + "&";
            querystring = querystring + "SERIAL_NO=" + dv.Table.Rows[0]["SERIAL_NO"].ToString() + "&";
            querystring = querystring + "MACHINE_CODE=" +  machineCode + "";
                // dv.Table.Rows[0]["PROCESS_CODE"].ToString() 
            }
        }


        return querystring;
    
    }
    
    protected void gridProcessRoot_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {          
           var gv = (GridView)e.Row.FindControl("gvChildGrid");
           var MACHINE_GROUP = e.Row.Cells[1].Text;
           var machineDetails = SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetMachineCodeForTheMachineGroups(MACHINE_GROUP, ORDER_TYPE, PRODUCT_TYPE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);

           var mdt = ModifyDataTableForMachineDetails(machineDetails);
           BindGridDetails(mdt, gv);
           
        }

    }

    private DataTable CreateDTForMachineDetails()
    {
        var dtMachineDetail = new DataTable();
        dtMachineDetail.Columns.Add("ORDER_NO", typeof(string));
        dtMachineDetail.Columns.Add("PI_NO", typeof(string));
        dtMachineDetail.Columns.Add("PLANNED_QTY", typeof(double));
        dtMachineDetail.Columns.Add("PROS_ROUTE_CODE", typeof(string));
        dtMachineDetail.Columns.Add("MACHINE_GROUP", typeof(string));
        dtMachineDetail.Columns.Add("MACHINE_CODE", typeof(string));
        dtMachineDetail.Columns.Add("TOTAL_NO_OF_MACHINE", typeof(int));
        dtMachineDetail.Columns.Add("AVAILABLE_MACHINE", typeof(int));
        dtMachineDetail.Columns.Add("BOOKED_MACHINE", typeof(int));
        dtMachineDetail.Columns.Add("SCHEDULE_DATE_FROM", typeof(DateTime));
        dtMachineDetail.Columns.Add("SCHEDULE_DATE_TO", typeof(DateTime));
        dtMachineDetail.Columns.Add("SCHEDULE_HOURE", typeof(double));
        dtMachineDetail.Columns.Add("DIA", typeof(string));
        dtMachineDetail.Columns.Add("GAUGE", typeof(string));
        dtMachineDetail.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtMachineDetail.Columns.Add("ORDER_TYPE", typeof(string));
        dtMachineDetail.Columns.Add("COMP_CODE", typeof(string));
        dtMachineDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtMachineDetail.Columns.Add("TDATE", typeof(DateTime));
        dtMachineDetail.Columns.Add("TUSER", typeof(string));
        dtMachineDetail.Columns.Add("ROWSTATE", typeof(string));
        dtMachineDetail.Columns.Add("YEAR", typeof(int));
        //  dtMachineDetail.Columns.Add("FLAG", typeof(string));
        return dtMachineDetail;
    }

    private DataTable ModifyDataTableForMachineDetails(DataTable dt )
    {
        if (!dt.Columns.Contains("START_HOUR"))
        {
            dt.Columns.Add("START_HOUR", typeof(int));

        }
         if (!dt.Columns.Contains("START_MINUT"))
        {
            dt.Columns.Add("START_MINUT", typeof(int));

        }
        if (!dt.Columns.Contains("END_HOUR"))
        {
            dt.Columns.Add("END_HOUR", typeof(int));
        }
         if (!dt.Columns.Contains("END_MINUT"))
        {
            dt.Columns.Add("END_MINUT", typeof(int));

        }
        if (dt.Rows.Count > 0 && dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var startdate=DateTime.Now;
                var enddate = DateTime.Now;
                
                DateTime.TryParse(dt.Rows[i]["SCHEDULED_DATE_FROM"].ToString(), out startdate);
                DateTime.TryParse(dt.Rows[i]["SCHEDULED_DATE_TO"].ToString(), out enddate);
                if (string.IsNullOrEmpty(dt.Rows[i]["SCHEDULED_DATE_TO"].ToString()))
                {                  
                        dt.Rows[i]["SCHEDULED_DATE_FROM"] = DateTime.Now.ToShortDateString();
                        dt.Rows[i]["SCHEDULED_DATE_TO"] = DateTime.Now.ToShortDateString();
                        dt.Rows[i]["START_HOUR"] = DateTime.Now.Hour;                   
                        dt.Rows[i]["START_MINUT"] = DateTime.Now.Minute;
                        dt.Rows[i]["END_HOUR"] = DateTime.Now.Hour;
                        dt.Rows[i]["END_MINUT"] = DateTime.Now.Minute;
                    
                }
                else
                {
                    if (enddate > DateTime.Now)
                    {
                        var S_HOUR = startdate.Hour;
                        var S_MINUT = startdate.Minute;
                        var E_HOUR = enddate.Hour;
                        var E_MINUT = enddate.Minute;
                        dt.Rows[i]["SCHEDULED_DATE_TO"] = enddate.ToShortDateString();
                        dt.Rows[i]["SCHEDULED_DATE_FROM"] = enddate.ToShortDateString();
                        dt.Rows[i]["START_HOUR"] = E_HOUR;
                        dt.Rows[i]["START_MINUT"] = E_MINUT;
                        dt.Rows[i]["END_HOUR"] = E_HOUR;
                        dt.Rows[i]["END_MINUT"] = E_MINUT;
                    }
                    else  
                    {
                        dt.Rows[i]["SCHEDULED_DATE_FROM"] = DateTime.Now.ToShortDateString();
                        dt.Rows[i]["SCHEDULED_DATE_TO"] = DateTime.Now.ToShortDateString();
                        dt.Rows[i]["START_HOUR"] = DateTime.Now.Hour;
                        dt.Rows[i]["START_MINUT"] = DateTime.Now.Minute;
                        dt.Rows[i]["END_HOUR"] = DateTime.Now.Hour;
                        dt.Rows[i]["END_MINUT"] = DateTime.Now.Minute;
                                           
                    }
                }
            }
        }
        dt.AcceptChanges();
        return dt;
    }

    protected bool checkDate(string sdate, string edate)
    {
        bool result = true;
        var sd = DateTime.Now;
        var ed = DateTime.Now;
        var ddate = DateTime.Now;
        DateTime.TryParse(sdate, out sd);        
        DateTime.TryParse(edate, out ed);
        DateTime.TryParse(txtDeleveryDate.Text, out ddate);

        if (!string.IsNullOrEmpty(sdate))
        {
            if (sd < DateTime.Now || ddate < sd )
            {
                result = false;
            }

            
        }

        if (!string.IsNullOrEmpty(edate))
        {
            if (ed < DateTime.Now || ddate < sd)
            {
                result = false;
            }
        }

        if (!string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
        {
            if (ed < sd)
            {
                result = false;
            }
        }
        return result;
    }

    protected DataTable  getChildGridViewValue()
    {
        var dt = CreateDTForMachineDetails();
        for (int i = 0; i < gridProcessRoot.Rows.Count; i++)
        {
            int count = 0;
            var childgrid = (GridView)gridProcessRoot.Rows[i].FindControl("gvChildGrid");
          
            for (int m = 0; m < childgrid.Rows.Count; m++)
            {
                   var chk = (CheckBox)childgrid.Rows[m].FindControl("chkMachine");
                   if (chk.Checked)
                   {
                        var machineGroup= (Label)childgrid.Rows[m].FindControl("lblMachineGroup");
                        var machineCode = (Label)childgrid.Rows[m].FindControl("lblMachineCode");
                        var machineCapacity = (Label)childgrid.Rows[m].FindControl("lblMachineCapacity");
                        //var lblDia = (TextBox)childgrid.Rows[m].FindControl("lblDia");
                        //var lblGauge = (TextBox)childgrid.Rows[m].FindControl("lblGauge");
                        var txtFrom = (TextBox)childgrid.Rows[m].FindControl("txtFrom");
                        var txtTo = (TextBox)childgrid.Rows[m].FindControl("txtTo");
                        var startTime = (TimeSelector)childgrid.Rows[m].FindControl("startTime");
                        var endTime = (TimeSelector)childgrid.Rows[m].FindControl("endTime");
                        
                        var  planned_QTY= (TextBox)grdOrderDetails.Rows[0].FindControl("txtPlannedQty");

                        var sDate = DateTime.Now;
                        var eDate = DateTime.Now;
                        DateTime.TryParse(txtFrom.Text, out sDate);
                        DateTime.TryParse(txtTo.Text, out eDate);

                        var stime = TimeSpan.Parse(string.Format("{0}:{1}", startTime.Hour, startTime.Minute));
                        var etime = TimeSpan.Parse(string.Format("{0}:{1}", endTime.Hour, endTime.Minute));
                        var startDateTime = sDate.Add(stime);
                        var endDateTime = eDate.Add(etime);
                        if (!checkDate(startDateTime.ToString(), endDateTime.ToString()))
                        {
                            CommonFuction.ShowMessage(@"select proper start date and time for the scheduling.");
                           
                        }
                        var Total = endDateTime.Subtract(startDateTime);
                        var totalHours = (Total.TotalHours);
                                             
                        var dr = dt.NewRow();
                        dr["ORDER_NO"] = lblorderno.Text;
                        dr["PLANNED_QTY"] = planned_QTY.Text;
                        dr["PROS_ROUTE_CODE"] = lblprocessroot.Text;
                        dr["MACHINE_GROUP"] = machineGroup.Text;
                        dr["MACHINE_CODE"] = machineCode.Text;
                        dr["SCHEDULE_DATE_FROM"] = startDateTime;
                        dr["SCHEDULE_DATE_TO"] = endDateTime;
                        dr["SCHEDULE_HOURE"] = totalHours;
                        dr["DIA"] = 0;//lblDia.Text;
                        dr["GAUGE"] = 0;// lblGauge.Text;
                        dr["PRODUCT_TYPE"] = PRODUCT_TYPE;
                        dr["ORDER_TYPE"] = ORDER_TYPE;
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["TUSER"] = oUserLoginDetail.UserCode;
                        dr["ROWSTATE"] = "1";
                        dt.Rows.Add(dr);
                       
                        count = count + 1;
                    }

                   if (m == (childgrid.Rows.Count-1) && count==0)
                   {
                       CommonFuction.ShowMessage(@"select checkbox of any machine from the machine list.");                       
                   }
               
            }
           
           
        }
        dt.AcceptChanges();
        return dt;
    }

    protected void gvChildGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var orderDetailsDataTable = getDataInDataTableFromGridForOrderDetails(grdOrderDetails);

            var machine_ID = e.CommandArgument.ToString();
            if (e.CommandName == "MachinSchedule")
            {
                var url = MachineQueryQtringForOrderDetails("MachineScheduling.aspx", orderDetailsDataTable, machine_ID);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=700');", true);
                                
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Machinge Scheduling.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected DataTable GetMachineScheduleDetailsForSelectDate(string PROCESS_ROOT_CODE ,string MACHINE_CODE,DateTime   startDate,DateTime endDate,string COMP_CODE,string BRANCH_CODE,string YEAR)
    {
        return SaitexDL.Interface.Method.OD_ORDER_DEVELOPMENT.GetMachineSchedulingDataForDatess(PROCESS_ROOT_CODE, MACHINE_CODE, startDate, endDate, COMP_CODE, BRANCH_CODE, PRODUCT_TYPE, ORDER_TYPE,YEAR );
       
    }
     
    protected void imgPlanned_Click(object sender, ImageClickEventArgs e)
    {
        string url = "ListOfOrderPlanning.aspx?ORDER_TYPE="+ORDER_TYPE+"&PRODUCT_TYPE="+PRODUCT_TYPE+"&TYPE=PLANNED";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=700,height=600');", true);
                
    }

    protected void imgUnplanned_Click(object sender, ImageClickEventArgs e)
    {
       string url = "ListOfOrderPlanning.aspx?ORDER_TYPE=" + ORDER_TYPE + "&PRODUCT_TYPE=" + PRODUCT_TYPE + "&TYPE=UNPLANNED";
       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=700,height=600');", true);
                
    }

    protected void imgRemaining_Click(object sender, ImageClickEventArgs e)
    {
       string url = "ListOfOrderPlanning.aspx?ORDER_TYPE=" + ORDER_TYPE + "&PRODUCT_TYPE=" + PRODUCT_TYPE + "&TYPE=REMAINING";
       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=700,height=600');", true);
            
    }





    /************************************************************************************************************************************************/
    /************************************************ Added By Nishant Rai at 27-08-2013*************************************************************/
    /************************************************************************************************************************************************/

   
}
