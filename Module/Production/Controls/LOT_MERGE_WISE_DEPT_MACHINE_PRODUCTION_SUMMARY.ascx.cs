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
using Obout.ComboBox;
using Obout.Interface;
using errorLog;
using Common;

public partial class Module_Production_Controls_LOT_MERGE_WISE_DEPT_MACHINE_PRODUCTION_SUMMARY : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    DateTime Sdate = System.DateTime.Now;
    DateTime Edate = System.DateTime.Now;
    DateTime StDate;
    DateTime EnDate;
    string COMP_CODE = string.Empty;
    string BRANCH_CODE = string.Empty;
    string IND_TYPE = string.Empty;


    string DEPT_CODE = string.Empty;
    string PROS_ID_NO = string.Empty;
    string From = string.Empty;
    string To = string.Empty;
    DateTime Stdate = System.DateTime.Now;
    DateTime Endate = System.DateTime.Now;
    string LOT_NUMBER = string.Empty;
    string ORDER_NO = string.Empty;
    string BRANCH_NAME = string.Empty;
    string PRTY_NAME = string.Empty;
    string ARTICLE_DESC = string.Empty;
    string PRODUCT_TYPE = string.Empty;
    string MERGE_CODE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Sdate = oUserLoginDetail.DT_STARTDATE;
                Edate = Common.CommonFuction.GetYearEndDate(Sdate);
                TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
                Initialize();
                bindGvWipStockDetails();


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }
    private void Initialize()
    {
        txtYCODE.SelectedIndex = -1;
        getBrachName();
        getDepartment();
    }


    private void getDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("------------Select-------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getBrachName()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            ddlBranch.DataSource = dt;
            ddlBranch.DataValueField = "BRANCH_NAME";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("------------Select-------------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void bindGvWipStockDetails()
    {
       
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
            if (txtLot.SelectedValue.ToString() != null && txtLot.SelectedValue.ToString() != string.Empty)
            {
                LOT_NUMBER = txtLot.SelectedValue.ToString();
            }
            else
            {
                LOT_NUMBER = string.Empty;
            }
            if (txtPartyCODE.SelectedValue.ToString() != null && txtPartyCODE.SelectedValue.ToString() != string.Empty)
            {
                PRTY_NAME = txtPartyCODE.SelectedValue.ToString();
            }
            else
            {
                PRTY_NAME = string.Empty;
            }
            if (txtYCODE.SelectedValue.ToString() != null && txtYCODE.SelectedValue.ToString() != string.Empty)
            {
                ARTICLE_DESC = txtYCODE.SelectedValue.ToString();
            }
            else
            {
                ARTICLE_DESC = string.Empty;
            }
            if (txtYCODE.SelectedValue.ToString() != null && txtYCODE.SelectedValue.ToString() != string.Empty)
            {
                MERGE_CODE = txtYCODE.SelectedValue.ToString();
            }
            else
            {
                MERGE_CODE = string.Empty;
            }
            if (TxtFromDate.Text.Trim() != null || TxtToDate.Text.Trim() != null)
            {
                DateTime.TryParse(TxtFromDate.Text, out StDate);
                DateTime.TryParse(TxtToDate.Text, out EnDate);
            }

            if (ddlProductType.SelectedValue.ToString() != null && ddlProductType.SelectedValue.ToString() != string.Empty && ddlProductType.SelectedIndex > 0)
            {
                PRODUCT_TYPE = ddlProductType.SelectedValue.ToString();
            }
            else
            {
                PRODUCT_TYPE = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.MERGEWISE_DEPT_MACHINE_PRODUCTION_SUMMARY(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, From, To, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, ChkStock.Checked, MERGE_CODE);
            Session["dtPROD"] = SaitexBL.Interface.Method.YRN_PROD_MST.GET_MERGE_LOT_WISE_DEPT_MACHINE_PRODUCTION(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, From, To, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, ChkStock.Checked, "");
            Session["dtPACK"] = SaitexBL.Interface.Method.YRN_PROD_MST.GET_MERGE_LOT_WISE_DEPT_MACHINE_PACKING(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, From, To, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, ChkStock.Checked, "");
          
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
              dt.Dispose();
             Session["dtPACK"]=null;
             Session["dtPROD"] = null;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        bindGvWipStockDetails();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            bindGvWipStockDetails();
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
            Session["dtPROD"] = null;
            Session["dtPACK"] = null;
           
            Response.Redirect("./MERGEWISE_DEPT_MACHINE_PRODUCTION_SUMMARY.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string Query_String = string.Empty;
        try
        {
            string QueryString = "";
            bool flag = false;

            if (ddlBranch.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "BRANCH_NAME" + ddlBranch.SelectedValue.Trim();
                flag = true;
            }

            if (txtLot.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "LOT_NUMBER=" + txtLot.SelectedValue.Trim();
                flag = true;
            }

            if (txtYCODE.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "ARTICLE_DESC=" + txtYCODE.SelectedValue.Trim();
                flag = true;
            }
            if (txtPartyCODE.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "PRTY_NAME=" + txtPartyCODE.SelectedValue.Trim();
                flag = true;
            }
            
            if (TxtFromDate.Text.Trim() != null || TxtToDate.Text.Trim() != null)
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "FromDate=" + TxtFromDate.Text + "&ToDate=" + TxtToDate.Text;
                flag = true;


            }
            string URL = "../../Production/Report/MERGEWISE_DEPT_MACHINE_PRODUCTION_SUMMARY_REPORT.aspx" + QueryString;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void txtYCODE_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
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

            string CommandText = " SELECT   *  FROM   (  SELECT   MST_CODE AS FIBER_CODE,                     MST_DESC, FIBER_DESC,                     MST_CODE || '@' || MST_DESC||'@'||FIBER_DESC                         AS Combined              FROM   TX_MASTER_TRN T, TX_FIBER_MASTER U             WHERE  T.OTHER_INFO=U.FIBER_CODE              AND (UPPER(MST_CODE) LIKE :SearchQuery    OR UPPER (FIBER_DESC) LIKE :SearchQuery )     ORDER BY   MST_CODE) asd WHERE   ROWNUM <= 15 ";
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
        string whereClause = "";
        string SortExpression = " ORDER BY FIBER_CODE ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery);
        return data.Rows.Count;

    }




    //---LAST--

    protected void txtLot_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItemsLot(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtLot.Items.Clear();
                txtLot.DataSource = data;
                txtLot.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCountLot(e.Text);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fabric loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItemsLot(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   *  FROM   (  SELECT   LOT_NO,   LOT_TYPE,   LOT_NO || '@' || LOT_TYPE      AS Combined     FROM   YRN_LOT_MAKING    WHERE   LOT_NO LIKE :SearchQuery     OR LOT_TYPE LIKE :SearchQuery    ORDER BY   LOT_NO) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND LOT_NO NOT IN (SELECT LOT_NO FROM (SELECT LOT_NO, LOT_TYPE, LOT_NO ||'@'|| LOT_TYPE  as Combined   FROM YRN_LOT_MAKING T WHERE LOT_NO LIKE :SearchQuery AND LOT_TYPE LIKE :SearchQuery AND PRODUCT_CATEGORY = 'TEXTURISING' AND CONF_FLAG = '1' ORDER BY LOT_NO) asd WHERE ROWNUM <= " + startOffset + ")   ";
            }

            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";

            DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery);

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCountLot(string text)
    {
        string CommandText = " SELECT  * FROM ( SELECT LOT_NO, LOT_TYPE  FROM YRN_LOT_MAKING T  WHERE   LOT_NO LIKE :SearchQuery AND LOT_TYPE LIKE :SearchQuery AND PRODUCT_CATEGORY = 'TEXTURISING' AND CONF_FLAG = '1'  ORDER BY LOT_NO) asd ";
        string WhereClause = " ";
        string SortExpression = "  ";
        string SearchQuery = text.ToUpper() + "%";

        DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery);

        return data.Rows.Count;

    }

    protected void txtPartyCODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItemsPartyName(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtPartyCODE.Items.Clear();
                txtPartyCODE.DataSource = data;
                txtPartyCODE.DataValueField = "PRTY_NAME";
                txtPartyCODE.DataTextField = "PRTY_NAME";
                txtPartyCODE.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCountPartyName(e.Text);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fabric loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItemsPartyName(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   *  FROM   (  SELECT DISTINCT PRTY_CODE, PRTY_NAME,  PRTY_CODE || '@' || PRTY_NAME      AS Combined     FROM   TX_VENDOR_MST U    WHERE  ( UPPER(PRTY_CODE) LIKE :SearchQuery   OR UPPER(PRTY_NAME) LIKE :SearchQuery) AND DEL_STATUS = '0'   ORDER BY   PRTY_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND PRTY_CODE NOT IN (SELECT DISTINCT PRTY_CODE FROM (SELECT PRTY_CODE, PRTY_NAME,  PRTY_CODE || '@' || PRTY_NAME      AS Combined     FROM   TX_VENDOR_MST U WHERE (UPPER(PRTY_CODE) LIKE :SearchQuery   OR UPPER(PRTY_NAME) LIKE :SearchQuery) AND DEL_STATUS = '0' ORDER BY PRTY_CODE) asd WHERE ROWNUM <= " + startOffset + ")   ";
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

    protected int GetItemsCountPartyName(string text)
    {
        string CommandText = " SELECT  * FROM ( SELECT DISTINCT PRTY_CODE, PRTY_NAME,   PRTY_CODE || '@' || PRTY_NAME      AS Combined     FROM   TX_VENDOR_MST U    WHERE  (UPPER(PRTY_CODE) LIKE :SearchQuery   OR UPPER(PRTY_NAME) LIKE :SearchQuery) AND DEL_STATUS = '0'  ORDER BY PRTY_CODE) asd ";
        string WhereClause = " ";
        string SortExpression = "  ";
        string SearchQuery = "%" + text.ToUpper() + "%";

        DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery);

        return data.Rows.Count;

    }



    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strFilename = "LOT_MERGE_WISE_DEPT_MACHINE_PRODUCTION_SUMMARY_" + DateTime.Now.ToString() + ".xls";
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
            if (txtLot.SelectedValue.ToString() != null && txtLot.SelectedValue.ToString() != string.Empty)
            {
                LOT_NUMBER = txtLot.SelectedValue.ToString();
            }
            else
            {
                LOT_NUMBER = string.Empty;
            }
            if (txtPartyCODE.SelectedValue.ToString() != null && txtPartyCODE.SelectedValue.ToString() != string.Empty)
            {
                PRTY_NAME = txtPartyCODE.SelectedValue.ToString();
            }
            else
            {
                PRTY_NAME = string.Empty;
            }
            if (txtYCODE.SelectedValue.ToString() != null && txtYCODE.SelectedValue.ToString() != string.Empty)
            {
                ARTICLE_DESC = txtYCODE.SelectedValue.ToString();
            }
            else
            {
                ARTICLE_DESC = string.Empty;
            }
            if (txtYCODE.SelectedValue.ToString() != null && txtYCODE.SelectedValue.ToString() != string.Empty)
            {
                MERGE_CODE = txtYCODE.SelectedValue.ToString();
            }
            else
            {
                MERGE_CODE = string.Empty;
            }
            if (TxtFromDate.Text.Trim() != null || TxtToDate.Text.Trim() != null)
            {
                DateTime.TryParse(TxtFromDate.Text, out StDate);
                DateTime.TryParse(TxtToDate.Text, out EnDate);
            }

            if (ddlProductType.SelectedValue.ToString() != null && ddlProductType.SelectedValue.ToString() != string.Empty && ddlProductType.SelectedIndex > 0)
            {
                PRODUCT_TYPE = ddlProductType.SelectedValue.ToString();
            }
            else
            {
                PRODUCT_TYPE = string.Empty;
            }

            
            DataTable dtPROD = SaitexBL.Interface.Method.YRN_PROD_MST.GET_MERGE_LOT_WISE_DEPT_MACHINE_PRODUCTION(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, From, To, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, ChkStock.Checked, "");
            DataTable dtPACK = SaitexBL.Interface.Method.YRN_PROD_MST.GET_MERGE_LOT_WISE_DEPT_MACHINE_PACKING(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, From, To, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, ChkStock.Checked, "");
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.MERGEWISE_DEPT_MACHINE_PRODUCTION_SUMMARY(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, From, To, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, ChkStock.Checked, MERGE_CODE);
            if (dt.Rows.Count > 0)
                ExportDataTableToExcel(dt, strFilename, "MERGEWISE DEPT, MACHINE & PRODUCTION SUMMARY WITH LOT NO",dtPROD, dtPACK);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Binding stock.\r\nSee error log for detail."));
        }
    }


    protected void ExportDataTableToExcel(DataTable table, string name, string title, DataTable dtPROD, DataTable dtPACK)
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
               

                //******************************************//
                if (i == 7)
                {
                  
                    DataView dvPROD = new DataView(dtPROD);                   
                    dvPROD.RowFilter = "MERGE_NO='" + row[2].ToString() + "'";

                    if (dvPROD.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<Tr>");
                        //foreach (DataColumn dtcol1 in dvPROD.Table.Columns )
                        //{
                        //    HttpContext.Current.Response.Write("<Td>");
                        //    HttpContext.Current.Response.Write("<B>");
                        //    HttpContext.Current.Response.Write(dtcol1.ColumnName.Replace("_", " "));
                        //    HttpContext.Current.Response.Write("</B>");
                        //    HttpContext.Current.Response.Write("</Td>");

                        //}
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("LOT_NO");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("PROD QTY");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("PROD COPS");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("</Tr>");
                        for  (int j=0;j<dvPROD.Count ;j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvPROD.Table.Columns.Count-1; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write(dvPROD[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                }



                if (i == 9)
                {
                    DataView dvPACK = new DataView(dtPACK);
                    dvPACK.RowFilter = "MERGE_NO='" + row[2].ToString() + "'";
                    if (dvPACK.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<Tr>");
                        //foreach (DataColumn dtcol1 in dvPACK.Table.Columns)
                        //{
                        //    HttpContext.Current.Response.Write("<Td>");
                        //    HttpContext.Current.Response.Write("<B>");
                        //    HttpContext.Current.Response.Write(dtcol1.ColumnName.Replace("_", " "));
                        //    HttpContext.Current.Response.Write("</B>");
                        //    HttpContext.Current.Response.Write("</Td>");

                        //}
                        
                            HttpContext.Current.Response.Write("<Td>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write("LOT_NO");
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");

                            HttpContext.Current.Response.Write("<Td>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write("PACK QTY");
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");
                            HttpContext.Current.Response.Write("<Td>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write("PACK COPS");
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");
                      
                        HttpContext.Current.Response.Write("</Tr>");


                        for (int j = 0; j < dvPACK.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvPACK.Table.Columns.Count-1; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write(dvPACK[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                }  
                //***********************************************//   
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

    /////////////////////////////

    protected void RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataView dvPACK = new DataView();
                DataView dvPROD = new DataView();
                if (Session["dtPROD"] != null)
                {
                     dvPROD = new DataView((DataTable)Session["dtPROD"]);
                }
                if (Session["dtPACK"] != null)
                {
                     dvPACK = new DataView((DataTable)Session["dtPACK"]);
                }

                GridViewRow grdRow = e.Row;
                Label lblMerge_No = (Label)grdRow.FindControl("lblMerge_No");

                dvPROD.RowFilter = "MERGE_NO='" + lblMerge_No.Text + "'";
                if (dvPROD != null && dvPROD.Count > 0)
                {
                    GridView grdPRODUCTION = (GridView)grdRow.FindControl("grdPRODUCTION");
                    grdPRODUCTION.DataSource = dvPROD;
                    grdPRODUCTION.DataBind();
                    grdPRODUCTION.Dispose();
                    dvPROD.Dispose();
                }

                dvPACK.RowFilter = "MERGE_NO='" + lblMerge_No.Text + "'";  
                if (dvPACK != null && dvPACK.Count > 0)
                {
                    GridView grdPACKING = (GridView)e.Row.FindControl("grdPACKING");
                    if (grdPACKING != null)
                    {
                        grdPACKING.DataSource = dvPACK;
                        grdPACKING.DataBind();
                        grdPACKING.Dispose();
                        dvPACK.Dispose();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));

        }


    }
}
