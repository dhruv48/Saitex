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

public partial class Module_Inventory_Reports_Pending_PO : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
 
    string year= string.Empty ;
    string branch = string.Empty;
    DateTime StDate;
    DateTime  EnDate ;
    string vendor = string.Empty;
    string ITEM_CODE = string.Empty ;
    int fromPO = 0;
    int  topo = 0 ;
    string status = string .Empty ;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        year = Request.QueryString["year"];
        branch = Request.QueryString["branch"];
        StDate = DateTime.Parse(Request.QueryString["StDate"].ToString());
        EnDate = DateTime.Parse(Request.QueryString["EnDate"].ToString());
        vendor = Request.QueryString["vendor"].ToString();
        ITEM_CODE = Request.QueryString["ITEM_CODE"].ToString();
        if (Request.QueryString["fromPO"].ToString() != string.Empty && Request.QueryString["fromPO"].ToString() != null)
        {
            fromPO = int.Parse(Request.QueryString["fromPO"].ToString());
        }
        else
        {
            fromPO = 0;
        }
        if (Request.QueryString["topo"].ToString() != string.Empty && Request.QueryString["topo"].ToString() != null)
        {
            topo = int.Parse(Request.QueryString["topo"].ToString());
        }
        else
        {
            topo = 0;
        }

        if (Request.QueryString["status"].ToString() != string.Empty && Request.QueryString["status"].ToString() != null && Request.QueryString["status"].ToString() != "All")
        {
            status = Request.QueryString["status"];
        }
        else
        {
            status = string.Empty;
        }
        DataSet ds = GetPendingPOData(branch, year, vendor, StDate, EnDate, ITEM_CODE, fromPO, topo, status);
       
        
        GetReport(ds);
    }

    private DataSet GetPendingPOData(string branch, string year, string vendor, DateTime StDate, DateTime EnDate, string ITEM_CODE, int fromPO, int topo, string status)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.PendingPoQuery(branch, year, vendor, StDate, EnDate, ITEM_CODE, fromPO, topo, status);
           // DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.Load_Pend_PO_Details(branch, year,vendor);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("COMP_CODE", typeof(string));
            dt1.Columns.Add("COMP_NAME", typeof(String));
            dt1.Columns.Add("BRANCH_CODE", typeof(string));
            dt1.Columns.Add("BRANCH_NAME", typeof(string));
            dt1.Columns.Add("USERNAME", typeof(string));
            DataRow dr = dt1.NewRow();
            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
            dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
            dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            dr["USERNAME"] = oUserLoginDetail.Username;
            dt1.Rows.Add(dr);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            ds.Tables[0].TableName = "Pending_PO";
            ds.Tables[1].TableName = "ReportHeader";
            return ds;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
    private void GetReport(DataSet ds)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Pending_PO.rpt"));
        rDoc.SetDataSource(ds);
        CrystalReportViewer1.ReportSource = rDoc;
    }

}
