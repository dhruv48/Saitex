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


public partial class Module_OrderDevelopment_Reports_Production_Planning_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);

    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Production_Planning_Report_.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }



    private DataTable GetData()
    {

        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        try
        {

            string YEAR = Request.QueryString["YEAR"].ToString();
            string ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
            string YARN_CODE = Request.QueryString["YARN_CODE"].ToString();
            string Comp_Code = Request.QueryString["Comp_Code"].ToString();
            string Branch_Code = Request.QueryString["Branch_Code"].ToString();
            DateTime Sdate = DateTime.Parse(Request.QueryString["Sdate"].ToString());
            DateTime Edate = DateTime.Parse(Request.QueryString["Edate"].ToString());
            string PRTY_CODE = Request.QueryString["PRTY_CODE"].ToString();
            DataTable data = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForPlanningGriedByMachineCode(YEAR, ORDER_NO, YARN_CODE, Comp_Code, Branch_Code, Sdate, Edate, PRTY_CODE);

            return data;
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