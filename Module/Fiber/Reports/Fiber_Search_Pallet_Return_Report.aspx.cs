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

public partial class Module_Fiber_Reports_Fiber_Search_Pallet_Return_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string ChallanNO = string.Empty;
        string ChallanDate = string.Empty;
        string PartyCode = string.Empty;
        string Merge = string.Empty;
        string MRNNO = string.Empty;
        string MergeNo = string.Empty;
        string PalletCode = string.Empty;
        string NoOfPallet = string.Empty;
        if (Request.QueryString["ChallanNO"] != null)
        {
            ChallanNO = Request.QueryString["ChallanNO"].ToString();
        }
        if (Request.QueryString["ChallanDate"] != null)
        {
            ChallanDate = Request.QueryString["ChallanDate"].ToString();
        }
        if (Request.QueryString["PartyCode"] != null)
        {
            PartyCode = Request.QueryString["PartyCode"].ToString();
        }
        if (Request.QueryString["Merge"] != null)
        {
            Merge = Request.QueryString["Merge"].ToString();
        }
        if (Request.QueryString["MRNNO"] != null)
        {
            MRNNO = Request.QueryString["MRNNO"].ToString();
        }
        if (Request.QueryString["MergeNo"] != null)
        {
            MergeNo = Request.QueryString["MergeNo"].ToString();
        }
        if (Request.QueryString["PalletCode"] != null)
        {
            PalletCode = Request.QueryString["PalletCode"].ToString();
        }
        if (Request.QueryString["NoOfPallet"] != null)
        {
            NoOfPallet = Request.QueryString["NoOfPallet"].ToString();
        }



        try
        {


            DataTable dtrportdat = GetData(ChallanNO, ChallanDate, PartyCode, Merge, MRNNO, MergeNo, PalletCode, NoOfPallet);
            GetReport(dtrportdat);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }



    private DataTable GetData(string ChallanNO, string ChallanDate, string PartyCode, string Merge, string MRNNO, string MergeNo, string PalletCode, string NoOfPallet)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FIBER_MST oTX_FIBER_MST = new SaitexDM.Common.DataModel.TX_FIBER_MST();
            DataTable dt = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetUserQuery(ChallanNO, ChallanDate, PartyCode, Merge, MRNNO, MergeNo, PalletCode, NoOfPallet);
            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));
            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));


            if (dt.Columns["DEVELOPER_WEB"] == null)
                dt.Columns.Add("DEVELOPER_WEB", typeof(string));
            if (dt.Columns["DEVELOPER_COMP"] == null)
                dt.Columns.Add("DEVELOPER_COMP", typeof(string));


            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dt.AcceptChanges();

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

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Fiber_Search_Pallet_Return_Report.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
}
