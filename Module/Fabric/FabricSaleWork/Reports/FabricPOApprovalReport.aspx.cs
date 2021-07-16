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
public partial class Module_Fabric_FabricSaleWork_Reports_FabricPOApprovalReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = GetData();
        GetReport(dt);

    }
    protected void GetReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        string ReportPath = string.Empty;
        ReportPath = Server.MapPath(@"Fabric_POApprovalReport.rpt");
       rdoc.Load(ReportPath);
       rdoc.SetDataSource(dt);
       CrystalReportViewer1.ReportSource = rdoc;


    }
    protected DataTable GetData()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_PU_MST.getDataForApprovalReport();
            dt.TableName = "Fabric_POApproval";
            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));



            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dt.AcceptChanges();
            }

            return dt;
        }
        catch (Exception exe)
        {
            errorLog.ErrHandler.WriteError(exe.Message);
            return null;
        }
    }

}
