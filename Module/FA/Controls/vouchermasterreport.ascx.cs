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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_vouchermasterreport : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string VCHR_CODE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
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
            BindVoucherType();
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetDataForVoucherType(string Text)
    {
        try
        {
            string whereClause = " WHERE VCHR_CODE like :SearchQuery OR VCHR_NAME LIKE :SearchQuery ";
            string sortExpression = " ORDER BY VCHR_CODE";
            string commandText = "SELECT * FROM V_FA_VCHR_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.FA_LGR_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", Text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private void BindVoucherType()
    {
        try
        {
            ddlVoucherType.Items.Clear();
            DataTable data = new DataTable();
            data = GetDataForVoucherType("");
            if (data != null && data.Rows.Count > 0)
            {
                ddlVoucherType.DataSource = data;
                ddlVoucherType.DataTextField = "VCHR_NAME";
                ddlVoucherType.DataValueField = "VCHR_CODE";
                ddlVoucherType.DataBind();
                ddlVoucherType.Items.Insert(0, new ListItem(" Print All Vouchers ", ""));
            }
            else
            {
                Common.CommonFuction.ShowMessage("There is no Voucher found in the DataBase..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "VCHR_CODE";
            myDataTable.Columns.Add(myDataColumn);
            DataRow row;
            row = myDataTable.NewRow();
            row["VCHR_CODE"] = ddlVoucherType.SelectedValue.ToString();

            if (ddlVoucherType.SelectedValue.ToString() != null && ddlVoucherType.SelectedValue.ToString() != string.Empty)
            {
                row["VCHR_CODE"] = ddlVoucherType.SelectedValue.ToString();
            }
            else
            {
                row["VCHR_CODE"] = string.Empty;
            }

            myDataTable.Rows.Add(row);
            Session["voucherreport"] = myDataTable;

            string URL = "./Voucher_Mst_Rpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./vouchermasterreport.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}
