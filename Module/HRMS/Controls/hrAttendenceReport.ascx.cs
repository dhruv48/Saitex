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
public partial class Module_HRMS_Controls_hrAttendenceReport : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                bindddlempcode();
                bindddlbranch();
                bindddldesignation();
                bindddlcompany();
                bindddldepartment();
                bindGridAttendence();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Load.\r\nSee error log for detail."));
        }
       
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
    private void bindddlcompany()
    {
        ddlcompany.Items.Clear();
        DataTable dt = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlcompany.DataValueField = "COMP_CODE";
            ddlcompany.DataTextField = "COMP_NAME";
            ddlcompany.DataSource = dt;
            ddlcompany.DataBind();

        }
        ddlcompany.Items.Insert(0, new ListItem("---------Select----------", ""));
    }
    private void bindddldepartment()
    {
     try
        {
         
            ddldept.Items.Clear();
           DataTable dt = SaitexBL.Interface .Method .CM_DEPT_MST .Select();  
           if (dt != null && dt.Rows.Count > 0)
            {
                ddldept.DataValueField = "DEPT_CODE";
                ddldept.DataTextField = "DEPT_NAME";
                ddldept.DataSource = dt;
                ddldept.DataBind();

            }
            ddldept.Items.Insert(0, new ListItem("---------Select----------", ""));

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
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    protected void ddlbranchcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }


    }
    protected void ddldesig_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    protected void txtformdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bindGridAttendence();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    private void bindGridAttendence()
    {
        try
        {
            string EMP_CODE = string.Empty;
            string DEPT_CODE = string.Empty;
            string DESIG_CODE = string.Empty;
            string COMP_CODE = string.Empty;
            string MONTH = string.Empty;
            string BRANCH_CODE = string.Empty;
            string ATN = string.Empty;
            string FromDate = string.Empty;
            string ToDate = string.Empty;
           
            if (ddlemployee.SelectedValue.ToString() != null && ddlemployee.SelectedValue.ToString() != string.Empty)
            {
                EMP_CODE = ddlemployee.SelectedValue.ToString();
            }
            else
            {
                EMP_CODE = string.Empty;
            }


            if (ddldept.SelectedValue.ToString() != null && ddldept.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddldept.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }

            if (ddldesig.SelectedValue.ToString() != null && ddldesig.SelectedValue.ToString() != string.Empty)
            {
                DESIG_CODE = ddldesig.SelectedValue.ToString();
            }
            else
            {
                DESIG_CODE = string.Empty;
            }
            
            if (ddldept.SelectedValue.ToString() != null && ddldept.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddldept.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }

            if (ddlcompany.SelectedValue.ToString() != null && ddlcompany.SelectedValue.ToString() != string.Empty)
            {
                COMP_CODE = ddlcompany.SelectedValue.ToString();
            }
            else
            {
                COMP_CODE = string.Empty;
            }
            if (ddlbranchcode.SelectedValue.ToString() != null && ddlbranchcode.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE  = ddlbranchcode.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }


            if (ddlmonth.SelectedValue.ToString() != null && ddlmonth.SelectedValue.ToString() != string.Empty && ddlmonth.SelectedValue.ToString() != "SELECT")
            {
                MONTH = ddlmonth.SelectedValue.ToString();
            }
            else
            {
                MONTH = string.Empty;
            }

            if (DropDownList1.SelectedValue.ToString() != null && DropDownList1.SelectedValue.ToString() != string.Empty && DropDownList1.SelectedValue.ToString()!= "SELECT")
            {
                ATN = DropDownList1.SelectedValue.ToString();
            }
            else
            {
                ATN = string.Empty;
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
            DataTable DT = SaitexBL.Interface.Method.HR_ATTN_TRN.GetAttenreport(EMP_CODE, DEPT_CODE, DESIG_CODE, COMP_CODE, BRANCH_CODE, MONTH, ATN, FromDate, ToDate);
            if (DT != null && DT.Rows.Count > 0)
            {
                GridAttendence.DataSource = DT;
                GridAttendence.DataBind();

            }
            else
            {

                GridAttendence.DataSource = null;
                GridAttendence.DataBind();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
            }
        }
        catch
        {
            throw;
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }
}
