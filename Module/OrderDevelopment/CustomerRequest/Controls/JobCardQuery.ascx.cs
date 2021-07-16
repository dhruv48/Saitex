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

public partial class Module_OrderDevelopment_CustomerRequest_Controls_JobCardQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string PRODUCT_TYPE = string.Empty;
    int BATCH_CODE_TO = 0;
    int BATCH_CODE_FROM = 0;
    string GREY_LOT_NO = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    DateTime StDate;
    DateTime EnDate;
    int YEAR;
    string MACHINE_CODE = string.Empty;
    string PROS_CODE = string.Empty;
    string URL = string.Empty;
    double tAmount = 0, tCost = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            PRODUCT_TYPE = "YARN DYEING";
            if (!IsPostBack)
            {
                InitialControls();
                getJobCardEntryDetail();

            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));

        }
    }
    private void InitialControls()
    {
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
        BindYear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
    }
    private void BindYear()
    {
        try
        {

            string branch = oUserLoginDetail.CH_BRANCHCODE.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);
            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "YEAR";
                ddlYear.DataBind();
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }
            else
            {
                string brnch = oUserLoginDetail.CH_BRANCHCODE.ToString();
                CommonFuction.ShowMessage(" No financial Year associated with " + brnch + " branch ");
                getBrachName();
                //ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                BindYear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                bindFromToDate();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getBrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            //ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            bindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlMachine_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMachineData(e.Text.ToUpper(), e.ItemsOffset);
            ddlMachine.Items.Clear();
            ddlMachine.DataSource = data;
            ddlMachine.DataTextField = "MACHINE_CODE";
            ddlMachine.DataValueField = "MACH_DATA";
            ddlMachine.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetMachineCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in machine selection.\r\nSee error log for detail."));
            // lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetMachineData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT MACHINE_CODE, MACHINE_MAKE, MACHINE_CAPACITY,NO_OF_SPINDLES, ( MACHINE_CODE || '@'|| MACHINE_MAKE || '@'|| MACHINE_CAPACITY || '@'|| NO_OF_SPINDLES ) MACH_DATA FROM MC_MACHINE_MASTER M, CM_COMPANY_MST C, CM_BRANCH_MST B  WHERE C.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "' AND B.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY MACH_DATA) WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_MAKE LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery) WHERE ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and MACH_DATA not in(SELECT * FROM (SELECT * FROM (SELECT MACHINE_CODE, MACHINE_MAKE, MACHINE_CAPACITY,NO_OF_SPINDLES, ( MACHINE_CODE || '@'|| MACHINE_MAKE || '@'|| MACHINE_CAPACITY || '@'|| NO_OF_SPINDLES ) MACH_DATA FROM MC_MACHINE_MASTER M, CM_COMPANY_MST C, CM_BRANCH_MST B  WHERE C.COMP_CODE ='C00001' AND B.BRANCH_CODE = 'SIT0001' ORDER BY MACH_DATA) WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_MAKE LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " order by MACH_DATA";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected int GetMachineCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT MACHINE_CODE, MACHINE_MAKE, MACHINE_CAPACITY,NO_OF_SPINDLES, ( MACHINE_CODE || '@'|| MACHINE_MAKE || '@'|| MACHINE_CAPACITY || '@'|| NO_OF_SPINDLES ) MACH_DATA FROM MC_MACHINE_MASTER M, CM_COMPANY_MST C, CM_BRANCH_MST B  WHERE C.COMP_CODE ='C00001' AND B.BRANCH_CODE = 'SIT0001' ORDER BY MACH_DATA) WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_MAKE LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery)";
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
            //lblMode.Text = ex.ToString();
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
        string PROS_CODE = ddlProcessCode.SelectedValue.ToString();
        string[] CombineData = PROS_CODE.Split('@');
        ddlProcessCode.SelectedValue = CombineData[3].ToString();
    }
    protected void ddlMachine_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        string MAC_DATA = ddlMachine.SelectedValue.ToString();
        string[] CombineData = MAC_DATA.Split('@');
        ddlMachine.SelectedValue = CombineData[0].ToString();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        getJobCardEntryDetail();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
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

        try
        {
            URL = "../Report/JobSheet_EntRep.aspx?BATCH_CODE_FROM=" + txtJobCodeFrom.Text.ToString() + "&BATCH_CODE_TO=" + txtJobCodeTo.Text.ToString() + "&YEAR=" + ddlYear.SelectedValue.ToString() + "&LOT_NUMBER=" + txtLotNo.Text.Trim() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString() + "&MACHINE_CODE=" + ddlMachine.SelectedValue.ToString() + "&PROS_CODE=" + ddlProcessCode.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "error"));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtJobCodeFrom.Text = string.Empty;
            txtJobCodeTo.Text = string.Empty;
            ddlMachine.SelectedIndex = -1;
            txtLotNo.Text = string.Empty;
            ddlProcessCode.SelectedIndex = -1;
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            getJobCardEntryDetail();
        }
        catch
        {
            throw;
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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

    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void bindFromToDate()
    {
        string FIN_YEAR_CODE = ddlYear.SelectedValue.ToString();
        DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "FIN_YEAR_CODE='" + ddlYear.SelectedValue.ToString() + "'";
            if (dv.Count > 0)
            {
                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                {
                    TxtFromDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                    TxtToDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                }
            }
        }
    }
    private void getJobCardEntryDetail()
    
{
        try
        {
            if (txtJobCodeFrom.Text.Trim() != null && txtJobCodeFrom.Text.Trim() != string.Empty)
            {
                BATCH_CODE_FROM = int.Parse(txtJobCodeFrom.Text.Trim());
            }
            else
            {
                BATCH_CODE_FROM = 0;
            }
            if (txtJobCodeTo.Text.Trim() != null && txtJobCodeTo.Text.Trim() != string.Empty)
            {
                BATCH_CODE_TO = int.Parse(txtJobCodeTo.Text.Trim());
            }
            else
            {
                BATCH_CODE_TO = 0;
            }
            if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
            {
                YEAR = Convert.ToInt32(ddlYear.SelectedValue.ToString());
            }
            else
            {
                YEAR = 0;
            }
            if (txtLotNo.Text != null && txtLotNo.Text != string.Empty)
            {
                GREY_LOT_NO = txtLotNo.Text.Trim().ToString();
            }
            else
            {
                GREY_LOT_NO = string.Empty;
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
            if (ddlMachine.SelectedValue.ToString() != null && ddlMachine.SelectedValue.ToString() != string.Empty)
            {
                MACHINE_CODE = ddlMachine.SelectedValue.Trim().ToString();
            }
            else
            {
                MACHINE_CODE = string.Empty;
            }
            if (ddlProcessCode.SelectedValue.ToString() != null && ddlProcessCode.SelectedValue.ToString() != string.Empty)
            {
                PROS_CODE = ddlProcessCode.SelectedValue.Trim().ToString();
            }
            else
            {
                PROS_CODE = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.BATCH_CARD_MST.getJobCardDataDetail(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, YEAR, BATCH_CODE_FROM, BATCH_CODE_TO, GREY_LOT_NO, StDate, EnDate, MACHINE_CODE, PROS_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdBindJobcardDetail.DataSource = dt;
                grdBindJobcardDetail.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

            }
            else
            {
                grdBindJobcardDetail.DataSource = null;
                grdBindJobcardDetail.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No data found by selected Parameter...");
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdBindJobcardDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;
                Label lblBATCH_CODE = (Label)grdRow.FindControl("lblBATCH_CODE");

                SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
                oBATCH_CARD_MST.BATCH_CODE = int.Parse(lblBATCH_CODE.Text.Trim());
                oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                DataTable dtjobCardTrn = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardApprovalTRN(oBATCH_CARD_MST);
                if (dtjobCardTrn != null && dtjobCardTrn.Rows.Count > 0)
                {
                    GridView gvJobCardTrn = (GridView)grdRow.FindControl("grdJobCardTrn");
                    gvJobCardTrn.DataSource = dtjobCardTrn;
                    gvJobCardTrn.DataBind();
                }

                DataTable dtJobTRNDYES = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardApprovalTRNDYES(oBATCH_CARD_MST);
                if (dtJobTRNDYES != null && dtJobTRNDYES.Rows.Count > 0)
                {
                    GridView gvJobCardDYES = (GridView)grdRow.FindControl("grdJobCardDYES");
                    gvJobCardDYES.DataSource = dtJobTRNDYES;
                    gvJobCardDYES.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            //Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
            //lblErrorMessage.Text = ex.ToString();
            throw ex;
        }
    }
    protected void grdJobCardTrn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double totalAmount = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Amount = ((Label)e.Row.FindControl("lblRate")).Text;

                totalAmount = totalAmount + double.Parse(Amount);
                tAmount = tAmount + totalAmount;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double lotSize = Convert.ToDouble(((Label)e.Row.FindControl("lblLotQty")).Text);
                tCost = tAmount / lotSize;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = ((Label)e.Row.FindControl("lblTotal"));
                Label lblCost = ((Label)e.Row.FindControl("lblCost"));
                lblTotal.Text = tAmount.ToString("#.00");
                lblCost.Text = tCost.ToString("#.00");
                tAmount = 0;
                tCost = 0;
            }
        }
        catch (Exception ex)
        {
            //Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
            throw ex;
        }
    }
    protected void grdBindJobcardDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBindJobcardDetail.PageIndex = e.NewPageIndex;
        getJobCardEntryDetail();

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdJobCardDYES_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}