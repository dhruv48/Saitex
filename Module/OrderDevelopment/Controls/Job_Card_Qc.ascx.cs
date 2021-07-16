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
using Common;
using errorLog;
using Obout.Interface;
using System.Collections.Generic;

public partial class Module_OrderDevelopment_Controls_Job_Card_Qc : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.Job_Card_Qc oJob_Card_Qc;
    string PRODUCT_TYPE = "YARN DYEING";
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in page loading.\r\nSee error log for detail."));
        }
    }



    private void InitialisePage()
    {
        try
        {
            ddlPaNo.Visible = true;
            ddlPaNo.SelectedIndex = -1;
            ddlJobCard.Visible = false;
            ddlJobCard.SelectedIndex= -1;
            txtDE.Text = "";
            txtDMF.Text = "";
            txtOILFR.Text = "";
            txtRC.Text = "";
            txtRemark.Text = "";
            dllGrade.SelectedIndex = -1;
            ddlRelease.SelectedIndex = -1;
            //BindGRADE();
           

            gvJobCardApproval.DataSource = null;
            gvJobCardApproval.DataBind();
            
            
        }
        catch
        {
            throw;
        }
    }




    protected void ddlPaNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPaNoData(e.Text.ToUpper(), e.ItemsOffset);

            ddlPaNo.Items.Clear();

            ddlPaNo.DataSource = data;
            ddlPaNo.DataTextField = "BATCH_CODE";
            ddlPaNo.DataValueField = "TRN_DATA";
            ddlPaNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPaNoCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetPaNoData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   *  FROM   (  SELECT   V.CUST_REQ_NO, V.PA_NO, V.LOT_SIZE AS TRN_QTY,  V.ORDER_NO, V.BATCH_CODE, V.ARTICLE_CODE, V.ARTICLE_DESC, V.SHADE_CODE, V.PRTY_CODE, V.PRTY_NAME,  V.TRN_NUMB, (   V.CUST_REQ_NO || '@'|| V.PA_NO  || '@'  || V.LOT_SIZE  || '@'  || V.BATCH_CODE   || '@'  || V.ARTICLE_CODE   || '@'  || V.ARTICLE_DESC || '@'   || V.SHADE_CODE || '@' || V.PRTY_CODE  || '@' || V.PRTY_NAME   || '@' || V.TRN_NUMB) TRN_DATA , M.TRN_DATE    FROM   V_BATCH_CARD_MST V,YARN_DYEING_PROD_MST M WHERE  V.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and V.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and V.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND V.CONF_FLAG = '1' AND QC_COMP_FLAG = '0' and PRODUCT_TYPE= '" + PRODUCT_TYPE + "' AND V.BATCH_CODE = M.BATCH_CODE  AND V.PA_NO = M.PA_NO  AND V.YEAR = M.YEAR    AND V.COMP_CODE = M.COMP_CODE   AND V.BRANCH_CODE = M.BRANCH_CODE   AND NVL (M.CONF_FLAG, 0) = 1 GROUP BY   V.CUST_REQ_NO, V.PA_NO,  V.LOT_SIZE,  V.ORDER_NO, V.BATCH_CODE,   V.ARTICLE_CODE,   V.ARTICLE_DESC, V.SHADE_CODE,  V.PRTY_CODE,  V.PRTY_NAME,   V.TRN_NUMB, M.TRN_DATE ORDER BY   V.CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery) WHERE 1=1   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND trn_data NOT IN(SELECT trn_data FROM (SELECT   *  FROM   (  SELECT   V.CUST_REQ_NO, V.PA_NO, V.LOT_SIZE AS TRN_QTY,  V.ORDER_NO, V.BATCH_CODE, V.ARTICLE_CODE, V.ARTICLE_DESC, V.SHADE_CODE, V.PRTY_CODE, V.PRTY_NAME,  V.TRN_NUMB, (   V.CUST_REQ_NO || '@'|| V.PA_NO  || '@'  || V.LOT_SIZE  || '@'  || V.BATCH_CODE   || '@'  || V.ARTICLE_CODE   || '@'  || V.ARTICLE_DESC || '@'   || V.SHADE_CODE || '@' || V.PRTY_CODE  || '@' || V.PRTY_NAME   || '@' || V.TRN_NUMB) TRN_DATA   , M.TRN_DATE  FROM   V_BATCH_CARD_MST V,YARN_DYEING_PROD_MST M WHERE  V.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and V.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and V.YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND V.CONF_FLAG = '1' AND QC_COMP_FLAG = '0' and PRODUCT_TYPE= '" + PRODUCT_TYPE + "' AND V.BATCH_CODE = M.BATCH_CODE  AND V.PA_NO = M.PA_NO  AND V.YEAR = M.YEAR    AND V.COMP_CODE = M.COMP_CODE   AND V.BRANCH_CODE = M.BRANCH_CODE   AND NVL (M.CONF_FLAG, 0) = 1 GROUP BY   V.CUST_REQ_NO, V.PA_NO,  V.LOT_SIZE,  V.ORDER_NO, V.BATCH_CODE,   V.ARTICLE_CODE,   V.ARTICLE_DESC, V.SHADE_CODE,  V.PRTY_CODE,  V.PRTY_NAME,   V.TRN_NUMB , M.TRN_DATE ORDER BY   V.CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by CUST_REQ_NO";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetPaNoCount(string text)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   *  FROM   (  SELECT   CUST_REQ_NO, PA_NO, LOT_SIZE AS TRN_QTY,  ORDER_NO, BATCH_CODE, ARTICLE_CODE, ARTICLE_DESC, SHADE_CODE, PRTY_CODE, PRTY_NAME,  TRN_NUMB, (   CUST_REQ_NO || '@'|| PA_NO  || '@'  || LOT_SIZE  || '@'  || BATCH_CODE   || '@'  || ARTICLE_CODE   || '@'  || ARTICLE_DESC || '@'   || SHADE_CODE || '@' || PRTY_CODE  || '@' || PRTY_NAME   || '@' || TRN_NUMB) TRN_DATA  , M.TRN_DATE   FROM   V_BATCH_CARD_MST WHERE  COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'   AND CONF_FLAG = '1' AND QC_COMP_FLAG = '0'   and PRODUCT_TYPE= '" + PRODUCT_TYPE + "' GROUP BY   CUST_REQ_NO, PA_NO,  LOT_SIZE,  ORDER_NO, BATCH_CODE,   ARTICLE_CODE,   ARTICLE_DESC, SHADE_CODE,  PRTY_CODE,  PRTY_NAME,   TRN_NUMB  ORDER BY   CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }


    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            if (ddlPaNo.SelectedValue != "Select Job Card No" || ddlJobCard.SelectedText.ToString() != "Select Job Card No")
            {
                if ( dllGrade.SelectedValue.ToString() != string.Empty && txtDMF.Text.ToString() != string.Empty && txtOILFR.Text.ToString() != string.Empty && txtRemark.Text.ToString()!=string.Empty)
                { 
                    oJob_Card_Qc = new SaitexDM.Common.DataModel.Job_Card_Qc();

                    if (ddlPaNo.SelectedText.ToString() != "Select Job Card No")
                        oJob_Card_Qc.BATCH_CODE = ddlPaNo.SelectedText.ToString();
                    else
                        oJob_Card_Qc.BATCH_CODE = ddlJobCard.SelectedText.ToString();
                    oJob_Card_Qc.COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
                    oJob_Card_Qc.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.ToString();

                    oJob_Card_Qc.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oJob_Card_Qc.QC_DE = txtDE.Text.ToString();
                    oJob_Card_Qc.QC_DMF = txtDMF.Text.ToString();
                    oJob_Card_Qc.QC_GRADE = dllGrade.SelectedValue.ToString();
                    oJob_Card_Qc.QC_OIL_FR = txtOILFR.Text.ToString();
                    oJob_Card_Qc.QC_RC = txtRC.Text.ToString();
                    oJob_Card_Qc.QC_REMARK = txtRemark.Text.ToString();
                    oJob_Card_Qc.QC_COMP_DATE = System.DateTime.Today.Date;
                    oJob_Card_Qc.QC_BY = oUserLoginDetail.Username.ToString();
                    oJob_Card_Qc.QC_RELEASE = ddlRelease.Text.ToString();
                    oJob_Card_Qc.QC_COMP_FLAG = "1";
                   


                    bool bResult = SaitexBL.Interface.Method.BATCH_CARD_MST.InsertJobCardQcEntry(oJob_Card_Qc);
                    if (bResult)
                    {
                        Common.CommonFuction.ShowMessage(@"Job Card QC Entry Saved Successfully.\r\nYour Batch Card No is " + oJob_Card_Qc.BATCH_CODE);
                        InitialisePage();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("JOB Card QC Entry saving failed.");
                    }

                }
                else 
                {

                    Common.CommonFuction.ShowMessage("All fields are mandatory to fill except DE");
                 
                }




            }
            else 
            {

                Common.CommonFuction.ShowMessage("Please select the Job Cord No.");
            
            }
        }
        catch
        {
            throw;
        }

    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        ddlPaNo.Visible = false;
        ddlJobCard.Visible = true;
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../../Reports/PrintJobSheet.aspx?QC=" + "QC");
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
   




    private void BindJobCardApproval(string JobCard)
    {
        try
        {
            SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardQCApproval(oBATCH_CARD_MST, JobCard);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;
                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvJobCardApproval.DataSource = dt;
                gvJobCardApproval.DataBind();
                //lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
               // lblTotalRecord.Text = "No JobSheet For Approval";
                gvJobCardApproval.DataSource = null;
                gvJobCardApproval.DataBind();
               // lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No JobSheet For Approval");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void grdJobCardTRN_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdJobCardDYES_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    protected void gvJobCardApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;
                Label lblBATCH_CODE = (Label)grdRow.FindControl("lblBATCH_CODE");

                SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
                oBATCH_CARD_MST.BATCH_CODE = int.Parse(lblBATCH_CODE.Text.Trim());
                oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                DataTable dtJobTRN = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardApprovalTRN(oBATCH_CARD_MST);
                if (dtJobTRN != null && dtJobTRN.Rows.Count > 0)
                {
                    GridView gvJobCardTrn = (GridView)grdRow.FindControl("grdJobCardTRN");
                    gvJobCardTrn.DataSource = dtJobTRN;
                    gvJobCardTrn.DataBind();
                }

                DataTable dtJobTRNDYES = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardApprovalTRNDYES(oBATCH_CARD_MST);
                if (dtJobTRNDYES != null && dtJobTRNDYES.Rows.Count > 0)
                {
                    GridView gvJobCardDYES = (GridView)grdRow.FindControl("grdJobCardDYES");
                    gvJobCardDYES.DataSource = dtJobTRNDYES;
                    gvJobCardDYES.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
           // lblErrorMessage.Text = ex.ToString();
        }
    }
    protected void ddlPaNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        BindJobCardApproval(ddlPaNo.SelectedText.ToString());
    }




    //private void BindGRADE()
    //{
    //    string Text = string.Empty;
    //    try
    //    {
    //        string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GRADE'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
    //        string whereClause = string.Empty;
            
    //        string SortExpression = " order by MST_CODE";
    //        string SearchQuery = Text + "%";
    //        DataTable dt= SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");

    //        dllGrade.DataSource = dt;
    //        dllGrade.DataTextField = "MST_CODE";
    //        dllGrade.DataValueField = "MST_CODE";
    //        dllGrade.DataBind();
        
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    
    
    //}
    protected void ddlJobCard_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetJobCardData(e.Text.ToUpper(), e.ItemsOffset);

            ddlJobCard.Items.Clear();

            ddlJobCard.DataSource = data;
            ddlJobCard.DataTextField = "BATCH_CODE";
            ddlJobCard.DataValueField = "TRN_DATA";
            ddlJobCard.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetJobCardCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }




    private DataTable GetJobCardData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   *  FROM   (  SELECT   CUST_REQ_NO, PA_NO, LOT_SIZE AS TRN_QTY,  ORDER_NO, BATCH_CODE, ARTICLE_CODE, ARTICLE_DESC, SHADE_CODE, PRTY_CODE, PRTY_NAME,  TRN_NUMB, (   CUST_REQ_NO || '@'|| PA_NO  || '@'  || LOT_SIZE  || '@'  || BATCH_CODE   || '@'  || ARTICLE_CODE   || '@'  || ARTICLE_DESC || '@'   || SHADE_CODE || '@' || PRTY_CODE  || '@' || PRTY_NAME   || '@' || TRN_NUMB) TRN_DATA     FROM   V_BATCH_CARD_MST WHERE  COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "' AND CONF_FLAG = '1' AND QC_COMP_FLAG = '1' and PRODUCT_TYPE= '" + PRODUCT_TYPE + "' GROUP BY   CUST_REQ_NO, PA_NO,  LOT_SIZE,  ORDER_NO, BATCH_CODE,   ARTICLE_CODE,   ARTICLE_DESC, SHADE_CODE,  PRTY_CODE,  PRTY_NAME,   TRN_NUMB  ORDER BY   CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery OR BATCH_CODE LIKE :SearchQuery) WHERE 1=1 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND trn_data NOT IN(SELECT trn_data FROM (SELECT   *  FROM   (  SELECT   CUST_REQ_NO, PA_NO, LOT_SIZE AS TRN_QTY,  ORDER_NO, BATCH_CODE, ARTICLE_CODE, ARTICLE_DESC, SHADE_CODE, PRTY_CODE, PRTY_NAME,  TRN_NUMB, (   CUST_REQ_NO || '@'|| PA_NO  || '@'  || LOT_SIZE  || '@'  || BATCH_CODE   || '@'  || ARTICLE_CODE   || '@'  || ARTICLE_DESC || '@'   || SHADE_CODE || '@' || PRTY_CODE  || '@' || PRTY_NAME   || '@' || TRN_NUMB) TRN_DATA     FROM   V_BATCH_CARD_MST WHERE  COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'  AND CONF_FLAG = '1'  AND QC_COMP_FLAG = '1' and PRODUCT_TYPE= '" + PRODUCT_TYPE + "' GROUP BY   CUST_REQ_NO, PA_NO,  LOT_SIZE,  ORDER_NO, BATCH_CODE,   ARTICLE_CODE,   ARTICLE_DESC, SHADE_CODE,  PRTY_CODE,  PRTY_NAME,   TRN_NUMB  ORDER BY   CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery OR BATCH_CODE LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }
         //   AND (PA_NO,  LOT_SIZE,  BATCH_CODE, COMP_CODE, BRANCH_CODE,  YEAR) NOT IN   (SELECT   PA_NO, TRN_QTY AS LOT_SIZE,   BATCH_CODE,  COMP_CODE,  BRANCH_CODE,   YEAR   FROM   YARN_DYEING_PROD_MST WHERE COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "') 




            string SortExpression = " order by CUST_REQ_NO";
            string SearchQuery = Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetJobCardCount(string text)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (SELECT   *  FROM   (  SELECT   CUST_REQ_NO, PA_NO, LOT_SIZE AS TRN_QTY,  ORDER_NO, BATCH_CODE, ARTICLE_CODE, ARTICLE_DESC, SHADE_CODE, PRTY_CODE, PRTY_NAME,  TRN_NUMB, (   CUST_REQ_NO || '@'|| PA_NO  || '@'  || LOT_SIZE  || '@'  || BATCH_CODE   || '@'  || ARTICLE_CODE   || '@'  || ARTICLE_DESC || '@'   || SHADE_CODE || '@' || PRTY_CODE  || '@' || PRTY_NAME   || '@' || TRN_NUMB) TRN_DATA     FROM   V_BATCH_CARD_MST WHERE  COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' and BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'   AND CONF_FLAG = '1' AND QC_COMP_FLAG = '1'   and PRODUCT_TYPE= '" + PRODUCT_TYPE + "'AND (PA_NO,  LOT_SIZE,  BATCH_CODE, COMP_CODE, BRANCH_CODE,  YEAR) NOT IN   (SELECT   PA_NO, TRN_QTY AS LOT_SIZE,   BATCH_CODE,  COMP_CODE,  BRANCH_CODE,   YEAR   FROM   YARN_DYEING_PROD_MST WHERE COMP_CODE='C00001' AND BRANCH_CODE='SL0001') GROUP BY   CUST_REQ_NO, PA_NO,  LOT_SIZE,  ORDER_NO, BATCH_CODE,   ARTICLE_CODE,   ARTICLE_DESC, SHADE_CODE,  PRTY_CODE,  PRTY_NAME,   TRN_NUMB  ORDER BY   CUST_REQ_NO) WHERE      PRTY_NAME LIKE :SearchQuery    OR PRTY_CODE LIKE :SearchQuery  OR PA_NO LIKE :SearchQuery    OR CUST_REQ_NO LIKE :SearchQuery  OR ARTICLE_CODE LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }



    protected void ddlJobCard_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {

        BindMSTDATA(ddlJobCard.SelectedText.ToString());
        
        BindJobCardApproval(ddlJobCard.SelectedText.ToString());
    }


    

    private void BindMSTDATA(string JobCard)
    {
        try
        {
            SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            oBATCH_CARD_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oBATCH_CARD_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oBATCH_CARD_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            DataTable dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetJobCardMstData(oBATCH_CARD_MST, JobCard);
            if (dt != null && dt.Rows.Count > 0)
            {

                
             //  DataRow dr = new DataRow(dt.Rows[0]);
                txtDE.Text = dt.Rows[0]["QC_DE"].ToString();
                txtRC.Text = dt.Rows[0]["QC_RC"].ToString();
                txtDMF.Text = dt.Rows[0]["QC_DMF"].ToString();
                txtOILFR.Text = dt.Rows[0]["QC_OIL_FR"].ToString();
                dllGrade.SelectedIndex = dllGrade.Items.IndexOf(dllGrade.Items.FindByValue(dt.Rows[0]["QC_GRADE"].ToString()));

                if (dt.Rows[0]["QC_RELEASE"].ToString() == "1")
                { ddlRelease.SelectedIndex = 0; }
                else
                {
                    ddlRelease.SelectedIndex = 1;

                }
                txtRemark.Text = dt.Rows[0]["QC_REMARK"].ToString();


              
               
            }
            else
            {
               
                CommonFuction.ShowMessage("No Data Found");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



}
