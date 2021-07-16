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

public partial class Module_Production_Controls_WIPStockQueryForm : System.Web.UI.UserControl
{
    DateTime StDate;
    DateTime EnDate;
    DateTime Sdate = System.DateTime.Now;
    DateTime Edate = System.DateTime.Now;

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
                getDenier();
               
                BindPartyCode();
                bindddlLotNo();
                bindGvWipStockDetails();
                Sdate = oUserLoginDetail.DT_STARTDATE;
                Edate = Common.CommonFuction.GetYearEndDate(Sdate);
                TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                TxtToDate.Text =  Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page loading.\r\nSee error log for detail."));
        }
    }
    private void getDenier()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetDenier();
            ddlDenier.DataSource = dt;
            ddlDenier.DataValueField = "ARTICLE_DESC";
            ddlDenier.DataTextField = "ARTICLE_DESC";
            ddlDenier.DataBind();
            ddlDenier.Items.Insert(0, new ListItem("------------Select-------------", ""));
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
            ddlDepartment.Items.Insert(0, new ListItem("------------Select-------------",""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void getBrachName( )
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_BRANCH_MST.GetBranchMaster();
            ddlBranch.DataSource = dt;
            ddlBranch.DataValueField = "BRANCH_NAME";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("------------Select-------------", ""));
            dt.Dispose();
            dt = null;
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void bindGvWipStockDetails() 
  {
        string DEPT_CODE = string.Empty;
        string BRANCH_CODE = string.Empty;
        string PROS_ID_NO = string.Empty;
        string FromDate = string.Empty;
        string ToDate = string.Empty;
        string LOT_NUMBER = string.Empty;
        string ORDER_NO = string.Empty;
        string BRANCH_NAME = string.Empty;
        string PRTY_NAME = string.Empty;
        string ARTICLE_DESC = string.Empty;
        string PRODUCT_TYPE = string.Empty;

        try
        {
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddlDepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }
            if (ddllotno.SelectedValue.ToString() != null && ddllotno.SelectedValue.ToString() != string.Empty)
            {
                LOT_NUMBER = ddllotno.SelectedValue.ToString();
            }
            else
            {
                LOT_NUMBER = string.Empty;
            }
            if (ddlpartycode.SelectedValue.ToString() != null && ddlpartycode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_NAME = ddlpartycode.SelectedValue.ToString();
            }
            else
            {
                PRTY_NAME = string.Empty;
            }
            if (ddlDenier.SelectedValue.ToString() != null && ddlDenier.SelectedValue.ToString() != string.Empty)
            {
                ARTICLE_DESC = ddlDenier.SelectedValue.ToString();
            }
            else
            {
                ARTICLE_DESC = string.Empty;
            }

            //if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            //{
            //    StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
            //    EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
            //}
            //else
            //{
            //    StDate = Sdate;
            //    EnDate = Edate;

            //}
            if (ddlProductType.SelectedValue.ToString() != null && ddlProductType.SelectedValue.ToString() != string.Empty && ddlProductType.SelectedIndex > 0)
            {
                PRODUCT_TYPE = ddlProductType.SelectedValue.ToString();
            }
            else
            {
                PRODUCT_TYPE = string.Empty;
            }
            
            

            DataTable dt = new DataTable();

            dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.WIPStockQuery(BRANCH_CODE, DEPT_CODE, PROS_ID_NO, FromDate, ToDate, LOT_NUMBER, ORDER_NO, PRTY_NAME, ARTICLE_DESC, StDate, EnDate, PRODUCT_TYPE, ChkStock.Checked);
            if (dt.Rows.Count > 0)
            {
                Grid1.DataSource = dt;
                Grid1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                Grid1.Visible = true;
                dt.Dispose();
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
    private void BindPartyCode()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            ddlpartycode.Items.Clear();
            ddlpartycode.DataSource = dt;
            ddlpartycode.DataValueField = "PRTY_CODE";
            ddlpartycode.DataTextField = "PRTY_NAME";
            ddlpartycode.DataBind();
            ddlpartycode.Items.Insert(0, new ListItem("------------Select-------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void bindddlLotNo()
    {
        try
        {
            ddllotno.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetLotNumber();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddllotno.DataValueField = "LOT_NUMBER";
                ddllotno.DataTextField = "LOT_NUMBER";
                ddllotno.DataSource = dt;
                ddllotno.DataBind();
            }
            ddllotno.Items.Insert(0, new ListItem("------------Select-------------", ""));
        }
        catch
        {
            throw;
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            bindGvWipStockDetails();
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
            Response.Redirect("./ProductionQueryForm.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string Query_String = string.Empty;
        try
        {
            string QueryString = "";
            bool flag = false;

            if (ddlBranch.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "BRANCH_NAME" + ddlBranch.SelectedValue.Trim();
                flag = true;
            }

            if (ddllotno.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "LOT_NUMBER=" + ddllotno.SelectedValue.Trim();
                flag = true;
            }

            if (ddlDenier.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "ARTICLE_DESC=" + ddlDenier.SelectedValue.Trim();
                flag = true;
            }
            if (ddlpartycode.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "PRTY_NAME=" + ddlpartycode.SelectedValue.Trim();
                flag = true;
            }

            //string Query_String = string.Empty;
            string URL = "../../Production/Report/FiberWIPStockReport.aspx" + QueryString;     
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
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                if (StartDate < StDate || StartDate > EnDate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = StDate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtToDate.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        bindGvWipStockDetails();
    }
}