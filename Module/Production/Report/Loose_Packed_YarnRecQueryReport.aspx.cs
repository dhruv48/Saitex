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

public partial class Module_Production_Report_Loose_Packed_YarnRecQueryReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        string url = string.Empty;
      int Packing_Slip_No = 0;
    string YARN_CODE = string.Empty;
    string BRANCH_CODE = string.Empty;
    string PI_NO = string.Empty;
   string Department = string.Empty;
    string Lot_No = string.Empty;
   string Location= string.Empty;
     string Store= string.Empty;
     DateTime PackingDate = System.DateTime.Today;
     DateTime PackingToDate = System.DateTime.Today; ;
    if (Request.QueryString["Packing_Slip_No"] != null)
        {
            Packing_Slip_No =int.Parse( Request.QueryString["Packing_Slip_No"].ToString());
        }
    if (Request.QueryString["YARN_CODE"] != null)
        {
            YARN_CODE = Request.QueryString["YARN_CODE"].ToString();
        }
    if (Request.QueryString["Department"] != null)
        {
            Department = Request.QueryString["Department"].ToString();
        }
    if (Request.QueryString["Lot_No"] != null)
        {
            Lot_No = Request.QueryString["Lot_No"].ToString();
        }

    if (Request.QueryString["Location"] != null)
        {
            Location = Request.QueryString["Location"].ToString();
        }
    if (Request.QueryString["Store"] != null)
        {
            Store = Request.QueryString["Store"].ToString();
        }
    if (Request.QueryString["BRANCH_CODE"] != null)
        {
            BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();
        }
    if (Request.QueryString["PI_NO"] != null)
        {
            PI_NO = Request.QueryString["PI_NO"].ToString();
        }
    if (Request.QueryString["PackingDate"] != null)
    {
        PackingDate = Convert.ToDateTime(Request.QueryString["PackingDate"].ToString());
    }
    if (Request.QueryString["PackingToDate"] != null)
    {
        PackingToDate = Convert.ToDateTime(Request.QueryString["PackingToDate"].ToString());
    }
        try
        {
            DataTable dtReportdata = GetData(Packing_Slip_No, YARN_CODE, Department, Lot_No, Location, Store, BRANCH_CODE, PI_NO, PackingDate, PackingToDate);

            GetReport(dtReportdata);
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
            rDoc.Load(Server.MapPath(@"YarnLoosePackingProductionQR.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
            rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(int Packing_Slip_No, string YARN_CODE, string Department, string Lot_No, string Location, string Store, string BRANCH_CODE, string PI_NO, DateTime PackingDate,DateTime  PackingToDate)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetYarnLoosePackingQuery(Packing_Slip_No, YARN_CODE, Department, Lot_No, Location, Store, BRANCH_CODE, PI_NO, PackingDate, PackingToDate);
            if (dt.Rows.Count > 0)
            {
                //if (dt.Columns["COMP_NAME"] == null)
                //    dt.Columns.Add("COMP_NAME", typeof(string));

                //if (dt.Columns["BRANCH_NAME"] == null)
                //    dt.Columns.Add("BRANCH_NAME", typeof(string));

                //if (dt.Columns["USER_NAME"] == null)
                //    dt.Columns.Add("USER_NAME", typeof(string));
                //if (dt.Columns["COMP_ADD"] == null)
                //    dt.Columns.Add("COMP_ADD", typeof(string));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                //    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                //    dr["USER_NAME"] = oUserLoginDetail.Username;
                //    dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                //    dt.AcceptChanges();
                //}
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
