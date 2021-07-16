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

public partial class Module_Waste_Report_Waste_StockStatement : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetData();
            GetReport(dt);
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
            rDoc.Load(Server.MapPath(@"tx_waste_stockstat.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            DateTime StartDate;
            DateTime EndDate;
            string ITEM_CODE = string.Empty;
            string CAT_CODE = string.Empty;
            string Item_Rac = string.Empty;
            string Associate = string.Empty;
            string Item_Make = string.Empty; 

            if (Request.QueryString["FromDate"] != null && Request.QueryString["FromDate"] != "")
            {
                StartDate = DateTime.Parse(Request.QueryString["FromDate"].ToString().Trim());
            }
            else
            {
              StartDate = oUserLoginDetail.DT_STARTDATE;
            }

            if (Request.QueryString["ToDate"] != null && Request.QueryString["ToDate"] != "")
            {
                EndDate = DateTime.Parse(Request.QueryString["ToDate"].ToString().Trim());
            }
            else
            {
                EndDate = Common.CommonFuction.GetYearEndDate(StartDate);
            }
            if (Request.QueryString["ITEM_CODE"] != null && Request.QueryString["ITEM_CODE"] != "")
            {
                ITEM_CODE = Request.QueryString["ITEM_CODE"].ToString().Trim();
            }
            if (Request.QueryString["CAT_CODE"] != null && Request.QueryString["CAT_CODE"] != "")
            {
                CAT_CODE = Request.QueryString["CAT_CODE"].ToString().Trim();
            }
            if (Request.QueryString["ITEM_MAKE"] != null && Request.QueryString["ITEM_MAKE"] != "")
            {
                Item_Make = Request.QueryString["ITEM_MAKE"].ToString().Trim();
            }
            if (Request.QueryString["RAC_CODE"] != null && Request.QueryString["RAC_CODE"] != "")
            {
                Item_Rac = Request.QueryString["RAC_CODE"].ToString().Trim();
            }
            if (Request.QueryString["ASSOC"] != null && Request.QueryString["ASSOC"] != "")
            {
                Associate = Request.QueryString["ASSOC"].ToString().Trim();
            }   

            DataTable DT = new DataTable("TX_MATERIAL_LEDGER");
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.Load_Material_Reports(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, StartDate, EndDate,ITEM_CODE,CAT_CODE,Item_Rac,Item_Make,Associate);
            if (!dt.Columns.Contains("BRANCH_NAME"))
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (!dt.Columns.Contains("TITLE"))
                dt.Columns.Add("TITLE", typeof(string));

            if (!dt.Columns.Contains("Date_Range"))
                dt.Columns.Add("Date_Range", typeof(string));

            if (!dt.Columns.Contains("BAL_QTY"))
                dt.Columns.Add("BAL_QTY", typeof(double));
            foreach (DataRow dr in dt.Rows)
            {
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["Date_Range"] = "From date :" + StartDate.ToShortDateString() + " To :" + EndDate.ToShortDateString();
                dr["TITLE"] = "Material Ledger Report";               
            }
            return dt;
        }
        catch
        {
            throw;
        }
    }

}
