using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class Module_Fiber_Controls_FiberStockLotWise : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            printGrid.Attributes.Add("onclick", "javascript:CallPrint('divPrint');");
            if (!IsPostBack)
            {              
                bindCustomerRequestApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }



    public void bindCustomerRequestApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetFiberStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year,  "","", "", "",  "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (dt != null && dt.Rows.Count > 0)
            {                
                gvStock.DataSource = dt;
                gvStock.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {               
                gvStock.DataSource = null;
                gvStock.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No record found.");
            }
        }
        catch
        {
            throw;
        }
    }
    

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/FIBER_LOTWISE_STOCK_REPORT.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void FilterGrid_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }



    public void SearchbyKeywords()
    {

        try
        {

            string BranchName = ((TextBox)gvStock.HeaderRow.FindControl("txtBranchName")).Text;
            string tdate = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDate")).Text;
            string trndesc = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).Text;
            string trntype = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).ToolTip;
            string trnno = ((TextBox)gvStock.HeaderRow.FindControl("txtTrnNo")).Text;
            string fibercode = ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCode")).Text;
            string fiberdesc = ((TextBox)gvStock.HeaderRow.FindControl("txtFiberDesc")).Text;
            string fibercat = ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCat")).Text;
            string finalrate = ((TextBox)gvStock.HeaderRow.FindControl("txtFinalRate")).Text;
            string lotno = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text;
            string totalbale = ((TextBox)gvStock.HeaderRow.FindControl("txtTotalBale")).Text;
            string issubale = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueBale")).Text;
            string balbale = ((TextBox)gvStock.HeaderRow.FindControl("txtBalBale")).Text;
            string weightofunit = ((TextBox)gvStock.HeaderRow.FindControl("txtWeightofUnit")).Text;
            string trnqty = ((TextBox)gvStock.HeaderRow.FindControl("txtQuantity")).Text;
            string totalvalue = ((TextBox)gvStock.HeaderRow.FindControl("txtTotalValue")).Text;
            string issueqty = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueQty")).Text;
            string issuevalue = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueValue")).Text;
            string balqty = ((TextBox)gvStock.HeaderRow.FindControl("txtBalQty")).Text;
            string balvalue = ((TextBox)gvStock.HeaderRow.FindControl("txtBalValue")).Text;




            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetFiberStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "", BranchName, tdate, trndesc, trntype, trnno, fibercode, fiberdesc, fibercat, finalrate, lotno, totalbale, issubale, balbale, weightofunit, trnqty, totalvalue, issueqty, issuevalue, balqty, balvalue);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvStock.DataSource = dt;
                gvStock.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }

            AutofillSearchContent(BranchName, tdate, trndesc, trntype, trnno, fibercode, fiberdesc, fibercat, finalrate, lotno, totalbale, issubale, balbale, weightofunit, trnqty, totalvalue, issueqty, issuevalue, balqty, balvalue);


        }
        catch
        {
            throw;
        }
    }

    private void AutofillSearchContent(string BranchName, string tdate, string trndesc, string trntype, string trnno, string fibercode, string fiberdesc, string fibercat, string finalrate, string lotno, string totalbale, string issubale, string balbale, string weightofunit, string trnqty, string totalvalue, string issueqty, string issuevalue, string balqty, string balvalue)
    {
        try
        {
             ((TextBox)gvStock.HeaderRow.FindControl("txtBranchName")).Text = BranchName;
             ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDate")).Text=tdate ;
             ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).Text=trndesc;
             //((TextBox)gvStock.HeaderRow.FindControl("txtTrnType")).ToolTip=trntype;
             ((TextBox)gvStock.HeaderRow.FindControl("txtTrnNo")).Text=trnno;
             ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCode")).Text=fibercode;
             ((TextBox)gvStock.HeaderRow.FindControl("txtFiberDesc")).Text=fiberdesc;
             ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCat")).Text=fibercat;
             ((TextBox)gvStock.HeaderRow.FindControl("txtFinalRate")).Text=finalrate;
             ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text=lotno;
             ((TextBox)gvStock.HeaderRow.FindControl("txtTotalBale")).Text=totalbale;
             ((TextBox)gvStock.HeaderRow.FindControl("txtIssueBale")).Text=issubale;
             ((TextBox)gvStock.HeaderRow.FindControl("txtBalBale")).Text=balbale;
             ((TextBox)gvStock.HeaderRow.FindControl("txtWeightofUnit")).Text=weightofunit;
             ((TextBox)gvStock.HeaderRow.FindControl("txtQuantity")).Text=trnqty;
             ((TextBox)gvStock.HeaderRow.FindControl("txtTotalValue")).Text=totalvalue;
             ((TextBox)gvStock.HeaderRow.FindControl("txtIssueQty")).Text=issueqty;
             ((TextBox)gvStock.HeaderRow.FindControl("txtIssueValue")).Text=issuevalue;
             ((TextBox)gvStock.HeaderRow.FindControl("txtBalQty")).Text=balqty;
            ((TextBox)gvStock.HeaderRow.FindControl("txtBalValue")).Text=balvalue;            

        }
        catch
        {
            throw;
        }

    }

    protected void gvStock_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvStock.PageIndex = e.NewPageIndex;
        bindCustomerRequestApproval();
    }

    protected void gvStock_PreRender1(object sender, EventArgs e)
    {
        gvStock.UseAccessibleHeader = true;
        //gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}
