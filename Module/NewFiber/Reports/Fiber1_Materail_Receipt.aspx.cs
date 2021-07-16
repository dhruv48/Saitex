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
public partial class Module_Yarn_SalesWork_Reports_YRN_Materail_Receipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataSet dt)
    {
        if (Request.QueryString["TRN_TYPE"] != null)
        {
            ReportDocument rDoc = new ReportDocument();
            string ReportPath = string.Empty;
            if (Request.QueryString["TRN_TYPE"].ToString().Substring(0, 1) == "R")
            {
                ReportPath = Server.MapPath(@"Fiber1_Material_Receipt.rpt");
            }
            else if (Request.QueryString["TRN_TYPE"].ToString().Substring(0, 1) == "I")
            {
                ReportPath = Server.MapPath(@"Fiber1_Material_Issue.rpt");
            }
            rDoc.Load(ReportPath);
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
    }
    private DataSet GetData()
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
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetDataForReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, TRN_TYPE);
            DataTable dt1 = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetDataLOTWISEForReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, TRN_TYPE);
            
            
            dt.TableName = "Material_Receipt";
            dt1.TableName = "MATERIAL_ISSUE_DATA";
            if (!dt.Columns.Contains("VALUE"))
                dt.Columns.Add("VALUE", typeof(double));
            if (!dt.Columns.Contains("TITLE"))
                dt.Columns.Add("TITLE", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                dr["VALUE"] = double.Parse(dr["TRN_QTY"].ToString()) * double.Parse(dr["FINAL_RATE"].ToString());
                if (TRN_TYPE == "RCR")
                    dr["TITLE"] = "Material Receipt Credit Note- RCR";
                else if (TRN_TYPE == "RCC")
                    dr["TITLE"] = "Material Receipt Cash Note- RCC";
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

            DataSet DS = new DataSet();
            DS.Tables.Add(dt);
            DS.Tables.Add(dt1);
            DS.AcceptChanges();
            return DS;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
