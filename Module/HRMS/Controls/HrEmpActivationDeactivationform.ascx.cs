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
using errorLog;
using Common;

public partial class Module_HRMS_Controls_HrEmpActivationDeactivationform : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.HR_EMP_MST oHR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {
                GetGriddata();
                bindddlempcode();
                bindddldesignation();
                bindddlbranch();
            }
        }
        catch
        {
            throw;
        }
       
    
    }
    private void GetGriddata()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();

            string EMP_CODE = string.Empty;
            string DEPT_CODE = string.Empty;
            string DESIG_CODE = string.Empty;
            string str = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            string branch = string.Empty;
            if (ddlemployee.SelectedValue.ToString() != null && ddlemployee.SelectedValue.ToString() != string.Empty)
            {
                EMP_CODE = ddlemployee.SelectedValue.ToString();
            }
            else
            {
                EMP_CODE = string.Empty;
            }


            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty && ddlDepartment.SelectedValue.ToString() != "SELECT")
            {
                DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }


            if (ddldesig.SelectedValue.ToString() != null && ddldesig.SelectedValue.ToString() != string.Empty && ddldesig.SelectedValue.ToString() != "SELECT")
            {
                DESIG_CODE = ddldesig.SelectedValue.ToString();
            }
            else
            {
                DESIG_CODE = string.Empty;
            }


            if (ddlstatus.SelectedValue.ToString() != null && ddlstatus.SelectedValue.ToString() != string.Empty && ddlstatus.SelectedValue.ToString() != "---Select-----")
            {
                str = ddlstatus.SelectedValue.ToString();
            }
            else
            {
                str = string.Empty;
            }

            if (ddlbranchcode.SelectedValue.ToString() != null && ddlbranchcode.SelectedValue.ToString() != string.Empty )
            {
                branch = ddlbranchcode.SelectedValue.ToString();
            }
            else
            {
                branch = string.Empty;
            }

            if (txtformdate.Text.ToString() != null && txtformdate.Text.ToString() != string.Empty)
            {
                FromDate = txtformdate.Text.ToString();

            }
            else
            {

                FromDate = oUserLoginDetail.DT_STARTDATE.ToShortDateString();  
            }

            if (txtTodate.Text.ToString() != null && txtTodate.Text.ToString() != string.Empty)
            {
                ToDate = txtTodate.Text.ToString();

            }
            else
            {
                ToDate = System.DateTime.Now.Date.ToShortDateString();

            }
           // DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.HrActivationreport(EMP_CODE, DEPT_CODE, DESIG_CODE, str);
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.GetEmpdata(EMP_CODE, DEPT_CODE, DESIG_CODE, str ,branch ,FromDate ,ToDate);

            if (dt != null && dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
            } 
        }
        catch
        {
            throw;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string EMP_CODE = string.Empty;
            string DEPT_CODE = string.Empty;
            string DESIG_CODE = string.Empty;
            string str = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            string branch = string.Empty;
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.GetEmpdata(EMP_CODE, DEPT_CODE, DESIG_CODE,str,branch ,FromDate ,ToDate);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                int id = int.Parse(e.CommandArgument.ToString()); 
                string Emp_code = e.CommandArgument.ToString();
                DataView dv = new DataView(dt);
                dv.RowFilter = "EMP_CODE='" + Emp_code + "'";
                if (dv != null && dv.Count > 0)
                {      
                   
                    if (dv[0]["DEL_STATUS"].ToString().Trim() == "0")
                     {
                         DeActive(Emp_code);
                     }
                    if (dv[0]["DEL_STATUS"].ToString().Trim() == "1")
                    {
                         GetActive(Emp_code);
                    }
                  }      

                }
             }
        catch
        {
            throw;
        }

    }     
    private void DeActive(string  Emp_code)
    {
        try
        {

            bool result = SaitexBL.Interface.Method.HR_EMP_MST.Active_DeActive(Emp_code,"1");
        
        if (result)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('DEACTIVE Successfully');", true);
            GetGriddata();
        }
        }
        catch
        {
            throw;
        }
    }
    private void GetActive(string Emp_code)
    {
        try
        {

            bool result = SaitexBL.Interface.Method.HR_EMP_MST.Active_DeActive(Emp_code,"0");
            if (result)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Active Successfully');", true);
                GetGriddata();
            }
        }
        catch
        {
            throw;
        }
    }   
    
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string EMP_CODE = string.Empty;
            string DEPT_CODE = string.Empty;
            string DESIG_CODE = string.Empty;
            string str = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
            string branch = string.Empty;

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "EMP_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DEPT_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "DESIG_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "str";
            myDataTable.Columns.Add(myDataColumn);

          
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "branch";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "FromDate";

            myDataTable.Columns.Add(myDataColumn);
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ToDate";
            myDataTable.Columns.Add(myDataColumn);

            DataRow row;

            row = myDataTable.NewRow();
            row["EMP_CODE"] = ddlemployee.SelectedValue.ToString();

            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty && ddlDepartment.SelectedValue.ToString() != "SELECT")
            {
                row["DEPT_CODE"] = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                row["DEPT_CODE"] = string.Empty;
            }
            
           
            row["DESIG_CODE"] = ddldesig.SelectedValue.ToString();

            if (ddlstatus.SelectedValue.ToString() != null && ddlstatus.SelectedValue.ToString() != string.Empty && ddlstatus.SelectedValue.ToString() != "---Select-----")
            {
                row["str"] = ddlstatus.SelectedValue.ToString();
            }
            else
            {
                row["str"] = string.Empty;
            }
           
            row["branch"] = ddlbranchcode.SelectedValue.ToString();
          
            if (txtformdate.Text.ToString() != null && txtformdate.Text.ToString() != string.Empty)
            {
                row["FromDate"] = txtformdate.Text.ToString();

            }
            else
            {
                row["FromDate"] = oUserLoginDetail.DT_STARTDATE.ToShortDateString(); ;
            }

            if (txtTodate.Text.ToString() != null && txtTodate.Text.ToString() != string.Empty)
            {
                row["ToDate"] = txtTodate.Text.ToString();

            }
            else
            {
                row["ToDate"] = System.DateTime.Now.Date.ToShortDateString();

            }
            myDataTable.Rows.Add(row);
            Session["Activereport"] = myDataTable;
            Response.Redirect("~/Module/HRMS/Reports/hractivationdeactivationreport.aspx", false);
           
        
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }         
    
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void bindddlempcode()
    {
        try
        {

            ddlemployee.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getempcode();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlemployee.DataValueField = "EMP_CODE";
                ddlemployee.DataTextField = "EMPLOYEENAME";
                ddlemployee.DataSource = dt;
                ddlemployee.DataBind();

            }

            ddlemployee.Items.Insert(0, new ListItem("---------Select----------", ""));

        }
        catch
        {
            throw;
        }
    }
    private void bindddlbranch()
    {
        try
        {
            ddlbranchcode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlbranchcode.DataValueField = "BRANCH_CODE";
                ddlbranchcode.DataTextField = "BRANCH_NAME";
                ddlbranchcode.DataSource = dt;
                ddlbranchcode.DataBind();

            }

            ddlbranchcode.Items.Insert(0, new ListItem("---------Select----------", ""));

        }
        catch
        {
            throw;
        }
    }
    private void bindddldesignation()
    {
        try
        {
            
            ddldesig.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.GetDesigcode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldesig.DataValueField = "DESIG_CODE";
                ddldesig.DataTextField = "DESIG_NAME";
                ddldesig.DataSource = dt;
                ddldesig.DataBind();

            }
            ddldesig.Items.Insert(0, new ListItem("---------Select----------", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    } 
    
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetGriddata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }      
    protected void CmdView_Click(object sender, EventArgs e)
    {
        try
        {
            GetGriddata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }
}
 
   



