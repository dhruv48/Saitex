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
using Common;
using errorLog;
public partial class Module_Production_Report_ProductEntryReport : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {   
        try
        {
            if (Session["Proceereport"] != null)
            {
                DataTable dt = (DataTable)Session["Proceereport"];

                string MACHINE_CODE = dt.Rows[0]["MACHINE_CODE"].ToString();
                string ORDER_NO = dt.Rows[0]["ORDER_NO"].ToString();
                string PROS_CODE = dt.Rows[0]["PROS_CODE"].ToString();
                string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
                string LOT_NUMBER = dt.Rows[0]["LOT_NUMBER"].ToString();
                string DYED_LOT_NO = dt.Rows[0]["DYED_LOT_NO"].ToString();
                string SFT_ID = dt.Rows[0]["SFT_ID"].ToString();
                string FromDate =  dt.Rows[0]["FromDate"].ToString();
                string ToDate = dt.Rows[0]["ToDate"].ToString();
                DataTable dtrportdat = GetData(MACHINE_CODE, ORDER_NO, PROS_CODE, DEPT_CODE, LOT_NUMBER, DYED_LOT_NO, SFT_ID,FromDate, ToDate);
                GetReport(dtrportdat);
                dtrportdat.Dispose();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }
    private void GetReport(DataTable dt)
    {
        try
        {
            ReportDocument rDoc = new ReportDocument();
            rDoc.Load(Server.MapPath(@"productEntry.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
         
        }
        catch
        {
            throw;
        }
    }

    private DataTable GetData(string MACHINE_CODE, string ORDER_NO, string PROS_CODE, string DEPT_CODE, string LOT_NUMBER, string DYED_LOT_NO, string SFT_ID, string FromDate, string ToDate)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetProductEntry(MACHINE_CODE, ORDER_NO, PROS_CODE, DEPT_CODE, LOT_NUMBER, DYED_LOT_NO, SFT_ID, FromDate, ToDate);
            //if (dt.Columns["COMP_NAME"] == null)
            //    dt.Columns.Add("COMP_NAME", typeof(string));

            //if (dt.Columns["BRANCH_NAME"] == null)
            //    dt.Columns.Add("BRANCH_NAME", typeof(string));

            //if (dt.Columns["USER_NAME"] == null)
            //    dt.Columns.Add("USER_NAME", typeof(string));
            //if (dt.Columns["COMP_ADD"] == null)
            //    dt.Columns.Add("COMP_ADD", typeof(string));

            //if (dt.Columns["DEVELOPER_WEB"] == null)
            //    dt.Columns.Add("DEVELOPER_WEB", typeof(string));
            //if (dt.Columns["DEVELOPER_COMP"] == null)
            //    dt.Columns.Add("DEVELOPER_COMP", typeof(string));


            //foreach (DataRow dr in dt.Rows)
            //{
            //    dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
            //    dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
            //    dr["USER_NAME"] = oUserLoginDetail.Username;
            //    dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
            //    dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
            //    dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;

            //    dt.AcceptChanges();
            //}

            return dt;

        }
       
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

    }

}
