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

public partial class CommonControls_LOV_ApproveLRLOV : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlApproveLR.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlApproveLR.SelectedIndex = value;
        }
    }

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlApproveLR.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlApproveLR.AutoPostBack = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlApproveLR.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlApproveLR.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlApproveLR.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlApproveLR.SelectedValue = _SelectedValue;
        }
    }

    private Unit _Width;
    public Unit Width
    {
        get
        {
            _Width = ddlApproveLR.Width;
            return _Width;
        }
        set
        {
            _Width = value;
            ddlApproveLR.Width = _Width;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadData(string.Empty);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading item detail.See error log for detail."));
        }

    }

    public void LoadData(string SHADE_FAMILY_CODE)
    {
        try
        {
            DataTable dtItem = GetApproveLR("%", SHADE_FAMILY_CODE, "");

            ddlApproveLR.Items.Clear();
            ddlApproveLR.Items.Add(new ListItem("NA", "NA"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlApproveLR.DataSource = dtItem;
                ddlApproveLR.DataTextField = "DISP_DATA";
                ddlApproveLR.DataValueField = "DISP_DATA";
                ddlApproveLR.DataBind();
            }
        }
        catch
        {
            throw;
        }

    }

    public void LoadData(string SHADE_FAMILY_CODE, string SHADE_CODE)
    {
        try
        {
            DataTable dtItem = GetApproveLR("%", SHADE_FAMILY_CODE, SHADE_CODE);

            ddlApproveLR.Items.Clear();
            ddlApproveLR.Items.Add(new ListItem("NA", "NA"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlApproveLR.DataSource = dtItem;
                ddlApproveLR.DataTextField = "DISP_DATA";
                ddlApproveLR.DataValueField = "DISP_DATA";
                ddlApproveLR.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    public void LoadData(string PARTY_CODE,string SHADE_FAMILY_CODE, string SHADE_CODE)
    {
        try
        {
            DataTable dtItem = GetApproveLR("%", SHADE_FAMILY_CODE, SHADE_CODE, PARTY_CODE);

            ddlApproveLR.Items.Clear();
            ddlApproveLR.Items.Add(new ListItem("NA", "NA"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlApproveLR.DataSource = dtItem;
                ddlApproveLR.DataTextField = "DISP_DATA";
                ddlApproveLR.DataValueField = "DISP_DATA";
                ddlApproveLR.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetApproveLR(string text, string SHADE_FAMILY_CODE, string SHADE_CODE)
    {
        try
        {
            string whereClause = " WHERE IS_APPROVED=1 and LAB_DIP_NO like :SearchQuery";
            if (!SHADE_FAMILY_CODE.Equals(string.Empty))
            {
                if (string.Compare(SHADE_FAMILY_CODE, "SELECT", true) != 1)
                {
                    whereClause += " and SHADE_FAMILY_CODE='" + SHADE_FAMILY_CODE + "' ";
                }
            }
            if (!SHADE_CODE.Equals(string.Empty))
            {
                if (string.Compare(SHADE_CODE, "SELECT", true) != 1)
                {
                    whereClause += " and SHADE_CODE='" + SHADE_CODE + "' ";
                }
            }
            string sortExpression = " ORDER BY LAB_DIP_NO asc, LR_OPTION asc";

            string commandText = "SELECT LAB_DIP_NO,LR_OPTION, (LAB_DIP_NO||LR_OPTION) DISP_DATA, (COMP_CODE||'@'||BRANCH_CODE||'@'||YEAR||'@'||ORDER_NO||'@'||PRTY_CODE||'@'||LAB_DIP_NO||'@'||LR_OPTION) MAIN_DATA FROM ST_LABDIP_SUB_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetApproveLR(string text, string SHADE_FAMILY_CODE, string SHADE_CODE, string PRTY_CODE)
    {
        try
        {
            string whereClause = " WHERE IS_APPROVED=1 and LAB_DIP_NO like :SearchQuery";
            if (!SHADE_FAMILY_CODE.Equals(string.Empty))
            {
                if (string.Compare(SHADE_FAMILY_CODE, "SELECT", true) != 1)
                {
                    whereClause += " and SHADE_FAMILY_CODE='" + SHADE_FAMILY_CODE + "' ";
                }
            }
            if (!SHADE_CODE.Equals(string.Empty))
            {
                if (string.Compare(SHADE_CODE, "SELECT", true) != 1)
                {
                    whereClause += " and SHADE_CODE='" + SHADE_CODE + "' ";
                }
            }
            if (!PRTY_CODE.Equals(string.Empty))
            {
                if (string.Compare(PRTY_CODE, "SELECT", true) != 1)
                {
                    whereClause += " and PRTY_CODE='" + PRTY_CODE + "' ";
                }
            }

            string sortExpression = " ORDER BY LAB_DIP_NO asc, LR_OPTION asc";

            string commandText = "SELECT LAB_DIP_NO,LR_OPTION, (LAB_DIP_NO||LR_OPTION) DISP_DATA, (COMP_CODE||'@'||BRANCH_CODE||'@'||YEAR||'@'||ORDER_NO||'@'||PRTY_CODE||'@'||LAB_DIP_NO||'@'||LR_OPTION) MAIN_DATA FROM ST_LABDIP_SUB_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlApproveLR_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlApproveLR.AutoPostBack)
            {
                OnTextChanged(ddlApproveLR.SelectedValue.Trim(), ddlApproveLR.SelectedItem.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in LR Selection.See error log for detail."));
        }
    }

    public void SetIndexByValue(string Value)
    {
        try
        {
            ddlApproveLR.SelectedIndex = ddlApproveLR.Items.IndexOf(ddlApproveLR.Items.FindByValue(Value));

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
            ddlApproveLR.SelectedIndex = ddlApproveLR.Items.IndexOf(ddlApproveLR.Items.FindByText(Text));

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
            return ddlApproveLR.Items.IndexOf(ddlApproveLR.Items.FindByValue(Value));
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
            return ddlApproveLR.Items.IndexOf(ddlApproveLR.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }

}
