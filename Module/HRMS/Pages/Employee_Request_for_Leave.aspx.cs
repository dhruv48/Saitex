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

public partial class Module_Hrms_Pages_Employee_Request_for_Leave : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    string User_Code = string.Empty;
    string POSITION = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["EmpCode"] != null)
            {
                HR_EMP_MST.EMP_CODE = Session["EmpCode"].ToString();
                User_Code = HR_EMP_MST.EMP_CODE;
                POSITION = Session["POSITION"].ToString();
                if (!Page.IsPostBack)
                {
                    Load_Leave_for_approved();
                }
            }
            else
            {
                Response.Redirect("/Saitex/Module/HRMS/Pages/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Page loading"));
        }
    }
    private void Load_Leave_for_approved()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_LV_APP.Load_HR_LV_APP_Form_Record(User_Code, POSITION, "1");
            gvReportDisplayGrid.DataSource = DTable;
            gvReportDisplayGrid.DataBind();

        }   
        catch 
        {
            throw;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Check Change"));
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
        catch 
        {
            throw;
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
