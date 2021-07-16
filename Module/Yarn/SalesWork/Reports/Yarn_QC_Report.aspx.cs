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

public partial class Module_Yarn_SalesWork_Reports_Yarn_QC_Report : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dtrportdat = GetData();
            GetReport(dtrportdat);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }


    private void GetReport(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"Yarn_QC_MasterReport.rpt"));
            rDoc.SetDataSource(dt);
            Yarn_QC_MasterReport1.ReportSource = rDoc;
        }
        else
        {
            Common.CommonFuction.ShowMessage("No Data Found");
        }
    }


    private DataTable GetData()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string Inward_Type = string.Empty;
            string YCat = string.Empty;
            string Y_Code = string.Empty;
            string YDesc = string.Empty;
            string NCount = string.Empty;
            string TI = string.Empty;
            string STD_TYPE = string.Empty;
            string TOLERANCE = string.Empty;
            string TOLERANCE_TYPE = string.Empty;
            string TOLERANCE_RANGE = string.Empty;
            string UOM = string.Empty;
            string STATUS = string.Empty;
         
            if (Request.QueryString["Inward_Type"] != null)
            {
                Inward_Type = Request.QueryString["Inward_Type"].ToString();
            }
            if (Request.QueryString["YCat"] != null)
            {
                YCat = Request.QueryString["YCat"].ToString();
            }
            if (Request.QueryString["Y_Code"] != null)
            {
                Y_Code = Request.QueryString["Y_Code"].ToString();
            }
            if (Request.QueryString["YDesc"] != null)
            {
                YDesc = Request.QueryString["YDesc"].ToString();
            }
            if (Request.QueryString["NCount"] != null)
            {
                NCount = Request.QueryString["NCount"].ToString();
            }
            if (Request.QueryString["TI"] != null)
            {
                TI = Request.QueryString["TI"].ToString();
            }
            if (Request.QueryString["STD_TYPE"] != null)
            {
                STD_TYPE = Request.QueryString["STD_TYPE"].ToString();
            }
            if (Request.QueryString["TOLERANCE"] != null)
            {
                TOLERANCE = Request.QueryString["TOLERANCE"].ToString();
            }
            if (Request.QueryString["TOLERANCE_TYPE"] != null)
            {
                TOLERANCE_TYPE = Request.QueryString["TOLERANCE_TYPE"].ToString();
            }
            if (Request.QueryString["TOLERANCE_RANGE"] != null)
            {
                TOLERANCE_RANGE = Request.QueryString["TOLERANCE_RANGE"].ToString();
            }

            if (Request.QueryString["UOM"] != null)
            {
                UOM = Request.QueryString["UOM"].ToString();
            }
            if (Request.QueryString["STATUS"] != null)
            {
                STATUS = Request.QueryString["STATUS"].ToString();
            }
           

            DataTable dt = SaitexBL.Interface.Method.YARN_STANDARD_PARAMETER_MST.GetQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Inward_Type, YCat, Y_Code, YDesc, NCount, TI, STD_TYPE, TOLERANCE, TOLERANCE_TYPE, TOLERANCE_RANGE, UOM,  STATUS, "", "");
            if (dt != null && dt.Rows.Count > 0)
            {
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

