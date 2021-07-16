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

public partial class Admin_SearchBranch : System.Web.UI.Page
{
    private static string BranchCodeId = "";
    private ComboBox ComboBox1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["BranchCodeId"] != null)
        BranchCodeId = Request.QueryString["BranchCodeId"].ToString();

    ComboBox1 = new ComboBox();
    ComboBox1.ID = "ComboBox1";
    ComboBox1.Width = Unit.Pixel(680);
    ComboBox1.Height = Unit.Pixel(250);
    ComboBox1.EnableLoadOnDemand = true;
    ComboBox1.DataTextField = "VC_BRANCHNAME";
    ComboBox1.DataValueField = "CH_BRANCHCODE";

    ComboBox1.LoadingItems += ComboBox1_LoadingItems;
    ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
    ComboBox1.AutoPostBack = true;

    ComboBox1.HeaderTemplate = new HeaderTemplate();
    ComboBox1.ItemTemplate = new ItemTemplate();
    ComboBox1.FooterTemplate = new FooterTemplate();

    ComboBox1Container.Controls.Add(ComboBox1);

    }

    protected DataTable GetBranch(string text, int startOffset, int numberOfItems)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        string whereClause = " and b.VC_COMPANYNAME like :SearchQuery or a.CH_BRANCHCODE like :SearchQuery or a.VC_BRANCHNAME like :SearchQuery or a.VC_BRANCHADDRESS like :SearchQuery ";
        string sortExpression = " ORDER BY b.VC_COMPANYNAME,b.COMP_CODE,a.CH_BRANCHCODE";

        //  string commandText = "SELECT TOP " + numberOfItems + " * FROM TX_ITEM_MST";
        //string commandText = "SELECT * FROM TBLCOMPANYMASTER";

        string commandText = "select b.VC_COMPANYNAME VC_COMPANYNAME ,a.CH_BRANCHCODE CH_BRANCHCODE,a.VC_BRANCHNAME VC_BRANCHNAME,a.VC_BRANCHADDRESS VC_BRANCHADDRESS from TBLBRANCHMASTER a,TBLCOMPANYMASTER b where A.COMP_CODE =B.COMP_CODE ";
        commandText += whereClause;
        if (startOffset != 0)
        {
            commandText += " AND a.CH_BRANCHCODE NOT IN (SELECT TOP " + startOffset + " a.CH_BRANCHCODE FROM TBLBRANCHMASTER ";
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

    protected int GetBranchCount(string text)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();
        //OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TBLBRANCHMASTER WHERE CH_BRANCHCODE like :SearchQuery or VC_BRANCHNAME like :SearchQuery or VC_BRANCHADDRESS like :SearchQuery ", con);
        //OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TBLBRANCHMASTER a,TBLCOMPANYMASTER b WHERE b.VC_COMPANYNAME like :SearchQuery or a.CH_BRANCHCODE like :SearchQuery or a.VC_BRANCHNAME like :SearchQuery or a.VC_BRANCHADDRESS like :SearchQuery ", con);
        OracleCommand cmd = new OracleCommand("SELECT Count(*)  FROM (select  b.VC_COMPANYNAME, a.CH_BRANCHCODE, a.VC_BRANCHNAME , a.VC_BRANCHADDRESS from  TBLBRANCHMASTER a, TBLCOMPANYMASTER b WHERE A.COMP_CODE =B.COMP_CODE  or b.VC_COMPANYNAME like :SearchQuery or a.CH_BRANCHCODE like :SearchQuery or  a.VC_BRANCHNAME like :SearchQuery or  a.VC_BRANCHADDRESS like :SearchQuery)c ", con);
        
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }

    protected void ComboBox1_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        // Getting the items
        DataTable data = GetBranch(e.Text, e.ItemsOffset, 10);

        ComboBox1.DataSource = data;
        ComboBox1.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetBranchCount(e.Text);
    }

    public class HeaderTemplate : ITemplate
    {

        public void InstantiateIn(Control container)
        {

            Literal header = new Literal();
            header.Text = "<div class=\"header c1\">COMP NAME</div>";
            container.Controls.Add(header);

            Literal header2 = new Literal();
            header2.Text = "<div class=\"header c2\">BRANCH CODE</div>";
            container.Controls.Add(header2);

            Literal header3 = new Literal();
            header3.Text = "<div class=\"header c3\">BRANCH NAME</div>";
            container.Controls.Add(header3);

            Literal header4 = new Literal();
            header4.Text = "<div class=\"header c4\">BRANCH ADDRESS</div>";
            container.Controls.Add(header4);


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

            Literal CompName = new Literal();
            CompName.Text = "<div class=\"item c1\">" + DataBinder.Eval(item.DataItem, "VC_COMPANYNAME").ToString() + "</div>";

            Literal BranchCode = new Literal();
            BranchCode.Text = "<div class=\"item c2\">" + DataBinder.Eval(item.DataItem, "CH_BRANCHCODE").ToString() + "</div>";

            Literal BranchName= new Literal();
            BranchName.Text = " <div class=\"item c3\">" + DataBinder.Eval(item.DataItem, "VC_BRANCHNAME").ToString() + "</div>";

            Literal BranchAddress = new Literal();
            BranchAddress.Text = " <div class=\"item c3\">" + DataBinder.Eval(item.DataItem, "VC_BRANCHADDRESS").ToString() + "</div>";


            templatePlaceHolder.Controls.Add(CompName);
            templatePlaceHolder.Controls.Add(BranchCode);
            templatePlaceHolder.Controls.Add(BranchName);
            templatePlaceHolder.Controls.Add(BranchAddress);

        }

    }

    protected void ComboBox1_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string BranchCode = ComboBox1.SelectedValue.Trim();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindBranchCode('" + BranchCode + "','" + BranchCodeId + "')", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
}
