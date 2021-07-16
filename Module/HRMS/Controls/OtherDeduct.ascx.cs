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

using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_OtherDeduct : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string empCode;
    public static string strCompanyCode = string.Empty;
    public static string strBranchCode = string.Empty;
    public static string OpenYear = string.Empty;
    public static string OpenMonth = string.Empty;

    
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
                    OpenYear = oUserLoginDetail.OPEN_YEAR.Trim().ToString();
                    OpenMonth = oUserLoginDetail.OPEN_MONTH.Trim().ToString();
                    lblMode.Text = "Save";                  
                    bindEmpCode();                     
                }
                
            }
            else
            {
                Response.Redirect("/Saitex/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Page Load"));      
        }
    }
    private void load_Record(string EmpCode)
    {
        try
        {            
            SaitexDM.Common.DataModel.EMP_OTHER_DEDUCT EOD=new SaitexDM.Common.DataModel.EMP_OTHER_DEDUCT();
            EOD.YEAR=int.Parse (OpenYear);
            EOD.EMP_CODE = EmpCode.Trim().ToString();
            EOD.MONTH = OpenMonth.Trim().ToString();
            DataTable dt = SaitexBL.Interface.Method.EMP_OTHER_DEDUCT.Get_Deduct(EOD);            
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    GVDeduction.DataSource = dt;
                    GVDeduction.DataBind();
                }
            
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
            if (Save_Deduction())
            {
                Common.CommonFuction.ShowMessage("Record Save Update");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to Update!try Again");
            }                 
           
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Updating Record"));
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("./EMPOtherCharges.aspx", false);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private bool Save_Deduction()
    {
        bool Res = false;
         try
         {
             if (Page.IsValid)
                {                       
                SaitexDM.Common.DataModel.EMP_OTHER_DEDUCT EOD = new SaitexDM.Common.DataModel.EMP_OTHER_DEDUCT();
                EOD.EMP_CODE = empCode;
                EOD.YEAR = int.Parse(OpenYear);
                EOD.MONTH = OpenMonth;
                foreach (GridViewRow Drow in GVDeduction.Rows)
                {
                    TextBox TxtAmount = (TextBox)Drow.FindControl("TxtAmount");
                    Label LblDeduction = (Label)Drow.FindControl("LblDeduction");
                    EOD.AMOUNT = decimal.Parse(TxtAmount.Text.Trim().ToString());
                    EOD.DEDUCTION=LblDeduction.Text.Trim().ToString();
                    Res = SaitexBL.Interface.Method.EMP_OTHER_DEDUCT.Save_Deduction(EOD);
                }
                return Res;            
                }
             else
             {
                 return false;
             }
         }
        catch (Exception ex)
         {

             Common.CommonFuction.ShowMessage(ex.Message.ToString());
             return false;
         }        
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems, bool Flag)
    {
        try
        {
            string whereClause = " WHERE EMP_CODE like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0' or F_NAME like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY EMP_CODE";
            string commandText = "SELECT * FROM  hr_emp_mst";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', strCompanyCode, strBranchCode, sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemsCount(string text, bool Flag)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM hr_emp_mst WHERE EMP_CODE like :SearchQuery And DEL_STATUS = '0' or F_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_EMP_COMP_INFO.GetCountForLOV(CommandText, text + '%', strCompanyCode, strBranchCode, "");
        }
        catch
        {
            throw;
        }
    }
    private void bindEmpCode()
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems("", 0, 10, false);
            cmbEmpCode.DataSource = data;
            cmbEmpCode.DataBind();
        }
        catch (Exception ex)
        {           
            throw ex;
        }
    }
    protected void cmbEmpCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            empCode = cmbEmpCode.SelectedValue.ToString().Trim();
            load_Record(empCode);
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in selecting Record"));
        }      
    }
    protected void cmbEmpCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10, false);

            cmbEmpCode.Items.Clear();
            cmbEmpCode.DataSource = data;
            cmbEmpCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text, false);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Loading Record"));
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
        imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
        imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
        imgbtnUpdate.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to update this record')");
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in exit Page"));
        }
    }
}
