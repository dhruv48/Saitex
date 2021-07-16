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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_HRMS_Pages_PrintSalrySlip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            DataSet DS = GetData();
            GetReport(DS);
            if (!Page.IsPostBack)
            {

            }
        }
    }
    private void GetReport(DataSet DS)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"\Saitex\Module\Admin\Reports\CrSalarySlipnew.rpt"));
        rDoc.SetDataSource(DS);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    private DataSet GetData()
    {
        string SalaryId = ""; string Year = ""; string EmpCode = ""; string SAL_MONTH = "", Branch = "", Department = "", CADDER_CODE = "";
        try
        {
            if (Request.QueryString["SalaryId"] != "" && Request.QueryString["SalaryId"] != null)
            {
                SalaryId = Request.QueryString["SalaryId"].ToString();
            }
            if (Request.QueryString["Year"] != "" && Request.QueryString["Year"] != null)
            {
                Year = Request.QueryString["Year"].ToString();
            }
            if (Request.QueryString["EmpCode"] != "" && Request.QueryString["EmpCode"] != null)
            {
                EmpCode = Request.QueryString["EmpCode"].ToString();
            }           
            if (Request.QueryString["MONTH"] != "" && Request.QueryString["MONTH"] != null)
            {
                SAL_MONTH = Request.QueryString["MONTH"].ToString();
            }
            if (Request.QueryString["DEPT_CODE"] != "" && Request.QueryString["DEPT_CODE"] != null)
            {
                Department = Request.QueryString["DEPT_CODE"].ToString();
            }
            if (Request.QueryString["BRANCH_CODE"] != "" && Request.QueryString["BRANCH_CODE"] != null)
            {
                Branch = Request.QueryString["BRANCH_CODE"].ToString();
            }
            if (Request.QueryString["CADDER_CODE"] != "" && Request.QueryString["CADDER_CODE"] != null)
            {
                CADDER_CODE = Request.QueryString["CADDER_CODE"].ToString();
            }
            DataSet DSet = SaitexBL.Interface.Method.HR_EMP_SAL_MST.GetSalaryDetail(EmpCode, Year, SalaryId, SAL_MONTH, Branch, Department, CADDER_CODE);
            return DSet;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}