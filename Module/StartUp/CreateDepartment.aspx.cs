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

public partial class Module_StartUp_CreateDepartment : System.Web.UI.Page
{
   
    bool chStatus;

    protected void Page_Load(object sender, EventArgs e)
    {
                   

            if (!IsPostBack)
            {
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                lblMode.Text = "Save";
                txtDepartmentCode.Visible = true;               
                chk_Status.Checked = true;
                grdDepartment.AutoPostBackOnSelect = false;
                bindDepartmentGrid();
            }
            lblErrorMessage.Text = "";
            lblMessage.Text = "";          
       
       
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        InsertData();
    }

    private void bindDepartmentGrid()
    {
        try
        {
            var dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
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

    private void InsertData()
    {
        try
        {
            if (Page.IsValid)
            {
                var oCM_DEPT_MST = new SaitexDM.Common.DataModel.CM_DEPT_MST();

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
                oCM_DEPT_MST.TUSER = "SUPER ADMIN";
                oCM_DEPT_MST.DEL_STATUS = false;

                int iRecordFound = 0;

                var bResult = SaitexBL.Interface.Method.CM_DEPT_MST.Insert(oCM_DEPT_MST, out iRecordFound);

                if (bResult)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Department Saved successfully');", true);
                    Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                    Response.Redirect("CreateFinancialYear.aspx", false);
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

}
