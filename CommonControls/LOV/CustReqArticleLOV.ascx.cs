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

public partial class CommonControls_LOV_CustReqArticleLOV : System.Web.UI.UserControl
{
    public delegate void RefreshDataGridView(string Value, string Text);
    public event RefreshDataGridView OnTextChanged;

    private bool _AutoPostBack;
    public bool AutoPostBack
    {
        get
        {
            _AutoPostBack = ddlCustReq.AutoPostBack;
            return _AutoPostBack;
        }
        set
        {
            _AutoPostBack = value;
            ddlCustReq.AutoPostBack = value;
        }
    }

    private int _SelectedIndex;
    public int SelectedIndex
    {
        get
        {
            _SelectedIndex = ddlCustReq.SelectedIndex;
            return _SelectedIndex;
        }
        set
        {
            _SelectedIndex = value;
            ddlCustReq.SelectedIndex = value;
        }
    }

    private bool _Enabled;
    public bool Enabled
    {
        get
        {
            _Enabled = ddlCustReq.Enabled;
            return _Enabled;
        }
        set
        {
            _Enabled = value;
            ddlCustReq.Enabled = value;
        }
    }

    private string _SelectedItem;
    public string SelectedItem
    {
        get
        {
            _SelectedItem = ddlCustReq.SelectedText.Trim();
            return _SelectedItem;
        }
        set
        {
            _SelectedItem = value;
            ddlCustReq.SelectedText = _SelectedItem;
        }
    }

    private string _SelectedValue;
    public string SelectedValue
    {
        get
        {
            _SelectedValue = ddlCustReq.SelectedValue.Trim();
            return _SelectedValue;
        }
        set
        {
            _SelectedValue = value;
            ddlCustReq.SelectedValue = _SelectedValue;
        }
    }

    private string _PI_TYPE;
    public string PI_TYPE
    {
        get
        {
            return _PI_TYPE;
        }
        set
        {
            _PI_TYPE = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //    LoadData(_PI_TYPE);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading item detail.See error log for detail."));
        }

    }

    protected void ddlCustReq_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetCRData(e.Text.ToUpper(), e.ItemsOffset, PI_TYPE);

            ddlCustReq.Items.Clear();

            ddlCustReq.DataSource = data;
            ddlCustReq.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetCRCount(e.Text, PI_TYPE);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
        }
    }

    private DataTable GetCRData(string Text, int startOffset, string PI_TYPE)
    {
        try
        {
            string CommandText = string.Empty;
            string WhereClause = string.Empty;
            string SortExpression = string.Empty;

            if (PI_TYPE == "YARN_SPINING" || PI_TYPE == "YARN_DYEING")
            {
                CommandText = "SELECT * FROM (SELECT '' as CUST_REQ_NO,'' as TKT_NO,'' as SHADE, 'YARN' AS PRODUCT_CAT, (Y.YARN_CODE || '@' || Y.UOM || '@' || Y.YARN_DESC||'@@@') SEARCH_DATA, Y.YARN_CODE CODE, Y.YARN_CAT ARTICLE_TYPE, Y.YARN_DESC DESCRIPTION, Y.YARN_CODE ARTICLE_CODE FROM   YRN_MST Y WHERE   YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery) ";
                WhereClause = " where PRODUCT_CAT ='YARN' AND ARTICLE_TYPE IN ('GRAY YARN' , 'DYED YARN') ";
            }
            else if (PI_TYPE == "GRAY_WEAV" || PI_TYPE == "FABR_PROC")
            {
                CommandText = "SELECT '' as CUST_REQ_NO,'' as TKT_NO,'' as SHADE,* FROM (SELECT 'YARN' AS PRODUCT_CAT, (Y.YARN_CODE || '@' || Y.UOM || '@' || Y.YARN_DESC||'@@@') SEARCH_DATA, Y.YARN_CODE CODE, Y.YARN_CAT ARTICLE_TYPE, Y.YARN_DESC DESCRIPTION, Y.YARN_CODE ARTICLE_CODE FROM YRN_MST Y WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery UNION SELECT 'FABRIC' AS PRODUCT_CAT, (Y.FABR_CODE || '@' || Y.UOM || '@' || Y.FDESC) SEARCH_DATA, Y.FABR_CODE CODE, Y.FABR_TYPE ARTICLE_TYPE, Y.FDESC DESCRIPTION, Y.FABR_CODE ARTICLE_CODE FROM TX_FABRIC_MST Y WHERE FABR_CODE LIKE :SearchQuery OR FDESC LIKE :SearchQuery UNION SELECT 'SEWING THREAD' AS PRODUCT_CAT, (Y.ARTICAL_CODE || '@' || '' || '@' || '') SEARCH_DATA, '' AS CODE, '' AS ARTICLE_TYPE, '' AS DESCRIPTION, Y.ARTICAL_CODE ARTICLE_CODE FROM ST_ARTICAL_MST Y WHERE ARTICAL_CODE LIKE :SearchQuery) ";
                WhereClause = " where PRODUCT_CAT ='FABRIC' ";
            }
            else if (PI_TYPE == "SEWING_THREAD")
            {
                CommandText = " SELECT DISTINCT * FROM (SELECT DISTINCT * FROM (SELECT DISTINCT cr.ORDER_NO CUST_REQ_NO, SHD.SHADE_NAME, ( Y.YARN_CODE|| '@'|| Y.UOM|| '@'|| Y.YARN_DESC|| '@'|| cr.ORDER_NO|| '@'|| cr.TKT_NO|| '@'|| cr.SHADE_CODE|| '@'|| cr.QUANTITY || '@'|| SHD.SHADE_NAME) SEARCH_DATA, 'YARN' AS PRODUCT_CAT, Y.YARN_CAT ARTICLE_TYPE, Y.YARN_CODE ARTICLE_CODE FROM OD_CUSTOMER_REQUEST_ST cr, YRN_MST Y, OD_SHADE_FAMILY_MST SHF, OD_SHADE_FAMILY_TRN SHD WHERE Y.YARN_CAT = 'SEWING THREAD' AND cr.ARTICLE_NO = Y.YARN_CODE AND CR.SHADE_FAMILY_CODE = SHF.SHADE_FAMILY_CODE(+) AND CR.SHADE_FAMILY_CODE = SHD.SHADE_FAMILY_CODE(+) AND CR.SHADE_CODE = SHD.SHADE_CODE(+) ORDER BY cr.order_no ASC, Y.YARN_CODE ASC) RR WHERE CUST_REQ_NO like:SearchQuery or ARTICLE_CODE LIKE :SearchQuery ) asd WHERE ROWNUM <= 15 ";
                WhereClause = string.Empty;
                if (startOffset != 0)
                {
                    WhereClause += " AND SEARCH_DATA NOT IN (SELECT DISTINCT SEARCH_DATA FROM (SELECT DISTINCT * FROM (SELECT DISTINCT cr.ORDER_NO CUST_REQ_NO, SHD.SHADE_NAME, ( Y.YARN_CODE|| '@'|| Y.UOM|| '@'|| Y.YARN_DESC|| '@'|| cr.ORDER_NO|| '@'|| cr.TKT_NO|| '@'|| cr.SHADE_CODE|| '@'|| cr.QUANTITY || '@'|| SHD.SHADE_NAME) SEARCH_DATA, 'YARN' AS PRODUCT_CAT, Y.YARN_CAT ARTICLE_TYPE, Y.YARN_CODE ARTICLE_CODE FROM OD_CUSTOMER_REQUEST_ST cr, YRN_MST Y, OD_SHADE_FAMILY_MST SHF, OD_SHADE_FAMILY_TRN SHD WHERE Y.YARN_CAT = 'SEWING THREAD' AND cr.ARTICLE_NO = Y.YARN_CODE AND CR.SHADE_FAMILY_CODE = SHF.SHADE_FAMILY_CODE(+) AND CR.SHADE_FAMILY_CODE = SHD.SHADE_FAMILY_CODE(+) AND CR.SHADE_CODE = SHD.SHADE_CODE(+) ORDER BY cr.order_no ASC, Y.YARN_CODE ASC) RR WHERE CUST_REQ_NO like:SearchQuery or ARTICLE_CODE LIKE :SearchQuery ) asd  WHERE ROWNUM <= '" + startOffset + "')";
                }

                SortExpression = " ORDER BY CUST_REQ_NO ASC, ARTICLE_CODE ASC";
            }

            string SearchQuery = Text + "%";

            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetCRCount(string text, string PI_TYPE)
    {

        string CommandText = "  SELECT DISTINCT * FROM (SELECT DISTINCT * FROM (SELECT DISTINCT cr.ORDER_NO CUST_REQ_NO, SHD.SHADE_NAME, ( Y.YARN_CODE|| '@'|| Y.UOM|| '@'|| Y.YARN_DESC|| '@'|| cr.ORDER_NO|| '@'|| cr.TKT_NO|| '@'|| cr.SHADE_CODE|| '@'|| cr.QUANTITY) SEARCH_DATA, 'YARN' AS PRODUCT_CAT, Y.YARN_CAT ARTICLE_TYPE, Y.YARN_CODE ARTICLE_CODE FROM OD_CUSTOMER_REQUEST_ST cr, YRN_MST Y, OD_SHADE_FAMILY_MST SHF, OD_SHADE_FAMILY_TRN SHD WHERE Y.YARN_CAT = 'SEWING THREAD' AND cr.ARTICLE_NO = Y.YARN_CODE AND CR.SHADE_FAMILY_CODE = SHF.SHADE_FAMILY_CODE(+) AND CR.SHADE_FAMILY_CODE = SHD.SHADE_FAMILY_CODE(+) AND CR.SHADE_CODE = SHD.SHADE_CODE(+) ORDER BY cr.order_no ASC, Y.YARN_CODE ASC) RR WHERE CUST_REQ_NO like:SearchQuery or ARTICLE_CODE LIKE :SearchQuery ) asd  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlCustReq_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (_AutoPostBack == true)
            {
                OnTextChanged(ddlCustReq.SelectedValue.Trim(), ddlCustReq.SelectedText.Trim());
            }
        }
        catch
        {
            throw;
        }
    }

    //public void SetIndexByValue(string Value)
    //{
    //    try
    //    {
    //        ddlCustReq.SelectedIndex = ddlCustReq.Items.IndexOf(ddlCustReq.Items.FindByValue(Value));

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public void SetIndexByText(string Text)
    //{
    //    try
    //    {
    //        ddlCustReq.SelectedIndex = ddlCustReq.Items.IndexOf(ddlCustReq.Items.FindByText(Text));

    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public int FindByValue(string Value)
    //{
    //    try
    //    {
    //        return ddlCustReq.Items.IndexOf(ddlCustReq.Items.FindByValue(Value));
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public int FindByText(string Text)
    //{
    //    try
    //    {
    //        return ddlCustReq.Items.IndexOf(ddlCustReq.Items.FindByText(Text));
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

}
