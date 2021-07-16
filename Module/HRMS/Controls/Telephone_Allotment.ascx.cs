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
using DBLibrary;
using Common;
using SaitexBL;
using SaitexDM;
using errorLog;

public partial class Module_HRMS_Controls_Telephone_Allotment : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable DTTRNRecord = null;
    private static string EMPName = string.Empty;
    private static string DEPT_NAME = string.Empty;
    private static string DESINATION = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Initial_Control();               
                Load_Employee_Record();               
                CreateTRNDetailTable();
                Load_Telephone();
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
            Load_Telephone();
            DDLEmployee.SelectedIndex = -1;
            DDLMobile.SelectedIndex = -1;
            TxtAllotmentDate.Text = System.DateTime.Now.Date.ToShortDateString();
            TxtMobileLimit .Text = string.Empty;
            TxtREmarks.Text = string.Empty;
            TxtTeriffPlane.Text = string.Empty;
            TxtAccountNo.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }
    private void Load_Telephone()
    {
        try
        {
            DataTable DTMobile = new DataTable();
            DTMobile = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Load_Mobile_Record();
            DDLMobile.DataSource = DTMobile;
            DDLEmployee.DataValueField = "TELEPHONE_NO";
            DDLEmployee.DataTextField = "TELEPHONE_NO";
            DDLMobile.DataBind();
            DDLMobile.Items.Insert(0, "----SELECT----");
        }
        catch
        {
            throw;
        }
    }
    private void Load_Employee_Record()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_MST.Load_Employee_Detail(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            DDLEmployee.DataSource = DT;
            DDLEmployee.DataValueField = "EMP_CODE";
            DDLEmployee.DataTextField = "EMPLOYEENAME";
            DDLEmployee.DataBind();
            DDLEmployee.Items.Insert(0, "------------SELECT--------------");
        }
        catch
        {
            throw;
        }
    }
    private void CreateTRNDetailTable()
    {
        try
        {
            DTTRNRecord = new DataTable();
            DTTRNRecord.Columns.Add("UniqueId", typeof(int));
            DTTRNRecord.Columns.Add("EMP_CODE", typeof(string));
            DTTRNRecord.Columns.Add("EMPLOYEENAME", typeof(string));
            DTTRNRecord.Columns.Add("DEPARTMENT", typeof(string));
            DTTRNRecord.Columns.Add("DESIGINATION", typeof(string));
            DTTRNRecord.Columns.Add("TELEPHONE_NO", typeof(string));
            DTTRNRecord.Columns.Add("TERIFF_PLAN", typeof(string));
            DTTRNRecord.Columns.Add("TELEPHONE_LIMIT", typeof(string));
            DTTRNRecord.Columns.Add("ALLOT_DATE", typeof(string));
            DTTRNRecord.Columns.Add("ACCOUNT_NO", typeof(double));
            DTTRNRecord.Columns.Add("REMARKS", typeof(string));
        }
        catch
        {
            throw;
        }
    }
    protected void DDLEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_Telephone_Allotment_Details();            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in finding Record"));
        }
    }
    private void Load_Telephone_Allotment_Details()
    {   
        try    
        {
            if (DDLEmployee.SelectedIndex != 0)
            {
                DTTRNRecord.Rows.Clear();
                DataTable DTable = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Load_Alloted_Record(DDLEmployee.SelectedValue.Trim().ToString());
                DEPT_NAME = DTable.Rows[0]["DEPARTMENT"].ToString();
                DESINATION = DTable.Rows[0]["DESIGINATION"].ToString();
                if (DTable.Rows[0]["TELEPHONE_NO"].ToString() != string.Empty)
                {                    
                    foreach (DataRow Drow in DTable.Rows)
                    {
                        tdUpdate.Visible = true;
                        lblMode.Text = "Update";
                        tdSave.Visible = false;
                        DataRow dr;
                        dr = DTTRNRecord.NewRow();
                        dr["UniqueId"] = DTTRNRecord.Rows.Count + 1;
                        dr["EMP_CODE"] = Drow["EMP_CODE"];
                        dr["EMPLOYEENAME"] = Drow["EMPLOYEENAME"].ToString();
                        dr["DEPARTMENT"] = Drow["DEPARTMENT"].ToString();
                        dr["DESIGINATION"] = Drow["DESIGINATION"].ToString();                        
                        dr["TELEPHONE_NO"] = Drow["TELEPHONE_NO"].ToString();
                        dr["TERIFF_PLAN"] = Drow["TERIFF_PLAN"].ToString();
                        dr["TELEPHONE_LIMIT"] = Drow["TELEPHONE_LIMIT"].ToString();
                        dr["ALLOT_DATE"] = Drow["ALLOT_DATE"].ToString();
                        dr["ACCOUNT_NO"] = Drow["ACCOUNT_NO"].ToString();
                        dr["REMARKS"] = Drow["REMARKS"].ToString();
                        DTTRNRecord.Rows.Add(dr);
                    } 
                }
                GridViewTelephoneTRN.DataSource = DTTRNRecord;
                GridViewTelephoneTRN.DataBind();
            }           
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
                Load_Telephone_Allotment_Details();
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
        {   SaitexDM.Common.DataModel.HR_TELEPHONE_MST TM=new SaitexDM.Common.DataModel.HR_TELEPHONE_MST();
            if (DTTRNRecord.Rows.Count >= 0)
            {
                TM.COMP_CODE=oUserLoginDetail.COMP_CODE;
                TM.BRANCH_CODE=oUserLoginDetail.CH_BRANCHCODE;
                TM.TUSER = oUserLoginDetail.UserCode;
                Res = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Insert_Telephone_Allotments(DTTRNRecord,TM );
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
                Load_Telephone_Allotment_Details();
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
            Load_Telephone_Allotment_Details();
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
    protected void GridViewTelephoneTRN_RowCommand(object sender, GridViewCommandEventArgs e)
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
            DataView dv = new DataView(DTTRNRecord);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                ViewState["UniqueId"] = UniqueId;                

                DDLEmployee.SelectedValue = dv[0]["EMP_CODE"].ToString();
                DDLMobile.SelectedValue = dv[0]["TELEPHONE_NO"].ToString();
                TxtAllotmentDate.Text = dv[0]["ALLOT_DATE"].ToString();
                TxtMobileLimit.Text = dv[0]["TELEPHONE_LIMIT"].ToString();
                TxtREmarks.Text = dv[0]["REMARKS"].ToString();
                TxtTeriffPlane.Text = dv[0]["TERIFF_PLAN"].ToString();
                TxtAccountNo.Text = dv[0]["ACCOUNT_NO"].ToString();
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
            if (GridViewTelephoneTRN.Rows.Count == 1)
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
            if (GridViewTelephoneTRN.Rows.Count > 0)
            {
                DTTRNRecord.Rows.Clear();
                foreach (GridViewRow grdRow in GridViewTelephoneTRN.Rows)
                {

                    Label LblEmp_Code = (Label)grdRow.FindControl("LblEmp_Code");
                    Label LblEmpName = (Label)grdRow.FindControl("LblEmpName");
                    //Label LblDepartment = (Label)grdRow.FindControl("LblDepartment");
                    //Label LblDesignation = (Label)grdRow.FindControl("LblDesignation");
                    Label LblTelephoneNo = (Label)grdRow.FindControl("LblTelephoneNo");
                    Label  LblTariffPlan = (Label )grdRow.FindControl("LblTariffPlan");
                    Label  LblMobileLimit = (Label )grdRow.FindControl("LblMobileLimit");
                    Label  LblAccountNo = (Label )grdRow.FindControl("LblAccountNo");
                    Label  LblAllotedDate = (Label)grdRow.FindControl("LblAllotedDate");
                    Label  LblRemarks = (Label)grdRow.FindControl("LblRemarks");


                    if (LblEmp_Code.Text.Trim() != "" && LblTelephoneNo.Text != "" && LblAllotedDate.Text != "" )
                    {                        
                            DataRow dr = DTTRNRecord.NewRow();
                            dr["UniqueId"] = DTTRNRecord.Rows.Count + 1;
                            dr["EMP_CODE"] = LblEmp_Code.Text.Trim();
                            dr["EMPLOYEENAME"] = LblEmpName.Text;
                            dr["DEPARTMENT"] = DEPT_NAME.ToString();
                            dr["DESIGINATION"] = DESINATION.ToString();
                            dr["TELEPHONE_NO"] = LblTelephoneNo.Text.Trim();
                            dr["TERIFF_PLAN"] = LblTariffPlan.Text.Trim();
                            dr["TELEPHONE_LIMIT"] = LblMobileLimit.Text.Trim();
                            dr["ALLOT_DATE"] = LblAllotedDate.Text.Trim();
                            dr["ACCOUNT_NO"] = LblAccountNo .Text.Trim();
                            dr["REMARKS"] = LblRemarks .Text.Trim();
                            DTTRNRecord.Rows.Add(dr);                       
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
    private bool SearchItemCodeInGrid(string Telephone, string EMP_CODE, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in GridViewTelephoneTRN.Rows)
            {
                Label LblTelephoneNo = (Label)grdRow.FindControl("LblTelephoneNo");
                Label LblEmp_Code = (Label)grdRow.FindControl("lblempcode");
                LinkButton lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (LblTelephoneNo.Text.Trim() == Telephone && EMP_CODE == LblEmp_Code.Text.Trim().ToString() && UniqueId != iUniqueId)
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
            if ( DDLEmployee.SelectedIndex != 0 && DDLMobile.SelectedIndex != 0 )
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                bool bb = SearchItemCodeInGrid(DDLMobile.SelectedValue.Trim(), DDLEmployee.SelectedValue.Trim().ToString(), UniqueId);
                if (!bb)
                {
                    
                        if (UniqueId > 0)
                        {
                            DataView dv = new DataView(DTTRNRecord);
                            dv.RowFilter = "UniqueId=" + UniqueId;
                            if (dv.Count > 0)
                            {
                                dv[0]["EMP_CODE"] = DDLEmployee.SelectedValue.Trim();
                                dv[0]["EMPLOYEENAME"] = DDLEmployee.SelectedItem.Text.Trim();
                                dv[0]["DEPARTMENT"] = DEPT_NAME.ToString();
                                dv[0]["DESIGINATION"] = DESINATION.ToString();
                                dv[0]["TELEPHONE_NO"] = DDLMobile.SelectedValue.Trim().ToString();
                                dv[0]["TERIFF_PLAN"] = TxtTeriffPlane.Text.Trim();
                                dv[0]["TELEPHONE_LIMIT"] = Math.Round(decimal.Parse(TxtMobileLimit.Text.Trim().ToString()), 2);
                                dv[0]["ALLOT_DATE"] = DateTime.Parse(TxtAllotmentDate.Text.Trim().ToString()).ToShortDateString();
                                dv[0]["ACCOUNT_NO"] = TxtAccountNo.Text.Trim();
                                dv[0]["REMARKS"] = TxtREmarks.Text.Trim();
                                DTTRNRecord.AcceptChanges();
                            }
                        }
                        else
                        {
                            DataRow dr = DTTRNRecord.NewRow();
                            dr["UniqueId"] = DTTRNRecord.Rows.Count + 1;
                            dr["EMP_CODE"] = DDLEmployee.SelectedValue.Trim();
                            dr["EMPLOYEENAME"] = DDLEmployee.SelectedItem.Text.Trim();
                            dr["DEPARTMENT"] = DEPT_NAME.ToString();
                            dr["DESIGINATION"] = DESINATION.ToString();
                            dr["TELEPHONE_NO"] = DDLMobile.SelectedValue.Trim().ToString();
                            dr["TERIFF_PLAN"] = TxtTeriffPlane .Text.Trim();
                            dr["TELEPHONE_LIMIT"] =Math.Round(decimal.Parse(TxtMobileLimit.Text.Trim().ToString()),2);
                            dr["ALLOT_DATE"] = DateTime.Parse(TxtAllotmentDate.Text.Trim().ToString()).ToShortDateString();
                            dr["ACCOUNT_NO"] = TxtAccountNo.Text.Trim();
                            dr["REMARKS"] = TxtREmarks.Text.Trim();
                            DTTRNRecord.Rows.Add(dr);
                        }
                        RefreshDetailRow();                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Duplicate Subh_Head_Master Code');", true);
                }
            }
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
            GridViewTelephoneTRN.DataSource = DTTRNRecord;
            GridViewTelephoneTRN.DataBind();                        
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
            DDLEmployee.SelectedIndex = -1;
            DDLMobile.SelectedIndex = -1;
            TxtAllotmentDate.Text = System.DateTime.Now.Date.ToShortDateString();
            TxtMobileLimit.Text = string.Empty;
            TxtREmarks.Text = string.Empty;
            TxtTeriffPlane.Text = string.Empty;
            TxtAccountNo.Text = string.Empty;
            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }
}
