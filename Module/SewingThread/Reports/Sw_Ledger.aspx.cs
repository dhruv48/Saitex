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

public partial class Module_Yarn_SalesWork_Reports_Yarn_Ledger : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string BRANCH = string.Empty;
    string YARNCAT = string.Empty;
    string YARNTYPE = string.Empty;
    string YARN1 = string.Empty;
    string YEAR1 = string.Empty;
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
                    string YEAR = dt.Rows[0]["YEAR"].ToString();
                    //string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
                    string YARN_CAT = dt.Rows[0]["YARN_CAT"].ToString();
                    string YARN_TYPE = dt.Rows[0]["YARN_TYPE"].ToString();
                    string YARN_CODE = dt.Rows[0]["YARN_CODE"].ToString();
                    StDate = DateTime.Parse(dt.Rows[0]["StDate"].ToString());
                    EnDate = DateTime.Parse(dt.Rows[0]["EnDate"].ToString());
                    BRANCH = dt.Rows[0]["BRANCH"].ToString();
                    YARNCAT = dt.Rows[0]["YARNCAT"].ToString();
                    YARNTYPE = dt.Rows[0]["YARNTYPE"].ToString();
                    YARN1 = dt.Rows[0]["YARN1"].ToString();
                    YEAR1 = dt.Rows[0]["YEAR1"].ToString();
                    DataTable dtrportdat = GetData(BRANCH_CODE, YEAR, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate);
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
            rDoc.Load(Server.MapPath(@"Sw_Ledger.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("BRANCH",BRANCH);


            if (YARNTYPE != string.Empty)
            {
                rDoc.SetParameterValue("YARNTYPE",YARNTYPE);
            }
            else
            {
                rDoc.SetParameterValue("YARNTYPE", "ALL TYPE");
            }
            if (YARNCAT != string.Empty)
            {

                rDoc.SetParameterValue("YARNCAT",YARNCAT);
            }
            else
            {
                rDoc.SetParameterValue("YARNCAT", "ALL CATEGORY");
            }

            if (YARN1 != string.Empty)
            {

                rDoc.SetParameterValue("YARN1",YARN1);
            }
            else
            {
                rDoc.SetParameterValue("YARN1","ALL YARN");
            }
            rDoc.SetParameterValue("YEAR",YEAR1);
            rDoc.SetParameterValue("DATERANGE", "From date :" + StDate.ToShortDateString() + " To :" + EnDate.ToShortDateString());
          
            CrystalReportViewer1.ReportSource = rDoc;
            //  rDoc.PrintToPrinter(1, false, 1, 1);
        }
        catch
        {
            throw;
        }
    }
   
    private DataTable GetData(string BRANCH_CODE, string YEAR, string YARN_CAT, string YARN_TYPE, string  YARN_CODE, DateTime StDate, DateTime EnDate)
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

            //DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetDataForLEDGER_Report(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, StartDate, EndDate);
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetDataForLEDGER_Report1(BRANCH_CODE, YEAR, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate,"","","","");

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
