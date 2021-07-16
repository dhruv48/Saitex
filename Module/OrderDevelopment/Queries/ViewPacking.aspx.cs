using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using errorLog;
using System.Data;
public partial class Module_OrderDevelopment_Queries_ViewPacking : System.Web.UI.Page
{
    //URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
    //URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
    //URL = URL + "&ORDER_NO=" + lblORDER_NO.Text;
    //URL = URL + "&PI_NO=" + lblPI_NO.Text;

    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    private static DataTable dtTRN_BOM = null;

    string COMP_CODE = string.Empty;
    string BRANCH_CODE = string.Empty;
    string ORDER_NO = string.Empty;
    string PI_NO = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["COMP_CODE"] != null)
                {
                    COMP_CODE = Request.QueryString["COMP_CODE"].Trim();
                }
                if (Request.QueryString["BRANCH_CODE"] != null)
                {
                    BRANCH_CODE = Request.QueryString["BRANCH_CODE"].Trim();
                }

                if (Request.QueryString["ORDER_NO"] != null)
                {
                    ORDER_NO = Request.QueryString["ORDER_NO"].Trim();
                }

                if (Request.QueryString["PI_NO"] != null)
                {
                    PI_NO = Request.QueryString["PI_NO"].Trim();
                }



                SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
                oOD_CAPTURE_MST.COMP_CODE = COMP_CODE;
                oOD_CAPTURE_MST.BRANCH_CODE = BRANCH_CODE;
                oOD_CAPTURE_MST.ORDER_NO = ORDER_NO;

                DataTable dt = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetPaking_ByORDER_NO(oOD_CAPTURE_MST, PI_NO);
                BindGrid(dt);

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    private void BindGrid(DataTable dt)
    {
        try
        {

            //DataView dv = new DataView(dt);
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("COMP_CODE='" + COMP_CODE + "' AND ");
            //sb.Append("BRANCH_CODE='" + BRANCH_CODE + "' AND ");
            //sb.Append("BUSINESS_TYPE='" + BUSINESS_TYPE + "' AND ");
            //sb.Append("PRODUCT_TYPE='" + PRODUCT_TYPE + "' AND ");
            //sb.Append("ORDER_CAT='" + ORDER_CAT + "' AND ");
            //sb.Append("ORDER_TYPE='" + ORDER_TYPE + "' AND ");
            //sb.Append("ORDER_NO='" + ORDER_NO + "' AND ");
            //sb.Append("PI_TYPE='" + PI_TYPE + "' AND ");
            //sb.Append("PI_NO='" + PI_NO + "' AND ");
            //sb.Append("ARTICAL_CODE='" + ARTICAL_CODE + "' AND ");
            //sb.Append("SHADE_CODE='" + SHADE_CODE + "'");
            //dv.RowFilter = sb.ToString();
            if (dt != null && dt.Rows.Count > 0)
            {
                grdViewCR.DataSource = dt;
                grdViewCR.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string BOM = string.Empty;
            string TextBoxBOM = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNSPIN_BOM('" + BOM + "','" + TextBoxBOM + "')", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }
    
    protected void btnCompCode_Click(object sender, ImageClickEventArgs e)
    {
        //SearchbyKeywords();
    }
    
    private void SearchbyKeywords()
    {
        try
        {

            string COMP_CODE = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCompCode")).Text;
            string BRANCH_NAME = ((TextBox)grdViewCR.HeaderRow.FindControl("txtBranchCode")).Text;
            string CR_YEAR = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCrYear")).Text;
            string CR_ORDER_TYPE = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCROrderType")).Text;
            string CR_ORDER_CAT = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCROrderCat")).Text;
            string CR_PRODUCT_TYPE = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCRProductType")).Text;
            string CR_BUSINESS_TYPE = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCRBusinessType")).Text;
            string CR_PRTY_NAME = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCRParty")).Text;
            string CR_ST_ORDER_NO = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCrNo")).Text;
            string CR_ST_ARTICLE_NO = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCRArticle")).Text;
            string CR_ST_SUBSTRATE = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCRSubstrate")).Text;
            string CR_ST_SHADE_FAMILY_CODE = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCRShadeFamilyCode")).Text;
            string CR_ST_SHADE_CODE = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCRShadeCode")).Text;
            string ADJ_QTY = ((TextBox)grdViewCR.HeaderRow.FindControl("txtCRAdjQty")).Text;

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.SearchDataByfilterCR(COMP_CODE, BRANCH_NAME, CR_BUSINESS_TYPE, int.Parse(CR_YEAR), CR_ORDER_TYPE, CR_ORDER_CAT, CR_PRODUCT_TYPE, CR_BUSINESS_TYPE, CR_PRTY_NAME, CR_ST_ORDER_NO, CR_ST_ARTICLE_NO, CR_ST_SUBSTRATE, CR_ST_SHADE_FAMILY_CODE, CR_ST_SHADE_CODE, double.Parse(ADJ_QTY));
            if (dt != null && dt.Rows.Count > 0)
            {
                grdViewCR.DataSource = dt;
                grdViewCR.DataBind();
                //AutofillSearchContent(BranchName, BusinessType, ProductType, OrderCat, OrderType, OrderNo, PrtyCode, PrtyName, PiType, PiNo, ArticleCode, Articaldesc, Uom);

            }
            else
            {
                //DataTable dtblank = CreateDataTable();
                //DataRow dr = dtblank.NewRow();
                //dr["COMP_CODE"] = string.Empty;
                //dtblank.Rows.Add(dr);
                grdViewCR.DataSource = null;
                grdViewCR.DataBind();
                //AutofillSearchContent(BranchName, BusinessType, ProductType, OrderCat, OrderType, OrderNo, PrtyCode, PrtyName, PiType, PiNo, ArticleCode, Articaldesc, Uom);

            }

        }
        catch
        {
            throw;
        }


    }



   
    protected void BtnBranchName_Click(object sender, ImageClickEventArgs e)
    {

    }
}
