using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class Module_Fiber_Reports_FiberItemStockAgeingQuery : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    string strBranch = string.Empty;
    string strItemType = string.Empty;
    string strCatCode = string.Empty;
    string strDay1 = string.Empty;
    string strDay2 = string.Empty;
    string strDay3 = string.Empty;
    int P_d1;
    int P_D2;
    int P_D3;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }
    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Print Mode";
            GetBranch();
            GetItemType();
            GetItemCategory();
        }
        catch
        {
            throw;
        }
    }
    private void GetBranch()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("---------------Select-------------------", ""));
            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void GetItemType()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiber();
            DataView Dv = new DataView(dt);
            ddlItemType.DataSource = Dv;
            ddlItemType.DataValueField = "FIBER_CODE";
            ddlItemType.DataTextField = "FIBER_DESC";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("------------------All----------------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void GetItemCategory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiberCat();
            ddlCatCode.DataSource = dt;
            ddlCatCode.DataValueField = "FIBER_CAT";
            ddlCatCode.DataTextField = "FIBER_CAT";
            ddlCatCode.DataBind();
            ddlCatCode.Items.Insert(0, new ListItem("------------------All----------------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string strBranch = string.Empty;
        string strItemType = string.Empty;
        string strCatCode = string.Empty;
        string strDay1 = string.Empty;
        string strDay2 = string.Empty;
        string strDay3 = string.Empty;
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                strBranch = ddlBranch.SelectedValue.ToString().Trim();

                if (ddlItemType.SelectedIndex > 0)
                {
                    strItemType = ddlItemType.SelectedValue.ToString().Trim();
                }


                if (ddlCatCode.SelectedIndex > 0)
                {
                    strCatCode = ddlCatCode.SelectedValue.ToString().Trim();
                }

                if (txtDay1.Text != string.Empty && txtDay2.Text != string.Empty && txtDay3.Text != string.Empty)
                {
                    strDay1 = txtDay1.Text.Trim();
                    strDay2 = txtDay2.Text.Trim();
                    strDay3 = txtDay3.Text.Trim();

                    string URL = "./FiberItemStockAge.aspx?strBranch=" + strBranch + "&strItemType=" + strItemType + "&strCatCode=" + strCatCode + "&strDay1=" + strDay1 + "&strDay2=" + strDay2 + "&strDay3=" + strDay3 + "";
                    //URL = URL.Replace("'", "$");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                {
                    CommonFuction.ShowMessage("Please enter Day1, Day2, and Day3 first..");
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please Select Branch first..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./MaterialStockAging_OPT.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtGetRecord_Click(object sender, EventArgs e)
    {
        try
        {
            BindStockAgeingGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdAgin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAgin.PageIndex = e.NewPageIndex;
        BindStockAgeingGrid();
    }
    private bool CheckValidation()
    {
        try
        {
            bool IsValidation = false;
            if (ddlBranch.SelectedIndex != 0)
            {
                if (txtDay1.Text != "")
                {
                    if (txtDay2.Text != "")
                    {
                        if (txtDay3.Text != "")
                        {
                            IsValidation = true;
                        }
                        else
                        {
                            IsValidation = false;
                            Common.CommonFuction.ShowMessage("Dear! Please enter Day 3..");
                        }
                    }
                    else
                    {
                        IsValidation = false;
                        Common.CommonFuction.ShowMessage("Dear! Please enter Day 2..");
                    }
                }
                else
                {
                    IsValidation = false;
                    Common.CommonFuction.ShowMessage("Dear! Please enter Day 1..");
                }
            }
            else
            {
                IsValidation = false;
                Common.CommonFuction.ShowMessage("Dear! Please select Branch first..");
            }
            return IsValidation;
        }
        catch
        {
            return false;
        }
    }
    private void BindStockAgeingGrid()
    {
        try
        {
            string strBranch = string.Empty;
            string strItemType = string.Empty;
            string strCatCode = string.Empty;
            int iDay1;
            int iDay2;
            int iDay3;

            if (CheckValidation())
            {
                iDay1 = Convert.ToInt32(txtDay1.Text);
                iDay2 = Convert.ToInt32(txtDay2.Text);
                iDay3 = Convert.ToInt32(txtDay3.Text);
                strBranch = ddlBranch.SelectedValue.ToString().Trim();

                if (ddlItemType.SelectedIndex > 0)
                {
                    strItemType = ddlItemType.SelectedValue.ToString().Trim();
                }

                if (ddlCatCode.SelectedIndex > 0)
                {
                    strCatCode = ddlCatCode.SelectedValue.ToString().Trim();
                }
                DataTable dt = SaitexBL.Interface.Method.TX_FIBER_PROD_MST.GetFiberStockAgingReport(oUserLoginDetail.COMP_CODE, strBranch, strCatCode, strItemType, iDay1, iDay2, iDay3);
                if (dt != null && dt.Rows.Count > 0)
                {
                    grdAgin.DataSource = dt;
                    grdAgin.DataBind();
                    lblTotalRecord.Text = dt.Rows.Count.ToString();

                    if (grdAgin.Rows.Count > 0)
                    {
                        int iCountgrd = grdAgin.Rows.Count;
                        int iCountLoop;
                        double dblCommonTotal = 0;
                        double dbllbld1day = 0;
                        double dbllbld2day = 0;
                        double dbllbld3day = 0;
                        double dbllbld4day = 0;
                        double dbllblTQTY = 0;
                        double dbllbld1dayval = 0;
                        double dbllbld2dayval = 0;
                        double dbllbld3dayval = 0;
                        double dbllbld4sayval = 0;
                        double dbllbltqtyval = 0;

                        for (iCountLoop = 0; iCountLoop < iCountgrd; iCountLoop++)
                        {
                            GridViewRow thisGrid = grdAgin.Rows[iCountLoop];
                            if (thisGrid.RowType == DataControlRowType.DataRow)
                            {
                                Label lbld1day = (Label)thisGrid.FindControl("lbld1day");
                                double.TryParse(lbld1day.Text.Trim(), out dblCommonTotal);
                                dbllbld1day = dbllbld1day + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lbld2day = (Label)thisGrid.FindControl("lbld2day");
                                double.TryParse(lbld2day.Text.Trim(), out dblCommonTotal);
                                dbllbld2day = dbllbld2day + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lbld3day = (Label)thisGrid.FindControl("lbld3day");
                                double.TryParse(lbld3day.Text.Trim(), out dblCommonTotal);
                                dbllbld3day = dbllbld3day + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lbld4day = (Label)thisGrid.FindControl("lbld4day");
                                double.TryParse(lbld4day.Text.Trim(), out dblCommonTotal);
                                dbllbld4day = dbllbld4day + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lblTQTY = (Label)thisGrid.FindControl("lblTQTY");
                                double.TryParse(lblTQTY.Text.Trim(), out dblCommonTotal);
                                dbllblTQTY = dbllblTQTY + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lbld1dayval = (Label)thisGrid.FindControl("lbld1dayval");
                                double.TryParse(lbld1dayval.Text.Trim(), out dblCommonTotal);
                                dbllbld1dayval = dbllbld1dayval + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lbld2dayval = (Label)thisGrid.FindControl("lbld2dayval");
                                double.TryParse(lbld2dayval.Text.Trim(), out dblCommonTotal);
                                dbllbld2dayval = dbllbld2dayval + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lbld3dayval = (Label)thisGrid.FindControl("lbld3dayval");
                                double.TryParse(lbld3dayval.Text.Trim(), out dblCommonTotal);
                                dbllbld3dayval = dbllbld3dayval + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lbld4sayval = (Label)thisGrid.FindControl("lbld4sayval");
                                double.TryParse(lbld4sayval.Text.Trim(), out dblCommonTotal);
                                dbllbld4sayval = dbllbld4sayval + dblCommonTotal;

                                dblCommonTotal = 0;
                                Label lbltqtyval = (Label)thisGrid.FindControl("lbltqtyval");
                                double.TryParse(lbltqtyval.Text.Trim(), out dblCommonTotal);
                                dbllbltqtyval = dbllbltqtyval + dblCommonTotal;
                            }
                        }

                        Label lblFtrDay1 = (Label)grdAgin.FooterRow.FindControl("lblFtrDay1");
                        Math.Round(dbllbld1day, 2);
                        lblFtrDay1.Text = dbllbld1day.ToString();

                        Label lblFtrDay2 = (Label)grdAgin.FooterRow.FindControl("lblFtrDay2");
                        Math.Round(dbllbld2day, 2);
                        lblFtrDay2.Text = dbllbld2day.ToString();

                        Label lblFtrDay3 = (Label)grdAgin.FooterRow.FindControl("lblFtrDay3");
                        Math.Round(dbllbld3day, 2);
                        lblFtrDay3.Text = dbllbld3day.ToString();

                        Label lblFtrDay4 = (Label)grdAgin.FooterRow.FindControl("lblFtrDay4");
                        Math.Round(dbllbld4day, 2);
                        lblFtrDay4.Text = dbllbld4day.ToString();

                        Label lblFtrTotQty = (Label)grdAgin.FooterRow.FindControl("lblFtrTotQty");
                        Math.Round(dbllblTQTY, 2);
                        lblFtrTotQty.Text = dbllblTQTY.ToString();

                        Label lblFtrDay1Value = (Label)grdAgin.FooterRow.FindControl("lblFtrDay1Value");
                        Math.Round(dbllbld1dayval, 2);
                        lblFtrDay1Value.Text = dbllbld1dayval.ToString();

                        Label lblFtrDay2Value = (Label)grdAgin.FooterRow.FindControl("lblFtrDay2Value");
                        Math.Round(dbllbld2dayval, 2);
                        lblFtrDay2Value.Text = dbllbld2dayval.ToString();

                        Label lblFtrDay3Value = (Label)grdAgin.FooterRow.FindControl("lblFtrDay3Value");
                        Math.Round(dbllbld3dayval, 2);
                        lblFtrDay3Value.Text = dbllbld3dayval.ToString();

                        Label lblFtrDay4Value = (Label)grdAgin.FooterRow.FindControl("lblFtrDay4Value");
                        Math.Round(dbllbld4sayval, 2);
                        lblFtrDay4Value.Text = dbllbld4sayval.ToString();

                        Label lblFtrTotQtyValue = (Label)grdAgin.FooterRow.FindControl("lblFtrTotQtyValue");
                        Math.Round(dbllbltqtyval, 2);
                        lblFtrTotQtyValue.Text = dbllbltqtyval.ToString();
                    }
                }
                else
                {
                    CommonFuction.ShowMessage("No Record found..");
                    lblTotalRecord.Text = "0";
                    grdAgin.DataSource = null;
                    grdAgin.DataBind();
                }
            }
            else
            {
                CommonFuction.ShowMessage("Enter mendatory fields (*)");
            }
        }
        catch
        {
            throw;
        }
    }
    protected void grdAgin_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[4].Text = "Stock <= " + txtDay1.Text + " Days";
                e.Row.Cells[5].Text = "Stock > " + txtDay1.Text + " and <= " + txtDay2.Text + " Days";
                e.Row.Cells[6].Text = "Stock > " + txtDay2.Text + " and <= " + txtDay3.Text + " Days";
                e.Row.Cells[7].Text = "Stock > " + txtDay3.Text + " Days";
                e.Row.Cells[9].Text = "Stock <= " + txtDay1.Text + " Days";
                e.Row.Cells[10].Text = "Stock > " + txtDay1.Text + " and <= " + txtDay2.Text + " Days";
                e.Row.Cells[11].Text = "Stock > " + txtDay2.Text + " and <= " + txtDay3.Text + " Days";
                e.Row.Cells[12].Text = "Stock > " + txtDay3.Text + " Days";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Data Bound..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void grdAgin_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdAgin_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}
