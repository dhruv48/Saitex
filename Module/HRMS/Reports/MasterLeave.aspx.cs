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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_HRMS_Reports_MasterLeave : System.Web.UI.Page
{
  public static   string TableName = string.Empty;
  public static string SQuery = string.Empty;
  public static string ReportName = string.Empty;
  public static string LV_ID = string.Empty;
  public static string LEAVE_NAME = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Request.QueryString["TName"] != string.Empty && Request.QueryString["TName"].ToString() != "")
        {
            TableName = Request.QueryString["TName"].ToString();
        }
        if (Request.QueryString["SQuery"] != string.Empty && Request.QueryString["SQuery"].ToString() != "")
        {
            SQuery = Request.QueryString["SQuery"].ToString();
        }
        if (Request.QueryString["RName"] != string.Empty && Request.QueryString["RName"].ToString() != "")
        {
            ReportName  = Request.QueryString["RName"].ToString();
        }
        if (Request.QueryString["LV_ID"].ToString() != "" && Request.QueryString["LV_ID"] != string.Empty )
        {
            LV_ID = Request.QueryString["LV_ID"].ToString();
        }
        DataTable dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load(Server.MapPath(@""+ ReportName + ".rpt"));
        rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rdoc;
    }
    private void Report_Name()
    {
        if (LV_ID == "1")
        {
            LEAVE_NAME = "CASUAL LEAVE REPORTS";
        }
        else if (LV_ID == "2")
        {
            LEAVE_NAME = "EARN LEAVE REPORTS";
        }
        else if (LV_ID == "3")
        {
            LEAVE_NAME = "SICK LEAVE REPORTS";
        }
        else if (LV_ID == "4")
        {
            LEAVE_NAME = "MEDICAL LEAVE REPORTS";
        }
        else if (LV_ID == "7")
        {
            LEAVE_NAME = "LEAVE WITHOUT PAY REPORTS";
        }
        else if (LV_ID == "9")
        {
            LEAVE_NAME = "COMPENSATORY OFF REPORTS";
        }
        else
        {
            LEAVE_NAME = "LEAVE REPORTS";
        }
        
    }
    private DataTable GetData()
    {
        Report_Name();
        DataTable dt = SaitexBL.Interface.Method.MASTER_SEARCH.Master_Query_LEAVE(TableName,SQuery );

        if (dt.Columns["COMP_NAME"] == null)
            dt.Columns.Add("COMP_NAME", typeof(string));

        if (dt.Columns["BRANCH_NAME"] == null)
            dt.Columns.Add("BRANCH_NAME", typeof(string));

        if (dt.Columns["USER_NAME"] == null)
            dt.Columns.Add("USER_NAME", typeof(string));

        if (dt.Columns["COMP_ADD"] == null)
            dt.Columns.Add("COMP_ADD", typeof(string));

        if (dt.Columns["DEVELOPER_COMP"] == null)
            dt.Columns.Add("DEVELOPER_COMP", typeof(string));

        if (dt.Columns["DEVELOPER_WEB"] == null)
            dt.Columns.Add("DEVELOPER_WEB", typeof(string));

        if (dt.Columns["RPTNAME"] == null)
            dt.Columns.Add("RPTNAME", typeof(string));

        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        foreach (DataRow dr in dt.Rows)
        {
            dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            dr["USER_NAME"] = oUserLoginDetail.Username;
            dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
            dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
            dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
            dr["RPTNAME"] = LEAVE_NAME.ToString();
            dt.AcceptChanges();
        }

        return dt;
    }

}
