using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Data;

public partial class Module_Prod_plan_Reports_BatchCardEntryReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_ENTRY_RPT> ds = GetPurchaseReportData();
        GetReport(ds);
    }


    private List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_ENTRY_RPT> GetPurchaseReportData()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.BATCH_CARD_MST oBATCH_CARD_MST = new SaitexDM.Common.DataModel.BATCH_CARD_MST();
            List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_ENTRY_RPT> dt = new List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_ENTRY_RPT>();
            if (Request.QueryString["BATCH_CODE"] != null && Request.QueryString["BATCH_CODE"].ToString() != "")
            {
                oBATCH_CARD_MST.BATCH_CODE = int.Parse(Request.QueryString["BATCH_CODE"].ToString());

                //dt = SaitexBL.Interface.Method.BATCH_CARD_MST.GetBatchEntryReport(oBATCH_CARD_MST);
                
            }
            return dt;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
    private void GetReport(List<SaitexDM.Common.DataModel.BATCH_CARD_MST.BATCH_CARD_ENTRY_RPT> ds)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"BatchCardEntry.rpt"));
        rDoc.SetDataSource(ds);
        CrystalReportViewer1.ReportSource = rDoc;
    }

}
