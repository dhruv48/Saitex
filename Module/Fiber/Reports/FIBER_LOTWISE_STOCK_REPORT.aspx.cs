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

public partial class Module_Fiber_Reports_FIBER_LOTWISE_STOCK_REPORT : System.Web.UI.Page
{
    string BranchName = string.Empty;
    string tdate = string.Empty;
    string trndesc = string.Empty;
    string trntype = string.Empty;
    string trnno = string.Empty;
    string fibercode = string.Empty;
    string fiberdesc = string.Empty;
    string fibercat = string.Empty;
    string finalrate = string.Empty;
    string lotnostring = string.Empty;
    string lotno = string.Empty;
    string totalbale = string.Empty;
    string issubale = string.Empty;
    string balbale = string.Empty;
    string weightofunit = string.Empty;
    string trnqty = string.Empty;
    string totalvalue = string.Empty;
    string issueqty = string.Empty;
    string issuevalue = string.Empty;
    string balqty = string.Empty;
    string balvalue = string.Empty;
    string palletcode = string.Empty;
    string grade = string.Empty;
    string palletno = string.Empty;
    ReportDocument rDoc = new ReportDocument();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_unLoad(object sender, EventArgs e)
    {
        rDoc.Close();
        rDoc.Dispose();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (Request.QueryString["BRANCHNAME"] != null)
        {
            BranchName = Request.QueryString["BRANCHNAME"].ToString();
        }

        if (Request.QueryString["TDATE"] != null)
        {
            tdate = Request.QueryString["TDATE"].ToString();
        }


        trndesc = "";

        if (Request.QueryString["TRNTYPE"] != null)
        {
            trntype = Request.QueryString["TRNTYPE"].ToString();
        }

        if (Request.QueryString["TRNNO"] != null)
        {
            trnno = Request.QueryString["TRNNO"].ToString();
        }
        if (Request.QueryString["FIBERCODE"] != null)
        {
            fibercode = Request.QueryString["FIBERCODE"].ToString();
        }
        if (Request.QueryString["FIBERDESC"] != null)
        {
            fiberdesc = Request.QueryString["FIBERDESC"].ToString();
        }
        if (Request.QueryString["FIBERCAT"] != null)
        {
            fibercat = Request.QueryString["FIBERCAT"].ToString();
        }
        if (Request.QueryString["FINALRATE"] != null)
        {
            finalrate = Request.QueryString["FINALRATE"].ToString();
        }
        if (Request.QueryString["LOTNOSTRING"] != null)
        {
            lotnostring = Request.QueryString["LOTNOSTRING"].ToString();
        }

        if (Request.QueryString["LOTNO"] != null)
        {
            lotno = Request.QueryString["LOTNO"].ToString();
        }

        if (Request.QueryString["TOTALBALE"] != null)
        {
            totalbale = Request.QueryString["TOTALBALE"].ToString();
        }
        if (Request.QueryString["ISSUEBALE"] != null)
        {
            issubale = Request.QueryString["ISSUEBALE"].ToString();
        }
        if (Request.QueryString["BALEBALE"] != null)
        {
            balbale = Request.QueryString["BALEBALE"].ToString();
        }
        if (Request.QueryString["WEIGHTOFUNIT"] != null)
        {
            weightofunit = Request.QueryString["WEIGHTOFUNIT"].ToString();
        }
        if (Request.QueryString["TRNQTY"] != null)
        {
            trnqty = Request.QueryString["TRNQTY"].ToString();
        }
        if (Request.QueryString["TOTALVALUE"] != null)
        {
            totalvalue = Request.QueryString["TOTALVALUE"].ToString();
        }
        if (Request.QueryString["ISSUEQTY"] != null)
        {
            issueqty = Request.QueryString["ISSUEQTY"].ToString();
        }
        if (Request.QueryString["BALQTY"] != null)
        {
            balqty = Request.QueryString["BALQTY"].ToString();
        }
        if (Request.QueryString["BALVALUE"] != null)
        {
            balvalue = Request.QueryString["BALVALUE"].ToString();
        }
        if (Request.QueryString["PALLETCODE"] != null)
        {
            palletcode = Request.QueryString["PALLETCODE"].ToString();
        }
        if (Request.QueryString["GRADE"] != null)
        {
            grade = Request.QueryString["GRADE"].ToString();
        }

        if (Request.QueryString["PALLETNO"] != null)
        {
            palletno = Request.QueryString["PALLETNO"].ToString();
        }

    
         
         
        try
        {


                DataTable dtrportdat = GetData("", "", "", DateTime.MinValue, DateTime.MinValue, 0, BranchName,tdate,trndesc,trntype, trnno, fibercode, fiberdesc, fibercat,finalrate, lotnostring,totalbale, issubale, balbale,weightofunit,trnqty,totalvalue,issueqty,issuevalue,balqty, balvalue,palletcode,grade,palletno);
                GetReport(dtrportdat);
                dtrportdat.Dispose();
           
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
            rDoc.Load(Server.MapPath(@"FIBER_LOTWISE_STOCK_REPORT.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
            rDoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
            rDoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);
            rDoc.SetParameterValue("BRANCH_NAME", oUserLoginDetail.VC_BRANCHNAME);
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string BRANCH_CODE, string FIBER_CAT, string FIBER_CODE, DateTime StDate, DateTime EnDate, int Year, string BranchName,string tdate, string trndesc,string trntype,string trnno,string fibercode,string fiberdesc,string fibercat,string finalrate,string lotnostring,string totalbale,string issubale,string balbale,string weightofunit,string trnqty,string totalvalue,string issueqty,string issuevalue,string balqty,string balvalue,string palletcode, string grade,string palletno)
    {


     try 
        {

           

           
            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetFiberStockLotWise(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year,"", BranchName, tdate, trndesc, trntype,trnno, fibercode, fiberdesc,fibercat, finalrate, lotnostring, totalbale, issubale, balbale, weightofunit,trnqty, totalvalue, issueqty, issuevalue, balqty,balvalue, palletcode, grade, palletno);
           
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
