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
using System.Data.OracleClient;
using errorLog;
using Common;
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Fiber_Pages_FiberPendingIndent : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
  

    string yr = string.Empty;
    string branch = string.Empty;
    string department = string.Empty;
    string itemcode = string.Empty;
    string status = string.Empty;
    string url = string.Empty;
    string idate1 = string.Empty;
    string idate2 = string.Empty;
    string location = string.Empty;
    string store = string.Empty;
    private string Start_Year = string.Empty;
    private string End_Year = string.Empty;
    private DateTime Sdate;
    private DateTime Edate;
  


    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
       
        if (!Page.IsPostBack)
        {
            getBranchName();
            getDepartment();
            fillYear();
            Load_Pend_Indents();
            //BindDropDown(ddllocation);
            //BindDepartment(ddlstore);
            Initial_Control();
        }

    }

    private void Initial_Control()
    {
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);

        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        bindyear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
        bindFromToDate();





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


                    txtDate1.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                    txtDate2.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                }
            }
        }
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
                getBranchName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                bindyear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void BindDepartment(DropDownList ddlstore)
    {
        try
        {
            ddlstore.Items.Clear();
            DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            //if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            //{

            ddlstore.DataSource = dtDepartment;
            ddlstore.DataValueField = "DEPT_NAME";
            ddlstore.DataTextField = "DEPT_NAME";
            ddlstore.DataBind();
            ddlstore.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dtDepartment.Dispose();
            dtDepartment = null;
            //}
            //ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate1.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(txtDate1.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {
                    Common.CommonFuction.ShowMessage("Date is fine .");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    txtDate1.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();

                }

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Load_Pend_Indents()

    {
        if (ddlYear.SelectedValue.Trim() != "")
        {
            //yr = " and a.year='" + ddlYear.SelectedValue.Trim() + "'";
            yr = ddlYear.SelectedValue.Trim();
        }

        idate1 = txtDate1.Text.ToString();
        idate2 = txtDate2.Text.ToString();
        if (ddlBranch.SelectedValue.Trim() != "")
        {
           // branch = " and c.branch_code='" + ddlBranch.SelectedValue.Trim() + "'";
            branch = ddlBranch.SelectedValue.Trim();
        }
        if (ddlDepartment.SelectedValue.Trim() != "")
        {
           // department = " and c.dept_code='" + ddlDepartment.SelectedValue.Trim() + "'";
            department = ddlDepartment.SelectedValue.Trim() ;
        }
        //if (ddllocation.SelectedValue.Trim() != "")
        //{
        //    location = " and c.location='" + ddllocation.SelectedValue.Trim() + "'";
        //}
        //if (ddlstore.SelectedValue.Trim() != "")
        //{
        //    store = " and c.store='" + ddlstore.SelectedValue.Trim() + "'";
        //}
        if (txtICODE.SelectedValue.Trim() != "")
        {
          //  itemcode = " and a.FIBER_CODE='" + txtICODE.SelectedValue.Trim() + "'";
            itemcode =  txtICODE.SelectedValue.Trim() ;
        }
        status = ddlStatus.SelectedValue.Trim();
        //try
        //{
        //    DataTable Dtable = SaitexBL.Interface.Method.TX_ITEM_IND_MST.Load_Pend_Indents(branch, department, yr);
        //    gvDeptPendIndents.DataSource = Dtable;
        //    gvDeptPendIndents.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    Common.CommonFuction.ShowMessage(ex.Message.ToString());
        //}
        try
        {
            DataTable Dtable = SaitexBL.Interface.Method.FIBER_IND_MST.Load_Pend_Indentpoy_Details(branch, department, yr, idate1, idate2, itemcode, status, "", "");
            gvDeptPendIndents.DataSource = Dtable;
            gvDeptPendIndents.DataBind();
            lblTotalRecord.Text = Dtable.Rows.Count.ToString().Trim();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
    protected void gvDeptPendIndents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDeptPendIndents.PageIndex = e.NewPageIndex;
        Load_Pend_Indents();
    }
    private void getBranchName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);

            ddlBranch.DataSource = dt;
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
            ////////////////////////// Bind Department Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void fillYear()
    {
        for (int i = -15; i < 15; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
        //ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        if (ddlYear.SelectedValue.Trim() != "")
        {
          //  yr = "and a.year='" + ddlYear.SelectedValue.Trim() + "'";
              yr = ddlYear.SelectedValue.Trim() ;
        }

        idate1 = txtDate1.Text.ToString();
        idate2 = txtDate2.Text.ToString();
        if (ddlBranch.SelectedValue.Trim() != "")
        {

         //   branch = "and c.branch_code='" + ddlBranch.SelectedValue.Trim() + "'";
            branch = ddlBranch.SelectedValue.Trim();
        }
        if (ddlDepartment.SelectedValue.Trim() != "")
        {
            department = ddlDepartment.SelectedValue.Trim();
        }

        if (txtICODE.SelectedValue.Trim() != "")
        {
            itemcode = txtICODE.SelectedValue.Trim();
        }
        status = ddlStatus.SelectedValue.Trim();

       


            url = "../Reports/FIBER_PENDING_INDENTRPT.aspx?yr=" + yr + "&idate1=" + idate1 + "&idate2=" + idate2 + "&branch=" + branch + "&department=" + department + "&itemcode=" + itemcode + "&status=" + status;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);

       

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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void gvDeptPendIndents_SelectedIndexChanged(object sender, EventArgs e)
    {

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


    protected int GetItemsCount(string text)
    {
        string CommandText = "SELECT   * FROM   (  SELECT   FIBER_CODE, FIBER_DESC, FIBER_CAT FROM   TX_FIBER_MASTER WHERE      FIBER_CODE LIKE :SearchQuery   OR FIBER_DESC LIKE :SearchQuery   OR FIBER_CAT LIKE :SearchQuery  ORDER BY   FIBER_CODE) asd";
        string WhereClause = " ";
        string SortExpression = " ORDER BY FIBER_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }


    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = "  SELECT   *  FROM   (  SELECT   FIBER_CODE,         FIBER_DESC,   FIBER_CAT,       FIBER_CODE || '@' || FIBER_DESC || '@' || UOM AS Combined     FROM   TX_FIBER_MASTER    WHERE      FIBER_CODE LIKE :SearchQuery     OR FIBER_DESC LIKE :SearchQuery      OR FIBER_CAT LIKE :SearchQuery   ORDER BY   FIBER_CODE) asd  WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                //whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM as Combined   FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
                whereClause += "AND FIBER_CODE NOT IN    (SELECT   FIBER_CODE   FROM   (  SELECT   FIBER_CODE,   FIBER_DESC,      FIBER_CODE    || '@'    || FIBER_DESC   || '@'  || UOM   AS Combined   FROM   TX_FIBER_MASTER  WHERE      FIBER_CODE LIKE :SearchQuery   OR FIBER_CAT LIKE :SearchQuery   OR FIBER_DESC LIKE :SearchQuery     ORDER BY   FIBER_CODE) asd    WHERE   ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY   FIBER_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV("","","",CommandText, whereClause, SortExpression, "", SearchQuery);

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void txtICODE_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }

    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDeptPendIndents.PageIndex = e.NewPageIndex;
        Load_Pend_Indents();
    }
    protected void imgbtnPrint_Clickexcel(object sender, ImageClickEventArgs e)
    {
        try
        {

            string strFilename = "Poy_indent_List_" + DateTime.Now.Date.ToString("dd/MM/yyyy") + ".xls";


            if (ddlYear.SelectedValue.Trim() != "")
            {
                //  yr = "and a.year='" + ddlYear.SelectedValue.Trim() + "'";
                yr = ddlYear.SelectedValue.Trim();
            }

            idate1 = txtDate1.Text.ToString();
            idate2 = txtDate2.Text.ToString();
            if (ddlBranch.SelectedValue.Trim() != "")
            {

                //   branch = "and c.branch_code='" + ddlBranch.SelectedValue.Trim() + "'";
                branch = ddlBranch.SelectedValue.Trim();
            }
            if (ddlDepartment.SelectedValue.Trim() != "")
            {
                department = ddlDepartment.SelectedValue.Trim();
            }

            if (txtICODE.SelectedValue.Trim() != "")
            {
                itemcode = txtICODE.SelectedValue.Trim();
            }
            status = ddlStatus.SelectedValue.Trim();

            var data = SaitexBL.Interface.Method.FIBER_IND_MST.Load_Pend_Indentpoy_Details(branch, department, yr, idate1, idate2, itemcode, status, location, store);
            UploadDataTableToExcel(data, strFilename);
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void UploadDataTableToExcel(DataTable dtEmp, string filename)
    {


        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;
        foreach (DataColumn dtcol in dtEmp.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dtEmp.Rows)
        {
            tab = "";
            for (int j = 0; j < dtEmp.Columns.Count; j++)
            {
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
}
