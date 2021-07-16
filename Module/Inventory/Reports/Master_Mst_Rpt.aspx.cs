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

public partial class Inventory_Master_Mst_Rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData('Y');
        GetReport(dt);

    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Master_Mst.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable GetData(char ch_View)
    {
        try
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();
            string strSQL = "";

            strSQL = "select * from TX_MASTER_MST";
            OracleParameter param;

            string WhereClause = "";

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            #region if searched
            if (Request.QueryString["MST_NAME"] != null && Request.QueryString["MST_NAME"] != "")
            {
                WhereClause = WhereClause + " Where ltrim(rtrim(MST_NAME)) like :MST_NAME ";
                param = new OracleParameter(":MST_NAME", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = Common.CommonFuction.funFixQuotes(Request.QueryString["MST_NAME"].ToString().Trim()) + "%";
                cmd.Parameters.Add(param);
            }

            #endregion

            if (WhereClause != "")
                strSQL = strSQL + WhereClause;

            if (ch_View == 'Y')
            {
                strSQL = strSQL + " order by MST_NAME asc";
            }
            else
            {
                strSQL = strSQL + " order by TDATE asc";
            }

            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //dt.Columns.Add("TUSER", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                dr["TUSER"] = oUserLoginDetail.UserCode;
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
