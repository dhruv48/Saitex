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


public partial class Inventory_MaterialIndentAppRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData('Y');
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"MaterialIndent.rpt"));
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

            strSQL = "select distinct a.DEPT_CODE, a.ITEM_CODE,(nvl(b.OP_BAL_STOCK,0) + nvl(b.YTD_RCPT,0) - nvl(b.YTD_ISS,0))  currentStock,b.OP_RATE* d.RQST_QTY iValue,a.MIN_STOCK_LVL,b.OP_RATE,a.ITEM_DESC, e.MST_CODE UOM,c.IND_NUMB,c.IND_DATE,c.PREP_BY,c.REQD_DATE, c.CONF_BY, c.CONF_DATE,c.CONF_COMMENT,a.ITEM_DESC,d.DPT_CONF_DATE,d.RQST_QTY,d.DPT_REMARK,d.TUSER,d.TDATE from TX_ITEM_MST a,TX_ITEM_OP_BAL b,TX_ITEM_IND_MST c,TX_ITEM_IND_TRN d,tx_master_trn e where ltrim(rtrim(a.ITEM_CODE))=ltrim(rtrim(b.ITEM_CODE)) and ltrim(rtrim(a.UOM))=ltrim(rtrim(e.MST_CODE)) and ltrim(rtrim(e.MST_NAME))='UOM' and ltrim(rtrim(c.IND_NUMB))=ltrim(rtrim(d.IND_NUMB)) and ltrim(rtrim(a.ITEM_CODE))=ltrim(rtrim(d.ITEM_CODE))";
            OracleParameter param;

            string WhereClause = "";

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            #region if searched
            if (Request.QueryString["IND_NUMB"] != null && Request.QueryString["IND_NUMB"] != "")
            {
                WhereClause = WhereClause + " and ltrim(rtrim(c.IND_NUMB))=:IND_NUMB";
                param = new OracleParameter(":IND_NUMB", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = Common.CommonFuction.funFixQuotes(Request.QueryString["IND_NUMB"].ToString().Trim());
                cmd.Parameters.Add(param);
            }
        
            #endregion

            if (WhereClause != "")
                strSQL = strSQL + WhereClause;

           

            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //   dt.Columns.Add("TUSER", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                dr["TUSER"] = oUserLoginDetail.Username;
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
