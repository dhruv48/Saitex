﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OracleClient;
using errorLog;
using Common;
using Obout.ComboBox;

public partial class Module_Production_Controls_ProductionQueryForm : System.Web.UI.UserControl
{
    string ORDER_NO = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    DateTime Sdate;
    DateTime Edate;
    DateTime StDate = System.DateTime.Now;
    DateTime EnDate = System.DateTime.Now;
    protected void Page_Load(object sender, EventArgs e)
   {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {  
                getBrachName();
                getDepartment();
                getBatchNo();
                GetLotNumber1();
                getMergeNo1();
                getOrderNo();
               
                //Sdate = oUserLoginDetail.DT_STARTDATE;
                //Edate = Common.CommonFuction.GetYearEndDate(Sdate);               
                //TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                //TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
                TxtFromDate.Text = DateTime.Now.ToShortDateString();
                TxtToDate.Text = DateTime.Now.ToShortDateString();
                bindGvItemMaster();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }

    private void getOrderNo()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetOrderNo();
            ddlOrderNo.DataSource = dt;
            ddlOrderNo.DataValueField = "ORDER_NO";
            ddlOrderNo.DataTextField = "ORDER_NO";
            ddlOrderNo.DataBind();
            ddlOrderNo.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void GetLotNumber1()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetLotNumber();
            ddlLotNo.DataSource = dt;
            ddlLotNo.DataValueField = "LOT_NUMBER";
            ddlLotNo.DataTextField = "LOT_NUMBER";
            ddlLotNo.DataBind();
            ddlLotNo.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getBatchNo()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.selectBatchNo();
            ddlBatchNo.DataSource = dt;
            ddlBatchNo.DataValueField = "PROS_ID_NO";
            ddlBatchNo.DataTextField = "PROS_ID_NO";
            ddlBatchNo.DataBind();
            ddlBatchNo.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getMergeNo1()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.selectMergeNo();
            ddlMergeNo.DataSource = dt;
            ddlMergeNo.DataValueField = "MERGE_NO";
            ddlMergeNo.DataTextField = "MERGE_NO";
            ddlMergeNo.DataBind();
            ddlMergeNo.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("------------Select------------",""));
            dt.Dispose();
            dt = null;
        }      
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getBrachName( )
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            ddlBranch.DataSource = dt;
            ddlBranch.DataValueField = "BRANCH_NAME";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    private DataTable bindGvItemMaster()       
  {
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string PROS_ID_NO = string.Empty;
        string FromDate = string.Empty;
        string ToDate = string.Empty;
        string LOT_NUMBER = string.Empty;
        string ORDER_NO = string.Empty;
        string BRANCH_NAME = string.Empty;
        string LOT_TYPE = string.Empty;
        string FINISH_TYPE = string.Empty;
        string MERGE_NO = string.Empty;
        string MACHINE = string.Empty;
        string ISBAL = string.Empty; 
        try
        {
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }
            if (ddlBatchNo.SelectedValue.ToString() != null && ddlBatchNo.SelectedValue.ToString() != string.Empty)
            {
                PROS_ID_NO = ddlBatchNo.SelectedValue.ToString();
            }
            else
            {
                PROS_ID_NO = string.Empty;
            }

            if (TxtFromDate.Text.ToString() != null && TxtFromDate.Text.ToString() != string.Empty)
            {
                FromDate = TxtFromDate.Text.ToString();
            }
            else
            {
                FromDate = string.Empty;
            }

            if (TxtToDate.Text.ToString() != null && TxtToDate.Text.ToString() != string.Empty)
            {
                ToDate = TxtToDate.Text.ToString();
            }
            else
            {
                ToDate = string.Empty ;
            }
            if (ddlLotNo.SelectedValue.ToString() != null && ddlLotNo.SelectedValue.ToString() != string.Empty)
            {
                LOT_NUMBER = ddlLotNo.SelectedValue.ToString();
            }
            else
            {
                LOT_NUMBER = string.Empty;
            }
            if (ddlLotType.SelectedValue.ToString() != null && ddlLotType.SelectedValue.ToString() != string.Empty)
            {
                LOT_TYPE = ddlLotType.SelectedValue.ToString();
            }
            else
            {
                LOT_TYPE = string.Empty;
            }
            if (ddlFinishType.SelectedValue.ToString() != null && ddlFinishType.SelectedValue.ToString() != string.Empty)
            {
                FINISH_TYPE = ddlFinishType.SelectedValue.ToString();
            }
            else
            {
                FINISH_TYPE = ddlFinishType.SelectedValue.ToString();
            }
            if (ddlMergeNo.SelectedValue.ToString() != null && ddlMergeNo.SelectedValue.ToString() != string.Empty)
            {
                MERGE_NO = ddlMergeNo.SelectedValue.ToString();
            }
            else
            {
                MERGE_NO = ddlMergeNo.SelectedValue.ToString();
            }
            if (ddlOrderNo.SelectedValue.ToString() != null && ddlOrderNo.SelectedValue.ToString() != string.Empty)
            {
                ORDER_NO = ddlOrderNo.SelectedValue.ToString();
            }
            else
            {
                ORDER_NO = ddlOrderNo.SelectedValue.ToString();
            }
            if (ddlMacCode.SelectedValue.ToString() != null && ddlMacCode.SelectedValue.ToString() != string.Empty)
            {
                MACHINE = ddlMacCode.SelectedValue.ToString();
            }
            else
            {
                MACHINE = ddlMacCode.SelectedValue.ToString();
            }
            if (chkBal.Checked)
            {
                ISBAL = "1";
            }
            else
            {
                ISBAL = "0";
            }


            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionQuery(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, ORDER_NO, "", MACHINE,ISBAL );
            if (dt.Rows.Count > 0)
            {
                Grid1.DataSource = dt;
                Grid1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                Grid1.Visible = true;
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
                lblTotalRecord.Text = "0";
                Grid1.Visible = false;
                Common.CommonFuction.ShowMessage("Record Not Available By This Parameter.");
            }
            CalculateAllData();
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            bindGvItemMaster();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./ProductionQueryForm.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string QueryString = "";
            bool flag = false;
            if (ddlBranch.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "BRANCH_NAME=" + ddlBranch.SelectedValue.Trim();
                flag = true;
            }
           
            if (ddlLotNo.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "LOT_NO=" + ddlLotNo.SelectedValue.Trim();
                flag = true;
            }

            if (ddlBatchNo.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "PROS_ID_NO=" + ddlBatchNo.SelectedValue.Trim();
                flag = true;
            }

            if (ddlDepartment.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "DEPT_CODE=" + ddlDepartment.SelectedValue.Trim();
                flag = true;
            }
            if (ddlLotType.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "LOT_TYPE=" + ddlLotType.SelectedValue.Trim();
                flag = true;
            }
            if (ddlFinishType.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "FINISH_TYPE=" + ddlFinishType.SelectedValue.Trim();
                flag = true;
            }
            if (ddlMergeNo.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "MERGE_NO=" + ddlMergeNo.SelectedValue.Trim();
                flag = true;
            }    
                if (ddlOrderNo.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "ORDER_NO=" + ddlOrderNo.SelectedValue.Trim();
                flag = true;
            }
                if (ddlMacCode.SelectedValue.Trim() != "")
                {
                    if (!flag)
                        QueryString = QueryString + "?";
                    else
                        QueryString = QueryString + "&";
                    QueryString = QueryString + "MACHINE=" + ddlMacCode.SelectedValue.Trim();
                    flag = true;
                }
                if (TxtFromDate.Text.ToString() != null && TxtFromDate.Text.ToString() != string.Empty)
                {
                    if (!flag)
                        QueryString = QueryString + "?";
                    else
                        QueryString = QueryString + "&";
                    QueryString = QueryString + "FROM_DATE=" + TxtFromDate.Text.ToString();
                    flag = true;
                }


                if (TxtToDate.Text.ToString() != null && TxtToDate.Text.ToString() != string.Empty)
                {
                    if (!flag)
                        QueryString = QueryString + "?";
                    else
                        QueryString = QueryString + "&";
                    QueryString = QueryString + "TO_DATE=" + TxtToDate.Text.ToString();
                    flag = true;
                }

                if (chkBal.Checked )
                {
                    if (!flag)
                        QueryString = QueryString + "?";
                    else
                        QueryString = QueryString + "&";
                    QueryString = QueryString + "IS_BAL=1";
                    flag = true;
                }
            string URL = "../../Production/Report/ProductionFormReport.aspx" + QueryString;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtToDate.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                if (StartDate < StDate || StartDate > EnDate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = StDate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Yarn_Prduction_Details_List_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(bindGvItemMaster(), strFilename, "Yarn Prduction Details List");



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

    protected void CalculateAllData()
    {
        if (Grid1.Rows.Count > 0)
        {
            double TotalPOYIssueQty = 0;
            double TotalPOYIssueCops = 0;
            //double TotalProductionCops = 0;
            //double TotalDoff = 0;
            //double TotalPackedQty = 0;
            //double TotalPackedCops = 0;
            double TotalProductionQty = 0;
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                 double POYIssueQty = 0;
            double POYIssueCops = 0;
            double ProductionCops = 0;
            double Doff = 0;
            double PackedQty = 0;
            double PackedCops = 0;
            double ProductionQty = 0;



                Label lblPOYIssueQty = Grid1.Rows[i].FindControl("lblPOYIssueQty") as Label;
                Label lblPOYIssueCops = Grid1.Rows[i].FindControl("lblPOYIssueCops") as Label;
                Label lblProductionQty = Grid1.Rows[i].FindControl("lblProductionQty") as Label;
                //Label lblProductionCops = Grid1.Rows[i].FindControl("lblProductionCops") as Label;
                // Label lblDoff = Grid1.Rows[i].FindControl("lblDoff") as Label;
                //Label lblPackedQty = Grid1.Rows[i].FindControl("lblPackedQty") as Label;
                //Label lblPackedCops = Grid1.Rows[i].FindControl("lblPackedCops") as Label;

                double.TryParse(lblPOYIssueQty.Text, out POYIssueQty);
                double.TryParse(lblPOYIssueCops.Text, out POYIssueCops);
                double.TryParse(lblProductionQty.Text, out ProductionQty);
                //double.TryParse(lblProductionCops.Text, out ProductionCops);
                //double.TryParse(lblDoff.Text, out Doff);
                //double.TryParse(lblPackedQty.Text, out PackedQty);
                //double.TryParse(lblPackedCops.Text, out PackedCops);
                TotalPOYIssueQty = TotalPOYIssueQty + POYIssueQty;
                TotalPOYIssueCops = TotalPOYIssueCops + POYIssueCops;
                TotalProductionQty = TotalProductionQty + ProductionQty;
                //TotalProductionCops = TotalProductionCops + ProductionCops;
                //TotalDoff = TotalDoff + Doff;
                //TotalPackedQty = TotalPackedQty + PackedQty;
                //TotalPackedCops = TotalPackedCops + PackedCops;
               

            }

            ((Label)Grid1.FooterRow.FindControl("lblTotalPOYIssueQty")).Text = TotalPOYIssueQty.ToString();
            ((Label)Grid1.FooterRow.FindControl("lblTotalPOYIssueCops")).Text = TotalPOYIssueCops.ToString();
            ((Label)Grid1.FooterRow.FindControl("lblTotalProductionQty")).Text = TotalProductionQty.ToString();
            //((Label)Grid1.FooterRow.FindControl("lblTotalProductionCops")).Text = TotalProductionCops.ToString();
            //((Label)Grid1.FooterRow.FindControl("lblTotalDoff")).Text = TotalDoff.ToString();
            //((Label)Grid1.FooterRow.FindControl("lblTotalPackedQty")).Text = TotalPackedQty.ToString();
            //((Label)Grid1.FooterRow.FindControl("lblTotalPackedCops")).Text = TotalPackedCops.ToString();

        }
    }



    protected void ddlMacCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMacData(e.Text.ToUpper(), e.ItemsOffset);
            ddlMacCode.Items.Clear();
            ddlMacCode.DataSource = data;
            ddlMacCode.DataTextField = "MACHINE_CODE";
            ddlMacCode.DataValueField = "MACHINE_CODE";
            ddlMacCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            e.ItemsCount = GetMacCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for Machine Code.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetMacData(string text, int startoffset)
    {
        try
        {
            DataTable dt = new DataTable();
            string commandText = string.Empty;
            //if (startoffset == 0)
            //{
            //    commandText += "SELECT 'MISC' AS MACHINE_CODE, 'MISC' AS MACHINE_GROUP, 0 AS MACHINE_CAPACITY, 'MISC' AS MACHINE_SEGMENT, 'MISC' AS MACHINE_TYPE, 'MISC' AS MACHINE_SEC,'OLD_MACHINE_NAME' AS OLD_MACHINE_NAME FROM DUAL UNION ALL ";
            //}
            commandText += " SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC,OLD_MACHINE_NAME FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += " AND MACHINE_CODE NOT IN(SELECT MACHINE_CODE FROM (SELECT MACHINE_CODE,MACHINE_GROUP,MACHINE_CAPACITY,MACHINE_SEGMENT,MACHINE_TYPE,MACHINE_SEC,OLD_MACHINE_NAME FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC)WHERE ROWNUM <= '" + startoffset + "')";
            }

            string SortExpression = " order by MACHINE_CODE asc";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetMacCount(string text)
    {
        try
        {
            DataTable dt = new DataTable();

            string whereClause = " ";
            string sortExpression = " ";
            string commandText = "SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC)  ";

            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");

            return dt.Rows.Count;
        }
        catch
        {
            throw;
        }
    }


}