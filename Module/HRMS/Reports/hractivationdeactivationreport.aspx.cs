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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_HRMS_Reports_hractivationdeactivationreport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Activereport"] != null)
        {
            
            DataTable dt = (DataTable)Session["Activereport"];
            string EMP_CODE = dt.Rows[0]["EMP_CODE"].ToString();
            string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
            string DESIG_CODE = dt.Rows[0]["DESIG_CODE"].ToString();
            string str = dt.Rows[0]["str"].ToString();
            string branch = dt.Rows[0]["branch"].ToString();
            string FromDate = dt.Rows[0]["FromDate"].ToString();
            string ToDate = dt.Rows[0]["ToDate"].ToString();
            
            DataTable dtrportdat = GetData(EMP_CODE, DEPT_CODE, DESIG_CODE ,str,branch ,FromDate ,ToDate);

            //DataTable dtrportdat = GetData();       
            GetReport(dtrportdat);
        }
           
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"hractivationreport.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    //private DataTable GetData()
    private DataTable GetData(string EMP_CODE, string DEPT_CODE, string DESIG_CODE, string str, string branch, string FromDate, string ToDate)
    {

        try
        {
          DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.HrActivationreport(EMP_CODE, DEPT_CODE, DESIG_CODE, str,branch ,FromDate ,ToDate);
          
            //DataTable dt = SaitexBL.Interface.Method.HR_EMP_MST.GetEmpdata(EMP_CODE, DEPT_CODE, DESIG_CODE, str);
            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));
            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));
            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));
            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));
            if (dt.Columns["DEVELOPER_COMP"] == null)
                dt.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (dt.Columns["DEVELOPER_WEB"] == null)
                dt.Columns.Add("DEVELOPER_WEB", typeof(string));
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;


                dt.AcceptChanges();
            }

            return dt;

        }
      
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

    }
}
