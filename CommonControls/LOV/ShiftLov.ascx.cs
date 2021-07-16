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

public partial class CommonControls_LOV_ShiftLov : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlShift.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlShift.AutoPostBack = value;
        }
    }

    private bool _Enabled;
    public bool Enabled
    {
        get
        {
            _Enabled = ddlShift.Enabled;
            return _Enabled;
        }
        set
        {
            _Enabled = value;
            ddlShift.Enabled = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlShift.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlShift.SelectedIndex = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlShift.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlShift.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlShift.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlShift.SelectedValue = _SelectedValue;
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

            ddlShift.Items.Clear();
          
            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlShift.DataSource = dtItem;
                ddlShift.DataTextField = "SFT_NAME";
                ddlShift.DataValueField = "SFT_ID";
                ddlShift.DataBind();
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
            string whereClause = " WHERE SFT_ID like :SearchQuery or SFT_NAME like :SearchQuery ";
            string sortExpression = " ORDER BY SFT_NAME";

            string commandText = "SELECT SFT_ID,SFT_NAME FROM HR_SFT_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            OnTextChanged(ddlShift.SelectedValue.Trim(), ddlShift.SelectedItem.Text.Trim());
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
            ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByValue(Value));

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
            ddlShift.SelectedIndex = ddlShift.Items.IndexOf(ddlShift.Items.FindByText(Text));

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
            return ddlShift.Items.IndexOf(ddlShift.Items.FindByValue(Value));
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
            return ddlShift.Items.IndexOf(ddlShift.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }

}
