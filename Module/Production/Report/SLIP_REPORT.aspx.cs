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

public partial class Module_Production_Report_SLIP_REPORT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        GetReport(dt);
    }



    private void GetReport(DataSet dt)
    {

        ReportDocument rDoc = new ReportDocument();
        string ReportPath = string.Empty;

        ReportPath = Server.MapPath(@"SLIP_REPORT.rpt");

        rDoc.Load(ReportPath);
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    private DataSet GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            string JOB_CARD = "";
            string PRTY_NAME = "";
            string QUALITY = "";
            string SHADE = "";
            string REF_NO = "";
            string NO_OF_SLIP = "";


            if (Request.QueryString["JOB_CARD"] != null)
            {
                JOB_CARD = Request.QueryString["JOB_CARD"].ToString().Trim();
            }
            if (Request.QueryString["PRTY_NAME"] != null)
            {
                PRTY_NAME = Request.QueryString["PRTY_NAME"].ToString().Trim();
            }
            if (Request.QueryString["QUALITY"] != null)
            {
                QUALITY = Request.QueryString["QUALITY"].ToString().Trim();
            }
            if (Request.QueryString["SHADE"] != null)
            {
                SHADE = Request.QueryString["SHADE"].ToString().Trim();
            }
            if (Request.QueryString["REF_NO"] != null)
            {
                REF_NO = Request.QueryString["REF_NO"].ToString().Trim();
            }
            if (Request.QueryString["NO_OF_SLIP"] != null)
            {
                NO_OF_SLIP = Request.QueryString["NO_OF_SLIP"].ToString().Trim();
            }


            

            
            DataTable dt = new DataTable();
            dt.Columns.Add("JOB_CARD", typeof(string));
            dt.Columns.Add("PRTY_NAME", typeof(string));
            dt.Columns.Add("QUALITY", typeof(string));
            dt.Columns.Add("SHADE", typeof(string));
            dt.Columns.Add("REF_NO", typeof(string));
            dt.Columns.Add("NO_OF_SLIP", typeof(Int32));



            
            DataRow row;
            for(int i=0;i < int.Parse(NO_OF_SLIP);i++)
            {
                row= dt.NewRow();
                row["JOB_CARD"] = JOB_CARD;
                row["PRTY_NAME"] = PRTY_NAME;
                row["QUALITY"] = QUALITY;
                row["SHADE"] = SHADE;
                row["REF_NO"] = REF_NO;
                row["NO_OF_SLIP"] = i;
                dt.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "SLIP_REPORT";
           

            //DataSet dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDataForReport(oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString(), oUserLoginDetail.DT_STARTDATE.Year.ToString(), From, To);
            //dt.Tables[0].TableName = "DYEING_PRODUCTION";

            return ds;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }


   

   

}
