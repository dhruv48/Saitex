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
using DBLibrary;
using Common;
using SaitexBL;
using SaitexDM;
using errorLog;

public partial class Module_HRMS_Controls_Employe_Variable_trn : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private  DataTable DTTRNRecord;
    private  string EMPName = string.Empty;
    private  double   FinalTotal;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                if (Convert.ToString(oUserLoginDetail.OPEN_MONTH) != null && Convert.ToString(oUserLoginDetail.OPEN_YEAR) != null)
                {
                    TxtMonthName.Text = oUserLoginDetail.OPEN_MONTH.ToString();
                    TxtYear.Text = oUserLoginDetail.OPEN_YEAR.ToString();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("No Open Year Or open Month,Please Open Year or Month");
                }  
                CreateTRNDetailTable();
                Initial_Control();                
                Load_Salary_Head_Record();
                Load_Salary_SubHead_Record("");
                
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
        }
    }
    private void Initial_Control()
    {
        try
        {
           
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            if (Convert.ToString(oUserLoginDetail.OPEN_MONTH) != null && Convert.ToString(oUserLoginDetail.OPEN_YEAR) != null)
            {
                TxtMonthName.Text = oUserLoginDetail.OPEN_MONTH.ToString();
                TxtYear.Text = oUserLoginDetail.OPEN_YEAR.ToString();
            }
            else
            {                
                TxtYear.Text = System.DateTime.Now.Year.ToString();
            }              
            DDLSalryHeadMaster.SelectedIndex = -1;
            DDLSalrySubHeadMaster.SelectedIndex = -1;
            TxtAmount.Text = string.Empty;
            TxtDepartment.Text = string.Empty;
            TxtRemarks.Text = string.Empty;           
          
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
    private void Load_Month_Transaction()
    {
        string SearchQuery = string.Empty;
       
        try
        {
            SearchQuery = " LTRIM(RTRIM(T.TRN_YEAR))='" + TxtYear.Text.Trim().ToString() + "' AND LTRIM(RTRIM(T.TRN_MONTH))='" + oUserLoginDetail.OPEN_MONTH_NO.ToString() + "'";
                        
            if (DDLSalryHeadMaster.SelectedIndex >0)
            {
                if (SearchQuery != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(T.HEAD_ID))='" + DDLSalryHeadMaster.SelectedValue.Trim().ToString() + "'";
                }
                else
                {
                    SearchQuery = " LTRIM(RTRIM(T.HEAD_ID))='" + DDLSalryHeadMaster.SelectedValue.Trim().ToString() + "'";
                }
            }
            if (DDLSalrySubHeadMaster .SelectedIndex > 0)
            {
                if (SearchQuery != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND LTRIM(RTRIM(T.SUBH_HEAD_ID))='" + DDLSalrySubHeadMaster.SelectedValue.Trim().ToString() + "'";
                }
                else
                {
                    SearchQuery = " LTRIM(RTRIM(T.SUBH_HEAD_ID))='" + DDLSalrySubHeadMaster.SelectedValue.Trim().ToString() + "'";
                }
            }
            if (SearchQuery != string.Empty)
            {
                if (ViewState["DTTRNRecord"] != null)
                    DTTRNRecord = (DataTable)ViewState["DTTRNRecord"];
                if (DTTRNRecord == null || DTTRNRecord.Rows.Count == 0)
                    CreateTRNDetailTable();
                DTTRNRecord.Rows.Clear();
                DataTable DTable = SaitexBL.Interface.Method.HR_EMP_VARIABLE_TRN.Load_Transaction_Record(SearchQuery);
                foreach (DataRow Drow in DTable.Rows)
                {
                    tdUpdate.Visible = true;
                    lblMode.Text = "Update";
                    tdSave.Visible = false;
                    DataRow dr;
                    dr = DTTRNRecord.NewRow();
                    dr["UniqueId"] = DTTRNRecord.Rows.Count + 1;
                    dr["EMP_CODE"] = Drow["EMP_CODE"];
                    dr["EMPLOYEE"] = Drow["EMPLOYEE"].ToString();
                    dr["DEPARTMENT"] = Drow["DEPARTMENT"].ToString();
                    dr["HEAD_ID"] = Drow["HEAD_ID"].ToString();
                    dr["SUBH_HEAD_ID"] = Drow["SUBH_HEAD_ID"].ToString();
                    dr["TRN_AMOUNT"] = Drow["TRN_AMOUNT"].ToString();

                    dr["HEAD_NAME"] = Drow["HEAD_NAME"].ToString();
                    dr["SUBH_NAME"] = Drow["SUBH_NAME"].ToString();

                    dr["TRN_YEAR"] = Drow["TRN_YEAR"].ToString();
                    dr["TRN_MONTH"] = Drow["TRN_MONTH"].ToString();

                    dr["TRN_REMARKS"] = Drow["TRN_REMARKS"].ToString();
                    DTTRNRecord.Rows.Add(dr);
                }
                GridViewVariableTRN.DataSource = DTTRNRecord;
                GridViewVariableTRN.DataBind();
             
            }
        }
        catch 
        {
            throw; 
        }
    }
    private void Load_Salary_Head_Record()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_VARIABLE_TRN.Load_Salary_Head_Record();
            DDLSalryHeadMaster.DataSource = DT;
            DDLSalryHeadMaster.DataValueField = "HEAD_ID";
            DDLSalryHeadMaster.DataTextField = "HEAD_NAME";
            DDLSalryHeadMaster.DataBind();
            DDLSalryHeadMaster.Items.Insert(0, "------------SELECT--------------");
        }
        catch 
        {
            throw;
        }
    }
    
    protected void DDLSalryHeadMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLSalrySubHeadMaster.SelectedValue != "0")
            {
                Load_Salary_SubHead_Record(DDLSalryHeadMaster.SelectedValue.Trim().ToString());
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting subhead.\r\nSee error log for detail."));
        }
    }
    private void Load_Salary_SubHead_Record(string Head_ID)
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_VARIABLE_TRN.Load_Salary_SubHead_Record(Head_ID);
            DDLSalrySubHeadMaster.DataSource = DT;
            DDLSalrySubHeadMaster.DataValueField = "SUBH_ID";
            DDLSalrySubHeadMaster.DataTextField = "SUBH_NAME";
            DDLSalrySubHeadMaster.DataBind();
            DDLSalrySubHeadMaster.Items.Insert(0, "------------SELECT--------------");
        }
        catch 
        {
            throw;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Insert_Record())
            {
                Common.CommonFuction.ShowMessage("Record Insert Sucesfully");
                Load_Month_Transaction();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in record inserting");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record Saving.\r\nSee error log for detail."));
        }
    }

    private bool Insert_Record()
    {       
        bool Res = false;
        try
        {
            if (ViewState["DTTRNRecord"] != null)
                DTTRNRecord = (DataTable)ViewState["DTTRNRecord"];
            SaitexDM.Common.DataModel.HR_EMP_VARIABLE_TRN TM = new SaitexDM.Common.DataModel.HR_EMP_VARIABLE_TRN();
            if (DTTRNRecord.Rows.Count >=0 && DDLSalryHeadMaster.SelectedValue.Trim().ToString() != "0" && DDLSalrySubHeadMaster.SelectedValue.Trim().ToString() != "0")
            {
                TM.TUSER = oUserLoginDetail.UserCode.Trim().ToString();
                TM.TRN_YEAR = TxtYear.Text.Trim().ToString();
                TM.TRN_MONTH = oUserLoginDetail.OPEN_MONTH_NO.Trim().ToString();
                TM.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                TM.COMP_CODE = oUserLoginDetail.COMP_CODE;
                TM.HEAD_ID = DDLSalryHeadMaster.SelectedValue.Trim().ToString();
                TM.SUBH_HEAD_ID = DDLSalrySubHeadMaster.SelectedValue.Trim().ToString();
                Res = SaitexBL.Interface.Method.HR_EMP_VARIABLE_TRN.Insert_Variable_Tran(DTTRNRecord, TM);
                
            }
            return Res;
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
            if (Insert_Record())
            {
                Common.CommonFuction.ShowMessage("Record Update Sucesfully");
                Load_Month_Transaction();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in record updating");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Record.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Rercord Printing.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DTTRNRecord.Clear();
            Initial_Control();
            Load_Month_Transaction();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Control.\r\nSee error log for detail."));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Exit.\\r\\nSee error log for detail."));
        }
    }
    private void CreateTRNDetailTable()
    {
        try
        {
            DTTRNRecord = new DataTable();
            DTTRNRecord.Columns.Add("UniqueId", typeof(int));
            DTTRNRecord.Columns.Add("EMP_CODE", typeof(string ));
            DTTRNRecord.Columns.Add("EMPLOYEE", typeof(string));
            DTTRNRecord.Columns.Add("DEPARTMENT", typeof(string));
            DTTRNRecord.Columns.Add("HEAD_ID", typeof(string ));
            DTTRNRecord.Columns.Add("SUBH_HEAD_ID", typeof(string));
            DTTRNRecord.Columns.Add("HEAD_NAME", typeof(string));
            DTTRNRecord.Columns.Add("SUBH_NAME", typeof(string));
            DTTRNRecord.Columns.Add("TRN_MONTH", typeof(string));
            DTTRNRecord.Columns.Add("TRN_YEAR", typeof(string));
            DTTRNRecord.Columns.Add("TRN_AMOUNT", typeof(double));
            DTTRNRecord.Columns.Add("TRN_REMARKS", typeof(string));
            ViewState["DTTRNRecord"] = DTTRNRecord;
        }
        catch
        {
            throw;
        }
    }

    protected void GridViewVariableTRN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EmpEdit")
            {
                FillDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "EmpDelete")
            {
                DeleteTRNDetailRow(UniqueId);
                BindTRnDetailGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing/ deletion.\r\nSee error log for detail."));
        }
    }

    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            if (ViewState["DTTRNRecord"] != null)
                DTTRNRecord = (DataTable)ViewState["DTTRNRecord"];
            DataView dv = new DataView(DTTRNRecord);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                ViewState["UniqueId"] = UniqueId;
                DDLEmployee.SelectedValue = dv[0]["EMP_CODE"].ToString();
                TxtEmpCode.Text = dv[0]["EMP_CODE"].ToString();
                TxtAmount.Text = dv[0]["TRN_AMOUNT"].ToString();
                TxtDepartment.Text = dv[0]["DEPARTMENT"].ToString();
                TxtRemarks.Text = dv[0]["TRN_REMARKS"].ToString();
                DDLSalrySubHeadMaster.SelectedValue = dv[0]["SUBH_HEAD_ID"].ToString();
                DDLSalryHeadMaster.SelectedValue = dv[0]["HEAD_ID"].ToString();  
            }
        }
        catch
        {
            throw;
        }
    }
    private void DeleteTRNDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["DTTRNRecord"] != null)
                DTTRNRecord = (DataTable)ViewState["DTTRNRecord"];
            if (GridViewVariableTRN.Rows.Count == 1)
            {
                DTTRNRecord.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in DTTRNRecord.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        DTTRNRecord.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in DTTRNRecord.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
                ViewState["DTTRNRecord"] = DTTRNRecord;
            }
        }
        catch
        {
            throw;
        }
    }

    private bool FillDataTableByGrid()
    {
        try
        {
            bool result = true;
            if (GridViewVariableTRN.Rows.Count > 0)
            {
                DTTRNRecord.Rows.Clear();               
                foreach (GridViewRow grdRow in GridViewVariableTRN.Rows)
                {
                   
                    Label LblEmp_Code = (Label)grdRow.FindControl("LblEmp_Code");
                    Label LblEmpName = (Label)grdRow.FindControl("LblEmpName");
                    Label LblDepartment = (Label)grdRow.FindControl("LblDepartment");
                    Label LblAmount = (Label)grdRow.FindControl("LblAmount");
                    Label LblRemark = (Label)grdRow.FindControl("LblRemark");
                    Label LblHead_ID = (Label)grdRow.FindControl("LblHead_ID");

                    Label LblHeadMaster = (Label)grdRow.FindControl("LblHeadMaster");
                    Label LblSubHeadMaster = (Label)grdRow.FindControl("LblSubHeadMaster");

                    Label  LblSubhHeadID = (Label)grdRow.FindControl("LblSubhHeadID");

                    if (LblEmp_Code.Text.Trim() != "" && LblAmount.Text != "" && LblHead_ID.Text != "" && LblSubhHeadID.Text != "")
                    {
                      decimal Amt=  decimal.Parse(LblAmount.Text.Trim());
                      if (Amt > 0)
                        {
                            DataRow dr = DTTRNRecord.NewRow();
                            dr["UniqueId"] = DTTRNRecord.Rows.Count + 1;
                            dr["EMP_CODE"] = LblEmp_Code.Text.Trim();
                            dr["EMPLOYEE"] = LblEmpName.Text;
                            dr["DEPARTMENT"] = LblDepartment.Text.Trim();
                            dr["HEAD_ID"] = LblHead_ID.Text.Trim();
                            dr["SUBH_HEAD_ID"] = LblSubhHeadID.Text.Trim();

                            dr["HEAD_NAME"] = LblHeadMaster.Text.Trim();
                            dr["SUBH_NAME"] = LblSubHeadMaster.Text.Trim();

                            dr["TRN_AMOUNT"] = decimal.Parse(LblAmount.Text.Trim());
                            dr["TRN_REMARKS"] = LblRemark .Text.Trim();                            
                            DTTRNRecord.Rows.Add(dr);
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }
        catch
        {
            throw;
        }
    }
    private bool SearchItemCodeInGrid(string Subh_Head_Id,string EMP_CODE, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in GridViewVariableTRN.Rows)
            {
                Label LblSubhHeadID = (Label)grdRow.FindControl("LblSubhHeadID");
                 Label LblEmp_Code = (Label)grdRow.FindControl("LblEmp_Code");
                LinkButton  lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (LblSubhHeadID.Text.Trim() == Subh_Head_Id && EMP_CODE == LblEmp_Code.Text.Trim().ToString() && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["DTTRNRecord"] != null)
                DTTRNRecord = (DataTable)ViewState["DTTRNRecord"];
            if (TxtAmount.Text.Trim() != string.Empty && TxtEmpCode.Text.Trim()  != "" && DDLSalryHeadMaster.SelectedIndex != 0 && DDLSalrySubHeadMaster.SelectedIndex != 0)
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    bool bb = SearchItemCodeInGrid(DDLSalrySubHeadMaster.SelectedValue.Trim(), TxtEmpCode.Text.Trim().ToString(), UniqueId);
                    if (!bb)
                    {
                        decimal Amt =Math.Round(decimal.Parse(TxtAmount.Text.Trim()),2);
                      //if (Amt > 0)
                      //{
                            if (UniqueId > 0)
                            {
                                DataView dv = new DataView(DTTRNRecord);
                                dv.RowFilter = "UniqueId=" + UniqueId;
                                if (dv.Count > 0)
                                {

                                    dv[0]["EMP_CODE"] = TxtEmpCode.Text.Trim();
                                    dv[0]["EMPLOYEE"] = DDLEmployee.SelectedText.ToString();
                                    dv[0]["DEPARTMENT"] = TxtDepartment.Text.Trim();
                                    dv[0]["HEAD_ID"] = DDLSalryHeadMaster.SelectedValue.Trim().ToString();
                                    dv[0]["SUBH_HEAD_ID"] = DDLSalrySubHeadMaster.SelectedValue.Trim ().ToString();

                                    dv[0]["HEAD_NAME"] = DDLSalryHeadMaster.SelectedItem.Text.Trim().ToString();
                                    dv[0]["SUBH_NAME"] = DDLSalrySubHeadMaster.SelectedItem.Text.Trim().ToString();

                                    dv[0]["TRN_AMOUNT"] = Math.Round(decimal.Parse(TxtAmount.Text.Trim()), 2);
                                    dv[0]["TRN_REMARKS"] = TxtRemarks.Text.Trim();                                  
                                  
                                    DTTRNRecord.AcceptChanges();
                                }
                            }
                            else
                            {

                                DataRow dr = DTTRNRecord.NewRow();
                                dr["UniqueId"] = DTTRNRecord.Rows.Count + 1;
                                dr["EMP_CODE"] = TxtEmpCode.Text.Trim();
                                dr["EMPLOYEE"] = DDLEmployee.SelectedText.ToString();
                                dr["DEPARTMENT"] = TxtDepartment.Text.Trim();
                                dr["HEAD_ID"] = DDLSalryHeadMaster.SelectedValue.Trim().ToString();
                                dr["SUBH_HEAD_ID"] = DDLSalrySubHeadMaster.SelectedValue.Trim().ToString();

                                dr["HEAD_NAME"] = DDLSalryHeadMaster.SelectedItem.Text.Trim().ToString();
                                dr["SUBH_NAME"] = DDLSalrySubHeadMaster.SelectedItem.Text.Trim().ToString();

                                dr["TRN_AMOUNT"] = Math.Round(decimal.Parse(TxtAmount.Text.Trim()), 2);
                                dr["TRN_REMARKS"] = TxtRemarks.Text.Trim();  
                                DTTRNRecord.Rows.Add(dr);                                
                            }
                            RefreshDetailRow();
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Amount can not be zero');", true);
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Duplicate Subh_Head_Master Code');", true);
                    }
                }
            ViewState["DTTRNRecord"] = DTTRNRecord;
                BindTRnDetailGrid();                       
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving item detail row.\r\nSee error log for detail."));
        }
    }
    private void BindTRnDetailGrid()
    {
        try
        {
            if (ViewState["DTTRNRecord"] != null)
                DTTRNRecord = (DataTable)ViewState["DTTRNRecord"];
            GridViewVariableTRN.DataSource = DTTRNRecord;
            GridViewVariableTRN.DataBind();
            FinalTotal = 0;
            foreach (GridViewRow row in GridViewVariableTRN.Rows)
            {
                Label txtAmount = (Label)row.FindControl("LblAmount");
                FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
            }
            if (GridViewVariableTRN.Rows.Count > 0)
            {
                Label txtFooterAmount = (Label)GridViewVariableTRN.FooterRow.FindControl("LblFooterAmount");
                txtFooterAmount.Text = String.Format("{0:0.00}", FinalTotal.ToString());
            }            
            AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
            lblAmountInWords.Text = oRupeesToWord.changeNumericToWords(FinalTotal);
        }
        catch
        {
            throw;
        }
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearing item detail row.\r\nSee error log for detail."));
        }
    }
    private void RefreshDetailRow()
    {
        try
        {
            DDLEmployee .SelectedIndex = -1;
            TxtEmpCode.Text = string.Empty;
            TxtAmount.Text = string.Empty;
            TxtDepartment.Text = string.Empty;
            TxtRemarks.Text = string.Empty;
            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }
    protected void DDLSalrySubHeadMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_Month_Transaction();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Record.\r\nSee error log for detail."));
        }
    }
    protected void DDLEmployee_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(DDLEmployee.SelectedValue.Trim().ToString());
            EMPName = DT.Rows[0]["EMPLOYEENAME"].ToString();
            TxtEmpCode.Text = DDLEmployee.SelectedValue.Trim().ToString();
            TxtDepartment.Text = DT.Rows[0]["DEPT_NAME"].ToString();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Employee Record.\r\nSee error log for detail."));
        }
    }
    protected void CmdViewRecord_Click(object sender, EventArgs e)
    {
        try
        {
            Load_Month_Transaction();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Record.\r\nSee error log for detail."));
        }
    }
}
