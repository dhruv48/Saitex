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

public partial class Module_Inventory_Controls_ITEM_MATER_QUERY : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    //int  STATUS = 1;
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
            //btnITCHS.Visible = true;


           
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
             dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetUserQuery("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
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
      

        string CatCode = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCatCode")).Text;
        string ItemCode = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemCode")).Text;
        string ItemType = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemType")).Text;
         string ItemDesc = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemDesc")).Text;
         string HSNCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtHSNCODE")).Text;
        string ItemMake = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemMake")).Text;
        string UOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string OpeningBal = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtOpeningBal")).Text;
        string OpeningRate = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtOpeningRate")).Text;
        string MinStLeveL = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMinStLeveL")).Text;
        string ExpireDays = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtExpireDays")).Text;
        string Department = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtDepartment")).Text;
        string ReorderQty = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtReorderQty")).Text;
        string Rack = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtRack")).Text;
        string MaxStLeveL = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMaxStLeveL")).Text;
        string CONSUME = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCONSUME")).Text;
        string MinProcure = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMinProcure")).Text;
        string QC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC")).Text;
        string REMARKS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtREMARKS")).Text;
        string STATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;
  // string CUSTOMITCHSCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
 //  string SALESITCHSCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
   string ISEXCISABLE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
 //  string TARIFFHEADING = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;
   string ISMOVABLE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtISMOVABLE")).Text;
   string ITEMSIZE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEMSIZE")).Text;
   string WEIGHT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtWEIGHT")).Text;



        //if ( chk =="open")
        //{
        //  int  STATUS = 1 ;
        //}
        //else
        //{
        //   int  STATUS = 2;
        
        //}
        string ReOrderl = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtReOrderl")).Text;





        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetUserQuery(CatCode, ItemCode, ItemType, ItemDesc,HSNCODE, ItemMake, UOM, OpeningBal, OpeningRate, MinStLeveL, ExpireDays, Department, ReorderQty, Rack, MaxStLeveL, CONSUME, MinProcure, QC, REMARKS, STATUS, ISEXCISABLE,ISMOVABLE,ITEMSIZE,WEIGHT);
        if (dt.Rows.Count > 0)
        {

            grdITEMMasterQuery.DataSource = dt;
            grdITEMMasterQuery.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            grdITEMMasterQuery.Visible = true;
           // btnITCHSMasterQuery.Visible = true;

        }
        AutofillSearchContent(CatCode, ItemCode, ItemType, ItemMake, ItemDesc, HSNCODE, UOM, OpeningBal, OpeningRate, MinStLeveL, ExpireDays, Department, ReorderQty, Rack, MaxStLeveL, CONSUME, MinProcure, QC, REMARKS, STATUS, ReOrderl, ISEXCISABLE, ISMOVABLE, ITEMSIZE, WEIGHT);



    }

    private void AutofillSearchContent(string CatCode, string ItemCode, string ItemType, string ItemMake, string ItemDesc, string HSNCODE, string UOM, string OpeningBal, string OpeningRate, string MinStLeveL, string ExpireDays, string Department, string ReorderQty, string Rack, string MaxStLeveL, string CONSUME, string MinProcure, string QC, string REMARKS, string STATUS, string ReOrderl,string ISEXCISABLE, string ISMOVABLE, string ITEMSIZE, string WEIGHT)
    {
        try
        {



             ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCatCode")).Text=CatCode;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemCode")).Text= ItemCode;
             ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemType")).Text=ItemType;
             ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemMake")).Text=ItemMake;
             ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemDesc")).Text = ItemDesc;
             ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtHSNCODE")).Text = HSNCODE;
             ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text=UOM;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtOpeningBal")).Text=OpeningBal;
          ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtOpeningRate")).Text=OpeningRate;
          ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMinStLeveL")).Text=MinStLeveL;
           ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtExpireDays")).Text=ExpireDays;
           ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtDepartment")).Text=Department;
           ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtReorderQty")).Text = ReorderQty;
          ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtRack")).Text=Rack;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMaxStLeveL")).Text=MaxStLeveL;
           ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCONSUME")).Text=CONSUME;
          ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMinProcure")).Text=MinProcure;
           ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC")).Text=QC;
           ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtREMARKS")).Text=REMARKS;
            //((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text=STATUS;
            ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtReOrderl")).Text=ReOrderl;
       // ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text = CUSTOMITCHSCODE;
       // ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text = SALESITCHSCODE;
        ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text = ISEXCISABLE;
       // ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text = TARIFFHEADING;
        ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtISMOVABLE")).Text = ISMOVABLE;
        ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEMSIZE")).Text = ITEMSIZE;
        ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtWEIGHT")).Text = WEIGHT;

        }
        catch
        {
            throw;
        }

    }
    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/ItemMaster.aspx");
    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {

        string CatCode = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCatCode")).Text;
        string ItemCode = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemCode")).Text;
        string ItemType = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemType")).Text;
        string ItemDesc = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemDesc")).Text;
        string HSNCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtHSNCODE")).Text;
        string ItemMake = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemMake")).Text;
        string UOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string OpeningBal = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtOpeningBal")).Text;
        string OpeningRate = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtOpeningRate")).Text;
        string MinStLeveL = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMinStLeveL")).Text;
        string ExpireDays = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtExpireDays")).Text;
        string Department = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtDepartment")).Text;
        string ReorderQty = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtReorderQty")).Text;
        string Rack = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtRack")).Text;
        string MaxStLeveL = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMaxStLeveL")).Text;
        string CONSUME = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCONSUME")).Text;
        string MinProcure = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMinProcure")).Text;
        string QC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC")).Text;
        string REMARKS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtREMARKS")).Text;
        string STATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;
        string ReOrderl = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtReOrderl")).Text;

    //string CUSTOMITCHSCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
    //string SALESITCHSCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
    string ISEXCISABLE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
 //   string TARIFFHEADING = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;
    string ISMOVABLE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtISMOVABLE")).Text;
    string ITEMSIZE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEMSIZE")).Text;
    string WEIGHT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtWEIGHT")).Text;



    string URL = "../Reports/ITEM_MATER_QUERY_REPORT.aspx?CatCode=" + CatCode + "&ItemCode=" + ItemCode + "&ItemType=" + ItemType + "&ItemDesc=" + ItemDesc + "&HSNCODE=" + HSNCODE + "&ItemMake=" + ItemMake + "&UOM=" + UOM + "&MinStLeveL=" + MinStLeveL + "&OpeningRate=" + OpeningRate + "&Department=" + Department + "&ReorderQty=" + ReorderQty + "&OpeningBal=" + OpeningBal + "&Rack=" + Rack + "&MaxStLeveL=" + MaxStLeveL + "&CONSUME=" + CONSUME + "&MinProcure=" + MinProcure + "&QC=" + QC + "&REMARKS=" + REMARKS + "&STATUS=" + STATUS + "&ReOrderl=" + ReOrderl +  "&ISEXCISABLE=" + ISEXCISABLE +  "&ISMOVABLE=" + ISMOVABLE + "&ITEMSIZE=" + ITEMSIZE + "&WEIGHT=" + WEIGHT;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
    

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string strFilename = "Item_Master_List_" + DateTime.Now.ToShortDateString() + ".xls";
        string CatCode = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCatCode")).Text;
        string ItemCode = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemCode")).Text;
        string ItemType = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemType")).Text;
        string ItemDesc = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemDesc")).Text;
        string HSNCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtHSNCODE")).Text;
        string ItemMake = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtItemMake")).Text;
        string UOM = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string OpeningBal = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtOpeningBal")).Text;
        string OpeningRate = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtOpeningRate")).Text;
        string MinStLeveL = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMinStLeveL")).Text;
        string ExpireDays = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtExpireDays")).Text;
        string Department = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtDepartment")).Text;
        string ReorderQty = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtReorderQty")).Text;
        string Rack = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtRack")).Text;
        string MaxStLeveL = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMaxStLeveL")).Text;
        string CONSUME = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCONSUME")).Text;
        string MinProcure = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtMinProcure")).Text;
        string QC = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtQC")).Text;
        string REMARKS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtREMARKS")).Text;
        string STATUS = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSTATUS")).Text;
        //string ReOrder1 = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtReOrder1")).Text;
  // string CUSTOMITCHSCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
  // string SALESITCHSCODE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
   string ISEXCISABLE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
 //  string TARIFFHEADING = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;
   string ISMOVABLE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtISMOVABLE")).Text;
   string ITEMSIZE = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtITEMSIZE")).Text;
   string WEIGHT = ((TextBox)grdITEMMasterQuery.HeaderRow.FindControl("txtWEIGHT")).Text;





        var data = SaitexBL.Interface.Method.TX_ITEM_MST.GetUserQuery(CatCode, ItemCode, ItemType, ItemDesc,HSNCODE, ItemMake, UOM, OpeningBal, OpeningRate, MinStLeveL, ExpireDays, Department, ReorderQty, Rack, MaxStLeveL, CONSUME, MinProcure, QC, REMARKS, STATUS, ISEXCISABLE,ISMOVABLE,ITEMSIZE,WEIGHT);
        ExporttoExcel(data, strFilename, "Item Master List");
       


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

            if (dtcol.ColumnName == "ITEM_MAKE")
            {
               HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("ITEM_MAKE", "COLORS"));
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
            
            }
            else if (dtcol.ColumnName == "IS_EXCISABLE")
            {
            }

            else if (dtcol.ColumnName == "IS_MOVABLE")
            {
             
            }

            else{
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
            }

        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (i == 23 || i==24) 
                {
                
                }
               
                else
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
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
    protected void grdITEMMasterQuery_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
