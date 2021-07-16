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
using Common;
using errorLog;
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_PlanningAndScheduling_Queries_OrderMovement1 : System.Web.UI.Page
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    string branch = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                Initial_Control();

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }

    private void Initial_Control()
    {
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        Sdate = oUserLoginDetail.DT_STARTDATE;
        Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
        getBrachName();
     //   BindYear();
    //    ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
        BindProductType();
        getDepartment();
  
    }

    private void BindProductType()
    {
        try
        {
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);
            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();
            BindOrderNo(ddlProductType.SelectedItem.ToString());
           // ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(PRODUCT_TYPE));
           // ddlProductType.Text = PRODUCT_TYPE;
           // ddlProductType.Enabled = false;

            dtProductionType.Dispose();
            dtProductionType = null;
        }
        catch
        {

            throw;
        }
    }

    private void BindOrderNo(string strProductType)
    {
        try
        {
          //  string strProductType = ddlProductType.SelectedValue.ToString();
            DataTable dt = SaitexDL.Interface.Method.OD_CAPT_MST.GetOrderNo(strProductType);
            ddlOrderNo.DataSource = dt;
            ddlOrderNo.DataValueField = "ORDER_NO";
            ddlOrderNo.DataTextField = "ORDER_NO";
            ddlOrderNo.DataBind();
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void getBrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            //ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }


    //private void BindYear()
    //{
    //    try
    //    {

    //        string branch = ddlBranch.SelectedValue.ToString();
    //        DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddlYear.Items.Clear();
    //            ddlYear.DataSource = dt;
    //            ddlYear.DataTextField = "YEAR";
    //            ddlYear.DataValueField = "FIN_YEAR_CODE";
    //            ddlYear.DataBind();
    //            //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));

    //            dt.Dispose();
    //            dt = null;
    //        }
    //        else
    //        {
    //            string brnch = ddlBranch.SelectedItem.Text.Trim();
    //            CommonFuction.ShowMessage(" No financial Year associated with " + brnch + " branch ");
    //            getBrachName();
    //            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
    //            BindYear();
    //            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
    //            bindFromToDate();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}


    private void bindFromToDate()
    {
       // string FIN_YEAR_CODE = ddlYear.SelectedValue.ToString();
        string FIN_YEAR_CODE = oUserLoginDetail.FinYear;
        DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "FIN_YEAR_CODE='" + oUserLoginDetail.FinYear + "'";
            if (dv.Count > 0)
            {
                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                {


                    TxtFromDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                    TxtToDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                }
            }
        }
    }


    private void getDepartment()
    {
        try
        {
            ddlDepartment.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlDepartment.DataValueField = "DEPT_CODE";
                ddlDepartment.DataTextField = "DEPT_NAME";
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Queries/OrderMovement.aspx?BRANCH_CODE="+ ddlBranch.SelectedValue.ToString() + "&DEPT_CODE=" + ddlDepartment.SelectedValue.ToString() + "&BUSINESS_TYPE=" + ddlBusinessType.SelectedValue.ToString() + "&PRODUCT_TYPE=" + ddlProductType.SelectedValue.ToString() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString() + "&ORDER_NO=" + ddlOrderNo.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
 
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Open Report.\r\nSee error log for detail."));
    
        }
    }
    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click1(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOrderNo(ddlProductType.SelectedItem.ToString());
        ddlOrderNo.Items.Insert(0, new ListItem("---------------All---------------", ""));
       
    }
}
