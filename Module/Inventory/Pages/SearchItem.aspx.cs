using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Data.OracleClient;
using Obout.ComboBox;


public partial class Inventory_SearchItem : System.Web.UI.Page
{
    private static string ItemCodeId = "";
    private static int PONumb = 0;
    private static bool ForPONumb = false;
    private ComboBox ComboBox1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ItemCodeId"] != null)
            ItemCodeId = Request.QueryString["ItemCodeId"].ToString();
        if (Request.QueryString["PONumb"] != null)
            PONumb = int.Parse(Request.QueryString["PONumb"].ToString());
        if (Request.QueryString["ForPONumb"] != null)
            ForPONumb = bool.Parse(Request.QueryString["ForPONumb"].ToString());

        ComboBox1 = new ComboBox();
        ComboBox1.ID = "ComboBox1";
        ComboBox1.Width = Unit.Pixel(400);
        ComboBox1.Height = Unit.Pixel(350);
        ComboBox1.EnableLoadOnDemand = true;
        ComboBox1.DataTextField = "ITEM_DESC";
        ComboBox1.DataValueField = "ITEM_CODE";

        ComboBox1.LoadingItems += ComboBox1_LoadingItems;
        ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        ComboBox1.AutoPostBack = true;

        ComboBox1.HeaderTemplate = new HeaderTemplate();
        ComboBox1.ItemTemplate = new ItemTemplate();
        ComboBox1.FooterTemplate = new FooterTemplate();

        ComboBox1Container.Controls.Add(ComboBox1);

    }
    protected void ComboBox1_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        // Getting the items
        DataTable data = new DataTable();
        if (PONumb > 0)
        {
            data = GetPOItems(e.Text, e.ItemsOffset, 10);
        }
        else if (ForPONumb)
        {
            data = GetIndentedItems(e.Text, e.ItemsOffset, 10);
        }
        else
        {
            data = GetItems(e.Text, e.ItemsOffset, 10);
        }
        ComboBox1.DataSource = data;
        ComboBox1.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        if (PONumb > 0)
        {
            e.ItemsCount = GetPOItemsCount(e.Text);
        }
        else if (ForPONumb)
        {
            e.ItemsCount = GetIndentItemsCount(e.Text);
        }
        else
        {
            e.ItemsCount = GetItemsCount(e.Text);
        }
    }

    #region PO Item LOV
    // General Items
    protected DataTable GetPOItems(string text, int startOffset, int numberOfItems)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        // string whereClause = " WHERE i.ITEM_CODE=pt.ITEM_CODE and pt.PO_NUMB=:PONumb and pt.ITEM_CODE like :SearchQuery or i.ITEM_TYPE like :SearchQuery or i.ITEM_DESC like :SearchQuery";
        string whereClause = " WHERE i.ITEM_CODE=pt.ITEM_CODE and pt.PO_NUMB=:PONumb and pt.ITEM_CODE like :SearchQuery";
        string sortExpression = " ORDER BY i.ITEM_CODE";

        //  string commandText = "SELECT TOP " + numberOfItems + " * FROM TX_ITEM_MST";
        string commandText = "SELECT i.*,pt.PO_TYPE FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i";

        commandText += whereClause;
        if (startOffset != 0)
        {
            commandText += " AND ITEM_CODE NOT IN (SELECT TOP " + startOffset + " ITEM_CODE FROM TX_ITEM_PU_TRN";
            commandText += whereClause + sortExpression + ")";
        }

        commandText += sortExpression;

        OracleCommand cmd = new OracleCommand(commandText, con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';
        cmd.Parameters.Add(":PONumb", OracleType.Number).Value = PONumb;

        OracleDataAdapter da = new OracleDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();

        return ds.Tables[0];
    }

    protected int GetPOItemsCount(string text)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        OracleCommand cmd = new OracleCommand("SELECT count(*) FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i WHERE i.ITEM_CODE=pt.ITEM_CODE and pt.PO_NUMB=:PONumb and i.ITEM_CODE like :SearchQuery or i.ITEM_TYPE like :SearchQuery or i.ITEM_DESC like :SearchQuery", con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';
        cmd.Parameters.Add(":PONumb", OracleType.Number).Value = PONumb;

        return int.Parse(cmd.ExecuteScalar().ToString());
    }
    #endregion

    #region Indent Item LOV
    // General Items
    protected DataTable GetIndentedItems(string text, int startOffset, int numberOfItems)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        // string whereClause = " WHERE i.ITEM_CODE=pt.ITEM_CODE and pt.PO_NUMB=:PONumb and pt.ITEM_CODE like :SearchQuery or i.ITEM_TYPE like :SearchQuery or i.ITEM_DESC like :SearchQuery";
        string whereClause = " where ITEM_CODE like :SearchQuery or ITEM_DESC like :SearchQuery";
        string sortExpression = " ORDER BY ITEM_CODE";

        //  string commandText = "SELECT TOP " + numberOfItems + " * FROM TX_ITEM_MST";
        string commandText = "select  * from (SELECT i.*,pt.IND_TYPE FROM TX_ITEM_IND_TRN pt,TX_ITEM_MST i WHERE i.ITEM_CODE=pt.ITEM_CODE and nvl(pt.APPR_QTY,0)-nvl(PT.PUR_ADJ_QTY,0)<>0 ) asd";

        commandText += whereClause;
        if (startOffset != 0)
        {
            commandText += " AND ITEM_CODE NOT IN (SELECT TOP " + startOffset + " ITEM_CODE FROM TX_ITEM_IND_TRN";
            commandText += whereClause + sortExpression + ")";
        }

        commandText += sortExpression;

        OracleCommand cmd = new OracleCommand(commandText, con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        OracleDataAdapter da = new OracleDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();

        return ds.Tables[0];
    }

    protected int GetIndentItemsCount(string text)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        OracleCommand cmd = new OracleCommand("select count(*) from (SELECT i.*,pt.IND_TYPE FROM TX_ITEM_IND_TRN pt,TX_ITEM_MST i WHERE i.ITEM_CODE=pt.ITEM_CODE and nvl(pt.APPR_QTY,0)-nvl(PT.PUR_ADJ_QTY,0)<>0 ) asd  where ITEM_CODE like :SearchQuery or ITEM_DESC like :SearchQuery", con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }
    #endregion


    #region General Item LOV
    // General Items
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        string whereClause = " WHERE ITEM_CODE like :SearchQuery or ITEM_TYPE like :SearchQuery or ITEM_DESC like :SearchQuery";
        string sortExpression = " ORDER BY ITEM_CODE";

        //  string commandText = "SELECT TOP " + numberOfItems + " * FROM TX_ITEM_MST";
        string commandText = "SELECT * FROM TX_ITEM_MST";

        commandText += whereClause;
        if (startOffset != 0)
        {
            commandText += " AND ITEM_CODE NOT IN (SELECT TOP " + startOffset + " ITEM_CODE FROM TX_ITEM_MST";
            commandText += whereClause + sortExpression + ")";
        }

        commandText += sortExpression;

        OracleCommand cmd = new OracleCommand(commandText, con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        OracleDataAdapter da = new OracleDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();

        return ds.Tables[0];
    }

    protected int GetItemsCount(string text)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TX_ITEM_MST WHERE ITEM_CODE like :SearchQuery or ITEM_TYPE like :SearchQuery or ITEM_DESC like :SearchQuery", con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }
    #endregion
    public class HeaderTemplate : ITemplate
    {

        public void InstantiateIn(Control container)
        {

            Literal header = new Literal();
            header.Text = "<div class=\"header c1\">ITEM CODE</div>";
            container.Controls.Add(header);

            Literal header2 = new Literal();
            header2.Text = "<div class=\"header c2\">ITEM TYPE</div>";
            container.Controls.Add(header2);

            Literal header3 = new Literal();
            header3.Text = "<div class=\"header c3\">ITEM DESC</div>";
            container.Controls.Add(header3);
        }
    }
    public class FooterTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {

            PlaceHolder templatePlaceHolder = new PlaceHolder();
            container.Controls.Add(templatePlaceHolder);
            templatePlaceHolder.DataBinding += new EventHandler(DataBindTemplate);
        }

        public void DataBindTemplate(object sender, EventArgs e)
        {
            PlaceHolder templatePlaceHolder = sender as PlaceHolder;
            ComboBoxFooterTemlateContainer container = templatePlaceHolder.NamingContainer as ComboBoxFooterTemlateContainer;

            Literal footer = new Literal();
            footer.Text = "Displaying " + (container.ItemsCount > 0 ? "1" : "0") + " - " + container.ItemsLoadedCount.ToString() + " out of " + container.ItemsCount.ToString();
            Literal countryText1 = new Literal();

            templatePlaceHolder.Controls.Add(footer);
        }
    }

    public class ItemTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {

            PlaceHolder templatePlaceHolder = new PlaceHolder();
            container.Controls.Add(templatePlaceHolder);
            templatePlaceHolder.DataBinding += new EventHandler(DataBindTemplate);
        }

        public void DataBindTemplate(object sender, EventArgs e)
        {
            PlaceHolder templatePlaceHolder = sender as PlaceHolder;
            ComboBoxItemTemlateContainer container = templatePlaceHolder.NamingContainer as ComboBoxItemTemlateContainer;
            ComboBoxItem item = (ComboBoxItem)container.Parent;

            templatePlaceHolder.Controls.Clear();

            Literal ItemCode = new Literal();
            ItemCode.Text = "<div class=\"item c1\">" + DataBinder.Eval(item.DataItem, "ITEM_CODE").ToString() + "</div>";

            Literal ItemType = new Literal();
            ItemType.Text = "<div class=\"item c2\">" + DataBinder.Eval(item.DataItem, "ITEM_TYPE").ToString() + "</div>";

            Literal ItemDesc = new Literal();
            ItemDesc.Text = " <div class=\"item c3\">" + DataBinder.Eval(item.DataItem, "ITEM_DESC").ToString() + "</div>";

            templatePlaceHolder.Controls.Add(ItemCode);
            templatePlaceHolder.Controls.Add(ItemType);
            templatePlaceHolder.Controls.Add(ItemDesc);

        }

    }

    protected void ComboBox1_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string ItemCode = ComboBox1.SelectedValue.Trim();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindItemCode('" + ItemCode + "','" + ItemCodeId + "')", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
}
