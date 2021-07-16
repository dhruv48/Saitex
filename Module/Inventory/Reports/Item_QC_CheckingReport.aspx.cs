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

public partial class Module_Inventory_Reports_Item_QC_CheckingReport : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            }
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
            rDoc.Load(Server.MapPath(@"Item_QC_CheckingReport.rpt"));
            rDoc.SetDataSource(dt);
            Item_QC_CheckingReport1.ReportSource = rDoc;
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



            int TRN_NUMB = 0, TRN_YEAR = 0, QC_NUMB = 0;
            string TRN_TYPE = string.Empty, QC_Apprv_By = "", QC_CONF_DATE = "", TRN_DATE = "", ITEM_DESC = "", QC_VALUE = "", MAX_VALUE = "", MIN_VALUE = "", QC_DONE_BY = "", QC_DATE = "", Item_Code = string.Empty, STD_TYPE = string.Empty, QCResult = string.Empty, MQCResult = string.Empty, STATUS = string.Empty;
            if (Request.QueryString["QC_NUMB"] != null)
            {
                int.TryParse(Request.QueryString["QC_NUMB"].ToString(), out QC_NUMB);
            }

            if (Request.QueryString["TRN_NUMB"] != null)
            {
                int.TryParse(Request.QueryString["TRN_NUMB"].ToString(), out TRN_NUMB);
            }

            if (Request.QueryString["TRN_TYPE"] != null)
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].ToString();
            }
            if (Request.QueryString["TRN_YEAR"] != null)
            {
                int.TryParse(Request.QueryString["TRN_YEAR"].ToString(), out TRN_YEAR);
            }
            if (Request.QueryString["TRN_DATE"] != null)
            {
                TRN_DATE = Request.QueryString["TRN_DATE"].ToString();
            }
            if (Request.QueryString["ITEM_DESC"] != null)
            {
                ITEM_DESC = Request.QueryString["ITEM_DESC"].ToString();
            }
            if (Request.QueryString["QC_VALUE"] != null)
            {
                QC_VALUE = Request.QueryString["QC_VALUE"].ToString();
            }
            if (Request.QueryString["MAX_VALUE"] != null)
            {
                MAX_VALUE = Request.QueryString["MAX_VALUE"].ToString();
            }
            if (Request.QueryString["MIN_VALUE"] != null)
            {
                MIN_VALUE = Request.QueryString["MIN_VALUE"].ToString();
            }
            if (Request.QueryString["QC_DONE_BY"] != null)
            {
                QC_DONE_BY = Request.QueryString["QC_DONE_BY"].ToString();
            }
            if (Request.QueryString["QC_DATE"] != null)
            {
                QC_DATE = Request.QueryString["QC_DATE"].ToString();
            }
            if (Request.QueryString["QC_CONF_DATE"] != null)
            {
                QC_CONF_DATE = Request.QueryString["QC_CONF_DATE"].ToString();
            }
            if (Request.QueryString["ITEM_CODE"] != null)
            {
                Item_Code = Request.QueryString["ITEM_CODE"].ToString();
            }
            if (Request.QueryString["STD_TYPE"] != null)
            {
                STD_TYPE = Request.QueryString["STD_TYPE"].ToString();
            }
            if (Request.QueryString["QC_Rst"] != null)
            {
                QCResult = Request.QueryString["QC_Rst"].ToString();
            }
            if (Request.QueryString["QC_Apprv_Rst"] != null)
            {
                MQCResult = Request.QueryString["QC_Apprv_Rst"].ToString();
            }
            if (Request.QueryString["STATUS"] != null)
            {
                STATUS = Request.QueryString["STATUS"].ToString();
            }
            if (Request.QueryString["QC_Apprv_By"] != null)
            {
                QC_Apprv_By = Request.QueryString["QC_Apprv_By"].ToString();
            }

            dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetQCDataForReport(QC_NUMB, oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_NUMB, TRN_YEAR, TRN_TYPE, TRN_DATE, Item_Code, ITEM_DESC, STD_TYPE, MAX_VALUE, MIN_VALUE, QC_VALUE, QCResult, MQCResult, STATUS, QC_DATE, QC_CONF_DATE, QC_DONE_BY, QC_Apprv_By);

            if (dt.Columns["TITLE"] == null)
                dt.Columns.Add("TITLE", typeof(string));

            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                dr["TITLE"] = "Material QC Checking";
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
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
