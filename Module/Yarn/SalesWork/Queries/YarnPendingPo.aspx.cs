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
    string pyarn_code = string.Empty;        
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string User_Code = string.Empty;
    string mth = string.Empty;
    string yr = string.Empty;
    string branch = string.Empty;
    string dept = string.Empty;
    string vendor = string.Empty;
    string url = string.Empty;
    string ITEM_CODE = string.Empty;
    string pshadecode = string.Empty;
    DataTable dtmain = null;
     
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (!Page.IsPostBack)
        {
            InitialisePage();
            Load_PO_Detail();
         
        }
    }

    private void InitialisePage()
    {
        try
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
            ddlYarn.SelectedIndex = -1;
            //txtShadcode.Text = string.Empty;
            txtPoFrom.Text = string.Empty;
            txtPoTo.Text = string.Empty;
            cmbShade.SelectedIndex = -1;
            Session["dtmain"] = null;
           

            
        }
        catch
        {
            throw;
        }
    
    }
    private void Load_PO_Detail()
    {
        DateTime StDate;
        DateTime EnDate;
        string   BRANCH_CODE = string .Empty ;
        string YEAR = string .Empty ;
        string VENDOR_CODE= string .Empty ;
        string YARN_CODE = string.Empty;
        string status = string.Empty;
        int fromPO = 0 ;
        int topo = 0;
        string YARN_SHADE_FAMILY = string.Empty;
        string YARN_SHADE = string.Empty;
        //string ShadeCode = string.Empty;
    
      
        if (ddlBranch.SelectedValue.Trim() != null && ddlBranch.SelectedValue.Trim() != string.Empty)
        {
            BRANCH_CODE = ddlBranch.SelectedValue.Trim();
        }
        else
        {
            BRANCH_CODE = string.Empty;
        }
        if (ddlVendor.SelectedValue.Trim() !=null && ddlVendor.SelectedValue.Trim()!= string .Empty  )
        {
            vendor = ddlVendor.SelectedValue.Trim();
        }
        else
        {
            vendor = string.Empty;
        }
        if (ddlYarn.SelectedValue.ToString() != null && ddlYarn.SelectedValue.ToString() != string.Empty)
        {

            YARN_CODE = ddlYarn.SelectedValue.ToString();
        }
        else
        {
            YARN_CODE = string.Empty;
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
            topo = int.Parse(txtPoTo.Text .ToString());
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
        if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
        {
            YARN_SHADE = cmbShade.SelectedValue.Trim().ToString();
        }
        else
        {
            YARN_SHADE = string.Empty;
        }
        if (!string.IsNullOrEmpty(cmbShade.SelectedText))
        {
            YARN_SHADE_FAMILY = cmbShade.SelectedText;
        }
        else
        {
            YARN_SHADE_FAMILY = string.Empty;
        }

        
        try
        {
            var dt = SaitexBL.Interface.Method.YRN_INT_MST.YarnPendingPoQuery(BRANCH_CODE, vendor, StDate, EnDate, YARN_CODE, fromPO, topo, status, YARN_SHADE_FAMILY, YARN_SHADE,oUserLoginDetail.DT_STARTDATE.Year );
            DataTable dtmain = dt;
            Session["dtmain"] = dtmain;
            DataTable dtDelivery;

             dtDelivery = SaitexBL.Interface.Method.YRN_INT_MST.YarnPendingPoDeliveryQuery(BRANCH_CODE, vendor, StDate, EnDate, YARN_CODE, fromPO, topo, status, YARN_SHADE_FAMILY, YARN_SHADE, oUserLoginDetail.DT_STARTDATE.Year);

             if (dtDelivery.Rows.Count > 0)
             {
                 Session["dtDelivery"] = null;
                 if (Session["dtDelivery"] == null)
                 {
                     Session["dtDelivery"] = dtDelivery;

                 }

             }




            gvReportDisplayGrid.DataSource = dt;
            gvReportDisplayGrid.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

            


        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

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
    //private void fillYear()
    //{
    //    for (int i = -15; i < 15; i++)
    //    {
    //        ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
    //    }
    //    // ddlYear.Items.Insert(0, new ListItem("---Select---", ""));
    //    ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();

    //}
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
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));

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
                //url = "../Reports/Pending_PO.aspx?year="+ddlYear.SelectedItem.Text.ToString()+"&branch="+ddlBranch .SelectedValue.ToString()+"&StDate="+TxtFromDate.Text.ToString()+"&EnDate="+TxtToDate.Text.ToString()+"&vendor="+ddlVendor.SelectedValue.ToString()+"&ITEM_CODE="+txtICODE.SelectedValue.ToString()+"&fromPO="+txtPoFrom.Text.Trim().ToString()+"&topo="+txtPoTo.Text.Trim().ToString()+"&status="+ddlstatus.SelectedValue.ToString();
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            
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

                Label LblYarnCode = (Label)e.Row.FindControl("LblYarnCode");
                pyarn_code = LblYarnCode.Text.Trim();

                Label LblShadeCode = (Label)e.Row.FindControl("LblShadeCode");
                pshadecode = LblShadeCode.Text.Trim();

                DataTable dtc = BindePoSupGrid(lblComp, lblBranch, lblPType, lblPO, pyarn_code, pshadecode);
                GridView grdTaxDetail = (GridView)e.Row.FindControl("grdTaxDetail");
                if (grdTaxDetail != null)
                {
                    grdTaxDetail.DataSource = dtc;
                    grdTaxDetail.DataBind();
                }

                //------------------Delivery Date-------------------//
                LinkButton Idn_Adj = (LinkButton)e.Row.FindControl("Idn_Adj");
                string ORDER_NO = Idn_Adj.CommandArgument;

               // Label LblYarnCode = (Label)e.Row.FindControl("LblYarnCode");
                String YARN_CODE = LblYarnCode.Text.ToString();
                DataTable dtItem = (DataTable)Session["dtDelivery"];

                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    DataView dv1 = new DataView(dtItem);

                    dv1.RowFilter = "ORDER_NO='" + ORDER_NO + "' and YARN_CODE='" + YARN_CODE + "'";
                    if (dv1.Count > 0)
                    {

                        GridView Idn_grid = e.Row.FindControl("Idn_grid") as GridView;
                        Idn_grid.DataSource = dv1;
                        Idn_grid.DataBind();
                    }
                }
                //-------------------------------------------------//

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Bound Event.\r\nSee error log for detail."));
        }
    }

    protected DataTable BindePoSupGrid(string lblComp, string lblBranch, string lblPType, string LblPOno, string pyarn_code , string  pShadeCode)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.YRN_INT_MST.YarnSubPendingPOQuery(lblComp, lblBranch, lblPType, lblPO, pyarn_code, pShadeCode);
            return dt;
        }
        catch
        {
            throw;
        }
    
    }


     protected void ddlYarn_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
     {
         try
         {
             DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

             // Looping through the items and adding them to the "Items" collection of the ComboBox
             if (data != null && data.Rows.Count > 0)
             {
                 ddlYarn.Items.Clear();

                 ddlYarn.DataSource = data;
                 ddlYarn.DataBind();


             }

             // Calculating the numbr of items loaded so far in the ComboBox
             e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

             // Getting the total number of items that start with the typed text
             e.ItemsCount = GetItemsCount(e.Text);
         }
         catch (Exception ex)
         {
             CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
             //lblMode.Text = ex.ToString();

         }
     }
     protected DataTable GetItems(string text, int startOffset)
     {
         try
         {
             string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
             string whereClause = string.Empty;

             if (startOffset != 0)
             {
                 whereClause += " AND YARN_code NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
             }

             string SortExpression = " ORDER BY YARN_CODE";
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
         string CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd WHERE YARN_TYPE <> 'SEWING THREAD'";
         string WhereClause = " ";
         string SortExpression = " ORDER BY YARN_CODE ";
         string SearchQuery = text.ToUpper() + "%";
         DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
         return data.Rows.Count;
     }

     //protected void Item_LOV_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
     //{
     //    try
     //    {

     //        string Description = "";
     //        string UOM = "";
     //        double CurrentStock = 0;
     //        double MinStockLevel = 0;
     //        double OpeningRate = 0;
     //        double Min_Procure_days = 0;
     //        int UniqueId = 0;
     //        if (ViewState["UniqueId"] != null)
     //            UniqueId = int.Parse(ViewState["UniqueId"].ToString());

     //        txtItemCodeLabel.Text = string.Empty;
     //        txtItemCodeLabel.Text = txtItemCode.SelectedValue.ToString().Trim();
     //        //if (!SearchItemCodeInGrid(txtItemCode.SelectedValue.Trim(), UniqueId))
     //        //{
     //        int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(txtItemCode.SelectedValue.Trim()), out Description, out UOM, out CurrentStock, out MinStockLevel, out OpeningRate, out Min_Procure_days);

     //        if (iRecordFound > 0)
     //        {
     //            txtOpeningRate.Text = OpeningRate.ToString();
     //            txtItemDesc.Text = Description;
     //            txtCurrentStock.Text = CurrentStock.ToString();
     //            //txtMinStockLevel.Text = MinStockLevel.ToString();
     //            txtUnitName.Text = UOM;
     //            txtRequestQty.Focus();
     //            lblMin_Procure_days.Text = Min_Procure_days.ToString();
     //        }
     //        else
     //        {
     //            RefreshDetailRow();
     //            txtItemCode.Focus();
     //        }
     //        //}
     //        //else
     //        //{
     //        //    CommonFuction.ShowMessage("This Yarn already included");
     //        //    txtItemCode.SelectedValue = "SELECT";
     //        //}
     //    }
     //    catch (Exception ex)
     //    {
     //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn selection.\r\nSee error log for detail."));
     //        lblMode.Text = ex.ToString();
     //    }
     //}


     protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
     {
         try
         {
             DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

             // Looping through the items and adding them to the "Items" collection of the ComboBox
             if (data != null && data.Rows.Count > 0)
             {
                 ddlYarn.Items.Clear();

                 ddlYarn.DataSource = data;
                 ddlYarn.DataBind();
             }

             // Calculating the numbr of items loaded so far in the ComboBox
             e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

             // Getting the total number of items that start with the typed text
             e.ItemsCount = GetItemsCount(e.Text);
         }
         catch (Exception ex)
         {
             CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
             //lblMode.Text = ex.ToString();

         }
     }
     
     protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
     {
         try
         {
             InitialisePage();
            
         }
         catch
         {
             throw;
         }
     }
     protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
     {
         try
         {
             DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
             // Looping through the items and adding them to the "Items" collection of the ComboBox
             if (data != null && data.Rows.Count > 0)
             {
                 cmbShade.Items.Clear();
                 cmbShade.DataSource = data;
                 cmbShade.DataBind();
             }
             // Calculating the numbr of items loaded so far in the ComboBox
             e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
             // Getting the total number of items that start with the typed text
             e.ItemsCount = GetShadeItemsCount(e.Text);
         }
         catch (Exception ex)
         {
             Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));

         }
     }

     protected DataTable GetShadeItems(string text, int startOffset)
     {
         try
         {
             string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_NAME ||'@' ||T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
             string whereClause = string.Empty;

             if (startOffset != 0)
             {
                 whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
             }
             string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
             string SearchQuery = text.ToUpper() + "%";
             return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

         }
         catch
         {
             throw;
         }
     }

     protected int GetShadeItemsCount(string text)
     {
         try
         {
             string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
             string WhereClause = " ";
             string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
             string SearchQuery = text.ToUpper() + "%";
             return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
         }
         catch
         {
             throw;
         }
     }




     private DataTable Load_PO_Detail1()
     {
         DateTime StDate;
         DateTime EnDate;
         string BRANCH_CODE = string.Empty;
         string YEAR = string.Empty;
         string VENDOR_CODE = string.Empty;
         string YARN_CODE = string.Empty;
         string status = string.Empty;
         int fromPO = 0;
         int topo = 0;
         string YARN_SHADE_FAMILY = string.Empty;
         string YARN_SHADE = string.Empty;
         //string ShadeCode = string.Empty;


         if (ddlBranch.SelectedValue.Trim() != null && ddlBranch.SelectedValue.Trim() != string.Empty)
         {
             BRANCH_CODE = ddlBranch.SelectedValue.Trim();
         }
         else
         {
             BRANCH_CODE = string.Empty;
         }
         if (ddlVendor.SelectedValue.Trim() != null && ddlVendor.SelectedValue.Trim() != string.Empty)
         {
             vendor = ddlVendor.SelectedValue.Trim();
         }
         else
         {
             vendor = string.Empty;
         }
         if (ddlYarn.SelectedValue.ToString() != null && ddlYarn.SelectedValue.ToString() != string.Empty)
         {

             YARN_CODE = ddlYarn.SelectedValue.ToString();
         }
         else
         {
             YARN_CODE = string.Empty;
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
         if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
         {
             YARN_SHADE = cmbShade.SelectedValue.Trim().ToString();
         }
         else
         {
             YARN_SHADE = string.Empty;
         }
         if (!string.IsNullOrEmpty(cmbShade.SelectedText))
         {
             YARN_SHADE_FAMILY = cmbShade.SelectedText;
         }
         else
         {
             YARN_SHADE_FAMILY = string.Empty;
         }


         //try
         //{  
             
             DataTable dt=new DataTable();
         //change by Arun Sharma


             dt = SaitexBL.Interface.Method.YRN_INT_MST.YarnPendingPoQuery(BRANCH_CODE, vendor, StDate, EnDate, YARN_CODE, fromPO, topo, status, YARN_SHADE_FAMILY, YARN_SHADE, oUserLoginDetail.DT_STARTDATE.Year);
         //Acoording to chandram
             //dt = SaitexBL.Interface.Method.YRN_INT_MST.YarnPendingPoQuery1(BRANCH_CODE, vendor, StDate, EnDate, YARN_CODE, fromPO, topo, status, YARN_SHADE_FAMILY, YARN_SHADE, oUserLoginDetail.DT_STARTDATE.Year);
             return dt;
             //dtDelivery = SaitexBL.Interface.Method.YRN_INT_MST.YarnPendingPoDeliveryQuery(BRANCH_CODE, vendor, StDate, EnDate, YARN_CODE, fromPO, topo, status, YARN_SHADE_FAMILY, YARN_SHADE, oUserLoginDetail.DT_STARTDATE.Year);

             //if (dtDelivery.Rows.Count > 0)
             //{
             //    Session["dtDelivery"] = null;
             //    if (Session["dtDelivery"] == null)
             //    {
             //        Session["dtDelivery"] = dtDelivery;

             //    }

             //}




             




         //}
         //catch (Exception ex)
         //{
         //    Common.CommonFuction.ShowMessage(ex.Message.ToString());
         //}

     }





     protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
     {
         DataTable dtDelivery =(DataTable)Session["dtDelivery"];
         
         string strFilename = "Quality_Purchase_Order_Query" + DateTime.Now.ToString() + ".xls";
         ExporttoExcel(Load_PO_Detail1(), strFilename, "Quality Purchase Order Query", oUserLoginDetail.VC_COMPANYNAME, dtDelivery);

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

                 if (i == 17)
                 {

                     DataView dv = new DataView(dtDelivery);
                     dv.RowFilter = "ORDER_NO='" + row["PO_NUMB"] + "'";

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




                     for (int x=0; x < dv.Count ; x++)
             {//write in new row
                 HttpContext.Current.Response.Write("<TR>");
                 for (int j = 0; j < 9; j++)
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


}


