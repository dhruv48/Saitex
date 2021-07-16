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

public partial class CommonControls_LOV_CompanyLov : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlCompany.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlCompany.AutoPostBack = value;
        }
    }

    private bool _Enabled;
    public bool Enabled
    {
        get
        {
            _Enabled = ddlCompany.Enabled;
            return _Enabled;
        }
        set
        {
            _Enabled = value;
            ddlCompany.Enabled = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlCompany.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlCompany.SelectedIndex = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlCompany.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlCompany.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlCompany.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlCompany.SelectedValue = _SelectedValue;
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

            ddlCompany.Items.Clear();
        
            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlCompany.DataSource = dtItem;
                ddlCompany.DataTextField = "COMP_NAME";
                ddlCompany.DataValueField = "COMP_CODE";
                ddlCompany.DataBind();
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
            string whereClause = " WHERE COMP_CODE like :SearchQuery or COMP_NAME like :SearchQuery ";
            string sortExpression = " ORDER BY COMP_NAME";

            string commandText = "SELECT COMP_CODE,COMP_NAME FROM CM_COMPANY_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            OnTextChanged(ddlCompany.SelectedValue.Trim(), ddlCompany.SelectedItem.Text.Trim());
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
            ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(Value));

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
            ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByText(Text));

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
            return ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(Value));
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
            return ddlCompany.Items.IndexOf(ddlCompany.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }

}
