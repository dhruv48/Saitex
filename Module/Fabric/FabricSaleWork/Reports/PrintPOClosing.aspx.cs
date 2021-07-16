using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.OracleClient;

public partial class Module_Fabric_FabricSaleWork_Reports_PrintPOClosing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);
    }
    public DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.GetPODataClosing(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
        dt.TableName = "FabricPO_CLOSING";

        if (dt.Columns["COMP_NAME"] == null)
            dt.Columns.Add("COMP_NAME", typeof(string));

        if (dt.Columns["BRANCH_NAME"] == null)
            dt.Columns.Add("BRANCH_NAME", typeof(string));

        if (dt.Columns["USER_NAME"] == null)
            dt.Columns.Add("USER_NAME", typeof(string));
        if (dt.Columns["COMP_ADD"] == null)
            dt.Columns.Add("COMP_ADD", typeof(string));


        if (dt.Columns["DEVELOPER_WEB"] == null)
            dt.Columns.Add("DEVELOPER_WEB", typeof(string));
        if (dt.Columns["DEVELOPER_COMP"] == null)
            dt.Columns.Add("DEVELOPER_COMP", typeof(string));


        foreach (DataRow dr in dt.Rows)
        {
            dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            dr["USER_NAME"] = oUserLoginDetail.Username;
            dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
            dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
            dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;

            dt.AcceptChanges();
        }

        return dt;
    }
    public void GetReport(DataTable dt)
    {
        ReportDocument RDoc = new ReportDocument();
        RDoc.Load(Server.MapPath(@"FabricPOClosingReport.rpt"));
        RDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = RDoc;

    }
}
