using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Module_HRMS_Controls_BUDGET_LOV : System.Web.UI.UserControl
{

    //private int _PONumb = 0;
    //private bool _ForPONumb = false;

    //public int PONumb
    //{
    //    get { return _PONumb; }
    //    set { _PONumb = value; }
    //}
    //public bool ForPONumb
    //{
    //    get { return _ForPONumb; }
    //    set { _ForPONumb = value; }
    //}

    private Unit _Width;
    private Unit _Height;
    private string _SelectedText;
    private string _SelectedValue;

    public string SelectedText
    {
        get
        {
            _SelectedText = cmb_BUDGET_LOV.SelectedText;
            return _SelectedText;
        }

    }
    public string SelectedValue
    {
        get
        {
            _SelectedValue = cmb_BUDGET_LOV.SelectedValue;
            return _SelectedValue;
        }
    }
    public Unit Width
    {
        get { return _Width; }
        set
        {
            _Width = value;
            cmb_BUDGET_LOV.Width = _Width;
        }
    }
    public Unit Height
    {
        get { return _Height; }
        set
        {
            _Height = value;
            cmb_BUDGET_LOV.Height = _Height;
        }
    }
    public delegate void RefreshDataGridView(BUDGET_LOV_EventArgs e);
    public event RefreshDataGridView SelectedIndexChanged;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cmb_BUDGET_LOV_LoadingItems1(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();   
      
        data = GetItems(e.Text, e.ItemsOffset, 10);
        cmb_BUDGET_LOV.DataSource = data;
        cmb_BUDGET_LOV.DataBind();   
        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        // Getting the total number of items that start with the typed text        
        e.ItemsCount = GetItemsCount(e.Text);       

    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        string whereClause = " WHERE  DEPT_CODE like :SearchQuery or YEAR like :SearchQuery or BUDGET_AMT like :SearchQuery";
        string sortExpression = " ORDER BY YEAR";

        string commandText = "SELECT * FROM HR_DEPT_BUDGET";
       
        DataTable dt = SaitexBL.Interface.Method.HR_DEPT_BUDGET.GetDataForLOV(commandText, whereClause, sortExpression,"", text + '%');

        return dt;
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = "SELECT COUNT(*) FROM HR_DEPT_BUDGET WHERE  DEPT_CODE like :SearchQuery or YEAR like :SearchQuery or BUDGET_AMT like :SearchQuery";
        return SaitexBL.Interface.Method.HR_DEPT_BUDGET.GetCountForLOV(CommandText, text + '%');        
        
    }

    protected void cmb_BUDGET_LOV_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            BUDGET_LOV_EventArgs Event = new BUDGET_LOV_EventArgs();
            Event.SelectedText = cmb_BUDGET_LOV.SelectedText;
            Event.SelectedValue = cmb_BUDGET_LOV.SelectedValue;
            SelectedIndexChanged(Event);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }

    public class BUDGET_LOV_EventArgs
    {
        private string _SelectedText;
        private string _SelectedValue;

        public string SelectedText
        {
            get { return _SelectedText; }
            set { _SelectedText = value; }
        }
        public string SelectedValue
        {
            get { return _SelectedValue; }
            set { _SelectedValue = value; }
        }
    }


}
