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

public partial class Module_Waste_Reports_WasteStockAgeing : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string strBranch = string.Empty;
    private string strItemType = string.Empty;
    private string strCatCode = string.Empty;
    private string strDay1 = string.Empty;
    private string strDay2 = string.Empty;
    private string strDay3 = string.Empty;
    private int P_d1;
    private int P_D2;
    private int P_D3;

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

            if (Request.QueryString["strItemType"] != null && Request.QueryString["strItemType"] != "")
            {
                strItemType = Request.QueryString["strItemType"].ToString().Trim();
            }

            if (Request.QueryString["strCatCode"] != null && Request.QueryString["strCatCode"] != "")
            {
                strCatCode = Request.QueryString["strCatCode"].ToString().Trim();
            }

            if (Request.QueryString["strDay1"] != null && Request.QueryString["strDay1"] != "")
            {
                strDay1 = Request.QueryString["strDay1"].ToString().Trim();
                P_d1 = Convert.ToInt32(strDay1);
            }

            if (Request.QueryString["strDay2"] != null && Request.QueryString["strDay2"] != "")
            {
                strDay2 = Request.QueryString["strDay2"].ToString().Trim();
                P_D2 = Convert.ToInt32(strDay2);
            }

            if (Request.QueryString["strDay3"] != null && Request.QueryString["strDay3"] != "")
            {
                strDay3 = Request.QueryString["strDay3"].ToString().Trim();
                P_D3 = Convert.ToInt32(strDay3);
            }

            DataTable dt = new DataTable();
            dt = GetData(strBranch, strItemType, strCatCode, Convert.ToInt32(strDay1), Convert.ToInt32(strDay2), Convert.ToInt32(strDay3));
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
            rdoc.Load(Server.MapPath(@"WASTESTOCKAGING.rpt"));
            rdoc.SetDataSource(dt);
            rdoc.SetParameterValue("P_d1", P_d1);
            rdoc.SetParameterValue("P_D2", P_D2);
            rdoc.SetParameterValue("P_D3", P_D3);
            rdoc.SetParameterValue("COMP_NAME", oUserLoginDetail.VC_COMPANYNAME);
            rdoc.SetParameterValue("BRANCH_NAME", oUserLoginDetail.VC_BRANCHNAME);
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

    private DataTable GetData(string strBranch, string strItemType, string strCatCode, int iDay1, int iDay2, int iDay3)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_STOCK_DATA.GetItemStockAgingReport(oUserLoginDetail.COMP_CODE, strBranch, strItemType, strCatCode, iDay1, iDay2, iDay3);

            //if (dt.Columns["COMP_NAME"] == null)
            //    dt.Columns.Add("COMP_NAME", typeof(string));

            //if (dt.Columns["BRANCH_NAME"] == null)
            //    dt.Columns.Add("BRANCH_NAME", typeof(string));

            //if (dt.Columns["USER_NAME"] == null)
            //    dt.Columns.Add("USER_NAME", typeof(string));

            //if (dt.Columns["COMP_ADD"] == null)
            //    dt.Columns.Add("COMP_ADD", typeof(string));

            //if (dt.Columns["DEVELOPER_COMP"] == null)
            //    dt.Columns.Add("DEVELOPER_COMP", typeof(string));

            //if (dt.Columns["DEVELOPER_WEB"] == null)
            //    dt.Columns.Add("DEVELOPER_WEB", typeof(string));

            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            //    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            //    dr["USER_NAME"] = oUserLoginDetail.Username;
            //    dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
            //    dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
            //    dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
            //}
            return dt;
        }
        catch
        {
            throw;
        }
    }
}
