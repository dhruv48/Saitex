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
using errorLog;
using DBLibrary;

public partial class Module_Admin_Controls_DepartmentMaster : System.Web.UI.UserControl
{
    string strTUser = "";
    bool chStatus;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["urLoginId"] != null)
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strTUser = oUserLoginDetail.LOGINDETAILID;

            if (!IsPostBack)
            {
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                lblMode.Text = "Save";
                txtDepartmentCode.Visible = true;
                cmbDepartmentCode.Visible = false;
                chk_Status.Checked = true;
                grdDepartment.AutoPostBackOnSelect = false;

                bindDepartmentGrid();
            }
            lblErrorMessage.Text = "";
            lblMessage.Text = "";
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }

                Session["saveStatus"] = 0;
            }
        }
        else
        {
            Response.Redirect("/Saitex/Default.aspx", false);
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        InsertData();
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        grdDepartment.AutoPostBackOnSelect = true;
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        tdDelete.Visible = true;
        lblMode.Text = "Update";
        txtDepartmentCode.Visible = false;
        cmbDepartmentCode.Visible = true;
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpdateData();
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        DeleteData();
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("./DepartmentMaster.aspx", false);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/DeptMasterRpt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
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
                Response.Redirect("~/Admin/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void bindDepartmentGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dt != null && dt.Rows.Count > 0)
            {
                grdDepartment.DataSource = dt;
                grdDepartment.DataBind();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        string whereClause = " WHERE DEPT_CODE like :SearchQuery And DEL_STATUS = '0' or DEPT_NAME like :SearchQuery And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY DEPT_CODE";
        string commandText = "SELECT * FROM  CM_DEPT_MST";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }
    protected int GetItemsCount(string text)
    {
        string CommandText = "SELECT COUNT(*) FROM CM_DEPT_MST WHERE DEPT_CODE like :SearchQuery And DEL_STATUS = '0' or DEPT_NAME like :SearchQuery And DEL_STATUS = '0'";
        return SaitexBL.Interface.Method.CM_DEPT_MST.GetCountForLOV(CommandText, text + '%', "");
    }
    protected void cmbDepartmentCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            char chCheck;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "DEPT_CODE='" + cmbDepartmentCode.SelectedValue.ToString().Trim() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtDepartmentCode.Text = dv[iLoop]["DEPT_CODE"].ToString();
                        txtDeparmentName.Text = dv[iLoop]["DEPT_NAME"].ToString();
                        txtRemarks.Text = dv[iLoop]["DEPT_REMARKS"].ToString();
                        chCheck = char.Parse(dv[iLoop]["STATUS"].ToString());

                        if (chCheck == '1')
                        {
                            chk_Status.Checked = true;
                        }
                        else
                        {
                            chk_Status.Checked = false;
                        }
                    }
                    cmbDepartmentCode.Visible = false;
                    txtDepartmentCode.Visible = true;
                    txtDepartmentCode.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbDepartmentCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10);

        cmbDepartmentCode.DataSource = data;
        cmbDepartmentCode.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);
    }
    private void InsertData()
    {
        try
        {
            if (Page.IsValid)
            {
                SaitexDM.Common.DataModel.CM_DEPT_MST oCM_DEPT_MST = new SaitexDM.Common.DataModel.CM_DEPT_MST();

                if (chk_Status.Checked == true)
                {
                    chStatus = true;
                }
                else
                {
                    chStatus = false;
                }

                oCM_DEPT_MST.DEPT_CODE = txtDepartmentCode.Text.ToUpper().Trim();
                oCM_DEPT_MST.DEPT_NAME = txtDeparmentName.Text.ToUpper().Trim();
                oCM_DEPT_MST.DEPT_REMARKS = txtRemarks.Text.Trim();
                oCM_DEPT_MST.STATUS = chStatus;
                oCM_DEPT_MST.TUSER = strTUser;
                oCM_DEPT_MST.DEL_STATUS = false;

                int iRecordFound = 0;

                bool bResult = SaitexBL.Interface.Method.CM_DEPT_MST.Insert(oCM_DEPT_MST, out iRecordFound);

                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("./DepartmentMaster.aspx?cId=S", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void UpdateData()
    {
        bool chStatus;
        SaitexDM.Common.DataModel.CM_DEPT_MST oCM_DEPT_MST = new SaitexDM.Common.DataModel.CM_DEPT_MST();

        if (chk_Status.Checked == true)
        {
            chStatus = true;
        }
        else
        {
            chStatus = false;
        }

        oCM_DEPT_MST.DEPT_CODE = txtDepartmentCode.Text.ToUpper().Trim();
        oCM_DEPT_MST.DEPT_NAME = txtDeparmentName.Text.ToUpper().Trim();
        oCM_DEPT_MST.DEPT_REMARKS = txtRemarks.Text.Trim();
        oCM_DEPT_MST.STATUS = chStatus;

        int iRecordFound = 0;
        bool bResult = SaitexBL.Interface.Method.CM_DEPT_MST.Update(oCM_DEPT_MST, out iRecordFound);

        if (bResult)
        {
            Session["saveStatus"] = 1;
            Response.Redirect("./DepartmentMaster.aspx?cId=U", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
        }
    }
    private void DeleteData()
    {
        try
        {
            SaitexDM.Common.DataModel.CM_DEPT_MST oCM_DEPT_MST = new SaitexDM.Common.DataModel.CM_DEPT_MST();

            oCM_DEPT_MST.TUSER = strTUser;

            if (txtDepartmentCode.Visible == true)
            {
                oCM_DEPT_MST.DEPT_CODE = txtDepartmentCode.Text.ToUpper().Trim();
            }
            else
            {
                oCM_DEPT_MST.DEPT_CODE = cmbDepartmentCode.SelectedValue.ToString().Trim();
            }

            bool bResult = SaitexBL.Interface.Method.CM_DEPT_MST.Delete(oCM_DEPT_MST);

            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./DepartmentMaster.aspx?cId=D", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.!');", true);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void grdDepartment_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        char chCheck;
        cmbDepartmentCode.Visible = false;
        txtDepartmentCode.Visible = true;
        txtDepartmentCode.Enabled = false;

        ArrayList ar = grdDepartment.SelectedRecords;

        lblMessage.Text = "";
        tdDelete.Visible = true;
        tdUpdate.Visible = true;
        tdSave.Visible = false;

        Hashtable ht = (Hashtable)ar[0];

        txtDepartmentCode.Text = ht["DEPT_CODE"].ToString().Trim();
        txtDeparmentName.Text = ht["DEPT_NAME"].ToString().Trim();
        txtRemarks.Text = ht["DEPT_REMARKS"].ToString().Trim();
        chCheck = char.Parse(ht["STATUS"].ToString());

        if (chCheck == '1')
        {
            chk_Status.Checked = true;
        }
        else
        {
            chk_Status.Checked = false;
        }
    }
}
