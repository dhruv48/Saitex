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
public partial class Module_PlanningAndScheduling_Reports_LotPlanReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = DataGet();
        GetDatareport(dt);

    }
    public void GetDatareport(DataSet dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"LotPlaning.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
    public DataSet DataGet()
    {
        try
        {
            string BUSINESS_TYPE = string.Empty;
            string PRODUCT_TYPE = string.Empty;
            string ORDER_CAT = string.Empty;
            string ORDER_TYPE = string.Empty;
            string ORDER_PREFIX = string.Empty;
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
                TITLE = "Order Capture For Yarn Spining";
            }
            else if (PRODUCT_TYPE == "SEWING THREAD")
            {
                TITLE = "Order Capture For Sewing Thread";
            }
            else if (PRODUCT_TYPE == "YARN DYEING")
            {
                TITLE = "Order Capture For Yarn Dyeing";
            }
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.OD_CAPTURE_MST oOD_CAPTURE_MST = new SaitexDM.Common.DataModel.OD_CAPTURE_MST();
            oOD_CAPTURE_MST.BUSINESS_TYPE = BUSINESS_TYPE;
            oOD_CAPTURE_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_CAPTURE_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_CAPTURE_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oOD_CAPTURE_MST.ORDER_TYPE = ORDER_TYPE;
            oOD_CAPTURE_MST.ORDER_CAT = ORDER_CAT;
            DataSet Lot_Plan = SaitexBL.Interface.Method.OD_CAPT_MST.getData4LotPlanning(oOD_CAPTURE_MST);
            Lot_Plan.Tables[0].TableName = "OD_CAPT_TRN_MAIN";
            Lot_Plan.Tables[1].TableName = "OD_CAPT_TRN_BOM";
            Lot_Plan.Tables[2].TableName = "OD_CAPT_TRN_LOT";
            if (Lot_Plan.Tables[0].Columns["PRINT_USER"] == null)
                Lot_Plan.Tables[0].Columns.Add("PRINT_USER", typeof(string));
            if (Lot_Plan.Tables[0].Columns["TITLE"] == null)
                Lot_Plan.Tables[0].Columns.Add("TITLE", typeof(string));

            if (Lot_Plan.Tables[0].Columns["DEV_COMP"] == null)
                Lot_Plan.Tables[0].Columns.Add("DEV_COMP", typeof(string));
            if (Lot_Plan.Tables[0].Columns["DEV_INFO"] == null)
                Lot_Plan.Tables[0].Columns.Add("DEV_INFO", typeof(string));
            if (Lot_Plan.Tables[0].Columns["TUSER_NAME"] == null)
                Lot_Plan.Tables[0].Columns.Add("TUSER_NAME", typeof(string));
            foreach (DataRow dr in Lot_Plan.Tables[0].Rows)
            {
                dr["PRINT_USER"] = oUserLoginDetail.Username;
                dr["TITLE"] = TITLE;
                dr["DEV_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEV_INFO"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["TUSER_NAME"] = oUserLoginDetail.Username;
            }
            Lot_Plan.Tables[0].AcceptChanges();

            return Lot_Plan;
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }
}
