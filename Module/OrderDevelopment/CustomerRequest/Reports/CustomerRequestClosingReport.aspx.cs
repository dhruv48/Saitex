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

public partial class Module_OrderDevelopment_CustomerRequest_Reports_CustomerRequestClosingReport : System.Web.UI.Page
{
    string PRODUCT_TYPE = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        DataTable dt = GetData();
        GetReport(dt);

    }
    protected DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (Request.QueryString["PRODUCT_TYPE"] != null)
        {
            PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
        }
        DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForReportClosing(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE);
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
    public void GetReport(DataTable dt)
    {
        ReportDocument Rdoc = new ReportDocument();
        if (PRODUCT_TYPE == "WASTE")
        {
            Rdoc.Load(Server.MapPath(@"CRApprovReportForWaste.rpt"));
        }
       else if (PRODUCT_TYPE == "FABRIC")
        {
            Rdoc.Load(Server.MapPath(@"CRApprovReportforyrnspinning.rpt"));
        }
        else
        {
            Rdoc.Load(Server.MapPath(@"CRApprovReportforyrnspinning.rpt"));
        }
        Rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = Rdoc;

    }
}
