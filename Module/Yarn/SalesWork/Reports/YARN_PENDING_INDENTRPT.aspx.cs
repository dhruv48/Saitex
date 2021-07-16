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

public partial class Module_Yarn_SalesWork_Reports_YARN_PENDING_INDENTRPT : System.Web.UI.Page
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string yr = string.Empty;
    string branch = string.Empty;
    string department = string.Empty;
    string itemcode = string.Empty;
    string status = string.Empty;
    string url = string.Empty;
    string idate1 = string.Empty;
    string idate2 = string.Empty;
    string location = string.Empty;
    string store = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["yr"] != null)
        {
            yr = Request.QueryString["yr"].ToString();
        }
        if (Request.QueryString["branch"] != null)
        {
            branch = Request.QueryString["branch"].ToString();
        }
        if (Request.QueryString["department"] != null)
        {
            department = Request.QueryString["department"].ToString();
        }
        if (Request.QueryString["location"] != null)
        {
            location = Request.QueryString["location"].ToString();
        }

        if (Request.QueryString["store"] != null)
        {
            store = Request.QueryString["store"].ToString();
        }
        if (Request.QueryString["itemcode"] != null)
        {
            itemcode = Request.QueryString["itemcode"].ToString();
        }
        if (Request.QueryString["status"] != null)
        {
            status = Request.QueryString["status"].ToString();
        }
        if (Request.QueryString["idate1"] != null)
        {
            idate1 = Request.QueryString["idate1"].ToString();
        }
        if (Request.QueryString["idate2"] != null)
        {
            idate2 = Request.QueryString["idate2"].ToString();
        }

        try
        {


            DataTable dtrportdat = GetData(branch, department, yr, idate1, idate2, itemcode, status, location, store);
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
            rDoc.Load(Server.MapPath(@"YARN_PENDING_INDENTRPT.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }

    private DataTable GetData(string branch, string department, string yr, string idate1, string idate2, string itemcode, string status, string location, string store)
    {


        try
        {



            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt = SaitexBL.Interface.Method.YRN_INT_MST.Load_Pendyarn_Indent_Details(branch, department, yr, idate1, idate2, itemcode, status, location, store);

            if (dt.Rows.Count > 0)
            {
                if (dt.Columns["COMP_NAME"] == null)
                    dt.Columns.Add("COMP_NAME", typeof(string));

                if (dt.Columns["BRANCH_NAME"] == null)
                    dt.Columns.Add("BRANCH_NAME", typeof(string));

                if (dt.Columns["USER_NAME"] == null)
                    dt.Columns.Add("USER_NAME", typeof(string));
                if (dt.Columns["COMP_ADD"] == null)
                    dt.Columns.Add("COMP_ADD", typeof(string));



                foreach (DataRow dr in dt.Rows)
                {

                    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                    dr["USER_NAME"] = oUserLoginDetail.Username;
                    dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;


                    dt.AcceptChanges();
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
