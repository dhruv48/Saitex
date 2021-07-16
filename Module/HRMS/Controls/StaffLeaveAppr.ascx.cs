using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using errorLog;
using Common;
using DBLibrary;

public partial class Module_HRMS_Controls_StaffLeaveAppr : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    string User_Code = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            HR_EMP_MST.EMP_CODE = oUserLoginDetail.UserCode.ToString();
            User_Code = HR_EMP_MST.EMP_CODE;
            if (!Page.IsPostBack)
            {
                Load_Leave_for_approved();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
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
    private void Load_Leave_for_approved()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_LV_APP.Load_HR_LV_APP_Form_Record("", "", "");
            gvReportDisplayGrid.DataSource = DTable;
            gvReportDisplayGrid.DataBind();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }
    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReportDisplayGrid.PageIndex = e.NewPageIndex;
            Load_Leave_for_approved();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in grid view page index change"));
        }
    }
    protected void gvReportDisplayGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ChkAll_CheckedChanged(object sender, System.EventArgs e)
    {
        try
        {
            CheckBox Ctl = (CheckBox)gvReportDisplayGrid.HeaderRow.FindControl("ChkAll");
            bool chkFlag = false;
            if (Ctl.Checked)
            {
                chkFlag = true;
            }
            foreach (GridViewRow dr in gvReportDisplayGrid.Rows)
            {
                CheckBox Chk = (CheckBox)dr.FindControl("ChkSelect");
                Chk.Checked = chkFlag;
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private string Read_DocIDs()
    {
        try
        {
            string DocIDs = "";
            foreach (GridViewRow dr in gvReportDisplayGrid.Rows)
            {
                CheckBox Chk = (CheckBox)dr.FindControl("ChkSelect");
                if (Chk.Checked)
                {
                    if (DocIDs.Trim() != string.Empty)
                    {
                        DocIDs = DocIDs + "','" + gvReportDisplayGrid.DataKeys[dr.RowIndex].Value;
                    }
                    else
                    {
                        DocIDs = gvReportDisplayGrid.DataKeys[dr.RowIndex].Value.ToString();
                    }
                }
            }
            return DocIDs;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void CmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Lv_Id = Read_DocIDs();
            if (DDLStatus.SelectedValue != "0" && Lv_Id != string.Empty)
            {
                bool Result = SaitexBL.Interface.Method.HR_LV_APP.Change_Leave_Status(Lv_Id, Convert.ToChar(DDLStatus.SelectedValue.ToString()));
                if (Result)
                {
                    Common.CommonFuction.ShowMessage("Records update successfully");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("unable to update");
                }
                Load_Leave_for_approved();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Status or Leave");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Saving Record"));
        }
    }
}