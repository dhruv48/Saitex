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
using CrystalDecisions.Shared;
public partial class Module_Inventory_Reports_MaterialPurchaseRegisterCrReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
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
                    string YEAR = dt.Rows[0]["YEAR"].ToString();
                    string BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
                    string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
                    string ITEM_CATE = dt.Rows[0]["ITEM_CATE"].ToString();
                    string ITEM_TYPE = dt.Rows[0]["ITEM_TYPE"].ToString();
                    string ITEM_CODE = dt.Rows[0]["ITEM_CODE"].ToString();
                    string PRTY_CODE = dt.Rows[0]["PRTY_CODE"].ToString();
                    DateTime StDate = DateTime.Parse(dt.Rows[0]["StDate"].ToString());
                    DateTime EnDate = DateTime.Parse(dt.Rows[0]["EnDate"].ToString());
                    DataTable dtrportdat = GetData(YEAR, BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, PRTY_CODE, StDate, EnDate);
                    GetReport(dtrportdat);
                }

                //DataTable dt = GetData();
                //GetReport(dt);
            }


        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(" Sir Data not found accroding to this record . ");
            //Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"Purchase_Register.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
            //  rDoc.PrintToPrinter(1, false, 1, 1);
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string YEAR, string BRANCH_CODE, string DEPT_CODE, string ITEM_CATE, string ITEM_TYPE, string ITEM_CODE, string PRTY_CODE, DateTime StDate, DateTime EnDate)
    {
        try     
        {
            
          
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DateTime StartDate = oUserLoginDetail.DT_STARTDATE;
            DateTime EndDate = Common.CommonFuction.GetYearEndDate(StartDate);

            //DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetDataForLEDGER_Report(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, StartDate, EndDate);
           // DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetDataForLEDGER_Report1(BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, StDate, EnDate);
            DataTable dt  = SaitexBL.Interface.Method.TX_ITEM_IR_MST.Purchaseregisterdetail(YEAR, BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, PRTY_CODE, StDate, EnDate);
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
                //dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["Date_Range"] = "From date :" + StDate.ToShortDateString() + " To :" + EnDate.ToShortDateString();
               
            }
            return dt;
        }
        catch
        {
            throw;
        }
    }

}

