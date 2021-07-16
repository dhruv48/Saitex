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

public partial class Module_OrderDevelopment_CustomerRequest_Controls_Cust_Req_for_yarn2 : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
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

    private void InitialControls()
    {
        try
        {

            tdPrint.Visible = true;
            txtCRFrom.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txtCRTo.Text = System.DateTime.Now.ToShortDateString();
            BindBranch();
            BindYarnGrd();
        }
        catch (Exception ex)
        {
            throw ex;
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
            ddlBranch.Items.Insert(0, new ListItem("--------Select---------", ""));
            dt.Dispose();
            dt = null;
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

        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (  SELECT  YA.ASS_YARN_CODE YARN_CODE, YA.ASS_YARN_DESC      YARN_DESC,         YM.YARN_TYPE,  YM.YARN_CAT,    (   YM.TKTNO || '@' ||      YM.MAKE || '@'|| YM.ENDUSE  || '@'                      || YA.ASS_YARN_CODE || '@'   || UNITWT|| '@' || NET_BOX_WT  || '@'  || NET_CART_WT                      || '@'  || UOM  || '@'  || YA.ASS_YARN_DESC) AS Combined      FROM   YRN_MST YM ,YRN_ASSOCATED_MST YA      WHERE   YM.YARN_CODE=YA.YARN_CODE    AND NOT  (YARN_TYPE = 'NON DYED')   AND (   YA.ASS_YARN_CODE LIKE :SearchQuery   OR YM.YARN_TYPE LIKE :SearchQuery OR YA.ASS_YARN_DESC LIKE :SearchQuery)      ORDER BY   YA.YARN_CODE) asd WHERE 1=1";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN (SELECT YARN_CODE FROM (SELECT  YA.ASS_YARN_CODE YARN_CODE, YA.ASS_YARN_DESC      YARN_DESC,         YM.YARN_TYPE,  YM.YARN_CAT,    (   YM.TKTNO || '@' ||      YM.MAKE || '@'|| YM.ENDUSE  || '@'                      || YA.ASS_YARN_CODE || '@'   || UNITWT|| '@' || NET_BOX_WT  || '@'  || NET_CART_WT                      || '@'  || UOM  || '@'  || YA.ASS_YARN_DESC) AS Combined      FROM   YRN_MST YM ,YRN_ASSOCATED_MST YA      WHERE   YM.YARN_CODE=YA.YARN_CODE  AND NOT  (YARN_TYPE = 'NON DYED')  AND (   YA.ASS_YARN_CODE LIKE :SearchQuery   OR YM.YARN_TYPE LIKE :SearchQuery OR YA.ASS_YARN_DESC LIKE :SearchQuery)  AND ROWNUM <= " + startOffset + "      ORDER BY   YA.YARN_CODE) asd   )";
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
            string CommandText = "SELECT * FROM ( SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST  WHERE YARN_CAT = 'YARN') WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd ";
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

    private void BindYarnGrd()
    {
        string strBranch = string.Empty;
        string strParty = string.Empty;
        string strArticle = string.Empty;
        string strShadeCode = string.Empty;
        string DTCRFrom = string.Empty;
        string DTCRTo = string.Empty;
        string strStatus = string.Empty;
        string strCustNo = string.Empty;
        try
        {
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
            if (txtCustNo.Text != null && txtCustNo.Text != string.Empty)
            {
                strCustNo = txtCustNo.Text.ToUpper().Trim();
            }
            else
            {
                strCustNo = string.Empty;
            }
            if (txtshadecode.Text.ToString() != null && txtshadecode.Text.ToString() != string.Empty)
            {
                strShadeCode = txtshadecode.Text.ToString();
            }
            else
            {
                strShadeCode = string.Empty;
            }
            if (txtCRFrom.Text.Trim().ToString() != null && txtCRFrom.Text.Trim().ToString() != string.Empty && txtCRTo.Text.Trim().ToString() != null && txtCRTo.Text.Trim().ToString() != string.Empty)
            {
                DTCRFrom = txtCRFrom.Text.Trim().ToString();
                DTCRTo = txtCRTo.Text.Trim().ToString();
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
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetCRForYarn1(oUserLoginDetail.COMP_CODE, strBranch, oUserLoginDetail.DT_STARTDATE.Year, strParty, strArticle, strShadeCode, DTCRFrom, DTCRTo, strStatus, strCustNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdCustomerRequest.DataSource = dt;
                grdCustomerRequest.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grdCustomerRequest.Visible = true;
            }
            else
            {
                grdCustomerRequest.DataSource = null;
                grdCustomerRequest.DataBind();
                lblTotalRecord.Text = "0";
                grdCustomerRequest.Visible = false;
                CommonFuction.ShowMessage("No data found..");
            }
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));

        }
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

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindYarnGrd();
        }
        catch (Exception ex)
        {
            throw ex;
        }
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

        }
    }

    protected void grdCustomerRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindYarnGrd();

            grdCustomerRequest.PageIndex = e.NewPageIndex;
            grdCustomerRequest.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable BindYarnGrdExcel()
    {
        string strBranch = string.Empty;
        string strParty = string.Empty;
        string strArticle = string.Empty;
        string strShadeCode = string.Empty;
        string DTCRFrom = string.Empty;
        string DTCRTo = string.Empty;
        string strStatus = string.Empty;
        string strCustNo = string.Empty;
        try
        {
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
            if (txtCustNo.Text != null && txtCustNo.Text != string.Empty)
            {
                strCustNo = txtCustNo.Text.ToUpper().Trim();
            }
            else
            {
                strCustNo = string.Empty;
            }
            if (txtshadecode.Text.ToString() != null && txtshadecode.Text.ToString() != string.Empty)
            {
                strShadeCode = txtshadecode.Text.ToString();
            }
            else
            {
                strShadeCode = string.Empty;
            }
            if (txtCRFrom.Text.Trim().ToString() != null && txtCRFrom.Text.Trim().ToString() != string.Empty && txtCRTo.Text.Trim().ToString() != null && txtCRTo.Text.Trim().ToString() != string.Empty)
            {
                DTCRFrom = txtCRFrom.Text.Trim().ToString();
                DTCRTo = txtCRTo.Text.Trim().ToString();
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
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.GetCRForYarn1(oUserLoginDetail.COMP_CODE, strBranch, oUserLoginDetail.DT_STARTDATE.Year, strParty, strArticle, strShadeCode, DTCRFrom, DTCRTo, strStatus, strCustNo);
            return dt;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    grdCustomerRequest.DataSource = dt;
            //    grdCustomerRequest.DataBind();
            //    lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            //    grdCustomerRequest.Visible = true;
            //}
            //else
            //{
            //    grdCustomerRequest.DataSource = null;
            //    grdCustomerRequest.DataBind();
            //    lblTotalRecord.Text = "0";
            //    grdCustomerRequest.Visible = false;
            //    CommonFuction.ShowMessage("No data found..");
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Customer_Request_For_Yarn_Query_" + DateTime.Now.ToShortDateString() + ".xls";
        ExporttoExcel(BindYarnGrdExcel(), strFilename, "Customer Request For Yarn Query");

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




}
