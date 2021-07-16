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


public partial class Module_Fabric_FabricSaleWork_Controls_FabricStockDetail : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    string branch = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControl();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void InitialControl()
    {
        try
        {
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            getBrachName();
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            BindFabricType();
            BindParty();
            BindShadeCode();
            ddlfabrictype.SelectedIndex = -1;
            ddlpartycode.SelectedIndex = -1;
            GrdFabricDetail.Visible = false;
            lblTotalRecord.Text = "0";

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindShadeCode()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL(oOD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeCode.Items.Clear();
                ddlShadeCode.DataSource = dt;
                ddlShadeCode.DataTextField = "SHADE_CODE";
                ddlShadeCode.DataValueField = "SHADE_CODE";
                ddlShadeCode.DataBind();
                ddlShadeCode.Items.Insert(0, new ListItem("--------All--------", string.Empty));

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btngetdata_Click(object sender, EventArgs e)
    {
        try
        {
            GetFabricDetail();
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
            InitialControl();
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
    protected void GrdFabricDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetFabricDetail();

            GrdFabricDetail.PageIndex = e.NewPageIndex;
            GrdFabricDetail.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
            Common.CommonFuction.ShowMessage("Unable to Load the Next Page");
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
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(" No financial Year associated with " + brnch + " branch ");
                getBrachName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                BindYear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                bindFromToDate();
            }
        }
        catch (Exception ex)
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
    private void BindFabricType()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabrType();
            ddlfabrictype.Items.Clear();
            ddlfabrictype.DataSource = dt;
            ddlfabrictype.DataValueField = "FABR_TYPE";
            ddlfabrictype.DataTextField = "FABR_TYPE";
            ddlfabrictype.DataBind();
            ddlfabrictype.Items.Insert(0, new ListItem("------All------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindParty()
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
            ddlpartycode.Items.Insert(0, new ListItem("------All------", ""));
            dt.Dispose();
            dt = null;
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
    private void bindFromToDate()
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindFromToDate();
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
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void GetFabricDetail()
    {
        DateTime StDate;
        DateTime EnDate;
        string BRANCH_CODE = string.Empty;
        string FABRIC_TYPE = string.Empty;
        string PRTY_CODE = string.Empty;
        string SHADE_CODE = string.Empty;
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
            
            if (ddlfabrictype.SelectedValue.ToString() != null && ddlfabrictype.SelectedValue.ToString() != string.Empty)
            {
                FABRIC_TYPE = ddlfabrictype.SelectedValue.ToString();
            }
            else
            {
                FABRIC_TYPE = string.Empty;
            }
            if (ddlpartycode.SelectedValue.ToString() != null && ddlpartycode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddlpartycode.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (ddlShadeCode.SelectedValue.ToString() != null && ddlShadeCode.SelectedValue.ToString() != string.Empty)
            {
                SHADE_CODE = ddlShadeCode.SelectedValue.ToString();
            }
            else
            {
                SHADE_CODE = string.Empty;
            }

            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
            }
            else
            {
                StDate = Sdate;
                EnDate = Edate;
            }

            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricDetail(BRANCH_CODE,FABRIC_TYPE, PRTY_CODE, SHADE_CODE, StDate, EnDate);
            if (dt.Rows.Count > 0)
            {
                GrdFabricDetail.DataSource = dt;
                GrdFabricDetail.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                GrdFabricDetail.Visible = true;
            }
            else
            {
                GrdFabricDetail.DataSource = null;
                GrdFabricDetail.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
