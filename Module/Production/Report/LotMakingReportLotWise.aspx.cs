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

public partial class Module_Production_Reports_LotMakingReportLotWise : System.Web.UI.Page
{
    string COMP_CODE = string.Empty;
    string Year = string.Empty;
    string LotNo = string.Empty;
    string Branch = string.Empty;
    string MachineName = string.Empty;
    string PoyDenier = string.Empty;
    string LotType = string.Empty;
    string FinishDenier = string.Empty;
    string Purpose = string.Empty;
    string STATUS = string.Empty;
    string Query_String = string.Empty;
    string where_Query = string.Empty;

    DateTime StDate;
    DateTime EnDate;
    DateTime Sdate = System.DateTime.Now;
    DateTime Edate = System.DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);
    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"LotMakingReportLotWise1.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        try
        {
            if (Request.QueryString["CH_BRANCHCODE"] != null && Request.QueryString["CH_BRANCHCODE"] != "")
            {
                Branch = Request.QueryString["CH_BRANCHCODE"].ToString();
                where_Query += " and BRANCH_CODE='" + Branch + "'";
            }
            
            if (Request.QueryString["LOT_NO"] != null && Request.QueryString["LOT_NO"] != "")
            {
                LotNo = Request.QueryString["LOT_NO"].ToString();
                where_Query += " and LOT_NO='" + LotNo + "'";
            }
            if (Request.QueryString["MACHINE_NAME"] != null && Request.QueryString["MACHINE_NAME"] != "")
            {
                MachineName = Request.QueryString["MACHINE_NAME"].ToString();
                where_Query += " and MACHINE_NAME='" + MachineName + "'";
            }
            if (Request.QueryString["PURPOSE"] != null && Request.QueryString["PURPOSE"] != "")
            {
                Purpose = Request.QueryString["PURPOSE"].ToString();
                where_Query += " and PURPOSE='" + Purpose + "'";
            }
            if (Request.QueryString["CONF_FLAG"] != null && Request.QueryString["CONF_FLAG"] != "")
            {
                STATUS = Request.QueryString["CONF_FLAG"].ToString();
                where_Query += " and CONF_FLAG='" + STATUS + "'";
            }
            if (Request.QueryString["POY"] != null && Request.QueryString["POY"] != "")
            {
                PoyDenier = Request.QueryString["POY"].ToString();
                where_Query += " and POY='" + PoyDenier + "'";
            }
            if (Request.QueryString["FINISHED_DENIER"] != null && Request.QueryString["FINISHED_DENIER"] != "")
            {
                FinishDenier = Request.QueryString["FINISHED_DENIER"].ToString();
                where_Query += " and FINISHED_DENIER='" + FinishDenier + "'";
            }
            if (Request.QueryString[""] != null && Request.QueryString["CONF_FLAG"] != "")
            {
                STATUS = Request.QueryString["CONF_FLAG"].ToString();
                where_Query += " and CONF_FLAG='" + STATUS + "'";
            }

            DataTable dt = SaitexBL.Interface.Method.YRN_LOT_MAKING.GetLotMakingQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, LotNo, MachineName, FinishDenier, PoyDenier, LotType, Purpose, STATUS, StDate, EnDate);
            dt.TableName = "LotMakingForm1";


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