using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

public partial class Module_PlanningAndScheduling_Queries_OrderMovement : System.Web.UI.Page
{
    string BRANCH_CODE = string.Empty;
 //   string YEAR = string.Empty;
    string DEPT_CODE = string.Empty;
    string BUSINESS_TYPE = string.Empty;
    string PRODUCT_TYPE = string.Empty;
    DateTime StDate;
    DateTime EnDate;
    string ORDER_NO = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Request.QueryString["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = Request.QueryString["BRANCH_CODE"];
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }

            //if (Request.QueryString["YEAR"] != null)
            //{
            //    YEAR = Request.QueryString["YEAR"];
            //}
            //else
            //{
            //    YEAR = string.Empty;
            //}
            if (Request.QueryString["DEPT_CODE"] != null)
            {
                DEPT_CODE = Request.QueryString["DEPT_CODE"];
            }
            else
            {
                DEPT_CODE = string.Empty;
            }

            if (Request.QueryString["BUSINESS_TYPE"] != null)
            {
                BUSINESS_TYPE = Request.QueryString["BUSINESS_TYPE"];
            }
            else
            {
                BUSINESS_TYPE = string.Empty;
            }
            if (Request.QueryString["PRODUCT_TYPE"] != null)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"];
            }
            else
            {
                PRODUCT_TYPE = string.Empty;
            }

            if (Request.QueryString["StDate"] != null)
            {
                StDate = DateTime.Parse(Request.QueryString["StDate"].ToString());
            }
            else
            {
                StDate = oUserLoginDetail.DT_STARTDATE;
            }

            if (Request.QueryString["EnDate"] != null)
            {
                EnDate = DateTime.Parse(Request.QueryString["EnDate"].ToString());
            }

            if (Request.QueryString["ORDER_NO"] != null)
            {
                ORDER_NO = Request.QueryString["ORDER_NO"];
            }
            else
            {
                ORDER_NO = string.Empty;
            }

            DataTable dtRpt = GetData(BRANCH_CODE, DEPT_CODE, BUSINESS_TYPE, PRODUCT_TYPE, StDate, EnDate, ORDER_NO);
            GetReport(dtRpt);
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void GetReport(DataTable dtRpt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"OrderMovement.rpt"));
            rDoc.SetDataSource(dtRpt);
            rDoc.SetParameterValue("BRANCH", BRANCH_CODE);
            if (PRODUCT_TYPE != string.Empty)
            {
                rDoc.SetParameterValue("PRODUCT", PRODUCT_TYPE);
            }
            else
            {
                rDoc.SetParameterValue("PRODUCT", "All Type");
            }

            if (ORDER_NO != string.Empty)
            {
                rDoc.SetParameterValue("ORDER", ORDER_NO);
            }
            else
            {
                rDoc.SetParameterValue("ORDER", "All Order");
            }

            if (DEPT_CODE != string.Empty)
            {
                rDoc.SetParameterValue("DEPARTMENT", DEPT_CODE);
            }

            else
            {
                rDoc.SetParameterValue("DEPARTMENT", "All Department");
            }

            if (BUSINESS_TYPE != string.Empty)
            {
                rDoc.SetParameterValue("BUSINESS", BUSINESS_TYPE);
            }

            else
            {
                rDoc.SetParameterValue("BUSINESS", "All Type");
            }

            rDoc.SetParameterValue("DATERANGE", "From date :" + StDate.ToShortDateString() + " To :" + EnDate.ToShortDateString());          
          
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch
        {
            
            throw;
        }
    }

    private DataTable GetData(string BRANCH_CODE, string DEPT_CODE, string BUSINESS_TYPE, string PRODUCT_TYPE, DateTime StDate, DateTime EnDate, string ORDER_NO)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.GetRptData(BRANCH_CODE, DEPT_CODE, BUSINESS_TYPE, StDate, EnDate, ORDER_NO);
            return dt;
        }
        catch
        {
            
            throw;
        }
    }
}
