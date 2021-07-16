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
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;

public partial class Module_Inventory_Reports_DEBIT_NOTE_REPORT : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Session["urLoginId"] != null)
        {
            ViewState["dtRate1"] = null;
            DataSet dt = GetData();
              GetReport(dt);
        }
    }

    private void GetReport(DataSet dt)
    {
        string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"DEBIT_NOTE_REPORT.RPT")); 
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;

    }
    private DataSet GetData()
    {
        try
        {
            int From = 0;
            int To = 0;
            string NOTE_TYPE = "";
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }
            if (Request.QueryString["NOTE_TYPE"] != null)
            {
                NOTE_TYPE = Request.QueryString["NOTE_TYPE"].ToString().Trim();
            }
            DataSet dt = SaitexBL.Interface.Method.TX_DEBIT_MST.GetDataForReport(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, From, To, NOTE_TYPE);
            dt.Tables[0].TableName = "DEBIT_NOTE";
            dt.Tables[1].TableName = "DEBIT_NOTE_TRN";
            if (dt.Tables[0].Columns["WORKS_ADDRESS"] == null)
            {
                dt.Tables[0].Columns.Add("DEVELOPER_COMP", typeof(string));
                dt.Tables[0].Columns.Add("DEVELOPER_WEB", typeof(string));
                dt.Tables[0].Columns.Add("WORKS_ADDRESS", typeof(string));

                
            }
            foreach (DataRow dr in dt.Tables[0].Rows)
            {

                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["WORKS_ADDRESS"] = oUserLoginDetail.WORKS_ADDRESS;
            }


            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }


   

   

}
