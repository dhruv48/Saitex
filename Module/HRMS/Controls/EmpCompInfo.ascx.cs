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

using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_EmpCompInfo : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string empCode;
    public static string strCompanyCode = string.Empty;
    public static string strBranchCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
                strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
                if (!IsPostBack)
                {
                    if (Request.QueryString["EMP_CODE"] != null && Request.QueryString["EMP_CODE"] != "")
                    {
                        empCode = Request.QueryString["EMP_CODE"].ToString();
                       lblEmp_id.Text  = empCode;
                        getRecord();
                    }
                    else
                    {
                        lblMode.Text = "Save";
                    }                    
                    bindBankName();                    
                }               
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (InsertData())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved fail!try again');", true);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }    
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (UpdateData())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated fail!try again');", true);
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = "Problem in updating";
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (DeleteData())
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted fail!try again');", true);
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = "Problem in deleting the record";
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }      
    protected DataTable GetItems(string text, int startOffset, int numberOfItems, bool Flag)
    {
        if (Flag)
        {
            string whereClause = " WHERE BANK_CODE like :SearchQuery And DEL_STATUS = '0' or BANK_NAME like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY BANK_CODE";
            string commandText = "SELECT * FROM  hr_bank_mst";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%',"","", sPO);
            return dt;
        }
        else
        {
            string whereClause = " WHERE EMP_CODE like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0' or F_NAME like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY EMP_CODE";
            string commandText = "SELECT * FROM  hr_emp_mst";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', strCompanyCode, strBranchCode, sPO);
            return dt;
        }
    }
    protected int GetItemsCount(string text, bool Flag)
    {
        if (Flag)
        {
            string CommandText = "SELECT COUNT(*) FROM hr_bank_mst WHERE BANK_CODE like :SearchQuery And DEL_STATUS = '0' or BANK_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text + '%', "", "", "");
        }
        else
        {
            string CommandText = "SELECT COUNT(*) FROM hr_emp_mst WHERE EMP_CODE like :SearchQuery And DEL_STATUS = '0' or F_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text + '%', strCompanyCode, strBranchCode, "");
        }
    }
    
    private void bindBankName()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.SelectAllBank();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbBankName.DataSource = dt;
                cmbBankName.DataBind();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private bool  InsertData()
    {
        bool bResult = false;
        try
        {
            if (cmbBankName.SelectedText != "" && lblEmp_id.Text.Trim()!="")
            {
                SaitexDM.Common.DataModel.HR_EMP_COMP_INFO oHR_EMP_COMP_INFO = new SaitexDM.Common.DataModel.HR_EMP_COMP_INFO();

                oHR_EMP_COMP_INFO.EMP_ID = lblEmp_id.Text.ToString().Trim();
                oHR_EMP_COMP_INFO.BANK_CODE = cmbBankName.SelectedValue.ToString().Trim();
                oHR_EMP_COMP_INFO.AC_NO = txtACNo.Text.Trim();
                oHR_EMP_COMP_INFO.DL_NO = txtDLNo.Text.Trim();
                oHR_EMP_COMP_INFO.DL_ISS_DT = txtDLIssueDate.Text.Trim();
                oHR_EMP_COMP_INFO.PASSPORT_NO = txtPassportNo.Text.Trim();
                oHR_EMP_COMP_INFO.PASSPORT_ISS_DT = txtPassportIssueDate.Text.Trim();
                oHR_EMP_COMP_INFO.PAN_NO = txtPANNo.Text.Trim();
                oHR_EMP_COMP_INFO.PF_AC_NO = txtPFACNo.Text.Trim();
                oHR_EMP_COMP_INFO.PR_ADD = txtPreAddress.Text.Trim();
                oHR_EMP_COMP_INFO.PR_CITY = txtPreCity.Text.Trim();
                oHR_EMP_COMP_INFO.PR_STATE = txtPreState.Text.Trim();
                oHR_EMP_COMP_INFO.PR_COUNTRY = txtPreCountry.Text.Trim();
                oHR_EMP_COMP_INFO.PR_PIN_NO = txtPrePinNo.Text.Trim();
                oHR_EMP_COMP_INFO.PR_TEL_NO = txtPreTelNo.Text.Trim();
                oHR_EMP_COMP_INFO.PR_FAX_NO = txtPreFAXNo.Text.Trim();
                oHR_EMP_COMP_INFO.PR_EMAIL_ID = txtPreEmailID.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_ADD = txtPermAddress.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_CITY = txtPermCity.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_STATE = txtPermState.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_COUNTRY = txtPermCountry.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_PIN_NO = txtPermPinNo.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_TEL_NO = txtPermTelNo.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_FAX_NO = txtPermFAXNo.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_EMAIL_ID = txtPermEmailID.Text.Trim();
                oHR_EMP_COMP_INFO.INSURANCE = txtInsuranceNo.Text.Trim();
                oHR_EMP_COMP_INFO.DISPENSARY = txtDispensary.Text.Trim();
                oHR_EMP_COMP_INFO.STATUS = true;
                oHR_EMP_COMP_INFO.DEL_STATUS = false;
                oHR_EMP_COMP_INFO.TUSER = oUserLoginDetail.UserCode;
                int iRecordFound = 0;
                bResult = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.Insert(oHR_EMP_COMP_INFO, out iRecordFound);               
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please select Bank Name.');", true);

            }
            return bResult;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private bool  UpdateData()
    {
        bool bResult = false;
        try
        {
            if (cmbBankName.SelectedText != "")
            {
                SaitexDM.Common.DataModel.HR_EMP_COMP_INFO oHR_EMP_COMP_INFO = new SaitexDM.Common.DataModel.HR_EMP_COMP_INFO();

                oHR_EMP_COMP_INFO.EMP_ID = lblEmp_id.Text.ToString().Trim();
                oHR_EMP_COMP_INFO.BANK_CODE = cmbBankName.SelectedValue.ToString().ToUpper().Trim();
                oHR_EMP_COMP_INFO.AC_NO = txtACNo.Text.ToUpper().Trim();
                oHR_EMP_COMP_INFO.DL_NO = txtDLNo.Text.ToUpper().Trim();
                oHR_EMP_COMP_INFO.DL_ISS_DT = txtDLIssueDate.Text.Trim();
                oHR_EMP_COMP_INFO.PASSPORT_NO = txtPassportNo.Text.ToUpper().Trim();
                oHR_EMP_COMP_INFO.PASSPORT_ISS_DT = txtPassportIssueDate.Text.Trim();
                oHR_EMP_COMP_INFO.PAN_NO = txtPANNo.Text.ToUpper().Trim();
                oHR_EMP_COMP_INFO.PF_AC_NO = txtPFACNo.Text.ToUpper().Trim();
                oHR_EMP_COMP_INFO.PR_ADD = txtPreAddress.Text.Trim();
                oHR_EMP_COMP_INFO.PR_CITY = txtPreCity.Text.Trim();
                oHR_EMP_COMP_INFO.PR_STATE = txtPreState.Text.Trim();
                oHR_EMP_COMP_INFO.PR_COUNTRY = txtPreCountry.Text.Trim();
                oHR_EMP_COMP_INFO.PR_PIN_NO = txtPrePinNo.Text.Trim();
                oHR_EMP_COMP_INFO.PR_TEL_NO = txtPreTelNo.Text.Trim();
                oHR_EMP_COMP_INFO.PR_FAX_NO = txtPreFAXNo.Text.Trim();
                oHR_EMP_COMP_INFO.PR_EMAIL_ID = txtPreEmailID.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_ADD = txtPermAddress.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_CITY = txtPermCity.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_STATE = txtPermState.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_COUNTRY = txtPermCountry.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_PIN_NO = txtPermPinNo.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_TEL_NO = txtPermTelNo.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_FAX_NO = txtPermFAXNo.Text.Trim();
                oHR_EMP_COMP_INFO.PERM_EMAIL_ID = txtPermEmailID.Text.Trim();
                oHR_EMP_COMP_INFO.INSURANCE = txtInsuranceNo.Text.Trim();
                oHR_EMP_COMP_INFO.DISPENSARY = txtDispensary.Text.Trim();
                oHR_EMP_COMP_INFO.STATUS = true;
                oHR_EMP_COMP_INFO.DEL_STATUS = false;
                oHR_EMP_COMP_INFO.TUSER = oUserLoginDetail.UserCode;
                bResult = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.Update(oHR_EMP_COMP_INFO);               
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please select Bank Name.');", true);
            }
            return bResult;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private bool  DeleteData()
    {
        bool bResult = false;
        try
        {
            SaitexDM.Common.DataModel.HR_EMP_COMP_INFO oHR_EMP_COMP_INFO = new SaitexDM.Common.DataModel.HR_EMP_COMP_INFO();
            oHR_EMP_COMP_INFO.EMP_ID = lblEmp_id.Text .ToString().Trim();
            bResult = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.Delete(oHR_EMP_COMP_INFO);
            return bResult;          
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void getRecord()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_EMP_COMP_INFO oHR_EMP_COMP_INFO = new SaitexDM.Common.DataModel.HR_EMP_COMP_INFO();

            oHR_EMP_COMP_INFO.EMP_ID = empCode.ToUpper().Trim();

            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.Select(oHR_EMP_COMP_INFO);

            if (dt != null && dt.Rows.Count > 0)
            {
                tdUpdate.Visible = true;
                tdSave.Visible = false;
                DataView dv = new DataView(dt);

                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        TxtEmpname.Text = dv[iLoop]["EMPLOYEENAME"].ToString().Trim();
                        cmbBankName.SelectedValue = dv[iLoop]["BANK_CODE"].ToString().Trim();
                        txtACNo.Text = dv[iLoop]["AC_NO"].ToString();
                        txtDLNo.Text = dv[iLoop]["DL_NO"].ToString();
                        txtDLIssueDate.Text = dv[iLoop]["DL_ISS_DT"].ToString();
                        txtPassportNo.Text = dv[iLoop]["PASSPORT_NO"].ToString();
                        txtPassportIssueDate.Text = dv[iLoop]["PASSPORT_ISS_DT"].ToString();
                        txtPANNo.Text = dv[iLoop]["PAN_NO"].ToString();
                        txtPFACNo.Text = dv[iLoop]["PF_AC_NO"].ToString();
                        txtInsuranceNo.Text = dv[iLoop]["INSURANCE"].ToString();
                        txtDispensary.Text = dv[iLoop]["DISPENSARY"].ToString();
                        txtPreAddress.Text = dv[iLoop]["PR_ADD"].ToString();
                        txtPreCity.Text = dv[iLoop]["PR_CITY"].ToString();
                        txtPreState.Text = dv[iLoop]["PR_STATE"].ToString();
                        txtPreCountry.Text = dv[iLoop]["PR_COUNTRY"].ToString();
                        txtPrePinNo.Text = dv[iLoop]["PR_PIN_NO"].ToString();
                        txtPreTelNo.Text = dv[iLoop]["PR_TEL_NO"].ToString();
                        txtPreFAXNo.Text = dv[iLoop]["PR_FAX_NO"].ToString();
                        txtPreEmailID.Text = dv[iLoop]["PR_EMAIL_ID"].ToString();
                        txtPermAddress.Text = dv[iLoop]["PERM_ADD"].ToString();
                        txtPermCity.Text = dv[iLoop]["PERM_CITY"].ToString();
                        txtPermState.Text = dv[iLoop]["PERM_STATE"].ToString();
                        txtPermCountry.Text = dv[iLoop]["PERM_COUNTRY"].ToString();
                        txtPermPinNo.Text = dv[iLoop]["PERM_PIN_NO"].ToString();
                        txtPermTelNo.Text = dv[iLoop]["PERM_TEL_NO"].ToString();
                        txtPermFAXNo.Text = dv[iLoop]["PERM_FAX_NO"].ToString();
                        txtPermEmailID.Text = dv[iLoop]["PERM_EMAIL_ID"].ToString();
                    }
                }
            }
            else
            {
                tdUpdate.Visible = false;
                tdSave.Visible = true;
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void chkSameAddress_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkSameAddress.Checked == true)
            {
                txtPermAddress.Text = txtPreAddress.Text;
                txtPermCity.Text = txtPreCity.Text;
                txtPermState.Text = txtPreState.Text;
                txtPermCountry.Text = txtPreCountry.Text;
                txtPermPinNo.Text = txtPrePinNo.Text;
                txtPermTelNo.Text = txtPreTelNo.Text;
                txtPermFAXNo.Text = txtPreFAXNo.Text;
                txtPermEmailID.Text = txtPreEmailID.Text;
            }
            else
            {
                txtPermAddress.Text = "";
                txtPermCity.Text = "";
                txtPermState.Text = "";
                txtPermCountry.Text = "";
                txtPermPinNo.Text = "";
                txtPermTelNo.Text = "";
                txtPermFAXNo.Text = "";
                txtPermEmailID.Text = "";
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void cmbBankName_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10, true);

        cmbBankName.Items.Clear();
        cmbBankName.DataSource = data;
        cmbBankName.DataBind();
        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text, true);
    }  
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnDelete.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to delete this record')");
        imgbtnSave.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to save this record')");
        imgbtnUpdate.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to update this record')");
    }
}
