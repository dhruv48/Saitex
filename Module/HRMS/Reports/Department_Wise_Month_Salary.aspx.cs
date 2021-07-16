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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.IO;

public partial class Module_HRMS_Reports_Department_Wise_Month_Salary : System.Web.UI.Page
{
    private string SearchQuery = string.Empty;
    private string Search = string.Empty; 
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["Search"] != null && Request.QueryString["Search"] != "")
            {
                SearchQuery = Request.QueryString["Search"].ToString().Trim();
            }
            if (Request.QueryString["SearchNew"] != null && Request.QueryString["SearchNew"] != "")
            {
                Search = Request.QueryString["SearchNew"].ToString().Trim();
            }
            DataSet  DS = GetData();
            GetReport(DS, "Salary_Detail_Rpt.rpt");
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataSet  ds, string rptname)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"" + rptname));
            rDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }
    private DataSet  GetData()
    {
        try
        {
            DataSet ds = new DataSet();
            ds  = SaitexBL.Interface.Method.HR_SAL_SLIP_MST.Load_Salary_Record_By_Dept(SearchQuery,Search);
            return ds;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }
}
