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
using Common;

public partial class Module_FA_Reports_GroupMstReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string GroupCodeStart = string.Empty;
    public string GroupCodeEnd = string.Empty;
    public string IsGroup = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["IsGroup"] != null && Request.QueryString["IsGroup"] != "")
            {
                IsGroup = Request.QueryString["IsGroup"].ToString().Trim();
            }

            if (Request.QueryString["GroupCodeStart"] != null && Request.QueryString["GroupCodeStart"] != "" && Request.QueryString["GroupCodeEnd"] != null && Request.QueryString["GroupCodeEnd"] != "")
            {
                GroupCodeStart = Request.QueryString["GroupCodeStart"].ToString().Trim();
                GroupCodeEnd = Request.QueryString["GroupCodeEnd"].ToString().Trim();
            }

            DataTable dt = GetData(IsGroup, GroupCodeStart, GroupCodeEnd);
            GetReport(dt);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading page.\r\nSee error log for detail."));
        }
    }

    private DataTable GetData(string IsGroup, string GroupCodeStart, string GroupCodeEnd)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_GRP_MST.GetReportNew(IsGroup, GroupCodeStart, GroupCodeEnd);
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

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dt.AcceptChanges();
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
            ReportDocument rdoc = new ReportDocument();
            rdoc.Load(Server.MapPath(@"GroupMaster.rpt"));
            rdoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rdoc;
        }
        catch
        {
            throw;
        }
    }
}