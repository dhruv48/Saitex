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


public partial class Module_Fabric_FabricSaleWork_Reports_Fabric_INDENT_REPORT_NEW : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataSet dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"Fabric_Indent_Report.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ese)
        {
            throw ese;
        }

    }
    private DataSet GetData()
    {
        try
        {
            int From = 0;
            int To = 0;

            if (Request.QueryString["From"] != null && Request.QueryString["From"] != "")
                From = int.Parse(Request.QueryString["From"].ToString().Trim());

            if (Request.QueryString["To"] != null && Request.QueryString["To"] != "")
                To = int.Parse(Request.QueryString["To"].ToString().Trim());

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataSet ds = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.GetDataForPrint(oUserLoginDetail, "GEN", From, To);

            ds.Tables[0].Columns.Add("VC_USERNAME", typeof(string));
            ds.Tables[0].Columns.Add("TotalAmount", typeof(string));
            double TotalAmount = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TotalAmount = TotalAmount + double.Parse(dr["iValue"].ToString());
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["TotalAmount"] = TotalAmount.ToString();
                dr["VC_USERNAME"] = oUserLoginDetail.Username;
            }
            if (ds.Tables[1].Columns["COMP_NAME"] == null)
                ds.Tables[1].Columns.Add("COMP_NAME", typeof(string));

            if (ds.Tables[1].Columns["BRANCH_NAME"] == null)
                ds.Tables[1].Columns.Add("BRANCH_NAME", typeof(string));

            if (ds.Tables[1].Columns["USER_NAME"] == null)
                ds.Tables[1].Columns.Add("USER_NAME", typeof(string));
            if (ds.Tables[1].Columns["COMP_ADD"] == null)
                ds.Tables[1].Columns.Add("COMP_ADD", typeof(string));

            if (ds.Tables[1].Columns["DEVELOPER_COMP"] == null)
                ds.Tables[1].Columns.Add("DEVELOPER_COMP", typeof(string));
            if (ds.Tables[1].Columns["DEVELOPER_WEB"] == null)
                ds.Tables[1].Columns.Add("DEVELOPER_WEB", typeof(string));

            //    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                ds.AcceptChanges();
            }

            //return dt;
            return ds;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    } 
}
