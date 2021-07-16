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

public partial class CommonControls_ItemCodeLOV : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlItemcodeLov.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlItemcodeLov.AutoPostBack = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlItemcodeLov.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlItemcodeLov.SelectedIndex = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlItemcodeLov.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlItemcodeLov.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlItemcodeLov.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlItemcodeLov.SelectedValue = _SelectedValue;
        }
    }

    private string _CAT_CODE;
    public string CAT_CODE
    {
        get
        {
            return _CAT_CODE;
        }
        set
        {
            _CAT_CODE = value;         
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                LoadData();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading item detail.See error log for detail."));
        }

    }
   
    private void LoadData()
   
    {
        try
        {
            DataTable dtItem = GetItems("%");

            ddlItemcodeLov.Items.Clear();
            ddlItemcodeLov.Items.Add(new ListItem("--------------- Select Item Code ---------------------", "SELECT"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlItemcodeLov.DataSource = dtItem;
                ddlItemcodeLov.DataTextField = "ITEM_DATA";
                ddlItemcodeLov.DataValueField = "ITEM_CODE";
                ddlItemcodeLov.DataBind();
            }
        }
        catch
        {
            throw;
        }

    }
    
    protected DataTable GetItems(string text)
    {
        try
        {
            string whereClause = " WHERE ITEM_CODE like :SearchQuery ";
            
            if (_CAT_CODE != string.Empty)
            {
                whereClause += " and CAT_CODE like '" + CAT_CODE + "'";
            }

            string sortExpression = " ORDER BY ITEM_CODE";

            string commandText = "SELECT ITEM_CODE,ITEM_TYPE,ITEM_DESC,UOM,CAT_CODE, (ITEM_CODE||'-----'||ITEM_DESC) ITEM_DATA FROM TX_ITEM_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlItemcodeLov_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            OnTextChanged(ddlItemcodeLov.SelectedValue.Trim(), ddlItemcodeLov.SelectedItem.Text.Trim());
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
            ddlItemcodeLov.SelectedIndex = ddlItemcodeLov.Items.IndexOf(ddlItemcodeLov.Items.FindByValue(Value));

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
            ddlItemcodeLov.SelectedIndex = ddlItemcodeLov.Items.IndexOf(ddlItemcodeLov.Items.FindByText(Text));

        }
        catch
        {
            throw;
        }
    }

    public int FindByValue(string Value)
    {
        try
        {
            return ddlItemcodeLov.Items.IndexOf(ddlItemcodeLov.Items.FindByValue(Value));
        }
        catch
        {
            throw;
        }
    }

    public int FindByText(string Text)
    {
        try
        {
            return ddlItemcodeLov.Items.IndexOf(ddlItemcodeLov.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }
}
