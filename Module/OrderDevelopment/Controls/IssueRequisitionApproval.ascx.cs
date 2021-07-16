using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using errorLog;
using Common;
public partial class Module_OrderDevelopment_Controls_IssueRequisitionApproval : System.Web.UI.UserControl
{
    string ISSUE = string.Empty;
    string branch = string.Empty ;
    string ShadCode = string.Empty;
    string ArticleCode = string.Empty;
    string itemtype = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    string pino = string.Empty;
    string producttype = string.Empty;
    string str = string.Empty;
    string Qty = string.Empty;
    DataTable dt1 = new DataTable();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;  
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                grdIsuueReqApp();
            }
        }
        catch
        {
            throw;
        }
    }
    private void grdIsuueReqApp()
    {
        DataTable dt = new DataTable();
        dt = SaitexBL.Interface.Method.OD_ISS_REQ_MST.GetIssueReqApp();
       
        if (dt != null && dt.Rows.Count > 0)
        {
            grdIssueReqApproval.DataSource = dt;
            grdIssueReqApproval.DataBind();
        }
        else
        {
            Common.CommonFuction.ShowMessage("Data Not Found .");
        }
    }  
    public  void btnView_Click(object sender, EventArgs e)
    {
           GridViewRow selectedRow = ((Control)sender).Parent.NamingContainer as GridViewRow;
           grdIssueReqApproval.SelectedIndex = selectedRow.RowIndex;
            Label issueReqNo = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblIssueReqNo");
            Label brc = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblBranch");
            Label prd = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblProductType");
            Label pno = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblPaNo");
            Label item = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblITEMTYPE");
            Label Shad = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblSHADECODE");
            Label Article = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblArticle");
            Label qty = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblQTY");

            ISSUE = issueReqNo.Text.ToString();
            branch = brc.Text.ToString();
            string producttype = prd.Text.ToString();
            pino = pno.Text.ToString();
            itemtype = item.Text.ToString();
            ArticleCode = Article.Text.ToString();
            ShadCode = Shad.Text.ToString();
             Qty = qty.Text.ToString();
             Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            bindGridStockDetail(branch, ArticleCode, ShadCode ,Sdate, Edate);   
    
    
    
    }
    private void bindGridStockDetail(string branchs, string ArticleCodes, string ShadCode, DateTime Sdate, DateTime Edate)
    {
        
        DataTable dt = new DataTable();
        dt = SaitexBL.Interface.Method.OD_ISS_REQ_MST.GetStockDetail(branchs,ArticleCodes,ShadCode, Sdate, Edate);
       
        ViewState["Data"] = dt;
        if (dt != null && dt.Rows.Count > 0)
        {
            GridStockDetail.DataSource = dt;
            GridStockDetail.DataBind();
        }
        else
        {
            Common.CommonFuction.ShowMessage("Data Not Found.");
        }
    }   
    protected void ChkUnApproved_CheckedChanged(object sender, EventArgs e)
    { 
        GridViewRow selectedRow = ((Control)sender).Parent.NamingContainer as GridViewRow;
        grdIssueReqApproval.SelectedIndex = selectedRow.RowIndex;
        Label issueReqNo = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblIssueReqNo");
        Label brc = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblBranch");
        Label prd = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblProductType");
        Label pno = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblPaNo");
        Label item = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblITEMTYPE");
        Label Shad = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblSHADECODE");
        Label Article = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblArticle");
        Label qty = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblQTY");

        ISSUE = issueReqNo.Text.ToString();
        branch = brc.Text.ToString();
        string producttype = prd.Text.ToString();
        pino = pno.Text.ToString();
        itemtype = item.Text.ToString();
        ArticleCode = Article.Text.ToString();
        ShadCode = Shad.Text.ToString();
         Qty = qty.Text.ToString();
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        
        CheckBox ChkApproved = (CheckBox)grdIssueReqApproval.SelectedRow.FindControl("chkApproved");
        CheckBox ChkUnApproved = (CheckBox)grdIssueReqApproval.SelectedRow.FindControl("ChkUnApproved");
        if (ChkUnApproved.Checked == true)
        {
            ChkApproved.Checked = false;
            str = "UnApproved";
            update();
            Common.CommonFuction.ShowMessage("Record Unapproved .");
        }
        else
        {
            ChkApproved.Checked = true;
            str = "Approved";          
            update();
            Common.CommonFuction.ShowMessage("Record Approved .");
        }
        
       
    
    }
    protected void chkApproved_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow selectedRow = ((Control)sender).Parent.NamingContainer as GridViewRow;
        grdIssueReqApproval.SelectedIndex = selectedRow.RowIndex;
       
        Label issueReqNo = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblIssueReqNo");
        Label brc = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblBranch");
        Label prd = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblProductType");
        Label pno = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblPaNo");
        Label item = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblITEMTYPE");
        Label Shad = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblSHADECODE");
        Label Article = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblArticle");
        Label qty = (Label)grdIssueReqApproval.SelectedRow.FindControl("lblQTY");

        ISSUE = issueReqNo.Text.ToString();
        branch = brc.Text.ToString();
        producttype = prd.Text.ToString();
        pino = pno.Text.ToString();
        itemtype = item.Text.ToString();
        ArticleCode = Article.Text.ToString();
        ShadCode = Shad.Text.ToString();
         Qty = qty.Text.ToString();
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        
        CheckBox ChkApproved = (CheckBox)grdIssueReqApproval.SelectedRow.FindControl("chkApproved");
        CheckBox ChkUnApproved = (CheckBox)grdIssueReqApproval.SelectedRow.FindControl("ChkUnApproved");
        if (ChkApproved.Checked == true)
        {
            str = "Approved";
            ChkUnApproved.Checked = false;
            update();
            Common.CommonFuction.ShowMessage("Record Approved ."); 
        }
        else
        {
            ChkUnApproved.Checked = true ;
            str = "UnApproved";
            update();
            Common.CommonFuction.ShowMessage("Record Unapproved .");
        
        }
    }
    private void update()
    {
        SaitexDM.Common.DataModel.OD_ISS_REQ_TRN oOD_ISS_REQ_TRN = new SaitexDM.Common.DataModel.OD_ISS_REQ_TRN();
        try
        {
          
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            oOD_ISS_REQ_TRN.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_ISS_REQ_TRN.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_ISS_REQ_TRN.ISSUE_REQ_NO = CommonFuction.funFixQuotes(ISSUE.ToString());
            oOD_ISS_REQ_TRN.ITEM_TYPE =  CommonFuction.funFixQuotes(itemtype.ToString());
            oOD_ISS_REQ_TRN.ARTICLE_CODE = CommonFuction.funFixQuotes(ArticleCode.ToString());
            oOD_ISS_REQ_TRN.SHADE_CODE = CommonFuction.funFixQuotes(ShadCode.ToString());
            oOD_ISS_REQ_TRN.APPR_BY = oUserLoginDetail.Username;
            oOD_ISS_REQ_TRN.APPR_DATE = DateTime.Parse(DateTime .Now .ToShortDateString());
            if (str == "Approved")
            {
                oOD_ISS_REQ_TRN.APPR_QTY =  Qty.ToString();
            }
            else
            {
                oOD_ISS_REQ_TRN.APPR_QTY = "0";
            }


            bool bResult = SaitexBL.Interface.Method.OD_ISS_REQ_MST.GetIssueApproval(oOD_ISS_REQ_TRN, out iRecordFound);
            if (bResult)
            {          
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Here Are Problem .');", true);
            }     
        }
        catch
        {
            throw;
        }
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void GridStockDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridStockDetail.PageIndex = e.NewPageIndex;
        bindGridStockDetail(branch, ArticleCode, ShadCode ,Sdate, Edate);   
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }
    protected void GridStockDetail_Sorting(object sender, GridViewSortEventArgs e)
    {
        
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            SortGridView(sortExpression, DESCENDING);
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            SortGridView(sortExpression, ASCENDING);
        }

    
    
    }
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }

  

    private void SortGridView(string sortExpression, string direction)
    {
        //  You can cache the DataTable for improving performance
      
        DataTable dt =(DataTable)ViewState["Data"];

        DataView dv = new DataView(dt);
        dv.Sort = sortExpression + direction;

        GridStockDetail.DataSource = dv;
        GridStockDetail.DataBind();
    }


}
