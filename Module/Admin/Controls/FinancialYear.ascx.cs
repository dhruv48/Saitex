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

public partial class Admin_UserControls_FinancialYear : System.Web.UI.UserControl
{
    
    
    SaitexDM.Common.DataModel.CM_FIN_YEAR_MST oCMFINYEARMST = new SaitexDM.Common.DataModel.CM_FIN_YEAR_MST();
    string strTUser = string.Empty;
    private static DataSet ds;
   
    private static bool IsInsert;
    private static bool IsView;
    private static bool IsModify;
    private static bool IsDelete;
    private static string UserCode;
    private void GetAccessRights()
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                string LocalPath = Request.Url.LocalPath;
                string NavigateUrl;
                NavigateUrl = LocalPath.Replace("/stl", "~");

                UserCode = Session["urLoginId"].ToString();

                IsInsert = true;
                IsView = true;
                IsDelete = true;
                IsModify = true;
                bool Result = SaitexBL.Interface.Method.UserNavigationRight.IsAccessAllowed(UserCode, NavigateUrl, out IsView, out IsInsert, out IsModify, out IsDelete);
                if (!Result)
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "myfun", "window.alert('Sorry you dont have right to view this page');", true);

                    string url = "Welcome.aspx";
                    Response.AddHeader("REFRESH", "2;URL='" + url + "'");
                   // lblErrorMessage.Text = "<b>Sorry you dont have right to view this page";
                }
            }
            else
            {
                Response.Redirect("/Saitex/Default.aspx", false);
            }
        }
        catch (Exception ex)
        { //lblErrorMessage.Text = ex.Message;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                if (!IsPostBack)
                {
                    SaitexDM.Common.DataModel.UserLoginDetail  oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                    ViewState["strTUser"] = oUserLoginDetail.UserCode;
                    binddlcompany();
                    BindFinancialyear();
                    lblMode.Text = "Save"; 

                }

            }
            else
            {
                Response.Redirect("/Saitex/Default.aspx", false);

            }
        }
        catch (Exception ex)
        {
            //lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
    private void binddlcompany()
    {
           DataTable dtCompanyinfo=  SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMaster();
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
        DataTable dtbranch =  SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
          if (dtbranch != null &&dtbranch.Rows.Count > 0)
          {
              DataView dvbranchbycompany = new DataView(dtbranch);
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
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/FinancialYear.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ResetPage();
        BindFinancialyear(); 
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
    protected void grdFinancialYear_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            string FinancialYearCode = e.CommandArgument.ToString();
            if (e.CommandName == "EditFinYear")
            {
                lblMode.Text = "Update"; 
                txtFinancialYearCode.Enabled = false;
                ddlbranch.Enabled = false;
                ddlcompany.Enabled = false; 
                imgbtnSave.ImageUrl = "~/CommonImages/edit1.jpg"; 
                FillEditData(FinancialYearCode);
            }
            else if (e.CommandName == "DeleteFinYear")
            {
                //   if (IsDelete)
                DeleteFinancialYearData(FinancialYearCode);
                //   else
                //       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('You dont have delete rights')", true);

            }
        }
        catch (Exception ex)
        {
            //lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (imgbtnSave.ImageUrl != "~/CommonImages/edit1.jpg")
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

                    DateTime dtStartDate = System.DateTime.Now;
                    DateTime.TryParse(txtStartDate.Text, out dtStartDate);
                    oCMFINYEARMST.START_DATE = dtStartDate;

                    DateTime dtEndDate = System.DateTime.Now;
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

                    DateTime dtyear = DateTime.Parse(txtStartDate.Text);

                    oCMFINYEARMST.YEAR = dtyear.Year;


                    oCMFINYEARMST.TUSER = ViewState["strTUser"].ToString();
                    bool result = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.InsertFinacialMaster(oCMFINYEARMST, out iRecordFound);
                    if (result)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Financial year Saved Successfully');", true);
                        BindFinancialyear();
                        ResetPage();
                    }
                    else if (iRecordFound > 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Financial year Already Exists');", true);
                    }

                }
                else
                {

                    int iRecordFound = 0;
                    oCMFINYEARMST.FIN_YEAR_CODE = Common.CommonFuction.funFixQuotes(txtFinancialYearCode.Text.Trim());
                    oCMFINYEARMST.COMP_CODE = ddlcompany.SelectedValue.ToString();
                    oCMFINYEARMST.BRANCH_CODE = ddlbranch.SelectedValue.ToString();
                    oCMFINYEARMST.FIN_DESC = Common.CommonFuction.funFixQuotes(txtFinancialDesc.Text.Trim());

                    DateTime dtStartDate = System.DateTime.Now;
                    DateTime.TryParse(txtStartDate.Text, out dtStartDate);
                    oCMFINYEARMST.START_DATE = dtStartDate;

                    DateTime dtEndDate = System.DateTime.Now;
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

                    DateTime dtyear = DateTime.Parse(txtStartDate.Text);

                    oCMFINYEARMST.YEAR = dtyear.Year;

                    oCMFINYEARMST.TUSER = ViewState["strTUser"].ToString();
                    bool result = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.UpdateFinacialMaster(oCMFINYEARMST, out iRecordFound);
                    if (result)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Financial year Updated Successfully');", true);
                        BindFinancialyear();
                    }
                    else if (iRecordFound > 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Financial year Already Exists');", true);
                    }

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
        DataTable dtFinancialyear = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.GetFinacialMaster();
        if (dtFinancialyear != null && dtFinancialyear.Rows.Count > 0)
        {
            grdFinancialYear.DataSource = dtFinancialyear;
            grdFinancialYear.DataBind(); 
        
        }
    
    
    }
    private void FillEditData(string Code)
    {
        DataTable dtFinacialMaster = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.GetFinacialMaster();
        DataView dvFin = new DataView(dtFinacialMaster);
        dvFin.RowFilter = "FIN_YEAR_CODE='" + Code + "'";
        if (dvFin.Count > 0)
        {
            binddlcompany(); 
            
            ddlcompany.SelectedValue = dvFin[0]["COMP_CODE"].ToString();
            bindddlbranch(ddlcompany.SelectedValue.ToString());    
            ddlbranch.SelectedValue = dvFin[0]["BRANCH_CODE"].ToString();
            txtEndDate.Text = DateTime.Parse(dvFin[0]["END_DATE"].ToString()).ToShortDateString();
            txtFinancialDesc.Text = dvFin[0]["FIN_DESC"].ToString();
            txtFinancialYearCode.Text = dvFin[0]["FIN_YEAR_CODE"].ToString();
            txtStartDate.Text = DateTime.Parse(dvFin[0]["START_DATE"].ToString()).ToShortDateString();
            if (dvFin[0]["STATUS"].ToString() == "0")
                chkStatus.Checked = false;
            else
                chkStatus.Checked = true;

        }
    }
    private void DeleteFinancialYearData(string Code)
    {
        try
        {
            oCMFINYEARMST.FIN_YEAR_CODE = Code;
            oCMFINYEARMST.TUSER = ViewState["strTUser"].ToString(); 


            bool result = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.DeleteFinacialMaster(oCMFINYEARMST);

            if (result)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Financial year Deleted Successfully');", true);
                BindFinancialyear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Some Problem in Deleting Financial year!!');", true);
            }
            //strSQL = "UPDATE TBLFINANCIALYEARMASTER SET CH_DELETESTATUS = :CH_DELETESTATUS where VC_FINANCIALYEARCODE = :VC_FINANCIALYEARCODE";

           
           
            
        }
        catch (Exception ex)
        {
            //lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
       
    }
    //protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);

    //    //for (int loop = 0; grdFinancialYear.Rows.Count > loop; loop++)
    //    //{
    //    //    LinkButton lnk = grdFinancialYear.Rows[loop].FindControl("lbtnDelete") as LinkButton;
    //    //    lnk.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Delete this record ? ')");
        
    //    //}
    //    imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit? ')");
    //    imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear Form? ')");
    //    if (imgbtnSave.ImageUrl == "~/CommonImages/save.jpg")
    //    {

    //        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save? ')");
    //    }
    //    else
    //    {
    //        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update? ')");
        
    //    }
    
    //}
    private void ResetPage()
    {
        lblMode.Text = "Save"; 
        txtFinancialYearCode.Enabled = true;
        //UpdateMode = false;
        txtFinancialYearCode.Enabled = true;
        ddlbranch.Enabled = true;
        ddlcompany.Enabled = true;
        ddlbranch.Items.Clear();   
        txtEndDate.Text = "";
        txtFinancialDesc.Text = "";
        txtFinancialYearCode.Text = "";
        txtStartDate.Text = "";
        //lblErrorMessage.Text = "";
        //lblMessage.Text = "";
        ddlcompany.SelectedIndex = -1;
        ddlbranch.SelectedIndex = -1;
        imgbtnSave.ImageUrl = "~/CommonImages/save.jpg"; 
    }
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    { 
        
        DateTime sdt;
        sdt = DateTime.Parse(txtStartDate.Text);
        DateTime endt = Common.CommonFuction.GetYearEndDate(sdt);
        //SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        //DateTime startdate = oUserLoginDetail.DT_STARTDATE;
        //DateTime Enddate = Common.CommonFuction.GetYearEndDate(startdate);

        txtEndDate.Text = endt.ToString();
    }
}
