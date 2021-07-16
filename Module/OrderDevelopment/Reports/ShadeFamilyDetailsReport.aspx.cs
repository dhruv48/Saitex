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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.OracleClient;


public partial class Module_OrderDevelopment_Reports_ShadeFamilyDetailsReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);

    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"ShadeFamilyDetailsReport1.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer2.ReportSource=rDoc;
    }

    

    private DataTable GetData()
    {
        
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeDetailsReport();


            if (!dt.Columns.Contains("COMP_NAME"))
                dt.Columns.Add("COMP_NAME", typeof(string));
            if (!dt.Columns.Contains("COMP_ADD"))
                dt.Columns.Add("COMP_ADD", typeof(string));
            if (!dt.Columns.Contains("BRANCH_NAME"))
                dt.Columns.Add("BRANCH_NAME", typeof(string));
            if (!dt.Columns.Contains("USER_NAME"))
                dt.Columns.Add("USER_NAME", typeof(string));

            SaitexDM.Common.DataModel.UserLoginDetail OUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            dt.TableName = "ShadeFamilyDetails1";
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