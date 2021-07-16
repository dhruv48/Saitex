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

public partial class Module_OrderDevelopment_Reports_OrderCapture4YarnDyeing : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataSet dt = GetData();
            GetReport(dt);
        }
        catch (Exception exe)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(exe, @"Problem in generating report.\r\nSee error log for detail."));
        }

    }
    public DataSet GetData()
    {
        string PRODUCT_TYPE = string.Empty;
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"] != "")
        {
            PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
        }
        DataSet ORDER_DATASET = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForOrderCapture(PRODUCT_TYPE,oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE);
        ORDER_DATASET.Tables[0].TableName = "OrderCapture";
        ORDER_DATASET.Tables[1].TableName = "OrderCaptureAdjust";
        if (ORDER_DATASET.Tables[0].Columns["COMP_NAME"] == null)
            ORDER_DATASET.Tables[0].Columns.Add("COMP_NAME", typeof(string));

        if (ORDER_DATASET.Tables[0].Columns["BRANCH_NAME"] == null)
            ORDER_DATASET.Tables[0].Columns.Add("BRANCH_NAME", typeof(string));

        if (ORDER_DATASET.Tables[0].Columns["USER_NAME"] == null)
            ORDER_DATASET.Tables[0].Columns.Add("USER_NAME", typeof(string));
        if (ORDER_DATASET.Tables[0].Columns["DEVELOPER_WEB"] == null)
            ORDER_DATASET.Tables[0].Columns.Add("DEVELOPER_WEB", typeof(string));
        if (ORDER_DATASET.Tables[0].Columns["DEVELOPER_COMP"] == null)
            ORDER_DATASET.Tables[0].Columns.Add("DEVELOPER_COMP", typeof(string));


        foreach (DataRow dr in ORDER_DATASET.Tables[0].Rows)
        {
            dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            dr["USER_NAME"] = oUserLoginDetail.Username;
             dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
            ORDER_DATASET.AcceptChanges();
        }

        return ORDER_DATASET;
          
    }
    public void GetReport(DataSet dt)
    {
        ReportDocument Rdoc = new ReportDocument();
        Rdoc.Load(Server.MapPath(@"OrderCapture.rpt"));
        Rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = Rdoc;
    }
}
