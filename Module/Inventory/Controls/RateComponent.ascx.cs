using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DBLibrary;
using Common;
using errorLog;
using System.IO;

public partial class Module_Inventory_Controls_RateComponent : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (!Page.IsPostBack)
                {
                    cmbFind.Visible = false;
                    BlankControls();
                }

                if (Convert.ToInt16(Session["saveStatus"]) == 1)
                {
                    if (Request.QueryString["cId"].ToString().Trim() == "S")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                    }
                    if (Request.QueryString["cId"].ToString().Trim() == "U")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
                    }
                    if (Request.QueryString["cId"].ToString().Trim() == "D")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
                    }
                    Session["saveStatus"] = 0;
                }
            }
            else
            {
                Response.Redirect("/Saitex/default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BlankControls()
    {
        try
        {
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            txtCompoCode.Text = "";
            txtCompoSL.Text = "";
            ddlCompoType.SelectedIndex = 0;
            lblMode.Text = "You are in Save Mode";
            GetCompoData();
            BindGrid();
            grdRateCompo.AutoPostBackOnSelect = false;
        }
        catch
        {
            throw;
        }
    }

    private void GetCompoData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_RATE_COMPONENT.Select();
            ViewState["dtCompoData"] = dt;
        }
        catch
        {
            throw;
        }
    }

    private void BindGrid()
    {
        try
        {
            grdRateCompo.DataSource = null;
            grdRateCompo.DataBind();
            DataTable dt = (DataTable)ViewState["dtCompoData"];
            if (dt != null && dt.Rows.Count > 0)
            {
                grdRateCompo.DataSource = dt;
                grdRateCompo.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void grdRateCompo_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList ar = e.RecordsCollection;
            for (int iLoop = 0; iLoop < ar.Count; iLoop++)
            {
                Hashtable ht = (Hashtable)ar[iLoop];
                string Compo_code = ht["COMPO_CODE"].ToString();
                FillDataForEdit(Compo_code);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                int iRecordFound = 0;
                SaitexDM.Common.DataModel.TX_RATE_COMPONENT_MST oTX_RATE_COMPONENT_MST = new SaitexDM.Common.DataModel.TX_RATE_COMPONENT_MST();
                oTX_RATE_COMPONENT_MST.COMPO_CODE = Common.CommonFuction.funFixQuotes(txtCompoCode.Text.ToUpper().Trim());
                oTX_RATE_COMPONENT_MST.COMPO_SL = int.Parse(txtCompoSL.Text);
                oTX_RATE_COMPONENT_MST.COMPO_TYPE = ddlCompoType.SelectedValue.Trim();
                oTX_RATE_COMPONENT_MST.STATUS = true;
                oTX_RATE_COMPONENT_MST.TUSER = oUserLoginDetail.UserCode;
                oTX_RATE_COMPONENT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_RATE_COMPONENT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_RATE_COMPONENT_MST.PRODUCT_TYPE = "MATERIAL";
                oTX_RATE_COMPONENT_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

                bool bResult = SaitexBL.Interface.Method.TX_RATE_COMPONENT.Insert(oTX_RATE_COMPONENT_MST, out iRecordFound);
                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Response.Redirect("./RateComponent.aspx?cId=S", false);
                }
                else if (iRecordFound > 0)
                {
                    Common.CommonFuction.ShowMessage("Data already exists.");

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Data saving Failed.");
                }
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            SaitexDM.Common.DataModel.TX_RATE_COMPONENT_MST oTX_RATE_COMPONENT_MST = new SaitexDM.Common.DataModel.TX_RATE_COMPONENT_MST();
            oTX_RATE_COMPONENT_MST.COMPO_CODE = Common.CommonFuction.funFixQuotes(txtCompoCode.Text.ToUpper().Trim());
            oTX_RATE_COMPONENT_MST.COMPO_SL = int.Parse(txtCompoSL.Text);
            oTX_RATE_COMPONENT_MST.COMPO_TYPE = ddlCompoType.SelectedValue.Trim();
            oTX_RATE_COMPONENT_MST.STATUS = true;
            oTX_RATE_COMPONENT_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_RATE_COMPONENT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_RATE_COMPONENT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_RATE_COMPONENT_MST.PRODUCT_TYPE = "MATERIAL";
            oTX_RATE_COMPONENT_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            bool bResult = SaitexBL.Interface.Method.TX_RATE_COMPONENT.Update(oTX_RATE_COMPONENT_MST);
            if (bResult)
            {

                Session["saveStatus"] = 1;
                Response.Redirect("./RateComponent.aspx?cId=U", false);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Data updation Failed.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            SaitexDM.Common.DataModel.TX_RATE_COMPONENT_MST oTX_RATE_COMPONENT_MST = new SaitexDM.Common.DataModel.TX_RATE_COMPONENT_MST();
            oTX_RATE_COMPONENT_MST.COMPO_CODE = Common.CommonFuction.funFixQuotes(txtCompoCode.Text.ToUpper().Trim());
            oTX_RATE_COMPONENT_MST.TUSER = oUserLoginDetail.UserCode;
            bool bResult = SaitexBL.Interface.Method.TX_RATE_COMPONENT.Delete(oTX_RATE_COMPONENT_MST);
            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./RateComponent.aspx?cId=D", false);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Data deletion Failed.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtCompoCode.Enabled = false;
            BlankControls();
            cmbFind.Visible = true;
            cmbFind.SelectedIndex = -1;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
            grdRateCompo.AutoPostBackOnSelect = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtCompoCode.Enabled = true;
            BlankControls();
            cmbFind.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbFind_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {

        try
        {

            DataTable data = GetCompoDataForLOV(e.Text.ToUpper().Trim());

            if (data != null && data.Rows.Count > 0)
            {

                cmbFind.DataTextField = "COMPO_CODE";
                cmbFind.DataValueField = "COMPO_CODE";
                cmbFind.DataSource = data;
                cmbFind.DataBind();
            }

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading dropdown.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbFind_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        txtCompoCode.Enabled = false;

        try
        {
            string compcode = cmbFind.SelectedValue.Trim();
            DataTable dt = SaitexBL.Interface.Method.TX_RATE_COMPONENT.selectcmbfind(compcode);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtCompoCode.Text = dt.Rows[0]["COMPO_CODE"].ToString();
                txtCompoSL.Text = dt.Rows[0]["COMPO_SL"].ToString();
                ddlCompoType.SelectedValue = dt.Rows[0]["COMPO_TYPE"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting dropdown.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetCompoDataForLOV(string Text)
    {
        try
        {
            string CommandText = "select COMPO_CODE,COMPO_SL,COMPO_TYPE from V_TX_RATE_COMPONENT_MST ";
            string WhereClause = " where COMPO_CODE like :SearchQuery";
            string SortExpression = " order by COMPO_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private void FillDataForEdit(string CompoCode)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dtCompoData"];
            DataView dv = new DataView(dt);
            dv.RowFilter = "COMPO_CODE='" + CompoCode + "'";
            if (dv.Count > 0)
            {
                txtCompoCode.Text = dv[0]["COMPO_CODE"].ToString();
                txtCompoSL.Text = dv[0]["COMPO_SL"].ToString();
                ddlCompoType.SelectedValue = dv[0]["COMPO_TYPE"].ToString();
                txtCompoCode.ReadOnly = true;
            }
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to delete this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ? ')");

    }

    protected void grdRateCompo_Rebind(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Rebind Event.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}