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
    decimal grdTotal = 0;   
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                }
                fillYear();
             
                BindDesignation();
                bindddlBrachName();
                getEmployeeDepartment();              
               
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    private void bindgvSalaryDisplay()
    {
        try
        {

            string strCompanyCode = oUserLoginDetail.COMP_CODE;
            string StrQuery = "";
            StrQuery = " WHERE ltrim(rtrim(E.COMP_CODE))='" + strCompanyCode + "' AND ltrim(rtrim(E.DEL_STATUS))='0'";
            if (ddlBranch.SelectedValue.Trim() != "" && ddlBranch.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + "AND ltrim(rtrim(E.BRANCH_CODE))='" + ddlBranch.SelectedValue.Trim() + "'";
            }
            if (ddlDepartment.SelectedValue.Trim() != "" && ddlDepartment.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(E.DEPT_CODE))='" + ddlDepartment.SelectedValue.Trim() + "'";
            }
            if (DDLDesigination.SelectedValue.Trim() != "" && DDLDesigination.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(E.DESIG_CODE))='" + DDLDesigination.SelectedValue.Trim() + "'";
            }
            if (ddlYear.SelectedValue.Trim() != "" && ddlYear.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(S.SAL_YEAR))='" + ddlYear.SelectedValue.Trim() + "'";
            }
            if (ddlMonth.SelectedValue.Trim() != "" && ddlMonth.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(LPAD(S.SAL_MONTH,2,0)))=LPAD('" + ddlMonth.SelectedValue.Trim() + "',2,0)";
            }
            else if (oUserLoginDetail.OPEN_MONTH_NO.ToString() != "")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(LPAD(S.SAL_MONTH,2,0)))=LPAD('" + oUserLoginDetail.OPEN_MONTH_NO.ToString() + "',2,0)";
            }
            else
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(LPAD(S.SAL_MONTH,2,0)))=LPAD('" + System.DateTime.Now.Month.ToString() + "',2,0)";
            }
            if (DDLEmployee.SelectedValue.Trim() != "" && DDLEmployee.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(S.EMP_CODE))='" + DDLEmployee.SelectedValue.Trim() + "'";
            }
            if (DDLCader.SelectedValue.Trim() != "" && DDLCader.SelectedValue.Trim() != "0")
            {
                StrQuery = StrQuery + " AND ltrim(rtrim(E.CADDER_CODE))='" + DDLCader.SelectedValue.Trim() + "'";
            }
            DataTable DTable = SaitexBL.Interface.Method.HR_SAL_SLIP_MST.Load_Salary_Record(StrQuery.ToString());
                gvSalaryDisplay.DataSource = DTable;
                gvSalaryDisplay.DataBind();
                lblTotalRecord.Text = DTable.Rows.Count.ToString().Trim();
                if (DTable.Rows.Count < 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Salry for selected month and year not generated');", true);
                }            
        }
        catch
        {
            throw;
        }
    }
    //private void bindgvSalaryDisplay()
    //{
    //    try
    //    {
    //        con = new OracleConnection();
    //        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
    //        con.Open();
    //        string strBindCompany = "SELECT ltrim(rtrim(SAL_SLIP_MST_ID)) SAL_SLIP_MST_ID,slp.EMP_CODE,SAL_YEAR,(F_NAME||' '||M_NAME||' '|| L_NAME) empName,";
    //        strBindCompany = strBindCompany + " SAL_MONTH,SAL_WORKING_DAY,HLD,PAID_DAY,NET_SAL,EPF,ERN_AMT, LOAN_AMT,DEDCT_AMT,CASUAL_LV,SICK_LV,EARN_LV, MATERNITY_LV,PATERNITY_LV,COMPENSATORY_LV,LV_WITHOUT_PAY,LOCK_LV FROM HR_SAL_SLIP_MST slp,HR_EMP_MST em where slp.EMP_CODE=em.EMP_CODE and ltrim(rtrim(slp.DEL_STATUS))='0'";
    //        if (ddlBranch.SelectedValue.Trim() != "" || ddlDepartment.SelectedValue.Trim() != "" || ddlMonth.SelectedValue.Trim() != "")
    //        {
    //            if (ddlBranch.SelectedValue.Trim() != "")
    //            {
    //                strBindCompany = strBindCompany + " and ltrim(rtrim(em.BRANCH_CODE))='" + ddlBranch.SelectedValue.Trim() + "'";
    //            }

    //            if (ddlDepartment.SelectedValue.Trim() != "")
    //            {
    //                strBindCompany = strBindCompany + " and ltrim(rtrim(em.DEPT_CODE))='" + ddlDepartment.SelectedValue.Trim() + "'";
    //            }

    //            if (ddlMonth.SelectedValue.Trim() != "")
    //            {
    //                strBindCompany = strBindCompany + " and ltrim(rtrim(slp.SAL_MONTH))='" + ddlMonth.SelectedValue.Trim() + "'";
    //            }
    //            if (ddlYear.SelectedValue.Trim() != "")
    //            {
    //                strBindCompany = strBindCompany + " and ltrim(rtrim(slp.SAL_YEAR))='" + ddlYear.SelectedValue.Trim() + "'";
    //            }
    //            if (DDLEmployee.SelectedValue.Trim() != "")
    //            {
    //                strBindCompany = strBindCompany + " and ltrim(rtrim(EM.EMP_CODE))='" + ddlYear.SelectedValue.Trim() + "'";
    //            }
    //            strBindCompany = strBindCompany + " order by EMP_CODE asc";
    //            OracleDataAdapter OCD = new OracleDataAdapter(strBindCompany, con);
    //            DataTable DTable = new DataTable();
    //            OCD.Fill(DTable);
    //            gvSalaryDisplay.DataSource = DTable;
    //            gvSalaryDisplay.DataBind();
    //            lblTotalRecord.Text = DTable.Rows.Count.ToString().Trim();
    //            if (DTable.Rows.Count < 1)
    //            {
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Salry for selected month and year not generated');", true);
    //            }
    //        }
    //    }       
    //    catch 
    //    {
    //        throw ;
    //    }        
    //}
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
            DDLDesigination.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
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
        catch
        {
            throw;
        }

    }
    private void fillYear()
    {
        try
        {
            for (int i = -2; i < 2; i++)
            {
                ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
            }
            ddlYear.Items.Insert(0, new ListItem("---Select---", ""));
            ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
        }
        catch
        {
            throw;
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            bindgvSalaryDisplay();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
        }
    }        
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string Month="", Year = "";
            if (ddlMonth.SelectedValue.Trim() != "" && ddlMonth.SelectedValue.Trim() != "0")
            {
                Month = ddlMonth.SelectedValue.Trim().ToString();
            }
            else if (oUserLoginDetail.OPEN_MONTH_NO.ToString() != "")
            {
                Month = oUserLoginDetail.OPEN_MONTH_NO.ToString();
            }
            else
            {
                Month = System.DateTime.Now.Month.ToString();
            }
            if (ddlYear.SelectedValue.Trim() != "" && ddlYear.SelectedValue.Trim() != "0")
            {
                Year  = ddlYear.SelectedValue.Trim().ToString();
            }
            else if (oUserLoginDetail.OPEN_MONTH_NO.ToString() != "")
            {
                Year  = oUserLoginDetail.OPEN_YEAR.ToString();
            }
            else
            {
                Year  = System.DateTime.Now.Year.ToString();
            }
            if (ddlYear.SelectedValue.ToString() != "" && ddlYear.SelectedValue != null)
            {
                Response.Redirect("../../Admin/Pages/PrintSSlip.aspx?Year=" + Year + "&MONTH='" + Month  + "'");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Year");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
        }
    }
    //protected void gvSalaryDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        gvSalaryDisplay.PageIndex = e.NewPageIndex;
    //        bindgvSalaryDisplay();
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
    //    }
    //}
    protected void gvSalaryDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NET_SAL"));
          grdTotal = grdTotal + rowTotal;
         }
         if (e.Row.RowType == DataControlRowType.Footer)
         {
          Label lbl = (Label)e.Row.FindControl("lblTotal");
          lbl.Text = grdTotal.ToString();
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
            ddlMonth.SelectedIndex = -1;
            ddlYear.SelectedIndex = -1;
            ddlBranch.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = -1;
            DDLEmployee.SelectedIndex = -1;
            DDLDesigination.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
    protected void DDLEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindgvSalaryDisplay();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindgvSalaryDisplay();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
        }
    }
    protected void DDLDesigination_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindgvSalaryDisplay();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
        }
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindgvSalaryDisplay();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindgvSalaryDisplay();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"problem in Displaying salary.\r\nSee error log for detail."));
        }
    }
}
