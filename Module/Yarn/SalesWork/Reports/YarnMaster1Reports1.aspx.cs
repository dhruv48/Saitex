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
using Common;
using errorLog;
using System.Data.OracleClient;

public partial class Module_Yarn_SalesWork_Reports_YarnMaster1Reports1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    
{
        DataSet dt = DataSet();
        GetReport(dt);
    }
    private void GetReport(DataSet dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"YarnMaster1Report1.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataSet DataSet()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        try
        {
            DataSet dt = SaitexBL.Interface.Method.YRN_IR_MST.GetYarnMaster1Report1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);

            dt.Tables[0].TableName = "YRN_MST";
            dt.Tables[1].TableName = "YRN_BLAND_TRN";            
            dt.Tables[2].TableName = "YRN_BASE_ARTICLE_TRN";
            dt.Tables[3].TableName = "YRN_ASSOCATED_MST"; 

            if (dt.Tables[0].Columns["WORKS_ADDRESS"] == null)
            {
                dt.Tables[0].Columns.Add("WORKS_ADDRESS", typeof(string));
                dt.Tables[0].Columns.Add("COMP_ADD", typeof(string));
                dt.Tables[0].Columns.Add("USER_NAME", typeof(string));
            }
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                dr["COMP_ADD"] = oUserLoginDetail.DEVELOPER_WEB;                
                dr["USER_NAME"] = oUserLoginDetail.WORKS_ADDRESS;
                dr["WORKS_ADDRESS"] = oUserLoginDetail.WORKS_ADDRESS;

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