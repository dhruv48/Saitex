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

public partial class Module_Yarn_SalesWork_Reports_YarnIndentReport1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataSet dt)
    {
        try
        {
            ReportDocument rdoc = new ReportDocument();
            rdoc.Load(Server.MapPath(@"YarnIndentReport1.rpt"));
            rdoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rdoc;
        }
        catch
        { 
        
        }

    }

    private DataSet GetData()
    {
        try
        {
            int From = 0;
            int To = 0;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["From"] != null && Request.QueryString["From"] != "")
            {
                From = int.Parse(Request.QueryString["From"].ToString().Trim());


            }
            if (Request.QueryString["To"] != null && Request.QueryString["To"] != "")
            {
                To = int.Parse(Request.QueryString["To"].ToString().Trim());

            }

           return SaitexBL.Interface.Method.YRN_INT_MST.GetDataForPrint(oUserLoginDetail,oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.VC_DEPARTMENTCODE, "GEN", From, To);
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
