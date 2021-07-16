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
using System.Xml.Linq;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_Admin_Controls_Payroll_MST : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;   
    SaitexDM.Common.DataModel.PAYROLL_PARAMETERS PAY = new SaitexDM.Common.DataModel.PAYROLL_PARAMETERS();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            imgbtnSave.Attributes.Add("onclick", "return validate()");
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }
    private void InitialisePage()
    {
        try
        {
            TxtFromDate.Text = string.Empty;
            TxtToDate.Text = string.Empty;
            DDLOpenMonth.SelectedIndex = -1;
            TxtMasterCode.Text = "NEW";
            Bind_Year();
            Load_Grid_Record();
            TxtMasterCode.Visible = true;
            DDLSearch.Visible = false;
            ChkActive.Checked = false;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            tdSave.Visible = true;
            lblMode.Text = "Save";
        }
        catch 
        {
            throw;
        }
    }
    private void Load_Grid_Record()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.PAYROLL_PARAMETERS.Load_Master_Record("", oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE );
            GridViewMasterRecord.DataSource = DTable;
            GridViewMasterRecord.DataBind();

        }
        catch
        {
            throw;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    }
    private void Bind_Year()
    {
        try
        {
            for (int i = -2; i < 4; i++)
            {
                DDLOpenYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
            }
            DDLOpenYear.Items.Insert(0, new ListItem("---Select---", ""));
            DDLOpenYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
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
            if (Save_Record())
            {
                InitialisePage();
                Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                Response.Redirect("~/GetUserAuthorisation.aspx", false);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to insert!please try again");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Data.\r\nSee error log for detail."));
        }
    }
    private bool Save_Record()
    {
        bool Res = false;
        try
        {
            if (Page.IsValid)
            {
                if (TxtMasterCode.Text.Trim().ToUpper() == "NEW")
                {
                    PAY.PMASTER_CODE = 0;
                }
                else
                {
                    PAY.PMASTER_CODE = int.Parse(CommonFuction.funFixQuotes(TxtMasterCode.Text.Trim()));
                }
                PAY.OPEN_YEAR = int.Parse(DDLOpenYear.SelectedValue.ToString());
                PAY.OPEN_MONTH = int.Parse(DDLOpenMonth.SelectedValue.ToString());
                PAY.SALARY_FROMDATE = DateTime.Parse(CommonFuction.funFixQuotes(TxtFromDate.Text.Trim()));
                PAY.SALARY_TODATE = DateTime.Parse(CommonFuction.funFixQuotes(TxtToDate.Text.Trim()));
                if (ChkActive.Checked)
                {
                    PAY.ISACTIVE = '1';
                }
                else
                {
                    PAY.ISACTIVE ='0';
                }
                PAY.TUSER = oUserLoginDetail.UserCode;
                PAY.COMP_CODE=oUserLoginDetail.COMP_CODE;
                PAY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                if (Can_Save(PAY))
                {
                    Res = SaitexBL.Interface.Method.PAYROLL_PARAMETERS.Save_Detail(PAY);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("This Month & Year Already Exist");
                }
            }
            return Res;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private bool Can_Save(SaitexDM.Common.DataModel.PAYROLL_PARAMETERS PAY)
    {
        try
        {
          bool   Res = SaitexBL.Interface.Method.PAYROLL_PARAMETERS.Can_Save_Detail(PAY);
          return Res;
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
            if (Save_Record())
            {
                InitialisePage();
                Common.CommonFuction.ShowMessage("Record Update Sucessfully");
                Response.Redirect("~/GetUserAuthorisation.aspx", false);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to Update!please try again");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in update Data.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refreshing the page.\r\nSee error log for detail."));
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
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DDLSearch.Visible = true;
            Load_Serach_Control();
            TxtMasterCode.Visible = false;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = true;
            lblMode.Text = "Find";
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.HR_POSITION_MST oHR_POSITION_MST = new SaitexDM.Common.DataModel.HR_POSITION_MST();
            oHR_POSITION_MST.POSITION_CODE = ViewState["iPosition_CODE"].ToString().ToUpper();
            bool bResult = SaitexBL.Interface.Method.HR_POSITION_MST.DeletePOSITIONMaster(oHR_POSITION_MST);
            if (bResult)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Record Deleted Successfully");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);

        }
    }
    private void Load_Serach_Control()
    {
        try
        {
           DataTable DTable = SaitexBL.Interface.Method.PAYROLL_PARAMETERS.SearchBy_Record (oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE );
           DDLSearch.DataSource = DTable;
           DDLSearch.DataBind();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
    protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLSearch.SelectedValue.Trim().ToString() != "0")
            {
                DataTable DTable = SaitexBL.Interface.Method.PAYROLL_PARAMETERS.Load_Master_Record(DDLSearch.SelectedValue.Trim().ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE );
                if (DTable.Rows.Count > 0)
                {
                    DDLOpenYear.SelectedValue = DTable.Rows[0]["OPEN_YEAR"].ToString();
                    DDLOpenMonth.SelectedValue = DTable.Rows[0]["OPEN_MONTH"].ToString();
                    TxtFromDate.Text = DTable.Rows[0]["SALARY_FROMDATE"].ToString();
                    TxtToDate.Text = DTable.Rows[0]["SALARY_TODATE"].ToString();
                    TxtMasterCode.Text = DTable.Rows[0]["PMASTER_CODE"].ToString();
                    if (DTable.Rows[0]["ISACTIVE"].ToString() != "0")
                    {
                        ChkActive.Checked = true;
                    }
                    else
                    {
                        ChkActive.Checked = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
}
