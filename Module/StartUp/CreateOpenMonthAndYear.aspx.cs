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

public partial class Module_StartUp_CreateOpenMonthAndYear : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.PAYROLL_PARAMETERS PAY = new SaitexDM.Common.DataModel.PAYROLL_PARAMETERS();
    string COMP_CODE=string.Empty;
    string BRANCH_CODE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            imgbtnSave.Attributes.Add("onclick", "return validate()");
            var branchDT = SaitexBL.Interface.Method.CM_BRANCH_MST.GET_BRANCH_FOR_STARTUP();
            var cmpDT = SaitexBL.Interface.Method.CM_COMP_MST.GET_COMPANY_FOR_STARTUP();
            if (branchDT.Rows.Count > 0 && cmpDT.Rows.Count > 0)
            {
                COMP_CODE = cmpDT.Rows[0]["COMP_CODE"].ToString();
                BRANCH_CODE = branchDT.Rows[0]["BRANCH_CODE"].ToString();
            
            }
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
            var DTable = SaitexBL.Interface.Method.PAYROLL_PARAMETERS.Load_Master_Record("", COMP_CODE, BRANCH_CODE);
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
                Response.Redirect("CreateAdminUser.aspx", false);
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
                    PAY.ISACTIVE = '0';
                }
                PAY.TUSER = "ADMIN USER";
                PAY.COMP_CODE = COMP_CODE ;
                PAY.BRANCH_CODE = BRANCH_CODE;
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
            bool Res = SaitexBL.Interface.Method.PAYROLL_PARAMETERS.Can_Save_Detail(PAY);
            return Res;
        }
        catch
        {
            throw;
        }

    }
  
   
}
