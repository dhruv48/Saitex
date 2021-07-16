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
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OracleClient;
using Common;

public partial class Module_Machine_Reports_Machine_Proute_MstRpt : System.Web.UI.Page
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (!IsPostBack)
        {
            Bindvendorcategory();

        }

        else
        {
            //To solve the error:
            //Failed to export using the options you specified. Please check your options and try again.

            string key = ddProducttype.SelectedValue;
            DataTable st = new DataTable();
            st = GetData(key);
            GetReport(st);

        }
        //DataTable dt = GetData();
        //GetReport(dt);

    }

    private void Bindvendorcategory()
    {
        try
        {


            ddProducttype.Items.Clear();
            DataTable dt = GET_VEND_DATA("", "PROS_ROUTE_CODE");
            ddProducttype.DataSource = dt;
            ddProducttype.DataValueField = "PROS_ROUTE_CODE";
            ddProducttype.DataTextField = "PROS_ROUTE_CODE";
            ddProducttype.DataBind();
            ddProducttype.Items.Insert(0, new ListItem("--------SELECT-------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddProducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string key = ddProducttype.SelectedValue;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Product type selection.\r\nSee error log for detail."));
        }
    }

    private DataTable GET_VEND_DATA(string Text, string PRTY_GRP_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select DISTINCT PROS_ROUTE_CODE from TX_PRO_STN_MST where Del_Status=0 and  comp_code='" + oUserLoginDetail.COMP_CODE + "'   ";
            string WhereClause = " and PROS_ROUTE_CODE like :SearchQuery";
            string SortExpression = " order by PROS_ROUTE_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, PRTY_GRP_CODE, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    
    private void GetReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load((Server.MapPath(@"Machine_Proute_MstRpt.rpt")));
        rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rdoc;
    }


    private DataTable GetData(string  key)
    {


        try
        {

            SaitexDM.Common.DataModel.MC_MACHINE_MASTER oTX_VENDOR_MST = new SaitexDM.Common.DataModel.MC_MACHINE_MASTER();

            DataTable dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.Printprocroutesmaster(key);
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
        catch (Exception ex)
        {
            throw ex;
        }



    }




    protected void btnselect_Click(object sender, EventArgs e)
    {
        string key = ddProducttype.SelectedValue;
        DataTable dt = GetData(key);
        GetReport(dt);
    }
}

