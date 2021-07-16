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


public partial class Admin_SearchCompany : System.Web.UI.Page
{
    private static string CompCodeId = "";
    private ComboBox ComboBox1;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["CompCodeId"] != null)
        CompCodeId = Request.QueryString["CompCodeId"].ToString();


    ComboBox1 = new ComboBox();
    ComboBox1.ID = "ComboBox1";
    ComboBox1.Width = Unit.Pixel(680);
    ComboBox1.Height = Unit.Pixel(250);
    ComboBox1.EnableLoadOnDemand = true;
    ComboBox1.DataTextField = "VC_COMPANYNAME";
    ComboBox1.DataValueField = "COMP_CODE";

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
        DataTable data = GetComp(e.Text, e.ItemsOffset, 10);

        ComboBox1.DataSource = data;
        ComboBox1.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetCompCount(e.Text);
    }

    protected DataTable GetComp(string text, int startOffset, int numberOfItems)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        string whereClause = " WHERE COMP_CODE like :SearchQuery or VC_COMPANYNAME like :SearchQuery or VC_COMPANYADDRESS like :SearchQuery or VC_COMPANYSTATE like :SearchQuery or VC_COMNPANYCOUNTRY like :SearchQuery";
        string sortExpression = " ORDER BY COMP_CODE";

        //  string commandText = "SELECT TOP " + numberOfItems + " * FROM TX_ITEM_MST";
        //string commandText = "SELECT * FROM TBLCOMPANYMASTER";

        string commandText = "select  COMP_CODE,VC_COMPANYNAME,VC_COMPANYADDRESS ||',  '||nvl( VC_COMPANYSTATE,' ') ||',  '|| nvl(VC_COMNPANYCOUNTRY,' ') Address  from TBLCOMPANYMASTER";
        commandText += whereClause;
        if (startOffset != 0)
        {
            commandText += " AND COMP_CODE NOT IN (SELECT TOP " + startOffset + " COMP_CODE FROM TBLCOMPANYMASTER";
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

    protected int GetCompCount(string text)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TBLCOMPANYMASTER WHERE COMP_CODE like :SearchQuery or VC_COMPANYNAME like :SearchQuery or VC_COMPANYADDRESS like :SearchQuery or VC_COMPANYSTATE like :SearchQuery or VC_COMNPANYCOUNTRY like :SearchQuery", con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }

    public class HeaderTemplate : ITemplate
    {

        public void InstantiateIn(Control container)
        {

            Literal header = new Literal();
            header.Text = "<div class=\"header c1\">COMP CODE</div>";
            container.Controls.Add(header);

            Literal header2 = new Literal();
            header2.Text = "<div class=\"header c2\">COMPANY NAME</div>";
            container.Controls.Add(header2);

            Literal header3 = new Literal();
            header3.Text = "<div class=\"header c3\">COMPANY ADDRESS</div>";
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

            Literal CompCode = new Literal();
            CompCode.Text = "<div class=\"item c1\">" + DataBinder.Eval(item.DataItem, "COMP_CODE").ToString() + "</div>";

            Literal CompName = new Literal();
            CompName.Text = "<div class=\"item c2\">" + DataBinder.Eval(item.DataItem, "VC_COMPANYNAME").ToString() + "</div>";

            Literal CompAddress = new Literal();
           CompAddress.Text = " <div class=\"item c3\">" + DataBinder.Eval(item.DataItem, "Address").ToString() + "</div>";
            templatePlaceHolder.Controls.Add(CompCode);
            templatePlaceHolder.Controls.Add(CompName);
            templatePlaceHolder.Controls.Add(CompAddress);

        }

    }

    protected void ComboBox1_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string CompCode = ComboBox1.SelectedValue.Trim();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindCompCode('" + CompCode + "','" + CompCodeId + "')", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
}
