using System;
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

public partial class Module_Production_Report_ProductionDoffReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    DateTime Sdate;
    DateTime Edate;
    DateTime StDate = System.DateTime.Now;
    DateTime EnDate = System.DateTime.Now;
    string ORDER_NO = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                getBrachName();
                getDepartment();
                getBatchNo1();
                getLotNo();
                getMergeNo();
                getOrderNo();
                getProdProcIdNo();
                bindGvItemMaster();
                Sdate = oUserLoginDetail.DT_STARTDATE;
                Edate = Common.CommonFuction.GetYearEndDate(Sdate);
                TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }

    private void getLotNo()
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


    private void getBatchNo1()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.selectBatchNo();
            //ddlBatchNo.DataSource = dt;
            //ddlBatchNo.DataValueField = "PROS_ID_NO";
            //ddlBatchNo.DataTextField = "PROS_ID_NO";
            //ddlBatchNo.DataBind();
            //ddlBatchNo.Items.Insert(0, new ListItem("------------Select------------", ""));

            ddlProdPocsIdNo.DataSource = dt;
            ddlProdPocsIdNo.DataValueField = "PROS_ID_NO";
            ddlProdPocsIdNo.DataTextField = "PROS_ID_NO";
            ddlProdPocsIdNo.DataBind();
            ddlProdPocsIdNo.Items.Insert(0, new ListItem("------------Select------------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getProdProcIdNo()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.selectProdProcsIdNo();         
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
    private void getMergeNo()
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
            ddlDepartment.Items.Insert(0, new ListItem("------------Select------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getBrachName()
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
        string PROD_PROS_ID_NO = string.Empty;
        string MACHINE = string.Empty;

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


            if (TxtFromDate.Text.ToString() != null && TxtFromDate.Text.ToString() != string.Empty)
            {
                FromDate = TxtFromDate.Text.ToString();
            }
            else
            {
                FromDate = string.Empty; ;
            }

            if (TxtToDate.Text.ToString() != null && TxtToDate.Text.ToString() != string.Empty)
            {
                ToDate = TxtToDate.Text.ToString();
            }
            else
            {
                ToDate = string.Empty; ;
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

            if (ddlBatchNo.SelectedValue.ToString() != null && ddlBatchNo.SelectedValue.ToString() != string.Empty)
            {

                PROS_ID_NO = ddlBatchNo.SelectedValue.ToString();
            }
            else
            {
                PROS_ID_NO = string.Empty;

            }


            if (ddlProdPocsIdNo.SelectedValue.ToString() != null && ddlProdPocsIdNo.SelectedValue.ToString() != string.Empty)
            {

                PROD_PROS_ID_NO = ddlProdPocsIdNo.SelectedValue.ToString();
            }
            else
            {

                PROD_PROS_ID_NO = ddlProdPocsIdNo.SelectedValue.ToString();
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

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionDoffQuery(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, LOT_TYPE, FINISH_TYPE, MERGE_NO, PROD_PROS_ID_NO, ORDER_NO, MACHINE,"");
            if (dt.Rows.Count > 0)
            {
                //Grid1.DataSource = dt;
                //Grid1.DataBind();
                //lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                //Grid1.Visible = true;
            }
            else
            {
                //Grid1.DataSource = null;
                //Grid1.DataBind();
                //lblTotalRecord.Text = "0";
                //Grid1.Visible = false;
                //Common.CommonFuction.ShowMessage("Record Not Available By This Parameter.");
            }
          
            return dt;
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
            Response.Redirect("./ProductionDoffQueryForm.aspx", false);
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
            if (ddlProdPocsIdNo.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "PROD_PROS_ID_NO=" + ddlProdPocsIdNo.SelectedValue.Trim();
                flag = true;
            }
            if (ddlOrderNo.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
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
            string chk = string.Empty;
            if (redForQuery.SelectedValue == "red")
            {
                chk = "0";
            }
            else if (redForQuery.SelectedValue == "green")
            {
                chk = "1";
            }
            else if (redForQuery.SelectedValue == "blue")
            {
                chk = "2";
            }
            else if (redForQuery.SelectedValue == "yellow")
            {
                chk = "3";
            }
            else if (redForQuery.SelectedValue == "dark")
            {
                chk = "4";
            }
            else if (redForQuery.SelectedValue == "sky")
            {
                chk = "5";
            }
            
            if (chk != null && chk != string.Empty)
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "chk=" + chk;
                flag = true;
            }
           // string URL = "../../Production/Report/ProductionDoffFormReport.aspx" + QueryString;
             string URL = "../../Production/Report/YarnDoffProductionReport.aspx" + QueryString;
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
        string strFilename = string.Empty;
        string strTitle = string.Empty;
        DataTable dt = null;
        string TRN_TYPE = string.Empty;
        if (redForQuery.SelectedValue == "red")
        {
            strFilename = "PRODUCTION_SUMMARY_" + DateTime.Now.ToString() + ".xls";
            strTitle = "PRODUCTION SUMMARY ";
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionSummary(ddlBranch.SelectedValue, ddlDepartment.SelectedValue, ddlBatchNo.SelectedValue.Trim(), TxtFromDate.Text, TxtToDate.Text, ddlLotNo.SelectedValue, ddlLotType.SelectedValue, ddlFinishType.SelectedValue, ddlMergeNo.SelectedValue, ddlProdPocsIdNo.SelectedValue, ddlOrderNo.SelectedValue, ddlMacCode.SelectedValue, TRN_TYPE);
          
        }
        else if (redForQuery.SelectedValue == "green")
        {
            strFilename = "LOT_WISE_PRODUCTION_SUMMARY_" + DateTime.Now.ToString() + ".xls";
            strTitle = "LOT WISE PRODUCTION SUMMARY ";
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionLotWiseSummary(ddlBranch.SelectedValue, ddlDepartment.SelectedValue, ddlBatchNo.SelectedValue.Trim(), TxtFromDate.Text, TxtToDate.Text, ddlLotNo.SelectedValue, ddlLotType.SelectedValue, ddlFinishType.SelectedValue, ddlMergeNo.SelectedValue, ddlProdPocsIdNo.SelectedValue, ddlOrderNo.SelectedValue, ddlMacCode.SelectedValue, TRN_TYPE);

        }
        else if (redForQuery.SelectedValue == "blue")
        {
            strFilename = "LOT_WISE_PRODUCTION_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "LOT WISE PRODUCTION DETAILS ";
            
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionLotMachineWiseDetails(ddlBranch.SelectedValue, ddlDepartment.SelectedValue, ddlBatchNo.SelectedValue.Trim(), TxtFromDate.Text, TxtToDate.Text, ddlLotNo.SelectedValue, ddlLotType.SelectedValue, ddlFinishType.SelectedValue, ddlMergeNo.SelectedValue, ddlProdPocsIdNo.SelectedValue, ddlOrderNo.SelectedValue, ddlMacCode.SelectedValue, TRN_TYPE);
         
        }
        else if (redForQuery.SelectedValue == "yellow")
        {
            strFilename = "MACHINE_WISE_PRODUCTION_SUMMARY_" + DateTime.Now.ToString() + ".xls";
            strTitle = "MACHINE WISE PRODUCTION SUMMARY ";
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionMachineWiseSummary(ddlBranch.SelectedValue, ddlDepartment.SelectedValue, ddlBatchNo.SelectedValue.Trim(), TxtFromDate.Text, TxtToDate.Text, ddlLotNo.SelectedValue, ddlLotType.SelectedValue, ddlFinishType.SelectedValue, ddlMergeNo.SelectedValue, ddlProdPocsIdNo.SelectedValue, ddlOrderNo.SelectedValue, ddlMacCode.SelectedValue, TRN_TYPE);
          
        }
        else if (redForQuery.SelectedValue == "dark")
        {
            strFilename = "MACHINE_WISE_PRODUCTION_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "MACHINE WISE PRODUCTION DETAILS ";
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionLotMachineWiseDetails(ddlBranch.SelectedValue, ddlDepartment.SelectedValue, ddlBatchNo.SelectedValue.Trim(), TxtFromDate.Text, TxtToDate.Text, ddlLotNo.SelectedValue, ddlLotType.SelectedValue, ddlFinishType.SelectedValue, ddlMergeNo.SelectedValue, ddlProdPocsIdNo.SelectedValue, ddlOrderNo.SelectedValue, ddlMacCode.SelectedValue, TRN_TYPE);
          
        }
        else if (redForQuery.SelectedValue == "sky")
        {
            strFilename = "PRODUCTION_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "PRODUCTION DETAILS ";
           
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.SelectProductionDoffQuery(ddlBranch.SelectedValue, ddlDepartment.SelectedValue, ddlBatchNo.SelectedValue.Trim(), TxtFromDate.Text, TxtToDate.Text, ddlLotNo.SelectedValue, ddlLotType.SelectedValue, ddlFinishType.SelectedValue, ddlMergeNo.SelectedValue, ddlProdPocsIdNo.SelectedValue, ddlOrderNo.SelectedValue, ddlMacCode.SelectedValue, TRN_TYPE);

        }
            
       
        

        ExporttoExcel(dt, strFilename, strTitle);      
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
            
        }
    }

    protected DataTable GetMacData(string text, int startoffset)
    {
        try
        {
            DataTable dt = new DataTable();
            string commandText = string.Empty;
            if (startoffset == 0)
            {
                commandText += "SELECT 'MISC' AS MACHINE_CODE, 'MISC' AS MACHINE_GROUP, 0 AS MACHINE_CAPACITY, 'MISC' AS MACHINE_SEGMENT, 'MISC' AS MACHINE_TYPE, 'MISC' AS MACHINE_SEC FROM DUAL UNION ALL ";
            }
            commandText += " SELECT * FROM (SELECT MACHINE_CODE, MACHINE_GROUP, MACHINE_CAPACITY, MACHINE_SEGMENT, MACHINE_TYPE, MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startoffset != 0)
            {
                whereClause += " AND MACHINE_CODE NOT IN(SELECT MACHINE_CODE FROM (SELECT MACHINE_CODE,MACHINE_GROUP,MACHINE_CAPACITY,MACHINE_SEGMENT,MACHINE_TYPE,MACHINE_SEC FROM v_MC_MACHINE_MASTER WHERE MACHINE_CODE LIKE :SearchQuery OR MACHINE_GROUP LIKE :SearchQuery OR MACHINE_CAPACITY LIKE :SearchQuery OR MACHINE_SEGMENT LIKE :SearchQuery OR MACHINE_TYPE LIKE :SearchQuery ORDER BY MACHINE_CODE ASC)WHERE ROWNUM <= '" + startoffset + "')";
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
