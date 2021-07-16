using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_OrderDevelopment_CustomerRequest_Controls_InvoiceOrderList : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                pageInstial();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void pageInstial()
    {
        try
        {
            grideBind();
            Branch_bind();
            Invoice_bind();
            YarnCode_bind();
            LotNo_bind();
            Year_bind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grideBind()
    {
        String Branch = string.Empty;
        String INVOICE_NUMB = String.Empty;
        String YARN_CODE = String.Empty;
        String LOT_NO = String.Empty;
        String Year = String.Empty;
        String FromDate = String.Empty;
        String ToDate = String.Empty;
        try
        {
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty && ddlbranch.SelectedIndex > 0)
            {
                Branch = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                Branch = string.Empty;
            }
            if (ddlinvoice.SelectedValue.ToString() != null && ddlinvoice.SelectedValue.ToString() != string.Empty && ddlinvoice.SelectedIndex > 0)
            {
                INVOICE_NUMB = ddlinvoice.SelectedValue.ToString();
            }
            else
            {
                INVOICE_NUMB = string.Empty;
            }
            if (ddlyrancode.SelectedValue.ToString() != null && ddlyrancode.SelectedValue.ToString() != string.Empty && ddlyrancode.SelectedIndex > 0)
            {
                YARN_CODE = ddlyrancode.SelectedValue.ToString();
            }
            else
            {
                YARN_CODE = string.Empty;
            }
            if (ddllotno.SelectedValue.ToString() != null && ddllotno.SelectedValue.ToString() != string.Empty && ddllotno.SelectedIndex > 0)
            {
                LOT_NO = ddllotno.SelectedValue.ToString();
            }
            else
            {
               LOT_NO = string.Empty;
            }
            if (ddlyear.SelectedValue.ToString() != null && ddlyear.SelectedValue.ToString() != string.Empty && ddlyear.SelectedIndex > 0)
            {
                Year = ddlyear.SelectedValue.ToString();
            }
            else
            {
                Year = string.Empty;
            }
            if (txtFROMDATE.Text.Trim().ToString() != null && txtFROMDATE.Text.Trim().ToString() != string.Empty && txtTODATE.Text.Trim().ToString() != null && txtTODATE.Text.Trim().ToString() != string.Empty)
            {
                FromDate = txtFROMDATE.Text.Trim().ToString();
                ToDate = txtTODATE.Text.Trim().ToString();
            }
            else
            {
                FromDate = string.Empty;
                ToDate = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.TX_INVOICE_MST.GetInvoice_OrderList(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE,Branch, LOT_NO, YARN_CODE, INVOICE_NUMB, FromDate, ToDate, Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdInvoiceOrderList.DataSource = dt;
                grdInvoiceOrderList.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
               
                grdInvoiceOrderList.Visible = true;
            }
            else
            {
                grdInvoiceOrderList.DataSource = null;
                grdInvoiceOrderList.DataBind();
                lblTotalRecord.Text = "0";
                grdInvoiceOrderList.Visible = false;
                CommonFuction.ShowMessage("No data found..");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void grdInvoiceOrderList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdInvoiceOrderList.PageIndex = e.NewPageIndex;
        grideBind();
    }
    protected void Branch_bind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);

            ddlbranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlbranch.DataSource = dv;
            ddlbranch.DataValueField = "BRANCH_CODE";
            ddlbranch.DataTextField = "BRANCH_NAME";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void Invoice_bind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_INVOICE_MST.getInvoiceNum(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlinvoice.Items.Clear();
            DataView dv = new DataView(dt);
            ddlinvoice.DataSource = dv;
            ddlinvoice.DataValueField = "INVOICE_NUMB";
            ddlinvoice.DataTextField = "INVOICE_NUMB";
            ddlinvoice.DataBind();
            ddlinvoice.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void YarnCode_bind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_INVOICE_MST.getYarnCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlyrancode.Items.Clear();
            DataView dv = new DataView(dt);
            ddlyrancode.DataSource = dv;
            ddlyrancode.DataValueField = "YARN_CODE";
            ddlyrancode.DataTextField = "YARN_CODE";
            ddlyrancode.DataBind();
            ddlyrancode.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void LotNo_bind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_INVOICE_MST.getLotNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddllotno.Items.Clear();
            DataView dv = new DataView(dt);
            ddllotno.DataSource = dv;
            ddllotno.DataValueField = "LOT_NO";
            ddllotno.DataTextField = "LOT_NO";
            ddllotno.DataBind();
            ddllotno.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void Year_bind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_INVOICE_MST.getYear(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlyear.Items.Clear();
            DataView dv = new DataView(dt);
            ddlyear.DataSource = dv;
            ddlyear.DataValueField = "YEAR";
            ddlyear.DataTextField = "YEAR";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./InvoiceOrderList.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    protected void btn_serch(object sender, EventArgs e)
    {
        String Branch = string.Empty;
        String INVOICE_NUMB = String.Empty;
        String YARN_CODE = String.Empty;
        String LOT_NO = String.Empty;
        String Year = String.Empty;
        String FromDate = String.Empty;
        String ToDate = String.Empty;
        try
        {
            
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty && ddlbranch.SelectedIndex > 0)
            {
                Branch = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                Branch = string.Empty;
            }
            if (ddlinvoice.SelectedValue.ToString() != null && ddlinvoice.SelectedValue.ToString() != string.Empty && ddlinvoice.SelectedIndex > 0)
            {
                INVOICE_NUMB = ddlinvoice.SelectedValue.ToString();
            }
            else
            {
                INVOICE_NUMB = string.Empty;
            }
            if (ddlyrancode.SelectedValue.ToString() != null && ddlyrancode.SelectedValue.ToString() != string.Empty && ddlyrancode.SelectedIndex > 0)
            {
                YARN_CODE = ddlyrancode.SelectedValue.ToString();
            }
            else
            {
                YARN_CODE = string.Empty;
            }
            if (ddllotno.SelectedValue.ToString() != null && ddllotno.SelectedValue.ToString() != string.Empty && ddllotno.SelectedIndex > 0)
            {
                LOT_NO = ddllotno.SelectedValue.ToString();
            }
            else
            {
               LOT_NO = string.Empty;
            }
            if (ddlyear.SelectedValue.ToString() != null && ddlyear.SelectedValue.ToString() != string.Empty && ddlyear.SelectedIndex > 0)
            {
                Year = ddlyear.SelectedValue.ToString();
            }
            else
            {
                Year = string.Empty;
            }
            if (txtFROMDATE.Text.Trim().ToString() != null && txtFROMDATE.Text.Trim().ToString() != string.Empty && txtTODATE.Text.Trim().ToString() != null && txtTODATE.Text.Trim().ToString() != string.Empty)
            {
                FromDate = txtFROMDATE.Text.Trim().ToString();
                ToDate = txtTODATE.Text.Trim().ToString();
            }
            else
            {
                FromDate = string.Empty;
                ToDate = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.TX_INVOICE_MST.GetInvoice_OrderList(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE,Branch, LOT_NO, YARN_CODE, INVOICE_NUMB, FromDate, ToDate, Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdInvoiceOrderList.DataSource = dt;
                grdInvoiceOrderList.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

                grdInvoiceOrderList.Visible = true;
            }
            else
            {
                grdInvoiceOrderList.DataSource = null;
                grdInvoiceOrderList.DataBind();
                lblTotalRecord.Text = "0";
                grdInvoiceOrderList.Visible = false;
                CommonFuction.ShowMessage("No data found..");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
