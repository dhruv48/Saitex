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
using CrystalDecisions.Shared;
public partial class Module_HRMS_Reports_HrShifRotaionMstCry : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Session["PrintSftRotation"] != null)
        {
            DataTable dt = (DataTable)Session["PrintSftRotation"];
            GetReport(dt);
        }
    }

    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"HrShiftRotationMst.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("cName",oUserLoginDetail.VC_COMPANYNAME);
            rDoc.SetParameterValue("bName",oUserLoginDetail .VC_BRANCHNAME);
            rDoc.SetParameterValue("uName", oUserLoginDetail.Username);
            rDoc.SetParameterValue("cAdd", oUserLoginDetail.COMP_ADD);
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
}
