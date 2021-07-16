using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data.SqlClient;

public partial class CommonControls_ListOfValue : System.Web.UI.UserControl
{
    
    private int iUniqueIdCount = 0;
    private bool _AlreadyWhereClause;
    private string _WhereClause;
    private string _ConnectionString;
    private string _ProviderName;
    private string _SelectCommand;
    private string _PrimaryColName;
    private string _SearchOperator;
    private SqlDataSourceCommandType _SelectCommandType;

    public string ConnectionString
    {
        get { return _ConnectionString; }
        set { _ConnectionString = value; }
    }
    public string ProviderName
    {
        get { return _ProviderName; }
        set { _ProviderName = value; }
    }
    public string SelectCommand
    {
        get { return _SelectCommand; }
        set { _SelectCommand = value; }
    }
    public string PrimaryColName
    {
        get { return _PrimaryColName; }
        set { _PrimaryColName = value; }
    }
    public bool AlreadyWhereClause
    {
        get
        {
            if (_AlreadyWhereClause == null) _AlreadyWhereClause = false;
            return _AlreadyWhereClause;
        }
        set { _AlreadyWhereClause = value; }
    }
    public string SearchOperator
    {
        get { return _SearchOperator; }
        set { _SearchOperator = value; }
    }

    public SqlDataSourceCommandType SelectCommandType
    {
        get { return _SelectCommandType; }
        set { _SelectCommandType = value; }
    }

    private OracleCommand _cmd;
    public OracleCommand cmd
    {
        get { return _cmd; }
        set { _cmd = value; }
    }

    Dictionary<string, LOVGridCol> _GridColumnList;
    public Dictionary<string, LOVGridCol> GridColumnList
    {
        get { return _GridColumnList; }
        set { _GridColumnList = value; }
    }

    Object dataSource;
    public Object DataSource
    {
        get { return dataSource; }
        set { dataSource = value; }
    }

    public delegate void RefreshDataGridView(string Val);
    public event RefreshDataGridView OnTextChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
       // SelectedGridRow = -1;
        CreateGridView();
        Page.MaintainScrollPositionOnPostBack = true;
    }

    private DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        if (SelectCommand == "" || SelectCommand == null)
            SelectCommand = cmd.CommandText;
        if (cmd.Connection.State == ConnectionState.Closed)
            cmd.Connection.Open();

        cmd.CommandText = SelectCommand + _WhereClause;
        cmd.Parameters.Add(":SearchQuery", OracleType.VarChar, 100).Value = Common.CommonFuction.funFixQuotes(txtLov.Text.Trim()) + '%';

        OracleDataAdapter da = new OracleDataAdapter(cmd);

        da.Fill(dt);

        return dt;
    }

    private void CreateGridView()
    {

        GridView grdLovRuntime;
        string Id = "grdLovRuntime";
        if (GridAlreadyExists(pnlLovGrid, Id))
        {
            grdLovRuntime = (GridView)pnlLovGrid.FindControl(Id);
        }
        else
        {
            grdLovRuntime = new GridView();

            _WhereClause = "";
            if (GridColumnList.Count > 0)
            {
                foreach (LOVGridCol oLOVGridCol in GridColumnList.Values)
                {
                    if (oLOVGridCol.DisplayInGrid)
                    {
                        TemplateField tf1 = new TemplateField();
                        tf1.HeaderTemplate = new DataGridTemplate(ListItemType.Header, oLOVGridCol.HeaderName);
                        tf1.ItemTemplate = new DataGridTemplate(ListItemType.Item, oLOVGridCol.DataField, oLOVGridCol.DataField);

                        grdLovRuntime.Columns.Add(tf1);
                    }
                    if (oLOVGridCol.UseInSearch)
                    {
                        string sOperator = SearchOperator;
                        if (!AlreadyWhereClause)
                        {
                            sOperator = " where ";
                            AlreadyWhereClause = true;
                        }
                        _WhereClause = _WhereClause + sOperator + oLOVGridCol.DataField + " like :SearchQuery";
                    }
                }
            }
            grdLovRuntime.AutoGenerateColumns = false;
            grdLovRuntime.RowCommand += new GridViewCommandEventHandler(grdLovRuntime_RowCommand);
            pnlLovGrid.Controls.Add(grdLovRuntime);
        }
        DataTable dt = GetDataTable();
        grdLovRuntime.DataSource = dt;
        grdLovRuntime.Visible = true;
        grdLovRuntime.ID = Id;
        //   
        grdLovRuntime.DataBind();
        MapGridView(grdLovRuntime, dt);

    }

    void grdLovRuntime_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string PrimaryId = e.CommandArgument.ToString();
        OnTextChanged(PrimaryId);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        btnLovFillRuntime.Attributes.Add("style", "visibility :hidden");
    }

    protected void txtLov_TextChanged(object sender, EventArgs e)
    {
    }
    protected void btnLovFillRuntime_Click(object sender, EventArgs e)
    {
        CreateGridView();
        txtLov.Focus();

    }
    private void MapGridView(GridView grd, DataTable dt)
    {

        int GRow = grd.Rows.Count;
        int DRow = dt.Rows.Count;
        if (GRow == DRow)
        {
            for (int iLoop = 0; iLoop < GRow; iLoop++)
            {
                if (grd.Rows[iLoop].HasControls())
                {

                    foreach (Control c in grd.Rows[iLoop].Controls)
                    {
                        SetValueInGrid(c, iLoop, dt);
                    }
                }
            }
        }
    }
    private void SetValueInGrid(Control ctrl, int iRowCount, DataTable dt)
    {
        Random rnd = new Random();
        if (ctrl.HasControls())
        {
            foreach (Control c in ctrl.Controls)
            {
                SetValueInGrid(c, iRowCount, dt);
                if (c.GetType() == typeof(LinkButton))
                {
                    LinkButton lbtn = (LinkButton)c;
                    iUniqueIdCount = iUniqueIdCount + 1;
                    string ColName = lbtn.Text;
                    lbtn.ID = "lbtn" + iUniqueIdCount;
                    lbtn.Text = dt.Rows[iRowCount][ColName].ToString();
                    lbtn.CommandArgument = dt.Rows[iRowCount][PrimaryColName].ToString();
                }
            }
        }
    }
    private bool GridAlreadyExists(Control ctrl, string Id)
    {
        bool IsExists = false;
        if (ctrl.HasControls())
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c.FindControl(Id) != null)
                {
                    IsExists = true;
                }
                if (!IsExists)
                {
                    IsExists = GridAlreadyExists(c, Id);
                }
            }
        }
        return IsExists;
    }
    
}
