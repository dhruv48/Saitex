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
using CrystalDecisions.Shared;

public partial class Module_Fabric_FabricSaleWork_Reports_Waste_mgt : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.TX_WASTE_MGT_MST oTX_WASTE_MGT_MST = new SaitexDM.Common.DataModel.TX_WASTE_MGT_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string COMP_CODE = string.Empty;
    string BRANCH_CODE = string.Empty;
   
    string  PRODUCT_TYPE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        COMP_CODE = Request.QueryString["COMP_CODE"];
        BRANCH_CODE = Request.QueryString["BRANCH_CODE"];
        PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"];  

        DataSet ds = GetPendIndentDetails(COMP_CODE, BRANCH_CODE);
        GetReport(ds);
    }

    private DataSet GetPendIndentDetails(string COMP_CODE, string BRANCH_CODE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MGT_MST.SelectWASTE(COMP_CODE, BRANCH_CODE);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("COMP_CODE", typeof(String));
            dt1.Columns.Add("COMP_NAME", typeof(String));
            dt1.Columns.Add("BRANCH_CODE", typeof(String));
            dt1.Columns.Add("BRANCH_NAME", typeof(String));
            dt1.Columns.Add("USERNAME", typeof(String));
            DataRow dr = dt1.NewRow();
            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
            dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
            dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            dr["USERNAME"] = oUserLoginDetail.Username;
            dt1.Rows.Add(dr);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            ds.Tables[0].TableName = "WASTE_DTL";
            ds.Tables[1].TableName = "REPORT_HEADER";
            return ds;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
    private void GetReport(DataSet ds)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Wastemgt.rpt"));
        rDoc.SetDataSource(ds);
        CrystalReportViewer1.ReportSource = rDoc;
    }
}
