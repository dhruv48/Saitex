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
using System.Data.OracleClient;
using Common;
using errorLog;
using System.IO;
using DBLibrary;



public partial class Module_HRMS_Controls_EmpLeaveDetails : System.Web.UI.UserControl
{
    string EmpId;
    string OpenYear = string.Empty;
    decimal PreYear =System.DateTime.Now.Year ;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (Request.QueryString["EMP_CODE"] != null)
                {
                    EmpId = Request.QueryString["EMP_CODE"].ToString().Trim();
                    if (oUserLoginDetail.OPEN_YEAR.ToString() != "" && oUserLoginDetail.OPEN_YEAR != null)
                    {
                        OpenYear = oUserLoginDetail.OPEN_YEAR.Trim().ToString();
                    }
                    else
                    {
                        OpenYear = System.DateTime.Now.Year.ToString();
                    }
                    if (!IsPostBack)
                    {
                        if (EmpId != null)
                        {
                            BindEmp_Info();
                            fillYear();
                            PreYear  = int.Parse(OpenYear.ToString()) - 1;
                            LblPreYear.Text = PreYear.ToString();
                            getEmpLeaveRecord(OpenYear);
                        }
                    }
                }

            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Page Loading"));
        }
    }
    private void fillYear()
    {
        try
        {
            for (int i = -2; i < 2; i++)
            {
                DDLYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
            }
            DDLYear.Items.Insert(0, new ListItem("---Select---", ""));
            DDLYear.SelectedValue = OpenYear;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }
    private void BindEmp_Info()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_EMP_LV oHR_EMP_LV = new SaitexDM.Common.DataModel.HR_EMP_LV();
            oHR_EMP_LV.EMP_ID = EmpId;
            DataTable ETable = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(EmpId);
            foreach (DataRow Drow in ETable.Rows)
            {
                lblEmployeeCode.Text = Drow["EMP_CODE"].ToString();
                lblEmployeeName.Text = Drow["EMPLOYEENAME"].ToString();
                LblDept.Text = Drow["DEPT_NAME"].ToString();
                LblDesig.Text = Drow["DESIG_NAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int Res = 0;
            if (Page.IsValid)
            {
                if (grdLeave.Rows.Count > 0)
                {
                    foreach (GridViewRow rw in grdLeave.Rows)
                    {
                        Label lblLeaveId = (Label)rw.FindControl("lblLeaveId");
                        Label lblMstId = (Label)rw.FindControl("lBLMst_ID");
                        TextBox TxtCurrYear = (TextBox)rw.FindControl("TxtCurrYear");
                        string strLV_ID = "";
                        string IN_LV_DAYS = "";
                        string LV_MSTID = "";
                        strLV_ID = lblLeaveId.Text.Trim();
                        LV_MSTID = lblMstId.Text.Trim();
                        if (TxtCurrYear.Text.Trim() != "0")
                        {
                            IN_LV_DAYS = TxtCurrYear.Text.Trim();
                            if (saveEmpLeave(strLV_ID, LV_MSTID, IN_LV_DAYS))
                            {
                                Res = Res + 1;
                            }
                        }
                    }
                    if (Res > 0)
                    {
                        Common.CommonFuction.ShowMessage("Leave Update Sucessfully");
                        getEmpLeaveRecord(OpenYear);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Unable to Save");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Updating Data"));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void getEmpLeaveRecord(string CurrentYear)
    {
        DataTable dt = new DataTable();
        try
        {

            SaitexDM.Common.DataModel.HR_EMP_LV oHR_EMP_LV = new SaitexDM.Common.DataModel.HR_EMP_LV();
            oHR_EMP_LV.EMP_ID = EmpId.ToString();
            oHR_EMP_LV.CUR_YEAR = CurrentYear.ToString();
            dt = SaitexBL.Interface.Method.HR_EMP_LV.GetLeaveMaster(oHR_EMP_LV);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdLeave.DataSource = dt;
                grdLeave.DataBind();
            }
            dt.Dispose();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }

    private bool saveEmpLeave(string strLV_ID, string LV_MSTID, string IN_LV_DAYS)
    {
        bool REsult = false;
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.HR_EMP_LV oHR_EMP_LV = new SaitexDM.Common.DataModel.HR_EMP_LV();
            oHR_EMP_LV.EMP_ID = EmpId;
            oHR_EMP_LV.LV_MST_ID = LV_MSTID;
            oHR_EMP_LV.CUR_YEAR = OpenYear.ToString();
            oHR_EMP_LV.LV_ID = strLV_ID;
            oHR_EMP_LV.LV_DAYS = IN_LV_DAYS;
            oHR_EMP_LV.TUSER = oUserLoginDetail.LOGINDETAILID;
            oHR_EMP_LV.TAKEN_LV_DAYS = "0";
            oHR_EMP_LV.REMAIN_LV_DAYS = IN_LV_DAYS;
            int iRecordFound = 0;
            bool bResult = SaitexBL.Interface.Method.HR_EMP_LV.Insert(oHR_EMP_LV, out iRecordFound);
            if (bResult)
            {
                REsult = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                REsult = false;
            }
            return REsult;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DDLYear.Enabled = true;
        }
        catch
        {
            throw;
        }
    }
    protected void DDLYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            getEmpLeaveRecord(DDLYear.SelectedValue.Trim().ToString());
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Selecting Year"));
        }
    }
    protected void txtDefaultValue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal TotalLeave = 0;
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
            Label LastyearClosing = (Label)grdRow.FindControl("LblClosing");
            Label iCarry = (Label)grdRow.FindControl("LblCarry");
            TextBox txtNewLeave = (TextBox)grdRow.FindControl("txtDefaultValue");
            TextBox txtTotalLeave = (TextBox)grdRow.FindControl("TxtCurrYear");
            if (iCarry.Text.Trim().ToUpper() == "YES")
            {
                TotalLeave = decimal.Parse(LastyearClosing.Text.ToString()) + decimal.Parse(txtNewLeave.Text.ToString());
                txtTotalLeave.Text = TotalLeave.ToString();
            }
            else
            {
                txtTotalLeave.Text = txtNewLeave.Text.Trim().ToString();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Text Change"));
        }
    }
}
