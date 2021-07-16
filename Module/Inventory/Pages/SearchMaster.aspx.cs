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


public partial class Inventory_SearchMaster : System.Web.UI.Page
{
    private static string MstNameId;
    private ComboBox ComboBox1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["MstNameId"] != null)
            MstNameId = Request.QueryString["MstNameId"].ToString();

        ComboBox1 =new ComboBox ();
        ComboBox1.ID = "ComboBox1";
        ComboBox1.Width = Unit.Pixel(680);
        ComboBox1.Height = Unit.Pixel(250);
        ComboBox1.EnableLoadOnDemand = true;
        ComboBox1.DataTextField = "MST_DESC";
        ComboBox1.DataValueField = "MST_NAME";

        ComboBox1.LoadingItems += ComboBox1_LoadingItems;
        ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        ComboBox1.AutoPostBack = true;

        ComboBox1.HeaderTemplate = new HeaderTemplate();
        ComboBox1.ItemTemplate = new ItemTemplate();
        ComboBox1.FooterTemplate = new FooterTemplate();
        
        ComboBox1Container.Controls.Add(ComboBox1);
    }
    protected DataTable GetMaster(string text, int startOffset, int numberOfItems)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        string whereClause = " WHERE MST_NAME like :SearchQuery or MST_DESC like :SearchQuery ";
        string sortExpression = " ORDER BY MST_NAME";

        //  string commandText = "SELECT TOP " + numberOfItems + " * FROM TX_ITEM_MST";
        //string commandText = "SELECT * FROM TBLCOMPANYMASTER";

        string commandText = "select MST_NAME, MST_DESC from TX_MASTER_MST";
        commandText += whereClause;
        if (startOffset != 0)
        {
            commandText += " AND MST_NAME NOT IN (SELECT TOP " + startOffset + " MST_NAME FROM TX_MASTER_MST";
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
    protected int GetMasterCount(string text)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TX_MASTER_MST WHERE MST_NAME like :SearchQuery or MST_DESC like :SearchQuery", con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }
    protected void ComboBox1_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        // Getting the items
        DataTable data = GetMaster(e.Text, e.ItemsOffset, 10);

        ComboBox1.DataSource = data;
        ComboBox1.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetMasterCount(e.Text);
    }
    public class HeaderTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {

            Literal header = new Literal();
            header.Text = "<div class=\"header c1\">Master Name</div>";
            container.Controls.Add(header);

            Literal header2 = new Literal();
            header2.Text = "<div class=\"header c2\">Description</div>";
            container.Controls.Add(header2);

            


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

            Literal MasterName = new Literal();
            MasterName.Text = "<div class=\"item c1\">" + DataBinder.Eval(item.DataItem, "MST_NAME").ToString() + "</div>";

            Literal Desc = new Literal();
            Desc.Text = "<div class=\"item c2\">" + DataBinder.Eval(item.DataItem, "MST_DESC").ToString() + "</div>";
            
            templatePlaceHolder.Controls.Add(MasterName);
            templatePlaceHolder.Controls.Add(Desc);           

        }
    }
   protected void ComboBox1_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
        {
            try
            {
                string MasterName = ComboBox1.SelectedValue.Trim();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindMasterName('" + MasterName + "','" + MstNameId + "')", true);
            }
            catch (Exception ex)
            {
                errorLog.ErrHandler.WriteError(ex.Message);
            }

        }
}
