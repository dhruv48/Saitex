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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_Inventory_Reports_Material_Master_Report : System.Web.UI.Page
{
    private string SearchQuery = string.Empty;
    private string TrnType = string.Empty;
    private string FromDate = string.Empty;
    private string ToDate = string.Empty;
    private string RName = string.Empty;
    string BRANCH = string .Empty ;
    string  YEAR = string.Empty ;
    string DATERANGE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["Search"] != null && Request.QueryString["Search"] != "")
            {
                SearchQuery = Request.QueryString["Search"].ToString().Trim();
            }
            if (Request.QueryString["TrnType"] != null && Request.QueryString["TrnType"] != "")
            {
                TrnType = Request.QueryString["TrnType"].ToString().Trim();
            }
            if (Request.QueryString["FromDate"] != null && Request.QueryString["FromDate"] != "")
            {
                FromDate = Request.QueryString["FromDate"].ToString().Trim();
            }
            else
            {
                FromDate = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            }
            if (Request.QueryString["ToDate"] != null && Request.QueryString["ToDate"] != "")
            {
                ToDate = Request.QueryString["ToDate"].ToString().Trim();
            }
            else
            {
                ToDate = Common.CommonFuction.GetYearEndDate(oUserLoginDetail.DT_STARTDATE).ToShortDateString();
            }
            if (Request.QueryString["RptName"] != null && Request.QueryString["RptName"] != "")
            {
                RName = Request.QueryString["RptName"].ToString().Trim();
            }
           
        
            DataTable dt = GetData();
            string ReportName = Set_Report_Name(RName);
            GetReport(dt, ReportName);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private string Set_Report_Name(string Rpt)
    {
        string Report = string.Empty;
        try
        {
            if (Rpt != string.Empty)
            {
                if (Rpt == "CW")
                {
                    Report = "CRMAterialIssue_Challan_Wise.rpt";
                }
                else if (Rpt == "DI")
                {
                    Report = "RptDeptWiseItemIssue.rpt";
                }
                else if (Rpt == "ML")
                {
                    Report = "TX_MATERIAL_LEDGER.rpt";
                }
                else if (Rpt == "MS")
                {
                    Report = "tx_item_stockstat.rpt";
                }
            }
            return Report;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }
    private void GetReport(DataTable dt,string rptname)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@""+rptname));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("BRANCH",oUserLoginDetail.VC_BRANCHNAME);
            rDoc.SetParameterValue("YEAR",oUserLoginDetail.VC_FINANCIALYEARCODE);
            rDoc.SetParameterValue("DATERANGE", "From date :" + FromDate.ToString() + " To :" + ToDate.ToString());
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }           private DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        try
        {
            DataTable DTable = new DataTable("MATERIAL_ISSUE");
            DTable = SaitexBL.Interface.Method.ItemMaster.Load_Material_Transaction_Report(TrnType.ToUpper().ToString(), SearchQuery);

           
            if (!DTable.Columns.Contains("BRANCH_NAME"))
                DTable.Columns.Add("BRANCH_NAME", typeof(string));

            if (!DTable.Columns.Contains("TITLE"))
                DTable.Columns.Add("TITLE", typeof(string));

            if (!DTable.Columns.Contains("Date_Range"))
                DTable.Columns.Add("Date_Range", typeof(string));
            BRANCH = oUserLoginDetail.VC_BRANCHNAME;
            YEAR = oUserLoginDetail.FinYear;
            DATERANGE = TrnType.ToString();
            foreach (DataRow dr in DTable.Rows)
            {
              
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["Date_Range"] = "From Date: " + FromDate  + " To :" + ToDate ;
                if (TrnType.ToUpper() == "I")
                {
                    dr["TITLE"] = "Material Issue Report";
                }
                else
                {
                    dr["TITLE"] = "Material Receive Report";
                }
            }
            return DTable;

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
            throw ex;
        }
    }

}
