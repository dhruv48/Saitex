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
using System.IO;
using DBLibrary;


public partial class Module_HRMS_Controls_Attendance_New : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable DTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DDLMonth.Attributes.Add("onChange", "ShowModalPopup('pload');");
            if (!IsPostBack)
            {
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
            Bind_Shift();
            Bind_BrachName();
            Load_Department();
            BindDesignation();
            fillYear();
            Create_Data_Table();
            DDLShift.SelectedIndex = 0;
            if (oUserLoginDetail.OPEN_MONTH_NO != null && oUserLoginDetail.OPEN_MONTH_NO.ToString() != "")
            {
                DDLMonth.SelectedValue = oUserLoginDetail.OPEN_MONTH_NO.ToString();
            }
            // Load_Attendance_Records();
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
            DDLDesigination.Items.Insert(0, new ListItem("--------SELECT--------", ""));
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
            DDLShift.Items.Insert(0, new ListItem("--------SELECT--------", ""));
            DDLShift.DataBind();
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
            DDLBranch.Items.Insert(0, new ListItem("--------SELECT--------", ""));
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
            DDLDepartment.Items.Insert(0, new ListItem("--------SELECT--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void fillYear()
    {
        try
        {
            for (int i = -1; i < 2; i++)
            {
                DDLYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
            }
            DDLYear.Items.Insert(0, new ListItem("--------SELECT--------", ""));
            DDLYear.SelectedValue = oUserLoginDetail.OPEN_YEAR;
        }
        catch
        {
            throw;
        }
    }
    protected void ddlEmployee_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            ddlEmployee.Items.Clear();
            ddlEmployee.DataSource = data;
            ddlEmployee.DataBind();
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
    private void Create_Data_Table()
    {
        try
        {
            DTable = new DataTable();
            DTable.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));
            //DTable.Columns.Add(new DataColumn("ATTN_ID", typeof(string)));
            DTable.Columns.Add(new DataColumn("ATTN_DATE", typeof(string)));
            DTable.Columns.Add(new DataColumn("CARD_NO", typeof(string)));
            DTable.Columns.Add(new DataColumn("SFT_ID", typeof(int)));
            DTable.Columns.Add(new DataColumn("ENTRY_TYPE", typeof(string)));
            DTable.Columns.Add(new DataColumn("IN_TIME", typeof(string)));
            DTable.Columns.Add(new DataColumn("OUT_TIME", typeof(string)));
            //DTable.Columns.Add(new DataColumn("OT", typeof(string)));
            //DTable.Columns.Add(new DataColumn("EARLYT", typeof(string)));
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
        int sftid = 0;
        try
        {
            if (DDLShift.SelectedIndex == 0)
            {
                sftid = 6;
            }
            else
            {
                sftid = int.Parse(DDLShift.SelectedValue.Trim().ToString());
            }

            SearchQuery = "WHERE LTRIM(RTRIM(E.DEL_STATUS))='0' and LTRIM(RTRIM(E.STATUS))='A' ";
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
            if (ddlEmployee.SelectedValue.ToString() != null && ddlEmployee.SelectedValue.ToString() != string.Empty)
            {
                SearchQuery = SearchQuery + " AND LTRIM(RTRIM(E.EMP_CODE))='" + ddlEmployee.SelectedValue.Trim().ToString() + "'";
            }

            SaitexDM.Common.DataModel.HR_ATTN_TRN oHR_ATTN_TRN = new SaitexDM.Common.DataModel.HR_ATTN_TRN();
            DataTable DT = new DataTable();
            DT = SaitexBL.Interface.Method.HR_ATTN_TRN.Get_Employee_Record(SearchQuery, sftid);

            if (DT.Rows.Count > 0)
            {
                LblInTime.Text = DT.Rows[0]["SFT_IN_TIME"].ToString();
                LblOutTime.Text = DT.Rows[0]["SFT_OUT_TIME"].ToString();
            }
            gvAttendanceRegister.DataSource = DT;
            gvAttendanceRegister.DataBind();
            tdClear.Visible = true;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Dyas_In_Month()
    {
        string Branch = string.Empty;
        try
        {

            int MDays = System.DateTime.DaysInMonth(int.Parse(DDLYear.SelectedValue.Trim().ToString()), int.Parse(DDLMonth.SelectedValue.Trim().ToString()));
            DateTime start = new DateTime(int.Parse(DDLYear.SelectedValue.Trim().ToString()), int.Parse(DDLMonth.SelectedValue.Trim().ToString()), 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            if (DDLBranch.SelectedValue.ToString() != null && DDLBranch.SelectedValue.ToString() != string.Empty)
            {
                Branch = DDLBranch.SelectedValue.Trim().ToString();
            }
            else
            {
                Branch = oUserLoginDetail.CH_BRANCHCODE.ToString();
            }
            System.Threading.Thread.Sleep(1000);
            bool res = SaitexBL.Interface.Method.HR_ATTN_TRN.Mark_Attendance(start.Date, end.Date, oUserLoginDetail.COMP_CODE, Branch);
            modalPopup.Hide();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private bool Mark_Attendance(DateTime sdate, DateTime edate)
    {
        string Branch = string.Empty;
        try
        {
            if (DDLBranch.SelectedValue.ToString() != null && DDLBranch.SelectedValue.ToString() != string.Empty)
            {
                Branch = DDLBranch.SelectedValue.Trim().ToString();
            }
            else
            {
                Branch = oUserLoginDetail.CH_BRANCHCODE.ToString();
            }
            bool res = SaitexBL.Interface.Method.HR_ATTN_TRN.Mark_Attendance(sdate, edate, oUserLoginDetail.COMP_CODE, Branch);
            return res;
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
        int daysInMonth = 0;
        try
        {
            bool bResult = false;
            SaitexDM.Common.DataModel.HR_ATTN_TRN oHR_ATTN_TRN = new SaitexDM.Common.DataModel.HR_ATTN_TRN();
            foreach (GridViewRow rw in gvAttendanceRegister.Rows)
            {
                Label LblEmp_Code = (Label)rw.FindControl("lblEmployeeCode");
                Label LblCard_No = (Label)rw.FindControl("LblCardNO");
                Label lblSftiiId = (Label)rw.FindControl("lblSftiiId");
                GridView GVAttandance = (GridView)rw.FindControl("gvATTN");
                daysInMonth = System.DateTime.DaysInMonth(int.Parse(DDLYear.SelectedValue.Trim().ToString()), int.Parse(DDLMonth.SelectedValue.Trim().ToString()));
                foreach (GridViewRow rw1 in GVAttandance.Rows)
                {
                    DTable.Rows.Clear();
                    for (int i = 1; i <= daysInMonth; i++)
                    {
                        string InControl = "TIN" + i;
                        string OutControl = "TOUT" + i;
                        String InTime = string.Empty;
                        String OutTime = string.Empty;
                        string AttnDate = i + "/" + DDLMonth.SelectedValue.Trim().ToString() + "/" + DDLYear.SelectedValue.Trim().ToString();
                        Control controlToFindInTime = FindControlRecursive(GVAttandance, InControl);
                        if (controlToFindInTime != null)
                        {
                            TextBox TxtInTime = (TextBox)controlToFindInTime;
                            InTime = TxtInTime.Text.ToString();
                        }
                        Control controlToFindOutTime = FindControlRecursive(GVAttandance, OutControl);
                        if (controlToFindOutTime != null)
                        {
                            TextBox TxtOutTime = (TextBox)controlToFindOutTime;
                            OutTime = TxtOutTime.Text.ToString();
                        }
                        oHR_ATTN_TRN.IN_TIME = InTime;
                        oHR_ATTN_TRN.OUT_TIME = OutTime;
                        if (oHR_ATTN_TRN.IN_TIME == "WO" || oHR_ATTN_TRN.IN_TIME == "NH" || oHR_ATTN_TRN.IN_TIME == "CL" || oHR_ATTN_TRN.IN_TIME == "SL" || oHR_ATTN_TRN.IN_TIME == "EL" || oHR_ATTN_TRN.IN_TIME == "ML" || oHR_ATTN_TRN.IN_TIME == "CO")
                        {
                            oHR_ATTN_TRN.IN_TIME = null;
                            oHR_ATTN_TRN.OUT_TIME = null;
                        }
                        if (oHR_ATTN_TRN.IN_TIME != null && oHR_ATTN_TRN.IN_TIME != "" && oHR_ATTN_TRN.OUT_TIME != null && oHR_ATTN_TRN.OUT_TIME != "")
                        {
                            DataRow dr = DTable.NewRow();
                            dr["EMP_CODE"] = CommonFuction.funFixQuotes(LblEmp_Code.Text.Trim().ToString());
                            dr["ATTN_DATE"] = CommonFuction.funFixQuotes(AttnDate);
                            dr["SFT_ID"] = int.Parse(lblSftiiId.ToolTip.ToString());
                            dr["ENTRY_TYPE"] = "0";
                            dr["IN_TIME"] = oHR_ATTN_TRN.IN_TIME;
                            dr["CARD_NO"] = CommonFuction.funFixQuotes(LblCard_No.Text.Trim().ToString());
                            dr["OUT_TIME"] = oHR_ATTN_TRN.OUT_TIME;
                            dr["TUSER"] = Session["urLoginId"].ToString().Trim();
                            DTable.Rows.Add(dr);
                        }
                    }
                }
                if (DTable.Rows.Count > 0)
                {
                    bResult = SaitexBL.Interface.Method.HR_ATTN_TRN.InsertAttendance(DTable);
                }
            }
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
    private Control FindControlRecursive(Control ctlRoot, string sControlId)
    {
        if (ctlRoot.ID == sControlId)
        {
            return ctlRoot;
        }
        foreach (Control ctl in ctlRoot.Controls)
        {
            Control ctlFound = FindControlRecursive(ctl, sControlId);
            if (ctlFound != null)
            {
                return ctlFound;
            }
        }
        return null;

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

    protected void gvAttendanceRegister_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label LblEmp_Code = (Label)e.Row.FindControl("lblEmployeeCode");
                GridView gvAttendance = (GridView)e.Row.FindControl("gvATTN");
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "40px");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In GridView Row Data Bound"));
        }
    }
    protected void DDLMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_Attendance_Records();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Loading Records"));
        }
    }
    protected void gvAttendanceRegister_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SqlDataSource ctrl = e.Row.FindControl("sqlDsOrders") as SqlDataSource;
                if (ctrl != null && e.Row.DataItem != null)
                {
                    ctrl.SelectParameters["EMP_CODE"].DefaultValue = ((DataRowView)e.Row.DataItem)["EMP_CODE"].ToString();
                    ctrl.SelectParameters["AYEAR"].DefaultValue = DDLYear.SelectedValue.Trim().ToString();
                    ctrl.SelectParameters["AMONTH"].DefaultValue = DDLMonth.SelectedValue.Trim().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void ChkMark_CheckedChanged(object sender, EventArgs e)
    {
        int daysInMonth = 0;
        try
        {
            foreach (GridViewRow rw in gvAttendanceRegister.Rows)
            {
                GridView GVAttandance = (GridView)rw.FindControl("gvATTN");
                Control controlToFind = FindControlRecursive(GVAttandance, "ChkMark");
                daysInMonth = System.DateTime.DaysInMonth(int.Parse(DDLYear.SelectedValue.Trim().ToString()), int.Parse(DDLMonth.SelectedValue.Trim().ToString()));
                if (controlToFind != null)
                {
                    CheckBox ChkMark = (CheckBox)controlToFind;
                    if (ChkMark.Checked)
                    {
                        for (int i = 1; i <= daysInMonth; i++)
                        {
                            string InControl = "TIN" + i;
                            string OutControl = "TOUT" + i;
                            Control controlToFindInTime = FindControlRecursive(GVAttandance, InControl);
                            if (controlToFindInTime != null)
                            {
                                TextBox TxtInTime = (TextBox)controlToFindInTime;
                                TxtInTime.Text.ToString();
                                if (TxtInTime.Text.Trim().ToString() == "")
                                {
                                    TxtInTime.Text = LblInTime.Text.Trim().ToString();
                                }
                            }
                            Control controlToFindOutTime = FindControlRecursive(GVAttandance, OutControl);
                            if (controlToFindOutTime != null)
                            {
                                TextBox TxtOutTime = (TextBox)controlToFindOutTime;
                                if (TxtOutTime.Text.Trim().ToString() == "")
                                {
                                    TxtOutTime.Text = LblOutTime.Text.Trim().ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Loading Records"));
        }
    }
    protected void CmdCalCulate_Click(object sender, EventArgs e)
    {
        try
        {
            if (DDLMonth.SelectedIndex > 0 && DDLYear.SelectedIndex > 0)
            {
                Dyas_In_Month();
            }
            Load_Attendance_Records();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Loading Records"));
        }
    }
}
