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

public partial class Module_HRMS_Reports_PrintPayRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {
            DataSet DS = GetData();
            GetReport(DS);            
        }
    }
    private void GetReport(DataSet DS)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            //rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\PayRegister.rpt"));
            rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\SalaryPayRegister.rpt"));            
            rDoc.SetDataSource(DS);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message.ToString());
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    private DataSet GetData()
    {      
        string SearchQuery = string.Empty;
        try
        {
            SearchQuery = "WHERE DEL_STATUS=0 ";
            if (Request.QueryString["Year"] != "" && Request.QueryString["Year"] != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " SAL_YEAR='" + Request.QueryString["Year"].ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND SAL_YEAR='" + Request.QueryString["Year"].ToString() + "'";
                }                        
            }
            if (Request.QueryString["MONTH"] != "" && Request.QueryString["MONTH"] != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " LPAD(MONTHNO,2,0)=LPAD(" + Request.QueryString["MONTH"].ToString()+",2,0)";   
                }
                else
                {
                    SearchQuery = SearchQuery + " AND LPAD(MONTHNO,2,0)=LPAD(" + Request.QueryString["MONTH"].ToString() + ",2,0)";   
                }
            }
            if (Request.QueryString["SalaryId"] != "" && Request.QueryString["SalaryId"] != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " SAL_SLIP_MST_ID='" + Request.QueryString["SalaryId"].ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND SAL_SLIP_MST_ID='" + Request.QueryString["SalaryId"].ToString() + "'";
           }                
            }
           
            if (Request.QueryString["EmpCode"] != "" && Request.QueryString["EmpCode"] != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " EMP_CODE='" + Request.QueryString["EmpCode"].ToString() + "'";
                }
                else
                {
                    SearchQuery = SearchQuery + " AND EMP_CODE='" + Request.QueryString["EmpCode"].ToString() + "'";
                }                 
            }
            if (Request.QueryString["CADDER_CODE"] != "" && Request.QueryString["CADDER_CODE"] != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " CADDER_CODE=" + Request.QueryString["CADDER_CODE"].ToString();
                }
                else
                {
                    SearchQuery = SearchQuery + " AND CADDER_CODE=" + Request.QueryString["CADDER_CODE"].ToString();
                }
            }
            if (Request.QueryString["DEPT_CODE"] != "" && Request.QueryString["DEPT_CODE"] != null)
            {
                if (SearchQuery == string.Empty)
                {
                    SearchQuery = SearchQuery + " DEPT_CODE=" + Request.QueryString["DEPT_CODE"].ToString() ;
                }
                else
                {
                    SearchQuery = SearchQuery + " AND DEPT_CODE=" + Request.QueryString["DEPT_CODE"].ToString();
                }
            }
            DataSet DSet = SaitexBL.Interface.Method.HR_EMP_SAL_MST.Load_PayRegister(SearchQuery);
            return DSet;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
