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
using Common;
using errorLog; 

public partial class Module_Yarn_SalesWork_Controls_Yarn_Purchase_Reg : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    DataTable dtMain = null;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControls();
                //GridYarnPurchaseReg();

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }

    private void InitialControls()
    {
        try
        {
            grd_yarnPurchase_query.Visible = false;
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            getBranchName();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            BindFromToDate();
            //BindYarntype();
            //BindYarnCat();
            bindCategory("YARN_CAT");
            bindYarnType("YARN_TYPE");
            //BindPartyCode();
            BindDropDown(ddllocation);
            BindDepartment(ddlstore);
            cmbShade.SelectedIndex = -1;
            Session["dtDelivery"] = null;
            Session["dtMain"] = null;
            DataTable dtMain = null;
            //TxtFromDate.Text = string.Empty;
            //TxtToDate.Text = string.Empty;
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void getBranchName()
    {
        try
        {


            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            ddlBranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlBranch.DataSource = dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
         
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindYear()
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
                CommonFuction.ShowMessage(" No financial Year associated with " + brnch + " branch ");
                getBranchName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                BindYear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFromToDate()
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
                        TxtFromDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                        TxtToDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void BindYarntype()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnType();
            ddlyarntype.Items.Clear();
            ddlyarntype.DataSource = dt;
            ddlyarntype.DataValueField = "YARN_TYPE";
            ddlyarntype.DataTextField = "YARN_TYPE";
            ddlyarntype.DataBind();
            ddlyarntype.Items.Insert(0, new ListItem("------SELECT------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindYarnCat()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnCate();
            ddlyarncat.Items.Clear();
            ddlyarncat.DataSource = dt;
            ddlyarncat.DataValueField = "YARN_CAT";
            ddlyarncat.DataTextField = "YARN_CAT";
            ddlyarncat.DataBind();
            ddlyarncat.Items.Insert(0, new ListItem("-------SELECT--------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bindCategory(string MST_NAME)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlyarncat.Items.Clear();
                ddlyarncat.DataSource = dt;
                ddlyarncat.DataTextField = "MST_DESC";
                ddlyarncat.DataValueField = "MST_DESC";
                ddlyarncat.DataBind();
                ddlyarncat.Items.Insert(0, new ListItem("-------SELECT--------", string.Empty));

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindYarnType(string MST_NAME)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAMEForYarnType(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                // dv.RowFilter = "MST_DESC='" + TYPE + "'";
                if (dv != null && dv.Count > 0)
                {
                    ddlyarntype.Items.Clear();
                    ddlyarntype.DataSource = dv;
                    ddlyarntype.DataTextField = "MST_CODE";
                    ddlyarntype.DataValueField = "MST_CODE";
                    ddlyarntype.DataBind();
                    ddlyarntype.Items.Insert(0, new ListItem("------SELECT------", string.Empty));
                }
                else
                {
                    ddlyarntype.Items.Clear();
                    ddlyarntype.Items.Insert(0, new ListItem("------NoItems------", "0"));

                }
            }
        }
        catch
        {
            throw;
        }

    }

    //private void BindPartyCode()
    //{
    //    try
    //    {
    //        DataTable dt = null;
    //        dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
    //        ddlpartycode.Items.Clear();
    //        ddlpartycode.DataSource = dt;
    //        ddlpartycode.DataValueField = "PRTY_CODE";
    //        ddlpartycode.DataTextField = "PRTY_NAME";
    //        ddlpartycode.DataBind();
    //        ddlpartycode.Items.Insert(0, new ListItem("------SELECT-------", ""));
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

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
            throw ex;
        }
    }

    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim().ToString() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
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
            throw ex;
        }
    }

    private void GridYarnPurchaseReg()
    {
        DateTime StDate;
        DateTime EnDate;
        string BRANCH_CODE = string.Empty;
        string YEAR = string.Empty;
        string YARN_TYPE = string.Empty;
        string YARN_CAT = string.Empty;
        string PRTY_CODE = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
        string YARN_SHADE_FAMILY = string.Empty;
        string YARN_SHADE = string.Empty;

        string YARN_CODE = string.Empty;
        try
        {
            if (ddlYear.SelectedItem.Text.ToString() != null && ddlYear.SelectedItem.Text.ToString() != string.Empty)
            {
                YEAR = ddlYear.SelectedItem.Text.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (TxtFromDate.Text.Trim().ToString() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                StDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                EnDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
            }
            else
            {
                StDate = Sdate;
                EnDate = Edate;
            }
            if (ddlyarntype.SelectedValue.ToString() != null && ddlyarntype.SelectedValue.ToString() != string.Empty)
            {
                YARN_TYPE = ddlyarntype.SelectedValue.ToString();
            }
            else
            {
                YARN_TYPE = string.Empty;
            }
            if (ddlyarncat.SelectedValue.ToString() != null && ddlyarncat.SelectedValue.ToString() != string.Empty)
            {
                YARN_CAT = ddlyarncat.SelectedValue.ToString();
            }
            else
            {
                YARN_CAT = string.Empty;
            }
            if (txtPartyCode.SelectedValue.ToString() != null && txtPartyCode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = txtPartyCode.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (ddllocation.SelectedValue.ToString() != null && ddllocation.SelectedValue.ToString() != string.Empty)
            {
                LOCATION = ddllocation.SelectedValue.ToString();
            }
            else
            {
                LOCATION = string.Empty;
            }
            if (ddlstore.SelectedValue.ToString() != null && ddlstore.SelectedValue.ToString() != string.Empty)
            {
                STORE = ddlstore.SelectedValue.ToString();
            }
            else
            {
                STORE = string.Empty;
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
            if (!string.IsNullOrEmpty(ddlYarn.SelectedText))
            {
                YARN_CODE = ddlYarn.SelectedText;
            }
            else
            {
                YARN_CODE = string.Empty;
            }

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetYrnData(YEAR, BRANCH_CODE, StDate, EnDate, YARN_TYPE, YARN_CAT, PRTY_CODE, LOCATION, STORE, YARN_SHADE_FAMILY, YARN_SHADE, YARN_CODE);
            
            DataTable dtDelivery = new DataTable();
            dtDelivery = SaitexBL.Interface.Method.YRN_IR_MST.GetYrnDeliveryData(YEAR, BRANCH_CODE, StDate, EnDate, YARN_TYPE, YARN_CAT, PRTY_CODE, LOCATION, STORE, YARN_SHADE_FAMILY, YARN_SHADE, YARN_CODE);

           
            
            if (dtDelivery.Rows.Count > 0)
            {
                if (Session["dtDelivery"] ==null)
                Session["dtDelivery"] = dtDelivery;
            
            }

            if (dt.Rows.Count > 0)
            {
                grd_yarnPurchase_query.DataSource = dt; 
                grd_yarnPurchase_query.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grd_yarnPurchase_query.Visible = true;
                Session["dtMain"] = dt;
            }
            else
            {
                grd_yarnPurchase_query.DataSource = null;
                grd_yarnPurchase_query.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


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



    protected void btngetdata_Click(object sender, EventArgs e)
    {
        GridYarnPurchaseReg();
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindYear();
            BindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void grd_yarnPurchase_query_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridYarnPurchaseReg();

            grd_yarnPurchase_query.PageIndex = e.NewPageIndex;
            grd_yarnPurchase_query.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindDropDown(DropDownList ddllocation)
    {
        try
        {
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {


                ddllocation.DataSource = dt;
                ddllocation.DataValueField = "MST_DESC";
                ddllocation.DataTextField = "MST_DESC";
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }

            else
            {
                ddllocation.DataSource = null;
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

            }
            ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
        }
        catch
        {
            throw;
        }
    }
    private void BindDepartment(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("-------SELECT--------", ""));
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
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


    protected void GridSpinningThread_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton Idn_Adj = (LinkButton)e.Row.FindControl("Idn_Adj");
                string ORDER_NO = Idn_Adj.CommandArgument;

                Label lblYarn = (Label)e.Row.FindControl("lblYarn");
                string YARN_CODE = lblYarn.Text.ToString();

                //Label YEAR = (Label)e.Row.FindControl("lblYEAR");
                //string YEAR = YEAR.ToString();

                //-----------------Delivery Date---------------------//


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

            }


            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            //    lblAmount.Text = FinalTotal.ToString();

            //}

        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Material GridRow DataBound.\r\nSee error log for detail."));
           // lblMode.Text = ex.ToString();
        }





    }



    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {   
        dtMain = (DataTable)Session["dtMain"];
        string strFilename = "Yarn_Purchase_Register_Detail" + DateTime.Now.ToString() + ".xls";
        if (dtMain != null)
            ExporttoExcel(dtMain, strFilename, "Yarn Purchase Register Detail", oUserLoginDetail.VC_COMPANYNAME);
        else
        {
            Common.CommonFuction.ShowMessage("Data not found");
        }
               
               

    }
    public static void ExporttoExcel(DataTable table, string name, string title, string companyName)
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

    

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE  UPPER (VENDOR_CAT_CODE) NOT IN ('TRANSPORTER','SPINNER','VENDOR')   and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE  UPPER (VENDOR_CAT_CODE) IN ('TRANSPORTER','SPINNER','VENDOR')  and ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode.Items.Clear();
            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            //CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();
        }

    }
    protected int GetPartyCount(string text)
    {
        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE  UPPER (VENDOR_CAT_CODE) IN ('PARTY', 'KNITTER', 'SUPPLIER','SELF')  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
}
