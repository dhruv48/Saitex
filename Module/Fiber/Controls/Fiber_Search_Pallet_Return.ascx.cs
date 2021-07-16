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
using System.Data;

public partial class Module_Fiber_Controls_Fiber_Search_Pallet_Return : System.Web.UI.UserControl
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
            throw ex;
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
            dt = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetUserQuery("", "", "", "", "", "", "","");
            if (dt.Rows.Count > 0)
            {

                grdsearchpalletreturn.DataSource = dt;
                grdsearchpalletreturn.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grdsearchpalletreturn.Visible = true;
            }
            else
            {
                grdsearchpalletreturn.DataSource = null;
                grdsearchpalletreturn.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }



        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Fiber/Pages/Fiber_Pallet_Return.aspx", false);
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strFilename = "pallet_return_List_" + DateTime.Now.Date.ToString("dd/MM/yyyy") + ".xls";

            string ChallanNO = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtChallanNO")).Text;
            string ChallanDate = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtChallanDate")).Text;
            string PartyCode = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtPartyCode")).Text;
            string Merge = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMerge")).Text;
            string MRNNO = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMRNNO")).Text;
            string MergeNo = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMergeNo")).Text;
            string PalletCode = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtPalletCode")).Text;
            string NoOfPallet = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtNoOfPallet")).Text;

            var data = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetUserQuery(ChallanNO, ChallanDate, PartyCode, Merge, MRNNO, MergeNo, PalletCode, NoOfPallet);
            UploadDataTableToExcel(data, strFilename);
        }

        catch( Exception ex)
            {
                throw ex;
            }
    }

    protected void UploadDataTableToExcel(DataTable dtEmp, string filename)
    {


        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;
        foreach (DataColumn dtcol in dtEmp.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dtEmp.Rows)
        {
            tab = "";
            for (int j = 0; j < dtEmp.Columns.Count; j++)
            {
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void grdsearchpalletreturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetQuery();

            grdsearchpalletreturn.PageIndex = e.NewPageIndex;
            grdsearchpalletreturn.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        string ChallanNO = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtChallanNO")).Text;
        string ChallanDate = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtChallanDate")).Text;
        string PartyCode = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtPartyCode")).Text;
        string Merge = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMerge")).Text;
        string MRNNO = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMRNNO")).Text;
        string MergeNo = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMergeNo")).Text;
        string PalletCode = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtPalletCode")).Text;
        string NoOfPallet = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtNoOfPallet")).Text;

        string URL = "../Reports/Fiber_Search_Pallet_Return_Report.aspx?ChallanNO=" + ChallanNO + "&ChallanDate=" + ChallanDate + "&PartyCode=" + PartyCode + "&Merge=" + Merge + "&PalletCode=" + PalletCode + "&NoOfPallet=" + NoOfPallet;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
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



        string ChallanNO = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtChallanNO")).Text;
        string ChallanDate = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtChallanDate")).Text;
        string PartyCode = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtPartyCode")).Text;
        string Merge = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMerge")).Text;
        string MRNNO = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMRNNO")).Text;
        string MergeNo = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMergeNo")).Text;
        string PalletCode = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtPalletCode")).Text;
        string NoOfPallet = ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtNoOfPallet")).Text;




        DataTable dt = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetUserQuery(ChallanNO, ChallanDate, PartyCode,  Merge, MRNNO, MergeNo,PalletCode, NoOfPallet);
        if (dt.Rows.Count > 0)
        {

            grdsearchpalletreturn.DataSource = dt;
            grdsearchpalletreturn.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            grdsearchpalletreturn.Visible = true;

        }
        AutofillSearchContent(ChallanNO, ChallanDate, PartyCode, Merge, MRNNO, MergeNo, PalletCode, NoOfPallet);



    }
    private void AutofillSearchContent(string ChallanNO, string ChallanDate, string PartyCode, string Merge, string MRNNO, string MergeNo, string PalletCode, string NoOfPallet)
    {
        try
        {


            ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtChallanNO")).Text = ChallanNO;
            ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtChallanDate")).Text = ChallanDate;
            ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtPartyCode")).Text = PartyCode;
            ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMerge")).Text = Merge;
            ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMRNNO")).Text = MRNNO;
            ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtMergeNo")).Text = MergeNo;
            ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtPalletCode")).Text = PalletCode;
            ((TextBox)grdsearchpalletreturn.HeaderRow.FindControl("txtNoOfPallet")).Text = NoOfPallet;
           

        }
        catch
        {
            throw;
        }

    }
}
