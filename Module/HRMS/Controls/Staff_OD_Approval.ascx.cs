using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using errorLog;
using System.IO;
using DBLibrary;
using System.Data;

public partial class Module_HRMS_Controls_Staff_OD_Approval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DateTime DTFrom;
    private static DateTime DTTo;
    private static string iEmp_Code;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!Page.IsPostBack)
            {               
                Load_OD_for_approved();
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Page Loading"));
        }
    }
    private void Load_OD_for_approved()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.EMPOUTDOORDUTY.Load_ODD_Record_For_Approved("", "", "3");
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
            Load_OD_for_approved();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in gridview page index change"));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Check Changed"));
        }
    }
    private bool Read_DocIDs()
    {
        try
        {
            string DocIDs = "";
            int number = 0;
            bool ReturnResult = false;
            foreach (GridViewRow dr in gvReportDisplayGrid.Rows)
            {
                CheckBox Chk = (CheckBox)dr.FindControl("ChkSelect");
                if (Chk.Checked)
                {
                    Label LblFromDate = (Label)dr.Cells[4].FindControl("lblFromDate");
                    Label LblToDate = (Label)dr.Cells[5].FindControl("lblToDate");
                    Label LblEmpCode = (Label)dr.Cells[10].FindControl("lblEMP_CODE");

                    Label LblShiftID = ((Label)dr.Cells[11].FindControl("LblShiftID"));
                    Label LblSiftInTime = ((Label)dr.Cells[11].FindControl("LblSiftInTime"));
                    Label LblSiftOutTime = ((Label)dr.Cells[11].FindControl("LblSiftOutTime"));

                    iEmp_Code = LblEmpCode.Text.ToString();
                    DTFrom = DateTime.Parse(LblFromDate.Text.ToString());
                    DTTo = DateTime.Parse(LblToDate.Text.ToString());
                    DocIDs = gvReportDisplayGrid.DataKeys[dr.RowIndex].Value.ToString();
                    if (DocIDs.Trim() != string.Empty)
                    {
                        if (Save_Record(DocIDs, DTTo, DTFrom, LblShiftID.Text.Trim().ToString(), LblSiftInTime.Text.Trim().ToString(), LblSiftOutTime.Text.Trim().ToString()))
                        {
                            number = number + 1;
                        }
                    }

                }
            }
            if (number > 0)
            {
                ReturnResult = true;
            }
            else
            {
                ReturnResult = false;
            }
            return ReturnResult;
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
            if (DDLStatus.SelectedValue != "0")
            {
                if (Read_DocIDs())
                {
                    Common.CommonFuction.ShowMessage("Records update successfully");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("unable to update");
                }
                Load_OD_for_approved();
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
    private bool Save_Record(string OD_Id, DateTime DTTo, DateTime DTFrom, string SFT_ID, string IN_TIME, string OUT_TIME)
    {
        bool ReturnResult = false;
        try
        {
            TimeSpan ts = DTTo - DTFrom;
            int TotalDays = ts.Days + 1;
            bool Result = SaitexBL.Interface.Method.EMPOUTDOORDUTY.Change_OD_Status(OD_Id, Convert.ToChar(DDLStatus.SelectedValue.ToString()));
            if (Result)
            {
                int number = 0;
                DateTime Attn_DATE_1 = DTFrom;
                while (number < TotalDays)
                {

                    SaitexDM.Common.DataModel.HR_ATTN_TRN oHR_ATTN_TRN = new SaitexDM.Common.DataModel.HR_ATTN_TRN();
                    oHR_ATTN_TRN.EMP_CODE = iEmp_Code;
                    oHR_ATTN_TRN.ATTN_DATE = Attn_DATE_1.ToString();
                    if (IN_TIME.Trim() != string.Empty)
                    {
                        oHR_ATTN_TRN.IN_TIME = IN_TIME.ToString();
                    }
                    else
                    {
                        oHR_ATTN_TRN.IN_TIME = "00:00";
                    }
                    if (OUT_TIME.Trim() != string.Empty)
                    {
                        oHR_ATTN_TRN.OUT_TIME = OUT_TIME.ToString();
                    }
                    else
                    {
                        oHR_ATTN_TRN.OUT_TIME = "00:00";
                    }
                    oHR_ATTN_TRN.SFT_ID = int.Parse(SFT_ID.Trim().ToString());
                    oHR_ATTN_TRN.TUSER = oUserLoginDetail.UserCode.ToString().Trim();
                    oHR_ATTN_TRN.ENTRY_TYPE = "0";
                    bool rESULTE = SaitexBL.Interface.Method.HR_ATTN_TRN.InsertAttendanceod(oHR_ATTN_TRN);
                    if (rESULTE)
                    {
                        number = number + 1;
                        Attn_DATE_1 = Attn_DATE_1.AddDays(1);
                    }
                }
                if (number > 0)
                {
                    ReturnResult = true;

                }
            }
            else
            {
                ReturnResult = true;

            }
            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }
}
