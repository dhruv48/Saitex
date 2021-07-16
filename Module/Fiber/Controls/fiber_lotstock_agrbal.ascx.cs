using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class Module_Fiber_Controls_fiber_lotstock_agrbal : System.Web.UI.UserControl
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
                //ViewState["Filter"] = "ALL";
                //DropDownList ddlbranch = (DropDownList)gvStock.HeaderRow.FindControl("ddlBranchName");

                bindCustomerRequestApproval();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }







    private DataTable GET_MOM_DATA(string Text, string PRTY_GRP_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select DISTINCT BRANCH_NAME from CM_BRANCH_MST where Del_Status=0 and  comp_code='" + oUserLoginDetail.COMP_CODE + "'  ";
            string WhereClause = " and BRANCH_NAME like :SearchQuery";
            string SortExpression = " order by BRANCH_NAME asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, PRTY_GRP_CODE, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }

    protected void row_boundgrd(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList ddlbranch = (DropDownList)e.Row.FindControl("ddlBranchName");
            try
            {
                DataTable dt = null;
                dt = new DataTable();
                dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectbranchMst();
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = dt;
                ddlbranch.DataValueField = "BRANCH_NAME";
                ddlbranch.DataTextField = "BRANCH_NAME";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, new ListItem("--SELECT--", string.Empty));
                dt.Dispose();
                dt = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


   

    public void bindCustomerRequestApproval()
    {
        try
        {


            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetFiberStockaggreagte(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
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

        try
        {

            string BranchName = ((DropDownList)gvStock.HeaderRow.FindControl("ddlBranchName")).SelectedValue;
            string tdate = "";
            string trndesc = "";
            string trntype = "";
            string trnno = "";
            string fibercode = "";
            string fiberdesc = ((TextBox)gvStock.HeaderRow.FindControl("txtFiberDesc")).Text;
            string fibercat = ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCat")).Text;
            string finalrate = "";
            string lotno = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text;
            string totalbale = ((TextBox)gvStock.HeaderRow.FindControl("txtTotalBale")).Text;
            string issubale = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueBale")).Text;
            string balbale = "";
            string weightofunit = "";
            string trnqty = ((TextBox)gvStock.HeaderRow.FindControl("txtQuantity")).Text;
            string totalvalue = "";
            string issueqty = "";
            string issuevalue = "";
            string balqty = ((TextBox)gvStock.HeaderRow.FindControl("txtBalQty")).Text;
            string balvalue = ((TextBox)gvStock.HeaderRow.FindControl("txtBalValue")).Text;
            string palletcode = "";
            string grade = "";
            string palletno = "";
            string PartyName = "";






            string URL = "../Reports/fiber_lotstock_agrbalrpt.aspx?BRANCHNAME=" + BranchName + "&TDATE=" + tdate + "&TRNDESC=" + trndesc + "TRNDESC=" + trntype + "&TRNNO=" + trnno + "&FIBERCODE=" + fibercode + "&FIBERDESC=" + fiberdesc + "&FIBERCAT=" + fibercat + "&FINALRATE=" + finalrate + "&LOTNO=" + lotno + "&TOTALBALE=" + totalbale + "&ISSUBALE=" + issubale + "&BALBALE=" + balbale + "&WEIGHTOFUNIT=" + weightofunit + "&TRNQTY=" + trnqty + "&TOATALVALUE=" + totalvalue + "&ISSUEQTY=" + issueqty + "&ISSUEVALUE=" + issuevalue + "&BALQTY=" + balqty + "&BALVALUE=" + balvalue + "&PALLETCODE=" + palletcode + "&GRADE=" + grade + "&PALLETNO=" + palletno;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

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


            string BranchName = ((DropDownList)gvStock.HeaderRow.FindControl("ddlBranchName")).SelectedValue;
            string tdate = "";
            string trndesc = "";
            string trntype = "";
            string trnno ="";
            string fibercode = "";
            string fiberdesc = ((TextBox)gvStock.HeaderRow.FindControl("txtFiberDesc")).Text;
            string fibercat = ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCat")).Text;
            string finalrate = "";
            string lotno = ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text;
            string totalbale = ((TextBox)gvStock.HeaderRow.FindControl("txtTotalBale")).Text;
            string issubale = ((TextBox)gvStock.HeaderRow.FindControl("txtIssueBale")).Text;
            string balbale ="";
            string weightofunit = "";
            string trnqty = ((TextBox)gvStock.HeaderRow.FindControl("txtQuantity")).Text;
            string totalvalue = "";
            string issueqty = "";
            string issuevalue = "";
            string balqty = ((TextBox)gvStock.HeaderRow.FindControl("txtBalQty")).Text;
            string balvalue = ((TextBox)gvStock.HeaderRow.FindControl("txtBalValue")).Text;
            string palletcode = ((TextBox)gvStock.HeaderRow.FindControl("txtpalletcode")).Text;
            string grade = "";
            string palletno = "";
            string PartyName = "";
            






            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetFiberStockaggreagte(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "", BranchName, tdate, trndesc, trntype, trnno, fibercode, fiberdesc, fibercat, finalrate, lotno, totalbale, issubale, balbale, weightofunit, trnqty, totalvalue, issueqty, issuevalue, balqty, balvalue, palletcode, grade, palletno, PartyName);

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
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }

            AutofillSearchContent(BranchName, tdate, trndesc, trntype, trnno, fibercode, fiberdesc, fibercat, finalrate, lotno, totalbale, issubale, balbale, weightofunit, trnqty, totalvalue, issueqty, issuevalue, balqty, balvalue, palletcode, grade, palletno, PartyName);


        }
        catch
        {
            throw;
        }
    }

    private void AutofillSearchContent(string BranchName, string tdate, string trndesc, string trntype, string trnno, string fibercode, string fiberdesc, string fibercat, string finalrate, string lotno, string totalbale, string issubale, string balbale, string weightofunit, string trnqty, string totalvalue, string issueqty, string issuevalue, string balqty, string balvalue, string palletcode, string grade, string palletno, string PartyName)
    {
        try
        {
          ((DropDownList)gvStock.HeaderRow.FindControl("ddlBranchName")).SelectedValue = BranchName;
          //  ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDate")).Text = tdate;
          //  ((TextBox)gvStock.HeaderRow.FindControl("txtTrnDesc")).Text = trndesc;
            //((TextBox)gvStock.HeaderRow.FindControl("txtTrnType")).ToolTip=trntype;
          //  ((TextBox)gvStock.HeaderRow.FindControl("txtTrnNo")).Text = trnno;
            //((TextBox)gvStock.HeaderRow.FindControl("txtFiberCode")).Text = fibercode;
           ((TextBox)gvStock.HeaderRow.FindControl("txtFiberDesc")).Text = fiberdesc;
            ((TextBox)gvStock.HeaderRow.FindControl("txtFiberCat")).Text = fibercat;
          //  ((TextBox)gvStock.HeaderRow.FindControl("txtFinalRate")).Text = finalrate;
            ((TextBox)gvStock.HeaderRow.FindControl("txtLotNo")).Text = lotno;
            ((TextBox)gvStock.HeaderRow.FindControl("txtTotalBale")).Text = totalbale;
            ((TextBox)gvStock.HeaderRow.FindControl("txtIssueBale")).Text = issubale;
         //   ((TextBox)gvStock.HeaderRow.FindControl("txtBalBale")).Text = balbale;
         //   ((TextBox)gvStock.HeaderRow.FindControl("txtWeightofUnit")).Text = weightofunit;
            ((TextBox)gvStock.HeaderRow.FindControl("txtQuantity")).Text = trnqty;
        //    ((TextBox)gvStock.HeaderRow.FindControl("txtTotalValue")).Text = totalvalue;
          //  ((TextBox)gvStock.HeaderRow.FindControl("txtIssueQty")).Text = issueqty;
          //  ((TextBox)gvStock.HeaderRow.FindControl("txtIssueValue")).Text = issuevalue;
            ((TextBox)gvStock.HeaderRow.FindControl("txtBalQty")).Text = balqty;
            ((TextBox)gvStock.HeaderRow.FindControl("txtBalValue")).Text = balvalue;
            ((TextBox)gvStock.HeaderRow.FindControl("txtpalletcode")).Text = palletcode;
        //    ((TextBox)gvStock.HeaderRow.FindControl("txtgrade")).Text = grade;
       //     ((TextBox)gvStock.HeaderRow.FindControl("txtpalletno")).Text = palletno;
         //   ((TextBox)gvStock.HeaderRow.FindControl("txtPartyName")).Text = PartyName;

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
