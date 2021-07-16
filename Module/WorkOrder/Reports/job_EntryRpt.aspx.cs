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
using System.Data.OracleClient;
using errorLog;
using Common;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_WorkOrder_Reports_job_EntryRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
       DataSet ds = GetData();
       GetReport(ds);
    }

    public void GetReport(DataSet ds)
    {
        ReportDocument rDoc = new ReportDocument();
        //rDoc.Load(Server.MapPath(@"WorkOrderEntry.rpt"));
        rDoc.Load(Server.MapPath(@"WorkOrderEntryy.rpt"));
        rDoc.SetDataSource(ds);
        CrystalReportViewer1.ReportSource = rDoc;
     }
    string PR_TYPE;
    private DataSet GetData()
    {
        try
        {
            int From = 0;
            int To = 0;
            

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = null;
            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }
            if (Request.QueryString["WO_TYPE"] != null && Request.QueryString["WO_TYPE"] != "")
            {
                PR_TYPE = Request.QueryString["WO_TYPE"].ToString();
            }
            DataSet dt_Size = SaitexBL.Interface.Method.OD_WO_MST.Get_work_Entry_Report(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, PR_TYPE);

           // dt = SaitexBL.Interface.Method.APP_JOB_WORK_MST.GetDataForReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, WO_TYPE);


            //DataTable dt_Size = new DataTable();

            //dt_Size.Columns.Add("COMP_ADD", typeof(string));
            //dt_Size.Columns.Add("COMP_NAME", typeof(String));
            //dt_Size.Columns.Add("BRANCH_CODE", typeof(string));
            //dt_Size.Columns.Add("BRANCH_NAME", typeof(string));
            //dt_Size.Columns.Add("USERNAME", typeof(string));

            //DataRow dr = dt_Size.NewRow();

            //dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
            //dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            //dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
            //dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            //dr["USERNAME"] = oUserLoginDetail.Username;

           // dt_Size.Rows.Add(dr);

            DataSet ds = new DataSet();

           // ds.Tables.Add(dt_Size);
            //ds.Tables.Add(dt1);
            ////ds.Tables.Add(dt_Size);

            dt_Size.Tables[0].TableName = "WorkOrder_Entry";
            dt_Size.Tables[1].TableName = "TAX";
            dt_Size.Tables[2].TableName = "BOM";
            //ds.Tables[1].TableName = "ReportHeader";
            //ds.Tables[2].TableName = "JOB_WORK_SIZE_DETAILS";

            return dt_Size;

        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
}
