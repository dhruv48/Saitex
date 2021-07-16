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
public partial class Module_Yarn_SalesWork_Reports_YarnMasterReport : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["yarnreportdt"] != null)
        {
            DataTable dt = (DataTable)Session["yarnreportdt"];

            string yarncode = dt.Rows[0]["YARN_CODE"].ToString();
            string dept = dt.Rows[0]["DEPT_CODE"].ToString();
            string yarncat = dt.Rows[0]["YARN_CAT"].ToString();
            string branchcode = dt.Rows[0]["BRANCH_CODE"].ToString();
            string RGB=dt.Rows[0]["RGB"].ToString();
            string YARNSHADE = dt.Rows[0]["SHADE"].ToString();
            //string TARIFFHEADING = dt.Rows[0]["TARIFF_HEADING"].ToString();
            string COATING = dt.Rows[0]["COATING"].ToString();
            string ENDUSE = dt.Rows[0]["ENDUSE"].ToString();
            string LOCATION = dt.Rows[0]["LOCATION"].ToString();
            string STORE = dt.Rows[0]["STORE"].ToString();
            string ISEXCISABLE = dt.Rows[0]["IS_EXCISABLE"].ToString();
            DataTable dtrportdat = GetData('Y', yarncode, dept, yarncat, branchcode, RGB, YARNSHADE, COATING, ENDUSE, LOCATION, STORE, ISEXCISABLE);
           GetReport(dtrportdat);
        }
           
   }
        

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Yarn.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }


    private DataTable GetData(char ch_view, string yarncode, string dept, string yarncat, string branchcode, string RGB, string YARNSHADE, string COATING, string ENDUSE, string LOCATION, string STORE, string ISEXCISABLE) 
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.YRN_MST oYRN_MST = new SaitexDM.Common.DataModel.YRN_MST();
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetDataForReport(yarncode, dept, yarncat, branchcode, oUserLoginDetail.DT_STARTDATE.Year, RGB, YARNSHADE, COATING, ENDUSE, LOCATION, STORE, ISEXCISABLE);
            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));
            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));
            
            
            if (dt.Columns["DEVELOPER_WEB"] == null)
                dt.Columns.Add("DEVELOPER_WEB", typeof(string));
            if (dt.Columns["DEVELOPER_COMP"] == null)
                dt.Columns.Add("DEVELOPER_COMP", typeof(string));
            
           
            foreach (DataRow dr in dt.Rows)
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

    //private DataTable GetData(char ch_View)
    //{
    //    try
    //    {

    //        SaitexDM.Common.DataModel.YRN_MST oYRN_MST = new SaitexDM.Common.DataModel.YRN_MST();
    //        DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetReport();
    //        return dt;
    //    }
    //    catch (OracleException ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //        return null;
    //    }
    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //        return null;
    //    }

    //}
}
