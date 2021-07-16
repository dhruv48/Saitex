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
public partial class Module_Machine_Controls_ProcessMasterQuery : System.Web.UI.UserControl
{
    private static string PROS_CODE = string.Empty;
    private static string DEPT_CODE = string.Empty;
    private static string MACHINE_GROUP = string.Empty;
    private static string MST_CODE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                GridProcesss();
                bindddlProcessCode();
                bindddlDeptCode();
                bindddlMacCode();
                bindddlMainProcess();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Load\r\nSee error log for detail."));
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
            ddlprocessno.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }


    }
    private void bindddlDeptCode()
    {

        try
        {
            ddldepartment.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldepartment.DataTextField = "DEPT_NAME";
                ddldepartment.DataValueField = "DEPT_CODE";
                ddldepartment.DataSource = dt;
                ddldepartment.DataBind();

            }
            ddldepartment.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddlMacCode()
    {

        try
        {
            ddlmaccode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MAC_PROC_MST.Get_Maccode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlmaccode.DataTextField = "MACHINE_GROUP";
                ddlmaccode.DataValueField = "MACHINE_GROUP";
                ddlmaccode.DataSource = dt;
                ddlmaccode.DataBind();

            }
            ddlmaccode.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddlMainProcess()
    {

        try
        {
            ddlmainprocess.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MAC_PROC_MST.Get_MainProcess();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlmainprocess.DataTextField = "MST_CODE";
                ddlmainprocess.DataValueField = "MST_DESC";
                ddlmainprocess.DataSource = dt;
                ddlmainprocess.DataBind();

            }
            ddlmainprocess.Items.Insert(0, new ListItem("Select", ""));
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


            if (ddldepartment.SelectedValue.ToString() != null && ddldepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddldepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }

            if (ddlmaccode.SelectedValue.ToString() != null && ddlmaccode.SelectedValue.ToString() != string.Empty)
            {
                MACHINE_GROUP = ddlmaccode.SelectedValue.ToString();
            }
            else
            {
                MACHINE_GROUP = string.Empty;
            }

            if (ddlmainprocess.SelectedValue.ToString() != null && ddlmainprocess.SelectedValue.ToString()!= string .Empty )
            {
                MST_CODE = ddlmainprocess.SelectedValue.ToString();
            }
            else 
            {
                MST_CODE = string.Empty;
            }

            DataTable DT = SaitexBL.Interface.Method.TX_MAC_PROC_MST.ProcessMasterQuery(PROS_CODE, DEPT_CODE, MACHINE_GROUP, MST_CODE);
            if (DT != null && DT.Rows.Count > 0)
            {
                GridProcessMaster.DataSource = DT;
                GridProcessMaster.DataBind();
            }
            else
            {
                GridProcessMaster.DataSource = null;
                GridProcessMaster.DataBind();
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
    protected void ddlmaccode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProcesss();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting .\r\nSee error log for detail."));
        }

    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProcesss();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting .\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting .\r\nSee error log for detail."));
        }

    }
    protected void ddlmainprocess_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridProcesss();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting .\r\nSee error log for detail."));
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
            myDataColumn.ColumnName = "PROS_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DEPT_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "MACHINE_GROUP";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "MST_CODE";
            myDataTable.Columns.Add(myDataColumn);



            DataRow row;
            row = myDataTable.NewRow();
     
            row["PROS_CODE"] = ddlprocessno.SelectedValue.ToString();
            row["DEPT_CODE"] = ddldepartment.SelectedValue.ToString();
            row["MACHINE_GROUP"] = ddlmaccode.SelectedValue.ToString();
            row["MST_CODE"] = ddlmainprocess.SelectedValue.ToString();

            myDataTable.Rows.Add(row);
            Session["Proceereport"] = myDataTable;
            Response.Redirect("~/Module/Machine/Reports/ProcessMasterQuery.aspx", false);
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }         
    }
    protected void GridProcessMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridProcessMaster.PageIndex = e.NewPageIndex;
        GridProcesss();
        
    }
}

