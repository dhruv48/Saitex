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

public partial class Module_OrderDevelopment_Controls_Production_Planning_Query : System.Web.UI.UserControl
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
            //bindBusinessType();
            yearRecipe();
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = (Sdate.ToShortDateString()).ToString();
            TxtToDate.Text = (Edate.ToShortDateString()).ToString();
            cmbCustomer.SelectedIndex = -1;
            txtYarn.SelectedIndex = -1;
            cmbPartyCode.SelectedIndex = -1;
            //cmbGreyLotNo.SelectedIndex = -1;
            //cmbShade.SelectedIndex = -1;
            
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
            dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.yearRecipe(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            ddlYear.DataSource = dt;
            //ddlYear.Items.Add("----");
            ddlYear.DataValueField = "YEAR";
            ddlYear.DataTextField = "YEAR";
            ddlYear.DataBind();
           // ddlYear.Items.Insert(0, DateTime.Parse(TxtFromDate.Text).Year.ToString());
           
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

    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        ddlYear.Items.Add((DateTime.Parse(TxtFromDate.Text).Year).ToString());
        ddlYear.Text = (DateTime.Parse(TxtFromDate.Text).Year).ToString();
        //try
        //{
        //    if (TxtFromDate.Text.Trim() != string.Empty)
        //    {
        //        DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());

        //        if (StartDate >= Sdate && StartDate <= Edate)
        //        {
        //            Common.CommonFuction.ShowMessage("Date is fine .");
        //        }
        //        else
        //        {
        //            Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
        //            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();

        //        }

        //    }
        //}
        //catch (Exception ex)
        //{
        //    errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
        //    throw ex;
        //}
    }



    protected void cmbCustomer_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetCustomer(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbCustomer.Items.Clear();
                cmbCustomer.DataSource = data;
                cmbCustomer.DataBind();


            }
            // Calculating the numbr of items loaded so far in the ComboBox
            //e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            //// Getting the total number of items that start with the typed text
            //e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Customer Request NO loading.\r\nSee error log for detail."));
        }
    }

    private DataTable GetCustomer(string Text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;


            CommandText = " SELECT   ORDER_NO, TORDER_NO ,CUST_REQ_NO  FROM   (  SELECT   DISTINCT M.ORDER_NO, T.ORDER_NO AS TORDER_NO,M.CUST_REQ_NO  FROM   OD_CAPT_TRN_MAIN M, OD_CAPT_MST T   WHERE M.YEAR='"+(DateTime.Parse(TxtFromDate.Text).Year).ToString()+"' and  M.ORDER_NO LIKE :SearchQuery AND M.CUST_REQ_NO LIKE :SearchQuery ORDER BY   M.ORDER_NO ASC) asd WHERE   ORDER_NO = TORDER_NO and ROWNUM<=15 ";
           
            string whereClause = string.Empty;

            if (startOffset != 0)
            {

                whereClause += " AND ORDER_NO NOT IN  (SELECT   ORDER_NO  FROM   (  SELECT   DISTINCT  M.ORDER_NO,  T.ORDER_NO AS TORDER_NO,M.CUST_REQ_NO FROM   OD_CAPT_TRN_MAIN M, OD_CAPT_MST T  WHERE M.YEAR='" + (DateTime.Parse(TxtFromDate.Text).Year).ToString() + "' and  M.ORDER_NO LIKE :SearchQuery AND M.CUST_REQ_NO LIKE :SearchQuery ORDER BY   M.ORDER_NO ASC) asd  WHERE   ORDER_NO = TORDER_NO AND ROWNUM <=  " + startOffset + ")";

               
            }



            string SortExpression = "  ORDER BY ORDER_NO ASC  ";
            
                        string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
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
            e.ItemsCount = GetIYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
        }
    }


    protected int GetIYarnCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = "  SELECT   DISTINCT M.ARTICAL_NO    FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T   WHERE       M.ORDER_NO = T.ORDER_NO           AND M.COMP_CODE = T.COMP_CODE           AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.COMP_CODE=:COMP_CODE AND M.BRANCH_CODE=:BRANCH_CODE  AND M.ARTICAL_NO LIKE :SearchQuery ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY   M.ARTICAL_NO ASC  ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,oUserLoginDetail.DT_STARTDATE.Year);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }


    private DataTable GetYCODE(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   DISTINCT ARTICAL_CODE, YARN_DESC, ASS_YARN_DESC, ASS_YARN_CODE ,YEAR FROM   (  SELECT   DISTINCT M.ARTICAL_CODE, Y.YARN_DESC, Y.ASS_YARN_DESC,  Y.ASS_YARN_CODE ,M.YEAR FROM   OD_CAPT_TRN_MAIN M, OD_CAPT_MST T, YARN_MST Y  WHERE       M.ORDER_NO = T.ORDER_NO  AND M.YEAR = T.YEAR  AND M.ORDER_TYPE = T.ORDER_TYPE  AND M.ORDER_CAT = T.ORDER_CAT  AND M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.ARTICAL_CODE = Y.ASS_YARN_CODE  AND M.ARTICAL_CODE LIKE :SearchQuery  OR Y.YARN_DESC LIKE :SearchQuery  OR Y.ASS_YARN_DESC LIKE :SearchQuery  ORDER BY   M.ARTICAL_CODE ASC) asd WHERE   ARTICAL_CODE = ASS_YARN_CODE AND YEAR=" + DateTime.Parse(TxtFromDate.Text).Year + " AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND ARTICAL_CODE NOT IN  (SELECT   DISTINCT ARTICAL_CODE  FROM   (   SELECT   DISTINCT M.ARTICAL_CODE, Y.YARN_DESC, Y.ASS_YARN_DESC,  Y.ASS_YARN_CODE ,M.YEAR FROM   OD_CAPT_TRN_MAIN M, OD_CAPT_MST T, YARN_MST Y  WHERE       M.ORDER_NO = T.ORDER_NO  AND M.YEAR = T.YEAR  AND M.ORDER_TYPE = T.ORDER_TYPE  AND M.ORDER_CAT = T.ORDER_CAT  AND M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.ARTICAL_CODE = Y.ASS_YARN_CODE  AND M.ARTICAL_CODE LIKE :SearchQuery  OR Y.YARN_DESC LIKE :SearchQuery  OR Y.ASS_YARN_DESC LIKE :SearchQuery  ORDER BY   M.ARTICAL_CODE ASC) asd  WHERE   ARTICAL_CODE = ASS_YARN_CODE AND YEAR=" + DateTime.Parse(TxtFromDate.Text).Year + " AND ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY ARTICAL_CODE ASC";
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


            DataTable data = GetBranchPartyData(e.Text.ToUpper(), e.ItemsOffset);

            cmbPartyCode.Items.Clear();

            cmbPartyCode.DataSource = data;
            cmbPartyCode.DataBind();

            //e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            //e.ItemsCount = GetBranchPartyCount(e.Text);


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            // lblMode.Text = ex.ToString();
        }
    }



    private DataTable GetBranchPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   DISTINCT PRTY_CODE, PARTY_CODE, PRTY_NAME,YEAR  FROM   (  SELECT   DISTINCT T.PRTY_CODE, V.PRTY_CODE AS PARTY_CODE, V.PRTY_NAME,M.YEAR FROM   OD_CAPT_TRN_MAIN M, OD_CAPT_MST T, TX_VENDOR_MST V  WHERE M.ORDER_NO = T.ORDER_NO  AND M.YEAR = T.YEAR  AND M.ORDER_TYPE = T.ORDER_TYPE  AND M.ORDER_CAT = T.ORDER_CAT  AND M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE AND T.PRTY_CODE = V.PRTY_CODE  AND T.PRTY_CODE LIKE :SearchQuery   OR V.PRTY_NAME LIKE :SearchQuery  ORDER BY   M.YEAR DESC) asd WHERE   PRTY_CODE = PARTY_CODE AND YEAR=" + DateTime.Parse(TxtFromDate.Text).Year + " AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND PRTY_CODE NOT IN (SELECT   DISTINCT PRTY_CODE ,YEAR FROM   (  SELECT   DISTINCT T.PRTY_CODE, V.PRTY_CODE AS PARTY_CODE, V.PRTY_NAME ,M.YEAR FROM   OD_CAPT_TRN_MAIN M,  OD_CAPT_MST T, TX_VENDOR_MST V  WHERE       M.ORDER_NO = T.ORDER_NO   AND M.YEAR = T.YEAR  AND M.ORDER_TYPE = T.ORDER_TYPE  AND M.ORDER_CAT = T.ORDER_CAT  AND M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE  AND T.PRTY_CODE = V.PRTY_CODE   AND T.PRTY_CODE LIKE :SearchQuery   OR V.PRTY_NAME LIKE :SearchQuery  ORDER BY   M.YEAR DESC) asd   WHERE   PRTY_CODE = PARTY_CODE AND YEAR=" + DateTime.Parse(TxtFromDate.Text).Year + " AND ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }


   

    protected void grdLabDipSubmission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;


                Label lblOrderNo = (Label)grdRow.FindControl("lblOrderNo");
                Label lblPI_NO = (Label)grdRow.FindControl("lblPI_NO");
                Label lblShade_Code = (Label)grdRow.FindControl("lblShade_Code");
                Label lblYear = (Label)grdRow.FindControl("lblYear");
                DataTable data = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningBomGriedSub(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,ddlYear.SelectedValue);


                if (data != null && data.Rows.Count > 0)
                {
                    DataView dv = new DataView(data);
                    dv.RowFilter = "ORDER_NO='" + lblOrderNo.Text + "' and PI_NO='" + lblPI_NO.Text + "' and YEAR='"+lblYear.Text+"' ";
                    GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
                    grdPOTRN.DataSource = dv;
                    grdPOTRN.DataBind();
                }


                DataTable dataMAC = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningMACGriedSub(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlYear.Text);



                if (dataMAC != null && dataMAC.Rows.Count > 0)
                {
                    DataView MACdv = new DataView(dataMAC);
                    MACdv.RowFilter = "ORDER_NO='" + lblOrderNo.Text + "' and PI_NO='" + lblPI_NO.Text + "'  and YEAR='" + lblYear.Text + "' ";
                    GridView grdPOTRNMAC = (GridView)grdRow.FindControl("grdPOTRNMAC");
                    grdPOTRNMAC.DataSource = MACdv;
                    grdPOTRNMAC.DataBind();
                }




                DataTable dataRoute = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningROUTEGriedSub(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlYear.Text);

                if (dataRoute != null && dataRoute.Rows.Count > 0)
                {
                    DataView ROUTEdv = new DataView(dataRoute);
                    ROUTEdv.RowFilter = "ORDER_NO='" + lblOrderNo.Text + "' and PI_NO='" + lblPI_NO.Text + "'  and YEAR='" + lblYear.Text + "' ";
                    GridView grdPOTRNROUTE = (GridView)grdRow.FindControl("grdPOTRNROUTE");
                    grdPOTRNROUTE.DataSource = ROUTEdv;
                    grdPOTRNROUTE.DataBind();
                }



                DataTable dataOD = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningODGriedSub(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlYear.Text);

                if (dataOD != null && dataOD.Rows.Count > 0)
                {
                    DataView ODdv = new DataView(dataOD);
                    ODdv.RowFilter = "PI_NO='" + lblPI_NO.Text + "' and YEAR='" + lblYear.Text + "' ";
                    GridView grdPOOD = (GridView)grdRow.FindControl("grdPOOD");
                    grdPOOD.DataSource = ODdv;
                    grdPOOD.DataBind();
                }



            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));

        }
    }






    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        Sdate = DateTime.Parse(TxtFromDate.Text);
        Edate = DateTime.Parse(TxtToDate.Text);

        DataTable data = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningGried(ddlYear.Text, cmbCustomer.SelectedText.ToString(), txtYarn.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Sdate, Edate, cmbPartyCode.SelectedText.ToString());



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

        DataTable data = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningGried(ddlYear.Text, cmbCustomer.SelectedText.ToString(), txtYarn.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Sdate, Edate, cmbPartyCode.SelectedText.ToString());


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
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Production_planning_Query_From_" + DateTime.Now.ToShortDateString() + ".xls";

        Sdate = DateTime.Parse(TxtFromDate.Text);
        Edate = DateTime.Parse(TxtToDate.Text);

        DataTable table = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningGried(ddlYear.Text, cmbCustomer.SelectedText.ToString(), txtYarn.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Sdate, Edate, cmbPartyCode.SelectedText.ToString());
        DataTable dataBom = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningBomGriedSubExcel(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlYear.SelectedValue);

        DataTable dataMAC = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningMACGriedSubExcel(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlYear.Text);

        DataTable dataRoute = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningROUTEGriedSubExcel(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlYear.Text);

        ExporttoExcel(table, strFilename, "Production Planning Query Form", dataBom, dataMAC, dataRoute);
    }



    private void ExporttoExcel(DataTable table, string name, string title, DataTable dataBom, DataTable dataMAC, DataTable dataRoute)
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

        HttpContext.Current.Response.Write("<Td align='center' valing='top'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("BOM Details");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");



        HttpContext.Current.Response.Write("<Td align='center' valing='top'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("Machine Details");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");


        HttpContext.Current.Response.Write("<Td align='center' valing='top'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("Route Details");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");

        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                HttpContext.Current.Response.Write(row[i].ToString());
                //******************************************//
                if (i == 23)
                {
                    //// Bom////
                    HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                    DataView dvBOM = new DataView(dataBom);
                    dvBOM.RowFilter = "ORDER_NO='" + row["ORDER_NO"].ToString() + "' and PI_NO='" + row["PI_NO"].ToString() + "' AND YEAR='" + row["YEAR"].ToString() + "'";

                    if (dvBOM.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<TR>");

                        foreach (DataColumn dtcol in dataBom.Columns)
                        {
                            HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");

                        }
                        HttpContext.Current.Response.Write("</TR>");
                        for (int j = 0; j < dvBOM.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvBOM.Table.Columns.Count; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td >");
                                HttpContext.Current.Response.Write(dvBOM[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                    HttpContext.Current.Response.Write("</Td>");

                }
                if (i == 23)
                {
                    //// Bom////
                    HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                    DataView dvMAC = new DataView(dataMAC);
                    dvMAC.RowFilter = "ORDER_NO='" + row["ORDER_NO"].ToString() + "' and PI_NO='" + row["PI_NO"].ToString() + "' AND YEAR='" + row["YEAR"].ToString() + "'";

                    if (dvMAC.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<TR>");

                        foreach (DataColumn dtcol in dataMAC.Columns)
                        {
                            HttpContext.Current.Response.Write("<Td bgcolor=#f4f142>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");

                        }
                        HttpContext.Current.Response.Write("</TR>");
                        for (int j = 0; j < dvMAC.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvMAC.Table.Columns.Count; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td >");
                                HttpContext.Current.Response.Write(dvMAC[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                    HttpContext.Current.Response.Write("</Td>");

                }








                if (i == 23)
                {
                    //// Bom////
                    HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                    DataView dvROUTE = new DataView(dataRoute);
                    dvROUTE.RowFilter = "ORDER_NO='" + row["ORDER_NO"].ToString() + "' and PI_NO='" + row["PI_NO"].ToString() + "' AND YEAR='" + row["YEAR"].ToString() + "'";

                    if (dvROUTE.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<TR>");

                        foreach (DataColumn dtcol in dataRoute.Columns)
                        {
                            HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");

                        }
                        HttpContext.Current.Response.Write("</TR>");
                        for (int j = 0; j < dvROUTE.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvROUTE.Table.Columns.Count; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td >");
                                HttpContext.Current.Response.Write(dvROUTE[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                    HttpContext.Current.Response.Write("</Td>");

                }


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



}
