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

public partial class CommonControls_LOV_OrderNoLOV : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlOrderNo.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlOrderNo.AutoPostBack = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlOrderNo.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlOrderNo.SelectedIndex = value;
        }
    }

    private bool _Enabled;
    public bool Enabled
    {
        get
        {
            _Enabled = ddlOrderNo.Enabled;
            return _Enabled;
        }
        set
        {
            _Enabled = value;
            ddlOrderNo.Enabled = value;
        }
    }

    private bool _Visible;
    public bool Visible
    {
        get
        {
            _Visible = ddlOrderNo.Visible;
            return _Visible;
        }
        set
        {
            _Visible = value;
            ddlOrderNo.Visible = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlOrderNo.SelectedItem.Text.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlOrderNo.SelectedItem.Text = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlOrderNo.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlOrderNo.SelectedValue = _SelectedValue;
        }
    }

    private string _PRODUCT_TYPE;
    public string PRODUCT_TYPE
    {
        get
        {
            return _PRODUCT_TYPE;
        }
        set
        {
            _PRODUCT_TYPE = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
            
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading item detail.See error log for detail."));
        }

    }

    public void LoadData(string PRODUCT_TYPE)
    {
        try
        {
            DataTable dtItem = GetParty("%", PRODUCT_TYPE);

            ddlOrderNo.Items.Clear();
            ddlOrderNo.Items.Add(new ListItem("Select", "SELECT"));

            if (dtItem != null && dtItem.Rows.Count > 0)
            {
                ddlOrderNo.DataSource = dtItem;
                ddlOrderNo.DataTextField = "DISP_DATA";
                ddlOrderNo.DataValueField = "ORDER_STRING";
                ddlOrderNo.DataBind();
            }
        }
        catch
        {
            throw;
        }

    }

    private DataTable GetParty(string Text, string PRODUCT_TYPE)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
           
            string whereClause = " where BUSINESS_TYPE like :searchQuery or PRODUCT_TYPE like :searchQuery or PRTY_CODE like :searchQuery or ORDER_NO like :searchQuery or ORDER_TYPE like :searchQuery or PRTY_NAME like :searchQuery";
            string sortExpression = " order by ORDER_NO desc,PRODUCT_TYPE asc,BUSINESS_TYPE asc,ORDER_TYPE asc ";
            string commandText = "select * from (SELECT distinct O.COMP_CODE, O.BRANCH_CODE, O.BUSINESS_TYPE, O.PRODUCT_TYPE, O.ORDER_CAT, O.ORDER_TYPE, O.ORDER_NO,(O.ORDER_NO ||'    ----    '||O.PRTY_CODE)as DISP_DATA, O.COMP_CODE||'@'||O.BRANCH_CODE||'@'||O.BUSINESS_TYPE||'@'|| O.PRODUCT_TYPE||'@'|| O.ORDER_CAT ||'@'|| O.ORDER_TYPE||'@'|| O.ORDER_NO ORDER_STRING, O.PRTY_CODE, CASE WHEN O.PRTY_CODE = 'SELF' THEN 'SELF' ELSE ( O.PRTY_CODE|| ', '|| V.PRTY_NAME|| ', '|| V.PRTY_ADD1) END PRTY_NAME FROM OD_CAPT_MST O, tx_vendor_mst V ,OD_CAPT_TRN_MAIN T where  O.ORDER_NO=T.ORDER_NO  AND O.BRANCH_CODE=T.BRANCH_CODE  AND O.COMP_CODE=T.COMP_CODE AND  O.BUSINESS_TYPE=T.BUSINESS_TYPE AND O.PRTY_CODE = V.PRTY_CODE(+) and o.COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' and o.BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' and O.PRODUCT_TYPE='" + PRODUCT_TYPE + "' AND (T.BOM_FLAG='0'AND T.FINAL_ORDER_CONF_CLAG='0' AND T.COST_PRICE_FLAG='0' AND T.PROCESS_ROUTE_FLAG='0')) asd";

            string SearchQuery = Text + "%";

            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            OnTextChanged(ddlOrderNo.SelectedValue.Trim(), ddlOrderNo.SelectedItem.Text.Trim());
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
            ddlOrderNo.SelectedIndex = ddlOrderNo.Items.IndexOf(ddlOrderNo.Items.FindByValue(Value));

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
            ddlOrderNo.SelectedIndex = ddlOrderNo.Items.IndexOf(ddlOrderNo.Items.FindByText(Text));

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
            return ddlOrderNo.Items.IndexOf(ddlOrderNo.Items.FindByValue(Value));
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
            return ddlOrderNo.Items.IndexOf(ddlOrderNo.Items.FindByText(Text));
        }
        catch
        {
            throw;
        }
    }

}
