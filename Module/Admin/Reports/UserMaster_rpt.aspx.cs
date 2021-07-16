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
using System.Data.OracleClient;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;


public partial class Admin_UserMaster_rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = GetData();
        GetReport(dt);
    }
    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"UserMaster.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
       
    private DataTable GetData()
    { 
        DataTable dt ;
        try
        {
            dt = SaitexBL.Interface.Method.CM_USER_MST.PrintReport();

            if (dt.Columns["COMP_NAME"] == null)
               dt.Columns.Add("COMP_NAME", typeof(string));

           if (dt.Columns["BRANCH_NAME"] == null)
               dt.Columns.Add("BRANCH_NAME", typeof(string));

          
           
            if (dt.Columns["COMP_ADD"] == null)
               dt.Columns.Add("COMP_ADD", typeof(string));

         
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
           foreach (DataRow dr in dt.Rows)
           {
               dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
               dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
               
               dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
            
               dt.AcceptChanges();
           }


           return dt;
        }
        catch (Exception ex)
        {     
            throw ex;
        }
    
    }


}



