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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OracleClient;
public partial class Module_HRMS_Reports_EmployeeMasterReport : System.Web.UI.Page
{
    OracleConnection ocon = null;
    OracleCommand cmd = null;
    OracleDataAdapter da = null;
    DataTable dt = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetData('Y');
            getReport(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load(Server.MapPath(@"EmployeeMaster.rpt"));
        rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rdoc;
    }
    private DataTable GetData(char ch_View)
    {
        try
        {
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();
            string strSQL = "";
            strSQL = "select distinct c.COMP_NAME,b.BRANCH_NAME,d.DEPT_NAME,ds.DESIG_NAME,ds.DESIG_CODE,e.EMP_CODE,(F_NAME||' '||M_NAME||' '|| L_NAME) empName,e.GENDER,e.NATION,e.DOB,e.MRTL_STATUS,e.SKILL,e.DESIG_CODE,e.EMAIL_ID from CM_COMPANY_MST c,CM_BRANCH_MST b,CM_DEPT_MST d, CM_DESIG_MST ds,HR_EMP_MST e where c.COMP_CODE=e.COMP_CODE and b. BRANCH_CODE=e. BRANCH_CODE and d.DEPT_CODE=e.DEPT_CODE and ds.DESIG_CODE=e.DESIG_CODE and e.DEL_STATUS='0'";
            if (Request.QueryString["bncCode"] != null)
            {
                strSQL = strSQL + " and ltrim(rtrim(b.BRANCH_CODE))='" + Request.QueryString["bncCode"].ToString().Trim() + "'";
            }
           
            if (Request.QueryString["decCode"] != null)
            {
                strSQL = strSQL + " and ltrim(rtrim(d.DEPT_CODE))='" + Request.QueryString["decCode"].ToString().Trim() + "'";
            }  
            cmd = new OracleCommand();
            cmd.Connection = ocon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;

            #region if searched
            //if (Request.QueryString["ECO"] != null && Request.QueryString["ECO"] != "")
            //{
            //    WhereClause = WhereClause + " and ltrim(rtrim(CH_EMPLOYEECODE)) like :CH_EMPLOYEECODE";
            //    param = new OracleParameter(":CH_EMPLOYEECODE", OracleType.VarChar, 6);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value = Common.CommonFuction.funFixQuotes(Request.QueryString["ECO"].ToString().Trim()) + "%";
            //    cmd.Parameters.Add(param);
            //}
            //if (Request.QueryString["ENA"] != null && Request.QueryString["ENA"] != "")
            //{
            //    WhereClause = WhereClause + " and VC_FIRSTNAME like :VC_FIRSTNAME";
            //    param = new OracleParameter(":VC_FIRSTNAME", OracleType.VarChar, 150);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value = Common.CommonFuction.funFixQuotes(Request.QueryString["ENA"].ToString().Trim()) + "%";
            //    cmd.Parameters.Add(param);
            //}
            //if (Request.QueryString["COMP"] != null && Request.QueryString["COMP"] != "")
            //{
            //    WhereClause = WhereClause + " and VC_COMPANYNAME like :VC_COMPANYNAME";
            //    param = new OracleParameter(":VC_COMPANYNAME", OracleType.VarChar, 10);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value =  Common.CommonFuction.funFixQuotes(Request.QueryString["COMP"].ToString().Trim()) + "%";
            //    cmd.Parameters.Add(param);
            //}
            //if (Request.QueryString["BRANCH"] != null && Request.QueryString["BRANCH"] != "")
            //{
            //    WhereClause = WhereClause + " and VC_BRANCHNAME like :VC_BRANCHNAME";
            //    param = new OracleParameter(":VC_BRANCHNAME", OracleType.VarChar, 10);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value =  Common.CommonFuction.funFixQuotes(Request.QueryString["BRANCH"].ToString().Trim()) + "%";
            //    cmd.Parameters.Add(param);
            //}
            //if (Request.QueryString["DEP"] != null && Request.QueryString["DEP"] != "")
            //{
            //    WhereClause = WhereClause + " and VC_DEPARTMENTNAME like :VC_DEPARTMENTNAME";
            //    param = new OracleParameter(":VC_DEPARTMENTNAME", OracleType.VarChar, 10);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value =  Common.CommonFuction.funFixQuotes(Request.QueryString["DEP"].ToString().Trim()) + "%";
            //    cmd.Parameters.Add(param);
            //}
            //if (Request.QueryString["DES"] != null && Request.QueryString["DES"] != "")
            //{
            //    WhereClause = WhereClause + " and VC_DESIGNATIONNAME like :VC_DESIGNATIONNAME";
            //    param = new OracleParameter(":VC_DESIGNATIONNAME", OracleType.Char, 50);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value =  Common.CommonFuction.funFixQuotes(Request.QueryString["DES"].ToString().Trim()) + "%";
            //    cmd.Parameters.Add(param);
            //}


            #endregion


            da = new OracleDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
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
        finally
        {
            if (ocon != null)
            {
                ocon.Close();
                ocon.Dispose();
                ocon = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (da != null)
            {
                da.Dispose();
                da = null;
            }

        }
        }

       
}      
