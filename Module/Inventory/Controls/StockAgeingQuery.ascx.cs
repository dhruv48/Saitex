using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


using Common;
using System.Data;

public partial class Module_Inventory_Controls_StockAgeingQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                BindStockAgeingGrid();
                btnPrint.Attributes.Add("onclick", "javascript:CallPrint('divPrint');");
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
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = false;
            lblMode.Text = "You are in Print Mode";
            txtDay1.Text = "15";
            txtDay2.Text = "30";
            txtDay3.Text = "45";

            GetBranch();
            GetItemType();
            GetItemCategory();
            BindDropDown(ddllocation);
            BindDepartment(ddlstore);
            //BindStockAgeingGrid();
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
            ddlBranch.SelectedIndex = 0;
            //ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(oUserLoginDetail.CH_BRANCHCODE));
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
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemType();
            DataView Dv = new DataView(dt);
            ddlItemType.DataSource = Dv;
            ddlItemType.DataValueField = "ITEM_TYPE";
            ddlItemType.DataTextField = "ITEM_TYPE";
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
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemCate();
            ddlCatCode.DataSource = dt;
            ddlCatCode.DataValueField = "CAT_CODE";
            ddlCatCode.DataTextField = "CAT_CODE";
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

    private void BindStockAgeingGrid()
    {
        try
        {
            string strBranch = string.Empty;
            string strItemType = string.Empty;
            string strCatCode = string.Empty;
            string strlocation = string.Empty;
            string strstore = string.Empty;
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
                if (ddllocation.SelectedIndex > 0)
                {
                    strlocation = ddllocation.SelectedValue.ToString().Trim();
                }
                if (ddlstore.SelectedIndex > 0)
                {
                    strstore = ddlstore.SelectedValue.ToString().Trim();
                }

                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_STOCK_DATA.GetItemStockAgingReport(oUserLoginDetail.COMP_CODE, strBranch, strItemType, strCatCode, iDay1, iDay2, iDay3,strlocation,strstore);
                if (dt != null && dt.Rows.Count > 0)
                {
                    grdStockAgeing.DataSource = dt;
                    grdStockAgeing.DataBind();
                    lblTotalRecord.Text = dt.Rows.Count.ToString();

                    if (grdStockAgeing.Rows.Count > 0)
                    {
                        int iCountgrd = grdStockAgeing.Rows.Count;
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
                            GridViewRow thisGrid = grdStockAgeing.Rows[iCountLoop];
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

                        Label lblFtrDay1 = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrDay1");
                        Math.Round(dbllbld1day, 2);
                        lblFtrDay1.Text = dbllbld1day.ToString();

                        Label lblFtrDay2 = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrDay2");
                        Math.Round(dbllbld2day, 2);
                        lblFtrDay2.Text = dbllbld2day.ToString();

                        Label lblFtrDay3 = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrDay3");
                        Math.Round(dbllbld3day, 2);
                        lblFtrDay3.Text = dbllbld3day.ToString();

                        Label lblFtrDay4 = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrDay4");
                        Math.Round(dbllbld4day, 2);
                        lblFtrDay4.Text = dbllbld4day.ToString();

                        Label lblFtrTotQty = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrTotQty");
                        Math.Round(dbllblTQTY, 2);
                        lblFtrTotQty.Text = dbllblTQTY.ToString();

                        Label lblFtrDay1Value = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrDay1Value");
                        Math.Round(dbllbld1dayval, 2);
                        lblFtrDay1Value.Text = dbllbld1dayval.ToString();

                        Label lblFtrDay2Value = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrDay2Value");
                        Math.Round(dbllbld2dayval, 2);
                        lblFtrDay2Value.Text = dbllbld2dayval.ToString();

                        Label lblFtrDay3Value = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrDay3Value");
                        Math.Round(dbllbld3dayval, 2);
                        lblFtrDay3Value.Text = dbllbld3dayval.ToString();

                        Label lblFtrDay4Value = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrDay4Value");
                        Math.Round(dbllbld4sayval, 2);
                        lblFtrDay4Value.Text = dbllbld4sayval.ToString();

                        Label lblFtrTotQtyValue = (Label)grdStockAgeing.FooterRow.FindControl("lblFtrTotQtyValue");
                        Math.Round(dbllbltqtyval, 2);
                        lblFtrTotQtyValue.Text = dbllbltqtyval.ToString();
                    }
                }
                else
                {
                    CommonFuction.ShowMessage("No Record found..");
                    lblTotalRecord.Text = "0";
                    grdStockAgeing.DataSource = null;
                    grdStockAgeing.DataBind();
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


    private DataTable BindStockAgeingGrid1()
    {
        try
        {
            DataTable dt; 
            string strBranch = string.Empty;
            string strItemType = string.Empty;
            string strCatCode = string.Empty;
            string strlocation = string.Empty;
            string strstore = string.Empty;
            int iDay1;
            int iDay2;
            int iDay3;

           
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
                if (ddllocation.SelectedIndex > 0)
                {
                    strlocation = ddllocation.SelectedValue.ToString().Trim();
                }
                if (ddlstore.SelectedIndex > 0)
                {
                    strstore = ddlstore.SelectedValue.ToString().Trim();
                }

                 dt = SaitexBL.Interface.Method.TX_ITEM_STOCK_DATA.GetItemStockAgingReport(oUserLoginDetail.COMP_CODE, strBranch, strItemType, strCatCode, iDay1, iDay2, iDay3, strlocation, strstore);

                 return dt;
           
        }
        catch
        {
            throw;
        }
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

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("../Reports/MaterialStockAging_OPT.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./StockAgeingQueryForm.aspx", false);
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindStockAgeingGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Branch Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindStockAgeingGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Item Type Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlCatCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindStockAgeingGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Category Code Selection..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdStockAgeing_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
            imgbtnUpdate.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to update this record')");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void BindDropDown(DropDownList ddllocation)
    {
        try
        {
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
            //if (dt != null && dt.Rows.Count > 0)
            //{


            ddllocation.DataSource = dt;
            ddllocation.DataValueField = "MST_DESC";
            ddllocation.DataTextField = "MST_DESC";
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        //}
        //else
        //{
        //    ddllocation.DataSource = null;
        //    ddllocation.DataBind();
        //    ddllocation.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

        //}
        //ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
        catch
        {
            throw;
        }
    }
    private void BindDepartment(DropDownList ddlstore)
    {
        try
        {
            ddlstore.Items.Clear();
            DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();



            //if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            //{

                ddlstore.DataSource = dtDepartment;
                ddlstore.DataValueField = "DEPT_NAME";
                ddlstore.DataTextField = "DEPT_NAME";
                ddlstore.DataBind();
                ddlstore.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dtDepartment.Dispose();
                dtDepartment = null;
            //}
            //ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }


    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Material_Transaction_Query_Form_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(BindStockAgeingGrid1(), strFilename, "Material Transaction Query Form");

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
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

}


