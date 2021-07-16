using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Common;
using errorLog;
using System.IO;
using System.Globalization;
using WCFMain;
using System.Data;

public partial class Module_List_Search_Controls_listofItemMaster : System.Web.UI.UserControl
{

    //************************* Developed by Pragya Shukla at 01/05/2015 ******************************************//


    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public double CURRENTSTOCK { get; set; }
    public string HEADER { get; set; }
    public string DATES { get; set; }
    public string QueryType { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            //lblHeader.Text = HEADER;
            lblHeader.Text = "List Of Item Master";
            bindListofItemMaster();
            controlGrid();
        }

    }

    protected void gvLogistic_PreRender(object sender, EventArgs e)
    {
        gvLogistic.UseAccessibleHeader = true;
        gvLogistic.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvLogistic_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/ITEM_Master.aspx", true);

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {


        string strFilename = "List Of Item Master_" + DateTime.Now.ToString() + ".xls";
        var ITEM_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtItemCode")).Text;
        var ITEM_DESC = ((TextBox)gvLogistic.HeaderRow.FindControl("txtItemDesc")).Text;
        var ITEM_TYPE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtItemtype")).Text;
        var ITEM_MAKE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtItemMake")).Text;
        var CAT_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtCatCode")).Text;
        var ITEM_REMARKS = ((TextBox)gvLogistic.HeaderRow.FindControl("txtRemarks")).Text;
        var UOM = ((TextBox)gvLogistic.HeaderRow.FindControl("txtUnit")).Text;
        var LAST_PO_RATE = string.Empty; //((TextBox)gvLogistic.HeaderRow.FindControl("txtlastporate")).Text;
        var CURRENTSTOCK = string.Empty; // ((TextBox)gvLogistic.HeaderRow.FindControl("txtCurrentStock")).Text;


        var data = SaitexBL.Interface.Method.LIST_SEARCH_MST.SelectItemDetailForExcelsformaterial(oUserLoginDetail.VC_DEPARTMENTCODE, oUserLoginDetail.CH_BRANCHCODE, ITEM_CODE, ITEM_DESC, ITEM_TYPE, ITEM_MAKE, CAT_CODE, ITEM_REMARKS, UOM, LAST_PO_RATE, CURRENTSTOCK);
        UploadDataTableToExcel(data, strFilename);

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/List_Search/Pages/ListofItemMaster.aspx", false);

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

    protected void FilterGrid_Click(object sender, ImageClickEventArgs e)
    {
        SearchbyKeywords();
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Export this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
        imgbtnAddNew.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Add Item ')");

    }

    private void bindListofItemMaster()
    {
        try
        {
            var dt = SaitexBL.Interface.Method.LIST_SEARCH_MST.GetListItemOfMaster(oUserLoginDetail.VC_DEPARTMENTCODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                gvLogistic.DataSource = dt;
                gvLogistic.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Item Master Details Available.";
                gvLogistic.DataSource = null;
                gvLogistic.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Item Master Details Available");
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable()
    {
        var dtLogistic = new DataTable();

        dtLogistic.Columns.Add("DEPT_CODE", typeof(string));
        dtLogistic.Columns.Add("BRANCH_CODE", typeof(string));
        dtLogistic.Columns.Add("ITEM_CODE", typeof(string));
        dtLogistic.Columns.Add("ITEM_DESC", typeof(string));
        dtLogistic.Columns.Add("ITEM_TYPE", typeof(string));
        dtLogistic.Columns.Add("ITEM_MAKE", typeof(string));
        dtLogistic.Columns.Add("CAT_CODE", typeof(string));
        dtLogistic.Columns.Add("ITEM_REMARKS", typeof(string));
        dtLogistic.Columns.Add("UOM", typeof(string));
        dtLogistic.Columns.Add("LAST_PO_RATE", typeof(double));
        dtLogistic.Columns.Add("CURRENTSTOCK", typeof(double));
        return dtLogistic;
    }

    private DataTable CreatedtForGrid()
    {
        try
        {
            var dtLogistic = new DataTable();

            dtLogistic.Columns.Add("DEPT_CODE", typeof(string));
            dtLogistic.Columns.Add("BRANCH_CODE", typeof(string));
            dtLogistic.Columns.Add("ITEM_CODE", typeof(string));
            dtLogistic.Columns.Add("ITEM_DESC", typeof(string));
            dtLogistic.Columns.Add("ITEM_TYPE", typeof(string));
            dtLogistic.Columns.Add("ITEM_MAKE", typeof(string));
            dtLogistic.Columns.Add("CAT_CODE", typeof(string));
            dtLogistic.Columns.Add("ITEM_REMARKS", typeof(string));
            dtLogistic.Columns.Add("UOM", typeof(string));
            dtLogistic.Columns.Add("LAST_PO_RATE", typeof(double));
            dtLogistic.Columns.Add("CURRENTSTOCK", typeof(double));



            return dtLogistic;
        }
        catch
        {
            throw;
        }
    }



    private void SearchbyKeywords()
    {
        try
        {

            var ITEM_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtItemCode")).Text;
            var ITEM_DESC = ((TextBox)gvLogistic.HeaderRow.FindControl("txtItemDesc")).Text;
            var ITEM_TYPE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtItemtype")).Text;
            var ITEM_MAKE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtItemMake")).Text;
            var CAT_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtCatCode")).Text;
            var ITEM_REMARKS = ((TextBox)gvLogistic.HeaderRow.FindControl("txtRemarks")).Text;
            var UOM = ((TextBox)gvLogistic.HeaderRow.FindControl("txtUnit")).Text;
            var LAST_PO_RATE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtlastporate")).Text;
            var CURRENTSTOCK = ((TextBox)gvLogistic.HeaderRow.FindControl("txtCurrentStock")).Text;

            var o = new AutoComplete();
            var dt =  o.GetItemMasterDetailsmaterialAuto(oUserLoginDetail.VC_DEPARTMENTCODE, oUserLoginDetail.CH_BRANCHCODE, ITEM_CODE, ITEM_DESC, ITEM_TYPE, ITEM_MAKE, CAT_CODE, ITEM_REMARKS, UOM, LAST_PO_RATE, CURRENTSTOCK);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvLogistic.DataSource = dt;
                gvLogistic.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }

            AutofillSearchContent(ITEM_CODE, ITEM_DESC, ITEM_TYPE, ITEM_MAKE, CAT_CODE, ITEM_REMARKS, UOM, LAST_PO_RATE, CURRENTSTOCK);

        }
        catch
        {
            throw ;
        }
    }

    private void AutofillSearchContent(string ITEM_CODE, string ITEM_DESC, string ITEM_TYPE, string ITEM_MAKE, string CAT_CODE, string ITEM_REMARKS, string UOM, string LAST_PO_RATE, string CURRENTSTOCK)
    {

        try
        {
            var tITEM_CODE = (TextBox)gvLogistic.HeaderRow.FindControl("txtItemCode");
            var tITEM_DESC = (TextBox)gvLogistic.HeaderRow.FindControl("txtItemDesc");
            var tITEM_TYPE = (TextBox)gvLogistic.HeaderRow.FindControl("txtItemtype");
            var tITEM_MAKE = (TextBox)gvLogistic.HeaderRow.FindControl("txtItemMake");
            var tCAT_CODE = (TextBox)gvLogistic.HeaderRow.FindControl("txtCatCode");
            var tITEM_REMARKS = (TextBox)gvLogistic.HeaderRow.FindControl("txtRemarks");
            var tUOM = (TextBox)gvLogistic.HeaderRow.FindControl("txtUnit");
            var tLAST_PO_RATE = (TextBox)gvLogistic.HeaderRow.FindControl("txtlastporate");
            var tCURRENTSTOCK = (TextBox)gvLogistic.HeaderRow.FindControl("txtCurrentStock");

            tITEM_CODE.Text = ITEM_CODE;
            tITEM_DESC.Text = ITEM_DESC;
            tITEM_TYPE.Text = ITEM_TYPE;
            tITEM_MAKE.Text = ITEM_MAKE;
            tCAT_CODE.Text = CAT_CODE;
            tITEM_REMARKS.Text = ITEM_REMARKS;
            tUOM.Text = UOM;
            tLAST_PO_RATE.Text = LAST_PO_RATE;
            tCURRENTSTOCK.Text = CURRENTSTOCK;
        }

        catch
        {
            throw;
        }

    }

    protected void gvLogistic_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLogistic.PageIndex = e.NewPageIndex;
        bindListofItemMaster();
    }


    private void controlGrid()
    {
        if (gvLogistic.Rows.Count > 0)
        {
            var txtVehicle = (TextBox)gvLogistic.HeaderRow.FindControl("txtVehicleNo");
            var btnVehicle = (ImageButton)gvLogistic.HeaderRow.FindControl("btnVehicleNo");




            if (HEADER == "List of item Master")
            {
                gvLogistic.HeaderRow.Cells[14].Visible = false;
                gvLogistic.HeaderRow.Cells[15].Visible = true;
                gvLogistic.HeaderRow.Cells[16].Visible = true;

            }
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




}

