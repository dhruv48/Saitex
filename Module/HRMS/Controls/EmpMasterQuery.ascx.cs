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
using Obout.ComboBox;


public partial class Module_HRMS_Controls_EmpMasterQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    //SaitexDM.Common.DataModel.EX_EmployeeMaster odtn;
    //List<SaitexDM.Common.DataModel.EX_EmployeeMaster>dtImage;
   private static  DataTable DTable,dt1,DTable1;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Load_Control();
                //ViewState["dtImage"]=
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Page Loading"));
        }
    }
    public void Load_Control()
    { bindDepartment();
      bindBranch();
      bindDesignation();
      bindShift();
      bindEmployee();
      bindCadderCode();
      
    }

   
   private void bindCadderCode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.Cadder_Code();
            DDLCader.DataSource = dt;
            DDLCader.DataValueField = "CADDER_CODE";
            DDLCader.DataTextField = "CADDER_CODE";
            DDLCader.DataBind();
            DDLCader.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void bindDepartment()
    {
        try
        {
            DataTable td = new DataTable();
            td = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            DDLDepartment.DataSource = td;
            DDLDepartment.DataValueField = "DEPT_CODE";
            DDLDepartment.DataTextField = "DEPT_NAME";
            DDLDepartment.DataBind();
            DDLDepartment.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            td.Dispose();
            td = null;
        }
        catch
        { throw; }
    }
    private void bindBranch()
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
            DDLBranch.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            dt.Dispose(); dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void bindDesignation()
    {
        try
        {
            
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            DDLDesigination.DataSource = dt;
            DDLDesigination.DataValueField = "desig_Code";
            DDLDesigination.DataTextField = "desig_Name";
            DDLDesigination.DataBind();
            DDLDesigination.Items.Insert(0, new ListItem("---------SELECT---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void bindEmployee()
    {
        try
        {

            DDLEmployee.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getempcode();

            if (dt != null && dt.Rows.Count > 0)
            {
                DDLEmployee.DataValueField = "EMP_CODE";
                DDLEmployee.DataTextField = "EMPLOYEENAME";
                DDLEmployee.DataSource = dt;
                DDLEmployee.DataBind();
            }
            DDLEmployee.Items.Insert(0, new ListItem("---------Select--------", string.Empty));
        }
        catch
        {
            throw;
        }
    }
    private void bindShift()
    {
       try
        {
            
            DataTable dt=SaitexBL.Interface.Method.EmployeeMaster.getgetEmployeeShift();
            //dt = SaitexBL.Interface.Method.HR_SFT_MST.SelectShiftName();
            DDLShift.DataSource = dt;
            DDLShift.DataValueField = "SFT_ID";
            DDLShift.DataTextField = "SFT_NAME";
            
            DDLShift.DataBind();
            DDLShift.Items.Insert(0, new ListItem("---------SELECT---------", string.Empty));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DDLBranch.SelectedValue = "";
            DDLCader.SelectedValue = "";
            DDLDepartment.SelectedValue = "";
            DDLDesigination.SelectedValue = "";
            DDLEmployee.SelectedValue = "";
            DDLShift.SelectedValue = "";
            tdClear.Visible = false;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
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
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string URL = "../pages/EmpMaster_OPT.aspx";
            string URL = "../reports/HR_EMP_MST_REPORT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    
    
    public void viewonclick()
    {
        DataTable dt;
        dt1 = new DataTable();
        string Department = string.Empty;
        string Designation = string.Empty;
        string Branch = string.Empty;
        string Employee = string.Empty;
        string Cader = string.Empty;
        string Shift = string.Empty;

        if (DDLDepartment.SelectedValue.ToString() != null && DDLDepartment.SelectedValue.ToString() != string.Empty)
        {
            Department = DDLDepartment.SelectedValue.ToString(); 
        }
        if (DDLDesigination.SelectedValue.ToString() != null && DDLDesigination.SelectedValue.ToString() != string.Empty)
        {
            Designation = DDLDesigination.SelectedValue.Trim().ToString(); 
        }
        if (DDLBranch.SelectedValue.ToString() != null && DDLBranch.SelectedValue.ToString() != string.Empty)
        {
            Branch = DDLBranch.SelectedValue.Trim().ToString();

        }
        if (DDLEmployee.SelectedValue.ToString() != null && DDLEmployee.SelectedValue.ToString() != string.Empty)
        {
            Employee=DDLEmployee.SelectedValue.Trim().ToString() ;
        }
        if (DDLCader.SelectedValue.ToString() != null && DDLCader.SelectedValue.ToString() != string.Empty)
        {
            Cader = DDLCader.SelectedValue.Trim().ToString();
        }
        if (DDLShift.SelectedValue.ToString() != null && DDLShift.SelectedValue.ToString() != string.Empty)
        {
            Shift = DDLShift.SelectedValue.Trim().ToString();
        }
        else {
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.GetAllEmp();
         //   txtTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            ViewState["dtimage"] = dt;
        }
        
        dt1 = SaitexBL.Interface.Method.EmployeeMaster.GetEmployee_Record(Department,Designation,Branch,Employee,Cader,Shift);
        //DTable = new DataTable();
        //DTable = SaitexBL.Interface.Method.EmployeeMaster.GetEmpCompanyInfo(Department, Designation, Employee, Cader, Shift, Branch);
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            grdpnl1.DataSource = dt1;
          //  txtTotalRecord.Text = dt1.Rows.Count.ToString();
            grdpnl1.DataBind();
            tdClear.Visible = true;
        }
        else
        {
            grdpnl1.DataSource = null;
            grdpnl1.DataBind();
            tdClear.Visible = false;
        }
        
    }
    protected void grdpnl1_PageChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdpnl1.PageIndex = e.NewPageIndex;
            viewonclick();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Gridview Page Index Changes"));
        }
    }
    protected void Grid_dataBouund(object sender, GridViewRowEventArgs e)
    {
         try
        {

            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable DTable2, DTable3, DTable4, DTable5,dtImage;
                LinkButton EMP_CODE = (LinkButton)e.Row.FindControl("EMP_CODE");
                string emp_code = EMP_CODE.Text;
                Image imgDesignImage = (Image)e.Row.FindControl("imgDesignImage");
                //dtImage = (DataTable)ViewState["dtImage"];
                DTable = new DataTable();
                DTable = SaitexBL.Interface.Method.EmployeeMaster.GetEmpCompanyInfo(emp_code);
                DTable1 = new DataTable();
                DTable1 = SaitexBL.Interface.Method.EmployeeMaster.GetMedicalDetail(emp_code);
                DTable2 = new DataTable();
                DTable2 = SaitexBL.Interface.Method.EmployeeMaster.GetLeaveDetail(emp_code);
                DTable3 = new DataTable();
                DTable3 = SaitexBL.Interface.Method.EmployeeMaster.GetSalDetail(emp_code);
                DTable4 = new DataTable();
                DTable4 = SaitexBL.Interface.Method.EmployeeMaster.GetFamilyDetail(emp_code);
                DTable5 = new DataTable();
                DTable5 = SaitexBL.Interface.Method.EmployeeMaster.GetQual(emp_code);
                LinkButton Qualification = (LinkButton)e.Row.FindControl("Qualification");
                LinkButton Family_Detail = (LinkButton)e.Row.FindControl("Family_Detail");
                LinkButton Salary_Detail = (LinkButton)e.Row.FindControl("Salary_Detail");
                LinkButton LeaveDetail = (LinkButton)e.Row.FindControl("LeaveAssignDetail");
                LinkButton Company_info = (LinkButton)e.Row.FindControl("Company_info");
                LinkButton Medical_Detail = (LinkButton)e.Row.FindControl("Medical_Detail");
                string E_QUAL = Qualification.CommandArgument;
                string ECode = Medical_Detail.CommandArgument;
                string E_CODE =Company_info.CommandArgument;
                string eLeavecode = LeaveDetail.CommandArgument;
                string E_sal = Salary_Detail.CommandArgument;
                string E_FAL = Family_Detail.CommandArgument;
                dtImage = SaitexBL.Interface.Method.EmployeeMaster.GetImg();

                imgDesignImage.ImageUrl = @"~/CommonImages/ImageResizer/No_Image.jpg";
                if (dtImage != null && dtImage.Rows.Count > 0)
                {
                    DataView dvImage = new DataView(dtImage);
                    dvImage.RowFilter = "EMP_CODE=" + emp_code;

                    if (dvImage.Count > 0)
                    {
                        byte[] SUB_IMG1 = dvImage[0]["SUB_IMG"] as byte[];
                        if (SUB_IMG1 != null)
                        {
                            MemoryStream ms = new MemoryStream(dvImage[0]["SUB_IMG"] as byte[]);
                            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                            string myUniqueFileName = string.Format(@"{0}.jpg", Guid.NewGuid());

                            returnImage.Save(Server.MapPath(@"~/CommonImages/ImageResizer/temp/" + myUniqueFileName));
                            imgDesignImage.ImageUrl = @"~/CommonImages/ImageResizer/temp/" + myUniqueFileName;
                        }
                        else
                        {
                            imgDesignImage.ImageUrl = @"~/CommonImages/ImageResizer/No_Image.jpg";
                        }
                    }
                }

                if (DTable!= null && DTable.Rows.Count > 0)
                {
                    DataView dv = new DataView(DTable);

                    dv.RowFilter = "EMP_CODE=" + E_CODE;
                    if (dv.Count > 0)
                    {
                      GridView grdpnl2 = e.Row.FindControl("grdpnl2") as GridView;
                      grdpnl2.DataSource = DTable;
                      grdpnl2.DataBind();
                            

                       }
                }
              
                if (DTable1 != null && DTable1.Rows.Count > 0)
                {
                    DataView dv1 = new DataView(DTable1);

                   
                    
                        GridView grdpnl3 = e.Row.FindControl("grdpnl3") as GridView;
                        grdpnl3.DataSource = DTable1;
                        grdpnl3.DataBind();


                 }
               
                     
                if (DTable2 != null && DTable2.Rows.Count > 0)
                {
                    DataView dv2 = new DataView(DTable2);

                    GridView grdpnl4 = e.Row.FindControl("grdpnl4") as GridView;
                    grdpnl4.DataSource = DTable2;
                    grdpnl4.DataBind();


                    
                }
                if (DTable3 != null && DTable3.Rows.Count > 0)
                {
                    DataView dv3 = new DataView(DTable3);

                    
                    GridView grdpnl5 = e.Row.FindControl("grdpnl5") as GridView;
                    grdpnl5.DataSource = DTable3;
                    grdpnl5.DataBind();


                }
                if (DTable4 != null && DTable4.Rows.Count > 0)
                {
                    DataView dv4 = new DataView(DTable4);

                    GridView grdpnl6 = e.Row.FindControl("grdpnl6") as GridView;
                    grdpnl6.DataSource = DTable4;
                    grdpnl6.DataBind();


                    
                }
                if (DTable5 != null && DTable5.Rows.Count > 0)
                {
                    DataView dv5 = new DataView(DTable5);

                   
                    GridView grdpnl7 = e.Row.FindControl("grdpnl7") as GridView;
                    grdpnl7.DataSource = DTable5;
                    grdpnl7.DataBind();


                    
                }


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Material GridRow DataBound.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }
    public void createTable1(string Department,string Designation,string Branch,string Employee,string Cader,string Shift)
    {
        try
        {
            
            DTable = new DataTable();
            DTable.Columns.Add(new DataColumn("EMP_CODE", typeof(string)));
            DTable.Columns.Add(new DataColumn("EMP_COMP_INFO", typeof(int)));
            DTable.Columns.Add(new DataColumn("AC_NO", typeof(string)));
            DTable.Columns.Add(new DataColumn("PASSPORT_NO", typeof(int)));
            DTable.Columns.Add(new DataColumn("PAN_NO", typeof(string)));
            DTable.Columns.Add(new DataColumn("PF_AC_NO", typeof(string)));
            DTable.Columns.Add(new DataColumn("PR_ADD", typeof(string)));
            DTable.Columns.Add(new DataColumn("PR_CITY", typeof(string)));
            DTable.Columns.Add(new DataColumn("PR_STATE", typeof(string)));
            DTable.Columns.Add(new DataColumn("PR_PIN_NO", typeof(string)));
            DTable.Columns.Add(new DataColumn("BANK_CODE", typeof(string)));
           
            
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }


    protected void CmdViewRecord_Click(object sender, EventArgs e)
    {
        viewonclick();
       
    }
}
