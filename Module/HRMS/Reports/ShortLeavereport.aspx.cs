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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_HRMS_Reports_ShortLeavereport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DataTable dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load(Server.MapPath(@"ShortLeavereport.rpt"));
        rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rdoc;
    }
    private DataTable GetData()
    {

        DataTable dt = SaitexBL.Interface.Method.HR_LV_MST.printreport("L");

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

}
