using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Obout.ComboBox;

public partial class CommonControls_LOV_STArticle : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string STCode, string STDesc, string STTKTNo, string Make);
    public event RefreshDataGridView OnTextChanged;

    private Unit _Width;
    public Unit Width
    {
        get
        {
            _Width = ddlSTArticle.Width;
            return _Width;
        }
        set
        {
            _Width = value;
            ddlSTArticle.Width = value;
        }
    }

    private Unit _MenuWidth;
    public Unit MenuWidth
    {
        get
        {
            _MenuWidth = ddlSTArticle.MenuWidth;
            return _MenuWidth;
        }
        set
        {
            _MenuWidth = value;
            ddlSTArticle.MenuWidth = value;
        }
    }

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlSTArticle.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlSTArticle.AutoPostBack = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlSTArticle.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlSTArticle.SelectedIndex = value;
        }
    }

    private bool _Enabled;
    public bool Enabled
    {
        get
        {
            _Enabled = ddlSTArticle.Enabled;
            return _Enabled;
        }
        set
        {
            _Enabled = value;
            ddlSTArticle.Enabled = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlSTArticle.SelectedText.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlSTArticle.SelectedText = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlSTArticle.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlSTArticle.SelectedValue = _SelectedValue;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading item detail.See error log for detail."));
        }

    }

    protected void ddlSTArticle_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetSTArticle(e.Text.ToUpper(), e.ItemsOffset);

            ddlSTArticle.Items.Clear();

            ddlSTArticle.DataSource = data;
            ddlSTArticle.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetSTArticleCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
        }
    }

    private DataTable GetSTArticle(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT YARN_CODE, YARN_DESC, YARN_DATA FROM (SELECT YARN_CODE, YARN_DESC,YARN_DATA FROM (SELECT YARN_CODE, YARN_DESC,(YARN_CODE||'@'||YARN_DESC||'@'||TKTNO||'@'||MAKE) YARN_DATA FROM YRN_MST WHERE yarn_cat = 'SEWING THREAD') ASD  WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE ASC) asd WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE yarn_cat = 'SEWING THREAD')ASD WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE ASC) asd WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by YARN_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetSTArticleCount(string text)
    {

        string CommandText = " SELECT YARN_CODE, YARN_DESC FROM (SELECT YARN_CODE, YARN_DESC FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE yarn_cat = 'SEWING THREAD') ASD  WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE ASC) asd ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlSTArticle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string ARTICLE_CODE = string.Empty;
            string Description = string.Empty;
            string TKT_NO = string.Empty;
            string MAKE = string.Empty;

            char[] splitter = { '@' };
            string[] arrString = ddlSTArticle.SelectedValue.Split(splitter);
            ARTICLE_CODE = arrString[0].ToString();
            Description = arrString[1].ToString();
            TKT_NO = arrString[2].ToString();
            MAKE = arrString[3].ToString();

            OnTextChanged(ARTICLE_CODE.Trim(), Description.Trim(), TKT_NO.Trim(), MAKE.Trim());
        }
        catch
        {
            throw;
        }
    }

    public void SetIndexByValue(string Value)
    {
        try
        {

            string CommandText = "SELECT YARN_CODE, YARN_DESC FROM (SELECT YARN_CODE, YARN_DESC FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE yarn_cat = 'SEWING THREAD') ASD  WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE ASC) ";
            string WhereClause = " ";
            string SortExpression = " order by YARN_CODE asc";
            string SearchQuery = "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            ddlSTArticle.DataSource = data;
            ddlSTArticle.DataTextField = "YARN_CODE";
            ddlSTArticle.DataValueField = "YARN_CODE";
            ddlSTArticle.DataBind();

            foreach (ComboBoxItem item in ddlSTArticle.Items)
            {
                if (item.Value == Value)
                {
                    ddlSTArticle.SelectedIndex = ddlSTArticle.Items.IndexOf(item);
                    break;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    public void SetIndexByText(string Text)
    {
        try
        {
            string CommandText = "SELECT YARN_CODE, YARN_DESC FROM (SELECT YARN_CODE, YARN_DESC FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE yarn_cat = 'SEWING THREAD') ASD  WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE ASC) ";
            string WhereClause = " ";
            string SortExpression = " order by YARN_CODE asc";
            string SearchQuery = "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            ddlSTArticle.DataSource = data;
            ddlSTArticle.DataTextField = "YARN_CODE";
            ddlSTArticle.DataValueField = "YARN_CODE";
            ddlSTArticle.DataBind();

            foreach (ComboBoxItem item in ddlSTArticle.Items)
            {
                if (item.Text == Text)
                {
                    ddlSTArticle.SelectedIndex = ddlSTArticle.Items.IndexOf(item);
                    break;
                }
            }
        }
        catch
        {
            throw;
        }
    }

}
