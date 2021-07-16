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

public partial class Module_Production_Controls_ProductionEntryReport : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string PROS_CODE = string.Empty;
    private static string ORDER_NO = string.Empty;
    private static string MACHINE_CODE = string.Empty;
    private static string DEPT_CODE = string.Empty;
    private static string LOT_NUMBER = string.Empty;
    private static string DYED_LOT_NO = string.Empty;
    private static string SFT_ID = string.Empty;
    private static string FromDate;
    private static string ToDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "You are in Print Mode";
                txtformdate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                txtTodate.Text = System.DateTime.Now.ToShortDateString();
                bindddlshiftid();
                bindddlProcessno();
                bindddlOrderNO();
                bindddlMachinCode();
                BindddlDyedLotNo();
                bindddlDeptCode();
                bindddlLotNo();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Load.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void bindddlshiftid()
    {
        try
        {
            ddlshift.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftName();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlshift.DataValueField = "SFT_ID";
                ddlshift.DataTextField = "SFT_NAME";
                ddlshift.DataSource = dt;
                ddlshift.DataBind();
            }
            ddlshift.Items.Insert(0, new ListItem("Select", ""));
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

    private void bindddlOrderNO()
    {
        try
        {
            ddlOrderNo.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MAC_PROC_MST.Get_OrderNo();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlOrderNo.DataValueField = "ORDER_NO";
                ddlOrderNo.DataTextField = "ORDER_NO";
                ddlOrderNo.DataSource = dt;
                ddlOrderNo.DataBind();
            }
            ddlOrderNo.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }

    private void bindddlMachinCode()
    {
        try
        {
            ddlMachin.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineCode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlMachin.DataValueField = "MACHINE_CODE";
                ddlMachin.DataTextField = "MACHINE_CODE";
                ddlMachin.DataSource = dt;
                ddlMachin.DataBind();
            }
            ddlMachin.Items.Insert(0, new ListItem("Select", ""));
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
            ddldept.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldept.DataTextField = "DEPT_NAME";
                ddldept.DataValueField = "DEPT_CODE";
                ddldept.DataSource = dt;
                ddldept.DataBind();

            }
            ddldept.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }

    private void bindddlLotNo()
    {
        try
        {
            ddllotno.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetLotNumber();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddllotno.DataValueField = "LOT_NUMBER";
                ddllotno.DataTextField = "LOT_NUMBER";
                ddllotno.DataSource = dt;
                ddllotno.DataBind();
            }
            ddllotno.Items.Insert(0, new ListItem("Select", ""));
        }
        catch
        {
            throw;
        }
    }

    private void BindddlDyedLotNo()
    {
        try
        {
            ddldyedlotno.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDyedlotno();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldyedlotno.DataTextField = "DYED_LOT_NO";
                ddldyedlotno.DataValueField = "DYED_LOT_NO";
                ddldyedlotno.DataSource = dt;
                ddldyedlotno.DataBind();
            }
            ddldyedlotno.Items.Insert(0, new ListItem("Select", ""));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            myDataColumn.ColumnName = "MACHINE_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ORDER_NO";
            myDataTable.Columns.Add(myDataColumn);

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
            myDataColumn.ColumnName = "LOT_NUMBER";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DYED_LOT_NO";

            myDataTable.Columns.Add(myDataColumn);
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "SFT_ID";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "FromDate";

            myDataTable.Columns.Add(myDataColumn);
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ToDate";
            myDataTable.Columns.Add(myDataColumn);

            DataRow row;
            row = myDataTable.NewRow();
            row["MACHINE_CODE"] = ddlMachin.SelectedValue.ToString();
            row["ORDER_NO"] = ddlOrderNo.SelectedValue.ToString();
            row["PROS_CODE"] = ddlprocessno.SelectedValue.ToString();
            row["DEPT_CODE"] = ddldept.SelectedValue.ToString();
            row["LOT_NUMBER"] = ddllotno.SelectedValue.ToString();
            row["DYED_LOT_NO"] = ddldyedlotno.SelectedValue.ToString();
            row["SFT_ID"] = ddlshift.SelectedValue.ToString();

            if (txtformdate.Text.ToString() != null && txtformdate.Text.ToString() != string.Empty)
            {
                row["FromDate"] = txtformdate.Text.ToString();
            }
            else
            {
                row["FromDate"] = oUserLoginDetail.DT_STARTDATE.ToShortDateString(); ;
            }

            if (txtTodate.Text.ToString() != null && txtTodate.Text.ToString() != string.Empty)
            {
                row["ToDate"] = txtTodate.Text.ToString();
            }
            else
            {
                row["ToDate"] = System.DateTime.Now.Date.ToShortDateString();
            }

            myDataTable.Rows.Add(row);
            Session["Proceereport"] = myDataTable;

            string URL = "../Report/ProductEntryReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in Printing Records..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtformdate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtformdate.Text) < Convert.ToDateTime(txtTodate.Text))
                {

                }
                else
                {
                    CommonFuction.ShowMessage("To Date should not be greater than From Date..");
                    txtTodate.Text = string.Empty;
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please enter From Date first..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in To Date Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./ProductionEntryOPT.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}
