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
using Common;
using errorLog;
using System.Data.OracleClient;
public partial class Inventory_ItemIndentReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Session["urLoginId"] != null)
        {
            DataSet ds = GetData();
            GetReport(ds);
        }
    }
    private void GetReport(DataSet ds)
    {

        string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
     

        ReportDocument rDoc = new ReportDocument();
        //if (BRANCH_CODE == "SIT0001")
        //{
            rDoc.Load(Server.MapPath(@"IndentRptNew.rpt"));
        //}
        //else if (BRANCH_CODE == "GRG001")
        //{
        //    rDoc.Load(Server.MapPath(@"IndentRptNew1.rpt"));
        //}
     
        rDoc.SetDataSource(ds);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    
    private DataSet GetData()
    {
        try
        {
            int From = 0;
            int To = 0;

            if (Request.QueryString["From"] != null && Request.QueryString["From"] != "")          
                From = int.Parse(Request.QueryString["From"].ToString().Trim());

            if (Request.QueryString["To"] != null && Request.QueryString["To"] != "")
                            To = int.Parse(Request.QueryString["To"].ToString().Trim());
            
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataSet ds = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForPrint(oUserLoginDetail, "GEN", From, To);

           

                return ds;
            
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    } 
}
