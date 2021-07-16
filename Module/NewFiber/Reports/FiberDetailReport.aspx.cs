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
    string FIBERCAT = string.Empty;
    string PARTY = string.Empty;
    //string SHADCODE = string.Empty;
    string COMP_NAME1 = string.Empty;
    string BRANCH_NAME1 = string.Empty;
    string USER_NAME = string.Empty;
    string COMP_ADD = string.Empty;
    //string YARNTYPE = string.Empty;
    string BRANCH_CODE = string.Empty;
    string FIBER_CAT = string.Empty;
    //string YARN_TYPE = string.Empty;
    string PRTY_CODE = string.Empty;
   // string SHADE_CODE = string.Empty;
    string FIBER_CODE = string.Empty;
    DateTime StDate;
    DateTime EnDate;
    int chksumry = 0;
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
            if (Request.QueryString["PRTY_CODE"] != null)
            {
                PRTY_CODE = Request.QueryString["PRTY_CODE"];
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            //if (Request.QueryString["SHADE_CODE"] != null)
            //{
            //    SHADE_CODE = Request.QueryString["SHADE_CODE"];

            //}
            //else
            //{
            //    SHADE_CODE = string.Empty;
            //}
            if (Request.QueryString["BRANCH"] != null)
            {
                BRANCH = Request.QueryString["BRANCH"];

            }
            else
            {
                BRANCH = string.Empty;
            }
            if (Request.QueryString["FIBERCAT"] != null)
            {
                FIBERCAT = Request.QueryString["FIBERCAT"];

            }
            else
            {
                string FIBERCAT = string.Empty;
            }
            //if (Request.QueryString["YARNTYPE"] != null)
            //{
            //    YARNTYPE = Request.QueryString["YARNTYPE"];

            //}
            //else
            //{
            //    YARNTYPE = string.Empty;
            //}
            if (Request.QueryString["PARTY"] != null)
            {
                PARTY = Request.QueryString["PARTY"];

            }
            else
            {
                PARTY = string.Empty;


            }

            //if (Request.QueryString["SHADCODE"] != null)
            //{
            //    SHADCODE = Request.QueryString["SHADCODE"];

            //}
            //else
            //{
            //    SHADCODE = string.Empty;
            //}
            if (Request.QueryString["chk"] != null && Request.QueryString["chk"] != "")
            {
                chksumry = 1;
            }
            else
            {
                chksumry = 0;
            }
            if (Request.QueryString["FIBER_CODE"] != null && Request.QueryString["FIBER_CODE"] != "")
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
            DataTable dtrportdat = GetData(BRANCH_CODE, FIBER_CAT, PRTY_CODE, FIBER_CODE, StDate, EnDate);
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
            if (chksumry == 0)
            {
                rDoc.Load(Server.MapPath(@"FiberDetailStock.rpt"));

            }
            else
            {
                rDoc.Load(Server.MapPath(@"FiberSumryReport.rpt"));
            }
            rDoc.SetDataSource(dt);

            rDoc.SetParameterValue("BRANCH", BRANCH);
           // rDoc.SetParameterValue("FIBERCAT", FIBERCAT);
            if (PARTY != string.Empty)
            {
                rDoc.SetParameterValue("PARTY", PARTY);
            }
            else
            {
                rDoc.SetParameterValue("PARTY", "ALL TYPE");
            }
            //if (SHADCODE != string.Empty)
            //{

            //    rDoc.SetParameterValue("SHADCODE", SHADCODE);
            //}
            //else
            //{
            //    rDoc.SetParameterValue("SHADCODE", "ALL CATEGORY");
            //}

            rDoc.SetParameterValue("COMP_NAME1", oUserLoginDetail.VC_COMPANYNAME);
            rDoc.SetParameterValue("BRANCH_NAME1", oUserLoginDetail.VC_BRANCHNAME);
            rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);


            rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);

            //if (FIBERCODE != string.Empty)
            //{

            //    rDoc.SetParameterValue("YARNTYPE", YARNTYPE);
            //}
            //else
            //{
            //    rDoc.SetParameterValue("YARNTYPE", "YARNTYPE");
            //}

            CrystalReportViewer1.ReportSource = rDoc;




        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string BRANCH_CODE, string FIBER_CAT, string PRTY_CODE, string FIBER_CODE, DateTime StDate, DateTime EnDate)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetFiberDetail(BRANCH_CODE, FIBER_CAT, PRTY_CODE, FIBER_CODE, StDate, EnDate);

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
