using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Common;
using errorLog;
using System.IO;
using System.Globalization;
using WCFMain;
using System.Data;

public partial class Module_Prod_plan_Controls_RM_PLANNING : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST;

    private static string txtQTY;
    private static string SHADE_CODE;
    private static string FABR_CODE;
    private static string PI_TYPE;
    private static string BUSINESS_TYPE;
    private static string ORDER_CAT;
    private static string ORDER_TYPE;
    private static string ORDER_NO;
    private static string CR_NO;
    private static int YEAR;
    private string sOrderNo = string.Empty;
    public string PRODUCT_TYPE = "YARN DYEING";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    private void InitialisePage()
    {
        try
        {
            BindBusinessType();
            Session["dtYarnSpinningCustAdj"] = null;
            ClearPage();
            BlanksControls();
            bindCustomerRequestApproval();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BlanksControls()
    {
        try
        {
            {
                int totalRows = grdPlaningData.Rows.Count;
                for (int r = 0; r < totalRows; r++)
                {
                    GridViewRow thisGridViewRow = grdPlaningData.Rows[r];
                    if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                    {
                        Label lblCompCode = (Label)thisGridViewRow.FindControl("lblCompCode");
                        Label lblBRANCH_CODE = (Label)thisGridViewRow.FindControl("lblbranchcode");
                        Label lblYEAR = (Label)thisGridViewRow.FindControl("lblYEAR");
                        string YEAR = lblYEAR.Text.Trim();
                        Label lblORDER_TYPE = (Label)thisGridViewRow.FindControl("lblORDER_TYPE");
                        Label lblYARN_CODE = (Label)thisGridViewRow.FindControl("lblQualityCode");
                        string ArticleCode = lblYARN_CODE.Text.Trim();
                        Label txtTRNYarnSpiningUOM = (Label)thisGridViewRow.FindControl("lblUom");
                        Label txtTRNYarnSpiningOrderQty = (Label)thisGridViewRow.FindControl("lblreqqty");
                        Label txtTRNYarnSpiningDelDate = (Label)thisGridViewRow.FindControl("lblDeldate");
                        Label lblCRNo = (Label)thisGridViewRow.FindControl("lblOrderNo");
                        Label txtShadeCode = (Label)thisGridViewRow.FindControl("lblShadeCode");
                        string SHADE_CODE = txtShadeCode.Text.Trim();
                        Label lblShadeFamily = (Label)thisGridViewRow.FindControl("lblShadeFamily");
                        string SHADE_FAMILY_CODE = lblShadeFamily.Text.Trim();
                        Label txtPartyCode1 = (Label)thisGridViewRow.FindControl("txtPartyCode1");
                        Label txtPartyDetail1 = (Label)thisGridViewRow.FindControl("txtPartyDetail1");

                        Label txtLabDipNo = (Label)thisGridViewRow.FindControl("lblLabDip");
                        Label txtTRNYyarnRemarks = (Label)thisGridViewRow.FindControl("lblRemark");
                       

                        

                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void grdPlaningData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            bindCustomerRequestApproval();
            grdPlaningData.PageIndex = e.NewPageIndex;
            grdPlaningData.DataBind();

           

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {

       
    }

    private DataTable CreateDataTable()
    {
        DataTable dtPODetail = new DataTable();
        dtPODetail.Columns.Add("YEAR", typeof(int));
        dtPODetail.Columns.Add("COMP_CODE", typeof(string));
        dtPODetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtPODetail.Columns.Add("ORDER_TYPE", typeof(string));
        dtPODetail.Columns.Add("ORDER_CAT", typeof(string));
        dtPODetail.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtPODetail.Columns.Add("BUSINESS_TYPE", typeof(string));
        dtPODetail.Columns.Add("ORDER_NO", typeof(string));
        dtPODetail.Columns.Add("ARTICLE_NO", typeof(string));
        dtPODetail.Columns.Add("SUBSTRATE", typeof(string));
        dtPODetail.Columns.Add("COUNT", typeof(string));
        dtPODetail.Columns.Add("SHADE_FAMILY_CODE", typeof(string));
        dtPODetail.Columns.Add("SHADE_CODE", typeof(string));
        dtPODetail.Columns.Add("APP_WEIGHT_OF_UNIT", typeof(double));
        dtPODetail.Columns.Add("APP_NO_OF_UNIT", typeof(double));
        dtPODetail.Columns.Add("QTY_APPROVED", typeof(double));
        dtPODetail.Columns.Add("PLAN_BY", typeof(string));
        dtPODetail.Columns.Add("PLAN_DATE", typeof(DateTime));
        dtPODetail.Columns.Add("PLAN_FLAG", typeof(string));
        dtPODetail.Columns.Add("TDATE", typeof(string));
        dtPODetail.Columns.Add("TUSER", typeof(string));
        dtPODetail.Columns.Add("REMARKS", typeof(string));
        dtPODetail.Columns.Add("TRANS_PRICE", typeof(double));
        dtPODetail.Columns.Add("SALE_RATE", typeof(double));

        return dtPODetail;
    }
    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyDataSearch(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode1.Items.Clear();
            txtPartyCode1.DataSource = data;
            txtPartyCode1.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCountSearch(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));

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
        catch
        {
            throw;
        }
    }

    protected int GetPartyCountSearch(string text)
    {
        try
        {
            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

        }
        catch
        {
            throw;
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
        catch
        {
            throw;
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
        catch
        {
            throw;
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
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_NAME ||'@' ||T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
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
        catch
        {
            throw;
        }
    }

    protected void txtCRTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtCRFrom.Text == null || txtCRFrom.Text == string.Empty)
            {
                CommonFuction.ShowMessage("Please enter From CR Date first..");
            }
            else
            {
                if (DateTime.Parse(txtCRFrom.Text) > DateTime.Parse(txtCRTo.Text))
                {
                    CommonFuction.ShowMessage("Please From CR Date should not be greater than To CR Date..");
                }
                else
                {
                    //BindCRGrid();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Customer Request To Date TextBox.\r\nSee error log for detail."));

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            bindCustomerRequestApproval();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



   

    private void bindCustomerRequestApproval()
    {
        string sBusinessType = string.Empty;
        string strParty = string.Empty;
        string strArticle = string.Empty;
        string strShadeCode = string.Empty;
        string DTCRFrom = string.Empty;
        string DTCRTo = string.Empty;
        string sShadeCode = string.Empty;

        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (ddlBusinessType.SelectedValue.ToString() != null && ddlBusinessType.SelectedValue.ToString() != string.Empty && ddlBusinessType.SelectedIndex > -1)
            {
                sBusinessType = ddlBusinessType.SelectedValue.ToString();
            }
            else
            {
                sBusinessType = string.Empty;
            }

            if (txtPartyCode1.SelectedValue.ToString() != null && txtPartyCode1.SelectedValue.ToString() != string.Empty && txtPartyCode1.SelectedIndex > -1)
            {
                strParty = txtPartyCode1.SelectedText.Trim();
            }
            else
            {
                strParty = string.Empty;
            }

            if (ddlArticle.SelectedValue.ToString() != null && ddlArticle.SelectedValue.ToString() != string.Empty && ddlArticle.SelectedIndex > -1)
            {
                strArticle = ddlArticle.SelectedText.Trim();
            }
            else
            {
                strArticle = string.Empty;
            }
            if (cmbShade.SelectedValue.ToString() != null && cmbShade.SelectedValue.ToString() != string.Empty)
            {
                strShadeCode = cmbShade.SelectedValue.ToString();
            }
            else
            {
                strShadeCode = string.Empty;
            }
            if (txtCRFrom.Text.Trim().ToString() != null && txtCRFrom.Text.Trim().ToString() != string.Empty && txtCRTo.Text.Trim().ToString() != null && txtCRTo.Text.Trim().ToString() != string.Empty)
            {
                DTCRFrom = txtCRFrom.Text.Trim().ToString();
                DTCRTo = txtCRTo.Text.Trim().ToString();
            }
            else
            {
                DTCRFrom = string.Empty;
                DTCRTo = string.Empty;
            }
            DataTable dt = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, sBusinessType, strParty, strArticle, strShadeCode, DTCRFrom, DTCRTo);
            if (dt != null && dt.Rows.Count > 0)
            {
                
                grdPlaningData.DataSource = dt;
                grdPlaningData.DataBind();
                // lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {


                Common.CommonFuction.ShowMessage("Records Not Found");
                grdPlaningData.DataSource = dt;
                grdPlaningData.DataBind();

            }
        }
        catch
        {
            throw;
        }
    }



    protected void grdPlaningData_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;


                Label YARN_CODE = (Label)grdRow.FindControl("lblQulityCode");
               Label BUSINESS__TYPE=   (Label)grdRow.FindControl("lblBUSINESS_TYPE");

               ;
               DataTable data = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningStockData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BUSINESS__TYPE.Text.ToString(),"");


                if (data != null && data.Rows.Count > 0)
                {
                    DataView dv = new DataView(data);
                    dv.RowFilter = "YARN_CODE='" + YARN_CODE.Text +"' ";
                    DataTable dt=(DataTable)dv.ToTable();
                    dt.Columns.Add("PO_RECIVE_QTY", typeof(string));
                    dt.Columns.Add("PO_ORDER_QTY", typeof(string));
                    dt.Columns.Add("PO_NUMB", typeof(string));
                    dt.Columns.Add("BALANCE", typeof(string));
                    if (BUSINESS__TYPE.Text == "SALE WORK")
                    { for(int i=0;i<dv.Count;i++)
                    {
                        DataTable dataPO = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningPoData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, dt.Rows[i]["YARN_CODE"].ToString(), dt.Rows[i]["LOT_NO"].ToString());

                       
                        if (dataPO.Rows.Count != 0)
                        {
                            dt.Rows[i]["PO_RECIVE_QTY"] = dataPO.Rows[0]["PO_RECIVE_QTY"];
                            dt.Rows[i]["PO_ORDER_QTY"] = dataPO.Rows[0]["PO_ORDER_QTY"];
                            dt.Rows[i]["PO_NUMB"] = dataPO.Rows[0]["PO_NUMB"];
                            dt.Rows[i]["BALANCE"] = dataPO.Rows[0]["BALANCE"];
                            dt.AcceptChanges();
                        }
                        
                    }
                    }
                    GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
                    grdPOTRN.DataSource = dt;
                    grdPOTRN.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));

        }






    }




    protected void ClearPage()
    {
        try
        {
           
            bindCustomerRequestApproval();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   

    private void BindBusinessType()
    {
        try
        {
            ddlBusinessType.Items.Clear();
            DataTable dtBusinessType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);

            ddlBusinessType.DataSource = dtBusinessType;
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataBind();
            ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue("SW"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void ActivateUpdateMode()
    {
        try
        {
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            lblMode.Text = "Update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearPage();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the page"));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        
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

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnTrnSave_Click(object sender, EventArgs e)
    {
        // SaveTRNYarnSpiningRow();
    }

    protected void btnTrnCancel_Click(object sender, EventArgs e)
    {
        // RefreshTRNYarnSpiningRow();
    }
}
