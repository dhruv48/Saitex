using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OracleClient;
using errorLog;
using Common;

public partial class Module_Inventory_Queries_Pending_PO : System.Web.UI.Page
{
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    
    string lblComp = string.Empty;
    string lblBranch = string.Empty;
    string lblPType = string.Empty;
    string lblPO = string.Empty;
    string pitem_code = string.Empty;        
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string User_Code = string.Empty;
    string mth = string.Empty;
    string yr = string.Empty;
    string branch = string.Empty;
    string dept = string.Empty;
    string vendor = string.Empty;
    string url = string.Empty;
    string ITEM_CODE = string.Empty;
    
     
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (!Page.IsPostBack)
        {
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            //fillYear();
            getBrachName();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            bindyear();
            getVendorName();

            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));

            Load_PO_Detail();
        }
    }
    private void Load_PO_Detail()
    {
        DateTime StDate;
        DateTime EnDate;
        string   BRANCH_CODE = string .Empty ;
        string YEAR = string .Empty ;
        string VENDOR_CODE= string .Empty ;
        string ITEM_CODE = string.Empty;
        string status = string.Empty;
        int fromPO = 0 ;
        int topo = 0;
       
        ////YEAR = ddlYear.SelectedValue.Trim();
        YEAR = ddlYear.SelectedItem.ToString();
        if (ddlBranch.SelectedValue.Trim() != null && ddlBranch.SelectedValue.Trim() != string.Empty)
        {
            branch = ddlBranch.SelectedValue.Trim();
        }
        else
        {
            branch = string.Empty;
        }
        if (ddlVendor.SelectedValue.Trim() !=null && ddlVendor.SelectedValue.Trim()!= string .Empty  )
        {
            vendor = ddlVendor.SelectedValue.Trim();
        }
        else
        {
            vendor = string.Empty;
        }

        if (txtICODE.SelectedValue.ToString() != null && txtICODE.SelectedValue.ToString() != string.Empty)
        {
            ITEM_CODE = txtICODE.SelectedValue.Trim().ToString();
        }
        else
        {
            ITEM_CODE = string.Empty;
        }
        if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
        {
            StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
            EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
        }
        else
        {
            StDate = Sdate;
            EnDate = Edate;
        }
        if (txtPoFrom.Text.Trim() != string.Empty && txtPoTo.Text.Trim() != string.Empty)
        {
            fromPO = int.Parse(txtPoFrom.Text.ToString());
            topo = int.Parse(txtPoTo .Text .ToString());
        }
        else
        {
            fromPO = 0;
            topo = 0;
        }
        if (ddlstatus.SelectedValue.ToString() != string.Empty && ddlstatus.SelectedValue.ToString() != null && ddlstatus.SelectedValue.ToString() != "All")
        {
            status = ddlstatus.SelectedValue.ToString();
        }
        else
        {
            status = string.Empty;
        }
        
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.PendingPoQuery(branch, YEAR, vendor, StDate, EnDate, ITEM_CODE, fromPO, topo, status);
            gvReportDisplayGrid.DataSource = dt;
            gvReportDisplayGrid.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }


    private DataTable Load_PO_Detail1()
    {
        DateTime StDate;
        DateTime EnDate;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string VENDOR_CODE = string.Empty;
        string ITEM_CODE = string.Empty;
        string status = string.Empty;
        int fromPO = 0;
        int topo = 0;

        ////YEAR = ddlYear.SelectedValue.Trim();
        YEAR = ddlYear.SelectedItem.ToString();
        if (ddlBranch.SelectedValue.Trim() != null && ddlBranch.SelectedValue.Trim() != string.Empty)
        {
            branch = ddlBranch.SelectedValue.Trim();
        }
        else
        {
            branch = string.Empty;
        }
        if (ddlVendor.SelectedValue.Trim() != null && ddlVendor.SelectedValue.Trim() != string.Empty)
        {
            vendor = ddlVendor.SelectedValue.Trim();
        }
        else
        {
            vendor = string.Empty;
        }

        if (txtICODE.SelectedValue.ToString() != null && txtICODE.SelectedValue.ToString() != string.Empty)
        {
            ITEM_CODE = txtICODE.SelectedValue.Trim().ToString();
        }
        else
        {
            ITEM_CODE = string.Empty;
        }
        if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
        {
            StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
            EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
        }
        else
        {
            StDate = Sdate;
            EnDate = Edate;
        }
        if (txtPoFrom.Text.Trim() != string.Empty && txtPoTo.Text.Trim() != string.Empty)
        {
            fromPO = int.Parse(txtPoFrom.Text.ToString());
            topo = int.Parse(txtPoTo.Text.ToString());
        }
        else
        {
            fromPO = 0;
            topo = 0;
        }
        if (ddlstatus.SelectedValue.ToString() != string.Empty && ddlstatus.SelectedValue.ToString() != null && ddlstatus.SelectedValue.ToString() != "All")
        {
            status = ddlstatus.SelectedValue.ToString();
        }
        else
        {
            status = string.Empty;
        }

       
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.PendingPoQuery1(branch, YEAR, vendor, StDate, EnDate, ITEM_CODE, fromPO, topo, status);
            return dt;
      
        
    }



    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReportDisplayGrid.PageIndex = e.NewPageIndex;
        Load_PO_Detail();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Load_PO_Detail();
    }
   
    private void bindyear()
    {

        try
        {

            string branch = ddlBranch.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);
            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataBind();

                dt.Dispose();
                dt = null;

            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(brnch + " No have financial year & data .");
                getBrachName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                bindyear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                bindFromToDate();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void getVendorName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            DataView Dv = new DataView(dt);
            ddlVendor.DataSource = Dv;
            ddlVendor.DataValueField = "PRTY_CODE";
            ddlVendor.DataTextField = "PRTY_NAME";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, ""));
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindyear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            bindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //Load_PO_Detail();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindFromToDate();
        }
        catch
        {
            throw;
        }
        //Load_PO_Detail();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {       
                url = "../Reports/Pending_PO.aspx?year="+ddlYear.SelectedItem.Text.ToString()+"&branch="+ddlBranch .SelectedValue.ToString()+"&StDate="+TxtFromDate.Text.ToString()+"&EnDate="+TxtToDate.Text.ToString()+"&vendor="+ddlVendor.SelectedValue.ToString()+"&ITEM_CODE="+txtICODE.SelectedValue.ToString()+"&fromPO="+txtPoFrom.Text.Trim().ToString()+"&topo="+txtPoTo.Text.Trim().ToString()+"&status="+ddlstatus.SelectedValue.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, " Error"));
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
                Response.Redirect("/Saitex/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PO_Detail();
    }
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void TxtToDate_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtToDate.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void bindFromToDate()
    {
        string FIN_YEAR_CODE = ddlYear.SelectedValue.ToString();
        DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "FIN_YEAR_CODE='" + ddlYear.SelectedValue.ToString() + "'";
            if (dv.Count > 0)
            {
                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                {
                    TxtFromDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                    TxtToDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                }
            }
        }
    }
    private void getBrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            //ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtICODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtICODE.Items.Clear();
                txtICODE.DataSource = data;
                txtICODE.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
        }
    }
    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_TYPE, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM  as Combined FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM as Combined   FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE, ITEM_DESC, ITEM_TYPE FROM   TX_ITEM_MST WHERE      ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY ITEM_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
    protected void btnGetRecord_Click(object sender, EventArgs e)
    {
        try
        {
            Load_PO_Detail();
        }
        catch
        {
            throw;
        }
    }
    protected void gvReportDisplayGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

       
        try
        {
         
          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCompCode = (Label)e.Row.FindControl("lblCompCode1");
                lblComp = lblCompCode.Text.Trim();

                Label lblBranchCode = (Label)e.Row.FindControl("lblBranchCode");
                lblBranch = lblBranchCode.Text.Trim();

                Label LblPOType = (Label)e.Row.FindControl("LblPOType");
                lblPType = LblPOType.Text.Trim();

                Label LblPOno = (Label)e.Row.FindControl("LblPOno");
                lblPO = LblPOno.Text.Trim();

                Label LblItemCode = (Label)e.Row.FindControl("LblItemCode");
                pitem_code = LblItemCode.Text.Trim();


                DataTable dtc = BindePoSupGrid(lblComp, lblBranch, lblPType, lblPO, pitem_code);
                GridView grdTaxDetail = (GridView)e.Row.FindControl("grdTaxDetail");
                if (grdTaxDetail != null)
                {
                    grdTaxDetail.DataSource = dtc;
                    grdTaxDetail.DataBind();
                }


                LinkButton Idn_Adjd = (LinkButton)e.Row.FindControl("Idn_Adjd");
                string ARTICLE_NO = Idn_Adjd.CommandArgument;
                int YEAR=int.Parse(oUserLoginDetail.DT_STARTDATE.Year.ToString());
                DataTable dtDel = BindDeliveryDate(YEAR, lblPType,int.Parse(lblPO), lblComp, lblBranch);

                

                if (dtDel != null && dtDel.Rows.Count > 0)
                {
                    DataView dv1 = new DataView(dtDel);

                    dv1.RowFilter = "PO_NUMB='" + lblPO + "' and ITEM_CODE='" + ARTICLE_NO + "'";
                    if (dv1.Count > 0)
                    {

                        GridView Idn_gridd = e.Row.FindControl("Idn_gridd") as GridView;
                        Idn_gridd.DataSource = dv1;
                        Idn_gridd.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Bound Event.\r\nSee error log for detail."));
        }
    }
    
     protected  DataTable  BindePoSupGrid(string lblComp,string lblBranch,string lblPType,string LblPOno ,string  pitem_code)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.SubPendingPOQuery(lblComp, lblBranch, lblPType, lblPO, pitem_code);
            return dt;
        }
        catch
        {
            throw;
        }
    
    }

     protected DataTable BindDeliveryDate(int YEAR, string lblPType, int PO_NUMB, string lblComp, string lblBranch)
    {
        try
        {

            DataTable dt = SaitexDL.Interface.Method.Material_Purchase_Order.GetDeliveryDate(YEAR, lblPType, PO_NUMB, lblComp, lblBranch);
            return dt;
        }
        catch
        {
            throw;
        }
    
    }





     protected DataTable BindDeliveryDate1(int YEAR, string PoFrom, string POTo, string ITEM_CODE, string COMP_CODE, string BRANCH_CODE)
     {
         try
         {

             DataTable dt = SaitexDL.Interface.Method.Material_Purchase_Order.BindDeliveryDate1(YEAR, PoFrom, POTo, ITEM_CODE, COMP_CODE, BRANCH_CODE);
             return dt;
         }
         catch
         {
             throw;
         }

     }


     protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
     {
         int YEAR=int.Parse(ddlYear.SelectedItem.Text.ToString());

         string PoFrom=txtPoFrom.Text.ToString();
         string POTo = txtPoTo.Text.ToString();
         string ITEM_CODE = txtICODE.SelectedText.ToString();
         DataTable dtDel = BindDeliveryDate1(YEAR, PoFrom,POTo,ITEM_CODE, oUserLoginDetail.COMP_CODE.ToString(), oUserLoginDetail.CH_BRANCHCODE.ToString());

         string strFilename = "Purchase_Register_Detail_" + DateTime.Now.ToShortDateString() + ".xls";
         ExporttoExcel(Load_PO_Detail1(), strFilename, "Purchase Register Detail", oUserLoginDetail.VC_COMPANYNAME, dtDel);

     }

    


     public static void ExporttoExcel(DataTable table, string name, string title, string companyName, DataTable dtDelivery)
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
         columnscount += 1;
         HttpContext.Current.Response.Write("<TR>");
         HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
         HttpContext.Current.Response.Write("<B>");
         HttpContext.Current.Response.Write(companyName);
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


         HttpContext.Current.Response.Write("<Td align='center'>");
         HttpContext.Current.Response.Write("<B>");
         HttpContext.Current.Response.Write("DELIVERY DETAILS");
         HttpContext.Current.Response.Write("</B>");
         HttpContext.Current.Response.Write("</Td>");

         HttpContext.Current.Response.Write("</TR>");
         foreach (DataRow row in table.Rows)
         {//write in new row
             HttpContext.Current.Response.Write("<TR>");
             for (int i = 0; i < table.Columns.Count; i++)
             {
                 HttpContext.Current.Response.Write("<Td>");
                 HttpContext.Current.Response.Write(row[i].ToString());
                 HttpContext.Current.Response.Write("</Td>");

                 if (i == 14)
                 {

                     DataView dv = new DataView(dtDelivery);
                     dv.RowFilter = "PO_NUMB='" + row["PO_NUMB"] + "'and ITEM_CODE='" + row["ITEM_CODE"] + "'";

                     HttpContext.Current.Response.Write("<Td>");

                     HttpContext.Current.Response.Write("<table border='1'>");

                     HttpContext.Current.Response.Write("<TR bgcolor='Yellow'>");

                     foreach (DataColumn dtcol in dtDelivery.Columns)
                     {
                         HttpContext.Current.Response.Write("<Td>");
                         HttpContext.Current.Response.Write("<B>");
                         HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                         HttpContext.Current.Response.Write("</B>");
                         HttpContext.Current.Response.Write("</Td>");

                     }
                     HttpContext.Current.Response.Write("</TR>");




                     for (int x = 0; x < dv.Count; x++)
                     {//write in new row
                         HttpContext.Current.Response.Write("<TR>");
                         for (int j = 0; j < 4; j++)
                         {
                             HttpContext.Current.Response.Write("<Td>");
                             HttpContext.Current.Response.Write(dv[x][j].ToString());
                             HttpContext.Current.Response.Write("</Td>");
                         }

                         HttpContext.Current.Response.Write("</TR>");
                     }
                     HttpContext.Current.Response.Write("</table>");
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




    //private void ExporttoExcel(DataTable table, string name, string title)
    //{
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.ClearContent();
    //    HttpContext.Current.Response.ClearHeaders();
    //    HttpContext.Current.Response.Buffer = true;
    //    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
    //    HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
    //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

    //    HttpContext.Current.Response.Charset = "utf-8";
    //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
    //    //sets font
    //    HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
    //    HttpContext.Current.Response.Write("<BR><BR><BR>");
    //    //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
    //    HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
    //      "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
    //      "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
    //    //am getting my grid's column headers
    //    int columnscount = table.Columns.Count;
    //    HttpContext.Current.Response.Write("<TR>");
    //    HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
    //    HttpContext.Current.Response.Write("<B>");
    //    HttpContext.Current.Response.Write(oUserLoginDetail.VC_COMPANYNAME);
    //    HttpContext.Current.Response.Write("</B>");
    //    HttpContext.Current.Response.Write("</TD>");
    //    HttpContext.Current.Response.Write("</TR>");

    //    HttpContext.Current.Response.Write("<TR>");
    //    HttpContext.Current.Response.Write("<TD style='font-size:12.0pt;' align='center' colspan=" + columnscount + ">");
    //    HttpContext.Current.Response.Write("<B>");
    //    HttpContext.Current.Response.Write(" " + title + " ");
    //    HttpContext.Current.Response.Write("</B>");
    //    HttpContext.Current.Response.Write("</TD>");
    //    HttpContext.Current.Response.Write("</TR>");

    //    HttpContext.Current.Response.Write("<TR>");
    //    HttpContext.Current.Response.Write("<TD  align='center' colspan=" + columnscount + ">");
    //    HttpContext.Current.Response.Write("<B>");
    //    HttpContext.Current.Response.Write("DATED:" + DateTime.Now.ToString() + "");
    //    HttpContext.Current.Response.Write("</B>");
    //    HttpContext.Current.Response.Write("</TD>");
    //    HttpContext.Current.Response.Write("</TR>");


    //    HttpContext.Current.Response.Write("<TR>");

    //    foreach (DataColumn dtcol in table.Columns)
    //    {
    //        HttpContext.Current.Response.Write("<Td>");
    //        HttpContext.Current.Response.Write("<B>");
    //        HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
    //        HttpContext.Current.Response.Write("</B>");
    //        HttpContext.Current.Response.Write("</Td>");

    //    }
    //    HttpContext.Current.Response.Write("</TR>");
    //    foreach (DataRow row in table.Rows)
    //    {//write in new row
    //        HttpContext.Current.Response.Write("<TR>");
    //        for (int i = 0; i < table.Columns.Count; i++)
    //        {
    //            HttpContext.Current.Response.Write("<Td>");
    //            HttpContext.Current.Response.Write(row[i].ToString());
    //            HttpContext.Current.Response.Write("</Td>");
    //        }

    //        HttpContext.Current.Response.Write("</TR>");
    //    }
    //    HttpContext.Current.Response.Write("</Table>");
    //    HttpContext.Current.Response.Write("</font>");
    //    HttpContext.Current.Response.Flush();
    //    HttpContext.Current.Response.End();
    //}



}


