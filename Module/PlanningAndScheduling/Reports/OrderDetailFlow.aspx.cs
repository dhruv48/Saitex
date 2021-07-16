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

public partial class Module_PlanningAndScheduling_Reports_OrderDetailFlow : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = DataGet();
        GetDatareport(dt);
    }

    private void GetDatareport(DataSet dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"OrderFlowDetailPRT.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }

    private DataSet DataGet()
    {
        try
        {
            string BUSINESS_TYPE = string.Empty;
            string PRODUCT_TYPE = string.Empty;
            string ORDER_CAT = string.Empty;
            string ORDER_TYPE = string.Empty;
            string TITLE = string.Empty;

            if (Request.QueryString["BUSINESS_TYPE"] != null && Request.QueryString["BUSINESS_TYPE"] != "")
            {
                BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"].ToString();
            }

            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"] != "")
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }

            if (Request.QueryString["ORDER_CAT"] != null && Request.QueryString["ORDER_CAT"] != "")
            {
                ORDER_CAT = Request.QueryString["ORDER_CAT"].ToString();
            }

            if (Request.QueryString["ORDER_TYPE"] != null && Request.QueryString["ORDER_TYPE"] != "")
            {
                ORDER_TYPE = Request.QueryString["ORDER_TYPE"].ToString();
            }

            if (PRODUCT_TYPE == "YARN SPINING")
            {
                TITLE = "Order Flow Detail For Yarn Spining.";
            }
            else if (PRODUCT_TYPE == "SEWING THREAD")
            {
                TITLE = "Order Flow Detail For Sewing Thread";
            }

            else if (PRODUCT_TYPE == "YARN DYEING")
            {
                TITLE = "Order Flow Detail For Yarn Dyeing";
            }

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
            oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
            DataSet OrderFlow_Detail = SaitexBL.Interface.Method.OD_CAPT_MST.GetDataForOrderFlowDetail(oOD_CAPTURE_MST);
            OrderFlow_Detail.Tables[0].TableName = "OrderCapture";
            OrderFlow_Detail.Tables[1].TableName = "OrderCaptureBOM";
            OrderFlow_Detail.Tables[2].TableName = "OrderCaptureCOST";
            OrderFlow_Detail.Tables[3].TableName = "OrderProcessRoute";
            OrderFlow_Detail.Tables[4].TableName = "BatchCardEntry";
            OrderFlow_Detail.Tables[5].TableName = "SellBillEntry";
            if (OrderFlow_Detail.Tables[0].Columns["PRINT_USER"] == null)
                OrderFlow_Detail.Tables[0].Columns.Add("PRINT_USER", typeof(string));
            if (OrderFlow_Detail.Tables[0].Columns["TITLE"] == null)
                OrderFlow_Detail.Tables[0].Columns.Add("TITLE", typeof(string));

            if (OrderFlow_Detail.Tables[0].Columns["DEV_COMP"] == null)
                OrderFlow_Detail.Tables[0].Columns.Add("DEV_COMP", typeof(string));
            if (OrderFlow_Detail.Tables[0].Columns["DEV_INFO"] == null)
                OrderFlow_Detail.Tables[0].Columns.Add("DEV_INFO", typeof(string));
            if (OrderFlow_Detail.Tables[0].Columns["TUSER_NAME"] == null)
                OrderFlow_Detail.Tables[0].Columns.Add("TUSER_NAME", typeof(string));
            foreach (DataRow dr in OrderFlow_Detail.Tables[0].Rows)
            {
                dr["PRINT_USER"] = oUserLoginDetail.Username;
                dr["TITLE"] = TITLE;
                dr["DEV_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEV_INFO"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["TUSER_NAME"] = oUserLoginDetail.Username;
            }
            OrderFlow_Detail.Tables[0].AcceptChanges();

            return OrderFlow_Detail;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
