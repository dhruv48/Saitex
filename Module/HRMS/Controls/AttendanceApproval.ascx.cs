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
using Common;
using errorLog;
using AmountToWords;


public partial class Module_HRMS_Controls_AttendanceApproval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string EMP_CODE = string.Empty;
    private static string DEPT_CODE = string.Empty;
    private static string DESIG_CODE = string.Empty;
    private static string BRANCH_CODE = string.Empty;
    private static DataTable DTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Initial_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nplease see error log"));
        }
    }
    private void Initial_Control()
    {
        try
        {
            EMP_CODE = string.Empty;
            DEPT_CODE = string.Empty;
            DESIG_CODE = string.Empty;
            BRANCH_CODE = string.Empty;
            Create_Data_Table();
            txtDate.Text = String.Format("{0:dd/MM/yyyy}", System.DateTime.Now);
            bindDDLDepartment();
            BindDesignation();
            Bind_BrachName();
            Bind_Shift();
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
            DTable.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));
            DTable.Columns.Add(new DataColumn("ATTN_ID", typeof(string)));
            DTable.Columns.Add(new DataColumn("ATTN_DATE", typeof(string)));
            DTable.Columns.Add(new DataColumn("SFT_ID", typeof(int)));
            DTable.Columns.Add(new DataColumn("ENTRY_TYPE", typeof(string)));
            DTable.Columns.Add(new DataColumn("IN_TIME", typeof(string)));
            DTable.Columns.Add(new DataColumn("OUT_TIME", typeof(string)));
            DTable.Columns.Add(new DataColumn("OT", typeof(string)));
            DTable.Columns.Add(new DataColumn("EARLYT", typeof(string)));

            DTable.Columns.Add(new DataColumn("APPROVED_BY", typeof(string)));
            DTable.Columns.Add(new DataColumn("APPROVE_STATUS", typeof(string)));
            DTable.Columns.Add(new DataColumn("APPROVE_COMMENT", typeof(string)));

            DTable.Columns.Add(new DataColumn("TUSER", typeof(string)));

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
            DDLShift.Items.Insert(0, new ListItem("--------SELECT--------", ""));
            DDLShift.DataBind();
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
                SearchQuery = "WHERE LTRIM(RTRIM(E.DEL_STATUS))='0'  AND A.ATTN_STATUS = 'P' ";
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
                DT = SaitexBL.Interface.Method.HR_ATTN_TRN.Attendance_Search(SearchQuery, int.Parse(DDLShift.SelectedValue.Trim().ToString()), DateTime.Parse(txtDate.Text.Trim().ToString()));

                if (DT.Rows.Count > 0)
                {
                    LblInTime.Text = DT.Rows[0]["SFT_IN_TIME"].ToString();
                    LblOutTime.Text = DT.Rows[0]["SFT_OUT_TIME"].ToString();
                }
                gvAttendanceRegister.DataSource = DT;
                gvAttendanceRegister.DataBind();
                tdClear.Visible = true;
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
                oHR_ATTN_TRN.IN_TIME = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("TxtIntime")).Text);
                oHR_ATTN_TRN.OUT_TIME = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("TxtOutTime")).Text);
                CheckBox ChkMark = (CheckBox)gvAttendanceRegister.Rows[i].FindControl("ChkMark");
                if (ChkMark.Checked)
                {
                    if (oHR_ATTN_TRN.IN_TIME != null && oHR_ATTN_TRN.IN_TIME != "" && oHR_ATTN_TRN.OUT_TIME != null && oHR_ATTN_TRN.OUT_TIME != "")
                    {
                        DataRow dr = DTable.NewRow();
                        dr["EMP_CODE"] = (((Label)gvAttendanceRegister.Rows[i].FindControl("lblempcode")).Text);
                        dr["ATTN_DATE"] = CommonFuction.funFixQuotes(txtDate.Text.Trim());
                        dr["SFT_ID"] = int.Parse(DDLShift.SelectedValue.ToString());

                        dr["ENTRY_TYPE"] = "0";
                        dr["IN_TIME"] = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("TxtIntime")).Text);
                        dr["OUT_TIME"] = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("TxtOutTime")).Text);
                        dr["APPROVED_BY"] = CommonFuction.funFixQuotes(((Label)gvAttendanceRegister.Rows[i].FindControl("LblApprovedBy")).Text);
                        dr["APPROVE_STATUS"] = "A";
                        dr["APPROVE_COMMENT"] = CommonFuction.funFixQuotes(((TextBox)gvAttendanceRegister.Rows[i].FindControl("txtUserComment")).Text);
                        if (oHR_ATTN_TRN.OUT_TIME != string.Empty)
                        {
                            TimeSpan TShiftOut = System.TimeSpan.Parse(LblOutTime.Text.Trim().ToString());
                            TimeSpan TOutTime = System.TimeSpan.Parse(oHR_ATTN_TRN.OUT_TIME);
                            if (TShiftOut > TOutTime)
                            {
                                Early = (TShiftOut - TOutTime).ToString();
                                OT = string.Empty;
                            }
                            else if (TShiftOut < TOutTime)
                            {
                                OT = (TOutTime - TShiftOut).ToString();
                                Early = string.Empty;
                            }
                        }
                        dr["OT"] = OT;
                        dr["EARLYT"] = Early;
                        dr["TUSER"] = Session["urLoginId"].ToString().Trim();
                        DTable.Rows.Add(dr);
                    }
                }
            }
            bool bResult = SaitexBL.Interface.Method.HR_ATTN_TRN.InsertAttendance(DTable);
            if (bResult)
            {
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
                TextBox TxtIntime = (TextBox)row.FindControl("TxtIntime");
                TxtIntime.Enabled = false;

            }
            else if (e.CommandName == "OutTime")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                TextBox TxtOutTime = (TextBox)row.FindControl("TxtOutTime");
                TextBox TxtIntime = (TextBox)row.FindControl("TxtIntime");
                TxtIntime.Enabled = true;

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

    protected void DDLEmployee_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            DDLEmployee.Items.Clear();
            DDLEmployee.DataSource = data;
            DDLEmployee.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string sPO = "";

            string commandText = "SELECT * FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause = " and emp_code not in(SELECT emp_code FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' AND ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " ORDER BY EMP_CODE";
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, SortExpression, "", text.ToUpper() + '%', oUserLoginDetail.COMP_CODE.ToString(), string.Empty, sPO);
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT count(*) FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS FROM HR_EMP_MST WHERE EMP_CODE LIKE :SearchQuery OR F_NAME LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' ";
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text.ToUpper() + '%', oUserLoginDetail.COMP_CODE.ToString(), string.Empty, "");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void bindDDLDepartment()
    {
        try
        {
            DDLDepartment.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            if (dt != null && dt.Rows.Count > 0)
            {
                DDLDepartment.DataValueField = "DEPT_CODE";
                DDLDepartment.DataTextField = "DEPT_NAME";
                DDLDepartment.DataSource = dt;
                DDLDepartment.DataBind();
            }
            DDLDepartment.Items.Insert(0, new ListItem("------Select------", string.Empty));
        }
        catch
        {
            throw;
        }
    }
    private void BindDesignation()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            DDLDesigination.DataSource = dt;
            DDLDesigination.DataValueField = "DESIG_CODE";
            DDLDesigination.DataTextField = "DESIG_NAME";
            DDLDesigination.DataBind();
            DDLDesigination.Items.Insert(0, new ListItem("------SELECT------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
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
            DDLBranch.Items.Insert(0, new ListItem("------SELECT------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void Clear_Control()
    {
        try
        {
            Initial_Control();
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
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
                Response.Redirect("~/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Help file open.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear_Control();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Record Clearing.\r\nSee error log for detail."));
        }
    }
    protected void CmdView_Click(object sender, EventArgs e)
    {
        try
        {
            Load_Attendance_Records();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
}
