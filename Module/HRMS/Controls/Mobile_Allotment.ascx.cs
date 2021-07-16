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


public partial class Module_HRMS_Controls_Mobile_Allotment : System.Web.UI.UserControl
{
    private static string EMP_CODE = string.Empty;
    private static string DEPT_CODE = string.Empty;
    private static string DESIG_CODE = string.Empty;
    private static string BRANCH_CODE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataView dv;
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
             tdUpdate.Visible = false;
             DataTable  DTMobile = new DataTable();
            DTMobile = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Load_Mobile_Record();
            dv = new DataView(DTMobile);
            fillYear();
            Bind_Grid_Record();
            bindddldepartment();
            BindDesignation();
            Bind_BrachName();
            bindddlemployee();
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
            for (int i = -15; i < 15; i++)
            {
                DDLOpenYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
            }
            DDLOpenYear.Items.Insert(0, new ListItem("---------Select---------", ""));
            DDLOpenYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
        }
        catch
        {
            throw;
        }
    }
    private void bindddlemployee()
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
            ddlemployee.Items.Insert(0, new ListItem("------------Select-----------", string.Empty));
        }
        catch
        {
            throw;
        }
    }
    private void Bind_Grid_Record()
    {
        try
        {
            if (ddldepartment.SelectedValue.ToString() != null && ddldepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddldepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }
            if (DDLDesign.SelectedValue.ToString() != null && DDLDesign.SelectedValue.ToString() != string.Empty)
            {
                DESIG_CODE = DDLDesign.SelectedValue.ToString();
            }
            else
            {
                DESIG_CODE = string.Empty;
            }
            if (DDLBranch.SelectedValue.ToString() != null && DDLBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = DDLBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlemployee.SelectedValue.ToString() != null && ddlemployee.SelectedValue.ToString() != string.Empty)
            {
                EMP_CODE = ddlemployee.SelectedValue.ToString();
            }
            else
            {
                EMP_CODE = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Load_Grid_Record_For_Allot(EMP_CODE, DEPT_CODE, DESIG_CODE, BRANCH_CODE);
            GVTelephoneRecord.DataSource = dt;
            GVTelephoneRecord.DataBind();
        }
        catch
        {
            throw;
        }
    }
    private bool Insert_Record()
    {
        bool Res = false;       
        try
        {
            SaitexDM.Common.DataModel.HR_TELEPHONE_MST TM = new SaitexDM.Common.DataModel.HR_TELEPHONE_MST();
           for (int i = 0; i < GVTelephoneRecord.Rows.Count; i++)
            {
                TM.TELEPHONE_NO  =decimal .Parse(CommonFuction.funFixQuotes(((DropDownList)GVTelephoneRecord.Rows[i].FindControl("DDLMobile")).SelectedValue.Trim().ToString()));
                string MobileLmt = CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtmoblimit")).Text.Trim().ToString());
                if (TM.TELEPHONE_NO != 0 && MobileLmt != string.Empty )
                {
                    TM.TELEPHONE_LIMIT = decimal.Parse(CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtmoblimit")).Text.Trim().ToString()));
                    TM.EMP_CODE = (((Label)GVTelephoneRecord.Rows[i].FindControl("lblempcode")).Text);
                    TM.TERIFF_PLAN = CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtterif")).Text);
                    TM.REMARKS = CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtremarks")).Text);                                    
                    TM.COMP_CODE = oUserLoginDetail.COMP_CODE.Trim().ToString();
                    TM.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
                    TM.TUSER = oUserLoginDetail.UserCode.Trim().ToString();
                    TM.ALLOT_DATE = DateTime.Parse(CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("TxtAllotedDate")).Text).ToString());
                    bool bResult = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Insert_Telephone_Allotment(TM);
                    if (bResult)
                    {
                        Res = true;
                    }
                    else
                    {
                        Res = false;
                        return Res;
                    }
                }                 
            }
            return Res;
        }
        catch
        {
            throw;
        }

    }
    private void bindddldepartment()
    {
        try
        {
            ddldepartment.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldepartment.DataValueField = "DEPT_CODE";
                ddldepartment.DataTextField = "DEPT_NAME";
                ddldepartment.DataSource = dt;
                ddldepartment.DataBind();
            }
            ddldepartment.Items.Insert(0, new ListItem("------------Select-----------", string.Empty));
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
            DDLDesign.DataSource = dt;
            DDLDesign.DataValueField = "DESIG_CODE";
            DDLDesign.DataTextField = "DESIG_NAME";
            DDLDesign.DataBind();
            DDLDesign.Items.Insert(0, new ListItem("------------SELECT------------", ""));
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
            DDLBranch.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    protected void DDLDesign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
    protected void DDLBranch_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void GVTelephoneRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVTelephoneRecord.PageIndex = e.NewPageIndex;
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gridview Paging.\r\nplease see error log"));
        }
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
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
    private void Clear_Control()
    {
        try
        {
            tdSave.Visible = true;
            trFindingRecord.Visible = false;
            Initial_Control();
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
            if (Page.IsValid)
            {
                if (Insert_Record())
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Save Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Problem in saving the record');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
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
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Problem in updating the record');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            trFindingRecord.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in finding record.\r\nSee error log for detail."));
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
    protected void GVTelephoneRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label TELEPHONE_NO = ((Label)e.Row.FindControl("LblMobileID"));
                DropDownList ddl = (DropDownList)e.Row.FindControl("DDLMobile");
                ddl.DataSource = dv;
                ddl.DataTextField = "TELEPHONE_NO";
                ddl.DataValueField = "TELEPHONE_NO";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select..", "0"));
                if (TELEPHONE_NO.Text.Trim().ToString() != string.Empty)
                {
                    ddl.SelectedValue = CommonFuction.funFixQuotes((TELEPHONE_NO.Text.Trim())).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Mobile Dropdown loading.\r\nplease see error log"));
        }
    }
}
