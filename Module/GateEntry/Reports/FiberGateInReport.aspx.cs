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
using System.Data.OracleClient;

public partial class Module_GateEntry_Reports_FiberGateInReport : System.Web.UI.Page
{
    string Query_String=string.Empty;
    string BRANCH = string.Empty;
    string TRNTYPE = string.Empty;
    string PRTY_CODE = string.Empty;
    string DOC_NO = string.Empty;
    int GT_FROM = 0;
    int GT_TO = 0;
    string FR_DATE = string.Empty;
    string T_DATE = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);
    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"FiberGateInReport1.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData()
    {

        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];


        try
        {

           // DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.GetRecord(oUserLoginDetail.COMP_CODE, BRANCH, TRNTYPE, PRTY_CODE, DOC_NO, GT_FROM, GT_TO, FR_DATE, T_DATE);
           DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.GetFiberGateInReport(oUserLoginDetail.COMP_CODE, BRANCH, TRNTYPE, PRTY_CODE, DOC_NO, GT_FROM, GT_TO, FR_DATE, T_DATE);
            
            dt.TableName = "FiberGateInReport1";


            if (!dt.Columns.Contains("COMP_NAME"))
                dt.Columns.Add("COMP_NAME", typeof(string));
            if (!dt.Columns.Contains("COMP_ADD"))
                dt.Columns.Add("COMP_ADD", typeof(string));
            if (!dt.Columns.Contains("BRANCH_NAME"))
                dt.Columns.Add("BRANCH_NAME", typeof(string));
            if (!dt.Columns.Contains("USER_NAME"))
                dt.Columns.Add("USER_NAME", typeof(string));

            SaitexDM.Common.DataModel.UserLoginDetail OUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = OUserLoginDetail.VC_COMPANYNAME;
                dr["COMP_ADD"] = OUserLoginDetail.COMP_ADD;
                dr["BRANCH_NAME"] = OUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = OUserLoginDetail.Username;

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
