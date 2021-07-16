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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;
using System.Data.OracleClient;

public partial class Module_Admin_Pages_DepartmentManPower : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                RefreshDetailRow();
                Load_Department();
                Load_Desination();
                Initial_Control();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail/"));
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SaveGroup();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in data saving.\r\nSee error log for detail/"));
        }
    }
    private void SaveGroup()
    {
        try
        {
            int iRecordFound = 0;
            if (DDLdepartment.SelectedIndex > 0)
            {
                if (DDLDesination.SelectedIndex > 0)
                {

                    SaitexDM.Common.DataModel.DeptManPower DMP = new SaitexDM.Common.DataModel.DeptManPower();
                    DMP.DEPT_CODE = Common.CommonFuction.funFixQuotes(DDLdepartment.SelectedValue.ToString());
                    DMP.DESIG_CODE = Common.CommonFuction.funFixQuotes(DDLDesination.SelectedValue.ToString());
                    DMP.NO_OF_PERSON = int.Parse(TxtNoOfMan.Text.Trim());
                    DMP.YEAR = int.Parse(TxtCurrentYear.Text.Trim());
                    DMP.TUSER = oUserLoginDetail.UserCode;
                    bool Res = SaitexBL.Interface.Method.DeptManPower.InsertDeptMPowerMST(DMP, out iRecordFound);

                    if (Res)
                    {
                        Common.CommonFuction.ShowMessage("Record Save sucessfully .");
                        Initial_Control();

                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Not Save .");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("please select desigination");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("please select department");
            }
        }
        catch
        {
            throw;
        }

    }
    private void Load_Department()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.DeptManPower.SelectDeptName();
            DDLdepartment.DataSource = DTable;
            DDLdepartment.DataTextField = "DEPT_NAME";
            DDLdepartment.DataValueField = "DEPT_CODE";
            DDLdepartment.DataBind();
            DDLdepartment.Items.Insert(0, "--SELECT--");
        }
        catch
        {
            throw;
        }
    }
    private void Load_Desination()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.DeptManPower.SelectDesigName();
            DDLDesination.DataSource = DTable;
            DDLDesination.DataTextField = "DESIG_NAME";
            DDLDesination.DataValueField = "DESIG_CODE";
            DDLDesination.DataBind();
            DDLDesination.Items.Insert(0, "--SELECT--");
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
                Response.Redirect("~/GetUserAuthorisation.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem In Exit Form"));
        }
    }
    private void Initial_Control()
    {
        try
        {
            TxtNoOfMan.Text = "";
            DDLdepartment.SelectedIndex = 0;
            DDLDesination.SelectedIndex = 0;
            cmbfind.Visible = false;
            DDLdepartment.Visible = true;
            lblMode.Text = "Save";
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            imgbtnSave.Visible = true;
            bindgrid();
            DDLdepartment.Enabled = true;
            DDLDesination.Enabled = true;
            tdFind.Visible = true;
            TxtCurrentYear.Enabled = true;
            bindgrid();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initial_Control();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page reloading.\r\nSee error log for detail/"));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Common.CommonFuction.ShowMessage("Report UnderProcess");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting print.\r\nSee error log for detail/"));
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (DDLdepartment.SelectedIndex != 0)
            {
                if (DDLDesination.SelectedIndex != 0)
                {
                    DataTable DTable = new DataTable();
                    SaitexDM.Common.DataModel.DeptManPower DMP = new SaitexDM.Common.DataModel.DeptManPower();
                    DMP.YEAR = DateTime.Now.Year;
                    DMP.DEPT_CODE = DDLdepartment.SelectedValue.ToString();
                    DMP.DESIG_CODE = DDLDesination.SelectedValue.ToString();
                    DMP.TUSER = oUserLoginDetail.UserCode;
                    DMP.TDATE = DateTime.Now;
                    bool Res = SaitexBL.Interface.Method.DeptManPower.DeleteDeptMPowerMST(DMP);
                    if (Res)
                    {
                        Common.CommonFuction.ShowMessage("Record delete sucessfully");
                        RefreshDetailRow();
                        Initial_Control();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Unable to delete");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("please select desigination");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("please select department");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail/"));
        }
    }
    private void RefreshDetailRow()
    {
        DDLdepartment.SelectedIndex = -1;
        DDLDesination.SelectedIndex = -1;
        TxtCurrentYear.Text = DateTime.Now.Year.ToString();
        TxtNoOfMan.Text = "";
        ViewState["UniqueId"] = null;
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ?')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help ?')");
    }
    protected void bindgrid()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.DeptManPower.selectgrid();
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cmbfind.Visible = true;
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            imgbtnSave.Visible = false;
            DDLdepartment.Enabled = false;
            DDLDesination.Enabled = false;
            TxtCurrentYear.Enabled = false;
            lblMode.Text = "Update";
            TxtCurrentYear.Text = "";
            TxtNoOfMan.Text = "";
            DDLdepartment.SelectedIndex = -1;
            DDLDesination.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail/"));
        }
    }
    protected void cmbfind_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.DeptManPower.CmpLoding();
            cmbfind.DataSource = dt;
            cmbfind.DataValueField = "DEPARTMENT";
            cmbfind.DataTextField = "DEPT_NAME";
            cmbfind.DataBind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail/"));
        }
    }
    protected void cmbfind_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {

        cmbfind.Visible = true;
        DDLDesination.Enabled = false;
        TxtCurrentYear.Enabled = false;
        cmbfind.AutoPostBack = true;
        tdUpdate.Visible = true;
        try
        {
            string adjData = cmbfind.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = adjData.Split(splitter);

            int YEAR = int.Parse(arrString[0].ToString());
            string DEPT_CODE = arrString[1].ToString();
            string DESIG_CODE = arrString[2].ToString();
            int NO_OF_PERSON = int.Parse(arrString[3].ToString());


            DDLdepartment.SelectedValue = DEPT_CODE;
            DDLDesination.SelectedValue = DESIG_CODE;
            TxtNoOfMan.Text = NO_OF_PERSON.ToString();
            TxtCurrentYear.Text = YEAR.ToString();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail/"));
        }
    }
    private void findgrid()
    {
        try
        {
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
            lblMode.Text = "Update";

            ArrayList ar = Grid1.SelectedRecords;
            Hashtable ht = (Hashtable)ar[0];
            DDLdepartment.SelectedIndex = DDLdepartment.Items.IndexOf(DDLdepartment.Items.FindByText(ht["DEPT_NAME"].ToString().Trim()));
            DDLdepartment.SelectedIndex = DDLdepartment.Items.IndexOf(DDLdepartment.Items.FindByValue(ht["DESIG_NAME"].ToString().Trim()));
            TxtCurrentYear.Text = ht["YEAR"].ToString().Trim();
            TxtNoOfMan.Text = ht["NO_OF_PERSON"].ToString().Trim();


        }
        catch
        {
            throw;
        }

    }
    private void Update()
    {

        try
        {
            int iRecordFound = 0;
            if (DDLdepartment.SelectedIndex > 0)
            {
                if (DDLDesination.SelectedIndex > 0)
                {

                    SaitexDM.Common.DataModel.DeptManPower DMP = new SaitexDM.Common.DataModel.DeptManPower();
                    DMP.DEPT_CODE = Common.CommonFuction.funFixQuotes(DDLdepartment.SelectedValue.ToString());
                    DMP.DESIG_CODE = Common.CommonFuction.funFixQuotes(DDLDesination.SelectedValue.ToString());
                    DMP.NO_OF_PERSON = int.Parse(TxtNoOfMan.Text.Trim());
                    DMP.YEAR = int.Parse(TxtCurrentYear.Text.Trim());
                    DMP.TUSER = oUserLoginDetail.UserCode;

                    bool Res = SaitexBL.Interface.Method.DeptManPower.UpdateDeptMPowerMST(DMP, out iRecordFound);
                    if (Res)
                    {
                        Common.CommonFuction.ShowMessage("Record Update sucessfully .");
                        Initial_Control();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("NotUpdate .");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Designation");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Department");
            }

        }
        catch
        {
            throw;

        }

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Update();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in data updation.\r\nSee error log for detail/"));
        }
    }
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail/"));
        }
    }
}
