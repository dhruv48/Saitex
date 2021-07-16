using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for GridTemplate
/// </summary>

public class DynamicGrid
{
    // property to set DataTablePropertyList of the grid
    private List<DataTableProperty> _DataTablePropertyList;
    public List<DataTableProperty> DataTablePropertyList
    {
        get { return _DataTablePropertyList; }
        set { _DataTablePropertyList = value; }
    }

    public class GridViewTemplate : ITemplate
    {
        ////////A variable to hold the type of ListItemType.
        //////ListItemType _templateType;

        ////////A variable to hold the column name.
        //////string _columnName;

        ////////Constructor where we define the template type and column name.
        //////public GridViewTemplate(ListItemType type, string colname)
        //////{
        //////    //Stores the template type.
        //////    _templateType = type;

        //////    //Stores the column name.
        //////    _columnName = colname;
        //////}

        private DataControlRowType templateType;
        private string DataField;
        private string columnName;
        private bool Filter;
        private bool Link;
        private string commandName;
        private HorizontalAlign HorAlign;
        private Unit colWidth;
        private int iCountHead = 1;
        private int iCountItem = 1;

        public GridViewTemplate(DataControlRowType type, string colname, string datafield, bool filter, bool link, HorizontalAlign Align, Unit Width, string CommandName)
        {
            templateType = type;
            columnName = colname;
            DataField = datafield;
            Filter = filter;
            Link = link;
            HorAlign = Align;
            colWidth = Width;
            commandName = CommandName;
        }

        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (templateType)
            {
                case DataControlRowType.Header:
                    //Creates a new label control and add it to the container.
                    Label lbl = new Label();            //Allocates the new label object.
                    lbl.Text = columnName;             //Assigns the name of the column in the lable.
                    lbl.ID = "txt" + iCountHead;
                    container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                    iCountHead += 1;
                    break;

                case DataControlRowType.DataRow:
                    if (Link)
                    {
                        //Creates a new text box control and add it to the container.
                        LinkButton lbtn = new LinkButton();                            //Allocates the new text box object.
                        lbtn.ID = "lbtn" + iCountItem;
                        lbtn.ToolTip = DataField;
                        lbtn.CommandName = commandName;
                        lbtn.DataBinding += new EventHandler(lbtn_DataBinding);   //Attaches the data binding event.
                        container.Controls.Add(lbtn);                            //Adds the newly created textbox to the container.
                    }
                    else
                    {
                        //Creates a new text box control and add it to the container.
                        Label lblItem = new Label();                            //Allocates the new text box object.
                        lblItem.ID = "lblItem" + iCountItem;
                        lblItem.ToolTip = DataField;
                        lblItem.DataBinding += new EventHandler(lblItem_DataBinding); //Attaches the data binding event.
                        container.Controls.Add(lblItem);                            //Adds the newly created textbox to the container.
                    }
                    break;

                case DataControlRowType.Footer:
                    CheckBox chkColumn = new CheckBox();
                    chkColumn.ID = "Chk" + columnName;
                    container.Controls.Add(chkColumn);
                    break;
            }
        }

        /// <summary>
        /// This is the event, which will be raised when the binding happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblItem_DataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            GridViewRow container = (GridViewRow)lbl.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, lbl.ToolTip);
            if (dataValue != DBNull.Value)
            {
                lbl.Text = dataValue.ToString();
            }
        }

        /// <summary>
        /// This is the event, which will be raised when the binding happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lbtn_DataBinding(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;
            GridViewRow container = (GridViewRow)lbtn.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, lbtn.ToolTip);
            if (dataValue != DBNull.Value)
            {
                lbtn.Text = dataValue.ToString();
            }
        }
    }

    public class DataTableProperty
    {
        public DataTableProperty(string ColumnName, string FieldName, bool bFilter, bool bDisplay, bool bLink, HorizontalAlign HAlign, Unit ColumnWidth, string cmdName)
        {
            _ColName = ColumnName;
            _DataFieldName = FieldName;
            _IsFilter = bFilter;
            _IsDisplay = bDisplay;
            _IsLink = bLink;
            _HorAlign = HAlign;
            _ColWidth = ColumnWidth;
            _cname = cmdName;
        }

        private string _ColName;
        private string _DataFieldName;
        private bool _IsFilter;
        private bool _IsDisplay;
        private bool _IsLink;
        private HorizontalAlign _HorAlign;
        private Unit _ColWidth;
        private string _cname;

        public string ColName
        {
            get { return _ColName; }
            set { _ColName = value; }
        }

        public string CommandName
        {
            get { return _cname; }
            set { _cname = value; }
        }

        public string DataFieldName
        {
            get { return _DataFieldName; }
            set { _DataFieldName = value; }
        }

        public bool IsFilter
        {
            get { return _IsFilter; }
            set { _IsFilter = value; }
        }

        public bool IsDisplay
        {
            get { return _IsDisplay; }
            set { _IsDisplay = value; }
        }

        public bool IsLink
        {
            get { return _IsLink; }
            set { _IsLink = value; }
        }

        public HorizontalAlign HorAlign
        {
            get { return _HorAlign; }
            set { _HorAlign = value; }
        }

        public Unit ColWidth
        {
            get { return _ColWidth; }
            set { _ColWidth = value; }
        }

    }

}
