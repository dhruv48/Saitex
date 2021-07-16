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
using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Admin_Branch_Mst_Rpt : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["voucherreport"] != null)
            {
                DataTable dt = (DataTable)Session["voucherreport"];
                string VCHR_CODE = dt.Rows[0]["VCHR_CODE"].ToString();         
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                DataTable dtc = GetData(VCHR_CODE);
                GetReport(dtc);

            }
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
            rDoc.Load(Server.MapPath(@"Voucher_Mst.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetData(string VCHR_CODE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_VCHR_MST.GetReportData1(VCHR_CODE);

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

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;

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
