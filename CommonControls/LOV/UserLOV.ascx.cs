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

public partial class CommonControls_LOV_UserLOV : System.Web.UI.UserControl
{
    public delegate void RefreshDropDownList(string Value, string Text);
    public event RefreshDropDownList OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlUser.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlUser.AutoPostBack = value;
        }
    }
    
    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlUser.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlUser.SelectedIndex = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlUser.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlUser.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlUser.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlUser.SelectedValue = _SelectedValue;
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
            DataTable dtItem = GetUser("%");

            ddlUser.Items.Clear();
            ddlUser.Items.Add(new ListItem("Select", "SELECT"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlUser.DataSource = dtItem;
                ddlUser.DataTextField = "DISP_DATA";
                ddlUser.DataValueField = "USER_CODE";
                ddlUser.DataBind();
            }
        }
        catch
        {
            throw;
        }

    }

    protected DataTable GetUser(string text)
    {
        try
        {
            string whereClause = " WHERE NVL(DEL_STATUS,0)=0  AND ( USER_CODE like :SearchQuery or USER_NAME like :SearchQuery) ";
            string sortExpression = " ORDER BY USER_CODE asc,USER_NAME asc  ";

            string commandText = "SELECT USER_CODE,USER_NAME,(USER_CODE||'-----'||USER_NAME) DISP_DATA FROM CM_USER_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUser.AutoPostBack)
            {

                OnTextChanged(ddlUser.SelectedValue.Trim(), ddlUser.SelectedItem.Text.Trim());
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
            ddlUser.SelectedIndex = ddlUser.Items.IndexOf(ddlUser.Items.FindByValue(Value));

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
            ddlUser.SelectedIndex = ddlUser.Items.IndexOf(ddlUser.Items.FindByText(Text));

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
            return ddlUser.Items.IndexOf(ddlUser.Items.FindByValue(Value));
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
            return ddlUser.Items.IndexOf(ddlUser.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }

}
