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

public partial class Inventory_VendorMasterRpt : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        string PRTY_CODE = string.Empty;
        string PRTY_CITY = string.Empty;
        string VENDOR_CODE = string.Empty;
        string VENDOR_CAT = string.Empty;
        string MOBILENO = string.Empty;
        string STATUS = string.Empty;
        string CREDITLIMIT = string.Empty;
        string REGION = string.Empty;
        string PINCODE = string.Empty;
        if (Request.QueryString["PRTY_CODE"] != null)
        {
            PRTY_CODE = Request.QueryString["PRTY_CODE"].ToString();
        }
        if (Request.QueryString["PRTY_CITY"] != null)
        {
            PRTY_CITY = Request.QueryString["PRTY_CITY"].ToString();
        }
        if (Request.QueryString["VENDOR_CODE"] != null)
        {
            VENDOR_CODE = Request.QueryString["VENDOR_CODE"].ToString();
        }
        if (Request.QueryString["VENDOR_CAT"] != null)
        {
            VENDOR_CAT = Request.QueryString["VENDOR_CAT"].ToString();
        }
        if (Request.QueryString["MOBILENO"] != null)
        {
            MOBILENO = Request.QueryString["MOBILENO"].ToString();
        }
        if (Request.QueryString["STATUS"] != null)
        {
            STATUS = Request.QueryString["STATUS"].ToString();
        }
        if (Request.QueryString["CREDITLIMIT"] != null)
        {
            CREDITLIMIT = Request.QueryString["CREDITLIMIT"].ToString();
        }
        if (Request.QueryString["REGION"] != null)
        {
            REGION = Request.QueryString["REGION"].ToString();
        }

        if (Request.QueryString["PINCODE"] != null)
        {
            PINCODE = Request.QueryString["PINCODE"].ToString();
        }
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        DataTable dt = GetData(PRTY_CODE, PRTY_CITY, VENDOR_CODE, VENDOR_CAT, MOBILENO, CREDITLIMIT, REGION, STATUS, PINCODE);
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"VendorMaster.rpt"));     
        rDoc.SetDataSource(dt);
        rDoc.SetParameterValue("USER", oUserLoginDetail.Username);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable GetData(string PRTY_CODE, string PRTY_CITY, string VENDOR_CODE, string VENDOR_CAT, string MOBILENO, string CREDITLIMIT, string REGION, string STATUS, string PINCODE)
    {
       

            try 
            {
               
                SaitexDM.Common.DataModel.TX_VENDOR_MST oTX_VENDOR_MST = new SaitexDM.Common.DataModel.TX_VENDOR_MST();
                DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.PrintVendor(PRTY_CODE, PRTY_CITY, VENDOR_CODE, VENDOR_CAT, MOBILENO, CREDITLIMIT, REGION, STATUS, PINCODE);
                return dt;
            }
            catch(Exception ex) 
            {
                throw ex;
            }


            
    }
}
