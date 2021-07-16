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

public partial class CommonControls_LOV_BranchLov : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlBranch.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlBranch.AutoPostBack = value;
        }
    }

    private bool _Enabled;
    public bool Enabled
    {
        get
        {
            _Enabled = ddlBranch.Enabled;
            return _Enabled;
        }
        set
        {
            _Enabled = value;
            ddlBranch.Enabled = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlBranch.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlBranch.SelectedIndex = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlBranch.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlBranch.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlBranch.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlBranch.SelectedValue = _SelectedValue;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                LoadData(oUserLoginDetail.COMP_CODE);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading item detail.See error log for detail."));
        }

    }

    public void LoadData(string CompanyCode)
    {
        try
        {
            DataTable dtItem = GetItems(CompanyCode, "%");

            ddlBranch.Items.Clear();

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlBranch.DataSource = dtItem;
                ddlBranch.DataTextField = "BRANCH_NAME";
                ddlBranch.DataValueField = "BRANCH_CODE";
                ddlBranch.DataBind();
            }
        }
        catch
        {
            throw;
        }

    }

    protected DataTable GetItems(string CompanyCode, string text)
    {
        try
        {
            string whereClause = " WHERE BRANCH_CODE like :SearchQuery or BRANCH_NAME like :SearchQuery ";
            string sortExpression = " ORDER BY BRANCH_NAME";

            string commandText = "SELECT * from (select BRANCH_CODE,BRANCH_NAME FROM CM_BRANCH_MST where COMP_CODE='" + CompanyCode + "' ) ";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (_AutoPostBack == true)
            {
                OnTextChanged(ddlBranch.SelectedValue.Trim(), ddlBranch.SelectedItem.Text.Trim());
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
            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(Value));

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
            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByText(Text));

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
            return ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(Value));
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
            return ddlBranch.Items.IndexOf(ddlBranch.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }

}
