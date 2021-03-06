using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using errorLog;

using System.Collections;
using System.Configuration;

using System.Linq;
using System.Web;
using System.Web.Security;

using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class Module_Inventory_Controls_MaterialIssue : System.Web.UI.UserControl
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
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
                Initial_Control();

                try
                {
                    gridmaterialledger();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }

    private void Initial_Control()
    {
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
        getBrachName();
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        bindyear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
        getDepartment();
        getItemType();
        getItemCategory();
        //gridmaterialledger();
        txtICODE.SelectedIndex = -1;
        bindddltrntype();
        bindddlPartycode();
        BindDropDown(ddllocation);
        BindDepartment(ddlstore);
        
    }

    private void bindyear()
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
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(brnch + " No have financial year & data .");
                getBrachName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                bindyear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                bindFromToDate();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
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

    private void getDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
             //dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";

            //ddlDepartment.DataValueField = "MST_CODE";
            //ddlDepartment.DataTextField = "MST_NAME";

            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }

    private void getItemType()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemType();
            DataView Dv = new DataView(dt);
            ddlItemType.DataSource = Dv;
            ddlItemType.DataValueField = "ITEM_TYPE";
            ddlItemType.DataTextField = "ITEM_TYPE";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getItemCategory()
    {
        try
        {

            DataTable dt = new DataTable();

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemCate();
            ddlItemCate.DataSource = dt;
            ddlItemCate.DataValueField = "CAT_CODE";
            ddlItemCate.DataTextField = "CAT_CODE";
            ddlItemCate.DataBind();
            ddlItemCate.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
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

    private void gridmaterialledger()
    {

        DateTime StDate;
        DateTime EnDate;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string ITEM_TYPE = string.Empty;
        string ITEM_CATE = string.Empty;
        string ITEM_CODE = string.Empty;
        string TRAN_TYPE = string.Empty;
        string PARTY_CODE = string.Empty;
        string YEAR = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
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
            if (ddlItemCate.SelectedValue.ToString() != null && ddlItemCate.SelectedValue.ToString() != string.Empty)
            {
                ITEM_CATE = ddlItemCate.SelectedValue.ToString();
            }
            else
            {
                ITEM_CATE = string.Empty;
            }

            if (ddlItemType.SelectedValue.ToString() != null && ddlItemType.SelectedValue.ToString() != string.Empty)
            {
                ITEM_TYPE = ddlItemType.SelectedValue.ToString();
            }
            else
            {
                ITEM_TYPE = string.Empty;
            }
            if (txtICODE.SelectedValue.ToString() != null && txtICODE.SelectedValue.ToString() != string.Empty)
            {
                ITEM_CODE = txtICODE.SelectedValue.Trim().ToString();
            }
            else
            {
                ITEM_CODE = string.Empty;
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

            if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
            {
                YEAR = ddlYear.SelectedItem.ToString().Trim();
            }
            else
            {
                YEAR = string.Empty;
            }
            if (ddllocation.SelectedValue.ToString() != null && ddllocation.SelectedValue.ToString() != string.Empty)
            {
                LOCATION = ddllocation.SelectedItem.ToString().Trim();
            }
            else
            {
                LOCATION = string.Empty;
            }
            if (ddlstore.SelectedItem.ToString() != null && ddlstore.SelectedItem.ToString() != string.Empty && ddlstore.SelectedItem.ToString().Trim() != "---------------All---------------")
            {
                STORE = ddlstore.SelectedItem.Text.ToString().Trim();
            }
            else
            {
                STORE = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.ItemMaster.MaterialTransaction(BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE,YEAR,LOCATION,STORE);
            if (dt.Rows.Count > 0)
            {
                GridLedger.DataSource = dt;
                GridLedger.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                GridLedger.Visible = true;
            }
            else
            {
                GridLedger.DataSource = dt;
                GridLedger.DataBind();
                lblTotalRecord.Text = "0";
                Common.CommonFuction.ShowMessage("Record Not Available By Selected Parameter.");
                GridLedger.Visible = false;
            }
            Initial_Control();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }






    private DataTable gridmaterialledger1()
    {

        DateTime StDate;
        DateTime EnDate;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string ITEM_TYPE = string.Empty;
        string ITEM_CATE = string.Empty;
        string ITEM_CODE = string.Empty;
        string TRAN_TYPE = string.Empty;
        string PARTY_CODE = string.Empty;
        string YEAR = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
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
            if (ddlItemCate.SelectedValue.ToString() != null && ddlItemCate.SelectedValue.ToString() != string.Empty)
            {
                ITEM_CATE = ddlItemCate.SelectedValue.ToString();
            }
            else
            {
                ITEM_CATE = string.Empty;
            }

            if (ddlItemType.SelectedValue.ToString() != null && ddlItemType.SelectedValue.ToString() != string.Empty)
            {
                ITEM_TYPE = ddlItemType.SelectedValue.ToString();
            }
            else
            {
                ITEM_TYPE = string.Empty;
            }
            if (txtICODE.SelectedValue.ToString() != null && txtICODE.SelectedValue.ToString() != string.Empty)
            {
                ITEM_CODE = txtICODE.SelectedValue.Trim().ToString();
            }
            else
            {
                ITEM_CODE = string.Empty;
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

            if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
            {
                YEAR = ddlYear.SelectedItem.ToString().Trim();
            }
            else
            {
                YEAR = string.Empty;
            }
            if (ddllocation.SelectedValue.ToString() != null && ddllocation.SelectedValue.ToString() != string.Empty)
            {
                LOCATION = ddllocation.SelectedItem.ToString().Trim();
            }
            else
            {
                LOCATION = string.Empty;
            }
            if (ddlstore.SelectedValue.ToString() != null && ddlstore.SelectedValue.ToString() != string.Empty)
            {
                STORE = ddlstore.SelectedItem.ToString().Trim();
            }
            else
            {
                STORE = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.ItemMaster.MaterialTransaction(BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE, YEAR, LOCATION, STORE);
            return dt;
            
            //if (dt.Rows.Count > 0)
            //{
               
            //    //GridLedger.DataSource = dt;
            //    //GridLedger.DataBind();
            //    //lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            //    //GridLedger.Visible = true;
            //}
            //else
            //{
            //    //GridLedger.DataSource = dt;
            //    //GridLedger.DataBind();
            //    //lblTotalRecord.Text = "0";
            //    Common.CommonFuction.ShowMessage("Record Not Available By Selected Parameter.");
            //    //GridLedger.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        try
        {
            gridmaterialledger();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void TxtToDate_TextChanged1(object sender, EventArgs e)
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

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./MaterialIssue.aspx", false);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string StDate = string.Empty;
        string EnDate = string.Empty;
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string ITEM_TYPE = string.Empty;
        string ITEM_CATE = string.Empty;
        string ITEM_CODE = string.Empty;
        string TRAN_TYPE = string.Empty;
        string PRTY_CODE = string.Empty;
        string BRANCH1 = string.Empty;
        string YEAR1 = string.Empty;
        string ITEMTYPE = string.Empty;
        string ITEMCAT = string.Empty;
        string PARTY = string.Empty;
        string DEPARTMENT1 = string.Empty;
        string ITEM1 = string.Empty;
        string TRNTYPE = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
        try
        {

            DataTable myDataTable = new DataTable();

            myDataTable.Columns.Add("BRANCH_CODE", typeof(string));
            myDataTable.Columns.Add("DEPT_CODE", typeof(string));
            myDataTable.Columns.Add("ITEM_TYPE", typeof(string));
            myDataTable.Columns.Add("ITEM_CATE", typeof(string));
            myDataTable.Columns.Add("ITEM_CODE", typeof(string));
            myDataTable.Columns.Add("StDate", typeof(string));
            myDataTable.Columns.Add("EnDate", typeof(string));
            myDataTable.Columns.Add("PARTY_CODE", typeof(string));
            myDataTable.Columns.Add("TRAN_TYPE", typeof(string));

            myDataTable.Columns.Add("BRANCH1", typeof(string));
            myDataTable.Columns.Add("YEAR1", typeof(string));
            myDataTable.Columns.Add("ITEMTYPE", typeof(string));
            myDataTable.Columns.Add("ITEMCAT", typeof(string));
            myDataTable.Columns.Add("PARTY", typeof(string));
            myDataTable.Columns.Add("DEPARTMENT1", typeof(string));
            myDataTable.Columns.Add("ITEM1", typeof(string));
            myDataTable.Columns.Add("TRNTYPE", typeof(string));
            myDataTable.Columns.Add("YEAR", typeof(string));
            myDataTable.Columns.Add("LOCATION", typeof(string));
            myDataTable.Columns.Add("STORE", typeof(string));

            DataRow row;

            row = myDataTable.NewRow();

            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                row["BRANCH_CODE"] = ddlBranch.SelectedValue.ToString();
                row["BRANCH1"] = ddlBranch.SelectedItem.Text.ToString();
            }
            else
            {
                row["BRANCH_CODE"] = string.Empty;
                row["BRANCH1"] = string.Empty;
            }
            if (ddllocation.SelectedValue.ToString() != null && ddllocation.SelectedValue.ToString() != string.Empty)
            {
                row["LOCATION"] = ddllocation.SelectedValue.ToString();
                row["LOCATION"] = ddllocation.SelectedItem.Text.ToString();
            }
            else
            {
                row["LOCATION"] = string.Empty;
                row["LOCATION"] = string.Empty;
            }
            if (ddlstore.SelectedValue.ToString() != null && ddlstore.SelectedValue.ToString() != string.Empty)
            {
                row["STORE"] = ddlstore.SelectedValue.ToString();
                row["STORE"] = ddlstore.SelectedItem.Text.ToString();
            }
            else
            {
                row["STORE"] = string.Empty;
                row["STORE"] = string.Empty;
            }


            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            {
                row["DEPT_CODE"] = ddlDepartment.SelectedValue.ToString();
                row["DEPARTMENT1"] = ddlDepartment.SelectedItem.Text.ToString();
            }
            else
            {
                row["DEPT_CODE"] = string.Empty;
                row["DEPARTMENT1"] = string.Empty;
            }
            if (ddlItemCate.SelectedValue.ToString() != null && ddlItemCate.SelectedValue.ToString() != string.Empty)
            {
                row["ITEM_CATE"] = ddlItemCate.SelectedValue.ToString();
                row["ITEMCAT"] = ddlItemCate.SelectedItem.ToString();
            }
            else
            {
                row["ITEM_CATE"] = string.Empty;
                row["ITEMCAT"] = string.Empty;
            }

            if (ddlItemType.SelectedValue.ToString() != null && ddlItemType.SelectedValue.ToString() != string.Empty)
            {
                row["ITEM_TYPE"] = ddlItemType.SelectedValue.ToString();
                row["ITEMTYPE"] = ddlItemType.SelectedItem.ToString();
            }
            else
            {
                row["ITEM_TYPE"] = string.Empty;
                row["ITEMTYPE"] = string.Empty;
            }
            if (txtICODE.SelectedValue.ToString() != null && txtICODE.SelectedValue.ToString() != string.Empty)
            {
                row["ITEM_CODE"] = txtICODE.SelectedValue.Trim().ToString();
                row["ITEM1"] = txtICODE.SelectedValue.ToString();
            }
            else
            {
                row["ITEM_CODE"] = string.Empty;
                row["ITEM1"] = string.Empty;
            }

            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                row["StDate"] = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                row["EnDate"] = DateTime.Parse(TxtToDate.Text.Trim().ToString());
            }
            else
            {
                row["StDate"] = Sdate;
                row["EnDate"] = Edate;
            }
            if (ddlTrnType.SelectedValue.ToString() != null && ddlTrnType.SelectedValue.ToString() != string.Empty)
            {
                row["TRAN_TYPE"] = ddlTrnType.SelectedValue.ToString();
                row["TRNTYPE"] = ddlTrnType.SelectedItem.ToString();
            }
            else
            {
                row["TRAN_TYPE"] = string.Empty;
                row["TRNTYPE"] = string.Empty;
            }

            if (ddlPartycode.SelectedValue.ToString() != null && ddlPartycode.SelectedValue.ToString() != string.Empty)
            {
                row["PARTY_CODE"] = ddlPartycode.SelectedValue.ToString();
                row["PARTY"] = ddlPartycode.SelectedItem.Text.ToString();
            }
            else
            {
                row["PARTY_CODE"] = string.Empty;
                row["PARTY"] = string.Empty;
            }

            if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
            {

                row["YEAR1"] = ddlYear.SelectedItem.Text.ToString();
            }
            else
            {
                row["YEAR1"] = string.Empty;
            }
            myDataTable.Rows.Add(row);

            Session["MaterialLedger"] = myDataTable;
            string URL = "../Reports/MaterialIssueQueryReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            bindyear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            bindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
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

    private void bindddlPartycode()
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
        catch
        {
            throw;
        }

    }

    private void bindddltrntype()
    {

        try
        {

            ddlTrnType.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.getTransType();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTrnType.DataTextField = "TRN_DESC";
                ddlTrnType.DataValueField = "TRN_TYPE";
                ddlTrnType.DataSource = dt;
                ddlTrnType.DataBind();

            }
            ddlTrnType.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }
    }

    protected void GridLedger_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridmaterialledger();

            GridLedger.PageIndex = e.NewPageIndex;
            GridLedger.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtICODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtICODE.Items.Clear();
                txtICODE.DataSource = data;
                txtICODE.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_TYPE, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM  as Combined FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM as Combined   FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY ITEM_CODE";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE, ITEM_DESC, ITEM_TYPE FROM   TX_ITEM_MST WHERE      ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY ITEM_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    private void BindDropDown(DropDownList ddllocation)
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
    private void BindDepartment(DropDownList ddlstore)
    {
        try
        {
            ddlstore.Items.Clear();
            //DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            DataTable dtDepartment = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            {
                
                ddlstore.DataSource = dtDepartment;
                ddlstore.DataValueField = "MST_CODE";
                ddlstore.DataTextField = "MST_CODE";
                ddlstore.DataBind();
                ddlstore.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dtDepartment.Dispose();
                dtDepartment = null;
            }
           // ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Purchase_Register_Detail_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(gridmaterialledger1(), strFilename, "Purchase Register Detail");

    }

    private void ExporttoExcel(DataTable table, string name, string title)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;
        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(oUserLoginDetail.VC_COMPANYNAME);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:12.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(" " + title + " ");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD  align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("DATED:" + DateTime.Now.ToString() + "");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");


        HttpContext.Current.Response.Write("<TR>");

        foreach (DataColumn dtcol in table.Columns)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");

        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
   
}
