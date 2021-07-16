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


public partial class Inventory_SearchPO : System.Web.UI.Page
{
    private static string TextBoxId;
    private static string PartyCode;
    private static string txtICODE;
    private static string txtDESC;
    private static string txtUNIT;
    private static string txtQTY;
    private static string txtBasicRate;
    private static string txtFinalRate;
    private static string txtAmount;
    private static string PO_REF;
    private static string PO_TYPE;
    private ComboBox ComboBox1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["TextBoxId"] != null)
            TextBoxId = Request.QueryString["TextBoxId"].ToString();
        if (Request.QueryString["PartyCode"] != null)
            PartyCode = Request.QueryString["PartyCode"].ToString();
        if (Request.QueryString["txtICODE"] != null)
            txtICODE = Request.QueryString["txtICODE"].ToString();
        if (Request.QueryString["txtDESC"] != null)
            txtDESC = Request.QueryString["txtDESC"].ToString();
        if (Request.QueryString["txtUNIT"] != null)
            txtUNIT = Request.QueryString["txtUNIT"].ToString();
        if (Request.QueryString["txtQTY"] != null)
            txtQTY = Request.QueryString["txtQTY"].ToString();
        if (Request.QueryString["txtBasicRate"] != null)
            txtBasicRate = Request.QueryString["txtBasicRate"].ToString();
        if (Request.QueryString["txtFinalRate"] != null)
            txtFinalRate = Request.QueryString["txtFinalRate"].ToString();
        if (Request.QueryString["txtAmount"] != null)
            txtAmount = Request.QueryString["txtAmount"].ToString();
        if (Request.QueryString["PO_REF"] != null)
            PO_REF = Request.QueryString["PO_REF"].ToString();
        if (Request.QueryString["PO_TYPE"] != null)
            PO_TYPE = Request.QueryString["PO_TYPE"].ToString();

        ComboBox1 = new ComboBox();
        ComboBox1.ID = "ComboBox1";
        ComboBox1.Width = Unit.Pixel(750);
        ComboBox1.Height = Unit.Pixel(350);
        ComboBox1.EnableLoadOnDemand = true;
        ComboBox1.DataTextField = "PO_NUMB";
        ComboBox1.DataValueField = "IN_ITEM_PU_TRN";

        ComboBox1.LoadingItems += ComboBox1_LoadingItems;
        ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        ComboBox1.AutoPostBack = true;

        ComboBox1.HeaderTemplate = new HeaderTemplate();
        ComboBox1.ItemTemplate = new ItemTemplate();
        ComboBox1.FooterTemplate = new FooterTemplate();

        ComboBox1Container.Controls.Add(ComboBox1);

    }

    protected DataTable GetPO(string text, int startOffset, int numberOfItems)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        string whereClause = " where pm.PO_TYPE=:PO_TYPE and pt.ITEM_CODE = i.ITEM_CODE and pm.PO_NUMB=PT.PO_NUMB and PM.PRTY_CODE=:PRTY_CODE and pt.PO_NUMB like :SearchQuery and pt.ITEM_CODE like :SearchQuery";
        string sortExpression = " ORDER BY PO_NUMB";

        //  string commandText = "SELECT TOP " + numberOfItems + " * FROM TX_ITEM_MST";
        //string commandText = "SELECT * FROM TBLCOMPANYMASTER";

        string commandText = "SELECT pt.IN_ITEM_PU_TRN, pt.PO_NUMB, pt.ITEM_CODE, pt.ORD_QTY, pt.UOM, pt.IN_CURRENCYCODE, pt.CONV_RATE, pt.DEL_DATE,PM.PRTY_CODE,pt.COMMENTS, pt.BASIC_RATE, pt.FINAL_RATE, pt.PRC_TYPE, i.ITEM_DESC,nvl(PT.QTY_RCPT,0) QTY_RCPT,nvl(PT.ORD_QTY,0)-nvl(PT.QTY_RCPT,0) as QTY_REM FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i ,tx_item_pu_mst pm ";
        commandText += whereClause;
        if (startOffset != 0)
        {
            commandText += " AND PO_NUMB NOT IN (SELECT TOP " + startOffset + " PO_NUMB FROM TX_ITEM_PU_MST";
            commandText += whereClause + sortExpression + ")";
        }

        commandText += sortExpression;

        OracleCommand cmd = new OracleCommand(commandText, con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';
        cmd.Parameters.Add(":PRTY_CODE", OracleType.VarChar, 50).Value = PartyCode;
        cmd.Parameters.Add(":PO_TYPE", OracleType.VarChar, 50).Value = PO_TYPE;

        OracleDataAdapter da = new OracleDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);

        con.Close();

        return ds.Tables[0];
    }

    protected int GetPOCount(string text)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
        con.Open();

        OracleCommand cmd = new OracleCommand("SELECT count(*) FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i ,tx_item_pu_mst pm  where pt.ITEM_CODE = i.ITEM_CODE and pm.PO_NUMB=PT.PO_NUMB and PM.PRTY_CODE=:PRTY_CODE and pt.PO_NUMB like :SearchQuery and pt.ITEM_CODE like :SearchQuery", con);
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 50).Value = text + '%';
        cmd.Parameters.Add(":PRTY_CODE", OracleType.VarChar, 50).Value = PartyCode;

        return int.Parse(cmd.ExecuteScalar().ToString());
    }

    protected void ComboBox1_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        // Getting the items
        DataTable data = GetPO(e.Text, e.ItemsOffset, 10);

        ComboBox1.DataSource = data;
        ComboBox1.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetPOCount(e.Text);
    }

    public class HeaderTemplate : ITemplate
    {

        public void InstantiateIn(Control container)
        {

            Literal header1 = new Literal();
            header1.Text = "<div class=\"header c1\">PO No.</div>";
            container.Controls.Add(header1);

            Literal header2 = new Literal();
            header2.Text = "<div class=\"header c2\">Item Code</div>";
            container.Controls.Add(header2);

            Literal header3 = new Literal();
            header3.Text = "<div class=\"header c3\">Description</div>";
            container.Controls.Add(header3);

            Literal header4 = new Literal();
            header4.Text = "<div class=\"header c4\">Quantity</div>";
            container.Controls.Add(header4);

            Literal header5 = new Literal();
            header5.Text = "<div class=\"header c5\">Quantity Received</div>";
            container.Controls.Add(header5);

            Literal header6 = new Literal();
            header6.Text = "<div class=\"header c6\">Quantity remaining</div>";
            container.Controls.Add(header6);
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

            Literal L1 = new Literal();
            L1.Text = "<div class=\"item c1\">" + DataBinder.Eval(item.DataItem, "PO_NUMB").ToString() + "</div>";

            Literal L2 = new Literal();
            L2.Text = "<div class=\"item c2\">" + DataBinder.Eval(item.DataItem, "ITEM_CODE").ToString() + "</div>";

            Literal L3 = new Literal();
            L3.Text = "<div class=\"item c3\">" + DataBinder.Eval(item.DataItem, "ITEM_DESC").ToString() + "</div>";

            Literal L4 = new Literal();
            L4.Text = "<div class=\"item c4\">" + DataBinder.Eval(item.DataItem, "ORD_QTY").ToString() + "</div>";

            Literal L5 = new Literal();
            L5.Text = "<div class=\"item c5\">" + DataBinder.Eval(item.DataItem, "QTY_RCPT").ToString() + "</div>";

            Literal L6 = new Literal();
            L6.Text = "<div class=\"item c6\">" + DataBinder.Eval(item.DataItem, "QTY_REM").ToString() + "</div>";

            templatePlaceHolder.Controls.Add(L1);
            templatePlaceHolder.Controls.Add(L2);
            templatePlaceHolder.Controls.Add(L3);
            templatePlaceHolder.Controls.Add(L4);
            templatePlaceHolder.Controls.Add(L5);
            templatePlaceHolder.Controls.Add(L6);
        }
    }
    protected void ComboBox1_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string Code = ComboBox1.SelectedValue.Trim();

            //OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString);
            //con.Open();
            //string query = "SELECT pt.IN_ITEM_PU_TRN, pt.PO_NUMB, pt.ITEM_CODE, pt.ORD_QTY, pt.UOM, pt.IN_CURRENCYCODE, pt.CONV_RATE, pt.DEL_DATE, pt.COMMENTS, pt.BASIC_RATE,PM.PRTY_CODE, pt.FINAL_RATE, pt.PRC_TYPE, i.ITEM_DESC,nvl(PT.QTY_RCPT,0) QTY_RCPT,nvl(PT.ORD_QTY,0)-nvl(PT.QTY_RCPT,0) as QTY_REM  FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i ,tx_item_pu_mst pm  where pt.ITEM_CODE = i.ITEM_CODE and pm.PO_NUMB=PT.PO_NUMB and  PT.IN_ITEM_PU_TRN =:Code";
            //OracleCommand cmd = new OracleCommand(query, con);
            //cmd.Parameters.Add(":Code", OracleType.VarChar, 50).Value = Code;

            //OracleDataAdapter da = new OracleDataAdapter(cmd);

            //DataSet ds = new DataSet();
            //da.Fill(ds);

            //con.Close();
            //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindPO('" + Code + "','" + TextBoxId + "','" + txtICODE + "','" + txtDESC + "','" + txtUNIT + "','" + txtQTY + "','" + txtBasicRate + "','" + txtFinalRate + "','" + txtAmount + "','" + ds.Tables[0].Rows[0]["ITEM_CODE"].ToString().Trim() + "','" + ds.Tables[0].Rows[0]["ITEM_DESC"].ToString().Trim() + "','" + ds.Tables[0].Rows[0]["UOM"].ToString().Trim() + "','" + ds.Tables[0].Rows[0]["QTY_REM"].ToString().Trim() + "','" + ds.Tables[0].Rows[0]["BASIC_RATE"].ToString().Trim() + "','" + ds.Tables[0].Rows[0]["FINAL_RATE"].ToString().Trim() + "')", true);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindPOOnly('" + Code + "','" + TextBoxId + "','" + txtICODE + "','" + ds.Tables[0].Rows[0]["ITEM_CODE"].ToString().Trim() + "')", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindPOOnlyTT('" + Code + "','" + TextBoxId + "','" + PO_REF + "')", true);

            //}
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
}
