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
using Obout.ComboBox;


public partial class Module_Prod_plan_Controls_Prod_DyeingPlan : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST;

    private DataTable dtTRN_SUB = null;
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
            ddlOrderNo.PRODUCT_TYPE = PRODUCT_TYPE;
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"].ToString().Equals(string.Empty) == false)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }
            SetPageAccordingProductType();
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

    void ddlOrderNo_OnTextChanged(string Value, string Text)
    {
        try
        {
            string ORDER_STRING = ddlOrderNo.SelectedValue.Trim();

            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();
            dtTRNYarnSpining.Rows.Clear();
            int iRecordFound = GetdataByOrderNumber(Common.CommonFuction.funFixQuotes(ORDER_STRING));
            if (iRecordFound > 0)
            {
                ActivateUpdateMode();
                txtOrderNo.Visible = true;
                ddlOrderNo.Visible = false;
            }
            else
            {
                //ClearPage();
                lblMode.Text = "Update";
                txtOrderNo.Text = "";
                ActivateUpdateMode();
                string msg = "Dear " + oUserLoginDetail.Username + " !! Order already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }

    void txtTRNYarnSpiningArticalCode_OnTextChanged(string Value, string Text)
    {
        try
        {
            string sYarnData = txtTRNYarnSpiningArticalCode.SelectedValue.ToString();

            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

            string ARTICLE_CODE = string.Empty;
            string UOM = string.Empty;
            string Description = string.Empty;
            string Cust_Req_no = string.Empty;
            string TKT_NO = string.Empty;
            string SHADE = string.Empty;
            string SHADE_NAME = string.Empty;


            if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                ARTICLE_CODE = string.Empty;//GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out Cust_Req_no, out TKT_NO, out SHADE, out crQty, out SHADE_NAME);
            else
                ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

            bool bb = SearchTRNYarnSpiningArticalCodeInGrid(ARTICLE_CODE, UNIQUE_ID, txtShadeCode.Text);
            if (!bb)
            {
                txtTRNYarnSpiningUOM.Text = UOM;
                lblTRNYSpinDesc.Text = Description;
                lblTRNYarnSpiningArticalCode.Text = ARTICLE_CODE;
                txtTRNYarnSpiningUOM.Text = UOM;

            }
            else
            {
                lblTRNYarnSpiningArticalCode.Text = string.Empty;
                txtTRNYarnSpiningArticalCode.SelectedIndex = -1;
                lblTRNYSpinDesc.Text = string.Empty;
                Common.CommonFuction.ShowMessage("This Yarn artical code already exists.");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Yarn artical code"));
        }
    }

    private void InitialisePage()
    {
        try
        {
            BindBusinessType();
            BindOrderType();
            BindProductType();
            BindCurrency();
            Session["dtYarnSpinningCustAdj"] = null;
            Session["dtTRN_SUB"] = null;
            ClearPage();
            BlanksControls();
            bindCustomerRequestApproval();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void RefreshDetailRow()
    {
        if (Session["dtTRNYarnSpining"] != null)
            dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
        else
            dtTRNYarnSpining = CreateTRNYarnSpiningTable();
        if (dtTRNYarnSpining != null)
            dtTRNYarnSpining.Rows.Clear();
        grdPlaningData.DataSource = null;
        grdPlaningData.DataBind();
        grdPlaningData.SelectedIndex = -1;
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
                        Label txtTRNYarnSpiningOrderQty = (Label)thisGridViewRow.FindControl("lblMaxQTY");
                        Label txtTRNYarnSpiningDelDate = (Label)thisGridViewRow.FindControl("lblDeldate");
                        Label lblCRNo = (Label)thisGridViewRow.FindControl("lblOrderNo");
                        Label txtShadeCode = (Label)thisGridViewRow.FindControl("lblShadeCode");
                        string SHADE_CODE = txtShadeCode.Text.Trim();
                        Label lblShadeFamily = (Label)thisGridViewRow.FindControl("lblShadeFamily");
                        string SHADE_FAMILY_CODE = lblShadeFamily.Text.Trim();
                        Label txtPartyCode1 = (Label)thisGridViewRow.FindControl("txtPartyCode1");
                        Label txtPartyDetail1 = (Label)thisGridViewRow.FindControl("txtPartyDetail1");
                        txtPartyCode.Text = txtPartyCode1.Text.ToString();
                        txtPartyDetail.Text = txtPartyDetail1.Text.ToString();
                        Label txtLabDipNo = (Label)thisGridViewRow.FindControl("lblLabDip");
                        Label txtTRNYyarnRemarks = (Label)thisGridViewRow.FindControl("lblRemark");
                        CheckBox chkTransfer = (CheckBox)thisGridViewRow.FindControl("chkTransfer");
                        chkTransfer.Checked = false;
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

            foreach (GridViewRow gvr in grdPlaningData.Rows)
            {
                CheckBox chkTransfer = (CheckBox)gvr.FindControl("chkTransfer");
                chkTransfer.Checked = false;
            }
            BindTRNYarnSpiningGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void chkTransfer_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = ((CheckBox)(sender));
            if (chk.Checked == true)
            {
                GridViewRow gv1 = ((GridViewRow)(chk.NamingContainer));
                CheckBox chkTransfer = (CheckBox)gv1.FindControl("chkTransfer");

                if (Session["dtTRNYarnSpining"] != null)
                    dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
                else
                    dtTRNYarnSpining = CreateTRNYarnSpiningTable();
                Label lblCompCode = (Label)gv1.FindControl("lblCompCode");
                Label lblBRANCH_CODE = (Label)gv1.FindControl("lblbranchcode");
                Label lblYEAR = (Label)gv1.FindControl("lblYEAR");
                string YEAR = lblYEAR.Text.Trim();
                Label lblORDER_TYPE = (Label)gv1.FindControl("lblORDER_TYPE");
                Label lblYARN_CODE = (Label)gv1.FindControl("lblQualityCode");
                string ArticleCode = lblYARN_CODE.Text.Trim();
                Label txtTRNYarnSpiningUOM = (Label)gv1.FindControl("lblUom");
                TextBox txtTRNYarnSpiningOrderQty = (TextBox)gv1.FindControl("txtTRNYarnSpiningOrderQty");
                Label txtTRNYarnSpiningDelDate = (Label)gv1.FindControl("lblDeldate");
                Label lblCRNo = (Label)gv1.FindControl("lblOrderNo");
                Label txtShadeCode = (Label)gv1.FindControl("lblShadeCode");
                Label txtTRN_NUMB = (Label)gv1.FindControl("lblTrnNumb");

                string SHADE_CODE = txtShadeCode.Text.Trim();
                Label lblShadeFamily = (Label)gv1.FindControl("lblShadeFamily");
                string SHADE_FAMILY_CODE = lblShadeFamily.Text.Trim();

                Label txtPartyCode1 = (Label)gv1.FindControl("txtPartyCode1");
                Label txtPartyDetail1 = (Label)gv1.FindControl("txtPartyDetail1");

                txtPartyCode.Text = txtPartyCode1.Text.ToString();
                txtPartyDetail.Text = txtPartyDetail1.Text.ToString();

                Label txtLabDipNo = (Label)gv1.FindControl("lblLabDip");
                Label txtTRNYyarnRemarks = (Label)gv1.FindControl("lblRemark");


                DataRow dr = dtTRNYarnSpining.NewRow();
                dr["UNIQUE_ID"] = dtTRNYarnSpining.Rows.Count + 1;
                dr["PI_TYPE"] = PI_TYPE;
                dr["PI_NO"] = dtTRNYarnSpining.Rows.Count + 1;
                dr["ARTICAL_CODE"] = lblYARN_CODE.Text;
                dr["UOM"] = txtTRNYarnSpiningUOM.Text;
                dr["ORD_QTY"] = double.Parse(txtTRNYarnSpiningOrderQty.Text.Trim());
                DateTime deldate = Convert.ToDateTime(txtTRNYarnSpiningDelDate.Text.Trim());
                dr["DEL_DATE"] = deldate.ToShortDateString();
                dr["LOT_ID"] = lblpi_no.Text;
                dr["SHADE_CODE"] = txtShadeCode.Text.Trim();
                dr["LAB_DIP_NO"] = "NA";
                double srinkage = 0;
                double.TryParse(txtTRNYarnSpiningSrinkage.Text.Trim(), out srinkage);
                dr["SRINKAGE"] = srinkage;
                dr["CUST_REQ_NO"] = lblCRNo.Text;
                dr["TRN_NUMB"] = txtTRN_NUMB.Text.Trim();
                dr["REMARKS"] = txtTRNYyarnRemarks.Text.Trim();
                dtTRNYarnSpining.Rows.Add(dr);
                Session["dtTRNYarnSpining"] = dtTRNYarnSpining;
                BindTRNYarnSpiningGrid();
                RefreshTRNYarnSpiningRow();
            }
            else
            {
                dtTRNYarnSpining = null;
                Session["dtTRNYarnSpining"] = dtTRNYarnSpining;
                BindTRNYarnSpiningGrid();
                RefreshTRNYarnSpiningRow();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Confirming.\r\nSee error log for detail."));
        }
    }
    protected void grdPlaningData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Page" && e.CommandName != "")
        {
            GridViewRow gv1 = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            Label lblCompCode = (Label)gv1.FindControl("lblCompCode");
            Label lblBRANCH_CODE = (Label)gv1.FindControl("lblbranchcode");
            Label lblYEAR = (Label)gv1.FindControl("lblYEAR");

            string YEAR = lblYEAR.Text.Trim();
            Label lblYARN_CODE = (Label)gv1.FindControl("lblQualityCode");
            string ArticleCode = lblYARN_CODE.Text.Trim();
            Label txtTRNYarnSpiningUOM = (Label)gv1.FindControl("lblUom");
            Label lblCRNo = (Label)gv1.FindControl("lblOrderNo");
            Label lblOrderCat = (Label)gv1.FindControl("lblOrderCat");
            Label txtShadeCode = (Label)gv1.FindControl("lblShadeCode");
            Label lblTrnNumb = (Label)gv1.FindControl("lblTrnNumb");
            Label lblMachine = (Label)gv1.FindControl("lblMachine");
            Label lblMaxQTY = (Label)gv1.FindControl("lblMaxQTY");
            TextBox txtTRNYarnSpiningOrderQty = (TextBox)gv1.FindControl("txtTRNYarnSpiningOrderQty");
            string SHADE_CODE = txtShadeCode.Text.Trim();
            Label lblShadeFamily = (Label)gv1.FindControl("lblShadeFamily");
            Label lblTotalyarnstock = (Label)gv1.FindControl("lblTotalyarnstock");
            Label txtPartyDetail1 = (Label)gv1.FindControl("txtPartyDetail1");
            Label lblQulityCode = (Label)gv1.FindControl("lblQulityCode");

            string SHADE_FAMILY_CODE = lblShadeFamily.Text.Trim();

            if (e.CommandName == "ViewAdjOrder")
            {
                if (PRODUCT_TYPE == "YARN DYEING")
                {
                    try
                    {
                        txtTRNYarnSpiningOrderQty.ReadOnly = false;//Code to enable TxtQty for insert
                        string URL = "OCYarnCustAdjustment.aspx";
                        URL = URL + "?YARN_CODE=" + lblYARN_CODE.Text.Trim(); //lblTRNYarnSpiningArticalCode.Text.Trim();
                        URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
                        URL = URL + "&COMP_CODE=" + lblCompCode.Text.Trim();
                        URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text.Trim();
                        URL = URL + "&SHADE_FAMILY=" + lblShadeFamily.Text.Trim();
                        URL = URL + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
                        URL = URL + "&PI_TYPE=" + PI_TYPE;
                        URL = URL + "&txtQTY=" + txtTRNYarnSpiningOrderQty.ClientID;
                        URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.ToString();
                        URL = URL + "&ORDER_CAT=" + lblOrderCat.Text.Trim();
                        URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.ToString();
                        URL = URL + "&ORDER_NO=" + lblCRNo.Text.Trim();
                        URL = URL + "&lblMaxQTY=" + lblMaxQTY.Text;
                        URL = URL + "&TRN_NUMB=" + lblTrnNumb.Text.Trim();
                        URL = URL + "&YEAR=" + YEAR;
                        URL = URL + "&ARTICAL_CODE=" + lblQulityCode.ToolTip.Trim();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=900,height=500,left=50,top=50');", true);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }




            if (e.CommandName == "ViewAdjStock")
            {
                if (PRODUCT_TYPE == "YARN DYEING")
                {
                    try
                    {
                        txtTRNYarnSpiningOrderQty.ReadOnly = false;//Code to enable TxtQty for insert
                        string URL = "Prod_Plan_Stock_Details.aspx";
                        URL = URL + "?YARN_CODE=" + lblQulityCode.ToolTip.Trim(); //lblTRNYarnSpiningArticalCode.Text.Trim();
                        URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
                        URL = URL + "&COMP_CODE=" + lblCompCode.Text.Trim();
                        URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text.Trim();
                        URL = URL + "&SHADE_FAMILY=" + lblShadeFamily.Text.Trim();
                        URL = URL + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
                        URL = URL + "&PI_TYPE=" + PI_TYPE;
                        URL = URL + "&txtQTY=" + txtTRNYarnSpiningOrderQty.ClientID;
                        URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedItem.ToString();
                        URL = URL + "&ORDER_CAT=" + lblOrderCat.Text.Trim();
                        URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.ToString();
                        URL = URL + "&ORDER_NO=" + lblCRNo.Text.Trim();
                        URL = URL + "&lblMaxQTY=" + lblMaxQTY.Text;
                        URL = URL + "&TRN_NUMB=" + lblTrnNumb.Text.Trim();
                        URL = URL + "&YEAR=" + YEAR;
                        URL = URL + "&STOCK=" + lblTotalyarnstock.Text.ToString();
                        URL = URL + "&PARTY_CODE=" + txtPartyDetail1.ToolTip.ToString();
                        

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=900,height=500,left=50,top=50');", true);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }



        }
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
        catch (Exception ex)
        {
            throw ex;
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






    protected void cmbMachine_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMachineItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbMachine.Items.Clear();
                cmbMachine.DataSource = data;
                cmbMachine.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetMachineItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetMachineItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.MACHINE_CODE FROM   MC_MACHINE_MASTER m WHERE MACHINE_GROUP = 'DYEING' AND M.COMP_CODE = '"+oUserLoginDetail.COMP_CODE+"' AND M.BRANCH_CODE = '"+ oUserLoginDetail.CH_BRANCHCODE+"' ORDER BY   MACHINE_CODE) ASD  WHERE   MACHINE_CODE LIKE :SearchQuery) WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND MACHINE_CODE NOT IN (SELECT MACHINE_CODE FROM (SELECT * FROM (SELECT   M.MACHINE_CODE FROM   MC_MACHINE_MASTER m WHERE MACHINE_GROUP = 'DYEING' AND M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY   MACHINE_CODE) ASD  WHERE   MACHINE_CODE LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY MACHINE_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int GetMachineItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT   M.MACHINE_CODE FROM   MC_MACHINE_MASTER m WHERE MACHINE_GROUP = 'DYEING' AND M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' ORDER BY   MACHINE_CODE) ASD  WHERE   MACHINE_CODE LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY MACHINE_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
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

    private void MapAdjustDataTable(DataTable dtReceiptAdjustment)
    {
        try
        {

            if (!dtReceiptAdjustment.Columns.Contains("UNIQUEID"))
                dtReceiptAdjustment.Columns.Add("UNIQUEID", typeof(double));

            if (!dtReceiptAdjustment.Columns.Contains("NO_OF_UNIT"))
                dtReceiptAdjustment.Columns.Add("NO_OF_UNIT", typeof(double));

            if (!dtReceiptAdjustment.Columns.Contains("UOM_OF_UNIT"))
                dtReceiptAdjustment.Columns.Add("UOM_OF_UNIT", typeof(string));

            if (!dtReceiptAdjustment.Columns.Contains("WEIGHT_OF_UNIT"))
                dtReceiptAdjustment.Columns.Add("WEIGHT_OF_UNIT", typeof(double));

            if (!dtReceiptAdjustment.Columns.Contains("PI_NO"))
                dtReceiptAdjustment.Columns.Add("PI_NO", typeof(string));

            if (!dtReceiptAdjustment.Columns.Contains("ISS_PI_NO"))
                dtReceiptAdjustment.Columns.Add("ISS_PI_NO", typeof(string));

            for (int iLoop = 0; iLoop < dtReceiptAdjustment.Rows.Count; iLoop++)
            {
                dtReceiptAdjustment.Rows[iLoop]["UNIQUEID"] = iLoop + 1;

                if (dtReceiptAdjustment.Rows[iLoop]["NO_OF_UNIT"] == null)
                    dtReceiptAdjustment.Rows[iLoop]["NO_OF_UNIT"] = 0f;

                if (dtReceiptAdjustment.Rows[iLoop]["UOM_OF_UNIT"] == null)
                    dtReceiptAdjustment.Rows[iLoop]["UOM_OF_UNIT"] = string.Empty;

                if (dtReceiptAdjustment.Rows[iLoop]["WEIGHT_OF_UNIT"] == null)
                    dtReceiptAdjustment.Rows[iLoop]["WEIGHT_OF_UNIT"] = 0f;
            }
            dtReceiptAdjustment.AcceptChanges();

            Session["dtYarnSpinningCustAdj"] = dtReceiptAdjustment;
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
        string MACHINE_CODE = string.Empty;

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

            if (cmbMachine.SelectedValue.ToString() != null && cmbMachine.SelectedValue.ToString() != string.Empty)
            {
                MACHINE_CODE = cmbMachine.SelectedValue.ToString();
            }
            else
            {
                MACHINE_CODE = string.Empty;
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
            DataTable dt = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetPlanningData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, sBusinessType, strParty, strArticle, strShadeCode, DTCRFrom, DTCRTo,MACHINE_CODE );
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("ORDER_DATE"))
                    dt.Columns.Add("ORDER_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("REMARKS"))
                    dt.Columns.Add("REMARKS", typeof(string));
                dt.Columns.Add("ABL_STOCK", typeof(string));
                dt.Columns.Add("QTY_REM", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["REMARKS"].ToString();
                    if (ConfBy == "")
                        dr["REMARKS"] = oUserLoginDetail.Username;
                    dr["ORDER_DATE"] = System.DateTime.Now.Date.ToShortDateString();

                //    DataTable data = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningBalanceData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, dr["BUSINESS_TYPE"].ToString(), dr["ARTICLE_CODE"].ToString(), dr["PRTY_CODE"].ToString());

                //    if (data.Rows[0]["QTY_REM"].ToString() != "")
                //    {
                //        dr["QTY_REM"] = double.Parse(data.Rows[0]["QTY_REM"].ToString());
                //    }
                //    else
                //    {
                //        dr["QTY_REM"] = "0";
                //    }

                //    if (dr["YARN_STOCK"].ToString() != null)
                //    {
                //        dr["ABL_STOCK"] = (double.Parse(dr["YARN_STOCK"].ToString()) - double.Parse(dr["QTY_REM"].ToString()));
                //    }
                //    else
                //    {
                //        dr["ABL_STOCK"] = "0";
                //    }
               }
                DataView dv = dt.DefaultView;
                dv.Sort = " MACHINE desc ";
                grdPlaningData.DataSource = dv;
                grdPlaningData.DataBind();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Records Not Found");
                grdPlaningData.DataSource = dt;
                grdPlaningData.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





    private DataTable bindCustomerRequestApproval1()
    {
        string sBusinessType = string.Empty;
        string strParty = string.Empty;
        string strArticle = string.Empty;
        string strShadeCode = string.Empty;
        string DTCRFrom = string.Empty;
        string DTCRTo = string.Empty;
        string sShadeCode = string.Empty;
        string MACHINE_CODE = string.Empty;

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

            if (cmbMachine.SelectedValue.ToString() != null && cmbMachine.SelectedValue.ToString() != string.Empty)
            {
                MACHINE_CODE = cmbMachine.SelectedValue.ToString();
            }
            else
            {
                MACHINE_CODE = string.Empty;
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
            DataTable dt = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetPlanningData1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, sBusinessType, strParty, strArticle, strShadeCode, DTCRFrom, DTCRTo, MACHINE_CODE);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    private void BindAppPlanData()
    {
        try
        {
            if (ViewState["dtTRNYarnSpining"] != null)
            {
                dtTRNYarnSpining = (DataTable)ViewState["dtTRNYarnSpining"];
            }
            grdPlaningData.DataSource = dtTRNYarnSpining;
            grdPlaningData.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void grdPlaningData_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //GridViewRow grdRow = e.Row;

                //Label YARN_CODE = (Label)grdRow.FindControl("lblQulityCode");
                //Label BUSINESS__TYPE = (Label)grdRow.FindControl("lblarticleCode");
                //Label PRTY_CODE = (Label)grdRow.FindControl("txtPartyCode1");
                //DataTable data = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningStockData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, BUSINESS__TYPE.Text.ToString(), PRTY_CODE.Text.ToString());
                
                //if (data != null && data.Rows.Count > 0)
                //{
                //    DataView dv = new DataView(data);
                //    dv.RowFilter = "YARN_CODE='" + YARN_CODE.Text + "' ";
                //    DataTable dt = (DataTable)dv.ToTable();
                //    dt.Columns.Add("PO_RECIVE_QTY", typeof(string));
                //    dt.Columns.Add("PO_ORDER_QTY", typeof(string));
                //    dt.Columns.Add("PO_NUMB", typeof(string));
                //    dt.Columns.Add("BALANCE", typeof(string));
                //    if (BUSINESS__TYPE.Text == "SALE WORK")
                //    {
                //        for (int i = 0; i < dv.Count; i++)
                //        {
                //            DataTable dataPO = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetRMPlanningPoData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, dt.Rows[i]["YARN_CODE"].ToString(), dt.Rows[i]["LOT_NO"].ToString());
                            
                //            if (dataPO.Rows.Count != 0)
                //            {
                //                dt.Rows[i]["PO_RECIVE_QTY"] = dataPO.Rows[0]["PO_RECIVE_QTY"];
                //                dt.Rows[i]["PO_ORDER_QTY"] = dataPO.Rows[0]["PO_ORDER_QTY"];
                //                dt.Rows[i]["PO_NUMB"] = dataPO.Rows[0]["PO_NUMB"];
                //                dt.Rows[i]["BALANCE"] = dataPO.Rows[0]["BALANCE"];
                //                dt.AcceptChanges();
                //            }
                //        }
                //    }
                //    GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
                //    grdPOTRN.DataSource = dt;
                //    grdPOTRN.DataBind();
                //}
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
        }
    }
    protected void SetPageAccordingProductType()
    {
        try
        {
            #region code to set pi type

            if (PRODUCT_TYPE == "YARN DYEING")
            {
                PI_TYPE = "YARN DYEING";
            }
            else if (PRODUCT_TYPE == "TEXTURISED YARN")
            {
                PI_TYPE = "YARN TEXTURISING";
            }

            else if (PRODUCT_TYPE == "TWISTED YARN")
            {
                PI_TYPE = "YARN TWISTING";
            }
            else if (PRODUCT_TYPE == "YARN SPINING")
            {
                PI_TYPE = "YARN SPINING";
            }

            ddlOrderNo.PRODUCT_TYPE = PRODUCT_TYPE;
            lblFormHeading.Text = PRODUCT_TYPE;
            #endregion
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ClearPage()
    {
        try
        {
            ClearMainData();
            ActivateSaveMode();
            BindOrderNo();
            RefreshTRNYarnSpiningRow();
            dtTRNYarnSpining = null;
            BindTRNYarnSpiningGrid();
            bindCustomerRequestApproval();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ClearMainData()
    {
        try
        {
            ddlBusinessType.SelectedIndex = -1;
            ddlCurrencyCode.SelectedIndex = -1;
            ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue("PRODUCTION"));
            txtPartyCodecmb.SelectedIndex = -1;
            ddlOrderCategory.SelectedIndex = -1;
            txtConversionRate.Text = "1";
            txtOrderDate.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtPartyDetail.Text = string.Empty;
            txtOrderDate.Text = System.DateTime.Now.Date.ToShortDateString();
            Session["dtTRN_DEL_SCHEDULE"] = null;
            Session["dtTRN_COST"] = null;
            Session["dtTRN_BOM"] = null;
            Session["dtBOMAdj"] = null;
            Session["dtYarnSpinningCustAdj"] = null;
            Session["dtTRNYarnSpining"] = null;
            ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue("SW"));
            ddlCurrencyCode.SelectedIndex = ddlCurrencyCode.Items.IndexOf(ddlCurrencyCode.Items.FindByValue("Rs."));
            txtPartyCode.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ActivateSaveMode()
    {
        try
        {
            lblMode.Text = "Save";
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            ddlOrderCategory.Enabled = true;
            ddlBusinessType.Enabled = true;
            ddlProductType.Enabled = false;
            ddlOrderType.Enabled = true;
            txtOrderNo.Text = string.Empty;
            txtOrderNo.Visible = true;
            ddlOrderNo.Visible = false;
            ddlOrderNo.SelectedIndex = -1;
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
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindProductType()
    {
        try
        {
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);

            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();

            ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(PRODUCT_TYPE));
            ddlProductType.Text = PRODUCT_TYPE;
            ddlProductType.Enabled = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindOrderType()
    {
        try
        {
            ddlOrderType.Items.Clear();
            DataTable dtOrderType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);

            ddlOrderType.DataSource = dtOrderType;
            ddlOrderType.DataTextField = "MST_DESC";
            ddlOrderType.DataValueField = "MST_CODE";
            ddlOrderType.DataBind();
            ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue("PRODUCTION"));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindCurrency()
    {
        try
        {
            ddlCurrencyCode.Items.Clear();
            DataTable dtCurrencyCode = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("CURRENCY", oUserLoginDetail.COMP_CODE);
            ddlCurrencyCode.DataSource = dtCurrencyCode;
            ddlCurrencyCode.DataTextField = "MST_DESC";
            ddlCurrencyCode.DataValueField = "MST_CODE";
            ddlCurrencyCode.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindOrderNo()
    {
        try
        {
            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtOrderNo.Text = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetMaxOrderNo(oOD_CAPTURE_MST);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlBusinessType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Business Type Selection"));
        }
    }
    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Production Type Selection"));
        }
    }
    protected void ddlOrderCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            OrdercategorySelection();
            BindOrderNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order category Selection"));
        }
    }
    private void OrdercategorySelection()
    {
        try
        {
            txtPartyDetail.Text = string.Empty;

            if (ddlOrderCategory.SelectedValue.Trim().Equals("INHOUSE"))
            {
                txtPartyDetail.Text = "SELF";
                txtPartyCodecmb.Enabled = false;
            }
            else
            {
                txtPartyCodecmb.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Type Selection"));
        }
    }

    #region Code to Manage Yarn Spining Data

    private DataTable dtTRNYarnSpining;

    protected void btnsaveTRNYarnSpiningDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateYarnSpiningTRNRow(out msg))
            {
                SaveTRNYarnSpiningRow();
                BindTRNYarnSpiningGrid();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving row for Yarn Spining PI Indent"));
        }
    }

    private bool ValidateYarnSpiningTRNRow(out string msg)
    {
        try
        {
            string ARTICLE_CODE = string.Empty;
            string UOM = string.Empty;
            string Description = string.Empty;
            string Cust_Req_no = string.Empty;
            string TKT_NO = string.Empty;
            string SHADE = string.Empty;
            string SHADE_NAME = string.Empty;
            double crQty = 0;

            if (txtTRNYarnSpiningArticalCode.SelectedValue != "SELECT")
                ARTICLE_CODE = string.Empty;// GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description, out Cust_Req_no, out TKT_NO, out SHADE, out crQty, out SHADE_NAME);
            else
                ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;

            int iCount = 0;
            int TotalCount = 0;
            msg = string.Empty;

            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

            if (ddlOrderCategory.SelectedItem.Text != "INHOUSE")
            {
                TotalCount++;
                //if (txtTRNYarnSpiningCReqNo.Text != string.Empty)
                //{
                //    iCount++;
                //}
                //else
                //{
                //    msg += @"\r\nInvalid Customer Request selected";
                //}
            }

            TotalCount++;
            if (UNIQUE_ID == 0)
            {
                if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Artical Code";
                }
            }
            else
            {
                if (txtTRNYarnSpiningArticalCode.SelectedValue.Trim() != string.Empty)
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Artical Code";
                }
            }

            double dTemp = 0;
            TotalCount++;



            if (txtTRNYarnSpiningOrderQty.Text != string.Empty && double.TryParse(txtTRNYarnSpiningOrderQty.Text, out dTemp))
            {
                iCount++;
            }
            else
            {
                msg += @"\r\nInvalid Ordered Quantity";
            }

            if (ddlOrderCategory.SelectedItem.Text != "INHOUSE")
            {
                //dTemp = 0;
                //TotalCount++;
                //if (txtTRNYarnSpiningCost.Text != string.Empty && double.TryParse(txtTRNYarnSpiningCost.Text, out dTemp))
                //{
                //    iCount++;
                //}
                //else
                //{
                //    msg += @"\r\nInvalid Cost Price";
                //}
            }

            TotalCount++;
            if (Session["dtTRN_BOM"] != null)
            {
                DataTable dtTRN_BOM = (DataTable)Session["dtTRN_BOM"];
                DataView dv = new DataView(dtTRN_BOM);
                dv.RowFilter = "ARTICAL_CODE='" + ARTICLE_CODE + "' and PI_TYPE='" + PI_TYPE + "'";
                if (dv.Count > 0)
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid BOM";
                }
            }
            else
            {
                msg += @"\r\nInvalid BOM";
            }

            dTemp = 0;
            if (txtTRNYarnSpiningSrinkage.Text != string.Empty)
            {
                TotalCount++;
                if (double.TryParse(txtTRNYarnSpiningSrinkage.Text, out dTemp))
                {
                    iCount++;
                }
                else
                {
                    msg += @"\r\nInvalid Shrinkage";
                }
            }

            if (iCount == TotalCount)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SaveTRNYarnSpiningRow()
    {
        try
        {
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();

            if (dtTRNYarnSpining.Rows.Count < 15)
            {
                int UNIQUE_ID = 0;
                if (ViewState["UNIQUE_ID"] != null)
                {
                    UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                }

                string ARTICLE_CODE = string.Empty;
                string GREY_LOT_NO = string.Empty;
                string UOM = string.Empty;
                string Description = string.Empty;
                string CUST_REQ_NO = string.Empty;
                string TKT_NO = string.Empty;
                string SHADE = string.Empty;
                string SHADE_NAME = string.Empty;


                if (txtTRNYarnSpiningArticalCode.SelectedIndex != -1)
                {
                    ARTICLE_CODE = GetArticalCodeFromString(txtTRNYarnSpiningArticalCode.SelectedValue.Trim(), out UOM, out Description);
                }

                else
                {
                    ARTICLE_CODE = lblTRNYarnSpiningArticalCode.Text;
                }
                bool bb = SearchTRNYarnSpiningArticalCodeInGrid(ARTICLE_CODE, UNIQUE_ID, txtShadeCode.Text.Trim());
                if (!bb)
                {
                    double ORD_Qty = 0;
                    double.TryParse(txtTRNYarnSpiningOrderQty.Text.Trim(), out ORD_Qty);
                    if (ORD_Qty > 0)
                    {
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtTRNYarnSpining);
                            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                            if (dv.Count > 0)
                            {
                                dv[0]["PI_TYPE"] = PI_TYPE;
                                dv[0]["PI_NO"] = lblpi_no.Text;
                                dv[0]["ARTICAL_CODE"] = ARTICLE_CODE;
                                //dv[0]["GREY_LOT_NO"] = txtLotNo.SelectedValue;
                                dv[0]["UOM"] = txtTRNYarnSpiningUOM.Text;
                                dv[0]["ORD_QTY"] = double.Parse(txtTRNYarnSpiningOrderQty.Text.Trim());
                                DateTime deldate = Convert.ToDateTime(txtTRNYarnSpiningDelDate.Text.Trim());
                                dv[0]["DEL_DATE"] = deldate.ToShortDateString();
                                dv[0]["SHADE_CODE"] = txtShadeCode.Text.Trim();
                                dv[0]["LAB_DIP_NO"] = txtLabDipNo.Text.Trim();
                                // dv[0]["LOT_ID"] = txtLotID.Text.Trim();
                                double srinkage = 0;
                                double.TryParse(txtTRNYarnSpiningSrinkage.Text.Trim(), out srinkage);
                                dv[0]["SRINKAGE"] = srinkage;
                                dv[0]["LOT_ID"] = lblpi_no.Text;

                                dv[0]["CUST_REQ_NO"] = lblCRNo.Text;
                                dv[0]["REMARKS"] = txtTRNYyarnRemarks.Text.Trim();
                                dtTRNYarnSpining.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = dtTRNYarnSpining.NewRow();
                            dr["UNIQUE_ID"] = dtTRNYarnSpining.Rows.Count + 1;
                            dr["PI_TYPE"] = PI_TYPE;
                            dr["PI_NO"] = dtTRNYarnSpining.Rows.Count + 1;
                            dr["ARTICAL_CODE"] = ARTICLE_CODE;
                            //dr["GREY_LOT_NO"] = txtLotNo.SelectedValue;
                            dr["UOM"] = txtTRNYarnSpiningUOM.Text;
                            dr["ORD_QTY"] = double.Parse(txtTRNYarnSpiningOrderQty.Text.Trim());
                            DateTime deldate = Convert.ToDateTime(txtTRNYarnSpiningDelDate.Text.Trim());
                            dr["DEL_DATE"] = deldate.ToShortDateString();
                            dr["LOT_ID"] = lblpi_no.Text;
                            dr["SHADE_CODE"] = txtShadeCode.Text.Trim();
                            dr["LAB_DIP_NO"] = txtLabDipNo.Text.Trim();
                            double srinkage = 0;
                            double.TryParse(txtTRNYarnSpiningSrinkage.Text.Trim(), out srinkage);
                            dr["SRINKAGE"] = srinkage;
                            dr["CUST_REQ_NO"] = lblCRNo.Text;
                            dr["REMARKS"] = txtTRNYyarnRemarks.Text.Trim();
                            dtTRNYarnSpining.Rows.Add(dr);

                        }
                        Session["dtTRNYarnSpining"] = dtTRNYarnSpining;
                        BindTRNYarnSpiningGrid();
                        RefreshTRNYarnSpiningRow();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(@"Quantity can not be zero");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage(@"enter valid Artical Code");
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchTRNYarnSpiningArticalCodeInGrid(string ArticalCode, int UNIQUE_ID, string Shade_code)
    {
        bool Result = false;
        try
        {
            if (grdTRNYarnSpiningDetail.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdTRNYarnSpiningDetail.Rows)
                {
                    Label txtTRNYarnSpiningArticalCodes = (Label)grdRow.FindControl("txtTRNYarnSpiningArticalCode");
                    Label txtTRNYarnSpiningShade = (Label)grdRow.FindControl("txtTRNYarnSpiningShade");

                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUE_ID = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                    if (txtTRNYarnSpiningArticalCodes.Text.Trim() == ArticalCode && UNIQUE_ID != iUNIQUE_ID && txtTRNYarnSpiningShade.Text == Shade_code)
                        Result = true;
                }
            }
            return Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindTRNYarnSpiningGrid()
    {
        try
        {
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();
            grdTRNYarnSpiningDetail.DataSource = dtTRNYarnSpining;
            grdTRNYarnSpiningDetail.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable CreateTRNYarnSpiningTable()
    {
        try
        {
            dtTRNYarnSpining = new DataTable();
            dtTRNYarnSpining.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRNYarnSpining.Columns.Add("PI_TYPE", typeof(string));
            dtTRNYarnSpining.Columns.Add("PI_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("ARTICAL_CODE", typeof(string));
            dtTRNYarnSpining.Columns.Add("GREY_LOT_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("REMARKS", typeof(string));
            dtTRNYarnSpining.Columns.Add("UOM", typeof(string));
            dtTRNYarnSpining.Columns.Add("ORD_QTY", typeof(double));
            dtTRNYarnSpining.Columns.Add("DEL_DATE", typeof(DateTime));
            dtTRNYarnSpining.Columns.Add("LOT_ID", typeof(string));
            dtTRNYarnSpining.Columns.Add("SHADE_CODE", typeof(string));
            dtTRNYarnSpining.Columns.Add("LAB_DIP_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("SRINKAGE", typeof(double));
            dtTRNYarnSpining.Columns.Add("PRTY_CODE", typeof(double));
            dtTRNYarnSpining.Columns.Add("FINAL_ORDER_CONF_CLAG", typeof(string));
            dtTRNYarnSpining.Columns.Add("CUST_REQ_NO", typeof(string));
            dtTRNYarnSpining.Columns.Add("TRN_NUMB", typeof(string));
            return dtTRNYarnSpining;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void RefreshTRNYarnSpiningRow()
    {
        try
        {
            txtTRNYarnSpiningUOM.Text = string.Empty;
            txtTRNYyarnRemarks.Text = string.Empty;
            lblTRNYSpinDesc.Text = string.Empty;
            txtTRNYarnSpiningOrderQty.Text = string.Empty;
            txtTRNYarnSpiningArticalCode.SelectedIndex = -1;
            txtTRNYarnSpiningArticalCode.Enabled = true;
            txtShadeCode.Enabled = true;
            txtLabDipNo.Enabled = true;
            //txtLotNo.SelectedIndex = -1;
            lblTRNYarnSpiningArticalCode.Text = string.Empty;
            txtTRNYarnSpiningDelDate.Text = string.Empty;
            txtTRNYarnSpiningSrinkage.Text = string.Empty;
            txtShadeCode.Text = string.Empty;
            txtLabDipNo.Text = string.Empty;
            lblCRNo.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnTRNYarnSpiningCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshTRNYarnSpiningRow();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearing Yarn Spining Detail Row"));
        }
    }

    protected void grdTRNYarnSpiningDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "DelTRNYarnSpiningDetail")
            {
                DeleteTRNYarnSpiningRow(UNIQUE_ID);
                BindTRNYarnSpiningGrid();
            }
            if (e.CommandName == "EditTRNYarnSpiningDetail")
            {
                txtTRNYarnSpiningArticalCode.SelectedIndex = -1;
                FillTRNYarnSpiningRowByGrid(UNIQUE_ID);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting data for updation/ deletion of Detail Row"));
        }
    }

    private void DeleteTRNYarnSpiningRow(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();

            if (grdTRNYarnSpiningDetail.Rows.Count == 1)
            {
                dtTRNYarnSpining.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRNYarnSpining.Rows)
                {
                    int IUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (IUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRNYarnSpining.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRNYarnSpining.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            Session["dtTRNYarnSpining"] = dtTRNYarnSpining;
        }
        catch
        {
            throw;
        }
    }

    private void FillTRNYarnSpiningRowByGrid(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();
            DataView dv = new DataView(dtTRNYarnSpining);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {

                ViewState["UNIQUE_ID"] = UNIQUE_ID;
                txtTRNYarnSpiningUOM.Text = dv[0]["UOM"].ToString();
                txtTRNYarnSpiningSrinkage.Text = dv[0]["SRINKAGE"].ToString();
                txtTRNYarnSpiningOrderQty.Text = dv[0]["ORD_QTY"].ToString();
                txtTRNYarnSpiningDelDate.Text = dv[0]["DEL_DATE"].ToString();
                txtTRNYarnSpiningArticalCode.Enabled = false;
                txtShadeCode.Enabled = false;
                txtLabDipNo.Enabled = false;
                lblTRNYarnSpiningArticalCode.Text = dv[0]["ARTICAL_CODE"].ToString();
                lblTRNYSpinDesc.Text = dv[0]["ARTICAL_CODE"].ToString();
                lblpi_no.Text = dv[0]["PI_NO"].ToString();
                lblCRNo.Text = dv[0]["CUST_REQ_NO"].ToString();
                txtTRNYyarnRemarks.Text = dv[0]["REMARKS"].ToString();
                txtShadeCode.Text = dv[0]["SHADE_CODE"].ToString();
                txtLabDipNo.Text = dv[0]["LAB_DIP_NO"].ToString();

            }
        }
        catch
        {
            throw;
        }
    }

    private string GetArticalCodeFromString(string sString, out string UOM, out string Description)
    {
        try
        {
            UOM = string.Empty;
            Description = string.Empty;
            char[] splitter = { '@' };
            string[] arrString = sString.Split(splitter);
            string ARTICLE_CODE = arrString[0].ToString();
            Description = arrString[1].ToString();
            UOM = arrString[2].ToString();
            return ARTICLE_CODE;
        }
        catch
        {
            throw;
        }
    }
    protected void txtTRNYarnSpiningOrderQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTRNYarnSpiningOrderQty.ReadOnly = true;
        }
        catch
        {

        }
    }

    protected void grdTRNYarnSpiningDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFINAL_ORDER_CONF_CLAG = (Label)e.Row.FindControl("lblFINAL_ORDER_CONF_CLAG");
                if (lblFINAL_ORDER_CONF_CLAG.Text.Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    LinkButton lnkbtnEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton lnkbtnDel = (LinkButton)e.Row.FindControl("lnkbtnDel");

                    lnkbtnDel.Visible = false;
                    lnkbtnEdit.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is Gridview RowDataBound. See Error log for detail"));
        }
    }

    #endregion

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            txtPartyCode.Text = txtPartyCode.Text;
            txtPartyDetail.Text = txtPartyDetail.Text.ToString();
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                saveOrder();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving Data.\r\nSee error log for detail."));
        }
    }

    private void saveOrder()
    {
        try
        {
            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.CONSIGNEE_ADD = "NA";
            oOD_CAPTURE_MST.CONSIGNEE_NAME = "NA";
            oOD_CAPTURE_MST.CONV_RATE = double.Parse(txtConversionRate.Text);
            oOD_CAPTURE_MST.CURRENCY_CODE = ddlCurrencyCode.SelectedValue.Trim();
            oOD_CAPTURE_MST.DEL_STATUS = false;
            oOD_CAPTURE_MST.ORDER_DATE = DateTime.Parse(txtOrderDate.Text);
            oOD_CAPTURE_MST.ORDER_NO = txtOrderNo.Text;
            oOD_CAPTURE_MST.ORDER_PROCESS = ddlOrderProcess.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DateTime date = System.DateTime.Now.Date;
            bool IsPartyPODate = DateTime.TryParse(txtPartyRefDate.Text, out date);
            oOD_CAPTURE_MST.PARTY_REF_DATE = date;
            oOD_CAPTURE_MST.PARTY_REF_NO = txtPartyRefNumber.Text;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;

            if (ddlOrderCategory.SelectedValue.Trim().Equals("INHOUSE"))
            {
                oOD_CAPTURE_MST.PRTY_CODE = "SELF";
            }
            else
            {
                oOD_CAPTURE_MST.PRTY_CODE = txtPartyCode.Text.Trim();
            }
            oOD_CAPTURE_MST.SHIPMENT = txtShipment.Text;
            oOD_CAPTURE_MST.REMARKS = "NA";
            oOD_CAPTURE_MST.STATUS = true;
            oOD_CAPTURE_MST.TDATE = DateTime.Now.Date;
            oOD_CAPTURE_MST.TUSER = oUserLoginDetail.UserCode;
            oOD_CAPTURE_MST.PAYMENT_MODE = "NA";
            oOD_CAPTURE_MST.PAYMENT_TERMS = "NA";
            oOD_CAPTURE_MST.BILL_TO = "NA";
            oOD_CAPTURE_MST.DELIVERY_MODE = "NA";
            oOD_CAPTURE_MST.GENERAL_INSTRUCTION = "NA";
            oOD_CAPTURE_MST.SPECIAL_INSTRUCTION = "NA";
            oOD_CAPTURE_MST.FROM_BRANCH = "NA";
            string msg_YRNSPIN = string.Empty;

            DataTable dtSewingCustAdj = null;
            if (Session["dtTRN_SUB"] != null)
                dtSewingCustAdj = (DataTable)Session["dtTRN_SUB"];

            DataTable dtMachinePlan = null;
            if (Session["dtTRN_SUB"] != null)
                dtMachinePlan = (DataTable)Session["dtTRN_SUB"];


            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();

            bool result = SaitexBL.Interface.Method.OD_CAPTURE_MST.Insert_Order_MachineWise(oOD_CAPTURE_MST, out sOrderNo, dtTRNYarnSpining, out msg_YRNSPIN, IsPartyPODate, dtSewingCustAdj, dtMachinePlan);

            if (result)
            {
                ClearPage();
                RefreshDetailRow();
                string msg = string.Empty;
                msg += "Order Number : " + sOrderNo + " saved successfully.";


                Common.CommonFuction.ShowMessage(msg);
                Response.Redirect("~/Module/Prod_plan/Pages/Prod_DyeingPlan.aspx");
            }
            //else
            //{
            //    Common.CommonFuction.ShowMessage("data Saving Failed");
            //}
        }
        catch
        {
            throw;
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            bool ReturnResult = false;

            int count = 0;
            msg = string.Empty;

            if (txtOrderNo.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Order Number required.\r\n";
            }

            if (txtPartyCode.Text != string.Empty)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select party first.\r\n";
            }



            if (ValidateTRNDataForFormSaving(ref msg))
            {
                count += 1;
            }

            if (count == 3)
                ReturnResult = true;

            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }

    private bool ValidateTRNDataForFormSaving(ref string msg)
    {
        try
        {
            bool bResult = false;
            int iCount = 0;
            int iCountAll = 0;

            if (iCount == iCountAll)
            {
                bResult = true;
            }
            else
            {
                bResult = false;
            }
            return bResult;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ActivateUpdateMode();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in update mode.\r\nSee error log for detail."));
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            lblMode.Text = "Update";

            ddlOrderCategory.Enabled = false;
            ddlBusinessType.Enabled = false;
            ddlProductType.Enabled = false;
            ddlOrderType.Enabled = false;
            txtOrderNo.Visible = false;

            ddlOrderNo.Visible = true;

            ddlOrderNo.LoadData(PRODUCT_TYPE);
        }
        catch
        {
            throw;
        }
    }

    private int GetdataByOrderNumber(string ORDER_STRING)
    {
        int iRecordFound = 0;
        try
        {

            string[] Order_strings = ORDER_STRING.Split('@');
            string sComp_code = Order_strings[0].ToString();
            string sBRANCH_CODE = Order_strings[1].ToString();
            string sBUSINESS_TYPE = Order_strings[2].ToString();
            string sPRODUCT_TYPE = Order_strings[3].ToString();
            string sORDER_CAT = Order_strings[4].ToString();
            string sORDER_TYPE = Order_strings[5].ToString();
            string sORDER_NO = Order_strings[6].ToString();

            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
            oOD_CAPTURE_MST.COMP_CODE = sComp_code;
            oOD_CAPTURE_MST.BRANCH_CODE = sBRANCH_CODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = sBUSINESS_TYPE;
            oOD_CAPTURE_MST.PRODUCT_TYPE = sPRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = sORDER_CAT;
            oOD_CAPTURE_MST.ORDER_TYPE = sORDER_TYPE;
            oOD_CAPTURE_MST.ORDER_NO = sORDER_NO;
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetORDERDATAByORDER_NO(oOD_CAPTURE_MST);

            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;

                ddlBusinessType.SelectedIndex = ddlBusinessType.Items.IndexOf(ddlBusinessType.Items.FindByValue(dt.Rows[0]["BUSINESS_TYPE"].ToString().Trim()));
                ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(dt.Rows[0]["PRODUCT_TYPE"].ToString().Trim()));
                ddlOrderCategory.SelectedIndex = ddlOrderCategory.Items.IndexOf(ddlOrderCategory.Items.FindByValue(dt.Rows[0]["ORDER_CAT"].ToString().Trim()));
                ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue(dt.Rows[0]["ORDER_TYPE"].ToString().Trim()));

                txtOrderNo.Text = dt.Rows[0]["ORDER_NO"].ToString().Trim();
                txtOrderDate.Text = DateTime.Parse(dt.Rows[0]["ORDER_DATE"].ToString().Trim()).ToShortDateString();
                ddlCurrencyCode.SelectedIndex = ddlCurrencyCode.Items.IndexOf(ddlCurrencyCode.Items.FindByValue(dt.Rows[0]["CURRENCY_CODE"].ToString().Trim()));
                txtConversionRate.Text = dt.Rows[0]["CONV_RATE"].ToString().Trim();



                if (dt.Rows[0]["ORDER_CAT"].ToString().Trim().Equals("INHOUSE"))
                {
                    OrdercategorySelection();
                }
                else
                {
                    txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();

                    txtPartyDetail.Text = txtPartyCodecmb.SelectedText.Trim();
                }
            }

            if (iRecordFound == 1)
            {
                //Code For Yarn Spining 
                DataTable dtTemp_YRNSPIN = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetTRN_ByORDER_NO(oOD_CAPTURE_MST);

                if (dtTemp_YRNSPIN != null && dtTemp_YRNSPIN.Rows.Count > 0)
                {
                    DataTable dtADJ = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetADJ_ByORDER_NO(oOD_CAPTURE_MST);
                    if (dtADJ != null & dt.Rows.Count > 0)
                    {
                        Session["dtYarnSpinningCustAdj"] = dtADJ;
                    }


                    MapDataTable_YRNSPIN(dtTemp_YRNSPIN);


                    BindTRNYarnSpiningGrid();
                }
            }

            return iRecordFound;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void MapDataTable_YRNSPIN(DataTable dtTemp)
    {
        try
        {
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();
            dtTRNYarnSpining.Rows.Clear();



            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtTRNYarnSpining.NewRow();

                    dr["UNIQUE_ID"] = dtTRNYarnSpining.Rows.Count + 1;
                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["PI_NO"] = drTemp["PI_NO"];
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["GREY_LOT_NO"] = drTemp["GREY_LOT_NO"];
                    dr["REMARKS"] = drTemp["REMARKS"];
                    dr["UOM"] = drTemp["UOM"];
                    dr["ORD_QTY"] = drTemp["ORD_QTY"];
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"];
                    dr["LAB_DIP_NO"] = drTemp["LAB_DIP_NO"];
                    dr["SRINKAGE"] = drTemp["SRINKAGE"];
                    dr["DEL_DATE"] = drTemp["DEL_DATE"];
                    dr["FINAL_ORDER_CONF_CLAG"] = drTemp["FINAL_ORDER_CONF_CLAG"];
                    dr["CUST_REQ_NO"] = drTemp["CUST_REQ_NO"];


                    dtTRNYarnSpining.Rows.Add(dr);
                    Session["dtTRNYarnSpining"] = dtTRNYarnSpining;

                }
                dtTemp = null;
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable_YRNSPIN_ADJ(DataTable dtTemp)
    {
        try
        {

            if (Session["dtYarnSpinningCustAdj"] != null)
                Session["dtYarnSpinningCustAdj"] = null;

            DataTable dtSewingCustAdj = createAdjTable();

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtSewingCustAdj.NewRow();

                    dr["PI_TYPE"] = drTemp["PI_TYPE"];
                    dr["ARTICAL_CODE"] = drTemp["ARTICAL_CODE"];
                    dr["GREY_LOT_NO"] = drTemp["GREY_LOT_NO"];
                    dr["REMARKS"] = drTemp["REMARKS"];
                    dr["SALE"] = drTemp["SALE"];
                    dr["FREIGHT"] = drTemp["FREIGHT"];
                    dr["COMMISSION"] = drTemp["COMMISSION"];
                    dr["BROKERAGE"] = drTemp["BROKERAGE"];
                    dr["INCENTIVES"] = drTemp["INCENTIVES"];
                    dr["EX_MILL_RATE"] = drTemp["EX_MILL_RATE"];
                    dr["TOTAL"] = drTemp["TOTAL"];
                    dr["COST_REMARKS"] = drTemp["COST_REMARKS"];

                    // ADDED ON 11 MAY 2011

                    dr["FOB"] = drTemp["FOB"];
                    dr["HANDLING_CHARGES"] = drTemp["HANDLING_CHARGES"];
                    dr["BILL_D_CHARGES"] = drTemp["BILL_D_CHARGES"];
                    dr["EXPORT_INCENTIVES"] = drTemp["EXPORT_INCENTIVES"];
                    dr["EXPORT_INCENTIVES_AMT"] = drTemp["EXPORT_INCENTIVES_AMT"];
                    dr["OTHER_COST"] = drTemp["OTHER_COST"];

                    dtSewingCustAdj.Rows.Add(dr);
                }
                dtTemp = null;
                Session["dtYarnSpinningCustAdj"] = dtSewingCustAdj;
            }
        }
        catch
        {
            throw;
        }
    }




    private DataTable createAdjTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ORDER_NO", typeof(string));
        dt.Columns.Add("ARTICAL_CODE", typeof(string));
        dt.Columns.Add("SHADE_CODE", typeof(string));
        dt.Columns.Add("PRODUCT_TYPE", typeof(string));
        dt.Columns.Add("PI_TYPE", typeof(string));
        dt.Columns.Add("PI_NO", typeof(string));
        dt.Columns.Add("CR_COMP_CODE", typeof(string));
        dt.Columns.Add("CR_BRANCH_CODE", typeof(string));
        dt.Columns.Add("CR_YEAR", typeof(int));
        dt.Columns.Add("CR_ORDER_TYPE", typeof(string));
        dt.Columns.Add("CR_ORDER_CAT", typeof(string));
        dt.Columns.Add("CR_PRODUCT_TYPE", typeof(string));
        dt.Columns.Add("CR_BUSINESS_TYPE", typeof(string));
        dt.Columns.Add("CR_ST_ORDER_NO", typeof(string));
        dt.Columns.Add("CR_ST_ARTICLE_NO", typeof(string));
        dt.Columns.Add("CR_ST_SUBSTRATE", typeof(string));
        dt.Columns.Add("CR_ST_COUNT", typeof(string));
        dt.Columns.Add("CR_ST_SHADE_FAMILY_CODE", typeof(string));
        dt.Columns.Add("CR_ST_SHADE_CODE", typeof(string));
        dt.Columns.Add("CR_YRN_COUNT", typeof(int));
        dt.Columns.Add("CR_YRN_PLY", typeof(string));
        dt.Columns.Add("ADJ_QTY", typeof(double));
        return dt;
    }



    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                UpdateOrder();
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating Data.\r\nSee error log for detail."));
        }

    }

    private void UpdateOrder()
    {
        try
        {
            oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = ddlBusinessType.SelectedValue.Trim();
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;

            oOD_CAPTURE_MST.CONV_RATE = double.Parse(txtConversionRate.Text);
            oOD_CAPTURE_MST.CURRENCY_CODE = ddlCurrencyCode.SelectedValue.Trim();
            oOD_CAPTURE_MST.DEL_STATUS = false;
            oOD_CAPTURE_MST.ORDER_DATE = DateTime.Parse(txtOrderDate.Text);
            oOD_CAPTURE_MST.ORDER_NO = txtOrderNo.Text.Trim();
            oOD_CAPTURE_MST.ORDER_TYPE = ddlOrderType.SelectedValue.Trim();
            oOD_CAPTURE_MST.ORDER_CAT = ddlOrderCategory.SelectedValue.Trim();

            DateTime date = System.DateTime.Now.Date;

            oOD_CAPTURE_MST.PARTY_REF_DATE = date;

            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            if (ddlOrderCategory.SelectedValue.Trim().Equals("INHOUSE"))
            {
                oOD_CAPTURE_MST.PRTY_CODE = "SELF";
            }
            else
            {
                oOD_CAPTURE_MST.PRTY_CODE = txtPartyCode.Text.Trim();
            }



            oOD_CAPTURE_MST.STATUS = true;
            oOD_CAPTURE_MST.TDATE = DateTime.Now.Date;
            oOD_CAPTURE_MST.TUSER = oUserLoginDetail.UserCode;


            string ORDER_NO = txtOrderNo.Text;
            string msg_YRNSPIN = string.Empty;


            DataTable dtSewingCustAdj = null;
            if (Session["dtYarnSpinningCustAdj"] != null)
            {
                dtSewingCustAdj = (DataTable)Session["dtYarnSpinningCustAdj"];
            }
            if (Session["dtTRNYarnSpining"] != null)
                dtTRNYarnSpining = (DataTable)Session["dtTRNYarnSpining"];
            else
                dtTRNYarnSpining = CreateTRNYarnSpiningTable();

            //bool result = SaitexBL.Interface.Method.OD_CAPTURE_MST.Update_Order(oOD_CAPTURE_MST, dtTRNYarnSpining, out msg_YRNSPIN, IsPartyPODate, dtSewingCustAdj);
            //if (result)
            {
                ClearPage();
                string msg = string.Empty;
                msg += "Order Number : " + ORDER_NO + " Updated successfully.";



                Common.CommonFuction.ShowMessage(msg);

            }
            //else
            //{
            //    Common.CommonFuction.ShowMessage("Data Updation Failed");
            //}
        }
        catch
        {
            throw;
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
        Response.Redirect("~/Module/OrderDevelopment/Reports/OC_Parameter.aspx?PRODUCT_TYPE=" + PRODUCT_TYPE, false);
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

    protected void btnAdjCustReq_Click(object sender, EventArgs e)
    {
        if (lblTRNYarnSpiningArticalCode.Text != string.Empty)
        {
            // if (PRODUCT_TYPE == "YARN DYEING")
            //{
            //    try
            //    {
            //        if (lblTRNYarnSpiningArticalCode.Text != string.Empty)
            //        {
            //            txtTRNYarnSpiningOrderQty.ReadOnly = false;//Code to enable TxtQty for insert

            //            string URL = "OCYarnCustAdjustment.aspx";
            //            URL = URL + "?YARN_CODE=" + lblTRNYarnSpiningArticalCode.Text.Trim();
            //            URL = URL + "&SHADE_CODE=" + txtShadeCode.Text.Trim();
            //            URL = URL + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
            //            URL = URL + "&PI_TYPE=" + PI_TYPE;
            //            URL = URL + "&txtQTY=" + txtTRNYarnSpiningOrderQty.ClientID;
            //            URL = URL + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.ToString();
            //            URL = URL + "&ORDER_CAT=" + ddlOrderCategory.SelectedValue.ToString();
            //            URL = URL + "&ORDER_TYPE=" + ddlOrderType.SelectedValue.ToString();
            //            URL = URL + "&ORDER_NO=" + txtOrderNo.Text.Trim();
            //            URL = URL + "&CR_NO=" + lblCRNo.Text.Trim();

            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            //        }
            //        else
            //        {
            //            Common.CommonFuction.ShowMessage("Please select Article Number");
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}

        }

    }

    protected void txtTRNYarnSpiningArticalCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtTRNYarnSpiningArticalCode.Items.Clear();
                txtTRNYarnSpiningArticalCode.DataSource = data;
                txtTRNYarnSpiningArticalCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article  loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;
            string whereClause = string.Empty;
            string SortExpression = string.Empty;

            if (ddlProductType.SelectedValue.Trim() == "TEXTURISED YARN" || ddlProductType.SelectedValue.Trim() == "TWISTED YARN" || ddlProductType.SelectedValue.Trim() == "YARN DYEING")
            {

                CommandText = "SELECT   *  FROM   (SELECT   * FROM  (SELECT   * FROM  (  SELECT  YA.ASS_YARN_CODE AS ARTICAL_CODE,  YA.ASS_YARN_DESC AS ARTICAL_DESC,    M.YARN_TYPE,    M.COLOUR,ST.SHADE_CODE,           ST.ORDER_NO,YA.ASS_YARN_CODE   || '@'||YA.ASS_YARN_DESC|| '@'|| M.UOM || '@' || M.TRANSFER_PRICE|| '@'|| ST.SHADE_CODE|| '@' || ST.MATCHING_REFF || '@' || ST.QTY_APPROVED|| '@'|| ST.ORDER_NO ||'@'||ST.REMARKS AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0) AS QTYBAL        FROM   YRN_MST M, YRN_ASSOCATED_MST YA,  OD_CUSTOMER_REQUEST_ST st, OD_CUSTOMER_REQUEST_mst A  WHERE   M.YARN_CODE=YA.YARN_CODE AND  st.PRODUCT_TYPE = '" + ddlProductType.SelectedValue + "'  AND A.PRTY_CODE = '" + txtPartyCode.Text + "'  AND A.YEAR=ST.YEAR AND a.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + "    AND YA.ASS_YARN_CODE = ST.ARTICLE_NO AND ST.ORDER_NO = A.ORDER_NO   AND ST.PRODUCT_TYPE = A.PRODUCT_TYPE AND ST.CONF_FLAG = 1 AND ST.ORDER_TYPE NOT IN ('FROM_STOCK') and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0  AND   ( NVL (St.QTY_APPROVED, 0) - NVL (ST.INVOICE_QTY, 0))>0  ORDER BY   M.YARN_CODE)  WHERE   ORDER_NO NOT IN (SELECT   DISTINCT AFT.ORDER_NO FROM   OD_CAPT_TRN_MAIN AFT    WHERE   AFT.STATUS = '1'))asd  WHERE asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd ";
                if (startOffset != 0)
                {
                    whereClause += "  AND (ARTICAL_CODE,SHADE_CODE) NOT IN(SELECT   *  FROM   (SELECT   * FROM  (SELECT   * FROM  (  SELECT  YA.ASS_YARN_CODE AS ARTICAL_CODE,  YA.ASS_YARN_DESC AS ARTICAL_DESC,    M.YARN_TYPE,    M.COLOUR,ST.SHADE_CODE,           ST.ORDER_NO,YA.ASS_YARN_CODE   || '@'||YA.ASS_YARN_DESC|| '@'|| M.UOM || '@' || M.TRANSFER_PRICE|| '@'|| ST.SHADE_CODE|| '@' || ST.MATCHING_REFF ||'@'|| ST.QTY_APPROVED|| '@'|| ST.ORDER_NO ||'@'||ST.REMARKS AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0) AS QTYBAL        FROM   YRN_MST M, YRN_ASSOCATED_MST YA,  OD_CUSTOMER_REQUEST_ST st, OD_CUSTOMER_REQUEST_mst A  WHERE   M.YARN_CODE=YA.YARN_CODE AND   m.YARN_TYPE <> 'SEWING THREAD' AND st.PRODUCT_TYPE = '" + ddlProductType.SelectedValue + "'  AND A.PRTY_CODE = '" + txtPartyCode.Text + "'  AND A.YEAR=ST.YEAR AND a.YEAR =" + oUserLoginDetail.DT_STARTDATE.Year + "    AND YA.ASS_YARN_CODE = ST.ARTICLE_NO AND ST.ORDER_NO = A.ORDER_NO   AND ST.PRODUCT_TYPE = A.PRODUCT_TYPE AND ST.CONF_FLAG = 1 AND ST.ORDER_TYPE NOT IN ('FROM_STOCK') and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 AND  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.INVOICE_QTY, 0))>0  ORDER BY   M.YARN_CODE)  WHERE   ORDER_NO NOT IN (SELECT   DISTINCT AFT.ORDER_NO FROM   OD_CAPT_TRN_MAIN AFT    WHERE   AFT.STATUS = '1'))asd  WHERE asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd  WHERE ROWNUM <= " + startOffset + ")";
                }
            }
            else if (ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
            {
                CommandText = "SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE as ARTICAL_CODE, M.YARN_DESC as ARTICAL_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE  YARN_TYPE = 'SEWING THREAD' AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE   asd.ARTICAL_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.ARTICAL_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd WHERE   ROWNUM <= 15  ";

                if (startOffset != 0)
                {
                    whereClause += "  AND (ARTICAL_CODE,SHADE_CODE) NOT IN(SELECT   YARN_code as ARTICAL_CODE,SHADE_CODE FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR,           ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE || '@' || ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0)    AS QUANTITY, NVL (ST.QTY_APPROVED, 0)    AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL     FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st    WHERE   m.YARN_TYPE = 'SEWING THREAD'         AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY M.YARN_CODE) asd WHERE asd.YARN_CODE LIKE :SearchQuery OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd WHERE  ROWNUM <= " + startOffset + ")";
                }
            }
            else if (ddlProductType.SelectedValue.Trim() == "FABRIC")
            {
                CommandText = "SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.FABR_CODE as ARTICAL_CODE, M.FABR_DESC as ARTICAL_DESC, M.FABR_TYPE,   ST.SHADE_CODE,ST.ORDER_NO,    M.FABR_CODE || '@' || M.FABR_DESC || '@' || M.UOM || '@' || M.TRANSFER_RATE || '@' || M.FABR_TYPE || '@' || ST.QTY_APPROVED || '@' || ST.ORDER_NO   AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   TX_FABRIC_MST M, OD_CUSTOMER_REQUEST_ST st            WHERE        M.FABR_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 AND (NVL (St.QTY_APPROVED, 0)      - NVL (ST.ADJUST_QTY, 0)) > 0         ORDER BY   M.FABR_CODE) asd           WHERE      asd.ARTICAL_CODE LIKE :SearchQuery        OR asd.FABR_TYPE LIKE :SearchQuery        OR asd.ARTICAL_DESC LIKE :SearchQuery        OR asd.ORDER_NO LIKE :SearchQuery) bd WHERE   ROWNUM <= 15 ";

                if (startOffset != 0)
                {
                    whereClause += "  AND (ARTICAL_CODE, SHADE_CODE) NOT IN (SELECT   FABR_CODE as ARTICAL_CODE, SHADE_CODE   FROM   (SELECT   * FROM   (  SELECT   M.FABR_CODE,M.FABR_DESC, M.FABR_TYPE,  ST.SHADE_CODE,   ST.ORDER_NO,    M.FABR_CODE || '@' || M.FABR_DESC || '@'|| M.UOM || '@' || M.TRANSFER_PRICE || '@' || M.FABR_TYPE || '@' || ST.QTY_APPROVED || '@' || ST.ORDER_NO     AS Combined, NVL (ST.QUANTITY, 0)   AS QUANTITY, NVL (ST.QTY_APPROVED, 0)    AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL             FROM   TX_FABRIC_MST M, OD_CUSTOMER_REQUEST_ST st            WHERE    M.FABR_CODE =       ST.ARTICLE_NO AND ST.CONF_FLAG = 1 AND (NVL (St.QTY_APPROVED, 0)      - NVL (ST.ADJUST_QTY, 0)) >       0        ORDER BY   M.FABR_CODE) asd           WHERE      asd.FABR_CODE LIKE :SearchQuery        OR asd.FABR_TYPE LIKE :SearchQuery        OR asd.FABR_DESC LIKE :SearchQuery        OR asd.ORDER_NO LIKE :SearchQuery) bd WHERE   ROWNUM <= " + startOffset + ")  ";
                }
            }

            SortExpression = " ORDER BY ARTICAL_CODE";


            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = string.Empty;
        string SortExpression = string.Empty;
        if (ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
        {
            CommandText = "  SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE  YARN_TYPE = 'SEWING THREAD' AND  M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE   asd.YARN_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd ";
        }
        else if (ddlProductType.SelectedValue.Trim() == "YARN DYEING")
        {
            CommandText = " SELECT   *  FROM   (SELECT   * FROM   (  SELECT   M.YARN_CODE, M.YARN_DESC, M.YARN_TYPE, M.COLOUR, ST.SHADE_CODE,    M.YARN_CODE || '@' || M.YARN_DESC || '@' || M.UOM || '@' || M.TRANSFER_PRICE  || '@' ||ST.SHADE_CODE    AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL  FROM   YRN_MST M, OD_CUSTOMER_REQUEST_ST st WHERE   m.YARN_TYPE <> 'SEWING THREAD' AND   M.YARN_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 and  ( NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0))>0 ORDER BY   M.YARN_CODE) asd  WHERE      asd.YARN_CODE LIKE :SearchQuery  OR asd.YARN_TYPE LIKE :SearchQuery OR asd.YARN_DESC LIKE :SearchQuery or asd.SHADE_CODE like :SearchQuery) bd ";
        }
        else if (ddlProductType.SelectedValue.Trim() == "FABRIC")
        {
            CommandText = " SELECT   *  FROM   (SELECT   *            FROM   (  SELECT   M.FABR_CODE, M.FABR_DESC, M.FABR_TYPE, ST.ORDER_NO,    M.FABR_CODE || '@' || M.FABR_DESC || '@' || M.UOM || '@' || M.TRANSFER_RATE || '@' || M.FABR_TYPE || '@' ||  ST.ORDER_NO   AS Combined, NVL (ST.QUANTITY, 0) AS QUANTITY, NVL (ST.QTY_APPROVED, 0) AS QTY_APPROVED, NVL (St.QTY_APPROVED, 0) - NVL (ST.ADJUST_QTY, 0)    AS QTYBAL FROM   TX_FABRIC_MST M, OD_CUSTOMER_REQUEST_ST st WHERE       M.FABR_CODE = ST.ARTICLE_NO AND ST.CONF_FLAG = 1 AND (NVL (St.QTY_APPROVED, 0)      - NVL (ST.ADJUST_QTY, 0)) > 0 ORDER BY   M.FABR_CODE) asd WHERE      asd.FABR_CODE LIKE :SearchQuery OR asd.FABR_TYPE LIKE :SearchQuery OR asd.FABR_DESC LIKE :SearchQuery) bd  ";
        }
        string WhereClause = " ";
        if (ddlProductType.SelectedValue.Trim() == "FABRIC")
        {
            SortExpression = " ORDER BY FABR_CODE";
        }
        else if (ddlProductType.SelectedValue.Trim() == "YARN DYEING" || ddlProductType.SelectedValue.Trim() == "SEWING THREAD")
        {
            SortExpression = " ORDER BY YARN_code";

        }
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }



    protected void txtTRNYarnSpiningArticalCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string cString = txtTRNYarnSpiningArticalCode.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string YARN_CODE = arrString[0].ToString();
            string YARN_DESC = arrString[1].ToString();
            string UOM = arrString[2].ToString();
            //string TRANSFER_PRICE = arrString[3].ToString();
            string shadeCode = arrString[4].ToString();
            string LABDIPNO = arrString[5].ToString();
            string CRNO = arrString[7].ToString();
            string REMARKS = arrString[8].ToString();
            // string approvedQty=arrString[5].ToString();
            // string LotId="";
            lblTRNYarnSpiningArticalCode.Text = YARN_CODE;
            lblTRNYSpinDesc.Text = YARN_DESC;
            txtTRNYarnSpiningUOM.Text = UOM;
            txtShadeCode.Text = shadeCode;
            txtLabDipNo.Text = LABDIPNO;
            lblCRNo.Text = CRNO;
            txtTRNYyarnRemarks.Text = REMARKS;
            // txtTRNYarnSpiningOrderQty.Text = approvedQty;
            //GenerateLotId();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem Article Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }



    protected void btnTrnSave_Click(object sender, EventArgs e)
    {
        SaveTRNYarnSpiningRow();
    }

    protected void btnTrnCancel_Click(object sender, EventArgs e)
    {
        RefreshTRNYarnSpiningRow();
    }

    protected void txtPartyCodecmb_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCodecmb.Items.Clear();
            txtPartyCodecmb.DataSource = data;
            txtPartyCodecmb.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   DISTINCT  B.PRTY_CODE, A.PRTY_NAME, A.PRTY_ADD1,  (A.PRTY_NAME || A.PRTY_ADD1) ADDRESS,A.PRTY_GRP_CODE, A.VENDOR_CAT_CODE    FROM   TX_VENDOR_MST A,   OD_CUSTOMER_REQUEST_MST B,  OD_CUSTOMER_REQUEST_ST C  WHERE   (B.PRTY_CODE LIKE :SearchQuery       OR A.PRTY_NAME LIKE :SearchQuery)  AND A.PRTY_CODE = B.PRTY_CODE       AND B.ORDER_NO = C.ORDER_NO      AND C.CONF_FLAG = '1' AND C.ORDER_TYPE NOT IN ('FROM_STOCK') AND  UPPER (PRTY_GRP_CODE) IN (UPPER ('18'))     AND UPPER (VENDOR_CAT_CODE) IN (UPPER ('PARTY'))    AND B.PRODUCT_TYPE =  '" + ddlProductType.SelectedValue + "'         AND  B.ORDER_NO  IN (  SELECT   DISTINCT AFT.ORDER_NO       FROM   OD_CUSTOMER_REQUEST_ST AFT         WHERE  ( NVL(AFT.QTY_APPROVED,0) - NVL(AFT.ADJUST_QTY,0))>0    )   AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT   DISTINCT  B.PRTY_CODE, A.PRTY_NAME, A.PRTY_ADD1,  (A.PRTY_NAME || A.PRTY_ADD1) ADDRESS,A.PRTY_GRP_CODE, A.VENDOR_CAT_CODE    FROM   TX_VENDOR_MST A,   OD_CUSTOMER_REQUEST_MST B,  OD_CUSTOMER_REQUEST_ST C  WHERE   (B.PRTY_CODE LIKE :SearchQuery       OR A.PRTY_NAME LIKE :SearchQuery)  AND A.PRTY_CODE = B.PRTY_CODE       AND B.ORDER_NO = C.ORDER_NO      AND C.CONF_FLAG = '1' AND C.ORDER_TYPE NOT IN ('FROM_STOCK') AND  UPPER (PRTY_GRP_CODE) IN (UPPER ('18'))     AND UPPER (VENDOR_CAT_CODE) IN (UPPER ('PARTY'))    AND B.PRODUCT_TYPE =  '" + ddlProductType.SelectedValue + "'         AND  B.ORDER_NO  IN (  SELECT   DISTINCT AFT.ORDER_NO       FROM   OD_CUSTOMER_REQUEST_ST AFT         WHERE  ( NVL(AFT.QTY_APPROVED,0) - NVL(AFT.ADJUST_QTY,0))>0     ) and ROWNUM <= " + startOffset + ")";
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

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT   DISTINCT  B.PRTY_CODE, A.PRTY_NAME, A.PRTY_ADD1,  (A.PRTY_NAME || A.PRTY_ADD1) ADDRESS,A.PRTY_GRP_CODE, A.VENDOR_CAT_CODE     FROM   TX_VENDOR_MST A,   OD_CUSTOMER_REQUEST_MST B,  OD_CUSTOMER_REQUEST_ST C  WHERE   (B.PRTY_CODE LIKE :SearchQuery       OR A.PRTY_NAME LIKE :SearchQuery)  AND A.PRTY_CODE = B.PRTY_CODE       AND B.ORDER_NO = C.ORDER_NO AND C.CONF_FLAG = '1' AND C.ORDER_TYPE NOT IN ('FROM_STOCK') AND  UPPER (PRTY_GRP_CODE) IN (UPPER ('18'))     AND UPPER (VENDOR_CAT_CODE) IN (UPPER ('PARTY'))    AND B.PRODUCT_TYPE =  '" + ddlProductType.SelectedValue + "'         AND  B.ORDER_NO  IN (  SELECT   DISTINCT AFT.ORDER_NO       FROM   OD_CUSTOMER_REQUEST_ST AFT         WHERE  ( NVL(AFT.QTY_APPROVED,0) - NVL(AFT.ADJUST_QTY,0))>0 ) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count; ;


    }

    protected void txtPartyCodecmb_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {

            txtPartyCode.Text = txtPartyCodecmb.SelectedText.ToString();
            txtPartyDetail.Text = txtPartyCodecmb.SelectedValue.ToString();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlShadeCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlCurrencyCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Sale_Order_Production_Machine_For_Yarn_Dyeing_" + DateTime.Now.ToShortDateString() + ".xls";


        ExporttoExcel(bindCustomerRequestApproval1(), strFilename, "Order Pending  For  Dyeing");
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



}