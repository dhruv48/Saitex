using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data.OracleClient;

public partial class Module_Waste_Report_WasteMasterReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData('Y');
        GetReport(dt);
    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"WasteMaster.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable GetData(char ch_View)
    {
        try
        {

            string ITEM_CODE = string.Empty;
            string ITEM_DESC = string.Empty;
            string Department_Code = string.Empty;
            string Item_category = string.Empty;
            string CH_BRANCHCODE = string.Empty;
            string ITEM_TYPE = string.Empty;

            string where_Query = string.Empty;

            if (Request.QueryString["ITEM_CODE"] != null && Request.QueryString["ITEM_CODE"] != "")
            {
                ITEM_CODE = Request.QueryString["ITEM_CODE"].ToString();

                where_Query += " and A.ITEM_CODE LIKE '" + ITEM_CODE + "%'";
            }
            if (Request.QueryString["ITEM_DESC"] != null && Request.QueryString["ITEM_DESC"] != "")
            {
                ITEM_DESC = Request.QueryString["ITEM_DESC"].ToString();
                where_Query += " and A.ITEM_DESC LIKE '" + ITEM_DESC + "%'";
            }
            if (Request.QueryString["DEPARTMENT"] != null && Request.QueryString["DEPARTMENT"] != "")
            {
                Department_Code = Request.QueryString["DEPARTMENT"].ToString();
                where_Query += " and A.DEPT_CODE='" + Department_Code + "'";
            }
            if (Request.QueryString["VC_CATEGORYCODE"] != null && Request.QueryString["VC_CATEGORYCODE"] != "")
            {
                Item_category = Request.QueryString["VC_CATEGORYCODE"].ToString();
                where_Query += " and A.CAT_CODE='" + Item_category + "'";
            }
            if (Request.QueryString["CH_BRANCHCODE"] != null && Request.QueryString["CH_BRANCHCODE"] != "")
            {
                CH_BRANCHCODE = Request.QueryString["CH_BRANCHCODE"].ToString();
                where_Query += " and A.BRANCH_CODE='" + CH_BRANCHCODE + "'";
            }
            if (Request.QueryString["ITEM_TYPE"] != null && Request.QueryString["ITEM_TYPE"] != "")
            {
                ITEM_TYPE = Request.QueryString["ITEM_TYPE"].ToString();
                where_Query += " and A.ITEM_TYPE='" + ITEM_TYPE + "'";
            }

            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetDataForReport(where_Query);
            
            if (!dt.Columns.Contains("COMP_NAME"))
                dt.Columns.Add("COMP_NAME", typeof(string));
            if (!dt.Columns.Contains("COMP_ADD"))
                dt.Columns.Add("COMP_ADD", typeof(string));
            if (!dt.Columns.Contains("BRANCH_NAME"))
                dt.Columns.Add("BRANCH_NAME", typeof(string));
            if (!dt.Columns.Contains("USER_NAME"))
                dt.Columns.Add("USER_NAME", typeof(string));
            if (!dt.Columns.Contains("DEVELOPER_COMP"))
                dt.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (!dt.Columns.Contains("DEVELOPER_WEB"))
                dt.Columns.Add("DEVELOPER_WEB", typeof(string));
            if (!dt.Columns.Contains("WORKS_ADDRESS"))
            {
                dt.Columns.Add("WORKS_ADDRESS", typeof(string));
            }
            SaitexDM.Common.DataModel.UserLoginDetail OUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = OUserLoginDetail.VC_COMPANYNAME;
                dr["COMP_ADD"] = OUserLoginDetail.COMP_ADD;
                dr["BRANCH_NAME"] = OUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = OUserLoginDetail.Username;
                dr["WORKS_ADDRESS"] = OUserLoginDetail.WORKS_ADDRESS;
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
