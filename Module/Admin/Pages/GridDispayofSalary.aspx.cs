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
using System.Data.OracleClient;
using errorLog;
using Common;
using DBLibrary;

public partial class Module_HRMS_Pages_GridDispayofSalary : System.Web.UI.Page
{
    csSaitex obj = null;
    OracleConnection con = null;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];       
        if (!IsPostBack)
        {
            if (Request.QueryString["cId"] != null)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Salry for selected month and year generated.');", true);
                }
                else if (Request.QueryString["cId"].ToString().Trim() == "R")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Salry for selected month and year re-generated.');", true);
                }
            }
            fillYear();
            bindddlBrachName();
            getEmployeeDepartment();
            bindgvSalaryDisplay();            
            }
            Session["saveStatus"] = 0;  

    }
    private void bindgvSalaryDisplay()
    {
        try
        {
            string strBindCompany = "SELECT ltrim(rtrim(SAL_SLIP_MST_ID)) SAL_SLIP_MST_ID,slp.EMP_CODE,SAL_YEAR,";
            strBindCompany = strBindCompany + " SAL_MONTH,SAL_WORKING_DAY,HLD,PAID_DAY,NET_SAL,EPF,ERN_AMT, LOAN_AMT,DEDCT_AMT,CASUAL_LV,SICK_LV,EARN_LV, MATERNITY_LV,PATERNITY_LV,COMPENSATORY_LV,LV_WITHOUT_PAY,LOCK_LV FROM HR_SAL_SLIP_MST slp,HR_EMP_MST em where slp.EMP_CODE=em.EMP_CODE and ltrim(rtrim(slp.DEL_STATUS))='0'";
            if (ddlBranch.SelectedValue.Trim() != "" || ddlDepartment.SelectedValue.Trim() != "" || ddlMonth.SelectedValue.Trim() != "")
            {
                if (ddlBranch.SelectedValue.Trim() != "")
                {
                    strBindCompany = strBindCompany + " and ltrim(rtrim(em.BRANCH_CODE))='" + ddlBranch.SelectedValue.Trim() + "'";
                }

                if (ddlDepartment.SelectedValue.Trim() != "")
                {
                    strBindCompany = strBindCompany + " and ltrim(rtrim(em.DEPT_CODE))='" + ddlDepartment.SelectedValue.Trim() + "'";
                }

                if (ddlMonth.SelectedValue.Trim() != "")
                {
                    strBindCompany = strBindCompany + " and ltrim(rtrim(slp.SAL_MONTH))='" + ddlMonth.SelectedValue.Trim() + "'";
                }
                if (ddlYear.SelectedValue.Trim() != "")
                {
                    strBindCompany = strBindCompany + " and ltrim(rtrim(slp.SAL_YEAR))='" + ddlYear.SelectedValue.Trim() + "'";
                }
                strBindCompany = strBindCompany + " order by to_number(SAL_YEAR) desc,to_number(SAL_MONTH) asc";
                OracleDataAdapter OCD = new OracleDataAdapter(strBindCompany, con);
                DataTable DTable = new DataTable();
                OCD.Fill(DTable);
                gvSalaryDisplay.DataSource = DTable;
                gvSalaryDisplay.DataBind();
                lblTotalRecord.Text = DTable.Rows.Count.ToString().Trim();
                if (DTable.Rows.Count < 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Salry for selected month and year not generated');", true);
                }
            }
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Binding Salary.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Binding Salary.\r\nSee error log for detail."));
        }        
    }
   private void bindddlBrachName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE .Trim().ToString();           
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
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Binding Branch.\r\nSee error log for detail."));
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
            ddlDepartment.Items.Insert(0, new ListItem("---------------Select---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Binding Department.\r\nSee error log for detail."));
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Binding Department.\r\nSee error log for detail."));
        }


    }
    private void fillYear()
    {
        for (int i = -15; i < 15; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        ddlYear.Items.Insert(0, new ListItem("---Select---", ""));
        ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            bindgvSalaryDisplay();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Binding Salary.\r\nSee error log for detail."));
        }
    }
    protected void gvSalaryDisplay_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SalarySlipEdit")
        {
            if (CheckingLockingStatus(e.CommandArgument.ToString().Trim()) == 1)
            {
                Response.Redirect("./SalarySlipView.aspx?SalaryId=" + e.CommandArgument.ToString().Trim(), false);
            }
            else
            {
                Response.Redirect("./UpdateSalarySlip.aspx?SalaryId=" + e.CommandArgument.ToString().Trim(), false);
            }
        }
    }
    

    private int CheckingLockingStatus(string TableId)
    {
        int result = 0;
        try
        {
            string strLockSataus = "SELECT ltrim(rtrim(SAL_SLIP_MST_ID)) ,LOCK_LV FROM HR_SAL_SLIP_MST where ltrim(rtrim(SAL_SLIP_MST_ID))='" + TableId.Trim() + "' and LOCK_LV='Lock' and ltrim(rtrim(DEL_STATUS))='0'";
             OracleDataAdapter OCD = new OracleDataAdapter(strLockSataus, con);
            DataTable DTable = new DataTable();
            OCD.Fill(DTable);
            if (DTable.Rows.Count > 0)
            {
                result = 1;
            }           
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Binding Branch.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Binding Branch.\r\nSee error log for detail."));
        }        
        return result;
    }
    protected void gvSalaryDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSalaryDisplay.PageIndex = e.NewPageIndex;
        bindgvSalaryDisplay();  
    }
}
