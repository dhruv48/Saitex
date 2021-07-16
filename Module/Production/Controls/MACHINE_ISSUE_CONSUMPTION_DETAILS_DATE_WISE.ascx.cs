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

public partial class Module_Production_Controls_MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISE : System.Web.UI.UserControl
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
                //Sdate = oUserLoginDetail.DT_STARTDATE;
                //Edate = Common.CommonFuction.GetYearEndDate(Sdate);
                //TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                //TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();

                TxtFromDate.Text = DateTime.Now.ToShortDateString();
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


            //txtYCODE.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }

    private void bindGvItemMaster()
    {
        string MACHINE_ISS_NO = string.Empty;
        string FromDate = string.Empty;
        string ToDate = string.Empty;
        string MERGE = string.Empty;


        try
        {
            if (txtMachIssNo.Text.Trim() != null && txtMachIssNo.Text.Trim() != string.Empty)
            {
                MACHINE_ISS_NO = txtMachIssNo.Text.Trim();
            }
            else
            {
                MACHINE_ISS_NO = string.Empty;
            }
            if (TxtFromDate.Text.Trim() != null || TxtToDate.Text.Trim() != null)
            {
                FromDate = TxtFromDate.Text.Trim().ToString();
                ToDate = TxtToDate.Text.Trim().ToString();

                DateTime.TryParse(TxtFromDate.Text, out FmDate);
                DateTime.TryParse(TxtToDate.Text, out Tdate);
            }
            if (txtYCODE.SelectedValue != null )
            {
                MERGE = txtYCODE.SelectedValue;
            }

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISE(MACHINE_ISS_NO, FmDate, Tdate, MERGE);
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
    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        bindGvItemMaster();
    }
    protected void CalculateAllData()
    {
        if (Grid1.Rows.Count > 0)
        {

            double TotalMachIssQty = 0;
            double TotalUnloadQty = 0;
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                double UnloadQty = 0;
                double IssQty = 0;
                Label lblMachIssQty = Grid1.Rows[i].FindControl("lblMachIssQty") as Label;
                Label lblUnloadQty = Grid1.Rows[i].FindControl("lblUnloadQty") as Label;
                double.TryParse(lblMachIssQty.Text, out IssQty);
                double.TryParse(lblUnloadQty.Text, out UnloadQty);
                TotalMachIssQty = TotalMachIssQty + IssQty;
                TotalUnloadQty = TotalUnloadQty + UnloadQty;
            }

            ((Label)Grid1.FooterRow.FindControl("lblTotalIssQty")).Text = TotalMachIssQty.ToString();
            ((Label)Grid1.FooterRow.FindControl("lblTotalUnloadQty")).Text = TotalUnloadQty.ToString();

        }
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
            Response.Redirect("./MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISE.aspx", false);
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
        string MACHINE_ISS_NO = string.Empty;
        string FromDate = string.Empty;
        string ToDate = string.Empty;
        string MERGE = string.Empty;
        try
        {
            string strFilename = "Machine_Issue_Details_Against_Doff_" + DateTime.Now.ToString() + ".xls";

            if (txtMachIssNo.Text.Trim() != null && txtMachIssNo.Text.Trim() != string.Empty)
            {
                MACHINE_ISS_NO = txtMachIssNo.Text.Trim();
            }
            else
            {
                MACHINE_ISS_NO = string.Empty;
            }
            if (TxtFromDate.Text.Trim() != null || TxtToDate.Text.Trim() != null)
            {
                FromDate = TxtFromDate.Text.Trim().ToString();
                ToDate = TxtToDate.Text.Trim().ToString();

                DateTime.TryParse(TxtFromDate.Text, out FmDate);
                DateTime.TryParse(TxtToDate.Text, out Tdate);
            } 
            if (txtYCODE.SelectedValue != null)
            {
                MERGE = txtYCODE.SelectedValue;
            }
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISE(MACHINE_ISS_NO, FmDate, Tdate, MERGE);
            if (dt.Rows.Count > 0)
                ExportDataTableToExcel(dt, strFilename, "Machine Issue Details Against COPS");
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



    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string msg = "";
            string QueryString = "";
            QueryString = "../Report/MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISEReport.aspx";
            bool flag = false;
            string BussType = string.Empty;

            if (txtMachIssNo.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "MACH_ISS_NO=" + txtMachIssNo.Text;
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

            if (txtYCODE.SelectedValue != null)
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "MERGE=" + txtYCODE.SelectedValue;
                flag = true;
              
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + QueryString + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=1000,height=600,left=200,top=300');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adjusting Receiving.\r\nSee error log for detail."));
        }
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
}
