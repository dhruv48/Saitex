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

using Common;
using errorLog;


public partial class Module_Production_Report_Production_Lot_Move_Report : System.Web.UI.Page
{
    string Query_String = string.Empty;
    string DEPT_CODE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData(Query_String);
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Production_Lot_Move_Report.aspx.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable GetData(string Query_String)
    {

        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];


        try
        {


            //DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetReportForProductionLotMovement(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, Query_String);
            dt.TableName = "Production_Lot_Move_Conf1";


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
