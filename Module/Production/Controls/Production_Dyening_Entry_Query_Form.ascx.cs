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


public partial class Module_Production_Controls_Production_Dyening_Entry_Query_Form : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {


            InitialiseData();
        }
    }



    private void InitialiseData()
    {
        try
        {
             yearRecipe();
             cmbTRNNO.SelectedIndex = -1;
             txtYarn.SelectedIndex = -1;
             cmbPartyCode.SelectedIndex = -1;
             cmbJobCard.SelectedIndex = -1;
             btnGetReport_Click();

        }
        catch
        {
            throw;
        }

    }

    private void yearRecipe()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.yearRecipe(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            ddlYear.DataSource = dt;
            ddlYear.DataValueField = "YEAR";
            ddlYear.DataTextField = "YEAR";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, "");


        }
        catch
        {
            throw;

        }
    }

    protected void cmbTRN_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetTRN(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbTRNNO.Items.Clear();
                cmbTRNNO.DataSource = data;
                cmbTRNNO.DataBind();


            }
            // Calculating the numbr of items loaded so far in the ComboBox
            //e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            //// Getting the total number of items that start with the typed text
            //e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Prod TRN NO loading.\r\nSee error log for detail."));
        }
    }

    private DataTable GetTRN(string Text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;


            CommandText = " SELECT   TRN_NUMB, TRN_TYPE, TRN_NUMB2  FROM   (  SELECT   DISTINCT M.TRN_NUMB, M.TRN_TYPE, T.TRN_NUMB AS TRN_NUMB2  FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T  WHERE       M.COMP_CODE = T.COMP_CODE   AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR  AND M.TRN_NUMB = T.TRN_NUMB  AND M.BATCH_CODE = T.BATCH_CODE  AND M.TRN_NUMB LIKE :SearchQuery  AND M.TRN_TYPE LIKE :SearchQuery  ORDER BY   M.TRN_NUMB ASC) asd WHERE   TRN_NUMB = TRN_NUMB2 AND ROWNUM <= 15";

            string whereClause = string.Empty;

            if (startOffset != 0)
            {

                whereClause += "   AND TRN_NUMB NOT IN (SELECT   TRN_NUMB, TRN_NUMB2 FROM ( SELECT   DISTINCT M.TRN_NUMB, M.TRN_TYPE, T.TRN_NUMB AS TRN_NUMB2 FROM   YARN_DYEING_PROD_MST M,  YARN_DYEING_PROD_TRN T   WHERE  M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR  AND M.TRN_NUMB = T.TRN_NUMB  AND M.BATCH_CODE = T.BATCH_CODE  AND M.TRN_NUMB LIKE :SearchQuery  AND M.TRN_TYPE LIKE :SearchQuery ORDER BY   M.TRN_NUMB ASC) asd WHERE   TRN_NUMB = TRN_NUMB2 AND ROWNUM <= " + startOffset + ")";


            }



            string SortExpression = "  ORDER BY TRN_NUMB ASC  ";

            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }


    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            btnGetReport_Click();
            grdLabDipSubmission.PageIndex = e.NewPageIndex;
            grdLabDipSubmission.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void btnGetReport_Click(object sender, EventArgs e)
    {


        DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForProductionDyeingQueryGried(ddlYear.Text, cmbTRNNO.SelectedText.ToString(), txtYarn.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, cmbPartyCode.SelectedText.ToString(),cmbJobCard.SelectedValue.ToString());



        if (data.Rows.Count > 0)
        {
            int count = data.Rows.Count;
            lblTotalRecord.Text = count.ToString();
            grdLabDipSubmission.DataSource = data;
            grdLabDipSubmission.DataBind();


        }
        else
        {

            Common.CommonFuction.ShowMessage("Data Not Found");
        }

    }







    protected void btnGetReport_Click()
    {


        DataTable data = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForProductionDyeingQueryGried(ddlYear.Text, cmbTRNNO.SelectedText.ToString(), txtYarn.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, cmbPartyCode.SelectedText.ToString(), cmbJobCard.SelectedValue.ToString());



        if (data.Rows.Count > 0)
        {
            int count = data.Rows.Count;
            lblTotalRecord.Text = count.ToString();
            grdLabDipSubmission.DataSource = data;
            grdLabDipSubmission.DataBind();


        }
        else
        {

            Common.CommonFuction.ShowMessage("Data Not Found");
        }

    }





    protected void txtYCODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetYarn(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtYarn.Items.Clear();
                txtYarn.DataSource = data;
                txtYarn.DataBind();


            }
            // Calculating the numbr of items loaded so far in the ComboBox
            //e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            //// Getting the total number of items that start with the typed text
            //e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Quality loading.\r\nSee error log for detail."));
        }
    }



    private DataTable GetYarn(string Text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;


            CommandText = " SELECT   ARTICLE_CODE, ARTICAL_CODE, ARTICAL_DESC  FROM   (  SELECT   DISTINCT M.ARTICLE_CODE, T.ARTICAL_CODE, M.ARTICAL_DESC FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T WHERE M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR AND M.TRN_NUMB = T.TRN_NUMB AND M.ARTICLE_CODE = T.ARTICAL_CODE AND M.BATCH_CODE = T.BATCH_CODE AND M.ARTICLE_CODE LIKE :SearchQuery AND M.ARTICAL_DESC LIKE :SearchQuery ORDER BY   M.ARTICLE_CODE ASC) asd WHERE   ARTICLE_CODE = ARTICAL_CODE AND ROWNUM <= 15";

            string whereClause = string.Empty;

            if (startOffset != 0)
            {

                whereClause += " AND ARTICLE_CODE NOT IN (SELECT   ARTICLE_CODE, ARTICAL_CODE FROM   (  SELECT   DISTINCT M.ARTICLE_CODE, T.ARTICAL_CODE, M.ARTICAL_DESC FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T WHERE  M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR  AND M.TRN_NUMB = T.TRN_NUMB AND M.ARTICLE_CODE = T.ARTICAL_CODE AND M.BATCH_CODE = T.BATCH_CODE  AND M.ARTICLE_CODE LIKE :SearchQuery  AND M.ARTICAL_DESC LIKE :SearchQuery ORDER BY   M.ARTICLE_CODE ASC) asd  WHERE   ARTICLE_CODE = ARTICAL_CODE  AND ROWNUM <= " + startOffset + ")";


            }



            string SortExpression = "  ORDER BY ARTICLE_CODE ASC  ";

            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }











    protected void cmbPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetParty(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbPartyCode.Items.Clear();
                cmbPartyCode.DataSource = data;
                cmbPartyCode.DataBind();


            }
            // Calculating the numbr of items loaded so far in the ComboBox
            //e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            //// Getting the total number of items that start with the typed text
            //e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Name loading.\r\nSee error log for detail."));
        }
    }



    private DataTable GetParty(string Text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;


            CommandText = " SELECT   PRTY_CODE, PARTY_NAME, PARTY_CODE FROM   (  SELECT   DISTINCT M.PRTY_CODE, M.PARTY_NAME, M.PRTY_CODE AS PARTY_CODE FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T WHERE M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR AND M.TRN_NUMB = T.TRN_NUMB AND M.BATCH_CODE = T.BATCH_CODE AND M.PRTY_CODE LIKE :SearchQuery  AND M.PARTY_NAME LIKE :SearchQuery  ORDER BY   M.PRTY_CODE ASC) asd WHERE   PRTY_CODE = PARTY_CODE AND ROWNUM <= 15";

            string whereClause = string.Empty;

            if (startOffset != 0)
            {

                whereClause += " AND PRTY_CODE NOT IN (SELECT   PRTY_CODE, PARTY_NAME, PARTY_CODE FROM   (  SELECT   DISTINCT M.PRTY_CODE, M.PARTY_NAME, M.PRTY_CODE AS PARTY_CODE  FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T WHERE       M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR  AND M.TRN_NUMB = T.TRN_NUMB   AND M.BATCH_CODE = T.BATCH_CODE  AND M.PRTY_CODE LIKE :SearchQuery  AND M.PARTY_NAME LIKE :SearchQuery ORDER BY   M.PRTY_CODE ASC) asd  WHERE   PRTY_CODE = PARTY_CODE  AND ROWNUM <= " + startOffset + ")";


            }



            string SortExpression = "  ORDER BY PRTY_CODE ASC  ";

            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }







    protected void cmbJobCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetJob(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbJobCard.Items.Clear();
                cmbJobCard.DataSource = data;
                cmbJobCard.DataBind();


            }
            // Calculating the numbr of items loaded so far in the ComboBox
            //e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            //// Getting the total number of items that start with the typed text
            //e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Name loading.\r\nSee error log for detail."));
        }
    }



    private DataTable GetJob(string Text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;


            CommandText = " SELECT   BATCH_CODE, BATCH_CODE2  FROM   (  SELECT   DISTINCT M.BATCH_CODE, T.BATCH_CODE AS BATCH_CODE2 FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T WHERE M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR AND M.TRN_NUMB = T.TRN_NUMB  AND M.BATCH_CODE = T.BATCH_CODE  AND M.BATCH_CODE LIKE :SearchQuery ORDER BY   M.BATCH_CODE ASC) asd WHERE   BATCH_CODE = BATCH_CODE2 AND ROWNUM <= 15";

            string whereClause = string.Empty;

            if (startOffset != 0)
            {

                whereClause += " AND BATCH_CODE NOT IN (SELECT   BATCH_CODE, BATCH_CODE2 FROM   (  SELECT   DISTINCT  M.BATCH_CODE,  T.BATCH_CODE AS BATCH_CODE2 FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T WHERE M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR AND M.TRN_NUMB = T.TRN_NUMB  AND M.BATCH_CODE = T.BATCH_CODE  AND M.BATCH_CODE LIKE :SearchQuery  ORDER BY   M.BATCH_CODE ASC) asd WHERE   BATCH_CODE = BATCH_CODE2 AND ROWNUM <= " + startOffset + ")";


            }



            string SortExpression = "  ORDER BY BATCH_CODE ASC  ";

            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }


    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Production_Dyeing_Query_Form_" + DateTime.Now.ToShortDateString() + ".xls";

        DataTable table = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForProductionDyeingQueryGried(ddlYear.Text, cmbTRNNO.SelectedText.ToString(), txtYarn.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, cmbPartyCode.SelectedText.ToString(), cmbJobCard.SelectedValue.ToString());

        ExporttoExcel(table, strFilename, "Production Dyeing Query Form");
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
        InitialiseData();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));

        }

    }
}
