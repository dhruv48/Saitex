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

public partial class Module_Production_Report_Yarn_Packed_Report : System.Web.UI.Page
{
    string Query_String = string.Empty;
    string BRANCH_CODE = string.Empty;
    string DEPT_CODE = string.Empty;
    string PROS_ID_NO = string.Empty;
    string LOT_NUMBER = string.Empty;
    string ORDER_NO = string.Empty;
    string FromDate = string.Empty;
    string ToDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData(Query_String);
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Yarn_Packed_Report1.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable GetData(string Query_String)
    {

        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        //string BRANCH_CODE = string.Empty;
        //string DEPT_CODE = string.Empty;
        //string PROS_ID_NO = string.Empty;
        //string LOT_NUMBER = string.Empty;
        //string ORDER_NO = string.Empty;
        //string FromDate = string.Empty;
        //string ToDate = string.Empty;

        try
        {
            string where_Query = string.Empty;

            //if (Request.QueryString["BRANCH_CODE "] != null && Request.QueryString["BRANCH_CODE"] != "")
            //{
            //    BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString();

            //    where_Query += " and A.BRANCH_CODE LIKE '" + BRANCH_CODE + "%'";
            //}
            //if (Request.QueryString["DEPT_CODE"] != null && Request.QueryString["DEPT_CODE"] != "")
            //{
            //    DEPT_CODE = Request.QueryString["DEPT_CODE"].ToString();

            //    where_Query += " and A.DEPT_CODE LIKE '" + DEPT_CODE + "%'";
            //}
            //if (Request.QueryString["PROS_ID_NO "] != null && Request.QueryString["PROS_ID_NO "] != "")
            //{
            //    PROS_ID_NO = Request.QueryString["PROS_ID_NO "].ToString();

            //    where_Query += " and A.PROS_ID_NO  LIKE '" + PROS_ID_NO + "%'";
            //}
            ////if (Request.QueryString["LOT_NUMBER"] != null && Request.QueryString["LOT_NUMBER "] != "")
            ////{
            ////    LOT_NUMBER = Request.QueryString["LOT_NUMBER "].ToString();
            ////    where_Query += "and A.LOT_NUMBER LIKE '" + LOT_NUMBER + "%'";
            ////}
            //if(Request.QueryString["ORDER_NO "]!=null && Request.QueryString["ORDER_NO "]!="")
            //{
            //    ORDER_NO =Request.QueryString["ORDER_NO "].ToString();
            //    where_Query += " and A.ORDER_NO LIKE '" + ORDER_NO + "%' " ;
            //}



            // DataTable dt = new DataTable();

           DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetPackedYarnQuery(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, ORDER_NO);
           
            //DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetReportForProduction(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, Query_String);
            dt.TableName = "Yarn_Packing_Recepit1";


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
