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
using errorLog;
using Common;
using DBLibrary;

public partial class Module_HRMS_Controls_Attn_Mark_All : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                if (Convert.ToString(oUserLoginDetail.OPEN_MONTH) != null && Convert.ToString(oUserLoginDetail.OPEN_YEAR) != null)
                {
                    TxtMonth.Text = oUserLoginDetail.OPEN_MONTH.ToString();
                    TxtYear.Text = oUserLoginDetail.OPEN_YEAR.ToString();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("No Open Year Or open Month,Please Open Year or Month");
                }
                bindddlBrachName();
                getEmployeeDepartment();
                Bind_Grid_Record();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
        }
    }
    private void getEmployeeDepartment()
    {
        try
        {
            ////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("--------------All--------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    //private void getEmployeeDepartment()
    //{
    //    try
    //    {
    //        ////////////////////////// Bind Branch Name//////////////////////////////////////
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
    //        DataView DV = new DataView(dt);
    //        if (oUserLoginDetail.VC_DEPARTMENTNAME.Trim().ToString().ToUpper() != "ADMIN")
    //        {
    //            DV.RowFilter = "DEPT_CODE='" + oUserLoginDetail.VC_DEPARTMENTCODE.Trim().ToString() + "'";
    //            ddlDepartment.DataSource = DV;
    //        }
    //        else
    //        {
    //            ddlDepartment.DataSource = dt;
    //        }
    //        ddlDepartment.DataValueField = "DEPT_CODE";
    //        ddlDepartment.DataTextField = "DEPT_NAME";
    //        ddlDepartment.DataBind();
    //        if (oUserLoginDetail.VC_DEPARTMENTNAME.Trim().ToString().ToUpper() != "ADMIN")
    //        {
    //            ddlDepartment.Items.Insert(0, new ListItem("-------------Select------------", ""));
    //        }
    //        else
    //        {
    //            ddlDepartment.Items.Insert(0, new ListItem("--------------All--------------", ""));
    //        }
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    private void bindddlBrachName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("-------------Select------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch
        {
            throw;
        }
    }
    protected void btnSalaryGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            bool Result = false;
            System.Threading.Thread.Sleep(2000);
            Result = SaitexBL.Interface.Method.HR_SAL_SLIP_MST.Mark_Attendance_Record(DateTime.Parse(oUserLoginDetail.SALARY_FROMDATE), DateTime.Parse(oUserLoginDetail.SALARY_TODATE));
            ModalProgress.Hide();
            if (Result)
            {
                Common.CommonFuction.ShowMessage("Attendance Mark for the Month " + TxtMonth.Text.ToString() + " Sucessfully");
                Bind_Grid_Record();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Attendance Mark for the Month " + TxtMonth.Text.ToString() + " Faild!Try Again");
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Attendance Saving.\r\nSee error log for detail."));
        }
    }
    protected void GVAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVAttendance.PageIndex = e.NewPageIndex;
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gridview Paging.\r\nplease see error log"));
        }
    }
    private void Bind_Grid_Record()
    {
        string StrQuery = string.Empty;
        try
        {
            string strCompanyCode = oUserLoginDetail.COMP_CODE;
            StrQuery = " WHERE ltrim(rtrim(E.COMP_CODE))='" + strCompanyCode + "' AND ltrim(rtrim(E.DEL_STATUS))='0' AND ltrim(rtrim(A.SAL_MONTH))=LPAD('" + oUserLoginDetail.OPEN_MONTH_NO.ToString() + "',2,0) AND ltrim(rtrim(A.SAL_YEAR))='" + oUserLoginDetail.OPEN_YEAR.ToString() + "'";
            if (ddlBranch.SelectedValue.Trim() != "" && ddlBranch.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(E.branch_code))='" + ddlBranch.SelectedValue.Trim() + "'";
            }
            if (ddlDepartment.SelectedValue.Trim() != "" && ddlDepartment.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(E.DEPT_CODE))='" + ddlDepartment.SelectedValue.Trim() + "'";
            }
            DataTable dt = SaitexBL.Interface.Method.HR_SAL_SLIP_MST.Load_Attendance_Record(StrQuery, string.Empty, "0");
            GVAttendance.DataSource = dt;
            GVAttendance.DataBind();
        }
        catch
        {
            throw;
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlBranch.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
}
