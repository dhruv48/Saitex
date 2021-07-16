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
public partial class Module_Yarn_SalesWork_Reports_yrnissuereport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["Proceereport"] != null)
            {
                DataTable dt = (DataTable)Session["Proceereport"];

                string trn = dt.Rows[0]["trn"].ToString();
                string item = dt.Rows[0]["item"].ToString();
                int from = int.Parse(dt.Rows[0]["fromto"].ToString());
                int to = int.Parse(dt.Rows[0]["toto"].ToString());
                DataTable dtrportdat = GetData(trn, item, from, to);
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
            rDoc.Load(Server.MapPath(@"yrnissue.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string trn, string item, int from, int to)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.YrnIssue(trn, item, from, to);
          
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
         
            if (dt.Columns["NAME"] == null)
                dt.Columns.Add("NAME", typeof(string));
          
            
            foreach (DataRow dr in dt.Rows)
            {
                if (trn == "I")
                {
                    dr["NAME"] = "YARN ISSUE REPORT";
                }
                if (trn == "R")
                {
                    dr["NAME"] = "YARN MATERIAL REPORT";
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

