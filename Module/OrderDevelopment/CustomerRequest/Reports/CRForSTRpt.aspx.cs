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
using System.IO;

using Common;

public partial class Module_Inventory_Reports_CRForSTRpt : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string strBranch = string.Empty;
    private string strParty = string.Empty;
    private string strArticle = string.Empty;
    private string strShadeFamily = string.Empty;
    private string strShadeCode = string.Empty;
    private string strCRFrom = string.Empty;
    private string strCRTo = string.Empty;
    private string strStatus = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["strBranch"] != null && Request.QueryString["strBranch"] != "")
            {
                strBranch = Request.QueryString["strBranch"].ToString().Trim();
                //strBranch = strBranch.Replace("$", "
            }
            else
            {
                strBranch = string.Empty;
            }

            if (Request.QueryString["strParty"] != null && Request.QueryString["strParty"] != "")
            {
                strParty = Request.QueryString["strParty"].ToString().Trim();
            }
            else
            {
                strParty = string.Empty;
            }

            if (Request.QueryString["strArticle"] != null && Request.QueryString["strArticle"] != "")
            {
                strArticle = Request.QueryString["strArticle"].ToString().Trim();
            }
            else
            {
                strArticle = string.Empty;
            }

            if (Request.QueryString["strShadeFamily"] != null && Request.QueryString["strShadeFamily"] != "")
            {
                strShadeFamily = Request.QueryString["strShadeFamily"].ToString().Trim();
            }
            else
            {
                strShadeFamily = string.Empty;
            }

            if (Request.QueryString["strShadeCode"] != null && Request.QueryString["strShadeCode"] != "")
            {
                strShadeCode = Request.QueryString["strShadeCode"].ToString().Trim();
            }
            else
            {
                strShadeCode = string.Empty;
            }

            if (Request.QueryString["strCRFrom"] != null && Request.QueryString["strCRFrom"] != "")
            {
                strCRFrom = Request.QueryString["strCRFrom"].ToString().Trim();
            }
            else
            {
                strCRFrom = string.Empty;
            }

            if (Request.QueryString["strCRTo"] != null && Request.QueryString["strCRTo"] != "")
            {
                strCRTo = Request.QueryString["strCRTo"].ToString().Trim();
            }
            else
            {
                strCRTo = string.Empty;
            }

            if (Request.QueryString["strStatus"] != null && Request.QueryString["strStatus"] != "")
            {
                strStatus = Request.QueryString["strStatus"].ToString().Trim();
            }
            else
            {
                strStatus = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = GetData(strBranch, strParty, strArticle, strShadeFamily, strShadeCode, strCRFrom, strCRTo, strStatus);
            GetReport(dt);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rdoc = new ReportDocument();
            rdoc.Load(Server.MapPath(@"CRForSTReport.rpt"));
            rdoc.SetDataSource(dt);
            rdoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
            rdoc.SetParameterValue("CH_BRANCH_NAME", oUserLoginDetail.VC_BRANCHNAME);
            rdoc.SetParameterValue("USER_NAME", oUserLoginDetail.Username);
            rdoc.SetParameterValue("COMP_ADD", oUserLoginDetail.COMP_ADD);
            rdoc.SetParameterValue("DEVELOPER_COMP", oUserLoginDetail.DEVELOPER_COMP);
            rdoc.SetParameterValue("DEVELOPER_WEB", oUserLoginDetail.DEVELOPER_WEB);
            CrystalReportViewer1.ReportSource = rdoc;
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetData(string strBranch, string strParty, string strArticle, string strShadeFamily, string strShadeCode, string strCRFrom, string strCRTo, string strStatus)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetCRForSTQuery(oUserLoginDetail.COMP_CODE, strBranch, oUserLoginDetail.DT_STARTDATE.Year, strParty, strArticle, strShadeFamily, strShadeCode, strCRFrom, strCRTo, strStatus);
            return dt;
        }
        catch
        {
            throw;
        }
    }
}