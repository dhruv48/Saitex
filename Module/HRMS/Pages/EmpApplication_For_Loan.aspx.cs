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


public partial class Module_HRMS_Pages_EmpApplication_For_Loan : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    string User_Code = string.Empty;
    private static string POSITION = string.Empty;
    string ReportTo = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            HR_EMP_MST.EMP_CODE = Session["EmpCode"].ToString();
            HR_EMP_MST.POSITION = Session["POSITION"].ToString();
            ReportTo = Session["ReportTo"].ToString();
            User_Code = HR_EMP_MST.EMP_CODE;
            POSITION = HR_EMP_MST.POSITION;
            if (!Page.IsPostBack)
            {
                Load_Detail();
                Load_lOAN_Detail();
            }
        }
        else
        {
            Response.Redirect("/Saitex/Module/HRMS/Pages/Default.aspx", false);
        }

    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate .Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    }
    private void Load_Detail()
    {
        try
        {
            tdSave.Visible = true ;
            tdUpdate.Visible = false;
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_LOAN.Load_Loan_Detail(User_Code, POSITION, "", "3");
            DataTable EmpTable = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(User_Code);
            if (EmpTable.Rows.Count > 0)
            {
                TxtContactNo.Text = EmpTable.Rows[0]["PERM_TEL_NO"].ToString();
                TxtDept.Text = EmpTable.Rows[0]["DEPT_NAME"].ToString();
                TxtDesig.Text = EmpTable.Rows[0]["DESIG_NAME"].ToString();               
                TxtDOJ.Text = EmpTable.Rows[0]["JoinDate"].ToString();
                TxtEmpCode.Text = EmpTable.Rows[0]["EMP_CODE"].ToString();
                TxtEmpName.Text = EmpTable.Rows[0]["EMPLOYEENAME"].ToString();
                TxtGrade.Text = EmpTable.Rows[0]["GRADE"].ToString();
                TxtGrossSalary.Text = EmpTable.Rows[0]["GROSSSALARY"].ToString();
                TxtParmanentAdd.Text = EmpTable.Rows[0]["PADDRESS"].ToString();
                TxtPresentAddress.Text = EmpTable.Rows[0]["PREADDRESS"].ToString();
                           
            }
            if (DTable.Rows.Count > 0)
            {
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                TxtLoanId.Text = DTable.Rows[0]["LOAN_ID"].ToString();
                TxtLoanPurpose.Text = DTable.Rows[0]["LOAN_PURPOSE"].ToString();
                TxtRepayment.Text = DTable.Rows[0]["RE_PAYMENT"].ToString();
                TxtReqAmt.Text = DTable.Rows[0]["LOAN_AMOUNT"].ToString();
                TxtMonthlyInst.Text = DTable.Rows[0]["MONTHLY_INST"].ToString();
                TxtDuration.Text = DTable.Rows[0]["INST_DURATION"].ToString();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        Save_Records();
    }
    private void Save_Records()
    {
        if (Page.IsValid)
        {
            if (ChkAgree.Checked)
            {
                SaitexDM.Common.DataModel.HR_EMP_LOAN HR_EMP_LOAN = new SaitexDM.Common.DataModel.HR_EMP_LOAN();
                HR_EMP_LOAN.EMP_CODE = Session["EmpCode"].ToString();
                HR_EMP_LOAN.LOAN_AMOUNT = decimal .Parse(TxtReqAmt.Text.ToString());
                HR_EMP_LOAN.LOAN_PURPOSE = TxtLoanPurpose.Text.ToString();
                HR_EMP_LOAN.RE_PAYMENT = TxtRepayment.Text.ToString();
                HR_EMP_LOAN.MONTHLY_INST = decimal.Parse(TxtMonthlyInst.Text.ToString());
                HR_EMP_LOAN.INST_DURATION = TxtDuration.Text.ToString();
                HR_EMP_LOAN.LOAN_ID = decimal.Parse(TxtLoanId.Text.ToString());
                HR_EMP_LOAN.APPROVED_BY = ReportTo.ToString();
                bool Res = SaitexBL.Interface.Method.HR_EMP_LOAN.Save_Loan_Detail(HR_EMP_LOAN);
                if (Res)
                {
                    Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                    Load_lOAN_Detail();
                    Clear_Field();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Unable to save!try agin");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please accept term and condition");
            }

        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "";
        URL = "EmpLoanRpt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear_Field();  
    }
    private void Clear_Field()
    {
        TxtReqAmt.Text = "0";
        TxtRepayment.Text = "";
        TxtLoanPurpose.Text = "";
        TxtDuration.Text = "";
        TxtMonthlyInst.Text = "0";
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
                Response.Redirect("/Saitex/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
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
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        Save_Records();
    }

    private void Load_lOAN_Detail()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_LOAN.Load_Loan_Detail(User_Code, POSITION, "", "2");
            gvReportDisplayGrid.DataSource = DTable;
            gvReportDisplayGrid.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReportDisplayGrid.PageIndex = e.NewPageIndex;
    }  
}

