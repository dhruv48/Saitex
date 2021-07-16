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
public partial class Module_Yarn_SalesWork_Reports_YarnTransactionCrstReport : System.Web.UI.Page
{

    string  BRANCH_CODE = string.Empty;
    string  DEPT_CODE = string.Empty;
    string TO_DEPT_CODE = string.Empty;
    string  YARN_CAT = string.Empty;
    string YARN_TYPE = string.Empty;
    string YARN_CODE = string.Empty;
    DateTime StDate;
    DateTime EnDate;
    string TRAN_TYPE = string.Empty;
    string PARTY_CODE= string.Empty;
    string BRANCH= string.Empty;
    string TRNTYPE= string.Empty;
    string YARNTYPE= string.Empty;
    string YARNCAT= string.Empty;
    string DEPARTMENT1= string.Empty;
    string YARN1= string.Empty;
    string YEAR1= string.Empty;
    string PARTY = string.Empty;
    string LOT_NO = string.Empty;
      
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
            if (Request.QueryString["TO_DEPT_CODE"] != null)
            {
                TO_DEPT_CODE = Request.QueryString["TO_DEPT_CODE"];

            }
            else
            {
                TO_DEPT_CODE = string.Empty;
            }

            if(Request.QueryString["LOT_NO"] != null)
            {
                LOT_NO = Request.QueryString["LOT_NO"];

            }
            else
            {
                LOT_NO = string.Empty;

            }

            if (Request.QueryString["YARN_CAT"] != null)
            {
                YARN_CAT = Request.QueryString["YARN_CAT"];

            }
            else
            {
                YARN_CAT = string.Empty;
            }
            if (Request.QueryString["YARN_TYPE"] != null)
            {
                YARN_TYPE = Request.QueryString["YARN_TYPE"];

            }
            else
            {
                YARN_TYPE = string.Empty;
            }
            if (Request.QueryString["PARTY_CODE"] != null)
            {
                PARTY_CODE = Request.QueryString["PARTY_CODE"];
            }
            else
            {
                PARTY_CODE = string.Empty;
            }
            if (Request.QueryString["YARN_CODE"] != null)
            {
                YARN_CODE = Request.QueryString["YARN_CODE"];
            }
            else
            {
                YARN_CODE = string.Empty;
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
                EnDate = DateTime.Parse (Request.QueryString["EnDate"].ToString());
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
            if (Request.QueryString["YRNTYPE"] != null)
            {
                YARNTYPE = Request.QueryString["YRNTYPE"];

            }
            else
            {
                string YARNTYPE = string.Empty;
            }
            if (Request.QueryString["YARNCAT"] != null)
            {
                YARNCAT = Request.QueryString["YARNCAT"];

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
            if (Request.QueryString["YARN1"] != null)
            {
                YARN1 = Request.QueryString["YARN1"];

            }
            else
            {
                YARN1 = string.Empty;


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


            DataTable dtrportdat = GetData(BRANCH_CODE, DEPT_CODE,TO_DEPT_CODE, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE,LOT_NO);
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
            rDoc.Load(Server.MapPath(@"Yarntransactionreport.rpt"));
            rDoc.SetDataSource(dt);  
            rDoc.SetParameterValue("BRANCH",BRANCH);
           
            if (TRNTYPE != string.Empty)
            {
                rDoc.SetParameterValue("TRNTYPE", TRNTYPE);
            }
            else
            {
                rDoc.SetParameterValue("TRNTYPE", "ALL TYPE");
            }
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

      
            if (DEPARTMENT1 != string.Empty)
            {

                rDoc.SetParameterValue("DEPARTMENT1", DEPARTMENT1);
            }
            else
            {
                rDoc.SetParameterValue("DEPARTMENT1", "ALL DEPARTMENT");
            }

            if (YARN1 != string.Empty)
            {

                rDoc.SetParameterValue("YARN1", YARN1);
            }
            else
            {
                rDoc.SetParameterValue("YARN1", "ALL YARN");
            }

            rDoc.SetParameterValue("YEAR",YEAR1);
            rDoc.SetParameterValue("DATERANGE", "From date :" + StDate.ToShortDateString() + " To :" + EnDate.ToShortDateString());          
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string BRANCH_CODE, string DEPT_CODE,string TO_DEPT_CODE, string YARN_CAT, string YARN_TYPE, string YARN_CODE, DateTime StDate, DateTime EnDate, string TRAN_TYPE, string PARTY_CODE,string LOT_NO)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.YARNTRANSACTION(BRANCH_CODE, DEPT_CODE, TO_DEPT_CODE, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE, "", "", "", "", LOT_NO);
           return dt;
        }
        catch
        {
            throw;
        }

    }
}