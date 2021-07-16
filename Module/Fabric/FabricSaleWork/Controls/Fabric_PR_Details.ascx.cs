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


public partial class Module_Fabric_FabricSaleWork_Controls_Fabric_PR_Details : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                Initial_Control();

            }
        }
        catch (Exception exe)
        {
            Common.CommonFuction.ShowMessage(exe.Message);
        }
    }
      public  void  Initial_Control()
        {

            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            getBranchName();
           ddl_branch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
           getYear();
           ddl_year.SelectedIndex = ddl_year.Items.IndexOf(ddl_year.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
           getFabrType();
           getFabrCode();
           getDepartment();
           ddlFabrCode.SelectedIndex = -1;
           getParty();
        }
      private void getFabrCode()
      {
          DataTable td = new DataTable();
          td = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabric();
          ddlFabrCode.DataSource = td;
          ddlFabrCode.DataTextField = "FABR_CODE";
          ddlFabrCode.DataValueField = "FABR_CODE";
          ddlFabrCode.DataBind();
          ddlFabrCode.Items.Insert(0, new ListItem("---------------------ALL------------------", ""));
          td = null;
      }
      private void getDepartment()
      {
          try
          {
              DataTable dt = new DataTable();
              dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
              ddlDepartment.DataSource = dt;
              ddlDepartment.DataValueField = "DEPT_CODE";
              ddlDepartment.DataTextField = "DEPT_NAME";
              ddlDepartment.DataBind();
              ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
              dt.Dispose();
              dt = null;
          }

          catch (Exception ex)
          {
              Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
          }
      }

      public void getFabrType()
      {
          try
          {
              ddlFabrType.Items.Clear();
              DataTable tb = new DataTable();
              tb = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricType();
              if (tb != null && tb.Rows.Count > 0)
              {
                  ddlFabrType.DataTextField = "FABR_TYPE";
                  ddlFabrType.DataValueField = "FABR_TYPE";
                  ddlFabrType.DataSource = tb;
                  ddlFabrType.DataBind();
                  ddlFabrType.Items.Insert(0, new ListItem("---------------ALL--------------------", ""));
              }
          }
          catch
          {
              throw;
          }

      }
      public void getParty()
      {
          try
          {

              ddlpartycode.Items.Clear();
              DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
              if (dt != null && dt.Rows.Count > 0)
              {
                  ddlpartycode.DataTextField = "PRTY_NAME";
                  ddlpartycode.DataValueField = "PRTY_CODE";
                  ddlpartycode.DataSource = dt;
                  ddlpartycode.DataBind();

              }

              ddlpartycode.Items.Insert(0, new ListItem("---------------All---------------", ""));
          }
          catch
          {
              throw;
          }
      }
      public void getBranchName()
      {

          try
          {
              DataTable dt = null;
              dt = new DataTable();
              string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
              dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
              DataView Dv = new DataView(dt);
                ddl_branch.DataSource = Dv;
                ddl_branch.DataValueField = "BRANCH_CODE";
                ddl_branch.DataTextField = "BRANCH_NAME";
                ddl_branch.DataBind();

              dt.Dispose();
              dt = null;

          }
          catch (Exception ex)
          {
              ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
              ErrHandler.WriteError(ex.Message);
          }
      }
      public void getYear()
      {

          try
          {

              string branch = ddl_branch.SelectedValue.ToString();
              DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);
              if (dt.Rows.Count > 0)
              {
                  ddl_year.Items.Clear();
                  ddl_year.DataSource = dt;
                  ddl_year.DataTextField = "YEAR";
                  ddl_year.DataValueField = "FIN_YEAR_CODE";
                  ddl_year.DataBind();
                  //ddl_year.Items.Insert(0, new ListItem("---------------All---------------", ""));

                  dt.Dispose();
                  dt = null;

              }
              else
              {
                  string brnch = ddl_branch.SelectedItem.Text.Trim();
                  CommonFuction.ShowMessage(brnch + " No have financial year & data .");
                  getBranchName();
                  ddl_branch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                  getYear();
                  ddl_year.SelectedIndex = ddl_year.Items.IndexOf(ddl_year.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                  gridPurchaseDetail();
              }

          }
          catch (Exception ex)
          {
              ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
              ErrHandler.WriteError(ex.Message);
          }
      }
      public void gridPurchaseDetail()
      {
          try
          {
              DateTime StDate;
              DateTime EnDate;
              string DEPT_CODE = string.Empty;
              string BRANCH_CODE = string.Empty;
              string FABR_TYPE = string.Empty;

              string FABR_CODE = string.Empty;
              string PRTY_CODE = string.Empty;
              string YEAR = string.Empty;

              if (ddlDepartment.SelectedValue.ToString() != null && ddlDepartment.SelectedValue.ToString() != string.Empty)
              {
                  DEPT_CODE = ddlDepartment.SelectedValue.ToString();
              }
              else
              {
                  DEPT_CODE = string.Empty;
              }
              if (ddl_branch.SelectedValue.ToString() != "" && ddl_branch.SelectedValue.ToString() != string.Empty)
              {
                  BRANCH_CODE = ddl_branch.SelectedValue.ToString();
              }
              else
              {
                  BRANCH_CODE = string.Empty;
              }
              if (ddl_year.SelectedValue.ToString() != "" && ddl_year.SelectedValue.ToString() != string.Empty)
              {
                  YEAR = ddl_year.SelectedValue.ToString();
              }
              else
              {
                  YEAR = string.Empty;
              }
              if (ddlFabrType.SelectedValue.ToString() != "" && ddlFabrType.SelectedValue.ToString() != string.Empty)
              {
                  FABR_TYPE = ddlFabrType.SelectedValue.ToString();
              }
              else
              {
                  FABR_TYPE = string.Empty;
              }
              if (ddlpartycode.SelectedValue.ToString() != "" && ddlpartycode.SelectedValue.ToString() != string.Empty)
              {
                  PRTY_CODE = ddlpartycode.SelectedValue.ToString();
              }
              else
              {
                  PRTY_CODE = string.Empty;
              }
              if (ddlFabrCode.SelectedValue.ToString() != "" && ddlFabrCode.SelectedValue.ToString() != string.Empty)
              {
                  FABR_CODE = ddlFabrCode.SelectedValue.ToString();
              }
              else
              {
                  FABR_CODE = string.Empty;
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
              DataTable dv = new DataTable();
              dv = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.Purchaseregisterdetail( YEAR,  BRANCH_CODE, DEPT_CODE, FABR_TYPE,  FABR_CODE,  PRTY_CODE,  StDate,  EnDate);
              if (dv.Rows.Count > 0)
              {
                  grd_PrDetail.DataSource = dv;
                  grd_PrDetail.DataBind();
                  lbl_TotalRecord.Text = dv.Rows.Count.ToString().Trim();
              }
              else
              {
                  Common.CommonFuction.ShowMessage("No Data Found");
              }
          }
          catch (Exception ex)
          {
              ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
              ErrHandler.WriteError(ex.Message);
          }

      }
    protected void Print_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void Clear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Initial_Control();
            Response.Redirect("./PurchaseRegisterDetail.aspx", false);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }
    protected void Exit_Click(object sender, ImageClickEventArgs e)
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
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            getYear();
            ddl_year.SelectedIndex = ddl_year.Items.IndexOf(ddl_year.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString())); ddl_year.SelectedIndex = ddl_year.Items.IndexOf(ddl_year.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            //TxtFromDate.Text = string.Empty;
            //TxtToDate.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
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
    //protected void txtICODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
    //        // Looping through the items and adding them to the "Items" collection of the ComboBox
    //        if (data != null && data.Rows.Count > 0)
    //        {
    //            txtICODE.Items.Clear();
    //            txtICODE.DataSource = data;
    //            txtICODE.DataBind();
    //        }
    //        // Calculating the numbr of items loaded so far in the ComboBox
    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    //        // Getting the total number of items that start with the typed text
    //        e.ItemsCount = GetItemsCount(e.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
    //    }
    //}
    //protected DataTable GetItems(string text, int startOffset)
    //{
    //    try
    //    {
    //        string CommandText = " SELECT * FROM (SELECT FABR_CODE, FABR_DESC, FABR_TYPE, FABR_CODE||'@'|| FABR_DESC||'@'||UOM  as Combined FROM TX_FABRIC_MST WHERE FABR_CODE LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY FABR_CODE) asd WHERE   ROWNUM <= 15 ";
    //        string whereClause = string.Empty;

    //        if (startOffset != 0)
    //        {
    //            whereClause += "  AND FABR_CODE NOT IN (SELECT FABR_CODE FROM (SELECT FABR_CODE, FABR_DESC, FABR_CODE||'@'|| FABR_DESC||'@'||UOM as Combined   FROM TX_FABRIC_MST WHERE FABR_CODE LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY FABR_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
    //        }

    //        string SortExpression = " ORDER BY FABR_CODE";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //protected int GetItemsCount(string text)
    //{
    //    string CommandText = " SELECT   *  FROM   (  SELECT   FABR_CODE, FABR_DESC, FABR_TYPE FROM   TX_FABRIC_MST WHERE   FABR_CODE LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY   FABR_CODE) asd ";
    //    string WhereClause = " ";
    //    string SortExpression = " ORDER BY FABR_CODE ";
    //    string SearchQuery = text.ToUpper() + "%";
    //    DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
    //    return data.Rows.Count;
    //}

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        Clear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        Print.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        Exit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private void bindFromToDate()
    {
        string FIN_YEAR_CODE = ddl_year.SelectedValue.ToString();
        DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "FIN_YEAR_CODE='" + ddl_year.SelectedValue.ToString() + "'";
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

    protected void GetReport_Click(object sender, EventArgs e)
    {
        gridPurchaseDetail();
    }
    protected void grd_PrDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_PrDetail.PageIndex = e.NewPageIndex;
        gridPurchaseDetail();

    }
    protected void grd_PrDetail_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());
                if (StartDate < Sdate || StartDate > Edate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = Sdate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }
}
