using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_OrderDevelopment_CustomerRequest_Reports_CR_Yarn_Process_Report : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public string PRODUCT_TYPE { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["PRODUCT_TYPE"] != null)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }
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
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            txtPartyCode.SelectedIndex = -1;
            cmbAgent.SelectedIndex = -1;
            ddlArticle.SelectedIndex = -1;
            txtCustNo.Text = string.Empty;
            ddlStatus.SelectedIndex = -1;
            getProduct();
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
            ddlBranch.Items.Insert(0, new ListItem("--------All---------", ""));
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
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE upper(VENDOR_CAT_CODE) NOT IN (upper('Transporter'),upper('Broker')) AND ( PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery) ORDER BY PRTY_CODE ASC) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE upper(VENDOR_CAT_CODE) NOT IN (upper('Transporter'),upper('Broker')) AND (PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery) ORDER BY PRTY_CODE ASC) asd WHERE ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

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
            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE upper(VENDOR_CAT_CODE) NOT IN (upper('Transporter'),upper('Broker')) AND (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery) ORDER BY PRTY_CODE ASC) asd ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

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
            string CommandText = " SELECT * FROM ( SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE,COLOUR , YARN_CODE||'@'|| YARN_DESC||'@'||UOM ||'@'||TRANSFER_PRICE as Combined FROM YRN_MST  WHERE YARN_CAT = 'TEXTURISED') WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND YARN_code NOT IN (SELECT YARN_CODE FROM ( SELECT * FROM ( SELECT YARN_CODE, YARN_DESC, YARN_TYPE, COLOUR, YARN_CODE||'@'|| YARN_DESC||'@'||UOM ||'@'||TRANSFER_PRICE as Combined   FROM YRN_MST WHERE YARN_CAT = 'TEXTURISED') WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

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
            string CommandText = "SELECT * FROM ( SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST  WHERE YARN_CAT = 'TEXTURISED') WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY YARN_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

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

        printReport();
        
    }


    protected void printReport()
    {
        string BRANCH_CODE = string.Empty;
        string BRANCH_NAME = string.Empty;
        string PARTY_CODE = string.Empty;
        string PARTY_NAME = string.Empty;
        string AGENT_CODE = string.Empty;
        string AGENT_NAME = string.Empty;
        string ARTICLE_CODE = string.Empty;
        string ARTICLE_NAME = string.Empty;
        string SHADE_CODE = string.Empty;
        string SHADE_NAME = string.Empty;
        string StDate = string.Empty;
        string EnDate = string.Empty;
        string YEAR = string.Empty;
        string STATUS = string.Empty;
        string STATUS_NAME = string.Empty;
        string ORDER_NO = string.Empty;
        string ORDER_NO_DETAILS = string.Empty;
        string REPORT_TYPE = string.Empty;





        try
        {
            DataTable myDataTable = new DataTable();
            myDataTable.Columns.Add("BRANCH_CODE", typeof(string));
            myDataTable.Columns.Add("BRANCH_NAME", typeof(string));
            myDataTable.Columns.Add("PARTY_CODE", typeof(string));
            myDataTable.Columns.Add("PARTY_NAME", typeof(string));
            myDataTable.Columns.Add("AGENT_CODE", typeof(string));
            myDataTable.Columns.Add("AGENT_NAME", typeof(string));
            myDataTable.Columns.Add("ARTICLE_CODE", typeof(string));
            myDataTable.Columns.Add("ARTICLE_NAME", typeof(string));
            myDataTable.Columns.Add("SHADE_CODE", typeof(string));
            myDataTable.Columns.Add("SHADE_NAME", typeof(string));
            myDataTable.Columns.Add("StDate", typeof(string));
            myDataTable.Columns.Add("EnDate", typeof(string));
            myDataTable.Columns.Add("YEAR", typeof(string));
            myDataTable.Columns.Add("STATUS", typeof(string));
            myDataTable.Columns.Add("STATUS_NAME", typeof(string));
            myDataTable.Columns.Add("ORDER_NO", typeof(string));
            myDataTable.Columns.Add("ORDER_NO_DETAILS", typeof(string));
            myDataTable.Columns.Add("REPORT_TYPE", typeof(string));
            myDataTable.Columns.Add("PRODUCT_TYPE", typeof(string));
            Session["CR_REPORT_YARN_PROCESS"] = null;
            DataRow row = myDataTable.NewRow();
            if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty && ddlBranch.SelectedIndex > 0)
            {
                row["BRANCH_CODE"] = ddlBranch.SelectedValue.ToString();
                row["BRANCH_NAME"] = ddlBranch.SelectedItem.Text.ToString();
                row["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
            }
            else
            {
                row["BRANCH_CODE"] = string.Empty;
                row["BRANCH_NAME"] = "All";
                row["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;

            }

            if (txtPartyCode.SelectedValue.ToString() != null && txtPartyCode.SelectedValue.ToString() != string.Empty && txtPartyCode.SelectedIndex > -1)
            {
                row["PARTY_CODE"] = txtPartyCode.SelectedText;
                row["PARTY_NAME"] = txtPartyCode.SelectedValue;
            }
            else
            {
                row["PARTY_CODE"] = string.Empty;
                row["PARTY_NAME"] = "All";
            }
            if (cmbAgent.SelectedValue.ToString() != null && cmbAgent.SelectedValue.ToString() != string.Empty && cmbAgent.SelectedIndex > -1)
            {
                row["AGENT_CODE"] = cmbAgent.SelectedText;
                row["AGENT_NAME"] = cmbAgent.SelectedValue;
            }
            else
            {
                row["AGENT_CODE"] = string.Empty;
                row["AGENT_NAME"] = "All";
            }
            if (ddlArticle.SelectedValue.ToString() != null && ddlArticle.SelectedValue.ToString() != string.Empty && ddlArticle.SelectedIndex > -1)
            {
                row["ARTICLE_CODE"] = ddlArticle.SelectedText;
                row["ARTICLE_NAME"] = ddlArticle.SelectedValue;
            }
            else
            {
                row["ARTICLE_CODE"] = string.Empty;
                row["ARTICLE_NAME"] = "All";
            }
            if (txtCustNo.Text != null && txtCustNo.Text != string.Empty)
            {
                row["ORDER_NO"] = txtCustNo.Text.ToUpper().Trim();
                row["ORDER_NO_DETAILS"] = txtCustNo.Text.ToUpper().Trim();
            }
            else
            {
                row["ORDER_NO"] = string.Empty;
                row["ORDER_NO_DETAILS"] = "All";
            }
            if (txtshadecode.Text.ToString() != null && txtshadecode.Text.ToString() != string.Empty)
            {
                row["SHADE_CODE"] = txtshadecode.Text;
                row["SHADE_NAME"] = txtshadecode.Text;
            }
            else
            {
                row["SHADE_CODE"] = string.Empty;
                row["SHADE_NAME"] = "All";
            }
            if (txtCRFrom.Text.Trim().ToString() != null && txtCRFrom.Text.Trim().ToString() != string.Empty && txtCRTo.Text.Trim().ToString() != null && txtCRTo.Text.Trim().ToString() != string.Empty)
            {
                row["StDate"] = txtCRFrom.Text.Trim().ToString();
                row["EnDate"] = txtCRTo.Text.Trim().ToString();
            }
            else
            {
                row["StDate"] = DateTime.Now;
                row["EnDate"] = DateTime.Now;
            }

            if (ddlStatus.SelectedValue.ToString() != null && ddlStatus.SelectedValue.ToString() != string.Empty && ddlStatus.SelectedIndex > 0)
            {
                row["STATUS"] = ddlStatus.SelectedValue.ToString();
                row["STATUS_NAME"] = ddlStatus.SelectedItem.ToString();
            }
            else
            {
                row["STATUS"] = string.Empty;
                row["STATUS_NAME"] = "All";
            }
            if (!string.IsNullOrEmpty(ddlProductType.SelectedValue))
            {
                row["PRODUCT_TYPE"] = ddlProductType.SelectedValue;
            }
            else
            {
                row["PRODUCT_TYPE"] = string.Empty;
            }
            row["REPORT_TYPE"] = redForQuery.Text;
            myDataTable.Rows.Add(row);
            Session["CR_REPORT_YARN_PROCESS"] = myDataTable;
            string URL = "../Reports/CR_Report_Yarn_Process.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);





            //if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty && ddlBranch.SelectedIndex > 0)
            //{
            //    BRANCH_CODE = ddlBranch.SelectedValue.ToString();
            //    BRANCH_NAME = ddlBranch.SelectedItem.Text.ToString();
            //    YEAR = oUserLoginDetail.DT_STARTDATE.Year.ToString();
            //}
            //else
            //{
            //    BRANCH_CODE = string.Empty;
            //    BRANCH_NAME = "All";
            //    YEAR = oUserLoginDetail.DT_STARTDATE.Year.ToString();

            //}

            //if (txtPartyCode.SelectedValue.ToString() != null && txtPartyCode.SelectedValue.ToString() != string.Empty && txtPartyCode.SelectedIndex > -1)
            //{
            //    PARTY_CODE = txtPartyCode.SelectedText;
            //    PARTY_NAME = txtPartyCode.SelectedValue;
            //}
            //else
            //{
            //    PARTY_CODE = string.Empty;
            //    PARTY_NAME = "All";
            //}

            //if (ddlArticle.SelectedValue.ToString() != null && ddlArticle.SelectedValue.ToString() != string.Empty && ddlArticle.SelectedIndex > -1)
            //{
            //    ARTICLE_CODE = ddlArticle.SelectedText;
            //    ARTICLE_NAME = ddlArticle.SelectedValue;
            //}
            //else
            //{
            //    ARTICLE_CODE = string.Empty;
            //    ARTICLE_NAME = "All";
            //}
            //if (txtCustNo.Text != null && txtCustNo.Text != string.Empty)
            //{
            //    ORDER_NO = txtCustNo.Text.ToUpper().Trim();
            //    ORDER_NO_DETAILS = txtCustNo.Text.ToUpper().Trim();
            //}
            //else
            //{
            //    ORDER_NO = string.Empty;
            //    ORDER_NO_DETAILS = "All";
            //}
            //if (txtshadecode.Text.ToString() != null && txtshadecode.Text.ToString() != string.Empty)
            //{
            //    SHADE_CODE = txtshadecode.Text;
            //    SHADE_NAME = txtshadecode.Text;
            //}
            //else
            //{
            //    SHADE_CODE = string.Empty;
            //    SHADE_NAME = "All";
            //}
            //if (txtCRFrom.Text.Trim().ToString() != null && txtCRFrom.Text.Trim().ToString() != string.Empty && txtCRTo.Text.Trim().ToString() != null && txtCRTo.Text.Trim().ToString() != string.Empty)
            //{
            //    StDate = txtCRFrom.Text.Trim().ToString();
            //    EnDate = txtCRTo.Text.Trim().ToString();
            //}
            //else
            //{
            //    StDate = string.Empty;
            //    EnDate = string.Empty;
            //}

            //if (ddlStatus.SelectedValue.ToString() != null && ddlStatus.SelectedValue.ToString() != string.Empty && ddlStatus.SelectedIndex > 0)
            //{
            //    STATUS = ddlStatus.SelectedValue.ToString();
            //    STATUS_NAME = ddlStatus.SelectedItem.ToString();
            //}
            //else
            //{
            //    STATUS = string.Empty;
            //    STATUS_NAME = "All";
            //}

            //string URL = "../Reports/CR_Report_Yarn_Process.aspx?BRANCH_CODE =" + BRANCH_CODE + "&BRANCH_NAME=" + BRANCH_NAME + "&PARTY_CODE=" + PARTY_CODE + "&PARTY_NAME=" + PARTY_NAME + "&ARTICLE_CODE=" + ARTICLE_CODE + "&ARTICLE_NAME=" + ARTICLE_NAME + "&SHADE_CODE =" + SHADE_CODE + "&SHADE_NAME=" + SHADE_NAME + "&StDate=" + StDate + "&EnDate=" + EnDate + "&YEAR=" + YEAR + "&STATUS=" + STATUS + "&STATUS_NAME=" + STATUS_NAME + "&ORDER_NO=" + ORDER_NO + "&ORDER_NO_DETAILS=" + ORDER_NO_DETAILS + "&REPORT_TYPE=" + REPORT_TYPE + "&PRODUCT_TYPE=" + PRODUCT_TYPE;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);



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
   
    public void getProduct()
    {
        try
        {

            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);
            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();
            ddlProductType.Items.Insert(0, new ListItem("--------All---------", ""));
            if (!string.IsNullOrEmpty(PRODUCT_TYPE))
            {
                ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(PRODUCT_TYPE));
                ddlProductType.Text = PRODUCT_TYPE;
                ddlProductType.Enabled = false;
            }
           
        }
        catch
        {
            throw;
        }
    }

    protected void cmbAgent_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetAgentData(e.Text.ToUpper(), e.ItemsOffset);
            cmbAgent.Items.Clear();
            cmbAgent.DataSource = data;
            cmbAgent.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetAgentCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));

        }
    }

    private DataTable GetAgentData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE upper(VENDOR_CAT_CODE) IN (upper('Broker')) AND (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery)   ORDER BY PRTY_CODE ASC) asd WHERE  ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE upper(VENDOR_CAT_CODE) IN (upper('Broker')) AND (PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery)   ORDER BY PRTY_CODE ASC) asd WHERE  ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetAgentCount(string text)
    {
        try
        {
            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE  upper(VENDOR_CAT_CODE) IN (upper('Broker')) AND  (PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery) ORDER BY PRTY_CODE ASC) asd   ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

        }
        catch
        {
            throw;
        }
    }

}
