using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DBLibrary;
using Common;
using errorLog;

public partial class Module_HRMS_Pages_STAFF_LEAVE : System.Web.UI.Page
{

    SaitexDM.Common.DataModel.HR_LV_APP_FORM_DTL HR_LV_APP_FORM_DTL;
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST ;
    SaitexDM.Common.DataModel.HR_LV_APP_DTL HR_LV_APP_DTL;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string POSITION = string.Empty;
    private static string ReportTo = string.Empty;
    private static decimal RequireDays;
    private static decimal sResult = 0;
    private static int JOINDAY;
    private static string EMPLOYEE_CODE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            HR_LV_APP_FORM_DTL = new SaitexDM.Common.DataModel.HR_LV_APP_FORM_DTL();
            HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
            HR_LV_APP_DTL = new SaitexDM.Common.DataModel.HR_LV_APP_DTL();
            if (!IsPostBack)
            {             
                Fill_EmpData_InForm();
                Load_Leave();
                txtCurrentDate.Text = System.DateTime.Today.ToString("dd/MM/yyyy").Trim();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page loading"));
        }
    }

    private void getLeaveRecord()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_LV_APP.GetEmpLeaveRecord(EMPLOYEE_CODE.ToString().Trim());
            gvLeaveAssign.DataSource = DTable;
            gvLeaveAssign.DataBind();
            if (DTable.Rows.Count > 0)
            {
                gvLeaveAssign.Visible = true;
            }
            else
            {
                gvLeaveAssign.Visible = false;
            }
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
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
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
            string commandText = "SELECT * FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS,STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0'and STATUS='A' AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause = " and emp_code not in(SELECT emp_code FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS,STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE and STATUS='A' AND DEL_STATUS = '0' AND ROWNUM <= '" + startOffset + "')";
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
            string CommandText = "SELECT count(*) FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS,STATUS FROM HR_EMP_MST WHERE EMP_CODE LIKE :SearchQuery OR F_NAME LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE and STATUS='A' AND DEL_STATUS = '0' ";
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text.ToUpper() + '%', oUserLoginDetail.COMP_CODE.ToString(), string.Empty, "");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            EMPLOYEE_CODE = string.Empty;
            EMPLOYEE_CODE = ddlEmployee.SelectedValue.Trim().ToString();
            HR_EMP_MST.EMP_CODE = EMPLOYEE_CODE.ToString();
            Fill_EmpData_InForm();
            getLeaveRecord();
            Load_Leave();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void Fill_EmpData_InForm()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_LV_APP.Employee_Info(HR_EMP_MST);
            if (DT.Rows.Count > 0)
            {
                txtEmpName.Text = DT.Rows[0]["EMPLOYEENAME"].ToString().Trim();
                DateTime JoinDate = DateTime.Parse(DT.Rows[0]["JOINDATE"].ToString().Trim());
                TimeSpan JOINDAYs = System.DateTime.Today.Subtract(JoinDate);
                JOINDAY = int.Parse(JOINDAYs.TotalDays.ToString());
            }
        }
        catch
        {
            throw;
        }
    }
    protected void chk_LeaveType_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow Row in gvLeaveAssign.Rows)
            {
                CheckBox Chk = (CheckBox)Row.FindControl("chk_LeaveType"); ;
                if (Chk.Checked)
                {
                    Label lblLeaveMasterId = (Label)Row.FindControl("lblLeaveMasterId");
                    int LeaveMasterId = int.Parse(lblLeaveMasterId.Text.Trim());
                    Leave_Mapping(LeaveMasterId);
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Leave Mapping"));
        }

    }

    private void Leave_Mapping(int LeaveId)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_LV_APP.Leave_Map_Info(LeaveId);
            foreach (DataRow DRow in DTable.Rows)
            {
                int iLEAVEMASTERID = int.Parse(DRow["MAP_ID"].ToString());
                foreach (GridViewRow Row in gvLeaveAssign.Rows)
                {
                    Label lblLeaveMasterId = (Label)Row.FindControl("lblLeaveMasterId");
                    CheckBox chk_LeaveType = (CheckBox)Row.FindControl("chk_LeaveType");
                    TextBox txtReqDay = (TextBox)Row.FindControl("txtReqDay");
                    TextBox txtRemainDay = (TextBox)Row.FindControl("txtRemain");
                    TextBox FromDate = (TextBox)Row.FindControl("txtDuration_From");
                    TextBox ToDate = (TextBox)Row.FindControl("txtDuration_To");
                    int LeaveMasterId = int.Parse(lblLeaveMasterId.Text.Trim());
                    if (LeaveMasterId == iLEAVEMASTERID)
                    {
                        chk_LeaveType.Checked = false;
                        txtReqDay.Text = string.Empty;
                        txtRemainDay.Text = string.Empty;
                        FromDate.ReadOnly = true;
                        ToDate.ReadOnly = true;
                    }
                    else if (FromDate.ReadOnly != true)
                    {
                        txtReqDay.Text = string.Empty;
                        txtRemainDay.Text = string.Empty;
                        ToDate.ReadOnly = false;
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void txtDuration_To_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal iRemain = 0;
            decimal ReqDays = 0;
            decimal Remain = 0;
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
            TextBox FromDate = (TextBox)grdRow.FindControl("txtDuration_From");
            TextBox ToDate = (TextBox)grdRow.FindControl("txtDuration_To");
            TextBox txtLeaveDaysGD = (TextBox)grdRow.FindControl("TxtRemaining");
            TextBox txtReqDay = (TextBox)grdRow.FindControl("txtReqDay");
            TextBox txtRemain = (TextBox)grdRow.FindControl("txtRemain");
            TextBox txtPending = (TextBox)grdRow.FindControl("txtLeavePending");
            DropDownList DDLType = (DropDownList)grdRow.FindControl("DDLLeaveReq");
            if (DDLType.SelectedValue == "FH" || DDLType.SelectedValue == "SH")
            {
                txtReqDay.Text = "0.5";
                iRemain = decimal.Parse(txtLeaveDaysGD.Text.ToString()) - decimal.Parse(txtReqDay.Text.ToString());
                txtRemain.Text = iRemain.ToString();
            }
            else
            {
                TimeSpan tDAYS = DateTime.Parse(ToDate.Text.ToString()).Subtract(DateTime.Parse(FromDate.Text.ToString()));
                ReqDays = tDAYS.Days + 1;
                Remain = decimal.Parse(txtLeaveDaysGD.Text.ToString());
                RequireDays = Remain;
                txtReqDay.Text = ReqDays.ToString();
                if (ReqDays > Remain)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('You don't have leave remaining,You Can Apply for Only " + ReqDays + " days');", true);
                    FromDate.Text = string.Empty;
                    ToDate.Text = string.Empty;
                    txtReqDay.Text = string.Empty;
                    txtRemain.Text = string.Empty;
                }
                else
                {
                    iRemain = decimal.Parse(txtLeaveDaysGD.Text.ToString()) - decimal.Parse(txtReqDay.Text.ToString());
                    txtRemain.Text = iRemain.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in textChange"));
        }
    }

    private bool Can_Save(DateTime FromDate, DateTime ToDate)
    {
        bool Result = false;
        try
        {

            //HR_LV_APP_FORM_DTL.EMP_CODE = Session["EmpCode"].ToString();
            if (ddlEmployee.SelectedValue.ToString() != "" && ddlEmployee.SelectedValue.ToString() != null)
            {
                HR_LV_APP_FORM_DTL.EMP_CODE = ddlEmployee.SelectedValue.ToString();
            }
            HR_LV_APP_FORM_DTL.LV_FROM_DATE = FromDate;
            HR_LV_APP_FORM_DTL.LV_TO_DATE = ToDate;
            TimeSpan diff = ToDate.Subtract(FromDate);
            decimal days = Convert.ToDecimal(diff.TotalDays);
            if (days > RequireDays)
            {
                Common.CommonFuction.ShowMessage("Requre days greater then remaining days!Plz Check From date & To Date");
                Result = false;
            }
            bool Res = SaitexBL.Interface.Method.HR_LV_APP.Can_Save_Leave_Form(HR_LV_APP_FORM_DTL);
            if (Res)
            {
                Result = true;
            }
            else
            {
                Result = false;
            }
            return Result;

        }
        catch
        {
            throw;
        }
    }

    private decimal SaveRecord(int LV_ID, DateTime FromDate, DateTime ToDate, string IsHalfday)
    {
        decimal number = 0;
        try
        {
            TimeSpan ts = ToDate - FromDate;
            int TotalDays = ts.Days + 1;
            decimal ReqD = 0;
            decimal ReqDay = 0;
            ////////////////////////////// Code to getting Max Id of HR_LV_APP_FORM_DTL ///////////////////////////////////////////
            int NewMaxLv_Id = SaitexBL.Interface.Method.HR_LV_APP.Get_Leave_App_Form_Id();
            ///////////////////////////////////////  Code to insert the data /////////////////////////////////
            HR_LV_APP_FORM_DTL.LV_APP_ID = NewMaxLv_Id;
            HR_LV_APP_FORM_DTL.EMP_CODE = ddlEmployee.SelectedValue.Trim().ToString();
           // HR_LV_APP_FORM_DTL.EMP_CODE = Session["EmpCode"].ToString();
            HR_LV_APP_FORM_DTL.LV_APPROVED_BY = "25";
            HR_LV_APP_FORM_DTL.LV_ID = LV_ID;
            HR_LV_APP_FORM_DTL.LV_FROM_DATE = FromDate;
            HR_LV_APP_FORM_DTL.LV_TO_DATE = ToDate;
            if (IsHalfday == "1H" || IsHalfday == "2H")
            {
                ReqD = decimal.Parse("0.5");
                HR_LV_APP_FORM_DTL.LV_TOTAL_DAYS = ReqD;
                ReqDay = ReqD;
            }
            else
            {
                HR_LV_APP_FORM_DTL.LV_TOTAL_DAYS = TotalDays;
                ReqD = TotalDays;
                ReqDay = TotalDays;
            }
            HR_LV_APP_FORM_DTL.LV_PURPOSE = txtPurpose.Text.Trim().ToString();
            HR_LV_APP_FORM_DTL.TDATE = DateTime.Now;
            bool Res = SaitexBL.Interface.Method.HR_LV_APP.Save_Leave_Form(HR_LV_APP_FORM_DTL);
            if (Res)
            {

                DateTime Lv_APP_DATE_1 = FromDate;
                while (number < ReqDay)
                {
                    decimal lv_Days_1 = 1;
                    HR_LV_APP_DTL.LV_APP_FORM_ID = NewMaxLv_Id;
                    HR_LV_APP_DTL.EMP_CODE = ddlEmployee.SelectedValue.ToString();                  
                    HR_LV_APP_DTL.LV_APP_DATE = Lv_APP_DATE_1;
                    HR_LV_APP_DTL.LV_TYPE = LV_ID;
                    HR_LV_APP_DTL.LV_DAY_DTL = IsHalfday;
                    if (ReqD >= 0)
                    {
                        if (IsHalfday == "1H" || IsHalfday == "2H")
                        {
                            lv_Days_1 = decimal.Parse("0.5");
                        }
                        else if (IsHalfday == "FD" && ReqD > 1)
                        {
                            lv_Days_1 = 1;
                        }
                    }
                    HR_LV_APP_DTL.DAYS_LV = lv_Days_1.ToString();
                    HR_LV_APP_DTL.LV_APP_CANCEL = '0';
                    HR_LV_APP_DTL.SUP_ADMIN_LOCK = '0';
                    HR_LV_APP_DTL.LV_ADMINLOCKING = '0';
                    bool rESULTE = SaitexBL.Interface.Method.HR_LV_APP.Save_Leave_AppDetail(HR_LV_APP_DTL);
                    if (rESULTE)
                    {
                        number = number + 1;
                        ReqD = ReqD - lv_Days_1;
                        Lv_APP_DATE_1 = Lv_APP_DATE_1.AddDays(1);
                    }
                }
            }
            else
            {
                CommonFuction.ShowMessage("Unable To Save");
            }
            return number;
        }
        catch
        {
            throw;
        }

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = string.Empty;
            URL = "HR_LEAVE_APP_REPORT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Printing Record"));
        }

    }

    private void Clear_Field()
    {
        try
        {
            foreach (GridViewRow Row in gvLeaveAssign.Rows)
            {
                Label lblLeaveMasterId = (Label)Row.FindControl("lblLeaveMasterId");
                CheckBox chk_LeaveType = (CheckBox)Row.FindControl("chk_LeaveType");
                TextBox txtReqDay = (TextBox)Row.FindControl("txtReqDay");
                TextBox txtRemainDay = (TextBox)Row.FindControl("txtRemain");
                TextBox txtFromDate = (TextBox)Row.Cells[6].FindControl("txtDuration_From");
                TextBox txtToDate = (TextBox)Row.Cells[7].FindControl("txtDuration_To");
                DropDownList DDLType = (DropDownList)Row.FindControl("DDLLeaveReq");
                //CheckBox ChbHalf = (CheckBox)Row.FindControl("ChbHalf");
                txtReqDay.Text = string.Empty;
                txtRemainDay.Text = string.Empty;
                txtPurpose.Text = string.Empty;
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                chk_LeaveType.Checked = false;
                DDLType.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }
    ///////////////////////////////Leave Detail/////////////////////////////////
    private void Load_Leave()
    {
        try
        {
            DataTable DTable = new DataTable();
            DTable = SaitexBL.Interface.Method.HR_LV_APP.Load_HR_LV_APP_Form_Record(ddlEmployee.SelectedValue.ToString(), POSITION, "2");
            gvReportDisplayGrid.DataSource = DTable;
            gvReportDisplayGrid.DataBind();
            gvReportDisplayGrid.Visible = true;
        }
        catch
        {
            throw;
        }

    }

    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReportDisplayGrid.PageIndex = e.NewPageIndex;
            Load_Leave();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in gridview page index Change"));
        }
    }

    protected void imgbtnInsert_Click1(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid == true)
        {
            try
            {
                foreach (GridViewRow Row in gvLeaveAssign.Rows)
                {
                    CheckBox Chk = (CheckBox)Row.FindControl("chk_LeaveType"); ;
                    if (Chk.Checked)
                    {
                        string IsHalfDay = null;
                        Label lblLeaveMasterId = (Label)Row.FindControl("lblLeaveMasterId");
                        int LeaveMasterId = int.Parse(lblLeaveMasterId.Text.Trim());
                        DropDownList DDLType = (DropDownList)Row.FindControl("DDLLeaveReq");
                        TextBox txtFromDate = (TextBox)Row.Cells[6].FindControl("txtDuration_From");
                        TextBox txtToDate = (TextBox)Row.Cells[7].FindControl("txtDuration_To");
                        txtFromDate.Text = Request.Form[txtFromDate.UniqueID];
                        txtToDate.Text = Request.Form[txtToDate.UniqueID];
                        if (txtFromDate.Text.Trim() != null && txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "" && txtToDate.Text.Trim() != null)
                        {

                            DateTime DTFrom = DateTime.Parse(txtFromDate.Text.ToString());
                            DateTime DTTo = DateTime.Parse(txtToDate.Text.ToString());

                            IsHalfDay = DDLType.SelectedValue.ToString();
                            if (DDLType.SelectedValue == "FH")
                            {
                                IsHalfDay = "1H";
                            }
                            else if (DDLType.SelectedValue == "SH")
                            {
                                IsHalfDay = "2H";
                            }
                            else
                            {
                                IsHalfDay = "FD";
                            }
                            if (DateTime.Compare(DTTo, DTFrom) < 0)
                            {
                                Common.CommonFuction.ShowMessage("To date must be greater than from date");
                            }
                            else
                            {
                                if (Can_Save(DTFrom, DTTo))
                                {
                                    sResult = SaveRecord(LeaveMasterId, DTFrom, DTTo, IsHalfDay);
                                }
                                else
                                {
                                    CommonFuction.ShowMessage("You already applied for leave for this period.Or Check Leave Mapping");
                                }
                            }
                        }
                        else
                        {
                            CommonFuction.ShowMessage("Please Check To Date & From date");
                        }
                    }
                }
                if (sResult > 0)
                {
                    Load_Leave();
                    getLeaveRecord();
                    Clear_Field();
                    CommonFuction.ShowMessage("Record Save Scessfully");

                }
            }
            catch (Exception ex)
            {
                Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Inserting Record"));
            }
        }

    }

    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/HRMS/Pages/LeaveApplication.aspx", false);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Clear page  Record"));
        }
    }

    protected void imgbtnExit_Click1(object sender, ImageClickEventArgs e)
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
                Response.Redirect("~/HRMS/Pages/EmployeeHomePage.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Exit Page"));
        }
    }    
    protected void gvReportDisplayGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UID = int.Parse(e.CommandArgument.ToString());
           if (e.CommandName == "EmpDelete")
            {
                int res= SaitexBL.Interface.Method.HR_LV_APP.Delete_Record_by_AppId(UID);
                if (res > 0)
                {
                    Common.CommonFuction.ShowMessage("Record Delete Sucessfully");
                    getLeaveRecord();
                    Load_Leave();
                }                
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }  
}
