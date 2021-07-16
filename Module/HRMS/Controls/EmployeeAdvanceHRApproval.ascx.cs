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

public partial class Module_HRMS_Controls_EmployeeAdvanceHRApproval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable DTable;
    private static int SelectRowNo;
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
            TxtAppNo.Attributes.Add("readonly", "readonly");
            TxtEmpCode.Attributes.Add("readonly", "readonly");
            TxtEmpName.Attributes.Add("readonly", "readonly");
            TxtDepartment.Attributes.Add("readonly", "readonly");
            TxtDesignation.Attributes.Add("readonly", "readonly");
            TxtBranch.Attributes.Add("readonly", "readonly");
            TxtEmpLevel.Attributes.Add("readonly", "readonly");
            TxtPosition.Attributes.Add("readonly", "readonly");
            TxtGrade.Attributes.Add("readonly", "readonly");
            TxtPosition.Attributes.Add("readonly", "readonly");
            TxtAmountApproveByHOD.Attributes.Add("readonly", "readonly");
            TxtAmountRequest.Attributes.Add("readonly", "readonly");
            TxtApplydate.Attributes.Add("readonly", "readonly");           
            TxtHODApprovDate.Attributes.Add("readonly", "readonly");
            TxtHRApprovalDate.Attributes.Add("readonly", "readonly");            
          
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
            TxtAmountApproveByHR.Text = string.Empty;
            TxtNoOfInstalMent.Text = string.Empty;
            TxtHRApprovalDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            DivDetail.Visible = false;
            Load_Employee_Advance_Taken();
        }
        catch
        {
            throw;
        }
    }
    private void Load_Employee_Detail(string EMP_CODE,string COMP_CODE,string BRANCH_CODE, string APPLICATION_ID)
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_ADVANCE_REQUEST.Advance_Taken_By_Employee_For_Approved(EMP_CODE.ToString(), COMP_CODE.Trim().ToString(), BRANCH_CODE.Trim().ToString(),APPLICATION_ID );
            if (DT.Rows.Count > 0)
            {
                TxtBranch.Text = DT.Rows[0]["BRANCH_NAME"].ToString();
                TxtDepartment.Text = DT.Rows[0]["Department"].ToString();
                TxtDesignation.Text = DT.Rows[0]["Designation"].ToString();
                TxtEmpCode.Text = DT.Rows[0]["EMP_CODE"].ToString();
                TxtEmpName.Text = DT.Rows[0]["EMPLOYEENAME"].ToString();
                TxtGrade.Text = DT.Rows[0]["GRADENAME"].ToString();
                TxtEmpLevel.Text = DT.Rows[0]["EMPLEVEL"].ToString();
                TxtPosition.Text = DT.Rows[0]["POSITION"].ToString();
                TxtAppNo.Text = DT.Rows[0]["APPL_NO"].ToString();                
                TxtAmountRequest.Text = DT.Rows[0]["APPLY_AMOUNT"].ToString();
                TxtApplydate.Text = DT.Rows[0]["APPLY_DATE"].ToString();
                TxtAmountApproveByHOD.Text = DT.Rows[0]["HOD_APPROVED_AMT"].ToString();
                TxtHODApprovDate.Text = DT.Rows[0]["HOD_APPROVED_DATE"].ToString();                

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
            DTable = SaitexBL.Interface.Method.HR_EMP_ADVANCE_REQUEST.Advance_Taken_By_Employee_For_Approved(string.Empty, oUserLoginDetail.COMP_CODE.Trim().ToString(), oUserLoginDetail.CH_BRANCHCODE.Trim().ToString(),string.Empty);
            GridViewAdvanceTakenDetail.DataSource = DTable;
            GridViewAdvanceTakenDetail.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void GridViewAdvanceTakenDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DivDetail.Visible = true;
                Label lblempcode = ((Label)row.FindControl("lblempcode"));
                Load_Employee_Detail(lblempcode.Text.Trim().ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, UniqueId.ToString());                
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in selecting record"));
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
    protected void GridViewAdvanceTakenDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                SelectRowNo = e.Row.RowIndex;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in grid"));
        }

    }
}
