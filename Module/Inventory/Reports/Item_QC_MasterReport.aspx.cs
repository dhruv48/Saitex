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

public partial class Module_Inventory_Reports_Item_QC_MasterReport : System.Web.UI.Page
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
            rDoc.Load(Server.MapPath(@"Item_QC_MasterReport.rpt"));
            rDoc.SetDataSource(dt);
            Item_QC_MasterReport1.ReportSource = rDoc;
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
            DataTable dt = null;
            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                string Cat = string.Empty;
                string Item_Code = string.Empty;
                string ItemDesc = string.Empty;
                string STD_VALUE = string.Empty;
                string STD_TYPE = string.Empty;
                string TOLERANCE = string.Empty;
                string TOLERANCE_TYPE = string.Empty;
                string TOLERANCE_RANGE = string.Empty;
                string UOM = string.Empty;
                string STATUS = string.Empty;

                if (Request.QueryString["Cat"] != null)
                {
                    Cat = Request.QueryString["Cat"].ToString();
                }
                if (Request.QueryString["Item_Code"] != null)
                {
                    Item_Code = Request.QueryString["Item_Code"].ToString();
                }
                if (Request.QueryString["ItemDesc"] != null)
                {
                    ItemDesc = Request.QueryString["ItemDesc"].ToString();
                }
                if (Request.QueryString["STD_VALUE"] != null)
                {
                    STD_VALUE = Request.QueryString["STD_VALUE"].ToString();
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

                 dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, Cat, Item_Code, ItemDesc, STD_VALUE, STD_TYPE, TOLERANCE, TOLERANCE_TYPE, TOLERANCE_RANGE, UOM, "", STATUS, "", "");
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
