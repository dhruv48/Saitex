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
using System.Collections.Generic;
using errorLog;
using Common;
using DBLibrary;

public partial class Module_HRMS_Controls_HR_PF_ESI_MAST : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Bind_BrachName();                
                Bind_Sub_Head();
                Initial_Control();
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
            DDLBranch.SelectedIndex = -1;
            DDLSubhHead.SelectedIndex = -1;
            TxtEmployerContr.Text = string.Empty;
            TxtBasicSubhHeadLmt.Text = string.Empty;
            TxtDLI.Text = string.Empty;
            TxtDLIAdminCharges.Text = string.Empty;
            TxtEmpContr.Text = string.Empty;
            TxtFromDate.Text = string.Empty;
            TxtSubhHeadLmt.Text = string.Empty;
            TxtTodate.Text = string.Empty;            
            Bind_Sub_Head(DDLSubhHead, "D");
            Load_PF_ESI_Record();
            
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void Bind_BrachName()
    {
        try
        {
            DataTable dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DDLBranch.DataSource = dt;
            DDLBranch.DataValueField = "BRANCH_CODE";
            DDLBranch.DataTextField = "BRANCH_NAME";
            DDLBranch.DataBind();
            DDLBranch.Items.Insert(0, new ListItem("-------Select-------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void Bind_Sub_Head(DropDownList ddl, string Sub_Cat)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SAL_GRD.Get_Sub_Head(Sub_Cat);
            ddl.DataSource = dt;
            ddl.DataValueField = "SUBH_ID";
            ddl.DataTextField = "SUBH_NAME";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-------Select-------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void Bind_Sub_Head()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SAL_GRD.Get_Sub_Head("A");
            ChkCBLDEPDSubHead.DataSource = dt;
            ChkCBLDEPDSubHead.DataValueField = "SUBH_ID";
            ChkCBLDEPDSubHead.DataTextField = "SUBH_NAME";
            ChkCBLDEPDSubHead.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch(Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Insert_Record())
            {
                Common.CommonFuction.ShowMessage("Record Insert Sucessfully");
                Initial_Control();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in Saving Record");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Record.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Insert_Record())
            {
                Common.CommonFuction.ShowMessage("Record Update Sucessfully");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in Updating Record");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Record.\r\nSee error log for detail."));
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
                Response.Redirect("~/Saitex/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Exit Form"));
        }

    }
    private bool Insert_Record()
    {
        bool Result = false;
        try
        {
            if (DDLSubhHead.SelectedIndex > 0 && DDLBranch.SelectedIndex > 0)
            {
                SaitexDM.Common.DataModel.HR_PF_ESI_MAST HR = new SaitexDM.Common.DataModel.HR_PF_ESI_MAST();
                HR.COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
                HR.BRANCH_CODE = DDLBranch.SelectedValue.Trim().ToString();
                HR.SUBH_HEAD_ID = DDLSubhHead.SelectedValue.Trim().ToString();
                HR.FROM_DATE = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                HR.TO_DATE = DateTime.Parse(TxtTodate.Text.Trim().ToString());
                HR.EMP_CONTR = decimal.Parse(TxtEmpContr.Text.Trim().ToString());
                HR.EMPLR_CONTR = decimal.Parse(TxtEmployerContr.Text.Trim().ToString());
                HR.BASE_SUBH_HEAD_LMT = decimal.Parse(TxtBasicSubhHeadLmt.Text.Trim().ToString());
                HR.DLI = decimal.Parse(TxtDLI.Text.Trim().ToString());
                HR.DLI_ADMIN_CHRG = decimal.Parse(TxtDLIAdminCharges.Text.Trim().ToString());
                HR.SUBH_HEAD_LMT = decimal.Parse(TxtSubhHeadLmt.Text.Trim().ToString());
                HR.TUSER = oUserLoginDetail.UserCode.ToString();
                ArrayList list = new ArrayList();
                foreach (ListItem li in ChkCBLDEPDSubHead.Items)
                {
                    if (li.Selected == true)
                    {
                        list.Add(li.Value.ToString());
                    }
                }
                Result = SaitexBL.Interface.Method.HR_PF_ESI_MAST.Insert_Record(HR, list);

            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Subh Head & Branch");
            }
            return Result;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void Load_PF_ESI_Record()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_PF_ESI_MAST.Load_PF_ESI_MST("");
            GVPFESIRecord.DataSource = DT;
            GVPFESIRecord.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
   
    protected void GVPFESIRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EmpEdit")
            {
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                GridViewRow row = GVPFESIRecord.Rows[UniqueId];               
                DDLBranch.SelectedValue = Convert.ToString(((Label)row.FindControl("LblBranchCode")).Text);
                DDLSubhHead.SelectedValue = Convert.ToString(((Label)row.FindControl("LblSubhHeadId")).Text);
                TxtFromDate.Text = Convert.ToString(((Label)row.FindControl("LblFromDate")).Text);
                TxtTodate.Text = Convert.ToString(((Label)row.FindControl("LblTODate")).Text);
                TxtBasicSubhHeadLmt.Text = Convert.ToString(((Label)row.FindControl("LblBASE_SUBH_HEAD_LMT")).Text);
                TxtDLI.Text = Convert.ToString(((Label)row.FindControl("LblDLI")).Text);
                TxtDLIAdminCharges.Text = Convert.ToString(((Label)row.FindControl("LblDLI_ADMIN_CHRG")).Text);
                TxtEmpContr.Text = Convert.ToString(((Label)row.FindControl("LblEMP_CONTR")).Text);
                TxtEmployerContr.Text = Convert.ToString(((Label)row.FindControl("LblEMPLR_CONTR")).Text);
                TxtSubhHeadLmt.Text = Convert.ToString(((Label)row.FindControl("LblSUBH_HEAD_LMT")).Text);
                Load_Subh_Head(Convert.ToString(((Label)row.FindControl("LblSubhHeadId")).Text), Convert.ToString(((Label)row.FindControl("LblBranchCode")).Text), Convert.ToString(((Label)row.FindControl("LblComp_Code")).Text));
            }
            else if (e.CommandName == "EmpDelete")
            {

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void Load_Subh_Head(string SUBH_ID,string BRANCH,string COMP)
    {
        try
        {
            Bind_Sub_Head();
            DataTable dt = SaitexBL.Interface.Method.HR_PF_ESI_MAST.Load_PF_ESI_TRN(SUBH_ID, BRANCH, COMP);
            foreach (DataRow DR in dt.Rows)
            {
                ListItem Lst = ChkCBLDEPDSubHead.Items.FindByValue(DR["DEPD_SUBH_HEAD_ID"].ToString());
                if (Lst != null)
                {
                    Lst.Selected = true;
                }                
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    
}
