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
public partial class Module_Yarn_SalesWork_Reports_YarnMasterwiseReoprt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string yarncode = string.Empty;
        string yarncat = string.Empty;
        string yarntype = string.Empty;
        string ply = string.Empty;
        string uom = string.Empty;
        string yarndesc = string.Empty;
        string HSNCODE = string.Empty;
        string maxstock = string.Empty;
        string fancyeffect = string.Empty;
        string blending = string.Empty;
        string colour = string.Empty;
     //   string CUSTOMITCHSCODE = string.Empty;
    //    string SALESITCHSCODE = string.Empty;
        string ISEXCISABLE = string.Empty;
    //    string TARIFFHEADING = string.Empty;
        string YARNSHADE = string.Empty;
        string LOCATION = string.Empty;
        string RGB = string.Empty;
        string STORE = string.Empty;
        string ENDUSE = string.Empty;

        if (Request.QueryString["yarncode"] != null)
        {
            yarncode = Request.QueryString["yarncode"].ToString();

        }
        if (Request.QueryString["yarncat"] != null)
        {
            yarncat = Request.QueryString["yarncat"].ToString();
        }
        if (Request.QueryString["yarntype"] != null)
        {
            yarntype = Request.QueryString["yarntype"].ToString();
        }
     
        if (Request.QueryString["ply"] != null)
        {
            ply = Request.QueryString["ply"].ToString();
        }
        if (Request.QueryString["uom"] != null)
        {
            uom = Request.QueryString["uom"].ToString();

        }
        if (Request.QueryString["yarndesc"] != null)
        {
            yarndesc = Request.QueryString["yarndesc"].ToString();
        }

        if (Request.QueryString["HSNCODE"] != null)
        {
            HSNCODE = Request.QueryString["HSNCODE"].ToString();
        }
        if (Request.QueryString["maxstock"] != null)
        {
            maxstock = Request.QueryString["maxstock"].ToString();
        }
        if (Request.QueryString["blending"] != null)
        {
            fancyeffect = Request.QueryString["blending"].ToString();
        }
        if (Request.QueryString["blending"] != null)
        {
            blending = Request.QueryString["blending"].ToString();
        }
        if (Request.QueryString["colour"] != null)
        {
            colour = Request.QueryString["colour"].ToString();
        }

        //if (Request.QueryString["CUSTOMITCHSCODE"] != null)
        //{
        //    CUSTOMITCHSCODE = Request.QueryString["CUSTOMITCHSCODE"].ToString();
        //}
        //if (Request.QueryString["SALESITCHSCODE"] != null)
        //{
        //    SALESITCHSCODE = Request.QueryString["SALESITCHSCODE"].ToString();
        //}
        if (Request.QueryString["ISEXCISABLE"] != null)
        {
            ISEXCISABLE = Request.QueryString["ISEXCISABLE"].ToString();
        }
        //if (Request.QueryString["TARIFFHEADING"] != null)
        //{
        //    TARIFFHEADING = Request.QueryString["TARIFFHEADING"].ToString();
        //}
        if (Request.QueryString["YARNSHADE"] != null)
        {
            YARNSHADE = Request.QueryString["YARNSHADE"].ToString();
        }
        if (Request.QueryString["LOCATION"] != null)
        {
            LOCATION = Request.QueryString["LOCATION"].ToString();
        }
        if (Request.QueryString["STORE"] != null)
        {
            STORE = Request.QueryString["STORE"].ToString();
        }
        if (Request.QueryString["RGB"] != null)
        {
            RGB = Request.QueryString["RGB"].ToString();
        }
        if (Request.QueryString["ENDUSE"] != null)
        {
            ENDUSE = Request.QueryString["ENDUSE"].ToString();
        }

        try
        {


            DataTable dtrportdat = GetData(yarncode, yarncat, yarntype, ply, colour, uom, yarndesc,HSNCODE, maxstock, fancyeffect, blending,  ISEXCISABLE,YARNSHADE,LOCATION,STORE,RGB,ENDUSE);
            GetReport(dtrportdat);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }

    }
    private DataTable GetData(string yarncode, string yarncat, string yarntype, string ply, string colour, string uom, string yarndesc,string HSNCODE, string maxstock, string fancyeffect, string blending, string ISEXCISABLE,string YARNSHADE,string LOCATION, string STORE,string RGB,string ENDUSE )
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FIBER_MST oTX_FIBER_MST = new SaitexDM.Common.DataModel.TX_FIBER_MST();
            DataTable dt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetUserQuery(yarncode, yarncat, yarntype, ply, colour, uom, yarndesc,HSNCODE, maxstock, fancyeffect, blending, ISEXCISABLE, YARNSHADE, LOCATION, STORE, RGB, ENDUSE);
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

    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"Yarn_masterqueryR.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }
}
