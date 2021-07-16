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
using Obout.Interface;
using System.Collections.Generic;

public partial class Module_Production_Pages_Slips_Report : System.Web.UI.Page
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
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
            cmbJobCard.SelectedIndex = -1;
            txtParty.Text = "";
            txtDisQuality.Text = "";
            txtRefNo.Text = "";
            txtShade.Text = "";
            txtNoOfSlip.Text = "";

        }
        catch
        {
            throw;
        }
    }


    protected void cmbJobCard_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetJobCardNoData(e.Text.ToUpper(), e.ItemsOffset);

            cmbJobCard.Items.Clear();

            cmbJobCard.DataSource = data;
            cmbJobCard.DataTextField = "BATCH_CODE";
            cmbJobCard.DataValueField = "BATCH_CODE";
            cmbJobCard.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPaNoCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            
        }
    }



    private DataTable GetJobCardNoData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (SELECT   * FROM   (SELECT   M.BATCH_CODE, T.BATCH_CODE AS TBATCH_CODE, T.BATCH_ISSUE_NO, M.TRN_DATE FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T WHERE       M.TRN_NUMB = T.TRN_NUMB AND M.COMP_CODE = T.COMP_CODE  AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.BATCH_CODE = T.BATCH_CODE AND M.YEAR=T.YEAR) WHERE      BATCH_CODE = TBATCH_CODE  OR BATCH_CODE LIKE :SearchQuery  OR BATCH_ISSUE_NO LIKE :SearchQuery  OR TRN_DATE LIKE :SearchQuery) WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "AND BATCH_CODE NOT IN(SELECT   BATCH_CODE FROM   (SELECT   *  FROM   (SELECT   M.BATCH_CODE,  T.BATCH_CODE AS TBATCH_CODE, T.BATCH_ISSUE_NO,  M.TRN_DATE FROM   YARN_DYEING_PROD_MST M,  YARN_DYEING_PROD_TRN T WHERE   M.TRN_NUMB = T.TRN_NUMB  AND M.COMP_CODE = T.COMP_CODE AND M.BRANCH_CODE =  T.BRANCH_CODE  AND M.BATCH_CODE = T.BATCH_CODE AND M.YEAR=T.YEAR) WHERE      BATCH_CODE = TBATCH_CODE   OR BATCH_CODE LIKE :SearchQuery  OR BATCH_ISSUE_NO LIKE :SearchQuery OR TRN_DATE LIKE :SearchQuery) WHERE   ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by BATCH_CODE";
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
            string CommandText = "SELECT   *  FROM   (SELECT   * FROM   (SELECT   M.BATCH_CODE, T.BATCH_CODE AS TBATCH_CODE,   T.BATCH_ISSUE_NO,  M.TRN_DATE FROM   YARN_DYEING_PROD_MST M, YARN_DYEING_PROD_TRN T WHERE       M.TRN_NUMB = T.TRN_NUMB AND M.COMP_CODE = T.COMP_CODE   AND M.BRANCH_CODE = T.BRANCH_CODE  AND M.BATCH_CODE = T.BATCH_CODE AND M.YEAR=T.YEAR)  WHERE BATCH_CODE = TBATCH_CODE  OR BATCH_CODE LIKE :SearchQuery  OR BATCH_ISSUE_NO LIKE :SearchQuery OR TRN_DATE LIKE :SearchQuery) ";
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




    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void cmbJobCard_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        DataTable bResult = SaitexBL.Interface.Method.YRN_PROD_MST.getSlipDetails(cmbJobCard.SelectedValue.Trim(), oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString());

        if (bResult.Rows.Count > 0)
        {
            txtParty.Text = bResult.Rows[0]["PRTY_NAME"].ToString();
            txtDisQuality.Text = bResult.Rows[0]["ASS_YARN_DESC"].ToString();
            txtShade.Text = bResult.Rows[0]["SHADE_CODE"].ToString();
            txtRefNo.Text = bResult.Rows[0]["PRTY_NAME"].ToString();
        }
        else 
        {
            Common.CommonFuction.ShowMessage("Data not found!");
            
        }
        

    }
    protected void btnGetReport_Click(object sender, EventArgs e)
    {     string URL="~/Module/Production/Report/SLIP_REPORT.aspx?";
        URL+="JOB_CARD="+cmbJobCard.SelectedValue.Trim();
        URL+="&PRTY_NAME="+txtParty.Text;
        URL+="&QUALITY="+txtDisQuality.Text;
        URL += "&SHADE=" + txtShade.Text;
        URL += "&REF_NO=" + txtRefNo.Text;
        URL += "&NO_OF_SLIP=" + txtNoOfSlip.Text;

        Response.Redirect(URL);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        InitialisePage();
    }
}
