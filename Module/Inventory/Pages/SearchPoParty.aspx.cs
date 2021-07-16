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
using Obout.ComboBox;
using System.Data.OracleClient;

public partial class Inventory_SearchPoParty : System.Web.UI.Page
{
    private static string PartyCodeId = "";
    private ComboBox ComboBox1;    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PartyCodeId"] != null)
            PartyCodeId = Request.QueryString["PartyCodeId"].ToString();

        ComboBox1 = new ComboBox();
        ComboBox1.ID = "ComboBox1";
        ComboBox1.Width = Unit.Pixel(600);
        ComboBox1.Height = Unit.Pixel(250);
        ComboBox1.EnableLoadOnDemand = true;
        ComboBox1.DataTextField = "PRTY_NAME";
        ComboBox1.DataValueField = "PRTY_CODE";

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
        DataTable data = GetVendor(e.Text, e.ItemsOffset, 10);


        ComboBox1.DataSource = data;
        ComboBox1.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetVendorCount(e.Text);

    }


    protected DataTable GetVendor(string text, int startOffset, int numberOfItems)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        string whereClause = " where PRTY_CODE like :SearchQuery or  PRTY_NAME like :SearchQuery or  PRTY_ADD1 like :SearchQuery  or PRTY_ADD2 like :SearchQuery or PRTY_STATE like :SearchQuery ";
        string sortExpression = " ORDER BY PRTY_CODE ";

        //  string commandText = "SELECT TOP " + numberOfItems + " * FROM TX_ITEM_MST";
        string commandText = "select distinct * from (select * from(Select distinct v.PRTY_CODE,PO_TYPE,PRTY_STATE,PRTY_ADD1,PRTY_ADD2,PRTY_NAME,PRTY_ADD1 ||',  '||nvl( PRTY_ADD2,' ') ||',  '|| nvl(PRTY_STATE,' ') address from TX_VENDOR_MST v  right outer join tx_item_pu_mst p  on V.PRTY_CODE=P.PRTY_CODE)  a  where PO_TYPE='PUM') b ";

        commandText += whereClause;
        //if (startOffset != 0)
        //{
        //    commandText += " and PRTY_CODE NOT IN (SELECT TOP " + startOffset + " PRTY_CODE FROM TX_VENDOR_MST";
        //    commandText += whereClause + sortExpression + ")";
        //}

        commandText += sortExpression;

        OracleCommand cmd = new OracleCommand(commandText, con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        OracleDataAdapter da = new OracleDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();

        return ds.Tables[0];
    }

    protected int GetVendorCount(string text)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        OracleCommand cmd = new OracleCommand("select distinct count(*) from (select * from(Select distinct v.PRTY_CODE,PO_TYPE,PRTY_STATE,PRTY_ADD1,PRTY_ADD2,PRTY_NAME,PRTY_ADD1 ||',  '||nvl( PRTY_ADD2,' ') ||',  '|| nvl(PRTY_STATE,' ') address from TX_VENDOR_MST v  right outer join tx_item_pu_mst p  on V.PRTY_CODE=P.PRTY_CODE)  a  where PO_TYPE='PUM') b where PRTY_CODE like :SearchQuery or  PRTY_NAME like :SearchQuery or  PRTY_ADD1 like :SearchQuery  or PRTY_ADD2 like :SearchQuery or PRTY_STATE like :SearchQuery", con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }

    public class HeaderTemplate : ITemplate
    {

        public void InstantiateIn(Control container)
        {

            Literal header = new Literal();
            header.Text = "<div class=\"header c1\">Vendor Code</div>";
            container.Controls.Add(header);           

            Literal header2 = new Literal();
            header2.Text = "<div class=\"header c3\">Vendor Name</div>";
            container.Controls.Add(header2);           

            Literal header3 = new Literal();
            header3.Text = "<div class=\"header c5\">Address</div>";
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

            Literal PartyCode = new Literal();
            PartyCode.Text = "<div class=\"item c1\">" + DataBinder.Eval(item.DataItem, "PRTY_CODE").ToString() + "</div>";

            Literal PartyName = new Literal();
            PartyName.Text = "<div class=\"item c2\">" + DataBinder.Eval(item.DataItem, "PRTY_NAME").ToString() + "</div>";

            Literal PartyAddress = new Literal();
            PartyAddress.Text = " <div class=\"item c3\">" + DataBinder.Eval(item.DataItem, "Address").ToString() + "</div>";
            templatePlaceHolder.Controls.Add(PartyCode);
            templatePlaceHolder.Controls.Add(PartyName);
            templatePlaceHolder.Controls.Add(PartyAddress);

        }

    }

    protected void ComboBox1_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string PartyCode = ComboBox1.SelectedValue.Trim();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindPartyCode('" + PartyCode + "','" + PartyCodeId + "')", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }


}
