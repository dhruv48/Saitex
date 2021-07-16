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
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Inventory_Queries_MaterialStockQuery : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    int balance = 0;
    string yr = string.Empty;
    string branch = string.Empty;
    string itemcode = string.Empty;
    string itemtype = string.Empty;
    string catcode = string.Empty;
    string url = string.Empty;
    string CAT_CODE = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    string sdate1 = string.Empty;
    string edate1 = string.Empty;
    string location = string.Empty;
    string store = string.Empty;
    double totalOpQty = 0;
    double totalRecQty = 0;
    double totalIssQty = 0;
    double totalBalQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!Page.IsPostBack)
            {

                Initial_Control();
                try
                {
                    Load_Stock_Data();
                    ModalProgress.Hide();
                }
                catch (Exception ex)
                {
                    CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in View Button..\r\nSee error log for detail."));
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void Initial_Control()
    {
        try
        {
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            getBranchName();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            bindyear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            bindFromToDate();
            bindItemCategory("");
            bindItemType("");
            BindDepartment();
            BindDropDown(ddlLocation);
            if (oUserLoginDetail.UserType != "SA")
            {
               // ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
            }
        }
        catch
        {
            throw;
        }
    }

    private void getBranchName()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
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
        catch
        {
            throw;
        }
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
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(brnch + " No have financial year & data .");
                getBranchName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                bindyear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                bindFromToDate();
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindFromToDate()
    {
        try
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
                        txtDate1.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                        txtDate2.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindItemCategory(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "ITEM_CATG");
            ddlItemCategory.Items.Clear();
            ddlItemCategory.DataSource = dt;
            ddlItemCategory.DataTextField = "MST_CODE";
            ddlItemCategory.DataValueField = "MST_CODE";
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void bindItemType(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "ITEM_TYPE");
            ddlItemType.Items.Clear();
            ddlItemType.DataSource = dt;
            ddlItemType.DataTextField = "MST_CODE";
            ddlItemType.DataValueField = "MST_CODE";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void Load_Stock_Data()
    {
        try
        {
            if (ddlYear.SelectedItem.Text.ToString() != "")
            {
                yr = " and A1.year='" + ddlYear.SelectedItem.Text.ToString() + "'";
            }

            sdate1 = txtDate1.Text.ToString();
            edate1 = txtDate2.Text.ToString();

            if (ddlBranch.SelectedValue.Trim() != "")
            {
                branch = " and A1.branch_code='" + ddlBranch.SelectedValue.ToString() + "'";
            }

            if (txtItemCode.SelectedValue.Trim() != "")
            {
                itemcode = " and A1.item_code='" + txtItemCode.SelectedValue.Trim() + "'";
            }

            if (ddlItemCategory.SelectedValue.Trim() != "")
            {
                catcode = " and A2.cat_code='" + ddlItemCategory.SelectedValue.Trim() + "'";
            }

            if (ddlItemType.SelectedValue.Trim() != "")
            {
                itemtype = " and A2.item_type='" + ddlItemType.SelectedValue.Trim() + "'";
            }

            if (ddlLocation.SelectedValue.Trim() != "")
            {
                location = " and A1.LOCATION='" + ddlLocation.SelectedValue.Trim() + "'";
            }

            if (ddlStore.SelectedValue.Trim() != "")
            {
                store = " and A1.STORE='" + ddlStore.SelectedValue.Trim() + "'";
            }

            DataTable Dtable = SaitexBL.Interface.Method.TX_ITEM_STOCK_DATA.Load_Stock_Data(branch, yr, itemcode, itemtype, catcode, sdate1, edate1,location,store);
            //  DataTable Dtable = SaitexBL.Interface.Method.TX_ITEM_STOCK_DATA.Load_Stock_Data(branch, yr, itemcode,itemtype, catcode, sdate1, edate1);
            //Dtable.Columns.Add(new DataColumn("CLOSING_QTY", typeof(decimal), "OPBAL_STOK + RECPT_QTY - ISSUE_QTY"));
            //Dtable.Columns.Add(new DataColumn("CLOSING_VALUE", typeof(decimal), "OPBAL_VALUE + RECPT_VALUE - ISSUE_VALUE"));
            //dt.Columns.Add(new DataColumn("ExtendedPrice", typeof(Decimal), "UnitPrice * (1 - UnitPriceDiscount) * OrderQty"));

            if (chkBalance.Checked == true)
            {
                DataView dv = new DataView(Dtable);
                dv.RowFilter = "CLOSING_QTY > 0";
                gvReportDisplayGrid.DataSource = dv;
                gvReportDisplayGrid.DataBind();
                lblTotalRecord.Text = dv.Count.ToString();
            }
            else
            {
                gvReportDisplayGrid.DataSource = Dtable;
                gvReportDisplayGrid.DataBind();
                lblTotalRecord.Text = Dtable.Rows.Count.ToString().Trim();
            }
            
        }
        catch
        {
            throw;
        }
    }


    //private DataTable Load_Stock_Data1()
    //{
    //    try
    //    {
    //        if (ddlYear.SelectedItem.Text.ToString() != "")
    //        {
    //            yr = " and A1.year='" + ddlYear.SelectedItem.Text.ToString() + "'";
    //        }

    //        sdate1 = txtDate1.Text.ToString();
    //        edate1 = txtDate2.Text.ToString();

    //        if (ddlBranch.SelectedValue.Trim() != "")
    //        {
    //            branch = " and A1.branch_code='" + ddlBranch.SelectedValue.ToString() + "'";
    //        }

    //        if (txtItemCode.SelectedValue.Trim() != "")
    //        {
    //            itemcode = " and A1.item_code='" + txtItemCode.SelectedValue.Trim() + "'";
    //        }

    //        if (ddlItemCategory.SelectedValue.Trim() != "")
    //        {
    //            catcode = " and A2.cat_code='" + ddlItemCategory.SelectedValue.Trim() + "'";
    //        }

    //        if (ddlItemType.SelectedValue.Trim() != "")
    //        {
    //            itemtype = " and A2.item_type='" + ddlItemType.SelectedValue.Trim() + "'";
    //        }

    //        if (ddlLocation.SelectedValue.Trim() != "")
    //        {
    //            location = " and A1.LOCATION='" + ddlLocation.SelectedValue.Trim() + "'";
    //        }

    //        if (ddlStore.SelectedValue.Trim() != "")
    //        {
    //            store = " and A1.STORE='" + ddlStore.SelectedValue.Trim() + "'";
    //        }

    //        DataTable Dtable = SaitexBL.Interface.Method.TX_ITEM_STOCK_DATA.Load_Stock_Data(branch, yr, itemcode, itemtype, catcode, sdate1, edate1, location, store);
    //        //  DataTable Dtable = SaitexBL.Interface.Method.TX_ITEM_STOCK_DATA.Load_Stock_Data(branch, yr, itemcode,itemtype, catcode, sdate1, edate1);
    //        //Dtable.Columns.Add(new DataColumn("CLOSING_QTY", typeof(decimal), "OPBAL_STOK + RECPT_QTY - ISSUE_QTY"));
    //        //Dtable.Columns.Add(new DataColumn("CLOSING_VALUE", typeof(decimal), "OPBAL_VALUE + RECPT_VALUE - ISSUE_VALUE"));
    //        //dt.Columns.Add(new DataColumn("ExtendedPrice", typeof(Decimal), "UnitPrice * (1 - UnitPriceDiscount) * OrderQty"));

    //        if (chkBalance.Checked == true)
    //        {
    //            DataView dv = new DataView(Dtable);
    //            dv.RowFilter = "CLOSING_QTY > 0";
    //            gvReportDisplayGrid.DataSource = dv;
    //            gvReportDisplayGrid.DataBind();
    //            lblTotalRecord.Text = dv.Count.ToString();
    //        }
    //        else
    //        {
    //            gvReportDisplayGrid.DataSource = Dtable;
    //            gvReportDisplayGrid.DataBind();
    //            lblTotalRecord.Text = Dtable.Rows.Count.ToString().Trim();
    //        }
    //        return Dtable;

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}




    private DataTable GET_MOM_DATA(string Text, string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select MST_CODE from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and MST_CODE like :SearchQuery";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, MST_NAME, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch
        {
            throw;
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_Stock_Data();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item Category Selection.\r\nSee error log for detail."));
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindFromToDate();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Year Selection.\r\nSee error log for detail."));
        }
    }

    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReportDisplayGrid.PageIndex = e.NewPageIndex;
            Load_Stock_Data();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in GridView Paging.\r\nSee error log for detail."));
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            Load_Stock_Data();
            ModalProgress.Hide();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in View Button..\r\nSee error log for detail."));
        }
    }

    protected void txtDate1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate1.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(txtDate1.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    txtDate1.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Start Date TextChanged.\r\nSee error log for detail."));
        }
    }

    protected void txtDate2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate1.Text.Trim() != string.Empty && txtDate2.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(txtDate2.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    txtDate2.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in End Date TextChanged.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            url = "../Reports/StockDetail.aspx?TRANS_YEAR=" + ddlYear.SelectedItem.Text.ToString() + "&&BRANCH_CODE=" + ddlBranch.SelectedValue.Trim() + "&&CATCODE=" + ddlItemCategory.SelectedValue.Trim() + "&&ITEM_CODE=" + txtItemCode.SelectedValue.Trim() + "&&ITEM_TYPE=" + ddlItemType.SelectedValue.Trim() + "&&SDATE=" + txtDate1.Text.ToString() + "&&EDATE=" + txtDate2.Text.ToString() + "&&balance=" + balance + "&LOCATION=" + ddlLocation.SelectedValue + "&STORE=" + ddlStore.SelectedValue;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
        }
    }

    protected void ddlItemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Load_Stock_Data();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item Type Selected Index Changed Event.\r\nSee error log for detail."));
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindyear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString())); ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            bindFromToDate();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Branch Selection.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Initial_Control();
            Response.Redirect("./MaterialStockQuery.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear..\r\nSee error log for detail."));
        }
    }

    protected void txtItemCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtItemCode.Items.Clear();
                txtItemCode.DataSource = data;
                txtItemCode.DataBind();
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

    protected void txtItemCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            Load_Stock_Data();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item Code Selection.\r\nSee error log for detail."));
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

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    protected void gvReportDisplayGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            double OpQty = 0;
            double RecQty = 0;
            double IssQty = 0;
            double BalQty = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               Label lblOpnBal = (Label)e.Row.FindControl("lblOpnBalQty");
               HyperLink hlReceiptQty = (HyperLink)e.Row.FindControl("hlReceiptQty");
               HyperLink hlIssueQty = (HyperLink)e.Row.FindControl("hlIssueQty");
               Label lblClosingBal = (Label)e.Row.FindControl("lblClosingBal");

               double.TryParse(lblOpnBal.Text,out OpQty);
               double.TryParse(hlReceiptQty.Text, out RecQty);
               double.TryParse(hlIssueQty.Text, out IssQty);
               double.TryParse(lblClosingBal.Text, out BalQty);

              totalOpQty = totalOpQty + OpQty;
              totalRecQty = totalRecQty + RecQty;
              totalIssQty= totalIssQty + IssQty;
              totalBalQty = totalBalQty + BalQty;

              lblTotalOpBalQty.Text = totalOpQty .ToString();
              lblTotalRecQty.Text = totalRecQty.ToString();
              lblTotalIssueQty.Text = totalIssQty.ToString();
              lblTotalBalanceQty.Text = totalBalQty.ToString();


            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is cancel. See Error log for detail"));
        }
    }


    private void BindDropDown(DropDownList ddl)
    {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {

            ddl.DataSource = dt;
            ddl.DataTextField = "MST_DESC";
            ddl.DataValueField = "MST_DESC";
            ddl.DataBind();
        }
        else
        {
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_DEPARTMENTNAME, oUserLoginDetail.VC_DEPARTMENTNAME));

        }
        ddl.Items.Insert(0, new ListItem("--Select--", ""));
    }
    //private void BindDepartment()
    //{
    //    try
    //    {
    //        ddlStore.Items.Clear(); 
    //        DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
    //        if (dtDepartment != null && dtDepartment.Rows.Count > 0)
    //        {
    //            ddlStore.DataSource = dtDepartment;
    //            ddlStore.DataTextField = "DEPT_NAME";
    //            ddlStore.DataValueField = "DEPT_NAME";
    //            ddlStore.DataBind();
    //        }
    //        ddlStore.Items.Insert(0,new ListItem("--Select--",""));
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}


    private void BindDepartment()
    {
        try
        {
            ddlStore.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlStore.DataSource = dt;
                ddlStore.DataTextField = "MST_CODE";
                ddlStore.DataValueField = "MST_CODE";
                ddlStore.DataBind();
            }
            ddlStore.Items.Insert(0, new ListItem("--Select--", ""));
        }
        catch
        {
            throw;
        }
    }


    //protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    //{
    //    string strFilename = "Item_Master_Query_" + DateTime.Now.ToString() + ".xls";
    //    ExporttoExcel(Load_Stock_Data1(), strFilename, "Item Master List");

    //}

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
