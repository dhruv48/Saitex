using System;
using System.Collections;
using System.Configuration;
using System.Data;
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
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Fiber_Controls_Fiber_tran_query : System.Web.UI.UserControl
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    string branch = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }

    }

    private void InitialControls()
    {
        try
        {
            grid_trn_Query.Visible = false;
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            GetBranchName();
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            BindFiberType();
            //BindFiberCat();
            BindPartyCode();
            BindTrnType();
            BindDepartment();
            ddlFiber.SelectedIndex = -1;
            GridFiberTrn();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetBranchName()
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
            //ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", string.Empty));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindYear()
    {
        try
        {
            string branch = ddlBranch.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);
            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataBind();
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));

                dt.Dispose();
                dt = null;
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFiberType()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiberCat();
            DataView Dv = new DataView(dt);
            ddlFiberType.DataSource = Dv;
            ddlFiberType.DataValueField = "FIBER_CAT";
            ddlFiberType.DataTextField = "FIBER_CAT";
            ddlFiberType.DataBind();
            ddlFiberType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //private void BindFiberCat()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.YRN_MST.GetFiberCate();
    //        ddlFiberCate.DataSource = dt;
    //        ddlFiberCate.DataValueField = "Fiber_CAT";
    //        ddlFiberCate.DataTextField = "Fiber_CAT";
    //        ddlFiberCate.DataBind();
    //        ddlFiberCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void BindPartyCode()
    {
        try
        {
            ddlPartycode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlPartycode.DataTextField = "PRTY_NAME";
                ddlPartycode.DataValueField = "PRTY_CODE";
                ddlPartycode.DataSource = dt;
                ddlPartycode.DataBind();
            }

            ddlPartycode.Items.Insert(0, new ListItem("---------------All---------------", ""));

        }
        catch (Exception ex)
        {
        }
    }

    private void BindTrnType()
    {
        try
        {
            ddlTrnType.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.getTransType();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTrnType.DataTextField = "TRN_TDESC";
                ddlTrnType.DataValueField = "TRN_TYPE";
                ddlTrnType.DataSource = dt;
                ddlTrnType.DataBind();
            }
            ddlTrnType.Items.Insert(0, new ListItem("-----------All---------------", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
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

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControls();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }

    private void GridFiberTrn()
    {
        DateTime StDate;
        DateTime EnDate;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string FIBER_CAT = string.Empty;
       // string Fiber_CAT = string.Empty;
        string FIBER_CODE = string.Empty;
        string TRAN_TYPE = string.Empty;
        string PARTY_CODE = string.Empty;
        try
        {
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }
            //if (ddlFiberCate.SelectedValue.ToString() != null && ddlFiberCate.SelectedValue.ToString() != string.Empty)
            //{
            //    Fiber_CAT = ddlFiberCate.SelectedValue.ToString();
            //}
            //else
            //{
            //    Fiber_CAT = string.Empty;
            //}

            if (ddlFiberType.SelectedValue.ToString() != null && ddlFiberType.SelectedValue.ToString() != string.Empty)
            {
                FIBER_CAT = ddlFiberType.SelectedValue.ToString();
            }
            else
            {
                FIBER_CAT = string.Empty;
            }

            if (ddlFiber.SelectedValue.ToString() != null && ddlFiber.SelectedValue.ToString() != string.Empty)
            {
                FIBER_CODE = ddlFiber.SelectedValue.Trim().ToString();
            }
            else
            {
                FIBER_CODE = string.Empty;
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


            if (ddlTrnType.SelectedValue.ToString() != null && ddlTrnType.SelectedValue.ToString() != string.Empty)
            {
                TRAN_TYPE = ddlTrnType.SelectedValue.ToString();
            }
            else
            {
                TRAN_TYPE = string.Empty;

            }

            if (ddlPartycode.SelectedValue.ToString() != null && ddlPartycode.SelectedValue.ToString() != string.Empty)
            {
                PARTY_CODE = ddlPartycode.SelectedValue.ToString();
            }
            else
            {
                PARTY_CODE = string.Empty;

            }
            DataTable dt = new DataTable();

            dt = SaitexBL.Interface.Method.TX_FIBER_MST.FIBERTRANSACTION(BRANCH_CODE, DEPT_CODE, FIBER_CAT, FIBER_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE);
            if (dt.Rows.Count > 0)
            {
                grid_trn_Query.DataSource = dt;
                grid_trn_Query.DataBind();
                grid_trn_Query.Visible = true;
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grid_trn_Query.Visible = true;

            }
            else
            {
                grid_trn_Query.DataSource = null;
                grid_trn_Query.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                grid_trn_Query.Visible = true;
                lblTotalRecord.Text = "0";
                grid_trn_Query.Visible = true;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        try
        {
            GridFiberTrn();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnExit_Click1(object sender, ImageClickEventArgs e)
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

    protected void grid_trn_Query_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridFiberTrn();

            grid_trn_Query.PageIndex = e.NewPageIndex;
            grid_trn_Query.DataBind();

        }
        catch (Exception ex)
        {
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

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void ddlFiber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiber();
            DataView Dv = new DataView(dt);
            ddlFiber.DataSource = Dv;
            ddlFiber.DataValueField = "FIBER_CODE";
            ddlFiber.DataTextField = "FIBER_CODE";
            ddlFiber.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }

    protected void Item_LOV_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlFiber.Items.Clear();

                ddlFiber.DataSource = data;
                ddlFiber.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fiber loading.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();

        }
    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/FiberTransactionReport.aspx?BRANCH_CODE=" + ddlBranch.SelectedValue.ToString() + "&YEAR=" + ddlYear.SelectedItem.Text.ToString() + "&DEPT_CODE=" + ddlDepartment.SelectedValue.ToString() + "&FIBER_CAT=" + ddlFiberType.SelectedValue.ToString() + "&FIBER_CODE=" + ddlFiber.SelectedValue.ToString() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString() + "&TRAN_TYPE=" + ddlTrnType.SelectedValue.ToString() + "&PARTY_CODE=" + ddlPartycode.SelectedValue.ToString() + "&BRANCH=" + ddlBranch.SelectedItem.ToString() + "&TRNTYPE=" + ddlTrnType.SelectedItem.ToString() + "&FIBERCAT=" + ddlFiberType.SelectedItem.ToString() + "&PARTY=" + ddlPartycode.SelectedItem.ToString() + "&DEPARTMENT1=" + ddlDepartment.SelectedItem.ToString() + "&YARN1=" + ddlFiber.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Open Report.\r\nSee error log for detail."));
        }
    }
    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT FIBER_CODE, FIBER_DESC, FIBER_CAT FROM TX_FIBER_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND FIBER_code NOT IN (SELECT FIBER_CODE FROM (SELECT FIBER_CODE, FIBER_DESC FROM TX_FIBER_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY FIBER_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   FIBER_CODE, FIBER_DESC, FIBER_CAT FROM   TX_FIBER_MASTER WHERE      FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY   FIBER_CODE) asd WHERE FIBER_CAT <> 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY FIBER_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
}

