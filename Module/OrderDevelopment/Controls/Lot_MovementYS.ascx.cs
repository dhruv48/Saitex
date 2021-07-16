using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
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

public partial class Module_OrderDevelopment_Controls_Lot_MovementYS : System.Web.UI.UserControl
{
    private static string FromBranchCode = string.Empty;

    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string PRODUCT_TYPE = string.Empty;

    public string PRODUCTTYPE
    {
        get { return PRODUCT_TYPE; }
        set { PRODUCT_TYPE = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                txtToLotQty.Attributes.Add("readonly", "readonly");

                Page_Control_Initial();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in page loading.\r\nSee error log for detail."));
        }
    }

    private void Page_Control_Initial()
    {
        try
        {
            Clear_Control();
            Get_Max_EntryNo();
            GetDepartment();
            Bind_Control(DDLUOM, "UOM");
        }
        catch
        {
            throw;
        }
    }

    private void Clear_Control()
    {
        try
        {
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            txtCheckBy.Text = string.Empty;
            txtEntryDate.Text = string.Empty;
            txtEntryNo.Text = string.Empty;
            txtFromBatchNo.Text = string.Empty;
            txtFromDepartment.Text = string.Empty;
            txtFromLocation.Text = string.Empty;
            txtFromLotQty.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtPartyAddress.Text = string.Empty;
            txtPartyCode.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtToBatchNo.Text = string.Empty;
            txtToLocation.Text = string.Empty;
            txtToLotQty.Text = string.Empty;
            TxtFromNOU.Text = string.Empty;
            TxtFromUOM.Text = string.Empty;
            TxtFromWOU.Text = string.Empty;
            TxtTONOU.Text = string.Empty;
            TxtToWOU.Text = string.Empty;
            lblProsCode.Text = string.Empty;
            DDLUOM.SelectedIndex = -1;
            ddlLotIdNo.SelectedIndex = -1;
            ddlToDepartment.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    private void Get_Max_EntryNo()
    {
        try
        {

            string Max_ID = SaitexBL.Interface.Method.TX_FIBER_WIP_LOT_MOVE_LOG.Max_Entry_NO(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            txtEntryNo.Text = Max_ID;
            txtEntryDate.Text = System.DateTime.Now.Date.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    private void Bind_Control(DropDownList DDL, string MST_NAME)
    {
        try
        {
            DDL.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            DDL.DataSource = dt;
            DDL.DataTextField = "MST_DESC";
            DDL.DataValueField = "MST_CODE";
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("--------Select-------"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getDepartmentByGroup("PRODUCTION");
            ddlToDepartment.DataSource = dt;
            ddlToDepartment.DataValueField = "DEPT_CODE";
            ddlToDepartment.DataTextField = "DEPT_NAME";
            ddlToDepartment.DataBind();
            ddlToDepartment.Items.Insert(0, new ListItem("-------SELECT-------", ""));
            dt.Dispose();
            dt = null;
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
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( ORDER_NO|| '@'|| LOT_NUMBER|| '@'|| DEPT_CODE|| '@'|| PROS_CODE ) LOT_DATA, COMP_CODE, BRANCH_CODE, DEPT_CODE, ORDER_NO, LOT_NUMBER, PRTY_CODE, PROS_CODE, PRTY_NAME AS PARTY FROM V_TX_FIBER_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND WS.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (StartOffSet != 0)
            {
                whereClause += " AND LOT_DATA NOT IN(SELECT LOT_DATA FROM (SELECT * FROM (SELECT ( ORDER_NO || '@' || LOT_NUMBER || '@' || DEPT_CODE || '@' || PROS_CODE) LOT_DATA,COMP_CODE,BRANCH_CODE,DEPT_CODE,ORDER_NO,LOT_NUMBER,PRTY_CODE,PROS_CODE,PRTY_NAME AS PARTY FROM V_TX_FIBER_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE ='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND WS.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery) WHERE ROWNUM <= '" + StartOffSet + "')";
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
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT ( ORDER_NO|| '@'|| LOT_NUMBER|| '@'|| DEPT_CODE|| '@'|| PROS_CODE) LOT_DATA, COMP_CODE, BRANCH_CODE, DEPT_CODE, ORDER_NO, LOT_NUMBER, PRTY_CODE, PROS_CODE, PRTY_NAME AS PARTY FROM V_TX_FIBER_WIP_STOCK WS WHERE WS.STOCK_QTY > 0 AND WS.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND WS.DEPT_CODE = '" + oUserLoginDetail.VC_DEPARTMENTCODE + "' AND WS.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND WS.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' ORDER BY LOT_NUMBER) WHERE ORDER_NO LIKE :SearchQuery OR LOT_NUMBER LIKE :SearchQuery OR PARTY LIKE :SearchQuery)  ";
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

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Insert_Record())
            {
                CommonFuction.ShowMessage("Record Save Sucessfully");
                Clear_Control();
                Get_Max_EntryNo();
            }
            else
            {
                CommonFuction.ShowMessage("Unable to Save!Please try again");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in record saving"));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Insert_Record())
            {
                CommonFuction.ShowMessage("Record Update Sucessfully");
                Clear_Control();
                Get_Max_EntryNo();
            }
            else
            {
                CommonFuction.ShowMessage("Unable to Update!Please try again");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in record updating"));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear_Control();
            Get_Max_EntryNo();
        }
        catch
        {
            throw;
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));

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

            DataTable DT = new DataTable();
            DT.Rows.Clear();
            DT = SaitexBL.Interface.Method.TX_FIBER_WIP_LOT_MOVE_LOG.Party_Detail_ByLotID(sORDER_NO, sLOT_NUMBER, oUserLoginDetail.VC_DEPARTMENTCODE, sPROS_CODE);
            if (DT.Rows.Count > 0)
            {

                txtOrderNo.Text = DT.Rows[0]["ORDER_NO"].ToString();
                txtPartyCode.Text = DT.Rows[0]["PRTY_CODE"].ToString();
                txtPartyAddress.Text = DT.Rows[0]["PRTY_NAME"].ToString();

                txtFromLotQty.Text = DT.Rows[0]["STOCK_QTY"].ToString();
                txtOrdArticle.Text = DT.Rows[0]["ORD_ARTICAL_DESC"].ToString();

                txtFromDepartment.Text = DT.Rows[0]["DEPT_NAME"].ToString();
                LblFromDept.Text = DT.Rows[0]["DEPT_CODE"].ToString();

                txtFromLocation.Text = DT.Rows[0]["BRANCH_NAME"].ToString();

                FromBranchCode = DT.Rows[0]["BRANCH_CODE"].ToString();
                txtFromBatchNo.Text = DT.Rows[0]["BATCH_NO"].ToString();

                TxtFromNOU.Text = DT.Rows[0]["NO_OF_UNIT"].ToString();
                TxtFromUOM.Text = DT.Rows[0]["UOM_OF_UNIT"].ToString();
                TxtFromWOU.Text = DT.Rows[0]["WEIGHT_OF_UNIT"].ToString();

                txtFrPros.Text = DT.Rows[0]["PROS_CODE"].ToString();
            }
        }
        catch
        {
            throw;
        }
    }

    private bool Insert_Record()
    {
        bool Res = false;
        try
        {
            if (decimal.Parse(txtFromLotQty.Text.Trim().ToString()) >= decimal.Parse(txtToLotQty.Text.Trim().ToString()))
            {
                SaitexDM.Common.DataModel.TX_FIBER_WIP_LOT_MOVE_LOG YR = new SaitexDM.Common.DataModel.TX_FIBER_WIP_LOT_MOVE_LOG();
                YR.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                YR.CHECK_BY = txtCheckBy.Text.Trim().ToString();
                YR.COMP_CODE = oUserLoginDetail.COMP_CODE;
                // YR.CONF_DATE = "";
                YR.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                YR.ENTRY_DATE = DateTime.Parse(txtEntryDate.Text.Trim().ToString());
                YR.ENTRY_NO = txtEntryNo.Text.Trim().ToString();
                YR.ENTRY_TYPE = "DPI01";
                YR.FR_BATCH_NO = txtFromBatchNo.Text.Trim().ToString();
                YR.FR_BRANCH_CODE = FromBranchCode.Trim().ToString();
                YR.FR_DEPT_CODE = LblFromDept.Text.Trim().ToString();
                YR.FR_MOVE_QTY = decimal.Parse(txtFromLotQty.Text.Trim().ToString());
                YR.FR_NO_OF_UNIT = decimal.Parse(TxtFromNOU.Text.Trim().ToString());
                YR.FR_ORDERNO = txtOrderNo.Text.Trim().ToString();
                YR.FR_PROS_CODE = txtFrPros.Text;
                YR.FR_UOM_OF_UNIT = TxtFromUOM.Text.Trim().ToString();
                YR.FR_WEIGHT_OF_UNIT = decimal.Parse(TxtFromWOU.Text.Trim().ToString());
                YR.LOT_NUMBER = ddlLotIdNo.SelectedText.Trim().ToString();
                YR.LOT_QTY = decimal.Parse(txtToLotQty.Text.Trim().ToString());
                YR.REMARKS = txtRemarks.Text.Trim().ToString();
                YR.TO_BATCH_NO = txtToBatchNo.Text.Trim().ToString();
                YR.TO_BRANCH_CODE = txtToLocation.Text.Trim().ToString();
                YR.TO_DEPT_CODE = ddlToDepartment.SelectedValue.Trim().ToString();
                YR.TO_MOVE_QTY = decimal.Parse(txtToLotQty.Text.Trim().ToString());
                YR.TO_NO_OF_UNIT = decimal.Parse(TxtTONOU.Text.Trim().ToString());
                YR.TO_ORDERNO = txtOrderNo.Text.Trim().ToString();
                YR.TO_PROS_CODE = txtFrPros.Text;
                YR.TO_UOM_UNIT = DDLUOM.SelectedValue.Trim().ToString();
                YR.TO_WEIGHT_OF_UNIT = decimal.Parse(TxtToWOU.Text.Trim().ToString());
                YR.TUSER = oUserLoginDetail.UserCode;
                YR.UOM = DDLUOM.SelectedValue.Trim().ToString();
                YR.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                Res = SaitexBL.Interface.Method.TX_FIBER_WIP_LOT_MOVE_LOG.Insert_Record(YR);

            }
            else
            {
                Common.CommonFuction.ShowMessage("FromLotQty is less then ToLotQty");
            }
            return Res;
        }
        catch
        {
            throw;
        }
    }
}
