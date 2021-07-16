using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_PlanningAndScheduling_Controls_LotPlaningQueryForm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string COMP_CODE = string.Empty;
    public static string BRANCH_CODE = string.Empty;
    public static string PRODUCT_TYPE = string.Empty;
    public static string ORDER_CAT = string.Empty;
    public static string ORDER_TYPE = string.Empty;
    public static string ORDER_NO = string.Empty;
    public static string BUSINESS_TYPE = string.Empty;
    public static string PA_NO = string.Empty;
    private static string STATUS = string.Empty;
    public static string ARTICAL_CODE = string.Empty;
    public static string SHADE_CODE = string.Empty;
    public string PRODUCTTYPE
    {
        get
        {
            return PRODUCT_TYPE;
        }
        set
        {
            PRODUCT_TYPE = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"].ToString().Equals(string.Empty) == false)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }
            InitializePage();
        }

    }
    protected void InitializePage()
    {
        lblTotalRecords.Text = "0";
        BindOrder();
        BindDepartment();
        BindProductType();
        ddlOrderCategory.Items.Insert(0, new ListItem("---Select-----", ""));
        ddlBusiness.Items.Insert(0, new ListItem("---Select--------", ""));

        
    }
    protected void BindDepartment()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = null;
            dt = new DataTable();
            string strCompany = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompany);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0,new ListItem("-----Select--------",""));
            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue("oUserLoginDetail.CH_BRANCHCODE"));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    protected void BindOrder()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);
            ddlordertype.Items.Clear();
            ddlordertype.DataSource = dt;
            ddlordertype.DataValueField = "MST_DESC";
            ddlordertype.DataTextField = "MST_CODE";
            ddlordertype.DataBind();
            ddlordertype.Items.Insert(0, new ListItem("----Select---", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void BindProductType()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);

            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();

            ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(PRODUCT_TYPE));
            ddlProductType.Text = PRODUCT_TYPE;
            ddlProductType.Enabled = false;
        }
        catch
        {
            throw;
        }
    }
    protected void imgPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgExit_Click(object sender, ImageClickEventArgs e)
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ViewInGrid();
    }
    protected void ViewInGrid()
    {

        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (ddlProductType.SelectedValue.ToString() != null || ddlProductType.SelectedValue.ToString() != string.Empty)
            {
                PRODUCT_TYPE = ddlProductType.SelectedValue.ToString();
            }
            else
            {
                PRODUCT_TYPE = string.Empty;
            }
            if (ddlBranch.SelectedValue.ToString() != null || ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlBusiness.SelectedValue.ToString() != null || ddlBusiness.SelectedValue.ToString() != string.Empty)
            {
                BUSINESS_TYPE = ddlBusiness.SelectedValue.ToString();
            }
            else
            {
                BUSINESS_TYPE = string.Empty;
            }
            if (ddlOrderCategory.SelectedValue.ToString() != null || ddlOrderCategory.SelectedValue.ToString() != string.Empty)
            {
                ORDER_CAT = ddlOrderCategory.SelectedValue.ToString();

            }
            else
            {
                ORDER_CAT = string.Empty;
            }
            if (ddlordertype.SelectedValue.ToString() != null || ddlordertype.SelectedValue.ToString() != string.Empty)
            {
                ORDER_TYPE = ddlordertype.SelectedValue.ToString();
            }
            else
            {
                ORDER_TYPE = string.Empty;
            }
            if (ddlStatus.SelectedValue.ToString() != null || ddlStatus.SelectedValue.ToString() != string.Empty)
            {
                STATUS = ddlStatus.SelectedValue.ToString();

            }
            else
            {
                STATUS = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.ViewDataForGrid(PRODUCT_TYPE, ORDER_CAT, ORDER_TYPE, oUserLoginDetail.COMP_CODE, BRANCH_CODE, STATUS, BUSINESS_TYPE);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdLotPlanning.DataSource = dt;
                grdLotPlanning.DataBind();
                lblTotalRecords.Text = dt.Rows.Count.ToString();
            }
            else
            {
                grdLotPlanning.DataSource = null;
                grdLotPlanning.DataBind();
                Common.CommonFuction.ShowMessage("Data Not Present");
                lblTotalRecords.Text = "0";
            }


        }
        catch
        {
            throw;
        }
 
    }
    protected void grdLotPlanning_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdLotPlanning.PageIndex = e.NewPageIndex;
        ViewInGrid();

    }
    protected void grdLotPlanning_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail=(SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkLotDetail = (LinkButton)e.Row.FindControl("linkLotDetail");
                Label lblLot_id = (Label)e.Row.FindControl("lblLot_id");
                string LOT_ID = lblLot_id.Text;
                DataTable dt = new DataTable();
                dt = SaitexBL.Interface.Method.OD_CAPT_MST.GetLotDetailForHover(LOT_ID,PRODUCT_TYPE,oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE);
                if (dt.Rows.Count > 0)
                {
                    GridView grdLotDetail = (GridView)e.Row.FindControl("grdLotDetail");
                    grdLotDetail.DataSource = dt;
                    grdLotDetail.DataBind();
                }

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    protected void grdLotPlanning_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
          if (e.CommandName != "Page" && e.CommandName != "")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            Label lblComp_Code = (Label)gvr.FindControl("lblComp_Code");
            Label lblBranch_Code = (Label)gvr.FindControl("lbl_Branch_Code");
            Label lblBusiness_Type = (Label)gvr.FindControl("lblBusiness_Type");
            Label lblProduct_Type = (Label)gvr.FindControl("lblProduct_type");
            Label lblOrder_Type = (Label)gvr.FindControl("lblOrder_Type");
            Label lblOrder_Cat = (Label)gvr.FindControl("lblOrder_Cat");
            Label lblOrder_No = (Label)gvr.FindControl("lblOrder_No");
            Label lblPi_Type = (Label)gvr.FindControl("lblPi_Type");
            Label lblPA_No = (Label)gvr.FindControl("lblPA_NO");
            Label lblArtical_Code = (Label)gvr.FindControl("lblArtical_Code");
            Label lblShade_Code = (Label)gvr.FindControl("lblShade_Code");
            Label lblBom_Flag = (Label)gvr.FindControl("lblBom_Flag");
            Label lblLot_Flag = (Label)gvr.FindControl("lblLot_Flag");
            Label lblOrd_Qty = (Label)gvr.FindControl("lblOrder_QTY");
            Label lblLot_id = (Label)gvr.FindControl("lblLot_id");
            if (e.CommandName == "ViewBOM")
            {
                try
                {
                    string URL = "/Saitex/Module/PlanningAndScheduling/Pages/BOM.aspx";
                    URL = URL + "?COMP_CODE=" + lblComp_Code.Text;
                    URL = URL + "&BRANCH_CODE=" + lblBranch_Code.Text;
                    URL = URL + "&BUSINESS_TYPE=" + lblBusiness_Type.Text;
                    URL = URL + "&PRODUCT_TYPE=" + lblProduct_Type.Text;
                    URL = URL + "&ORDER_CAT=" + lblOrder_Cat.Text;
                    URL = URL + "&ORDER_TYPE=" + lblOrder_Type.Text;
                    URL = URL + "&ORDER_NO=" + lblOrder_No.Text;
                    URL = URL + "&PI_TYPE=" + lblPi_Type.Text;
                    URL = URL + "&PI_NO=" + lblPA_No.Text;
                    URL = URL + "&ARTICAL_CODE=" + lblArtical_Code.Text;
                    URL = URL + "&SHADE_CODE=" + lblShade_Code.Text;
                    URL = URL + "&BOM_FLAG=" + lblBom_Flag.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
 
        }
    }
}
