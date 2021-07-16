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
public partial class Module_Machine_Controls_ProcessRouteQuery : System.Web.UI.UserControl
{
    private static string PROS_CODE = string.Empty;
    private static string MST_CODE = string.Empty;
    private static string PROS_ROUTE_CODE = string.Empty;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                GridProcesss();
                bindddlProcessCode();
                binddlProcess();
                bindddlProsRoute();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Load .\r\nSee error log for detail."));
        }
    }
    private void bindddlProcessCode()
    {

        try
        {
            ddlprocessno.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.TX_MAC_PROC_MST.Get_PROCODE();
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
    private void binddlProcess()
    {
        try
        {
            ddlProcess.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_PRO_STN_MST.GetProcess();
           if (dt != null && dt.Rows.Count > 0)
            {
                ddlProcess.DataValueField = "MST_CODE";
                ddlProcess.DataTextField = "MST_DESC";
                ddlProcess.DataSource = dt;
                ddlProcess.DataBind();
            }
            ddlProcess.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddlProsRoute()
    {
      

        try
        {
            ddlProsRoute.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.TX_PRO_STN_MST.GetProcessRoutecode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlProsRoute.DataValueField = "PROS_ROUTE_CODE";
                ddlProsRoute.DataTextField = "PROS_ROUTE_CODE";
                ddlProsRoute.DataSource = dt;
                ddlProsRoute.DataBind();
            }
            ddlProsRoute.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }

    }
    private void GridProcesss()
    {
        try
        {
            if (ddlprocessno.SelectedValue.ToString() != null && ddlprocessno.SelectedValue.ToString() != string.Empty)
            {
                PROS_CODE = ddlprocessno.SelectedValue.ToString();
            }
            else
            {
                PROS_CODE = string.Empty;
            }


            if (ddlProcess.SelectedValue.ToString() != null && ddlProcess.SelectedValue.ToString() != string.Empty)
            {
                MST_CODE= ddlProcess.SelectedValue.ToString();
            }
            else
            {
                MST_CODE = string.Empty;
            }
            if (ddlProsRoute.SelectedValue.ToString() != null && ddlProsRoute.SelectedValue.ToString() != string.Empty)
            {
                PROS_ROUTE_CODE = ddlProsRoute.SelectedValue.ToString();
            }
            else
            {
                PROS_ROUTE_CODE = string.Empty;
            }

            DataTable DT = SaitexBL.Interface.Method.TX_PRO_STN_MST.ProcessRouteQuery(PROS_CODE, MST_CODE, PROS_ROUTE_CODE);
            if (DT != null && DT.Rows.Count > 0)
            {
                GridProcess.DataSource = DT;
                GridProcess.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
            }
            else
            {
                GridProcess.DataSource = null;
                GridProcess.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
                CommonFuction.ShowMessage("Data not Found by selected Item .");

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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit .\r\nSee error log for detail."));
        }
    }
    protected void ddlProsRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProcesss();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Item.\r\nSee error log for detail."));
        }

    }
    protected void ddlprocessno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProcesss();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Item\r\nSee error log for detail."));
        }

    }
    protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProcesss();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Item.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PROS_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "MST_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PROS_ROUTE_CODE";
            myDataTable.Columns.Add(myDataColumn);


            DataRow row;
            row = myDataTable.NewRow();

            row["PROS_CODE"] = ddlprocessno.SelectedValue.ToString();
            row["MST_CODE"] = ddlProcess.SelectedValue.ToString();
            row["PROS_ROUTE_CODE"] = ddlProsRoute.SelectedValue.ToString();


            myDataTable.Rows.Add(row);
            Session["Proceereport"] = myDataTable;
            Response.Redirect("~/Module/Machine/Reports/ProcessRouteQueryReport.aspx", false);
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }         
    }
    protected void GridProcess_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridProcess.PageIndex = e.NewPageIndex;
        GridProcesss();

    }
}
