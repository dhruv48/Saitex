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

public partial class Inventory_Indent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        //    DataFilter1.BeginFilter();
            // DataFilter1.AddNewFilter("CName", "LIKE", "Dav");
        }
    }

    void DataFilter1_OnFilterAdded()
    {
        try
        {
       
            DataSet ds = new DataSet();

        }
        catch (Exception e)
        {
        }
    }
}
