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


public partial class Module_Yarn_SalesWork_Controls_YarnMasterQuery : System.Web.UI.UserControl
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
            dt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetUserQuery("", "", "", "","", "", "", "", "", "","","","","","","","");
            Session["dtBASEQUALITY"] = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetBaseQualityDetails("", "", "");
            Session["dtDISPALYQUALITY"] = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetDispalyDetails("", "", "");
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
            dt.Dispose();
            Session["dtBASEQUALITY"] = null;
            Session["dtDISPALYQUALITY"] = null;


        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdFiberMasterQuery_SelectedIndexChanged1(object sender, GridViewPageEventArgs e)
    {
        grdFiberMasterQuery.PageIndex = e.NewPageIndex;
        GetQuery();
    }
    protected void RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataView dvBASEQUALITY = new DataView();
                DataView dvDISPALYQUALITY = new DataView();
                if (Session["dtBASEQUALITY"] != null)
                {
                    dvBASEQUALITY = new DataView((DataTable)Session["dtBASEQUALITY"]);
                }
                if (Session["dtDISPALYQUALITY"] != null)
                {
                    dvDISPALYQUALITY = new DataView((DataTable)Session["dtDISPALYQUALITY"]);
                }

                GridViewRow grdRow = e.Row;
                Label lblFabCode = (Label)grdRow.FindControl("lblFabCode");

                dvBASEQUALITY.RowFilter = "YARN_CODE='" + lblFabCode.Text + "'";
                if (dvBASEQUALITY != null && dvBASEQUALITY.Count > 0)
                {
                    GridView grdBaseQuality = (GridView)grdRow.FindControl("grdBaseQuality");
                    grdBaseQuality.DataSource = dvBASEQUALITY;
                    grdBaseQuality.DataBind();
                    grdBaseQuality.Dispose();
                    dvBASEQUALITY.Dispose();
                }

                dvDISPALYQUALITY.RowFilter = "YARN_CODE='" + lblFabCode.Text + "'";
                if (dvDISPALYQUALITY != null && dvDISPALYQUALITY.Count > 0)
                {
                    GridView grdDisplayQuality = (GridView)e.Row.FindControl("grdDisplayQuality");
                    if (grdDisplayQuality != null)
                    {
                        grdDisplayQuality.DataSource = dvDISPALYQUALITY;
                        grdDisplayQuality.DataBind();
                        grdDisplayQuality.Dispose();
                        dvDISPALYQUALITY.Dispose();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));

        }
    }


    protected void FilterGrid_Click1(object sender, EventArgs e)
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



        string yarncode = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarncode")).Text;
        string yarncat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarncat")).Text;
        string yarntype = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarntype")).Text;
        string ply = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtPly")).Text;
        string colour = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtColour")).Text;
        string uom = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string yarndesc = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtYarnDesc")).Text;
        string HSNCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtHSNCODE")).Text;
        string maxstock = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtMaxStock")).Text;
        string fancyeffect = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtFancyEffect")).Text;
        string blending = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtBlending")).Text;
        //string branch = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtbranch")).Text;
 //    string CUSTOMITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
 //  string SALESITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
   string ISEXCISABLE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
 //  string TARIFFHEADING = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;
   string YARNSHADE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtYARNSHADE")).Text;
   string LOCATION = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtLOCATION")).Text;
   string STORE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSTORE")).Text;
   string RGB = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtRGB")).Text;
   string ENDUSE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtENDUSE")).Text;



   DataTable dt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetUserQuery(yarncode, yarncat, yarntype, ply, colour, uom, yarndesc,HSNCODE, maxstock, fancyeffect, blending, ISEXCISABLE,YARNSHADE,LOCATION,STORE,RGB,ENDUSE);
   Session["dtBASEQUALITY"] = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetBaseQualityDetails(yarncode, yarncat, yarntype);
   Session["dtDISPALYQUALITY"] = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetDispalyDetails(yarncode, yarncat, yarntype);

        
        if (dt.Rows.Count > 0)
        {

            grdFiberMasterQuery.DataSource = dt;
            grdFiberMasterQuery.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            grdFiberMasterQuery.Visible = true;

        }

        AutofillSearchContent(yarncode, yarncat, yarntype, ply, colour, uom, yarndesc,HSNCODE, maxstock, fancyeffect, blending, ISEXCISABLE, YARNSHADE, LOCATION, STORE, RGB, ENDUSE);

        Session["dtBASEQUALITY"] = null;
        Session["dtDISPALYQUALITY"] = null;

    }

    private void AutofillSearchContent(string yarncode, string yarncat, string yarntype, string ply, string colour, string uom, string yarndesc,string HSNCODE, string maxstock, string fancyeffect, string blending,  string ISEXCISABLE, string YARNSHADE,string LOCATION,string STORE,string RGB,string ENDUSE)
    {
        try
        {

            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarncode")).Text = yarncode;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarncat")).Text = yarncat;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarntype")).Text = yarntype;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtPly")).Text = ply;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtColour")).Text = colour;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtUOM")).Text = uom;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtYarnDesc")).Text = yarndesc;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtHSNCODE")).Text = HSNCODE;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtMaxStock")).Text = maxstock;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtFancyEffect")).Text = fancyeffect;
            ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtBlending")).Text = blending;
     // ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text = CUSTOMITCHSCODE;
     // ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text = SALESITCHSCODE;
      ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text = ISEXCISABLE;
      ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text = ISEXCISABLE;
    //  ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text = TARIFFHEADING;
      ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtYARNSHADE")).Text = YARNSHADE;
      ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtLOCATION")).Text = LOCATION;
      ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSTORE")).Text = STORE;
      ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtRGB")).Text = RGB;
      ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtENDUSE")).Text = ENDUSE;
        }
        catch
        {
            throw;
        }

    }
    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/YarnMaster.aspx", false);
    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        string yarncode = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarncode")).Text;
        string yarncat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarncat")).Text;
        string yarntype = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarntype")).Text;
        string ply = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtPly")).Text;
        string colour = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtColour")).Text;
        string uom = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string yarndesc = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtYarnDesc")).Text;
        string HSNCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtHSNCODE")).Text;
        string maxstock = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtMaxStock")).Text;
        string fancyeffect = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtFancyEffect")).Text;
        string blending = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtBlending")).Text;
     //   string CUSTOMITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
    //    string SALESITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
        string ISEXCISABLE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
     //   string TARIFFHEADING = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;
        string YARNSHADE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtYARNSHADE")).Text;
        string LOCATION = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtLOCATION")).Text;
        string STORE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSTORE")).Text;
        string RGB = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtRGB")).Text;
        string ENDUSE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtENDUSE")).Text;

        string URL = "../Reports/YarnMasterwiseReoprt.aspx?yarncode=" + yarncode + "&yarncat=" + yarncat + "&yarntype=" + yarntype + "&ply=" + ply + "&colour=" + colour + "&uom=" + uom + "&yarndesc=" + yarndesc + "&HSNCODE=" + HSNCODE + "&maxstock=" + maxstock + "&fancyeffect=" + fancyeffect + "&blending=" + blending +  "&ISEXCISABLE=" + ISEXCISABLE + "&YARNSHADE=" + YARNSHADE + "&LOCATION=" + LOCATION + "&STORE=" + STORE + "&RGB=" + RGB + "&ENDUSE=" + ENDUSE;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Yarn_Master_List_" + DateTime.Now.ToShortDateString() + ".xls";
        string yarncode = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarncode")).Text;
        string yarncat = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarncat")).Text;
        string yarntype = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtyarntype")).Text;
        string ply = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtPly")).Text;
        string colour = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtColour")).Text;
        string uom = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtUOM")).Text;
        string yarndesc = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtYarnDesc")).Text;
        string HSNCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtHSNCODE")).Text;
        string maxstock = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtMaxStock")).Text;
        string fancyeffect = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtFancyEffect")).Text;
        string blending = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtBlending")).Text;

 //  string CUSTOMITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtCUSTOMITCHSCODE")).Text;
 //  string SALESITCHSCODE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSALESITCHSCODE")).Text;
   string ISEXCISABLE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtISEXCISABLE")).Text;
 //  string TARIFFHEADING = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtTARIFFHEADING")).Text;
   string YARNSHADE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtYARNSHADE")).Text;
   string LOCATION = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtLOCATION")).Text;
   string STORE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtSTORE")).Text;
   string RGB = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtRGB")).Text;
   string ENDUSE = ((TextBox)grdFiberMasterQuery.HeaderRow.FindControl("txtENDUSE")).Text;

   var data = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetUserQuery(yarncode, yarncat, yarntype, ply, colour, uom, yarndesc,HSNCODE, maxstock, fancyeffect, blending, ISEXCISABLE, YARNSHADE, LOCATION, STORE, RGB, ENDUSE);
    var dtBASEQUALITY = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetBaseQualityDetails(yarncode, yarncat, yarntype);
    var dtDISPALYQUALITY = SaitexBL.Interface.Method.YARN_QUERY_MASTER.GetDispalyDetails(yarncode, yarncat, yarntype);
      ExporttoExcel(data, strFilename, "Yarn Master List",  dtBASEQUALITY , dtDISPALYQUALITY );
  // UploadDataTableToExcel(data, strFilename);
    }

    private void ExporttoExcel(DataTable table, string name, string title, DataTable dtBASEQUALITY, DataTable dtDISPALYQUALITY)
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


                //******************************************//
                if (i == 20)
                {

                    DataView dvBASEQUALITY = new DataView(dtBASEQUALITY);
                    dvBASEQUALITY.RowFilter = "YARN_CODE='" + row[0].ToString() + "'";

                    if (dvBASEQUALITY.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<Tr>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("CATEGORY");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("Yarn Code");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("Quality Description");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("UOM");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");


                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("SHADE");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("Percentage");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("</Tr>");
                        for (int j = 0; j < dvBASEQUALITY.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvBASEQUALITY.Table.Columns.Count - 1; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write(dvBASEQUALITY[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                }



                if (i == 21)
                {
                    DataView dvDISPALYQUALITY = new DataView(dtDISPALYQUALITY);
                    dvDISPALYQUALITY.RowFilter = "YARN_CODE='" + row[0].ToString() + "'";
                    if (dvDISPALYQUALITY.Count > 0)
                    {
                        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                        HttpContext.Current.Response.Write("<Tr>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("Yarn Code");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("Display Quality");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");

                        HttpContext.Current.Response.Write("</Tr>");


                        for (int j = 0; j < dvDISPALYQUALITY.Count; j++)
                        {
                            HttpContext.Current.Response.Write("<Tr>");
                            for (int i1 = 0; i1 < dvDISPALYQUALITY.Table.Columns.Count - 1; i1++)
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write(dvDISPALYQUALITY[j][i1].ToString());
                                HttpContext.Current.Response.Write("</Td>");

                            }
                            HttpContext.Current.Response.Write("</Tr>");
                        }
                        HttpContext.Current.Response.Write("</Table>");
                    }
                }
                //***********************************************//   
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");

            }

            HttpContext.Current.Response.Write("</TR>");


        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
        //  HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void grdFiberMasterQuery_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void FilterGrid_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void grUserMasterQuery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      //  GetQuery();
        
        grdFiberMasterQuery.PageIndex = e.NewPageIndex;
        grdFiberMasterQuery.DataBind();
        SearchbyKeywords();
    }
    protected void grdFiberMasterQuery_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}
