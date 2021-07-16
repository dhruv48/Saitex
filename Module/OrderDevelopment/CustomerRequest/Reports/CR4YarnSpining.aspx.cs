using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;

public partial class Module_OrderDevelopment_CustomerRequest_Reports_CR4YarnSpining : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string PRODUCT_TYPE =string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
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
    private void InitialControls()
    {
        try
        {

          
            txtCRFrom.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txtCRTo.Text = System.DateTime.Now.ToShortDateString();
            BindBranch();
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            txtPartyCode.SelectedIndex = -1;
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
 
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            string party = string.Empty;
            string agent = string.Empty;
            string article = string.Empty;
            if (ddlArticle.SelectedIndex > -1)
            {
                article = ddlArticle.SelectedText;
            }
            if (txtPartyCode.SelectedIndex > -1)
            {
                party = txtPartyCode.SelectedText;
            }
            if (ddlArticle.SelectedIndex > -1)
            {
                article = ddlArticle.SelectedText;
            }
            if (cmbAgent.SelectedIndex > -1)
            {
                agent = cmbAgent.SelectedText;
            }
            string QueryString = "";
            QueryString += "?ArticleNo=" + article;
            QueryString += "&BusinessType=" + string.Empty ;
            QueryString += "&PRODUCT_TYPE=" + ddlProductType.SelectedValue;
            QueryString += "&OrderNo=" + txtCustNo.Text;
            QueryString += "&Branch=" + ddlBranch.SelectedValue.ToString();
            QueryString += "&Party=" + party;
            QueryString += "&Shade=" + txtshadecode.Text ;
            QueryString += "&StDate=" + txtCRFrom.Text;
            QueryString += "&EnDate=" + txtCRTo.Text;
            QueryString += "&Status=" +  ddlStatus.SelectedValue;
            QueryString += "&Year=" + oUserLoginDetail.DT_STARTDATE.Year.ToString();
            QueryString += "&Agent=" + agent;    
            string URL = "../../../../Module/OrderDevelopment/CustomerRequest/Reports/CustomerRequest4YD.aspx" + QueryString;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialControls();
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
        catch (Exception exe)
        {
            throw exe;
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