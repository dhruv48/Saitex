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

public partial class Module_Waste_Reports_TX_WASTE_LEDGER : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string BRANCH1 = string.Empty;
    string YEAR1 = string.Empty;
    string ITEMTYPE = string.Empty;
    string ITEMCAT = string.Empty;
    string ITEM1 = string.Empty;
    string DATERANGE = string.Empty;
    string ITEM_TYPE = string.Empty;
    string ITEM_CATE = string.Empty;
    DateTime StDate;
    DateTime EnDate;
    protected void Page_Load(object sender, EventArgs e)
    {
         oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        try
        {

            if (Session["MaterialLedger"] != null)
            {
                DataTable dt = (DataTable)Session["MaterialLedger"];
                if (dt.Rows.Count > 0)
                {
                    string BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
                    string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
                    ITEM_CATE = dt.Rows[0]["ITEM_CATE"].ToString();
                    ITEM_TYPE = dt.Rows[0]["ITEM_TYPE"].ToString();
                    string ITEM_CODE = dt.Rows[0]["ITEM_CODE"].ToString();
                    BRANCH1 = dt.Rows[0]["BRANCH"].ToString();
                    YEAR1 = dt.Rows[0]["YEAR"].ToString();
                    ITEMTYPE = dt.Rows[0]["ITEMTYPE"].ToString();
                    ITEMCAT = dt.Rows[0]["ITEMCAT"].ToString();
                    ITEM1 = dt.Rows[0]["ITEM1"].ToString();
                     StDate = DateTime.Parse(dt.Rows[0]["StDate"].ToString());
                     EnDate = DateTime.Parse(dt.Rows[0]["EnDate"].ToString());
                    DataTable dtrportdat = GetData(BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, StDate, EnDate);
                    GetReport(dtrportdat);
                }
               
                //DataTable dt = GetData();
                //GetReport(dt);
            }

            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"TX_WASTE_LEDGER.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("BRANCH", BRANCH1);
            rDoc.SetParameterValue("YEAR", YEAR1);
            if (ITEMTYPE != string.Empty)
            {
                rDoc.SetParameterValue("ITEMTYPE", ITEMTYPE);
            }
            else
            {
                rDoc.SetParameterValue("ITEMTYPE", "ALL TYPE");
            }
            if (ITEMCAT != string.Empty)
            {

                rDoc.SetParameterValue("ITEMCAT", ITEMCAT);
            }
            else
            {
                rDoc.SetParameterValue("ITEMCAT", "ALL CATEGORY");
            }
          
            if (ITEM1 != string.Empty)
            {

                rDoc.SetParameterValue("ITEM1", ITEM1);
            }
            else
            {
                rDoc.SetParameterValue("ITEM1", "ALL ITEM");
            }

            rDoc.SetParameterValue("DATERANGE", "From date :" + StDate.ToShortDateString() + " To :" + EnDate.ToShortDateString());
            
            CrystalReportViewer1.ReportSource = rDoc;
            //  rDoc.PrintToPrinter(1, false, 1, 1);
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData( string  BRANCH_CODE, string  DEPT_CODE, string  ITEM_CATE, string  ITEM_TYPE, string  ITEM_CODE, DateTime  StDate, DateTime  EnDate)
    {
        try
        {
            int From = 0;
            int To = 0;

            if (Request.QueryString["From"] != null && Request.QueryString["From"] != "")
            {
                From = int.Parse(Request.QueryString["From"].ToString().Trim());
            }
            if (Request.QueryString["To"] != null && Request.QueryString["To"] != "")
            {
                To = int.Parse(Request.QueryString["To"].ToString().Trim());
            }
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DateTime StartDate = oUserLoginDetail.DT_STARTDATE;
            DateTime EndDate = Common.CommonFuction.GetYearEndDate(StartDate);

           //DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetDataForLEDGER_Report(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, StartDate, EndDate);
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetDataForLEDGER_Report1(BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, StDate, EnDate);
            if (dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("BRANCH_NAME"))
                    dt.Columns.Add("BRANCH_NAME", typeof(string));

                if (!dt.Columns.Contains("TITLE"))
                    dt.Columns.Add("TITLE", typeof(string));

                if (!dt.Columns.Contains("Date_Range"))
                    dt.Columns.Add("Date_Range", typeof(string));

                if (!dt.Columns.Contains("BAL_QTY"))
                    dt.Columns.Add("BAL_QTY", typeof(double));

                double Bal_Qty = 0;
                double Dr_Qty = 0;
                double Cr_Qty = 0;
                double Op_Bal = 0;

                Op_Bal = double.Parse(dt.Rows[0]["OP_BAL_STOCK"].ToString());
                Bal_Qty = Op_Bal;

                foreach (DataRow dr in dt.Rows)
                {
                    //dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                    dr["Date_Range"] = "From date :" + StDate.ToShortDateString() + " To :" + EnDate.ToShortDateString();
                    dr["TITLE"] = "Material Ledger Report";

                    Dr_Qty = 0;
                    Cr_Qty = 0;
                    double.TryParse(dt.Rows[0]["RECEIVE_QTY"].ToString(), out Dr_Qty);
                    double.TryParse(dt.Rows[0]["ISSUE_QTY"].ToString(), out Cr_Qty);

                    Bal_Qty = Bal_Qty + Dr_Qty - Cr_Qty;
                    dr["BAL_QTY"] = Bal_Qty;
                }
                
            }
            else
            {
                Common.CommonFuction.ShowMessage(" Sir Data not found accroding to this record . ");
            }
            return dt;
        
        }
        catch
        {
            throw;
        }
    }

}
