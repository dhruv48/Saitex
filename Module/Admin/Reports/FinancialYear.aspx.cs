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
public partial class Module_Admin_Reports_FinancialYear : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData('Y');
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc1 = new ReportDocument();
        rDoc1.Load(Server.MapPath(@"FinancialMaster.rpt"));
        rDoc1.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc1;
    }

    private DataTable GetData(char ch_View)
    {
        DataTable dt;
        try
        {
            //OracleConnection con = new OracleConnection();
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            //con.Open();

            //string strSQL = "";

            //strSQL = "select * from V_CM_COMPANY_MST where DEL_STATUS=0";
            //OracleParameter param;

            //string WhereClause = "";

            //OracleCommand cmd = new OracleCommand();
            //cmd.Connection = con;
            //#region if searched
            //if (Request.QueryString["COMP_CODE"] != null && Request.QueryString["COMP_CODE"] != "")
            //{
            //    WhereClause = WhereClause + " Where ltrim(rtrim(COMP_CODE))=:COMP_CODE";
            //    param = new OracleParameter(":COMP_CODE", OracleType.VarChar, 10);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value = Common.CommonFuction.funFixQuotes(Request.QueryString["COMP_CODE"].ToString().Trim());
            //    cmd.Parameters.Add(param);
            //}
            //if (Request.QueryString["COMP_NAME"] != null && Request.QueryString["COMP_NAME"] != "")
            //{
            //    WhereClause = WhereClause + " and COMP_NAME=:COMP_NAME";
            //    param = new OracleParameter(":COMP_NAME", OracleType.VarChar, 100);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value = Common.CommonFuction.funFixQuotes(Request.QueryString["COMP_NAME"].ToString().Trim());
            //    cmd.Parameters.Add(param);
            //}


            //#endregion

            //if (WhereClause != "")
            //    strSQL = strSQL + WhereClause;

            //if (ch_View == 'Y')
            //{
            //    strSQL = strSQL + " order by COMP_CODE asc";
            //}
            //else
            //{
            //    strSQL = strSQL + " order by TDATE asc";
            //}

            //cmd.CommandText = strSQL;
            //cmd.CommandType = CommandType.Text;

            //OracleDataAdapter da = new OracleDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //dt.Columns.Add("VC_COMPANYNAME", typeof(string));

            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["TUSER"] = Session["urLoginId"].ToString();
            //    //dr[" VC_COMPANYNAME"] = Session["COMPNAME"].ToString();
            //}

            dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.GetFinacialMaster();

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
