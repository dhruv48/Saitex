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
using Common;
using errorLog;
using AmountToWords;

public partial class Module_HRMS_Controls_Salary_Pre : System.Web.UI.UserControl
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
            bindDDLDepartment();
            BindDesignation();
            Bind_BrachName();
            //Bind_Grid_Record();            
        }
        catch
        {
            throw;
        }
    }
    
    private void Bind_Grid_Record()
    {
        string SearchQuery=string.Empty;
        try
        {
            SearchQuery = "WHERE LTRIM(RTRIM(E.DEL_STATUS))='0' AND SAL_MONTH=LPAD ('"+oUserLoginDetail.OPEN_MONTH_NO.ToString() +"', 2, 0) AND SAL_YEAR='"+ oUserLoginDetail.OPEN_YEAR.ToString() +"'";
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
            DataTable dt = SaitexBL.Interface.Method.HR_SAL_SLIP_MST.Load_Attendance_Record(SearchQuery, "D", "0");
            GVSalaryRecord.DataSource = dt;
            GVSalaryRecord.DataBind();            
        }
        catch
        {
            throw;
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
    private void Create_Data_Table()
    {
        try
        {
            DTable = new DataTable();
            DTable.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));
            DTable.Columns.Add(new DataColumn("SAL_MONTH", typeof(string)));
            DTable.Columns.Add(new DataColumn("SAL_YEAR", typeof(string)));
            DTable.Columns.Add(new DataColumn("UPDATE_WORKING_DAYS", typeof(string)));
            DTable.Columns.Add(new DataColumn("UPDATE_LWP_DAYS", typeof(string)));
            DTable.Columns.Add(new DataColumn("UPDATE_PAID_DAYS", typeof(string)));
            DTable.Columns.Add(new DataColumn("REMARKS", typeof(string)));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private bool Insert_Record()
    {
        bool Res = false;
        try
        {
            foreach (GridViewRow rw in GVSalaryRecord.Rows)
            {
                CheckBox chk = (CheckBox)rw.FindControl("ChkMark");
                if (chk.Checked)
                {
                    DataRow dr = DTable.NewRow();
                    dr["EMP_CODE"] = CommonFuction.funFixQuotes(((Label)rw.FindControl("lblempcode")).Text.ToString());
                    dr["SAL_MONTH"] = CommonFuction.funFixQuotes(((Label)rw.FindControl("LblSMonth")).Text.ToString());
                    dr["SAL_YEAR"] = CommonFuction.funFixQuotes(((Label)rw.FindControl("LblSYear")).Text.ToString());
                    dr["UPDATE_WORKING_DAYS"] = CommonFuction.funFixQuotes(((TextBox)rw.FindControl("TxtUpdateDays")).Text.ToString());
                    dr["UPDATE_LWP_DAYS"] = CommonFuction.funFixQuotes(((TextBox)rw.FindControl("TxtUpdateWP")).Text.ToString());
                    dr["UPDATE_PAID_DAYS"] = CommonFuction.funFixQuotes(((TextBox)rw.FindControl("TxtTotalPaidDays")).Text.ToString());
                    dr["REMARKS"] = CommonFuction.funFixQuotes(((TextBox)rw.FindControl("txtremarks")).Text.ToString());
                    DTable.Rows.Add(dr);
                }
            }
            if (DTable.Rows.Count > 0)
            {
                Res = SaitexBL.Interface.Method.HR_SAL_SLIP_MST.Update_Attendance_Record(DTable, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.UserCode);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Checked atleast one record");
            }
            return Res;
        }
        catch
        {
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
            if (Page.IsValid)
            {
                if (Insert_Record())
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Update Successfully');", true);
                    Bind_Grid_Record();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No record updated');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Printing record.\r\nSee error log for detail."));
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
                Response.Redirect("/Saitex/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
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
    protected void GVSalaryRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("ChkMarkAll")).Attributes.Add("onclick", "javascript:SelectAll('" + ((CheckBox)e.Row.FindControl("ChkMarkAll")).ClientID + "')");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                TextBox TxtWPDays = ((TextBox)e.Row.FindControl("TxtWithoutPay"));
                if (TxtWPDays.Text != null)
                {
                    if (Convert.ToInt32(TxtWPDays.Text.Trim().ToString()) >= 25)
                    {                        
                        e.Row.BackColor = System.Drawing.Color.Orchid;                
                    }
                    else if (Convert.ToInt32(TxtWPDays.Text.Trim().ToString()) >= 20 && Convert.ToInt32(TxtWPDays.Text.Trim().ToString()) < 25)
                    {                        
                        e.Row.BackColor = System.Drawing.Color.Khaki;                    
                    }
                    else if (Convert.ToInt32(TxtWPDays.Text.Trim().ToString()) >= 10 && Convert.ToInt32(TxtWPDays.Text.Trim().ToString()) < 20)
                    {
                        e.Row.BackColor = System.Drawing.Color.MistyRose;                  
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, "Problem in GridView RowData Bound");
        }
    }
    protected void TxtUpdateDays_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal TotalPayDays = 0;
            TextBox thisTextBox = (TextBox)sender;
            decimal UpdatePayDays = decimal.Parse(thisTextBox.Text.Trim().ToString());
            if (UpdatePayDays > 0)
            {
                GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
                TextBox TxtPaidDays = (TextBox)grdRow.FindControl("TxtPaidDays");
                TextBox TxtSL = (TextBox)grdRow.FindControl("TxtSL");
                TextBox TxtCL = (TextBox)grdRow.FindControl("TxtCL");
                TextBox TxtEL = (TextBox)grdRow.FindControl("TxtEL");
                TextBox TxtML = (TextBox)grdRow.FindControl("TxtML");
                TextBox TxtCO = (TextBox)grdRow.FindControl("TxtCO");
                TextBox TxtNH = (TextBox)grdRow.FindControl("TxtNH");
                TextBox TxtWO = (TextBox)grdRow.FindControl("TxtWO");
                TextBox TxtUpdateWP = (TextBox)grdRow.FindControl("TxtUpdateWP");
                TextBox TxtTotalPaidDays = (TextBox)grdRow.FindControl("TxtTotalPaidDays");

                Label LblTotalMonthDays = (Label)grdRow.FindControl("LblTotalMonthDays");
                decimal TMonthDays = decimal.Parse(LblTotalMonthDays.Text.Trim().ToString());

                decimal Paidays = decimal.Parse(TxtPaidDays.Text.Trim().ToString());
                decimal SL = decimal.Parse(TxtSL.Text.Trim().ToString());
                decimal CL = decimal.Parse(TxtCL.Text.Trim().ToString());
                decimal EL = decimal.Parse(TxtSL.Text.Trim().ToString());
                decimal ML = decimal.Parse(TxtML.Text.Trim().ToString());
                decimal CO = decimal.Parse(TxtCO.Text.Trim().ToString());
                decimal NH = decimal.Parse(TxtNH.Text.Trim().ToString());
                decimal WO = decimal.Parse(TxtWO.Text.Trim().ToString());
                decimal UpdateWP = decimal.Parse(TxtUpdateWP.Text.Trim().ToString());
                decimal OldPayDays = decimal.Parse(TxtTotalPaidDays.Text.Trim().ToString());


                decimal TotalCalculateDays = UpdatePayDays+SL+CL+EL+ML+CO+NH+WO;
                if (TotalCalculateDays > TMonthDays)
                {
                    TotalPayDays = TMonthDays;                    
                }
                else if(TotalCalculateDays >OldPayDays )
                {
                    decimal diff = TotalCalculateDays - OldPayDays;
                    if (UpdateWP >= diff)
                    {
                        decimal NewVal = UpdateWP - diff;
                        TxtUpdateWP.Text = NewVal.ToString();
                        UpdateWP = decimal.Parse(TxtUpdateWP.Text.Trim().ToString());
                    }
                }
                if (TotalPayDays > 0)
                {
                    TxtTotalPaidDays.Text = TotalPayDays.ToString();
                }
                else
                {
                    TotalPayDays = UpdatePayDays + SL +CL+ EL + ML + CO + NH + WO;
                    TxtTotalPaidDays.Text = TotalPayDays.ToString();
                }
               
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Adjustment.\r\nSee eroor log"));
        }
    }
    protected void TxtTotalPaidDays_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal TotalPayDays = 0;
            TextBox thisTextBox = (TextBox)sender;
            decimal UpdatePayDays = decimal.Parse(thisTextBox.Text.Trim().ToString());
            if (UpdatePayDays > 0)
            {
                GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
                TextBox TxtPaidDays = (TextBox)grdRow.FindControl("TxtPaidDays");
                TextBox TxtSL = (TextBox)grdRow.FindControl("TxtSL");
                TextBox TxtCL = (TextBox)grdRow.FindControl("TxtCL");
                TextBox TxtEL = (TextBox)grdRow.FindControl("TxtEL");
                TextBox TxtML = (TextBox)grdRow.FindControl("TxtML");
                TextBox TxtCO = (TextBox)grdRow.FindControl("TxtCO");
                TextBox TxtNH = (TextBox)grdRow.FindControl("TxtNH");
                TextBox TxtWO = (TextBox)grdRow.FindControl("TxtWO");
                TextBox TxtUpdateWP = (TextBox)grdRow.FindControl("TxtUpdateWP");
                TextBox TxtUpdateDays = (TextBox)grdRow.FindControl("TxtUpdateDays");


                Label LblTotalMonthDays = (Label)grdRow.FindControl("LblTotalMonthDays");
                decimal TMonthDays = decimal.Parse(LblTotalMonthDays.Text.Trim().ToString());

                decimal Paidays = decimal.Parse(TxtPaidDays.Text.Trim().ToString());
                decimal SL = decimal.Parse(TxtSL.Text.Trim().ToString());
                decimal CL = decimal.Parse(TxtCL.Text.Trim().ToString());
                decimal EL = decimal.Parse(TxtSL.Text.Trim().ToString());
                decimal ML = decimal.Parse(TxtML.Text.Trim().ToString());
                decimal CO = decimal.Parse(TxtCO.Text.Trim().ToString());
                decimal NH = decimal.Parse(TxtNH.Text.Trim().ToString());
                decimal WO = decimal.Parse(TxtWO.Text.Trim().ToString());
                decimal UpdateWP = decimal.Parse(TxtUpdateWP.Text.Trim().ToString());

                decimal OldPayDays = Paidays + SL+CL + EL + ML + CO + NH + WO ;

                if (UpdatePayDays > TMonthDays)
                {
                    TotalPayDays = TMonthDays;
                    thisTextBox.Text = TotalPayDays.ToString();
                    TxtUpdateWP.Text = "0";
                    decimal Pay_Days_Update=TMonthDays-(SL+CL + EL + ML + CO + NH + WO);
                    TxtUpdateDays.Text = Pay_Days_Update.ToString();
                }
                else if (UpdatePayDays > OldPayDays)
                {
                    decimal diff = UpdatePayDays -OldPayDays  ;
                    if (UpdateWP >= diff)
                    {
                        decimal NewVal = UpdateWP - diff;
                        decimal Pay_Days_Update = TMonthDays - (NewVal+SL + CL + EL + ML + CO + NH + WO);
                        TxtUpdateDays.Text = Pay_Days_Update.ToString();
                        TxtUpdateWP.Text = NewVal.ToString();
                        UpdateWP = decimal.Parse(TxtUpdateWP.Text.Trim().ToString());
                    }
                }

            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Adjustment.\r\nSee eroor log"));
        }
    }

    protected void CmdView_Click(object sender, EventArgs e)
    {
       try
       {
           Bind_Grid_Record();
       }
       catch (Exception ex)
       {
           Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
       }
    }
}
