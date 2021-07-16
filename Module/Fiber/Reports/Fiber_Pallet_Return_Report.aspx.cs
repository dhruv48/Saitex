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
public partial class Module_Fiber_Reports_Fiber_Pallet_Return_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["dtRate1"] = null;
        ViewState["dtRateMST1"] = null;
        DataTable dt = GetData();
        GetReport(dt);
    }

    private void GetReport(DataTable dt)
    {
        if (Request.QueryString["TRN_TYPE"] != null)
        {
            ReportDocument rDoc = new ReportDocument();
            string ReportPath = string.Empty;
            if (Request.QueryString["TRN_TYPE"].ToString() == "IPT01")
            {
                ReportPath = Server.MapPath(@"Fiber_Pallet_Return_Report.rpt");
            }
          
            rDoc.Load(ReportPath);
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
    }

    private DataTable GetData()
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            int From = 0;
            int To = 0;
            string TRN_TYPE = "";

            if (Request.QueryString["FromNo"] != null)
            {
                From = int.Parse(Request.QueryString["FromNo"].ToString().Trim());
            }
            if (Request.QueryString["ToNo"] != null)
            {
                To = int.Parse(Request.QueryString["ToNo"].ToString().Trim());
            }
            if (Request.QueryString["TRN_TYPE"] != null)
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString().Trim();
            }
            DataTable dt = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetDataForReportForpalletreturn(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, From, To, TRN_TYPE);
            
           

            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));
            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dt.AcceptChanges();
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
