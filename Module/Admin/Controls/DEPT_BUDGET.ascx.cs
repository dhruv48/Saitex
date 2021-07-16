using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Web.UI.WebControls.Adapters;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_DEPT_BUDGET : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            BUDGET_LOV1.SelectedIndexChanged += new Module_HRMS_Controls_BUDGET_LOV.RefreshDataGridView(BUDGET_LOV1_SelectedIndexChanged);
            if (!IsPostBack)
            {
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                tdClear.Visible = true;
                bindGvDeptBudget();
                tdFind.Visible = true;
                lblMode.Text = "Save";
                txtYear.Text = Convert.ToString(System.DateTime.Now.Year);
                BUDGET_LOV1.Visible = false;
                BUDGET_LOV1.Height = Unit.Pixel(200);
                BUDGET_LOV1.Width = Unit.Pixel(200);
                bindDDLDept();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }

    private void bindGvDeptBudget()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_DEPT_BUDGET.GetDeptBgdName();
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch
        {
            throw;
        }

    }

    void BUDGET_LOV1_SelectedIndexChanged(Module_HRMS_Controls_BUDGET_LOV.BUDGET_LOV_EventArgs e)
    {
        try
        {
            GetBdgData(Convert.ToInt32(e.SelectedValue.Trim()));
            BUDGET_LOV1.Visible = false;
            txtYear.Visible = true;
            txtYear.Enabled = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row selection.\r\nSee error log for detail."));
        }

    }
    private void bindDDLDept()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDeptCode.DataTextField = "DEPT_NAME";
            ddlDeptCode.DataValueField = "DEPT_CODE";
            ddlDeptCode.DataSource = dt;
            ddlDeptCode.DataBind();
            ddlDeptCode.Items.Insert(0, new ListItem("---------Select----------", ""));
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
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in data saving.\r\nSee error log for detail."));
        }

    }
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (lblMode.Text != "Save")
            {
                ArrayList ar = Grid1.SelectedRecords;

                lblMode.Text = "Update";
                tdDelete.Visible = true;
                //tdClear.Visible = true;
                tdUpdate.Visible = true;
                tdSave.Visible = false;
                Hashtable ht = (Hashtable)ar[0];
                ViewState["DEPT_BUDGET_CODE"] = ht["DEPT_BUDGET_CODE"].ToString().Trim();
                BUDGET_LOV1.Visible = false;
                txtYear.Visible = true;
                txtYear.Text = ht["YEAR"].ToString().Trim();
                txtBdgAmt.Text = ht["BUDGET_AMT"].ToString().Trim();
                ddlDeptCode.SelectedValue = ht["DEPT_CODE"].ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }

    }
    private void InsertData()
    {
        try
        {
            if (txtYear.Text != "" && ddlDeptCode.SelectedValue.Trim() != "")
            {
                int iRecordFound = 0;
                SaitexDM.Common.DataModel.HR_DEPT_BUDGET oDEPT_BGT = new SaitexDM.Common.DataModel.HR_DEPT_BUDGET();
                oDEPT_BGT.DEPT_CODE = CommonFuction.funFixQuotes(ddlDeptCode.SelectedValue.Trim());
                oDEPT_BGT.Year = Convert.ToInt32(CommonFuction.funFixQuotes(txtYear.Text.Trim()));
                oDEPT_BGT.BUDGET_AMT = Convert.ToDouble(txtBdgAmt.Text.Trim());
                oDEPT_BGT.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_DEPT_BUDGET.Insert_HR_DEPT_BUDGET(oDEPT_BGT, out iRecordFound);
                if (bResult)
                {
                    txtBdgAmt.Text = "";
                    ddlDeptCode.SelectedIndex = -1;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                    bindGvDeptBudget();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists:Please Enter another Record');", true);
                }
            }
        }
        catch
        {
            throw;
        }

    }
    private void UpdateData()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.HR_DEPT_BUDGET oDEPT_BGT = new SaitexDM.Common.DataModel.HR_DEPT_BUDGET();
            oDEPT_BGT.DEPT_BUDGET_CODE = Convert.ToInt32(ViewState["DEPT_BUDGET_CODE"].ToString().Trim());
            oDEPT_BGT.DEPT_CODE = CommonFuction.funFixQuotes(ddlDeptCode.SelectedValue.Trim());
            oDEPT_BGT.Year = Convert.ToInt32(CommonFuction.funFixQuotes(txtYear.Text.Trim()));
            oDEPT_BGT.BUDGET_AMT = Convert.ToDouble(txtBdgAmt.Text.Trim());
            oDEPT_BGT.TUSER = Session["urLoginId"].ToString().Trim();
            bool bResult = SaitexBL.Interface.Method.HR_DEPT_BUDGET.Update_HR_DEPT_BUDGET(oDEPT_BGT, out iRecordFound);
            if (bResult)
            {
                txtBdgAmt.Text = "";
                ddlDeptCode.SelectedIndex = -1;
                BUDGET_LOV1.Visible = false;
                bindGvDeptBudget();

                tdDelete.Visible = false;
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                lblMode.Text = "Save";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
            }
        }
        catch
        {
            throw;
        }
    }
    private void DeleteData()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.HR_DEPT_BUDGET oDEPT_BGT = new SaitexDM.Common.DataModel.HR_DEPT_BUDGET();
            oDEPT_BGT.DEPT_BUDGET_CODE = Convert.ToInt32(ViewState["DEPT_BUDGET_CODE"].ToString().Trim());
            oDEPT_BGT.TUSER = Session["urLoginId"].ToString().Trim();
            bool bResult = SaitexBL.Interface.Method.HR_DEPT_BUDGET.Delete_HR_DEPT_BUDGET(oDEPT_BGT, out iRecordFound);
            if (bResult)
            {
                txtBdgAmt.Text = "";
                ddlDeptCode.SelectedIndex = -1;
                BUDGET_LOV1.Visible = false;

                bindGvDeptBudget();
                tdDelete.Visible = false;
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                lblMode.Text = "Save";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
                bindGvDeptBudget();
            }
        }
        catch
        {
            throw;
        }


    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlDeptCode.SelectedIndex = -1;
            BUDGET_LOV1.Visible = false;
            txtBdgAmt.Text = "";
            txtYear.Enabled = true;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            lblMode.Text = "Save";
            bindGvDeptBudget();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page reloading.\r\nSee error log for detail."));
        }

    }
    private void GetBdgData(int iDeptBgtCode)
    {
        try
        {
            SaitexDM.Common.DataModel.HR_DEPT_BUDGET oDeptBudget = new SaitexDM.Common.DataModel.HR_DEPT_BUDGET();
            //oDeptBudget.Year = Convert.ToInt32(CommonFuction.funFixQuotes(txtYear.Text.Trim()));
            DataTable dt = SaitexBL.Interface.Method.HR_DEPT_BUDGET.GetDeptBgdCode(iDeptBgtCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtYear.Text = dt.Rows[0]["YEAR"].ToString();
                txtBdgAmt.Text = dt.Rows[0]["BUDGET_AMT"].ToString();
                ddlDeptCode.SelectedIndex = ddlDeptCode.Items.IndexOf(ddlDeptCode.Items.FindByValue(dt.Rows[0]["DEPT_CODE"].ToString()));
                ViewState["DEPT_BUDGET_CODE"] = dt.Rows[0]["DEPT_BUDGET_CODE"].ToString().Trim();

                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;

                lblMode.Text = "Update";
            }
        }
        catch
        { throw; }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            UpdateData();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in data updation.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DeleteData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in data deletion.\r\nSee error log for detail."));
        }
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GetBdgData('Y');
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data by year.\r\nSee error log for detail."));
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
                Response.Redirect("~/GetUserAuthorisation.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMode.Text = "Update";
            BUDGET_LOV1.Visible = true;
            txtYear.Visible = true;
            Grid1.AutoPostBackOnSelect = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "DeptBudgetReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing data.\r\nSee error log for detail."));
        }

    }
}
