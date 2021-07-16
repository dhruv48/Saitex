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

public partial class Module_HRMS_Pages_InvestmentRpt : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            DataSet DS  = GetData();
            GetReport(DS );
            if (!Page.IsPostBack)
            {

            }
        }
    }
    private void GetReport(DataSet  DS)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"\Saitex\Module\HRMS\Reports\RptInvestment.rpt"));
            rDoc.SetDataSource(DS);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch(Exception Ex)
        {
            throw Ex;
        }
    }
    private DataSet GetData()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(Session["EmpCode"].ToString());
            DataSet DS = SaitexBL.Interface.Method.HR_EMP_INVESMENT_DECLARATION.GetDataForReport(Session["EmpCode"].ToString());
            DS.Tables.Add(DT);
            DS.Tables[0].TableName = "HouseRent";
            DS.Tables[1].TableName = "Investment";
            DS.Tables[2].TableName = "EMPTable";
            return DS;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
