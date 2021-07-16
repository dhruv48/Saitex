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


public partial class Module_WorkOrder_Controls_WorkOrder_Issue : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            Initializing();
        }
    }
    private void Initializing()
    {
        ddlYarn.SelectedIndex = -1;
        BindTrnType();
        wo_no();
        party();
        ViewState["GridRecord"] = null;
        TXTDATEFROM.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
        TXTDATETO.Text = Common.CommonFuction.GetYearEndDate(DateTime.Parse(TXTDATEFROM.Text)).ToShortDateString();
        BINDGRID();
        
    }
    protected void BINDGRID()
    {
        int WO_NUMB=0;
        string PARTY_CODE = string.Empty;
        string YARN = string.Empty;
        if (ddwo.SelectedItem.Value != null  && ddwo.SelectedItem.Value!=string.Empty)
        {
            WO_NUMB = int.Parse(ddwo.SelectedItem.Value);
        }
        if (ddprty.SelectedItem.Text.Trim() != null && ddprty.SelectedItem.Text.Trim() != string.Empty && ddprty.SelectedIndex!=0)
        {
            PARTY_CODE = ddprty.SelectedItem.Text.Trim();
        }
        if (ddlYarn.SelectedValue.Trim() != null && ddlYarn.SelectedValue.Trim()!=string.Empty)
        {
            YARN = ddlYarn.SelectedValue.Trim();
        }
        DataTable DT = BindIssueOrderGrid(oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, WO_NUMB,PARTY_CODE, TXTDATEFROM.Text, TXTDATETO.Text, YARN, oUserLoginDetail.DT_STARTDATE.Year);
        if (DT.Rows.Count > 0)
        {
            lblrecd.Text = "RECORD FOUND: "+DT.Rows.Count.ToString();
            Get_WO_Detail.DataSource = DT;
            Get_WO_Detail.DataBind();
            ViewState["GridRecord"] = DT;
        }
        else
        {
            Get_WO_Detail.DataSource = DT;
            Get_WO_Detail.DataBind();
        }
    }

    
    protected void Get_WO_Detail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BINDGRID();

            Get_WO_Detail.PageIndex = e.NewPageIndex;
            Get_WO_Detail.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
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
    private void BindTrnType()
    {
        try
        {
            ddlTrnType.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.YRN_MST.getTransType();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView DV = dt.DefaultView;
                DV.RowFilter = "TRN_TYPE LIKE 'IYS11%'";
                ddlTrnType.DataTextField = "TRN_TDESC";
                ddlTrnType.DataValueField = "TRN_TYPE";
                ddlTrnType.DataSource = DV;// dt;
                ddlTrnType.DataBind();
            }
            ddlTrnType.Items.Insert(0, new ListItem("-----------All---------------", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void wo_no()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_WO_MST.getwo_no(oUserLoginDetail.COMP_CODE);
            ddwo.Items.Clear();
            ddwo.DataSource = dt;
            ddwo.DataValueField = "WO_NUMB";
            ddwo.DataTextField = "WO_NUMB";
            ddwo.DataBind();
            ddwo.Items.Insert(0, new ListItem("-------Select-----", string.Empty));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void party()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_WO_MST.getparty(oUserLoginDetail.COMP_CODE);
            
            ddprty.Items.Clear();
            ddprty.DataSource = dt;
            ddprty.DataTextField = "PRTY_NAME";
            ddprty.DataValueField = "JOBER_PARTY";
            ddprty.DataBind();
            ddprty.Items.Insert(0, new ListItem("-------select--------", string.Empty));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void ddlYarn_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarn();
            DataView Dv = new DataView(dt);
            ddlYarn.DataSource = Dv;
            ddlYarn.DataValueField = "YARN_CODE";
            ddlYarn.DataTextField = "YARN_CODE";
            ddlYarn.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }
    public DataTable BindIssueOrderGrid(string BRANCH_CODE, string COMP_CODE, int WO_NUM, string PRTY_CODE, string FROM_DATE, string TO_DATE, string YARN_DESC, int YEAR)
    {
        try
        {

            DataTable DT = SaitexDL.Interface.Method.YRN_IR_MST.BindIssueOrder(BRANCH_CODE, COMP_CODE, WO_NUM, PRTY_CODE, FROM_DATE, TO_DATE, YARN_DESC, YEAR);
            return DT;
        }
        catch (Exception xex)
        { throw xex; }
    }
    
   
    protected void BTNRECORD_Click(object sender, EventArgs e)
    {
        BINDGRID();
    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "JobWork_OutSide_Issue_Report_" + DateTime.Now.ToShortDateString() + ".xls";
        DateTime Sdate = DateTime.Parse(TXTDATEFROM.Text);
        DateTime Edate = DateTime.Parse(TXTDATETO.Text);

        DataTable data = (DataTable)ViewState["GridRecord"];

        ExporttoExcel(data, strFilename, "JobWork_OutSide_Issue_Report");
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

        //HttpContext.Current.Response.Write("<Td align='center' valing='top'>");
        //HttpContext.Current.Response.Write("<B>");
        //HttpContext.Current.Response.Write("Recipe Details");
        //HttpContext.Current.Response.Write("</B>");
        //HttpContext.Current.Response.Write("</Td>");

        //HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                HttpContext.Current.Response.Write(row[i].ToString());
                //******************************************//
                //      if (i == 15)
                //      {

                //          HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                //          DataView dvBASEQUALITY = new DataView(dtBASEQUALITY);
                //          dvBASEQUALITY.RowFilter = "CUSTOMER_REQ_NO='" + row["CUSTOMER_REQ_NO"].ToString() + "' and LAB_DIP_NO='" + row["LAB_DIP_NO"].ToString() + "' AND LR_OPTION='" + row["LR_OPTION"].ToString() + "'";

                //          if (dvBASEQUALITY.Count > 0)
                //          {
                //              HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                //"borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                //"style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                //              HttpContext.Current.Response.Write("<TR>");

                //              foreach (DataColumn dtcol in dtBASEQUALITY.Columns)
                //              {
                //                  HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                //                  HttpContext.Current.Response.Write("<B>");
                //                  HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                //                  HttpContext.Current.Response.Write("</B>");
                //                  HttpContext.Current.Response.Write("</Td>");

                //              }
                //              HttpContext.Current.Response.Write("</TR>");
                //              for (int j = 0; j < dvBASEQUALITY.Count; j++)
                //              {
                //                  HttpContext.Current.Response.Write("<Tr>");
                //                  for (int i1 = 0; i1 < dvBASEQUALITY.Table.Columns.Count; i1++)
                //                  {
                //                      HttpContext.Current.Response.Write("<Td >");
                //                      HttpContext.Current.Response.Write(dvBASEQUALITY[j][i1].ToString());
                //                      HttpContext.Current.Response.Write("</Td>");

                //                  }
                //                  HttpContext.Current.Response.Write("</Tr>");
                //              }
                //              HttpContext.Current.Response.Write("</Table>");
                //          }
                //          HttpContext.Current.Response.Write("</Td>");

                //      }



                //***********************************************//   

                HttpContext.Current.Response.Write("</Td>");

            }

            HttpContext.Current.Response.Write("</TR>");


        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
        //  HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Initializing();
    }
    protected void ImagePrint_Click(object sender, ImageClickEventArgs e)
    {
        int WO_NUMB = 0;
        string PARTY_CODE = string.Empty;
        string YARN = string.Empty;
        if (ddwo.SelectedItem.Value != null && ddwo.SelectedItem.Value != string.Empty)
        {
            WO_NUMB = int.Parse(ddwo.SelectedItem.Value);
        }
        if (ddprty.SelectedItem.Text.Trim() != null && ddprty.SelectedItem.Text.Trim() != string.Empty && ddprty.SelectedIndex != 0)
        {
            PARTY_CODE = ddprty.SelectedItem.Text.Trim();
        }
        if (ddlYarn.SelectedValue.Trim() != null && ddlYarn.SelectedValue.Trim() != string.Empty)
        {
            YARN = ddlYarn.SelectedValue.Trim();
        }
        DateTime Sdate = DateTime.Parse(TXTDATEFROM.Text);
        DateTime Edate = DateTime.Parse(TXTDATETO.Text);
        string URL = "../Reports/JobWork_OutSide_Issue_Report.aspx?";
        URL += "Year=" + oUserLoginDetail.DT_STARTDATE.Year;
        URL += "&BRANCH_CODE=" + oUserLoginDetail.CH_BRANCHCODE;
        URL += "&COMP_CODE=" + oUserLoginDetail.COMP_CODE;
        URL += "&WO_NUM=" + WO_NUMB;
        URL += "&PRTY_CODE=" + PARTY_CODE;
        URL += "&FROM_DATE=" + Sdate.ToShortDateString();
        URL += "&TO_DATE=" + Edate.ToShortDateString();
        URL += "&YARN_DESC=" + YARN;
        
        Response.Redirect(URL);
    }
}