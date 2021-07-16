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

public partial class Module_Production_Controls_Daily_Dyeing_Query : System.Web.UI.UserControl
{
    int Production_Slip_No = 0;
    string YARN_CODE = string.Empty;
    string BRANCH_CODE = string.Empty;
    string PI_NO = string.Empty;
    private static string Department = string.Empty;
    private static string GREY_LOT_NO = string.Empty;
    private static string Location = string.Empty;
    private static string Store = string.Empty;
    DateTime ProductionDate;
    DateTime ProductionToDate;
    string url = string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                //lblMode.Text = "You are in Print Mode";
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Load.\r\nSee error log for detail."));
        }
    }
    private void InitialisePage()
    {
        try
        {
            BindDepartment(ddlStore);
           // BindDropDown(ddlLocation);
            bindddlDeptCode();
            getBrachName();
            txtMRNDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtMRNToDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            GridYarnLoosePackingQuery();

        }
        catch
        {
            throw;
        }
    }

    private void bindddlDeptCode()
    {
        try
        {
            ddldept.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldept.DataTextField = "DEPT_NAME";
                ddldept.DataValueField = "DEPT_CODE";
                ddldept.DataSource = dt;
                ddldept.DataBind();

            }
            ddldept.Items.Insert(0, new ListItem("------All------", ""));
            //ddldept.SelectedIndex = ddldept.Items.IndexOf(ddldept.Items.FindByValue(oUserLoginDetail.VC_DEPARTMENTCODE));
        }
        catch
        {
            throw;
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
                txtItemCode.Items.Clear();
                txtItemCode.DataSource = data;
                txtItemCode.DataBind();
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
            string CommandText = "select * from (select * from (SELECT 'All' YARN_CODE,'All' YARN_DESC from dual union  SELECT   Y.YARN_CODE, Y.YARN_DESC  FROM    YRN_MST y WHERE        (UPPER(Y.YARN_CODE) LIKE (:SearchQuery)         OR UPPER(Y.YARN_DESC) LIKE (:SearchQuery)) ) ORDER BY YARN_CODE) where ROWNUM <= 15";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN (select * from (select * from (SELECT 'All' YARN_CODE from dual union  SELECT   YARN_CODE   FROM    YRN_MST y WHERE        (UPPER(Y.YARN_CODE) LIKE (:SearchQuery)         OR  UPPER(Y.YARN_DESC) LIKE (:SearchQuery))) ORDER BY YARN_CODE) where ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
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
        string CommandText = " select * from (SELECT 'All' YARN_CODE,'All' YARN_DESC from dual union  SELECT   Y.YARN_CODE, Y.YARN_DESC  FROM    YRN_MST y WHERE        (UPPER(Y.YARN_CODE) LIKE (:SearchQuery)         OR  UPPER(Y.YARN_DESC) LIKE (:SearchQuery)))";
        string WhereClause = " ";
        string SortExpression = " ORDER BY YARN_CODE ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    //private void BindDropDown(DropDownList ddl)
    //{
    //    DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        ddl.Items.Clear();
    //        ddl.DataSource = dt;
    //        ddl.DataTextField = "MST_DESC";
    //        ddl.DataValueField = "MST_DESC";
    //        ddl.DataBind();
    //    }
    //    else
    //    {
    //        ddl.DataSource = null;
    //        ddl.DataBind();
    //        ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

    //    }
    //    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

    //}
    private void BindDepartment(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PACK_PROCESS", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("------All------", ""));
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
    private void getBrachName()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            ddlBranch.DataSource = dt;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("---------Select--------", ""));
            dt.Dispose();
            dt = null;
            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    private DataTable GridYarnLoosePackingQuery()
    {
        try
        {
            if (txtTRNNUMBer.Text.ToString() != null && txtTRNNUMBer.Text.ToString() != string.Empty)
            {
                Production_Slip_No = int.Parse(txtTRNNUMBer.Text.Trim());
            }
            else
            {
                Production_Slip_No = 0;
            }
            if (txtItemCode.SelectedValue.ToString() != null && txtItemCode.SelectedValue.ToString() != string.Empty && txtItemCode.SelectedValue.ToString() != "All")
            {
                YARN_CODE = txtItemCode.SelectedValue.ToString();
            }
            else
            {
                YARN_CODE = string.Empty;
            }
            if (ddldept.SelectedValue.ToString() != null && ddldept.SelectedValue.ToString() != string.Empty)
            {
                Department = ddldept.SelectedValue.ToString();
            }
            else
            {
                Department = string.Empty;
            }
            if (ddllotno.SelectedValue.ToString() != null && ddllotno.SelectedValue.ToString() != string.Empty && ddllotno.SelectedValue.ToString() != "All")
            {
                GREY_LOT_NO = ddllotno.SelectedValue.ToString();
            }
            else
            {
                GREY_LOT_NO = string.Empty;
            }
            if (ddlLocation.SelectedValue.ToString() != null && ddlLocation.SelectedValue.ToString() != string.Empty)
            {
                Location = ddlLocation.SelectedValue.ToString();
            }
            else
            {
                Location = string.Empty;
            }
            if (ddlStore.SelectedValue.ToString() != null && ddlStore.SelectedValue.ToString() != string.Empty)
            {
                Store = ddlStore.SelectedValue.ToString();
            }
            else
            {
                Store = string.Empty;
            }
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlMergeNo.SelectedValue.ToString() != null && ddlMergeNo.SelectedValue.ToString() != string.Empty && ddlMergeNo.SelectedValue.ToString() != "All")
            {
                PI_NO = ddlMergeNo.SelectedValue.ToString();
            }
            else
            {
                PI_NO = string.Empty;
            }
            if (txtMRNDate.Text.Trim().ToString() != null && txtMRNDate.Text.Trim().ToString() != string.Empty)
            {
                ProductionDate = Convert.ToDateTime(txtMRNDate.Text.Trim());
            }
            else
            {
                ProductionDate = System.DateTime.MinValue;
            }

            if (txtMRNToDate.Text.Trim().ToString() != null && txtMRNToDate.Text.Trim().ToString() != string.Empty)
            {
                ProductionToDate = Convert.ToDateTime(txtMRNToDate.Text.Trim());
            }
            else
            {
                ProductionToDate = System.DateTime.MinValue;
            }
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDyeingProductionQueryDaily(Production_Slip_No, YARN_CODE, Department, GREY_LOT_NO, Location, Store, BRANCH_CODE, PI_NO, ProductionDate, ProductionToDate);
            if (dt != null & dt.Rows.Count > 0)
            {
                GridYarnLoosePacking.DataSource = dt;
                GridYarnLoosePacking.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                GridYarnLoosePacking.DataSource = null;
                GridYarnLoosePacking.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
            }
            CalculateAllData();
            return dt;
        }
        catch
        {
            throw;
        }

    }
    protected void GridYarnLoosePacking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void GridYarnLoosePacking_SelectedIndexChanged(Object sender, EventArgs e)
    {
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./Loose_Packed_YarnRecQuery.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            GridYarnLoosePackingQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void CalculateAllData()
    {
        if (GridYarnLoosePacking.Rows.Count > 0)
        {
            double TotalNetWt = 0;
            double TotalGrossWt = 0;
            double TotalTareWt = 0;
            double TotalCops = 0;
            double TotalCortonNo = 0;
            //double TotalIssueWt = 0;
            //double TotalIssueCops = 0;
            for (int i = 0; i < GridYarnLoosePacking.Rows.Count; i++)
            {
                double NetWt = 0;
                double GrossWt = 0;
                double TareWt = 0;
                double Cops = 0;
                //double IssueWt = 0;
                // double IssueCops = 0;     
                Label lblNetWt = GridYarnLoosePacking.Rows[i].FindControl("lblNetWt") as Label;
                Label lblGrossWt = GridYarnLoosePacking.Rows[i].FindControl("lblGrossWt") as Label;
                Label lblTareWt = GridYarnLoosePacking.Rows[i].FindControl("lblTareWt") as Label;
                Label lblCops = GridYarnLoosePacking.Rows[i].FindControl("lblCops") as Label;
                //Label lblIssueWt = GridYarnLoosePacking.Rows[i].FindControl("lblIssueWt") as Label;
                //Label lblIssueCops = GridYarnLoosePacking.Rows[i].FindControl("lblIssueCops") as Label;

                double.TryParse(lblNetWt.Text, out NetWt);
                double.TryParse(lblGrossWt.Text, out GrossWt);
                double.TryParse(lblTareWt.Text, out TareWt);
                double.TryParse(lblCops.Text, out Cops);
                //double.TryParse(lblIssueWt.Text, out IssueWt);
                // double.TryParse(lblIssueCops.Text, out IssueCops);
                TotalNetWt = TotalNetWt + NetWt;
                TotalGrossWt = TotalGrossWt + GrossWt;
                TotalTareWt = TotalTareWt + TareWt;
                TotalCops = TotalCops + Cops;
                TotalCortonNo = TotalCortonNo + 1;
                //TotalIssueWt = TotalIssueWt + IssueWt;
                //TotalIssueCops = TotalIssueCops + IssueCops;   

            }

            ((Label)GridYarnLoosePacking.FooterRow.FindControl("lblTotalNetWt")).Text = Math.Round(TotalNetWt, 3).ToString();
            ((Label)GridYarnLoosePacking.FooterRow.FindControl("lblTotalGrossWt")).Text = Math.Round(TotalGrossWt, 3).ToString();
            ((Label)GridYarnLoosePacking.FooterRow.FindControl("lblTotalTareWt")).Text = Math.Round(TotalTareWt, 3).ToString();
            ((Label)GridYarnLoosePacking.FooterRow.FindControl("lblTotalCops")).Text = TotalCops.ToString();
            ((Label)GridYarnLoosePacking.FooterRow.FindControl("lblTotalCortonNo")).Text = TotalCortonNo.ToString();
            //((Label)GridYarnLoosePacking.FooterRow.FindControl("lblTotalIssueWt")).Text = Math.Round(TotalIssueWt,3).ToString();
            //((Label)GridYarnLoosePacking.FooterRow.FindControl("lblTotalIssueCops")).Text = TotalIssueCops.ToString();

        }
    }

    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {

        string strFilename = "Daily_Dyeing_Details_List_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(GridYarnLoosePackingQuery(), strFilename, "Daily Dyeing Details List");


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

    protected void ddllotno_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLotDetails(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddllotno.Items.Clear();
                ddllotno.DataSource = data;
                ddllotno.DataTextField = "GREY_LOT_NO";
                ddllotno.DataValueField = "GREY_LOT_NO";
                ddllotno.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetLotDetailsCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fabric loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetLotDetails(string text, int startOffset)
    {
        try
        {

            string CommandText = " select * from (select * from (SELECT 'All' ASS_YARN_CODE,'All' ASS_YARN_DESC,'All' LOT_NO from dual union   SELECT   A.ASS_YARN_CODE , A.ASS_YARN_DESC , L.LOT_NO   FROM   YRN_MST M, YRN_LOT_MAKING L,YRN_ASSOCATED_MST A WHERE   M.YARN_CODE=A.YARN_CODE AND L.FINISHED_DENIER = A.ASS_YARN_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (A.ASS_YARN_CODE) LIKE :SearchQuery              OR UPPER (A.ASS_YARN_DESC) LIKE :SearchQuery OR UPPER (L.LOT_NO) LIKE :SearchQuery))   ORDER BY LOT_NO) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND (ASS_YARN_CODE,LOT_NO) NOT IN ( select * from (select * from (SELECT 'All' ASS_YARN_CODE,'All' LOT_NO from dual union  SELECT   A.ASS_YARN_CODE , L.LOT_NO  FROM   YRN_MST M, YRN_LOT_MAKING L,YRN_ASSOCATED_MST A WHERE   M.YARN_CODE=A.YARN_CODE AND L.FINISHED_DENIER = A.ASS_YARN_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (A.ASS_YARN_CODE) LIKE :SearchQuery              OR UPPER (A.ASS_YARN_DESC) LIKE :SearchQuery OR UPPER (L.LOT_NO) LIKE :SearchQuery)) ORDER BY LOT_NO  )  WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = "";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery);
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetLotDetailsCount(string text)
    {
        string CommandText = "  select * from (SELECT 'All' ASS_YARN_CODE,'All' ASS_YARN_DESC,'All' LOT_NO from dual union  SELECT   A.ASS_YARN_CODE YARN_CODE, A.ASS_YARN_DESC YARN_DESC, L.LOT_NO  FROM   YRN_MST M, YRN_LOT_MAKING L,YRN_ASSOCATED_MST A WHERE   M.YARN_CODE=A.YARN_CODE AND L.FINISHED_DENIER = A.ASS_YARN_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (A.ASS_YARN_CODE) LIKE :SearchQuery              OR UPPER (A.ASS_YARN_DESC) LIKE :SearchQuery OR UPPER (L.LOT_NO) LIKE :SearchQuery))   ";
        string whereClause = "";
        string SortExpression = "  ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery);
        return data.Rows.Count;

    }


    protected void ddlMergeNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMergeDetails(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlMergeNo.Items.Clear();
                ddlMergeNo.DataSource = data;
                ddlMergeNo.DataTextField = "GREY_LOT_NO";
                ddlMergeNo.DataValueField = "GREY_LOT_NO";
                ddlMergeNo.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetMergeDetailsCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fabric loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetMergeDetails(string text, int startOffset)
    {
        try
        {

            string CommandText = "   SELECT * FROM ( SELECT * FROM (SELECT 'All' YARN_CODE,'All' YARN_DESC,'All' LOT_NO from dual UNION  SELECT   M.FIBER_CODE YARN_CODE, M.FIBER_DESC YARN_DESC, L.MERGE_NO LOT_NO  FROM   TX_FIBER_MASTER M, YRN_LOT_MAKING L WHERE   L.POY = M.FIBER_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (M.FIBER_CODE) LIKE :SearchQuery              OR UPPER (M.FIBER_DESC) LIKE :SearchQuery OR UPPER (L.MERGE_NO) LIKE :SearchQuery)  UNION           SELECT   M.YARN_CODE, M.YARN_DESC, L.MERGE_NO LOT_NO  FROM   YRN_MST M, YRN_LOT_MAKING L WHERE   L.POY = M.YARN_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (M.YARN_CODE) LIKE :SearchQuery              OR UPPER (M.YARN_DESC) LIKE :SearchQuery OR UPPER (L.MERGE_NO) LIKE :SearchQuery)) ORDER BY   LOT_NO )WHERE ROWNUM <= 15    ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND (YARN_CODE,LOT_NO) NOT IN ( select * from(select * from (SELECT 'All' YARN_CODE,'All' LOT_NO from dual UNION SELECT YARN_CODE,LOT_NO FROM (SELECT   M.FIBER_CODE YARN_CODE, M.FIBER_DESC YARN_DESC, L.MERGE_NO LOT_NO  FROM   TX_FIBER_MASTER M, YRN_LOT_MAKING L WHERE   L.POY = M.FIBER_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (M.FIBER_CODE) LIKE :SearchQuery              OR UPPER (M.FIBER_DESC) LIKE :SearchQuery OR UPPER (L.MERGE_NO) LIKE :SearchQuery)  UNION           SELECT   M.YARN_CODE, M.YARN_DESC, L.MERGE_NO LOT_NO  FROM   YRN_MST M, YRN_LOT_MAKING L WHERE   L.POY = M.YARN_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (M.YARN_CODE) LIKE :SearchQuery              OR UPPER (M.YARN_DESC) LIKE :SearchQuery OR UPPER (L.MERGE_NO) LIKE :SearchQuery))) ORDER BY LOT_NO) WHERE ROWNUM <= " + startOffset + " ) ";
            }

            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery);
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetMergeDetailsCount(string text)
    {
        string CommandText = "  SELECT * FROM (SELECT 'All' YARN_CODE,'All' YARN_DESC,'All' LOT_NO from DUAL UNION  SELECT   M.FIBER_CODE YARN_CODE, M.FIBER_DESC YARN_DESC, L.MERGE_NO LOT_NO  FROM   TX_FIBER_MASTER M, YRN_LOT_MAKING L WHERE   L.POY = M.FIBER_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (M.FIBER_CODE) LIKE :SearchQuery              OR UPPER (M.FIBER_DESC) LIKE :SearchQuery)  UNION           SELECT   M.YARN_CODE, M.YARN_DESC, L.MERGE_NO LOT_NO  FROM   YRN_MST M, YRN_LOT_MAKING L WHERE   L.POY = M.YARN_CODE AND NVL (L.CONF_FLAG, 0) = 1         AND (UPPER (M.YARN_CODE) LIKE :SearchQuery              OR UPPER (M.YARN_DESC) LIKE :SearchQuery))   ";
        string whereClause = "";
        string SortExpression = "  ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery);
        return data.Rows.Count;

    }
}
