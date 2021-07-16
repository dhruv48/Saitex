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
public partial class Module_Yarn_SalesWork_Queries_Dispatch_Query_Form : System.Web.UI.UserControl
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;

    private DataTable dtLRGenerate = null;
    private DataTable dtDye = null;
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
            bindBusinessType();
            yearRecipe();
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = (Sdate.ToShortDateString()).ToString();
            TxtToDate.Text = (Edate.ToShortDateString()).ToString();

            txtYarn.SelectedIndex = -1;


            btnGetReport_Click();

        }
        catch
        {
            throw;
        }

    }

    private void bindBusinessType()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);
            ddlBusinessType.DataSource = dt;
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataBind();
            ddlBusinessType.Items.Insert(0, "");
            ddlBusinessType.SelectedIndex = -1;
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
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.yearRecipe(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            ddlYear.DataSource = dt;
            //ddlYear.Items.Add("----");
            ddlYear.DataValueField = "YEAR";
            ddlYear.DataTextField = "YEAR";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, "");
            ddlYear.SelectedIndex = -1;
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
            //     btnGetReport_Click();
            // grdLabDipSubmission.PageIndex = e.NewPageIndex;
            // grdLabDipSubmission.DataBind();
        }
        catch (Exception ex)
        {
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
                    Common.CommonFuction.ShowMessage("Date is fine .");
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








    protected void txtYCODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetYCODE(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtYarn.Items.Clear();
                txtYarn.DataSource = data;
                txtYarn.DataBind();
            }
            //  Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            //e.ItemsCount = GetIYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
        }
    }


    //protected int GetIYarnCount(string text)
    //{
    //    try
    //    {
    //        string CommandText = string.Empty;
    //        CommandText = "SELECT   a.YARN_CODE FROM   YRN_IR_MST M, YRN_IR_TRN T, YRN_ASSOCATED_MST A  WHERE M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR AND M.TRN_NUMB = T.TRN_NUMB   AND M.TRN_TYPE = T.TRN_TYPE AND M.TRN_TYPE = 'IYS26' AND RTRIM (LTRIM (T.YARN_CODE)) =  RTRIM (LTRIM (A.YARN_CODE))";
    //        string WhereClause = " ";
    //        string SortExpression = " ORDER BY   a.YARN_CODE ASC  ";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
    //        return data.Rows.Count;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}


    private DataTable GetYCODE(string Text, int startOffset)
    {
        try
        {
            string CommandText = "  SELECT   YARN_CODE, ASS_YARN_DESC    FROM   (  SELECT   *  FROM   (SELECT   DISTINCT a.YARN_CODE, A.ASS_YARN_DESC  FROM   YRN_IR_MST M, YRN_IR_TRN T, YRN_ASSOCATED_MST A  WHERE       M.COMP_CODE = T.COMP_CODE   AND M.BRANCH_CODE = T.BRANCH_CODE   AND M.YEAR = T.YEAR   AND M.TRN_NUMB = T.TRN_NUMB   AND M.TRN_TYPE = T.TRN_TYPE  AND M.TRN_TYPE = 'IYS26' AND RTRIM (LTRIM (T.YARN_CODE)) = RTRIM (LTRIM (A.YARN_CODE))) WHERE   YARN_CODE LIKE :SearchQuery  OR ASS_YARN_DESC LIKE :SearchQuery            ORDER BY   YARN_CODE ASC) asd   WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN ( SELECT   YARN_CODE, ASS_YARN_DESC    FROM   (  SELECT   *  FROM   (SELECT   DISTINCT a.YARN_CODE, A.ASS_YARN_DESC  FROM   YRN_IR_MST M, YRN_IR_TRN T, YRN_ASSOCATED_MST A  WHERE       M.COMP_CODE = T.COMP_CODE   AND M.BRANCH_CODE = T.BRANCH_CODE   AND M.YEAR = T.YEAR   AND M.TRN_NUMB = T.TRN_NUMB   AND M.TRN_TYPE = T.TRN_TYPE  AND M.TRN_TYPE = 'IYS26' AND RTRIM (LTRIM (T.YARN_CODE)) = RTRIM (LTRIM (A.YARN_CODE))) WHERE   YARN_CODE LIKE :SearchQuery  OR ASS_YARN_DESC LIKE :SearchQuery            ORDER BY   YARN_CODE ASC)asd where   ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY YARN_CODE ASC";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }



    //protected void grdLabDipSubmission_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            GridViewRow grdRow = e.Row;




    //            DataTable data = SaitexBL.Interface.Method.YRN_IR_MST.Yrn_Delevery_Order_Query_Form(ddlBusinessType.SelectedValue.ToString(),oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, Sdate, Edate, txtYarn.SelectedText.ToString());


    //            if (data != null && data.Rows.Count > 0)
    //            {
    //                DataView dv = new DataView(data);
                   
    //                GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
    //                grdPOTRN.DataSource = dv;
    //                grdPOTRN.DataBind();
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));

    //    }
    //}






    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        Sdate = DateTime.Parse(TxtFromDate.Text);
        Edate = DateTime.Parse(TxtToDate.Text);

        DataTable data = SaitexBL.Interface.Method.YRN_IR_MST.Yrn_Delevery_Order_Query_Form(ddlBusinessType.SelectedValue.ToString(), oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), Sdate, Edate, txtYarn.SelectedText.ToString());


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
        Sdate = DateTime.Parse(TxtFromDate.Text);
        Edate = DateTime.Parse(TxtToDate.Text);



        DataTable data = SaitexBL.Interface.Method.YRN_IR_MST.Yrn_Delevery_Order_Query_Form(ddlBusinessType.SelectedValue.ToString(), oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), Sdate, Edate, txtYarn.SelectedText.ToString());



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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));

        }
    }
    //protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    //{
    //    string strFilename = "Issue_Against_PA_Report_" + DateTime.Now.ToShortDateString() + ".xls";
    //    Sdate = DateTime.Parse(TxtFromDate.Text);
    //    Edate = DateTime.Parse(TxtToDate.Text);



    //    DataTable data = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetDataForRecipeGried(ddlYear.Text, cmbCustomer.SelectedText.ToString(), txtYarn.SelectedText.ToString(), cmbGreyLotNo.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Sdate, Edate, cmbMachineCode.SelectedValue.ToString());

    //    ExporttoExcel(data, strFilename, "Issue Against PA Report");
    //}



    //private void ExporttoExcel(DataTable table, string name, string title)
    //{
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.ClearContent();
    //    HttpContext.Current.Response.ClearHeaders();
    //    HttpContext.Current.Response.Buffer = true;
    //    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
    //    HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
    //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

    //    HttpContext.Current.Response.Charset = "utf-8";
    //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
    //    //sets font
    //    HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
    //    HttpContext.Current.Response.Write("<BR><BR><BR>");
    //    //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
    //    HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
    //      "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
    //      "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
    //    //am getting my grid's column headers
    //    int columnscount = table.Columns.Count;
    //    HttpContext.Current.Response.Write("<TR>");
    //    HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
    //    HttpContext.Current.Response.Write("<B>");
    //    HttpContext.Current.Response.Write(oUserLoginDetail.VC_COMPANYNAME);
    //    HttpContext.Current.Response.Write("</B>");
    //    HttpContext.Current.Response.Write("</TD>");
    //    HttpContext.Current.Response.Write("</TR>");

    //    HttpContext.Current.Response.Write("<TR>");
    //    HttpContext.Current.Response.Write("<TD style='font-size:12.0pt;' align='center' colspan=" + columnscount + ">");
    //    HttpContext.Current.Response.Write("<B>");
    //    HttpContext.Current.Response.Write(" " + title + " ");
    //    HttpContext.Current.Response.Write("</B>");
    //    HttpContext.Current.Response.Write("</TD>");
    //    HttpContext.Current.Response.Write("</TR>");

    //    HttpContext.Current.Response.Write("<TR>");
    //    HttpContext.Current.Response.Write("<TD  align='center' colspan=" + columnscount + ">");
    //    HttpContext.Current.Response.Write("<B>");
    //    HttpContext.Current.Response.Write("DATED:" + DateTime.Now.ToString() + "");
    //    HttpContext.Current.Response.Write("</B>");
    //    HttpContext.Current.Response.Write("</TD>");
    //    HttpContext.Current.Response.Write("</TR>");


    //    HttpContext.Current.Response.Write("<TR>");

    //    foreach (DataColumn dtcol in table.Columns)
    //    {
    //        HttpContext.Current.Response.Write("<Td>");
    //        HttpContext.Current.Response.Write("<B>");
    //        HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
    //        HttpContext.Current.Response.Write("</B>");
    //        HttpContext.Current.Response.Write("</Td>");

    //    }

    //    //HttpContext.Current.Response.Write("<Td align='center' valing='top'>");
    //    //HttpContext.Current.Response.Write("<B>");
    //    //HttpContext.Current.Response.Write("Recipe Details");
    //    //HttpContext.Current.Response.Write("</B>");
    //    //HttpContext.Current.Response.Write("</Td>");

    //    //HttpContext.Current.Response.Write("</TR>");
    //    foreach (DataRow row in table.Rows)
    //    {//write in new row
    //        HttpContext.Current.Response.Write("<TR>");
    //        for (int i = 0; i < table.Columns.Count; i++)
    //        {
    //            HttpContext.Current.Response.Write("<Td align=left valign=Top>");

    //            HttpContext.Current.Response.Write(row[i].ToString());
    //            //******************************************//
    //            //      if (i == 15)
    //            //      {

    //            //          HttpContext.Current.Response.Write("<Td align=left valign=Top>");

    //            //          DataView dvBASEQUALITY = new DataView(dtBASEQUALITY);
    //            //          dvBASEQUALITY.RowFilter = "CUSTOMER_REQ_NO='" + row["CUSTOMER_REQ_NO"].ToString() + "' and LAB_DIP_NO='" + row["LAB_DIP_NO"].ToString() + "' AND LR_OPTION='" + row["LR_OPTION"].ToString() + "'";

    //            //          if (dvBASEQUALITY.Count > 0)
    //            //          {
    //            //              HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
    //            //"borderColor='#000000' cellSpacing='0' cellPadding='0' " +
    //            //"style='font-size:10.0pt; font-family:Calibri; background:white;'>");
    //            //              HttpContext.Current.Response.Write("<TR>");

    //            //              foreach (DataColumn dtcol in dtBASEQUALITY.Columns)
    //            //              {
    //            //                  HttpContext.Current.Response.Write("<Td bgcolor=silver>");
    //            //                  HttpContext.Current.Response.Write("<B>");
    //            //                  HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
    //            //                  HttpContext.Current.Response.Write("</B>");
    //            //                  HttpContext.Current.Response.Write("</Td>");

    //            //              }
    //            //              HttpContext.Current.Response.Write("</TR>");
    //            //              for (int j = 0; j < dvBASEQUALITY.Count; j++)
    //            //              {
    //            //                  HttpContext.Current.Response.Write("<Tr>");
    //            //                  for (int i1 = 0; i1 < dvBASEQUALITY.Table.Columns.Count; i1++)
    //            //                  {
    //            //                      HttpContext.Current.Response.Write("<Td >");
    //            //                      HttpContext.Current.Response.Write(dvBASEQUALITY[j][i1].ToString());
    //            //                      HttpContext.Current.Response.Write("</Td>");

    //            //                  }
    //            //                  HttpContext.Current.Response.Write("</Tr>");
    //            //              }
    //            //              HttpContext.Current.Response.Write("</Table>");
    //            //          }
    //            //          HttpContext.Current.Response.Write("</Td>");

    //            //      }



    //            //***********************************************//   

    //            HttpContext.Current.Response.Write("</Td>");

    //        }

    //        HttpContext.Current.Response.Write("</TR>");


    //    }
    //    HttpContext.Current.Response.Write("</Table>");
    //    HttpContext.Current.Response.Write("</font>");
    //    HttpContext.Current.Response.Flush();
    //    HttpContext.Current.Response.End();
    //    //  HttpContext.Current.ApplicationInstance.CompleteRequest();
    //}

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Sdate = DateTime.Parse(TxtFromDate.Text);
        Edate = DateTime.Parse(TxtToDate.Text);
        string URL = "../Reports/Dispatch_Print_Report.aspx?";
        URL += "Year=" + oUserLoginDetail.DT_STARTDATE.Year.ToString();
        URL += "&YARN_CODE=" + txtYarn.SelectedText.ToString();
        URL += "&Comp_Code=" + oUserLoginDetail.COMP_CODE;
        URL += "&Branch_Code=" + oUserLoginDetail.CH_BRANCHCODE;
        URL += "&Sdate=" + Sdate;
        URL += "&Edate=" + Edate;
        Response.Redirect(URL);
    }
}