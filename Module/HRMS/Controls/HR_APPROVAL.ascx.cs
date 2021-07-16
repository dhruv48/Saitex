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
using System.IO;
using DBLibrary;


public partial class Module_HRMS_Controls_HR_APPROVAL : System.Web.UI.UserControl 
{      
     private static  string gradeId = string.Empty;

    SaitexDM.Common.DataModel.HR_APPROVAL_MST oHR_APPROVAL_MST;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
    private static DataTable dtGetgriddata = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GridView2.Visible = false;
                CreateGridDatatable();
                Clear();
                datetime();
                bindcmbemp();
                MaxProid();
                TxtAmtbyhod.Enabled = false;
                //TxtAmtbyHr.Enabled = false;
                TxtApplyDate.Enabled = false;
                TxtHodApprovalDate.Enabled = false;
                // Txthrapprovaldate.Enabled = false;
                TxtNoofInstallments.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    


        
    }
    private void datetime()
    {
        try
        {
            TxtApplyDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
        catch
        {
            throw;
        }
      
    }
    private void bindcmbemp()
    {
        try
        {

            cmbEmpCode.Items.Clear();
           // DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getempcode();
            DataTable dt = SaitexBL.Interface.Method.HR_APPROVAL_MST.GetEmpCode();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbEmpCode.DataValueField = "EMP_CODE";
                cmbEmpCode.DataTextField = "EMPLOYEENAME";
                cmbEmpCode.DataSource = dt;
                cmbEmpCode.DataBind();

            }
        }
        catch
        {
            throw;
        }
    }
    private void bindcmbfind()
    {
        try
        {
            Cmbfind.SelectedIndex = -1;
            cmbEmpCode.Enabled = false;
            xyz.Visible = true;
            Cmbfind.Visible = true;
            Cmbfind.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.HR_APPROVAL_MST.Getfind();
            if (dt != null && dt.Rows.Count > 0)
            {
                Cmbfind.DataValueField = "EMP_CODE";
                Cmbfind.DataTextField = "EMPLOYEENAME";
                Cmbfind.DataSource = dt;
                Cmbfind.DataBind();
            }
        }
        catch 
        {
            throw;
        }
    }
    private void save()
    {
        try
        {
            if (cmbEmpCode.SelectedIndex != -1)
            {
                int iRecordFound = 0;
                oHR_APPROVAL_MST = new SaitexDM.Common.DataModel.HR_APPROVAL_MST();
                oHR_APPROVAL_MST.APPLICATION_NO = Common.CommonFuction.funFixQuotes(TxtApplicationNo.Text.Trim());
                oHR_APPROVAL_MST.EMP_CODE = cmbEmpCode.SelectedValue.ToString().Trim();
                oHR_APPROVAL_MST.AMOUNT_REQUESTED = Common.CommonFuction.funFixQuotes(TxtAmountRequested.Text.Trim());
                oHR_APPROVAL_MST.NO_OF_INSTALLMENTS = Common.CommonFuction.funFixQuotes(TxtNoofInstallments.Text.Trim());
                bool bResult = SaitexBL.Interface.Method.HR_APPROVAL_MST.Insert(oHR_APPROVAL_MST, out iRecordFound);
                if (bResult)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Save');", true);
                    Clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Save');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Pls Select Employee');", true);
            }
           }
        catch
        {
            throw;
        }
       
    }
    private void Delete()
    {
        try
        {
            Cmbfind.SelectedIndex = -1;
            oHR_APPROVAL_MST = new SaitexDM.Common.DataModel.HR_APPROVAL_MST();
            oHR_APPROVAL_MST.EMP_CODE = cmbEmpCode.SelectedValue.ToString().Trim();
            oHR_APPROVAL_MST.APPLICATION_NO = Common.CommonFuction.funFixQuotes(TxtApplicationNo.Text.Trim());
           
            bool bResult = SaitexBL.Interface.Method.HR_APPROVAL_MST.Delete(oHR_APPROVAL_MST);
            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Delete Successfully');", true);
                Clear();

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.! Pls enter valid Emp Code.');", true);
            }
        }
        catch
        {
            throw;
        }
    }
    private void MaxProid()
    {
        try
        {
            string x = string.Empty;
            int y = 0;
            DataTable dt = SaitexBL.Interface.Method.HR_APPROVAL_MST.GetMaxProid();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        x = dv[iLoop]["MAX_ID"].ToString();
                        y = int.Parse(x);
                        y = y + 1;
                        TxtApplicationNo.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void TxtAmtbyhod_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmbEmpCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbEmpCode.SelectedIndex != -1)
            {
                GetData(cmbEmpCode.SelectedValue.ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    private void GetData(string EMP_CODE)
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_APPROVAL_MST.GetData(EMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtdept.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();
                txtdesig.Text = dt.Rows[0]["DESIG_NAME"].ToString().Trim();
                txtbranch.Text = dt.Rows[0]["BRANCH_NAME"].ToString().Trim();
                txtlevel.Text = dt.Rows[0]["EMPLEVEL"].ToString().Trim();
                txtposition.Text = dt.Rows[0]["POSITION_NAME"].ToString().Trim();
                txtgrade.Text = dt.Rows[0]["GRADE_ID"].ToString();           
            }
        }
        catch
        {
            throw;
        }

    }
    private void Clear()
    {
        try
        {
            cmbEmpCode.Enabled = true;
            txtbranch.Text = string.Empty;
            txtdept.Text = string.Empty;
            txtdesig.Text = string.Empty;
            txtgrade.Text = string.Empty;
            txtlevel.Text = string.Empty;
            txtposition.Text = string.Empty;
            cmbEmpCode.SelectedIndex = -1;
            lblMode.Text = "Save";
            txtbranch.Enabled = false;
            txtdept.Enabled = false;
            txtdesig.Enabled = false;
            txtgrade.Enabled = false;
            TxtApplyDate.Enabled = false;
            TxtApplicationNo.Enabled = false;
            txtlevel.Enabled = false;
            txtposition.Enabled = false;
            xyz.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            MaxProid();
            GridDeduction.Visible = false;
            Cmbfind.Visible = false;
            TxtAmountRequested.Text = string.Empty;
            TxtApplyDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbEmpCode.SelectedIndex = -1;
            GridDeduction.Visible = false;
            GridView2.Visible = false;
            TxtNoofInstallments.Text = string.Empty;
            cmbEmpCode.Visible = true;
        
        }
        catch
        {
            throw;
        }
        
    }
    private void Update()
    {
        try
        {
            int iRecordFound = 0;
            oHR_APPROVAL_MST = new SaitexDM.Common.DataModel.HR_APPROVAL_MST();
            oHR_APPROVAL_MST.APPLICATION_NO = Common.CommonFuction.funFixQuotes(TxtApplicationNo.Text.Trim());
            oHR_APPROVAL_MST.EMP_CODE = cmbEmpCode.SelectedValue.ToString().Trim();
            oHR_APPROVAL_MST.AMOUNT_REQUESTED = Common.CommonFuction.funFixQuotes(TxtAmountRequested.Text.Trim());
            oHR_APPROVAL_MST.NO_OF_INSTALLMENTS = Common.CommonFuction.funFixQuotes(TxtNoofInstallments.Text.Trim());
            bool bResult = SaitexBL.Interface.Method.HR_APPROVAL_MST.Update(oHR_APPROVAL_MST, out iRecordFound);
            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Update Successfully');", true);
                Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Update');", true);
            }
        }
        catch
        {
            throw;
        }
        
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }         
    }  
    protected void GetGrid(string EMP_CODE)
    {
        int month = 0;
        try
        {
            DataRow row;
            GridDeduction.Visible = true;
            DataTable dt = SaitexBL.Interface.Method.HR_APPROVAL_MST.GetGrid(EMP_CODE);
            GridDeduction.DataSource = dt;
            GridDeduction.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (EMP_CODE == dt.Rows[0]["EMP_CODE"].ToString().Trim())
                    {   
                        int Amt = int.Parse(dt.Rows[0]["AMOUNT_REQUESTED"].ToString().Trim());
                        int Installments = int.Parse(dt.Rows[0]["NO_OF_INSTALLMENTS"].ToString().Trim());
                        string  Apply_Date= dt.Rows[0]["APPLY_DATE"].ToString().Trim();
                        //DateTime date = Convert.ToDateTime(Apply_Date);
                        DateTime date = DateTime.ParseExact(Apply_Date, "dd/mm/yyyy",System.Globalization.CultureInfo.CreateSpecificCulture("en-AU").DateTimeFormat);
                        int IAmt;
                        int Installment;
                        for (int i = 1; i <= Installments; i++)
                        {
                            IAmt = Amt / Installments ;
                            Installment = i;
                            string Idate = date.Year.ToString();
                            string mdate = string.Format("{0:MMMM}",date.AddMonths(month));
                            row = dtGetgriddata.NewRow();
                            row["Installment_NO"] = i;
                            row["Amount_Deducted"] = IAmt;
                            row["Month"] = mdate;
                            row["Year"] = Idate;
                            month = month + 1;
                            dtGetgriddata.Rows.Add(row);
                        }
                    }
                }         
                    GridView2.DataSource = dtGetgriddata;
                    GridView2.DataBind();
                    GridView2.Visible = false ; 
            }      
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Imgbtnfind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
            bindcmbfind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }   
       
    }
    protected void Cmbfind_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (Cmbfind.SelectedIndex != -1)
            {
                GetGrid(Cmbfind.SelectedValue.ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        { 
            GridView2.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
          CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
        }
    }
    private void CreateGridDatatable()
    {
        try
        {

            dtGetgriddata = new DataTable();
            dtGetgriddata.Columns.Add("Installment_NO", typeof(int));
            dtGetgriddata.Columns.Add("Amount_Deducted", typeof(decimal));
            dtGetgriddata.Columns.Add("Month", typeof(string ));
            dtGetgriddata.Columns.Add("Year", typeof(string ));

            
        }
        catch
        {
            throw;
        }

    }
    protected void GridDeduction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                GridRowdata(Convert.ToInt32(e.CommandArgument));
                ViewState["RecordEdit"] = e.CommandArgument.ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    


       
    }
    private void GridRowdata(int APPLICATION_NO)
    {
      
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_APPROVAL_MST.GetData2();
         
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = " APPLICATION_NO='" + APPLICATION_NO + "'";
                if (dv.Count > 0)
                {
                    tdUpdate.Visible = true;
                    tdDelete.Visible = true;
                    tdSave.Visible = false;
                    lblMode.Text = "Update";
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        cmbEmpCode.SelectedValue = dv[iLoop]["EMP_CODE"].ToString().Trim();                                         
                        GetData(cmbEmpCode.SelectedValue.ToString().Trim());                    
                        TxtAmountRequested.Text = dv[iLoop]["AMOUNT_REQUESTED"].ToString().Trim();
                        TxtApplicationNo.Text = dv[iLoop]["APPLICATION_NO"].ToString().Trim();
                        TxtNoofInstallments.Text = dv[iLoop]["NO_OF_INSTALLMENTS"].ToString().Trim();
                    }
                }       
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
            Update();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    


      
    }
    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                save();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    


        
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
       
        if (Page.IsValid)
        {
            Update();
        }
    }
    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Delete();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }    
       
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click1(object sender, ImageClickEventArgs e)
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

}




