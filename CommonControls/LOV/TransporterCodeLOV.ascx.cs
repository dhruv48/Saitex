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

public partial class CommonControls_LOV_TransporterCode : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlTransproterLOV.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlTransproterLOV.AutoPostBack = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlTransproterLOV.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlTransproterLOV.SelectedIndex = value;
        }
    }

    private bool _Enabled;
    public bool Enabled
    {
        get
        {
            _Enabled = ddlTransproterLOV.Enabled;
            return _Enabled;
        }
        set
        {
            _Enabled = value;
            ddlTransproterLOV.Enabled = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlTransproterLOV.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlTransproterLOV.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlTransproterLOV.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlTransproterLOV.SelectedValue = _SelectedValue;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Transporter detail.See error log for detail."));
        }

    }

    private void LoadData()
    {
        try
        {
            DataTable dtItem = GetParty("%");

            ddlTransproterLOV.Items.Clear();
            ddlTransproterLOV.Items.Add(new ListItem("---Select---", "SELECT"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlTransproterLOV.DataSource = dtItem;
                ddlTransproterLOV.DataTextField = "DISP_DATA";
                ddlTransproterLOV.DataValueField = "PRTY_CODE";
                ddlTransproterLOV.DataBind();
            }
        }
        catch
        {
            throw;
        }

    }

    protected DataTable GetParty(string text)
    {
        try
        {
            string whereClause = " WHERE PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
            string sortExpression = " ORDER BY PRTY_NAME asc";

            string commandText = "select * from (SELECT   PRTY_CODE, PRTY_NAME, PRTY_ADD1, PRTY_ADD2, PRTY_CITY, PRTY_STATE,(   PRTY_CODE  || '-----'  || PRTY_NAME  || ', '  || PRTY_ADD1  || ', '  || PRTY_ADD2  || ', '  || PRTY_CITY  || ', '  || PRTY_STATE)    DISP_DATA, (   PRTY_CODE  || '@'  || PRTY_NAME  || '@'  || PRTY_ADD1  || '@'  || PRTY_ADD2  || '@'  || PRTY_CITY  || '@'  || PRTY_STATE)    PRTY_DATA  FROM   TX_VENDOR_MST  Where PRTY_GRP_CODE='Transporter')asd";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlTransproterLOV_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (_AutoPostBack == true)
            {
                OnTextChanged(ddlTransproterLOV.SelectedValue.Trim(), ddlTransproterLOV.SelectedItem.Text.Trim());
            }
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
            ddlTransproterLOV.SelectedIndex = ddlTransproterLOV.Items.IndexOf(ddlTransproterLOV.Items.FindByValue(Value));

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
            ddlTransproterLOV.SelectedIndex = ddlTransproterLOV.Items.IndexOf(ddlTransproterLOV.Items.FindByText(Text));

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
            return ddlTransproterLOV.Items.IndexOf(ddlTransproterLOV.Items.FindByValue(Value));
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
            return ddlTransproterLOV.Items.IndexOf(ddlTransproterLOV.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }
}
