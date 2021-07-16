using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_SewingThread_Controls_CRQueryForm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    //SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY;
    private static string PRODUCT_TYPE = "SEWING THREAD";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
              
               
               // btnPrint.Attributes.Add("onclick", "javascript:CallPrint('divPrint');");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Print Mode";
            tdFind.Visible = false;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdPrint.Visible = true;
            txtCRFrom.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txtCRTo.Text = System.DateTime.Now.ToShortDateString();
            BindBranch();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            //BindShadeFamily();
            //BindShadeCode();
            //BindCRGrid();
        }
        catch
        {
            throw;
        }
    }

    private void BindBranch()
    {
        try
        {
            
            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
           
            ddlBranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlBranch.DataSource = dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--------ALL---------", ""));
            
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    //private void BindShadeFamily()
    //{
    //    try
    //    {
    //        oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    //        oOD_SHADE_FAMILY.PRODUCT_TYPE = "SEWING THREAD";
    //        oOD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;

    //        DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeFamilyCodeByProducttype(oOD_SHADE_FAMILY);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            ddlShadeFamily.Items.Clear();
    //            ddlShadeFamily.DataSource = dt;
    //            ddlShadeFamily.DataTextField = "SHADE_FAMILY_CODE";
    //            ddlShadeFamily.DataValueField = "SHADE_FAMILY_CODE";
    //            ddlShadeFamily.DataBind();
    //            ddlShadeFamily.Items.Insert(0, new ListItem("------ALL------", "0"));

    //        }
    //        else
    //        {
    //            ddlShadeFamily.Items.Clear();
    //            ddlShadeFamily.Items.Insert(0, new ListItem("NoShadeExists", "0"));
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    private void BindCRGrid()
    {
        try
        {
            string strBranch = string.Empty;
            string strParty = string.Empty;
            string strArticle = string.Empty;
            //string strShadeFamily = string.Empty;
            string strShadeCode = string.Empty;
            string DTCRFrom = string.Empty;
            string DTCRTo = string.Empty;
            string strStatus = string.Empty;
            string strCustNo = string.Empty;

            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty && ddlBranch.SelectedIndex > 0)
            {
                strBranch = ddlBranch.SelectedValue.ToString();
            }
            else
            {
                strBranch = string.Empty;
            }

            if (txtPartyCode.SelectedValue.ToString() != null && txtPartyCode.SelectedValue.ToString() != string.Empty && txtPartyCode.SelectedIndex > -1)
            {
                strParty = txtPartyCode.SelectedText.Trim();
            }
            else
            {
                strParty = string.Empty;
            }

            if (ddlArticle.SelectedValue.ToString() != null && ddlArticle.SelectedValue.ToString() != string.Empty && ddlArticle.SelectedIndex > -1)
            {
                strArticle = ddlArticle.SelectedText.Trim();
            }
            else
            {
                strArticle = string.Empty;
            }

            //if (ddlShadeFamily.SelectedValue.ToString() != null && ddlShadeFamily.SelectedValue.ToString() != string.Empty && ddlShadeFamily.SelectedIndex > 0)
            //{
            //    strShadeFamily = ddlShadeFamily.SelectedValue.ToString();
            //}
            //else
            //{
            //    strShadeFamily = string.Empty;
            //}

            if (txtCustNo.Text != null && txtCustNo.Text != string.Empty)
            {
                strCustNo = txtCustNo.Text.ToUpper().Trim();
            }
            else
            {
                strCustNo = string.Empty;
            }

            if (ddlShadeCode.SelectedValue.ToString() != null && ddlShadeCode.SelectedValue.ToString() != string.Empty && ddlShadeCode.SelectedIndex > 0)
            {
                strShadeCode = ddlShadeCode.SelectedValue.ToString();
            }
            else
            {
                strShadeCode = string.Empty;
            }

            if (txtCRFrom.Text != null && txtCRFrom.Text != string.Empty && txtCRTo.Text != null && txtCRTo.Text != string.Empty)
            {
                DTCRFrom = txtCRFrom.Text.Trim();
                DTCRTo = txtCRTo.Text.Trim();
            }
            else
            {
                DTCRFrom = string.Empty;
                DTCRTo = string.Empty;
            }

            if (ddlStatus.SelectedValue.ToString() != null && ddlStatus.SelectedValue.ToString() != string.Empty && ddlStatus.SelectedIndex > 0)
            {
                strStatus = ddlStatus.SelectedValue.ToString();
            }
            else
            {
                strStatus = string.Empty;
            }

            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetCRForSTQuery(oUserLoginDetail.COMP_CODE, strBranch, oUserLoginDetail.DT_STARTDATE.Year, strParty, strArticle, strShadeCode, DTCRFrom, DTCRTo, strStatus, strCustNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdCustomerRequest.DataSource = dt;
                grdCustomerRequest.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                grdCustomerRequest.DataSource = null;
                grdCustomerRequest.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No data found..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./CRForSWQueryForm.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void txtCRTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtCRFrom.Text == null || txtCRFrom.Text == string.Empty)
            {
                CommonFuction.ShowMessage("Please enter From CR Date first..");
            }
            else
            {
                if (DateTime.Parse(txtCRFrom.Text) > DateTime.Parse(txtCRTo.Text))
                {
                    CommonFuction.ShowMessage("Please From CR Date should not be greater than To CR Date..");
                }
                else
                {
                    //BindCRGrid();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Customer Request To Date TextBox.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlArticle_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlArticle.Items.Clear();
                ddlArticle.DataSource = data;
                ddlArticle.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM ( SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE,COLOUR , YARN_CODE||'@'|| YARN_DESC||'@'||UOM ||'@'||TRANSFER_PRICE as Combined FROM YRN_MST  WHERE YARN_CAT = 'SEWING THREAD') WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND YARN_code NOT IN (SELECT YARN_CODE FROM ( SELECT * FROM ( SELECT YARN_CODE, YARN_DESC, YARN_TYPE, COLOUR, YARN_CODE||'@'|| YARN_DESC||'@'||UOM ||'@'||TRANSFER_PRICE as Combined   FROM YRN_MST WHERE YARN_CAT = 'SEWING THREAD') WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
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
        try
        {
            string CommandText = "SELECT * FROM ( SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST  WHERE YARN_CAT = 'SEWING THREAD') WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY YARN_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
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

    protected int GetPartyCount(string text)
    {
        try
        {
            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("../Reports/CRForSTReportOPT.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlShadeCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlShadeCode.Items.Clear();
                ddlShadeCode.DataSource = data;
                ddlShadeCode.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetShadeItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetShadeItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
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
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindCRGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Records.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnWord_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdCustomerRequest.Rows.Count > 0)
            {
                grdCustomerRequest.AllowPaging = false;
                //grdCustomerRequest.EnableViewState = false;
                grdCustomerRequest.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CustomerRequestForSewingThread.doc"));
                Response.Charset = "";
                Response.ContentType = "application/ms-word";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                ////////
                ////grdCustomerRequest.RenderBeginTag(htw);
                ////grdCustomerRequest.HeaderRow.RenderControl(htw);
                ////foreach (GridViewRow row in grdCustomerRequest.Rows)
                ////{
                ////    row.RenderControl(htw);
                ////}
                ////grdCustomerRequest.FooterRow.RenderControl(htw);
                ////grdCustomerRequest.RenderEndTag(htw);

                //Control parent = grdCustomerRequest.Parent;
                //int GridIndex = 0;
                //if (parent != null)
                //{
                //    GridIndex = parent.Controls.IndexOf(grdCustomerRequest);
                //    parent.Controls.Remove(grdCustomerRequest);
                //}

                //grdCustomerRequest.RenderControl(htw);

                //if (parent != null)
                //{
                //    parent.Controls.AddAt(GridIndex, grdCustomerRequest);
                //}

                ////////
                grdCustomerRequest.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                CommonFuction.ShowMessage("First fill the GridView..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exporting Data into MS-Word.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdCustomerRequest.Rows.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CustomerRequestForSewingThread.xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grdCustomerRequest.AllowPaging = false;
                grdCustomerRequest.DataBind();
                //Change the Header Row back to white color
                grdCustomerRequest.HeaderRow.Style.Add("background-color", "#FFFFFF");
                //Applying stlye to gridview header cells
                for (int i = 0; i < grdCustomerRequest.HeaderRow.Cells.Count; i++)
                {
                    grdCustomerRequest.HeaderRow.Cells[i].Style.Add("background-color", "#507CD1");
                }
                int j = 1;
                //This loop is used to apply stlye to cells based on particular row
                foreach (GridViewRow gvrow in grdCustomerRequest.Rows)
                {
                    gvrow.BackColor = System.Drawing.Color.White;
                    if (j <= grdCustomerRequest.Rows.Count)
                    {
                        if (j % 2 != 0)
                        {
                            for (int k = 0; k < gvrow.Cells.Count; k++)
                            {
                                gvrow.Cells[k].Style.Add("background-color", "#EFF3FB");
                            }
                        }
                    }
                    j++;
                }
                grdCustomerRequest.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                CommonFuction.ShowMessage("First fill the GridView..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exporting Data into MS-Excel.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void grdCustomerRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCustomerRequest.PageIndex = e.NewPageIndex;
        BindCRGrid();
    }

    //protected void gvReportDisplayGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    //{


    //    try
    //   {


    //       if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //          Label LblPartyCode = (Label)e.Row.FindControl("LblPartyCode");
    //           lblParty = LblPartyCode.Text.Trim();

    //            Label lblBranchCode = (Label)e.Row.FindControl("lblBranchCode");
    //            lblBranch = lblBranchCode.Text.Trim();

    //            Label LblPOType = (Label)e.Row.FindControl("LblPOType");
    //            lblPType = LblPOType.Text.Trim();

    //            Label LblPOno = (Label)e.Row.FindControl("LblPOno");
    //            lblPO = LblPOno.Text.Trim();

    //            Label LblItemCode = (Label)e.Row.FindControl("LblItemCode");
    //            pitem_code = LblItemCode.Text.Trim();


    //            DataTable dtc = BindePoSupGrid(lblComp, lblBranch, lblPType, lblPO, pitem_code);
    //            GridView grdTaxDetail = (GridView)e.Row.FindControl("grdTaxDetail");
    //            if (grdTaxDetail != null)
    //            {
    //                grdTaxDetail.DataSource = dtc;
    //                grdTaxDetail.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Bound Event.\r\nSee error log for detail."));
    //    }
    //}
    
}