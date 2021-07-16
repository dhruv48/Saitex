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
using System.Data.OracleClient;

using errorLog;
using Common;

public partial class Module_Production_Controls_Packed_Yarn_Recepit_Query : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {


                getBrachName();
                getDepartment();
                getLotNo();
                
                bindGvItemMaster();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }

    private void getLotNo()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetLotNumber();
            ddlLotNo.DataSource = dt;
            ddlLotNo.DataValueField = "LOT_NUMBER";
            ddlLotNo.DataTextField = "LOT_NUMBER";
            ddlLotNo.DataBind();
            ddlLotNo.Items.Insert(0, new ListItem("-------Select-------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
   

    private void getDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("---------Select--------", ""));
            dt.Dispose();
            dt = null;
        }


        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }

    private void getBrachName()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            ddlBranch.DataSource = dt;
            ddlBranch.DataValueField = "BRANCH_NAME";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("---------Select--------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void bindGvItemMaster()
    {
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string PROS_ID_NO = string.Empty;
        string FromDate = string.Empty;
        string ToDate = string.Empty;
        string LOT_NUMBER = string.Empty;
        string ORDER_NO = string.Empty;
        string BRANCH_NAME = string.Empty;

        try
        {


            //if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            //{
            //    BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            //}
            //else
            //{
            //    BRANCH_CODE = string.Empty;
            //}
            //if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            //{
            //    DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            //}
            //else
            //{
            //    DEPT_CODE = string.Empty;
            //}
            //if (ddlBatchNo.SelectedValue.ToString() != null && ddlBatchNo.SelectedValue.ToString() != string.Empty)
            //{
            //    PROS_ID_NO = ddlBatchNo.SelectedValue.ToString();
            //}
            //else
            //{
            //    PROS_ID_NO = string.Empty;
            //}

            //if (txtformdate.Text.ToString() != null && txtformdate.Text.ToString() != string.Empty)
            //{
            //    FromDate = txtformdate.Text.ToString();
            //}
            //else
            //{
            //    FromDate = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            //}

            //if (txtTodate.Text.ToString() != null && txtTodate.Text.ToString() != string.Empty)
            //{
            //    ToDate = txtTodate.Text.ToString();
            //}
            //else
            //{
            //    ToDate = System.DateTime.Now.Date.ToShortDateString();
            //}
            //if (ddlLotNo.SelectedValue.ToString() != null && ddlLotNo.SelectedValue.ToString() != string.Empty)
            //{
            //    LOT_NUMBER = ddlLotNo.SelectedValue.ToString();
            //}
            //else
            //{
            //    LOT_NUMBER = string.Empty;
            //}
            //if (ddlOrderNo.SelectedValue.ToString() != null && ddlOrderNo.SelectedValue.ToString() != string.Empty)
            //{
            //    ORDER_NO = ddlOrderNo.SelectedValue.ToString();
            //}
            //else
            //{
            //    ORDER_NO = string.Empty;
            //}


            DataTable dt = new DataTable();

            
            dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetPackedYarnQuery(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, ORDER_NO);
            if (dt.Rows.Count > 0)
            {
                Grid1.DataSource = dt;
                Grid1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                Grid1.Visible = true;
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
                lblTotalRecord.Text = "0";
                Grid1.Visible = false;
                Common.CommonFuction.ShowMessage("Record Not Available By This Parameter.");

            }
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            bindGvItemMaster();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./FiberWIPStockQuery.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            string QueryString = "";
            //bool flag = false;

            //if (ddlBranch.SelectedValue.Trim() != "")
            //{
            //    if (!flag)
            //        QueryString = QueryString + "?";
            //    else
            //        QueryString = QueryString + "&";
            //    QueryString = QueryString + "BRANCH_NAME=" + ddlBranch.SelectedValue.Trim();
            //    flag = true;
            //}


            //if (ddlOrderNo.SelectedValue.Trim() != "")
            //{
            //    if (!flag)
            //        QueryString = QueryString + "?";
            //    else
            //        QueryString = QueryString + "&";
            //    QueryString = QueryString + "ORDER_NO=" + ddlOrderNo.SelectedValue.Trim();
            //    flag = true;
            //}
            //if (ddlLotNo.SelectedValue.Trim() != "")
            //{
            //    if (!flag)
            //        QueryString = QueryString + "?";
            //    else
            //        QueryString = QueryString + "&";
            //    QueryString = QueryString + "LOT_NUMBER=" + ddlLotNo.SelectedValue.Trim();
            //    flag = true;
            //}
            //if (ddlBatchNo.SelectedValue.Trim() != "")
            //{
            //    if (!flag)
            //        QueryString = QueryString + "?";
            //    else
            //        QueryString = QueryString + "&";
            //    QueryString = QueryString + "PROS_ID_NO=" + ddlBatchNo.SelectedValue.Trim();
            //    flag = true;
            //}
            //if (ddlDepartment.SelectedValue.Trim() != "")
            //{
            //    if (!flag)
            //        QueryString = QueryString + "?";
            //    else
            //        QueryString = QueryString + "&";
            //    QueryString = QueryString + "DEPT_CODE=" + ddlDepartment.SelectedValue.Trim();
            //    flag = true;
            //}




            string Query_String = string.Empty;
            string URL = "../../Production/Report/Yarn_Packed_Report.aspx" + QueryString;
            //string URL = "../../Production/Report/ProductionFormReport.aspx?Query_String =" + Query_String;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }


}