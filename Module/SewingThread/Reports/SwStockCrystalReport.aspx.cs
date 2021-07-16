using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
public partial class Module_SewingThread_Reports_SwStockCrystalReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      try
        {
            if (Session["MaterialLedger"] != null)
            {
                DataTable dt = (DataTable)Session["MaterialLedger"];

                string BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
                string DEPT_CODE = dt.Rows[0]["DEPT_CODE"].ToString();
                string YARN_CAT = dt.Rows[0]["YARN_CAT"].ToString();
                string YARN_TYPE = dt.Rows[0]["YARN_TYPE"].ToString();
                string YARN_CODE = dt.Rows[0]["YARN_CODE"].ToString();
                DateTime StDate = DateTime.Parse(dt.Rows[0]["StDate"].ToString());
                DateTime EnDate = DateTime.Parse(dt.Rows[0]["EnDate"].ToString());
                int Year = int.Parse(dt.Rows[0]["YEAR"].ToString());
                DataTable dtrportdat = GetData(BRANCH_CODE, DEPT_CODE, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate, Year);
                GetReport(dtrportdat);
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
            rDoc.Load(Server.MapPath(@"SWStockReport.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;

        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(string BRANCH_CODE, string DEPT_CODE, string YARN_CAT, string YARN_TYPE, string YARN_CODE, DateTime StDate, DateTime EnDate, int Year)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt = SaitexBL.Interface.Method.YRN_MST.Load_Stock_Data(BRANCH_CODE, DEPT_CODE, YARN_CAT, YARN_TYPE, YARN_CODE, StDate, EnDate, oUserLoginDetail.COMP_CODE, Year,"","","","","");

            if (dt.Rows.Count > 0)
            {
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
            }
            else
            {
                Common.CommonFuction.ShowMessage(" Sir Data not found accroding to this record . ");
            }

            return dt;
        }
        catch
        {
            throw;
        }

    }
}

