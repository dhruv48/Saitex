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
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_Attendance : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable DTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                txtDate.Text = System.DateTime.Now.ToShortDateString();
                Load_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Page Loading"));
        }
    }
    private void Load_Control()
    {
        try
        {
            lblMode.Text = "Save";
            tdClear.Visible = true;
            lblTotal.Visible = false;
            lblTotalRecord.Visible = false;
            Bind_Shift();
            Bind_BrachName();
            Load_Department();
            BindDesignation();
            Bind_Employee();
            Create_Data_Table();
            DDLShift.SelectedIndex = 0;
            Load_Attendance_Records();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void BindDesignation()
    {
        try
        {
            //////////////////////////// Bind Degination Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            DDLDesigination.DataSource = dt;
            DDLDesigination.DataValueField = "desig_Code";
            DDLDesigination.DataTextField = "desig_Name";
            DDLDesigination.DataBind();
            DDLDesigination.Items.Insert(0, new ListItem("---------SELECT--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_Shift()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_SFT_MST oHR_SFT_MST = new SaitexDM.Common.DataModel.HR_SFT_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftName();
            DDLShift.DataSource = dt;
            DDLShift.DataValueField = "SFT_ID";
            DDLShift.DataTextField = "SFT_NAME";
            DDLShift.DataBind();
           
            DDLShift.Items.Insert(0, new ListItem("---------SELECT--------", ""));
            
            
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_BrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            DDLBranch.DataSource = dt;
            DDLBranch.DataValueField = "BRANCH_CODE";
            DDLBranch.DataTextField = "BRANCH_NAME";
            DDLBranch.DataBind();
            DDLBranch.Items.Insert(0, new ListItem("---------SELECT--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void Load_Department()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            DDLDepartment.DataSource = dt;
            DDLDepartment.DataValueField = "DEPT_CODE";
            DDLDepartment.DataTextField = "DEPT_NAME";
            DDLDepartment.DataBind();
            DDLDepartment.Items.Insert(0, new ListItem("---------SELECT--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void Bind_Employee()
    {
        try
        {
            DDLEmployee.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getempcode();

            if (dt != null && dt.Rows.Count > 0)
            {
                DDLEmployee.DataValueField = "EMP_CODE";
                DDLEmployee.DataTextField = "EMPLOYEENAME";
                DDLEmployee.DataSource = dt;
                DDLEmployee.DataBind();
            }
            DDLEmployee.Items.Insert(0, new ListItem("---------SELECT--------", string.Empty));
        }
        catch
        {
            throw;
        }
    }
    private void Create_Data_Table()
    {
        try
        {
            DTable = new DataTable();
            DTable.Columns.Add(new DataColumn("SFT_ID", typeof(int)));
            DTable.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));
            DTable.Columns.Add(new DataColumn("CARD_NO", typeof(string)));
            DTable.Columns.Add(new DataColumn("ATTN_DATE", typeof(string)));
            DTable.Columns.Add(new DataColumn("ENTRY_TYPE", typeof(string)));
            DTable.Columns.Add(new DataColumn("IN_TIME", typeof(string)));
            DTable.Columns.Add(new DataColumn("OUT_TIME", typeof(string)));
            DTable.Columns.Add(new DataColumn("TUSER", typeof(string)));

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Load_Attendance_Records()
    {
        string SearchQuery = string.Empty;
        try
        {
            if (DDLShift.SelectedIndex != -1)
            {
                SearchQuery = "WHERE LTRIM(RTRIM(E.DEL_STATUS))='0' and  LTRIM(RTRIM(E.STATUS))='A'";
                if (DDLDepartment.SelectedValue.ToString() != null && DDLDepartment.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(DE.DEPT_CODE))='" + DDLDepartment.SelectedValue.ToString() + "'";
                }
                if (DDLDesigination.SelectedValue.ToString() != null && DDLDesigination.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(D.DESIG_CODE))='" + DDLDesigination.SelectedValue.Trim().ToString() + "'";
                }
                if (DDLBranch.SelectedValue.ToString() != null && DDLBranch.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(E.BRANCH_CODE))='" + DDLBranch.SelectedValue.Trim().ToString() + "'";
                }
                if (DDLEmployee.SelectedValue.ToString() != null && DDLEmployee.SelectedValue.ToString() != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(E.EMP_CODE))='" + DDLEmployee.SelectedValue.Trim().ToString() + "'";
                }
                SaitexDM.Common.DataModel.HR_ATTN_TRN oHR_ATTN_TRN = new SaitexDM.Common.DataModel.HR_ATTN_TRN();
                DataTable DT = new DataTable();
                if (DDLShift.SelectedIndex > 0)
                {
                    DT = SaitexBL.Interface.Method.HR_ATTN_TRN.Attendance_Search(SearchQuery, int.Parse(DDLShift.SelectedValue.Trim().ToString()), DateTime.Parse(txtDate.Text.ToString()));
                    if (DT.Rows.Count > 0)
                    {
                        LblInTime.Text = DT.Rows[0]["SFT_IN_TIME"].ToString();
                        LblOutTime.Text = DT.Rows[0]["SFT_OUT_TIME"].ToString();
                    }
                }
                else
                {
                    DT = SaitexBL.Interface.Method.HR_ATTN_TRN.Attendance_Search(SearchQuery, DateTime.Parse(txtDate.Text.Trim().ToString()));
                    if (DT.Rows.Count > 0)
                    {
                        LblInTime.Text = DT.Rows[0]["SFT_IN_TIME"].ToString();
                        LblOutTime.Text = DT.Rows[0]["SFT_OUT_TIME"].ToString();
                    }
                }
                gvAttendanceRegister.DataSource = DT;
                lblTotalRecord.Text = DT.Rows.Count.ToString();
                gvAttendanceRegister.DataBind();
                tdClear.Visible = true;
                lblTotal.Visible = true;
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Insertdata()
    {
        string OT = string.Empty;
        string Early = string.Empty;
        try
        {
            DateTime Attn_Date = DateTime.Parse(txtDate.Text.Trim().ToString());
            SaitexDM.Common.DataModel.HR_ATTN_TRN oHR_ATTN_TRN = new SaitexDM.Common.DataModel.HR_ATTN_TRN();
            for (int i = 0; i < gvAttendanceRegister.Rows.Count; i++)
            {
                oHR_ATTN_TRN.IN_TIME = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("txtInTime")).Text);
                oHR_ATTN_TRN.OUT_TIME = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("txtOutTime")).Text);
                if (oHR_ATTN_TRN.IN_TIME != null && oHR_ATTN_TRN.IN_TIME != "" && oHR_ATTN_TRN.OUT_TIME != null && oHR_ATTN_TRN.OUT_TIME != "")
                {
                    DataRow dr = DTable.NewRow();
                    dr["SFT_ID"] = int.Parse(DDLShift.SelectedValue.ToString());
                    dr["EMP_CODE"] = (((Label)gvAttendanceRegister.Rows[i].FindControl("lblEmployeeCode")).Text);
                    dr["CARD_NO"] = (((Label)gvAttendanceRegister.Rows[i].FindControl("LblCardNo")).Text);
                    dr["ATTN_DATE"] = CommonFuction.funFixQuotes(txtDate.Text.Trim());
                    dr["ENTRY_TYPE"] = "0";
                    dr["IN_TIME"] = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("txtInTime")).Text);
                    dr["OUT_TIME"] = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("txtOutTime")).Text);
                    dr["TUSER"] = Session["urLoginId"].ToString().Trim();
                    DTable.Rows.Add(dr);
                }
            }
            bool bResult = SaitexBL.Interface.Method.HR_ATTN_TRN.InsertAttendance(DTable);
            if (bResult)
            {
                BlankControls();
                Load_Attendance_Records();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Attendance Marked Successfully');", true);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to save!please try again");
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void gvAttendanceRegister_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "InTime")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                TextBox txtInTime = (TextBox)row.FindControl("txtInTime");
                txtInTime.Enabled = false;

            }
            else if (e.CommandName == "OutTime")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                TextBox txtOutTime = (TextBox)row.FindControl("txtOutTime");
                TextBox txtInTime = (TextBox)row.FindControl("txtInTime");
                txtInTime.Enabled = true;

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Gridview Row Command"));
        }
    }
    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Inserting Data"));
        }
    }

    private void BlankControls()
    {
        try
        {
            gvAttendanceRegister.DataSource = null;
            gvAttendanceRegister.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BlankControls();
            lblMode.Text = "Save";
            tdClear.Visible = false;
            tdSave.Visible = true;
            lblTotalRecord.Visible = false;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "AttendanceMaster_OPT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=500,height=300');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Page Printing"));
        }
    }

    protected void gvAttendanceRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAttendanceRegister.PageIndex = e.NewPageIndex;
            Load_Attendance_Records();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Gridview Page Index Changes"));
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
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Load_Attendance_Records();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Date Changing"));
        }
    }
    protected void gvAttendanceRegister_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "40px");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In GridView Row Data Bound"));
        }
    }
    protected void CmdViewRecord_Click(object sender, EventArgs e)
    {
        try
        {
            Load_Attendance_Records();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Shift Selecting Index"));
        }

    }
}

