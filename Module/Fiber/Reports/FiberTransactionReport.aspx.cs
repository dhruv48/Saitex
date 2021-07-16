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

public partial class Module_Fiber_Reports_FiberTransactionReport : System.Web.UI.Page
{
    string BRANCH_CODE = string.Empty;
    string DEPT_CODE = string.Empty;
    string FIBER_CAT = string.Empty;
   // string YARN_TYPE = string.Empty;
    string FIBER_CODE = string.Empty;
    DateTime StDate;
    DateTime EnDate;
    string TRAN_TYPE = string.Empty;
    string PARTY_CODE = string.Empty;
    string BRANCH = string.Empty;
    string TRNTYPE = string.Empty;
   // string YARNTYPE = string.Empty;
    string FIBERCAT = string.Empty;
    string DEPARTMENT1 = string.Empty;
    string FIBER1 = string.Empty;
    string YEAR1 = string.Empty;
    string PARTY = string.Empty;
    string user = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
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
            if (Request.QueryString["DEPT_CODE"] != null)
            {
                DEPT_CODE = Request.QueryString["DEPT_CODE"];

            }
            else
            {
                DEPT_CODE = string.Empty;
            }
            if (Request.QueryString["FIBER_CAT"] != null)
            {
                FIBER_CAT = Request.QueryString["FIBER_CAT"];

            }
            else
            {
                FIBER_CAT = string.Empty;
            }
            //if (Request.QueryString["YARN_TYPE"] != null)
            //{
            //    YARN_TYPE = Request.QueryString["YARN_TYPE"];

            //}
            //else
            //{
            //    YARN_TYPE = string.Empty;
            //}
            if (Request.QueryString["PARTY_CODE"] != null)
            {
                PARTY_CODE = Request.QueryString["PARTY_CODE"];
            }
            else
            {
                PARTY_CODE = string.Empty;
            }
            if (Request.QueryString["FIBER_CODE"] != null)
            {
                FIBER_CODE = Request.QueryString["FIBER_CODE"];
            }
            else
            {
                FIBER_CODE = string.Empty;
            }

            if (Request.QueryString["StDate"] != null)
            {
                StDate = DateTime.Parse(Request.QueryString["StDate"].ToString());
            }
            else
            {
                StDate = oUserLoginDetail.DT_STARTDATE;
            }
            if (Request.QueryString["EnDate"] != null)
            {
                EnDate = DateTime.Parse(Request.QueryString["EnDate"].ToString());
            }


            if (Request.QueryString["TRAN_TYPE"] != null)
            {
                TRAN_TYPE = Request.QueryString["TRAN_TYPE"];

            }
            else
            {
                TRAN_TYPE = string.Empty;
            }
            if (Request.QueryString["PARTY_CODE"] != null)
            {
                PARTY_CODE = Request.QueryString["PARTY_CODE"];

            }
            else
            {
                PARTY_CODE = string.Empty;
            }
            if (Request.QueryString["BRANCH"] != null)
            {
                BRANCH = Request.QueryString["BRANCH"];

            }
            else
            {
                BRANCH = string.Empty;
            }
            if (Request.QueryString["TRNTYPE"] != null)
            {
                TRNTYPE = Request.QueryString["TRNTYPE"];

            }
            else
            {
                string TRNTYPE = string.Empty;
            }
            //if (Request.QueryString["YRNTYPE"] != null)
            //{
            //    YARNTYPE = Request.QueryString["YRNTYPE"];

            //}
            //else
            //{
            //    string YARNTYPE = string.Empty;
            //}
            if (Request.QueryString["FIBERCAT"] != null)
            {
                FIBERCAT = Request.QueryString["FIBERCAT"];

            }
            else
            {
                string YARNCAT = string.Empty;
            }

            if (Request.QueryString["DEPARTMENT1"] != null)
            {
                DEPARTMENT1 = Request.QueryString["DEPARTMENT1"];

            }
            else
            {
                DEPARTMENT1 = string.Empty;


            }
            if (Request.QueryString["FIBER1"] != null)
            {
                FIBER1 = Request.QueryString["FIBER1"];

            }
            else
            {
                FIBER1 = string.Empty;


            }
            if (Request.QueryString["YEAR"] != null)
            {
                YEAR1 = Request.QueryString["YEAR"];

            }
            else
            {
                YEAR1 = string.Empty;


            }
            if (Request.QueryString["PARTY"] != null)
            {
                PARTY = Request.QueryString["PARTY"];

            }
            else
            {
                PARTY = string.Empty;


            }


            DataTable dtrportdat = GetData(BRANCH_CODE, DEPT_CODE, FIBER_CAT, FIBER_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE);
            GetReport(dtrportdat);


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
            rDoc.Load(Server.MapPath(@"Fibertransactionreport.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("BRANCH", BRANCH);
            rDoc.SetParameterValue("user", oUserLoginDetail.Username);
            if (TRNTYPE != string.Empty)
            {
                rDoc.SetParameterValue("TRNTYPE", TRNTYPE);
            }
            else
            {
                rDoc.SetParameterValue("TRNTYPE", "ALL TYPE");
            }
            //if (YARNTYPE != string.Empty)
            //{
            //    rDoc.SetParameterValue("YARNTYPE", YARNTYPE);
            //}
            //else
            //{
            //    rDoc.SetParameterValue("YARNTYPE", "ALL TYPE");
            //}
            if (FIBERCAT != string.Empty)
            {

                rDoc.SetParameterValue("FIBERCAT", FIBERCAT);
            }
            else
            {
                rDoc.SetParameterValue("FIBERCAT", "ALL CATEGORY");
            }


            if (DEPARTMENT1 != string.Empty)
            {

                rDoc.SetParameterValue("DEPARTMENT1", DEPARTMENT1);
            }
            else
            {
                rDoc.SetParameterValue("DEPARTMENT1", "ALL DEPARTMENT");
            }

            if (FIBER1 != string.Empty)
            {

                rDoc.SetParameterValue("FIBER1", FIBER1);
            }
            else
            {
                rDoc.SetParameterValue("FIBER1", "ALL YARN");
            }

            rDoc.SetParameterValue("YEAR", YEAR1);
            rDoc.SetParameterValue("DATERANGE", "From date :" + StDate.ToShortDateString() + " To :" + EnDate.ToShortDateString());
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string BRANCH_CODE, string DEPT_CODE, string FIBER_CAT, string FIBER_CODE, DateTime StDate, DateTime EnDate, string TRAN_TYPE, string PARTY_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.FIBERTRANSACTION(BRANCH_CODE, DEPT_CODE, FIBER_CAT, FIBER_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE);
            return dt;
        }
        catch
        {
            throw;
        }

    }
}