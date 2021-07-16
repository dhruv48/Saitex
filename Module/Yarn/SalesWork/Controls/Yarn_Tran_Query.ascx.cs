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

public partial class Module_Yarn_SalesWork_Controls_Yarn_Tran_Query : System.Web.UI.UserControl
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    string branch = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    double tQty = 0;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
                GridYarnTrn();
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
            //BindYarnType();
            //BindYarnCat();

            bindCategory("YARN_CAT");
            bindYarnType("YARN_TYPE");

            BindPartyCode();
            BindTrnType();
            BindDepartment();
            BindToDepartment();
            ddlYarn.SelectedIndex = -1;
            BindDropDown(ddllocation);
            BindDepartment(ddlstore);
            cmbShade.SelectedIndex = -1;
            TxtLotNo.Text = string.Empty;

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

    private void BindYarnType()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnType();
            DataView Dv = new DataView(dt);
            ddlYarnType.DataSource = Dv;
            ddlYarnType.DataValueField = "YARN_TYPE";
            ddlYarnType.DataTextField = "YARN_TYPE";
            ddlYarnType.DataBind();
            ddlYarnType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindYarnCat()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnCate();
            ddlYarnCate.DataSource = dt;
            ddlYarnCate.DataValueField = "YARN_CAT";
            ddlYarnCate.DataTextField = "YARN_CAT";
            ddlYarnCate.DataBind();
            ddlYarnCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bindCategory(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlYarnCate.Items.Clear();
                ddlYarnCate.DataSource = dt;
                ddlYarnCate.DataTextField = "MST_DESC";
                ddlYarnCate.DataValueField = "MST_DESC";
                ddlYarnCate.DataBind();
                ddlYarnCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindYarnType(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAMEForYarnType(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                // dv.RowFilter = "MST_DESC='" + TYPE + "'";
                if (dv != null && dv.Count > 0)
                {
                    ddlYarnType.Items.Clear();
                    ddlYarnType.DataSource = dv;
                    ddlYarnType.DataTextField = "MST_CODE";
                    ddlYarnType.DataValueField = "MST_CODE";
                    ddlYarnType.DataBind();
                    ddlYarnType.Items.Insert(0, new ListItem("---------------All---------------", ""));
                    dt.Dispose();
                    dt = null;
                }
                else
                {
                    ddlYarnType.Items.Clear();
                    ddlYarnType.Items.Insert(0, new ListItem("------NoItems------", "0"));

                }
            }
        }
        catch
        {
            throw;
        }


    }



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

            DataTable dt = SaitexBL.Interface.Method.YRN_MST.getTransType();
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
    private void BindToDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlToDepartment.DataSource = dt;
            ddlToDepartment.DataValueField = "DEPT_CODE";
            ddlToDepartment.DataTextField = "DEPT_NAME";
            ddlToDepartment.DataBind();
            ddlToDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
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

    private void GridYarnTrn()
    {
        DateTime StDate;
        DateTime EnDate;
        string DEPT_CODE = string.Empty;
        string TO_DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string YARN_TYPE = string.Empty;
        string YARN_CAT = string.Empty;
        string YARN_CODE = string.Empty;
        string TRAN_TYPE = string.Empty;
        string PARTY_CODE = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
        string YARN_SHADE_FAMILY = string.Empty;
        string YARN_SHADE = string.Empty;
        string LOT_NO = string.Empty;


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
            if (ddlToDepartment.SelectedValue.ToString() != null && ddlToDepartment.SelectedValue.ToString() != string.Empty)
            {
                TO_DEPT_CODE = ddlToDepartment.SelectedValue.ToString();
            }
            else
            {
                TO_DEPT_CODE = string.Empty;
            }
            if (ddlYarnCate.SelectedValue.ToString() != null && ddlYarnCate.SelectedValue.ToString() != string.Empty)
            {
                YARN_CAT = ddlYarnCate.SelectedValue.ToString();
            }
            else
            {
                YARN_CAT = string.Empty;
            }

            if (ddlYarnType.SelectedValue.ToString() != null && ddlYarnType.SelectedValue.ToString() != string.Empty)
            {
                YARN_TYPE = ddlYarnType.SelectedValue.ToString();
            }
            else
            {
                YARN_TYPE = string.Empty;
            }

            if (ddlYarn.SelectedValue.ToString() != null && ddlYarn.SelectedValue.ToString() != string.Empty)
            {
                YARN_CODE = ddlYarn.SelectedValue.Trim().ToString();
            }
            else
            {
                YARN_CODE = string.Empty;
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
            if (ddllocation.SelectedValue.ToString() != null && ddllocation.SelectedValue.ToString() != string.Empty)
            {
                LOCATION = ddllocation.SelectedValue.ToString();
            }
            else
            {
                LOCATION = string.Empty;
            }
            if (ddlstore.SelectedValue.ToString() != null && ddlstore.SelectedValue.ToString() != string.Empty)
            {
                STORE = ddlstore.SelectedValue.ToString();
            }
            else
            {
                STORE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
            {
                YARN_SHADE = cmbShade.SelectedValue.Trim().ToString();
            }
            else
            {
                YARN_SHADE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedText))
            {
                YARN_SHADE_FAMILY = cmbShade.SelectedText;
            }
            else
            {
                YARN_SHADE_FAMILY = string.Empty;
            }

            if(TxtLotNo.Text.Trim() != string.Empty)
            {
                LOT_NO = TxtLotNo.Text.Trim().ToString();

            }
            else
            {
                LOT_NO = string.Empty;

            }


            DataTable dt = new DataTable();

            dt = SaitexBL.Interface.Method.YRN_MST.YARNTRANSACTION(BRANCH_CODE, DEPT_CODE, TO_DEPT_CODE, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE, LOCATION, STORE, YARN_SHADE_FAMILY, YARN_SHADE,LOT_NO);
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
            GridYarnTrn();
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
            GridYarnTrn();

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

    protected void ddlYarn_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarn();
            DataView Dv = new DataView(dt);
            ddlYarn.DataSource = Dv;
            ddlYarn.DataValueField = "YARN_CODE";
            ddlYarn.DataTextField = "YARN_CODE";
            ddlYarn.DataBind();
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
                ddlYarn.Items.Clear();

                ddlYarn.DataSource = data;
                ddlYarn.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();

        }
    }
    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND YARN_code NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY YARN_CODE";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd WHERE YARN_TYPE <> 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = "ORDER BY YARN_CODE";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
    private void BindDropDown(DropDownList ddllocation)
    {
        try
        {
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {


                ddllocation.DataSource = dt;
                ddllocation.DataValueField = "MST_DESC";
                ddllocation.DataTextField = "MST_DESC";
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }

            else
            {
                ddllocation.DataSource = null;
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

            }
            ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
        }
        catch
        {
            throw;
        }
    }
    private void BindDepartment(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetShadeItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));

        }
    }

    protected DataTable GetShadeItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_NAME ||'@' ||T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetShadeItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch
        {
            throw;
        }
    }


    protected void grid_trn_Query_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double totalQty = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Qty = ((Label)e.Row.FindControl("lblTrnQty")).Text;

                totalQty = totalQty + double.Parse(Qty);
                tQty = tQty + totalQty;
            }
            

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotal = ((Label)e.Row.FindControl("lblTotal"));
                //Label lblCost = ((Label)e.Row.FindControl("lblCost"));
                lblTotal.Text = tQty.ToString();
                //lblCost.Text = tCost.ToString("#.00");
                tQty = 0;
                //tCost = 0;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
            throw ex;
        }

    }
}
