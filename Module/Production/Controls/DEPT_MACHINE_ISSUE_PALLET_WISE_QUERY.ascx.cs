using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OracleClient;
using errorLog;
using Common;
using Obout.ComboBox;

public partial class Module_Production_Controls_DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY : System.Web.UI.UserControl
{
    string ORDER_NO = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    //DateTime Sdate;
    //DateTime Edate;

    DateTime Sdate = System.DateTime.Now;
    DateTime Edate = System.DateTime.Now;
    DateTime StDate;
    DateTime EnDate;

    DateTime FmDate = System.DateTime.Now;
    DateTime Tdate = System.DateTime.Now;
    //string FromDate = string.Empty;
    //string ToDate  = string.Empty;
    DateTime FromDate;
    DateTime ToDate;
    string COMP_CODE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                //getBrachName();
                //getDepartment();
                //getBatchNo();
                //GetLotNumber1();
                //getMergeNo1();
                //getOrderNo();

                //Sdate = oUserLoginDetail.DT_STARTDATE;
                //Edate = Common.CommonFuction.GetYearEndDate(Sdate);
                //TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                //TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();


                FmDate = oUserLoginDetail.DT_STARTDATE;
                Tdate = Common.CommonFuction.GetYearEndDate(FmDate);
                txtMachIssFrom.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                txtMachIssTo.Text = Common.CommonFuction.GetYearEndDate(FmDate).ToShortDateString();

                TxtFromDate.Text =DateTime.Now.ToShortDateString();
                TxtToDate.Text = DateTime.Now.ToShortDateString();
              
                Initialize();
                bindGvItemMaster();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }
    private void Initialize()
    {
        try
        {
            txtDeptIssNo.Text = string.Empty;

            //txtYCODE.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }
    //private void getOrderNo()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetOrderNo();
    //        ddlOrderNo.DataSource = dt;
    //        ddlOrderNo.DataValueField = "ORDER_NO";
    //        ddlOrderNo.DataTextField = "ORDER_NO";
    //        ddlOrderNo.DataBind();
    //        ddlOrderNo.Items.Insert(0, new ListItem("------------Select------------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
    //    }
    //}
    //private void GetLotNumber1()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetLotNumber();
    //        ddlLotNo.DataSource = dt;
    //        ddlLotNo.DataValueField = "LOT_NUMBER";
    //        ddlLotNo.DataTextField = "LOT_NUMBER";
    //        ddlLotNo.DataBind();
    //        ddlLotNo.Items.Insert(0, new ListItem("------------Select------------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
    //    }
    //}
    //private void getBatchNo()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.YRN_PROD_MST.selectBatchNo();
    //        ddlBatchNo.DataSource = dt;
    //        ddlBatchNo.DataValueField = "PROS_ID_NO";
    //        ddlBatchNo.DataTextField = "PROS_ID_NO";
    //        ddlBatchNo.DataBind();
    //        ddlBatchNo.Items.Insert(0, new ListItem("------------Select------------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
    //    }
    //}
    //private void getMergeNo1()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.YRN_PROD_MST.selectMergeNo();
    //        ddlMergeNo.DataSource = dt;
    //        ddlMergeNo.DataValueField = "MERGE_NO";
    //        ddlMergeNo.DataTextField = "MERGE_NO";
    //        ddlMergeNo.DataBind();
    //        ddlMergeNo.Items.Insert(0, new ListItem("------------Select------------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
    //    }
    //}
    //private void getDepartment()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
    //        ddlDepartment.DataSource = dt;
    //        ddlDepartment.DataValueField = "DEPT_CODE";
    //        ddlDepartment.DataTextField = "DEPT_NAME";
    //        ddlDepartment.DataBind();
    //        ddlDepartment.Items.Insert(0, new ListItem("------------Select------------",""));
    //        dt.Dispose();
    //        dt = null;
    //    }      
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
    //    }
    //}
    //private void getBrachName( )
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
    //        ddlBranch.DataSource = dt;
    //        ddlBranch.DataValueField = "BRANCH_NAME";
    //        ddlBranch.DataTextField = "BRANCH_NAME";
    //        ddlBranch.DataBind();
    //        ddlBranch.Items.Insert(0, new ListItem("------------Select------------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //}
    private void bindGvItemMaster()
    {
        string PALLET_NO = string.Empty;
        string DEPT_ISS_NO = string.Empty;
        string MACHINE_ISS_NO = string.Empty;
        //string FromDate = string.Empty;
        //string ToDate = string.Empty;
        DateTime FromDate = DateTime.Now;
        DateTime ToDate = DateTime.Now;
        string LOT_NO = string.Empty;
        try
        {
            if (txtPalletNo.Text.Trim() != null && txtPalletNo.Text.Trim() != string.Empty)
            {
                PALLET_NO = txtPalletNo.Text.Trim();
            }
            else
            {
                PALLET_NO = string.Empty;
            }
            if (txtDeptIssNo.Text.Trim() != null && txtDeptIssNo.Text.Trim() != string.Empty)
            {
                DEPT_ISS_NO = txtDeptIssNo.Text.Trim();
            }
            else
            {
                DEPT_ISS_NO = string.Empty;
            }
            if (txtMachineNo.Text.Trim() != null && txtMachineNo.Text.Trim() != string.Empty)
            {
                MACHINE_ISS_NO = txtMachineNo.Text.Trim();
            }
            else
            {
                MACHINE_ISS_NO = string.Empty;
            }

            if (TxtFromDate.Text.Trim() != null || TxtToDate.Text.Trim() != null)
            {
                //FromDate = TxtFromDate.Text.Trim().ToString();
                //ToDate = TxtToDate.Text.Trim().ToString();
                //DateTime FromDate = DateTime.Now;
                //DateTime ToDate = DateTime.Now;
                DateTime.TryParse(TxtFromDate.Text, out FromDate);
                DateTime.TryParse(TxtToDate.Text, out ToDate);
            }
            if (txtMachIssFrom.Text.Trim() != null || txtMachIssTo.Text.Trim() != null)
            {
                DateTime.TryParse(txtMachIssFrom.Text, out StDate);
                DateTime.TryParse(txtMachIssTo.Text, out EnDate);
            }

            if (txtYCODE.SelectedValue.ToString() != null && txtYCODE.SelectedValue.ToString() != string.Empty)
            {
                LOT_NO = txtYCODE.SelectedValue.ToString();
            }
            else
            {
                LOT_NO = txtYCODE.SelectedValue.ToString();
            }
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY(PALLET_NO, DEPT_ISS_NO, MACHINE_ISS_NO, FromDate, ToDate, LOT_NO, StDate, EnDate);
            if (dt.Rows.Count > 0)
            {
                Grid1.DataSource = dt;
                Grid1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                Grid1.Visible = true;
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
                lblTotalRecord.Text = "0";
                Grid1.Visible = false;
                Common.CommonFuction.ShowMessage("Record Not Available By This Parameter.");
            }
            CalculateAllData();
            dt.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void CalculateAllData()
    {
        if (Grid1.Rows.Count > 0)
        {

            double TotalMachIssQty = 0;
            double TotallblTotalIssQty = 0;
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                double MachIssQty = 0;
                double IssQty = 0;
                Label lblMachIssQty = Grid1.Rows[i].FindControl("lblMachIssQty") as Label;
                Label lblIssQty = Grid1.Rows[i].FindControl("lblIssQty") as Label;
                double.TryParse(lblMachIssQty.Text, out MachIssQty);
                double.TryParse(lblIssQty.Text, out IssQty);
                TotalMachIssQty = TotalMachIssQty + MachIssQty;
                TotallblTotalIssQty = TotallblTotalIssQty + IssQty;
            }

            ((Label)Grid1.FooterRow.FindControl("lblTotalMachIssQty")).Text =Math.Round(TotalMachIssQty,3).ToString();
            ((Label)Grid1.FooterRow.FindControl("lblTotalIssQty")).Text = Math.Round(TotallblTotalIssQty, 3).ToString();

        }
    }
    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        bindGvItemMaster();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            bindGvItemMaster();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY.ASPX", false);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        string PALLET_NO = string.Empty;
        string DEPT_ISS_NO = string.Empty;
        string MACHINE_ISS_NO = string.Empty;
        //string FromDate = string.Empty;
        //string ToDate = string.Empty;

        string LOT_NO = string.Empty;
        try
        {
            string strFilename = "DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY_" + DateTime.Now.ToString() + ".xls";
            if (txtPalletNo.Text.Trim() != null && txtPalletNo.Text.Trim() != string.Empty)
            {
                PALLET_NO = txtPalletNo.Text.Trim();
            }
            else
            {
                PALLET_NO = string.Empty;
            }
            if (txtDeptIssNo.Text.Trim() != null && txtDeptIssNo.Text.Trim() != string.Empty)
            {
                DEPT_ISS_NO = txtDeptIssNo.Text.Trim();
            }
            else
            {
                DEPT_ISS_NO = string.Empty;
            }
            if (txtMachineNo.Text.Trim() != null && txtMachineNo.Text.Trim() != string.Empty)
            {
                MACHINE_ISS_NO = txtMachineNo.Text.Trim();
            }
            else
            {
                MACHINE_ISS_NO = string.Empty;
            }

            if (TxtFromDate.Text.Trim() != null || TxtToDate.Text.Trim() != null)
            {
                //FromDate = TxtFromDate.Text.Trim().ToString();
                //ToDate = TxtToDate.Text.Trim().ToString();
                //DateTime FromDate = DateTime.Now;
                //DateTime ToDate = DateTime.Now;
                DateTime.TryParse(TxtFromDate.Text, out FromDate);
                DateTime.TryParse(TxtToDate.Text, out ToDate);
            }
            if (txtMachIssFrom.Text.Trim() != null || txtMachIssTo.Text.Trim() != null)
            {
                DateTime.TryParse(txtMachIssFrom.Text, out StDate);
                DateTime.TryParse(txtMachIssTo.Text, out EnDate);
            }

            if (txtYCODE.SelectedValue.ToString() != null && txtYCODE.SelectedValue.ToString() != string.Empty)
            {
                LOT_NO = txtYCODE.SelectedValue.ToString();
            }
            else
            {
                LOT_NO = txtYCODE.SelectedValue.ToString();
            }
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.DEPT_MACHINE_ISSUE_PALLET_WISE_QUERY(PALLET_NO, DEPT_ISS_NO, MACHINE_ISS_NO, FromDate, ToDate, LOT_NO, StDate, EnDate);
            if (dt.Rows.Count > 0)
                ExportDataTableToExcel(dt, strFilename, "DEPT MACHINE ISSUE PALLET WISE QUERY");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Binding stock.\r\nSee error log for detail."));
        }
    }


    protected void ExportDataTableToExcel(DataTable table, string name, string title)
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
        //  HttpContext.Current.ApplicationInstance.CompleteRequest();
    }



    protected void txtYCODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtYCODE.Items.Clear();
                txtYCODE.DataSource = data;
                txtYCODE.DataTextField = "FIBER_CODE";
                txtYCODE.DataValueField = "FIBER_CODE";
                txtYCODE.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fabric loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   *  FROM   (  SELECT   MST_CODE AS FIBER_CODE,                     MST_DESC, FIBER_DESC,                     MST_CODE || '@' || MST_DESC||'@'||FIBER_DESC                         AS Combined              FROM   TX_MASTER_TRN T, TX_FIBER_MASTER U             WHERE  T.OTHER_INFO=U.FIBER_CODE              AND (UPPER(MST_CODE) LIKE :SearchQuery    OR UPPER(FIBER_DESC) LIKE :SearchQuery )     ORDER BY   MST_CODE) asd WHERE   ROWNUM <= 15 ";

            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND FIBER_CODE NOT IN (SELECT FIBER_CODE FROM (SELECT MST_CODE AS FIBER_CODE, MST_DESC, FIBER_DESC, MST_CODE ||'@'|| MST_DESC||'@'||FIBER_DESC  as Combined   FROM TX_MASTER_TRN T, TX_FIBER_MASTER U WHERE (UPPER(MST_CODE) LIKE :SearchQuery OR UPPER(FIBER_DESC) LIKE :SearchQuery) AND T.OTHER_INFO=U.FIBER_CODE ORDER BY MST_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY FIBER_CODE";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery);
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   MST_CODE AS FIBER_CODE, MST_DESC,FIBER_DESC              FROM   TX_MASTER_TRN T, TX_FIBER_MASTER U             WHERE  T.OTHER_INFO=U.FIBER_CODE              AND  (UPPER(MST_CODE) LIKE :SearchQuery   OR  UPPER(FIBER_DESC) LIKE :SearchQuery )       ORDER BY   MST_CODE) asd ";
        string whereClause = string.Empty;
        string SortExpression = " ORDER BY FIBER_CODE ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery);
        return data.Rows.Count;

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string msg = "";
            string QueryString = "";
            QueryString = "../Report/DEPT_MACHINE_ISSUE_PALLET_WISE_REPORT.aspx";
            bool flag = false;
            string BussType = string.Empty;
            if (txtDeptIssNo.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "DEPT_ISS_NO=" + txtDeptIssNo.Text;
                flag = true;
            }
            if (txtMachineNo.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "MACHINE_NO=" + txtMachineNo.Text;
                flag = true;
            }
            if (txtPalletNo.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "PALLET_NO=" + txtPalletNo.Text.Trim();
                flag = true;
            }
            if (txtYCODE.SelectedValue != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "YARN_CODE=" + txtYCODE.SelectedValue;
                flag = true;
            }
            if (txtMachIssTo.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "MACHINE_ISS_TO=" + txtMachIssTo.Text;
                flag = true;
            }
            if (txtMachIssFrom.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "MACHINE_ISS_FROM=" + txtMachIssFrom.Text;
                flag = true;
            }
            if (TxtFromDate.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "FROM_DATE=" + TxtFromDate.Text;
                flag = true;
            }
            if (TxtToDate.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "TO_DATE=" + TxtToDate.Text;
                flag = true;
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + QueryString + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=1000,height=600,left=200,top=300');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adjusting Receiving.\r\nSee error log for detail."));
        }
    }
}
