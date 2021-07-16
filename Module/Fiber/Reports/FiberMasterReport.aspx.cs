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
public partial class Module_Fiber_Reports_FiberMasterReport : System.Web.UI.Page
{

    


    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["fiberreportdt"] != null)
        //{
        //    DataTable dt = (DataTable)Session["fiberreportdt"];

        //   // string yarncode = dt.Rows[0]["YARN_CODE"].ToString();
        //   // string dept = dt.Rows[0]["DEPT_CODE"].ToString();
        //    //string fibercat = dt.Rows[0]["FIBER_CAT"].ToString();
        //    //string branchcode = dt.Rows[0]["BRANCH_CODE"].ToString();
        //    //DataTable dtrportdat = GetData( fibercat, branchcode);
        //    //GetReport(dtrportdat);
        //}
        string poydesc = string.Empty;
        string poycat = string.Empty;
        string poysubcat = string.Empty;
        string filament = string.Empty;
        string lusture = string.Empty;
        string denier = string.Empty;
        string openrate = string.Empty;
        string maximumstock = string.Empty;
        string remarks = string.Empty;
        string partyname = string.Empty;
        string branch = string.Empty;
        string CUSTOMITCHSCODE = string.Empty;
        string SALESITCHSCODE = string.Empty;
        string ISEXCISABLE = string.Empty;
        string TARIFFHEADING = string.Empty;


        if (Request.QueryString["POYDESC"] != null)
        {
            poydesc = Request.QueryString["POYDESC"].ToString();
        }
       
        if (Request.QueryString["POYCAT"] != null)
        {
            poycat = Request.QueryString["POYCAT"].ToString();
        }
        if (Request.QueryString["POYSUBCAT"] != null)
        {
            poysubcat = Request.QueryString["POYSUBCAT"].ToString();
        }
        if (Request.QueryString["FILAMENT"] != null)
        {
            filament = Request.QueryString["FILAMENT"].ToString();
        }
        if (Request.QueryString["LUSTURE"] != null)
        {
            lusture = Request.QueryString["LUSTURE"].ToString();
        }

        if (Request.QueryString["DENIER"] != null)
        {
            denier = Request.QueryString["DENIER"].ToString();
        }
        if (Request.QueryString["OPENRATE"] != null)
        {
            openrate = Request.QueryString["OPENRATE"].ToString();
        }
        if (Request.QueryString["MAXIMUMSTOCK"] != null)
        {
            maximumstock = Request.QueryString["MAXIMUMSTOCK"].ToString();
        }
        if (Request.QueryString["REMARKS"] != null)
        {
            remarks = Request.QueryString["REMARKS"].ToString();
        }
        if (Request.QueryString["PARTYNAME"] != null)
        {
            partyname = Request.QueryString["PARTYNAME"].ToString();
        }
        if (Request.QueryString["BRANCH"] != null)
        {
            branch = Request.QueryString["BRANCH"].ToString();
        }

        if (Request.QueryString["TARIFFHEADING"] != null)
        {
            TARIFFHEADING = Request.QueryString["TARIFFHEADING"].ToString();
        }
        if (Request.QueryString["SALESITCHSCODE"] != null)
        {
            SALESITCHSCODE = Request.QueryString["SALESITCHSCODE"].ToString();
        }
        if (Request.QueryString["CUSTOMITCHSCODE"] != null)
        {
            CUSTOMITCHSCODE = Request.QueryString["CUSTOMITCHSCODE"].ToString();
        }
        if (Request.QueryString["ISEXCISABLE"] != null)
        {
            ISEXCISABLE = Request.QueryString["ISEXCISABLE"].ToString();
        }
        try
        {


            DataTable dtrportdat = GetData(poydesc, poycat, poysubcat, filament, lusture, denier, openrate, maximumstock, remarks, partyname, branch,CUSTOMITCHSCODE, SALESITCHSCODE, ISEXCISABLE, TARIFFHEADING);
                GetReport(dtrportdat);
                dtrportdat.Dispose();
           
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }


    


    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Fiber.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }


    private DataTable GetData(string poydesc, string poycat, string poysubcat, string filament, string lusture, string denier, string openrate, string maximumstock, string remarks, string partyname, string branch, string CUSTOMITCHSCODE, string SALESITCHSCODE, string ISEXCISABLE, string TARIFFHEADING)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FIBER_MST oTX_FIBER_MST = new SaitexDM.Common.DataModel.TX_FIBER_MST();
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetUserQuery(poydesc, poycat, poysubcat, filament, lusture, denier, openrate, maximumstock, remarks, partyname, branch, CUSTOMITCHSCODE, SALESITCHSCODE, ISEXCISABLE, TARIFFHEADING);
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

   
}
