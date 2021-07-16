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
using Common;



public partial class Inventory_Trn_MST_RPT : System.Web.UI.Page
{

    private string FormHeading;

    private string MasterName;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["FormHeading"] != null && Request.QueryString["FormHeading"].ToString() != "")
            {
                FormHeading = Request.QueryString["FormHeading"].ToString();
            }

            if (Request.QueryString["MasterName"] != null && Request.QueryString["MasterName"].ToString() != "")
            {
                MasterName = Request.QueryString["MasterName"].ToString();
            }

            DataTable dt = GetData('Y');
            GetReport(dt);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"Trnsaction_Mst.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(char ch_View)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.PrintReportByMST_NAME(MasterName,oUserLoginDetail.COMP_CODE);

            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));

            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));

            if (dt.Columns["DEVELOPER_COMP"] == null)
                dt.Columns.Add("DEVELOPER_COMP", typeof(string));

            if (dt.Columns["DEVELOPER_WEB"] == null)
                dt.Columns.Add("DEVELOPER_WEB", typeof(string));

            if (dt.Columns["REPORT_TITLE"] == null)
                dt.Columns.Add("REPORT_TITLE", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["REPORT_TITLE"] = FormHeading;
                dt.AcceptChanges();
            }

            return dt;
        }
        catch
        {
            throw;
        }
    }
}
