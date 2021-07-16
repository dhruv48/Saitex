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

public partial class Admin_UserControls_GetUserAuthorisation : System.Web.UI.UserControl
{
   
    private static string UserCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                lblErrorMessage.Text = "";
                lblMessage.Text = "";
                UserCode = Session["urLoginId"].ToString().Trim();
                lblUserWelcome.Text = Session["usrNames"].ToString().Trim();
                if (!IsPostBack)
                {
                    imgbtnGetAccess.Focus();
                    GetData();
                    BindCompanyByUser();
                    BindDepartmentByUser();
                    getLoginDetail();
                }
            }
            else
            {
                errorLog.ErrHandler.WriteError("Session Expired");
                Response.Redirect("~/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page. \\r\\n see error log for detail."));
        }
    }
    private void GetData()
    
    
    
    {
        try
        {
            string msg = string.Empty;

            DataTable dtUserAccess = SaitexBL.Interface.Method.UserAuthorisation.GetUserCompanyAuthorisation();
            if (dtUserAccess != null && dtUserAccess.Rows.Count > 0)
            {
                DataView dv = new DataView(dtUserAccess);
                dv.RowFilter = "USER_CODE='" + UserCode + "'";
                if (dv.Count > 0)
                {
                    ViewState["UserCompanyAccess"] = dv.ToTable();
                }
            }
            else
            {
                //     lblErrorMessage.Text = lblErrorMessage + "Sorry No Company Assigned to you.<br />Contact Administrator";
                msg += @"\r\nSorry No Company Assigned to you.";
            }

            DataTable dtUserDeptAccess = SaitexBL.Interface.Method.UserAuthorisation.GetUserDepartmentAuthorisation();
            if (dtUserDeptAccess != null && dtUserDeptAccess.Rows.Count > 0)
            {
                DataView dv = new DataView(dtUserDeptAccess);
                dv.RowFilter = "USER_CODE='" + UserCode + "'";
                if (dv.Count > 0)
                {
                    ViewState["UserDepartmentAccess"] = dv.ToTable();
                }
            }
            else
            {
                msg += @"\r\Sorry No department Assigned to you.";
                //   lblErrorMessage.Text = lblErrorMessage + "Sorry No department Assigned to you.<br />Contact Administrator";
            }
            if (msg != string.Empty)
            {
                msg += @"\r\nContact Administrator";
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch
        {
            throw;
        }
    }
    private void getLoginDetail()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.CM_LOG_DTL.GetLastTOPLoginDetail(UserCode);

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(dt.Rows[0]["COMP_CODE"].ToString()));
                BindBranchByUserAndCompany();
                ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByText(dt.Rows[0]["BRANCH_NAME"].ToString()));
                ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(dt.Rows[0]["DEPT_CODE"].ToString()));
                BindFinYearByBranch();
                ddlFinYear.SelectedIndex = ddlFinYear.Items.IndexOf(ddlFinYear.Items.FindByValue(dt.Rows[0]["FIN_YEAR_CODE"].ToString()));

            }
        }
        catch
        {
            throw;
        }
    }
    private void BindCompanyByUser()
    {
        try
        {
            if (ViewState["UserCompanyAccess"] != null)
            {
                DataTable dtUserAccessCompany = (DataTable)ViewState["UserCompanyAccess"];

                ddlCompany.Items.Clear();
                ddlCompany.Items.Add("Select Company");
                DataTable dtTempComp = new DataTable();
                dtTempComp.Columns.Add("COMP_CODE", typeof(string));
                dtTempComp.Columns.Add("COMP_NAME", typeof(string));

                if (dtUserAccessCompany != null && dtUserAccessCompany.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUserAccessCompany.Rows)
                    {
                        DataView dv = new DataView(dtTempComp);
                        dv.RowFilter = "COMP_CODE='" + dr["COMP_CODE"].ToString() + "'";
                        if (dv.Count == 0)
                        {
                            DataRow dr1 = dtTempComp.NewRow();
                            dr1["COMP_CODE"] = dr["COMP_CODE"];
                            dr1["COMP_NAME"] = dr["COMP_NAME"];
                            dtTempComp.Rows.Add(dr1);
                        }
                    }
                    ddlCompany.DataSource = dtTempComp;
                    ddlCompany.DataTextField = "COMP_NAME";
                    ddlCompany.DataValueField = "COMP_CODE";
                    ddlCompany.DataBind();
                }
            }
            else
            {
                //lblErrorMessage.Text = "No company assigned to you<br />Contact admin for details";
                Common.CommonFuction.ShowMessage(@"No company assigned to you.\r\nContact admin for details");
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindBranchByUserAndCompany();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in company selection. \\r\\n see error log for detail."));
        }
    }
    private void BindBranchByUserAndCompany()
    {
        try
        {
            if (ddlCompany.SelectedIndex != 0)
            {
                string CompanyId = ddlCompany.SelectedValue.Trim();
                if (ViewState["UserCompanyAccess"] != null)
                {
                    DataTable dtUserAccessBranch = (DataTable)ViewState["UserCompanyAccess"];
                    if (dtUserAccessBranch != null && dtUserAccessBranch.Rows.Count > 0)
                    {
                        DataView dvUserAccess = new DataView(dtUserAccessBranch);
                        dvUserAccess.RowFilter = "COMP_CODE='" + CompanyId + "'";

                        if (dvUserAccess.Count > 0)
                        {
                            ddlBranch.Items.Clear();
                            ddlBranch.Items.Add("Select Branch");

                            ddlBranch.DataSource = dvUserAccess;
                            ddlBranch.DataTextField = "BRANCH_NAME";
                            ddlBranch.DataValueField = "cmbNamePre";
                            ddlBranch.DataBind();
                        }
                    }
                }
                else
                {
                    //lblErrorMessage.Text = "No Branch assigned to you<br />Contact admin for details";
                    Common.CommonFuction.ShowMessage(@"No Branch assigned to you.\r\nContact admin for details");
                }
            }
            else
            {
                lblErrorMessage.Text = "No Company selected";
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add("Select");
                ddlFinYear.Items.Clear();
                ddlFinYear.Items.Add("Select");
            }
        }
        catch
        {
            throw;
        }
    }
    private void BindDepartmentByUser()
    {
        try
        {
            if (ViewState["UserDepartmentAccess"] != null)
            {
                DataTable dtUserAccessDepartment = (DataTable)ViewState["UserDepartmentAccess"];

                ddlDepartment.Items.Clear();
                ddlDepartment.Items.Add("Select Department");
                if (dtUserAccessDepartment != null && dtUserAccessDepartment.Rows.Count > 0)
                {
                    DataTable dtTempDept = new DataTable();
                    dtTempDept.Columns.Add("DEPT_CODE", typeof(string));
                    dtTempDept.Columns.Add("DEPT_NAME", typeof(string));

                    foreach (DataRow dr in dtUserAccessDepartment.Rows)
                    {
                        DataView dv = new DataView(dtTempDept);
                        dv.RowFilter = "DEPT_CODE='" + dr["DEPT_CODE"].ToString() + "'";
                        if (dv.Count == 0)
                        {
                            DataRow dr1 = dtTempDept.NewRow();
                            dr1["DEPT_CODE"] = dr["DEPT_CODE"];
                            dr1["DEPT_NAME"] = dr["DEPT_NAME"];
                            dtTempDept.Rows.Add(dr1);
                        }
                    }

                    ddlDepartment.DataSource = dtTempDept;
                    ddlDepartment.DataTextField = "DEPT_NAME";
                    ddlDepartment.DataValueField = "DEPT_CODE";
                    ddlDepartment.DataBind();
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage(@"No Department assigned to you.\r\nContact admin for details");
            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnGetAccess_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           if (Validate())
           {
            SaveLoginDetail();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Authorisation. \\r\\n see error log for detail."));
        }
    }

    private void SaveLoginDetail()
    {
        try
        {
            SaitexDM.Common.DataModel.CM_LOG_DTL oCM_LOG_DTL = new SaitexDM.Common.DataModel.CM_LOG_DTL();

            DateTime LoginDate = System.DateTime.Now.Date;
            string LoginTime = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString();

            string Ip = string.Empty;
            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                Ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                if (!string.IsNullOrEmpty(Ip))
                {
                    string[] ipRange = Ip.Split(',');
                    int le = ipRange.Length - 1;
                    string trueIP = ipRange[le];
                }
                else
                {
                    Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
            }
            else
            {
                Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

            string sComp_code = string.Empty;
            string sComp_Name = string.Empty;
            if (ddlCompany.SelectedValue != "Select Company")
            {
                sComp_code = ddlCompany.SelectedValue.Trim();
                sComp_Name = ddlCompany.SelectedItem.Text.Trim();
            }

          
            string sBranch_Name = string.Empty;
            string sBranch_code = string.Empty;
            string sSEQ_PREFIX = string.Empty;

            if (ddlBranch.SelectedValue != "Select Branch")
            {
                string Combined = ddlBranch.SelectedValue.Trim();
                string cString = Combined;
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
                sBranch_code = arrString[0].ToString();
                sSEQ_PREFIX   = arrString[1].ToString();
                sBranch_Name = ddlBranch.SelectedItem.Text.Trim();
            }

            string sDept_code = string.Empty;
            string sDept_Name = string.Empty;
            if (ddlDepartment.SelectedValue != "Select Department")
            {
                sDept_code = ddlDepartment.SelectedValue.Trim();
                sDept_Name = ddlDepartment.SelectedItem.Text.Trim();
            }

            string sFin_code = string.Empty;
            string sFin_Year = string.Empty;
            if (ddlFinYear.SelectedValue != "Select Financial Year")
            {
                sFin_code = ddlFinYear.SelectedValue.Trim();
                sFin_Year = ddlFinYear.SelectedItem.Text.Trim();
            }

            oCM_LOG_DTL.COMP_CODE = sComp_code;
            oCM_LOG_DTL.BRANCH_CODE = sBranch_code;
            oCM_LOG_DTL.USER_CODE = UserCode;
            oCM_LOG_DTL.FIN_YEAR_CODE = sFin_code;
            oCM_LOG_DTL.DPT_CODE = sDept_code;
            oCM_LOG_DTL.LOG_IN_DATE = LoginDate;
            oCM_LOG_DTL.LOG_IN_TIME = LoginTime;
            oCM_LOG_DTL.SYSTEM_IP = Ip;

            bool iRecordEffected = false;
            int LOGINDETAILID = 0;
            iRecordEffected = SaitexBL.Interface.Method.CM_LOG_DTL.SaveLoginDetail(oCM_LOG_DTL, out LOGINDETAILID);
            if (iRecordEffected)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();

                string strDup = string.Empty;
                if (sFin_code != string.Empty)
                {
                    strDup = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.GetFinacial_START_DATE_ByFIN_YEAR_CODE(sFin_code);

                    oUserLoginDetail.DT_STARTDATE = DateTime.Parse(strDup);
                }

                oUserLoginDetail.LOGINDETAILID = LOGINDETAILID.ToString();
                oUserLoginDetail.UserCode = UserCode;
                oUserLoginDetail.Username = Session["usrNames"].ToString();
                oUserLoginDetail.COMP_CODE = sComp_code;
                oUserLoginDetail.VC_COMPANYNAME = sComp_Name;
                oUserLoginDetail.CH_BRANCHCODE = sBranch_code;
                oUserLoginDetail.VC_BRANCHNAME = sBranch_Name;
                oUserLoginDetail.VC_FINANCIALYEARCODE = sFin_code;
                oUserLoginDetail.FinYear = sFin_Year;
                oUserLoginDetail.VC_DEPARTMENTCODE = sDept_code;
                oUserLoginDetail.VC_DEPARTMENTNAME = sDept_Name;
                oUserLoginDetail.DT_LOGINDATE = LoginDate;
                oUserLoginDetail.DT_LOGINTIME = LoginTime;
                oUserLoginDetail.UserType = Session["urType"].ToString();
                oUserLoginDetail.SEQ_PREFIX = sSEQ_PREFIX;
                string Combined = ddlBranch.SelectedValue.Trim();
                string cString = Combined;
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
              

                oUserLoginDetail.BRANCH_PRTYCODE = arrString[2].ToString();
                oUserLoginDetail.BRANCH_PRTYNAMEADDRES = arrString[3].ToString(); 
                //////////////////////////////// To get Logo of the company  //////////////////////////////////// 

                if (sComp_code != string.Empty)
                {
                    SaitexDM.Common.DataModel.CM_COMPANY_MST oCM_COMPANY_MST = new SaitexDM.Common.DataModel.CM_COMPANY_MST();
                    oCM_COMPANY_MST.COMP_CODE = sComp_code;
                    DataTable dt = SaitexBL.Interface.Method.CM_COMP_MST.GetCompanyMasterByCompCode(oCM_COMPANY_MST);

                    oUserLoginDetail.COMP_LOGO = dt.Rows[0]["LOGO_PATH"].ToString();
                    oUserLoginDetail.COMP_ADD = dt.Rows[0]["COMP_ADD"].ToString();
                }
                else
                {
                    oUserLoginDetail.COMP_LOGO = string.Empty;
                    oUserLoginDetail.COMP_ADD = string.Empty;
                }
                oUserLoginDetail.DEVELOPER_COMP = "Design and Developed By Jingle Infosolutions Pvt. Ltd.";
                oUserLoginDetail.DEVELOPER_WEB = "http://www.jingleinfo.com";
                oUserLoginDetail.WORKS_ADDRESS = "VPO BHATIAN, TEH NALAGARH, DISTT, SOLAN, HP"; 
                //**********************Change by viresh***********************
                if (sComp_code != string.Empty)
                {
                    DataTable DTable = SaitexBL.Interface.Method.CM_BRANCH_MST.Get_Opening_YearMonth(oUserLoginDetail);
                    if (DTable.Rows.Count > 0)
                    {
                        oUserLoginDetail.OPEN_YEAR = DTable.Rows[0]["OPEN_YEAR"].ToString();
                        oUserLoginDetail.OPEN_MONTH = DTable.Rows[0]["OP_MONTH"].ToString();
                        oUserLoginDetail.OPEN_MONTH_NO = DTable.Rows[0]["MONTH_NO"].ToString();
                        oUserLoginDetail.SALARY_FROMDATE = DTable.Rows[0]["SALARY_FROMDATE"].ToString();
                        oUserLoginDetail.SALARY_TODATE = DTable.Rows[0]["SALARY_TODATE"].ToString();
                    }
                }
                //////////////////////////////////////////////////////////////////////////
                Session["LoginDetail"] = oUserLoginDetail;
                /////////////////////////////////////////////////////////////////////////////////////////////////
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch
        {
            throw;
        }
    }
    private bool Validate()
    {
        try
        {
            bool IsValid = false;
            if (Page.IsValid)
            {
                if (ddlCompany.SelectedIndex != 0)
                {
                    if (ddlBranch.SelectedIndex != 0)
                    {
                        if (ddlFinYear.SelectedIndex != 0)
                        {
                            if (ddlDepartment.SelectedIndex != 0)
                            {
                                return true;
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Pls select department to enter");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Pls select Financial Year to enter");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Pls select Branch to enter");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Pls select Company to enter");
                }
            }
            return IsValid;
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
            Response.Redirect("~/default.aspx", false);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leaving This Page. \\r\\n see error log for detail."));
        }
    }
    private void BindFinYearByBranch()
    {
        try
        {
            if (ddlBranch.SelectedIndex != 0)
            {

                string CompanyId = ddlCompany.SelectedValue.Trim();
                string Combined = ddlBranch.SelectedValue.Trim();
                string cString = Combined;
                char[] splitter = { '@' };
                string[] arrString = cString.Split(splitter);
                string BranchCode = arrString[0].ToString();
                
                
              

                DataTable dtFinancialYearMaster = new DataTable();

                dtFinancialYearMaster = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.GetFinacialMasterByBranch(CompanyId, BranchCode);
                dtFinancialYearMaster.TableName = "FinancialYearMaster";

                if (dtFinancialYearMaster != null && dtFinancialYearMaster.Rows.Count > 0)
                {

                    ddlFinYear.Items.Clear();
                    ddlFinYear.Items.Add("Select Year");

                    ddlFinYear.DataSource = dtFinancialYearMaster;
                    ddlFinYear.DataTextField = "FinYear";
                    ddlFinYear.DataValueField = "FIN_YEAR_CODE";
                    ddlFinYear.DataBind();
                }
            }
            else
            {
                ddlFinYear.Items.Clear();
                ddlFinYear.Items.Add("Select");
                lblErrorMessage.Text = "No Branch selected";
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindFinYearByBranch();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Branch selection. \\r\\n see error log for detail."));

        }
    }
}
