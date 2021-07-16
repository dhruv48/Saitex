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

public partial class Module_OrderDevelopment_CustomerRequest_Reports_CR_Report_Yarn_Process : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string BRANCH_CODE = string.Empty;
    string BRANCH_NAME = string.Empty;
    string PARTY_CODE = string.Empty;
    string PARTY_NAME = string.Empty;
    string AGENT_CODE = string.Empty;
    string AGENT_NAME = string.Empty;
    string ARTICLE_CODE = string.Empty;
    string ARTICLE_NAME = string.Empty;
    string SHADE_CODE = string.Empty;
    string SHADE_NAME = string.Empty;   
    string STATUS = string.Empty;
    string STATUS_NAME = string.Empty;
    string ORDER_NO_DETAILS = string.Empty;
    string ORDER_NO = string.Empty;
    string YEAR = string.Empty;
    string REPORT_TYPE = string.Empty;
    string PRODUCT_TYPE = string.Empty;    
    string StDate;
    string EnDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            //if(Request.QueryString["BRANCH_CODE"]!=null)
            //BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
            //if (Request.QueryString["BRANCH_NAME"] != null)
            //BRANCH_NAME = Request.QueryString["BRANCH_NAME"].ToString();
            //if (Request.QueryString["PARTY_CODE"] != null)
            //PARTY_CODE = Request.QueryString["PARTY_CODE"].ToString();
            //if (Request.QueryString["PARTY_NAME"] != null)
            //PARTY_NAME = Request.QueryString["PARTY_NAME"].ToString();
            //if (Request.QueryString["ARTICLE_CODE"] != null)
            //ARTICLE_CODE = Request.QueryString["ARTICLE_CODE"].ToString();
            //if (Request.QueryString["ARTICLE_NAME"] != null)
            //ARTICLE_NAME = Request.QueryString["ARTICLE_NAME"].ToString();
            //if (Request.QueryString["SHADE_CODE"] != null)
            //SHADE_CODE = Request.QueryString["SHADE_CODE"].ToString();
            //if (Request.QueryString["SHADE_NAME"] != null)
            //SHADE_NAME = Request.QueryString["SHADE_NAME"].ToString();
            //if (Request.QueryString["YEAR"] != null)
            //YEAR = Request.QueryString["YEAR"].ToString();
            //if (Request.QueryString["ORDER_NO"] != null)
            //ORDER_NO = Request.QueryString["ORDER_NO"].ToString();
            //if (Request.QueryString["ORDER_NO_DETAILS"] != null)
            //ORDER_NO_DETAILS = Request.QueryString["ORDER_NO_DETAILS"].ToString();
            //if (Request.QueryString["STATUS"] != null)
            //STATUS = Request.QueryString["STATUS"].ToString();
            //if (Request.QueryString["STATUS_NAME"] != null)
            //STATUS_NAME = Request.QueryString["STATUS_NAME"].ToString();
            //if (Request.QueryString["StDate"] != null)
            //StDate = Request.QueryString["StDate"].ToString();
            //if (Request.QueryString["EnDate"] != null)
            //EnDate = Request.QueryString["EnDate"].ToString();
            //if (Request.QueryString["REPORT_TYPE"] != null)
            //REPORT_TYPE = Request.QueryString["REPORT_TYPE"].ToString();
            //if (Request.QueryString["PRODUCT_TYPE"] != null)
            //PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            //GetReport(GetData(oUserLoginDetail.COMP_CODE, BRANCH_CODE, PARTY_CODE, ARTICLE_CODE, SHADE_CODE, YEAR, ORDER_NO, STATUS, StDate, EnDate, PRODUCT_TYPE));

            if (Session["CR_REPORT_YARN_PROCESS"] != null)
            {
                DataTable dt = (DataTable)Session["CR_REPORT_YARN_PROCESS"];
                if (dt.Rows.Count > 0)
                {
                    BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
                    BRANCH_NAME = dt.Rows[0]["BRANCH_NAME"].ToString();
                    PARTY_CODE = dt.Rows[0]["PARTY_CODE"].ToString();
                    PARTY_NAME = dt.Rows[0]["PARTY_NAME"].ToString();
                    ARTICLE_CODE = dt.Rows[0]["ARTICLE_CODE"].ToString();
                    ARTICLE_NAME = dt.Rows[0]["ARTICLE_NAME"].ToString();
                    SHADE_CODE = dt.Rows[0]["SHADE_CODE"].ToString();
                    SHADE_NAME = dt.Rows[0]["SHADE_NAME"].ToString();
                    YEAR = dt.Rows[0]["YEAR"].ToString();
                    ORDER_NO = dt.Rows[0]["ORDER_NO"].ToString();
                    ORDER_NO_DETAILS = dt.Rows[0]["ORDER_NO_DETAILS"].ToString();
                    STATUS = dt.Rows[0]["STATUS"].ToString();
                    STATUS_NAME = dt.Rows[0]["STATUS_NAME"].ToString();
                    StDate = dt.Rows[0]["StDate"].ToString();
                    EnDate = dt.Rows[0]["EnDate"].ToString();
                    REPORT_TYPE = dt.Rows[0]["REPORT_TYPE"].ToString();
                    PRODUCT_TYPE = dt.Rows[0]["PRODUCT_TYPE"].ToString();
                    AGENT_CODE = dt.Rows[0]["AGENT_CODE"].ToString();
                    AGENT_NAME = dt.Rows[0]["AGENT_NAME"].ToString();
                    GetReport(GetData(oUserLoginDetail.COMP_CODE, BRANCH_CODE, PARTY_CODE, ARTICLE_CODE, SHADE_CODE, YEAR, ORDER_NO, STATUS, StDate, EnDate, PRODUCT_TYPE,AGENT_CODE));
                }


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
            rDoc.Load(Server.MapPath(@"CRReport" + REPORT_TYPE + "Wise_YarnProcess.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("BRANCH", BRANCH_NAME);           
            if (PARTY_CODE != string.Empty)
            {
                rDoc.SetParameterValue("PARTY", PARTY_NAME);
            }
            else
            {
                rDoc.SetParameterValue("PARTY", "ALL PARTY");
            }
            if (ARTICLE_CODE != string.Empty)
            {

                rDoc.SetParameterValue("ARTICLE", ARTICLE_NAME );
            }
            else
            {
                rDoc.SetParameterValue("ARTICLE", "ALL YARN");
            }

            if (SHADE_CODE != string.Empty)
            {

                rDoc.SetParameterValue("SHADE", SHADE_NAME);
            }
            else
            {
                rDoc.SetParameterValue("SHADE", "ALL SHADE");
            }
            if (STATUS != string.Empty)
            {

                rDoc.SetParameterValue("STATUS", STATUS_NAME);
            }
            else
            {
                rDoc.SetParameterValue("STATUS", "ALL");
            }
            rDoc.SetParameterValue("DATERANGE", "From date :" + StDate + " To :" + EnDate);

            CrystalReportViewer1.ReportSource = rDoc;
            //  rDoc.PrintToPrinter(1, false, 1, 1);
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string COMP_CODE,string BRANCH_CODE, string PARTY_CODE,  string ARTICLE_CODE,  string SHADE_CODE, string YEAR, string ORDER_NO, string STATUS, string StDate, string EnDate, string PRODUCT_TYPE,string AGENT_CODE)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetDataForReportForYarnProcess(COMP_CODE, BRANCH_CODE, PARTY_CODE, ARTICLE_CODE, SHADE_CODE, YEAR, ORDER_NO, STATUS, StDate, EnDate, PRODUCT_TYPE, AGENT_CODE);
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns["COMP_NAME"] == null)
                    dt.Columns.Add("COMP_NAME", typeof(string));
                
                if (!dt.Columns.Contains("TITLE"))
                    dt.Columns.Add("TITLE", typeof(string));

                if (!dt.Columns.Contains("Date_Range"))
                    dt.Columns.Add("Date_Range", typeof(string));
                
                if (dt.Columns["USER_NAME"] == null)
                    dt.Columns.Add("USER_NAME", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {                  
                    dr["Date_Range"] = "From date :" + StDate + " To :" + EnDate;
                    dr["TITLE"] = "Sale Order Report " + REPORT_TYPE + " Wise";
                    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;                    
                    dr["USER_NAME"] = oUserLoginDetail.Username;
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage(" Data not found accroding to this record . ");
            }
            return dt;
        }
        catch
        {
            throw;
        }
    }
}
