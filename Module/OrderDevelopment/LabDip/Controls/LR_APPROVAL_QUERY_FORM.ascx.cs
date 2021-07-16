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

using errorLog;
using System.IO;
using DBLibrary;
using Obout.Grid;

public partial class Module_OrderDevelopment_LabDip_Controls_LR_APPROVAL_QUERY_FORM : System.Web.UI.UserControl
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
            txtYarn.SelectedIndex = -1;
            cmbGreyLotNo.SelectedIndex = -1;
            cmbShade.SelectedIndex = -1;
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
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
        }
    }

    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = "SELECT   ARTICAL_NO,           ARTICAL_DESC  FROM   (  SELECT   DISTINCT M.ARTICAL_NO,   M.ARTICAL_DESC FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T   WHERE   M.ORDER_NO = T.ORDER_NO  AND M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR AND IS_APPROVED = 1 and M.COMP_CODE=:COMP_CODE and M.BRANCH_CODE=:BRANCH_CODE  ORDER BY   M.ARTICAL_NO ASC) asd   WHERE     ARTICAL_NO LIKE :SearchQuery  OR ARTICAL_DESC LIKE :SearchQuery ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY   ARTICAL_NO ASC ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsGreyCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = "SELECT distinct M.GREY_LOT_NO , V.PRTY_CODE, V.PRTY_NAME FROM ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T , TX_MASTER_TRN L, TX_VENDOR_MST V WHERE    M.IS_APPROVED = 1   AND L.MST_NAME = 'GREY_LOT_NO'           AND M.ORDER_NO = T.ORDER_NO           AND M.BRANCH_CODE = T.BRANCH_CODE    AND M.YEAR = T.YEAR  AND M.GREY_LOT_NO = L.MST_CODE AND M.COMP_CODE=:COMP_CODE AND M.BRANCH_CODE=:BRANCH_CODE   AND L.CODE_PREFIX = V.PRTY_CODE(+) AND M.GREY_LOT_NO LIKE :SearchQuery    ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY   M.GREY_LOT_NO asc ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
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
            string whereClause = string.Empty;
            string CommandText = " SELECT   *    FROM   (SELECT   ARTICAL_NO, ARTICAL_DESC              FROM   (  SELECT   DISTINCT M.ARTICAL_NO, M.ARTICAL_DESC                          FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T                         WHERE       M.ORDER_NO = T.ORDER_NO                                 AND M.COMP_CODE = T.COMP_CODE                                 AND M.BRANCH_CODE = T.BRANCH_CODE                                 AND M.YEAR = T.YEAR                                 AND IS_APPROVED = 1                      ORDER BY   M.ARTICAL_NO ASC) asd             WHERE   ARTICAL_NO LIKE :SearchQuery                     OR ARTICAL_DESC LIKE :SearchQuery) bd   WHERE   ROWNUM <= 15 ";
           
            if (startOffset != 0)
            {
                whereClause += " AND ARTICAL_NO NOT IN(select ARTICAL_NO from ( SELECT   ARTICAL_NO,           ARTICAL_DESC  FROM   (  SELECT   DISTINCT M.ARTICAL_NO,   M.ARTICAL_DESC FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T   WHERE   M.ORDER_NO = T.ORDER_NO  AND M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE AND M.YEAR = T.YEAR AND IS_APPROVED = 1  ORDER BY   M.ARTICAL_NO ASC) asd   WHERE     ARTICAL_NO LIKE :SearchQuery  OR ARTICAL_DESC LIKE :SearchQuery )bd where  ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY ARTICAL_NO ASC";
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



    protected void cmbPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable data = GetBranchPartyData(e.Text.ToUpper(), e.ItemsOffset);

            cmbPartyCode.Items.Clear();

            cmbPartyCode.DataSource = data;
            cmbPartyCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetBranchPartyCount(e.Text);


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
            string CommandText = " SELECT  PRTY_CODE,PRTY_NAME,PRTY_ADD1,PARTY_CODE,ORDER_TYPE FROM( SELECT  distinct TV.PRTY_CODE, TV.PRTY_NAME,     TV.PRTY_ADD1, BR.PRTY_CODE AS PARTY_CODE, BR.ORDER_TYPE  FROM   TX_VENDOR_MST TV,    OD_CUSTOMER_REQUEST_MST BR  LEFT JOIN ST_LABDIP_SUB_MST M  ON BR.ORDER_NO = M.ORDER_NO               WHERE   M.IS_APPROVED = 1  AND BR.COMP_CODE = M.COMP_CODE           AND BR.BRANCH_CODE = M.BRANCH_CODE           AND BR.YEAR = M.YEAR           AND BR.PRTY_CODE = TV.PRTY_CODE  ORDER BY   TV.PRTY_CODE ASC )asd where PRTY_CODE=PARTY_CODE AND ORDER_TYPE='DEVELOPMENT'  AND PRTY_CODE LIKE :SearchQuery         OR PRTY_NAME LIKE :SearchQuery AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND TV.PRTY_CODE NOT IN ( SELECT  PRTY_CODE FROM( SELECT  distinct TV.PRTY_CODE, TV.PRTY_NAME,     TV.PRTY_ADD1, BR.PRTY_CODE AS PARTY_CODE, BR.ORDER_TYPE  FROM   TX_VENDOR_MST TV,    OD_CUSTOMER_REQUEST_MST BR  LEFT JOIN ST_LABDIP_SUB_MST M  ON BR.ORDER_NO = M.ORDER_NO               WHERE   M.IS_APPROVED = 1  AND BR.COMP_CODE = M.COMP_CODE           AND BR.BRANCH_CODE = M.BRANCH_CODE           AND BR.YEAR = M.YEAR           AND BR.PRTY_CODE = TV.PRTY_CODE  ORDER BY   TV.PRTY_CODE ASC )asd where PRTY_CODE=PARTY_CODE AND ORDER_TYPE='DEVELOPMENT'  AND PRTY_CODE LIKE :SearchQuery         OR PRTY_NAME LIKE :SearchQuery AND ROWNUM <= " + startOffset + ")";
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


    protected int GetBranchPartyCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = " SELECT   DISTINCT TV.PRTY_CODE    FROM   TX_VENDOR_MST TV,    OD_CUSTOMER_REQUEST_MST BR                             LEFT JOIN                                ST_LABDIP_SUB_MST M                             ON BR.ORDER_NO = M.ORDER_NO   WHERE       M.IS_APPROVED = 1           AND BR.COMP_CODE = M.COMP_CODE           AND BR.BRANCH_CODE = M.BRANCH_CODE           AND BR.YEAR = M.YEAR           AND BR.PRTY_CODE = TV.PRTY_CODE           AND BR.ORDER_TYPE = 'DEVELOPMENT'           AND BR.COMP_CODE = :COMP_CODE           AND BR.BRANCH_CODE = :BRANCH_CODE";
            string WhereClause = " ";
            string SortExpression = " ORDER BY   TV.PRTY_CODE ASC ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }


    protected void cmbGREY_LOT_NO_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetGreyLotNo(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbGreyLotNo.Items.Clear();
                cmbGreyLotNo.DataSource = data;
                cmbGreyLotNo.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsGreyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gray Lot No loading.\r\nSee error log for detail."));
        }
    }


    private DataTable GetGreyLotNo(string Text, int startOffset)
    
    {
        try
        {
            string CommandText = " SELECT GREY_LOT_NO,PRTY_CODE, PRTY_NAME FROM (SELECT distinct M.GREY_LOT_NO, V.PRTY_CODE, V.PRTY_NAME FROM ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T , TX_MASTER_TRN L, TX_VENDOR_MST V WHERE    M.IS_APPROVED = 1 AND M.ORDER_NO = T.ORDER_NO AND M.COMP_CODE=T.COMP_CODE AND M.BRANCH_CODE=T.BRANCH_CODE AND M.YEAR=T.YEAR  AND M.GREY_LOT_NO = L.MST_CODE    AND L.CODE_PREFIX = V.PRTY_CODE(+)  AND M.GREY_LOT_NO LIKE :SearchQuery   ORDER BY   M.GREY_LOT_NO asc)asd where  ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND GREY_LOT_NO NOT IN (  SELECT GREY_LOT_NO from (SELECT distinct M.GREY_LOT_NO, V.PRTY_CODE, V.PRTY_NAME FROM ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T , TX_MASTER_TRN L, TX_VENDOR_MST V WHERE    M.IS_APPROVED = 1 AND M.ORDER_NO = T.ORDER_NO AND M.COMP_CODE=T.COMP_CODE AND M.BRANCH_CODE=T.BRANCH_CODE AND M.YEAR=T.YEAR  AND M.GREY_LOT_NO = L.MST_CODE    AND L.CODE_PREFIX = V.PRTY_CODE(+) AND M.GREY_LOT_NO LIKE :SearchQuery   ORDER BY   M.GREY_LOT_NO asc)asd where  ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY GREY_LOT_NO ASC";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }



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
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsSahdeCount(e.Text);
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
            string CommandText = " SELECT SHADE_FAMILY_CODE,SHADE_CODE,ORDER_NO,TORDER_NO FROM (SELECT   DISTINCT M.SHADE_FAMILY_CODE, M.SHADE_CODE, M.ORDER_NO, T.ORDER_NO AS TORDER_NO   FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T  WHERE  M.IS_APPROVED = 1   AND  M.SHADE_FAMILY_CODE LIKE :SearchQuery     OR M.SHADE_CODE LIKE :SearchQuery  ORDER BY   M.SHADE_FAMILY_CODE ASC)asd where ORDER_NO=TORDER_NO  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND ORDER_NO NOT IN                    (SELECT   ORDER_NO                       FROM   (SELECT   DISTINCT M.SHADE_FAMILY_CODE,                                                 M.SHADE_CODE,                                                 M.ORDER_NO,                                                 T.ORDER_NO AS TORDER_NO                                 FROM   ST_LABDIP_SUB_MST M,                                        ST_LABDIP_SUB_TRN T                                WHERE       M.IS_APPROVED = 1                                        AND M.ORDER_NO = T.ORDER_NO                                        AND M.COMP_CODE = T.COMP_CODE                                        AND M.BRANCH_CODE = T.BRANCH_CODE                                        AND M.YEAR = T.YEAR                               AND SHADE_FAMILY_CODE LIKE :SearchQuery                              OR SHADE_CODE LIKE :SearchQuery) ak                      WHERE   ORDER_NO = TORDER_NO  AND ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ASC";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }


    protected int GetItemsSahdeCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = "SELECT   ORDER_NO  FROM   (SELECT   DISTINCT M.SHADE_FAMILY_CODE,  M.SHADE_CODE,   M.ORDER_NO,  T.ORDER_NO AS TORDER_NO    FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T  WHERE       M.IS_APPROVED = 1  AND M.ORDER_NO = T.ORDER_NO  AND M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR   AND M.COMP_CODE = :COMP_CODE   AND M.BRANCH_CODE = :BRANCH_CODE) asd WHERE   SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY  ORDER_NO ASC ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }




    protected void cmbCustReqNO_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {


        try
        {
            DataTable data = GetCustReqNo(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmdCustReqNo.Items.Clear();
                cmdCustReqNo.DataSource = data;
                cmdCustReqNo.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCustCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gray Lot No loading.\r\nSee error log for detail."));
        }



    }



    private DataTable GetCustReqNo(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT ORDER_NO, YEAR FROM (SELECT distinct M.ORDER_NO , M.YEAR FROM ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T  WHERE    M.IS_APPROVED = 1 AND M.ORDER_NO = T.ORDER_NO AND M.COMP_CODE=T.COMP_CODE AND M.BRANCH_CODE=T.BRANCH_CODE AND M.YEAR=T.YEAR     AND M.ORDER_NO LIKE :SearchQuery   ORDER BY   M.ORDER_NO asc)asd where  ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND ORDER_NO NOT IN (  SELECT ORDER_NO ,YEAR from (SELECT distinct M.ORDER_NO,M.YEAR FROM ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T WHERE    M.IS_APPROVED = 1 AND M.ORDER_NO = T.ORDER_NO   AND M.COMP_CODE=T.COMP_CODE AND M.BRANCH_CODE=T.BRANCH_CODE AND M.YEAR=T.YEAR   AND M.ORDER_NO LIKE :SearchQuery   ORDER BY   M.ORDER_NO asc)asd where  ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY ORDER_NO ASC";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }


    protected int GetItemsCustCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = "SELECT   ORDER_NO  FROM   (SELECT   DISTINCT    M.ORDER_NO   FROM   ST_LABDIP_SUB_MST M, ST_LABDIP_SUB_TRN T  WHERE       M.IS_APPROVED = 1  AND M.ORDER_NO = T.ORDER_NO  AND M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.YEAR = T.YEAR   AND M.COMP_CODE = :COMP_CODE   AND M.BRANCH_CODE = :BRANCH_CODE) asd WHERE   ORDER_NO LIKE :SearchQuery  ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY  ORDER_NO ASC ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            return data.Rows.Count;
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
              

                Label lrno = (Label)grdRow.FindControl("lblLRNO");
                Label lroption = (Label)grdRow.FindControl("lblOption");
                Label lblCNo = (Label)grdRow.FindControl("lblCNo");
                Label lblYear = (Label)grdRow.FindControl("lblYear");

                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForRecipeGriedSub(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, lrno.Text.Trim(), lroption.Text.Trim(), lblCNo.Text.Trim());


                if (data != null && data.Rows.Count > 0)
                {
                    DataView dv = new DataView(data);
                    dv.RowFilter = "LAB_DIP_NO='" + lrno.Text + "' and LR_OPTION='" + lroption.Text + "'and Year='" + lblYear .Text+ "' ";
                    GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
                    grdPOTRN.DataSource = dv;
                    grdPOTRN.DataBind();
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
        //Sdate = DateTime.Parse(TxtFromDate.Text);
        //Edate = DateTime.Parse(TxtToDate.Text);

        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForRecipeGriedApprove(txtYarn.SelectedText.ToString(), cmbGreyLotNo.SelectedText.ToString(), cmbShade.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, cmbPartyCode.SelectedText.ToString(),cmdCustReqNo.SelectedText.ToString(),ddlYear.Text);

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
        //Sdate = DateTime.Parse(TxtFromDate.Text);
        //Edate = DateTime.Parse(TxtToDate.Text);

        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForRecipeGriedApprove( txtYarn.SelectedText.ToString(), cmbGreyLotNo.SelectedText.ToString(), cmbShade.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, cmbPartyCode.SelectedText.ToString(),cmdCustReqNo.SelectedText.ToString() ,ddlYear.Text);


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
        string strFilename = "LR_Approval_Query_From_" + DateTime.Now.ToShortDateString() + ".xls";

        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForRecipeGriedApproveExcel(txtYarn.SelectedText.ToString(), cmbGreyLotNo.SelectedText.ToString(), cmbShade.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, cmbPartyCode.SelectedText.ToString(),cmdCustReqNo.SelectedText.ToString(),ddlYear.Text);

        DataTable Dyedata = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForRecipeGriedSubExcel(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "", "", "");
        ExporttoExcel(data, strFilename, "LR Approval Query From", Dyedata);
    }



    private void ExporttoExcel(DataTable table, string name, string title, DataTable dtBASEQUALITY)
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


        HttpContext.Current.Response.Write("<Td align=Center valign=Top>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("Recipe Details");
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
                if (i == 20)
                {

                    HttpContext.Current.Response.Write("<Td align=left valign=Top>");


                    DataView dvBASEQUALITY = new DataView(dtBASEQUALITY);
                    dvBASEQUALITY.RowFilter = "CUSTOMER_REQ_NO='" + row["CUSTOMER_REQ_NO"].ToString() + "'and LAB_DIP_NO='" + row["LAB_DIP_NO"].ToString() + "' and LR_OPTION='" + row["LR_OPTION"].ToString() + "' and YEAR='" + row["YEAR"].ToString() + "'";

                    if (dvBASEQUALITY.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<TR>");

                        foreach (DataColumn dtcol in dtBASEQUALITY.Columns)
                        {
                            HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                            HttpContext.Current.Response.Write("<B>");
                            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                            HttpContext.Current.Response.Write("</B>");
                            HttpContext.Current.Response.Write("</Td>");

                        }
                        HttpContext.Current.Response.Write("</TR>");
                        for (int j = 0; j < dvBASEQUALITY.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvBASEQUALITY.Table.Columns.Count; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td >");
                                HttpContext.Current.Response.Write(dvBASEQUALITY[j][i1].ToString());
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






