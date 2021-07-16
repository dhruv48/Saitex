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

public partial class Module_Production_Controls_Lot_Making_QueryForm : System.Web.UI.UserControl
{

    DateTime StDate;
    DateTime EnDate;
    DateTime Sdate = System.DateTime.Now;
    DateTime Edate = System.DateTime.Now;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialControls();
                BindGridLotMaking();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindGridLotMaking()
    
    {
        string COMP_CODE = string.Empty;
        string Year = string.Empty;
        string LotNo = string.Empty;
        string Branch = string.Empty;
        string MachineName = string.Empty;
        string PoyDenier= string.Empty;
        string LotType = string.Empty;
        string FinishDenier=string.Empty;
        string Purpose = string.Empty;
        string STATUS = string.Empty;
 
      
        try
        {
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty && ddlBranch.SelectedIndex > 0)
            {
                Branch = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                Branch = string.Empty;
            }

            if (ddlLotNo.SelectedValue.ToString() != null && ddlLotNo.SelectedValue.ToString() != string.Empty && ddlLotNo.SelectedIndex > 0)
            {
                LotNo = ddlLotNo.SelectedValue.ToString();
            }
            else
            {
                LotNo = string.Empty;
            }
            if (ddlMacCode.SelectedValue.ToString() != null && ddlMacCode.SelectedValue.ToString() != string.Empty && ddlMacCode.SelectedIndex > 0)
            {
                MachineName = ddlMacCode.SelectedValue.ToString();
            }
            else
            {
                MachineName = string.Empty;
            }
            if (cmbYarn.SelectedValue.ToString() != null && cmbYarn.SelectedValue.ToString() != string.Empty && cmbYarn.SelectedIndex > 0)
            {
                FinishDenier = cmbYarn.SelectedValue.ToString();
            }
            else
            {
                FinishDenier = string.Empty;
            }
            if (txtItemCode.SelectedValue.ToString() != null && txtItemCode.SelectedValue.ToString() != string.Empty && txtItemCode.SelectedIndex > 0)
            {
                PoyDenier = txtItemCode.SelectedValue.ToString();
            }
            else
            {
                PoyDenier = string.Empty;
            }
            if (ddlLotType.SelectedValue.ToString() != null && ddlLotType.SelectedValue.ToString() != string.Empty && ddlLotType.SelectedIndex > 0)
            {
                LotType = ddlLotType.SelectedValue.ToString();
            }
            else
            {
                LotType = string.Empty;
            }
            if (ddlPurpose.SelectedValue.ToString() != null && ddlPurpose.SelectedValue.ToString() != string.Empty && ddlPurpose.SelectedIndex > 0)
            {
                Purpose = ddlPurpose.SelectedValue.ToString();
            }
            else
            {
                Purpose = string.Empty;
            }


            if (ddlStatus.SelectedValue.ToString() != null && ddlStatus.SelectedValue.ToString() != string.Empty && ddlStatus.SelectedIndex > 0)
            {
                STATUS = ddlStatus.SelectedValue.ToString();
            }
            else
            {
                STATUS=string.Empty;
            }

            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
            }
            else
            {
                StDate = Sdate;
                EnDate = Edate;

            }

            
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.YRN_LOT_MAKING.GetLotMakingQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, LotNo, MachineName, FinishDenier, PoyDenier, LotType, Purpose, STATUS, StDate, EnDate);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                gvLotMakingQuery.DataSource = dt;
                gvLotMakingQuery.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                gvLotMakingQuery.Visible = true;
            }
            else
            {
                gvLotMakingQuery.DataSource = null;
                gvLotMakingQuery.DataBind();
                lblTotalRecord.Text = "0";
                gvLotMakingQuery.Visible = false;
                CommonFuction.ShowMessage("No data found..");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
        }
    }
          
    private void InitialControls()
    {
        try
        {

            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            

            tdPrint.Visible = true;
            BindBranch();
            BindLotNo();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindLotNo()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_LOT_MAKING.getLotNo();
            ddlLotNo.Items.Clear();
            DataView dv = new DataView(dt);
            ddlLotNo.DataSource = dv;
            ddlLotNo.DataValueField = "LOT_NO";
            ddlLotNo.DataTextField = "LOT_NO";
            ddlLotNo.DataBind();
            ddlLotNo.Items.Insert(0, new ListItem("-------Select-------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void BindBranch()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            ddlBranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlBranch.DataSource = dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--------Select---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string Query_String = string.Empty;
        try
        {
            string QueryString = "";
            bool flag = false;

            if (ddlBranch.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "BRANCH_NAME=" + ddlBranch.SelectedValue.Trim();
                flag = true;
            }

            if (ddlLotNo.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "LOT_NO=" + ddlLotNo.SelectedValue.Trim();
                flag = true;
            }

           
            if (ddlMacCode.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "MACHINE_NAME=" + ddlMacCode.SelectedValue.Trim();
                flag = true;
            }

            if (cmbYarn.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "FINISHED_DENIER=" + cmbYarn.SelectedValue.Trim();
                flag = true;
            }
           
           
            if (txtItemCode.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "POY=" + txtItemCode.SelectedValue.Trim();
                flag = true;
            }
            if (ddlPurpose.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "PURPOSE=" + ddlPurpose.SelectedValue.Trim();
                flag = true;
            }
            if (ddlStatus.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "CONF_FLAG=" + ddlStatus.SelectedValue.Trim();
                flag = true;
            }
            


            string URL = "../../Production/Report/LotMakingReportLotWise.aspx?Query_String =" + QueryString;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Open Print Page.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridLotMaking();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cmbLotNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
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
           // lblMode.Text = ex.ToString();
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
    private DataTable GetLotNoItems(string text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT * FROM (SELECT DISTINCT  LOT_NO,PRTY_NAME FROM   v_TX_FIBER_IR_TRN  WHERE  FIBER_CODE LIKE '" + txtItemCode.SelectedValue + "' AND  (UPPER(LOT_NO) LIKE :SearchQuery OR UPPER(PRTY_NAME) LIKE  :SearchQuery OR UPPER(FIBER_DESC) LIKE :SearchQuery OR UPPER(FIBER_CODE) LIKE :SearchQuery OR UPPER(PRTY_CODE) LIKE  :SearchQuery )  ) WHERE  ROWNUM <= 15 ";
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

    private DataTable GetFindLotNoItems(string text, int startOffset)
    {
        try
        {

            string CommandText = "SELECT * FROM (SELECT DISTINCT  LOT_NO FROM   YRN_LOT_MAKING WHERE   (UPPER(LOT_NO) LIKE :SearchQuery ) ) WHERE  ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " WHERE  LOT_NO NOT IN  ( SELECT LOT_NO FROM (SELECT DISTINCT  LOT_NO FROM   YRN_LOT_MAKING WHERE   (UPPER(LOT_NO) LIKE :SearchQuery ) ) WHERE  ROWNUM <= " + startOffset + ")";
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

            string CommandText = " SELECT DISTINCT  LOT_NO FROM   YRN_LOT_MAKING WHERE   (UPPER(LOT_NO) LIKE :SearchQuery )  ";
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
    protected void ddlMacCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
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
            //lblMode.Text = ex.ToString();
        }

    }
    protected int GetMacCount(string text)
    {
        try
        {
            DataTable dt = new DataTable();

            string whereClause = " ";
            string sortExpression = " ";
            string commandText = "SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC)  ";

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
            string commandText = string.Empty;
            if (startoffset == 0)
            {
                commandText += "SELECT 'MISC' AS MACHINE_CODE, 'MISC' AS MACHINE_GROUP, 0 AS MACHINE_CAPACITY, 'MISC' AS MACHINE_SEGMENT, 'MISC' AS MACHINE_TYPE, 'MISC' AS MACHINE_SEC FROM DUAL UNION ALL ";
            }
            commandText += " SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += " AND MACHINE_CODE NOT IN(SELECT MACHINE_CODE FROM (SELECT MACHINE_CODE,MACHINE_GROUP,MACHINE_CAPACITY,MACHINE_SEGMENT,MACHINE_TYPE,MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC)WHERE ROWNUM <= '" + startoffset + "')";
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
  

    private DataTable BindYarnCodeCombo(string text, int startOffset)
    {
        try
        {


            string CommandText = "SELECT   'WHITE' SHADE_FAMILY,'WHITE' SHADE_CODE,      'IND_TYPE' IND_TYPE, i.YARN_CODE,  i.YARN_TYPE,    i.YARN_desc,   (   i.YARN_CODE   || '@' || i.YARN_desc    || '@'     || 'WHITE'    || '@'   || 'WHITE')         COMBINED     FROM   YRN_MST i    WHERE   UPPER (YARN_CODE) LIKE :SearchQuery          OR UPPER (YARN_desc) LIKE :SearchQuery       AND    ROWNUM <= 15";

            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN    (      SELECT   i.YARN_CODE   FROM   YRN_MST i    WHERE   UPPER (YARN_CODE) LIKE :SearchQuery          OR UPPER (YARN_desc) LIKE :SearchQuery       AND     ROWNUM <= " + startOffset + " )   ";
            }

            string SortExpression = " order by YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToString(), "");

        }
        catch
        {
            throw;
        }
    }

    protected void cmbYarn_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = BindYarnCodeCombo(e.Text.ToUpper(), e.ItemsOffset);

            if (data != null && data.Rows.Count > 0)
            {
                cmbYarn.Items.Clear();

                cmbYarn.DataSource = data;
                cmbYarn.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
           // lblMode.Text = ex.ToString();
        }
    }
    protected int GetYarnCount(string text)
    {


        string CommandText = " SELECT  i.YARN_CODE     FROM   YRN_MST i    WHERE   UPPER (YARN_CODE) LIKE :SearchQuery          OR UPPER (YARN_desc) LIKE :SearchQuery   ";
        string WhereClause = "  ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
    }
    protected void txtItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
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
            //lblMode.Text = ex.ToString();
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
    protected void txtItemCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        cmbLotNo.SelectedIndex = -1;
    }
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                if (StartDate < StDate || StartDate > EnDate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = StDate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtToDate.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
}
