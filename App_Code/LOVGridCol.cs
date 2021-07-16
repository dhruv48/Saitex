using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for LOVGridCol
/// </summary>
[Serializable]
public class LOVGridCol
{
    string _HeaderName;
    string _DataField;
    Unit _Width;
    bool _useInsearch;
    bool _displayingrid;

    public string HeaderName
    {
        get { return _HeaderName; }
        set { _HeaderName = value; }
    }
    public string DataField
    {
        get { return _DataField; }
        set { _DataField = value; }
    }
    public Unit Width
    {
        get { return _Width; }
        set { _Width = value; }
    }
    public bool UseInSearch
    {
        get { return _useInsearch; }
        set { _useInsearch = value; }
    }
    public bool DisplayInGrid
    {
        get { return _displayingrid; }
        set { _displayingrid = value; }
    }

    public LOVGridCol()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public LOVGridCol(string _HeaderName, string _DataField, Unit _Width, bool _UseInSearch, bool _DisplayInGrid)
    {
        this._HeaderName = _HeaderName;
        this._DataField = _DataField;
        this._Width = _Width;
        this._useInsearch = _UseInSearch;
        this._displayingrid = _DisplayInGrid;
    }
}

public class DataGridTemplate : ITemplate
{
    ListItemType templateType;
    string columnName;
    string _DataField;
    string _PrimaryId;

    public DataGridTemplate(ListItemType type, string colname)
    {
        templateType = type;
        columnName = colname;
    }
    public DataGridTemplate(ListItemType type, string DataField, string PrimaryId)
    {
        templateType = type;
        _DataField = DataField;
        _PrimaryId = PrimaryId;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        Literal lc = new Literal();
        LinkButton lbtn = new LinkButton();
        switch (templateType)
        {
            case ListItemType.Header:
                lc.Text = "<B>" + columnName + "</B>";
                container.Controls.Add(lc);
                break;
            case ListItemType.Item:
                //lbtn.Text = "'<%# Bind('" + _DataField + "') %>'";
                //lbtn.CommandArgument = "'<%# Bind(" + _PrimaryId + ") %>'";
                //container.Controls.Add(lbtn);
                lbtn.Text = _DataField;
                lbtn.ID = "lbtn" + columnName;
                lbtn.CommandArgument = _PrimaryId;
                container.Controls.Add(lbtn);
                break;
        }
    }
}

