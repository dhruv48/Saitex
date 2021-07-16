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

public partial class Module_Yarn_SalesWork_Reports_YARN_LOTWISE_STOCK_REPORT : System.Web.UI.Page
{
   string    YarnCode= string.Empty;
   string    YarnDesc= string.Empty;
   string    PoNumb=string.Empty;
   string    LotNo=string.Empty;
   string DYED_BATCH = string.Empty;
   string   grade=string.Empty;
   string   NOOFUNIT=string.Empty;
   string   WEIGHTOFUNIT=string.Empty;
   string   SHADECODE=string.Empty;
   string   SHADEFAMILY=string.Empty;
   string   RGB=string.Empty;
   string  LOCATION = string.Empty;
   string TRNTYPE = string.Empty;
   string STORE = string.Empty;
   SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    

    protected void Page_Load(object sender, EventArgs e)
   {
       oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        
        if (Request.QueryString["YarnCode"] != null)
        {
            YarnCode = Request.QueryString["YarnCode"].ToString();
        }
        if (Request.QueryString["YarnDesc"] != null)
        {
            YarnDesc = Request.QueryString["YarnDesc"].ToString();
        }
        if (Request.QueryString["PoNumb"] != null)
        {
            PoNumb = Request.QueryString["PoNumb"].ToString();
        }
        if (Request.QueryString["LotNo"] != null)
        {
            LotNo = Request.QueryString["LotNo"].ToString();
        }
        if (Request.QueryString["DYED_BATCH"] != null)
        {
            DYED_BATCH = Request.QueryString["DYED_BATCH"].ToString();
        }
        if (Request.QueryString["grade"] != null)
        {
            grade = Request.QueryString["grade"].ToString();

        }
        if (Request.QueryString["NOOFUNIT"] != null)
        {
            NOOFUNIT = Request.QueryString["NOOFUNIT"].ToString();
        }
        if (Request.QueryString["WEIGHTOFUNIT"] != null)
        {
            WEIGHTOFUNIT = Request.QueryString["WEIGHTOFUNIT"].ToString();
        }
        if (Request.QueryString["SHADECODE"] != null)
        {
            SHADECODE = Request.QueryString["SHADECODE"].ToString();
        }
        if (Request.QueryString["SHADEFAMILY"] != null)
        {
            SHADEFAMILY = Request.QueryString["SHADEFAMILY"].ToString();
        }
        if (Request.QueryString["RGB"] != null)
        {
            RGB = Request.QueryString["RGB"].ToString();
        }
        if (Request.QueryString["LOCATION"] != null)
        {
            LOCATION = Request.QueryString["LOCATION"].ToString();
        }
        if (Request.QueryString["TRNTYPE"] != null)
        {
            TRNTYPE = Request.QueryString["TRNTYPE"].ToString();
        }
        if (Request.QueryString["STORE"] != null)
        {
            STORE = Request.QueryString["STORE"].ToString();
        }
      
        try
        {


            DataTable dtrportdat = GetData(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, YarnCode, YarnDesc, PoNumb, LotNo, DYED_BATCH, grade, NOOFUNIT, WEIGHTOFUNIT, SHADECODE, SHADEFAMILY, RGB, LOCATION, TRNTYPE, STORE);
            GetReport(dtrportdat);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
           
           
        }
    }
    private DataTable GetData( string COMP_CODE, string CH_BRANCHCODE, int Year,string YarnCode, string  YarnDesc,string  PoNumb,string  LotNo, string DYED_BATCH, string grade,string  NOOFUNIT,string  WEIGHTOFUNIT, string SHADECODE, string SHADEFAMILY,string  RGB, string LOCATION,string TRNTYPE,string STORE)
    {


        try
        {




            DataTable dt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetYarnStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, YarnCode, YarnDesc, PoNumb, LotNo, DYED_BATCH, grade, NOOFUNIT, WEIGHTOFUNIT, SHADECODE, SHADEFAMILY, RGB, LOCATION, TRNTYPE, STORE);

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
    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"YARN_LOTWISE_STOCK_REPORT.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
}
