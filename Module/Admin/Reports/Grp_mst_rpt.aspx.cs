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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.OracleClient;

public partial class Admin_Grp_mst_rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);

    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Grp_Mst1.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
        ////CrystalReportViewer1.Visible = true;
        //rDoc.PrintToPrinter(1,false,1,1); 
    }

    private DataTable GetData(char ch_View)
    {
        try
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";

            strSQL = "select * from TBLGROUPMASTER";
            OracleParameter param;

            string WhereClause = "";

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            //#region if searched
            //if (Request.QueryString["COMP_CODE"] != null && Request.QueryString["COMP_CODE"] != "")
            //{
            //    WhereClause = WhereClause + " Where ltrim(rtrim(COMP_CODE))=:COMP_CODE";
            //    param = new OracleParameter(":COMP_CODE", OracleType.VarChar, 10);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value = Common.CommonFuction.funFixQuotes(Request.QueryString["COMP_CODE"].ToString().Trim());
            //    cmd.Parameters.Add(param);
            //}
            //if (Request.QueryString["VC_COMPANYNAME"] != null && Request.QueryString["VC_COMPANYNAME"] != "")
            //{
            //    WhereClause = WhereClause + " and VC_COMPANYNAME=:VC_COMPANYNAME";
            //    param = new OracleParameter(":VC_COMPANYNAME", OracleType.VarChar, 100);
            //    param.Direction = ParameterDirection.Input;
            //    param.Value = Common.CommonFuction.funFixQuotes(Request.QueryString["VC_COMPANYNAME"].ToString().Trim());
            //    cmd.Parameters.Add(param);
            //}


            //#endregion

            if (WhereClause != "")
                strSQL = strSQL + WhereClause;

            if (ch_View == 'Y')
            {
                strSQL = strSQL + " order by IN_GROUPID asc";
            }
            else
            {
                strSQL = strSQL + " order by DT_UPDATED asc";
            }

            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //dt.Columns.Add("VC_COMPANYNAME", typeof(string));

            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["TUSER"] = Session["urLoginId"].ToString();
            //    //dr[" VC_COMPANYNAME"] = Session["COMPNAME"].ToString();
            //}
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

    private DataTable GetData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_GroupMaster.Select();
          
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
               
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dt.AcceptChanges();
            }

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
      

    }

}
