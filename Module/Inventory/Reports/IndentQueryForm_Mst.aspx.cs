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
using Common;
using errorLog;
public partial class Module_Inventory_Queries_IndentQueryForm_Mst : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Session["IndentQueryReport"] != null)
            {
                DataTable dt = (DataTable)Session["IndentQueryReport"];

                string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
                string BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
                string ITEM_TYPE = dt.Rows[0]["ITEM_TYPE"].ToString();
                string ITEM_CATE = dt.Rows[0]["ITEM_CATE"].ToString();
                string LOCATION = dt.Rows[0]["LOCATION"].ToString();
                string STORE = dt.Rows[0]["STORE"].ToString();
                DataTable dtrportdat = GetData(BRANCH_CODE, DEPT_CODE, ITEM_TYPE, ITEM_CATE,LOCATION,STORE);
                GetReport(dtrportdat);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load(Server.MapPath(@"IndentQueryForm.rpt"));
       
        rdoc.SetDataSource(dt);
        rdoc.SetParameterValue("BRANCH", oUserLoginDetail.VC_BRANCHNAME);
        rdoc.SetParameterValue("COMPNAME", oUserLoginDetail.VC_COMPANYNAME);
        rdoc.SetParameterValue("COMPADD", oUserLoginDetail.COMP_ADD);
        rdoc.SetParameterValue("USERNAME", oUserLoginDetail.Username);
        CrystalReportViewer1.ReportSource = rdoc;

    }

    private DataTable GetData(string BRANCH_CODE, string DEPT_CODE, string ITEM_TYPE, string ITEM_CATE,string LOCATION,string STORE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetItemIndMst1(BRANCH_CODE, DEPT_CODE, ITEM_TYPE, ITEM_CATE,LOCATION,STORE);   

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


