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
using Common;
using errorLog;
public partial class Module_Waste_Report_WasteIssueQueryReport : System.Web.UI.Page
{
    string BRANCH1 = string.Empty;
    string YEAR1 = string.Empty;
    string ITEMTYPE = string.Empty;
    string ITEMCAT = string.Empty;
    string PARTY = string.Empty;
    string DEPARTMENT1 = string.Empty;
    string ITEM1 = string.Empty;
    string TRNTYPE = string.Empty;
    DateTime StDate;
    DateTime EnDate;
    string DATERANGE = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Session["MaterialLedger"] != null)
            {
                DataTable dt = (DataTable)Session["MaterialLedger"];
                string BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
                string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
                string ITEM_CATE = dt.Rows[0]["ITEM_CATE"].ToString();
                string ITEM_TYPE = dt.Rows[0]["ITEM_TYPE"].ToString();
                string ITEM_CODE = dt.Rows[0]["ITEM_CODE"].ToString();
                StDate = DateTime.Parse(dt.Rows[0]["StDate"].ToString());
                EnDate = DateTime.Parse(dt.Rows[0]["EnDate"].ToString());
                string TRAN_TYPE = dt.Rows[0]["TRAN_TYPE"].ToString();
                string PARTY_CODE = dt.Rows[0]["PARTY_CODE"].ToString();
                BRANCH1 = dt.Rows[0]["BRANCH1"].ToString();
                YEAR1 = dt.Rows[0]["YEAR1"].ToString();
                ITEMTYPE = dt.Rows[0]["ITEMTYPE"].ToString();
                ITEMCAT = dt.Rows[0]["ITEMCAT"].ToString();
                PARTY = dt.Rows[0]["PARTY"].ToString();
                DEPARTMENT1 = dt.Rows[0]["DEPARTMENT1"].ToString();
                ITEM1 = dt.Rows[0]["ITEM1"].ToString();
                TRNTYPE = dt.Rows[0]["TRNTYPE"].ToString();
                DataTable dtrportdat = GetData(BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE,"");
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
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"WasteIssueQueryReport.rpt"));
            rDoc.SetDataSource(dt);
            rDoc.SetParameterValue("BRANCH1", BRANCH1);
            rDoc.SetParameterValue("USER",oUserLoginDetail.Username);
            rDoc.SetParameterValue("YEAR1", YEAR1);
            if (ITEMTYPE != string.Empty)
            {
                rDoc.SetParameterValue("ITEMTYPE", ITEMTYPE);
            }
            else
            {
                rDoc.SetParameterValue("ITEMTYPE", "ALL TYPE");
            }
            if (ITEMCAT != string.Empty)
            {

                rDoc.SetParameterValue("ITEMCAT", ITEMCAT);
            }
            else
            {
                rDoc.SetParameterValue("ITEMCAT", "ALL CATEGORY");
            }

            if (PARTY != string.Empty)
            {

                rDoc.SetParameterValue("PARTY", PARTY);
            }
            else
            {
                rDoc.SetParameterValue("PARTY", "ALL PARTY");
            }

            if (DEPARTMENT1 != string.Empty)
            {

                rDoc.SetParameterValue("DEPARTMENT1", DEPARTMENT1);
            }
            else
            {
                rDoc.SetParameterValue("DEPARTMENT1", "ALL DEPARTMENT");
            }

            if (ITEM1 != string.Empty)
            {

                rDoc.SetParameterValue("ITEM1", ITEM1);
            }
            else
            {
                rDoc.SetParameterValue("ITEM1", "ALL ITEM");
            }

            if (TRNTYPE != string.Empty)
            {
                rDoc.SetParameterValue("Trn_Type", TRNTYPE);
            }
            else
            {
                rDoc.SetParameterValue("Trn_Type", "ALL TYPE");
            }

            rDoc.SetParameterValue("DATERANGE", "From date :" + StDate.ToShortDateString() + " To :" + EnDate.ToShortDateString());

            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }

    private DataTable GetData(string BRANCH_CODE, string DEPT_CODE, string ITEM_CATE, string ITEM_TYPE, string ITEM_CODE, DateTime StDate, DateTime EnDate, string TRAN_TYPE, string PARTY_CODE,string YEAR)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            // DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.recevingmaterailissu(trn, item, from, to);
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.MaterialTransaction(BRANCH_CODE, DEPT_CODE, ITEM_CATE, ITEM_TYPE, ITEM_CODE, StDate, EnDate, TRAN_TYPE, PARTY_CODE, YEAR);

            return dt;

        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

    }
}
