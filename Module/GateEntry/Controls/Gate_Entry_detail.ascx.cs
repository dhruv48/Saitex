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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;

public partial class Module_GateEntry_Controls_Gate_Entry_detail : System.Web.UI.UserControl
{


    string MST_NAME = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialControls();
                BindBranch();
                ddlbranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                //ddlbranch.SelectedValue.ToString() = oUserLoginDetail.CH_BRANCHCODE;
                BindTrnType("GATE_TRN_TYPE");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControls()
    {

        txtgateno_from.Text = "";
        txtgateno_to.Text = "";
        txtdocno.Text = "";
        txtentrydate_from.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        txtentrydate_to.Text = System.DateTime.Now.ToShortDateString();
    }

    private void BindBranch()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            ddlbranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlbranch.DataSource = dv;
            ddlbranch.DataValueField = "BRANCH_CODE";
            ddlbranch.DataTextField = "BRANCH_NAME";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("--------Select---------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void BindTrnType(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                ddltrntype.Items.Clear();
                ddltrntype.DataSource = dv;
                ddltrntype.DataTextField = "MST_DESC";
                ddltrntype.DataValueField = "MST_CODE";
                ddltrntype.DataBind();
                ddltrntype.Items.Insert(0, new ListItem("------Select------", string.Empty));
                dt.Dispose();
                dt = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
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

    private void GetGridRecord()
    {
        string BRANCH = string.Empty;
        string TRNTYPE = string.Empty;
        string PRTY_CODE = string.Empty;
        string DOC_NO = string.Empty;
        int GT_FROM = 0;
        int GT_TO = 0;
        string FR_DATE = string.Empty;
        string T_DATE = string.Empty;

        try
        {
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH = string.Empty;
            }
            if (ddltrntype.SelectedValue.ToString() != null && ddltrntype.SelectedValue.ToString() != string.Empty)
            {
                TRNTYPE = ddltrntype.SelectedValue.ToString();
            }
            else
            {
                TRNTYPE = string.Empty;
            }
            if (txtPartyCode.SelectedValue.ToString() != null && txtPartyCode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = txtPartyCode.SelectedText.Trim();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }

            if (txtdocno.Text.ToString() != null && txtdocno.Text.ToString() != string.Empty)
            {
                DOC_NO = txtdocno.Text.Trim().ToString();
            }
            else
            {
                DOC_NO = string.Empty;
            }
            if (txtgateno_from.Text.ToString() != null && txtgateno_from.Text.ToString() != string.Empty)
            {
                GT_FROM = int.Parse(txtgateno_from.Text.Trim().ToString());
            }
            else
            {
                GT_FROM = 0;
            }
            if (txtgateno_to.Text.ToString() != null && txtgateno_to.Text.ToString() != string.Empty)
            {
                GT_TO = int.Parse(txtgateno_to.Text.Trim().ToString());
            }
            else
            {
                GT_TO = 0;
            }
            if (txtentrydate_from.Text.ToString() != null && txtentrydate_from.Text.ToString() != string.Empty)
            {
                FR_DATE = txtentrydate_from.Text.Trim();
            }
            else
            {
                FR_DATE = string.Empty;
            }
            if (txtentrydate_to.Text.ToString() != null && txtentrydate_to.Text.ToString() != string.Empty)
            {
                T_DATE = txtentrydate_to.Text.Trim();
            }
            else
            {
                T_DATE = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.GetRecord(oUserLoginDetail.COMP_CODE, BRANCH, TRNTYPE, PRTY_CODE, DOC_NO, GT_FROM, GT_TO, FR_DATE, T_DATE);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdgateentrydetail.DataSource = dt;
                grdgateentrydetail.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                grdgateentrydetail.Visible = true;
            }
            else
            {
                grdgateentrydetail.DataSource = null;
                grdgateentrydetail.DataBind();
                lblTotalRecord.Text = "0";
                grdgateentrydetail.Visible = false;
                CommonFuction.ShowMessage("No data found by selected Parameter...");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    private DataTable GetGridRecord1()
    {
        string BRANCH = string.Empty;
        string TRNTYPE = string.Empty;
        string PRTY_CODE = string.Empty;
        string DOC_NO = string.Empty;
        int GT_FROM = 0;
        int GT_TO = 0;
        string FR_DATE = string.Empty;
        string T_DATE = string.Empty;

        try
        {
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH = string.Empty;
            }
            if (ddltrntype.SelectedValue.ToString() != null && ddltrntype.SelectedValue.ToString() != string.Empty)
            {
                TRNTYPE = ddltrntype.SelectedValue.ToString();
            }
            else
            {
                TRNTYPE = string.Empty;
            }
            if (txtPartyCode.SelectedValue.ToString() != null && txtPartyCode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = txtPartyCode.SelectedText.Trim();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }

            if (txtdocno.Text.ToString() != null && txtdocno.Text.ToString() != string.Empty)
            {
                DOC_NO = txtdocno.Text.Trim().ToString();
            }
            else
            {
                DOC_NO = string.Empty;
            }
            if (txtgateno_from.Text.ToString() != null && txtgateno_from.Text.ToString() != string.Empty)
            {
                GT_FROM = int.Parse(txtgateno_from.Text.Trim().ToString());
            }
            else
            {
                GT_FROM = 0;
            }
            if (txtgateno_to.Text.ToString() != null && txtgateno_to.Text.ToString() != string.Empty)
            {
                GT_TO = int.Parse(txtgateno_to.Text.Trim().ToString());
            }
            else
            {
                GT_TO = 0;
            }
            if (txtentrydate_from.Text.ToString() != null && txtentrydate_from.Text.ToString() != string.Empty)
            {
                FR_DATE = txtentrydate_from.Text.Trim();
            }
            else
            {
                FR_DATE = string.Empty;
            }
            if (txtentrydate_to.Text.ToString() != null && txtentrydate_to.Text.ToString() != string.Empty)
            {
                T_DATE = txtentrydate_to.Text.Trim();
            }
            else
            {
                T_DATE = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.GetRecord(oUserLoginDetail.COMP_CODE, BRANCH, TRNTYPE, PRTY_CODE, DOC_NO, GT_FROM, GT_TO, FR_DATE, T_DATE);


            return dt;


            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    grdgateentrydetail.DataSource = dt;
            //    grdgateentrydetail.DataBind();
            //    lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            //    grdgateentrydetail.Visible = true;
            //}
            //else
            //{
            //    grdgateentrydetail.DataSource = null;
            //    grdgateentrydetail.DataBind();
            //    lblTotalRecord.Text = "0";
            //    grdgateentrydetail.Visible = false;
            //    CommonFuction.ShowMessage("No data found by selected Parameter...");
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    protected void btngetrecord_Click(object sender, EventArgs e)
    {
        try
        {
            GetGridRecord();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdgateentrydetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetGridRecord();

            grdgateentrydetail.PageIndex = e.NewPageIndex;
            grdgateentrydetail.DataBind();
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
            InitialControls();
            grdgateentrydetail.Visible = false;
            lblTotalRecord.Text = "0";
            txtPartyCode.SelectedIndex = -1;

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
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }



    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {

        string strFilename = "Gate_Entry_Details_List_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(GetGridRecord1(), strFilename, "Gate Entry Details List");


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
