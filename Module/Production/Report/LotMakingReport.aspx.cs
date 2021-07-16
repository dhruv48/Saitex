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

public partial class Module_Production_Reports_LotMakingReport : System.Web.UI.Page
{
    string PRODUCT_CATEGORY = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["PRODUCT_CATEGORY"]!=null)
        {
            PRODUCT_CATEGORY = Request.QueryString["PRODUCT_CATEGORY"].ToString();
        }
        DataTable dt = GetData();
        GetReport(dt);
    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"LotMakingReport1.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable GetData()
    {
       SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail =(SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

       try
       {
           DataTable dt = SaitexBL.Interface.Method.YRN_LOT_MAKING.LotMakingReport(PRODUCT_CATEGORY);
           dt.TableName = "LotMakingForm1";


           if (!dt.Columns.Contains("COMP_NAME"))
               dt.Columns.Add("COMP_NAME", typeof(string));
           if (!dt.Columns.Contains("COMP_ADD"))
               dt.Columns.Add("COMP_ADD", typeof(string));
           if (!dt.Columns.Contains("BRANCH_NAME"))
               dt.Columns.Add("BRANCH_NAME", typeof(string));
           if (!dt.Columns.Contains("USER_NAME"))
               dt.Columns.Add("USER_NAME", typeof(string));

           SaitexDM.Common.DataModel.UserLoginDetail OUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

           foreach (DataRow dr in dt.Rows)
           {
               dr["COMP_NAME"] = OUserLoginDetail.VC_COMPANYNAME;
               dr["COMP_ADD"] = OUserLoginDetail.COMP_ADD;
               dr["BRANCH_NAME"] = OUserLoginDetail.VC_BRANCHNAME;
               dr["USER_NAME"] = OUserLoginDetail.Username;

           }
           return dt;
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