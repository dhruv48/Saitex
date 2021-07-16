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

public partial class Module_GateEntry_Reports_Gate_Out_Print : System.Web.UI.Page
{
    string Query_String = string.Empty;
    string BRANCH = string.Empty;
    string TRNTYPE = string.Empty;
    string PRTY_CODE = string.Empty;
    string DOC_NO = string.Empty;
    int FromNo = 0;
    int ToNO = 0;
    string FR_DATE = string.Empty;
    string T_DATE = string.Empty;
    string ITEM_TYPE = string.Empty;
    string GATE_TYPE = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);
    }

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"GATE_OUT_PRINT.rpt"));
        
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    private DataTable GetData()
    {

        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];


        try
        {
            if (Request.QueryString["ITEM_TYPE"] != null && Request.QueryString["ITEM_TYPE"] != "")
            {
                ITEM_TYPE = Request.QueryString["ITEM_TYPE"].ToString();

            }
            if (Request.QueryString["FromNo"] != null && Request.QueryString["FromNo"] != "")
            {
                FromNo = int.Parse(Request.QueryString["FromNo"].ToString());
            }
            if (Request.QueryString["ToNO"] != null && Request.QueryString["ToNO"] != "")
            {
                ToNO = int.Parse(Request.QueryString["ToNO"].ToString());
            }


            if (Request.QueryString["GATE_TYPE"] != null && Request.QueryString["GATE_TYPE"] != "")
            {
                GATE_TYPE = Request.QueryString["GATE_TYPE"].ToString();
            }
            
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.GetYarnGateOutReport(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE.ToString(), ITEM_TYPE, FromNo, ToNO, GATE_TYPE, oUserLoginDetail.DT_STARTDATE.Year.ToString());

            dt.TableName = "GateOutReport";


            //if (!dt.Columns.Contains("COMP_NAME"))
            //    dt.Columns.Add("COMP_NAME", typeof(string));
            //if (!dt.Columns.Contains("COMP_ADD"))
            //    dt.Columns.Add("COMP_ADD", typeof(string));
            //if (!dt.Columns.Contains("BRANCH_NAME"))
            //    dt.Columns.Add("BRANCH_NAME", typeof(string));
            //if (!dt.Columns.Contains("USER_NAME"))
            //    dt.Columns.Add("USER_NAME", typeof(string));

            //SaitexDM.Common.DataModel.UserLoginDetail OUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["COMP_NAME"] = OUserLoginDetail.VC_COMPANYNAME;
            //    dr["COMP_ADD"] = OUserLoginDetail.COMP_ADD;
            //    dr["BRANCH_NAME"] = OUserLoginDetail.VC_BRANCHNAME;
            //    dr["USER_NAME"] = OUserLoginDetail.Username;

            //}
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
