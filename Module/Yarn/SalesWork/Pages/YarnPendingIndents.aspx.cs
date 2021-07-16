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
using System.Drawing;   

public partial class Module_Yarn_SalesWork_Pages_YarnPendingIndents : System.Web.UI.Page
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
            BindDropDown(ddllocation);
            BindDepartment(ddlstore);
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
    private void Load_Pend_Indents()
    {
        if (ddlYear.SelectedValue.Trim() != "")
        {
            yr = " and a.year='" + ddlYear.SelectedValue.Trim() + "'";
        }

        idate1 = txtDate1.Text.ToString();
        idate2 = txtDate2.Text.ToString();
        if (ddlBranch.SelectedValue.Trim() != "")
        {
            branch =   ddlBranch.SelectedValue.Trim();
        }
        if (ddlDepartment.SelectedValue.Trim() != "")
        {
            department =  ddlDepartment.SelectedValue.Trim();
        }
        if (ddllocation.SelectedValue.Trim() != "")
        {
            location =  ddllocation.SelectedValue.Trim();
        }
        if (ddlstore.SelectedValue.Trim() != "")
        {
            store =  ddlstore.SelectedValue.Trim() ;
        }
        if (txtICODE.SelectedValue.Trim() != "")
        {
            itemcode =  txtICODE.SelectedValue.Trim() ;
        }
        status = ddlStatus.SelectedValue.Trim();
        try
        {
            DataTable Dtable = SaitexBL.Interface.Method.YRN_INT_MST.Load_Pendyarn_Indent_Details(branch, department, yr, idate1, idate2, itemcode, status, location, store);
            gvReportDisplayGrid.DataSource = Dtable;
            gvReportDisplayGrid.DataBind();
            lblTotalRecord.Text = Dtable.Rows.Count.ToString().Trim();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReportDisplayGrid.PageIndex = e.NewPageIndex;
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
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
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
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fill Department.\r\nSee error log for detail."));
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
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        if (ddlYear.SelectedValue.Trim() != "")
        {
            yr = "2015";
        }

        idate1 = txtDate1.Text.ToString();
        idate2 = txtDate2.Text.ToString();
        if (ddlBranch.SelectedValue.Trim() != "")
        {
            branch =  ddlBranch.SelectedValue.Trim() ;
        }
        if (ddlDepartment.SelectedValue.Trim() != "")
        {
            department =ddlDepartment.SelectedValue.Trim();
        }
        if (ddllocation.SelectedValue.Trim() != "")
        {
            location = ddllocation.SelectedValue.Trim();
        }
        if (ddlstore.SelectedValue.Trim() != "")
        {
            store = ddlstore.SelectedValue.Trim();
        }
        if (txtICODE.SelectedValue.Trim() != "")
        {
            itemcode =  txtICODE.SelectedValue.Trim();
        }
        status = ddlStatus.SelectedValue.Trim();


        url = "../Reports/YARN_PENDING_INDENTRPT.aspx?yr=" + yr + "&idate1=" + idate1 + "&idate2=" + idate2 + "&branch=" + branch + "&department=" + department + "&location=" + location + "&store=" + store + "&itemcode=" + itemcode + "&status=" + status;
       
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
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void txtDate1_TextChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void txtDate2_TextChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Pend_Indents();
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
    //protected DataTable GetItems(string text, int startOffset)
    //{
    //    try
    //    {
    //        string CommandText = "  SELECT   *  FROM   (  SELECT   FIBER_CODE,         FIBER_DESC,   FIBER_CAT,       FIBER_CODE || '@' || FIBER_DESC || '@' || UOM AS Combined     FROM   TX_FIBER_MASTER    WHERE      FIBER_CODE LIKE :SearchQuery     OR FIBER_DESC LIKE :SearchQuery      OR FIBER_CAT LIKE :SearchQuery   ORDER BY   FIBER_CODE) asd  WHERE   ROWNUM <= 15 ";
    //        string whereClause = string.Empty;

    //        if (startOffset != 0)
    //        {
    //            //whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM as Combined   FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
    //            whereClause += "AND FIBER_CODE NOT IN    (SELECT   FIBER_CODE   FROM   (  SELECT   FIBER_CODE,   FIBER_DESC,      FIBER_CODE    || '@'    || FIBER_DESC   || '@'  || UOM   AS Combined   FROM   TX_FIBER_MASTER  WHERE      FIBER_CODE LIKE :SearchQuery   OR FIBER_CAT LIKE :SearchQuery   OR FIBER_DESC LIKE :SearchQuery     ORDER BY   FIBER_CODE) asd    WHERE   ROWNUM <= " + startOffset + ")";
    //        }

    //        string SortExpression = " ORDER BY   FIBER_CODE";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE, YARN_CODE||'@'|| YARN_DESC||'@'||UOM  as Combined FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "   AND YARN_CODE NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC, YARN_CODE||'@'|| YARN_DESC||'@'||UOM as Combined   FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_CAT LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE, ITEM_DESC, ITEM_TYPE FROM   TX_ITEM_MST WHERE      ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY ITEM_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void txtICODE_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        Load_Pend_Indents();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Load_Pend_Indents();
    }
    private void BindDropDown(DropDownList ddl)
    {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.Items.Clear();
            ddl.DataSource = dt;
            ddl.DataTextField = "MST_DESC";
            ddl.DataValueField = "MST_DESC";
            ddl.DataBind();
        }
        else
        {
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

        }
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

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


    protected void imgbtnPrint_Clickexcel(object sender, ImageClickEventArgs e)
    {


        try
        {


            string strFilename = "Yarn_item_List_" + DateTime.Now.Date.ToString("dd/MM/yyyy") + ".xls";
            if (ddlYear.SelectedValue.Trim() != "")
            {
                yr = " and a.year='" + ddlYear.SelectedValue.Trim() + "'";
            }

            idate1 = txtDate1.Text.ToString();
            idate2 = txtDate2.Text.ToString();
            //if (ddlBranch.SelectedValue.Trim() != "")
            //{
            //    branch = " and c.branch_code='" + ddlBranch.SelectedValue.Trim() + "'";
            //}
            if (ddlDepartment.SelectedValue.Trim() != "")
            {
                department = " and c.dept_code='" + ddlDepartment.SelectedValue.Trim() + "'";
            }
            //if (ddllocation.SelectedValue.Trim() != "")
            //{
            //    location = " and c.location='" + ddllocation.SelectedValue.Trim() + "'";
            //}
            if (ddlstore.SelectedValue.Trim() != "")
            {
                store = " and c.store='" + ddlstore.SelectedValue.Trim() + "'";
            }
            if (txtICODE.SelectedValue.Trim() != "")
            {
                itemcode = " and a.YARN_CODE='" + txtICODE.SelectedValue.Trim() + "'";
            }
            status = ddlStatus.SelectedValue.Trim();

            var data = SaitexBL.Interface.Method.YRN_INT_MST.Load_Pendyarn_Indent_Details_Excel(branch, department, yr, idate1, idate2, itemcode, status, location, store);
            UploadDataTableToExcel(data, strFilename, "Yarn Indet Query(Lot Wise)");
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void UploadDataTableToExcel(DataTable table, string name, string title)
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

    protected void grdrpt_RowDataBound(object sender, GridViewRowEventArgs e)
    {




        // try
        //{
        //    for (int iLoop = 0; iLoop < gvReportDisplayGrid.Rows.Count; iLoop++)
        //    {
        //        var QTY_PENDING = (Label)e.Row.FindControl("lblQTY_PENDING");
        //        if (Convert.ToInt32( QTY_PENDING) < 1  )
        //        {
        //            //(Label)e.Row.FindControl("txtlblPENDING_DAYS") = string.Empty;
        //        }
        //    }
          
        //}
        //catch
        //{
        //    throw;
        //}

    }  
        


   
          
          
            

       

    


  
    
}
