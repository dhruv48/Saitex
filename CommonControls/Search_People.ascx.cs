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

public partial class CommonControls_Search_People : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Prloblem in page loading.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            BindDept();
        }
        catch
        {
            throw;
        }
    }

    private void BindDept()
    {
        try
        {
            DataTable dtDept = SaitexBL.Interface.Method.CM_DEPT_MST.Select();

            ddldept.Items.Clear();
            ddldept.Items.Add(new ListItem("Select", "Select"));

            ddldept.DataSource = dtDept;
            ddldept.DataTextField = "DEPT_NAME";
            ddldept.DataValueField = "DEPT_CODE";
            ddldept.DataBind();

        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SearchEmployee();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Prloblem in Searching Employee.\r\nSee error log for detail."));
        }
    }

    private void SearchEmployee()
    {
        try
        {

            string Where_Query = string.Empty;

            if (txtEmpName.Text != string.Empty)
            {
                Where_Query += " and EMP_NAME like '%" + txtEmpName.Text.Trim() + "%' ";
            }

            if (ddldept.SelectedIndex != 0 && ddldept.SelectedValue != "Select")
            {
                Where_Query += " and DEPT_CODE = '" + ddldept.SelectedValue.Trim() + "' ";
            }

            DataTable dtSearchEmployee = SaitexBL.Interface.Method.HR_EMP_MST.GetSearchEmp(Where_Query);

            int iCount = 0;

            if (dtSearchEmployee != null)
            {
                iCount = dtSearchEmployee.Rows.Count;

                dlSearchEmp.DataSource = dtSearchEmployee;
                dlSearchEmp.DataBind();
            }
            lblRecordFound.Text = "Total " + iCount + " record found.";
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    protected void dlSearchEmp_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            Label lbldlEmpCode = e.Item.FindControl("lbldlEmpCode") as Label;
            Image imgEmp = e.Item.FindControl("imgEmp") as Image;

            imgEmp.ImageUrl = "~/Module/Admin/ShowImage.aspx?EMP_CODE=" + lbldlEmpCode.Text.Trim();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fetching Image.\r\nSee error log for detail."));
        }
    }
}
