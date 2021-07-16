using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.OracleClient;


public partial class Module_OrderDevelopment_CustomerRequest_Reports_CustomerRequest4YD : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);
    }
    public DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        string PRODUCT_TYPE = "";
        string ArticleNo = "";
        string BusinessType = "";
        string Branch = "";
        string OrderNo = "";
        string Party = "";
        string Shade = "";
        string StDate = "";
        string EnDate = "";
        string Status = "";
        string Year = "";
        string Agent = "";  
        if (Request.QueryString["PRODUCT_TYPE"].ToString() != null)
        {
            PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
        }
        if (Request.QueryString["ArticleNo"].ToString() != null)
        {
            ArticleNo = Request.QueryString["ArticleNo"].ToString();
        }
        if (Request.QueryString["BusinessType"].ToString() != null)
        {
            BusinessType = Request.QueryString["BusinessType"].ToString();
        }
        if (Request.QueryString["Branch"].ToString() != null)
        {
            Branch = Request.QueryString["Branch"].ToString();
        }
        if (Request.QueryString["OrderNo"].ToString() != null)
        {
            OrderNo = Request.QueryString["OrderNo"].ToString();
        }
        if (Request.QueryString["Year"].ToString() != null)
        {
            Year = Request.QueryString["Year"].ToString();
        }

        if (Request.QueryString["Status"].ToString() != null)
        {
            Status = Request.QueryString["Status"].ToString();
        }

        if (Request.QueryString["EnDate"].ToString() != null)
        {
            EnDate = Request.QueryString["EnDate"].ToString();
        }

        if (Request.QueryString["StDate"].ToString() != null)
        {
            StDate = Request.QueryString["StDate"].ToString();
        }
        if (Request.QueryString["Shade"].ToString() != null)
        {
            Shade = Request.QueryString["Shade"].ToString();
        }
        if (Request.QueryString["Party"].ToString() != null)
        {
            Party = Request.QueryString["Party"].ToString();
        }
        if (Request.QueryString["Agent"].ToString() != null)
        {
            Agent = Request.QueryString["Agent"].ToString();
        }
        DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForCustomerRequestYDReport(PRODUCT_TYPE, ArticleNo, BusinessType, Branch, OrderNo, Party, Shade, StDate, EnDate, Status, Year, Agent);
       


            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));



            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dt.AcceptChanges();
            }
        

        return dt;
    }
    public void GetReport(DataTable dt)
    {
        ReportDocument Rdoc = new ReportDocument();
        Rdoc.Load(Server.MapPath(@"CR4YarnDyeing.rpt"));
        Rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = Rdoc;
    }
}
