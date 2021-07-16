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
using DBLibrary;
using Common;
using errorLog;


public partial class Module_HRMS_Pages_OutDoorDutyForm : System.Web.UI.Page
{
    private static string User_Code= string.Empty;
    private static string Auth_By = string.Empty;
    private static string DESIG_CODE = string.Empty;
    SaitexDM.Common.DataModel.EMPOUTDOORDUTY EMPOUTDOORDUTY = new SaitexDM.Common.DataModel.EMPOUTDOORDUTY();
    protected void Page_Load(object sender, EventArgs e)
    {
        imgbtnInsert.Attributes.Add("onclick", "return validate()");
        if (Session["EmpCode"] != null)
        {
            if (!IsPostBack)
            {
                User_Code = Session["EmpCode"].ToString();
                Auth_By = Session["ReportTo"].ToString();
                DESIG_CODE = Session["DESIG_CODE"].ToString();
                Fill_EmpData_InForm();
                Load_OD_Duty();
            }
        }
        else
        {
            Response.Redirect("/Saitex/Module/HRMS/Pages/Default.aspx", false);
        }

    }
    private void Fill_EmpData_InForm()
    {
        try
        {
            DataTable DT = SaitexDL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(User_Code.ToString());
            if (DT.Rows.Count > 0)
            {
                txtEmpName.Text  = DT.Rows[0]["EMPLOYEENAME"].ToString().Trim();
                TxtDept .Text = DT.Rows[0]["DEPT_NAME"].ToString();
                TxtDesig .Text = DT.Rows[0]["DESIG_NAME"].ToString();               
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(ex.Message);
        }
    }
    private void CantrolInitialised()
    {
        TxtFromdate.Text = "";
        TxtFromTime.Text = "00:00";
        TxtToDate.Text = "";
        TxtToTime.Text = "00:00";
        TxtOnFrom.Text = "";
        TxtOnTo.Text = "";
        TxtPlace.Text = "";       
    }
    protected void imgbtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        if (Can_Save())
        {
            if (Save_Record())
            {
                Common.CommonFuction.ShowMessage("Save record sucessfully");
                CantrolInitialised();
                Load_OD_Duty();
            }
            else
            {
                Common.CommonFuction.ShowMessage("unable to save");
            }          
            
        }

    }
    private bool Can_Save()
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
    private bool Save_Record()
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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        CantrolInitialised();
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
                Response.Redirect("/Saitex/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "";
        URL = "Out_Door_DutyRpt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
    }
    private void Load_OD_Duty()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.EMPOUTDOORDUTY.Load_ODD_Record_For_Approved(User_Code, DESIG_CODE , "2");
            gvReportDisplayGrid.DataSource = DTable;
            gvReportDisplayGrid.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReportDisplayGrid.PageIndex = e.NewPageIndex;
        Load_OD_Duty();
    }  

}
