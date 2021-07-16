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

public partial class Module_StartUp_CreateFinancialYear : System.Web.UI.Page
{

    SaitexDM.Common.DataModel.CM_FIN_YEAR_MST oCMFINYEARMST = new SaitexDM.Common.DataModel.CM_FIN_YEAR_MST();
    string strTUser = string.Empty;
    private static DataSet ds;

    private static bool IsInsert;
    private static bool IsView;
    private static bool IsModify;
    private static bool IsDelete;
    private static string UserCode;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
                if (!IsPostBack)
                { 
                    binddlcompany();
                    BindFinancialyear();
                    lblMode.Text = "Save";
                    chkStatus.Checked = true;
                }
        }
        catch (Exception ex)
        {           
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
    private void binddlcompany()
    {
        var dtCompanyinfo = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();
        if (dtCompanyinfo != null && dtCompanyinfo.Rows.Count > 0)
        {
            ddlcompany.Items.Clear();
            ddlcompany.Items.Insert(0, new ListItem("----Select----", "0"));
            ddlcompany.DataTextField = "COMP_NAME";
            ddlcompany.DataValueField = "COMP_CODE";
            ddlcompany.DataSource = dtCompanyinfo;
            ddlcompany.DataBind();
        }

    }
    private void bindddlbranch(string companyName)
    {
        var dtbranch = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
        if (dtbranch != null && dtbranch.Rows.Count > 0)
        {
            var dvbranchbycompany = new DataView(dtbranch);
            dvbranchbycompany.RowFilter = "COMP_CODE='" + companyName + "'";
            if (dvbranchbycompany != null && dvbranchbycompany.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataValueField = "BRANCH_CODE";
                ddlbranch.DataTextField = "BRANCH_NAME";
                ddlbranch.DataSource = dvbranchbycompany;
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, new ListItem("------Select-------", ""));

            }
            else
            {
                ddlbranch.Items.Clear();
                ddlbranch.Items.Insert(0, new ListItem("No Branch Available", ""));

            }

        }

    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedIndex != 0)
        {
            bindddlbranch(ddlcompany.SelectedValue.ToString());
        }
    }     
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {                
                    int iRecordFound = 0;
                    oCMFINYEARMST.FIN_YEAR_CODE = Common.CommonFuction.funFixQuotes(txtFinancialYearCode.Text.Trim());
                    oCMFINYEARMST.COMP_CODE = ddlcompany.SelectedValue.ToString();
                    if (ddlbranch.SelectedIndex != null && ddlbranch.SelectedIndex != 0)
                    {
                        oCMFINYEARMST.BRANCH_CODE = ddlbranch.SelectedValue.ToString();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Select Branch ');", true);
                    }
                    oCMFINYEARMST.FIN_DESC = Common.CommonFuction.funFixQuotes(txtFinancialDesc.Text.Trim());
                    var dtStartDate = System.DateTime.Now;
                    DateTime.TryParse(txtStartDate.Text, out dtStartDate);
                    oCMFINYEARMST.START_DATE = dtStartDate;
                    var dtEndDate = System.DateTime.Now;
                    DateTime.TryParse(txtEndDate.Text, out dtEndDate);
                    oCMFINYEARMST.END_DATE = dtEndDate;

                    if (chkStatus.Checked)
                    {
                        oCMFINYEARMST.STATUS = true;
                    }
                    else
                    {
                        oCMFINYEARMST.STATUS = false;
                    }

                    var dtyear = DateTime.Parse(txtStartDate.Text);
                    oCMFINYEARMST.YEAR = dtyear.Year;
                    oCMFINYEARMST.TUSER = "SUPER ADMIN";
                    bool result = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.InsertFinacialMaster(oCMFINYEARMST, out iRecordFound);
                    if (result)
                    {
                        BindFinancialyear();
                        ResetPage();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Financial year Saved Successfully');", true);
                        Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                        Response.Redirect("CreateOpenMonthAndYear.aspx", false);
                    }
                    else if (iRecordFound > 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Financial year Already Exists');", true);
                    }

               

            }
            catch
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Financial year Not Saved');", true);

            }
        }

    }
    private void BindFinancialyear()
    {
        var dtFinancialyear = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.GetFinacialMaster();
        if (dtFinancialyear != null && dtFinancialyear.Rows.Count > 0)
        {
            grdFinancialYear.DataSource = dtFinancialyear;
            grdFinancialYear.DataBind();

        }


    }
    private void ResetPage()
    {
        lblMode.Text = "Save";
        txtFinancialYearCode.Enabled = true;      
        txtFinancialYearCode.Enabled = true;
        ddlbranch.Enabled = true;
        ddlcompany.Enabled = true;
        ddlbranch.Items.Clear();
        txtEndDate.Text = "";
        txtFinancialDesc.Text = "";
        txtFinancialYearCode.Text = "";
        txtStartDate.Text = "";
       
        ddlcompany.SelectedIndex = -1;
        ddlbranch.SelectedIndex = -1;
        imgbtnSave.ImageUrl = "~/CommonImages/save.jpg";
    }
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {       
        var  sdt = DateTime.Parse(txtStartDate.Text);
        var endt = Common.CommonFuction.GetYearEndDate(sdt);
        txtEndDate.Text = endt.ToString();
    }
}
