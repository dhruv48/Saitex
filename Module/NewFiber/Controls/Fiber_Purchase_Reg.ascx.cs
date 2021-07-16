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

public partial class Module_Fiber_Controls_Fiber_Purchase_Reg : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private  string Start_Year = string.Empty;
    private  string End_Year = string.Empty;
    private  DateTime Sdate;
    private  DateTime Edate;

    protected void Page_Load(object sender, EventArgs e)
    {

        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
                GridFiberPurchaseReg();

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }

    private void InitialControls()
    {
        try
        {
            grd_FiberPurchase_query.Visible = false;
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            getBranchName();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            BindFromToDate();
            BindFibertype();
            BindFiberCat();
            BindPartyCode();
            //TxtFromDate.Text = string.Empty;
            //TxtToDate.Text = string.Empty;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void getBranchName()
    {
        try
        {


            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            ddlBranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlBranch.DataSource = dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindYear()
    {
        try
        {

            string branch = ddlBranch.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);
            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataBind();


                dt.Dispose();
                dt = null;
            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(" No financial Year associated with " + brnch + " branch ");
                getBranchName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                BindYear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFromToDate()
    {
        try
        {
            string FIN_YEAR_CODE = ddlYear.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "FIN_YEAR_CODE='" + ddlYear.SelectedValue.ToString() + "'";
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
        catch (Exception ex)
        {
        }
    }

    private void BindFibertype()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiberCat();
            ddlFibertype.Items.Clear();
            ddlFibertype.DataSource = dt;
            ddlFibertype.DataValueField = "FIBER_CAT";
            ddlFibertype.DataTextField = "FIBER_CAT";
            ddlFibertype.DataBind();
            ddlFibertype.Items.Insert(0, new ListItem("------SELECT------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFiberCat()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiber();
            ddlFibercat.Items.Clear();
            ddlFibercat.DataSource = dt;
            ddlFibercat.DataValueField = "FIBER_CODE";
            ddlFibercat.DataTextField = "FIBER_DESC";
            ddlFibercat.DataBind();
            ddlFibercat.Items.Insert(0, new ListItem("-------SELECT--------", string.Empty));
            dt.Dispose();
            dt = null;
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
            ddlpartycode.Items.Insert(0, new ListItem("------SELECT-------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim().ToString() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
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
            throw ex;
        }
    }

    private void GridFiberPurchaseReg()
    {
        DateTime StDate;
        DateTime EnDate;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string FIBER_CAT = string.Empty;
        string FIBER_CODE = string.Empty;
        string PRTY_CODE = string.Empty;
        try
        {
            if (ddlYear.SelectedItem.Text.ToString() != null && ddlYear.SelectedItem.Text.ToString() != string.Empty)
            {
                YEAR = ddlYear.SelectedItem.Text.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (TxtFromDate.Text.Trim().ToString() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
            }
            else
            {
                StDate = Sdate;
                EnDate = Edate;
            }
            if (ddlFibertype.SelectedValue.ToString() != null && ddlFibertype.SelectedValue.ToString() != string.Empty)
            {
                FIBER_CAT = ddlFibertype.SelectedValue.ToString();
            }
            else
            {
                FIBER_CAT = string.Empty;
            }
            if (ddlFibercat.SelectedValue.ToString() != null && ddlFibercat.SelectedValue.ToString() != string.Empty)
            {
                FIBER_CODE = ddlFibercat.SelectedValue.ToString();
            }
            else
            {
                FIBER_CODE = string.Empty;
            }
            if (ddlpartycode.SelectedValue.ToString() != null && ddlpartycode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddlpartycode.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }


            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetFiberData(YEAR, BRANCH_CODE, StDate, EnDate, FIBER_CAT, FIBER_CODE, PRTY_CODE);
            if (dt.Rows.Count > 0)
            {
                grd_FiberPurchase_query.DataSource = dt;
                grd_FiberPurchase_query.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grd_FiberPurchase_query.Visible = true;
            }
            else
            {
                grd_FiberPurchase_query.DataSource = null;
                grd_FiberPurchase_query.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btngetdata_Click(object sender, EventArgs e)
    {
        GridFiberPurchaseReg();
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControls();
        }
        catch (Exception ex)
        {
            throw ex;
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
            throw ex;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindYear();
            BindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void grd_FiberPurchase_query_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridFiberPurchaseReg();

            grd_FiberPurchase_query.PageIndex = e.NewPageIndex;
            grd_FiberPurchase_query.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
