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

public partial class Module_Yarn_SalesWork_Controls_Order_bom_query : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
                BindControls();
                GetBomData();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
            txtarticlecode.Text = "";
            txtshadecode.Text = "";
            txtbasearticlecode.Text = "";
            txtbaseshadecode.Text = "";
            ddlProductType.SelectedIndex = -1;
            
            grdOrderBomQuery.Visible = false;
            lblTotalRecord.Text = "0";

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindControls()
    {
        try
        {
            BindOrderType();
            BindProductType();
            
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    //private void BindShadeCode()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL(oOD_SHADE_FAMILY);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            ddlShadeCode.Items.Clear();
    //            ddlShadeCode.DataSource = dt;
    //            ddlShadeCode.DataTextField = "SHADE_CODE";
    //            ddlShadeCode.DataValueField = "SHADE_CODE";
    //            ddlShadeCode.DataBind();
    //            ddlShadeCode.Items.Insert(0, new ListItem("----------Select-------------", string.Empty));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void BindOrderType()
    {
        try
        {
            ddlOrderType.Items.Clear();
            DataTable dtOrderType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);
            ddlOrderType.Items.Clear();
            ddlOrderType.DataSource = dtOrderType;
            ddlOrderType.DataTextField = "MST_DESC";
            ddlOrderType.DataValueField = "MST_CODE";
            ddlOrderType.DataBind();
            ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue("PRODUCTION"));
            // ddlOrderType.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));

            dtOrderType.Dispose();
            dtOrderType = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindProductType()
    {
        try
        {
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);
            ddlProductType.Items.Clear();
            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();
            ddlProductType.Items.Insert(0, new ListItem("-----------Select---------", string.Empty));

            dtProductionType.Dispose();
            dtProductionType = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetBomData()
    {
        string ORDER_TYPE = string.Empty;
        string PRODUCT_TYPE = string.Empty;
        string SHADE_CODE = string.Empty;
        string pi_no = string.Empty;
        string artical_code = string.Empty;
        string base_artical_code = string.Empty;
        string BASE_SHADE_CODE = string.Empty;
        try
        {
            if (ddlOrderType.SelectedValue.ToString() != null && ddlOrderType.SelectedValue.ToString() != string.Empty)
            {
                ORDER_TYPE = ddlOrderType.SelectedValue.ToString();
            }
            else
            {
                ORDER_TYPE = string.Empty;
            }
            if (ddlProductType.SelectedValue.ToString() != null && ddlProductType.SelectedValue.ToString() != string.Empty)
            {
                PRODUCT_TYPE = ddlProductType.SelectedValue.ToString();
            }
            else
            {
                PRODUCT_TYPE = string.Empty;
            }
            if (txtshadecode.Text.ToString() != null && txtshadecode.Text.ToString() != string.Empty)
            {
                SHADE_CODE = txtshadecode.Text.ToString();
            }
            else
            {
                SHADE_CODE = string.Empty;
            }
            if (txtpano.Text.ToString() != null && txtpano.Text.ToString() != string.Empty)
            {
                pi_no = txtpano.Text.ToString();
            }
            else
            {
                pi_no = string.Empty;
            }
            if (txtarticlecode.Text.ToString() != null && txtarticlecode.Text.ToString() != string.Empty)
            {
                artical_code = txtarticlecode.Text.ToString();
            }
            else
            {
                artical_code = string.Empty;
            }
            if (txtbasearticlecode.Text.ToString() != null && txtbasearticlecode.Text.ToString() != string.Empty)
            {
                base_artical_code = txtbasearticlecode.Text.ToString();
            }
            else
            {
                base_artical_code = string.Empty;
            }
            if (txtbaseshadecode.Text.ToString() != null && txtbaseshadecode.Text.ToString() != string.Empty)
            {
                BASE_SHADE_CODE = txtbaseshadecode.Text.ToString();
            }
            else
            {
                BASE_SHADE_CODE = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetBomData(ORDER_TYPE, PRODUCT_TYPE, SHADE_CODE, pi_no, artical_code, base_artical_code, BASE_SHADE_CODE);
            if (dt.Rows.Count > 0)
            {
                grdOrderBomQuery.DataSource = dt;
                grdOrderBomQuery.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grdOrderBomQuery.Visible = true;
            }
            else
            {
                grdOrderBomQuery.DataSource = null;
                grdOrderBomQuery.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }
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

    protected void grdOrderBomQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetBomData();

            grdOrderBomQuery.PageIndex = e.NewPageIndex;
            grdOrderBomQuery.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btngetrecord_Click1(object sender, EventArgs e)
    {
        try
        {
            GetBomData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
