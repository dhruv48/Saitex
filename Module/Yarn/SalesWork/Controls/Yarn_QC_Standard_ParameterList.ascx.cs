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


public partial class Module_Yarn_SalesWork_Controls_Yarn_QC_Standard_ParameterList : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            try
            {
                if (!IsPostBack)
                {
                    InitialControls();

                }
            }
            catch (Exception ex)
            {
                errorLog.ErrHandler.WriteError(ex.Message);
            }
        }
        else
        {
            Response.Redirect("~/default.aspx", false);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void GetQuery()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YARN_STANDARD_PARAMETER_MST.GetQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    public void SearchbyKeywords()
    {
        string txtINWARD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtINWARD_TYPE")).Text;
        string txtYARN_CATEGORY = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CATEGORY")).Text;
        string txtYARN_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CODE")).Text;
        string txtYARN_DESC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_DESC")).Text;
        string txtNOMINAL_COUNT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtNOMINAL_COUNT")).Text;
        string txtTOTAL_IMPERFECTION = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOTAL_IMPERFECTION")).Text;


        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtTOLERANCE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE")).Text;
        string txtTOLERANCE_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_TYPE")).Text;
        string txtTOLERANCE_RANGE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_RANGE")).Text;
        string txtUOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string txtREMARKS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtREMARKS")).Text;
        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;

        string txtMAX_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text;
        string txtMIN_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text;

        DataTable dt = SaitexBL.Interface.Method.YARN_STANDARD_PARAMETER_MST.GetQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtINWARD_TYPE, txtYARN_CATEGORY, txtYARN_CODE, txtYARN_DESC, txtNOMINAL_COUNT, txtTOTAL_IMPERFECTION, txtSTD_TYPE, txtTOLERANCE, txtTOLERANCE_TYPE, txtTOLERANCE_RANGE, txtUOM, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE);
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
        AutofillSearchContent(txtINWARD_TYPE, txtYARN_CATEGORY, txtYARN_CODE, txtYARN_DESC, txtNOMINAL_COUNT, txtTOTAL_IMPERFECTION, txtSTD_TYPE, txtTOLERANCE, txtTOLERANCE_TYPE, txtTOLERANCE_RANGE, txtUOM, txtREMARKS, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE);

    }

    private void AutofillSearchContent(string txtINWARD_TYPE, string txtYARN_CATEGORY, string txtYARN_CODE, string txtYARN_DESC, string txtNOMINAL_COUNT, string txtTOTAL_IMPERFECTION, string txtSTD_TYPE, string txtTOLERANCE, string txtTOLERANCE_TYPE, string txtTOLERANCE_RANGE, string txtUOM, string txtREMARKS, string txtSTATUS, string txtMAX_VALUE, string txtMIN_VALUE)
    {
        try
        {
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtINWARD_TYPE")).Text = txtINWARD_TYPE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CATEGORY")).Text = txtYARN_CATEGORY;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CODE")).Text = txtYARN_CODE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_DESC")).Text = txtYARN_DESC;

            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtNOMINAL_COUNT")).Text = txtNOMINAL_COUNT;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOTAL_IMPERFECTION")).Text = txtTOTAL_IMPERFECTION;

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
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_QC_Master.aspx");
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string txtINWARD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtINWARD_TYPE")).Text;
        string txtYARN_CATEGORY = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CATEGORY")).Text;
        string txtYARN_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CODE")).Text;
        string txtYARN_DESC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_DESC")).Text;
        string txtNOMINAL_COUNT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtNOMINAL_COUNT")).Text;
        string txtTOTAL_IMPERFECTION = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOTAL_IMPERFECTION")).Text;


        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtTOLERANCE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE")).Text;
        string txtTOLERANCE_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_TYPE")).Text;
        string txtTOLERANCE_RANGE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_RANGE")).Text;
        string txtUOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;

        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;

        string URL = "../Reports/Yarn_QC_Report.aspx?Inward_Type=" + txtINWARD_TYPE + "&YCat=" + txtYARN_CATEGORY + "&Y_Code=" + txtYARN_CODE + "&YDesc=" + txtYARN_DESC + "&NCount=" + txtNOMINAL_COUNT + "&TI=" + txtTOTAL_IMPERFECTION + "&STD_TYPE=" + txtSTD_TYPE + "&TOLERANCE=" + txtTOLERANCE + "&TOLERANCE_TYPE=" + txtTOLERANCE_TYPE + "&TOLERANCE_RANGE=" + txtTOLERANCE_RANGE + "&UOM=" + txtUOM + "&STATUS=" + txtSTATUS;
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
            errorLog.ErrHandler.WriteError(ex.Message);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Yarn_QC_Master_List_" + DateTime.Now.ToString() + ".xls";
        string txtINWARD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtINWARD_TYPE")).Text;
        string txtYARN_CATEGORY = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CATEGORY")).Text;
        string txtYARN_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CODE")).Text;
        string txtYARN_DESC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_DESC")).Text;
        string txtNOMINAL_COUNT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtNOMINAL_COUNT")).Text;
        string txtTOTAL_IMPERFECTION = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOTAL_IMPERFECTION")).Text;


        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtTOLERANCE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE")).Text;
        string txtTOLERANCE_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_TYPE")).Text;
        string txtTOLERANCE_RANGE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTOLERANCE_RANGE")).Text;
        string txtUOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;

        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;
        string txtMAX_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text;
        string txtMIN_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text;
        var data = SaitexBL.Interface.Method.YARN_STANDARD_PARAMETER_MST.GetQCQuery(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtINWARD_TYPE, txtYARN_CATEGORY, txtYARN_CODE, txtYARN_DESC, txtNOMINAL_COUNT, txtTOTAL_IMPERFECTION, txtSTD_TYPE, txtTOLERANCE, txtTOLERANCE_TYPE, txtTOLERANCE_RANGE, txtUOM, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE);
        Common.CommonFuction.ExporttoExcel(data, strFilename, "Yarn QC Standard Master List", oUserLoginDetail.VC_COMPANYNAME);

    }
}