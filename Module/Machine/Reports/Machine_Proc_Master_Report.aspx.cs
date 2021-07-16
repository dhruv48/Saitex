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
using errorLog;

public partial class Module_Machine_Reports_Machine_Proc_Master_Report : System.Web.UI.Page
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
                DataSet st = new DataSet();
                st= GetData(key);
                GetReport(st);
            
        }
    }

  private DataSet GetData(string key)
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {

            DataSet dt =  SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetDataForReportFormachineproccess(key);
            dt.Tables[0].TableName = "TX_MACH_PROC_MST";
            dt.Tables[1].TableName = "TX_MAC_STD_TRN";
            dt.Tables[2].TableName = "TX_MAC_RECP_TRN";
           if (dt.Tables[0].Columns["COMP_NAME"] == null)
                dt.Tables[0].Columns.Add("COMP_NAME", typeof(string));

            if (dt.Tables[0].Columns["BRANCH_NAME"] == null)
                dt.Tables[0].Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Tables[0].Columns["USER_NAME"] == null)
                dt.Tables[0].Columns.Add("USER_NAME", typeof(string));

            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dt.AcceptChanges();
            }

            return dt;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
    
    private void GetReport(DataSet dt)
    {
        
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load((Server.MapPath(@"Machine_Proc_Master_Report.rpt")));
        rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rdoc;
    }


    protected void btnselect_Click(object sender, EventArgs e)
    {
        string key = ddProducttype.SelectedValue;
        DataSet dt = GetData(key);
        GetReport(dt);
    }



    protected void ddProducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindProcessCodeByProductType();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Product type selection.\r\nSee error log for detail."));
        }
    }


      private void BindProcessCodeByProductType()
    {
        try
        {

            string key = ddProducttype.SelectedValue;
           
        }
        catch
        {
            throw;
        }
    }


      private void Bindvendorcategory()
      {
          try
          {


              ddProducttype.Items.Clear();
              DataTable dt = GET_VEND_DATA("", "PROS_CODE");
              ddProducttype.DataSource = dt;
              ddProducttype.DataValueField = "PROS_CODE";
              ddProducttype.DataTextField = "PROS_CODE";
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


      private DataTable GET_VEND_DATA(string Text, string PRTY_GRP_CODE)
      {
          try
          {
              SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
              string CommandText = "select DISTINCT PROS_CODE from TX_MAC_PROC_MST where Del_Status=0 and  comp_code='" + oUserLoginDetail.COMP_CODE + "'   ";
              string WhereClause = " and PROS_CODE like :SearchQuery";
              string SortExpression = " order by PROS_CODE asc";
              string SearchQuery = Text + "%";
              DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, PRTY_GRP_CODE, oUserLoginDetail.COMP_CODE);
              return dt;
          }
          catch (Exception EX)
          {
              throw EX;
          }
      }
}
