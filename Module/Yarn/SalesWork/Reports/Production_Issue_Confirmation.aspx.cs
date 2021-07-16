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
public partial class Module_Yarn_SalesWork_Reports_Production_Issue_Confirmation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = GetData();
        GetReport(ds);
    }
    private void GetReport(DataSet ds)
    {
        
            ReportDocument rDoc = new ReportDocument();
            string ReportPath = string.Empty;


            ReportPath = Server.MapPath(@"Production_Issue_Confirmation.rpt");
           
            rDoc.Load(ReportPath);
            rDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rDoc;
        
    }
    private DataSet GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            string COMP_CODE = string.Empty;
            string BRANCH_CODE = string.Empty;
            string YEAR = string.Empty;
            string PRODUCT_TYPE = string.Empty;
            string BRANCH_NAME = string.Empty;
            string PLANNING_DATE = string.Empty;
            if (Request.QueryString["COMP_CODE"] != null)
            {
                COMP_CODE = Request.QueryString["COMP_CODE"].ToString().Trim();
            }
            if (Request.QueryString["BRANCH_CODE"] != null)
            {
                BRANCH_CODE = Request.QueryString["BRANCH_CODE"].ToString().Trim();
            }
            if (Request.QueryString["YEAR"] != null)
            {
                YEAR = Request.QueryString["YEAR"].ToString().Trim();
            }

            if (Request.QueryString["PRODUCT_TYPE"] != null)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString().Trim();
            }
            if (Request.QueryString["PLANNING_DATE"] != null)
            {
                PLANNING_DATE = Request.QueryString["PLANNING_DATE"].ToString().Trim();
            }

       //  PRODUCT_TYPE=   "YARN DYEING";
            if (Request.QueryString["BRANCH_NAME"] != null)
            {
                BRANCH_NAME = Request.QueryString["BRANCH_NAME"].ToString().Trim();
            }

            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetAllProdcutionIssueByDepartement1(COMP_CODE, BRANCH_CODE, YEAR, PRODUCT_TYPE, BRANCH_NAME, "", "", "", "", "", "", "", PLANNING_DATE);
            
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "PRODUCT_YARN_ISSUE";
            return ds;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
