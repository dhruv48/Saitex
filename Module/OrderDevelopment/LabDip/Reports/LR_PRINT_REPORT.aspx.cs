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

public partial class Module_OrderDevelopment_LabDip_Reports_LR_PRINT_REPORT : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        dt.Tables[0].TableName = "LAB_DIP_APPROVAL";
        GetReport(dt);

    }



    private void GetReport(DataSet dt)
    {
        try
        {
            ReportDocument rdoc = new ReportDocument();
            rdoc.Load(Server.MapPath(@"LR_APPROVAL_REPORT.rpt"));
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
            string From = string.Empty;
            string To = string.Empty;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["From"] != null && Request.QueryString["From"] != "")
            {
                From = Request.QueryString["From"].ToString().Trim();


            }
            if (Request.QueryString["To"] != null && Request.QueryString["To"] != "")
            {
                To = Request.QueryString["To"].ToString().Trim();

            }

            return SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForPrint(oUserLoginDetail, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, From, To);
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
