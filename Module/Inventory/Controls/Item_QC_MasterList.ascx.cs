using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using errorLog;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using WCFMain;

public partial class Module_Inventory_Controls_Item_QC_MasterList : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                if (!IsPostBack)
                {
                    InitialControls();

                }

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page Loading.\r\nSee error log for detail."));
        }


    }

    protected void InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;
            lblTotalRecord.Text = "0";
            GetQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GetQuery()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (dt.Rows.Count > 0)
            {

                grdITEMMasterQuery.DataSource = dt;
                grdITEMMasterQuery.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grdITEMMasterQuery.Visible = true;
            }
            else
            {
                grdITEMMasterQuery.DataSource = null;
                grdITEMMasterQuery.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }



        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void FilterGrid_Click(object sender, EventArgs e)
    {
        try
        {
            SearchbyKeywords();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SearchbyKeywords()
    {

        string txtITEM_CATEGORY = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CATEGORY")).Text;
        string txtITEM_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CODE")).Text;
        string txtItemDesc = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemDesc")).Text;
        string txtSTD_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_VALUE")).Text;
        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtTOLERANCE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE")).Text;
        string txtTOLERANCE_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_TYPE")).Text;
        string txtTOLERANCE_RANGE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_RANGE")).Text;
        string txtUOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string txtREMARKS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtREMARKS")).Text;
        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;

        string txtMAX_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text;
        string txtMIN_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text;

        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtITEM_CATEGORY, txtITEM_CODE, txtItemDesc, txtSTD_VALUE, txtSTD_TYPE, txtTOLERANCE, txtTOLERANCE_TYPE, txtTOLERANCE_RANGE, txtUOM, txtREMARKS, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE);
        if (dt.Rows.Count > 0)
        {
            grdITEMMasterQuery.DataSource = dt;
            grdITEMMasterQuery.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            grdITEMMasterQuery.Visible = true;

        }
        else
        {
            grdITEMMasterQuery.DataSource = dt;
            grdITEMMasterQuery.DataBind();
            lblTotalRecord.Text = "0";
            CommonFuction.ShowMessage("No data found by this parameter");
        }
        AutofillSearchContent(txtITEM_CATEGORY, txtITEM_CODE, txtItemDesc, txtSTD_VALUE, txtSTD_TYPE, txtTOLERANCE, txtTOLERANCE_TYPE, txtTOLERANCE_RANGE, txtUOM, txtREMARKS, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE);

    }

    private void AutofillSearchContent(string txtITEM_CATEGORY, string txtITEM_CODE, string txtItemDesc, string txtSTD_VALUE, string txtSTD_TYPE, string txtTOLERANCE, string txtTOLERANCE_TYPE, string txtTOLERANCE_RANGE, string txtUOM, string txtREMARKS, string txtSTATUS, string txtMAX_VALUE, string txtMIN_VALUE)
    {
        try
        {
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CATEGORY")).Text = txtITEM_CATEGORY;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CODE")).Text = txtITEM_CODE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemDesc")).Text = txtItemDesc;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_VALUE")).Text = txtSTD_VALUE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text = txtSTD_TYPE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE")).Text = txtTOLERANCE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_TYPE")).Text = txtTOLERANCE_TYPE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_RANGE")).Text = txtTOLERANCE_RANGE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text = txtUOM;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtREMARKS")).Text = txtREMARKS;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text = txtSTATUS;

            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text = txtMAX_VALUE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text = txtMIN_VALUE;
        }
        catch
        {
            throw;
        }

    }
    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/ITEM_QC_Master.aspx");
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string txtITEM_CATEGORY = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CATEGORY")).Text;
        string txtITEM_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CODE")).Text;
        string txtItemDesc = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemDesc")).Text;
        string txtSTD_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_VALUE")).Text;
        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtTOLERANCE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE")).Text;
        string txtTOLERANCE_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_TYPE")).Text;
        string txtTOLERANCE_RANGE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_RANGE")).Text;
        string txtUOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;


        string URL = "../Reports/Item_QC_MasterReport.aspx?Cat=" + txtITEM_CATEGORY + "&Item_Code=" + txtITEM_CODE + "&ItemDesc=" + txtItemDesc + "&STD_VALUE=" + txtSTD_VALUE + "&STD_TYPE=" + txtSTD_TYPE + "&TOLERANCE=" + txtTOLERANCE + "&TOLERANCE_TYPE=" + txtTOLERANCE_TYPE + "&TOLERANCE_RANGE=" + txtTOLERANCE_RANGE + "&UOM=" + txtUOM + "&STATUS=" + txtSTATUS;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);


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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void grUserMasterQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        try
        {
            GetQuery();

            grdITEMMasterQuery.PageIndex = e.NewPageIndex;
            grdITEMMasterQuery.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Item_Master_List_" + DateTime.Now.ToString() + ".xls";
        string txtITEM_CATEGORY = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CATEGORY")).Text;
        string txtITEM_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CODE")).Text;
        string txtItemDesc = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemDesc")).Text;
        string txtSTD_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_VALUE")).Text;
        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtTOLERANCE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE")).Text;
        string txtTOLERANCE_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_TYPE")).Text;
        string txtTOLERANCE_RANGE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_RANGE")).Text;
        string txtUOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string txtREMARKS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtREMARKS")).Text;
        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;
        string txtMAX_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text;
        string txtMIN_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text;

        var data = SaitexBL.Interface.Method.TX_ITEM_MST.GetQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtITEM_CATEGORY, txtITEM_CODE, txtItemDesc, txtSTD_VALUE, txtSTD_TYPE, txtTOLERANCE, txtTOLERANCE_TYPE, txtTOLERANCE_RANGE, txtUOM, txtREMARKS, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE);
        Common.CommonFuction.ExporttoExcel(data, strFilename, "Item QC Standard Master List", oUserLoginDetail.VC_COMPANYNAME);

    }
}
