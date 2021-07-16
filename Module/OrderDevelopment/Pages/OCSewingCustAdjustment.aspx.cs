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

public partial class Module_OrderDevelopment_Pages_OCSewingCustAdjustment : System.Web.UI.Page
{
    private static string txtQTY;
    private static string PRODUCT_TYPE;
    private static string SHADE_CODE;
    private static string YARN_CODE;
    private static string PI_TYPE;

    private static string BUSINESS_TYPE;
    private static string ORDER_CAT;
    private static string ORDER_TYPE;
    private static string ORDER_NO;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (Request.QueryString["PI_TYPE"] != null)
            {
                PI_TYPE = Request.QueryString["PI_TYPE"].ToString();
            }
            if (Request.QueryString["PRODUCT_TYPE"] != null)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }

            if (Request.QueryString["SHADE_CODE"] != null)
            {
                SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();

            }
            if (Request.QueryString["txtQTY"] != null)
            {
                txtQTY = Request.QueryString["txtQTY"].ToString();
            }

            if (Request.QueryString["BUSINESS_TYPE"] != null)
            {
                BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].ToString();
            }
            if (Request.QueryString["ORDER_CAT"] != null)
            {
                ORDER_CAT = Request.QueryString["ORDER_CAT"].ToString();
            }
            if (Request.QueryString["ORDER_TYPE"] != null)
            {
                ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
            }
            if (Request.QueryString["ORDER_NO"] != null)
            {
                ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
            }
            if (Request.QueryString["YARN_CODE"] != null)
            {
                 YARN_CODE = Request.QueryString["YARN_CODE"].ToString();
                GetDataForItemAdjustment(YARN_CODE);
            }

        }
    }
    
    private void GetDataForItemAdjustment(string YARN_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtCustReqAdjustment = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetCustomerRequestAdjustmentByArticleCode(oUserLoginDetail.COMP_CODE, YARN_CODE, SHADE_CODE, PRODUCT_TYPE,oUserLoginDetail.CH_BRANCHCODE,BUSINESS_TYPE,ORDER_CAT,ORDER_TYPE,ORDER_NO,"",oUserLoginDetail.DT_STARTDATE.Year);
            if (dtCustReqAdjustment != null && dtCustReqAdjustment.Rows.Count > 0)
            {

                grdCustAdjustment.DataSource = dtCustReqAdjustment;
                grdCustAdjustment.DataBind();
                lblAdjustArticleCode.Text = YARN_CODE;
                lblShadeCode.Text = SHADE_CODE;
            }
            else
            {
                lblIndentAdjustmentError.Text = "No Record exists for adjustment for provided Article";
            }
        }
        catch (Exception ex)
        {
            lblIndentAdjustmentError.Text = ex.Message;
        }

    }
    
    protected void txtAdjustedIndentQTY_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            Label txtFinalQTY = (Label)grdCustAdjustment.FooterRow.FindControl("txtTotalAdjustedIndentQTY");
            txtFinalQTY.Text = GetFinalTotalOfCustomerAdjustment().ToString();

        }
        catch (Exception ex)
        {
            lblIndentAdjustmentError.Text = ex.Message;
        }

    }

   
    protected void btnAdjustIndentItem_Click(object sender, EventArgs e)
            {
        try
        {
            double TotalQTY = 0;
            DataTable dtSewingCustAdj = createdatatableforadjustment(out TotalQTY);
            Session["dtSewingCustAdj"] = dtSewingCustAdj;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + TotalQTY + "','" + txtQTY + "')", true);
        }
        catch (Exception ex)
        {
            lblIndentAdjustmentError.Text = ex.Message;
        }
    }
    //private DataTable createdatatableforadjustment(out double TotalQTY)
    //{
    //    //try
    //    //{
    //    //    TotalQTY = 0;
    //    //    DataTable dtSewingCustAdj = new DataTable();
    //    //    if (Session["dtSewingCustAdj"] == null)
    //    //        dtSewingCustAdj = createAdjTable();
    //    //    else
    //    //    {
    //    //        dtSewingCustAdj.Rows.Clear();
    //    //        dtSewingCustAdj = (DataTable)Session["dtSewingCustAdj"];
    //    //        //if (!dtSewingCustAdj.Columns.Contains("ADJUST_QTY"))
    //    //        //{
    //    //        //    dtSewingCustAdj.Columns.Add("ADJUST_QTY", typeof(int));
    //    //        ////}
    //    //    }
    //    //    for (int iLoop = 0; iLoop < grdCustAdjustment.Rows.Count; iLoop++)
    //    //    {
    //    //        Label lblAdjustIndentNuber = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblAdjustIndentNuber");
    //    //        string ORDERNo = lblAdjustIndentNuber.Text.Trim();

    //    //        TextBox txtAdjustedIndentQTY = (TextBox)grdCustAdjustment.Rows[iLoop].FindControl("txtAdjustedIndentQTY");
    //    //        double ADJUST_QTY = double.Parse(txtAdjustedIndentQTY.Text.Trim());

    //    //        string ItemCode = lblAdjustArticleCode .Text.Trim();

    //    //        Label lblBranch = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblBranch");
    //    //        string BRANCH_NAME = lblBranch.Text.Trim();
    //    //        string BRANCH_CODE = lblBranch.ToolTip.Trim();

    //    //        Label lblAPPR_QTY = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
    //    //        double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());

    //    //        string Indent_Type = lblAPPR_QTY.ToolTip.Trim();

    //    //        DataView dv = new DataView(dtSewingCustAdj);
    //    //        dv.RowFilter = " YARN_CODE='" + ItemCode + "' and SHADE_CODE='" + SHADE_CODE + "' and IND_NUMB=" + IndentNumber;
    //    //        if (dv.Count == 0)
    //    //        {
    //    //            DataRow dr = dtSewingCustAdj.NewRow();
    //    //            dr["IND_NUMB"] = IndentNumber;
    //    //            dr["IND_BRANCH_NAME"] = BRANCH_NAME;
    //    //            dr["IND_BRANCH_CODE"] = BRANCH_CODE;
    //    //            dr["YARN_CODE"] = ItemCode;
    //    //            dr["SHADE_CODE"] = SHADE_CODE;
    //    //            dr["ADJUST_QTY"] = ADJUST_QTY;
    //    //            dr["APPR_QTY"] = APPR_QTY;
    //    //            dr["IND_TYPE"] = Indent_Type;
    //    //            dtSewingCustAdj.Rows.Add(dr);
    //    //        }
    //    //        else
    //    //        {
    //    //            dv[0]["ADJUST_QTY"] = ADJUST_QTY;
    //    //            dv[0]["APPR_QTY"] = APPR_QTY;
    //    //        }
    //    //        dtSewingCustAdj.AcceptChanges();
    //    //        TotalQTY = TotalQTY + ADJUST_QTY;
    //    //    }
    //          return dtSewingCustAdj;
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //    throw ex;
    //    //    }
    //}

    private DataTable createdatatableforadjustment(out double TotalQTY)
    {

        try
        {
            TotalQTY = 0;
            DataTable dtSewingCustAdj = new DataTable();
            if (Session["dtSewingCustAdj"] == null)
                dtSewingCustAdj = createAdjTable();
            else
            {
                dtSewingCustAdj.Rows.Clear();
                dtSewingCustAdj = (DataTable)Session["dtSewingCustAdj"];
                //if (!dtSewingCustAdj.Columns.Contains("ADJUST_QTY"))
                //{
                //    dtSewingCustAdj.Columns.Add("ADJUST_QTY", typeof(int));
                ////}
            }
            for (int iLoop = 0; iLoop < grdCustAdjustment.Rows.Count; iLoop++)
            {
                Label lblAdjustOrderNuber = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblAdjustOrderNuber");
                string ORDERNo = lblAdjustOrderNuber.Text.Trim();

                TextBox txtAdjustedIndentQTY = (TextBox)grdCustAdjustment.Rows[iLoop].FindControl("txtAdjustedIndentQTY");
                double ADJUST_QTY = double.Parse(txtAdjustedIndentQTY.Text.Trim());

                string ArticleCode = lblAdjustArticleCode.Text.Trim();
                string Shade = lblShadeCode.Text;
 
                Label lblCR_BRANCH_CODE = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblCR_BRANCH_CODE");
                 // string BRANCH_NAME = lblCR_BRANCH_CODE.Text.Trim();
                string BRANCH_CODE = lblCR_BRANCH_CODE.Text.Trim();

                Label lblAPPR_QTY = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblAPPR_QTY");
                double APPR_QTY = double.Parse(lblAPPR_QTY.Text.Trim());
                
                Label lblCR_BUSINESS_TYPE = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblCR_BUSINESS_TYPE");
                string BUSINESS_TYPE = lblCR_BUSINESS_TYPE.Text.Trim();

                Label lblCustNo = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblCustNo");
                string CR_ST_ORDER_NO = lblCustNo.Text.Trim();

                 Label lblCR_ST_SUBSTRATE = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblCR_ST_SUBSTRATE");
                string CR_ST_SUBSTRATE = lblCR_ST_SUBSTRATE.Text.Trim();

                 Label lblCR_ST_COUNT = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblCR_ST_COUNT");
                string CR_ST_COUNT = lblCR_ST_COUNT.Text.Trim();

                 Label lblCR_ST_SHADE_FAMILY_CODE = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblCR_ST_SHADE_FAMILY_CODE");
                string CR_ST_SHADE_FAMILY_CODE = lblCR_ST_SHADE_FAMILY_CODE.Text.Trim();
                  
                Label lblCR_ST_SHADE_CODE = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblCR_ST_SHADE_CODE");
                string CR_ST_SHADE_CODE = lblCR_ST_SHADE_CODE.Text.Trim();


                Label lblCR_COMP_CODE = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblCR_COMP_CODE");
                string CR_COMP_CODE = lblCR_COMP_CODE.Text.Trim();


                Label lblORDER_TYPE = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblORDER_TYPE");
                string ORDER_TYPE = lblORDER_TYPE.Text.Trim();
                Label lblORDER_CAT = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblORDER_CAT");
                string ORDER_CAT = lblORDER_CAT.Text.Trim();

                Label lblPRODUCT_TYPE = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblPRODUCT_TYPE");
                string PRODUCT_TYPE = lblPRODUCT_TYPE.Text.Trim();
                Label lblYEAR = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblYEAR");
                string YEAR = lblYEAR.Text.Trim();
               
               
               
                
              

                DataView dv = new DataView(dtSewingCustAdj);
                string filter = "ARTICAL_CODE='" + ArticleCode + "' and SHADE_CODE='" + Shade + "'";
                dv.RowFilter = filter;
                if (dv != null)
                {

                    if (dv.Count == 0)
                    {
                        DataRow dr = dtSewingCustAdj.NewRow();
                        dr["ARTICAL_CODE"] = ArticleCode;
                        dr["SHADE_CODE"] = Shade;
                        dr["PI_TYPE"] = PI_TYPE;
                        dr["PRODUCT_TYPE"] = PRODUCT_TYPE;

                        dr["CR_ST_SUBSTRATE"] = CR_ST_SUBSTRATE;
                        dr["CR_COMP_CODE"] = CR_COMP_CODE;
                        dr["CR_BRANCH_CODE"] = BRANCH_CODE;
                        dr["CR_YEAR"] = YEAR;
                        dr["CR_ORDER_TYPE"] = ORDER_TYPE;
                        dr["CR_ORDER_CAT"] = ORDER_CAT;
                        dr["CR_PRODUCT_TYPE"] = PRODUCT_TYPE;
                        dr["CR_BUSINESS_TYPE"] = BUSINESS_TYPE;
                        dr["CR_ST_ORDER_NO"] = CR_ST_ORDER_NO;
                        dr["CR_ST_ARTICLE_NO"] = ArticleCode;
                        dr["CR_ST_COUNT"] = CR_ST_COUNT;
                        dr["CR_ST_SHADE_FAMILY_CODE"] = CR_ST_SHADE_FAMILY_CODE;
                        dr["CR_ST_SHADE_CODE"] = CR_ST_SHADE_CODE;
                        dr["ADJ_QTY"] = ADJUST_QTY;
                        dr["CR_YRN_COUNT"] = 0;
                        dr["CR_YRN_PLY"] ="NA";
                       
                        dtSewingCustAdj.Rows.Add(dr);
                    }
                    else
                    {
                        dv[0]["ADJ_QTY"] = ADJUST_QTY;
                       // dv[0]["APPR_QTY"] = APPR_QTY;
                    }
                    dtSewingCustAdj.AcceptChanges();
                    TotalQTY = TotalQTY + ADJUST_QTY;
                }
            }
            return dtSewingCustAdj;


        }

        catch
        {
            throw;
        }
    }
    private double GetFinalTotalOfCustomerAdjustment()
    {
        try
        {
            double FinalTotal = 0;
            for (int iLoop = 0; iLoop < grdCustAdjustment.Rows.Count; iLoop++)
            {
                TextBox txtAdjustedIndentQTY = (TextBox)grdCustAdjustment.Rows[iLoop].FindControl("txtAdjustedIndentQTY");
                Label lblAdjustRemQty = (Label)grdCustAdjustment.Rows[iLoop].FindControl("lblAdjustRemQty");
                double Total = double.Parse(txtAdjustedIndentQTY.Text);
                if (Total <= double.Parse(lblAdjustRemQty.Text))
                {
                    FinalTotal = FinalTotal + Total;
                }
                else
                {
                    lblIndentAdjustmentError.Text = "Entered quantity is larger then Request quantity";
                    txtAdjustedIndentQTY.Text = 0.ToString();
                    break;
                }
            }
            return FinalTotal;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void grdCustAdjustment_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblAdjustRemQty = (Label)row.FindControl("lblAdjustRemQty");
            TextBox txtAdjustedIndentQTY = (TextBox)row.FindControl("txtAdjustedIndentQTY");
            txtAdjustedIndentQTY.Text = lblAdjustRemQty.Text;
            Label txtFinalQTY = (Label)grdCustAdjustment.FooterRow.FindControl("txtTotalAdjustedIndentQTY");
            txtFinalQTY.Text = GetFinalTotalOfCustomerAdjustment().ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

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
}
