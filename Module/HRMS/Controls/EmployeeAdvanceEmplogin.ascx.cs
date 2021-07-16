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
public partial class Module_HRMS_Controls_EmployeeAdvanceApplicationScreen_employee_Login_ : System.Web.UI.UserControl
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
                Read_Only_Field();
                Initial_Page_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log"));
        }
    }
    private void Read_Only_Field()
    {
        try
        {
            txtAdvdate.Attributes.Add("readonly", "readonly");
            txtBranch.Attributes.Add("readonly", "readonly");
            txtDept.Attributes.Add("readonly", "readonly");
            txtDesig.Attributes.Add("readonly", "readonly");
            txtEmpCode.Attributes.Add("readonly", "readonly");
            txtEmpName.Attributes.Add("readonly", "readonly");
            txtGrade.Attributes.Add("readonly", "readonly");
            txtlavel.Attributes.Add("readonly", "readonly");
            txtposition.Attributes.Add("readonly", "readonly");            
        }
        catch
        {
            throw;
        }
    }
    private void Initial_Page_Control()
    {
        try
        {
            lblMode.Text = "Save";
            txtAmtAply.Text = string.Empty;
            txtpurpose.Text = string.Empty;
            txtAdvdate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            Load_Employee_Detail();
            Load_Employee_Advance_Taken();

        }
        catch
        {
            throw;
        }
    }
    private void Load_Employee_Detail()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Record(oUserLoginDetail.UserCode.Trim().ToString(), oUserLoginDetail.COMP_CODE.Trim().ToString(), oUserLoginDetail.CH_BRANCHCODE.Trim().ToString());
            if (DT.Rows.Count > 0)
            {
                txtBranch.Text = DT.Rows[0]["BRANCH_NAME"].ToString();
                txtDept.Text = DT.Rows[0]["Department"].ToString();
                txtDesig.Text = DT.Rows[0]["Designation"].ToString();
                txtEmpCode.Text = DT.Rows[0]["EMP_CODE"].ToString();
                txtEmpName.Text = DT.Rows[0]["EMPLOYEENAME"].ToString();
                txtGrade.Text = DT.Rows[0]["GRADENAME"].ToString();
                txtlavel.Text = DT.Rows[0]["EMPLEVEL"].ToString();
                txtposition.Text = DT.Rows[0]["POSITION"].ToString();                
            }

        }
        catch 
        {
            throw;
        }
    }
    private void Load_Employee_Advance_Taken()
    {
        try
        {
            DTable = new DataTable();
            DTable  = SaitexBL.Interface.Method.HR_EMP_ADVANCE_REQUEST.Advance_Taken_By_Employee(oUserLoginDetail.UserCode.Trim().ToString(), oUserLoginDetail.COMP_CODE.Trim().ToString(), oUserLoginDetail.CH_BRANCHCODE.Trim().ToString());
            GridViewAdvanceTakenDetail.DataSource=DTable;
            GridViewAdvanceTakenDetail.DataBind();           
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initial_Page_Control ();
        }
        catch (Exception ex)
        {  
          
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Insert_Record ())
            {
                Common.CommonFuction.ShowMessage("Record Insert Sucesfully");
                Initial_Page_Control();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in record inserting");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record Saving.\r\nSee error log"));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Insert_Record())
            {
                Common.CommonFuction.ShowMessage("Record Update Sucesfully");
                Initial_Page_Control();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in record updating");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record Updating.\r\nSee error log"));
        }
    }
    private bool Insert_Record()
    {
        try
        {
            bool Res=false ;
            if (txtAmtAply.Text.Trim().ToString() != string.Empty && txtAmtAply.Text.Trim().ToString() != "0")
            {
                SaitexDM.Common.DataModel.HR_EMP_ADVANCE_REQUEST AR = new SaitexDM.Common.DataModel.HR_EMP_ADVANCE_REQUEST();
                AR.APPL_NO = decimal.Parse(TxtApplicationNo.Text.Trim().ToString());
                AR.APPLY_AMOUNT = decimal.Parse(txtAmtAply.Text.Trim().ToString());
                AR.APPLY_DATE = DateTime.Parse(txtAdvdate.Text.Trim().ToString());
                AR.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
                AR.COMP_CODE = oUserLoginDetail.COMP_CODE.Trim().ToString();
                AR.EMP_CODE = oUserLoginDetail.UserCode.Trim().ToString();
                AR.HOD_APPROVED_AMT = 0;
                AR.PURPOSE = txtpurpose.Text.Trim().ToString();
                AR.REMAIN_AMT = decimal.Parse(txtAmtAply.Text.Trim().ToString());
                AR.TUSER = oUserLoginDetail.UserCode.Trim().ToString();
                Res = SaitexBL.Interface.Method.HR_EMP_ADVANCE_REQUEST.Advance_Request_Insert(AR);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please enter the amount");
            }
            return Res;
        }
        catch
        {
            throw;
        }
    }
    protected void GridViewAdvanceTakenDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridViewAdvanceTakenDetail.PageIndex = e.NewPageIndex;
            Load_Employee_Advance_Taken();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Grid View.\\r\\nSee error log for detail."));
        }
    }
    protected void GridViewAdvanceTakenDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UniqueId = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "EmpEdit")
        {
            FillDetailByGrid(UniqueId);
            tdUpdate.Visible = true;
            tdSave.Visible = false;
        }
        else if (e.CommandName == "EmpDelete")
        {
            Delete_record_by_ID(UniqueId.ToString());
        }
    }
    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            DataView dv = new DataView(DTable);
            dv.RowFilter = "APPL_NO=" + UniqueId;
            if (dv.Count > 0)
            {
                string HRStatus = dv[0]["HR_APPROVED_STATUS"].ToString();
                string HODStatus = dv[0]["HOD_APPROVED_STATUS"].ToString();
                if (HRStatus != "1" && HODStatus != "1")
                {
                    txtAdvdate.Text = dv[0]["APPLY_DATE"].ToString();
                    txtAmtAply.Text = dv[0]["APPLY_AMOUNT"].ToString();
                    TxtApplicationNo.Text = dv[0]["APPL_NO"].ToString();
                    txtpurpose.Text = dv[0]["PURPOSE"].ToString();
                    ViewState["APPL_NO"] = UniqueId;
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Your Application in process!You Can't Change");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Your Application in process!You Can't Change');", true);
                }
            }
        }
        catch
        {
            throw;
        }
    }
    private void Delete_record_by_ID(string aPPLIED_iD)
    {
        try
        {
            bool Res = SaitexBL.Interface.Method.HR_EMP_ADVANCE_REQUEST.Delete_Record_By_Id(aPPLIED_iD,oUserLoginDetail.UserCode );
            if (Res)
            {
                Common.CommonFuction.ShowMessage("Record delete sucessfully");
                Initial_Page_Control ();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to delete,please try again");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting Record.\r\nSee error log for detail."));
        }
    }    
}
