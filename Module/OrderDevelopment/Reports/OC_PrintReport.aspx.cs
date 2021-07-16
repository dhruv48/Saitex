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
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
public partial class Module_OrderDevelopment_Reports_OC_PrintReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataSet ORDER_DATASET = GetData();
            GetReport(ORDER_DATASET);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in generating report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataSet ORDER_DATASET)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"OrderCapture.rpt"));
            rDoc.SetDataSource(ORDER_DATASET);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch
        {
            throw;
        }
    }
    private DataSet GetData()
    {

        try
        {
            int From = 0;
            int To = 0;
            string BUSINESS_TYPE = string.Empty;
            string PRODUCT_TYPE = string.Empty;
            string ORDER_CAT = string.Empty;
            string ORDER_TYPE = string.Empty;
            string ORDER_PREFIX = string.Empty;

            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }

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

            if (Request.QueryString["ORDER_PREFIX"] != null && Request.QueryString["ORDER_PREFIX"] != "")
            {
                ORDER_PREFIX = Request.QueryString["ORDER_PREFIX"].ToString();
            }

            SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();

            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
            oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;

            DataSet ORDER_DATASET = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetDataForReportOc(oOD_CAPTURE_MST);
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

            ORDER_DATASET.Tables[0].AcceptChanges();

            return ORDER_DATASET;

        }
        catch
        {
            throw;
        }
    }
}
