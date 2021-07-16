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
using System.Data.OracleClient;
public partial class Module_HRMS_Reports_HolidayMasterReport : System.Web.UI.Page
{
    OracleConnection ocon = null;
    OracleCommand cmd = null;
    OracleDataAdapter da = null;
    DataTable dt = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData('Y');
        getReport(dt);

    }
    private void getReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load(Server.MapPath(@"HolidayMasterReport.rpt"));
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

            strSQL = "SELECT DISTINCT  H.*,CASE H.OPT_LV WHEN 'N' THEN 'NO' ELSE 'OPTIONAL'  end  HSTATUS  FROM HR_HLD_MST H where 1=1 ";

            if (Request.QueryString["year"] != null)
            {
                strSQL = strSQL + " and ltrim(rtrim(H.YEAR))='" + Request.QueryString["year"].ToString().Trim() + "'";
            }
            if (Request.QueryString["date"] != null)
            {
                strSQL = strSQL + " and ltrim(rtrim(H.HLD_DATE))='" + Request.QueryString["date"].ToString().Trim() + "'";
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
