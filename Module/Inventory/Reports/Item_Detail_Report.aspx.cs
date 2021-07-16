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

public partial class Module_Inventory_Reports_Item_Detail_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ItemDetail"] != null)
            {
                DataTable dt = (DataTable)Session["ItemDetail"];
                string BRANCH_CODE = dt.Rows[0]["BRANCH_CODE"].ToString();
                string CAT_CODE = dt.Rows[0]["CAT_CODE"].ToString();
                string ITEM_TYPE = dt.Rows[0]["ITEM_TYPE"].ToString();
                string PRTY_CODE = dt.Rows[0]["PRTY_CODE"].ToString();

                DataTable dtrportdat = GetData(BRANCH_CODE, CAT_CODE, ITEM_TYPE, PRTY_CODE);
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
            rDoc.Load(Server.MapPath(@"Item_Detail_Report.rpt"));
            rDoc.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rDoc;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetData(string BRANCH_CODE, string CAT_CODE, string ITEM_TYPE, string PRTY_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetMeterialData(BRANCH_CODE, CAT_CODE, ITEM_TYPE, PRTY_CODE);
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
                CommonFuction.ShowMessage("Data Not Found .");
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
