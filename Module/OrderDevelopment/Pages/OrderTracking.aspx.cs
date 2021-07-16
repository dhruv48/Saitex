using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_OrderDevelopment_Pages_OrderTracking : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
   
    public static string ORDER_TYPE = string.Empty;
    public static string PRTY_CODE = string.Empty;
    public static string COMP_CODE = string.Empty;
    public static string BRANCH_CODE = string.Empty;
    public static string Shade_Code = string.Empty;
    public static string Quality = string.Empty;
    public static int YEAR = 0;  
    public static string ORDER_NO = string.Empty;
    public static string BATCH_CODE = string.Empty;
    public static string CATEGORY = string.Empty;
   
   
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try {
            if (!IsPostBack)
            {
             
            }
            BindField();
        }
        catch (Exception ex)
        { throw ex; }
    }
    public void BindGrid()
    {
        try {
            DataTable dt = new DataTable();
            dt = BindData();
           
            if (CATEGORY == "Order Tracking")
            {
                GridJobCardAnalysis.Visible = false;
                Grid_OrderTracking.Visible = true;
                if (dt != null)
                {
                    Grid_OrderTracking.DataSource = dt;
                    Grid_OrderTracking.DataBind();
                }
            }
            if (CATEGORY == "JobCard Analysis")
            {
                Grid_OrderTracking.Visible = false;
                GridJobCardAnalysis.Visible = true;
                if (dt != null)
                {
                    GridJobCardAnalysis.DataSource = dt;
                    GridJobCardAnalysis.DataBind();

                }
            }
           
          

        }
        catch (Exception ex)
        { 
            throw ex;
        }
    }
    public void BindField()
    {
        try
        {


            if (ddlprtycode.SelectedItem != null && ddlprtycode.SelectedItem != "")
            {
                PRTY_CODE = ddlprtycode.SelectedItem;
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (ddlArticle.SelectedText != null && ddlArticle.SelectedText != "")
            {
                Quality = ddlArticle.SelectedValue;
            }
            else
            {
                Quality = string.Empty;
            }
            if (cmbShade.SelectedValue != null && cmbShade.SelectedValue != "")
            {
                Shade_Code = cmbShade.SelectedValue;
            }

            else
            {
                Shade_Code = string.Empty;
            }
            if (txtOrderNo.Text != null && txtOrderNo.Text != "")
            {
                ORDER_NO = txtOrderNo.Text;
            }
            else
            {
                ORDER_NO = string.Empty;
            }

            if (txt_BatchCode.Text != null && txt_BatchCode.Text != "")
            {
                BATCH_CODE = txt_BatchCode.Text;
            }
            else
            {
                BATCH_CODE = string.Empty;
            }



            if (ddl_category.SelectedValue != "0")
            {
                CATEGORY = ddl_category.SelectedItem.ToString();
            }
            else
            {
                CATEGORY = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable BindData()
    {


        try {

            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.OrderTrackingReport(PRTY_CODE, Quality, Shade_Code, ORDER_NO, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BATCH_CODE,CATEGORY);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlArticle_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
          
            DataTable data = GetItemsSearch(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlArticle.Items.Clear();
                ddlArticle.DataSource = data;
                ddlArticle.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCountSearch(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article Loading.\r\nSee error log for detail."));
        }
    }


    protected DataTable GetItemsSearch(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (  SELECT  YA.ASS_YARN_CODE YARN_CODE, YA.ASS_YARN_DESC      YARN_DESC,         YM.YARN_TYPE,  YM.YARN_CAT,    (   YM.TKTNO || '@' ||      YM.MAKE || '@'|| YM.ENDUSE  || '@'                      || YA.ASS_YARN_CODE || '@'   || UNITWT|| '@' || NET_BOX_WT  || '@'  || NET_CART_WT                      || '@'  || UOM  || '@'  || YA.ASS_YARN_DESC) AS Combined      FROM   YRN_MST YM ,YRN_ASSOCATED_MST YA      WHERE   YM.YARN_CODE=YA.YARN_CODE    AND NOT  (YARN_TYPE = 'NON DYED')   AND (   YA.ASS_YARN_CODE LIKE :SearchQuery   OR YM.YARN_TYPE LIKE :SearchQuery OR YA.ASS_YARN_DESC LIKE :SearchQuery)      ORDER BY   YA.YARN_CODE) asd WHERE 1=1";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN (SELECT YARN_CODE FROM (SELECT  YA.ASS_YARN_CODE YARN_CODE, YA.ASS_YARN_DESC      YARN_DESC,         YM.YARN_TYPE,  YM.YARN_CAT,    (   YM.TKTNO || '@' ||      YM.MAKE || '@'|| YM.ENDUSE  || '@'                      || YA.ASS_YARN_CODE || '@'   || UNITWT|| '@' || NET_BOX_WT  || '@'  || NET_CART_WT                      || '@'  || UOM  || '@'  || YA.ASS_YARN_DESC) AS Combined      FROM   YRN_MST YM ,YRN_ASSOCATED_MST YA      WHERE   YM.YARN_CODE=YA.YARN_CODE  AND NOT  (YARN_TYPE = 'NON DYED')  AND (   YA.ASS_YARN_CODE LIKE :SearchQuery   OR YM.YARN_TYPE LIKE :SearchQuery OR YA.ASS_YARN_DESC LIKE :SearchQuery)  AND ROWNUM <= " + startOffset + "      ORDER BY   YA.YARN_CODE) asd   )";
            }
            string SortExpression = " ORDER BY YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int GetItemsCountSearch(string text)
    {
        try
        {
            string CommandText = " SELECT  YA.ASS_YARN_CODE    FROM   YRN_MST YM ,YRN_ASSOCATED_MST YA      WHERE   YM.YARN_CODE=YA.YARN_CODE  AND YARN_SHADE='DYED'    AND (   YA.ASS_YARN_CODE LIKE :SearchQuery   OR YM.YARN_TYPE LIKE :SearchQuery OR YA.ASS_YARN_DESC LIKE :SearchQuery)      ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY YARN_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetPartyDataSearch(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch (Exception ex)
        {
            throw ex;
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
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT  distinct M.SHADE_CODE,   M.SHADE_FAMILY_CODE,  (M.SHADE_FAMILY_CODE || '@' || M.SHADE_CODE)   AS Combined  FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T   WHERE       M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'   AND M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE   AND M.YEAR = T.YEAR   AND M.LAB_DIP_NO = T.LAB_DIP_NO    AND M.LR_OPTION = T.LR_OPTION    ORDER BY   SHADE_FAMILY_CODE) ASD   WHERE   SHADE_FAMILY_CODE LIKE :SearchQuery    OR SHADE_CODE LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT distinct  M.SHADE_CODE,   M.SHADE_FAMILY_CODE,  (M.SHADE_FAMILY_CODE || '@' || M.SHADE_CODE)   AS Combined  FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T   WHERE       M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'   AND M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE   AND M.YEAR = T.YEAR   AND M.LAB_DIP_NO = T.LAB_DIP_NO    AND M.LR_OPTION = T.LR_OPTION    ORDER BY   SHADE_FAMILY_CODE) ASD   WHERE   SHADE_FAMILY_CODE LIKE :SearchQuery    OR SHADE_CODE LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch (Exception ex)
        {
            throw ex;
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
        catch (Exception ex)
        {
            throw ex;
        }
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
                HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                HttpContext.Current.Response.Write(row[i].ToString());
                //******************************************//




                //***********************************************//   

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



    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

        ddlprtycode.SelectedValue = string.Empty;
        ddlArticle.SelectedValue = string.Empty;
        cmbShade.SelectedValue = string.Empty;
        txtOrderNo.Text = "";
        txt_BatchCode.Text = "";
        ddl_category.SelectedValue = string.Empty;


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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leaving the page"));
        }
    }
    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddl_category.SelectedValue == "1")
            {
            string strFilename = "Order_Tracking_" + DateTime.Now.ToShortDateString() + ".xls";
            ExporttoExcel(BindData(), strFilename, "Order Tracking Details");
            }
            if (ddl_category.SelectedValue == "2")
            {
                string strFilename = "JobCard_Analysis_" + DateTime.Now.ToShortDateString() + ".xls";
                ExporttoExcel(BindData(), strFilename, "JobCard Analysis");
            }
           
        }
        catch (Exception eX)
        {
            throw eX;
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {

    }
    protected override void OnInit(EventArgs e)
    {
        ddlprtycode.AutoPostBack = false;
        base.OnInit(e);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try { 
            if(Page.IsValid)
            {
                string QueryString = "";
                QueryString += "?PRTY_CODE=" + PRTY_CODE;
                QueryString += "&Quality=" + Quality;
                QueryString += "&Shade_Code=" + Shade_Code;
                QueryString += "&ORDER_NO=" + ORDER_NO;
                QueryString += "&BATCH_CODE=" + BATCH_CODE;
                QueryString += "&CATEGORY=" + CATEGORY;
                string URL = "../Reports/OrderTrackingReport.aspx" + QueryString;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    protected void Grid_OrderTracking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try 
        {
            if (ddl_category.SelectedValue!= "0")
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}