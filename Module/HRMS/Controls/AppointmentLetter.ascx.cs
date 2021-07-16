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
using Common;


public partial class Module_HRMS_Controls_AppointmentLetter : System.Web.UI.UserControl
{
    string EmpCode = "";
    private static int MaxRefNO;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                if (!IsPostBack)
                {
                    bindEmpName();
                    Load_Max_RefNo();
                }
            }
            else
            {
                Response.Redirect("~/default.aspx", false);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Error in Page Loading');", true);
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        string whereClause = " WHERE EMP_CODE like :SearchQuery And DEL_STATUS = '0' or F_NAME like :SearchQuery And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY EMP_CODE";
        string commandText = "SELECT * FROM  HR_EMP_MST";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_Appoint_Let.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }
    protected int GetItemsCount(string text)
    {
        string CommandText = "SELECT COUNT(*) FROM HR_EMP_MST WHERE EMP_CODE like :SearchQuery And DEL_STATUS = '0' or F_NAME like :SearchQuery And DEL_STATUS = '0'";
        return SaitexBL.Interface.Method.HR_Appoint_Let.GetCountForLOV(CommandText, text + '%', "");
    }
    private void bindEmpName()
    {
        DataTable data = new DataTable();
        data = GetItems("", 0, 10);
        cmbEmpName.DataSource = data;
        cmbEmpName.DataBind();
    }
    protected void cmbEmpName_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10);

        cmbEmpName.Items.Clear();
        cmbEmpName.DataSource = data;
        cmbEmpName.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            string GradeId = "";
            string GradeName = "";
            if (cmbEmpName.SelectedValue != "" && txtDate.Text.Trim().ToString() != "" && txtRef.Text.Trim().ToString() != "")
            {
                bool SaveResult = SaitexBL.Interface.Method.HR_Appoint_Let.InsertAppointment(MaxRefNO, DateTime.Parse(txtDate.Text.Trim().ToString()), cmbEmpName.SelectedValue.ToString(), Session["urLoginId"].ToString());
                if (SaveResult)
                {
                    SaitexDM.Common.DataModel.HR_Appoint_Let oHR_Appoint_Let = new SaitexDM.Common.DataModel.HR_Appoint_Let();

                    oHR_Appoint_Let.EMP_CODE = cmbEmpName.SelectedValue.ToString().Trim();

                    DataTable dt = SaitexBL.Interface.Method.HR_Appoint_Let.GetEmpDetail(oHR_Appoint_Let);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataView dv = new DataView(dt);
                        if (dv.Count > 0)
                        {
                            for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                            {
                                GradeId = dv[iLoop]["GRADE_ID"].ToString();
                                EmpCode = dv[iLoop]["EMP_CODE"].ToString();
                            }
                        }
                    }

                    SaitexDM.Common.DataModel.HR_Appoint_Let oHR_Appoint_Let1 = new SaitexDM.Common.DataModel.HR_Appoint_Let();

                    oHR_Appoint_Let1.GRADE_ID = GradeId;

                    DataTable dt1 = SaitexBL.Interface.Method.HR_Appoint_Let.GetGradeDetail(oHR_Appoint_Let1);

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        DataView dv1 = new DataView(dt1);
                        if (dv1.Count > 0)
                        {
                            for (int iLoop = 0; iLoop < dv1.Count; iLoop++)
                            {
                                GradeName = dv1[iLoop]["MST_DESC"].ToString();
                            }
                        }
                    }

                    string URL = "../Reports/AppointmentLetterReport.aspx?ref1=" + txtRef.Text.Trim() + "&dt1=" + txtDate.Text.Trim() + "&empcode=" + EmpCode + "&gradename=" + GradeName;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Duplicate Reference No!please change and try agin");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Select Employee/Date');", true);
            }
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("./AppointmentLetter.aspx", false);
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void Load_Max_RefNo()
    {
        try
        {
            MaxRefNO = SaitexBL.Interface.Method.HR_Appoint_Let.Get_Max_RefNo();
            string StrRefNo = "STL/HRD/APP/" + System.DateTime.Now.Year + "/" + MaxRefNO;
            txtRef.Text = StrRefNo.ToString();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
    protected void cmbEmpName_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_Appoint_Let.Load_Record(cmbEmpName.SelectedValue.ToString());
            if (DTable.Rows.Count > 0)
            {
                MaxRefNO =int.Parse( DTable.Rows[0]["APP_REF_NO"].ToString());
                string StrRefNo = "STL/HRD/APP/" + DTable.Rows[0]["RefNo"].ToString();
                txtRef.Text = StrRefNo.ToString();
                txtDate.Text = DTable.Rows[0]["APP_DATE"].ToString();
            }
            else
            {
                txtDate.Text = "";
                Load_Max_RefNo();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
}
