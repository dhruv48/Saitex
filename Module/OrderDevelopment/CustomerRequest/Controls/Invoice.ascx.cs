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

public partial class Module_OrderDevelopment_CustomerRequest_Controls_Invoice : System.Web.UI.UserControl
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
            bindInvoiceType();
            yearRecipe();
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = (Sdate.ToShortDateString()).ToString();
            TxtToDate.Text = (Edate.ToShortDateString()).ToString();
            cmbInvoiceNo.SelectedIndex = -1;
            cmbChallanNo.SelectedIndex = -1;
            txtYarn.SelectedIndex = -1;
            cmbShade.SelectedIndex = -1;
            cmbPartyCode.SelectedIndex = -1;
         
           // cmbGreyLotNo.SelectedIndex = -1;
            
            btnGetReport_Click();

        }
        catch
        {
            throw;
        }

    }

    private void bindInvoiceType()
    {
        try
        {

            DataTable dt = new DataTable();


            dt = SaitexBL.Interface.Method.YRN_INT_MST.SelectinvoiceType(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            ddlInvoiceType.DataSource = dt;
            ddlInvoiceType.DataValueField = "INVOICE_TYPE";
            ddlInvoiceType.DataTextField = "INVOICE_TYPE";
            ddlInvoiceType.DataBind();
            ddlInvoiceType.Items.Insert(0, "");
            ddlInvoiceType.SelectedIndex = -1;
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
            dt = SaitexBL.Interface.Method.YRN_INT_MST.yearRecipe(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
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



    protected void cmbInvoiceNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetInvoiceNo(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbInvoiceNo.Items.Clear();
                cmbInvoiceNo.DataSource = data;
                cmbInvoiceNo.DataBind();


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

    private DataTable GetInvoiceNo(string Text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;



            CommandText = " SELECT   INVOICE_NUMB, TINVOICE_NUMB, YEAR  FROM   (  SELECT   DISTINCT M.INVOICE_NUMB, M.INVOICE_NUMB AS TINVOICE_NUMB, M.YEAR              FROM   TX_INVOICE_MST  M             WHERE   M.INVOICE_NUMB LIKE :SearchQuery          ORDER BY   M.INVOICE_NUMB DESC) asd WHERE   INVOICE_NUMB = TINVOICE_NUMB  AND ROWNUM<=15"; 


            string whereClause = string.Empty;

            if (startOffset != 0)
            {

                whereClause += " AND INVOICE_NUMB NOT IN (  SELECT   DISTINCT M.INVOICE_NUMB, M.INVOICE_NUMB AS TINVOICE_NUMB, M.YEAR              FROM   TX_INVOICE_MST  M             WHERE   M.INVOICE_NUMB LIKE :SearchQuery          ORDER BY   M.INVOICE_NUMB DESC)asd where  INVOICE_NUMB= INVOICE_NUMB   AND ROWNUM <= " + startOffset + ")";
                
            }
            string SortExpression;
            if (ddlYear.Text != "")
            {

                SortExpression = " and YEAR='" + ddlYear.Text + "' ORDER BY INVOICE_NUMB DESC  ";
            }
            else
            {
                SortExpression = " ORDER BY INVOICE_NUMB Desc";
            }
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
            //e.ItemsCount = GetIYarnCount(e.Text);
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
            CommandText = "   SELECT   DISTINCT T.YARN_CODE, T.YARN_DESC    FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T   WHERE       M.COMP_CODE = T.COMP_CODE           AND M.BRANCH_CODE = T.BRANCH_CODE           AND M.YEAR = T.YEAR           AND M.INVOICE_NUMB = T.INVOICE_NUMB           AND M.INVOICE_TYPE = T.INVOICE_TYPE           AND T.YARN_CODE LIKE :SearchQuery           AND T.YARN_DESC LIKE :SearchQuery  ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY   T.YARN_CODE DESC  ";
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
            string CommandText = " SELECT   YARN_CODE,YARN_DESC FROM   ( SELECT   DISTINCT T.YARN_CODE, T.YARN_DESC     FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T   WHERE M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR AND M.INVOICE_NUMB = T.INVOICE_NUMB AND M.INVOICE_TYPE = T.INVOICE_TYPE AND T.YARN_CODE LIKE :SearchQuery AND T.YARN_DESC LIKE :SearchQuery ORDER BY   T.YARN_CODE DESC) asd   WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN ( SELECT   DISTINCT T.YARN_CODE, T.YARN_DESC FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T   WHERE       M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR  AND M.INVOICE_NUMB = T.INVOICE_NUMB AND M.INVOICE_TYPE = T.INVOICE_TYPE AND T.YARN_CODE LIKE :SearchQuery  AND T.YARN_DESC LIKE :SearchQuery ORDER BY   T.YARN_CODE DESC)asd where   ROWNUM <= " + startOffset + ")";
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





    protected void txtChallanNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetChallanNO(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbChallanNo.Items.Clear();
                cmbChallanNo.DataSource = data;
                cmbChallanNo.DataBind();
            }
            //  Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
           // e.ItemsCount = GetIYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
        }
    }



    private DataTable GetChallanNO(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   CHALLAN_NO, YEAR    FROM   (  SELECT   DISTINCT M.CHALLAN_NO, M.YEAR FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T WHERE  M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR AND M.INVOICE_NUMB = T.INVOICE_NUMB AND M.INVOICE_TYPE = T.INVOICE_TYPE  AND T.CHALLAN_NO LIKE :SearchQuery ORDER BY   M.CHALLAN_NO DESC) asd  ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND CHALLAN_NO NOT IN (  SELECT   DISTINCT M.CHALLAN_NO, M.YEAR FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T WHERE  M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR AND M.INVOICE_NUMB = T.INVOICE_NUMB AND M.INVOICE_TYPE = T.INVOICE_TYPE  AND T.CHALLAN_NO LIKE :SearchQuery ORDER BY   M.CHALLAN_NO DESC)asd where   ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY CHALLAN_NO ASC";
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
            string CommandText = "  SELECT   PRTY_CODE, PRTY_NAME    FROM   (  SELECT   DISTINCT M.PRTY_CODE, V.PRTY_NAME  FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T, Tx_VENDOR_MST V  WHERE  M.COMP_CODE = T.COMP_CODE   AND M.BRANCH_CODE = T.BRANCH_CODE   AND M.YEAR = T.YEAR   AND M.PRTY_CODE = V.PRTY_CODE   AND M.INVOICE_NUMB = T.INVOICE_NUMB  AND M.INVOICE_TYPE = T.INVOICE_TYPE   AND V.PRTY_CODE LIKE :SearchQuery    AND V.PRTY_NAME LIKE :SearchQuery  ORDER BY   M.PRTY_CODE DESC) asd ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND M.PRTY_CODE NOT IN (  SELECT   DISTINCT M.PRTY_CODE, V.PRTY_NAME  FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T, Tx_VENDOR_MST V  WHERE  M.COMP_CODE = T.COMP_CODE   AND M.BRANCH_CODE = T.BRANCH_CODE   AND M.YEAR = T.YEAR   AND M.PRTY_CODE = V.PRTY_CODE   AND M.INVOICE_NUMB = T.INVOICE_NUMB  AND M.INVOICE_TYPE = T.INVOICE_TYPE   AND V.PRTY_CODE LIKE :SearchQuery    AND V.PRTY_NAME LIKE :SearchQuery  ORDER BY   M.PRTY_CODE DESC) asd where  ROWNUM <= " + startOffset + ")";
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


    //protected void cmbGREY_LOT_NO_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = GetGreyLotNo(e.Text.ToUpper(), e.ItemsOffset);
    //        // Looping through the items and adding them to the "Items" collection of the ComboBox
    //        if (data != null && data.Rows.Count > 0)
    //        {
    //            cmbGreyLotNo.Items.Clear();
    //            cmbGreyLotNo.DataSource = data;
    //            cmbGreyLotNo.DataBind();
    //        }
    //        // Calculating the numbr of items loaded so far in the ComboBox
    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    //        // Getting the total number of items that start with the typed text
    //        e.ItemsCount = GetGreyCount(e.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gray Lot No loading.\r\nSee error log for detail."));
    //    }
    //}


    //private DataTable GetGreyLotNo(string Text, int startOffset)
    //{
    //    try
    //    {
    //        string CommandText = " SELECT GREY_LOT_NO, PRTY_CODE, PRTY_NAME FROM ( select *  from(SELECT distinct M.GREY_LOT_NO, V.PRTY_CODE, V.PRTY_NAME FROM ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T , TX_MASTER_TRN L, TX_VENDOR_MST V WHERE M.COMP_CODE=T.COMP_CODE AND M.BRANCH_CODE=T.BRANCH_CODE AND M.GREY_LOT_NO = L.MST_CODE    AND L.CODE_PREFIX = V.PRTY_CODE) where PRTY_NAME  LIKE :SearchQuery or GREY_LOT_NO LIKE :SearchQuery     ORDER BY   GREY_LOT_NO asc)asd where ROWNUM <=15";
    //        string whereClause = string.Empty;
    //        if (startOffset != 0)
    //        {
    //            whereClause += " AND GREY_LOT_NO NOT IN (select * from(SELECT distinct M.GREY_LOT_NO, V.PRTY_CODE, V.PRTY_NAME FROM ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T , TX_MASTER_TRN L, TX_VENDOR_MST V WHERE  M.COMP_CODE=T.COMP_CODE AND M.BRANCH_CODE=T.BRANCH_CODE AND M.GREY_LOT_NO = L.MST_CODE    AND L.CODE_PREFIX = V.PRTY_CODE) where PRTY_NAME  LIKE :SearchQuery or GREY_LOT_NO LIKE :SearchQuery     ORDER BY   GREY_LOT_NO asc)asd where   AND ROWNUM <= " + startOffset + ")";
    //        }

    //        string SortExpression = " ORDER BY GREY_LOT_NO ASC";
    //        string SearchQuery = Text + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}


    //protected int GetGreyCount(string text)
    //{
    //    try
    //    {
    //        string CommandText = string.Empty;
    //        CommandText = " SELECT distinct M.GREY_LOT_NO, V.PRTY_CODE, V.PRTY_NAME FROM ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T , TX_MASTER_TRN L, TX_VENDOR_MST V WHERE M.COMP_CODE=T.COMP_CODE AND M.BRANCH_CODE=T.BRANCH_CODE AND M.GREY_LOT_NO = L.MST_CODE    AND L.CODE_PREFIX = V.PRTY_CODE AND M.COMP_CODE=:COMP_CODE AND M.BRANCH_CODE=:BRANCH_CODE AND M.GREY_LOT_NO LIKE :SearchQuery ";
    //        string WhereClause = " ";
    //        string SortExpression = " ORDER BY   M.GREY_LOT_NO ASC  ";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
    //        return data.Rows.Count;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}


    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShade(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            //e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            //// Getting the total number of items that start with the typed text
            //e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gray Lot No loading.\r\nSee error log for detail."));
        }
    }


    private DataTable GetShade(string Text, int startOffset)
    {
        try
        {
            string CommandText = "  SELECT   SHADE_CODE, SHADE_FAMILY_CODE    FROM   (  SELECT   DISTINCT T.SHADE_CODE, S.SHADE_FAMILY_CODE  FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T, OD_SHADE_FAMILY_TRN S WHERE       M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR AND T.COMP_CODE = S.COMP_CODE AND T.SHADE_CODE = S.SHADE_CODE  AND M.INVOICE_NUMB = T.INVOICE_NUMB  AND M.INVOICE_TYPE = T.INVOICE_TYPE  AND T.SHADE_CODE LIKE :SearchQuery AND S.SHADE_FAMILY_CODE LIKE :SearchQuery ORDER BY   T.SHADE_CODE DESC) asd  ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND SHADE_CODE NOT IN (SELECT   DISTINCT T.SHADE_CODE, S.SHADE_FAMILY_CODE  FROM   TX_INVOICE_MST M, TX_INVOICE_TRN T, OD_SHADE_FAMILY_TRN S WHERE       M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR AND T.COMP_CODE = S.COMP_CODE AND T.SHADE_CODE = S.SHADE_CODE  AND M.INVOICE_NUMB = T.INVOICE_NUMB  AND M.INVOICE_TYPE = T.INVOICE_TYPE  AND T.SHADE_CODE LIKE :SearchQuery AND S.SHADE_FAMILY_CODE LIKE :SearchQuery ORDER BY   T.SHADE_CODE DESC)asd where   ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY SHADE_CODE ASC";
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

                Label lblAmount = (Label)grdRow.FindControl("lblAmount");

                Label lblFreight = (Label)grdRow.FindControl("lblFreight");
                Label lblOtherC = (Label)grdRow.FindControl("lblOtherC");
                Label lblCGSTAmount = (Label)grdRow.FindControl("lblCGSTAmount");
                Label lblSGSTAmount = (Label)grdRow.FindControl("lblCGSTAmount");
                Label lblIGSTAmount = (Label)grdRow.FindControl("lblIGSTAmount");
                double Grand = double.Parse(lblAmount.Text.ToString()) + double.Parse(lblFreight.Text.ToString()) + double.Parse(lblOtherC.Text.ToString()) + double.Parse(lblCGSTAmount.Text.ToString()) + double.Parse(lblSGSTAmount.Text.ToString()) + double.Parse(lblIGSTAmount.Text.ToString());

               Label lblGrandAmount=  (Label)grdRow.FindControl("lblGrandAmount") ;
               Grand = Math.Round(Grand, 2);
               lblGrandAmount.Text = Grand.ToString();


                //Label lrno = (Label)grdRow.FindControl("lblLRNO");
                //Label lroption = (Label)grdRow.FindControl("lblOption");
                //Label lblCNo = (Label)grdRow.FindControl("lblCNo");

                //DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForRecipeGriedSub(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, lrno.Text.Trim(), lroption.Text.Trim(), lblCNo.Text.Trim());


                //if (data != null && data.Rows.Count > 0)
                //{
                //    DataView dv = new DataView(data);
                //    dv.RowFilter = "LAB_DIP_NO='" + lrno.Text + "' and LR_OPTION='" + lroption.Text + "' ";
                //    GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
                //    grdPOTRN.DataSource = dv;
                //    grdPOTRN.DataBind();
                //}
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

        DataTable data = SaitexBL.Interface.Method.YRN_INT_MST.GetDataForInvoiceGried(ddlInvoiceType.Text,ddlYear.Text, cmbPartyCode.SelectedText.ToString(), txtYarn.SelectedText.ToString(), cmbShade.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Sdate.ToShortDateString(), Edate.ToShortDateString(), cmbInvoiceNo.SelectedText.ToString(), cmbChallanNo.SelectedText.ToString());


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

        DataTable data = SaitexBL.Interface.Method.YRN_INT_MST.GetDataForInvoiceGried(ddlInvoiceType.Text, ddlYear.Text, cmbPartyCode.SelectedText.ToString(), txtYarn.SelectedText.ToString(), cmbShade.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Sdate.ToShortDateString(), Edate.ToShortDateString(), cmbInvoiceNo.SelectedText.ToString(), cmbChallanNo.SelectedText.ToString());


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


        Sdate = DateTime.Parse(TxtFromDate.Text);
        Edate = DateTime.Parse(TxtToDate.Text);

        DataTable data = SaitexBL.Interface.Method.YRN_INT_MST.GetDataForInvoiceGried(ddlInvoiceType.Text, ddlYear.Text, cmbPartyCode.SelectedText.ToString(), txtYarn.SelectedText.ToString(), cmbShade.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Sdate.ToShortDateString(), Edate.ToShortDateString(), cmbInvoiceNo.SelectedText.ToString(), cmbChallanNo.SelectedText.ToString());


        string strFilename = "Invoice_Query_Form_" + DateTime.Now.ToShortDateString() + ".xls";


        ExporttoExcel(data, strFilename, "Invoice Query From");
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

        //HttpContext.Current.Response.Write("<Td align='center' valing='top'>");
        //HttpContext.Current.Response.Write("<B>");
        //HttpContext.Current.Response.Write("Recipe Details");
        //HttpContext.Current.Response.Write("</B>");
        //HttpContext.Current.Response.Write("</Td>");

        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                HttpContext.Current.Response.Write(row[i].ToString());
                //******************************************//
          //      if (i == 15)
          //      {

          //          HttpContext.Current.Response.Write("<Td align=left valign=Top>");

          //          DataView dvBASEQUALITY = new DataView(dtBASEQUALITY);
          //          dvBASEQUALITY.RowFilter = "CUSTOMER_REQ_NO='" + row["CUSTOMER_REQ_NO"].ToString() + "' and LAB_DIP_NO='" + row["LAB_DIP_NO"].ToString() + "' AND LR_OPTION='" + row["LR_OPTION"].ToString() + "'";

          //          if (dvBASEQUALITY.Count > 0)
          //          {
          //              HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          //"borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          //"style='font-size:10.0pt; font-family:Calibri; background:white;'>");
          //              HttpContext.Current.Response.Write("<TR>");

          //              foreach (DataColumn dtcol in dtBASEQUALITY.Columns)
          //              {
          //                  HttpContext.Current.Response.Write("<Td bgcolor=silver>");
          //                  HttpContext.Current.Response.Write("<B>");
          //                  HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
          //                  HttpContext.Current.Response.Write("</B>");
          //                  HttpContext.Current.Response.Write("</Td>");

          //              }
          //              HttpContext.Current.Response.Write("</TR>");
          //              for (int j = 0; j < dvBASEQUALITY.Count; j++)
          //              {
          //                  HttpContext.Current.Response.Write("<Tr>");
          //                  for (int i1 = 0; i1 < dvBASEQUALITY.Table.Columns.Count; i1++)
          //                  {
          //                      HttpContext.Current.Response.Write("<Td >");
          //                      HttpContext.Current.Response.Write(dvBASEQUALITY[j][i1].ToString());
          //                      HttpContext.Current.Response.Write("</Td>");

          //                  }
          //                  HttpContext.Current.Response.Write("</Tr>");
          //              }
          //              HttpContext.Current.Response.Write("</Table>");
          //          }
          //          HttpContext.Current.Response.Write("</Td>");

          //      }



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
