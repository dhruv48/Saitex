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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;


public partial class Module_HRMS_Pages_EmpLoanRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            DataSet  dt = GetData();
            GetReport(dt);
            if (!Page.IsPostBack)
            {

            }
        }
    }
    private void GetReport(DataSet  dS)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\RptEmpLoan.rpt"));
        rDoc.SetDataSource(dS);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataSet  GetData()
    {
        try
        {
            string LOAN_ID = string.Empty; string EmpCode = string.Empty;
            if (Request.QueryString["LOAN_ID"] != "" && Request.QueryString["LOAN_ID"] != null)
            {
                LOAN_ID = Request.QueryString["LOAN_ID"].Trim().ToString();
            }
            if (Request.QueryString["EmpCode"] != "" && Request.QueryString["EmpCode"] != null)
            {
                EmpCode = Request.QueryString["EmpCode"].Trim().ToString();
            }
            else
            {
                EmpCode = Session["EmpCode"].ToString();
            }         
           
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_LOAN.Load_Loan_Detail(EmpCode ,"",LOAN_ID ,"2");
            DataTable EmpTable = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(EmpCode);
            DataSet Ds = new DataSet();
            Ds.Tables.Add(DTable);
            Ds.Tables.Add(EmpTable);
            Ds.Tables[0].TableName = "LoanTable";
            Ds.Tables[1].TableName = "EmpTable";
            return Ds;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
