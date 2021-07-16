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

public partial class Module_Inventory_Reports_ITEM_MATER_QUERY_REPORT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string CatCode = string.Empty;
        string ItemCode = string.Empty;
        string ItemType = string.Empty;
        string ItemDesc = string.Empty;
        string HSNCODE = string.Empty;
        string ItemMake = string.Empty;
        string UOM = string.Empty;
        string OpeningBal =string.Empty; 
        string OpeningRate = string.Empty;
        string MinStLeveL = string.Empty;
        string ExpireDays =string.Empty;
        string Department = string.Empty;
        string ReorderQty = string.Empty;
        string Rack = string.Empty;
        string MaxStLeveL = string.Empty;
        string CONSUME = string.Empty;
        string MinProcure = string.Empty;
        string QC = string.Empty;
        string REMARKS = string.Empty;
        string STATUS = string.Empty;
        string ReOrderl = string.Empty;
        string CUSTOMITCHSCODE = string.Empty;
        string SALESITCHSCODE = string.Empty;
        string ISEXCISABLE = string.Empty;
        string TARIFFHEADING = string.Empty;
        string ISMOVABLE = string.Empty;
        string ITEMSIZE = string.Empty;
        string WEIGHT = string.Empty;


        if (Request.QueryString["Rack"] != null)
        {
            Rack = Request.QueryString["Rack"].ToString();
        }
        if (Request.QueryString["MaxStLeveL"] != null)
        {
            MaxStLeveL = Request.QueryString["MaxStLeveL"].ToString();
        }
        if (Request.QueryString["CONSUME"] != null)
        {
            CONSUME = Request.QueryString["CONSUME"].ToString();
        }
        if (Request.QueryString["MinProcure"] != null)
        {
            MinProcure = Request.QueryString["MinProcure"].ToString();
        }
        if (Request.QueryString["QC"] != null)
        {
            QC = Request.QueryString["QC"].ToString();
        }
        if (Request.QueryString["REMARKS"] != null)
        {
            REMARKS = Request.QueryString["REMARKS"].ToString();
        }
        if (Request.QueryString["STATUS"] != null)
        {
            STATUS = Request.QueryString["STATUS"].ToString();
        }
        if (Request.QueryString["ReOrderl"] != null)
        {
            ReOrderl = Request.QueryString["ReOrderl"].ToString();
        }

        //----------
        if (Request.QueryString["CatCode"] != null)
        {
            CatCode = Request.QueryString["CatCode"].ToString();
        }
        if (Request.QueryString["ItemCode"] != null)
        {
            ItemCode = Request.QueryString["ItemCode"].ToString();
        }
        if (Request.QueryString["ItemType"] != null)
        {
            ItemType = Request.QueryString["ItemType"].ToString();
        }
        if (Request.QueryString["ItemDesc"] != null)
        {
            ItemDesc = Request.QueryString["ItemDesc"].ToString();
        }
        if (Request.QueryString["HSNCODE"] != null)
        {
            ItemDesc = Request.QueryString["HSNCODE"].ToString();
        }

        if (Request.QueryString["ItemMake"] != null)
        {
            ItemMake = Request.QueryString["ItemMake"].ToString();
        }
        if (Request.QueryString["UOM"] != null)
        {
            UOM = Request.QueryString["UOM"].ToString();
        }
        if (Request.QueryString["OpeningBal"] != null)
        {
            OpeningBal = Request.QueryString["OpeningBal"].ToString();
        }
        if (Request.QueryString["OpeningRate"] != null)
        {
            OpeningRate = Request.QueryString["OpeningRate"].ToString();
        }
        if (Request.QueryString["MinStLeveL"] != null)
        {
            MinStLeveL = Request.QueryString["MinStLeveL"].ToString();

        }
        if (Request.QueryString["ExpireDays"] != null)
        {
            ExpireDays = Request.QueryString["ExpireDays"].ToString();
        }
        if (Request.QueryString["Department"] != null)
        {
            Department = Request.QueryString["Department"].ToString();
        }
        //if (Request.QueryString["ReorderQty"] != null)
        //{
        //    ReorderQty = Request.QueryString["ReorderQty"].ToString();
        //}
        if (Request.QueryString["CUSTOMITCHSCODE"] != null)
        {
            CUSTOMITCHSCODE = Request.QueryString["CUSTOMITCHSCODE"].ToString();
        }
        if (Request.QueryString["SALESITCHSCODE"] != null)
        {
            SALESITCHSCODE = Request.QueryString["SALESITCHSCODE"].ToString();
        }
        if (Request.QueryString["ISEXCISABLE"] != null)
        {
            ISEXCISABLE = Request.QueryString["ISEXCISABLE"].ToString();
        }
        if (Request.QueryString["TARIFFHEADING"] != null)
        {
            TARIFFHEADING = Request.QueryString["TARIFFHEADING"].ToString();
        }
        if (Request.QueryString["ISMOVABLE"] != null)
        {
            ISMOVABLE = Request.QueryString["ISMOVABLE"].ToString();
        }
        if (Request.QueryString["ITEMSIZE"] != null)
        {
            ITEMSIZE = Request.QueryString["ITEMSIZE"].ToString();
        }
        if (Request.QueryString["WEIGHT"] != null)
        {
            WEIGHT = Request.QueryString["WEIGHT"].ToString();
        }

        try
        {


            DataTable dtrportdat = GetData(CatCode, ItemCode, ItemType, ItemDesc,HSNCODE, ItemMake, UOM, OpeningBal, OpeningRate, MinStLeveL, ExpireDays, Department, ReorderQty, Rack, MaxStLeveL, CONSUME, MinProcure, QC, REMARKS, STATUS, ReOrderl, CUSTOMITCHSCODE, SALESITCHSCODE, ISEXCISABLE, TARIFFHEADING,ISMOVABLE,ITEMSIZE,WEIGHT);
            GetReport(dtrportdat);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Report.\r\nSee error log for detail."));
        }
    }


    private void GetReport(DataTable dt)
    {
        ReportDocument rDoc = new ReportDocument();
        rDoc.Load(Server.MapPath(@"ITEM_MATER_QUERY_REPORT.rpt"));
        rDoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rDoc;
    }


    private DataTable GetData(string CatCode, string ItemCode, string ItemType, string ItemMake, string ItemDesc,string HSNCODE, string UOM, string OpeningBal, string OpeningRate, string MinStLeveL, string ExpireDays, string Department, string ReorderQty, string Rack, string MaxStLeveL, string CONSUME, string MinProcure, string QC, string REMARKS, string STATUS, string ReOrderl, string CUSTOMITCHSCODE, string SALESITCHSCODE, string ISEXCISABLE, string TARIFFHEADING, string ISMOVABLE, string ITEMSIZE, string WEIGHT)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.TX_FIBER_MST oTX_FIBER_MST = new SaitexDM.Common.DataModel.TX_FIBER_MST();
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetUserQuery(CatCode, ItemCode, ItemType, ItemDesc,HSNCODE, ItemMake, UOM, OpeningBal, OpeningRate, MinStLeveL, ExpireDays, Department, ReorderQty, Rack, MaxStLeveL, CONSUME, MinProcure, QC, REMARKS, STATUS, ISEXCISABLE, ISMOVABLE, ITEMSIZE, WEIGHT);
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
