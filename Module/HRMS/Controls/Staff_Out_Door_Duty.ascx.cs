using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBLibrary;
using Common;
using errorLog;
using System.Data;


public partial class Module_HRMS_Controls_Staff_Out_Door_Duty : System.Web.UI.UserControl
{
    private static string User_Code = string.Empty;
    private static string Auth_By = string.Empty;
    private static string DESIG_CODE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.EMPOUTDOORDUTY EMPOUTDOORDUTY = new SaitexDM.Common.DataModel.EMPOUTDOORDUTY();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            imgbtnInsert.Attributes.Add("onclick", "return validate()");
            if (!IsPostBack)
            {  
                
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Page Loading"));
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
            string commandText = "SELECT * FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS,STATUS FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' and STATUS='A' AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause = " and emp_code not in(SELECT emp_code FROM (SELECT EMP_CODE, F_NAME, F_NAME || ' ' || M_NAME || ' ' || L_NAME AS EMPLOYEENAME,COMP_CODE, DEL_STATUS,STATUS  FROM HR_EMP_MST WHERE UPPER(EMP_CODE) LIKE :SearchQuery OR UPPER(F_NAME) LIKE :SearchQuery ORDER BY EMP_CODE) WHERE COMP_CODE = :COMP_CODE AND DEL_STATUS = '0' and STATUS='A' AND ROWNUM <= '" + startOffset + "')";
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
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text.ToUpper() + '%',oUserLoginDetail.COMP_CODE.ToString(), string.Empty, "");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    
    private void Fill_EmpData_InForm(string Emp_code)
    {
        try
        {
            DataTable DT = SaitexDL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(Emp_code.ToString());
            if (DT.Rows.Count > 0)
            {               
                TxtDept.Text = DT.Rows[0]["DEPT_NAME"].ToString();
                TxtDesig.Text = DT.Rows[0]["DESIG_NAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void CantrolInitialised()
    {
        try
        {
            TxtFromdate.Text = "";
            TxtFromTime.Text = "00:00";
            TxtToDate.Text = "";
            TxtToTime.Text = "00:00";
            TxtOnFrom.Text = "";
            TxtOnTo.Text = "";
            TxtPlace.Text = "";
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgbtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Can_Save())
            {
                if (Save_Record())
                {
                    Common.CommonFuction.ShowMessage("Save record sucessfully");
                    CantrolInitialised();
                    Load_OD_Duty(User_Code);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("unable to save");
                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Inserting Record"));
        }

    }
    private bool Can_Save()
    {
        try
        {
            bool Result = false;
            DateTime DTFrom = DateTime.Parse(TxtFromdate.Text.ToString());
            DateTime DTTo = DateTime.Parse(TxtToDate.Text.ToString());
            if (DateTime.Compare(DTTo, DTFrom) < 0)
            {
                Common.CommonFuction.ShowMessage("To date must be greater than from date");
                return false;
            }
            if (TxtFromdate.Text != "" && TxtToDate.Text != "")
            {
                Result = true;
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Check from date or to date");
                return false;
            }
            return Result;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private bool Save_Record()
    {
        try
        {
            string Str1 = null;
            string Str2 = null;
            Str1 = String.Format("{0:dd/MM/yyyy}", TxtFromdate.Text.ToString()) + " " + TxtFromTime.Text.ToString();
            Str2 = String.Format("{0:dd/MM/yyyy}", TxtToDate.Text.ToString()) + " " + TxtToTime.Text.ToString();
            DateTime DtFrom = DateTime.Parse(Str1.ToString());
            DateTime DtTo = DateTime.Parse(Str2.ToString());

            EMPOUTDOORDUTY.AUTH_BY = Auth_By;
            EMPOUTDOORDUTY.EMP_CODE = User_Code;
            EMPOUTDOORDUTY.FROM_DATE = DtFrom;
            EMPOUTDOORDUTY.TO_DATE = DtTo;
            EMPOUTDOORDUTY.PLACE = TxtPlace.Text.Trim().ToString();
            EMPOUTDOORDUTY.ON_FROM = TxtOnFrom.Text.Trim().ToString();
            EMPOUTDOORDUTY.ON_TO = TxtOnTo.Text.Trim().ToString();
            EMPOUTDOORDUTY.DEL_STATUS = '0';
            bool Result = SaitexBL.Interface.Method.EMPOUTDOORDUTY.InsertEmployeeMaster(EMPOUTDOORDUTY);
            if (Result)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            CantrolInitialised();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Initial Control"));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Exit Page"));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "";
            URL = "../Reports/OutDoorDutyRpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Printing Record"));
        }
    }
    private void Load_OD_Duty(string Emp_code)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.EMPOUTDOORDUTY.Load_ODD_Record_For_Approved(Emp_code, DESIG_CODE, "2");
            gvReportDisplayGrid.DataSource = DTable;
            gvReportDisplayGrid.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }
    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReportDisplayGrid.PageIndex = e.NewPageIndex;
            Load_OD_Duty(User_Code);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Paging"));
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            User_Code = ddlEmployee.SelectedValue.Trim().ToString();
            Fill_EmpData_InForm(User_Code.ToString());
            Load_OD_Duty(User_Code);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Selecting Record"));
        }
    }
}
