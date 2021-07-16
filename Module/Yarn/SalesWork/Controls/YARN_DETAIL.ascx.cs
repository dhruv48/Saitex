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
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Yarn_SalesWork_Controls_YARN_DETAIL : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    string branch = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControl();
                GetYarnDetail();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void InitialControl()
    {
        try
        {
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);           
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            getBrachName();
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            //BindYarnCat();
            //BindYarnType();
            bindCategory("YARN_CAT");
            bindYarnType("YARN_TYPE");
            BindParty();           
            ddlyarncat.SelectedIndex = -1;
            ddlyarntype.SelectedIndex = -1;
            ddlpartycode.SelectedIndex = -1;
            GrdYarnDetail.Visible = false;
            lblTotalRecord.Text = "0";
            cmbShade.SelectedIndex = -1;
            
        }
        catch (Exception ex)
        {
            throw ex;
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
            ddlyarncat.Items.Insert(0, new ListItem("------All------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void BindYarnType()
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
            ddlyarntype.Items.Insert(0, new ListItem("------All------", string.Empty));
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
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlyarncat.Items.Clear();
                ddlyarncat.DataSource = dt;
                ddlyarncat.DataTextField = "MST_DESC";
                ddlyarncat.DataValueField = "MST_DESC";
                ddlyarncat.DataBind();
                ddlyarncat.Items.Insert(0, new ListItem("------All------", string.Empty));

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
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAMEForYarnType(MST_NAME, oUserLoginDetail.COMP_CODE);
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
                    ddlyarntype.Items.Insert(0, new ListItem("------All------", string.Empty));
                    dt.Dispose();
                    dt = null;
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
    private void BindParty()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            ddlpartycode.Items.Clear();
            ddlpartycode.DataSource = dt;
            ddlpartycode.DataValueField = "PRTY_CODE";
            ddlpartycode.DataTextField = "PRTY_NAME";
            ddlpartycode.DataBind();
            ddlpartycode.Items.Insert(0, new ListItem("------All------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetYarnDetail()
    {
        DateTime StDate;
        DateTime EnDate;
        string BRANCH_CODE = string.Empty;
        string YARN_CAT = string.Empty;
        string YARN_TYPE = string.Empty;
        string PRTY_CODE = string.Empty;        
        string ARTICLE_CODE = string.Empty;
        string SHADE_FAMILY = string.Empty;
        string SHADE = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
        try
        {
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlyarncat.SelectedValue.ToString() != null && ddlyarncat.SelectedValue.ToString() != string.Empty)
            {
                YARN_CAT = ddlyarncat.SelectedValue.ToString();
            }
            else
            {
                YARN_CAT = string.Empty;
            }
            if (ddlyarntype.SelectedValue.ToString() != null && ddlyarntype.SelectedValue.ToString() != string.Empty)
            {
                YARN_TYPE = ddlyarntype.SelectedValue.ToString();
            }
            else
            {
                YARN_TYPE = string.Empty;
            }
            if (ddlpartycode.SelectedValue.ToString() != null && ddlpartycode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddlpartycode.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (!string.IsNullOrEmpty(ddlLocation.SelectedValue))
            {
                LOCATION = ddlLocation.SelectedValue;
            }
            else
            {
                LOCATION = string.Empty;
            }

            if (!string.IsNullOrEmpty(ddlStore.SelectedValue))
            {
                STORE = ddlStore.SelectedValue;
            }
            else
            {
                STORE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
            {
                SHADE = cmbShade.SelectedValue.Trim().ToString();
            }
            else
            {
                SHADE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedText))
            {
                SHADE_FAMILY = cmbShade.SelectedText;
            }
            else
            {
                SHADE_FAMILY = string.Empty;
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

            if (!string.IsNullOrEmpty(TxtYarnCode.Text))
            {
                ARTICLE_CODE = TxtYarnCode.Text.Trim();
            }
            else 
            {
                ARTICLE_CODE = string.Empty;
            }

           var dt = SaitexBL.Interface.Method.YRN_IR_MST.GetYarnDetails(BRANCH_CODE, YARN_CAT, YARN_TYPE, PRTY_CODE, SHADE, StDate, EnDate, ARTICLE_CODE,SHADE_FAMILY,LOCATION,STORE);
           
            if (dt.Rows.Count > 0)
            {
                GrdYarnDetail.DataSource = dt;
                GrdYarnDetail.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                GrdYarnDetail.Visible = true;
            }
            else
            {
                GrdYarnDetail.DataSource = null;
                GrdYarnDetail.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item");
                lblTotalRecord.Text = "0";

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    protected void btngetdata_Click(object sender, EventArgs e)
    {
        try
        {
            GetYarnDetail();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControl();
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
    protected void GrdYarnDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetYarnDetail();

            GrdYarnDetail.PageIndex = e.NewPageIndex;
            GrdYarnDetail.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
            Common.CommonFuction.ShowMessage("Unable to Load the Next Page");
        }
    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
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
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(" No financial Year associated with " + brnch + " branch ");
                getBrachName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                BindYear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                bindFromToDate();
            }
        }
        catch (Exception ex)
        {
            throw;
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
            throw ex;
        }
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

    private void BindDropDown(DropDownList ddl)
    {
        DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            ddl.Items.Clear();
            ddl.DataSource = dt;
            ddl.DataTextField = "MST_DESC";
            ddl.DataValueField = "MST_DESC";
            ddl.DataBind();
        }
        else
        {
            ddl.DataSource = null;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

        }
        ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

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
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
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

    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Yarn_Detail_Stock_Report_Query_" + DateTime.Now.ToString() + ".xls";
        Common.CommonFuction.ExporttoExcel(GetYarnDetail1(), strFilename, "Yarn Detail Stock Report Query", oUserLoginDetail.VC_COMPANYNAME);

    }



    private DataTable GetYarnDetail1()
    {
        DateTime StDate;
        DateTime EnDate;
        string BRANCH_CODE = string.Empty;
        string YARN_CAT = string.Empty;
        string YARN_TYPE = string.Empty;
        string PRTY_CODE = string.Empty;
        string ARTICLE_CODE = string.Empty;
        string SHADE_FAMILY = string.Empty;
        string SHADE = string.Empty;
        string LOCATION = string.Empty;
        string STORE = string.Empty;
        try
        {
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlyarncat.SelectedValue.ToString() != null && ddlyarncat.SelectedValue.ToString() != string.Empty)
            {
                YARN_CAT = ddlyarncat.SelectedValue.ToString();
            }
            else
            {
                YARN_CAT = string.Empty;
            }
            if (ddlyarntype.SelectedValue.ToString() != null && ddlyarntype.SelectedValue.ToString() != string.Empty)
            {
                YARN_TYPE = ddlyarntype.SelectedValue.ToString();
            }
            else
            {
                YARN_TYPE = string.Empty;
            }
            if (ddlpartycode.SelectedValue.ToString() != null && ddlpartycode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddlpartycode.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (!string.IsNullOrEmpty(ddlLocation.SelectedValue))
            {
                LOCATION = ddlLocation.SelectedValue;
            }
            else
            {
                LOCATION = string.Empty;
            }

            if (!string.IsNullOrEmpty(ddlStore.SelectedValue))
            {
                STORE = ddlStore.SelectedValue;
            }
            else
            {
                STORE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
            {
                SHADE = cmbShade.SelectedValue.Trim().ToString();
            }
            else
            {
                SHADE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedText))
            {
                SHADE_FAMILY = cmbShade.SelectedText;
            }
            else
            {
                SHADE_FAMILY = string.Empty;
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

            if (!string.IsNullOrEmpty(TxtYarnCode.Text))
            {
                ARTICLE_CODE = TxtYarnCode.Text.Trim();
            }
            else
            {
                ARTICLE_CODE = string.Empty;
            }
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetYarnDetailsExcel(BRANCH_CODE, YARN_CAT, YARN_TYPE, PRTY_CODE, SHADE, StDate, EnDate, ARTICLE_CODE, SHADE_FAMILY, LOCATION, STORE);

            return dt;
            //if (dt.Rows.Count > 0)
            //{
            //    GrdYarnDetail.DataSource = dt;
            //    GrdYarnDetail.DataBind();
            //    lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            //    GrdYarnDetail.Visible = true;
            //}
            //else
            //{
            //    GrdYarnDetail.DataSource = null;
            //    GrdYarnDetail.DataBind();
            //    Common.CommonFuction.ShowMessage("Data not found by selected item");
            //    lblTotalRecord.Text = "0";

            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



}
