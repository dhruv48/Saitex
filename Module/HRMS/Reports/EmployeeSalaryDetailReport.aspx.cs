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
using errorLog;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.OracleClient;
public partial class Module_HRMS_Reports_EmployeeSalaryDetailReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["promotiondt"] != null)
        {
            DataTable dt = (DataTable)Session["promotiondt"];
            string EMP_CODE = dt.Rows[0]["EMP_CODE"].ToString();
            string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
            string DESIG_CODE = dt.Rows[0]["DESIG_CODE"].ToString();
            string BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
            DataTable dtrportdat = GetData(EMP_CODE, DEPT_CODE, DESIG_CODE, BRANCH_CODE);

            //DataTable dtrportdat = GetData();       
            GetReport(dtrportdat);
        }
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"EmpSlryDtl.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    //private DataTable GetData()
    private DataTable GetData(string EMP_CODE, string DEPT_CODE, string DESIG_CODE, string BRANCH_CODE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_EMP_SAL_MST.EmpSlryDTlRpt(EMP_CODE, DEPT_CODE, DESIG_CODE, BRANCH_CODE);
           
            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));
            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));
           
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
               

                dt.AcceptChanges();
            }

            return dt;

        }
        catch (OracleException ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

    }

}
