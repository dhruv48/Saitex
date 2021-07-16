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
using System.Data.OracleClient;

public partial class Module_Yarn_SalesWork_Reports_MakeYarnTaxation : System.Web.UI.Page
{

    string TAXATIONTYPE = string.Empty;
    string where_Query = string.Empty;
    string BRANCH = string.Empty;
    string Comp_Code = string.Empty;
    string Branch_Code = string.Empty;
    string Year = string.Empty;
    string PRTYNAME = string.Empty;
    string GRNNo=string.Empty;
    string YarnDesc=string.Empty;
    string DateFrom = string.Empty;
    string DateTo = string.Empty;
   


    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData(where_Query, BRANCH, TAXATIONTYPE, YarnDesc, GRNNo, PRTYNAME, DateFrom, DateTo);
        GetReport(dt);
    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"MakeYarnTaxationReport.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable GetData(string where_Query, string BRANCH, string TAXATIONTYPE,string YarnDesc,string GRNNo,string PRTYNAME,string DateFrom,string DateTo)
    {

       SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
       

        try
        {
            //string where_Query = string.Empty; 

            where_Query = TAXATIONTYPE;
            if (Request.QueryString["TAXATION_TYPE"] != null && Request.QueryString["TAXATION_TYPE"] != "")
            {
                TAXATIONTYPE = Request.QueryString["TAXATION_TYPE"].ToString();

                where_Query += " and TAXATION_TYPE LIKE '" + TAXATIONTYPE + "%'";
            }

            if (Request.QueryString["BRANCH_NAME"] != null && Request.QueryString["BRANCH_NAME"] != "")
            {
                BRANCH = Request.QueryString["BRANCH_NAME"].ToString();

                where_Query += " and BRANCH_NAME LIKE '" + BRANCH + "%'";
            }
            if (Request.QueryString["YARN_DESC"] != null && Request.QueryString["YARN_DESC"] != "")
            {
                YarnDesc = Request.QueryString["YARN_DESC"].ToString();

                where_Query += " and YARN_DESC LIKE '" + YarnDesc + "%'";
            }
            if (Request.QueryString["TRN_NUMB"] != null && Request.QueryString["TRN_NUMB"] != "")
            {
                GRNNo = Request.QueryString["TRN_NUMB"].ToString();

                where_Query += " and TRN_NUMB LIKE '" + GRNNo + "%'";
            }
            if (Request.QueryString["PRTY_NAME"] != null && Request.QueryString["PRTY_NAME"] != "")
            {
                PRTYNAME = Request.QueryString["PRTY_NAME"].ToString();

                where_Query += " and PRTY_NAME LIKE '" + PRTYNAME + "%'";
            }
            if (Request.QueryString["TRN_DATE1"] != null && Request.QueryString["TRN_DATE2"] != "")
            {

                DateFrom = Request.QueryString["TRN_DATE1"].ToString();
                DateTo = Request.QueryString["TRN_DATE2"].ToString();

                where_Query += " AND TO_CHAR (TRN_DATE, 'dd/MM/yyyy') BETWEEN :TRN_DATE1 AND  :TRN_DATE2";
            }

           


            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetDataForReportTaxation(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, where_Query, BRANCH, TAXATIONTYPE, YarnDesc, GRNNo, PRTYNAME,DateFrom,DateTo);
            dt.TableName = "MakeYarnTaxation1";
            //DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetDataForReport(where_Query);

            if (!dt.Columns.Contains("COMP_NAME"))
                dt.Columns.Add("COMP_NAME", typeof(string));
            if (!dt.Columns.Contains("COMP_ADD"))
                dt.Columns.Add("COMP_ADD", typeof(string));
            if (!dt.Columns.Contains("BRANCH_NAME"))
                dt.Columns.Add("BRANCH_NAME", typeof(string));
            if (!dt.Columns.Contains("USER_NAME"))
                dt.Columns.Add("USER_NAME", typeof(string));
           
            SaitexDM.Common.DataModel.UserLoginDetail OUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = OUserLoginDetail.VC_COMPANYNAME;
                dr["COMP_ADD"] = OUserLoginDetail.COMP_ADD;
                dr["BRANCH_NAME"] = OUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = OUserLoginDetail.Username;
                
            }
            return dt;
        }

        catch (OracleException ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}