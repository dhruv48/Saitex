using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCFMain;
using System.Data;

public partial class CommonControls_QueryGridView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    public void CreateGridDynamic(List<DynamicGrid.DataTableProperty> _List)
    {
        try
        {
            if (_List != null && _List.Count > 0)
            {
                foreach (DynamicGrid.DataTableProperty _Property in _List)
                {
                    if (_Property.IsDisplay)
                    {
                        // Dynamically create field columns to display the desired
                        // fields from the data source. Create a TemplateField object 
                        // to display an author's first and last name.
                        TemplateField customField = new TemplateField();

                        // Create the dynamic templates and assign them to 
                        // the appropriate template property.
                        customField.ItemTemplate = new DynamicGrid.GridViewTemplate(DataControlRowType.DataRow, _Property.ColName, _Property.DataFieldName, _Property.IsFilter, _Property.IsLink, _Property.HorAlign, _Property.ColWidth, _Property.CommandName);
                        customField.HeaderTemplate = new DynamicGrid.GridViewTemplate(DataControlRowType.DataRow, _Property.ColName, _Property.DataFieldName, _Property.IsFilter, _Property.IsLink, _Property.HorAlign, _Property.ColWidth, _Property.CommandName);

                        if (_Property.IsDisplay)
                        {
                            customField.Visible = true;
                        }
                        else
                        {
                            customField.Visible = false;
                        }

                        // Add the field column to the Columns collection of the
                        // GridView control.
                        grdCustomView.Columns.Add(customField);

                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    public void BindGridView(string Query)
    {
        try
        {
            DynamicGridForQueryForm obj = new DynamicGridForQueryForm();
            DataTable dt = obj.GetDataByQuery(Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                grdCustomView.DataSource = dt;
                grdCustomView.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

}
