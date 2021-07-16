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

public partial class Module_Fiber_Reports_FiberDetailReport : System.Web.UI.Page
{
    string BRANCH = string.Empty;
    string FIBER_TYPE = string.Empty;
    string PARTY = string.Empty;
    string COMP_NAME1 = string.Empty;
    string BRANCH_NAME1 = string.Empty;
    string USER_NAME = string.Empty;
    string COMP_ADD = string.Empty;
    string BRANCH_CODE = string.Empty;
    string FIBER_CAT = string.Empty;
    string PRTY_CODE = string.Empty;
    string FIBER_CODE = string.Empty;
    string TRN_TYPE = string.Empty;
    DateTime StDate;
    DateTime EnDate;
    int chksumry = 0;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    ReportDocument rDoc = new ReportDocument();
    protected void Page_unLoad(object sender, EventArgs e)
    {
        rDoc.Close();
        rDoc.Dispose();
    
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Request.QueryString["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = Request.QueryString["BRANCH_CODE"];
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (Request.QueryString["BRANCH"] != null)
            {
                BRANCH = Request.QueryString["BRANCH"];
            }
            else
            {
                BRANCH = "All";
            }
            if (Request.QueryString["FIBER_CAT"] != null)
            {
                FIBER_CAT = Request.QueryString["FIBER_CAT"];
            }
            else
            {
                FIBER_CAT = string.Empty;
            }

            if (Request.QueryString["PRTY_CODE"] != null)
            {
                PRTY_CODE = Request.QueryString["PRTY_CODE"];
            }
            else
            {
                PRTY_CODE = string.Empty;
            }

           
            if (Request.QueryString["chk"] != null && Request.QueryString["chk"] != "")
            {
                chksumry = int.Parse(Request.QueryString["chk"].ToString());
            }            

            if (Request.QueryString["FIBER_CODE"] != null && Request.QueryString["FIBER_CODE"] != "")
            {
                FIBER_CODE = Request.QueryString["FIBER_CODE"];
            }
            else
            {
                FIBER_CODE = string.Empty;
            }
            if (Request.QueryString["FIBER_TYPE"] != null && Request.QueryString["FIBER_TYPE"] != "")
            {
                FIBER_CODE = Request.QueryString["FIBER_TYPE"];
            }            
            if (Request.QueryString["StDate"] != null)
            {
                DateTime.TryParse(Request.QueryString["StDate"].ToString(), out StDate);
            }
            else
            {
                StDate = DateTime.MinValue;
            }
            if (Request.QueryString["EnDate"] != null)
            {
                DateTime.TryParse(Request.QueryString["EnDate"].ToString(),out EnDate);
            }
            else
            {
                EnDate = DateTime.Now.Date;
            }
            if (Request.QueryString["TRN_TYPE"] != null && Request.QueryString["TRN_TYPE"] != "")
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"];
            }
            else
            {
                TRN_TYPE = string.Empty;
            }
            DataTable dtrportdat = GetData(BRANCH_CODE, FIBER_CAT, PRTY_CODE, FIBER_CODE, StDate, EnDate,TRN_TYPE );
            GetReport(dtrportdat);
            dtrportdat.Dispose();

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
           
            if (chksumry == 0)
            {
                rDoc.Load(Server.MapPath(@"FiberDetailStock.rpt"));

            }
            else if (chksumry == 1)
            {
                //rDoc.Load(Server.MapPath(@"FiberSumryReport.rpt"));
                rDoc.Load(Server.MapPath(@"FiberDetailStockCartonWise.rpt"));
            }
            else if (chksumry == 2)
            {
                rDoc.Load(Server.MapPath(@"FiberDetailStockPartyWise.rpt"));
            }
            else if (chksumry == 3)
            {
                rDoc.Load(Server.MapPath(@"FiberDetailStockChallanWise.rpt"));

            }
            else             
            {
                rDoc.Load(Server.MapPath(@"FIBER_LOTWISE_STOCK_REPORT.rpt"));
            
            }
            rDoc.SetDataSource(dt);
            if (chksumry == 0 || chksumry == 1 || chksumry == 2 || chksumry == 3)
            {
                rDoc.SetParameterValue("BRANCH", BRANCH);
                if (PARTY != string.Empty)
                {
                    rDoc.SetParameterValue("PARTY", PARTY);
                }
                else
                {
                    rDoc.SetParameterValue("PARTY", "ALL TYPE");
                }
                if (TRN_TYPE != string.Empty)
                {
                    rDoc.SetParameterValue("TRN_TYPE", TRN_TYPE == "R" ? "Receiving" : "Opening");
                }
                else
                {
                    rDoc.SetParameterValue("TRN_TYPE", "ALL");
                }

                rDoc.SetParameterValue("COMP_NAME1", oUserLoginDetail.VC_COMPANYNAME);
                rDoc.SetParameterValue("BRANCH_NAME1", oUserLoginDetail.VC_BRANCHNAME);
                rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
                rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);
            }
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string BRANCH_CODE, string FIBER_CAT, string PRTY_CODE, string FIBER_CODE, DateTime StDate, DateTime EnDate,string TRN_TYPE)
    {
        try
        {
                //DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetFiberDetail(BRANCH_CODE, FIBER_CAT, PRTY_CODE, FIBER_CODE, StDate, EnDate);
            DataTable dt = null;
            if (chksumry == 0)
            {
                 dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetFiberDetailLotWise(BRANCH_CODE, FIBER_CAT, PRTY_CODE, FIBER_CODE, StDate, EnDate,TRN_TYPE);
           
            }
            else if (chksumry == 1)
            {
                 dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetFiberDetailCartonWise(BRANCH_CODE, FIBER_CAT, PRTY_CODE, FIBER_CODE, StDate, EnDate,TRN_TYPE );
           
            }
            else if (chksumry == 2)
            {
                dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetFiberDetailPartyWise(BRANCH_CODE, FIBER_CAT, PRTY_CODE, FIBER_CODE, StDate, EnDate,TRN_TYPE );

            }
            else if (chksumry == 3)
            {
                dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetFiberDetail(BRANCH_CODE, FIBER_CAT, PRTY_CODE, FIBER_CODE, StDate, EnDate, TRN_TYPE);
            }
            else
            {
                dt = SaitexBL.Interface.Method.Fiber_Master_new.GetFiberStockLotWise(oUserLoginDetail.COMP_CODE, BRANCH_CODE, oUserLoginDetail.DT_STARTDATE.Year , "", "", "", "", TRN_TYPE, "", FIBER_CODE, "", FIBER_CAT, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            }
            if (dt.Rows.Count > 0)
            {

            }
            else
            {
                CommonFuction.ShowMessage("Data Not Found .");
            }

            return dt;
        }
        catch
        {
            throw;
        }

    }
}
