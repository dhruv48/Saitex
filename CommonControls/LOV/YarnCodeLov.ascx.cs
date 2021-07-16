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

public partial class CommonControls_LOV_YarnCodeLov : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlYarncodeLov.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlYarncodeLov.AutoPostBack = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlYarncodeLov.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlYarncodeLov.SelectedIndex = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlYarncodeLov.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlYarncodeLov.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlYarncodeLov.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlYarncodeLov.SelectedValue = _SelectedValue;
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Yarn detail.See error log for detail."));
        }

    }

    private void LoadData()
    {
        try
        {
            DataTable dtItem = GetItems("%");

            ddlYarncodeLov.Items.Clear();
            ddlYarncodeLov.Items.Add(new ListItem("--------------- Select Yarn Code ---------------------", "SELECT"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlYarncodeLov.DataSource = dtItem;
                ddlYarncodeLov.DataTextField = "YARN_DATA";
                ddlYarncodeLov.DataValueField = "YARN_CODE";
                ddlYarncodeLov.DataBind();
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
            string whereClause = " WHERE YARN_CODE like :SearchQuery or YARN_TYPE like :SearchQuery or YARN_DESC like :SearchQuery";
            string sortExpression = " ORDER BY YARN_CODE";

            string commandText = "SELECT   *  FROM   (SELECT   YARN_CODE,  YARN_TYPE,  YARN_DESC,  UOM,  YARN_CAT,  (YARN_CODE || '-----' || YARN_DESC) YARN_DATA FROM   YRN_MST WHERE   YARN_CAT <> 'SEWING THREAD') a";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlYarncodeLov_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            OnTextChanged(ddlYarncodeLov.SelectedValue.Trim(), ddlYarncodeLov.SelectedItem.Text.Trim());
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
            ddlYarncodeLov.SelectedIndex = ddlYarncodeLov.Items.IndexOf(ddlYarncodeLov.Items.FindByValue(Value));

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
            ddlYarncodeLov.SelectedIndex = ddlYarncodeLov.Items.IndexOf(ddlYarncodeLov.Items.FindByText(Text));

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
            return ddlYarncodeLov.Items.IndexOf(ddlYarncodeLov.Items.FindByValue(Value));
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
            return ddlYarncodeLov.Items.IndexOf(ddlYarncodeLov.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }
}
