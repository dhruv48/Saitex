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

public partial class Module_Fabric_FabricSaleWork_Reports_FabricMasterReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["fabricreportdt"] != null)
        {
            DataTable dt = (DataTable)Session["fabricreportdt"];
            string dept = "";
            string FABR_CODE = dt.Rows[0]["FABR_CODE"].ToString();
           // string dept = dt.Rows[0]["DEPT_CODE"].ToString();
            string FABR_TYPE = dt.Rows[0]["FABR_TYPE"].ToString();
            string branchcode = dt.Rows[0]["BRANCH_CODE"].ToString();
            DataSet dtrportdat = GetData(FABR_CODE, dept, FABR_TYPE, branchcode);
            GetReport(dtrportdat);
        }

    }


    private void GetReport(DataSet dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"FabricMasterReportNew.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }


    private DataSet GetData(string FABR_CODE, string dept, string FABR_TYPE, string branchcode)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
           // SaitexDM.Common.DataModel.YRN_MST oYRN_MST = new SaitexDM.Common.DataModel.YRN_MST();
            DataSet dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetDataForReport(FABR_CODE, dept, FABR_TYPE, branchcode, oUserLoginDetail.DT_STARTDATE.Year);
            dt.Tables[0].TableName = "DataTable1";
            dt.Tables[1].TableName = "DesignShade";

            if (dt.Tables[0].Columns["COMP_NAME"] == null)
                dt.Tables[0].Columns.Add("COMP_NAME", typeof(string));

            if (dt.Tables[0].Columns["BRANCH_NAME"] == null)
                dt.Tables[0].Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Tables[0].Columns["USER_NAME"] == null)
                dt.Tables[0].Columns.Add("USER_NAME", typeof(string));
            if (dt.Tables[0].Columns["COMP_ADD"] == null)
                dt.Tables[0].Columns.Add("COMP_ADD", typeof(string));


            if (dt.Tables[0].Columns["DEVELOPER_WEB"] == null)
                dt.Tables[0].Columns.Add("DEVELOPER_WEB", typeof(string));
            if (dt.Tables[0].Columns["DEVELOPER_COMP"] == null)
                dt.Tables[0].Columns.Add("DEVELOPER_COMP", typeof(string));


            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;

                dt.AcceptChanges();
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
