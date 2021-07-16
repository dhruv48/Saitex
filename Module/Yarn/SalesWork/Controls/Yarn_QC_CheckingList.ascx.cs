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

public partial class Module_Yarn_SalesWork_Controls_Yarn_QC_CheckingList : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
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
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetQCChecking_Query(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,"", 0, "", "", "", 0, 0, "", 0,  "", "", "", "", "");
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

    public DataTable SearchbyKeywords()
    {
        int TRN_NUMB = 0, TRN_YEAR = 0, QC_NUMB = 0;

        string txtQC_NUMB = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_NUMB")).Text;
        string txtTRN_YEAR = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_YEAR")).Text;
        string txtTRN_NUMB = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_NUMB")).Text;
         string txtTRN_DATE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_DATE")).Text;
      
   
        string txtYARN_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CODE")).Text;
        string txtYARN_DESC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_DESC")).Text;
        string txtY_COUNT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtY_COUNT")).Text;

        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtQC_Value = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Value")).Text;
        string txtQ_RESULT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQ_RESULT")).Text;
        string txtQC_CHANGE_RESULT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_CHANGE_RESULT")).Text;

        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;

        string txtMAX_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMAX_VALUE")).Text;
        string txtMIN_VALUE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMIN_VALUE")).Text;

        int.TryParse(txtTRN_YEAR, out TRN_YEAR);
        int.TryParse(txtTRN_NUMB, out TRN_NUMB);
        double STD_VALUE = 0;
        double.TryParse(txtQC_Value, out STD_VALUE);
        int.TryParse(txtQC_NUMB, out QC_NUMB);

        DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetQCChecking_Query(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,txtTRN_DATE, TRN_NUMB, txtY_COUNT, txtYARN_CODE, txtYARN_DESC, QC_NUMB, TRN_YEAR, txtSTD_TYPE,  STD_VALUE, txtQ_RESULT, txtQC_CHANGE_RESULT, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE);
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
        AutofillSearchContent(txtTRN_DATE,txtTRN_YEAR, txtTRN_NUMB, QC_NUMB, txtYARN_CODE, txtYARN_DESC, txtY_COUNT, txtSTD_TYPE, txtQC_Value, txtQ_RESULT, txtQC_CHANGE_RESULT, txtSTATUS, txtMAX_VALUE, txtMIN_VALUE);
        return dt;
    }

    private void AutofillSearchContent(string txtTRN_DATE, string txtTRN_YEAR, string txtTRN_NUMB, int QC_NUMB, string txtYARN_CODE, string txtYARN_DESC, string txtY_COUNT, string txtSTD_TYPE, string txtQC_Value, string txtQ_RESULT, string txtQC_CHANGE_RESULT, string txtSTATUS, string txtMAX_VALUE, string txtMIN_VALUE)
    {
        try
        {
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_YEAR")).Text = txtTRN_YEAR;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_NUMB")).Text = txtTRN_NUMB;
            if (QC_NUMB > 0)
            {
                ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_NUMB")).Text = QC_NUMB.ToString();

            }
            else
            {
                ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_NUMB")).Text = QC_NUMB.ToString();
            }
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_DATE")).Text = txtTRN_DATE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CODE")).Text = txtYARN_CODE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_DESC")).Text = txtYARN_DESC;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtY_COUNT")).Text = txtY_COUNT;

            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text = txtSTD_TYPE;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Value")).Text = txtQC_Value;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQ_RESULT")).Text = txtQ_RESULT;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_CHANGE_RESULT")).Text = txtQC_CHANGE_RESULT;


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
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_QC_Checking.aspx");
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        int TRN_NUMB = 0, TRN_YEAR = 0 , QC_NUMB = 0;

        string txtQC_NUMB = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_NUMB")).Text;
        string txtTRN_YEAR = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_YEAR")).Text;
        string txtTRN_NUMB = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTRN_NUMB")).Text;
        string txtYARN_CODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_CODE")).Text;
        string txtYARN_DESC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtYARN_DESC")).Text;
        string txtY_COUNT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtY_COUNT")).Text;
        string txtSTD_TYPE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTD_TYPE")).Text;
        string txtQC_Value = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_Value")).Text;
        string txtQ_RESULT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQ_RESULT")).Text;
        string txtQC_CHANGE_RESULT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC_CHANGE_RESULT")).Text;
        string txtSTATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;
        int.TryParse(txtTRN_YEAR, out TRN_YEAR);
        int.TryParse(txtTRN_NUMB, out TRN_NUMB);
        int.TryParse(txtQC_NUMB, out QC_NUMB);
        double STD_VALUE = 0;
        double.TryParse(txtQC_Value, out STD_VALUE);

        string URL = "../Reports/Yarn_QC_CheckingReport.aspx?QC_NUMB=" + QC_NUMB + "&TRN_YEAR=" + TRN_YEAR + "&TRN_NUMB=" + TRN_NUMB + "&Y_Code=" + txtYARN_CODE + "&YDesc=" + txtYARN_DESC + "&Y_COUNT=" + txtY_COUNT + "&STD_TYPE=" + txtSTD_TYPE + "&QC_VALUE=" + STD_VALUE + "&Q_RESULT=" + txtQ_RESULT + "&QC_CHANGE_RESULT=" + txtQC_CHANGE_RESULT + "&STATUS=" + txtSTATUS;
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
        string strFilename = "Yarn_QC_Checking_List_" + DateTime.Now.ToString() + ".xls";
        DataTable dt = SearchbyKeywords();
        Common.CommonFuction.ExporttoExcel(dt, strFilename, "Yarn QC Checking List", oUserLoginDetail.VC_COMPANYNAME);

    }
}