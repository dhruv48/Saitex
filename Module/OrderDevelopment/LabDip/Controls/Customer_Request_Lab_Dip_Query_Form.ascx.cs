using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using errorLog;

using System.Collections;
using System.Configuration;

using System.Linq;
using System.Web;
using System.Web.Security;

using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Module_OrderDevelopment_LabDip_Controls_Customer_Request_Lab_Dip_Query_Form : System.Web.UI.UserControl
{
    static string BusinessType = "";
   
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;


    protected void Page_Load(object sender, EventArgs e)
    {

        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];


        if (!IsPostBack)
        {
            Initial_Control();


        }

    }


    private void Initial_Control()
    {
        txtYarn.SelectedIndex = -1;
        cmbPartyCode.SelectedIndex = -1;
       
       
        bindyear();
      //  bindBusinessType();
       
        GetReport_Click();

        
       

    }

    //private void bindBusinessType()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);
    //        ddlBusinessType.DataSource = dt;
    //        ddlBusinessType.DataValueField = "MST_DESC";
    //        ddlBusinessType.DataTextField = "MST_CODE";
    //        ddlBusinessType.DataBind();
    //        ddlBusinessType.Items.Insert(0, "");
           
    //        //ddlBusinessType.SelectedIndex = 2;
    //    }
    //    catch
    //    {
    //        throw;

    //    }
    //}


    private void customerRequestNO(string BusinessType)
    {
        try
        {
            ddlCustomerRequestNo.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByCustomerRNo(BusinessType, oUserLoginDetail.COMP_CODE,ddlYear.Text);
            ddlCustomerRequestNo.DataSource = dt;
            ddlCustomerRequestNo.DataValueField = "ORDER_NO";
            ddlCustomerRequestNo.DataTextField = "ORDER_NO";
            ddlCustomerRequestNo.DataBind();
            //ddlCustomerRequestNo.Items.Insert(0, "");
            //ddlBusinessType.SelectedIndex = 2;
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

           // string branch = ddlBranch.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.bindyear("DEVELOPMENT");
            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "YEAR";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, "");
                ddlYear.SelectedIndex = 0;

                  
               // ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));

                //dt.Dispose();
                //dt = null;

            }
            else
            {
             //   string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(" No have financial year & data .");
               
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
               
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void cmbPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            
                
                    DataTable data = GetBranchPartyData(e.Text.ToUpper(), e.ItemsOffset);

                    cmbPartyCode.Items.Clear();

                    cmbPartyCode.DataSource = data;
                    cmbPartyCode.DataBind();

                    e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
                    e.ItemsCount = GetBranchPartyCount(e.Text);
              
            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
           // lblMode.Text = ex.ToString();
        }
    }


    private DataTable GetBranchPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT distinct PRTY_CODE,PRTY_NAME  FROM(SELECT distinct TV.PRTY_CODE, TV.PRTY_NAME FROM TX_VENDOR_MST TV, OD_CUSTOMER_REQUEST_MST BR  WHERE   ORDER_TYPE='DEVELOPMENT' AND TV.PRTY_CODE = BR.PRTY_CODE         AND TV.COMP_CODE = BR.COMP_CODE         AND TV.BRANCH_CODE = BR.BRANCH_CODE   ORDER BY   TV.PRTY_CODE ASC )asd where   PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND TV.PRTY_CODE NOT IN ( SELECT PRTY_CODE FROM(SELECT distinct TV.PRTY_CODE, TV.PRTY_NAME FROM TX_VENDOR_MST TV, OD_CUSTOMER_REQUEST_MST BR  WHERE ORDER_TYPE='DEVELOPMENT' AND TV.PRTY_CODE = BR.PRTY_CODE         AND TV.COMP_CODE = BR.COMP_CODE         AND TV.BRANCH_CODE = BR.BRANCH_CODE   ORDER BY   TV.PRTY_CODE ASC) asd PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery  AND ROWNUM <= " + startOffset + ")";
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


    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetReport_Click();
            GridLedger.PageIndex = e.NewPageIndex;
            GridLedger.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected int GetBranchPartyCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = " SELECT   DISTINCT TV.PRTY_CODE, TV.PRTY_NAME  FROM   TX_VENDOR_MST TV, OD_CUSTOMER_REQUEST_MST BR WHERE       ORDER_TYPE = 'DEVELOPMENT'         AND TV.PRTY_CODE = BR.PRTY_CODE         AND TV.COMP_CODE = BR.COMP_CODE         AND TV.BRANCH_CODE = BR.BRANCH_CODE     AND BR.COMP_CODE = :COMP_CODE           AND BR.BRANCH_CODE = :BRANCH_CODE     AND TV.PRTY_CODE LIKE :SearchQuery ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY   TV.PRTY_CODE ASC   ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }




    protected void txtYCODE_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetYCODE(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtYarn.Items.Clear();
                txtYarn.DataSource = data;
                txtYarn.DataBind();
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


    private DataTable GetYCODE(string Text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT YARN_CODE, YARN_DESC,ARTICLE_NO FROM (SELECT distinct TV.YARN_CODE, TV.YARN_DESC ,BR.ARTICLE_NO FROM YRN_MST TV, OD_CUSTOMER_REQUEST_ST BR  WHERE   TV.COMP_CODE=BR.COMP_CODE AND TV.BRANCH_CODE=BR.BRANCH_CODE  AND BR.ARTICLE_NO = TV.YARN_CODE   AND TV.YARN_CODE LIKE :SearchQuery OR TV.YARN_DESC LIKE :SearchQuery   ORDER BY   TV.YARN_CODE asc)asd where YARN_CODE=ARTICLE_NO  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN ( SELECT YARN_CODE from(SELECT distinct TV.YARN_CODE, TV.YARN_DESC ,BR.ARTICLE_NO FROM YRN_MST TV, OD_CUSTOMER_REQUEST_ST BR  WHERE  TV.COMP_CODE=BR.COMP_CODE AND TV.BRANCH_CODE=BR.BRANCH_CODE  AND BR.ARTICLE_NO = TV.YARN_CODE   AND TV.YARN_CODE LIKE :SearchQuery OR TV.YARN_DESC LIKE :SearchQuery    ORDER BY   TV.YARN_CODE asc)asd where  YARN_CODE= ARTICLE_NO  ASC AND ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY YARN_CODE ASC";
            string SearchQuery = Text + "%";
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
            string CommandText = string.Empty;
            CommandText = " SELECT distinct TV.YARN_CODE, TV.YARN_DESC FROM YRN_MST TV, OD_CUSTOMER_REQUEST_ST BR  WHERE  TV.COMP_CODE=BR.COMP_CODE AND TV.BRANCH_CODE=BR.BRANCH_CODE AND BR.COMP_CODE=:COMP_CODE  AND BR.ARTICLE_NO = TV.YARN_CODE AND BR.BRANCH_CODE=:BRANCH_CODE AND TV.YARN_CODE LIKE :SearchQuery  ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY   TV.YARN_CODE asc   ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }


    protected void ddlBusinessType_SelectedIndexChanged(object sender, EventArgs e)
    {  BusinessType= ddlBusinessType.SelectedItem.Value.Trim();
        customerRequestNO(BusinessType);
    }
    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForGried(ddlBusinessType.SelectedItem.Value, ddlYear.Text, ddlCustomerRequestNo.SelectedText.ToString(), txtYarn.SelectedText.ToString(), cmbPartyCode.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);

           
        if (data.Rows.Count>0)
        {
            int count = data.Rows.Count;
            lblTotalRecord.Text = count.ToString();
            GridLedger.DataSource = data;
            GridLedger.DataBind();


        }
        else 
        {

            Common.CommonFuction.ShowMessage("Data Not Found");
        }


    }


    protected void GetReport_Click()
    {
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForGried(ddlBusinessType.SelectedItem.Text, ddlYear.Text, ddlCustomerRequestNo.SelectedText.ToString(), txtYarn.SelectedText.ToString(), cmbPartyCode.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);


        if (data.Rows.Count > 0)
        {
            int count = data.Rows.Count;
            lblTotalRecord.Text = count.ToString();
            GridLedger.DataSource = data;
            GridLedger.DataBind();


        }
        else
        {

            Common.CommonFuction.ShowMessage("Data Not Found");
        }


    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
       
     
        Initial_Control();
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Leaving Page.\r\nSee error log for detail."));
            

        }
    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForGriedExcel(ddlBusinessType.SelectedItem.Value, ddlYear.Text, ddlCustomerRequestNo.SelectedText.ToString(), txtYarn.SelectedText.ToString(), cmbPartyCode.SelectedText.ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);


        if (data.Rows.Count > 0)
        {
            string strFilename = "Yarn_Master_List_" + DateTime.Now.ToShortDateString() + ".xls";
            ExporttoExcel(data, strFilename, "Customer Request Lab Dip List");


        }
        else
        {

            Common.CommonFuction.ShowMessage("Data Not Found");
        }



       
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
    protected void txtCustomerRequestItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        customerRequestNO(BusinessType);

    }
}
