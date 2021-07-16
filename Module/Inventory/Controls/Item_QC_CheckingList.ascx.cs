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

public partial class Module_Inventory_Controls_Item_QC_CheckingList : System.Web.UI.UserControl
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
            errorLog.ErrHandler.WriteError(ex.Message);
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
            dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetQC_Checking_Query(0,oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, 0, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
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
                Common.CommonFuction.ShowMessage("No Data found");
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

    public DataTable SearchbyKeywords()
    {
        int TRN_NUMB = 0, TRN_YEAR = 0,QC_NUMB=0;
        int.TryParse(((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_YEAR")).Text, out TRN_YEAR);
        int.TryParse(((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_NUMB")).Text, out TRN_NUMB);

        int.TryParse(((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_NUMB")).Text, out QC_NUMB);

        //string txtTRN_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_TYPE")).Text;
        string txtTRN_DATE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_DATE")).Text;
        string txtITEM_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CODE")).Text;
        string txtITEM_DESC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_DESC")).Text;
        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtQC_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_VALUE")).Text;
        string txtQC_Result = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Result")).Text;
        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;
        string txtMAX_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text;
        string txtMIN_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text;
        string txtQC_DONE_BY = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_DONE_BY")).Text;
        string txtQC_DATE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_DATE")).Text;
        string txtQC_Approved_Result = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Approved_Result")).Text;
        string txtQC_CONF_DATE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_CONF_DATE")).Text;
        string txtQC_Approved_By = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Approved_By")).Text;


        string txtTRN_TYPE = "RMS01";
        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetQC_Checking_Query(QC_NUMB,oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_NUMB, TRN_YEAR, txtTRN_TYPE, txtTRN_DATE, txtITEM_CODE, txtITEM_DESC, txtSTD_TYPE, txtMAX_VALUE, txtMIN_VALUE, txtQC_VALUE, txtQC_Result, txtQC_Approved_Result, txtSTATUS, txtQC_DATE, txtQC_CONF_DATE, txtQC_DONE_BY, txtQC_Approved_By);
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
        AutofillSearchContent(QC_NUMB,TRN_YEAR, TRN_NUMB, txtTRN_DATE, txtITEM_CODE, txtITEM_DESC, "", txtSTD_TYPE, txtQC_VALUE, "", txtQC_Result, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE, txtQC_DONE_BY, "", txtQC_DATE, txtQC_Approved_Result, txtQC_CONF_DATE, txtQC_Approved_By);
        return dt;
    }

    private void AutofillSearchContent(int QC_NUMB,int TRN_YEAR, int txtTRN_NUMB,string txtTRN_DATE, string txtITEM_CODE, string txtITEM_DESC, string txtUOM, string txtSTD_TYPE, string txtQC_VALUE, string txtQC_REMARKS, string txtQC_Result, string txtSTATUS, string txtMAX_VALUE, string txtMIN_VALUE, string txtQC_DONE_BY, string txtPARTY_DATA, string txtQC_DATE, string txtQC_Approved_Result, string txtQC_CONF_DATE, string txtQC_Approved_By)
    {
        try
        {
            if (QC_NUMB == 0)
            {
                ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_NUMB")).Text = "";
            }
            else
            {
                ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_NUMB")).Text = QC_NUMB.ToString();
            }
            if (TRN_YEAR == 0)
            {
                ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_YEAR")).Text = "";
            }
            else
            {
                ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_YEAR")).Text = TRN_YEAR.ToString();
            }
            if (txtTRN_NUMB == 0)
            {
                ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_NUMB")).Text = "";
            }
            else
            {
                ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_NUMB")).Text = txtTRN_NUMB.ToString();
            }

          
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_DATE")).Text = txtTRN_DATE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CODE")).Text = txtITEM_CODE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_DESC")).Text = txtITEM_DESC;

            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text = txtSTD_TYPE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_VALUE")).Text = txtQC_VALUE;
     
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Result")).Text = txtQC_Result;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text = txtSTATUS;

            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text = txtMAX_VALUE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text = txtMIN_VALUE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_DONE_BY")).Text = txtQC_DONE_BY;
        
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_DATE")).Text = txtQC_DATE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Approved_Result")).Text = txtQC_Approved_Result;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_CONF_DATE")).Text = txtQC_CONF_DATE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Approved_By")).Text = txtQC_Approved_By;
        }
        catch
        {
            throw;
        }

    }
    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Item_QC_Checking.aspx");
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        int TRN_NUMB = 0, TRN_YEAR = 0, QC_NUMB = 0;
        int.TryParse(((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_NUMB")).Text, out QC_NUMB);
        int.TryParse(((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_YEAR")).Text, out TRN_YEAR);
        int.TryParse(((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_NUMB")).Text, out TRN_NUMB);
        string txtTRN_TYPE = "RMS01";
        string txtTRN_DATE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_DATE")).Text;
        string txtITEM_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_CODE")).Text;
        string txtITEM_DESC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEM_DESC")).Text;
        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtQC_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_VALUE")).Text;
        string txtQC_Result = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Result")).Text;
        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;
        string txtMAX_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text;
        string txtMIN_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text;
        string txtQC_DONE_BY = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_DONE_BY")).Text;
        string txtQC_DATE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_DATE")).Text;
        string txtQC_Approved_Result = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Approved_Result")).Text;
        string txtQC_CONF_DATE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_CONF_DATE")).Text;
        string txtQC_Approved_By = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Approved_By")).Text;

        string URL = "../Reports/Item_QC_CheckingReport.aspx?QC_NUMB=" + QC_NUMB + "&TRN_NUMB=" + TRN_NUMB.ToString() + "&TRN_TYPE=" + txtTRN_TYPE + "&TRN_YEAR=" + TRN_YEAR + "&TRN_DATE=" + txtTRN_DATE + "&ITEM_CODE=" + txtITEM_CODE + "&ITEM_DESC=" + txtITEM_DESC + "&STD_TYPE=" + txtSTD_TYPE + "&QC_VALUE=" + txtQC_VALUE + "&QC_Rst=" + txtQC_Result + "&STATUS=" + txtSTATUS + "&MAX_VALUE=" + txtMAX_VALUE + "&MIN_VALUE=" + txtMIN_VALUE + "&QC_DONE_BY=" + txtQC_DONE_BY + "&QC_DATE=" + txtQC_DATE + "&QC_Apprv_Rst=" + txtQC_Approved_Result + "&QC_CONF_DATE=" + txtQC_CONF_DATE + "&QC_Apprv_By=" + txtQC_Approved_By;
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
        string strFilename = "Item_QC_Checking_List_" + DateTime.Now.ToString() + ".xls";
        Common.CommonFuction.ExporttoExcel(SearchbyKeywords(), strFilename, "Item QC Checking List", oUserLoginDetail.VC_COMPANYNAME);

    }
}