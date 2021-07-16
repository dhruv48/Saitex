using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_Fabric_FabricSaleWork_Reports_Fabric_REcieptAgainst : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        if (Request.QueryString["TRN_TYPE"] != null)
        {
            ReportDocument rDoc = new ReportDocument();
            string ReportPath = string.Empty;
            if (Request.QueryString["TRN_TYPE"].ToString().Substring(0, 1) == "R")
            {
                ReportPath = Server.MapPath(@"Fabric_Receipt.rpt");
            }
            else if (Request.QueryString["TRN_TYPE"].ToString().Substring(0, 1) == "I")
            {
                ReportPath = Server.MapPath(@"FabricIssue.rpt");
            }
            rDoc.Load(ReportPath);
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
    }
    private DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            int From = 0;
            int To = 0;
            string TRN_TYPE = "";

            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }
            if (Request.QueryString["TRN_TYPE"] != null)
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString().Trim();
            }
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.GetDataForReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, TRN_TYPE);
            dt.TableName = "Fabric_Receipt";
            if (!dt.Columns.Contains("VALUE"))
                dt.Columns.Add("VALUE", typeof(double));
            if (!dt.Columns.Contains("TITLE"))
                dt.Columns.Add("TITLE", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                dr["VALUE"] = double.Parse(dr["TRN_QTY"].ToString()) * double.Parse(dr["FINAL_RATE"].ToString());
                if (TRN_TYPE == "RCR")
                    dr["TITLE"] = "Fabric Receipt Credit Note- RCR";
                else if (TRN_TYPE == "RCC")
                    dr["TITLE"] = "Fabric Receipt Cash Note- RCC";
            }

            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));



            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
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
