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
//using Microsoft.Office.Interop.Excel
//using Excel = Microsoft.Office.Interop.Excel;

public partial class Module_Fiber_Controls_FiberMasterQuery : System.Web.UI.UserControl
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
               // BindControls();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindControls()
    {
        try
        {
           



           // GetBranchName();
          //  GetFiberCat();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //private void GetBranchName()
    //{
    //    try
    //    {
    //        DataTable dt = null;
    //        dt = new DataTable();
    //        string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
    //        dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
    //        DataView Dv = new DataView(dt);
    //        ddlBranchCode.DataSource = Dv;
    //        ddlBranchCode.DataValueField = "BRANCH_CODE";
    //        ddlBranchCode.DataTextField = "BRANCH_NAME";
    //        ddlBranchCode.DataBind();
    //        ddlBranchCode.Items.Insert(0, new ListItem("---------------All---------------", string.Empty));
    //        dt.Dispose();
    //        dt = null;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //private void GetFiberCat()
    //{
    //    try
    //    {
    //        DataTable dt = null;
    //        dt = new DataTable();
    //        string Mst_Name = "FIBER_MASTER";
    //        dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetFiberCat(Mst_Name);
    //        DataView Dv = new DataView(dt);
    //        ddlFiberCat.DataSource = Dv;
    //        ddlFiberCat.DataValueField = "MST_CODE";
    //        ddlFiberCat.DataTextField = "MST_CODE";
    //        ddlFiberCat.DataBind();
    //        ddlFiberCat.Items.Insert(0, new ListItem(".............ALL..............", string.Empty));
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
  //  }
    protected void GetQuery()
    {
    try
        {


            
       
	
        //if (ddlBranchCode.SelectedValue.ToString() != null && ddlBranchCode.SelectedValue.ToString() != string.Empty)
        //{
        //    BRANCH_CODE = ddlBranchCode.SelectedValue.ToString();
        //}
        //else
        //{
        //    BRANCH_CODE = string.Empty;
        //}
        //if (ddlFiberCat.SelectedValue.ToString() != null && ddlFiberCat.SelectedValue.ToString() != string.Empty)
        //{
        //    FIBER_CAT = ddlFiberCat.SelectedValue.ToString();
        //}
        //else
        //{
        //    FIBER_CAT = string.Empty;
        //}


            
 
            //string poydesc = string.Empty;
            //string poycat = string.Empty;
            //string poysubcat = string.Empty;
            //string filament = string.Empty;
            //string lusture = string.Empty;
            //string denier = string.Empty;
            //string openrate = string.Empty;
            //string maximumstock = string.Empty;
            //string remarks = string.Empty;
            //string partyname = string.Empty;
            //string branch = string.Empty;

    


     
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetUserQuery("", "", "", "", "", "", "", "", "", "", "","","","","");
            if (dt.Rows.Count > 0)
            {

                grdFiberMasterQuery.DataSource = dt;
                grdFiberMasterQuery.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grdFiberMasterQuery.Visible = true;
            }
            else
            {
                grdFiberMasterQuery.DataSource = null;
                grdFiberMasterQuery.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }



        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void SearchbyKeywords()
    {



        string poydesc = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoydesc")).Text;
        string poycat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoycat")).Text;
        string poysubcat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoysubcat")).Text;
        string filament = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtfilament")).Text;
        string lusture = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtlusture")).Text;
        string denier = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtdenier")).Text;
        string openrate = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtopenrate")).Text;
        string maximumstock = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtmaximumstock")).Text;
        string remarks = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtremarks")).Text;
        string partyname = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpartyname")).Text;
        string branch = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtbranch")).Text;

   string CUSTOMITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
   string SALESITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
   string ISEXCISABLE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
   string TARIFFHEADING = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;



        DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetUserQuery(poydesc, poycat, poysubcat, filament, lusture, denier, openrate, maximumstock, remarks, partyname, branch, CUSTOMITCHSCODE, SALESITCHSCODE, ISEXCISABLE, TARIFFHEADING);
         if (dt.Rows.Count > 0)
         {

             grdFiberMasterQuery.DataSource = dt;
             grdFiberMasterQuery.DataBind();
             lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
             grdFiberMasterQuery.Visible = true;
             
         }
         AutofillSearchContent(poydesc, poycat, poysubcat, filament, lusture, denier, openrate, maximumstock, remarks, partyname, branch, CUSTOMITCHSCODE, SALESITCHSCODE, ISEXCISABLE, TARIFFHEADING);
         
        
    
    }
    private void AutofillSearchContent(string poydesc, string poycat, string poysubcat, string filament, string lusture, string denier, string openrate, string maximumstock, string remarks, string partyname, string branch, string CUSTOMITCHSCODE, string SALESITCHSCODE, string ISEXCISABLE, string TARIFFHEADING)
    {
        try
        {

          
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoydesc")).Text = poydesc;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoycat")).Text =  poycat; 
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoycat")).Text = poysubcat;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtfilament")).Text = filament;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtlusture")).Text = lusture;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtdenier")).Text = denier;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtopenrate")).Text = openrate;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtmaximumstock")).Text=maximumstock;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtremarks")).Text = remarks;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpartyname")).Text = partyname;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtbranch")).Text = branch;
     ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text = CUSTOMITCHSCODE;
     ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text = SALESITCHSCODE;
     ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text = ISEXCISABLE;
     ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text = TARIFFHEADING;


       
        
        }
        catch
        {
            throw;
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
    protected void InitialControls()
    {
        try
        {
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;

           // ddlBranchCode.SelectedIndex = -1;
           // ddlFiberCat.SelectedIndex = -1;
          //  grdFiberMasterQuery.SelectedIndex = -1;
          //  grdFiberMasterQuery.Visible = false;
            lblTotalRecord.Text = "0";
            GetQuery();
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
    { }
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
    protected void grUserMasterQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    grdFiberMasterQuery.PageIndex = e.NewPageIndex;
        //    grdFiberMasterQuery.DataBind();
        //    GetUserQuery();
        //}
        //catch
        //{
        //}
        try
        {
            GetQuery();

            grdFiberMasterQuery.PageIndex = e.NewPageIndex;
            grdFiberMasterQuery.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {


        string strFilename = "POY_Master_Report" + DateTime.Now.ToString() + ".xls";       
        string poydesc = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoydesc")).Text;
        string poycat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoycat")).Text;
        string poysubcat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoysubcat")).Text;
        string filament = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtfilament")).Text;
        string lusture = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtlusture")).Text;
        string denier = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtdenier")).Text;
        string openrate = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtopenrate")).Text;
        string maximumstock = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtmaximumstock")).Text;
        string remarks = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtremarks")).Text;
        string partyname = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpartyname")).Text;
        string branch = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtbranch")).Text;
  string CUSTOMITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
  string SALESITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
  string ISEXCISABLE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
  string TARIFFHEADING = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;

        DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetUserQuery(poydesc, poycat, poysubcat, filament, lusture, denier, openrate, maximumstock, remarks, partyname, branch, CUSTOMITCHSCODE, SALESITCHSCODE, ISEXCISABLE, TARIFFHEADING);
       
        ExporttoExcel(dt, strFilename, "P.O.Y. Master List");


    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("FIBER_MST_NEW.aspx", false);

    }
    protected void ddlBranchCode_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ddlFiberCat_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        string poydesc = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoydesc")).Text;
        string poycat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoycat")).Text;
        string poysubcat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpoysubcat")).Text;
        string filament = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtfilament")).Text;
        string lusture = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtlusture")).Text;
        string denier = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtdenier")).Text;
        string openrate = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtopenrate")).Text;
        string maximumstock = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtmaximumstock")).Text;
        string remarks = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtremarks")).Text;
        string partyname = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtpartyname")).Text;
        string branch = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtbranch")).Text;

  string CUSTOMITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
  string SALESITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
  string ISEXCISABLE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
  string TARIFFHEADING = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;
       // DataTable myDataTable = new DataTable();
       // DataColumn myDataColumn;      

   

       // myDataColumn = new DataColumn();
       // myDataColumn.DataType = Type.GetType("System.String");
       // myDataColumn.ColumnName = "FIBER_CAT";
       // myDataTable.Columns.Add(myDataColumn);



       // myDataColumn = new DataColumn();
       // myDataColumn.DataType = Type.GetType("System.String");
       // myDataColumn.ColumnName = "BRANCH_NAME";
       // myDataTable.Columns.Add(myDataColumn);

       // DataRow row;

       // row = myDataTable.NewRow();       
       //// row["FIBER_CAT"] = ddlFiberCat.Text;
       //// row["BRANCH_CODE"] = ddlBranchCode.Text;
       // myDataTable.Rows.Add(row);
       // Session["fiberreportdt"] = myDataTable;

  string URL = "../Reports/FiberMasterReport.aspx?POYDESC=" + poydesc + "&POYCAT=" + poycat + "&POYSUBCAT=" + poysubcat + "&FILAMENT=" + filament + "&LUSTURE=" + lusture + "&DENIER=" + denier + "&OPENRATE=" + openrate + "&MAXIMUMSTOCK=" + maximumstock + "&REMARKS=" + remarks + "&PARTYNAME=" + partyname + "&BRANCH=" + branch + "&CUSTOMITCHSCODE=" + CUSTOMITCHSCODE + "&SALESITCHSCODE=" + SALESITCHSCODE + "&ISEXCISABLE=" + ISEXCISABLE + "&TARIFFHEADING=" + TARIFFHEADING;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
    }


    private void ExporttoExcel(DataTable table, string name, string title)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;
        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(oUserLoginDetail.VC_COMPANYNAME);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:12.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(" " + title + " ");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD  align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("DATED:" + DateTime.Now.ToString() + "");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");


        HttpContext.Current.Response.Write("<TR>");

        foreach (DataColumn dtcol in table.Columns)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");

        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }



 
}
  

    

  



   
  

