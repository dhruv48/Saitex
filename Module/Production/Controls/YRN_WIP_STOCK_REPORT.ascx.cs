﻿using System;
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
public partial class Module_Production_Controls_YRN_WIP_STOCK_REPORT : System.Web.UI.UserControl
{
    private static string PI_NO = string.Empty;
    private static string DEPT_CODE = string.Empty;
    private static string BRANCH_CODE = string.Empty;
    private static string PROS_CODE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
              
               GridYRN_STOCKs();
               bindddlOrderno();
               bindddlDepartment();         
               bindddlProcessno();
               bindBranch();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    private void GridYRN_STOCKs()
    {
        try
        {
            if (ddlDepartMent.SelectedValue.ToString() != null && ddlDepartMent.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddlDepartMent.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }
           
            
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlprocessno.SelectedValue.ToString() != null && ddlprocessno.SelectedValue.ToString() != string.Empty)
            {
                PROS_CODE = ddlprocessno.SelectedValue.ToString();
            }
            else
            {
                PROS_CODE = string.Empty;
            }
            if (ddlOrderNo.SelectedValue.ToString() != null && ddlOrderNo.SelectedValue.ToString() != string.Empty)
            {
                PI_NO = ddlOrderNo.SelectedValue.ToString();
            }
            else
            {
                PI_NO = string.Empty;
            }


            DataTable DT = SaitexBL.Interface.Method.YRN_WIP_LOT_MOVE_LOG.YRN_WIP_STOCK_REPORT(DEPT_CODE, PROS_CODE, BRANCH_CODE, PI_NO);
            if (DT != null && DT.Rows.Count > 0)
            {
                GridYRN_STOCK.DataSource = DT;
                GridYRN_STOCK.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
            }
            else
            {
                GridYRN_STOCK.DataSource = null ;
                GridYRN_STOCK.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
            
            }
        }
        catch
        {
            throw;
        }

    }
    private void bindddlOrderno()
    {
        try
        {
            ddlOrderNo.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_WIP_LOT_MOVE_LOG.YRN_WIP_STOCK_PI_NO();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlOrderNo.DataValueField = "PI_NO";
                ddlOrderNo.DataTextField = "PI_NO";
                ddlOrderNo.DataSource = dt;
                ddlOrderNo.DataBind();
            }
            ddlOrderNo.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddlDepartment()
    {
        try
        {
            ddlDepartMent.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_WIP_LOT_MOVE_LOG.YRN_WIP_STOCK_DEPTCODE();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlDepartMent.DataValueField = "DEPT_CODE";
                ddlDepartMent.DataTextField = "DEPT_NAME";
                ddlDepartMent.DataSource = dt;
                ddlDepartMent.DataBind();
            }
            ddlDepartMent.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddlProcessno()
      {
     
        try
        {
            ddlprocessno.Items.Clear();
           
            DataTable dt = SaitexBL.Interface.Method.YRN_WIP_LOT_MOVE_LOG.YRN_WIP_STOCK_PROCODE();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlprocessno.DataValueField = "PROS_CODE";
                ddlprocessno.DataTextField = "PROS_DESC";
                ddlprocessno.DataSource = dt;
                ddlprocessno.DataBind();
            }
            ddlprocessno.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
       
       
    }
    private void bindBranch()
    {

        try
        {  
            ddlbranch.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_WIP_LOT_MOVE_LOG.YRN_WIP_STOCK_BRANCHCODE();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlbranch.DataValueField = "BRANCH_CODE";
                ddlbranch.DataTextField = "BRANCH_NAME";
                ddlbranch.DataSource = dt;
                ddlbranch.DataBind();
          
            }
            ddlbranch.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }


    }
    protected void ddlprocessno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridYRN_STOCKs();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridYRN_STOCKs();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void ddlDepartMent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridYRN_STOCKs();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }

    }
    protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            GridYRN_STOCKs();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DEPT_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PROS_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BRANCH_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PI_NO";
            myDataTable.Columns.Add(myDataColumn);           
            DataRow row;

            row = myDataTable.NewRow();
            row["DEPT_CODE"] = ddlDepartMent.SelectedValue.ToString();
            row["PROS_CODE"] = ddlprocessno.SelectedValue.ToString();
            row["BRANCH_CODE"] = ddlbranch.SelectedValue.ToString();
            row["PI_NO"] = ddlOrderNo.SelectedValue.ToString();

            myDataTable.Rows.Add(row);
            Session["Proceereport"] = myDataTable;



            Response.Redirect("~/Module/Production/Report/WorkProgressReport.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }         
    }
    protected void GridYRN_STOCK_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridYRN_STOCK.PageIndex = e.NewPageIndex;
        GridYRN_STOCKs();
    }
}
