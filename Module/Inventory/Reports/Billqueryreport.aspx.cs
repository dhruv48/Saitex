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
using Common;
using errorLog;
public partial class Module_Inventory_Reports_Billqueryreport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["Proceereport"] != null)
            {
                DataTable dt = (DataTable)Session["Proceereport"];

                string BILL_NUMB = dt.Rows[0]["BILL_NUMB"].ToString();
                string PRTY_CODE = dt.Rows[0]["PRTY_CODE"].ToString();
                string TRN_TYPE = dt.Rows[0]["TRN_TYPE"].ToString();
                string FIN_VOUCH_NO = dt.Rows[0]["FIN_VOUCH_NO"].ToString();
                DataTable dtrportdat = GetData(BILL_NUMB, PRTY_CODE, TRN_TYPE, FIN_VOUCH_NO);
                GetReport(dtrportdat);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"BillReport.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string BILL_NUMB, string PRTY_CODE, string TRN_TYPE, string FIN_VOUCH_NO)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetBilldata(BILL_NUMB, PRTY_CODE, TRN_TYPE, FIN_VOUCH_NO,"");
           
            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));
            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));

            if (dt.Columns["DEVELOPER_WEB"] == null)
                dt.Columns.Add("DEVELOPER_WEB", typeof(string));
            if (dt.Columns["DEVELOPER_COMP"] == null)
                dt.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (dt.Columns["TITLE"] == null)
                dt.Columns.Add("TITLE", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                if (TRN_TYPE == "IYS07")
                {
                    dr["TITLE"] = "YARN BILL QUERY REPORT";
                }
                else
                {
                    dr["TITLE"] = "MATERIAL BILL QUERY REPORT";
                }
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;

                dt.AcceptChanges();
            }

            return dt;

        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

    }

}
