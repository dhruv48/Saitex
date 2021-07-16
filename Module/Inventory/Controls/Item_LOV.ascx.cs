using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Module_Inventory_Controls_Item_LOV : System.Web.UI.UserControl
{

    private int _PONumb = 0;
    private bool _ForPONumb = false;

    public int PONumb
    {
        get { return _PONumb; }
        set { _PONumb = value; }
    }
    public bool ForPONumb
    {
        get { return _ForPONumb; }
        set { _ForPONumb = value; }
    }

    private Unit _Width;
    private Unit _Height;
    private string _SelectedText;
    private string _SelectedValue;

    private string _Text;
    private string _Value;

    public string Text
    {
        get
        {
            _Text = cmb_Item_LOV.SelectedText;
            return _Text;
        }
        set
        {
            _Text = value;
            cmb_Item_LOV.SelectedText = _Text;
        }

    }
    public string Value
    {
        get
        {
            _Value = cmb_Item_LOV.SelectedValue;
            return _Value;
        }
        set
        {
            _Text = value;
            cmb_Item_LOV.SelectedText = _Text;
        }

    }
    public string SelectedText
    {
        get
        {
            _SelectedText = cmb_Item_LOV.SelectedText;
            return _SelectedText;
        }

    }
    public string SelectedValue
    {
        get
        {
            _SelectedValue = cmb_Item_LOV.SelectedValue;
            return _SelectedValue;
        }
    }
    public Unit Width
    {
        get { return _Width; }
        set
        {
            _Width = value;
            cmb_Item_LOV.Width = _Width;
        }
    }
    public Unit Height
    {
        get { return _Height; }
        set
        {
            _Height = value;
            cmb_Item_LOV.Height = _Height;
        }
    }

    public delegate void RefreshDataGridView(Item_LOV_EventArgs e);
    public event RefreshDataGridView SelectedIndexChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    // Handles the "LoadingItems" event of the ComboBox
    protected void cmb_Item_LOV_LoadingItems1(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    { // Getting the items
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
        cmb_Item_LOV.DataSource = data;
        cmb_Item_LOV.DataBind();

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
        string whereClause = " WHERE i.ITEM_CODE=pt.ITEM_CODE and pt.PO_NUMB=:PONumb and pt.ITEM_CODE like :SearchQuery";
        string sortExpression = " ORDER BY i.ITEM_CODE";

        string commandText = "SELECT i.*,pt.PO_TYPE FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i";

        string sPO = "";
        if (PONumb > 0)
            sPO = PONumb.ToString();
        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

        return dt;
    }

    protected int GetPOItemsCount(string text)
    {
        string Spo = "";
        if (PONumb > 0)
            Spo = PONumb.ToString();
        string CommandText = "SELECT count(*) FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i WHERE i.ITEM_CODE=pt.ITEM_CODE and pt.PO_NUMB=:PONumb and i.ITEM_CODE like :SearchQuery or i.ITEM_TYPE like :SearchQuery or i.ITEM_DESC like :SearchQuery";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetCountForLOV(CommandText, text + '%', Spo);

    }
    #endregion

    #region Indent Item LOV
    // General Items
    protected DataTable GetIndentedItems(string text, int startOffset, int numberOfItems)
    {
        string whereClause = " where ITEM_CODE like :SearchQuery or ITEM_DESC like :SearchQuery";
        string sortExpression = " ORDER BY ITEM_CODE";

        string commandText = "select  * from (SELECT i.*,pt.INDENT_TYPE FROM TX_ITEM_INDENT_TRN pt,TX_ITEM_MST i WHERE i.ITEM_CODE=pt.ITEM_CODE and nvl(pt.APPR_QTY,0)-nvl(PT.PUR_ADJ_QTY,0)<>0 ) asd";

        string sPO = "";

        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

        return dt;
    }

    protected int GetIndentItemsCount(string text)
    {
        string CommandText = "select count(*) from (SELECT i.*,pt.INDENT_TYPE FROM TX_ITEM_INDENT_TRN pt,TX_ITEM_MST i WHERE i.ITEM_CODE=pt.ITEM_CODE and nvl(pt.APPR_QTY,0)-nvl(PT.PUR_ADJ_QTY,0)<>0 ) asd  where ITEM_CODE like :SearchQuery or ITEM_DESC like :SearchQuery";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetCountForLOV(CommandText, text + '%', "");

    }
    #endregion


    #region General Item LOV
    // General Items
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        string whereClause = " WHERE ITEM_CODE like :SearchQuery or ITEM_TYPE like :SearchQuery or ITEM_DESC like :SearchQuery";
        string sortExpression = " ORDER BY ITEM_CODE";

        string commandText = "SELECT * FROM TX_ITEM_MST";

        string sPO = "";

        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

        return dt;
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = "SELECT COUNT(*) FROM TX_ITEM_MST WHERE ITEM_CODE like :SearchQuery or ITEM_TYPE like :SearchQuery or ITEM_DESC like :SearchQuery";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetCountForLOV(CommandText, text + '%', "");
    }
    #endregion

    protected void cmb_Item_LOV_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            Item_LOV_EventArgs Event = new Item_LOV_EventArgs();
            Event.SelectedText = cmb_Item_LOV.SelectedText;
            Event.SelectedValue = cmb_Item_LOV.SelectedValue;
            SelectedIndexChanged(Event);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }

    public class Item_LOV_EventArgs
    {
        private string _SelectedText;
        private string _SelectedValue;

        public string SelectedText
        {
            get { return _SelectedText; }
            set { _SelectedText = value; }
        }
        public string SelectedValue
        {
            get { return _SelectedValue; }
            set { _SelectedValue = value; }
        }
    }

}
