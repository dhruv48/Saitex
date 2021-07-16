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
using CrystalDecisions.Shared;

using Common;

public partial class Module_Inventory_Reports_StockDetail : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string yr = string.Empty;
    string branch_code = string.Empty;
    string item_code = string.Empty;
    string item_type = string.Empty;
    string catcode = string.Empty;
    string sdate = string.Empty;
    string edate = string.Empty;
    string location = string.Empty;
    string store = string.Empty;
    int balance = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (Request.QueryString["TRANS_YEAR"] != null && Request.QueryString["TRANS_YEAR"] != "")
            {
                yr = Request.QueryString["TRANS_YEAR"].ToString().Trim();
            }

            if (Request.QueryString["BRANCH_CODE"] != null && Request.QueryString["BRANCH_CODE"] != "")
            {
                branch_code = Request.QueryString["BRANCH_CODE"].ToString().Trim();
            }

            if (Request.QueryString["ITEM_CODE"] != null && Request.QueryString["ITEM_CODE"] != "")
            {
                item_code = Request.QueryString["ITEM_CODE"].ToString().Trim();
            }

            if (Request.QueryString["ITEM_TYPE"] != null && Request.QueryString["ITEM_TYPE"] != "")
            {
                item_type = Request.QueryString["ITEM_TYPE"].ToString().Trim();
            }

            if (Request.QueryString["CATCODE"] != null && Request.QueryString["CATCODE"] != "")
            {
                catcode = Request.QueryString["CATCODE"].ToString().Trim();
            }

            if (Request.QueryString["SDATE"] != null && Request.QueryString["SDATE"] != "")
            {
                sdate = Request.QueryString["SDATE"].ToString().Trim();
            }

            if (Request.QueryString["EDATE"] != null && Request.QueryString["EDATE"] != "")
            {
                edate = Request.QueryString["EDATE"].ToString().Trim();
            }

            if (Request.QueryString["balance"] != null && Request.QueryString["balance"] != "")
            {
                balance = int.Parse(Request.QueryString["balance"].ToString().Trim());
            }
            if (yr != "")
            {
                yr = " and A1.year='" + yr + "'";
            }
            if (branch_code != "")
            {
                branch_code = " and A1.branch_code='" + branch_code + "'";
            }
            if (item_code != "")
            {
                item_code = " and A1.item_code='" + item_code + "'";
            }
            if (catcode != "")
            {
                catcode = " and A2.cat_code='" + catcode + "'";
            }
            if (item_type != "")
            {
                item_type = " and A2.item_type='" + item_type + "'";
            }
            if (balance > 0)
            {
                balance = 1;
            }
            else
            {
                balance = 0;
            }
            if (Request.QueryString["LOCATION"] != null && Request.QueryString["LOCATION"] != "")
            {
                location = " AND A1.LOCATION='" + Request.QueryString["LOCATION"].ToString()+"'";
            }
            if (Request.QueryString["STORE"] != null && Request.QueryString["STORE"] != "")
            {
                store = " AND A1.STORE='" + Request.QueryString["STORE"].ToString()+"'";
            }
            DataSet ds = GetStockDetail(branch_code, yr, item_code, item_type, catcode, sdate, edate, balance,location,store);
            GetReport(ds);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private DataSet GetStockDetail(string branch, string year, string itemcode, string item_type, string catcode, string sdate, string edate, int balance,string location,string store)
    {
        try
        {
            DataTable dtNew = null;
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_STOCK_DATA.Load_Stock_Data(branch, year, itemcode, item_type, catcode, sdate, edate,location ,store);
            DataView dv = new DataView(dt);
            if (balance == 1)
            {
                dv.RowFilter = "CLOSING_QTY > 0";
            }
            dtNew = dv.ToTable();

            DataTable dt1 = new DataTable();

            dt1.Columns.Add("COMP_CODE", typeof(string));
            dt1.Columns.Add("COMP_NAME", typeof(String));
            dt1.Columns.Add("BRANCH_CODE", typeof(string));
            dt1.Columns.Add("BRANCH_NAME", typeof(string));
            dt1.Columns.Add("USERNAME", typeof(string));
            dt1.Columns.Add("SDATE", typeof(string));
            dt1.Columns.Add("EDATE", typeof(string));

            DataRow dr = dt1.NewRow();
            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
            dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
            dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            dr["USERNAME"] = oUserLoginDetail.Username;
            dr["SDATE"] = sdate;
            dr["EDATE"] = edate;
            dt1.Rows.Add(dr);

            DataSet ds = new DataSet();
            ds.Tables.Add(dtNew);
            ds.Tables.Add(dt1);
            ds.Tables[0].TableName = "StockDetails";
            ds.Tables[1].TableName = "ReportHeader";
            return ds;
        }
        catch
        {
            throw;
        }
    }

    private void GetReport(DataSet ds)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"StockDetail.rpt"));
            rDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch
        {
            throw;
        }
    }
}
